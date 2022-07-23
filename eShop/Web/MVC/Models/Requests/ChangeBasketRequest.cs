using MVC.ViewModels;

namespace MVC.Models.Requests
{
    public class ChangeBasketRequest
    {
        public CatalogItem catalogItem { get; set; }
        public bool change { get; set; }
    }
}
