using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Services
{
    public partial class ServiceWIP_POItems : Base
    {
        private string GblJobNum = string.Empty;
        private Int32 GbljobLineNum = -11;

        public ServiceWIP_POItems(string job, Int32 jobLine)
        {
            GblJobNum = job;
            GbljobLineNum = jobLine;
            InitializeComponent();
            dgvItems.AutoGenerateColumns = false;
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (m.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = m.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }
            base.WndProc(ref m);
        }


        private void btnView_Click(object sender, EventArgs e)
        {
          
        }

        private void dgvItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
           
        }

        private void itemID_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void dgvItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvItems.IsCurrentCellDirty)
            {
                dgvItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void btnVouNo_Click(object sender, EventArgs e)
        {
           // txtItemCode_DoubleClick(null, null);
        }

        private void txtItemCode_DoubleClick(object sender, EventArgs e)
        {
     
        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void txtItemCode_Leave(object sender, EventArgs e)
        {
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
   
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
         
        }

        private void updateConsumableItems()
        {
           
        }

        private void dgvItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        
        }

        private void ServiceWIP_ConsumableItems_Load(object sender, EventArgs e)
        {

            List<Service_Purchase_Approval> _tmpPOList = CHNLSVC.CustService.GetServicePOApp(GblJobNum, GbljobLineNum);
            dgvItems.AutoGenerateColumns = false;
            dgvItems.DataSource = _tmpPOList;
            dgvItems.CellToolTipTextNeeded += new DataGridViewCellToolTipTextNeededEventHandler(dataGridView1_CellToolTipTextNeeded);
        }

        private void dgvItems_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
         
        }

        private void dgvItems_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void btnCloseFrom_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvItems_MouseMove(object sender, MouseEventArgs e)
        {
     
        }

        private void dataGridView1_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
  
        }

        private void btnRetun_Click(object sender, EventArgs e)
        {
        }
    }
}