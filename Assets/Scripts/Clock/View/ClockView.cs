using UnityEngine;
using UnityEngine.UI;

namespace Clock
{
    public class ClockView : MonoBehaviour, IClockView
    {
        [SerializeField] private AnalogClockDisplay _analogClock;
        [SerializeField] private DigitalClockDisplay _digitalClock;
        [SerializeField] private Button _dayTimeSwitchButton;
        [SerializeField] private Text _dayTimeButtonText;

        private enum DayTimeFormat { AM, PM }
        private DayTimeFormat _displayingFormat;
        private int _lastDisplayedHour;

        private void OnEnable()
        {
            _analogClock.OnInputUpdated -= HandleAnalogClockInputUpdate;
            _digitalClock.OnInputUpdated -= HandleDigitalClockInputUpdate;
            _dayTimeSwitchButton.onClick.AddListener(SwitchDayTimeFormat);
        }

        private void OnDisable()
        {
            _analogClock.OnInputUpdated -= HandleAnalogClockInputUpdate;
            _digitalClock.OnInputUpdated -= HandleDigitalClockInputUpdate;
            _dayTimeSwitchButton.onClick.RemoveAllListeners();
        }

        public void DisplayTime(Time time)
        {
            _analogClock.DisplayTime(time);
            _digitalClock.DisplayTime(time);
            _lastDisplayedHour = time.Hours;
        }

        public Time GetInputtedTime()
        {
            int hours = _digitalClock.GetInputtedTime().Hours;
            int minutes = _digitalClock.GetInputtedTime().Minutes;
            int seconds = _digitalClock.GetInputtedTime ().Seconds;

            if (_displayingFormat == DayTimeFormat.PM)
                hours += 12;

            return new Time(hours, minutes, seconds);
        }

        public void SetInputPossibility(bool flag)
        {
            _analogClock.SetInputPossibility(flag);
            _digitalClock.SetInputPossibility(flag);
            _dayTimeSwitchButton.gameObject.SetActive(flag);

            if (flag == true) HandleInputEnabling();      
        }

        private void HandleInputEnabling()
        {
            _displayingFormat = _lastDisplayedHour > 12 ? DayTimeFormat.PM : DayTimeFormat.AM;
            SetDayTimeFormatButtonText(_displayingFormat.ToString());
            _digitalClock.DisplayTime(_analogClock.GetInputtedTime());
        }

        private void HandleAnalogClockInputUpdate() => _digitalClock.DisplayTime(_analogClock.GetInputtedTime());

        private void HandleDigitalClockInputUpdate()
        {
            _analogClock.DisplayTime(_digitalClock.GetInputtedTime());

            Time inputtedTime = _digitalClock.GetInputtedTime();
            int modifiedHoursValue = inputtedTime.Hours;

            if (inputtedTime.Hours >= 12)           
                modifiedHoursValue = inputtedTime.Hours - 12;         
            
            _digitalClock.DisplayTime(new Time(modifiedHoursValue, inputtedTime.Minutes, inputtedTime.Seconds));
        }

        private void SwitchDayTimeFormat()
        {
            _displayingFormat = _displayingFormat == DayTimeFormat.AM ? DayTimeFormat.PM : DayTimeFormat.AM;
            SetDayTimeFormatButtonText(_displayingFormat.ToString());
        }

        private void SetDayTimeFormatButtonText(string text) => _dayTimeButtonText.text = text;
    }
}