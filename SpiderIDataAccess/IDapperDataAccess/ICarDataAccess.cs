﻿#region ----------------备注----------------
// Author:CK 
// FileName:ICarDataAccess.cs 
// Create Date:2017-09-06
// Create Time:11:07 
#endregion

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SpiderIDataAccess.IDapperDataAccess
{
    public interface ICarDataAccess<T> where T:class 
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="car">实体对象</param>
        /// <returns></returns>
        int Insert(T car);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="car">实体对象</param>
        /// <returns></returns>
        bool Delete(T car);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="car">实体对象</param>
        /// <returns></returns>
        bool Update(T car);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回对象</returns>
        T FindBy(object id);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回列表</returns>
        IList<T> QueryList(Expression<Func<T, object>> expression, object parameters);
    }
}