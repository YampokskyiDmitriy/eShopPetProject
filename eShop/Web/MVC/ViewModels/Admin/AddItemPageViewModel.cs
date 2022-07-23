namespace MVC.ViewModels.Admin
{
    public class AddItemPageViewModel
    {
        public IEnumerable<SelectListItem> Types { get; set; }
        public IEnumerable<SelectListItem> Brands { get; set; }
        public int? CatalogTypeId { get; set; }
        public int? CatalogBrandId { get; set; }
    }
}
