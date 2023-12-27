using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectGamebook.Models;
using ProjectGamebook.Services;

namespace ProjectGamebook.Pages
{
    public class GameEndModel : PageModel
    {
        private const string KEY = "SWEETREVENGE";
        private readonly ISessionStorage<GameState> _ss;

        public GameState GS { get; set; }

        public GameEndModel(ISessionStorage<GameState> ss)
        {
            _ss = ss;

            GS = _ss.LoadOrCreate(KEY);
            GS.PreviousLocation = 666666;
            _ss.Save(KEY, GS);
        }

        public void OnGet()
        {
            
        }
    }
}
