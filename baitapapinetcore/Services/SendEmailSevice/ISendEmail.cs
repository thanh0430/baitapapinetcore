using baitapapinetcore.ViewModels;

namespace baitapapinetcore.Services.SendEmailSevice
{
    public interface ISendEmail
    {
        Task  SendEmail(EmailViewModel emailViewModel);
    }
}
