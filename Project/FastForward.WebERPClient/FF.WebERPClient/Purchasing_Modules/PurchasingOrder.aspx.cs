using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using FF.BusinessObjects;
using FF.BusinessObjects.Sales;
using System.Globalization;
using System.Transactions;
using FF.WebERPClient.UserControls;

namespace FF.WebERPClient.Purchasing_Modules
{
    public partial class PurchasingOrder : BasePage
    {
        protected Boolean _IsCons
        {
            get { return (Boolean)Session["_IsCons"]; }
            set { Session["_IsCons"] = value; }
        }

        protected Boolean _IsRecall
        {
            get { return (Boolean)Session["_IsRecall"]; }
            set { Session["_IsRecall"] = value; }
        }

        protected string _POstatus
        {
            get { return (string)Session["_POstatus"]; }
            set { Session["_POstatus"] = value; }
        }

        protected Int32 _POSeqNo
        {
            get { return (Int32)Session["_POSeqNo"]; }
            set { Session["_POSeqNo"] = value; }
        }


        protected List<PurchaseOrderDetail> _POItemList
        {
            get { return (List<PurchaseOrderDetail>)Session["_POItemList"]; }
            set { Session["_POItemList"] = value; }
        }

        protected List<PurchaseOrderDelivery> _PODelList
        {
            get { return (List<PurchaseOrderDelivery>)Session["_PODelList"]; }
            set { Session["_PODelList"] = value; }
        }

        protected List<QoutationDetails> _SupplierQuotaionList
        {
            get { return (List<QoutationDetails>)Session["_SupplierQuotaionList"]; }
            set { Session["_SupplierQuotaionList"] = value; }
        }

        protected Int32 _lineNo
        {
            get { return (Int32)Session["_lineNo"]; }
            set { Session["_lineNo"] = value; }
        }

        protected Int32 _delLineNo
        {
            get { return (Int32)Session["_delLineNo"]; }
            set { Session["_delLineNo"] = value; }
        }

        protected decimal GrndSubTotal
        {
            get { return (decimal)Session["GrndSubTotal"]; }
            set { Session["GrndSubTotal"] = value; }
        }

        protected decimal GrndDiscount
        {
            get { return (decimal)Session["GrndDiscount"]; }
            set { Session["GrndDiscount"] = value; }
        }

        protected decimal GrndTax
        {
            get { return (decimal)Session["GrndTax"]; }
            set { Session["GrndTax"] = value; }
        }

        protected decimal delQty
        {
            get { return (decimal)Session["delQty"]; }
            set { Session["delQty"] = value; }
        }

        //protected static Boolean _IsCons = false;
        //protected static Boolean _IsRecall = false;
        //protected static string _POstatus = "";
        //protected static Int32 _POSeqNo = 0;
        //protected static List<PurchaseOrderDetail> _POItemList = new List<PurchaseOrderDetail>();
        //protected static List<PurchaseOrderDelivery> _PODelList = new List<PurchaseOrderDelivery>();
        //protected static List<QoutationDetails> _SupplierQuotaionList = null;
        //protected static Int32 _lineNo;
        //protected static Int32 _delLineNo;

        //protected static decimal GrndSubTotal = 0;
        //protected static decimal GrndDiscount = 0;
        //protected static decimal GrndTax = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtSupplier.Attributes.Add("onKeyup", "return clickButton(event,'" + imgsup.ClientID + "')");
                txtItem.Attributes.Add("onKeyup", "return clickButton(event,'" + imgItmSearch.ClientID + "')");
                txtItmStatus.Attributes.Add("onKeyup", "return clickButton(event,'" + imgStatusSearch.ClientID + "')");
                txtOrderNo.Attributes.Add("onKeyup", "return clickButton(event,'" + imgPOSearch.ClientID + "')");
                txtDelLoc.Attributes.Add("onKeyup", "return clickButton(event,'" + imgSearchLoc.ClientID + "')");

                txtItem.Attributes.Add("onkeypress", "uppercase();");
                txtItmStatus.Attributes.Add("onkeypress", "uppercase();");
                txtSupplier.Attributes.Add("onkeypress", "uppercase();");

                txtItem.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnItem, ""));
                txtQty.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnQty, ""));
                txtUPrice.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnUPrice, ""));
                txtDisRate.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnDisRate, ""));
                txtDisAmt.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnDisAmt, ""));
                txtTax.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnTax, ""));
                txtSupplier.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnSupplier, ""));
                txtOrderNo.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnPODoc, ""));
                txtItmStatus.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnItmStatus, ""));
                txtDelLoc.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btndelLoc, ""));
                ddlCur.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnExRate, ""));


                //txtSupplier.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtSupRef.ClientID + "').focus();return false;}} else {return true}; ");
                txtSupRef.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtDate.ClientID + "').focus();return false;}} else {return true}; ");
                txtDate.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtItem.ClientID + "').focus();return false;}} else {return true}; ");
                txtDelLoc.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnApply.ClientID + "').focus();return false;}} else {return true}; ");
                //txtItmStatus.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtQty.ClientID + "').focus();return false;}} else {return true}; ");
                //txtItem.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtQty.ClientID + "').focus();return false;}} else {return true}; ");
                //_IsCons = true;
                this.ClearData();
                _IsCons = false;
                _POstatus = "";
                _IsRecall = false;
                _POSeqNo = 0;
                _POItemList = new List<PurchaseOrderDetail>();
                _PODelList = new List<PurchaseOrderDelivery>();
                _SupplierQuotaionList = new List<QoutationDetails>();
                _lineNo = 0;
                _delLineNo = 0;
                GrndSubTotal = 0;
                GrndDiscount = 0;
                GrndTax = 0;
                delQty = 0;
                // _POItemList = new List<PurchaseOrderDetail>();
                // _PODelList = new List<PurchaseOrderDelivery>();
            }
        }

        #region Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.PurchaseOrder:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        protected void imgsup_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
            DataTable dataSource = CHNLSVC.CommonSearch.GetSupplierData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtSupplier.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgItmSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable dataSource = CHNLSVC.CommonSearch.GetItemSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtItem.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgStatusSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCompanyItemStatusData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtItmStatus.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgSearchLoc_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
            DataTable dataSource = CHNLSVC.CommonSearch.GetLocationSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtDelLoc.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        #region Clear
        protected void btnClear_Click(object sender, EventArgs e)
        {
            this.ClearData();
            GrndSubTotal = 0;
            GrndDiscount = 0;
            GrndTax = 0;
            _lineNo = 0;
            _delLineNo = 0;
            _IsRecall = false;
            _POSeqNo = 0;
            _POstatus = "";
            _POItemList = new List<PurchaseOrderDetail>();
            // BindAddItem();
            _PODelList = new List<PurchaseOrderDelivery>();
            // BindAddDelItem();
            List<QoutationDetails> _list = null;
            BindSupplierQuotation(_list);
            //_POItemList = new List<PurchaseOrderDetail>();
            //BindAddItem();
            //_PODelList = new List<PurchaseOrderDelivery>();

            // DataTable _table = new DataTable();
            //  _table.Columns.AddRange(new DataColumn[] { new DataColumn("podi_seq_no"), new DataColumn("podi_line_no")});
            //BindAddDelItem();
            // gvDelDetails.DataSource = _table;
            // gvDelDetails.DataBind();

            // List<QoutationDetails> _list = null;
            // BindSupplierQuotation(_list);
            _IsRecall = false;
            _IsCons = false;
            cheBaseCon.Enabled = false;
        }

        private void ClearData()
        {
            txtSupplier.Text = "";
            txtSupRef.Text = "N/A";
            txtDate.Text = Convert.ToString(DateTime.Today.ToShortDateString());
            txtOrderNo.Text = "";
            txtItem.Text = "";
            chkIsCons.Checked = false;
            txtItmStatus.Text = "";
            txtItmStatus.Enabled = true;
            imgStatusSearch.Enabled = true;
            txtDelLoc.Text = "";
            //txtDelLoc.ReadOnly = true;
            txtQty.Text = "";
            txtUPrice.Text = "";
            txtAmount.Text = "";
            txtTax.Text = "";
            txtDisRate.Text = "";
            txtDisAmt.Text = "";
            txtTotal.Text = "";
            txtRemarks.Text = "";
            lblSubAmt.Text = "0.00";
            lblDisAmt.Text = "0.00";
            lblTaxAmt.Text = "0.00";
            lblTotAmt.Text = "0.00";
            cheBaseCon.Checked = false;
            cheBaseCon.Enabled = false;
            chkIsCons.Enabled = true;
            lblSeq.Text = "";
            lbldelLine.Text = "";
            lblDelItem.Text = "";
            lblDelStatus.Text = "";
            lblDelQty.Text = "";
            txtEditDelQty.Text = "";
            lblExRate.Text = "";
            BindCurrency(ddlCur);
            ddlCur.Text = "LKR";
            LoadExRate();
            DataTable _table = new DataTable();
            //  _table.Columns.AddRange(new DataColumn[] { new DataColumn("podi_seq_no"), new DataColumn("podi_line_no")});
            //BindAddDelItem();
            //_POItemList = new List<PurchaseOrderDetail>();
            //// BindAddItem();
            //_PODelList = new List<PurchaseOrderDelivery>();
            //// BindAddDelItem();
            //List<QoutationDetails> _list = null;
            //BindSupplierQuotation(_list);

            gvDelDetails.DataSource = _table;
            gvDelDetails.DataBind();

            DataTable _Itemtable = new DataTable();
            gvPOItem.DataSource = _Itemtable;
            gvPOItem.DataBind();

            DataTable _Quoitem = new DataTable();
            gvQuotation.DataSource = _Quoitem;
            gvQuotation.DataBind();
            //_lineNo = 0;
            //_delLineNo = 0;

            // _IsRecall = false;
            // _POstatus = "";
            btnApproved.Enabled = false;
            btnCancel.Enabled = false;
            btnSave.Enabled = true;
            //_POSeqNo = 0;
            // GrndSubTotal = 0;
            // GrndDiscount = 0;
            // GrndTax = 0;
        }

        #endregion

        protected void BindAddItem()
        {
            // if (_POItemList.Count > 0)
            //  {
            gvPOItem.DataSource = _POItemList;
            gvPOItem.DataBind();
            //  }
        }

        protected void BindAddDelItem()
        {
            // if (_PODelList.Count >0)
            // {
            gvDelDetails.DataSource = _PODelList;
            gvDelDetails.DataBind();
            // }
        }




        private PurchaseOrderDelivery AssignDatatoObject()
        {
            PurchaseOrderDelivery _tempDelItem = new PurchaseOrderDelivery();

            _tempDelItem.Podi_seq_no = 12;
            _tempDelItem.Podi_line_no = _lineNo;
            _tempDelItem.Podi_del_line_no = _delLineNo;
            _tempDelItem.Podi_loca = GlbUserDefLoca;
            _tempDelItem.Podi_itm_cd = txtItem.Text.Trim();
            _tempDelItem.Podi_itm_stus = txtItmStatus.Text.Trim();
            _tempDelItem.Podi_qty = Convert.ToDecimal(txtQty.Text);
            _tempDelItem.Podi_bal_qty = Convert.ToDecimal(txtQty.Text);
            return _tempDelItem;
        }

        private PurchaseOrderDelivery AssignEditDatatoObject()
        {
            PurchaseOrderDelivery _tempDelItem = new PurchaseOrderDelivery();

            _tempDelItem.Podi_seq_no = 12;
            _tempDelItem.Podi_line_no = Convert.ToInt32(lblSeq.Text);
            _tempDelItem.Podi_del_line_no = Convert.ToInt32(lbldelLine.Text);
            _tempDelItem.Podi_loca = txtDelLoc.Text;
            _tempDelItem.Podi_itm_cd = lblDelItem.Text;
            _tempDelItem.Podi_itm_stus = lblDelStatus.Text;
            _tempDelItem.Podi_qty = Convert.ToDecimal(txtEditDelQty.Text);
            _tempDelItem.Podi_bal_qty = Convert.ToDecimal(txtEditDelQty.Text);
            return _tempDelItem;
        }

        private PurchaseOrderDetail AssignDataToObject()
        {

            //String.Format("{0:0,0.0}", 12345.67); 
            PurchaseOrderDetail _tempItem = new PurchaseOrderDetail();

            _tempItem.Pod_act_unit_price = (Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUPrice.Text) - Convert.ToDecimal(txtDisAmt.Text) + Convert.ToDecimal(txtTax.Text)) / Convert.ToDecimal(txtQty.Text);
            _tempItem.Pod_dis_amt = Convert.ToDecimal(txtDisAmt.Text);
            _tempItem.Pod_dis_rt = Convert.ToDecimal(txtDisRate.Text);
            _tempItem.Pod_grn_bal = Convert.ToDecimal(txtQty.Text);
            _tempItem.Pod_item_desc = "";
            _tempItem.Pod_itm_cd = txtItem.Text;
            _tempItem.Pod_itm_stus = txtItmStatus.Text;
            _tempItem.Pod_itm_tp = "";
            _tempItem.Pod_kit_itm_cd = "";
            _tempItem.Pod_kit_line_no = 0;
            _tempItem.Pod_lc_bal = 0;
            _tempItem.Pod_line_amt = Convert.ToDecimal(txtTotal.Text);
            _tempItem.Pod_line_no = _lineNo;
            _tempItem.Pod_line_tax = Convert.ToDecimal(txtTax.Text);
            _tempItem.Pod_line_val = Convert.ToDecimal(txtAmount.Text);
            _tempItem.Pod_nbt = 0;
            _tempItem.Pod_nbt_before = 0;
            _tempItem.Pod_pi_bal = 0;
            _tempItem.Pod_qty = Convert.ToDecimal(txtQty.Text);
            _tempItem.Pod_ref_no = txtSupRef.Text;
            _tempItem.Pod_seq_no = 12;
            _tempItem.Pod_si_bal = 0;
            _tempItem.Pod_tot_tax_before = 0;
            _tempItem.Pod_unit_price = Convert.ToDecimal(txtUPrice.Text);
            _tempItem.Pod_uom = "";
            _tempItem.Pod_vat = 0;
            _tempItem.Pod_vat_before = 0;
            return _tempItem;


        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter item.");
                txtItem.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtQty.Text))
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter qty.");
                txtQty.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtUPrice.Text))
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter unit price.");
                txtUPrice.Focus();
                return;
            }

            var currrange = (from cur in _POItemList
                             where cur.Pod_itm_cd == txtItem.Text.Trim() && cur.Pod_unit_price == Convert.ToDecimal(txtUPrice.Text)
                             select cur).ToList();

            if (currrange.Count > 0)// ||currrange !=null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected item already exsist with same price.");
                return;
            }

            _lineNo += 1;
            _POItemList.Add(AssignDataToObject());
            BindAddItem();
            _delLineNo += 1;
            _PODelList.Add(AssignDatatoObject());
            BindAddDelItem();

            CalculateGrandTotal(Convert.ToDecimal(txtQty.Text), Convert.ToDecimal(txtUPrice.Text), Convert.ToDecimal(txtDisAmt.Text), Convert.ToDecimal(txtTax.Text), true);

            txtItem.Text = "";
            if (_IsCons == true)
            {
                txtItmStatus.Text = "CONS";
                txtItmStatus.Enabled = false;
                imgStatusSearch.Enabled = false;
            }
            else
            {
                txtItmStatus.Text = "";
                txtItmStatus.Enabled = true;
                imgStatusSearch.Enabled = true;
            }
            chkIsCons.Enabled = false;
            txtQty.Text = "";
            txtUPrice.Text = "";
            txtAmount.Text = "";
            txtTax.Text = "";
            txtDisRate.Text = "";
            txtDisAmt.Text = "";
            txtTotal.Text = "";
            DataTable _Quoitem = new DataTable();
            gvQuotation.DataSource = _Quoitem;
            gvQuotation.DataBind();
            txtItem.Focus();
        }

        private void UpdatePOStatus(string _POUpdateStatus, bool _isApproved)
        {
            Int32 row_aff = 0;
            string _msg = string.Empty;

            PurchaseOrder _UpdatepurchaseOrder = new PurchaseOrder();
            _UpdatepurchaseOrder.Poh_seq_no = _POSeqNo;
            _UpdatepurchaseOrder.Poh_doc_no = txtOrderNo.Text.Trim();
            _UpdatepurchaseOrder.Poh_stus = _POUpdateStatus;
            _UpdatepurchaseOrder.Poh_com = GlbUserComCode;

            row_aff = (Int32)CHNLSVC.Inventory.UpdatePOStatus(_UpdatepurchaseOrder);

            if (row_aff == 1)
            {
                if (_isApproved == true)
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully approved. ");
                }
                else if (_isApproved == false)
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully cancelled. ");
                }
                ClearData();
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


        //Update purchase order
        private void UpdatePurchaseOrder()
        {
            Int32 row_aff = 0;
            string _msg = string.Empty;
            Int32 _isBaseCons = 0;

            string _type = string.Empty;

            if (_IsCons == true)
            {
                _type = "C";
            }
            else
            {
                _type = "N";
            }

            if (cheBaseCon.Checked == true)
            {
                _isBaseCons = 1;
            }
            else
            {
                _isBaseCons = 0;
            }


            PurchaseOrder _PurchaseOrder = new PurchaseOrder();
            _PurchaseOrder.Poh_seq_no = _POSeqNo;
            _PurchaseOrder.Poh_tp = "L";
            _PurchaseOrder.Poh_sub_tp = _type;
            _PurchaseOrder.Poh_doc_no = txtOrderNo.Text.Trim();
            _PurchaseOrder.Poh_com = GlbUserComCode;
            _PurchaseOrder.Poh_ope = "INV";
            _PurchaseOrder.Poh_profit_cd = "";
            _PurchaseOrder.Poh_dt = Convert.ToDateTime(txtDate.Text).Date;
            _PurchaseOrder.Poh_ref = txtSupRef.Text;
            _PurchaseOrder.Poh_job_no = "";
            _PurchaseOrder.Poh_pay_term = "";
            _PurchaseOrder.Poh_supp = txtSupplier.Text;
            _PurchaseOrder.Poh_cur_cd = ddlCur.SelectedValue;
            _PurchaseOrder.Poh_ex_rt = Convert.ToDecimal(lblExRate.Text);
            _PurchaseOrder.Poh_trans_term = "";
            _PurchaseOrder.Poh_port_of_orig = "";
            _PurchaseOrder.Poh_cre_period = "0";
            _PurchaseOrder.Poh_frm_yer = Convert.ToDateTime(txtDate.Text).Year;
            _PurchaseOrder.Poh_frm_mon = Convert.ToDateTime(txtDate.Text).Month;
            _PurchaseOrder.Poh_to_yer = Convert.ToDateTime(txtDate.Text).Year;
            _PurchaseOrder.Poh_to_mon = Convert.ToDateTime(txtDate.Text).Month;
            _PurchaseOrder.Poh_preferd_eta = Convert.ToDateTime(txtDate.Text).Date;
            _PurchaseOrder.Poh_contain_kit = false;
            _PurchaseOrder.Poh_sent_to_vendor = false;
            _PurchaseOrder.Poh_sent_by = "";
            _PurchaseOrder.Poh_sent_via = "";
            _PurchaseOrder.Poh_sent_add = "";
            _PurchaseOrder.Poh_stus = "P";
            _PurchaseOrder.Poh_remarks = txtRemarks.Text;
            _PurchaseOrder.Poh_sub_tot = Convert.ToDecimal(lblSubAmt.Text);
            _PurchaseOrder.Poh_tax_tot = Convert.ToDecimal(lblTaxAmt.Text);
            _PurchaseOrder.Poh_dis_rt = 0;
            _PurchaseOrder.Poh_dis_amt = Convert.ToDecimal(lblDisAmt.Text);
            _PurchaseOrder.Poh_oth_tot = 0;
            _PurchaseOrder.Poh_tot = Convert.ToDecimal(lblTotAmt.Text);
            _PurchaseOrder.Poh_reprint = false;
            _PurchaseOrder.Poh_tax_chg = false;
            _PurchaseOrder.poh_is_conspo = _isBaseCons;


            List<PurchaseOrderDetail> _POItemListSave = new List<PurchaseOrderDetail>();
            foreach (PurchaseOrderDetail line in _POItemList)
            {
                line.Pod_seq_no = _PurchaseOrder.Poh_seq_no;
                _POItemListSave.Add(line);
            }

            List<PurchaseOrderDelivery> _PODelSave = new List<PurchaseOrderDelivery>();
            foreach (PurchaseOrderDelivery line in _PODelList)
            {
                line.Podi_seq_no = _PurchaseOrder.Poh_seq_no;
                _PODelSave.Add(line);
            }

            //MasterAutoNumber masterAuto = new MasterAutoNumber();
            //masterAuto.Aut_cate_cd = GlbUserComCode;
            //masterAuto.Aut_cate_tp = "COM";
            //masterAuto.Aut_direction = null;
            //masterAuto.Aut_modify_dt = null;
            //masterAuto.Aut_moduleid = "PO_LOCAL";
            //masterAuto.Aut_number = 5;//what is Aut_number
            //masterAuto.Aut_start_char = "PO";
            //masterAuto.Aut_year = null;

            //string QTNum;

            row_aff = (Int32)CHNLSVC.Inventory.UpdateSavedPO(_PurchaseOrder, _POItemListSave, _PODelSave, _PurchaseOrder.Poh_seq_no);

            if (row_aff == 1)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully updated.");
                ClearData();
            }
            else
            {
                if (!string.IsNullOrEmpty(_msg))
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _msg);
                }
                else
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Update Fail.");
                }
            }
        }

        //save Purchase Ordedr
        private void SavePOHeader()
        {
            Int32 row_aff = 0;
            string _PONo = string.Empty;
            string _msg = string.Empty;
            Int32 _isBaseCons = 0;
            string _type = string.Empty;

            if (_IsCons == true)
            {
                _type = "C";
            }
            else
            {
                _type = "N";
            }

            if (cheBaseCon.Checked == true)
            {
                _isBaseCons = 1;
            }
            else
            {
                _isBaseCons = 0;
            }


            PurchaseOrder _PurchaseOrder = new PurchaseOrder();
            _PurchaseOrder.Poh_seq_no = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "PO", 1, GlbUserComCode);
            _PurchaseOrder.Poh_tp = "L";
            _PurchaseOrder.Poh_sub_tp = _type;
            _PurchaseOrder.Poh_doc_no = txtOrderNo.Text.Trim();
            _PurchaseOrder.Poh_com = GlbUserComCode;
            _PurchaseOrder.Poh_ope = "INV";
            _PurchaseOrder.Poh_profit_cd = "";
            _PurchaseOrder.Poh_dt = Convert.ToDateTime(txtDate.Text).Date;
            _PurchaseOrder.Poh_ref = txtSupRef.Text;
            _PurchaseOrder.Poh_job_no = "";
            _PurchaseOrder.Poh_pay_term = "";
            _PurchaseOrder.Poh_supp = txtSupplier.Text;
            _PurchaseOrder.Poh_cur_cd = ddlCur.SelectedValue;
            _PurchaseOrder.Poh_ex_rt = Convert.ToDecimal(lblExRate.Text);
            _PurchaseOrder.Poh_trans_term = "";
            _PurchaseOrder.Poh_port_of_orig = "";
            _PurchaseOrder.Poh_cre_period = "0";
            _PurchaseOrder.Poh_frm_yer = Convert.ToDateTime(txtDate.Text).Year;
            _PurchaseOrder.Poh_frm_mon = Convert.ToDateTime(txtDate.Text).Month;
            _PurchaseOrder.Poh_to_yer = Convert.ToDateTime(txtDate.Text).Year;
            _PurchaseOrder.Poh_to_mon = Convert.ToDateTime(txtDate.Text).Month;
            _PurchaseOrder.Poh_preferd_eta = Convert.ToDateTime(txtDate.Text).Date;
            _PurchaseOrder.Poh_contain_kit = false;
            _PurchaseOrder.Poh_sent_to_vendor = false;
            _PurchaseOrder.Poh_sent_by = "";
            _PurchaseOrder.Poh_sent_via = "";
            _PurchaseOrder.Poh_sent_add = "";
            _PurchaseOrder.Poh_stus = "P";
            _PurchaseOrder.Poh_remarks = txtRemarks.Text;
            _PurchaseOrder.Poh_sub_tot = Convert.ToDecimal(lblSubAmt.Text);
            _PurchaseOrder.Poh_tax_tot = Convert.ToDecimal(lblTaxAmt.Text);
            _PurchaseOrder.Poh_dis_rt = 0;
            _PurchaseOrder.Poh_dis_amt = Convert.ToDecimal(lblDisAmt.Text);
            _PurchaseOrder.Poh_oth_tot = 0;
            _PurchaseOrder.Poh_tot = Convert.ToDecimal(lblTotAmt.Text);
            _PurchaseOrder.Poh_reprint = false;
            _PurchaseOrder.Poh_tax_chg = false;
            _PurchaseOrder.poh_is_conspo = _isBaseCons;


            List<PurchaseOrderDetail> _POItemListSave = new List<PurchaseOrderDetail>();
            foreach (PurchaseOrderDetail line in _POItemList)
            {
                line.Pod_seq_no = _PurchaseOrder.Poh_seq_no;
                _POItemListSave.Add(line);
            }

            List<PurchaseOrderDelivery> _PODelSave = new List<PurchaseOrderDelivery>();
            foreach (PurchaseOrderDelivery line in _PODelList)
            {
                line.Podi_seq_no = _PurchaseOrder.Poh_seq_no;
                _PODelSave.Add(line);
            }

            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_cd = GlbUserComCode;
            masterAuto.Aut_cate_tp = "COM";
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "PUR";
            masterAuto.Aut_number = 5;//what is Aut_number
            masterAuto.Aut_start_char = "PUR";
            masterAuto.Aut_year = null;

            string QTNum;

            row_aff = (Int32)CHNLSVC.Inventory.SaveNewPO(_PurchaseOrder, _POItemListSave, _PODelSave, masterAuto, out QTNum);

            if (row_aff == 1)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully created.PO No: " + QTNum);
                ClearData();
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

        //        static List<PurchaseOrderDelivery> _tempDelList1 = new List<PurchaseOrderDelivery>();
        protected void OnRemoveFromPOItemGrid(object sender, GridViewDeleteEventArgs e)
        {
            int row_id = e.RowIndex;

            if (_PODelList != null)
                if (_PODelList.Count > 0)
                {

                    int _Line = (int)gvPOItem.DataKeys[row_id][0];
                    string _item = (string)gvPOItem.DataKeys[row_id][1];
                    decimal _qty = (decimal)gvPOItem.DataKeys[row_id][2];
                    decimal _uprice = (decimal)gvPOItem.DataKeys[row_id][3];
                    decimal _disamt = (decimal)gvPOItem.DataKeys[row_id][4];
                    decimal _taxamt = (decimal)gvPOItem.DataKeys[row_id][5];

                    List<PurchaseOrderDelivery> _tempDelList = _PODelList;

                    //                  _tempDelList1 = _PODelList;
                    var _remove = from _list in _tempDelList
                                  where _list.Podi_itm_cd == _item && _list.Podi_line_no == _Line
                                  select _list;

                    foreach (var _single in _remove)
                    {
                        _tempDelList.Remove(_single);
                        goto L1;
                    }
                L1:
                    _PODelList = _tempDelList;
                    BindAddDelItem();
                    CalculateGrandTotal(_qty, _uprice, _disamt, _taxamt, false);
                }

            List<PurchaseOrderDetail> _tempList = _POItemList;
            _tempList.RemoveAt(row_id);
            _POItemList = _tempList;
            BindAddItem();

        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSupplier.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Supplier code is missing.");
                txtSupplier.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtDate.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select order date.");
                txtDate.Focus();
                return;
            }

            if (gvPOItem.Rows.Count == 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter order items.");
                txtItem.Focus();
                return;
            }

            if (_POstatus == "A")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected order already approved.");
                txtOrderNo.Focus();
                return;
            }

            if (_POstatus == "C")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected order is canceled.");
                txtOrderNo.Focus();
                return;
            }

            if (Convert.ToDecimal(lblExRate.Text) <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Exchange rate missing.");
                ddlCur.Focus();
                return;
            }

            if (_IsRecall == false)
            {
                SavePOHeader();
            }
            else if (_IsRecall == true)
            {
                UpdatePurchaseOrder();
            }

            GrndSubTotal = 0;
            GrndDiscount = 0;
            GrndTax = 0;
            _IsRecall = false;
            _lineNo = 0;
            _delLineNo = 0;
            _POstatus = "";
            _POItemList = new List<PurchaseOrderDetail>();

            _PODelList = new List<PurchaseOrderDelivery>();

            List<QoutationDetails> _list = null;
            BindSupplierQuotation(_list);
            //_POItemList = new List<PurchaseOrderDetail>();
            //BindAddItem();
            //_PODelList = new List<PurchaseOrderDelivery>();
            //BindAddDelItem();
            //List<QoutationDetails> _list = null;
            //BindSupplierQuotation(_list);
            _IsCons = false;
            txtSupplier.Focus();
        }



        protected void btnApply_Click(object sender, EventArgs e)
        {
            // var l= from _PODelLists in _PODelList
            string _delLoc = "";
            _delLoc = txtDelLoc.Text.Trim();


            //if (string.IsNullOrEmpty(_delLoc))
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Delivery location is missing.");
            //    txtDelLoc.Focus();
            //    return;
            //}

            if (gvDelDetails.Rows.Count <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot find delivery details.");
                btnApply.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(lblDelItem.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please add selected item first.");
                return;
            }

            if (delQty > 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please add selected item first.");
                return;
            }

            var l = from _PODelLists in _PODelList
                    select new PurchaseOrderDelivery
                    {
                        Actual_qty = _PODelLists.Actual_qty,
                        MasterItem = _PODelLists.MasterItem,
                        Podi_bal_qty = _PODelLists.Podi_bal_qty,
                        Podi_del_line_no = _PODelLists.Podi_del_line_no,
                        Podi_itm_cd = _PODelLists.Podi_itm_cd,
                        Podi_itm_stus = _PODelLists.Podi_itm_stus,
                        Podi_line_no = _PODelLists.Podi_line_no,
                        Podi_loca = _delLoc,
                        Podi_qty = _PODelLists.Podi_qty,
                        Podi_remarks = _PODelLists.Podi_remarks,
                        Podi_seq_no = _PODelLists.Podi_seq_no,
                        PurchaseOrderDetail = _PODelLists.PurchaseOrderDetail

                    };

            List<PurchaseOrderDelivery> p = new List<PurchaseOrderDelivery>();
            foreach (var n in l)
            {
                p.Add(n);
            }

            _PODelList = p;

            BindAddDelItem();
        }

        protected void imgPOSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PurchaseOrder);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPurchaseOrders(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtOrderNo.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }


        private void GetSupplierQuotation()
        {
            String _type = "";
            if (_IsCons == true)
            {
                _type = "C";
            }
            else
            {
                _type = "N";
            }

            List<QoutationDetails> _list = CHNLSVC.Inventory.GetSupplierQuotation(GlbUserComCode, txtSupplier.Text, "S", _type, Convert.ToDateTime(txtDate.Text), Convert.ToDecimal(txtQty.Text), "A", txtItem.Text);
            BindSupplierQuotation(_list);
        }

        protected void BindSupplierQuotation(List<QoutationDetails> _list)
        {
            gvQuotation.DataSource = _list;
            gvQuotation.DataBind();
        }

        protected void gvQuotation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            decimal _price = 0;



            switch (e.CommandName.ToUpper())
            {
                case "PICK":
                    {
                        GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                        _price = Convert.ToDecimal(row.Cells[2].Text);
                        txtUPrice.Text = _price.ToString();
                        txtUPrice.Focus();
                        break;
                    }

            }
        }

        protected void gvDelDetails_RowCommand(object sender, GridViewDeleteEventArgs e)
        {

            if (!string.IsNullOrEmpty(lblDelQty.Text))
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Before edit new item please add selected item.");
                return;
            }

            if (!string.IsNullOrEmpty(lblDelItem.Text))
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Before edit new item please add selected item.");
                return;
            }

            Int32 _delSeq = 0;
            Int32 _delRefLine = 0;
            string _delItem = "";
            string _delStatus = "";
            decimal _delQty = 0;
            string _delLoc = "";
            delQty = 0;
            int row_id = e.RowIndex;


          


            _delSeq = (Int32)gvDelDetails.DataKeys[row_id][1];
            _delRefLine = (Int32)gvDelDetails.DataKeys[row_id][2];
            _delItem = (string)gvDelDetails.DataKeys[row_id][3];
            _delStatus = (string)gvDelDetails.DataKeys[row_id][4];
            _delQty = (decimal)gvDelDetails.DataKeys[row_id][5];
            _delLoc = (string)gvDelDetails.DataKeys[row_id][6];

            lblSeq.Text = _delSeq.ToString();
            lbldelLine.Text = _delRefLine.ToString();
            lblDelItem.Text = _delItem.ToString();
            lblDelStatus.Text = _delStatus.ToString();
            lblDelQty.Text = _delQty.ToString();
            txtEditDelQty.Text = _delQty.ToString();
            txtDelLoc.Text = _delLoc.ToString();
            delQty = _delQty;
            List<PurchaseOrderDelivery> _tempdel = _PODelList;

            _tempdel.RemoveAt(row_id);
            _PODelList = _tempdel;
            BindAddDelItem();


        }

        protected void gvPOItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        private decimal TaxCalculation(string _com, string _item, string _status, decimal _UnitPrice, decimal _TaxVal)
        {

            List<MasterItemTax> _taxs = new List<MasterItemTax>();
            _taxs = CHNLSVC.Inventory.GetItemTax(_com, _item, _status, string.Empty, string.Empty);
            var _Tax = from _itm in _taxs
                       select _itm;
            foreach (MasterItemTax _one in _Tax)
            {
                _TaxVal = _UnitPrice * _one.Mict_tax_rate / 100;
            }


            return _TaxVal;
        }

        protected void CheckTax(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtAmount.Text))
            {
                if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(txtUPrice.Text))
                {
                    txtAmount.Text = Convert.ToString(Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUPrice.Text));
                }
                else
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Unit price or Qty missing.");
                    txtQty.Focus();
                    return;
                }
            }

            if (string.IsNullOrEmpty(txtDisRate.Text))
            {
                txtDisRate.Text = "0";
                txtDisAmt.Text = "0";
            }


            if (string.IsNullOrEmpty(txtTax.Text))
            {
                txtTax.Text = "0";
            }
            txtTotal.Text = Convert.ToString(Convert.ToDecimal(txtAmount.Text) + Convert.ToDecimal(txtTax.Text) - Convert.ToDecimal(txtDisAmt.Text));
            btnAdd.Focus();
        }

        protected void CheckDisAmt(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDisAmt.Text))
            {
                txtDisRate.Text = "0";
                txtDisAmt.Text = "0";
            }

            else
            {

                txtDisRate.Text = Convert.ToString(Convert.ToDecimal(txtDisAmt.Text) / Convert.ToDecimal(txtAmount.Text) * 100);
                txtTax.Text = Convert.ToString(TaxCalculation(GlbUserComCode, txtItem.Text.Trim(), txtItmStatus.Text.Trim(), Convert.ToDecimal(txtAmount.Text), 0));
            }
            txtTax.Focus();
        }

        protected void CheckDisRate(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDisRate.Text))
            {
                txtDisRate.Text = "0";
                txtDisAmt.Text = "0";
            }
            else
            {
                txtDisAmt.Text = Convert.ToString(Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(txtDisRate.Text) / 100);
            }

            txtDisAmt.Focus();
        }

        protected void CheckValidItem(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtItem.Text))
            {
                if (!CHNLSVC.Inventory.IsValidCompanyItem(GlbUserComCode, txtItem.Text, 1))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Item Not allow to company or invalid item.");
                    txtItem.Text = "";
                    txtItem.Focus();
                    return;
                }
            }

            if (txtItmStatus.Enabled == false)
            {
                txtQty.Focus();
            }
            else
            {
                txtItmStatus.Focus();
            }
        }

        protected void CheckSupplier(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSupplier.Text))
            {
                if (!CHNLSVC.Inventory.IsValidSupplier(GlbUserComCode, txtSupplier.Text, 1, "S"))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid supplier.");
                    txtSupplier.Text = "";
                    txtSupplier.Focus();
                    return;
                }
            }
            txtSupRef.Focus();
        }

        protected void Checkstatus(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItmStatus.Text))
            {
                if (!CHNLSVC.Inventory.IsValidItemStatus(txtItmStatus.Text.Trim()))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid item status.");
                    txtItmStatus.Text = "";
                    txtItmStatus.Focus();
                    return;
                }
            }
            txtQty.Focus();
        }

        protected void CheckValidDelLoc(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDelLoc.Text))
            {
                DataTable d = CHNLSVC.Security.GetUserLocTable(GlbUserName, GlbUserComCode, txtDelLoc.Text);

                if (d.Rows.Count == 0)
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid location.");
                    txtDelLoc.Text = "";
                    txtDelLoc.Focus();
                    return;
                }
            }
        }

        protected void CheckUPrice(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(txtUPrice.Text))
            {
                txtAmount.Text = Convert.ToString(Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUPrice.Text));
            }
            else
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Unit price or Qty missing.");
                txtQty.Focus();
                return;
            }
            txtDisRate.Focus();

        }

        protected void CheckQty(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtQty.Text))
            {
                if (string.IsNullOrEmpty(txtSupplier.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Supplier is missing.");
                    txtSupplier.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Item is missing.");
                    txtItem.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDate.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Date is missing.");
                    txtDate.Focus();
                    return;
                }

                GetSupplierQuotation();
            }
            else
            {
                return;
            }

            txtUPrice.Focus();
        }

        protected void CheckPO(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOrderNo.Text))
            {
                LoadSaveDocument();
            }
        }

        private void BindSavePOItems(Int32 _seqNo)
        {
            PurchaseOrderDetail _paramPOItems = new PurchaseOrderDetail();

            _paramPOItems.Pod_seq_no = _seqNo;

            List<PurchaseOrderDetail> _list = CHNLSVC.Inventory.GetPOItems(_paramPOItems);
            _POItemList = new List<PurchaseOrderDetail>();
            _POItemList = _list;
            _lineNo = _POItemList.Count;
            gvPOItem.DataSource = _POItemList;
            gvPOItem.DataBind();

        }

        private void bindSavePODelItems(Int32 _seqNo)
        {
            PurchaseOrderDelivery _paramPOdelItems = new PurchaseOrderDelivery();

            _paramPOdelItems.Podi_seq_no = _seqNo;

            List<PurchaseOrderDelivery> _list = CHNLSVC.Inventory.GetPODelItems(_paramPOdelItems);
            _PODelList = new List<PurchaseOrderDelivery>();
            _PODelList = _list;
            _delLineNo = _PODelList.Count;
            gvDelDetails.DataSource = _PODelList;
            gvDelDetails.DataBind();
        }

        private void LoadSaveDocument()
        {

            _IsRecall = false;
            _POstatus = "";


            PurchaseOrder _POHeader = null;
            _POHeader = CHNLSVC.Inventory.GetPOHeader(GlbUserComCode, txtOrderNo.Text.Trim(), "L");
            if (_POHeader != null)
            {
                txtSupplier.Text = _POHeader.Poh_supp;
                txtSupRef.Text = _POHeader.Poh_ref;
                txtRemarks.Text = _POHeader.Poh_remarks;
                ddlCur.Text = _POHeader.Poh_cur_cd;
                lblExRate.Text = _POHeader.Poh_ex_rt.ToString();
                txtDate.Text = Convert.ToDateTime(_POHeader.Poh_dt).ToShortDateString();
                lblSubAmt.Text = Convert.ToDecimal(_POHeader.Poh_sub_tot).ToString("0,0.00", CultureInfo.InvariantCulture);
                lblDisAmt.Text = Convert.ToDecimal(_POHeader.Poh_dis_amt).ToString("0,0.00", CultureInfo.InvariantCulture);
                lblTaxAmt.Text = Convert.ToDecimal(_POHeader.Poh_tax_tot).ToString("0,0.00", CultureInfo.InvariantCulture);
                lblTotAmt.Text = Convert.ToDecimal(_POHeader.Poh_tot).ToString("0,0.00", CultureInfo.InvariantCulture);
                GrndSubTotal = Convert.ToDecimal(_POHeader.Poh_sub_tot);
                GrndDiscount = Convert.ToDecimal(_POHeader.Poh_dis_amt);
                GrndTax = Convert.ToDecimal(_POHeader.Poh_tax_tot);

                if (_POHeader.poh_is_conspo == 0)
                {
                    cheBaseCon.Checked = false;
                }
                else if (_POHeader.poh_is_conspo == 1)
                {
                    cheBaseCon.Checked = true;
                }
                else
                {
                    cheBaseCon.Checked = false;
                }

                _POstatus = _POHeader.Poh_stus;
                _POSeqNo = _POHeader.Poh_seq_no;
                if (_POHeader.Poh_sub_tp == "C")
                {
                    _IsCons = true;
                    chkIsCons.Checked = true;
                    txtItmStatus.Text = "CONS";
                    txtItmStatus.Enabled = false;
                    imgStatusSearch.Enabled = false;
                    cheBaseCon.Enabled = true;
                }
                else
                {
                    _IsCons = false;
                    chkIsCons.Checked = false;
                    txtItmStatus.Text = "";
                    txtItmStatus.Enabled = true;
                    imgStatusSearch.Enabled = true;
                    cheBaseCon.Enabled = false;
                }

            }

            BindSavePOItems(_POHeader.Poh_seq_no);
            bindSavePODelItems(_POHeader.Poh_seq_no);
            _IsRecall = true;
            chkIsCons.Enabled = false;
            btnApproved.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void CalculateGrandTotal(decimal _qty, decimal _uprice, decimal _discount, decimal _tax, bool _isAddition)
        {
            if (_isAddition)//++
            {
                GrndSubTotal = GrndSubTotal + Convert.ToDecimal(_qty) * Convert.ToDecimal(_uprice);
                GrndDiscount = GrndDiscount + Convert.ToDecimal(_discount);
                GrndTax = GrndTax + Convert.ToDecimal(_tax);

                lblSubAmt.Text = GrndSubTotal.ToString("0,0.00", CultureInfo.InvariantCulture);
                lblDisAmt.Text = GrndDiscount.ToString("0,0.00", CultureInfo.InvariantCulture);
                lblTaxAmt.Text = GrndTax.ToString("0,0.00", CultureInfo.InvariantCulture);

                //lblGrndAfterDiscount.Text = (GrndSubTotal - GrndDiscount).ToString("0,0.00", CultureInfo.InvariantCulture);
                lblTotAmt.Text = (GrndSubTotal - GrndDiscount + GrndTax).ToString("0,0.00", CultureInfo.InvariantCulture);
            }
            else//--
            {
                GrndSubTotal = GrndSubTotal - Convert.ToDecimal(_qty) * Convert.ToDecimal(_uprice);
                GrndDiscount = GrndDiscount - Convert.ToDecimal(_discount);
                GrndTax = GrndTax - Convert.ToDecimal(_tax);

                lblSubAmt.Text = GrndSubTotal.ToString("0,0.00", CultureInfo.InvariantCulture);
                lblDisAmt.Text = GrndDiscount.ToString("0,0.00", CultureInfo.InvariantCulture);
                lblTaxAmt.Text = GrndTax.ToString("0,0.00", CultureInfo.InvariantCulture);

                //lblGrndAfterDiscount.Text = (GrndSubTotal + GrndDiscount).ToString("0,0.00", CultureInfo.InvariantCulture);
                lblTotAmt.Text = (GrndSubTotal - GrndDiscount + GrndTax).ToString("0,0.00", CultureInfo.InvariantCulture);
            }



        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOrderNo.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select purchase order #");
                txtOrderNo.Focus();
                return;
            }

            if (_IsRecall == false)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please recall saved order.");
                txtOrderNo.Focus();
                return;
            }

            if (string.IsNullOrEmpty(_POstatus))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot retrive current po status.Please re-try.");
                txtOrderNo.Focus();
                return;
            }

            if (_POstatus == "A")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected order already approved.");
                txtOrderNo.Focus();
                return;
            }

            if (_POstatus == "C")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected order is canceled.");
                txtOrderNo.Focus();
                return;
            }

            UpdatePOStatus("A", true);
            GrndSubTotal = 0;
            GrndDiscount = 0;
            GrndTax = 0;
            _IsRecall = false;
            _POSeqNo = 0;
            _lineNo = 0;
            _POstatus = "";
            _delLineNo = 0;
            _POItemList = new List<PurchaseOrderDetail>();
            _PODelList = new List<PurchaseOrderDelivery>();
            List<QoutationDetails> _list = null;
            BindSupplierQuotation(_list);
            // _POItemList = new List<PurchaseOrderDetail>();
            // BindAddItem();
            // _PODelList = new List<PurchaseOrderDelivery>();
            // BindAddDelItem();
            // List<QoutationDetails> _list = null;
            // BindSupplierQuotation(_list);
            _IsCons = false;

            txtSupplier.Focus();
        }

        protected void chkIsCons_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsCons.Checked == true)
            {
                _IsCons = true;
                txtItmStatus.Text = "CONS";
                txtItmStatus.Enabled = false;
                imgStatusSearch.Enabled = false;
                cheBaseCon.Enabled = true;
            }
            else
            {
                _IsCons = false;
                txtItmStatus.Text = "";
                txtItmStatus.Enabled = true;
                imgStatusSearch.Enabled = true;
                cheBaseCon.Enabled = false;
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOrderNo.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select purchase order #");
                txtOrderNo.Focus();
                return;
            }

            if (_IsRecall == false)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please recall saved order.");
                txtOrderNo.Focus();
                return;
            }

            if (string.IsNullOrEmpty(_POstatus))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot retrive current po status.Please re-try.");
                txtOrderNo.Focus();
                return;
            }

            if (_POstatus == "A")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected order already approved.");
                txtOrderNo.Focus();
                return;
            }

            if (_POstatus == "C")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected order is already canceled.");
                txtOrderNo.Focus();
                return;
            }

            UpdatePOStatus("C", false);
            GrndSubTotal = 0;
            GrndDiscount = 0;
            GrndTax = 0;
            _IsRecall = false;
            _POSeqNo = 0;
            _lineNo = 0;
            _POstatus = "";
            _delLineNo = 0;
            // _POItemList = new List<PurchaseOrderDetail>();
            // BindAddItem();
            // _PODelList = new List<PurchaseOrderDelivery>();
            // BindAddDelItem();
            // List<QoutationDetails> _list = null;
            // BindSupplierQuotation(_list);
            _IsCons = false;

            txtSupplier.Focus();
        }

        protected void BindCurrency(DropDownList _ddl)
        {
            _ddl.Items.Clear();
            //List<PaymentTypeRef> _paymentTypeRef = CHNLSVC.Sales.GetAllPaymentType(string.Empty);
            List<MasterCurrency> _Currency = CHNLSVC.General.GetAllCurrency(string.Empty);
            if (_Currency != null)
            {
                _ddl.DataSource = _Currency.OrderBy(items => items.Mcr_cd);
                _ddl.DataTextField = "Mcr_cd";
                _ddl.DataValueField = "Mcr_cd";
                _ddl.DataBind();
            }

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Int32 _seq = 0;
            decimal _balQty = 0;

            //if (gvDelDetails.Rows.Count > 0)
            //{
            //    _seq = gvDelDetails.Rows.Count;
            //}
            //else
            //{
            //    _seq = 0;
            //}

            if (delQty < Convert.ToDecimal(txtEditDelQty.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Total order qty is exceeded.");
                txtEditDelQty.Text = delQty.ToString();
                txtEditDelQty.Focus();
                return;
            }

            if (Convert.ToDecimal(lblDelQty.Text) < Convert.ToDecimal(txtEditDelQty.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Qty exceeded.");
                return;
            }


            if (string.IsNullOrEmpty(lblDelItem.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select item to add delivery shcedule.");
                return;
            }

            if (string.IsNullOrEmpty(txtEditDelQty.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter qty to add delivery shcedule.");
                return;
            }

            if (Convert.ToDecimal(txtEditDelQty.Text) <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter valid qty.");
                return;
            }

            if (string.IsNullOrEmpty(txtDelLoc.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter delivery location.");
                return;
            }

            var currrange = (from cur in _PODelList
                             where cur.Podi_itm_cd == lblDelItem.Text.Trim() && cur.Podi_itm_stus == lblDelStatus.Text.Trim() && cur.Podi_loca == txtDelLoc.Text.Trim()
                             select cur).ToList();

            if (currrange.Count > 0)// ||currrange !=null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected item already added to delivery shedule for same loation same status.");
                return;
            }

            List<PurchaseOrderDelivery> _tempPoDel = new List<PurchaseOrderDelivery>();
            List<PurchaseOrderDelivery> _tempPoDel1 = new List<PurchaseOrderDelivery>();

            _PODelList.Add(AssignEditDatatoObject());

            _tempPoDel = _PODelList;

            foreach (PurchaseOrderDelivery _tempdel in _tempPoDel)
            {
                _seq = _seq + 1;
                _tempdel.Podi_del_line_no = _seq;
                _tempPoDel1.Add(_tempdel);
            }
            _PODelList.Clear();
            _PODelList = _tempPoDel1;

            BindAddDelItem();

            delQty = delQty - Convert.ToDecimal(txtEditDelQty.Text);

            if (delQty == 0)
            {
                lblSeq.Text = "";
                lbldelLine.Text = "";
                lblDelItem.Text = "";
                lblDelStatus.Text = "";
                lblDelQty.Text = "";
                txtEditDelQty.Text = "";
                txtDelLoc.Text = "";
            }
            else
            {
                txtEditDelQty.Text = delQty.ToString();
                txtDelLoc.Text = "";
                txtDelLoc.Focus();
            }

        }

        protected void GetExRate(object sender, EventArgs e)
        {

            LoadExRate();
        }

        protected void LoadExRate()
        {
            decimal _curExRate = 0;
            MasterExchangeRate _ExRate = CHNLSVC.Sales.GetLaterstExchangeRate(GlbUserComCode, ddlCur.SelectedValue, Convert.ToDateTime(txtDate.Text).Date);

            if (_ExRate != null)
            {
                _curExRate = _ExRate.Mer_bnksel_rt;
                lblExRate.Text = _curExRate.ToString("0.00");
                btnSave.Enabled = true;
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Exchange Rate not define.");
                lblExRate.Text = "";
                btnSave.Enabled = false;
                btnApproved.Enabled = false;
                return;
            }
        }

        protected void ddlCur_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadExRate();
        }
    }
}



