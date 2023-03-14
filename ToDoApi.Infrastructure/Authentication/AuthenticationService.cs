using Microsoft.EntityFrameworkCore;
using ToDoApi.Infrastructure.Data;

namespace ToDoApi.Infrastructure.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ToDoContext _context;

        public AuthenticationService(ToDoContext context)
        {
            _context = context;
        }

        public async Task<User?> AuthenticateUserAsync(AuthenticateUserDto userDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == userDto.Name);
            if (user is null 
                || user.Password != userDto.Password)
            {
                return null;
            }

            return new User
            {
                Id = user.Id,
                Name = user.Name,
            };
        }

        public async Task<User> CreateUserAsync(CreateUserDto userDto)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Name == userDto.Name);
            if (existingUser != null) 
            {
                throw new UserAlreadyExistsException();
            }

            var user = new Domain.User 
            { 
                Id = Guid.NewGuid(),
                Name = userDto.Name, 
                Password = userDto.Password 
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            
            return new User
            {
                Id = user.Id,
                Name = userDto.Name
            };
        }
    }
}
