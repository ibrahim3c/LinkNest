using LinkNest.Application.Abstraction.Messaging;
using LinkNest.Domain.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkNest.Application.Posts.UpdateCommentToPost
{
    public record UpdateCommentCommand(Guid commandId,string content):ICommand;
}
