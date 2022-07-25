using Comments.Host.Data.Entities;

namespace Comments.Host.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.Comments.Any())
            {
                await context.Comments.AddRangeAsync(GetPreconfiguredComments());

                await context.SaveChangesAsync();
            }


        }

        private static IEnumerable<CommentEntity> GetPreconfiguredComments()
        {
            return new List<CommentEntity>()
            {
            new CommentEntity
            {
                ProductId = 1,
                Rate = 4,
                Commentary = "Impressive reading",
                UserId = 88421113,
                UserName = "Bob Smith"
            },
            new CommentEntity
            {
                ProductId = 2,
                Rate = 5,
                Commentary = "Fantastic",
                UserId = 88421113,
                UserName = "Bob Smith"
            },
            new CommentEntity
            {
                ProductId = 3,
                Rate = 1,
                Commentary = "To weak expect more",
                UserId = 88421113,
                UserName = "Bob Smith"
            },
            new CommentEntity
            {
                ProductId = 4,
                Rate = 2,
                Commentary = "I will not recomend this for my friends",
                UserId = 88421113,
                UserName = "Bob Smith"
            }
            };
        }
    }
}
