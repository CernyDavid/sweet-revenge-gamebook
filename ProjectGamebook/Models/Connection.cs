namespace ProjectGamebook.Models
{
    public class Connection
    {
        public LocationId From { get; set; }
        public string TargetSpecialPage { get; set; } = null;
        public LocationId Target { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
