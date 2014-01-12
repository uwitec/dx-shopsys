#region Author & Version
/* ======================================================================== 
* 
* Author：sunjianwen Time：11/20/2012 11:23:21 AM 
* File name：SQLReflectorException 
* Version：V1.0.1
* Company: APHT
* 
* ======================================================================== 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBFramework.Exception
{
    public class ReflectorException : System.Exception
    {
        public ReflectorException(string message)
            :base(message)
        {
        }
    }
}
