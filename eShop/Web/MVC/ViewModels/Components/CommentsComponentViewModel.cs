namespace MVC.ViewModels.Components
{
    public class CommentsComponentViewModel
    {
        public int ProductId { get; set; }
        public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();
    }
}
