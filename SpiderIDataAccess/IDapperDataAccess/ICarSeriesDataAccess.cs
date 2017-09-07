#region ----------------备注----------------
// Author:CK 
// FileName:ICarSeriesDataAccess.cs 
// Create Date:2017-09-07
// Create Time:9:21 
#endregion

using SpiderModel.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SpiderIDataAccess.IDapperDataAccess
{
    public interface ICarSeriesDataAccess
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="brand">品牌</param>
        /// <returns></returns>
        int Insert(CarSeriesEntity brand);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="brand">品牌</param>
        /// <returns></returns>
        bool Delete(CarSeriesEntity brand);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="brand">品牌</param>
        /// <returns></returns>
        bool Update(CarSeriesEntity brand);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回对象</returns>
        CarSeriesEntity FindBy(object id);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回列表</returns>
        IList<CarSeriesEntity> QueryList(Dictionary<Expression<Func<CarSeriesEntity, object>>, object> expression, object parameters);
    }
}