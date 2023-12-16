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
        public int TextIndex { get; set; } = 0;

        public IActionResult OnPostChangeItem()
        {
            TextIndex++;
            return Partial("_ItemPartial", this);
        }

            public LocationModel(ISessionStorage<GameState> ss, ILocationProvider lp)
        {
            _ss = ss;
            _lp = lp;
        }

        public IActionResult OnGet(LocationId id)
        {
            GS = _ss.LoadOrCreate(KEY);
            if (GS.HP <= 0 || GS.DL >= 100) return RedirectToPage("GameOver");
            GS.Location = id;
            _ss.Save(KEY, GS);
            Location = _lp.GetLocation(id);
            Connections = _lp.GetConnectionsFrom(id);
            return Page();
        }
    }
}
