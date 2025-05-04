using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace LinkNest.Api.Controllers.V1
{
    [ApiController]
    //[Route("posts/v{version:apiVersion}/[controller]")]
    [Route("posts/[controller]")]
    [ApiVersion("1.0")]
    public class PostsController:ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllPosts()
        {
            return Ok("this from V1");
        }
    }
}
