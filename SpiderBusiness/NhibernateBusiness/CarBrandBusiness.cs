#region ----------------备注----------------

// Author:TQ 
// FileName:CarBrandBusiness.cs 
// Create Date:2017-06-14
// Create Time:14:34 

#endregion

using SpiderDataAccess.NhibernateDataAccess;
using SpiderIBusiness.INhibernateBusiness;
using SpiderModel.Models;

namespace SpiderBusiness.NhibernateBusiness
{
    public class CarBrandBusiness :CarBusiness<CarBrandEntity>, ICarBrandBusiness
    {

        /// <summary>初始化 <see cref="T:System.Object" /> 类的新实例。</summary>
        public CarBrandBusiness()
        {
            this.CarDataAccess = new CarBrandDataAccess();
        }
    }
}