namespace Forum.Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Guid PostId { get; set; }
    }
}
