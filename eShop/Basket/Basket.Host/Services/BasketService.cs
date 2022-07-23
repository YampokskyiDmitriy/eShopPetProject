using Basket.Host.Models;
using Basket.Host.Models.Dtos;
using Basket.Host.Services.Interfaces;
using BBasket.Host.Models.Response;

namespace Basket.Host.Services;

public class BasketService : IBasketService
{
    private readonly ICacheService _cacheService;
    private readonly ILogger<BasketService> _logger;
    private readonly IJsonSerializer _jsonSerializer;
    public BasketService(ICacheService cacheService, ILogger<BasketService> logger, IJsonSerializer jsonSerializer)
    {
        _cacheService = cacheService;
        _logger = logger;
        _jsonSerializer = jsonSerializer;
    }

    public async Task AddAsync(CatalogItemDto catalogItemDto, int countItem, string userId)
    {
        var originalData = await _cacheService.GetAsync<BasketDataDto>(userId);

        if (originalData is null)
        {
            originalData = new BasketDataDto
            {
                Data = new Dictionary<String, int>()
            };
        }
        var serializedItem = _jsonSerializer.Serialize(catalogItemDto);
        if (originalData.Data.ContainsKey(serializedItem))
        {
            originalData.Data[serializedItem] = originalData.Data[serializedItem] + countItem;
        }
        else
        {
            originalData.Data.Add(serializedItem, countItem);
        }

        await _cacheService.AddOrUpdateAsync(userId, originalData);
    }

    public async Task ChangeCountAsync(CatalogItemDto catalogItemDto, string userId, bool change)
    {
        var originalData = await _cacheService.GetAsync<BasketDataDto>(userId);

        var serializedItem = _jsonSerializer.Serialize(catalogItemDto);

        if(change)
        {
            originalData.Data[serializedItem] = originalData.Data[serializedItem] + 1;
        }
        else
        {
            originalData.Data[serializedItem] = originalData.Data[serializedItem] - 1;
        }

        await _cacheService.AddOrUpdateAsync(userId, originalData);
    }

    public async Task DeleteAsync(CatalogItemDto catalogItemDto, string userId)
    {
        var originalData = await _cacheService.GetAsync<BasketDataDto>(userId);

        var serializedItem = _jsonSerializer.Serialize(catalogItemDto);

        originalData.Data.Remove(serializedItem);

        await _cacheService.AddOrUpdateAsync(userId, originalData);
    }

    public async Task<BasketDataDto>? GetAsync(string userId)
    {
        return await _cacheService.GetAsync<BasketDataDto>(userId);
    }
}