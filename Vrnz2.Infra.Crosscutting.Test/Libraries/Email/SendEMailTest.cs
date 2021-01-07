using Microsoft.Extensions.DependencyInjection;
using Vrnz2.Infra.CrossCutting.Libraries.Email;
using Xunit;

namespace Vrnz2.Infra.Crosscutting.Test.Libraries.Email
{
    public class SendEMailTest
    {
        [Fact]
        public void IServiceCollection_Test()
        {
            var services = new ServiceCollection();

            services.AddSendMail();

            var provider = services.BuildServiceProvider();

            var sendEmail = provider.GetService<ISendEMail>();            

            Assert.NotNull(sendEmail);
            Assert.NotNull(sendEmail.EMailSettings);
            Assert.Equal("from@internet.com", sendEmail.EMailSettings.mail_from);
        }
    }
}
