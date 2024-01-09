using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectGamebook.Models;
using ProjectGamebook.Services;

namespace ProjectGamebook.Pages
{
    public class GameEndModel : PageModel
    {
        private string KEY;
        private readonly ISessionStorage<GameState> _ss;
        private readonly IConfiguration _config;

        public GameState GS { get; set; }

        public GameEndModel(ISessionStorage<GameState> ss, IConfiguration config)
        {
            _config = config;
            _ss = ss;

            KEY = _config["SessionKey"];

            GS = _ss.LoadOrCreate(KEY);
            GS.PreviousLocation = 666666;
            _ss.Save(KEY, GS);
        }

        public void OnGet()
        {
            
        }
    }
}
