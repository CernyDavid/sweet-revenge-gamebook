namespace ProjectGamebook.Models
{
    public class SaltyConsumable : Item
    {
        public SaltyConsumable(int saltiness, string name, string imageUrl, int id) : base(name, imageUrl, id)
        {
            Saltiness = saltiness;
            DLDecrease = saltiness * 2;
        }

        public int Saltiness { get; set; }
        public int DLDecrease { get; set; }

        public override string ReturnItem()
        {
            return ("<div id=\"item\">" +
            "<p>" + Name + "</p>" +
                "<p>" + "Saltiness: " + Saltiness + "</p>" +
                "<img src=\"" + ImageUrl + "\" width=200px >" +
                "</div>");
        }
    }
}
