﻿namespace MVC.Models.Requests
{
    public class UpdateItemRequest
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public int CatalogTypeId { get; set; }

        public int CatalogBrandId { get; set; }

        public string PictureFileName { get; set; } = null!;

        public int AvailableStock { get; set; }
    }
}
