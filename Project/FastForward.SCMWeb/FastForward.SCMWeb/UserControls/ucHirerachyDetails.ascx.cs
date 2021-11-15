using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
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
    public partial class ucHirerachyDetails : System.Web.UI.UserControl
    {
       //
        private bool _isDisplayRawData = false;
        Base _basePage;
        private bool _isAllLocation = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAllLocation
        {
            set { _isAllLocation = value; }
        }
        #region properties
        public string CompanyGrop
        {
            get { return txtgroupcom.Text; }
            set { txtgroupcom.Text = value; }
        }
        public string CompanyGropDes
        {
            get { return txtgropDes.Text; }
            set { txtgropDes.Text = value; }
        }
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

        public string ChannelDes
        {
            get { return TextBoxChannelDes.Text; }
            set { TextBoxChannelDes.Text = value; }
        }

        public string SubChannelDes
        {
            get { return TextBoxSubChannelDes.Text; }
            set { TextBoxSubChannelDes.Text = value; }
        }

        public string AreaDes
        {
            get { return TextBoxAreaDes.Text; }
            set { TextBoxAreaDes.Text = value; }
        }

        public string RegienDes
        {
            get { return TextBoxRegionDes.Text; }
            set { TextBoxRegionDes.Text = value; }
        }

        public string ZoneDes
        {
            get { return TextBoxZoneDes.Text; }
            set { TextBoxZoneDes.Text = value; }
        }

        public string ProfitCenterDes
        {
            get { return TextBoxLocationDes.Text; }
            set { TextBoxLocationDes.Text = value; }
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

                }
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
                        paramsText.Append(TextBoxCompany.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel:
                    {
                        //ucLCSE001
                        if (_isDisplayRawData == false)
                            paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel.ToString() + seperator);
                        else
                            paramsText.Append(TextBoxCompany.Text + seperator + string.Empty + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel.ToString() + seperator);
                        break;

                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area:
                    {
                        //ucLCSE001
                        if (_isDisplayRawData == false)
                            paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + TextBoxSubChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area.ToString() + seperator);
                        else
                            paramsText.Append(TextBoxCompany.Text + seperator + string.Empty + seperator + string.Empty + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region:
                    {
                        //ucLCSE001
                        if (_isDisplayRawData == false)
                            paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + TextBoxSubChannel.Text + seperator + TextBoxArea.Text + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region.ToString() + seperator);
                        else
                            paramsText.Append(TextBoxCompany.Text + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone:
                    {
                        //ucLCSE001
                        if (_isDisplayRawData == false)
                            paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + TextBoxSubChannel.Text + seperator + TextBoxArea.Text + seperator + TextBoxRegion.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone.ToString() + seperator);
                        else
                            paramsText.Append(TextBoxCompany.Text + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + TextBoxSubChannel.Text + seperator + TextBoxArea.Text + seperator + TextBoxRegion.Text + seperator + TextBoxZone.Text + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(TextBoxCompany.Text.Trim() + seperator);
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
            }
            UserPopoup.Hide();
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
                DispMsg("Please select a company !!!","E");
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
            //Modified by Kelum
            if (TextBoxCompany.Text.Trim() == "")
            {
                DispMsg("Please select a company !!!", "W");
                return;
            }
            if (string.IsNullOrEmpty(TextBoxChannel.Text))
            {
                DispMsg("Please select a channel !!!", "W");
                return;
            }

            if (string.IsNullOrEmpty(TextBoxChannel.Text))
            {
                TextBoxChannelDes.Text = "";
                return;
            }

            if (!_basePage.CHNLSVC.General.CheckChannel(TextBoxCompany.Text.Trim(), TextBoxChannel.Text.Trim().ToUpper()))
            {
                DispMsg("Please check the channel !!!", "W");
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
            //Modified by Kelum
            if (TextBoxCompany.Text.Trim() == "")
            {
                DispMsg("Please select a company !!!", "W");
                return;
            }
            if (string.IsNullOrEmpty(TextBoxChannel.Text))
            {
                DispMsg("Please select a channel !!!", "W");
                return;
            }
            if (string.IsNullOrEmpty(TextBoxSubChannel.Text))
            {
                DispMsg("Please select a sub channel !!!", "W");
                return;
            }
            if (string.IsNullOrEmpty(TextBoxSubChannel.Text))
            {
                TextBoxSubChannelDes.Text = "";
                return;
            }
            if (!_basePage.CHNLSVC.General.CheckSubChannel(TextBoxCompany.Text.Trim(), TextBoxSubChannel.Text.Trim().ToUpper()))
            {
                DispMsg("Please check the sub channel","W");
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
            //Modified by Kelum
            if (TextBoxCompany.Text.Trim() == "")
            {
                DispMsg("Please select a company !!!", "W");
                return;
            }
            if (string.IsNullOrEmpty(TextBoxChannel.Text))
            {
                DispMsg("Please select a channel !!!", "W");
                return;
            }
            if (string.IsNullOrEmpty(TextBoxSubChannel.Text))
            {
                DispMsg("Please select a sub channel !!!", "W");
                return;
            }
            if (string.IsNullOrEmpty(TextBoxArea.Text))
            {
                DispMsg("Please select a area !!!", "W");
                return;
            }
            _basePage = new Base();
            if (string.IsNullOrEmpty(TextBoxArea.Text))
            {
                TextBoxAreaDes.Text = "";
                return;
            }

            if (!_basePage.CHNLSVC.General.CheckArea(TextBoxCompany.Text.Trim(), TextBoxArea.Text.Trim().ToUpper()))
            {
                DispMsg("Please select a valid region !!!", "W");
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
            //Modified by Kelum
            if (TextBoxCompany.Text.Trim() == "")
            {
                lblWarn.Text = "Enter Company Code.";
                errorDiv.Visible = true;
                //MessageBox.Show("Enter Company Code");
                return;
            }
            if (string.IsNullOrEmpty(TextBoxChannel.Text))
            {
                DispMsg("Please select a channel !!!", "W");
                return;
            }
            if (string.IsNullOrEmpty(TextBoxSubChannel.Text))
            {
                DispMsg("Please select a sub channel !!!", "W");
                return;
            }
            if (string.IsNullOrEmpty(TextBoxArea.Text))
            {
                DispMsg("Please select a area !!!", "W");
                return;
            }
            if (string.IsNullOrEmpty(TextBoxRegion.Text))
            {
                DispMsg("Please select a region !!!", "W");
                return;
            }

            _basePage = new Base();
            if (string.IsNullOrEmpty(TextBoxRegion.Text))
            {
                TextBoxRegionDes.Text = "";
                return;
            }

            if (!_basePage.CHNLSVC.General.CheckRegion(TextBoxCompany.Text.Trim(), TextBoxRegion.Text.Trim().ToUpper()))
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
                return;
            }
            _basePage = new Base();
            if (!_basePage.CHNLSVC.General.CheckZone(TextBoxCompany.Text.Trim(), TextBoxZone.Text.Trim().ToUpper()))
            {
                lblWarn.Text = "Please check the zone.";
                errorDiv.Visible = true;
                // MessageBox.Show("Please check the zone.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                TextBoxZone.Text = "";
                TextBoxZoneDes.Text = "";
                return;
            }


            DataTable _tbl = _basePage.CHNLSVC.Inventory.Get_location_by_code(TextBoxCompany.Text.Trim(), TextBoxLocation.Text.Trim());
            if (_tbl == null || _tbl.Rows.Count <= 0)
            {
                lblWarn.Text = "Please select the valid location.";
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
            TextBoxChannel.Text = "";
            TextBoxChannelDes.Text = "";
            TextBoxSubChannel.Text = "";
            TextBoxSubChannelDes.Text = "";
            TextBoxArea.Text = "";
            TextBoxAreaDes.Text = "";
            TextBoxRegion.Text = "";
            TextBoxRegionDes.Text = "";
            TextBoxZone.Text = "";
            TextBoxZoneDes.Text = "";

            errorDiv.Visible = false;
            _basePage = new Base();
            string name = TextBoxCompany.Text.ToUpper();
            MasterCompany mstCom = CHNLSVC.General.GetCompByCode(TextBoxCompany.Text.ToUpper());
            if (mstCom==null)
            {
                DispMsg("Please enter a valid company !!!", "W");
                TextBoxCompany.Text = "";
                return;
            }
            // CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            TextBoxCompanyDes.Text = GetLoc_HIRC_SearchDesc(35, TextBoxCompany.Text.ToUpper());
            //GetLoc_HIRC_SearchDesc
            TextBoxCompanyDes.ToolTip = TextBoxCompanyDes.Text;
            TextBoxChannel.Focus();
            TextBoxChannel.Text = "";

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
            TextBoxSubChannel.Text = "";
            TextBoxSubChannelDes.Text = "";
            TextBoxArea.Text = "";
            TextBoxAreaDes.Text = "";
            TextBoxRegion.Text = "";
            TextBoxRegionDes.Text = "";
            TextBoxZone.Text = "";
            TextBoxZoneDes.Text = "";

            errorDiv.Visible = false;
            if (TextBoxCompany.Text.Trim() == "")
            {
                DispMsg("Please select a company !!!", "W");
                TextBoxChannel.Text = "";
                return;

            }
            if (string.IsNullOrEmpty(TextBoxChannel.Text))
            {
                TextBoxChannelDes.Text = "";
                return;
            }
            _basePage = new Base();
            if (!_basePage.CHNLSVC.General.CheckChannel(TextBoxCompany.Text.Trim(), TextBoxChannel.Text.Trim().ToUpper()))
            {
                DispMsg("Please check the channel !!!", "W");
                TextBoxChannel.Text = "";
                TextBoxChannelDes.Text = "";
                return;
            }
            TextBoxChannelDes.Text = GetLoc_HIRC_SearchDesc(36, TextBoxChannel.Text.ToUpper());
            TextBoxChannelDes.ToolTip = TextBoxChannelDes.Text;
            TextBoxSubChannel.Focus();
        }

        protected void TextBoxSubChannel_TextChanged(object sender, EventArgs e)
        {
            TextBoxArea.Text = "";
            TextBoxAreaDes.Text = "";
            TextBoxRegion.Text = "";
            TextBoxRegionDes.Text = "";
            TextBoxZone.Text = "";
            TextBoxZoneDes.Text = "";
            errorDiv.Visible = false;
            if (TextBoxCompany.Text.Trim() == "")
            {
                DispMsg("Please select a company !!!", "W");
                TextBoxSubChannel.Text = "";
                return;
            }
            if (TextBoxChannel.Text.Trim() == "")
            {
                DispMsg("Please select a channel !!!", "W");
                TextBoxSubChannel.Text = "";
                return;
            }
            if (string.IsNullOrEmpty(TextBoxSubChannel.Text))
            {
                TextBoxSubChannelDes.Text = "";
                return;
            }
            _basePage = new Base();
            if (!_basePage.CHNLSVC.General.CheckSubChannel(TextBoxCompany.Text.Trim(), TextBoxSubChannel.Text.Trim().ToUpper()))
            {
                DispMsg("Please check the sub channel !!!", "W");
                TextBoxSubChannel.Text = "";
                TextBoxSubChannelDes.Text = "";
                return;
            }

            TextBoxSubChannelDes.Text = GetLoc_HIRC_SearchDesc(37, TextBoxSubChannel.Text.ToUpper());
            TextBoxSubChannelDes.ToolTip = TextBoxSubChannelDes.Text;
            TextBoxArea.Focus();
        }

        protected void TextBoxArea_TextChanged(object sender, EventArgs e)
        {
            TextBoxRegion.Text = "";
            TextBoxRegionDes.Text = "";
            TextBoxZone.Text = "";
            TextBoxZoneDes.Text = "";
            errorDiv.Visible = false;
            if (TextBoxCompany.Text.Trim() == "")
            {
                DispMsg("Please select a company !!!", "W");
                TextBoxArea.Text = "";
                return;
            }
            if (TextBoxSubChannel.Text.Trim() == "")
            {
                DispMsg("Please select a sub channel !!!", "W");
                TextBoxArea.Text = "";
                return;
            }
            if (string.IsNullOrEmpty(TextBoxArea.Text))
            {
                TextBoxAreaDes.Text = ""; return;
            }
            _basePage = new Base();
            if (!_basePage.CHNLSVC.General.CheckArea(TextBoxCompany.Text.Trim(), TextBoxArea.Text.Trim().ToUpper()))
            {
                DispMsg("Please check the sub channel !!!", "W");
                TextBoxArea.Text = "";
                TextBoxAreaDes.Text = "";
                return;
            }

            TextBoxAreaDes.Text = GetLoc_HIRC_SearchDesc(38, TextBoxArea.Text.ToUpper());
            TextBoxAreaDes.ToolTip = TextBoxAreaDes.Text;
        }

        protected void TextBoxRegion_TextChanged(object sender, EventArgs e)
        {
            TextBoxZone.Text = "";
            TextBoxZoneDes.Text = "";
            errorDiv.Visible = false;
            if (TextBoxCompany.Text.Trim() == "")
            {
                DispMsg("Please select a company !!!", "W");
                TextBoxRegion.Text = "";
                return;
            }
            if (TextBoxArea.Text.Trim() == "")
            {
                DispMsg("Please select a area !!!", "W");
                TextBoxRegion.Text = "";
                return;
            }
            if (string.IsNullOrEmpty(TextBoxRegion.Text)) { TextBoxRegionDes.Text = ""; return; }
            _basePage = new Base();
            if (!_basePage.CHNLSVC.General.CheckRegion(TextBoxCompany.Text.Trim(), TextBoxRegion.Text.Trim().ToUpper()))
            {
                DispMsg("Please enter a valid region !!!", "W");
                TextBoxRegion.Text = "";
                TextBoxRegionDes.Text = "";
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
                DispMsg("Please select a company !!!", "W");
                return;
            }
            if (TextBoxRegion.Text.Trim() == "")
            {
                DispMsg("Please select a region !!!", "W");
                TextBoxZone.Text = "";
                return;
            }
            if (string.IsNullOrEmpty(TextBoxZone.Text)) { TextBoxZoneDes.Text = ""; return; }
            _basePage = new Base();
            if (!_basePage.CHNLSVC.General.CheckZone(TextBoxCompany.Text.Trim(), TextBoxZone.Text.Trim().ToUpper()))
            {
                DispMsg("Please enter a valid zone !!!", "W");
                TextBoxZone.Text = "";
                TextBoxZoneDes.Text = "";
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
                DispMsg("Please select a company !!!", "W");
                TextBoxLocation.Text = "";
                return;
            }
            if (TextBoxZone.Text.Trim() == "")
            {
                DispMsg("Please select a zone !!!", "W");
                TextBoxLocation.Text = "";
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
                DataTable _tbl = _basePage.CHNLSVC.Inventory.Get_location_by_code(TextBoxCompany.Text.Trim(), TextBoxLocation.Text.Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    lblWarn.Text = "Please select the valid location.";
                    errorDiv.Visible = true;
                    // MessageBox.Show("Please select the valid location.", "Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TextBoxLocation.Text = "";
                    return;
                }
                if (_isAllLocation == false)
                {
                    _basePage = new Base();

                    TextBoxLocationDes.Text = GetLoc_HIRC_SearchDesc(41, TextBoxLocation.Text.ToUpper());
                    TextBoxLocationDes.ToolTip = GetLoc_HIRC_SearchDesc(41, TextBoxLocation.Text.ToUpper());
                    this.imgProCeSearch_Click(null, null);
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

        protected void TextBoxChannelDes_TextChanged(object sender, EventArgs e)
        {

        }
        private void DispMsg(string msgText, string msgType)
        {
            msgText = msgText.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");

            if (msgType == "N")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + msgText + "');", true);
            }
            else if (msgType == "W")
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

        public void DisableHierachyDetails()
        {
            TextBoxCompany.ReadOnly = true;
            ImgBtnAccountNo.Visible = false;

            TextBoxChannel.ReadOnly = true;
            imgChaSearch.Visible = false;

            TextBoxSubChannel.ReadOnly = true;
            imgSubChaSearch.Visible = false;

            TextBoxArea.ReadOnly = true;
            imgAreaSearch.Visible = false;

            TextBoxRegion.ReadOnly = true;
            imgRegionSearch.Visible = false;

            TextBoxZone.ReadOnly = true;
            imgZoneSearch.Visible = false;
        }

        public void EnableHierachyDetails()
        {
            TextBoxCompany.ReadOnly = false;
            ImgBtnAccountNo.Visible = true;

            TextBoxChannel.ReadOnly = false;
            imgChaSearch.Visible = true;

            TextBoxSubChannel.ReadOnly = false;
            imgSubChaSearch.Visible = true;

            TextBoxArea.ReadOnly = false;
            imgAreaSearch.Visible = true;

            TextBoxRegion.ReadOnly = false;
            imgRegionSearch.Visible = true;

            TextBoxZone.ReadOnly = false;
            imgZoneSearch.Visible = true;
        }

    }
}