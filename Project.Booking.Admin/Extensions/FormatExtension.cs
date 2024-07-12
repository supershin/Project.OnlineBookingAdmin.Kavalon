using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Extensions
{
    public static class FormatExtension
    {
        public static string StandardNumberFormat = "{0:#,##0.00}";
        public static string StandardCulture = "en-US";
        public static string StandardDateFormat = "yyyy-MM-dd";
        public static string StandardDateTimeFormat = "yyyy-MM-dd HH:mm";
        public static Guid AsGuid(this Guid? value1)
        {
            return value1.HasValue ? value1.Value : Guid.Empty;
        }
        public static int AsInt(this int? value1)
        {
            return value1.HasValue ? value1.Value : 0;
        }
        public static int AsLong(this int? value1)
        {
            return value1.HasValue ? value1.Value : 0;
        }
        public static DateTime AsDate(this DateTime? value1)
        {
            return value1.HasValue ? value1.Value : DateTime.Now;
        }
        public static bool AsBool(this bool? value1)
        {
            return value1.HasValue ? value1.Value : false;
        }
        public static decimal AsDecimal(this decimal? value1)
        {
            return value1.HasValue ? value1.Value : 0;
        }

        public static int? ToInt(this string str)
        {
            int result;
            if (int.TryParse(str, out result))
                return result;

            return null;
        }
        public static DateTime? ToDate(this string str)
        {
            DateTime result;
            if (!string.IsNullOrEmpty(str.ToStringNullable()))
            {
                if (DateTime.TryParse(str, out result))
                    return result;
            }
            return null;
        }
        public static string ToStringNullable(this string param)
        {
            return string.IsNullOrEmpty(param) ? null : param.Trim();
        }
        public static string ToStringEmpty(this string param)
        {
            return string.IsNullOrEmpty(param) ? string.Empty : param.Trim();
        }
        public static string ToStringNumber(this decimal number)
        {            
            return string.Format(StandardNumberFormat, number);
        }
        public static string ToStringNumber(this long number)
        {
            return string.Format(StandardNumberFormat, number);
        }
        public static string ToStringNumber(this decimal? number)
        {
            if (number == null)
            {
                return null;
            }
            return string.Format(StandardNumberFormat, number);
        }
        public static string ToStringNumber(this int number)
        {
            return number.ToString(StandardNumberFormat);
        }
        public static string ToStringDateTime(this DateTime? dateValue)
        {
            var culture = CultureInfo.CreateSpecificCulture(StandardCulture);
            return dateValue.HasValue ? dateValue.Value.ToString(StandardDateTimeFormat, culture) : string.Empty;
        }
        public static string ToStringDate(this DateTime? dateValue)
        {

            var culture = CultureInfo.CreateSpecificCulture(StandardCulture);
            return dateValue.HasValue ? dateValue.Value.ToString(StandardDateFormat, culture) : string.Empty;
        }
        public static string Right(this string sValue, int iMaxLength)
        {
            //Check if the value is valid
            if (string.IsNullOrEmpty(sValue))
            {
                //Set valid empty string as string could be null
                sValue = string.Empty;
            }
            else if (sValue.Length > iMaxLength)
            {
                //Make the string no longer than the max length
                sValue = sValue.Substring(sValue.Length - iMaxLength, iMaxLength);
            }

            //Return the string
            return sValue;
        }

        public static string Left(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            maxLength = Math.Abs(maxLength);

            return (value.Length <= maxLength
                   ? value
                   : value.Substring(0, maxLength)
                   );
        }
    }
}
