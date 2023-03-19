using FluentValidation;
using System.Text.RegularExpressions;
using ToDoApi.Infrastructure.Authentication;

namespace ToDoApi.API.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        private static readonly Regex _userNameRegex = new Regex(@"^[\w\.]{3,20}$");

        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(x => _userNameRegex.IsMatch(x));
        }
    }
}
