// <copyright file="MsmqModel.cs" company="Fundoo Notes Application">
//     MsmqModel copyright tag.
// </copyright>

namespace Common.Models
{
    using Experimental.System.Messaging;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Msmq Model
    /// </summary>
    public class MsmqModel
    {
        MessageQueue msmq = new MessageQueue();
        public void MsmqSender(string token)
        {
            msmq.Path = @".\private$\Token";
            if (!MessageQueue.Exists(msmq.Path))
            {
                MessageQueue.Create(msmq.Path);
            }

            msmq.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            msmq.ReceiveCompleted += MsmqReceiver;
            msmq.Send(token);
            msmq.BeginReceive();
            msmq.Close();
        }

        /// <summary>
        /// MSMQs the receiver.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ReceiveCompletedEventArgs"/> instance containing the event data.</param>
        private void MsmqReceiver(object sender, ReceiveCompletedEventArgs e)
        {
            var message = msmq.EndReceive(e.AsyncResult);
            string token = message.Body.ToString();
            string Subject = "Fundoo Notes Application Password Reset";
            string Body = token;
            string jwt = DecodeJwt(token);


            //mail sending code

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("vickytestsmtp@gmail.com", "TestSmtp"),
                EnableSsl = true,
            };

            smtpClient.Send("vickytestsmtp@gmail.com", jwt, Subject, Body);

            //msmq receiver
            msmq.BeginReceive();
        }

        /// <summary>
        /// Decodes the JWT.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>EmailId</returns>
        public static string DecodeJwt(string token)
        {
            try
            {
                var decodeToken = token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken((decodeToken));
                var result = jsonToken.Claims.FirstOrDefault().Value;
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
