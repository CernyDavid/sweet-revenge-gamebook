using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectGamebook.Models;
using ProjectGamebook.Services;

namespace ProjectGamebook.Pages
{
    public class LoreModel : PageModel
    {
        private string KEY;
        private readonly ISessionStorage<GameState> _ss;
        private readonly IConfiguration _config;

        public GameState GS { get; set; }
        public string jsonString;

        public List<string> Texts { get; set; }


        public LoreModel(ISessionStorage<GameState> ss, IConfiguration config)
        {
            _config = config;
            _ss = ss;

            KEY = _config["SessionKey"];

            GS = _ss.LoadOrCreate(KEY);
            _ss.Save(KEY, GS);
        }

        public IActionResult OnGet()
        {
            if (GS.HP <= 0 || GS.DL >= 100) return RedirectToPage("./GameOver");

            if (GS.PreviousLocation == 12)
            {
                Texts = new List<string> { "You discovered a mysterious place. It’s dark and quiet. But for some reason, this place feels safe.", "You found a book just lying on a table. It doesn’t look very interesting, but you can sense the immense knowledge it contains.", "Will you read it or not?" };
            }
            else if (GS.PreviousLocation == 14)
            {
                Texts = new List<string> { "The moment you set foot in this room, the stench of blood and sugar is replaced by the calming smell of candles. Your heartbeat slows down and you suddenly feel safe.", "You look around you and find a book.", "Will you read it or just ignore it and continue to the next place? The decision is all yours." };
            }
            else if (GS.PreviousLocation == 20)
            {
                Texts = new List<string> { "Yes, it is indeed a very special place. There are no monsters, no weapons, no traps. It’s peaceful and quiet. It looks like a library.", "There is a lot of books, but only one seems like it contains something useful.", "Will you read it?" };
            }
            else if (GS.PreviousLocation == 23)
            {
                Texts = new List<string> { "You entered a mostly empty room. You don’t sense anyone nearby, so you decide to take a little break. You sit on the floor and relax for a while.", "But since you are already here, you might as well take a look at the contents of that mysterious book over there, don’t you think?" };
            }
            else
            {
                return RedirectToPage("./Error");
            }

            jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(Texts);
            return Page();

        }

    }
}
