#region ----------------备注----------------

// Author:CK 
// FileName:CarSeriesDataAccess.cs 
// Create Date:2017-09-06
// Create Time:11:09 

#endregion

using SpiderIDataAccess.IDapperDataAccess;
using SpiderModel.Entity;

namespace SpiderDataAccess.DapperDataAccess
{
    public class CarSeriesDataAccess : DapperDataAccess<CarSeriesEntity>, ICarSeriesDataAccess
    {
        /// <summary>
        ///     删除
        /// </summary> 
        /// <param name="id">ID</param>
        /// <returns></returns>
        public override bool Delete(int id)
        {
            return base.Delete("DELETE FROM hengtu_carmodeldetail WHERE modelid=@Id", id);
        }

        /// <summary>
        ///     删除
        /// </summary> 
        /// <param name="id">ID</param>
        /// <returns></returns>
        bool ICarDataAccess<CarSeriesEntity>.Delete(int id)
        {
            return Delete(id);
        }
    }
}