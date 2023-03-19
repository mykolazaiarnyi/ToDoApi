using FluentValidation.TestHelper;
using ToDoApi.API.Validators;
using ToDoApi.Infrastructure.Authentication;

namespace ToDoApi.API.Tests.Validators
{
    public class CreateUserDtoValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("123")]
        [InlineData("abc")]
        [InlineData("...")]
        [InlineData("___")]
        [InlineData("a.1")]
        [InlineData("abcdefgh._1234567890")]
        public void UserNameIsGiven_UserNameIsValid(string userName)
        {
            var validator = new CreateUserDtoValidator();
            var userDto = new CreateUserDto
            {
                Name = userName
            };

            var result = validator.TestValidate(userDto);

            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("12")]
        [InlineData("abc abc")]
        [InlineData("abc&abc")]
        [InlineData("abc/abc")]
        [InlineData("abc\\abc")]
        [InlineData("abc'abc")]
        [InlineData("123456789012345678901")]
        public void UserNameIsGiven_UserNameIsInvalid(string userName)
        {
            var validator = new CreateUserDtoValidator();
            var userDto = new CreateUserDto
            {
                Name = userName
            };

            var result = validator.TestValidate(userDto);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
    }
}
