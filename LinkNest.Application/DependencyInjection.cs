using FluentValidation;
using LinkNest.Application.UserProfiles.AddUserProfile;
using Microsoft.Extensions.DependencyInjection;

namespace LinkNest.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly));
            //fluent validation
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly); 
            return services;
        }
    }
}
