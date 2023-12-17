namespace ProjectGamebook.Models
{
    public class GameState
    {
        public int Location { get; set; }
        public int HP { get; set; } = 100;
        public int DL { get; set; }
        public Weapon EquippedWeapon { get; set; }
        public Shield EquippedShield { get; set; }
        public Inventory inventory { get; set; } = new Inventory();
    }
}
