using Basket.Host.Models.Dtos;

namespace Basket.Host.Models.Requests
{
    public class AddRequest
    {
        public CatalogItemDto catalogItem { get; set; }
        public int countItems { get; set; }
    }
}
