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
using System.Diagnostics;

namespace FF.WindowsERPClient.HP
{
    //pkg_search.sp_searchhpaccount  UPDATE
    //pkg_search.get_active_hp_ACC    NEW
    //sp_Transfer_HPAccount =   UPDATE
    //sp_get_AllGurantors  =NEW
    //SP_Get_hp_customer = UPDATE
    //sp_remove_Guranter =NEW

    public partial class AccountsDetailUpdates : Base
    {
        // DataTable bankCodes = new DataTable();

        List<MasterBusinessEntity> NewGurantorList = new List<MasterBusinessEntity>();
        public AccountsDetailUpdates()
        {
            InitializeComponent();
            visible_panel(new Panel());
            txtPC.Text = BaseCls.GlbUserDefProf;
            txtCurrentDt.Text = CHNLSVC.Security.GetServerDateTime().Date.ToString();//DateTime.Now;
            //bankCodes = CHNLSVC.Sales.GetHp_flag_bank_onType("BANK");

        }
        private void clearOptionsAndPanels()
        {
            rdoMortgage.Checked = false;
            rdoCategorize.Checked = false;
            rdoAccTransfer.Checked = false;
            rdoCustTransfer.Checked = false;
            rdoGurnantorTransfer.Checked = false;
            rdoCustDetChange.Checked = false;
            rdoClsAccAct.Checked = false;
            visible_panel(null);

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            AccountsDetailUpdates formnew = new AccountsDetailUpdates();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

        private void AccountsDetailUpdates_Load(object sender, EventArgs e)
        {

        }

        private void rdoMortgage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdoMortgage.Checked == true)
                {
                    //string _OrgPC = txtPC.Text.Trim();
                    //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _OrgPC, "HP1"))
                    //{                   
                    //    DataTable datasource1 = CHNLSVC.Sales.GetHp_flag_bank_onType("BANK");
                    //    ddlMortgageCd.DisplayMember = "description";// datasource1.Columns["description"].ToString();//"description" 
                    //    ddlMortgageCd.ValueMember = "code";
                    //    ddlMortgageCd.DataSource = datasource1;
                    //    visible_panel(divMortgage);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Permission Not Granted!\n( Advice: Reqired permission code :HP1 )", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}


                    string _OrgPC = txtPC.Text.Trim();
                    if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10028))
                    {
                        DataTable datasource1 = CHNLSVC.Sales.GetHp_flag_bank_onType("BANK");
                        ddlMortgageCd.DisplayMember = "description";// datasource1.Columns["description"].ToString();//"description" 
                        ddlMortgageCd.ValueMember = "code";
                        ddlMortgageCd.DataSource = datasource1;
                        visible_panel(divMortgage);
                    }
                    else
                    {
                        MessageBox.Show("Permission Not Granted!\n( Advice: Reqired permission code :10028 )", "Option Permission", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        clearOptionsAndPanels();
                        return;
                    }

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

        private void rdoCategorize_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdoCategorize.Checked == true)
                {
                    //string _OrgPC = txtPC.Text.Trim();
                    //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _OrgPC, "HP2"))
                    //{
                    //    DataTable datasource2 = CHNLSVC.Sales.GetHp_flag_bank_onType("FLAG");
                    //    ddlCategoCd.DisplayMember = "description";
                    //    ddlCategoCd.ValueMember = "code";
                    //    ddlCategoCd.DataSource = datasource2;
                    //    visible_panel(divCategorize);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Permission Not Granted!\n( Advice: Reqired permission code :HP2 )", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}               
                    string _OrgPC = txtPC.Text.Trim();
                    if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10029))
                    {
                        DataTable datasource2 = CHNLSVC.Sales.GetHp_flag_bank_onType("FLAG");
                        ddlCategoCd.DisplayMember = "description";
                        ddlCategoCd.ValueMember = "code";
                        ddlCategoCd.DataSource = datasource2;
                        visible_panel(divCategorize);
                    }
                    else
                    {
                        MessageBox.Show("Permission Not Granted!\n( Advice: Reqired permission code :10029 )", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        clearOptionsAndPanels();
                        return;
                    }
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

        private void rdoAccTransfer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdoAccTransfer.Checked == true)
                {
                    //string _OrgPC = txtPC.Text.Trim();
                    //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _OrgPC, "HP3"))
                    //{
                    //    visible_panel(divAccTrans);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Permission Not Granted!\n( Advice: Reqired permission code :HP3 )", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //} 
                    string _OrgPC = txtPC.Text.Trim();
                    if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10030))
                    {
                        visible_panel(divAccTrans);
                    }
                    else
                    {
                        MessageBox.Show("Permission Not Granted!\n( Advice: Reqired permission code :10030 )", "Option Permission", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        clearOptionsAndPanels();
                        return;
                    }
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

        private void rdoCustTransfer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdoCustTransfer.Checked == true)
                {
                    //string _OrgPC = txtPC.Text.Trim();
                    //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "HP4"))
                    //{
                    //    visible_panel(divCustTrans);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Permission Not Granted!\n( Advice: Reqired permission code :HP4 )", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}
                    string _OrgPC = txtPC.Text.Trim();
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10033))
                    {

                        MessageBox.Show("Permission Not Granted!\n( Advice: Reqired permission code :10033 )", "Option Permission", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //----------------------------------------------------------------------------
                    List<string> accountsList = new List<string>();

                    foreach (DataGridViewRow gvr in grvAccounts.Rows)
                    {
                        DataGridViewCheckBoxCell chk = gvr.Cells[0] as DataGridViewCheckBoxCell;
                        if (Convert.ToBoolean(chk.Value) == true)
                        {
                            string acc_no = gvr.Cells["hpa_acc_no"].Value.ToString();//gvr.Cells[1].Text;
                            accountsList.Add(acc_no);
                        }
                    }
                    if (accountsList.Count > 1)
                    {
                        MessageBox.Show("Please select only one account at a time!", "Accounts", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearOptionsAndPanels();
                        return;
                    }
                    //-----------------------------------------------------------------------------


                    visible_panel(divCustTrans);
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

        private void rdoCustDetChange_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdoCustDetChange.Checked == true)
                {
                    visible_panel(null);
                    //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "HP4"))
                    //{
                    //    General.CustomerCreation CUST = new General.CustomerCreation(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty);                   
                    //    CUST.Show();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Permission Not Granted!\n( Advice: Reqired permission code :HP4 )", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}
                    if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10031))
                    {
                        General.CustomerCreation CUST = new General.CustomerCreation(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty);
                        CUST.Show();
                    }
                    else
                    {
                        MessageBox.Show("Permission Not Granted!\n( Advice: Reqired permission code :10031 )", "Option Permission", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        clearOptionsAndPanels();
                        return;
                    }
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

        private void btnAccOk_Click(object sender, EventArgs e)
        {
            try
            {
                panel5.Visible = false;
                DateTime currDate = Convert.ToDateTime(txtCurrentDt.Text.Trim());
                DateTime fromDt;
                DateTime toDt;
                try
                {
                    fromDt = txtFrmDt.Value;
                    toDt = txtToDt.Value;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enter valid dates!");
                    return;
                }
                DataTable dt = new DataTable();

                this.Cursor = Cursors.WaitCursor;
                dt = CHNLSVC.Sales.GetHp_ActiveAccounts(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), currDate, fromDt, toDt, null, null);
                grvAccounts.DataSource = null;
                grvAccounts.AutoGenerateColumns = false;
                grvAccounts.DataSource = dt;
                grvAccounts_RowDataBound();
                this.Cursor = Cursors.Default;
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
        private void visible_panel(Panel showPanel)
        {
            try
            {
                divMortgage.Visible = false;
                divCategorize.Visible = false;
                divAccTrans.Visible = false;
                divManTrans.Visible = false;
                divCustTrans.Visible = false;
                panel_newMortgage.Visible = false;
                panel_newFlag.Visible = false;
                divClsAccAct.Visible = false;
                divGuranterTrans.Visible = false;

                if (showPanel != null)
                {
                    showPanel.Visible = true;
                    // showPanel.Location = new Point(1, 451);   
                    showPanel.Location = new Point(1, 391);
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

        private void btn_UPDATE_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                panel5.Visible = false;
                DateTime currDate = txtCurrentDt.Value;
                string AccNumber = txtAccNo.Text.Trim();

                DataTable dt = new DataTable();
                this.Cursor = Cursors.WaitCursor;
                //dt = CHNLSVC.Sales.GetHp_ActiveAccounts(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper(), currDate, DateTime.MaxValue, DateTime.MaxValue, AccNumber, null);
                dt = CHNLSVC.Sales.GetHp_ActiveAccounts(BaseCls.GlbUserComCode, string.Empty, currDate, DateTime.MaxValue, DateTime.MaxValue, AccNumber, null);
                grvAccounts.DataSource = null;
                grvAccounts.AutoGenerateColumns = false;
                grvAccounts.DataSource = dt;

                grvAccounts_RowDataBound();
                this.Cursor = Cursors.Default;
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
        protected void grvAccounts_RowDataBound()
        {
            try
            {
                if (grvAccounts.Rows.Count > 0)
                {
                    DataTable bnkCodes = CHNLSVC.Sales.GetHp_flag_bank_onType("BANK");
                    DataTable flagCodes = CHNLSVC.Sales.GetHp_flag_bank_onType("FLAG");
                    DateTime currDate = txtCurrentDt.Value;
                    foreach (DataGridViewRow gvr in grvAccounts.Rows)
                    {
                        #region
                        string Acc_no = gvr.Cells["hpa_acc_no"].Value.ToString();//e.Row.Cells[1].Text;
                        //DateTime currDate = Convert.ToDateTime(txtCurrentDt.Text.Trim());
                        Decimal accBal = CHNLSVC.Sales.Get_AccountBalance(currDate, Acc_no);
                        gvr.Cells["accountBal"].Value = accBal;

                        string custName = CHNLSVC.Sales.GetHpCustomerName(Acc_no);
                        gvr.Cells["custName"].Value = custName.ToString();

                        try
                        {
                            HpCustomer customer = CHNLSVC.Sales.Get_HpAccCustomer("C", string.Empty, 1, Acc_no);
                            gvr.Cells["customerCode"].Value = customer.Htc_cust_cd;
                        }
                        catch (Exception ex) { }


                        string pc = gvr.Cells["hal_pc"].Value.ToString();
                        //DateTime ars_dt;
                        //DateTime sup_dt;
                        //processForGettingArrears(pc, out ars_dt, out sup_dt);

                        //*************************************************
                        //DateTime arrDt = new DateTime();
                        //DateTime supDt = new DateTime();
                        //HpAccountSummary.get_ArearsDate_SupDate(pc, currDate, out arrDt, out supDt);
                        //HpAccountSummary SUMM = new HpAccountSummary();
                        //Decimal ARREARS = HpAccountSummary.getArears(Acc_no, SUMM, pc, currDate, arrDt, sup_dt);  

                        //***************
                        DateTime arr_date = new DateTime();
                        DateTime sup_date = new DateTime();
                        HpAccountSummary SUMM = new HpAccountSummary();
                        HpAccountSummary.get_ArearsDate_SupDate(pc, currDate, out arr_date, out sup_date);
                        //DateTime dt1 = GetLastDayOfPreviousMonth(recipt_date.AddMonths(1));
                        Decimal MIN_ARREARS = HpAccountSummary.Get_Minimum_Arrears(Acc_no, arr_date, sup_date, pc);//88
                        Decimal ARREARS = HpAccountSummary.getArears(Acc_no, SUMM, pc, currDate, arr_date, currDate);
                        //***************
                        gvr.Cells["arrears"].Value = string.Format("{0:n2}", ARREARS);//ARREARS.ToString();
                        #endregion

                        txtPC.Text = gvr.Cells["hal_pc"].Value.ToString();//pc.Text;

                        ////////////////////////////////////////////////////////////*****

                        var rowColl = bnkCodes.AsEnumerable();

                        string bank_name = (from r in rowColl
                                            // where r.Field<int>("ID") == 0
                                            where r.Field<string>("code") == gvr.Cells["hpa_bank"].Value.ToString()
                                            select r.Field<string>("description")).First<string>();
                        //gvr.Cells["hpa_bank"].Value = gvr.Cells["hpa_bank"].Value.ToString() + " " + bank_name + ")";
                        gvr.Cells["bankName"].Value = bank_name;

                        rowColl = flagCodes.AsEnumerable();
                        string flag_name = (from r in rowColl
                                            where r.Field<string>("code") == gvr.Cells["hpa_flag"].Value.ToString()
                                            select r.Field<string>("description")).First<string>();
                        gvr.Cells["flagName"].Value = flag_name;

                    }
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
        protected void processForGettingArrears(string pc, out DateTime ars_dt, out DateTime sup_dt)
        {
            try
            {
                ars_dt = DateTime.MinValue.Date;
                sup_dt = DateTime.MinValue.Date;
                HpAccountSummary SUMMARY = new HpAccountSummary();
                DataTable hierchy_tbl = new DataTable();
                hierchy_tbl = SUMMARY.getHP_Hierachy(pc);//call sp_get_hp_hierachy
                if (hierchy_tbl.Rows.Count > 0)
                {
                    foreach (DataRow da in hierchy_tbl.Rows)
                    {
                        string party_tp = Convert.ToString(da["MPI_CD"]);
                        string party_cd = Convert.ToString(da["MPI_VAL"]);
                        //----------------------------------------------------
                        DataTable info_tbl = new DataTable();
                        info_tbl = SUMMARY.getArrearsInfo(party_tp, party_cd, txtCurrentDt.Value);//returns one row
                        if (info_tbl.Rows.Count > 0)
                        {
                            DataRow DrECD = info_tbl.Rows[0];
                            DateTime HADD_ARS_DT = Convert.ToDateTime(info_tbl.Rows[0]["HADD_ARS_DT"]);//hadd_ars_dt
                            DateTime HADD_SUP_DT = Convert.ToDateTime(info_tbl.Rows[0]["HADD_SUP_DT"]);//hadd_sup_dt
                            ars_dt = HADD_ARS_DT;
                            sup_dt = HADD_SUP_DT;

                            return;
                        }
                        else
                        {
                            // Arrears = 0;
                            // return Arrears;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ars_dt = DateTime.Now;
                sup_dt = DateTime.Now;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void btnBank_Create_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_bCode.Text.Trim() == "")
                {
                    MessageBox.Show("Enter new code!", "Information missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txt_bDescript.Text.Trim() == "")
                {
                    MessageBox.Show("Enter code description!", "Information missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //CREATE NEW
                DataTable datasource = CHNLSVC.Sales.GetHp_flag_bank_onType("BANK");
                foreach (DataRow dr in datasource.Rows)
                {
                    if (txt_bCode.Text.Trim().ToUpper() == dr["code"].ToString() && "BANK" == dr["type_"].ToString())
                    {
                        MessageBox.Show("Code already exists!\n(" + dr["code"].ToString() + ": " + dr["description"].ToString() + ")", "Information missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (dr["description"].ToString().Trim().ToUpper() == txt_bDescript.Text.Trim().ToUpper() && dr["type_"].ToString() == "BANK")
                    {
                        MessageBox.Show("Enter different description!\n(This description is given to a code aleardy.)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (MessageBox.Show("Are you sure to create?", "Confirm Create", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                #region
                HPR_FlagBank fb = new HPR_FlagBank();
                fb.Hfb_cd = txt_bCode.Text.Trim().ToUpper();
                fb.Hfb_tp = "BANK";//ddlFB_type.SelectedValue;
                fb.Hpf_desc = txt_bDescript.Text.Trim().ToUpper();
                fb.Hpf_cre_by = BaseCls.GlbUserID;//GlbUserName;
                fb.Hpf_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;
                Int32 eff = 0;
                try
                {
                    eff = CHNLSVC.Sales.Save_FlagBank(fb);
                }
                catch (Exception ex)
                {
                    CHNLSVC.CloseChannel();
                }

                if (eff > 0)
                {
                    txt_bCode.Text = "";
                    txt_bDescript.Text = "";
                    MessageBox.Show("Successfully Created!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    txt_bCode.Text = "";
                    txt_bDescript.Text = "";
                    MessageBox.Show("Not Created!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                #endregion
                //--------------------------------------------
                DataTable datasource1 = CHNLSVC.Sales.GetHp_flag_bank_onType("BANK");
                ddlMortgageCd.DisplayMember = "description";// datasource1.Columns["description"].ToString();//"description" 
                ddlMortgageCd.ValueMember = "code";
                ddlMortgageCd.DataSource = datasource1;
                //----------------------------------------
                visible_panel(divMortgage);
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

        private void btnFlag_Create_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_fCode.Text.Trim() == "")
                {
                    MessageBox.Show("Enter new code!", "Information missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txt_fDescript.Text.Trim() == "")
                {
                    MessageBox.Show("Enter code description!", "Information missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ///////////////////////////////////
                DataTable datasource = CHNLSVC.Sales.GetHp_flag_bank_onType("FLAG");
                foreach (DataRow dr in datasource.Rows)
                {
                    if (txt_fCode.Text.Trim().ToUpper() == dr["code"].ToString() && "FLAG" == dr["type_"].ToString())
                    {
                        MessageBox.Show("Code already exists!\n(" + dr["code"].ToString() + ": " + dr["description"].ToString() + ")", "Information missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (dr["description"].ToString().Trim().ToUpper() == txt_fDescript.Text.Trim().ToUpper() && dr["type_"].ToString() == "FLAG")
                    {
                        MessageBox.Show("Enter different description!\n(This description is given to a code aleardy.)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (MessageBox.Show("Are you sure to create?", "Confirm Create", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                ///////////////////////////////////
                #region
                HPR_FlagBank fb = new HPR_FlagBank();
                fb.Hfb_cd = txt_fCode.Text.Trim().ToUpper();
                fb.Hfb_tp = "FLAG";//ddlFB_type.SelectedValue;
                fb.Hpf_desc = txt_fDescript.Text.Trim().ToUpper();
                fb.Hpf_cre_by = BaseCls.GlbUserID;//GlbUserName;
                fb.Hpf_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;

                Int32 eff = 0;
                try
                {
                    eff = CHNLSVC.Sales.Save_FlagBank(fb);
                }
                catch (Exception ex)
                {
                    CHNLSVC.CloseChannel();
                }
                if (eff > 0)
                {
                    MessageBox.Show("Successfully Created!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Not Created!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                #endregion
                txt_fCode.Text = "";
                txt_fDescript.Text = "";

                //--------------------------------------
                DataTable datasource2 = CHNLSVC.Sales.GetHp_flag_bank_onType("FLAG");
                ddlCategoCd.DisplayMember = "description";
                ddlCategoCd.ValueMember = "code";
                ddlCategoCd.DataSource = datasource2;
                //--------------------------------------
                visible_panel(divCategorize);
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                visible_panel(panel_newMortgage);
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

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                visible_panel(panel_newFlag);
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

        private void btnCloseNewMort_Click(object sender, EventArgs e)
        {
            try
            {
                txt_bCode.Text = "";
                txt_bDescript.Text = "";
                visible_panel(divMortgage);
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

        private void btnNewCloseFlag_Click(object sender, EventArgs e)
        {
            try
            {
                visible_panel(divCategorize);
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

        private void grvAccounts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 14 && e.RowIndex != -1)
                {
                    string AccNo = grvAccounts.Rows[e.RowIndex].Cells["hpa_acc_no"].Value.ToString();//e.Row.Cells[1].Text;
                    // MessageBox.Show(AccNo);

                    DateTime curDate = txtCurrentDt.Value;

                    HpAccount Acc = new HpAccount();
                    this.Cursor = Cursors.WaitCursor;
                    this.Enabled = false;
                    Acc = CHNLSVC.Sales.GetHP_Account_onAccNo(AccNo);
                    panel5.Visible = true;
                    ucHpAccountSummary1.set_all_values(Acc, BaseCls.GlbUserDefProf, curDate, txtPC.Text.Trim());
                    //ucHpAccountSummary1.set_all_values(ac, BaseCls.GlbUserDefProf, Convert.ToDateTime(txtReceiptDate.Text).Date, ddl_Location.SelectedValue.ToString());
                    this.Cursor = Cursors.Default;
                    this.Enabled = true;


                }
                if (e.ColumnIndex == 0 && e.RowIndex != -1)
                {
                    clearOptionsAndPanels();
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
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "A" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Hp_ActiveAccounts:
                    {
                        DateTime currDate = Convert.ToDateTime(txtCurrentDt.Text.Trim());
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + currDate.ToShortDateString() + seperator + DateTime.MaxValue.ToShortDateString() + seperator + DateTime.MaxValue.ToShortDateString() + seperator + "");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HpAcc4Act:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtAccActPc.Text.Trim() + seperator + "T" + seperator);
                        break;
                    }

                //Hp_ActiveAccounts
                default:
                    break;
            }
            return paramsText.ToString();
        }
        private void btnAcSummClose_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }

        private void ImgBtnNewPC_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtTrPC;
                _CommonSearch.ShowDialog();
                txtTrPC.Focus();
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

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    // txtNIC.Focus();
                    btnConfCustTr.Select();

                }
                else if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                    DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtCustCode;
                    _CommonSearch.ShowDialog();
                    txtCustCode.Select();
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

        private void ImgBtnCust_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCustCode;
                _CommonSearch.ShowDialog();
                txtCustCode.Focus();
                this.txtCustCode_KeyPress(null, null);
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

        private void btnPcSearch_Click(object sender, EventArgs e)
        {
            try
            {
                panel5.Visible = false;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPC;
                _CommonSearch.ShowDialog();
                txtPC.Focus();
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

        private void txtPC_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnPcSearch_Click(null, null);
        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.btnPcSearch_Click(null, null);
            }

        }

        private void btnAccSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                //_CommonSearch.ReturnIndex = 1;
                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
                //DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
                //_CommonSearch.dvResult.DataSource = _result;
                //_CommonSearch.BindUCtrlDDLData(_result);
                //_CommonSearch.obj_TragetTextBox = txtAccNo;
                //_CommonSearch.ShowDialog();
                //txtAccNo.Focus();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Hp_ActiveAccounts);
                DataTable _result = CHNLSVC.CommonSearch.Get_Hp_ActiveAccounts(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAccNo;
                _CommonSearch.ShowDialog();
                txtAccNo.Focus();
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

        private void txtAccNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.btnAccSearch_Click(null, null);
            }
        }

        private void txtAccNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnAccSearch_Click(null, null);
        }

        private void txtTrPC_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ImgBtnNewPC_Click(null, null);
        }

        private void txtTrPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // txtNIC.Focus();
                btnConfAccTr.Select();
            }
            else if (e.KeyCode == Keys.F2)
            {
                this.ImgBtnNewPC_Click(null, null);
            }

        }

        private void txtCustCode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ImgBtnCust_Click(null, null);
        }

        private void btnConfAccTr_Click(object sender, EventArgs e)
        {
            try
            {
                if (grvAccounts.Rows.Count < 1)
                {
                    MessageBox.Show("Please add account(s) first!", "Add Accounts", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                txtTrPC.Text = txtTrPC.Text.Trim().ToUpper();
                DataTable DT = CHNLSVC.General.GetPartyCodes(BaseCls.GlbUserComCode, txtTrPC.Text.Trim().ToUpper());
                if (DT == null)
                {
                    MessageBox.Show("Invalid Profit center!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTrPC.Focus();
                    return;
                }
                else if (DT.Rows.Count < 1)
                {
                    MessageBox.Show("Invalid Profit center!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTrPC.Focus();
                    return;
                }
                List<string> accountsList = new List<string>();
                foreach (DataGridViewRow gvr in grvAccounts.Rows)
                {
                    DataGridViewCheckBoxCell chk = gvr.Cells[0] as DataGridViewCheckBoxCell;
                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        string acc_no = gvr.Cells["hpa_acc_no"].Value.ToString();//gvr.Cells[1].Text;
                        accountsList.Add(acc_no);
                    }
                }
                if (accountsList.Count < 1)
                {
                    MessageBox.Show("Please select account(s) first!", "Select Accounts", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("Are you sure to transfer?", "Confirm Transfer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                DateTime curDate = Convert.ToDateTime(txtCurrentDt.Text.Trim());

                Int32 eff = 0;
                try
                {
                    eff = CHNLSVC.Sales.Transfer_accounts(txtTrPC.Text.Trim().ToUpper(), curDate, accountsList);
                }
                catch (Exception ex)
                {
                    CHNLSVC.CloseChannel();
                }

                if (eff > 0)
                {
                    MessageBox.Show("Succsessfully Transfered!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error Occured. Failed Transfer!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.btnClear_Click(null, null);
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

        private void btnConfMorg_Click(object sender, EventArgs e)
        {
            try
            {
                if (grvAccounts.Rows.Count < 1)
                {
                    MessageBox.Show("Please add account(s) first!", "Add Accounts", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                List<string> accountsList = new List<string>();
                foreach (DataGridViewRow gvr in grvAccounts.Rows)
                {
                    DataGridViewCheckBoxCell chk = gvr.Cells[0] as DataGridViewCheckBoxCell;
                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        string acc_no = gvr.Cells["hpa_acc_no"].Value.ToString();//gvr.Cells[1].Text;
                        accountsList.Add(acc_no);
                    }
                }
                if (accountsList.Count < 1)
                {
                    MessageBox.Show("Please select accounts first!", "Select Accounts", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (MessageBox.Show("Are you sure to change?", "Confirm Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                Int32 eff = 0;
                try
                {
                    eff = CHNLSVC.Sales.Update_Flag_Bank("BANK", ddlMortgageCd.SelectedValue.ToString(), accountsList);
                }
                catch (Exception ex)
                {
                    CHNLSVC.CloseChannel();
                }

                if (eff > 0)
                {
                    MessageBox.Show("Successfully Updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error in Updation!\nRecords are not updated.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.btnClear_Click(null, null);
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

        private void txtCustCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                BlackListCustomers black_g = CHNLSVC.Sales.GetBlackListCustomerDetails(txtGurantorCd.Text.Trim(), 1);
                if (black_g != null)
                {
                    if (black_g.Hbl_cust_cd == txtGurantorCd.Text.Trim())
                    {
                        MessageBox.Show("This customer is black listed!", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                //--------------------------------------------------------------------------------------
                MasterBusinessEntity cust = getCustomer(txtCustCode.Text.Trim(), 3, null);
                if (cust == null)
                {
                    cust = getCustomer(txtCustCode.Text.Trim(), 1, null);
                }
                if (cust != null)
                {
                    txtAddresline1.Text = cust.Mbe_add1;//cust.Htc_adr_01;
                    txtAddresline2.Text = cust.Mbe_add2; //cust.Htc_adr_02;
                    txtAddresline3.Text = "";// cust.Htc_adr_03;
                }
                else
                {
                    txtAddresline1.Text = string.Empty;
                    txtAddresline2.Text = string.Empty;
                    txtAddresline3.Text = string.Empty;
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
        private MasterBusinessEntity getCustomer(string custCode, Int32 addressType, string accNo)
        {
            try
            {
                MasterBusinessEntity customer = new MasterBusinessEntity();
                customer = CHNLSVC.Sales.Get_HpAccCustomer_NEW("C", custCode, addressType, accNo);
                if (customer != null)
                {
                    //txtAddresline1.Text = customer.Htc_adr_01;
                    //txtAddresline2.Text = customer.Htc_adr_02;
                    //txtAddresline3.Text = customer.Htc_adr_03;
                    return customer;
                }
                else
                {
                    txtAddresline1.Text = string.Empty;
                    txtAddresline2.Text = string.Empty;
                    txtAddresline3.Text = string.Empty;
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnConfCustTr_Click(object sender, EventArgs e)
        {
            try
            {
                string _OrgPC = txtPC.Text.Trim();
                List<string> accountsList = new List<string>();

                foreach (DataGridViewRow gvr in grvAccounts.Rows)
                {
                    DataGridViewCheckBoxCell chk = gvr.Cells[0] as DataGridViewCheckBoxCell;
                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        string acc_no = gvr.Cells["hpa_acc_no"].Value.ToString();//gvr.Cells[1].Text;
                        accountsList.Add(acc_no);
                    }
                }
                if (accountsList.Count < 1)
                {
                    MessageBox.Show("Please select accounts first!", "Select Accounts", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DateTime curDate = Convert.ToDateTime(txtCurrentDt.Text.Trim());//CHNLSVC.Security.GetServerDateTime().Date;//DateTime.Now;
                HpCustomer _HpAccountCus = new HpCustomer();
                _HpAccountCus.Htc_adr_01 = txtAddresline1.Text; //+ txtAddresline2.Text;
                _HpAccountCus.Htc_adr_02 = txtAddresline2.Text;
                _HpAccountCus.Htc_adr_03 = txtAddresline3.Text;
                _HpAccountCus.Htc_adr_tp = 3;
                _HpAccountCus.Htc_cre_by = BaseCls.GlbUserID;// GlbUserName;
                _HpAccountCus.Htc_cre_dt = curDate;
                _HpAccountCus.Htc_cust_cd = txtCustCode.Text.Trim();
                _HpAccountCus.Htc_cust_tp = "C";

                //  HpCustomer customer = getCustomer(txtCustCode.Text.Trim(), 3, null);
                BlackListCustomers black_g = CHNLSVC.Sales.GetBlackListCustomerDetails(txtGurantorCd.Text.Trim(), 1);
                if (black_g != null)
                {
                    if (black_g.Hbl_cust_cd == txtGurantorCd.Text.Trim())
                    {
                        MessageBox.Show("This customer is black listed!", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                MasterBusinessEntity customer = getCustomer(txtCustCode.Text.Trim(), 3, null);
                if (customer != null)
                {
                    // txtAddresline1.Text = customer.Htc_adr_01;
                    // txtAddresline2.Text = customer.Htc_adr_02;
                    // txtAddresline3.Text = customer.Htc_adr_03;
                }
                else
                {
                    //customer = CHNLSVC.Sales.Get_HpAccCustomer("C", txtCustCode.Text.Trim(), 1, null);
                    customer = getCustomer(txtCustCode.Text.Trim(), 1, null);
                    if (customer == null)
                    {
                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cusomer does not exists in our records!");//this customer number is not in HPT_CUST table
                        MessageBox.Show("Cusomer does not exists in our records!", "Select Accounts", MessageBoxButtons.OK, MessageBoxIcon.Warning);//this customer number is not in HPT_CUST table
                        return;
                    }

                }
                if (MessageBox.Show("Are you sure to change the ownership?", "Confirm Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                Int32 eff = 0;
                try
                {
                    //  eff = CHNLSVC.Sales.Update_AccCustomer(_HpAccountCus, accountsList, txtCustCode.Text.Trim());//commented on 03/06/2013
                    eff = CHNLSVC.Sales.Update_Account_Ownership(txtCustCode.Text.Trim(), accountsList, curDate);
                    if (eff > 0)
                    {
                        MessageBox.Show("Successfully changed the ownership!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to change!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    CHNLSVC.CloseChannel();
                    MessageBox.Show("Error occured!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.btnClear_Click(null, null);
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

        private void btnConfCatego_Click(object sender, EventArgs e)
        {
            try
            {
                if (grvAccounts.Rows.Count < 1)
                {
                    MessageBox.Show("Please add account(s) first!", "Add Accounts", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                List<string> accountsList = new List<string>();
                foreach (DataGridViewRow gvr in grvAccounts.Rows)
                {
                    //CheckBox chekbox = (CheckBox)gvr.Cells[0].FindControl("chkSelect");
                    DataGridViewCheckBoxCell chk = gvr.Cells[0] as DataGridViewCheckBoxCell;
                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        string acc_no = gvr.Cells["hpa_acc_no"].Value.ToString();//gvr.Cells[1].Text;
                        accountsList.Add(acc_no);
                    }
                }
                if (accountsList.Count < 1)
                {
                    MessageBox.Show("Please select accounts first!", "Select Accounts", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("Are you sure to change?", "Confirm Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                //Int32 eff = CHNLSVC.Sales.Update_Flag_Bank("FLAG", ddlCategoCd.SelectedValue, accountsList);
                Int32 eff = 0;
                try
                {
                    eff = CHNLSVC.Sales.Update_Flag_Bank("FLAG", ddlCategoCd.SelectedValue.ToString(), accountsList);
                }
                catch (Exception ex)
                {
                    CHNLSVC.CloseChannel();
                }

                if (eff > 0)
                {
                    MessageBox.Show("Successfully Updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error in Updation!\nRecords are not updated.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.btnClear_Click(null, null);
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

        private void txtAddresline1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtAddresline2.Focus();
            }

        }

        private void txtAddresline2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtAddresline3.Focus();
            }

        }

        private void txtAccNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.button1_Click(null, null);
            }
        }

        private void grvAccounts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblPanel_popup_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Int32 x_position = 0;
                Int32 y_position = 0;

                x_position = this.panel5.Location.X + e.X;
                y_position = this.panel5.Location.Y + e.Y;

                this.panel5.Location = new Point(x_position, y_position);
                //this.panel_move.Location = new Point(Cursor.Position.X + e.X, Cursor.Position.Y + e.Y);
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                General.CustomerCreation _CusCre = new General.CustomerCreation(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty);
                //CUST.Show();
                _CusCre._isFromOther = true;
                _CusCre.obj_TragetTextBox = txtCustCode;
                _CusCre.ShowDialog();
                txtCustCode.Select();
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

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                General.CustomerCreation _CusCre = new General.CustomerCreation();

                _CusCre._isFromOther = true;
                _CusCre.obj_TragetTextBox = txtGurantorCd;
                _CusCre.ShowDialog();
                txtGurantorCd.Select();
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

        private void btnSearchGurantor_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtGurantorCd;
                _CommonSearch.ShowDialog();
                txtGurantorCd.Select();
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

        private void txtGurantorCd_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                MasterBusinessEntity cust = getCustomer(txtGurantorCd.Text.Trim(), 3, null);
                if (cust == null)
                {
                    cust = getCustomer(txtGurantorCd.Text.Trim(), 1, null);
                }
                if (cust != null)
                {
                    // txtAddresline1_G.Text = cust.Mbe_add1;//cust.Htc_adr_01;
                    // txtAddresline2_G.Text = cust.Mbe_add2; //cust.Htc_adr_02;
                    // txtAddresline3_G.Text = "";// cust.Htc_adr_03;
                }
                else
                {
                    //  txtAddresline1_G.Text = string.Empty;
                    //  txtAddresline2_G.Text = string.Empty;
                    // txtAddresline3_G.Text = string.Empty;
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

        private void rdoGurnantorTransfer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdoGurnantorTransfer.Checked == true)
                {
                    string _OrgPC = txtPC.Text.Trim();

                    txtAccGurantChng.Text = "";

                    //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "HP5")==false)
                    //{
                    //    MessageBox.Show("Permission Not Granted!\n( Advice: Reqired permission code :HP5 )", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;

                    //}
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10032))
                    {
                        MessageBox.Show("Permission Not Granted!\n( Advice: Reqired permission code :10032 )", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        rdoGurnantorTransfer.Checked = false;
                        return;
                    }
                    NewGurantorList = new List<MasterBusinessEntity>();

                    // TODO:
                    //CHECK FOR ONLU ONE ACCOUNT
                    List<string> accountsList = new List<string>();

                    foreach (DataGridViewRow gvr in grvAccounts.Rows)
                    {
                        DataGridViewCheckBoxCell chk = gvr.Cells[0] as DataGridViewCheckBoxCell;
                        if (Convert.ToBoolean(chk.Value) == true)
                        {
                            string acc_no = gvr.Cells["hpa_acc_no"].Value.ToString();//gvr.Cells[1].Text;
                            accountsList.Add(acc_no);
                        }
                    }

                    if (accountsList.Count > 1)
                    {
                        MessageBox.Show("Please select only one account at a time!", "Accounts", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (accountsList.Count == 0)
                    {
                        MessageBox.Show("Please select an account!", "Accounts", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearOptionsAndPanels();
                        return;
                    }
                    //--------------------------------------------------------

                    visible_panel(divGuranterTrans);

                    txtAccGurantChng.Text = accountsList[0].ToString();
                    DataTable dt = CHNLSVC.Sales.Get_gurantors(txtAccGurantChng.Text.Trim());
                    grvRemoveList.DataSource = null;
                    //grvRemoveList.AutoGenerateColumns = false;
                    grvRemoveList.DataSource = dt;

                    DataTable dt_cust = CHNLSVC.Sales.GetHpCustomer_Details(txtAccGurantChng.Text.Trim());
                    txtCustomer.Text = dt_cust.Rows[0]["MBE_CD"].ToString();
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

        private bool checkCustomer(string _com, string _identification)
        {
            try
            {
                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                BlackListCustomers _blackListCustomers = new BlackListCustomers();
                string _cusCode = "";
                Label lblInfo = new Label();
                lblInfo.Text = "";
                bool _isBlack = false;

                if (!string.IsNullOrEmpty(_identification))
                {
                    _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(_com, _identification.Trim(), string.Empty, string.Empty, "C");

                    if (_masterBusinessCompany.Mbe_cd != null)
                    {

                        _cusCode = _masterBusinessCompany.Mbe_cd;
                        _blackListCustomers = CHNLSVC.Sales.GetBlackListCustomerDetails(_cusCode, 1);

                        if (_blackListCustomers != null)
                        {
                            if (string.IsNullOrEmpty(_blackListCustomers.Hbl_com) && string.IsNullOrEmpty(_blackListCustomers.Hbl_pc))
                            {
                                lblInfo.Text = "Black listed customer." + _masterBusinessCompany.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                MessageBox.Show(lblInfo.Text, "Black List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                _isBlack = true;
                                return _isBlack;
                            }
                            else if (_blackListCustomers.Hbl_com == BaseCls.GlbUserComCode && _blackListCustomers.Hbl_pc == BaseCls.GlbUserDefProf)
                            {
                                lblInfo.Text = "Black listed customer by showroom end." + _masterBusinessCompany.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                MessageBox.Show(lblInfo.Text, "Black List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                _isBlack = false;
                                return _isBlack;
                            }
                        }
                        else
                        {
                            lblInfo.Text = "Exsisting customer.";
                            //MessageBox.Show(lblInfo.Text, "Black List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return _isBlack;
                        }
                    }
                    else
                    {
                        _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(_com, string.Empty, _identification.Trim(), string.Empty, "C");

                        if (_masterBusinessCompany.Mbe_cd != null)
                        {
                            _cusCode = _masterBusinessCompany.Mbe_cd;
                            _blackListCustomers = CHNLSVC.Sales.GetBlackListCustomerDetails(_cusCode, 1);

                            if (_blackListCustomers != null)
                            {
                                //lblInfo.Text = "Black listed customer." + _masterBusinessCompany.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                //_isBlack = true;
                                //return;
                                if (string.IsNullOrEmpty(_blackListCustomers.Hbl_com) && string.IsNullOrEmpty(_blackListCustomers.Hbl_pc))
                                {
                                    lblInfo.Text = "Black listed customer." + _masterBusinessCompany.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                    MessageBox.Show(lblInfo.Text, "Black List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    _isBlack = true;
                                    return _isBlack;
                                }
                                else if (_blackListCustomers.Hbl_com == BaseCls.GlbUserComCode && _blackListCustomers.Hbl_pc == BaseCls.GlbUserDefProf)
                                {
                                    lblInfo.Text = "Black listed customer by showroom end." + _masterBusinessCompany.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                    MessageBox.Show(lblInfo.Text, "Black List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    _isBlack = false;
                                    return _isBlack;
                                }
                            }
                            else
                            {
                                lblInfo.Text = "Exsisting customer.";
                                return _isBlack;
                            }
                        }

                        else
                        {
                            _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(_com, string.Empty, string.Empty, _identification.Trim(), "C");

                            if (_masterBusinessCompany.Mbe_cd != null)
                            {
                                _cusCode = _masterBusinessCompany.Mbe_cd;
                                _blackListCustomers = CHNLSVC.Sales.GetBlackListCustomerDetails(_cusCode, 1);

                                if (_blackListCustomers != null)
                                {
                                    //lblInfo.Text = "Black listed customer." + _masterBusinessCompany.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                    //_isBlack = true;
                                    //return;
                                    if (string.IsNullOrEmpty(_blackListCustomers.Hbl_com) && string.IsNullOrEmpty(_blackListCustomers.Hbl_pc))
                                    {
                                        lblInfo.Text = "Black listed customer." + _masterBusinessCompany.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                        MessageBox.Show(lblInfo.Text, "Black List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        _isBlack = true;
                                        return _isBlack;
                                    }
                                    else if (_blackListCustomers.Hbl_com == BaseCls.GlbUserComCode && _blackListCustomers.Hbl_pc == BaseCls.GlbUserDefProf)
                                    {
                                        lblInfo.Text = "Black listed customer by showroom end." + _masterBusinessCompany.Mbe_name + " - " + _blackListCustomers.Hbl_rmk;
                                        MessageBox.Show(lblInfo.Text, "Black List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        _isBlack = false;
                                        return _isBlack;
                                    }
                                }
                                else
                                {
                                    lblInfo.Text = "Exsisting customer.";
                                    return _isBlack;
                                }
                            }
                            else
                            {
                                lblInfo.Text = "Cannot find exsisting customer details for given identification.";
                                MessageBox.Show(lblInfo.Text, "Black List", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return _isBlack;
                            }
                        }
                        return _isBlack;
                    }
                    return _isBlack;
                }
                return _isBlack;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                return false;

            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnConfGurantorTr_Click(object sender, EventArgs e)
        {
            try
            {
                //TODO: TRANSFER GURANTOOR

                grvGurantorAddList.EndEdit();

                grvRemoveList.EndEdit();
                List<string> list_remove = new List<string>();
                foreach (DataGridViewRow dgvr in grvRemoveList.Rows)
                {
                    DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        list_remove.Add(dgvr.Cells[1].Value.ToString()); //take gurantor code
                    }
                }
                if (grvGurantorAddList.Rows.Count == 0)
                {
                    MessageBox.Show("Please add gurantor(s)!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (list_remove.Count > 0)
                {
                    if (list_remove.Count > grvGurantorAddList.Rows.Count)
                    {
                        MessageBox.Show("Please add " + (list_remove.Count - grvGurantorAddList.Rows.Count).ToString() + " more gurantor(s)!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (list_remove.Count == 0)
                {
                    MessageBox.Show("Please add gurantor(s)!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //TODO: REMOVE AND ADD
                List<HpCustomer> NewGuranter_list = new List<HpCustomer>();
                foreach (DataGridViewRow dgvr in grvGurantorAddList.Rows)
                {
                    // DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                    // if (Convert.ToBoolean(chk.Value) == true)
                    //{
                    // list_remove.Add(dgvr.Cells[1].Value.ToString()); //take gurantor code
                    string newGuranerCd = dgvr.Cells[1].Value.ToString();

                    HpCustomer newGur = new HpCustomer();
                    newGur.Htc_acc_no = txtAccGurantChng.Text.Trim();
                    //newGur.Htc_adr_01;
                    //newGur.Htc_adr_02=;
                    //newGur.Htc_adr_03 =""; 
                    //newGur.Htc_adr_tp =1 ;
                    newGur.Htc_cre_by = BaseCls.GlbUserID;
                    newGur.Htc_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;
                    newGur.Htc_cust_cd = newGuranerCd.Trim();
                    newGur.Htc_cust_tp = "G";
                    //newGur.Htc_cust_tp = "H"; // tHARINDU 
                    //newGur.Htc_seq;
                    NewGuranter_list.Add(newGur);

                    //}
                }
                if (grvRemoveList.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dgvr in grvRemoveList.Rows)
                    {
                        //DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                        //if (Convert.ToBoolean(chk.Value) == true)
                        //{
                        //    list_remove.Add(dgvr.Cells[1].Value.ToString()); //take gurantor code
                        //}
                        var _duplicate = from _dup in NewGuranter_list
                                         where _dup.Htc_cust_cd == dgvr.Cells[1].Value.ToString()// && _dup.Sccd_brd == obj.Sccd_brd
                                         select _dup;
                        if (_duplicate.Count() > 0)
                        {
                            MessageBox.Show(dgvr.Cells[1].Value.ToString() + " is already a guranter.\nPlease remove it from the newly added guranter list.", "Save Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }


                //var _duplicate = from _dup in ItemBrandCat_List
                //                 where _dup.Sccd_itm == obj.Sccd_itm && _dup.Sccd_brd == obj.Sccd_brd
                //                 select _dup;
                //if (_duplicate.Count() == 0)
                //{
                //    addList.Add(obj);
                //}   

                //----------------------------------------------------------------------------------------------------------------------
                if (MessageBox.Show("Are you sure to save ?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                Int32 eff = CHNLSVC.Sales.Add_Remove_Guranter_Of_Account(txtAccGurantChng.Text.Trim(), list_remove, NewGuranter_list);

                if (eff > 0)
                {
                    MessageBox.Show("Sucessfully Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.btnClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Not Saved. Please try again.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                //Add_Remove_Guranter_Of_Account();
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

        private void btnAddGurantor_Click(object sender, EventArgs e)
        {
            try
            {
                MasterBusinessEntity newGurantor = getCustomer(txtGurantorCd.Text.Trim(), 1, null);
                if (newGurantor.Mbe_cd == null)
                {
                    MessageBox.Show("Invalid gurantor.", "Add new", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                NewGurantorList.Add(newGurantor);
                grvGurantorAddList.DataSource = null;
                grvGurantorAddList.AutoGenerateColumns = false;
                grvGurantorAddList.DataSource = NewGurantorList;
                //grvGurantorAddList.Rows.Add();
                //grvGurantorAddList["Mbe_cd", grvGurantorAddList.Rows.Count - 1].Value = newGurantor.Mbe_cd;
                //grvGurantorAddList["Mbe_name", grvGurantorAddList.Rows.Count - 1].Value = newGurantor.Mbe_name;
                //grvGurantorAddList["Mbe_nic", grvGurantorAddList.Rows.Count - 1].Value = newGurantor.Mbe_nic;
                //grvGurantorAddList["Mbe_add1", grvGurantorAddList.Rows.Count - 1].Value = newGurantor.Mbe_add1;
                //grvGurantorAddList["Mbe_add2", grvGurantorAddList.Rows.Count - 1].Value = newGurantor.Mbe_add2;
                // grvGurantorAddList["col_GPreAdd1", grvGurantorAddList.Rows.Count - 1].Value = newGurantor.Mbe_cr_add1;
                // grvGurantorAddList["col_GPreAdd2", grvGurantorAddList.Rows.Count - 1].Value = newGurantor.Mbe_cr_add2;

                //cust.Mbe_cd;
                //cust.Mbe_add1;
                //cust.Mbe_add2;
                //cust.Mbe_contact;
                //cust.Mbe_name;
                //cust.Mbe_nic;
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

        private void btnAddNewGurantor_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtGurantorCd.Text.Trim() == "")
                {
                    MessageBox.Show("Select guranter code first!", "Add new", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                MasterBusinessEntity newGurantor = getCustomer(txtGurantorCd.Text.Trim(), 1, null);
                if (newGurantor.Mbe_cd == null)
                {
                    MessageBox.Show("Invalid gurantor.", "Add new", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //bool _isBlack = checkCustomer(null, txtGurantorCd.Text.Trim());
                //if (_isBlack == true)
                //{
                //    MessageBox.Show("Above gurantor is black listed.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    //txtGurantorCd.Text = "";
                //    return;
                //}
                BlackListCustomers black_g = CHNLSVC.Sales.GetBlackListCustomerDetails(txtGurantorCd.Text.Trim(), 1);
                if (black_g != null)
                {
                    if (black_g.Hbl_cust_cd == txtGurantorCd.Text.Trim())
                    {
                        MessageBox.Show("This gurantor is black listed!", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (txtCustomer.Text.Trim() == txtGurantorCd.Text.Trim())
                {
                    MessageBox.Show("Customer & gurantor cannot be same.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGurantorCd.Focus();
                    return;
                }

                NewGurantorList.Add(newGurantor);
                grvGurantorAddList.DataSource = null;
                grvGurantorAddList.AutoGenerateColumns = false;
                grvGurantorAddList.DataSource = NewGurantorList;
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

        private void grvGurantorAddList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            { //REMOVE FROM THE LIST            e.RowIndex
                NewGurantorList.RemoveAll(x => x.Mbe_cd == grvGurantorAddList.Rows[e.RowIndex].Cells["Mbe_cd"].Value.ToString());
                grvGurantorAddList.DataSource = null;
                grvGurantorAddList.AutoGenerateColumns = false;
                grvGurantorAddList.DataSource = NewGurantorList;
            }
        }

        private void linkBtnNewGuarantor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                //General.CustomerCreation CUST = new General.CustomerCreation(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty);
                //CUST.Show();
                General.CustomerCreation _CusCre = new General.CustomerCreation(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty);
                //CUST.Show();
                _CusCre._isFromOther = true;
                _CusCre.obj_TragetTextBox = txtGurantorCd;
                _CusCre.ShowDialog();
                txtGurantorCd.Select();
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

        private void btnSearchAccActPc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAccActPc;
                _CommonSearch.ShowDialog();
                txtAccActPc.Focus();
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

        private void txtAccActPc_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnSearchAccActPc_Click(null, null);
        }

        private void txtAccActPc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtClsAcc.Select();
            }
            else if (e.KeyCode == Keys.F2)
            {
                this.btnSearchAccActPc_Click(null, null);
            }
        }

        private void txtAccActPc_Leave(object sender, EventArgs e)
        {
            try
            {
                return;
                if (!string.IsNullOrEmpty(txtAccActPc.Text))
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                    DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(SearchParams, null, null);

                    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtAccActPc.Text.Trim()).ToList();
                    if (_validate == null || _validate.Count <= 0)
                    {
                        MessageBox.Show("Invalid profit center.", "Account Activation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtAccActPc.Clear();
                        txtAccActPc.Focus();
                        return;
                    }
                }

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchClsAcc_Click(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(txtAccActPc.Text))
                {
                    MessageBox.Show("Please select account related profit center.", "Account Activation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAccActPc.Clear();
                    txtAccActPc.Focus();
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAcc4Act);
                DataTable _result = CHNLSVC.CommonSearch.GetHpAcc4ActiveSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtClsAcc;
                _CommonSearch.ShowDialog();
                txtClsAcc.Focus();

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

        private void txtClsAcc_DoubleClick(object sender, EventArgs e)
        {
            this.btnSearchClsAcc_Click(null, null);
        }

        private void txtClsAcc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnActive.Select();
            }
            else if (e.KeyCode == Keys.F2)
            {
                this.btnSearchClsAcc_Click(null, null);
            }
        }

        private void txtClsAcc_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtClsAcc.Text)) return;

                if (string.IsNullOrEmpty(txtAccActPc.Text))
                {
                    MessageBox.Show("Please select account related profit center.", "Account Activation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAccActPc.Clear();
                    txtAccActPc.Focus();
                    return;
                }

                HpAccount _acc = new HpAccount();
                _acc = CHNLSVC.Sales.GetHP_Account_onAccNo(txtClsAcc.Text);

                if (_acc == null)
                {
                    MessageBox.Show("Please select valid account.", "Account Activation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtClsAcc.Text = "";
                    txtClsAcc.Focus();
                    return;
                }

                if (_acc.Hpa_pc != txtAccActPc.Text.Trim())
                {
                    MessageBox.Show("Selected account is not related the selected profit center.", "Account Activation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtClsAcc.Text = "";
                    txtClsAcc.Focus();
                    return;
                }

                if (_acc.Hpa_cls_dt == Convert.ToDateTime("31-DEC-9999"))
                /*if (_acc.Hpa_stus != "C" && _acc.Hpa_stus != "T")*/
                {
                    MessageBox.Show("Selected account is not a close account.", "Account Activation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtClsAcc.Text = "";
                    txtClsAcc.Focus();
                    return;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            try
            {
                string _error = "";
                if (string.IsNullOrEmpty(txtAccActPc.Text))
                {
                    MessageBox.Show("Please select profit center.", "Account Activation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAccActPc.Text = "";
                    txtAccActPc.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtClsAcc.Text))
                {
                    MessageBox.Show("Please select account no.", "Account Activation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtClsAcc.Text = "";
                    txtClsAcc.Focus();
                    return;
                }


                
                if (MessageBox.Show("Are you sure to active this account?", "Confirm Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                
                Int32 eff = 0;
                
                try
                {
                    eff = CHNLSVC.Sales.UpdateHpAccActive(BaseCls.GlbUserComCode, txtAccActPc.Text.Trim(), txtClsAcc.Text.Trim(), Convert.ToDateTime("31-Dec-9999").Date, out _error);
                }
                catch (Exception ex)
                {
                    CHNLSVC.CloseChannel();
                }

                if (eff > 0)
                {
                    MessageBox.Show("Successfully Updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error in Updation!\nRecords are not updated.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
               this.btnClear_Click(null, null);

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

        private void rdoClsAccAct_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdoClsAccAct.Checked == true)
                {
                    
                    if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10079))
                    {
                        visible_panel(divClsAccAct);
                        txtAccActPc.Text = BaseCls.GlbUserDefProf;
                    }
                    else
                    {
                        MessageBox.Show("Permission Not Granted!\n( Advice: Reqired permission code :10079 )", "Option Permission", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        clearOptionsAndPanels();
                        return;
                    }

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

        private void txtMan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_man_Click(null, null);
        }

        private void txtMan_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_man_Click(null, null);
        }

        private void rdoManTrans_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdoManTrans.Checked == true)
                {

                    string _OrgPC = txtPC.Text.Trim();
                    if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10132))
                    {
                        visible_panel(divManTrans);
                    }
                    else
                    {
                        MessageBox.Show("Permission Not Granted!\n( Advice: Reqired permission code :10132 )", "Option Permission", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        clearOptionsAndPanels();
                        return;
                    }
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

        private void btnConfManTrans_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10136))
                {

                    MessageBox.Show("Permission Not Granted!\n( Advice: Reqired permission code :10136 )", "Option Permission", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (grvAccounts.Rows.Count < 1)
                {
                    MessageBox.Show("Please add account(s) first!", "Add Accounts", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                txtTrPC.Text = txtTrPC.Text.Trim().ToUpper();
                DataTable DT = CHNLSVC.General.GetPartyCodes(BaseCls.GlbUserComCode, txtTrPC.Text.Trim().ToUpper());
                if (DT == null)
                {
                    MessageBox.Show("Invalid Profit center!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTrPC.Focus();
                    return;
                }
                else if (DT.Rows.Count < 1)
                {
                    MessageBox.Show("Invalid Profit center!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTrPC.Focus();
                    return;
                }
                List<HpAccount> accountsList = new List<HpAccount>();
                foreach (DataGridViewRow gvr in grvAccounts.Rows)
                {
                    DataGridViewCheckBoxCell chk = gvr.Cells[0] as DataGridViewCheckBoxCell;
                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        HpAccount _obj = new HpAccount();
                        _obj.Hpa_acc_no = gvr.Cells["hpa_acc_no"].Value.ToString();//gvr.Cells[1].Text;
                        _obj.Hpa_mgr_cd = gvr.Cells["hpa_mgr_cd"].Value.ToString();
                        _obj.Hpa_com = BaseCls.GlbUserComCode;
                        _obj.Hpa_pc = gvr.Cells["hal_pc"].Value.ToString();
                        accountsList.Add(_obj);
                    }
                }
                if (accountsList.Count < 1)
                {
                    MessageBox.Show("Please select account(s) first!", "Select Accounts", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("Are you sure to update?", "Confirm update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                Int32 eff = 0;
                try
                {
                    eff = CHNLSVC.Sales.UpdateAccountManager( accountsList,txtMan.Text,BaseCls.GlbUserID);
                }
                catch (Exception ex)
                {
                    CHNLSVC.CloseChannel();
                }

                if (eff > 0)
                {
                    MessageBox.Show("Succsessfully Updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error Occured. Failed Update!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.btnClear_Click(null, null);
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


    }
}
