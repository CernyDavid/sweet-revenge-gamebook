using System.Xml.Linq;

namespace ProjectGamebook.Models
{
    public class Shield : Item
    {
        public Shield(string name, string imageUrl, int blockChance, string equippedImageUrl, int id) : base(name, imageUrl, id)
        {
            BlockChance = blockChance;
            EquippedImageUrl = equippedImageUrl;
        }

        public int BlockChance { get; set; }
        public string EquippedImageUrl { get; set; }
    }
}
