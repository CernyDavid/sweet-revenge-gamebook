namespace ProjectGamebook.Models
{
    public class GameState
    {
        public int Location { get; set; }
        public int PreviousLocation { get; set; } = 666666;
        public int HP { get; set; } = 100;
        public int DL { get; set; }
        public Weapon EquippedWeapon { get; set; }
        public Shield EquippedShield { get; set; }
        public Inventory Inventory { get; set; } = new Inventory();
    }
}
