using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBFramework.Entities.Attribute;

namespace DBFramework.Entities
{
    public class DXAdmin : IEntity, IId
    {
        [PropertyType(IsPrimaryKey = true)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Pwd { get; set; }
        public DateTime? LoginTime { get; set; }
        public bool IsDeleted { get; set; }
        public bool isAdmin { get; set; }
    }
}