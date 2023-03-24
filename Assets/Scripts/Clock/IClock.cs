using System;

namespace Clock
{
    public interface IRunnable
    {
        void Run();

        void Stop();
    }

    public interface ITimeService
    {
        Time Time { get; }

        event Action OnTimeUpdated;
    }

    public interface IClock : ITimeService, IRunnable
    {
        void SetTime(Time time);
    }
}