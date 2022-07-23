using Comments.Host.Models.Dtos;
using Comments.Host.Models.Request;

namespace Comments.Host.Services.Interfaces
{
    public interface ICommentService
    {
        Task<CommentDto?> AddCommentAsync(AddRequest request, int userId);
        Task<IEnumerable<CommentDto>> GetCommentsByProductIdAsync(int id);
        Task<CommentDto?> RemoveCommentAsync(int commentId, int userId);
    }
}
