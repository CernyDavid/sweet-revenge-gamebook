using static System.Net.Mime.MediaTypeNames;

namespace ProjectGamebook.Models
{
    public class Item
    {
        public Item(string name, string imageUrl)
        {
            Name = name;
            ImageUrl = imageUrl;
        }

        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public virtual string ReturnItem()
        {
            return ("<div class=\"item\">" +
            "<p>" + Name + "</p>" +
                "</div>" +
                "<img src=\"" + ImageUrl + "\" width=200px >");
        }
    }
}
