using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogBrandService
    {
        Task<IEnumerable<CatalogBrandDto>> GetBrandsAsync();
        Task<CatalogBrandDto?> GetBrandAsync(int id);
        Task<CatalogBrandDto?> Create(string name);
        Task<CatalogBrandDto?> Delete(int id);
        Task<CatalogBrandDto?> Update(int id, string name);
    }
}
