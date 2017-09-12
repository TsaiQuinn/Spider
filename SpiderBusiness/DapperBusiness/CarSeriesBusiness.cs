#region ----------------备注----------------

// Author:CK 
// FileName:CarSeriesBusiness.cs 
// Create Date:2017-09-06
// Create Time:16:21 

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using SpiderDataAccess.DapperDataAccess;
using SpiderIBusiness.IDapperBusiness;
using SpiderModel;
using SpiderModel.Entity;

namespace SpiderBusiness.DapperBusiness
{
    public class CarSeriesBusiness : CarBusiness<CarSeriesEntity>, ICarSeriesBusiness
    {
        public CarSeriesBusiness()
        {
            this.CarDataAccess = new CarSeriesDataAccess();
        }

        public new event EventHandler<ViewModelArg<CarSeriesEntity>> ShowInfoEventHandler;

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="car">实体对象</param>
        /// <returns></returns>
        public override int Insert(CarSeriesEntity car)
        {
            int id = 0;
            var list = this.QueryList(car);
            if (list.Any())
            { 
                this.Update(car);
                id = list[0].Id;
            }
            else
            {
                id = CarDataAccess.Insert(
                    "INSERT INTO hengtu_carmodeldetail(modelid,model,linkurl,reference_price,full_payment_reference,msrp,add_time)" +
                    "VALUES(@ModelId,@SeriesName,@LinkUrl,@ReferencePrice,@FullPaymentReference,@Msrp,@AddTime)",
                    new
                    {
                        car.ModelId,
                        car.SeriesName,
                        car.LinkUrl,
                        car.ReferencePrice, 
                        car.FullPaymentReference,
                        car.Msrp,
                        DateTime.Now, 
                    });
            }
            ShowInfoEventHandler?.Invoke(this, new ViewModelArg<CarSeriesEntity> {Car = car, Type = list.Any() ? 0 : 1});
            return car.Id = id;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="car">实体对象</param>
        /// <returns></returns>
        public override bool Delete(CarSeriesEntity car)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="car">实体对象</param>
        /// <returns></returns>
        public override bool Update(CarSeriesEntity car)
        {
            return CarDataAccess.Update(
                "UPDATE hengtu_carmodeldetail SET model=@SeriesName WHERE modelid=@ModelId AND linkurl=@LinkUrl", new
                {
                    car.SeriesName, 
                    car.ModelId,
                    car.LinkUrl
                });
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回对象</returns>
        public override CarSeriesEntity FindBy(object id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        ///     查询列表
        /// </summary> 
        /// <param name="car">实体对象</param>
        /// <returns>返回列表</returns>
        public override IList<CarSeriesEntity> QueryList(CarSeriesEntity car)
        {
            return CarDataAccess.QueryList(
                "SELECT id,modelid AS ModelId,model AS SeriesName,linkurl AS LinkUrl,reference_price AS ReferencePrice,full_payment_reference AS FullPaymentReference,msrp AS Msrp,fuel_consumption AS FullPaymentReference,displacement AS Displacement,engine AS Engine,gearbox AS Gearbox,add_time AS AddTime FROM hengtu_carmodeldetail WHERE modelid=@ModelId AND linkurl=@LinkUrl",
                new {car.ModelId, car.LinkUrl}, null);
        }
    }
}