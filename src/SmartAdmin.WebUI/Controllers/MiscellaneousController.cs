using Microsoft.AspNetCore.Mvc;

namespace Cooperchip.ITDeveloper.Mvc.Controllers
{
    public class MiscellaneousController : Controller
    {
        public IActionResult Fullcalendar() => View();
        public IActionResult Lightgallery() => View();
        public IActionResult Treeview() => View();
    }
}
