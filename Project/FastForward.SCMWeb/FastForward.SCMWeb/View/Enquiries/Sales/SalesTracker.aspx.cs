using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Enquiries.Sales
{
    public partial class SalesTracker : BasePage
    {
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
        string _Para="";

        string SerPopShow
        {
            get { if (Session["SerPopShow"] != null) { return (string)Session["SerPopShow"]; } else { return ""; } }
            set { Session["SerPopShow"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserDefLoca"] == null || Session["UserDefProf"] == null || Session["UserID"] == null || Session["UserCompanyCode"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                Clear();
            }
            else
            {
                if (SerPopShow == "Show")
                {
                    PopupSearch.Show();
                }
                else
                {
                    PopupSearch.Hide();
                }
            }
        }
        private void Clear()
        {
            SerPopShow = "Hide";
            PopupSearch.Hide();
            txtFrDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            txtToDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            txtPc.Text = Session["UserDefProf"].ToString();
            txtExe.Text = "";
            txtCust.Text = "";
            txtCat1.Text = "";
            txtInvNo.Text = "";
            txtDelNo.Text = "";
            txtSer.Text = "";
            txtItm.Text = "";

            dgvInv.DataSource = new int[] { };
            dgvInv.DataBind();
            dgvInvDet.DataSource = new int[] { };
            dgvInvDet.DataBind();
            dgvDel.DataSource = new int[] { };
            dgvDel.DataBind();
            dgvSerial.DataSource = new int[] { };
            dgvSerial.DataBind();
        }
        private void DispMsg(string msgText, string msgType = "")
        {
            //msgText = msgText.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");

            if (msgType == "N")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + msgText + "');", true);
            }
            else if (msgType == "W" || msgType == "")
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
        public void BindCmbSearchbykey(DataTable _dataSource)
        {
            this.cmbSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.cmbSearchbykey.Items.Add(col.ColumnName);
            }
            this.cmbSearchbykey.SelectedIndex = 0;
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            SerPopShow = "Hide";
            PopupSearch.Hide();
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator+Session["UserID"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceExecutive:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "" + "" + seperator + "" + seperator+"");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItems:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceWithDate:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "DO" + seperator + "0" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes:
                    {
                        paramsText.Append("SER1_WOITEM" + seperator + Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item_Serials:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "0" + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
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
                SerPopShow = "Show";
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }
        protected void dgvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string code = dgvResult.SelectedRow.Cells[1].Text;
                if (_serType == "AllProfitCenters")
                {
                    txtPc.Text = code;
                    txtPc_TextChanged(null, null);
                }
                else if (_serType == "Customer")
                {
                    txtCust.Text = code;
                    txtCust_TextChanged(null, null);
                }
                else if (_serType == "Item")
                {
                    txtItm.Text = code;
                    txtItm_TextChanged(null, null);
                }
                else if (_serType == "CAT_Main")
                {
                    txtCat1.Text = code;
                    txtCat1_TextChanged(null, null);
                }
                else if (_serType == "InvoiceExecutive")
                {
                    txtExe.Text = code;
                    txtExe_TextChanged(null, null);
                }
                else if (_serType == "InvoiceWithDate")
                {
                    txtInvNo.Text = code;
                    txtInvNo_TextChanged(null, null);
                }
                else if (_serType == "MovementDocDateSearch")
                {
                    txtDelNo.Text = code;
                    txtDelNo_TextChanged(null, null);
                }
                else if (_serType == "Item_Serials")
                {
                    txtSer.Text = code;
                    txtSer_TextChanged(null, null);
                }
                SerPopShow = "Hide";
                PopupSearch.Hide();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                if (_serType == "AllProfitCenters")
                {
                    _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                    _serData = CHNLSVC.CommonSearch.SearchProfitCenterByUser(_Para, cmbSearchbykey.SelectedValue, txtSearchbyword.Text);
                }
                else if (_serType == "Customer")
                {
                    _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    _serData = CHNLSVC.CommonSearch.GetSupplierCommonNew(_Para, cmbSearchbykey.SelectedValue, txtSearchbyword.Text);
                }
                else if (_serType == "Item")
                {
                    _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    _serData = CHNLSVC.CommonSearch.GetItemSearchDataMaster(_Para, cmbSearchbykey.SelectedValue, txtSearchbyword.Text);
                }
                else if (_serType == "CAT_Main")
                {
                    _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                    _serData = CHNLSVC.CommonSearch.GetCat_SearchData(_Para, cmbSearchbykey.SelectedValue,  txtSearchbyword.Text);
                }
                else if (_serType == "InvoiceExecutive")
                {
                    _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceExecutive);
                    _serData = CHNLSVC.CommonSearch.SearchEmployeeAssignToProfitCenter(_Para, cmbSearchbykey.SelectedValue, txtSearchbyword.Text);
                }
                else if (_serType == "InvoiceWithDate")
                {
                    _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                    _serData = CHNLSVC.CommonSearch.SearchInvoice(_Para, cmbSearchbykey.SelectedValue,  txtSearchbyword.Text, DateTime.Today.AddYears(-1), DateTime.Today.AddDays(1));
                }
                else if (_serType == "MovementDocDateSearch")
                {
                    _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                    _serData = CHNLSVC.CommonSearch.Search_int_hdr_Infor(_Para, cmbSearchbykey.SelectedValue, txtSearchbyword.Text, DateTime.Today.AddYears(-1), DateTime.Today.AddDays(1));
                }
                else if (_serType == "Item_Serials")
                {
                    _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                    _serData = CHNLSVC.Inventory.SearchSerialsInr(_Para, cmbSearchbykey.SelectedValue, txtSearchbyword.Text);
                }
                dgvResult.DataSource = new int[] { };
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                }
                //txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                dgvResult.DataBind();
                PopupSearch.Show();
                SerPopShow = "Show";
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }
        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void lbtnSePC_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                dgvResult.DataSource = new int[] { };
               _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
               _serData = CHNLSVC.CommonSearch.SearchProfitCenterByUser(_Para, null, null);
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                    BindCmbSearchbykey(_serData);
                }
                dgvResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                _serType = "AllProfitCenters";
                PopupSearch.Show();
                SerPopShow = "Show";
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtSeCus_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                dgvResult.DataSource = new int[] { };
                _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                _serData = CHNLSVC.CommonSearch.GetSupplierCommonNew(_Para, null, null);
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                    BindCmbSearchbykey(_serData);
                }
                dgvResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                _serType = "Customer";
                PopupSearch.Show();
                SerPopShow = "Show";
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnSeItm_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                dgvResult.DataSource = new int[] { };
                _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                _serData = CHNLSVC.CommonSearch.GetItemSearchDataMaster(_Para, null, null);
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
                SerPopShow = "Show";
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnSeCat1_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                dgvResult.DataSource = new int[] { };
                _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                _serData = CHNLSVC.CommonSearch.GetCat_SearchData(_Para, null, null);
                if (_serData.Rows.Count > 0)
                {
                    DataView dv = _serData.DefaultView;
                    dv.Sort = "Code asc";
                    _serData = dv.ToTable();
                    dgvResult.DataSource = _serData;
                    BindCmbSearchbykey(_serData);
                }
                dgvResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                _serType = "CAT_Main";
                PopupSearch.Show();
                SerPopShow = "Show";
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnSeExee_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                dgvResult.DataSource = new int[] { };
                _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceExecutive);
                _serData = CHNLSVC.CommonSearch.SearchEmployeeAssignToProfitCenter(_Para, null, null);
                if (_serData.Rows.Count > 0)
                {
                    DataView dv = _serData.DefaultView;
                    dv.Sort = "Code asc";
                    _serData = dv.ToTable();
                    dgvResult.DataSource = _serData;
                    BindCmbSearchbykey(_serData);
                }
                dgvResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                _serType = "InvoiceExecutive";
                PopupSearch.Show();
                SerPopShow = "Show";
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnSeInvNo_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                dgvResult.DataSource = new int[] { };
                _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                _serData = CHNLSVC.CommonSearch.SearchInvoice(_Para, null, null, DateTime.Today.AddMonths(-1), DateTime.Today.AddDays(1));
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                    BindCmbSearchbykey(_serData);
                }
                dgvResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                _serType = "InvoiceWithDate";
                PopupSearch.Show();
                SerPopShow = "Show";
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnSeDelNo_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                dgvResult.DataSource = new int[] { };
                _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                _serData = CHNLSVC.CommonSearch.Search_int_hdr_Infor(_Para, null, null, DateTime.Today.AddMonths(-1), DateTime.Today.AddDays(1));
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Document");
                    BindCmbSearchbykey(dt);
                }
                dgvResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                _serType = "MovementDocDateSearch";
                PopupSearch.Show();
                SerPopShow = "Show";
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnSeSer_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                dgvResult.DataSource = new int[] { };
                _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                _serData = CHNLSVC.Inventory.SearchSerialsInr(_Para, null, null);
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                    BindCmbSearchbykey(_serData);
                }
                dgvResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                _serType = "Item_Serials";
                PopupSearch.Show();
                SerPopShow = "Show";
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtPc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPc.Text))
                {
                    bool b2 = false;
                    string toolTip = "";
                    
                    _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                    DataTable _result = CHNLSVC.CommonSearch.SearchProfitCenterByUser(_Para, "CODE", txtPc.Text.Trim().ToUpper());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtPc.Text.ToUpper() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtPc.ToolTip = toolTip;
                    }
                    else
                    {
                        txtPc.ToolTip = "";
                        txtPc.Text = "";
                        txtPc.Focus();
                        DispMsg("Please select a valid profit center !!!");
                        return;
                    }
                }
                else
                {
                    txtPc.ToolTip = "";
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtCust_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCust.Text))
                {
                    bool b2 = false;
                    string toolTip = "";

                    _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    DataTable _result = CHNLSVC.CommonSearch.GetSupplierCommonNew(_Para, "CODE", txtCust.Text.Trim().ToUpper());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtCust.Text.ToUpper() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtCust.ToolTip = toolTip;
                    }
                    else
                    {
                        txtCust.ToolTip = "";
                        txtCust.Text = "";
                        txtCust.Focus();
                        DispMsg("Please select a valid customer !!!");
                        return;
                    }
                }
                else
                {
                    txtCust.ToolTip = "";
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtItm_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtItm.Text))
                {
                    bool b2 = false;
                    string toolTip = "";

                    _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(_Para, "ITEM", txtItm.Text.Trim().ToUpper());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["ITEM"].ToString()))
                        {
                            if (txtItm.Text.ToUpper() == row["ITEM"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtItm.ToolTip = toolTip;
                    }
                    else
                    {
                        txtItm.ToolTip = "";
                        txtItm.Text = "";
                        txtItm.Focus();
                        DispMsg("Please select a valid item !!!");
                        return;
                    }
                }
                else
                {
                    txtItm.ToolTip = "";
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtCat1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCat1.Text))
                {
                    bool b2 = false;
                    string toolTip = "";

                    _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_Para, "CODE", txtCat1.Text.Trim().ToUpper());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["CODE"].ToString()))
                        {
                            if (txtCat1.Text.ToUpper() == row["CODE"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtCat1.ToolTip = toolTip;
                    }
                    else
                    {
                        txtCat1.ToolTip = "";
                        txtCat1.Text = "";
                        txtCat1.Focus();
                        DispMsg("Please select a valid category 1 !!!");
                        return;
                    }
                }
                else
                {
                    txtCat1.ToolTip = "";
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }
        protected void txtExe_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtExe.Text))
                {
                    bool b2 = false;
                    string toolTip = "";

                    _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceExecutive);
                    DataTable _result = CHNLSVC.CommonSearch.SearchEmployeeAssignToProfitCenter(_Para, "EPF", txtExe.Text.Trim().ToUpper());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["EPF"].ToString()))
                        {
                            if (txtExe.Text.ToUpper() == row["EPF"].ToString())
                            {
                                b2 = true;
                                toolTip = row["First Name"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtExe.ToolTip = toolTip;
                    }
                    else
                    {
                        txtExe.ToolTip = "";
                        txtExe.Text = "";
                        txtExe.Focus();
                        DispMsg("Please select a valid executive !!!");
                        return;
                    }
                }
                else
                {
                    txtExe.ToolTip = "";
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtInvNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtInvNo.Text))
                {
                    bool b2 = false;
                    string toolTip = "";
                    InvoiceHeader _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvNo.Text.Trim());
                    if (_hdr!=null)
                    {
                        b2 = true;
                    }
                    if (b2)
                    {
                        txtInvNo.ToolTip = toolTip; lbtnSer2.Focus(); lbtnSer2_Click(null, null);
                    }
                    else
                    {
                        txtInvNo.ToolTip = "";
                        txtInvNo.Text = "";
                        txtInvNo.Focus();
                        DispMsg("Please select a valid invoice # !!!");
                        return;
                    }
                }
                else
                {
                    txtInvNo.ToolTip = "";
                    lbtnSer2.Focus();
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtDelNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDelNo.Text))
                {
                    bool b2 = false;
                    string toolTip = "";
                    InventoryHeader _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(txtDelNo.Text);
                    if (_invHdr != null)
                    {
                        b2 = true;
                    }
                    if (b2)
                    {
                        txtDelNo.ToolTip = toolTip; lbtnSer2_Click(null, null);
                    }
                    else
                    {
                        txtDelNo.ToolTip = "";
                        txtDelNo.Text = "";
                        txtDelNo.Focus();
                        DispMsg("Please select a valid do # !!!");
                        return;
                    }
                }
                else
                {
                    txtDelNo.ToolTip = ""; lbtnSer2.Focus();
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtSer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSer.Text))
                {
                    bool b2 = false;
                    string toolTip = "";

                    _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                    DataTable _result = CHNLSVC.Inventory.SearchSerialsInr(_Para, "Serial #", txtSer.Text.Trim());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Serial #"].ToString()))
                        {
                            if (txtSer.Text == row["Serial #"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Item Code"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtSer.ToolTip = toolTip; lbtnSer2.Focus(); lbtnSer2_Click(null, null);
                    }
                    else
                    {
                        txtSer.ToolTip = "";
                        txtSer.Text = "";
                        txtSer.Focus();
                        DispMsg("Please select a valid serial # !!!");
                        return;
                    }
                }
                else
                {
                    txtSer.ToolTip = "";
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnSer1_Click(object sender, EventArgs e)
        {
            try
            {
                dgvInv.DataSource = new int[] { };
                dgvInvDet.DataSource = new int[] { };
                dgvDel.DataSource = new int[] { };
                dgvSerial.DataSource = new int[] { };

                dgvInv.DataBind();
                dgvInvDet.DataBind();
                dgvDel.DataBind();
                dgvSerial.DataBind();

                #region Validation
                DateTime dtFrom = new DateTime(), dtTo = new DateTime();
                if (!DateTime.TryParse(txtFrDate.Text, out dtFrom))
                {
                    DispMsg("Please select valid from date !!!"); return;
                }
                if (!DateTime.TryParse(txtToDate.Text, out dtTo))
                {
                    DispMsg("Please select valid to date !!!"); return;
                }
                #endregion

                #region Set Parameter
                InvoiceHeader invhdr = new InvoiceHeader();
                InvoiceItem invItm = new InvoiceItem();
                invhdr.Sah_dt = dtTo;
                invhdr.Sah_anal_12 = dtFrom;
                invhdr.Sah_com = Session["UserCompanyCode"].ToString(); ;
                invhdr.Sah_pc = string.IsNullOrEmpty(txtPc.Text) ? null : txtPc.Text.ToUpper().Trim();
                invItm.Sad_itm_cd = string.IsNullOrEmpty(txtItm.Text) ? null : txtItm.Text.ToUpper().Trim();
                invItm.Sad_itm_tp = string.IsNullOrEmpty(txtCat1.Text) ? null : txtCat1.Text.ToUpper().Trim();
                invhdr.Sah_sales_ex_cd = string.IsNullOrEmpty(txtExe.Text) ? null : txtExe.Text.ToUpper().Trim();
                invhdr.Sah_cus_cd = string.IsNullOrEmpty(txtCust.Text) ? null : txtCust.Text.ToUpper().Trim();
                #endregion
               
                DataTable _dtInvoice = new DataTable();
                if (string.IsNullOrEmpty(txtPc.Text))
                {
                    _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                    DataTable _result = CHNLSVC.CommonSearch.SearchProfitCenterByUser(_Para, null, null);
                    foreach (DataRow rw  in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(rw["CODE"].ToString()))
                        {
                            invhdr.Sah_pc = rw["CODE"].ToString();
                            DataTable _dt = CHNLSVC.Sales.GetSalesTrackerInvoices(invhdr, invItm);
                            _dtInvoice.Merge(_dt);
                        }
                    }
                }
                else
                {
                    _dtInvoice = CHNLSVC.Sales.GetSalesTrackerInvoices(invhdr, invItm);
                }
               

                dgvInv.DataSource = new int[] { };
                if (_dtInvoice != null)
                {
                    if (_dtInvoice.Rows.Count > 0)
                    {
                        dgvInv.DataSource = _dtInvoice;
                    }
                }
                dgvInv.DataBind();
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnInvoiceSelect_Click(object sender, EventArgs e)
        {
            try
            {
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                Label lblsah_inv_no = row.FindControl("lblsah_inv_no") as Label;
                DataTable dtSah= CHNLSVC.Sales.GetSalesDet(lblsah_inv_no.Text.Trim());
                DataTable dtDelivery = CHNLSVC.Sales.GetIntHdrData(new InventoryHeader() { Ith_doc_tp = "DO", Ith_oth_docno=lblsah_inv_no.Text.Trim()});
                List<InventorySerialN> _serList = new List<InventorySerialN>();
                if (dtDelivery!=null)
                {
                    if (dtDelivery.Rows.Count>0)
                    {
                        DataTable grtb = dtDelivery;
                    grtb = grtb.AsEnumerable()
       .GroupBy(r => new { Col1 = r["ith_doc_no"] })
       .Select(g => g.OrderBy(r => r["ith_doc_no"]).First())
       .CopyToDataTable();
                    foreach (DataRow rw in grtb.Rows)
                    {
                        if (!string.IsNullOrEmpty(rw["ith_doc_no"].ToString()))
                        {
                            List<InventorySerialN> _tmpList = CHNLSVC.Inventory.Get_INT_SER_DATA(new InventorySerialN()
                            {
                                Ins_doc_no = rw["ith_doc_no"].ToString()
                            });
                            if (_tmpList != null)
                            {
                                if (_tmpList.Count > 0)
                                {
                                    foreach (var item in _tmpList)
                                    {
                                        _serList.Add(item);
                                    }
                                }
                            }
                        }

                    }
                    }
                }
               
                
                dgvInvDet.DataSource = new int[] { };
                dgvDel.DataSource = new int[] { };
                dgvSerial.DataSource = new int[] { };
                if (dtSah!=null)
                {
                    if (dtSah.Rows.Count>0)
                    {
                        dgvInvDet.DataSource = dtSah;
                    }
                }
                if (dtDelivery != null)
                {
                    if (dtDelivery.Rows.Count > 0)
                    {
                        dgvDel.DataSource = dtDelivery;
                    }
                }
                Dictionary<string, string> status_list = CHNLSVC.Inventory.Get_all_ItemSatus();
                
                if (_serList != null)
                {
                    foreach (var item in _serList)
                    {
                        MasterItem _mstItm=CHNLSVC.General.GetItemMaster(item.Ins_itm_cd);
                        if (_mstItm!=null)
                        {
                            item.tmpItmDesc = _mstItm.Mi_shortdesc;
                            item.tmpItmTp = _mstItm.Mi_itm_tp;
                        }
                        item.tmpItmStsDesc = status_list.Where(c => c.Key == item.Ins_itm_stus).FirstOrDefault().Value;
                        
                    }
                    if (_serList.Count > 0)
                    {
                        dgvSerial.DataSource = _serList;
                    }
                }
                dgvInvDet.DataBind();
                dgvDel.DataBind();
                dgvSerial.DataBind();
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnSer2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtInvNo.Text) && string.IsNullOrEmpty(txtDelNo.Text) && string.IsNullOrEmpty(txtSer.Text))
                {
                    DispMsg("Please select search data !!!"); return;
                }
                dgvInv.DataSource = new int[] { };
                dgvInvDet.DataSource = new int[] { };
                dgvDel.DataSource = new int[] { };
                dgvSerial.DataSource = new int[] { };

                dgvInv.DataBind();
                dgvInvDet.DataBind();
                dgvDel.DataBind();
                dgvSerial.DataBind();

                InventoryHeader invhdr = new InventoryHeader();
                invhdr.Ith_doc_tp = !string.IsNullOrEmpty(txtInvNo.Text) ? "INV" : !string.IsNullOrEmpty(txtDelNo.Text) ? "DEL" : !string.IsNullOrEmpty(txtSer.Text) ? "SER" : "";
                invhdr.Ith_doc_no = !string.IsNullOrEmpty(txtInvNo.Text) ? txtInvNo.Text.Trim() : "";
                invhdr.Ith_oth_docno = !string.IsNullOrEmpty(txtDelNo.Text) ? txtDelNo.Text.Trim() : !string.IsNullOrEmpty(txtSer.Text) ? txtSer.Text.Trim() : "";
                DataTable _dtInvoice = new DataTable();
                try
                {
                    _dtInvoice = CHNLSVC.Sales.GetIntHdrDataByInv(invhdr);
                }
                catch (Exception)
                {
                    _dtInvoice = new DataTable();
                }
                dgvInv.DataSource = new int[] { };
                if (_dtInvoice != null)
                {
                    if (_dtInvoice.Rows.Count > 0)
                    {
                        dgvInv.DataSource = _dtInvoice;
                    }
                }
                dgvInv.DataBind();

                if (invhdr.Ith_doc_tp=="SER")
                {
                    if (dgvInv.Rows.Count>0)
                    {
                        foreach (GridViewRow row in dgvInv.Rows)
                        {
                            Label lblsah_inv_no = row.FindControl("lblsah_inv_no") as Label;
                            DataTable dtSah = CHNLSVC.Sales.GetSalesDet(lblsah_inv_no.Text.Trim());
                            DataTable dtDelivery = CHNLSVC.Sales.GetIntHdrData(new InventoryHeader() { Ith_doc_tp = "DO", Ith_oth_docno = lblsah_inv_no.Text.Trim() });
                            List<InventorySerialN> _serList = new List<InventorySerialN>();
                            if (dtDelivery != null)
                            {
                                foreach (DataRow rw in dtDelivery.Rows)
                                {
                                    if (!string.IsNullOrEmpty(rw["ith_doc_no"].ToString()))
                                    {
                                        List<InventorySerialN> _tmpList = CHNLSVC.Inventory.Get_INT_SER_DATA(new InventorySerialN()
                                        {
                                            Ins_doc_no = rw["ith_doc_no"].ToString()
                                        });
                                        if (_tmpList != null)
                                        {
                                            if (_tmpList.Count > 0)
                                            {
                                                foreach (var item in _tmpList)
                                                {
                                                    _serList.Add(item);
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                            dgvInvDet.DataSource = new int[] { };
                            dgvDel.DataSource = new int[] { };
                            dgvSerial.DataSource = new int[] { };
                            if (dtSah != null)
                            {
                                if (dtSah.Rows.Count > 0)
                                {
                                    dgvInvDet.DataSource = dtSah;
                                }
                            }
                            if (dtDelivery != null)
                            {
                                if (dtDelivery.Rows.Count > 0)
                                {
                                    dgvDel.DataSource = dtDelivery;
                                }
                            }
                            Dictionary<string, string> status_list = CHNLSVC.Inventory.Get_all_ItemSatus();

                            if (_serList != null)
                            {
                                foreach (var item in _serList)
                                {
                                    MasterItem _mstItm = CHNLSVC.General.GetItemMaster(item.Ins_itm_cd);
                                    if (_mstItm != null)
                                    {
                                        item.tmpItmDesc = _mstItm.Mi_shortdesc;
                                        item.tmpItmTp = _mstItm.Mi_itm_tp;
                                    }
                                    item.tmpItmStsDesc = status_list.Where(c => c.Key == item.Ins_itm_stus).FirstOrDefault().Value;

                                }
                                if (_serList.Count > 0)
                                {
                                    dgvSerial.DataSource = _serList;
                                }
                            }
                            dgvInvDet.DataBind();
                            dgvDel.DataBind();
                            dgvSerial.DataBind();
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

      
    }
}