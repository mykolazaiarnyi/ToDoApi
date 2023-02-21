using Microsoft.AspNetCore.Mvc;
using ToDoApi.BusinessLogic.Interfaces;
using ToDoApi.BusinessLogic.Models;
using ToDoApi.Domain;

namespace ToDoApi.API.Controllers
{
    [Route("api/to-do-items")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _service;

        public ToDoController(IToDoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<ToDoItem>> CreateItemAsync(CreateItemDto itemDto)
        {
            var createdItem = await _service.CreateItemAsync(itemDto);
            return Ok(createdItem);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetAllItemsAsync()
        {
            return Ok(await _service.GetAllItemsAsync());
        }

        [HttpGet("done")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetAllDoneItemsAsync()
        {
            return Ok(await _service.GetAllDoneItemsAsync());
        }

        [HttpPut("{itemId}")]
        public async Task<ActionResult> ChangeStatusAsync(Guid itemId, bool isDone)
        {
            await _service.ChangeStatusAsync(itemId, isDone);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> ChangeStatusAsync(UpdateItemDto itemDto)
        {
            await _service.UpdateItemAsync(itemDto);
            return Ok();
        }
    }
}
