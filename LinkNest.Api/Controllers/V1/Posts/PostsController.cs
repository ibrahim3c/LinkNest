using Asp.Versioning;
using LinkNest.Application.Follows.GetAllFollowees;
using LinkNest.Application.Posts.GetPost;
using LinkNest.Application.Posts.GetPostComments;
using LinkNest.Application.Posts.GetPostInteractions;
using LinkNest.Application.Posts.GetUserPosts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LinkNest.Api.Controllers.V1.Posts
{
    [ApiController]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class PostsController:ControllerBase
    {
        private readonly ISender sender;

        public PostsController(ISender sender)
        {
            this.sender = sender;
        }

        // GET: api/users/{userId}/posts
        [HttpGet("{userId}/posts")]
        public async Task<IActionResult> GetUserPosts(Guid userId)
        {
            var query=new GetAllFolloweesQuery(userId);
            var result=await sender.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return Ok(result);

        }

        // GET: api/posts/{postId}
        [HttpGet("/api/posts/{postId}")]
        public async Task<IActionResult> GetPost(Guid postId)
        {
            var query = new GetPostQuery(postId);
            var result = await sender.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result.Errors);

                return Ok(result);

        }

        // GET: api/posts/{postId}/interactions
        [HttpGet("/api/posts/{postId}/interactions")]
        public async Task<IActionResult> GetPostInteractions(Guid postId)
        {
            var query = new GetPostInteractionsQuery(postId);
            var result = await sender.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result.Errors);

            return Ok(result);
        }

        // GET: api/posts/{postId}/comments
        [HttpGet("/api/posts/{postId}/comments")]
        public async Task<IActionResult> GetPostComments(Guid postId)
        {
            var query = new GetPostCommentsQuery(postId);
            var result = await sender.Send(query);

            if (!result.IsSuccess)
                return BadRequest(result.Errors);

            return Ok(result);
        }

    }
}
