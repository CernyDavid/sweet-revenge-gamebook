namespace ProjectGamebook.Models
{
    public class Location
    {
        public List<string> Texts { get; set; }
        public bool IsFight { get; set; }
        public Monster? Monster { get; set; }
        public string? Content { get; set; }
        public string ImageUrl { get; set; }

    }
}
