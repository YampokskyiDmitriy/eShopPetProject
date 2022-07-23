using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Requests.Brand;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AdminPolicy)]

[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBrandController : ControllerBase
{
    private readonly ILogger<CatalogBrandController> _logger;
    private readonly ICatalogBrandService _catalogBrandService;

    public CatalogBrandController(ILogger<CatalogBrandController> logger, ICatalogBrandService catalogItemService)
    {
        _logger = logger;
        _catalogBrandService = catalogItemService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<CatalogBrandDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Brands()
    {
        var result = await _catalogBrandService.GetBrandsAsync();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CatalogBrandDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBrand(GetItemRequest request)
    {
        var result = await _catalogBrandService.GetBrandAsync(request.Id);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CatalogBrandDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Create(CreateBrandRequest request)
    {
        var result = await _catalogBrandService.Create(request.Name);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CatalogBrandDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(DeleteBrandRequest request)
    {
        var result = await _catalogBrandService.Delete(request.Id);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CatalogBrandDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateBrandRequest request)
    {
        var result = await _catalogBrandService.Update(request.Id, request.Name);
        return Ok(result);
    }
}