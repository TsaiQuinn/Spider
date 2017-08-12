#region ----------------备注----------------

// Author:CK 
// FileName:NinjectModule .cs 
// Create Date:2017-08-05
// Create Time:17:58 

#endregion

using Ninject.Modules;

namespace SpiderCommon.Ioc
{
    public class AppNinjectModule : NinjectModule
    {
        public override void Load()
        {
//            Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>));
        }
    }
}