using System;
using static Clock.Constants;

namespace Clock
{
    public class Alarm : ClockDecorator
    {     
        public Alarm(IClock clock) : base(clock) { }

        public override Time Time { get; protected set; }

        public event Action OnRing;

        public override event Action OnTimeUpdated;

        public override void Run() =>
            clock.OnTimeUpdated += HandleTimeUpdate;


        public override void Stop() =>
            clock.OnTimeUpdated -= HandleTimeUpdate;

        public override void SetTime(Time ringTime)
        {
            Time = new Time
            (
               Math.Clamp(ringTime.Hours, MIN_TIME_VALUE, MAX_HOURS_VALUE),
               Math.Clamp(ringTime.Minutes, MIN_TIME_VALUE, MAX_MINUTES_VALUE),
               Math.Clamp(ringTime.Seconds, MIN_TIME_VALUE, MAX_SECONDS_VALUE)
           );
        }

        private void HandleTimeUpdate()
        {
            if (clock.Time.Hours == Time.Hours &&
                clock.Time.Minutes == Time.Minutes &&
                clock.Time.Seconds == Time.Seconds)
                Ring();
        }

        private void Ring() => OnRing?.Invoke();
    }
}