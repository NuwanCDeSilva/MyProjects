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
using FF.WindowsERPClient.Reports.Sales;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms.Internal.Win32;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;


//Written By kapila on 26/12/2012
namespace FF.WindowsERPClient.Reports.Service
{
    public partial class Service__Rep : Base
    {
        clsServiceRep objService = new clsServiceRep();

        public Service__Rep()
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
            pnljobno.Enabled = false;
            pnlLoyTp.Enabled = false;
            pnl_approved.Enabled = false;
            pnl_approved.Visible = false;
            pnl_TechCRE.Enabled = false;
            pnl_TechCRE.Visible = false;
            pnl_origin.Visible = false;
            pnl_origin.Enabled = false;
            BaseCls.GlbReportViewPC = false;
            pnlPart.Visible = false;
            pnl_defect.Visible = false;
            pnl_defect.Enabled = false;
            pnl_serial.Visible = false;
            pnl_serial.Enabled = false;

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
            pnl_promotor.Visible = false;
            txtPO.Visible = true;
            btn_Srch_PO.Visible = true;
            label10.Text = "PO #";
            label41.Text = "Discount Rate";
            chkAsAtDate.Text = "Run at at Date";
            txtDocType.Text = "";
            pnljobno.Visible = false;
            pnljobno.Enabled = false;
            pnlwarrstatus.Visible = false;
            pnlwarrstatus.Enabled = false;
            pnljobstatus.Visible = false;
            pnljobstatus.Enabled = false;
            pnlitemtype.Visible = false;
            pnlitemtype.Enabled = false;
            pnljobcat.Visible = false;
            pnljobcat.Enabled = false;
            pnltechnician.Visible = false;
            pnltechnician.Enabled = false;
            pnl_jobprocess.Visible = false;
            lbl_JobProcesses.Visible = false;
            pnl_deftp.Visible = false;
            pnl_deftp.Enabled = false;
            pnlEsti.Visible = false;
            pnl_req_status.Visible = false;
            label5.Text = "Profitcenter";

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
                        pnl_DocType.Enabled = true;
                        break;
                    }
                case 2:
                    {
                        pnljobno.Visible = true;
                        pnljobno.Enabled = true;
                        pnl_Item.Enabled = true;
                        pnlwarrstatus.Visible = true;
                        pnlwarrstatus.Enabled = true;
                        pnljobstatus.Visible = true;
                        pnljobstatus.Enabled = true;
                        pnlitemtype.Visible = true;
                        pnlitemtype.Enabled = true;
                        pnljobcat.Visible = true;
                        pnljobcat.Enabled = true;
                        pnltechnician.Visible = true;
                        pnltechnician.Enabled = true;
                        break;
                    }
                case 6:
                    {
                        pnljobno.Visible = true;
                        pnljobno.Enabled = true;
                        pnl_Item.Enabled = true;
                        pnlwarrstatus.Visible = true;
                        pnlwarrstatus.Enabled = true;
                        //pnljobstatus.Visible = true;
                        //pnljobstatus.Enabled = true;
                        pnlitemtype.Visible = true;
                        pnlitemtype.Enabled = true;
                        pnljobcat.Visible = true;
                        pnljobcat.Enabled = true;
                        pnltechnician.Visible = true;
                        pnltechnician.Enabled = true;
                        break;
                    }
                case 7:
                    {
                        pnl_jobprocess.Visible = true;
                        lbl_JobProcesses.Visible = true;
                        pnl_Item.Enabled = true;
                        pnl_Export.Visible = true;
                        pnl_Export.Enabled = true;
                        chk_Export.Visible = true;
                        pnltechnician.Visible = true;
                        pnltechnician.Enabled = true;
                        pnljobcat.Visible = true;
                        pnljobcat.Enabled = true;
                        pnlitemtype.Visible = true;
                        pnlitemtype.Enabled = true;
                        pnl_origin.Visible = true;
                        pnl_origin.Enabled = true;
                        load_Processes();
                        break;
                    }
                case 8:
                    {
                        pnl_Item.Enabled = true;
                        pnlwarrstatus.Visible = true;
                        pnlwarrstatus.Enabled = true;
                        pnljobstatus.Visible = true;
                        pnljobstatus.Enabled = true;
                        pnlitemtype.Visible = true;
                        pnlitemtype.Enabled = true;
                        pnljobcat.Visible = true;
                        pnljobcat.Enabled = true;
                        pnltechnician.Visible = true;
                        pnltechnician.Enabled = true;
                        break;
                    }
                case 9:
                    {
                        pnl_Item.Enabled = true;
                        pnlwarrstatus.Visible = true;
                        pnlwarrstatus.Enabled = true;
                        pnl_deftp.Visible = true;
                        pnl_deftp.Enabled = true;                        
                        break;
                    }
                case 11:
                    {
                        pnlEsti.Visible = true;
                        pnlEsti.Enabled = true;
                        break;
                    }
                case 12:
                    {
                        pnl_Item.Enabled = true;
                        break;
                    }
                case 14:
                    {
                        pnlPart.Visible = true;
                        pnlwarrstatus.Visible = false;
                        pnlDiscRate.Visible = false;
                        pnl_Export.Visible = true;
                        pnl_Export.Enabled = true;
                        chk_Export.Visible = true;
                        break;
                    }
                case 15:
                    {
                        pnljobno.Enabled = true;
                        pnljobno.Visible = true;
                        break;
                    }
                case 16:
                    {
                        pnljobno.Enabled = true;
                        pnljobno.Visible = true;
                        break;
                    }
                case 17:
                    {
                        //pnl_Item.Enabled = true;
                        pnl_Export.Visible = true;
                        pnl_Export.Enabled = true;
                        chk_Export.Visible = true;
                        pnl_req_status.Visible = true;
                        break;
                    }
                case 18:
                    {
                        pnl_Item.Enabled = true;                        
                        break;
                    }
                case 19:
                    {
                        pnl_Item.Enabled = true;
                        pnl_DocType.Enabled = true;
                        break;
                    }
                case 20:
                    {                        
                        pnl_Item.Enabled = true;                        
                        pnltechnician.Visible = true;
                        pnltechnician.Enabled = true;                        
                        break;
                    }
                case 21:
                    {
                        pnl_Item.Enabled = true;                        
                        break;
                    }

                case 22:
                    {
                        pnljobcat.Visible = true;
                        pnljobcat.Enabled = true;
                        break;
                    }
                case 23:
                    {
                        pnl_Item.Enabled = true;
                        label5.Text = "Location";
                        pnl_defect.Visible = true;
                        pnl_defect.Enabled = true;
                        pnl_serial.Visible = true;
                        pnl_serial.Enabled = true;
                        break;
                    }
                case 26:
                    {
                        txtComp.Enabled = true;
                        txtPC.Enabled = true;
                       

                        
                        pnlComent.Enabled = false;
                       
                        break;
                    }
                case 27:
                    {
                        txtComp.Enabled = true;
                        txtPC.Enabled = true;



                        pnlComent.Enabled = false;
                        pnl_Export.Visible = true;
                        pnl_Export.Enabled = true;
                        chkCost.Visible = true;
                        chkCost.Enabled = true;
                        break;
                    }
                case 24:
                    {
                        break;
                    }
        }
        }

        private void load_Processes()
        {
            DataTable _processes = new DataTable();
            _processes = CHNLSVC.CustService.JobProcesses();

            dgProcess.AutoGenerateColumns = false;
            dgProcess.DataSource = _processes;

        }

        private void addPartStatus()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            PartyTypes.Add("PR", "Part Received");
            PartyTypes.Add("PRO", "Part Request Only");
            PartyTypes.Add("PO", "Part Order Pending");
            PartyTypes.Add("PH", "Part Hold/Reject");
            PartyTypes.Add("PF", "Part Fails");
            PartyTypes.Add("ALL", "ALL");
            cmbPart.DataSource = new BindingSource(PartyTypes, null);
            cmbPart.DisplayMember = "Value";
            cmbPart.ValueMember = "Key";
            cmbPart.SelectedIndex = 5;
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

            addPartStatus();


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

                //kapila 4/7/2014
                if (CheckServerDateTime() == false) return;

                //check this user has permission for this PC
                if (txtPC.Text != string.Empty)
                {
                    Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(BaseCls.GlbUserComCode, txtPC.Text);
                    //if (_IsValid == false)
                    //{
                    //    MessageBox.Show("Invalid Profit Center.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    return;
                    //}
                    string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                    //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, null, "REPS"))
                    //Add by Chamal 30-Aug-2013
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10044))
                    {
                        Int16 is_Access = 0;
                        if (BaseCls.GlbReportViewPC == false)
                        {
                            is_Access = CHNLSVC.Security.Check_User_Loc(BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtPC.Text);
                            if (is_Access != 1)
                            {
                                //MessageBox.Show("Access Denied.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MessageBox.Show("Sorry, You have no permission for view reports!\n( Advice: Required permission code :10044)", "Service Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

                BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Value).Date;
                BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Value).Date;
                BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Value).Date;

                btnDisplay.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;

                if (opt1.Checked == true)   //cash sales summary
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportDocType = txtDocType.Text;
                    BaseCls.GlbReportHeading = "Cash Sales Summary";
                    BaseCls.GlbReportType = "CASHSALE";

                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "SalesSummary1.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt2.Checked == true)   //Job summary
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportJobNo = txt_JobNo.Text;
                    BaseCls.GlbReportTechncian = txt_technician.Text;
                    BaseCls.GlbReportJobCat = string.Empty;
                    BaseCls.GlbReportItemType = string.Empty;
                    BaseCls.GlbReportJobStatus = string.Empty;
                    BaseCls.GlbReportWarrStatus = string.Empty;

                    BaseCls.GlbReportHeading = "Job Summary";

                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "Job_Summary.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt6.Checked == true)   //tech comments - kapila
                {
                    //update temporary table
                    update_PC_List_RPTDB();
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;

                    BaseCls.GlbReportJobNo = txt_JobNo.Text;
                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportDoc = txt_coment.Text;

                    BaseCls.GlbReportTechncian = "";
                    BaseCls.GlbReportJobCat = txt_jobcat.Text;
                    BaseCls.GlbReportItemType = string.Empty;
                    BaseCls.GlbReportJobStatus = string.Empty;
                    BaseCls.GlbReportWarrStatus = string.Empty;

                    BaseCls.GlbReportHeading = "Technician Comments";

                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "Tech_Comments.rpt";
                    _view.GlbReportName = "Tech_Comments.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt7.Checked == true)   //Process Tracking Report
                {
                    DateTime startTime = Convert.ToDateTime(BaseCls.GlbReportFromDate);
                    DateTime endtime = Convert.ToDateTime(BaseCls.GlbReportToDate);
                    TimeSpan duration = startTime - endtime;

                    if (duration.Days > 90)
                    {
                        MessageBox.Show("You are allowed to search for 90 Days period.", "Service Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        return;
                    }

                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportJobNo = txt_JobNo.Text;
                    BaseCls.GlbReportTechncian = txt_technician.Text;
                    BaseCls.GlbReportJobCat = txt_jobcat.Text ;
                    BaseCls.GlbReportItemType = txt_itemtype.Text ;
                    BaseCls.GlbReportDoc2 = txtOrigin.Text;
                    if (cmb_warrstatus.Text == "")
                    { BaseCls.GlbReportWarrStatus = "0"; }
                    else
                    { BaseCls.GlbReportWarrStatus = cmb_warrstatus.Text.Substring(1, 1).ToString(); }
                    BaseCls.GlbReportJobStatus = opt_achieved.Checked == true ? "Y" :opt_CREwise.Checked==true?"C": "N";
                    BaseCls.GlbReportDiscRate = 1;
                    BaseCls.GlbReportIsExport = chk_Export.Checked == true ? 1 : 0;

                    foreach (DataGridViewRow row in dgProcess.Rows)
                    {
                        DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;
                        if (Convert.ToBoolean(chk.Value) == true)
                        {
                            BaseCls.GlbReportDiscRate = Convert.ToDecimal(row.Cells[2].Value.ToString());
                        }
                    }

                    BaseCls.GlbReportHeading = "Process Tracking Report";

                    if (chk_Export.Checked == false)
                    {
                        Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                        BaseCls.GlbReportName = "Process_Tracking_Report.rpt";
                        _view.Show();
                        _view = null;
                    }
                    else
                    {
                        string _filePath = string.Empty;
                        string _error = string.Empty;

                        _filePath = CHNLSVC.CustService.JobProcessTrackingDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportTechncian, BaseCls.GlbReportJobCat, BaseCls.GlbReportItemType, BaseCls.GlbReportDiscRate, BaseCls.GlbReportWarrStatus, BaseCls.GlbReportJobNo, BaseCls.GlbUserID, BaseCls.GlbReportJobStatus,BaseCls.GlbReportIsExport,BaseCls.GlbReportDoc2, out _error);
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
                    }

                }

                if (opt8.Checked == true)   //repeated jobs - kapila
                {
                    //update temporary table
                    update_PC_List_RPTDB();
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;

                    BaseCls.GlbReportJobNo = txt_JobNo.Text;
                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportJobNo = txt_JobNo.Text;
                    BaseCls.GlbReportTechncian = "";
                    BaseCls.GlbReportJobCat = txt_jobcat.Text;
                    if (!string.IsNullOrEmpty(txt_jobstatus.Text))
                        BaseCls.GlbReportStatus = Convert.ToInt16(txt_jobstatus.Text);
                    BaseCls.GlbReportJobStatus = string.Empty;
                    BaseCls.GlbReportWarrStatus = string.Empty;
                    BaseCls.GlbReportTp = "RPTJ";

                    BaseCls.GlbReportHeading = "Repeatable Jobs";

                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "Repeated_Jobs.rpt";
                    _view.GlbReportName = "Repeated_Jobs.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt9.Checked == true)   //Defect Analysis Report - Sanjeewa 2015-04-27
                {
                    //update temporary table
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportDefectType = txt_deftp.Text;
                    BaseCls.GlbReportWarrStatus = string.Empty;

                    BaseCls.GlbReportHeading = "Defect Analysis Report";

                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "Job_Defect_Analysis.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt10.Checked == true)   //Service GP Nadeeka 27-04-2015
                {
                    //update temporary table
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportDefectType = txt_deftp.Text;
                    BaseCls.GlbReportWarrStatus = string.Empty;

                    BaseCls.GlbReportHeading = "Service GP Report";

                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "ServiceGP.rpt";
                    _view.GlbReportName = "ServiceGP.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt11.Checked == true)   //estimates - kapila
                {
                    Boolean _recFound = false;
                    //update temporary table
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportDocType = "";
                    if (optEst.Checked == true)
                        BaseCls.GlbReportDocType = "JEST";
                    if (optQuo.Checked == true)
                        BaseCls.GlbReportDocType = "QATN";


                    BaseCls.GlbReportHeading = "Estimate Details";

                    DataTable tmp_user_pc = CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbReportCompCode, BaseCls.GlbUserID);

                    if (tmp_user_pc.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in tmp_user_pc.Rows)
                        {
                            DataTable tmp_job = CHNLSVC.CustService.sp_get_Estimatejobs(BaseCls.GlbReportCompCode, dr1["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDocType, BaseCls.GlbReportCusId, BaseCls.GlbReportTechncian);
                            if (tmp_job.Rows.Count > 0)
                            {
                                _recFound = true;
                                break;
                            }
                        }
                    }

                    if (_recFound == false)
                    {
                        MessageBox.Show("No estimate records found !", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnDisplay.Enabled = true;
                        return;
                    }

                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "Estimate_Det.rpt";
                    _view.GlbReportName = "Estimate_Det.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt12.Checked == true)   //StandBy Nadeeka 27-04-2015
                {
                    //update temporary table
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportDefectType = txt_deftp.Text;
                    BaseCls.GlbReportWarrStatus = string.Empty;
                    BaseCls.GlbReportHeading = "Service Standby Issue Report";

                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "ServiceStandyIssue.rpt";
                    _view.GlbReportName = "ServiceStandyIssue.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt13.Checked == true)   //Defect Analysis Report - Sanjeewa 2015-04-27
                {
                    //update temporary table
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportDefectType = txt_deftp.Text;
                    BaseCls.GlbReportWarrStatus = string.Empty;

                    BaseCls.GlbReportHeading = "Defect Analysis Report";

                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "Job_Defect_Analysis.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt14.Checked == true)   //Supplier Warranty - Nadeeka 06-05-2015
                {
                    clsServiceRep objSer = new clsServiceRep();
                    //update temporary table
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportDefectType = txt_deftp.Text;
                    BaseCls.GlbReportWarrStatus = string.Empty;
                    

                    BaseCls.GlbReportHeading = "Supplier Warranty Details Report";
                   

                    if (chk_Export.Checked)
                    {
                        BaseCls.GlbReportDocType = Convert.ToString(cmbPart.SelectedValue);

                        string _filePath = string.Empty;
                        string _error = string.Empty;

                        _filePath = CHNLSVC.CustService.GetServiceSupplierWarranty_Excel(BaseCls.GlbUserComCode, txtPC.Text, txtSup.Text, txtIcat1.Text, txtIcat2.Text, txtIcat3.Text, txtModel.Text, txtBrand.Text, txtItemCode.Text, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDocType, BaseCls.GlbUserID, out _error);
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

                        //BaseCls.GlbReportParaLine1 = 1;
                        //Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                        //BaseCls.GlbReportName = "SupplierWarranty_Excel.rpt";
                        //_view.GlbReportName = "SupplierWarranty_Excel.rpt";
                        //_view.Show();
                        //_view = null;
                        //BaseCls.GlbReportName = "SupplierWarranty_Excel.rpt";
                        //BaseCls.GlbReportParaLine1 = 1;
                        //objSer.ServiceSupplierClaim();

                        //MasterCompany _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
                        //string _path = _MasterComp.Mc_anal6;
                        //objSer._suppWarEx.ExportToDisk(ExportFormatType.Excel, _path + "SupplierWarranty_Excel" + BaseCls.GlbUserID + ".xls");

                        //Excel.Application excelApp = new Excel.Application();
                        //excelApp.Visible = true;
                        //string workbookPath = _path + "SupplierWarranty_Excel" + BaseCls.GlbUserID + ".xls";
                        //Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath,
                        //        0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                        //        true, false, 0, true, false, false);
                    }
                    else
                    {
                        BaseCls.GlbReportParaLine1 = 0;
                        BaseCls.GlbReportDocType = "N";
                        Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                        BaseCls.GlbReportName = "SupplierWarranty.rpt";
                        _view.GlbReportName = "SupplierWarranty.rpt";
                        _view.Show();
                        _view = null;
                    }
                }

                if (opt15.Checked == true)   //Warranty Replacement Letter - Sanjeewa 16-05-2015
                {
                    BaseCls.GlbReportJobNo = txt_JobNo.Text;
                    BaseCls.GlbReportTp = "WRPL";

                    BaseCls.GlbReportHeading = "Warranty Replacement Letter";

                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "Job_BER_Letter.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt16.Checked == true)   //BER Letter - Sanjeewa 16-05-2015
                {
                    BaseCls.GlbReportJobNo = txt_JobNo.Text;
                    BaseCls.GlbReportTp = "BER";

                    BaseCls.GlbReportHeading = "BER Letter";

                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "Job_BER_Letter.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt17.Checked == true)   //Exchange Details - Sanjeewa 27-06-2015
                {
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = "Customer Warranty Replacements";
                    BaseCls.GlbReportIsExport = chk_Export.Checked == true ? 1 : 0;
                    if (cmb_ReqStatus.Text != "")
                    {
                        BaseCls.GlbReportWarrStatus = cmb_ReqStatus.Text.Substring(0, 1).ToString();
                    }
                    else
                    {
                    BaseCls.GlbReportWarrStatus="0";
                    }

                    if (chk_Export.Checked == false)
                    {
                        Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                        BaseCls.GlbReportName = "df_exchange_report.rpt";
                        _view.Show();
                        _view = null;
                    }
                    else
                    {
                        string _filePath = string.Empty;
                        string _error = string.Empty;

                        _filePath = CHNLSVC.CustService.DFExchangeDetails1(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date,BaseCls.GlbUserComCode, BaseCls.GlbReportIsExport,BaseCls.GlbReportWarrStatus,BaseCls.GlbUserID, out _error);
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
                    }
                }

                if (opt18.Checked == true)   //Smart Insurance Claim Details 2015-08-25
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;

                    BaseCls.GlbReportHeading = "Smart Insurance Claim Details";

                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "smart_warr_Iss_Dtl_Report.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt19.Checked == true)   //Service GP Detail Sanjeewa 31-08-2015
                {
                    //update temporary table
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportDocType = txtDocType.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportDefectType = txt_deftp.Text;
                    BaseCls.GlbReportWarrStatus = string.Empty;

                    BaseCls.GlbReportHeading = "Service GP Report";

                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "ServiceGP_Detail.rpt";
                    _view.GlbReportName = "ServiceGP_Detail.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt20.Checked == true)   //Job Detail
                {                    
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportJobNo = txt_JobNo.Text;
                    BaseCls.GlbReportTechncian = txt_technician.Text;
                    BaseCls.GlbReportJobCat = string.Empty;
                    BaseCls.GlbReportItemType = string.Empty;
                    if (cmb_warrstatus.Text == "")
                    { BaseCls.GlbReportWarrStatus = "0"; }
                    else
                    { BaseCls.GlbReportWarrStatus = cmb_warrstatus.Text.Substring(1, 1).ToString(); }                   
                   
                    BaseCls.GlbReportHeading = "Job Detail Report";
                                        
                    string _filePath = string.Empty;
                    string _error = string.Empty;

                    _filePath = CHNLSVC.CustService.JobDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportTechncian, BaseCls.GlbReportJobCat, BaseCls.GlbReportItemType, BaseCls.GlbReportDiscRate, BaseCls.GlbReportWarrStatus, BaseCls.GlbReportJobNo, BaseCls.GlbUserID, out _error);
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

                }


                if (opt21.Checked == true)   //Service Agreement Nadeeka 07-11-2015
                {
                    //update temporary table
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportDefectType = txt_deftp.Text;
                    BaseCls.GlbReportWarrStatus = string.Empty;

                    BaseCls.GlbReportHeading = "Service Agreement Report";

                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "AgreementDet.rpt";
                    _view.GlbReportName = "AgreementDet.rpt";
                    _view.Show();
                    _view = null;
                }


                if (opt22.Checked == true)   //Incentive Report Sanjeewa 09-11-2015
                {
                    //update temporary table
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = "Incentive Report";
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportDoc = txt_jobcat.Text;

                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "Incentive_Detail.rpt";
                    _view.GlbReportName = "Incentive_Detail.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt23.Checked == true)   //Job Details with duration to complete
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportDefectType = txt_defect.Text;
                    BaseCls.GlbReportDoc1 = txt_serial.Text;
                    
                    BaseCls.GlbReportHeading = "Job Details with duration to complete";

                    string _filePath = string.Empty;
                    string _error = string.Empty;

                    _filePath = CHNLSVC.CustService.JobTimeDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbUserID, BaseCls.GlbReportDefectType, BaseCls.GlbReportDoc1, out _error);
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

                }

                if (opt24.Checked == true)   //Service Agreement Detail
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "Service Agreement Detail";

                    string _filePath = string.Empty;
                    string _error = string.Empty;

                    _filePath = CHNLSVC.CustService.AgreementDetailsReport(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, BaseCls.GlbUserID, out _error);
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

                }

                if (opt25.Checked == true)   //insurance for free service locations
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "Insurance for free service locations";

                    string _filePath = string.Empty;
                    string _error = string.Empty;

                    _filePath = CHNLSVC.CustService.InsuranceForServiceReport(BaseCls.GlbUserComCode, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date,BaseCls.GlbUserID, out _error);
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

                }
                if (opt26.Checked == true)   //Tharanga 2017/07/11
                {
                    ////update temporary table
                    //update_PC_List_RPTDB();
                    BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Value);
                    BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Value);
                    string pc = txtPC.Text.ToString().Trim();
                    string loc = BaseCls.GlbUserDefLoca;
                    BaseCls.GlbUserDefProf = txtPC.Text.ToString().Trim();


                    //BaseCls.GlbReportItemCode = txtItemCode.Text;
                    //BaseCls.GlbReportBrand = txtBrand.Text;
                    //BaseCls.GlbReportModel = txtModel.Text;
                    //BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    //BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    //BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    //BaseCls.GlbReportDefectType = txt_deftp.Text;
                    //BaseCls.GlbReportWarrStatus = string.Empty;
                    BaseCls.GlbReportHeading = "Spare Parts Movement Report";
                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "Spare_Parts_Movement_Report.rpt";
                    _view.GlbReportName = "Spare_Parts_Movement_Report.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt27.Checked == true)   //Tharanga 2017/07/11
                {
                    ////update temporary table
                    //update_PC_List_RPTDB();
                    BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Value);
                    BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Value);
                    string pc = txtPC.Text.ToString().Trim();
                    string loc = BaseCls.GlbUserDefLoca;
                    BaseCls.GlbUserDefProf = txtPC.Text.ToString().Trim();
                    if (chkCost.Checked == true)
                    {
                        BaseCls.GlbReqUserPermissionLevel = 1;
                    }
                    else
                    {
                        BaseCls.GlbReqUserPermissionLevel = 0;
                    }

                
                    BaseCls.GlbReportHeading = "Spare Parts Movement Report";
                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "Spare_parts_movement.rpt";
                    _view.GlbReportName = "Spare_parts_movement.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt28.Checked == true)  //Tharanga 2017/07/25 
                {
                    BaseCls.GlbReportJobNo = txt_JobNo.Text;                                  
                    BaseCls.GlbReportHeading = "READY LETTER";
                    #region Validate job_item 
                    DataTable ods = CHNLSVC.CustService.sp_get_job_details(txt_JobNo.Text, "JOB");
                    if (ods.Rows.Count > 1)
                    {
                        if (string.IsNullOrEmpty(BaseCls.GlbReportItmClasif))
                        {
                            MessageBox.Show("Job Item Not select. Please select Job Item First", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnDisplay.Enabled = true;
                            return;
                        }

                    }
                    #endregion

                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "RedyForCollection_ABE.rpt";
                    _view.Show();
                    _view = null;

                }
                if (opt30.Checked == true)  //Tharanga 2017/07/25 
                {
                    BaseCls.GlbReportJobNo = txt_JobNo.Text;
                    // BaseCls.GlbReportTp = "BER";
                    #region Validate job_item
                    DataTable ods = CHNLSVC.CustService.sp_get_job_details(txt_JobNo.Text, "JOB");
                    if (ods.Rows.Count > 1)
                    {
                        if (string.IsNullOrEmpty(BaseCls.GlbReportItmClasif))
                        {
                            MessageBox.Show("Job Item Not select. Please select Job Item First", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnDisplay.Enabled = true;
                            return;
                        }

                    }
                    #endregion
                    BaseCls.GlbReportHeading = "READY LETTER";
                   
                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "RedyForDelivery_ABE.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt31.Checked == true)  //Tharanga 2017/07/25 
                {
                    BaseCls.GlbReportJobNo = txt_JobNo.Text;
                    // BaseCls.GlbReportTp = "BER";

                    BaseCls.GlbReportHeading = "NOTICE OF DISPOSAL";
                    #region Validate job_item
                    DataTable ods = CHNLSVC.CustService.sp_get_job_details(txt_JobNo.Text, "JOB");
                    if (ods.Rows.Count > 1)
                    {
                        if (string.IsNullOrEmpty(BaseCls.GlbReportItmClasif))
                        {
                            MessageBox.Show("Job Item Not select. Please select Job Item First", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnDisplay.Enabled = true;
                            return;
                        }

                    }
                    #endregion
                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "ReadyForDisposal_ABE.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt32.Checked == true)  //Tharanga 2017/07/25 
                {
                    BaseCls.GlbReportJobNo = txt_JobNo.Text;
                    // BaseCls.GlbReportTp = "BER";

                    BaseCls.GlbReportHeading = "Shipment Letter";
                    #region Validate job_item
                    DataTable ods = CHNLSVC.CustService.sp_get_job_details(txt_JobNo.Text, "JOB");
                    if (ods.Rows.Count > 1)
                    {
                        if (string.IsNullOrEmpty(BaseCls.GlbReportItmClasif))
                        {
                            MessageBox.Show("Job Item Not select. Please select Job Item First", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnDisplay.Enabled = true;
                            return;
                        }

                    }
                    #endregion
                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "ShipmentLetter_ABE.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt33.Checked == true)  //Tharanga 2017/07/27
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportJobNo = txt_JobNo.Text;
                    BaseCls.GlbReportTechncian = txt_technician.Text;
                    BaseCls.GlbReportJobCat = string.Empty;
                    BaseCls.GlbReportItemType = string.Empty;
                    if (cmb_warrstatus.Text == "")
                    { BaseCls.GlbReportWarrStatus = "0"; }
                    else
                    { BaseCls.GlbReportWarrStatus = cmb_warrstatus.Text.Substring(1, 1).ToString(); }

                    BaseCls.GlbReportHeading = "Job Detail Report";
                  
                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "BrandModelwiseItemDetails_ABE.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt34.Checked == true)  //Tharanga 2017/07/27 
                {
                    BaseCls.GlbReportJobNo = txt_JobNo.Text;
                    // BaseCls.GlbReportTp = "BER";

                    BaseCls.GlbReportHeading = "Can not Repair Letter";
                    #region Validate job_item
                    DataTable ods = CHNLSVC.CustService.sp_get_job_details(txt_JobNo.Text, "JOB");
                    if (ods.Rows.Count > 1)
                    {
                        if (string.IsNullOrEmpty(BaseCls.GlbReportItmClasif))
                        {
                            MessageBox.Show("Job Item Not select. Please select Job Item First", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnDisplay.Enabled = true;
                            return;
                        }

                    }
                    #endregion
                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "Cannotbbrepaired_ABE.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt35.Checked == true)  //Tharanga 2017/07/27 
                {
                    BaseCls.GlbReportJobNo = txt_JobNo.Text;


                    BaseCls.GlbReportHeading = "Final Notice Of Disposal";
                    #region Validate job_item
                    DataTable ods = CHNLSVC.CustService.sp_get_job_details(txt_JobNo.Text, "JOB");
                    if (ods.Rows.Count > 1)
                    {
                        if (string.IsNullOrEmpty(BaseCls.GlbReportItmClasif))
                        {
                            MessageBox.Show("Job Item Not select. Please select Job Item First", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnDisplay.Enabled = true;
                            return;
                        }

                    }
                    #endregion
                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "NoticeOfDisposal_ABE.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt36.Checked == true)  //Tharanga 2017/07/27
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportJobNo = txt_JobNo.Text;
                    BaseCls.GlbReportTechncian = txt_technician.Text;
                    BaseCls.GlbReportJobCat = string.Empty;
                    BaseCls.GlbReportItemType = string.Empty;
                    if (cmb_warrstatus.Text == "")
                    { BaseCls.GlbReportWarrStatus = "0"; }
                    else
                    { BaseCls.GlbReportWarrStatus = cmb_warrstatus.Text.Substring(1, 1).ToString(); }

                    BaseCls.GlbReportHeading = "Job Item Summary Report";

                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportName = "BrandModelwiseItemSummary_ABE_N.rpt";
                    _view.Show();
                    _view = null;
                }



                pnlitem.Visible = false;//Add by tharanga   
                grdItem.Visible = false;//add by tharanga 

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
            btnDisplay.Enabled = true;
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
            btnDisplay.Enabled = true;
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
        private void opt1_CheckedChanged(object sender, EventArgs e)
        {
            if (opt1.Checked == true)
            {
                // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RSL1"))
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 6001))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :6001)");
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
                case CommonUIDefiniton.SearchUserControlType.TechComments:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + null + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EMP_ALL:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceTaskCate:
                    {
                        paramsText.Append("ACTIVE" + seperator + BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.DefectTypes:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.Promotor:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + string.Empty + seperator);
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
            BaseCls.GlbReportGroupPromotor = 0;
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
                if (Item.Text == "PRM")
                {
                    BaseCls.GlbReportGroupPromotor = i;
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


        private void chk_promotor_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_promotor.Checked == true)
            {
                txt_promotor.Text = "";
                txt_promotor.Enabled = false;
                btn_promotor.Enabled = false;
            }
            else
            {
                txt_promotor.Enabled = true;
                btn_promotor.Enabled = true;
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

        private void txt_promotor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_promotor_Click(null, null);
            }
        }

        private void btn_promotor_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotor);
            DataTable _result = CHNLSVC.CommonSearch.SearchSalesPromotor(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txt_promotor;
            _CommonSearch.ShowDialog();
            txt_promotor.Select();
        }

        private void lbl15_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstGroup.Items.Count; i++)
            {
                if (lstGroup.Items[i].Text == "PRM")
                    return;
            }
            lstGroup.Items.Add(("PRM").ToString());
        }

        private void chk_AppBy_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_AppBy.Checked == true)
            {
                txt_AppBy.Text = "";
                txt_AppBy.Enabled = false;
                btn_AppBy.Enabled = false;
            }
            else
            {
                txt_AppBy.Enabled = true;
                btn_AppBy.Enabled = true;
            }
        }

        private void chk_jobno_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_jobno.Checked == true)
            {
                txt_JobNo.Text = "";
                txt_JobNo.Enabled = false;
                btn_jobno.Enabled = false;
            }
            else
            {
                txt_JobNo.Enabled = true;
                btn_jobno.Enabled = true;
            }
        }

        private void btn_jobno_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
            DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, Convert.ToDateTime("31-12-1111"), Convert.ToDateTime("31-12-2999"));
            _CommonSearch.dtpFrom.Value = DateTime.Today.AddMonths(-1);
            _CommonSearch.dtpTo.Value = DateTime.Today;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txt_JobNo;
            this.Cursor = Cursors.Default;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txt_JobNo.Focus();
        }

        private void opt2_CheckedChanged(object sender, EventArgs e)
        {
            if (opt2.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11002))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11002)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(2);
            }
        }

        private void opt6_CheckedChanged(object sender, EventArgs e)
        {
            if (opt6.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11006))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11006)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(6);
            }
        }

        private void opt7_CheckedChanged(object sender, EventArgs e)
        {
            if (opt7.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11007))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11007)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(7);
            }
        }

        private void dgProcess_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            foreach (DataGridViewRow row in dgProcess.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    chk.Value = false;
                }
            }

            DataGridViewCheckBoxCell ch1 = new DataGridViewCheckBoxCell();
            ch1 = (DataGridViewCheckBoxCell)dgProcess.Rows[dgProcess.CurrentRow.Index].Cells[0];

            if (ch1.Value == null)
                ch1.Value = false;
            switch (ch1.Value.ToString())
            {
                case "False":
                    {
                        ch1.Value = true;
                        break;
                    }
            }
        }

        private void opt8_CheckedChanged(object sender, EventArgs e)
        {
            if (opt8.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11008))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11008)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(8);
            }
        }

        private void opt9_CheckedChanged(object sender, EventArgs e)
        {
            if (opt9.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11009))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11009)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(9);
            }
        }

        private void chk_deftp_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_deftp.Checked == true)
            {
                txt_deftp.Text = "";
                txt_deftp.Enabled = false;
                btn_deftp.Enabled = false;
            }
            else
            {
                txt_deftp.Enabled = true;
                btn_deftp.Enabled = true;
            }
        }

        private void opt10_CheckedChanged(object sender, EventArgs e)
        {
            if (opt10.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11010))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11010)");
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
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11011))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11011)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(11);
            }
        }

        private void opt12_CheckedChanged(object sender, EventArgs e)
        {
            if (opt12.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11012))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11012)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(12);
            }
        }

        private void opt13_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void opt14_CheckedChanged(object sender, EventArgs e)
        {
            if (opt14.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11014))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11014)");
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
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11015))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11015)");
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
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11016))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11016)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(16);
            }
        }

        private void chk_jobcat_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_jobcat.Checked == true)
            {
                txt_jobcat.Text = "";
                txt_jobcat.Enabled = false;
                btn_jobcat.Enabled = false;
            }
            else
            {
                txt_jobcat.Enabled = true;
                btn_jobcat.Enabled = true;
            }
        }

        private void btn_jobcat_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceTaskCate);
            DataTable _result = CHNLSVC.CommonSearch.SearchScvTaskCateByLoc(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txt_jobcat;
            _CommonSearch.ShowDialog();
            txt_jobcat.Focus();
        }

        private void chk_jobcat_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void chk_jobstatus_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_jobstatus.Checked == true)
            {
                txt_jobstatus.Text = "";
                txt_jobstatus.Enabled = false;
                btn_jobstatus.Enabled = false;
            }
            else
            {
                txt_jobstatus.Enabled = true;
                btn_jobstatus.Enabled = true;
            }
        }

        private void btn_jobstatus_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobStage);
            DataTable _result = CHNLSVC.CommonSearch.SearchJobStage(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txt_jobstatus;
            _CommonSearch.ShowDialog();
            txt_jobstatus.Focus();
        }

        private void chk_coment_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_coment.Checked == true)
            {
                txt_coment.Text = "";
                txt_coment.Enabled = false;
                btn_coment.Enabled = false;
            }
            else
            {
                txt_coment.Enabled = true;
                btn_coment.Enabled = true;
            }
        }

        private void btn_coment_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                DataTable _result = new DataTable();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TechComments);
                _result = CHNLSVC.CommonSearch.SERCH_TECHCOMMTBYCHNNL(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txt_coment;
                _CommonSearch.ShowDialog();
                txt_coment.Select();
                Cursor = Cursors.Default;
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

        private void chk_technician_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_technician.Checked == true)
            {
                txt_technician.Text = "";
                txt_technician.Enabled = false;
                btn_technician.Enabled = false;
            }
            else
            {
                txt_technician.Enabled = true;
                btn_technician.Enabled = true;
            }
        }

        private void btn_technician_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = new DataTable();
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EMP_ALL);
            _result = CHNLSVC.CommonSearch.GetAllEmp(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txt_technician;
            _CommonSearch.ShowDialog();
            txt_technician.Select();
            Cursor = Cursors.Default;            
            
        }

        private void lblshowlevel_Click(object sender, EventArgs e)
        {
            if (pnl_jobprocess.Visible == true)
            {
                pnl_jobprocess.Visible = false;
            }
            else
            {
                pnl_jobprocess.Visible = true;
            }
        }

        private void opt17_CheckedChanged(object sender, EventArgs e)
        {
            if (opt17.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11017))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11017)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(17);
            }
        }

        private void opt18_CheckedChanged(object sender, EventArgs e)
        {
            if (opt18.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11018))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11018)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(18);
            }
        }

        private void opt19_CheckedChanged(object sender, EventArgs e)
        {
            if (opt19.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11019))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11019)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(19);
            }
        }

        private void opt20_CheckedChanged(object sender, EventArgs e)
        {
            if (opt20.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11020))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11020)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(20);
            }
        }

        private void chk_itemtype_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_itemtype.Checked == true)
            {
                txt_itemtype.Text = "";
                txt_itemtype.Enabled = false;
                btn_itemtype.Enabled = false;
            }
            else
            {
                txt_itemtype.Enabled = true;
                btn_itemtype.Enabled = true;
            }
        }

        private void txt_itemtype_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_itemtype_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txt_itemtype;
            _CommonSearch.txtSearchbyword.Text = txt_itemtype.Text;
            _CommonSearch.ShowDialog();
            txt_itemtype.Focus();
        }

        private void opt21_CheckedChanged(object sender, EventArgs e)
        {
            if (opt21.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11021))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11021)");
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
                //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11022))
                //{
                //    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11022)");
                //    RadioButton RDO = (RadioButton)sender;
                //    RDO.Checked = false;
                //    return;
                //}
                setFormControls(22);
            }
        }

        private void opt23_CheckedChanged(object sender, EventArgs e)
        {
            if (opt23.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11023))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11023)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(23);
            }
        }

        private void opt24_CheckedChanged(object sender, EventArgs e)
        {
            if (opt24.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11024))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11024)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(24);
            }
        }

        private void btn_defect_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                DataTable _result = new DataTable();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DefectTypes);
                _result = CHNLSVC.CommonSearch.GetDefectTypes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txt_defect;
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.ShowDialog();
                txt_defect.Focus();
                txt_defect.SelectAll();
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                SystemErrorMessage(ex);
            }
        }

        private void SystemErrorMessage(Exception ex)
        { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        private void chk_defect_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_defect.Checked == true)
            {
                txt_defect.Text = "";
                txt_defect.Enabled = false;
                btn_defect.Enabled = false;
            }
            else
            {
                txt_defect.Enabled = true;
                btn_defect.Enabled = true;
            }
        }

        private void opt25_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void opt26_CheckedChanged(object sender, EventArgs e)
        {
            if (opt26.Checked == true)
            {
                //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11024))
                //{
                //    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11024)");
                //    RadioButton RDO = (RadioButton)sender;
                //    RDO.Checked = false;
                //    return;
                //}
                setFormControls(26);
            }
        }

        private void opt27_CheckedChanged(object sender, EventArgs e)
        {
            if (opt27.Checked == true)
            {
                //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11024))
                //{
                //    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11024)");
                //    RadioButton RDO = (RadioButton)sender;
                //    RDO.Checked = false;
                //    return;
                //}
                setFormControls(27);
            }
        }

        private void opt28_CheckedChanged(object sender, EventArgs e)
        {
            if (opt28.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11016))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11016)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(16);
            }
            else
            {
                pnlitem.Visible = false;
                grdItem.Visible = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (opt30.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11016))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11016)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(16);
            }
            else
            {
                pnlitem.Visible = false;
                grdItem.Visible = false;
            }
        }

        private void opt31_CheckedChanged(object sender, EventArgs e)
        {
            if (opt31.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11016))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11016)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(16);
            }
            else
            {
                pnlitem.Visible = false;
                grdItem.Visible = false;
            }
        }

        private void opt32_CheckedChanged(object sender, EventArgs e)
        {
            if (opt32.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11016))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11016)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(16);
            }
            else
            {
                pnlitem.Visible = false;
                grdItem.Visible = false;
            }
        }

        private void txt_JobNo_Leave(object sender, EventArgs e)//Tharanga 2017/07/26
        {
            BaseCls.GlbReportItmClasif = "";
            BaseCls.GlbReportItemCode = "";
            BaseCls.GlbReportParaLine1 = 0;
            if (opt28.Checked == true || opt30.Checked == true || opt31.Checked == true || opt31.Checked == true)
            {
                DataTable ods = CHNLSVC.CustService.sp_get_job_details(txt_JobNo.Text, "JOB");
                grdItem.Rows.Clear();
                if (ods.Rows.Count > 1)
                {
                    for (int count = 0; count < ods.Rows.Count; count++)
                    {

                        grdItem.Rows.Add();
                        grdItem.Rows[count].Cells["ItemCode"].Value = ods.Rows[count]["JBD_ITM_CD"].ToString();
                        grdItem.Rows[count].Cells["JobNo"].Value = ods.Rows[count]["JBD_JOBNO"].ToString();
                        grdItem.Rows[count].Cells["lineNo"].Value = ods.Rows[count]["JBD_JOBLINE"].ToString();
                        grdItem.Rows[count].Cells["itemSer"].Value = ods.Rows[count]["JBD_SER1"].ToString();
                    }
                    pnlitem.Visible = true;
                    grdItem.Visible = true;

                }
                else
                {
                    pnlitem.Visible = false;
                    grdItem.Visible = false;
                }
            }
            else
            {
                pnlitem.Visible = false;
                grdItem.Visible = false;
            }
               
            
        }

        private void grdItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            BaseCls.GlbReportItemCode = grdItem.Rows[e.RowIndex].Cells["ItemCode"].Value == null ? string.Empty : grdItem.Rows[e.RowIndex].Cells["ItemCode"].Value.ToString();
            BaseCls.GlbReportJobNo = grdItem.Rows[e.RowIndex].Cells["JobNo"].Value == null ? string.Empty : grdItem.Rows[e.RowIndex].Cells["JobNo"].Value.ToString();
            BaseCls.GlbReportParaLine1 = grdItem.Rows[e.RowIndex].Cells["lineNo"].Value == null   ? 0 : Convert.ToInt32(grdItem.Rows[e.RowIndex].Cells["lineNo"].Value.ToString());
            BaseCls.GlbReportItmClasif = grdItem.Rows[e.RowIndex].Cells["itemSer"].Value == null ? string.Empty : grdItem.Rows[e.RowIndex].Cells["itemSer"].Value.ToString();
            //BaseCls.GlbSerial = grdItem.Rows[e.RowIndex].Cells["lineNo"].Value.ToString();
        }

        private void btnitemlist_Click(object sender, EventArgs e)
        {
            if (pnlItemList.Visible)
            {
                pnlitem.Visible = false;
                grdItem.Visible = false;
            }
           
        }

        private void opt33_CheckedChanged(object sender, EventArgs e)
        {
            if (opt33.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16096))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :16096)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(20);
            }
        }

        private void opt34_CheckedChanged(object sender, EventArgs e)
        {
            if (opt34.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11016))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11016)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(16);
            }
            else
            {
                pnlitem.Visible = false;
                grdItem.Visible = false;
            }
        }

        private void opt35_CheckedChanged(object sender, EventArgs e)
        {
            if (opt35.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11016))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :11016)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(16);
            }
            else
            {
                pnlitem.Visible = false;
                grdItem.Visible = false;
            }
        }

        private void opt36_CheckedChanged(object sender, EventArgs e)
        {
            if (opt36.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16097))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :16097)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(20);
            }
        }

        private void chkCost_CheckedChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10075))
            {
               
                MessageBox.Show("Sorry, You have no permission to view with cost!\n( Advice: Required permission code :10075 )");
                chkCost.Checked = false;
                return;
            }
        }

        

    }
}


