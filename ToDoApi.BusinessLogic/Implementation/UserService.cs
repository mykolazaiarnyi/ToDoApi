using ToDoApi.BusinessLogic.Interfaces;

namespace ToDoApi.BusinessLogic.Implementation
{
    public class UserService : IUserService
    {
        public static readonly Guid UserId = new("8360e917-5e1d-44c7-a449-e115b69280bb");

        public Guid CurrentUserId => UserId;
    }
}
