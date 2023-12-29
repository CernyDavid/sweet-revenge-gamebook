using System.Xml.Linq;

namespace ProjectGamebook.Models
{
    public class Shield : Item
    {
        public Shield(string name, string imageUrl, int blockChance) : base(name, imageUrl)
        {
            BlockChance = blockChance;
        }

        public int BlockChance { get; set; }
    }
}
