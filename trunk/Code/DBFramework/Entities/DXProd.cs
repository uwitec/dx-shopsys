using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBFramework.Entities.Attribute;

namespace DBFramework.Entities
{
    public class DXProd : IEntity, IId
    {
        /*
 SELECT [Id]
      ,[Name]
      ,[Description]
      ,[Body]
      ,[Price]
      ,[Price2]
      ,[Price3]
      ,[CreateTime]
      ,[EditorName]
      ,[CreatorName]
      ,[EditTime]
      ,[IsDeleted]
  FROM [DXDB].[dbo].[dxProd]
         */
        [PropertyType(IsPrimaryKey = true)]
        public int Id { get; set; }
        public int lbid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public double? Price { get; set; }
        public double? Price2 { get; set; }
        public double? Price3 { get; set; }

        public DateTime? CreateTime { get; set; }
        public DateTime? EditTime { get; set; }
        public string CreatorName { get; set; }
        public string EditorName { get; set; }

        public bool IsDeleted { get; set; }

    }
}
