using Infrastructure.Services.Interfaces;
using MVC.Dtos;
using MVC.Models.Enums;
using MVC.Models.Requests;
using MVC.Models.Response;
using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Services;

public class CatalogService : ICatalogService
{
    private readonly IOptions<AppSettings> _settings;
    private readonly IHttpClientService _httpClient;
    private readonly ILogger<CatalogService> _logger;

    public CatalogService(IHttpClientService httpClient, ILogger<CatalogService> logger, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings;
        _logger = logger;
    }

    public async Task<Catalog> GetCatalogItems(int page, int take, int? brand, int? type, CatalogTypeSorting? sorting)
    {
        var filters = new Dictionary<CatalogTypeFilter, int>();

        if (brand.HasValue)
        {
            filters.Add(CatalogTypeFilter.Brand, brand.Value);
        }
        
        if (type.HasValue)
        {
            filters.Add(CatalogTypeFilter.Type, type.Value);
        }
        
        var result = await _httpClient.SendAsync<Catalog, PaginatedItemsRequest<CatalogTypeFilter, CatalogTypeSorting>>($"{_settings.Value.CatalogUrl}/items",
           HttpMethod.Post, 
           new PaginatedItemsRequest<CatalogTypeFilter, CatalogTypeSorting>()
            {
                PageIndex = page,
                PageSize = take,
                Filters = filters,
                Sorting = sorting.GetValueOrDefault()
           });

        return result;
    }

    public async Task<IEnumerable<SelectListItem>> GetBrands()
    {
        var result = await _httpClient.SendAsync<ItemsListResponse<CatalogBrand>, object?>(
            $"{_settings.Value.CatalogUrl}/GetBrands",
            HttpMethod.Post,
            null
            );

        var list = new List<SelectListItem>()
        {
            new SelectListItem()
            {
                Text = "All"
            }
        };

        foreach (var item in result.Data)
        {
            list.Add(new SelectListItem()
            {
                Value = item.Id.ToString(),
                Text = item.Brand
            });
        }

        return list;
    }

    public async Task<IEnumerable<SelectListItem>> GetTypes()
    {
        var result = await _httpClient.SendAsync<ItemsListResponse<CatalogType>, object?>(
            $"{_settings.Value.CatalogUrl}/GetTypes",
            HttpMethod.Post,
            null
            );

        var list = new List<SelectListItem>()
        {
            new SelectListItem()
            {
                Text = "All"
            }
        };

        foreach (var item in result.Data)
        {
            list.Add(new SelectListItem()
            {
                Value = item.Id.ToString(),
                Text = item.Type
            });
        }

        return list;
    }

    public async Task<CatalogItem> GetCatalogItemById(int id)
    {
        var result = await _httpClient.SendAsync<CatalogItem, object?>(
            $"{_settings.Value.CatalogUrl}/GetById",
            HttpMethod.Post,
            new GetItemRequest
            {
                Id = id
            }
            );

        return result;
    }

    public IEnumerable<SelectListItem> GetSortingTypes()
    {
        var list = new List<SelectListItem>();
        var names = Enum.GetNames(typeof(CatalogTypeSorting));
        var values = Enum.GetValues(typeof(CatalogTypeSorting));

        for (var i = 0; i < names.Length; i++)
        {
            list.Add(new SelectListItem(names[i], ((int)values.GetValue(i)).ToString()));
        }

        return list;
    }
}
