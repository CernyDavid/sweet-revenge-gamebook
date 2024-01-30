using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectGamebook.Models;
using ProjectGamebook.Services;

namespace ProjectGamebook.Pages
{
    public class ChocoBossFightModel : PageModel
    {
        private string KEY;
        private readonly ISessionStorage<GameState> _ss;
        private readonly ILocationProvider _lp;
        private readonly IConfiguration _config;

        public GameState GS { get; set; }
        public string jsonString;

        public List<string> Texts { get; set; } = new List<string> { "Did someone say boss? Well, sorry, I jinxed you. The real boss is this guy right here.", "Since he was just a little kid, he’s been bullied by the white chocolate folks. Immense rage lies deep within him and now he wants to take it all out on you.", "Well, it’s nothing but a minor inconvenience.", "Kill him." };

        Boss Chocolate { get; set; } = new Boss("The #1 racist jokes victim", "Mr. Brown", 50, 30, 30, "../imgs/enemies/choco.png");

        public ChocoBossFightModel(ISessionStorage<GameState> ss, ILocationProvider lp, IConfiguration config)
        {
            _config = config;
            _ss = ss;
            _lp = lp;

            KEY = _config["SessionKey"];

            GS = _ss.LoadOrCreate(KEY);
            if (GS.Boss == null)
            {
                GS.Boss = Chocolate;
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
            if (GS.HP <= 0 || GS.DL >= 100) return RedirectToPage("./GameOver");
            if (GS.Boss.HP < 0)
            {
                GS.Boss = null;
                _ss.Save(KEY, GS);
                return RedirectToPage("./Location", new { id = 30 });
            }
            if (GS.Boss != Chocolate)
            {
                GS.Boss = Chocolate;
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
