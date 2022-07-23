using Basket.Host.Models.Dtos;

namespace Basket.Host.Models.Requests
{
    public class DeleteRequest
    {
        public CatalogItemDto catalogItem { get; set; }
    }
}
