using FluentValidation;
using PersonalDiary.Common.DTO.User;

namespace PersonalDiary.BLL.Validations
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterDTO>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is invalid");

            RuleFor(x => x.Password)
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters");

            RuleFor(x => x.Nickname)
                .MinimumLength(2).WithMessage("Username must be at least 2 characters")
                .MaximumLength(30).WithMessage("Username must not exceed 30 characters");
        }
    }
}
