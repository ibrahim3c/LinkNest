using Dapper;
using LinkNest.Application.Abstraction.Data;
using LinkNest.Application.Abstraction.Messaging;
using LinkNest.Domain.Abstraction;

namespace LinkNest.Application.Posts.GetPost
{
    public sealed class GetPostQueryHandler : IQueryHandler<GetPostQuery, GetPostResponse>
    {
        private readonly ISqlConnectionFactory sqlConnectionFactory;

        public GetPostQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this.sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<GetPostResponse>> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            using var connection= sqlConnectionFactory.CreateConnection();
            const string sql = """
            SELECT 
                id AS Id,
                content AS Content,
                to_char(created_at, 'YYYY-MM-DD HH24:MI:SS') AS CreatedAt,
                image_url AS ImageUrl,
                user_profile_id AS UserProfileId
            FROM 
                post
            WHERE 
                id = @PostId
        """;

            var Response = await connection.QuerySingleOrDefaultAsync<GetPostResponse>(sql, new { request.PostId });

            if (Response is null)
                return Result<GetPostResponse>.Failure(["Post not found"]);

            return  Result<GetPostResponse>.Success(Response);
        }
    }
}
