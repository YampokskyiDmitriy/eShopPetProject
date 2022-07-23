using Comments.Host.Models.Dtos;
using Comments.Host.Models.Request;
using Comments.Host.Services.Interfaces;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Comment.Controllers
{
    [Route(ComponentDefaults.DefaultRoute)]
    [Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
    [ApiController]
    public class CommentBffController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentBffController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IEnumerable<CommentDto>> GetComments(GetRequest request)
        {
            return await _commentService.GetCommentsByProductIdAsync(request.Id);
        }

        [HttpPost]
        public async Task<CommentDto?> Add(AddRequest request)
        {
            if (!int.TryParse(User.Claims.FirstOrDefault(x => x.Type == "sub")!.Value,out int userId))
            {
                return null;
            }
            return await _commentService.AddCommentAsync(request, userId);
        }

        [HttpPost]
        public async Task<CommentDto?> Remove(RemoveRequest request)
        {
            if (!int.TryParse(User.Claims.FirstOrDefault(x => x.Type == "sub")!.Value, out int userId))
            {
                return null;
            }
            return await _commentService.RemoveCommentAsync(request.Id, userId);
        }
    }
}
