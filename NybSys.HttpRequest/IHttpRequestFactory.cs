using System.Net.Http;
using System.Threading.Tasks;

namespace NybSys.HttpRequest
{
    public interface IHttpRequestFactory
    {
        Task<HttpResponseMessage> Get(string requestUri);
        Task<HttpResponseMessage> Get(string requestUri, string bearerToken);

        Task<HttpResponseMessage> Post(string requestUri, object data);
        Task<HttpResponseMessage> Post(string requestUri, object data, string bearerToken);

        Task<HttpResponseMessage> Put(string requestUri, object data);
        Task<HttpResponseMessage> Put(string requestUri, object data, string bearerToken);

        Task<HttpResponseMessage> Patch(string requestUri, object data);
        Task<HttpResponseMessage> Patch(string requestUri, object data, string bearerToken);

        Task<HttpResponseMessage> Delete(string requestUri);
        Task<HttpResponseMessage> Delete(string requestUri, string bearerToken);

        HttpRequestFactory BaseAddress(string BaseAddressName);
    }
}
