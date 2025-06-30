using Dapper;
using LinkNest.Application.Abstraction.Data;
using LinkNest.Application.Abstraction.IServices.Email;
using LinkNest.Infrastructure.Data;
using LinkNest.Infrastructure.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkNest.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration) {

            services.AddScoped<IEmailService, EmailService>();

            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ??throw new ArgumentNullException(nameof(configuration));

            #region EFCore
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            #endregion

            #region Dapper
            services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
            #endregion

            return services;
        }
    }
}
