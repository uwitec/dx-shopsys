using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBFramework.Entities.Attribute;

namespace DBFramework.Entities
{
    public class DXLb : IEntity, IId
    {
        [PropertyType(IsPrimaryKey = true)]
        public int Id { get; set; }
        public string LbName { get; set; }
        public int ParentId { get; set; }
        public int OrderId { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

    }
}
