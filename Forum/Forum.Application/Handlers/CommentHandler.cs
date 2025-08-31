using Forum.Application.Exceptions;
using Forum.Application.Interfaces;
using Forum.Domain.Interfaces.Repository;

namespace Forum.Application.Handlers
{
    public class CommentHandler : ICommentHandler
    {
        private readonly ICommentRepository _commentRepository;

        public CommentHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<Guid> CreateCommentAsync(Guid postId, string content, Guid userId, CancellationToken cancellationToken)
        {
            Guid commentId = Guid.NewGuid();

            int resultCode = await _commentRepository.CreateCommentAsync(commentId, postId, content, userId, cancellationToken);

            switch (resultCode)
            {
                case 0:
                    return commentId;
                case 1001:
                    throw new PostNotFoundException();
                default:
                    throw new InvalidOperationException($"Unexpected error code: {resultCode}");
            }
        }
    }
}
