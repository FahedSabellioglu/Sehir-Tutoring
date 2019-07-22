using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Sehir.App_Classes
{
    public class emailSending
    {
        public string fromMail;
        public string StdMail;

        public emailSending(string fromMail, string stdMail)
        {
            this.fromMail = fromMail;
            this.StdMail = stdMail;
        }

        public void SendEmail(string code, string title,string usrMessage = "")
        {
            string message = "Sehir Tutoring:\nYour Offer with the code" + code +" and the title " + title + " from the user with the email: "
                + this.StdMail + "." + "\n" +"Please email him as soon as possible.";
            
            try
            {

                MailMessage mail = new MailMessage();
                mail.To.Add(fromMail);
                mail.From = new MailAddress("qwer1x1@gmail.com");
                mail.Subject = "Sehir Tutoring Request";
                if (usrMessage != "")
                {
                    mail.Body = message + "\n" + "The following message is from the user:\n" + usrMessage;

                }
                else
                {
                    mail.Body = message;
                }
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("qwer1x1@gmail.com", "ekokobaba1");
                smtp.Send(mail);
            }
            catch
            {

            }



        }
    }
}