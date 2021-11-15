using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Net.Mail;
using System.Reflection;
using System.Net.Mime;
using iTextSharp.text.pdf;


namespace Hero_Service_Consol
{
    //Added by akila 2016/12/08
    public class JobAgentEmail
    {
        public string EmailSender { get; set; }
        public string RecipientName { get; set; }
        public string RecipientAddress { get; set; }
        public string CcRecipient { get; set; }
        public string BccRecipient { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string AttachmentPath { get; set; }
        public string EmailDisplay { get; set; }
        public string EmailHost { get; set; }
        public string EmailFooter { get; set; }
        public string ContactNo { get; set; }

        public string ErrorEmailTo { get; set; }

        public JobAgentEmail(DataTable _emailAddresses)
        {
            if (_emailAddresses.Rows.Count > 0)
            {
                GetEmailSettings();

                RecipientName = _emailAddresses.Rows[0]["UserName"] == DBNull.Value ? string.Empty : _emailAddresses.Rows[0]["UserName"].ToString();
                RecipientAddress = _emailAddresses.Rows[0]["EmailAdd"] == DBNull.Value ? string.Empty : _emailAddresses.Rows[0]["EmailAdd"].ToString();
                string _attachmentName = Path.GetFileName(AttachmentPath);
                EmailBody = PopulateBody(EmailDisplay, RecipientName, _attachmentName);
            }            
        }

        public JobAgentEmail()
        {
            GetEmailSettings();
        }

        private void GetEmailSettings()
        {
            NameValueCollection _emailSettings = new NameValueCollection();
            try
            {
                _emailSettings = (NameValueCollection)ConfigurationManager.GetSection("EmailSettings");
                if (_emailSettings.Keys.Count > 0)
                {
                    EmailSender = _emailSettings["MailAddress"].ToString();
                    EmailDisplay = _emailSettings["MailDisplay"].ToString();
                    EmailHost = _emailSettings["MailHost"].ToString();
                    EmailFooter = _emailSettings["MailFooter"].ToString();
                    ContactNo = _emailSettings["HPContPhoneNo"].ToString();
                    AttachmentPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Reports\\CanceledTransferRequest.pdf");
                   // AttachmentPath = _emailSettings["AttachmentPath"].ToString();
                    EmailSubject = _emailSettings["EmailSubject"].ToString();
                    ErrorEmailTo = _emailSettings["ErrorEmailTo"];
                }
                else
                {
                    throw new Exception("Cannot send email. Email settings not found in App.config file (<EmailSettings>, section name=EmailSettings type=System.Configuration.AppSettingsSection, System.Configuration, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string PopulateBody(string _emailDisplai, string _recipientName, string _reportName)
        {
            string body = string.Empty;

            try
            {
                using (StreamReader reader = new StreamReader(Application.StartupPath + @"\template.html"))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("[Header]", _emailDisplai);
                body = body.Replace("[Receiver]", _recipientName);
                body = body.Replace("[ReportName]", _reportName);
            }
            catch (Exception)
            {                
                throw;
            }
            
            return body;
        }

        public int SendEMail( string _recipientAddress, string _bccAddress, string _ccAddress, string _emailBody, ref string _errorMessage)
        {
            int result = 0;

            try
            {
                if (!string.IsNullOrEmpty(_emailBody))
                {
                    //MailMessage message = new MailMessage();
                    //message.To.Add(_recipientAddress);
                    //message.From = new MailAddress(EmailSender);

                    //message.Subject = EmailSubject;
                    //message.Body = _emailBody;
                    //message.IsBodyHtml = true;

                    //if (!string.IsNullOrEmpty(_bccAddress))
                    //{
                    //    message.Bcc.Add(_bccAddress);
                    //}

                    //if (!string.IsNullOrEmpty(AttachmentPath))
                    //{
                    //    PdfReader reader = new PdfReader(System.IO.File.ReadAllBytes(AttachmentPath));
                    //    Attachment data = new Attachment(AttachmentPath, MediaTypeNames.Application.Octet);
                    //    message.Attachments.Add(data);

                    //    //message.Attachments.Add(new Attachment(AttachmentPath, MediaTypeNames.Application.Octet));
                    //    //message.Attachments.Add(new Attachment(AttachmentPath));
                    //}

                    //SmtpClient smtpClient = new SmtpClient(EmailHost);
                    //smtpClient.EnableSsl = false;

                    //string _fromAddress = "Admin";

                    //smtpClient.Credentials = new System.Net.NetworkCredential(_fromAddress, "cha@123");
                    //smtpClient.EnableSsl = true;

                    //message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                    //smtpClient.Send(message);
                    //string fromAddress = "Admin";
                    //string toAddress = "chamald@abansgroup.com";
                    //MailMessage message = new MailMessage(fromAddress, toAddress);

                    MailMessage mail = new MailMessage();
                    mail.Bcc.Add(_recipientAddress);
                    mail.From = new MailAddress(EmailSender);


                    mail.Subject = EmailSubject;
                    mail.Body = _emailBody;
                    mail.IsBodyHtml = true;

                    if (!string.IsNullOrEmpty(AttachmentPath))
                    {
                        PdfReader reader = new PdfReader(System.IO.File.ReadAllBytes(AttachmentPath));
                        Attachment data = new Attachment(AttachmentPath, MediaTypeNames.Application.Octet);
                        mail.Attachments.Add(data);

                        //message.Attachments.Add(new Attachment(AttachmentPath, MediaTypeNames.Application.Octet));
                        //message.Attachments.Add(new Attachment(AttachmentPath));
                    }


                    //PdfReader reader = new PdfReader(System.IO.File.ReadAllBytes(AttachmentPath));
                    //Attachment data = new Attachment(AttachmentPath, MediaTypeNames.Application.Octet);
                    //mail.Attachments.Add(data);

                    ////Set True if your message contains Html data
                    //mail.IsBodyHtml = true;

                    string fromAddress = "Admin";
                    SmtpClient client = new SmtpClient("192.168.1.204");

                    //Set EnableSsl = True if SSL is required
                    client.EnableSsl = false;

                    //Credentials for the sender
                    client.Credentials = new System.Net.NetworkCredential(fromAddress, "cha@123");
                    //try
                    //{
                        //Send the Email
                        client.Send(mail);
                    //}
                    //catch (Exception ex)
                    //{
                    //    Console.WriteLine(ex.Message.ToString());
                    //}
                    //result = 1;
                }
            }
            catch (Exception ex)
            {
                result = 0;
                _errorMessage = "Error occurred while sending the email. " + ex.Message + Environment.NewLine + ex.Source;
            }

            return result;
        }

        public void SendErrorEmail(string _errorrMsg)
        {
            EmailSubject = "Herso Service Console - Job Error";
            EmailBody = _errorrMsg;
            string _message = null;
            SendEMail(ErrorEmailTo, null, null, EmailBody, ref _message);
            if (!string.IsNullOrEmpty(_message))
            {
                Console.WriteLine(_message);
            }
        }
    }
}
