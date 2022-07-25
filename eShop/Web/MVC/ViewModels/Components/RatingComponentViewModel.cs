namespace MVC.ViewModels.Components
{
    public class RatingComponentViewModel
    {
        public int ProductId { get; set; }
        public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();
    }
}
