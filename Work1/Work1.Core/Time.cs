using System.Globalization;

namespace Work1.Core

{
    
    public class Time
    {
        private int _hour;
        private int _millisecond;
        private int _minute;
        private int _second;



        public int Hour
        {
            get => _hour;
            set
            {
                if (!ValidHour(value))
                    throw new Exception($"The hour: {value}, is not valid.");
                _hour = value;
            }
        }
        public int Millisecond
        {
            get => _millisecond;
            set
            {
                if (!ValidMillisecond(value))
                    throw new Exception($"The milisecond: {value}, is not valid.");
                _millisecond = value;
            }
        }
        public int Minute
        {
            get => _minute;
            set
            {
                if (!ValidMinute(value))
                    throw new Exception($"The minute: {value}, is not valid.");
                _minute = value;
            }
        }
        public int Second
        {
            get => _second;
            set
            {
                if (!ValidSecond(value))
                    throw new Exception($"The second: {value}, is not valid.");
                _second = value;
            }
        }
        public Time() : this(0, 0, 0, 0) { }
        public Time(int hour) : this(hour, 0, 0, 0) { }
        public Time(int hour, int minute) : this(hour, minute, 0, 0) { }
        public Time(int hour, int minute, int second) : this(hour, minute, second, 0) { }
        public Time(int hour, int minute, int second, int millisecond)
        {
            Hour = hour;
            Minute = minute;
            Second = second;
            Millisecond = millisecond;

        }

        // Validations
        public static bool ValidHour(int hour) => hour >= 0 && hour <= 23;
        public static bool ValidMinute(int minute) => minute >= 0 && minute <= 59;
        public static bool ValidSecond(int second) => second >= 0 && second <= 59;
        public static bool ValidMillisecond(int millisecond) => millisecond >= 0 && millisecond <= 999;

        public long ToMilliseconds()
        {
            return (long)Hour * 3600000 +
                   (long)Minute * 60000 +
                   (long)Second * 1000 +
                   Millisecond;
        }
        public long ToSeconds()
        {
            return (long)Hour * 3600 +
                   (long)Minute * 60 +
                   Second;
        }
        public long ToMinutes()
        {
            return (long)Hour * 60 +
                   Minute;
        }

        //operations
        public Time Add(Time other)
        {
            int ms = this.Millisecond + other.Millisecond;
            int carryS = ms / 1000;
            ms = ms % 1000;

            int s = this.Second + other.Second + carryS;
            int carryM = s / 60;
            s %= 60;

            int m = this.Minute + other.Minute + carryM;
            int carryH = m / 60;
            m %= 60;

            int h = this.Hour + other.Hour + carryH;
            h %= 24;

            return new Time(h, m, s, ms);

        }
        public bool IsOtherDay(Time other)
        {
            long totalMs = this.ToMilliseconds() + other.ToMilliseconds();
            return totalMs >= 24L * 3600 * 1000;
        }

        //ToString
        public override string ToString()
        {
            string ampm = Hour >= 12 ? "PM" : "AM";

            string hourStr;
            if (Hour == 0) hourStr = "00";
            else if (Hour == 12) hourStr = "12";
            else if (Hour < 12) hourStr = Hour.ToString("D2");
            else hourStr = (Hour - 12).ToString("D2");

            return $"{hourStr}:{Minute:D2}:{Second:D2}:{Millisecond:D3} {ampm}";
        }
    }
}
