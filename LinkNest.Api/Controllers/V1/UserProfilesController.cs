using Asp.Versioning;
using LinkNest.Application.UserProfiles.AddUserProfile;
using LinkNest.Application.UserProfiles.UpdateUserProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LinkNest.Api.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class UserProfilesController:ControllerBase
    {
        private readonly ISender sender;

        public UserProfilesController(ISender sender)
        {
            this.sender = sender;
        }
        [HttpGet]
        public IActionResult GetAllUserProfiles()
        {
            return Ok("this from V1");
        }

        [HttpGet("{id}")]
        public IActionResult GetUserProfileById(Guid id)
        {
            return Ok("this from V1");
        }

        [HttpPost]
        public async Task<IActionResult> AddUserProfile(AddUserProfileCommand command)
        {
            var result = await sender.Send(command);
            if(!result.IsSuccess)
                return BadRequest(result.Errors);

            return CreatedAtAction(nameof(GetUserProfileById), new { id = result.Value });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserProfile( Guid id, UpdateUserProfileCommand command)
        {
            if (id != command.Id) return BadRequest();

            var result = await sender.Send(command);

            if (!result.IsSuccess)
                return BadRequest(result.Errors);

            return Ok();
        }
    }
}
