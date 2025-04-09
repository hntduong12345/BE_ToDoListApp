using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.Application.Utils
{
    public static class EnumUtil
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        
        public static string GetEnumName<T>(byte value)
        {
            return Enum.GetName(typeof(T), value)!;
        }

        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        //public static string GetDescriptionFromEnum<T>(this T value)
        //{
        //    if (value == null) return string.Empty;
        //    var fi = value?.GetType().GetField(value.ToString());
        //    if (fi != null)
        //    {
        //        var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
        //        if (attributes.Length > 0)
        //        {
        //            return attributes[0].Description;
        //        }
        //    }

        //    return value.ToString();
        //}
    }
}
