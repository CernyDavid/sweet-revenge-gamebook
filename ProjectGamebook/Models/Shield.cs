using System.Xml.Linq;

namespace ProjectGamebook.Models
{
    public class Shield : Item
    {
        public Shield(int id, string name, string imageUrl, int blockChance) : base(id, name, imageUrl)
        {
            BlockChance = blockChance;
        }

        public int BlockChance { get; set; }
    }
}
