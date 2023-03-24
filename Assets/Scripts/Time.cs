namespace Clock
{
    public readonly struct Time
    {
        public Time(int hours, int minutes, int seconds)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        public readonly int Hours;
        public readonly int Minutes;
        public readonly int Seconds;      
    }
}