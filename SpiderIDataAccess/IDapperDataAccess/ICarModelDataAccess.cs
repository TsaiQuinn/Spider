#region ----------------备注----------------
// Author:CK 
// FileName:ICarModelDataAccess.cs 
// Create Date:2017-09-06
// Create Time:11:06 
#endregion

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SpiderModel.Entity;
using SpiderModel.Models;

namespace SpiderIDataAccess.IDapperDataAccess
{
    public interface ICarModelDataAccess
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="brand">品牌</param>
        /// <returns></returns>
        int Insert(CarModelEntity brand);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="brand">品牌</param>
        /// <returns></returns>
        bool Delete(CarModelEntity brand);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="brand">品牌</param>
        /// <returns></returns>
        bool Update(CarModelEntity brand);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回对象</returns>
        CarModelEntity FindBy(object id);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回列表</returns>
        IList<CarModelEntity> QueryList(Dictionary<Expression<Func<CarModelEntity, object>>, object> expression, object parameters);
    }
}