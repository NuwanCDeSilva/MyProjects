using FF.BusinessObjects;
using FastForward.SCMWeb.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.UserControls
{
    public partial class ucLoactionSearch : System.Web.UI.UserControl
    {
        private bool _isDisplayRawData = false;
        Base _basePage;
        private bool _isAllLocation = false;
       // private bool _ischeckLocationValidation = true;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]

        #region Propert2


        public Panel PnlHeading
        {
            get { return pnlHeading; }
            set { pnlHeading = value; }
        }
        public Label LblHeading
        {
            get { return lblFormName; }
            set { lblFormName = value; }
        }
        public Label LblLocation
        {
            get { return lblLocation; }
            set { lblLocation = value; }
        }
        public TextBox txtCompany
        {
            get { return TextBoxCompany; }
            set { TextBoxCompany = value; }
        }
         public LinkButton lbtnCompany
        {
            get { return ImgBtnAccountNo; }
            set { ImgBtnAccountNo = value; }
        }
         public TextBox txtChanel
         {
             get { return TextBoxChannel; }
             set { TextBoxChannel = value; }
         }
         public LinkButton lbtnChanel
         {
             get { return imgChaSearch; }
             set { imgChaSearch = value; }
         }
         public TextBox txtSubChanel
         {
             get { return TextBoxSubChannel; }
             set { TextBoxSubChannel = value; }
         }
         public LinkButton lbtnSubChanel
         {
             get { return imgSubChaSearch; }
             set { imgSubChaSearch = value; }
         }
         public TextBox txtAreya
         {
             get { return TextBoxArea; }
             set { TextBoxArea = value; }
         }
         public LinkButton lbtnAreya
         {
             get { return imgAreaSearch; }
             set { imgAreaSearch = value; }
         }
         public TextBox txtRegion
         {
             get { return TextBoxRegion; }
             set { TextBoxRegion = value; }
         }
         public LinkButton lbtnRegion
         {
             get { return imgRegionSearch; }
             set { imgRegionSearch = value; }
         }
         public TextBox txtZone
         {
             get { return TextBoxZone; }
             set { TextBoxZone = value; }
         }
         public LinkButton lbtnZone
         {
             get { return imgZoneSearch; }
             set { imgZoneSearch = value; }
         }
         public TextBox txtLocation
         {
             get { return TextBoxLocation; }
             set { TextBoxLocation = value; }
         }
         public LinkButton lbtnLocation
         {
             get { return imgProCeSearch; }
             set { imgProCeSearch = value; }
         }
         public LinkButton lbtnImgSearch
         {
             get { return ImgSearch; }
             set { ImgSearch = value; }
         }
         public TextBox txtSearch
         {
             get { return txtSearchbyword; }
             set { txtSearchbyword = value; }
         }

         public DropDownList cmbSearchby
         {
             get { return cmbSearchbykey; }
             set { cmbSearchbykey = value; }
         }

        #endregion

        public bool IsAllLocation
        {
            set { _isAllLocation = value; }
        }
        public bool IscheckLocationValidation
        {
            get { if (Session["_ischeckLocationValidation"] == null) return false; return Convert.ToBoolean(Session["_ischeckLocationValidation"]); }
            set { Session["_ischeckLocationValidation"] = value; }
        }
        #region properties

        public string Company
        {
            get { return TextBoxCompany.Text; }
            set { TextBoxCompany.Text = value; }
        }

        public string CompanyDes
        {
            get { return TextBoxCompanyDes.Text; }
            set { TextBoxCompanyDes.Text = value; }
        }

        public string Channel
        {
            get { return TextBoxChannel.Text; }
            set { TextBoxChannel.Text = value; }
        }

        public string SubChannel
        {
            get { return TextBoxSubChannel.Text; }
            set { TextBoxSubChannel.Text = value; }
        }

        public string Area
        {
            get { return TextBoxArea.Text; }
            set { TextBoxArea.Text = value; }
        }

        public string Regien
        {
            get { return TextBoxRegion.Text; }
            set { TextBoxRegion.Text = value; }
        }

        public string Zone
        {
            get { return TextBoxZone.Text; }
            set { TextBoxZone.Text = value; }
        }

        public string ProfitCenter
        {
            get { return TextBoxLocation.Text; }
            set { TextBoxLocation.Text = value; }
        }

        public TextBox TXTItemCode
        {
            get { return TextBoxLocation; }
            set { TextBoxLocation = value; }
        }

        public string ParrentFormName
        {
            get { return lblFormName.Text; }
            set { lblFormName.Text = value; }
        }
        public void ChangeCompany(Boolean IsAllowed)
        {
            if (IsAllowed == true)
            {
                TextBoxCompany.Enabled = true;
                ImgBtnAccountNo.Enabled = true;
            }
            else
            {
                TextBoxCompany.Enabled = false;
                ImgBtnAccountNo.Enabled = false;
            }
        }

        public int _allcom1;
        public int _allcom
        {
            get { return _allcom1; }
            set { _allcom1 = value; }
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["locationsearch"] == null))
            {
                if (IsPostBack)
                {

                    UserPopoup.Show();
                    Session["locationsearch"] = null;
                    dvResult.DataSource = null;
                    Session["UcLoSeLocation"] = null;
                   
                }
            }
            else
            {
               
            }





        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            //_basePage = new Base();
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel:
                    {
                        paramsText.Append(TextBoxCompany.Text.ToUpper() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel:
                    {
                        //ucLCSE001
                        if (_isDisplayRawData == false)
                            paramsText.Append(TextBoxCompany.Text.ToUpper() + seperator + TextBoxChannel.Text.ToUpper() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel.ToString() + seperator);
                        else
                            paramsText.Append(TextBoxCompany.Text.ToUpper() + seperator + string.Empty + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel.ToString() + seperator);
                        break;

                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area:
                    {
                        //ucLCSE001
                        if (_isDisplayRawData == false)
                            paramsText.Append(TextBoxCompany.Text.ToUpper() + seperator + TextBoxChannel.Text.ToUpper() + seperator + TextBoxSubChannel.Text.ToUpper() + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area.ToString() + seperator);
                        else
                            paramsText.Append(TextBoxCompany.Text.ToUpper() + seperator + string.Empty + seperator + string.Empty + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region:
                    {
                        //ucLCSE001
                        if (_isDisplayRawData == false)
                            paramsText.Append(TextBoxCompany.Text.ToUpper() + seperator + TextBoxChannel.Text.ToUpper() + seperator + TextBoxSubChannel.Text.ToUpper() + seperator + TextBoxArea.Text.ToUpper() + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region.ToString() + seperator);
                        else
                            paramsText.Append(TextBoxCompany.Text.ToUpper() + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone:
                    {
                        //ucLCSE001
                        if (_isDisplayRawData == false)
                            paramsText.Append(TextBoxCompany.Text.ToUpper() + seperator + TextBoxChannel.Text.ToUpper() + seperator + TextBoxSubChannel.Text.ToUpper() + seperator + TextBoxArea.Text.ToUpper() + seperator + TextBoxRegion.Text.ToUpper() + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone.ToString() + seperator);
                        else
                            paramsText.Append(TextBoxCompany.Text.ToUpper() + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        paramsText.Append(TextBoxCompany.Text.ToUpper() + seperator + TextBoxChannel.Text.ToUpper() + seperator + TextBoxSubChannel.Text.ToUpper() + seperator + TextBoxArea.Text.ToUpper() + seperator + TextBoxRegion.Text.ToUpper() + seperator + TextBoxZone.Text.ToUpper() + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(TextBoxCompany.Text.ToUpper().Trim() + seperator);
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
        protected void ImgBtnAccountNo_Click(object sender, EventArgs e)
        {
            _basePage = new Base();
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company);
            DataTable _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, null, null);
            dvResult.DataSource = _result;
            dvResult.DataBind();
            Label8.Text = "35";
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
        }
        protected void dvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string name = dvResult.SelectedRow.Cells[1].Text;
            string Des = dvResult.SelectedRow.Cells[2].Text;
            if (Label8.Text == "35")
            {
                TextBoxCompany.Text = name;
                TextBoxCompany_TextChanged(null,null);
                TextBoxCompanyDes.Text = Des;
                TextBoxCompanyDes.ToolTip = Des;
                errorDiv.Visible = false;
            }
            if (Label8.Text == "36")
            {
                TextBoxChannel.Text = name;
                TextBoxChannelDes.Text = Des;
                TextBoxChannelDes.ToolTip = Des;
                errorDiv.Visible = false;
            }
            if (Label8.Text == "37")
            {
                TextBoxSubChannel.Text = name;
                TextBoxSubChannelDes.Text = Des;
                TextBoxSubChannelDes.ToolTip = Des;
                errorDiv.Visible = false;
            }
            if (Label8.Text == "38")
            {
                TextBoxArea.Text = name;
                TextBoxAreaDes.Text = Des;
                TextBoxAreaDes.ToolTip = Des;
                errorDiv.Visible = false;
            }
            if (Label8.Text == "39")
            {
                TextBoxRegion.Text = name;
                TextBoxRegionDes.Text = Des;
                TextBoxRegionDes.ToolTip = Des;
                errorDiv.Visible = false;
            }
            if (Label8.Text == "40")
            {
                TextBoxZone.Text = name;
                TextBoxZoneDes.Text = Des;
                TextBoxZoneDes.ToolTip = Des;
                errorDiv.Visible = false;
            }
            if (Label8.Text == "41")
            {
                TextBoxLocation.Text = name;
                TextBoxLocationDes.Text = Des;
                TextBoxLocationDes.ToolTip = Des;
                errorDiv.Visible = false;
                if (ParrentFormName == "InventoryTracker")
                {
                    ((TextBox)this.Parent.FindControl("txtLocation")).Text = name;
                    //Bind Grid
                    List<MasterLocation> loc_list = CHNLSVC.Inventory.getAllLoc_WithSubLoc(Session["UserCompanyCode"].ToString(), name);
                    if (loc_list == null)
                    {
                        MasterLocation loc_ = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), name);
                        loc_list = new List<MasterLocation>();
                        loc_list.Add(loc_);
                    }
                    else if (loc_list.Count < 1)
                    {
                        MasterLocation loc_ = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), name);
                        loc_list.Add(loc_);
                    }
                    if (loc_list.Count > 0)
                    {
                        if (((GridView)this.Parent.FindControl("grvAllLocation")) != null)
                        {
                            ((GridView)this.Parent.FindControl("grvAllLocation")).DataSource = loc_list;
                            ((GridView)this.Parent.FindControl("grvAllLocation")).DataBind();
                            foreach (GridViewRow row in ((GridView)this.Parent.FindControl("grvAllLocation")).Rows)
                            {
                                CheckBox chkcheck = (CheckBox)row.FindControl("chkLocationRow");
                                chkcheck.Checked = true;
                            }
                        }
                    }
                    else
                    {
                        ((GridView)this.Parent.FindControl("grvAllLocation")).DataSource = null;
                        ((GridView)this.Parent.FindControl("grvAllLocation")).DataBind();
                    }
                }
            }
            UserPopoup.Hide();
            txtSearchbyword.Text = "";
            Session["locationsearch"] = null;
            Label8.Text = "";
        }

        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {

            _basePage = new Base();
            if (Label8.Text == "35")
            {
                _result = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company);
                _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResult.DataSource = _result;
                dvResult.DataBind();
                UserPopoup.Show();
                Session["locationsearch"] = "search";
            }
            else if (Label8.Text == "36")
            {
                _result = null;
                if (IsDisplayRawData)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                    IsRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                    IsRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                dvResult.DataSource = null;
                dvResult.DataSource = _result;
                dvResult.DataBind();
                UserPopoup.Show();
               
                Session["locationsearch"] = "search";

            }
            else if (Label8.Text == "37")
            {
                _result = null;

                if (IsDisplayRawData)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
                    IsRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
                    IsRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                dvResult.DataSource = null;
                dvResult.DataSource = _result;
                dvResult.DataBind();
                UserPopoup.Show();
                Session["locationsearch"] = "search";
            }
            else if (Label8.Text == "38")
            {
                _result = null;
                if (IsDisplayRawData)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area);
                    IsRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area);
                    IsRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                dvResult.DataSource = null;
                dvResult.DataSource = _result;
                dvResult.DataBind();
                UserPopoup.Show();
                Session["locationsearch"] = "search";

            }

            else if (Label8.Text == "39")
            {
                _result = null;
                if (IsDisplayRawData)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region);
                    IsRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region);
                    IsRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                dvResult.DataSource = null;
                dvResult.DataSource = _result;
                dvResult.DataBind();
                UserPopoup.Show();
                Session["locationsearch"] = "search";
            }
            else if (Label8.Text == "40")
            {
                _result = null;
                if (IsDisplayRawData)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone);
                    IsRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone);
                    IsRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                dvResult.DataSource = null;
                dvResult.DataSource = _result;
                dvResult.DataBind();
                UserPopoup.Show();
                Session["locationsearch"] = "search";
            }
            else if (Label8.Text == "41")
            {
                _result = null;
                if (_isAllLocation == false)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                    _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());


                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    _result = _basePage.CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    dvResult.DataSource = _result;

                }
                dvResult.DataSource = null;
                dvResult.DataSource = _result;
                dvResult.DataBind();

                UserPopoup.Show();
                Session["locationsearch"] = "search";
            }
            //UserPopoup.Show();
          //  txtSearchbyword.Text = "";
        }
        public bool IsRawData = false;
        DataTable _result;
        protected void ImgSearch_Click(object sender, EventArgs e)
        {
            _basePage = new Base();
            if (Label8.Text == "35")
            {
                _result = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company);
                _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResult.DataSource = _result;
                dvResult.DataBind();
                UserPopoup.Show();
            }
            else if (Label8.Text == "36")
            {
                _result = null;
                if (IsDisplayRawData)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                    IsRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                    IsRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                dvResult.DataSource = null;
                dvResult.DataSource = _result;
                dvResult.DataBind();
                UserPopoup.Show();

            }
            else if (Label8.Text == "37")
            {
                _result = null;

                if (IsDisplayRawData)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
                    IsRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
                    IsRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                dvResult.DataSource = null;
                dvResult.DataSource = _result;
                dvResult.DataBind();
                UserPopoup.Show();
            }
            else if (Label8.Text == "38")
            {
                _result = null;
                if (IsDisplayRawData)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area);
                    IsRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area);
                    IsRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                dvResult.DataSource = null;
                dvResult.DataSource = _result;
                dvResult.DataBind();
                UserPopoup.Show();


            }

            else if (Label8.Text == "39")
            {
                _result = null;
                if (IsDisplayRawData)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region);
                    IsRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region);
                    IsRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                dvResult.DataSource = null;
                dvResult.DataSource = _result;
                dvResult.DataBind();
                UserPopoup.Show();
            }
            else if (Label8.Text == "40")
            {
                _result = null;
                if (IsDisplayRawData)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone);
                    IsRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone);
                    IsRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                dvResult.DataSource = null;
                dvResult.DataSource = _result;
                dvResult.DataBind();
                UserPopoup.Show();
            }
            else if (Label8.Text == "41")
            {
                _result = null;
                if (_isAllLocation == false)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                    _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());


                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    _result = _basePage.CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    dvResult.DataSource = _result;

                }
                dvResult.DataSource = null;
                dvResult.DataSource = _result;
                dvResult.DataBind();
                BindUCtrlDDLData(_result);
                UserPopoup.Show();

            }
            //UserPopoup.Show();
            txtSearchbyword.Text = "";
        }
        //protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        //{
        //    _basePage = new Base();
        //    if (Label8.Text == "35")
        //    {
        //        _result = null;
        //        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company);
        //        _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
        //        dvResult.DataSource = _result;
        //        dvResult.DataBind();
        //        UserPopoup.Show();
        //    }
        //    else if (Label8.Text == "36")
        //    {
        //        _result = null;
        //        if (IsDisplayRawData)
        //        {
        //            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
        //            IsRawData = true;
        //            _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
        //        }
        //        else
        //        {
        //            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
        //            IsRawData = false;
        //            _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
        //        }
        //        dvResult.DataSource = null;
        //        dvResult.DataSource = _result;
        //        dvResult.DataBind();
        //        UserPopoup.Show();

        //    }
        //    else if (Label8.Text == "37")
        //    {
        //        _result = null;

        //        if (IsDisplayRawData)
        //        {
        //           string  SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
        //            IsRawData = true;
        //            _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
        //        }
        //        else
        //        {
        //            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
        //            IsRawData = false;
        //            _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
        //        }
        //        dvResult.DataSource = null;
        //        dvResult.DataSource = _result;
        //        dvResult.DataBind();
        //        UserPopoup.Show();
        //    }
        //    else if (Label8.Text == "38")
        //    {
        //        _result = null;
        //        if (IsDisplayRawData)
        //        {
        //           string  SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area);
        //            IsRawData = true;
        //            _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
        //        }
        //        else
        //        {
        //           string  SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area);
        //            IsRawData = false;
        //            _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
        //        }
        //        dvResult.DataSource = null;
        //        dvResult.DataSource = _result;
        //        dvResult.DataBind();
        //        UserPopoup.Show();


        //    }

        //    else if (Label8.Text=="39")
        //    {
        //        _result = null;
        //        if (IsDisplayRawData)
        //        {
        //            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region);
        //            IsRawData = true;
        //            _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
        //        }
        //        else
        //        {
        //            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region);
        //            IsRawData = false;
        //            _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
        //        }
        //        dvResult.DataSource = null;
        //        dvResult.DataSource = _result;   
        //        dvResult.DataBind();
        //        UserPopoup.Show();
        //    }
        //    else if (Label8.Text == "40")
        //    {
        //        _result = null;
        //        if (IsDisplayRawData)
        //        {
        //            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone);
        //            IsRawData = true;
        //            _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
        //        }
        //        else
        //        {
        //            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone);
        //            IsRawData = false;
        //            _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
        //        }
        //        dvResult.DataSource = null;
        //        dvResult.DataSource = _result;
        //        dvResult.DataBind();
        //        UserPopoup.Show();
        //    }
        //    else if(Label8.Text=="41")
        //    {
        //        _result = null;
        //        if (_isAllLocation == false)
        //        {
        //            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
        //            _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());


        //        }
        //        else
        //        {
        //            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
        //            _result = _basePage.CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
        //            dvResult.DataSource = _result;

        //        }
        //        dvResult.DataSource = null;
        //        dvResult.DataSource = _result;
        //        dvResult.DataBind();
        //        BindUCtrlDDLData(_result);
        //        UserPopoup.Show();

        //    }
        //    //UserPopoup.Show();


        //}

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDisplayRawData
        {
            get { return _isDisplayRawData; }
            set { _isDisplayRawData = value; }
        }
        protected void imgChaSearch_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            string SearchParams;
            if (TextBoxCompany.Text.Trim() == "")
            {
                // lblCError.Visible = true;
                lblWarn.Text = "Enter Company Code.";
                errorDiv.Visible = true;
                //MessageBox.Show("Enter Company Code");
                return;
            }
            _basePage = new Base();
            //lblCError.Visible = false;
            DataTable _result = null;
            if (IsDisplayRawData)
            {
                SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                bool IsRawData = true;
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, null, null);
            }
            else
            {
                SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                bool IsRawData = false;
                _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, null, null);
            }

            dvResult.DataSource = _result;
            dvResult.DataBind();
            BindUCtrlDDLData(_result);
            UserPopoup.Show();
            Label8.Text = "36";
            TextBoxChannel.Focus();
        }

        protected void imgSubChaSearch_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            string SearchParams;
            bool IsRawData;
            _basePage = new Base();
            if (TextBoxCompany.Text.Trim() == "")
            {
                lblWarn.Text = "Enter Company Code.";
                errorDiv.Visible = true;
                // MessageBox.Show("Enter Company Code");
                //return;
            }
            if (string.IsNullOrEmpty(TextBoxChannel.Text))
            {
                TextBoxChannelDes.Text = "";
                // return;
            }

            if (!_basePage.CHNLSVC.General.CheckChannel(TextBoxCompany.Text.ToUpper().Trim(), TextBoxChannel.Text.ToUpper().Trim().ToUpper()) && !string.IsNullOrEmpty(TextBoxChannel.Text))
            {
                lblWarn.Text = "Please check the channel.";
                errorDiv.Visible = true;
                //MessageBox.Show("Please check the channel.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);

                TextBoxChannel.Text = "";
                TextBoxChannelDes.Text = "";
                return;
            }
            else
            {


                DataTable _result = null;
                if (IsDisplayRawData)
                {
                    SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
                    IsRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, null, null);
                }
                else
                {
                    SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
                    IsRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, null, null);
                }
                dvResult.DataSource = _result;
                dvResult.DataBind();
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
                Label8.Text = "37";
                TextBoxSubChannel.Focus();
            }
        }

        protected void imgAreaSearch_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            string SearchParams;
            bool IsRawData;
            _basePage = new Base();
            if (TextBoxCompany.Text.Trim() == "")
            {
                lblWarn.Text = "Enter Company Code.";
                errorDiv.Visible = true;
                //MessageBox.Show("Enter Company Code");
                return;
            }
            if (string.IsNullOrEmpty(TextBoxSubChannel.Text))
            {
                TextBoxSubChannelDes.Text = "";
                //return;
            }
            if (!_basePage.CHNLSVC.General.CheckSubChannel(TextBoxCompany.Text.ToUpper().Trim(), TextBoxSubChannel.Text.ToUpper().Trim()) && !string.IsNullOrEmpty(TextBoxSubChannel.Text.ToUpper()))
            {
                lblWarn.Text = "Please check the sub channel";
                errorDiv.Visible = true;
                //MessageBox.Show("Please check the sub channel.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TextBoxSubChannel.Text = "";
                TextBoxSubChannelDes.Text = "";
                return;
            }
            else
            {

                DataTable _result = null;
                if (IsDisplayRawData)
                {
                    SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area);
                    IsRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, null, null);
                }
                else
                {
                    SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area);
                    IsRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, null, null);
                }
                dvResult.DataSource = _result;
                dvResult.DataBind();
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
                Label8.Text = "38";

                TextBoxArea.Focus();
            }
        }

        protected void imgRegionSearch_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            string SearchParams;
            bool IsRawData;
            if (TextBoxCompany.Text.Trim() == "")
            {
                lblWarn.Text = "Enter Company Code.";
                errorDiv.Visible = true;
                // MessageBox.Show("Enter Company Code");
                return;
            }
            _basePage = new Base();
            if (string.IsNullOrEmpty(TextBoxArea.Text))
            {
                TextBoxAreaDes.Text = "";
                // return;
            }

            if (!_basePage.CHNLSVC.General.CheckArea(TextBoxCompany.Text.ToUpper().Trim(), TextBoxArea.Text.ToUpper().Trim().ToUpper()) && !string.IsNullOrEmpty(TextBoxArea.Text.ToUpper()))
            {
                lblWarn.Text = "Please check the area.";
                errorDiv.Visible = true;
                // MessageBox.Show("Please check the area.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TextBoxArea.Text = "";
                TextBoxAreaDes.Text = "";
                return;
            }
            else
            {
                DataTable _result = null;
                if (IsDisplayRawData)
                {
                    SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region);
                    IsRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, null, null);
                }
                else
                {
                    SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region);
                    IsRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, null, null);
                }
                dvResult.DataSource = _result;
                BindUCtrlDDLData(_result);
                dvResult.DataBind();
                UserPopoup.Show();
                Label8.Text = "39";

                TextBoxRegion.Focus();
            }
        }

        protected void imgZoneSearch_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            string SearchParams;
            bool IsRawData;
            if (TextBoxCompany.Text.Trim() == "")
            {
                lblWarn.Text = "Enter Company Code.";
                errorDiv.Visible = true;
                //MessageBox.Show("Enter Company Code");
                return;
            }
            _basePage = new Base();
            if (string.IsNullOrEmpty(TextBoxRegion.Text))
            {
                TextBoxRegionDes.Text = "";
                // return;
            }

            if (!_basePage.CHNLSVC.General.CheckRegion(TextBoxCompany.Text.ToUpper().Trim(), TextBoxRegion.Text.Trim().ToUpper()) && !string.IsNullOrEmpty(TextBoxRegion.Text))
            {
                lblWarn.Text = "Please check the region.";
                errorDiv.Visible = true;
                //MessageBox.Show("Please check the region.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                TextBoxRegion.Text = "";
                TextBoxRegionDes.Text = "";
                return;
            }
            else
            {
                DataTable _result = null;
                if (IsDisplayRawData)
                {
                    SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone);
                    IsRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, null, null);
                }
                else
                {
                    SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone);
                    IsRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, null, null);
                }
                //dvResult.DataSource = null;
                dvResult.DataSource = _result;
                dvResult.DataBind();
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
                Label8.Text = "40";

                TextBoxZone.Focus();
            }
        }

        protected void imgProCeSearch_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            string SearchParams;
            bool IsRawData;
            if (TextBoxCompany.Text.Trim() == "")
            {
                lblWarn.Text = "Enter Company Code.";
                errorDiv.Visible = true;
                //  MessageBox.Show("Enter Company Code");
                return;
            }

            _basePage = new Base();
            if (string.IsNullOrEmpty(TextBoxZone.Text))
            {
                TextBoxZoneDes.Text = "";
                //return;
            }
            _basePage = new Base();
            if (!_basePage.CHNLSVC.General.CheckZone(TextBoxCompany.Text.ToUpper().Trim(), TextBoxZone.Text.Trim().ToUpper()) && !string.IsNullOrEmpty(TextBoxZone.Text))
            {
                lblWarn.Text = "Please check the zone.";
                errorDiv.Visible = true;
                // MessageBox.Show("Please check the zone.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                TextBoxZone.Text = "";
                TextBoxZoneDes.Text = "";
                return;
            }


            DataTable _tbl = _basePage.CHNLSVC.Inventory.Get_location_by_code(TextBoxCompany.Text.ToUpper().Trim(), TextBoxLocation.Text.ToUpper().Trim());
            if (_tbl == null || _tbl.Rows.Count <= 0)
            {
                lblWarn.Text = "Please select a valid location.";
                errorDiv.Visible = true;
                //MessageBox.Show("Please select the valid location.", "Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TextBoxLocation.Text = "";
                return;
            }

            if (_isAllLocation == false)
            {
                SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                DataTable _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, null, null);
                dvResult.DataSource = _result;
                dvResult.DataBind();
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
                Label8.Text = "41";
            }
            else
            {
                SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = _basePage.CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, null, null);
                dvResult.DataSource = _result;
                dvResult.DataBind();
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
                Label8.Text = "41";
            }

            TextBoxLocation.Focus();
        }

        protected void TextBoxCompany_TextChanged(object sender, EventArgs e)
        {
            errorDiv.Visible = false;
            _basePage = new Base();
            string name = TextBoxCompany.Text.ToUpper();
            // CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            TextBoxCompanyDes.Text = GetLoc_HIRC_SearchDesc(35, TextBoxCompany.Text.ToUpper());
            //GetLoc_HIRC_SearchDesc
            TextBoxCompanyDes.ToolTip = TextBoxCompanyDes.Text;
            TextBoxChannel.Focus();
            TextBoxChannel.Text = "";
            TextBoxChannelDes.Text = "";
            //Lakshan Clear Data when company select
            TextBoxSubChannel.Text = "";
            TextBoxSubChannelDes.Text = "";
            TextBoxArea.Text = "";
            TextBoxAreaDes.Text = "";
            TextBoxRegion.Text = "";
            TextBoxRegionDes.Text = "";
            TextBoxZone.Text = "";
            TextBoxZoneDes.Text = "";
            TextBoxLocation.Text = "";
            TextBoxLocationDes.Text = "";
            _basePage = new Base();

            if (!_basePage.CHNLSVC.General.CheckCompany(TextBoxCompany.Text.ToUpper().Trim()))
            {
                lblWarn.Text = "Please check the company.";
                errorDiv.Visible = true;
                TextBoxCompany.Text = string.Empty;
                TextBoxCompanyDes.Text = string.Empty;
                return;
            }

        }

        public string GetLoc_HIRC_SearchDesc(int i, string _code)
        {
            if (i > 41 || i < 35)
            {
                return null;
            }
            ChannelOperator chnlOpt = new ChannelOperator();
            CommonUIDefiniton.SearchUserControlType _type = (CommonUIDefiniton.SearchUserControlType)i;

            Base _basePage = new Base();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            _basePage = new Base();
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                        break;
                    }

                default:
                    break;
            }


            DataTable dt = chnlOpt.CommonSearch.Getloc_HIRC_SearchData(paramsText.ToString(), "CODE", _code);
            if (dt == null)
            {
                return null;
            }
            if (dt.Rows.Count <= 0 || dt == null || dt.Rows.Count > 1)
                return null;
            else
                return dt.Rows[0][1].ToString();
        }

        protected void TextBoxChannel_TextChanged(object sender, EventArgs e)
        {
            errorDiv.Visible = false;
            if (TextBoxCompany.Text.Trim() == "")
            {
                //MessageBox.Show("Enter Company Code");
                lblWarn.Text = "Enter Company Code.";
                errorDiv.Visible = true;
                TextBoxChannel.Text = "";
                return;

            }
            if (string.IsNullOrEmpty(TextBoxChannel.Text))
            {
                TextBoxChannelDes.Text = "";
                return;
            }
            _basePage = new Base();
            if (!_basePage.CHNLSVC.General.CheckChannel(TextBoxCompany.Text.ToUpper().Trim(), TextBoxChannel.Text.ToUpper().Trim().ToUpper()))
            {
                lblWarn.Text = "Please check the channel.";
                errorDiv.Visible = true;
                // MessageBox.Show("Please check the channel.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TextBoxChannel.Text = "";
                TextBoxChannelDes.Text = "";
                return;
            }
            TextBoxChannelDes.Text = GetLoc_HIRC_SearchDesc(36, TextBoxChannel.Text.ToUpper().ToUpper());
            TextBoxChannelDes.ToolTip = TextBoxChannelDes.Text;
            TextBoxSubChannel.Focus();
        }

        protected void TextBoxSubChannel_TextChanged(object sender, EventArgs e)
        {
            errorDiv.Visible = false;
            if (TextBoxCompany.Text.Trim() == "")
            {
                lblWarn.Text = "Enter Company Code.";
                errorDiv.Visible = true;
                return;
            }
            if (string.IsNullOrEmpty(TextBoxSubChannel.Text))
            {
                TextBoxSubChannelDes.Text = "";
                return;
            }
            _basePage = new Base();
            if (!_basePage.CHNLSVC.General.CheckSubChannel(TextBoxCompany.Text.ToUpper().Trim(), TextBoxSubChannel.Text.ToUpper().Trim()))
            {
                lblWarn.Text = "Please check the sub channel..";
                errorDiv.Visible = true;
                //MessageBox.Show("Please check the sub channel.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TextBoxSubChannel.Text = "";
                TextBoxSubChannelDes.Text = "";
                return;
            }
            //TextBoxSubChannelDes.Text = GetLoc_HIRC_SearchDesc(71, TextBoxSubChannel.Text.ToUpper());
            //TextBoxSubChannelDes.ToolTip = TextBoxSubChannelDes.Text;
            //TextBoxArea.Focus();
            TextBoxSubChannelDes.Text = Get_pc_HIRC_SearchDesc(71, TextBoxSubChannel.Text.ToUpper());
            TextBoxSubChannelDes.ToolTip = TextBoxSubChannelDes.Text;
            TextBoxSubChannelDes.Focus();
        }

        protected void TextBoxArea_TextChanged(object sender, EventArgs e)
        {
            errorDiv.Visible = false;
            if (TextBoxCompany.Text.Trim() == "")
            {
                lblWarn.Text = "Please check the sub channel..";
                errorDiv.Visible = true;
                return;
            }
            if (string.IsNullOrEmpty(TextBoxArea.Text))
            {
                TextBoxAreaDes.Text = ""; return;
            }
            _basePage = new Base();
            if (!_basePage.CHNLSVC.General.CheckArea(TextBoxCompany.Text.ToUpper().Trim(), TextBoxArea.Text.Trim().ToUpper()))
            {
                lblWarn.Text = "Please check the area.";
                errorDiv.Visible = true;
                //MessageBox.Show("Please check the area.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TextBoxArea.Text = "";
                TextBoxAreaDes.Text = "";
                return;
            }

            TextBoxAreaDes.Text = GetLoc_HIRC_SearchDesc(38, TextBoxArea.Text.ToUpper());
            TextBoxAreaDes.ToolTip = TextBoxAreaDes.Text;
        }

        protected void TextBoxRegion_TextChanged(object sender, EventArgs e)
        {
            errorDiv.Visible = false;
            if (TextBoxCompany.Text.Trim() == "")
            {
                lblWarn.Text = "Enter Company Code..";
                errorDiv.Visible = true;
                //MessageBox.Show("Enter Company Code");
                return;
            }
            if (string.IsNullOrEmpty(TextBoxRegion.Text)) { TextBoxRegionDes.Text = ""; return; }
            _basePage = new Base();
            if (!_basePage.CHNLSVC.General.CheckRegion(TextBoxCompany.Text.ToUpper().Trim(), TextBoxRegion.Text.Trim().ToUpper()))
            {
                lblWarn.Text = "Please check the region..";
                errorDiv.Visible = true;
                TextBoxRegion.Text = "";
                TextBoxRegionDes.Text = "";
                // MessageBox.Show("Please check the region.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); TextBoxRegion.Clear(); TextBoxRegionDes.Clear(); 
                return;
            }


            TextBoxRegionDes.Text = GetLoc_HIRC_SearchDesc(39, TextBoxRegion.Text.ToUpper());
            TextBoxRegionDes.ToolTip = TextBoxRegionDes.Text;
        }

        protected void TextBoxZone_TextChanged(object sender, EventArgs e)
        {
            errorDiv.Visible = false;
            if (TextBoxCompany.Text.Trim() == "")
            {
                lblWarn.Text = "Enter Company Code..";
                errorDiv.Visible = true;
                return;
            }
            if (string.IsNullOrEmpty(TextBoxZone.Text)) { TextBoxZoneDes.Text = ""; return; }
            _basePage = new Base();
            if (!_basePage.CHNLSVC.General.CheckZone(TextBoxCompany.Text.ToUpper().Trim(), TextBoxZone.Text.Trim().ToUpper()))
            {
                lblWarn.Text = "Please check the zone..";
                errorDiv.Visible = true;
                //MessageBox.Show("Please check the zone.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); TextBoxZone.Clear(); TextBoxZoneDes.Clear(); 
                return;
            }

            TextBoxZoneDes.Text = GetLoc_HIRC_SearchDesc(40, TextBoxZone.Text.ToUpper());
            TextBoxZoneDes.ToolTip = TextBoxZoneDes.Text;
        }

        private ChannelOperator channelService = new ChannelOperator();

        public ChannelOperator CHNLSVC
        {
            get
            {
                return channelService;
            }
        }

        SystemUser _systemUser;

        protected void TextBoxLocation_TextChanged(object sender, EventArgs e)
        {
            errorDiv.Visible = false;

            if (TextBoxCompany.Text.Trim() == "")
            {
                lblWarn.Text = "Enter Company Code..";
                errorDiv.Visible = true;
                return;
            }
            if (string.IsNullOrEmpty(TextBoxLocation.Text))
            {
                TextBoxLocationDes.Text = "";
                return;
            }
            try
            {
                _basePage = new Base();
                DataTable _tbl = new DataTable();
                //if (_allcom1 == 0)
                //{
                //    _tbl = _basePage.CHNLSVC.Inventory.Get_location_by_code(TextBoxCompany.Text.ToUpper().Trim(), TextBoxLocation.Text.ToUpper().Trim());
                //}
                //else
                //{
                    _tbl = _basePage.CHNLSVC.Inventory.Get_location_by_code_all(TextBoxCompany.Text.ToUpper().Trim(), TextBoxLocation.Text.ToUpper().Trim(), 1);
                //}
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    if (IscheckLocationValidation == true) // add by rukshan ,request by kapila
                    {
                        lblWarn.Text = "Please select a valid location.";
                        errorDiv.Visible = true;
                        // MessageBox.Show("Please select the valid location.", "Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TextBoxLocation.Text = "";
                        return;
                    }
                    else
                    {
                        if (!_basePage.CHNLSVC.General.CheckProfitCenter(TextBoxCompany.Text.ToUpper().Trim(), TextBoxLocation.Text.Trim().ToUpper()))
                        {
                            lblWarn.Text = "Please check the profit center.";
                            errorDiv.Visible = true;
                            TextBoxLocation.Text = string.Empty;
                            return;
                        }
                        else
                        {
                            TextBoxLocationDes.Text = Get_pc_HIRC_SearchDesc(75, TextBoxLocation.Text.ToUpper().ToUpper());
                            TextBoxLocationDes.ToolTip = TextBoxLocationDes.Text;
                        }
                    }
                }
                if (_isAllLocation == false)
                {
                    _basePage = new Base();

                    //TextBoxLocationDes.Text = GetLoc_HIRC_SearchDesc(41, TextBoxLocation.Text.ToUpper());
                    //TextBoxLocationDes.ToolTip = GetLoc_HIRC_SearchDesc(41, TextBoxLocation.Text.ToUpper());
                    //  this.imgProCeSearch_Click(null, null);
                    TextBoxLocationDes.Text = _tbl.Rows[0]["ml_loc_desc"].ToString();
                    TextBoxLocationDes.ToolTip = _tbl.Rows[0]["ml_loc_desc"].ToString();
                }
                else
                {
                    //TODO: Load description of the PC - ucLCSE001
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void dvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvResult.PageIndex = e.NewPageIndex;
            _basePage = new Base();
            if (Label8.Text == "35")
            {
                _result = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company);
                _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResult.DataSource = _result;
                dvResult.DataBind();
                UserPopoup.Show();
            }
            else if (Label8.Text == "36")
            {
                _result = null;
                if (IsDisplayRawData)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                    IsRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                    IsRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                dvResult.DataSource = null;
                dvResult.DataSource = _result;
                dvResult.DataBind();
                UserPopoup.Show();

            }
            else if (Label8.Text == "37")
            {
                _result = null;

                if (IsDisplayRawData)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
                    IsRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
                    IsRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                dvResult.DataSource = null;
                dvResult.DataSource = _result;
                dvResult.DataBind();
                UserPopoup.Show();
            }
            else if (Label8.Text == "38")
            {
                _result = null;
                if (IsDisplayRawData)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area);
                    IsRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area);
                    IsRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                dvResult.DataSource = null;
                dvResult.DataSource = _result;
                dvResult.DataBind();
                UserPopoup.Show();


            }

            else if (Label8.Text == "39")
            {
                _result = null;
                if (IsDisplayRawData)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region);
                    IsRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region);
                    IsRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                dvResult.DataSource = null;
                dvResult.DataSource = _result;
                dvResult.DataBind();
                UserPopoup.Show();
            }
            else if (Label8.Text == "40")
            {
                _result = null;
                if (IsDisplayRawData)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone);
                    IsRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone);
                    IsRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                dvResult.DataSource = null;
                dvResult.DataSource = _result;
                dvResult.DataBind();
                UserPopoup.Show();
            }
            else if (Label8.Text == "41")
            {
                _result = null;
                if (_isAllLocation == false)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                    _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());


                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    _result = _basePage.CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    dvResult.DataSource = _result;

                }
                dvResult.DataSource = null;
                dvResult.DataSource = _result;
                dvResult.DataBind();
                BindUCtrlDDLData(_result);
                UserPopoup.Show();

            }

        }

        public void ClearText(bool loadCom = false)
        {
            TextBoxCompany.Text = ""; TextBoxCompanyDes.Text = "";
            TextBoxChannel.Text = ""; TextBoxChannelDes.Text = "";
            TextBoxSubChannel.Text = ""; TextBoxSubChannelDes.Text = "";
            TextBoxArea.Text = ""; TextBoxAreaDes.Text = "";
            TextBoxRegion.Text = ""; TextBoxRegionDes.Text = "";
            TextBoxZone.Text = ""; TextBoxZoneDes.Text = "";
            TextBoxLocation.Text = ""; TextBoxLocationDes.Text = "";

            if (loadCom)
            {
                TextBoxCompany.Text = Session["UserCompanyCode"].ToString();
                TextBoxCompany_TextChanged(null, null);
            }
        }

        protected void TextBoxChannelDes_TextChanged(object sender, EventArgs e)
        {

        }

        public string Get_pc_HIRC_SearchDesc(int i, string _code)
        {
            //if (i > 68 || i < 76)
            //{
            //    return null;
            //}
            ChannelOperator chnlOpt = new ChannelOperator();
            CommonUIDefiniton.SearchUserControlType _type = (CommonUIDefiniton.SearchUserControlType)i;

            Base _basePage = new Base();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            _basePage = new Base();
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(TextBoxCompany.Text.ToUpper() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(TextBoxCompany.Text.ToUpper() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(TextBoxCompany.Text.ToUpper() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(TextBoxCompany.Text.ToUpper() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(TextBoxCompany.Text.ToUpper() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(TextBoxCompany.Text.ToUpper() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }

                default:
                    break;
            }

            DataTable dt = chnlOpt.CommonSearch.Get_PC_HIRC_SearchData(paramsText.ToString(), "CODE", _code);
            if (dt == null)
            {
                return null;
            }
            if (dt.Rows.Count <= 0 || dt == null || dt.Rows.Count > 1)
                return null;
            else
                return dt.Rows[0][1].ToString();
        }
        protected void lBtnAlertDanger_Click(object sender, EventArgs e)
        {
            errorDiv.Visible = false;
        }
    }
}