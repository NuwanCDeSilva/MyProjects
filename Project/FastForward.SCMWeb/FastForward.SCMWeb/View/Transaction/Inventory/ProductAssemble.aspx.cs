using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Inventory
{
    public partial class ProductAssemble : BasePage
    {
        #region PropertyData
        DataTable _serData
        {
            get { if (Session["_serData"] != null) { return (DataTable)Session["_serData"]; } else { return new DataTable(); } }
            set { Session["_serData"] = value; }
        }
        string _serType
        {
            get { if (Session["_serType"] != null) { return (string)Session["_serType"]; } else { return ""; } }
            set { Session["_serType"] = value; }
        }
        List<InventoryLocation> _StockBal = new List<InventoryLocation>();
        private List<MasterItemComponent> _itmComs
        {
            get { if (Session["_itmComs"] != null) { return (List<MasterItemComponent>)Session["_itmComs"]; } else { return new List<MasterItemComponent>(); } }
            set { Session["_itmComs"] = value; }
        }
        private List<MasterItemComponent> _itmComStock
        {
            get { if (Session["_itmComStock"] != null) { return (List<MasterItemComponent>)Session["_itmComStock"]; } else { return new List<MasterItemComponent>(); } }
            set { Session["_itmComStock"] = value; }
        }
        private MasterItemComponent _itmCom
        {
            get { if (Session["_itmCom"] != null) { return (MasterItemComponent)Session["_itmCom"]; } else { return new MasterItemComponent(); } }
            set { Session["_itmCom"] = value; }
        }
        private List<ReptPickSerials> _repPickSerAssProduct
        {
            get { if (Session["_repPickSerAssProduct"] != null) { return (List<ReptPickSerials>)Session["_repPickSerAssProduct"]; } else { return new List<ReptPickSerials>(); } }
            set { Session["_repPickSerAssProduct"] = value; }
        }

        private List<ReptPickSerials> _repPickSerItem
        {
            get { if (Session["_repPickSerItem"] != null) { return (List<ReptPickSerials>)Session["_repPickSerItem"]; } else { return new List<ReptPickSerials>(); } }
            set { Session["_repPickSerItem"] = value; }
        }
        private List<ReptPickSerials> _repPickSerialials
        {
            get { if (Session["_repPickSerialials"] != null) { return (List<ReptPickSerials>)Session["_repPickSerialials"]; } else { return new List<ReptPickSerials>(); } }
            set { Session["_repPickSerialials"] = value; }
        }
        private List<ReptPickSerials> _repPickSerScanHdr
        {
            get { if (Session["_repPickSerScanHdr"] != null) { return (List<ReptPickSerials>)Session["_repPickSerScanHdr"]; } else { return new List<ReptPickSerials>(); } }
            set { Session["_repPickSerScanHdr"] = value; }
        }
        private List<ReptPickSerials> _repPickSerScanItm
        {
            get { if (Session["_repPickSerScanItm"] != null) { return (List<ReptPickSerials>)Session["_repPickSerScanItm"]; } else { return new List<ReptPickSerials>(); } }
            set { Session["_repPickSerScanItm"] = value; }
        }
        private ReptPickSerials _repPickSer
        {
            get { if (Session["_repPickSer"] != null) { return (ReptPickSerials)Session["_repPickSer"]; } else { return new ReptPickSerials(); } }
            set { Session["_repPickSer"] = value; }
        }
        List<InventorySerialN> _intSerList = new List<InventorySerialN>(); 
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserCompanyCode"] == null || Session["UserID"] == null || Session["UserDefLoca"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                ClearData();
            }
        }
        private void ClearData()
        {
            txtCompany.Text = Session["UserCompanyCode"].ToString();
            txtLocation.Text = Session["UserDefLoca"].ToString();
            txtLocation_TextChanged(null,null);
            txtAssJobNo.Text = "";
            txtManualRef.Text = "";
            txtLocationStoreAt.Text = "";
            txtDate.Text = DateTime.Today.Date.ToString("dd/MMM/yyyy");

            txtProduct.Text = "";
            txtStatus.Text = "";
            txtNoOfUnit.Text = "";
            txtProduct.ToolTip = "";
            txtStatus.ToolTip = "";
            txtNoOfUnit.ToolTip = "";

            //txtProduct.Text = "LGPH-E405BK";
            //txtStatus.Text = "GOD";
            //txtNoOfUnit.Text = "1";

            //txtProduct.ToolTip = "LG PHONE E405 BLACK";
            //txtStatus.ToolTip = "GOOD";
            //txtNoOfUnit.Text = "1";

            lblItemCode.Text = "";
            //lblMfc.Text = "";
            lblStatus.Text = "";
            lblQtyInHand.Text = "";
            lblResQty.Text = "";
            lblFreeQty.Text = "";

            dgvBillOfQty.DataSource = new int[] { };
            dgvBillOfQty.DataBind();

            dgvScanHdr.DataSource = new int[] { };
            dgvScanHdr.DataBind();

            dgvScanItm.DataSource = new int[] { };
            dgvScanItm.DataBind();

            dgvFinishGoodDetails.DataSource = new int[] { };
            dgvFinishGoodDetails.DataBind();

            dgvItems.DataSource = new int[] { };
            dgvItems.DataBind();
            SessionClear();
        }
        private void SessionClear()
        {
            _serData = new DataTable();
            _serType = "";
            _itmComStock = new List<MasterItemComponent>();
            _itmComs = new List<MasterItemComponent>();
            _itmCom = new MasterItemComponent();
            _repPickSerialials = new List<ReptPickSerials>();
            _repPickSerItem = new List<ReptPickSerials>();
            _repPickSerScanItm= new List<ReptPickSerials>();
            _repPickSerScanHdr = new List<ReptPickSerials>();
            _repPickSer = new ReptPickSerials();
        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

        }

       
        protected void lbtnMainSearch_Click(object sender, EventArgs e)
        {
            lblQtyInHand.Text = "";
            lblResQty.Text = "";
            lblFreeQty.Text = "";
            SessionClear();
            dgvBillOfQty.DataSource = new int[] { };
            dgvFinishGoodDetails.DataSource = new int[] { };
            dgvItems.DataSource = new int[] { };
            dgvScanHdr.DataSource = new int[] { };
            dgvScanItm.DataSource = new int[] { };

            dgvBillOfQty.DataBind();
            dgvFinishGoodDetails.DataBind();
            dgvItems.DataBind();
            dgvScanHdr.DataBind();
            dgvScanItm.DataBind();

            #region Validation
            if (string.IsNullOrEmpty(txtCompany.Text))
            {
                DispMsg("Please select a company !!!"); return;
            }
            if (string.IsNullOrEmpty(txtLocation.Text))
            {
                DispMsg("Please select a location !!!"); return;
            }
            if (string.IsNullOrEmpty(txtProduct.Text))
            {
                DispMsg("Please select a finish good !!!"); return;
            }
            if (string.IsNullOrEmpty(txtStatus.Text))
            {
                DispMsg("Please select a product status !!!"); return;
            }
            Int32 qty = 0;
            Int32 noOfUnit = 0;
            noOfUnit = Int32.TryParse(txtNoOfUnit.Text, out qty) ? qty : 0;
            if (noOfUnit < 1)
            {
                DispMsg("Please enter valid units to be produced !!!"); return;
            }
            #endregion
            BindComponentStock();
            LoadProductData(txtCompany.Text.Trim().ToUpper(), txtLocation.Text.Trim().ToUpper(), txtProduct.Text.Trim().ToUpper(), txtStatus.Text.Trim().ToUpper());
            if (!ValidateItemKitData())
            {
                DispMsg("No stock available for this location !!!");
                return;
            }
            BindAssProduct();
            if (dgvFinishGoodDetails.Rows.Count>0)
            {
                TextBox txtTus_ser_1 = dgvFinishGoodDetails.Rows[0].FindControl("txtTus_ser_1") as TextBox;
                txtTus_ser_1.Focus();
            }
        }
        private void DispMsg(string msgText, string msgType="")
        {
            msgText = msgText.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");

            if (msgType == "N")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + msgText + "');", true);
            }
            else if (msgType == "W" || msgType=="")
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
        protected void txtCompany_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtLocation.Text = "";
                txtLocation_TextChanged(null,null);
                if (!string.IsNullOrEmpty(txtCompany.Text))
                {
                    bool b2 = false;
                    string toolTip = "";
                    DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.Company), "Code", txtCompany.Text.ToUpper());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtCompany.Text.ToUpper() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtCompany.ToolTip = toolTip;
                    }
                    else
                    {
                        txtCompany.ToolTip = "";
                        txtCompany.Text = "";
                        txtCompany.Focus();
                       DispMsg("Please enter a valid company !!!");
                        return;
                    }
                }
                else
                {
                    txtCompany.ToolTip = "";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtLocation_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtLocBin.Text = "";
                if (!string.IsNullOrEmpty(txtLocation.Text))
                {
                    if (string.IsNullOrEmpty(txtCompany.Text))
                    {
                        txtLocation.Text = "";
                        txtCompany.Focus();
                        DispMsg("Please select a company", "W"); return;
                    }
                    bool b2 = false;
                    string toolTip = "";
                    DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.Location), "Code", txtLocation.Text.ToUpper());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtLocation.Text.ToUpper() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                MasterBinLocation _bin = CHNLSVC.General.GetMasterBinLocation(new MasterBinLocation() { Ibl_com_cd = txtCompany.Text.ToUpper(), Ibl_loc_cd = txtLocation.Text.ToUpper() });
                                if (_bin != null)
                                {
                                    txtLocBin.Text = _bin.Ibl_bin_cd;
                                }
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtLocation.ToolTip = toolTip;
                    }
                    else
                    {
                        txtLocation.ToolTip = "";
                        txtLocation.Text = "";
                        txtLocation.Focus();
                        DispMsg("Please enter a valid location !!!");
                        return;
                    }
                }
                else
                {
                    txtLocation.ToolTip = "";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtAssJobNo_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtStatus_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtStatus.Text))
                {
                    bool b2 = false;
                    string toolTip = "";
                    DataTable _result = CHNLSVC.Inventory.GetAllCompanyStatus(Session["UserCompanyCode"].ToString());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["MIC_CD"].ToString()))
                        {
                            if (txtStatus.Text.ToUpper() == row["MIC_CD"].ToString())
                            {
                                b2 = true;
                                toolTip = row["MIS_DESC"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtStatus.ToolTip = toolTip;
                        txtNoOfUnit.Focus();
                    }
                    else
                    {
                        txtStatus.ToolTip = "";
                        txtStatus.Text = "";
                        txtStatus.Focus();
                        DispMsg("Please enter a valid status !!!");
                        return;
                    }
                }
                else
                {
                    txtStatus.ToolTip = "";
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtProduct_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtProduct.Text))
                {
                    bool b2 = false;
                    string toolTip = "";
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.Item), "ITEM", txtProduct.Text.ToUpper());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["ITEM"].ToString()))
                        {
                            if (txtProduct.Text.ToUpper() == row["ITEM"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                               
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtProduct.ToolTip = toolTip;
                        txtStatus.Focus();
                    }
                    else
                    {
                        txtProduct.ToolTip = "";
                        txtProduct.Text = "";
                        txtProduct.Focus();
                        DispMsg("Please enter a valid item !!!");
                        return;
                    }
                }
                else
                {
                    txtProduct.ToolTip = "";
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtFgStBin_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtLocBin.Text))
                {
                    if (string.IsNullOrEmpty(txtCompany.Text))
                    {
                        txtLocBin.Text = "";
                        txtCompany.Focus();
                        DispMsg("Please select a company", "W"); return;
                    }
                    if (string.IsNullOrEmpty(txtLocation.Text))
                    {
                        txtLocBin.Text = "";
                        txtLocation.Focus();
                        DispMsg("Please select a location", "W"); return;
                    }
                    bool b2 = false;
                    string toolTip = "";
                    DataTable _result = CHNLSVC.CommonSearch.SearchInrBinLoc(SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.SearchBINCodes), "Code", txtLocBin.Text.ToUpper());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtLocBin.Text.ToUpper() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtLocBin.ToolTip = toolTip;
                    }
                    else
                    {
                        txtLocBin.ToolTip = "";
                        txtLocBin.Text = "";
                        txtLocBin.Focus();
                        DispMsg("Please enter a valid fg stored bin !!!");
                        return;
                    }
                }
                else
                {
                    txtLocBin.ToolTip = "";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {

        }

        protected void dgvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvResult.PageIndex = e.NewPageIndex;
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                }
                else
                {
                    dgvResult.DataSource = new int[] { };
                }
                dgvResult.DataBind();
                txtSearchbyword.Text = string.Empty;
                txtSearchbyword.Focus();
                PopupSearch.Show();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!","E");
            }
        }

        protected void dgvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string code = dgvResult.SelectedRow.Cells[1].Text;
                if (_serType == "Company")
                {
                    txtCompany.Text = code;
                    txtCompany_TextChanged(null, null);
                }
                else if (_serType == "Location")
                {
                    txtLocation.Text = code;
                    txtLocation_TextChanged(null, null);
                }
                else if (_serType == "FGLocation")
                {
                    txtLocationStoreAt.Text = code;
                    txtLocationStoreAt_TextChanged(null, null);
                }
                else if (_serType == "SearchBINCodes")
                {
                    txtLocBin.Text = code;
                   // txtLocBin_TextChanged(null, null);
                }
                else if (_serType == "Item")
                {
                    txtProduct.Text = code;
                    txtProduct_TextChanged(null, null);
                }
                else if (_serType == "Status")
                {
                    txtStatus.Text = code;
                    txtStatus_TextChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        private string SetComSeInParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            // _basePage = new BasePage();
            System.Text.StringBuilder paramsText = new System.Text.StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SearchBINCodes:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtLocation.Text.ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
               
                default:
                    break;
            }
            return paramsText.ToString();
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (_serType != "Status")
                {
                    _serData = new DataTable();
                }
               
                if (_serType == "Company")
                {
                    _serData = CHNLSVC.CommonSearch.GetCompanySearchData(SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.Company), cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                }
                else if (_serType == "Location")
                {
                    _serData = CHNLSVC.CommonSearch.GetLocationSearchData(SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.Location), cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                }
                else if (_serType == "FGLocation")
                {
                    _serData = CHNLSVC.CommonSearch.GetLocationSearchData(SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.Location), cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                }
                else if (_serType == "SearchBINCodes")
                {
                    _serData = CHNLSVC.CommonSearch.SearchInrBinLoc(SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.SearchBINCodes), cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                }
                else if (_serType == "Item")
                {
                    _serData = CHNLSVC.CommonSearch.GetItemSearchData(SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.Item), cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text);
                }
                else if (_serType == "Status")
                {
                    if (string.IsNullOrEmpty(txtSearchbyword.Text))
                    {
                        _serData = CHNLSVC.Inventory.GetAllCompanyStatus(Session["UserCompanyCode"].ToString());
                        DataTable _dt = new DataTable();
                        _dt.Columns.Add("Code");
                        _dt.Columns.Add("Description");
                        foreach (DataRow dr in _serData.Rows)
                        {
                            DataRow ddr = _dt.NewRow();
                            ddr["Code"] = dr["MIC_CD"].ToString();
                            ddr["Description"] = dr["MIS_DESC"].ToString();
                            _dt.Rows.Add(ddr);
                        }
                        DataView dv = _dt.DefaultView;
                        dv.Sort = "Code ASC";
                        DataTable sortedDT = dv.ToTable();
                        _serData = sortedDT;
                    }
                    else
                    {
                        if (_serData.Rows.Count > 0)
                        {
                            DataView dv = new DataView(_serData);
                            string searchParameter = cmbSearchbykey.Text;
                            dv.RowFilter = "" + cmbSearchbykey.Text + " like '%" + txtSearchbyword.Text + "%'";

                            if (dv.Count > 0)
                            {
                                _serData = dv.ToTable();
                            }
                        }
                    }
                }
                dgvResult.DataSource = new int[] { };
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                }
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                dgvResult.DataBind();
                PopupSearch.Show();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        public void BindCmbSearchbykey(DataTable _dataSource)
        {
            this.cmbSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.cmbSearchbykey.Items.Add(col.ColumnName);
            }
            this.cmbSearchbykey.SelectedIndex = 0;
        }
        protected void lbtnSeCompany_Click(object sender, EventArgs e)
        {
            _serData = new DataTable();
            dgvResult.DataSource = new int[] { };
            _serData = CHNLSVC.CommonSearch.GetCompanySearchData(SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.Company), null, null);
            if (_serData.Rows.Count > 0)
            {
                dgvResult.DataSource = _serData;
                BindCmbSearchbykey(_serData);
            }
            dgvResult.DataBind();
            txtSearchbyword.Text = "";
            txtSearchbyword.Focus();
            _serType = "Company";
            PopupSearch.Show();
            if (dgvResult.PageIndex > 0)
            { dgvResult.SetPageIndex(0); }
        }

        protected void lbtnSeLocation_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCompany.Text))
            {
                DispMsg("Please select company !!!"); return;
            }
            _serData = new DataTable();
            dgvResult.DataSource = new int[] { };
            _serData = CHNLSVC.CommonSearch.GetLocationSearchData(SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.Location), null, null);
            if (_serData.Rows.Count > 0)
            {
                dgvResult.DataSource = _serData;
                BindCmbSearchbykey(_serData);
            }
            dgvResult.DataBind();
            txtSearchbyword.Text = "";
            txtSearchbyword.Focus();
            _serType = "Location";
            PopupSearch.Show();
            if (dgvResult.PageIndex > 0)
            { dgvResult.SetPageIndex(0); }
        }

        protected void lbtnSeAssJobNo_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnSeProduct_Click(object sender, EventArgs e)
        {
            _serData = new DataTable();
            dgvResult.DataSource = new int[] { };
            _serData = CHNLSVC.CommonSearch.GetItemSearchData(SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.Item), null, null);
            if (_serData.Rows.Count > 0)
            {
                dgvResult.DataSource = _serData;
                BindCmbSearchbykey(_serData);
            }
            dgvResult.DataBind();
            txtSearchbyword.Text = "";
            txtSearchbyword.Focus();
            _serType = "Item";
            PopupSearch.Show();
            if (dgvResult.PageIndex > 0)
            { dgvResult.SetPageIndex(0); }
        }

        protected void lbtnSeStatus_Click(object sender, EventArgs e)
        {
            _serData = new DataTable();
            dgvResult.DataSource = new int[] { };
            _serData = CHNLSVC.Inventory.GetAllCompanyStatus(Session["UserCompanyCode"].ToString());
            DataTable _dt = new DataTable();
            _dt.Columns.Add("Code");
            _dt.Columns.Add("Description");
            foreach (DataRow dr in _serData.Rows)
            {
                DataRow ddr = _dt.NewRow();
                ddr["Code"] = dr["MIC_CD"].ToString();
                ddr["Description"] = dr["MIS_DESC"].ToString();
                _dt.Rows.Add(ddr);
            }
            DataView dv = _dt.DefaultView;
            dv.Sort = "Code ASC";
            DataTable sortedDT = dv.ToTable();
            _serData = sortedDT;
            if (_serData.Rows.Count > 0)
            {
                dgvResult.DataSource = _serData;
                BindCmbSearchbykey(_serData);
            }
            dgvResult.DataBind();
            txtSearchbyword.Text = "";
            txtSearchbyword.Focus();
            _serType = "Status";
            PopupSearch.Show();
            if (dgvResult.PageIndex > 0)
            { dgvResult.SetPageIndex(0); }
        }

        protected void lbtnSeBin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCompany.Text))
            {
                DispMsg("Please select company !!!"); return;
            }
            if (string.IsNullOrEmpty(txtLocation.Text))
            {
                DispMsg("Please select location !!!"); return;
            }
            _serData = new DataTable();
            dgvResult.DataSource = new int[] { };
            _serData = CHNLSVC.CommonSearch.SearchInrBinLoc(SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.SearchBINCodes), null, null);
            if (_serData.Rows.Count > 0)
            {
                dgvResult.DataSource = _serData;
                BindCmbSearchbykey(_serData);
            }
            dgvResult.DataBind();
            txtSearchbyword.Text = "";
            txtSearchbyword.Focus();
            _serType = "SearchBINCodes";
            PopupSearch.Show();
            if (dgvResult.PageIndex > 0)
            { dgvResult.SetPageIndex(0); }
        }

        private void LoadProductData(string _company, string _location, string _item, string _status)
        {
            Int32 defInt = 0;
            lblItemCode.Text = _item;
           // lblMfc.Text = "";
            lblStatus.Text = "";
            DataTable _result = CHNLSVC.Inventory.GetAllCompanyStatus(_company);
            foreach (DataRow row in _result.Rows)
            {
                if (!string.IsNullOrEmpty(row["MIC_CD"].ToString()))
                {
                    if (_status == row["MIC_CD"].ToString())
                    {
                        lblStatus.Text =  row["MIS_DESC"].ToString();
                        break;
                    }
                }
            }
            _StockBal = new List<InventoryLocation>();
            _StockBal = CHNLSVC.Inventory.GetItemInventoryBalance(_company, _location,_item, _status);
            lblQtyInHand.Text = defInt.ToString("N2");
            lblResQty.Text = defInt.ToString("N2");
            lblFreeQty.Text = defInt.ToString("N2");
            if (_StockBal != null)
            {
                if (_StockBal.Count > 0)
                {
                    lblQtyInHand.Text = _StockBal[0].Inl_qty.ToString("N2");
                    lblResQty.Text = _StockBal[0].Inl_res_qty.ToString("N2");
                    lblFreeQty.Text = _StockBal[0].Inl_free_qty.ToString("N2");
                }
            }
        }
        private void BindComponentStock()
        {
            dgvBillOfQty.DataSource = new int[] { };
            _itmCom = new MasterItemComponent()
            {
                Micp_itm_cd = txtProduct.Text.ToUpper(),
                Micp_act = true,
                Mci_com = txtCompany.Text.Trim().ToUpper(),
                Mi_is_scansub = 0,
                Mi_itm_tp = "M",
                Mi_act = 1
            };
            _itmComStock = CHNLSVC.Inventory.Get_MST_ITM_COMPONENT(_itmCom);
            if (_itmComs != null)
            {
                if (_itmComStock.Count > 0)
                {
                    dgvBillOfQty.DataSource = BindMasterItemComponent(_itmComStock);
                }
            }
            dgvBillOfQty.DataBind();
        }

        private List<MasterItemComponent> BindMasterItemComponent(List<MasterItemComponent> _list)
        {
            if (_list!=null)
            {
                foreach (var item in _list)
                {
                    MasterItem _mstItm = CHNLSVC.General.GetItemMaster(item.Micp_comp_itm_cd);
                    if (_mstItm!=null)
                    {
                        item.ItemDesc = _mstItm.Mi_shortdesc;
                        item.ItemModel = _mstItm.Mi_model;
                        item.ItemIsSerialized = _mstItm.Mi_is_ser1;
                        item.ItemUom = _mstItm.Mi_weight_uom=="NULL" ? "N/A" : _mstItm.Mi_weight_uom;
                        DataTable dtModel = CHNLSVC.CommonSearch.GetAllModels("", "Code",item.ItemModel.ToUpper().Trim());
                        if (dtModel!=null)
                        {
                            if (dtModel.Rows.Count>0)
                            {
                                item.ItemModelDesc = dtModel.Rows[0]["Description"].ToString();
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(txtStatus.Text))
                    {
                        item.ItemStatus = txtStatus.Text.ToUpper().Trim();
                    }
                    if (!string.IsNullOrEmpty(txtNoOfUnit.Text))
                    {
                        Int32 noOfUnit = 0;
                        if (Int32.TryParse(txtNoOfUnit.Text,out noOfUnit))
                        {
                            item.ProductAssemblyQty = noOfUnit*(Int32) item.Micp_qty;
                        }
                    }
                }
            }
            return _list;
        }

      
        private void BindAssProduct()
        {
            dgvFinishGoodDetails.DataSource = new int[] { };
            _repPickSerAssProduct = new List<ReptPickSerials>();
            Int32 noOfQty=0;
            Int32 _qty = Int32.TryParse(txtNoOfUnit.Text, out noOfQty) ? noOfQty : 0;

            for (int i = 0; i < _qty; i++)
            {

                _repPickSer = new ReptPickSerials();
                _repPickSer.Tus_temp_itm_line = i + 1;
                _repPickSer.Tus_itm_cd = txtProduct.Text.Trim().ToUpper();
                MasterItem _mstItm = CHNLSVC.General.GetItemMaster(_repPickSer.Tus_itm_cd);
                if (_mstItm!=null)
                {
                    _repPickSer.Tus_itm_desc = _mstItm.Mi_shortdesc;
                    _repPickSer.Tus_itm_stus = txtStatus.Text.Trim().ToUpper();
                    _repPickSer.Tus_itm_stus_Desc = txtStatus.ToolTip.Trim().ToUpper();
                    _repPickSer.Tus_itm_model = _mstItm.Mi_model;
                    _repPickSer.Tus_itm_brand = _mstItm.Mi_itm_tp;
                    DataTable dtModel = CHNLSVC.CommonSearch.GetAllModels("", "Code", _repPickSer.Tus_itm_model);
                    if (dtModel != null)
                    {
                        if (dtModel.Rows.Count > 0)
                        {
                            _repPickSer.Tus_itm_model_desc = dtModel.Rows[0]["Description"].ToString();
                        }
                    }
                }

                _repPickSerAssProduct.Add(_repPickSer);
            }

            if (_repPickSerAssProduct.Count > 0)
            {
                dgvFinishGoodDetails.DataSource = _repPickSerAssProduct;
            }
            dgvFinishGoodDetails.DataBind();
        }

        protected void lbtnSelFinishGood_Click(object sender, EventArgs e)
        {
            try
            {
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                Label lblTus_itm_cd = row.FindControl("lblTus_itm_cd") as Label;
                Label lblTus_temp_itm_line = row.FindControl("lblTus_temp_itm_line") as Label;
                Label lblTus_itm_desc = row.FindControl("lblTus_itm_desc") as Label;
                TextBox txtTus_ser_1 = row.FindControl("txtTus_ser_1") as TextBox;
                Label lblTus_itm_stus_Desc = row.FindControl("lblTus_itm_stus_Desc") as Label;
                Label lblTus_ser_1 = row.FindControl("lblTus_ser_1") as Label;
                Label lblTus_warr_no = row.FindControl("lblTus_warr_no") as Label;
                Label lblTus_itm_stus = row.FindControl("lblTus_itm_stus") as Label;
                Label lblTus_ser_id = row.FindControl("lblTus_ser_id") as Label;

                if (string.IsNullOrEmpty(txtTus_ser_1.Text))
                {
                    txtTus_ser_1.Focus(); DispMsg("Please enter serial # !!!"); return;
                }

                _itmCom = new MasterItemComponent()
                {
                    Micp_itm_cd = lblTus_itm_cd.Text.ToUpper().Trim(),
                    Micp_act = true,
                    Mci_com = txtCompany.Text.ToUpper().Trim(),
                    Mi_is_scansub = 0,
                    Mi_itm_tp = "M",
                    Mi_act = 1
                };
                _itmComs = CHNLSVC.Inventory.Get_MST_ITM_COMPONENT(_itmCom);
                _repPickSerItem = new List<ReptPickSerials>();
                Int32 lineNo = 0;
                if (_itmComs != null)
                {
                    foreach (var com in _itmComs)
                    {
                        for (int i = 0; i < com.Micp_qty; i++)
                        {
                            lineNo++;
                            _repPickSer = new ReptPickSerials();
                            _repPickSer.Tus_itm_line = lineNo;
                            _repPickSer.MainItemSerialNo = txtTus_ser_1.Text;
                            _repPickSer.Tus_itm_cd = com.Micp_comp_itm_cd;
                            _repPickSer.Tus_itm_stus = lblTus_itm_stus.Text;
                            _repPickSer.Tus_itm_stus_Desc = lblTus_itm_stus_Desc.Text;
                            _repPickSer.Tus_temp_itm_line = Convert.ToInt32(lblTus_temp_itm_line.Text);
                            MasterItem _mstItm = CHNLSVC.General.GetItemMaster(_repPickSer.Tus_itm_cd);
                            _repPickSer.Tus_itm_model = _mstItm.Mi_model;
                            _repPickSer.Tus_itm_desc = _mstItm.Mi_shortdesc;
                            DataTable dtModel = CHNLSVC.CommonSearch.GetAllModels("", "Code", _repPickSer.Tus_itm_model);
                            if (dtModel != null)
                            {
                                if (dtModel.Rows.Count > 0)
                                {
                                    _repPickSer.Tus_itm_model_desc = dtModel.Rows[0]["Description"].ToString();
                                }
                            }
                            _repPickSer.Tus_itm_brand = _mstItm.Mi_weight_uom == "NULL" ? "N/A" : _mstItm.Mi_weight_uom;
                            _repPickSerItem.Add(_repPickSer);
                        }
                    }
                }
                dgvItems.DataSource = new int[] { };
                if (_repPickSerItem.Count > 0)
                {
                    dgvItems.DataSource = _repPickSerItem;
                }
                dgvItems.DataBind();
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        private void BindSaveData()
        {
            dgvScanHdr.DataSource = new int[] { };
            if (_repPickSerScanHdr.Count > 0)
            {
                dgvScanHdr.DataSource = _repPickSerScanHdr;
            }
            dgvScanHdr.DataBind();
        }

        private bool ValidateStockInHand(string _company, string _location, string _item, string _status, Int32 qty=1)
        {
            bool result = false;
            _StockBal = new List<InventoryLocation>();
            _StockBal = CHNLSVC.Inventory.GetItemInventoryBalance(_company, _location, _item, _status);
            if (_StockBal!=null)
            {
               // var v=_StockBal.Where(c=> c.Inl_free_qty>0);
                //Int32 stockBal=v.Count();
                if (_StockBal.Count > 0)
                {
                    if (_StockBal[0].Inl_free_qty >= qty)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        private bool ValidateItemKitData() {
            bool b = true;
            foreach (var item in _itmComStock)
            {
                b = ValidateStockInHand(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), item.Micp_comp_itm_cd, txtStatus.Text.Trim().ToUpper(), item.ProductAssemblyQty);
                if (!b)
                {
                    break;
                }
            }
            return b;
        }

        protected void lbtnSeFGLocation_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                dgvResult.DataSource = new int[] { };
                _serData = CHNLSVC.CommonSearch.GetLocationSearchData(SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.Location), null, null);
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                    BindCmbSearchbykey(_serData);
                }
                dgvResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                _serType = "FGLocation";
                PopupSearch.Show();
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtLocationStoreAt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtLocBin.Text = "";
                if (!string.IsNullOrEmpty(txtLocationStoreAt.Text))
                {
                    bool b2 = false;
                    string toolTip = "";
                    DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(SetComSeInParameters(CommonUIDefiniton.SearchUserControlType.Location), "Code", txtLocationStoreAt.Text.ToUpper());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtLocationStoreAt.Text.ToUpper() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                MasterBinLocation _bin = CHNLSVC.General.GetMasterBinLocation(new MasterBinLocation() { Ibl_com_cd = txtCompany.Text.ToUpper(), Ibl_loc_cd = txtLocationStoreAt.Text.ToUpper() });
                                if (_bin != null)
                                {
                                    txtLocStorAtBin.Text = _bin.Ibl_bin_cd;
                                }
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtLocationStoreAt.ToolTip = toolTip;
                    }
                    else
                    {
                        txtLocationStoreAt.ToolTip = "";
                        txtLocationStoreAt.Text = "";
                        txtLocationStoreAt.Focus();
                        DispMsg("Please enter a valid store at location !!!");
                        return;
                    }
                }
                else
                {
                    txtLocationStoreAt.ToolTip = "";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnSeBillOfQty_Click(object sender, EventArgs e)
        {
            try
            {
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                Label lblItmCode = row.FindControl("lblItmCode") as Label;
                LoadProductData(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), lblItmCode.Text, txtStatus.Text.ToUpper().Trim());
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtTus_ser_1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var lb = (TextBox)sender;
                var row = (GridViewRow)lb.NamingContainer;
                TextBox txtTus_ser_1 = row.FindControl("txtTus_ser_1") as TextBox;
                LinkButton lbtnSelFinishGood = row.FindControl("lbtnSelFinishGood") as LinkButton;
                Label lblTus_itm_cd = row.FindControl("lblTus_itm_cd") as Label;
                Label lblTus_temp_itm_line = row.FindControl("lblTus_temp_itm_line") as Label;
                Int32 lineNo=Convert.ToInt32(lblTus_temp_itm_line.Text);
                if (!string.IsNullOrEmpty(txtTus_ser_1.Text))
                {
                    var _serAss = _repPickSerAssProduct.Where(c => c.Tus_ser_1 == txtTus_ser_1.Text).ToList();
                    if (_serAss!=null)
                    {
                        if (_serAss.Count > 0)
                        {
                            txtTus_ser_1.Text = ""; txtTus_ser_1.Focus(); DispMsg("Entered serial # is already picked !!!"); return;
                        }
                    }
                    var _serHdr = _repPickSerScanHdr.Where(c => c.Tus_ser_1 == txtTus_ser_1.Text).ToList();
                    if (_serHdr != null)
                    {
                        if (_serHdr.Count > 0)
                        {
                            //txtTus_ser_1.Text = ""; txtTus_ser_1.Focus(); DispMsg("Entered serial # is already picked !!!"); return;
                        }
                    }
                    var _serItm = _repPickSerScanItm.Where(c => c.Tus_ser_1 == txtTus_ser_1.Text).ToList();
                    if (_serItm != null)
                    {
                        if (_serItm.Count > 0)
                        {
                            txtTus_ser_1.Text = ""; txtTus_ser_1.Focus(); DispMsg("Entered serial # is already picked !!!"); return;
                        }
                    }
                    bool _serAvailable = true;
                    _intSerList = new List<InventorySerialN>();
                    _intSerList = CHNLSVC.Inventory.Get_INR_SER_DATA(new InventorySerialN()
                    {
                        Ins_ser_1 = txtTus_ser_1.Text,
                        Ins_itm_cd = lblTus_itm_cd.Text,
                        Ins_loc=Session["UserDefLoca"].ToString(),
                        Ins_com = Session["UserCompanyCode"].ToString(),
                        Ins_available = 0,
                        Ser_tp=1
                    });
                    if (_intSerList!=null)
                    {
                        if (_intSerList.Count > 0)
                        {
                            _serAvailable = false;
                        }
                    }
                    
                    if (!_serAvailable)
                    {
                        txtTus_ser_1.Text = ""; txtTus_ser_1.Focus();
                        DispMsg("Entered serial # already exist !!!"); return;
                        //DispMsg("Please enter a valid serial #"); return;
                    }
                    else
                    {
                        foreach (var _product in _repPickSerAssProduct)
                        {
                            if (_product.Tus_temp_itm_line == lineNo)
                            {
                                _product.Tus_ser_1 = txtTus_ser_1.Text;
                                _product.SerialAvailable = true;
                            }
                        }
                    }
                    dgvFinishGoodDetails.DataSource = new int[] { };
                    if (_repPickSerAssProduct.Count > 0)
                    {
                        dgvFinishGoodDetails.DataSource = _repPickSerAssProduct;
                    }
                    dgvFinishGoodDetails.DataBind();
                    //var v = _repPickSerAssProduct.Where(c => c.SerialAvailable == true).ToList();
                    //if (v!=null)
                    //{
                        if (_repPickSerAssProduct.Count ==1) 
                        {
                            lbtnSelFinishGood_Click(lbtnSelFinishGood, null);
                            txtSubSerial.Text = "";
                            txtSubSerial.Focus();
                        }
                   // }
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtSubSerial_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSubSerial.Text))
                {
                    var _serAss = _repPickSerAssProduct.Where(c => c.Tus_ser_1 == txtSubSerial.Text).ToList();
                    if (_serAss != null)
                    {
                        if (_serAss.Count > 0)
                        {
                            //txtSubSerial.Text = ""; txtSubSerial.Focus(); DispMsg("Entered serial # is already picked !!!"); return;
                        }
                    }
                    var _serHdr = _repPickSerScanHdr.Where(c => c.Tus_ser_1 == txtSubSerial.Text).ToList();
                    if (_serHdr != null)
                    {
                        if (_serHdr.Count > 0)
                        {
                            //txtSubSerial.Text = ""; txtSubSerial.Focus(); DispMsg("Entered serial # is already picked !!!"); return;
                        }
                    }
                    var _serItm = _repPickSerScanItm.Where(c => c.Tus_ser_1 == txtSubSerial.Text).ToList();
                    if (_serItm != null)
                    {
                        if (_serItm.Count > 0)
                        {
                            txtSubSerial.Text = ""; txtSubSerial.Focus(); DispMsg("Entered serial # is already picked !!!"); return;
                        }
                    }
                    var vData = _repPickSerItem.Where(c => c.Tus_ser_1 == txtSubSerial.Text).ToList();
                    if (vData!=null)
                    {
                        if (vData.Count>0)
                        {
                            txtSubSerial.Text = ""; txtSubSerial.Focus(); DispMsg("Entered serial # is already picked !!!"); return;
                        }
                    }
                    List<InventorySerialN> _invSerMasterList = CHNLSVC.Inventory.Get_INR_SER_DATA(
                                            new InventorySerialN() { Ins_ser_1 = txtSubSerial.Text.Trim(), 
                                                 Ins_available = 1,Ins_com = Session["UserCompanyCode"].ToString(),
                                                                     Ins_loc = Session["UserDefLoca"].ToString()
                                            });
                    bool _InrSerAvailable = false;
                    if (_invSerMasterList != null)
                    {
                        if (_invSerMasterList.Count > 0)
                        {
                            _InrSerAvailable = true;
                        }
                    }
                    if (!_InrSerAvailable)
                    {
                        txtSubSerial.Text = ""; txtSubSerial.Focus(); DispMsg("Please enter a valid serial # !!!"); return;
                    }

                    _intSerList = new List<InventorySerialN>();
                    _intSerList = CHNLSVC.Inventory.Get_INT_SER_DATA(new InventorySerialN()
                    {
                        Ins_ser_1 = txtSubSerial.Text,
                      //  Ins_itm_cd = lblTus_itm_cd.Text,
                        Ins_loc = Session["UserDefLoca"].ToString(),
                        Ins_com = Session["UserCompanyCode"].ToString(),
                        Ins_available = 1
                    });
                    bool _serAvailable = false;
                    if (_intSerList != null)
                    {
                        if (_intSerList.Count > 0)
                        {
                            _serAvailable = true;
                        }
                    }
                    if (!_serAvailable)
                    {
                        txtSubSerial.Text = ""; txtSubSerial.Focus(); DispMsg("Please enter a valid serial # !!!"); return;
                    }
                    
                    bool haveItem = false;
                    bool tempSerAva = false;
                    foreach (var item in _repPickSerItem)
                    {
                        foreach (var serItm in _intSerList)
                        {
                            if (item.Tus_itm_cd == serItm.Ins_itm_cd)
                            {
                                List<InventorySerialN> _inrSerList = CHNLSVC.Inventory.Get_INR_SER_DATA(
                                                new InventorySerialN()
                                                {
                                                    Ins_ser_1 = txtSubSerial.Text.Trim(),
                                                    Ins_itm_cd = item.Tus_itm_cd,
                                                    Ins_available = 1,
                                                    Ins_com = Session["UserCompanyCode"].ToString(),
                                                    Ins_loc = Session["UserDefLoca"].ToString()
                                                });
                                if (_inrSerList != null)
                                {
                                    if (_inrSerList.Count > 0)
                                    {
                                        tempSerAva = true;
                                    }
                                }
                                if (!tempSerAva)
                                {
                                    txtSubSerial.Text = ""; txtSubSerial.Focus(); DispMsg("Please enter a valid serial # !!!"); return;
                                }
                                var _serAva = _inrSerList[0];
                                if (!item.SerialAvailable)
                                {
                                    item.Tus_ser_1 = _serAva.Ins_ser_1;
                                    item.Tus_ser_id = _serAva.Ins_ser_id;
                                    item.Tus_doc_no = _serAva.Ins_doc_no;
                                    item.Tus_itm_line = _serAva.Ins_itm_line;
                                    item.Tus_batch_line = _serAva.Ins_batch_line;
                                    item.Tus_seq_no = _serAva.Ins_seq_no;
                                    item.Tus_base_doc_no = _serAva.Ins_doc_no;
                                    item.Tus_base_itm_line = _serAva.Ins_itm_line;
                                    item.SerialAvailable = true;
                                    txtSubSerial.Text = "";
                                    haveItem = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (!haveItem)
                    {
                        txtSubSerial.Text = ""; txtSubSerial.Focus(); DispMsg("Selected serial # is not in the item list !!!"); return;
                    }
                    var v=_repPickSerItem.Where(c=> c.SerialAvailable==true).ToList();
                    if (_repPickSerAssProduct.Count>0)
                    {
                        if (_repPickSerItem.Count == v.Count)
                        {
                            
                            dgvFinishGoodDetails.DataSource = new int[] { };
                            if (_repPickSerAssProduct.Count > 0)
                            {
                                dgvFinishGoodDetails.DataSource = _repPickSerAssProduct;
                            }
                            dgvFinishGoodDetails.DataBind();
                            txtSubSerial.Focus();
                        }
                    }
                    dgvItems.DataSource = new int[] { };
                    if (_repPickSerItem.Count>0)
                    {
                        dgvItems.DataSource = _repPickSerItem;
                    }
                    dgvItems.DataBind(); txtSubSerial.Focus();
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtLocationStoreAt.Text))
                {
                    DispMsg("Please select a location !!!"); return;
                }
                Int32 pickCount = 0;
                var pickSer = _repPickSerItem.Where(c => c.SerialAvailable == true).ToList();
                pickCount = pickSer != null ? pickSer.Count : 0;
                if (_repPickSerItem.Count == pickCount && pickCount!=0)
                {
                    var hdrSer = _repPickSerAssProduct.Where(c => c.Tus_ser_1 == _repPickSerItem[0].MainItemSerialNo).FirstOrDefault();
                    if (hdrSer!=null)
                    {
                        if (!hdrSer.Tus_isSelect)
                        {
                            #region LoadPickSer
                            _intSerList = new List<InventorySerialN>();
                                _intSerList = CHNLSVC.Inventory.Get_INR_SER_DATA(new InventorySerialN()
                                {
                                    Ins_ser_1 = hdrSer.Tus_ser_1,
                                    Ins_itm_cd = hdrSer.Tus_itm_cd,
                                    Ins_loc = Session["UserDefLoca"].ToString(),
                                    Ins_com = Session["UserCompanyCode"].ToString(),
                                    Ins_available = 0,
                                    Ser_tp=1
                                });
                                bool serAvailable = true;
                                if (_intSerList != null)
                                {
                                    if (_intSerList.Count >0)
                                    {
                                        serAvailable = false;
                                    }
                                }
                                if (serAvailable)
                                {
                                    InventorySerialN _intSerN = new InventorySerialN();
                                    //_intSerN = _intSerList[0];
                                    _repPickSer = new ReptPickSerials();
                                    _repPickSer.Tus_itm_cd = hdrSer.Tus_itm_cd;
                                    _repPickSer.Tus_ser_1 = hdrSer.Tus_ser_1;
                                    _repPickSer.MainItemSerialNo = hdrSer.Tus_ser_1;
                                    _repPickSer.Tus_itm_desc = hdrSer.Tus_itm_desc;
                                    _repPickSer.Tus_itm_model = hdrSer.Tus_itm_model;
                                    _repPickSer.Tus_itm_model_desc = hdrSer.Tus_itm_model_desc;
                                    _repPickSer.Tus_itm_stus = hdrSer.Tus_itm_stus;
                                    _repPickSer.Tus_itm_stus_Desc = hdrSer.Tus_itm_stus_Desc;
                                    _repPickSer.Tus_qty = 1;
                                    _repPickSer.Tus_ser_id = 0;
                                    _repPickSer.Tus_ser_line = 0;
                                    _repPickSer.Tus_seq_no = 0;
                                    _repPickSer.Tus_itm_line =0;
                                    _repPickSer.Tus_unit_cost = 0;
                                    _repPickSer.Tus_unit_price = 0;
                                    _repPickSer.Tus_seq_no = hdrSer.Tus_seq_no;
                                    _repPickSer.Tus_doc_no = hdrSer.Tus_base_doc_no;
                                    _repPickSer.Tus_base_doc_no = hdrSer.Tus_base_doc_no;
                                    _repPickSer.Tus_base_itm_line = hdrSer.Tus_base_itm_line;
                                    MasterBinLocation _bin = CHNLSVC.General.GetMasterBinLocation(new MasterBinLocation()
                                    {
                                        Ibl_com_cd = Session["UserCompanyCode"].ToString(), 
                                        Ibl_loc_cd = txtLocation.Text.ToUpper() });
                                    if (_bin != null)
                                    {
                                        _repPickSer.Tus_bin= _bin.Ibl_bin_cd;
                                    }
                                    _repPickSer.Tus_loc = txtLocation.Text.ToUpper().Trim();
                                    _repPickSer.Tus_com = Session["UserCompanyCode"].ToString();
                                    _repPickSer.Tus_warr_no = "";
                                    _repPickSer.Tus_warr_period = 0;
                                    _repPickSer.Tus_cre_by = Session["UserID"].ToString();
                                    _repPickSer.Tus_cre_dt = DateTime.Now;
                                    //
                                    // _repPickSer.Tus_doc_no = _intSerN.Ins_doc_no;
                                    _repPickSerScanHdr.Add(_repPickSer);
                                }
                                else
                                {
                                    DispMsg("Assembly product serial # already exist !!!"); return;
                                }
                            #endregion

                            // Make all sub serial has pick
                            _repPickSerAssProduct.Where(c => c.Tus_temp_itm_line == hdrSer.Tus_temp_itm_line &&
                                c.Tus_ser_1 == _repPickSerItem[0].MainItemSerialNo).FirstOrDefault().Tus_isSelect = true;

                            #region BindAssProduct
                            dgvFinishGoodDetails.DataSource = new int[] { };
                            if (_repPickSerAssProduct.Count > 0)
                            {
                                dgvFinishGoodDetails.DataSource = _repPickSerAssProduct;
                            }
                            dgvFinishGoodDetails.DataBind();
                            #endregion

                            #region AOD In Main Product
                            
                            #endregion
                        }
                        #region AOD OUT
                        if (_repPickSerItem.Count > 0)
                        {
                            foreach (var item in _repPickSerItem)
                            {
                                _intSerList = new List<InventorySerialN>();
                                _intSerList = CHNLSVC.Inventory.Get_INT_SER_DATA(new InventorySerialN()
                                {
                                    Ins_ser_1 = item.Tus_ser_1,
                                    Ins_itm_cd = item.Tus_itm_cd,
                                    Ins_loc = Session["UserDefLoca"].ToString(),
                                    Ins_com = Session["UserCompanyCode"].ToString(),
                                    Ins_available = 1
                                });
                                
                                if (_intSerList!=null)
                                {
                                    #region LoadPickSer
                                    if (_intSerList.Count > 0)
                                    {
                                        InventorySerialN _intSerN = new InventorySerialN();
                                        _intSerN = _intSerList[0];
                                        _repPickSer = new ReptPickSerials();
                                        _repPickSer.Tus_itm_cd = item.Tus_itm_cd;
                                        _repPickSer.Tus_ser_1 = item.Tus_ser_1;
                                        _repPickSer.MainItemSerialNo = hdrSer.Tus_ser_1;
                                        _repPickSer.Tus_itm_desc = item.Tus_itm_desc;
                                        _repPickSer.Tus_itm_model = item.Tus_itm_model;
                                        _repPickSer.Tus_itm_model_desc = item.Tus_itm_model_desc;
                                        _repPickSer.Tus_itm_stus = item.Tus_itm_stus;
                                        _repPickSer.Tus_itm_stus_Desc = item.Tus_itm_stus_Desc;
                                        _repPickSer.Tus_qty = 1;
                                        _repPickSer.Tus_ser_id = _intSerN.Ins_ser_id;
                                        _repPickSer.Tus_ser_line = _intSerN.Ins_ser_line;
                                        _repPickSer.Tus_seq_no = _intSerN.Ins_seq_no;
                                        _repPickSer.Tus_itm_line = _intSerN.Ins_itm_line;
                                        _repPickSer.Tus_unit_cost = _intSerN.Ins_unit_cost;
                                        _repPickSer.Tus_unit_price = _intSerN.Ins_unit_price;
                                        _repPickSer.Tus_bin = _intSerN.Ins_bin;
                                        _repPickSer.Tus_loc = _intSerN.Ins_loc;
                                        _repPickSer.Tus_com = _intSerN.Ins_com;
                                        _repPickSer.Tus_warr_no = _intSerN.Ins_warr_no;
                                        _repPickSer.Tus_warr_period = _intSerN.Ins_warr_period;
                                        _repPickSer.Tus_cre_by = Session["UserID"].ToString();
                                        _repPickSer.Tus_cre_dt = DateTime.Now;
                                        _repPickSer.Tus_doc_no = item.Tus_base_doc_no;
                                        _repPickSer.Tus_itm_line = item.Tus_itm_line;
                                        _repPickSer.Tus_batch_line = item.Tus_batch_line;
                                        _repPickSer.Tus_seq_no = item.Tus_seq_no;
                                        _repPickSer.Tus_base_doc_no = item.Tus_base_doc_no;
                                        _repPickSer.Tus_base_itm_line = item.Tus_base_itm_line;
                                        //
                                       // _repPickSer.Tus_doc_no = _intSerN.Ins_doc_no;
                                        List<InventorySerialN> _invSerMasterList = CHNLSVC.Inventory.Get_INR_SER_DATA(
                                            new InventorySerialN() { Ins_ser_1 = _repPickSer.Tus_ser_1, Ins_itm_cd = _repPickSer.Tus_itm_cd, Ins_available = 1, Ins_com = _repPickSer.Tus_com, Ins_loc = _repPickSer.Tus_loc });
                                        if (_invSerMasterList!=null)
                                        {
                                            if (_invSerMasterList.Count>0)
                                            {
                                               // _repPickSer.Tus_doc_no = _invSerMasterList[0].Ins_seq_no.ToString();
                                            }
                                        }
                                        _repPickSerScanItm.Add(_repPickSer);
                                    }
                                    #endregion
                                }
                            }
                        }
                        #endregion
                    }
                    #region Final
                    BindSaveData();
                    AssAddBtnDesable();
                    _repPickSerItem = new List<ReptPickSerials>();
                    dgvItems.DataSource = new int[] { };
                    dgvItems.DataBind();
                    #endregion
                }
                else
                {
                    DispMsg("Please pick the serials !!!"); return;
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        private void AssAddBtnDesable()
        {
            foreach (GridViewRow row in dgvFinishGoodDetails.Rows)
            {
                Label lblTus_isSelect = row.FindControl("lblTus_isSelect") as Label;
                LinkButton lbtnSelFinishGood = row.FindControl("lbtnSelFinishGood") as LinkButton;
                if (lblTus_isSelect.Text=="Yes")
                {
                    lbtnSelFinishGood.Enabled = false;
                    lbtnSelFinishGood.CssClass = "buttoncolorleft";
                    lbtnSelFinishGood.OnClientClick = "return Enable();";
                }
            }
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            dgvItems.DataSource = new int[] { };
            _repPickSerItem = new List<ReptPickSerials>();
            dgvItems.DataBind();
        }

        protected void lbtnSeScanHdr_Click(object sender, EventArgs e)
        {
            try
            {
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                Label lblTus_itm_cd = row.FindControl("lblTus_itm_cd") as Label;
                Label lblTus_ser_1 = row.FindControl("lblTus_ser_1") as Label;
                dgvScanItm.DataSource = new int[] { };
                var v = _repPickSerScanItm.Where(c => c.MainItemSerialNo == lblTus_ser_1.Text).ToList();
                if (v != null)
                {
                    if (v.Count > 0)
                    {
                        dgvScanItm.DataSource = v;
                    }
                }
                dgvScanItm.DataBind();
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region Validate
                if (string.IsNullOrEmpty(txtLocation.Text))
                {
                    txtLocation.Focus(); DispMsg("Please select a source location !!!"); return;
                }
                if (string.IsNullOrEmpty(txtLocationStoreAt.Text))
                {
                    txtLocationStoreAt.Focus(); DispMsg("Please select a store at location !!!"); return;
                }
                DateTime _dtTmp = DateTime.MinValue;
                if (!DateTime.TryParse(txtDate.Text, out _dtTmp))
                {
                    txtLocationStoreAt.Focus(); DispMsg("Please select a valid date !!!"); return;
                }
                if (string.IsNullOrEmpty(txtAssJobNo.Text))
                {
                    //txtAssJobNo.Focus(); DispMsg("Please select a assembly job # !!!"); return;
                }
                if (string.IsNullOrEmpty(txtManualRef.Text))
                {
                    txtManualRef.Focus(); DispMsg("Please select a manual ref # !!!"); return;
                }
                if (_repPickSerScanHdr.Count < 1)
                {
                    DispMsg("Please add a scan finish good !!!"); return;
                }
                if (_repPickSerScanItm.Count < 1)
                {
                    DispMsg("Please add a scan assembly product !!!"); return;
                }
                #endregion
                Int32 res = 0;
                List<ReptPickSerials> repPickSerAodOut = new List<ReptPickSerials>();
                List<ReptPickSerials> repPickSerAodIn = new List<ReptPickSerials>();

                List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();

                List<ReptPickSerials> reptPickSerialsListIn = new List<ReptPickSerials>();
                List<ReptPickSerialsSub> reptPickSubSerialsListIn = new List<ReptPickSerialsSub>();

                string minesDocumntNo = "";
                string plusDocumntNo = "";
                string docAodIn = "";
                string docAodOut = "";
                string assmblyDocumntNo = "";
                string _error = "";
                Int32 result = -99;
                Int32 _userSeqNo = 0;
                int _direction = 1;
                DateTime mydate = DateTime.Now;
                reptPickSerialsList = _repPickSerScanItm;
                reptPickSerialsListIn = _repPickSerScanHdr;
                #region Validation
                if (reptPickSerialsList == null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No items found !!!')", true);
                    return;
                }

                string exist_grncom = "";
                string exist_grnno = "";
                string exist_sup = "";
                DateTime exist_grnDt = DateTime.Today;
                string orig_grncom = "";
                string orig_grnno = "";
                string orig_sup = "";
                DateTime orig_grnDt = DateTime.Today;

                foreach (var item in reptPickSerialsList)
                {
                    MasterItem _mstItm = CHNLSVC.General.GetItemMaster(item.Tus_itm_cd);
                    if (_mstItm.Mi_itm_tp == "M")
                    {
                        ReptPickSerials _serDet1 = CHNLSVC.Inventory.GetReservedByserialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), null, item.Tus_itm_cd, item.Tus_ser_id);
                        ReptPickSerials _serDet = CHNLSVC.Inventory.getSerialDet_INTSER(_serDet1.Tus_seq_no, _serDet1.Tus_itm_line, _serDet1.Tus_batch_line, _serDet1.Tus_ser_line);

                        exist_grncom = _serDet.Tus_exist_grncom;
                        exist_grnno = _serDet.Tus_exist_grnno;
                        exist_sup = _serDet.Tus_exist_supp;
                        exist_grnDt = _serDet.Tus_exist_grndt;
                        orig_grncom = _serDet.Tus_orig_grncom;
                        orig_grnno = _serDet.Tus_orig_grnno;
                        orig_sup = _serDet.Tus_orig_supp;
                        orig_grnDt = _serDet.Tus_orig_grndt;
                    }
                    List<InventorySerialN> _invSerMasterList = CHNLSVC.Inventory.Get_INR_SER_DATA(
                                           new InventorySerialN()
                                           {
                                               Ins_ser_1 = item.Tus_ser_1,
                                               Ins_itm_cd = item.Tus_itm_cd,
                                               Ins_available = 1,
                                               Ins_com = Session["UserCompanyCode"].ToString(),
                                               Ins_loc = Session["UserDefLoca"].ToString()
                                           });
                    bool _InrSerAvailable = false;
                    if (_invSerMasterList != null)
                    {
                        if (_invSerMasterList.Count > 0)
                        {
                            _InrSerAvailable = true;
                        }
                    }
                    if (!_InrSerAvailable)
                    {
                        DispMsg("Selected serial # is not available !!!"); return;
                    }
                }
                #endregion

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
                    string msg2 = "Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg2 + "')", true);
                    return;
                }
                #endregion

                foreach (var item in reptPickSerialsListIn)
                {
                    item.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();
                }

                InventoryHeader inHeader = new InventoryHeader();
                #region Fill InventoryHeader
                DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                foreach (DataRow r in dt_location.Rows)
                {
                    inHeader.Ith_sbu = (string)r["ML_OPE_CD"];
                    if (System.DBNull.Value != r["ML_CATE_2"])
                    {
                        inHeader.Ith_channel = (string)r["ML_CATE_2"];
                    }
                    else
                    {
                        inHeader.Ith_channel = string.Empty;
                    }
                }
                ////if (txtSupplierCd.Text.Trim() == "")
                ////{
                ////    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter supplier code !!!')", true);
                ////    return;
                ////}
             ////   inHeader.Ith_acc_no = "CONS_OUTS";
                inHeader.Ith_anal_1 = "";
                inHeader.Ith_anal_2 = "";
                inHeader.Ith_anal_3 = "";
                inHeader.Ith_anal_4 = "";
                inHeader.Ith_anal_5 = "";
                inHeader.Ith_anal_6 = 0;
                inHeader.Ith_anal_7 = 0;
                inHeader.Ith_anal_8 = DateTime.MinValue;
                inHeader.Ith_anal_9 = DateTime.MinValue;
                inHeader.Ith_anal_10 = false;
                inHeader.Ith_anal_11 = false;
                inHeader.Ith_anal_12 = false;
                inHeader.Ith_bus_entity = "";
                inHeader.Ith_cate_tp = "NOR";
                inHeader.Ith_com = Session["UserCompanyCode"].ToString();
                inHeader.Ith_com_docno = "";
                inHeader.Ith_cre_by = Session["UserID"].ToString();
                inHeader.Ith_cre_when = CHNLSVC.Security.GetServerDateTime().Date;//DateTime.Now;
                inHeader.Ith_del_add1 = "";
                inHeader.Ith_del_add2 = "";
                inHeader.Ith_del_code = "";
                inHeader.Ith_del_party = "";
                inHeader.Ith_del_town = "";
                inHeader.Ith_direct = false;
                inHeader.Ith_doc_date = mydate.Date;
                inHeader.Ith_doc_no = string.Empty;
                inHeader.Ith_doc_tp = "ADJ";
                inHeader.Ith_doc_year = mydate.Year;
                //inHeader.Ith_entry_no = _ISTEMP ? txtUserSeqNo.Text : txtOtherRef.Text.ToString().Trim();
                //inHeader.Ith_entry_tp = txtAdjSubType.Text.ToString().Trim();
                inHeader.Ith_git_close = true;
                inHeader.Ith_git_close_date = DateTime.MinValue;
                inHeader.Ith_git_close_doc = string.Empty;
                inHeader.Ith_isprinted = false;//????
                inHeader.Ith_is_manual = false;//????
                inHeader.Ith_job_no = txtAssJobNo.Text;
                inHeader.Ith_loading_point = string.Empty;
                inHeader.Ith_loading_user = string.Empty;
                inHeader.Ith_loc = Session["UserDefLoca"].ToString();
                inHeader.Ith_manual_ref = txtManualRef.Text.Trim();
                inHeader.Ith_mod_by = Session["UserID"].ToString();
                inHeader.Ith_mod_when = CHNLSVC.Security.GetServerDateTime().Date;//DateTime.Now;
                inHeader.Ith_noofcopies = 0;
                inHeader.Ith_oth_loc = txtLocationStoreAt.Text.ToUpper();
                inHeader.Ith_remarks = "";// txtRemarks.Text;
                inHeader.Ith_session_id =Session["SessionID"].ToString();
                inHeader.Ith_stus = "A";
                inHeader.Ith_sub_tp = "NOR";
                inHeader.Ith_vehi_no = string.Empty;
                inHeader.Ith_session_id = Session["SessionID"].ToString();
                inHeader.Ith_pc = Session["UserDefProf"].ToString();
                //if (Session["DocType"].ToString() == "TempDoc")
                //{
                //    inHeader.Ith_anal_10 = true;
                //    inHeader.Ith_anal_2 = txtDocumentNo.Text;
                //}
                //else
                //{
                inHeader.Ith_anal_10 = false;
                inHeader.Ith_anal_2 = "";
                //}

                ////inHeader.Ith_sub_docno = txtDocumentNo.Text.Trim();
                ////inHeader.Ith_oth_docno = txtDocumentNo.Text.Trim();
                #endregion

                 InventoryHeader inHeaderOut = new InventoryHeader();
                #region Fill InventoryHeader
                DataTable dt_locations = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                foreach (DataRow r in dt_locations.Rows)
                {
                    inHeaderOut.Ith_sbu = (string)r["ML_OPE_CD"];
                    if (System.DBNull.Value != r["ML_CATE_2"])
                    {
                        inHeaderOut.Ith_channel = (string)r["ML_CATE_2"];
                    }
                    else
                    {
                        inHeaderOut.Ith_channel = string.Empty;
                    }
                }
                ////if (txtSupplierCd.Text.Trim() == "")
                ////{
                ////    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter supplier code !!!')", true);
                ////    return;
                ////}
             ////   inHeader.Ith_acc_no = "CONS_OUTS";
                inHeaderOut.Ith_anal_1 = "";
                inHeaderOut.Ith_anal_2 = "";
                inHeaderOut.Ith_anal_3 = "";
                inHeaderOut.Ith_anal_4 = "";
                inHeaderOut.Ith_anal_5 = "";
                inHeaderOut.Ith_anal_6 = 0;
                inHeaderOut.Ith_anal_7 = 0;
                inHeaderOut.Ith_anal_8 = DateTime.MinValue;
                inHeaderOut.Ith_anal_9 = DateTime.MinValue;
                inHeaderOut.Ith_anal_10 = false;
                inHeaderOut.Ith_anal_11 = false;
                inHeaderOut.Ith_anal_12 = false;
                inHeaderOut.Ith_bus_entity = "";
                inHeaderOut.Ith_cate_tp = "NOR";
                inHeaderOut.Ith_com = Session["UserCompanyCode"].ToString();
                inHeaderOut.Ith_com_docno = "";
                inHeaderOut.Ith_cre_by = Session["UserID"].ToString();
                inHeaderOut.Ith_cre_when = CHNLSVC.Security.GetServerDateTime().Date;//DateTime.Now;
                inHeaderOut.Ith_del_add1 = "";
                inHeaderOut.Ith_del_add2 = "";
                inHeaderOut.Ith_del_code = "";
                inHeaderOut.Ith_del_party = "";
                inHeaderOut.Ith_del_town = "";
                inHeaderOut.Ith_direct = true;
                inHeaderOut.Ith_doc_date = mydate.Date;
                inHeaderOut.Ith_doc_no = string.Empty;
                inHeaderOut.Ith_doc_tp = "ADJ";
                inHeaderOut.Ith_doc_year = mydate.Year;
                //inHeader.Ith_entry_no = _ISTEMP ? txtUserSeqNo.Text : txtOtherRef.Text.ToString().Trim();
                //inHeader.Ith_entry_tp = txtAdjSubType.Text.ToString().Trim();
                inHeaderOut.Ith_git_close = true;
                inHeaderOut.Ith_git_close_date = DateTime.MinValue;
                inHeaderOut.Ith_git_close_doc = string.Empty;
                inHeaderOut.Ith_isprinted = false;//????
                inHeaderOut.Ith_is_manual = false;//????
                inHeaderOut.Ith_job_no = txtAssJobNo.Text.ToUpper().Trim();
                inHeaderOut.Ith_loading_point = string.Empty;
                inHeaderOut.Ith_loading_user = string.Empty;
                inHeaderOut.Ith_loc = txtLocation.Text.ToUpper().Trim();
                inHeaderOut.Ith_manual_ref = txtManualRef.Text.Trim();
                inHeaderOut.Ith_mod_by = Session["UserID"].ToString();
                inHeaderOut.Ith_mod_when = CHNLSVC.Security.GetServerDateTime().Date;//DateTime.Now;
                inHeaderOut.Ith_noofcopies = 0;
                inHeaderOut.Ith_oth_loc = Session["UserDefLoca"].ToString();
                inHeaderOut.Ith_remarks = "";// txtRemarks.Text;
                inHeaderOut.Ith_session_id =Session["SessionID"].ToString();
                inHeaderOut.Ith_stus = "A";
                inHeaderOut.Ith_sub_tp = "NOR";
                inHeaderOut.Ith_vehi_no = string.Empty;
                inHeaderOut.Ith_session_id = Session["SessionID"].ToString();
                //if (Session["DocType"].ToString() == "TempDoc")
                //{
                //    inHeaderOut.Ith_anal_10 = true;
                //    inHeaderOut.Ith_anal_2 = txtDocumentNo.Text;
                //}
                //else
                //{
                inHeaderOut.Ith_anal_10 = false;
                inHeaderOut.Ith_anal_2 = "";
                //}

                ////inHeaderOut.Ith_sub_docno = txtDocumentNo.Text.Trim();
                ////inHeaderOut.Ith_oth_docno = txtDocumentNo.Text.Trim();
                inHeaderOut.Ith_pc = Session["UserDefProf"].ToString();
                #endregion

                MasterAutoNumber _autonoAsbl = new MasterAutoNumber();
                #region Fill MasterAutoNumber
                _autonoAsbl.Aut_cate_cd = Session["UserDefLoca"].ToString();
                _autonoAsbl.Aut_cate_tp = "LOC";
                _autonoAsbl.Aut_direction = null;
                _autonoAsbl.Aut_modify_dt = null;
                _autonoAsbl.Aut_moduleid = "ASBL";
                _autonoAsbl.Aut_number = 0;
                _autonoAsbl.Aut_start_char = "ASBL";
                _autonoAsbl.Aut_year = null;
                #endregion

                MasterAutoNumber _autonoMinus = new MasterAutoNumber();
                #region Fill MasterAutoNumber
                _autonoMinus.Aut_cate_cd = Session["UserDefLoca"].ToString();
                _autonoMinus.Aut_cate_tp = "LOC";
                _autonoMinus.Aut_direction = null;
                _autonoMinus.Aut_modify_dt = null;
                _autonoMinus.Aut_moduleid = "ADJ";
                _autonoMinus.Aut_number = 5;
                _autonoMinus.Aut_start_char = "ADJ";
                _autonoMinus.Aut_year = null;
                #endregion

                MasterAutoNumber _autonoPlus = new MasterAutoNumber();
                #region Fill MasterAutoNumber
                _autonoPlus.Aut_cate_cd = Session["UserDefLoca"].ToString();
                _autonoPlus.Aut_cate_tp = "LOC";
                _autonoPlus.Aut_direction = null;
                _autonoPlus.Aut_modify_dt = null;
                _autonoPlus.Aut_moduleid = "ADJ";
                _autonoPlus.Aut_number = 5;
                _autonoPlus.Aut_start_char = "ADJ";
                _autonoPlus.Aut_year = null;
                #endregion

                #region Update reptPickSerialsListIn
                if (reptPickSerialsListIn != null)
                {
                    foreach (var _seritem in reptPickSerialsListIn)
                    {
                        _seritem.Tus_exist_grncom =  exist_grncom;
                        _seritem.Tus_exist_grnno = exist_grnno;
                        _seritem.Tus_exist_supp = exist_sup;
                        _seritem.Tus_exist_grndt = exist_grnDt;
                        _seritem.Tus_orig_grncom = orig_grncom;
                        _seritem.Tus_orig_grnno = orig_grnno;
                        _seritem.Tus_orig_supp = orig_sup;
                        _seritem.Tus_orig_grndt = orig_grnDt;
                    }
                }

                #endregion

                MasterAutoNumber _autAodIn = new MasterAutoNumber();
                #region Fill MasterAutoNumber AOD +
                _autAodIn.Aut_cate_cd = Session["UserDefLoca"].ToString();
                _autAodIn.Aut_cate_tp = "LOC";
                _autAodIn.Aut_direction = 1;
                _autAodIn.Aut_modify_dt = null;
                _autAodIn.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
                _autAodIn.Aut_moduleid = "AOD";
                _autAodIn.Aut_start_char = "AOD";
                #endregion

                #region Fill Aod In InvHdr
                InventoryHeader invHdrAodIn = new InventoryHeader();
                invHdrAodIn.Ith_loc = txtLocationStoreAt.Text.ToUpper().Trim();
                invHdrAodIn.Ith_com = Session["UserCompanyCode"].ToString();
                //invHdrAodIn.Ith_oth_docno = pendocno;
                //invHdrAodIn.Ith_entry_no = pendocno;
                    invHdrAodIn.Ith_doc_date = Convert.ToDateTime(txtDate.Text).Date;
                invHdrAodIn.Ith_doc_year = Convert.ToDateTime(txtDate.Text).Year;
                invHdrAodIn.Ith_doc_tp = "AOD";
                invHdrAodIn.Ith_cate_tp = "NOR";
                invHdrAodIn.Ith_sub_tp = "NOR";
                string _supplierclaimcode = string.Empty;
             
                    invHdrAodIn.Ith_anal_10 = false;
                    invHdrAodIn.Ith_anal_2 = "";
                

                invHdrAodIn.Ith_is_manual = false;
                invHdrAodIn.Ith_stus = "A";
                invHdrAodIn.Ith_cre_by = Session["UserID"].ToString();
                invHdrAodIn.Ith_mod_by = Session["UserID"].ToString();
                invHdrAodIn.Ith_direct = true;
                invHdrAodIn.Ith_session_id = Session["SessionID"].ToString();
                invHdrAodIn.Ith_manual_ref = txtManualRef.Text.Trim();
                //invHdrAodIn.Ith_remarks = txtRemarks.Text;
                //invHdrAodIn.Ith_vehi_no = txtVehicle.Text;
                //invHdrAodIn.Ith_bus_entity = OutwardType == "DO" ? _pohdr.Poh_supp : string.Empty;
                invHdrAodIn.Ith_oth_com = Session["UserCompanyCode"].ToString();
                invHdrAodIn.Ith_oth_loc = txtLocationStoreAt.Text.ToUpper();
                invHdrAodIn.Ith_pc = Session["UserDefProf"].ToString();
                #endregion


                MasterAutoNumber _autAodOut = new MasterAutoNumber();
                #region Fill MasterAutoNumber AOD PLUS
                _autAodOut.Aut_cate_cd = Session["UserDefLoca"].ToString();
                _autAodOut.Aut_cate_tp = "LOC";
                _autAodOut.Aut_direction = null;
                _autAodOut.Aut_modify_dt = null;
                _autAodOut.Aut_moduleid = "AOD";
                _autAodOut.Aut_start_char = "AOD";
                _autAodOut.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
                #endregion

                #region Inventory Header aod out Value Assign
                InventoryHeader _invHdrAodOut = new InventoryHeader();
                _invHdrAodOut.Ith_acc_no = string.Empty;
                _invHdrAodOut.Ith_anal_1 = string.Empty;
                _invHdrAodOut.Ith_anal_10 = true;//Direct AOD
                _invHdrAodOut.Ith_anal_11 = false;
                _invHdrAodOut.Ith_anal_12 = false;
                _invHdrAodOut.Ith_anal_2 = string.Empty;
                _invHdrAodOut.Ith_anal_3 = string.Empty;
                _invHdrAodOut.Ith_anal_4 = string.Empty;
                _invHdrAodOut.Ith_anal_5 = string.Empty;
                _invHdrAodOut.Ith_anal_6 = 0;
                _invHdrAodOut.Ith_anal_7 = 0;
                _invHdrAodOut.Ith_anal_8 = Convert.ToDateTime(txtDate.Text).Date;
                _invHdrAodOut.Ith_anal_9 = Convert.ToDateTime(txtDate.Text).Date;
                _invHdrAodOut.Ith_bus_entity = string.Empty;
                _invHdrAodOut.Ith_cate_tp = "NOR";
                _invHdrAodOut.Ith_channel = string.Empty;
                _invHdrAodOut.Ith_com = Session["UserCompanyCode"].ToString();
                _invHdrAodOut.Ith_com_docno = string.Empty;
                _invHdrAodOut.Ith_cre_by = Session["UserID"].ToString();
                _invHdrAodOut.Ith_cre_when = DateTime.Now.Date;
                _invHdrAodOut.Ith_del_add1 = string.Empty;
                _invHdrAodOut.Ith_del_add2 = string.Empty;
                _invHdrAodOut.Ith_del_code = string.Empty;
                _invHdrAodOut.Ith_del_party = string.Empty;
                _invHdrAodOut.Ith_del_town = string.Empty;
                _invHdrAodOut.Ith_direct = false;
                _invHdrAodOut.Ith_doc_date = Convert.ToDateTime(txtDate.Text);
                _invHdrAodOut.Ith_doc_no = string.Empty;
                _invHdrAodOut.Ith_doc_tp = "AOD";
                _invHdrAodOut.Ith_doc_year = Convert.ToDateTime(txtDate.Text).Date.Year;
                //_invHdrAodOut.Ith_entry_no = _requestno;
                _invHdrAodOut.Ith_entry_tp = string.Empty;
                _invHdrAodOut.Ith_git_close = false;
                _invHdrAodOut.Ith_git_close_date = Convert.ToDateTime(txtDate.Text).Date;
                _invHdrAodOut.Ith_git_close_doc = string.Empty;
                _invHdrAodOut.Ith_is_manual = true;
                _invHdrAodOut.Ith_isprinted = false;
                _invHdrAodOut.Ith_job_no = string.Empty;
                _invHdrAodOut.Ith_loading_point = string.Empty;
                _invHdrAodOut.Ith_loading_user = string.Empty;
                _invHdrAodOut.Ith_loc = txtLocation.Text.ToUpper().Trim();
                _invHdrAodOut.Ith_manual_ref = txtManualRef.Text;
                _invHdrAodOut.Ith_mod_by = Session["UserID"].ToString();
                _invHdrAodOut.Ith_mod_when = DateTime.Now.Date;
                _invHdrAodOut.Ith_noofcopies = 0;
                //_invHdrAodOut.Ith_oth_loc = txtDispatchRequried.Text.Trim();
                //_invHdrAodOut.Ith_oth_docno = chkDirectOut.Checked ? string.Empty : _requestno;
                _invHdrAodOut.Ith_oth_docno = string.Empty ;
                //_invHdrAodOut.Ith_remarks = txtRemarks.Text;
                _invHdrAodOut.Ith_sbu = string.Empty;
                //_invHdrAodOut.Ith_seq_no = 0; removed by Chamal 12-05-2013
                _invHdrAodOut.Ith_session_id = Session["SessionID"].ToString();
                _invHdrAodOut.Ith_stus = "A";
                _invHdrAodOut.Ith_sub_tp = string.Empty;
                _invHdrAodOut.Ith_vehi_no = string.Empty;
                _invHdrAodOut.Ith_oth_com = Session["UserCompanyCode"].ToString();
                //_invHdrAodOut.Ith_anal_1 =  "0";
                //_invHdrAodOut.Ith_anal_2 = chkManualRef.Checked ? ddlManType.Text : string.Empty;
                _invHdrAodOut.Ith_anal_2 = string.Empty;
                //_invHdrAodOut.Ith_sub_tp = ddlType.SelectedValue.ToString();
                _invHdrAodOut.Ith_sub_tp = "NOR";
                _invHdrAodOut.Ith_session_id = Session["SessionID"].ToString();
                _invHdrAodOut.Ith_pc = Session["UserDefProf"].ToString();
                //_invHdrAodOut.Ith_vehi_no = txtVehicle.Text;//add rukshan 06/jan/2016
                //_invHdrAodOut.Ith_anal_3 = ddlDeliver.SelectedItem.Text;//add rukshan 06/jan/2016
                //if (_ServiceJobBase == true)
                //{
                //    //_invHdrAodOut.Ith_isjobbase = true;
                //    _invHdrAodOut.Ith_job_no = JobNo;
                //    _invHdrAodOut.Ith_cate_tp = "SERVICE";
                //    _invHdrAodOut.Ith_sub_tp = "NOR";
                //    _invHdrAodOut.Ith_sub_docno = JobNo;
                //}


                #endregion

                bool aod=false;
                if (txtLocation.Text.ToUpper().Trim()!=txtLocationStoreAt.Text.ToUpper().Trim())
                {
                    aod = true;
                }
                //repPickSerAodOut = reptPickSerialsListIn;
                foreach (var item in reptPickSerialsListIn)
                {
                    ReptPickSerials _sers = MakeObject(item);
                    _sers.Tus_loc = txtLocation.Text.Trim().ToUpper();
                    MasterBinLocation _bin = CHNLSVC.General.GetMasterBinLocation(new MasterBinLocation() { Ibl_com_cd = txtCompany.Text.ToUpper(), Ibl_loc_cd = txtLocation.Text.ToUpper() });
                    if (_bin != null)
                    {
                        _sers.Tus_bin = _bin.Ibl_bin_cd;
                    }
                    repPickSerAodOut.Add(_sers);
                }
                //repPickSerAodIn = reptPickSerialsListIn;
                foreach (var item in reptPickSerialsListIn)
                {
                    ReptPickSerials _ser = MakeObject(item);
                    _ser.Tus_loc = txtLocationStoreAt.Text.Trim().ToUpper();
                    MasterBinLocation _bin = CHNLSVC.General.GetMasterBinLocation(new MasterBinLocation() { Ibl_com_cd = txtCompany.Text.ToUpper(), Ibl_loc_cd = txtLocationStoreAt.Text.ToUpper() });
                    if (_bin != null)
                    {
                        _ser.Tus_bin = _bin.Ibl_bin_cd;
                    }
                    repPickSerAodIn.Add(_ser);
                }

                //Add unit price
                foreach (var item in reptPickSerialsListIn)
                {
                    item.Tus_unit_cost = reptPickSerialsList.Where(c => c.MainItemSerialNo == item.Tus_ser_1).ToList().Select(x => x.Tus_unit_cost).Sum();
                    item.Tus_unit_price = reptPickSerialsList.Where(c => c.MainItemSerialNo == item.Tus_ser_1).ToList().Select(x => x.Tus_unit_price).Sum();
                }


                if (reptPickSerialsList != null)
                {
                    foreach (var _seritem in reptPickSerialsList)
                    {
                        _seritem.Tus_exist_grncom = exist_grncom;
                        _seritem.Tus_exist_grnno = exist_grnno;
                        _seritem.Tus_exist_supp = exist_sup;
                        _seritem.Tus_exist_grndt = exist_grnDt;
                        _seritem.Tus_orig_grncom = orig_grncom;
                        _seritem.Tus_orig_grnno = orig_grnno;
                        _seritem.Tus_orig_supp = orig_sup;
                        _seritem.Tus_orig_grndt = orig_grnDt;
                    }
                }
              
                //Save ADJ -+
                result = CHNLSVC.Inventory.ProductAssemblySave(_autonoAsbl,out assmblyDocumntNo, 
                    inHeader, reptPickSerialsList, reptPickSubSerialsList, _autonoMinus, out minesDocumntNo, 
                    inHeaderOut, reptPickSerialsListIn, reptPickSubSerialsList, _autonoPlus, out plusDocumntNo,
                    invHdrAodIn,repPickSerAodIn,out docAodIn,_autAodIn,
                    _invHdrAodOut,repPickSerAodOut, out docAodOut, _autAodOut, out _error, aod);

                if (result != -99 && result >= 0)
                {
                    DispMsg("Successfully Saved  Document No : " + " " + assmblyDocumntNo + " " + docAodIn + " " + docAodOut, "S");
                   ClearData();
                }
                else
                {
                    DispMsg(_error+" !!!", "E");
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        private ReptPickSerials MakeObject(ReptPickSerials _obj)
        {
            ReptPickSerials _tmpObj = new ReptPickSerials();
            _tmpObj.Tus_itm_cd = _obj.Tus_itm_cd;
            _tmpObj.Tus_ser_1 = _obj.Tus_ser_1;
            _tmpObj.MainItemSerialNo = _obj.MainItemSerialNo;
            _tmpObj.Tus_itm_desc = _obj.Tus_itm_desc;
            _tmpObj.Tus_itm_model = _obj.Tus_itm_model;
            _tmpObj.Tus_itm_stus = _obj.Tus_itm_stus;
            _tmpObj.Tus_qty = _obj.Tus_qty;
            _tmpObj.Tus_ser_id = _obj.Tus_ser_id;
            _tmpObj.Tus_ser_line = _obj.Tus_ser_line;
            _tmpObj.Tus_seq_no = _obj.Tus_seq_no;
            _tmpObj.Tus_itm_line = _obj.Tus_itm_line;
            _tmpObj.Tus_unit_cost = _obj.Tus_unit_cost;
            _tmpObj.Tus_unit_price = _obj.Tus_unit_price;
            _tmpObj.Tus_bin = _obj.Tus_bin;
            _tmpObj.Tus_loc = _obj.Tus_loc;
            _tmpObj.Tus_com = _obj.Tus_com;
            _tmpObj.Tus_warr_no = _obj.Tus_warr_no;
            _tmpObj.Tus_warr_period = _obj.Tus_warr_period;
            _tmpObj.Tus_cre_by = _obj.Tus_cre_by;
            _tmpObj.Tus_cre_dt = _obj.Tus_cre_dt;
            return _tmpObj;
        }
    }
}