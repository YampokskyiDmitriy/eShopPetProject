using MVC.Models.Requests;
using MVC.Services.Interfaces;
using MVC.ViewModels.Admin;

namespace MVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminCatalogService _adminCatalogService;

        public AdminController(IAdminCatalogService adminCatalogService)
        {
            _adminCatalogService = adminCatalogService;
        }
        public IActionResult AdminPage()
        {
            return View();
        }

        public async Task<IActionResult> ItemListPage()
        {
            var model = new ItemListPageViewModel
            {
                Items = await _adminCatalogService.GetItems()
            };

            return View(model);
        }

        public async Task<IActionResult> ItemUpdatePage(int id)
        {
            var item = await _adminCatalogService.GetItem(id);
            var pictureFileName = item.PictureUrl.Split('/').LastOrDefault();
            var model = new ItemUpdatePageViewModel
            {
                Item = item,
                Brands = await _adminCatalogService.GetBrandSelectList(),
                Types = await _adminCatalogService.GetTypeSelectList(),
                PictureFileName = pictureFileName
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ItemUpdate(MvcItemUpdateRequest request)
        {
            await _adminCatalogService.UpdateItem(new UpdateItemRequest
            {
                Id = request.Id,
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                CatalogBrandId = request.BrandApplied,
                CatalogTypeId = request.TypeApplied,
                AvailableStock = request.AvailableStock,
                PictureFileName = request.PictureFileName
            });

            return Redirect("~/");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int id)
        {
            await _adminCatalogService.RemoveItem(new RemoveRequest { Id = id });
            return Redirect("~/");
        }

        public async Task<IActionResult> AddItemPage()
        {
            var model = new AddItemPageViewModel
            {
                Brands = await _adminCatalogService.GetBrandSelectList(),
                Types = await _adminCatalogService.GetTypeSelectList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(AddItemRequest request)
        {
            await _adminCatalogService.AddItem(request);
            return Redirect("~/");
        }

        public async Task<IActionResult> BrandListPage()
        {
            var model = new BrandListPageViewModel
            {
                Brands = await _adminCatalogService.GetBrands()
            };

            return View(model);
        }

        public async Task<IActionResult> BrandUpdatePage(int id)
        {
            var model = new BrandUpdatePageViewModel
            {
                Brand = await _adminCatalogService.GetBrand(id)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> BrandUpdate(UpdateRequest request)
        {
            await _adminCatalogService.UpdateBrand(request);

            return Redirect("~/");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveBrand(int id)
        {
            await _adminCatalogService.RemoveBrand(new RemoveRequest { Id = id });
            return Redirect("~/");
        }

        public IActionResult AddBrandPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBrand(AddRequest request)
        {
            await _adminCatalogService.AddBrand(request);
            return Redirect("~/");
        }

        public async Task<IActionResult> TypeListPage()
        {
            var model = new TypeListPageViewModel
            {
                Types = await _adminCatalogService.GetTypes()
            };

            return View(model);
        }

        public async Task<IActionResult> TypeUpdatePage(int id)
        {
            var model = new TypeUpdatePageViewModel
            {
                Type = await _adminCatalogService.GetType(id)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> TypeUpdate(UpdateRequest request)
        {
            await _adminCatalogService.UpdateType(request);

            return Redirect("~/");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveType(int id)
        {
            await _adminCatalogService.RemoveType(new RemoveRequest { Id = id });
            return Redirect("~/");
        }

        public IActionResult AddTypePage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddType(AddRequest request)
        {
            await _adminCatalogService.AddType(request);
            return Redirect("~/");
        }
    }
}
