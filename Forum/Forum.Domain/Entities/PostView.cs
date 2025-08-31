namespace Forum.Domain.Entities
{
    public class PostView
    {
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Author { get; set; }
        public int TotalLikes { get; set; }
        public List<Comment> Comments { get; set; } = new();
        public List<string> Tags { get; set; } = new();
    }
}
