
namespace LinkNest.Api.Registers
{
    public class MiddlewareRegisterar : IWebApplicationRegisterar
    {
        public void RegisterPipelineComponents(WebApplication app)
        {

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
        }
    }
}
