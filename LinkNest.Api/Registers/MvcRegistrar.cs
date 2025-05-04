
namespace LinkNest.Api.Registers
{
    public class MvcRegisterar : IWebApplicationBuilderRegisterar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
        }
    }
}
