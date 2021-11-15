using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using FF.WebERPClient.UserControls;
using FF.BusinessObjects;

namespace FF.WebERPClient.Inventory_Module
{
    public partial class SerialScan : BasePage
    {
        private static List<ReptPickSerialsSub> _reptPickSerialsSubList;

        private string _paraDocumentNo = "N/A";//referance Document no

        public Int32 ParaSeqNo
        {
            get { return Convert.ToInt32(ViewState["paraSeqNo"]); }
            set { ViewState["paraSeqNo"] = value; }
        }

        public Int32 ParaIsCheckItemStatus
        {
            get { return Convert.ToInt32(ViewState["paraIsCheckItemStatus"]); }
            set { ViewState["paraIsCheckItemStatus"] = value; }
        }

        public Int32 ParaIsCheckSimilerItem
        {
            get { return Convert.ToInt32(ViewState["paraIsCheckSimilerItem"]); }
            set { ViewState["paraIsCheckSimilerItem"] = value; }
        }

        public Int32 ParaIsCheckRequestQty
        {
            get { return Convert.ToInt32(ViewState["paraIsCheckRequestQty"]); }
            set { ViewState["paraIsCheckRequestQty"] = value; }
        }


        private Int32 _paraSerialID = 0;
        private decimal _paraUnitCost = 0;
        private decimal _paraUnitPrice = 0;
        private string _paraWarrantyNo = "N/A";
        private Int16 _paraWarrantyPeriod = 0;
        private string _paraWarrantyRemarks = string.Empty;
        private static Boolean _isSaveSubSerials = false;


        protected void Page_Load(object sender, EventArgs e)
        {
            SetInitialValues();
            if (!IsPostBack)
            {
                _reptPickSerialsSubList = new List<ReptPickSerialsSub>();
                BindBinCode();
                BindCompanyItemStatus();

                BindScanSerials();
                //BindScanSubSerial();
                BindScanRequestItems();

                //For serial search
                txtSerial1.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnSerial.ClientID + "')");
                //For item search
                txtItem.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnItem.ClientID + "')");
                //For sub item search
                txtSItem.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnSItem.ClientID + "')");
                //For get main item description
                txtItem.Attributes.Add("onblur", "GetItemDescription('" + txtItem.ClientID + "','" + txtItemDesc.ClientID + "|" + txtItem.ClientID + "');IsItemSerialized_1('" + txtItem.ClientID + "','" + txtQty.ClientID + "|" + txtSerial1.ClientID + "');IsItemSerialized_2('" + txtItem.ClientID + "','" + hdnIsSerialized_2.ClientID + "');IsItemSerialized_3('" + txtItem.ClientID + "','" + hdnIsSerialized_3.ClientID + "');IsUOMDecimalAllow('" + txtItem.ClientID + "','" + txtQty.ClientID + "');IsItemHaveSubSerial('" + txtItem.ClientID + "','" + HdnIsHaveSubItem.ClientID + "')");
                //For get sub item description
                txtSItem.Attributes.Add("onblur", "GetItemDescription('" + txtSItem.ClientID + "','" + txtSubItemDesc.ClientID + "|" + txtSItem.ClientID + "');IsItemSerialized_1('" + txtSItem.ClientID + "','" + txtSQty.ClientID + "|" + txtSSerial.ClientID + "');IsUOMDecimalAllow('" + txtSItem.ClientID + "','" + txtSQty.ClientID + "')");
            }
            txtBin.Attributes.Add("ReadOnly", "ReadOnly");
            txtStatus.Attributes.Add("ReadOnly", "ReadOnly");
            txtSStatus.Attributes.Add("ReadOnly", "ReadOnly");
        }

        private void SetInitialValues()
        {
            //GlbSerialScanUserSeqNo = 1;
            //GlbSerialScanDocumentType = "G";
            //_paraDocumentNo = "d1";
            ParaSeqNo = 2;

            // ParaRequestNo = "1";
        }

        #region Searching Area
        /// <summary>
        /// Search Serial from available inventory
        /// </summary>
        /// <param name="sender"> button object </param>
        /// <param name="e"> event sender </param>
        protected void imgBtnSerial_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerial);
            DataTable dataSource = CHNLSVC.CommonSearch.GetAvailableSerialSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtSerial1.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        /// <summary>
        /// Search Item
        /// </summary>
        /// <param name="sender"> button</param>
        /// <param name="e">event</param>
        protected void imgBtnItem_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable dataSource = CHNLSVC.CommonSearch.GetItemSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtItem.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        protected void imgBtnSItem_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable dataSource = CHNLSVC.CommonSearch.GetItemSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtSItem.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerial:
                    {
                        paramsText.Append(GlbUserComCode + seperator + "RWHAE" + seperator + "LGDVD270" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        #region Data Bind
        /// <summary>
        /// Bind Bin Codes
        /// </summary>
        /// <param name="_direction">In/Out</param>
        private void BindBinCode()
        {

            dgvBin.DataSource = CHNLSVC.Inventory.GetAllLocationBin(GlbUserComCode, GlbUserDefLoca);
            dgvBin.DataBind();

        }
        /// <summary>
        /// Bind Company Item Status
        /// </summary>
        /// <param name="_direction"> In/Out </param>
        private void BindCompanyItemStatus()
        {
            dgvStatus.DataSource = CHNLSVC.Inventory.GetAllCompanyStatus(GlbUserComCode);
            dgvStatus.DataBind();
            dgvSStatus.DataSource = CHNLSVC.Inventory.GetAllCompanyStatus(GlbUserComCode);
            dgvSStatus.DataBind();
        }

        private void BindScanSerials()
        {
            gvMainSerial.DataSource = CHNLSVC.Inventory.GetAllScanSerials(GlbUserComCode, GlbUserDefLoca, GlbUserName, GlbSerialScanUserSeqNo, GlbSerialScanDocumentType);
            gvMainSerial.DataBind();
        }

        private void BindScanSubSerial()
        {
            gvSSerial.DataSource = CHNLSVC.Inventory.GetAllScanSubSerials(GlbSerialScanUserSeqNo, GlbSerialScanDocumentType);
            gvSSerial.DataBind();
        }

        private void BindScanRequestItems()
        {
            //gvRequest.DataSource = CHNLSVC.Inventory.GetAllScanRequestItems(GlbSerialScanRequestNo);
            //gvRequest.DataBind();
        }


        #endregion

        #region  User Events
        Int32 _count = 0;
        protected void BinRowDataBound(object sender, GridViewRowEventArgs e)
        {

            System.Data.DataRowView drv;
            drv = (System.Data.DataRowView)e.Row.DataItem;
            if (drv != null)
            {
                int length = drv[0].ToString().Length;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    dgvBin.Columns[1].ItemStyle.Width = length * 30;
                    // pnlBin.Width = length * 74;
                    dgvBin.Columns[1].ItemStyle.Wrap = false;
                    e.Row.Attributes.Add("id", "tab" + _count.ToString());

                    if (e.Row.RowState == DataControlRowState.Alternate)
                    {
                        e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue';");
                        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='White';");
                    }
                    else
                    {
                        e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue';");
                        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#EFF3FB';");
                    }


                    e.Row.Attributes.Add("onclick", " TableRowClicks(" + _count.ToString() + ",'" + txtBin.ClientID + "')");
                    _count += 1;
                }
            }
        }

        protected void BinSelectedIndexChanged(object sender, EventArgs e)
        {
            txtBin.Text = dgvBin.SelectedRow.Cells[0].Text;
        }

        protected void StatusRowDataBound(object sender, GridViewRowEventArgs e)
        {
            System.Data.DataRowView drv;
            drv = (System.Data.DataRowView)e.Row.DataItem;
            if (drv != null)
            {
                int length = drv[0].ToString().Length;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    dgvStatus.Columns[1].ItemStyle.Width = length * 30;
                    //pnlStatus.Width = length * 74;
                    dgvStatus.Columns[1].ItemStyle.Wrap = false;
                    e.Row.Attributes.Add("id", "tab" + _count.ToString());

                    if (e.Row.RowState == DataControlRowState.Alternate)
                    {
                        e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue';");
                        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='White';");
                    }
                    else
                    {
                        e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue';");
                        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#EFF3FB';");
                    }


                    e.Row.Attributes.Add("onclick", " TableRowClicks(" + _count.ToString() + ",'" + txtStatus.ClientID + "')");
                    _count += 1;

                }
            }
        }

        protected void StatusSelectedIndexChanged(object sender, EventArgs e)
        {
            txtStatus.Text = dgvStatus.SelectedRow.Cells[0].Text;
        }

        protected void SStatusRowDataBound(object sender, GridViewRowEventArgs e)
        {
            System.Data.DataRowView drv;
            drv = (System.Data.DataRowView)e.Row.DataItem;
            if (drv != null)
            {
                int length = drv[0].ToString().Length;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    dgvSStatus.Columns[1].ItemStyle.Width = length * 30;
                    // pnlSStatus.Width = length * 74;
                    dgvSStatus.Columns[1].ItemStyle.Wrap = false;
                    e.Row.Attributes.Add("id", "tab" + _count.ToString());

                    if (e.Row.RowState == DataControlRowState.Alternate)
                    {
                        e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue';");
                        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='White';");
                    }
                    else
                    {
                        e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue';");
                        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#EFF3FB';");
                    }

                    e.Row.Attributes.Add("onclick", " TableRowClicks(" + _count.ToString() + ",'" + txtSStatus.ClientID + "')");
                    _count += 1;

                }
            }
        }

        protected void SStatusSelectedIndexChanged(object sender, EventArgs e)
        {
            txtSStatus.Text = dgvSStatus.SelectedRow.Cells[0].Text;
        }

        protected void ScanSerialRowDataBound(object sender, GridViewRowEventArgs e)
        {

            System.Data.DataRowView drv;
            drv = (System.Data.DataRowView)e.Row.DataItem;
            if (drv != null)
            {
                int length = drv[0].ToString().Length;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    e.Row.Attributes.Add("id", "tab" + _count.ToString());

                    if (e.Row.RowState == DataControlRowState.Alternate)
                    {
                        e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue';");
                        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='White';");
                    }
                    else
                    {
                        e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue';");
                        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#EFF3FB';");
                    }

                    _count += 1;
                }
            }
        }

        protected void ScanSubSerialRowDataBound(object sender, GridViewRowEventArgs e)
        {

            //System.Data.DataRowView drv;
            //drv = (System.Data.DataRowView)e.Row.DataItem;
            //if (drv != null)
            //{
            //    int length = drv[0].ToString().Length;
            //    if (e.Row.RowType == DataControlRowType.DataRow)
            //    {
            //        e.Row.Attributes.Add("id", "tab" + _count.ToString());

            //        if (e.Row.RowState == DataControlRowState.Alternate)
            //        {
            //            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue';");
            //            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='White';");
            //        }
            //        else
            //        {
            //            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue';");
            //            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#EFF3FB';");
            //        }
            //        _count += 1;
            //    }
            //}
        }

        protected void ScanRequestItemRowDataBound(object sender, GridViewRowEventArgs e)
        {

            System.Data.DataRowView drv;
            drv = (System.Data.DataRowView)e.Row.DataItem;
            if (drv != null)
            {
                int length = drv[0].ToString().Length;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    e.Row.Attributes.Add("id", "tab" + _count.ToString());

                    if (e.Row.RowState == DataControlRowState.Alternate)
                    {
                        e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue';");
                        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='White';");
                    }
                    else
                    {
                        e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='lightblue';");
                        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#EFF3FB';");
                    }
                    _count += 1;
                }
            }
        }

        #endregion

        [System.Web.Services.WebMethod]
        public static void IsHaveSubSerial(string _isHaveSubSerials)
        {
            if (_isHaveSubSerials == "true")
                _isSaveSubSerials = true;
            else
                _isSaveSubSerials = false;
        }

        protected void SaveToTemporary(Object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBin.Text.Trim()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the bin!");
                txtBin.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtItem.Text.Trim()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the item!");
                txtItem.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtStatus.Text.Trim()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the item status!");
                txtStatus.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtSerial1.Text.Trim()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the serial!");
                txtSerial1.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtSerial2.Text.Trim()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the serial!");
                txtSerial2.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtQty.Text.Trim()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the qty!");
                txtQty.Focus();
                return;
            }

            if (hdnIsSerialized_1.Value == "1" && (string.IsNullOrEmpty(txtSerial1.Text) || txtSerial1.Text == "N/A"))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected item is serialized. please enter the serial no!");
                txtSerial1.Focus();
                return;
            }
            else if (hdnIsSerialized_1.Value == "0")
            {
                txtSerial1.Text = "N/A";
            }


            if (hdnIsSerialized_2.Value == "1" && (string.IsNullOrEmpty(txtSerial2.Text) || txtSerial2.Text == "N/A"))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected item is having serial 2. please enter the serial no!");
                txtSerial2.Focus();
                return;
            }
            else if (hdnIsSerialized_2.Value == "0")
            {
                txtSerial2.Text = "N/A";
            }

            else if (hdnIsSerialized_3.Value == "0")
            {
                txtSerial3.Text = "N/A";
            }


            if (hdnIsSerialized_1.Value == "1")
            {
                txtQty.Text = "1";
            }


            //if (HdnIsHaveSubItem.Value == "true" && _isSaveSubSerials == true)
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter the sub item details first and add the main item details");
            //    txtSItem.Focus();
            //    return;
            //}

            if (_isSaveSubSerials == true && gvSSerial.Rows.Count <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter the sub item details");
                txtSItem.Focus();
                return;
            }

            ReptPickSerials _reptPickSerials = null;

            try
            {
                if (GlbSerialScanDocumentType == "GRN")
                {
                    DateTime _date = Convert.ToDateTime(DateTime.Now.Date).Date;
                    _reptPickSerials = new ReptPickSerials();
                    _reptPickSerials.Tus_batch_line = 1;
                    _reptPickSerials.Tus_bin = txtBin.Text.Trim();
                    _reptPickSerials.Tus_com = GlbUserComCode.Trim();
                    _reptPickSerials.Tus_cre_by = GlbUserName.Trim();
                    _reptPickSerials.Tus_cre_dt = _date;
                    _reptPickSerials.Tus_doc_dt = _date;
                    _reptPickSerials.Tus_doc_no = _paraDocumentNo;
                    _reptPickSerials.Tus_exist_grncom = GlbUserComCode.Trim();
                    _reptPickSerials.Tus_exist_grndt = _date;
                    _reptPickSerials.Tus_exist_grnno = _paraDocumentNo;
                    _reptPickSerials.Tus_exist_supp = "N/A";//TODO : GET SUPPLIER
                    _reptPickSerials.Tus_itm_cd = txtItem.Text.Trim();
                    _reptPickSerials.Tus_itm_line = 1;
                    _reptPickSerials.Tus_itm_stus = txtStatus.Text.Trim();
                    _reptPickSerials.Tus_loc = GlbUserDefLoca;
                    _reptPickSerials.Tus_orig_grncom = GlbUserComCode.Trim();
                    _reptPickSerials.Tus_orig_grndt = _date;
                    _reptPickSerials.Tus_orig_grnno = _paraDocumentNo;
                    _reptPickSerials.Tus_orig_supp = "N/A";//TODO : GET SUPPLIER
                    _reptPickSerials.Tus_qty = Convert.ToDecimal(txtQty.Text.Trim());
                    _reptPickSerials.Tus_seq_no = ParaSeqNo;
                    _reptPickSerials.Tus_ser_1 = txtSerial1.Text.Trim();
                    _reptPickSerials.Tus_ser_2 = txtSerial2.Text.Trim();
                    _reptPickSerials.Tus_ser_3 = txtSerial3.Text.Trim();
                    _reptPickSerials.Tus_ser_4 = "NA";
                    _reptPickSerials.Tus_ser_id = _paraSerialID;
                    _reptPickSerials.Tus_ser_line = 1;
                    _reptPickSerials.Tus_session_id = GlbUserSessionID;
                    _reptPickSerials.Tus_unit_cost = _paraUnitCost;
                    _reptPickSerials.Tus_unit_price = _paraUnitPrice;
                    _reptPickSerials.Tus_usrseq_no = GlbSerialScanUserSeqNo;
                    _reptPickSerials.Tus_warr_no = _paraWarrantyNo;
                    _reptPickSerials.Tus_warr_period = _paraWarrantyPeriod;


                }

                CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerials, _reptPickSerialsSubList);

                if (_isSaveSubSerials == true)
                {
                    _reptPickSerialsSubList = null;
                    _reptPickSerialsSubList = new List<ReptPickSerialsSub>();
                }

                _isSaveSubSerials = false;
                BindScanSerials();

                gvSSerial.DataSource = _reptPickSerialsSubList;
                gvSSerial.DataBind();

                BindScanRequestItems();

                ResetMainSerialScreenArea();
                ResetSubSerialScreenArea();
                ResetHiddenVariables();
            }
            catch (Exception es)
            {
                throw new Exception(es.Message);
            }
        }

        protected void UpdateReptPickSerialsSub(Object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtSItem.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the sub item!");
                txtSItem.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtSStatus.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the sub status!");
                txtSStatus.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtSQty.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the sub qty!");
                txtSQty.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtSSerial.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the sub serial!");
                txtSSerial.Focus();
                return;
            }


            ReptPickSerialsSub _reptPickSerialsSub = null;
            _reptPickSerialsSub = new ReptPickSerialsSub();
            _reptPickSerialsSub.Tpss_itm_cd = txtSItem.Text.Trim();
            _reptPickSerialsSub.Tpss_itm_stus = txtStatus.Text.Trim();
            _reptPickSerialsSub.Tpss_m_itm_cd = txtItem.Text.Trim();
            _reptPickSerialsSub.Tpss_m_ser = txtSerial1.Text.Trim();
            _reptPickSerialsSub.Tpss_mfc = "NA";
            _reptPickSerialsSub.Tpss_sub_ser = txtSSerial.Text.Trim();
            _reptPickSerialsSub.Tpss_tp = GlbSerialScanDocumentType;
            _reptPickSerialsSub.Tpss_usrseq_no = GlbSerialScanUserSeqNo;
            _reptPickSerialsSub.Tpss_warr_no = _paraWarrantyNo;
            _reptPickSerialsSub.Tpss_warr_period = _paraWarrantyPeriod;
            _reptPickSerialsSub.Tpss_warr_rem = _paraWarrantyRemarks;
            _reptPickSerialsSubList.Add(_reptPickSerialsSub);

            gvSSerial.DataSource = _reptPickSerialsSubList;
            gvSSerial.DataBind();
            ResetSubSerialScreenArea();
        }

        private void ResetMainSerialScreenArea()
        {
            txtBin.Text = "";
            txtItem.Text = "";
            txtItemDesc.Text = "";
            txtMFC.Text = "";
            txtQty.Text = "";
            txtSerial1.Text = "";
            txtSerial2.Text = "";
            txtSerial3.Text = "";
            txtStatus.Text = "";
        }

        private void ResetSubSerialScreenArea()
        {
            txtSItem.Text = "";
            txtSQty.Text = "";
            txtSSerial.Text = "";
            txtSStatus.Text = "";
        }

        private void ResetHiddenVariables()
        {
            HdnIsHaveSubItem.Value = "";
            hdnIsSerialized_1.Value = "";
            hdnIsSerialized_2.Value = "";
            hdnIsSerialized_3.Value = "";
            hdnResultControl.Value = "";
        }



    }
}