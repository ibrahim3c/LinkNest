using Dapper;
using LinkNest.Application.Abstraction.Data;
using LinkNest.Application.Abstraction.Messaging;
using LinkNest.Application.UserProfiles.GetUserProfile;
using LinkNest.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkNest.Application.Follows.GetAllFollowees
{
    public sealed class GetAllFolloweesQueryHandler : IQueryHandler<GetAllFolloweesQuery, GetAllFolloweesRespones>
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public GetAllFolloweesQueryHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Result<GetAllFolloweesRespones>> Handle(GetAllFolloweesQuery request, CancellationToken cancellationToken)
        {
            using var connection= _connectionFactory.CreateConnection();
            var sql = """
                SELECT 
                    up.firstName AS FirstName,
                    up.lastName AS LastName,
                    up.email AS Email,
                    up.dateOfBirth AS DateOfBirth,
                    up.createdOn AS CreatedOn,
                    up.currentCity AS CurrentCity
                FROM 
                    userProfile up
                INNER JOIN 
                    follows f ON up.Guid = f.followeeId
                WHERE 
                    f.followerId = @UserProfileId;
                """;
            var followees = (await connection.QueryAsync<GetUserProfileResponse>(sql, new { UserProfileId = request.userProfileId })).ToList();
            if (!followees.Any())
                return Result<GetAllFolloweesRespones>.Failure(["No Followees Found"]);

            var response = new GetAllFolloweesRespones
            {
                FolloweesInfo = followees,
                UserProfileId = request.userProfileId
            };
            return Result<GetAllFolloweesRespones>.Success(response);
        }
    }
}
