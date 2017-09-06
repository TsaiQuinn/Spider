#region ----------------备注----------------
// Author:CK 
// FileName:CarSeriesEntityMapper.cs 
// Create Date:2017-09-06
// Create Time:17:05 
#endregion

using System;
using DapperExtensions.Mapper;

namespace SpiderModel.Entity
{
    [Serializable]
    public class CarSeriesEntityMapper : ClassMapper<CarSeriesEntity>
    {
        public CarSeriesEntityMapper()
        {
            base.Table("hengtu_carmodeldetail");
            Map(f => f.Id).Key(KeyType.Identity);
            // ReSharper disable once VirtualMemberCallInConstructor
            AutoMap();
        }
    }
}