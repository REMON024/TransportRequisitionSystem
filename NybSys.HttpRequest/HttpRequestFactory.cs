using System.Net.Http;
using System.Threading.Tasks;

namespace NybSys.HttpRequest
{
    public class HttpRequestFactory : IHttpRequestFactory
    {
        private HttpRequestBuilder _httpRequetBuilder;

        public HttpRequestFactory(HttpRequestBuilder httpRequetBuilder)
        {
            _httpRequetBuilder = httpRequetBuilder;
        }

        public HttpRequestFactory BaseAddress(string BaseAddressName)
        {
            _httpRequetBuilder = _httpRequetBuilder.SetBaseAddress(BaseAddressName);
            return this;
        }

        public async Task<HttpResponseMessage> Get(string requestUri)
        {
            return await Get(requestUri, string.Empty);
        }

        public async Task<HttpResponseMessage> Get(string requestUri, string bearerToken)
        {
            var builder = _httpRequetBuilder
                            .AddMethod(HttpMethod.Get)
                            .AddRequestUri(requestUri)
                            .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }

        public async Task<HttpResponseMessage> Patch(string requestUri, object data)
        {
            return await Patch(requestUri, data, string.Empty);
        }

        public async Task<HttpResponseMessage> Patch(string requestUri, object data, string bearerToken)
        {
            var builder = _httpRequetBuilder
                            .AddMethod(new HttpMethod("PATCH"))
                            .AddRequestUri(requestUri)
                            .AddContent(new PatchContent(data))
                            .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }

        public async Task<HttpResponseMessage> Post(string requestUri, object data)
        {
            return await Post(requestUri, data, string.Empty);
        }

        public async Task<HttpResponseMessage> Post(string requestUri, object data, string bearerToken)
        {
            var builder = _httpRequetBuilder
                            .AddMethod(HttpMethod.Post)
                            .AddRequestUri(requestUri)
                            .AddContent(new JsonContent(data))
                            .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }

        public async Task<HttpResponseMessage> Delete(string requestUri)
        {
            return await Delete(requestUri, string.Empty);
        }

        public async Task<HttpResponseMessage> Delete(string requestUri, string bearerToken)
        {
            var builder = _httpRequetBuilder
                                .AddMethod(HttpMethod.Delete)
                                .AddRequestUri(requestUri)
                                .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }

        public async Task<HttpResponseMessage> Put(string requestUri, object data)
        {
            return await Put(requestUri, data, string.Empty);
        }

        public async Task<HttpResponseMessage> Put(string requestUri, object data, string bearerToken)
        {
            var builder = _httpRequetBuilder
                                .AddMethod(HttpMethod.Put)
                                .AddRequestUri(requestUri)
                                .AddContent(new JsonContent(data))
                                .AddBearerToken(bearerToken);

            return await builder.SendAsync();
        }
    }
}
