#region ----------------备注----------------
// Author:CK 
// FileName:CarBrandEntityMapper.cs 
// Create Date:2017-09-05
// Create Time:17:36 
#endregion

using System;
using DapperExtensions.Mapper;

namespace SpiderModel.Entity
{
    [Serializable]
    public class CarBrandEntityMapper:ClassMapper<CarBrandEntity>
    {
        public CarBrandEntityMapper()
        {
            base.Table("hengtu_carbrand"); 
            Map(f => f.Id).Column("id").Key(KeyType.Identity);
            Map(f => f.Rid).Column("rid");
            Map(f => f.Url).Column("url");
            Map(f => f.BrandName).Column("brand");
            Map(f => f.BrandLogo).Column("logo");
            Map(f => f.TagName).Column("tagname");
            Map(f => f.AddTime).Column("add_time"); 
            // ReSharper disable once VirtualMemberCallInConstructor
            AutoMap();
        }
    }
}