using MVC.Models.Requests;
using MVC.Services.Interfaces;
using MVC.ViewModels.Components;

namespace MVC.ViewComponents
{
    public class CommentsComponent : ViewComponent
    {
        private readonly ICommentService _commentService;

        public CommentsComponent(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            

            var comments = await _commentService.GetComments(
                new GetRequest { Id = id }
                );            

            return View(new CommentsComponentViewModel { ProductId = id, Comments = comments } );
        }
    }
}
