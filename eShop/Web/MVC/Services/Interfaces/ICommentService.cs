using MVC.Models.Requests;
using MVC.ViewModels;

namespace MVC.Services.Interfaces
{
    public interface ICommentService
    {
        Task<Comment?> Add(AddCommentRequest request);
        Task<IEnumerable<Comment>> GetComments(GetRequest request);
        Task<Comment?> Remove(RemoveRequest request);
    }
}