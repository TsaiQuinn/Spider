#region ----------------备注----------------
// Author:CK 
// FileName:CarBrandBusiness.cs 
// Create Date:2017-09-06
// Create Time:16:19 
#endregion

using SpiderIBusiness.IDapperBusiness;
using SpiderIDataAccess.IDapperDataAccess;
using SpiderModel.Entity;

namespace SpiderBusiness.DapperBusiness
{
    public class CarBrandBusiness:CarBusiness<CarBrandEntity>,ICarBrandBusiness
    {
        public CarBrandBusiness()
        {
        }
    }
}