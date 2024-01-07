namespace ProjectGamebook.Models
{
    public class Boss : Monster
    {
        public string Description { get; set; }
        public Boss(string desc, string? name, int hP, int damage, int dlIncrease, string imageUrl) : base(name, hP, damage, dlIncrease, imageUrl)
        {
            Description = desc;
        }

        public override string ReturnMonster()
        {
            return ("<div>" +
                "<p>" + Name + "</p>" +
                "<p>" + Description + "</p>" +
                "<p id=\"monsterHP\">" + HP + "</p>" +
                "</div>" +
                "<img src=\"" + ImageUrl + "\" width=200px >");
        }
    }
}
