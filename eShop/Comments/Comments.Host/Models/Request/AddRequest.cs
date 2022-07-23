namespace Comments.Host.Models.Request
{
    public class AddRequest
    {
        public int ProductId { get; set; }
        public string UserName { get; set; } = null!;
        public int Rate { get; set; }
        public string Commentary { get; set; } = null!;
    }
}
