#region ----------------备注----------------

// Author:CK 
// FileName:CarBusiness.cs 
// Create Date:2017-09-06
// Create Time:14:11 

#endregion

using Ninject;
using SpiderIBusiness.IDapperBusiness;
using SpiderIDataAccess.IDapperDataAccess;
using SpiderModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace SpiderBusiness.DapperBusiness
{
    public abstract class CarBusiness<T> : ICarBusiness<T> where T : class
    {
//        [Inject]
        public ICarDataAccess<T> CarDataAccess { get; set; } 

        /// <summary>
        /// 数据操作通知
        /// </summary>
        public event EventHandler<ViewModelArg<T>> ShowInfoEventHandler;

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="car">实体对象</param>
        /// <returns></returns>
        public abstract int Insert(T car);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="car">实体对象</param>
        /// <returns></returns>
        public abstract bool Delete(T car);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="car">实体对象</param>
        /// <returns></returns>
        public abstract bool Update(T car);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回对象</returns>
        public abstract T FindBy(object id);

        /// <summary>
        ///     查询列表
        /// </summary> 
        /// <param name="car">实体对象</param>
        /// <returns>返回列表</returns>
        public abstract IList<T> QueryList(T car);
    }
}