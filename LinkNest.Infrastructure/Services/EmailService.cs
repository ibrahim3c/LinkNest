using LinkNest.Application.Abstraction.IServices;

namespace LinkNest.Infrastructure.Email
{
    internal sealed class EmailService : IEmailService
    {

        public Task<string> SendAsync(string email, string v, string v1)
        {
            throw new NotImplementedException();
        }
    }
}
