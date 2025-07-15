using FluentValidation;
using LinkNest.Application.Common.Behaviors;
using LinkNest.Application.UserProfiles.AddUserProfile;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LinkNest.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly));

            //fluent validation
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly,
                includeInternalTypes: true);

            return services;
        }
    }
}
