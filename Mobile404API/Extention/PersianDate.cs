using System.Globalization;

namespace Mobile404API.Extention
{
    public static class PersianDate
    {
        public static string ToShamsiDate(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();

            return pc.GetHour(date) + ":" + pc.GetMinute(date) + ",," + pc.GetMonth(date) + ":" + pc.GetDayOfMonth(date).ToString("00");
        }

        public static DateTime toMiladi(this DateTime date) 
        {
            return new DateTime(date.Year, date.Month, date.Day, new PersianCalendar());
        }
    }
}
