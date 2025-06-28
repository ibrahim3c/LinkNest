using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using LinkNest.Infrastructure.Email;
using LinkNest.Application.Services;
using LinkNest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkNest.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration) {

            services.AddScoped<IEmailService, EmailService>();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("LinkNestDb");
            });

            return services;
        }
    }
}
