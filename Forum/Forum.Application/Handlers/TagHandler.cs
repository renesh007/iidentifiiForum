using Forum.Application.Exceptions;
using Forum.Application.Interfaces;
using Forum.Domain.Interfaces.Repository;

namespace Forum.Application.Handlers
{
    public class TagHandler : ITagHandler
    {
        private readonly ITagRepository _tagRepository;

        public TagHandler(ITagRepository tagRepository, IPostRepository postRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task TagPostAsync(Guid postId, string tagType, Guid userId, CancellationToken cancellationToken)
        {
            Guid postTagId = Guid.NewGuid();
            int result = await _tagRepository.TagPostAsync(postTagId, postId, tagType, userId, cancellationToken);

            switch (result)
            {
                case 0:
                    return;
                case 1001:
                    throw new PostNotFoundException();
                case 1002:
                    throw new TagNotFoundException();
                case 1003:
                    throw new DuplicateTagException();
                default:
                    throw new InvalidOperationException($"Unexpected result code {result}");
            }

        }
    }
}
