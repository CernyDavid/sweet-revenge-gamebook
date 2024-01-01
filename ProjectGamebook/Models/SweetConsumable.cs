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
    }
}
