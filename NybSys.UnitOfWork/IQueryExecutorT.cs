using Microsoft.EntityFrameworkCore;

namespace NybSys.UnitOfWork
{
    public interface IQueryExecutor<TContext> : IQueryExecutor where TContext : DbContext
    {

    }
}
