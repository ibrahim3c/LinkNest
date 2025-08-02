namespace LinkNest.Application.Abstraction.IServices
{
    public interface IEmailService
    {
        Task<string> SendAsync(string email, string v, string v1);
    }
}
