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


namespace FF.WebERPClient.Inventory_Module
{
    public partial class InterTransferDocument : BasePage
    {

        List<InventoryRequestItem> _invReqItemList = null;
        InventoryRequestItem _invReqItem = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetInitialValues();
                BindUserCompanyItemStatusDDLData(ddlItemStatus);
                //BindRequestTypesDDLData(ddlRequestType);
                BindRequestSubTypesDDLData(ddlRequestSubType);
                BindJavaScripts();
                //Test 
                BindMRNListGridData();
                BindEmptyMRNItemTable();
                gvMRNItems.DataBind();

                txtRequestBy.Text = Session["UserName"].ToString();
                string _masterLocation = (string.IsNullOrEmpty(GlbUserDefLoca)) ? GlbUserDefProf : GlbUserDefLoca;
                if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, _masterLocation, "INV3"))
                {
                    btnApproved.Enabled = true;
                    btnMRNSave.Enabled = false;
                    btnAddItemNew.Enabled = false;
                }
                else
                {
                    btnApproved.Enabled = false;
                    btnMRNSave.Enabled = true;
                    btnAddItemNew.Enabled = true;
                }
                

                //_invReqItemList = new List<InventoryRequestItem>();
                //_invReqItem = new InventoryRequestItem();
                //_invReqItemList.Add(_invReqItem);
                //gvMRNItems.DataSource = _invReqItemList;
                //gvMRNItems.DataBind();

            }
        }


        #region User Defined Methods

        private void BindCompanies()
        {
            ddlCompany.Items.Clear();
            //cmb_Company.Items.Add(new ListItem("--Select Company Code--", "-1"));
            ddlCompany.Items.Add(new ListItem("", "-1"));
            ddlCompany.DataSource = CHNLSVC.General.GetALLMasterCompaniesData();
            ddlCompany.DataTextField = "mc_cd";
            ddlCompany.DataValueField = "mc_cd";
            ddlCompany.DataBind();
       }

        private void SetInitialValues()
        {
            BindCompanies();
            ddlRequestSubType.Items.Clear();
            //ddlRequestSubType.Items.Add(new ListItem("--Select Sub Type--", "-1"));
            ddlRequestSubType.Items.Add(new ListItem(string.Empty, "-1"));
            //txtDispatchRequried.Text = "--Search Location--";
            txtDispatchRequried.Text = string.Empty;
            txtRequestBy.Text = CHNLSVC.Security.GetUserByUserID(GlbUserName).Se_usr_name;
            txtRequestDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            //txtRequriedDate.Text = "--Select Date--";
            txtRequriedDate.Text = string.Empty;
            txtQty.Text = "0";
            txtAvalQty.Text = "0";
            txtFreeQty.Text = "0";
            lblSelectedReqNo.Text = string.Empty;
            hdnSelectedReqNo.Value = string.Empty;
            hdnIsSubItem.Value = string.Empty;
            imgRequestDate.Visible = false;
            if (CHNLSVC.General.IsAllowBackDate(GlbUserComCode, GlbUserDefLoca, string.Empty, string.Empty) == true)
            {
                imgRequestDate.Visible = true;
            }
        }


        private void BindUserCompanyItemStatusDDLData(DropDownList ddl)
        {
            ddl.Items.Clear();
            //ddl.Items.Add(new ListItem("--Select--", "-1"));
            ddl.Items.Add(new ListItem(string.Empty, "-1"));
            ddl.DataSource = CHNLSVC.Inventory.GetAllCompanyStatus(GlbUserComCode);
            ddl.DataTextField = "MIS_DESC";
            ddl.DataValueField = "MIC_CD";
            ddl.DataBind();

            ddl.SelectedValue  = "GOD";
        }

        private void BindRequestTypesDDLData(DropDownList ddl)
        {
            ddl.Items.Clear();
            //ddl.Items.Add(new ListItem("--Select Request Type--", "-1"));
            ddl.Items.Add(new ListItem(string.Empty, "-1"));
            ddl.DataSource = CHNLSVC.General.GetAllType(CommonUIDefiniton.MasterTypeCategory.REQ.ToString());
            ddl.DataTextField = "Mtp_desc";
            ddl.DataValueField = "Mtp_cd";
            ddl.DataBind();
        }

        private void BindRequestSubTypesDDLData(DropDownList ddl)
        {
            ddl.DataSource = CHNLSVC.General.GetAllSubTypes("INTR");
            ddl.DataTextField = "mstp_desc";
            ddl.DataValueField = "mstp_cd";
            ddl.DataBind();
        }

        private void BindJavaScripts()
        {
            txtSearchItemCode.Attributes.Add("onblur", "GetItemData()");
            txtInvoiceNo.Attributes.Add("onblur", "GetInvoiceData()");

            txtReceiptNo.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnGetReceipt, ""));
            txtDispatchRequried.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnGetdispatchlocation, ""));

            txtSearchItemCode.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + ddlItemStatus.ClientID + "').focus();return false;}} else {return true}; ");
            ddlItemStatus.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtQty.ClientID + "').focus();return false;}} else {return true}; ");
            txtQty.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + txtRemarks.ClientID + "').focus();return false;}} else {return true}; ");
        }

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

                case CommonUIDefiniton.SearchUserControlType.Location: // Add Chamal 27/06/2012
                    {
                        //paramsText.Append(GlbUserComCode + seperator);
                        paramsText.Append(ddlCompany.SelectedItem.Text + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Receipt: // Add Chamal 27/06/2012
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + ddlRequestSubType.SelectedValue.ToUpper().ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private List<InventoryRequestItem> GetInventoryRequestItemList()
        {
            _invReqItemList = new List<InventoryRequestItem>();
            if (gvMRNItems.Rows.Count > 0)
            {
                for (int i = 0; i < gvMRNItems.Rows.Count; i++)
                {
                    _invReqItem = new InventoryRequestItem();

                    //Get MasterItem data.
                    MasterItem _mItem = new MasterItem();
                    _mItem.Mi_cd = ((Label)gvMRNItems.Rows[i].FindControl("lblItemCode")).Text;
                    _mItem.Mi_longdesc = ((Label)gvMRNItems.Rows[i].FindControl("lblDescription")).Text;
                    _mItem.Mi_model = ((Label)gvMRNItems.Rows[i].FindControl("lblModel")).Text;
                    _mItem.Mi_brand = ((Label)gvMRNItems.Rows[i].FindControl("lblBrand")).Text;
                    _invReqItem.MasterItem = _mItem;

                    _invReqItem.Itri_itm_stus = ((Label)gvMRNItems.Rows[i].FindControl("lblItemStatus")).Text;
                    _invReqItem.Itri_res_no = ((Label)gvMRNItems.Rows[i].FindControl("lblReservationNo")).Text;
                    _invReqItem.Itri_note = ((Label)gvMRNItems.Rows[i].FindControl("lblRemarks")).Text;
                    _invReqItem.Itri_qty = Convert.ToDecimal(((Label)gvMRNItems.Rows[i].FindControl("lblReqQty")).Text);
                    _invReqItem.Itri_unit_price = 0;
                    //_invReqItem.Itri_app_qty = Convert.ToDecimal(((TextBox)gvMRNItems.Rows[i].FindControl("txtAppQty")).Text);
                    _invReqItem.Itri_app_qty = 0;
                    _invReqItem.Itri_line_no = Convert.ToInt32(((HiddenField)gvMRNItems.Rows[i].FindControl("hdnlineNo")).Value);

                    _invReqItem.Itri_mitm_cd = ((HiddenField)gvMRNItems.Rows[i].FindControl("hdnMstItemCode")).Value;
                    _invReqItem.Itri_mitm_stus = ((HiddenField)gvMRNItems.Rows[i].FindControl("hdnMstItemStatus")).Value;
                    _invReqItem.Itri_mqty =   _invReqItem.Itri_bqty = _invReqItem.Itri_qty;Convert.ToDecimal(((HiddenField)gvMRNItems.Rows[i].FindControl("hdnMstQty")).Value);
                    _invReqItem.Itri_bqty = _invReqItem.Itri_qty;

                    _invReqItemList.Add(_invReqItem);
                }
            }
            return _invReqItemList;
        }

        private bool IsExistingItem(string _itemCode, string _itemStatus, string _resvationNo, List<InventoryRequestItem> _invRequestItemList)
        {
            bool result = false;
            List<InventoryRequestItem> _resultList = null;

            if (!string.IsNullOrEmpty(_resvationNo))
            {
                _resultList = _invRequestItemList.Where(x => x.MasterItem.Mi_cd.Equals(_itemCode) &&
                                          x.Itri_itm_stus.Equals(_itemStatus) && x.Itri_res_no.Equals(_resvationNo)).ToList();
            }
            else
            {
                _resultList = _invRequestItemList.Where(x => x.MasterItem.Mi_cd.Equals(_itemCode) &&
                                          x.Itri_itm_stus.Equals(_itemStatus)).ToList();
            }

            if (_resultList.Count > 0)
                result = true;

            return result;
        }

        private void BindInventoryRequestItemsGridData()
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearchItemCode.Text))
                {
                    txtSearchItemCode.Focus();
                    throw new UIValidationException("Please enter Item Code.");
                }

                if (!this.IsValidItem())
                {
                    txtSearchItemCode.Focus();
                    throw new UIValidationException("Please enter valid Item.");
                }

                if (ddlItemStatus.SelectedValue.Equals("-1"))
                {
                    ddlItemStatus.Focus();
                    throw new UIValidationException("Please select a Item Status.");
                }

                //if (string.IsNullOrEmpty(txtReservationNo.Text))
                //    throw new UIValidationException("Please enter Reservation Number.");

                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    txtQty.Focus();
                    throw new UIValidationException("Please enter Requried Quantity.");
                }

                if (Convert.ToDecimal(txtQty.Text.ToString()) <= 0)
                {
                    txtQty.Focus();
                    throw new UIValidationException("Please enter Valid Quantity.");
                }

                //Check Invoice 
                if (ddlRequestSubType.SelectedValue.ToString() == "SALES")
                {
                    if (string.IsNullOrEmpty(txtInvoiceNo.Text))
                    {
                        txtInvoiceNo.Focus();
                        throw new UIValidationException("Please enter invoice no.");
                    }

                    InvoiceItem _satitm = new InvoiceItem();
                    _satitm = CHNLSVC.Sales.GetPendingInvoiceItemsByItem(txtInvoiceNo.Text, txtSearchItemCode.Text);
                    if (_satitm != null)
                    {
                        if (_satitm.Sad_itm_cd.ToString().ToUpper() != txtSearchItemCode.Text.ToString().ToUpper())
                        {
                            txtSearchItemCode.Focus();
                            throw new UIValidationException("Selected item code does not exist in invoice.");
                        }
                    }
                    else
                    {
                        txtSearchItemCode.Focus();
                        throw new UIValidationException("Selected item code does not exist in invoice.");
                    }
                }


                //Get existing items details from the grid.
                List<InventoryRequestItem> _invRequestItemList = GetInventoryRequestItemList();
                string _mainItemCode = txtSearchItemCode.Text.Trim().ToUpper();
                string _itemStatus = ddlItemStatus.SelectedValue;
                string _reservationNo = string.IsNullOrEmpty(txtReservationNo.Text.Trim()) ? string.Empty : txtReservationNo.Text.Trim();
                decimal _mainItemQty = (string.IsNullOrEmpty(txtQty.Text)) ? 0 : Convert.ToDecimal(txtQty.Text.Trim());
                string _remarksText = string.IsNullOrEmpty(txtRemarks.Text.Trim()) ? string.Empty : txtRemarks.Text.Trim();
                bool _isSubItemHave = (string.IsNullOrEmpty(hdnIsSubItem.Value)) ? false : Convert.ToBoolean(hdnIsSubItem.Value);


                if ((!_isSubItemHave) && IsExistingItem(_mainItemCode, _itemStatus, _reservationNo, _invRequestItemList))
                {
                    txtSearchItemCode.Focus();
                    throw new UIValidationException("Item already added.");
                }

                //Check whether that Master Item have sub Items.
                if (_isSubItemHave)
                {
                    //Get the ralavent sub items.
                    List<MasterItemComponent> _itemComponentList = CHNLSVC.Inventory.GetItemComponents(_mainItemCode);

                    //This is a temporary colletion for newlly added items.
                    List<InventoryRequestItem> _resultList = null;

                    if ((_itemComponentList != null) && (_itemComponentList.Count > 0))
                    {
                        //Update qty for existing items.
                        foreach (MasterItemComponent _itemCompo in _itemComponentList)
                        {
                            _resultList = _invRequestItemList.Where(x => x.MasterItem.Mi_cd.Equals(_itemCompo.ComponentItem.Mi_cd)).ToList();

                            // If selected sub item exists in the grid,update the qty.
                            if ((_resultList != null) && (_resultList.Count > 0))
                            {
                                foreach (InventoryRequestItem _result in _resultList)
                                {
                                    _result.Itri_qty = _result.Itri_qty + (_mainItemQty * _itemCompo.Micp_qty);
                                }
                            }
                            else
                            {
                                // If selected sub item does not exists in the grid add it.
                                InventoryRequestItem _inventoryRequestItem = new InventoryRequestItem();
                                decimal _subItemQty = _mainItemQty * _itemCompo.Micp_qty;

                                MasterItem _componentItem = new MasterItem();
                                _componentItem.Mi_cd = _itemCompo.ComponentItem.Mi_cd;
                                _componentItem.Mi_longdesc = _itemCompo.ComponentItem.Mi_longdesc;
                                _componentItem.Mi_model = _itemCompo.ComponentItem.Mi_model;
                                _componentItem.Mi_brand = _itemCompo.ComponentItem.Mi_brand;
                                _inventoryRequestItem.MasterItem = _componentItem;

                                _inventoryRequestItem.Itri_itm_stus = _itemStatus;
                                _inventoryRequestItem.Itri_res_no = _reservationNo;
                                _inventoryRequestItem.Itri_note = _remarksText;
                                _inventoryRequestItem.Itri_qty = _subItemQty;
                                _inventoryRequestItem.Itri_app_qty = 0;

                                //Add Main item details.
                                _inventoryRequestItem.Itri_mitm_cd = _mainItemCode;
                                _inventoryRequestItem.Itri_mitm_stus = _itemStatus;
                                _inventoryRequestItem.Itri_mqty = _mainItemQty;

                                _invRequestItemList.Add(_inventoryRequestItem);
                            }
                        }
                    }
                }
                else
                {
                    //Add new item to existing list.
                    InventoryRequestItem _inventoryRequestItem = new InventoryRequestItem();

                    MasterItem _masterItem = new MasterItem();
                    _masterItem.Mi_cd = _mainItemCode;
                    _masterItem.Mi_longdesc = txtItemDescription.Text.Trim();
                    _masterItem.Mi_model = txtModelNo.Text.Trim();
                    _masterItem.Mi_brand = txtBrand.Text.Trim();
                    _inventoryRequestItem.MasterItem = _masterItem;

                    _inventoryRequestItem.Itri_itm_stus = _itemStatus;
                    _inventoryRequestItem.Itri_res_no = _reservationNo;
                    _inventoryRequestItem.Itri_note = _remarksText;
                    _inventoryRequestItem.Itri_qty = _mainItemQty;
                    _inventoryRequestItem.Itri_app_qty = 0;

                    //Add Main item details.
                    _inventoryRequestItem.Itri_mitm_cd = string.Empty;
                    _inventoryRequestItem.Itri_mitm_stus = string.Empty;
                    _inventoryRequestItem.Itri_mqty = 0;

                    _invRequestItemList.Add(_inventoryRequestItem);

                }

                //Clear add new data.
                ClearItemData();

                //Bind the updated list to grid.
                gvMRNItems.DataSource = _invRequestItemList;
                gvMRNItems.DataBind();

                txtSearchItemCode.Focus();

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

        private void ClearItemData()
        {
            txtSearchItemCode.Text = string.Empty;
            txtItemDescription.Text = string.Empty;
            txtBrand.Text = string.Empty;
            txtModelNo.Text = string.Empty;
            ddlItemStatus.SelectedIndex = 0;
            txtReservationNo.Text = "N/A";
            txtQty.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            hdnIsSubItem.Value = string.Empty;

        }

        private void RemoveItemFromGrid(int rowIndex)
        {
            List<InventoryRequestItem> _invRequestItemList = GetInventoryRequestItemList();
            _invRequestItemList.RemoveAt(rowIndex);

            gvMRNItems.DataSource = _invRequestItemList;

            gvMRNItems.DataBind();
        }



        private void LoadEditData(int rowIndex)
        {
            //Get the selected item from list.
            List<InventoryRequestItem> _invRequestItemList = GetInventoryRequestItemList();
            InventoryRequestItem _inventoryRequestItem = _invRequestItemList[rowIndex];

            //Set selected data.
            txtSearchItemCode.Text = _inventoryRequestItem.MasterItem.Mi_cd;
            txtItemDescription.Text = _inventoryRequestItem.MasterItem.Mi_longdesc;
            txtBrand.Text = _inventoryRequestItem.MasterItem.Mi_brand;
            txtModelNo.Text = _inventoryRequestItem.MasterItem.Mi_model;
            ddlItemStatus.SelectedValue = _inventoryRequestItem.Itri_itm_stus;

            txtReservationNo.Text = _inventoryRequestItem.Itri_res_no;
            txtQty.Text = _inventoryRequestItem.Itri_qty.ToString();
            txtRemarks.Text = _inventoryRequestItem.Itri_note;

            //Tempory remove the selected item from the grid. 
            RemoveItemFromGrid(rowIndex);

        }


        private string GetRequestNo()
        {
            string _reqNo = string.Empty;

            if (string.IsNullOrEmpty(hdnSelectedReqNo.Value))
                _reqNo = "DOC-" + DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();
            else
                _reqNo = hdnSelectedReqNo.Value;

            return _reqNo;
        }

        #region Save
        private void SaveInventoryRequestData()
        {
            try
            {
                //UI validation.
                if (ddlRequestSubType.SelectedValue.Equals("-1"))
                {
                    ddlRequestSubType.Focus();
                    throw new UIValidationException("Please select Request Sub Type.");
                }

                if (ddlCompany.SelectedValue.Equals("-1"))
                { 
                    ddlCompany.Focus();
                    throw new UIValidationException("Please select dispatch requried company.");
                }

                if (string.IsNullOrEmpty(txtDispatchRequried.Text) || txtDispatchRequried.Text.Trim().Equals("--Search Location--") || txtDispatchRequried.Text.Trim().Equals(""))
                {
                    txtDispatchRequried.Focus();
                    throw new UIValidationException("Please enter dispatch requried location.");
                }

                if (txtDispatchRequried.Text.ToString().ToUpper() == GlbUserDefLoca.ToString().ToUpper())
                {
                    txtDispatchRequried.Focus();
                    throw new UIValidationException("Please enter valid dispatch requried location.");
                }

                //MasterUserLocation Or MasterUserProfitCenter.

                if (!this.IsValidLocation())
                    throw new UIValidationException("Please enter valid Location.");

                if (string.IsNullOrEmpty(txtRequestDate.Text))
                {
                    txtRequestDate.Focus();
                    throw new UIValidationException("Please enter Request Date.");
                }

                if (DateTime.Compare(Convert.ToDateTime(txtRequestDate.Text.Trim()), DateTime.Now.Date) < 0)
                {
                    //txtRequestDate.Focus();
                    //throw new UIValidationException("Request date can't be less than current date.");
                    txtRequestDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                }

                if (string.IsNullOrEmpty(txtRequriedDate.Text) || txtRequriedDate.Text.Trim().Equals("--Select Date--"))
                {
                    txtRequestDate.Focus();
                    throw new UIValidationException("Please enter Requried Date.");
                }
                if (DateTime.Compare(Convert.ToDateTime(txtRequriedDate.Text.Trim()), Convert.ToDateTime(txtRequestDate.Text.Trim())) < 0)
                {
                    txtRequestDate.Focus();
                    throw new UIValidationException("Requried date can't be less than request date.");
                }

                if (ddlRequestSubType.SelectedValue.ToUpper().ToString() == "SALES")
                {
                    if (string.IsNullOrEmpty(txtInvoiceNo.Text))
                    {
                        txtInvoiceNo.Focus();
                        throw new UIValidationException("Please enter Invoice No.");
                    }
                    if (!this.IsValidInvoice())
                    {
                        txtInvoiceNo.Focus();
                        throw new UIValidationException("Please enter valid Invoice No.");
                    }

                }
                else if (ddlRequestSubType.SelectedValue.ToUpper().ToString() == "ADVAN")
                {
                    if (string.IsNullOrEmpty(txtReceiptNo.Text))
                    {
                        txtReceiptNo.Focus();
                        throw new UIValidationException("Please enter Receipt No.");
                    }
                }
                else if (ddlRequestSubType.SelectedValue.ToUpper().ToString() == "DISP")
                {

                }

                List<InventoryRequestItem> _inventoryRequestItemList = GetInventoryRequestItemList();
                if ((_inventoryRequestItemList == null) || (_inventoryRequestItemList.Count == 0))
                {
                    txtSearchItemCode.Focus();
                    throw new UIValidationException("Please add items to List.");
                }
                if (string.IsNullOrEmpty(txtCollectorNIC.Text))
                {
                    txtCollectorNIC.Text = string.Empty;
                }
                else
                {
                    if (IsValidNIC(txtCollectorNIC.Text.ToString()) == false)
                    {
                        txtCollectorNIC.Focus();
                        throw new UIValidationException("Please enter valid NIC No.");
                    }
                }


                string _masterLocation = (string.IsNullOrEmpty(GlbUserDefLoca)) ? GlbUserDefProf : GlbUserDefLoca;
                InventoryRequest _inventoryRequest = new InventoryRequest();

                //Fill the InventoryRequest header & footer data.
                _inventoryRequest.Itr_com = GlbUserComCode;
                _inventoryRequest.Itr_req_no = GetRequestNo();
                //_inventoryRequest.Itr_tp = ddlRequestType.SelectedValue;
                _inventoryRequest.Itr_tp = "INTR";
                _inventoryRequest.Itr_sub_tp = ddlRequestSubType.SelectedValue;
                _inventoryRequest.Itr_loc = _masterLocation;
                _inventoryRequest.Itr_ref = string.Empty;
                _inventoryRequest.Itr_dt = Convert.ToDateTime(txtRequestDate.Text);
                _inventoryRequest.Itr_exp_dt = Convert.ToDateTime(txtRequriedDate.Text);
                _inventoryRequest.Itr_stus = "P";  //P - Pending , A - Approved, C - Cancel
                if (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "SALES")
                {
                    _inventoryRequest.Itr_job_no = txtInvoiceNo.Text;  //Invoice No.
                    _inventoryRequest.Itr_bus_code = txtCustomerCode.Text;  //Customer Code.
                }
                else if (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "ADVAN")
                {
                    _inventoryRequest.Itr_job_no = txtReceiptNo.Text;  //Invoice No.
                    _inventoryRequest.Itr_bus_code = txtReceiptCustCode.Text;  //Customer Code.
                }
                else if (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "DISP")
                {
                    _inventoryRequest.Itr_job_no = "DISP";
                }
                _inventoryRequest.Itr_note = txtInvReqRemarks.Text;
                _inventoryRequest.Itr_issue_from = txtDispatchRequried.Text;
                _inventoryRequest.Itr_rec_to = _masterLocation;
                _inventoryRequest.Itr_direct = 0;
                _inventoryRequest.Itr_country_cd = string.Empty;  //Counrty Code.
                _inventoryRequest.Itr_town_cd = string.Empty;     //Town Code.
                _inventoryRequest.Itr_cur_code = string.Empty;    //Currency Code.
                _inventoryRequest.Itr_exg_rate = 0;              //Exchange rate.
                _inventoryRequest.Itr_collector_id = string.IsNullOrEmpty(txtCollectorNIC.Text) ? string.Empty : txtCollectorNIC.Text.Trim();
                _inventoryRequest.Itr_collector_name = string.IsNullOrEmpty(txtCollectorName.Text) ? string.Empty : txtCollectorName.Text.Trim();
                _inventoryRequest.Itr_act = 1;
                _inventoryRequest.Itr_cre_by = GlbUserName;
                _inventoryRequest.Itr_session_id = GlbUserSessionID;
                _inventoryRequest.Itr_issue_com = ddlCompany.SelectedValue.ToString();

                _inventoryRequest.InventoryRequestItemList = _inventoryRequestItemList;
                int rowsAffected = 0;
                string _docNo = string.Empty;

                if (string.IsNullOrEmpty(hdnSelectedReqNo.Value))
                {
                    rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_inventoryRequest, GenerateMasterAutoNumber(), out _docNo);
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Inventory Request Document Sucessfully saved. " + _docNo);
                    string Msg = "<script>alert('Inventory Request Document Sucessfully saved! Document No. : " + _docNo + "');window.location = 'InterTransferDocument.aspx';</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                }
                else
                {
                    rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_inventoryRequest, null, out _docNo);
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Inventory Request Document Sucessfully Updated.");
                    string Msg = "<script>alert('Inventory Request Document Sucessfully Updated!');window.location = 'InterTransferDocument.aspx';</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                }

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
        #endregion

        #region Approved
        private void ApprovedSelectedRequest()
        {
            try
            {
                if (string.IsNullOrEmpty(hdnSelectedReqNo.Value))
                    throw new UIValidationException("Please select request before Approved.");

                InventoryRequest _inputInvReq = new InventoryRequest();
                _inputInvReq.Itr_com = GlbUserComCode;
                _inputInvReq.Itr_loc = GlbUserDefLoca;
                _inputInvReq.Itr_req_no = hdnSelectedReqNo.Value;
                _inputInvReq.Itr_stus = "A";
                _inputInvReq.Itr_mod_by = GlbUserName;
                _inputInvReq.Itr_session_id = GlbUserSessionID;

                int result = CHNLSVC.Inventory.UpdateInventoryRequestStatus(_inputInvReq);

                if (result > 0)
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Inventory Request " + _inputInvReq.Itr_req_no + " Sucessfully Approved.");
                    string Msg = "<script>alert('Inventory Request " + _inputInvReq.Itr_req_no + " sucessfully Approved!');window.location = 'InterTransferDocument.aspx';</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                }
                else
                {
                    throw new UIValidationException("Inventory Request " + _inputInvReq.Itr_req_no + " can't be Approved.");
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
        #endregion

        private MasterAutoNumber GenerateMasterAutoNumber()
        {
            string moduleText = "INTR";

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

        private void BindMRNListGridData()
        {
            InventoryRequest _inventoryRequest = new InventoryRequest();
            _inventoryRequest.Itr_com = GlbUserComCode;
            _inventoryRequest.Itr_loc = MasterUserLocation;
            _inventoryRequest.FromDate = null;
            _inventoryRequest.ToDate = null;
            _inventoryRequest.Itr_stus = null;
            _inventoryRequest.Itr_cre_by = null;

            List<InventoryRequest> _reqHdr = new List<InventoryRequest>();
            _reqHdr = CHNLSVC.Inventory.GetAllInventoryRequestData(_inventoryRequest);

            if (_reqHdr == null)
            {
                BindEmptyMRNListTable();
            }
            else
            {
                gvMRNList.DataSource = _reqHdr;
            }

            gvMRNList.DataBind();
        }

        #region Bind Empty Table
        private void BindEmptyMRNItemTable()
        {
            //Add Chamal 06/06/2012
            DataTable _table = CHNLSVC.Inventory.GetInventoryRequestItemDataByReqNoTable("");
            gvMRNItems.DataSource = _table;
        }

        private void BindEmptyMRNListTable()
        {
            //Add Chamal 06/06/2012
            InventoryRequest _inventoryRequest = new InventoryRequest();
            DataTable _table = CHNLSVC.Inventory.GetAllInventoryRequestDataTable(_inventoryRequest);
            _table.Columns.AddRange(new DataColumn[] { new DataColumn("SubTpDesc"), new DataColumn("Itr_dt") });
            gvMRNList.DataSource = _table;
        }
        #endregion

        private void GetSearchData()
        {
            InventoryRequest _inventoryRequest = new InventoryRequest();
            _inventoryRequest.Itr_com = GlbUserComCode;
            _inventoryRequest.Itr_loc = MasterUserLocation;
            _inventoryRequest.Itr_tp = "INTR";

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
            _reqHdr = CHNLSVC.Inventory.GetAllInventoryRequestData(_inventoryRequest);

            gvMRNList.DataSource = _reqHdr;
            if (_reqHdr == null)
            {
                BindEmptyMRNListTable();
            }
            gvMRNList.DataBind();

        }


        private void SetSelectedInventoryRequestData(InventoryRequest _selectedInventoryRequest)
        {
            if (_selectedInventoryRequest != null)
            {
                //Set Header details.
                lblSelectedReqNo.Text = "&nbsp" + "Request No . . . . . . " + "<b><font color=\"#0000FF\">" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + _selectedInventoryRequest.Itr_req_no + "</font></b>";
                hdnSelectedReqNo.Value = _selectedInventoryRequest.Itr_req_no;
                //ddlRequestType.SelectedValue = _selectedInventoryRequest.Itr_tp;
                BindRequestSubTypesDDLData(ddlRequestSubType);
                ddlRequestSubType.SelectedValue = _selectedInventoryRequest.Itr_sub_tp;
                ddlCompany.SelectedValue = _selectedInventoryRequest.Itr_issue_com; //add chamal 17-08-2012
                txtDispatchRequried.Text = _selectedInventoryRequest.Itr_issue_from;
                txtRequestDate.Text = _selectedInventoryRequest.Itr_dt.ToString("dd/MMM/yyyy");
                txtRequriedDate.Text = _selectedInventoryRequest.Itr_exp_dt.ToString("dd/MMM/yyyy");
                txtInvoiceNo.Text = _selectedInventoryRequest.Itr_job_no;
                txtCollectorNIC.Text = _selectedInventoryRequest.Itr_collector_id;
                txtCollectorName.Text = _selectedInventoryRequest.Itr_collector_name;
                txtInvReqRemarks.Text = _selectedInventoryRequest.Itr_note;

                //Set Item details.
                if (_selectedInventoryRequest.InventoryRequestItemList != null)
                {
                    gvMRNItems.DataSource = _selectedInventoryRequest.InventoryRequestItemList;
                    gvMRNItems.DataBind();
                }

                tcMRNContainer.ActiveTabIndex = 0;

                ClearRequestSubType();

                if (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "SALES")
                {
                    divInvoiceData.Visible = true;
                    txtInvoiceNo.Text = _selectedInventoryRequest.Itr_job_no;
                    IsValidInvoice();
                }
                else if (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "ADVAN")
                {
                    divAdvanceData.Visible = true;
                    txtReceiptNo.Text = _selectedInventoryRequest.Itr_job_no;
                    LoadReceiptDetails();
                }
                else if (ddlRequestSubType.SelectedValue.ToString().ToUpper() == "DISP")
                {
                    divDisplayData.Visible = true;
                    chkForDisplay.Checked = true;
                }

                //Set relavant buttons according to the MRN status.
                //btnMRNSave.Enabled = (_selectedInventoryRequest.Itr_stus.ToUpper().Equals("P")) ? true : false;
                //btnAddItemNew.Enabled = (_selectedInventoryRequest.Itr_stus.ToUpper().Equals("P")) ? true : false;
                //btnMRNCancel.Enabled = (_selectedInventoryRequest.Itr_stus.ToUpper().Equals("P")) ? true : false;

                if (_selectedInventoryRequest.Itr_stus.ToUpper() == "A" || _selectedInventoryRequest.Itr_stus.ToUpper() == "F" || _selectedInventoryRequest.Itr_stus.ToUpper() == "C")
                {
                    btnApproved.Enabled = false;
                    btnMRNSave.Enabled = false;
                    btnMRNCancel.Enabled = false;
                }
                else if (_selectedInventoryRequest.Itr_stus.ToUpper() =="P")
                {
                    btnApproved.Enabled = false;
                    btnMRNSave.Enabled = true;
                    string _masterLocation = (string.IsNullOrEmpty(GlbUserDefLoca)) ? GlbUserDefProf : GlbUserDefLoca;
                    if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, _masterLocation, "INV3"))
                    {
                        btnApproved.Enabled = true;
                        btnMRNSave.Enabled = false;
                        btnAddItemNew.Enabled = false;
                    }
                }
                
            }
        }

        private void ClearPageData()
        {
            //ddlRequestType.SelectedIndex = 0;
            ddlRequestSubType.Items.Clear();
            //txtDispatchRequried.Text = "--Search Location--";
            txtDispatchRequried.Text = "";
            txtInvoiceNo.Text = string.Empty;
            txtRequestDate.Text = string.Empty;
            //txtRequriedDate.Text = "--Select Date--";
            txtRequriedDate.Text = "";
            ClearItemData();
            txtRequestBy.Text = string.Empty;
            txtCollectorNIC.Text = string.Empty;
            txtCollectorName.Text = string.Empty;
            txtInvReqRemarks.Text = string.Empty;

            //btnMRNSave.Enabled = true;
            //btnAddItemNew.Enabled = true;
            //btnMRNCancel.Enabled = false;

            lblSelectedReqNo.Text = string.Empty;
            hdnSelectedReqNo.Value = string.Empty;
            this.SetInitialValues();


            BindEmptyMRNListTable();
            BindEmptyMRNItemTable();
            gvMRNList.DataBind();
            gvMRNItems.DataBind();
        }

        //Validate user entered Invoice is valid or not.
        private bool IsValidInvoice()
        {
            bool status = false;
            if ((divInvoiceData.Visible) && (!string.IsNullOrEmpty(txtInvoiceNo.Text)))
            {
                InvoiceHeader _invoiceHeader = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoiceNo.Text.Trim());
                status = (_invoiceHeader == null) ? false : true;
                if (_invoiceHeader != null)
                {
                    txtCustomerCode.Text = _invoiceHeader.Sah_cus_cd;
                    txtCustomerName.Text = _invoiceHeader.Sah_cus_name;
                    txtAddress1.Text = _invoiceHeader.Sah_d_cust_add1;
                    txtAddress2.Text = _invoiceHeader.Sah_d_cust_add2;
                }
                else
                {
                    txtInvoiceNo.Text = string.Empty;
                    txtCustomerCode.Text = string.Empty;
                    txtCustomerName.Text = string.Empty;
                    txtAddress1.Text = string.Empty;
                    txtAddress2.Text = string.Empty;
                }
            }
            return status;
        }

        //Validate user entered Item is valid or not.
        private bool IsValidItem()
        {
            bool status = false;
            if (!string.IsNullOrEmpty(txtSearchItemCode.Text))
            {
                MasterItem _masterItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtSearchItemCode.Text.Trim().ToUpper());
                status = (_masterItem == null) ? false : true;
            }
            return status;
        }

        //GetLocationByLocCode
        private bool IsValidLocation()
        {
            bool status = false;
            if (!string.IsNullOrEmpty(txtDispatchRequried.Text))
            {
                txtDispatchRequried.Text = txtDispatchRequried.Text.Trim().ToUpper().ToString();
                //MasterLocation _masterLocation = CHNLSVC.General.GetLocationByLocCode(GlbUserComCode, txtDispatchRequried.Text.ToString());
                MasterLocation _masterLocation = CHNLSVC.General.GetLocationByLocCode(ddlCompany.SelectedValue.ToString().ToUpper(), txtDispatchRequried.Text.ToString());
                status = (_masterLocation == null) ? false : true;
            }
            return status;
        }

        private void CancelSelectedRequest()
        {
            try
            {
                if (string.IsNullOrEmpty(hdnSelectedReqNo.Value))
                    throw new UIValidationException("Please select request before cancel.");

                if (DateTime.Compare(Convert.ToDateTime(txtRequestDate.Text.Trim()), DateTime.Now.Date) != 0)
                    throw new UIValidationException("Request date should be current date in order to Cancel.");

                InventoryRequest _inputInvReq = new InventoryRequest();
                _inputInvReq.Itr_com = GlbUserComCode;
                _inputInvReq.Itr_loc = GlbUserDefLoca;
                _inputInvReq.Itr_req_no = hdnSelectedReqNo.Value;
                _inputInvReq.Itr_stus = "C";
                _inputInvReq.Itr_mod_by = GlbUserName;
                _inputInvReq.Itr_session_id = GlbUserSessionID;

                int result = CHNLSVC.Inventory.UpdateInventoryRequestStatus(_inputInvReq);

                if (result > 0)
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Inventory Request " + _inputInvReq.Itr_req_no + " sucessfully Cancel.");
                    string Msg = "<script>alert('Inventory Request " + _inputInvReq.Itr_req_no + " sucessfully Cancel!');window.location = 'InterTransferDocument.aspx';</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                }
                else
                {
                    throw new UIValidationException("Inventory Request " + _inputInvReq.Itr_req_no + " can't be Cancel.");
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

        private void SetSHCurrentBalances()
        {
            try
            {
                //UI validation.
                if (string.IsNullOrEmpty(txtSearchItemCode.Text))
                    throw new UIValidationException("Please enter Item Code.");

                if (ddlItemStatus.SelectedValue.Equals("-1"))
                    throw new UIValidationException("Please select a Item Status.");

                List<InventoryLocation> _inventoryLocationList = CHNLSVC.Inventory.GetItemInventoryBalance(GlbUserComCode, GlbUserDefLoca, txtSearchItemCode.Text.Trim().ToUpper(), ddlItemStatus.SelectedValue);

                if ((_inventoryLocationList != null) && (_inventoryLocationList.Count > 0))
                {
                    txtAvalQty.Text = _inventoryLocationList[0].Inl_qty.ToString();
                    txtFreeQty.Text = _inventoryLocationList[0].Inl_free_qty.ToString();
                }
                else
                {
                    txtAvalQty.Text = "0";
                    txtFreeQty.Text = "0";
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
        #endregion

        protected void gvMRNList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMRNList.PageIndex = e.NewPageIndex;
            this.GetSearchData();
        }

        protected void ddlRequestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //divInvoiceData.Visible = (ddlRequestType.SelectedValue.ToUpper().Equals("INTR")) ? true : false;
            BindRequestSubTypesDDLData(ddlRequestSubType);
        }

        protected void ImgItemSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable dataSource = CHNLSVC.CommonSearch.GetItemSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtSearchItemCode.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgDispatchRequried_Click(object sender, ImageClickEventArgs e)
        {
            if (!ddlCompany.SelectedValue.Equals("-1"))
            {
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                //DataTable dataSource = CHNLSVC.CommonSearch.GetUserLocationSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
                DataTable dataSource = CHNLSVC.CommonSearch.GetLocationSearchData(MasterCommonSearchUCtrl.SearchParams, null, null); //Edit Chamal 25/06/2012
                MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
                MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
                MasterCommonSearchUCtrl.ReturnResultControl = txtDispatchRequried.ClientID;
                MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
            }
        }

        protected void imgInvoiceNo_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesInvoice);
            DataTable dataSource = CHNLSVC.CommonSearch.GetInvoiceSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtInvoiceNo.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgReceiptNo_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Receipt);
            DataTable dataSource = CHNLSVC.CommonSearch.GetReceiptsByType(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtReceiptNo.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            BindInventoryRequestItemsGridData();
        }


        protected void gvMRNItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "DELETEITEM":
                    {
                        ImageButton imgbtndelete = (ImageButton)e.CommandSource;
                        GridViewRow gvr = (GridViewRow)imgbtndelete.NamingContainer;
                        int rowIndex = gvr.RowIndex;
                        RemoveItemFromGrid(rowIndex);
                        break;
                    }

                case "EDITITEM":
                    {
                        ImageButton imgbtnEdit = (ImageButton)e.CommandSource;
                        GridViewRow gvr = (GridViewRow)imgbtnEdit.NamingContainer;
                        int rowIndex = gvr.RowIndex;
                        LoadEditData(rowIndex);
                        break;
                    }

                default:
                    break;
            }
        }


        protected void gvMRNList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "SELECTINVREQ":
                    {
                        string req_no = e.CommandArgument.ToString();
                        InventoryRequest _inputInvReq = new InventoryRequest();
                        _inputInvReq.Itr_req_no = req_no;
                        InventoryRequest _selectedInventoryRequest = CHNLSVC.Inventory.GetInventoryRequestData(_inputInvReq);
                        this.SetSelectedInventoryRequestData(_selectedInventoryRequest);
                        //updPnlMRNDataEntry.Update();
                        break;
                    }

                default:
                    break;
            }
        }


        protected void gvMRNItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnlineNo = (HiddenField)e.Row.FindControl("hdnlineNo");
                if (hdnlineNo != null)
                {
                    int rowIndex = e.Row.RowIndex + 1;
                    hdnlineNo.Value = rowIndex.ToString();
                }
            }
        }

        protected void btnMRNSave_Click(object sender, EventArgs e)
        {
            this.SaveInventoryRequestData();
        }

        protected void btnMRNCancel_Click(object sender, EventArgs e)
        {
            this.CancelSelectedRequest();
        }

        protected void btnMRNApproved_Click(object sender, EventArgs e)
        {
            this.ApprovedSelectedRequest();
        }

        protected void btnMRNClear_Click(object sender, EventArgs e)
        {
            //this.ClearPageData();
            Response.Redirect("~/Inventory_Module/InterTransferDocument.aspx", false);
        }

        protected void btnMRNClose_Click(Object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        protected void imgbtnRequestSearch_Click(object sender, ImageClickEventArgs e)
        {
            GetSearchData();
        }

        protected void ddlItemStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSHCurrentBalances();
        }

        #region Regular Expressions
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";

            System.Text.RegularExpressions.Match match = Regex.Match(email.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                //MessageBox.Show("Success","Checking");
                return true;
            else
                //MessageBox.Show("Fail", "Checking");
                return false;
        }

        private bool IsValidNIC(string nic)
        {
            string pattern = @"^[0-9]{9}[V]{1}$";

            System.Text.RegularExpressions.Match match = Regex.Match(nic.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                //MessageBox.Show("Success","Checking");
                return true;
            else
                //MessageBox.Show("Fail", "Checking");
                return false;
        }
        #endregion

        protected void ddlRequestSubType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearRequestSubType();
            if (ddlRequestSubType.SelectedValue.ToUpper().ToString() == "SALES")
            {
                divInvoiceData.Visible = true;
                txtInvoiceNo.Focus();
            }
            else if (ddlRequestSubType.SelectedValue.ToUpper().ToString() == "ADVAN")
            {
                divAdvanceData.Visible = true;
                txtReceiptNo.Focus();
            }
            else if (ddlRequestSubType.SelectedValue.ToUpper().ToString() == "DISP")
            {
                divDisplayData.Visible = true;
                chkForDisplay.Checked = true;
            }

            //BindRequestSubTypesDDLData(ddlRequestSubType);
        }

        private void ClearRequestSubType()
        {
            divInvoiceData.Visible = false;
            divAdvanceData.Visible = false;
            divDisplayData.Visible = false;
            chkForDisplay.Checked = false;
            txtInvoiceNo.Text = string.Empty;
            txtCustomerCode.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtAddress1.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtReceiptNo.Text = string.Empty;
            txtReceiptCustCode.Text = string.Empty;
            txtReceiptCustName.Text = string.Empty;
            txtReceiptAdd1.Text = string.Empty;
            txtReceiptAdd2.Text = string.Empty;
        }

        protected void GetReceiptDetails(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtReceiptNo.Text))
            {
                LoadReceiptDetails();
            }

        }

        protected void GetGetdispatchlocation(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDispatchRequried.Text))
            {
                if (IsValidLocation() == false)
                {
                    txtDispatchRequried.Text = string.Empty;
                    txtDispatchRequried.Focus();
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid Location");
                }
            }

        }

        private void LoadReceiptDetails()
        {
            RecieptHeader _ReceiptHeader = null;
            _ReceiptHeader = CHNLSVC.Sales.GetReceiptHeaderByType(GlbUserComCode, GlbUserDefProf, txtReceiptNo.Text.Trim(), ddlRequestSubType.SelectedValue.ToUpper().ToString());
            if (_ReceiptHeader != null)
            {
                //txtDate.Text = Convert.ToDateTime(_ReceiptHeader.Sar_receipt_date).ToShortDateString();
                //txtRefNo.Text = _ReceiptHeader.Sar_manual_ref_no;
                //txtRemarks.Text = _ReceiptHeader.Sar_remarks;
                //txtTotal.Text = Convert.ToDecimal(_ReceiptHeader.Sar_tot_settle_amt).ToString("0,0.00", CultureInfo.InvariantCulture);
                txtReceiptCustCode.Text = _ReceiptHeader.Sar_debtor_cd;
                txtReceiptCustName.Text = _ReceiptHeader.Sar_debtor_name;
                txtReceiptAdd1.Text = _ReceiptHeader.Sar_debtor_add_1;
                txtReceiptAdd2.Text = _ReceiptHeader.Sar_debtor_add_2;
                //txtMob.Text = _ReceiptHeader.Sar_mob_no;
                //txtNIC.Text = _ReceiptHeader.Sar_nic_no;
                //txtProvince.Text = _ReceiptHeader.Sar_anal_2;
            }
            else
            {
                txtReceiptCustCode.Text = string.Empty;
                txtReceiptCustName.Text = string.Empty;
                txtReceiptAdd1.Text = string.Empty;
                txtReceiptAdd2.Text = string.Empty;
            }
        }

    }
}