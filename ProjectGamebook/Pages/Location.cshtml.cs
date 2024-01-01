using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProjectGamebook.Models;
using ProjectGamebook.Services;

namespace ProjectGamebook.Pages
{
    public class LocationModel : PageModel
    {
        private const string KEY = "SWEETREVENGE";
        private readonly ISessionStorage<GameState> _ss;
        private readonly ILocationProvider _lp;

        public Location Location { get; set; }
        public List<Connection> Connections { get; set; }
        public GameState GS { get; set; }
        public string jsonString;

        public Weapon StartingWeapon { get; set; } = new Weapon("Fist", null, 13, 50, null, 0);
        public Shield StartingShield { get; set; } = new Shield("Your mom's fat ass", null, 25, null, 0);

        public static Dictionary<int, Monster> Monsters = new Dictionary<int, Monster>();
        public static Dictionary<int, Item> Items = new Dictionary<int, Item>();

        public LocationModel(ISessionStorage<GameState> ss, ILocationProvider lp)
        {

            _ss = ss;
            _lp = lp;

            Location = _lp.GetLocation(0);

            GS = _ss.LoadOrCreate(KEY);
            _ss.Save(KEY, GS);
        }

        public IActionResult OnPostUpdateHp(int dmg, int dlIncrease)
        {
            GS.HP -= dmg;
            GS.DL += dlIncrease;
            _ss.Save(KEY, GS);
            Console.WriteLine(GS.DL);
            int[] results = { GS.HP, GS.DL };
            return new JsonResult(results);
        }

        public IActionResult OnGet(int id)
        {

            if (GS.PreviousLocation == 666666)
            {
                Items = new Dictionary<int, Item> { { 1, new Weapon("Sword", "/imgs/bsword.png", 20, 50, "/imgs/bsword.png", 1) }, { 4, new Weapon("Pocket Bachi", "/imgs/bachi.png", 50, 50, "/imgs/bachi.png", 4) } };
                Monsters = new Dictionary<int, Monster> { { 0, new Monster("ThiccBachi", 30, 25, 20, "/imgs/bachi.jpg") }, { 3, new Monster("Bachi", 20, 25, 25, "/imgs/bachi.png") } };
                GS.HP = 100;
                GS.DL = 0;
                GS.Inventory = new Inventory();
                GS.EquippedWeapon = StartingWeapon;
                GS.EquippedShield = StartingShield;
                _ss.Save(KEY, GS);
                _lp.IsNavigationLegitimate(GS.PreviousLocation, id, GS);
            }
            if (GS.HP <= 0 || GS.DL >= 100) return RedirectToPage("GameOver");
            try
            {
                _lp.IsNavigationLegitimate(GS.PreviousLocation, id, GS);
            }
            catch (InvalidNavigationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToPage("/Error");
            }
            try
            {
                GS = _ss.LoadOrCreate(KEY);
                GS.Location = id;
                GS.PreviousLocation = id;
                _ss.Save(KEY, GS);
                Console.WriteLine(GS.PreviousLocation);
                Location = _lp.GetLocation(id);
                if (Monsters.ContainsKey(id))
                {
                    Location.Monster = Monsters[id];
                    Console.WriteLine(Location.Monster.Name + " " + Location.Monster.HP);
                    Location.Content = Monsters[id].ReturnMonster();
                }
                if (Items.ContainsKey(id))
                {
                    Location.Item = Items[id];
                    Location.Content = Items[id].ReturnItem();
                }
                for (int i = 0; i < GS.Inventory.slots.Count; i++)
                {
                    Console.WriteLine(GS.Inventory.slots[i].ImageUrl);
                }
                jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(Location.Texts);
                Connections = _lp.GetConnectionsFrom(id);
                return Page();
            }
            catch (LocationNotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToPage("/Error");
            }
        }

        public IActionResult OnPostHitMonster(int dmg, int id)
        {
            if (Monsters.ContainsKey(id))
            {
                Monsters[id].HP -= dmg;

                if (Location.Monster != null)
                {
                    Location.Monster = Monsters[id];
                    return new JsonResult(Location.Monster.HP);
                }
            }

            return BadRequest("Monster not found");
        }

        public IActionResult OnPostAddItem()
        {
            Location = _lp.GetLocation(GS.Location);

            GS.Inventory.AddItem(Location.Item);
            _ss.Save(KEY, GS);

            Items.Remove(GS.Location);
            Location.Content = null;
            return new JsonResult(Location.Item.ImageUrl);
        }

        public IActionResult OnPostUseItem(int i)
        {
            /*if (GS.Inventory.slots[i] is Weapon)
            {
                GS.EquippedWeapon = (Weapon)GS.Inventory.slots[i];
                Console.WriteLine(GS.EquippedWeapon.EquippedImageUrl);
                GS.Inventory.RemoveItem(i);
                _ss.Save(KEY, GS);

                return new JsonResult(GS.EquippedWeapon.EquippedImageUrl);
            }*/
            return new JsonResult("not successful");
        }
    }
}
