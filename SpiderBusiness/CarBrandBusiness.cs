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
        public event EventHandler<BrandViewModelEventArg> ShowInfoEvent;

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
                    brand.Url = SpiderCommon.SpiderConfig.GetConfigValue("Domain") +
                                brand.Url;
                    var brands = this.QueryList(e => e.TagName == brand.TagName && e.Rid == brand.Rid &&
                                                     e.Url == brand.Url);
                    if (brands.Count == 1)
                    {
                        brands[0].BrandName = brand.BrandName;
                        brands[0].AddTime = DateTime.Now;
                        this.Update(brands[0]);
                    }
                    else
                    {
                        this.Insert(brand);
                    }
                    ShowInfoEvent?.Invoke(this, new BrandViewModelEventArg
                    {
                        Brand = brands.Count > 0 ? brands[0] : brand,
                        Type = brands.Count > 0 ? 0 : 1
                    }); 
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