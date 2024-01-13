using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectGamebook.Models;
using ProjectGamebook.Services;

namespace ProjectGamebook.Pages
{
    public class Lore1Model : PageModel
    {
        private string KEY;
        private readonly ISessionStorage<GameState> _ss;
        private readonly ILocationProvider _lp;
        private readonly IConfiguration _config;

        public GameState GS { get; set; }
        public string jsonString;

        public List<string> Texts { get; set; } = new List<string> { "You discovered a mysterious place. It’s dark and quiet. But for some reason, this place feels safe.", "You found a book just lying on a table. It doesn’t look very interesting, but you can sense the immense knowledge it contains.", "Will you read it or not?" };


        public Lore1Model(ISessionStorage<GameState> ss, ILocationProvider lp, IConfiguration config)
        {
            _config = config;
            _ss = ss;
            _lp = lp;

            KEY = _config["SessionKey"];

            GS = _ss.LoadOrCreate(KEY);
            _ss.Save(KEY, GS);
        }

        public IActionResult OnGet()
        {
            if (GS.HP <= 0 || GS.DL >= 100) return RedirectToPage("GameOver");

            jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(Texts);
            return Page();

        }

    }
}
