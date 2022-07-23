namespace MVC.Models.Requests
{
    public class MvcItemUpdateRequest
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public int TypeApplied { get; set; }

        public int BrandApplied { get; set; }

        public string PictureFileName { get; set; } = null!;

        public int AvailableStock { get; set; }
    }
}
