using MVC.Models.Requests;
using MVC.ViewModels;

namespace MVC.Services.Interfaces
{
    public interface IAdminCatalogService
    {
        Task<CatalogItem?> AddItem(AddItemRequest request);
        Task<CatalogType?> AddType(AddRequest request);
        Task<CatalogBrand?> AddBrand(AddRequest request);
        Task<CatalogItem> GetItem(int id);
        Task<IEnumerable<CatalogItem>> GetItems();
        Task<CatalogType> GetType(int id);
        Task<IEnumerable<CatalogType>> GetTypes();
        Task<IEnumerable<SelectListItem>> GetTypeSelectList();
        Task<CatalogBrand> GetBrand(int id);
        Task<IEnumerable<CatalogBrand>> GetBrands();
        Task<IEnumerable<SelectListItem>> GetBrandSelectList();
        Task<CatalogItem?> RemoveItem(RemoveRequest request);
        Task<CatalogType?> RemoveType(RemoveRequest request);
        Task<CatalogBrand?> RemoveBrand(RemoveRequest request);
        Task<CatalogItem?> UpdateItem(UpdateItemRequest request);
        Task<CatalogType?> UpdateType(UpdateRequest request);
        Task<CatalogBrand?> UpdateBrand(UpdateRequest request);
    }
}