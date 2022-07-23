using Comments.Host.Data.Entities;
using Comments.Host.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Comments.Host.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CommentEntity> Comments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CommentEntityTypeConfiguration());
        }
    }
}