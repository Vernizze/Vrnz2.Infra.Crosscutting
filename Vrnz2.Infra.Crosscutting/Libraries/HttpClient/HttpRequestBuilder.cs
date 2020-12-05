using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Vrnz2.Infra.Crosscutting.Extensions;

namespace Vrnz2.Infra.CrossCutting.Libraries.HttpClient
{
    public class HttpRequestBuilder
    {
        #region Variables

        private HttpMethod method;
        private string requestUri = "";
        private HttpContent content;
        private string bearerToken = "";
        private string user = "";
        private string password = "";
        private string acceptHeader = "";
        private List<CustomHttpHeader> custom_headers = new List<CustomHttpHeader>();
        private TimeSpan timeout = new TimeSpan(0, 0, 15);
        private bool allowAutoRedirect;
        private List<(string, string)> _queryList = new List<(string, string)>();

        #endregion Variables

        #region Constructors

        public HttpRequestBuilder()
        {
        }

        #endregion Constructors

        #region Methods

        public HttpRequestBuilder AddMethod(HttpMethod method)
        {
            this.method = method;
            return this;
        }

        public HttpRequestBuilder AddRequestUri(string requestUri)
        {
            this.requestUri = requestUri;
            return this;
        }

        public HttpRequestBuilder AddContent(HttpContent content)
        {
            this.content = content;
            return this;
        }

        public HttpRequestBuilder AddBearerToken(string bearerToken)
        {
            this.bearerToken = bearerToken;
            return this;
        }

        public HttpRequestBuilder AddBasicAuth(string user, string password)
        {
            this.user = user;
            this.password = password;
            return this;
        }

        public HttpRequestBuilder AddAcceptHeader(string acceptHeader)
        {
            this.acceptHeader = acceptHeader;
            return this;
        }

        public HttpRequestBuilder AddCustomHeader(string Name, string Value)
        {
            custom_headers.Add(new CustomHttpHeader(Name, Value));
            return this;
        }

        public HttpRequestBuilder AddCustomHeaders(List<CustomHttpHeader> custom_headers)
        {
            this.custom_headers = custom_headers;
            return this;
        }

        public HttpRequestBuilder AddTimeout(TimeSpan? timeout)
        {
            if (timeout.IsNotNull())
                this.timeout = (TimeSpan)timeout;

            return this;
        }

        public HttpRequestBuilder AddAllowAutoRedirect(bool allowAutoRedirect)
        {
            this.allowAutoRedirect = allowAutoRedirect;
            return this;
        }

        public HttpRequestBuilder AddQuery(string Name, string Value)
        {
            _queryList.Add((Name, Value));
            return this;
        }

        public async Task<HttpResponseMessage> SendAsync(string query = "")
        {
            // Check required arguments
            EnsureArguments();

            if (_queryList.HaveAny())
            {
                query += "?";
                foreach (var item in _queryList)
                {
                    query += $"&{HttpUtility.UrlEncode(item.Item1)}={HttpUtility.UrlEncode(item.Item2)}";
                }

            }

            // Set up request
            var request = new HttpRequestMessage
            {
                Method = this.method,
                RequestUri = new Uri(this.requestUri + query)
            };

            if (this.content != null)
                request.Content = this.content;

            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(password))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{user}:{password}")));
            }

            if (!string.IsNullOrEmpty(this.bearerToken))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.bearerToken);

            if (custom_headers.HaveAny())
                custom_headers.ForEach(h =>
                {
                    request.Headers.Add(h.name, h.value);
                });

            request.Headers.Accept.Clear();
            if (!string.IsNullOrEmpty(acceptHeader))
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(this.acceptHeader));

            // Setup client
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = allowAutoRedirect
            };

            var client = new System.Net.Http.HttpClient(handler)
            {
                Timeout = timeout
            };

            return await client.SendAsync(request).ConfigureAwait(false);
        }

        //Private
        private void EnsureArguments()
        {
            if (this.method == null)
                throw new ArgumentNullException("Method");

            if (string.IsNullOrEmpty(this.requestUri))
                throw new ArgumentNullException("Request Uri");
        }

        #endregion Methods
    }
}