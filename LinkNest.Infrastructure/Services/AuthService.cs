using ApartmentBooking.Domain.Users;
using FluentValidation;
using LinkNest.Application.Abstraction.Helpers;
using LinkNest.Application.Abstraction.IServices;
using LinkNest.Application.DTOs.Auth;
using LinkNest.Domain.Abstraction;
using LinkNest.Domain.Identity;
using LinkNest.Domain.UserProfiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LinkNest.Infrastructure.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly IUnitOfWork uow;
        private readonly IOptionsMonitor<JWT> JWTConfigs;
        private readonly  IEmailService mailingService;
        private readonly IValidator<RegisterDTO> userRegistValidator;

        public AuthService(UserManager<AppUser> userManager
            , RoleManager<AppRole> roleManager
            , IUnitOfWork uow
            , IOptionsMonitor<JWT> JWTConfigs
            , IEmailService mailingService
            , IValidator<RegisterDTO> userRegisterValidator)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.uow = uow;
            this.JWTConfigs = JWTConfigs;
            this.mailingService = mailingService;
            this.userRegistValidator = userRegisterValidator;
        }
       
        private async Task<JwtSecurityToken> GenerateJwtTokenAsync(AppUser appUser)
        {

            var userClaims = await userManager.GetClaimsAsync(appUser);
            var roles = await userManager.GetRolesAsync(appUser);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                        new Claim(JwtRegisteredClaimNames.Sub, appUser.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
                        new Claim("uid", appUser.Id)
                    }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTConfigs.CurrentValue.SecretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: JWTConfigs.CurrentValue.Issuer,
                audience: JWTConfigs.CurrentValue.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(JWTConfigs.CurrentValue.ExpireAfterInMinute),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;


        }

        // JWT with RefreshToken
        private RefreshToken GenereteRefreshToken()
        {

            var randomNum = new byte[32];
            using var generator = RandomNumberGenerator.Create();
            generator.GetBytes(randomNum);

            return new RefreshToken
            {
                ExpiresOn = DateTime.UtcNow.AddDays(15),
                Token = Convert.ToBase64String(randomNum),
                CreatedOn = DateTime.UtcNow
            };
        }

        public async Task<AuthResult> RegisterAsync(RegisterDTO userRegisterDTO)
        {
            var validationResult = userRegistValidator.Validate(userRegisterDTO);

            if (!validationResult.IsValid)
            {
                return new AuthResult()
                {
                    Success = false,
                    Messages = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }


            if (await userManager.FindByEmailAsync(userRegisterDTO.Email) is not null || await uow.userProfileRepo.IsEmailExist(userRegisterDTO.Email))
                return new AuthResult()
                {
                    Success = false,
                    Messages = new List<string> { "Email is already Registered!" }
                };


            //Create a userProfile
            var userProfile = UserProfile.Create(new FirstName(userRegisterDTO.Fname),
                new LastName(userRegisterDTO.Lname),
                new UserProfileEmail(userRegisterDTO.Email),
                userRegisterDTO.BirthDate,
                new CurrentCity(userRegisterDTO.CurrentCity));

            await uow.userProfileRepo.AddAsync(userProfile);
            await uow.SaveChangesAsync();


            // Create a new user
            var user = new AppUser
            {
                UserName = userRegisterDTO.Fname + userRegisterDTO.Lname, //TODO:
                Email = userRegisterDTO.Email,
                PhoneNumber = userRegisterDTO.PhoneNumber,
                UserProfileId=userProfile.Guid
            };

            var result = await userManager.CreateAsync(user, userRegisterDTO.Password);
            if (!result.Succeeded)
            {
                // Rollback profile creation if user creation fails
                uow.userProfileRepo.Remove(userProfile);
                await uow.SaveChangesAsync();
                return new AuthResult()
                {
                    Success = false,
                    Messages = result.Errors.Select(e => e.Description).ToList()
                };
            }

            // add role to user
            await userManager.AddToRoleAsync(user, Roles.UserRole);


            // generate token
            var token = await GenerateJwtTokenAsync(user);
            // generate refreshToken
            var refreshToken = GenereteRefreshToken();


            // then save it in db
            user.RefreshTokens.Add(refreshToken);
            await userManager.UpdateAsync(user);
            return new AuthResult()
            {
                Success = true,
                RefreshTokenExpiresOn = refreshToken.ExpiresOn,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken.Token
            };

        }

        public async Task<AuthResult> LoginWithRefreshTokenAsync(LoginDTO UserDTO)
        {
            // to include the RefreshTokens 
            var user = await userManager.Users
                                    .Include(u => u.RefreshTokens)
                                    .FirstOrDefaultAsync(u => u.Email == UserDTO.Email);
            if (user == null)
                return new AuthResult
                {
                    Success = false,
                    Messages = new List<string> { "Email or Password is incorrect" }
                };

            var result = await userManager.CheckPasswordAsync(user, UserDTO.Password);
            if (!result)
                return new AuthResult
                {
                    Success = false,
                    Messages = new List<string> { "Email or Password is incorrect" }
                };

            var token = await GenerateJwtTokenAsync(user);



            var authResult = new AuthResult()
            {
                Success = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };

            // check if user has already active refresh token 
            // so no need to give him new refresh token
            if (user.RefreshTokens.Any(r => r.IsActive))
            {
                // TODO: check this 
                var UserRefreshToken = user.RefreshTokens.FirstOrDefault(r => r.IsActive);
                authResult.RefreshToken = UserRefreshToken.Token;
                authResult.RefreshTokenExpiresOn = UserRefreshToken.ExpiresOn;
            }

            // if he does not
            // generate new refreshToken
            else
            {
                var refreshToken = GenereteRefreshToken();
                authResult.RefreshToken = refreshToken.Token;
                authResult.RefreshTokenExpiresOn = refreshToken.ExpiresOn;

                // then save it in db
                user.RefreshTokens.Add(refreshToken);
                await userManager.UpdateAsync(user);
            }

            return authResult;


        }

        public async Task<AuthResult> RefreshTokenAsync(string refreshToken)
        {
            // ensure there is user has this refresh token
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(r => r.Token == refreshToken));
            if (user == null)
            {
                return new AuthResult
                {
                    // u can don't add false=> cuz it's the default value 
                    Success = false,
                    Messages = ["InValid Token"]
                };
            }
            // ensure this token is active
            var oldRefreshToken = user.RefreshTokens.SingleOrDefault(t => t.Token == refreshToken);
            if (!oldRefreshToken.IsActive)
                return new AuthResult
                {
                    Success = false,
                    Messages = ["InValid Token"]
                };
            // if all things well
            //revoke old refresh token
            oldRefreshToken.RevokedOn = DateTime.UtcNow;

            // generate new refresh token and add it to db
            var newRefreshToken = GenereteRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await userManager.UpdateAsync(user);

            // generate new JWT Token
            var jwtToken = await GenerateJwtTokenAsync(user);

            return new AuthResult
            {
                Success = true,
                Messages = ["Refresh Token Successfully"],
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiresOn = newRefreshToken.ExpiresOn,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken)
            };

        }

        public async Task<bool> RevokeTokenAsync(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                return false;

            var user = await userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(r => r.Token == refreshToken));
            if (user == null)
                return false;

            var oldRefreshToken = user.RefreshTokens.SingleOrDefault(t => t.Token == refreshToken);
            if (!oldRefreshToken.IsActive)
                return false;


            oldRefreshToken.RevokedOn = DateTime.UtcNow;


            await userManager.UpdateAsync(user);


            return true;

        }



        //Forgot Password
        //public async Task<Result<string>> ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDTO, string scheme, string host)
        //{
        //    var user = await userManager.FindByEmailAsync(forgotPasswordDTO.Email);
        //    if (user == null)
        //        return Result<string>.Failure(["Email is incorrect!]");
        //    // generete token and  send it to user
        //    //await SendPasswordResetEmailAsync(user, scheme, host);
        //    await SendResetPasswordEmailAsync(user);


        //    return Result<string>.Success("Please go to your email and reset your password");

        //    // after that user click on link and go to frontend page that
        //    //1-capture userId, code
        //    //2-make form for user to reset new password
        //    // then user send data to reset password endpoint


        //}

        //private async Task SendPasswordResetEmailAsync(AppUser user, string scheme, string host)
        //{
        //    // Generate the password reset token
        //    var code = await userManager.GeneratePasswordResetTokenAsync(user);

        //    // Construct the reset link
        //    var callbackUrl = $"{scheme}://{host}/api/Account/ResetPassword?userId={user.Id}&code={Uri.EscapeDataString(code)}";
        //    // Send email with the reset link
        //    await mailingService.SendMailBySendGridAsync(user.Email, "Reset Your Password",
        //        $"Please reset your password by clicking this link: <a href='{callbackUrl}'>Reset Password</a>");
        //}

        //private async Task SendResetPasswordEmailAsync(AppUser user)
        //{
        //    // Generate the password reset token
        //    var code = await userManager.GeneratePasswordResetTokenAsync(user);

        //    // Construct the reset link
        //    var callbackUrl = $"https://full-stack-website-react-asp-net-eight.vercel.app/reset-password?userId={user.Id}&code={Uri.EscapeDataString(code)}";
        //    // Send email with the reset link
        //    await mailingService.SendMailBySendGridAsync(user.Email, "Reset Your Password",
        //        $"Please reset your password by clicking this link: <a href='{callbackUrl}'>Reset Password</a>");
        //}
       
        public async Task<Result<string>> ResetPasswordAsync(ResetPasswordDTO resetPasswordDto)
        {
            if (string.IsNullOrWhiteSpace(resetPasswordDto.UserId) || string.IsNullOrWhiteSpace(resetPasswordDto.code))
            {
                return Result<string>.Failure(["UserId and code are required"]);
            }

            var user = await userManager.FindByIdAsync(resetPasswordDto.UserId);
            if (user == null)
            {
                return Result<string>.Failure(["User not found"]);
            }

            // Decode the token before using it
            var decodedCode = Uri.UnescapeDataString(resetPasswordDto.code);

            var result = await userManager.ResetPasswordAsync(user, decodedCode, resetPasswordDto.NewPassword);
            if (result.Succeeded)
            {
                return Result<string>.Success("Password Reset successfully");
            }
            else
            {
                return Result<string>.Failure(["Error resetting password."]);
            }
        }


        /*

         Flow Between Frontend and Backend
            User Requests a Password Reset
                Frontend:
                    User enters their email in a "Forgot Password" form.
                    Frontend sends a POST request to ForgotPasswordAsync endpoint with the email.
                Backend (ForgotPasswordAsync)
                    Checks if the email exists.
                    Generates a password reset token.
                    Sends an email with a reset link (SendPasswordResetEmailAsync).
                    Returns a success message: "Please go to your email and reset your password".
            User Clicks the Reset Link in Email
                Frontend:
                    Extracts userId and code from the URL.
                    Displays a password reset form for the user to enter a new password.
            User Submits the New Password
                Frontend:
                    Sends a POST request to ResetPasswordAsync with userId, code, and newPassword.
                Backend (ResetPasswordAsync)
                    Validates input.
                    Finds the user.
                    Decodes the reset token.
                    Resets the password using userManager.ResetPasswordAsync().
                    Returns a success message: "Password Reset successfully".

         */

    }

}
