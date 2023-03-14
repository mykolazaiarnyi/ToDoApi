namespace ToDoApi.Infrastructure.Authentication
{
    public interface IAuthenticationService
    {
        Task<User> CreateUserAsync(CreateUserDto userDto);
        Task<User?> AuthenticateUserAsync(AuthenticateUserDto userDto);
    }
}
