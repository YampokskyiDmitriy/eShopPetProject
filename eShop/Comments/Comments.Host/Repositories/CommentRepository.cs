using Comments.Host.Data;
using Comments.Host.Data.Entities;
using Comments.Host.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Comments.Host.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CommentRepository> _logger;

        public CommentRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CommentRepository> logger
            )
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<CommentEntity?> GetCommentAsync(int id)
        {
            return await _dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<CommentEntity>> GetCommentsByProductIdAsync(int id)
        {
            return await _dbContext.Comments.Where(x => x.ProductId == id).ToListAsync();
        }

        public async Task<CommentEntity?> AddCommentAsync(CommentEntity comment)
        {
            var userHaveComment = await _dbContext.Comments
                .Where(x => x.ProductId == comment.ProductId)
                .FirstOrDefaultAsync(x => x.UserId == comment.UserId);

            if (userHaveComment is not null)
            {
                return null;
            }

            var entity = await _dbContext.AddAsync(comment);
            _dbContext.SaveChanges();

            return entity.Entity;
        }

        public async Task<CommentEntity?> RemoveCommentAsync(int id)
        {
            var entity = await _dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is not null)
            {
                _dbContext.Comments.Remove(entity);
                _dbContext.SaveChanges();
                return entity;
            }

            return null;

        }
    }
}
