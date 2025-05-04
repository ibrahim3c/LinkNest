namespace LinkNest.Api.Registers
{
    // Used to register services(AddXyz()) into the WebApplicationBuilder.
    public interface IWebApplicationBuilderRegisterar
    {
        void RegisterServices(WebApplicationBuilder builder);
    }
}
