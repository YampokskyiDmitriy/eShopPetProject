using Catalog.Host.Configurations;
using Catalog.Host.Data.Enums;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Requests.Brand;
using Catalog.Host.Models.Requests.Type;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Host.Controllers;

[ApiController]

// [Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBffController : ControllerBase
{
    private readonly ILogger<CatalogBffController> _logger;
    private readonly ICatalogService _catalogService;
    private readonly IOptions<CatalogConfig> _config;
    private readonly ICatalogBrandService _catalogBrandService;
    private readonly ICatalogTypeService _catalogTypeService;
    private readonly ICatalogItemService _catalogItemService;

    public CatalogBffController(
        ILogger<CatalogBffController> logger,
        ICatalogService catalogService,
        IOptions<CatalogConfig> config,
        ICatalogBrandService catalogBrandService,
        ICatalogItemService catalogItemService,
        ICatalogTypeService catalogTypeService)
    {
        _logger = logger;
        _catalogService = catalogService;
        _config = config;
        _catalogBrandService = catalogBrandService;
        _catalogTypeService = catalogTypeService;
        _catalogItemService = catalogItemService;
    }

    [HttpPost]

    // [AllowAnonymous]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Items(PaginatedItemsRequest<CatalogTypeFilter, CatalogTypeSorting> request)
    {
        var result = await _catalogService.GetCatalogItemsAsync(request.PageSize, request.PageIndex, request.Filters, request.Sorting);
        return Ok(result);
    }

    [HttpPost]

    // [AllowAnonymous]
    [ProducesResponseType(typeof(ItemsListResponse<CatalogBrandDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBrands()
    {
        var list = await _catalogBrandService.GetBrandsAsync();
        var result = new ItemsListResponse<CatalogBrandDto>
        {
            Data = list,
            Count = list.Count()
        };
        return Ok(result);
    }

    [HttpPost]

    // [AllowAnonymous]
    [ProducesResponseType(typeof(ItemsListResponse<CatalogTypeDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetTypes()
    {
        var list = await _catalogTypeService.GetTypesAsync();
        var result = new ItemsListResponse<CatalogTypeDto>
        {
            Data = list,
            Count = list.Count()
        };
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CatalogItemDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetById(GetItemRequest request)
    {
        var result = await _catalogService.GetByIdAsync(request.Id);

        return Ok(result);
    }
}