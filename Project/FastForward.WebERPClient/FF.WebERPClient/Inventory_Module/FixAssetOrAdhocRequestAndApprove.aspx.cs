using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Data;
using System.Globalization;
using System.Text;
using System.Drawing;

namespace FF.WebERPClient.Inventory_Module
{
    public partial class FixAssetOrAdhocRequestAndApprove : BasePage
    {
        BasePage _basePage = null;

        public string Gen_ADJ_DocNo
        {
            get { return (Session["Gen_ADJ_DocNo"]).ToString(); }
            set { Session["Gen_ADJ_DocNo"] = value; }
        }
        public Int32 ItmLine
        { 
            get { return Convert.ToInt32(Session["ItmLine"]); }
            set { Session["ItmLine"] = value; }
        }
        public string SelectedItemCD
        {
            get { return (Session["SelectedItemCD"]).ToString(); }
            set { Session["SelectedItemCD"] = value; }
        }
        public List<InventoryAdhocDetail> AdhodDetList
        {
            get { return (List<InventoryAdhocDetail>)Session["AdhodDetList"]; }
            set { Session["AdhodDetList"] = value; }
        }
        public List<ReptPickSerials> AvailableSerialList
        {
            get { return (List<ReptPickSerials>)Session["AvailableSerialList"]; }
            set { Session["AvailableSerialList"] = value; }
        }
        public List<ReptPickSerials> Approved_SerialList
        {
            get { return (List<ReptPickSerials>)Session["Approved_SerialList"]; }
            set { Session["Approved_SerialList"] = value; }
        }

        public InventoryAdhocHeader Searched_AdhodHeader
        {
            get { return (InventoryAdhocHeader)Session["Searched_AdhodHeader"]; }
            set { Session["Searched_AdhodHeader"] = value; }
        }
        //----------Payments----------------------------------------------------
        public List<PaymentType> PaymentTypes
        {
            get { return (List<PaymentType>)Session["PaymentTypes"]; }
            set { Session["PaymentTypes"] = value; }
        }
        public Decimal PaidAmount
        {
            get { return Convert.ToDecimal(Session["PaidAmount"]); }
            set { Session["PaidAmount"] = Math.Round(value, 2); }
        }

        public Decimal BalanceAmount
        {
            get { return Convert.ToDecimal(Session["BalanceAmount"]); }
            set { Session["BalanceAmount"] = Math.Round(value, 2); }
        }
        public Decimal BankOrOther_Charges
        {
            get { return Convert.ToDecimal(Session["BankOrOther_Charges"]); }
            set { Session["BankOrOther_Charges"] = Math.Round(value, 2); }
        }
        public Decimal AmtToPayForFinishPayment
        {
            get { return Convert.ToDecimal(Session["AmtToPayForFinishPayment"]); }
            set { Session["AmtToPayForFinishPayment"] = Math.Round(value, 2); }
        }
        public List<RecieptItem> _recieptItem
        {
            get { return (List<RecieptItem>)Session["RecieptItemList"]; }
            set { Session["RecieptItemList"] = value; }
        }
        public List<HpTransaction> Transaction_List
        {
            get { return (List<HpTransaction>)Session["Transaction_List"]; }
            set { Session["Transaction_List"] = value; }
        }
        public List<InventoryAdhocDetail> det_list_selected
        {
            get { return (List<InventoryAdhocDetail>)Session["det_list_selected"]; }
            set { Session["det_list_selected"] = value; }
        }
        public List<InventoryAdhocDetail> searchedAdhocDetList //Always contain the original requested item detail list
        {
             get { return (List<InventoryAdhocDetail>)Session["searchedAdhocDetList"]; }
            set { Session["searchedAdhocDetList"] = value; }
        }
       
        //----------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                txtItemCD.Attributes.Add("onblur", "return onblurFire(event,'" + btnItmCd.ClientID + "')");
                //txtItemCD.Attributes.Add("onFocus", "return onblurFire(event,'" + btnItmCd.ClientID + "')");
                txtPBLevel.Attributes.Add("onblur", "return onblurFire(event,'" + btnUnitPrice.ClientID + "')");   //btnUnitPrice

                txtRefNo.Attributes.Add("onFocus", "return onFocusFire(event,'" + btnRefOk.ClientID + "')");                
             
              //  txtRefNo.Attributes.Add("onFocus", "JavaScript: onBlurOnFocus('" + rb1.ClientID + "', '" + tb1.ClientID + "');");

                Dictionary<string, string> status_list = CHNLSVC.Inventory.Get_all_ItemSatus();
                status_list.Add("Any", "Any");
                ddlStatus.DataSource = status_list.Keys;
                ddlStatus.DataBind();
                ddlStatus.SelectedValue = "Any";

                ddlStatus.Enabled = true;
                //----------view states--------------------------
                Reset_Session_values();
                //-----------------------------------------------

                //-----------Bind grids------------
                grvAvailableSerials.DataSource = AvailableSerialList;
                grvAvailableSerials.DataBind();
                grvApproveItms.DataSource = Approved_SerialList;
                grvApproveItms.DataBind();
                grvItmDes.DataSource = AdhodDetList;
                grvItmDes.DataBind();
                //---------------------------------


                if (ddlReuestType.SelectedValue == "2")
                {
                    txtSendLoc.Text = GlbUserDefLoca;
                    divLoc.Visible = true;
                    Panel_PriceDet.Visible = false;
                }
                else
                {
                    txtSendLoc.Text = "";
                    divLoc.Visible = false;
                    Panel_PriceDet.Visible = true;
                }

                PaymentTypes = CHNLSVC.Sales.GetPossiblePaymentTypes(GlbUserDefProf, "HPR", DateTime.Now.Date);
                BindPaymentType(ddlPayMode);
                BindReceiptItem();
                BankOrOther_Charges = 0;
                AmtToPayForFinishPayment = 0;

                //  div_payment.Visible = false;
                pnlPayss.Enabled = false;
                
            }
        }
        private void Reset_Session_values()
        {
            //----------view states--------------------------
            ItmLine = 0;
            AdhodDetList = new List<InventoryAdhocDetail>();
            SelectedItemCD = string.Empty;
            AvailableSerialList = new List<ReptPickSerials>();
            Approved_SerialList = new List<ReptPickSerials>();
            Searched_AdhodHeader = null;
            det_list_selected = new List<InventoryAdhocDetail>();
            searchedAdhocDetList = new List<InventoryAdhocDetail>();
            //-----------------------------------------------
        }

        private void getItemDesc()
        { 
        
        }
        private void clearCompleateScreen()
        {
            Dictionary<string, string> status_list = CHNLSVC.Inventory.Get_all_ItemSatus();
            status_list.Add("Any", "Any");
            ddlStatus.DataSource = status_list.Keys;
            ddlStatus.DataBind();
            ddlStatus.SelectedValue = "Any";

            ddlStatus.Enabled = true;
            //----------view states--------------------------
            Reset_Session_values();
            //-----------------------------------------------

            //-----------Bind grids------------
            grvAvailableSerials.DataSource = AvailableSerialList;
            grvAvailableSerials.DataBind();
            grvApproveItms.DataSource = Approved_SerialList;
            grvApproveItms.DataBind();
            grvItmDes.DataSource = AdhodDetList;
            grvItmDes.DataBind();
            //---------------------------------


            if (ddlReuestType.SelectedValue == "2")
            {
                txtSendLoc.Text = GlbUserDefLoca;
                divLoc.Visible = true;
                Panel_PriceDet.Visible = false;
            }
            else
            {
                txtSendLoc.Text = "";
                divLoc.Visible = false;
                Panel_PriceDet.Visible = true;
            }

            PaymentTypes = CHNLSVC.Sales.GetPossiblePaymentTypes(GlbUserDefProf, "HPR", DateTime.Now.Date);
            BindPaymentType(ddlPayMode);
            BindReceiptItem();
            BankOrOther_Charges = 0;
            AmtToPayForFinishPayment = 0;

            //--------------------------------------------------------
            txtItemCD.Text = "";
            txtItmDescription.Text = "";
            txtModel.Text = "";
            txtPBLevel.Text = "";
            txtPC.Text = "";
            txtQty.Text = "";
            txtUnitPrice.Text = "";

        
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            // _basePage = new BasePage();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {


                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(GlbUserName + seperator + GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(GlbUserName + seperator + GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBook:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + "CS" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevel:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + "CS" + seperator + txtPriceBook.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.FixAssetRefNo:
                    {
                        //( p_com in NVARCHAR2,p_loc in NVARCHAR2,p_refNo in NVARCHAR2,p_type in NUMBER,p_status in NUMBER,c_data OUT sys_refcursor)
                        string Loc = txtSendLoc.Text.Trim().ToUpper();                       
                        Int32 type = ddlReuestType.SelectedValue=="2"?2:1;
                       // Int32 status = rdoPending.Checked == true ? 0 : 1;
                        Int32 status = ddlAction.SelectedValue == "Approve" ? 0 : 1;
                        paramsText.Append(GlbUserComCode + seperator + Loc + seperator + type + seperator + status);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void imgBtnItemStatus_Click(object sender, ImageClickEventArgs e)
        {

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCompanyItemStatusData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = ddlStatus.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void btn_CLEAR_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Inventory_Module/FixAssetOrAdhocRequestAndApprove.aspx");

        }

        protected void btn_CLOSE_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        protected void imgbtnSearchLocation_Click(object sender, ImageClickEventArgs e)
        {

            _basePage = new BasePage();

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            DataTable dataSource = _basePage.CHNLSVC.CommonSearch.GetUserLocationSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtFgapLoc.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();

        }

        protected void imgbtnSearchProfitCenter_Click(object sender, ImageClickEventArgs e)
        {


        }

        protected void btnItmAdd_Click(object sender, EventArgs e)
        {
            if (ddlReuestType.SelectedValue == "2")
            {
                InventoryAdhocDetail Det = fillAdhocDet_FixAsset_Request();
                if (Det != null)
                {
                    AdhodDetList.Add(Det);
                    grvItmDes.DataSource = AdhodDetList;
                    grvItmDes.DataBind();
                }
            }
            else if (ddlReuestType.SelectedValue == "1")
            {
                if (txtPriceBook.Text.Trim() == "" || txtPBLevel.Text.Trim() == "")
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Price Book details.");
                    return;
                }
                if (txtFgapLoc.Text.Trim() == "")
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Location.");
                    txtFgapLoc.Focus();
                    return;
                }
                if (txtPC.Text.Trim() == "")
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Profit Center.");
                    txtPC.Focus();
                    return;
                }
                //InventoryAdhocDetail Det = fillAdhocDet_FixAsset_Request();
                txtSendLoc.Text = txtFgapLoc.Text.Trim();
                InventoryAdhocDetail Det = fillAdhocDet_FGAP_Request();

                if (Det != null)
                {
                    if (AdhodDetList==null)
                    {
                        AdhodDetList = new List<InventoryAdhocDetail>();
                    }
                    AdhodDetList.Add(Det);
                    grvItmDes.DataSource = AdhodDetList;
                    grvItmDes.DataBind();
                    //--------------------------------------------------------------
                    if (btnApprove.Enabled == false)//this means this is an alrady approved, and it has been modified
                    {
                        Decimal totalApprovedVal = 0;
                        foreach (InventoryAdhocDetail detail in AdhodDetList)
                        {
                            totalApprovedVal = totalApprovedVal + detail.Iadd_app_val * Convert.ToDecimal(detail.Iadd_anal1);
                        }

                        lblCollectVal.Text = string.Format("{0:n2}", totalApprovedVal);
                        Decimal priceDiference = Convert.ToDecimal(lblCollectVal.Text) - Convert.ToDecimal(lblApprVal.Text);
                        lblPriceDeference.Text = string.Format("{0:n2}", priceDiference);
                        lblReceiptAmt.Text = priceDiference.ToString();
                        if (priceDiference > 0)
                        {
                            btnConfirm.Enabled = false;
                            btn_SendReq.Enabled = false;
                        }
                        else
                        {
                            btnConfirm.Enabled = true;
                            
                        }
                    }

                    //--------------------------------------------------------------


                }
                else
                { 
                    
                }

            }
            txtItemCD.Text = "";
            txtItmDescription.Text = "";
            txtModel.Text = "";
            txtPBLevel.Text = "";
            txtPC.Text = "";
            txtQty.Text = "";
            txtUnitPrice.Text = "";

        }
        private InventoryAdhocDetail fillAdhocDet_FGAP_Request()
        {
            InventoryAdhocDetail Det = new InventoryAdhocDetail();
            try
            {
                Det.Iadd_anal1 = Convert.ToDecimal(txtQty.Text.Trim());
            }
            catch (Exception ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter valid Qty.");
                return null;
            }

            Det.Iadd_anal2 = txtModel.Text.Trim().ToUpper();
            Det.Iadd_anal3 = txtItmDescription.Text.Trim().ToUpper();
            //Det.Iadd_anal4 = ;
            //Det.Iadd_anal5 =;
            Det.Iadd_app_itm = txtItemCD.Text.Trim().ToUpper(); ;
            Det.Iadd_app_pb = txtPriceBook.Text.ToUpper();
            Det.Iadd_app_pb_lvl = txtPBLevel.Text.ToUpper();


            Det.Iadd_claim_itm = txtItemCD.Text.Trim().ToUpper();
            //Det.Iadd_claim_pb =;
            //Det.Iadd_claim_pb_lvl = ;
            //Det.Iadd_claim_val = ;
            //Det.Iadd_coll_itm =;
            //Det.Iadd_coll_pb = ;
            //Det.Iadd_coll_pb_lvl = ;
            //Det.Iadd_coll_ser1 = ;
            //Det.Iadd_coll_ser2 = ;,
            //Det.Iadd_coll_ser3 = ;
            //Det.Iadd_coll_ser3 = ;
            //Det.Iadd_coll_val = ;
            Det.Iadd_line = ItmLine++;
            //Det.Iadd_ref_no =;
            //Det.Iadd_seq = ;
            Det.Iadd_stus = 1;
            List<PriceDetailRef> _priceDetailRef = new List<PriceDetailRef>();
            _priceDetailRef = CHNLSVC.Sales.GetPrice(GlbUserComCode, txtPC.Text.Trim(), "CS", txtPriceBook.Text, txtPBLevel.Text, string.Empty, txtItemCD.Text, Convert.ToDecimal(txtQty.Text), DateTime.Now.Date);
            if (_priceDetailRef != null)
            {
                if (_priceDetailRef.Count > 0)
                {
                    Decimal unitPrice = _priceDetailRef[0].Sapd_itm_price;
                    txtUnitPrice.Text = unitPrice.ToString();
                    if (txtUnitPrice.Text.Trim() != "")
                    {
                        Det.Iadd_app_val = unitPrice;
                        //Det.Iadd_app_val = Convert.ToDecimal(txtUnitPrice.Text.Trim());
                    }
                    else
                    {

                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Prices not defined for this item in this PriceBook and Level!!");
                        return null;

                    }
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Prices not defined for this item in this PriceBook and Level!!");
                    return null;
                }
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Prices not defined for this item in this PriceBook and Level!!");
                return null;
            }


            return Det;
        }

        private InventoryAdhocDetail fillAdhocDet_FixAsset_Request()
        {
            InventoryAdhocDetail Det = new InventoryAdhocDetail();
            try
            {
                Det.Iadd_anal1 = Convert.ToDecimal(txtQty.Text.Trim());
            }
            catch (Exception ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter valid Qty.");
                return null;
            }

            Det.Iadd_anal2 = txtModel.Text.Trim().ToUpper();
            Det.Iadd_anal3 = txtItmDescription.Text.Trim().ToUpper();
            //Det.Iadd_anal4 = ;
            //Det.Iadd_anal5 =;
            //Det.Iadd_app_itm =;
            //Det.Iadd_app_pb = ;
            //Det.Iadd_app_pb_lvl =;
            //Det.Iadd_app_val = ;
            Det.Iadd_claim_itm = txtItemCD.Text.Trim().ToUpper();
            //Det.Iadd_claim_pb =;
            //Det.Iadd_claim_pb_lvl = ;
            //Det.Iadd_claim_val = ;
            //Det.Iadd_coll_itm =;
            //Det.Iadd_coll_pb = ;
            //Det.Iadd_coll_pb_lvl = ;
            //Det.Iadd_coll_ser1 = ;
            //Det.Iadd_coll_ser2 = ;,
            //Det.Iadd_coll_ser3 = ;
            //Det.Iadd_coll_ser3 = ;
            //Det.Iadd_coll_val = ;
            Det.Iadd_line = ItmLine++;
            //Det.Iadd_ref_no =;
            //Det.Iadd_seq = ;
            Det.Iadd_stus = 0;
            return Det;
        }
        private InventoryAdhocDetail fillAdhocDet_FixAsset_Approve()
        {
            InventoryAdhocDetail Det = new InventoryAdhocDetail();
            try
            {
                Det.Iadd_anal1 = Convert.ToDecimal(txtQty.Text.Trim());
            }
            catch (Exception ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter valid Qty.");
                return null;
            }

            Det.Iadd_anal2 = txtModel.Text.Trim().ToUpper();
            Det.Iadd_anal3 = txtItmDescription.Text.Trim().ToUpper();
            //Det.Iadd_anal4 = ;
            //Det.Iadd_anal5 =;
            //Det.Iadd_app_itm =;
            //Det.Iadd_app_pb = ;
            //Det.Iadd_app_pb_lvl =;
            //Det.Iadd_app_val = ;
            Det.Iadd_claim_itm = txtItemCD.Text.Trim().ToUpper();
            //Det.Iadd_claim_pb =;
            //Det.Iadd_claim_pb_lvl = ;
            //Det.Iadd_claim_val = ;
            //Det.Iadd_coll_itm =;
            //Det.Iadd_coll_pb = ;
            //Det.Iadd_coll_pb_lvl = ;
            //Det.Iadd_coll_ser1 = ;
            //Det.Iadd_coll_ser2 = ;,
            //Det.Iadd_coll_ser3 = ;
            //Det.Iadd_coll_ser3 = ;
            //Det.Iadd_coll_val = ;
            Det.Iadd_line = ItmLine++;
            //Det.Iadd_ref_no =;
            //Det.Iadd_seq = ;
            Det.Iadd_stus = 0;
            return Det;
        }

        protected void grvItmDes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlStatus.SelectedValue = "Any";

            GridViewRow row = grvItmDes.SelectedRow;
            //string ItmCode = row.Cells[1].Text.Trim();
            SelectedItemCD = row.Cells[1].Text.Trim();

            Label lblStatus = (Label)row.Cells[6].FindControl("lbl_itm_Status");
            Int32 reqStatus = Convert.ToInt32(lblStatus.Text.Trim());

            List<ReptPickSerials> serList = new List<ReptPickSerials>();
            string status = string.Empty;
            serList = CHNLSVC.Inventory.GET_ser_FOR_STATUS(GlbUserComCode, txtSendLoc.Text.Trim().ToUpper(), SelectedItemCD, status);
            AvailableSerialList = new List<ReptPickSerials>();

            if (reqStatus == 0)//only Pending FIX ASSET
            {
                AvailableSerialList = serList;

                string location = txtSendLoc.Text.Trim().ToUpper();
                InventoryAdhocHeader Header;
                det_list_selected = CHNLSVC.Inventory.GET_adhocDET_byRefNo(GlbUserComCode, location, 2, txtRefNo.Text.Trim(), 0, out Header);
            }
            else if (reqStatus == 1 && ddlReuestType.SelectedValue == "2") //Approved FIX ASSET
            {
                //Label lblApprovedSerID = (Label)row.Cells[7].FindControl("lblApprSerID");
                // Int32 ApprSerID = Convert.ToInt32(lblApprovedSerID.Text.Trim());


                var _dup = from _l in AdhodDetList
                           where _l.Iadd_claim_itm == SelectedItemCD && _l.Iadd_stus == reqStatus //&& _l.Iadd_anal4 == ApprSerID.ToString()
                           select _l;

                foreach (InventoryAdhocDetail det in _dup)
                {
                    var _dup2 = from _l in serList
                                where _l.Tus_itm_cd == det.Iadd_claim_itm && _l.Tus_ser_id == Convert.ToInt32(det.Iadd_anal4)
                                select _l;
                    AvailableSerialList.AddRange(_dup2);
                }
                // serList= _dup.ToList<ReptPickSerials>();

            }
            else if (reqStatus == 1 && ddlReuestType.SelectedValue == "1")// Approved FGAP 
            {
                //serList = CHNLSVC.Inventory.GET_ser_FOR_STATUS(GlbUserComCode, txtFgapLoc.Text.Trim().ToUpper(), SelectedItemCD, status);
                AvailableSerialList = serList;
            }
            else
            {
                AvailableSerialList = null;
            }
            grvAvailableSerials.DataSource = AvailableSerialList;
            grvAvailableSerials.DataBind();


        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string status = ddlStatus.SelectedValue;
            List<ReptPickSerials> serList = new List<ReptPickSerials>();
            serList = CHNLSVC.Inventory.GET_ser_FOR_STATUS(GlbUserComCode, txtSendLoc.Text.Trim().ToUpper(), SelectedItemCD, status);
            grvAvailableSerials.DataSource = serList;
            grvAvailableSerials.DataBind();
            AvailableSerialList = serList;

        }

        protected void btnAllIn_Click(object sender, EventArgs e)
        {
            string status = ddlStatus.SelectedValue == "Any" ? string.Empty : ddlStatus.SelectedValue;
            List<ReptPickSerials> serList = new List<ReptPickSerials>();
            serList = CHNLSVC.Inventory.GET_ser_FOR_STATUS(GlbUserComCode, txtSendLoc.Text.Trim().ToUpper(), SelectedItemCD, status);

            Approved_SerialList.AddRange(serList);
            grvAvailableSerials.DataSource = Approved_SerialList;
            grvAvailableSerials.DataBind();

        }

        protected void btnAllBack_Click(object sender, EventArgs e)
        {
            List<ReptPickSerials> serList = new List<ReptPickSerials>();
            Approved_SerialList = new List<ReptPickSerials>();
            grvAvailableSerials.DataSource = Approved_SerialList;
            grvAvailableSerials.DataBind();
        }

        protected void btnIn_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in this.grvAvailableSerials.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("chekSelect1");
                //  Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                if (chkSelect.Checked)
                {
                    Label lblSerID = (Label)gvr.Cells[4].FindControl("lblSerID_av");//Convert.ToInt32(gvr.Cells[4].FindControl("lblSerID_av"));
                    Int32 serID = Convert.ToInt32(lblSerID.Text);
                    string ItemCD = gvr.Cells[1].Text.Trim().ToString();
                    List<ReptPickSerials> serList = new List<ReptPickSerials>();

                    var _dup = from _l in AvailableSerialList
                               where _l.Tus_itm_cd == ItemCD && _l.Tus_ser_id == serID
                               select _l;


                    // serList= _dup.ToList<ReptPickSerials>();
                    Approved_SerialList.AddRange(_dup);

                    AvailableSerialList.RemoveAll(x => x.Tus_itm_cd == ItemCD && x.Tus_ser_id == serID);//&& x.Iadd_anal2 == DelModle
                   
                    
                }

                //serList = List<ReptPickSerials> (_dup);
            }
            grvApproveItms.DataSource = Approved_SerialList;
            grvApproveItms.DataBind();

            grvAvailableSerials.DataSource = AvailableSerialList;
            grvAvailableSerials.DataBind();

           
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in this.grvApproveItms.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("chekSelect2");
                Label lblSerID_appr = (Label)(gvr.Cells[4].FindControl("lblSerID_appr"));
                Int32 serID = Convert.ToInt32(lblSerID_appr.Text);
                string DEL_ItemCD = gvr.Cells[1].Text.Trim().ToString();
                List<ReptPickSerials> serList = new List<ReptPickSerials>();

                var _dup = from _l in Approved_SerialList
                           where _l.Tus_itm_cd == DEL_ItemCD && _l.Tus_ser_id == serID
                           select _l;
                AvailableSerialList.AddRange(_dup);

                Approved_SerialList.RemoveAll(x => x.Tus_itm_cd == DEL_ItemCD && x.Tus_ser_id == serID);//&& x.Iadd_anal2 == DelModle
                //var _dup = from _l in Approved_SerialList
                //           where _l.Tus_itm_cd == ItemCD && _l.Tus_ser_id == serID
                //           select _l;


                //// serList= _dup.ToList<ReptPickSerials>();
                //Approved_SerialList.AddRange(_dup);
                
                //serList = List<ReptPickSerials> (_dup);
            }
            grvApproveItms.DataSource = Approved_SerialList;
            grvApproveItms.DataBind();

            grvAvailableSerials.DataSource = AvailableSerialList;
            grvAvailableSerials.DataBind();
        }

        protected void btn_SendReq_Click(object sender, EventArgs e)
        {
            if (txtRefNo.Text.Trim() == "" && ddlReuestType.SelectedValue=="1")
            {
                txtRefNo.Focus();
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please enter a Reference No!");
                return;
            }
            //Fill Request header
            InventoryAdhocHeader reqHdr = new InventoryAdhocHeader();
            if (ddlReuestType.SelectedValue == "2")
            {
                reqHdr.Iadh_ref_no = txtRefNo.Text.Trim().ToUpper();
                //reqHdr.Iadh_adj_no=
                //reqHdr.Iadh_anal1=
                //reqHdr.Iadh_anal2=
                //reqHdr.Iadh_anal3=
                //reqHdr.Iadh_anal4=
                //reqHdr.Iadh_anal5=
                //reqHdr.Iadh_app_by=
                //reqHdr.Iadh_app_dt=
                //reqHdr.Iadh_coll_by=
                //reqHdr.Iadh_coll_dt=
                reqHdr.Iadh_com = GlbUserComCode;
                reqHdr.Iadh_dt = DateTime.Now.Date;
                reqHdr.Iadh_loc = GlbUserDefLoca;
                reqHdr.Iadh_pc = GlbUserDefProf;
                //reqHdr.Iadh_ref_no=
                reqHdr.Iadh_req_by = GlbUserName;
                reqHdr.Iadh_req_dt = DateTime.Now.Date;
                //reqHdr.Iadh_seq
                reqHdr.Iadh_stus = 0;

                reqHdr.Iadh_tp = Convert.ToInt32(ddlReuestType.SelectedValue);
            }
            else if (ddlReuestType.SelectedValue == "1")
            {
                reqHdr.Iadh_ref_no = txtRefNo.Text.Trim().ToUpper();
                //reqHdr.Iadh_adj_no=
                //reqHdr.Iadh_anal1=
                //reqHdr.Iadh_anal2=
                //reqHdr.Iadh_anal3=
                //reqHdr.Iadh_anal4=
                //reqHdr.Iadh_anal5=
                reqHdr.Iadh_app_by = GlbUserName;
                reqHdr.Iadh_app_dt = DateTime.Now.Date;
                //reqHdr.Iadh_coll_by=
                //reqHdr.Iadh_coll_dt=
                reqHdr.Iadh_com = GlbUserComCode;
                reqHdr.Iadh_dt = DateTime.Now.Date;
                reqHdr.Iadh_loc = txtFgapLoc.Text.ToUpper();
                //reqHdr.Iadh_pc = GlbUserDefProf;
                //reqHdr.Iadh_ref_no=
                //reqHdr.Iadh_req_by = GlbUserName;
                //reqHdr.Iadh_req_dt = DateTime.Now.Date;
                //reqHdr.Iadh_seq
                reqHdr.Iadh_stus = 1;

                reqHdr.Iadh_tp = Convert.ToInt32(ddlReuestType.SelectedValue);
            }
            
            Int32 effect = 0;
            if (AdhodDetList.Count > 0)
            {               
                if (Approved_SerialList == null)
                {
                    Approved_SerialList = new List<ReptPickSerials>();
                }

                string RefNumber = "";
                effect = CHNLSVC.Inventory.Save_Adhoc_Request(reqHdr, AdhodDetList, Approved_SerialList, out RefNumber);  

                if (effect > 0)
                {
                    clearCompleateScreen();
                  
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Sent Successfully! Reference# :" + RefNumber);

                    string Msgg = "<script>alert('Sent Successfully!' );</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);
                   
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Sending Failed!");
                    return;
                }
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "No Items are added. Please add Items first!");
                return;
            }
        }

        protected void ddlReuestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //----------view states--------------------------
            Reset_Session_values();
            //-----------------------------------------------
            txtRefNo.Text = "";

            if (ddlReuestType.SelectedValue == "2")
            {
                txtSendLoc.Text = GlbUserDefLoca;
                divLoc.Visible = true;
                Panel_PriceDet.Visible = false;
                btn_SendReq.Text = "Send Request";
               // div_payment.Visible = false;
                pnlPayss.Enabled = false;
                
            }
            else
            {
                txtSendLoc.Text = "";
                divLoc.Visible = false;
                Panel_PriceDet.Visible = true;
                btn_SendReq.Text = "Approve FGAP";
               // div_payment.Visible = true;
                pnlPayss.Enabled = true;
              
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (Searched_AdhodHeader==null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please send the request first!");
                return;
            }


            string _OrgPC = txtPC.Text.Trim();
            if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, _OrgPC, "INV5"))
            {

            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "No Permission Granted!");
                string Msg = "<script>alert('No Permission Granted!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                return;
            }
            if (Approved_SerialList.Count > 0)
            {
                List<InventoryAdhocDetail> approved_detList = new List<InventoryAdhocDetail>();
                //Approve the requested items.
                InventoryAdhocDetail Det = null;
                foreach (ReptPickSerials rps in Approved_SerialList)
                {
                    #region fill Approved detail

                    Det = new InventoryAdhocDetail();
                    Det.Iadd_anal1 = 1; //not sure

                    Det.Iadd_anal2 = rps.Tus_itm_model;
                    Det.Iadd_anal3 = rps.Tus_itm_desc; ;
                    Det.Iadd_anal4 = rps.Tus_ser_id;
                    //Det.Iadd_anal5 =;
                    Det.Iadd_app_itm = rps.Tus_itm_cd;
                    Det.Iadd_app_pb = txtPriceBook.Text.Trim().ToUpper();
                    Det.Iadd_app_pb_lvl = txtPBLevel.Text.Trim().ToUpper();
                    if (txtUnitPrice.Text.Trim() != "")
                    {
                        Det.Iadd_app_val = Convert.ToDecimal(txtUnitPrice.Text.Trim());
                    }

                    Det.Iadd_claim_itm = rps.Tus_itm_cd;
                    //Det.Iadd_claim_pb =;
                    //Det.Iadd_claim_pb_lvl = ;
                    //Det.Iadd_claim_val = ;
                    //Det.Iadd_coll_itm =;
                    //Det.Iadd_coll_pb = ;
                    //Det.Iadd_coll_pb_lvl = ;
                    //Det.Iadd_coll_ser1 = ;
                    //Det.Iadd_coll_ser2 = ;,
                    //Det.Iadd_coll_ser3 = ;
                    //Det.Iadd_coll_ser3 = ;
                    //Det.Iadd_coll_val = ;
                    Det.Iadd_line = ItmLine++;
                    //Det.Iadd_ref_no =;
                    //Det.Iadd_seq = ;
                    Det.Iadd_stus = 1;
                    #endregion

                    approved_detList.Add(Det);
                }

                //try {
                //Update header
                if (Searched_AdhodHeader==null)
                {
                    Searched_AdhodHeader = new InventoryAdhocHeader();
                }
                
                Searched_AdhodHeader.Iadh_ref_no = txtRefNo.Text.Trim().ToUpper();
                Searched_AdhodHeader.Iadh_app_by = GlbUserName;
                Searched_AdhodHeader.Iadh_app_dt = DateTime.Now.Date;
                Searched_AdhodHeader.Iadh_stus = 1;

                //call approve method
                Int32 effect = 0;
                effect = CHNLSVC.Inventory.Save_Adhoc_Approve(Searched_AdhodHeader, approved_detList);
                if (effect < 0)
                {
                    clearCompleateScreen();
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Failed to Approve. Error occured!");
                    return;
                }
                else if (effect > 0)
                {
                    clearCompleateScreen();
                    
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Approved Successfully!");

                    string Msgg = "<script>alert('Approved Successfully!' );</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);
                }

                //}
                //catch(Exception ex){
                //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Error occured!");
                //    return;
                //}

            }

        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (Searched_AdhodHeader == null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please send the request first!");
                return;
            }
                       
            //Searched_AdhodHeader.Iadh_coll_by = GlbUserName;
            //Searched_AdhodHeader.Iadh_coll_dt = DateTime.Now.Date;
            Searched_AdhodHeader.Iadh_stus = 2;
            Searched_AdhodHeader.Iadh_app_by = GlbUserName; //rejected person
            Searched_AdhodHeader.Iadh_app_dt = DateTime.Now.Date;//rejected date
            Int32 effect = 0;
            effect = CHNLSVC.Inventory.Save_Adhoc_Confirm(Searched_AdhodHeader, null);
            if (effect>0)
            {
                clearCompleateScreen();
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Rejected!");

                string Msgg = "<script>alert('Successfully Rejected!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);
                   return;
            }
        }

        protected void btnRefOk_Click(object sender, EventArgs e)
        {
            if (txtRefNo.Text.Trim() == "")
            {
               
                return;
            }

            Reset_Session_values();
           
            Int32 type_ = Convert.ToInt32(ddlReuestType.SelectedValue);
            string ref_no = txtRefNo.Text.Trim().ToUpper();
            InventoryAdhocHeader Header = new InventoryAdhocHeader();
            List<InventoryAdhocDetail> det_list = null;
            string location = txtSendLoc.Text.Trim().ToUpper();

            //****added on 15-01-2013************************************
            if (type_==1)
            {
                location = GlbUserDefProf;
            }
            if (type_ == 1 && ddlAction.SelectedValue == "Confirmation" )
            {                
                btn_SendReq.Enabled = false;
            }
            else
            {
                btn_SendReq.Enabled = true;
            }
            //**********************************************************
            searchedAdhocDetList = CHNLSVC.Inventory.GET_adhocDET_byRefNo(GlbUserComCode, location, type_, ref_no, 0, out Header);// status must be 0
           
            if (ddlAction.SelectedValue == "Approve")  //if (rdoPending.Checked)
            {
                //det_list = CHNLSVC.Inventory.GET_adhocDET_byRefNo(GlbUserComCode, GlbUserDefLoca, type_, ref_no, 0, out Header);


                det_list = CHNLSVC.Inventory.GET_adhocDET_byRefNo(GlbUserComCode, location, type_, ref_no, 0, out Header);
                if (det_list != null)
                {
                    List<InventoryAdhocDetail> bind_AdhocDetList = new List<InventoryAdhocDetail>();
                    bind_AdhocDetList.AddRange(det_list);
                    var distinctList = bind_AdhocDetList.GroupBy(x => x.Iadd_claim_itm)
                                  .Select(g => g.First())
                                  .ToList();
                    List<InventoryAdhocDetail> bind_AdhocDetList2 = new List<InventoryAdhocDetail>();
                    bind_AdhocDetList2.AddRange(distinctList);

                    grvItmDes.DataSource = bind_AdhocDetList2;
                    grvItmDes.DataBind();

                  
                }
                else 
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "There are no Pending requested items available with this Ref.No!");
                    return; 
                }

            }
            else if (ddlAction.SelectedValue == "Confirmation") //else if (rdoApproved.Checked)
            {
               // det_list = CHNLSVC.Inventory.GET_adhocDET_byRefNo(GlbUserComCode, GlbUserDefLoca, type_, ref_no, 1, out Header);
                det_list = CHNLSVC.Inventory.GET_adhocDET_byRefNo(GlbUserComCode, location, type_, ref_no, 1, out Header);
                if (det_list != null)
                {
                    List<InventoryAdhocDetail> bind_AdhocDetList = new List<InventoryAdhocDetail>();
                    bind_AdhocDetList.AddRange(det_list);
                    var distinctList = bind_AdhocDetList.GroupBy(x => x.Iadd_app_itm)
                                  .Select(g => g.First())
                                  .ToList();
                    List<InventoryAdhocDetail> bind_AdhocDetList2 = new List<InventoryAdhocDetail>();
                    bind_AdhocDetList2.AddRange(distinctList);

                    grvItmDes.DataSource = bind_AdhocDetList2;
                    grvItmDes.DataBind();

                        // det_list = CHNLSVC.Inventory.GET_adhocDET_byRefNo(GlbUserComCode, GlbUserDefLoca, type_, ref_no, 1, out Header);
                   
                   
                   
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "There are no Approved request items available with this Ref.No!");
                    return;
                }
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Select 'Search Status' first!");
                return;
            }


            AdhodDetList = det_list;

            Searched_AdhodHeader = Header;
            if (Header != null)
            {
                txtSendLoc.Text = Header.Iadh_loc;

                btnItmClear.Enabled = false;
                ddlStatus.Enabled = false;
                btnApprove.Enabled = false;
                btnItmAdd.Enabled = false;

                if (Header.Iadh_tp == 1)//FGAP
                {
                    btnItmAdd.Enabled = true;
                    Decimal totalApprovedVal = 0;
                    foreach (InventoryAdhocDetail detail in AdhodDetList)
                    {
                        totalApprovedVal = totalApprovedVal + detail.Iadd_app_val*Convert.ToDecimal(detail.Iadd_anal1);
                    }
                    lblApprVal.Text = string.Format("{0:n2}", totalApprovedVal);
                    lblCollectVal.Text = string.Format("{0:n2}", totalApprovedVal);
                    lblPriceDeference.Text = string.Format("{0:n2}", 0);
                    lblReceiptAmt.Text = "0";
                }
                else //Header.Iadh_tp == 2 //FIX ASSET
                {

                    if (Header.Iadh_stus == 0)
                    {
                        btnApprove.Enabled = true;
                    }
                }
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "There are no pending/approved request with this Ref.No!");
               // txtSendLoc.Text = "";
                btnItmAdd.Enabled = true;
                btnItmClear.Enabled = true;
                ddlStatus.Enabled = true;
                btnApprove.Enabled = true;
            }

        }

        protected void btnItmClear_Click(object sender, EventArgs e)
        {
            txtItemCD.Text = "";
            txtItmDescription.Text = "";
            txtModel.Text = "";
            txtPBLevel.Text = "";
            txtPC.Text = "";
            txtQty.Text = "";
            txtUnitPrice.Text = "";
        }

        protected void imgBtnPriceBook_Click(object sender, ImageClickEventArgs e)
        {
            //MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
            //DataTable dataSource = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBook);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPriceBookData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPriceBook.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgBtnLevelSearch_Click(object sender, ImageClickEventArgs e)
        {
            //PriceLevel
            //MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
            //DataTable dataSource = CHNLSVC.CommonSearch.GetPriceLevelByBookData(MasterCommonSearchUCtrl.SearchParams, null, null);


            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevel);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPriceLevelData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPBLevel.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (Searched_AdhodHeader == null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please send the request first!");
                return;
            }

            if (Approved_SerialList.Count < 1)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please add serials to Confirm List!");
                return;
            }
            List<InventoryAdhocDetail> confirmed_detList = new List<InventoryAdhocDetail>();
            //Approve the requested items.
            InventoryAdhocDetail Det = null;
            foreach (ReptPickSerials rps in Approved_SerialList)
            {
                #region fill Confirm detail

                Det = new InventoryAdhocDetail();
                Det.Iadd_anal1 = 1; //not sure

                Det.Iadd_anal2 = rps.Tus_itm_model;
                Det.Iadd_anal3 = rps.Tus_itm_desc;
                Det.Iadd_anal4 = rps.Tus_ser_id;
                //Det.Iadd_anal5 =;
                Det.Iadd_app_itm = rps.Tus_itm_cd;
                Det.Iadd_app_pb = txtPriceBook.Text.Trim().ToUpper();
                Det.Iadd_app_pb_lvl = txtPBLevel.Text.Trim().ToUpper();
                if (txtUnitPrice.Text.Trim() != "")
                {
                    Det.Iadd_app_val = Convert.ToDecimal(txtUnitPrice.Text.Trim());
                }

                Det.Iadd_claim_itm = rps.Tus_itm_cd;
                //Det.Iadd_claim_pb =;
                //Det.Iadd_claim_pb_lvl = ;
                //Det.Iadd_claim_val = ;
                Det.Iadd_coll_itm = rps.Tus_itm_cd;
                //Det.Iadd_coll_pb = ;
                //Det.Iadd_coll_pb_lvl = ;
                //Det.Iadd_coll_ser1 = ;
                //Det.Iadd_coll_ser2 = ;,
                //Det.Iadd_coll_ser3 = ;
                //Det.Iadd_coll_ser3 = ;
                //Det.Iadd_coll_val = ;
                Det.Iadd_line = ItmLine++;
                //Det.Iadd_ref_no =;
                //Det.Iadd_seq = ;
                Det.Iadd_stus = 3;
                #endregion

                confirmed_detList.Add(Det);
            }

            //***************************
           
            //***************************
            //try {
            //Update header
            // Searched_AdhodHeader.Iadh_ref_no = txtRefNo.Text.Trim().ToUpper();
            // Searched_AdhodHeader.Iadh_app_by = GlbUserName;
            // Searched_AdhodHeader.Iadh_app_dt = DateTime.Now.Date;
            // Searched_AdhodHeader.Iadh_stus = 1;
            Searched_AdhodHeader.Iadh_coll_by = GlbUserName;
            Searched_AdhodHeader.Iadh_coll_dt = DateTime.Now.Date;
            Searched_AdhodHeader.Iadh_stus = 3;

            #region ADJ(-)
            //string AdjNumber = txtAdjustmentNo.Text.Trim();
            //string AdjNumber = "";
            //string manualNum = txtManualRefNo.Text.Trim();
            //string remarks = txtRemarks.Text.Trim();
            //string adj_base = ddlAdjBased.SelectedValue;
            //string adj_sub_type = ddlAdjSubTyepe.SelectedValue;
            //string adj_type = ddlAdjType_.SelectedValue;
            InventoryHeader inHeader = new InventoryHeader();


            inHeader.Ith_acc_no = "";
            inHeader.Ith_anal_1 = "";
            inHeader.Ith_anal_10 = true;
            inHeader.Ith_anal_11 = true;
            inHeader.Ith_anal_12 = true;
            inHeader.Ith_anal_2 = "";
            inHeader.Ith_anal_3 = "";
            inHeader.Ith_anal_4 = "";
            inHeader.Ith_anal_5 = "";
            inHeader.Ith_anal_6 = 0;
            inHeader.Ith_anal_7 = 0;
            inHeader.Ith_anal_8 = DateTime.MinValue;
            inHeader.Ith_anal_9 = DateTime.MinValue;
            inHeader.Ith_bus_entity = "";
            //inHeader.Ith_cate_tp = ddlAdjSubTyepe.SelectedValue.ToString();
            inHeader.Ith_channel = "";
            inHeader.Ith_com = GlbUserComCode;

            //inHeader.Ith_com ="";
            inHeader.Ith_com_docno = "";
            inHeader.Ith_cre_by = "";
            inHeader.Ith_cre_when = DateTime.MinValue;
            inHeader.Ith_del_add1 = "";
            inHeader.Ith_del_add2 = "";
            inHeader.Ith_del_code = "";

            inHeader.Ith_del_party = "";
            inHeader.Ith_del_town = "";


            inHeader.Ith_direct = true;
            //  inHeader.Ith_direct =true;
            inHeader.Ith_doc_date = DateTime.Today;
            //  inHeader.Ith_doc_date  =DateTime.MinValue;
            inHeader.Ith_doc_no = "";//"DPS32-ADJ-12-588888";

            inHeader.Ith_doc_tp = "ADJ";
            //   inHeader.Ith_doc_tp ="";
            inHeader.Ith_doc_year = DateTime.Today.Year;
            inHeader.Ith_entry_no = "";
            inHeader.Ith_entry_tp = "";
            inHeader.Ith_git_close = true;
            inHeader.Ith_git_close_date = DateTime.MinValue;
            inHeader.Ith_git_close_doc = "";
            inHeader.Ith_isprinted = true;
            inHeader.Ith_is_manual = true;
            inHeader.Ith_job_no = "";
            inHeader.Ith_loading_point = "";
            inHeader.Ith_loading_user = "";
            inHeader.Ith_loc = GlbUserDefLoca;
            //if (txtManualRefNo.Text == null || txtManualRefNo.Text == "")
            //{
            //    inHeader.Ith_manual_ref = "N/A";
            //}
            //else
            //{
            //    inHeader.Ith_manual_ref = txtManualRefNo.Text.Trim();
            //}
            inHeader.Ith_manual_ref = "N/A";
            // inHeader.Ith_manual_ref ="";
            inHeader.Ith_mod_by = GlbUserName;//"ADMIN";
            inHeader.Ith_mod_when = DateTime.MinValue;
            inHeader.Ith_noofcopies = 1;
            inHeader.Ith_oth_loc = "";

            inHeader.Ith_remarks = "ADHOC Confirm";//txtRemarks.Text;
            // inHeader.Ith_remarks ="";
            inHeader.Ith_sbu = "INV";
            inHeader.Ith_seq_no = 6;
            //inHeader.Ith_seq_no =54;
            inHeader.Ith_session_id = GlbUserSessionID;
            inHeader.Ith_stus = "A";
            inHeader.Ith_sub_tp = (CommonUIDefiniton.AdjustmentType.ADHOC).ToString(); //ddlAdjSubTyepe.SelectedValue.ToString();
            // inHeader.Ith_sub_tp ="";
            inHeader.Ith_vehi_no = "";

            inHeader.Ith_direct = false;
            //*********************************
            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_cd = GlbUserDefLoca;
            masterAuto.Aut_cate_tp = "LOC";
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "ADJ";
            masterAuto.Aut_number = 5;//what is Aut_number
            masterAuto.Aut_start_char = "ADJ";
            masterAuto.Aut_year = null;
            //  masterAuto.Aut_year = Convert.ToDateTime(txtDate_.Text).Year;

            #endregion
            //------------------------------------------------------------
            if (Searched_AdhodHeader.Iadh_tp == 2)
            {
                #region
                string location = txtSendLoc.Text.Trim().ToUpper();
                InventoryAdhocHeader Header;
                List<InventoryAdhocDetail> det_list = CHNLSVC.Inventory.GET_adhocDET_byRefNo(GlbUserComCode, location, 2, txtRefNo.Text.Trim(), 0, out Header);

                foreach(InventoryAdhocDetail invdet in AdhodDetList)
                {
                    var _dup = from _l in confirmed_detList
                               where _l.Iadd_claim_itm == invdet.Iadd_app_itm//&& _l.Iadd_anal4 == ApprSerID.ToString()
                               select _l;
                    Decimal count_CONF = _dup.Count();

                    Decimal count_REQ = 0;

                    //var _dup2 = from _l in det_list
                    //            where _l.Iadd_claim_itm == invdet.Iadd_claim_itm//&& _l.Iadd_anal4 == ApprSerID.ToString()
                    //            select _l;
                    //count_REQ = _dup2.Count();
                    foreach( InventoryAdhocDetail req_det in searchedAdhocDetList)
                    {
                        if (req_det.Iadd_claim_itm == invdet.Iadd_app_itm )
                        {
                            count_REQ = req_det.Iadd_anal1;
                        }
                    }
                  

                    if (count_CONF > count_REQ)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cannot Exceed the request item Qty.!");
                        return;
                    }
                }
               

                //call confirm method                

                string AdjNumber = string.Empty;
                List<ReptPickSerialsSub> rps_subList = new List<ReptPickSerialsSub>();
                Int16 adjEffect = CHNLSVC.Inventory.ADJMinus(inHeader, Approved_SerialList, rps_subList, masterAuto, out AdjNumber);
                Searched_AdhodHeader.Iadh_adj_no = AdjNumber;

                Int32 effect = 0;
                effect = CHNLSVC.Inventory.Save_Adhoc_Confirm(Searched_AdhodHeader, confirmed_detList);
                if (effect < 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Failed to Confirmed. Error occured. Try Again!");
                   
                }
                else if (effect > 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Confirmed Successfully!  ADJ(-) NO. =" + AdjNumber );

                    clearCompleateScreen();
                  
                    string Msgg = "<script>alert('Confirmed Successfully! ADJ(-) NO. = '" + AdjNumber + " );</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);
                    return;
                }

                //}
                //catch(Exception ex){
                //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Error occured!");
                //    return;
                //}
                #endregion


            }
            if (Searched_AdhodHeader.Iadh_tp == 1)
            {
                if (lblReceiptAmt.Text != "0")
                {
                    if (Convert.ToDecimal(lblReceiptAmt.Text) > 0)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please do payments!");
                        //btnConfirm.Enabled = false;
                        return;
                    }
                }
                else
                {
                    //call confirm method


                    string AdjNumber = string.Empty;
                    List<ReptPickSerialsSub> rps_subList = new List<ReptPickSerialsSub>();
                    Int16 adjEffect = CHNLSVC.Inventory.ADJMinus(inHeader, Approved_SerialList, rps_subList, masterAuto, out AdjNumber);
                    Searched_AdhodHeader.Iadh_adj_no = AdjNumber;

                    //added for printing
                    Gen_ADJ_DocNo = AdjNumber;

                    Int32 effect = 0;
                    effect = CHNLSVC.Inventory.Save_Adhoc_Confirm(Searched_AdhodHeader, confirmed_detList);
                    if (effect < 0)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Failed to Confirmed. Error occured. Try Again!");
                        return;
                    }
                    else if (effect > 0)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Confirmed Successfully! ADJ(-) NO. = " + AdjNumber);

                        clearCompleateScreen();
                        
                        string Msgg = "<script>alert('Confirmed Successfully! ADJ(-) NO. = '" + AdjNumber + " );</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);
                    }

                }
                
            }
        }

        protected void grvItmDes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label ReqStatus = (Label)e.Row.FindControl("lbl_itm_Status");
                if (ddlReuestType.SelectedValue == "2" && ReqStatus.Text == "1")
                {
                    ImageButton btnDelete = (ImageButton)e.Row.FindControl("imgBtnDeleteItm");
                    btnDelete.Enabled = false;
                }
                if (ddlAction.SelectedValue != "Request" && searchedAdhocDetList!=null)
                {
                    string ItemCode = e.Row.Cells[1].Text;
                    foreach (InventoryAdhocDetail det in searchedAdhocDetList)
                    {
                        if (det.Iadd_claim_itm == ItemCode)
                        {
                            e.Row.Cells[4].Text = det.Iadd_anal1.ToString();
                        }
                    }
                    //searchedAdhocDetList
                }
                
            }            
        }

        protected void grvItmDes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
           // List<InventoryAdhocDetail> AdhodDetList
            Int32 rowIndex = e.RowIndex;
             string DelItemCD = grvItmDes.Rows[rowIndex].Cells[1].Text;
             string DelModle = grvItmDes.Rows[rowIndex].Cells[2].Text;

            // AdhodDetList.RemoveAll(x => x.Iadd_app_itm == DelItemCD );//&& x.Iadd_anal2 == DelModle
             if (ddlAction.SelectedValue == "Request")
             {
                 AdhodDetList.RemoveAll(x => x.Iadd_claim_itm == DelItemCD);//&& x.Iadd_anal2 == DelModle
             }
             else
             {
                 AdhodDetList.RemoveAll(x => x.Iadd_app_itm == DelItemCD);//&& x.Iadd_anal2 == DelModle
             }
            
             grvItmDes.DataSource = AdhodDetList;
            grvItmDes.DataBind();
            //     Label lblSEQ = (Label)(grvResctrict.Rows[rowIndex].FindControl("lblSEQno"));
            //Int32 SEQ = Convert.ToInt32(lblSEQ.Text);
            //AccRestrList.RemoveAll(x => x.Hrs_seq == SEQ);
            //grvResctrict.DataSource = AccRestrList;
            //grvResctrict.DataBind();
        }

        protected void PaymentType_LostFocus(object sender, EventArgs e)
        {
            //-------------------------------
            divCardDet.Visible = false;
            divCRDno.Visible = false;
            divChequeNum.Visible = false;
            divCredit.Visible = false;
            divAdvReceipt.Visible = false;
            divCreditCard.Visible = false;
            divBankDet.Visible = false;
            //-------------------------------

            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) return;


            List<PaymentTypeRef> _case = CHNLSVC.Sales.GetAllPaymentType(GlbUserComCode, GlbUserDefProf, ddlPayMode.SelectedValue.ToString());
            PaymentTypeRef _type = null;
            if (_case != null)
            {
                if (_case.Count > 0)
                    _type = _case[0];
            }
            else
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Payment types are not properly setup!");
                return;
            }
            if (_type.Sapt_cd == null) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid payment type"); return; }
            if (_type.Sapt_is_settle_bank == true)
            {
                divCredit.Visible = true; divAdvReceipt.Visible = false;
            }
            else if (_type.Sapt_cd == "ADVAN" || _type.Sapt_cd == "CRNOTE")
            {
                //divCredit.Visible = false; 
                divAdvReceipt.Visible = true;
            }
            else
            {
                //divCredit.Visible = false; 
                //divAdvReceipt.Visible = false;

            }
            if (ddlPayMode.SelectedValue == "CHEQUE")
            {
                //divCRDno.Visible = false;
                divChequeNum.Visible = true;
                divBankDet.Visible = true;
            }
            else
            {
                //divChequeNum.Visible = false;
                //  divCRDno.Visible = true;
            }
            if (ddlPayMode.SelectedValue == "CRCD")
            {
                divCRDno.Visible = true;
                divCardDet.Visible = true;
                divCreditCard.Visible = true;
                divBankDet.Visible = true;
            }
            if (ddlPayMode.SelectedValue == "DEBT")
            {
                divCRDno.Visible = true;
                divCardDet.Visible = false;
                divCreditCard.Visible = true;
                divBankDet.Visible = true;
            }
            
       
            Decimal BankOrOtherCharge = 0;
            BankOrOther_Charges = 0;
            AmtToPayForFinishPayment = 0;
           // BalanceAmount= Convert.ToDecimal(lblReceiptAmt.Text)-Convert.ToDecimal(lblPayBalance.Text);//reciept amount
            try {
                BalanceAmount = Convert.ToDecimal(lblReceiptAmt.Text) - Convert.ToDecimal(lblPayBalance.Text);//reciept amount
            }
            catch(Exception ex)
            {
                return;
            }
            foreach (PaymentType pt in PaymentTypes)
            {
                if (ddlPayMode.SelectedValue == pt.Stp_pay_tp)
                {
                    Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                    BankOrOtherCharge = BalanceAmount * BCR / 100;

                    Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                    BankOrOtherCharge = BankOrOtherCharge + BCV;

                    BankOrOther_Charges = BankOrOtherCharge;
                }
            }

            //-----------------**********
            AmtToPayForFinishPayment = (BankOrOtherCharge + BalanceAmount);
            txtPayAmount.Text = AmtToPayForFinishPayment.ToString();

            //-----------------**********
            txtPayAmount.Focus();

            //---------------

            txtPayRemarks.Text = "";
            txtPayCrCardNo.Text = "";
            txtPayCrBank.Text = "";
            txtPayCrBranch.Text = "";
            txtPayCrCardType.Text = "";
            txtPayCrExpiryDate.Text = "";
            chkPayCrPromotion.Checked = false;
            txtPayCrPeriod.Text = "";
            txtPayAdvReceiptNo.Text = "";
            txtPayCrBatchNo.Text = "";
        }

        protected void ddl_Location_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        protected void BindPaymentType(DropDownList _ddl)
        {
            //try {
            //   DateTime receiptDT= Convert.ToDateTime(txtReceiptDate.Text).Date;
            //}
            //catch(Exception ex){
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter Receipt date!");
            //    return;
            //}
            _ddl.Items.Clear();
            //  List<PaymentTypeRef> _paymentTypeRef = CHNLSVC.Sales.GetAllPaymentType(string.Empty);
            //   List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(ddl_Location.SelectedValue.Trim(), "HPR", Convert.ToDateTime(txtReceiptDate.Text).Date);
            List<PaymentType> _paymentTypeRef = CHNLSVC.Sales.GetPossiblePaymentTypes(GlbUserDefProf, "HPR", DateTime.Now.Date);
            // _ddl.DataSource = _paymentTypeRef.OrderBy(items => items.Sapt_cd);
            //_ddl.DataTextField = "Sapt_cd";
            //_ddl.DataValueField = "Sapt_cd";
            List<string> payTypes = new List<string>();
            payTypes.Add("");
            if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
            {
                foreach (PaymentType pt in _paymentTypeRef)
                {
                    payTypes.Add(pt.Stp_pay_tp);
                }
            }
            _ddl.DataSource = payTypes;
            _ddl.DataBind();



        }
        protected void AddPayment(object sender, EventArgs e)
        {
            try
            {
                Decimal d = Convert.ToDecimal(txtPayAmount.Text.Trim());
            }
            catch (Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter valid amount!");
                return;
            }
            //Decimal sum_receipt_amt = 0;
            //foreach (GridViewRow gvr in this.gvReceipts.Rows)
            //{
            //    //CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
            //    //  Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
            //    Decimal amt = Convert.ToDecimal(gvr.Cells[3].Text.Trim());
            //    sum_receipt_amt = sum_receipt_amt + amt;
            //}
            Decimal sum_receipt_amt = Convert.ToDecimal(lblReceiptAmt.Text);
            Decimal BankOrOtherCharge_ = 0;
            if (AmtToPayForFinishPayment != Convert.ToDecimal(txtPayAmount.Text))
            {
                foreach (PaymentType pt in PaymentTypes)
                {
                    if (ddlPayMode.SelectedValue == pt.Stp_pay_tp)
                    {
                        Decimal BCV = Convert.ToDecimal(pt.Stp_bank_chg_val);
                        //   Convert.ToDecimal(txtPayAmount.Text) - BCV;
                        Decimal BCR = Convert.ToDecimal(pt.Stp_bank_chg_rt);
                        BankOrOtherCharge_ = (Convert.ToDecimal(txtPayAmount.Text) - BCV) * BCR / (BCR + 100);


                        BankOrOtherCharge_ = BankOrOtherCharge_ + BCV;


                        BankOrOther_Charges = BankOrOtherCharge_;
                    }
                }
            }

            if ((PaidAmount + Convert.ToDecimal(txtPayAmount.Text.Trim()) - BankOrOther_Charges) > Math.Round(sum_receipt_amt, 2))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot Exceed Receipt Total Amount ");
                return;
            }
            Decimal bankorother = BankOrOther_Charges;
            AddPayment();
            set_PaidAmount();
            set_BalanceAmount();
        }
        private void set_PaidAmount()
        {
            PaidAmount = 0;
            if (gvPayment.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in this.gvPayment.Rows)
                {
                    //CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
                    //  Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                    Decimal amt = Convert.ToDecimal(gvr.Cells[18].Text.Trim());
                    PaidAmount = PaidAmount + amt;
                }
            }
            lblPayPaid.Text = PaidAmount.ToString();

        }
        private void set_BalanceAmount()
        {
            //BalanceAmount = 0;
            //Decimal receiptAmt = 0;
            //if (gvReceipts.Rows.Count > 0)
            //{
            //    foreach (GridViewRow gvr in this.gvReceipts.Rows)
            //    {
            //        //CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
            //        //  Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
            //        Decimal amt = Convert.ToDecimal(gvr.Cells[3].Text.Trim());
            //        receiptAmt = receiptAmt + amt;
            //    }
            //    BalanceAmount = receiptAmt - PaidAmount;
            //}
            //lblPayBalance.Text = BalanceAmount.ToString();
            BalanceAmount = Convert.ToDecimal(lblReceiptAmt.Text) - PaidAmount;
            lblPayBalance.Text = BalanceAmount.ToString();
        }
        private void AddPayment()
        {
            if (_recieptItem == null || _recieptItem.Count == 0)
            {
                _recieptItem = new List<RecieptItem>();
            }

            if (string.IsNullOrEmpty(ddlPayMode.SelectedValue.ToString())) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid payment type"); return; }
            if (string.IsNullOrEmpty(txtPayAmount.Text)) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount"); return; }
            if (Convert.ToDecimal(txtPayAmount.Text) <= 0) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount"); return; }



            Int32 _period = 0;
            decimal _payAmount = 0;
            if (chkPayCrPromotion.Checked)
            {
                if (string.IsNullOrEmpty(txtPayCrPeriod.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid period");
                    return;
                }
                if (!string.IsNullOrEmpty(txtPayCrPeriod.Text))
                {
                    try
                    {
                        if (Convert.ToInt32(txtPayCrPeriod.Text) <= 0)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid period");
                            return;
                        }
                    }
                    catch
                    {
                        _period = 0;
                    }
                }
            }

            if (string.IsNullOrEmpty(txtPayCrPeriod.Text)) _period = 0;
            else _period = Convert.ToInt32(txtPayCrPeriod.Text);


            if (string.IsNullOrEmpty(txtPayAmount.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount");
                return;
            }
            else
            {
                try
                {
                    if (Convert.ToDecimal(txtPayAmount.Text) <= 0)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid pay amount");
                        return;
                    }
                }
                catch
                {
                    _payAmount = 0;
                }
            }


            //  _payAmount = Convert.ToDecimal(txtPayAmount.Text);
            _payAmount = Convert.ToDecimal(txtPayAmount.Text) - BankOrOther_Charges;

            //if (_recieptItem.Count <= 0)
            //{
            RecieptItem _item = new RecieptItem();
            if (!string.IsNullOrEmpty(txtPayCrExpiryDate.Text))
            { _item.Sard_cc_expiry_dt = Convert.ToDateTime(txtPayCrExpiryDate.Text).Date; }

            string _cardno = string.Empty;
            //if (ddlPayMode.SelectedValue.ToString() == "CRCD" || ddlPayMode.SelectedValue.ToString() == "CHEQUE") _cardno = txtPayCrCardNo.Text.Trim();
            if (ddlPayMode.SelectedValue.ToString() == "CRCD")
            {
                if (txtPayCrCardNo.Text.Trim() == "" || txtPayCrCardType.Text.Trim() == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please Enter all the Card Details.");
                    return;
                }
                _cardno = txtPayCrCardNo.Text.Trim();
                _item.Sard_chq_bank_cd = _cardno;


            }
            if (ddlPayMode.SelectedValue.ToString() == "ADVAN" || ddlPayMode.SelectedValue.ToString() == "LORE" || ddlPayMode.SelectedValue.ToString() == "CRNOTE" || ddlPayMode.SelectedValue.ToString() == "GVO" || ddlPayMode.SelectedValue.ToString() == "GVS")
            { _cardno = txtPayAdvReceiptNo.Text; _item.Sard_chq_bank_cd = txtPayCrBank.Text; }
            if (ddlPayMode.SelectedValue.ToString() == "CHEQUE")
            {
                if (txtChequeNo.Text.Trim() == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please Enter all the Cheque Details.");
                    return;
                }
                //--------------------------------------
                if (txtPayCrBank.Text.Trim() == "" || txtPayCrBranch.Text.Trim() == "")
                {

                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please Enter Bank Details.");
                    return;
                }
                //validate bank and branch.
                Boolean valid = CHNLSVC.Sales.validateBank_and_Branch(txtPayCrBank.Text.Trim(), txtPayCrBranch.Text.Trim(), "BANK");
                if (valid == false)
                {

                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "INVALID BANK DETAILS!");
                    return;
                }

                //---------------------------------------
                _cardno = txtChequeNo.Text.Trim();
                //_item.Sard_chq_bank_cd = _cardno;
                _item.Sard_ref_no = _cardno;
            }


            if (ddlPayMode.SelectedValue.ToString() == "DEBT" || ddlPayMode.SelectedValue.ToString() == "CRCD")
            {
                if (txtPayCrBank.Text.Trim() == "")
                {

                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please Enter Bank!");
                    return;
                }
                //validate bank and branch.
                Boolean valid = CHNLSVC.Sales.validateBank_and_Branch(txtPayCrBank.Text.Trim(), null, "BANK");
                if (valid == false)
                {

                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "INVALID BANK !");
                    return;
                }

            }
            _item.Sard_cc_is_promo = chkPayCrPromotion.Checked ? true : false;
            _item.Sard_cc_period = _period;
            _item.Sard_cc_tp = txtPayCrCardType.Text;
            _item.Sard_chq_bank_cd = txtPayCrBank.Text;
            _item.Sard_chq_branch = txtPayCrBranch.Text;
            _item.Sard_credit_card_bank = null;
            _item.Sard_deposit_bank_cd = null;
            _item.Sard_deposit_branch = null;
            _item.Sard_pay_tp = ddlPayMode.SelectedValue.ToString();
            _item.Sard_settle_amt = Convert.ToDecimal(_payAmount);
            _item.Sard_anal_3 = BankOrOther_Charges;
            // _paidAmount += _payAmount;

            _item.Sard_receipt_no = "";//To be filled when saving.

            _item.Sard_ref_no = _cardno;

            _recieptItem.Add(_item);


            gvPayment.DataSource = gvPayment.DataSource = _recieptItem; ;
            gvPayment.DataBind();

            clearPaymetnScreen();

        }
        private void clearPaymetnScreen()
        {
            txtPayAmount.Text = "";
            ddlPayMode.SelectedIndex = 0;
            txtPayRemarks.Text = "";
            txtPayCrCardNo.Text = "";
            txtPayCrBank.Text = "";
            txtPayCrBranch.Text = "";
            txtPayCrCardType.Text = "";
            txtPayCrExpiryDate.Text = "";
            chkPayCrPromotion.Checked = false;
            txtPayCrPeriod.Text = "";
            txtPayAdvReceiptNo.Text = "";
            txtPayCrBatchNo.Text = "";
        }
        protected void BindReceiptItem()
        {
            DataTable _table = CHNLSVC.Sales.GetReceiptItemTable(string.Empty);
            if (_table.Rows.Count <= 0)
            {
                //_table = SetEmptyRow(_table);
                gvPayment.DataSource = _table;
            }
            else
            {
                gvPayment.DataSource = CHNLSVC.Sales.GetReceiptItemList(string.Empty);

            }
            gvPayment.DataBind();
        }
        protected void ImgBtnBankSearch_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void gvPayment_OnDelete(object sender, GridViewDeleteEventArgs e)
        {
            if (_recieptItem == null || _recieptItem.Count == 0) return;

            int row_id = e.RowIndex;
            string _payType = (string)gvPayment.DataKeys[row_id][0];
            decimal _settleAmount = (decimal)gvPayment.DataKeys[row_id][1];

            List<RecieptItem> _temp = new List<RecieptItem>();
            _temp = _recieptItem;


            _temp.RemoveAll(x => x.Sard_pay_tp == _payType && x.Sard_settle_amt == _settleAmount);
            _recieptItem = _temp;

            gvPayment.DataSource = gvPayment.DataSource = _recieptItem; ;
            gvPayment.DataBind();

            set_PaidAmount();
            set_BalanceAmount();
        }

        protected void grvAvailableSerials_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void fill_Transactions(RecieptHeader r_hdr)
        {
            //(to write to hpt_txn)
            HpTransaction tr = new HpTransaction();
           // tr.Hpt_acc_no = lblAccNo.Text.Trim();
            tr.Hpt_ars = 0;
            tr.Hpt_bal = 0;
            tr.Hpt_crdt = r_hdr.Sar_tot_settle_amt;//amount
            tr.Hpt_cre_by = GlbUserName;
            tr.Hpt_cre_dt = DateTime.Now.Date;
            tr.Hpt_dbt = 0;
            tr.Hpt_com = GlbUserComCode;
            tr.Hpt_pc = GlbUserDefProf;
            if (r_hdr.Sar_is_oth_shop == true)
            {
                tr.Hpt_desc = ("Other shop collection").ToUpper() + "-" + GlbUserDefProf; ;
                tr.Hpt_mnl_ref = r_hdr.Sar_prefix + "-" + r_hdr.Sar_manual_ref_no;//+"-"+GlbUserDefProf;   //"prefix-receiptNo-pc"

            }
            else
            {
                tr.Hpt_desc = ("Payment receive").ToUpper();

            }
            if (r_hdr.Sar_is_mgr_iss)
            {
                //"prefix-receiptNo-issues"
                //tr.Hpt_mnl_ref = r_hdr.Sar_prefix + "-" + r_hdr.Sar_manual_ref_no + (" issues").ToUpper();

            }
            else
            { //"prefix-receiptNo"
                //tr.Hpt_mnl_ref = r_hdr.Sar_prefix + "-" + r_hdr.Sar_manual_ref_no;
            }
            tr.Hpt_pc = GlbUserDefProf;

            tr.Hpt_ref_no = "";
            tr.Hpt_txn_dt = DateTime.Now.Date;
            tr.Hpt_txn_ref = "";
            tr.Hpt_txn_tp = r_hdr.Sar_receipt_type;
            tr.Hpt_ref_no = r_hdr.Sar_seq_no.ToString();

            if (Transaction_List==null)
            {
                Transaction_List = new List<HpTransaction>();
            }
            Transaction_List.Add(tr);

        }
        protected void Button10_Click(object sender, EventArgs e)
        {
            if (BalanceAmount>0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Payment is not done!");
                return;
            }
            if (_recieptItem==null)
            {
                return;
            }
            if (Approved_SerialList==null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please add serials to Selected list!");
                return;
            }
            if (Approved_SerialList.Count<1)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please add serials to Selected list!");
                return;

            }
            //Decimal totalApprovedVal = 0;
            //foreach (InventoryAdhocDetail detail in AdhodDetList)
            //{
            //    totalApprovedVal = totalApprovedVal + detail.Iadd_app_val * Convert.ToDecimal(detail.Iadd_anal1);
            //}
            //if (totalApprovedVal != Convert.ToDecimal(lblCollectVal.Text))
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please add serials to Selected List!");
            //    return;             
            //}

            //*****************************************************************************************************************
            //Decimal count=0;
            //foreach (DataRow dr in grvItmDes.Rows)
            //{
            //    count = count + Convert.ToDecimal(dr["Iadd_anal1"].ToString());
            //}
            //Decimal count2 = grvApproveItms.Rows.Count;
            //if (count != count2)
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please add serials to Selected list!");
            //    return;
            //}
                //if (BalanceAmount <= 0 )
            //{

            #region Receipt Header Value Assign
                RecieptHeader _recHeader = new RecieptHeader();
            // _recHeader.Sar_acc_no = "";//////////////////////TODO
            //_recHeader.Sar_acc_no = lblAccNo.Text;

            _recHeader.Sar_act = true;
            _recHeader.Sar_com_cd = GlbUserComCode;
            _recHeader.Sar_comm_amt = 0;
            _recHeader.Sar_create_by = GlbUserName;
            _recHeader.Sar_create_when = DateTime.Now;
            //_recHeader.Sar_currency_cd = txtCurrency.Text;
            //_recHeader.Sar_debtor_add_1 = txtAddress.Text;
            //_recHeader.Sar_debtor_add_2 = txtAddress.Text;
            //_recHeader.Sar_debtor_cd = txtCustomer.Text;
            //_recHeader.Sar_debtor_name = txtCusName.Text;
            _recHeader.Sar_direct = true;
            _recHeader.Sar_direct_deposit_bank_cd = "";
            _recHeader.Sar_direct_deposit_branch = "";
            _recHeader.Sar_epf_rate = 0;
            _recHeader.Sar_esd_rate = 0;
            //if (rdoBtnManager.Checked)
            //{
            //    _recHeader.Sar_is_mgr_iss = true;
            //}
            //else { _recHeader.Sar_is_mgr_iss = false; }
            _recHeader.Sar_is_oth_shop = false;// Not sure!
            //_recHeader.Sar_is_oth_shop = false;//////////////////////TODO
            //if (GlbUserDefProf != ddl_Location.SelectedValue)
            //{
            //    _recHeader.Sar_is_oth_shop = true;// Not sure!
            //    _recHeader.Sar_remarks = "OTHER SHOP COLLECTION-" + ddl_Location.SelectedValue;
            //    _recHeader.Sar_oth_sr = ddl_Location.SelectedValue;
            //}
            //else
            //{
            //    _recHeader.Sar_is_oth_shop = false; // Not sure!
            //    _recHeader.Sar_remarks = "COLLECTION";
            //}

            _recHeader.Sar_is_used = false;//////////////////////TODO
            //_recHeader.Sar_manual_ref_no = txtManualRefNo.Text;
            //_recHeader.Sar_mob_no = txtMobile.Text;
            _recHeader.Sar_mod_by = GlbUserName;
            _recHeader.Sar_mod_when = DateTime.Now;
            //_recHeader.Sar_nic_no = txtNIC.Text;
          

            //  _recHeader.Sar_prefix = "PRFX";//////////////////////TODO
            //_recHeader.Sar_prefix = ddlPrefix.SelectedValue;

            _recHeader.Sar_profit_center_cd = GlbUserDefProf;

            //_recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);//////////////////////TODO
            _recHeader.Sar_receipt_date = DateTime.Now.Date;//Convert.ToDateTime(txtReceiptDate.Text.Trim()).Date;

            //_recHeader.Sar_receipt_no = "na";/////////////////////TODO
            //  _recHeader.Sar_receipt_no = txtReceiptNo.Text;

           // _recHeader.Sar_manual_ref_no = txtReceiptNo.Text; //the receipt no
            //_recHeader.Sar_receipt_type = txtInvType.Text;
            //if (rdoBtnManual.Checked)
            //{
            //    _recHeader.Sar_receipt_type = "HPRM";
            //}
            //else { _recHeader.Sar_receipt_type = "HPRS"; }
            _recHeader.Sar_receipt_type = "FGAP";
            _recHeader.Sar_ref_doc = "";
            _recHeader.Sar_remarks = "FGAP RECEIPT";
            _recHeader.Sar_seq_no = 1;
            _recHeader.Sar_ser_job_no = "";
            _recHeader.Sar_session_id = GlbUserSessionID;
            //_recHeader.Sar_tel_no = txtMobile.Text;

            //  _recHeader.Sar_tot_settle_amt = 0;///////////////////////TODO sum_receipt_amt
            _recHeader.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(lblReceiptAmt.Text), 2);

            _recHeader.Sar_uploaded_to_finance = false;
            _recHeader.Sar_used_amt = 0;//////////////////////TODO
            _recHeader.Sar_wht_rate = 0;

            //_recHeader.Sar_anal_5 = uc_HpAccountSummary1.Uc_Inst_CommRate;
            //_recHeader.Sar_comm_amt = (uc_HpAccountSummary1.Uc_Inst_CommRate * _recHeader.Sar_tot_settle_amt / 100);

            //_recHeader.Sar_anal_6 = uc_HpAccountSummary1.Uc_AdditonalCommisionRate;

                //  _recHeader.Sar_anal_7 = (uc_HpAccountSummary1.Uc_AdditonalCommisionRate * _recHeader.Sar_tot_settle_amt / 100);
            #endregion
            #region Receipt Details creation
                List<RecieptHeader> receiptHeaderList = new List<RecieptHeader>();
               // receiptHeaderList = Receipt_List;
                receiptHeaderList.Add(_recHeader);
                List<RecieptItem> receipItemList = new List<RecieptItem>();
                receipItemList = _recieptItem;
                List<RecieptItem> save_receipItemList = new List<RecieptItem>();
                List<RecieptItem> finish_receipItemList = new List<RecieptItem>();
                Int32 tempHdrSeq = 0;
                foreach (RecieptHeader _h in receiptHeaderList)
                {
                    _h.Sar_seq_no = tempHdrSeq;// Sar_seq_no is changed when saving records
                    fill_Transactions(_h);
                    tempHdrSeq--;
                    Decimal orginal_HeaderTotAmt = _h.Sar_tot_settle_amt;
                    foreach (RecieptItem _i in receipItemList)
                    {
                        if (_i.Sard_settle_amt <= _h.Sar_tot_settle_amt && _i.Sard_settle_amt != 0)
                        {
                            // _i.Sard_receipt_no = _h.Sar_manual_ref_no;
                            //  save_receipItemList.Add(_i);
                            // finish_receipItemList.Add(_i);
                            RecieptItem ri = new RecieptItem();
                            //ri = _i;
                            ri.Sard_settle_amt = _i.Sard_settle_amt;
                            ri.Sard_pay_tp = _i.Sard_pay_tp;
                            ri.Sard_receipt_no = _h.Sar_manual_ref_no;//when saving  ri.Sard_receipt_no is changed 
                            ri.Sard_seq_no = _h.Sar_seq_no;
                            //-------------------------------    //have to copy all properties.
                            ri.Sard_cc_expiry_dt = _i.Sard_cc_expiry_dt;
                            ri.Sard_cc_is_promo = _i.Sard_cc_is_promo;
                            ri.Sard_cc_period = _i.Sard_cc_period;
                            ri.Sard_cc_tp = _i.Sard_cc_tp;
                            ri.Sard_chq_bank_cd = _i.Sard_chq_bank_cd;
                            ri.Sard_chq_branch = _i.Sard_chq_branch;
                            ri.Sard_credit_card_bank = _i.Sard_credit_card_bank;
                            ri.Sard_deposit_bank_cd = _i.Sard_deposit_bank_cd;
                            ri.Sard_deposit_branch = _i.Sard_deposit_branch;
                            //--------------------------------
                            ri.Sard_ref_no = _i.Sard_ref_no;

                            //********
                            ri.Sard_anal_3 = _i.Sard_anal_3;
                            //--------------------------------
                            save_receipItemList.Add(ri);

                            _h.Sar_tot_settle_amt = _h.Sar_tot_settle_amt - _i.Sard_settle_amt;
                            _i.Sard_settle_amt = 0;

                        }
                        else if (_h.Sar_tot_settle_amt != 0 && _i.Sard_settle_amt != 0)
                        {
                            RecieptItem ri = new RecieptItem();
                            ri.Sard_receipt_no = _h.Sar_manual_ref_no;//when saving  ri.Sard_receipt_no is changed 
                            ri.Sard_settle_amt = _h.Sar_tot_settle_amt;//equals to the header's amount.
                            ri.Sard_pay_tp = _i.Sard_pay_tp;
                            ri.Sard_seq_no = _h.Sar_seq_no;
                            //-------------------------------    //have to copy all properties.
                            ri.Sard_cc_expiry_dt = _i.Sard_cc_expiry_dt;
                            ri.Sard_cc_is_promo = _i.Sard_cc_is_promo;
                            ri.Sard_cc_period = _i.Sard_cc_period;
                            ri.Sard_cc_tp = _i.Sard_cc_tp;
                            ri.Sard_chq_bank_cd = _i.Sard_chq_bank_cd;
                            ri.Sard_chq_branch = _i.Sard_chq_branch;
                            ri.Sard_credit_card_bank = _i.Sard_credit_card_bank;
                            ri.Sard_deposit_bank_cd = _i.Sard_deposit_bank_cd;
                            ri.Sard_deposit_branch = _i.Sard_deposit_branch;

                            //--------------------------------
                            ri.Sard_ref_no = _i.Sard_ref_no;
                            //--------------------------------
                            save_receipItemList.Add(ri);
                            _i.Sard_settle_amt = _i.Sard_settle_amt - _h.Sar_tot_settle_amt;
                            _h.Sar_tot_settle_amt = _h.Sar_tot_settle_amt - ri.Sard_settle_amt;

                        }
                    }
                    _h.Sar_tot_settle_amt = orginal_HeaderTotAmt;


                }
                gvPayment.DataSource = save_receipItemList;
                gvPayment.DataBind();
                #endregion

            #region Receipt AutoNumber Value Assign
            MasterAutoNumber _receiptAuto = new MasterAutoNumber();
            _receiptAuto.Aut_cate_cd = GlbUserDefProf;
            //_receiptAuto.Aut_cate_tp = "PC";
            _receiptAuto.Aut_cate_tp = "PC"; //GlbUserDefProf;
            _receiptAuto.Aut_direction = 1;
            _receiptAuto.Aut_modify_dt = null;
            _receiptAuto.Aut_moduleid = "FGAP";
            _receiptAuto.Aut_number = 0;
            _receiptAuto.Aut_start_char = "FGAP";
            //Fill the Aut_start_char at the saving place (in BLL)
            //if (_h.Sar_receipt_type=="HPRS")
            //{ _receiptAuto.Aut_start_char = "HPRS"; }
            //else if (_h.Sar_receipt_type == "HPRM")
            //{ _receiptAuto.Aut_start_char = "HPRM"; }
            //_receiptAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
            _receiptAuto.Aut_year = null;
            #endregion

            #region Transaction AutoNumber Value Assign
            MasterAutoNumber _transactionAuto = new MasterAutoNumber();
            _transactionAuto.Aut_cate_cd = GlbUserDefProf;
            // _transactionAuto.Aut_cate_tp = "PC";//change this to GlbUserDefProf
            _transactionAuto.Aut_cate_tp = "PC";//GlbUserDefProf;
            _transactionAuto.Aut_direction = 1;
            _transactionAuto.Aut_modify_dt = null;
            _transactionAuto.Aut_moduleid = "FGAP";
            _transactionAuto.Aut_number = 0;
            _transactionAuto.Aut_start_char = "FGAP";
            _transactionAuto.Aut_year = null;
            #endregion

            Transaction_List = new List<HpTransaction>();
            fill_Transactions(_recHeader);
            
            //------------------------------------------------------------------------------------------------------
                //CHNLSVC.Inventory.Save_FGAP_confirmation
            //call confirm method

            //****************************************************************************************************************
            List<InventoryAdhocDetail> confirmed_detList = new List<InventoryAdhocDetail>();
            //Approve the requested items.
            InventoryAdhocDetail Det = null;
            foreach (ReptPickSerials rps in Approved_SerialList)
            {
                #region fill Confirm detail

                Det = new InventoryAdhocDetail();
                Det.Iadd_anal1 = 1; //not sure

                Det.Iadd_anal2 = rps.Tus_itm_model;
                Det.Iadd_anal3 = rps.Tus_itm_desc;
                Det.Iadd_anal4 = rps.Tus_ser_id;
                //Det.Iadd_anal5 =;
                Det.Iadd_app_itm = rps.Tus_itm_cd;
                Det.Iadd_app_pb = txtPriceBook.Text.Trim().ToUpper();
                Det.Iadd_app_pb_lvl = txtPBLevel.Text.Trim().ToUpper();
                if (txtUnitPrice.Text.Trim() != "")
                {
                    Det.Iadd_app_val = Convert.ToDecimal(txtUnitPrice.Text.Trim());
                }

                Det.Iadd_claim_itm = rps.Tus_itm_cd;
                //Det.Iadd_claim_pb =;
                //Det.Iadd_claim_pb_lvl = ;
                //Det.Iadd_claim_val = ;
                Det.Iadd_coll_itm = rps.Tus_itm_cd;
                //Det.Iadd_coll_pb = ;
                //Det.Iadd_coll_pb_lvl = ;
                //Det.Iadd_coll_ser1 = ;
                //Det.Iadd_coll_ser2 = ;,
                //Det.Iadd_coll_ser3 = ;
                //Det.Iadd_coll_ser3 = ;
                //Det.Iadd_coll_val = ;
                Det.Iadd_line = ItmLine++;
                //Det.Iadd_ref_no =;
                //Det.Iadd_seq = ;
                Det.Iadd_stus = 3;
                #endregion

                confirmed_detList.Add(Det);
            }

            //try {
            //Update header
            // Searched_AdhodHeader.Iadh_ref_no = txtRefNo.Text.Trim().ToUpper();
            // Searched_AdhodHeader.Iadh_app_by = GlbUserName;
            // Searched_AdhodHeader.Iadh_app_dt = DateTime.Now.Date;
            // Searched_AdhodHeader.Iadh_stus = 1;
            Searched_AdhodHeader.Iadh_coll_by = GlbUserName;
            Searched_AdhodHeader.Iadh_coll_dt = DateTime.Now.Date;
            Searched_AdhodHeader.Iadh_stus = 3;

            #region ADJ(-)
            //string AdjNumber = txtAdjustmentNo.Text.Trim();
            //string AdjNumber = "";
            //string manualNum = txtManualRefNo.Text.Trim();
            //string remarks = txtRemarks.Text.Trim();
            //string adj_base = ddlAdjBased.SelectedValue;
            //string adj_sub_type = ddlAdjSubTyepe.SelectedValue;
            //string adj_type = ddlAdjType_.SelectedValue;
            InventoryHeader inHeader = new InventoryHeader();


            inHeader.Ith_acc_no = "";
            inHeader.Ith_anal_1 = "";
            inHeader.Ith_anal_10 = true;
            inHeader.Ith_anal_11 = true;
            inHeader.Ith_anal_12 = true;
            inHeader.Ith_anal_2 = "";
            inHeader.Ith_anal_3 = "";
            inHeader.Ith_anal_4 = "";
            inHeader.Ith_anal_5 = "";
            inHeader.Ith_anal_6 = 0;
            inHeader.Ith_anal_7 = 0;
            inHeader.Ith_anal_8 = DateTime.MinValue;
            inHeader.Ith_anal_9 = DateTime.MinValue;
            inHeader.Ith_bus_entity = "";
            //inHeader.Ith_cate_tp = ddlAdjSubTyepe.SelectedValue.ToString();
            inHeader.Ith_channel = "";
            inHeader.Ith_com = GlbUserComCode;

            //inHeader.Ith_com ="";
            inHeader.Ith_com_docno = "";
            inHeader.Ith_cre_by = "";
            inHeader.Ith_cre_when = DateTime.MinValue;
            inHeader.Ith_del_add1 = "";
            inHeader.Ith_del_add2 = "";
            inHeader.Ith_del_code = "";

            inHeader.Ith_del_party = "";
            inHeader.Ith_del_town = "";


            inHeader.Ith_direct = true;
            //  inHeader.Ith_direct =true;
            inHeader.Ith_doc_date = DateTime.Today;
            //  inHeader.Ith_doc_date  =DateTime.MinValue;
            inHeader.Ith_doc_no = "";//"DPS32-ADJ-12-588888";

            inHeader.Ith_doc_tp = "ADJ";
            //   inHeader.Ith_doc_tp ="";
            inHeader.Ith_doc_year = DateTime.Today.Year;
            inHeader.Ith_entry_no = "";
            inHeader.Ith_entry_tp = "";
            inHeader.Ith_git_close = true;
            inHeader.Ith_git_close_date = DateTime.MinValue;
            inHeader.Ith_git_close_doc = "";
            inHeader.Ith_isprinted = true;
            inHeader.Ith_is_manual = true;
            inHeader.Ith_job_no = "";
            inHeader.Ith_loading_point = "";
            inHeader.Ith_loading_user = "";
            inHeader.Ith_loc = GlbUserDefLoca;
            //if (txtManualRefNo.Text == null || txtManualRefNo.Text == "")
            //{
            //    inHeader.Ith_manual_ref = "N/A";
            //}
            //else
            //{
            //    inHeader.Ith_manual_ref = txtManualRefNo.Text.Trim();
            //}
            inHeader.Ith_manual_ref = "N/A";
            // inHeader.Ith_manual_ref ="";
            inHeader.Ith_mod_by = GlbUserName;//"ADMIN";
            inHeader.Ith_mod_when = DateTime.MinValue;
            inHeader.Ith_noofcopies = 1;
            inHeader.Ith_oth_loc = "";

            inHeader.Ith_remarks = "ADHOC Confirm";//txtRemarks.Text;
            // inHeader.Ith_remarks ="";
            inHeader.Ith_sbu = "INV";
            inHeader.Ith_seq_no = 6;
            //inHeader.Ith_seq_no =54;
            inHeader.Ith_session_id = GlbUserSessionID;
            inHeader.Ith_stus = "A";
            inHeader.Ith_sub_tp = (CommonUIDefiniton.AdjustmentType.ADHOC).ToString(); //ddlAdjSubTyepe.SelectedValue.ToString();
            // inHeader.Ith_sub_tp ="";
            inHeader.Ith_vehi_no = "";

            inHeader.Ith_direct = false;
            //*********************************
            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_cd = GlbUserDefLoca;
            masterAuto.Aut_cate_tp = "LOC";
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "ADJ";
            masterAuto.Aut_number = 5;//what is Aut_number
            masterAuto.Aut_start_char = "ADJ";
            masterAuto.Aut_year = null;
            //  masterAuto.Aut_year = Convert.ToDateTime(txtDate_.Text).Year;

            #endregion
            string AdjNumber = string.Empty;
            List<ReptPickSerialsSub> rps_subList = new List<ReptPickSerialsSub>();
            Int16 adjEffect = CHNLSVC.Inventory.ADJMinus(inHeader, Approved_SerialList, rps_subList, masterAuto, out AdjNumber);
            Searched_AdhodHeader.Iadh_adj_no = AdjNumber;

            Int32 effect = 0;
            //effect = CHNLSVC.Inventory.Save_Adhoc_Confirm(Searched_AdhodHeader, confirmed_detList);//_recieptItem
            string GenReceiptNo = "";
            effect = CHNLSVC.Inventory.Save_FGAP_confirmation(Searched_AdhodHeader, confirmed_detList, receiptHeaderList, save_receipItemList, Transaction_List, _receiptAuto, _transactionAuto, GlbUserDefLoca, GlbUserDefProf, out GenReceiptNo);
            if (effect < 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Failed to Confirmed. Error occured. Try Again!");
                return;
            }
            else if (effect > 0)
            {
                clearCompleateScreen();
               
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Confirmed Successfully! ADJ(-) NO. =" + AdjNumber + ", Receipt No.=" + GenReceiptNo);

                string Msgg = "<script>alert('Confirmed Successfully! ADJ(-) NO. = '" + AdjNumber + " , Receipt No.=" + GenReceiptNo + " );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);
            }
            //------------------------------------------------------------------------------------------------------
            //}
        }

       
        protected void imgBtnSearchItmCD_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable dataSource = CHNLSVC.CommonSearch.GetItemSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            //  MasterCommonSearchUCtrl.ReturnResultControl = ddlItemCodes.ClientID; //txtItemCD.ClientID;
            MasterCommonSearchUCtrl.ReturnResultControl = txtItemCD.ClientID; //txtItemCD.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImgSearchRefNo_Click(object sender, ImageClickEventArgs e)
        {
            //string Loc = txtSendLoc.Text.Trim().ToUpper();
            //Int32 status = rdoPending.Checked == true ? 0 : 1;
            //Int32 type = ddlReuestType.SelectedValue=="2"?2:1;
           if(ddlAction.SelectedValue=="Request")
           {
               return;
           }
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.FixAssetRefNo);
            DataTable dataSource = CHNLSVC.CommonSearch.GET_FixAsset_ref(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            //  MasterCommonSearchUCtrl.ReturnResultControl = ddlItemCodes.ClientID; //txtItemCD.ClientID;
            MasterCommonSearchUCtrl.ReturnResultControl = txtRefNo.ClientID; //txtItemCD.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void grvAvailableSerials_RowDataBound(object sender, GridViewRowEventArgs e)
        {  

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string itemCode = e.Row.Cells[1].Text;
                Label serID = (Label)e.Row.FindControl("lblSerID_av");
                if (det_list_selected != null)
                {
                    var _exist = from _dup in det_list_selected
                                     where _dup.Iadd_claim_itm == itemCode //&& _dup.Sccd_brd == obj.Sccd_brd 
                                     select _dup;

                    if (_exist.Count() != 0)
                    {
                        foreach (InventoryAdhocDetail det in _exist)
                        {
                            if (det.Iadd_anal4 == Convert.ToInt32(serID.Text))
                            {
                                e.Row.BackColor = Color.LightSalmon;
                            }
                        }
                    }
                }
            }
        }

        protected void txtRefNo_TextChanged(object sender, EventArgs e)
        {
            if(ddlAction.SelectedValue!="Request")
            {
                if (txtRefNo.Text.Trim()!="")
                {
                    btnRefOk_Click(sender, e);
                }
                
                txtRefNo.Focus();
            }
           
        }

        protected void btnItmCd_Click(object sender, EventArgs e)
        {

            MasterItem msitem = new MasterItem();
            try {
                msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItemCD.Text.Trim().ToUpper());
                txtModel.Text = msitem.Mi_model;
                txtItmDescription.Text = msitem.Mi_shortdesc;
                txtQty.Focus();
                MasterMsgInfoUCtrl.Clear();
            }
            catch(Exception ex){
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid Item code!");
                txtModel.Text ="";
                txtItmDescription.Text ="";
                return;
            }
           
        }

        protected void btn_PRINT_Click(object sender, EventArgs e)
        {
            try {
               
                Int32 status=0;                             
                   
                if (ddlAction.SelectedValue == "Confirmation")
                {
                    status = 3;//confiremed
                    GlbFixAssetStatus = 3;
                    GlbReportPath = "~\\Reports_Module\\Inv_Rep\\FixedAssetConfirmationNote.rpt";
                    GlbReportMapPath = "~/Reports_Module/Inv_Rep/FixedAssetConfirmationNote.rpt";
                }
                else
                {
                    status = 0;//requested
                    GlbFixAssetStatus = 0;
                    GlbReportPath = "~\\Reports_Module\\Inv_Rep\\FixedAssetTransferNote.rpt";
                    GlbReportMapPath = "~/Reports_Module/Inv_Rep/FixedAssetTransferNote.rpt";
                }
                          
               // GlbSelectionFormula = "{INT_REQ.ITR_REQ_NO}='" + ReqNo + "'";
                string refNo = txtRefNo.Text;
                GlbFixAsset_RefNo = refNo;
                GlbReportUser = GlbUserName;
                GlbProfitCenter = txtSendLoc.Text.ToUpper();

                GlbMainPage = "~/Inventory_Module/FixAssetOrAdhocRequestAndApprove.aspx";

                string Msg1 = "<script>window.open('../Reports_Module/Inv_Rep/FixedAssetTransferPrint.aspx','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);
            }
            catch(Exception ex){
            
            }
            
        }

        protected void btnUnitPrice_Click(object sender, EventArgs e)
        {
            List<PriceDetailRef> _priceDetailRef = new List<PriceDetailRef>();
            _priceDetailRef = CHNLSVC.Sales.GetPrice(GlbUserComCode, txtPC.Text.Trim(), "CS", txtPriceBook.Text, txtPBLevel.Text, string.Empty, txtItemCD.Text, txtQty.Text == "" ? 1 : Convert.ToDecimal(txtQty.Text), DateTime.Now.Date);
            if (_priceDetailRef != null)
            {
                if (_priceDetailRef.Count > 0)
                {
                    Decimal unitPrice = _priceDetailRef[0].Sapd_itm_price;
                    txtUnitPrice.Text = unitPrice.ToString();
                    if (txtUnitPrice.Text.Trim() == "")
                    {                       

                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Prices not defined for this item in this PriceBook and Level!!");
                        return;
                    }                   
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Prices not defined for this item in this PriceBook and Level!!");
                    return;
                }
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Prices not defined for this item in this PriceBook and Level!!");
                return;
            }
        }

    }
}