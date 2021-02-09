using System;
namespace Engine
{
    public class Time
    {
        long last_time = DateTime.Now.Ticks;
        public float DeltaTime { get; private set; } = float.Epsilon;
        public float SecondsSinceStart { get; protected set; } = float.Epsilon;
        /// <summary>
        /// <seealso cref="DateTime.Ticks"/>
        /// </summary>
        public long Ticks => DateTime.Now.Ticks;
        public void Update()
        {
            long time = Ticks;
            DeltaTime = ((time - last_time) / 10_000_000f);
            last_time = time;
            SecondsSinceStart += DeltaTime;
        }
        /// <summary>
        /// Reset time
        /// </summary>
        public void Reset()
        {
            SecondsSinceStart = float.Epsilon;
            DeltaTime = float.Epsilon;
            last_time = Ticks;
        }
        /// <summary>
        /// Returns a formated time <see langword="string"/>.
        /// (<paramref name="time"/> >= 1day) => DD:HH:MM
        /// (<paramref name="time"/> >= 1h) => HH:MM:SS
        /// (<paramref name="time"/> < 1h) => MM:SS:mmm
        /// </summary>
        /// <param name="time">Time in seconds</param>
        /// <returns></returns>
        public static string GetTimeStringFromSeconds(float time)
        {
            TimeSpan ts = new TimeSpan((long)(TimeSpan.TicksPerSecond * time));
            if (ts.Days >= 1)
            {
                return string.Format("{0} {1}:{2}", ts.Days.ToString("00"), ts.Hours.ToString("00"), ts.Minutes.ToString("00"));
            }
            else if (ts.Hours >= 1)
            {
                return string.Format("{0}:{1}:{2}", ts.Hours.ToString("00"), ts.Minutes.ToString("00"), ts.Seconds.ToString("00"));
            }
            else
            {
                return string.Format("{0}:{1}.{2}", ts.Minutes.ToString("00"), ts.Seconds.ToString("00"), ts.Milliseconds.ToString("000"));
            }
        }
        public override string ToString()
        {
            return GetTimeStringFromSeconds(SecondsSinceStart);
        }
    }
}
