using Infrastructure.Identity;

namespace MVC;

public class AppSettings
{
    public string CatalogUrl { get; set; }
    public string BasketUrl { get; set; }
    public string CommentUrl { get; set; }
    public int SessionCookieLifetimeMinutes { get; set; }    
    public string CallBackUrl { get; set; }
    public string IdentityUrl { get; set; }
    public string AdminItemUrl { get; set; }
    public string AdminBrandUrl { get; set; }
    public string AdminTypeUrl { get; set; }
}
