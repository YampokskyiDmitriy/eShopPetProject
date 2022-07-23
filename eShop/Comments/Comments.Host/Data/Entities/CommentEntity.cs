namespace Comments.Host.Data.Entities
{
    public class CommentEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public int Rate { get; set; }
        public string Commentary { get; set; } = null!;
    }
}
