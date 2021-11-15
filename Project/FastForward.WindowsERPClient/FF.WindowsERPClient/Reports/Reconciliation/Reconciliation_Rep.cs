using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;
using System.Threading;
using System.Collections;
using FF.WindowsERPClient.Reports.HP;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms.Internal.Win32;
using Excel = Microsoft.Office.Interop.Excel;

//Written By kapila on 17/01/2012
namespace FF.WindowsERPClient.Reports.Reconciliation
{
    public partial class Reconciliation_Rep : Base
    {

        public Reconciliation_Rep()
        {

            InitializeComponent();
            InitializeEnv();
            GetCompanyDet(null, null);
            GetPCDet(null, null);
            setFormControls(0);


        }

        private void setFormControls(Int32 _index)
        {

            pnlAsAtDate.Enabled = false;
            pnlDateRange.Enabled = true;
            pnlHP.Enabled = false;
            cmbGroupBy.Items.Clear();
            switch (_index)
            {
                case 3:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = false;
                        pnlHP.Enabled = true;

                        cmbGroupBy.Items.Add("Default");
                        cmbGroupBy.Items.Add("Profit Center");
                        cmbGroupBy.Items.Add("Manager");
                        cmbGroupBy.Items.Add("Scheme Type");
                        cmbGroupBy.Items.Add("Scheme");
                        cmbGroupBy.SelectedIndex = 0;
                        break;
                    }
            }
        }

        protected void GetCompanyDet(object sender, EventArgs e)
        {

            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(txtComp.Text);
            if (_masterComp != null)
            {
                txtCompDesc.Text = _masterComp.Mc_desc;
                txtCompAddr.Text = _masterComp.Mc_add1 + _masterComp.Mc_add2;
            }
            else
            {
                txtCompDesc.Text = "";
                txtCompAddr.Text = "";
            }
        }

        protected void GetPCDet(object sender, EventArgs e)
        {

            MasterProfitCenter _masterPC = null;
            _masterPC = CHNLSVC.General.GetPCByPCCode(txtComp.Text, txtPC.Text);
            if (_masterPC != null)
            {
                txtPCDesn.Text = _masterPC.Mpc_desc;
            }
            else
            {
                txtPCDesn.Text = "";
            }
        }

        private void InitializeEnv()
        {
            txtFromDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            txtAsAtDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");

            txtComp.Text = BaseCls.GlbUserComCode;
            txtPC.Text = BaseCls.GlbUserDefProf;

            cmbYear.Items.Add("2012");
            cmbYear.Items.Add("2013");
            cmbYear.Items.Add("2014");
            cmbYear.Items.Add("2015");
            cmbYear.Items.Add("2016");
            cmbYear.Items.Add("2017");
            cmbYear.Items.Add("2018");

            int _Year = DateTime.Now.Year;
            cmbYear.SelectedIndex = _Year % 2013 + 1;

            cmbMonth.Items.Add("January");
            cmbMonth.Items.Add("February");
            cmbMonth.Items.Add("March");
            cmbMonth.Items.Add("April");
            cmbMonth.Items.Add("May");
            cmbMonth.Items.Add("June");
            cmbMonth.Items.Add("July");
            cmbMonth.Items.Add("August");
            cmbMonth.Items.Add("September");
            cmbMonth.Items.Add("October");
            cmbMonth.Items.Add("November");
            cmbMonth.Items.Add("December");
            cmbMonth.SelectedIndex = DateTime.Now.Month - 1;


        }

        private void update_PC_List()
        {
            string _tmpPC = "";
            BaseCls.GlbReportProfit = "";

            Boolean _isPCFound = false;
            Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC(BaseCls.GlbUserID, txtComp.Text, null, null);

            foreach (ListViewItem Item in lstPC.Items)
            {
                string pc = Item.Text;

                if (Item.Checked == true)
                {
                    Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC(BaseCls.GlbUserID, txtComp.Text, pc, null);

                    _isPCFound = true;
                    if (string.IsNullOrEmpty(BaseCls.GlbReportProfit))
                    {
                        BaseCls.GlbReportProfit = pc;
                    }
                    else
                    {
                        //BaseCls.GlbReportProfit = BaseCls.GlbReportProfit + "," + pc;
                        BaseCls.GlbReportProfit = "All Locations Based on User Rights";
                    }
                }
            }

            if (_isPCFound == false)
            {
                BaseCls.GlbReportProfit = txtPC.Text;
                Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC(BaseCls.GlbUserID, txtComp.Text, txtPC.Text, null);
            }
        }

        //private void update_PC_List()
        //{
        //    string _tmpPC = "";
        //    BaseCls.GlbReportProfit = "";

        //    Boolean _isPCFound = false;
        //    Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC(BaseCls.GlbUserID, txtComp.Text, null, null);

        //    foreach (ListViewItem Item in lstPC.Items)
        //    {
        //        string pc = Item.Text;

        //        if (Item.Checked == true)
        //        {
        //            Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC(BaseCls.GlbUserID, txtComp.Text, pc, null);

        //            _isPCFound = true;
        //            if (string.IsNullOrEmpty(BaseCls.GlbReportProfit))
        //            {
        //                BaseCls.GlbReportProfit = pc;
        //            }
        //            else
        //            {
        //                BaseCls.GlbReportProfit = BaseCls.GlbReportProfit + "," + pc;
        //            }
        //        }
        //    }

        //    if (_isPCFound == false)
        //    {
        //        BaseCls.GlbReportProfit = txtPC.Text;
        //        Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC(BaseCls.GlbUserID, txtComp.Text, txtPC.Text, null);
        //    }
        //}

        private void btnDisplay_Click_1(object sender, EventArgs e)
        {
            //check this user has permission for this Loc
            if (txtPC.Text != string.Empty)
            {
                string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPH"))
                {
                    Int16 is_Access = CHNLSVC.Security.Check_User_Loc(BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtPC.Text);
                    if (is_Access != 1)
                    {
                        MessageBox.Show("Access Denied.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

         


            if (opt1.Checked == true)  
            {   //update temporary table
                update_PC_List();
                //04-03-13 Nadeeka
                BaseCls.GlbReportProfit = txtPC.Text;
                BaseCls.GlbReportComp = txtComp.Text;
                BaseCls.GlbReportCompCode = txtComp.Text;
                BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text);
                BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Text);


                BaseCls.GlbReportName = "TransactionVariance.rpt";
                Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
                _view.Show();
                _view = null;
            }



           
            btnDisplay.Enabled = true;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            string com = txtComp.Text;
            string chanel = txtChanel.Text;
            string subChanel = txtSChanel.Text;
            string area = txtArea.Text;
            string region = txtRegion.Text;
            string zone = txtZone.Text;
            string pc = txtPC.Text;

            lstPC.Clear();
            string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPH"))
            {
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(com, chanel, subChanel, area, region, zone, pc);
                foreach (DataRow drow in dt.Rows)
                {
                    lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                }
            }
            else
            {
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep(BaseCls.GlbUserID, com, chanel, subChanel, area, region, zone, pc);
                foreach (DataRow drow in dt.Rows)
                {
                    lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                }
            }

        }

        private void txtChanel_LostFocus(object sender, EventArgs e)
        {
            lstPC.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lstPC.Clear();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstPC.Items)
            {
                Item.Checked = true;
            }
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstPC.Items)
            {
                Item.Checked = false;
            }
        }

        //#region option change events
        //private void opt1_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (opt1.Checked == true)
        //    {
        //        if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP1"))
        //        {
        //            MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :RHP1 )");
        //            opt1.Checked = false;
        //            return;
        //        }
        //        setFormControls(1);
        //    }
        //}
        //private void opt2_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (opt2.Checked == true)
        //    {
        //        if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP2"))
        //        {
        //            MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :RHP2 )");
        //            opt2.Checked = false;
        //            return;
        //        }
        //        setFormControls(2);
        //    }
        //}
        //private void opt3_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (opt3.Checked == true)
        //    {
        //        if (BaseCls.GlbUserID != "ADMIN")
        //        {

        //            if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP3"))
        //            {
        //                MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :RHP3 )");
        //                opt3.Checked = false;
        //                return;
        //            }
        //        }
        //        setFormControls(3);
        //    }
        //}
        //private void opt4_CheckedChanged(object sender, EventArgs e)
        //{
        //    //if (opt4.Checked == true)
        //    //{
        //    //    if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP4"))
        //    //    {
        //    //        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :RHP4 )");
        //    //        opt4.Checked = false;
        //    //        return;
        //    //    }
        //    //    setFormControls(4);
        //    //}
        //}

        //private void opt5_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (opt5.Checked == true)
        //    {
        //        if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP5"))
        //        {
        //            MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :RHP5 )");
        //            opt5.Checked = false;
        //            return;
        //        }
        //        cmbGroupBy.Items.Add("Default");
        //        cmbGroupBy.Items.Add("Scheme Type");
        //        cmbGroupBy.Items.Add("Scheme");
        //        cmbGroupBy.Items.Add("Location");
        //        cmbGroupBy.SelectedIndex = 0;
        //        pnlDateRange.Enabled = false;
        //        pnlAsAtDate.Enabled = true;
        //        pnlHP.Enabled = true;
        //    }
        //    else
        //    {
        //        cmbGroupBy.Items.Clear();
        //        pnlDateRange.Enabled = true;
        //        pnlAsAtDate.Enabled = false;
        //        pnlHP.Enabled = false;
        //    }
        //}

        //#endregion
        #region option change events
        private void opt1_CheckedChanged(object sender, EventArgs e)
        {
            if (opt1.Checked == true)
            {
                // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP1"))
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8001))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8001)");
                    //opt1.Checked = false;
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(1);
            }
        }
      
       

        #endregion
        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbYear.Text))
            {
                MessageBox.Show("Select the year");
                return;
            }

            int month = cmbMonth.SelectedIndex + 1;
            int year = Convert.ToInt32(cmbYear.Text);

            int numberOfDays = DateTime.DaysInMonth(year, month);
            DateTime lastDay = new DateTime(year, month, numberOfDays);

            txtToDate.Text = lastDay.ToString("dd/MMM/yyyy");

            DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear.Text), month, 1);
            txtFromDate.Text = (dtFrom.AddDays(-(dtFrom.Day - 1))).ToString("dd/MMM/yyyy");
        }

       

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtFromDate_ValueChanged(object sender, EventArgs e)
        {
            BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Value);
        }

        private void txtToDate_ValueChanged(object sender, EventArgs e)
        {
            BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Value);
        }

        private void txtAsAtDate_ValueChanged(object sender, EventArgs e)
        {
            BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Value);
        }






    }
}


