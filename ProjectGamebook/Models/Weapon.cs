namespace ProjectGamebook.Models
{
    public class Weapon : Item
    {
        public Weapon( string name, string imageUrl, int damage, int criticalChance, string equippedImageUrl, int id) : base( name, imageUrl, id)
        {
            Damage = damage;
            CriticalChance = criticalChance;
            EquippedImageUrl = equippedImageUrl;
        }

        public int Damage { get; set; }
        public int CriticalChance { get; set; }
        public string EquippedImageUrl { get; set; }

        public override string ReturnItem()
        {
            return ("<div class=\"item\" id=\"item\">" +
                "<div class=\"item__tag\">" +
                "<p>" + Name + "</p>" +
                "<p>" + "Damage: " + Damage + "</p>" +
                "<p>" + "Critical Chance: " + CriticalChance + "</p>" +
                "</div>" +
                "<img class=\"item__img\" src=\"" + ImageUrl + "\">" +
                "</div>");
        }
    }
}
