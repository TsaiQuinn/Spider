#region ----------------备注----------------
// Author:CK 
// FileName:ICarModelBusiness.cs 
// Create Date:2017-09-01
// Create Time:17:52 
#endregion

using System;
using System.Collections.Generic;
using SpiderModel.Models;

namespace SpiderIBusiness.INhibernateBusiness
{
    public interface ICarModelBusiness
    {
        /// <summary>
        /// 数据操作通知
        /// </summary>
        event EventHandler<ViewModelEventArg> ShowInfoEvent;

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns>返回ID</returns>
        int Insert(CarModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model">实体对象</param>
        void Delete(CarModel model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model">实体对象</param>
        bool Update(CarModel model);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回对象</returns>
        CarBrand FindBy(CarModel id);

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>返回列表</returns>
        IList<CarModel> QueryList(Func<CarModel, bool> expression);

        /// <summary>  
        /// 批量保存更新实体类  
        /// </summary>  
        /// <param name="list"></param>  
        /// <returns></returns>  
        bool SaveOrUpdateList(IList<CarModel> list);
    }
}