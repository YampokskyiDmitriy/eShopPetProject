using Microsoft.AspNetCore.Mvc;
using MVC.Models.Requests;
using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private IBasketService _basketService;
        private readonly ICatalogService _catalogService;

        public BasketController(IBasketService basketService, ICatalogService catalogService)
        {
            _basketService = basketService;
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Add(int catalogItemId, int countItems)
        {
            var catalogItem = await _catalogService.GetCatalogItemById(catalogItemId);
            await _basketService.AddBasket(catalogItem, countItems);

            return Redirect("~/");
        }

        public async Task<IActionResult> Delete(int catalogItemId)
        {
            var catalogItem = await _catalogService.GetCatalogItemById(catalogItemId);
            await _basketService.DeleteBasket(catalogItem);


            return Redirect("~/Basket/Get");
        }

        public async Task<IActionResult> Change(int catalogItemId, bool change)
        {
            var catalogItem = await _catalogService.GetCatalogItemById(catalogItemId);
            await _basketService.ChangeBasketCount(catalogItem, change);


            return Redirect("~/Basket/Get");
        }

        public async Task<IActionResult> Get()
        {
            var model = await _basketService.GetBasket();

            return View(model);
        }
    }
}
