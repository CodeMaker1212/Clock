using System;
using static Clock.Constants;

namespace Clock
{
    public class Clock : IClock
    {
        public Clock(ISecondsCountdownMechanism mechanism)
        {
            _mechanism = mechanism;
        }

        private ISecondsCountdownMechanism _mechanism;

        public Time Time { get; private set; }

        public event Action OnTimeUpdated;

        public void SetTime(Time time)
        {
            Time = new Time
            (
                Math.Clamp(time.Hours, MIN_TIME_VALUE, MAX_HOURS_VALUE),
                Math.Clamp(time.Minutes, MIN_TIME_VALUE, MAX_MINUTES_VALUE),
                Math.Clamp(time.Seconds, MIN_TIME_VALUE, MAX_SECONDS_VALUE)
            );

            OnTimeUpdated?.Invoke();
        }

        public void Run()
        {
            _mechanism.OnSecondPassed += Tick;
            _mechanism.Run();
        }

        public void Stop()
        {
            _mechanism.OnSecondPassed -= Tick;
            _mechanism.Stop();
        }

        private void Tick()
        {
            byte updatedSeconds = (byte)Time.Seconds;
            byte updatedMinutes = (byte)Time.Minutes;
            byte updatedHours = (byte)Time.Hours;

            updatedSeconds++;

            if (updatedSeconds > MAX_SECONDS_VALUE)
            {
                updatedMinutes++;
                updatedSeconds = MIN_TIME_VALUE;

                if (updatedMinutes > MAX_MINUTES_VALUE)
                {
                    updatedHours++;
                    updatedMinutes = MIN_TIME_VALUE;

                    if (updatedHours > MAX_HOURS_VALUE)
                        updatedHours = MIN_TIME_VALUE;
                }
            }

            SetTime(new Time(updatedHours, updatedMinutes, updatedSeconds));        
        }
    }
}