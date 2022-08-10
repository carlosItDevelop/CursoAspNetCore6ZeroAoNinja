using Microsoft.AspNetCore.Mvc;

namespace Cooperchip.ITDeveloper.Mvc.Controllers
{
    public class NotificationsController : Controller
    {
        public IActionResult Sweetalert2() => View();
        public IActionResult Toastr() => View();
    }
}
