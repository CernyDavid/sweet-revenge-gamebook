using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectGamebook.Models;
using ProjectGamebook.Services;

namespace ProjectGamebook.Pages
{
    public class Lore2Model : PageModel
    {
        private string KEY;
        private readonly ISessionStorage<GameState> _ss;
        private readonly ILocationProvider _lp;
        private readonly IConfiguration _config;

        public GameState GS { get; set; }
        public string jsonString;

        public List<string> Texts { get; set; } = new List<string> { "The moment you set foot in this room, the stench of blood and sugar is replaced by the calming smell of candles. Your heartbeat slows down and you suddenly feel safe.", "You look around you and find a book.", "Will you read it or just ignore it and continue to the next place? The decision is all yours." };


        public Lore2Model(ISessionStorage<GameState> ss, ILocationProvider lp, IConfiguration config)
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
