using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace NybSys.Common.Extension
{
    public static class Extension
    {
        public static DateTime GetLocalZoneDate(this DateTime date)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.Local).Date;
        }

        public static decimal GetDecimalValue(this decimal amount)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            return decimal.Parse(amount.ToString());
        }

        public static bool EqualsWithLower(this string value1, string value2)
        {
            return value1.ToLower().Equals(value2.ToLower());
        }

        public static string ToDescription<TEnum>(this TEnum EnumValue) where TEnum : IConvertible
        {
            if (EnumValue is Enum)
            {
                Type type = EnumValue.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == EnumValue.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() as DescriptionAttribute;

                        if (descriptionAttribute != null)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }

            throw new ArgumentNullException();
        }
    }
}
