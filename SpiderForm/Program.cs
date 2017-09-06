using System;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.UserSkins;
using Ninject;
using SpiderModel.Entity;
using SpiderPresenters;
using Spring.Context.Support;
using Spring.Objects.Factory;

namespace SpiderForm
{
    internal static class Program
    {
        /// <summary>
        ///     应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            #region Autofac 采用配置文件或者代码的方式进行注入，这种方式还有问题

            //            var builder = new ContainerBuilder();

            #region 配置文件方式

            //            var config = new ConfigurationBuilder();
            //            config.AddXmlFile("config/autofac_ioc.xml", false);
            //            var module = new ConfigurationModule(config.Build());
            //            builder.RegisterModule(module);
            //            builder.RegisterType<CrawlPresenter>().As<BasePresenter<CrawlForm>>();

            #endregion

            #region 代码方式

            //            builder.RegisterType<CrawlForm>().As<ICrawlView>().InstancePerLifetimeScope();
            //            builder.RegisterType<CarBrandBusiness>().As<ICarBrandBusiness>().PropertiesAutowired().SingleInstance();
            //            builder.RegisterType<CarBrandDataAccess>().As<ICarBrandDataAccess>().SingleInstance();
            //            builder.RegisterType<CrawlPresenter>().PropertiesAutowired();

            #endregion

            //            var container = builder.Build();
            //            var form = container.Resolve<CrawlPresenter>();

            #endregion

            #region Ninject 采用配置文件或者代码的方式进行注入

                        IKernel kernel = new StandardKernel();

            #region 配置文件方式

                        kernel.Load("config/ninject_ioc.xml");

            #endregion

            #region 代码方式加载

            //            kernel.Bind<ICrawlView>().To<CrawlForm>();
            //            kernel.Bind<ICarBrandBusiness>().To<CarBrandBusiness>();
            //            kernel.Bind<ICarBrandDataAccess>().To<CarBrandDataAccess>();
            //            kernel.Bind<CrawlPresenter>().ToSelf();

            #endregion

                        var form = kernel.Get<CrawlPresenter>();

            #endregion

            #region Spring.NET方式配置

//            var ctx = new XmlApplicationContext("config/spring_ioc.xml");
//            var objectFactory = (IObjectFactory) ctx;
//            var form = objectFactory.GetObject("CrawlPresenter") as CrawlPresenter;

            #endregion

            DapperExtensions.DapperExtensions.SetMappingAssemblies(new[]
            {
                typeof(CarBrandEntityMapper).Assembly,
                typeof(CarModelEntityMapper).Assembly,
                typeof(CarSeriesEntityMapper).Assembly
            });

            Application.Run(form?.View as Form);
        }
    }
}