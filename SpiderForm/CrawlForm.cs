using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SpiderIView;
using SpiderModel.Entity;
using SpiderModel.Models;

namespace SpiderForm
{
    public partial class CrawlForm : BaseForm, ICrawlView
    {
        public CrawlForm()
        {
            InitializeComponent();
            ShowInfoAction = ShowInfo;
        }


        /// <summary>
        ///采集品牌事件
        /// </summary>
        public event EventHandler PickBrandEventHandler;

        /// <summary>
        /// 采集车型事件
        /// </summary>
        public event EventHandler PickModelEventHandler;

        /// <summary>
        /// 界面显示
        /// </summary>
        public Action<ViewModelArg<Car>> ShowInfoAction { get; set; }

        /// <summary>
        /// 界面显示
        /// </summary> 
        private void ShowInfo(ViewModelArg<Car> viewModelArg)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new Action<ViewModelArg<Car>>(ShowInfo), viewModelArg);
                return;
            }
            if (viewModelArg.Type == 0)
            {
                this.labelBrandUpdate.Text = $@"更新:{int.Parse(labelBrandUpdate.Text.Split(':')[1]) + 1}";
            }
            else
            {
                this.labelBrandInsert.Text = $@"新增:{int.Parse(labelBrandInsert.Text.Split(':')[1]) + 1}";
            }
            CarBrandEntity arg = (CarBrandEntity) viewModelArg.Car;
            this.labelBrandName.Text = arg.BrandName;
            BackgroundImageLayout = ImageLayout.Center;
            string logo =
                $"{Directory.GetCurrentDirectory()}{arg.BrandLogo.Replace("Upload", "image").Replace("/", "\\")}";
            BackgroundImage = Image.FromFile(logo);
        }

        /// <summary>
        ///     采集品牌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void carBrandButton_Click(object sender, EventArgs e)
        {
            PickBrandEventHandler?.Invoke(sender, null);
//            carBrandButton.Enabled = false;
        }

        /// <summary>
        ///     采集车型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void carModelButton_Click_1(object sender, EventArgs e)
        {
            PickModelEventHandler?.Invoke(sender, e);
//            carModelButton.Enabled = false;
        }
    }
}