#region ----------------备注----------------

// Author:CK 
// FileName:NinjectIoc.cs 
// Create Date:2017-08-05
// Create Time:17:54 

#endregion

using Ninject;
using Ninject.Modules;

namespace SpiderCommon.Ioc
{
    public class NinjectIoc
    {
        private static IKernel _kernel;

        public static void Ioc(INinjectModule module)
        {
            _kernel = new StandardKernel(module);
        }

        public static T Resolve<T>()
        {
            return _kernel.Get<T>();
        }
    }
}