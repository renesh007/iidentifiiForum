namespace Forum.Application.DTO.Post.Responses
{
    public class SummarizedPostResponse
    {
        public Guid PostId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int TotalComments { get; set; }
        public int TotalLikes { get; set; }
        public string Author { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
}