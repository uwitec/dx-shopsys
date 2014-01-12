using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBFramework
{
    public static class DefaultValueConverter
    {
        public static string GetDefaultNullValue()
        {
            return "NULL";
        }

        public static string GetDefaultNOTNullValue()
        {
            return "NOT NULL";
        }
    }
}
