using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories
{
    public class CatalogBrandRepository : ICatalogBrandRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogBrandRepository> _logger;

        public CatalogBrandRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogBrandRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<CatalogBrand>> GetBrandsAsync()
        {
            var brands = await _dbContext.CatalogBrands
                .ToListAsync();

            return brands;
        }

        public async Task<CatalogBrand?> Create(string title)
        {
            var brand = new CatalogBrand { Brand = title };

            await _dbContext.CatalogBrands.AddAsync(brand);

            _dbContext.SaveChanges();

            return brand;
        }

        public async Task<CatalogBrand?> Delete(int id)
        {
            var brands = await _dbContext.CatalogBrands.FirstOrDefaultAsync(x => x.Id == id);

            if (brands is null)
            {
                return null;
            }

            _dbContext.Remove(brands);
            _dbContext.SaveChanges();

            return brands;
        }

        public async Task<CatalogBrand> Update(int id, string title)
        {
            var brands = await _dbContext.CatalogBrands.FirstOrDefaultAsync(x => x.Id == id);

            if (brands is null)
            {
                return null;
            }

            brands.Brand = title;
            _dbContext.CatalogBrands.Update(brands);
            _dbContext.SaveChanges();

            return brands;
        }

        public async Task<CatalogBrand?> GetBrand(int id)
        {
            var result = await _dbContext.CatalogBrands
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }
    }
}
