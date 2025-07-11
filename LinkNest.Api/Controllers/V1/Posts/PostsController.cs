using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace LinkNest.Api.Controllers.V1.Posts
{
    [ApiController]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
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
