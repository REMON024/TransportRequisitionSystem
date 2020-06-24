using System;
using System.Threading;
using System.Threading.Tasks;

namespace NybSys.Scheduler
{
    public interface ISchedulerTask
    {
        Task ExecuteTaskAsync(string phoneNo , CancellationToken cancellationToken);
    }
}
