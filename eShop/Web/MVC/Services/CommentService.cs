using MVC.Models.Requests;
using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Services
{
    public class CommentService : ICommentService
    {
        private IHttpClientService _httpClient;
        private AppSettings _setting;
        public CommentService(IHttpClientService httpClient, IOptionsSnapshot<AppSettings> options)
        {
            _httpClient = httpClient;
            _setting = options.Value;
        }

        public async Task<IEnumerable<Comment>> GetComments(GetRequest request)
        {
            var result = await _httpClient.SendAsync<IEnumerable<Comment>, GetRequest>($"{_setting.CommentUrl}/GetComments",
                HttpMethod.Post,
                request);

            return result;
        }

        [HttpPost]
        public async Task<Comment?> Add(AddCommentRequest request)
        {
            var result = await _httpClient.SendAsync<Comment?, AddCommentRequest>($"{_setting.CommentUrl}/Add",
                HttpMethod.Post,
                request);

            return result;
        }

        [HttpPost]
        public async Task<Comment?> Remove(RemoveRequest request)
        {
            var result = await _httpClient.SendAsync<Comment?, RemoveRequest>($"{_setting.CommentUrl}/Remove",
                HttpMethod.Post,
                request);

            return result;
        }
    }
}
