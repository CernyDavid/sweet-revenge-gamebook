using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectGamebook.Models;
using ProjectGamebook.Services;
using System.IO;

namespace ProjectGamebook.Pages
{
    public class CakeBossFightModel : PageModel
    {
        private string KEY;
        private readonly ISessionStorage<GameState> _ss;
        private readonly ILocationProvider _lp;
        private readonly IConfiguration _config;

        public GameState GS { get; set; }
        public string jsonString;

        public List<string> Texts { get; set; } = new List<string> { "Finally, you encountered one of the nobles.", "This one is a gambling addict. However, he sucks at gambling. He lost his private mansion, his wife and kids and even his left nut. He is furious. He is going to kill you.", "Unless you kill him first.", "However, since he loves gambling, it’s to be expected that this fight is not about skill, but about luck. For that reason, you can only perform critical attacks." };

        Boss Jack { get; set; } = new Boss("The worst gambler in history", "Archduke Jack Pot", 80, 25, 20, "../imgs/enemies/cupcake.png");

        public CakeBossFightModel(ISessionStorage<GameState> ss, ILocationProvider lp, IConfiguration config)
        {
            _config = config;
            _ss = ss;
            _lp = lp;

            KEY = _config["SessionKey"];

            GS = _ss.LoadOrCreate(KEY);
            if (GS.Boss == null)
            {
                GS.Boss = Jack;
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
            if (GS.Boss.HP < 1)
            {
                GS.Boss = null;
                _ss.Save(KEY, GS);
                return RedirectToPage("./Location", new { id = 33 });
            }
            if (GS.Boss != Jack)
            {
                GS.Boss = Jack;
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
