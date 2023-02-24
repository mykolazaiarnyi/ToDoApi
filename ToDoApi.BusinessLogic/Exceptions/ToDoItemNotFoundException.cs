using System.Net;

namespace ToDoApi.BusinessLogic.Exceptions
{
    public class ToDoItemNotFoundException : ToDoException
    {
        public ToDoItemNotFoundException() : base("ToDoItem with given Id is not found", HttpStatusCode.NotFound)
        {
        }
    }
}
