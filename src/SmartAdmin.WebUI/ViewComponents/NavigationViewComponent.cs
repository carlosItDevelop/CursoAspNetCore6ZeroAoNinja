using Cooperchip.ITDeveloper.Mvc.Models;
using Microsoft.AspNetCore.Mvc;


namespace Cooperchip.ITDeveloper.Mvc.ViewComponents
{
    public class NavigationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var items = NavigationModel.Full;

            return View(items);
        }
    }
}
