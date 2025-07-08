using LinkNest.Application.Abstraction.Messaging;
using LinkNest.Domain.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkNest.Application.Posts.UpdateCommentToPost
{
    internal record UpdateCommentCommand(Guid commandId,Content content):ICommand;
}
