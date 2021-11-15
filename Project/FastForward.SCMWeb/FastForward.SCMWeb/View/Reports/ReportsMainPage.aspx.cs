using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Reports
{
    public partial class ReportsMainPage : BasePage
    {
        string framURL;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString[0] == null)
                {
                    DisplayMessages("Please check report menu details(sec_system_menu)");
                    return;
                }

                lblReportName.Text = "";

                string menuName = Request.QueryString[0];
                getMenuItems(menuName);

                setDefaultValues();
            }
        }

        private void DisplayMessages(string message)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
            }
        }

        private void getMenuItems(String Name)
        {
            List<SEC_SYSTEM_MENU_SUB> omenItems = CHNLSVC.General.GET_REPORT_LIST_BY_MENU(Name);
            foreach (SEC_SYSTEM_MENU_SUB item in omenItems)
            {
                lstMenuItems.Items.Add(new ListItem(item.Ssms_name, item.Ssms_target_page, true));
            }
        }

        private void setDefaultValues()
        {
            txtFromDate.Text = DateTime.Now.AddYears(-1).ToString("dd/MMM/yyyy");
            txtTodate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtAsAtDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
        }

        protected void lstMenuItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblReportName.Text = lstMenuItems.SelectedItem.Text;

                framURL = lstMenuItems.SelectedItem.Value;

                if (framURL == "rptAllSecurityUsers.rpt")
                {
                    divCompany.Visible = true;
                    divDepartment.Visible = true;
                    divUser.Visible = true;
                }
                else if (framURL == "UserRole_report.rpt")
                {
                    divDepartment.Visible = true;
                    divUser.Visible = true;
                }
                else if (framURL == "UserPrivileges_reportReport.rpt")
                {
                    divDepartment.Visible = true;
                    divUser.Visible = true;
                }
                // iFrame.Attributes["src"] = "ReportParameters.aspx";
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
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
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Sales_Type:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Sales_SubType:
                    {
                        paramsText.Append(txtSalesType.Text.Trim().ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CircularByComp:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ReceiptType:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceExecutive:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Employee_Executive:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "SEX" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(txtItemCate1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtItemCate1.Text + seperator + txtItemCate2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SystemUser:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        #region Methods
        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.cmbSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.cmbSearchbykey.Items.Add(col.ColumnName);
            }

            this.cmbSearchbykey.SelectedIndex = 0;
        }

        private void setYearAndMonth()
        {
            ddlYear.Items.Add("2012");
            ddlYear.Items.Add("2013");
            ddlYear.Items.Add("2014");
            ddlYear.Items.Add("2015");
            ddlYear.Items.Add("2016");
            ddlYear.Items.Add("2017");
            ddlYear.Items.Add("2018");

            int _Year = DateTime.Now.Year;
            ddlYear.SelectedIndex = _Year % 2013 + 1;

            ddlMonth.Items.Add("January");
            ddlMonth.Items.Add("February");
            ddlMonth.Items.Add("March");
            ddlMonth.Items.Add("April");
            ddlMonth.Items.Add("May");
            ddlMonth.Items.Add("June");
            ddlMonth.Items.Add("July");
            ddlMonth.Items.Add("August");
            ddlMonth.Items.Add("September");
            ddlMonth.Items.Add("October");
            ddlMonth.Items.Add("November");
            ddlMonth.Items.Add("December");
            ddlMonth.SelectedIndex = DateTime.Now.Month - 1;
        }

        #endregion

        #region Events
        protected void btnAddParameters_Click(object sender, EventArgs e)
        {
            try
            {
                string com = ucProfitCenterSearch.Company;
                string chanel = ucProfitCenterSearch.Channel;
                string subChanel = ucProfitCenterSearch.SubChannel;
                string area = ucProfitCenterSearch.Area;
                string region = ucProfitCenterSearch.Regien;
                string zone = ucProfitCenterSearch.Zone;
                string pc = ucProfitCenterSearch.ProfitCenter;

                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com.ToUpper(), chanel.ToUpper(), subChanel.ToUpper(), area.ToUpper(), region.ToUpper(), zone.ToUpper(), pc.ToUpper());
                dgvPcs.DataSource = null;
                dgvPcs.AutoGenerateColumns = false;
                dgvPcs.DataSource = dt;
                dgvPcs.DataBind();
                if (dt.Rows.Count <= 0)
                {
                    DisplayMessages("Please check profit center hierarchy setup.");
                    return;
                }
            }
            catch (Exception ex)
            {
                DisplayMessages("Error Occurred while processing..");
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btnSelectAllPC_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvPcs.Rows.Count; i++)
            {
                GridViewRow row = dgvPcs.Rows[i];
                CheckBox chk = (CheckBox)row.FindControl("chkSelect");
                chk.Checked = true;
            }
        }

        protected void btnUnselectAllPc_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvPcs.Rows.Count; i++)
            {
                GridViewRow row = dgvPcs.Rows[i];
                CheckBox chk = (CheckBox)row.FindControl("chkSelect");
                chk.Checked = false;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            dgvPcs.DataSource = null;
            dgvPcs.DataBind();
        }

        #region Search
        protected void btnSalesType_Click(object sender, ImageClickEventArgs e)
        {
            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_Type);
            DataTable _result = CHNLSVC.General.GetSalesTypes(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "GetSalesTypes";
            lblTargetControl.Text = txtSalesType.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

        protected void btnSalesSubType_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSalesType.Text))
            {
                txtSearchbyword.Text = "";
                dvResultUser.DataSource = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_SubType);
                DataTable _result = CHNLSVC.CommonSearch.Get_sales_subtypes(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                Label1.Text = "Get_sales_subtypes";
                lblTargetControl.Text = txtSalesSubType.ID;
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
            }
            else
            {
                DisplayMessages("Please select a sales type");
                txtSalesType.Focus();
            }
        }

        protected void btnDocumentNo_Click(object sender, ImageClickEventArgs e)
        {
            if (lblDocNum.Text == "Circular No")
            {
                txtSearchbyword.Text = "";
                dvResultUser.DataSource = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CircularByComp);
                DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchDataByComp(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                Label1.Text = "GetCircularSearchDataByComp";
                lblTargetControl.Text = txtDocumentNo.ID;
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
            }
        }

        protected void btnDirection_Click(object sender, ImageClickEventArgs e)
        {
            if (lblDirection.Text == "Promo Code")
            {
                txtSearchbyword.Text = "";
                dvResultUser.DataSource = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromoByComp);
                DataTable _result = CHNLSVC.CommonSearch.GetSearchDataForPromoByComp(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                Label1.Text = "GetSearchDataForPromoByComp";
                lblTargetControl.Text = txtDirection.ID;
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
            }
        }

        protected void btnEntryType_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnReceiptType_Click(object sender, ImageClickEventArgs e)
        {
            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReceiptType);
            DataTable _result = CHNLSVC.CommonSearch.GetReceiptTypes(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "GetReceiptTypes";
            lblTargetControl.Text = txtReceiptType.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

        protected void btnStatus_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnCustomer_Click(object sender, ImageClickEventArgs e)
        {
            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "GetCustomerGenaral";
            lblTargetControl.Text = txtCustomer.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

        protected void btnExecutive_Click(object sender, ImageClickEventArgs e)
        {
            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceExecutive);
            DataTable _result = CHNLSVC.CommonSearch.SearchEmployeeAssignToProfitCenter(SearchParams, null, null);
            Label1.Text = "SearchEmployeeAssignToProfitCenter";
            if (_result == null || _result.Rows.Count <= 0)
            {
                SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Employee_Executive);
                _result = CHNLSVC.CommonSearch.GetEmployeeData(SearchParams, null, null);
                Label1.Text = "GetEmployeeData";
            }
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            lblTargetControl.Text = txtExecutive.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

        protected void btnItemStatus_Click(object sender, ImageClickEventArgs e)
        {
            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
            DataTable _result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "GetCompanyItemStatusData";
            lblTargetControl.Text = txtItemStatus.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

        protected void btnPayType_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnItemCate1_Click(object sender, ImageClickEventArgs e)
        {
            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "GetCat_SearchData1";
            lblTargetControl.Text = txtItemCate1.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

        protected void btnItemCate2_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCate1.Text))
            {
                txtSearchbyword.Text = "";
                dvResultUser.DataSource = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                Label1.Text = "GetCat_SearchData2";
                lblTargetControl.Text = txtItemCate2.ID;
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
            }
            else
            {
                DisplayMessages("please select a item category 1");
                txtItemCate1.Focus();
            }
        }

        protected void btnItemCate3_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCate2.Text))
            {
                txtSearchbyword.Text = "";
                dvResultUser.DataSource = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                Label1.Text = "GetCat_SearchData3";
                lblTargetControl.Text = txtItemCate3.ID;
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
            }
            else
            {
                DisplayMessages("Please enter item category 2");
                txtItemCate2.Focus();
            }
        }

        protected void btnItemCode_Click(object sender, ImageClickEventArgs e)
        {
            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "GetItemSearchData";
            lblTargetControl.Text = txtItemCode.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

        protected void btnBrand_Click(object sender, ImageClickEventArgs e)
        {
            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "GetCat_SearchData";
            lblTargetControl.Text = txtBrand.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

        protected void btnModel_Click(object sender, ImageClickEventArgs e)
        {
            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
            DataTable _result = CHNLSVC.CommonSearch.GetAllModels(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "GetAllModels";
            lblTargetControl.Text = txtModel.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }



        protected void btnDepartment_Click(object sender, ImageClickEventArgs e)
        {
            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Department);
            DataTable _result = CHNLSVC.CommonSearch.Get_Departments(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "Get_Departments";
            lblTargetControl.Text = txtDepartment.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

        protected void btnUser_Click(object sender, ImageClickEventArgs e)
        {
            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SystemUser);
            DataTable _result = CHNLSVC.CommonSearch.Get_All_SystemUsers(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "Get_All_SystemUsers";
            lblTargetControl.Text = txtUser.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }
        #endregion

        #region Common Search
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            ImageSearch_Click(null, null);
        }

        protected void ImageSearch_Click(object sender, ImageClickEventArgs e)
        {
            DataTable _result = new DataTable();

            if (Label1.Text == "GetSalesTypes")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_Type);
                _result = CHNLSVC.General.GetSalesTypes(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
            }
            else if (Label1.Text == "Get_sales_subtypes")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_SubType);
                _result = CHNLSVC.CommonSearch.Get_sales_subtypes(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());

            }
            else if (Label1.Text == "GetCircularSearchDataByComp")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CircularByComp);
                _result = CHNLSVC.CommonSearch.GetCircularSearchDataByComp(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
            }
            else if (Label1.Text == "GetSearchDataForPromoByComp")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromoByComp);
                _result = CHNLSVC.CommonSearch.GetSearchDataForPromoByComp(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());

            }
            else if (Label1.Text == "GetReceiptTypes")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReceiptType);
                _result = CHNLSVC.CommonSearch.GetReceiptTypes(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
            }
            else if (Label1.Text == "GetCustomerGenaral")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                _result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
            }
            else if (Label1.Text == "SearchEmployeeAssignToProfitCenter")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceExecutive);
                _result = CHNLSVC.CommonSearch.SearchEmployeeAssignToProfitCenter(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
            }
            else if (Label1.Text == "GetEmployeeData")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Employee_Executive);
                _result = CHNLSVC.CommonSearch.GetEmployeeData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
            }
            else if (Label1.Text == "GetCompanyItemStatusData")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
                _result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
            }
            else if (Label1.Text == "GetCat_SearchData1")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
            }
            else if (Label1.Text == "GetCat_SearchData2")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
            }
            else if (Label1.Text == "GetCat_SearchData3")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
            }
            else if (Label1.Text == "GetItemSearchData")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                _result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
            }
            else if (Label1.Text == "GetCat_SearchData")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                _result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
            }
            else if (Label1.Text == "GetAllModels")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                _result = CHNLSVC.CommonSearch.GetAllModels(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
            }
            else if (Label1.Text == "Get_PC_HIRC_SearchData")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
            }
            else if (Label1.Text == "Get_Departments")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Department);
                _result = CHNLSVC.CommonSearch.Get_Departments(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
            }
            else if (Label1.Text == "Get_All_SystemUsers")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SystemUser);
                _result = CHNLSVC.CommonSearch.Get_All_SystemUsers(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
            }

            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            UserPopoup.Show();
        }

        protected void dvResultUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = dvResultUser.SelectedRow.Cells[1].Text;
            string Des = dvResultUser.SelectedRow.Cells[2].Text;
            if (lblTargetControl.Text == "txtReceiptType")
            {
                txtReceiptType.Text = name;
            }
            if (lblTargetControl.Text == "txtCompany")
            {
                txtCompany.Text = name;
            }
            if (lblTargetControl.Text == "txtDepartment")
            {
                txtDepartment.Text = name;
            }
            if (lblTargetControl.Text == "txtUser")
            {
                txtUser.Text = name;
            }

        }
        #endregion

        protected void btnSalesTypeAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSalesType.Text))
            {
                lstGroupingDetails.Items.Add(new ListItem(txtSalesType.Text));
            }
        }

        protected void btnSalesSubTypeAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSalesSubType.Text))
            {
                lstGroupingDetails.Items.Add(txtSalesSubType.Text);
            }
        }

        protected void btnDocumentNoAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDocumentNo.Text))
            {
                lstGroupingDetails.Items.Add(txtDocumentNo.Text);
            }
        }

        protected void btnCustomerAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCustomer.Text))
            {
                lstGroupingDetails.Items.Add(txtCustomer.Text);
            }
        }

        protected void btnExecutiveAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtExecutive.Text))
            {
                lstGroupingDetails.Items.Add(txtExecutive.Text);
            }
        }

        protected void btnItemStatusAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemStatus.Text))
            {
                lstGroupingDetails.Items.Add(txtItemStatus.Text);
            }
        }

        protected void btnItemCate1Add_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCate1.Text))
            {
                lstCate1.Items.Add(txtItemCate1.Text);
            }
        }

        protected void btnItemCate2Add_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCate2.Text))
            {
                lstCate2.Items.Add(txtItemCate2.Text);
            }
        }

        protected void btnItemCate3Add_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCate3.Text))
            {
                lstCate3.Items.Add(txtItemCate3.Text);
            }
        }

        protected void btnItemCodeAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCode.Text))
            {
                lstItemCode.Items.Add(txtItemCode.Text);
            }
        }

        protected void btnBrandAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBrand.Text))
            {
                lstBrands.Items.Add(txtBrand.Text);
            }
        }

        protected void btnRemoveItems_Click(object sender, EventArgs e)
        {
            if (lstGroupingDetails.SelectedItem != null)
            {
                lstGroupingDetails.Items.Remove(lstGroupingDetails.SelectedItem);
            }
        }

        protected void btnRemoveCate1_Click(object sender, EventArgs e)
        {
            if (lstCate1.SelectedItem != null)
            {
                lstCate1.Items.Remove(lstCate1.SelectedItem);
            }
        }

        protected void btnRemoveCate2_Click(object sender, EventArgs e)
        {
            if (lstCate2.SelectedItem != null)
            {
                lstCate2.Items.Remove(lstCate2.SelectedItem);
            }
        }

        protected void btnRemoveCate3_Click(object sender, EventArgs e)
        {
            if (lstCate3.SelectedItem != null)
            {
                lstCate3.Items.Remove(lstCate3.SelectedItem);
            }
        }

        protected void btnRemoveItemCode_Click(object sender, EventArgs e)
        {
            if (lstItemCode.SelectedItem != null)
            {
                lstItemCode.Items.Remove(lstItemCode.SelectedItem);
            }
        }

        protected void btnRemoveBrand_Click(object sender, EventArgs e)
        {
            if (lstBrands.SelectedItem != null)
            {
                lstBrands.Items.Remove(lstBrands.SelectedItem);
            }
        }

        protected void btnBrandAddD_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBrand.Text))
            {
                lstBrands.Items.Add(txtBrand.Text);
            }
        }

        protected void btnItemCodeAddD_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCode.Text))
            {
                lstItemCode.Items.Add(txtItemCode.Text);
            }
        }

        protected void btnItemCate3AddD_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCate2.Text))
            {
                lstCate2.Items.Add(txtItemCate2.Text);
            }
        }

        protected void btnItemCate2AddD_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCate2.Text))
            {
                lstCate2.Items.Add(txtItemCate2.Text);
            }
        }

        protected void btnItemCate1AddD_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCate1.Text))
            {
                lstCate1.Items.Add(txtItemCate1.Text);
            }
        }

        protected void btnItemCate1Add_Click(object sender, ImageClickEventArgs e)
        {
            lstGroupingDetails.Items.Add("CAT1");
        }

        protected void btnItemCate2Add_Click(object sender, ImageClickEventArgs e)
        {
            lstGroupingDetails.Items.Add("CAT2");
        }

        protected void btnItemCate3Add_Click(object sender, ImageClickEventArgs e)
        {
            lstGroupingDetails.Items.Add("CAT3");
        }

        protected void btnItemCodeAdd_Click(object sender, ImageClickEventArgs e)
        {
            lstGroupingDetails.Items.Add("ITM");
        }

        protected void btnBrandAdd_Click(object sender, ImageClickEventArgs e)
        {
            lstGroupingDetails.Items.Add("BRND");
        }

        protected void btnModelD_Click(object sender, ImageClickEventArgs e)
        {
            lstGroupingDetails.Items.Add("MDL");
        }

        #endregion

        #region Checkbox
        protected void chkCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCompany.Checked)
            {
                btnCompanyNew.Enabled = false;
                txtCompany.Enabled = false;
                txtCompany.Text = "";
            }
            else
            {
                txtCompany.Enabled = true;
                btnCompanyNew.Enabled = true;
            }
        }

        protected void chkDepartment_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDepartment.Checked)
            {
                btnDepartmentNew.Enabled = false;
                txtDepartment.Enabled = false;
                txtDepartment.Text = "";
            }
            else
            {
                txtDepartment.Enabled = true;
                btnDepartmentNew.Enabled = true;
            }
        }

        protected void chkUser_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUser.Checked)
            {
                btnDepartmentNew.Enabled = true;
                txtUser.Enabled = false;
                txtUser.Text = "";
            }
            else
            {
                txtUser.Enabled = true;
                btnUserNew.Enabled = true;
            }
        }

        protected void chkSalesType_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSalesType.Checked)
            {
                btnSalesTypeNew.Enabled = false;
                txtSalesType.Enabled = false;
                txtSalesType.Text = "";
            }
            else
            {
                txtSalesType.Enabled = true;
                btnSalesTypeNew.Enabled = true;
            }
        }

        protected void ckSalesSubType_CheckedChanged(object sender, EventArgs e)
        {
            if (ckSalesSubType.Checked)
            {
                btnSalesSubTypeNew.Enabled = false;
                txtSalesSubType.Enabled = false;
                txtSalesSubType.Text = "";
            }
            else
            {
                txtSalesSubType.Enabled = true;
                btnSalesSubTypeNew.Enabled = true;
            }
        }

        protected void chkDocumentNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDocumentNo.Checked)
            {
                btnDocumentNoNew.Enabled = false;
                txtDocumentNo.Enabled = false;
                txtDocumentNo.Text = "";
            }
            else
            {
                txtDocumentNo.Enabled = true;
                btnDocumentNoNew.Enabled = true;
            }
        }

        protected void chkDirection_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDirection.Checked)
            {
                btnDirectionNew.Enabled = false;
                txtDirection.Enabled = false;
                txtDirection.Text = "";
            }
            else
            {
                txtDirection.Enabled = true;
                btnDirectionNew.Enabled = true;
            }
        }

        protected void chkEntryType_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEntryType.Checked)
            {

                ddlEntryType.Enabled = false;
            }
            else
            {
                ddlEntryType.Enabled = true;

            }
        }

        protected void chkReciptType_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReciptType.Checked)
            {
                btnReceiptTypeNew.Enabled = false;
                txtReceiptType.Enabled = false;
                txtReceiptType.Text = "";
            }
            else
            {
                txtReceiptType.Enabled = true;
                btnReceiptTypeNew.Enabled = true;
            }
        }

        protected void chkStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (chkStatus.Checked)
            {
                ddlStatus.Enabled = false;
                ddlStatus.Enabled = false;
            }
            else
            {
                ddlStatus.Enabled = true;
                ddlStatus.Enabled = true;
            }
        }

        protected void chkCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCustomer.Checked)
            {
                btnCustomerNew.Enabled = false;
                txtCustomer.Enabled = false;
                txtCustomer.Text = "";
            }
            else
            {
                txtCustomer.Enabled = true;
                btnCustomerNew.Enabled = true;
            }
        }

        protected void chkExecutive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExecutive.Checked)
            {
                btnExecutiveNew.Enabled = false;
                txtExecutive.Enabled = false;
                txtExecutive.Text = "";
            }
            else
            {
                txtExecutive.Enabled = true;
                btnExecutiveNew.Enabled = true;
            }
        }

        protected void chkItemStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (chkItemStatus.Checked)
            {
                btnItemStatusNew.Enabled = false;
                txtItemStatus.Enabled = false;
                txtItemStatus.Text = "";
            }
            else
            {
                txtItemStatus.Enabled = true;
                btnItemStatusNew.Enabled = true;
            }
        }

        protected void chkPayType_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chkItemCate1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkItemCate1.Checked)
            {
                btnItemCate1New.Enabled = false;
                txtItemCate1.Enabled = false;
                txtItemCate1.Text = "";
            }
            else
            {
                txtItemCate1.Enabled = true;
                btnItemCate1New.Enabled = true;
            }
        }

        protected void chkItemCate2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkItemCate2.Checked)
            {
                btnItemCate2New.Enabled = false;
                txtItemCate2.Enabled = false;
                txtItemCate2.Text = "";
            }
            else
            {
                txtItemCate2.Enabled = true;
                btnItemCate2New.Enabled = true;
            }
        }

        protected void chkItemCate3_CheckedChanged(object sender, EventArgs e)
        {
            if (chkItemCate3.Checked)
            {
                btnItemCate3New.Enabled = false;
                txtItemCate3.Enabled = false;
                txtItemCate3.Text = "";
            }
            else
            {
                txtItemCate3.Enabled = true;
                btnItemCate3New.Enabled = true;
            }
        }

        protected void chkItemCode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkItemCode.Checked)
            {
                btnItemCodeNew.Enabled = false;
                txtItemCode.Enabled = false;
                txtItemCode.Text = "";
            }
            else
            {
                txtItemCode.Enabled = true;
                btnItemCodeNew.Enabled = true;
            }
        }

        protected void chkBrand_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBrand.Checked)
            {
                btnBrandNew.Enabled = false;
                txtBrand.Enabled = false;
                txtBrand.Text = "";
            }
            else
            {
                txtBrand.Enabled = true;
                btnBrandNew.Enabled = true;
            }
        }

        protected void chkModel_CheckedChanged(object sender, EventArgs e)
        {
            if (chkModel.Checked)
            {
                btnModelNew.Enabled = false;
                txtModel.Enabled = false;
                txtModel.Text = "";
            }
            else
            {
                txtModel.Enabled = true;
                btnModelNew.Enabled = true;
            }
        }
        #endregion

        protected void btnDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstMenuItems.SelectedItem == null)
                {
                    DisplayMessages("Please select a report from the list");
                    return;
                }

                framURL = lstMenuItems.SelectedItem.Value;

                if (framURL == "rptAllSecurityUsers.rpt")
                {
                    if (validate())
                    {
                        Session["GlbReportName"] = string.Empty;
                        GlbReportName = string.Empty;

                        BaseCls.GlbReportFromDate = Convert.ToDateTime(txtFromDate.Text).Date;
                        BaseCls.GlbReportToDate = Convert.ToDateTime(txtTodate.Text).Date;
                        BaseCls.GlbReportAsAtDate = Convert.ToDateTime(txtAsAtDate.Text).Date;
                        BaseCls.GlbReportHeading = "All Security Users";
                        Session["GlbReportName"] = "rptAllSecurityUsers.rpt";
                        BaseCls.GlbReportCompCode = txtCompany.Text;
                        BaseCls.GlbReportDepartment = txtDepartment.Text;
                        BaseCls.GlbReportUser = txtUser.Text;
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'Audit/Audit_Viwer.aspx', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                    }
                }
                else if (framURL == "UserRole_report.rpt")
                {
                    BaseCls.GlbReportHeading = "All Security Users";
                    Session["GlbReportName"] = "UserRole_report.rpt";
                    BaseCls.GlbReportDepartment = txtDepartment.Text;
                    BaseCls.GlbReportUser = txtUser.Text;
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'Audit/Audit_Viwer.aspx', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                }
                else if (framURL == "UserPrivileges_reportReport.rpt")
                {
                    BaseCls.GlbReportHeading = "All Security Users";
                    Session["GlbReportName"] = "UserPrivileges_reportReport.rpt";
                    BaseCls.GlbReportDepartment = txtDepartment.Text;
                    BaseCls.GlbReportUser = txtUser.Text;
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( 'Audit/Audit_Viwer.aspx', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                }
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }

        }

        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {
            DateTime oOut;
            bool o1 = DateTime.TryParse(txtFromDate.Text, out oOut);
            if (o1 == false)
            {
                DisplayMessages("Please enter a valid from date");
                txtFromDate.Text = DateTime.Now.AddYears(-1).ToString("dd/MMM/yyyy");
            }
        }

        protected void txtTodate_TextChanged(object sender, EventArgs e)
        {
            DateTime oOut;
            bool o1 = DateTime.TryParse(txtTodate.Text, out oOut);
            if (o1 == false)
            {
                DisplayMessages("Please enter a valid to date");
                txtTodate.Text = DateTime.Now.AddYears(-1).ToString("dd/MMM/yyyy");
            }
        }

        protected void txtAsAtDate_TextChanged(object sender, EventArgs e)
        {
            DateTime oOut;
            bool o1 = DateTime.TryParse(txtAsAtDate.Text, out oOut);
            if (o1 == false)
            {
                DisplayMessages("Please enter a valid As At Date date");
                txtAsAtDate.Text = DateTime.Now.AddYears(-1).ToString("dd/MMM/yyyy");
            }
        }

        private bool validate()
        {
            bool status = true;
            DateTime oOut;
            bool o1 = DateTime.TryParse(txtFromDate.Text, out oOut);
            if (o1 == false)
            {
                DisplayMessages("Please enter a valid from date");
                txtFromDate.Text = DateTime.Now.AddYears(-1).ToString("dd/MMM/yyyy");
                status = false;
                return status;
            }
            o1 = DateTime.TryParse(txtTodate.Text, out oOut);
            if (o1 == false)
            {
                DisplayMessages("Please enter a valid to date");
                txtTodate.Text = DateTime.Now.AddYears(-1).ToString("dd/MMM/yyyy");
                status = false;
                return status;
            }
            o1 = DateTime.TryParse(txtAsAtDate.Text, out oOut);
            if (o1 == false)
            {
                DisplayMessages("Please enter a valid As At Date date");
                txtAsAtDate.Text = DateTime.Now.AddYears(-1).ToString("dd/MMM/yyyy");
                status = false;
                return status;
            }
            return status;
        }

        protected void btnCompanyNew_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "Get_PC_HIRC_SearchData";
            lblTargetControl.Text = txtCompany.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

        protected void btnDepartmentNew_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Department);
            DataTable _result = CHNLSVC.CommonSearch.Get_Departments(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "Get_Departments";
            lblTargetControl.Text = txtDepartment.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

        protected void btnUserNew_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SystemUser);
            DataTable _result = CHNLSVC.CommonSearch.Get_All_SystemUsers(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "Get_All_SystemUsers";
            lblTargetControl.Text = txtUser.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

        protected void btnSalesTypeNew_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_Type);
            DataTable _result = CHNLSVC.General.GetSalesTypes(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "GetSalesTypes";
            lblTargetControl.Text = txtSalesType.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

        protected void btnSalesTypeAddINew_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSalesType.Text))
            {
                lstGroupingDetails.Items.Add(new ListItem(txtSalesType.Text));
            }
        }

        protected void btnSalesSubTypeNew_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSalesType.Text))
            {
                txtSearchbyword.Text = "";
                dvResultUser.DataSource = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_SubType);
                DataTable _result = CHNLSVC.CommonSearch.Get_sales_subtypes(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                Label1.Text = "Get_sales_subtypes";
                lblTargetControl.Text = txtSalesSubType.ID;
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
            }
            else
            {
                DisplayMessages("Please select a sales type");
                txtSalesType.Focus();
            }
        }

        protected void btnSalesSubTypeAddINew_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSalesSubType.Text))
            {
                lstGroupingDetails.Items.Add(txtSalesSubType.Text);
            }
        }

        protected void btnDocumentNoNew_Click(object sender, EventArgs e)
        {
            if (lblDocNum.Text == "Circular No")
            {
                txtSearchbyword.Text = "";
                dvResultUser.DataSource = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CircularByComp);
                DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchDataByComp(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                Label1.Text = "GetCircularSearchDataByComp";
                lblTargetControl.Text = txtDocumentNo.ID;
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
            }
        }

        protected void btnDocumentNoAddINew_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDocumentNo.Text))
            {
                lstGroupingDetails.Items.Add(txtDocumentNo.Text);
            }
        }

        protected void btnDirectionNew_Click(object sender, EventArgs e)
        {
            if (lblDirection.Text == "Promo Code")
            {
                txtSearchbyword.Text = "";
                dvResultUser.DataSource = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PromoByComp);
                DataTable _result = CHNLSVC.CommonSearch.GetSearchDataForPromoByComp(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                Label1.Text = "GetSearchDataForPromoByComp";
                lblTargetControl.Text = txtDirection.ID;
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
            }
        }

        protected void btnEntryTypeNew_Click(object sender, EventArgs e)
        {

        }

        protected void btnReceiptTypeNew_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReceiptType);
            DataTable _result = CHNLSVC.CommonSearch.GetReceiptTypes(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "GetReceiptTypes";
            lblTargetControl.Text = txtReceiptType.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

        protected void btnStatusNew_Click(object sender, EventArgs e)
        {

        }

        protected void btnCustomerNew_Click(object sender, EventArgs e)
        {

        }

        protected void btnCustomerAddINew_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "GetCustomerGenaral";
            lblTargetControl.Text = txtCustomer.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

        protected void btnExecutiveNew_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceExecutive);
            DataTable _result = CHNLSVC.CommonSearch.SearchEmployeeAssignToProfitCenter(SearchParams, null, null);
            Label1.Text = "SearchEmployeeAssignToProfitCenter";
            if (_result == null || _result.Rows.Count <= 0)
            {
                SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Employee_Executive);
                _result = CHNLSVC.CommonSearch.GetEmployeeData(SearchParams, null, null);
                Label1.Text = "GetEmployeeData";
            }
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            lblTargetControl.Text = txtExecutive.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

        protected void btnExecutiveAddINew_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtExecutive.Text))
            {
                lstGroupingDetails.Items.Add(txtExecutive.Text);
            }
        }

        protected void btnItemStatusNew_Click(object sender, EventArgs e)
        {

            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
            DataTable _result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "GetCompanyItemStatusData";
            lblTargetControl.Text = txtItemStatus.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

        protected void btnItemStatusAddINew_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemStatus.Text))
            {
                lstGroupingDetails.Items.Add(txtItemStatus.Text);
            }
        }

        protected void btnPayTypeNew_Click(object sender, EventArgs e)
        {

        }

        protected void btnItemCate1New_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "GetCat_SearchData1";
            lblTargetControl.Text = txtItemCate1.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

        protected void btnItemCate1AddDNew_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCate1.Text))
            {
                lstCate1.Items.Add(txtItemCate1.Text);
            }
        }

        protected void btnItemCate1AddINew_Click(object sender, EventArgs e)
        {
            lstGroupingDetails.Items.Add("CAT1");
        }

        protected void btnItemCate2New_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCate1.Text))
            {
                txtSearchbyword.Text = "";
                dvResultUser.DataSource = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                Label1.Text = "GetCat_SearchData2";
                lblTargetControl.Text = txtItemCate2.ID;
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
            }
            else
            {
                DisplayMessages("please select a item category 1");
                txtItemCate1.Focus();
            }
        }

        protected void btnItemCate2AddDNew_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCate2.Text))
            {
                lstCate2.Items.Add(txtItemCate2.Text);
            }
        }

        protected void btnItemCate2AddINew_Click(object sender, EventArgs e)
        {
            lstGroupingDetails.Items.Add("CAT2");
        }

        protected void btnItemCate3New_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCate2.Text))
            {
                txtSearchbyword.Text = "";
                dvResultUser.DataSource = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
                Label1.Text = "GetCat_SearchData3";
                lblTargetControl.Text = txtItemCate3.ID;
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
            }
            else
            {
                DisplayMessages("Please enter item category 2");
                txtItemCate2.Focus();
            }
        }

        protected void btnItemCate3AddDNew_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCate2.Text))
            {
                lstCate2.Items.Add(txtItemCate2.Text);
            }
        }

        protected void btnItemCate3AddINew_Click(object sender, EventArgs e)
        {
            lstGroupingDetails.Items.Add("CAT3");
        }

        protected void btnItemCodeNew_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "GetItemSearchData";
            lblTargetControl.Text = txtItemCode.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

        protected void btnItemCodeNew_Click1(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "GetItemSearchData";
            lblTargetControl.Text = txtItemCode.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

        protected void btnItemCodeAddDNew_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCode.Text))
            {
                lstItemCode.Items.Add(txtItemCode.Text);
            }
        }

        protected void btnItemCodeAddINew_Click(object sender, EventArgs e)
        {
            lstGroupingDetails.Items.Add("ITM");
        }

        protected void btnBrandNew_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "GetCat_SearchData";
            lblTargetControl.Text = txtBrand.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

        protected void btnBrandNew_Click1(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "GetCat_SearchData";
            lblTargetControl.Text = txtBrand.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

        protected void btnBrandAddDNew_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBrand.Text))
            {
                lstBrands.Items.Add(txtBrand.Text);
            }
        }

        protected void btnBrandAddINew_Click(object sender, EventArgs e)
        {
            lstGroupingDetails.Items.Add("BRND");
        }

        protected void btnModelNew_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
            DataTable _result = CHNLSVC.CommonSearch.GetAllModels(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            Label1.Text = "GetAllModels";
            lblTargetControl.Text = txtModel.ID;
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }

        protected void btnModelDNew_Click(object sender, EventArgs e)
        {
            lstGroupingDetails.Items.Add("MDL");
        }
    }
}