using Microsoft.AspNetCore.Mvc;
using MVC.Models.Requests;
using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCommentRequest request)
        {
            await _commentService.Add(request);

            return Redirect($"~/Catalog/Single?prodID={request.ProductId}");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(RemoveRequest request)
        {
            await _commentService.Remove(request);

            return Redirect($"~/");
        }
    }
}
