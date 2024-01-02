using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectGamebook.Models;
using ProjectGamebook.Services;
using System.Diagnostics;

namespace ProjectGamebook.Pages
{
    public class ErrorModel : PageModel
    {
        public string ErrorMessage { get; set; } = "Unknown error";
        private const string KEY = "SWEETREVENGE";
        private readonly ISessionStorage<GameState> _ss;

        public GameState GS { get; set; }

        public ErrorModel(ISessionStorage<GameState> ss)
        {
            _ss = ss;

            GS = _ss.LoadOrCreate(KEY);
            GS.PreviousLocation = 666666;
            _ss.Save(KEY, GS);
        }

        public void OnGet()
        {
            ErrorMessage = (TempData["ErrorMessage"]?.ToString() == null) ? "Unknown error" : TempData["ErrorMessage"]?.ToString();
        }
    }
}