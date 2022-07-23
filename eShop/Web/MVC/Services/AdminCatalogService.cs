using MVC.Models.Requests;
using MVC.Models.Response;
using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Services
{
    public class AdminCatalogService : IAdminCatalogService
    {
        private readonly IHttpClientService _httpClient;
        private readonly AppSettings _settings;

        public AdminCatalogService(IHttpClientService httpClient,
            IOptions<AppSettings> settings,
            ICatalogService catalogService)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        public async Task<CatalogBrand?> AddBrand(AddRequest request)
        {
            var result = await _httpClient.SendAsync<CatalogBrand?, AddRequest>(
                    $"{_settings.AdminBrandUrl}/Create",
                    HttpMethod.Post,
                    request
                );

            return result;
        }

        public async Task<CatalogItem?> AddItem(AddItemRequest request)
        {
            var result = await _httpClient.SendAsync<CatalogItem?, AddItemRequest>(
                    $"{_settings.AdminItemUrl}/Add",
                    HttpMethod.Post,
                    request
                );

            return result;
        }

        public async Task<CatalogType?> AddType(AddRequest request)
        {
            var result = await _httpClient.SendAsync<CatalogType?, AddRequest>(
                    $"{_settings.AdminTypeUrl}/Create",
                    HttpMethod.Post,
                    request
                );

            return result;
        }

        public async Task<CatalogBrand> GetBrand(int id)
        {
            var result = await _httpClient.SendAsync<CatalogBrand, object?>(
            $"{_settings.AdminBrandUrl}/GetBrand",
            HttpMethod.Post,
            new GetRequest
            {
                Id = id
            }
            );

            return result;
        }

        public async Task<IEnumerable<CatalogBrand>> GetBrands()
        {
            var result = await _httpClient.SendAsync<IEnumerable<CatalogBrand>, object>(
                    $"{_settings.AdminBrandUrl}/Brands",
                    HttpMethod.Post,
                    null
                );

            return result;
        }

        public async Task<IEnumerable<SelectListItem>> GetBrandSelectList()
        {
            var result = await _httpClient.SendAsync<IEnumerable<CatalogBrand>, object?>(
            $"{_settings.AdminBrandUrl}/Brands",
            HttpMethod.Post,
            null
            );

            var list = new List<SelectListItem>();

            foreach (var item in result)
            {
                if (item.Brand.Count() > 20)
                {
                    item.Brand = $"{item.Brand.Substring(0, 20)}...";
                }

                list.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Brand
                });
            }

            return list;
        }

        public async Task<CatalogItem> GetItem(int id)
        {
            var result = await _httpClient.SendAsync<CatalogItem, object?>(
            $"{_settings.AdminItemUrl}/GetItem",
            HttpMethod.Post,
            new GetRequest
            {
                Id = id
            }
            );

            return result;
        }

        public async Task<IEnumerable<CatalogItem>> GetItems()
        {
            var result = await _httpClient.SendAsync<IEnumerable<CatalogItem>, object>(
                    $"{_settings.AdminItemUrl}/Items",
                    HttpMethod.Post,
                    null
                );

            return result;
        }

        public async Task<CatalogType> GetType(int id)
        {
            var result = await _httpClient.SendAsync<CatalogType, object?>(
            $"{_settings.AdminTypeUrl}/GetType",
            HttpMethod.Post,
            new GetRequest
            {
                Id = id
            }
            );

            return result;
        }

        public async Task<IEnumerable<CatalogType>> GetTypes()
        {
            var result = await _httpClient.SendAsync<IEnumerable<CatalogType>, object>(
                    $"{_settings.AdminTypeUrl}/Types",
                    HttpMethod.Post,
                    null
                );

            return result;
        }

        public async Task<IEnumerable<SelectListItem>> GetTypeSelectList()
        {
            var result = await _httpClient.SendAsync<IEnumerable<CatalogType>, object?>(
            $"{_settings.AdminTypeUrl}/Types",
            HttpMethod.Post,
            null
            );

            var list = new List<SelectListItem>();

            foreach (var item in result)
            {
                if (item.Type.Count() > 20)
                {
                    item.Type = $"{item.Type.Substring(0, 20)}...";
                }

                list.Add(new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Type
                });
            }

            return list;
        }

        public async Task<CatalogBrand?> RemoveBrand(RemoveRequest request)
        {
            var result = await _httpClient.SendAsync<CatalogBrand?, RemoveRequest>(
                    $"{_settings.AdminBrandUrl}/Delete",
                    HttpMethod.Post,
                    request
                );
            return result;
        }

        public async Task<CatalogItem?> RemoveItem(RemoveRequest request)
        {
            var result = await _httpClient.SendAsync<CatalogItem?, RemoveRequest>(
                    $"{_settings.AdminItemUrl}/Delete",
                    HttpMethod.Post,
                    request
                );
            return result;
        }

        public async Task<CatalogType?> RemoveType(RemoveRequest request)
        {
            var result = await _httpClient.SendAsync<CatalogType?, RemoveRequest>(
                    $"{_settings.AdminTypeUrl}/Delete",
                    HttpMethod.Post,
                    request
                );
            return result;
        }

        public async Task<CatalogBrand?> UpdateBrand(UpdateRequest request)
        {
            var result = await _httpClient.SendAsync<CatalogBrand?, UpdateRequest>(
                    $"{_settings.AdminBrandUrl}/Update",
                    HttpMethod.Post,
                    request
                );

            return result;
        }

        public async Task<CatalogItem?> UpdateItem(UpdateItemRequest request)
        {
            var result = await _httpClient.SendAsync<CatalogItem?, UpdateItemRequest>(
                    $"{_settings.AdminItemUrl}/Update",
                    HttpMethod.Post,
                    request
                );

            return result;
        }

        public async Task<CatalogType?> UpdateType(UpdateRequest request)
        {
            var result = await _httpClient.SendAsync<CatalogType?, UpdateRequest>(
                    $"{_settings.AdminTypeUrl}/Update",
                    HttpMethod.Post,
                    request
                );

            return result;
        }
    }
}
