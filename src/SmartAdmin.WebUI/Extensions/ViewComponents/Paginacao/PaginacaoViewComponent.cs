

using Cooperchip.ITDeveloper.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Cooperchip.ITDeveloper.Mvc.Extensions.ViewComponents.Paginacao
{
    public class PaginacaoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IPaged modeloPaginado)
        {
            return View(modeloPaginado);
        }
    }
}
