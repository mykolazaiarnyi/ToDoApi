using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ToDoApi.BusinessLogic.Interfaces;

namespace ToDoApi.Infrastructure.Authentication
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid CurrentUserId
        {
            get
            {
                var id = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return Guid.Parse(id);
            }
        }
    }
}
