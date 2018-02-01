using System;
using System.Net;
using System.Net.Mail;

namespace shop.Helpers
{
    public class Emailer
    {

        public void SendMail(shop.Models.ContactUs contactUs, string sparkpostKey)
        {
            if (contactUs != null)
            {
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.sparkpostmail.com";
                    smtp.Port = 587;
                    //smtp.EnableSsl = true;

                    // You will need an API Key with 'Send via SMTP' permissions.
                    // Create one here: https://app.sparkpost.com/account/credentials
                    smtp.Credentials = new NetworkCredential(
                        "SMTP_Injection", "5dce1cf550fc1cbb830f1e2c55cdb4e31be97446");

                    // sparkpostbox.com is a sending domain used for testing
                    // purposes and is limited to 50 messages per account.
                    // Visit https://app.sparkpost.com/account/sending-domains
                    // to register and verify your own sending domain.
                    MailAddress from = new MailAddress(Constants.FROM_EMAIL);

                    MailAddress to = new MailAddress(Constants.TO_EMAIL);
                    MailMessage mail = new MailMessage(from, to);

                    mail.Subject = contactUs.Subject;

                    mail.Body = "NAME: " + contactUs.Name + "\n"
                        + "EMAIL: " + contactUs.Email + "\n"
                        + "NOTES: \n" + contactUs.Notes;

                    smtp.Send(mail);
                }
            }
        }
 

    }
}
