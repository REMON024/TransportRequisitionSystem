using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace NybSys.RedisSession.DAL
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddRedisSession(this IServiceCollection services, Action<RedisCacheOptions> setupAction)
        {
            services.AddDistributedRedisCache(setupAction);

            return services;
        }

        public static IServiceCollection AddInMemorySession(this IServiceCollection services, Action<MemoryDistributedCacheOptions> setupAction)
        {
            services.AddDistributedMemoryCache(setupAction);

            return services;
        }
    }

}
