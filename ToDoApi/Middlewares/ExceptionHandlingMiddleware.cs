using ToDoApi.BusinessLogic.Exceptions;

namespace ToDoApi.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ToDoException ex)
            {
                context.Response.StatusCode = (int)ex.StatusCode;
                await context.Response.WriteAsJsonAsync(new { Message = ex.Message });
            }
            catch (Exception ex) 
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new { Message = "An error has occurred" });
            }
        }
    }
}
