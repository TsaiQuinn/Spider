#region ----------------备注----------------
// Author:CK 
// FileName:ICarBusiness.cs 
// Create Date:2017-09-07
// Create Time:15:41 
#endregion

using SpiderModel;
using System;
using System.Collections.Generic;

namespace SpiderIBusiness.INhibernateBusiness
{
    public interface ICarBusiness<T> where T : class
    {
        /// <summary>
        /// 数据操作通知
        /// </summary> 
        event EventHandler<ViewModelArg<T>> ShowInfoEventHandler;

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
        /// <returns>返回列表</returns>
        IList<T> QueryList(Func<T, bool> expression);
    }
}