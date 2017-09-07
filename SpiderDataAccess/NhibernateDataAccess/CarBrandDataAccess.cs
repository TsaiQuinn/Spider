#region ----------------备注----------------

// Author:TQ 
// FileName:CarBrandDataAccess.cs 
// Create Date:2017-06-14
// Create Time:14:33 

#endregion

using SpiderIDataAccess.INhibernateDataAccess;
using SpiderModel.Models;

namespace SpiderDataAccess.NhibernateDataAccess
{
    public class CarBrandDataAccess : BaseDataAccess<CarBrandEntity>, ICarBrandDataAccess
    { 
    }
}