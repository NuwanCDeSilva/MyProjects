using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.Services;
using FastForward.SCMWeb.View.Reports.Inventory;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
using FF.BusinessObjects.InventoryNew;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Local_Purchasing
{
    public partial class PurchaseOrder : Base
    {
        bool _isApprovePrint
        {
            get { if (Session["_isApprovePrint"] != null) { return (bool)Session["_isApprovePrint"]; } else { return false; } }
            set { Session["_isApprovePrint"] = value; }
        }
        protected bool _isBackDate { get { return (bool)Session["_isBackDate"]; } set { Session["_isBackDate"] = value; } }
        protected DateTime _serverDt { get { return (DateTime)Session["_serverDt"]; } set { Session["_serverDt"] = value; } }
        protected Boolean _chksaveso { get { return (Boolean)Session["_chksaveso"]; } set { Session["_chksaveso"] = value; } }
        protected Boolean _sveso { get { return (Boolean)Session["_sveso"]; } set { Session["_sveso"] = value; } }

        private List<SupplierItem> _supItm
        {
            get { if (Session["_supItm"] != null) { return (List<SupplierItem>)Session["_supItm"]; } else { return new List<SupplierItem>(); } }
            set { Session["_supItm"] = value; }
        }
        private DataTable _itmData
        {
            get { if (Session["_itmData"] != null) { return (DataTable)Session["_itmData"]; } else { return new DataTable(); } }
            set { Session["_itmData"] = value; }
        }
        private List<QuotationHeader> _quotHd
        {
            get { if (Session["_quotHd"] != null) { return (List<QuotationHeader>)Session["_quotHd"]; } else { return new List<QuotationHeader>(); } }
            set { Session["_quotHd"] = value; }
        }
        private List<PurchaseOrderDetail> _POItemList = new List<PurchaseOrderDetail>();
        private List<PurchaseOrderDelivery> _POItemDel = new List<PurchaseOrderDelivery>();
        //private MasterBusinessEntity _supDet = new MasterBusinessEntity();
        private List<PurchaseReq> _PurchaseReqList = new List<PurchaseReq>();
        private List<InventoryRequestItem> _InventoryRequestItem = new List<InventoryRequestItem>();
        protected MasterBusinessEntity _supDet { get { return (MasterBusinessEntity)Session["_supDet"]; } set { Session["_supDet"] = value; } }
        protected bool _isStrucBaseTax { get { Session["_isStrucBaseTax "] = (Session["_isStrucBaseTax "] == null) ? false : (bool)Session["_isStrucBaseTax "]; ; return (bool)Session["_isStrucBaseTax "]; } set { Session["_isStrucBaseTax "] = value; } }

        Int32 _lineNo = 0;
        Int32 _delLineNo = 0;
        decimal _AddDelQty = 0;
        Boolean _IsRecall = false;

        string _POstatus = "";
        Int32 _POSeqNo = 0;
        decimal _suppNBTVal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearPage();
                ChangeName();
                BackDatePermission();
                ddlMainType.SelectedIndex = ddlMainType.Items.IndexOf(ddlMainType.Items.FindByValue("L"));
                ddlMainType_SelectedIndexChanged(null, null);
                CheckBox allPOItems = (CheckBox)grdReqItems.HeaderRow.FindControl("allPOItems");
                allPOItems.Visible = false;
                if (chkMultiReq.Checked)
                {
                    if (allPOItems != null && grdReqItems.Rows.Count > 1)
                    {
                        allPOItems.Visible = true;
                    }
                }
            }
            else if (IsPostBack)
            {
                txtPoDate.Text = Request[txtPoDate.UniqueID];
                txtFrom.Text = Request[txtFrom.UniqueID];
                txtTo.Text = Request[txtTo.UniqueID];

                if (Session["Job"] == "Job")
                {
                    UserDPopoup.Show();
                    Session["Job"] = "";
                }

            }
        }
        private void ChangeName()
        {
            var id = Request.QueryString["id"];
            if(id!=null)
            {
                divCenter.InnerHtml = "Asset Details";
                ItemCodeDiv.InnerHtml = "Asset Code";
                itemdetailsDiv.InnerHtml ="Asset Details";
                deliveryItemDiv.InnerHtml = "Asset";
                grdReqItems.Columns[1].HeaderText = "Asset";
                grdPOItems.Columns[3].HeaderText = "Asset";
                grdDel.Columns[3].HeaderText = "Asset";
            }
            
           
        }
        private void BackDatePermission()
        {
            try
            {
                _isBackDate = false;

                bool _allowCurrentTrans = false;
                LinkButton btntest = new LinkButton();
                if (IsAllowBackDateForModuleNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "", this.Page, "", btntest, lblBackDateInfor, "m_Trans_ProLocal_LOPO", out  _allowCurrentTrans))
                {
                    CalendarExtender3.Enabled = true;
                    // lbtnimgselectdate.CssClass = "buttonUndocolor";
                }
                else
                {
                    CalendarExtender3.Enabled = false;
                    // lbtnimgselectdate.CssClass = "buttoncolor";
                }
            }
            catch (Exception ex)
            {
                // DisplayMessage(ex.Message, 4);
            }
        }

        private void ClearErrorMsg()
        {
            WarningPorder.Visible = false;
            SuccessPorder.Visible = false;
            divSucces1.Visible = false;
            divDanger1.Visible = false;
        }
        private void ErrorMsgPOrder(string _Msg)
        {
            WarningPorder.Visible = true;
            lblWPorder.Text = _Msg;
        }
        private void ErrorPo(string _Msg)
        {
            divDanger1.Visible = true;
            lblDanger.Text = _Msg;
        }
        private void SuceesPo(string _Msg)
        {
            divSucces1.Visible = true;
            lblSuccess.Text = _Msg;
        }
        private void SuccessMsgPOrder(string _Msg)
        {
            SuccessPorder.Visible = true;
            lblSPorder.Text = _Msg;
        }
        private void ClearTextBox()
        {
            _sveso = false;
            _chksaveso = false;
            _supDet = new MasterBusinessEntity();
            Session["LowPrice"] = "0";
            Session["LowSupplier"] = "";
            Session["_BQty"] = "";
            Session["_Seq"] = "";
            Session["PRN_No"] = "";
            Session["_ItemLine"] = "";
            Session["_IsRecall"] = "false";
            Session["QuItem"] = "";
            _serverDt = CHNLSVC.Security.GetServerDateTime().Date;
            //Session["_IsRecall"] = "";
            ddlType.SelectedIndex = -1;
            DateTime date = DateTime.Now;
            // txtPoDate.Text = date.ToString("dd/MMM/yyyy");
            ddlPayTerm.SelectedIndex = -1;
            txtRemarks.Text = string.Empty;
            txtSupCode.Text = string.Empty;
            txtSupRef.Text = string.Empty;
            txtCreditPeriod.Text = "0";
            // txtCurrency.Text = "LKR";
            companyCurrancy();
            ExchangeRate();
            txtPurNo.Text = string.Empty;
            lblEx.Text = "0.00";
            LoadExRate();
            txtJob.Enabled = false;
            txtJob.Text = string.Empty;

            chkBaseToConsGrn.Checked = false;
            chkBaseToConsGrn.Enabled = false;
            txtFrom.Text = date.AddDays(-1).ToString("dd/MMM/yyyy");
            txtTo.Text = date.ToString("dd/MMM/yyyy");
            txtexDate.Text = date.ToString("dd/MMM/yyyy");
            txtprofitCenter.Text = Session["UserDefLoca"] != null ? Session["UserDefLoca"].ToString() : "";
            chkAll.Checked = false;
            grdReq.DataSource = new int[] { };
            grdReq.DataBind();
            txtItem.Text = string.Empty;
            txtItmDes.Text = string.Empty;
            ddlStatus.Enabled = true;
            // ddlStatus.SelectedItem.Text = string.Empty;
            while (ddlStatus.Items.Count > 1)
            {
                ddlStatus.Items.RemoveAt(1);
            }
            DataTable _result1 = CHNLSVC.Inventory.GetOrderStatus("A");
            ddlStatus.DataSource = _result1;
            ddlStatus.DataTextField = "mis_desc";
            ddlStatus.DataValueField = "mis_cd";
            ddlStatus.DataBind();
            //ddlStatus.SelectedValue = "GDLP";
            ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByValue("GDLP"));
            txtQty.Text = string.Empty;
            txtUnitPrice.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtDisRate.Text = string.Empty;
            txtDiscount.Text = string.Empty;
            txtTax.Text = string.Empty;
            txtTotal.Text = string.Empty;
            grdPOItems.DataSource = new int[] { };
            grdPOItems.DataBind();
            grdReqItems.DataSource = new int[] { };
            grdReqItems.DataBind();
            chkACosting.Checked = false;
            lblDelItem.Text = string.Empty;
            lblDelStatus.Text = string.Empty;
            txtDelLoca.Text = string.Empty;
            grdDel.DataSource = new int[] { };
            grdDel.DataBind();
            grdQuo.DataSource = new int[] { };
            grdQuo.DataBind();
            lblSubTot.Text = "0.00";
            lblDisAmt.Text = "0.00";
            lblTotAfterDis.Text = "0.00";
            lblTaxAmt.Text = "0.00";
            lblTotal.Text = "0.00";

            lbtnApproval.Enabled = false;
            lbtnApproval.OnClientClick = "return Enable();";
            lbtnApproval.CssClass = "buttoncolor";
            //lbtnApproval.Attributes["disabled"] = "disabled";
            lbtnSave.Enabled = true;

            lbtnCancel.Enabled = false;
            lbtnCancel.CssClass = "buttoncolor";
            lbtnCancel.OnClientClick = "return Enable();";

            lbtntJob.Visible = false;

            _IsRecall = false;
            _POstatus = "";
            _POSeqNo = 0;
            _POItemList = new List<PurchaseOrderDetail>();

            _POItemDel = new List<PurchaseOrderDelivery>();

            _supDet = new MasterBusinessEntity();

            _lineNo = 0;
            _delLineNo = 0;
            _AddDelQty = 0;

            clear_item();
            grdPOItems.AutoGenerateColumns = false;
            grdPOItems.DataSource = new List<PurchaseOrderDetail>();

            grdDel.AutoGenerateColumns = false;
            grdDel.DataSource = new List<PurchaseOrderDelivery>();

            grdQuo.AutoGenerateColumns = false;
            grdQuo.DataSource = new List<QuotationHeader>();

            //txtSupCode.Enabled = true;
            lbtnSupCode.Visible = true;
            LinkButton1.Visible = true;
            // LinkButton1.Enabled = true;
            ddlType.Focus();
            lblStatus.Text = string.Empty;
            lblEx.Text = string.Empty;
            txtCurrency.Text = string.Empty;
            ViewState["POItemList"] = null;
            ViewState["POItemDel"] = null;

            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());

            if (_masterComp.MC_TAX_CALC_MTD == "1") _isStrucBaseTax = true;

            if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10161))
            {
                txtUnitPrice.Enabled = true;
            }
            else
            {
                txtUnitPrice.Enabled = false;
            }    
        }

        //save Purchase Ordedr
        private void SavePOHeader()
        {
            try
            {
                string[] _purOrNo = new string[] { };
                Int32 row_aff = 0;
                string _PONo = string.Empty;
                string _msg = string.Empty;
                Int32 _isBaseCons = 0;
                string _type = string.Empty;

                if (ddlType.SelectedItem.Text == "CONSIGNMENT")
                {
                    _type = "C";
                }
                else if (ddlType.SelectedItem.Text == "NORMAL")
                {
                    _type = "N";
                }
                else if (ddlType.SelectedItem.Text == "SERVICE")
                {
                    _type = "S";
                }

                if (chkBaseToConsGrn.Checked == true)
                {
                    _isBaseCons = 1;
                }
                else
                {
                    _isBaseCons = 0;
                }


                FF.BusinessObjects.PurchaseOrder _PurchaseOrder = new FF.BusinessObjects.PurchaseOrder();
                _PurchaseOrder.Poh_seq_no = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "PO", 1, Session["UserCompanyCode"].ToString());
                _PurchaseOrder.Poh_tp = ddlMainType.SelectedValue;//"L";
                _PurchaseOrder.Poh_sub_tp = _type;
                _PurchaseOrder.Poh_doc_no = txtPurNo.Text.Trim();
                _PurchaseOrder.Poh_com = Session["UserCompanyCode"].ToString();
                _PurchaseOrder.Poh_ope = "INV";
                _PurchaseOrder.Poh_profit_cd = Session["UserDefProf"].ToString();
                _PurchaseOrder.Poh_dt = Convert.ToDateTime(txtPoDate.Text).Date;
                _PurchaseOrder.Poh_ref = txtSupRef.Text;
                _PurchaseOrder.Poh_job_no = txtJob.Text;
                _PurchaseOrder.Poh_pay_term = ddlPayTerm.SelectedItem.Text;
                _PurchaseOrder.Poh_supp = txtSupCode.Text;
                _PurchaseOrder.Poh_cur_cd = txtCurrency.Text;
                _PurchaseOrder.Poh_ex_rt = Convert.ToDecimal(lblEx.Text);
                _PurchaseOrder.Poh_trans_term = "";
                _PurchaseOrder.Poh_port_of_orig = "";
                _PurchaseOrder.Poh_cre_period = txtCreditPeriod.Text;
                _PurchaseOrder.Poh_frm_yer = Convert.ToDateTime(txtPoDate.Text).Year;
                _PurchaseOrder.Poh_frm_mon = Convert.ToDateTime(txtPoDate.Text).Month;
                _PurchaseOrder.Poh_to_yer = Convert.ToDateTime(txtPoDate.Text).Year;
                _PurchaseOrder.Poh_to_mon = Convert.ToDateTime(txtPoDate.Text).Month;
                _PurchaseOrder.Poh_preferd_eta = Convert.ToDateTime(txtPoDate.Text).Date;
                _PurchaseOrder.Poh_contain_kit = false;
                _PurchaseOrder.Poh_sent_to_vendor = false;
                _PurchaseOrder.Poh_sent_by = "";
                _PurchaseOrder.Poh_sent_via = "";
                _PurchaseOrder.Poh_sent_add = "";
                _PurchaseOrder.Poh_stus = "P";
                _PurchaseOrder.Poh_remarks = txtRemarks.Text;
                _PurchaseOrder.Poh_sub_tot = Convert.ToDecimal(lblSubTot.Text);
                _PurchaseOrder.Poh_tax_tot = Convert.ToDecimal(lblTaxAmt.Text);
                _PurchaseOrder.Poh_dis_rt = 0;
                _PurchaseOrder.Poh_dis_amt = Convert.ToDecimal(lblDisAmt.Text);
                _PurchaseOrder.Poh_oth_tot = 0;
                _PurchaseOrder.Poh_tot = Convert.ToDecimal(lblTotal.Text);
                _PurchaseOrder.Poh_reprint = false;
                _PurchaseOrder.Poh_tax_chg = false;
                _PurchaseOrder.poh_is_conspo = _isBaseCons;
                _PurchaseOrder.Poh_cre_by = Session["UserID"].ToString();
                // _PurchaseOrder.poh_session_id = Session["SessionID"].ToString();

                List<PurchaseOrderDetail> _POItemListSave = new List<PurchaseOrderDetail>();
                _POItemList = ViewState["POItemList"] as List<PurchaseOrderDetail>;

                foreach (PurchaseOrderDetail line in _POItemList)
                {
                    line.Pod_seq_no = _PurchaseOrder.Poh_seq_no;
                    line.Pod_pi_bal = line.Pod_qty;
                    _POItemListSave.Add(line);
                }

                List<PurchaseOrderDelivery> _PODelSave = new List<PurchaseOrderDelivery>();
                _POItemDel = ViewState["POItemDel"] as List<PurchaseOrderDelivery>;
                foreach (PurchaseOrderDelivery line in _POItemDel)
                {
                    line.Podi_seq_no = _PurchaseOrder.Poh_seq_no;
                    _PODelSave.Add(line);
                }

                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                masterAuto.Aut_cate_tp = "COM";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "PUR";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "PUR";
                masterAuto.Aut_year = null;

                string QTNum;
                _PurchaseReqList = ViewState["PurchaseReqList"] as List<PurchaseReq>;
                // List<PurchaseReq> _PurchaseReq = new List<PurchaseReq>();
                _InventoryRequestItem = ViewState["InventoryRequestItem"] as List<InventoryRequestItem>;

                row_aff = (Int32)CHNLSVC.Inventory.SaveNewPO(_PurchaseOrder, _POItemListSave, _PODelSave, null, masterAuto, _PurchaseReqList, _InventoryRequestItem, out QTNum, _sveso);

                if (row_aff == 1)
                {
                    SuccessMsgPOrder("Purchase order generated.PO # : " + QTNum);

                    ClearTextBox();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        ErrorMsgPOrder(_msg);


                    }
                    else
                    {
                        ErrorMsgPOrder("Faild to generate");

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsgPOrder("Error Occurred while processing...\n");
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }
        //Update purchase order
        private void UpdatePurchaseOrder()
        {
            try
            {
                Int32 row_aff = 0;
                string _msg = string.Empty;
                Int32 _isBaseCons = 0;

                string _type = string.Empty;

                if (ddlType.SelectedItem.Text == "CONSIGNMENT")
                {
                    _type = "C";
                }
                else if (ddlType.SelectedItem.Text == "NORMAL")
                {
                    _type = "N";
                }
                else if (ddlType.SelectedItem.Text == "SERVICE")
                {
                    _type = "S";
                }

                if (chkBaseToConsGrn.Checked == true)
                {
                    _isBaseCons = 1;
                }
                else
                {
                    _isBaseCons = 0;
                }


                FF.BusinessObjects.PurchaseOrder _PurchaseOrder = new FF.BusinessObjects.PurchaseOrder();
                _PurchaseOrder.Poh_seq_no = Convert.ToInt32(Session["_POSeqNo"].ToString());//_POSeqNo;
                _PurchaseOrder.Poh_tp = ddlMainType.SelectedValue; //"L";
                _PurchaseOrder.Poh_sub_tp = _type;
                _PurchaseOrder.Poh_doc_no = txtPurNo.Text.Trim();
                _PurchaseOrder.Poh_com = Session["UserCompanyCode"].ToString();
                _PurchaseOrder.Poh_ope = "INV";
                _PurchaseOrder.Poh_profit_cd = Session["UserDefProf"].ToString();
                _PurchaseOrder.Poh_dt = Convert.ToDateTime(txtPoDate.Text).Date;
                _PurchaseOrder.Poh_ref = txtSupRef.Text;
                _PurchaseOrder.Poh_job_no = txtJob.Text;
                _PurchaseOrder.Poh_pay_term = ddlPayTerm.SelectedItem.Text;
                _PurchaseOrder.Poh_supp = txtSupCode.Text;
                _PurchaseOrder.Poh_cur_cd = txtCurrency.Text;
                _PurchaseOrder.Poh_ex_rt = Convert.ToDecimal(lblEx.Text);
                _PurchaseOrder.Poh_trans_term = "";
                _PurchaseOrder.Poh_port_of_orig = "";
                _PurchaseOrder.Poh_cre_period = txtCreditPeriod.Text;
                _PurchaseOrder.Poh_frm_yer = Convert.ToDateTime(txtPoDate.Text).Year;
                _PurchaseOrder.Poh_frm_mon = Convert.ToDateTime(txtPoDate.Text).Month;
                _PurchaseOrder.Poh_to_yer = Convert.ToDateTime(txtPoDate.Text).Year;
                _PurchaseOrder.Poh_to_mon = Convert.ToDateTime(txtPoDate.Text).Month;
                _PurchaseOrder.Poh_preferd_eta = Convert.ToDateTime(txtPoDate.Text).Date;
                _PurchaseOrder.Poh_contain_kit = false;
                _PurchaseOrder.Poh_sent_to_vendor = false;
                _PurchaseOrder.Poh_sent_by = "";
                _PurchaseOrder.Poh_sent_via = "";
                _PurchaseOrder.Poh_sent_add = "";
                _PurchaseOrder.Poh_stus = "P";
                _PurchaseOrder.Poh_remarks = txtRemarks.Text;
                _PurchaseOrder.Poh_sub_tot = Convert.ToDecimal(lblSubTot.Text);
                _PurchaseOrder.Poh_tax_tot = Convert.ToDecimal(lblTaxAmt.Text);
                _PurchaseOrder.Poh_dis_rt = 0;
                _PurchaseOrder.Poh_dis_amt = Convert.ToDecimal(lblDisAmt.Text);
                _PurchaseOrder.Poh_oth_tot = 0;
                _PurchaseOrder.Poh_tot = Convert.ToDecimal(lblTotal.Text);
                _PurchaseOrder.Poh_reprint = false;
                _PurchaseOrder.Poh_tax_chg = false;
                _PurchaseOrder.poh_is_conspo = _isBaseCons;
                _PurchaseOrder.Poh_cre_by = Session["UserID"].ToString();


                List<PurchaseOrderDetail> _POItemListSave = new List<PurchaseOrderDetail>();
                _POItemList = ViewState["POItemList"] as List<PurchaseOrderDetail>;

                foreach (PurchaseOrderDetail line in _POItemList)
                {
                    line.Pod_seq_no = _PurchaseOrder.Poh_seq_no;
                    _POItemListSave.Add(line);
                }

                List<PurchaseOrderDelivery> _PODelSave = new List<PurchaseOrderDelivery>();
                _POItemDel = ViewState["POItemDel"] as List<PurchaseOrderDelivery>;
                foreach (PurchaseOrderDelivery line in _POItemDel)
                {
                    line.Podi_seq_no = _PurchaseOrder.Poh_seq_no;
                    _PODelSave.Add(line);
                }

                row_aff = (Int32)CHNLSVC.Inventory.UpdateSavedPO(_PurchaseOrder, _POItemListSave, _PODelSave, _PurchaseOrder.Poh_seq_no);

                if (row_aff == 1)
                {
                    SuccessMsgPOrder("Purchase order updated.");

                    ClearTextBox();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        ErrorMsgPOrder(_msg);

                    }
                    else
                    {
                        ErrorMsgPOrder("Failed to update.");

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsgPOrder("Error Occurred while processing...\n");
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        protected void LoadExRate()
        {
            try
            {
                decimal _curExRate = 0;
                MasterExchangeRate _ExRate = CHNLSVC.Sales.GetLaterstExchangeRate(Session["UserCompanyCode"].ToString(), txtCurrency.Text.Trim(), Convert.ToDateTime(txtPoDate.Text).Date);

                if (_ExRate != null)
                {
                    _curExRate = _ExRate.Mer_bnksel_rt;
                    lblEx.Text = _curExRate.ToString("0.00");
                    lbtnSave.Enabled = true;
                    // txtCurrency.Focus();
                }
                else
                {
                    ErrorMsgPOrder("Exchange rate is not define for above currency.");
                    // MessageBox.Show("Exchange rate is not define for above currency.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblEx.Text = "";
                    txtCurrency.Text = "";
                    lbtnSave.Enabled = false;
                    lbtnApproval.Enabled = false;
                    txtCurrency.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorMsgPOrder("Error Occurred while processing...\n");
                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void clear_item()
        {
            txtItem.Text = "";
            txtItmDes.Text = "";
            if (ddlType.SelectedItem.Text != "CONSIGNMENT")
            {
                // ddlStatus.SelectedItem.Text = "";
            }
            txtQty.Text = "";
            txtUnitPrice.Text = "";
            txtAmount.Text = "";
            txtDisRate.Text = "";
            txtDiscount.Text = "";
            txtTax.Text = "";
            txtTotal.Text = "";
            Session["Qu__Item"] = "";
            txtwarrper.Text = "";
            txtwarrremk.Text = "";
        }
        private void ClearPage()
        {
            _isApprovePrint = false;
            ClearErrorMsg();
            ClearTextBox();

        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            if (Session["UserCompanyCode"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Currency:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        string type;
                        if (ddlMainType.SelectedItem.Text == "Local") { type = "L"; } else { type = "F"; }
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + type);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.POByDate:
                    {
                        string type;
                        if (ddlMainType.SelectedItem.Text == "Local") { type = "L"; } else { type = "I"; }
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + type);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearch:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "2,2.1,3,4,4.1,5,5.1" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PurchaseOrder:
                    {
                        string type;
                        if (ddlMainType.SelectedItem.Text == "Local") { type = "L"; } else { type = "I"; }
                        paramsText.Append(txtSupCode.Text + seperator + type);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }
        private void companyCurrancy()
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
            DataTable CompanyCurrancytbl = CHNLSVC.CommonSearch.SearchCompanyCurrancy(SearchParams);
            if (CompanyCurrancytbl.Rows.Count > 0)
            {
                Session["Compayc"] = CompanyCurrancytbl.Rows[0]["CURRENCY"].ToString();
                txtCurrency.Text = Session["Compayc"].ToString();
                return;
            }
            //ErrorMsgPOrder("not load Compnay currrency ");
        }

        private void ExchangeRate()
        {
            DataTable ERateTbl = CHNLSVC.Financial.GetExchangeRate(Session["UserCompanyCode"].ToString(), txtCurrency.Text, Session["Compayc"].ToString());
            if (ERateTbl.Rows.Count > 0)
            {
                lblEx.Text = ERateTbl.Rows[0][5].ToString();
                return;
            }
            ErrorMsgPOrder("not setup currrency Rate");
            lblEx.Text = string.Empty;
        }
        protected void lbtnWPorderok_Click(object sender, EventArgs e)
        {
            ClearErrorMsg();

        }
        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }
        public void BindUCtrlDDLData2(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykeyD.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykeyD.SelectedIndex = 0;
        }
        public static Boolean IsNumeric(string stringToTest)
        {
            decimal result;
            return decimal.TryParse(stringToTest, out result);
        }
        private void LoadSaveDocument()
        {
            _POItemList = ViewState["POItemList"] as List<PurchaseOrderDetail>;
            _POItemDel = ViewState["POItemDel"] as List<PurchaseOrderDelivery>;
            lbtnSupCode.Visible = false;
            LinkButton1.Visible = false;

            _IsRecall = false;
            _POstatus = "";

            FF.BusinessObjects.PurchaseOrder _POHeader = new FF.BusinessObjects.PurchaseOrder();
            _POHeader = CHNLSVC.Inventory.GetPOHeader(Session["UserCompanyCode"].ToString(), txtPurNo.Text.Trim(), ddlMainType.SelectedValue);

            if (_POHeader != null)
            {
                ddlMainType.SelectedValue = _POHeader.Poh_tp;
                txtSupCode.Text = _POHeader.Poh_supp;
                txtSupRef.Text = _POHeader.Poh_ref;
                txtRemarks.Text = _POHeader.Poh_remarks;
                txtCurrency.Text = _POHeader.Poh_cur_cd;
                lblEx.Text = _POHeader.Poh_ex_rt.ToString();
                txtPoDate.Text = Convert.ToDateTime(_POHeader.Poh_dt).ToShortDateString();
                lblSubTot.Text = Convert.ToDecimal(_POHeader.Poh_sub_tot).ToString("n");
                lblDisAmt.Text = Convert.ToDecimal(_POHeader.Poh_dis_amt).ToString("n");
                lblTotAfterDis.Text = Convert.ToDecimal(_POHeader.Poh_sub_tot - _POHeader.Poh_dis_amt).ToString("n");
                lblTaxAmt.Text = Convert.ToDecimal(_POHeader.Poh_tax_tot).ToString("n");
                lblTotal.Text = Convert.ToDecimal(_POHeader.Poh_tot).ToString("n");
                ddlPayTerm.SelectedItem.Text = _POHeader.Poh_pay_term;
                txtCreditPeriod.Text = _POHeader.Poh_cre_period;
                txtJob.Text = _POHeader.Poh_job_no;

                if (_POHeader.Poh_stus == "A")
                {
                    lblStatus.Text = "APPROVED";

                    lbtnApproval.Enabled = false;
                    lbtnApproval.OnClientClick = "return Enable();";
                    lbtnApproval.CssClass = "buttoncolor";

                    lbtnCancel.Enabled = true;
                    lbtnCancel.OnClientClick = "CancelConfirm();";
                    lbtnCancel.CssClass = "buttonUndocolor";

                    lbtnSave.Enabled = false;
                    lbtnSave.OnClientClick = "return Enable();";
                    lbtnSave.CssClass = "buttoncolor";


                    //lbtnApproval.Enabled = false;
                    //lbtnCancel.Enabled = false;
                    //lbtnSave.Enabled = false;
                }
                else if (_POHeader.Poh_stus == "P")
                {
                    lblStatus.Text = "PENDING";
                    lbtnApproval.Enabled = true;
                    lbtnApproval.OnClientClick = "ApprovalConfirm();";
                    lbtnApproval.CssClass = "buttonUndocolor";

                    lbtnCancel.Enabled = true;
                    lbtnCancel.OnClientClick = "CancelConfirm();";
                    lbtnCancel.CssClass = "buttonUndocolor";

                    lbtnSave.Enabled = true;
                    lbtnSave.OnClientClick = "SaveConfirm();";
                    lbtnSave.CssClass = "buttonUndocolor";


                    //lbtnApproval.Enabled = true;
                    //lbtnCancel.Enabled = true;
                    //lbtnSave.Enabled = true;
                }
                else if (_POHeader.Poh_stus == "C")
                {
                    lblStatus.Text = "CANCELLED";
                    lbtnApproval.Enabled = false;
                    lbtnApproval.OnClientClick = "return Enable();";
                    lbtnApproval.CssClass = "buttoncolor";

                    lbtnCancel.Enabled = false;
                    lbtnCancel.OnClientClick = "return Enable();";
                    lbtnCancel.CssClass = "buttoncolor";

                    lbtnSave.Enabled = false;
                    lbtnSave.OnClientClick = "return Enable();";
                    lbtnSave.CssClass = "buttoncolor";

                    //lbtnApproval.Enabled = false;
                    //lbtnCancel.Enabled = false;
                    //lbtnSave.Enabled = false;
                }
                else if (_POHeader.Poh_stus == "F")
                {
                    lblStatus.Text = "COMPLETED";
                    lbtnApproval.Enabled = false;
                    lbtnApproval.OnClientClick = "return Enable();";
                    lbtnApproval.CssClass = "buttoncolor";

                    lbtnCancel.Enabled = false;
                    lbtnCancel.OnClientClick = "return Enable();";
                    lbtnCancel.CssClass = "buttoncolor";

                    lbtnSave.Enabled = false;
                    lbtnSave.OnClientClick = "return Enable();";
                    lbtnSave.CssClass = "buttoncolor";

                    //lbtnApproval.Enabled = false;
                    //lbtnCancel.Enabled = false;
                    //lbtnSave.Enabled = false;
                }

                if (_POHeader.Poh_sub_tp == "N")
                {
                    //ddlType.Text = "NORMAL";
                    ddlType.SelectedItem.Text = "NORMAL";

                }
                else if (_POHeader.Poh_sub_tp == "C")
                {
                    //ddlType.Text = "CONSIGNMENT";
                    ddlType.SelectedItem.Text = "CONSIGNMENT";
                }
                else if (_POHeader.Poh_sub_tp == "S")
                {
                    ddlType.SelectedItem.Text = "SERVICE";
                    //ddlType.SelectedValue = "1";
                }

                if (_POHeader.poh_is_conspo == 0)
                {
                    chkBaseToConsGrn.Checked = false;
                }
                else if (_POHeader.poh_is_conspo == 1)
                {
                    chkBaseToConsGrn.Checked = true;
                }
                else
                {
                    chkBaseToConsGrn.Checked = false;
                }

                _POstatus = _POHeader.Poh_stus;
                Session["_POstatus"] = _POstatus;
                _POSeqNo = _POHeader.Poh_seq_no;
                Session["_POSeqNo"] = _POSeqNo;
                //if (_POHeader.Poh_sub_tp == "C")
                //{
                //    //_IsCons = true;DataValueField
                //    //chkIsCons.Checked = true;
                //    ddlType.SelectedItem.Text = "CONSIGNMENT";
                //    ddlType.Enabled = false;
                //    chkBaseToConsGrn.Enabled = true;
                //}
                //else
                //{
                //    // _IsCons = false;
                //    // chkIsCons.Checked = false;
                //    ddlType.SelectedIndex = 0;
                //    ddlType.Enabled = true;
                //    chkBaseToConsGrn.Enabled = false;
                //}

                PurchaseOrderDetail _paramPOItems = new PurchaseOrderDetail();

                _paramPOItems.Pod_seq_no = _POSeqNo;

                List<PurchaseOrderDetail> _list = CHNLSVC.Inventory.GetPOItems(_paramPOItems);
                _POItemList = new List<PurchaseOrderDetail>();
                _POItemList = _list;

                if (_POItemList != null)
                {
                    var max_Query =
         (from tab1 in _POItemList
          select tab1.Pod_line_no).Max();

                    _lineNo = max_Query;
                }


                grdPOItems.AutoGenerateColumns = false;
                grdPOItems.DataSource = new List<PurchaseOrderDetail>();
                grdPOItems.DataSource = _POItemList;
                grdPOItems.DataBind();

                // DataTable tbl =ConvertToDataTable(_POItemList);
                ViewState["POItemList"] = _POItemList;

                PurchaseOrderDelivery _paramPOdelItems = new PurchaseOrderDelivery();

                _paramPOdelItems.Podi_seq_no = _POSeqNo;

                List<PurchaseOrderDelivery> _delList = CHNLSVC.Inventory.GetPODelItems(_paramPOdelItems);
                _POItemDel = new List<PurchaseOrderDelivery>();
                _POItemDel = _delList;

                if (_POItemDel != null)
                {
                    var max_DelQuery =
           (from tab1 in _POItemDel
            select tab1.Podi_del_line_no).Max();

                    _delLineNo = max_DelQuery;


                    _POItemDel = _POItemDel.OrderBy(x => x.Podi_line_no).ToList();
                    grdDel.AutoGenerateColumns = false;
                    grdDel.DataSource = new List<PurchaseOrderDelivery>();
                    grdDel.DataSource = _POItemDel;
                    grdDel.DataBind();
                    ViewState["POItemDel"] = _POItemDel;
                    //BindSavePOItems(_POHeader.Poh_seq_no);
                    //bindSavePODelItems(_POHeader.Poh_seq_no);
                    _IsRecall = true;
                    Session["_IsRecall"] = "true";
                    //btnApprove.Enabled = true;
                    //btnCancel.Enabled = true;
                    //btnSave.Enabled = false;
                    _PurchaseReqList = CHNLSVC.Inventory.GetPoReqLogDetails(_POHeader.Poh_seq_no);
                    if (_PurchaseReqList != null)
                    {
                        ViewState["PurchaseReqList"] = _PurchaseReqList;
                        pnlReq.Enabled = false;
                        ErrorMsgPOrder("You cannot use, more than one purchase request note to place an order.");
                    }
                    else
                    {
                        ViewState["PurchaseReqList"] = _PurchaseReqList;
                        pnlReq.Enabled = false;

                    }


                }
            }
            else
            {
                ErrorMsgPOrder("Invalid purchase order.");
                //  Clear_Data();
                return;
            }


        }
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        private decimal TaxCalculationActualCost(string _com, string _item, string _status, decimal _UnitPrice, string _supTaxCate, decimal _actTaxVal)
        {
            decimal _totNBT = 0;
            decimal _NBT = 0;
            decimal _oTax = 0;
            decimal _claimTaxRt = 0;
            _actTaxVal = 0;

            // List<MasterItemTax> _taxs = new List<MasterItemTax>();
            // _taxs = CHNLSVC.Inventory.GetItemTax(_com, _item, _status, string.Empty, string.Empty);

            List<MasterItemTax> _taxs = new List<MasterItemTax>();
            if (_isStrucBaseTax == true)       //kapila added one in pos invoice copy by darshana
            {
                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(_com, _item);
                _taxs = CHNLSVC.Sales.GetItemTax_strucbase(_com, _item, _status, string.Empty, string.Empty, _mstItem.Mi_anal1);
            }
            else
                _taxs = CHNLSVC.Sales.GetItemTax(_com, _item, _status, string.Empty, string.Empty);

            var _Tax = from _itm in _taxs
                       select _itm;

            MasterItemTaxClaim _claimTax = new MasterItemTaxClaim();
            _claimTax = CHNLSVC.Sales.GetTaxClaimDet(_com, _item, _supTaxCate);

            SupplierWiseNBT _supNBT = new SupplierWiseNBT();
            if (txtSupCode.Text != null && txtSupCode.Text != "")
            {
                _supNBT = CHNLSVC.Sales.GetSupplierNBT(_com, txtSupCode.Text);
            }
            else
            {
                ErrorMsgPOrder("Please select supplier...!!!");
            }
            if (_supNBT != null && _supNBT.MBIT_TAX_RT != 0)
            {
                _suppNBTVal = ((_UnitPrice * _supNBT.MBIT_TAX_RT) / _supNBT.MBIT_DIV_RT);
            }

            foreach (MasterItemTax _one in _Tax)
            {
                if (_one.Mict_tax_cd == "NBT")
                {
                    _NBT = _UnitPrice * _one.Mict_tax_rate / 100;
                    _actTaxVal = _actTaxVal + _NBT;
                    _totNBT = _totNBT + _NBT;
                }
                else if (_suppNBTVal > 0)
                {
                    _NBT = _suppNBTVal;
                    _actTaxVal = _actTaxVal + _NBT;
                    _totNBT = _totNBT + _NBT;
                }
                //_TaxVal = _UnitPrice * _one.Mict_tax_rate / 100;
            }


            foreach (MasterItemTax _two in _Tax)
            {
                if (_two.Mict_tax_cd != "NBT")
                {
                    if (_claimTax != null)
                    {
                        _claimTaxRt = _two.Mict_tax_rate - _claimTax.Mic_claim;
                    }
                    else
                    {
                        _claimTaxRt = _two.Mict_tax_rate;
                    }

                    _oTax = (_UnitPrice + _totNBT) * _claimTaxRt / 100;
                    _actTaxVal = _actTaxVal + _oTax;

                }
            }


            return _actTaxVal;
        }

        private decimal TaxCalculation(string _com, string _item, string _status, decimal _UnitPrice, decimal _TaxVal)
        {
            decimal _totNBT = 0;
            decimal _NBT = 0;
            decimal _oTax = 0;
            _TaxVal = 0;

            SupplierWiseNBT _supNBT = new SupplierWiseNBT();
            if (txtSupCode.Text != null && txtSupCode.Text != "")
            {
                _supNBT = CHNLSVC.Sales.GetSupplierNBT(_com, txtSupCode.Text);
            }
            else
            {
                ErrorMsgPOrder("Please select supplier...!!!");
            }
            // List<MasterItemTax> _taxs = new List<MasterItemTax>();
            // _taxs = CHNLSVC.Inventory.GetItemTax(_com, _item, _status, string.Empty, string.Empty);
            List<MasterItemTax> _taxs = new List<MasterItemTax>();
            if (Convert.ToDateTime(txtPoDate.Text).Date == _serverDt)
            {

                if (_isStrucBaseTax == true)       //kapila added one in pos invoice copy by darshana
                {
                    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(_com, _item);
                    if (_mstItem == null)
                    {
                        ErrorMsgPOrder("Please Setup Item For Company");
                    }

                    _taxs = CHNLSVC.Sales.GetItemTax_strucbase(_com, _item, _status, string.Empty, string.Empty, _mstItem.Mi_anal1);


                }
                else
                {
                    _taxs = CHNLSVC.Sales.GetItemTax(_com, _item, _status, string.Empty, string.Empty);
                }
            }
            else
            {
                _taxs = new List<MasterItemTax>();
                _taxs = CHNLSVC.Sales.GetTaxEffDt(_com, _item, _status, Convert.ToDateTime(txtPoDate.Text).Date);
            }


            var _Tax = from _itm in _taxs
                       select _itm;

            if (_supNBT != null && _supNBT.MBIT_TAX_RT != 0)
            {
                _suppNBTVal = ((_UnitPrice * _supNBT.MBIT_TAX_RT) / _supNBT.MBIT_DIV_RT);
            }
            if (_taxs.Count > 0)
            {
                foreach (MasterItemTax _one in _Tax)
                {
                    if (_one.Mict_tax_cd == "NBT")
                    {
                        _NBT = _UnitPrice * _one.Mict_tax_rate / 100;
                        _TaxVal = _TaxVal + _NBT;
                        _totNBT = _totNBT + _NBT;
                    }
                    else if (_suppNBTVal > 0)
                    {
                        _NBT = _suppNBTVal;
                        _TaxVal = _TaxVal + _NBT;
                        _totNBT = _totNBT + _NBT;
                    }
                    //_TaxVal = _UnitPrice * _one.Mict_tax_rate / 100;
                }

                foreach (MasterItemTax _two in _Tax)
                {
                    if (_two.Mict_tax_cd != "NBT")
                    {
                        _oTax = (_UnitPrice + _totNBT) * _two.Mict_tax_rate / 100;
                        _TaxVal = _TaxVal + _oTax;
                    }
                }
            }
            else
            {
                List<LogMasterItemTax> _taxsEffDt = new List<LogMasterItemTax>();
                //_taxsEffDt = CHNLSVC.Sales.GetTaxLog(_com, _item, _status, Convert.ToDateTime(txtPoDate.Text).Date); 
                _taxsEffDt = CHNLSVC.Sales.GetItemTaxLog(_com, _item, _status, string.Empty, string.Empty, Convert.ToDateTime(txtPoDate.Text).Date);
                var _TaxEffDt = from _itm in _taxsEffDt
                                select _itm;

                if (_supNBT != null && _supNBT.MBIT_TAX_RT != 0)
                {
                    _suppNBTVal = ((_UnitPrice * _supNBT.MBIT_TAX_RT) / _supNBT.MBIT_DIV_RT);
                }
                foreach (LogMasterItemTax _one in _TaxEffDt)
                {
                    if (_one.Lict_tax_cd == "NBT")
                    {
                        _NBT = (_UnitPrice + _suppNBTVal) * _one.Lict_tax_rate / 100;
                        _TaxVal = _TaxVal + _NBT;
                        _totNBT = _totNBT + _NBT;
                    }
                    else if (_suppNBTVal > 0)
                    {
                        _NBT = _suppNBTVal;
                        _TaxVal = _TaxVal + _NBT;
                        _totNBT = _totNBT + _NBT;
                    }
                    //_TaxVal = _UnitPrice * _one.Mict_tax_rate / 100;
                }

                foreach (LogMasterItemTax _two in _TaxEffDt)
                {
                    if (_two.Lict_tax_cd != "NBT")
                    {
                        _oTax = (_UnitPrice + _totNBT) * _two.Lict_tax_rate / 100;
                        _TaxVal = _TaxVal + _oTax;

                    }
                }

            }

            if (_status == "CONS")
            {
                _TaxVal = 0;
            }

            return _TaxVal;
        }
        private void GetSupplierQuotation()
        {
            String _type = "";
            if (ddlType.SelectedItem.Text == "CONSIGNMENT")
            {
                _type = "C";
            }
            else if (ddlType.SelectedItem.Text == "NORMAL")
            {
                _type = "N";
            }
            else if (ddlType.SelectedItem.Text == "SERVICE")
            {
                _type = "S";
            }
            else
            {
                ErrorMsgPOrder("Quotations will not load due to undefine order type.");
            }
            //GetLatestValidQuotation
            List<QuotationHeader> _list = CHNLSVC.Inventory.GetLatestAllValidQuotation(Session["UserCompanyCode"].ToString(), txtSupCode.Text, "S", _type, Convert.ToDateTime(txtPoDate.Text).Date, Convert.ToDecimal(txtQty.Text), "A", txtItem.Text);
            grdQuo.AutoGenerateColumns = false;
            grdQuo.DataSource = new List<QuotationHeader>();
            grdQuo.DataSource = _list;
            grdQuo.DataBind();
            if (_list.Count > 0)
            {
                var _filter = _list.OrderBy(x => x.Qh_anal_5).ToList();
                Session["LowPrice"] = _filter[0].Qh_anal_5;
                Session["LowSupplier"] = _filter[0].Qh_party_cd;
            }

        }
        private void FilterData(string _RNo)
        {
            DataTable _result = (DataTable)ViewState["ReqItem"];
            DataView dv = new DataView(_result);
            // string searchParameter = ddlSearchbykey.SelectedItem.Text;
            dv.RowFilter = " itr_req_no = '" + _RNo + "'";
            // dv.RowFilter = "REFERENCESNO = '" + txtSearchbyword.Text + "' ";
            if (dv.Count > 0)
            {
                if (chkMultiReq.Checked)
                {
                    _itmData.Merge(dv.ToTable());
                }
                else
                {
                    _itmData = dv.ToTable();
                }
            }
            grdReqItems.DataSource = new int[] { };
            if (_itmData.Rows.Count > 0)
            {
                grdReqItems.DataSource = _itmData;
            }
            grdReqItems.DataBind();
        }

        private Boolean check_Total(decimal _tot)
        {
            decimal _unitVal = 0;
            decimal _qty = 0;
            decimal _amt = 0;
            decimal _disRate = 0;
            decimal _disAmount = 0;
            decimal _tax = 0;
            decimal _total = 0;
            Boolean _correct = false;
            decimal _diff = 0;


            _unitVal = Convert.ToDecimal(txtUnitPrice.Text);
            _qty = Convert.ToDecimal(txtQty.Text);
            _amt = _unitVal * _qty;
            _disRate = Convert.ToDecimal(txtDisRate.Text);
            _disAmount = _amt * _disRate / 100;
            _tax = TaxCalculation(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), ddlStatus.SelectedValue.Trim(), _amt - _disAmount, 0);
            _total = Math.Round((_amt - _disAmount) + _tax, 2);

            if (_tot == _total)
            {
                _correct = true;
            }
            else if (_diff > 0)
            {
                if (_diff > 10)
                {
                    _correct = false;
                }
                else
                {
                    _correct = true;
                }
            }
            else if (_diff < 0)
            {
                if (_diff > -10)
                {
                    _correct = false;
                }
                else
                {
                    _correct = true;
                }
            }
            else if (_diff == 0)
            {
                _correct = true;
            }
            else
            {
                _correct = false;
            }

            return _correct;
        }

        private void UpdatePOStatus(string _POUpdateStatus, bool _isApproved)
        {
            Int32 row_aff = 0;
            string _msg = string.Empty;


            FF.BusinessObjects.PurchaseOrder _UpdatepurchaseOrder = new FF.BusinessObjects.PurchaseOrder();
            _UpdatepurchaseOrder.Poh_seq_no = Convert.ToInt32(Session["_POSeqNo"].ToString());//_POSeqNo;
            _UpdatepurchaseOrder.Poh_doc_no = txtPurNo.Text.Trim();
            _UpdatepurchaseOrder.Poh_stus = _POUpdateStatus;
            _UpdatepurchaseOrder.Poh_com = Session["UserCompanyCode"].ToString();
            _UpdatepurchaseOrder.Poh_cre_by = Session["UserID"].ToString();
            _UpdatepurchaseOrder.Poh_supp = txtSupCode.Text.Trim();
            _UpdatepurchaseOrder.Poh_ref = txtSupRef.Text.Trim();
            _UpdatepurchaseOrder.Poh_remarks = txtRemarks   .Text.Trim();
            _PurchaseReqList = ViewState["PurchaseReqList"] as List<PurchaseReq>;
            if (_isApproved == true)
            {
                row_aff = (Int32)CHNLSVC.Inventory.UpdatePOStatusNew(_UpdatepurchaseOrder, null);

            }
            else if ((_UpdatepurchaseOrder != null))
            {
                row_aff = (Int32)CHNLSVC.Inventory.UpdatePOStatusNew(_UpdatepurchaseOrder, null);
            }
            else if (_PurchaseReqList != null)
            {
                ReallocateQtyPRN(_POUpdateStatus, _isApproved);
                return;
            }

            if (row_aff == 1)
            {
                if (_isApproved == true)
                {
                    SuccessMsgPOrder("Successfully approved.");
                    #region genaret mail 14 dec 2017 by nuwan
                    if (_UpdatepurchaseOrder.Poh_supp != null)
                    {
                        
                        try
                        {
                            _POItemList = ViewState["POItemList"] as List<PurchaseOrderDetail>;
                            CHNLSVC.MsgPortal.SendMailPOApprove(_UpdatepurchaseOrder, _POItemList, "APPROVE");
                        }
                        catch (Exception ex)
                        {
                            SuccessMsgPOrder("Successfully approved.");
                        }
                    }
                    #endregion
                }
                else if (_isApproved == false)
                {

                    SuccessMsgPOrder("Successfully cancelled.");
                    #region genaret mail 14 dec 2017 by nuwan
                    if (_UpdatepurchaseOrder.Poh_supp != null)
                    {

                        try
                        {
                            _POItemList = ViewState["POItemList"] as List<PurchaseOrderDetail>;
                            CHNLSVC.MsgPortal.SendMailPOApprove(_UpdatepurchaseOrder, _POItemList, "CANCEL");
                        }
                        catch (Exception ex)
                        {
                            SuccessMsgPOrder("Successfully approved.");
                        }
                    }
                    #endregion

                }
                // Clear_Data();
            }
            else
            {
                if (!string.IsNullOrEmpty(_msg))
                {
                    ErrorMsgPOrder(_msg);

                }
                else
                {
                    ErrorMsgPOrder("Faild to update.");

                }
            }
        }


        private void ReallocateQtyPRN(string _POUpdateStatus, bool _isApproved)
        {
            _PurchaseReqList = ViewState["PurchaseReqList"] as List<PurchaseReq>;
            _POItemList = ViewState["POItemList"] as List<PurchaseOrderDetail>;

            List<InventoryRequestItem> _reqItem = new List<InventoryRequestItem>();

            //    var currrange = (from cur in _PurchaseReqList
            //                     where cur.Por_itm_cd == _Item.Pod_itm_cd && cur.Por_itm_stus == _Item.Pod_itm_stus
            //                     select cur).ToList();
            //    if (currrange != null)
            //    {
            foreach (PurchaseReq _Update in _PurchaseReqList)
            {
                Session["PRNReqNo"] = _Update.Por_req_no;
                DataTable dtHeaders = CHNLSVC.CommonSearch.SearchPRNHeader(_Update.Por_req_no);
                if (dtHeaders.Rows.Count > 0)
                {
                    foreach (DataRow item in dtHeaders.Rows)
                    {
                        Session["PRNSEQNO"] = item[0].ToString();
                    }
                }
            }
            foreach (PurchaseOrderDetail _Item in _POItemList)
            {
                DataTable dtitems = CHNLSVC.CommonSearch.SearchPRNItems(Convert.ToInt32(Session["PRNSEQNO"].ToString()));
                if (dtitems.Rows.Count > 0)
                {

                    foreach (DataRow row in dtitems.Rows)
                    {
                        if (_Item.Pod_itm_cd == row["Item"].ToString())
                        {
                            InventoryRequestItem req = new InventoryRequestItem();
                            req.Itri_res_no = Session["PRNReqNo"].ToString();
                            req.Itri_itm_cd = _Item.Pod_itm_cd;
                            req.Itri_itm_stus = _Item.Pod_itm_stus;
                            decimal Qty = Convert.ToDecimal(row["B.Qty"].ToString());
                            decimal PoQty = Convert.ToDecimal(row["Po.Qty"].ToString());
                            req.Itri_po_qty = PoQty - _Item.Pod_qty;
                            req.Itri_bqty = Qty + _Item.Pod_qty;
                            _reqItem.Add(req);
                        }


                    }

                    // CHNLSVC.Inventory.Update_PORequestBalanceQty(_reqItem);
                }

            }


            //  }
            // }
            FF.BusinessObjects.PurchaseOrder _UpdatepurchaseOrder = new FF.BusinessObjects.PurchaseOrder();
            _UpdatepurchaseOrder.Poh_seq_no = Convert.ToInt32(Session["_POSeqNo"].ToString());//_POSeqNo;
            _UpdatepurchaseOrder.Poh_doc_no = txtPurNo.Text.Trim();
            _UpdatepurchaseOrder.Poh_stus = _POUpdateStatus;
            _UpdatepurchaseOrder.Poh_com = Session["UserCompanyCode"].ToString();
            _UpdatepurchaseOrder.Poh_cre_by = Session["UserID"].ToString();

            int row_aff1 = (Int32)CHNLSVC.Inventory.UpdatePOStatusNew(_UpdatepurchaseOrder, _reqItem);
            if (row_aff1 == 1)
            {

                if (_isApproved == true)
                {
                    SuccessMsgPOrder("Successfully approved.");

                }
                else if (_isApproved == false)
                {

                    SuccessMsgPOrder("Successfully cancelled.");


                }

            }
        }
        private void Cal_Totals()
        {
            decimal _amtBeforeDis = 0;
            decimal _disAmt = 0;
            decimal _amtAfterDis = 0;
            decimal _taxAmt = 0;
            decimal _totAmt = 0;

            foreach (PurchaseOrderDetail _tmpPo in _POItemList)
            {
                _amtBeforeDis = _amtBeforeDis + _tmpPo.Pod_line_val;
                _disAmt = _disAmt + _tmpPo.Pod_dis_amt;
                _amtAfterDis = _amtBeforeDis - _disAmt;
                _taxAmt = _taxAmt + _tmpPo.Pod_line_tax;
                _totAmt = _totAmt + _tmpPo.Pod_line_amt;
            }

            lblSubTot.Text = Convert.ToString(_amtBeforeDis.ToString("n"));
            lblDisAmt.Text = Convert.ToString(_disAmt.ToString("n"));
            lblTotAfterDis.Text = Convert.ToString(_amtAfterDis.ToString("n"));
            lblTaxAmt.Text = Convert.ToString(_taxAmt.ToString("n"));
            lblTotal.Text = Convert.ToString(_totAmt.ToString("n"));
        }

        # region Main Button Click event
        protected void lbtnApproval_Click(object sender, EventArgs e)
        {
            try
            {
                ClearErrorMsg();
                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16023))
                {
                    ErrorMsgPOrder("Sorry, You have no permission to approve this purchase order.( Advice: Required permission code : 16023) !!!");
                    return;
                }
                if (string.IsNullOrEmpty(txtPurNo.Text))
                {
                    ErrorMsgPOrder("Please select purchase order #.");

                    txtPurNo.Focus();
                    return;
                }

                if (Session["_IsRecall"].ToString() == "false")
                {
                    ErrorMsgPOrder("Please recall saved order");
                    txtPurNo.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(Session["_POstatus"].ToString()))
                {
                    ErrorMsgPOrder("Cannot retrive current po status.Please re-try.");

                    txtPurNo.Focus();
                    return;
                }

                if (Session["_POstatus"].ToString() == "A")
                {
                    ErrorMsgPOrder("Selected order already approved.");

                    txtPurNo.Focus();
                    return;
                }

                if (Session["_POstatus"].ToString() == "C")
                {
                    ErrorMsgPOrder("Selected order is canceled.");

                    txtPurNo.Focus();
                    return;
                }

                if (Session["_POstatus"].ToString() == "F")
                {
                    ErrorMsgPOrder("Selected order is completed.");

                    txtPurNo.Focus();
                    return;
                }
                if (txtApprovalconformmessageValue.Value == "No")
                {
                    return;
                }

                UpdatePOStatus("A", true);
                _isApprovePrint = true;
                lbtnPrintPO_Click(null, null);
                //if (MessageBox.Show("Do you want to print the purchase order?", "Purchase order", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                //{
                //    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                //    Session["GlbReportName"] = "PurchaseOrderPrint.rpt";
                //    BaseCls.GlbReportDoc = txtPurNo.Text.Trim();
                //    _view.Show();
                //    _view = null;
                //}

                ClearTextBox();
            }
            catch (Exception err)
            {

                CHNLSVC.CloseChannel();
                ErrorMsgPOrder(err.Message);

            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ClearErrorMsg();
                if (string.IsNullOrEmpty(txtPurNo.Text))
                {
                    ErrorMsgPOrder("Please select purchase order #.");

                    txtPurNo.Focus();
                    return;
                }

                if (Session["_IsRecall"].ToString() == "false")
                {
                    ErrorMsgPOrder("Please recall saved order.");

                    txtPurNo.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(Session["_POstatus"].ToString()))
                {
                    ErrorMsgPOrder("Cannot retrive current po status.Please re-try.");

                    txtPurNo.Focus();
                    return;
                }

                if (_POstatus == "A")
                {
                    ErrorMsgPOrder("Selected order is already approved.");

                    txtPurNo.Focus();
                    return;
                }

                if (_POstatus == "F")
                {
                    ErrorMsgPOrder("Selected order is already completed.");

                    txtPurNo.Focus();
                    return;
                }

                if (_POstatus == "C")
                {
                    ErrorMsgPOrder("Selected order is already cancelled.");

                    txtPurNo.Focus();
                    return;
                }

                if (txtCancelconformmessageValue.Value == "No")
                {
                    return;
                }
                if (lblStatus.Text == "APPROVED")
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16023))
                    {
                        ErrorMsgPOrder("Sorry, You have no permission to Cancel this purchase order.( Advice: Required permission code : 16023) !!!");
                        return;
                    }
                    UpdatePOStatus("C", false);

                }
                else
                {

                    UpdatePOStatus("C", false);
                }


                ClearTextBox();
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                ErrorMsgPOrder(err.Message);

            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ClearErrorMsg();
                if (string.IsNullOrEmpty(txtSupCode.Text))
                {
                    ErrorMsgPOrder("Please select supplier code.");

                    txtSupCode.Focus();
                    return;
                }
                if (!(ddlMainType.SelectedIndex > 0))
                {
                    ErrorMsgPOrder("Please select Type.");

                    ddlMainType.Focus();
                    return;
                }


                if (grdPOItems.Rows.Count == 0)
                {
                    ErrorMsgPOrder("Cannot find purchase order items.");

                    txtItem.Focus();
                    return;
                }

                if (_POstatus == "A")
                {
                    ErrorMsgPOrder("Order is already approved.");

                    txtPurNo.Focus();
                    return;
                }

                if (_POstatus == "F")
                {
                    ErrorMsgPOrder("Order is already completed.");

                    txtPurNo.Focus();
                    return;
                }

                if (_POstatus == "C")
                {
                    ErrorMsgPOrder("Order is already cancelled.");

                    txtPurNo.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCurrency.Text))
                {
                    ErrorMsgPOrder("Please select po currency.");

                    txtCurrency.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(lblEx.Text))
                {
                    ErrorMsgPOrder("Exchange rate is missing.");

                    txtCurrency.Focus();
                    return;
                }

                if (Convert.ToDecimal(lblEx.Text) <= 0)
                {
                    ErrorMsgPOrder("Invalid exchange rate.");

                    txtCurrency.Focus();
                    return;
                }
                if (txtSupRef.Text=="")
                {
                    ErrorMsgPOrder("Please Enter Supplier Ref");
                    txtSupRef.Focus();
                    return;
                }
                if (ddlType.SelectedItem.Text == "SERVICE")
                {
                    if (string.IsNullOrEmpty(txtJob.Text))
                    {
                        ErrorMsgPOrder("Please select job number.");

                        txtJob.Focus();
                        return;
                    }
                }

                //if (MessageBox.Show("Do you want to save this order ?", "Purchase order", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                //{
                //    return;
                //}
                if (txtSavelconformmessageValue.Value == "No")
                {
                    return;
                }
                decimal _poQty = 0;
                decimal _delQty = 0;
                _POItemList = ViewState["POItemList"] as List<PurchaseOrderDetail>;
                _POItemDel = ViewState["POItemDel"] as List<PurchaseOrderDelivery>;
                foreach (PurchaseOrderDetail _tmpItm in _POItemList)
                {
                    _poQty = _tmpItm.Pod_qty;
                    _delQty = 0;
                    foreach (PurchaseOrderDelivery _tmpDel in _POItemDel)
                    {
                        //  Session["_IsRecall"] = "false";
                        if (_tmpDel.Podi_line_no == _tmpItm.Pod_line_no)
                        {
                            _delQty = _delQty + _tmpDel.Podi_qty;
                        }
                    }

                    if (_delQty != _poQty)
                    {//Please add a location for the item 
                        ErrorMsgPOrder("Delivery schedule quantity and purchase quantity mismatch for the item " + _tmpItm.Pod_itm_cd);
                        return;
                    }
                }
                if (Session["_IsRecall"] == null)
                {
                    Session["_IsRecall"] = "false";
                }

                if (_chksaveso == false)
                {
                    MasterBusinessEntity _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtSupCode.Text, null, null, null, null, Session["UserCompanyCode"].ToString());
                    if (_masterBusinessCompany != null && _masterBusinessCompany.Mbe_intr_com && _masterBusinessCompany.Mbe_tp == "S")
                    {
                        List<InterCompanySalesParameter> oInterCompanySalesParameters = CHNLSVC.Sales.GET_INTERCOM_PAR_BY_SUP(Session["UserCompanyCode"].ToString(), txtSupCode.Text);
                        if (oInterCompanySalesParameters != null && oInterCompanySalesParameters.Count > 0)
                        {
                            MDPDOConf.Show();
                            return;

                        }

                    }
                }
                if (Session["_IsRecall"].ToString() == "false")
                {
                    SavePOHeader();
                    Session["_IsRecall"] = "false";
                }
                else if (Session["_IsRecall"].ToString() == "true")
                {
                    UpdatePurchaseOrder();
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                ErrorMsgPOrder(err.Message);

            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "Yes")
            {
                try
                {
                    Response.Redirect(Request.RawUrl, false);
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                }
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
            }
            txtClearlconformmessageValue.Value = "";
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                ClearErrorMsg();
                if (string.IsNullOrEmpty(txtPurNo.Text))
                {
                    ErrorMsgPOrder("Please select purchase order.");

                    txtPurNo.Focus();
                    return;
                }


                if (_IsRecall == false)
                {
                    ErrorMsgPOrder("Please recall purchase order.");
                    //MessageBox.Show("Please recall purchase order.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPurNo.Focus();
                    return;
                }

                if (_POstatus == "P")
                {
                    ErrorMsgPOrder("Selected order is still in pending status.");

                    txtPurNo.Focus();
                    return;
                }

                //Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                Session["GlbReportName"] = "PurchaseOrderPrint.rpt";
                BaseCls.GlbReportDoc = txtPurNo.Text.Trim();
                // _view.Show();
                // _view = null;


            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                ErrorMsgPOrder(err.Message);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private decimal getClaimableTax(string _com, string _item, string _status, decimal _UnitPrice, string _supTaxCate, decimal _actTaxRate)
        {
            _actTaxRate = 0;

            List<MasterItemTax> _taxs = new List<MasterItemTax>();
            if (_isStrucBaseTax == true)       //kapila added one in pos invoice copy by darshana
            {
                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(_com, _item);

                _taxs = CHNLSVC.Sales.GetItemTax_strucbase(_com, _item, _status, string.Empty, string.Empty, _mstItem.Mi_anal1);


            }
            else
            {
                _taxs = CHNLSVC.Inventory.GetItemTax(_com, _item, _status, string.Empty, string.Empty);
            }

            var _Tax = from _itm in _taxs
                       select _itm;

            foreach (MasterItemTax _two in _Tax)
            {
                if (_two.Mict_tax_cd != "NBT")
                {
                    _actTaxRate = _two.Mict_tax_rate;
                }
            }

            return _actTaxRate;
        }

        protected void lbtnAddItem_Click(object sender, EventArgs e)
        {
            decimal _amt = 0;
            decimal _disamt = 0;
            decimal _unitAmt = 0;
            decimal _qty = 0;
            decimal _amount = 0;
            decimal _tax = 0;
            decimal _total = 0;
            _amt = Convert.ToDecimal(txtAmount.Text);
            if ( txtDiscount.Text != null && txtDiscount.Text != "")
            {
                _disamt = Convert.ToDecimal(txtDiscount.Text);
            }
            else
            {
                _disamt = 0;
            }
            _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtSupCode.Text.Trim(), null, null, "S");
            _qty = Convert.ToDecimal(txtQty.Text);
            _unitAmt = Convert.ToDecimal(txtUnitPrice.Text);
            _amount = _qty * _unitAmt;
            if (_supDet.Mbe_is_tax)
            {
                _tax = TaxCalculation(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), ddlStatus.SelectedValue.ToString(), _amt - _disamt, 0);
            }
            _total = (_amt - _disamt) + _tax;
            try
            {
                ClearErrorMsg();
                // List<PurchaseOrderDetail>  _POItemList = new List<PurchaseOrderDetail>();

                // DataTable POItemListTbl = ViewState["POItemList"] as DataTable;
                // DatatbleToList(POItemListTbl);
                _POItemList = ViewState["POItemList"] as List<PurchaseOrderDetail>;
                _POItemDel = ViewState["POItemDel"] as List<PurchaseOrderDelivery>;
                if (ViewState["POItemList"] == null)
                {
                    _POItemList = new List<PurchaseOrderDetail>();
                    _POItemDel = new List<PurchaseOrderDelivery>();
                }

                Boolean _isVatClaim = false;
                string _suppTaxCate = "";
                if (string.IsNullOrEmpty(txtSupCode.Text))
                {
                    ErrorMsgPOrder("Please select supplier code.");
                    txtSupCode.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCurrency.Text))
                {
                    ErrorMsgPOrder("Please select currecy code.");

                    txtCurrency.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    ErrorMsgPOrder("Please enter purchasing item.");
                    txtItem.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(ddlStatus.SelectedItem.Text))
                {
                    ErrorMsgPOrder("Please select item status.");
                    ddlStatus.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    ErrorMsgPOrder("Please enter purchasing quantity.");
                    txtQty.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtUnitPrice.Text))
                {
                    ErrorMsgPOrder("Please enter item unit price.");
                    txtUnitPrice.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtAmount.Text))
                {
                    ErrorMsgPOrder("Amount is not calculation. Please re-enter unit price.");
                    txtUnitPrice.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDisRate.Text))
                {
                    ErrorMsgPOrder("Discount rate is not found.");
                    txtDisRate.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDiscount.Text))
                {
                    ErrorMsgPOrder("Discount amount is not found.");
                    txtDiscount.Focus();
                    return;
                }

                Boolean _proceed = check_Total(Convert.ToDecimal(txtTotal.Text));

                if (_proceed == false)
                {
                    ErrorMsgPOrder("Found amount mismatches. Please re-enter this item details.");
                    return;
                }


                var currrange = (from cur in _POItemList
                                 where cur.Pod_itm_cd == txtItem.Text.Trim() && cur.Pod_unit_price == Convert.ToDecimal(txtUnitPrice.Text) && cur.Pod_itm_stus == ddlStatus.SelectedValue.Trim()
                                 select cur).ToList();
                if (currrange.Count > 0)// ||currrange !=null)
                {
                    ErrorMsgPOrder("Selected item already exsist with same price.");
                    txtItem.Focus();
                    return;
                }

                PurchaseOrderDetail _tmpPoDetails = new PurchaseOrderDetail();
                PurchaseOrderDelivery _tmpPoDel = new PurchaseOrderDelivery();
                MasterItem _tmpItem = new MasterItem();

                _tmpItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim());

                if ((_POItemList.Count > 0))
                {
                    var max_Query =
                 (from tab1 in _POItemList
                  select tab1.Pod_line_no).Max();

                    _lineNo = max_Query;
                }
                else
                {
                    _lineNo = 0;
                }

                
                if (_supDet != null)
                {
                    _isVatClaim = _supDet.Mbe_is_tax;
                    _suppTaxCate = _supDet.Mbe_cate;
                }
                else
                {
                    ErrorMsgPOrder("Cannot find supplier details.");
                    return;
                }

                if (_supDet != null)
                {
                    if (!string.IsNullOrEmpty(_supDet.Mbe_cate))
                    {
                        MasterItemTaxClaim _claimTax = new MasterItemTaxClaim();
                        decimal _claimTax_Rt = 0;
                        _claimTax = CHNLSVC.Sales.GetTaxClaimDet(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), _supDet.Mbe_cate);
                        if (_claimTax != null)
                        {
                            _claimTax_Rt = _claimTax.Mic_claim;
                        }
                        lblclmPre.Text = _claimTax_Rt.ToString("0.00");


                        decimal _disRate = string.IsNullOrEmpty(txtDisRate.Text) ? 0 : Convert.ToDecimal(txtDisRate.Text);
                        decimal _disAmount = _amount * _disRate / 100;

                        decimal _taxRate = getClaimableTax(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), ddlStatus.SelectedValue.ToString(), _amount - _disAmount, _supDet.Mbe_cate, 0);
                        if (_taxRate != 0)
                        {
                            lblclmTaxAmt.Text = (_tax / _taxRate * _claimTax_Rt).ToString("0.00");
                        }
                        else lblclmTaxAmt.Text = "0";
                        lblfinalUnitPrc.Text = (_total - Convert.ToDecimal(lblclmTaxAmt.Text)).ToString("0.00");
                    }
                }
                //Added By Dulaj Prevent taxClaimable if taxStructure tax is less than taxclaimable  2018/Jul/06                 
                // decimal _structaxs
                if (_isStrucBaseTax == true) 
                {
                    MasterItem _mstItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim());
                    var _taxspo = CHNLSVC.Sales.GetItemTax_strucbase(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), ddlStatus.SelectedValue.ToString(), string.Empty, string.Empty, _mstItem.Mi_anal1);
                    decimal _disRateFPo = string.IsNullOrEmpty(txtDisRate.Text) ? 0 : Convert.ToDecimal(txtDisRate.Text);
                    decimal _disAmountPO = _amount * _disRateFPo / 100;
                    decimal _taxRatepo = 0;
                    if(!(lblclmPre.Text.Equals(string.Empty)))
                    {
                         _taxRatepo = Convert.ToDecimal(lblclmPre.Text);
                    }                    
                    if (_taxspo.Count>0 && _taxRatepo > 0)
                    {
                        if (_taxspo[0].Mict_tax_rate < _taxRatepo)
                        {
                            ErrorMsgPOrder("Item tax structure and claimable tax rate are mismatch. Please contact inventory department!");
                            lblfinalUnitPrc.Text = "0";
                            lblclmPre.Text = "0";
                            return;
                        }
                    }
                }
                //
                decimal _taxForActual = 0;

                if (_tmpItem != null)
                {
                    _lineNo = _lineNo + 1;
                    // Add po items ______________________
                    if (_supDet.Mbe_is_tax)
                    {
                        if (string.IsNullOrEmpty(_suppTaxCate))
                        {
                            _tmpPoDetails.Pod_act_unit_price = (Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text) - Convert.ToDecimal(txtDiscount.Text) + Convert.ToDecimal(txtTax.Text)) / Convert.ToDecimal(txtQty.Text);
                        }
                        else
                        {
                            decimal _unitVal = Convert.ToDecimal(txtUnitPrice.Text);
                            _qty = Convert.ToDecimal(txtQty.Text);
                            _amt = _unitVal * _qty;
                            decimal _disRate = Convert.ToDecimal(txtDisRate.Text);
                            decimal _disAmount = _amt * _disRate / 100;

                            _taxForActual = TaxCalculationActualCost(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), ddlStatus.SelectedValue.Trim(), _amt - _disAmount, _suppTaxCate, 0);
                            _tmpPoDetails.Pod_act_unit_price = ((Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text) - Convert.ToDecimal(txtDiscount.Text)) + _taxForActual) / Convert.ToDecimal(txtQty.Text);
                        }
                    }else
                    {
                        _tmpPoDetails.Pod_act_unit_price = (Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text) - Convert.ToDecimal(txtDiscount.Text) + Convert.ToDecimal(txtTax.Text)) / Convert.ToDecimal(txtQty.Text);//Added by Dulaj 2018/Jan/07
                    }
                    _tmpPoDetails.Pod_dis_amt = Convert.ToDecimal(txtDiscount.Text);
                    _tmpPoDetails.Pod_dis_rt = Convert.ToDecimal(txtDisRate.Text);
                    _tmpPoDetails.Pod_grn_bal = Convert.ToDecimal(txtQty.Text);
                    _tmpPoDetails.Pod_item_desc = txtItmDes.Text.Trim();
                    _tmpPoDetails.Pod_itm_cd = txtItem.Text.Trim();
                    if (ddlStatus.SelectedItem.Text == "CONS")
                    {
                        _tmpPoDetails.Pod_itm_stus = "CONS";
                    }
                    else
                    {
                        _tmpPoDetails.Pod_itm_stus = ddlStatus.SelectedValue.Trim();
                    }

                    _tmpPoDetails.Pod_itm_tp = _tmpItem.Mi_itm_tp;
                    _tmpPoDetails.Pod_kit_itm_cd = "";
                    _tmpPoDetails.Pod_kit_line_no = 0;
                    _tmpPoDetails.Pod_lc_bal = 0;
                    _tmpPoDetails.Pod_line_amt = Convert.ToDecimal(txtTotal.Text);
                    _tmpPoDetails.Pod_line_no = _lineNo;
                    _tmpPoDetails.Pod_line_tax = Convert.ToDecimal(txtTax.Text);
                    _tmpPoDetails.Pod_line_val = Convert.ToDecimal(txtAmount.Text);
                    _tmpPoDetails.Pod_nbt = _suppNBTVal;
                    _tmpPoDetails.Pod_vat = (Convert.ToDecimal(txtTax.Text) - _suppNBTVal);
                    _tmpPoDetails.Pod_nbt_before = 0;
                    _tmpPoDetails.Pod_pi_bal = 0;
                    _tmpPoDetails.Pod_qty = Convert.ToDecimal(txtQty.Text);
                    _tmpPoDetails.Pod_ref_no = txtSupRef.Text;
                    _tmpPoDetails.Pod_seq_no = 12;
                    _tmpPoDetails.Pod_si_bal = 0;
                    _tmpPoDetails.Pod_tot_tax_before = 0;
                    _tmpPoDetails.Pod_unit_price = Convert.ToDecimal(txtUnitPrice.Text);
                    _tmpPoDetails.Pod_warr_rmk = txtwarrremk.Text.ToString();
                    _tmpPoDetails.Pod_warr_per = txtwarrper.Text.ToString();

                    MasterItem _itemdetail = new MasterItem();
                    _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper().Trim());
                    if (_itemdetail.Mi_itm_uom != null)
                    {
                        _tmpPoDetails.Pod_uom = _itemdetail.Mi_itm_uom;
                    }
                    else
                    {
                        _tmpPoDetails.Pod_uom = "";
                    }


                    //_tmpPoDetails.Pod_vat = 0;
                    _tmpPoDetails.Pod_vat_before = 0;
                    _tmpPoDetails.Pod_qt_supp = Session["LowSupplier"].ToString();
                    string princ = Session["LowPrice"].ToString();
                    _tmpPoDetails.Pod_qt_price = Convert.ToDecimal(princ);//(decimal)Session["LowPrice"];
                    // _POItemList.Add(_tmpPoDetails);

                    _tmpPoDetails.Pod_req_no = Session["PRN_No"].ToString();
                    _POItemList.Add(_tmpPoDetails);

                    if (_POItemDel.Count > 0)
                    {
                        //var max_DelQuery = (from tab1 in _POItemDel select tab1.Podi_del_line_no).Max();
                        //_delLineNo = max_DelQuery;
                        //Edit by Chamal 24/06/2013
                        var result = (from rs in _POItemDel where rs.Podi_line_no == _lineNo select rs.Podi_del_line_no).ToList();
                        if (result != null && result.Count > 0)
                        {
                            _delLineNo = Convert.ToInt32(result.Max());
                        }
                        else _delLineNo = 0;
                    }
                    else
                    {
                        _delLineNo = 0;
                    }

                    // Add po defualt delivery details
                    _delLineNo = _delLineNo + 1;
                    _tmpPoDel.Podi_seq_no = 12;
                    _tmpPoDel.Podi_line_no = _lineNo;
                    _tmpPoDel.Podi_del_line_no = _delLineNo;
                    _tmpPoDel.Podi_loca = Session["UserDefLoca"].ToString();
                    _tmpPoDel.Podi_itm_cd = txtItem.Text.Trim();
                    if (ddlStatus.SelectedItem.Text == "CONS")
                    {
                        _tmpPoDel.Podi_itm_stus = "CONS";
                    }
                    else
                    {
                        _tmpPoDel.Podi_itm_stus = ddlStatus.SelectedValue.Trim();
                    }


                    _tmpPoDel.Podi_qty = Convert.ToDecimal(txtQty.Text);
                    _tmpPoDel.Podi_bal_qty = Convert.ToDecimal(txtQty.Text);
                    _tmpPoDel.Podi_ex_dt = Convert.ToDateTime(txtexDate.Text);
                    
                    _POItemDel.Add(_tmpPoDel);
                    if (Session["PRN_No"].ToString() != "")
                    {
                        decimal oldPoQty = 0;
                        Decimal CurrentQty = Convert.ToDecimal(Session["_BQty"].ToString());
                        string PoQty = Session["PoQty"].ToString();
                        Decimal newQty = Convert.ToDecimal(txtQty.Text);
                        if (CurrentQty < newQty)
                        {
                            ErrorMsgPOrder("please add low quantity");
                            txtQty.Text = "";
                            txtUnitPrice.Text = "";
                            txtAmount.Text = "";
                            txtDiscount.Text = "";
                            txtDisRate.Text = "";
                            txtTax.Text = "";
                            txtTotal.Text = "";
                            return;
                        }
                        PurchaseReq _PurchaseReq = new PurchaseReq();
                        _PurchaseReq.Por_act = true;
                        // _PurchaseReq.Por_seq_no = Convert.ToInt32(Session["_Seq"].ToString());
                        _PurchaseReq.Por_cre_by = Session["UserID"].ToString();
                        _PurchaseReq.Por_cre_dt = System.DateTime.Now;
                        _PurchaseReq.Por_itm_cd = txtItem.Text;
                        _PurchaseReq.Por_itm_stus = ddlStatus.SelectedValue;
                        _PurchaseReq.Por_qty = Convert.ToDecimal(txtQty.Text);
                        _PurchaseReq.Por_req_no = Session["PRN_No"].ToString();
                        string lint = Session["_ItemLine"].ToString();
                        _PurchaseReq.Por_req_line = Convert.ToInt32(lint);
                        _PurchaseReqList.Add(_PurchaseReq);
                        ViewState["PurchaseReqList"] = _PurchaseReqList;

                        InventoryRequestItem _Req = new InventoryRequestItem();
                        _Req.Itri_res_no = Session["PRN_No"].ToString();
                        _Req.Itri_itm_cd = txtItem.Text;
                        _Req.Itri_bqty = CurrentQty - newQty;
                        _Req.Itri_itm_stus = ddlStatus.SelectedValue;
                        if ((PoQty == "") || (PoQty == null))
                        {
                            oldPoQty = 0;
                        }
                        else
                        {
                            oldPoQty = Convert.ToDecimal(PoQty);
                        }
                        _Req.Itri_po_qty = newQty + oldPoQty;

                        _InventoryRequestItem = ViewState["InventoryRequestItem"] as List<InventoryRequestItem>;
                        if (_InventoryRequestItem == null)
                        {
                            _InventoryRequestItem = new List<InventoryRequestItem>();
                        }


                        _InventoryRequestItem.Add(_Req);
                        ViewState["InventoryRequestItem"] = _InventoryRequestItem;
                        Session["_BQty"] = _Req.Itri_bqty;
                    }

                }
                else
                {
                    ErrorMsgPOrder("Item details not loading.");
                    return;
                }

                grdPOItems.AutoGenerateColumns = false;
                // grdPOItems.DataSource = new List<PurchaseOrderDetail>();
                grdPOItems.DataSource = _POItemList;
                grdPOItems.DataBind();
                ViewState["POItemList"] = _POItemList;


                _POItemDel = _POItemDel.OrderBy(x => x.Podi_line_no).ToList();
                grdDel.AutoGenerateColumns = false;
                // grdDel.DataSource = new List<PurchaseOrderDelivery>();
                grdDel.DataSource = _POItemDel;
                grdDel.DataBind();
                ViewState["POItemDel"] = _POItemDel;

                grdQuo.AutoGenerateColumns = false;
                grdQuo.DataSource = new List<QoutationDetails>();
                grdQuo.DataBind();
                Cal_Totals();

                clear_item();
                txtSupCode.Enabled = false;
                lbtnSupCode.Visible = false;
                LinkButton1.Visible = false;
                LinkButton1.Enabled = false;
                txtItem.Focus();



                if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10161))
                {
                    txtUnitPrice.Enabled = true;
                }
                else
                {
                    txtUnitPrice.Enabled = false;
                }
    


                txtAmount.Enabled = true;
                txtDisRate.Enabled = true;
                txtDiscount.Enabled = true;
                txtTax.Enabled = true;
                txtTotal.Enabled = true;
                ddlStatus.Enabled = true;
            }
            catch (Exception ex)
            {
                ErrorMsgPOrder("Error Occurred while processing...\n");
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lbtnAddDel_Click(object sender, EventArgs e)
        {
            ClearErrorMsg();
            _POItemDel = ViewState["POItemDel"] as List<PurchaseOrderDelivery>;
            if (!(Session["AddDelQty"] == null))
            {
                _AddDelQty = Convert.ToDecimal(Session["AddDelQty"].ToString());
            }

            try
            {
                if (string.IsNullOrEmpty(lblDelItem.Text))
                {
                    ErrorMsgPOrder("Please select item.");
                    txtDelQty.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtDelQty.Text))
                {
                    ErrorMsgPOrder("Please enter delivery quantity.");
                    txtDelQty.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDelLoca.Text))
                {
                    ErrorMsgPOrder("Please enter delivery location.");

                    txtDelLoca.Focus();
                    return;
                }

                if (_POstatus == "A")
                {
                    ErrorMsgPOrder("Order is already approved.Cannot amend.");

                    txtPurNo.Focus();
                    return;
                }

                if (_POstatus == "C")
                {
                    ErrorMsgPOrder("Order is already cancelled.Cannot amend.");

                    txtPurNo.Focus();
                    return;
                }

                decimal _delQty = 0;
                decimal _qty = Convert.ToDecimal(txtDelQty.Text);

                foreach (PurchaseOrderDelivery _tmpDel in _POItemDel)
                {
                    if (_tmpDel.Podi_line_no == Convert.ToInt32(lblItemLine.Text) && _tmpDel.Podi_loca == txtDelLoca.Text.Trim())
                    {
                        ErrorMsgPOrder("Already existing same item with same location. Please remove that and add again.");

                        return;
                    }

                    if (_tmpDel.Podi_line_no == Convert.ToInt32(lblItemLine.Text) && _tmpDel.Podi_itm_cd == lblDelItem.Text)
                    {
                        _delQty = _delQty + _tmpDel.Podi_qty;
                    }
                }

                if (_AddDelQty < (_qty + _delQty))
                {
                    ErrorMsgPOrder("Delivery schedule quantity exceeds purchase quantity.");
                    txtDelQty.Text = _AddDelQty.ToString();
                    txtDelQty.Focus();
                    return;
                }


                PurchaseOrderDelivery _tmpPoDel = new PurchaseOrderDelivery();

                if (_POItemDel.Count > 0)
                {
                    var query = _POItemDel.Where(a => a.Podi_line_no == Convert.ToInt32(lblItemLine.Text)).Select(x => x.Podi_del_line_no).ToList();

                    if (query.Count > 0)
                    {
                        var max_DelQuery =
                          (from tab1 in _POItemDel
                           where tab1.Podi_line_no == Convert.ToInt32(lblItemLine.Text)
                           select tab1.Podi_del_line_no).Max();

                        _delLineNo = max_DelQuery;
                    }
                    else
                    {
                        _delLineNo = 0;
                    }

                }
                else
                {
                    _delLineNo = 0;
                }

                _delLineNo = _delLineNo + 1;
                _tmpPoDel.Podi_seq_no = 12;
                _tmpPoDel.Podi_line_no = Convert.ToInt32(lblItemLine.Text);
                _tmpPoDel.Podi_del_line_no = _delLineNo;
                _tmpPoDel.Podi_loca = txtDelLoca.Text.Trim();
                _tmpPoDel.Podi_itm_cd = lblDelItem.Text.Trim();
                _tmpPoDel.Podi_itm_stus = lblDelStatus.Text.Trim();
                _tmpPoDel.Podi_qty = Convert.ToDecimal(txtDelQty.Text);
                _tmpPoDel.Podi_bal_qty = Convert.ToDecimal(txtDelQty.Text);
                _tmpPoDel.Podi_ex_dt = Convert.ToDateTime(txtexDate.Text);
                _POItemDel.Add(_tmpPoDel);
                _POItemDel = _POItemDel.OrderBy(x => x.Podi_line_no).ToList();
                grdDel.AutoGenerateColumns = false;
                grdDel.DataSource = new List<PurchaseOrderDelivery>();
                grdDel.DataSource = _POItemDel;
                grdDel.DataBind();
                ViewState["POItemDel"] = _POItemDel;
                if (_AddDelQty - (_qty + _delQty) > 0)
                {
                    txtDelQty.Text = Convert.ToString(_AddDelQty - (_qty + _delQty));
                    txtDelLoca.Text = "";
                    txtDelLoca.Focus();
                }
                else if (_AddDelQty - (_qty + _delQty) == 0)
                {
                    lblDelItem.Text = "";
                    lblDelStatus.Text = "";
                    lblItemLine.Text = "";
                    txtDelQty.Text = "";
                    txtDelLoca.Text = "";
                }
            }
            catch (Exception ex)
            {
                ErrorMsgPOrder("Error Occurred while processing...\n");
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        protected void lbtnAddDelAll_Click(object sender, EventArgs e)
        {
            ClearErrorMsg();
            _POItemDel = ViewState["POItemDel"] as List<PurchaseOrderDelivery>;
            _POItemList = ViewState["POItemList"] as List<PurchaseOrderDetail>;
            try
            {
                if (!(grdPOItems.Rows.Count > 0))
                {
                    ErrorMsgPOrder("Please select Item.");

                    txtDelLoca.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtDelLoca.Text))
                {
                    ErrorMsgPOrder("Please select relavant delivery location.");

                    txtDelLoca.Focus();
                    return;
                }

                if (_POstatus == "A")
                {
                    ErrorMsgPOrder("Order is already approved.Cannot amend.");

                    txtPurNo.Focus();
                    return;
                }

                if (_POstatus == "C")
                {
                    ErrorMsgPOrder("Order is already cancelled.Cannot amend.");

                    txtPurNo.Focus();
                    return;
                }

                _delLineNo = 0;
                _POItemDel = new List<PurchaseOrderDelivery>();
                PurchaseOrderDelivery _tmpPoDel = new PurchaseOrderDelivery();

                foreach (PurchaseOrderDetail _tmpList in _POItemList)
                {
                    if (_POItemDel.Count > 0)
                    {
                        var max_DelQuery =
                        (from tab1 in _POItemDel
                         select tab1.Podi_del_line_no).Max();

                        _delLineNo = max_DelQuery;
                    }
                    else
                    {
                        _delLineNo = 0;
                    }
                    _tmpPoDel = new PurchaseOrderDelivery();
                    _delLineNo = _delLineNo + 1;
                    _tmpPoDel.Podi_seq_no = 12;
                    _tmpPoDel.Podi_line_no = _tmpList.Pod_line_no;
                    _tmpPoDel.Podi_del_line_no = _delLineNo;
                    _tmpPoDel.Podi_loca = txtDelLoca.Text.Trim();
                    _tmpPoDel.Podi_itm_cd = _tmpList.Pod_itm_cd;
                    _tmpPoDel.Podi_itm_stus = _tmpList.Pod_itm_stus;
                    _tmpPoDel.Podi_qty = _tmpList.Pod_qty;
                    _tmpPoDel.Podi_bal_qty = _tmpList.Pod_qty;
                    _tmpPoDel.Podi_ex_dt = Convert.ToDateTime(txtexDate.Text);
                    _POItemDel.Add(_tmpPoDel);
                }
                _POItemDel = _POItemDel.OrderBy(x => x.Podi_line_no).ToList();
                grdDel.AutoGenerateColumns = false;
                grdDel.DataSource = new List<PurchaseOrderDelivery>();
                grdDel.DataSource = _POItemDel;
                grdDel.DataBind();
                ViewState["POItemDel"] = _POItemDel;
            }
            catch (Exception ex)
            {
                ErrorMsgPOrder("Error Occurred while processing...\n");
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lbtnItemDelete_Click(object sender, EventArgs e)
        {
            ClearErrorMsg();
            bool result = false;
            _POItemDel = ViewState["POItemDel"] as List<PurchaseOrderDelivery>;
            _POItemList = ViewState["POItemList"] as List<PurchaseOrderDetail>;
            _PurchaseReqList = ViewState["PurchaseReqList"] as List<PurchaseReq>;
            _InventoryRequestItem = ViewState["InventoryRequestItem"] as List<InventoryRequestItem>;
            if (txtRemoveconformmessageValue.Value == "Yes")
            {
                foreach (GridViewRow dgvr in grdPOItems.Rows)
                {
                    CheckBox chk = (CheckBox)dgvr.FindControl("chk_POItems");

                    if (chk != null & chk.Checked)
                    {
                        Int32 _line = Convert.ToInt32((dgvr.FindControl("col_seq") as Label).Text);
                        string _Item = (dgvr.FindControl("col_Item") as Label).Text;

                        List<PurchaseOrderDetail> _temp = new List<PurchaseOrderDetail>();
                        _temp = _POItemList;

                        _temp.RemoveAll(x => x.Pod_line_no == _line);
                        _POItemList = _temp;

                        grdPOItems.AutoGenerateColumns = false;
                        grdPOItems.DataSource = new List<PurchaseOrderDetail>();
                        grdPOItems.DataSource = _POItemList;
                        grdPOItems.DataBind();
                        ViewState["POItemList"] = _POItemList;

                        if (_POItemDel == null || _POItemDel.Count == 0) return;

                        List<PurchaseOrderDelivery> _tmpDel = new List<PurchaseOrderDelivery>();
                        _tmpDel = _POItemDel;

                        _tmpDel.RemoveAll(x => x.Podi_line_no == _line);
                        _POItemDel = _tmpDel;

                        _POItemDel = _POItemDel.OrderBy(x => x.Podi_line_no).ToList();
                        grdDel.AutoGenerateColumns = false;
                        grdDel.DataSource = new List<PurchaseOrderDelivery>();
                        grdDel.DataSource = _POItemDel;
                        grdDel.DataBind();
                        ViewState["POItemDel"] = _POItemDel;
                        Cal_Totals();

                        List<PurchaseReq> _Req = new List<PurchaseReq>();
                        _Req = _PurchaseReqList;
                        if (_Req != null)
                        {
                            _Req.RemoveAll(x => x.Por_itm_cd == _Item);
                            _PurchaseReqList = _Req;
                            ViewState["PurchaseReqList"] = _PurchaseReqList;
                        }


                        List<InventoryRequestItem> _DelPoReqItem = new List<InventoryRequestItem>();
                        _DelPoReqItem = _InventoryRequestItem;
                        if (_DelPoReqItem != null)
                        {
                            _DelPoReqItem.RemoveAll(x => x.Itri_itm_cd == _Item);
                            _InventoryRequestItem = _DelPoReqItem;
                            ViewState["InventoryRequestItem"] = _InventoryRequestItem;
                        }



                        if (_POItemList.Count == 0)
                        {
                            txtSupCode.Enabled = true;
                            lbtnSupCode.Visible = true;
                            LinkButton1.Visible = true;
                            LinkButton1.Enabled = true;
                        }
                        result = true;
                    }

                }
                if (result == false)
                {
                    ErrorMsgPOrder("Please select item  to delete!");
                }
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cacel Delete!')", true);
            }
        }

        protected void lbtngrdDele_Click(object sender, EventArgs e)
        {
            ClearErrorMsg();
            bool result = false;
            _POItemDel = ViewState["POItemDel"] as List<PurchaseOrderDelivery>;
            _POItemList = ViewState["POItemList"] as List<PurchaseOrderDetail>;
            if (txtRemoveconformmessageValue.Value == "Yes")
            {
                foreach (GridViewRow dgvr in grdDel.Rows)
                {
                    CheckBox chk = (CheckBox)dgvr.FindControl("chk_Deltems");

                    if (chk != null & chk.Checked)
                    {

                        if (_POItemDel == null || _POItemDel.Count == 0) return;

                        Int32 _Dline = Convert.ToInt32((dgvr.FindControl("col_dNo") as Label).Text);
                        Int32 _line = Convert.ToInt32((dgvr.FindControl("col_dLNo") as Label).Text);


                        List<PurchaseOrderDelivery> _temp = new List<PurchaseOrderDelivery>();
                        _temp = _POItemDel;

                        _temp.RemoveAll(x => x.Podi_del_line_no == _Dline && x.Podi_line_no == _line);
                        _POItemDel = _temp;
                        _POItemDel = _POItemDel.OrderBy(x => x.Podi_line_no).ToList();
                        grdDel.AutoGenerateColumns = false;
                        grdDel.DataSource = new List<PurchaseOrderDelivery>();
                        grdDel.DataSource = _POItemDel;
                        grdDel.DataBind();
                        ViewState["POItemDel"] = _POItemDel;
                        result = true;
                    }

                }
                if (result == false)
                {
                    ErrorMsgPOrder("Please select item  to delete!");
                }
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cacel Delete!')", true);
            }
        }
        protected void lbtnPOItemsAdd_Click(object sender, EventArgs e)
        {
            ClearErrorMsg();
            if (grdPOItems.Rows.Count == 0) return;
            _AddDelQty = 0;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {

                string _line = (row.FindControl("col_seq") as Label).Text;
                string _itm = (row.FindControl("col_Item") as Label).Text;
                string _status = (row.FindControl("col_Status") as Label).Text;
                _AddDelQty = Convert.ToDecimal((row.FindControl("col_Qty") as Label).Text);

                lblItemLine.Text = _line.ToString();
                lblDelItem.Text = _itm.ToString();
                lblDelStatus.Text = _status.ToString();

                decimal _delQty = 0;
                decimal _balDelQty = 0;

                foreach (PurchaseOrderDelivery _tmpDel in _POItemDel)
                {
                    if (_tmpDel.Podi_line_no == Convert.ToInt32(lblItemLine.Text) && _tmpDel.Podi_itm_cd == lblDelItem.Text)
                    {
                        _delQty = _delQty + _tmpDel.Podi_qty;
                    }
                }

                _balDelQty = _AddDelQty - _delQty;
                txtDelQty.Text = _balDelQty.ToString();
                Session["AddDelQty"] = _AddDelQty;
            }


        }
        #endregion

        #region Modalpopup
        protected void grdItemQ_SelectedIndexChanged(object sender, EventArgs e)
        {

            //txtUnitPrice.Text = (grdItemQ.FindControl("qd_unit_price") as Label).Text;
            //txtQty.Text = (grdItemQ.FindControl("qd_to_qty") as Label).Text;
            txtSupCode.Text = (grdItemQ.SelectedRow.FindControl("qh_party_cd") as Label).Text;
            txtUnitPrice.Text = (grdItemQ.SelectedRow.FindControl("qd_unit_price") as Label).Text;
            txtQty.Text = Session["qty"].ToString();
            txtItem.Text = Session["Qu__Item"].ToString();
            if (!string.IsNullOrEmpty(txtItem.Text))
            {
                MasterItem _mstItm = CHNLSVC.General.GetItemMaster(txtItem.Text.ToUpper());
                if (_mstItm != null)
                {
                    txtItmDes.Text = _mstItm.Mi_shortdesc;
                }
            }
            txtDiscount.Text = "0.0";
            txtDisRate.Text = "0";
            ddlStatus.SelectedItem.Text = "GOOD";

            decimal _unitVal = 0;
            decimal _qty = 0;
            decimal _amt = 0;
            decimal _disRate = 0;
            decimal _disAmount = 0;
            decimal _tax = 0;
            decimal _total = 0;
            Boolean _correct = false;


            _unitVal = Convert.ToDecimal(txtUnitPrice.Text);
            _qty = Convert.ToDecimal(txtQty.Text);
            _amt = _unitVal * _qty;
            _disRate = Convert.ToDecimal(txtDisRate.Text);
            _disAmount = _amt * _disRate / 100;
            _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtSupCode.Text.Trim(), null, null, "S");
            if (_supDet.Mbe_is_tax == true)
            {
                _tax = TaxCalculation(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), ddlStatus.SelectedValue, _amt - _disAmount, 0);
            }
            _total = (_amt - _disAmount) + _tax;
            txtAmount.Text = _amt.ToString();
            txtTax.Text = _tax.ToString("n");
            txtTotal.Text = _total.ToString("n");







            Session["QuItem"] = "Q";
            txtUnitPrice.Enabled = false;
            txtAmount.Enabled = false;
            txtDisRate.Enabled = false;
            txtDiscount.Enabled = false;
            txtTax.Enabled = false;
            txtTotal.Enabled = false;
            ddlStatus.Enabled = false;
            txtSupCode.Enabled = false;
            LinkButton1.Visible = false;
            lbtnSupCode.Visible = false;
        }
        protected void grdItemQ_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            grdItemQ.PageIndex = e.NewPageIndex;
            DataTable _Result;
            if (lblvalue.Text == "Q_S")
            {
                _Result = CHNLSVC.Inventory.GetItemsQuotation(Session["UserCompanyCode"].ToString(), "S", "N", txtReqSupplerS.Text, Session["Qu__Item"].ToString(), "A");
                lblvalue.Text = "Q_S";
                grdItemQ.DataSource = _Result;
                grdItemQ.DataBind();
                QuotationPopup.Show();
                return;
            }
            else if (lblvalue.Text == "Q_")
            {
                _Result = CHNLSVC.Inventory.GetItemsQuotation(Session["UserCompanyCode"].ToString(), "S", "N", "", Session["Qu__Item"].ToString(), "A");
                lblvalue.Text = "Q_";
                grdItemQ.DataSource = _Result;
                grdItemQ.DataBind();
                QuotationPopup.Show();
                return;

            }

        }


        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string Des = grdResult.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "16")
            {
                // string _curr = grdResult.SelectedRow.Cells[3].Text;

                txtSupCode.Text = Des;
                txtReqSupplerS.Text = Des;
                decimal _curExRate = 0;
                List<MasterBusinessEntity> supplierlist = CHNLSVC.Sales.GetCustomerDetailList(Session["UserCompanyCode"].ToString(), txtSupCode.Text, string.Empty, string.Empty, "S");
                _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtSupCode.Text.Trim(), null, null, "S");
                if (supplierlist != null || supplierlist.Count > 1)
                {
                    foreach (var _nicCust in supplierlist)
                    {
                        if (ddlMainType.SelectedValue == "L")
                        {
                            Session["SUPPLER_CURRENCY"] = "LKR";
                            txtCurrency.Text = "LKR";
                            _curExRate = 1;
                            lblEx.Text = _curExRate.ToString("0.00");
                            lbtnSave.Enabled = true;
                            txtCreditPeriod.Text = _nicCust.MBE_CR_PERIOD.ToString();
                        }
                        else
                        {
                            Session["SUPPLER_CURRENCY"] = _nicCust.Mbe_cur_cd;
                            txtCurrency.Text = _nicCust.Mbe_cur_cd;
                            // ExchangeRate();
                            LoadExRate();
                            txtCreditPeriod.Text = _nicCust.MBE_CR_PERIOD.ToString();
                        }

                    }
                }
                lblvalue.Text = "c";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "14")
            {
                lblvalue.Text = "c";
                txtCurrency.Text = Des;
                ExchangeRate();
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "293")
            {
                lblvalue.Text = "c";
                txtPurNo.Text = Des;
                UserPopoup.Hide();
                if (!string.IsNullOrEmpty(txtPurNo.Text))
                {
                    if (ddlMainType.SelectedValue == "0")
                    {
                        ErrorMsgPOrder("Please Select Type");
                        return;
                    }

                    LoadSaveDocument();
                }
                return;
            }
            if (lblvalue.Text == "275")
            {
                lblvalue.Text = "c";
                txtJob.Text = Des;
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "3")
            {
                ItemTax_Validate();
                lblvalue.Text = "c";
                txtItem.Text = Des;
                if (!string.IsNullOrEmpty(txtItem.Text))
                {
                    MasterItem _mstItm = CHNLSVC.General.GetItemMaster(txtItem.Text.ToUpper());
                    if (_mstItm != null)
                    {
                        txtItmDes.Text = _mstItm.Mi_shortdesc;
                    }
                }
                //Warra remk

                DataTable _dtwarr = CHNLSVC.Inventory.GetWarrRangeRmk(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper(), txtSupCode.Text.ToString());
                if (_dtwarr != null && _dtwarr.Rows.Count > 0)
                {
                    txtwarrper.Text = _dtwarr.Rows[0][0].ToString();
                    txtwarrremk.Text = _dtwarr.Rows[0][1].ToString();

                }
                else
                {
                    txtwarrper.Text = "";
                    txtwarrremk.Text = "";
                }

                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "17")
            {
                lblvalue.Text = "c";
                ddlStatus.SelectedItem.Text = Des;
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "5")
            {
                lblvalue.Text = "c";
                txtDelLoca.Text = Des;
                UserPopoup.Hide();
                return;
            }
            else if (lblvalue.Text == "6")
            {
                lblvalue.Text = "c";
                txtprofitCenter.Text = Des;
                UserPopoup.Hide();
                return;
            }
            UserPopoup.Hide();
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            grdResult.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "16")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable _result = CHNLSVC.Inventory.GetSupplierData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "16";
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "14")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                DataTable _result = CHNLSVC.CommonSearch.GetCurrencyData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "14";
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "293")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.POByDate);
                DateTime dtTemp = DateTime.Today.AddMonths(-1);
                DataTable _result = CHNLSVC.Inventory.GetPurchaseOrders(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString(), dtTemp, DateTime.Today);
                txtFrom.Text = dtTemp.ToShortDateString();
                txtTo.Text = DateTime.Today.ToShortDateString();
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "293";
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "275")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
                DateTime dtTemp = DateTime.Today.AddMonths(-1);
                DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString(), dtTemp, DateTime.Today);
                txtFrom.Text = dtTemp.ToShortDateString();
                txtTo.Text = DateTime.Today.ToShortDateString();
                grdResult.DataSource = _result;
                grdResult.DataBind(); ;
                lblvalue.Text = "275";
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "3")
            {
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                // DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PurchaseOrder);
                DataTable _result = CHNLSVC.Inventory.GetSuppierItem(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "3";
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "17")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "17";
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "5")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "5";
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "6")
            {
                grdResult.DataSource = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "6";
                grdResult.PageIndex = 0;
                UserPopoup.Show();
            }

        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {

            if (lblvalue.Text == "16")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable _result = CHNLSVC.Inventory.GetSupplierData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "16";
                UserPopoup.Show();
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                return;
            }
            if (lblvalue.Text == "14")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                DataTable _result = CHNLSVC.CommonSearch.GetCurrencyData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "14";
                UserPopoup.Show();
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                return;
            }
            if (lblvalue.Text == "293")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.POByDate);
                DateTime dtTemp = DateTime.Today.AddMonths(-1);
                DataTable _result = CHNLSVC.Inventory.GetPurchaseOrders(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString(), dtTemp, DateTime.Today);
                txtFrom.Text = dtTemp.ToShortDateString();
                txtTo.Text = DateTime.Today.ToShortDateString();
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "293";
                UserPopoup.Show();
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                return;
            }
            if (lblvalue.Text == "275")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
                DateTime dtTemp = DateTime.Today.AddMonths(-1);
                DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString(), dtTemp, DateTime.Today);
                txtFrom.Text = dtTemp.ToShortDateString();
                txtTo.Text = DateTime.Today.ToShortDateString();
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "275";
                UserPopoup.Show();
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                return;
            }
            if (lblvalue.Text == "3")
            {
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                // DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PurchaseOrder);
                DataTable _result = CHNLSVC.Inventory.GetSuppierItem(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "3";
                UserPopoup.Show();
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                return;
            }
            if (lblvalue.Text == "17")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "17";
                UserPopoup.Show();
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                return;
            }
            if (lblvalue.Text == "5")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "5";
                UserPopoup.Show();
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                return;
            }
            else if (lblvalue.Text == "6")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "6";
                UserPopoup.Show();
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                return;
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {

            if (lblvalue.Text == "c")
            {
                UserPopoup.Hide();
                lblvalue.Text = "";
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                return;
            }
            if (lblvalue.Text == "16")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable _result = CHNLSVC.Inventory.GetSupplierData(SearchParams, ddlSearchbykey.SelectedItem.ToString(),  txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "16";
                UserPopoup.Show();
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                return;
            }
            if (lblvalue.Text == "14")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                DataTable _result = CHNLSVC.CommonSearch.GetCurrencyData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "14";
                UserPopoup.Show();
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                return;
            }
            if (lblvalue.Text == "293")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.POByDate);
                DateTime dtTemp = DateTime.Today.AddMonths(-1);
                DataTable _result = CHNLSVC.Inventory.GetPurchaseOrders(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString(), dtTemp, DateTime.Today);
                txtFrom.Text = dtTemp.ToShortDateString();
                txtTo.Text = DateTime.Today.ToShortDateString();
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "293";
                UserPopoup.Show();
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                return;
            }
            if (lblvalue.Text == "275")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
                DateTime dtTemp = DateTime.Today.AddMonths(-1);
                DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString(), dtTemp, DateTime.Today);
                txtFrom.Text = dtTemp.ToShortDateString();
                txtTo.Text = DateTime.Today.ToShortDateString();
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "275";
                UserPopoup.Show();
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                return;
            }
            if (lblvalue.Text == "3")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PurchaseOrder);
                DataTable _result = CHNLSVC.Inventory.GetSuppierItem(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "3";
                UserPopoup.Show();
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                return;
            }
            if (lblvalue.Text == "17")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "17";
                UserPopoup.Show();
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                return;
            }
            if (lblvalue.Text == "5")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "5";
                UserPopoup.Show();
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                return;
            }
            else if (lblvalue.Text == "6")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "6";
                UserPopoup.Show();
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                return;
            }
        }
        #endregion

        #region Search Button
        protected void chk_Req_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow Row = ((GridViewRow)((Control)sender).Parent.Parent);
            CheckBox chkSelect = (CheckBox)Row.FindControl("chk_Req");
            bool select = chkSelect.Checked;
            if (!chkMultiReq.Checked)
            {
                foreach (GridViewRow row in grdReq.Rows)
                {
                    CheckBox chk_Req = (CheckBox)row.FindControl("chk_Req");
                    if (chk_Req.Checked == true)
                    {
                        chk_Req.Checked = false;
                    }
                }
                if (select)
                {
                    chkSelect.Checked = true;
                }
            }
            _itmData = new DataTable();

            foreach (GridViewRow row in grdReq.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_Req") as CheckBox);
                    if (chkRow.Checked)
                    {
                        string _ReqNo = (row.FindControl("col_ReqNo") as Label).Text;
                        FilterData(_ReqNo);
                    }
                }
            }
        }
        protected void chk_ReqItem_CheckedChanged(object sender, EventArgs e)
        {
            ClearErrorMsg();
            GridViewRow Row = ((GridViewRow)((Control)sender).Parent.Parent);
            CheckBox chkSelect = (CheckBox)Row.FindControl("chk_ReqItem");
            bool select = chkSelect.Checked;
            if (!chkMultiReq.Checked)
            {
                foreach (GridViewRow row in grdReqItems.Rows)
                {
                    CheckBox chk_ReqItem = (CheckBox)row.FindControl("chk_ReqItem");
                    if (chk_ReqItem.Checked == true)
                    {
                        chk_ReqItem.Checked = false;
                    }
                }
                if (select)
                {
                    chkSelect.Checked = true;
                }
            }
            foreach (GridViewRow row in grdReqItems.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_ReqItem") as CheckBox);
                    if (chkRow.Checked)
                    {
                        Label _Reqno = (row.Cells[0].FindControl("col_ReqINo") as Label);
                        Label _ItemLine = (row.Cells[0].FindControl("col_ItemLine") as Label);
                        Label _reqItem = (row.Cells[0].FindControl("col_reqItem") as Label);
                        Label _ItemStatus = (row.Cells[0].FindControl("col_ItemStatus") as Label);
                        Label _Seq = (row.Cells[0].FindControl("col_Seq") as Label);
                        Label _BQty = (row.Cells[0].FindControl("col_BQty") as Label);
                        Label _PoQty = (row.Cells[0].FindControl("col_poqty") as Label);
                        txtItem.Text = _reqItem.Text;
                        if (!string.IsNullOrEmpty(txtItem.Text))
                        {
                            MasterItem _mstItm = CHNLSVC.General.GetItemMaster(txtItem.Text.ToUpper());
                            if (_mstItm != null)
                            {
                                txtItmDes.Text = _mstItm.Mi_shortdesc;
                            }
                        }
                        ddlStatus.SelectedValue = _ItemStatus.Text;
                        QDetailDiv.Enabled = false;
                        pnlReq.Enabled = false;
                        Session["_ItemLine"] = _ItemLine.Text;
                        Session["_Seq"] = _Seq.Text;
                        Session["_BQty"] = _BQty.Text;
                        Session["PRN_No"] = _Reqno.Text;
                        Session["PoQty"] = _PoQty.Text;

                        divReq.Enabled = false;
                        return;
                    }
                    else
                    {

                        Session["_ItemLine"] = "";
                        Session["_Seq"] = "";
                        Session["_BQty"] = "";
                        Session["PRN_No"] = "";
                    }

                }
            }
        }

        protected void chk_Quo_CheckedChanged(object sender, EventArgs e)
        {
            ClearErrorMsg();
            foreach (GridViewRow row in grdQuo.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chk_Quo") as CheckBox);
                    if (chkRow.Checked)
                    {
                        Label _reqItem = (row.Cells[0].FindControl("col_qPrice") as Label);
                        // decimal _uPrice = Convert.ToDecimal(row.Cells[0].FindControl("col_qPrice") as Label);

                        // txtUnitPrice.Text = _uPrice.ToString("n");
                        txtUnitPrice.Text = _reqItem.Text;
                        txtUnitPrice.Focus();


                    }
                }
            }
        }
        protected void allchk_RqItem_CheckedChanged(object sender, EventArgs e)
        {
            //foreach (GridViewRow row in grdReqItems.Rows)
            //{
            //    if (row.RowType == DataControlRowType.DataRow)
            //    {
            //        CheckBox chkRow = (row.Cells[0].FindControl("allchk_RqItem") as CheckBox);
            //        if (chkRow.Checked)
            //        {

            //            Label _reqItem = (row.Cells[0].FindControl("col_reqItem") as Label);
            //            Label _ItemStatus = (row.Cells[0].FindControl("col_ItemStatus") as Label);
            //            txtItem.Text = _reqItem.Text;
            //            ddlStatus.Text = _ItemStatus.Text;

            //        }
            //    }
            //}
        }
        protected void lbtnSupCode_Click(object sender, EventArgs e)
        {
            ClearErrorMsg();
            if (!(ddlMainType.SelectedIndex > 0))
            {
                ErrorMsgPOrder("Please select Type..!");
                return;
            }
            try
            {
                if (txtItem.Text != "")
                {
                    txtSupCode.Text = "";
                    string SearchParams1 = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PurchaseOrder);
                    DataTable _result1 = CHNLSVC.Inventory.GetSuppierItem(SearchParams1, "Item", "%" + txtItem.Text);
                    grdResult.DataSource = _result1;
                    grdResult.DataBind();
                    BindUCtrlDDLData(_result1);
                    lblvalue.Text = "16";
                    UserPopoup.Show();
                    return;
                }

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable _result = CHNLSVC.Inventory.GetSupplierData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "16";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                ErrorMsgPOrder("Error Occurred while processing...\n");

            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lbtnCurrency_Click(object sender, EventArgs e)
        {
            try
            {
                ClearErrorMsg();
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                DataTable _result = CHNLSVC.CommonSearch.GetCurrencyData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "14";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                ErrorMsgPOrder("Error Occurred while processing...\n");
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lbtnPurNo_Click(object sender, EventArgs e)
        {
            try
            {
                ClearErrorMsg();
                DateTime dtTemp = DateTime.Today.AddMonths(-1);
                txtFDate.Text = dtTemp.ToShortDateString();
                txtTDate.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.POByDate);
                DataTable _result = CHNLSVC.Inventory.GetPurchaseOrders(SearchParams, null, null, dtTemp, DateTime.Now.Date);
                txtFrom.Text = dtTemp.ToShortDateString();
                txtTo.Text = DateTime.Today.ToShortDateString();
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                BindUCtrlDDLData2(_result);
                ddlSearchbykeyD.Items.FindByText("Date").Enabled = false;
                ddlSearchbykeyD.Items.FindByText("Status").Enabled = false;
                lblvalue.Text = "293";
                ViewState["SEARCH"] = _result;
                //UserPopoup.Show();
                UserDPopoup.Show();

            }
            catch (Exception ex)
            {
                ErrorMsgPOrder("Error Occurred while processing...\n");
                CHNLSVC.CloseChannel();
            }

        }
        protected void lbtntJob_Click(object sender, EventArgs e)
        {
            try
            {
                ClearErrorMsg();
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
                DateTime dtTemp = DateTime.Today.AddMonths(-1);
                DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(SearchParams, null, null, dtTemp, DateTime.Today);
                txtFDate.Text = dtTemp.ToShortDateString();
                txtTDate.Text = DateTime.Today.ToShortDateString();
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                BindUCtrlDDLData2(_result);
                lblvalue.Text = "275";
                //Session["Job"] = "Job";
                UserDPopoup.Show();
            }
            catch (Exception ex)
            {
                ErrorMsgPOrder("Error Occurred while processing...\n");
                CHNLSVC.CloseChannel();
            }

        }



        protected void lbtnItemSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ClearErrorMsg();
                txtQty.Text = "";
                txtUnitPrice.Text = "";
                txtAmount.Text = "";
                txtDisRate.Text = "";
                txtDiscount.Text = "";
                txtTax.Text = "";
                txtTotal.Text = "";



                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PurchaseOrder);
                DataTable _result = CHNLSVC.Inventory.GetSuppierItem(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
               // ddlSearchbykey.Items.FindByText("Description").Enabled = false;
                lblvalue.Text = "3";
                UserPopoup.Show();

            }
            catch (Exception ex)
            {
                ErrorMsgPOrder("Error Occurred while processing...");
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }
        protected void lbtnstatusSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ClearErrorMsg();
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    ErrorMsgPOrder("First you have to select item.");
                    txtItem.Focus();
                    return;
                }
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "17";
                UserPopoup.Show();
            }

            catch (Exception ex)
            {
                ErrorMsgPOrder("Error Occurred while processing...\n");
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }


        }

        protected void lbtnDelLoca_Click(object sender, EventArgs e)
        {
            try
            {
                ClearErrorMsg();
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "5";
                UserPopoup.Show();

            }
            catch (Exception ex)
            {
                ErrorMsgPOrder("Error Occurred while processing...\n");
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lbtnprofitCenter_Click(object sender, EventArgs e)
        {
            try
            {
                ClearErrorMsg();
                grdResult.DataSource = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "6";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                ErrorMsgPOrder("Error Occurred while processing...\n");
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lbtnView_Click(object sender, EventArgs e)
        {
            DataTable _result;
            _itmData = new DataTable();
            ClearErrorMsg();
            string _dateSelector = chkReqDate.Checked ? "R" : "M";

            DateTime _dtFrom = Convert.ToDateTime(txtFrom.Text);
            DateTime _tmpdtFrom = new DateTime(_dtFrom.Year, _dtFrom.Month, _dtFrom.Day, 0, 0, 0);

            DateTime _dtTo = Convert.ToDateTime(txtTo.Text);
            DateTime _tmpdtTo = new DateTime(_dtTo.Year, _dtTo.Month, _dtTo.Day, 12, 0, 0);
            _tmpdtTo = _tmpdtTo.AddDays(1);
            if ((chkAll.Checked == false) && (txtprofitCenter.Text == string.Empty) && (txtReqSupplerS.Text == string.Empty))
            {
                ErrorMsgPOrder("Please select search option");
                return;
            }
            if (chkAll.Checked == true)
            {
                //Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text)
                _result = CHNLSVC.Inventory.GetRequestItems(Session["UserCompanyCode"].ToString(), "PRQ", "", "", _tmpdtFrom, _tmpdtTo, "A", null, null, _dateSelector);
                DataTable RemoveDup = new DataTable();
                RemoveDup = _result;
                RemoveDup = RemoveDup.DefaultView.ToTable(true, "itr_req_no", "itr_dt", "itr_loc");
                grdReq.DataSource = RemoveDup;
                grdReq.DataBind();
                grdReqItems.DataSource = _result;
                grdReqItems.DataBind();
                ViewState["ReqItem"] = _result;
                return;
            }
            else if ((txtprofitCenter.Text != string.Empty) && (txtReqSupplerS.Text == string.Empty))
            {
                _result = CHNLSVC.Inventory.GetRequestItems(Session["UserCompanyCode"].ToString(), "PRQ", txtprofitCenter.Text, "", _tmpdtFrom, _tmpdtTo, "A", "S", "N", _dateSelector);
                DataTable RemoveDup = new DataTable();
                RemoveDup = _result;
                RemoveDup = RemoveDup.DefaultView.ToTable(true, "itr_req_no", "itr_dt", "itr_loc");
                grdReq.DataSource = RemoveDup;
                grdReq.DataBind();
                grdReqItems.DataSource = _result;
                grdReqItems.DataBind();
                ViewState["ReqItem"] = _result;
                return;
            }
            else if ((txtprofitCenter.Text != string.Empty) && (txtReqSupplerS.Text != string.Empty))
            {
                _result = CHNLSVC.Inventory.GetRequestItems(Session["UserCompanyCode"].ToString(), "PRQ", txtprofitCenter.Text, txtReqSupplerS.Text, _tmpdtFrom, _tmpdtTo, "A", "S", "N", _dateSelector);
                DataTable RemoveDup = new DataTable();
                RemoveDup = _result;
                RemoveDup = RemoveDup.DefaultView.ToTable(true, "itr_req_no", "itr_dt", "itr_loc");
                grdReq.DataSource = RemoveDup;
                grdReq.DataBind();
                grdReqItems.DataSource = _result;
                grdReqItems.DataBind();
                ViewState["ReqItem"] = _result;
                return;
            }
            else if (txtReqSupplerS.Text != string.Empty)
            {
                _result = CHNLSVC.Inventory.GetRequestItems(Session["UserCompanyCode"].ToString(), "PRQ", "", txtReqSupplerS.Text, _tmpdtFrom, _tmpdtTo, "A", "S", "N", _dateSelector);
                DataTable RemoveDup = new DataTable();
                RemoveDup = _result;
                RemoveDup = RemoveDup.DefaultView.ToTable(true, "itr_req_no", "itr_dt", "itr_loc");
                grdReq.DataSource = RemoveDup;
                grdReq.DataBind();
                grdReqItems.DataSource = _result;
                grdReqItems.DataBind();
                ViewState["ReqItem"] = _result;
                return;
            }
            CheckBox allPOItems = (CheckBox)grdReqItems.HeaderRow.FindControl("allPOItems");
            allPOItems.Visible = false;
            if (chkMultiReq.Checked)
            {
                if (allPOItems != null && grdReqItems.Rows.Count > 1)
                {
                    allPOItems.Visible = true;
                }
            }
            ErrorMsgPOrder("No Data Found");
        }

        protected void lbtnReqItemsQ_Click(object sender, EventArgs e)
        {
            ClearErrorMsg();
            _POItemList = ViewState["POItemList"] as List<PurchaseOrderDetail>;
            if (grdReqItems.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                DataTable _Result;
                string _Item = (row.FindControl("col_reqItem") as Label).Text;
                string _qty = (row.FindControl("col_BQty") as Label).Text;

                if (ViewState["POItemList"] != null)
                {
                    var currrange = (from cur in _POItemList
                                     where cur.Pod_itm_cd == _Item
                                     select cur).ToList();
                    if (currrange.Count > 0)// ||currrange !=null)
                    {
                        ErrorMsgPOrder("Selected item already exsist.");

                        return;
                    }
                }



                Session["Qu__Item"] = _Item;
                Session["qty"] = _qty;
                if (txtReqSupplerS.Text != "")
                {
                    _Result = CHNLSVC.Inventory.GetItemsQuotation(Session["UserCompanyCode"].ToString(), "S", "N", txtReqSupplerS.Text, _Item, "A");
                    lblvalue.Text = "Q_S";
                    ViewState["QuSupplier"] = _Result;
                }
                else
                {
                    _Result = CHNLSVC.Inventory.GetItemsQuotation(Session["UserCompanyCode"].ToString(), "S", "N", null, _Item, "A");
                    lblvalue.Text = "Q_";
                    ViewState["QuSupplier"] = _Result;
                }
                grdItemQ.DataSource = _Result;
                grdItemQ.DataBind();

                Label _Reqno = (row.Cells[0].FindControl("col_ReqINo") as Label);
                Label _ItemLine = (row.Cells[0].FindControl("col_ItemLine") as Label);
                Label _reqItem = (row.Cells[0].FindControl("col_reqItem") as Label);
                Label _ItemStatus = (row.Cells[0].FindControl("col_ItemStatus") as Label);
                Label _Seq = (row.Cells[0].FindControl("col_Seq") as Label);
                Label _BQty = (row.Cells[0].FindControl("col_BQty") as Label);
                txtItem.Text = _reqItem.Text;
                if (!string.IsNullOrEmpty(txtItem.Text))
                {
                    MasterItem _mstItm = CHNLSVC.General.GetItemMaster(txtItem.Text.ToUpper());
                    if (_mstItm != null)
                    {
                        txtItmDes.Text = _mstItm.Mi_shortdesc;
                    }
                }
                ddlStatus.SelectedValue = _ItemStatus.Text;
                QDetailDiv.Enabled = false;
                pnlReq.Enabled = false;
                Session["_ItemLine"] = _ItemLine.Text;
                Session["_Seq"] = _Seq.Text;
                Session["_BQty"] = _BQty.Text;
                Session["PRN_No"] = _Reqno.Text;

                divReq.Enabled = false;
            }

            QuotationPopup.Show();
        }
        #endregion

        #region textchange dropdownlist
        protected void txtSupCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ClearErrorMsg();
                if (!string.IsNullOrEmpty(txtSupCode.Text))
                {
                    if (!CHNLSVC.Inventory.IsValidSupplier(Session["UserCompanyCode"].ToString(), txtSupCode.Text, 1, "S"))
                    {
                        ErrorMsgPOrder("Invalid supplier code.");
                        txtSupCode.Text = "";
                        txtSupCode.Focus();
                        return;
                    }
                    else
                    {
                        _supDet = new MasterBusinessEntity();
                        _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtSupCode.Text.Trim(), null, null, "S");
                        txtSupRef.Focus();
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorMsgPOrder("Error Occurred while processing...\n");

            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void txtSupRef_TextChanged(object sender, EventArgs e)
        {
            ClearErrorMsg();
            if (txtJob.Enabled == true)
            {
                txtJob.Focus();
            }
            else
            {
                txtRemarks.Focus();
            }
        }

        protected void txtCreditPeriod_TextChanged(object sender, EventArgs e)
        {
            ClearErrorMsg();
            if (!IsNumeric(txtCreditPeriod.Text))
            {
                ErrorMsgPOrder("Credit period should be numeric.");
                // MessageBox.Show("Credit period should be numeric.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCreditPeriod.Text = "0";
                txtCreditPeriod.Focus();
            }
        }

        protected void txtCurrency_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ClearErrorMsg();
                if (!string.IsNullOrEmpty(txtCurrency.Text))
                {
                    List<MasterCurrency> _currency = new List<MasterCurrency>();
                    _currency = CHNLSVC.General.GetAllCurrency(txtCurrency.Text.Trim());

                    if (_currency == null)
                    {
                        ErrorMsgPOrder("Invalid currency code.");

                        txtCurrency.Text = "";
                        lblEx.Text = "";
                        return;
                    }
                    else
                    {
                        LoadExRate();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsgPOrder("Error Occurred while processing...\n");
                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void txtPurNo_TextChanged(object sender, EventArgs e)
        {
            ClearErrorMsg();
            if (!string.IsNullOrEmpty(txtPurNo.Text))
            {
                LoadSaveDocument();
            }
        }

        protected void txtJob_TextChanged(object sender, EventArgs e)
        {
            ClearErrorMsg();
            if (string.IsNullOrEmpty(txtJob.Text))
            {
                return;
            }
            Boolean _validJob = false;
            List<Service_job_Det> _JobDet = new List<Service_job_Det>();
            _JobDet = CHNLSVC.CustService.GetPcJobDetails(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtJob.Text.Trim());

            if (_JobDet != null && _JobDet.Count > 0)
            {
                foreach (Service_job_Det _jDet in _JobDet)
                {
                    if (_jDet.Jbd_stage < 6)
                    {
                        _validJob = true;
                        goto L1;
                    }

                }
            }
        L1: int I = 0;
            if (_validJob == false)
            {
                ErrorMsgPOrder("Invalid job number.");

                txtJob.Text = "";
                txtJob.Focus();
                return;
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearErrorMsg();
            lblvalue.Text = "c";

            //ddlType.ClearSelection(); 
            if (ddlType.SelectedItem.Text == "NORMAL") //NORMAL
            {
                txtJob.Enabled = false;
                lbtntJob.Visible = false;
                chkBaseToConsGrn.Checked = false;
                chkBaseToConsGrn.Enabled = false;
                // ddlStatus.SelectedItem.Text = "";
                ddlStatus.SelectedValue = "GDLP";
                ddlStatus.Enabled = true;


                return;
            }
            else if (ddlType.SelectedItem.Text == "SERVICE")//SERVICE
            {
                txtJob.Enabled = true;
                lbtntJob.Visible = true;
                chkBaseToConsGrn.Checked = false;
                chkBaseToConsGrn.Enabled = false;
                ddlStatus.SelectedValue = "GDLP";
                //ddlStatus.SelectedValue = "0";
                ddlStatus.Enabled = true;
                grdQuo.DataSource = new int[] { };
                grdQuo.DataBind();
                return;
            }
            else if (ddlType.SelectedItem.Text == "CONSIGNMENT")//CONSIGNMENT
            {
                txtJob.Enabled = false;
                lbtntJob.Visible = false;
                chkBaseToConsGrn.Checked = false;
                chkBaseToConsGrn.Enabled = true;
                ddlStatus.SelectedValue = "CONS";
                //ddlStatus.SelectedValue = "CONS";
                ddlStatus.Enabled = false;

                return;
            }
        }

        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtItmDes.Text = "";
                ClearErrorMsg();
                if (!string.IsNullOrEmpty(txtItem.Text))
                {
                    Boolean _isValid = false;
                    _isValid = CHNLSVC.Inventory.IsValidCompanyItem(Session["UserCompanyCode"].ToString(), txtItem.Text, 1);

                    if (_isValid == false)
                    {
                        ErrorMsgPOrder("Invalid item selected.");
                        txtItem.Text = "";
                        txtItmDes.Text = "";
                        txtItem.Focus();
                        return;
                    }
                    MasterItem _mstItm = CHNLSVC.General.GetItemMaster(txtItem.Text.ToUpper());
                    if (_mstItm != null)
                    {
                        txtItmDes.Text = _mstItm.Mi_shortdesc;
                    }
                    txtQty.Focus();

                    //Warra remk

                    DataTable _dtwarr = CHNLSVC.Inventory.GetWarrRangeRmk(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper(), txtSupCode.Text.ToString());
                    if (_dtwarr != null && _dtwarr.Rows.Count>0)
                    {
                        txtwarrper.Text = _dtwarr.Rows[0][0].ToString();
                        txtwarrremk.Text = _dtwarr.Rows[0][1].ToString();

                    }
                    else
                    {
                        txtwarrper.Text = "";
                        txtwarrremk.Text = "";
                    }

                    ////kapila 14/8/2015
                    //MasterItem _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text);
                    //if (_itemdetail != null)
                    //{
                    //    if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    //    {
                    //        if (_itemdetail.Mi_itm_tp == "V")
                    //        {
                    //            ErrorMsgPOrder("Virtual item not allowed.");
                    //            txtItem.Text = "" ;
                    //        }
                    //    }
                    //}
                }
                ItemTax_Validate();
            }
            catch (Exception ex)
            {
                ErrorMsgPOrder(ex.Message);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void ItemTax_Validate()
        {
            MasterCompany _mstCom = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
            if (_mstCom.MC_TAX_CALC_MTD == "0")
            {
                DataTable _tbl = CHNLSVC.Inventory.GetItemTaxData(Session["UserCompanyCode"].ToString(), txtItem.Text);
                if (_tbl != null)
                {
                    if (_tbl.Rows.Count <= 0)
                    {
                        ErrorMsgPOrder("Selected Item have not tax setup...!!!");
                        return;
                    }
                }
            }
        }

        protected void ddlStatus_TextChanged(object sender, EventArgs e)
        {
            ClearErrorMsg();
            try
            {
                if (!string.IsNullOrEmpty(ddlStatus.SelectedItem.Text))
                {
                    if (!CHNLSVC.Inventory.IsValidItemStatus(ddlStatus.SelectedItem.Text))
                    {
                        ErrorMsgPOrder("Invalid item status.");

                        ddlStatus.SelectedItem.Text = "";
                        ddlStatus.Focus();
                        return;
                    }
                    else
                    {
                        DataTable _lpstatus = CHNLSVC.General.GetItemLPStatus();
                        var _lp = _lpstatus.AsEnumerable().Where(x => x.Field<string>("mis_cd") == ddlStatus.SelectedItem.Text.ToString()).Select(x => x.Field<string>("mis_cd")).ToList();
                        if (_lp.Count <= 0)
                        {
                            ErrorMsgPOrder("Invalid item status.");

                            ddlStatus.SelectedItem.Text = "";
                            ddlStatus.Focus();
                            return;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsgPOrder("Error Occurred while processing...\n");
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            ClearErrorMsg();
            try
            {
                txtItem.Text = txtItem.Text.ToUpper().Trim();
                MasterItem _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text);
                if (_itemdetail != null)
                {
                    if (_itemdetail.Mi_is_ser1 == 0)
                    {
                        if (int.Parse(txtQty.Text) < 0)
                        {
                            ErrorMsgPOrder("only allow Numeric Value!..");
                            txtQty.Text = "0";
                            return;
                        }
                    }
                    else if (_itemdetail.Mi_is_ser1 == -1)
                    {
                        if (decimal.Parse(txtQty.Text) < 0)
                        {
                            ErrorMsgPOrder("only allow Numeric Value!..");
                            txtQty.Text = "0";
                            return;
                        }

                    }
                }

                if (!string.IsNullOrEmpty(txtQty.Text))
                {
                    if (!IsNumeric(txtQty.Text))
                    {
                        ErrorMsgPOrder("Quantity should be numiric.");
                        txtQty.Text = "";
                        txtQty.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtSupCode.Text))
                    {
                        ErrorMsgPOrder("Please select supplier.");
                        txtQty.Text = "";
                        txtSupCode.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtItem.Text))
                    {
                        ErrorMsgPOrder("Please select item.");
                        txtQty.Text = "";
                        txtItem.Focus();
                        return;
                    }
                    if (Session["QuItem"].ToString() == "Q")
                    {
                        decimal Qty = Convert.ToDecimal(txtQty.Text);
                        decimal UPrice = Convert.ToDecimal(txtUnitPrice.Text);
                        decimal _amt = Qty * UPrice;
                        decimal _tax = 0;
                        txtDiscount.Text = "0";
                        txtDisRate.Text = "0";
                        _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtSupCode.Text.Trim(), null, null, "S");
                        if (_supDet.Mbe_is_tax == true)
                        {
                            _tax = TaxCalculation(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), ddlStatus.SelectedValue.Trim(), _amt - 0, 0);
                        }
                        txtTax.Text = _tax.ToString("n");
                        decimal _total = (_amt - 0) + _tax;
                        txtAmount.Text = _amt.ToString();
                        txtTotal.Text = _total.ToString("n");

                        txtUnitPrice.Enabled = false;
                        txtAmount.Enabled = false;
                        txtDisRate.Enabled = false;
                        txtDiscount.Enabled = false;
                        txtTax.Enabled = false;
                        txtTotal.Enabled = false;
                    }
                    //else
                    //{
                    //    decimal Qty = Convert.ToDecimal(txtQty.Text);
                    //    if (txtUnitPrice.Text == "")
                    //    {
                    //        txtUnitPrice.Focus();
                    //        return;
                    //    }


                    //    decimal UPrice = Convert.ToDecimal(txtUnitPrice.Text);
                    //    decimal _amt = Qty * UPrice;
                    //    txtDiscount.Text = "0";
                    //    txtDisRate.Text = "0";
                    //    decimal _tax = TaxCalculation(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), ddlStatus.SelectedValue.Trim(), _amt - 0, 0);
                    //    txtTax.Text = _tax.ToString("n");
                    //    decimal _total = (_amt - 0) + _tax;
                    //    txtAmount.Text = _amt.ToString();
                    //    txtTotal.Text = _total.ToString("n");
                    //    txtUnitPrice.Focus();

                    //}
                    GetSupplierQuotation();
                    txtUnitPrice.Focus();
                    ViewState["txtQty"] = "t";
                }
            }
            catch (Exception ex)
            {
                ErrorMsgPOrder("Error Occurred while processing...\n");
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            ClearErrorMsg();
            try
            {
                if (decimal.Parse(txtUnitPrice.Text) < 0)
                {
                    ErrorMsgPOrder("only allow Numeric Value!..");
                    txtUnitPrice.Text = "0";
                    return;
                }

                if (!string.IsNullOrEmpty(txtUnitPrice.Text))
                {
                    if (!IsNumeric(txtUnitPrice.Text))
                    {
                        ErrorMsgPOrder("Invalid unit amount.");
                        txtUnitPrice.Text = "";
                        txtUnitPrice.Focus();
                    }
                    else if (string.IsNullOrEmpty(txtQty.Text))
                    {
                        ErrorMsgPOrder("Please enter quantity.");

                        txtUnitPrice.Text = "";
                        txtQty.Focus();
                    }
                    else
                    {
                        decimal _unitAmt = 0;
                        decimal _qty = 0;
                        decimal _amount = 0;
                        decimal _disAmt = 0;
                        decimal _tax = 0;
                        _qty = Convert.ToDecimal(txtQty.Text);
                        _unitAmt = Convert.ToDecimal(txtUnitPrice.Text);
                        txtUnitPrice.Text = _unitAmt.ToString("n");
                        _amount = _qty * _unitAmt;
                        txtAmount.Text = _amount.ToString("n");
                        txtDisRate.Text = "0";
                        txtDiscount.Text = "0";
                        _disAmt = Convert.ToDecimal(txtDiscount.Text);
                        _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtSupCode.Text.Trim(), null, null, "S");
                        if (_supDet.Mbe_is_tax == true)
                        {
                            _tax = TaxCalculation(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), ddlStatus.SelectedValue, _amount - _disAmt, 0);
                        }
                        txtTax.Text = _tax.ToString("n");
                        decimal _total = (_amount - _disAmt) + _tax;
                        txtTotal.Text = _total.ToString("n");

                        txtDisRate.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsgPOrder("Error Occurred while processing...\n");
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            ClearErrorMsg();
            try
            {
                if (int.Parse(txtAmount.Text) < 0)
                {
                    ErrorMsgPOrder("only allow Numeric Value!..");
                    txtAmount.Text = "0";
                    return;
                }
                if (!string.IsNullOrEmpty(txtDisRate.Text))
                {
                    if (!IsNumeric(txtDisRate.Text))
                    {
                        ErrorMsgPOrder("Invalid discount rate.");

                        txtDisRate.Text = "0";
                        txtDisRate.Focus();
                        return;
                    }
                    else if (Convert.ToDecimal(txtDisRate.Text) > 100)
                    {
                        ErrorMsgPOrder("Invalid discount rate.");
                        txtDisRate.Text = "0";
                        txtDisRate.Focus();
                        return;
                    }
                    else if (string.IsNullOrEmpty(txtAmount.Text))
                    {
                        ErrorMsgPOrder("Amount is not calculate. Please enter unit price and press enter key.");
                        txtDisRate.Text = "0";
                        txtUnitPrice.Focus();
                        return;
                    }
                    else
                    {
                        decimal _amount = 0;
                        decimal _disRate = 0;
                        decimal _disAmt = 0;
                        _amount = Convert.ToDecimal(txtAmount.Text);
                        _disRate = Convert.ToDecimal(txtDisRate.Text);
                        _disAmt = _amount * _disRate / 100;
                        txtDiscount.Text = _disAmt.ToString("n");
                        txtDisRate.Text = _disRate.ToString("n");

                    }
                }
                else
                {
                    decimal _disRate = 0;
                    decimal _disAmt = 0;
                    txtDisRate.Text = _disRate.ToString("n");
                    txtDiscount.Text = _disAmt.ToString("n");
                }
            }
            catch (Exception ex)
            {
                ErrorMsgPOrder("Error Occurred while processing...\n");
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void txtDisRate_TextChanged(object sender, EventArgs e)
        {
            ClearErrorMsg();
            try
            {
                if (Convert.ToDecimal(txtDisRate.Text) < 0)
                {
                    ErrorMsgPOrder("only allow Numeric Value!..");
                    txtDisRate.Text = "0";
                    return;
                }
                if (!string.IsNullOrEmpty(txtDisRate.Text))
                {
                    if (!IsNumeric(txtDisRate.Text))
                    {
                        ErrorMsgPOrder("IInvalid discount rate.");

                        txtDisRate.Text = "0";
                        txtDisRate.Focus();
                        return;
                    }
                    else if (Convert.ToDecimal(txtDisRate.Text) > 100)
                    {
                        ErrorMsgPOrder("Invalid discount rate.");

                        txtDisRate.Text = "0";
                        txtDisRate.Focus();
                        return;
                    }
                    else if (string.IsNullOrEmpty(txtAmount.Text))
                    {
                        ErrorMsgPOrder("Amount is not calculate. Please enter unit price and press enter key.");

                        txtDisRate.Text = "0";
                        txtUnitPrice.Focus();
                        return;
                    }
                    else
                    {
                        decimal _amount = 0;
                        decimal _disRate = 0;
                        decimal _disAmt = 0;
                        _amount = Convert.ToDecimal(txtAmount.Text);
                        _disRate = Convert.ToDecimal(txtDisRate.Text);
                        _disAmt = _amount * _disRate / 100;
                        txtDiscount.Text = _disAmt.ToString("n");
                        txtDisRate.Text = _disRate.ToString("n");
                        //
                        decimal _disrate = 0;
                        decimal _amt = Convert.ToDecimal(txtAmount.Text);
                        decimal _disamt = Convert.ToDecimal(txtDiscount.Text);
                        if (_amt > 0)
                        {
                            _disrate = _disamt / _amt * 100;
                        }
                        else
                        {
                            _disrate = 0;
                        }
                        txtDisRate.Text = _disrate.ToString("n");
                        txtDiscount.Text = _disamt.ToString("n");
                        decimal _tax = 0;
                        _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtSupCode.Text.Trim(), null, null, "S");
                        if (_supDet.Mbe_is_tax == true)
                        {
                            _tax = TaxCalculation(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), ddlStatus.SelectedValue, _amt - _disamt, 0);
                        }
                        txtTax.Text = _tax.ToString("n");
                        decimal _total = (_amt - _disamt) + _tax;
                        txtTotal.Text = _total.ToString("n");
                        txtDiscount.Focus();
                    }
                }
                else
                {
                    decimal _disRate = 0;
                    decimal _disAmt = 0;
                    txtDisRate.Text = _disRate.ToString("n");
                    txtDiscount.Text = _disAmt.ToString("n");
                }
            }
            catch (Exception ex)
            {
                ErrorMsgPOrder("Error Occurred while processing...\n");
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            ClearErrorMsg();
            try
            {
                decimal _amt = 0;
                decimal _disrate = 0;
                decimal _disamt = 0;
                decimal _tax = 0;
                decimal _total = 0;
                if (decimal.Parse(txtDiscount.Text) < 0)
                {
                    ErrorMsgPOrder("only allow Numeric Value!..");
                    txtDiscount.Text = "0";
                    return;
                }
                if (!string.IsNullOrEmpty(txtDiscount.Text))
                {
                    if (!IsNumeric(txtDiscount.Text))
                    {
                        ErrorMsgPOrder("Invalid discount amount..");
                        txtDiscount.Text = "0";
                        txtDisRate.Text = "0";
                        txtDiscount.Focus();
                        return;
                    }
                    else if (Convert.ToDecimal(txtDiscount.Text) > Convert.ToDecimal(txtAmount.Text))
                    {
                        ErrorMsgPOrder("Invalid discount amount.");
                        txtDiscount.Text = "0";
                        txtDisRate.Text = "0";
                        txtDiscount.Focus();
                        return;
                    }
                    else if (string.IsNullOrEmpty(txtAmount.Text))
                    {
                        ErrorMsgPOrder("Amount is not calculate. Please enter unit price and press enter key.");

                        txtDisRate.Text = "0";
                        txtDiscount.Text = "0";
                        txtAmount.Focus();
                        return;
                    }
                    else
                    {
                        _amt = Convert.ToDecimal(txtAmount.Text);
                        _disamt = Convert.ToDecimal(txtDiscount.Text);
                        if (_amt > 0)
                        {
                            _disrate = _disamt / _amt * 100;
                        }
                        else
                        {
                            _disrate = 0;
                        }
                        txtDisRate.Text = _disrate.ToString("n");
                        txtDiscount.Text = _disamt.ToString("n");
                        _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtSupCode.Text.Trim(), null, null, "S");
                        if (_supDet.Mbe_is_tax == true)
                        {
                            _tax = TaxCalculation(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), ddlStatus.SelectedValue, _amt - _disamt, 0);
                        }
                        txtTax.Text = _tax.ToString("n");
                        _total = (_amt - _disamt) + _tax;
                        txtTotal.Text = _total.ToString("n");
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorMsgPOrder("Error Occurred while processing...\n");
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void rdbLprice_CheckedChanged(object sender, EventArgs e)
        {
            ClearErrorMsg();
            DataTable _result;
            DataTable tbl = ViewState["QuSupplier"] as DataTable;
            DataView view = new DataView(tbl);
            view.Sort = "qd_unit_price ASC";
            //DESC
            if (view.Count > 0)
            {
                _result = view.ToTable();
                grdItemQ.DataSource = _result;
                grdItemQ.DataBind();
                QuotationPopup.Show();
            }

        }

        protected void rdbLDate_CheckedChanged(object sender, EventArgs e)
        {
            ClearErrorMsg();
            DataTable _result;
            DataTable tbl = ViewState["QuSupplier"] as DataTable;
            DataView view = new DataView(tbl);
            view.Sort = "qh_frm_dt DESC";
            //DESC
            if (view.Count > 0)
            {
                _result = view.ToTable();
                grdItemQ.DataSource = _result;
                grdItemQ.DataBind();
                QuotationPopup.Show();
            }
        }

        protected void ddlMainType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearTextBox();
            ClearErrorMsg();
            if (ddlMainType.SelectedItem.Text == "Imports")
            {
               // QDetailDiv.Enabled = false;
               // pnlItem.Enabled = false;
               // pnlReq.Enabled = false;
                ClearTextBox();//
               // ddlType.Enabled = false;
                ddlType.Enabled = true;
                QDetailDiv.Enabled = true;
                pnlItem.Enabled = true;
                pnlReq.Enabled = true;
                ddlType.SelectedIndex = 0;
                //ddlStatus.SelectedIndex = 0;
                while (ddlStatus.Items.Count > 1)
                {
                    ddlStatus.Items.RemoveAt(1);
                }
                DataTable _result = CHNLSVC.Inventory.GetOrderStatus("I");
                ddlStatus.DataSource = _result;
                ddlStatus.DataTextField = "mis_desc";
                ddlStatus.DataValueField = "mis_cd";
                ddlStatus.DataBind();
                //ddlStatus.SelectedValue = "GOD";
                ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByValue("GOD"));
                // ddlStatus.SelectedIndex = 15;
                lblvalue.Text = "c";

                // ddlStatus.SelectedIndex = 15;
                ddlType.SelectedIndex = 1;
                return;
            }
            while (ddlStatus.Items.Count > 1)
            {
                ddlStatus.Items.RemoveAt(1);
            }
            DataTable _result1 = CHNLSVC.Inventory.GetOrderStatus("A");
            ddlStatus.DataSource = _result1;
            ddlStatus.DataTextField = "mis_desc";
            ddlStatus.DataValueField = "mis_cd";
            ddlStatus.DataBind();
            //ddlStatus.SelectedIndex = 5;
            ddlType.Enabled = true;
            QDetailDiv.Enabled = true;
            pnlItem.Enabled = true;
            pnlReq.Enabled = true;
            lblvalue.Text = "c";
            //ddlStatus.SelectedIndex = 4;
            ddlStatus.SelectedValue = "GDLP";
            ddlType.SelectedIndex = 1;
        }
        #endregion

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            UserPopoup.Hide();
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            QuotationPopup.Hide();
        }

        protected void ddlPayTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPayTerm.SelectedValue == "1")
            {
                txtCreditPeriod.Text = "0";
            }
        }

        #region Modal Popup 2

        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "293")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.POByDate);
                DataTable _result = CHNLSVC.Inventory.GetPurchaseOrders(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "293";
                UserDPopoup.Show();
                ViewState["SEARCH"] = _result;
            }
            else
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
                DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "275";
                UserDPopoup.Show();
            }


        }
        protected void grdResultD_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
            string Des = grdResultD.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "275")
            {
                lblvalue.Text = "275";
                txtJob.Text = Des;
                UserDPopoup.Hide();
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "293")
            {
                lblvalue.Text = "c";
                txtPurNo.Text = Des;
                UserDPopoup.Hide();
                if (!string.IsNullOrEmpty(txtPurNo.Text))
                {
                    if (ddlMainType.SelectedValue == "0")
                    {
                        ErrorMsgPOrder("Please Select Type");
                        return;
                    }

                    LoadSaveDocument();
                }
                return;
            }
        }
        protected void grdResultD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            if (lblvalue.Text == "293")
            {
                grdResultD.PageIndex = e.NewPageIndex;
                grdResultD.DataSource = null;
                grdResultD.DataSource = (DataTable)ViewState["SEARCH"];
                grdResultD.DataBind();
                grdResultD.PageIndex = 0;
                UserDPopoup.Show();
            }
            else
            {
                grdResultD.PageIndex = e.NewPageIndex;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
                DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                UserDPopoup.Show();

            }

        }
        protected void lbtnSearchD_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "275")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
                DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                UserDPopoup.Show();

            }
            if (lblvalue.Text == "293")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.POByDate);
                DataTable _result = CHNLSVC.Inventory.GetPurchaseOrders(SearchParams, ddlSearchbykeyD.SelectedItem.ToString(), "%" + txtSearchbywordD.Text.ToString(), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "293";
                UserDPopoup.Show();
                ViewState["SEARCH"] = _result;
            }

        }
        protected void txtSearchbywordD_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "275")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
                DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                Session["Job"] = "Job";
                UserDPopoup.Show();
            }
            if (lblvalue.Text == "293")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.POByDate);
                DataTable _result = CHNLSVC.Inventory.GetPurchaseOrders(SearchParams, ddlSearchbykeyD.SelectedItem.ToString(), "%" + txtSearchbywordD.Text.ToString(), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = _result;
                grdResultD.DataBind();
                lblvalue.Text = "293";
                UserDPopoup.Show();
                ViewState["SEARCH"] = _result;
            }
        }
        #endregion

        protected void grdQuo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string supplier = (grdQuo.SelectedRow.FindControl("col_qh_party_cd") as Label).Text;
            string price = (grdQuo.SelectedRow.FindControl("col_qPrice") as Label).Text;
            if (supplier != txtSupCode.Text)
            {
                DispMsg("You cannot select a different supplier", "W");
                return;
            }

            txtUnitPrice.Text = price;
            decimal _unitAmt = 0;
            decimal _qty = 0;
            decimal _amount = 0;
            _qty = Convert.ToDecimal(txtQty.Text);
            _unitAmt = Convert.ToDecimal(txtUnitPrice.Text);
            txtUnitPrice.Text = _unitAmt.ToString("n");
            _amount = _qty * _unitAmt;
            txtAmount.Text = _amount.ToString("n");
            txtDisRate.Focus();
        }

        protected void grdQuo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[0].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdQuo, "Select$" + e.Row.RowIndex);
                    e.Row.Cells[0].Attributes["style"] = "cursor:pointer";


                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void dgvSupDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        private void DispMsg(string msgText, string msgType)
        {
            msgText = msgText.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");

            if (msgType == "N")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + msgText + "');", true);
            }
            else if (msgType == "W")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msgText + "');", true);
            }
            else if (msgType == "S")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + msgText + "');", true);
            }
            else if (msgType == "E")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + msgText + "');", true);
            }
            else if (msgType == "A")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + msgText + "');", true);
            }
        }
        protected void lbtnGenarate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserCompanyCode"] == null || Session["UserID"] == null)
                {
                    Response.Redirect("~/Login.aspx"); return;
                }
                if (ddlMainType.SelectedIndex < 1)
                {
                    DispMsg("Please select a main type", "W"); return;
                }
                if (ddlType.SelectedIndex < 1)
                {
                    DispMsg("Please selct a sub type", "W"); return;
                }
                if (string.IsNullOrEmpty(txtPoDate.Text))
                {
                    DispMsg("Please enter a date", "W"); return;
                }
                DateTime _date = new DateTime();
                if (!DateTime.TryParse(txtPoDate.Text, out _date))
                {
                    DispMsg("Please enter a valid date", "W"); return;
                }
                if (string.IsNullOrEmpty(txtCurrency.Text))
                {
                    DispMsg("Please select po currency !!!", "W");
                    txtCurrency.Focus();
                    return;
                }
                _quotHd = new List<QuotationHeader>();
                var v = _quotHd;
                bool chkSelect = false;
                decimal d = 0, qty = 0;
                foreach (GridViewRow r in grdReqItems.Rows)
                {
                    Label col_reqQty = (Label)r.FindControl("col_reqQty");
                    Label col_ReqINo = (Label)r.FindControl("col_ReqINo");
                    Label col_reqItem = (Label)r.FindControl("col_reqItem");
                    Label col_BQty = (Label)r.FindControl("col_BQty");
                    CheckBox chk_ReqItem = (CheckBox)r.FindControl("chk_ReqItem");
                    if (col_reqItem.Text=="LEXMARKMS310")
                    {
                        int g = 0;
                    }
                    qty = decimal.TryParse(col_BQty.Text, out d) ? d : 0;
                    if (chk_ReqItem.Checked)
                    {
                        chkSelect = true;
                        List<QuotationHeader> _list = new List<QuotationHeader>();
                        List<QuotationHeader> _tmpList =
                        CHNLSVC.Inventory.GetLatestValidQuotationData(Session["UserCompanyCode"].ToString(), "", "S", ddlType.SelectedValue,
                        Convert.ToDateTime(txtPoDate.Text).Date, Convert.ToDecimal(1), "A", col_reqItem.Text);
                       //Please Get min qut item
                        
                        //if (_list != null)
                        //{
                        //    if (_list.Count>1)
                        //    {
                        //        _list = _list.OrderBy(a => a.Qh_ex_rt).ToList();
                        //    }
                        //}

                        #region add by lakshan 08Sep2017
                        if (_tmpList != null)
                        {
                            _tmpList = _tmpList.OrderBy(c => c.Qh_anal_5).ToList();
                            if (_tmpList.Count>0)
                            {
                                //_tmpList[0].Qh_jobno = col_ReqINo.Text;
                                if (_tmpList.Count > 1)
                                {
                                    int x = 0;
                                }
                                _list.Add(_tmpList[0]);  
                            }
                        }
                        #endregion
                        if (_list != null)
                        {
                            foreach (QuotationHeader item in _list)
                            {
                                item.Tmp_req_no = col_ReqINo.Text;
                                item.Qd_frm_qty = qty;
                                //   item.Qh_anal_5 = item.Qh_anal_5 * qty;
                                item.Qh_anal_5 = item.Qh_anal_5; //Req BY ASANKA
                                var i = v.Where(c => c.Qd_itm_cd == item.Qd_itm_cd).SingleOrDefault();
                                if (i == null)
                                {
                                    v.Add(item);
                                }
                                else
                                {
                                    if (i.Qh_anal_5 > item.Qh_anal_5)
                                    {
                                        v.Where(c => c.Qd_itm_cd == item.Qd_itm_cd).SingleOrDefault().Qh_anal_5 = item.Qh_anal_5;
                                    }
                                    var q = v.Where(c => c.Qd_itm_cd == col_reqItem.Text).SingleOrDefault();
                                    if (q != null)
                                    {
                                        var xy = v.Where(c => c.Qd_itm_cd == col_reqItem.Text).ToList();
                                        if (xy.Count == 1)
                                        {
                                            v.Where(c => c.Qd_itm_cd == col_reqItem.Text).SingleOrDefault().Qd_frm_qty += qty;
                                        }
                                    }
                                }
                            }
                        }
                        //var q = v.Where(c => c.Qd_itm_cd == col_reqItem.Text).SingleOrDefault();
                        //if (q != null)
                        //{
                        //    if (v.Where(c => c.Qd_itm_cd == col_reqItem.Text).ToList().Count > 1)
                        //    {
                        //        v.Where(c => c.Qd_itm_cd == col_reqItem.Text).SingleOrDefault().Qd_frm_qty += qty;
                        //    }
                        //}
                    }
                }
                if (!chkSelect)
                {
                    DispMsg("Please select a item", "W"); return;
                }
                _quotHd = v;
                _quotHd = _quotHd.OrderBy(c => c.Qh_party_cd).ToList();
                dgvSupDetails.DataSource = new int[] { };
                if (_quotHd != null)
                {
                    if (_quotHd.Count > 0)
                    {
                        dgvSupDetails.DataSource = _quotHd;
                    }
                }
                dgvSupDetails.DataBind();
                PopupSearch.Show();
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void chkMultiReq_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkMultiReq.Checked)
            {
                foreach (GridViewRow r in grdReq.Rows)
                {
                    CheckBox chk_Req = (CheckBox)r.FindControl("chk_Req");
                    chk_Req.Checked = false;
                }
                _itmData = new DataTable();
                grdReqItems.DataSource = new int[] { };
                grdReqItems.DataBind();
            }
        }

        protected void lbtnSavePo_Click(object sender, EventArgs e)
        {
            ClearErrorMsg();

            if (!(ddlMainType.SelectedIndex > 0))
            {
                ErrorPo("Please select Type.");
                ddlMainType.Focus();
                PopupSearch.Show();
                return;
            }
            bool chkSelect = false;
            foreach (GridViewRow r in dgvSupDetails.Rows)
            {
                CheckBox chkPoselect = (CheckBox)r.FindControl("chkPoselect");
                if (chkPoselect.Checked)
                {
                    chkSelect = true; break;
                }
            }
            if (!chkSelect)
            {
                ErrorPo("Cannot find purchase order items !!! ");
                PopupSearch.Show(); return;
            }
            if (_POstatus == "A")
            {
                ErrorPo("Order is already approved !!! ");
                txtPurNo.Focus();
                PopupSearch.Show();
                return;
            }

            if (_POstatus == "F")
            {
                ErrorPo("Order is already completed !!! ");
                txtPurNo.Focus();
                PopupSearch.Show();
                return;
            }

            if (_POstatus == "C")
            {
                ErrorPo("Order is already cancelled !!! ");
                txtPurNo.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtCurrency.Text))
            {
                ErrorPo("Please select po currency !!! ");
                txtCurrency.Focus();
                PopupSearch.Show();
                return;
            }

            if (string.IsNullOrEmpty(lblEx.Text))
            {
                ErrorPo("Exchange rate is missing !!! ");
                txtCurrency.Focus();
                PopupSearch.Show();
                return;
            }

            if (Convert.ToDecimal(lblEx.Text) <= 0)
            {
                ErrorPo("Invalid exchange rate !!! ");
                txtCurrency.Focus();
                PopupSearch.Show();
                return;
            }

            if (ddlType.SelectedItem.Text == "SERVICE")
            {
                if (string.IsNullOrEmpty(txtJob.Text))
                {
                    ErrorPo("Please select job number. !!! ");
                    txtJob.Focus();
                    PopupSearch.Show();
                    return;
                }
            }
           // SavePOHeaderPopup();
            SavePOHeaderPopupNew();

        }

        protected void lbtnDanger_Click(object sender, EventArgs e)
        {
            divDanger1.Visible = false;
            PopupSearch.Show();
        }

        protected void lbtnSucces_Click(object sender, EventArgs e)
        {
            divSucces1.Visible = false;
            PopupSearch.Show();
        }
        private void SavePOHeaderPopup()
        {
            try
            {
                Int32 row_aff = 0;
                string _PONo = string.Empty;
                string _msg = string.Empty;
                Int32 _isBaseCons = 0;
                string _type = string.Empty;

                if (ddlType.SelectedItem.Text == "CONSIGNMENT")
                {
                    _type = "C";
                }
                else if (ddlType.SelectedItem.Text == "NORMAL")
                {
                    _type = "N";
                }
                else if (ddlType.SelectedItem.Text == "SERVICE")
                {
                    _type = "S";
                }

                if (chkBaseToConsGrn.Checked == true)
                {
                    _isBaseCons = 1;
                }
                else
                {
                    _isBaseCons = 0;
                }
                LoadExRate();
                List<FF.BusinessObjects.QuotationHeader> _quotationHeader = new List<FF.BusinessObjects.QuotationHeader>();
                List<FF.BusinessObjects.QuotationHeader> _quotationHeaderTemp = new List<FF.BusinessObjects.QuotationHeader>();
                List<FF.BusinessObjects.PurchaseOrder> _puOrderList = new List<FF.BusinessObjects.PurchaseOrder>();

                foreach (GridViewRow row in dgvSupDetails.Rows)
                {
                    CheckBox chkPoselect = (CheckBox)row.FindControl("chkPoselect");
                    Label lblSupplier = (Label)row.FindControl("lblSupplier");
                    Label lblItem = (Label)row.FindControl("lblItem");
                    Label lblPrice = (Label)row.FindControl("lblPrice");

                    if (chkPoselect.Checked)
                    {
                        var v = _quotHd.Where(c => c.Qh_party_cd == lblSupplier.Text && c.Qd_itm_cd == lblItem.Text).FirstOrDefault();
                        if (v != null)
                        {
                            _quotationHeader.Add(v);
                            if (_quotationHeaderTemp.Count > 0)
                            {
                                var v2 = _quotationHeaderTemp.Where(c => c.Qh_party_cd == lblSupplier.Text).FirstOrDefault();
                                if (v2 == null)
                                {
                                    _quotationHeaderTemp.Add(v);
                                }
                            }
                            else
                            {
                                _quotationHeaderTemp.Add(v);
                            }
                        }
                        else
                        {
                            ErrorMsgPOrder("Please Setup Supplier :" + lblSupplier.Text + " Item:" + lblItem.Text);
                            return;
                        }
                    }
                }
                string QTNum;
                List<PurchaseNoTemp> _PurchaseNoTemp = new List<PurchaseNoTemp>();
                foreach (QuotationHeader qh in _quotationHeaderTemp)
                {
                    #region qhTemp
                    FF.BusinessObjects.PurchaseOrder _puOrder = new FF.BusinessObjects.PurchaseOrder();
                    _puOrder.Poh_seq_no = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "PO", 1, Session["UserCompanyCode"].ToString());
                    _puOrder.Poh_tp = ddlMainType.SelectedValue;//"L";
                    _puOrder.Poh_sub_tp = _type;
                    _puOrder.Poh_doc_no = txtPurNo.Text.Trim();
                    _puOrder.Poh_com = Session["UserCompanyCode"].ToString();
                    _puOrder.Poh_ope = "INV";
                    _puOrder.Poh_profit_cd = Session["UserDefProf"].ToString();
                    _puOrder.Poh_dt = Convert.ToDateTime(txtPoDate.Text).Date;
                    _puOrder.Poh_ref = txtSupRef.Text;
                    _puOrder.Poh_job_no = txtJob.Text;
                    _puOrder.Poh_pay_term = ddlPayTerm.SelectedItem.Text;
                    _puOrder.Poh_supp = qh.Qh_party_cd;
                    _puOrder.Poh_cur_cd = txtCurrency.Text;
                    _puOrder.Poh_ex_rt = Convert.ToDecimal(lblEx.Text);
                    _puOrder.Poh_trans_term = "";
                    _puOrder.Poh_port_of_orig = "";
                    _puOrder.Poh_cre_period = txtCreditPeriod.Text;
                    _puOrder.Poh_frm_yer = Convert.ToDateTime(txtPoDate.Text).Year;
                    _puOrder.Poh_frm_mon = Convert.ToDateTime(txtPoDate.Text).Month;
                    _puOrder.Poh_to_yer = Convert.ToDateTime(txtPoDate.Text).Year;
                    _puOrder.Poh_to_mon = Convert.ToDateTime(txtPoDate.Text).Month;
                    _puOrder.Poh_preferd_eta = Convert.ToDateTime(txtPoDate.Text).Date;
                    _puOrder.Poh_contain_kit = false;
                    _puOrder.Poh_sent_to_vendor = false;
                    _puOrder.Poh_sent_by = "";
                    _puOrder.Poh_sent_via = "";
                    _puOrder.Poh_sent_add = "";
                    _puOrder.Poh_stus = "P";
                    _puOrder.Poh_remarks = txtRemarks.Text;
                    _puOrder.Poh_sub_tot = 0;
                    _puOrder.Poh_tax_tot = 0;
                    _puOrder.Poh_dis_rt = 0;
                    _puOrder.Poh_dis_amt = 0;
                    _puOrder.Poh_oth_tot = 0;
                    _puOrder.Poh_tot = Convert.ToDecimal(lblTotal.Text);
                    List<PurchaseOrderDetail> _POItemListSave = new List<PurchaseOrderDetail>();
                    _POItemList = new List<PurchaseOrderDetail>();
                    bool _isVatClaim = false;
                    string _suppTaxCate = "";
                    _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), _puOrder.Poh_supp, null, null, "S");
                    foreach (var item in _quotationHeader)
                    {
                        #region QH
                        if (qh.Qh_party_cd == item.Qh_party_cd)
                        {
                            PurchaseOrderDetail _tmpPoDetails = new PurchaseOrderDetail();
                            MasterItem _tmpItem = new MasterItem();
                            _tmpItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), item.Qd_itm_cd);
                            if ((_POItemList.Count > 0))
                            {
                                var max_Query =
                             (from tab1 in _POItemList
                              select tab1.Pod_line_no).Max();

                                _lineNo = max_Query;
                            }
                            else
                            {
                                _lineNo = 0;
                            }
                            if (_supDet != null)
                            {
                                _isVatClaim = _supDet.Mbe_is_tax;
                                _suppTaxCate = _supDet.Mbe_cate;
                            }
                            else
                            {
                                ErrorMsgPOrder("Cannot find supplier details.");
                                return;
                            }
                            decimal _taxForActual = 0;
                            if (_tmpItem != null)
                            {
                                _lineNo = _lineNo + 1;
                                // Add po items ______________________
                                _tmpPoDetails.Pod_act_unit_price = item.Qh_anal_5;
                                if (string.IsNullOrEmpty(_suppTaxCate))
                                {
                                    // _tmpPoDetails.Pod_act_unit_price = (Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text) - Convert.ToDecimal(txtDiscount.Text) + Convert.ToDecimal(txtTax.Text)) / Convert.ToDecimal(txtQty.Text);
                                }
                                else
                                {
                                    decimal _unitVal = item.Qh_anal_5;
                                    decimal _qty = item.Qd_frm_qty;
                                    decimal _amt = _unitVal * _qty;
                                    decimal _disRate = 0;
                                    decimal _disAmount = 0;

                                    _taxForActual = TaxCalculationActualCost(Session["UserCompanyCode"].ToString(), item.Qd_itm_cd,
                                        ddlStatus.SelectedValue.Trim(), _amt - _disAmount, _suppTaxCate, 0);
                                    _tmpPoDetails.Pod_act_unit_price = ((item.Qd_frm_qty * item.Qh_anal_5) + _taxForActual) / item.Qd_frm_qty;
                                }
                                _tmpPoDetails.Pod_dis_amt = 0;
                                _tmpPoDetails.Pod_dis_rt = 0;
                                _tmpPoDetails.Pod_grn_bal = item.Qd_frm_qty;
                                _tmpPoDetails.Pod_item_desc = _tmpItem.Mi_shortdesc;
                                _tmpPoDetails.Pod_itm_cd = item.Qd_itm_cd;
                                if (ddlStatus.SelectedItem.Text == "CONS")
                                {
                                    _tmpPoDetails.Pod_itm_stus = "CONS";
                                }
                                else
                                {
                                    _tmpPoDetails.Pod_itm_stus = ddlStatus.SelectedValue.Trim();
                                }
                                _tmpPoDetails.Pod_itm_tp = _tmpItem.Mi_itm_tp;
                                _tmpPoDetails.Pod_kit_itm_cd = "";
                                _tmpPoDetails.Pod_kit_line_no = 0;
                                _tmpPoDetails.Pod_lc_bal = 0;
                                _tmpPoDetails.Pod_line_amt = item.Qh_anal_5 * item.Qd_frm_qty;
                                _tmpPoDetails.Pod_line_no = _lineNo;
                                decimal _tax = 0;
                                _tax = TaxCalculation(Session["UserCompanyCode"].ToString(), item.Qd_itm_cd, _tmpPoDetails.Pod_itm_stus, _tmpPoDetails.Pod_line_amt, 0);
                                _tmpPoDetails.Pod_line_tax = _tax;
                                _tmpPoDetails.Pod_line_val = item.Qh_anal_5 * item.Qd_frm_qty;
                                _tmpPoDetails.Pod_nbt = 0;
                                _tmpPoDetails.Pod_nbt_before = 0;
                                _tmpPoDetails.Pod_pi_bal = 0;
                                _tmpPoDetails.Pod_qty = item.Qd_frm_qty;
                                _tmpPoDetails.Pod_ref_no = txtSupRef.Text;
                                _tmpPoDetails.Pod_seq_no = 12;
                                _tmpPoDetails.Pod_si_bal = 0;
                                _tmpPoDetails.Pod_tot_tax_before = 0;
                                _tmpPoDetails.Pod_unit_price = item.Qh_anal_5;
                                _tmpPoDetails.Pod_uom = _tmpItem.Mi_itm_uom;
                                _tmpPoDetails.Pod_vat = 0;
                                _tmpPoDetails.Pod_vat_before = 0;
                                _POItemList.Add(_tmpPoDetails);
                            }
                        }
                        #endregion
                    }

                    foreach (PurchaseOrderDetail line in _POItemList)
                    {
                        line.Pod_seq_no = _puOrder.Poh_seq_no;
                        _POItemListSave.Add(line);
                    }
                    List<PurchaseOrderDelivery> _poDeliveryList = new List<PurchaseOrderDelivery>();
                    #region Delivery
                    foreach (PurchaseOrderDetail item in _POItemListSave)
                    {
                        PurchaseOrderDelivery _poDel = new PurchaseOrderDelivery();
                        _poDel.Podi_seq_no = item.Pod_seq_no;
                        _poDel.Podi_line_no = item.Pod_line_no;
                        _poDel.Podi_del_line_no = 1;
                        _poDel.Podi_loca = Session["UserDefLoca"].ToString();
                        _poDel.Podi_itm_cd = item.Pod_itm_cd;
                        _poDel.Podi_itm_stus = item.Pod_itm_stus;
                        _poDel.Podi_qty = item.Pod_qty;
                        _poDel.Podi_bal_qty = item.Pod_qty;
                        _poDeliveryList.Add(_poDel);
                    }
                    #endregion
                    if (_POItemList.Count > 0)
                    {
                        _puOrder.Poh_sub_tot = _POItemList.Select(x => x.Pod_line_amt).Sum();
                        _puOrder.Poh_tax_tot = _POItemList.Select(x => x.Pod_line_tax).Sum();
                        _puOrder.Poh_dis_rt = 0;
                        _puOrder.Poh_dis_amt = 0;
                        _puOrder.Poh_oth_tot = 0;
                        _puOrder.Poh_tot = _POItemList.Select(x => x.Pod_line_val).Sum();
                    }
                    #endregion

                    #region QH 2
                    _puOrder.Poh_reprint = false;
                    _puOrder.Poh_tax_chg = false;
                    _puOrder.poh_is_conspo = _isBaseCons;
                    _puOrder.Poh_cre_by = Session["UserID"].ToString();

                    MasterAutoNumber masterAuto = new MasterAutoNumber();
                    masterAuto.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                    masterAuto.Aut_cate_tp = "COM";
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "PUR";
                    masterAuto.Aut_number = 5;//what is Aut_number
                    masterAuto.Aut_start_char = "PUR";
                    masterAuto.Aut_year = null;

                    List<PurchaseOrderDelivery> _PODelSave = new List<PurchaseOrderDelivery>();
                    if (ViewState["PurchaseReqList"] != null)
                    {
                        _PurchaseReqList = ViewState["PurchaseReqList"] as List<PurchaseReq>;
                    }
                    if (ViewState["InventoryRequestItem"] != null)
                    {
                        _InventoryRequestItem = ViewState["InventoryRequestItem"] as List<InventoryRequestItem>;
                    }
                    // return;
                    //row_aff += (Int32)CHNLSVC.Inventory.SaveNewPO(_puOrder, _POItemList, _PODelSave, null, masterAuto,
                    //   _PurchaseReqList, _InventoryRequestItem, out QTNum);
                    row_aff += (Int32)CHNLSVC.Inventory.SaveNewPO(_puOrder, _POItemList, _poDeliveryList, null, masterAuto,
                        _PurchaseReqList, _InventoryRequestItem, out QTNum);
                    if (row_aff > 0)
                    {
                        _PurchaseNoTemp.Add(new PurchaseNoTemp() { PoNo = QTNum });
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_msg))
                        {
                            ErrorMsgPOrder(_msg);
                        }
                        else
                        {
                            ErrorMsgPOrder("Faild to generate");
                        }
                    }
                    #endregion
                }
                if (row_aff > 0)
                {
                    string msg = "";
                    int i = 0;
                    foreach (var v in _PurchaseNoTemp)
                    {
                        msg += i == 0 ? v.PoNo : ", " + v.PoNo;
                        i++;
                    }
                    if (_PurchaseNoTemp.Count == 1)
                    {
                        //DispMsg("Purchase orders generated " + msg, "S");
                        SuccessMsgPOrder("Purchase order generated " + msg);
                    }
                    else
                    {
                        dgvPoSaved.DataSource = new int[] { };
                        dgvPoSaved.DataSource = _PurchaseNoTemp;
                        dgvPoSaved.DataBind();
                        popupSaveData.Show();

                    }
                    ClearTextBox();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        ErrorMsgPOrder(_msg);
                    }
                    else
                    {
                        ErrorMsgPOrder("Faild to generate");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsgPOrder("Error Occurred while processing...\n");
            }
        }
        private void SavePOHeaderPopupNew()
        {
            try
            {
                Int32 row_aff = 0;
                string _PONo = string.Empty;
                string _msg = string.Empty;
                Int32 _isBaseCons = 0;
                string _type = string.Empty;

                if (ddlType.SelectedItem.Text == "CONSIGNMENT")
                {
                    _type = "C";
                }
                else if (ddlType.SelectedItem.Text == "NORMAL")
                {
                    _type = "N";
                }
                else if (ddlType.SelectedItem.Text == "SERVICE")
                {
                    _type = "S";
                }

                if (chkBaseToConsGrn.Checked == true)
                {
                    _isBaseCons = 1;
                }
                else
                {
                    _isBaseCons = 0;
                }
                LoadExRate();
                List<FF.BusinessObjects.QuotationHeader> _quotationHeader = new List<FF.BusinessObjects.QuotationHeader>();
                List<FF.BusinessObjects.QuotationHeader> _quotationHeaderTemp = new List<FF.BusinessObjects.QuotationHeader>();
                List<FF.BusinessObjects.PurchaseOrder> _puOrderList = new List<FF.BusinessObjects.PurchaseOrder>();

                foreach (GridViewRow row in dgvSupDetails.Rows)
                {
                    CheckBox chkPoselect = (CheckBox)row.FindControl("chkPoselect");
                    Label lblSupplier = (Label)row.FindControl("lblSupplier");
                    Label lblItem = (Label)row.FindControl("lblItem");
                    Label lblPrice = (Label)row.FindControl("lblPrice");
                    Label lblTmp_req_no = (Label)row.FindControl("lblTmp_req_no");

                    if (chkPoselect.Checked)
                    {
                        var v = _quotHd.Where(c => c.Qh_party_cd == lblSupplier.Text && c.Qd_itm_cd == lblItem.Text).FirstOrDefault();
                        if (v != null)
                        {
                            v.Tmp_req_no =lblTmp_req_no.Text;
                            _quotationHeader.Add(v);
                            if (_quotationHeaderTemp.Count > 0)
                            {
                                var v2 = _quotationHeaderTemp.Where(c => c.Qh_party_cd == lblSupplier.Text).FirstOrDefault();
                                if (v2 == null)
                                {v2.Tmp_req_no =lblTmp_req_no.Text; 
                                    _quotationHeaderTemp.Add(v2);
                                }
                            }
                            else
                            {
                                v.Tmp_req_no =lblTmp_req_no.Text;
                                _quotationHeaderTemp.Add(v);
                            }
                        }
                        else
                        {
                            ErrorMsgPOrder("Please Setup Supplier :" + lblSupplier.Text + " Item:" + lblItem.Text);
                            return;
                        }
                    }
                }
                string QTNum;
                List<PurchaseNoTemp> _PurchaseNoTemp = new List<PurchaseNoTemp>();
                List<FF.BusinessObjects.PurchaseOrder> _purhdrall = new List<FF.BusinessObjects.PurchaseOrder>();
                int k = 0;
                foreach (QuotationHeader qh in _quotationHeaderTemp)
                {

                    #region qhTemp
                    FF.BusinessObjects.PurchaseOrder _puOrder = new FF.BusinessObjects.PurchaseOrder();
                    _puOrder.PurchaseOrderDeliveryList = new List<PurchaseOrderDelivery>();
                    _puOrder.PurchaseOrderDetailList = new List<PurchaseOrderDetail>();
                    _puOrder.Poh_seq_no = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "PO", 1, Session["UserCompanyCode"].ToString()) +k;
                    _puOrder.Poh_tp = ddlMainType.SelectedValue;//"L";
                    _puOrder.Poh_sub_tp = _type;
                    _puOrder.Poh_doc_no = txtPurNo.Text.Trim();
                    _puOrder.Poh_com = Session["UserCompanyCode"].ToString();
                    _puOrder.Poh_ope = "INV";
                    _puOrder.Poh_profit_cd = Session["UserDefProf"].ToString();
                    _puOrder.Poh_dt = Convert.ToDateTime(txtPoDate.Text).Date;
                    _puOrder.Poh_ref = txtSupRef.Text;
                    _puOrder.Poh_job_no = txtJob.Text;
                    _puOrder.Poh_pay_term = ddlPayTerm.SelectedItem.Text;
                    _puOrder.Poh_supp = qh.Qh_party_cd;
                    _puOrder.Poh_cur_cd = txtCurrency.Text;
                    _puOrder.Poh_ex_rt = Convert.ToDecimal(lblEx.Text);
                    _puOrder.Poh_trans_term = "";
                    _puOrder.Poh_port_of_orig = "";
                    _puOrder.Poh_cre_period = txtCreditPeriod.Text;
                    _puOrder.Poh_frm_yer = Convert.ToDateTime(txtPoDate.Text).Year;
                    _puOrder.Poh_frm_mon = Convert.ToDateTime(txtPoDate.Text).Month;
                    _puOrder.Poh_to_yer = Convert.ToDateTime(txtPoDate.Text).Year;
                    _puOrder.Poh_to_mon = Convert.ToDateTime(txtPoDate.Text).Month;
                    _puOrder.Poh_preferd_eta = Convert.ToDateTime(txtPoDate.Text).Date;
                    _puOrder.Poh_contain_kit = false;
                    _puOrder.Poh_sent_to_vendor = false;
                    _puOrder.Poh_sent_by = "";
                    _puOrder.Poh_sent_via = "";
                    _puOrder.Poh_sent_add = "";
                    _puOrder.Poh_stus = "P";
                    _puOrder.Poh_remarks = txtRemarks.Text;
                    _puOrder.Poh_sub_tot = 0;
                    _puOrder.Poh_tax_tot = 0;
                    _puOrder.Poh_dis_rt = 0;
                    _puOrder.Poh_dis_amt = 0;
                    _puOrder.Poh_oth_tot = 0;
                    _puOrder.Poh_tot = 0;
                    List<PurchaseOrderDetail> _POItemListSave = new List<PurchaseOrderDetail>();
                    _POItemList = new List<PurchaseOrderDetail>();
                    bool _isVatClaim = false;
                    string _suppTaxCate = "";
                    _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), _puOrder.Poh_supp, null, null, "S");
                    foreach (var item in _quotationHeader)
                    {
                        #region QH
                        if (qh.Qh_party_cd == item.Qh_party_cd)
                        {
                            PurchaseOrderDetail _tmpPoDetails = new PurchaseOrderDetail();
                            MasterItem _tmpItem = new MasterItem();
                            _tmpItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), item.Qd_itm_cd);
                            if ((_POItemList.Count > 0))
                            {
                                var max_Query =
                             (from tab1 in _POItemList
                              select tab1.Pod_line_no).Max();

                                _lineNo = max_Query;
                            }
                            else
                            {
                                _lineNo = 0;
                            }
                            if (_supDet != null)
                            {
                                _isVatClaim = _supDet.Mbe_is_tax;
                                _suppTaxCate = _supDet.Mbe_cate;
                            }
                            else
                            {
                                ErrorMsgPOrder("Cannot find supplier details.");
                                return;
                            }
                            decimal _taxForActual = 0;
                            if (_tmpItem != null)
                            {
                                _lineNo = _lineNo + 1;
                                // Add po items ______________________
                                _tmpPoDetails.Pod_act_unit_price = item.Qh_anal_5;
                                if (string.IsNullOrEmpty(_suppTaxCate))
                                {
                                    // _tmpPoDetails.Pod_act_unit_price = (Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text) - Convert.ToDecimal(txtDiscount.Text) + Convert.ToDecimal(txtTax.Text)) / Convert.ToDecimal(txtQty.Text);
                                }
                                else
                                {
                                    decimal _unitVal = item.Qh_anal_5;
                                    decimal _qty = item.Qd_frm_qty;
                                    decimal _amt = _unitVal * _qty;
                                    decimal _disRate = 0;
                                    decimal _disAmount = 0;

                                    _taxForActual = TaxCalculationActualCost(Session["UserCompanyCode"].ToString(), item.Qd_itm_cd,
                                        ddlStatus.SelectedValue.Trim(), _amt - _disAmount, _suppTaxCate, 0);
                                    _tmpPoDetails.Pod_act_unit_price = ((item.Qd_frm_qty * item.Qh_anal_5) + _taxForActual) / item.Qd_frm_qty;
                                }
                                _tmpPoDetails.Pod_dis_amt = 0;
                                _tmpPoDetails.Pod_dis_rt = 0;
                                _tmpPoDetails.Pod_grn_bal = item.Qd_frm_qty;
                                _tmpPoDetails.Pod_item_desc = _tmpItem.Mi_shortdesc;
                                _tmpPoDetails.Pod_itm_cd = item.Qd_itm_cd;
                                if (ddlStatus.SelectedItem.Text == "CONS")
                                {
                                    _tmpPoDetails.Pod_itm_stus = "CONS";
                                }
                                else
                                {
                                    _tmpPoDetails.Pod_itm_stus = ddlStatus.SelectedValue.Trim();
                                }
                                _tmpPoDetails.Pod_itm_tp = _tmpItem.Mi_itm_tp;
                                _tmpPoDetails.Pod_kit_itm_cd = "";
                                _tmpPoDetails.Pod_kit_line_no = 0;
                                _tmpPoDetails.Pod_lc_bal = 0;
                                _tmpPoDetails.Pod_line_amt = item.Qh_anal_5 * item.Qd_frm_qty;
                                _tmpPoDetails.Pod_line_no = _lineNo;
                                decimal _tax = 0;
                                _tax = TaxCalculation(Session["UserCompanyCode"].ToString(), item.Qd_itm_cd, _tmpPoDetails.Pod_itm_stus, _tmpPoDetails.Pod_line_amt, 0);
                                _tmpPoDetails.Pod_line_tax = _tax;
                                _tmpPoDetails.Pod_line_val = item.Qh_anal_5 * item.Qd_frm_qty;
                                _tmpPoDetails.Pod_nbt = 0;
                                _tmpPoDetails.Pod_nbt_before = 0;
                                _tmpPoDetails.Pod_pi_bal = 0;
                                _tmpPoDetails.Pod_qty = item.Qd_frm_qty;
                                _tmpPoDetails.Pod_ref_no = item.Qh_no;
                                _tmpPoDetails.Pod_seq_no = 12;
                                _tmpPoDetails.Pod_si_bal = 0;
                                _tmpPoDetails.Pod_tot_tax_before = 0;
                                _tmpPoDetails.Pod_unit_price = item.Qh_anal_5;
                                _tmpPoDetails.Pod_uom = _tmpItem.Mi_itm_uom;
                                _tmpPoDetails.Pod_vat = _tmpPoDetails.Pod_line_tax - _tmpPoDetails.Pod_nbt;
                                _tmpPoDetails.Pod_vat_before = 0;
                                _tmpPoDetails.Pod_req_no = item.Tmp_req_no;
                                // _POItemList.Add(_tmpPoDetails);
                                _puOrder.PurchaseOrderDetailList.Add(_tmpPoDetails);
                                #region QH 2
                              
                            }
                        }
                        #endregion
                    }
                    List<PurchaseOrderDelivery> _poDeliveryList = new List<PurchaseOrderDelivery>();
                    #region Delivery
                    foreach (PurchaseOrderDetail item2 in _puOrder.PurchaseOrderDetailList)
                    {
                        Int32 _maxLine = 0;
                        if (_puOrder.PurchaseOrderDeliveryList != null)
                        {
                            if (_puOrder.PurchaseOrderDeliveryList.Count>0)
                            {
                                _maxLine = _puOrder.PurchaseOrderDeliveryList.Max(c => c.Podi_del_line_no);
                            }
                        }
                        
                        PurchaseOrderDelivery _poDel = new PurchaseOrderDelivery();
                        _poDel.Podi_seq_no = item2.Pod_seq_no;
                        _poDel.Podi_line_no = item2.Pod_line_no;
                        _poDel.Podi_del_line_no = _maxLine + 1;
                        _poDel.Podi_loca = Session["UserDefLoca"].ToString();
                        _poDel.Podi_itm_cd = item2.Pod_itm_cd;
                        _poDel.Podi_itm_stus = item2.Pod_itm_stus;
                        _poDel.Podi_qty = item2.Pod_qty;
                        _poDel.Podi_bal_qty = item2.Pod_qty;
                        // _poDeliveryList.Add(_poDel);
                        _puOrder.PurchaseOrderDeliveryList.Add(_poDel);
                    }
                    if (_puOrder.PurchaseOrderDetailList.Count > 0)
                    {
                        _puOrder.Poh_sub_tot = _puOrder.PurchaseOrderDetailList.Select(x => x.Pod_line_amt).Sum();
                        _puOrder.Poh_tax_tot = _puOrder.PurchaseOrderDetailList.Select(x => x.Pod_line_tax).Sum();
                        _puOrder.Poh_dis_rt = 0;
                        _puOrder.Poh_dis_amt = 0;
                        _puOrder.Poh_oth_tot = 0;
                        _puOrder.Poh_tot = _puOrder.Poh_sub_tot + _puOrder.Poh_tax_tot - _puOrder.Poh_dis_amt;
                    }
                    _puOrder.Poh_reprint = false;
                    _puOrder.Poh_tax_chg = false;
                    _puOrder.poh_is_conspo = _isBaseCons;
                    _puOrder.Poh_cre_by = Session["UserID"].ToString();

                    #endregion
                    #endregion
                 
                    List<PurchaseOrderDelivery> _PODelSave = new List<PurchaseOrderDelivery>();
                    if (ViewState["PurchaseReqList"] != null)
                    {
                        _PurchaseReqList = ViewState["PurchaseReqList"] as List<PurchaseReq>;
                    }
                    if (ViewState["InventoryRequestItem"] != null)
                    {
                        _InventoryRequestItem = ViewState["InventoryRequestItem"] as List<InventoryRequestItem>;
                    }
                    #endregion
                    k++;
                    _purhdrall.Add(_puOrder);
                }
                
                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                masterAuto.Aut_cate_tp = "COM";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "PUR";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "PUR";
                masterAuto.Aut_year = null;           
             
                //Add all items to service
                row_aff = CHNLSVC.Inventory.SaveAllPOList(_purhdrall, masterAuto, _PurchaseReqList, _InventoryRequestItem, out QTNum);

                if (row_aff >= 0)
                {
                    _PurchaseNoTemp.Add(new PurchaseNoTemp() { PoNo = QTNum });
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        ErrorMsgPOrder(_msg);
                    }
                    else
                    {
                        ErrorMsgPOrder("Faild to generate");
                    }
                }

                if (row_aff >= 0)
                {
                    string msg = "";
                    int i = 0;
                    foreach (var v in _PurchaseNoTemp)
                    {
                        msg += i == 0 ? v.PoNo : ", " + v.PoNo;
                        i++;
                    }
                    if (_PurchaseNoTemp.Count == 1)
                    {
                        //DispMsg("Purchase orders generated " + msg, "S");
                        SuccessMsgPOrder("Purchase order generated " + msg);
                    }
                    else
                    {
                        dgvPoSaved.DataSource = new int[] { };
                        dgvPoSaved.DataSource = _PurchaseNoTemp;
                        dgvPoSaved.DataBind();
                        popupSaveData.Show();

                    }
                    ClearTextBox();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_msg))
                    {
                        ErrorMsgPOrder(_msg);
                    }
                    else
                    {
                        ErrorMsgPOrder("Faild to generate");
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMsgPOrder("Error Occurred while processing...\n");
            }
        }
        protected void allPOItems_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                var lb = (CheckBox)sender;
                var row = (GridViewRow)lb.NamingContainer;
                CheckBox allPOItems = (CheckBox)row.FindControl("allPOItems");
                foreach (GridViewRow r in grdReqItems.Rows)
                {
                    CheckBox chk_ReqItem = (CheckBox)r.FindControl("chk_ReqItem");
                    chk_ReqItem.Checked = allPOItems.Checked;
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error occured", "E");
            }
        }

        protected void lbtnPrintPO_Click(object sender, EventArgs e)
        {
            try
            {
                ClearErrorMsg();
                if (string.IsNullOrEmpty(txtPurNo.Text))
                {
                    ErrorMsgPOrder("Please select purchase order.");
                    txtPurNo.Focus();
                    return;
                }


                //if (_IsRecall == false)
                //{
                //    ErrorMsgPOrder("Please recall purchase order.");
                //    txtPurNo.Focus();
                //    return;
                //}

                if (lblStatus.Text == "PENDING" && !_isApprovePrint)
                {
                    ErrorMsgPOrder("Selected order is still in pending status.");

                    txtPurNo.Focus();
                    return;
                }
                _isApprovePrint = false;
                Session["GlbReportType"] = "SCM1_PO";
                Session["ReportDoc"] = txtPurNo.Text.Trim();
                BaseCls.GlbReportHeading = "Purchase Order Report";

                if (Session["UserCompanyCode"].ToString() == "ARL")
                {


                    Session["GlbReportName"] = "Rep_Pur_Order_ARL.rpt";
                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                    clsInventory obj = new clsInventory();
                    obj.purchaseOrderPrint_ARL(Session["UserCompanyCode"].ToString(), Session["ReportDoc"].ToString());
                    PrintPDF(targetFileName, obj._PoPrint_ARL);


                }
                else if (Session["UserCompanyCode"].ToString() == "AAL")
                {
                    Session["GlbReportName"] = "Rep_Pur_OrderN_AAL.rpt";
                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                    clsInventory obj = new clsInventory();
                    obj.purchaseOrderPrint_AAL(Session["UserCompanyCode"].ToString(), Session["ReportDoc"].ToString());
                    PrintPDF(targetFileName, obj._PoPrint_AAL);
                }


                    // code add by tharindu 2018-01-26 for all report purpose
                else
                {
                    Session["GlbReportName"] = "Rep_Pur_OrderN_ABE.rpt";
                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                    clsInventory obj = new clsInventory();
                    obj.purchaseOrderPrint_ABE(Session["UserCompanyCode"].ToString(), Session["ReportDoc"].ToString());
                    PrintPDF(targetFileName, obj._PoPrint_ABE);
                }

                //else if (Session["UserCompanyCode"].ToString() == "ABE")
                //{
                //    Session["GlbReportName"] = "Rep_Pur_OrderN_ABE.rpt";
                //    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                //    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                //    clsInventory obj = new clsInventory();
                //    obj.purchaseOrderPrint_ABE(Session["UserCompanyCode"].ToString(), Session["ReportDoc"].ToString());
                //    PrintPDF(targetFileName, obj._PoPrint_ABE);
                //}

                //else
                //{
                //    //Session["GlbReportName"] = "Rep_Pur_OrderN_ABL_N.rpt";
                //    //string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                //    //string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                //    //clsInventory obj = new clsInventory();
                //    //obj.purchaseOrderPrint(Session["UserCompanyCode"].ToString(), Session["ReportDoc"].ToString());
                //    //PrintPDF(targetFileName, obj._PoPrint_ABL);

                //    Session["GlbReportName"] = "Rep_Pur_OrderN.rpt";
                //    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                //    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                //    clsInventory obj = new clsInventory();
                //    obj.purchaseOrderPrint(Session["UserCompanyCode"].ToString(), Session["ReportDoc"].ToString());
                //    PrintPDF(targetFileName, obj._PoPrint);


                //}

                //string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                //string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                //clsInventory obj = new clsInventory();
                //obj.purchaseOrderPrint(Session["UserCompanyCode"].ToString(), Session["ReportDoc"].ToString());
                //PrintPDF(targetFileName, obj._PoPrint);

                string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                //Response.Redirect("~/View/Reports/Inventory/InventoryReportViewer.aspx", false);
            }
            catch(Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("PO Print", "PurchaseOrder", ex.Message, Session["UserID"].ToString());
            }
        }

        public void PrintPDF(string targetFileName, ReportDocument _rpt)
        {
            try
            {
                ReportDocument rptDoc = (ReportDocument)_rpt;
                DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                diskOpts.DiskFileName = targetFileName;
                rptDoc.ExportOptions.DestinationOptions = diskOpts;
                rptDoc.Export();

                rptDoc.Close();
                rptDoc.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void grdresultdd_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdresultdd_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void lbtnSearchDd_Click(object sender, EventArgs e)
        {

        }

        protected void txtSearchbywordDd_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnDateSd_Click(object sender, EventArgs e)
        {

        }

        protected void btndclose_Click(object sender, EventArgs e)
        {

        }

        protected void btndclose_Click1(object sender, EventArgs e)
        {

        }

        protected void btndDclose_Click(object sender, EventArgs e)
        {

        }
        private bool IsAllowBackDateForModuleNew(string _com, string _loc, string _pc, System.Web.UI.Page _page, string _backdate, LinkButton _imgcontrol, Label _lblcontrol, String moduleName, out bool _allowCurrentTrans)
        {
            string _outmsg = "";
            BackDates _bdt = new BackDates();
            _imgcontrol.Visible = false;
            _allowCurrentTrans = false;
            string _filename = string.Empty;

            if (String.IsNullOrEmpty(moduleName))
            {
                _filename = _page.AppRelativeVirtualPath.Substring(_page.AppRelativeVirtualPath.LastIndexOf("/") + 1).Replace(".aspx", "").ToUpper();
            }
            else
            {
                _filename = moduleName;
            }
            bool _isAllow = CHNLSVC.General.IsAllowBackDateForModule(_com, _loc, _pc, _filename, _backdate, out _bdt);
            if (_isAllow == true)
            {
                _imgcontrol.Visible = true;
                if (_bdt != null)
                {
                    _outmsg = "This module back dated. Valid period is from " + _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy") + " date to " + _bdt.Gad_act_to_dt.Date.ToString("dd/MMM/yyyy") + " date.";
                    txtPoDate.Text = _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy");
                }
                else
                {
                    DateTime date = DateTime.Now;
                    txtPoDate.Text = date.ToString("dd/MMM/yyyy");
                }
            }

            if (_bdt == null)
            {
                _allowCurrentTrans = true;
                DateTime date = DateTime.Now;
                txtPoDate.Text = date.ToString("dd/MMM/yyyy");
                lbtnPIDate.Visible = true;
            }
            else
            {
                if (_bdt.Gad_alw_curr_trans == true)
                {
                    _allowCurrentTrans = true;
                    lbtnPIDate.Visible = true;
                }
                else
                {
                    lbtnPIDate.Visible = false;
                }
            }

            //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _outmsg);
            _lblcontrol.Text = _outmsg;

            return _isAllow;
        }
        protected void btnok_Click(object sender, EventArgs e)
        {
            _chksaveso = true;
            _sveso = true;
            lbtnAdd_Click(null, null);
        }
        protected void btnno_Click(object sender, EventArgs e)
        {
            _chksaveso = true;
            _sveso = false;
            lbtnAdd_Click(null, null);
        }

        protected void chkReqDate_CheckedChanged(object sender, EventArgs e)
        {
            chkReqDate.Checked = true;
            chkModDate.Checked = false;
        }

        protected void chkModDate_CheckedChanged(object sender, EventArgs e)
        {
            chkModDate.Checked = true;
            chkReqDate.Checked = false;
        }

        protected void chkall_Req_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow Row = ((GridViewRow)((Control)sender).Parent.Parent);
            CheckBox chkSelect = (CheckBox)Row.FindControl("chkall_Req");
            bool select = chkSelect.Checked;
            if (!chkMultiReq.Checked)
            {
                ErrorMsgPOrder("Please Check Multy Req" );
                return;
            }
            _itmData = new DataTable();

            foreach (GridViewRow row in grdReq.Rows)
            {
                    CheckBox chkRow = (row.FindControl("chk_Req") as CheckBox);

                    if (select == true)
                    {
                        chkRow.Checked = true;
                    }
                    else
                    {
                        chkRow.Checked = false;
                    }

                    
                    if (chkRow.Checked)
                    {
                        string _ReqNo = (row.FindControl("col_ReqNo") as Label).Text;
                        FilterData(_ReqNo);
                    }
            }
        }
    }


    public class PurchaseNoTemp
    {
        public string PoNo { get; set; }
    }
}
