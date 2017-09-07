#region ----------------备注----------------

// Author:CK 
// FileName:CarBrandDataAccess.cs 
// Create Date:2017-09-05
// Create Time:17:31 

#endregion

using DapperExtensions;
using SpiderIDataAccess.IDapperDataAccess;
using SpiderModel.Entity;
using System;
using System.Data;

namespace SpiderDataAccess.DapperDataAccess
{
    public class CarBrandDataAccess : DapperDataAccess<CarBrandEntity>, ICarBrandDataAccess
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="car">实体对象</param>
        /// <returns></returns>
        public bool Delete(SpiderModel.Entity.CarBrandEntity car)
        {
            bool result = false;
            base.OpenConnection(connection =>
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var predicate =
                            Predicates.Field<SpiderModel.Entity.CarModelEntity>(entity => entity.BrandId, Operator.Eq, car.Id);
                        var models = connection.GetList<SpiderModel.Entity.CarModelEntity>(predicate, null, transaction);
                        foreach (var carModel in models)
                        {
                            var predicates =
                                Predicates.Field<CarSeriesEntity>(entity => entity.ModelId, Operator.Eq, carModel.Id);
                            connection.Delete<CarSeriesEntity>(predicates, transaction);
                            connection.Delete(carModel, transaction);
                            
                        }
                        connection.Delete(car, transaction);
                        transaction.Commit();
                        result = true;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        Console.WriteLine(e);
                        throw;
                    } 
                }
            });
            return result;
        } 
    }
}