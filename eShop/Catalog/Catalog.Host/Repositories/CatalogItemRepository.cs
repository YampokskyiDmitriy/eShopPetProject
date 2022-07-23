using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Data.Enums;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories;

public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize, int? brandFilter, int? typeFilter, CatalogTypeSorting? sorting)
    {
        IQueryable<CatalogItem> query = _dbContext.CatalogItems;

        if (brandFilter.HasValue)
        {
            query = query.Where(w => w.CatalogBrandId == brandFilter.Value);
        }

        if (typeFilter.HasValue)
        {
            query = query.Where(w => w.CatalogTypeId == typeFilter.Value);
        }

        switch (sorting)
        {
            case CatalogTypeSorting.PriceAsc:
                query = query.OrderBy(x => x.Price);
                break;
            case CatalogTypeSorting.PriceDesc:
                query = query.OrderByDescending(x => x.Price);
                break;
            case CatalogTypeSorting.NameAsc:
                query = query.OrderBy(x => x.Name);
                break;
            case CatalogTypeSorting.NameDesc:
                query = query.OrderByDescending(x => x.Name);
                break;
            default:
                query = query.OrderBy(x => x.Name);
                break;
        }

        var totalItems = await query.LongCountAsync();

        var itemsOnPage = await query.Include(i => i.CatalogBrand)
           .Include(i => i.CatalogType)
           .Skip(pageSize * pageIndex)
           .Take(pageSize)
           .ToListAsync();

        return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<CatalogItem?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var item = new CatalogItem
        {
            CatalogBrandId = catalogBrandId,
            CatalogTypeId = catalogTypeId,
            Description = description,
            Name = name,
            PictureFileName = pictureFileName,
            Price = price
        };
        var result = await _dbContext.CatalogItems.AddAsync(item);
        _dbContext.SaveChanges();

        return result.Entity;
    }

    public async Task<CatalogItem?> Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName)
    {
        var item = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (item is not null)
        {
            item.CatalogBrandId = catalogBrandId;
            item.CatalogTypeId = catalogTypeId;
            item.Description = description;
            item.Name = name;
            item.PictureFileName = pictureFileName;
            item.Price = price;
            item.AvailableStock = availableStock;

            await _dbContext.SaveChangesAsync();
            return item;
        }

        return null;
    }

    public async Task<CatalogItem?> Delete(int id)
    {
        var item = await _dbContext.CatalogItems.FirstOrDefaultAsync(x => x.Id == id);

        if (item is null)
        {
            return null;
        }

        var result = _dbContext.Remove(item);
        _dbContext.SaveChanges();

        return result.Entity;
    }

    public async Task<CatalogItem?> GetByIdAsync(int id)
    {
        var res = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogType)
            .FirstAsync(ci => ci.Id == id);

        return res;
    }

    public async Task<IEnumerable<CatalogItem>> GetItemsAsync()
    {
        var items = await _dbContext.CatalogItems
                .Include(i => i.CatalogBrand)
                .Include(i => i.CatalogType)
                .ToListAsync();

        return items;
    }
}