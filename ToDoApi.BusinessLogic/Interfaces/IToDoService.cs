using ToDoApi.Domain;

namespace ToDoApi.BusinessLogic.Interfaces
{
    public interface IToDoService
    {
        Task<ToDoItem> CreateItemAsync(ToDoItem item);
        Task<IEnumerable<ToDoItem>> GetAllItemsAsync();
        Task<IEnumerable<ToDoItem>> GetAllDoneItemsAsync();
    }
}
