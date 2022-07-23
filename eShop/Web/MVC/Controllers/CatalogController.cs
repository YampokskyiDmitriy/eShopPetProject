using MVC.Services.Interfaces;
using MVC.ViewModels.CatalogViewModels;
using MVC.ViewModels.Pagination;
using MVC.ViewModels;
using MVC.Models.Enums;

namespace MVC.Controllers;

public class CatalogController : Controller
{
    private  readonly ICatalogService _catalogService;

    public CatalogController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    public async Task<IActionResult> Index(
        CatalogTypeSorting? sortingApplied, 
        int? brandFilterApplied, 
        int? typesFilterApplied, 
        int? page, 
        int? itemsPage)
    {   
        page ??= 0;
        itemsPage ??= 9;
        
        var catalog = await _catalogService.GetCatalogItems(
            page.Value, 
            itemsPage.Value, 
            brandFilterApplied, 
            typesFilterApplied, 
            sortingApplied);
        if (catalog == null)
        {
            return View("Error");
        }
        var info = new PaginationInfo()
        {
            PageItemsRequest = itemsPage.Value,
            ActualPage = page.Value,
            ItemsPerPage = catalog.Data.Count,
            TotalItems = catalog.Count,
            TotalPages = (int)Math.Ceiling((decimal)catalog.Count / itemsPage.Value)
        };
        var vm = new IndexViewModel()
        {
            CatalogItems = catalog.Data,
            Brands = await _catalogService.GetBrands(),
            Types = await _catalogService.GetTypes(),
            Sorting = _catalogService.GetSortingTypes(),
            PaginationInfo = info
        };

        vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
        vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

        return View(vm);
    }

    public async Task<IActionResult> Single(int prodID)
    {
        var vm = new ItemInfoPageViewModel
        {
            Item = await _catalogService.GetCatalogItemById(prodID)
        };

        return View(vm);
    }
}