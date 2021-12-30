using System;

namespace MVVMReactive.Service.Service
{
    public class TimeService
    {
        public DateTime AddHour(DateTime dateTime, int hours)
        {
            return dateTime.AddHours(hours);
        }

        public DateTime AddOneSecond(DateTime dateTime)
        {
            return dateTime.AddSeconds(1);
        }
    }
}
