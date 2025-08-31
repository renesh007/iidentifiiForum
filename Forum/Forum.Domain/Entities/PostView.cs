
namespace Forum.Domain.Entities
{
    public class PostView
    {
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Author { get; set; }
        public int TotalLikes { get; set; }
        public List<CommentView> Comments { get; set; } = new();
        public List<string> Tags { get; set; } = new();

    }

    public class CommentView
    {
        public Guid CommentId { get; set; }
        public string CommentContent { get; set; }
        public DateTime CommentCreatedOn { get; set; }
        public string CommentAuthor { get; set; }
    }
}
