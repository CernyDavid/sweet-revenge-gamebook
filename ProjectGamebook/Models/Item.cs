using static System.Net.Mime.MediaTypeNames;

namespace ProjectGamebook.Models
{
    public class Item
    {
        public Item(string name, string imageUrl, int id)
        {
            Name = name;
            ImageUrl = imageUrl;
            Id = id;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public virtual string ReturnItem()
        {
            return ("<div id=\"item\">" +
            "<p>" + Name + "</p>" +
                "<img src=\"" + ImageUrl + "\" width=200px >" +
                "</div>");
        }
    }
}
