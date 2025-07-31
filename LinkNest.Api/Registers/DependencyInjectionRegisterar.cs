
using LinkNest.Application;
using LinkNest.Infrastructure;

namespace LinkNest.Api.Registers
{
    public class DependencyInjectionRegisterar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            //Add configuration from the secret.json file
            builder.Configuration.AddJsonFile("Secret.json", optional: false, reloadOnChange: true);

            builder.Services.AddApplicationLayer();
            builder.Services.AddInfrastructure(builder.Configuration);
        }
    }
}
