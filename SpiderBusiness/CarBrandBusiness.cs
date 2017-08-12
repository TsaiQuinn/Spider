#region ----------------备注----------------

// Author:TQ 
// FileName:CarBrandBusiness.cs 
// Create Date:2017-06-14
// Create Time:14:34 

#endregion

using System;
using System.Collections.Generic;
using Ninject;
using SpiderIBusiness;
using SpiderIDataAccess;
using SpiderModel.Models;

namespace SpiderBusiness
{
    public class CarBrandBusiness : ICarBrandBusiness
    {
        //        [Inject] 
        public ICarBrandDataAccess CarBrandDataAccess { get; set; }

        /// <summary>
        ///     新增
        /// </summary>
        /// <param name="modelCarBrand">实体对象</param>
        /// <returns>返回ID</returns>
        public int Insert(CarBrand modelCarBrand)
        {
            return CarBrandDataAccess.Insert(modelCarBrand);
        }

        public void Delete(CarBrand model)
        {
            throw new NotImplementedException();
        }

        public void Update(CarBrand model)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     查询列表
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>返回列表</returns>
        public IList<CarBrand> QueryList(Func<CarBrand, bool> expression)
        {
            return CarBrandDataAccess.QueryList(expression);
        }

        public bool SaveOrUpdateList(IList<CarBrand> list)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     查询
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回对象</returns>
        public CarBrand FindBy(object id)
        {
            return CarBrandDataAccess.FindBy(id);
        }
    }
}