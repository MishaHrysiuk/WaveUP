using System.Net;
using System.Net.Mail;
using System.Text;
using WaveUP.Domain.Abstract;
using WaveUP.Domain.Entities;

namespace WaveUP.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "waveupua@gmail.com";
        public string MailFromAddress = "0973853103h@gmail.com";
        public bool UseSsl = true;
        public string Username = "MySmtpUsername";
        public string Password = "MySmtpPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 25;
        public bool WriteAsFile = true;
        public string FileLocation = @"D:\waveup_emails";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials
                    = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod
                        = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("Новий заказ опрацьований")
                    .AppendLine("---")
                    .AppendLine("Товари:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Instrument.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (всьго: {2:c})\n",
                        line.Quantity, line.Instrument.Name, subtotal);
                }

                body.AppendFormat("Загальна вартість: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("\n---")
                    .AppendLine("Доставка:")
                    .AppendLine(shippingInfo.FirstName)
                    .AppendLine(shippingInfo.LastName)
                    .AppendLine(shippingInfo.Region)
                    .AppendLine(shippingInfo.Town)
                    .AppendLine(shippingInfo.Address)
                    .AppendLine(shippingInfo.HouseNumber)
                    .AppendLine(shippingInfo.ApartmentNumber ?? "")
                    .AppendLine(shippingInfo.PhoneNumber);

                MailMessage mailMessage = new MailMessage(
                                       emailSettings.MailFromAddress,	// От кого
                                       emailSettings.MailToAddress,		// Кому
                                       "Новий заказ відправлений!",		// Тема
                                       body.ToString()); 				// Тело письма

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
}
