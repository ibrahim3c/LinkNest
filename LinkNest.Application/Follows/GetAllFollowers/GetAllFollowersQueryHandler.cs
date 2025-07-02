using Dapper;
using LinkNest.Application.Abstraction.Data;
using LinkNest.Application.Abstraction.Messaging;
using LinkNest.Application.UserProfiles.GetUserProfile;
using LinkNest.Domain.Abstraction;

namespace LinkNest.Application.Follows.GetAllFollowers
{
    internal sealed class GetAllFollowersQueryHandler : IQueryHandler<GetAllFollowersQuery, GetAllFollowersResponse>
    {
        private readonly ISqlConnectionFactory connectionFactory;

        public GetAllFollowersQueryHandler(ISqlConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<Result<GetAllFollowersResponse>> Handle(GetAllFollowersQuery request, CancellationToken cancellationToken)
        {
            using var connection= connectionFactory.CreateConnection();
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
                    follows f ON up.Guid = f.followerId
                WHERE 
                    f.followeeId = @UserProfileId;
                """;

            var follwers = (await connection.QueryAsync<GetUserProfileResponse>(sql, new { UserProfileId = request.UserProfileId })).ToList();
            if (!follwers.Any())
                return Result<GetAllFollowersResponse>.Failure(["No Followers Found"]);

            var response=new GetAllFollowersResponse()
            {
                FollowersInfo=follwers,
                UserProfileId=request.UserProfileId
            };
            return Result<GetAllFollowersResponse>.Success(response);
        }
    }
}
