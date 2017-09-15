using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Net;
using System.Net.Mail;

namespace PoolLibrary
{
    public class Utils
    {
        public static void SendEmail1(String psToEmailAddress, String psSubject, String psBody)
        {
            var fromAddress = new MailAddress("phu.cao.tvl1@gmail.com", "Phu Cao");
            var toAddress = new MailAddress(psToEmailAddress, "Customer");
            const string fromPassword = "ThanhPhu04";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = psSubject,
                Body = psBody
            })
            {
                smtp.Send(message);
            }
        }
        public static void SendEmail(String psToEmailAddress, String psSubject, String psBody)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("phu.cao.tvl1@gmail.com");
            mail.To.Add(psToEmailAddress);
            mail.Subject = psSubject;
            mail.Body = psBody;

            //System.Net.Mail.Attachment attachment;
            //attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            //mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("phu.cao.tvl1@gmail.com", "ThanhPhu04");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        public static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(filePath, append);
                serializer.Serialize(writer, objectToWrite);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        public static T ReadFromXmlFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                reader = new StreamReader(filePath);
                return (T)serializer.Deserialize(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
    }
}
