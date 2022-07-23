using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Vrnz2.Infra.CrossCutting.Libraries.HttpClient
{
    public static class HttpRequestFactory
    {
        #region Get

        public static async Task<HttpResponseMessage> Get(string requestUri, TimeSpan? timeout = null)
            => await Get(requestUri, string.Empty, timeout);

        public static async Task<HttpResponseMessage> Get(string requestUri, List<CustomHttpHeader> custom_headers, TimeSpan? timeout = null)
            => await Get(requestUri, string.Empty, custom_headers, timeout);

        public static async Task<HttpResponseMessage> Get(string requestUri, string bearerToken, TimeSpan? timeout = null)
            => await Get(requestUri, bearerToken, new List<CustomHttpHeader>(), timeout);

        public static async Task<HttpResponseMessage> Get(string requestUri, string bearerToken, List<CustomHttpHeader> custom_headers, TimeSpan? timeout = null)
            => await new HttpRequestBuilder()
                        .AddMethod(HttpMethod.Get)
                        .AddRequestUri(requestUri)
                        .AddCustomHeaders(custom_headers)
                        .AddBearerToken(bearerToken)
                        .AddTimeout(timeout)
                        .SendAsync();

        #endregion

        #region Post

        public static async Task<HttpResponseMessage> Post(string requestUri, object value, TimeSpan? timeout = null)
            => await Post(requestUri, value, string.Empty, new List<CustomHttpHeader>(), timeout);

        public static async Task<HttpResponseMessage> Post(string requestUri, object value, string bearerToken, TimeSpan? timeout = null)
            => await Post(requestUri, value, bearerToken, new List<CustomHttpHeader>(), timeout);

        public static async Task<HttpResponseMessage> Post(string requestUri, object value, List<CustomHttpHeader> custom_headers, TimeSpan? timeout = null)
            => await Post(requestUri, value, string.Empty, custom_headers, timeout);

        public static async Task<HttpResponseMessage> Post(string requestUri, object value, string bearerToken, List<CustomHttpHeader> custom_headers, TimeSpan? timeout = null)
            => await new HttpRequestBuilder()
                        .AddMethod(HttpMethod.Post)
                        .AddRequestUri(requestUri)
                        .AddContent(value is string ? new StringContent(value.ToString(), Encoding.UTF8, "Application/json") : new JsonContent(value))
                        .AddCustomHeaders(custom_headers)
                        .AddBearerToken(bearerToken)
                        .AddTimeout(timeout)
                        .SendAsync();

        #endregion

        #region Put

        public static async Task<HttpResponseMessage> Put(string requestUri, object value, TimeSpan? timeout = null)
            => await Put(requestUri, value, string.Empty, new List<CustomHttpHeader>(), timeout);

        public static async Task<HttpResponseMessage> Put(string requestUri, object value, List<CustomHttpHeader> custom_headers, TimeSpan? timeout = null)
            => await Put(requestUri, value, string.Empty, custom_headers, timeout);

        public static async Task<HttpResponseMessage> Put(string requestUri, object value, string bearerToken, List<CustomHttpHeader> custom_headers, TimeSpan? timeout = null)
            => await new HttpRequestBuilder()
                        .AddMethod(HttpMethod.Put)
                        .AddRequestUri(requestUri)
                        .AddContent(new JsonContent(value))
                        .AddCustomHeaders(custom_headers)
                        .AddBearerToken(bearerToken)
                        .AddTimeout(timeout)
                        .SendAsync();

        #endregion

        #region Patch

        public static async Task<HttpResponseMessage> Patch(string requestUri, object value, TimeSpan? timeout = null)
            => await Patch(requestUri, value, string.Empty, timeout);

        public static async Task<HttpResponseMessage> Patch(string requestUri, object value, string bearerToken, TimeSpan? timeout = null)
        => await new HttpRequestBuilder()
                    .AddMethod(new HttpMethod("PATCH"))
                    .AddRequestUri(requestUri)
                    .AddContent(new PatchContent(value))
                    .AddBearerToken(bearerToken)
                    .AddTimeout(timeout)
                    .SendAsync();

        #endregion

        #region Delete

        public static async Task<HttpResponseMessage> Delete(string requestUri, TimeSpan? timeout = null)
            => await Delete(requestUri, string.Empty, timeout);

        public static async Task<HttpResponseMessage> Delete(string requestUri, string bearerToken, TimeSpan? timeout = null)
            => await new HttpRequestBuilder()
                        .AddMethod(HttpMethod.Delete)
                        .AddRequestUri(requestUri)
                        .AddBearerToken(bearerToken)
                        .AddTimeout(timeout)
                        .SendAsync();

        #endregion

        #region PostFile

        public static async Task<HttpResponseMessage> PostFile(string requestUri, string filePath, string acceptHeader, TimeSpan? timeout = null)
            => await PostFile(requestUri, filePath, acceptHeader, string.Empty, new List<CustomHttpHeader>(), timeout);

        public static async Task<HttpResponseMessage> PostFile(string requestUri, string filePath, string acceptHeader, List<CustomHttpHeader> custom_headers, TimeSpan? timeout = null)
            => await PostFile(requestUri, filePath, acceptHeader, string.Empty, custom_headers, timeout);

        public static async Task<HttpResponseMessage> PostFile(string requestUri, string filePath, string acceptHeader, string bearerToken, List<CustomHttpHeader> custom_headers, TimeSpan? timeout = null)
            => await new HttpRequestBuilder()
                        .AddMethod(HttpMethod.Post)
                        .AddRequestUri(requestUri)
                        .AddPostFileContent(filePath, acceptHeader)
                        .AddCustomHeaders(custom_headers)
                        .AddBearerToken(bearerToken)
                        .AddTimeout(timeout)
                        .SendAsync();

        #endregion
    }
}