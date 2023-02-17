using Microsoft.EntityFrameworkCore;
using ToDoApi.BusinessLogic.Interfaces;
using ToDoApi.Domain;
using ToDoApi.Infrastructure.Data;

namespace ToDoApi.Infrastructure.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private static readonly Guid UserId = new Guid("8360e917-5e1d-44c7-a449-e115b69280bb");
        private readonly ToDoContext _context;

        public ToDoRepository(ToDoContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ToDoItem item)
        {
            var user = await GetUserAsync();
            user.ToDoItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ToDoItem>> GetAllDoneItemsAsync()
        {
            var user = await GetUserAsync();
            return user.ToDoItems.Where(x => x.IsDone);
        }

        public async Task<IEnumerable<ToDoItem>> GetAllItemsAsync()
        {
            var user = await GetUserAsync();
            return user.ToDoItems;
        }

        private async Task<User> GetUserAsync()
        {
            var user = await _context.Users.FirstAsync(x => x.Id.Equals(UserId));
            user.ToDoItems ??= new List<ToDoItem>();
            return user;
        }
    }
}
