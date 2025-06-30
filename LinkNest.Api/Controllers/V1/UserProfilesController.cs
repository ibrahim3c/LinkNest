using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace LinkNest.Api.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class UserProfilesController:ControllerBase
    {
    }
}
