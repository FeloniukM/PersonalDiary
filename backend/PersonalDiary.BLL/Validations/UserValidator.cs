using FluentValidation;
using PersonalDiary.DAL.Entities;

namespace PersonalDiary.BLL.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {

        }
    }
}
