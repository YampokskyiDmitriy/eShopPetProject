using AutoMapper;
using Comments.Host.Data;
using Comments.Host.Data.Entities;
using Comments.Host.Models.Dtos;
using Comments.Host.Models.Request;
using Comments.Host.Repositories.Interfaces;
using Comments.Host.Services.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;

namespace Comments.Host.Services
{
    public class CommentService : BaseDataService<ApplicationDbContext>, ICommentService
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepository _repository;
        public CommentService(
            IMapper mapper,
            ICommentRepository repository,
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger
        ) : base(dbContextWrapper, logger)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommentDto?> AddCommentAsync(AddRequest request, int userId)
        {
            var entity = _mapper.Map<CommentEntity>(request);
            entity.UserId = userId;
            var result = await ExecuteSafeAsync(async () =>
            {
                return await _repository.AddCommentAsync(entity);
            });

            return _mapper.Map<CommentDto>(result);
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsByProductIdAsync(int id)
        {

            var entitys = await _repository.GetCommentsByProductIdAsync(id);

            return entitys.Select(x => _mapper.Map<CommentDto>(x));
        }

        public async Task<CommentDto?> RemoveCommentAsync(int commentId, int userId)
        {
            var comment = await _repository.GetCommentAsync(commentId);

            if (comment!.UserId != userId)
            {
                return null;
            }

            var entity = await ExecuteSafeAsync(async () =>
            {
                return await _repository.RemoveCommentAsync(commentId);
            });

            return _mapper.Map<CommentDto>(entity);
        }
    }
}
