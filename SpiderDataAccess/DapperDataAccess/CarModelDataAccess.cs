#region ----------------备注----------------

// Author:CK 
// FileName:CarModelDataAccess.cs 
// Create Date:2017-09-06
// Create Time:11:05 

#endregion

using System;
using System.Linq;
using Dapper;
using SpiderIDataAccess.IDapperDataAccess;
using SpiderModel.Entity;

namespace SpiderDataAccess.DapperDataAccess
{
    public class CarModelDataAccess : DapperDataAccess<CarModelEntity>, ICarModelDataAccess
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
                var transaction = connection.BeginTransaction();
                try
                {
                    connection.Execute("DELETE FROM hengtu_carmodel WHERE id=@Id", id, transaction);
                    connection.Execute("DELETE FROM hengtu_carmodeldetail WHERE modelid=@Id", id, transaction);
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    result = false;
                    transaction.Rollback();
                    Console.WriteLine(e);
                    throw;
                }
            });
            return result;
        }

        /// <summary>
        ///     删除
        /// </summary> 
        /// <param name="id">ID</param>
        /// <returns></returns>
        bool ICarDataAccess<CarModelEntity>.Delete(int id)
        {
            return Delete(id);
        }
    }
}