#region ----------------备注----------------

// Author:TQ 
// FileName:CarBrandDataAccess.cs 
// Create Date:2017-06-14
// Create Time:14:33 

#endregion

using System.Linq;
using SpiderIDataAccess.INhibernateDataAccess;
using SpiderModel.Models;

namespace SpiderDataAccess.NhibernateDataAccess
{
    public class CarBrandDataAccess : BaseDataAccess<CarBrandEntity>, ICarBrandDataAccess
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns>返回ID</returns>
        public override int Insert(CarBrandEntity model)
        {
            var list = this.QueryList(entity => entity.Rid == model.Rid && entity.TagName == model.TagName &&
                                                entity.Url == model.Url);
            if (list.Count > 0)
            {
                if (this.Update(model))
                {
                    return list[0].Id;
                }
                return 0;
            }
            return base.Insert(model);
        }
    }
}