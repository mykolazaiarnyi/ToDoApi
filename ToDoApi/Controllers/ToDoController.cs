using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.BusinessLogic.Interfaces;
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
        public async Task<ActionResult<ToDoItem>> CreateItemAsync(ToDoItem item)
        {
            var createdItem = await _service.CreateItemAsync(item);
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
    }
}
