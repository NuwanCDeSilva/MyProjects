using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;
using System.Threading;
using System.Collections;
using FF.WindowsERPClient.Reports.Reconciliation;

//Written By kapila on 26/12/2012
namespace FF.WindowsERPClient.Reports.Reconciliation
{
    public partial class Reconcile_Rep : Base
    {
        clsRecon objRecon = new clsRecon();
        public bool CheckPermission = true;
        public bool _isProcessed = false;   //kapila 12/5/2017

        public Reconcile_Rep()
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
            //pnlItem.Enabled = false;
            pnl_Circular.Enabled = false;
            pnl_Circular.Visible = false;
            pnl_comp.Visible = false;
            pnl_comp.Enabled = false;
            label18.Text = "Document No";
            BaseCls.GlbReportViewPC = false;
            txtFromDate.Enabled = true;
            txtToDate.Enabled = true;
            pnl_Export.Visible = false;
            pnl_Export.Enabled = false;
            chk_Export.Checked = false;
            pnlInsu.Enabled = false;
            pnl_asatdate.Visible = false;
            pnl_regtp.Visible = false;
            optPC.Text = "Profit Center-wise";
            optDebtor.Text = "Debtor-wise";
            optProfit.Text = "Profit Center-wise";
            OptItem.Text = "Item-wise";
            panel5.Enabled = false;
            panel5.Visible = false;
            pnl_appstatus.Visible = false;
            pnl_appstatus.Enabled = false;
            pnl_Reqtp.Visible = true;
            pnl_Reqtp.Enabled = true;
            opt_pend.Visible = true;
            opt_cancel.Visible = true;
            opt_fin.Visible = true;
            pnl_Entry_Tp.Visible = true;
            pnlReason.Visible = false;
            panel6.Visible = false;
            pnlReg.Enabled = false;

            switch (_index)
            {
                case 2:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = false;
                        BaseCls.GlbReportViewPC = true;
                        break;
                    }
                case 3:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = false;
                        break;
                    }
                case 4:
                    {
                        pnlDateRange.Enabled = false;
                        pnlAsAtDate.Enabled = true;
                        break;
                    }
                case 5:
                    {
                        pnlDateRange.Enabled = false;
                        pnl_DocNo.Enabled = true;
                        label18.Text = "Circular No";
                        break;
                    }
                case 6:
                    {
                        //Added by dilshan on 20/19/2017************
                        pnlDateRange.Enabled = true;
                        pnl_Circular.Enabled = true;
                        pnl_Circular.Visible = true;
                        break;
                        //************************************

                        //commented by dilshan on 20/19/2017************
                        //pnlDateRange.Enabled = false;
                        //pnl_DocNo.Enabled = true;
                        //label18.Text = "Circular No";
                        //break;
                        //************************************
                    }
                case 7:
                    {
                        pnlDateRange.Enabled = true;
                        BaseCls.GlbReportViewPC = true;
                        break;
                    }
                case 8:
                    {
                        pnlDateRange.Enabled = true;
                        pnl_Circular.Enabled = true;
                        pnl_Circular.Visible = true;
                        pnl_Export.Visible = false;
                        pnl_Export.Enabled = false;
                        chk_Export.Checked = true;
                        pnl_Item.Enabled = true;
                        break;
                    }
                case 9:
                    {
                        pnlDateRange.Enabled = false;
                        BaseCls.GlbReportViewPC = true;
                        break;
                    }
                case 10:
                    {
                        pnlDateRange.Enabled = true;
                        BaseCls.GlbReportViewPC = true;
                        break;
                    }
                case 11:
                    {
                        pnlDateRange.Enabled = true;
                        BaseCls.GlbReportViewPC = true;
                        break;
                    }
                case 12:
                    {
                        pnlDateRange.Enabled = false;
                        BaseCls.GlbReportViewPC = true;
                        pnl_Item.Enabled = true;
                        pnlAsAtDate.Enabled = true;
                        pnlInsu.Enabled = true;
                        break;
                    }
                case 13:
                    {
                        pnlDateRange.Enabled = false;
                        pnl_Item.Enabled = true;
                        pnlAsAtDate.Enabled = false;
                        break;
                    }
                case 14:
                    {
                        pnlDateRange.Enabled = false;
                        pnlAsAtDate.Enabled = false;
                        break;
                    }
                case 15:
                    {
                        pnlDateRange.Enabled = false;
                        pnlAsAtDate.Enabled = false;
                        break;
                    }
                case 16:
                    {
                        pnl_DocNo.Enabled = true;
                        label18.Text = "Doc Type";
                        pnlDateRange.Enabled = true;
                        pnlAsAtDate.Enabled = false;
                        break;
                    }
                case 17:
                    {
                        pnl_DocNo.Enabled = true;
                        label18.Text = "Circular No";
                        pnlDateRange.Enabled = true;
                        pnlAsAtDate.Enabled = true;
                        pnl_asatdate.Visible = true;
                        pnlDateRange.Enabled = false;
                        txtAsAtDate.Enabled = true;
                        break;
                    }
                case 18:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        BaseCls.GlbReportViewPC = true;
                        pnl_regtp.Visible = true;
                        break;
                    }
                case 19:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        BaseCls.GlbReportViewPC = true;
                        pnl_appstatus.Visible = true;
                        pnl_appstatus.Enabled = true;
                        pnl_Reqtp.Visible = true;
                        pnl_Reqtp.Enabled = true;
                        pnl_DocType.Enabled = true;
                        break;
                    }
                case 20:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        BaseCls.GlbReportViewPC = true;

                        pnl_appstatus.Visible = true;
                        pnl_appstatus.Enabled = true;
                        opt_pend.Visible = false;
                        opt_cancel.Visible = false;
                        opt_fin.Visible = false;

                        pnl_Reqtp.Visible = true;
                        pnl_Reqtp.Enabled = true;

                        pnl_DocType.Enabled = true;
                        pnl_Entry_Tp.Visible = false;
                        pnl_user.Visible = true;
                        chk_user.Visible = false;
                        txt_user.Enabled = true;
                        btn_user.Enabled = true;
                        opt_app.Checked = true;
                        break;
                    }
                case 21:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnl_asatdate.Visible = true;
                        pnlDateRange.Enabled = false;
                        BaseCls.GlbReportViewPC = true;
                        break;
                    }
                case 22:
                    {
                        pnlDateRange.Enabled = false;
                        BaseCls.GlbReportViewPC = true;
                        pnl_Item.Enabled = true;
                        pnlAsAtDate.Enabled = false;
                        pnlInsu.Enabled = true;
                        pnl_Circular.Enabled = true;
                        pnl_Circular.Visible = true;
                        break;
                    }
                case 23:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        BaseCls.GlbReportViewPC = true;
                        pnl_appstatus.Visible = true;
                        pnl_appstatus.Enabled = true;
                        pnlReason.Visible = true;
                        pnlReason.Enabled = true;
                        pnl_DocType.Enabled = true;
                        pnl_Item.Enabled = true;
                        break;
                    }
                case 24:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = false;

                        break;
                    }
                case 25:
                    {
                        pnlAsAtDate.Enabled = false;


                        break;
                    }
                case 26:
                    {
                        pnlReg.Enabled = true;
                        pnl_Export.Enabled = true;
                        pnl_Export.Visible = true;
                        pnl_comp.Enabled = true;
                        pnl_comp.Visible = true;
                        break;
                    }
                case 27:
                    {
                        break;
                    }
                case 28:
                    {
                        pnlDateRange.Enabled = true;
                        break;
                    }
                case 30:
                    {
                        pnlDateRange.Enabled = true;
                        pnlReg.Visible = true;
                        pnlReg.Enabled = true;
                        break;
                    }

                case 31:
                    {
                        pnl_DocNo.Enabled = true;
                        label18.Text = "Circular No";
                        pnlDateRange.Enabled = true;
                        pnlAsAtDate.Enabled = true;
                        pnl_asatdate.Visible = false;
                        pnlDateRange.Enabled = false;
                        txtAsAtDate.Enabled = true;
                        break;
                    }
                case 32:
                    {
                        pnlDateRange.Enabled = true;
                        break;
                    }
                case 33:
                    {
                        pnl_Direc.Visible = false;
                        panel6.Enabled = true;
                        panel6.Visible = true;
                        break;
                    }
                case 34:
                    {
                        pnl_Direc.Visible = false;
                        panel6.Enabled = false;
                        //panel6.Visible = false;
                        pnl_user.Enabled = false;
                        pnl_Reqtp.Enabled = false;
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

        private void update_Channel_List()
        {
            string _tmpPC = "";
            BaseCls.GlbReportProfit = "";

            Boolean _isPCFound = false;
            Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC(BaseCls.GlbUserID, txtComp.Text, null, null);


            BaseCls.GlbReportProfit = txtPC.Text;
            Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC(BaseCls.GlbUserID, txtComp.Text, txtChanel.Text, null);

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

        //Update by Akila (make public)
        public void btnDisplay_Click(object sender, EventArgs e)
        {
            btnClear.Focus();
            //check whether current session is expired
            CheckSessionIsExpired();

            //kapila 4/7/2014
            if (CheckServerDateTime() == false) return;

            //check this user has permission for this Loc
            if (txtPC.Text != string.Empty)
            {
                string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPR"))
                //Add by Chamal 30-Aug-2013

                //Add by akila - 2017/05/17 ignore the permission cheking option in stockverification screen
                if (CheckPermission) // default value is true
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10047))
                    {
                        Int16 is_Access = 0;
                        if (BaseCls.GlbReportViewPC == false)
                        {
                            is_Access = CHNLSVC.Security.Check_User_Loc(BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtPC.Text);
                            if (is_Access != 1)
                            {
                                //MessageBox.Show("Access Denied.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MessageBox.Show("Sorry, You have no permission for view reports!\n( Advice: Required permission code :10047)", "Reconcile Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                        else
                        {
                            is_Access = CHNLSVC.Security.Check_User_PC(BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtPC.Text);
                            if (is_Access != 1)
                            {
                                MessageBox.Show("Access Denied.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                    }
                }
                //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10047))
                //{
                //    Int16 is_Access = 0;
                //    if (BaseCls.GlbReportViewPC == false)
                //    {
                //        is_Access = CHNLSVC.Security.Check_User_Loc(BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtPC.Text);
                //        if (is_Access != 1)
                //        {
                //            //MessageBox.Show("Access Denied.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //            MessageBox.Show("Sorry, You have no permission for view reports!\n( Advice: Required permission code :10047)", "Reconcile Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        is_Access = CHNLSVC.Security.Check_User_PC(BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtPC.Text);
                //        if (is_Access != 1)
                //        {
                //            MessageBox.Show("Access Denied.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //            return;
                //        }
                //    }
                //}
            }

            btnDisplay.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;

            BaseCls.GlbReportName = string.Empty;
            GlbReportName = string.Empty;

            if (opt1.Checked == true)
            {   //update temporary table
                update_PC_List();
                //04-03-13 Nadeeka
                BaseCls.GlbReportProfit = txtPC.Text;
                BaseCls.GlbReportComp = txtComp.Text;
                BaseCls.GlbReportCompCode = txtComp.Text;



                BaseCls.GlbReportName = "TransactionVariance.rpt";
                Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
                _view.Show();
                _view = null;
            }

            if (opt2.Checked == true)
            {   //update temporary table
                update_PC_List();
                //09-04-13 Sanjeewa

                BaseCls.GlbReportHeading = "LAST NUMBER SEQUENCE REPORT - PROFITCENTER";
                BaseCls.GlbReportDocType = "PC";
                if (_isProcessed == true)
                {
                    BaseCls.GlbReportParaLine1 = 1;
                    BaseCls.GlbReportProfit = txtPC.Text;
                }
                else
                    BaseCls.GlbReportParaLine1 = 0;

                BaseCls.GlbReportName = "Last_No_Seq_Rep.rpt";
                Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
                _view.Show();
                _view = null;
            }

            if (opt3.Checked == true)
            {   //update temporary table
                update_PC_List();
                //09-04-13 Sanjeewa

                BaseCls.GlbReportHeading = "LAST NUMBER SEQUENCE REPORT - LOCATION";
                BaseCls.GlbReportDocType = "INV";
                if (_isProcessed == true)
                {
                    BaseCls.GlbReportParaLine1 = 1;
                    BaseCls.GlbReportProfit = txtPC.Text;
                }
                else
                    BaseCls.GlbReportParaLine1 = 0;

                BaseCls.GlbReportName = "Last_No_Seq_Rep.rpt";
                Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
                _view.Show();
                _view = null;
            }

            if (opt4.Checked == true)
            {   //update temporary table
                update_PC_List();
                //10-04-13 Sanjeewa

                BaseCls.GlbReportHeading = "LAST TRANSACTION DATE AND TIME LOG REPORT";

                BaseCls.GlbReportName = "Latest_Day_End_Log.rpt";
                Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
                _view.Show();
                _view = null;
            }

            if (opt5.Checked == true)
            {   //update temporary table
                update_PC_List();
                //10-05-13 Sanjeewa

                BaseCls.GlbReportHeading = "SCHEME CREATION DETAILS REPORT";
                BaseCls.GlbReportDoc = txtDocNo.Text;

                BaseCls.GlbReportName = "Scheme_Creation_Dtl_Report.rpt";
                Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
                _view.Show();
                _view = null;
            }
            if (opt6.Checked == true)
            {   //update temporary table
                txtDocNo.Text = txtCircular.Text;
                //if (txtDocNo.Text == "")
                //{
                //    MessageBox.Show("Select Circular.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                update_PC_List();
                //Nadeeka 19-07-2013
                BaseCls.GlbReportComp = txtComp.Text;
                BaseCls.GlbReportHeading = "VEHICLE REGISTRATION DEFINITION";
                //************
                BaseCls.GlbReportFromDate = txtFromDate.Value;
                BaseCls.GlbReportToDate = txtToDate.Value;
                //************
                BaseCls.GlbReportDocType = txtDocNo.Text;
                BaseCls.GlbReportName = "VehicleRegDefinition.rpt";
                Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
                _view.Show();
                _view = null;
            }

            if (opt7.Checked == true)
            {   //update temporary table
                update_PC_List();
                //16-09-13 Sanjeewa

                BaseCls.GlbReportHeading = "REQUEST APPROVAL DETAILS REPORT";

                BaseCls.GlbReportType = "";
                BaseCls.GlbStatus = "";
                BaseCls.GlbReportName = "Request_Approval_Details.rpt";
                Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
                _view.Show();
                _view = null;
            }

            if (opt8.Checked == true)
            {   //07-12-13 Sanjeewa

                BaseCls.GlbReportHeading = "DISCOUNT DETAILS REPORT";

                //if (chk_Export.Checked == true)
                //{
                //    fldgOpenPath.ShowDialog();
                //}
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                BaseCls.GlbReportIsExport = chk_Export.Checked == true ? 1 : 0;

                BaseCls.GlbReportDoc = txtCircular.Text;
                objRecon.DiscountDetailReport();
                //BaseCls.GlbReportName = "discount_report.rpt";
                //Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
                //_view.Show();
                //_view = null;

                //if (chk_Export.Checked == true)
                //{
                //    string sourcefileName = BaseCls.GlbUserID + ".xls";
                //    string targetfileName = ".xls";
                //    string sourcePath = @"\\192.168.1.222\scm2\Print";
                //    string targetPath = BaseCls.GlbReportFilePath;
                //    string sourceFile = System.IO.Path.Combine(sourcePath, sourcefileName);
                //    string targetFile = targetPath + targetfileName;

                //    System.IO.File.Copy(sourceFile, targetFile);
                //    System.IO.File.Delete(sourceFile);
                //}
            }

            if (opt9.Checked == true)
            {   //27-12-13 Sanjeewa

                //update temporary table
                update_PC_List();

                BaseCls.GlbReportHeading = "PROFITCENTER MASTER";

                chk_Export.Checked = true;
                if (chk_Export.Checked == true)
                {
                    fldgOpenPath.ShowDialog();
                }

                BaseCls.GlbReportIsExport = chk_Export.Checked == true ? 1 : 0;

                //BaseCls.GlbReportName = "discount_report.rpt";
                //Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
                //_view.Show();
                //_view = null;

                if (chk_Export.Checked == true)
                {
                    objRecon.ProfitcenterMasterReport();
                    string sourcefileName = BaseCls.GlbUserID + ".xls";
                    string targetfileName = ".xls";
                    string sourcePath = @"\\192.168.1.222\scm2\Print";
                    string targetPath = BaseCls.GlbReportFilePath;
                    string sourceFile = System.IO.Path.Combine(sourcePath, sourcefileName);
                    string targetFile = targetPath + targetfileName;

                    System.IO.File.Copy(sourceFile, targetFile);
                    System.IO.File.Delete(sourceFile);

                    MessageBox.Show("Export Completed", "Reconciliation Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (opt10.Checked == true)
            {   //update temporary table
                update_PC_List();
                //30-12-13 Sanjeewa

                BaseCls.GlbReportHeading = "RECIEVING DESK SUMMARY";

                BaseCls.GlbReportName = "Rec_Desk_Sum_Report.rpt";
                Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
                _view.Show();
                _view = null;
            }

            if (opt12.Checked == true)
            {   //update temporary table
                string _PB = string.Empty;
                string _PBLevel = string.Empty;
                string _Pol = string.Empty;
                string _Party = string.Empty;
                string _InsCom = string.Empty;

                //kapila
                if (!chk_PB.Checked)
                    if (string.IsNullOrEmpty(Convert.ToString(txtPB.Text)))
                    {
                        MessageBox.Show("Please select the price book", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                if (!chk_PBLevel.Checked)
                    if (string.IsNullOrEmpty(Convert.ToString(txtPBLevel.Text)))
                    {
                        MessageBox.Show("Please select the price level", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                if (!chkInsCom.Checked)
                    if (string.IsNullOrEmpty(Convert.ToString(txtInsCom.Text)))
                    {
                        MessageBox.Show("Please select the insurance company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                if (!chkParty.Checked)
                    if (string.IsNullOrEmpty(Convert.ToString(txtHierchCode.Text)))
                    {
                        MessageBox.Show("Please select the heirachy", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                if (!chkPol.Checked)
                    if (string.IsNullOrEmpty(Convert.ToString(txtInsPol.Text)))
                    {
                        MessageBox.Show("Please select the policy", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                if (!chk_PB.Checked) BaseCls.GlbReportPriceBook = Convert.ToString(txtPB.Text); ;
                if (!chk_PBLevel.Checked) BaseCls.GlbReportPBLevel = Convert.ToString(txtPBLevel.Text);
                if (!chkInsCom.Checked) BaseCls.GlbReportInsComp = Convert.ToString(txtInsCom.Text);
                if (!chkPol.Checked) BaseCls.GlbReportPolCode = Convert.ToString(txtInsPol.Text);
                if (!chkParty.Checked) BaseCls.GlbReportPartyCode = Convert.ToString(txtHierchCode.Text);


                update_PC_List();
                //30-12-13 Sanjeewa
                BaseCls.GlbReportHeading = "INSURANCE DEFINITION";
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportAsAtDate = txtAsAtDate.Value.Date;



                BaseCls.GlbReportName = "InsuDef.rpt";
                Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
                _view.Show();
                _view = null;
            }

            if (opt13.Checked == true) //Restricted Items
            {
                //24-06-2014 Sanjeewa
                update_PC_List();

                BaseCls.GlbReportHeading = "RESTRICTED ITEMS";

                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportBrand = txtBrand.Text;
                BaseCls.GlbReportModel = txtModel.Text;
                BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                BaseCls.GlbReportItemCat3 = txtIcat3.Text;

                BaseCls.GlbReportName = "Item_Restr_Report.rpt";
                Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
                _view.Show();
                _view = null;
            }

            if (opt14.Checked == true) //Deposite Bank Definition
            {
                //29-08-2014 Sanjeewa
                update_PC_List();

                BaseCls.GlbReportHeading = "DEPOSITE BANK DEFINITION";

                BaseCls.GlbReportName = "Dep_Bank_Def_Report.rpt";
                Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
                _view.Show();
                _view = null;
            }
            if (opt15.Checked == true) //Merchant Id Definition
            {
                //12-09-2014 shanuka
                update_PC_List();

                BaseCls.GlbReportHeading = "Merchant Id Definition";

                BaseCls.GlbReportName = "Merchant_Id_Def_Report.rpt";
                Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
                _view.Show();
                _view = null;
            }
            if (opt16.Checked == true) //Unused Document Details
            {
                //12-09-2014 shanuka
                update_PC_List();

                BaseCls.GlbReportHeading = "Unused Document Details";
                BaseCls.GlbReportFromDate = txtFromDate.Value;
                BaseCls.GlbReportToDate = txtToDate.Value;
                BaseCls.GlbReportType = txtDocNo.Text.Trim();
                BaseCls.GlbReportName = "Unused_doc_details_Report.rpt";

                Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
                _view.Show();
                _view = null;
            }

            if (opt17.Checked == true) //Transaction Paytype Definition
            {
                //13-10-2014 Sanjeewa
                //update_PC_List();

                BaseCls.GlbReportHeading = "Transaction Paytype Definition";
                BaseCls.GlbReportFromDate = txtFromDate.Value;
                BaseCls.GlbReportToDate = txtToDate.Value;
                BaseCls.GlbReportAsAtDate = txtAsAtDate.Value;
                BaseCls.GlbReportDoc = txtDocNo.Text.Trim(); //Circular
                BaseCls.GlbReportDoc1 = txtpaytp.Text.Trim(); //Paytp
                BaseCls.GlbReportDoc2 = chkasatdate.Checked == true ? "Y" : "N";

                objRecon.TrPayTypeDefinitionReport();
            }

            //if (opt18.Checked == true) //Registered, Unregistered Vehicles
            //{
            //    //27-10-2014 Sanjeewa
            //    //update_PC_List();

            //    BaseCls.GlbReportHeading = "Registered, Unregistered Vehicles";
            //    BaseCls.GlbReportFromDate = txtFromDate.Value;
            //    BaseCls.GlbReportToDate = txtToDate.Value;
            //    BaseCls.GlbReportDoc2 = opt_regall.Checked == true ? "X" : opt_regyes.Checked == true ? "Y" : "N";
            //    BaseCls.GlbReportName = "Reg_Unreg_Vehicle.rpt";

            //    Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
            //    _view.Show();
            //    _view = null;
            //}

            if (opt18.Checked == true) //Registered, Unregistered Vehicles 27-10-2014 Sanjeewa
            {
                //update temporary table
                update_PC_List();

                string _filePath = string.Empty;
                string _error = string.Empty;

                BaseCls.GlbReportHeading = "Registered, Unregistered Vehicles";
                BaseCls.GlbReportDoc2 = opt_regall.Checked == true ? "X" : opt_regyes.Checked == true ? "Y" : "N";
                BaseCls.GlbReportDocType = txtDocType.Text.Trim();

                _filePath = CHNLSVC.MsgPortal.GetVehicleRegUnreg1_Report(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDoc2, BaseCls.GlbUserComCode, BaseCls.GlbUserID, out _error);

                if (!string.IsNullOrEmpty(_error))
                {
                    MessageBox.Show(_error);
                    Cursor.Current = Cursors.Default;
                    btnDisplay.Enabled = true;
                    return;
                }

                if (string.IsNullOrEmpty(_filePath))
                {
                    MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cursor.Current = Cursors.Default;
                    btnDisplay.Enabled = true;
                    return;
                }

                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(_filePath);
                p.Start();

                MessageBox.Show("Export Completed", "HP Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (opt19.Checked == true) //Current Status of the Given Approvals
            {   //24-11-2014 Sanjeewa
                update_PC_List();

                BaseCls.GlbReportHeading = "CURRENT STATUS OF THE GIVEN APPROVALS";
                BaseCls.GlbReportStrStatus = opt_pend.Checked == true ? "P" : opt_app.Checked == true ? "A" : opt_rej.Checked == true ? "R" : opt_cancel.Checked == true ? "C" : opt_fin.Checked == true ? "F" : "";
                BaseCls.GlbReportDoc1 = txtReqtp.Text.Trim(); //Req Type
                BaseCls.GlbReportName = "app_curr_status_report.rpt";

                Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
                _view.Show();
                _view = null;
            }

            if (opt20.Checked == true) //Current Status of the Given Approvals  user wise
            {   //9/4/2015 kapila

                if (string.IsNullOrEmpty(txt_user.Text))
                {
                    MessageBox.Show("Please select the user", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    btnDisplay.Enabled = true;
                    txt_user.Focus();
                    return;
                }

                update_PC_List_RPTDB();

                BaseCls.GlbReportHeading = "CURRENT STATUS OF THE GIVEN APPROVALS(USER WISE)";
                BaseCls.GlbReportStrStatus = opt_pend.Checked == true ? "P" : opt_app.Checked == true ? "A" : opt_rej.Checked == true ? "R" : opt_cancel.Checked == true ? "C" : opt_fin.Checked == true ? "F" : "";
                BaseCls.GlbReportDoc1 = txtReqtp.Text.Trim(); //Req Type
                BaseCls.GlbReportUser = txt_user.Text;
                BaseCls.GlbReportName = "app_curr_status_user.rpt";

                Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
                _view.Show();
                _view = null;
            }
            if (opt21.Checked == true)   //manual docs
            {
                //update temporary table
                update_PC_List_RPTDB();


                BaseCls.GlbReportHeading = "Manual documents";
                BaseCls.GlbReportComp = txtComp.Text;
                Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
                BaseCls.GlbReportName = "ManualDocsRep.rpt";
                if (chkasatdate.Checked == true)
                    BaseCls.GlbReportIsAsAt = 1;
                _view.Show();
                _view = null;
            }
            if (opt22.Checked == true)
            {   //update temporary table
                string _PB = string.Empty;
                string _PBLevel = string.Empty;
                string _Pol = string.Empty;
                string _Party = string.Empty;
                string _InsCom = string.Empty;

                //Sanjeewa 2015-09-07
                if (!chk_PB.Checked)
                    if (string.IsNullOrEmpty(Convert.ToString(txtPB.Text)))
                    {
                        MessageBox.Show("Please select the price book", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                if (!chk_PBLevel.Checked)
                    if (string.IsNullOrEmpty(Convert.ToString(txtPBLevel.Text)))
                    {
                        MessageBox.Show("Please select the price level", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                if (!chkInsCom.Checked)
                    if (string.IsNullOrEmpty(Convert.ToString(txtInsCom.Text)))
                    {
                        MessageBox.Show("Please select the insurance company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                if (!chkParty.Checked)
                    if (string.IsNullOrEmpty(Convert.ToString(txtHierchCode.Text)))
                    {
                        MessageBox.Show("Please select the heirachy", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                if (!chkPol.Checked)
                    if (string.IsNullOrEmpty(Convert.ToString(txtInsPol.Text)))
                    {
                        MessageBox.Show("Please select the policy", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                if (!chk_PB.Checked) BaseCls.GlbReportPriceBook = Convert.ToString(txtPB.Text); ;
                if (!chk_PBLevel.Checked) BaseCls.GlbReportPBLevel = Convert.ToString(txtPBLevel.Text);
                if (!chkInsCom.Checked) BaseCls.GlbReportInsComp = Convert.ToString(txtInsCom.Text);
                if (!chkPol.Checked) BaseCls.GlbReportPolCode = Convert.ToString(txtInsPol.Text);
                if (!chkParty.Checked) BaseCls.GlbReportPartyCode = Convert.ToString(txtHierchCode.Text);


                update_PC_List();

                BaseCls.GlbReportHeading = "INSURANCE DEFINITION";
                BaseCls.GlbReportItemCode = txtItemCode.Text;
                BaseCls.GlbReportAsAtDate = txtAsAtDate.Value.Date;
                BaseCls.GlbReportDocType = "CIRC";
                BaseCls.GlbReportDoc = txt_Circular.Text;

                BaseCls.GlbReportName = "InsuDef.rpt";
                Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
                _view.Show();
                _view = null;
            }

            if (opt23.Checked == true)
            {   //kapila
                update_PC_List();

                BaseCls.GlbReportHeading = "APPROVAL DETAILS BY REASON";
                BaseCls.GlbReportStrStatus = opt_pend.Checked == true ? "P" : opt_app.Checked == true ? "A" : opt_rej.Checked == true ? "R" : opt_cancel.Checked == true ? "C" : opt_fin.Checked == true ? "F" : opt_incomp.Checked == true ? "I" : "";
                BaseCls.GlbReportDoc = txtReason.Text;
                BaseCls.GlbReportName = "app_status_by_reason.rpt";
                BaseCls.GlbReportDocType = txtIcat1.Text;
                BaseCls.GlbReportDocSubType = opt_incomp.Checked == true ? "1" : "0";

                string _filePath = string.Empty;
                string _error = string.Empty;

                _filePath = CHNLSVC.MsgPortal.ReqAppDetByReasonReport_new(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, BaseCls.GlbReportDoc, BaseCls.GlbUserID, BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType, out _error);
                if (!string.IsNullOrEmpty(_error))
                {
                    btnDisplay.Enabled = true;
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(_error);
                    return;
                }

                if (string.IsNullOrEmpty(_filePath))
                {
                    btnDisplay.Enabled = true;
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(_filePath);
                p.Start();

                MessageBox.Show("Export Completed", "Service Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
                //_view.Show();
                //_view = null;
            }
            if (opt24.Checked == true)
            {   //kapila

                BaseCls.GlbReportHeading = "DEPARTMENT WISE PENDING APPROVALS";

                Boolean isItem = false;
                string _filePath = string.Empty;
                string _error = string.Empty;
                _filePath = CHNLSVC.Financial.Process_No_of_pending_app(BaseCls.GlbUserComCode, BaseCls.GlbUserID, out _error);

                if (!string.IsNullOrEmpty(_error))
                {
                    MessageBox.Show(_error);
                    btnDisplay.Enabled = true;
                    return;
                }

                if (string.IsNullOrEmpty(_filePath))
                {
                    MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnDisplay.Enabled = true;
                    return;
                }

                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(_filePath);
                p.Start();
            }

            if (opt25.Checked == true)
            {   //update temporary table
                //update_PC_List();
                update_PC_List_RPTDB();
                //hasith 26/12/2015
                BaseCls.GlbReportToDate = txtToDate.Value.Date;
                BaseCls.GlbReportFromDate = txtFromDate.Value.Date;
                BaseCls.GlbReportCompCode = txtComp.Text;
                BaseCls.GlbReportProfit = txtPC.Text;



                BaseCls.GlbReportName = "GVDetailsReport.rpt";
                Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
                _view.Show();
                _view = null;
            }
            if (opt26.Checked)   //registration process
            {
                //update temporary table
                update_Channel_List();

                string _filePath = string.Empty;
                string _error = string.Empty;

                BaseCls.GlbReportHeading = "Registration Process";
                if (optReg1.Checked) BaseCls.GlbReportParaLine1 = 1;
                if (optReg2.Checked) BaseCls.GlbReportParaLine1 = 2;
                if (optReg3.Checked) BaseCls.GlbReportParaLine1 = 3;
                if (optReg4.Checked) BaseCls.GlbReportParaLine1 = 4;
                if (optReg5.Checked) BaseCls.GlbReportParaLine1 = 5;
                if (optReg6.Checked) BaseCls.GlbReportParaLine1 = 6;
                if (optReg7.Checked) BaseCls.GlbReportParaLine1 = 7;
                if (optReg8.Checked) BaseCls.GlbReportParaLine1 = 8;
                if (optReg9.Checked) BaseCls.GlbReportParaLine1 = 9;
                if (optReg10.Checked) BaseCls.GlbReportParaLine1 = 10;

                if (chk_Export.Checked)
                {
                    _filePath = CHNLSVC.MsgPortal.RegisProcessReport(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbReportChannel, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportParaLine1, BaseCls.GlbReportDoc, out _error);

                    if (!string.IsNullOrEmpty(_error))
                    {
                        MessageBox.Show(_error);
                        Cursor.Current = Cursors.Default;
                        btnDisplay.Enabled = true;
                        return;
                    }

                    if (string.IsNullOrEmpty(_filePath))
                    {
                        MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Cursor.Current = Cursors.Default;
                        btnDisplay.Enabled = true;
                        return;
                    }

                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();

                    MessageBox.Show("Export Completed", "HP Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    BaseCls.GlbReportDoc = txt_fincomp.Text;
                    BaseCls.GlbReportParaLine1 = 10;
                    BaseCls.GlbReportName = "RegisProcess.rpt";
                    Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
                    _view.Show();
                    _view = null;
                }
            }
            if (opt27.Checked == true) //Issued Cover Note Details 
            {
                //update temporary table
                update_PC_List();

                string _filePath = string.Empty;
                string _error = string.Empty;

                BaseCls.GlbReportHeading = "Issued Cover Note Details";

                _filePath = CHNLSVC.MsgPortal.getCoverNoteDetailsReport(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, BaseCls.GlbUserID, out _error);

                if (!string.IsNullOrEmpty(_error))
                {
                    MessageBox.Show(_error);
                    Cursor.Current = Cursors.Default;
                    btnDisplay.Enabled = true;
                    return;
                }

                if (string.IsNullOrEmpty(_filePath))
                {
                    MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cursor.Current = Cursors.Default;
                    btnDisplay.Enabled = true;
                    return;
                }

                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(_filePath);
                p.Start();

                MessageBox.Show("Export Completed", "HP Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (opt28.Checked == true) //ecd voucher Details 
            {
                //update temporary table
                update_PC_List();

                string _filePath = string.Empty;
                string _error = string.Empty;

                BaseCls.GlbReportHeading = "ECD Voucher Details";

                _filePath = CHNLSVC.MsgPortal.ProcessECDVoucherDetReport(BaseCls.GlbUserID, BaseCls.GlbUserComCode, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, out _error);

                if (!string.IsNullOrEmpty(_error))
                {
                    MessageBox.Show(_error);
                    Cursor.Current = Cursors.Default;
                    btnDisplay.Enabled = true;
                    return;
                }

                if (string.IsNullOrEmpty(_filePath))
                {
                    MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cursor.Current = Cursors.Default;
                    btnDisplay.Enabled = true;
                    return;
                }

                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(_filePath);
                p.Start();

                MessageBox.Show("Export Completed", "HP Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (opt30.Checked == true) //my abans details
            {
                //update temporary table
                update_PC_List();

                string _filePath = string.Empty;
                string _error = string.Empty;

                BaseCls.GlbReportHeading = "MyAbans Details";
                if (optReg1.Checked) BaseCls.GlbReportParaLine1 = 1;
                if (optReg2.Checked) BaseCls.GlbReportParaLine1 = 2;
                if (optReg3.Checked) BaseCls.GlbReportParaLine1 = 3;
                if (optReg4.Checked) BaseCls.GlbReportParaLine1 = 4;
                if (optReg5.Checked) BaseCls.GlbReportParaLine1 = 5;
                if (optReg6.Checked) BaseCls.GlbReportParaLine1 = 6;
                if (optReg7.Checked) BaseCls.GlbReportParaLine1 = 7;
                if (optReg8.Checked) BaseCls.GlbReportParaLine1 = 8;
                if (optReg9.Checked) BaseCls.GlbReportParaLine1 = 9;
                if (optReg10.Checked) BaseCls.GlbReportParaLine1 = 10;

                _filePath = CHNLSVC.MsgPortal.ProcessMyAbansReport(BaseCls.GlbUserID, BaseCls.GlbUserComCode, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportParaLine1, out _error);

                if (!string.IsNullOrEmpty(_error))
                {
                    MessageBox.Show(_error);
                    Cursor.Current = Cursors.Default;
                    btnDisplay.Enabled = true;
                    return;
                }

                if (string.IsNullOrEmpty(_filePath))
                {
                    MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cursor.Current = Cursors.Default;
                    btnDisplay.Enabled = true;
                    return;
                }

                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(_filePath);
                p.Start();

                MessageBox.Show("Export Completed", "HP Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (opt31.Checked == true) //ecd voucher Details 
            {
                //update temporary table
                update_PC_List();

                string _filePath = string.Empty;
                string _error = string.Empty;

                BaseCls.GlbReportHeading = "ECD Voucher Details";

                _filePath = CHNLSVC.MsgPortal.ProcessDayEndProcessReport(BaseCls.GlbUserID, BaseCls.GlbUserComCode, Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, 1, out _error);

                if (!string.IsNullOrEmpty(_error))
                {
                    MessageBox.Show(_error);
                    Cursor.Current = Cursors.Default;
                    btnDisplay.Enabled = true;
                    return;
                }

                if (string.IsNullOrEmpty(_filePath))
                {
                    MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cursor.Current = Cursors.Default;
                    btnDisplay.Enabled = true;
                    return;
                }

                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(_filePath);
                p.Start();

                MessageBox.Show("Export Completed", "HP Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (opt32.Checked == true)  //SalesReconcilation kapila 27/3/2017
            {   //update temporary table
                //update_PC_List();
                update_PC_List_RPTDB();
                //hasith 26/12/2015
                BaseCls.GlbReportToDate = txtToDate.Value.Date;
                BaseCls.GlbReportFromDate = txtFromDate.Value.Date;
                BaseCls.GlbReportCompCode = txtComp.Text;
                BaseCls.GlbReportProfit = txtCompDesc.Text;
                BaseCls.GlbReportHeading = "Sales Reconcilation Report";

                BaseCls.GlbReportName = "SalesReconcilation.rpt";
                Reports.Reconciliation.ReportViewerRecon _view = new Reports.Reconciliation.ReportViewerRecon();
                _view.Show();
                _view = null;
            }
            if (opt33.Checked == true)  //scheme definitions kapila 4/5/2017
            {

                BaseCls.GlbReportHeading = "SCHEME DEFINITION DETAILS";

                Boolean isItem = false;
                string _filePath = string.Empty;
                string _error = string.Empty;
                _filePath = CHNLSVC.MsgPortal.GetSchemeDefinitionDetails(BaseCls.GlbUserComCode, BaseCls.GlbUserID,txt_Circular.Text, out _error);

                if (!string.IsNullOrEmpty(_error))
                {
                    MessageBox.Show(_error);
                    btnDisplay.Enabled = true;
                    return;
                }

                if (string.IsNullOrEmpty(_filePath))
                {
                    MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnDisplay.Enabled = true;
                    return;
                }

                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(_filePath);
                p.Start();
            }

            if (opt34.Checked == true)  //Price Discrepancy Report by Dilshan on 02/11/2017
            {
                //update temporary table
                update_PC_List();

                string _filePath = string.Empty;
                string _error = string.Empty;

                BaseCls.GlbReportHeading = "Price Discrepancy Report";
                BaseCls.GlbReportDoc2 = opt_regall.Checked == true ? "X" : opt_regyes.Checked == true ? "Y" : "N";
                BaseCls.GlbReportDocType = txtDocType.Text.Trim();

                _filePath = CHNLSVC.MsgPortal.GetPriceDiscrepancyReport(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDoc2, BaseCls.GlbUserComCode, BaseCls.GlbUserID, out _error);

                if (!string.IsNullOrEmpty(_error))
                {
                    MessageBox.Show(_error);
                    Cursor.Current = Cursors.Default;
                    btnDisplay.Enabled = true;
                    return;
                }

                if (string.IsNullOrEmpty(_filePath))
                {
                    MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Cursor.Current = Cursors.Default;
                    btnDisplay.Enabled = true;
                    return;
                }

                Process p = new Process();
                p.StartInfo = new ProcessStartInfo(_filePath);
                p.Start();

                MessageBox.Show("Export Completed", "Price Discrepancy Report Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            Cursor.Current = Cursors.Default;
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
            //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPR"))
            if (opt3.Checked == true)
            {
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10045))
                {
                    DataTable dt = CHNLSVC.Sales.GetLoc_from_Hierachy_Rep_all(com, chanel, subChanel, area, region, zone, pc);

                    foreach (DataRow drow in dt.Rows)
                    {
                        lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                    }
                }
                else
                {
                    DataTable dt = CHNLSVC.Sales.GetLoc_from_Hierachy_Rep(BaseCls.GlbUserID, com, chanel, subChanel, area, region, zone, pc);
                    foreach (DataRow drow in dt.Rows)
                    {
                        lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                    }
                }
            }
            else
            {
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10047))
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

        #region option change events

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


        private void txtChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
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
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.SchByCir:
                    {
                        paramsText.Append(txt_Circular.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append("LEASE" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocSubType:
                    {
                        paramsText.Append("SRN" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SystemUser:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
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
                case CommonUIDefiniton.SearchUserControlType.InsuCom:
                    {
                        paramsText.Append("INS" + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.InsuPolicy:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PromotionalCircular:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ApprovePermCode:
                    {
                        paramsText.Append(seperator);
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
            }
        }

        private void txtArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
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
            }
        }

        private void txtRegion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
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
            }
        }

        private void txtZone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
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

        private void txtIcat3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Cat3_Click(null, null);
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

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Itm_Click(null, null);
            }
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


        private void txtToDate_ValueChanged(object sender, EventArgs e)
        {
            BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Value);
        }

        private void txtFromDate_ValueChanged(object sender, EventArgs e)
        {
            BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Value);
        }

        private void txtAsAtDate_ValueChanged(object sender, EventArgs e)
        {
            BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Value);
        }

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

        private void opt2_CheckedChanged(object sender, EventArgs e)
        {
            if (opt2.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8002))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8002)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(2);
            }
        }

        private void opt3_CheckedChanged(object sender, EventArgs e)
        {
            if (opt3.Checked == true)
            {
                if (CheckPermission) // add by akila 2017/05/12
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8003))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8003)");
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                }
                
                setFormControls(3);
            }
        }

        private void opt4_CheckedChanged(object sender, EventArgs e)
        {
            if (opt4.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8004))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8004)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(4);
            }
        }

        private void txtFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            //e.SuppressKeyPress = true;
        }

        private void txtToDate_KeyDown(object sender, KeyEventArgs e)
        {
            // e.SuppressKeyPress = true;
        }

        private void txtAsAtDate_KeyDown(object sender, KeyEventArgs e)
        {
            //  e.SuppressKeyPress = true;
        }

        private void opt5_CheckedChanged(object sender, EventArgs e)
        {
            if (opt5.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8005))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8005)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(5);
            }
        }

        private void btn_Srch_Doc_Click(object sender, EventArgs e)
        {

        }

        private void opt6_CheckedChanged(object sender, EventArgs e)
        {
            if (opt6.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8006))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8006)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(6);
            }
        }

        private void chkCircular_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCircular.Checked == true)
            {
                txtCircular.Text = "";
                txtCircular.Enabled = false;
                btnCircular.Enabled = false;
            }
            else
            {
                txtCircular.Enabled = true;
                btnCircular.Enabled = true;
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

        private void opt7_CheckedChanged(object sender, EventArgs e)
        {
            if (opt7.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8007))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8007)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(7);
            }
        }

        private void opt8_CheckedChanged(object sender, EventArgs e)
        {
            if (opt8.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8008))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8008)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(8);
            }
        }

        private void fldgOpenPath_FileOk(object sender, CancelEventArgs e)
        {
            BaseCls.GlbReportFilePath = fldgOpenPath.FileName;
        }

        private void opt9_CheckedChanged(object sender, EventArgs e)
        {
            if (opt9.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8009))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8009)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(9);
            }
        }

        private void opt10_CheckedChanged(object sender, EventArgs e)
        {
            if (opt10.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8010))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8010)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(10);
            }
        }

        private void opt11_CheckedChanged(object sender, EventArgs e)
        {
            if (opt11.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8011))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8011)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(11);
            }
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

        private void txtPC_Leave(object sender, EventArgs e)
        {
            load_PCDesc();
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

        private void txtPC_TextChanged(object sender, EventArgs e)
        {

        }

        private void opt12_CheckedChanged(object sender, EventArgs e)
        {
            if (opt12.Checked == true)
            {
                //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8011))
                //{
                //    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8011)");
                //    RadioButton RDO = (RadioButton)sender;
                //    RDO.Checked = false;
                //    return;
                //}
                setFormControls(12);
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

        private void btn_srch_ins_com_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsuCom);
            DataTable _result = CHNLSVC.CommonSearch.GetInsuCompany(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtInsCom;
            _CommonSearch.ShowDialog();
            txtInsCom.Select();
        }

        private void btnHierachySearch_Click(object sender, EventArgs e)
        {
            try
            {
                Base _basePage = new Base();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                DataTable dt = new DataTable();
                DataTable _result = new DataTable();
                if (DropDownListPartyTypes.SelectedValue.ToString() == "COM")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "CHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "SCHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }

                else if (DropDownListPartyTypes.SelectedValue.ToString() == "AREA")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "REGION")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "ZONE")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "PC")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "GPC")
                {
                    // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GPC);
                    //  _result = _basePage.CHNLSVC.CommonSearch.Get_GPC(_CommonSearch.SearchParams, null, null);
                    _result = CHNLSVC.General.Get_GET_GPC("", "");
                }

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtHierchCode;
                _CommonSearch.ShowDialog();
                txtHierchCode.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
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

        private void chkInsCom_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInsCom.Checked == true)
            {
                txtInsCom.Text = "";
                txtInsCom.Enabled = false;
                btn_srch_ins_com.Enabled = false;
            }
            else
            {
                txtInsCom.Enabled = true;
                btn_srch_ins_com.Enabled = true;
            }
        }

        private void chkPol_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPol.Checked == true)
            {
                txtInsPol.Text = "";
                txtInsPol.Enabled = false;
                btn_srch_pol.Enabled = false;
            }
            else
            {
                txtInsPol.Enabled = true;
                btn_srch_pol.Enabled = true;
            }
        }

        private void btn_srch_pol_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsuPolicy);
            DataTable _result = CHNLSVC.CommonSearch.GetInsuPolicy(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtInsPol;
            _CommonSearch.ShowDialog();
            txtInsPol.Select();
        }

        private void btnCircular_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(opt22.Checked))
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromotionalCircular);
                    DataTable _result = CHNLSVC.CommonSearch.SearchPromotinalCircular(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtCircular;
                    _CommonSearch.txtSearchbyword.Text = txtCircular.Text;
                    _CommonSearch.ShowDialog();
                    txtCircular.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt13_CheckedChanged(object sender, EventArgs e)
        {
            if (opt13.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8013))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8013)");
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
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8014))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8014)");
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
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8015))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8015)");
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
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8016))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8015)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(16);
            }
        }

        private void txtDocNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable _result = CHNLSVC.CommonSearch.Load_ItemSearch_details(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtDocNo;
            _CommonSearch.ShowDialog();
            txtDocNo.Focus();
        }

        private void txtDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtDocNo_MouseDoubleClick(null, null);
            }
        }

        private void opt17_CheckedChanged(object sender, EventArgs e)
        {
            if (opt17.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8017))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8017)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(17);
            }
        }

        private void chkpaytp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkpaytp.Checked == true)
            {
                txtpaytp.Text = "";
                txtpaytp.Enabled = false;
                btnpaytp.Enabled = false;
            }
            else
            {
                txtpaytp.Enabled = true;
                btnpaytp.Enabled = true;
            }
        }

        private void chkasatdate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkasatdate.Checked == true)
            {
                pnlDateRange.Enabled = false;
                txtAsAtDate.Enabled = true;
            }
            else
            {
                pnlDateRange.Enabled = true;
                txtAsAtDate.Enabled = false;
            }

        }

        private void opt18_CheckedChanged(object sender, EventArgs e)
        {
            if (opt18.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8018))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8018)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(18);
            }
        }

        private void optPC_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void opt19_CheckedChanged(object sender, EventArgs e)
        {
            if (opt19.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8019))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8019)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(19);
            }
        }

        private void chkReqtp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReqtp.Checked == true)
            {
                txtReqtp.Text = "";
                txtReqtp.Enabled = false;
                btn_reqtp.Enabled = false;
            }
            else
            {
                txtReqtp.Enabled = true;
                btn_reqtp.Enabled = true;
            }
        }

        private void btn_reqtp_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ApprovePermCode);
            DataTable _result = CHNLSVC.CommonSearch.Search_Approve_Permissions(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtReqtp;
            _CommonSearch.ShowDialog();
            txtReqtp.Select();

        }

        private void opt20_CheckedChanged(object sender, EventArgs e)
        {
            if (opt20.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8020))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8020)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(20);
            }
        }

        private void chk_user_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_user.Checked == true)
            {
                txt_user.Text = "";
                txt_user.Enabled = false;
                btn_user.Enabled = false;
            }
            else
            {
                txt_user.Enabled = true;
                btn_user.Enabled = true;
            }
        }

        private void btn_user_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SystemUser);
                DataTable _result = CHNLSVC.CommonSearch.Get_All_SystemUsers(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txt_user;
                _CommonSearch.ShowDialog();

                txt_user.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt21_CheckedChanged(object sender, EventArgs e)
        {
            if (opt21.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8021))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8021)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(21);
            }
        }

        private void opt22_CheckedChanged(object sender, EventArgs e)
        {
            if (opt22.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8022))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8022)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(22);
            }
        }

        private void chk_Circular_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Circular.Checked == true)
            {
                txt_Circular.Text = "";
                txt_Circular.Enabled = false;
                btn_Circular.Enabled = false;
            }
            else
            {
                txt_Circular.Enabled = true;
                btn_Circular.Enabled = true;
            }
        }

        private void opt23_CheckedChanged(object sender, EventArgs e)
        {
            if (opt23.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8023))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8023)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(23);
            }
        }

        private void btn_srch_reason_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocSubType);
                DataTable _result = CHNLSVC.CommonSearch.GetMovementDocSubTypes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtReason;
                _CommonSearch.ShowDialog();
                txtReason.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void chkReason_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReason.Checked == true)
            {
                txtReason.Text = "";
                txtReason.Enabled = false;
                btn_srch_reason.Enabled = false;
            }
            else
            {
                txtReason.Enabled = true;
                btn_srch_reason.Enabled = true;
            }
        }

        private void opt24_CheckedChanged(object sender, EventArgs e)
        {
            setFormControls(24);
        }

        private void opt25_CheckedChanged(object sender, EventArgs e)
        {

            if (opt25.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16044))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :16044)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(25);
            }
        }

        private void opt26_CheckedChanged(object sender, EventArgs e)
        {
            setFormControls(26);
        }

        private void opt27_CheckedChanged(object sender, EventArgs e)
        {
            if (opt27.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8027))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8027)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(27);
            }
        }

        private void opt28_CheckedChanged(object sender, EventArgs e)
        {
            setFormControls(28);
        }

        private void opt30_CheckedChanged(object sender, EventArgs e)
        {
            setFormControls(30);
        }



        private void opt_fin_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkFinComp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFinComp.Checked == true)
            {
                txt_fincomp.Text = "";
                txt_fincomp.Enabled = false;
                btn_srch_fin_comp.Enabled = false;
            }
            else
            {
                txt_fincomp.Enabled = true;
                btn_srch_fin_comp.Enabled = true;
            }
        }

        private void btn_srch_fin_comp_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 2;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
            DataTable _result = CHNLSVC.CommonSearch.GetBusinessCompany(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txt_fincomp;
            _CommonSearch.ShowDialog();
            txt_fincomp.Select();
        }

        private void chk_Export_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Export.Checked)
                pnlReg.Enabled = true;
            else
                pnlReg.Enabled = false;
        }

        private void opt31_CheckedChanged(object sender, EventArgs e)
        {
            setFormControls(31);
        }

        private void opt32_CheckedChanged(object sender, EventArgs e)
        {
            if (opt32.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8032))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8032)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(32);
            }
        }

        private void opt33_CheckedChanged(object sender, EventArgs e)
        {
            if (opt33.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8033))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8033)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(33);
            }
        }

        private void btn_Circular_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SchByCir);
            DataTable _result = CHNLSVC.CommonSearch.GetSchemeComByCircular(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.obj_TragetTextBox = txt_Circular;
            _CommonSearch.ShowDialog();
            txt_Circular.Select();
        }

        private void opt34_CheckedChanged(object sender, EventArgs e)
        {
            if (opt34.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 8034))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :8034)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(34);
            }
        }
    }
}


