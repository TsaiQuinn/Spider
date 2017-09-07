#region ----------------备注----------------

// Author:TQ 
// FileName:CarBrandBusiness.cs 
// Create Date:2017-06-14
// Create Time:14:34 

#endregion

using SpiderIBusiness.INhibernateBusiness;
using SpiderModel.Models;

namespace SpiderBusiness.NhibernateBusiness
{
    public class CarBrandBusiness :CarBusiness<CarBrandEntity>, ICarBrandBusiness
    {
         
    }
}