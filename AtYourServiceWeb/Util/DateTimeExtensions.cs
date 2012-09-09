

namespace AtYourService.Web.Util
{
    using System;

    public static class DateTimeExtensions
    {
        public static long ToMinutes(this DateTime dateTime)
        {
            const long epochTicks = 621355968000000000;
            var unixTicks = dateTime.Ticks - epochTicks;
            var milliseconds = unixTicks / 10000;

            return milliseconds / (60 * 1000);
        }
    }
}