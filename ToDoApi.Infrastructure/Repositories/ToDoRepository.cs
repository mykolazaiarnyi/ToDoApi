using Microsoft.EntityFrameworkCore;
using ToDoApi.BusinessLogic.Interfaces;
using ToDoApi.Domain;
using ToDoApi.Infrastructure.Data;

namespace ToDoApi.Infrastructure.Repositories
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ToDoContext _context;
        private readonly IUserService _userService;

        public ToDoRepository(ToDoContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task AddAsync(ToDoItem item)
        {
            var userId = _userService.CurrentUserId;
            
            var user = await _context.Users.FirstAsync(x => x.Id.Equals(userId));
            user.ToDoItems ??= new List<ToDoItem>();
            
            user.ToDoItems.Add(item);
            
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ToDoItem>> GetAllDoneItemsAsync()
        {
            var allItems = await GetAllItemsAsync();
            return allItems.Where(x => x.IsDone).ToList();
        }

        public async Task<IEnumerable<ToDoItem>> GetAllItemsAsync()
        {
            var userId = _userService.CurrentUserId;

            var user = await _context.Users.FirstAsync(x => x.Id == userId);

            return user.ToDoItems;
        }

        public async Task UpdateAsync(ToDoItem item)
        {
            await _context.SaveChangesAsync();
        }
    }
}
