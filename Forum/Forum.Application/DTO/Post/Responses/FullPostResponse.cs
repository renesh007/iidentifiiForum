using Forum.Application.DTO.Comment;

namespace Forum.Application.DTO.Post.Responses
{
    public class FullPostResponse
    {
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Author { get; set; }
        public int TotalLikes { get; set; }
        public List<CommentResponse> Comments { get; set; } = new();
        public List<string> Tags { get; set; } = new List<string>();
    }
}