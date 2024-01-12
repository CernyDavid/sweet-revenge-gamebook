namespace ProjectGamebook.Models
{
    public class Inventory
    {
        public Inventory()
        {
            Ids = new List<int> { 333, 333, 333, 333 };
            Items = new List<Item> { new Item("nothing", null, 333), new Item("nothing", null, 333), new Item("nothing", null, 333), new Item("nothing", null, 333) };
        }

        public List<int> Ids { get; set; }
        public List<Item> Items { get; set; }

        public void AddItem(Item item)
        {
            if (Items[0].Id == 333)
            {
                Items[0] = item;
                Ids[0] = item.Id;
            }
			else if (Items[1].Id == 333)
			{
				Items[1] = item;
				Ids[1] = item.Id;
			}
			else if (Items[2].Id == 333)
			{
				Items[2] = item;
				Ids[2] = item.Id;
			}
			else if (Items[3].Id == 333)
			{
				Items[3] = item;
				Ids[3] = item.Id;
			}
		}

        public void RemoveItem(int i)
        {
            Items[i] = new Item("nothing", null, 333);
            Ids[i] = 333;
        }
    }
}
