namespace Forum.Domain.Entities
{
    public class Like
    {
        public int Id { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
    }
}
