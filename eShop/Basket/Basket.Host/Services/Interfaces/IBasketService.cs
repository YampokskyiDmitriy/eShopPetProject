using Basket.Host.Models;
using Basket.Host.Models.Dtos;
using BBasket.Host.Models.Response;

namespace Basket.Host.Services.Interfaces;

public interface IBasketService
{
    Task AddAsync(CatalogItemDto catalogItemDto, int countItem, string userId);
    Task DeleteAsync(CatalogItemDto catalogItemDto, string userId);
    Task ChangeCountAsync(CatalogItemDto catalogItemDto, string userId, bool change);
    Task<BasketDataDto>? GetAsync(string userId);
}