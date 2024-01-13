using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectGamebook.Models;
using ProjectGamebook.Services;

namespace ProjectGamebook.Pages
{
    public class MarshmallowBossFightModel : PageModel
    {
        private string KEY;
        private readonly ISessionStorage<GameState> _ss;
        private readonly ILocationProvider _lp;
        private readonly IConfiguration _config;

        public GameState GS { get; set; }
        public string jsonString;

        public List<string> Texts { get; set; } = new List<string> { "Finally, you encountered one of the nobles.", "He’s a masochist. He likes to set himself on fire and then eating pieces of himself. And now, he wants you to stab him with that beautiful weapon of yours.", "Keep in mind that you cannot use critical attacks during this fight. The archduke would be very sad if you ended up not hitting him at all, you know?" };

        Boss Mello { get; set; } = new Boss("Has a burning passion", "Archduke Mello Jr.", 100, 20, 20, "/imgs/enemies/mallow.png");

        public MarshmallowBossFightModel(ISessionStorage<GameState> ss, ILocationProvider lp, IConfiguration config)
        {
            _config = config;
            _ss = ss;
            _lp = lp;

            KEY = _config["SessionKey"];

            GS = _ss.LoadOrCreate(KEY);
            if (GS.Boss == null)
            {
                GS.Boss = Mello;
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
                return RedirectToPage("Location", new { id = 32 });
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
