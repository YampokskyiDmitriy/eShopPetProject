using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogTypeService : BaseDataService<ApplicationDbContext>, ICatalogTypeService
    {
        private readonly ICatalogTypeRepository _catalogTypeRepository;
        private readonly IMapper _mapper;

        public CatalogTypeService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogTypeRepository catalogTypeRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _catalogTypeRepository = catalogTypeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CatalogTypeDto>> GetTypesAsync()
        {
            var result = await _catalogTypeRepository.GetTypesAsync();
            return result.Select(x => _mapper.Map<CatalogTypeDto>(x)).ToList();
        }

        public async Task<CatalogTypeDto?> Create(string title)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _catalogTypeRepository.Create(title);
                return _mapper.Map<CatalogTypeDto>(result);
            });
        }

        public async Task<CatalogTypeDto?> Delete(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _catalogTypeRepository.Delete(id);
                return _mapper.Map<CatalogTypeDto>(result);
            });
        }

        public async Task<CatalogTypeDto?> Update(int id, string name)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _catalogTypeRepository.Update(id, name);
                return _mapper.Map<CatalogTypeDto>(result);
            });
        }

        public async Task<CatalogTypeDto?> GetTypeAsync(int id)
        {
            var result = await _catalogTypeRepository.GetType(id);

            return _mapper.Map<CatalogTypeDto>(result);
        }
    }
}
