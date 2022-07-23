using MVC.ViewModels;

namespace MVC.Models.Requests
{
    public class AddBasketRequest
    {
        public CatalogItem catalogItem { get; set; }
        public int countItems { get; set; }
    }
}
