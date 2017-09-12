#region ----------------备注----------------

// Author:CK 
// FileName:CarBrandBusiness.cs 
// Create Date:2017-09-06
// Create Time:16:19 

#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Ninject;
using SpiderDataAccess.DapperDataAccess;
using SpiderIBusiness.IDapperBusiness;
using SpiderIDataAccess.IDapperDataAccess;
using SpiderModel;
using SpiderModel.Entity;

namespace SpiderBusiness.DapperBusiness
{
    public class CarBrandBusiness : CarBusiness<CarBrandEntity>, ICarBrandBusiness
    {
        public CarBrandBusiness()
        {
            this.CarDataAccess = new CarBrandDataAccess();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="car">实体对象</param>
        /// <returns></returns>
        public override int Insert(CarBrandEntity car)
        {
            int id = 0;
            var list = this.QueryList(car);
            if (list.Any())
            {
                car.BrandLogo = list[0].BrandLogo;
                this.Update(car);
                id = list[0].Id;
            }
            else
            {
                id = CarDataAccess.Insert(
                    "INSERT INTO hengtu_carbrand(rid,url,brand,logo,tagname,add_time)VALUES(@Rid,@Url,@BrandName,@BrandLogo,@TagName,@AddTime)",
                    new
                    {
                        car.Rid,
                        car.Url,
                        car.BrandName,
                        car.BrandLogo,
                        car.AddTime
                    });
            }
            ShowInfoEventHandler?.Invoke(this, new ViewModelArg<CarBrandEntity> {Car = car, Type = list.Any() ? 0 : 1});
            return car.Id=id;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="car">实体对象</param>
        /// <returns></returns>
        public override bool Delete(CarBrandEntity car)
        {
            return CarDataAccess.Delete(id: car.Id);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="car">实体对象</param>
        /// <returns></returns>
        public override bool Update(CarBrandEntity car)
        {
            return CarDataAccess.Update(
                "UPDATE hengtu_carbrand SET brand=@BrandName,logo=@BrandLogo,add_time=@AddTime WHERE rid=@Rid AND url=@Url AND tagname=@TagName",
                new {car.BrandName, car.BrandLogo, car.AddTime, car.Rid, car.Url, car.TagName});
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回对象</returns>
        public override CarBrandEntity FindBy(object id)
        {
            return CarDataAccess.FindBy(
                "SELECT id,rid,url,brand,logo,tagname,add_time WHERE id=@id", id, null);
        }

        /// <summary>
        ///     查询列表
        /// </summary> 
        /// <param name="car">实体对象</param>
        /// <returns>返回列表</returns>
        public override IList<CarBrandEntity> QueryList(CarBrandEntity car)
        {
            return CarDataAccess.QueryList(
                "SELECT id,rid,url,brand AS BrandName,logo AS BrandLogo,tagname,add_time AS AddTime FROM hengtu_carbrand WHERE rid=@Rid AND url=@Url AND tagname=@TagName",
                new {car.Rid, car.Url, car.TagName}, null);
        }

        public new event EventHandler<ViewModelArg<CarBrandEntity>> ShowInfoEventHandler;
    }
}