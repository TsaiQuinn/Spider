#region ----------------备注----------------
// Author:CK 
// FileName:CarModelBusiness.cs 
// Create Date:2017-09-06
// Create Time:16:21 
#endregion

using SpiderIBusiness.IDapperBusiness;
using SpiderModel.Entity;

namespace SpiderBusiness.DapperBusiness
{
    public class CarModelBusiness : CarBusiness<CarModelEntity>, ICarModelBusiness
    {
        public CarModelBusiness()
        {
        }
    }
}