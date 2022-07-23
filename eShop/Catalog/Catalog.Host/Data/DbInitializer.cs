using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data;

public static class DbInitializer
{
    public static async Task Initialize(ApplicationDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (!context.CatalogBrands.Any())
        {
            await context.CatalogBrands.AddRangeAsync(GetPreconfiguredCatalogBrands());

            await context.SaveChangesAsync();
        }

        if (!context.CatalogTypes.Any())
        {
            await context.CatalogTypes.AddRangeAsync(GetPreconfiguredCatalogTypes());

            await context.SaveChangesAsync();
        }

        if (!context.CatalogItems.Any())
        {
            await context.CatalogItems.AddRangeAsync(GetPreconfiguredItems());

            await context.SaveChangesAsync();
        }
    }

    private static IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
    {
        return new List<CatalogBrand>()
        {
            new CatalogBrand() { Brand = "DC" },
            new CatalogBrand() { Brand = "Marvel" },
            new CatalogBrand() { Brand = "Shogakukan" },
            new CatalogBrand() { Brand = "Kodansha" },
            new CatalogBrand() { Brand = "Shueisha" },
            new CatalogBrand() { Brand = "DarkHorse" },
            new CatalogBrand() { Brand = "Verigo" },
            new CatalogBrand() { Brand = "Other" }
        };
    }

    private static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
    {
        return new List<CatalogType>()
        {
            new CatalogType() { Type = "ComicBook" },
            new CatalogType() { Type = "Manga" }
        };
    }

    private static IEnumerable<CatalogItem> GetPreconfiguredItems()
    {
        return new List<CatalogItem>()
        {
            new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 1, AvailableStock = 100, Description = "Justice: The Deluxe Edition", Name = "Justice: The Deluxe Edition", Price = 19.5M, PictureFileName = "1.jpg" },
            new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 2, AvailableStock = 100, Description = "Moon Knight Omnibus Vol. 1", Name = "Moon Knight Omnibus Vol. 1", Price = 8.50M, PictureFileName = "2.jpg" },
            new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 6, AvailableStock = 100, Description = "Hellsing Deluxe Volume 1", Name = "Hellsing Deluxe Volume 1", Price = 12, PictureFileName = "3.jpg" },
            new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 6, AvailableStock = 100, Description = "Hellboy Omnibus Volume 2", Name = "Hellboy Omnibus Volume 2", Price = 12, PictureFileName = "4.jpg" },
            new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 7, AvailableStock = 100, Description = "Daytripper: Deluxe Edition", Name = "Daytripper: Deluxe Edition", Price = 8.5M, PictureFileName = "5.jpg" },
            new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 7, AvailableStock = 100, Description = "Lucifer Omnibus Vol. 1", Name = "Lucifer Omnibus Vol. 1", Price = 12, PictureFileName = "6.jpg" },
            new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 3, AvailableStock = 100, Description = "Pocket Monsters Special Vol.1", Name = "Pocket Monsters Special Vol.1", Price = 12, PictureFileName = "7.jpg" },
            new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 3, AvailableStock = 100, Description = "Haikyu!!, Vol. 35", Name = "Haikyu!!, Vol. 35", Price = 8.5M, PictureFileName = "8.jpg" },
            new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 4, AvailableStock = 100, Description = "Fire Force 27", Name = "Fire Force 27", Price = 12, PictureFileName = "9.jpg" },
            new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 4, AvailableStock = 100, Description = "Cells at Work! 6", Name = "Cells at Work! 6", Price = 12, PictureFileName = "10.jpg" },
            new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 5, AvailableStock = 100, Description = "JOJO'S BIZARRE ADVENTURE 1", Name = "JOJO'S BIZARRE ADVENTURE 1", Price = 8.5M, PictureFileName = "11.jpg" },
            new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 5, AvailableStock = 100, Description = "One Piece, Vol. 3", Name = "One Piece, Vol. 3", Price = 12, PictureFileName = "12.jpg" },
        };
    }
}