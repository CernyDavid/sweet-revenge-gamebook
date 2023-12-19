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
        public string jsonString;

        public LocationModel(ISessionStorage<GameState> ss, ILocationProvider lp)
        {
            _ss = ss;
            _lp = lp;

            GS = _ss.LoadOrCreate(KEY);
            _ss.Save(KEY, GS);
        }

        public IActionResult OnPostUpdateHp()
        {
            GS.HP -= 50;
            _ss.Save(KEY, GS);

            return new JsonResult(GS.HP);
        }

        public IActionResult OnGet(int id)
        {
            if (GS.PreviousLocation == 666666)
            {
                GS.HP = 100;
                _ss.Save(KEY, GS);
                _lp.IsNavigationLegitimate(GS.PreviousLocation, id, GS);
            }
            else
            {
                if (GS.HP <= 0 || GS.DL >= 100) return RedirectToPage("GameOver");
                try
                {
                    _lp.IsNavigationLegitimate(GS.PreviousLocation, id, GS);
                }
                catch (InvalidNavigationException ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return RedirectToPage("/Error");
                }
            }
            try
            {
                GS = _ss.LoadOrCreate(KEY);
                GS.Location = id;
                GS.PreviousLocation = id;
                _ss.Save(KEY, GS);
                Location = _lp.GetLocation(id);
                jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(Location.Texts);
                Connections = _lp.GetConnectionsFrom(id);
                return Page();
            }
            catch (LocationNotFoundException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToPage("/Error");
            }
        }
    }
}
