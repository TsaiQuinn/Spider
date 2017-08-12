#region ----------------备注----------------

// Author:CK 
// FileName:CrawlPresenter.cs 
// Create Date:2017-08-02
// Create Time:14:48 

#endregion

using System;
using Ninject;
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

//        [Inject]
        public ICarBrandBusiness CarBrandBusiness { get; set; }

        protected override void ViewLoad()
        {
            View.Load += OnViewLoad;
            View.PickBrand += OnPickBrand;
        }

        /// <summary>
        ///     采集品牌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPickBrand(object sender, CarBrandEventArgs e)
        {
            var list = CarBrandBusiness.QueryList(null);
            View.ShowMessage($"品牌数量为{list.Count}", "采集品牌");
        }

        /// <summary>
        ///     加载视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnViewLoad(object sender, EventArgs e)
        {
            var list = CarBrandBusiness.QueryList(null);
            View.ShowMessage($"品牌数量为{list.Count}", "采集品牌");
        }
    }
}