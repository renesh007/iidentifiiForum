namespace Forum.Domain.Models
{
    public class FilterOptions
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Author { get; set; }
        public string? TagId { get; set; }
    }
}
