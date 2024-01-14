using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectGamebook.Models;
using ProjectGamebook.Services;
using System.IO;

namespace ProjectGamebook.Pages
{
    public class FinalBossFightModel : PageModel
    {
        private string KEY;
        private readonly ISessionStorage<GameState> _ss;
        private readonly ILocationProvider _lp;
        private readonly IConfiguration _config;

        public GameState GS { get; set; }
        public string jsonString;

        public List<string> Texts { get; set; } = new List<string> { "That’s him. Doesn’t he look kind of funny?" };

        Boss Emperor { get; set; } = new Boss("The Sweet Emperor", "Yummy, tummy, funny, lucky Gummy Bear", 100, 40, 20, "/imgs/enemies/bear.png");

        public FinalBossFightModel(ISessionStorage<GameState> ss, ILocationProvider lp, IConfiguration config)
        {
            _config = config;
            _ss = ss;
            _lp = lp;

            KEY = _config["SessionKey"];

            GS = _ss.LoadOrCreate(KEY);
            if (GS.Boss == null)
            {
                GS.Boss = Emperor;
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
            if (GS.Boss.HP < 1)
            {
                GS.Boss = null;
                _ss.Save(KEY, GS);
                return RedirectToPage("GameEnd");
            }
            if (GS.Boss != Emperor)
            {
                GS.Boss = Emperor;
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