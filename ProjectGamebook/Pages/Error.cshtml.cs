using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace ProjectGamebook.Pages
{
    public class ErrorModel : PageModel
    {
        public string ErrorMessage { get; set; } = "Unknown error";

        public void OnGet()
        {
            ErrorMessage = (TempData["ErrorMessage"]?.ToString() == null) ? "Unknown error" : TempData["ErrorMessage"]?.ToString();
        }
    }
}