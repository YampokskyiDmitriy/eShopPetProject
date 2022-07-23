using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogItemService : BaseDataService<ApplicationDbContext>, ICatalogItemService
{
    private readonly ICatalogItemRepository _catalogItemRepository;
    private readonly IMapper _mapper;

    public CatalogItemService(
        IMapper mapper,
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository)
        : base(dbContextWrapper, logger)
    {
        _mapper = mapper;
        _catalogItemRepository = catalogItemRepository;
    }

    public async Task<CatalogItemDto?> AddAsync(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.Add(name, description, price, availableStock, catalogBrandId, catalogTypeId, pictureFileName);
            return _mapper.Map<CatalogItemDto>(result);
        });
    }

    public async Task<CatalogItemDto?> Delete(int id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.Delete(id);
            return _mapper.Map<CatalogItemDto>(result);
        });
    }

    public async Task<CatalogItemDto?> GetItemAsync(int id)
    {
        var result = await _catalogItemRepository.GetByIdAsync(id);

        return _mapper.Map<CatalogItemDto>(result);
    }

    public async Task<IEnumerable<CatalogItemDto>> GetItemsAsync()
    {
        var items = await _catalogItemRepository.GetItemsAsync();
        return items.Select(x => _mapper.Map<CatalogItemDto>(x));
    }

    public async Task<CatalogItemDto?> Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.Update(id, name, description, price, availableStock, catalogBrandId, catalogTypeId, pictureFileName);
            return _mapper.Map<CatalogItemDto>(result);
        });
    }
}