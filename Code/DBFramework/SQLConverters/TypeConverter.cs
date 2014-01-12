using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBFramework
{
    public static class TypeConverter
    {
        public static string GetSQLType(Type type)
        {
            if (type == typeof(int))
                return GetTypeInt32();
            else if (type == typeof(bool))
                return GetTypeBool();
            else if (type == typeof(string))
                return GetTypeString();
            else if (type == typeof(DateTime))
                return GetTypeDateTime();
            else if (type == typeof(float))
                return GetTypeFloat();
            else if (type == typeof(double))
                return GetTypeDouble();
            else return GetTypeString(); 
        }

        public static bool IsNullableType(Type theType)
        {
            return (theType.IsGenericType && theType.
              GetGenericTypeDefinition().Equals
              (typeof(Nullable<>)));
        }

        public static string GetTypeInt32()
        {
            return "int";
        }

        public static string GetTypeBool()
        {
            return "bit";
        }

        public static string GetTypeString()
        {
            return "nvarchar";
        }

        public static string GetTypeDateTime()
        {
            return "datetime";
        }

        public static string GetTypeFloat()
        {
            return "float";
        }

        public static string GetTypeDouble()
        {
            return "float";
        }
    }
}
