using ToDoApi.BusinessLogic.Models;
using ToDoApi.Domain;

namespace ToDoApi.BusinessLogic.Interfaces
{
    public interface IToDoService
    {
        Task<ToDoItem> CreateItemAsync(CreateItemDto itemDto);
        Task<IEnumerable<ToDoItem>> GetAllItemsAsync();
        Task<IEnumerable<ToDoItem>> GetAllDoneItemsAsync();
        Task ChangeStatusAsync(Guid itemId, bool isDone);
        Task UpdateItemAsync(UpdateItemDto itemDto);
    }
}
