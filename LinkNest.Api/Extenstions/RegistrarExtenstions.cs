using LinkNest.Api.Registers;
using System.Reflection;

namespace LinkNest.Api.Extenstions
{
    public static class RegisterarExtenstions
    {
        public static void RegisterServicesFromAssembly(this WebApplicationBuilder builder, Assembly assembly)
        {
            var registers = assembly
            .GetTypes()
                .Where(t => typeof(IWebApplicationBuilderRegisterar).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .Select(Activator.CreateInstance)
                .Cast<IWebApplicationBuilderRegisterar>();

            foreach (var register in registers)
            {
                register.RegisterServices(builder);
            }
        }

        public static void RegisterPipelineFromAssembly(this WebApplication app, Assembly assembly)
        {
            var registers = assembly
                .GetTypes()
                .Where(t => typeof(IWebApplicationRegisterar).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IWebApplicationRegisterar>();

            foreach (var register in registers)
            {
                register.RegisterPipelineComponents(app);
            }
        }
    }
}
