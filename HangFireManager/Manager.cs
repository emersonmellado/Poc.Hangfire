using Hangfire;
using System;
using System.Linq.Expressions;

namespace HangFireManager
{
    public class Manager
    {
        public Manager()
        {
        }

        //Fire-and-forget
        public void AddJob(Expression<Action> action)
        {
            BackgroundJob.Enqueue(action);
        }

        //Delayed
        public void AddJob(Expression<Action> action, int minutesDelay)
        {
            BackgroundJob.Schedule(action, TimeSpan.FromMinutes(minutesDelay));
        }

        //Recurring
        public void AddJob(Expression<Action> action, Period period)
        {
            var hfperiod = period.HasFlag(Period.Daily) ? Cron.Daily() : Cron.Hourly();
            RecurringJob.AddOrUpdate(action, hfperiod);
        }
    }

    public enum Period
    {
        Daily,
        Hourly
    }
}