using Microsoft.EntityFrameworkCore;
using ToDoApi.BusinessLogic.Implementation;
using ToDoApi.Domain;
using ToDoApi.Infrastructure.Data;

namespace ToDoApi.API.Helpers
{
    public static class SeedHelper
    {
        public static async Task SeedUserAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ToDoContext>();
            
            await context.Database.EnsureCreatedAsync();
            var userId = UserService.UserId;
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id.Equals(userId));
            if (user is null)
            {
                user = new User
                {
                    Id = userId,
                    Name = "John Doe",
                    ToDoItems = new List<ToDoItem>()
                };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
