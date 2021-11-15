using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace FF.WebERPClient.UserControls
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

      
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtSearchText.Text = string.Empty;                              
            }
            ClearHiddenFields();
            
        }


        #region User Defined methods
      
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
                this.ddlSearchCatergory.Items.Add(new ListItem(col.ColumnName, col.ColumnName));
            }         
        }


        private void ClearHiddenFields()
        {
            hdnResultControl.Value = string.Empty;
            hdnSearchParams.Value = string.Empty;
        }


        #endregion

      


    }
}