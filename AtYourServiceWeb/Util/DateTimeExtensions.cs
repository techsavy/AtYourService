

namespace AtYourService.Web.Util
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class DateTimeExtensions
    {
        public static long ToMinutes(this DateTime dateTime)
        {
            const long epochTicks = 621355968000000000;
            var unixTicks = dateTime.Ticks - epochTicks;
            var milliseconds = unixTicks / 10000;

            return milliseconds / (60 * 1000);
        }

        public static DateTime MinDate(this IEnumerable<DateTime> left, IEnumerable<DateTime> right)
        {
            var minLeft = left.Any() ? left.Min() : DateTime.MaxValue;
            var minRight = right.Any() ? right.Min() : DateTime.MaxValue;

            return minLeft.Ticks < minRight.Ticks ? minLeft : minRight;
        }

        public static DateTime MaxDate(this IEnumerable<DateTime> left, IEnumerable<DateTime> right)
        {
            var maxLeft = left.Any() ? left.Max() : DateTime.MinValue;
            var maxRight = right.Any() ? right.Max() : DateTime.MinValue;

            return maxLeft.Ticks > maxRight.Ticks ? maxLeft : maxRight;
        }
    }
}