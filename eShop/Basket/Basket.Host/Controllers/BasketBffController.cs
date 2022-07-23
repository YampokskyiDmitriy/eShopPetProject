using Basket.Host.Models;
using Basket.Host.Models.Requests;
using Basket.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Basket.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class BasketBffController : Controller
{
    private readonly ILogger<BasketBffController> _logger;
    private readonly IBasketService _basketService;

    public BasketBffController(
        ILogger<BasketBffController> logger, IBasketService basketService
    )
    {
        _logger = logger;
        _basketService = basketService;
    }

    [HttpPost]
    public async Task Add(AddRequest addRequest)
    {
        _logger.LogInformation("Start add");
        var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        _logger.LogInformation($"userId: {userId} {User.Claims.FirstOrDefault(x => x.Type == "role")?.Value}");
        await _basketService.AddAsync(addRequest.catalogItem, addRequest.countItems, userId!);
        Ok();
    }

    [HttpPost]
    public async Task Delete(DeleteRequest deleteRequest)
    {
        _logger.LogInformation("Start delete");
        var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        _logger.LogInformation($"userId: {userId} {User.Claims.FirstOrDefault(x => x.Type == "role")?.Value}");
        await _basketService.DeleteAsync(deleteRequest.catalogItem, userId!);
        Ok();
    }

    [HttpPost]
    public async Task Change(ChangeRequest changeRequest)
    {
        _logger.LogInformation("Start change");
        var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        _logger.LogInformation($"userId: {userId} {User.Claims.FirstOrDefault(x => x.Type == "role")?.Value}");
        await _basketService.ChangeCountAsync(changeRequest.catalogItem, userId!, changeRequest.change);
        Ok();
    }

    [HttpPost]
    public async Task<IActionResult>? Get()
    {
        _logger.LogInformation("Start get");
        var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
        _logger.LogInformation($"userId: {userId}");
        var response = await _basketService.GetAsync(userId!);
        _logger.LogInformation($"response get: {response}");
        return Ok(response);
    }
}