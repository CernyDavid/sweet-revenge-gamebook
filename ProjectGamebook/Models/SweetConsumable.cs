namespace ProjectGamebook.Models
{
    public class SweetConsumable : Item
    {
        public SweetConsumable(int sweetness, string name, string imageUrl, int id) : base(name, imageUrl, id)
        {
            Sweetness = sweetness;
            HPIncrease = sweetness * 2;
            DLIncrease = sweetness;
        }

        public int Sweetness { get; set; }
        public int HPIncrease { get; set; }
        public int DLIncrease { get; set; }

        public override string ReturnItem()
        {
            return ("<div class=\"item\" id=\"item\">" +
                "<div class=\"item__tag\">" +
                "<p>" + Name + "</p>" +
                "<p>" + "Sweetness: " + Sweetness + "</p>" +
                "</div>" +
                "<img class=\"item__img\" src=\"" + ImageUrl + "\">" +
                "</div>");
        }
    }
}
