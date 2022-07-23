using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogBrandService : BaseDataService<ApplicationDbContext>, ICatalogBrandService
    {
        private readonly ICatalogBrandRepository _catalogBrandRepository;
        private readonly IMapper _mapper;

        public CatalogBrandService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogBrandRepository catalogBrandRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _catalogBrandRepository = catalogBrandRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CatalogBrandDto>> GetBrandsAsync()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _catalogBrandRepository.GetBrandsAsync();
                return result.Select(x => _mapper.Map<CatalogBrandDto>(x)).ToList();
            });
        }

        public async Task<CatalogBrandDto?> Create(string title)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _catalogBrandRepository.Create(title);
                return _mapper.Map<CatalogBrandDto>(result);
            });
        }

        public async Task<CatalogBrandDto?> Delete(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _catalogBrandRepository.Delete(id);
                return _mapper.Map<CatalogBrandDto>(result);
            });
        }

        public async Task<CatalogBrandDto?> Update(int id, string name)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _catalogBrandRepository.Update(id, name);
                return _mapper.Map<CatalogBrandDto>(result);
            });
        }

        public async Task<CatalogBrandDto?> GetBrandAsync(int id)
        {
            var result = await _catalogBrandRepository.GetBrand(id);

            return _mapper.Map<CatalogBrandDto>(result);
        }
    }
}
