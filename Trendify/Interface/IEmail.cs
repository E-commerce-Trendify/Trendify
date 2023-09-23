namespace Trendify.Interface
{
    public interface IEmail
    {
        Task SendEmail(string email,string subject,string htmlMessage);
    }
}
