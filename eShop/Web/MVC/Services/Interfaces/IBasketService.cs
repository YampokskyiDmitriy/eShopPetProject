using MVC.ViewModels;

namespace MVC.Services.Interfaces
{
    public interface IBasketService
    {
        Task AddBasket(CatalogItem catalogItem, int countItems);
        Task DeleteBasket(CatalogItem catalogItem);
        Task ChangeBasketCount(CatalogItem catalogItem, bool change);
        Task<BasketData>? GetBasket();
    }
}