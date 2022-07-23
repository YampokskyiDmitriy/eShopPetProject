using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogBrandRepository
    {
        Task<IEnumerable<CatalogBrand>> GetBrandsAsync();
        Task<CatalogBrand?> GetBrand(int id);
        Task<CatalogBrand?> Create(string name);
        Task<CatalogBrand?> Delete(int id);
        Task<CatalogBrand?> Update(int id, string name);
    }
}