namespace ProjectGamebook.Models
{
    public class Monster
    {
        public Monster( string? name, int hP, int damage, int dlIncrease, string imageUrl)
        {
            Name = name;
            HP = hP;
            Damage = damage;
            DLIncrease = dlIncrease;
            ImageUrl = imageUrl;
        }

        public string? Name { get; set; }
        public int HP { get; set; }
        public int Damage { get; set; }
        public int DLIncrease { get; set; }

        public string ImageUrl { get; set; }

        public virtual string ReturnMonster()
        {
            return ("<div>" +
                "<p>" + Name + "</p>" +
                "<p id=\"monsterHP\">" + HP + "</p>" +
                "</div>" +
                "<img src=\"" + ImageUrl + "\" width=200px >");
        }
        public bool IsAlive()
        {
            return HP <= 0;
        }
    }
}
