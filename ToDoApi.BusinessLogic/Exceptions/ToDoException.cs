using System.Net;

namespace ToDoApi.BusinessLogic.Exceptions
{
    public class ToDoException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public ToDoException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
