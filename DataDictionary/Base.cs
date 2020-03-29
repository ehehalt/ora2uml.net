using System;

namespace Ora2Uml.DataDictionary
{
    public class Base
    {
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