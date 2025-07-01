using LinkNest.Application.Abstraction.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkNest.Application.Posts.GetPost
{
    public sealed record GetPostQuery(Guid PostId):IQuery<GetPostResponse>;
}
