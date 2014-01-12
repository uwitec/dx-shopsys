#region Author & Version
/* ======================================================================== 
* 
* Author：sunjianwen Time：9/5/2012 11:28:51 AM 
* File name：Anchor 
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
    public class Anchors : IEntity, IMacAddress, IId
    {
        public enum TANCHORTYPE
        {
            TANCHORTYPE_CHANGEROOM = 2,  //A7FE
            TANCHORTYPE_UPMINE = 1,
            TANCHORTYPE_UNDERMINE  =0,
        }

        public enum TANCHORCATECATEGORY
        {
            TANCHORCATECATEGORY_NONE = 0,
            TANCHORCATECATEGORY_FORBIDDEN = 1,
            TANCHORCATECATEGORY_RESTRICT = 1,
            TANCHORCATECATEGORY_IMMPORTANT = 3,
        }

        [PropertyType(IsPrimaryKey = true)]

        public int AnchorId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int Status
        {
            get;
            set;
        }

        public string MacAddress
        {
            get;
            set;
        }

        public int? RegionId
        {
            get;
            set;
        }

        public int NetworkType
        {
            get;
            set;
        }

        public string Placement
        {
            get;
            set;
        }

        public int AnchorType    // 1 井口 0 井下
        {
            get;
            set;
        }

        public int IsValid
        {
            get;
            set;
        }

        public int Show
        {
            get;
            set;
        }

        public double X
        {
            get;
            set;
        }

        public double Y
        {
            get;
            set;
        }

        public double? Z
        {
            get;
            set;
        }

        public int? BatteryState
        {
            get;
            set;
        }

        public DateTime? PowerLowTime
        {
            get;
            set;
        }

        public int? PerNum
        {
            get;
            set;
        }

        public int? AnchorCategory
        {
            get;
            set;
        }

        public bool IsDeleted
        {
            get;
            set;
        }

    }
}
