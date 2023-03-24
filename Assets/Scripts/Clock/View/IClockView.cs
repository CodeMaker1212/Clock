using System;

namespace Clock
{
    public interface IClockView
    {
        Time GetInputtedTime();

        void DisplayTime(Time time);

        void SetInputPossibility(bool flag);
    }

    public interface IClockDisplay : IClockView
    {
        event Action OnInputUpdated;
    }
}