using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Data;
using System.Text;

namespace FF.WebERPClient.HP_Module
{
    public partial class ManulReminder : BasePage
    {
        #region properties

        public string AccNo {
            get { return (string)ViewState["AccNo"]; }
            set { ViewState["AccNo"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                AccNo = "";
                TextBoxReminderDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                TextBoxDueDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            }
        }

        protected void ImgBtnAcc_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
            DataTable dataSource = CHNLSVC.CommonSearch.GetHpAccountSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxAccNo.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();

        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + "A" + seperator);
                        break;
                    }           
                default:
                    break;
            }
            return paramsText.ToString();
        }

        protected void LinkButtonTemp_Click(object sender, EventArgs e)
        {
            if (TextBoxAccNo.Text != "" && TextBoxDueDate.Text != "")
            {
                string location = GlbUserDefProf;
                string acc_seq = TextBoxAccNo.Text.Trim();
                List<HpAccount> accList = new List<HpAccount>();
                accList = CHNLSVC.Sales.GetHP_Accounts(GlbUserComCode, location, acc_seq, "A");
                if (accList!=null )
                {
                    if (accList.Count == 1)
                    {
                        AccNo = accList[0].Hpa_acc_no;
                        uc_HpAccountSummary1.set_all_values(accList[0], GlbUserDefLoca, Convert.ToDateTime(TextBoxDueDate.Text).Date, GlbUserDefLoca);
                        uc_HpAccountDetail1.Uc_hpa_acc_no = accList[0].Hpa_acc_no;
                        TextBoxAccBal.Text = CHNLSVC.Sales.Get_AccountBalance(Convert.ToDateTime(TextBoxDueDate.Text).Date, accList[0].Hpa_acc_no).ToString();

                        Hp_AccountSummary summary = new Hp_AccountSummary();
                        DateTime ars_date;
                        DateTime sup_date;
                        Hp_AccountSummary.get_ArearsDate_SupDate(GlbUserDefProf, Convert.ToDateTime(TextBoxDueDate.Text), out ars_date, out sup_date);

                        Decimal arrears = Hp_AccountSummary.getArears(AccNo, summary, GlbUserDefProf, Convert.ToDateTime(TextBoxDueDate.Text), ars_date, sup_date);

                        TextBoxArrears.Text = arrears.ToString();
                        //TextBoxArrears.Text = CHNLSVC.Sales.Get_hp_ArrearsSettlement(accList[0].Hpa_acc_no, Convert.ToDateTime(TextBoxDueDate.Text).Date).ToString();
                        LoadItemDetails(accList[0]);
                        LoadGuarantor(accList[0]);
                    }
                    else {
                        //show a pop up to select the account number
                        grvMpdalPopUp.DataSource = accList;
                        grvMpdalPopUp.DataBind();
                        ModalPopupExtItem.Show();
                    }
                }
                else {
                    AccNo = "";
                    uc_HpAccountDetail1.Clear();
                    uc_HpAccountSummary1.Clear();
                    TextBoxAccBal.Text = "";
                    TextBoxArrears.Text = "";
                }
            }
        }

        private void LoadGuarantor(HpAccount hpAccount)
        {
            GridViewGurantor.DataSource = CHNLSVC.Sales.GetHPCustomer(GlbUserComCode, GlbUserDefProf, DateTime.MinValue, DateTime.MaxValue, "Acc_Gurantor", hpAccount.Hpa_acc_no, DateTime.Now, DateTime.Now, DateTime.Now);
            GridViewGurantor.DataBind();
        }

        private void LoadItemDetails(HpAccount hpAccount)
        {
            GridViewItem.DataSource = CHNLSVC.Sales.GetInvoiceDetailByInvoice(hpAccount.Hpa_invc_no);
            GridViewItem.DataBind();
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            #region validation

            if (TextBoxReminderDate.Text == "") {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Pleasee enter reminder date");
                return;
            }
            if (TextBoxDueDate.Text == "") {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter due date");
                return;
            }
            string location = GlbUserDefProf;
            string acc_seq = TextBoxAccNo.Text.Trim();
            List<HpAccount> accList = new List<HpAccount>();
            accList = CHNLSVC.Sales.GetHP_Accounts(GlbUserComCode, location, acc_seq, "A");
            if (TextBoxAccNo.Text != "")
            {
                if (accList == null) {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid account no");
                    return;
                }
            }
            else {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter account no");
                return;
            }


            #endregion

            //find existing records

            int count = CHNLSVC.Sales.GetReminderLetterCount(accList[0].Hpa_acc_no, GlbUserDefProf, GlbUserComCode, DropDownListRemindeType.SelectedValue, Convert.ToDateTime(TextBoxDueDate.Text));
            if (count > 0)
            {
                //show message
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", "ShowConfirm()", true);
            }
            else
            {

                ReminderLetter _ltr = new ReminderLetter();
                _ltr.Hrl_acc_no = AccNo;
                _ltr.Hrl_ars = Convert.ToDecimal(TextBoxArrears.Text);
                _ltr.Hrl_bal = Convert.ToDecimal(TextBoxAccBal.Text);
                _ltr.Hrl_com = GlbUserComCode;
                _ltr.Hrl_cre_by = GlbUserName;
                _ltr.Hrl_cre_dt = DateTime.Now;
                _ltr.Hrl_dt = Convert.ToDateTime(TextBoxReminderDate.Text);
                _ltr.Hrl_due_dt = Convert.ToDateTime(TextBoxDueDate.Text);
                _ltr.Hrl_no_prt = 1;
                _ltr.Hrl_medium = "E";
                _ltr.Hrl_pc = GlbUserDefProf;
                _ltr.Hrl_rmk = TextBoxRmk.Text;
                _ltr.Hrl_rnt = CHNLSVC.Sales.Get_MonthlyRental( Convert.ToDateTime(TextBoxDueDate.Text),AccNo);
                _ltr.Hrl_tp = DropDownListRemindeType.SelectedValue;

                int result = CHNLSVC.Sales.SaveHPReminderLetter(_ltr);
                if (result > 0)
                {
                    if (DropDownListRemindeType.SelectedValue == "R")
                    {
                        GlbReportPath = "~\\Reports_Module\\Sales_Rep\\Reminder.rpt";
                        GlbReportMapPath = "~/Reports_Module/Sales_Rep/Reminder.rpt";
                        GlbSelectionFormula = "{HPT_ACC.HPA_ACC_NO} = '" + AccNo + "' AND {HPT_RMD_LTT.HRL_TP}='R'  AND {HPT_RMD_LTT.HRL_DUE_DT}= DateTime(" + Convert.ToDateTime(TextBoxDueDate.Text).Year + "," + Convert.ToDateTime(TextBoxDueDate.Text).Month + "," + Convert.ToDateTime(TextBoxDueDate.Text).Day + ",00,00,00)";
                        string Msg1 = "<script>window.open('../Reports_Module/Sales_Rep/ReminderPrint.aspx','_blank');</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);
                    }
                    if (DropDownListRemindeType.SelectedValue == "FR")
                    {
                        GlbReportPath = "~\\Reports_Module\\Sales_Rep\\FinalReminder.rpt";
                        GlbReportMapPath = "~/Reports_Module/Sales_Rep/FinalReminder.rpt";
                        GlbSelectionFormula = "{HPT_ACC.HPA_ACC_NO} = '" + AccNo + "'  AND {HPT_RMD_LTT.HRL_TP}='FR'  AND {HPT_RMD_LTT.HRL_DUE_DT}= DateTime(" + Convert.ToDateTime(TextBoxDueDate.Text).Year + "," + Convert.ToDateTime(TextBoxDueDate.Text).Month + "," + Convert.ToDateTime(TextBoxDueDate.Text).Day + ",00,00,00)";
                        string Msg1 = "<script>window.open('../Reports_Module/Sales_Rep/FinalReminderPrint.aspx','_blank');</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);
                    }
                    if (DropDownListRemindeType.SelectedValue == "AC")
                    {
                        GlbReportPath = "~\\Reports_Module\\Sales_Rep\\AccountClose.rpt";
                        GlbReportMapPath = "~/Reports_Module/Sales_Rep/AccountClose.rpt";
                        GlbSelectionFormula = "{HPT_ACC.HPA_ACC_NO} = '" + AccNo + "'  AND {HPT_RMD_LTT.HRL_TP}='AC'  AND {HPT_RMD_LTT.HRL_DUE_DT}= DateTime(" + Convert.ToDateTime(TextBoxDueDate.Text).Year + "," + Convert.ToDateTime(TextBoxDueDate.Text).Month + "," + Convert.ToDateTime(TextBoxDueDate.Text).Day + ",00,00,00)";
                        string Msg1 = "<script>window.open('../Reports_Module/Sales_Rep/AccountClosePrint.aspx','_blank');</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);
                    }
                    if (DropDownListRemindeType.SelectedValue == "A")
                    {
                        GlbReportPath = "~\\Reports_Module\\Sales_Rep\\HPArrears.rpt";
                        GlbReportMapPath = "~/Reports_Module/Sales_Rep/HPArrears.rpt";
                        GlbSelectionFormula = "{HPT_RMD_LTT.HRL_ACC_NO} = '" + AccNo + "'  AND {HPT_RMD_LTT.HRL_TP}='A'  AND {HPT_RMD_LTT.HRL_DUE_DT}= DateTime(" + Convert.ToDateTime(TextBoxDueDate.Text).Year + "," + Convert.ToDateTime(TextBoxDueDate.Text).Month + "," + Convert.ToDateTime(TextBoxDueDate.Text).Day + ",00,00,00)";
                        string Msg1 = "<script>window.open('../Reports_Module/Sales_Rep/HPArrearsPrint.aspx','_blank');</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);
                    }
                }
            }
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("../HP_Module/ManulReminder.aspx");
        }

        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

        protected void LinkButtonTemp1_Click(object sender, EventArgs e)
        {
            ReminderLetter _ltr = new ReminderLetter();
            _ltr.Hrl_acc_no = AccNo;
            _ltr.Hrl_ars = Convert.ToDecimal(TextBoxArrears.Text);
            _ltr.Hrl_bal = Convert.ToDecimal(TextBoxAccBal.Text);
            _ltr.Hrl_com = GlbUserComCode;
            _ltr.Hrl_cre_by = GlbUserName;
            _ltr.Hrl_cre_dt = DateTime.Now;
            _ltr.Hrl_dt = Convert.ToDateTime(TextBoxReminderDate.Text);
            _ltr.Hrl_due_dt = Convert.ToDateTime(TextBoxDueDate.Text);
            _ltr.Hrl_no_prt = 1;
            _ltr.Hrl_medium = "E";
            _ltr.Hrl_pc = GlbUserDefProf;
            _ltr.Hrl_rmk = TextBoxRmk.Text;
            _ltr.Hrl_rnt = CHNLSVC.Sales.Get_hp_ArrearsSettlement(AccNo, Convert.ToDateTime(TextBoxDueDate.Text));
            _ltr.Hrl_tp = DropDownListRemindeType.SelectedValue;

            int result = CHNLSVC.Sales.SaveHPReminderLetter(_ltr);
            if (result > 0)
            {
                if (DropDownListRemindeType.SelectedValue == "R")
                {
                    GlbReportPath = "~\\Reports_Module\\Sales_Rep\\Reminder.rpt";
                    GlbReportMapPath = "~/Reports_Module/Sales_Rep/Reminder.rpt";
                    GlbSelectionFormula = "{HPT_ACC.HPA_ACC_NO} = '" + AccNo + "' AND {HPT_RMD_LTT.HRL_TP}='R'  AND {HPT_RMD_LTT.HRL_DUE_DT}= DateTime(" + Convert.ToDateTime(TextBoxDueDate.Text).Year + "," + Convert.ToDateTime(TextBoxDueDate.Text).Month + "," + Convert.ToDateTime(TextBoxDueDate.Text).Day + ",00,00,00)";
                    string Msg1 = "<script>window.open('../Reports_Module/Sales_Rep/ReminderPrint.aspx','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);
                }
                if (DropDownListRemindeType.SelectedValue == "FR")
                {
                    GlbReportPath = "~\\Reports_Module\\Sales_Rep\\FinalReminder.rpt";
                    GlbReportMapPath = "~/Reports_Module/Sales_Rep/FinalReminder.rpt";
                    GlbSelectionFormula = "{HPT_ACC.HPA_ACC_NO} = '" + AccNo + "'  AND {HPT_RMD_LTT.HRL_TP}='FR'  AND {HPT_RMD_LTT.HRL_DUE_DT}= DateTime(" + Convert.ToDateTime(TextBoxDueDate.Text).Year + "," + Convert.ToDateTime(TextBoxDueDate.Text).Month + "," + Convert.ToDateTime(TextBoxDueDate.Text).Day + ",00,00,00)";
                    string Msg1 = "<script>window.open('../Reports_Module/Sales_Rep/FinalReminderPrint.aspx','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);
                }
                if (DropDownListRemindeType.SelectedValue == "AC")
                {
                    GlbReportPath = "~\\Reports_Module\\Sales_Rep\\AccountClose.rpt";
                    GlbReportMapPath = "~/Reports_Module/Sales_Rep/AccountClose.rpt";
                    GlbSelectionFormula = "{HPT_ACC.HPA_ACC_NO} = '" + AccNo + "'  AND {HPT_RMD_LTT.HRL_TP}='AC'  AND {HPT_RMD_LTT.HRL_DUE_DT}= DateTime(" + Convert.ToDateTime(TextBoxDueDate.Text).Year + "," + Convert.ToDateTime(TextBoxDueDate.Text).Month + "," + Convert.ToDateTime(TextBoxDueDate.Text).Day + ",00,00,00)";
                    string Msg1 = "<script>window.open('../Reports_Module/Sales_Rep/AccountClosePrint.aspx','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);
                }
                if (DropDownListRemindeType.SelectedValue == "A")
                {
                    GlbReportPath = "~\\Reports_Module\\Sales_Rep\\HPArrears.rpt";
                    GlbReportMapPath = "~/Reports_Module/Sales_Rep/HPArrears.rpt";
                    GlbSelectionFormula = "{HPT_RMD_LTT.HRL_ACC_NO} = '" + AccNo + "'  AND {HPT_RMD_LTT.HRL_TP}='A'  AND {HPT_RMD_LTT.HRL_DUE_DT}= DateTime(" + Convert.ToDateTime(TextBoxDueDate.Text).Year + "," + Convert.ToDateTime(TextBoxDueDate.Text).Month + "," + Convert.ToDateTime(TextBoxDueDate.Text).Day + ",00,00,00)";
                    string Msg1 = "<script>window.open('../Reports_Module/Sales_Rep/HPArrearsPrint.aspx','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);
                }
            }

        }

        protected void processForGettingArrears(string pc, out DateTime ars_dt, out DateTime sup_dt)
        {
            ars_dt = DateTime.MinValue.Date;
            sup_dt = DateTime.MinValue.Date;
            Hp_AccountSummary SUMMARY = new Hp_AccountSummary();
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
                    info_tbl = SUMMARY.getArrearsInfo(party_tp, party_cd, Convert.ToDateTime(TextBoxDueDate.Text));//returns one row
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

        protected void grvMpdalPopUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grvMpdalPopUp.SelectedRow;
            string accountNo = row.Cells[1].Text.Trim();
            HpAccount acc = CHNLSVC.Sales.GetHP_Account_onAccNo(accountNo);
            uc_HpAccountSummary1.set_all_values(acc, GlbUserDefLoca, Convert.ToDateTime(TextBoxDueDate.Text).Date, GlbUserDefLoca);
            uc_HpAccountDetail1.Uc_hpa_acc_no = acc.Hpa_acc_no;
            TextBoxAccBal.Text = CHNLSVC.Sales.Get_AccountBalance(Convert.ToDateTime(TextBoxDueDate.Text).Date, acc.Hpa_acc_no).ToString();

            Hp_AccountSummary summary = new Hp_AccountSummary();
            DateTime ars_date;
            DateTime sup_date;
            Hp_AccountSummary.get_ArearsDate_SupDate(GlbUserDefProf, Convert.ToDateTime(TextBoxDueDate.Text), out ars_date, out sup_date);

            Decimal arrears = Hp_AccountSummary.getArears(AccNo, summary, GlbUserDefProf, Convert.ToDateTime(TextBoxDueDate.Text), ars_date, sup_date);

            TextBoxArrears.Text = arrears.ToString();
            //TextBoxArrears.Text = CHNLSVC.Sales.Get_hp_ArrearsSettlement(accList[0].Hpa_acc_no, Convert.ToDateTime(TextBoxDueDate.Text).Date).ToString();
            LoadItemDetails(acc);
            LoadGuarantor(acc);
        }

    }
}