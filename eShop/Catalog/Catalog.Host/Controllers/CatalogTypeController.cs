using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Requests.Brand;
using Catalog.Host.Models.Requests.Type;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AdminPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogTypeController : ControllerBase
{
    private readonly ILogger<CatalogTypeController> _logger;
    private readonly ICatalogTypeService _catalogTypeService;

    public CatalogTypeController(ILogger<CatalogTypeController> logger, ICatalogTypeService catalogTypeService)
    {
        _logger = logger;
        _catalogTypeService = catalogTypeService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(IEnumerable<CatalogTypeDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Types()
    {
        var result = await _catalogTypeService.GetTypesAsync();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CatalogTypeDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetType(GetItemRequest request)
    {
        var result = await _catalogTypeService.GetTypeAsync(request.Id);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CatalogTypeDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Create(CreateTypeRequest request)
    {
        var result = await _catalogTypeService.Create(request.Name);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CatalogTypeDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(DeleteTypeRequest request)
    {
        var result = await _catalogTypeService.Delete(request.Id);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CatalogTypeDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateTypeRequest request)
    {
        var result = await _catalogTypeService.Update(request.Id, request.Name);
        return Ok(result);
    }
}