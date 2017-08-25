#region ----------------备注----------------

// Author:CK 
// FileName:CrawlPresenter.cs 
// Create Date:2017-08-02
// Create Time:14:48 

#endregion

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ninject;
using SpiderCommon;
using SpiderIBusiness;
using SpiderIView;
using SpiderModel.Models;

namespace SpiderPresenters
{
    public class CrawlPresenter : BasePresenter<ICrawlView>
    {
        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="view"></param>
        public CrawlPresenter(ICrawlView view) : base(view)
        {
        }

        //如果使用Inject则添加该属性
        //[Inject] 
        public ICarBrandBusiness CarBrandBusiness { get; set; }

        /// <summary>
        /// 窗体加载
        /// </summary>
        protected override void ViewLoad()
        {
            View.Load += OnViewLoad;
            View.PickBrandEvent += OnPickBrand;
            View.PickModelEvent += OnPickModel;   
        } 

        /// <summary>
        ///     采集品牌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPickBrand(object sender, EventArgs e)
        {
            string url = ConfigurationManager.AppSettings["BrandsUrl"];
            Task.Run(() =>
            {
                var task = SpiderRequest.DownloadString(url);
                task.Wait();
                string brandStr = task.Result.Substring(14, task.Result.Length - 15);
                JObject objJObject = (JObject)JsonConvert.DeserializeObject(brandStr);
                Dictionary<string, object> resultDictionary =
                    JsonConvert.DeserializeObject<Dictionary<string, object>>(objJObject["brand"].ToString());
                var definition = new[]
                {
                    new {type = string.Empty, id = 0, name = string.Empty, url = string.Empty, cur = string.Empty, num = 0}
                };
                IList<CarBrand> listBrands = (from valuePair in resultDictionary
                        let result = JsonConvert.DeserializeAnonymousType(valuePair.Value.ToString(), definition)
                        from brand in result
                        where !string.IsNullOrEmpty(brand.url)
                        let index = brand.url.IndexOf("mb_", StringComparison.Ordinal)
                        let temp = brand.url.Substring(index + 3, brand.url.Length - index - 4)
                        select new CarBrand
                        {
                            AddTime = DateTime.Now,
                            BrandName = brand.name,
                            TagName = valuePair.Key,
                            Url = brand.url,
                            Rid = brand.id
                        })
                    .ToList();
                if (listBrands.Count > 0)
                {
                    CarBrandBusiness.ShowInfoEvent += CarBrandBusiness_ShowDataEvent;
                    CarBrandBusiness.SaveOrUpdateList(listBrands);
                    CarBrandBusiness.ShowInfoEvent -= CarBrandBusiness_ShowDataEvent;
                }
                else
                {
                    View.ShowMessage("没有获取到品牌", "提示");
                }
            }); 
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarBrandBusiness_ShowDataEvent(object sender, BrandViewModelEventArg e)
        {
            View.ShowAction?.Invoke(e);
        }

        /// <summary>
        ///     加载视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnViewLoad(object sender, EventArgs e)
        {
//            var list = CarBrandBusiness.QueryList(null);
//            View.ShowMessage($"品牌数量为{list.Count}", "采集品牌"); 
        }

        /// <summary>
        ///     采集车型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPickModel(object sender, EventArgs e)
        {
        }
    }
}