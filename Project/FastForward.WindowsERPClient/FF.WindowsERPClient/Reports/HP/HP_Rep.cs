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
using FF.WindowsERPClient.Reports.HP;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms.Internal.Win32;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading.Tasks;

//Written By kapila on 17/01/2012
namespace FF.WindowsERPClient.Reports.HP
{
    public partial class HP_Rep : Base
    {
        clsHpSalesRep objHp = new clsHpSalesRep();

        public HP_Rep()
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
            cmbCatType.Items.Clear();
            pnl_Item.Enabled = false;
            pnl_Direc.Enabled = false;
            pnl_DocNo.Enabled = false;
            pnl_DocSubType.Enabled = false;
            pnl_DocType.Enabled = false;
            pnl_Entry_Tp.Enabled = false;
            pnl_Rec_Tp.Enabled = false;
            pnl_Rec_Tp.Visible = false;
            pnlMgr.Visible = false;
            txtFromDate.Enabled = true;
            txtToDate.Enabled = true;
            pnl_number.Enabled = false;
            pnlRevert.Enabled = false;
            pnlInstall.Enabled = false;
            optSR.Text = "SR Release";
            optOther.Text = "Other SR Release";
            chkRedBal.Text = "Reducing Balance";
            optBoth.Visible = true;
            chkRntSch.Enabled = false;
            chkRntSch.Visible = false;
            pnl_claim.Visible = false;
            pnl_claim.Enabled = false;
            pnlaccount.Enabled = false;
            pnl_Sch.Enabled = false;
            pnlPromoter.Visible = false;
            pnl_user.Visible = false;
            txtAsAtDate.Enabled = true;
            cmb_Week.Enabled = false;
            pnlPayType.Visible = false;
            pnl_sum.Visible = false;
            pnl_sum.Enabled = false;
            cmb_itm_grp.Enabled = false;
            chkRntSch.Text = "Rental Schedule";
            pnlSupplym.Visible = false;
            pnlSupplym.Enabled = false;

            cmbPayModes.DataSource = CHNLSVC.Sales.GetAllPaymentType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, null);
            cmbPayModes.DisplayMember = "SAPT_DESC";
            cmbPayModes.ValueMember = "SAPT_CD";
            cmbPayModes.SelectedIndex = -1;

            switch (_index)
            {
                case 1:
                    {
                        pnlMgr.Visible = true;
                        break;
                    }
                case 3:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = false;
                        pnlHP.Enabled = true;
                        pnl_Sch.Enabled = true;
                        pnl_Grp_By.Enabled = true;
                        pnl_Item.Enabled = true;
                        cmbGroupBy.Items.Add("Default");
                        cmbGroupBy.Items.Add("Profit Center");
                        cmbGroupBy.Items.Add("Manager");
                        cmbGroupBy.Items.Add("Scheme Type");
                        cmbGroupBy.Items.Add("Scheme");
                        cmbGroupBy.SelectedIndex = 0;
                        break;
                    }
                case 5:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = false;
                        pnlHP.Enabled = true;
                        pnl_Sch.Enabled = true;
                        pnl_Grp_By.Enabled = true;
                        pnl_Item.Enabled = true;

                        cmbGroupBy.Items.Add("Default");
                        cmbGroupBy.Items.Add("Scheme Type");
                        cmbGroupBy.Items.Add("Scheme");
                        cmbGroupBy.Items.Add("Location");
                        cmbGroupBy.Items.Add("Channel Wise Summary");
                        cmbGroupBy.SelectedIndex = 0;
                        break;
                    }
                case 8:
                    {
                        pnlAsAtDate.Enabled = true;
                        break;
                    }
                case 10:
                    {
                        pnlHP.Enabled = true;
                        pnl_Grp_By.Enabled = true;
                        cmbGroupBy.Items.Add("Default");
                        cmbGroupBy.Items.Add("Summary");
                        cmbGroupBy.SelectedIndex = 0;
                        break;
                    }
                case 12:
                    {
                        pnlInstall.Enabled = true;
                        break;
                    }
                case 19:
                    {
                        pnlAsAtDate.Enabled = true;
                        break;
                    }
                case 22:
                    {
                        txtFromDate.Enabled = false;
                        txtToDate.Enabled = false;
                        pnl_Sch.Enabled = true;
                        break;
                    }
                case 23:
                    {
                        txtFromDate.Enabled = false;
                        txtToDate.Enabled = false;
                        pnl_Sch.Enabled = true;
                        break;
                    }
                case 24:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = false;
                        pnl_Sch.Enabled = false;
                        pnlCusId.Enabled = true;
                        pnlAccBal.Enabled = true;
                        pnlOther.Enabled = true;
                        pnlHP.Enabled = true;
                        txtScheme.Enabled = false;
                        txtId.Enabled = true;
                        txtAccBal.Enabled = true;
                        if (txtAccBal.Text == "")
                        {
                            txtAccBal.Text = Convert.ToString(0);

                        }
                        break;
                    }
                case 25:
                    {
                        pnlHP.Enabled = true;
                        pnl_Sch.Enabled = true;
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = false;
                        break;
                    }
                case 27:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = false;
                        break;
                    }
                case 28:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = false;
                        break;
                    }
                case 30:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = true;
                        pnlredBal.Enabled = true;
                        break;
                    }
                case 31:
                    {
                        pnl_number.Enabled = true;
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        txtFromDate.Enabled = false;
                        txtToDate.Enabled = false;
                        pnlHP.Enabled = true;
                        pnl_Grp_By.Enabled = true;
                        chkRedBal.Text = "Group by Interest and Capital";
                        pnlredBal.Enabled = true;
                        pnl_sum.Visible = true;
                        pnl_sum.Enabled = true;

                        cmbGroupBy.Items.Add("Company");
                        cmbGroupBy.Items.Add("Profit Center");
                        cmbGroupBy.Items.Add("Account");
                        cmbGroupBy.SelectedIndex = 0;
                        chkRntSch.Text = "Debtor impairment";
                        chkRntSch.Visible = true;
                        chkRntSch.Enabled = true;
                        pnlSupplym.Visible = true;
                        pnlSupplym.Enabled = true;
                        break;
                    }
                case 33:
                    {
                        txtAsAtDate.Enabled = true; // Added by Chathura on 13-oct-2017
                        pnlAsAtDate.Enabled = true; // Change by Chathura on 13-oct-2017
                        pnlDateRange.Enabled = false;
                        break;
                    }
                case 34:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = false;
                        break;
                    }
                case 35:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = false;
                        break;
                    }
                case 36:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        pnl_sum.Visible = true;
                        pnl_sum.Enabled = true;
                        break;
                    }

                case 37:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = false;
                        pnl_Item.Enabled = true;
                        cmbCatType.Items.Add("Default");
                        cmbCatType.Items.Add("Electronic");
                        cmbCatType.Items.Add("Non Electronic");
                        pnl_Grp_By.Enabled = true;
                        pnlHP.Enabled = true;
                        cmbGroupBy.Items.Add("Profit Center");
                        cmbGroupBy.Items.Add("Item Code");
                        cmbGroupBy.Items.Add("Item Categoty");
                        cmbGroupBy.Items.Add("Categoty Type");
                        cmbGroupBy.SelectedIndex = 0;



                        cmbCatType.SelectedIndex = 0;
                        break;
                    }

                case 38:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        break;
                    }
                case 39:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        pnl_Sch.Enabled = true;
                        pnlHP.Enabled = true;
                        break;
                    }
                case 40:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        pnlHP.Enabled = true;
                        pnl_Grp_By.Enabled = true;

                        cmbGroupBy.Items.Add("Default");
                        cmbGroupBy.Items.Add("Scheme Type");
                        cmbGroupBy.Items.Add("Scheme");
                        cmbGroupBy.Items.Add("Location");
                        cmbGroupBy.SelectedIndex = 0;
                        break;
                    }
                case 41:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        break;
                    }
                case 42:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        pnlRevert.Enabled = true;
                        break;
                    }
                case 43:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        break;
                    }
                case 44:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        break;
                    }
                case 45:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        pnlRevert.Enabled = true;
                        optBoth.Visible = false;
                        optSR.Text = "Summary";
                        optSR.Checked = true;
                        optOther.Text = "Details";
                        chkRntSch.Visible = true;
                        chkRntSch.Enabled = true;
                        pnl_sum.Visible = true;
                        pnl_sum.Enabled = true;

                        break;
                    }
                case 46:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        pnlRevert.Enabled = true;
                        break;
                    }
                case 47:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = false;
                        break;
                    }
                case 48:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        txtFromDate.Enabled = false;
                        txtToDate.Enabled = false;
                        break;
                    }
                case 49:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        pnl_claim.Visible = true;
                        pnl_claim.Enabled = true;
                        break;
                    }

                case 50:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        break;
                    }

                case 51:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        break;
                    }
                case 52:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        pnl_Sch.Enabled = true;
                        pnlHP.Enabled = true;
                        pnlPromoter.Visible = true;
                        LoadPromotor();
                        break;
                    }
                case 53:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = false;
                        pnlHP.Enabled = true;
                        pnl_Sch.Enabled = true;
                        //pnl_Grp_By.Enabled = true;
                        pnl_Item.Enabled = true;
                        pnl_number.Enabled = true;
                        txt_number.Text = "6";
                        break;
                    }
                case 54:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        txtFromDate.Enabled = true;
                        txtToDate.Enabled = true;
                        pnl_user.Visible = true;
                        pnlPromoter.Visible = false;
                        pnlaccount.Visible = false;
                        chk_user.Visible = false;
                        txt_user.Enabled = true;
                        btn_user.Enabled = true;
                        break;
                    }
                case 55:
                    {
                        pnlAsAtDate.Enabled = true;
                        txtAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        txtFromDate.Enabled = false;
                        txtToDate.Enabled = false;
                        cmb_Week.Enabled = true;
                        break;
                    }
                case 56:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        pnlPayType.Visible = true;
                        break;
                    }
                case 57:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        break;
                    }
                case 58:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        pnl_Item.Enabled = true;
                        break;
                    }
                case 59:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        pnl_Item.Enabled = true;
                        break;
                    }
                case 61:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        pnl_Item.Enabled = true;
                        cmb_itm_grp.Enabled = true;
                        pnlHP.Enabled = true;
                        break;
                    }
                case 62:
                    {
                        pnlAsAtDate.Enabled = false;
                        pnlDateRange.Enabled = true;
                        pnl_Sch.Enabled = false;
                        pnlCusId.Enabled = false;
                        pnlAccBal.Enabled = false;
                        pnlOther.Enabled = false;
                        pnlHP.Enabled = false;
                        txtScheme.Enabled = false;
                        txtId.Enabled = false;
                        txtAccBal.Enabled = false;

                        break;
                    }
                case 63:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = false;
                        pnl_Sch.Enabled = false;
                        pnlCusId.Enabled = true;
                        pnlAccBal.Enabled = true;
                        pnlOther.Enabled = true;
                        pnlHP.Enabled = true;
                        txtScheme.Enabled = false;
                        txtId.Enabled = true;
                        txtAccBal.Enabled = true;
                        if (txtAccBal.Text == "")
                        {
                            txtAccBal.Text = Convert.ToString(0);

                        }
                        break;
                    }

                case 64:
                    {
                        pnlAsAtDate.Enabled = true;
                        pnlDateRange.Enabled = false;
                        pnl_Sch.Enabled = false;
                        pnlCusId.Enabled = false;
                        pnlAccBal.Enabled = false;
                        pnlOther.Enabled = false;
                        pnlHP.Enabled = false;
                        txtScheme.Enabled = false;
                        txtId.Enabled = false;
                        txtAccBal.Enabled = false;
                        pnlOther.Enabled = false;
                        if (txtAccBal.Text == "")
                        {
                            txtAccBal.Text = Convert.ToString(0);

                        }
                        break;
                    }
            }
        }

        protected void GetCompanyDet(object sender, EventArgs e)
        {
            try
            {
                MasterCompany _masterComp = null;
                _masterComp = CHNLSVC.General.GetCompByCode(txtComp.Text);
                if (_masterComp != null)
                {
                    txtCompDesc.Text = _masterComp.Mc_desc;
                    txtCompAddr.Text = _masterComp.Mc_add1 + _masterComp.Mc_add2;
                    opt10.Text = ConvertTo_ProperCase(_masterComp.Mc_anal3.ToString()) + " Fund";
                }
                else
                {
                    txtCompDesc.Text = "";
                    txtCompAddr.Text = "";
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void GetPCDet(object sender, EventArgs e)
        {
            try
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
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
            try
            {
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

                List<string> _listLocs = new List<string>();
                if (_isPCFound == false)
                {
                    BaseCls.GlbReportProfit = txtPC.Text;
                    Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC(BaseCls.GlbUserID, txtComp.Text, txtPC.Text, null);
                    _listLocs.Add(txtPC.Text);
                }
                else
                {
                    _listLocs = lstPC.Items.Cast<ListViewItem>().Where(item => item.Checked).Select(item => item.Text).ToList();

                }
                if (_listLocs.Count > 0) CHNLSVC.Security.Add_User_Selected_Loc_Pc_DR(BaseCls.GlbUserID, txtComp.Text, txtPC.Text, null, _listLocs);


            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            btnClear.Focus();
            try
            {
                //check whether current session is expired
                CheckSessionIsExpired();

                //kapila 4/7/2014
                if (CheckServerDateTime() == false) return;

                //check this user has permission for this Loc
                if (txtPC.Text != string.Empty)
                {
                    Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(BaseCls.GlbUserComCode, txtPC.Text);
                    if (_IsValid == false)
                    {
                        MessageBox.Show("Invalid Profit Center.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                    //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPH"))
                    //Add by Chamal 30-Aug-2013
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10043))
                    {
                        Int16 is_Access = CHNLSVC.Security.Check_User_PC(BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtPC.Text);
                        if (is_Access != 1)
                        {
                            //MessageBox.Show("Access Denied.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MessageBox.Show("Sorry, You have no permission for view reports!\n( Advice: Required permission code :10043)", "HP Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }

                BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Value).Date;
                BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Value).Date;
                BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Value).Date;

                btnDisplay.Enabled = false;


                BaseCls.GlbReportName = string.Empty;
                GlbReportName = string.Empty;

                if (opt1.Checked == true)  // Collection Summary
                {   //update temporary table
                    update_PC_List();
                    //18-02-13 Nadeeka
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportComp = txtComp.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportExecCode = "";
                    if (chkMan.Checked == false)
                        if (string.IsNullOrEmpty(txtMan.Text))
                        {
                            MessageBox.Show("Please select the manager code", "HP Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        else
                            BaseCls.GlbReportExecCode = txtMan.Text;

                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "HPCollectionSummary.rpt";

                    _view.Show();
                    _view = null;
                }



                if (opt3.Checked == true)  //age analysis of debtors arrears
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Text);
                    BaseCls.GlbReportGroupBy = cmbGroupBy.Text;
                    BaseCls.GlbReportName = "Age_Debtors_Arrears.rpt";
                    BaseCls.GlbReportStartOn = DateTime.Now;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportScheme = "";

                    //if (chkAllScheme.Checked == true)
                    //{
                    //     BaseCls.GlbReportScheme = "ALL";
                    //}
                    //else
                    //{
                    foreach (ListViewItem Item in lstSch.Items)
                    {
                        BaseCls.GlbReportScheme = BaseCls.GlbReportScheme == "" ? "^" + Item.Text + "$" : BaseCls.GlbReportScheme + "|" + "^" + Item.Text + "$";
                    }

                    //} 
                    BaseCls.GlbReportDoc = "N";

                    if (MessageBox.Show("Do you want to view service details ?", "Approve", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        BaseCls.GlbReportDoc = "Y";
                    }

                    ReportViewerHP _view = new ReportViewerHP();
                    _view.GlbReportName = "Age_Debtors_Arrears.rpt";
                    _view.Show();
                    _view = null;

                    if (BaseCls.GlbReportDoc == "Y")
                    {
                        ReportViewerHP _view1 = new ReportViewerHP();
                        _view1.GlbReportName = "Age_Debtors_Arrears_Service.rpt";
                        _view1.Show();
                        _view1 = null;
                    }

                }
                else if (opt5.Checked == true)
                {
                    update_PC_List();

                    string vScheme = "";
                    foreach (ListViewItem Item in lstSch.Items)
                    {
                        vScheme = vScheme == "" ? "^" + Item.Text + "$" : vScheme + "|" + "^" + Item.Text + "$";
                    }

                    BaseCls.GlbReportComp = txtComp.Text;
                    BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Text).Date;
                    BaseCls.GlbReportGroupBy = cmbGroupBy.Text;
                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportScheme = vScheme == "" ? txtScheme.Text == "" ? txtScheme.Text : "^" + txtScheme.Text + "$" : vScheme;

                    ReportViewerHP _view = new ReportViewerHP();
                    if (BaseCls.GlbReportGroupBy == "Channel Wise Summary")
                    {
                        BaseCls.GlbReportName = "HPClosingBalChannelSummary.rpt";
                        _view.GlbReportName = "HPClosingBalChannelSummary.rpt";
                    }
                    else
                    {
                        BaseCls.GlbReportName = "HPClosingBalSummaryRep.rpt";
                        _view.GlbReportName = "HPClosingBalSummaryRep.rpt";
                    }
                    _view.Show();
                    _view = null;
                }


                if (opt6.Checked == true)  //Total Receivable Movement
                {      //18-02-13 Nadeeka
                    update_PC_List();
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportComp = txtComp.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;


                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "ReceivableMovementReports.rpt";

                    _view.Show();
                    _view = null;
                }
                if (opt7.Checked == true)  //Total Receivable Movement summary
                {
                    update_PC_List();
                    //18-02-13 Nadeeka
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportComp = txtComp.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;


                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "ReceivableMovementSummaryReports.rpt";

                    _view.Show();
                    _view = null;
                }
                if (opt8.Checked == true)  //HP Cash Flow forecasting
                {    ///11-02-2013 Nadeeka
                    clsHpSalesRep objHp = new clsHpSalesRep();
                    //update temporary table
                    update_PC_List();
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportComp = txtComp.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportFromDate = Convert.ToDateTime(txtAsAtDate.Text);
                    BaseCls.GlbReportName = "HP_CashFlowForecastingReport.rpt";
                    objHp.HPCashFlowForecastingReport();
                    string _repPath = "";
                    MasterCompany _masterComp = null;
                    _masterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());

                    if (_masterComp != null)
                    {
                        _repPath = _masterComp.Mc_anal6;

                    }


                    //objHp._recCashFlow.ExportToDisk(ExportFormatType.Excel, @"\\192.168.1.222\SCM\Reports\HP_CashFlowForecastingReport.xls");
                    objHp._recCashFlow.ExportToDisk(ExportFormatType.Excel, _repPath + "HP_CashFlowForecastingReport" + BaseCls.GlbUserID + ".xls");

                    Excel.Application excelApp = new Excel.Application();
                    excelApp.Visible = true;




                    string workbookPath = _repPath + "HP_CashFlowForecastingReport" + BaseCls.GlbUserID + ".xls";
                    //string workbookPath = @"\\192.168.1.222\SCM\Reports\HP_CashFlowForecastingReport.xls";
                    Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath,
                            0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                            true, false, 0, true, false, false);





                }
                if (opt19.Checked == true)  //HP Cash Flow forecasting
                {    ///21-02-2013 Nadeeka
                    clsHpSalesRep objHp = new clsHpSalesRep();
                    //update temporary table
                    update_PC_List();
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportComp = txtComp.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportFromDate = Convert.ToDateTime(txtAsAtDate.Text);
                    BaseCls.GlbReportName = "HP_CashFlowForecastingSummaryReport.rpt";
                    string _repPath = "";
                    MasterCompany _masterComp = null;
                    _masterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());

                    if (_masterComp != null)
                    {
                        _repPath = _masterComp.Mc_anal6;

                    }
                    objHp.HPCashFlowForecastingReport();

                    objHp._recCashFlowSum.ExportToDisk(ExportFormatType.Excel, _repPath + "HP_CashFlowForecastingSummaryReport" + BaseCls.GlbUserID + ".xls");
                    Excel.Application excelApp = new Excel.Application();
                    excelApp.Visible = true;

                    string workbookPath = _repPath + "HP_CashFlowForecastingSummaryReport" + BaseCls.GlbUserID + ".xls";
                    Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath,
                            0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                            true, false, 0, true, false, false);

                }
                //if (opt9.Checked == true)  //Insurance Fund
                //{
                //    //update temporary table
                //    update_PC_List();
                //    BaseCls.GlbReportProfit = txtPC.Text;
                //    BaseCls.GlbReportComp = txtComp.Text;
                //    BaseCls.GlbReportCompCode = txtComp.Text;
                //    BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Text);
                //    // BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Text);

                //    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                //    BaseCls.GlbReportName = "InsuranceFund.rpt";

                //    _view.Show();
                //    _view = null;
                //}
                if (opt10.Checked == true)  //Insurance  
                {      //18-02-13 Nadeeka
                    //update temporary table
                    update_PC_List();
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportComp = txtComp.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportGroupBy = cmbGroupBy.Text;

                    BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtToDate.Text);
                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "HPInsurance.rpt";

                    _view.Show();
                    _view = null;

                    Reports.HP.ReportViewerHP _viewsub = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "InsuranceFund.rpt";

                    _viewsub.Show();
                    _viewsub = null;

                }

                if (opt11.Checked == true)   //No of Created Accounts
                {
                    //update temporary table
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = "No of Created Accounts Report";

                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "No_of_Acc_Report.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt12.Checked == true)   //Revert and Release
                {
                    //update temporary table
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = "Revert and Revert Release Report";
                    if (txtInstall.Text == "")
                    {
                        BaseCls.GlbReportDiscRate = 0;
                    }
                    else
                    {
                        BaseCls.GlbReportDiscRate = Convert.ToDecimal(txtInstall.Text);
                    }
                    if (optAll.Checked)
                    {
                        BaseCls.GlbReportExeType = "All";
                    }
                    else if (optLs.Checked)
                    {
                        BaseCls.GlbReportExeType = "<";
                    }
                    else if (optEq.Checked)
                    {
                        BaseCls.GlbReportExeType = "=";
                    }
                    else if (optGt.Checked)
                    {
                        BaseCls.GlbReportExeType = ">";
                    }

                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "Revert_and_Release_Report.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt13.Checked == true)  // Collected other shop Collection Summary
                {   //update temporary table
                    update_PC_List();
                    //18-02-13 Nadeeka
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportComp = txtComp.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;

                    BaseCls.GlbReportType = string.Empty;
                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "HPOtheCollectionSummary.rpt";

                    _view.Show();
                    _view = null;
                }

                if (opt14.Checked == true)  // Received other shop Collection Summary
                {   //update temporary table
                    update_PC_List();
                    //18-02-13 Nadeeka
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportComp = txtComp.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;

                    BaseCls.GlbReportType = "REC";
                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "HPOtheCollectionSummaryRec.rpt";

                    _view.Show();
                    _view = null;
                }

                if (opt15.Checked == true)   //No of Active Accounts
                {
                    //update temporary table
                    update_PC_List_RPTDB();


                    BaseCls.GlbReportHeading = "No of Active Accounts Report";
                    BaseCls.GlbReportComp = txtComp.Text;
                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "No_of_Act_Accounts.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt16.Checked == true)   //Credit Debit Note 
                {
                    //update temporary table
                    update_PC_List_RPTDB();
                    //20-02-13 Nadeeka

                    BaseCls.GlbReportHeading = "Credit Debit Note";
                    BaseCls.GlbReportComp = txtComp.Text;
                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "HpCeditDebitNote.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt17.Checked == true)   //Excess/Short Outstanding Statment
                {
                    //update temporary table
                    update_PC_List_RPTDB();
                    //22-02-13 Nadeeka
                    BaseCls.GlbReportFromDate = Convert.ToDateTime(txtAsAtDate.Text);
                    BaseCls.GlbReportHeading = "Excess/Short Outstanding statement";
                    BaseCls.GlbReportComp = txtComp.Text;
                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "ExcessShortOutstandingStatement.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt18.Checked == true)   //Transfered Accounts
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "Transfered Accounts Report";
                    BaseCls.GlbReportComp = txtComp.Text;
                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "Transfered_Accounts_Report.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt20.Checked == true)   //Closed Accounts
                {
                    //update temporary table
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = "Closed Accounts Details";
                    BaseCls.GlbReportComp = txtComp.Text;
                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "ClosedAccountsDetails.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt21.Checked == true)   //Closed Accounts
                {
                    //update temporary table
                    update_PC_List_RPTDB();


                    BaseCls.GlbReportHeading = "Unused Receipt Books";
                    BaseCls.GlbReportComp = txtComp.Text;
                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "UnusedReceipts.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt22.Checked == true)   //Current month Due Summary
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "CURRENT MONTH DUE SUMMARY";
                    BaseCls.GlbReportScheme = txtScheme.Text;
                    BaseCls.GlbReportFromDate = txtFromDate.Value;
                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "Curr_Month_Due_Report.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt23.Checked == true)   //All Due Summary
                {
                    //update temporary table
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = "ALL DUE SUMMARY";
                    BaseCls.GlbReportScheme = txtScheme.Text;
                    BaseCls.GlbReportFromDate = txtFromDate.Value;
                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "All_Due_Summ_Report.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt24.Checked == true)   //All Due Summary
                {
                    //update temporary table
                    update_PC_List_RPTDB();
                    if (txtAccBal.Text == "")
                    {
                        txtAccBal.Text = Convert.ToString(0);

                    }

                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportComp = txtComp.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportScheme = txtScheme.Text;
                    BaseCls.GlbReportCusId = txtId.Text;
                    BaseCls.GlbReportCusAccBal = Convert.ToDouble(txtAccBal.Text);
                    BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Text);
                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "HPMultipleAccounts.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt25.Checked == true)   //Total Arrears Report
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportScheme = txtScheme.Text;
                    BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Text);
                    BaseCls.GlbReportHeading = "TOTAL ARREARS REPORT";

                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "Total_Arrears_Report.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt26.Checked == true)   //diriya fund
                {
                    //update temporary table
                    update_PC_List_RPTDB();

                    MasterCompany _masterComp = null;
                    _masterComp = CHNLSVC.General.GetCompByCode(txtComp.Text);
                    if (_masterComp != null)
                    {
                        //add by Chamal 17-Jun-2014
                        BaseCls.GlbReportHeading = _masterComp.Mc_anal3.ToUpper().ToString() + " FUND REPORT";
                    }

                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;

                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "InsuFund.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt27.Checked == true)   //Grace Period Arrears
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportScheme = txtScheme.Text;
                    BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Text);
                    BaseCls.GlbReportHeading = "GRACE PERIOD ARREARS COLLECTION REPORT";

                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "GR_P_Arrears_Report.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt28.Checked == true)   //Collection Bonus
                {
                    //update temporary table
                    update_PC_List();
                    BaseCls.GlbReportComp = txtComp.Text;
                    BaseCls.GlbReportScheme = txtScheme.Text;
                    BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Text);
                    BaseCls.GlbReportHeading = "COLLECTION BONUS REPORT";

                    Reports.HP.clsHpSalesRep _clsHp = new Reports.HP.clsHpSalesRep();
                    BaseCls.GlbReportHeading = "COLLECTION BONUS REPORT";
                    //  _clsHp.CollectionBonus();
                    _clsHp.CollectionBonusNew();
                    _clsHp.CollectionBonus_SUMMARY();
                }

                if (opt29.Checked == true)   //hp information 1/10/2013 kapila
                {
                    //update temporary table
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;

                    BaseCls.GlbReportHeading = "Hire Purchase Informations";

                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "HP_Infor.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt30.Checked == true)   //Customer Details 1/10/2013 sanjeewa
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "Customer Details";

                    BaseCls.GlbReportDiscTp = 3;
                    string _filePath = string.Empty;
                    string _error = string.Empty;

                    //_filePath = CHNLSVC.Sales.GetRec_Age_Analysis_New(BaseCls.GlbReportYear, BaseCls.GlbReportMonth, BaseCls.GlbReportnoofDays, BaseCls.GlbUserComCode, BaseCls.GlbUserID, BaseCls.GlbReportGroupBy, out _error);
                    if (chkRedBal.Checked == true)
                    {
                        _filePath = CHNLSVC.MsgPortal.GetCutomerDetails_ReduceBal(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbUserID, BaseCls.GlbReportDiscTp, BaseCls.GlbUserComCode, out _error);

                    }
                    else
                    {
                        _filePath = CHNLSVC.MsgPortal.GetCutomerDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbUserID, BaseCls.GlbReportDiscTp, BaseCls.GlbUserComCode, out _error);

                    }


                    if (!string.IsNullOrEmpty(_error))
                    {
                        MessageBox.Show(_error);
                        return;
                    }

                    if (string.IsNullOrEmpty(_filePath))
                    {
                        MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();
                    //objHp.ExportCustomerDetailReport();
                    //fldgOpenPath.ShowDialog();


                    //string sourcefileName = BaseCls.GlbUserID + ".xls";
                    //string targetfileName = ".xls";
                    //string sourcePath = @"\\192.168.1.222\scm2\Print";
                    //string targetPath = BaseCls.GlbReportFilePath;
                    //string sourceFile = System.IO.Path.Combine(sourcePath, sourcefileName);
                    //string targetFile = targetPath + targetfileName;

                    //System.IO.File.Copy(sourceFile, targetFile);
                    //System.IO.File.Delete(sourceFile);

                    MessageBox.Show("Export Completed", "HP Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnDisplay.Enabled = true;
                }

                if (opt31.Checked == true)   //HP Recievable Age Analysis
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "HP RECIEVABLE AGE ANALYSIS";
                    BaseCls.GlbReportYear = Convert.ToInt32(cmbYear.Text);
                    BaseCls.GlbReportMonth = DateTime.Parse("1-" + cmbMonth.Text + "-2013").Month;
                    if (txt_number.Text == "")
                    {
                        BaseCls.GlbReportnoofDays = 0;
                    }
                    else
                    {
                        BaseCls.GlbReportnoofDays = Convert.ToInt16(txt_number.Text);
                    }
                    BaseCls.GlbReportGroupBy = cmbGroupBy.Text == "Company" ? "COM" : cmbGroupBy.Text == "Profit Center" ? "LOC" : "ACC";
                    BaseCls.GlbReportDocType = chkRedBal.Checked == true ? "Y" : "N";
                    BaseCls.GlbReportDoc1 = opt_sum.Checked == true ? "A" : "B";
                    if (chkRntSch.Checked == true)
                    {
                        BaseCls.GlbReportDoc1 = opt_sum.Checked == true ? "A1" : "B1";
                    }

                    bool isSupp = false;
                    if (chkSupp.Checked == true)
                    {
                        isSupp = true;
                    }

                    //Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    //BaseCls.GlbReportName = "Recievable_Age_Analysis.rpt";
                    //_view.Show();
                    //_view = null;

                    string _filePath = string.Empty;
                    string _error = string.Empty;

                    _filePath = CHNLSVC.MsgPortal.GetRec_Age_Analysis_New(BaseCls.GlbReportYear, BaseCls.GlbReportMonth, BaseCls.GlbReportnoofDays, BaseCls.GlbUserComCode, BaseCls.GlbReportDoc1, BaseCls.GlbUserID, BaseCls.GlbReportGroupBy, BaseCls.GlbReportDocType, isSupp, out _error);

                    if (!string.IsNullOrEmpty(_error))
                    {
                        MessageBox.Show(_error);
                        return;
                    }

                    if (string.IsNullOrEmpty(_filePath))
                    {
                        MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();

                    string _filePathnew = "";
                    _filePathnew = CHNLSVC.MsgPortal.GetRCV_AGE_CURRRNT(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbUserDefLoca, "LGD Computation", Convert.ToDateTime(txtFromDate.Text).Date,
                        Convert.ToDateTime(txtToDate.Text).Date, out _error);

                    if (!string.IsNullOrEmpty(_error))
                    {
                        MessageBox.Show(_error);
                        return;
                    }

                    if (string.IsNullOrEmpty(_filePathnew))
                    {
                        MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Process p1 = new Process();
                    p1.StartInfo = new ProcessStartInfo(_filePathnew);
                    p1.Start();


                }


                if (opt32.Checked == true)   //Hp REceipt List -- Nadeeka
                {
                    update_PC_List();
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;

                    BaseCls.GlbReportHeading = "HP Receipt List";

                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "HPCollectionList.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt33.Checked == true)   //Agreement Statement - Sanjeewa 2013-12-19
                {
                    update_PC_List();

                    BaseCls.GlbReportHeading = "AGREEMENT STATEMENT";

                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "Agreement_Statement.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt34.Checked == true)   //Agreement Checking Status - Sanjeewa 2013-12-20
                {
                    update_PC_List();

                    BaseCls.GlbReportHeading = "AGREEMENT CHECKING STATUS";

                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "Agreement_Check_Status.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt35.Checked == true)   //Agreement Statement Detail - Sanjeewa 2013-12-21
                {
                    update_PC_List();

                    BaseCls.GlbReportHeading = "AGREEMENT STATEMENT DETAIL";

                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "Agreement_Statement_Dtl.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt36.Checked == true)   //Group Sale Report - Sanjeewa 2014-01-20
                {
                    update_PC_List();
                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportDocType = txtDocType.Text;
                    BaseCls.GlbReportHeading = "GROUP SALE REPORT";
                    BaseCls.GlbReportGroupBy = cmbGroupBy.Text;
                    BaseCls.GlbReportItemType = opt_sum.Checked == true ? "N" : "Y";
                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "Grp_Sale_Report.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt37.Checked == true)   //HP Receivable Report - Nadeeka 2014-01-21
                {
                    update_PC_List();
                    BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Value).Date;
                    BaseCls.GlbReportHeading = "HP Receivable Report";
                    BaseCls.GlbReportItemCatType = cmbCatType.Text;
                    if (BaseCls.GlbReportItemCatType == "Electronic")
                    {
                        BaseCls.GlbReportItemCatType = "ELEC";

                    }
                    if (BaseCls.GlbReportItemCatType == "Non Electronic")
                    {
                        BaseCls.GlbReportItemCatType = "NELEC";

                    }
                    if (BaseCls.GlbReportItemCatType == "Default")
                    {
                        BaseCls.GlbReportItemCatType = null;

                    }
                    BaseCls.GlbReportGroupBy = cmbGroupBy.Text;
                    BaseCls.GlbReportComp = txtComp.Text;
                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "HPReceivableDetails.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt38.Checked == true)   //Agreement Check list - Sanjeewa 2014-01-23
                {
                    update_PC_List();

                    BaseCls.GlbReportHeading = "AGREEMENT RECEIVED REPORT";

                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "Agreement_Checklist.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt39.Checked == true)   //Additional Incentives for Schemes - Sanjeewa 2014-03-13
                {
                    update_PC_List();

                    BaseCls.GlbReportHeading = "ADDITIONAL INCENTIVES FOR SCHEMES";

                    BaseCls.GlbReportScheme = txtScheme.Text;
                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "add_Incentive_Scheme_Report.rpt";
                    _view.Show();
                    _view = null;
                }

                else if (opt40.Checked == true)
                {
                    update_PC_List();
                    BaseCls.GlbReportScheme = txtScheme.Text;
                    BaseCls.GlbReportGroupBy = cmbGroupBy.Text;

                    BaseCls.GlbReportHeading = "TRIM ACCOUNTS";
                    ReportViewerHP _view = new ReportViewerHP();
                    BaseCls.GlbReportName = "trim_account_report.rpt";
                    _view.GlbReportName = "trim_account_report.rpt";

                    _view.Show();
                    _view = null;
                }

                else if (opt41.Checked == true)
                {
                    update_PC_List();

                    BaseCls.GlbReportHeading = "CUSTOMER ACKNOWLEDGEMENT LOG";
                    BaseCls.GlbReportDoc = txtAccountNo.Text.Trim();

                    ReportViewerHP _view = new ReportViewerHP();
                    BaseCls.GlbReportName = "Cust_Ack_Log_Report.rpt";
                    _view.GlbReportName = "Cust_Ack_Log_Report.rpt";

                    _view.Show();
                    _view = null;
                }
                else if (opt42.Checked == true)
                {
                    update_PC_List();

                    BaseCls.GlbReportHeading = "Revert Release Actioned by Other Showroom";
                    BaseCls.GlbReportDoc = txtAccountNo.Text.Trim();
                    if (optBoth.Checked)
                    {
                        BaseCls.GlbReportStatus = 0;
                    }
                    if (optSR.Checked)
                    {
                        BaseCls.GlbReportStatus = 1;
                    }
                    if (optOther.Checked)
                    {
                        BaseCls.GlbReportStatus = 2;
                    }



                    ReportViewerHP _view = new ReportViewerHP();
                    BaseCls.GlbReportName = "RevertRelAction_Oth_Sr.rpt";
                    _view.GlbReportName = "RevertRelAction_Oth_Sr.rpt";

                    _view.Show();
                    _view = null;
                }

                else if (opt43.Checked == true) //Net no of Accounts Sanjeewa 2014-06-06
                {
                    update_PC_List();

                    BaseCls.GlbReportHeading = "NET NO OF ACCOUNTS";
                    BaseCls.GlbReportDoc = txtAccountNo.Text.Trim();

                    ReportViewerHP _view = new ReportViewerHP();
                    BaseCls.GlbReportName = "HP_Pure_Creation.rpt";
                    _view.GlbReportName = "HP_Pure_Creation.rpt";

                    _view.Show();
                    _view = null;
                }

                else if (opt44.Checked == true) //kapila 1/8/2014
                {
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = "Not received Manager issuances";
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;

                    ReportViewerHP _view = new ReportViewerHP();
                    BaseCls.GlbReportName = "NotRecManIssues.rpt";
                    _view.GlbReportName = "NotRecManIssues.rpt";

                    _view.Show();
                    _view = null;
                }

                else if (opt45.Checked == true) // Nadeeka 29-aug-2014
                {
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = " HP Interest Report- IFRS";
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    //if (optSR.Checked == true)//opt_sum
                    if (opt_sum.Checked == true)//opt_sum
                    {
                        BaseCls.GlbReportWithStatus = 1;//Summary
                    }
                    else
                    {
                        BaseCls.GlbReportWithStatus = 0;//Detail
                    }

                    //GlbReportWithDetail - Use this for schdule base tag
                    ReportViewerHP _view = new ReportViewerHP();
                    if (chkRntSch.Checked == true && opt_sum.Checked == false)
                    {
                        BaseCls.GlbReportWithDetail = 1;
                        //BaseCls.GlbReportName = "InterestCalcReduceBal_new.rpt";
                        //_view.GlbReportName = "InterestCalcReduceBal_new.rpt";
                        string _filePath = string.Empty;
                        string _error = string.Empty;

                        //_filePath = CHNLSVC.MsgPortal.getpurchaseOrderSummeryExcel(_invRepPara._GlbReportComp, "  ", _invRepPara._GlbEntryType, _invRepPara._GlbReportSupplier, _invRepPara._GlbReportItemCode, _invRepPara._GlbReportItemCat1, _invRepPara._GlbReportItemCat2, _invRepPara._GlbReportFromDate, _invRepPara._GlbReportToDate, _invRepPara._GlbReportDocType, _invRepPara._GlbReportDoc, _invRepPara._GlbUserID, out _error);
                        _filePath = CHNLSVC.MsgPortal.HP_intrest_report(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, Convert.ToDateTime(txtFromDate.Text).Date, Convert.ToDateTime(txtToDate.Text).Date,
                            BaseCls.GlbUserDefProf, BaseCls.GlbReportWithStatus, BaseCls.GlbReportWithDetail, BaseCls.GlbReportHeading, out _error);
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

                        MessageBox.Show("Export Completed", "Sales Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                    else
                    {
                        BaseCls.GlbReportWithDetail = 0;
                    BaseCls.GlbReportName = "InterestCalcReduceBal.rpt";
                    _view.GlbReportName = "InterestCalcReduceBal.rpt";
                    _view.Show();
                    _view = null;
                }



                    //     ReportViewerHP _view = new ReportViewerHP();
                    //BaseCls.GlbReportName = "InterestCalcReduceBal.rpt";
                    //_view.GlbReportName = "InterestCalcReduceBal.rpt";

                    //_view.Show();
                    //_view = null;
                }

                else if (opt46.Checked == true)
                { // Nadeeka
                    update_PC_List();
                    Reports.HP.clsHpSalesRep _clsHp = new Reports.HP.clsHpSalesRep();

                    _clsHp.RevertStatus();

                }

                if (opt47.Checked == true)   //Pending Agreements - Sanjeewa 2014-10-22
                {
                    update_PC_List();

                    BaseCls.GlbReportHeading = "PENDING AGREEMENTS";

                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "Agreement_Pending.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt48.Checked == true)   //No of Completed Agreements - Sanjeewa 2014-11-18
                {
                    update_PC_List();

                    BaseCls.GlbReportHeading = "NO OF COMPLETED AGREEMENTS";

                    string _filePath = string.Empty;
                    string _error = string.Empty;
                    _filePath = CHNLSVC.Sales.GetNoofCompletedAgreements(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserComCode, BaseCls.GlbUserID, out _error);

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

                if (opt49.Checked == true)   //Collection Bonus Reconciliation - Sanjeewa 2014-12-22
                {
                    update_PC_List();

                    BaseCls.GlbReportHeading = "COLLECTION BONUS RECONCILIATION";

                    BaseCls.GlbReportDocType = optclaim.Checked == true ? "CLAIM" : optunclaim.Checked == true ? "UNCLAIM" : opthold.Checked == true ? "HOLD" : "ALL";
                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "Collection_Bonus_Recon_Rep.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt50.Checked == true)   //Revert Info - Sanjeewa 2015-01-12
                {
                    update_PC_List();

                    BaseCls.GlbReportHeading = "REVERT INFO ";
                    BaseCls.GlbReportDocType = "RVT";

                    string _filePath = string.Empty;
                    string _error = string.Empty;
                    _filePath = CHNLSVC.MsgPortal.GetRevertInfoDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbReportDocType, BaseCls.GlbUserComCode, BaseCls.GlbUserID, out _error);

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

                if (opt51.Checked == true)   //Revert Release Info - Sanjeewa 2015-01-12
                {
                    update_PC_List();

                    BaseCls.GlbReportHeading = "REVERT RELEASE INFO";
                    BaseCls.GlbReportDocType = "RLS";

                    string _filePath = string.Empty;
                    string _error = string.Empty;
                    _filePath = CHNLSVC.MsgPortal.GetRevertInfoDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbReportDocType, BaseCls.GlbUserComCode, BaseCls.GlbUserID, out _error);

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

                if (opt52.Checked == true)   //Introducer Commision Done by Nadeeka 25-02-2015
                {
                    update_PC_List();

                    BaseCls.GlbReportHeading = "INTRODUCER COMMISSION REPORT";
                    BaseCls.GlbReportExecCode = Convert.ToString(cmbTechnician.SelectedValue);
                    BaseCls.GlbReportScheme = txtScheme.Text;
                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "IntroducerCommission_Report.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt53.Checked == true)   //Arrears Statement - Sanjeewa 2015-03-14
                {
                    update_PC_List();

                    BaseCls.GlbReportHeading = "ARREARS STATEMENT";

                    Boolean isItem = false;
                    string _filePath = string.Empty;
                    string _error = string.Empty;
                    _filePath = CHNLSVC.Financial.Process_AgeOfDebtors_Arrears36(Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbUserComCode, BaseCls.GlbUserID, BaseCls.GlbReportScheme, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportModel, BaseCls.GlbReportBrand, Convert.ToInt16(txt_number.Text), isItem, out _error);

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

                if (opt54.Checked == true)   //kapila
                {
                    if (string.IsNullOrEmpty(txt_user.Text))
                    {
                        MessageBox.Show("Please select the user", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txt_user.Focus();
                        return;
                    }
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = "COMPLETED AGREEMENT DETAIL";
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportUser = txt_user.Text;
                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "HP_Completed_Agreement.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt55.Checked == true)   //Weekly Reports Acknowledgement (Document Check List) - Sanjeewa 2015-07-09
                {
                    update_PC_List();

                    BaseCls.GlbReportHeading = "WEEKLY REPORTS ACKNOWLEDGEMENT (DOCUMENT CHECK LIST)";
                    BaseCls.GlbReportWeek = Convert.ToInt32(cmb_Week.SelectedIndex + 1);

                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "Doc_Chk_List_Report.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt56.Checked == true)   //Given Paymode wise Collections and Downpayments - Sanjeewa 2015-11-12
                {
                    update_PC_List();

                    BaseCls.GlbReportHeading = "GIVEN PAYMODE WISE COLLECTIONS AND DOWNPAYMENTS";
                    BaseCls.GlbReportDoc = "";

                    string _filePath = string.Empty;
                    string _error = string.Empty;
                    _filePath = CHNLSVC.Financial.Process_GivenPaymode_Collection(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserComCode, BaseCls.GlbReportDoc, BaseCls.GlbUserID, out _error);

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
                if (opt57.Checked)  //hp introducer comm details
                {
                    //update temporary table
                    update_PC_List();

                    string _filePath = string.Empty;
                    string _error = string.Empty;

                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;

                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;

                    BaseCls.GlbReportHeading = "HP Introducer Commission Details";

                    _filePath = CHNLSVC.MsgPortal.ProcessHPIntroCommReport(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbReportChannel, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, out _error);

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
                if (opt58.Checked)  //age of revert
                {
                    //update temporary table
                    update_PC_List();

                    string _filePath = string.Empty;
                    string _error = string.Empty;

                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;

                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;

                    BaseCls.GlbReportHeading = "Age of Revert";

                    _filePath = CHNLSVC.MsgPortal.GetAgeOfRevertReportDetails(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportCompCode, BaseCls.GlbReportBrand, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCode, BaseCls.GlbUserID, out _error);

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

                if (opt59.Checked == true)   //service charge
                {

                    update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = "SERVICE CHARGE REPORT";
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;

                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "HP_Service_Charge.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt60.Checked)  //closed accounts details - kapila 31/3/2017
                {
                    //update temporary table
                    update_PC_List();

                    string _filePath = string.Empty;
                    string _error = string.Empty;

                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;

                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;

                    BaseCls.GlbReportHeading = "Closed Accounts Details";

                    _filePath = CHNLSVC.MsgPortal.Get_ClosedAccountsDet(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportCompCode, BaseCls.GlbUserID, out _error);

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

                if (opt61.Checked == true)   //mobile interest income - kapila 4/7/2017
                {
                    if (string.IsNullOrEmpty(cmb_itm_grp.Text))
                    {
                        MessageBox.Show("Please select the report category", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Cursor.Current = Cursors.Default;
                        cmb_itm_grp.Focus();
                        return;
                    }
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = "MOBILE INTEREST INCOME";
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportBrand = txtBrand.Text;
                    BaseCls.GlbReportModel = txtModel.Text;
                    BaseCls.GlbReportItemCode = txtItemCode.Text;
                    BaseCls.GlbReportItemCat1 = txtIcat1.Text;
                    BaseCls.GlbReportItemCat2 = txtIcat2.Text;
                    BaseCls.GlbReportItemCat3 = txtIcat3.Text;
                    BaseCls.GlbReportDocType = cmb_itm_grp.Text;

                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "HP_mobile_int_income.rpt";
                    _view.Show();
                    _view = null;

                    Reports.HP.ReportViewerHP _view1 = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "HP_mobile_int_income_1.rpt";
                    _view1.Show();
                    _view1 = null;
                }
                if (opt62.Checked == true)  //add  by tharanga 2017/11/23
                {
                    update_PC_List();
                    BaseCls.GlbReportFromDate = txtFromDate.Value;
                    BaseCls.GlbReportToDate = txtToDate.Value;
                    BaseCls.GlbReportHeading = "Accounts Creation Restriction History";
                    BaseCls.GlbReportComp = BaseCls.GlbUserComCode;


                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "HP_Acc_Rescription_History.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt63.Checked == true)  //add  by tharanga 2017/11/23
                {
                    update_PC_List();
                    BaseCls.GlbReportFromDate = txtFromDate.Value;
                    BaseCls.GlbReportToDate = txtAsAtDate.Value;
                    BaseCls.GlbReportHeading = "Accounts Creation Restriction As at Date";
                    BaseCls.GlbReportComp = BaseCls.GlbUserComCode;


                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "HP_Acc_Rescription_History.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt64.Checked == true)  //add  by dilshan on 2018/11/02
                {
                    update_PC_List();
                    BaseCls.GlbReportFromDate = txtFromDate.Value;
                    BaseCls.GlbReportToDate = txtAsAtDate.Value;
                    BaseCls.GlbReportHeading = "Reject Account Details";
                    BaseCls.GlbReportComp = BaseCls.GlbUserComCode;


                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    BaseCls.GlbReportName = "HP_Reject_Acc_Details.rpt";
                    _view.Show();
                    _view = null;
                }

                //if (opt64.Checked == true)  //add  by Wimal 28/Sep/2018
                //{
                //    string _error;

                //    update_PC_List_RPTDB();

                //    BaseCls.GlbReportHeading = "Reject Account Details";

                //    string _filePath = CHNLSVC.MsgPortal.Get_rejectAccBalance(BaseCls.GlbUserComCode, "", BaseCls.GlbReportAsAtDate, BaseCls.GlbUserID, out _error);

                //    if (!string.IsNullOrEmpty(_error))
                //    {
                //        btnDisplay.Enabled = true;
                //        Cursor.Current = Cursors.Default;
                //        MessageBox.Show(_error);
                //        return;
                //    }

                //    if (string.IsNullOrEmpty(_filePath))
                //    {
                //        btnDisplay.Enabled = true;
                //        Cursor.Current = Cursors.Default;
                //        MessageBox.Show("The excel file path cannot identify. Please contact IT Dept", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        return;
                //    }

                //    Process p = new Process();
                //    p.StartInfo = new ProcessStartInfo(_filePath);
                //    p.Start();

                //    MessageBox.Show("Export Completed", "Service Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}

                btnDisplay.Enabled = true;

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDisplay.Enabled = true;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
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
                //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPH"))
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10043))
                {
                    DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(com, chanel, subChanel, area, region, zone, pc);
                    foreach (DataRow drow in dt.Rows)
                    {
                        MasterLocation _loc = CHNLSVC.General.GetAllLocationByLocCode(txtComp.Text.Trim().ToUpper(), drow["PROFIT_CENTER"].ToString(), 1);
                        if (_loc != null)
                        {
                            if (_loc.Ml_anal1 == "SCM2")
                            {
                                lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            continue;
                        }

                    }
                }
                else
                {
                    DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep(BaseCls.GlbUserID, com, chanel, subChanel, area, region, zone, pc);
                    foreach (DataRow drow in dt.Rows)
                    {
                        MasterLocation _loc = CHNLSVC.General.GetAllLocationByLocCode(txtComp.Text.Trim().ToUpper(), drow["PROFIT_CENTER"].ToString(), 1);
                        if (_loc != null)
                        {
                            if (_loc.Ml_anal1 == "SCM2")
                            {
                                lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            continue;
                        }
                        //lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                    }
                }

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
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
            try
            {
                if (opt1.Checked == true)
                {
                    // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP1"))
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5001))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5001)");
                        //opt1.Checked = false;
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(1);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        //private void opt2_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (opt2.Checked == true)
        //    {
        //        // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP2"))
        //        if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5002))
        //        {
        //            MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5002)");
        //            //opt1.Checked = false;
        //            RadioButton RDO = (RadioButton)sender;
        //            RDO.Checked = false;
        //            return;
        //        }
        //        setFormControls(2);
        //    }
        //}
        private void opt3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt3.Checked == true)
                {
                    if (BaseCls.GlbUserID != "ADMIN")
                    {

                        //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP3"))
                        if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5003))
                        {
                            MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5003)");
                            //opt1.Checked = false;
                            RadioButton RDO = (RadioButton)sender;
                            RDO.Checked = false;
                            return;
                        }
                    }
                    setFormControls(3);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void opt4_CheckedChanged(object sender, EventArgs e)
        {
            //if (opt4.Checked == true)
            //{
            //   // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP4"))
            //   if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID,5004))
            //  {
            //    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5004)");
            //    //opt1.Checked = false;
            //    RadioButton RDO = (RadioButton)sender;
            //    RDO.Checked = false;
            //    return;
            //  }
            //    setFormControls(4);
            //}
        }

        private void opt5_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt5.Checked == true)
                {
                    // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP5"))
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5005))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5005)");
                        //opt1.Checked = false;
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }

                    //cmbGroupBy.Items.Add("Default");
                    //cmbGroupBy.Items.Add("Scheme Type");
                    //cmbGroupBy.Items.Add("Scheme");
                    //cmbGroupBy.Items.Add("Location");
                    //cmbGroupBy.Items.Add("Channel Wise Summary");
                    //cmbGroupBy.SelectedIndex = 0;
                    setFormControls(5);
                }
                else
                {
                    cmbGroupBy.Items.Clear();
                    pnlDateRange.Enabled = true;
                    pnlAsAtDate.Enabled = false;
                    pnlHP.Enabled = false;
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #endregion
        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt8_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt8.Checked == true)
                {
                    //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP8"))
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5008))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5008)");
                        //opt1.Checked = false;
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(8);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt6_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt6.Checked == true)
                {
                    // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP6"))
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5006))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5006)");
                        //opt1.Checked = false;
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt7_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt7.Checked == true)
                {
                    // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP7"))
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5007))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5007)");
                        //opt1.Checked = false;
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void opt13_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt13.Checked == true)
                {
                    //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP13"))
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5013))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5013)");
                        //opt1.Checked = false;
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(13);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt14_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt14.Checked == true)
                {
                    // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP14"))
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5014))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5014)");
                        //opt1.Checked = false;
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(14);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt15_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt15.Checked == true)
                {
                    //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP15"))
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5015))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5015)");
                        //opt1.Checked = false;
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(15);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt16_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt16.Checked == true)
                {
                    // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP16"))
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5016))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5016)");
                        //opt1.Checked = false;
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(16);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt17_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt17.Checked == true)
                {
                    // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP17"))
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5017))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5017)");
                        //opt1.Checked = false;
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(17);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt18_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt18.Checked == true)
                {
                    // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP18"))
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5018))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5018)");
                        //opt1.Checked = false;
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(18);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt10_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt10.Checked == true)
                {
                    // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP18"))
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5010))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5010)");
                        //opt1.Checked = false;
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(10);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt11_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt11.Checked == true)
                {
                    // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP18"))
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5011))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5011)");
                        //opt1.Checked = false;
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt12_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt12.Checked == true)
                {
                    // if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP18"))
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5012))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5012)");
                        //opt1.Checked = false;
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(12);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
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
                case CommonUIDefiniton.SearchUserControlType.InvoiceExecutive:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.Scheme:
                    {
                        paramsText.Append(txtScheme.Text.Trim() + seperator);
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

        private void chkAllScheme_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllScheme.Checked == true)
            {
                txtScheme.Text = "";
                txtScheme.Enabled = false;
                btn_Srch_Sch.Enabled = false;
            }
            else
            {
                txtScheme.Enabled = true;
                btn_Srch_Sch.Enabled = true;
            }
        }

        private void btn_Srch_Cat1_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btn_Srch_Cat2_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btn_Srch_Cat3_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
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
            try
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
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
            try
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
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
            try
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtModel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Model_Click(null, null);
            }
        }

        private void btn_Srch_Sch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Scheme);
                DataTable _result = CHNLSVC.CommonSearch.Get_SchemesCD_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtScheme;
                _CommonSearch.ShowDialog();
                txtScheme.Select();
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtScheme_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Sch_Click(null, null);
            }
        }

        private void btn_Srch_Doc_Tp_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btn_Srch_DocSubTp_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
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

        private void opt19_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt19.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5019))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5019)");
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(19);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt20_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt20.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5020))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5020)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(20);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
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

        private void opt21_CheckedChanged(object sender, EventArgs e)
        {
            //if (opt21.Checked == true)
            //{

            //    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5021))
            //    {
            //        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5021)");

            //        RadioButton RDO = (RadioButton)sender;
            //        RDO.Checked = false;
            //        return;
            //    }
            //    setFormControls(21);
            //}
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


        private void opt22_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt22.Checked == true)
                {
                    //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP15"))
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5022))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5022)");
                        //opt1.Checked = false;
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(22);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt21_CheckedChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (opt21.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5021))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5021)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(21);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt23_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt23.Checked == true)
                {
                    //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "RHP15"))
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5023))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5023)");
                        //opt1.Checked = false;
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(23);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        private void txtChanel_DoubleClick(object sender, EventArgs e)
        {
            try
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtSChanel_DoubleClick(object sender, EventArgs e)
        {
            try
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtArea_DoubleClick(object sender, EventArgs e)
        {
            try
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtRegion_DoubleClick(object sender, EventArgs e)
        {
            try
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtZone_DoubleClick(object sender, EventArgs e)
        {
            try
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtPC_DoubleClick(object sender, EventArgs e)
        {
            try
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtModel_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Model_Click(null, null);
        }

        private void txtBrand_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Brnd_Click(null, null);
        }

        private void txtItemCode_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Itm_Click(null, null);
        }


        private void txtIcat1_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Cat1_Click(null, null);
        }

        private void txtIcat3_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Cat3_Click(null, null);
        }

        private void txtIcat2_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Cat2_Click(null, null);
        }

        private void opt24_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt24.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5024))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5024)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(24);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt25_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt25.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5025))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5025)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(25);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void update_PC_List_RPTDB()
        {
            try
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtAccBal_TextChanged(object sender, EventArgs e)
        {

        }

        private void opt27_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt27.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5027))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5027)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(27);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt28_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt28.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5028))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5028)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(28);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt29_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt29.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5029))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5029)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(29);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt30_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt30.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5030))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5030)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(30);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt31_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt31.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5031))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5031)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(31);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void fldgOpenPath_FileOk(object sender, CancelEventArgs e)
        {
            BaseCls.GlbReportFilePath = fldgOpenPath.FileName;

        }

        private void opt32_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt32.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5032))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5032)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(32);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt33_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt33.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5033))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5033)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(33);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void opt34_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt34.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5034))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5034)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(34);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt35_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt35.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5035))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5035)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(35);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt36_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt36.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5036))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5036)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(36);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void LoadPromotor()
        {
            cmbTechnician.DataSource = null;
            txtPromotor.Text = "";
            DataTable _tblPromotor = CHNLSVC.General.GetProfitCenterAllocatedPromotors(BaseCls.GlbUserComCode, null);

            if (_tblPromotor != null)
            {
                AutoCompleteStringCollection _string0 = new AutoCompleteStringCollection();
                var _lst0 = _tblPromotor.AsEnumerable().ToList();
                cmbTechnician.ValueMember = "mpp_promo_cd";
                cmbTechnician.DisplayMember = "mpp_promo_name";
                if (_lst0 != null && _lst0.Count > 0) cmbTechnician.DataSource = _lst0.CopyToDataTable();
                cmbTechnician.DropDownWidth = 200; if (_lst0 != null && _lst0.Count > 0)
                { Parallel.ForEach(_lst0, x => _string0.Add(x.Field<string>("mpp_promo_name"))); cmbTechnician.AutoCompleteSource = AutoCompleteSource.CustomSource; cmbTechnician.AutoCompleteMode = AutoCompleteMode.SuggestAppend; cmbTechnician.AutoCompleteCustomSource = _string0; }

                cmbTechnician.SelectedIndex = -1;
                //cmbTechnician.ValueMember = "mpp_promo_cd"; cmbTechnician.DisplayMember = "mpp_promo_name";
                //cmbExecutive.DataSource = _tblPromotor; cmbExecutive.DropDownWidth = 200;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btn_Srch_Dir_Click(object sender, EventArgs e)
        {

        }

        private void opt37_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt37.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5037))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5037)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(37);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt38_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt38.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5038))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5038)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(38);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt39_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt39.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5039))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5039)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(39);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt40_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt40.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5040))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5040)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(40);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt41_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt41.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5041))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5041)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(41);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtScheme.Text))
            {
                for (int i = 0; i < lstSch.Items.Count; i++)
                {
                    if (lstSch.Items[i].Text == txtScheme.Text)
                        return;
                }
                lstSch.Items.Add((txtScheme.Text).ToString());
                txtScheme.Text = "";
            }
        }

        private void lstSch_DoubleClick(object sender, EventArgs e)
        {
            for (int i = 0; i < lstSch.Items.Count; i++)
            {
                if (lstSch.Items[i].Selected)
                {
                    lstSch.Items[i].Remove();
                    i--;
                }
            }
        }

        private void chkAccountNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAccountNo.Checked == true)
            {
                txtAccountNo.Text = "";
                txtAccountNo.Enabled = false;
                btnAccountNo.Enabled = false;
            }
            else
            {
                txtAccountNo.Enabled = true;
                btnAccountNo.Enabled = true;
            }
        }

        private void opt42_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt42.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5042))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5042)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(42);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void optLs_CheckedChanged(object sender, EventArgs e)
        {
            if (optLs.Checked == true)
            {
                optEq.Checked = false;
                optGt.Checked = false;
                txtInstall.Enabled = true;
            }
        }

        private void optAll_CheckedChanged(object sender, EventArgs e)
        {
            if (optAll.Checked == true)
            {
                txtInstall.Text = "";
                txtInstall.Enabled = false;
            }
        }

        private void optEq_CheckedChanged(object sender, EventArgs e)
        {
            if (optEq.Checked == true)
            {
                optLs.Checked = false;
                optGt.Checked = false;
                txtInstall.Enabled = true;
            }
        }

        private void optGt_CheckedChanged(object sender, EventArgs e)
        {
            if (optGt.Checked == true)
            {
                optLs.Checked = false;
                optEq.Checked = false;
                txtInstall.Enabled = true;
            }
        }

        private void opt43_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt43.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5043))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5043)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(43);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt44_CheckedChanged(object sender, EventArgs e)
        {
            if (opt44.Checked == true)
            {
                setFormControls(44);
            }
        }

        private void opt45_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt45.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5045))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5045)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(45);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt46_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt46.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5046))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5046)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(46);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt47_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt47.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5047))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5047)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(47);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt48_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt48.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5048))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5048)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(48);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt49_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt49.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5049))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5049)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(49);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt50_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt50.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5050))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5050)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(50);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt51_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt51.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5051))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5051)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(51);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt52_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt52.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5052))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5052)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(52);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt53_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt53.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5053))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5053)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(53);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
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

        private void opt54_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt54.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5054))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5054)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(54);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
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

        private void txt_user_DoubleClick(object sender, EventArgs e)
        {
            btn_user_Click(null, null);
        }

        private void txt_user_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_user_Click(null, null);
        }

        private void opt55_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt55.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5055))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5055)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(55);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt56_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt56.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5056))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5056)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(56);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void chk_Pay_Tp_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Pay_Tp.Checked == true)
            {
                cmbPayModes.SelectedIndex = -1;
                cmbPayModes.Enabled = false;

            }
            else
            {
                cmbPayModes.Enabled = true;

            }
        }

        private void chkMan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMan.Checked == true)
            {
                txtMan.Text = "";
                txtMan.Enabled = false;
                btn_srch_man.Enabled = false;
            }
            else
            {
                txtMan.Enabled = true;
                btn_srch_man.Enabled = true;
            }
        }

        private void btn_srch_man_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceExecutive);
                DataTable _result = CHNLSVC.CommonSearch.SearchEmployeeAssignToProfitCenter(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtMan;
                _CommonSearch.txtSearchbyword.Text = txtMan.Text;
                _CommonSearch.ShowDialog();
                txtMan.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMan_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_man_Click(null, null);
        }

        private void opt_dtl_CheckedChanged(object sender, EventArgs e)
        {
            if (opt_dtl.Checked)
            {
                opt_sum.Checked = false;
                opt_dtl.Checked = true;
            }
            else
            {
                opt_sum.Checked = true;
                opt_dtl.Checked = false;
            }
        }

        private void opt_sum_CheckedChanged(object sender, EventArgs e)
        {
            if (opt_sum.Checked)
            {
                opt_sum.Checked = true;
                opt_dtl.Checked = false;
            }
            else
            {
                opt_sum.Checked = false;
                opt_dtl.Checked = true;
            }
        }

        private void opt57_CheckedChanged(object sender, EventArgs e)
        {
            setFormControls(57);
        }

        private void opt58_CheckedChanged(object sender, EventArgs e)
        {
            setFormControls(58);
        }

        private void opt59_CheckedChanged(object sender, EventArgs e)
        {
            setFormControls(59);
        }

        private void opt60_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void opt61_CheckedChanged(object sender, EventArgs e)
        {
            setFormControls(61);
        }

        private void opt62_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt62.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5032))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5032)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(62);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt63_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt63.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5024))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5024)");

                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(63);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt64_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt64.Checked == true)
                {
                    //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 5024))
                    //{
                    //    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :5024)");

                    //    RadioButton RDO = (RadioButton)sender;
                    //    RDO.Checked = false;
                    //    return;
                    //}
                    setFormControls(63);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "HP Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }



    }
}


