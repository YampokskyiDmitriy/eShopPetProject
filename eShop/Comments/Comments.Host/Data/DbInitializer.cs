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
                Commentary = "Test 1, Test 1, Test 1, Test 1, Test 1, Test 1",
                UserId = 88421113,
                UserName = "Bob Smith"
            },
            new CommentEntity
            {
                ProductId = 2,
                Rate = 5,
                Commentary = "Test 2, Test 2, Test 2, Test 2, Test 2, Test 2",
                UserId = 88421113,
                UserName = "Bob Smith"
            },
            new CommentEntity
            {
                ProductId = 3,
                Rate = 1,
                Commentary = "Test 3, Test 3, Test 3, Test 3, Test 3, Test 3",
                UserId = 88421113,
                UserName = "Bob Smith"
            },
            new CommentEntity
            {
                ProductId = 4,
                Rate = 2,
                Commentary = "Test 4, Test 4, Test 4, Test 4, Test 4, Test 4",
                UserId = 88421113,
                UserName = "Bob Smith"
            }
            };
        }
    }
}
