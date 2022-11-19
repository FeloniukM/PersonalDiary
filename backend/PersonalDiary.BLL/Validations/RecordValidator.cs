using FluentValidation;
using PersonalDiary.Common.DTO.Record;

namespace PersonalDiary.BLL.Validations
{
    public class RecordValidator : AbstractValidator<RecordCreateDTO>
    {
        public RecordValidator()
        {
            RuleFor(x => x.Text)
                .MaximumLength(500).WithMessage("Record text max lenght 500 characters")
                .NotEmpty().WithMessage("Record text can not be empty");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Record title can not be empty")
                .MaximumLength(100).WithMessage("Record title max lenght 100 characters");
        }
    }
}
