namespace LinkNest.Api.Registers
{
    //Used to register middleware (UseXyz()) into the WebApplication.
    public interface IWebApplicationRegisterar
    {
        void RegisterPipelineComponents(WebApplication app);

    }
}
