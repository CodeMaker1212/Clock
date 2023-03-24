using System;

namespace Clock
{
    public class TimeCorrector : ClockDecorator
    {
        public TimeCorrector(IClock clock, CurrentTimeFetcher timeService) : base(clock)
        {
            _currentTimeService = timeService;
            clock.OnTimeUpdated += HandleClockTimeUpdate;
        }

        private CurrentTimeFetcher _currentTimeService;
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
            _currentTimeService.GetTime(_ => SetTime(_));
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
                _currentTimeService.GetTime(_ => SetTime(_));             
            }                                
        }
    }
}