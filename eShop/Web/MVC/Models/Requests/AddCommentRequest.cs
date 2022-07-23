namespace MVC.Models.Requests
{
    public class AddCommentRequest
    {
        public int ProductId { get; set; }
        public string UserName { get; set; } = null!;
        public int Rate { get; set; }
        public string Commentary { get; set; } = null!;
    }
}
