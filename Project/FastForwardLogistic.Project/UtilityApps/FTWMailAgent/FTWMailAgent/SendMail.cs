using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace FTWMailAgent
{

    class SendMail
    {
        public void SendError(string _Err)
        {
            string[] to = "nuwanc@abansgroup.com".Split(';');
            //string[] to = "prabathw@abansgroup.com;wimal@abansgroup.com;chamald@abansgroup.com;darshana@abansgroup.com;kapila@abansgroup.com;nadeeka@abansgroup.com;amilasanjeewa@abansgroup.com;dilanda@abansgroup.com".Split(';');

            foreach (string emailAdd in to)
            {
                if (!string.IsNullOrEmpty(emailAdd))
                    Send_SMTPMail(emailAdd, _Err);
            }
        }

        void Send_SMTPMail(string _emailAdd, string _Err)
        {
            string fromAddress = "Admin";
            //string toAddress = "chamald@abansgroup.com";
            //MailMessage message = new MailMessage(fromAddress, toAddress);

            MailMessage mail = new MailMessage();
            mail.To.Add(_emailAdd);
            mail.From = new MailAddress("info@logirite.com");


            mail.Subject = "Exception message";
            mail.Body = "<b>" + _Err + "</b>";


            //Set True if your message contains Html data
            mail.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("192.168.1.203");

            //Set EnableSsl = True if SSL is required
            client.EnableSsl = false;

            //Credentials for the sender
            client.Credentials = new System.Net.NetworkCredential(fromAddress, "cha@123");

            //Send the Email
            client.Send(mail);
        }
        public bool SendEMail(string _recipientEmailAddress, string _subject, string _message, string attachment, string bcc, string excel1 = null, string excel2 = null)
        {
            string fromAddress = "Admin";
            //string toAddress = "chamald@abansgroup.com";
            //MailMessage message = new MailMessage(fromAddress, toAddress);

            MailMessage mail = new MailMessage();
            mail.To.Add(_recipientEmailAddress);
            mail.Bcc.Add("nuwanc@abansgroup.com");
            mail.From = new MailAddress("info@logirite.com");


            mail.Subject = _subject;
            mail.Body = "<b>" + _message + "</b>";
            if (attachment != "")
            {
                PdfReader reader = new PdfReader(System.IO.File.ReadAllBytes(attachment));
                Attachment data = new Attachment(attachment, MediaTypeNames.Application.Octet);
                mail.Attachments.Add(data);
            }
            if (excel1 != null && excel1 != "")
            {
                Attachment data = new Attachment(excel1, MediaTypeNames.Application.Octet);
                mail.Attachments.Add(data);
            }
            //Set True if your message contains Html data
            mail.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("192.168.1.203");

            //Set EnableSsl = True if SSL is required
            client.EnableSsl = false;

            //Credentials for the sender
            client.Credentials = new System.Net.NetworkCredential(fromAddress, "cha@123");
            try
            {
                //Send the Email
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return true;
        }
    }

}
