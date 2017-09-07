#region ----------------备注----------------

// Author:CK 
// FileName:CarModelDataAccess.cs 
// Create Date:2017-09-06
// Create Time:11:05 

#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using DapperExtensions;
using SpiderIDataAccess.IDapperDataAccess;
using SpiderModel.Entity;
using SpiderModel.Models;

namespace SpiderDataAccess.DapperDataAccess
{
    public class CarModelDataAccess : DapperDataAccess<CarModelEntity>, ICarModelDataAccess
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="car">实体对象</param>
        /// <returns></returns>
        public bool Delete(CarModelEntity car)
        {
            bool result = false;
            base.OpenConnection(connection =>
            {
                using (IDbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var predicates =
                            Predicates.Field<CarSeriesEntity>(entity => entity.ModelId, Operator.Eq, car.Id);
                        connection.Delete<CarSeriesEntity>(predicates, transaction);
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