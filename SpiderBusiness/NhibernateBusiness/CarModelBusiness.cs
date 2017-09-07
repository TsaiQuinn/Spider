#region ----------------备注----------------
// Author:CK 
// FileName:CarModelBusiness.cs 
// Create Date:2017-09-01
// Create Time:17:59 
#endregion

using SpiderIBusiness.INhibernateBusiness;
using SpiderModel.Models;

namespace SpiderBusiness.NhibernateBusiness
{
    public class CarModelBusiness :CarBusiness<CarModelEntity>,ICarModelBusiness
    {
       
    }
}