using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories
{
    public class CatalogTypeRepository : ICatalogTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogBrandRepository> _logger;

        public CatalogTypeRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogBrandRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<CatalogType>> GetTypesAsync()
        {
            var types = await _dbContext.CatalogTypes
                .ToListAsync();

            return types;
        }

        public async Task<CatalogType?> Create(string title)
        {
            var type = new CatalogType { Type = title };

            await _dbContext.CatalogTypes.AddAsync(type);

            _dbContext.SaveChanges();

            return type;
        }

        public async Task<CatalogType?> Delete(int id)
        {
            var type = await _dbContext.CatalogTypes.FirstOrDefaultAsync(x => x.Id == id);

            if (type is null)
            {
                return null;
            }

            _dbContext.Remove(type);
            _dbContext.SaveChanges();

            return type;
        }

        public async Task<CatalogType?> Update(int id, string name)
        {
            var type = await _dbContext.CatalogTypes.FirstOrDefaultAsync(x => x.Id == id);

            if (type is null)
            {
                return null;
            }

            type.Type = name;
            _dbContext.CatalogTypes.Update(type);
            _dbContext.SaveChanges();

            return type;
        }

        public async Task<CatalogType?> GetType(int id)
        {
            var result = await _dbContext.CatalogTypes
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }
    }
}
