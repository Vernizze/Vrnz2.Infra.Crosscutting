using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.IO;

namespace Vrnz2.Infra.CrossCutting.Libraries.Email
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddSendMail(this IServiceCollection services) 
        {
            var appSettings = JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText("appsettings.json"));

            return services
                .AddSingleton(_ => appSettings.EMailSettings)
                .AddScoped<ISendEMail, SendEMail>();
        }
    }
}
