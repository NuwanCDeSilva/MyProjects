using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Data;

namespace FF.WebERPClient.Inventory_Module
{
    public partial class DeliveryOrder : BasePage
    {
        #region Variable Scope
        public List<ReptPickSerials> serial_list
        {
            get { return (List<ReptPickSerials>)ViewState["ReptPickSerials"]; }
            set { ViewState["ReptPickSerials"] = value; }
        }

        public InvoiceHeader invoice_hdr
        {
            get { return (InvoiceHeader)ViewState["InvoiceHeader"]; }
            set { ViewState["InvoiceHeader"] = value; }
        }

        public List<InvoiceItem> invoice_items
        {
            get { return (List<InvoiceItem>)ViewState["InvoiceItem"]; }
            set { ViewState["InvoiceItem"] = value; }
        }

        public List<InvoiceItem> invoice_items_bind
        {
            get { return (List<InvoiceItem>)ViewState["invoice_items_bind"]; }
            set { ViewState["invoice_items_bind"] = value; }
        }

        public bool IsGrn
        {
            get { return (bool)ViewState["IsGrn"]; }
            set { ViewState["IsGrn"] = value; }

        }

        //ADDED BY SACTITH 2012/09/19
        //GET TH INVOICE NO
        //START
        public string Invoice_no
        {
            get { return (string)ViewState["Invoice_no"]; }
            set { ViewState["Invoice_no"] = value; }

        }

        public bool CanSave
        {
            get { return (bool)ViewState["CanSave"]; }
            set { ViewState["CanSave"] = value; }

        }


        //END


        Int32 SelectedRowIndex = 0;
        string _outbackdatestring;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //List<InvoiceHeader> GetPendingInvoiceDetails(string prof_center, string company, DateTime from_dt, DateTime to_date);
            string prof_cnter = GlbUserDefProf;
            //string company = GlbUserComCode;
            //DateTime fromDT = DateTime.MinValue; //Convert.ToDateTime(txtDateFrom.Text);
            //DateTime toDT = DateTime.Now;// Convert.ToDateTime(txtDateTo.Text);

            //List<InvoiceHeader> pending_inv_list = CHNLSVC.Sales.GetPendingInvoiceDetails(GlbUserDefProf, GlbUserComCode, fromDT, toDT);
            //gvInvoiceItem.DataSource = pending_inv_list;
            //DataTable dt = CHNLSVC.Sales.GetPendingInvoiceDetails(GlbUserDefProf, GlbUserComCode, fromDT, toDT);
            //if (dt.Rows.Count > 0)
            //{
            //    gvInvoiceItem.DataSource = dt;
            //    gvInvoiceItem.DataBind();

            //}
            //else
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No pending invoices found!");
            //    return;
            //}


            if (!IsPostBack)
            {
                Invoice_no = "";
                CanSave = true;
                IsGrn = false;
                //IF NOT POSBACK
                DateTime fromDT = DateTime.MinValue; //Convert.ToDateTime(txtDateFrom.Text);
                DateTime toDT = DateTime.Now;// Convert.ToDateTime(txtDateTo.Text);

                //Change By Prabhath on 19062012
                if (CallDObyInvoice == null)
                {
                    DataTable dt = CHNLSVC.Sales.GetPendingInvoiceDetails(GlbUserDefProf, GlbUserComCode, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), GlbUserDefLoca);
                    if (dt.Rows.Count > 0)
                    {
                        gvInvoiceItem.DataSource = dt;
                        gvInvoiceItem.DataBind();

                    }
                    else
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No pending invoices found!");
                        return;
                    }
                }

                List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                emptyGridList.Add(new ReptPickSerials());
                GridViewDo_serials.DataSource = emptyGridList;
                GridViewDo_serials.DataBind();


                InvoiceItem it = new InvoiceItem();
                MasterItem mi = new MasterItem(); //CHNLSVC.Inventory.GetItem(GlbUserComCode, it.Sad_alt_itm_cd);
                it.Sad_itm_cd = string.Empty;
                it.Sad_alt_itm_desc = string.Empty;// mi.Mi_shortdesc;
                it.Mi_model = string.Empty;// mi.Mi_model;
                it.Sad_qty = 0;
                it.Sad_tot_amt = 0;

                invoice_items = new List<InvoiceItem>();
                invoice_items_bind = new List<InvoiceItem>();


                //invoice_items.Clear();
                //invoice_items = null;
                //invoice_items_bind.Clear();
                //invoice_items_bind = null;
                invoice_items_bind.Add(it);

                GridViewDo_itm.DataSource = invoice_items_bind;
                GridViewDo_itm.DataBind();

                //Check back date
                IsAllowBackDateForModule(GlbUserComCode, GlbUserDefLoca, string.Empty, Page, string.Empty, imgDODate, lblDispalyInfor);

                //Added By Prabhath on 19062012
                if (CallDObyInvoice != null)
                {
                    txtManualRefNo.Text = CallDobyInvoiceManual;
                    this.LoadInvoiceItems(CallDObyInvoice, true);
                    this.LoadInvoiceHeader(CallDObyInvoice);

                    pnlScanItem.Visible = false;
                    CPEScanItem.Collapsed = false;
                    this.CPEScanItem.ClientState = "false";

                    pnlInvItem.Visible = false;
                    CPEInvoiceItem.Collapsed = false;
                    CPEInvoiceItem.ClientState = "false";

                    CPEGeneral.Collapsed = true;
                    CPEGeneral.ClientState = "true";

                }

                //txtDODate.Enabled = false;
                txtDODate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            }
        }

        protected void imgBtnPriceBook_Click(object sender, ImageClickEventArgs e)
        {
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //List<InvoiceHeader> GetPendingInvoiceDetails(string prof_center, string company, DateTime from_dt, DateTime to_date);
            string prof_cnter = GlbUserDefProf;
            string company = GlbUserComCode;

            try
            {
                //fromDT = Convert.ToDateTime(txtDateFrom.Text.Trim());
                //toDT = Convert.ToDateTime(txtDateTo.Text.Trim());
            }
            catch (Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter Search Dates correctly.");
                return;
            }

            try
            {

                DataTable dt = CHNLSVC.Sales.GetPendingInvoiceDetails(GlbUserDefProf, GlbUserComCode, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), GlbUserDefLoca);
                if (dt.Rows.Count > 0)
                {
                    gvInvoiceItem.DataSource = dt;
                    gvInvoiceItem.DataBind();

                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No pending invoices found!");
                    gvInvoiceItem.DataSource = dt;
                    gvInvoiceItem.DataBind();
                    return;
                }
            }
            catch (Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Data receive faliled!");
            }


            //DateTime fromDT = Convert.ToDateTime(txtDateFrom.Text);
            //DateTime toDT = Convert.ToDateTime(txtDateTo.Text);

            //  List<InvoiceHeader> pending_inv_list = CHNLSVC.Sales.GetPendingInvoiceDetails(GlbUserDefProf, GlbUserComCode, fromDT, toDT);
            //  gvInvoiceItem.DataSource = pending_inv_list;


        }

        //protected void gvPendingInvoices_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    switch (e.CommandName.ToUpper())
        //    {
        //        case "SELECTINVOICE":
        //            {
        //                LinkButton lnkbtnReqNo = (LinkButton)e.CommandSource;
        //                string _selectedReqNo = lnkbtnReqNo.CommandArgument;
        //                //SetSelectedConsignRequestData(_selectedReqNo);
        //                //pnlReceiptItemDetails.GroupingText = "Receipt Item Details : Request No.  " + lblRequestNo.Text;
        //                this.LoadInvoiceItems(_selectedReqNo, true);
        //                this.LoadInvoiceHeader(_selectedReqNo);
        //                //SelectedRowIndex = Convert.ToInt32(e.CommandArgument);



        //                break;
        //            }
        //    }
        //}

        #region Load Invoice Details

        private void LoadInvoiceHeader(string _invNo)
        {
            if (_invNo != null)
            {
                invoice_hdr = CHNLSVC.Sales.GetPendingInvoiceHeader(GlbUserComCode, GlbUserDefProf, "INV", lblInvoiceNo.Text, "A");
                if (invoice_hdr != null)
                {
                    IsGrn = invoice_hdr.Sah_is_grn;
                    lblInvoiceNo.Text = invoice_hdr.Sah_inv_no;
                    txtCustCode.Text = invoice_hdr.Sah_cus_cd;
                    txtCustName.Text = invoice_hdr.Sah_cus_name;
                    txtCustAdd1.Text = invoice_hdr.Sah_cus_add1;
                    txtCustAdd2.Text = invoice_hdr.Sah_cus_add2;
                    lblInvoiceDate.Text = invoice_hdr.Sah_dt.Date.ToString("dd/MMM/yyyy");
                }
            }
        }

        //static int _count = 0;
        //protected void OnRequestBind(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        e.Row.Attributes.Add("ID", "tab" + _count.ToString());
        //        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(
        //                    gvInvoiceItem,
        //                    String.Concat("Select$", e.Row.RowIndex),
        //                    true);

        //        _count += 1;
        //    }
        //}

        private void LoadInvoiceItems(string _invNo, bool _isCommand)
        {
            if (_isCommand == false)
            {
                //lblInvoiceNo.Text = gvInvoiceItem.SelectedRow.Cells[1].Text.ToString();
                lblInvoiceNo.Text = _invNo;
            }
            else
            {
                lblInvoiceNo.Text = _invNo;
            }

            invoice_items = new List<InvoiceItem>();
            invoice_items_bind = new List<InvoiceItem>();

            //invoice_items.Clear();
            //invoice_items = null;
            //invoice_items_bind.Clear();
            //invoice_items_bind = null;

            //Get Invoice Items Details
            invoice_items = CHNLSVC.Sales.GetAllSaleDocumentItemList(GlbUserComCode, GlbUserDefProf, "INV", lblInvoiceNo.Text, "A");
            if (invoice_items != null)
            {
                if (invoice_items.Count > 0)
                {
                    //Check Vehicle Reg.
                    Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("RECEIPT", GlbUserComCode, lblInvoiceNo.Text, 0);
                    if (user_seq_num != -1)
                    {
                        //GridViewDo_itm.DataSource = invoice_items;
                        //GridViewDo_itm.DataBind();
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invoice still not approved for release");
                        return;
                    }


                    //Check DO
                    user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("DO", GlbUserComCode, lblInvoiceNo.Text, 0);
                    if (user_seq_num == -1)
                    {
                        //Generate new user seq no and add new row to pick_hdr
                        user_seq_num = GenerateNewUserSeqNo();
                    }

                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    _serList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, user_seq_num, "DO");

                    if (_serList != null)
                    {
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        foreach (var itm in _scanItems)
                        {
                            foreach (InvoiceItem _invItem in invoice_items)
                                if (itm.Peo.Tus_itm_cd == _invItem.Sad_itm_cd && itm.Peo.Tus_base_itm_line == _invItem.Sad_itm_line)
                                {
                                    //it.Sad_do_qty = q.theCount;
                                    //it.Sad_tot_amt = q.theCount;//assigned for the display purpose only- shown in the "DO Qty" column in grid.
                                    _invItem.Sad_srn_qty = itm.theCount; // Current scan qty
                                }
                        }

                        GridViewDo_serials.DataSource = _serList;
                        GridViewDo_serials.DataBind();
                    }
                    else
                    {
                        List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                        emptyGridList.Add(new ReptPickSerials());
                        GridViewDo_serials.DataSource = emptyGridList;
                        GridViewDo_serials.DataBind();
                    }

                    GridViewDo_itm.DataSource = invoice_items;
                    GridViewDo_itm.DataBind();
                }
            }
            else
            {

                List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                emptyGridList.Add(new ReptPickSerials());
                GridViewDo_serials.DataSource = emptyGridList;
                GridViewDo_serials.DataBind();


                InvoiceItem it = new InvoiceItem();
                MasterItem mi = new MasterItem(); //CHNLSVC.Inventory.GetItem(GlbUserComCode, it.Sad_alt_itm_cd);
                it.Sad_alt_itm_desc = "";// mi.Mi_shortdesc;
                it.Mi_model = "";// mi.Mi_model;
                it.Sad_qty = 0;
                it.Sad_tot_amt = 0;

                invoice_items = new List<InvoiceItem>();
                invoice_items_bind = new List<InvoiceItem>();

                //invoice_items.Clear();
                //invoice_items = null;
                //invoice_items_bind.Clear();
                //invoice_items_bind = null;
                invoice_items_bind.Add(it);

                GridViewDo_itm.DataSource = invoice_items_bind;
                GridViewDo_itm.DataBind();
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No pending items found for this invoice!");
                return;
            }

            //get all from sat_itm
        }

        protected void InvoiceItemLoad(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (GridViewDo_itm.DataKeys[0].Value != null)
                {
                    string _item = GridViewDo_itm.DataKeys[0].Value.ToString();
                    TextBox _txt = e.Row.FindControl("txtGridDoQty") as TextBox;
                    bool _isDecimalAllow = CHNLSVC.Inventory.IsUOMDecimalAllow(_item);
                    if (_isDecimalAllow)
                        _txt.Enabled = true;
                    else _txt.Enabled = false;
                }
            }
        }
        #endregion

        #region Generate new user seq no
        private Int32 GenerateNewUserSeqNo()
        {
            Int32 generated_seq = 0;
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "DO", 1, GlbUserComCode);//direction always =1 for this method                    //assign user_seqno
            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = "DO";
            RPH.Tuh_cre_dt = DateTime.Today;//might change //Calendar-SelectedDate;
            RPH.Tuh_ischek_itmstus = true;//might change 
            RPH.Tuh_ischek_reqqty = true;//might change
            RPH.Tuh_ischek_simitm = true;//might change
            RPH.Tuh_session_id = GlbUserSessionID;
            RPH.Tuh_usr_com = GlbUserComCode;//might change 
            RPH.Tuh_usr_id = GlbUserName;
            RPH.Tuh_usrseq_no = generated_seq;

            RPH.Tuh_direct = false; //direction always (-) for change status
            RPH.Tuh_doc_no = lblInvoiceNo.Text;
            //write entry to TEMP_PICK_HDR
            int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
            if (affected_rows == 1)
            {
                return generated_seq;
            }
            else
            {
                return 0;
            }
        }
        #endregion

        //protected void gvInvoiceItem_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    lblInvoiceDate.Text = gvInvoiceItem.SelectedDataKey[0].ToString();
        //    this.LoadInvoiceItems(string.Empty, false);
        //}

        protected void GridViewDo_itm_SelectedIndexChanged(object sender, EventArgs e)//pick button is a select link
        {
            CanSave = true;
            GridViewRow gvr = GridViewDo_itm.SelectedRow;
            //CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
            TextBox ScannedDoQty = (TextBox)gvr.Cells[7].FindControl("txtGridDoQty");

            lblScanQty.Text = ScannedDoQty.Text.ToString();
            lblDeliveredQty.Text = GridViewDo_itm.SelectedRow.Cells[6].Text.ToString();
            lblInvoiceQty.Text = GridViewDo_itm.SelectedRow.Cells[5].Text.ToString();

            string longDiscript = GridViewDo_itm.SelectedRow.Cells[3].Text.ToString();
            lblPopupItemCode.Text = GridViewDo_itm.SelectedRow.Cells[2].Text.ToString();
            //selected_item_line = Convert.ToInt32(GridViewDo_itm.SelectedRow.Cells[1].Text);
            hdnInvoiceLineNo.Value = GridViewDo_itm.SelectedRow.Cells[1].Text.ToString();

            string priceBook = GridViewDo_itm.SelectedRow.Cells[10].Text.ToString();
            string priceLevl = GridViewDo_itm.SelectedRow.Cells[11].Text.ToString();

            //add column Chamal 13-11-2012
            string invcitemstaus = GridViewDo_itm.SelectedRow.Cells[12].Text.ToString();

            divPopupImg.Visible = false;
            lblpopupMsg.Text = string.Empty;

            //check price book
            int pbCount = CHNLSVC.Sales.GetDOPbCount(GlbUserComCode, priceBook, priceLevl);

            //ADDED BY SACHITH 2012/09/19
            //BLOCK ISAPP and SAD_ISCOVERNOTE false Items
            //START
            DataTable dt = CHNLSVC.Sales.GetPendingInvoiceItemsByItemDT(Invoice_no, lblPopupItemCode.Text);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt.Rows[0]["SAD_ISAPP"]) != 1 || Convert.ToInt32(dt.Rows[0]["SAD_ISCOVERNOTE"]) != 1)
                {
                    CanSave = false;
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Not Approved to Relese Item");
                    return;
                }
            }
            //END


            if (lblPopupItemCode.Text != "&nbsp;")
            {
                lblPopupItemCode.Text = lblPopupItemCode.Text;
                lblPopupBinCode.Visible = false;
                txtPopupQty.Visible = true;

                MasterItem msitem = new MasterItem();
                msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, lblPopupItemCode.Text);
                // msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, "LGDVD270");
                //try {


                //ADDED BY SACHITH 2012/09/19
                //BLOCK ISAPP and SAD_ISCOVERNOTE false Items
                //START

                //END


                if (msitem.Mi_is_ser1 == 0)//check whether it is a non-sirialized item // (msitem.Mi_is_ser1 == false)
                {
                    //lblPopQty.Visible = true;
                    //txtPopupQty.Visible = true;
                    serial_list = new List<ReptPickSerials>();
                    //Search_serials_for_itemCD(string company, string location, string itemCode)
                    if (pbCount <= 0)
                    {
                        serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text, priceBook, priceLevl);
                    }
                    else
                        serial_list = CHNLSVC.Sales.GetStatusGodSerial(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text, priceBook, priceLevl, invcitemstaus);
                    //serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca, "LGDVD270");
                    GridPopup.DataSource = serial_list;
                    GridPopup.DataBind();

                    ModalPopupExtItem.Show();
                }
                else if (msitem.Mi_is_ser1 == 1) //serial
                {
                    serial_list = new List<ReptPickSerials>();
                    //Search_serials_for_itemCD(string company, string location, string itemCode)
                    if (pbCount <= 0)
                    {
                        serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text, priceBook, priceLevl);
                    }
                    else
                        serial_list = CHNLSVC.Sales.GetStatusGodSerial(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text, priceBook, priceLevl, invcitemstaus);
                    //serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca, "LGDVD270");
                    GridPopup.DataSource = serial_list;
                    GridPopup.DataBind();

                    ModalPopupExtItem.Show();
                }
                else if (msitem.Mi_is_ser1 == -1)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Non serial, decimal allow");
                    if (pbCount <= 0)
                    {
                        serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text, priceBook, priceLevl);
                    }
                    else
                        serial_list = CHNLSVC.Sales.GetStatusGodSerial(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text, priceBook, priceLevl, invcitemstaus);
                    //serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca, "LGDVD270");
                    GridPopup.DataSource = serial_list;
                    GridPopup.DataBind();

                    ModalPopupExtItem.Show();
                }
                //}
                //catch(Exception ex){

                //}

            }

        }

        protected void btnPopupOk_Click(object sender, EventArgs e)
        {

            Int32 generated_seq = -1;

            // Dictionary<string, string> changed_serials = new Dictionary<string, string>();

            string itemCode = lblPopupItemCode.Text.Trim();

            Int32 userseq_no;
            Int32 num_of_checked_itms = 0;
            foreach (GridViewRow gvr in this.GridPopup.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");

                if (chkSelect.Checked)
                {
                    num_of_checked_itms++;
                }


            }
            Decimal pending_amt = Convert.ToDecimal(lblInvoiceQty.Text.ToString()) - Convert.ToDecimal(lblDeliveredQty.Text.ToString());
            if ((Convert.ToDecimal(lblScanQty.Text.ToString()) + num_of_checked_itms) > pending_amt)
            {
                //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Can't exceed Invoice Qty!");
                divPopupImg.Visible = true;
                lblpopupMsg.Text = "Can't exceed Invoice Qty!";
                ModalPopupExtItem.Show();
                return;
            }

            Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("DO", GlbUserComCode, lblInvoiceNo.Text, 0);
            if (user_seq_num != -1)//check whether Tuh_doc_no exestst in temp_pick_hdr
            {
                //INCR00001
                generated_seq = user_seq_num;
                userseq_no = generated_seq;
            }
            else
            {
                // generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "ADJ", 1, GlbUserComCode);//direction always =1 for this method
                generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "DO", 1, GlbUserComCode);//direction always =1 for this method
                //assign user_seqno
                userseq_no = generated_seq;
                ReptPickHeader RPH = new ReptPickHeader();
                RPH.Tuh_doc_tp = "DO";
                // RPH.Tuh_doc_tp = "DO";
                RPH.Tuh_cre_dt = DateTime.Today;//might change //Calendar-SelectedDate;
                RPH.Tuh_ischek_itmstus = true;//might change 
                RPH.Tuh_ischek_reqqty = true;//might change
                RPH.Tuh_ischek_simitm = true;//might change
                RPH.Tuh_session_id = GlbUserSessionID;
                RPH.Tuh_usr_com = GlbUserComCode;//might change 
                RPH.Tuh_usr_id = GlbUserName;
                RPH.Tuh_usrseq_no = generated_seq;

                RPH.Tuh_direct = false;
                RPH.Tuh_doc_no = lblInvoiceNo.Text;
                //write entry to TEMP_PICK_HDR
                int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);

            }

            MasterItem msitem = new MasterItem();
            msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
            if (msitem.Mi_is_ser1 == 1)
            //change msitem.Mi_is_ser1 == true
            {
                int rowCount = 0;

                foreach (GridViewRow gvr in this.GridPopup.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
                    // string serial_1 = (string)gvr.Cells[1].Text.Trim();
                    Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                    if (chkSelect.Checked)
                    {
                        // Boolean update = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serial_1, -1);
                        //-------------
                        string binCode = gvr.Cells[5].Text;
                        ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, serID);

                        _reptPickSerial_.Tus_cre_by = GlbUserName;
                        //if (ddlScanBatches.SelectedIndex == 0)
                        //{
                        _reptPickSerial_.Tus_usrseq_no = generated_seq;
                        // userseq_no= generated_seq;
                        //}
                        //else
                        //{
                        // _reptPickSerial_nonSer.Tus_usrseq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
                        //  userseq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
                        //}
                        //Update_inrser_INS_AVAILABLE
                        Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serID, -1);
                        //  Boolean update_inr_ser = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, "N/A", -1);
                        _reptPickSerial_.Tus_cre_by = GlbUserName;

                        _reptPickSerial_.Tus_base_doc_no = lblInvoiceNo.Text;
                        _reptPickSerial_.Tus_base_itm_line = Convert.ToInt16(hdnInvoiceLineNo.Value);

                        MasterItem mi = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
                        _reptPickSerial_.Tus_itm_desc = mi.Mi_shortdesc;
                        _reptPickSerial_.Tus_itm_model = mi.Mi_model;
                        //enter row into TEMP_PICK_SER
                        Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);

                        rowCount++;
                        //isManualscan = true;

                    }

                }
                if (rowCount > 0)
                {
                    // Boolean update_sat_do_qty = CHNLSVC.Inventory.Update_sat_itm_DO_qty(selected_invoice_no, selected_item_line, rowCount);//pass the qty to update
                }

            }

            else if (msitem.Mi_is_ser1 == 0)
            {
                int rowCount = 0;

                List<ReptPickSerials> actual_non_ser_List = new List<ReptPickSerials>();
                //string binCD = ""; //this is declared for the dummy bincode.

                foreach (GridViewRow gvr in this.GridPopup.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
                    // string serial_1 = (string)gvr.Cells[1].Text.Trim();
                    Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                    if (chkSelect.Checked)
                    {
                        // Boolean update = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serial_1, -1);
                        //-------------
                        // Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, serial_1);
                        string binCode = gvr.Cells[5].Text;

                        ReptPickSerials _reptPickSerial_nonSer = CHNLSVC.Inventory.Get_all_details_on_serialID(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, serID);


                        //  _reptPickSerial_nonSer.Tus_new_status = "DO";//lblItemStatus.Text.Trim();//1 status always= CONSG
                        _reptPickSerial_nonSer.Tus_cre_by = GlbUserName;
                        //if (ddlScanBatches.SelectedIndex == 0)
                        //{
                        _reptPickSerial_nonSer.Tus_usrseq_no = generated_seq;
                        // userseq_no= generated_seq;
                        //}
                        //else
                        //{
                        //    _reptPickSerial_nonSer.Tus_usrseq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
                        //    //  userseq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
                        //}
                        //Update_inrser_INS_AVAILABLE
                        Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serID, -1);
                        //  Boolean update_inr_ser = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, "N/A", -1);
                        _reptPickSerial_nonSer.Tus_cre_by = GlbUserName;

                        _reptPickSerial_nonSer.Tus_base_doc_no = lblInvoiceNo.Text;
                        _reptPickSerial_nonSer.Tus_base_itm_line = Convert.ToInt16(hdnInvoiceLineNo.Value);

                        MasterItem mi = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
                        _reptPickSerial_nonSer.Tus_itm_desc = mi.Mi_shortdesc;
                        _reptPickSerial_nonSer.Tus_itm_model = mi.Mi_model;
                        //enter row into TEMP_PICK_SER
                        Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_nonSer, null);

                        rowCount++;
                        //isManualscan = true;
                        actual_non_ser_List.Add(_reptPickSerial_nonSer);

                        //  binCD = _reptPickSerial_nonSer.Tus_bin;//the last items's bin will be assigned at last
                    }

                }
                if (rowCount > 0)
                {
                    //Remove Duplicates Prabhath on 25/07/2012


                    ////create a dummy to represent the sum

                    ////**************Add**********
                    //List<ReptPickSerials> SummaryList = new List<ReptPickSerials>();
                    //foreach (ReptPickSerials rps in actual_non_ser_List)
                    //{
                    //    if (rps.Tus_ser_1 != GlbDummySerial1)
                    //    {
                    //        SummaryList.Add(rps);
                    //    }
                    //}

                    //// var items = finalserList.GroupBy(x => x.Tus_itm_cd, x.Tus_base_itm_line).ToList();

                    //var sums = SummaryList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_bin }).Select(group => new { Peo = group.Key, theCount = group.Count() });

                    ////**************Add*********

                    //foreach (var s in sums)
                    //{

                    //    ReptPickSerials reptPickSerial_dummy = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, s.Peo.Tus_bin, itemCode, "N/A");
                    //    // ReptPickSerials reptPickSerial_dummy = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, "DOdummyBin", itemCode, "N/A");
                    //    reptPickSerial_dummy.Tus_usrseq_no = userseq_no;
                    //    reptPickSerial_dummy.Tus_ser_id = 0;
                    //    // reptPickSerial_dummy.Tus_ser_1 = "_";
                    //    reptPickSerial_dummy.Tus_ser_1 = GlbDummySerial1;
                    //    //reptPickSerial_dummy.Tus_new_status = ddlItemStatus.SelectedValue;
                    //    reptPickSerial_dummy.Tus_cre_by = GlbUserName;
                    //    MasterItem mi = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
                    //    reptPickSerial_dummy.Tus_itm_desc = mi.Mi_longdesc;
                    //    reptPickSerial_dummy.Tus_itm_model = mi.Mi_model;
                    //    reptPickSerial_dummy.Tus_qty = rowCount;

                    //    reptPickSerial_dummy.Tus_com = GlbUserComCode;
                    //    reptPickSerial_dummy.Tus_loc = GlbUserDefLoca;
                    //    reptPickSerial_dummy.Tus_bin = s.Peo.Tus_bin;
                    //    reptPickSerial_dummy.Tus_itm_cd = itemCode;
                    //    reptPickSerial_dummy.Tus_base_doc_no = lblInvoiceNo.Text;//-->if not needed, it is free to ignore this

                    //    // ReptPickSerials check_reptPickSerial_dummy = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, "_");
                    //    List<ReptPickSerials> temp_pick_ser = new List<ReptPickSerials>();
                    //    temp_pick_ser = CHNLSVC.Inventory.Get_TEMP_PICK_SER(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, 0, GlbDummySerial1, itemCode, s.Peo.Tus_bin);
                    //    if (temp_pick_ser != null)
                    //    {
                    //        foreach (ReptPickSerials check_reptPickSerial_dummy in temp_pick_ser)//this temp_pick_ser list contains only 1 row
                    //        {
                    //            // Update_temp_pick_serdummy
                    //            Decimal newQTY = check_reptPickSerial_dummy.Tus_qty + rowCount;
                    //            Boolean affectedrows = CHNLSVC.Inventory.Update_temp_pick_serdummy(GlbUserName, GlbUserComCode, GlbUserDefLoca, userseq_no, GlbDummySerial1, itemCode, s.Peo.Tus_bin, newQTY);
                    //            // Boolean affected = CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, userseq_no, 0);//delete existing dummy
                    //            //delete existing dummy (delete only the dummy, not all non serials along with it)
                    //            // Boolean affectedrows = CHNLSVC.Inventory.Del_temp_pick_serdummy(GlbUserComCode, GlbUserDefLoca, userseq_no, 0, itemCode, binCode);//neeed to be modified(the sp. Add user,ser_1)

                    //        }

                    //    }
                    //    else
                    //    {
                    //        Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(reptPickSerial_dummy, null);
                    //    }
                    //}
                }

            }
            //------------------------------------------------------------------------------------------------------------------------------
            else if (msitem.Mi_is_ser1 == -1)
            {
                int rowCount = 0;

                List<ReptPickSerials> actual_non_ser_List = new List<ReptPickSerials>();

                foreach (GridViewRow gvr in this.GridPopup.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
                    // string serial_1 = (string)gvr.Cells[1].Text.Trim();
                    Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                    if (chkSelect.Checked)
                    {
                        // Boolean update = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serial_1, -1);
                        //-------------
                        // Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, serial_1);
                        string binCode = gvr.Cells[5].Text;

                        ReptPickSerials _reptPickSerial_nonSer = CHNLSVC.Inventory.Get_all_details_on_serialID(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, serID);


                        //  _reptPickSerial_nonSer.Tus_new_status = "DO";//lblItemStatus.Text.Trim();//1 status always= CONSG
                        _reptPickSerial_nonSer.Tus_cre_by = GlbUserName;
                        //if (ddlScanBatches.SelectedIndex == 0)
                        //{
                        _reptPickSerial_nonSer.Tus_usrseq_no = generated_seq;


                        Decimal pending_amt_ = Convert.ToDecimal(lblInvoiceQty.Text.ToString()) - Convert.ToDecimal(lblDeliveredQty.Text.ToString());
                        //only updated if the whole amount is finished.
                        if ((Convert.ToDecimal(lblScanQty.Text.ToString()) + num_of_checked_itms) - pending_amt_ == 0)
                        {
                            //Update_inrser_INS_AVAILABLE
                            Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serID, -1);
                        }

                        //  Boolean update_inr_ser = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, "N/A", -1);
                        _reptPickSerial_nonSer.Tus_cre_by = GlbUserName;

                        _reptPickSerial_nonSer.Tus_base_doc_no = lblInvoiceNo.Text;
                        _reptPickSerial_nonSer.Tus_base_itm_line = Convert.ToInt16(hdnInvoiceLineNo.Value);

                        MasterItem mi = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
                        _reptPickSerial_nonSer.Tus_itm_desc = mi.Mi_shortdesc;
                        _reptPickSerial_nonSer.Tus_itm_model = mi.Mi_model;
                        _reptPickSerial_nonSer.Tus_qty = Convert.ToDecimal(txtPopupQty.Text.Trim());
                        //enter row into TEMP_PICK_SER
                        Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_nonSer, null);

                        rowCount++;
                        //isManualscan = true;
                        actual_non_ser_List.Add(_reptPickSerial_nonSer);

                        //  binCD = _reptPickSerial_nonSer.Tus_bin;//the last items's bin will be assigned at last
                    }

                }
                if (rowCount > 0)
                {
                    //Remove Duplicates Prabhath on 25/07/2012


                    ////create a dummy to represent the sum

                    ////**************Add**********
                    //List<ReptPickSerials> SummaryList = new List<ReptPickSerials>();
                    //foreach (ReptPickSerials rps in actual_non_ser_List)
                    //{
                    //    if (rps.Tus_ser_1 != GlbDummySerial1)
                    //    {
                    //        SummaryList.Add(rps);
                    //    }
                    //}

                    //// var items = finalserList.GroupBy(x => x.Tus_itm_cd, x.Tus_base_itm_line).ToList();

                    //var sums = SummaryList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_bin }).Select(group => new { Peo = group.Key, theCount = group.Count() });

                    ////**************Add*********

                    //foreach (var s in sums)
                    //{

                    //    ReptPickSerials reptPickSerial_dummy = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, s.Peo.Tus_bin, itemCode, "N/A");
                    //    // ReptPickSerials reptPickSerial_dummy = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, "DOdummyBin", itemCode, "N/A");
                    //    reptPickSerial_dummy.Tus_usrseq_no = userseq_no;
                    //    reptPickSerial_dummy.Tus_ser_id = 0;
                    //    // reptPickSerial_dummy.Tus_ser_1 = "_";
                    //    reptPickSerial_dummy.Tus_ser_1 = GlbDummySerial1;
                    //    //reptPickSerial_dummy.Tus_new_status = ddlItemStatus.SelectedValue;
                    //    reptPickSerial_dummy.Tus_cre_by = GlbUserName;
                    //    MasterItem mi = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
                    //    reptPickSerial_dummy.Tus_itm_desc = mi.Mi_longdesc;
                    //    reptPickSerial_dummy.Tus_itm_model = mi.Mi_model;
                    //    reptPickSerial_dummy.Tus_qty = Convert.ToDecimal(txtPopupQty.Text.Trim());

                    //    reptPickSerial_dummy.Tus_com = GlbUserComCode;
                    //    reptPickSerial_dummy.Tus_loc = GlbUserDefLoca;
                    //    reptPickSerial_dummy.Tus_bin = s.Peo.Tus_bin;
                    //    reptPickSerial_dummy.Tus_itm_cd = itemCode;
                    //    reptPickSerial_dummy.Tus_base_doc_no = lblInvoiceNo.Text;//-->if not needed, it is free to ignore this

                    //    // ReptPickSerials check_reptPickSerial_dummy = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, "_");
                    //    List<ReptPickSerials> temp_pick_ser = new List<ReptPickSerials>();
                    //    temp_pick_ser = CHNLSVC.Inventory.Get_TEMP_PICK_SER(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, 0, GlbDummySerial1, itemCode, s.Peo.Tus_bin);
                    //    if (temp_pick_ser != null)
                    //    {
                    //        foreach (ReptPickSerials check_reptPickSerial_dummy in temp_pick_ser)//this temp_pick_ser list contains only 1 row
                    //        {
                    //            // Update_temp_pick_serdummy
                    //            Decimal newQTY = check_reptPickSerial_dummy.Tus_qty + Convert.ToDecimal(txtPopupQty.Text.Trim());
                    //            Boolean affectedrows = CHNLSVC.Inventory.Update_temp_pick_serdummy(GlbUserName, GlbUserComCode, GlbUserDefLoca, userseq_no, GlbDummySerial1, itemCode, s.Peo.Tus_bin, newQTY);
                    //            // Boolean affected = CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, userseq_no, 0);//delete existing dummy
                    //            //delete existing dummy (delete only the dummy, not all non serials along with it)
                    //            // Boolean affectedrows = CHNLSVC.Inventory.Del_temp_pick_serdummy(GlbUserComCode, GlbUserDefLoca, userseq_no, 0, itemCode, binCode);//neeed to be modified(the sp. Add user,ser_1)

                    //        }

                    //    }
                    //    else
                    //    {
                    //        Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(reptPickSerial_dummy, null);
                    //    }
                    //}

                }

            }


            LoadInvoiceItems(lblInvoiceNo.Text, true);
        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnPopupAutoSelect_Click(object sender, EventArgs e)
        {
            string itemCode = lblPopupItemCode.Text.Trim();

            Int32 userseq_no;
            Int32 num_of_checked_itms = 0;
            foreach (GridViewRow gvr in this.GridPopup.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");

                if (chkSelect.Checked)
                {
                    num_of_checked_itms++;
                }


            }
            Decimal pending_amt = Convert.ToDecimal(lblInvoiceQty.Text.ToString()) - Convert.ToDecimal(lblDeliveredQty.Text.ToString());
            if ((Convert.ToDecimal(lblScanQty.Text.ToString()) + num_of_checked_itms) > pending_amt)
            {
                Decimal availability = Convert.ToDecimal(lblInvoiceQty.Text.ToString()) - (Convert.ToDecimal(lblDeliveredQty.Text.ToString()) + Convert.ToDecimal(lblScanQty.Text.ToString()));
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Can't exceed Invoice Qty! Can add only " + availability + " itmes more.");
                return;
            }

            ModalPopupExtItem.Show();
        }

        protected void btnPopupSarch_Click(object sender, EventArgs e)
        {
            //List<ReptPickSerials> serial_list = new List<ReptPickSerials>();

            if (ddlPopupSerial.SelectedValue == "Serial 1")
            {
                string serial_no = txtPopupSearchSer.Text.Trim();
                //call query.
                string serch_serial = txtPopupSearchSer.Text.Trim() + "%";
                string bin = lblPopupBinCode.Text.Trim();
                lblPopupItemCode.Text = lblPopupItemCode.Text;
                serial_list = CHNLSVC.Inventory.Search_by_serial(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text.Trim(), null, serch_serial, null);
                GridPopup.DataSource = serial_list;
                GridPopup.DataBind();

                ModalPopupExtItem.Show();


            }
            else if (ddlPopupSerial.SelectedValue == "Serial 2")
            {
                string serial_no = txtPopupSearchSer.Text.Trim();
                //call query.
                string serch_serial = txtPopupSearchSer.Text.Trim() + "%";
                string bin = lblPopupBinCode.Text.Trim();
                lblPopupItemCode.Text = lblPopupItemCode.Text;
                serial_list = CHNLSVC.Inventory.Search_by_serial(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text.Trim(), null, null, serch_serial);
                GridPopup.DataSource = serial_list;
                GridPopup.DataBind();

                ModalPopupExtItem.Show();
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select serial type from drop down!");

            }
        }

        protected void btnGridUpdate_Click(object sender, EventArgs e)
        {

            GridViewRow clickedRow = ((Button)sender).NamingContainer as GridViewRow;
            TextBox txtDOqty = (TextBox)clickedRow.FindControl("txtGridDoQty");
            Int32 DOQty = Convert.ToInt32(txtDOqty.Text);

            Int32 invQty = Convert.ToInt32(clickedRow.Cells[4].Text.ToString());

            Label lbl_itemLine = (Label)clickedRow.FindControl("lblItemLine");

            string priceBook = string.Empty;
            string priceLevel = string.Empty;


            Int32 item_line = Convert.ToInt32(lbl_itemLine.Text);
            // selected_item_line = item_line;
            lblPopupItemCode.Text = clickedRow.Cells[1].Text.ToString();
            Int32 generated_seq = -1;
            //lbltest.Text = "Item line ="+ clickedRow.Cells[7].Text.ToString();
            // lbltest.Text = "Item line =" + lbl_itemLine.Text;
            //string itemCode = lblPopupItemCode.Text.Trim();
            Int32 userseq_no;

            //Int32 invQty = Convert.ToInt32(gvInvoiceItem.SelectedRow.Cells[4].ToString());
            //Int32 DOQty = Convert.ToInt32(gvInvoiceItem.FindControl("txtGridDoQty"));
            if (DOQty > invQty)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Exeeds the invoice Qty!!");
            }

            else
            {
                //--have to DO the amount entered.--//

                //----------------------------//
                Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("DO", GlbUserComCode, lblInvoiceNo.Text, 0);
                if (user_seq_num != -1)//check whether Tuh_doc_no exestst in temp_pick_hdr
                {
                    //INCR00001
                    generated_seq = user_seq_num;
                    userseq_no = generated_seq;
                }
                else
                {
                    // generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "ADJ", 1, GlbUserComCode);//direction always =1 for this method
                    generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "DO", 1, GlbUserComCode);//direction always =1 for this method
                    //assign user_seqno
                    userseq_no = generated_seq;
                    ReptPickHeader RPH = new ReptPickHeader();
                    RPH.Tuh_doc_tp = "DO";
                    RPH.Tuh_cre_dt = DateTime.Today;//might change //Calendar-SelectedDate;
                    RPH.Tuh_ischek_itmstus = true;//might change 
                    RPH.Tuh_ischek_reqqty = true;//might change
                    RPH.Tuh_ischek_simitm = true;//might change
                    RPH.Tuh_session_id = GlbUserSessionID;
                    RPH.Tuh_usr_com = GlbUserComCode;//might change 
                    RPH.Tuh_usr_id = GlbUserName;
                    RPH.Tuh_usrseq_no = generated_seq;

                    RPH.Tuh_direct = false; //direction always (-) for change status
                    RPH.Tuh_doc_no = lblInvoiceNo.Text;
                    //write entry to TEMP_PICK_HDR
                    int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);

                    //seqNumList_out = CHNLSVC.Inventory.GetAll_Adj_SeqNums_for_user(GlbUserName, "ADJ", 0, GlbUserComCode);//for OUT(status changes comes as outs)
                    //seqNumList_out[0] = "New Scan";
                    //ddlScanBatches.DataSource = seqNumList_out;
                    //ddlScanBatches.DataBind();
                }


                //-----------------------------//

                MasterItem msitem = new MasterItem();
                msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, lblPopupItemCode.Text);
                // msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, "LGDVD270");

                if (msitem.Mi_is_ser1 == 1)//check whether it is a non-sirialized item
                //msitem.Mi_is_ser1 == true
                {

                    serial_list = new List<ReptPickSerials>();
                    //Search_serials_for_itemCD(string company, string location, string itemCode)

                    serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text, priceBook, priceLevel);
                    //serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca, "LGDVD270");
                    int count = 0;
                    foreach (ReptPickSerials r in serial_list)
                    {
                        if (DOQty > 0)
                        {
                            string binCode = r.Tus_bin;
                            Int32 serID = r.Tus_ser_id;
                            // ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(GlbUserComCode, GlbUserDefLoca, binCode, selected_itemCD, serID);
                            r.Tus_cre_by = GlbUserName;

                            r.Tus_usrseq_no = userseq_no;
                            //Update_inrser_INS_AVAILABLE
                            Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text, serID, -1);
                            //  Boolean update_inr_ser = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, "N/A", -1);
                            r.Tus_cre_by = GlbUserName;

                            MasterItem mi = CHNLSVC.Inventory.GetItem(GlbUserComCode, lblPopupItemCode.Text);
                            r.Tus_itm_desc = mi.Mi_shortdesc;
                            r.Tus_itm_model = mi.Mi_model;
                            //enter row into TEMP_PICK_SER
                            Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(r, null);
                            //update in the sat
                            foreach (InvoiceItem inv_itm in invoice_items_bind)
                            {
                                if (inv_itm.Sad_itm_line == item_line && inv_itm.Sad_itm_cd == lblPopupItemCode.Text)
                                {
                                    // Boolean update_sat_do_qty=CHNLSVC.Inventory.Update_sat_itm_DO_qty(selected_invoice_no,item_line,1);//pass the qty to update

                                    //call update sat_itm
                                }
                            }
                            count++;
                            if (count == DOQty)
                            {
                                break;
                                //return;
                            }
                        }
                        else
                        { }


                    }


                }
                else//if non-serial
                {
                    //lblPopQty.Visible = true;
                    //txtPopupQty.Visible = true;
                }

                //to view in Serial grid
                List<ReptPickSerials> AllserList = new List<ReptPickSerials>();
                AllserList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "DO");
                List<ReptPickSerials> finalserList = new List<ReptPickSerials>();
                if (AllserList != null)
                {
                    foreach (ReptPickSerials rps in AllserList)
                    {
                        if (rps.Tus_ser_1 != "N/A")
                        {
                            finalserList.Add(rps);
                        }
                    }
                    GridViewDo_serials.DataSource = finalserList;
                    // GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ");
                    GridViewDo_serials.DataBind();
                    //***********
                }
                else
                {
                    List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                    emptyGridList.Add(new ReptPickSerials());
                    GridViewDo_serials.DataSource = emptyGridList;
                    GridViewDo_serials.DataBind();

                }

            }

        }

        protected void btnDeleteSer_Click(object sender, EventArgs e)
        {
            //delete all cheked items from the list.(remove from grid)
            foreach (GridViewRow gvr in this.GridViewDo_serials.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("SelectCheck");

                Int32 usrseqNo = Convert.ToInt32(gvr.Cells[8].Text);

                Int32 ser_id = Convert.ToInt32(gvr.Cells[9].Text);

                string serial_1 = gvr.Cells[1].Text;

                string itemCode = gvr.Cells[2].Text;
                string bincode = gvr.Cells[3].Text;
                if (chkSelect.Checked)
                {
                    //if (serial_1 == "_")
                    if (serial_1 == GlbDummySerial1)
                    {
                        List<ReptPickSerials> NA_list = new List<ReptPickSerials>();

                        NA_list = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, usrseqNo, "DO");
                        if (NA_list != null)
                        {
                            foreach (ReptPickSerials r in NA_list)
                            {
                                if (r.Tus_bin == bincode && r.Tus_usrseq_no == usrseqNo && r.Tus_ser_1 == "N/A" && r.Tus_itm_cd == itemCode)
                                {
                                    Int32 serialID = r.Tus_ser_id;
                                    Boolean update_av = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serialID, 1);
                                    // Boolean update = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serial_1, 1);
                                }
                            }
                        }

                        //delete both dummy and relevent rows in temp_pick_ser
                        Boolean affectedrows = CHNLSVC.Inventory.Del_temp_pick_serdummy(GlbUserComCode, GlbUserDefLoca, usrseqNo, ser_id, itemCode, bincode);
                    }
                    else
                    {
                        //remove from TEMP_PICK_SER
                        Boolean affected = CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, usrseqNo, ser_id);
                    }
                    //remove from TEMP_PICK_SER
                    // Boolean affected = CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, usrseqNo, ser_id);
                    //Update_inrser_INS_AVAILABLE
                    Boolean update = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serial_1, 1);
                }
                else
                {
                    //do nothing
                }



            }//end of foreach

            LoadInvoiceItems(lblInvoiceNo.Text.ToString(), true);
        }

        protected void btnSaveDO_Click(object sender, EventArgs e)
        {
            DataTable dt = CHNLSVC.Sales.GetPendingInvoiceItemsByItemDT(Invoice_no, lblPopupItemCode.Text);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt.Rows[0]["SAD_ISAPP"]) != 1 || Convert.ToInt32(dt.Rows[0]["SAD_ISCOVERNOTE"]) != 1)
                {
                    CanSave = false;
                }
            }

            if (CanSave)
            {
                try
                {
                    if (IsDate(txtDODate.Text,DateTimeStyles.None) == false)
                    {
                        txtDODate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                        throw new UIValidationException("Invald Date.");
                    }

                    if (string.IsNullOrEmpty(GlbUserComCode))
                    {
                        txtManualRefNo.Focus();
                        throw new UIValidationException("Session expired! Please re-login to system.");
                    }

                    if (string.IsNullOrEmpty(lblInvoiceNo.Text))
                    {
                        txtManualRefNo.Focus();
                        throw new UIValidationException("Please select invoice no.");
                    }
                    if (string.IsNullOrEmpty(txtManualRefNo.Text))
                    {
                        //txtManualRefNo.Focus();
                        //throw new UIValidationException("Please enter manual referance no.");
                        txtManualRefNo.Text = "N/A";
                    }
                    //if (Convert.ToDateTime(txtDODate.Text).Date < Convert.ToDateTime(lblInvoiceDate.Text).Date)
                    
                    int resultDate = DateTime.Compare(Convert.ToDateTime(lblInvoiceDate.Text).Date, Convert.ToDateTime(txtDODate.Text).Date);
                    if (resultDate >0)
                    {
                        throw new UIValidationException("Delivery date should be greater than or equal to invoice date.");
                    }
                    if (string.IsNullOrEmpty(txtVehicleNo.Text))
                    {
                        txtVehicleNo.Text = string.Empty;
                    }
                    if (string.IsNullOrEmpty(txtRemarks.Text))
                    {
                        txtRemarks.Text = string.Empty;
                    }


                    //if (CHNLSVC.General.IsAllowBackDate(GlbUserComCode, GlbUserDefLoca, string.Empty, txtDODate.Text) == false)
                    if (IsAllowBackDateForModule(GlbUserComCode, GlbUserDefLoca, string.Empty, Page, txtDODate.Text, imgDODate, lblDispalyInfor) == false)
                    {
                        if (Convert.ToDateTime(txtDODate.Text).Date != DateTime.Now.Date)
                        {
                            throw new UIValidationException("Back date not allow for selected date!");
                        }
                    }


                    List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                    InventoryHeader invHdr = new InventoryHeader();
                    string documntNo = "";
                    Int32 result = -99;

                    Int32 _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("DO", GlbUserComCode, lblInvoiceNo.Text, 0);
                    reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, _userSeqNo, "DO");

                    if (reptPickSerialsList == null)
                    {
                        throw new UIValidationException("No delivery items found!");
                    }



                    #region Check Duplicate Serials
                    var _dup = reptPickSerialsList.Where(x => x.Tus_ser_1 != "N/A").Select(y => y.Tus_ser_id).ToList();

                    string _duplicateItems = string.Empty;
                    bool _isDuplicate = false;
                    if (_dup != null)
                        if (_dup.Count > 0)
                            foreach (Int32 _id in _dup)
                            {
                                Int32 _counts = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(y => y.Tus_ser_id).Count();
                                if (_counts > 1)
                                {
                                    _isDuplicate = true;
                                    var _item = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(x => x.Tus_itm_cd).Distinct();
                                    foreach (string _str in _item)
                                        if (string.IsNullOrEmpty(_duplicateItems))
                                            _duplicateItems = _str;
                                        else
                                            _duplicateItems += "," + _str;
                                }
                            }
                    if (_isDuplicate)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems);
                        return;
                    }
                    #endregion




                    List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                    reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "DO");

                    DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(GlbUserComCode, GlbUserDefLoca);
                    foreach (DataRow r in dt_location.Rows)
                    {
                        // Get the value of the wanted column and cast it to string
                        invHdr.Ith_sbu = (string)r["ML_OPE_CD"];
                        if (System.DBNull.Value != r["ML_CATE_2"])
                        {
                            invHdr.Ith_channel = (string)r["ML_CATE_2"];
                        }
                        else
                        {
                            invHdr.Ith_channel = string.Empty;
                        }
                    }

                    btnSaveDO.Enabled = false;

                    invHdr.Ith_loc = GlbUserDefLoca;
                    invHdr.Ith_com = GlbUserComCode;
                    invHdr.Ith_doc_tp = "DO";
                    invHdr.Ith_doc_date = Convert.ToDateTime(txtDODate.Text).Date;
                    invHdr.Ith_doc_year = Convert.ToDateTime(txtDODate.Text).Year;
                    invHdr.Ith_cate_tp = "DPS";
                    invHdr.Ith_is_manual = false;
                    invHdr.Ith_stus = "A";
                    invHdr.Ith_cre_by = GlbUserName;
                    invHdr.Ith_mod_by = GlbUserName;
                    invHdr.Ith_direct = false;
                    invHdr.Ith_session_id = GlbUserSessionID;
                    invHdr.Ith_manual_ref = txtManualRefNo.Text;
                    invHdr.Ith_vehi_no = txtVehicleNo.Text;
                    invHdr.Ith_remarks = txtRemarks.Text;
                    invHdr.Ith_anal_1 = _userSeqNo.ToString();
                    invHdr.Ith_oth_docno = lblInvoiceNo.Text.ToString();


                    MasterAutoNumber masterAutoNum = new MasterAutoNumber();
                    masterAutoNum.Aut_cate_cd = GlbUserDefLoca;
                    masterAutoNum.Aut_cate_tp = "LOC";
                    masterAutoNum.Aut_direction = 0;
                    masterAutoNum.Aut_moduleid = "DO";
                    masterAutoNum.Aut_start_char = "DO";
                    masterAutoNum.Aut_year = Convert.ToDateTime(txtDODate.Text).Year;
                    List<ReptPickSerials> reptPickSerialsListGRN = new List<ReptPickSerials>();
                    List<ReptPickSerialsSub> reptPickSubSerialsListGRN = new List<ReptPickSerialsSub>();
                    if (reptPickSerialsList != null)
                    { reptPickSerialsListGRN = reptPickSerialsList; }

                    if (reptPickSubSerialsList != null)
                    { reptPickSubSerialsListGRN = reptPickSubSerialsList; }


                    if (!IsGrn)
                        result = CHNLSVC.Inventory.DeliveryOrder(invHdr, reptPickSerialsList, reptPickSubSerialsList, masterAutoNum, out documntNo);
                    else
                    {
                        InventoryHeader _invHeaderGRN = new InventoryHeader();
                        _invHeaderGRN.Ith_com = GlbUserComCode;
                        _invHeaderGRN.Ith_loc = GlbUserDefLoca;
                        _invHeaderGRN.Ith_doc_date = Convert.ToDateTime(txtDODate.Text).Date;
                        _invHeaderGRN.Ith_doc_year = Convert.ToDateTime(txtDODate.Text).Year;
                        _invHeaderGRN.Ith_direct = true;
                        _invHeaderGRN.Ith_doc_tp = "GRN";
                        _invHeaderGRN.Ith_cate_tp = "LOCAL";
                        //_invHeader.Ith_sub_tp = "CONSIGN";
                        //_invHeader.Ith_bus_entity = lblSupplierCode.Text;
                        _invHeaderGRN.Ith_is_manual = true;
                        _invHeaderGRN.Ith_manual_ref = txtManualRefNo.Text;
                        _invHeaderGRN.Ith_remarks = txtRemarks.Text;
                        _invHeaderGRN.Ith_stus = "A";
                        _invHeaderGRN.Ith_cre_by = GlbUserName;
                        _invHeaderGRN.Ith_cre_when = DateTime.Now;
                        _invHeaderGRN.Ith_mod_by = GlbUserName;
                        _invHeaderGRN.Ith_mod_when = DateTime.Now;
                        _invHeaderGRN.Ith_session_id = GlbUserSessionID;
                        _invHeaderGRN.Ith_oth_docno = lblInvoiceNo.Text.ToString();


                        MasterAutoNumber _masterAutoGRN = new MasterAutoNumber();
                        _masterAutoGRN.Aut_cate_cd = GlbUserDefLoca;
                        _masterAutoGRN.Aut_cate_tp = "LOC";
                        _masterAutoGRN.Aut_direction = null;
                        _masterAutoGRN.Aut_modify_dt = null;
                        _masterAutoGRN.Aut_moduleid = "GRN";
                        _masterAutoGRN.Aut_number = 0;
                        _masterAutoGRN.Aut_start_char = "GRN";
                        _masterAutoGRN.Aut_year = null;
                        string documntNoGRN = "";

                        result = CHNLSVC.Inventory.DeliveryOrder_Auto(invHdr, reptPickSerialsList, reptPickSubSerialsList, masterAutoNum, out documntNo, _invHeaderGRN, reptPickSerialsListGRN, reptPickSubSerialsListGRN, _masterAutoGRN, out documntNoGRN);
                    }
                    if (result != -99 && result > 0)
                    {
                        btnSaveDO.Enabled = true;
                        string Msg = "<script>alert('Successfully Saved! Document No : " + documntNo + "');window.location = 'DeliveryOrder.aspx';</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                        //Response.Redirect("~/Inventory_Module/DeliveryOrder.aspx", false);

                        //kapila 16/7/2012
                        //string _DocNoList = string.Empty;
                        //string[] seperator = new string[] { "|" };
                        //string[] searchParams = documntNo.Split(seperator, StringSplitOptions.None);
                        //foreach (string s in searchParams)
                        //{
                        //    _DocNoList = _DocNoList + s;
                        //}

                        GlbDocNosList = documntNo;

                        if (GlbUserComCode == "SGL")//Sanjeewa 2012-11-10
                        {
                            GlbReportPath = "~\\Reports_Module\\Inv_Rep\\Outward_DocPre.rpt";
                            GlbReportMapPath = "~/Reports_Module/Inv_Rep/Outward_DocPre.rpt";
                        }
                        else
                        {
                            GlbReportPath = "~\\Reports_Module\\Inv_Rep\\Outward_Doc.rpt";
                            GlbReportMapPath = "~/Reports_Module/Inv_Rep/Outward_Doc.rpt";
                        }

                        GlbMainPage = "~/Inventory_Module/DeliveryOrder.aspx";
                        Response.Redirect("~/Reports_Module/Inv_Rep/Print.aspx");


                        Msg = "<script>onClick='showPopup('~/Reports_Module/Inv_Rep/Print.aspx',1); return(false);'</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "showPopup", Msg, false);

                        Msg = "<script>onClick='showPopup('~/Reports_Module/Sales_Rep/Print.aspx',2); return(false);'</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "showPopup", Msg, false);

                    }
                    else
                    {
                        btnSaveDO.Enabled = true;
                        string Msg = "<script>alert('Sorry, not Saved!');</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                    }

                }
                catch (UIValidationException ex)
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.ErrorMessege);
                }
                catch (Exception err)
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, err.Message);
                }
            }
            else
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Save unsucessful");
                CanSave = true;
            }
            CallDObyInvoice = null;

        }

        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            string prof_cnter = GlbUserDefProf;
            string company = GlbUserComCode;
            DateTime fromDT;
            DateTime toDT;

            DateTime thisDate_ = DateTime.MinValue;
            DateTime date_ = new DateTime(thisDate_.Year, thisDate_.Month, thisDate_.Day);
            CultureInfo culture_ = new CultureInfo("pt-BR");
            txtDateFrom.Text = thisDate_.ToString("d", culture_);

            DateTime thisDate = DateTime.Now;
            DateTime date = new DateTime(thisDate.Year, thisDate.Month, thisDate.Day);
            CultureInfo culture = new CultureInfo("pt-BR");
            txtDateTo.Text = thisDate.ToString("d", culture);


            try
            {
                fromDT = Convert.ToDateTime(txtDateFrom.Text.Trim());
                toDT = Convert.ToDateTime(txtDateTo.Text.Trim());
            }
            catch (Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter Search Dates correctly.");
                return;
            }

            try
            {

                DataTable dt = CHNLSVC.Sales.GetPendingInvoiceDetails(GlbUserDefProf, GlbUserComCode, txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), GlbUserDefLoca);
                if (dt.Rows.Count > 0)
                {
                    gvInvoiceItem.DataSource = dt;
                    gvInvoiceItem.DataBind();

                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No pending invoices found!");
                    gvInvoiceItem.DataSource = dt;
                    gvInvoiceItem.DataBind();
                    return;
                }


            }
            catch (Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Data receive faliled!");
            }

        }

        //Chamal 30-05-2012
        protected void Close(object sender, EventArgs e)
        {
            CallDObyInvoice = null;
            Response.Redirect("~/Default.aspx", false);
        }

        protected void chkDirectDO_CheckChange(object sender, EventArgs e)
        {
            //if (ch.Checked)
            //{
            //    pnlScanItem.Visible = true;
            //    CPEScanItem.Collapsed = false;
            //    this.CPEScanItem.ClientState = "false";
            //}
            //else pnlScanItem.Visible = false;
        }

        protected void btnPickScan_Click(object sender, EventArgs e)
        {

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Inventory_Module/DeliveryOrder.aspx", false);
        }

        protected void gvInvoiceItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 row_id = e.RowIndex;

            string _selectedReqNo = gvInvoiceItem.DataKeys[row_id]["SAH_INV_NO"].ToString();
            Invoice_no = _selectedReqNo;
            this.LoadInvoiceItems(_selectedReqNo, true);
            this.LoadInvoiceHeader(_selectedReqNo);
            this.LoadInvoiceItems(_selectedReqNo, false);

            lblInvoiceDate.Text = Convert.ToDateTime(gvInvoiceItem.DataKeys[row_id]["SAH_DT"].ToString()).Date.ToString("dd/MMM/yyyy");
        }



    }
}
