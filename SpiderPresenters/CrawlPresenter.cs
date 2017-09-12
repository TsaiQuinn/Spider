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
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions; 
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ninject;
using SpiderBusiness.DapperBusiness;
using SpiderCommon;
using SpiderIBusiness;
using SpiderIBusiness.IDapperBusiness;
using SpiderIView;
using SpiderModel;
using SpiderModel.Entity;

namespace SpiderPresenters
{
    public class CrawlPresenter : BasePresenter<ICrawlView>
    {
        private readonly string _logoPath;
        private readonly string _modelPath;

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="view"></param>
        public CrawlPresenter(ICrawlView view) : base(view)
        {
            this._logoPath = Directory.GetCurrentDirectory() + @"\image\logo\";
            _modelPath = Directory.GetCurrentDirectory() + @"\image\model\";
            this.CarBrandBusiness = new CarBrandBusiness();
            this.CarModelBusiness = new CarModelBusiness();
            this.CarSeriesBusiness = new CarSeriesBusiness();
        }

        //如果使用Inject则添加该属性
//        [Inject]
        public ICarBusiness<CarBrandEntity> CarBrandBusiness { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public ICarBusiness<CarModelEntity> CarModelBusiness { get; set; }

        /// <summary>
        /// 车系
        /// </summary>
        public ICarBusiness<CarSeriesEntity> CarSeriesBusiness { get; set; }

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

                string task = await SpiderFile.GetStringAsync(url);
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
                    var parallelResult = Parallel.ForEach(listBrands, async carBrand =>
                    {
                        //                    foreach (var carBrand in listBrands)
                        //                    {

                        #region 并行处理

                        var replaceUrl = ConfigurationManager.AppSettings["LogoUrl"]
                            .Replace("{0}", carBrand.Rid.ToString());
                        carBrand.BrandLogo =
                            await SpiderFile.DownImageAsync(replaceUrl, this._logoPath, "/Upload/logo/");
                        carBrand.Id = CarBrandBusiness.Insert(carBrand);

                        #region 车型 

                        var modelTask =await SpiderFile.GetStringAsync($"{ConfigurationManager.AppSettings["BrandsUrl"]}&pagetype=masterbrand&objid={carBrand.Rid}");
                      
                        string modelStr = modelTask.Substring(14, modelTask.Length - 15);
                        JObject modelJObject = (JObject) JsonConvert.DeserializeObject(modelStr);
                        Dictionary<string, object> modelDictionary =
                            JsonConvert.DeserializeObject<Dictionary<string, object>>(modelJObject["brand"].ToString());
                        IList<CarModelEntity> carModels = new List<CarModelEntity>();
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
                                            carModels.Add(new CarModelEntity
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
                                        carModels.Add(new CarModelEntity
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
                            //                            Task.Run(() => { Parallel.ForEach(carModels, model => { }); });
                            var document = new HtmlDocument();
                            CarModelBusiness.ShowInfoEventHandler += CarModelBusiness_ShowInfoEventHandler;
                            CarSeriesBusiness.ShowInfoEventHandler += CarSeriesBusiness_ShowInfoEventHandler;
                            foreach (CarModelEntity carModel in carModels)
                            {
                                #region 车型入库 

                                var htmlStream = await SpiderFile.GetStreamAsync(carModel.LinkUrl);
                                document.Load(htmlStream, Encoding.UTF8);
                                //全国参考价
                                HtmlNode infoNode = document.DocumentNode.SelectSingleNode("//div[@class='desc']");
                                var referencePriceNode = infoNode.SelectSingleNode("//div[@class='top']/h5/a/em") ??
                                                         infoNode.SelectSingleNode("//div[@class='top']/h5/em");
                                carModel.NationalReferencePrice = referencePriceNode.InnerText.Trim();
                                //厂商指导价
                                var msrpNode = infoNode.SelectSingleNode("//div[@class='mid row']/div[1]/h5");
                                carModel.Msrp = msrpNode.InnerText.Trim();
                                //二手车
                                var secondHandNode = infoNode.SelectSingleNode("//div[@class='mid row']/div[2]/h5/a");
                                carModel.SecondHand = secondHandNode.InnerText.Trim();
                                //油耗
                                var fuelConsumptionNode =
                                    infoNode.SelectSingleNode("//div[@class='mid row']/div[3]/h5/a") ??
                                    infoNode.SelectSingleNode("//div[@class='mid row']/div[3]/h5");
                                if (fuelConsumptionNode != null)
                                {
                                    carModel.FuelConsumption = fuelConsumptionNode.InnerText.Trim();
                                }
                                HtmlNode imageNode =
                                    document.DocumentNode.SelectSingleNode("//div[@class='img']/a[1]/img[1]");
                                string imageUrl = imageNode?.GetAttributeValue("src", null);
                                carModel.ImageUrl = imageUrl;
                                carModel.ImagePath =
                                    await SpiderFile.DownImageAsync(imageUrl, this._modelPath, "/Upload/model/");
                                carModel.Id = CarModelBusiness.Insert(carModel);

                                #endregion


                                #region 车系入库

                                #region  ------------------在售------------------------ 

                                //在售
                                HtmlNodeCollection nodeCollection =
                                    document.DocumentNode.SelectNodes("//td[starts-with(@id,'carlist_')]");
                                if (nodeCollection != null && nodeCollection.Count > 0)
                                {
                                    foreach (HtmlNode node in nodeCollection)
                                    {
                                        if (node.FirstChild.Name == "a")
                                        {
                                            CarSeriesBusiness.Insert(new CarSeriesEntity
                                            {
                                                AddTime = DateTime.Now,
                                                LinkUrl =
                                                    $"{ConfigurationManager.AppSettings["Domain"]}{node.FirstChild.GetAttributeValue("href", null)}",
                                                ModelId = carModel.Id,
                                                SeriesName = node.FirstChild.InnerText.Trim(),
                                            });
                                        }
                                    }
                                }

                                #endregion

                                #region  -------------------停售-----------------------

                                //停售
                                HtmlNodeCollection liNodeCollection =
                                    document.DocumentNode.SelectNodes("//div[@id='pop_nosalelist']/a");
                                if (liNodeCollection != null && liNodeCollection.Count > 0)
                                {
                                    foreach (HtmlNode node in liNodeCollection)
                                    {
                                        string href = node.GetAttributeValue("href", null);
                                        if (href != null)
                                        {
                                            HtmlDocument doc = new HtmlDocument();
                                            doc.Load(
                                                await SpiderFile.GetStreamAsync(ConfigurationManager.AppSettings["Domain"] + href),Encoding.UTF8);
                                            HtmlNodeCollection pdLnNodeCollection =
                                                doc.DocumentNode.SelectNodes("//td[starts-with(@id,'carlist_')]");
                                            if (pdLnNodeCollection != null && pdLnNodeCollection.Count > 0)
                                            {
                                                foreach (HtmlNode htmlNode in pdLnNodeCollection)
                                                {
                                                    if (htmlNode.FirstChild.Name == "a")
                                                    {
                                                        CarSeriesBusiness.Insert(new CarSeriesEntity
                                                        {
                                                            AddTime = DateTime.Now,
                                                            LinkUrl =
                                                                $"{ConfigurationManager.AppSettings["Domain"]}{htmlNode.FirstChild.GetAttributeValue("href", null)}",
                                                            ModelId = carModel.Id,
                                                            SeriesName = htmlNode.FirstChild.InnerText,
                                                        });
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                #endregion

                                #endregion
                            }
                            CarSeriesBusiness.ShowInfoEventHandler -= CarSeriesBusiness_ShowInfoEventHandler;
                            CarModelBusiness.ShowInfoEventHandler -= CarModelBusiness_ShowInfoEventHandler;
                        }

                        #endregion

                        //                    }
                    });

                    if (parallelResult.IsCompleted)
                    {
                        CarBrandBusiness.ShowInfoEventHandler -= CarBrandBusiness_ShowInfoEventHandler;
                    }
                }
                else
                {
                    View.ShowMessage("没有获取到品牌", "提示");
                }
            }).LogExceptions();
        }

        private void CarSeriesBusiness_ShowInfoEventHandler(object sender, ViewModelArg<CarSeriesEntity> e)
        {
            View.ShowSeriesInfoAction?.Invoke(new ViewModelArg<CarSeriesEntity>
            {
                Car = e.Car,
                Type = e.Type
            });
        }

        /// <summary>
        /// 车型显示信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarModelBusiness_ShowInfoEventHandler(object sender, ViewModelArg<CarModelEntity> e)
        {
            View.ShowModelInfoAction?.Invoke(new ViewModelArg<CarModelEntity>
            {
                Car = e.Car,
                Type = e.Type
            });
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarBrandBusiness_ShowInfoEventHandler(object sender, ViewModelArg<CarBrandEntity> e)
        {
            View.ShowBrandInfoAction?.Invoke(new ViewModelArg<CarBrandEntity>
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