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

        public static String AlgoToString(int piAlgo)
        {
            String result = "";
            switch (piAlgo)
            {
                case 0:
                    result = "Scrypt";
                    break;
                case 1:
                    result = "SHA256";
                    break;
                case 2:
                    result = "ScryptNf";
                    break;
                case 3:
                    result = "X11";
                    break;
                case 4:
                    result = "X13";
                    break;
                case 5:
                    result = "Keccak";
                    break;
                case 6:
                    result = "X15";
                    break;
                case 7:
                    result = "Nist5";
                    break;
                case 8:
                    result = "NeoScrypt";
                    break;
                case 9:
                    result = "Lyra2RE";
                    break;
                case 10:
                    result = "WhirlpoolX";
                    break;
                case 11:
                    result = "Qubit";
                    break;
                case 12:
                    result = "Quark";
                    break;
                case 13:
                    result = "Axiom";
                    break;
                case 14:
                    result = "Lyra2REv2";
                    break;
                case 15:
                    result = "ScryptJaneNf16";
                    break;
                case 16:
                    result = "Blake256r8";
                    break;
                case 17:
                    result = "Blake256r14";
                    break;
                case 18:
                    result = "Blake256r8vnl";
                    break;
                case 19:
                    result = "Hodl";
                    break;
                case 20:
                    result = "DaggerHashimoto";
                    break;
                case 21:
                    result = "Decred";
                    break;
                case 22:
                    result = "CryptoNight";
                    break;
                case 23:
                    result = "Lbry";
                    break;
                case 24:
                    result = "Equihash";
                    break;
                case 25:
                    result = "Pascal";
                    break;
                case 26:
                    result = "X11Gost";
                    break;
                case 27:
                    result = "Sia";
                    break;
                case 28:
                    result = "Blake2s";
                    break;
                case 29:
                    result = "Skunk";
                    break;
            }
            return result;
        }

    }
}
