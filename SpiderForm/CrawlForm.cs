using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SpiderCommon;
using SpiderIView;
using SpiderModel;
using SpiderModel.Entity;

namespace SpiderForm
{
    public partial class CrawlForm : BaseForm, ICrawlView
    {
        public CrawlForm()
        {
            InitializeComponent();
            ShowBrandInfoAction = ShowInfo;
            ShowModelInfoAction = ShowModelInfo;
            ShowSeriesInfoAction = ShowSeriesInfo;
        }

        /// <summary>
        /// 车系界面显示
        /// </summary>
        private void ShowSeriesInfo(ViewModelArg<CarSeriesEntity> viewModelArg)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new Action<ViewModelArg<CarSeriesEntity>>(ShowSeriesInfo), viewModelArg);
                return;
            }
            if (viewModelArg.Type == 0)
            {
                this.labelSeriesUpdate.Text = $@"更新:{int.Parse(labelSeriesUpdate.Text.Split(':')[1]) + 1}";
            }
            else
            {
                this.labelSeriesInsert.Text = $@"新增:{int.Parse(labelSeriesInsert.Text.Split(':')[1]) + 1}";
            }
            CarSeriesEntity arg = viewModelArg.Car;
            this.labelSeriesName.Text = arg.SeriesName;
        }

        /// <summary>
        /// 车型界面显示
        /// </summary>
        private void ShowModelInfo(ViewModelArg<CarModelEntity> viewModelArg)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new Action<ViewModelArg<CarModelEntity>>(ShowModelInfo), viewModelArg);
                return;
            }
            if (viewModelArg.Type == 0)
            {
                this.labelModelUpdate.Text = $@"更新:{int.Parse(labelModelUpdate.Text.Split(':')[1]) + 1}";
            }
            else
            {
                this.labelModelInsert.Text = $@"新增:{int.Parse(labelModelInsert.Text.Split(':')[1]) + 1}";
            }
            CarModelEntity arg = viewModelArg.Car;
            this.labelModelName.Text = arg.ModelName;
            BackgroundImageLayout = ImageLayout.Zoom;
            string logo =
                $"{Directory.GetCurrentDirectory()}{arg.ImagePath.Replace("Upload", "image").Replace("/", "\\")}";
            try
            {
                BackgroundImage = Image.FromFile(logo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                File.Delete(logo);
                SpiderFile.DownImage(arg.ImageUrl, logo);
                BackgroundImage = Image.FromFile(logo); 
            }
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
        /// 品牌界面显示
        /// </summary>
        public Action<ViewModelArg<CarBrandEntity>> ShowBrandInfoAction { get; set; }

        /// <summary>
        /// 车型界面显示
        /// </summary>
        public Action<ViewModelArg<CarModelEntity>> ShowModelInfoAction { get; set; }

        /// <summary>
        /// 车系界面显示
        /// </summary>
        public Action<ViewModelArg<CarSeriesEntity>> ShowSeriesInfoAction { get; set; }

        /// <summary>
        /// 车系界面显示
        /// </summary>
        public Action<ViewModelArg<Car>> ShowInfoAction { get; set; }

        /// <summary>
        /// 界面显示
        /// </summary> 
        private void ShowInfo(ViewModelArg<CarBrandEntity> viewModelArg)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new Action<ViewModelArg<CarBrandEntity>>(ShowInfo), viewModelArg);
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
            CarBrandEntity arg = viewModelArg.Car;
            this.labelBrandName.Text = arg.BrandName;
            BackgroundImageLayout = ImageLayout.Tile;
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