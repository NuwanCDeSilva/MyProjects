using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.MDINotification
{
    public partial class frmOtherShopCollections : Base
    {
        public frmOtherShopCollections()
        {
            InitializeComponent();
        }

        private void frmOtherShopCollections_Load(object sender, EventArgs e)
        {
            dgvDetails.AutoGenerateColumns = false;
            dgvDetails.RowTemplate.Height = 18;
            List<Notification_OthShpCollecitons> oItems = CHNLSVC.General.GetOtherShopCollections(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, DateTime.Today);
            dgvDetails.DataSource = new List<Notification_OthShpCollecitons>();
            dgvDetails.DataSource = oItems;
        }
    }
}
