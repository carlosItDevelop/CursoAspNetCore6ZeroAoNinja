using Microsoft.AspNetCore.Mvc;

namespace Cooperchip.ITDeveloper.Mvc.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult HowItWorks() => View();
        public IActionResult LayoutOptions() => View();
        public IActionResult SavingDb() => View();
        public IActionResult SkinOptions() => View();
        public IActionResult ThemeModes() => View();
    }
}
