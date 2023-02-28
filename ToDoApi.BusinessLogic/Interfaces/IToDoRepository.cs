using ToDoApi.Domain;

namespace ToDoApi.BusinessLogic.Interfaces
{
    public interface IToDoRepository
    {
        Task AddAsync(ToDoItem item);
        Task<ToDoItem> GetByIdAsync(Guid Id);
        Task<IEnumerable<ToDoItem>> GetAllItemsAsync();
        Task<IEnumerable<ToDoItem>> GetAllDoneItemsAsync();
        Task UpdateAsync(ToDoItem item);
    }
}
