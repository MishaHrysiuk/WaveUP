using System.Net;
using System.Net.Mail;
using System.Text;
using WaveUP.Domain.Abstract;
using WaveUP.Domain.Entities;

namespace WaveUP.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailAddress = "waveupua@gmail.com";
        public bool UseSsl = true;
        public string Username = "waveupua@gmail.com";
        public string Password = "waveup2812";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        //change here for local save
        public string FileLocation = @"E:\mails";
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
            using (SmtpClient smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials
                    = new NetworkCredential(emailSettings.Username, emailSettings.Password);

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
                    .AppendLine("\nEMAIL_OVER_HERE\n")
                    .AppendLine(shippingInfo.email)
                    .AppendLine("\nEMAIL_OVER_HERE\n")
                    .AppendLine(shippingInfo.PhoneNumber.Replace(" ", string.Empty));

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(emailSettings.MailAddress);
                mailMessage.To.Add(new MailAddress(emailSettings.MailAddress));
                //mailMessage.Subject = "TEST";
                mailMessage.Body = body.ToString();

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod
                       = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
}
