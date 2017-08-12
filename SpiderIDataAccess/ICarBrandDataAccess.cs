#region ----------------备注----------------
// Author:CK 
// FileName:ICarBrandDataAccess.cs 
// Create Date:2017-08-02
// Create Time:15:11 
#endregion

using System;
using System.Collections.Generic;
using SpiderModel.Models;

namespace SpiderIDataAccess
{
    public interface ICarBrandDataAccess
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns>返回ID</returns>
        int Insert(CarBrand model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model">实体对象</param>
        void Delete(CarBrand model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model">实体对象</param>
        void Update(CarBrand model);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回对象</returns>
        CarBrand FindBy(object id);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>返回列表</returns>
        IList<CarBrand> QueryList(Func<CarBrand, bool> expression);

        /// <summary>  
        /// 批量保存更新实体类  
        /// </summary>  
        /// <param name="list"></param>  
        /// <returns></returns>  
        bool SaveOrUpdateList(IList<CarBrand> list);
    }
}