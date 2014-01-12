#region Author & Version
/* ======================================================================== 
* 
* Author：sunjianwen Time：9/12/2012 2:19:19 PM 
* File name：Asset 
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
    public class Assets : IEntity, IId
    {
        public enum TSTATUS
        {
            TSTATUS_ONLINE = 1,
            TSTATUS_ALARM =2,
            TSTATUS_FORBIDDEN = 3,
            TSTATUS_OFFLINE = 8,
            TSTATUS_OUTMINE = 255
        }

        [PropertyType(IsPrimaryKey = true)]

        public int AssetId { get; set; }

        public int Type { get; set; } // 0 = person, 1 = equipment

        public string First { get; set; }

        public string Last { get; set; }

        public string Gender { get; set; }

        public string IC { get; set; } // person = Ident Card #, equipment = equipment ID

        public DateTime? BirthDate { get; set; } // equipment = purchase date

        public string Photo { get; set; } // url to a photo

        public string TagName { get; set; }

        public string TagMacAddress { get; set; }

        public string Name { get; set; }

        public int? BatteryState { get; set; } //  1 欠压

        public DateTime? PowerLowTime { get; set; }

        public DateTime? InTime { get; set; }

        public DateTime? OutTime { get; set; }

        public DateTime? InMineTime { get; set; }

        public DateTime? OutMineTime { get; set; }

        public double? Distance { get; set; }

        public double? GasDensity { get; set; }

        public int? State { get; set; } // 1 在线 2 报警 3 禁区 8 离线  255 出井

        public string ClassTypeId { get; set; }

        public DateTime? AddTime { get; set; }

        public int? IsValid { get; set; }

        public int? Show { get; set; }

        public int? RegionId { get; set; }

        public int? AnchorId { get; set; }

        public int? DepartmentId { get; set; }

        public int JobFunctionId { get; set; }

        public int TagId { get; set; }

        public double? X { get; set; }

        public double? Y { get; set; }

        public double? Z { get; set; }

        public DateTime? LastInMineTime { get; set; }

        public DateTime? LastOutMineTime { get; set; }

        public bool IsDeleted { get; set; }


    }
}
