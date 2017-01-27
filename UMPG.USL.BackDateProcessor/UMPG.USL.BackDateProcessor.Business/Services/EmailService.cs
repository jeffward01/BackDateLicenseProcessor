using NLog;
using NLog.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace UMPG.USL.BackDateProcessor.Business.Services
{
    public class EmailService : IEmailService
    {
        private static readonly IConfigurationManager _configurationManager = new ConfigurationManager();

        private readonly TimeSpan _timeSpanInterval =
            TimeSpan.FromSeconds(Convert.ToDouble(_configurationManager.AppSettings["pollInterval"]));

        private static readonly string _createFailedSubject =
            _configurationManager.AppSettings["sendCreateSnapshotNotificationEmailSubject"];

        private static readonly string _deleteFailedSubject =
            _configurationManager.AppSettings["sendDeleteSnapshotNotificationEmailSubject"];

        private static readonly string _createFailedBody = _configurationManager.AppSettings["sendCreateNotificationEmailBody"];
        private static readonly string _deleteFailedBody = _configurationManager.AppSettings["sendDeleteNotificationEmailBody"];
        private static readonly string _fromEmailAddress = _configurationManager.AppSettings["fromEmail"];
        private static readonly string _toEmailAddresses = _configurationManager.AppSettings["sendNotificationEmail"];
        private static readonly int _retryCount = Convert.ToInt32(_configurationManager.AppSettings["mailSendRetry"]);
        private static int _timeout = Convert.ToInt32(_configurationManager.AppSettings["timeOut"]); //10000 ms
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static string _createStringFailed = "";
        private static string _deleteStringFailed = "";

        public EmailService(int licenseId)
        {
            _createStringFailed = SetBodyMessage(licenseId, _createFailedBody);
            _deleteStringFailed = SetBodyMessage(licenseId, _deleteFailedBody);
        }

        public void SendDeleteFailedEmail()
        {
            //Send Notification Email here
            var emailAddressList = GetEmailAddresses(_toEmailAddresses);

            //Send Notification Email here
            var retryCounter = 0;
            var _sendTimeout = 6000;
            while (retryCounter <= _retryCount)
            {
                retryCounter++;
                var mailSender = new SmtpClient()
                {
                    Timeout = _sendTimeout
                };

                foreach (var email in emailAddressList)
                {
                    //make this dynamically
                    var to = new MailAddress(email);
                    var mailContent = new MailMessage()
                    {
                        BodyEncoding = Encoding.Default,
                        Subject = _deleteFailedSubject,
                        From = new MailAddress(_fromEmailAddress, _fromEmailAddress),
                        Body = _deleteStringFailed,
                        IsBodyHtml = true,
                        To = { to }
                    };

                    mailSender.Send(mailContent);

                    logger.Debug(string.Format("Email Message for Failed Deletetion of Snapshot sent"));
                    break;
                }

              
            }
        }

        public void SendCreateFailedEmail()
        {
            //Send Notification Email here
            var emailAddressList = GetEmailAddresses(_toEmailAddresses);
            var retryCounter = 0;
            var _sendTimeout = 6000;
            while (retryCounter <= _retryCount)
            {
                retryCounter++;
                var mailSender = new SmtpClient()
                {
                    Timeout = _sendTimeout
                };


                foreach (var emailAddress in emailAddressList)
                {
                    //make this dynamically
                    var to = new MailAddress(emailAddress.Trim());
                    var mailContent = new MailMessage()
                    {
                        BodyEncoding = Encoding.Default,
                        Subject = _createFailedSubject,
                        From = new MailAddress(_fromEmailAddress, _fromEmailAddress),
                        Body = _createStringFailed,
                        IsBodyHtml = true,
                        To = { to }
                    };
                    mailSender.Send(mailContent);
                }



  

                logger.Debug(string.Format("Email Message for Failed Creation of Snapshot sent"));
                break;
            }
        }

        private List<string> GetEmailAddresses(string emailAddresses)
        {
            return emailAddresses.Split(';').ToList();
        }

        private static string SetBodyMessage(int licenseId, string myString)
        {
            var marchineName = System.Environment.MachineName;
            return string.Format(myString, licenseId.ToString(),marchineName);
        }
    }
}