#region ----------------备注----------------

// Author:CK 
// FileName:ICarBrandDataAccess.cs 
// Create Date:2017-09-06
// Create Time:10:48 

#endregion

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SpiderModel.Entity; 

namespace SpiderIDataAccess.IDapperDataAccess
{
    public interface ICarBrandDataAccess
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="brand">品牌</param>
        /// <returns></returns>
        int Insert(SpiderModel.Entity.CarBrandEntity brand);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="brand">品牌</param>
        /// <returns></returns>
        bool Delete(SpiderModel.Entity.CarBrandEntity brand);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="brand">品牌</param>
        /// <returns></returns>
        bool Update(SpiderModel.Entity.CarBrandEntity brand);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回对象</returns>
        CarBrandEntity FindBy(object id);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回列表</returns>
        IList<SpiderModel.Entity.CarBrandEntity> QueryList(Dictionary<Expression<Func<SpiderModel.Entity.CarBrandEntity, object>>, object> expression, object parameters);
    }
}