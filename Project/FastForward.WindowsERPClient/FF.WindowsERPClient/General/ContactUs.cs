using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.Interfaces;

namespace FF.WindowsERPClient.General
{
    public partial class ContactUs : Base
    {

        public ContactUs()
        {
            InitializeComponent();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ContactUs formnew = new ContactUs();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

        private void btnIT_Click(object sender, EventArgs e)
        {
            btnIT.BackColor = Color.Bisque;
            btnInv.BackColor = Color.DarkKhaki;
            btnCost.BackColor = Color.DarkKhaki;
            btnCredit.BackColor = Color.DarkKhaki;
            btnAcc.BackColor = Color.DarkKhaki;
            btnIns.BackColor = Color.DarkKhaki;
            btnITOpr.BackColor = Color.DarkKhaki;

            load_details("IT");
        }

        private void btnAcc_Click(object sender, EventArgs e)
        {
            btnAcc.BackColor = Color.Bisque;
            btnInv.BackColor = Color.DarkKhaki;
            btnCost.BackColor = Color.DarkKhaki;
            btnCredit.BackColor = Color.DarkKhaki;
            btnIT.BackColor = Color.DarkKhaki;
            btnIns.BackColor = Color.DarkKhaki;
            btnITOpr.BackColor = Color.DarkKhaki;

            load_details("ACC");
        }

        private void load_details(string _code)
        {
            lblInfor.Text = "";
            grvDet.AutoGenerateColumns = false;
            MasterProfitCenter profCenter = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);

            DataTable _dt = CHNLSVC.General.Get_Dept_Cont_Dt(BaseCls.GlbUserComCode, profCenter.Mpc_chnl, _code,0);
            if(_dt.Rows.Count==0)
                _dt = CHNLSVC.General.Get_Dept_Cont_Dt(BaseCls.GlbUserComCode, "", _code, 1);
            if (_dt.Rows.Count != 0)
                lblInfor.Text=_dt.Rows[0]["dcnt_rem"].ToString();
            grvDet.DataSource = _dt;

        }

        private void btnInv_Click(object sender, EventArgs e)
        {
            btnInv.BackColor = Color.Bisque;
            btnAcc.BackColor = Color.DarkKhaki;
            btnCost.BackColor = Color.DarkKhaki;
            btnCredit.BackColor = Color.DarkKhaki;
            btnIT.BackColor = Color.DarkKhaki;
            btnIns.BackColor = Color.DarkKhaki;
            btnITOpr.BackColor = Color.DarkKhaki;

            load_details("INV");
        }

        private void btnCredit_Click(object sender, EventArgs e)
        {
            btnCredit.BackColor = Color.Bisque;
            btnInv.BackColor = Color.DarkKhaki;
            btnCost.BackColor = Color.DarkKhaki;
            btnAcc.BackColor = Color.DarkKhaki;
            btnIT.BackColor = Color.DarkKhaki;
            btnIns.BackColor = Color.DarkKhaki;
            btnITOpr.BackColor = Color.DarkKhaki;

            load_details("CRED");
        }

        private void btnCost_Click(object sender, EventArgs e)
        {
            btnCost.BackColor = Color.Bisque;
            btnInv.BackColor = Color.DarkKhaki;
            btnCredit.BackColor = Color.DarkKhaki;
            btnAcc.BackColor = Color.DarkKhaki;
            btnIT.BackColor = Color.DarkKhaki;
            btnIns.BackColor = Color.DarkKhaki;
            btnITOpr.BackColor = Color.DarkKhaki;

            load_details("COST");
        }

        private void ContactUs_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIns_Click(object sender, EventArgs e)
        {
            btnIns.BackColor = Color.Bisque;
            btnInv.BackColor = Color.DarkKhaki;
            btnCredit.BackColor = Color.DarkKhaki;
            btnAcc.BackColor = Color.DarkKhaki;
            btnIT.BackColor = Color.DarkKhaki;
            btnCost.BackColor = Color.DarkKhaki;
            btnITOpr.BackColor = Color.DarkKhaki;

            load_details("INS");
        }

        private void btnITOpr_Click(object sender, EventArgs e)
        {
            btnITOpr.BackColor = Color.Bisque;
            btnInv.BackColor = Color.DarkKhaki;
            btnCost.BackColor = Color.DarkKhaki;
            btnCredit.BackColor = Color.DarkKhaki;
            btnAcc.BackColor = Color.DarkKhaki;
            btnIns.BackColor = Color.DarkKhaki;
            btnIT.BackColor = Color.DarkKhaki;

            load_details("ITOPR");
        }
    }
}
