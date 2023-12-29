namespace ProjectGamebook.Models
{
    public class Inventory
    {
        public Inventory()
        {
            slots = new List<Item> { };
        }

        public List<Item> slots { get; set; }

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
