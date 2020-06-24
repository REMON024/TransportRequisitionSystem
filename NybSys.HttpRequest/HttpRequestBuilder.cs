using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace NybSys.HttpRequest
{
    public class HttpRequestBuilder
    {
        private HttpClient _httpClient;

        private readonly IHttpClientFactory _httpClientFactory;
        private HttpMethod method = null;
        private string requestUri = string.Empty;
        private HttpContent content = null;
        private string bearerToken = string.Empty;
        private string acceptHeader = "application/json";
        private TimeSpan timeout = TimeSpan.FromSeconds(30);

        public HttpRequestBuilder(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("AspNetCore.HttpRequest");
            _httpClient.Timeout = this.timeout;
        }

        public HttpRequestBuilder SetBaseAddress(string BaseAddressName)
        {
            _httpClient = _httpClientFactory.CreateClient(BaseAddressName);
            return this;
        }

        public HttpRequestBuilder AddMethod(HttpMethod httpMethod)
        {
            this.method = httpMethod;
            return this;
        }

        public HttpRequestBuilder AddRequestUri(string requestUri)
        {
            this.requestUri = requestUri;
            return this;
        }

        public HttpRequestBuilder AddContent(HttpContent httpContent)
        {
            this.content = httpContent;
            return this;
        }

        public HttpRequestBuilder AddAcceptHeader(string header)
        {
            this.acceptHeader = header;
            return this;
        }

        public HttpRequestBuilder AddBearerToken(string bearerToken)
        {
            this.bearerToken = bearerToken;
            return this;
        }

        public HttpRequestBuilder AddTimeout(TimeSpan timeout)
        {
            this.timeout = timeout;
            return this;
        }

        public async Task<HttpResponseMessage> SendAsync()
        {
            // Check required arguments
            EnsureArguments();

            // Set up request
            var request = new HttpRequestMessage
            {
                Method = this.method,
                RequestUri = new Uri(_httpClient.BaseAddress, this.requestUri)
            };

            if (this.content != null)
                request.Content = this.content;

            if (!string.IsNullOrEmpty(this.bearerToken))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.bearerToken);

            request.Headers.Accept.Clear();
            if (!string.IsNullOrEmpty(this.acceptHeader))
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(this.acceptHeader));

            return await _httpClient.SendAsync(request, CancellationToken.None);
        }

        #region " Private "

        private void EnsureArguments()
        {
            if (this.method == null)
                throw new ArgumentNullException("Method");

            if (string.IsNullOrEmpty(this.requestUri))
                throw new ArgumentNullException("Request Uri");
        }

        #endregion
    }
}
