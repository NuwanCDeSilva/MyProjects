using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Data;
using System.Text;


namespace FF.WebERPClient.General_Modules
{
    public partial class RegistrationAndInsuranceFeeInfo : BasePage
    {

        #region properties

        private DataTable Search_Table {
            get { return (DataTable)ViewState["Search_Table"]; }
            set { ViewState["Search_Table"] = value; }
        }

        private DataTable Search_Table_Main
        {
            get { return (DataTable)ViewState["Search_Table_Main"]; }
            set { ViewState["Search_Table_Main"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                CreateTable();
                CreataMainTable();
                LoadItemGrid(GridViewItems, Search_Table);

                DataTable dt = new DataTable();
                GridViewMainDetails.DataSource = dt;
                GridViewMainDetails.DataBind();
            }
            TextBoxSalesType.Attributes.Add("onKeyup", "return clickButton(event,'" + ImageButtonSalesType.ClientID + "')");
            TextBoxModel.Attributes.Add("onKeyup", "return clickButton(event,'" + ImageButtonModel.ClientID + "')");
            TextBoxTerm.Attributes.Add("onKeyup", "return clickButton(event,'" + ImageButtonTerm.ClientID + "')");
            TextBoxItem.Attributes.Add("onKeyup", "return clickButton(event,'" + ImageButtonItem.ClientID + "')");
        }

        #region table create

        private void CreateTable() {
            Search_Table = new DataTable();
            Search_Table.Columns.Add("Item", typeof(string));
            Search_Table.Columns.Add("Model", typeof(string));
            Search_Table.Columns.Add("Term", typeof(string));
            Search_Table.Columns.Add("Sales_Type", typeof(string));
        }

        private void CreataMainTable() {
            Search_Table_Main = new DataTable();
            Search_Table_Main.Columns.Add("Model", typeof(string));
            Search_Table_Main.Columns.Add("Description", typeof(string));
            Search_Table_Main.Columns.Add("Item", typeof(string));
            Search_Table_Main.Columns.Add("Sales_Type", typeof(string));
            Search_Table_Main.Columns.Add("Tot_Val", typeof(Decimal));
            Search_Table_Main.Columns.Add("Reg_val", typeof(Decimal));
            Search_Table_Main.Columns.Add("Term", typeof(string));
        }

        #endregion

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("../General_Modules/RegistrationAndInsuranceFeeInfo.aspx");
        }

        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

        #region common search buttons
        
        protected void ImageButtonSalesType_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_Type);
            DataTable dataSource = CHNLSVC.General.GetSalesTypes(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxSalesType.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImageButtonModel_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
            DataTable dataSource = CHNLSVC.CommonSearch.GetAllModels(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxModel.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImageButtonTerm_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Insurance_Term);
            DataTable dataSource = CHNLSVC.CommonSearch.GetAllInsTerms(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxTerm.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImageButtonItem_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable dataSource = CHNLSVC.CommonSearch.GetItemSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxItem.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        #endregion

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
                case CommonUIDefiniton.SearchUserControlType.Sales_Type:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Insurance_Term:
                    {
                        paramsText.Append(GlbUserComCode+seperator+""+seperator);
                        break;
                    }
            }
            return paramsText.ToString();
        }
        #endregion

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            DataRow datarow = Search_Table.NewRow();
            if (TextBoxItem.Text != "")
                datarow["Item"] = TextBoxItem.Text.ToUpper();
            else
                datarow["Item"] = "All";
            if (TextBoxModel.Text != "")
                datarow["Model"] = TextBoxModel.Text.ToUpper();
            else
                datarow["Model"] = "All";
            if (TextBoxTerm.Text != "")
                datarow["Term"] = TextBoxTerm.Text.ToUpper();
            else
                datarow["Term"] = "All";
            if (TextBoxSalesType.Text != "")
                datarow["Sales_Type"] = TextBoxSalesType.Text.ToUpper();
            else
                datarow["Sales_Type"] = "All";
            bool result=CheckDataRow(datarow);
            if (result)
            {
                Search_Table.Rows.Add(datarow);
                LoadItemGrid(GridViewItems, Search_Table);
                TextBoxItem.Text = string.Empty;
                TextBoxModel.Text = string.Empty;
                TextBoxSalesType.Text = string.Empty;
                TextBoxTerm.Text = string.Empty;
            }
            else {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Criteria already exists");
            }
        }

        private bool CheckDataRow(DataRow datarow)
        {
            DataRow[] dr = Search_Table.Select("Item= '" + datarow["Item"] + "' And Model= '"+datarow["Model"]+"' And Term= '"+datarow["Term"]+"' And Sales_Type= '"+datarow["Sales_Type"]+"'");
            if (dr.Length <= 0)
                return true;
            else
                return false;
        }

        private void LoadItemGrid(GridView gv, DataTable Search_Table)
        {
            gv.DataSource = Search_Table;
            gv.DataBind(); 
        }

        protected void GridViewItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Search_Table.Rows.RemoveAt(e.RowIndex);
            LoadItemGrid(GridViewItems, Search_Table);
        }


        private void RemoveEqalColumns(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                DataRow[] tem = this.Search_Table_Main.Select("Model= '" + dr["Model"] + "' AND  Description= '" + dr["Description"] + "' AND  Item= '" + dr["Item"] + "' AND Sales_Type= '" + dr["Sales_Type"] + "' AND Tot_Val= '" + dr["Tot_Val"] + "' AND Reg_val= '" + dr["Reg_Val"] + "' ");
                if (tem.Length > 0)
                {
                    foreach (DataRow temdr in tem)
                    {
                        Search_Table_Main.Rows.Remove(temdr);
                    }
                }
            }
        }

        protected void GridViewMainDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadItemGrid(GridViewMainDetails, Search_Table_Main);
            GridViewMainDetails.PageIndex = e.NewPageIndex;
            GridViewMainDetails.DataBind();
        }

        protected void ButtonView_Click(object sender, EventArgs e)
        {
            DataTable temDt = new DataTable();
            LoadItemGrid(GridViewMainDetails, temDt);
            CreataMainTable();

            for (int i = 0; i < GridViewItems.Rows.Count; i++)
            {
                GridViewRow gvRow = GridViewItems.Rows[i];
                bool result = ((CheckBox)gvRow.FindControl("CheckBoxGridSelect")).Checked;
                if (result)
                {
                    string _item = GridViewItems.Rows[i].Cells[1].Text;
                    string _model = GridViewItems.Rows[i].Cells[2].Text;
                    string _term = GridViewItems.Rows[i].Cells[3].Text;
                    string _salestype = GridViewItems.Rows[i].Cells[4].Text;
                    string _pc="";
                    string _com="";

                    _item = (_item != "All") ? (_item.ToUpper()) : "%";
                    _model = (_model != "All") ? (_model.ToUpper()) : "%";
                    _salestype = (_salestype != "All") ? (_salestype.ToUpper()) : "%";

                    if (_term == "All")
                    {
                        _term = "%";
                        _pc = (string.IsNullOrEmpty(GlbUserDefLoca)) ? GlbUserDefProf : GlbUserDefLoca;
                        _com = GlbUserComCode;
                    }
                    else {
                        _term = _term.ToUpper();
                        _pc = (string.IsNullOrEmpty(GlbUserDefLoca)) ? GlbUserDefProf : GlbUserDefLoca;
                        _com = GlbUserComCode;
                    }

                    //get result and merge
                    DataTable dt = CHNLSVC.General.GetRegAndInsSearch(_item, _model, _salestype, _term,_pc,_com);
                    RemoveEqalColumns(dt);
                    Search_Table_Main.Merge(dt, false, MissingSchemaAction.Ignore);
                    LoadItemGrid(GridViewMainDetails, Search_Table_Main);
                }
            }
        }

        protected void GridViewItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadItemGrid(GridViewItems,Search_Table);
            GridViewItems.PageIndex = e.NewPageIndex;
            GridViewItems.DataBind();
        }
    }
}