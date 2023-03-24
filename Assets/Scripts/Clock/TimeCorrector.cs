using System;

namespace Clock
{
    public class TimeCorrector : ClockDecorator
    {
        public TimeCorrector(IClock clock, CurrentTimeFetcher timeFetcher) : base(clock)
        {
            _timeFetcher = timeFetcher;
            clock.OnTimeUpdated += HandleClockTimeUpdate;
        }

        private CurrentTimeFetcher _timeFetcher;
        private int _lastClockHour;

        public override event Action OnTimeUpdated;

        public override Time Time
        {
            get => clock.Time;
            protected set => clock.SetTime(value);
        }

        public override void SetTime(Time time) => Time = time;

        public override void Run()
        {
            _timeFetcher.GetTime(_ => SetTime(_));
            _lastClockHour = clock.Time.Hours;
            clock.OnTimeUpdated += OnTimeUpdated;
            clock.Run();
        }

        public override void Stop()
        {
            clock.Stop();
            clock.OnTimeUpdated -= OnTimeUpdated;
        }   

        private void HandleClockTimeUpdate()
        {
            if (clock.Time.Hours != _lastClockHour)
            {
                _lastClockHour = clock.Time.Hours;
                _timeFetcher.GetTime(_ => SetTime(_));             
            }                                
        }
    }
}