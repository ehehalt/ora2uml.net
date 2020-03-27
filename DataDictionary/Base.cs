using System;

namespace Ora2Uml.DataDictionary
{
    public class Base
    {
        /*
        protected static Decimal? GetValue(object value)
        {
            Decimal? result = null;

            if (value != System.DBNull.Value && typeof(Decimal).Equals(value.GetType()))
            {
                result = (Decimal)value;
            }

            return result;
        }
        */

        protected static T GetValue<T>(object value)
        {
            T result = default(T);

            if (value != System.DBNull.Value && typeof(T).Equals(value.GetType()))
            {
                result = (T)value;
            }

            return result;
        }

        protected static String GetString(object value, String defaultValue = "")
        {
            String result = GetValue<String>(value);
            if(String.IsNullOrEmpty(result))
            {
                result = defaultValue;
            }
            return result;
        }
    }
}