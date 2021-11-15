using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FF.BusinessObjects;
using System.Transactions;

namespace FF.WebERPClient.HP_Module
{
    public partial class HPAdjustment : BasePage
    {
        HpAdjustment _MainAdj = new HpAdjustment();
        MasterAutoNumber  _MainAdjAuto = new MasterAutoNumber();
        HpTransaction _MainTrans = new HpTransaction();
        MasterAutoNumber _MainTxnAuto = new MasterAutoNumber();
        HpAdjustment _OthAdj = new HpAdjustment();
        MasterAutoNumber _OthAdjAuto = new MasterAutoNumber();
        HpTransaction _OthTrans = new HpTransaction();
        MasterAutoNumber _OthTxnAuto = new MasterAutoNumber();

        protected HPAdjustmentTypes _AdjTypeDef
        {
            get { return (HPAdjustmentTypes)ViewState["_AdjTypeDef"]; }
            set { ViewState["_AdjTypeDef"] = value; }
        }

        protected List<HpAccount> _AccountsList
        {
            get { return (List<HpAccount>)ViewState["_AccountsList"]; }
            set { ViewState["_AccountsList"] = value; }
        }

        protected Boolean _isMainAcc
        {
            get { return (Boolean)ViewState["_isMainAcc"]; }
            set { ViewState["_isMainAcc"] = value; }
        }

        protected Boolean _isMultiAcc
        {
            get { return (Boolean)ViewState["_isMultiAcc"]; }
            set { ViewState["_isMultiAcc"] = value; }
        }

        //protected HpAdjustment _MainAdj
        //{
        //    get { return (HpAdjustment)ViewState["_MainAdj"]; }
        //    set { ViewState["_MainAdj"] = value; }
        //}

        //protected MasterAutoNumber _MainAdjAuto
        //{
        //    get { return (MasterAutoNumber)ViewState["_MainAdjAuto"]; }
        //    set { ViewState["_MainAdjAuto"] = value; }
        //}

        //protected HpTransaction _MainTrans
        //{
        //    get { return (HpTransaction)ViewState["_MainTrans"]; }
        //    set { ViewState["_MainTrans"] = value; }
        //}

        //protected MasterAutoNumber _MainTxnAuto
        //{
        //    get { return (MasterAutoNumber)ViewState["_MainTxnAuto"]; }
        //    set { ViewState["_MainTxnAuto"] = value; }
        //}

        //protected HpAdjustment _OthAdj
        //{
        //    get { return (HpAdjustment)ViewState["_OthAdj"]; }
        //    set { ViewState["_OthAdj"] = value; }
        //}

        //protected MasterAutoNumber _OthAdjAuto
        //{
        //    get { return (MasterAutoNumber)ViewState["_OthAdjAuto"]; }
        //    set { ViewState["_OthAdjAuto"] = value; }
        //}

        //protected HpTransaction _OthTrans
        //{
        //    get { return (HpTransaction)ViewState["_OthTrans"]; }
        //    set { ViewState["_OthTrans"] = value; }
        //}

        //protected MasterAutoNumber _OthTxnAuto
        //{
        //    get { return (MasterAutoNumber)ViewState["_OthTxnAuto"]; }
        //    set { ViewState["_OthTxnAuto"] = value; }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Enter key
                if (txtCredit.Enabled == true)
                {
                    txtManualRef.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtCredit.ClientID + "').focus();return false;}} else {return true}; ");
                }
                else
                {
                    txtManualRef.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtDebit.ClientID + "').focus();return false;}} else {return true}; ");
                }

               
                txtOthManual.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtOthCrAmt.ClientID + "').focus();return false;}} else {return true}; ");
                

                //onblur
                txtAccountNo.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btn_validateAcc, ""));
                txtOthAcc.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btn_validateOthAcc, ""));
                txtCredit.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnCrAmt, ""));
                txtDebit.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnDrAmt, ""));
                txtOthCrAmt.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnOthCrAmt, ""));
                txtOthDeAmt.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnOthDrAmt, ""));

                //text boxes upper case set
                txtManualRef.Attributes.Add("onkeypress", "uppercase();");
                txtOthManual.Attributes.Add("onkeypress", "uppercase();");
                txtRematks.Attributes.Add("onkeypress", "uppercase();");
                txtOthRemarks.Attributes.Add("onkeypress", "uppercase();");

                this.Clear_Data();
                _AdjTypeDef = new HPAdjustmentTypes();
                _MainAdj = new HpAdjustment();
                _MainAdjAuto = new MasterAutoNumber();
                _MainTrans = new HpTransaction();
                _MainTxnAuto = new MasterAutoNumber();
                _OthAdj = new HpAdjustment();
                _OthAdjAuto = new MasterAutoNumber();
                _OthTrans = new HpTransaction();
                _OthTxnAuto = new MasterAutoNumber();
                _AccountsList = new List<HpAccount>();
                _isMainAcc = false;
                _isMultiAcc = false;
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            this.Clear_Data();
        }

        //clear all details
        protected void Clear_Data()
        {
            txtDate.Text = Convert.ToString(DateTime.Today.ToShortDateString());
            pnlOtherAcc.Enabled = false;
            pnlMainAcc.Enabled = false;
            _isMultiAcc = false;
            _isMainAcc = false;
            uc_HpAccountSummary1.Clear();
            uc_HpAccountSummary2.Clear();
            uc_HpAccountDetail1.Clear();
            uc_HpAccountDetail2.Clear();

            txtAccountNo.Text = "";
            txtManualRef.Text = "";
            txtCredit.Text = "0.00";
            txtDebit.Text = "0.00";
            txtRematks.Text = "";
            txtOthAcc.Text = "";
            txtOthCrAmt.Text = "0.00";
            txtOthDeAmt.Text = "0.00";
            txtOthManual.Text = "";
            txtOthRemarks.Text = "";
            lblAccountNo.Text = "";
            lblOthAcc.Text = "";

            ddlType.Items.Clear();
            ddlType.Items.Add(new ListItem("-Select type-", "-1"));
            List<HPAdjustmentTypes> _adjAllType = CHNLSVC.Sales.GetHPAdjTypes();
            if (_adjAllType != null)
            {
                foreach (HPAdjustmentTypes list in _adjAllType)
                {
                    ddlType.Items.Add(new ListItem(list.Hajt_adj_desc, list.Hajt_adj_cd));
                }
            }

            //ddlType.DataSource = CHNLSVC.Sales.GetHPAdjTypes();
            //ddlType.DataTextField = "hajt_adj_desc";
            //ddlType.DataValueField = "hajt_adj_cd";
            //ddlType.DataBind();
        }

        //clear Master account details
        protected void ClearMaster_details()
        {
            uc_HpAccountSummary1.Clear();
            uc_HpAccountDetail1.Clear();
            txtAccountNo.Text = "";
            txtManualRef.Text = "";
            txtCredit.Text = "0.00";
            txtDebit.Text = "0.00";
            txtRematks.Text = "";
            lblAccountNo.Text = "";
            txtAccountNo.Focus();
        }

        //Clear other account details
        protected void ClearOther_details()
        {
            uc_HpAccountSummary2.Clear();
            uc_HpAccountDetail2.Clear();
            txtOthAcc.Text = "";
            txtOthCrAmt.Text = "0.00";
            txtOthDeAmt.Text = "0.00";
            txtOthManual.Text = "";
            txtOthRemarks.Text = "";
            lblOthAcc.Text = "";
            txtOthAcc.Focus();
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {

            _AdjTypeDef = CHNLSVC.Sales.GetHPAdjByCode(ddlType.SelectedValue.ToString());
            this.ClearOther_details();
            this.ClearMaster_details();
           

            if (_AdjTypeDef.Hajt_adj_cd != null)
            {
                //if multiple account allow then enable other account details section
                if (_AdjTypeDef.Hajt_mult_acc == true)
                {
                    pnlMainAcc.Enabled = true;
                    pnlOtherAcc.Enabled = true;
                    _isMultiAcc = true;
                }
                else
                {
                    pnlMainAcc.Enabled = true;
                    pnlOtherAcc.Enabled = false;
                    _isMultiAcc = false;
                }

                //check and enable debit and credit amount
                if (_AdjTypeDef.Hajt_adj_tp == 1)
                {
                    txtCredit.Enabled = true;
                    txtOthCrAmt.Enabled = true;
                    txtDebit.Enabled = false;
                    txtOthDeAmt.Enabled = false;
                }
                else if (_AdjTypeDef.Hajt_adj_tp == 2)
                {
                    txtCredit.Enabled = false;
                    txtOthCrAmt.Enabled = false;
                    txtDebit.Enabled = true;
                    txtOthDeAmt.Enabled = true;
                }
                else if (_AdjTypeDef.Hajt_adj_tp == 3)
                {
                    txtCredit.Enabled = true;
                    txtOthCrAmt.Enabled = true;
                    txtDebit.Enabled = true;
                    txtOthDeAmt.Enabled = true;
                }
                else
                {
                    txtCredit.Enabled = false;
                    txtOthCrAmt.Enabled = false;
                    txtDebit.Enabled = false;
                    txtOthDeAmt.Enabled = false;
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Incorrect adjustment type. Adj. type value :" + _AdjTypeDef.Hajt_adj_tp);
                    return;
                }
            }
            else
            {
               pnlMainAcc.Enabled = false;
               pnlOtherAcc.Enabled = false;
            }
        }



        protected void btnDrAmt_Click(object sender, EventArgs e)
        {
            decimal _DrAmt = 0;
            if (Convert.ToDecimal(txtDebit.Text) > 0)
            {
                _DrAmt = Convert.ToDecimal(txtDebit.Text);
                txtDebit.Text = _DrAmt.ToString("0.00");
                if (_isMultiAcc == true)
                {
                    txtOthCrAmt.Text = txtDebit.Text;
                }
            }
            txtRematks.Focus();
        }

        protected void btnOthDrAmt_Click(object sender, EventArgs e)
        {
            decimal _OthDrAmt = 0;
            if (Convert.ToDecimal(txtOthDeAmt.Text) > 0)
            {
                _OthDrAmt = Convert.ToDecimal(txtOthDeAmt.Text);
                txtOthDeAmt.Text = _OthDrAmt.ToString("0.00");
            }
            txtOthRemarks.Focus();
        }

        protected void btnOthCrAmt_Click(object sender,EventArgs e)
        {
            decimal _OthCrAmt = 0;
            if (Convert.ToDecimal(txtOthCrAmt.Text) > 0)
            {
                _OthCrAmt = Convert.ToDecimal(txtOthCrAmt.Text);
                txtOthCrAmt.Text = _OthCrAmt.ToString("0.00");
                
                txtOthRemarks.Focus();
            }
            else
            {
                if (txtOthDeAmt.Enabled == true)
                {
                    txtOthDeAmt.Focus();
                }
                else
                {
                    txtOthRemarks.Focus();
                }
            }
        }

        protected void btnCrAmt_Click(object sender, EventArgs e)
        {
            decimal _CrAmt = 0;
            if (Convert.ToDecimal(txtCredit.Text) > 0)
            {
                _CrAmt = Convert.ToDecimal(txtCredit.Text);
                txtCredit.Text = _CrAmt.ToString("0.00");
                if (_isMultiAcc == true)
                {
                    txtOthDeAmt.Text = txtCredit.Text;
                }
                txtRematks.Focus();
            }
            else
            {
                if (txtDebit.Enabled == true)
                {
                    txtDebit.Focus();
                }
                else
                {
                    txtRematks.Focus();
                }
            }
        }

        protected void btn_validateAcc_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAccountNo.Text))
            {

                string acc_seq = txtAccountNo.Text.Trim();
                List<HpAccount> accList = new List<HpAccount>();
                accList = CHNLSVC.Sales.GetHP_Accounts(GlbUserComCode, GlbUserDefProf, acc_seq, "A");
                _AccountsList = accList;//save in veiw state
                if (accList == null || accList.Count == 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter valid Account number!");
                    txtAccountNo.Text = null;
                    this.ClearMaster_details();
                    return;
                }
                else if (accList.Count == 1)
                {
                    //show summury
                    foreach (HpAccount ac in accList)
                    {
                        lblAccountNo.Text = ac.Hpa_acc_no;

                        //set UC values.
                        uc_HpAccountSummary1.set_all_values(ac, GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, GlbUserDefProf);
                        uc_HpAccountDetail1.Uc_hpa_acc_no = ac.Hpa_acc_no;
                    }
                }
                else if (accList.Count > 1)
                {
                    _isMainAcc = true;
                    //show a pop up to select the account number
                    grvMpdalPopUp.DataSource = accList;
                    grvMpdalPopUp.DataBind();
                    ModalPopupExtItem.Show();
                }
                txtManualRef.Focus();
            }
        }

        protected void btn_validateOthAcc_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOthAcc.Text))
            {

                string acc_seq = txtOthAcc.Text.Trim();
                List<HpAccount> accList = new List<HpAccount>();
                accList = CHNLSVC.Sales.GetHP_Accounts(GlbUserComCode, GlbUserDefProf, acc_seq, "A");
                _AccountsList = accList;//save in veiw state
                if (accList == null || accList.Count == 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter valid Account number!");
                    txtAccountNo.Text = null;
                    this.ClearOther_details();
                    return;
                }
                else if (accList.Count == 1)
                {
                    //show summury
                    foreach (HpAccount ac in accList)
                    {
                        lblOthAcc.Text = ac.Hpa_acc_no;

                        //set UC values.
                        uc_HpAccountSummary2.set_all_values(ac, GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, GlbUserDefProf);
                        uc_HpAccountDetail2.Uc_hpa_acc_no = ac.Hpa_acc_no;
                    }
                }
                else if (accList.Count > 1)
                {
                    _isMainAcc = false;
                    //show a pop up to select the account number
                    grvMpdalPopUp.DataSource = accList;
                    grvMpdalPopUp.DataBind();
                    ModalPopupExtItem.Show();
                }
                txtOthManual.Focus();
            }
        }

        protected void grvMpdalPopUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            // string location = ddl_Location.SelectedValue;
            GridViewRow row = grvMpdalPopUp.SelectedRow;
            string accountNo = row.Cells[1].Text.Trim();
            if (_isMainAcc == true)
            {
                lblAccountNo.Text = accountNo;
            }
            else
            {
                lblOthAcc.Text = accountNo;
            }

            // set UC values.
            HpAccount account = new HpAccount();
            foreach (HpAccount acc in _AccountsList)
            {
                if (accountNo == acc.Hpa_acc_no)
                {
                    account = acc;
                }
            }

            if (_isMainAcc == true)
            {
                uc_HpAccountSummary1.set_all_values(account, GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, GlbUserDefProf);
                uc_HpAccountDetail1.Uc_hpa_acc_no = account.Hpa_acc_no;
            }
            else
            {
                uc_HpAccountSummary2.set_all_values(account, GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, GlbUserDefProf);
                uc_HpAccountDetail2.Uc_hpa_acc_no = account.Hpa_acc_no;
            }
            
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string _msg = string.Empty;
            string _docNo = "";

            if (string.IsNullOrEmpty(txtAccountNo.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select main account #.");
                txtAccountNo.Focus();
                return;
            }

            //if (string.IsNullOrEmpty(txtManualRef.Text))
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter manual reference #");
            //    txtManualRef.Focus();
            //    return;
            //}

            if (ddlType.SelectedValue.Equals("-1"))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select valid type.");
                ddlType.Focus();
                return;
            }


            if (txtCredit.Enabled == true && txtDebit.Enabled == true)
            {
                if (Convert.ToDecimal(txtCredit.Text) <= 0 && Convert.ToDecimal(txtDebit.Text) <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter credit or debit amount.");
                    txtCredit.Focus();
                    return;
                }

                if (Convert.ToDecimal(txtCredit.Text) > 0 && Convert.ToDecimal(txtDebit.Text) > 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot enter both credit and debit amounts.");
                    txtCredit.Focus();
                    return;
                }
            }
            else if (txtCredit.Enabled == true)
            {
                if (Convert.ToDecimal(txtCredit.Text) <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter credit amount.");
                    txtCredit.Focus();
                    return;
                }
            }
            else if (txtDebit.Enabled == true)
            {
                if (Convert.ToDecimal(txtDebit.Text) <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter debit amount.");
                    txtDebit.Focus();
                    return;
                }
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Error in amount.");
                return;
            }

            if (string.IsNullOrEmpty(txtRematks.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter remarks.");
                txtRematks.Focus();
                return;
            }

            if (_isMultiAcc == true)
            {
                if (string.IsNullOrEmpty(txtOthAcc.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter other account #.");
                    txtOthAcc.Focus();
                    return;
                }

                if (lblAccountNo.Text == lblOthAcc.Text)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Account no is duplicated.");
                    txtAccountNo.Focus();
                    return;
                }

                //if (string.IsNullOrEmpty(txtOthManual.Text))
                //{
                //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter manual reference.");
                //    txtOthManual.Focus();
                //    return;
                //}

                if (txtOthCrAmt.Enabled == true && txtOthDeAmt.Enabled == true)
                {
                    if (Convert.ToDecimal(txtOthCrAmt.Text) <= 0 && Convert.ToDecimal(txtOthDeAmt.Text) <= 0)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter credit or debit amount for other account.");
                        txtOthCrAmt.Focus();
                        return;
                    }

                    if (Convert.ToDecimal(txtOthCrAmt.Text) > 0 && Convert.ToDecimal(txtOthDeAmt.Text) > 0)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot enter both credit and debit amounts for other account.");
                        txtOthCrAmt.Focus();
                        return;
                    }
                }
                else if (txtOthCrAmt.Enabled == true)
                {
                    if (Convert.ToDecimal(txtOthCrAmt.Text) <= 0)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter credit amount for other account.");
                        txtOthCrAmt.Focus();
                        return;
                    }
                }
                else if (txtOthDeAmt.Enabled == true)
                {
                    if (Convert.ToDecimal(txtOthDeAmt.Text) <= 0)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter debit amount for other account.");
                        txtOthDeAmt.Focus();
                        return;
                    }
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Error in amount for other account.");
                    return;
                }

                if (string.IsNullOrEmpty(txtOthRemarks.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter remarks for other account.");
                    txtOthRemarks.Focus();
                    return;
                }
            }

            //Save area____________
            CollectMainADJ();

            if (_isMultiAcc == true)
            {
                CollectOthADJ();
            }

            int effect = CHNLSVC.Sales.SaveHPAdjustment(_MainAdj, _MainTrans, _MainAdjAuto, _MainTxnAuto, _OthAdj, _OthTrans, _OthAdjAuto, _OthTxnAuto, _isMultiAcc, out _docNo);

            if (effect == 1)
            {

                string Msg = "<script>alert('Request Successfully Saved!');window.location = 'HPAdjustment.aspx';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Generated Nos: " + _docNo);
                Clear_Data();
            }
            else
            {
                if (!string.IsNullOrEmpty(_msg))
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _msg);
                }
                else
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Creation Fail.");
                }
            }

        }

        protected void CollectMainADJ()
        {
            string _type = "";
            decimal _crAmt = 0;
            decimal _drAmt = 0;
            string _desc = "";

            if (Convert.ToDecimal(txtCredit.Text) > 0)
            {
                _type = "C";
                _crAmt = Convert.ToDecimal(txtCredit.Text);
                _desc = "Credit Note";
            }
            else if (Convert.ToDecimal(txtDebit.Text) > 0)
            {
                _type = "D";
                _drAmt = Convert.ToDecimal(txtDebit.Text);
                _desc = "Debit Note";
            }

            //master account area for adjustment___________
            _MainAdj = new HpAdjustment();

            _MainAdj.Had_seq = 1;
            _MainAdj.Had_ref = "na";
            _MainAdj.Had_com = GlbUserComCode;
            _MainAdj.Had_pc = GlbUserDefProf;
            _MainAdj.Had_acc_no = lblAccountNo.Text.Trim();
            _MainAdj.Had_dt = Convert.ToDateTime(txtDate.Text).Date;
            _MainAdj.Had_tp = _type;
            _MainAdj.Had_adj_tp = ddlType.SelectedValue;
            _MainAdj.Had_mnl_ref = txtManualRef.Text.Trim();
            _MainAdj.Had_dbt_val = _drAmt;
            _MainAdj.Had_crdt_val = _crAmt;
            _MainAdj.Had_rmk = txtRematks.Text.Trim();
            _MainAdj.Had_cre_by = GlbUserName;
            _MainAdj.Had_cre_dt = Convert.ToDateTime(DateTime.Now).Date;


            _MainAdjAuto = new MasterAutoNumber();
            _MainAdjAuto.Aut_cate_cd = GlbUserDefProf;
            _MainAdjAuto.Aut_cate_tp = "PC";
            _MainAdjAuto.Aut_direction = 1;
            _MainAdjAuto.Aut_modify_dt = null;
            _MainAdjAuto.Aut_moduleid = "HP";
            _MainAdjAuto.Aut_number = 0;
            _MainAdjAuto.Aut_start_char = "HPADJ";
            _MainAdjAuto.Aut_year = Convert.ToDateTime(DateTime.Now).Year;


            //master account area for hp trasaction__________
            _MainTrans = new HpTransaction();

            _MainTrans.Hpt_seq = 1;
            _MainTrans.Hpt_ref_no = "na";
            _MainTrans.Hpt_com = GlbUserComCode;
            _MainTrans.Hpt_pc = GlbUserDefProf;
            _MainTrans.Hpt_acc_no = lblAccountNo.Text.Trim();
            _MainTrans.Hpt_txn_dt = Convert.ToDateTime(txtDate.Text).Date;
            _MainTrans.Hpt_txn_tp = "HPADJ";
            _MainTrans.Hpt_txn_ref = "na";
            _MainTrans.Hpt_desc = _desc;
            _MainTrans.Hpt_mnl_ref = txtManualRef.Text.Trim();
            _MainTrans.Hpt_crdt = _crAmt;
            _MainTrans.Hpt_dbt = _drAmt;
            _MainTrans.Hpt_bal = 0;
            _MainTrans.Hpt_ars = 0;
            _MainTrans.Hpt_cre_by = GlbUserName;
            _MainTrans.Hpt_cre_dt = Convert.ToDateTime(DateTime.Now).Date;

            _MainTxnAuto = new MasterAutoNumber();
            _MainTxnAuto.Aut_cate_cd = GlbUserDefProf;
            _MainTxnAuto.Aut_cate_tp = "PC";
            _MainTxnAuto.Aut_direction = 1;
            _MainTxnAuto.Aut_modify_dt = null;
            _MainTxnAuto.Aut_moduleid = "HP";
            _MainTxnAuto.Aut_number = 0;
            _MainTxnAuto.Aut_start_char = "HPT";
            _MainTxnAuto.Aut_year = Convert.ToDateTime(DateTime.Now).Year;

        }


        protected void CollectOthADJ()
        {
            string _Othtype = "";
            decimal _OthcrAmt = 0;
            decimal _OthdrAmt = 0;
            string _Othdesc = "";

            if (Convert.ToDecimal(txtOthCrAmt.Text) > 0)
            {
                _Othtype = "C";
                _OthcrAmt = Convert.ToDecimal(txtOthCrAmt.Text);
                _Othdesc = "Credit Note";
            }
            else if (Convert.ToDecimal(txtOthDeAmt.Text) > 0)
            {
                _Othtype = "D";
                _OthdrAmt = Convert.ToDecimal(txtOthDeAmt.Text);
                _Othdesc = "Debit Note";
            }

            //master account area for adjustment___________
            _OthAdj = new HpAdjustment();

            _OthAdj.Had_seq = 1;
            _OthAdj.Had_ref = "na";
            _OthAdj.Had_com = GlbUserComCode;
            _OthAdj.Had_pc = GlbUserDefProf;
            _OthAdj.Had_acc_no = lblOthAcc.Text.Trim();
            _OthAdj.Had_dt = Convert.ToDateTime(txtDate.Text).Date;
            _OthAdj.Had_tp = _Othtype;
            _OthAdj.Had_adj_tp = ddlType.SelectedValue;
            _OthAdj.Had_mnl_ref = txtOthManual.Text.Trim();
            _OthAdj.Had_dbt_val = _OthdrAmt;
            _OthAdj.Had_crdt_val = _OthcrAmt;
            _OthAdj.Had_rmk = txtOthRemarks.Text.Trim();
            _OthAdj.Had_cre_by = GlbUserName;
            _OthAdj.Had_cre_dt = Convert.ToDateTime(DateTime.Now).Date;


            _OthAdjAuto = new MasterAutoNumber();
            _OthAdjAuto.Aut_cate_cd = GlbUserDefProf;
            _OthAdjAuto.Aut_cate_tp = "PC";
            _OthAdjAuto.Aut_direction = 1;
            _OthAdjAuto.Aut_modify_dt = null;
            _OthAdjAuto.Aut_moduleid = "HP";
            _OthAdjAuto.Aut_number = 0;
            _OthAdjAuto.Aut_start_char = "HPADJ";
            _OthAdjAuto.Aut_year = Convert.ToDateTime(DateTime.Now).Year;


            //master account area for hp trasaction__________
            _OthTrans = new HpTransaction();

            _OthTrans.Hpt_seq = 1;
            _OthTrans.Hpt_ref_no = "na";
            _OthTrans.Hpt_com = GlbUserComCode;
            _OthTrans.Hpt_pc = GlbUserDefProf;
            _OthTrans.Hpt_acc_no = lblOthAcc.Text.Trim();
            _OthTrans.Hpt_txn_dt = Convert.ToDateTime(txtDate.Text).Date;
            _OthTrans.Hpt_txn_tp = "HPADJ";
            _OthTrans.Hpt_txn_ref = "na";
            _OthTrans.Hpt_desc = _Othdesc;
            _OthTrans.Hpt_mnl_ref = txtOthManual.Text.Trim();
            _OthTrans.Hpt_crdt = _OthcrAmt;
            _OthTrans.Hpt_dbt = _OthdrAmt;
            _OthTrans.Hpt_bal = 0;
            _OthTrans.Hpt_ars = 0;
            _OthTrans.Hpt_cre_by = GlbUserName;
            _OthTrans.Hpt_cre_dt = Convert.ToDateTime(DateTime.Now).Date;

            _OthTxnAuto = new MasterAutoNumber();
            _OthTxnAuto.Aut_cate_cd = GlbUserDefProf;
            _OthTxnAuto.Aut_cate_tp = "PC";
            _OthTxnAuto.Aut_direction = 1;
            _OthTxnAuto.Aut_modify_dt = null;
            _OthTxnAuto.Aut_moduleid = "HP";
            _OthTxnAuto.Aut_number = 0;
            _OthTxnAuto.Aut_start_char = "HPT";
            _OthTxnAuto.Aut_year = Convert.ToDateTime(DateTime.Now).Year;

        }
    }
}