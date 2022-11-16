using PersonalDiary.Common.Email;

namespace PersonalDiary.BLL.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest, string? template);
    }
}
