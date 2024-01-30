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
        private string KEY;
        private readonly ISessionStorage<GameState> _ss;
        private readonly ILocationProvider _lp;
        private readonly IConfiguration _config;

        public Location Location { get; set; }
        public List<Connection> Connections { get; set; }
        public GameState GS { get; set; }
        public string jsonString;

        public Weapon StartingWeapon { get; set; } = new Weapon("Fist", null, 13, 30, null, 0);
        public Shield StartingShield { get; set; } = new Shield("Your mom's fat ass", null, 15, null, 0);

        public static Dictionary<int, Monster> Monsters = new Dictionary<int, Monster>();
        public static Dictionary<int, Weapon> Weapons = new Dictionary<int, Weapon>();
        public static Dictionary<int, Shield> Shields = new Dictionary<int, Shield>();
        public static Dictionary<int, SaltyConsumable> Salties = new Dictionary<int, SaltyConsumable>();
        public static Dictionary<int, SweetConsumable> Sweets = new Dictionary<int, SweetConsumable>();

        public LocationModel(ISessionStorage<GameState> ss, ILocationProvider lp, IConfiguration config)
        {
            _config = config;
            _ss = ss;
            _lp = lp;

            KEY = _config["SessionKey"];

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
                Weapons = new Dictionary<int, Weapon> { { 2, new Weapon("Baguette Sword", "../imgs/weapons/baguette.png", 20, 40, "../imgs/weapons/baguette_equipped.png", 2) }, { 5, new Weapon("Baguette Sword", "../imgs/weapons/baguette.png", 20, 40, "../imgs/weapons/baguette_equipped.png", 5) }, { 30, new Weapon("Cheesy Dagger with a cheesy name", "../imgs/weapons/cheese.png", 25, 50, "../imgs/weapons/cheese_equipped.png", 30) },
                { 32, new Weapon("Ascia Per La Pizza", "../imgs/weapons/pizza.png", 30, 60, "../imgs/weapons/pizza_equipped.png", 32) }, { 33, new Weapon("Throwing Pretzel", "../imgs/weapons/shuriken.png", 30, 60, "../imgs/weapons/shuriken_equipped.png", 33) }, { 29, new Weapon("Baguettecalibur", "../imgs/weapons/excalibur.png", 40, 70, "../imgs/weapons/excalibur_equipped.png", 29) } };
                Monsters = new Dictionary<int, Monster> { { 0, new Monster("Donut Infantryman", 20, 10, 10, "../imgs/enemies/donut.png") }, { 3, new Monster("Donut Infantryman", 20, 25, 25, "../imgs/enemies/donut.png") }, { 4, new Monster("Donut Infantryman", 20, 25, 25, "../imgs/enemies/donut.png") },
                { 9, new Monster("Candy Knight", 40, 25, 25, "../imgs/enemies/candy.png") }, { 10, new Monster("Candy Knight", 40, 25, 25, "../imgs/enemies/candy.png") }, {13, new Monster("Donut Squad", 50, 15, 15, "imgs/enemies/donut_army.png") },
                { 18, new Monster("Candy Butler", 50, 20, 25, "../imgs/enemies/candy.png") }, { 19, new Monster("Candy Butler", 50, 20, 25, "../imgs/enemies/candy.png") }, { 20, new Monster("Donut Infantryman", 40, 30, 25, "../imgs/enemies/donut.png") },
                { 23, new Monster("Lollipop Guardsman", 60, 40, 30, "../imgs/enemies/guard_pink.png") }, { 25, new Monster("Lollipop Guardsman", 60, 40, 30, "../imgs/enemies/guard_purple.png") }, { 26, new Monster("Lollipop Guardsman", 60, 40, 30, "../imgs/enemies/guard_purple.png") },
                { 28, new Monster("The Imperial Guard", 80, 35, 30, "../imgs/enemies/guard_army.png") } };
                Shields = new Dictionary<int, Shield> { {6, new Shield("Whole-grain Shield", "../imgs/shields/cookie.png", 40, "../imgs/shields/cookie_equipped.png", 6) }, { 31, new Shield("Holey Shield", "../imgs/shields/cheese.png", 50, "../imgs/shields/cheese_equipped.png", 31) }, { 22, new Shield("Mobile Cracker Barrier", "../imgs/shields/cracker.png", 60, "../imgs/shields/cracker_equipped.png", 22) } };
                Salties = new Dictionary<int, SaltyConsumable> { {8, new SaltyConsumable(10, "Slice of half-baked toast", "../imgs/consumables/toast.png", 8) }, { 11, new SaltyConsumable(10, "Slice of half-baked toast", "../imgs/consumables/toast.png", 11) },
                {14, new SaltyConsumable(10, "Leftover Chips", "../imgs/consumables/chips.png", 14) }, {16, new SaltyConsumable(25, "Pizza Mozarella", "../imgs/consumables/pizza.png", 16) }, {21, new SaltyConsumable(25, "Vegan Burger with Meat", "../imgs/consumables/burger.png", 21) } };
                Sweets = new Dictionary<int, SweetConsumable> { {12, new SweetConsumable(15, "Saliva Collector", "../imgs/consumables/lolli.png", 12) }, {17, new SweetConsumable(20, "Suspicious Pack of Sweets", "../imgs/consumables/pack.png", 17) },
                {24, new SweetConsumable(25, "Mr. Brown's Remains", "../imgs/consumables/chocolate.png", 24) }, {27, new SweetConsumable(25, "Mr. Brown's Remains", "../imgs/consumables/chocolate.png", 27) } };
                GS.HP = 100;
                GS.DL = 0;
                GS.Boss = null;
                GS.Inventory = new Inventory();
                GS.EquippedWeapon = StartingWeapon;
                GS.EquippedShield = StartingShield;
                _ss.Save(KEY, GS);
            }
            else
            {
                if (GS.HP <= 0 || GS.DL >= 100) return RedirectToPage("./GameOver");
                try
                {
                    _lp.IsNavigationLegitimate(GS.PreviousLocation, id, GS);
                }
                catch (InvalidNavigationException ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return RedirectToPage("./Error");
                }
            }
            if (GS.HP <= 0 || GS.DL >= 100) return RedirectToPage("./GameOver");
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
                if (Weapons.ContainsKey(id))
                {
                    Location.Item = Weapons[id];
                    Location.Content = Weapons[id].ReturnItem();
                }
                if (Shields.ContainsKey(id))
                {
                    Location.Item = Shields[id];
                    Location.Content = Shields[id].ReturnItem();
                }
                if (Sweets.ContainsKey(id))
                {
                    Location.Item = Sweets[id];
                    Location.Content = Sweets[id].ReturnItem();
                }
                if (Salties.ContainsKey(id))
                {
                    Location.Item = Salties[id];
                    Location.Content = Salties[id].ReturnItem();
                }
                for (int i = 0; i < GS.Inventory.Ids.Count; i++)
                {
                    Console.WriteLine(GS.Inventory.Ids[i] + "id");
                    Console.WriteLine(GS.Inventory.Items[i] + "item");
                }
                jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(Location.Texts);
                Connections = _lp.GetConnectionsFrom(id);
                return Page();
            }
            catch (LocationNotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToPage("./Error");
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

            return new JsonResult(Location.Item.ImageUrl);
        }

        public IActionResult OnPostUseItem(int i)
        {
            if (Weapons.ContainsKey(GS.Inventory.Ids[i]))
            {
                GS.EquippedWeapon = Weapons[GS.Inventory.Ids[i]];
                GS.Inventory.RemoveItem(i);
                _ss.Save(KEY, GS);
                string[] results = { "weapon", GS.EquippedWeapon.ImageUrl, GS.EquippedWeapon.Damage.ToString(), GS.EquippedWeapon.CriticalChance.ToString(), GS.EquippedWeapon.EquippedImageUrl }; 

                return new JsonResult(results);
            }
            if (Shields.ContainsKey(GS.Inventory.Ids[i]))
            {
                GS.EquippedShield = Shields[GS.Inventory.Ids[i]];
                GS.Inventory.RemoveItem(i);
                _ss.Save(KEY, GS);
                string[] results = { "shield", GS.EquippedShield.ImageUrl, GS.EquippedShield.BlockChance.ToString(), GS.EquippedShield.EquippedImageUrl };

                return new JsonResult(results);
            }
            if (Sweets.ContainsKey(GS.Inventory.Ids[i]))
            {
                GS.HP += Sweets[GS.Inventory.Ids[i]].HPIncrease;
                GS.DL += Sweets[GS.Inventory.Ids[i]].DLIncrease;
                if (GS.HP > 100)
                {
                    GS.HP = 100;
                }
                GS.Inventory.RemoveItem(i);
                _ss.Save(KEY, GS);
                string[] results = { "sweet", GS.HP.ToString(), GS.DL.ToString() };

                return new JsonResult(results);
            }
            if (Salties.ContainsKey(GS.Inventory.Ids[i]))
            {
                GS.DL -= Salties[GS.Inventory.Ids[i]].DLDecrease;
                if (GS.DL < 0)
                {
                    GS.DL = 0;
                }
                GS.Inventory.RemoveItem(i);
                _ss.Save(KEY, GS);
                string[] results = { "salty", GS.DL.ToString() };

                return new JsonResult(results);
            }
            return new JsonResult("not successful");
        }

        public IActionResult OnPostDropItem(int i)
        {
            GS.Inventory.RemoveItem(i);
            _ss.Save(KEY, GS);
            return new JsonResult("Successfully removed");
        }
    }
}
