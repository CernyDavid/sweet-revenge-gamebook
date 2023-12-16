namespace ProjectGamebook.Models
{
    public class Inventory
    {
        public List<Item> slots { get; set; } = new List<Item> { };

        public void AddItem(Item item)
        {
            if (slots.Count < 4)
            {
                slots.Add(item);
            }

        }
        public void RemoveItem(int i)
        {
            if (i >= 0 && i < slots.Count)
            {
                slots.RemoveAt(i);
            }
        }
    }
}
