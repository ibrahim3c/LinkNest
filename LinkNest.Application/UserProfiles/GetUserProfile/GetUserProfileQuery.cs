using LinkNest.Application.Abstraction.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkNest.Application.UserProfiles.GetUserProfile
{
    internal sealed record GetUserProfileQuery(Guid userProfileId):IQuery<GetUserProfileResponse>;
}
