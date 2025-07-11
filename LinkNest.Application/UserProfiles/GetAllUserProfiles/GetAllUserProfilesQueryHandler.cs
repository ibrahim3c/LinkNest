using Dapper;
using LinkNest.Application.Abstraction.Data;
using LinkNest.Application.Abstraction.Messaging;
using LinkNest.Application.UserProfiles.GetUserProfile;
using LinkNest.Domain.Abstraction;

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
                SELECT 
                    "Guid" AS UserProfileId,
                    "FirstName" AS FirstName,
                    "LastName" AS LastName,
                    "Email" AS Email,
                    "DateOfBirth" AS DateOfBirth,
                    "CreatedOn" AS CreatedOn,
                    "CurrentCity" AS CurrentCity
                FROM public."UserProfile"
                """;
            var response =( await connection.QueryAsync<GetUserProfileResponse>(sql)).ToList();
            if (!response.Any())
                return Result<List<GetUserProfileResponse>>.Failure(["No User Profiles"]);

            return Result<List<GetUserProfileResponse>>.Success(response);
        }
    }
}
