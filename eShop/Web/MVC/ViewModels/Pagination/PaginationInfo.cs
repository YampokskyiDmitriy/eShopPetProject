using MVC.Models.Enums;

namespace MVC.ViewModels.Pagination;

public class PaginationInfo
{
    public CatalogTypeSorting? sortingApplied { get; set; }
    public int TotalItems { get; set; }
    public int ItemsPerPage { get; set; }
    public int ActualPage { get; set; }
    public int PageItemsRequest { get; set; }
    public int TotalPages { get; set; }
    public string Previous { get; set; }
    public string Next { get; set; }
}
