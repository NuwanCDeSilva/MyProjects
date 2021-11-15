using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using FF.BusinessObjects;
using System.Text.RegularExpressions;
using System.IO;

namespace FF.WebERPClient.Inventory_Module
{
    public partial class GRAN_Note : BasePage
    {
        //remove view state
        //2012/12/03

        protected override object LoadPageStateFromPersistenceMedium()
        {
            object viewStateBag;
            string m_viewState = (string)Session["SalesEntryViewState"];
            LosFormatter m_formatter = new LosFormatter();
            try
            {
                viewStateBag = m_formatter.Deserialize(m_viewState);
            }
            catch
            {
                throw new HttpException("The View State is invalid.");
            }
            return viewStateBag;
        }
        protected override void SavePageStateToPersistenceMedium(object viewState)
        {
            MemoryStream ms = new MemoryStream();
            LosFormatter m_formatter = new LosFormatter();
            m_formatter.Serialize(ms, viewState);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            string viewStateString = sr.ReadToEnd();
            Session["SalesEntryViewState"] = viewStateString;
            ms.Close();
            return;
        }


        #region Properties/Local Variables

        protected List<InventoryRequestSerials> _invreqserialList
        {
            get { return (List<InventoryRequestSerials>)ViewState["_invreqserialList"]; }
            set { ViewState["_invreqserialList"] = value; }
        }

        //ADDED SACHITH 2012/08/16
        protected List<InventoryRequestItem> _invreqitmList
        {
            get { return (List<InventoryRequestItem>)ViewState["_invreqitmList"]; }
            set { ViewState["_invreqitmList"] = value; }
        }
       
        protected InventoryRequest _invreq
        {
            get { return (InventoryRequest)ViewState["_invreq"]; }
            set { ViewState["_invreq"] = value; }
        }

        protected string ReqNo {
            get { return (string)ViewState["reqNo"]; }
            set { ViewState["reqNo"] = value; }
        }

        protected bool IsApp
        {
            get { return (bool)ViewState["IsApp"]; }
            set { ViewState["IsApp"] = value; }
        }
        //END

        protected Int32 _lineNo
        {
            get { return (Int32)ViewState["_lineNo"]; }
            set { ViewState["_lineNo"] = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                IsAllowBackDateForModule(GlbUserComCode, GlbUserDefLoca, string.Empty, Page, string.Empty, imgBtnDate, lblDispalyInfor);

                IsApp = false;
                ReqNo = "";
                //SetInitialValues();
                _invreqitmList = new List<InventoryRequestItem>();
                _invreqserialList = new List<InventoryRequestSerials>();
                BindRequestTypesDDLData(ddlRequestType);
                //ADDED SACHITH 2012/08/16
                BindRequestTypesDDLData(DropDownListType);
                BindEmptyMRNListTable();
                //END

                BindUserCompanyItemStatusDDLData(ddlItemStatus);
                BindUserCompanyItemStatusDDLData(ddlNewStatus);
                BindUserCompanyItemStatusDDLData(DropDownListAppStatus);
                BindJavaScripts();
                BindRequestSerials();
                _lineNo = 0;
                txtRequestDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                lblAppby.Text = GlbUserName;
                btnApproved.Enabled = false;
                btnReject.Enabled = false;
                btnCancel.Enabled = false;
                BindEmptyMRNListTable();
                gvGRANList.DataBind();
                tcGRNContainer.ActiveTabIndex = 0;
            }
            txtRequestDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtSerialID.Attributes.Add("readonly", "readonly");
            txtTransferLocation.Attributes.Add("onKeyup", "return clickButton(event,'" + imgTransferLocation.ClientID + "')");
            txtSerNo.Attributes.Add("onKeyup", "return clickButton(event,'" + imgSearchSer.ClientID + "')");
            txtSerialID.Attributes.Add("onKeyup", "return clickButton(event,'" + imgSearchQty.ClientID + "')");
            txtItemCode.Attributes.Add("onKeyup", "return clickButton(event,'" + imgItemSearch.ClientID + "')");
        }

        #region Data Binding
        private void BindRequestTypesDDLData(DropDownList ddl)
        {
            ddl.Items.Clear();
            //ddl.Items.Add(new ListItem("--Select Request Type--", "-1"));
            ddl.Items.Add(new ListItem(string.Empty, "-1"));
            ddl.DataSource = CHNLSVC.General.GetAllType(CommonUIDefiniton.MasterTypeCategory.GRAN.ToString());
            ddl.DataTextField = "Mtp_desc";
            ddl.DataValueField = "Mtp_cd";
            ddl.DataBind();
        }

        private void BindUserCompanyItemStatusDDLData(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem(string.Empty, "-1"));
            ddl.DataSource = CHNLSVC.Inventory.GetAllCompanyStatus(GlbUserComCode);
            ddl.DataTextField = "MIS_DESC";
            ddl.DataValueField = "MIC_CD";
            ddl.DataBind();
        }

        private void BindDINstatusDDLData(DropDownList ddl) {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem(string.Empty, "-1"));
            ddl.DataSource = CHNLSVC.Inventory.GetModuleStatus("DIN");
            ddl.DataTextField = "MIS_DESC";
            ddl.DataValueField = "MIS_CD";
            ddl.DataBind();
        }

        private void BindJavaScripts()
        {
            txtItemCode.Attributes.Add("onblur", "GetItemData()");
            txtSerNo.Attributes.Add("onblur", "GetSerialInfor('" + GlbUserComCode + "','" + GlbUserDefLoca + "')");
            txtSerialID.Attributes.Add("onblur", "GetSerialInforDoc('" + GlbUserComCode + "','" + GlbUserDefLoca + "')");
        }

        protected void BindItemStatus(DropDownList ddl, string _company, string _location, string _bin, string _item, string _serial)
        {
            ddl.Items.Clear();
            DataTable _tbl = CHNLSVC.Inventory.GetAvailableItemStatus(_company, _location, _bin, _item, _serial);
            ddl.DataSource = _tbl;
            ddl.DataTextField = "ins_itm_stus";
            ddl.DataValueField = "ins_itm_stus";
            ddl.DataBind();
        }

        protected void BindRequestSerials()
        {
            DataTable _table = CHNLSVC.Inventory.GetAllGRANSerialsTable(GlbUserComCode, GlbUserDefLoca, "GRAN", "N/A");
            if (_table.Rows.Count <= 0)
            {
                //_table = SetEmptyRow(_table);
                gvItem.DataSource = _table;
            }
            else
            {
                _invreqserialList = CHNLSVC.Inventory.GetAllGRANSerialsList(GlbUserComCode, GlbUserDefLoca, "GRAN", "N/A");
                gvItem.DataSource = _invreqserialList;

            }
            gvItem.DataBind();
        }

        #endregion

        #region Events

        protected void ddlRequestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //divInvoiceData.Visible = (ddlRequestType.SelectedValue.ToUpper().Equals("INTR")) ? true : false;
            if (ddlRequestType.SelectedValue == "DIN")
            {
                chkToStores.Visible = false;
                chkToReport.Visible = true;
                chkToReport.Checked = true;
                txtTransferLocation.Enabled = false;
                imgTransferLocation.Enabled = false;
                BindDINstatusDDLData(ddlNewStatus);
                BindDINstatusDDLData(DropDownListAppStatus);
                GRAN_DIN.Visible = false;
                ResetFields(PanelGranAll.Controls);
                ddlRequestType.SelectedValue = "DIN";
                btnAddItem.Enabled = true;
                btnSave.Enabled = true;
                pnlGRANItem.Enabled = true;
                txtRequestDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            }
            if (ddlRequestType.SelectedValue == "GRAN")
            {
                chkToStores.Visible = true;
                chkToReport.Visible = false;
                chkToStores.Checked = true;
                txtTransferLocation.Enabled = true;
                imgTransferLocation.Enabled = true;
                ddlNewStatus.Enabled = true;
                DropDownListAppStatus.Enabled = true;
                BindUserCompanyItemStatusDDLData(ddlItemStatus);
                BindUserCompanyItemStatusDDLData(ddlNewStatus);
                BindUserCompanyItemStatusDDLData(DropDownListAppStatus);
                //ddlNewStatus.Items.Remove(ddlNewStatus.Items.FindByValue("DMG"));
                ddlNewStatus.Items.Remove(ddlNewStatus.Items.FindByValue("CONS"));
                //DropDownListAppStatus.Items.Remove(DropDownListAppStatus.Items.FindByValue("DMG"));
                DropDownListAppStatus.Items.Remove(DropDownListAppStatus.Items.FindByValue("CONS"));
                GRAN_DIN.Visible = true;
                LoadDin(DropDownListDINNo);
            }
        }

        private void LoadDin(DropDownList ddl)
        {
            string _masterLocation = (string.IsNullOrEmpty(GlbUserDefLoca)) ? GlbUserDefProf : GlbUserDefLoca;
            ddl.Items.Clear();
            //ddl.Items.Add(new ListItem("--Select Request Type--", "-1"));
            ddl.Items.Add(new ListItem(string.Empty, "-1"));
            ddl.DataSource = CHNLSVC.Inventory.GetDINList(GlbUserComCode, _masterLocation);
            ddl.DataTextField = "itr_req_no";
            ddl.DataValueField = "itr_req_no";
            ddl.DataBind();
        }

        #region Common Search

        protected void imgTransferLocation_Click(object sender, ImageClickEventArgs e)
        {
  
            if (ddlRequestType.SelectedValue == "DIN")
            {

                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WHAREHOUSE);
                //DataTable dataSource = CHNLSVC.CommonSearch.GetUserLocationSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
                DataTable dataSource = CHNLSVC.Inventory.GetLocationByType(GlbUserComCode, null, null); //Edit Chamal 25/06/2012
                MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
                MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
                MasterCommonSearchUCtrl.ReturnResultControl = txtTransferLocation.ClientID;
                MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
            }
            //load warehouses for loged user company
            else if (ddlRequestType.SelectedValue == "GRAN")
            {
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WHAREHOUSE);
                DataTable dataSource = CHNLSVC.Inventory.GetLocationByType(GlbUserComCode,null,null);
                MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
                MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
                MasterCommonSearchUCtrl.ReturnResultControl = txtTransferLocation.ClientID;
                MasterCommonSearchUCtrl.UCModalPopupExtender.Show();

            }
            else {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select Request Type.");
                return;
            }
        }

        protected void ImgItemSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable dataSource = CHNLSVC.CommonSearch.GetItemSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtItemCode.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImgSearchSer_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerial);
            DataTable dataSource = CHNLSVC.CommonSearch.GetAvailableSerialSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtSerNo.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImgSearchQty_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableNoneSerial);
            DataTable dataSource = CHNLSVC.CommonSearch.GetAvailableNoneSerialSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtSerialID.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        #region Common Search Methods
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(GlbUserName + seperator + GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.SalesInvoice:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.WHAREHOUSE: // Add Chamal 27/06/2012
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.AvailableSerial:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefLoca + seperator + txtItemCode.Text + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.AvailableNoneSerial:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefLoca + seperator + txtItemCode.Text + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        #endregion

        #region Buttons Events

        #region Button Pannel

        #region Approved Button
        protected void btnApproved_Click(object sender, EventArgs e)
        {
            //InventoryRequest _inputInvReq = new InventoryRequest();
            //_inputInvReq.Itr_req_no = ReqNo;
            //InventoryRequest _invreq = CHNLSVC.Inventory.GetInventoryRequestData(_inputInvReq);
            //_invreqitmList = _invreq.InventoryRequestItemList;
            //_invreqserialList = _invreq.InventoryRequestSerialsList;


             Update("A");
            
            
        }
        #endregion
        #region Save Button
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsApp)
                Save(true);
            else
                Save(false);
        }
        #endregion
        #region Cancel Button
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Update("C");
        }
        #endregion
        #region Clear Button
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Inventory_Module/GRAN_Note.aspx", false);
        }
        #endregion
        #region Close Button
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }
        #endregion
        #region reject button

        protected void btnReject_Click(object sender, EventArgs e)
        {
            Update("R");
        }

        #endregion

        #endregion

        #region Other Buttons
        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            //ADDED BY SACTITH
            //2012/08/16
            //CHECK ITEM AVALABILITY (mi_is_ser1!=-1)

            #region validation

            //if (ddlTransferType.SelectedValue.Equals("-1"))
            //{

            //    ddlTransferType.Focus();
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select Transfer Type.");
            //    return;
            //}
            if (gvItem.Rows.Count >= 1)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Only one item can add");
                return;
            }

            if (txtItemDescription.Text == "") {
                txtItemDescription.Focus();
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select Item.");
                return;
            }
            if (txtSerialID.Text == "")
            {
                txtSerialID.Focus();
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select serial.");
                return;
            }
            if (ddlNewStatus.SelectedValue == "-1") {
                ddlNewStatus.Focus();
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select New status.");
                return;
            }
            #endregion


            MasterItem _item = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItemCode.Text);
            ReptPickSerials result =CHNLSVC.Inventory.GetAvailableSerIDInformation(GlbUserComCode,GlbUserDefLoca,_item.Mi_cd,txtSerNo.Text,hdn_SER2.Value,txtSerialID.Text);
            ReptPickSerials _rept = CHNLSVC.Inventory.Get_all_details_on_serialID(GlbUserComCode, GlbUserDefLoca, null, txtItemCode.Text,Convert.ToInt32(txtSerialID.Text));
            if (_rept.Tus_itm_cd == null) {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Item Code Serial Mismatch");
                return;
            
            }
            
            if (_item != null && _item.Mi_is_ser1 != -1)
            {
                
                //TODO:REMOVE COMMENT
                if (ddlRequestType.SelectedValue == "DIN")
                {

                    AddItem();
                    //if (result.Tus_doc_dt.AddDays(3) >= DateTime.Now)
                    //{
                    //    AddItem();
                    //}
                    //else
                    //{
                    //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Document in date has to be in three days range");
                    //}
                }
                //END
                 if (ddlRequestType.SelectedValue == "GRAN")
                {
                    if (_item.Mi_itm_stus == "RVTLP" || _item.Mi_itm_stus == "RVT")
                    {
                        if (result.Tus_doc_dt.AddMonths(3) <= DateTime.Now)
                        {
                            AddItem();
                        }
                        else
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Document in Date has to be in three months range");
                        }
                    }
                    else
                        AddItem();
                }
               
               
            }

            else {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Item can not add");
            }
            //END

            //AddItem();
        }
        #endregion

        #endregion

        #endregion



        #region Functions

        //Get Location Information By Locaton Code
        private bool IsValidLocation(string _loc)
        {
            bool status = false;
            if (!string.IsNullOrEmpty(_loc))
            {
                MasterLocation _masterLocation = CHNLSVC.General.GetLocationByLocCode(GlbUserComCode, _loc);
                status = (_masterLocation == null) ? false : true;
            }
            return status;
        }

        private InventoryRequestSerials FillItemObject()
        {
            InventoryRequestSerials _tempSer = new InventoryRequestSerials();
            hdn_IN_BATCHLINE.Value = "0";
            hdn_IN_DOCDT.Value = "01/01/2012";
            hdn_IN_ITMLINE.Value = "0";
            hdn_IN_SERLINE.Value = "0";

            ReptPickSerials _reptSer = new ReptPickSerials();
            _reptSer = CHNLSVC.Inventory.GetAvailableSerIDInformation(GlbUserComCode, GlbUserDefLoca, txtItemCode.Text.ToString(), txtSerNo.Text.ToString(), hdn_SER2.Value.ToString(), txtSerialID.Text.ToString());

            _tempSer.Itrs_in_batchline = Convert.ToInt16(_reptSer.Tus_batch_line);
            _tempSer.Itrs_in_docdt = Convert.ToDateTime(_reptSer.Tus_doc_dt).Date;
            _tempSer.Itrs_in_docno = _reptSer.Tus_doc_no;
            _tempSer.Itrs_in_itmline = Convert.ToInt16(_reptSer.Tus_itm_line);
            _tempSer.Itrs_in_seqno = Convert.ToInt32(_reptSer.Tus_seq_no);
            _tempSer.Itrs_in_serline = Convert.ToInt16(_reptSer.Tus_ser_line);
            _tempSer.Itrs_itm_cd = txtItemCode.Text.ToString();
            _tempSer.Itrs_itm_stus = _reptSer.Tus_itm_stus;
            _tempSer.Itrs_line_no = _lineNo;
            _tempSer.Itrs_nitm_stus = ddlNewStatus.Text.ToString();
            _tempSer.Itrs_qty = 1;
            _tempSer.Itrs_rmk = txtItemRemarks.Text.ToString();
            _tempSer.Itrs_seq_no = 0;
            _tempSer.Itrs_ser_1 = txtSerNo.Text.ToString();
            _tempSer.Itrs_ser_2 = _reptSer.Tus_ser_2;
            _tempSer.Itrs_ser_3 = "";
            _tempSer.Itrs_ser_4 = "";
            _tempSer.Itrs_ser_id = _reptSer.Tus_ser_id;
            _tempSer.Itrs_ser_line = _lineNo;
           // _tempSer.Itrs_trns_tp = ddlTransferType.SelectedValue.ToString();
            _tempSer.Mi_longdesc = txtItemDescription.Text.ToString();
            _tempSer.Mi_model = txtModelNo.Text.ToString();
            _tempSer.Mi_brand = txtBrand.Text.ToString();
            return _tempSer;
        }

        //ADDED SACHITH
        //2012/08/16
        private InventoryRequestItem FillReqItm()
        {
            InventoryRequestItem _temitm = new InventoryRequestItem();
            
            ReptPickItems _repitm=new ReptPickItems();
            // _repitm = CHNLSVC.Inventory.GetAvailableItemStatus(GlbUserComCode, GlbUserDefLoc,, txtSerNo.Text.ToString());
            MasterItem item= CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItemCode.Text);
            _temitm.Itri_itm_cd = txtItemCode.Text.ToString();
            _temitm.MasterItem = new MasterItem();
            _temitm.MasterItem.Mi_cd = item.Mi_cd;
            _temitm.MasterItem.Mi_itm_stus = item.Mi_itm_stus;
            _temitm.Itri_mitm_cd = item.Mi_cd;
            _temitm.Itri_mitm_stus = item.Mi_itm_stus;
            _temitm.Itri_itm_stus = ddlItemStatus.SelectedValue;
            _temitm.Itri_app_qty = 1;
           // _temitm.Itri_itm_stus = _repitm.Tus_itm_stus;
            _temitm.Itri_line_no = _lineNo;
            _temitm.Itri_note = txtItemRemarks.Text;
            _temitm.Itri_bqty = 1;
            //_temitm.Itri_mitm_cd
            _temitm.Itri_qty = Convert.ToInt32(txtQty.Text);
            _temitm.Mi_brand = txtBrand.Text.ToString();
            return _temitm;
        }
        //END
        private void AddItem()
        {
            try
            {
                //if (string.IsNullOrEmpty(txtItemCode.Text))
                //{
                //    txtItemCode.Focus();
                //    throw new UIValidationException("Enter then item code!");
                //}
                //if (string.IsNullOrEmpty(txtSerNo.Text))
                //{
                //    txtSerNo.Focus();
                //    throw new UIValidationException("Enter the serial no!");
                //}
                //if (string.IsNullOrEmpty(txtInDocNo.Text))
                //{
                //    txtInDocNo.Focus();
                //    throw new UIValidationException("Enter the referance inward document no!");
                //}
                //if (string.IsNullOrEmpty(txtItemRemarks.Text))
                //{
                //    txtItemRemarks.Focus();
                //    throw new UIValidationException("Enter the item remarks!");
                //}
                //if (string.IsNullOrEmpty(ddlTransferType.Text))
                //{
                //    ddlTransferType.Focus();
                //    throw new UIValidationException("Select the transfer type!");
                //}
                List<InventoryRequestSerials> inReSe = _invreqserialList.Where(x => x.Itrs_itm_cd == txtItemCode.Text).ToList<InventoryRequestSerials>();
                if (inReSe.Count<= 0)
                {

                    _lineNo += 1;
                    _invreqserialList.Add(FillItemObject());
                    _invreqitmList.Add(FillReqItm());
                    gvItem.DataSource = _invreqserialList;
                    gvItem.DataBind();
                    ddlRequestType.Enabled = false;
                    //BindRequestSerials();
                    ClearAfterAddItem();
                }
                else {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Item alredy in GRAN");
                    return;
                }
            }
            catch (UIValidationException ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.ErrorMessege);
            }
            catch (Exception e)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, e.Message);
            }

        }

      

        private string GetRequestNo()
        {
            string _reqNo = string.Empty;

            if (string.IsNullOrEmpty(hdn_SelectedReqNo.Value))
                _reqNo = "DOC-" + DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();
            else
                _reqNo = hdn_SelectedReqNo.Value;

            return _reqNo;
        }

        private MasterAutoNumber GenerateMasterAutoNumber()
        {
            //string moduleText = ddlRequestType.SelectedValue.Equals("MRN") ? "MRN" : "INTR";

            string moduleText = ddlRequestType.SelectedValue.ToString().ToUpper();

            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_tp = "LOC";
            masterAuto.Aut_cate_cd = string.IsNullOrEmpty(GlbUserDefLoca) ? GlbUserDefProf : GlbUserDefLoca;
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = moduleText;
            masterAuto.Aut_number = 0;
            masterAuto.Aut_start_char = moduleText;
            masterAuto.Aut_year = null;

            return masterAuto;
        }

        private void ClearAfterAddItem()
        {
            txtItemCode.Text = string.Empty;
            txtItemDescription.Text = string.Empty;
            txtModelNo.Text = string.Empty;
            txtBrand.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtSerialID.Text = string.Empty;
            txtSerNo.Text = string.Empty;
            txtInDocNo.Text = string.Empty;
            txtItemRemarks.Text = string.Empty;
            hdn_IN_BATCHLINE.Value = "";
            hdn_IN_DOCDT.Value = "";
            hdn_IN_ITMLINE.Value = "";
            hdn_IN_SEQNO.Value = "";
            hdn_IN_SERLINE.Value = "";
            hdn_SER2.Value = "";
        }

        private void Save(bool _isdin)
        {
            try
            {
                #region UI validation.

                if (IsAllowBackDateForModule(GlbUserComCode, GlbUserDefLoca, string.Empty, Page, Convert.ToDateTime(txtRequestDate.Text).ToString("dd/MMM/yyyy"), imgBtnDate, lblDispalyInfor) == false)
                {
                    if (Convert.ToDateTime(txtRequestDate.Text).Date != DateTime.Now.Date)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Back date not allow for selected date!");
                        return;
                    }
                }


                if (string.IsNullOrEmpty(GlbUserDefLoca))
                {
                    throw new UIValidationException("You have not allow any location for transaction.");
                }
                if (gvItem.Rows.Count <= 0)
                {
                    throw new UIValidationException("Please add item.");
                }

                if (ddlRequestType.SelectedValue.Equals("-1"))
                {
                    ddlRequestType.Focus();
                    throw new UIValidationException("Please select Request Type.");
                }
                if (ddlNewStatus.SelectedValue.Equals("-1"))
                {
                    ddlNewStatus.Focus();
                    throw new UIValidationException("Please select change status.");
                }
                if (ddlRequestType.SelectedValue == "GRAN")
                {
                    if (string.IsNullOrEmpty(txtTransferLocation.Text) || txtTransferLocation.Text.Trim().Equals("--Search Location--") || txtTransferLocation.Text.Trim().Equals(""))
                    {
                        txtTransferLocation.Focus();
                        throw new UIValidationException("Please enter transfer location.");
                    }
                    if (!this.IsValidLocation(txtTransferLocation.Text.ToString()))
                        throw new UIValidationException("Please enter valid Location.");
                }
                
                //if (string.IsNullOrEmpty(txtManualRef.Text))
                //{
                //    txtManualRef.Focus();
                //    throw new UIValidationException("Please enter manual referance.");
                //}
                if (string.IsNullOrEmpty(txtRemarks.Text.ToString()))
                {
                    throw new UIValidationException("Please enter remarks.");
                }
                if (chkSellAtShop.Checked == false && chkDiscount.Checked == false && chkToReport.Checked == false && chkToStores.Checked == false  )
                {
                    chkSellAtShop.Focus();
                    throw new UIValidationException("Please select the request condition.");
                }
                if (_invreqserialList == null) 
                {
                    throw new UIValidationException("Please add items to List.");
                }
                string _masterLocation = (string.IsNullOrEmpty(GlbUserDefLoca)) ? GlbUserDefProf : GlbUserDefLoca;
                if (ddlRequestType.SelectedValue == "GRAN" && txtManualRef.Text!="")
                {
                if (!CHNLSVC.Inventory.IsValidManualDocument(GlbUserComCode,_masterLocation,"MDOC_GRAN",Convert.ToInt32(txtManualRef.Text)))
                    throw new UIValidationException("Invalid manual ref.");
                }
                if (ddlRequestType.SelectedValue == "DIN" && txtManualRef.Text!="")
                {
                    if (!CHNLSVC.Inventory.IsValidManualDocument(GlbUserComCode, _masterLocation, "MDOC_DIN", Convert.ToInt32(txtManualRef.Text)))
                        throw new UIValidationException("Invalid manual ref.");
                }
                //Check back date
                //if (CHNLSVC.General.IsAllowBackDate(GlbUserComCode, GlbUserDefLoca, string.Empty, txtRequestDate.Text) == false)
                //{
                //    throw new UIValidationException("Back date not allow for selected date!");
                //}
                //else
                //{
                //    //txtRequestDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                //    //imgRequestDate.Visible = false;
                //}

                txtRequestDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                #endregion


                _masterLocation = (string.IsNullOrEmpty(GlbUserDefLoca)) ? GlbUserDefProf : GlbUserDefLoca;
                InventoryRequest _inventoryRequest = new InventoryRequest();

                //Fill the InventoryRequest header & footer data.
                _inventoryRequest.Itr_com = GlbUserComCode;
                _inventoryRequest.Itr_req_no = "DOC-" + DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();
                _inventoryRequest.Itr_tp = ddlRequestType.SelectedValue;
                //CHANGE
                _inventoryRequest.Itr_sub_tp = "TEMP";
                _inventoryRequest.Itr_loc = _masterLocation;
                _inventoryRequest.Itr_ref = string.Empty;
                _inventoryRequest.Itr_dt = Convert.ToDateTime(txtRequestDate.Text);
                _inventoryRequest.Itr_exp_dt = Convert.ToDateTime(txtRequestDate.Text);
                if(!_isdin)
                _inventoryRequest.Itr_stus = "P";
                else
                { _inventoryRequest.Itr_stus = "A"; //P - Pending , A - Approved.
                _inventoryRequest.Itr_anal1 = DropDownListDINNo.SelectedValue;
                } 
                _inventoryRequest.Itr_job_no = string.Empty;  //Invoice No.
                _inventoryRequest.Itr_bus_code = string.Empty;  //Customer Code.
                _inventoryRequest.Itr_note = txtRemarks.Text;
                _inventoryRequest.Itr_issue_from = _masterLocation;
                _inventoryRequest.Itr_rec_to = txtTransferLocation.Text;
                _inventoryRequest.Itr_direct = 0;
                _inventoryRequest.Itr_country_cd = string.Empty;  //Counrty Code.
                _inventoryRequest.Itr_town_cd = string.Empty;     //Town Code.
                _inventoryRequest.Itr_cur_code = string.Empty;    //Currency Code.
                _inventoryRequest.Itr_exg_rate = 0;              //Exchange rate.
                _inventoryRequest.Itr_collector_id = string.Empty;                
                _inventoryRequest.Itr_collector_name = string.Empty;
                _inventoryRequest.Itr_issue_com = GlbUserComCode;
                _inventoryRequest.Itr_collector_name = txtFeildManager.Text;
                _inventoryRequest.Itr_act = 1;
                _inventoryRequest.Itr_cre_by = GlbUserName;
                _inventoryRequest.Itr_ref = txtManualRef.Text;
                _inventoryRequest.Itr_gran_nstus = ddlNewStatus.SelectedValue;
                _inventoryRequest.Itr_gran_app_note = txtAppRemarks.Text;
                _inventoryRequest.Itr_gran_app_stus = DropDownListAppStatus.SelectedValue;
                if (chkSellAtShop.Checked)
                    _inventoryRequest.Itr_gran_opt1 = 1;
                if(chkDiscount.Checked)
                    _inventoryRequest.Itr_gran_opt2 = 1;
                if(chkToReport.Checked)
                    _inventoryRequest.Itr_gran_opt3 = 1;
                if(chkToStores.Checked)
                    _inventoryRequest.Itr_gran_opt4 = 1;

                _inventoryRequest.InventoryRequestItemList = _invreqitmList;
                _inventoryRequest.Itr_session_id = GlbUserSessionID;

                _inventoryRequest.InventoryRequestSerialsList = _invreqserialList;

                if (_isdin)
                {
                    InventoryRequest _inputInvReq = new InventoryRequest();
                    _inputInvReq.Itr_req_no = DropDownListDINNo.SelectedValue;

                    InventoryRequest _invreq = CHNLSVC.Inventory.GetInventoryRequestData(_inputInvReq);
                    if (_invreq != null)
                    {
                        _invreqitmList = _invreq.InventoryRequestItemList;
                        _invreqserialList = _invreq.InventoryRequestSerialsList; //CHNLSVC.Inventory.GetAllGRANSerialsList(_invreq.Itr_com, _invreq.Itr_loc, _invreq.Itr_tp, _invreq.Itr_req_no);

                        _inventoryRequest.InventoryRequestItemList = _invreqitmList;
                        _inventoryRequest.InventoryRequestSerialsList = _invreqserialList;
                    }
                }

                int rowsAffected = 0;
                string _docNo = string.Empty;

                if (string.IsNullOrEmpty(hdn_SelectedReqNo.Value))
                {
                    bool res = CHNLSVC.Inventory.UpdateSerialIDAvailable(GlbUserComCode, GlbUserDefLoca, _inventoryRequest.InventoryRequestSerialsList[0].Itrs_itm_cd, _inventoryRequest.InventoryRequestSerialsList[0].Itrs_ser_id, 1, -1);
                    rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_inventoryRequest, GenerateMasterAutoNumber(), out _docNo);
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Inventory Request Document Sucessfully saved. " + _docNo);
                    //string Msg = "<script>alert('Inventory Request Document Sucessfully saved! Document No. : " + _docNo + "');window.location = 'GRAN_Note.aspx';</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                    _masterLocation = (string.IsNullOrEmpty(GlbUserDefLoca)) ? GlbUserDefProf : GlbUserDefLoca;
                    if (ddlRequestType.SelectedValue == "GRAN" && txtManualRef.Text != "")
                    {
                        CHNLSVC.Inventory.UpdateManualDocNo(_masterLocation, "MDOC_GRAN", Convert.ToInt32(txtManualRef.Text));

                    }
                    if (ddlRequestType.SelectedValue == "DIN" && txtManualRef.Text != "")
                    {
                        CHNLSVC.Inventory.UpdateManualDocNo(_masterLocation, "MDOC_DIN", Convert.ToInt32(txtManualRef.Text));

                    }
                    if (ddlRequestType.SelectedValue == "GRAN" && !_isdin)
                    {
                        GlbReportPath = "~\\Reports_Module\\Inv_Rep\\GRANPrint.rpt";
                        GlbReportMapPath = "~/Reports_Module/Inv_Rep/GRANPrint.rpt";
                        GlbSelectionFormula = "{INT_REQ.ITR_REQ_NO}='" + _docNo + "'";
                        //GlbMainPage = "~/General_Modules/VehicalInsurance.aspx";
                        string Msg2 = "<script>window.open('../Reports_Module/Inv_Rep/GRANPrint.aspx','_blank');</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg2, false);
                    }
                    if(!_isdin)
                    ResetFields(PanelGranAll.Controls);
                    if(_isdin)
                        ReqNo = _docNo;

                }
                else
                {
                    if (_isdin)
                    {
                        rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_inventoryRequest, GenerateMasterAutoNumber(), out _docNo);
                        ReqNo = _docNo;
                    }
                    else
                    {
                        bool res = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, _inventoryRequest.InventoryRequestSerialsList[0].Itrs_itm_cd, _inventoryRequest.InventoryRequestSerialsList[0].Itrs_ser_id, -1);
                        rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_inventoryRequest, null, out _docNo);
                    }
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Inventory Request Document Sucessfully Updated."+_docNo);
                    //string Msg = "<script>alert('Inventory Request Document Sucessfully Updated!');window.location = 'GRAN_Note.aspx';</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                    if (ddlRequestType.SelectedValue == "GRAN")
                    {
                        GlbReportPath = "~\\Reports_Module\\Inv_Rep\\GRANPrint.rpt";
                        GlbReportMapPath = "~/Reports_Module/Inv_Rep/GRANPrint.rpt";
                        GlbSelectionFormula = "{INT_REQ.ITR_REQ_NO}='" + _docNo + "'";
                        //GlbMainPage = "~/General_Modules/VehicalInsurance.aspx";
                        string Msg1 = "<script>window.open('../Reports_Module/Inv_Rep/GRANPrint.aspx','_blank');</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);
                    }
                }
                hdn_SelectedReqNo.Value = "";
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Inventory Request Document Sucessfully saved. " + _docNo);
                //this.ClearPageData();
                IsApp = false;

            }
            catch (UIValidationException ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.ErrorMessege);
            }
            catch (Exception e)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, e.Message);
            }

        }

        #endregion

        //ADDED BY SACHITH
        //2012/08/16
        protected void gvItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try {
                DataTable datasource = (DataTable)gvItem.DataSource;
                datasource.Rows.RemoveAt(e.RowIndex);
                gvItem.DataSource = datasource;
            }
            catch(Exception){
            _invreqserialList.RemoveAt(e.RowIndex);
            gvItem.DataSource = _invreqserialList;
            }
            gvItem.DataBind();

            if (gvItem.Rows.Count <= 0)
                ddlRequestType.Enabled = true;
        }

        protected void imgbtnRequestSearch_Click(object sender, ImageClickEventArgs e)
        {
            GetSearchData();
        }

        private void GetSearchData()
        {
            
            InventoryRequest _inventoryRequest = new InventoryRequest();
            _inventoryRequest.Itr_com = GlbUserComCode;
            _inventoryRequest.Itr_loc = MasterUserLocation;
            _inventoryRequest.Itr_tp =DropDownListType.SelectedValue;

            string _fromDate = null;
            string _toDate = null;

            if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
            {
                _fromDate = txtFromDate.Text.Trim();
                _toDate = txtToDate.Text.Trim();
            }

            _inventoryRequest.FromDate = _fromDate;
            _inventoryRequest.ToDate = _toDate;
            _inventoryRequest.Itr_stus = ddlRequestStatus.SelectedValue;
            _inventoryRequest.Itr_cre_by = (chbCreatedUser.Checked) ? GlbUserName.ToUpper() : null;

            if (_inventoryRequest.Itr_stus == "X")
            {
                _inventoryRequest.Itr_stus = string.Empty;
            }

            List<InventoryRequest> _reqHdr = new List<InventoryRequest>();
            DataTable dt = CHNLSVC.Inventory.GetAllInventoryRequestDataTable(_inventoryRequest);

            gvGRANList.DataSource = dt;

            if (dt == null)
            {
                BindEmptyMRNListTable();
            }
            gvGRANList.DataBind();
            //ModalPopupExtender1.Show();
        }

        private void BindEmptyMRNListTable()
        {
            InventoryRequest _inventoryRequest = new InventoryRequest();
            DataTable _table = CHNLSVC.Inventory.GetAllInventoryRequestDataTable(_inventoryRequest);
            gvGRANList.DataSource = _table;
        }

        protected void gvGRANList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "SELECTINVREQ":
                    {
                        string req_no = e.CommandArgument.ToString();
                        ReqNo=req_no;
                        InventoryRequest _inputInvReq = new InventoryRequest();
                        _inputInvReq.Itr_req_no = req_no;
                        InventoryRequest _selectedInventoryRequest = CHNLSVC.Inventory.GetInventoryRequestData(_inputInvReq);
                        this.SetSelectedInventoryRequestData(_selectedInventoryRequest,false);
                        hdn_SelectedReqNo.Value = _selectedInventoryRequest.Itr_req_no;
                        IsApp = true;
                        //_invreq = _selectedInventoryRequest;
                        //updPnlMRNDataEntry.Update();
                        break;
                    }
                default:
                    break;
            }
        }

        private void SetSelectedInventoryRequestData(InventoryRequest _selectedInventoryRequest,bool isGRN)
        {
            if (_selectedInventoryRequest != null)
            {
                if (!isGRN)
                {
                    ddlRequestType.SelectedValue = _selectedInventoryRequest.Itr_tp;
                    ddlRequestType_SelectedIndexChanged(null, null);
                }
                txtRequestDate.Text = _selectedInventoryRequest.Itr_dt.ToShortDateString();
                txtTransferLocation.Text = _selectedInventoryRequest.Itr_rec_to;
                txtRemarks.Text = _selectedInventoryRequest.Itr_note;
                txtManualRef.Text = _selectedInventoryRequest.Itr_ref;
                txtFeildManager.Text = _selectedInventoryRequest.Itr_collector_name;
                if (_selectedInventoryRequest.Itr_stus == "A")
                    lblReqCurrStatus.Text = "Approved";
                else if (_selectedInventoryRequest.Itr_stus == "C")
                    lblReqCurrStatus.Text = "Cancel";
                else
                    lblReqCurrStatus.Text = "Pending";
                txtAppRemarks.Text = _selectedInventoryRequest.Itr_gran_app_note;
                lblAppby.Text = _selectedInventoryRequest.Itr_gran_app_by;
                if (!isGRN)
                ddlNewStatus.SelectedValue = _selectedInventoryRequest.Itr_gran_nstus;

                chkSellAtShop.Checked = Convert.ToBoolean(_selectedInventoryRequest.Itr_gran_opt1);
                chkDiscount.Checked = Convert.ToBoolean(_selectedInventoryRequest.Itr_gran_opt2);
                chkToReport.Checked = Convert.ToBoolean(_selectedInventoryRequest.Itr_gran_opt3);
                chkToStores.Checked = Convert.ToBoolean(_selectedInventoryRequest.Itr_gran_opt4);
                txtAppRemarks.Text=_selectedInventoryRequest.Itr_gran_app_note;
                if (!isGRN)
                DropDownListAppStatus.SelectedValue = _selectedInventoryRequest.Itr_gran_app_stus;

                if (_selectedInventoryRequest.InventoryRequestSerialsList != null)
                {
                    gvItem.DataSource = _selectedInventoryRequest.InventoryRequestSerialsList;
                    gvItem.DataBind();
                }

                pnlGRANItem.Enabled = false;
                string _masterLocation = (string.IsNullOrEmpty(GlbUserDefLoca)) ? GlbUserDefProf : GlbUserDefLoca;
                if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, _masterLocation, "INV2"))
                {
                    cpeGRANApp.Collapsed = true;
                    pnlGRANApp.Enabled = true;
                    btnApproved.Enabled = true;
                    btnReject.Enabled = true;
                }
                else
                {
                    pnlGRANApp.Enabled = false;
                    btnApproved.Enabled = false;
                    btnReject.Enabled = false;
                }

                //Set relavant buttons according to the GRAN status.
                if (_selectedInventoryRequest.Itr_stus.ToUpper().Equals("P"))
                {
                    btnSave.Enabled = true;
                    btnAddItem.Enabled = true;
                    btnCancel.Enabled = true;
                }
                if (_selectedInventoryRequest.Itr_stus.ToUpper().Equals("A"))
                {
                    if (!isGRN)
                    {
                        btnApproved.Enabled = false;
                        btnReject.Enabled = false;
                        btnCancel.Enabled = false;
                       
                    }
                }
                if (_selectedInventoryRequest.Itr_stus.ToUpper().Equals("R"))
                {

                        btnApproved.Enabled = false;
                        btnReject.Enabled = false;
                        btnCancel.Enabled = false;
                        
                    
                }
                if (_selectedInventoryRequest.Itr_stus.ToUpper().Equals("C"))
                {
                    btnSave.Enabled =  false ;
                    btnAddItem.Enabled = false ;
                    btnCancel.Enabled =  false ;
                    btnApproved.Enabled =  false ;
                }
                if(!isGRN)
                ddlRequestType.Enabled = false;
                btnSave.Enabled = false;

                if (isGRN) {
                    btnSave.Enabled = true;
                    btnApproved.Enabled = false;
                    btnReject.Enabled = false;
                }

                //set chk status
                //ddlRequestType_SelectedIndexChanged(null, null);
            }
            tcGRNContainer.ActiveTabIndex = 0;
        }

        private void Update(string status )
        {
            try
            {
                #region UI validation.

                if (IsAllowBackDateForModule(GlbUserComCode, GlbUserDefLoca, string.Empty, Page, Convert.ToDateTime(txtRequestDate.Text).ToString("dd/MMM/yyyy"), imgBtnDate, lblDispalyInfor) == false)
                {
                    if (Convert.ToDateTime(txtRequestDate.Text).Date != DateTime.Now.Date)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Back date not allow for selected date!");
                        return;
                    }
                }


                if (string.IsNullOrEmpty(GlbUserDefLoca))
                {
                    throw new UIValidationException("You have not allow any location for transaction.");
                }

                if (ddlRequestType.SelectedValue.Equals("-1"))
                {
                    ddlRequestType.Focus();
                    throw new UIValidationException("Please select Request Type.");
                }


                if (ddlNewStatus.SelectedValue.Equals("-1"))
                {
                    ddlNewStatus.Focus();
                    throw new UIValidationException("Please select change status.");
                }
                if (DropDownListAppStatus.SelectedValue.Equals("-1") && status=="A")
                {
                    DropDownListAppStatus.Focus();
                    throw new UIValidationException("Please select Approval status.");
                }
                if (ddlRequestType.SelectedValue == "GRAN")
                {
                    if (string.IsNullOrEmpty(txtTransferLocation.Text) || txtTransferLocation.Text.Trim().Equals("--Search Location--") || txtTransferLocation.Text.Trim().Equals(""))
                    {
                        txtTransferLocation.Focus();
                        throw new UIValidationException("Please enter transfer location.");
                    }
                    if (!this.IsValidLocation(txtTransferLocation.Text.ToString()))
                        throw new UIValidationException("Please enter valid Location.");
                }

               

                //if (string.IsNullOrEmpty(txtManualRef.Text))
                //{
                //    txtManualRef.Focus();
                //    throw new UIValidationException("Please enter manual referance.");
                //}

                if (string.IsNullOrEmpty(txtRemarks.Text.ToString()))
                {
                    throw new UIValidationException("Please enter remarks.");
                }

                if (chkSellAtShop.Checked == false && chkDiscount.Checked == false && chkToReport.Checked == false && chkToStores.Checked == false)
                {
                    chkSellAtShop.Focus();
                    throw new UIValidationException("Please select the request condition.");
                }

                if (_invreqserialList == null)
                {
                    throw new UIValidationException("Please add items to List.");
                }



                //Check back date
                //if (CHNLSVC.General.IsAllowBackDate(GlbUserComCode, GlbUserDefLoca, string.Empty, txtRequestDate.Text) == false)
                //{
                //    throw new UIValidationException("Back date not allow for selected date!");
                //}
                //else
                //{
                //    //txtRequestDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                //    //imgRequestDate.Visible = false;
                //}

                InventoryRequest _inputInvReq = new InventoryRequest();
                _inputInvReq.Itr_req_no = ReqNo;
                InventoryRequest _invreq = CHNLSVC.Inventory.GetInventoryRequestData(_inputInvReq);
                _invreqitmList = _invreq.InventoryRequestItemList;
                _invreqserialList = _invreq.InventoryRequestSerialsList;

                //if (!IsApp)
                //{
                //    Save(true);
                //    return;
                //}


                txtRequestDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                #endregion

            
                string _masterLocation = (string.IsNullOrEmpty(GlbUserDefLoca)) ? GlbUserDefProf : GlbUserDefLoca;
                //InventoryRequest _invreq = new InventoryRequest();
                //Fill the InventoryRequest header & footer data.
                _invreq.Itr_com = GlbUserComCode;
                _invreq.Itr_req_no = GetRequestNo();
               // _inventoryRequest.Itr_req_no = GetRequestNo();
                _invreq.Itr_tp = ddlRequestType.SelectedValue;
                //CHANGE
                _invreq.Itr_stus = status;  //P - Pending , A - Approved. 
                _invreq.Itr_sub_tp = "TEMP";
                _invreq.Itr_loc = _masterLocation;
                _invreq.Itr_ref = string.Empty;
                _invreq.Itr_dt = Convert.ToDateTime(txtRequestDate.Text);
                _invreq.Itr_exp_dt = Convert.ToDateTime(txtRequestDate.Text);
                _invreq.Itr_job_no = string.Empty;  //Invoice No.
                _invreq.Itr_bus_code = string.Empty;  //Customer Code.
                _invreq.Itr_note = txtRemarks.Text;
                _invreq.Itr_issue_from =  _masterLocation;
                _invreq.Itr_rec_to = txtTransferLocation.Text;
                _invreq.Itr_direct = 0;
                _invreq.Itr_country_cd = string.Empty;  //Counrty Code.
                _invreq.Itr_town_cd = string.Empty;     //Town Code.
                _invreq.Itr_cur_code = string.Empty;    //Currency Code.
                _invreq.Itr_exg_rate = 0;              //Exchange rate.
                _invreq.Itr_collector_id = string.Empty;
                _invreq.Itr_collector_name = string.Empty;
                _invreq.Itr_act = 1;
                _invreq.Itr_cre_by = GlbUserName;
                _invreq.InventoryRequestItemList = _invreqitmList;
                _invreq.Itr_session_id = GlbUserSessionID;
                _invreq.Itr_gran_app_by = GlbUserName;
                _invreq.Itr_gran_app_note = txtAppRemarks.Text;
                _invreq.Itr_gran_nstus = ddlNewStatus.SelectedValue;
                _invreq.Itr_ref = txtManualRef.Text;
                _invreq.Itr_issue_com = GlbUserComCode;
                _invreq.Itr_collector_name = txtFeildManager.Text;
                _invreq.Itr_gran_app_note = txtAppRemarks.Text;
                _invreq.Itr_gran_app_stus = DropDownListAppStatus.SelectedValue;
                if (chkSellAtShop.Checked)
                    _invreq.Itr_gran_opt1 = 1;
                if (chkDiscount.Checked)
                    _invreq.Itr_gran_opt2 = 1;
                if (chkToReport.Checked)
                    _invreq.Itr_gran_opt3 = 1;
                if (chkToStores.Checked)
                    _invreq.Itr_gran_opt4 = 1;

                int rowsAffected = 0;
                string _docNo = string.Empty;

                //get item serial list
                //List<InventoryRequestSerials> _invReqsER = CHNLSVC.Inventory.GetAllGRANSerialsList(GlbUserComCode, _masterLocation, ddlRequestType.SelectedValue, GetRequestNo());

                //change item status
                //if apoval status and item status are not eqal
                if (status == "A" && DropDownListAppStatus.SelectedValue!=_invreqserialList[0].Itrs_itm_stus && ddlRequestType.SelectedValue=="DIN")
                {
                    InventoryHeader invHdr_min = new InventoryHeader();
                    invHdr_min.Ith_com = GlbUserComCode;
                    //  invHdr_min.Ith_sbu has been assigned later
                    // invHdr_min.Ith_channel has been assigned later
                    invHdr_min.Ith_loc = GlbUserDefLoca;
                    invHdr_min.Ith_cate_tp = "STUS";
                    invHdr_min.Ith_is_manual = false;
                    invHdr_min.Ith_stus = "A";
                    invHdr_min.Ith_cre_by = GlbUserName;
                    invHdr_min.Ith_mod_by = GlbUserName;
                    invHdr_min.Ith_mod_when = DateTime.Now;
                    invHdr_min.Ith_direct = false;
                    invHdr_min.Ith_session_id = GlbUserSessionID;
                    invHdr_min.Ith_manual_ref = txtManualRef.Text;
                    invHdr_min.Ith_remarks = txtRemarks.Text;


                    invHdr_min.Ith_doc_year = DateTime.Now.Year;
                    invHdr_min.Ith_doc_date = DateTime.Now.Date;
                    invHdr_min.Ith_doc_tp = "ADJ";
                    invHdr_min.Ith_sub_tp = "STUS";
                    invHdr_min.Ith_entry_tp = "STUS";

                    InventoryHeader invHdr_plus = new InventoryHeader();
                    DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(GlbUserComCode, GlbUserDefLoca);
                    foreach (DataRow r in dt_location.Rows)
                    {
                        // Get the value of the wanted column and cast it to string
                        string sbu = (string)r["ML_OPE_CD"];
                        invHdr_plus.Ith_sbu = sbu;
                        invHdr_min.Ith_sbu = sbu;
                        string chennel = "";
                        if (System.DBNull.Value != r["ML_CATE_2"])
                        {

                            chennel = (string)r["ML_CATE_2"];
                        }

                        invHdr_plus.Ith_channel = chennel;
                        invHdr_min.Ith_channel = chennel;
                    }
                    invHdr_plus.Ith_loc = GlbUserDefLoca;
                    invHdr_plus.Ith_com = GlbUserComCode;
                    invHdr_plus.Ith_cate_tp = "STUS";
                    invHdr_plus.Ith_is_manual = false;
                    invHdr_plus.Ith_stus = "A";
                    invHdr_plus.Ith_cre_by = GlbUserName;
                    invHdr_plus.Ith_mod_by = GlbUserName;
                    invHdr_plus.Ith_mod_when = DateTime.Now;
                    invHdr_plus.Ith_direct = true;
                    invHdr_plus.Ith_session_id = GlbUserSessionID;
                    invHdr_plus.Ith_manual_ref = txtManualRef.Text;
                    invHdr_plus.Ith_remarks = txtRemarks.Text;

                    invHdr_plus.Ith_doc_year = DateTime.Now.Year;
                    invHdr_plus.Ith_doc_date = DateTime.Now.Date;
                    invHdr_plus.Ith_doc_tp = "ADJ";
                    invHdr_plus.Ith_sub_tp = "STUS";
                    invHdr_plus.Ith_entry_tp = "STUS";



                    MasterAutoNumber masterAuto_min = new MasterAutoNumber();
                    masterAuto_min.Aut_cate_cd = GlbUserDefLoca;
                    masterAuto_min.Aut_cate_tp = "LOC";
                    masterAuto_min.Aut_direction = null;
                    masterAuto_min.Aut_modify_dt = null;
                    masterAuto_min.Aut_moduleid = "ADJ";
                    masterAuto_min.Aut_number = 5;
                    masterAuto_min.Aut_start_char = "ADJ";
                    masterAuto_min.Aut_year = null;


                    MasterAutoNumber masterAuto_plus = new MasterAutoNumber();
                    masterAuto_plus.Aut_cate_cd = GlbUserDefLoca;
                    masterAuto_plus.Aut_cate_tp = "LOC";
                    masterAuto_plus.Aut_direction = null;
                    masterAuto_plus.Aut_modify_dt = null;
                    masterAuto_plus.Aut_moduleid = "ADJ";
                    masterAuto_plus.Aut_number = 5;
                    masterAuto_plus.Aut_start_char = "ADJ";
                    masterAuto_plus.Aut_year = null;

                    List<ReptPickSerialsSub> list_ReptPickSerialsSubList = new List<ReptPickSerialsSub>();
                    list_ReptPickSerialsSubList = CHNLSVC.Inventory.GetAllScanSubSerialsList(1, "ADJ-S");
                    string _minusDocNo = null; string _plusDocNo = null;
                    List<ReptPickSerials> list_GetAllScanSerialsList = new List<ReptPickSerials>();
                    //string _bin = CHNLSVC.Inventory.GetDefaultBinCode(GlbUserComCode, GlbUserDefLoca);
                    foreach (InventoryRequestSerials serial in _invreq.InventoryRequestSerialsList)
                    {
                        //ReptPickSerials _rept = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, _masterLocation, _bin, serial.Itrs_itm_cd, serial.Itrs_ser_1);
                        ReptPickSerials _rept = CHNLSVC.Inventory.Get_all_details_on_doc(GlbUserComCode, _masterLocation, serial.Itrs_itm_cd, serial.Itrs_in_docno, serial.Itrs_ser_id.ToString());

                        //if (_rept.Tus_doc_no == null) {
                        //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Item Serial and Item Code mismatch" + _docNo);
                        //    return;
                        //}
                        //COMMENTED 2013/01/12
                        _rept.Tus_base_doc_no = _invreq.Itr_req_no;
                        _rept.Tus_base_itm_line = serial.Itrs_in_itmline;
                        //_rept.Tus_doc_no = serial.Itrs_in_docno;
                        //_rept.Tus_seq_no = serial.Itrs_seq_no;
                        _rept.Tus_batch_line = serial.Itrs_in_batchline;
                        _rept.Tus_ser_line = serial.Itrs_in_serline;
                        _rept.Tus_new_status = DropDownListAppStatus.SelectedValue;
                        _rept.Tus_serial_id = serial.Itrs_ser_id.ToString();
                       // _rept.Tus_doc_dt = DateTime.Now.Date;
                        //END

                        //need get bin

                        list_GetAllScanSerialsList.Add(_rept);
                    }
                    if (string.IsNullOrEmpty(hdn_SelectedReqNo.Value))
                    {


                        Int16 affected = CHNLSVC.Inventory.InventoryStatusChange(invHdr_min, invHdr_plus, list_GetAllScanSerialsList, list_ReptPickSerialsSubList, masterAuto_min, masterAuto_plus, out _minusDocNo, out _plusDocNo);
                        if (affected > 0)
                        {
                            rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_invreq, GenerateMasterAutoNumber(), out _docNo);
                            this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Inventory Request Document Sucessfully saved. " + _docNo);
                            string Msg = "<script>alert('Inventory Request Document Sucessfully saved! Document No. : " + _docNo + "');window.location = 'GRAN_Note.aspx';</script>";
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                        }
                        else
                        {
                            this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Error occured while processing...");
                        }
                    }
                    else
                    {

                        Int16 affected = CHNLSVC.Inventory.InventoryStatusChange(invHdr_min, invHdr_plus, list_GetAllScanSerialsList, list_ReptPickSerialsSubList, masterAuto_min, masterAuto_plus, out _minusDocNo, out _plusDocNo);
                        if (affected > 0)
                        {
                            rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_invreq, null, out _docNo);
                            this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Inventory Request Document Sucessfully Updated.");
                            string Msg = "<script>alert('Inventory Request Document Sucessfully Updated!');window.location = 'GRAN_Note.aspx';</script>";
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                        }
                        else
                        {
                            this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Error occured while processing...");
                        }

                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(hdn_SelectedReqNo.Value))
                    {
                        //set serial status
                        if (status == "R" || status == "C")
                        {
                            bool res = CHNLSVC.Inventory.UpdateSerialIDAvailable(GlbUserComCode, GlbUserDefLoca, _invreq.InventoryRequestSerialsList[0].Itrs_itm_cd, _invreq.InventoryRequestSerialsList[0].Itrs_ser_id, -1, 1);
                        }
                        rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_invreq, GenerateMasterAutoNumber(), out _docNo);
                        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Inventory Request Document Sucessfully saved. " + _docNo);
                        string Msg = "<script>alert('Inventory Request Document Sucessfully saved! Document No. : " + _docNo + "');window.location = 'GRAN_Note.aspx';</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                    }
                    else
                    {
                        //set serial status
                        if (status == "R" || status == "C")
                        {
                            bool res = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, _invreq.InventoryRequestSerialsList[0].Itrs_itm_cd, _invreq.InventoryRequestSerialsList[0].Itrs_ser_id, 1);
                        }
                        rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_invreq, null, out _docNo);
                        this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Inventory Request Document Sucessfully Updated.");
                        string Msg = "<script>alert('Inventory Request Document Sucessfully Updated!');window.location = 'GRAN_Note.aspx';</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                    }
                }

               IsApp = false;
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Inventory Request Document Sucessfully saved. " + _docNo);
                //this.ClearPageData();
            }
            
            catch (UIValidationException ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.ErrorMessege);
            }
            catch (Exception e)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, e.Message);
            }
        }

        protected void ddlNewStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlNewStatus.SelectedValue == "CONS")
            //    ddlItemStatus.Enabled = false;
            //else
            //    ddlItemStatus.Enabled = true;
        }



        protected void chkToStores_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckStatus();
        }

        protected void chkToReport_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckStatus();
        }

        protected void chkSellAtShop_CheckedChanged(object sender, EventArgs e)
        {
            SetCheckStatus();
        }

        private void SetCheckStatus()
        {
            if (chkSellAtShop.Checked)
            {

                chkToStores.Enabled = false;
                chkToReport.Enabled = false;
                chkToStores.Checked = false;
                chkToReport.Checked = false;
            }
            else if (chkToReport.Checked)
            {
                chkSellAtShop.Enabled = false;
                chkDiscount.Enabled = false;
                chkToStores.Enabled = false;
                chkSellAtShop.Checked = false;
                chkDiscount.Checked = false;
                chkToStores.Checked = false;
            }
            else if (chkToStores.Checked)
            {
                chkSellAtShop.Enabled = false;
                chkDiscount.Enabled = false;
                chkToReport.Enabled = false;
                chkSellAtShop.Checked = false;
                chkDiscount.Checked = false;
                chkToReport.Checked = false;
            }
            else
            {
                chkToReport.Enabled = true;
                chkSellAtShop.Enabled = true;
                chkDiscount.Enabled = true;
                chkToStores.Enabled = true;
            }
        }

        protected void CheckBoxManual_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxManual.Checked) {
                string _masterLocation = (string.IsNullOrEmpty(GlbUserDefLoca)) ? GlbUserDefProf : GlbUserDefLoca;
                if(ddlRequestType.SelectedValue=="GRAN")
                txtManualRef.Text = CHNLSVC.Inventory.GetNextManualDocNo(GlbUserComCode, _masterLocation, "MDOC_GRAN").ToString();
                if (ddlRequestType.SelectedValue == "DIN")
                    txtManualRef.Text = CHNLSVC.Inventory.GetNextManualDocNo(GlbUserComCode, _masterLocation, "MDOC_DIN").ToString();
                if (ddlRequestType.SelectedValue == "-1")
                {
                    CheckBoxManual.Checked = false;
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select Request Type.");
                    txtManualRef.Text = "0";
                }
            }
        }

        protected void btnPrint_Click1(object sender, EventArgs e)
        {

            if (ReqNo != "" && ddlRequestType.SelectedValue=="GRAN")
            {
                GlbReportPath = "~\\Reports_Module\\Inv_Rep\\GRANPrint.rpt";
                GlbReportMapPath = "~/Reports_Module/Inv_Rep/GRANPrint.rpt";
                GlbSelectionFormula = "{INT_REQ.ITR_REQ_NO}='" + ReqNo + "'";
                //GlbMainPage = "~/General_Modules/VehicalInsurance.aspx";
                string Msg1 = "<script>window.open('../Reports_Module/Inv_Rep/GRANPrint.aspx','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);
            }
            if (ReqNo != "" && ddlRequestType.SelectedValue == "DIN")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Under construction.");
            }
            else {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Nothing to print.");
            }

        }

        public static void ResetFields(ControlCollection pageControls)
        {
            foreach (Control contl in pageControls)
            {
                var strCntName = (contl.GetType()).Name; switch (strCntName)
                {
                    case "TextBox":
                        var txtSource = (TextBox)contl;
                        txtSource.Text = "";
                        break;
                    case "ListBox":
                        var lstSource = (ListBox)contl;
                        lstSource.Items.Clear();
                        break;
                    case "DropDownList":
                        var cmbSource = (DropDownList)contl;
                        cmbSource.SelectedIndex = -1;
                        break;
                    case "GridView":
                        var dgvSource = (GridView)contl;
                        DataTable dt = new DataTable();
                        dgvSource.DataSource = dt;
                        dgvSource.DataBind();
                        break;
                    case "CheckBox":
                        var chkSource = (CheckBox)contl;
                        chkSource.Checked = false;
                        break;
                } ResetFields(contl.Controls);
            }
        }

        protected void DropDownListDINNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string req_no = DropDownListDINNo.SelectedValue;
            ReqNo = req_no;
            InventoryRequest _inputInvReq = new InventoryRequest();
            _inputInvReq.Itr_req_no = req_no;
            InventoryRequest _selectedInventoryRequest = CHNLSVC.Inventory.GetInventoryRequestData(_inputInvReq);
            this.SetSelectedInventoryRequestData(_selectedInventoryRequest,true);
            hdn_SelectedReqNo.Value = _selectedInventoryRequest.Itr_req_no;
            IsApp = true;
        }
     //END
    }
}