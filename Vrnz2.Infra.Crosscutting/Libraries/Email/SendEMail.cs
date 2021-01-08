using System.Net;
using System.Net.Mail;
using System.Text;

namespace Vrnz2.Infra.CrossCutting.Libraries.Email
{
    public interface ISendEMail 
    {
        EMailSettings EMailSettings { get; }

        void Send(string to, string subject, string message);
    }

    public class SendEMail
        : ISendEMail
    {
        #region Variables

        private readonly MailMessage _mail;

        #endregion

        #region Constructors

        public SendEMail(EMailSettings eMailSettings)
        {
            EMailSettings = eMailSettings;

            _mail = new MailMessage 
            {
                From = new MailAddress(EMailSettings.mail_from, EMailSettings.email_alias),
                IsBodyHtml = true
            };
        }

        #endregion

        #region Attributes

        public EMailSettings EMailSettings { get; }

        #endregion

        #region Methods

        public void Send(string to, string subject, string message)
        {
            using (var smtp = new SmtpClient(EMailSettings.smtp_address))
            {
                _mail.To.Add(Encoding.UTF8.GetString(Encoding.Default.GetBytes(to)));                                           // Para
                _mail.Subject = Encoding.UTF8.GetString(Encoding.Default.GetBytes(subject));                                    // Assunto
                _mail.Body = message;                                                                                           // Mensagem
                smtp.EnableSsl = EMailSettings.enable_ssl;                                                                      // Requer SSL
                smtp.Port = EMailSettings.smtp_port;                                                                            // Porta para SSL
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;                                                               // Modo de envio
                smtp.UseDefaultCredentials = false;                                                                             // Utiliza credencias especificas

                // Usuário e Senha para autenticação
                smtp.Credentials = new NetworkCredential(EMailSettings.smtp_credencials_mail, this.EMailSettings.smtp_credencials_password);

                // Envia o e-mail
                smtp.Send(_mail);
            }
        }

        #endregion
    }
}
