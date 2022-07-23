using MVC.Services.Interfaces;
using MVC.ViewModels.CatalogViewModels;
using MVC.ViewModels.Pagination;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class AboutController : Controller
    {
        private readonly ICatalogService _catalogService;

        public AboutController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
