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
using FF.WindowsERPClient.Reports;
using CrystalDecisions.ReportAppServer;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;

//Written By Sanjeewa on 28/02/2014
namespace FF.WindowsERPClient.Reports.Sales_2
    {
    public partial class Sales2_Rep : Base
    {
        clsSales2Rep objBrand = new clsSales2Rep();        
        
        public Sales2_Rep()
        {
            InitializeComponent();
            InitializeEnv();
            GetCompanyDet(null, null);
            GetPCDet(null, null);
            setFormControls(0);
        }

        private void setFormControls(Int32 _index)
        {
            pnlDebt.Enabled = false;
            pnlOuts.Enabled = false;
            pnlAsAtDate.Enabled = false;
            pnlDateRange.Enabled = true;
            pnl_Direc.Enabled = false;
            pnl_DocNo.Enabled = false;
            pnl_DocSubType.Enabled = false;
            pnl_DocType.Enabled = false;
            pnl_Entry_Tp.Enabled = false;
            pnl_Rec_Tp.Enabled = false;
            pnl_Item.Enabled = false;
            pnlCust.Enabled = false;
            pnlDelivery.Enabled = false;
            pnlCust.Enabled = false;
            pnlExec.Enabled = false;
            pnl_Itm_Stus.Enabled = false;
            pnlPrefix.Enabled = false;
            pnlPayType.Enabled = false;
            pnl_PB.Visible = false;
            pnl_PB.Enabled = false;
            pnl_PBLevel.Visible = false;
            pnl_PBLevel.Enabled = false;
            pnlDiscRate.Visible = false;
            pnlDiscRate.Enabled = false;
            label41.Text = "Discount Rate";
            pnlStus.Enabled = false;
            pnlPO.Visible = false;
            pnlPO.Enabled = false;
            pnlSup.Visible = false;
            pnlSup.Enabled = false;
            panel7.Visible = false;

            lbl1.Enabled = false;
            lbl2.Enabled = false;
            lbl3.Enabled = false;
            lbl4.Enabled = false;
            lbl5.Enabled = false;
            lbl6.Enabled = false;
            lbl7.Enabled = false;
            lbl8.Enabled = false;
            lbl9.Enabled = false;
            lbl10.Enabled = false;
            lbl11.Enabled = false;
            lbl13.Enabled = false;
            lbl14.Enabled = false;

            lstCat1.Enabled = false;
            lstCat2.Enabled = false;
            lstCat3.Enabled = false;
            lstItem.Enabled = false;
            lstBrand.Enabled = false;
            btnCat1.Enabled = false;
            btnCat2.Enabled = false;
            btnCat3.Enabled = false;
            btnItem.Enabled = false;
            btnBrand.Enabled = false;
            label18.Text = "Document No";
            label16.Text = "Direction";
            label40.Text = "Pay Type";
            optDeliver.Text = "With Delivered Sales";
            optForward.Text = "With Forward Sales";
            chkAsAtDate.Visible = false;
            txtAsAtDate.Enabled = true;
            cmbExeType.Visible = false;
            comboBoxDocType.Visible = false;
            pnl_Export.Visible = false;
            pnl_Export.Enabled = false;
            chk_Export.Visible = false;
            chk_Ord.Visible = false;

            comboBoxPayModes.DataSource = CHNLSVC.Sales.GetAllPaymentType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, null);
            comboBoxPayModes.DisplayMember = "SAPT_DESC";
            comboBoxPayModes.ValueMember = "SAPT_CD";
            comboBoxPayModes.SelectedIndex = -1;
            cmbExeType.SelectedIndex = -1;
            comboBoxDocType.SelectedIndex = -1;

            switch (_index)
            {                
                case 1:
                    {
                        pnl_Item.Enabled = true;
                        btnCat1.Enabled = true;
                        btnCat2.Enabled = true;
                        btnCat3.Enabled = true;
                        btnItem.Enabled = true;
                        btnBrand.Enabled = true;
                        pnl_DocType.Enabled = true;
                        break;
                    } 
              case 13:
                    {
                        panel7.Visible = true;
                        pnl_Item.Enabled = true;
                        btnCat1.Enabled = true;
                        btnCat2.Enabled = true;
                        btnCat3.Enabled = true;
                        btnItem.Enabled = true;
                        btnBrand.Enabled = true;
                        //pnl_DocType.Enabled = true;
                        break;
                    }
              case 14:
                    {
                        panel7.Visible = true;
                        pnl_Item.Enabled = true;
                        btnCat1.Enabled = true;
                        btnCat2.Enabled = true;
                        btnCat3.Enabled = true;
                        btnItem.Enabled = true;
                        btnBrand.Enabled = true;
                        //pnl_DocType.Enabled = true;
                        break;
                    }

              case 15:
                    {
                        pnl_Item.Enabled = true;                        
                        break;
                    }
              case 16:
                    {
                        pnl_Item.Enabled = true;
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

            txtFromDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            txtAsAtDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");

            BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Value).Date;
            BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Value).Date;
            BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Value).Date;


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

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            btnNone.Focus();
            try
            {
                BaseCls.GlbReportName = string.Empty;
                GlbReportName = string.Empty;

                //check whether current session is expired
                CheckSessionIsExpired();

                //check this user has permission for this PC
                if (txtPC.Text != string.Empty)
                {
                    Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(BaseCls.GlbUserComCode, txtPC.Text);
                    if (_IsValid == false)
                    {
                        MessageBox.Show("Invalid Profit Center.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                    //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, null, "REPS"))
                    //Add by Chamal 30-Aug-2013
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10044))
                    {
                        Int16 is_Access = CHNLSVC.Security.Check_User_PC(BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtPC.Text);
                        if (is_Access != 1)
                        {
                            //MessageBox.Show("Access Denied.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            MessageBox.Show("Sorry, You have no permission for view reports!\n( Advice: Required permission code :10044)", "Sales Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }

                BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Value).Date;
                BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Value).Date;
                BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Value).Date;

                btnDisplay.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                
                if (opt1.Checked == true)   //Brand Rep 01, Main Cat, Sub Cat, Brand, Model
                    BaseCls.GlbReportType = "001";
                if (opt2.Checked == true)   //Brand Rep 02, Main Cat, Sub Cat, Brand, Model, Item Code, Channel(Assigned to User)
                    BaseCls.GlbReportType = "002";
                if (opt3.Checked == true)   //Brand Rep 03, Main Cat, Sub Cat, Channel(Assigned to User)
                    BaseCls.GlbReportType = "003";
                if (opt4.Checked == true)   //Brand Rep 04, Main Cat, Sub Cat
                    BaseCls.GlbReportType = "004";
                if (opt5.Checked == true)   //Brand Rep 05, Showroom, Sub Cat
                    BaseCls.GlbReportType = "005";
                if (opt6.Checked == true)   //Brand Rep 06, Showroom, Main Cat
                    BaseCls.GlbReportType = "006";
                if (opt7.Checked == true)   //Brand Rep 07, Channel(Assigned to User), Showroom, Main Cat
                    BaseCls.GlbReportType = "007";
                if (opt8.Checked == true)   //Brand Rep 08, Channel(Assigned to User), Executive, Main Cat
                    BaseCls.GlbReportType = "008";
                if (opt9.Checked == true)   //Brand Rep 09, Channel(Assigned to User), Executive, Sub Cat
                    BaseCls.GlbReportType = "009";
                if (opt10.Checked == true)   //Brand Rep 10, Area, Region, Zone, Showroom, Main Cat
                    BaseCls.GlbReportType = "010";
                if (opt11.Checked == true)   //Brand Rep 11, Area, Region, Zone, Showroom, Sub Cat
                    BaseCls.GlbReportType = "011";
                if (opt12.Checked == true)   //Brand Rep 12, Chanel
                    BaseCls.GlbReportType = "012";


                if (opt1.Checked == true || opt2.Checked == true || opt3.Checked == true || opt4.Checked == true || opt5.Checked == true || opt6.Checked == true || opt7.Checked == true || opt8.Checked == true || opt9.Checked == true || opt10.Checked == true || opt11.Checked == true || opt12.Checked == true  )   //Common
                {
                    //update temporary table
                    update_PC_List();

                    string vItemCat1 = "";
                    string vItemCat2 = "";
                    string vItemCat3 = "";
                    string vItemcode = "";
                    string vBrand = "";

                    foreach (ListViewItem Item in lstCat1.Items)
                    {
                        vItemCat1 = vItemCat1 == "" ? "^" + Item.Text + "$" : vItemCat1 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstCat2.Items)
                    {
                        vItemCat2 = vItemCat2 == "" ? "^" + Item.Text + "$" : vItemCat2 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstCat3.Items)
                    {
                        vItemCat3 = vItemCat3 == "" ? "^" + Item.Text + "$" : vItemCat3 + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstItem.Items)
                    {
                        vItemcode = vItemcode == "" ? "^" + Item.Text + "$" : vItemcode + "|" + "^" + Item.Text + "$";
                    }
                    foreach (ListViewItem Item in lstBrand.Items)
                    {
                        vBrand = vBrand == "" ? "^" + Item.Text + "$" : vBrand + "|" + "^" + Item.Text + "$";
                    }

                    BaseCls.GlbReportCustomerCode = txtCust.Text;
                    BaseCls.GlbReportExecCode = txtExec.Text;
                    BaseCls.GlbReportDocType = txtDocType.Text;
                    BaseCls.GlbReportItemCode = vItemcode == "" ? txtItemCode.Text == "" ? txtItemCode.Text : "^" + txtItemCode.Text + "$" : vItemcode;
                    BaseCls.GlbReportBrand = vBrand == "" ? txtBrand.Text == "" ? txtBrand.Text : "^" + txtBrand.Text + "$" : vBrand;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = vItemCat1 == "" ? txtIcat1.Text == "" ? txtIcat1.Text : "^" + txtIcat1.Text + "$" : vItemCat1;
                    BaseCls.GlbReportItemCat2 = vItemCat2 == "" ? txtIcat2.Text == "" ? txtIcat2.Text : "^" + txtIcat2.Text + "$" : vItemCat2;
                    BaseCls.GlbReportItemCat3 = vItemCat3 == "" ? txtIcat3.Text == "" ? txtIcat3.Text : "^" + txtIcat3.Text + "$" : vItemCat3;                    

                    objBrand.BrandRep01();
                }

                if (opt15.Checked == true)  //Sales with Warranty Details
                { 
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportCustomerCode = "Y";

                    BaseCls.GlbReportHeading = "Sales with Warranty Details";

                    string _filePath = string.Empty;
                    string _error = string.Empty;
                    _filePath = CHNLSVC.Sales.GetSaleswithWarranty(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportCustomerCode, BaseCls.GlbUserID, out _error);

                    if (!string.IsNullOrEmpty(_error))
                    {
                        MessageBox.Show(_error);
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        return;
                    }

                    if (string.IsNullOrEmpty(_filePath))
                    {
                        MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        return;
                    }

                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();
                
                }

                if (opt16.Checked == true)  //Sales with Warranty Details - SCM
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportCustomerCode = "Y";

                    BaseCls.GlbReportHeading = "Sales with Warranty Details";

                    string _filePath = string.Empty;
                    string _error = string.Empty;
                    _filePath = CHNLSVC.Sales.GetSaleswithWarrantySCM(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportCustomerCode, BaseCls.GlbUserID, out _error);

                    if (!string.IsNullOrEmpty(_error))
                    {
                        MessageBox.Show(_error);
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        return;
                    }

                    if (string.IsNullOrEmpty(_filePath))
                    {
                        MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        return;
                    }

                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();

                }
                //if (opt24.Checked == true)  //Delivered Sales
                //{
                //    string vItemCat1 = "";
                //    string vItemCat2 = "";
                //    string vItemCat3 = "";
                //    string vItemcode = "";
                //    string vBrand = "";

                //    //update temporary table
                //    update_PC_List();

                //    foreach (ListViewItem Item in lstCat1.Items)
                //    {
                //        vItemCat1 = vItemCat1 == "" ? "^" + Item.Text + "$" : vItemCat1 + "|" + "^" + Item.Text + "$";
                //    }
                //    foreach (ListViewItem Item in lstCat2.Items)
                //    {
                //        vItemCat2 = vItemCat2 == "" ? "^" + Item.Text + "$" : vItemCat2 + "|" + "^" + Item.Text + "$";
                //    }
                //    foreach (ListViewItem Item in lstCat3.Items)
                //    {
                //        vItemCat3 = vItemCat3 == "" ? "^" + Item.Text + "$" : vItemCat3 + "|" + "^" + Item.Text + "$";
                //    }
                //    foreach (ListViewItem Item in lstItem.Items)
                //    {
                //        vItemcode = vItemcode == "" ? "^" + Item.Text + "$" : vItemcode + "|" + "^" + Item.Text + "$";
                //    }
                //    foreach (ListViewItem Item in lstBrand.Items)
                //    {
                //        vBrand = vBrand == "" ? "^" + Item.Text + "$" : vBrand + "|" + "^" + Item.Text + "$";
                //    }

                //    BaseCls.GlbReportCustomerCode = txtCust.Text;
                //    BaseCls.GlbReportExecCode = txtExec.Text;
                //    BaseCls.GlbReportDocType = txtDocType.Text;
                //    BaseCls.GlbReportItemCode = vItemcode == "" ? txtItemCode.Text == "" ? txtItemCode.Text : "^" + txtItemCode.Text + "$" : vItemcode;
                //    BaseCls.GlbReportBrand = vBrand == "" ? txtBrand.Text == "" ? txtBrand.Text : "^" + txtBrand.Text + "$" : vBrand;
                //    BaseCls.GlbReportModel = txtModel.Text;
                //    BaseCls.GlbReportItemCat1 = vItemCat1 == "" ? txtIcat1.Text == "" ? txtIcat1.Text : "^" + txtIcat1.Text + "$" : vItemCat1;
                //    BaseCls.GlbReportItemCat2 = vItemCat2 == "" ? txtIcat2.Text == "" ? txtIcat2.Text : "^" + txtIcat2.Text + "$" : vItemCat2;
                //    BaseCls.GlbReportItemCat3 = vItemCat3 == "" ? txtIcat3.Text == "" ? txtIcat3.Text : "^" + txtIcat3.Text + "$" : vItemCat3;
                //    BaseCls.GlbReportDoc = txtDocNo.Text;
                //    BaseCls.GlbReportItemStatus = txtItemStatus.Text;

                //    BaseCls.GlbReportType = "DSALE";
                //    BaseCls.GlbReportHeading = "DELIVERED SALES REPORT";

                //    int x = 0;
                //    foreach (ListViewItem Item in lstGroup.Items)
                //    {
                //        x++;
                //        if (Item.Text == "INV")
                //        {
                //            if (lstGroup.Items.Count > x)
                //            {
                //                MessageBox.Show("Document Number group should be the last group.", "Sales Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                btnDisplay.Enabled = true;
                //                Cursor.Current = Cursors.Default;
                //                return;
                //            }
                //        }
                //    }

                //    set_GroupOrder();

                //    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();

                //    BaseCls.GlbReportName = "DeliveredSalesReport.rpt";

                //    _view.Show();
                //    _view = null;
                //}

                btnDisplay.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message + "\nTry again!", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDisplay.Enabled = true;
            }
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
            //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPS"))
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10044))
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



        private void Sales_Rep_Load(object sender, EventArgs e)
        {

        }

        private void txtChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtChanel_DoubleClick(null, null);
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(txtComp.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + txtRegion.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + txtRegion.Text + seperator + txtZone.Text + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OutsideParty:
                    {
                        paramsText.Append("HP" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Country:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ReceiptType:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(txtIcat1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtIcat1.Text + seperator + txtIcat2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Sales_Type:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Sales_SubType:
                    {
                        paramsText.Append(txtDocType.Text.Trim().ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceExecutive:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Employee_Executive:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "SEX" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Prefix:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CircularByComp:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {

                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPB.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PromoByComp:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PurchaseOrder:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.POrder:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "C" + seperator + "A");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CashComCirc:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SystemUser:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void txtSChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtSChanel_DoubleClick(null, null);
            }
        }

        private void txtArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtArea_DoubleClick(null, null);
            }
        }

        private void txtRegion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtRegion_DoubleClick(null, null);
            }
        }

        private void txtZone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtZone_DoubleClick(null, null);
            }
        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtPC_DoubleClick(null, null);
            }
            if (e.KeyCode == Keys.Enter)
            {
                load_PCDesc();
            }
        }

        private void txtComp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtComp;
                _CommonSearch.ShowDialog();
                txtComp.Select();
            }
        }

        private void txtRecType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearch_Click(null, null);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReceiptType);
            DataTable _result = CHNLSVC.CommonSearch.GetReceiptTypes(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtRecType;
            _CommonSearch.ShowDialog();
            txtRecType.Select();
            chkRecType.Checked = false;
        }


        private void chkRecType_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRecType.Checked == true)
            {
                txtRecType.Text = "";
                txtRecType.Enabled = false;
                btnSearch.Enabled = false;
            }
            else
            {
                txtRecType.Enabled = true;
                btnSearch.Enabled = true;
            }
        }

        private void chk_Entry_Tp_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Entry_Tp.Checked == true)
            {
                txtEntryTp.Text = "";
                txtEntryTp.Enabled = false;
                btn_Srch_Entry.Enabled = false;
            }
            else
            {
                txtEntryTp.Enabled = true;
                btn_Srch_Entry.Enabled = true;
            }
        }

        private void chk_Doc_Tp_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Doc_Tp.Checked == true)
            {
                txtDocType.Text = "";
                txtDocType.Enabled = false;
                btn_Srch_Doc_Tp.Enabled = false;
            }
            else
            {
                txtDocType.Enabled = true;
                btn_Srch_Doc_Tp.Enabled = true;
            }
        }

        private void chk_Doc_Sub_Tp_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Doc_Sub_Tp.Checked == true)
            {
                txtDocSubType.Text = "";
                txtDocSubType.Enabled = false;
                btn_Srch_DocSubTp.Enabled = false;
            }
            else
            {
                txtDocSubType.Enabled = true;
                btn_Srch_DocSubTp.Enabled = true;
            }
        }

        private void chk_Doc_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Doc.Checked == true)
            {
                txtDocNo.Text = "";
                txtDocNo.Enabled = false;
                btn_Srch_Doc.Enabled = false;
            }
            else
            {
                txtDocNo.Enabled = true;
                btn_Srch_Doc.Enabled = true;
            }
        }

        private void chk_Dir_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Dir.Checked == true)
            {
                txtDirec.Text = "";
                txtDirec.Enabled = false;
                btn_Srch_Dir.Enabled = false;
            }
            else
            {
                txtDirec.Enabled = true;
                btn_Srch_Dir.Enabled = true;
            }
        }

        private void chk_ICat1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ICat1.Checked == true)
            {
                txtIcat1.Text = "";
                txtIcat1.Enabled = false;
                btn_Srch_Cat1.Enabled = false;
            }
            else
            {
                txtIcat1.Enabled = true;
                btn_Srch_Cat1.Enabled = true;
            }
        }

        private void chk_ICat2_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ICat2.Checked == true)
            {
                txtIcat2.Text = "";
                txtIcat2.Enabled = false;
                btn_Srch_Cat2.Enabled = false;
            }
            else
            {
                txtIcat2.Enabled = true;
                btn_Srch_Cat2.Enabled = true;
            }
        }

        private void chk_ICat3_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ICat3.Checked == true)
            {
                txtIcat3.Text = "";
                txtIcat3.Enabled = false;
                btn_Srch_Cat3.Enabled = false;
            }
            else
            {
                txtIcat3.Enabled = true;
                btn_Srch_Cat3.Enabled = true;
            }
        }

        private void chk_Item_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Item.Checked == true)
            {
                txtItemCode.Text = "";
                txtItemCode.Enabled = false;
                btn_Srch_Itm.Enabled = false;
            }
            else
            {
                txtItemCode.Enabled = true;
                btn_Srch_Itm.Enabled = true;
            }
        }

        private void chk_Brand_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Brand.Checked == true)
            {
                txtBrand.Text = "";
                txtBrand.Enabled = false;
                btn_Srch_Brnd.Enabled = false;
            }
            else
            {
                txtBrand.Enabled = true;
                btn_Srch_Brnd.Enabled = true;
            }
        }

        private void chk_Model_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Model.Checked == true)
            {
                txtModel.Text = "";
                txtModel.Enabled = false;
                btn_Srch_Model.Enabled = false;
            }
            else
            {
                txtModel.Enabled = true;
                btn_Srch_Model.Enabled = true;
            }
        }

        private void btn_Srch_Cat1_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtIcat1;
            _CommonSearch.txtSearchbyword.Text = txtIcat1.Text;
            _CommonSearch.ShowDialog();
            txtIcat1.Focus();
        }

        private void btn_Srch_Cat2_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtIcat2;
            _CommonSearch.txtSearchbyword.Text = txtIcat2.Text;
            _CommonSearch.ShowDialog();
            txtIcat2.Focus();
        }

        private void btn_Srch_Cat3_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtIcat3;
            _CommonSearch.txtSearchbyword.Text = txtIcat3.Text;
            _CommonSearch.ShowDialog();
            txtIcat3.Focus();
        }

        private void txtIcat1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Cat1_Click(null, null);
            }
        }

        private void txtIcat2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Cat2_Click(null, null);
            }
        }


        private void btn_Srch_Itm_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtItemCode;
            _CommonSearch.ShowDialog();
            txtItemCode.Focus();
        }


        private void btn_Srch_Brnd_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtBrand;
            _CommonSearch.txtSearchbyword.Text = txtBrand.Text;
            _CommonSearch.ShowDialog();
            txtBrand.Focus();
        }

        private void txtBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Brnd_Click(null, null);
            }
        }

        private void btn_Srch_Model_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
            DataTable _result = CHNLSVC.CommonSearch.GetAllModels(_CommonSearch.SearchParams, null, null);

            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtModel; //txtBox;
            _CommonSearch.ShowDialog();
            txtModel.Focus();
        }

        private void txtModel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Model_Click(null, null);
            }
        }

        private void btn_Srch_Doc_Tp_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_Type);
            DataTable _result = CHNLSVC.General.GetSalesTypes(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtDocType;
            _CommonSearch.txtSearchbyword.Text = txtDocType.Text;
            _CommonSearch.ShowDialog();
            txtDocType.Focus();
        }

        private void btn_Srch_DocSubTp_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_SubType);
            DataTable _result = CHNLSVC.CommonSearch.Get_sales_subtypes(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtDocSubType;
            _CommonSearch.ShowDialog();
            txtDocSubType.Focus();
        }

        private void txtDocType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Doc_Tp_Click(null, null);
            }
        }

        private void txtDocSubType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_DocSubTp_Click(null, null);
            }
        }


        private void txtFromDate_ValueChanged(object sender, EventArgs e)
        {
            BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Value);
        }

        private void txtAsAtDate_ValueChanged(object sender, EventArgs e)
        {
            BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Value);
        }
        
        private void txtToDate_ValueChanged(object sender, EventArgs e)
        {
            BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Value);
        }

        private void txtToDate_KeyDown(object sender, KeyEventArgs e)
        {
            //e.SuppressKeyPress = true;
        }

        private void txtFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            //e.SuppressKeyPress = true;
        }

        private void txtAsAtDate_KeyDown(object sender, KeyEventArgs e)
        {
            //e.SuppressKeyPress = true;
        }

        private void btnCat1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIcat1.Text))
            {
                for (int i = 0; i < lstCat1.Items.Count; i++)
                {
                    if (lstCat1.Items[i].Text == txtIcat1.Text)
                        return;
                }
                lstCat1.Items.Add((txtIcat1.Text).ToString());
                txtIcat1.Text = "";
            }

        }

        private void lstCat1_DoubleClick(object sender, EventArgs e)
        {
            for (int i = 0; i < lstCat1.Items.Count; i++)
            {
                if (lstCat1.Items[i].Selected)
                {
                    lstCat1.Items[i].Remove();
                    i--;
                }
            }
        }

        private void btnCat2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIcat2.Text))
            {
                for (int i = 0; i < lstCat2.Items.Count; i++)
                {
                    if (lstCat2.Items[i].Text == txtIcat2.Text)
                        return;
                }
                lstCat2.Items.Add((txtIcat2.Text).ToString());
                txtIcat2.Text = "";
            }
        }

        private void btnCat3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIcat3.Text))
            {
                for (int i = 0; i < lstCat3.Items.Count; i++)
                {
                    if (lstCat3.Items[i].Text == txtIcat3.Text)
                        return;
                }
                lstCat3.Items.Add((txtIcat3.Text).ToString());
                txtIcat3.Text = "";
            }
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCode.Text))
            {
                for (int i = 0; i < lstItem.Items.Count; i++)
                {
                    if (lstItem.Items[i].Text == txtItemCode.Text)
                        return;
                }
                lstItem.Items.Add((txtItemCode.Text).ToString());
                txtItemCode.Text = "";
            }
        }

        private void btnBrand_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBrand.Text))
            {
                for (int i = 0; i < lstBrand.Items.Count; i++)
                {
                    if (lstBrand.Items[i].Text == txtBrand.Text)
                        return;
                }
                lstBrand.Items.Add((txtBrand.Text).ToString());
                txtBrand.Text = "";
            }
        }


        private void lstCat1_DoubleClick_1(object sender, EventArgs e)
        {
            for (int i = 0; i < lstCat1.Items.Count; i++)
            {
                if (lstCat1.Items[i].Selected)
                {
                    lstCat1.Items[i].Remove();
                    i--;
                }
            }
        }

        private void lstCat2_DoubleClick_1(object sender, EventArgs e)
        {
            for (int i = 0; i < lstCat2.Items.Count; i++)
            {
                if (lstCat2.Items[i].Selected)
                {
                    lstCat2.Items[i].Remove();
                    i--;
                }
            }
        }

        private void lstCat3_DoubleClick_1(object sender, EventArgs e)
        {
            for (int i = 0; i < lstCat3.Items.Count; i++)
            {
                if (lstCat3.Items[i].Selected)
                {
                    lstCat3.Items[i].Remove();
                    i--;
                }
            }
        }

        private void lstItem_DoubleClick_1(object sender, EventArgs e)
        {
            for (int i = 0; i < lstItem.Items.Count; i++)
            {
                if (lstItem.Items[i].Selected)
                {
                    lstItem.Items[i].Remove();
                    i--;
                }
            }
        }

        private void lstBrand_DoubleClick_1(object sender, EventArgs e)
        {
            for (int i = 0; i < lstBrand.Items.Count; i++)
            {
                if (lstBrand.Items[i].Selected)
                {
                    lstBrand.Items[i].Remove();
                    i--;
                }
            }
        }

        private void lstGroup_DoubleClick(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Selected)
                {
                    lstGroup.Items[i].Remove();
                    i--;
                }
            }
        }

        private void lbl1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "CAT1")
                    return;
            }
            lstGroup.Items.Add(("CAT1").ToString());
        }

        private void lbl2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "CAT2")
                    return;
            }
            lstGroup.Items.Add(("CAT2").ToString());
        }

        private void lbl3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "CAT3")
                    return;
            }
            lstGroup.Items.Add(("CAT3").ToString());
        }

        private void lbl4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "ITM")
                    return;
            }
            lstGroup.Items.Add(("ITM").ToString());
        }

        private void lbl5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "BRND")
                    return;
            }
            lstGroup.Items.Add(("BRND").ToString());
        }


        private void txtModel_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Model_Click(null, null);
        }

        private void txtBrand_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Brnd_Click(null, null);
            }
        }

        private void txtBrand_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Brnd_Click(null, null);
        }

        private void txtItemCode_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Itm_Click(null, null);
        }

        private void txtItemCode_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Itm_Click(null, null);
            }
        }

        private void txtIcat3_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Cat3_Click(null, null);
        }

        private void txtIcat3_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Cat3_Click(null, null);
            }
        }

        private void txtIcat2_DoubleClick(object sender, EventArgs e)
        {

            btn_Srch_Cat2_Click(null, null);

        }

        private void txtIcat1_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Cat1_Click(null, null);
        }

        private void txtChanel_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtChanel;
            _CommonSearch.ShowDialog();
            txtChanel.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "CHNL", txtChanel.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtChnlDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtSChanel_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSChanel;
            _CommonSearch.ShowDialog();
            txtSChanel.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "SCHNL", txtSChanel.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtSChnlDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtArea_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtArea;
            _CommonSearch.ShowDialog();
            txtArea.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "AREA", txtArea.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtAreaDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtRegion_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtRegion;
            _CommonSearch.ShowDialog();
            txtRegion.Select();


            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "REGION", txtRegion.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtRegDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtZone_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtZone;
            _CommonSearch.ShowDialog();
            txtZone.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "ZONE", txtZone.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtZoneDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtPC_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPC;
            _CommonSearch.ShowDialog();
            txtPC.Select();

            load_PCDesc();
        }

        private void set_GroupOrder()
        {
            int i = 1;
            int j = lstGroup.Items.Count;
            BaseCls.GlbReportGroupProfit = 0;
            BaseCls.GlbReportGroupDOLoc = 0;
            BaseCls.GlbReportGroupDocType = 0;
            BaseCls.GlbReportGroupCustomerCode = 0;
            BaseCls.GlbReportGroupExecCode = 0;
            BaseCls.GlbReportGroupItemCode = 0;
            BaseCls.GlbReportGroupBrand = 0;
            BaseCls.GlbReportGroupModel = 0;
            BaseCls.GlbReportGroupItemCat1 = 0;
            BaseCls.GlbReportGroupItemCat2 = 0;
            BaseCls.GlbReportGroupItemCat3 = 0;
            BaseCls.GlbReportGroupLastGroup = 0;
            BaseCls.GlbReportGroupInvoiceNo = 0;
            BaseCls.GlbReportGroupItemStatus = 0;
            BaseCls.GlbReportGroupLastGroupCat = "";

            foreach (ListViewItem Item in lstGroup.Items)
            {
                if (Item.Text == "PC")
                {
                    BaseCls.GlbReportGroupProfit = i;
                }
                if (Item.Text == "DLOC")
                {
                    BaseCls.GlbReportGroupDOLoc = i;
                }
                if (Item.Text == "DTP")
                {
                    BaseCls.GlbReportGroupDocType = i;
                }
                if (Item.Text == "CUST")
                {
                    BaseCls.GlbReportGroupCustomerCode = i;
                }
                if (Item.Text == "EXEC")
                {
                    BaseCls.GlbReportGroupExecCode = i;
                }

                if (Item.Text == "ITM")
                {
                    BaseCls.GlbReportGroupItemCode = i;
                }
                if (Item.Text == "BRND")
                {
                    BaseCls.GlbReportGroupBrand = i;
                }
                if (Item.Text == "MDL")
                {
                    BaseCls.GlbReportGroupModel = i;
                }
                if (Item.Text == "CAT1")
                {
                    BaseCls.GlbReportGroupItemCat1 = i;
                }
                if (Item.Text == "CAT2")
                {
                    BaseCls.GlbReportGroupItemCat2 = i;
                }
                if (Item.Text == "CAT3")
                {
                    BaseCls.GlbReportGroupItemCat3 = i;
                }
                if (Item.Text == "INV")
                {
                    BaseCls.GlbReportGroupInvoiceNo = i;
                }
                if (Item.Text == "STK")
                {
                    BaseCls.GlbReportGroupItemStatus = i;
                }
                BaseCls.GlbReportGroupLastGroup = j;
                if (j == i)
                {
                    BaseCls.GlbReportGroupLastGroupCat = Item.Text;
                }
                i++;
            }
            if (j == 0)
            {
                BaseCls.GlbReportGroupProfit = 1;
                BaseCls.GlbReportGroupItemCode = 2;
                BaseCls.GlbReportGroupLastGroup = 2;
                BaseCls.GlbReportGroupLastGroupCat = "ITM";
            }
        }

        private void lbl6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "MDL")
                    return;
            }
            lstGroup.Items.Add(("MDL").ToString());
        }

        private void lbl7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "PC")
                    return;
            }
            lstGroup.Items.Add(("PC").ToString());
        }

        private void lbl8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "DTP")
                    return;
            }
            lstGroup.Items.Add(("DTP").ToString());
        }

        private void lbl9_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "DSTP")
                    return;
            }
            lstGroup.Items.Add(("DSTP").ToString());
        }

        private void lbl10_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "CUST")
                    return;
            }
            lstGroup.Items.Add(("CUST").ToString());
        }

        private void chk_Cust_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Cust.Checked == true)
            {
                txtCust.Text = "";
                txtCust.Enabled = false;
                btn_Srch_Cust.Enabled = false;
            }
            else
            {
                txtCust.Enabled = true;
                btn_Srch_Cust.Enabled = true;
            }
        }

        private void btn_Srch_Cust_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCust;
            _CommonSearch.ShowDialog();
            txtCust.Select();
        }

        private void lbl11_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "EXEC")
                    return;
            }
            lstGroup.Items.Add(("EXEC").ToString());
        }

        private void btn_Srch_Exec_Click(object sender, EventArgs e)
        {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceExecutive);
                DataTable _result = CHNLSVC.CommonSearch.SearchEmployeeAssignToProfitCenter(_CommonSearch.SearchParams, null, null);
                if (_result == null || _result.Rows.Count <= 0)
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Employee_Executive);
                    _result = CHNLSVC.CommonSearch.GetEmployeeData(_CommonSearch.SearchParams, null, null);
                }
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtExec;
                _CommonSearch.ShowDialog();
                txtExec.Select();

        }

        private void lbl12_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "STK")
                    return;
            }
            lstGroup.Items.Add(("STK").ToString());
        }

        private void btn_Srch_Itm_Stus_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
            DataTable _result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtItemStatus;
            _CommonSearch.ShowDialog();
            txtItemStatus.Focus();
        }

        private void lbl13_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "INV")
                    return;
            }
            lstGroup.Items.Add(("INV").ToString());
        }

        private void chk_Exec_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Exec.Checked == true)
            {
                txtExec.Text = "";
                txtExec.Enabled = false;
                btn_Srch_Exec.Enabled = false;
            }
            else
            {
                txtExec.Enabled = true;
                btn_Srch_Exec.Enabled = true;
            }
        }

        private void chk_Itm_Stus_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Itm_Stus.Checked == true)
            {
                txtItemStatus.Text = "";
                txtItemStatus.Enabled = false;
                btn_Srch_Itm_Stus.Enabled = false;
            }
            else
            {
                txtItemStatus.Enabled = true;
                btn_Srch_Itm_Stus.Enabled = true;
            }
        }

        private void btn_Srch_Prefix_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Prefix);
            DataTable _result = CHNLSVC.CommonSearch.GetPrefixData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPrefix;
            _CommonSearch.ShowDialog();
            txtPrefix.Focus();
        }

        private void chkPrefix_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPrefix.Checked == true)
            {
                txtPrefix.Text = "";
                txtPrefix.Enabled = false;
                btn_Srch_Prefix.Enabled = false;
            }
            else
            {
                txtPrefix.Enabled = true;
                btn_Srch_Prefix.Enabled = true;
            }
        }

        private void txtPrefix_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Prefix_Click(null, null);
            }
        }

        private void txtPrefix_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Prefix_Click(null, null);
        }

        private void txtCust_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Cust_Click(null, null);
            }
        }

        private void txtCust_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Cust_Click(null, null);
        }

        private void chk_Pay_Tp_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Pay_Tp.Checked == true)
            {
                comboBoxPayModes.SelectedIndex = -1;
                comboBoxPayModes.Enabled = false;

            }
            else
            {
                comboBoxPayModes.Enabled = true;

            }
        }

        private void chkAsAtDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAsAtDate.Checked == false)
            {
                txtAsAtDate.Enabled = false;
                pnlDateRange.Enabled = true;
            }
            else
            {
                txtAsAtDate.Enabled = true;
                pnlDateRange.Enabled = false;
            }
        }

        private void btn_Srch_Doc_Click(object sender, EventArgs e)
        {
            if (label18.Text == "Circular No")
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CircularByComp);
                DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchDataByComp(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDocNo;
                _CommonSearch.ShowDialog();
                txtDocNo.Select();
            }
        }

        private void btn_PB_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
            DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPB;
            _CommonSearch.ShowDialog();
            txtPB.Select();
        }

        private void btn_PBLevel_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
            DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPBLevel;
            _CommonSearch.ShowDialog();
            txtPBLevel.Select();
        }

        private void btn_Srch_Dir_Click(object sender, EventArgs e)
        {
            if (label16.Text == "Promo Code")
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromoByComp);
                DataTable _result = CHNLSVC.CommonSearch.GetSearchDataForPromoByComp(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDirec;
                _CommonSearch.ShowDialog();
                txtDirec.Select();
            }
        }

        private void chk_PB_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_PB.Checked == true)
            {
                txtPB.Text = "";
                txtPB.Enabled = false;
                btn_PB.Enabled = false;
            }
            else
            {
                txtPB.Enabled = true;
                btn_PB.Enabled = true;
            }
        }

        private void chk_PBLevel_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_PBLevel.Checked == true)
            {
                txtPBLevel.Text = "";
                txtPBLevel.Enabled = false;
                btn_PBLevel.Enabled = false;
            }
            else
            {
                txtPBLevel.Enabled = true;
                btn_PBLevel.Enabled = true;
            }
        }

        private void lbl14_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "DLOC")
                    return;
            }
            lstGroup.Items.Add(("DLOC").ToString());
        }

        private void txtDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Doc_Click(null, null);
            }
        }

        private void txtDocNo_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Doc_Click(null, null);
        }

        private void txtDirec_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Dir_Click(null, null);
        }

        private void txtDirec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Dir_Click(null, null);
            }
        }



        private void update_PC_List_RPTDB()
        {
            string _tmpPC = "";
            BaseCls.GlbReportProfit = "";

            Boolean _isPCFound = false;
            Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, null, null);

            foreach (ListViewItem Item in lstPC.Items)
            {
                string pc = Item.Text;

                if (Item.Checked == true)
                {
                    Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, pc, null);

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
                Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC_RPTDB(BaseCls.GlbUserID, txtComp.Text, txtPC.Text, null);
            }
        }

        private void optLs_CheckedChanged(object sender, EventArgs e)
        {
            if (optLs.Checked == true)
            {
                optEq.Checked = false;
                optGt.Checked = false;
                txtDiscRate.Enabled = true;
            }
        }

        private void optEq_CheckedChanged(object sender, EventArgs e)
        {
            if (optEq.Checked == true)
            {
                optLs.Checked = false;
                optGt.Checked = false;
                txtDiscRate.Enabled = true;
            }
        }

        private void optGt_CheckedChanged(object sender, EventArgs e)
        {
            if (optGt.Checked == true)
            {
                optLs.Checked = false;
                optEq.Checked = false;
                txtDiscRate.Enabled = true;
            }
        }

        private void optAll_CheckedChanged(object sender, EventArgs e)
        {

            if (optAll.Checked == true)
            {
                txtDiscRate.Text = "";
                txtDiscRate.Enabled = false;
            }
        }

        private void chk_Stus_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Stus.Checked == true)
            {
                cmb_Stus.SelectedIndex = -1;
                cmb_Stus.Enabled = false;

            }
            else
            {
                cmb_Stus.Enabled = true;

            }
        }

        private void chk_Sup_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Sup.Checked == true)
            {
                txtSup.Text = "";
                txtSup.Enabled = false;
                btn_Srch_Sup.Enabled = false;
            }
            else
            {
                txtSup.Enabled = true;
                btn_Srch_Sup.Enabled = true;
            }
        }

        private void btn_Srch_Sup_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
            DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSup;
            _CommonSearch.ShowDialog();
            txtSup.Select();
        }

        private void chk_PO_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_PO.Checked == true)
            {
                txtPO.Text = "";
                txtPO.Enabled = false;
                btn_Srch_PO.Enabled = false;
            }
            else
            {
                txtPO.Enabled = true;
                btn_Srch_PO.Enabled = true;
            }
        }

        private void btn_Srch_PO_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PurchaseOrder);
            DataTable _result = CHNLSVC.CommonSearch.GetPurchaseOrders(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPO;
            _CommonSearch.ShowDialog();
            txtPO.Select();
        }

        private void fldgOpenPath_FileOk(object sender, CancelEventArgs e)
        {
            BaseCls.GlbReportFilePath = fldgOpenPath.FileName;
        }

        private void txtPC_Leave(object sender, EventArgs e)
        {
            load_PCDesc();
        }

        private void load_PCDesc()
        {
            txtPCDesn.Text = "";
            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "PC", txtPC.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtPCDesn.Text = row2["descp"].ToString();
            }
        }

        private void opt1_CheckedChanged(object sender, EventArgs e)
        {
            if (opt1.Checked == true)
            {                
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6501))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :6501)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(1);
            }
        }

        private void opt2_CheckedChanged(object sender, EventArgs e)
        {
            if (opt2.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6502))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :6502)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(1);
            }
        }

        private void opt3_CheckedChanged(object sender, EventArgs e)
        {
            if (opt3.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6503))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :6503)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(1);
            }
        }

        private void opt4_CheckedChanged(object sender, EventArgs e)
        {
            if (opt4.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6504))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :6504)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(1);
            }
        }

        private void opt5_CheckedChanged(object sender, EventArgs e)
        {
            if (opt5.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6505))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :6505)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(1);
            }
        }

        private void opt6_CheckedChanged(object sender, EventArgs e)
        {
            if (opt6.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6506))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :6506)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(1);
            }
        }

        private void opt7_CheckedChanged(object sender, EventArgs e)
        {
            if (opt7.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6507))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :6507)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(1);
            }
        }

        private void opt8_CheckedChanged(object sender, EventArgs e)
        {
            if (opt8.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6508))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :6508)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(1);
            }
        }

        private void opt9_CheckedChanged(object sender, EventArgs e)
        {
            if (opt9.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6509))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :6509)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(1);
            }
        }

        private void opt10_CheckedChanged(object sender, EventArgs e)
        {
            if (opt10.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6510))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :6510)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(1);
            }
        }

        private void opt11_CheckedChanged(object sender, EventArgs e)
        {
            if (opt11.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6511))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :6511)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(1);
            }
        }

        private void opt12_CheckedChanged(object sender, EventArgs e)
        {
            if (opt12.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6512))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :6512)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(12);
            }
        }

        private void opt13_CheckedChanged(object sender, EventArgs e)
        {
            if (opt13.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6513))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :6513)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(13);
            }
        }

        private void opt14_CheckedChanged(object sender, EventArgs e)
        {
            if (opt14.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6514))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :6514)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(14);
            }
        }

        private void opt15_CheckedChanged(object sender, EventArgs e)
        {
            if (opt15.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6515))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :6515)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(15);
            }
        }

        private void opt16_CheckedChanged(object sender, EventArgs e)
        {
            if (opt16.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6516))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :6516)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(16);
            }
        }


    }
}


