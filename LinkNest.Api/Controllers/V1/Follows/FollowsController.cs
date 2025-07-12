using Asp.Versioning;
using LinkNest.Application.Follows.GetAllFollowers;
using LinkNest.Application.Posts.GetUserPosts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LinkNest.Api.Controllers.V1.Follows
{
    [ApiController]
    [Route("api/[controller")]
    [ApiVersion("1.0")]
    public class FollowsController:ControllerBase
    {
        private readonly ISender sender;

        public FollowsController(ISender sender)
        {
            this.sender = sender;
        }
        // GET: api/users/{followerId}/following
        [HttpGet("{followerId}/following")]
        public async Task<IActionResult> GetAllFollowing(Guid followerId)
        {
            var query = new GetUserPostsQuery(followerId);
            var result = await sender.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return Ok(result);
        }

        // GET: api/users/{followeeId}/followers
        [HttpGet("{followeeId}/followers")]
        public async Task<IActionResult> GetAllFollowers(Guid followeeId)
        {
            var query = new GetAllFollowersQuery(followeeId);
            var result = await sender.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return Ok(result);
        }

    }
}
