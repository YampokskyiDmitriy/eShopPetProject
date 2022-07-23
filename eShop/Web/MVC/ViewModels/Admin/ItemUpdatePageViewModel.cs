namespace MVC.ViewModels.Admin
{
    public class ItemUpdatePageViewModel
    {
        public CatalogItem Item { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public IEnumerable<SelectListItem> Brands { get; set; }
        public int? TypeApplied { get; set; }
        public int? BrandApplied { get; set; }
        public string PictureFileName { get; set; }
    }
}
