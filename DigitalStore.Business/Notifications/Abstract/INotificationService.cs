namespace DigitalStore.Business.Notifications.Abstract;

public interface INotificationService
{
    public Task SendEmail(string subject, string email, string content);
    public Task SendEmailDirect(string subject, string email, string content);
    public void SendQueuedEmails();
}