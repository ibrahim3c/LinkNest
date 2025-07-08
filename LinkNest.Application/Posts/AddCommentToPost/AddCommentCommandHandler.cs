using LinkNest.Application.Abstraction.Messaging;
using LinkNest.Domain.Abstraction;
using LinkNest.Domain.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkNest.Application.Posts.AddCommentToPost
{
    internal class AddCommentCommandHandler : ICommandHandler<AddCommentCommand, Guid>
    {
        private readonly IUnitOfWork unitOfWork;

        public AddCommentCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            var post=await unitOfWork.PostRep.GetByIdAsync(request.PostId);
            if (post == null)
                return Result<Guid>.Failure(["No Post Found"]);
        
            var user=await unitOfWork.userProfileRepo.GetByIdAsync(request.UserProfileId);   
            if(user is null)
                return Result<Guid>.Failure(["No User Found"]);

            var comment = PostComment.Create(request.Content, request.PostId, request.UserProfileId);
            post.AddComment(comment);

            await unitOfWork.SaveChangesAsync();
            return Result<Guid>.Success(comment.Guid);
        }
    }
}
