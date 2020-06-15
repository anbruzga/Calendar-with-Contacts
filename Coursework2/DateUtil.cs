using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework2
{
    // DateUtil class for:
    // 1. Abstraction of some personally annoying DateTime syntax
    // 2. Other DateTime simplifications
    public static class DateUtil
    {
        
        // todo: Does not simplify, refactor 
        public static int DaysInMonth(int year, int month)
        {
            return DateTime.DaysInMonth(year, month);
        }

        public static DateTime TimeZero(DateTime d)
        {
            DateTime date = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0);
            return date;
        }

        public static DateTime TimeMax(DateTime d)
        {
            DateTime date = new DateTime(d.Year, d.Month, d.Day, 23, 59, 59);
            return date;
        }
        // Does not simplify, refactor
        public static DateTime AddMonth(DateTime CurrentDate, int MonthToAdd)
        {
            var month = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
            var first = month.AddMonths(MonthToAdd);
            return  first;
        }
       
        // Does not simplify, refactor;
        public static DateTime AddHour(DateTime d, double h)
        {
            d = d.AddHours(h);
            return d;
        }


        public static int FirstWeekDay(int year, int month)
        {
            DateTime date = new DateTime(year, month, 1);
            int WeekDay = WeekDayToInt(date.DayOfWeek.ToString());

            return WeekDay;
        }


        private static int WeekDayToInt(string name)
        {
            int WeekDay = -1;
            if (name.Equals("Sunday")) WeekDay = 7;
            else if (name.Equals("Saturday")) WeekDay = 6;
            else if (name.Equals("Friday")) WeekDay = 5;
            else if (name.Equals("Thursday")) WeekDay = 4;
            else if (name.Equals("Wednesday")) WeekDay = 3;
            else if (name.Equals("Tuesday")) WeekDay = 2;
            else if (name.Equals("Monday")) WeekDay = 1;
            
            if (WeekDay == -1) {
                Console.WriteLine("DateUtil.WeekDayToInt ERROR");
            }

            return WeekDay;
        }



        public static DateTime AddDay(DateTime CurrentDate, int DayToAdd)
        {
            var TimeFrom = CurrentDate.AddDays((double) DayToAdd);
            return TimeFrom;
        }


        public static DateTime AddDay(DateTime CurrentDate)
        {
            var TimeFrom = CurrentDate.AddDays(1);
            return TimeFrom;
        }

        public static bool DatesEqual(DateTime date1, DateTime date2)
        {
            if (date1.Date == date2.Date) return true;
            else return false;
        }
    }
}
