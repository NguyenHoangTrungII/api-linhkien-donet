using _01_WEBAPI.Helper.ApiResults;
using linhkien_donet.Models.EmailModels;

namespace linhkien_donet.Interfaces
{
    public interface IEmailService
    {
        Task<ApiResult<bool>> SendMail(EmailContent mailContent);

        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
