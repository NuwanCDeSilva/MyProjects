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
using FF.WindowsERPClient.Reports.Finance;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms.Internal.Win32;
using Excel = Microsoft.Office.Interop.Excel;

//Written By kapila on 26/12/2012
namespace FF.WindowsERPClient.Reports.Finance
{
    public partial class Financial_Rep : Base
    {
        clsFinanceRep objFin = new clsFinanceRep();

        public Financial_Rep()
        {
            try
            {
                InitializeComponent();
                InitializeEnv();
                GetCompanyDet(null, null);
                GetPCDet(null, null);
                setFormControls(0);
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void setFormControls(Int32 _index)
        {
            try
            {
                pnlDebt.Enabled = false;
                pnlOuts.Enabled = false;
                pnlAsAtDate.Enabled = false;
                pnl_Direc.Enabled = false;
                pnl_DocNo.Enabled = false;
                pnl_DocSubType.Enabled = false;
                pnl_DocType.Enabled = false;
                pnl_Entry_Tp.Enabled = false;
                pnl_Rec_Tp.Enabled = false;
                pnl_Item.Enabled = false;
                pnlItem.Enabled = false;
                pnlExec.Enabled = false;
                pnl_Circ.Enabled = false;
                pnlInc.Enabled = false;
                pnlSRWise.Enabled = false;
                pnlDate.Enabled = false;
                pnl_claimstus.Visible = false;
                pnl_claimstus.Enabled = false;
                pnlLoc.Enabled = true;
                lstPC.Enabled = true; // Added by Chathura on 21-oct-2017
                pnlAddress.Enabled = false;
                pnlAddress.Visible = false;
                //pnlAdjType.Enabled = false;
                //pnlAdjType.Visible = false;

                label16.Text = "Direction";
                OptDet.Text = "Detail Report";
                OptSumm.Text = "Summary Report";

                cmbPayModes.DataSource = CHNLSVC.Sales.GetAllPaymentType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, null);
                cmbPayModes.DisplayMember = "SAPT_DESC";
                cmbPayModes.ValueMember = "SAPT_CD";
                cmbPayModes.SelectedIndex = -1;          


                pnlDateRange.Enabled = true;
                txtFromDate.Enabled = true;
                txtToDate.Enabled = true;
                pnlPayType.Enabled = false;
                chkGroupPC.Enabled = true;
                cmbScheme.Enabled = true;

                txtFromDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                label22.Text = "Entry Type";
                lblAsAt.Text = "As At Date";

                pnl_PB.Visible = false;
                pnl_PBLevel.Visible = false;
                pnlAccouns.Visible = false;
                pnlCredNote.Visible = false;
                pnlCredNote.Enabled = false;
                chkallAccounts.Enabled = true;

                switch (_index)
                {
                    case 1:
                        {
                            pnlPayType.Enabled = true;
                            cmbPayModes.Enabled = true;
                            break;
                        }
                    case 3:
                        {
                            pnlAsAtDate.Enabled = true;
                            pnlDateRange.Enabled = false;
                            pnlDate.Enabled = true;
                            break;
                        }
                    case 6:
                        {
                            txtFromDate.Enabled = false;
                            txtToDate.Enabled = false;
                            cmbMonth_SelectedIndexChanged(null, null);
                            break;
                        }
                    case 7:
                        {
                            txtFromDate.Enabled = true;
                            txtToDate.Enabled = true;
                            cmbMonth_SelectedIndexChanged(null, null);
                            break;
                        }
                    case 8:
                        {
                            //cmbMonth_SelectedIndexChanged(null, null);
                            break;
                        }
                    case 5:
                        {
                            pnlAsAtDate.Enabled = false;
                            pnlDateRange.Enabled = true;
                            pnlRepSelect.Enabled = true;
                            //pnlInc.Enabled = true;
                            //pnlAddress.Visible = true;
                            //pnlAddress.Enabled = true;
                            break;
                        }
                    case 9:
                        {
                            pnlDateRange.Enabled = true;
                            txtFromDate.Enabled = false;
                            txtToDate.Enabled = false;
                            pnl_DocNo.Enabled = true;
                            label18.Text = "Circular No";
                            break;
                        }
                    case 10:
                        {
                            pnlDateRange.Enabled = true;
                            txtFromDate.Enabled = true;
                            txtToDate.Enabled = true;
                            pnl_DocNo.Enabled = true;
                            pnlRepSelect.Enabled = true;
                            label18.Text = "Circular No";
                            break;
                        }
                    case 11:
                        {
                            pnlDateRange.Enabled = true;
                            txtFromDate.Enabled = false;
                            txtToDate.Enabled = false;
                            pnl_DocNo.Enabled = true;
                            label18.Text = "Circular No";
                            break;
                        }
                    case 12:
                        {
                            pnlExec.Enabled = true;
                            cmbMonth_SelectedIndexChanged(null, null);
                            break;
                        }
                    case 14:
                        {
                            pnlAsAtDate.Enabled = true;
                            pnlDateRange.Enabled = false;
                            break;
                        }
                    case 15:
                        {
                            pnlDateRange.Enabled = false;
                            pnl_Circ.Enabled = true;
                            break;
                        }
                    case 16:
                        {
                            pnlAsAtDate.Enabled = true;
                            pnlDateRange.Enabled = false;

                            break;
                        }
                    case 17:
                        {
                            pnlAsAtDate.Enabled = false;
                            pnlDateRange.Enabled = true;

                            break;
                        }
                    case 18:
                        {
                            DropDownListRemitType.Enabled = true;
                            DropDownListRemitType.Visible = true;
                            pnlAsAtDate.Enabled = false;
                            pnlDateRange.Enabled = true;
                            LoadRemitance();
                            DropDownListRemitType.Enabled = true;
                            label22.Text = "Expence Code";
                            pnl_Entry_Tp.Enabled = true;
                            break;
                        }

                    case 20:
                        {
                            pnlAsAtDate.Enabled = false;
                            pnlDateRange.Enabled = true;
                            break;
                        }
                    case 21:
                        {
                            pnlAsAtDate.Enabled = false;
                            pnlDateRange.Enabled = false;
                            pnlInc.Enabled = true;
                            pnlSRWise.Enabled = true;
                            break;
                        }
                    case 22:
                        {
                            pnlAsAtDate.Enabled = false;
                            pnlDateRange.Enabled = false;
                            pnlInc.Enabled = true;
                            break;
                        }
                    case 23:
                        {
                            pnlAsAtDate.Enabled = false;
                            pnlDateRange.Enabled = false;
                            pnlInc.Enabled = true;
                            pnlSRWise.Enabled = true;
                            break;
                        }
                    case 24:
                        {
                            pnlAsAtDate.Enabled = false;
                            pnlDateRange.Enabled = true;
                            break;
                        }
                    case 25:
                        {
                            pnlAsAtDate.Enabled = false;
                            pnlDateRange.Enabled = true;
                            pnl_Direc.Enabled = true;
                            label16.Text = "Week";
                            txtFromDate.Enabled = false;
                            txtToDate.Enabled = false;
                            break;
                        }
                    case 26:
                        {
                            pnlAsAtDate.Enabled = true;
                            pnlDateRange.Enabled = false;
                            pnlDate.Enabled = true;
                            break;
                        }
                    case 27:
                        {
                            pnlAsAtDate.Enabled = false;
                            pnlDateRange.Enabled = true;
                            break;
                        }
                    case 28:
                        {
                            pnlAsAtDate.Enabled = false;
                            pnlDateRange.Enabled = true;
                            pnlExec.Enabled = true;
                            pnlRepSelect.Enabled = true;
                            OptDet.Text = "Issues";
                            OptSumm.Text = "Claims";
                            optUnclaim.Text = "Unclaim";
                            optnotLeg.Text = "Not in Ledger";
                            optCancel.Text = "Cancel";
                            break;
                        }
                    case 30:
                        {
                            pnlAsAtDate.Enabled = false;
                            pnlDateRange.Enabled = true;
                            break;
                        }
                    case 31:
                        {
                            txtFromDate.Enabled = false;
                            txtToDate.Enabled = false;
                            cmbMonth_SelectedIndexChanged(null, null);
                            break;
                        }
                    case 32:
                        {
                            pnlAsAtDate.Enabled = false;
                            pnlDateRange.Enabled = true;
                            txtFromDate.Enabled = true;
                            txtToDate.Enabled = true;
                            cmbMonth_SelectedIndexChanged(null, null);
                            break;
                        }
                    case 33:
                        {
                            pnlAsAtDate.Enabled = false;
                            pnlDateRange.Enabled = true;
                            break;
                        }
                    case 34:
                        {
                            pnlAsAtDate.Enabled = false;
                            pnlDateRange.Enabled = true;
                            pnl_claimstus.Visible = true;
                            pnl_claimstus.Enabled = true;
                            break;
                        }
                    case 35:
                        {
                            pnlLoc.Enabled = false;
                            pnlAsAtDate.Enabled = true;
                            pnlAccouns.Enabled = true;
                            pnlAccouns.Visible = true;
                            cmbaccounts.Enabled = true;
                            chkallAccounts.Enabled = false;
                            //cmbUsers.Enabled = false;
                            txtFromDate.Enabled = false;
                            txtToDate.Enabled = false;
                            cmbYear.Enabled = false;
                            cmbMonth.Enabled = false;
                            bind_Combo_Accounts();
                            break;
                        }
                    case 36:
                        {
                            pnlAsAtDate.Enabled = false;
                            pnlDateRange.Enabled = true;
                            //pnlInc.Enabled = true;
                            pnlLoc.Enabled = false;
                            cmbaccounts.Enabled = true;
                            pnlAccouns.Enabled = true;
                            pnlAccouns.Visible = true;
                            bind_Combo_Accounts();
                            label32.Text = "Account";
                            chkallAccounts.Enabled = false;
                            break;
                        }
                    case 37:
                        {
                            pnlAsAtDate.Enabled = true;
                            pnlDateRange.Enabled = false;
                            //pnlInc.Enabled = true;
                            label32.Text = "Account";
                            chkGroupPC.Enabled = false;
                            cmbScheme.Enabled = false;
                            cmbaccounts.Enabled = true;
                            pnlAccouns.Enabled = true;
                            pnlAccouns.Visible = true;
                            bind_Combo_Accounts();
                            lblAsAt.Text = "Date";
                            chkallAccounts.Enabled = false;
                            break;
                        }

                    case 38:
                        {
                            pnlDateRange.Enabled = false;
                            pnlAsAtDate.Enabled = true;
                            lblAsAt.Text = "Date";
                            cmbaccounts.Enabled = true;
                            pnlAccouns.Enabled = true;
                            pnlAccouns.Visible = true;
                            bind_Combo_Accounts();
                            chkallAccounts.Enabled = true;
                            pnlDate.Enabled = true;
                            break;
                        }
                    case 39:
                        {
                            pnlDateRange.Enabled = true;
                            txtFromDate.Enabled = true;
                            txtToDate.Enabled = true;
                            //pnlAccouns.Enabled = true;
                            pnlAccouns.Enabled = true;
                            pnlAccouns.Visible = true;
                            bind_Combo_Accounts();
                            //bind_Combo_Users();
                            cmbaccounts.Enabled = true;
                            chkallAccounts.Enabled = true;
                            pnlLoc.Enabled = false;
                            break;
                        }
                    case 40:
                        {
                            txtFromDate.Enabled = false;
                            txtToDate.Enabled = false;
                            cmbMonth_SelectedIndexChanged(null, null);
                            break;
                        }
                    case 41:
                        {
                            cmbAdjType.DataSource = CHNLSVC.Sales.GetAdjType();                          
                            cmbAdjType.SelectedIndex = -1;

                            txtFromDate.Enabled = true;
                            txtToDate.Enabled = true;
                            cmbMonth_SelectedIndexChanged(null, null);
                            cmbaccounts.Enabled = true;
                            pnlAccouns.Enabled = true;
                            pnlAccouns.Visible = true;
                           // pnlPayType.Enabled = true;
                            pnlAdjType.Enabled = true;
                            pnlAdjType.Visible = true;
                            bind_Combo_Accounts();
                            break;
                        }
                    case 42:
                        {
                            txtFromDate.Enabled = true;
                            txtToDate.Enabled = true;
                            pnl_PB.Visible = true;
                            pnl_PBLevel.Visible = true;
                            pnl_PB.Enabled = true;
                            pnl_PBLevel.Enabled = true;
                            pnlExec.Visible = false;
                            pnl_Circ.Visible = false;
                            break;
                        }
                    case 43:
                        {
                            txtFromDate.Enabled = true;
                            txtToDate.Enabled = true;
                            break;
                        }
                    case 44:
                        {
                            txtFromDate.Enabled = true;
                            txtToDate.Enabled = true;
                            pnlCredNote.Visible = true;
                            pnlCredNote.Enabled = true;
                            break;
                        }
                    case 46: // Added by Chathura on 20-oct-2017
                        {
                            pnlLoc.Enabled = false;
                            pnlAsAtDate.Enabled = true;
                            pnlAccouns.Enabled = true;
                            pnlAccouns.Visible = true;
                            pnlAccouns.Enabled = true;
                            cmbaccounts.Enabled = true;
                            chkallAccounts.Enabled = true;
                            //cmbUsers.Enabled = false;
                            txtFromDate.Enabled = false;
                            txtToDate.Enabled = false;
                            cmbYear.Enabled = false;
                            cmbMonth.Enabled = false;
                            bind_Combo_Accounts();
                            break;
                        }
                    case 47: // Added by Chathura on 20-oct-2017
                        {
                            pnlLoc.Enabled = true;
                            lstPC.Enabled = false;
                            pnlAsAtDate.Enabled = false;
                            pnlAccouns.Enabled = true;
                            pnlAccouns.Visible = true;
                            //pnlAccouns.Enabled = true;
                            cmbaccounts.Enabled = true;
                            chkallAccounts.Enabled = true;
                            //cmbUsers.Enabled = false;
                            txtFromDate.Enabled = true;
                            txtToDate.Enabled = true;
                            cmbYear.Enabled = false;
                            cmbMonth.Enabled = false;
                            bind_Combo_Accounts();
                            break;
                        }
                    case 48: // Added by Chathura on 24-oct-2017
                        {
                            pnlLoc.Enabled = true;
                            lstPC.Enabled = true;
                            pnlAsAtDate.Enabled = false;
                            //pnlAccouns.Enabled = true;
                            //pnlAccouns.Visible = false;
                            //pnlAccouns.Enabled = true;
                            //cmbaccounts.Enabled = false;
                            chkallAccounts.Enabled = false;
                            //cmbUsers.Enabled = false;
                            txtFromDate.Enabled = true;
                            txtToDate.Enabled = true;
                            cmbYear.Enabled = false;
                            cmbMonth.Enabled = false;
                            //bind_Combo_Accounts();
                            pnlRepSelect.Enabled = true;
                            OptDet.Enabled = true;
                            OptSumm.Enabled = true;
                            break;
                        }
                    case 49: // Added by Chathura on 24-oct-2017
                        {
                            
                            pnlAsAtDate.Enabled = false;
                            pnlDateRange.Enabled = true;
                            txtFromDate.Enabled = true;
                            txtToDate.Enabled = true;
                            cmbYear.Enabled = true;
                            cmbMonth.Enabled = true;
                            break;
                        }
                    case 50:
                        {
                            pnlExec.Enabled = true;
                            pnlAsAtDate.Enabled = false;
                            pnlDateRange.Enabled = true;
                            pnlRepSelect.Enabled = true;
                            pnlInc.Enabled = true;
                            pnlAddress.Visible = true;
                            pnlAddress.Enabled = true;
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
                            break;
                        }
                    case 54: // Wimal 01/Nov/2018
                        {
                            pnlLoc.Enabled = false;
                            pnlAsAtDate.Enabled = false ;
                            pnlAccouns.Enabled = true;
                            pnlAccouns.Visible = true;
                            pnlAccouns.Enabled = true;
                            cmbaccounts.Enabled = true;
                            chkallAccounts.Enabled = true;
                            pnlDateRange.Enabled = true;
                            chkallAccounts.Enabled = false ;
                            //cmbUsers.Enabled = false;
                            //txtFromDate.Enabled = false;
                            //txtToDate.Enabled = false;
                            //cmbYear.Enabled = false;
                            //cmbMonth.Enabled = false;
                            bind_Combo_CC_Accounts();
                            break;
                        }
                    case 55:
                        {
                            pnlAsAtDate.Enabled = false;
                            pnlDateRange.Enabled = true;
                            break;
                        }
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        //private void bind_Combo_Users()
        //{
        //    DataTable dtusers = CHNLSVC.Sales.LoadUsers();
        //    if (dtusers.Rows.Count > 0)
        //    {

        //        cmbUsers.DataSource = dtusers;
        //        cmbUsers.DisplayMember = "SE_USR_ID";
        //        cmbUsers.ValueMember = "SE_USR_ID";
        //        cmbUsers.SelectedIndex = -1;
        //     }
        //    else
        //    {
        //        cmbUsers.DataSource = null;
        //    }
        //}

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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void InitializeEnv()
        {
            try
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
                BaseCls.GlbReportYear = Convert.ToInt32(cmbYear.Text);

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
                BaseCls.GlbReportMonth = DateTime.Now.Month;

                txtFromDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
                txtAsAtDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");

                BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Value).Date;
                BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Value).Date;
                BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Value).Date;

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void update_PC_List()
        {
            try
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

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
                    //if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPF"))
                    //Add by Chamal 30-Aug-2013
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10046))
                    {
                        Int16 is_Access = CHNLSVC.Security.Check_User_PC(BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtPC.Text);
                        if (is_Access != 1)
                        {
                            //MessageBox.Show("Access Denied.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MessageBox.Show("Sorry, You have no permission for view reports!\n( Advice: Required permission code :10046)", "Financial Reports", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }

                btnDisplay.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                //BaseCls.GlbReportUser = cmbUsers.Text;
                BaseCls.GlbReportDocType = cmbaccounts.Text;
                BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text);
                BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Text);
                BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Text);

                BaseCls.GlbReportName = string.Empty;
                GlbReportName = string.Empty;

                if (opt1.Checked == true)
                {   //update temporary table
                    update_PC_List();
                    //04-03-13 Nadeeka
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportComp = txtComp.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;

                    if (cmbPayModes.SelectedValue != null)
                    {
                        BaseCls.GlbPayType = cmbPayModes.SelectedValue.ToString();
                    }
                    else
                    {
                        BaseCls.GlbPayType = null;
                    }

                    BaseCls.GlbReportName = "PersonalChequeStatement.rpt";
                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    _view.Show();
                    _view = null;
                }

                if (opt2.Checked == true) //AD Loan Settlement
                {   //update temporary table
                    update_PC_List();
                    //05-04-2013 Sanjeewa

                    BaseCls.GlbReportHeading = "AD LOAN SETTLEMENT REPORT";

                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "AD_Loan_Settlement.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt3.Checked == true) //short remitance statement
                {   //update temporary table
                    update_PC_List_RPTDB();
                    //08/4/2013 kapila
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;

                    BaseCls.GlbReportIsAsAt = Convert.ToInt16(optAsAt.Checked);

                    BaseCls.GlbReportHeading = "Short Remittance Statement";

                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "short_rem_statement.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt4.Checked == true) //Claim Expenses Voucher Statement
                {   //update temporary table
                    update_PC_List();
                    //08-04-2013 Sanjeewa


                    BaseCls.GlbReportHeading = "CLAIM EXPENSES VOUCHER STATEMENT";

                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "Claim_Expenses_Voucher.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt5.Checked == true)   // Manager Commission
                {
                    //update temporary table
                    update_PC_List();
                    BaseCls.GlbReportComp = txtComp.Text;
                    Reports.Finance.clsFinanceRep _clsfinance = new Reports.Finance.clsFinanceRep();


                    if (OptDet.Checked == true)
                    {
                        BaseCls.GlbReportHeading = "Manager Commission";
                        //_clsfinance.ProcessManagerReport(0);
                        _clsfinance.ManagerCommControl(0);
                    }
                    else
                    {
                        BaseCls.GlbReportHeading = "Manager Commission(Summary)";
                        //_clsfinance.ProcessManagerReport(1);
                        _clsfinance.ManagerCommControl(1);
                    }
                    MasterCompany _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
                    string _path = _MasterComp.Mc_anal6;
                    _clsfinance._managerComm.ExportToDisk(ExportFormatType.Excel, _path + "ManagerCommission" + BaseCls.GlbUserID + ".xls");

                    Excel.Application excelApp = new Excel.Application();
                    excelApp.Visible = true;
                    string workbookPath = _path + "ManagerCommission" + BaseCls.GlbUserID + ".xls";
                    Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath,
                            0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                            true, false, 0, true, false, false);

                }
                if (opt6.Checked == true)
                {
                    if (MessageBox.Show("Do you want cash control reconcilation?", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        clsFinanceRep objHp = new clsFinanceRep();

                        BaseCls.GlbReportProfit = txtPC.Text;
                        BaseCls.GlbReportCompCode = txtComp.Text;
                        BaseCls.GlbReportComp = txtCompDesc.Text;
                        BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                        BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text);
                        BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Text);
                        BaseCls.GlbReportName = "CashControl.rpt";
                        BaseCls.GlbReportStrStatus = "F";       //kapila 11/4/2017
                        objHp.CashControl();

                        MasterCompany _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
                        string _path = _MasterComp.Mc_anal6;
                        objHp._cashControl.ExportToDisk(ExportFormatType.Excel, _path + "CashControl" + BaseCls.GlbUserID + ".xls");

                        Excel.Application excelApp = new Excel.Application();
                        excelApp.Visible = true;
                        string workbookPath = _path + "CashControl" + BaseCls.GlbUserID + ".xls";
                        Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath,
                                0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                                true, false, 0, true, false, false);
                    }
                    else
                    {
                        update_PC_List();

                        clsFinanceRep objFin = new clsFinanceRep();

                        BaseCls.GlbReportProfit = txtPC.Text;
                        BaseCls.GlbReportCompCode = txtComp.Text;
                        BaseCls.GlbReportComp = txtCompDesc.Text;
                        BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                        BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text);
                        BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Text);
                        BaseCls.GlbReportName = "CashControlRecon.rpt";
                        objFin.CashControlRecon();

                        MasterCompany _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
                        string _path = _MasterComp.Mc_anal6;
                        objFin._cashControlRecon.ExportToDisk(ExportFormatType.Excel, _path + "CashControlRecon" + BaseCls.GlbUserID + ".xls");

                        Excel.Application excelApp = new Excel.Application();
                        excelApp.Visible = true;
                        string workbookPath = _path + "CashControlRecon" + BaseCls.GlbUserID + ".xls";
                        Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath,
                                0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                                true, false, 0, true, false, false);
                    }

                }
                if (opt7.Checked == true)  //remitance check list
                {
                    update_PC_List_RPTDB();
                    clsFinanceRep objHp = new clsFinanceRep();

                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text);
                    BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Text);
                    BaseCls.GlbReportName = "RemitanceCheckList.rpt";
                    objHp.RemitanceCheckList();

                    MasterCompany _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
                    string _path = _MasterComp.Mc_anal6;

                    objHp._remitCheckList.ExportToDisk(ExportFormatType.Excel, _path + "RemitanceCheckList" + BaseCls.GlbUserID + ".xls");

                    Excel.Application excelApp = new Excel.Application();
                    excelApp.Visible = true;
                    string workbookPath = _path + "RemitanceCheckList" + BaseCls.GlbUserID + ".xls";
                    Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath,
                            0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                            true, false, 0, true, false, false);

                    //BaseCls.GlbReportProfit = txtPC.Text;
                    //BaseCls.GlbReportCompCode = txtComp.Text;
                    //BaseCls.GlbReportComp = txtCompDesc.Text;
                    //BaseCls.GlbReportCompAddr = txtCompAddr.Text;

                    //BaseCls.GlbReportName = "RemitanceCheckList.rpt";
                    //Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    //_view.Show();
                    //_view = null;
                }

                if (opt8.Checked == true) //daily terminal
                {
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = "DAILY TERMINAL REPORT";
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportOpBal = 0;  // Convert.ToDecimal(txtOpenBal.Text);
                    BaseCls.GlbReportCloseBal = 0;  // Convert.ToDecimal(txtCloseBal.Text);
                    BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text);
                    BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Text);
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportGroupCustomerCode = 1;
                    BaseCls.GlbReportGroupExecCode = 0;
                    if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10062))
                    {
                        BaseCls.GlbReportIsCostPrmission = 0;
                    }
                    else
                    {
                        BaseCls.GlbReportIsCostPrmission = 1;
                    }
                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    if (MessageBox.Show("Do you want summary report?", "Sign Off", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        BaseCls.GlbReportName = "SignOff.rpt";
                    }
                    else
                    {
                        BaseCls.GlbReportName = "Cashierwisesales.rpt";
                    }
                    _view.Show();
                    _view = null;
                }

                if (opt9.Checked == true) //Sales Commission Definition
                {   //update temporary table
                    //update_PC_List();
                    //15-07-2013 Sanjeewa

                    BaseCls.GlbReportHeading = "SALES COMMISSION DEFINITION";
                    BaseCls.GlbReportDoc = txtDocNo.Text;
                    BaseCls.GlbReportYear = Convert.ToInt32(cmbYear.Text);
                    BaseCls.GlbReportMonth = Convert.ToInt32(cmbMonth.SelectedIndex + 1);

                    BaseCls.GlbReportName = "Elite_Comm_Def.rpt";
                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    _view.Show();
                    _view = null;
                }

                if (opt10.Checked == true) //Sales Commission Summary
                {
                    if (txtDocNo.Text == "")
                    {
                        MessageBox.Show("Please add Circular No.", "Reconciliation Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        return;
                    }
                    if (Convert.ToInt32(cmbYear.Text) < 2013 || (Convert.ToInt32(cmbYear.Text) == 2013 && Convert.ToInt32(cmbMonth.SelectedIndex + 1) < 6))
                    {
                        MessageBox.Show("Commission is not processed for this month.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        return;
                    }

                    //update temporary table
                    update_PC_List();
                    //11-07-2013 Sanjeewa

                    BaseCls.GlbReportDoc = txtDocNo.Text;
                    BaseCls.GlbReportYear = Convert.ToInt32(cmbYear.Text);
                    BaseCls.GlbReportMonth = Convert.ToInt32(cmbMonth.SelectedIndex + 1);

                    if (OptDet.Checked == true)
                    {
                        BaseCls.GlbReportHeading = "SALES COMMISSION DETAIL";
                        BaseCls.GlbReportName = "Elite_Comm_Summary_R1D.rpt";
                    }
                    else
                    {
                        BaseCls.GlbReportHeading = "SALES COMMISSION SUMMARY";
                        BaseCls.GlbReportName = "Elite_Comm_Summary_R1.rpt";
                    }

                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    _view.Show();
                    _view = null;
                }

                if (opt11.Checked == true) //Sales Commission Summary (Plus Allowances)
                {
                    if (txtDocNo.Text == "")
                    {
                        MessageBox.Show("Please add Circular No.", "Reconciliation Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        return;
                    }
                    if (Convert.ToInt32(cmbYear.Text) < 2013 || (Convert.ToInt32(cmbYear.Text) == 2013 && Convert.ToInt32(cmbMonth.SelectedIndex + 1) < 6))
                    {
                        MessageBox.Show("Commission is not processed for this month.", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        return;
                    }
                    //update temporary table
                    update_PC_List();
                    //11-07-2013 Sanjeewa

                    BaseCls.GlbReportHeading = "SALES COMMISSION SUMMARY (WITH ALLOWANCES)";
                    BaseCls.GlbReportDoc = txtDocNo.Text;
                    BaseCls.GlbReportYear = Convert.ToInt32(cmbYear.Text);
                    BaseCls.GlbReportMonth = Convert.ToInt32(cmbMonth.SelectedIndex + 1);

                    BaseCls.GlbReportName = "Elite_Comm_Summary_R2.rpt";
                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    _view.Show();
                    _view = null;
                }

                if (opt12.Checked == true) //daily terminal - Executivewise
                {
                    BaseCls.GlbReportHeading = "DAILY TERMINAL REPORT";
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportOpBal = 0;  // Convert.ToDecimal(txtOpenBal.Text);
                    BaseCls.GlbReportCloseBal = 0;  // Convert.ToDecimal(txtCloseBal.Text);
                    BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text);
                    BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Text);
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportGroupCustomerCode = 0;
                    BaseCls.GlbReportGroupExecCode = 1;

                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "Cashierwisesales.rpt";

                    _view.Show();
                    _view = null;
                }
                if (opt13.Checked == true) //advance receipt registry
                {

                    update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = "Advance Receipt Registry";
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text);
                    BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Text);

                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "AdvanceReceiptReg.rpt";

                    _view.Show();
                    _view = null;
                }
                if (opt14.Checked == true) //over and short statement
                {

                    update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = "Over and Short Statement";
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;

                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "OverShortStatement.rpt";

                    _view.Show();
                    _view = null;
                }
                if (opt15.Checked == true) //cash comm definition
                {
                    if (string.IsNullOrEmpty(txt_Circ.Text))
                    {
                        MessageBox.Show("Please select circular ", "Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnDisplay.Enabled = true;
                        return;
                    }
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = "Cash Commissions Definitions";
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportDoc = txt_Circ.Text;

                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "CashCommDef.rpt";

                    _view.Show();
                    _view = null;
                }

                if (opt17.Checked == true)
                {        //update temporary table
                    update_PC_List_RPTDB();
                    update_PC_List();
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportHeading = "Return Cheque Settlemt Settlements";
                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "ReturnChequeSettlemtPayments.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt16.Checked == true)
                {
                    update_PC_List();
                    //update temporary table
                    //  update_PC_List_RPTDB();
                    //  update_PC_List();
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportHeading = "Return Cheque Settlemt Outstanding";
                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = "ReturnChequeSettmentOutstanding.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt18.Checked == true)
                {        //update temporary table
                    update_PC_List_RPTDB();
                    update_PC_List();
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportHeading = "Daily Expences";
                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbRecType = string.Empty;
                    if (DropDownListRemitType.Text != "--Select Type--")
                    {
                        BaseCls.GlbRecType = DropDownListRemitType.Text;
                    }
                    BaseCls.GlbReportName = "DailyExpences.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt19.Checked == true)
                {        //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportDocType = "HS";
                    BaseCls.GlbReportHeading = "HP Group sales Commission for Acheived Targets";
                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "HP_GRP_Comm_Target_Report.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt20.Checked == true) //Pending Credit Note Details
                {   //update temporary table
                    update_PC_List();
                    //07-11-2013 Sanjeewa

                    BaseCls.GlbReportHeading = "PENDING CREDIT NOTE DETAILS";

                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "Cr_Balance_Report.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt21.Checked == true)
                {        //update temporary table
                    //update_PC_List();

                    if (string.IsNullOrEmpty(cmbScheme.Text))
                    {
                        MessageBox.Show("Please select scheme ", "Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnDisplay.Enabled = true;
                        return;
                    }
                    BaseCls.GlbReportGroupProfit = Convert.ToInt32(chkGroupPC.Checked);
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportDocType = txtCircularNo.Text;
                    BaseCls.GlbReportDocSubType = cmbScheme.SelectedValue.ToString();
                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    if (optSRWise.Checked == true)
                    {
                        BaseCls.GlbReportName = "ProductBonus.rpt";
                    }
                    else
                    {
                        BaseCls.GlbReportName = "ProductBonusApp.rpt";
                    }
                    _view.Show();
                    _view = null;
                }
                if (opt22.Checked == true)
                {

                    if (string.IsNullOrEmpty(cmbScheme.Text))
                    {
                        MessageBox.Show("Please select scheme ", "Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnDisplay.Enabled = true;
                        return;
                    }
                    BaseCls.GlbReportGroupProfit = Convert.ToInt32(chkGroupPC.Checked);
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportDocType = txtCircularNo.Text;
                    BaseCls.GlbReportDocSubType = cmbScheme.SelectedValue.ToString();
                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "ProductBonusInv.rpt";

                    _view.Show();
                    _view = null;
                }
                if (opt23.Checked == true)
                {

                    if (string.IsNullOrEmpty(cmbScheme.Text))
                    {
                        MessageBox.Show("Please select scheme ", "Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnDisplay.Enabled = true;
                        return;
                    }
                    BaseCls.GlbReportGroupProfit = Convert.ToInt32(chkGroupPC.Checked);
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportDocType = txtCircularNo.Text;
                    BaseCls.GlbReportDocSubType = cmbScheme.SelectedValue.ToString();
                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    if (optSRWise.Checked == true)
                    {
                        BaseCls.GlbReportName = "ProductBonusInvInc.rpt";
                    }
                    else
                    {
                        BaseCls.GlbReportName = "ProdBonusInvIncDet.rpt";
                    }

                    _view.Show();
                    _view = null;
                }

                if (opt24.Checked == true)   //Product Bonus Details 22/01/2014 sanjeewa
                {
                    //update temporary table
                    update_PC_List();

                    BaseCls.GlbReportHeading = "Product Bonus Details";

                    objFin.ExportProductBonusDetailReport();
                    fldgOpenPath.ShowDialog();

                    string sourcefileName = BaseCls.GlbUserID + ".xls";
                    string targetfileName = ".xls";
                    string sourcePath = @"\\192.168.1.222\scm2\Print";
                    string targetPath = BaseCls.GlbReportFilePath;
                    string sourceFile = System.IO.Path.Combine(sourcePath, sourcefileName);
                    string targetFile = targetPath + targetfileName;

                    System.IO.File.Copy(sourceFile, targetFile);
                    System.IO.File.Delete(sourceFile);

                    MessageBox.Show("Export Completed", "HP Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (opt25.Checked == true) //Recieving Desk Remitance Summary
                {   //update temporary table
                    update_PC_List();
                    //24-01-2014 Sanjeewa

                    BaseCls.GlbReportHeading = "RECIEVING DESK REMITANCE SUMMARY";
                    BaseCls.GlbReportnoofDays = txtDirec.Text == "" ? 0 : IsNumeric(txtDirec.Text) == true ? Convert.ToInt16(txtDirec.Text) : 0;

                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "rcv_desk_rem_sum_rep.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt26.Checked == true) //Excess remitance statement
                {   //update temporary table
                    update_PC_List_RPTDB();
                    //31/1/2014 Nadeeka
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;

                    BaseCls.GlbReportIsAsAt = Convert.ToInt16(optAsAt.Checked);

                    BaseCls.GlbReportHeading = "Excess Remittance Statement";

                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "Excess_Rem_Statement.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt27.Checked == true) //Short Settlement Report
                {   //update temporary table
                    update_PC_List();
                    //03-02-2014 Sanjeewa

                    BaseCls.GlbReportHeading = "SHORT SETTLEMENT REPORT";

                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "Short_Sett_Report.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt28.Checked == true) //Internal Payment Voucher Report
                {   //update temporary table
                    update_PC_List();
                    //28-03-2014 Sanjeewa

                    BaseCls.GlbReportHeading = "INTERNAL PAYMENT VOUCHER REPORT";
                    BaseCls.GlbReportExecCode = txtExec.Text;
                    if (OptDet.Checked == true)
                    {
                        BaseCls.GlbReportTp = "ISS";
                    }
                    if (OptSumm.Checked == true)
                    {
                        BaseCls.GlbReportTp = "CLAIM";
                    }
                    if (optUnclaim.Checked == true)
                    {
                        BaseCls.GlbReportTp = "UNCLAIM";
                    }
                    if (optnotLeg.Checked == true)
                    {
                        BaseCls.GlbReportTp = "NOTLED";
                    }
                    if (optCancel.Checked == true)
                    {
                        BaseCls.GlbReportTp = "CANCEL";
                    }
                    // BaseCls.GlbReportTp = OptDet.Checked==true ?"ISS":"CLAIM" ;

                    objFin.InternalPayVouReport();
                }

                if (opt30.Checked == true) //ESD Statement
                {   //update temporary table
                    update_PC_List();
                    //14-06-2014 Sanjeewa

                    BaseCls.GlbReportHeading = "ESD STATEMENT";

                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "ESD_Dtl_Report.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt31.Checked == true) //ESD reconcilation
                {
                    clsFinanceRep objHp = new clsFinanceRep();
                    //update temporary table
                    update_PC_List();
                    //kapila 16/6/2014

                    BaseCls.GlbReportHeading = "ESD RECONCILATION";
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;

                    objHp.ESDRecon();

                    MasterCompany _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
                    string _path = _MasterComp.Mc_anal6;
                    objHp._ESDRecon.ExportToDisk(ExportFormatType.Excel, _path + "ESDRecon" + BaseCls.GlbUserID + ".xls");

                    Excel.Application excelApp = new Excel.Application();
                    excelApp.Visible = true;
                    string workbookPath = _path + "ESDRecon" + BaseCls.GlbUserID + ".xls";
                    Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath,
                            0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                            true, false, 0, true, false, false);


                }
                if (opt32.Checked == true) //advance/refund reconcilation
                {   //update temporary table
                    update_PC_List_RPTDB();
                    //20/6/2014 kapila

                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportHeading = "Advance/Refund Reconcilation";

                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    if (MessageBox.Show("Do you want summary report ?", "Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        BaseCls.GlbReportName = "AdvRecRecon.rpt";
                    else
                        BaseCls.GlbReportName = "AdvRecReconSum.rpt";

                    _view.Show();
                    _view = null;
                }

                if (opt33.Checked == true) //Scan Physical Not Processed Report 
                {   //update temporary table
                    update_PC_List();
                    //10-07-2014 Sanjeewa

                    BaseCls.GlbReportHeading = "SCAN PHYSICAL NOT PROCESSED REPORT";

                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "rcv_dsk_processed_Report.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt34.Checked == true) //Collection Bonus Details Report
                {   //update temporary table
                    update_PC_List();
                    //09-08-2014 Sanjeewa

                    string _filePath = string.Empty;
                    string _error = string.Empty;

                    BaseCls.GlbReportHeading = "COLLECTION BONUS DETAILS REPORT";
                    BaseCls.GlbReportDocType = opt_Issued.Checked ? "ISS" : opt_Claimed.Checked ? "CLAIM" : opt_Unclaimed.Checked ? "UNCLAIM" : "ISS";

                    _filePath = CHNLSVC.Financial.GetCollBonusDtl(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserComCode, BaseCls.GlbReportDocType, BaseCls.GlbUserID, out _error);

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

                    MessageBox.Show("Export Completed", "HP Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                if (opt35.Checked == true)
                {

                    if (string.IsNullOrEmpty(cmbaccounts.Text))
                    {
                        MessageBox.Show("Please select account ", "Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbaccounts.Focus();
                        return;
                    }
                    BaseCls.GlbReportFromDate = txtFromDate.Value.Date;
                    BaseCls.GlbReportToDate = txtToDate.Value.Date;
                    BaseCls.GlbReportDoc = cmbaccounts.Text;

                    BaseCls.GlbReportHeading = "Bank Reconciliation";
                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    //BaseCls.GlbReportName = "BankReconciliation.rpt";
                    BaseCls.GlbReportName = "BankReconciliationnew.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt36.Checked == true)
                {
                    if (string.IsNullOrEmpty(cmbaccounts.Text))
                    {
                        MessageBox.Show("Please select account ", "Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbaccounts.Focus();
                        return;
                    }
                    update_PC_List();

                    BaseCls.GlbReportFromDate = txtFromDate.Value.Date;
                    BaseCls.GlbReportToDate = txtToDate.Value.Date;
                    BaseCls.GlbReportDoc = cmbaccounts.Text;

                    BaseCls.GlbReportHeading = "Bank Statement";
                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "Bank_Statement_Report.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt37.Checked == true) //Credit Card Reconciliation - Sanjeewa 2014-10-28
                {

                    BaseCls.GlbReportDoc = cmbaccounts.Text;

                    if (string.IsNullOrEmpty(cmbaccounts.Text))
                    {
                        MessageBox.Show("Please select account ", "Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbaccounts.Focus();
                        return;
                    }

                    //BaseCls.GlbReportDoc = txtCircularNo.Text;

                    BaseCls.GlbReportHeading = "Credit Card Reconciliation";
                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "crcd_recon_report.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt38.Checked == true)
                {
                    update_PC_List_RPTDB();

                    if (chkallAccounts.Checked == true)
                    {
                        BaseCls.GlbReportDoc = "ALL";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(cmbaccounts.Text))
                        {
                            MessageBox.Show("Please select account ", "Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cmbaccounts.Focus();
                            return;
                        }
                        BaseCls.GlbReportDoc = cmbaccounts.Text;
                    }
                    BaseCls.GlbReportIsAsAt = Convert.ToInt16(optAsAt.Checked);

                    BaseCls.GlbReportHeading = "NOT REALIZE TRANSACTION REPORT";
                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "Not_Realized_Transactions.rpt";
                    _view.Show();
                    _view = null;
                }

                if (opt39.Checked == true)
                {
                    if (chkallAccounts.Checked == true)
                    {
                        BaseCls.GlbReportDoc = "ALL";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(cmbaccounts.Text))
                        {
                            MessageBox.Show("Please select account ", "Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cmbaccounts.Focus();
                            return;
                        }
                        BaseCls.GlbReportDoc = cmbaccounts.Text;
                    }

                    BaseCls.GlbReportHeading = "REALIZATIONS FINALIZE STATUS REPORT";
                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "RealizationFinalizeStatus.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt40.Checked == true)
                {
                    update_PC_List_RPTDB();

                    clsFinanceRep objHp = new clsFinanceRep();

                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                    BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text);
                    BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Text);
                    BaseCls.GlbReportName = "CashControlCash.rpt";
                    objHp.CashControlCash();

                    MasterCompany _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
                    string _path = _MasterComp.Mc_anal6;
                    objHp._cashControlCash.ExportToDisk(ExportFormatType.Excel, _path + "CashControlCash" + BaseCls.GlbUserID + ".xls");

                    Excel.Application excelApp = new Excel.Application();
                    excelApp.Visible = true;
                    string workbookPath = _path + "CashControlCash" + BaseCls.GlbUserID + ".xls";
                    Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath,
                            0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                            true, false, 0, true, false, false);
                }

                if (opt41.Checked == true)
                {
                    update_PC_List_RPTDB();

                    if (chkallAccounts.Checked == true)
                    {
                        BaseCls.GlbReportDoc = "ALL";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(cmbaccounts.Text))
                        {
                            MessageBox.Show("Please select account ", "Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cmbaccounts.Focus();
                            return;
                        }
                        BaseCls.GlbReportDoc = cmbaccounts.Text;
                    }
                    BaseCls.GlbReportIsAsAt = Convert.ToInt16(optAsAt.Checked);

                    BaseCls.GlbReportHeading = "EXTRA ADDED DOCUMENTS REPORT";
                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "Extra_add_Docs.rpt";
                    BaseCls.GlbReportDoc = chkallAccounts.Checked ? "ALL" : cmbaccounts.Text;
                    BaseCls.GlbPayType = cmbAdjType.Text;
                    if (cmbAdjType.SelectedValue != null)
                    {
                        BaseCls.GlbPayType = cmbAdjType.SelectedValue.ToString();
                    }
                    else
                    {
                        BaseCls.GlbPayType = null;
                    }
                    _view.Show();
                    _view = null;
                }
                if(opt42.Checked==true)
                {
                    update_PC_List();

                    string _filePath = string.Empty;
                    string _error = string.Empty;

                    BaseCls.GlbReportHeading = "CONSIGNMENT DETAIL REPORT";
                    BaseCls.GlbReportPriceBook = txtPB.Text;
                    BaseCls.GlbReportPBLevel = txtPBLevel.Text;
                    BaseCls.GlbReportDocType = "C";

                    _filePath = CHNLSVC.Financial.GetConsignmentDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserComCode,BaseCls.GlbUserID,txtPB.Text,txtPBLevel.Text, BaseCls.GlbReportDocType,  out _error);

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

                    MessageBox.Show("Export Completed", "HP Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (opt44.Checked == true) //credit note details - kapila
                {
                    update_PC_List();

                    string _filePath = string.Empty;
                    string _error = string.Empty;

                    BaseCls.GlbReportHeading = "CREDIT NOTE DETAIL REPORT";
                    if(optCNDet.Checked==true)
                    BaseCls.GlbReportDocType = "D";
                    if (optCNSum.Checked == true)
                        BaseCls.GlbReportDocType = "S";
                    if (optCNOth.Checked == true)
                        BaseCls.GlbReportDocType = "O";

                    _filePath = CHNLSVC.MsgPortal.GetCreditNoteDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserComCode, BaseCls.GlbUserID,  BaseCls.GlbReportDocType, out _error);

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

                    MessageBox.Show("Export Completed", "HP Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (opt45.Checked == true) //excess short summary - kapil)
                {
                     update_PC_List();

                        clsFinanceRep objFin = new clsFinanceRep();

                        BaseCls.GlbReportProfit = txtPC.Text;
                        BaseCls.GlbReportCompCode = txtComp.Text;
                        BaseCls.GlbReportComp = txtCompDesc.Text;
                        BaseCls.GlbReportCompAddr = txtCompAddr.Text;
                        BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text);
                        BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Text);
                        BaseCls.GlbReportName = "Excess_Short_Summ.rpt";
                        objFin.ExcessShortSummary();

                        MasterCompany _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
                        string _path = _MasterComp.Mc_anal6;
                        objFin._excsShortSumm.ExportToDisk(ExportFormatType.Excel, _path + "Excess_Short_Summ" + BaseCls.GlbUserID + ".xls");

                        Excel.Application excelApp = new Excel.Application();
                        excelApp.Visible = true;
                        string workbookPath = _path + "Excess_Short_Summ" + BaseCls.GlbUserID + ".xls";
                        Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath,
                                0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                                true, false, 0, true, false, false);
                }
                if (opt46.Checked == true) // Added by Chathura on 20-oct-2017
                {
                    BaseCls.GlbReportFromDate = txtFromDate.Value.Date;
                    BaseCls.GlbReportToDate = txtToDate.Value.Date;
                    if (chkallAccounts.Checked == true) { 
                        BaseCls.GlbReportDoc = ""; 
                    }
                    else
                    {
                        BaseCls.GlbReportDoc = cmbaccounts.Text;
                    }
                    
                    //BaseCls.GlbReportDoc = cmbaccounts.Text;

                    BaseCls.GlbReportHeading = "Bank Reconciliation Summery";
                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "BankReconciliationSummery.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt47.Checked == true) // Added by Chathura on 20-oct-2017
                {
                    BaseCls.GlbReportFromDate = txtFromDate.Value.Date;
                    BaseCls.GlbReportToDate = txtToDate.Value.Date;
                    BaseCls.GlbReportProfit = txtPC.Text;

                    if (chkallAccounts.Checked == true)
                    {
                        BaseCls.GlbReportDoc = "ALL";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(cmbaccounts.Text))
                        {
                            MessageBox.Show("Please select account ", "Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cmbaccounts.Focus();
                            return;
                        }
                        BaseCls.GlbReportDoc = cmbaccounts.Text;
                    }
                    
                    BaseCls.GlbReportHeading = "Realization Status";
                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "RealizationStatus.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt48.Checked == true) // Added by Chathura on 20-oct-2017
                {
                    BaseCls.GlbReportFromDate = txtFromDate.Value.Date;
                    BaseCls.GlbReportToDate = txtToDate.Value.Date;
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportComp = txtComp.Text;
                    BaseCls.GlbReportAsAtDate = txtAsAtDate.Value.Date;
                    BaseCls.GlbReportIsAsAt = Convert.ToInt16(optAsAt.Checked);
                    update_PC_List();
                                        
                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();

                    if (OptDet.Checked == true)
                    {
                        BaseCls.GlbReportHeading = "Remitance Control Reconsiliation";
                        BaseCls.GlbReportName = "RemitanceControlRecon.rpt";
                    }
                    else if (OptSumm.Checked == true)
                    {
                        BaseCls.GlbReportHeading = "Remitance Control Reconsiliation Summery";
                        BaseCls.GlbReportName = "RemitanceControlReconSummery.rpt";
                    }
                    else
                    {
                        BaseCls.GlbReportHeading = "Remitance Control Reconsiliation Summery";
                        BaseCls.GlbReportName = "RemitanceControlReconSummery.rpt";
                    }
                    
                    _view.Show();
                    _view = null;
                }
                if (opt49.Checked == true) // Added tharanga 2018/08/19
                {
                    update_PC_List();
                    BaseCls.GlbReportFromDate = txtFromDate.Value.Date;
                    BaseCls.GlbReportToDate = txtToDate.Value.Date;
                   

                    //BaseCls.GlbReportDoc = cmbaccounts.Text;

                    BaseCls.GlbReportHeading = "Rank Account Transffering Report";
                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "bankaccounttransfferingreport.rpt";
                    _view.Show();
                    _view = null;
                }
                if (opt50.Checked == true) //Manager Income Statement
                {   //update temporary table
                    update_PC_List();
                    //21-08-2018 Dilshan
                    BaseCls.GlbReportComp = txtComp.Text;
                    BaseCls.GlbReportDoc = txtAddress.Text;
                    BaseCls.GlbSelectData = txtSign.Text;
                    BaseCls.GlbReportExecCode = txtExec.Text;

                    BaseCls.GlbReportHeading = "MANAGER INCOME STATEMENT";

                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "ManagerIncome.rpt";
                    _view.Show();
                    _view = null;
                }
                if(opt51.Checked == true)
                {
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = "Over and Short Statement";
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;

                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "OverShortDetail.rpt";

                    _view.Show();
                    _view = null;
                }
                if (opt52.Checked == true)
                {
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = "Over and Short Statement";
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;

                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "OverShortSum.rpt";

                    _view.Show();
                    _view = null;
                }

                if (opt53.Checked == true)
                {
                    string _err = "";
                    DataTable dt = new DataTable();

                    update_PC_List_RPTDB();
                    dt = CHNLSVC.MsgPortal.GetLocationCount(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

                    //if (dt.Rows.Count == 0)
                    //{
                    //    MessageBox.Show("Select location", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}

                    //if (txtPB.Text == "" || txtPBLevel.Text == "")
                    //{
                    //    MessageBox.Show("pls select price book and level", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}

                    //PriceBookLevelRef pb = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, txtPB.Text.Trim(), txtPBLevel.Text.Trim());
                    //if (pb.Sapl_is_serialized)
                    //{
                    //    MessageBox.Show("Serlized Price Book Level not allowed", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}

                    //BaseCls.GlbReportPriceBook = txtPB.Text.Trim();
                    //BaseCls.GlbReportPBLevel = txtPBLevel.Text.Trim();

                    string _filePath = CHNLSVC.MsgPortal.get_voucherDetails(BaseCls.GlbUserComCode, txtCompDesc.Text, "", BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportAsAtDate,
                        BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5, BaseCls.GlbReportModel, BaseCls.GlbReportBrand, BaseCls.GlbReportItemCode, "",
                        BaseCls.GlbUserID, out _err);

                    //string _filePath = CHNLSVC.MsgPortal.GetSalesWithInv_Bal_loc_wise(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5,
                    //     BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode,
                    //     BaseCls.GlbReportProfit, BaseCls.GlbUserDefLoca, txtAsAtDate.Text, BaseCls.GlbUserID, "", "", BaseCls.GlbReportWithCost, BaseCls.GlbReportWithSerial, BaseCls.GlbReportWithStatus, BaseCls.GlbReportWithDetail, "", out _err);

                    if (!string.IsNullOrEmpty(_err))
                    {
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(_err);
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

                    MessageBox.Show("Export Completed", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (opt54.Checked == true)
                {
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = "Credit Card Reconcilation Report";
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;         

                    BaseCls.GlbReportDocType = cmbaccounts.Text;
                    BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text);
                    BaseCls.GlbReportToDate = Convert.ToDateTime(txtToDate.Text);

                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "CreditCardsReconcilation.rpt";

                    _view.Show();
                    _view = null;
                }
                if (opt55.Checked == true)
                {
                    update_PC_List_RPTDB();

                    BaseCls.GlbReportHeading = "Over and Short Movement";
                    BaseCls.GlbReportProfit = txtPC.Text;
                    BaseCls.GlbReportCompCode = txtComp.Text;
                    BaseCls.GlbReportComp = txtCompDesc.Text;
                    BaseCls.GlbReportCompAddr = txtCompAddr.Text;

                    Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
                    BaseCls.GlbReportName = "OverShortMovement.rpt";

                    _view.Show();
                    _view = null;
                }

                if (opt56.Checked == true)
                {
                    string _err = "";
                    DataTable dt = new DataTable();

                    update_PC_List_RPTDB();

                    string _filePath = CHNLSVC.MsgPortal.get_commsionComparioson(BaseCls.GlbUserComCode, txtCompDesc.Text, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate,
                        BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5, BaseCls.GlbReportModel, BaseCls.GlbReportBrand, BaseCls.GlbReportItemCode, "CREDIT",
                        BaseCls.GlbUserID, out _err);


                    if (!string.IsNullOrEmpty(_err))
                    {
                        btnDisplay.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show(_err);
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

                    MessageBox.Show("Export Completed", "Inventory Reports Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Cursor.Current = Cursors.Default;
                btnDisplay.Enabled = true;
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPF"))
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10046))
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                foreach (ListViewItem Item in lstPC.Items)
                {
                    Item.Checked = true;
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem Item in lstPC.Items)
                {
                    Item.Checked = false;
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
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
            BaseCls.GlbReportMonth = cmbMonth.SelectedIndex + 1;
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
                case CommonUIDefiniton.SearchUserControlType.InvoiceExecutive:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Employee_Executive:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "SEX" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CashComCirc:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.IncentiveCirc:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.IncentiveCircular:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
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
            try
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
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
            try
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
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                cmbaccounts.SelectedIndex = -1;
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                if (opt1.Checked == true)
                {

                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9001))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :9001)");

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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt2.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9002))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9002)");
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(2);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt3.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9003))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9003)");
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(3);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt4.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9004))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9004)");
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(4);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt6_CheckedChanged(object sender, EventArgs e)
        {
            if (opt6.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9006))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9006)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(6);
            }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbYear.Text))
            {
                BaseCls.GlbReportYear = Convert.ToInt32(cmbYear.Text);
            }
        }

        private void opt5_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9005))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9005)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(5);
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9007))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9007)");
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(7);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            //e.SuppressKeyPress = true;
        }

        private void txtToDate_KeyDown(object sender, KeyEventArgs e)
        {
            //e.SuppressKeyPress = true;
        }

        private void txtAsAtDate_KeyDown(object sender, KeyEventArgs e)
        {
            //e.SuppressKeyPress = true;
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void opt8_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt8.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9008))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9008)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt9_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt9.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9009))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9009)");
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(9);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9010))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9010)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9011))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9011)");
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(11);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btn_Srch_Exec_Click(object sender, EventArgs e)
        {
            DataTable _result = null;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            if (opt28.Checked == true)
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Employee_Executive);
                _result = CHNLSVC.CommonSearch.GetEmployeeData(_CommonSearch.SearchParams, null, null);
            }
            else
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceExecutive);
                _result = CHNLSVC.CommonSearch.SearchEmployeeAssignToProfitCenter(_CommonSearch.SearchParams, null, null);
                if (_result == null || _result.Rows.Count <= 0)
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Employee_Executive);
                    _result = CHNLSVC.CommonSearch.GetEmployeeData(_CommonSearch.SearchParams, null, null);
                }
            }
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtExec;
            _CommonSearch.ShowDialog();
            txtExec.Select();
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

        private void txtExec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_Exec_Click(null, null);
            }
        }

        private void txtExec_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Exec_Click(null, null);
        }

        private void opt12_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt12.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9012))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9008)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt13_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt13.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9013))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9013)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9014))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9014)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9015))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9015)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btn_Srch_Circ_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CashComCirc);
                DataTable _result = CHNLSVC.CommonSearch.GetCashComCircSearchDataByComp(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txt_Circ;
                _CommonSearch.ShowDialog();
                txt_Circ.Select();
                chk_Circ.Checked = false;
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void chk_Circ_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Circ.Checked == true)
            {
                txt_Circ.Text = "";
                txt_Circ.Enabled = false;
                btn_Srch_Circ.Enabled = false;
            }
            else
            {
                txt_Circ.Enabled = true;
                btn_Srch_Circ.Enabled = true;
            }
        }

        private void opt16_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt16.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9016))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9016)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9017))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9017)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9018))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9018)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void LoadRemitance()
        {
            try
            {

                List<RemitanceSumHeading> bindlist_ = new List<RemitanceSumHeading>();
                RemitanceSumHeading select = new RemitanceSumHeading();
                select.Rsd_desc = "--Select Type--";
                select.Rsd_cd = "-1";
                bindlist_.Add(select);

                List<RemitanceSumHeading> getlist_ = new List<RemitanceSumHeading>();
                getlist_ = CHNLSVC.Financial.get_rem_type_by_sec("02", 0);
                if (getlist_ != null)
                {
                    bindlist_.AddRange(getlist_);
                }

                DropDownListRemitType.DataSource = bindlist_;
                DropDownListRemitType.DisplayMember = "rsd_desc";
                DropDownListRemitType.ValueMember = "rsd_cd";
                DropDownListRemitType.SelectedValue = "-1";
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

        private void opt19_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt19.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9019))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9019)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9020))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9020)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnCircular_Click(object sender, EventArgs e)
        {
            if (label32.Text == "Account")
            {
                CommonSearch.CommonSearch _CommonSearcha = new CommonSearch.CommonSearch();
                _CommonSearcha.ReturnIndex = 0;
                _CommonSearcha.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
                DataTable _resulta = CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearcha.SearchParams, null, null);
                _CommonSearcha.IsSearchEnter = false;
                _CommonSearcha.dvResult.DataSource = _resulta;
                _CommonSearcha.BindUCtrlDDLData(_resulta);
                _CommonSearcha.obj_TragetTextBox = txtCircularNo;
                _CommonSearcha.ShowDialog();
                txtCircularNo.Select();
            }
            else
            {

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.IncentiveCircular);
                DataTable _result = CHNLSVC.CommonSearch.GetSearchPBonusCircular(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCircularNo;
                _CommonSearch.ShowDialog();
                txtCircularNo.Focus();
                bindSchemes();
            }
        }

        private void bindSchemes()
        {
            // cmbScheme.Items.Clear();
            cmbScheme.DataSource = null;
            cmbScheme.DataSource = CHNLSVC.General.GetIncentiveSchemes(txtCircularNo.Text);
            cmbScheme.DisplayMember = "INC_REF";
            cmbScheme.ValueMember = "INC_REF";
            cmbScheme.SelectedIndex = -1;
        }

        private void opt21_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt21.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9021))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9021)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt22_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt22.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9022))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9022)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9023))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9023)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt24_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt24.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9024))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9024)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9025))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9025)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
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

        private void optAsAt_CheckedChanged(object sender, EventArgs e)
        {
            pnlAsAtDate.Enabled = true;
            pnlDateRange.Enabled = false;
        }

        private void optDtRange_CheckedChanged(object sender, EventArgs e)
        {
            pnlAsAtDate.Enabled = false;
            pnlDateRange.Enabled = true;
        }

        private void fldgOpenPath_FileOk(object sender, CancelEventArgs e)
        {
            BaseCls.GlbReportFilePath = fldgOpenPath.FileName;
        }

        private void opt26_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt26.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9026))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9026)");
                        RadioButton RDO = (RadioButton)sender;
                        RDO.Checked = false;
                        return;
                    }
                    setFormControls(26);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt27_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt27.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9027))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9027)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9028))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9028)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtCircularNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCircularNo.Text))
            {
                if (label32.Text == "Account")
                {
                    DataTable _result = new DataTable();
                    _result = CHNLSVC.Financial.GET_ACC_DETAILS(BaseCls.GlbUserComCode, txtCircularNo.Text);
                    if (_result == null || _result.Rows.Count == 0)
                    {
                        MessageBox.Show("Please select a correct account no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCircularNo.Clear();
                        txtCircularNo.Focus();
                    }
                    else
                    {


                    }
                }
                else
                {
                    bindSchemes();
                }
            }
        }

        private void opt30_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt30.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9030))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9030)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9031))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9031)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt32_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt32.Checked == true)
                {
                    //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9032))
                    //{
                    //    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9032)");
                    //    RadioButton RDO = (RadioButton)sender;
                    //    RDO.Checked = false;
                    //    return;
                    //}
                    setFormControls(32);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9033))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9033)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9034))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9034)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9035))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9035)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9035))
                    //{
                    //    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9035)");
                    //    RadioButton RDO = (RadioButton)sender;
                    //    RDO.Checked = false;
                    //    return;
                    //}
                    setFormControls(38);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9036))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9036)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9035))
                    //{
                    //    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9035)");
                    //    RadioButton RDO = (RadioButton)sender;
                    //    RDO.Checked = false;
                    //    return;
                    //}
                    setFormControls(39);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void bind_Combo_Accounts()
        {
            DataTable dtaccounts = CHNLSVC.Sales.LoadBankAccounts();
            if (dtaccounts.Rows.Count > 0)
            {

                cmbaccounts.DataSource = dtaccounts;
                cmbaccounts.DisplayMember = "MSTM_SUN_ACC";
                cmbaccounts.ValueMember = "MSTM_SUN_ACC";
                cmbaccounts.SelectedIndex = -1;


            }
            else
            {
                cmbaccounts.DataSource = null;
            }
        }

        private void bind_Combo_CC_Accounts()
        {
            DataTable dtaccounts = CHNLSVC.Sales.LoadCCAccounts();
            if (dtaccounts.Rows.Count > 0)
            {

                cmbaccounts.DataSource = dtaccounts;
                cmbaccounts.DisplayMember = "MSTM_SUN_ACC";
                cmbaccounts.ValueMember = "MSTM_SUN_ACC";
                cmbaccounts.SelectedIndex = -1;


            }
            else
            {
                cmbaccounts.DataSource = null;
            }
        }

        private void chkallAccounts_CheckedChanged(object sender, EventArgs e)
        {
            if (chkallAccounts.Checked == true)
            {
                cmbaccounts.Enabled = false;
                btnDisplay.Enabled = true;
            }
            else
            {
                cmbaccounts.Enabled = true;
                btnDisplay.Enabled = true;
            }


        }

        private void Financial_Rep_Load(object sender, EventArgs e)
        {

        }

        private void cmbaccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDisplay.Enabled = true;
        }

        //private void cmbUsers_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    btnDisplay.Enabled = true;
        //}

        private void opt37_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (opt37.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9037))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9037)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt40_CheckedChanged(object sender, EventArgs e)
        {
            if (opt40.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9040))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9040)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(40);
            }
        }

        private void opt41_CheckedChanged(object sender, EventArgs e)
        {
            if (opt41.Checked == true)
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9041))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9041)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(41);
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

        private void opt42_CheckedChanged(object sender, EventArgs e)
        {
            if (opt42.Checked == true)
            {
                //if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9041))
                //{
                //    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9041)");
                //    RadioButton RDO = (RadioButton)sender;
                //    RDO.Checked = false;
                //    return;
                //}
                setFormControls(42);
            }
        }

        private void opt43_CheckedChanged(object sender, EventArgs e)
        {
            setFormControls(43);
        }

        private void opt44_CheckedChanged(object sender, EventArgs e)
        {
            setFormControls(44);
        }

        private void opt45_CheckedChanged(object sender, EventArgs e)
        {
            if (opt45.Checked == true)
            {

                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9045))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Reqired permission code :9045)");

                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(6);
            }
        }
              
              
        private void opt47_CheckedChanged_1(object sender, EventArgs e) // Added by Chathura on 20-oct-2017
        {
            try
            {
                if (opt47.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9035))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9035)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt48_CheckedChanged(object sender, EventArgs e) // Added by Chathura on 24-oct-2017
        {
            try
            {
                if (opt48.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9035))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9035)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void opt46_CheckedChanged_1(object sender, EventArgs e) // Added by Chathura on 20-oct-2017
        {
            try
            {
                if (opt46.Checked == true)
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9035))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9035)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9046))
                    {
                        MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9046)");
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
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9005))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9050)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(50);
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        //dilshan on 07-09-2018
        private void opt51_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9051))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9051)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(51);
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9052))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9052)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(52);
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void chk_Adj_Tp_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Adj_Tp.Checked == true)
            {
                cmbAdjType.SelectedIndex = -1;
                cmbAdjType.Enabled = false;

            }
            else
            {
                cmbAdjType.Enabled = true;

            }
        }

        private void opt53_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16124))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :16124)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(53);
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 16126))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :16126)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(54);
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        //dilshan on 09-11-2018
        private void opt55_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 9051))
                {
                    MessageBox.Show("Sorry, You have no permission to view this report!\n( Advice: Required permission code :9055)");
                    RadioButton RDO = (RadioButton)sender;
                    RDO.Checked = false;
                    return;
                }
                setFormControls(55);
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}


