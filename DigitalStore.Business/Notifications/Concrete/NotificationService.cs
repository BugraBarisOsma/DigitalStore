using System.Net.Mail;
using System.Text;
using System.Text.Json;
using DigitalStore.Business.Notifications.Abstract;
using DigitalStore.Data.UnitOfWork;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DigitalStore.Business.Notifications.Concrete;

 public class NotificationService : INotificationService
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(ConnectionFactory connectionFactory,IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
            _connectionFactory = connectionFactory;
           
        }
        
        public async Task SendEmail(string subject, string userId, string content)
        {
             var connection = _connectionFactory.CreateConnection();
             var channel = connection.CreateModel();
             var user = await _unitOfWork.GetRepository<User>().GetByFilterAsync(x=>x.Id==userId && x.isActive);
             var userEmail = user.Email;
            channel.QueueDeclare(queue: "email_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var emailMessage = new EmailMessage
            {
                Subject = subject,
                Email = userEmail,
                Content = content
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(emailMessage));

            channel.BasicPublish(exchange: "", routingKey: "email_queue", basicProperties: null, body: body);
        }

        public void SendQueuedEmails()
        {
             var connection = _connectionFactory.CreateConnection();
             var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "email_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var emailMessage = JsonSerializer.Deserialize<EmailMessage>(message);

                if (emailMessage != null)
                {
                    SendEmailDirect(emailMessage.Subject, emailMessage.Email, emailMessage.Content);
                }

                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(queue: "email_queue", autoAck: false, consumer: consumer);
        }

        public void SendEmailDirect(string subject, string email, string content)
        {
            // Your existing SMTP email sending code
            SmtpClient mySmtpClient = new SmtpClient("my.smtp.exampleserver.net");

            mySmtpClient.UseDefaultCredentials = false;
            System.Net.NetworkCredential basicAuthenticationInfo = new
                System.Net.NetworkCredential("username", "password");
            mySmtpClient.Credentials = basicAuthenticationInfo;

            MailAddress from = new MailAddress("test@example.com", "TestFromName");
            MailAddress to = new MailAddress(email, "TestToName");
            MailMessage myMail = new System.Net.Mail.MailMessage(from, to);
            MailAddress replyTo = new MailAddress("reply@example.com");
            myMail.ReplyToList.Add(replyTo);

            myMail.Subject = subject;
            myMail.SubjectEncoding = System.Text.Encoding.UTF8;

            myMail.Body = "<b>Test Mail</b><br>using <b>HTML</b>." + content;
            myMail.BodyEncoding = System.Text.Encoding.UTF8;
            myMail.IsBodyHtml = true;

            mySmtpClient.Send(myMail);
        }

        private class EmailMessage
        {
            public string Subject { get; set; }
            public string Email { get; set; }
            public string Content { get; set; }
        }
    }