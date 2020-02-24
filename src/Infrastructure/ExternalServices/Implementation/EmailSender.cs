using Infrastructure.ExternalServices.Interfaces;

namespace Infrastructure.ExternalServices
{
    public class EmailSender : IEmailSender
    {
        // todo: add here some third party service that will send email

        public void Send(string message, string emailSender, string emailRecipient)
        {
            throw new System.NotImplementedException();
        }
    }
}