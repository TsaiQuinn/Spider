using System;
using System.Windows.Forms;
using Autofac;
using Autofac.Configuration;
using DevExpress.Skins;
using DevExpress.UserSkins;
using Microsoft.Extensions.Configuration;
using Ninject;
using SpiderBusiness;
using SpiderDataAccess;
using SpiderIBusiness;
using SpiderIDataAccess;
using SpiderIView;
using SpiderPresenters;

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

            #region Autofac 采用配置文件或者代码的方式进行注入

            var builder = new ContainerBuilder();

            #region 配置文件方式

            var config = new ConfigurationBuilder();
            config.AddXmlFile("config/autofac_ioc.xml", false);
            var module = new ConfigurationModule(config.Build());
            builder.RegisterModule(module);
            builder.RegisterType<CrawlPresenter>().As<CrawlPresenter>();
            #endregion

            #region 代码方式

            //            builder.RegisterType<CrawlForm>().As<ICrawlView>().InstancePerLifetimeScope();
            //            builder.RegisterType<CarBrandBusiness>().As<ICarBrandBusiness>().PropertiesAutowired().SingleInstance();
            //            builder.RegisterType<CarBrandDataAccess>().As<ICarBrandDataAccess>().SingleInstance();
            //            builder.RegisterType<CrawlPresenter>().PropertiesAutowired();

            #endregion

            var container = builder.Build();
            var form = container.Resolve<CrawlPresenter>();

            #endregion


            #region Ninject 采用配置文件或者代码的方式进行注入

            //            IKernel kernel = new StandardKernel();

            #region 配置文件方式

            //            kernel.Load("config/ninject_ioc.xml");

            #endregion

            #region 代码方式加载

            //            kernel.Bind<ICrawlView>().To<CrawlForm>();
            //            kernel.Bind<ICarBrandBusiness>().To<CarBrandBusiness>();
            //            kernel.Bind<ICarBrandDataAccess>().To<CarBrandDataAccess>();
            //            kernel.Bind<CrawlPresenter>().ToSelf();

            #endregion

            //            var form = kernel.Get<CrawlPresenter>();

            #endregion

            Application.Run(form.View as Form);
        }
    }
}