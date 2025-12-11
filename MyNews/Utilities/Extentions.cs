using System.Globalization;

namespace MyNews.Utilities
{
    public static class Extentions
    {
        public static string toShamsi(this DateTime dateTime)
        {
            PersianCalendar calendar = new PersianCalendar();
            return calendar.GetYear(dateTime) + "/" + calendar.GetMonth(dateTime) + "/" + calendar.GetDayOfMonth(dateTime);
        }


        public static string toShamsi(this DateOnly dateTime)
        {
            PersianCalendar calendar = new PersianCalendar();
            DateTime date = dateTime.ToDateTime(new TimeOnly());
            return calendar.GetYear(date) + "/" + calendar.GetMonth(date) + "/" + calendar.GetDayOfMonth(date);
        }

        public static string Shorten(this string text, int length)
        {
            string result = "";
            var str = text.Split(' ').ToList().Take(length);
            foreach (var item in str)
            {
                result += item + ' ';
            }
            return result + "...";

        }
    }
}
