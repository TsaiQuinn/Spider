#region ----------------备注----------------

// Author:CK 
// FileName:CarModelBusiness.cs 
// Create Date:2017-09-06
// Create Time:16:21 

#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using SpiderDataAccess.DapperDataAccess;
using SpiderIBusiness.IDapperBusiness;
using SpiderModel;
using SpiderModel.Entity;

namespace SpiderBusiness.DapperBusiness
{
    public class CarModelBusiness : CarBusiness<CarModelEntity>, ICarModelBusiness
    {
        public CarModelBusiness()
        {
            this.CarDataAccess = new CarModelDataAccess();
        }

        public new event EventHandler<ViewModelArg<CarModelEntity>> ShowInfoEventHandler;

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="car">实体对象</param>
        /// <returns></returns>
        public override int Insert(CarModelEntity car)
        {
            int id = 0;
            var list = this.QueryList(car);
            if (list.Any())
            {
                car.ModelName = list[0].ModelName; 
                this.Update(car);
                id = list[0].Id;
            }
            else
            {
                id = CarDataAccess.Insert(
                    "INSERT INTO hengtu_carmodel(brandid,model,linkurl,factory,image,add_time,national_reference_price,msrp,second_hand,displacement,fuel_consumption,gearbox,warranty)" +
                    "VALUES(@BrandId,@ModelName,@LinkUrl,@Factory,@ImagePath,@AddTime,@NationalReferencePrice,@Msrp,@SecondHand,@Displacement,@FuelConsumption,@Gearbox,@Warranty)",
                    new
                    {
                        car.BrandId,
                        car.ModelName,
                        car.LinkUrl,
                        car.Factory,
                        car.ImagePath,
                        car.AddTime,
                        car.NationalReferencePrice,
                        car.Msrp,
                        car.SecondHand,
                        car.Displacement,
                        car.FuelConsumption,
                        car.Gearbox,
                        car.Warranty
                    });
            }
            ShowInfoEventHandler?.Invoke(this, new ViewModelArg<CarModelEntity> {Car = car, Type = list.Any() ? 0 : 1});
            return car.Id = id;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="car">实体对象</param>
        /// <returns></returns>
        public override bool Delete(CarModelEntity car)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="car">实体对象</param>
        /// <returns></returns>
        public override bool Update(CarModelEntity car)
        {
            return CarDataAccess.Update(
                "UPDATE hengtu_carmodel SET model=@ModelName,factory=@Factory,add_time=@AddTime,image=@ImagePath,national_reference_price=@NationalReferencePrice,msrp=@Msrp,second_hand=@SecondHand,fuel_consumption=@FuelConsumption WHERE brandid=@BrandId AND linkurl=@LinkUrl",
                new
                {
                    car.ModelName,
                    car.Factory,
                    car.AddTime,
                    car.ImagePath,
                    car.NationalReferencePrice,
                    car.Msrp,
                    car.SecondHand, 
                    car.FuelConsumption, 
                    car.BrandId,
                    car.LinkUrl
                });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回对象</returns>
        public override CarModelEntity FindBy(object id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     查询列表
        /// </summary> 
        /// <param name="car">实体对象</param>
        /// <returns>返回列表</returns>
        public override IList<CarModelEntity> QueryList(CarModelEntity car)
        {
            return CarDataAccess.QueryList(
                "SELECT id,brandid AS BrandId,model AS ModelName, linkurl AS LinkUrl,factory,image AS ImagePath,add_time AS AddTime,national_reference_price AS NationalReferencePrice,msrp,second_hand AS SecondHand,displacement AS Displacement,fuel_consumption AS FuelConsumption,gearbox AS Gearbox,warranty AS Warranty,color_link AS ColorLink FROM hengtu_carmodel WHERE brandid=@BrandId AND linkurl=@LinkUrl",
                new {car.BrandId, car.LinkUrl}, null);
        }
    }
}