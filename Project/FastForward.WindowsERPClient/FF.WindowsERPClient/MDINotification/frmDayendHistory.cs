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
    public partial class frmDayendHistory :  Base
    {
        public frmDayendHistory()
        {
            InitializeComponent();
        }

        private void frmDayendHistory_Load(object sender, EventArgs e)
        {
            DataTable dtLOg = new DataTable();
            gvHistory.AutoGenerateColumns = false;
            dtLOg = CHNLSVC.General.Notification_Get_LastDayendDates(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            gvHistory.DataSource = dtLOg;
        }
    }
}
