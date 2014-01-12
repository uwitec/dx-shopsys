#region Author & Version
/* ======================================================================== 
* 
* Author：sunjianwen Time：9/7/2012 9:25:22 AM 
* File name：TagBlinkLog 
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
using DBFramework.Entities;
using DBFramework.Entities.Attribute;

namespace DBFramework.Entities
{
    public class TagBlinkLogs : IEntity, IMonthly
    {
        public int TagId
        {
            get;
            set;
        }

        public int AnchorId
        {
            get;
            set;
        }

        public double? Distance 
        { 
            get; 
            set; 
        }

        public int Capabilities
        {
            get;
            set;
        }

        public int Status
        {
            get;
            set;
        }

        public double? GasDensity 
        { 
            get;
            set; 
        }

        public DateTime TimeStamp
        {
            get;
            set;
        }

        public bool IsDeleted
        {
            get;
            set;
        }



        [Navigation]
        public Anchors Anchor
        {
            get;
            set;
        }
    }
}
