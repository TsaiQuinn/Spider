#region ----------------备注----------------
// Author:CK 
// FileName:CarSeriesBusiness.cs 
// Create Date:2017-09-06
// Create Time:16:21 
#endregion

using SpiderIBusiness.IDapperBusiness;
using SpiderModel.Entity;

namespace SpiderBusiness.DapperBusiness
{
    public class CarSeriesBusiness:CarBusiness<CarSeriesEntity>,ICarSeriesBusiness
    {
        public CarSeriesBusiness()
        {
        }
    }
}