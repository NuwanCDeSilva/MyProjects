using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Services
{
    public partial class ServiceWIP_VisitComent : Base
    {
        private string GblJobNum = string.Empty;
        private Int32 GbljobLineNum = -11;
        List<Service_VisitComments> oMainList = new List<Service_VisitComments>();

        public ServiceWIP_VisitComent(string job, Int32 jobLine)
        {
            GblJobNum = job;
            GbljobLineNum = jobLine;
            InitializeComponent();

            getItems();
        }

        private void getItems()
        {
            
            oMainList = CHNLSVC.CustService.GET_SCV_JOB_VISIT_COMNT(GblJobNum, GbljobLineNum);
            bindData();
        }

        private void bindData()
        {

            DataTable dtTemp = new DataTable();
            dtTemp = ToDataTable(oMainList);
            dgvRecords.AutoGenerateColumns = false;
            dgvRecords.DataSource = new List<Service_VisitComments>();
            dgvRecords.DataSource = dtTemp;

 
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


        

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnCloseFrom_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ServicePayments_Load(object sender, EventArgs e)
        {

            txtJobNo.Text = GblJobNum;
        }
    }
}