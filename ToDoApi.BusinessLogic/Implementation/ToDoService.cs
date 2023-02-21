﻿using ToDoApi.BusinessLogic.Interfaces;
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

        public async Task<ToDoItem> CreateItemAsync(ToDoItem item)
        {
            item.Id = Guid.NewGuid();
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
            var items = await _repository.GetAllItemsAsync();
            var item = items.FirstOrDefault(x => x.Id == itemId);

            item.IsDone = isDone;

            await _repository.UpdateAsync(item);
        }
    }
}