using System;
using SpiderIView;
using SpiderModel.Models;

namespace SpiderForm
{
    public partial class CrawlForm : BaseForm, ICrawlView
    {
        public CrawlForm()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     采集品牌事件
        /// </summary>
        public event EventHandler<CarBrandEventArgs> PickBrand;

        /// <summary>
        ///     采集品牌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void CarBrandButton_Click(object sender, EventArgs args)
        {
            PickBrand?.Invoke(sender, null);
        }
    }
}