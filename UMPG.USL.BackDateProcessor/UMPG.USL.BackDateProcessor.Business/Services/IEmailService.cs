namespace UMPG.USL.BackDateProcessor.Business.Services
{
    public interface IEmailService
    {
        void SendDeleteFailedEmail();

        void SendCreateFailedEmail();
    }
}