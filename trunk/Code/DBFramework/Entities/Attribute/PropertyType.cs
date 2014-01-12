#region Author & Version
/* ======================================================================== 
* 
* Author：sunjianwen Time：11/20/2012 10:00:10 AM 
* File name：ETProperty 
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

namespace DBFramework.Entities.Attribute
{
    public class PropertyType : System.Attribute
    {
        public bool IsPrimaryKey
        {
            get;
            set;
        }

        public bool IsNavigateKey
        {
            get;
            set;
        }
    }
}
