using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public Weapon Weapon { get; set; } = new Weapon(0, "Weapon,", null, 13, 50);

        public Dictionary<int, Monster> Monsters = new Dictionary<int, Monster> { { 0, new Monster("ThiccBachi", 30, 25, 0, "/imgs/bachi.jpg") }, { 3, new Monster("Bachi", 50, 25, 0, "/imgs/bachi.png") } };

        public LocationModel(ISessionStorage<GameState> ss, ILocationProvider lp)
        {
            _ss = ss;
            _lp = lp;

            Location = _lp.GetLocation(0);

            GS = _ss.LoadOrCreate(KEY);
            _ss.Save(KEY, GS);
        }

        public IActionResult OnPostHitMonster(int dmg, int id)
        {

            if (Location.Monster != null)
            {
                Console.WriteLine(Location.Monster.HP + "before");
                Location.Monster.HP -= dmg;
                Console.WriteLine("dmg" + dmg);
                Console.WriteLine(Location.Monster.HP + "after");
                return new JsonResult(Location.Monster.HP);
            }

            return BadRequest("Monster not found");

        }

        public IActionResult OnPostUpdateHp(int dmg)
        {
            GS.HP -= dmg;
            _ss.Save(KEY, GS);

            return new JsonResult(GS.HP);
        }

        public IActionResult OnGet(int id)
        {
            if (GS.PreviousLocation == 666666)
            {
                GS.HP = 100;
                _ss.Save(KEY, GS);
                _lp.IsNavigationLegitimate(GS.PreviousLocation, id, GS);
            }
            if (GS.HP <= 0 || GS.DL >= 100) return RedirectToPage("GameOver");
            else if (GS.PreviousLocation != GS.Location)
            {
                try
                {
                    _lp.IsNavigationLegitimate(GS.PreviousLocation, id, GS);
                }
                catch (InvalidNavigationException ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return RedirectToPage("/Error");
                }
            }
            try
            {
                GS = _ss.LoadOrCreate(KEY);
                GS.Location = id;
                GS.PreviousLocation = id;
                GS.EquippedWeapon = Weapon;
                _ss.Save(KEY, GS);
                Console.WriteLine(GS.PreviousLocation);
                Location = _lp.GetLocation(id);
                if (Monsters.ContainsKey(id))
                {
                    Location.Monster = Monsters[id];
                    Console.WriteLine(Location.Monster.Name + " " + Location.Monster.HP);
                    Location.Content = Monsters[id].ReturnMonster();
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
    }
}
