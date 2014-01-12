#region Author & Version
/* ======================================================================== 
* 
* Author：guoshuangquan Time：2013/1/22 8:51:18 
* File name：Users 
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
using DBFramework.Entities.Attribute;

namespace DBFramework.Entities
{
    public class Users : IEntity, IId
    {
        [PropertyType(IsPrimaryKey = true)]

        public int UserId { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string LoginName { get; set; }
        public string HashPassword { get; set; }
        public bool IsDeleted { get; set; }
    }
}
