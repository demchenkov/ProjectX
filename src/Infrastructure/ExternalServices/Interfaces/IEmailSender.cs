namespace Infrastructure.ExternalServices.Interfaces
{
    public interface IEmailSender
    {
        void Send(string message, string emailSender, string emailRecipient);
    }
}