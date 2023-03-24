using System;
using UnityEngine;
using UnityEngine.UI;
using static Clock.Constants;

namespace Clock
{
    public class DigitalClockDisplay : MonoBehaviour, IClockDisplay
    {
        [SerializeField] private InputField _hoursInput;
        [SerializeField] private InputField _minutesInput;
        [SerializeField] private InputField _secondsInput;
        [SerializeField] private Outline[] _inputHighights;

        private const string FORMAT = "00";

        public event Action OnInputUpdated;

        private void Start()
        {
            _hoursInput.onEndEdit.AddListener(HandleHoursInput);
            _minutesInput.onEndEdit.AddListener(HandleMinutesInput);
            _secondsInput.onEndEdit.AddListener(HandleSecondsInput);
        }

        private void HandleHoursInput(string value)
        {
            if (int.TryParse(value, out int handledValue))
            {
                _hoursInput.text = handledValue.ToString(FORMAT);

                if (handledValue > MAX_HOURS_VALUE)               
                    _hoursInput.text = MAX_HOURS_VALUE.ToString(FORMAT);

                if (handledValue < MIN_TIME_VALUE)
                    _hoursInput.text = MIN_TIME_VALUE.ToString(FORMAT);               
            }
            OnInputUpdated?.Invoke();
        }

        private void HandleMinutesInput(string value)
        {
            if (int.TryParse(value, out int handledValue))
            {
                _minutesInput.text = handledValue.ToString(FORMAT);

                if (handledValue > MAX_MINUTES_VALUE)
                    _minutesInput.text = MAX_MINUTES_VALUE.ToString(FORMAT);

                if (handledValue < MIN_TIME_VALUE)
                    _minutesInput.text = MIN_TIME_VALUE.ToString(FORMAT);              
            }
            OnInputUpdated?.Invoke();
        }

        private void HandleSecondsInput(string value)
        {
            if (int.TryParse(value, out int handledValue))
            {
                _secondsInput.text = handledValue.ToString(FORMAT);

                if (handledValue > MAX_SECONDS_VALUE)
                    _secondsInput.text = MAX_SECONDS_VALUE.ToString(FORMAT);

                if (handledValue < MIN_TIME_VALUE)
                    _secondsInput.text = MIN_TIME_VALUE.ToString(FORMAT);              
            }
            OnInputUpdated?.Invoke();
        }

        public void DisplayTime(Time time)
        {
             _hoursInput.text = time.Hours.ToString(FORMAT);
             _minutesInput.text = time.Minutes.ToString(FORMAT);
             _secondsInput.text = time.Seconds.ToString(FORMAT);
        }

        public void SetInputPossibility(bool flag)
        {
            _hoursInput.interactable = flag;
            _minutesInput.interactable = flag;
            _secondsInput.interactable = flag;

            for (int i = 0; i < _inputHighights.Length; i++)
                 _inputHighights[i].enabled = flag;
        }

        public Time GetInputtedTime()
        {
            return new Time(int.Parse(_hoursInput.text), 
                            int.Parse(_minutesInput.text),
                            int.Parse(_secondsInput.text));
        }
    }
}