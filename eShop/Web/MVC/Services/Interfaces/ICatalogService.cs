using MVC.Models.Enums;
using MVC.ViewModels;

namespace MVC.Services.Interfaces;

public interface ICatalogService
{
    IEnumerable<SelectListItem> GetSortingTypes();
    Task<Catalog> GetCatalogItems(int page, int take, int? brand, int? type, CatalogTypeSorting? sorting);
    Task<CatalogItem> GetCatalogItemById(int id);
    Task<IEnumerable<SelectListItem>> GetBrands();
    Task<IEnumerable<SelectListItem>> GetTypes();
}
