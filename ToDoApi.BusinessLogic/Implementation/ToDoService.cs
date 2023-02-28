using ToDoApi.BusinessLogic.Exceptions;
using ToDoApi.BusinessLogic.Interfaces;
using ToDoApi.BusinessLogic.Models;
using ToDoApi.Domain;

namespace ToDoApi.BusinessLogic.Implementation
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _repository;

        public ToDoService(IToDoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ToDoItem> CreateItemAsync(CreateItemDto itemDto)
        {
            var item = new ToDoItem
            {
                Id = Guid.NewGuid(),
                Description = itemDto.Description,
                IsDone = false
            };
            await _repository.AddAsync(item);
            return item;
        }

        public async Task<IEnumerable<ToDoItem>> GetAllDoneItemsAsync()
        {
            return await _repository.GetAllDoneItemsAsync();
        }

        public async Task<IEnumerable<ToDoItem>> GetAllItemsAsync()
        {
            return await _repository.GetAllItemsAsync();
        }

        public async Task ChangeStatusAsync(Guid itemId, bool isDone)
        {
            var item = await _repository.GetByIdAsync(itemId);

            item.IsDone = isDone;

            await _repository.UpdateAsync(item);
        }

        public async Task UpdateItemAsync(UpdateItemDto itemDto)
        {
            var item = await _repository.GetByIdAsync(itemDto.Id);

            if (item is null)
            {
                throw new ToDoItemNotFoundException();
            }

            item.IsDone = itemDto.IsDone;
            item.Description = itemDto.Description;

            await _repository.UpdateAsync(item);
        }
    }
}
