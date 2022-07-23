using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogItemService
{
    Task<CatalogItemDto?> GetItemAsync(int id);
    Task<IEnumerable<CatalogItemDto>> GetItemsAsync();
    Task<CatalogItemDto?> AddAsync(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<CatalogItemDto?> Delete(int id);
    Task<CatalogItemDto?> Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
}