
namespace LinkNest.Api.Registers
{
    public class SwaggerRegisterar : IWebApplicationBuilderRegisterar,IWebApplicationRegisterar
    {
        public void RegisterPipelineComponents(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }

        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen();
        }
    }
}
