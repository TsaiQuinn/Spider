using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SpiderForm
{
    public partial class BaseForm : XtraForm
    {
        public BaseForm()
        {
            InitializeComponent();
        }

        public virtual void ShowMessage(string msg, string title)
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}