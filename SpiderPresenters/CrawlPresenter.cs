#region ----------------备注----------------

// Author:CK 
// FileName:CrawlPresenter.cs 
// Create Date:2017-08-02
// Create Time:14:48 

#endregion

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ninject;
using SpiderCommon;
using SpiderIBusiness;
using SpiderIBusiness.IDapperBusiness;
using SpiderIView;
using SpiderModel.Entity;
using SpiderModel.Models;

namespace SpiderPresenters
{
    public class CrawlPresenter : BasePresenter<ICrawlView>
    {
        private readonly string _logoPath;

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="view"></param>
        public CrawlPresenter(ICrawlView view) : base(view)
        {
            this._logoPath = Directory.GetCurrentDirectory() + @"\image\logo\";
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
            View.PickBrandEventHandler += OnPickBrand;
            View.PickModelEventHandler += OnPickModel;
        }

        /// <summary>
        ///     采集品牌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPickBrand(object sender, EventArgs e)
        {
            string url = ConfigurationManager.AppSettings["BrandsUrl"];
            Task.Run(async () =>
            {
                #region 品牌 

                string task = await url.GetStringAsync();
                string brandStr = task.Substring(14, task.Length - 15);
                JObject objJObject = (JObject) JsonConvert.DeserializeObject(brandStr);
                Dictionary<string, object> resultDictionary =
                    JsonConvert.DeserializeObject<Dictionary<string, object>>(objJObject["brand"].ToString());
                var definition = new[]
                {
                    new
                    {
                        type = string.Empty,
                        id = 0,
                        name = string.Empty,
                        url = string.Empty,
                        cur = string.Empty,
                        num = 0
                    }
                };
                IList<CarBrandEntity> listBrands = (from valuePair in resultDictionary
                        let result = JsonConvert.DeserializeAnonymousType(valuePair.Value.ToString(), definition)
                        from brand in result
                        where !string.IsNullOrEmpty(brand.url)
                        let index = brand.url.IndexOf("mb_", StringComparison.Ordinal)
                        let temp = brand.url.Substring(index + 3, brand.url.Length - index - 4)
                        select new CarBrandEntity
                        {
                            AddTime = DateTime.Now,
                            BrandName = brand.name,
                            TagName = valuePair.Key,
                            Url = ConfigurationManager.AppSettings["Domain"] + brand.url,
                            Rid = brand.id
                        })
                    .ToList();

                #endregion

                if (listBrands.Count > 0)
                {
                    CarBrandBusiness.ShowInfoEventHandler += CarBrandBusiness_ShowInfoEventHandler;
//                    var parallelResult = Parallel.ForEach(listBrands, carBrand =>
//                    {
                    foreach (var carBrand in listBrands)
                    {
                        #region 并行处理

                        var replaceUrl = ConfigurationManager.AppSettings["LogoUrl"].ToString()
                            .Replace("{0}", carBrand.Rid.ToString());
                        SpiderFile.DownImage(replaceUrl, this._logoPath, "/Upload/logo/", dataBasePath =>
                        {
                            carBrand.BrandLogo = dataBasePath;
                            return CarBrandBusiness.Insert(carBrand,
                                entity => entity.TagName == carBrand.TagName && entity.Rid == carBrand.Rid &&
                                          entity.Url == carBrand.Url);
                        });


                        #region 车型 

                        var modelTask =
                            $"{ConfigurationManager.AppSettings["BrandsUrl"]}&pagetype=masterbrand&objid={carBrand.Rid}"
                                .GetStringAsync();
                        modelTask.Wait();
                        string modelStr = modelTask.Result.Substring(14, modelTask.Result.Length - 15);
                        JObject modelJObject = (JObject) JsonConvert.DeserializeObject(modelStr);
                        Dictionary<string, object> modelDictionary =
                            JsonConvert.DeserializeObject<Dictionary<string, object>>(modelJObject["brand"].ToString());
                        IList<CarModel> carModels = new List<CarModel>();
                        JArray array = JArray.FromObject(modelDictionary[carBrand.TagName]);
                        foreach (var token in array)
                        {
                            if (token["child"] != null)
                            {
                                JToken childArray = JToken.FromObject(token["child"]);
                                foreach (var child in childArray)
                                {
                                    if (child["child"] != null)
                                    {
                                        JToken childrenArray = JToken.FromObject(child["child"]);
                                        foreach (var model in childrenArray)
                                            carModels.Add(new CarModel
                                            {
                                                BrandId = carBrand.Id,
                                                ModelName = model["name"].ToString(),
                                                Factory = child["name"].ToString(),
                                                LinkUrl = $"{ConfigurationManager.AppSettings["Domain"]}{model["url"]}",
                                                AddTime = DateTime.Now,
                                            });
                                    }
                                    else
                                    {
                                        carModels.Add(new CarModel
                                        {
                                            BrandId = carBrand.Id,
                                            ModelName = child["name"].ToString(),
                                            Factory = carBrand.BrandName,
                                            LinkUrl = $"{ConfigurationManager.AppSettings["Domain"]}{child["url"]}",
                                            AddTime = DateTime.Now,
                                        });
                                    }
                                }
                                break;
                            }
                        }

                        #endregion

                        if (carModels.Count > 0)
                        {
                            Task.Run(() => { Parallel.ForEach(carModels, model => { }); });
                        }

                        #endregion
                    }


                    //                    });


                    //                    if (parallelResult.IsCompleted)
                    //                    {
                    CarBrandBusiness.ShowInfoEventHandler -= CarBrandBusiness_ShowInfoEventHandler;
//                    }
                }
                else
                {
                    View.ShowMessage("没有获取到品牌", "提示");
                }
            }).LogExceptions();
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarBrandBusiness_ShowInfoEventHandler(object sender, ViewModelArg<CarBrandEntity> e)
        {
            View.ShowInfoAction?.Invoke(new ViewModelArg<Car>
            {
                Car = e.Car,
                Type = e.Type
            });
        }

        /// <summary>
        ///     加载视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnViewLoad(object sender, EventArgs e)
        {
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