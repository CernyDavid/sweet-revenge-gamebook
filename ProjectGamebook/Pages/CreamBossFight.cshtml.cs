using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectGamebook.Models;
using ProjectGamebook.Services;
using System.Security.Cryptography;

namespace ProjectGamebook.Pages
{
    public class CreamBossFightModel : PageModel
    {
        private string KEY;
        private readonly ISessionStorage<GameState> _ss;
        private readonly ILocationProvider _lp;
        private readonly IConfiguration _config;

        public GameState GS { get; set; }
        public string jsonString;

        public List<string> Texts { get; set; } = new List<string> { "Looks like you reached the boss of this floor.", "We all know that children love ice cream, but did you know that ice cream also loves children? Yes, this guy right here is a dirty old pedo.", "So do something heroic for once and send him to nothingness.", "By the way, you can’t use your shield during this fight. You see, he’s not a huge fan of using protection." };

        Boss Cream { get; set; } = new Boss("Created from the frozen semen of the Sweet Emperor", "Iced Cream", 30, 50, 50, "/imgs/enemies/icecream.png");

        public CreamBossFightModel(ISessionStorage<GameState> ss, ILocationProvider lp, IConfiguration config)
        {
            _config = config;
            _ss = ss;
            _lp = lp;

            KEY = _config["SessionKey"];

            GS = _ss.LoadOrCreate(KEY);
            if (GS.Boss == null)
            {
                GS.Boss = Cream;
            }
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

        public IActionResult OnGet()
        {
            if (GS.HP <= 0 || GS.DL >= 100) return RedirectToPage("GameOver");
            if (GS.Boss.HP < 0)
            {
                GS.Boss = null;
                _ss.Save(KEY, GS);
                return RedirectToPage("Location", new { id = 31 });
            }

            jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(Texts);
            return Page();

        }

        public IActionResult OnPostHitMonster(int dmg)
        {
            Console.WriteLine(GS.Boss.HP + "before");
            GS.Boss.HP = GS.Boss.HP - dmg;
            _ss.Save(KEY, GS);
            Console.WriteLine(GS.Boss.HP + "after");
            return new JsonResult(GS.Boss.HP);

        }

    }
}
