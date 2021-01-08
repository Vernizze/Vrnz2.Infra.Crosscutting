namespace Vrnz2.Infra.CrossCutting.Libraries.Email
{
    internal class AppSettings 
    {
        public EMailSettings EMailSettings { get; set; }
    }

    public class EMailSettings
    {
        public string smtp_credencials_mail { get; set; }
        public string smtp_credencials_password { get; set; }
        public bool enable_ssl { get; set; }
        public string smtp_address { get; set; }
        public int smtp_port { get; set; }
        public string mail_from { get; set; }
        public string email_alias { get; set; }
    }
}
