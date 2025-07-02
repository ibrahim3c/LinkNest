using Dapper;
using LinkNest.Application.Abstraction.Data;
using LinkNest.Application.Abstraction.Messaging;
using LinkNest.Application.Posts.GetUserPosts;
using LinkNest.Application.UserProfiles.GetUserProfile;
using LinkNest.Domain.Abstraction;
using System.Data.Common;

namespace LinkNest.Application.UserProfiles.GetAllUserProfiles
{
    internal sealed class GetAllUserProfilesQueryHandler : IQueryHandler<GetAllUserProfilesQuery, List<GetUserProfileResponse>>
    {
        private readonly ISqlConnectionFactory sqlConnectionFactory;

        public GetAllUserProfilesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this.sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<List<GetUserProfileResponse>>> Handle(GetAllUserProfilesQuery request, CancellationToken cancellationToken)
        {
            using var connection=sqlConnectionFactory.CreateConnection();

            var sql = """
                select firstName as FirstName,
                lastName as LastName,
                email as Email
                dateOfBirth as DateOfBirth,
                createdOn as CreatedOn,
                currentCity as CurrentCity
                from UserProfile 
                """;
            var response =( await connection.QueryAsync<GetUserProfileResponse>(sql)).ToList();
            if (!response.Any())
                return Result<List<GetUserProfileResponse>>.Failure(["No User Profiles"]);

            return Result<List<GetUserProfileResponse>>.Success(response);
        }
    }
}
