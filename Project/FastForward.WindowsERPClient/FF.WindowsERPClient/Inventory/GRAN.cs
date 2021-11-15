using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects.InventoryNew;


using FF.BusinessObjects;

namespace FF.WindowsERPClient.Inventory
{
    /// <summary>
    /// written by sachith
    ///Create Date : 2012/01/16
    /// </summary>
    public partial class GRAN : Base
    {

        #region properties

        List<InventoryRequestItem> RequestItems;
        List<InventoryRequestSerials> RequestSerials;
        string otherSerial;
        int lineNo;
        bool isDINBaseGRAN;
        string requestNo;
        string subType = "";
        string _bkNo = "";
        bool itm_alw_auto_approvel;
        private Boolean _isGRAN_WO_Ser = false;
        private bool _GRANautoapp = false;

        #endregion

        const string BackDateName = "m_Trans_Inventory_GRN";

        public GRAN()
        {
            InitializeComponent();

            RequestItems = new List<InventoryRequestItem>();
            RequestSerials = new List<InventoryRequestSerials>();
            lineNo = 0;
            otherSerial = "";
            isDINBaseGRAN = false;
            requestNo = "";
            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
            if (_masterComp.MC_IS_GRAN_WO_SER == 1) _isGRAN_WO_Ser = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You want to Exit", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }

        private void GRAN_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                txtRequestDate.Text = _date.Date.ToString("dd/MM/yyyy");
                BindRequestTypesDDLData(ddlRequestType);
                BindRequestTypesDDLData1(DropDownListType);

                LoadRequestStatus(ddlRequestStatus);

                ddlRequestType.Select();
                gvGRANList.AutoGenerateColumns = false;
                gvItem.AutoGenerateColumns = false;
                //if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, string.Empty, dateTimePickerDate, lblBackDate, _date.Date.ToString("dd/MMM/yyyy")))
                //{
                //    dateTimePickerDate.Visible = true;
                //}

                string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "INV2"))
                {
                    pnlGRANApp.Enabled = true;
                }
                else
                    pnlGRANApp.Enabled = false;

              
                ddlCond.DropDownStyle = ComboBoxStyle.DropDownList;
                //TODO: need permission to location search
                //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPI"))
                //{
                //    pnlLoacationSearch.Visible = true;
                //    //pnlLocation.Visible = true;
                //}
                //else
                //{
                //    pnlLoacationSearch.Visible = false;
                //    //pnlLocation.Visible = false;
                //}

                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10011))
                {
                    pnlLoacationSearch.Visible = true;
                }
                else
                {
                    pnlLoacationSearch.Visible = false;
                }

                BindUserCompanyItemStatusDDLData(ddlItemStatus);
                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, dateTimePickerDate, lblBackDate, string.Empty, out _allowCurrentTrans);
                lblNoofDoc.Text = "0";
                lblTotQty.Text = "0.00";
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

        private void BindSubTypes(ComboBox cmbSubType)
        {
            try
            {
                if (ddlRequestType.SelectedValue != null)
                {
                    List<MasterSubType> _list = CHNLSVC.General.GetAllSubTypes(ddlRequestType.SelectedValue.ToString());
                    cmbSubType.DataSource = _list;
                    cmbSubType.DisplayMember = "MSTP_DESC";
                    cmbSubType.ValueMember = "MSTP_CD";
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

        private void ddlRequestType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                SetRequestCondition();
                //BindSubTypes(cmbSubType);
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

        private void SetRequestCondition()
        {
            if (ddlRequestType.SelectedValue == null)
            {
                chkToReport.Visible = true;
                chkToStores.Visible = true;
                chkToReport.Checked = false;
                chkToStores.Checked = false;
                txtTransferLocation.Enabled = false;
                txtTransferLocation.Text = "";
                buttonSearchTransferLoc.Enabled = false;
                ddlNewStatus.DataSource = null;
                DropDownListAppStatus.DataSource = null;
                //DropDownListDINNo.Visible = false;
                txtDINno.Visible = false;
                buttonSearchDIN.Visible = false;
                lblDIN.Visible = false;
                //DropDownListDINNo.DataSource = null;
            }
            else if (ddlRequestType.SelectedValue.ToString() == "GRAN")
            {
                chkToReport.Visible = true;
                chkToStores.Visible = true;
                chkToReport.Checked = false;
                chkToStores.Checked = true;
                txtTransferLocation.Enabled = true;
                buttonSearchTransferLoc.Enabled = true;
                BindGRANStatus(ddlNewStatus);
                BindGRANStatus(DropDownListAppStatus);
                //DropDownListDINNo.Visible = true;
                txtDINno.Visible = true;
                buttonSearchDIN.Visible = true;
                lblDIN.Visible = true;
                //BindDINNo(DropDownListDINNo);
            }
            else
            {
                chkToReport.Visible = true;
                chkToStores.Visible = true;
                chkToReport.Checked = true;
                chkToStores.Checked = false;
                txtTransferLocation.Enabled = false;
                buttonSearchTransferLoc.Enabled = false;
                BindDINStatus(ddlNewStatus);
                BindDINStatus(DropDownListAppStatus);
                //DropDownListDINNo.Visible = false;
                txtDINno.Visible = false;
                buttonSearchDIN.Visible = false;
                lblDIN.Visible = false;
                DropDownListDINNo.DataSource = null;
                txtTransferLocation.Text = "";
            }
        }

        private void CheckBoxManual_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (CheckBoxManual.Checked)
                {
                    string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                    if (ddlRequestType.SelectedValue == null)
                    {
                        CheckBoxManual.Checked = false;
                        MessageBox.Show("Please select Request Type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtManualRef.Text = "";
                        txtManualRef.Enabled = false;
                    }
                    else if (ddlRequestType.SelectedValue.ToString() == "GRAN")
                    {
                        txtManualRef.Text = CHNLSVC.Inventory.GetNextManualDocNo(BaseCls.GlbUserComCode, _masterLocation, "MDOC_GRAN").ToString();
                        txtManualRef.Enabled = true;
                    }
                    else if (ddlRequestType.SelectedValue.ToString() == "DIN")
                    {
                        txtManualRef.Text = CHNLSVC.Inventory.GetNextManualDocNo(BaseCls.GlbUserComCode, _masterLocation, "MDOC_DIN").ToString();
                        txtManualRef.Enabled = true;
                    }
                }
                else
                {
                    txtManualRef.Text = "";
                    txtManualRef.Enabled = false;
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

        #region clear methods

        protected void ItemAddClear()
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
            ddlItemStatus.DataSource = null;
            ddlItemStatus.Enabled = true;
            //ddlCond.Text = "";


            BindUserCompanyItemStatusDDLData(ddlItemStatus);
        }

        protected void ClearAll()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            if (tabControl1.SelectedTab == tabPage2)
            {
                txtRequestDate.Text = _date.Date.ToString("dd/MM/yyyy");
                txtTransferLocation.Text = "";
                txtManualRef.Text = "";
                txtFeildManager.Text = "";
                txtRemarks.Text = "";
                ddlNewStatus.SelectedIndex = -1;
                ddlRequestType.SelectedIndex = -1;
                ddlItemStatus.SelectedIndex = -1;
                //DropDownListDINNo.DataSource = null;

                txtTransferLocation.Enabled = false;
                buttonSearchTransferLoc.Enabled = false;

                ItemAddClear();
                gvItem.DataSource = null;

                lblAppby.Text = "";
                ddlAppNaration.Text = "";
                txtAppRemarks.Text = "";
                lblReqCurrStatus.Text = "";
                DropDownListAppStatus.SelectedIndex = -1;

                DropDownListAppStatus.SelectedIndex = -1;
                dateTimePickerFrom.Value = _date;
                dateTimePickerTo.Value = _date;
                ddlRequestStatus.SelectedIndex = -1;
                chkCreateUser.Checked = false;

                DisplayButtons();
                tabControl1.SelectedTab = tabPage2;
                btnApprove.Enabled = true;
                btnCancel.Enabled = true;
                btnPrint.Enabled = true;
                btnReject.Enabled = true;
                btnSave.Enabled = true;
                btnApproveCancel.Enabled = false;

                pnlGRANApp.Enabled = true;
                pnlGRANItem.Enabled = true;
                btnAddItem.Enabled = true;
                gvItem.Columns[0].Visible = true;

                chkToStores.Visible = true;
                chkToReport.Visible = true;
                chkToReport.Checked = false;
                chkToStores.Checked = false;

                txtDINno.Visible = false;
                buttonSearchDIN.Visible = false;
                lblDIN.Visible = false;
                //DropDownListDINNo.Visible = false;

                ddlRequestStatus.DataSource = null;
                DropDownListAppStatus.DataSource = null;

                //clear global variables
                RequestItems = new List<InventoryRequestItem>();
                RequestSerials = new List<InventoryRequestSerials>();
                ddlRequestType.Enabled = true;
                lineNo = 0;

                btnCancel.Enabled = false;
                btnApprove.Enabled = false;
                btnPrint.Enabled = false;
                btnReject.Enabled = false;
                btnAddItem.Enabled = true;
                CheckBoxManual.Checked = false;

                chkToReport.Checked = false;
                chkToStores.Checked = false;

                lblRequestNo.Text = "";
                lblRequestNo.Visible = false;
                lblRequest.Visible = false;
                txtDINno.Text = "";
                txtTransferLocation.Enabled = true;
                buttonSearchTransferLoc.Enabled = true;
                BindUserCompanyItemStatusDDLData(ddlItemStatus);
                ddlItemStatus.Enabled = true;
            }
            else
            {
                DropDownListType.SelectedIndex = 0;
                dateTimePickerFrom.Value = _date.Date;
                dateTimePickerTo.Value = _date.Date;
                ddlRequestStatus.SelectedIndex = 0;
                gvGRANList.DataSource = null;
                chkCreateUser.Checked = false;
                txtSeItem.Text = "";
                txtSeSerial.Text = "";
            }
            requestNo = "";
            LoadRequestStatus(ddlRequestStatus);
            DropDownListAppStatus.Enabled = true;
            chkEnable.Visible = false;
            ddlNewStatus.Enabled = true;
            ddlCond.Enabled = true;
            // cmbSubType.Enabled = true;
            subType = "";
        }

        #endregion

        #region search methods

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.GRNItem:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SalesInvoice:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.WHAREHOUSE:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerial:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + txtItemCode.Text.ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SerialNonSerial:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + txtItemCode.Text.ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DINRequestNo:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                default:
                    break;
            }
            return paramsText.ToString();
        }



        private void buttonSearchTransferLoc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WHAREHOUSE);
                DataTable _result = CHNLSVC.Inventory.GetLocationByType(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtTransferLocation;
                _CommonSearch.ShowDialog();
                txtTransferLocation.Select();
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

        private void buttonSearchItem_Click(object sender, EventArgs e)
        {
            try
            {
                ItemAddClear();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GRNItem);
                DataTable _result = CHNLSVC.CommonSearch.GetGRNItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemCode;

                _CommonSearch.ShowDialog();
                txtItemCode.Select();
                if (txtItemCode.Text != "")
                    LoadItemDetails();
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

        private void buttonSearchSerial_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SerialNonSerial);
                DataTable _result = CHNLSVC.CommonSearch.GetAvailableSerialNonSerialSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerialID;
                _CommonSearch.ShowDialog();
                txtSerNo.Select();
                if (txtSerialID.Text != "")
                    LoadSerialData();
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

        private void buttonSearchDoc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableNoneSerial);
                DataTable _result = CHNLSVC.CommonSearch.GetAvailableSerialNonSerialSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerialID;
                _CommonSearch.ShowDialog();
                txtSerialID.Select();
                if (txtSerialID.Text != "")
                    LoadDocNoData();
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DINRequestNo);
                DataTable _result = CHNLSVC.CommonSearch.GetDIN(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDINno;
                _CommonSearch.ShowDialog();
                LoadDIN();
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

        #endregion

        #region databind methods

        private void BindGRANStatus(ComboBox ddl)
        {
            DataTable dt = CHNLSVC.Inventory.GetAllCompanyStatus(BaseCls.GlbUserComCode);
            DataRow dr = dt.NewRow();
            dt.Rows.InsertAt(dr, 0);
            ddl.DataSource = dt;
            ddl.DisplayMember = "MIS_DESC";
            ddl.ValueMember = "MIC_CD";
        }

        private void BindDINStatus(ComboBox ddl)
        {
            DataTable dt = CHNLSVC.Inventory.GetModuleStatus("DIN");
            DataRow dr = dt.NewRow();
            dt.Rows.InsertAt(dr, 0);
            ddl.DataSource = dt;
            ddl.DisplayMember = "MIS_DESC";
            ddl.ValueMember = "MIS_CD";
        }

        private void BindDINNo(ComboBox ddl)
        {
            DataTable dt = CHNLSVC.Inventory.GetDINList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            DataRow dr = dt.NewRow();
            dt.Rows.InsertAt(dr, 0);
            //ComboBoxDraw(dt, ddl, "itr_req_no", "itr_dt");
            ddl.DataSource = dt;
            ddl.DisplayMember = "itr_req_no";
            ddl.ValueMember = "itr_req_no";
        }

        private void BindRequestTypesDDLData(ComboBox ddl)
        {
            List<MasterType> _list = new List<MasterType>();
            _list.Add(new MasterType());
            _list.AddRange(CHNLSVC.General.GetAllType(CommonUIDefiniton.MasterTypeCategory.GRAN.ToString()));
            ddl.DataSource = _list;
            ddl.DisplayMember = "Mtp_desc";
            ddl.ValueMember = "Mtp_cd";
        }
        private void BindRequestTypesDDLData1(ComboBox ddl)
        {
            List<MasterType> _list = new List<MasterType>();
            _list.AddRange(CHNLSVC.General.GetAllType(CommonUIDefiniton.MasterTypeCategory.GRAN.ToString()));
            MasterType _type = new MasterType();
            _type.Mtp_cd = "%";
            _type.Mtp_desc = "All";
            _list.Add(_type);
            ddl.DataSource = _list;
            ddl.DisplayMember = "Mtp_desc";
            ddl.ValueMember = "Mtp_cd";
        }
        protected void BindItemStatus(ComboBox ddl, string _company, string _location, string _item, string _serial, string serialId)
        {
            string bin = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            if (_serial == null)
                _serial = "N/A";
            //DataTable _tbl = CHNLSVC.Inventory.GetAvailableSerIDInformation(_company, _location, bin, _item, _serial,);
            ReptPickSerials rept = CHNLSVC.Inventory.GetAvailableSerIDInformation(_company, _location, _item, _serial, "N/A", serialId);
            ddl.SelectedValue = rept.Tus_itm_stus;
            //ddl.DataSource = _tbl;
            //ddl.DisplayMember = "ins_itm_stus";
            //ddl.ValueMember = "ins_itm_stus";
        }

        private void BindUserCompanyItemStatusDDLData(ComboBox ddl)
        {
            DataTable _tbl = CacheLayer.Get<DataTable>(CacheLayer.Key.CompanyItemStatus.ToString());
            var _s = (from L in _tbl.AsEnumerable()
                      select new
                      {
                          MIS_DESC = L.Field<string>("MIS_DESC"),
                          MIC_CD = L.Field<string>("MIC_CD")
                      }).ToList();
            var _n = new { MIS_DESC = string.Empty, MIC_CD = string.Empty };
            _s.Insert(0, _n);
            ddl.DataSource = _s;
            ddl.DisplayMember = "MIS_DESC";
            ddl.ValueMember = "MIC_CD";
        }

        #endregion

        #region key down events

        private void ddlRequestType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SetRequestCondition();
                    if (ddlRequestType.SelectedValue == null)
                        return;
                    else if (ddlRequestType.SelectedValue.ToString() == "GRAN")
                        txtTransferLocation.Focus();
                    else if (ddlRequestType.SelectedValue.ToString() == "DIN")
                        txtRemarks.Focus();
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

        private void txtTransferLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtManualRef.Enabled)
                    txtManualRef.Focus();
                else
                {
                    txtDINno.Focus();
                }
            }
            if (e.KeyCode == Keys.F2)
            {
                buttonSearchTransferLoc_Click(null, null);
            }
        }

        private void txtManualRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void CheckBoxManual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (ddlRequestType.SelectedValue == null)
                    return;
                else if (ddlRequestType.SelectedValue.ToString() == "GRAN")
                {
                    txtDINno.Focus();
                }
                else
                {
                    txtRemarks.Focus();
                }
        }

        private void txtFeildManager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ddlNewStatus.Focus();
        }

        private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtFeildManager.Focus();
        }

        private void ddlNewStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{Tab}");
        }

        private void chkToReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtItemCode.Focus();
        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtSerNo.Focus();
                    LoadItemDetails();
                }
                if (e.KeyCode == Keys.F2)
                {
                    buttonSearchItem_Click(null, null);
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

        private void txtSerNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtItemRemarks.Focus();
                    LoadSerialData();
                }
                if (e.KeyCode == Keys.F2)
                {
                    buttonSearchSerial_Click(null, null);
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

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtInDocNo.Focus();
            }
        }

        private void txtInDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtItemRemarks.Focus();
                    LoadDocNoData();
                }
                if (e.KeyCode == Keys.F2)
                {
                    buttonSearchDoc_Click(null, null);
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

        private void txtItemRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ddlCond.Focus();

        }

        private void DropDownListDINNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRemarks.Focus();
            }
        }

        private void chkToStores_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtItemCode.Focus();
        }

        private void txtDINno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtRemarks.Focus();
                    LoadDIN();
                }
                if (e.KeyCode == Keys.F2)
                    button2_Click(null, null);
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

        #endregion

        #region data load methods

        /// <summary>
        /// Load the item details
        /// </summary>
        private void LoadItemDetails()
        {
            txtQty.ReadOnly = true;
            ddlItemStatus.Enabled = false;
            if (txtItemCode.Text != "")
            {
                FF.BusinessObjects.MasterItem _item = (FF.BusinessObjects.MasterItem)CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemCode.Text.ToUpper());
                if (_item == null)
                {
                    MessageBox.Show("Invalid Item Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtItemCode.Text = "";
                    txtItemCode.Focus();
                    txtItemDescription.Text = "";
                    txtModelNo.Text = "";
                    txtBrand.Text = "";
                    return;
                }
                else
                {
                    //if (_item.Mi_is_ser1 == -1)
                    //{
                    //    MessageBox.Show("Item is a decimal Item.Cannot proceed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}
                    //else
                    //{
                        if (_isGRAN_WO_Ser == false)
                        {
                            if (_item.Mi_is_ser1 == -1 || _item.Mi_is_ser1 == 0)
                            {
                                DataTable _serialstatus = CHNLSVC.Inventory.GetAvailableItemStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, null, txtItemCode.Text.Trim(), "-1");
                                if (_serialstatus != null)
                                {
                                    if (_item.Mi_is_ser1 == -1)
                                    {
                                        if (_serialstatus.Rows.Count > 0)
                                        {
                                            // _serialstatus = _serialstatus.Rows[0].Field<string>("ins_itm_stus");
                                            ddlItemStatus.DataSource = _serialstatus;
                                            ddlItemStatus.ValueMember = "ins_itm_stus";
                                            ddlItemStatus.DisplayMember = "ins_itm_stus";
                                        }
                                        else
                                        {
                                            MessageBox.Show("Cannot proceed as no stocks available in " + BaseCls.GlbUserDefLoca + " location.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                    }

                                }

                            }
                            if (_item.Mi_is_ser1 == -1)
                            {
                                txtSerNo.Text = "N/A";
                                txtSerNo.Enabled = false;
                                txtSerialID.Text = "0";
                            }
                            txtItemDescription.Text = _item.Mi_shortdesc;
                            txtBrand.Text = _item.Mi_brand;
                            txtModelNo.Text = _item.Mi_model;
                            txtQty.ReadOnly = false;
                            ddlItemStatus.Enabled = true;
                            //else
                            // // {
                            //    MessageBox.Show("Item not allow to GRAN/DIN", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //   txtItemCode.Text = "";
                            //   txtItemCode.Focus();
                            //   txtItemDescription.Text = "";
                            //   txtModelNo.Text = "";
                            //  txtBrand.Text = "";
                            //   return;
                            //  }
                        }
                        else
                        {
                            if (_item.Mi_is_ser1 != -1)
                            {
                                txtQty.ReadOnly = true;
                                ddlItemStatus.Enabled = false;

                            }
                            else
                            {
                                txtQty.ReadOnly = false;
                                ddlItemStatus.Enabled = true;
                            }

                            txtItemDescription.Text = _item.Mi_shortdesc;
                            txtBrand.Text = _item.Mi_brand;
                            txtModelNo.Text = _item.Mi_model;
                        }
                    //}
                }
            }
        }

        /// <summary>
        /// Load details on in document number(for non serials)
        /// </summary>
        private void LoadDocNoData()
        {
            if (txtSerialID.Text != "")
            {
                ReptPickSerials _serial = CHNLSVC.Inventory.GetAvailableSerIDInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItemCode.Text.Trim(), "N/A", null, txtSerialID.Text);
                if (_serial != null)
                {
                    txtInDocNo.Text = _serial.Tus_doc_no;
                    txtSerialID.Text = _serial.Tus_ser_id.ToString();
                    txtSerNo.Text = _serial.Tus_ser_1;
                    otherSerial = _serial.Tus_ser_2;
                    txtQty.Text = "1";
                    BindItemStatus(ddlItemStatus, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItemCode.Text.Trim(), null, txtSerialID.Text);
                }
                else
                {
                    MessageBox.Show("Invalid Doc No", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtInDocNo.Text = "";
                    txtInDocNo.Focus();
                    txtSerialID.Text = "";
                    txtSerNo.Text = "";
                    txtQty.Text = "";
                    ddlItemStatus.DataSource = null;
                    return;
                }
            }
        }

        /// <summary>
        /// Load details on serial number
        /// </summary>
        private void LoadSerialData()
        {
            if (txtSerialID.Text != "")
            {
                ReptPickSerials _serial = CHNLSVC.Inventory.GetAvailableSerIDInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItemCode.Text.Trim(), "N/A", null, txtSerialID.Text);
                if (_serial != null)
                {
                    txtInDocNo.Text = _serial.Tus_doc_no;
                    txtSerialID.Text = _serial.Tus_ser_id.ToString();
                    txtSerNo.Text = _serial.Tus_ser_1;
                    otherSerial = _serial.Tus_ser_2;
                    txtQty.Text = "1";
                    BindItemStatus(ddlItemStatus, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItemCode.Text.Trim(), null, txtSerialID.Text);
                    ddlItemStatus.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Invalid Serial No", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtInDocNo.Text = "";
                    txtSerNo.Focus();
                    txtSerialID.Text = "";
                    txtSerNo.Text = "";
                    txtQty.Text = "";
                    ddlItemStatus.DataSource = null;
                    return;
                }
            }



            //if (txtSerNo.Text != "")
            //{
            //    ReptPickSerials _serial = CHNLSVC.Inventory.GetAvailableSerIDInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItemCode.Text.ToUpper(), txtSerNo.Text.ToUpper(), null, null);
            //    if (_serial.Tus_ser_id != 0)
            //    {
            //        txtInDocNo.Text = _serial.Tus_doc_no;
            //        txtSerialID.Text = _serial.Tus_ser_id.ToString();
            //        otherSerial = _serial.Tus_ser_2;
            //        txtQty.Text = "1";
            //        BindItemStatus(ddlItemStatus, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItemCode.Text, txtSerNo.Text);
            //    }
            //    else {
            //        MessageBox.Show("Invalid Serial No", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        txtSerNo.Text = "";
            //        txtInDocNo.Text = "";
            //        txtSerialID.Text = "";
            //        txtQty.Text="";
            //        ddlItemStatus.DataSource = null;
            //        txtSerNo.Focus();
            //        return;
            //    }
            //}
        }

        private void LoadRequestStatus(ComboBox ddl)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Text");
            dt.Columns.Add("Value");
            DataRow dr = dt.NewRow();
            dr[0] = "All";
            dr[1] = "%";
            DataRow dr1 = dt.NewRow();
            dr1[0] = "Pending";
            dr1[1] = "P";
            DataRow dr2 = dt.NewRow();
            dr2[0] = "Approved";
            dr2[1] = "A";
            DataRow dr3 = dt.NewRow();
            dr3[0] = "Cancel";
            dr3[1] = "C";
            DataRow dr4 = dt.NewRow();
            dr4[0] = "Rejected";
            dr4[1] = "R";
            DataRow dr5 = dt.NewRow();
            dr5[0] = "Finished";
            dr5[1] = "F";

            dt.Rows.Add(dr1);
            dt.Rows.Add(dr2);
            dt.Rows.Add(dr3);
            dt.Rows.Add(dr4);
            dt.Rows.Add(dr5);
            dt.Rows.Add(dr);

            ddl.DataSource = dt;
            ddl.DisplayMember = "Text";
            ddl.ValueMember = "Value";
        }
        bool _isSuperUSer = false;
        private void LoadRequestStatus(string reqNo)
        {
            DisplayButtons();
            EnableButtons();
            InventoryRequest _inputInvReq = new InventoryRequest();
            _inputInvReq.Itr_req_no = reqNo;
            InventoryRequest _selectedInventoryRequest = CHNLSVC.Inventory.GetInventoryRequestData(_inputInvReq);
            _isSuperUSer = false;
            ddlRequestType.SelectedValue = _selectedInventoryRequest.Itr_tp;
            SetRequestCondition();
            lblRequest.Visible = true;
            lblRequestNo.Visible = true;
            lblRequestNo.Text = _selectedInventoryRequest.Itr_req_no;
            txtRequestDate.Text = _selectedInventoryRequest.Itr_dt.ToShortDateString();
            txtTransferLocation.Text = _selectedInventoryRequest.Itr_rec_to;
            txtRemarks.Text = _selectedInventoryRequest.Itr_note;
            txtManualRef.Text = _selectedInventoryRequest.Itr_ref;
            txtFeildManager.Text = _selectedInventoryRequest.Itr_collector_name;
            if (_selectedInventoryRequest.Itr_stus == "A")
                lblReqCurrStatus.Text = "Approved";
            else if (_selectedInventoryRequest.Itr_stus == "C")
                lblReqCurrStatus.Text = "Cancel";
            else if (_selectedInventoryRequest.Itr_stus == "R")
                lblReqCurrStatus.Text = "Rejected";
            else if (_selectedInventoryRequest.Itr_stus == "F")
                lblReqCurrStatus.Text = "Finished";
            else
                lblReqCurrStatus.Text = "Pending";
            ddlNewStatus.SelectedValue = _selectedInventoryRequest.Itr_gran_nstus;
            DropDownListAppStatus.SelectedValue = _selectedInventoryRequest.Itr_gran_nstus;
            txtAppRemarks.Text = _selectedInventoryRequest.Itr_gran_app_note;
            lblAppby.Text = _selectedInventoryRequest.Itr_gran_app_by;

            ddlNewStatus.SelectedValue = _selectedInventoryRequest.Itr_gran_nstus;

            chkToReport.Checked = Convert.ToBoolean(_selectedInventoryRequest.Itr_gran_opt2);
            chkToStores.Checked = Convert.ToBoolean(_selectedInventoryRequest.Itr_gran_opt4);
            txtAppRemarks.Text = _selectedInventoryRequest.Itr_gran_app_note;

            DropDownListAppStatus.SelectedValue = _selectedInventoryRequest.Itr_gran_app_stus;

            if (_selectedInventoryRequest.InventoryRequestSerialsList != null)
            {
                gvItem.DataSource = _selectedInventoryRequest.InventoryRequestSerialsList;
            }
            ddlAppNaration.Text = _selectedInventoryRequest.Itr_gran_narrt;

            //pnlGRANItem.Enabled = false;
            btnAddItem.Enabled = false;
            gvItem.Columns[0].Visible = false;
            string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "INV2"))
            {
                _isSuperUSer = true;
                //pnlGRANApp.Enabled = true;
                //btnApprove.Enabled = true;
                //btnReject.Enabled = true;
                //pnlGRANApp.Enabled = true;
                //btnApproveCancel.Enabled = true;

                //Set relavant buttons according to the GRAN status.
                if (_selectedInventoryRequest.Itr_stus.ToUpper().Equals("P"))
                {
                    btnSave.Enabled = true;
                    btnAddItem.Enabled = true;
                    btnCancel.Enabled = true;
                    btnApproveCancel.Enabled = false;
                }
                if (_selectedInventoryRequest.Itr_stus.ToUpper().Equals("A") && _selectedInventoryRequest.Itr_tp == "GRAN")
                {
                    btnApprove.Enabled = false;
                    btnReject.Enabled = false;
                    btnCancel.Enabled = false;
                    btnApproveCancel.Enabled = true;
                }
                if (_selectedInventoryRequest.Itr_stus.ToUpper().Equals("A") && _selectedInventoryRequest.Itr_tp == "DIN")
                {
                    btnApprove.Enabled = false;
                    btnReject.Enabled = false;
                    btnCancel.Enabled = false;
                    btnApproveCancel.Enabled = false;
                }
                if (_selectedInventoryRequest.Itr_stus.ToUpper().Equals("R"))
                {
                    btnApprove.Enabled = false;
                    btnReject.Enabled = false;
                    btnCancel.Enabled = false;
                    btnApproveCancel.Enabled = false;
                }
                if (_selectedInventoryRequest.Itr_stus.ToUpper().Equals("C"))
                {
                    btnSave.Enabled = false;
                    btnAddItem.Enabled = false;
                    btnCancel.Enabled = false;
                    btnApprove.Enabled = false;
                    btnApproveCancel.Enabled = false;
                }
                if (_selectedInventoryRequest.Itr_stus.ToUpper().Equals("F"))
                {
                    btnSave.Enabled = false;
                    btnAddItem.Enabled = false;
                    btnCancel.Enabled = false;
                    btnApprove.Enabled = false;
                    btnApproveCancel.Enabled = false;
                    btnReject.Enabled = false;
                }
            }
            else
            {
                _isSuperUSer = false;
                pnlGRANApp.Enabled = false;
                btnApprove.Enabled = false;
                btnReject.Enabled = false;
                pnlGRANApp.Enabled = false;
                btnApproveCancel.Enabled = true;
            }

            ddlRequestType.Enabled = false;
            btnSave.Enabled = false;

            tabControl1.SelectedTab = tabPage2;
            DropDownListAppStatus.Enabled = false;
            ddlNewStatus.Enabled = false;
            //cmbSubType.Enabled = false;
        }

        private void LoadRequestStatusDIN(string reqNo)
        {
            EnableButtons();
            InventoryRequest _inputInvReq = new InventoryRequest();
            _inputInvReq.Itr_req_no = reqNo;
            InventoryRequest _selectedInventoryRequest = CHNLSVC.Inventory.GetInventoryRequestData(_inputInvReq);

            lblReqCurrStatus.Text = "Approved";
            ddlNewStatus.SelectedValue = _selectedInventoryRequest.Itr_gran_nstus;

            //check serial availbility
            foreach (InventoryRequestSerials _ser in _selectedInventoryRequest.InventoryRequestSerialsList)
            {
                ReptPickSerials _reptSer = CHNLSVC.Inventory.GetAvailableSerIDInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _ser.Itrs_itm_cd, _ser.Itrs_ser_1, otherSerial, _ser.Itrs_ser_id.ToString());

                //added 2014/03/07
                if (_reptSer == null || _reptSer.Tus_doc_no == null)
                {
                    MessageBox.Show("Inventory Balance not available\nFor Item - " + _ser.Itrs_itm_cd + " and Serial - " + _ser.Itrs_ser_id + "\n", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (_selectedInventoryRequest.InventoryRequestSerialsList != null)
            {
                gvItem.DataSource = _selectedInventoryRequest.InventoryRequestSerialsList;
            }

            // pnlGRANItem.Enabled = false;
            btnAddItem.Enabled = false;
            gvItem.Columns[0].Visible = false;

            btnReject.Enabled = false;
            btnCancel.Enabled = false;
            btnApprove.Enabled = false;
            btnSave.Enabled = true;
            isDINBaseGRAN = true;
        }

        private void LoadDIN()
        {
            if (txtDINno.Text != "")
            {
                if (!ValidateDINRequestNo(txtDINno.Text.ToUpper()))
                {
                    ItemAddClear();
                    pnlGRANItem.Enabled = true;
                    btnAddItem.Enabled = true;
                    gvItem.Columns[0].Visible = true;
                    gvItem.DataSource = null;
                    MessageBox.Show("Invalid DIN number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDINno.Text = "";
                    isDINBaseGRAN = false;
                }
                else
                {
                    LoadRequestStatusDIN(txtDINno.Text);
                }
            }
            else
            {
                ItemAddClear();
                pnlGRANItem.Enabled = true;
                btnAddItem.Enabled = true;
                gvItem.Columns[0].Visible = true;
                gvItem.DataSource = null;
                isDINBaseGRAN = false;
            }
        }

        #endregion

        #region main button click events

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlRequestType.SelectedValue.ToString() == "GRAN")
                {
                    if (string.IsNullOrEmpty(txtTransferLocation.Text))
                    {
                        MessageBox.Show("Please select the transfer location", "GRAN Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (!isValid(txtTransferLocation.Text.Trim()))
                    {
                        return;
                    }
                }

                //btnSave.Enabled = false;
                if (!isDINBaseGRAN)
                    SaveNormal();
                else
                    SaveDINBaseGRAN();
                //btnSave.Enabled = true;
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

        private void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                Update("A");
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

        private void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                Update("R");
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

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!_isSuperUSer && lblReqCurrStatus.Text != "Pending")
                {
                    MessageBox.Show("You are not allow to cancel " + lblReqCurrStatus.Text + " documents.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                Update("C");
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

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
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

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //validation
                if (ddlRequestType == null || ddlRequestType.SelectedValue.ToString() == "")
                    return;
                //check for reject and cancel status
                if (requestNo != "")
                {
                    InventoryRequest tem = new InventoryRequest();
                    tem.Itr_req_no = requestNo;
                    InventoryRequest invReq = CHNLSVC.Inventory.GetInventoryRequestData(tem);
                    if (invReq != null)
                    {
                        if (invReq.Itr_stus == "C" || invReq.Itr_stus == "R" || invReq.Itr_stus == "P")
                        {
                            MessageBox.Show("Cancel,Rejected and Pending Request can not print", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (invReq.Itr_anal2 != "0")
                        {
                            MessageBox.Show("Already Printed,Please make reprint request", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    if (invReq.Itr_ref != null && invReq.Itr_ref != "" && invReq.Itr_session_id != "MIG")
                    {
                        MessageBox.Show("Manual Request can not print", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (ddlRequestType.SelectedValue.ToString() == "GRAN")
                {
                    BaseCls.GlbReportTp = "GRAN";
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SGRANPrints.rpt" : "GRANPrints.rpt";
                    _view.GlbReportDoc = requestNo;
                    //_view.GlbUserComCode = BaseCls.GlbUserComCode;
                    _view.GlbReportProfit = BaseCls.GlbUserDefProf;
                    _view.Show();
                    _view = null;

                    InventoryRequest tem = new InventoryRequest();
                    tem.Itr_req_no = requestNo;
                    InventoryRequest invReq = CHNLSVC.Inventory.GetInventoryRequestData(tem);
                    string _docNo;
                    invReq.Itr_anal2 = "1";
                    int rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(invReq, null, out _docNo);

                }
                else if (ddlRequestType.SelectedValue.ToString() == "DIN")
                {
                    BaseCls.GlbReportTp = "DIN";
                    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SDINPrints.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_DINPrints.rpt" : "DINPrints.rpt";
                    _view.GlbReportDoc = requestNo;
                    //_view.GlbUserComCode = BaseCls.GlbUserComCode;
                    _view.GlbReportProfit = BaseCls.GlbUserDefProf;
                    _view.Show();
                    _view = null;

                    InventoryRequest tem = new InventoryRequest();
                    tem.Itr_req_no = requestNo;
                    InventoryRequest invReq = CHNLSVC.Inventory.GetInventoryRequestData(tem);
                    string _docNo;
                    invReq.Itr_anal2 = "1";
                    int rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(invReq, null, out _docNo);
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

        #endregion

        #region save/update methods

        private void Update(string status)
        {
            //if (ValidateSave())
            //{
            try
            {
                #region app validation
                txtRequestDate.Text = dateTimePickerDate.Value.ToString("dd/MM/yyyy");
                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, dateTimePickerDate, lblBackDate, Convert.ToDateTime(txtRequestDate.Text).ToString("dd/MMM/yyyy"), out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (Convert.ToDateTime(txtRequestDate.Text).Date != DateTime.Now.Date)
                        {
                            MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dateTimePickerDate.Focus();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dateTimePickerDate.Focus();
                        return;
                    }
                }
                txtRequestDate.Text = dateTimePickerDate.Value.ToString("dd/MM/yyyy");
                if (status == "A")
                {
                    if (DropDownListAppStatus.SelectedValue == null || DropDownListAppStatus.SelectedValue.ToString() == "")
                    {
                        MessageBox.Show("Please select approval status", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                #endregion
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                if (_date.Date != DateTime.Now.Date)
                {
                    MessageBox.Show("Your system date is not equal to server date! \nPlease contact system administrator....", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                InventoryRequest _inputInvReq = new InventoryRequest();
                _inputInvReq.Itr_req_no = requestNo;
                InventoryRequest _invreq = CHNLSVC.Inventory.GetInventoryRequestData(_inputInvReq);
                RequestItems = _invreq.InventoryRequestItemList;
                RequestSerials = _invreq.InventoryRequestSerialsList;




                _date = Convert.ToDateTime(txtRequestDate.Text);

                if (_invreq.Itr_stus == "A" && status == "A")
                {
                    MessageBox.Show("Can not approve previously approved requests", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                //if (pnlLocation.Visible && txtLoaction.Text != "")
                //{
                //    MasterLocation loc = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, txtLoaction.Text);
                //    if (loc != null)
                //        _masterLocation = loc.Ml_loc_cd;
                //}

                //Fill the InventoryRequest header & footer data.
                _invreq.Itr_com = BaseCls.GlbUserComCode;
                _invreq.Itr_req_no = GetRequestNo();
                _invreq.Itr_tp = ddlRequestType.SelectedValue.ToString();
                //CHANGE
                _invreq.Itr_stus = status;  //P - Pending , A - Approved. 
                _invreq.Itr_sub_tp = "TEMP";
                //_invreq.Itr_loc = _masterLocation;
                _invreq.Itr_ref = string.Empty;
                //TODO:
                //_invreq.Itr_dt = Convert.ToDateTime(txtRequestDate.Text);
                _invreq.Itr_exp_dt = Convert.ToDateTime(txtRequestDate.Text);
                _invreq.Itr_job_no = string.Empty;  //Invoice No.
                _invreq.Itr_bus_code = string.Empty;  //Customer Code.
                _invreq.Itr_note = txtRemarks.Text;
                //_invreq.Itr_issue_from = _masterLocation;
                _invreq.Itr_rec_to = txtTransferLocation.Text.ToUpper();
                _invreq.Itr_direct = 0;
                _invreq.Itr_country_cd = string.Empty;  //Counrty Code.
                _invreq.Itr_town_cd = string.Empty;     //Town Code.
                _invreq.Itr_cur_code = string.Empty;    //Currency Code.
                _invreq.Itr_exg_rate = 0;              //Exchange rate.
                _invreq.Itr_collector_id = string.Empty;
                _invreq.Itr_collector_name = string.Empty;
                _invreq.Itr_act = 1;
                _invreq.Itr_cre_by = BaseCls.GlbUserID;
                _invreq.InventoryRequestItemList = RequestItems;
                _invreq.Itr_session_id = BaseCls.GlbUserSessionID;
                _invreq.Itr_gran_app_by = BaseCls.GlbUserID;
                _invreq.Itr_gran_app_note = txtAppRemarks.Text;
                if (ddlNewStatus.SelectedValue != null)
                {
                    _invreq.Itr_gran_nstus = ddlNewStatus.SelectedValue.ToString();
                }
                _invreq.Itr_ref = txtManualRef.Text;
                _invreq.Itr_issue_com = BaseCls.GlbUserComCode;
                _invreq.Itr_collector_name = txtFeildManager.Text;
                _invreq.Itr_gran_app_note = txtAppRemarks.Text;
                _invreq.Itr_session_id = BaseCls.GlbUserSessionID;


                if (status == "A")
                {
                    _invreq.Itr_gran_app_stus = DropDownListAppStatus.SelectedValue.ToString();
                    //update serial stus
                    _invreq.InventoryRequestSerialsList.ForEach(x => x.Itrs_nitm_stus = DropDownListAppStatus.SelectedValue.ToString());
                    //print status
                    _invreq.Itr_anal2 = "0";
                    _invreq.Itr_gran_narrt = ddlAppNaration.Text;
                }
                else
                {
                    //print status
                    _invreq.Itr_anal2 = "-1";
                }
                if (chkToReport.Checked)
                    _invreq.Itr_gran_opt3 = 1;
                if (chkToStores.Checked)
                    _invreq.Itr_gran_opt4 = 1;

                int rowsAffected = 0;
                string _docNo = string.Empty;

                ReptPickSerials _reptTem = CHNLSVC.Inventory.Get_all_details_on_doc(BaseCls.GlbUserComCode, _masterLocation, RequestSerials[0].Itrs_itm_cd, RequestSerials[0].Itrs_in_docno, RequestSerials[0].Itrs_ser_id.ToString());

                //change item status
                //if apoval status and item status are not eqal
                if (status == "A" && DropDownListAppStatus.SelectedValue.ToString() != _reptTem.Tus_itm_stus && ddlRequestType.SelectedValue.ToString() == "DIN")
                {
                    InventoryHeader invHdr_min = new InventoryHeader();
                    invHdr_min.Ith_com = BaseCls.GlbUserComCode;
                    //  invHdr_min.Ith_sbu has been assigned later
                    // invHdr_min.Ith_channel has been assigned later
                    invHdr_min.Ith_loc = BaseCls.GlbUserDefLoca;
                    invHdr_min.Ith_cate_tp = "STUS";
                    invHdr_min.Ith_is_manual = false;
                    invHdr_min.Ith_stus = "A";
                    invHdr_min.Ith_cre_by = BaseCls.GlbUserID;
                    invHdr_min.Ith_mod_by = BaseCls.GlbUserID;
                    invHdr_min.Ith_mod_when = _date;
                    invHdr_min.Ith_direct = false;
                    invHdr_min.Ith_session_id = BaseCls.GlbUserSessionID;
                    invHdr_min.Ith_manual_ref = txtManualRef.Text;
                    invHdr_min.Ith_bk_no = _bkNo;   //kapila 25/4/2016
                    invHdr_min.Ith_remarks = txtRemarks.Text;


                    invHdr_min.Ith_doc_year = _date.Year;
                    invHdr_min.Ith_doc_date = _date.Date;
                    invHdr_min.Ith_doc_tp = "ADJ";
                    invHdr_min.Ith_sub_tp = "DIN";
                    invHdr_min.Ith_entry_tp = "DIN";
                    invHdr_min.Ith_entry_no = _invreq.Itr_req_no;
                    invHdr_min.Ith_session_id = BaseCls.GlbUserSessionID;
                    invHdr_min.Ith_acc_no = "DIN";

                    InventoryHeader invHdr_plus = new InventoryHeader();
                    DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
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
                    invHdr_plus.Ith_loc = BaseCls.GlbUserDefLoca;
                    invHdr_plus.Ith_com = BaseCls.GlbUserComCode;
                    invHdr_plus.Ith_cate_tp = "STUS";
                    invHdr_plus.Ith_is_manual = false;
                    invHdr_plus.Ith_stus = "A";
                    invHdr_plus.Ith_cre_by = BaseCls.GlbUserID;
                    invHdr_plus.Ith_mod_by = BaseCls.GlbUserID;
                    invHdr_plus.Ith_mod_when = _date;
                    invHdr_plus.Ith_direct = true;
                    invHdr_plus.Ith_session_id = BaseCls.GlbUserSessionID;
                    invHdr_plus.Ith_manual_ref = txtManualRef.Text;
                    invHdr_plus.Ith_bk_no = _bkNo;
                    invHdr_plus.Ith_remarks = txtRemarks.Text;

                    invHdr_plus.Ith_doc_year = _date.Year;
                    invHdr_plus.Ith_doc_date = _date.Date;
                    invHdr_plus.Ith_doc_tp = "ADJ";
                    invHdr_plus.Ith_sub_tp = "DIN";
                    invHdr_plus.Ith_entry_tp = "DIN";
                    invHdr_plus.Ith_entry_no = _invreq.Itr_req_no;
                    invHdr_plus.Ith_session_id = BaseCls.GlbUserSessionID;
                    invHdr_plus.Ith_acc_no = "DIN";


                    MasterAutoNumber masterAuto_min = new MasterAutoNumber();
                    masterAuto_min.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                    masterAuto_min.Aut_cate_tp = "LOC";
                    masterAuto_min.Aut_direction = null;
                    masterAuto_min.Aut_modify_dt = null;
                    masterAuto_min.Aut_moduleid = "ADJ";
                    masterAuto_min.Aut_number = 5;
                    masterAuto_min.Aut_start_char = "ADJ";
                    masterAuto_min.Aut_year = null;


                    MasterAutoNumber masterAuto_plus = new MasterAutoNumber();
                    masterAuto_plus.Aut_cate_cd = BaseCls.GlbUserDefLoca;
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
                    string _bin = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                    foreach (InventoryRequestSerials serial in _invreq.InventoryRequestSerialsList)
                    {
                        //chk avilability
                        ReptPickSerials _reptSer = new ReptPickSerials();
                        _reptSer = CHNLSVC.Inventory.GetAvailableSerIDInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, serial.Itrs_itm_cd, serial.Itrs_ser_1, otherSerial, serial.Itrs_ser_id.ToString());

                        //added 2013/09/28
                        if (_reptSer == null || _reptSer.Tus_doc_no == null)
                        {
                            MessageBox.Show("Cannot proceed as no stocks available in " + BaseCls.GlbUserDefLoca + " location.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        //kapila 8/8/2017
                        DataTable _dtFreeBal = null;
                        if (serial.Itrs_ser_1 != "N/A")
                        {
                            _dtFreeBal = CHNLSVC.Inventory.Get_serial_by_itm_stus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, serial.Itrs_itm_cd, serial.Itrs_itm_stus, serial.Itrs_ser_1);
                            if (_dtFreeBal.Rows.Count == 0)
                            {
                                MessageBox.Show("Cannot Approve.Requested Serial is not available", "GRAN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        //string bin = CHNLSVC.Inventory.GetDefaultBinCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                        ReptPickSerials _rept = CHNLSVC.Inventory.Get_all_details_on_doc(BaseCls.GlbUserComCode, _masterLocation, serial.Itrs_itm_cd, serial.Itrs_in_docno, serial.Itrs_ser_id.ToString());

                        //COMMENTED 2013/01/12
                        _rept.Tus_itm_line = serial.Itrs_in_itmline;
                        _rept.Tus_base_doc_no = _invreq.Itr_req_no;
                        _rept.Tus_base_itm_line = serial.Itrs_in_itmline;
                        _rept.Tus_doc_no = serial.Itrs_in_docno;
                        //_rept.Tus_seq_no = serial.Itrs_seq_no;
                        _rept.Tus_batch_line = serial.Itrs_in_batchline;
                        _rept.Tus_ser_line = serial.Itrs_in_serline;
                        _rept.Tus_new_status = DropDownListAppStatus.SelectedValue.ToString();
                        _rept.Tus_serial_id = serial.Itrs_ser_id.ToString();
                        // _rept.Tus_doc_dt = DateTime.Now.Date;
                        //END

                        InventoryHeader _inventoryHdr = CHNLSVC.Inventory.Get_Int_Hdr(_rept.Tus_doc_no);
                        //get difinition :: REMOVE AND THIS CODE IN SERVICE SIDE BY CHAMAL 08-JUL-2014
                        //List<InventoryCostRate> _costList = CHNLSVC.Inventory.GetInventoryCostRate(BaseCls.GlbUserComCode, "DIN", "DMG", (((dateTimePickerDate.Value.Year - _rept.Tus_doc_dt.Year) * 12) + dateTimePickerDate.Value.Month - _rept.Tus_doc_dt.Month), _rept.Tus_itm_stus);
                        //if (_costList != null && _costList.Count > 0)
                        //{
                        //    if (_costList[0].Rcr_rt == 0)
                        //    {
                        //        _rept.Tus_unit_cost = _rept.Tus_unit_cost - _costList[0].Rcr_val;
                        //        _rept.Tus_unit_cost = Math.Round(_rept.Tus_unit_cost, 4);
                        //    }
                        //    else
                        //    {
                        //        _rept.Tus_unit_cost = _rept.Tus_unit_cost - ((_rept.Tus_unit_cost * _costList[0].Rcr_rt) / 100);
                        //        _rept.Tus_unit_cost = Math.Round(_rept.Tus_unit_cost, 4);
                        //    }
                        //}


                        list_GetAllScanSerialsList.Add(_rept);
                    }


                    Int16 affected = CHNLSVC.Inventory.InventoryStatusChange(invHdr_min, invHdr_plus, list_GetAllScanSerialsList, null, masterAuto_min, masterAuto_plus, out _minusDocNo, out _plusDocNo);


                    if (affected > 0)
                    {
                        rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_invreq, null, out _docNo);
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Inventory Request Document Successfully saved. " + _docNo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Nothing saved", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    if (affected == -1)
                    {
                        MessageBox.Show("Error occurred while processing...\n" + _minusDocNo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }
                else
                {
                    //if rejected 
                    if (status == "R" || status == "C")
                    {
                        //set serial is availabal to 1
                        //WORK ONLY ONE ITEM
                        //  bool res = CHNLSVC.Inventory.UpdateSerialIDAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _invreq.InventoryRequestSerialsList[0].Itrs_itm_cd, _invreq.InventoryRequestSerialsList[0].Itrs_ser_id, 1,-1);
                        //if (!res)
                        //{
                        //    MessageBox.Show("Nothing saved. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    return;
                        //}
                    }

                    rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_invreq, null, out _docNo);

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Inventory Request Document Successfully Updated.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Nothing saved", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    ClearAll();
                }
                ClearAll();
            }

          //  }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while processing\n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }

            //else
            //{
            //    return;
            //}
        }

        private string GetRequestNo()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            string _reqNo = string.Empty;

            if (string.IsNullOrEmpty(requestNo))
                _reqNo = "DOC-" + _date.TimeOfDay.Hours.ToString() + ":" + _date.TimeOfDay.Minutes.ToString() + ":" + _date.TimeOfDay.Seconds.ToString();
            else
                _reqNo = requestNo;

            return _reqNo;
        }

        private void SaveDINBaseGRAN()
        {
            if (ValidateSave())
            {
                try
                {
                    DateTime _date = CHNLSVC.Security.GetServerDateTime();
                    if (_date.Date != DateTime.Now.Date)
                    {
                        MessageBox.Show("Your system date is not equal to server date! \nPlease contact system administrator....", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                    //if (pnlLocation.Visible && txtLoaction.Text != "") {
                    //    MasterLocation loc = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, txtLoaction.Text);
                    //    if (loc != null)
                    //        _masterLocation = loc.Ml_loc_cd;
                    //}
                    InventoryRequest _inventoryRequest = new InventoryRequest();

                    //Fill the InventoryRequest header & footer data.
                    _inventoryRequest.Itr_com = BaseCls.GlbUserComCode;
                    _inventoryRequest.Itr_req_no = "DOC-" + _date.TimeOfDay.Hours.ToString() + ":" + _date.TimeOfDay.Minutes.ToString() + ":" + _date.TimeOfDay.Seconds.ToString();
                    _inventoryRequest.Itr_tp = ddlRequestType.SelectedValue.ToString(); ;
                    //CHANGE
                    _inventoryRequest.Itr_sub_tp = "TEMP";
                    _inventoryRequest.Itr_loc = _masterLocation;
                    _inventoryRequest.Itr_ref = string.Empty;
                    _inventoryRequest.Itr_dt = Convert.ToDateTime(txtRequestDate.Text);
                    _inventoryRequest.Itr_exp_dt = Convert.ToDateTime(txtRequestDate.Text);
                    _inventoryRequest.Itr_stus = "A";
                    _inventoryRequest.Itr_anal1 = txtDINno.Text.ToUpper();

                    _inventoryRequest.Itr_job_no = string.Empty;  //Invoice No.
                    _inventoryRequest.Itr_bus_code = string.Empty;  //Customer Code.
                    _inventoryRequest.Itr_note = txtRemarks.Text;
                    _inventoryRequest.Itr_issue_from = _masterLocation;
                    _inventoryRequest.Itr_rec_to = txtTransferLocation.Text.ToUpper();
                    _inventoryRequest.Itr_direct = 0;
                    _inventoryRequest.Itr_country_cd = string.Empty;  //Counrty Code.
                    _inventoryRequest.Itr_town_cd = string.Empty;     //Town Code.
                    _inventoryRequest.Itr_cur_code = string.Empty;    //Currency Code.
                    _inventoryRequest.Itr_exg_rate = 0;              //Exchange rate.
                    _inventoryRequest.Itr_collector_id = string.Empty;
                    _inventoryRequest.Itr_collector_name = string.Empty;
                    _inventoryRequest.Itr_issue_com = BaseCls.GlbUserComCode;
                    _inventoryRequest.Itr_collector_name = txtFeildManager.Text;
                    _inventoryRequest.Itr_act = 1;
                    _inventoryRequest.Itr_cre_by = BaseCls.GlbUserID;
                    _inventoryRequest.Itr_ref = txtManualRef.Text;
                    _inventoryRequest.Itr_gran_nstus = ddlNewStatus.SelectedValue.ToString();
                    _inventoryRequest.Itr_gran_app_note = txtAppRemarks.Text;
                    //print status
                    _inventoryRequest.Itr_anal2 = "0";

                    //TODO:need to get approvel status
                    _inventoryRequest.Itr_gran_app_stus = "-1";

                    if (chkToReport.Checked)
                        _inventoryRequest.Itr_gran_opt2 = 1;
                    if (chkToStores.Checked)
                        _inventoryRequest.Itr_gran_opt4 = 1;
                    if (chkSellAtShop.Checked)
                        _inventoryRequest.Itr_gran_opt1 = 1;
                    if (chkNeedDiscount.Checked)
                        _inventoryRequest.Itr_gran_opt3 = 1;

                    _inventoryRequest.Itr_session_id = BaseCls.GlbUserSessionID;


                    InventoryRequest _inputInvReq = new InventoryRequest();
                    _inputInvReq.Itr_req_no = txtDINno.Text.ToUpper();

                    InventoryRequest _invreq = CHNLSVC.Inventory.GetInventoryRequestData(_inputInvReq);
                    if (_invreq != null)
                    {
                        RequestItems = _invreq.InventoryRequestItemList;
                        RequestSerials = _invreq.InventoryRequestSerialsList; //CHNLSVC.Inventory.GetAllGRANSerialsList(_invreq.Itr_com, _invreq.Itr_loc, _invreq.Itr_tp, _invreq.Itr_req_no);

                        string _newDMGStatus = string.Empty;
                        foreach (InventoryRequestSerials ser in RequestSerials)
                        {
                            ReptPickSerials _reptSer = new ReptPickSerials();
                            _reptSer = CHNLSVC.Inventory.GetAvailableSerIDInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, ser.Itrs_itm_cd, ser.Itrs_ser_1, otherSerial, ser.Itrs_ser_id.ToString());

                            bool _fail = true;
                            //Edit by Chamal 29-Aug-2014
                            if (_reptSer.Tus_itm_stus == "DMG")
                            { _fail = false; _newDMGStatus = "DMG"; }
                            if (_reptSer.Tus_itm_stus == "DMGLP")
                            { _fail = false; _newDMGStatus = "DMGLP"; }
                            if (_reptSer.Tus_itm_stus == "DGDNT")
                            { _fail = false; _newDMGStatus = "DGDNT"; } //Sanjeewa 2016-03-31 for SGL
                            if (_reptSer.Tus_itm_stus == "DGPKG")
                            { _fail = false; _newDMGStatus = "DGPKG"; } //kapila 21/12/2016

                            if (_reptSer.Tus_itm_stus == "ITMDMG") //akila 2017/08/23
                            { _fail = false; _newDMGStatus = "ITMDMG"; }

                            if (_reptSer.Tus_itm_stus == "PKDMG")
                            { _fail = false; _newDMGStatus = "PKDMG"; }

                            if (_reptSer.Tus_itm_stus == "DEF") // add by tharanga 2017/10/02
                            { _fail = false; _newDMGStatus = "DEF"; }

                            //added 2013/09/28
                            //if (_reptSer.Tus_itm_stus != "DMG")
                            if (_fail == true)
                            {
                                MessageBox.Show("Error occurred while Processing\n\nDIN NO - " + txtDINno.Text.ToUpper() + "\nITEM CODE - " + ser.Itrs_itm_cd + "\nSERIAL ID - " + ser.Itrs_ser_id.ToString() + "\nSTATUS - " + _reptSer.Tus_itm_stus + "\nSTATUS NOT CHANGED");
                                return;
                            }
                            ser.Itrs_itm_stus = _newDMGStatus;
                            ser.Itrs_in_batchline = Convert.ToInt16(_reptSer.Tus_batch_line);
                            ser.Itrs_in_docdt = Convert.ToDateTime(_reptSer.Tus_doc_dt).Date;
                            ser.Itrs_in_docno = _reptSer.Tus_doc_no;
                            ser.Itrs_in_itmline = Convert.ToInt16(_reptSer.Tus_itm_line);
                            ser.Itrs_in_seqno = Convert.ToInt32(_reptSer.Tus_seq_no);
                            ser.Itrs_in_serline = Convert.ToInt16(_reptSer.Tus_ser_line);
                        }
                        foreach (InventoryRequestItem itm in RequestItems)
                        {
                            itm.Itri_itm_stus = _newDMGStatus;
                        }

                        _inventoryRequest.InventoryRequestItemList = RequestItems;
                        _inventoryRequest.InventoryRequestSerialsList = RequestSerials;

                    }

                    int rowsAffected = 0;
                    string _docNo = string.Empty;
                    // bool res = CHNLSVC.Inventory.UpdateSerialIDAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _inventoryRequest.InventoryRequestSerialsList[0].Itrs_itm_cd, _inventoryRequest.InventoryRequestSerialsList[0].Itrs_ser_id, -1,1);
                    // if (res)
                    // {
                    rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_inventoryRequest, GenerateMasterAutoNumber(), out _docNo);
                    // }
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Inventory Request Document Successfully saved. " + _docNo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //string Msg = "<script>alert('Inventory Request Document Sucessfully saved! Document No. : " + _docNo + "');window.location = 'GRAN_Note.aspx';</script>";
                        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                        _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                        if (ddlRequestType.SelectedValue.ToString() == "GRAN" && txtManualRef.Text != "")
                        {
                            CHNLSVC.Inventory.UpdateManualDocNo(BaseCls.GlbUserComCode, _masterLocation, "MDOC_GRAN", Convert.ToInt32(txtManualRef.Text), _docNo);

                        }
                        if (ddlRequestType.SelectedValue.ToString() == "DIN" && txtManualRef.Text != "")
                        {
                            CHNLSVC.Inventory.UpdateManualDocNo(BaseCls.GlbUserComCode, _masterLocation, "MDOC_DIN", Convert.ToInt32(txtManualRef.Text), _docNo);

                        }
                        //if (ddlRequestType.SelectedValue.ToString() == "GRAN" && CheckBoxManual.Checked==false)
                        //{
                        //    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        //    _view.GlbReportName = "GRANPrints.rpt";
                        //    _view.GlbReportDoc = _docNo;
                        //    // _view.GlbUserComCode = BaseCls.GlbUserComCode;
                        //    _view.GlbReportProfit = BaseCls.GlbUserDefProf;
                        //    _view.Show();
                        //    _view = null;
                        //}
                        ClearAll();
                    }
                    else
                    {
                        MessageBox.Show("Serial Availability update fail" + _docNo, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CHNLSVC.CloseChannel();
                    return;
                }
            }
            else
            {
                return;
            }
        }

        private void SaveNormal()
        {
            if (ValidateSave())
            {
                try
                {
                    DateTime _date = CHNLSVC.Security.GetServerDateTime();
                    if (_date.Date != DateTime.Now.Date)
                    {
                        MessageBox.Show("Your system date is not equal to server date! \nPlease contact system administrator....", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                    //if (pnlLocation.Visible && txtLoaction.Text != "")
                    //{
                    //    MasterLocation loc = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, txtLoaction.Text);
                    //    if (loc != null)
                    //        _masterLocation = loc.Ml_loc_cd;
                    //}
                    InventoryRequest _inventoryRequest = new InventoryRequest();

                    //Fill the InventoryRequest header & footer data.
                    _inventoryRequest.Itr_com = BaseCls.GlbUserComCode;
                    _inventoryRequest.Itr_req_no = "DOC-" + _date.TimeOfDay.Hours.ToString() + ":" + _date.TimeOfDay.Minutes.ToString() + ":" + _date.TimeOfDay.Seconds.ToString();
                    _inventoryRequest.Itr_tp = ddlRequestType.SelectedValue.ToString();
                    _inventoryRequest.Itr_sub_tp = "TEMP";
                    if (subType != "")
                        _inventoryRequest.Itr_sub_tp = subType;

                    _inventoryRequest.Itr_loc = _masterLocation;
                    _inventoryRequest.Itr_ref = string.Empty;
                    _inventoryRequest.Itr_dt = Convert.ToDateTime(txtRequestDate.Text);
                    _inventoryRequest.Itr_exp_dt = Convert.ToDateTime(txtRequestDate.Text);
                    _inventoryRequest.Itr_stus = "P";
                    _inventoryRequest.Itr_job_no = string.Empty;  //Invoice No.
                    _inventoryRequest.Itr_bus_code = string.Empty;  //Customer Code.
                    _inventoryRequest.Itr_note = txtRemarks.Text;
                    _inventoryRequest.Itr_issue_from = _masterLocation;
                    _inventoryRequest.Itr_rec_to = txtTransferLocation.Text.ToUpper();
                    _inventoryRequest.Itr_direct = 0;
                    _inventoryRequest.Itr_country_cd = string.Empty;  //Counrty Code.
                    _inventoryRequest.Itr_town_cd = string.Empty;     //Town Code.
                    _inventoryRequest.Itr_cur_code = string.Empty;    //Currency Code.
                    _inventoryRequest.Itr_exg_rate = 0;              //Exchange rate.
                    _inventoryRequest.Itr_collector_id = string.Empty;
                    _inventoryRequest.Itr_collector_name = string.Empty;
                    _inventoryRequest.Itr_issue_com = BaseCls.GlbUserComCode;
                    _inventoryRequest.Itr_collector_name = txtFeildManager.Text;
                    _inventoryRequest.Itr_act = 1;
                    _inventoryRequest.Itr_cre_by = BaseCls.GlbUserID;
                    _inventoryRequest.Itr_ref = txtManualRef.Text;
                    _inventoryRequest.Itr_gran_nstus = ddlNewStatus.SelectedValue.ToString();
                    _inventoryRequest.Itr_gran_app_note = txtAppRemarks.Text;
                    _inventoryRequest.Itr_gran_app_stus = DropDownListAppStatus.SelectedValue.ToString();

                    //print status
                    _inventoryRequest.Itr_anal2 = "-1";
                    if (ddlNewStatus.SelectedValue.ToString() == "BB")
                    {
                        _inventoryRequest.Itr_stus = "A";
                        _inventoryRequest.Itr_anal2 = "0";
                        _inventoryRequest.Itr_gran_narrt = ddlAppNaration.Text;
                        _inventoryRequest.Itr_gran_app_stus = "BB";
                    }
                    #region ADD GRAN AUTO approvel :: By Tharanga 2017/11/10
                    HpSystemParameters _SystemPara = new HpSystemParameters();
                    _SystemPara = CHNLSVC.Sales.GetSystemParameter("CHNL", BaseCls.GlbDefChannel, "GRANAUTO",DateTime.Now.Date);
                   GRAN_ALWSTUS _GRAN_ALWSTUS = new GRAN_ALWSTUS();
                  
                   DataTable newdt = new DataTable();
                    //add temp condition
                   if (RequestSerials.Count > 0)
                   {

                       var autoappcon = RequestSerials.Where(r => r.itm_alw_auto_approvel == false).FirstOrDefault();
                       if (autoappcon != null)
                       {
                           goto Manual_approvel_path;
                           _GRANautoapp = false;
                       }

                       _inventoryRequest.Itr_stus = "A";
                       _inventoryRequest.Itr_gran_app_by = "GRAN_ATAP";
                       _inventoryRequest.Itr_anal2 = "0";
                       _inventoryRequest.Itr_gran_narrt = ddlAppNaration.Text;
                       _GRANautoapp = true;



                   }
                   var grouped = RequestSerials.GroupBy(m => m.Itri_itm_cat);
                    if (!string.IsNullOrEmpty( _SystemPara.Hsy_cd))
                    {
                        _SystemPara = CHNLSVC.Sales.GetSystemParameter("LOC", BaseCls.GlbUserDefLoca, "GRANAUTOBK", DateTime.Now.Date);
                        if (string.IsNullOrEmpty(_SystemPara.Hsy_cd))
                        {

                            var autoappcon = RequestSerials.Where(r => r.itm_alw_auto_approvel == false).FirstOrDefault();
                            if (autoappcon !=null)
                            {
                                 goto Manual_approvel_path;
                                 _GRANautoapp = false;
                            }
                            
                            _inventoryRequest.Itr_stus = "A";
                            _inventoryRequest.Itr_gran_app_by = "GRAN_ATAP";
                            _inventoryRequest.Itr_anal2 = "0";
                            _inventoryRequest.Itr_gran_narrt = ddlAppNaration.Text;
                            _GRANautoapp = true;
                        }
                    }
                    #endregion
                Manual_approvel_path:
                    //request condition
                    if (chkToReport.Checked)
                        _inventoryRequest.Itr_gran_opt2 = 1;
                    if (chkToStores.Checked)
                        _inventoryRequest.Itr_gran_opt4 = 1;
                    if (chkSellAtShop.Checked)
                        _inventoryRequest.Itr_gran_opt1 = 1;
                    if (chkNeedDiscount.Checked)
                        _inventoryRequest.Itr_gran_opt3 = 1;

                    _inventoryRequest.Itr_session_id = BaseCls.GlbUserSessionID;
                    _inventoryRequest.InventoryRequestItemList = RequestItems;
                    _inventoryRequest.Itr_session_id = BaseCls.GlbUserSessionID;
                    
                    _inventoryRequest.InventoryRequestSerialsList = RequestSerials;

                    if (_inventoryRequest.InventoryRequestSerialsList == null)
                    {
                        MessageBox.Show("Item list empty! Please try again.", "List Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (_inventoryRequest.InventoryRequestSerialsList.Count <= 0)
                    {
                        MessageBox.Show("Item list empty! Please try again.", "List Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //request item status
                    //foreach()


                    int rowsAffected = 0;
                    string _docNo = string.Empty;
                    //set serial is availabal to -1
                    //WORK ONLY ONE ITEM
                    //bool res = CHNLSVC.Inventory.UpdateSerialIDAvailable(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _inventoryRequest.InventoryRequestSerialsList[0].Itrs_itm_cd, _inventoryRequest.InventoryRequestSerialsList[0].Itrs_ser_id, -1,1);
                    //if (res)
                    //{
                    rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_inventoryRequest, GenerateMasterAutoNumber(), out _docNo);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Nothing saved. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}
                    if (rowsAffected > 0)
                    {
                        if (_GRANautoapp == true)
                        {
                            MessageBox.Show("Inventory Request Document Successfully saved. " + _docNo + " \nSuccessfully approved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Inventory Request Document Successfully saved. " + _docNo, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                            if (ddlRequestType.SelectedValue.ToString() == "GRAN" && txtManualRef.Text != "")
                        {
                            CHNLSVC.Inventory.UpdateManualDocNo(BaseCls.GlbUserComCode, _masterLocation, "MDOC_GRAN", Convert.ToInt32(txtManualRef.Text), _docNo);

                        }
                        if (ddlRequestType.SelectedValue.ToString() == "DIN" && txtManualRef.Text != "")
                        {
                            CHNLSVC.Inventory.UpdateManualDocNo(BaseCls.GlbUserComCode, _masterLocation, "MDOC_DIN", Convert.ToInt32(txtManualRef.Text), _docNo);

                        }
                        //if (ddlRequestType.SelectedValue.ToString() == "GRAN" && CheckBoxManual.Checked==false)
                        //{
                        //    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        //    _view.GlbReportName = "GRANPrints.rpt";
                        //    _view.GlbReportDoc = _docNo;
                        //    //_view.GlbUserComCode = BaseCls.GlbUserComCode;
                        //    _view.GlbReportProfit = BaseCls.GlbUserDefProf;
                        //    _view.Show();
                        //    _view = null;
                        //}
                        //else if (CheckBoxManual.Checked == false && ddlRequestType.SelectedValue.ToString() == "DIN")
                        //{
                        //    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        //    _view.GlbReportName = "DINPrints.rpt";
                        //    _view.GlbReportDoc = _docNo;
                        //    //_view.GlbUserComCode = BaseCls.GlbUserComCode;
                        //    _view.GlbReportProfit = BaseCls.GlbUserDefProf;
                        //    _view.Show();
                        //    _view = null;
                        //}
                        ClearAll();
                    }
                    else
                    {
                        MessageBox.Show(_docNo, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error occurred while processing\n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CHNLSVC.CloseChannel();
                    return;
                }
            }
            else
            {
                return;
            }
        }

        #endregion

        #region item/serial object fill

        private InventoryRequestSerials FillRequestSerials(int line)
        {
            InventoryRequestSerials _tempSer = new InventoryRequestSerials();

            ReptPickSerials _reptSer = new ReptPickSerials();
            if (_isGRAN_WO_Ser == false)
            {
                _reptSer = CHNLSVC.Inventory.GetAvailableSerIDInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItemCode.Text.ToString(), txtSerNo.Text.ToString(), otherSerial, txtSerialID.Text.ToString());
                InventoryHeader _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(_reptSer.Tus_doc_no);
                if (_invHdr.Ith_doc_tp == "SRN" || (_invHdr.Ith_doc_tp == "ADJ" && _invHdr.Ith_sub_tp == "RV"))
                {
                    subType = "CUSTM";
                }
                else
                {
                    subType = "STOCK";
                }
                _tempSer.Itrs_in_batchline = Convert.ToInt16(_reptSer.Tus_batch_line);
                _tempSer.Itrs_in_docdt = Convert.ToDateTime(_reptSer.Tus_doc_dt).Date;
                _tempSer.Itrs_in_docno = _reptSer.Tus_doc_no;
                _tempSer.Itrs_in_itmline = Convert.ToInt16(_reptSer.Tus_itm_line);
                _tempSer.Itrs_in_seqno = Convert.ToInt32(_reptSer.Tus_seq_no);
                _tempSer.Itrs_in_serline = Convert.ToInt16(_reptSer.Tus_ser_line);
                _tempSer.Itrs_itm_cd = txtItemCode.Text.Trim();
                if (ddlRequestType.SelectedValue.ToString() == "DIN")
                {
                    if (_reptSer.Tus_itm_stus == "DMG")
                    {
                        return null;
                    }
                    if (_reptSer.Tus_itm_stus == "DMGLP")
                    {
                        return null;
                    }
                }

                _tempSer.Itrs_itm_stus = _reptSer.Tus_itm_stus;
                _tempSer.Itrs_line_no = line;
                int serLin;
                try
                {
                    //int _line = RequestSerials.Where(r => r.Itrs_itm_cd == _tempSer.Itrs_itm_cd).FirstOrDefault().Itrs_ser_line;
                    //serLin = _line == 0 ? 1 : _line++;

                    serLin = Convert.ToInt32(RequestSerials.Max(x => x.Itrs_ser_line).ToString());
                }
                catch (Exception) { serLin = 0; }
                _tempSer.Itrs_ser_line = ++serLin;
                _tempSer.Itrs_nitm_stus = ddlNewStatus.SelectedValue.ToString();
                //REQUESTED BY ASANKA 2013/04/04
                if (ddlRequestType.SelectedValue.ToString() == "DIN")
                {
                    _tempSer.Itrs_nitm_stus = "DMG";
                }
                _tempSer.Itrs_qty = 1;
                _tempSer.Itrs_rmk = txtItemRemarks.Text.ToString();
                _tempSer.Itrs_seq_no = 0;
                _tempSer.Itrs_ser_1 = txtSerNo.Text.Trim();
                _tempSer.Itrs_ser_2 = _reptSer.Tus_ser_2;
                _tempSer.Itrs_ser_3 = "";
                _tempSer.Itrs_ser_4 = "";
                _tempSer.Itrs_ser_id = _reptSer.Tus_ser_id;
                // _tempSer.Itrs_ser_line = line;
                // _tempSer.Itrs_trns_tp = ddlTransferType.SelectedValue.ToString();
                _tempSer.Mi_longdesc = txtItemDescription.Text.ToString();
                _tempSer.Mi_model = txtModelNo.Text.ToString();
                _tempSer.Mi_brand = txtBrand.Text.ToString();
            }
            else
            {
                _tempSer.Itrs_in_batchline = 1;
                _tempSer.Itrs_in_docdt = DateTime.Now.Date; // Convert.ToDateTime(_reptSer.Tus_doc_dt).Date;
                _tempSer.Itrs_in_docno = "";  // _reptSer.Tus_doc_no;
                _tempSer.Itrs_in_itmline = 1;
                _tempSer.Itrs_in_seqno = 1;
                _tempSer.Itrs_in_serline = 1;
                _tempSer.Itrs_itm_cd = txtItemCode.Text.Trim();
                if (ddlRequestType.SelectedValue.ToString() == "DIN")
                {
                    if (ddlItemStatus.SelectedValue.ToString() == "DMG")
                    {
                        return null;
                    }
                    if (ddlItemStatus.SelectedValue.ToString() == "DMGLP")
                    {
                        return null;
                    }
                }

                _tempSer.Itrs_itm_stus = ddlItemStatus.SelectedValue.ToString();
                _tempSer.Itrs_line_no = line;
                int serLin;
                try
                {
                    serLin = Convert.ToInt32(RequestSerials.Max(x => x.Itrs_ser_line).ToString());
                }
                catch (Exception) { serLin = 0; }
                _tempSer.Itrs_ser_line = ++serLin;
                _tempSer.Itrs_nitm_stus = ddlNewStatus.SelectedValue.ToString();
                //REQUESTED BY ASANKA 2013/04/04
                if (ddlRequestType.SelectedValue.ToString() == "DIN")
                {
                    _tempSer.Itrs_nitm_stus = "DMG";
                }
                _tempSer.Itrs_qty = Convert.ToInt32(txtQty.Text);
                _tempSer.Itrs_rmk = txtItemRemarks.Text.ToString();
                _tempSer.Itrs_seq_no = 0;
                _tempSer.Itrs_ser_1 = txtSerNo.Text.Trim();
                _tempSer.Itrs_ser_2 = "";
                _tempSer.Itrs_ser_3 = "";
                _tempSer.Itrs_ser_4 = "";
                _tempSer.Itrs_ser_id = 0;
                // _tempSer.Itrs_ser_line = line;
                // _tempSer.Itrs_trns_tp = ddlTransferType.SelectedValue.ToString();
                _tempSer.Mi_longdesc = txtItemDescription.Text.ToString();
                _tempSer.Mi_model = txtModelNo.Text.ToString();
                _tempSer.Mi_brand = txtBrand.Text.ToString();
            }
            return _tempSer;
        }

        private InventoryRequestItem FillRequestItm(int line)
        {
            InventoryRequestItem _temitm = new InventoryRequestItem();

            ReptPickSerials _repitm = new ReptPickSerials();
            _repitm = CHNLSVC.Inventory.GetAvailableSerIDInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItemCode.Text.ToString(), txtSerNo.Text.ToString(), otherSerial, txtSerialID.Text.ToString());
            MasterItem item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemCode.Text.Trim());
            _temitm.Itri_itm_cd = txtItemCode.Text.Trim();
            _temitm.MasterItem = new MasterItem();
            _temitm.MasterItem.Mi_cd = item.Mi_cd;
            _temitm.MasterItem.Mi_itm_stus = item.Mi_itm_stus;
            _temitm.Itri_mitm_cd = item.Mi_cd;
            _temitm.Itri_mitm_stus = item.Mi_itm_stus;
            if (_isGRAN_WO_Ser == true)   //kapila 22/6/2016
                _temitm.Itri_app_qty = Convert.ToInt32(txtQty.Text);
            else
                _temitm.Itri_app_qty = Convert.ToInt32(txtQty.Text.ToString()); 
            //if(ddlItemStatus.SelectedValue!=null)
            //_temitm.Itri_itm_stus = _repitm.Tus_itm_stus;
            if (item.Mi_is_ser1 == -1)
            {
                _temitm.Itri_itm_stus = ddlItemStatus.SelectedValue.ToString();
            }
            else
            {
                _temitm.Itri_itm_stus = _repitm.Tus_itm_stus;
            }
            _temitm.Itri_line_no = line;
            _temitm.Itri_note = txtItemRemarks.Text;
            _temitm.Itri_bqty = Convert.ToInt32(txtQty.Text.ToString());
            _temitm.ITRI_ITM_COND = ddlCond.Text;   //kapila 19/5/2014
            //_temitm.Itri_mitm_cd
            _temitm.Itri_qty = Convert.ToInt32(txtQty.Text);
            _temitm.Mi_brand = txtBrand.Text.ToString();
            
            return _temitm;
        }

        #endregion

        #region check methods

        protected bool CheckTranferLocation(string locCode)
        {
            MasterLocation loc = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, txtTransferLocation.Text.ToUpper());
            if (loc != null)
                return true;
            else
                return false;
        }

        #endregion

        #region validation

        private bool ValidateSave()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, dateTimePickerDate, lblBackDate, Convert.ToDateTime(txtRequestDate.Text).ToString("dd/MMM/yyyy"), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (Convert.ToDateTime(txtRequestDate.Text).Date != DateTime.Now.Date)
                    {
                        MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dateTimePickerDate.Focus();
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dateTimePickerDate.Focus();
                    return false;
                }
            }

            if (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca))
            {
                MessageBox.Show("You have not allow any location for transaction.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (gvItem.Rows.Count <= 0)
            {
                MessageBox.Show("Please add item.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (ddlRequestType.SelectedValue == null)
            {
                ddlRequestType.Focus();
                MessageBox.Show("Please select Request Type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (ddlNewStatus.SelectedValue == null)
            {
                ddlNewStatus.Focus();
                MessageBox.Show("Please select change status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (ddlRequestType.SelectedValue.ToString() == "GRAN")
            {
                if (string.IsNullOrEmpty(txtTransferLocation.Text))
                {
                    txtTransferLocation.Focus();
                    MessageBox.Show("Please enter transfer location.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (string.IsNullOrEmpty(txtRemarks.Text.ToString()))
            {
                MessageBox.Show("Please enter remarks.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (RequestSerials == null)
            {
                MessageBox.Show("Please add items to List.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            if (ddlRequestType.SelectedValue.ToString() == "GRAN" && txtManualRef.Text != "")
            {
                if (!CHNLSVC.Inventory.IsValidManualDocument(BaseCls.GlbUserComCode, _masterLocation, "MDOC_GRAN", Convert.ToInt32(txtManualRef.Text)))
                {
                    MessageBox.Show("Invalid manual ref.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            if (ddlRequestType.SelectedValue.ToString() == "DIN" && txtManualRef.Text != "")
            {
                if (!CHNLSVC.Inventory.IsValidManualDocument(BaseCls.GlbUserComCode, _masterLocation, "MDOC_DIN", Convert.ToInt32(txtManualRef.Text)))
                {
                    MessageBox.Show("Invalid manual ref.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }


            //if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "", BackDateName, dateTimePickerDate, lblBackDate, dateTimePickerDate.Value.Date.ToString()) == false)
            //{
            //    if (dateTimePickerDate.Value.Date != DateTime.Now)
            //    {
            //        dateTimePickerDate.Enabled = true;
            //        MessageBox.Show("Back date not allow for selected date!", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        dateTimePickerDate.Focus();
            //        return false;
            //    }
            //}

            return true;
        }

        private bool ValidateDINRequestNo(string req)
        {

            InventoryRequest tem = new InventoryRequest();
            tem.Itr_req_no = req;
            InventoryRequest inv = CHNLSVC.Inventory.GetInventoryRequestData(tem);

            if (inv != null)
            {
                return true;
            }
            else
                return false;


        }

        private bool ValidateManulRef(string p)
        {
            int manref;
            if (!int.TryParse(p, out manref))
            {
                MessageBox.Show("Mannual Ref has to be Number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            _bkNo = "";
            string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            if (ddlRequestType.SelectedValue.ToString() == "GRAN")
            {
                if (!CHNLSVC.Inventory.IsValidManualDocument(BaseCls.GlbUserComCode, _masterLocation, "MDOC_GRAN", Convert.ToInt32(p)))
                {
                    MessageBox.Show("Invalid manual ref.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                //kapila 25/4/2016
                DataTable _dtBk = CHNLSVC.Inventory.GetManualDocBookNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "MDOC_GRAN", Convert.ToInt32(p), null);
                if (_dtBk.Rows.Count > 0) _bkNo = _dtBk.Rows[0]["mdd_bk_no"].ToString();
            }
            if (ddlRequestType.SelectedValue.ToString() == "DIN")
            {
                if (!CHNLSVC.Inventory.IsValidManualDocument(BaseCls.GlbUserComCode, _masterLocation, "MDOC_DIN", Convert.ToInt32(p)))
                {
                    MessageBox.Show("Invalid manual ref.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                //kapila 25/4/2016
                DataTable _dtBk = CHNLSVC.Inventory.GetManualDocBookNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "MDOC_DIN", Convert.ToInt32(p), null);
                if (_dtBk.Rows.Count > 0) _bkNo = _dtBk.Rows[0]["mdd_bk_no"].ToString();
            }
            return true;
        }

        #endregion

        #region textbox double click

        private void txtSerNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonSearchSerial_Click(null, null);
        }

        private void txtItemCode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonSearchItem_Click(null, null);
        }

        private void txtTransferLocation_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonSearchTransferLoc_Click(null, null);
        }

        private void txtDINno_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            button2_Click(null, null);
        }

        #endregion

        #region textbox leave

        private void txtTransferLocation_Leave_1(object sender, EventArgs e)
        {
            try
            {
                if (txtTransferLocation.Text != "")
                {
                    if (!CheckTranferLocation(txtTransferLocation.Text.ToUpper()))
                    {
                        MessageBox.Show("Invalid Transfer Location", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTransferLocation.Text = "";
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

        private void txtItemCode_Leave(object sender, EventArgs e)
        {
            try
            {
                LoadItemDetails();
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

        private void txtSerNo_Leave(object sender, EventArgs e)
        {
            try
            {
                LoadSerialData();
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

        private void txtTransferLocation_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtTransferLocation.Text != "")
                {
                    if (!CheckTranferLocation(txtTransferLocation.Text.ToUpper()))
                    {
                        MessageBox.Show("Invalid location code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTransferLocation.Focus();
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

        private void txtInDocNo_Leave(object sender, EventArgs e)
        {
            try
            {
                LoadDocNoData();
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

        private void txtDINno_Leave(object sender, EventArgs e)
        {
            try
            {
                LoadDIN();
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

        #endregion

        #region main button hide/display

        protected void DisplayButtons()
        {
            btnApprove.Visible = true;
            btnCancel.Visible = true;
            btnPrint.Visible = true;
            btnReject.Visible = true;
            btnSave.Visible = true;
            chkEnable.Visible = true;
            btnApproveCancel.Visible = true;

            toolStripSeparator1.Visible = true;
            toolStripSeparator3.Visible = true;
            toolStripSeparator4.Visible = true;
            toolStripSeparator5.Visible = true;
            toolStripSeparator6.Visible = true;
        }

        protected void HideButtons()
        {
            btnApprove.Visible = false;
            btnCancel.Visible = false;
            btnPrint.Visible = false;
            btnReject.Visible = false;
            btnSave.Visible = false;
            btnApproveCancel.Visible = false;

            toolStripSeparator1.Visible = false;
            toolStripSeparator3.Visible = false;
            toolStripSeparator4.Visible = false;
            toolStripSeparator5.Visible = false;
            toolStripSeparator6.Visible = false;
        }

        protected void EnableButtons()
        {
            btnApprove.Enabled = true;
            btnReject.Enabled = true;
            btnCancel.Enabled = true;
            btnPrint.Enabled = true;
            btnSave.Enabled = true;
            toolStripButton4.Enabled = true;
            toolStripButton5.Enabled = true;
        }

        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedIndex == 0)
                {
                    DisplayButtons();
                }
                else
                {
                    DropDownListType.Focus();
                    HideButtons();
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

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            lblNoofDoc.Text = "0";
            lblTotQty.Text = "0.00";
            try
            {
                GetSearchData();
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

        private void GetSearchData()
        {
            if (DropDownListType.SelectedValue == null)
                return;

            if (DropDownListType.SelectedValue.ToString() == "%")
            {
                InventoryRequest _inventoryRequest = new InventoryRequest();
                _inventoryRequest.Itr_com = BaseCls.GlbUserComCode;
                _inventoryRequest.Itr_loc = BaseCls.GlbUserDefLoca;
                if (pnlLoacationSearch.Visible && txtLocationSearch.Text != "")
                {
                    _inventoryRequest.Itr_loc = txtLocationSearch.Text;
                }
                if (pnlLoacationSearch.Visible && chkLocAll.Checked)
                {
                    _inventoryRequest.Itr_loc = "%";
                }
                _inventoryRequest.Itr_tp = "GRAN";
                string _itm = "";
                string _serial = "";
                string _cre = "";
                _itm = (string.IsNullOrEmpty(txtSeItem.Text)) ? "%" : txtSeItem.Text;
                _serial = (string.IsNullOrEmpty(txtSeSerial.Text)) ? "%" : txtSeSerial.Text;
                _cre = "%";


                string _fromDate = dateTimePickerFrom.Value.Date.ToString("dd/MM/yyyy");
                string _toDate = dateTimePickerTo.Value.Date.ToString("dd/MM/yyyy");


                _inventoryRequest.FromDate = _fromDate;
                _inventoryRequest.ToDate = _toDate;
                if (ddlRequestStatus.SelectedValue != null)
                    _inventoryRequest.Itr_stus = ddlRequestStatus.SelectedValue.ToString();
                else
                    _inventoryRequest.Itr_stus = "%";
                _inventoryRequest.Itr_cre_by = null;//(chkCreateUser.Checked) ? BaseCls.GlbUserID.ToUpper() : null;

                List<InventoryRequest> _reqHdr = new List<InventoryRequest>();
                DataTable dt = new DataTable();
                dt = CHNLSVC.Inventory.SearchGranDin(_inventoryRequest.Itr_com, _inventoryRequest.Itr_loc, _inventoryRequest.Itr_tp, _inventoryRequest.FromDate, _inventoryRequest.ToDate, _inventoryRequest.Itr_stus, _cre, _itm, _serial);


                _inventoryRequest = new InventoryRequest();
                _inventoryRequest.Itr_com = BaseCls.GlbUserComCode;
                _inventoryRequest.Itr_loc = BaseCls.GlbUserDefLoca;
                if (pnlLoacationSearch.Visible && txtLocationSearch.Text != "")
                {
                    _inventoryRequest.Itr_loc = txtLocationSearch.Text;
                }
                if (pnlLoacationSearch.Visible && chkLocAll.Checked)
                {
                    _inventoryRequest.Itr_loc = "%";
                }
                _inventoryRequest.Itr_tp = "DIN";


                _itm = (string.IsNullOrEmpty(txtSeItem.Text)) ? "%" : txtSeItem.Text;
                _serial = (string.IsNullOrEmpty(txtSeSerial.Text)) ? "%" : txtSeSerial.Text;
                _cre = "%";
                _fromDate = dateTimePickerFrom.Value.Date.ToString("dd/MM/yyyy");
                _toDate = dateTimePickerTo.Value.Date.ToString("dd/MM/yyyy");


                _inventoryRequest.FromDate = _fromDate;
                _inventoryRequest.ToDate = _toDate;
                if (ddlRequestStatus.SelectedValue != null)
                    _inventoryRequest.Itr_stus = ddlRequestStatus.SelectedValue.ToString();
                else
                    _inventoryRequest.Itr_stus = string.Empty;
                _inventoryRequest.Itr_cre_by = null;//(chkCreateUser.Checked) ? BaseCls.GlbUserID.ToUpper() : null;

                dt.Merge(CHNLSVC.Inventory.SearchGranDin(_inventoryRequest.Itr_com, _inventoryRequest.Itr_loc, _inventoryRequest.Itr_tp, _inventoryRequest.FromDate, _inventoryRequest.ToDate, _inventoryRequest.Itr_stus, _cre, _itm, _serial));

                // DataTable dt = new DataTable();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ITR_STUS"].ToString() == "")
                    {
                        dt.Rows.RemoveAt(i);
                    }
                }

                gvGRANList.DataSource = dt;


                if (dt == null || dt.Rows.Count <= 0)
                {
                    MessageBox.Show("No data found according to search Criteria", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    gvGRANList.DataSource = null;
                }

            }
            else
            {

                InventoryRequest _inventoryRequest = new InventoryRequest();
                _inventoryRequest.Itr_com = BaseCls.GlbUserComCode;
                _inventoryRequest.Itr_loc = BaseCls.GlbUserDefLoca;
                if (pnlLoacationSearch.Visible && txtLocationSearch.Text != "")
                {
                    _inventoryRequest.Itr_loc = txtLocationSearch.Text;
                }
                if (pnlLoacationSearch.Visible && chkLocAll.Checked)
                {
                    _inventoryRequest.Itr_loc = "%";
                }
                _inventoryRequest.Itr_tp = DropDownListType.SelectedValue.ToString();

                string _itm = "";
                string _serial = "";
                string _cre = "";
                _itm = (string.IsNullOrEmpty(txtSeItem.Text)) ? "%" : txtSeItem.Text;
                _serial = (string.IsNullOrEmpty(txtSeSerial.Text)) ? "%" : txtSeSerial.Text;
                _cre = "%";

                string _fromDate = dateTimePickerFrom.Value.Date.ToString("dd/MM/yyyy");
                string _toDate = dateTimePickerTo.Value.Date.ToString("dd/MM/yyyy");


                _inventoryRequest.FromDate = _fromDate;
                _inventoryRequest.ToDate = _toDate;
                if (ddlRequestStatus.SelectedValue != null)
                    _inventoryRequest.Itr_stus = ddlRequestStatus.SelectedValue.ToString();
                else
                    _inventoryRequest.Itr_stus = "%";
                _inventoryRequest.Itr_cre_by = null;//(chkCreateUser.Checked) ? BaseCls.GlbUserID.ToUpper() : null;

                List<InventoryRequest> _reqHdr = new List<InventoryRequest>();
                DataTable dt = CHNLSVC.Inventory.SearchGranDin(_inventoryRequest.Itr_com, _inventoryRequest.Itr_loc, _inventoryRequest.Itr_tp, _inventoryRequest.FromDate, _inventoryRequest.ToDate, _inventoryRequest.Itr_stus, _cre, _itm, _serial);

                // DataTable dt = new DataTable();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ITR_STUS"].ToString() == "")
                    {
                        dt.Rows.RemoveAt(i);
                    }
                }

                gvGRANList.DataSource = dt;

                if (dt == null || dt.Rows.Count <= 0)
                {
                    MessageBox.Show("No data found according to search Criteria", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    gvGRANList.DataSource = null;
                    return;
                }

                if (!Convert.ToString(DropDownListType.SelectedValue).Contains("All") && (!Convert.ToString(ddlRequestStatus.SelectedValue).Contains("All") && !Convert.ToString(ddlRequestStatus.SelectedValue).Contains("%")))
                {
                    int _doccount = dt.AsEnumerable().Select(x => x.Field<string>("Itr_req_no")).Distinct().Count();
                    DataTable _qty = CHNLSVC.Inventory.GetGRANQtySummary(_inventoryRequest.Itr_com, _inventoryRequest.Itr_loc, _inventoryRequest.Itr_tp, _inventoryRequest.FromDate, _inventoryRequest.ToDate, _inventoryRequest.Itr_stus, _cre, _itm, _serial);
                    if (_qty != null && _qty.Rows.Count > 0)
                    {
                        decimal _reqqty = _qty.Rows[0].Field<decimal>("ReqQty");
                        decimal _appqty = _qty.Rows[0].Field<decimal>("AppQty");
                        decimal _balqty = _qty.Rows[0].Field<decimal>("BalQty");
                        string _show = string.Empty;

                        switch (_inventoryRequest.Itr_stus)
                        {
                            case "A":
                                _show = _appqty.ToString();
                                break;
                            case "P":
                                _show = _reqqty.ToString();
                                break;
                            case "F":
                                _show = (_appqty - _balqty).ToString();
                                break;
                            case "C":
                                _show = _appqty.ToString();
                                break;
                            case "R":
                                _show = _appqty.ToString();
                                break;

                        }

                        lblTotQty.Text = _show;
                    }

                    lblNoofDoc.Text = _doccount.ToString();
                }
            }
        }

        private void gvGRANList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (e.ColumnIndex == 0)
                    {
                        string reqNo = gvGRANList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                        if (reqNo != "")
                        {
                            btnCancel.Enabled = true;
                            btnApprove.Enabled = true;
                            btnPrint.Enabled = true;
                            btnReject.Enabled = true;
                            requestNo = reqNo;
                            LoadRequestStatus(reqNo);
                        }
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

        private void DropDownListDINNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (DropDownListDINNo.SelectedValue == null || DropDownListDINNo.SelectedValue.ToString() == "")
                {
                    ItemAddClear();
                    pnlGRANItem.Enabled = true;
                    btnAddItem.Enabled = true;
                    gvItem.Columns[0].Visible = true;
                    gvItem.DataSource = null;
                    return;
                }
                LoadRequestStatusDIN(DropDownListDINNo.SelectedValue.ToString());
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

        private MasterAutoNumber GenerateMasterAutoNumber()
        {
            //string moduleText = ddlRequestType.SelectedValue.Equals("MRN") ? "MRN" : "INTR";

            string moduleText = ddlRequestType.SelectedValue.ToString().ToUpper();

            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_tp = "LOC";
            masterAuto.Aut_cate_cd = string.IsNullOrEmpty(BaseCls.GlbUserDefLoca) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = moduleText;
            masterAuto.Aut_number = 0;
            masterAuto.Aut_start_char = moduleText;
            masterAuto.Aut_year = null;

            return masterAuto;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                #region validation
                //if need
                //if (gvItem.Rows.Count >= 1)
                //{
                //    MessageBox.Show("Only one item can add","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                //    return;
                //}

                if (ddlRequestType.SelectedValue == null)
                {
                    ddlRequestType.Focus();
                    MessageBox.Show("Please select request type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (ddlRequestType.SelectedValue.ToString() == "GRAN" && txtTransferLocation.Text == "")
                {
                    txtTransferLocation.Focus();
                    MessageBox.Show("Please select Transfer location", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtItemDescription.Text == "")
                {
                    txtItemCode.Focus();
                    MessageBox.Show("Please select Item.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (ddlItemStatus.Enabled == true && ddlItemStatus.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select the Item status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ddlItemStatus.Focus();
                    return;
                }
                MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemCode.Text.Trim());
                if (_isGRAN_WO_Ser == false)
                {
                    // _iReqSe.Itri_itm_cond = "";
                    // _iReqSe.Itrs_itm_cd = txtItemCode.Text.ToString();
                    // _iReqSe.Itrs_in_itmline = 1;
                    //// _iReqSe.Itrs_i = txtItemDescription.Text.ToString();
                    // _iReqSe.Itrs_itm_stus = ddlItemStatus.SelectedValue.ToString();
                    //// _iReqSe.Itrs_itm_ = txtBrand.Text.ToString();
                    // //iReqSe.Tus_itm_brand = txtBrand.Text.ToString();
                    // _iReqSe.Itrs_qty = decimal.Parse(txtQty.Text.ToString());
                    // inReSe.Add(_iReqSe); //add by tharanga 2017/09/21
                    if (_item.Mi_is_ser1 != -1)
                    {
                        if (txtSerialID.Text == "")
                        {
                            txtSerNo.Focus();
                            MessageBox.Show("Please select valid serial.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            // return;
                        }
                    }
                    //if (txtSerialID.Text == "")
                    //{
                    //    txtSerNo.Focus();
                    //    MessageBox.Show("Please select valid serial.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}
                }
                else
                {
                    if (_item.Mi_is_ser1 != -1)
                    {
                        if (txtSerialID.Text == "")
                        {
                            txtSerNo.Focus();
                            MessageBox.Show("Please select valid serial.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                    }
                }
                if (ddlNewStatus.SelectedValue == null || ddlNewStatus.SelectedValue.ToString() == "")
                {
                    ddlNewStatus.Focus();
                    MessageBox.Show("Please select New status.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ReptPickSerials result = new ReptPickSerials();
                if (_item.Mi_is_ser1 != -1)
                {
                    result = CHNLSVC.Inventory.GetAvailableSerIDInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item.Mi_cd, txtSerNo.Text.Trim(), txtSerNo.Text.Trim(), txtSerialID.Text);
                }
               // ReptPickSerials result = CHNLSVC.Inventory.GetAvailableSerIDInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item.Mi_cd, txtSerNo.Text.Trim(), txtSerNo.Text.Trim(), txtSerialID.Text);
                ReptPickSerials _rept = new ReptPickSerials();
                if (_isGRAN_WO_Ser == false)
                {
                  // add  by tharanga 2017/09/22
                    if (_item.Mi_is_ser1 != -1)
                    {
                        _rept = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, null, txtItemCode.Text.Trim(), Convert.ToInt32(txtSerialID.Text));
                    }
                    else
                    {
                       
                        _rept.Tus_itm_cd = txtItemCode.Text.ToString();
                        //_rept.Tus_itm_line = lineNo;
                        //_rept.Tus_itm_desc = txtItemDescription.Text.ToString();
                        //_rept.Tus_itm_stus = ddlItemStatus.SelectedValue.ToString();
                        //_rept.Tus_itm_model = txtBrand.Text.ToString();
                        //_rept.Tus_itm_brand = txtBrand.Text.ToString();
                        //_rept.Tus_qty = decimal.Parse(txtQty.Text. ToString());

                        //InventoryRequestItem _itemnew = new InventoryRequestItem();
                        //_itemnew.Itri_itm_cd = txtItemCode.Text.ToString();
                        //_itemnew.Itri_itm_stus = ddlItemStatus.SelectedValue.ToString();
                        //_itemnew.Itri_qty = decimal.Parse(txtQty.Text.ToString());
                        //_itemnew.Itri_line_no = lineNo;
                        //_itemnew.Itri_mitm_cd = txtItemCode.Text.ToString();
                        // RequestItems.Add(_itemnew);
                       
                        
                    }
                    //_rept = CHNLSVC.Inventory.Get_all_details_on_serialID(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, null, txtItemCode.Text.Trim(), Convert.ToInt32(txtSerialID.Text));

                    if (_rept.Tus_itm_cd == null)
                    {
                        txtItemCode.Focus();
                        MessageBox.Show("Item Code Serial Mismatch", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                int val;
                if (!int.TryParse(txtQty.Text, out val))
                {
                    MessageBox.Show("Quantity has to be a number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                #endregion

                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                if (_item == null)
                {
                    MessageBox.Show("Item can not add", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {

                    if (ddlRequestType.SelectedValue.ToString() == "DIN")
                    {
                        if (_isGRAN_WO_Ser == false)
                        {
                            ReptPickSerials _reptSer = new ReptPickSerials();
                            _reptSer = CHNLSVC.Inventory.GetAvailableSerIDInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItemCode.Text.ToString(), txtSerNo.Text.ToString(), otherSerial, txtSerialID.Text.ToString());
                            if (_reptSer.Tus_itm_stus == "DMG")
                            {
                                MessageBox.Show("DMG status items can not add to DIN.");
                                return;
                            }
                            if (_reptSer.Tus_itm_stus == "DMGLP")
                            {
                                MessageBox.Show("DMGLP status items can not add to DIN.");
                                return;
                            }
                            InventoryRequest _invReq = CHNLSVC.Inventory.GetPendingRequest(txtItemCode.Text, Convert.ToInt32(txtSerialID.Text), txtInDocNo.Text);
                            if (_invReq != null)
                            {
                                MessageBox.Show("Item - " + txtItemCode.Text + " already have a GRAN/DIN\n" + "GRAN/DIN No - " + _invReq.Itr_req_no, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        AddItem();
                        ddlRequestType.Enabled = false;
                        //Check for date range
                        //remove comment when needed
                        //if (result.Tus_doc_dt.AddDays(3) >= DateTime.Now)
                        //{
                        //    AddItem();
                        //}
                        //else
                        //{
                        // MessageBox.Show("Document in date has to be in three days range","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        //}

                        if (result.Tus_doc_dt.AddDays(3) > DateTime.Now)
                        {
                            MessageBox.Show("Document in date is not in three days range", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {

                        }
                    }
                    if (ddlRequestType.SelectedValue.ToString() == "GRAN")
                    {
                        //ReptPickSerials _reptSer = new ReptPickSerials();
                        //_reptSer = CHNLSVC.Inventory.GetAvailableSerIDInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItemCode.Text.ToString(), txtSerNo.Text.ToString(), otherSerial, txtSerialID.Text.ToString());
                        //if (_reptSer.Tus_itm_stus == "DMG")
                        //{
                        //    MessageBox.Show("DMG status items can not add to GRAN.");
                        //    return; }

                        ReptPickSerials _reptSer = new ReptPickSerials();

                        if (_isGRAN_WO_Ser == false)
                        {
                            //add by tharanga 2017/09/22
                            if (_item.Mi_is_ser1 != -1)
                            {
                                _reptSer = CHNLSVC.Inventory.GetAvailableSerIDInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItemCode.Text.ToString(), txtSerNo.Text.ToString(), otherSerial, txtSerialID.Text.ToString());
                                InventoryRequest _invReq = CHNLSVC.Inventory.GetPendingRequest(txtItemCode.Text, Convert.ToInt32(txtSerialID.Text), txtInDocNo.Text);
                                if (_invReq != null)
                                {
                                    MessageBox.Show("Item - " + txtItemCode.Text + " already have a GRAN/DIN\n" + "GRAN/DIN No - " + _invReq.Itr_req_no, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                            //_reptSer = CHNLSVC.Inventory.GetAvailableSerIDInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItemCode.Text.ToString(), txtSerNo.Text.ToString(), otherSerial, txtSerialID.Text.ToString());
                            //InventoryRequest _invReq = CHNLSVC.Inventory.GetPendingRequest(txtItemCode.Text, Convert.ToInt32(txtSerialID.Text), txtInDocNo.Text);
                            //if (_invReq != null)
                            //{
                            //    MessageBox.Show("Item - " + txtItemCode.Text + " already have a GRAN/DIN\n" + "GRAN/DIN No - " + _invReq.Itr_req_no, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //    return;
                            //}
                        }

                        //added 2013/07/30
                        if (_reptSer.Tus_itm_stus == "CONS" && ddlNewStatus.SelectedValue.ToString() != "CONS")
                        {
                            MessageBox.Show("GRAN can not add Consignment items to other status", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // modify darshana 25-07-2014
                        if (ddlItemStatus.SelectedValue.ToString() != "DMG" && ddlItemStatus.SelectedValue.ToString() != "DMGLP")
                        {
                            if (ddlNewStatus.SelectedValue.ToString() == "DMG")
                            {
                                MessageBox.Show("GRAN can not add to Damage status", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            if (ddlNewStatus.SelectedValue.ToString() == "DMGLP")
                            {
                                MessageBox.Show("GRAN can not add to Damage status", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        //Chamal 24/07/2014 //Comment by darshana 25-07-2014
                        //if (ddlNewStatus.SelectedValue.ToString() != ddlItemStatus.SelectedValue.ToString())
                        //{
                        //    MessageBox.Show("GRAN can not add to because request status and actual status differ!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    return;
                        //}


                        //kapila 19/5/2014
                        if (string.IsNullOrEmpty(ddlCond.Text))
                        {
                            MessageBox.Show("Select the condition of the item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (_item.Mi_itm_stus == "RVTLP" || _item.Mi_itm_stus == "RVT")
                        {
                            if (result.Tus_doc_dt.AddMonths(3) >= _date)
                            {
                                AddItem();
                                //set enable true when delete all items in grid
                                ddlRequestType.Enabled = false;
                                ddlNewStatus.Enabled = false;
                                ddlCond.Enabled = false;
                            }
                            else
                            {
                                MessageBox.Show("Revert item can only GRAN after three months", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            AddItem();
                            ddlRequestType.Enabled = false;
                            ddlNewStatus.Enabled = false;
                            ddlCond.Enabled = false;
                        }
                    }
                }

                //ddlNewStatus.Enabled = false;
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

        private void gvItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (e.ColumnIndex == 0)
                    {
                        if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //TODO: DELETE ITEM CODE
                            string itemCode = gvItem.Rows[e.RowIndex].Cells[1].Value.ToString();
                            string serialId = gvItem.Rows[e.RowIndex].Cells[11].Value.ToString();
                            DeleteItem(itemCode, serialId);
                        }
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

        private void DeleteItem(string itemCode, string serialId)
        {
            try
            {
                int line = 0;
                InventoryRequestSerials serial = RequestSerials.Where(x => x.Itrs_itm_cd == itemCode && x.Itrs_ser_id == Convert.ToInt32(serialId)).ToList<InventoryRequestSerials>()[0];
                line = serial.Itrs_line_no;
                InventoryRequestItem item = RequestItems.Where(x => x.Itri_itm_cd == itemCode && x.Itri_line_no == line).ToList<InventoryRequestItem>()[0];
                if (item.Itri_qty > 1)
                {
                    RequestItems.Where(x => x.Itri_itm_cd == itemCode && x.Itri_line_no == line).ToList<InventoryRequestItem>().ForEach(x => x.Itri_qty = x.Itri_qty - 1);
                    // RequestItems.Where(x => x.Itri_itm_cd == itemCode && x.Itri_line_no == line).ToList<InventoryRequestItem>().ForEach(x => x.Itri_app_qty = x.Itri_qty - 1);
                    // RequestItems.Where(x => x.Itri_itm_cd == itemCode && x.Itri_line_no == line).ToList<InventoryRequestItem>().ForEach(x => x.Itri_bqty = x.Itri_qty - 1);
                    RequestItems.Where(x => x.Itri_itm_cd == itemCode && x.Itri_line_no == line).ToList<InventoryRequestItem>().ForEach(x => x.Itri_app_qty = x.Itri_qty);
                    RequestItems.Where(x => x.Itri_itm_cd == itemCode && x.Itri_line_no == line).ToList<InventoryRequestItem>().ForEach(x => x.Itri_bqty = x.Itri_qty);
                }
                else
                {
                    RequestItems.Remove(item);
                }
                RequestSerials.Remove(serial);
                if (RequestSerials.Count > 0)
                {
                    BindingSource _source = new BindingSource();
                    _source.DataSource = RequestSerials;
                    gvItem.DataSource = _source;
                }
                else
                    gvItem.DataSource = null;

                btnAddItem.Enabled = true;
                ddlRequestType.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while processong\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddItem()
        {
            try
            {
                FF.BusinessObjects.MasterItem _itemchk = (FF.BusinessObjects.MasterItem)CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemCode.Text.ToUpper());
                //check item code and serial available already
                 List<InventoryRequestSerials> inReSe  =new List<InventoryRequestSerials> ();
                if (_itemchk.Mi_is_ser1 != -1)
                {
                  inReSe = RequestSerials.Where(x => x.Itrs_itm_cd == txtItemCode.Text.Trim() && x.Itrs_ser_id == Convert.ToInt32(txtSerialID.Text)).ToList<InventoryRequestSerials>();
                }
                //List<InventoryRequestSerials> inReSe = RequestSerials.Where(x => x.Itrs_itm_cd == txtItemCode.Text.Trim() && x.Itrs_ser_id == Convert.ToInt32(txtSerialID.Text)).ToList<InventoryRequestSerials>();
               // _iReqSe.Itri_itm_cond = "";
               // _iReqSe.Itrs_itm_cd = txtItemCode.Text.ToString();
               // _iReqSe.Itrs_in_itmline = 1;
               //// _iReqSe.Itrs_i = txtItemDescription.Text.ToString();
               // _iReqSe.Itrs_itm_stus = ddlItemStatus.SelectedValue.ToString();
               //// _iReqSe.Itrs_itm_ = txtBrand.Text.ToString();
               // //iReqSe.Tus_itm_brand = txtBrand.Text.ToString();
               // _iReqSe.Itrs_qty = decimal.Parse(txtQty.Text.ToString());
               // inReSe.Add(_iReqSe);
                if (inReSe.Count <= 0)
                {
                    
                    //if (_itemchk.Mi_is_ser1 == -1) //add  by tharanga 2017/09/22
                    //{
                    //    InventoryRequestSerials _iReqSe = new InventoryRequestSerials();
                      
                    //    _iReqSe.Itrs_itm_cd = txtItemCode.Text.ToString();
                    //    _iReqSe.Itrs_in_itmline = 1;
                    //    _iReqSe.Itrs_rmk = txtRemarks.Text.ToString();
                    //    _iReqSe.Itrs_ser_1 = "N/A";
                    //    _iReqSe.Itrs_ser_2 = "N/A";
                        
                    //    _iReqSe.Itrs_ser_id = 0;
                    //    _iReqSe.Mi_brand = _itemchk.Mi_brand;
                    //    _iReqSe.Mi_model = _itemchk.Mi_model;
                    //    _iReqSe.Itrs_rmk = txtRemarks.Text.ToString();
                      
                    //    _iReqSe.Itrs_itm_stus = ddlItemStatus.SelectedValue.ToString();
                    //    _iReqSe.Mi_longdesc = txtItemDescription.Text.ToString();
                    //    _iReqSe.Itrs_ser_id = 0;
                    //    string a = ddlNewStatus.SelectedValue.ToString();
                    //  //  _iReqSe.Itri_itm_cond = ddlCond.SelectedValue.ToString();
                    //    _iReqSe.Itrs_nitm_stus = ddlNewStatus.SelectedValue.ToString();
                     
                    //    _iReqSe.Itrs_qty = decimal.Parse(txtQty.Text.ToString());
                    //    inReSe.Add(_iReqSe);
                    //    RequestSerials.Add(_iReqSe);
                    //}
                   

                    //    if (FillRequestSerials() == null) {
                    //        if (cmbSubType.SelectedValue != null)
                    //        {
                    //            if (cmbSubType.SelectedValue.ToString() == "STOCK")
                    //            {
                    //                MessageBox.Show("Type(STOCK ITEM) and selected item mismatch\nPlease select Type(CUSTOMER ITEM)", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //            }
                    //            else if (cmbSubType.SelectedValue.ToString() == "CUSTM")
                    //            {
                    //                MessageBox.Show("Type(CUSTOMER ITEM) and selected item mismatch\nPlease select Type(STOCK ITEM)", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //            }
                    //        }
                    //        return;
                    // }
                    bool isHaveItem = false;
                    int line = 0;
                    List<InventoryRequestItem> _itm = (from _res in RequestItems
                                                       where _res.Itri_itm_cd == txtItemCode.Text && _res.Itri_itm_stus == ddlItemStatus.SelectedValue.ToString()
                                                       select _res).ToList<InventoryRequestItem>();
                    if (_itm != null && _itm.Count > 0)
                    {
                        _itm[0].Itri_qty = _itm[0].Itri_qty + 1;
                        _itm[0].Itri_app_qty = _itm[0].Itri_qty;
                        _itm[0].Itri_bqty = _itm[0].Itri_qty;
                        isHaveItem = true;
                        line = _itm[0].Itri_line_no;
                    }
                    else
                    {
                        isHaveItem = false;
                        lineNo += 1;
                        line = lineNo;
                    }
                    InventoryRequestItem _item = FillRequestItm(line);
                    InventoryRequestSerials _serial=new InventoryRequestSerials ();
                    // tharanga 2017/09/22
                    itm_alw_auto_approvel = false;


                        _serial.Itri_itm_cat = _itemchk.Mi_cate_1;//add by tharanga 2017/11/11
                        DataTable newdt = new DataTable();// Cheak Werhose allow change item states add by tharanga 2017/11/13
                        if (ddlRequestType.SelectedValue.ToString() == "GRAN")
                        {
                            newdt = CHNLSVC.Inventory.SPGETLOCTMCAT(BaseCls.GlbUserComCode, txtTransferLocation.Text.ToUpper(), _itemchk.Mi_cate_1, ddlNewStatus.SelectedValue.ToString(), "GRAN", ddlItemStatus.SelectedValue.ToString());
                            if (newdt.Rows.Count <= 0)
                            {
                                MessageBox.Show("Transfer Location " + txtTransferLocation.Text.ToUpper() + " Not allowd", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            HpSystemParameters _SystemPara = new HpSystemParameters();
                            _SystemPara = CHNLSVC.Sales.GetSystemParameter("LOC", txtTransferLocation.Text.ToUpper(), "GRANATAPP", DateTime.Now.Date);
                            if (!string.IsNullOrEmpty(_SystemPara.Hsy_pty_cd))
                            {
                                 List<GRAN_ALWSTUS> _listGRAN_ALWSTUS = new List<GRAN_ALWSTUS>();
                                _listGRAN_ALWSTUS = CHNLSVC.Inventory.GET_GRAN_ALWSTUS(BaseCls.GlbUserComCode, "GRAN", ddlItemStatus.SelectedValue.ToString(), ddlNewStatus.SelectedValue.ToString());

                                if (_listGRAN_ALWSTUS.Count == 0)
                                { itm_alw_auto_approvel = false; }
                                else
                                { itm_alw_auto_approvel = true; }
                            }
                            else if ("Intact Packing" == ddlCond.Text.ToString() && (ddlNewStatus.SelectedValue.ToString() == "GOD" || ddlNewStatus.SelectedValue.ToString() == "GDLP" || ddlNewStatus.SelectedValue.ToString() == "CONS"))
                            //if ("Intact Packing" == ddlCond.Text.ToString())
                            {
                                List<GRAN_ALWSTUS> _listGRAN_ALWSTUS = new List<GRAN_ALWSTUS>();
                                _listGRAN_ALWSTUS = CHNLSVC.Inventory.GET_GRAN_ALWSTUS(BaseCls.GlbUserComCode, "GRAN", ddlItemStatus.SelectedValue.ToString(), ddlNewStatus.SelectedValue.ToString());

                                if (_listGRAN_ALWSTUS.Count == 0)
                                { itm_alw_auto_approvel = false; }
                                else
                                { itm_alw_auto_approvel = true; }

                                if (_itemchk.Mi_is_ser1 != -1)
                                {
                                    if (itm_alw_auto_approvel == true)
                                    {
                                        List<GRAN_ALWSTUS> _listGRAN_ALWSTUS_MOR_CON = new List<GRAN_ALWSTUS>();
                                        _listGRAN_ALWSTUS_MOR_CON = CHNLSVC.Inventory.GET_GRAN_ALWSTUS_MORE_CON(BaseCls.GlbUserComCode, "GRAN", ddlItemStatus.SelectedValue.ToString(), ddlNewStatus.SelectedValue.ToString(), _itemchk.Mi_cate_1, _itemchk.Mi_cate_2, _itemchk.Mi_cate_3);
                                        if (_listGRAN_ALWSTUS_MOR_CON.Count > 0)
                                        {
                                            ReptPickSerials _reptSer = new ReptPickSerials();
                                            _reptSer = CHNLSVC.Inventory.GetAvailableSerIDInformation(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItemCode.Text.ToString(), txtSerNo.Text.ToString(), otherSerial, txtSerialID.Text.ToString());
                                            double date_dif = (dateTimePickerDate.Value.Date - _reptSer.Tus_orig_grndt).TotalDays;
                                            if (Convert.ToDouble(_listGRAN_ALWSTUS_MOR_CON.FirstOrDefault().mga_intact_days) < date_dif)
                                            {
                                                itm_alw_auto_approvel = true;
                                            }
                                            else
                                            {
                                                decimal avl_qty = 0;
                                                decimal bufer_lvl = 0;
                                                #region avaliable stock
                                                List<InventoryLocation> _inventoryLocation = null;
                                                //if (string.IsNullOrEmpty(_status))
                                                //    _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _item.Trim(), string.Empty);
                                                //else
                                                _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtItemCode.Text.ToString(), ddlItemStatus.SelectedValue.ToString());

                                                if (_inventoryLocation != null)
                                                    if (_inventoryLocation.Count > 0)
                                                    {
                                                        var _aQty = _inventoryLocation.Select(x => x.Inl_qty).Sum();
                                                        var _aFree = _inventoryLocation.Select(x => x.Inl_free_qty).Sum();
                                                        avl_qty = _aQty;
                                                    }
                                                #endregion

                                                #region bufer_lvl
                                                MasterLocation _loc = CHNLSVC.General.GetAllLocationByLocCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, 1);
                                                int month = System.DateTime.Now.Month;
                                                int date = int.Parse(DateTime.Today.ToString("dd"));

                                                bufer_lvl = CHNLSVC.Inventory.get_buffer_qty(txtItemCode.Text.ToString(), BaseCls.GlbDefChannel, _loc.Ml_buffer_grd, date, month);
                                                #endregion
                                                if (bufer_lvl < avl_qty)
                                                { itm_alw_auto_approvel = true; }
                                                else
                                                { itm_alw_auto_approvel = false; }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                List<GRAN_ALWSTUS> _listGRAN_ALWSTUS = new List<GRAN_ALWSTUS>();
                                _listGRAN_ALWSTUS = CHNLSVC.Inventory.GET_GRAN_ALWSTUS(BaseCls.GlbUserComCode, "GRAN", ddlItemStatus.SelectedValue.ToString(), ddlNewStatus.SelectedValue.ToString());

                                if (_listGRAN_ALWSTUS.Count == 0)
                                { itm_alw_auto_approvel = false; }
                                else
                                { itm_alw_auto_approvel = true; }

                            }
                        }
                    
                   
                    //end by tharanga
                    if (_itemchk.Mi_is_ser1 != -1)
                    {
                         _serial = FillRequestSerials(line);
                         _serial.Itri_itm_cat = _itemchk.Mi_cate_1;//add by tharanga 2017/11/11
                         _serial.itm_alw_auto_approvel = itm_alw_auto_approvel; //add by tharanga 2017/11/13
                         _serial.Itri_itm_cond = ddlCond.Text.ToString();
                        RequestSerials.Add(_serial);

                         _item = FillRequestItm(line);
                        _item.Itri_itm_stus = _serial.Itrs_itm_stus;
                    }
                    else if (_itemchk.Mi_is_ser1 == -1)
                    {
                        _serial = FillRequestSerialsDecimalItm(line);
                        _serial.Itri_itm_cat = _itemchk.Mi_cate_1;//add by tharanga 2017/11/11
                        _serial.itm_alw_auto_approvel = itm_alw_auto_approvel; //add by tharanga 2017/11/13
                        _serial.Itri_itm_cond = ddlCond.Text.ToString();
                        RequestSerials.Add(_serial);

                        _item = FillRequestItm(line);
                        _item.Itri_itm_stus = _serial.Itrs_itm_stus;
                    }
                   
                    //InventoryRequestSerials _serial = FillRequestSerials(line);
                    //RequestSerials.Add(_serial);

                    //InventoryRequestItem _item = FillRequestItm(line);
                    //_item.Itri_itm_stus = _serial.Itrs_itm_stus;


                    if (!isHaveItem)
                    {
                        RequestItems.Add(_item);
                    }
                    //if item in list update qty
                    //else add


                    BindingSource _source = new BindingSource();
                    _source.DataSource = RequestSerials;
                    gvItem.DataSource = _source;

                    ddlRequestType.Enabled = false;
                    txtTransferLocation.Enabled = false;
                    buttonSearchTransferLoc.Enabled = false;
                    ItemAddClear();
                    //btnAddItem.Enabled = false;
                    toolStrip1.Focus();
                    btnSave.Select();
                }
                else
                {
                    MessageBox.Show("Item alredy in GRAN", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occured while Processing\n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ComboBoxDraw(DataTable table, ComboBox combo, string code, string desc)
        {

            combo.DataSource = table;
            combo.DisplayMember = desc;
            combo.ValueMember = code;

            // Enable the owner draw on the ComboBox.
            combo.DrawMode = DrawMode.OwnerDrawVariable;
            // Handle the DrawItem event to draw the items.
            combo.DrawItem += delegate(object cmb, DrawItemEventArgs args)
            {

                // Draw the default background
                args.DrawBackground();


                // The ComboBox is bound to a DataTable,
                // so the items are DataRowView objects.
                DataRowView drv = (DataRowView)combo.Items[args.Index];

                // Retrieve the value of each column.
                string id = drv[code].ToString();
                string name = drv[desc].ToString();

                // Get the bounds for the first column
                Rectangle r1 = args.Bounds;
                r1.Width /= 2;

                // Draw the text on the first column
                using (SolidBrush sb = new SolidBrush(args.ForeColor))
                {
                    args.Graphics.DrawString(id, args.Font, sb, r1);
                }

                // Draw a line to isolate the columns 
                using (Pen p = new Pen(Color.Black))
                {
                    args.Graphics.DrawLine(p, r1.Right - 5, 0, r1.Right - 5, r1.Bottom);
                }

                // Get the bounds for the second column
                Rectangle r2 = args.Bounds;
                r2.X = args.Bounds.Width / 2;
                r2.Width /= 2;

                // Draw the text on the second column
                using (SolidBrush sb = new SolidBrush(args.ForeColor))
                {
                    args.Graphics.DrawString(name, args.Font, sb, r2);
                }

            };
        }

        private void DropDownListType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ddlRequestStatus.Focus();
            }
        }

        private void ddlRequestStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePickerFrom.Focus();
            }
        }

        private void dateTimePickerFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePickerTo.Focus();
            }
        }

        private void dateTimePickerTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSearch.Focus();
            }
        }

        private void txtManualRef_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtManualRef.Text != "")
                {
                    if (!ValidateManulRef(txtManualRef.Text))
                        txtManualRef.Text = "";
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

        private void btnApproveCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Update("C");
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

        private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
        {
            txtRequestDate.Text = dateTimePickerDate.Value.ToString("dd/MM/yyyy"); ;
        }

        private void btnLocation_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLoaction;
                _CommonSearch.txtSearchbyword.Text = txtLoaction.Text;
                _CommonSearch.ShowDialog();
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

        private void btnLoactionSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLocationSearch;
                _CommonSearch.txtSearchbyword.Text = txtLocationSearch.Text;
                _CommonSearch.ShowDialog();
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

        private void btnSeItem_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSeItem;
                _CommonSearch.ShowDialog();
                txtSeItem.Select();
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

        private void btnSeSerial_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial);
                DataTable _result = CHNLSVC.CommonSearch.SearchAvlbleSerial4Invoice(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSeSerial;
                _CommonSearch.ShowDialog();
                txtSeSerial.Select();
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

        private void chkEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnable.Checked)
                DropDownListAppStatus.Enabled = true;
            else
                DropDownListAppStatus.Enabled = false;
        }

        private void ddlCond_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAddItem.Focus();
        }


        /// <summary>
        /// Enter key press for whole form
        /// work according to tab index
        /// </summary>
        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    if (keyData.HasFlag(Keys.Enter))
        //    {
        //        SelectNextControl(ActiveControl, true, true, true, true);
        //    }

        //    return base.ProcessCmdKey(ref msg, keyData);
        //}

        //Add by akila 2017/01/10
        private bool isValid(string _transferLocation)
        {
            bool _isValid = true;

            try
            {
                IEnumerable<DataRow> ItemStatus = CHNLSVC.Inventory.GetItemStatusDefinition_byLocation(BaseCls.GlbUserComCode, _transferLocation).AsEnumerable();
                if (ItemStatus != null)
                {
                    foreach (DataRow _row in ItemStatus)
                    {
                        List<string> _availableItemStatus = ItemStatus.Select(x => x.Field<string>("mis_desc")).ToList();
                        if (gvItem.Rows.Count > 0)
                        {
                            BindingSource _bindSource = (BindingSource)gvItem.DataSource;
                            List<InventoryRequestSerials> _requestedSerails = new List<InventoryRequestSerials>();
                            _requestedSerails = _bindSource.DataSource as List<InventoryRequestSerials>;

                            if (_requestedSerails != null)
                            {
                                foreach (InventoryRequestSerials _item in _requestedSerails)
                                {
                                    //comment by akila 2017/01/15
                                    //int _count = ItemStatus.Where(x => x.Field<string>("mlas_itm_stus") != _item.Itrs_itm_stus).Count();
                                    //if (_count > 0)
                                    //{
                                    //    MessageBox.Show("Invalid operation. Items cannot be saved" + Environment.NewLine + "Selected location only have permission to approve items having " + string.Join(", ", _availableItemStatus) + " status", "GRAN Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //    return false;
                                    //}
                                    int _count = ItemStatus.Where(x => x.Field<string>("mlas_itm_stus") == _item.Itrs_itm_stus).Count();
                                    if (_count == 0)
                                    {
                                        MessageBox.Show("Invalid operation. Items cannot be saved" + Environment.NewLine + "Selected location only have permission to approve items having " + string.Join(", ", _availableItemStatus) + " status", "GRAN Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return false;

                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select an item", "GRAN Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show("An error occurred while validating item details." + Environment.NewLine + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _isValid = false;
            }

            return _isValid;
        }


        private InventoryRequestSerials FillRequestSerialsDecimalItm(int line)
        {
            InventoryRequestSerials _tempSer = new InventoryRequestSerials();

            ReptPickSerials _reptSer = new ReptPickSerials();
            
                _tempSer.Itrs_in_batchline = 1;
                _tempSer.Itrs_in_docdt = DateTime.Now.Date; // Convert.ToDateTime(_reptSer.Tus_doc_dt).Date;
                _tempSer.Itrs_in_docno = "";  // _reptSer.Tus_doc_no;
                _tempSer.Itrs_in_itmline = 1;
                _tempSer.Itrs_in_seqno = 1;
                _tempSer.Itrs_in_serline = 1;
                _tempSer.Itrs_itm_cd = txtItemCode.Text.Trim();
                if (ddlRequestType.SelectedValue.ToString() == "DIN")
                {
                    if (ddlItemStatus.SelectedValue.ToString() == "DMG")
                    {
                        return null;
                    }
                    if (ddlItemStatus.SelectedValue.ToString() == "DMGLP")
                    {
                        return null;
                    }
                }

                _tempSer.Itrs_itm_stus = ddlItemStatus.SelectedValue.ToString();
                _tempSer.Itrs_line_no = line;
                int serLin;
                try
                {
                    //serLin = Convert.ToInt32(RequestSerials.Max(x => x.Itrs_ser_line).ToString());
                    int _line = RequestSerials.Where(r => r.Itrs_itm_cd == _tempSer.Itrs_itm_cd && r.Itrs_itm_stus == _tempSer.Itrs_itm_stus).FirstOrDefault().Itrs_ser_line;
                    serLin = _line == 0 ? 1 : _line++;

                    line = RequestSerials.Where(r => r.Itrs_itm_cd == _tempSer.Itrs_itm_cd).FirstOrDefault().Itrs_in_itmline == 0 ? line : RequestSerials.Where(r => r.Itrs_itm_cd == _tempSer.Itrs_itm_cd).FirstOrDefault().Itrs_in_itmline;

                }
                catch (Exception) { serLin = 0; }
                _tempSer.Itrs_ser_line = ++serLin;
                _tempSer.Itrs_nitm_stus = ddlNewStatus.SelectedValue.ToString();
                //REQUESTED BY ASANKA 2013/04/04
                if (ddlRequestType.SelectedValue.ToString() == "DIN")
                {
                    _tempSer.Itrs_nitm_stus = "DMG";
                }
                _tempSer.Itrs_qty = Convert.ToInt32(txtQty.Text);
                _tempSer.Itrs_rmk = txtItemRemarks.Text.ToString();
                _tempSer.Itrs_seq_no = 0;
                _tempSer.Itrs_ser_1 = txtSerNo.Text.Trim();
                _tempSer.Itrs_ser_2 = "N/A";
                _tempSer.Itrs_ser_3 = "N/A";
                _tempSer.Itrs_ser_4 = "";
                _tempSer.Itrs_ser_id = 0;
                // _tempSer.Itrs_ser_line = line;
                // _tempSer.Itrs_trns_tp = ddlTransferType.SelectedValue.ToString();
                _tempSer.Mi_longdesc = txtItemDescription.Text.ToString();
                _tempSer.Mi_model = txtModelNo.Text.ToString();
                _tempSer.Mi_brand = txtBrand.Text.ToString();
           
            
            return _tempSer;
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            InventoryLocation invloc=new InventoryLocation ();
            invloc.Inl_com=BaseCls.GlbUserComCode;
            invloc.Inl_loc=BaseCls.GlbUserDefLoca;
            invloc.Inl_itm_cd=txtItemCode.Text.ToString();
            invloc.Inl_itm_stus=ddlItemStatus.SelectedValue.ToString();
            InventoryLocation _tmpLocBal = new InventoryLocation();
            _tmpLocBal = CHNLSVC.Inventory.GET_INR_LOC_BALANCE(invloc);
            if (! string.IsNullOrEmpty(txtQty.Text))
            {
                if (_tmpLocBal.Inl_free_qty < Convert.ToDecimal(txtQty.Text.ToString()))
                {
                    //inr_loc free - gran pending qty >0
                    //free qty grnpengf
                    MessageBox.Show("Item QTY invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtQty.Focus();
                    return;
                }
            }
          
        }
    }
}
