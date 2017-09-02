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
        /// 数据操作通知
        /// </summary>  
        public event EventHandler<ViewModelEventArg> ShowInfoEvent;

        /// <summary>
        ///     新增
        /// </summary>
        /// <param name="modelCarBrand">实体对象</param>
        /// <returns>返回ID</returns>
        public int Insert(CarBrand modelCarBrand)
        {
            int result = 0;
            var brands = this.QueryList(e => e.TagName == modelCarBrand.TagName && e.Rid == modelCarBrand.Rid &&
                                             e.Url == modelCarBrand.Url);
            if (brands.Count == 1)
            {
                brands[0].BrandLogo = modelCarBrand.BrandLogo;
                brands[0].BrandName = modelCarBrand.BrandName;
                brands[0].AddTime = DateTime.Now;
                this.Update(brands[0]);
                if (brands[0].Id != null) result = brands[0].Id.Value;
            }
            else
            {
                result = CarBrandDataAccess.Insert(modelCarBrand);
            }
            ShowInfoEvent?.Invoke(this, new ViewModelEventArg
            {
                Brand = brands.Count > 0 ? brands[0] : modelCarBrand,
                Type = brands.Count > 0 ? 0 : 1
            });
            return result;
        }

        public void Delete(CarBrand model)
        {
            throw new NotImplementedException();
        }

        public bool Update(CarBrand model)
        {
            return CarBrandDataAccess.Update(model);
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

        /// <summary>
        /// 批量保存
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool SaveOrUpdateList(IList<CarBrand> list)
        {
            if (list.Count > 0)
            {
                foreach (var brand in list)
                {
                    Insert(brand);
                }
            }
            return true;
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