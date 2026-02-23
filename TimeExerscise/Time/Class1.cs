namespace Taller01

{
    public class Time
    {
        private int _hour;
        private int _minute;
        private int _second;
        private int _millisecond;

        public Time()
            : this(0, 0, 0, 0)
        { }

        public Time(int hours)
            : this(hours, 0, 0, 0)
        { }

        public Time(int hours, int minutes)
            : this(hours, minutes, 0, 0)
        { }

        public Time(int hours, int minutes, int seconds)
            : this(hours, minutes, seconds, 0)
        { }

        public Time(int hours, int minutes, int seconds, int milliseconds)
        {
            if (!ValidHours(hours))
            {
                throw new ArgumentOutOfRangeException(nameof(hours));
            }
                
            if (!ValidMinute(minutes))
            {
                throw new ArgumentOutOfRangeException(nameof(minutes));
            }
                
            if (!ValidSecond(seconds))
            {
                throw new ArgumentOutOfRangeException(nameof(seconds));
            }
                
            if (!ValidMillisecond(milliseconds))
            {
                throw new ArgumentOutOfRangeException(nameof(milliseconds));
            }
            _hour = hours;
            _minute = minutes;
            _second = seconds;
            _millisecond = milliseconds;
        }

        public int Hour
        {
            get => _hour;
            set
            {
                if (!ValidHours(value)) throw new ArgumentOutOfRangeException(nameof(value));
                _hour = value;
            }
        }

        public int Minute
        {
            get => _minute;
            set
            {
                if (!ValidMinute(value)) throw new ArgumentOutOfRangeException(nameof(value));
                _minute = value;
            }
        }

        public int Second
        {
            get => _second;
            set
            {
                if (!ValidSecond(value)) throw new ArgumentOutOfRangeException(nameof(value));
                _second = value;
            }
        }

        public int Millisecond
        {
            get => _millisecond;
            set
            {
                if (!ValidMillisecond(value)) throw new ArgumentOutOfRangeException(nameof(value));
                _millisecond = value;
            }
        }

        public long ToMilliseconds()
        {
            return ((long)_hour * 3600 + _minute * 60 + _second) * 1000 + _millisecond;
        }

        public long ToSeconds()
        {
            return ToMilliseconds() / 1000;
        }

        public long ToMinutes()
        {
            return ToMilliseconds() / (60 * 1000);
        }

        public Time Add(Time other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            long totalMs = ToMilliseconds() + other.ToMilliseconds();
            long msPerDay = 24L * 3600 * 1000;
            long wrapped = totalMs % msPerDay;

            int hours = (int)(wrapped / (3600 * 1000));
            wrapped %= 3600 * 1000;
            int minutes = (int)(wrapped / (60 * 1000));
            wrapped %= 60 * 1000;
            int seconds = (int)(wrapped / 1000);
            int milliseconds = (int)(wrapped % 1000);

            return new Time(hours, minutes, seconds, milliseconds);
        }

        public bool IsOtherDay(Time other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            long msPerDay = 24L * 3600 * 1000;
            return ToMilliseconds() + other.ToMilliseconds() >= msPerDay;
        }

        public override string? ToString()
        {
            if (_hour == 12)
            {
                int hour12 = _hour % 12;
                if (hour12 == 0) hour12 = 12;
                string ampm = _hour >= 12 ? "PM" : "AM";
                return string.Format("{0:00}:{1:00}:{2:00}.{3:000} {4}", hour12, _minute, _second, _millisecond, ampm);
            }
            else
            {
                int hour12 = _hour % 12;
                string ampm = _hour >= 12 ? "PM" : "AM";
                return string.Format("{0:00}:{1:00}:{2:00}.{3:000} {4}", hour12, _minute, _second, _millisecond, ampm);
            }

        }

        private bool ValidHours(int hour) => hour >= 0 && hour <= 23;
        private bool ValidMillisecond(int ms) => ms >= 0 && ms <= 999;
        private bool ValidMinute(int minute) => minute >= 0 && minute <= 59;
        private bool ValidSecond(int second) => second >= 0 && second <= 59;
    }

}
       
