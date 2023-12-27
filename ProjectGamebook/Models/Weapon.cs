namespace ProjectGamebook.Models
{
    public class Weapon : Item
    {
        public Weapon(int id, string name, string imageUrl, int damage, int criticalChance) : base(id, name, imageUrl)
        {
            Damage = damage;
            CriticalChance = criticalChance;
        }

        public int Damage { get; set; }
        public int CriticalChance { get; set; }
    }
}
