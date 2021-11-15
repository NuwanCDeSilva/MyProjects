using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Enquiries.WareHouse
{
    public partial class ScanDocumentTracker : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInitials();
            }
        }

        private void LoadInitials()
        {
            dropDownListDirection.Items.Insert(0, new ListItem("All", ""));
            dropDownListDirection.Items.Insert(1, new ListItem("Inward", "1"));
            dropDownListDirection.Items.Insert(2, new ListItem("Outward", "0"));
            dbtndate.SelectedValue = "0";

            DropDownListStatus.Items.Insert(0, new ListItem("All", ""));
            DropDownListStatus.Items.Insert(1, new ListItem("Pending", "Pending"));
            DropDownListStatus.Items.Insert(2, new ListItem("Send to scan", "Send to Scan"));
            DropDownListStatus.Items.Insert(3, new ListItem("Working Progress", "Working Progress"));
            DropDownListStatus.Items.Insert(4, new ListItem("Finished", "Finished"));

            //txtDocType.Text = "";
            txtLocation.Text = Session["UserDefLoca"].ToString();
            dtpFromDate.Text = DateTime.Today.AddDays(-7).ToString("dd/MMM/yyyy");
            dtpToDate.Text = DateTime.Today.ToString("dd/MMM/yyyy");

            documentStatusGrid.DataSource = null;
            documentStatusGrid.DataBind();
            itemStatusGrid.DataSource = null;
            itemStatusGrid.DataBind();
            LoadDocTypes();
        }
        private void LoadDocTypes()
        {
            DropDownList DropDownList1 = (DropDownList)UpdatePanelMain.ContentTemplateContainer.FindControl("dropDownDocType");
            DataTable _result = CHNLSVC.CommonSearch.GetDocTypes(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "");

            DropDownList1.Items.Insert(0, new ListItem("All", ""));
            int index = 1;
            foreach (DataRow dr in _result.Rows)
            {
                string dat = dr["DocType"].ToString();
                DropDownList1.Items.Insert(index, new ListItem(dat, dat));
                index++;
            }

        }
        protected void InvoiceChckedChanged(object sender, EventArgs e)
        {
            PendingCheckBox.Checked = false;
            CheckBoxSendtoScan.Checked = false;
            CheckBoxScanComplete.Checked = false;
            CheckBoxAll.Checked = false;
            CheckBoxScanning.Checked = false;
        }
        protected void AllChckedChanged(object sender, EventArgs e)
        {
            PendingCheckBox.Checked = false;
            CheckBoxSendtoScan.Checked = false;
            CheckBoxScanComplete.Checked = false;
            chkInvoice.Checked = false;
            CheckBoxScanning.Checked = false;
        }
        protected void PendingChckedChanged(object sender, EventArgs e)
        {      
            chkInvoice.Checked = false;
            CheckBoxAll.Checked = false;
        }
        protected void SendScanChckedChanged(object sender, EventArgs e)
        {
            chkInvoice.Checked = false;
            CheckBoxAll.Checked = false;
        }
        protected void ScanCompleteChckedChanged(object sender, EventArgs e)
        {
            chkInvoice.Checked = false;
            CheckBoxAll.Checked = false;
        }
        protected void ScanningChckedChanged(object sender, EventArgs e)
        {
            chkInvoice.Checked = false;
            CheckBoxAll.Checked = false;
        }        
        protected void lbtnSearch_Main_Click(object sender, EventArgs e)
        {
            try
            {
                //DropDownListStatus.Items.Insert(0, new ListItem("All", ""));
                //DropDownListStatus.Items.Insert(1, new ListItem("Pending", "Pending"));
                //DropDownListStatus.Items.Insert(2, new ListItem("Send to scan", "Send to Scan"));
                //DropDownListStatus.Items.Insert(3, new ListItem("Working Progress", "Working Progress"));
                //DropDownListStatus.Items.Insert(4, new ListItem("Finished", "Finished"));


                if (txtLocation.Text == string.Empty)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a location !!!')", true);
                    return;
                }
                DropDownList DropDownListDoc = (DropDownList)UpdatePanelMain.ContentTemplateContainer.FindControl("dropDownDocType");
                string pending_doc = "";
                if (DropDownListDoc.SelectedValue.Equals("") || DropDownListDoc.SelectedValue.Equals("SOA"))
                {
                    pending_doc = "SOA";
                }
                DataTable result = new DataTable();
                if (chkInvoice.Checked)
                {
                     result = CHNLSVC.General.GetBatchStatus(Session["UserCompanyCode"].ToString(), txtLocation.Text, DropDownListDoc.SelectedValue, dropDownListDirection.SelectedValue, Convert.ToDateTime(dtpFromDate.Text), Convert.ToDateTime(dtpToDate.Text), Convert.ToInt16(dbtndate.SelectedValue), 0, 0, "", 2);
                }
                else
                {
                     result = CHNLSVC.General.GetBatchStatus(Session["UserCompanyCode"].ToString(), txtLocation.Text, DropDownListDoc.SelectedValue, dropDownListDirection.SelectedValue, Convert.ToDateTime(dtpFromDate.Text), Convert.ToDateTime(dtpToDate.Text), Convert.ToInt16(dbtndate.SelectedValue), 0, 0, pending_doc, 1);
                }
                DataTable finalTable = new DataTable();
                IEnumerable<DataRow> results = null;
                if (result.Rows.Count > 1)
                {
                    if (!CheckBoxAll.Checked)
                    {
                        var sortedTable = result.AsEnumerable()
                       .OrderBy(r => r.Field<String>("Doc_No"));
                        result = sortedTable.CopyToDataTable();                        
                        if (PendingCheckBox.Checked)
                        {
                            results = (from MyRows in result.AsEnumerable()
                                       where
                                        MyRows.Field<string>("STATUS") == "Pending"
                                        && MyRows.Field<string>("Invoice_No") == null
                                       select MyRows);
                            if (results.Any())
                            {
                                DataTable filterresult = results.CopyToDataTable();
                                finalTable.Merge(filterresult);
                                finalTable.AcceptChanges();
                            }

                        }

                        if (CheckBoxSendtoScan.Checked)
                        {
                            results = (from MyRows in result.AsEnumerable()
                                       where
                                        MyRows.Field<string>("STATUS") == "Send to Scan"
                                        && MyRows.Field<string>("Invoice_No") == null
                                       select MyRows);
                            if (results.Any())
                            {
                                DataTable filterresult = results.CopyToDataTable();
                                finalTable.Merge(filterresult);
                                finalTable.AcceptChanges();
                            }

                        }
                        if (CheckBoxScanning.Checked)
                        {
                            results = (from MyRows in result.AsEnumerable()
                                       where
                                        MyRows.Field<string>("STATUS") == "Scanning"
                                        && MyRows.Field<string>("Invoice_No") == null
                                       select MyRows);
                            if (results.Any())
                            {
                                DataTable filterresult = results.CopyToDataTable();
                                finalTable.Merge(filterresult);
                                finalTable.AcceptChanges();
                            }

                        }
                        if (CheckBoxScanComplete.Checked)
                        {
                            results = (from MyRows in result.AsEnumerable()
                                       where
                                        MyRows.Field<string>("STATUS") == "Scan Complete"
                                        && MyRows.Field<string>("Invoice_No") == null
                                       select MyRows);
                            if (results.Any())
                            {
                                DataTable filterresult = results.CopyToDataTable();
                                finalTable.Merge(filterresult);
                                finalTable.AcceptChanges();
                            }

                        }
                    }
                    else
                    {
                        finalTable = result;
                    }
                    if (chkInvoice.Checked)
                    {
                        results = (from MyRows in result.AsEnumerable()
                                   where
                                    MyRows.Field<string>("Invoice_No") != null  
                                   select MyRows);
                        if (results.Any())
                        {
                            finalTable = results.CopyToDataTable();
                        }  
                        else
                        {
                            finalTable = new DataTable();
                        }
                    }

                }


                if (finalTable.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No Results Found !!!')", true);
                    lblDocCount.Text = "";
                    documentStatusGrid.DataSource = finalTable;
                    documentStatusGrid.DataBind();
                }
                else
                {
                    lblDocCount.Text = "Row Count :" + finalTable.Rows.Count.ToString();

                    documentStatusGrid.DataSource = finalTable;
                    documentStatusGrid.DataBind();

                    itemStatusGrid.DataSource = null;
                    itemStatusGrid.DataBind();
                    lblDoc.Text = "";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmclear.Value == "Yes")
                {
                    Response.Redirect(Request.RawUrl);
                }
                //Clear();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }
        protected void LinkButtonLoc_Click(object sender, EventArgs e)
        {
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            BindUCtrlDDLData(_result);
            Label1.Text = "5";
            UserPopoup.Show();

        }

        protected void documentStatusGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (documentStatusGrid.Rows.Count > 0)
                {
                    Int32 rowindex = documentStatusGrid.SelectedRow.RowIndex;

                    string userSeqNo = (documentStatusGrid.SelectedRow.FindControl("lblUserSeq") as Label).Text;
                    DataTable _result = CHNLSVC.General.GetItemStatusByUserSeq(userSeqNo);
                    if (_result.Rows.Count > 0)
                    {
                        string doc_No = "Doc Number : ";
                        lblDoc.Text = doc_No + (documentStatusGrid.SelectedRow.FindControl("lbldocno") as Label).Text;
                        itemStatusGrid.DataSource = _result;
                        itemStatusGrid.DataBind();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No Items Found !!!')", true);
                        itemStatusGrid.DataSource = null;
                        itemStatusGrid.DataBind();
                        lblDoc.Text = "";
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchbyword.Text))
                {
                    FilterData();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        private void FilterData()
        {
            try
            {
                //if (lblvalue.Text == "429")
                //{
                //    string filter = (string)Session["USER_TYPE"];

                //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProductConditionUpdate);
                //    DataTable result = CHNLSVC.Inventory.LoadProductConditionNew(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text, filter);
                //    grdResult.DataSource = result;
                //    grdResult.DataBind();
                //    lblvalue.Text = "429";
                //    ViewState["SEARCH"] = result;
                //    grdResult.PageIndex = 0;
                //    SIPopup.Show();
                //    txtSearchbyword.Focus();
                //}

                //else if (lblvalue.Text == "1000")
                //{

                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void documentStatusGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[0].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(documentStatusGrid, "Select$" + e.Row.RowIndex);
                    e.Row.Cells[0].Attributes["style"] = "cursor:pointer";

                    DataRowView drv = (DataRowView)e.Row.DataItem;
                    e.Row.Cells[0].Attributes["onclick"] = "ConfirmSelect();";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                return;
            }
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {

            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }
        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.cmbSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.cmbSearchbykey.Items.Add(col.ColumnName);
            }

            this.cmbSearchbykey.SelectedIndex = 0;
        }
        protected void dvResultUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

            if (Label1.Text == "5")
            {
                string Des = dvResultUser.SelectedRow.Cells[2].Text;
                string name = dvResultUser.SelectedRow.Cells[1].Text;
                txtLocation.Text = name;
            }
            if (Label1.Text == "6")
            {
                string name = dvResultUser.SelectedRow.Cells[1].Text;
                //txtDocType.Text = name;
            }

        }
        protected void dvResultUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvResultUser.PageIndex = e.NewPageIndex;
            if (Label1.Text == "5")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
            }
            //if (Label1.Text == "6")
            //{
            //    DataTable _result = CHNLSVC.CommonSearch.GetDocTypes(Session["UserCompanyCode"].ToString(), "%" + txtSearchbyword.Text.ToString());
            //    dvResultUser.DataSource = _result;
            //    dvResultUser.DataBind();
            //}
            UserPopoup.Show();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Label1.Text == "5")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
            }
            //if (Label1.Text == "6")
            //{
            //    DataTable _result = CHNLSVC.CommonSearch.GetDocTypes(Session["UserCompanyCode"].ToString(), "%" + txtSearchbyword.Text.ToString());
            //    dvResultUser.DataSource = _result;
            //    dvResultUser.DataBind();
            //}
            UserPopoup.Show();
        }

        protected void LinkButtonDoctype_Click(object sender, EventArgs e)
        {
            //dvResultUser.DataSource = null;
            //// string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            ////  DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
            //DataTable _result = CHNLSVC.CommonSearch.GetDocTypes(Session["UserCompanyCode"].ToString(), "");
            //dvResultUser.DataSource = _result;
            //dvResultUser.DataBind();
            //BindUCtrlDDLData(_result);
            //Label1.Text = "6";
            //UserPopoup.Show();

        }
    }
}