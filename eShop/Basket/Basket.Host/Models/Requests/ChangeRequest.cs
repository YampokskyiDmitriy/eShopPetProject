using Basket.Host.Models.Dtos;

namespace Basket.Host.Models.Requests
{
    public class ChangeRequest
    {
        public CatalogItemDto catalogItem { get; set; }
        public bool change { get; set; }
    }
}
