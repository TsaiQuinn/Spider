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
            Map(f => f.Id).Key(KeyType.Identity);
            // ReSharper disable once VirtualMemberCallInConstructor
            AutoMap();
        }
    }
}