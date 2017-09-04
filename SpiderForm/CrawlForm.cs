using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SpiderIView;
using SpiderModel.Models;

namespace SpiderForm
{
    public partial class CrawlForm : BaseForm, ICrawlView
    {
        public CrawlForm()
        {
            InitializeComponent();
            ShowAction = View_ShowEvent; 
        } 

        /// <summary>
        ///采集品牌事件
        /// </summary>
        public event EventHandler PickBrandEvent;

        /// <summary>
        /// 采集车型事件
        /// </summary>
        public event EventHandler PickModelEvent;

        /// <summary>
        /// 界面显示
        /// </summary>
        public Action<ViewModelEventArg> ShowAction { get; set; } 

        /// <summary>
        /// 界面显示
        /// </summary> 
        private void View_ShowEvent(ViewModelEventArg arg)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new Action<ViewModelEventArg>(View_ShowEvent), arg);
                return;
            }
            if (arg.Type == 0)
            {
                this.labelBrandUpdate.Text = $@"更新:{int.Parse(labelBrandUpdate.Text.Split(':')[1]) + 1}";
            }
            else
            {
                this.labelBrandInsert.Text = $@"新增:{int.Parse(labelBrandInsert.Text.Split(':')[1]) + 1}";
            }
            this.labelBrandName.Text = arg.Brand.BrandName;
            BackgroundImageLayout = ImageLayout.Center;
            string logo =
                $"{Directory.GetCurrentDirectory()}{arg.Brand.BrandLogo.Replace("Upload", "image").Replace("/","\\")}";
            BackgroundImage = Image.FromFile(logo);
        }
        /// <summary>
        ///     采集品牌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void carBrandButton_Click(object sender, EventArgs e)
        {
            PickBrandEvent?.Invoke(sender, null);
//            carBrandButton.Enabled = false;
        }
        /// <summary>
        ///     采集车型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void carModelButton_Click_1(object sender, EventArgs e)
        {
            PickModelEvent?.Invoke(sender, e);
//            carModelButton.Enabled = false;
        }
    }
}