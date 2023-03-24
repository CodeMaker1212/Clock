using System;

namespace Clock
{
    public abstract class ClockDecorator : IClock
    {
        public ClockDecorator(IClock clock) => this.clock = clock;

        protected IClock clock;

        public abstract Time Time { get; protected set; }

        public abstract event Action OnTimeUpdated;

        public abstract void SetTime(Time time);

        public abstract void Run();       

        public abstract void Stop();
    }
}