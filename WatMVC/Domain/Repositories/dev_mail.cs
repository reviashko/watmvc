using System;
using System.IO;
using System.Configuration;
using System.Data;
using System.Web.Mail;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Domain
{
    public class dev_mail
    {
        IDataBase db;

        public dev_mail(IDataBase db)
        {
            this.db = db;
        }

        public bool SendMessage(string srcMail, string destMail, string srcText, string subject, bool isHtml)
        {
            return SendMessage(srcMail, destMail, srcText, subject, isHtml, "");
        }

        public bool SendMessage(string srcMail, string destMail, string srcText, string subject, bool isHtml, string filename)
        {
            if (destMail.Length > 0)
            {
                MailMessage m = new MailMessage();
                m.From = srcMail;
                m.To = destMail;
                m.Subject = subject;
                m.Body = srcText;
                m.BodyFormat = isHtml ? MailFormat.Html : MailFormat.Text;
                m.BodyEncoding = Encoding.GetEncoding("windows-1251");

                if (filename.Length > 3 && File.Exists(filename))
                {
                    MailAttachment mailAttachment = new MailAttachment(filename);
                    m.Attachments.Add(mailAttachment);
                }

                SmtpMail.SmtpServer = "robots.1gb.ru";
                SmtpMail.Send(m);


                /*
                            System.Net.Mail.MailMessage MyMailMessage = new System.Net.Mail.MailMessage(srcMail, destMail, subject, srcText);

                            MyMailMessage.IsBodyHtml = isHtml;
                            System.Net.NetworkCredential mailAuthentication = new System.Net.NetworkCredential(srcMail, "Do67qat!");
                            System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient("smtp.yandex.ru", 587);
                            mailClient.EnableSsl = true;


                            if(filename.Length > 3 && File.Exists(filename))
                            {
                                //MailAttachment mailAttachment = new MailAttachment(filename);
                                System.Net.Mail.Attachment data = new System.Net.Mail.Attachment(filename, System.Net.Mime.MediaTypeNames.Application.Octet);
                                MyMailMessage.Attachments.Add(data);
                            }

                            mailClient.UseDefaultCredentials = false;
                            mailClient.Credentials = mailAuthentication;
                            mailClient.Send(MyMailMessage);  
                */

                return true;
            }

            return false;
        }

        public DataTable GetEmailList(int day_ago)
        {
            db.ClearParams();
            db.AddParameter(new SqlParameter("@day_ago", day_ago));
            db.SetStoredProcedure("Mail.GetEmails");
            return db.GetDataTable();
        }

        public void UpdateEmailsDate(string xml_str)
        {
            db.ClearParams();
            db.AddParameter(new SqlParameter("@Xml", xml_str));
            db.SetStoredProcedure("Adminka.UpdateEmailsDate");
            db.GetDataTable();
        }
    }
}