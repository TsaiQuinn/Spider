#region ----------------备注----------------

// Author:CK 
// FileName:CarBrandDataAccess.cs 
// Create Date:2017-09-05
// Create Time:17:31 

#endregion

using System;
using System.Data;
using System.Linq;
using Dapper;
using SpiderIDataAccess.IDapperDataAccess;
using SpiderModel.Entity;

namespace SpiderDataAccess.DapperDataAccess
{
    public class CarBrandDataAccess : DapperDataAccess<CarBrandEntity>, ICarBrandDataAccess
    {
        /// <summary>
        ///     删除
        /// </summary> 
        /// <param name="id">ID</param>
        /// <returns></returns>
        public override bool Delete(int id)
        {
            var result = true;
            base.OpenConnection(connection =>
            {
                var list = connection.Query<CarModelEntity>("SELECT id FROM hengtu_carmodel WHERE brandid=@Id", id)
                    .ToList();
                if (list.Any())
                {
                    var transaction = connection.BeginTransaction();
                    try
                    { 
                        foreach (var entity in list)
                        {
                            connection.Execute("DELETE FROM hengtu_carmodeldetail WHERE modelid=@Id", entity.Id, transaction);
                            connection.Execute("DELETE FROM hengtu_carmodel WHERE id=@Id", entity.Id, transaction);
                        }
                        connection.Execute("DELETE FROM hengtu_carbrand WHERE id=@Id", id,transaction);
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        result = false;
                        transaction.Rollback();
                        Console.WriteLine(e);
                        throw;
                    }
                }
                else
                {
                    result = base.Delete("DELETE FROM hengtu_carbrand WHERE id=@Id", id);
                }
            });
            return result;
        }

        /// <summary>
        ///     删除
        /// </summary> 
        /// <param name="id">ID</param>
        /// <returns></returns>
        bool ICarDataAccess<CarBrandEntity>.Delete(int id)
        {
            return Delete(id);
        }
    }
}