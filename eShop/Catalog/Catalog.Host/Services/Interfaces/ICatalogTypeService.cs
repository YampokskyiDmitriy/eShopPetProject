using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogTypeService
    {
        Task<IEnumerable<CatalogTypeDto>> GetTypesAsync();
        Task<CatalogTypeDto?> GetTypeAsync(int id);
        Task<CatalogTypeDto?> Create(string name);
        Task<CatalogTypeDto?> Delete(int id);
        Task<CatalogTypeDto?> Update(int id, string name);
    }
}