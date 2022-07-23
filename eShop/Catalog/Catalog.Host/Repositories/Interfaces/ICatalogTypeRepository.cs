using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<IEnumerable<CatalogType>> GetTypesAsync();
        Task<CatalogType?> GetType(int id);
        Task<CatalogType?> Create(string title);
        Task<CatalogType?> Delete(int id);
        Task<CatalogType?> Update(int id, string name);
    }
}
