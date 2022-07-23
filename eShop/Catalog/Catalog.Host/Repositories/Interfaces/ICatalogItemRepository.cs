using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Data.Enums;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogItemRepository
{
    Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize, int? brandFilter, int? typeFilter, CatalogTypeSorting? sorting);
    Task<CatalogItem?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<CatalogItem?> Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<CatalogItem?> Delete(int id);
    Task<IEnumerable<CatalogItem>> GetItemsAsync();
    Task<CatalogItem?> GetByIdAsync(int id);
}