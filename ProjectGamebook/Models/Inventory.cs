namespace ProjectGamebook.Models
{
    public class Inventory
    {
        public Inventory()
        {
            Ids = new List<int>();
            Items = new List<Item>();
        }

        public List<int> Ids { get; set; }
        public List<Item> Items { get; set; }

        public void AddId(int item)
        {
            if (Ids.Count < 4)
            {
                Ids.Add(item);
            }
        }

        public void RemoveId(int i)
        {
            if (i >= 0 && i < Ids.Count)
            {
                Ids.RemoveAt(i);
            }
        }

        public void AddItem(Item item)
        {
            if (Items.Count < 4)
            {
                Items.Add(item);
            }
        }

        public void RemoveItem(int i)
        {
            if (i >= 0 && i < Items.Count)
            {
                Items.RemoveAt(i);
            }
        }
    }
}
