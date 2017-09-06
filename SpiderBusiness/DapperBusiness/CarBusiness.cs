#region ----------------备注----------------

// Author:CK 
// FileName:CarBusiness.cs 
// Create Date:2017-09-06
// Create Time:14:11 

#endregion

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using SpiderIBusiness.IDapperBusiness;
using SpiderIDataAccess;
using SpiderIDataAccess.IDapperDataAccess;
using SpiderModel.Entity;
using SpiderModel.Models;

namespace SpiderBusiness.DapperBusiness
{
    public class CarBusiness<T> : ICarBusiness<T> where T : class
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
        /// <param name="expression">表达式</param>
        /// <returns></returns>
        public int Insert(T car, Expression<Func<T, object>> expression)
        {
            int result = 0;
            if (expression == null)
            {
                result = CarDataAccess.Insert(car);
                ShowInfoEventHandler?.Invoke(this, new ViewModelArg<T>
                {
                    Car = car,
                    Type = 1
                });
            }
            else
            {
                var carsList = this.QueryList(expression, car);
                if (carsList.Count == 1)
                {
                    PropertyInfo info = typeof(T).GetProperty("Id");
                    result = info == null ? 0 : (int) info.GetValue(carsList[0]);
                }
                else
                {
                    result = CarDataAccess.Insert(car);
                }
                ShowInfoEventHandler?.Invoke(this, new ViewModelArg<T>
                {
                    Car = car,
                    Type = carsList.Count > 0 ? 0 : 1
                });
            }

            return result;
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
        /// <param name="parameters">参数</param>
        /// <returns>返回列表</returns>
        public IList<T> QueryList(Expression<Func<T, object>> expression, object parameters)
        {
            return CarDataAccess.QueryList(expression, parameters);
        }
    }
}