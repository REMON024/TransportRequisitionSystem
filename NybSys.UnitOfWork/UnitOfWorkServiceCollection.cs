﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace NybSys.UnitOfWork
{
    /// <summary>
    /// Extension methods for setting up unit of work related services in an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtension
    {

        /// <summary>
        /// Registers the unit of work given context as a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TContext">The type of the db context.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        /// <remarks>
        /// This method only support one db context, if been called more than once, will throw exception.
        /// </remarks>
        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            services.AddUnitOfWorkRepository();
            // Following has a issue: IUnitOfWork cannot support multiple dbcontext/database, 
            // that means cannot call AddUnitOfWork<TContext> multiple times.
            // Solution: check IUnitOfWork whether or null
            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
            services.AddScoped<IUnitOfWork<TContext>, UnitOfWork<TContext>>();
            services.AddScoped<IQueryExecutor, QueryExecutor<TContext>>();
            services.AddScoped<IQueryExecutor<TContext>, QueryExecutor<TContext>>();

            return services;
        }

        /// <summary>
        /// Registers the unit of work given context as a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TContext1">The type of the db context.</typeparam>
        /// <typeparam name="TContext2">The type of the db context.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        /// <remarks>
        /// This method only support one db context, if been called more than once, will throw exception.
        /// </remarks>
        public static IServiceCollection AddUnitOfWork<TContext1, TContext2>(this IServiceCollection services)
            where TContext1 : DbContext
            where TContext2 : DbContext
        {
            services.AddUnitOfWorkRepository();

            services.AddScoped<IUnitOfWork<TContext1>, UnitOfWork<TContext1>>();
            services.AddScoped<IUnitOfWork<TContext2>, UnitOfWork<TContext2>>();

            services.AddScoped<IQueryExecutor<TContext1>, QueryExecutor<TContext1>>();
            services.AddScoped<IQueryExecutor<TContext2>, QueryExecutor<TContext2>>();

            return services;
        }

        /// <summary>
        /// Registers the unit of work given context as a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TContext1">The type of the db context.</typeparam>
        /// <typeparam name="TContext2">The type of the db context.</typeparam>
        /// <typeparam name="TContext3">The type of the db context.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        /// <remarks>
        /// This method only support one db context, if been called more than once, will throw exception.
        /// </remarks>
        public static IServiceCollection AddUnitOfWork<TContext1, TContext2, TContext3>(this IServiceCollection services)
            where TContext1 : DbContext
            where TContext2 : DbContext
            where TContext3 : DbContext
        {
            services.AddUnitOfWorkRepository();

            services.AddScoped<IUnitOfWork<TContext1>, UnitOfWork<TContext1>>();
            services.AddScoped<IUnitOfWork<TContext2>, UnitOfWork<TContext2>>();
            services.AddScoped<IUnitOfWork<TContext3>, UnitOfWork<TContext3>>();

            services.AddScoped<IQueryExecutor<TContext1>, QueryExecutor<TContext1>>();
            services.AddScoped<IQueryExecutor<TContext2>, QueryExecutor<TContext2>>();
            services.AddScoped<IQueryExecutor<TContext3>, QueryExecutor<TContext3>>();

            return services;
        }

        /// <summary>
        /// Registers the unit of work given context as a service in the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TContext1">The type of the db context.</typeparam>
        /// <typeparam name="TContext2">The type of the db context.</typeparam>
        /// <typeparam name="TContext3">The type of the db context.</typeparam>
        /// <typeparam name="TContext4">The type of the db context.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        /// <remarks>
        /// This method only support one db context, if been called more than once, will throw exception.
        /// </remarks>
        public static IServiceCollection AddUnitOfWork<TContext1, TContext2, TContext3, TContext4>(this IServiceCollection services)
            where TContext1 : DbContext
            where TContext2 : DbContext
            where TContext3 : DbContext
            where TContext4 : DbContext
        {
            services.AddUnitOfWorkRepository();


            services.AddScoped<IUnitOfWork<TContext1>, UnitOfWork<TContext1>>();
            services.AddScoped<IUnitOfWork<TContext2>, UnitOfWork<TContext2>>();
            services.AddScoped<IUnitOfWork<TContext3>, UnitOfWork<TContext3>>();
            services.AddScoped<IUnitOfWork<TContext4>, UnitOfWork<TContext4>>();

            services.AddScoped<IQueryExecutor<TContext1>, QueryExecutor<TContext1>>();
            services.AddScoped<IQueryExecutor<TContext2>, QueryExecutor<TContext2>>();
            services.AddScoped<IQueryExecutor<TContext3>, QueryExecutor<TContext3>>();
            services.AddScoped<IQueryExecutor<TContext4>, QueryExecutor<TContext4>>();

            return services;
        }
    }
}
