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
            return (
                "<div class=\"monster\">" +
                "<div class=\"monster__tag\">" +
                "<p>" + Name + "</p>" +
                "<p>" + Description + "</p>" +
                "<p id=\"monsterHP\">" + "HP: " + HP + "</p>" +
                "</div>" +
                "<img class=\"monster__img\" src=\"" + ImageUrl + "\">" +
                "</div>");
        }
    }
}
