using System;
using UnityEngine;

namespace Clock
{
    public class AnalogClockDisplay : MonoBehaviour, IClockDisplay
    {
        [SerializeField] private ClockHand _hourHand;
        [SerializeField] private ClockHand _minuteHand;
        [SerializeField] private ClockHand _secondHand;
       
        public event Action OnInputUpdated;

        public void DisplayTime(Time time)
        {
            float hourHandAngle = ((time.Hours % 12) * 30f) +
                                  (time.Minutes / 60f * 30f) + 
                                  (time.Seconds / 3600f * 30f);
            float minuteHandAngle = time.Minutes * 6f;
            float secondHandAngle = time.Seconds * 6f;

            _hourHand.Rotate(Quaternion.Euler(0, 0, -hourHandAngle));
            _minuteHand.Rotate(Quaternion.Euler(0, 0, -minuteHandAngle));
            _secondHand.Rotate(Quaternion.Euler(0, 0, -secondHandAngle));
        }

        public Time GetInputtedTime()
        {
            int hours = (int)Mathf.Floor((360 -_hourHand.Angle / 30) % 12);
            int minutes = (int)((360 - _minuteHand.Angle) / 360 * 60);
            int seconds = (int)((360 - _secondHand.Angle) / 360 * 60);

            return new Time(hours, minutes, seconds);
        }

        public void SetInputPossibility(bool flag)
        {
            _hourHand.IsCanBeDrag = flag;
            _minuteHand.IsCanBeDrag = flag;
            _secondHand.IsCanBeDrag = flag;          

            if (flag == true)
            {
                _hourHand.OnDragged += () => OnInputUpdated?.Invoke();
                _minuteHand.OnDragged += () => OnInputUpdated?.Invoke();
                _secondHand.OnDragged += () => OnInputUpdated?.Invoke();             
            }
            else
            {
                _hourHand.OnDragged -= () => OnInputUpdated?.Invoke();
                _minuteHand.OnDragged -= () => OnInputUpdated?.Invoke();
                _secondHand.OnDragged -= () => OnInputUpdated?.Invoke();             
            }
        }      
    }
}