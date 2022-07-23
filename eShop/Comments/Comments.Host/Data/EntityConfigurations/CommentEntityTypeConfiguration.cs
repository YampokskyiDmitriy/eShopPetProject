using Comments.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Comments.Host.Data.EntityConfigurations
{
    public class CommentEntityTypeConfiguration
        : IEntityTypeConfiguration<CommentEntity>
    {
        public void Configure(EntityTypeBuilder<CommentEntity> builder)
        {
            builder.ToTable("Comment");

            builder.Property(x => x.Id)
                .UseHiLo("comment_hilo")
                .IsRequired();

            builder.Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.Rate)
                .IsRequired();

            builder.Property(x => x.Commentary)
                .IsRequired()
                .HasMaxLength(1000);
        }
    }
}
