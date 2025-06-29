
using LinkNest.Infrastructure;

namespace LinkNest.Api.Registers
{
    public class DependencyInjectionRegisterar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddInfrastructure(builder.Configuration);
        }
    }
}
