namespace Clock
{
    public class ClockController
    {
        public ClockController(IClock clock, IClockView clockView, Alarm alarm, IAlarmView alarmView)
        {
            _clock = clock;
            _clockView = clockView;
            _alarm = alarm;
            _alarmView = alarmView;

            _alarmView.OnSetupClick += SetAlarmSetupMode;
            _alarmView.OnConfirmRingTimeClick += CompleteAlarmSetup;
            _alarmView.OnTurnOffRingClick += TurnOffAlarm;
            _alarmView.OnResetRingClick += ResetAlarm;

            SetTimeDisplayingMode();
        }
        private IClock _clock;
        private IClockView _clockView;
        private Alarm _alarm;
        private IAlarmView _alarmView;

        private void SetAlarmSetupMode()
        {
            DisableClock();
            _clock.OnTimeUpdated -= () => _clockView.DisplayTime(_clock.Time);
            _alarmView.WaitForSetupConfirming();
            _clockView.SetInputPossibility(true);
        }

        private void CompleteAlarmSetup()
        {
            _clockView.SetInputPossibility(false);
            _alarm.SetTime(_clockView.GetInputtedTime());
            _alarm.OnRing += _alarmView.Ring;
            _alarm.Run();

            _alarmView.WaitForRing(_alarm.Time);
            SetTimeDisplayingMode();                      
        }    

        private void TurnOffAlarm()
        {
            _alarm.Stop();
            _alarmView.StopRing();
            _alarmView.WaitForSetupStart();
        }
            
        private void ResetAlarm()
        {
            _alarm.Stop();
            _alarmView.WaitForSetupStart();
        }

        private void SetTimeDisplayingMode()
        {
            _clock.OnTimeUpdated += () => _clockView.DisplayTime(_clock.Time);
            _clock.Run();
        }

        public void DisableClock()
        {
            _clock.Stop();
            _alarm.Stop();
        }
    }
}