#region ----------------备注----------------
// Author:CK 
// FileName:CarModelBusiness.cs 
// Create Date:2017-09-01
// Create Time:17:59 
#endregion

using System;
using System.Collections.Generic;
using SpiderIBusiness;
using SpiderIDataAccess;
using SpiderModel.Models;

namespace SpiderBusiness
{
    public class CarModelBusiness :ICarModelBusiness
    {
        public ICarBrandDataAccess CarBrandDataAccess { get; set; }
         
        public event EventHandler<ViewModelEventArg> ShowInfoEvent;

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns>返回ID</returns>
        public int Insert(CarModel model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model">实体对象</param>
        public void Delete(CarModel model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model">实体对象</param>
        public bool Update(CarModel model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回对象</returns>
        public CarBrand FindBy(CarModel id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>返回列表</returns>
        public IList<CarModel> QueryList(Func<CarModel, bool> expression)
        {
            throw new NotImplementedException();
        }

        /// <summary>  
        /// 批量保存更新实体类  
        /// </summary>  
        /// <param name="list"></param>  
        /// <returns></returns>  
        public bool SaveOrUpdateList(IList<CarModel> list)
        {
            throw new NotImplementedException();
        }
    }
}