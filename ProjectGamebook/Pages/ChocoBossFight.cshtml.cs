using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectGamebook.Models;
using ProjectGamebook.Services;

namespace ProjectGamebook.Pages
{
    public class ChocoBossFightModel : PageModel
    {
        private const string KEY = "SWEETREVENGE";
        private readonly ISessionStorage<GameState> _ss;
        private readonly ILocationProvider _lp;

        public GameState GS { get; set; }
        public string jsonString;

        public List<string> Texts { get; set; } = new List<string> { "Did someone say boss? Well, sorry, I jinxed you. The real boss is this guy right here.", "Since he was just a little kid, he’s been bullied by the white chocolate folks. Immense rage lies deep within him and now he wants to take it all out on you.", "Well, it’s nothing but a minor inconvenience.", "Kill him." };

        Boss Chocolate { get; set; } = new Boss("The #1 racist jokes victim", "Mr. Brown", 50, 30, 40, "/imgs/enemies/choco.png");

        public ChocoBossFightModel(ISessionStorage<GameState> ss, ILocationProvider lp)
        {

            _ss = ss;
            _lp = lp;

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
            if (GS.HP <= 0 || GS.DL >= 100) return RedirectToPage("GameOver");
            if (GS.Boss.HP < 0)
            {
                GS.Boss = null;
                _ss.Save(KEY, GS);
                return RedirectToPage("Location", new { id = 30 });
            }

            jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(Texts);
            return Page();

        }

        public IActionResult OnPostHitMonster(int dmg)
        {
            GS.Boss.HP -= dmg;
            _ss.Save(KEY, GS);

            return new JsonResult(GS.Boss.HP);

        }

    }
}
