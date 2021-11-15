using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FF.AbansTours.UserControls
{
    public partial class uc_CommonSearch : System.Web.UI.UserControl
    {
        #region User Defined Properties

        public AjaxControlToolkit.ModalPopupExtender UCModalPopupExtender
        {
            get { return this.ucmdpExtender; }
        }

        public string ReturnResultControl
        {
            get { return this.hdnResultControl.Value; }
            set { this.hdnResultControl.Value = value; }
        }

        public string SearchParams
        {
            get { return this.hdnSearchParams.Value; }
            set { this.hdnSearchParams.Value = value; }
        }

        #endregion User Defined Properties

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {                
                
                txtSearchText.Focus();
                hdnDateEnable.Value = string.Empty;
                txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd-MMM-yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");                
            }
            ClearHiddenFields();
            txtSearchText.Text = string.Empty;
        }

        #region User Defined methods

        public void DateEnable(bool enable)
        {
            pnlDate.Visible = enable;
            if (enable)
                hdnDateEnable.Value = "DATE";
            else
                hdnDateEnable.Value = string.Empty;
        }

        public void BindUCtrlGridData(DataTable _dataSource)
        {
            divResults.InnerHtml = CommonUIOperations.ConvertDataTableToHtml(_dataSource);
        }

        public void BindUCtrlGridData(string _htmlText)
        {
            divResults.InnerHtml = _htmlText;
        }

        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchCatergory.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                if (col.ColumnName == "FROM DATE" || col.ColumnName == "TO DATE")
                {
                    if (this.ddlSearchCatergory.Items.FindByText("DATE") == null)
                    {
                        this.ddlSearchCatergory.Items.Add(new ListItem("DATE", "DATE"));
                    }
                }
                else
                {
                    this.ddlSearchCatergory.Items.Add(new ListItem(col.ColumnName, col.ColumnName));
                }
                //if (!col.ColumnName.Contains("DATE"))
                //{
                //    this.ddlSearchCatergory.Items.Add(new ListItem(col.ColumnName, col.ColumnName));
                //}
            }
        }

        private void ClearHiddenFields()
        {
            hdnResultControl.Value = string.Empty;
            hdnSearchParams.Value = string.Empty;
        }

        #endregion User Defined methods

        protected void txtSearchText_TextChanged(object sender, EventArgs e)
        {
        }
       
    }
}