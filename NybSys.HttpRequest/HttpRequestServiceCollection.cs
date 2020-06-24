using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace NybSys.HttpRequest
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddHttpRequest(this IServiceCollection services, string baseUri)
        {
            services.AddHttpClient("AspNetCore.HttpRequest", x =>
            {
                x.BaseAddress = new System.Uri(baseUri);
            });

            services.AddSingleton<HttpRequestBuilder>();
            services.AddSingleton<IHttpRequestFactory, HttpRequestFactory>();

            return services;
        }

        public static IServiceCollection AddHttpRequest(this IServiceCollection services)
        {
            services.AddSingleton<HttpRequestBuilder>();
            services.AddSingleton<IHttpRequestFactory, HttpRequestFactory>();

            return services;
        }

        public static IServiceCollection AddHttpBaseAddress(this IServiceCollection services, string BaseAddressName, string baseUri)
        {
            services.AddHttpRequest();

            services.AddHttpClient(BaseAddressName, x =>
            {
                x.BaseAddress = new System.Uri(baseUri);
            });

            return services;
        }

        public static IServiceCollection AddHttpBaseAddress(this IServiceCollection services, Action<BaseAddressBuilder> baseAddressBuilderAction)
        {
            services.AddHttpRequest();

            BaseAddressBuilder baseAddressBuilders = new BaseAddressBuilder();

            baseAddressBuilderAction(baseAddressBuilders);

            var lstAddress = baseAddressBuilders.lstAddress
                                         .GroupBy(item => new BaseAddress() { BaseAddressName = item.BaseAddressName, BaseAddressUri = item.BaseAddressUri })
                                         .Select(item => item.FirstOrDefault()).ToList();

            foreach (BaseAddress address in lstAddress)
            {
                services.AddHttpClient(address.BaseAddressName, x =>
                {
                    x.BaseAddress = new System.Uri(address.BaseAddressUri);
                });
            }

            return services;
        }
    }
}
