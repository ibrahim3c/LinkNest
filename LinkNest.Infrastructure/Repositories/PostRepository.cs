using LinkNest.Domain.Posts;
using LinkNest.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkNest.Infrastructure.Repositories
{
    internal class PostRepository : IPostRepository
    {
        private readonly AppDbContext appDbContext;
        public PostRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task AddAsync(Post post)
        {
            await appDbContext.Set<Post>().AddAsync(post);
        }


        public async Task AddCommentAsync(PostComment comment)
        {
            await appDbContext.Set<PostComment>().AddAsync(comment);
        }


        public async Task AddInteractionAsync(PostInteraction interaction)
        {
            await appDbContext.Set<PostInteraction>().AddAsync(interaction);
        }


        public async Task<Post> GetByIdAsync(Guid PostId)
        {
            return await appDbContext.Set<Post>().FirstOrDefaultAsync(u => u.Guid == PostId);
        }

        public void Update(Post post)
        {
            appDbContext.Set<Post>().Update(post);
        }

        public void Delete(Post post)
        {
            appDbContext.Set<Post>().Remove(post);
        }

        public void UpdateComment(PostComment comment)
        {
            appDbContext.Set<PostComment>().Update(comment);
        }

        public void DeleteComment(PostComment comment)
        {
            appDbContext.Set<PostComment>().Remove(comment);
        }

        public void DeleteInteraction(PostInteraction interaction)
        {
            appDbContext.Set<PostInteraction>().Remove(interaction);
        }

        public async Task<PostComment> GetCommentByIdAsync(Guid commentId)
        {
            return await appDbContext.Set<PostComment>().FirstOrDefaultAsync(u => u.Guid == commentId);
        }

        public async Task<PostInteraction> GetInteractionByIdAsync(Guid interactionId)
        {
            return await appDbContext.Set<PostInteraction>().FirstOrDefaultAsync(u => u.Guid == interactionId);
        }
    }
}
