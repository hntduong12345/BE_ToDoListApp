using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BE_ToDoListApp.Application.Utils
{
    public static class DaysUtil
    {
        public static List<DateOnly> GetWeekDays(DateOnly curDate)
        {
            DayOfWeek firstDayOfWeek = DayOfWeek.Monday;

            // Find the start of the week
            int diff = (7 + (curDate.DayOfWeek - firstDayOfWeek)) % 7;
            DateOnly startOfWeek = curDate.AddDays(-diff);

            // Get all days of the week
            List<DateOnly> daysOfWeek = new List<DateOnly>();
            for (int i = 0; i < 7; i++)
            {
                daysOfWeek.Add(startOfWeek.AddDays(i));
            }

            return daysOfWeek;
        }
    }
}
