using LinkNest.Application.Abstraction.Messaging;
using LinkNest.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkNest.Application.Posts.UpdateCommentToPost
{
    internal class UpdateCommentCommandHandler : ICommandHandler<UpdateCommentCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateCommentCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await unitOfWork.PostRep.GetCommentByIdAsync(request.commandId);
            if (comment == null)
                return Result.Failure(["No Command Found"]);

            var post = await unitOfWork.PostRep.GetByIdAsync(comment.PostId);
            if(post is null)
                return Result.Failure(["No Post found for this comment"]);

            // to be continue
            return  Result.Success();

        }
    }
}
