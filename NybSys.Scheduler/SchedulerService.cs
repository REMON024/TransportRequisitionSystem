using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NybSys.Scheduler
{
    public class SchedulerService : HostedService
    {
        private List<SchedulerTaskWrapper> _scheduledTasks = new List<SchedulerTaskWrapper>();
        //private readonly ISigtranGateWayService _sigtranGateWayService;

        public SchedulerService()
        {
            //_sigtranGateWayService = sigtranGateWayService;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await CreateSchedulerTask();

                await ExecuteOnceAsync(cancellationToken);

                await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
            }
        }

        private async Task ExecuteOnceAsync(CancellationToken cancellationToken)
        {
            var taskFactory = new TaskFactory(TaskScheduler.Current);
            DateTime referenceTime = DateTime.Now;

            var tasksThatShouldRun = _scheduledTasks.Where(t => t.ShouldRun(referenceTime)).OrderBy(p => p.NextRunTime).ToList();

            foreach (var taskThatShouldRun in tasksThatShouldRun)
            {
                taskThatShouldRun.Increment();

                await taskFactory.StartNew(
                    async () =>
                    {
                        try
                        {
                            await taskThatShouldRun.Task.ExecuteTaskAsync(taskThatShouldRun.MobileNo,cancellationToken);
                        }
                        catch (Exception ex)
                        {
                            // Here Write the Log
                            Console.WriteLine(ex.Message);
                        }
                    },
                    cancellationToken);
            }
        }

        private async Task CreateSchedulerTask()
        {
            List<CellularTrack> lstNumbers = NumbersForAlert.GetNumbers();

            DateTime referenceTime = DateTime.Now;

            _scheduledTasks.RemoveAll(p => true);

            foreach(CellularTrack track in lstNumbers)
            {
                _scheduledTasks.Add(new SchedulerTaskWrapper()
                {
                    //Task = new SchedulerTask(_sigtranGateWayService),
                    NextRunTime = referenceTime,
                    LapTime = track.LapTime,
                    MobileNo = track.PhoneNo
                });
            }

            await Task.CompletedTask;
        }       
    }
}
