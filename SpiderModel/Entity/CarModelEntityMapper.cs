#region ----------------备注----------------
// Author:CK 
// FileName:CarModelEntityMapper.cs 
// Create Date:2017-09-06
// Create Time:17:04 
#endregion

using System;
using DapperExtensions.Mapper;

namespace SpiderModel.Entity
{
    [Serializable]
    public class CarModelEntityMapper : ClassMapper<CarModelEntity>
    {
        public CarModelEntityMapper()
        {
            base.Table("hengtu_carmodel");
            Map(f => f.Id).Key(KeyType.Identity);
            // ReSharper disable once VirtualMemberCallInConstructor
            AutoMap();
        }
    }
}