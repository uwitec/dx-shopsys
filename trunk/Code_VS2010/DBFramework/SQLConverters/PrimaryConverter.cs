using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBFramework
{
    public static class PrimaryConverter
    {
        public static string GetDefaultPrimaryFiledString()
        {
            return "IDENTITY(1,1)";
        }

        public static string GetDefaultPrimaryTableEndString()
        {
            return "ON [PRIMARY]";
        }
    }
}
