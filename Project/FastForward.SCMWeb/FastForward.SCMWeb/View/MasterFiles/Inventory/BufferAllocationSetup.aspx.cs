using FF.BusinessObjects;
using FF.BusinessObjects.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.MasterFiles.Inventory
{
    public partial class BufferAllocationSetup : BasePage
    {
        protected List<MasterBufferChannel> _MasterBufferChannel { get { return (List<MasterBufferChannel>)Session["_MasterBufferChannel"]; } set { Session["_MasterBufferChannel"] = value; } }
        protected List<InventoryLocation> _InventoryLocation { get { return (List<InventoryLocation>)Session["_InventoryLocation"]; } set { Session["_InventoryLocation"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageClear();
                pageClear_2();
            }
        }

        private void pageClear()
        {
            txtCompany.Text = string.Empty;
            txtChannel.Text = string.Empty;
            ddlgrade.SelectedIndex = -1;
            ddlseason.SelectedIndex = -1;
            txtItemcode.Text = string.Empty;
            txtBQty.Text = string.Empty;
            lblName.Text = string.Empty;
            lblchanel.Text = string.Empty;
            lblItem.Text = string.Empty;
            chkIsExcelupload.Checked = false;
            LoadItemStatus();
            _MasterBufferChannel = new List<MasterBufferChannel>();
            Session["_itemSerializedStatus"] = "";
            LoadSeason();
            LoadGrade();

            txtCompany.Enabled = true;
            txtChannel.Enabled = true;
            ddlgrade.SelectedIndex = -1;
            ddlseason.SelectedIndex = -1;
            txtItemcode.Enabled = true;
            txtBQty.Enabled = true;
            lblName.Enabled = true;
            lblchanel.Enabled = true;
            lblItem.Enabled = true;
            ROLText.Text = "";
        }
        private void pageClear_2()
        {
            txtLcompany.Text = string.Empty;
            lblName_2.Text = string.Empty;
            txtlocation.Text = string.Empty;
            lblLocation.Text = string.Empty;
           // ddlStatusLoc.SelectedIndex = -1;
            txtLitemCode.Text = string.Empty;
            lblLoItemcode.Text = string.Empty;
            txtQtyLoc.Text = string.Empty;
            txtBQty.Text = string.Empty;
            LoadItemStatus();
            _InventoryLocation = new List<InventoryLocation>();
            Session["_itemSerializedStatus"] = "";

            txtLcompany.Enabled = true;
            lblName_2.Enabled = true;
            txtlocation.Enabled = true;
            lblLocation.Enabled = true;
            txtLitemCode.Enabled = true;
            lblLoItemcode.Enabled = true;
            txtQtyLoc.Enabled = true;
            ROLText.Text = "";
        }
        private void LoadItemStatus()
        {
            DataTable oItemStaus = CHNLSVC.General.GetItemStatusByCom(Session["UserCompanyCode"].ToString());

            if (oItemStaus != null && oItemStaus.Rows.Count > 0)
            {

                ddlStatus.DataSource = oItemStaus;
                ddlStatus.DataTextField = "mis_desc";
                ddlStatus.DataValueField = "Mis_cd";
                ddlStatus.DataBind();
                ddlStatus.SelectedValue = "GOD";

                ddlStatusLoc.DataSource = oItemStaus;
                ddlStatusLoc.DataTextField = "mis_desc";
                ddlStatusLoc.DataValueField = "Mis_cd";
                ddlStatusLoc.DataBind();
                ddlStatusLoc.SelectedValue = "GOD";
            }
        }
        private void LoadSeason()
        {
            DataTable oItemStaus = CHNLSVC.General.getSeason(Session["UserCompanyCode"].ToString());

            if (oItemStaus != null && oItemStaus.Rows.Count > 0)
            {

                ddlseason.DataSource = oItemStaus;
                ddlseason.DataTextField = "msa_desc";
                ddlseason.DataValueField = "msa_cd";
                ddlseason.DataBind();
               // ddlseason.Items.Insert(0, new ListItem("---Select---", ""));
///ddlseason.SelectedIndex = 0;
            }
        }
        private void LoadGrade()
        {
            GradeMaster _grad = new GradeMaster();
            _grad.Mg_com = txtCompany.Text.ToUpper();
            _grad.Mg_chnl = txtChannel.Text.ToUpper();

            if (_grad.Mg_com=="")
            {
                ddlgrade.Items.Insert(0, new ListItem("---Select---", ""));
                ddlgrade.SelectedIndex = 0;
                return;
            }
            DataTable oItemStaus = CHNLSVC.CommonSearch.SearchMstGradeTypes(_grad);

            if (oItemStaus != null && oItemStaus.Rows.Count > 0)
            {

                ddlgrade.DataSource = oItemStaus;
                ddlgrade.DataTextField = "mg_desc";
                ddlgrade.DataValueField = "mg_cd";
                ddlgrade.DataBind();
            }
            else
            {
                ddlgrade.Items.Insert(0, new ListItem("Select", ""));
                ddlgrade.SelectedIndex = 0;
            }
        }
        private void DisplayMessage(String _err, Int32 option)
        {
            string Msg = _err.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + Msg + "');", true);
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
                        paramsText.Append(Session["UserID"].ToString() + seperator + txtLcompany.Text+ seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel:
                    {
                        paramsText.Append(txtCompany.Text.ToUpper() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel.ToString() + seperator);
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
        #region Modalpopup

        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }
        private void FilterData()
        {
            if (lblvalue.Text == "Location")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString()); 
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                UserPopup.Show();
            }
            if ((lblvalue.Text == "Company") || (lblvalue.Text == "Company_2"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString()); 
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopup.Show();
                return;
            }
            if (lblvalue.Text == "Channel")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString()); 
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "Channel";
                UserPopup.Show();
                return;
            }
            if ((lblvalue.Text == "Item") || (lblvalue.Text == "Item_2"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString()); 
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopup.Show();
                return;
            }
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                string Name = grdResult.SelectedRow.Cells[1].Text;
                if (lblvalue.Text == "Location")
                {
                    txtlocation.Text = Name;

                    lblLocation.Text = grdResult.SelectedRow.Cells[2].Text;
                }
                if (lblvalue.Text == "Company")
                {
                    txtCompany.Text = Name;
                  
                    lblName.Text = grdResult.SelectedRow.Cells[2].Text;
                }
                if (lblvalue.Text == "Company_2")
                {
                    txtLcompany.Text = Name;

                    lblName_2.Text = grdResult.SelectedRow.Cells[2].Text;
                }
                if (lblvalue.Text == "Channel")
                {
                    txtChannel.Text = Name;
                    lblchanel.Text = grdResult.SelectedRow.Cells[2].Text;
                    LoadGrade();
                }
                if (lblvalue.Text == "Item")
                {
                    txtItemcode.Text = Name;
                    lblItem.Text = grdResult.SelectedRow.Cells[2].Text;
                }
                if (lblvalue.Text == "Item_2")
                {
                    txtLitemCode.Text = Name;
                    lblLoItemcode.Text = grdResult.SelectedRow.Cells[2].Text;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;
            grdResult.PageIndex = 1;
            try
            {
                if (lblvalue.Text == "Location")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    BindUCtrlDDLData(_result);
                    lblvalue.Text = "Location";
                    UserPopup.Show();

                }
                if (lblvalue.Text == "Company")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "Company";
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Company_2")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "Company_2";
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Channel")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                    DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, null, null);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "Channel";
                    UserPopup.Show();
                    return;
                }
                if (lblvalue.Text == "Item")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "Item";
                    UserPopup.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            txtSearchbyword.Text = "";
            UserPopup.Hide();
            lblvalue.Text = "";
        }

        #endregion
        protected void lbtnclear_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "Yes")
            {
                try
                {
                    try
                    {
                        Response.Redirect(Request.RawUrl, false);
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                    }
                }
                catch (Exception ex)
                {
                    //divalert.Visible = true;
                    DisplayMessage(ex.Message, 4);
                }
            }
        }
        #region tab one
        protected void lbtncompany_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Company";
                UserPopup.Show();
            }

            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtCompany_TextChanged(object sender, EventArgs e)
        {
            if (txtCompany.Text.Trim() != "")
            {
                //TODO: LOAD COMPANY DESCRIPTION
                MasterCompany com = CHNLSVC.General.GetCompByCode(txtCompany.Text.ToUpper().Trim());
                if (com == null)
                {
                    DisplayMessage("Invalid Company", 2);
                    txtCompany.Text = "";
                    lblName.Text = "";
                    txtCompany.Focus();
                    return;
                }
                else
                {
                    lblName.Text = com.Mc_desc;
                }
            }
        }

        protected void lblChannel_Click(object sender, EventArgs e)
        {
           string  SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
           DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, null, null);
           grdResult.DataSource = _result;
           grdResult.DataBind();
           BindUCtrlDDLData(_result);
           lblvalue.Text = "Channel";
           UserPopup.Show();
        }

        protected void txtChannel_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCompany.Text))
            {
                DisplayMessage("Please enter company.", 2);
                txtChannel.Text = "";
                lblchanel.Text = "";
                txtCompany.Focus();
                return;
            }
            DataTable _chnl = CHNLSVC.Sales.GetChanelData(txtCompany.Text.ToUpper().Trim(), txtChannel.Text.ToUpper().Trim().ToUpper());
            //(!CHNLSVC.General.CheckChannel(txtCompany.Text.ToUpper().Trim(), txtChannel.Text.ToUpper().Trim().ToUpper()))
            if (_chnl.Rows.Count==0)
            {              
                DisplayMessage("Please check the channel.", 2);
                txtChannel.Text = "";
                lblchanel.Text = "";
                txtChannel.Focus();
                return;
            }
            lblchanel.Text = _chnl.Rows[0][2].ToString();
            LoadGrade();
        }

        protected void lbtnItemCode_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            BindUCtrlDDLData(result);
            lblvalue.Text = "Item";
            UserPopup.Show();
        }

        protected void txtItemcode_TextChanged(object sender, EventArgs e)
        {
            if (!LoadItemDetail(txtItemcode.Text.ToUpper(),1))
            {
                DisplayMessage("Please check the item code", 1);
                txtItemcode.Text = "";
                lblItem.Text = "";
                txtItemcode.Focus();
            }
        }

        private bool LoadItemDetail(string _item,int option)
        {
            bool _isValid = false;
            MasterItem _itemdetail = new MasterItem();
             if (!string.IsNullOrEmpty(_item))
            {
                _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
            }
             if (_itemdetail != null && !string.IsNullOrEmpty(_itemdetail.Mi_cd))
             {
                 _isValid = true;
                 if (option == 1)
                 {
                     lblItem.Text = _itemdetail.Mi_longdesc;
                     Session["_itemSerializedStatus"] = _itemdetail.Mi_is_ser1;
                 }
                 else if (option == 2)
                 {
                     lblLoItemcode.Text = _itemdetail.Mi_longdesc;
                     Session["_itemSerializedStatus"] = _itemdetail.Mi_is_ser1;
                 }
             }
             else _isValid = false;
             return _isValid;

        }
        #region Excel Upload
        protected void lbtnexClose_Click(object sender, EventArgs e)
        {
            chkIsExcelupload.Checked = false;
            chkLoc.Checked = false;
        }
        protected void btnupload_Click(object sender, EventArgs e)
        {
          
            if (fileupexcelupload.HasFile)
            {
                string FileName = Path.GetFileName(fileupexcelupload.PostedFile.FileName);
                string Extension = Path.GetExtension(fileupexcelupload.PostedFile.FileName);

                if (Extension != ".xls" && Extension != ".XLS" && Extension != ".xlsx" && Extension != ".XLSX")
                {

                    Label3.Visible = true;
                    Label3.Text = "Please select a valid excel (.xls) file";
                    DisplayMessage("Please select a valid excel (.xls) file", 2);
                    excelUpload.Show();
                    return;
                }
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                //LoadData(FolderPath + FileName);
                string FilePath = Server.MapPath(FolderPath + FileName);
                fileupexcelupload.SaveAs(FilePath);
                Session["FilePath"] = FilePath;
                string value=string.Empty;
                ExcelProcess(FilePath,1,out value);
                if (value == "1")
                {
                    DisplayMessage("Excel file upload completed. Please save data", 1);
                    Label3.Visible = false;
                    lblsuccess2.Visible = true;
                    lblsuccess2.Text = "Excel file upload completed. Please save data";
                    pnlupload.Visible = false;
                    excelUpload.Show();
                }
                else if (value == "2")
                {
                    DisplayMessage("This excel sheet contains empty values please check again", 1);
                    lblsuccess2.Visible = false;
                    Label3.Visible = true;
                    Label3.Text = "This excel sheet contains empty values please check again";
                   
                    excelUpload.Show();
                
                }
                else if (value == "3")
                {
                    DisplayMessage("Excel  Data Invalid Please check Excel File and Upload", 1);
                    lblsuccess2.Visible = false;
                    Label3.Visible = true;
                    Label3.Text = "Excel  Data Invalid Please check Excel File and Upload";
                  
                    excelUpload.Show();
                }
                else if (value == "4")
                {
                    DisplayMessage("You can not upload minus and zero values", 1);
                    lblsuccess2.Visible = false;
                    Label3.Visible = true;
                    Label3.Text = "You can not upload minus and zero values";

                    excelUpload.Show();
                }
                else if (value == "5")
                {
                    DisplayMessage("Excel Data Item Code is invalid", 1);
                    lblsuccess2.Visible = false;
                    Label3.Visible = true;
                    Label3.Text = "Excel  Data Invalid Please check Excel File and Upload";

                    excelUpload.Show();
                }
                else if (value == "6")
                {
                    DisplayMessage("Excel Data cannot add minus values for buffer level", 1);
                    lblsuccess2.Visible = false;
                    Label3.Visible = true;
                    Label3.Text = "Excel  Data Invalid Please check Excel File and Upload";

                    excelUpload.Show();
                }
                else if (value == "7")
                {
                    DisplayMessage("Excel  Data cannot add minus values for re order level", 1);
                    lblsuccess2.Visible = false;
                    Label3.Visible = true;
                    Label3.Text = "Excel  Data Invalid Please check Excel File and Upload";

                    excelUpload.Show();
                }
                else
                {

                    DisplayMessage("Excel contains duplicate records please check", 2);
                    Label3.Visible = true;
                    Label3.Text = "Excel contains duplicate records please check";
                    excelUpload.Show();
                }
            }
            else
            {
                DisplayMessage("Please select the correct upload file path", 2);
                Label3.Visible = true;
                Label3.Text = "Please select the correct upload file path";
                excelUpload.Show();

            }
        }
        public string ConnectionString(string FileName, string Header)
        {
            OleDbConnectionStringBuilder Builder = new OleDbConnectionStringBuilder();
            if (System.IO.Path.GetExtension(FileName).ToUpper() == ".XLS")
            {
                Builder.Provider = "Microsoft.Jet.OLEDB.4.0";
                Builder.Add("Extended Properties", string.Format("Excel 8.0;IMEX=1;HDR={0};", Header));
            }
            else
            {
                Builder.Provider = "Microsoft.ACE.OLEDB.12.0";
                Builder.Add("Extended Properties", string.Format("Excel 12.0;IMEX=1;HDR={0};", Header));
            }

            Builder.DataSource = FileName;

            return Builder.ConnectionString;
        }
        public DataTable[] LoadData(string FileName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DataTable Tax = new DataTable();

            using (OleDbConnection cn = new OleDbConnection { ConnectionString = ConnectionString(FileName, "No") })
            {
                cn.Open();
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dtExcelSchema;
                cmdExcel.Connection = cn;

                dtExcelSchema = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                cn.Close();

                //Read Data from First Sheet
                cn.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(Tax);


                return new DataTable[] { Tax };
            }
        }
        private void ExcelProcess(string FilePath,int option,out string value)
        {
            DataTable[] GetExecelTbl = LoadData(FilePath);
            if (GetExecelTbl != null)
            {
                if (GetExecelTbl[0].Rows.Count > 0)
                {
                    if (Session["chkvalue"].ToString()=="1")
                    {
                        // _lstTaxDet = ViewState["_lstTaxDet"] as List<mst_itm_tax_structure_det>;
                        for (int i = 1; i < GetExecelTbl[0].Rows.Count; i++)
                        {
                            try
                            {
                                MasterBufferChannel _Buffer = new MasterBufferChannel();
                                _Buffer.MBC_ACT = 1;
                                _Buffer.MBC_CHNL = GetExecelTbl[0].Rows[i][1].ToString();
                                _Buffer.MBC_COM = GetExecelTbl[0].Rows[i][0].ToString();
                                _Buffer.MBC_CRE_BY = Session["UserID"].ToString();
                                _Buffer.MBC_CRE_SESSION = Session["SessionID"].ToString();
                                _Buffer.MBC_GRADE = GetExecelTbl[0].Rows[i][2].ToString();
                                _Buffer.MBC_ITM_CD = GetExecelTbl[0].Rows[i][4].ToString();
                                _Buffer.MBC_ITM_STUS = GetExecelTbl[0].Rows[i][5].ToString();
                                _Buffer.MBC_SEASON = GetExecelTbl[0].Rows[i][3].ToString();
                                _Buffer.MBC_MOD_BY = Session["UserID"].ToString();
                                _Buffer.MBC_QTY = Convert.ToInt32(GetExecelTbl[0].Rows[i][6].ToString());
                                _Buffer.MBC_MOD_SESSION = Session["SessionID"].ToString();
                                var _duplicate = _MasterBufferChannel.Where(x => x.MBC_COM == _Buffer.MBC_COM && x.MBC_CHNL == _Buffer.MBC_CHNL
                                    && x.MBC_GRADE == _Buffer.MBC_GRADE && x.MBC_SEASON == _Buffer.MBC_SEASON && x.MBC_ITM_CD == _Buffer.MBC_ITM_CD
                                    && x.MBC_ITM_STUS == _Buffer.MBC_ITM_STUS).ToList();

                                if (_duplicate.Count >0)
                                {
                                    value = "0";
                                    return;
                                }
                                else if ((_Buffer.MBC_CHNL == "")||(_Buffer.MBC_CHNL == null))
                                {
                                    value = "2";
                                    return;
                                }
                                else if ((_Buffer.MBC_COM == "") || (_Buffer.MBC_COM == null))
                                {
                                    value = "2";
                                    return;
                                }
                                else if ((_Buffer.MBC_GRADE == "") || (_Buffer.MBC_GRADE == null))
                                {
                                    value = "2";
                                    return;
                                }
                                else if ((_Buffer.MBC_ITM_CD == "") || (_Buffer.MBC_ITM_CD == null))
                                {
                                    value = "2";
                                    return;
                                }
                                else if ((_Buffer.MBC_ITM_STUS == "") || (_Buffer.MBC_ITM_STUS == null))
                                {
                                    value = "2";
                                    return;
                                }
                                else if ((_Buffer.MBC_QTY < 0) )
                                {
                                    value = "4";
                                    return;
                                } 
                                else
                                {
                                    _MasterBufferChannel.Add(_Buffer);
                                }
                               

                            }
                            catch (Exception ex)
                            {
                                DisplayMessage("Excel  Data Invalid Please check Excel File and Upload ", 2);
                                Label3.Visible = true;
                                Label3.Text = "Excel  Data Invalid Please check Excel File and Upload";
                                excelUpload.Show();
                                value = "3";
                                return;
                            }

                        }

                    }
                    else
                    {
                        for (int i = 1; i < GetExecelTbl[0].Rows.Count; i++)
                        {
                            try
                            {
                                MasterItem _mI = new MasterItem();
                                if(!(GetExecelTbl[0].Rows[i][2].ToString().Equals(string.Empty)))
                                {
                                    _mI = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), GetExecelTbl[0].Rows[i][2].ToString());
                                    if(_mI==null)
                                    {
                                        value = "5";
                                        return;
                                    }
                                }
                                if (!(GetExecelTbl[0].Rows[i][4].ToString().Equals(string.Empty)))
                                {
                                    decimal val = Convert.ToDecimal(GetExecelTbl[0].Rows[i][4].ToString());
                                    if (val < 0)
                                    {
                                        value = "6";
                                        return;
                                    }
                                }
                                if (!(GetExecelTbl[0].Rows[i][4].ToString().Equals(string.Empty)))
                                {
                                    decimal val = Convert.ToDecimal(GetExecelTbl[0].Rows[i][4].ToString());
                                    if (val < 0)
                                    {
                                        value = "7";
                                        return;
                                    }                                  
                                }

                                InventoryLocation _loc = new InventoryLocation();
                                _loc.Inl_com = GetExecelTbl[0].Rows[i][0].ToString();
                                _loc.Inl_loc = GetExecelTbl[0].Rows[i][1].ToString();
                                _loc.Inl_itm_cd = GetExecelTbl[0].Rows[i][2].ToString();
                                _loc.Inl_itm_stus = GetExecelTbl[0].Rows[i][3].ToString();
                                _loc.Inl_bl_qty = Convert.ToDecimal(GetExecelTbl[0].Rows[i][4].ToString());
                                _loc.Inl_ro_qty = Convert.ToDecimal(GetExecelTbl[0].Rows[i][5].ToString());
                                var _duplicate = _InventoryLocation.Where(x => x.Inl_com == _loc.Inl_com && x.Inl_loc == _loc.Inl_loc
                                  && x.Inl_itm_cd == _loc.Inl_itm_cd
                                  && x.Inl_itm_stus == _loc.Inl_itm_stus).ToList();

                              

                                if (_duplicate.Count > 0)
                                {
                                    value = "0";
                                    return;
                                }
                                else if ((_loc.Inl_com == "") || (_loc.Inl_com == null))
                                {
                                    value = "2";
                                    return;
                                }
                                else if ((_loc.Inl_loc == "") || (_loc.Inl_loc == null))
                                {
                                    value = "2";
                                    return;
                                }
                                else if ((_loc.Inl_itm_cd == "") || (_loc.Inl_itm_cd == null))
                                {
                                    value = "2";
                                    return;
                                }
                                else if ((_loc.Inl_itm_stus == "") || (_loc.Inl_itm_stus == null))
                                {
                                    value = "2";
                                    return;
                                }
                                else if ((_loc.Inl_bl_qty < 0))
                                {
                                    value = "4";
                                    return;
                                } 
                                else
                                {
                                    _InventoryLocation.Add(_loc);
                                }
                            }
                            catch (Exception ex)
                            {
                                DisplayMessage("Excel  Data Invalid Please check Excel File and Upload ", 2);
                                Label3.Visible = true;
                                Label3.Text = "Excel  Data Invalid Please check Excel File and Upload";
                                excelUpload.Show();
                                value = "3";
                                return;
                            }

                        }
                    }
                   // ViewState["_lstTaxDet"] = _lstTaxDet;

                }
            }
            value = "1";
        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSavelconformmessageValue.Value == "No")
            {
                return;
            }
            int rowsAffected=0;
            string err = string.Empty;

            //if (chkIsExcelupload.Checked == false)
            if (_MasterBufferChannel.Count==0)
            {
                if (string.IsNullOrEmpty(txtCompany.Text))
                {
                    DisplayMessage("Please enter company", 2);
                    return;
                }
                if (string.IsNullOrEmpty(txtChannel.Text))
                {
                    DisplayMessage("Please enter channel", 2);
                    return;
                }
                if (string.IsNullOrEmpty(txtItemcode.Text))
                {
                    DisplayMessage("Please enter item code", 2);
                    return;
                }
                if (string.IsNullOrEmpty(txtBQty.Text))
                {
                    DisplayMessage("Please enter qty", 2);
                    return;
                }
               
                if (_MasterBufferChannel.Count==0)
                {
                    MasterBufferChannel _Buffer = new MasterBufferChannel();
                    _Buffer.MBC_ACT = 1;
                    _Buffer.MBC_CHNL = txtChannel.Text;
                    _Buffer.MBC_COM = txtCompany.Text;
                    _Buffer.MBC_CRE_BY = Session["UserID"].ToString();
                    _Buffer.MBC_CRE_SESSION = Session["SessionID"].ToString();
                    _Buffer.MBC_GRADE = ddlgrade.SelectedValue;
                    _Buffer.MBC_ITM_CD = txtItemcode.Text;
                    _Buffer.MBC_ITM_STUS = ddlStatus.SelectedValue;
                    _Buffer.MBC_SEASON = ddlseason.SelectedValue;
                    _Buffer.MBC_MOD_BY = Session["UserID"].ToString();
                    _Buffer.MBC_QTY = Convert.ToInt32(txtBQty.Text);
                    _Buffer.MBC_MOD_SESSION = Session["SessionID"].ToString();
                    _MasterBufferChannel.Add(_Buffer);
                }
            }

            if (_MasterBufferChannel.Count == 0)
            {
                DisplayMessage("No data found for save", 2);
            }

            rowsAffected = CHNLSVC.Inventory.SaveBufferLevel(_MasterBufferChannel, out err);
            if (rowsAffected != -1)
            {
                string Msg = "Successfully saved. ";
                DisplayMessage(Msg, 3);
                pageClear();
            }
            else
            {
                string msg =  "Data Invalid Please check";
                DisplayMessage(msg, 4);
            }
        }

        protected void chkIsExcelupload_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsExcelupload.Checked) {
                lblsuccess2.Visible = false;
                Label3.Visible = false;
               // pnlexcel.Visible=true;
                txtCompany.Enabled=false;
                txtChannel.Enabled = false; 
                ddlgrade.SelectedIndex = -1;
                ddlseason.SelectedIndex = -1;
                txtItemcode.Enabled = false;
                txtBQty.Enabled = false;
                lblName.Enabled = false;
                lblchanel.Enabled = false;
                lblItem.Enabled = false;
                Hddnvalue.Value = "1";
                Session["chkvalue"] = "1";
                chkLoc.Checked = false;
                pnlupload.Visible = true;
                DisplayMessage("Please unselect blank cell/cells which you have selected in excel sheet", 1);
                excelUpload.Show();
            }else{
                //pnlexcel.Visible = false;
                txtCompany.Enabled = true;
                txtChannel.Enabled = true;
                ddlgrade.SelectedIndex = -1;
                ddlseason.SelectedIndex = -1;
                txtItemcode.Enabled = true;
                txtBQty.Enabled = true;
                lblName.Enabled = true;
                lblchanel.Enabled = true;
                lblItem.Enabled = true;
                chkLoc.Checked = false;

            }

            txtCompany.Text = string.Empty;
            txtChannel.Text = string.Empty;
            ddlgrade.SelectedIndex = -1;
            ddlseason.SelectedIndex = -1;
            txtItemcode.Text = string.Empty;
            txtBQty.Text = string.Empty;
            lblName.Text = string.Empty;
            lblchanel.Text = string.Empty;
            lblItem.Text = string.Empty;

            _MasterBufferChannel = new List<MasterBufferChannel>();

        }

        protected void txtBQty_TextChanged(object sender, EventArgs e)
        {
            string _itemSerializedStatus = Session["_itemSerializedStatus"].ToString();
            if ((_itemSerializedStatus == "1") || (_itemSerializedStatus == "0"))
            {
                decimal number = Convert.ToDecimal(txtBQty.Text);
                decimal fractionalPart = number % 1;
                if (fractionalPart != 0)
                {
                    DisplayMessage("only allow numeric value", 2);
                    return;
                }


            }
            if (Convert.ToDecimal(txtBQty.Text.Trim()) < 0)
            {
                DisplayMessage("Quantity should be positive value.",2);
                return;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "No") { return; }
            pageClear();
        }

#endregion

        #region second tab
        protected void lbtnlocompany_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Company_2";
                UserPopup.Show();
            }

            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtLcompany_TextChanged(object sender, EventArgs e)
        {
            if (txtLcompany.Text.Trim() != "")
            {
                //TODO: LOAD COMPANY DESCRIPTION
                MasterCompany com = CHNLSVC.General.GetCompByCode(txtLcompany.Text.ToUpper().Trim());
                if (com == null)
                {
                    DisplayMessage("Invalid Company", 2);
                    txtLcompany.Text = "";
                    lblName_2.Text = "";
                    txtCompany.Focus();
                    return;
                }
                else
                {
                    lblName_2.Text = com.Mc_desc;
                }
            }
        }

        protected void lbtnLocation_Click(object sender, EventArgs e)
        {
            if (txtLcompany.Text.Trim() == "")
            {
                DisplayMessage("Enter Company Code", 2);
                txtLcompany.Focus();
                return;
            }
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Location";
                UserPopup.Show();
            }

            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtlocation_TextChanged(object sender, EventArgs e)
        {

            if (txtLcompany.Text.Trim() == "")
            {
                DisplayMessage("Enter Company Code",2);
                txtLcompany.Focus();
                return;
            }
            try
            {
                DataTable _tbl = CHNLSVC.Inventory.Get_location_by_code(txtLcompany.Text.ToUpper().Trim(), txtlocation.Text.ToUpper().Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    DisplayMessage("Please select a valid location.", 2);                  
                    txtlocation.Text = "";
                    lblLocation.Text = "";
                    txtlocation.Focus();
                    return;
                }             
                else
                {
                    lblLocation.Text=_tbl.Rows[0][3].ToString();
                    //TODO: Load description of the PC - ucLCSE001
                }
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }


        protected void lbtnLoItemCode_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            BindUCtrlDDLData(result);
            lblvalue.Text = "Item_2";
            UserPopup.Show();
        }

        protected void txtLitemCode_TextChanged(object sender, EventArgs e)
        {
            if (!LoadItemDetail(txtLitemCode.Text.ToUpper(),2))
            {
                DisplayMessage("Please check the item code", 1);
                txtLitemCode.Text = "";
                lblLoItemcode.Text = "";
                txtLitemCode.Focus();
            }
        }
        protected void txtQtyLoc_TextChanged(object sender, EventArgs e)
        {
            string _itemSerializedStatus = Session["_itemSerializedStatus"].ToString();
            if ((_itemSerializedStatus == "1") || (_itemSerializedStatus == "0"))
            {
                decimal number = Convert.ToDecimal(txtQtyLoc.Text);
                decimal fractionalPart = number % 1;
                if (fractionalPart != 0)
                {
                    DisplayMessage("only allow numeric value", 2);
                    return;
                }


            }
            if(!(txtBQty.Text.Equals(string.Empty)))
            {
            if (Convert.ToDecimal(txtQtyLoc.Text.Trim()) < 0)
            {
                DisplayMessage("Quantity should be positive value.", 2);
                txtQtyLoc.Text = string.Empty;
                return;
            }
            }
        }
           protected void txtreorderLoc_TextChanged(object sender, EventArgs e)
        {
            string _itemSerializedStatus = Session["_itemSerializedStatus"].ToString();
            if ((_itemSerializedStatus == "1") || (_itemSerializedStatus == "0"))
            {
                decimal number = Convert.ToDecimal(ROLText.Text);
                decimal fractionalPart = number % 1;
                if (fractionalPart != 0)
                {
                    DisplayMessage("only allow numeric value", 2);
                    return;
                }


            }
            if(!(ROLText.Text.Equals(string.Empty)))
            {
            if (Convert.ToDecimal(ROLText.Text.Trim()) < 0)
            {
                DisplayMessage("Re order level should be positive value.", 2);
                ROLText.Text = string.Empty;
                return;
            }
            }
        }
        protected void btnClear2_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "No") { return; }
            pageClear_2();
        }


        protected void chkLoc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLoc.Checked)
            {
                lblsuccess2.Visible = false;
                Label3.Visible = false;
               // pnlexcelLoc.Visible = true;
                txtLcompany.Enabled = false;
                lblName_2.Enabled = false;
                txtlocation.Enabled = false;
                lblLocation.Enabled = false;
                ddlStatusLoc.SelectedIndex = -1;
                txtLitemCode.Enabled = false;
                lblLoItemcode.Enabled = false;
                txtQtyLoc.Enabled = false;
                Hddnvalue.Value = "2";
                Session["chkvalue"] = "2";
                pnlupload.Visible = true;
                chkIsExcelupload.Checked = false;
                pnlupload.Visible = true;
                DisplayMessage("Please unselect blank cell/cells which you have selected in excel sheet", 1);
                excelUpload.Show();
                
            }
            else
            {
                chkIsExcelupload.Checked = false;
               // pnlexcelLoc.Visible = false;
                lblsuccess2.Visible = false;
                Label3.Visible = false;
                txtLcompany.Enabled = true;
                lblName_2.Enabled = true;
                txtlocation.Enabled = true;
                lblLocation.Enabled = true;
                ddlStatusLoc.SelectedIndex = -1;
                txtLitemCode.Enabled = true;
                lblLoItemcode.Enabled = true;
                txtQtyLoc.Enabled = true;
                Hddnvalue.Value = "";
                pnlupload.Visible = true;
            }

            txtLcompany.Text = string.Empty;
            lblName_2.Text = string.Empty;
            txtlocation.Text = string.Empty;
            lblLocation.Text = string.Empty;
            ddlStatusLoc.SelectedIndex = -1;
            txtLitemCode.Text = string.Empty;
            lblLoItemcode.Text = string.Empty;
            txtQtyLoc.Text = string.Empty;
            _MasterBufferChannel = new List<MasterBufferChannel>();

        }

      
        protected void btnSave_loc_Click(object sender, EventArgs e)
        {
            if (txtSavelconformmessageValue.Value == "No")
            {
                return;
            }
            int rowsAffected = 0;
            string err = string.Empty;

            //if (chkLoc.Checked == false)
            if (_InventoryLocation.Count==0)
            {
                if (string.IsNullOrEmpty(txtLitemCode.Text))
                {
                    DisplayMessage("Please enter item code", 2);
                    return;
                }
                if (string.IsNullOrEmpty(txtlocation.Text))
                {
                    DisplayMessage("Please enter location code", 2);
                    return;
                }
                if (string.IsNullOrEmpty(txtQtyLoc.Text))
                {
                    DisplayMessage("Please enter qty", 2);
                    return;
                }
                if (string.IsNullOrEmpty(ROLText.Text))
                {
                    DisplayMessage("Please enter Re order level", 2);
                    return;
                }
                if (_InventoryLocation.Count == 0)
                {
                    InventoryLocation _loc = new InventoryLocation();
                    _loc.Inl_com = txtLcompany.Text.ToUpper();
                    _loc.Inl_loc = txtlocation.Text.ToUpper();
                    _loc.Inl_itm_cd = txtLitemCode.Text.ToUpper();
                    _loc.Inl_itm_stus = ddlStatusLoc.SelectedValue;
                    _loc.Inl_bl_qty = Convert.ToDecimal(txtQtyLoc.Text);
                    _loc.Inl_ro_qty = Convert.ToDecimal(ROLText.Text);
                    _loc.Inl_mod_by = Session["UserID"].ToString();

                    _InventoryLocation.Add(_loc);
                }
            }

            if (_InventoryLocation.Count == 0)
            {
                DisplayMessage("No data found for save", 2);

            }

            rowsAffected = CHNLSVC.Inventory.saveInrLocation(_InventoryLocation, out err);
            if (rowsAffected == 1)
            {
                string Msg = "Successfully saved. ";
                DisplayMessage(Msg, 3);
                pageClear_2();
            }
            else
            {
                string msg = "Data Invalid Please check";
                DisplayMessage(msg, 4);
            }
        }

        #endregion
    }
}