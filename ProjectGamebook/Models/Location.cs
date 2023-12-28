namespace ProjectGamebook.Models
{
    public class Location
    {
        public Location(List<string> texts, string imageUrl, Monster? monster = null, bool isFight = false)
        {
            Texts = texts;
            IsFight = isFight;
            Monster = monster;
            ImageUrl = imageUrl;
            Content = monster != null ? monster.ReturnMonster() : null;
        }

        public List<string> Texts { get; set; }
        public bool IsFight { get; set; }
        public Monster? Monster { get; set; }
        public string? Content { get; set; }
        public string ImageUrl { get; set; }

    }
}
