using LinkNest.Application.Abstraction.Messaging;
using LinkNest.Domain.Abstraction;

namespace LinkNest.Application.Posts.DeleteCommentToPost
{
    internal class DeleteCommentCommandHandler : ICommandHandler<DeleteCommentCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteCommentCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment= await unitOfWork.PostRep.GetCommentByIdAsync(request.CommentId);

            if (comment is null)
                return Result.Failure(["No Comment Found"]);

            unitOfWork.PostRep.DeleteComment(comment);
            await unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
