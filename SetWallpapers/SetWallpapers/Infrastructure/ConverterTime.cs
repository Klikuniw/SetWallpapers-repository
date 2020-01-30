using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetWallpapers.Infrastructure
{
    public static class ConverterTime
    {
        public static TimeSpan ToTimeSpan(string time)
        {
            TimeSpan res = new TimeSpan(0, 0, 0, 0);

            var value = time.Split(' ');
            switch (value[1])
            {
                case "min":
                {
                    res = new TimeSpan(0, Convert.ToInt32(value[0]), 0);
                    break;
                }
                case "day":
                {
                    res = new TimeSpan(Convert.ToInt32(value[0]), 0, 0, 0);
                    break;
                }
                case "sec":
                {
                    res = new TimeSpan(0, 0, 0, Convert.ToInt32(value[0]));
                    break;
                }
            }

            return res;
        }
    }
}
