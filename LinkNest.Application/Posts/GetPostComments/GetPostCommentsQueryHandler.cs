using Dapper;
using LinkNest.Application.Abstraction.Data;
using LinkNest.Application.Abstraction.Messaging;
using LinkNest.Domain.Abstraction;

namespace LinkNest.Application.Posts.GetPostComments
{
    internal sealed class GetPostInteractionsQueryHandler : IQueryHandler<GetPostInteractionsQuery, GetPostInteractionsResponse>
    {
        private readonly ISqlConnectionFactory sqlConnectionFactory;

        public GetPostInteractionsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this.sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<GetPostInteractionsResponse>> Handle(GetPostInteractionsQuery request, CancellationToken cancellationToken)
        {
            using var connection= sqlConnectionFactory.CreateConnection();

           const string sql = """
                SELECT
                content as Content,
                to_char(created_at, 'YYYY-MM-DD HH24:MI:SS') AS CreatedAt,
                userProfileId as UserProfileId,
                from postComment
                where postId=@PostId
                """;

            var comments = (await connection.QueryAsync<CommentInfo>(sql, new { PostId = request.postId })).ToList();
            if (!comments.Any())
                return Result<GetPostInteractionsResponse>.Failure(["No Comments Found"]);

            var response = new GetPostInteractionsResponse()
            {
                PostComments = comments,
                PostId = request.postId
            };

            return  Result<GetPostInteractionsResponse>.Success(response);

        }
    }
}
