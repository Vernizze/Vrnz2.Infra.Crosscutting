namespace Vrnz2.Infra.CrossCutting.Libraries.HttpClient
{
    public class CustomHttpHeader
    {
        public CustomHttpHeader()
        {

        }
        public CustomHttpHeader(string Name, string Value)
        {
            name = Name;
            value = Value;
        }

        public string name { get; set; }
        public string value { get; set; }
    }
}