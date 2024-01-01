using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

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

        public override string ReturnItem()
        {
            return ("<div id=\"item\">" +
            "<p>" + Name + "</p>" +
                "<p>" + "Block Chance: " + BlockChance + "</p>" +
                "<img src=\"" + ImageUrl + "\" width=200px >" +
                "</div>");
        }
    }
}
