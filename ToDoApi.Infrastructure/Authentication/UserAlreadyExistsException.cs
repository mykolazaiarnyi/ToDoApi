using System.Net;
using ToDoApi.BusinessLogic.Exceptions;

namespace ToDoApi.Infrastructure.Authentication
{
    public class UserAlreadyExistsException : ToDoException
    {
        public UserAlreadyExistsException() : base("User with given name already exists", HttpStatusCode.BadRequest)
        {
        }
    }
}
