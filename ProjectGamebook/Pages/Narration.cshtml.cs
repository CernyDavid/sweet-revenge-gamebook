using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectGamebook.Models;
using ProjectGamebook.Services;

namespace ProjectGamebook.Pages
{
    public class NarrationModel : PageModel
    {
        private string KEY;
        private readonly ISessionStorage<GameState> _ss;
        private readonly IConfiguration _config;

        public GameState GS { get; set; }
        public string jsonString;

        public List<string> Texts { get; set; }


        public NarrationModel(ISessionStorage<GameState> ss, IConfiguration config)
        {
            _config = config;
            _ss = ss;

            KEY = _config["SessionKey"];

            GS = _ss.LoadOrCreate(KEY);
            _ss.Save(KEY, GS);
        }
    }
}
