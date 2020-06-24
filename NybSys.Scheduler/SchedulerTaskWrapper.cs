using System;

namespace NybSys.Scheduler
{
    public class SchedulerTaskWrapper
    {
        public ISchedulerTask Task { get; set; }
        public DateTime LastRunTime { get; set; }
        public DateTime NextRunTime { get; set; }

        public string MobileNo { get; set; }
        public int LapTime { get; set; }

        public void Increment()
        {
            LastRunTime = NextRunTime;
            NextRunTime = NextRunTime.AddSeconds(LapTime);
        }

        public bool ShouldRun(DateTime currentTime)
        {
            return NextRunTime < currentTime && LastRunTime != NextRunTime;
        }
    }
}
