using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using FF.BusinessObjects;
using System.Globalization;
using System.Transactions;
using FF.WebERPClient.UserControls;

namespace FF.WebERPClient.Financial_Modules
{
    public partial class ReceiptReversal : BasePage
    {

        protected List<RecieptHeader> _receiptHdr
        {
            get { return (List<RecieptHeader>)ViewState["_receiptHdr"]; }
            set { ViewState["_receiptHdr"] = value; }
        }

        protected List<RecieptItem> _RecItemList
        {
            get { return (List<RecieptItem>)ViewState["_RecItemList"]; }
            set { ViewState["_RecItemList"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                _receiptHdr = new List<RecieptHeader>();
                _RecItemList = new List<RecieptItem>();
                this.Clear_Data();
            }
        }

        private void Clear_Data()
        {
            txtfDate.Text = Convert.ToString(DateTime.Today.ToShortDateString());
            txttDate.Text = Convert.ToString(DateTime.Today.ToShortDateString());

            DataTable _Itemtable = new DataTable();
            gvReceipt.DataSource = _Itemtable;
            gvReceipt.DataBind();

            gvItems.DataSource = _Itemtable;
            gvItems.DataBind();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
           Clear_Data();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            List<RecieptHeader> _paramReceipt = new List<RecieptHeader>();

            _paramReceipt = CHNLSVC.Sales.GetReceiptBydaterange(GlbUserComCode, GlbUserDefProf, Convert.ToDateTime(txtfDate.Text).Date, Convert.ToDateTime(txttDate.Text).Date, "ADVAN");
            _receiptHdr = _paramReceipt;
            if (_receiptHdr != null)
            {
                gvReceipt.DataSource = _receiptHdr;
                gvReceipt.DataBind();
            }
            else
            {
                DataTable _Itemtable = new DataTable();
                gvReceipt.DataSource = _Itemtable;
                gvReceipt.DataBind();
            }
        }

        protected void gvReceipt_Rowcommand(object sender, GridViewCommandEventArgs e)
        {
            string _RecNo = "";
            switch (e.CommandName.ToUpper())
            {
                case "SELECT":
                    {
                        for (int i = 0; i < gvReceipt.Rows.Count; i++)
                        {
                            CheckBox chk = (CheckBox)gvReceipt.Rows[i].FindControl("chkselect");

                            if (chk.Checked == true)
                            {
                                chk.Checked = false;
                            }
                        }

                        GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                        CheckBox check = (CheckBox)row.FindControl("chkSelect");
                        check.Checked = true;

                        _RecNo = row.Cells[4].Text.ToString();
                        List<RecieptItem> _paramInvoiceItems = new List<RecieptItem>();
                        _paramInvoiceItems = CHNLSVC.Sales.GetAllReceiptItems(_RecNo);

                        _RecItemList = _paramInvoiceItems;
                        gvItems.DataSource = _RecItemList;
                        gvItems.DataBind();

                        break;
                    }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (gvReceipt.Rows.Count <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Receipts are not found.");
                return;
            }

            if (gvItems.Rows.Count <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Details are not found.");
                return;
            }

            string _RefundNo;
            string _msg = string.Empty;

            RecieptHeader _ReceiptHeader = new RecieptHeader();

            for (int i = 0; i < gvReceipt.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gvReceipt.Rows[i].FindControl("chkselect");

                if (chk.Checked == true)
                {

                    _ReceiptHeader.Sar_seq_no = 1;
                    _ReceiptHeader.Sar_com_cd = GlbUserComCode;
                    _ReceiptHeader.Sar_receipt_type = "ADREF";
                    _ReceiptHeader.Sar_receipt_no = "1";
                    _ReceiptHeader.Sar_prefix = gvReceipt.DataKeys[i][1].ToString();
                    _ReceiptHeader.Sar_manual_ref_no = gvReceipt.Rows[i].Cells[6].Text;
                    _ReceiptHeader.Sar_receipt_date = Convert.ToDateTime(DateTime.Now).Date;
                    _ReceiptHeader.Sar_direct = false;
                    _ReceiptHeader.Sar_acc_no = "";
                    _ReceiptHeader.Sar_is_oth_shop = false;
                    _ReceiptHeader.Sar_oth_sr = "";
                    _ReceiptHeader.Sar_profit_center_cd = GlbUserDefProf;
                    _ReceiptHeader.Sar_debtor_cd = gvReceipt.Rows[i].Cells[13].Text;
                    _ReceiptHeader.Sar_debtor_name = gvReceipt.Rows[i].Cells[14].Text;
                    _ReceiptHeader.Sar_debtor_add_1 = gvReceipt.DataKeys[i][2].ToString();
                    _ReceiptHeader.Sar_debtor_add_2 = gvReceipt.DataKeys[i][3].ToString();
                    _ReceiptHeader.Sar_tel_no = gvReceipt.DataKeys[i][4].ToString();
                    _ReceiptHeader.Sar_mob_no = gvReceipt.DataKeys[i][5].ToString();
                    _ReceiptHeader.Sar_nic_no = gvReceipt.DataKeys[i][6].ToString();
                    _ReceiptHeader.Sar_tot_settle_amt = Convert.ToDecimal(gvReceipt.DataKeys[i][14].ToString());
                    _ReceiptHeader.Sar_comm_amt = Convert.ToDecimal(gvReceipt.DataKeys[i][7].ToString());
                    _ReceiptHeader.Sar_is_mgr_iss = false;
                    _ReceiptHeader.Sar_esd_rate = Convert.ToDecimal(gvReceipt.DataKeys[i][8].ToString()); 
                    _ReceiptHeader.Sar_wht_rate = Convert.ToDecimal(gvReceipt.DataKeys[i][9].ToString());
                    _ReceiptHeader.Sar_epf_rate = Convert.ToDecimal(gvReceipt.DataKeys[i][10].ToString());
                    _ReceiptHeader.Sar_currency_cd = "LKR";
                    _ReceiptHeader.Sar_uploaded_to_finance = false;
                    _ReceiptHeader.Sar_act = true;
                    _ReceiptHeader.Sar_direct_deposit_bank_cd = "";
                    _ReceiptHeader.Sar_direct_deposit_branch = "";
                    _ReceiptHeader.Sar_remarks = gvReceipt.DataKeys[i][11].ToString();
                    _ReceiptHeader.Sar_is_used = false;
                    _ReceiptHeader.Sar_ref_doc = gvReceipt.Rows[i].Cells[4].Text;
                    _ReceiptHeader.Sar_ser_job_no = "";
                    _ReceiptHeader.Sar_used_amt = 0;
                    _ReceiptHeader.Sar_create_by = GlbUserName;
                    _ReceiptHeader.Sar_mod_by = GlbUserName;
                    _ReceiptHeader.Sar_session_id = GlbUserSessionID;
                    _ReceiptHeader.Sar_anal_1 = gvReceipt.DataKeys[i][12].ToString();
                    _ReceiptHeader.Sar_anal_2 = gvReceipt.DataKeys[i][13].ToString();
                    _ReceiptHeader.Sar_anal_3 = gvReceipt.DataKeys[i][15].ToString();
                    _ReceiptHeader.Sar_anal_4 = "";
                    _ReceiptHeader.Sar_anal_5 = 0;
                    _ReceiptHeader.Sar_anal_6 = 0;
                    _ReceiptHeader.Sar_anal_7 = 0;
                    _ReceiptHeader.Sar_anal_8 = 0;
                    _ReceiptHeader.Sar_anal_9 = 0;


                    MasterAutoNumber masterAuto = new MasterAutoNumber();
                    masterAuto.Aut_cate_cd = GlbUserDefProf;
                    masterAuto.Aut_cate_tp = "PC";
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "RECEIPT";
                    masterAuto.Aut_number = 5;//what is Aut_number
                    masterAuto.Aut_start_char = _ReceiptHeader.Sar_prefix;
                    masterAuto.Aut_year = null;

                    int effect = CHNLSVC.Sales.CreateRefund(_ReceiptHeader, _RecItemList, masterAuto, out _RefundNo);

                    if (effect == 1)
                    {

                        string Msg = "<script>alert('Receipt refund Successfully!');window.location = 'ReceiptReversal.aspx';</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                        this.Clear_Data();
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
            }
        }
    }
}