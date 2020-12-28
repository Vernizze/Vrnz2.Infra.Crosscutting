using System.Net;

namespace Vrnz2.Infra.CrossCutting.Extensions
{
    public static class HttpExtensions
    {
        public static bool IsSuccessHttpStatusCode(this int? value)
        {
            if (value.IsNotNull())
                return value.Value.IsSuccessHttpStatusCode();
            else
                return false;
        }

        public static bool IsSuccessHttpStatusCode(this int value) 
            => value >= 200 && value < 300;

        public static bool IsSuccessHttpStatusCode(this HttpStatusCode? value)
        {
            if (value.IsNotNull())
                return value.Value.IsSuccessHttpStatusCode();
            else
                return false;
        }

        public static bool IsSuccessHttpStatusCode(this HttpStatusCode value)
            => (int)value >= 200 && (int)value < 300;
    }
}
