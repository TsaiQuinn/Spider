#region ----------------备注----------------
// Author:CK 
// FileName:CarBusiness.cs 
// Create Date:2017-09-07
// Create Time:15:41 
#endregion

using Ninject;
using SpiderIBusiness.INhibernateBusiness;
using SpiderIDataAccess.INhibernateDataAccess;
using SpiderModel;
using System;
using System.Collections.Generic;
using SpiderDataAccess.NhibernateDataAccess;

namespace SpiderBusiness.NhibernateBusiness
{
    public class CarBusiness<T> : ICarBusiness<T> where T : class
    {
        /// <summary>初始化 <see cref="T:System.Object" /> 类的新实例。</summary>
        public CarBusiness()
        {
            this.CarDataAccess=new BaseDataAccess<T>();
        }

        [Inject]
        public virtual ICarDataAccess<T> CarDataAccess { get; set; }

        /// <summary>
        /// 数据操作通知
        /// </summary> 
        public event EventHandler<ViewModelArg<T>> ShowInfoEventHandler;

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="car">实体对象</param>
        /// <returns></returns>
        public int Insert(T car)
        {
            return CarDataAccess.Insert(car);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="car">实体对象</param>
        /// <returns></returns>
        public bool Delete(T car)
        {
            return CarDataAccess.Delete(car);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="car">实体对象</param>
        /// <returns></returns>
        public bool Update(T car)
        {
            return CarDataAccess.Update(car);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回对象</returns>
        public T FindBy(object id)
        {
            return CarDataAccess.FindBy(id);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="expression">表达式</param> 
        /// <returns>返回列表</returns>
        public IList<T> QueryList(Func<T, bool> expression)
        {
            return CarDataAccess.QueryList(expression);
        }
    }
}