using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.MasterFiles.Organization
{
    public partial class LocationMaster : BasePage
    {
        bool _showExcelPop
        {
            get { if (Session["_showExcelPop"] != null) { return (bool)Session["_showExcelPop"]; } else { return false; } }
            set { Session["_showExcelPop"] = value; }
        }

        string _filPath
        {
            get { if (Session["_filPath"] != null) { return (string)Session["_filPath"]; } else { return ""; } }
            set { Session["_filPath"] = value; }
        }

        bool _showProcPop
        {
            get { if (Session["_showProcPop"] != null) { return (bool)Session["_showProcPop"]; } else { return false; } }
            set { Session["_showProcPop"] = value; }
        }



        MasterLocationNew _loc { get { return (MasterLocationNew)Session["_loc"]; } set { Session["_loc"] = value; } }
        string _searchDdl { get { return (string)Session["_searchDdl"]; } set { Session["_searchDdl"] = value; } }
        private bool _isDisplayRawData = false;
        //  string userid = Session["UserID"].ToString();
        // string session = Session["SessionID"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    // pageClear();
                    TextBox16.Text = "LK";
                    TextBox1.Text = Session["UserCompanyCode"].ToString();
                    TextBox1_TextChanged(null, null);
                    //   TextBox16.ToolTip = "Sri Lanka";
                    // showDescription();
                    TextBox16.ToolTip = Labelcountry.Text;
                    TextBox19.ToolTip = Labelprovince.Text;
                    Labelcurrent.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                    TextBox26.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                    hdfCurrDate.Value = DateTime.Now.ToString("dd/MMM/yyyy");
                    BindDropDown();
                    BindMain();
                    CheckBox1.Checked = false;
                    TextBox9.Text = "";
                    TextBox9.Enabled = false;
                    TextBox11.Enabled = false;
                    TextBox12.Enabled = false;
                    commDtSet.Visible = false;
                }
                catch (Exception)
                {
                    Msg("Error Occurred while processing !!! ", "E");
                }
            }
        }

        private void BindDropDown()
        {
            DataTable dt = CHNLSVC.Inventory.LoacationType();
            DataView dv = dt.DefaultView;
            dv.Sort = "rlt_desc ASC";
            dt = dv.ToTable();
            DropDownList2.DataSource = dt;
            DropDownList2.DataValueField = "rlt_cd";
            DropDownList2.DataTextField = "rlt_desc";
            DropDownList2.DataBind();

            dt = new DataTable();
            dt = CHNLSVC.Inventory.GetCategeryType();
            dv = dt.DefaultView;
            dv.Sort = "rlc_desc ASC";
            dt = dv.ToTable();
            DropDownList3.DataSource = dt;
            DropDownList3.DataTextField = "rlc_desc";
            DropDownList3.DataValueField = "rlc_cd";
            DropDownList3.DataBind();

            //dt = new DataTable();
            //dt = CHNLSVC.CommonSearch.SearchMstGradeTypes(new GradeMaster() { Mg_com = Session["UserCompanyCode"].ToString(), Mg_chnl = TextBox3.Text!=""?TextBox3.Text:null });
            //dv = dt.DefaultView;
            //dv.Sort = "MG_DESC ASC";
            //dt = dv.ToTable();
            //while (DropDownList10.Items.Count > 1)
            //{
            //    DropDownList10.Items.RemoveAt(1);
            //}
            //DropDownList10.DataSource = dt;
            //DropDownList10.DataValueField = "mg_CD";
            //DropDownList10.DataTextField = "MG_DESC";
            //DropDownList10.DataBind();
            BindWhCd();

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company);
            DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, null, null);
            DropDownList7.DataSource = _result;
            DropDownList7.DataValueField = "Code";
            DropDownList7.DataTextField = "Description";
            DropDownList7.DataBind();
        }
        private void BindGradeTp()
        {
            while (DropDownList10.Items.Count > 1)
            {
                DropDownList10.Items.RemoveAt(1);
            }
            if (!string.IsNullOrEmpty(TextBox3.Text))
            {
                DataTable dt = CHNLSVC.CommonSearch.SearchMstGradeTypes(new GradeMaster() { Mg_com = Session["UserCompanyCode"].ToString(), Mg_chnl = TextBox3.Text.ToUpper() });
                DataView dv = dt.DefaultView;
                dv.Sort = "MG_DESC ASC";
                dt = dv.ToTable();
                DropDownList10.DataSource = dt;
                DropDownList10.DataValueField = "mg_CD";
                DropDownList10.DataTextField = "MG_DESC";
                DropDownList10.DataBind();
            }
        }
        private void BindWhCd()
        {
            while (DropDownList11.Items.Count > 1)
            {
                DropDownList11.Items.RemoveAt(1);
            }
            if (DropDownList7.SelectedIndex > 0)
            {

                DataTable dt = CHNLSVC.Inventory.warehouse_company(DropDownList7.SelectedValue);
                DropDownList11.DataSource = dt;
                DropDownList11.DataTextField = "mw_cd";
                DropDownList11.DataTextField = "mw_cd";
                DropDownList11.DataBind();
            }
        }

        private void BindMain()
        {
            //while (DropDownList3.Items.Count > 1)
            //{
            //    DropDownList3.Items.RemoveAt(1);
            //}
            //DataTable dt= CHNLSVC.CommonSearch.SearchRefLocCate1();
            //if (dt.Rows.Count>0)
            //{
            //    DropDownList3.DataSource = dt;
            //    DropDownList3.DataValueField = "RLC_CD";
            //    DropDownList3.DataTextField = "RLC_DESC";
            //    DropDownList3.DataBind();
            //}
        }
        private void showDescription()
        {
            TextBox16.ToolTip = Labelcountry.Text;
            TextBox19.ToolTip = Labelprovince.Text;
            TextBox22.ToolTip = LabelDistrict.Text;
            TextBoxtown.ToolTip = Labeltown.Text;
        }

        //public void pageClear()
        //{
        //   gvSubSerial.DataSource = new int[] { };
        //   gvSubSerial.DataBind();
        //}


        public void clear()
        {

            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
            TextBox14.Text = "";
            TextBox17.Text = "";
            TextBox20.Text = "";
            TextBox13.Text = "";
            TextBox21.Text = "";
            TextBox23.Text = "";
            TextBoxtown.Text = "";
            TextBox16.Text = "";
            TextBox19.Text = "";
            TextBox22.Text = "";
            TextBox18.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";
            TextBox12.Text = "";
            Labelcompanyd.Text = "";
            TextBox26.Text = "";
            TextBox25.Text = "";
            TextBox26.Text = "";
            Labelchanel.Text = "";
            errorDiv.Visible = false;
            DropDownList7.SelectedIndex = 0;
            DropDownList11.SelectedIndex = 0;
            DropDownList2.SelectedIndex = 0;
            DropDownList3.SelectedIndex = 0;
            DropDownList10.SelectedIndex = 0;
            CheckBox3.Checked = false;
            CheckBoxpda.Checked = false;
            chkAutoIn.Checked = false;
            commDtSet.Visible = false;
            TextBox24.Text = "";
        }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDisplayRawData
        {
            get { return _isDisplayRawData; }
            set { _isDisplayRawData = value; }
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            //
            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.Country:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Country.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Province:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Province.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.District:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.District.ToString() + seperator);
                        break;
                    }


                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel:
                    {
                        paramsText.Append(TextBox1.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel:
                    {
                        //ucLCSE001
                        if (_isDisplayRawData == false)
                            paramsText.Append(TextBox1.Text + seperator + TextBox3.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel.ToString() + seperator);
                        else
                            paramsText.Append(TextBox1.Text + seperator + string.Empty + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel.ToString() + seperator);
                        break;

                    }

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
                case CommonUIDefiniton.SearchUserControlType.OPE:
                    {
                        paramsText.Append(TextBox1.Text + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.District:
                //    {
                //        //ucLCSE001
                //        if (_isDisplayRawData == false)
                //            paramsText.Append(TextBox16.Text + seperator + TextBox19.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.District.ToString() + seperator);
                //        else
                //            paramsText.Append(TextBox16.Text + seperator + string.Empty + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.District.ToString() + seperator);
                //        break;

                //    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        //paramsText.Append(txtCompany.Text.Trim() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area:
                    {
                        //ucLCSE001
                        if (_isDisplayRawData == false)
                            paramsText.Append(TextBox1.Text + seperator + TextBox3.Text + seperator + TextBox4.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area.ToString() + seperator);
                        else
                            paramsText.Append(TextBox1.Text + seperator + string.Empty + seperator + string.Empty + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area.ToString() + seperator);
                        break;
                    }



                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(TextBox1.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Channel:
                    {
                        paramsText.Append(TextBox1.Text.Trim() + seperator + "1" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub3:
                    {
                        paramsText.Append(TextBox1.Text.Trim() + seperator + "1" + seperator);
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


        public void BindUCtrlDDLData_LOC(DataTable _dataSource)
        {
            this.DropDownList9.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.DropDownList9.Items.Add(col.ColumnName);
            }
            this.DropDownList9.SelectedIndex = 0;
            if (!string.IsNullOrEmpty(_searchDdl))
            {
                if (_searchDdl == "20" || _searchDdl == "21" || _searchDdl == "23" || _searchDdl == "24")
                {
                    DropDownList9.SelectedIndex = DropDownList9.Items.IndexOf(DropDownList9.Items.FindByValue("Description"));
                    if (DropDownList9.SelectedIndex == 0)
                    {
                        DropDownList9.SelectedIndex = DropDownList9.Items.IndexOf(DropDownList9.Items.FindByValue("DESCRIPTION"));
                    }
                }
            }
        }

        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            ViewState["company"] = null;

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company);
            DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, null, null);
            dvResult.DataSource = _result;
            dvResult.DataBind();
            ViewState["company"] = _result;
            Label8.Text = "35";
            BindUCtrlDDLData(_result);
            txtSearchbyword.Text = "";
            txtSearchbyword.Focus();
            UserPopoup.Show();
        }

        public bool IsRawData = false;
        DataTable _result;
        protected void ImgSearch_Click(object sender, EventArgs e)
        {







            if (Label8.Text == "35")
            {
                _result = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company);
                _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text.ToString());
                dvResult.DataSource = _result;
                BindUCtrlDDLData(_result);
                dvResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                UserPopoup.Show();

            }
            else if (Label8.Text == "36")
            {

                _result = null;
                if (IsDisplayRawData)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                    IsRawData = true;
                    _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                    IsRawData = false;
                    _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                dvResult.DataSource = null;
                dvResult.DataSource = _result;
                dvResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                UserPopoup.Show();
            }

            else if (Label8.Text == "37")
            {

                _result = null;

                if (IsDisplayRawData)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
                    IsRawData = true;
                    _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
                    IsRawData = false;
                    //Add by Lakshan as per the Asanka 08 Nov 2016
                    if (SearchParams.Contains(TextBox3.Text))
                    {
                        SearchParams = SearchParams.Replace(TextBox3.Text, "");
                    }
                    _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                }
                dvResult.DataSource = null;
                dvResult.DataSource = _result;
                dvResult.DataBind();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                UserPopoup.Show();


            }

            if (Label8.Text == "UserLocation")
            {
                _result = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text.ToString());
                dvResult.DataSource = _result;
                BindUCtrlDDLData(_result);
                dvResult.DataBind();
                ViewState["UserLocation"] = _result;
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                UserPopoup.Show();

            }
            if (Label8.Text == "MainUserLocation")
            {
                _result = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text.ToString());
                dvResult.DataSource = _result;
                BindUCtrlDDLData(_result);
                dvResult.DataBind();
                ViewState["UserLocation"] = _result;
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                UserPopoup.Show();

            }
            if (Label8.Text == "70")
            {
                _result = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OPE);
                _result = CHNLSVC.CommonSearch.SearchOperation(SearchParams, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text.ToString());
                dvResult.DataSource = _result;
                BindUCtrlDDLData(_result);
                dvResult.DataBind();
                ViewState["opcd"] = _result;
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                UserPopoup.Show();

            }
            if (Label8.Text == "Channel")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Channel);
                DataTable _dt = CHNLSVC.CommonSearch.SearchMstChnl(SearchParams, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text.ToString());
                dvResult.DataSource = _dt;
                //BindUCtrlDDLData(_dt);
                dvResult.DataBind();
                ViewState["Channel"] = _dt;
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                UserPopoup.Show();

            }

            if (Label8.Text == "CAT_Sub3")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
                DataTable _dt = CHNLSVC.CommonSearch.SearchRefLocCate3(SearchParams, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text.ToString());
                dvResult.DataSource = _dt;
                //  BindUCtrlDDLData(_dt);
                dvResult.DataBind();
                ViewState["CAT_Sub3"] = _dt;
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                UserPopoup.Show();

            }
            if (Label8.Text == "CAT_Sub3")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
                DataTable _dt = CHNLSVC.CommonSearch.SearchRefLocCate3(SearchParams, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text.ToString());
                dvResult.DataSource = _dt;
                //  BindUCtrlDDLData(_dt);
                dvResult.DataBind();
                ViewState["CAT_Sub3"] = _dt;
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                UserPopoup.Show();

            }
            if (Label8.Text == "178")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
                DataTable _dt = CHNLSVC.CommonSearch.Get_All_Users_Loc(SearchParams, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text.ToString());
                dvResult.DataSource = _dt;
                //  BindUCtrlDDLData(_dt);
                dvResult.DataBind();
                ViewState["contactuser"] = _dt;
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                UserPopoup.Show();

            }

        }

        protected void ImgSearchLoc_Click(object sender, EventArgs e)
        {

            /*  BindUCtrlDDLData_LOC(_result);
              ViewState["Country"] = _result;
              LabelLoc.Text = "20";
             cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
             
              ViewState["province"] = null;
            string param=SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Province);
            DataTable _result = _basePage.CHNLSVC.CommonSearch.GetProvinceData(param, null, null);
             
                ViewState["district"] = null;
            //string param = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.District);
            //DataTable _result = _basePage.CHNLSVC.CommonSearch.GetDistrictByProvinceData(param,null,null);
             
             */




            if (LabelLoc.Text == "20")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
                _result = CHNLSVC.CommonSearch.GetCountrySearchData(SearchParams, DropDownList9.SelectedItem.ToString(), TextBox15.Text.ToString() + "%");
                dvResult_Loc.DataSource = _result;
                dvResult_Loc.DataBind();
                BindUCtrlDDLData_LOC(_result);
                TextBox15.Text = "";
                TextBox15.Focus();
                UserPopoupLocation.Show();

            }
            else if (LabelLoc.Text == "21")
            {
                ViewState["province"] = null;
                string param = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Province);
                DataTable _result = CHNLSVC.CommonSearch.GetProvinceData(param, DropDownList9.SelectedItem.ToString(), TextBox15.Text.ToString() + "%");
                dvResult_Loc.DataSource = _result;
                dvResult_Loc.DataBind();
                BindUCtrlDDLData_LOC(_result);
                TextBox15.Text = "";
                TextBox15.Focus();
                UserPopoupLocation.Show();

            }
            else if (LabelLoc.Text == "23")
            {
                DataTable dt = (DataTable)ViewState["district"];
                //= null;
                // string param = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.District);
                //  DataTable _result = _basePage.CHNLSVC.CommonSearch.GetDistrictByProvinceData(param, DropDownList9.SelectedItem.ToString(), TextBox15.Text.ToString() + "%");
                if (DropDownList9.SelectedItem.Text == "District")
                {
                    try
                    {
                        //DataTable _result = CHNLSVC.Inventory.s_districtDetails(TextBox15.Text + "%", "", TextBox19.Text);
                        string searchStr = "District like '" + TextBox15.Text + "*'";
                        DataRow[] drPaytable = dt.Select(searchStr);
                        DataTable tempDt = new DataTable();
                        tempDt = dt.Clone();
                        foreach (DataRow dr in drPaytable)
                        {
                            tempDt.ImportRow(dr);
                        }
                        dvResult_Loc.DataSource = tempDt;
                        dvResult_Loc.DataBind();
                        BindUCtrlDDLData_LOC(tempDt);
                        ViewState["district"] = tempDt;
                        TextBox15.Text = "";
                        TextBox15.Focus();
                        UserPopoupLocation.Show();
                    }
                    catch (Exception ex)
                    {

                    }
                }

                else if (DropDownList9.SelectedItem.Text == "Description")
                {
                    try
                    {
                        //DataTable _result = CHNLSVC.Inventory.s_districtDetails("", TextBox15.Text + "%", TextBox19.Text);

                        string searchStr = "Description like '" + TextBox15.Text + "*'";
                        DataRow[] drPaytable = dt.Select(searchStr);
                        DataTable tempDt = new DataTable();
                        tempDt = dt.Clone();
                        foreach (DataRow dr in drPaytable)
                        {
                            tempDt.ImportRow(dr);
                        }

                        dvResult_Loc.DataSource = tempDt;
                        dvResult_Loc.DataBind();
                        BindUCtrlDDLData_LOC(tempDt);
                        ViewState["district"] = tempDt;
                        TextBox15.Text = "";
                        TextBox15.Focus();
                        UserPopoupLocation.Show();
                    }
                    catch (Exception ex) { }

                }

                else if (DropDownList9.SelectedItem.Text == "Province")
                {
                    try
                    {
                        //DataTable _result = CHNLSVC.Inventory.s_districtDetails("", "", TextBox15.Text + "%");
                        string searchStr = "Province like '" + TextBox15.Text + "*'";
                        DataRow[] drPaytable = dt.Select(searchStr);
                        DataTable tempDt = new DataTable();
                        tempDt = dt.Clone();
                        foreach (DataRow dr in drPaytable)
                        {
                            tempDt.ImportRow(dr);
                        }
                        dvResult_Loc.DataSource = tempDt;
                        dvResult_Loc.DataBind();
                        BindUCtrlDDLData_LOC(tempDt);
                        ViewState["district"] = tempDt;
                        TextBox15.Text = "";
                        TextBox15.Focus();
                        UserPopoupLocation.Show();
                    }
                    catch (Exception ex) { }

                }



            }

            else if (LabelLoc.Text == "24")
            {

                DataTable dt = (DataTable)ViewState["Town"];
                if (DropDownList9.SelectedItem.Text == "Country")
                {
                    try
                    {
                        //DataTable st = CHNLSVC.Inventory.searchtowndata(TextBox15.Text+"%", "", "", "", "");
                        string searchStr = "Country like '" + TextBox15.Text + "*'";
                        DataRow[] drPaytable = dt.Select(searchStr);
                        DataTable tempDt = new DataTable();
                        tempDt = dt.Clone();
                        foreach (DataRow dr in drPaytable)
                        {
                            tempDt.ImportRow(dr);
                        }
                        dvResult_Loc.DataSource = tempDt;
                        dvResult_Loc.DataBind();
                        BindUCtrlDDLData_LOC(tempDt);
                        ViewState["Town"] = tempDt;
                    }
                    catch (Exception ex) { }
                }
                else if (DropDownList9.SelectedItem.Text == "Description")
                {
                    try
                    {
                        //DataTable st = CHNLSVC.Inventory.searchtowndata("", TextBox15.Text+"%", "", "", "");
                        string searchStr = "Description like '" + TextBox15.Text + "*'";
                        DataRow[] drPaytable = dt.Select(searchStr);
                        DataTable tempDt = new DataTable();
                        tempDt = dt.Clone();
                        foreach (DataRow dr in drPaytable)
                        {
                            tempDt.ImportRow(dr);
                        }
                        dvResult_Loc.DataSource = tempDt;
                        dvResult_Loc.DataBind();
                        BindUCtrlDDLData_LOC(tempDt);
                        ViewState["Town"] = tempDt;
                    }
                    catch (Exception ex)
                    { }
                }

                else if (DropDownList9.SelectedItem.Text == "Province")
                {
                    try
                    {
                        //DataTable st = CHNLSVC.Inventory.searchtowndata("", "", TextBox15.Text+"%", "", "");
                        string searchStr = "Province like '" + TextBox15.Text + "*'";
                        DataRow[] drPaytable = dt.Select(searchStr);
                        DataTable tempDt = new DataTable();
                        tempDt = dt.Clone();
                        foreach (DataRow dr in drPaytable)
                        {
                            tempDt.ImportRow(dr);
                        }
                        dvResult_Loc.DataSource = tempDt;
                        dvResult_Loc.DataBind();
                        BindUCtrlDDLData_LOC(tempDt);
                        ViewState["Town"] = tempDt;
                    }
                    catch (Exception ex) { }

                }
                else if (DropDownList9.SelectedItem.Text == "District")
                {
                    try
                    {
                        //DataTable st = CHNLSVC.Inventory.searchtowndata("", "", "", TextBox15.Text+"%","");
                        string searchStr = "District like '" + TextBox15.Text + "*'";
                        DataRow[] drPaytable = dt.Select(searchStr);
                        DataTable tempDt = new DataTable();
                        tempDt = dt.Clone();
                        foreach (DataRow dr in drPaytable)
                        {
                            tempDt.ImportRow(dr);
                        }
                        dvResult_Loc.DataSource = tempDt;
                        dvResult_Loc.DataBind();
                        BindUCtrlDDLData_LOC(tempDt);
                        ViewState["Town"] = tempDt;
                    }
                    catch (Exception ex)
                    {
                        //dvResult_Loc.DataSource = new int[]{};
                        //dvResult_Loc.DataBind();
                    }
                }

                else if (DropDownList9.Text == "Town")
                {
                    try
                    {
                        //DataTable st = CHNLSVC.Inventory.searchtowndata("", "", "", "", TextBox15.Text+"%");
                        string searchStr = "Town like '" + TextBox15.Text + "*'";
                        DataRow[] drPaytable = dt.Select(searchStr);
                        DataTable tempDt = new DataTable();
                        tempDt = dt.Clone();
                        foreach (DataRow dr in drPaytable)
                        {
                            tempDt.ImportRow(dr);
                        }
                        dvResult_Loc.DataSource = tempDt;
                        dvResult_Loc.DataBind();
                        BindUCtrlDDLData_LOC(tempDt);
                        ViewState["Town"] = tempDt;
                    }
                    catch (Exception ex) { }

                }


            }



        }

        protected void dvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvResult.PageIndex = e.NewPageIndex;
            dvResult.DataSource = null;
            if (Label8.Text == "35")
            {
                dvResult.DataSource = (DataTable)ViewState["company"];
                dvResult.DataBind();

                UserPopoup.Show();
            }
            else if (Label8.Text == "36")
            {
                dvResult.DataSource = (DataTable)ViewState["chanel"];
                dvResult.DataBind();
                UserPopoup.Show();

            }
            else if (Label8.Text == "37")
            {
                dvResult.DataSource = (DataTable)ViewState["subchanel"];
                dvResult.DataBind();
                UserPopoup.Show();
            }

            else if (Label8.Text == "70")
            {
                dvResult.DataSource = (DataTable)ViewState["opcd"];
                dvResult.DataBind();
                UserPopoup.Show();
            }
            else if (Label8.Text == "UserLocation")
            {
                dvResult.DataSource = (DataTable)ViewState["UserLocation"];
                dvResult.DataBind();
                UserPopoup.Show();
            }
            else if (Label8.Text == "MainUserLocation")
            {
                dvResult.DataSource = (DataTable)ViewState["UserLocation"];
                dvResult.DataBind();
                UserPopoup.Show();
            }
            else if (Label8.Text == "Channel")
            {
                dvResult.DataSource = (DataTable)ViewState["Channel"];
                dvResult.DataBind();
                UserPopoup.Show();
            }
            else if (Label8.Text == "CAT_Sub3")
            {
                dvResult.DataSource = (DataTable)ViewState["CAT_Sub3"];
                dvResult.DataBind();
                UserPopoup.Show();
            }
            else if (Label8.Text == "178")
            {
                dvResult.DataSource = (DataTable)ViewState["contactuser"];
                dvResult.DataBind();
                UserPopoup.Show();
            }

        }

        protected void dvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Label8.Text == "35")
            {
                TextBox1.Text = dvResult.SelectedRow.Cells[1].Text;
                TextBox1_TextChanged(null, null);
            }
            else if (Label8.Text == "36")
            {
                TextBox3.Text = dvResult.SelectedRow.Cells[1].Text;
                TextBox3_TextChanged(null, null);

            }
            else if (Label8.Text == "37")
            {
                TextBox4.Text = dvResult.SelectedRow.Cells[1].Text;
                TextBox4_TextChanged(null, null);
            }

            else if (Label8.Text == "70")
            {
                TextBox2.Text = dvResult.SelectedRow.Cells[1].Text;
                TextBox2_TextChanged(null, null);
            }
            else if (Label8.Text == "UserLocation")
            {
                TextBox5.Text = dvResult.SelectedRow.Cells[1].Text;
                TextBox5_TextChanged(null, null);
            }
            else if (Label8.Text == "MainUserLocation")
            {
                TextBox9.Text = dvResult.SelectedRow.Cells[1].Text;
                TextBox9_TextChanged(null, null);
            }
            else if (Label8.Text == "178")
            {
                TextBox23.Text = dvResult.SelectedRow.Cells[1].Text;
                TextBox24.Text = dvResult.SelectedRow.Cells[2].Text;
                TextBox18.Text = dvResult.SelectedRow.Cells[3].Text;
                TextBox13.Text = dvResult.SelectedRow.Cells[4].Text;
            }
            //else if (Label8.Text == "Channel")
            //{
            //    txtChannelCat.Text = dvResult.SelectedRow.Cells[1].Text;
            //    txtChannelCat_TextChanged(null, null);
            //}
            //else if (Label8.Text == "CAT_Sub3")
            //{
            //    txtOtherCat.Text = dvResult.SelectedRow.Cells[1].Text;
            //    txtOtherCat_TextChanged(null, null);
            //}
        }

        protected void LinkButton9_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            string SearchParams;
            if (TextBox1.Text.Trim() == "")
            {
                displayMessage_warning("Enter Company Code");
                // lblCError.Visible = true;
                //lblWarn.Text = "Enter Company Code.";
                //errorDiv.Visible = true;

                //MessageBox.Show("Enter Company Code");
                return;
            }



            DataTable _result = null;
            ViewState["chanel"] = null;
            if (IsDisplayRawData)
            {
                SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                bool IsRawData = true;
                _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, null, null);
            }
            else
            {
                SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                bool IsRawData = false;
                _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, null, null);
            }

            dvResult.DataSource = _result;

            dvResult.DataBind();
            BindUCtrlDDLData(_result);
            // UserPopoup.Show();
            ViewState["chanel"] = _result;
            Label8.Text = "36";
            TextBox3.Focus();
            txtSearchbyword.Text = "";
            txtSearchbyword.Focus();
            UserPopoup.Show();
        }

        protected void LinkButton10_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            string SearchParams;
            bool IsRawData;

            if (string.IsNullOrEmpty(TextBox1.Text))
            {
                displayMessage_warning("Please select a company"); return;
            }
            if (string.IsNullOrEmpty(TextBox3.Text))
            {
                displayMessage_warning("Please select a channel"); return;
            }

            if (!CHNLSVC.General.CheckChannel(TextBox1.Text.Trim(), TextBox3.Text.Trim().ToUpper()))
            {
                displayMessage_warning("Please select a valid channel");
                TextBox3.Text = "";
                TextBox3.Focus();
                return;
            }
            else
            {
                DataTable _result = null;
                ViewState["subchanel"] = null;
                if (IsDisplayRawData)
                {
                    SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
                    IsRawData = true;
                    _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, null, null);
                }
                else
                {
                    SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
                    IsRawData = false;
                    //Add by Lakshan as per the Asanka 08 Nov 2016
                    if (SearchParams.Contains(TextBox3.Text))
                    {
                        SearchParams = SearchParams.Replace(TextBox3.Text, "");
                    }
                    _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, null, null);
                }
                dvResult.DataSource = _result;
                dvResult.DataBind();
                BindUCtrlDDLData(_result);
                // UserPopoup.Show();
                ViewState["subchanel"] = _result;
                Label8.Text = "37";
                TextBox4.Focus();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                UserPopoup.Show();
            }
        }

        public void country_details()
        {

            ViewState["Country"] = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
            DataTable _result = CHNLSVC.CommonSearch.GetCountrySearchData(SearchParams, "Code", TextBox16.Text);
            //dvResult_Loc.DataSource = _result;

            if (_result.Rows.Count == 0) { }
            else
            {
                TextBox16.ToolTip = Convert.ToString(_result.Rows[0].Field<string>("Description"));

            }

        }

        public void provincedetails()
        {
            try
            {

                ViewState["province"] = null;
                string param = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Province);
                DataTable _result = CHNLSVC.CommonSearch.GetProvinceData(param, "CODE", TextBox19.Text);
                dvResult_Loc.DataSource = _result;
                if (_result.Rows.Count == 0) { }
                else
                {
                    TextBox19.ToolTip = Convert.ToString(_result.Rows[0].Field<string>("Description"));
                }
            }
            catch (Exception ex) { }
        }

        public void DistrictDetails()
        {

            try
            {


                ViewState["province"] = null;
                //     string param = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Province);
                DataTable _result = CHNLSVC.Inventory.getdistrictDetails(TextBox19.Text, TextBox22.Text);
                if (_result.Rows.Count == 0) { }
                else
                {
                    TextBox22.ToolTip = Convert.ToString(_result.Rows[0].Field<string>("Description"));
                }

            }
            catch (Exception ex)
            {

            }
        }



        protected void LinkButton2_Click(object sender, EventArgs e)
        {


            ViewState["Country"] = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
            DataTable _result = CHNLSVC.CommonSearch.GetCountrySearchData(SearchParams, null, null);
            dvResult_Loc.DataSource = _result;

            if (_result.Rows.Count == 0)
            {
                displayMessage_warning("No Country found");
            }
            else
            {


                dvResult_Loc.DataBind();
                _searchDdl = "20";
                BindUCtrlDDLData_LOC(_result);
                ViewState["Country"] = _result;
                LabelLoc.Text = "20";

                TextBox15.Text = "";
                TextBox15.Focus();
                UserPopoupLocation.Show();
            }

        }

        protected void dvResult_Loc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvResult_Loc.PageIndex = e.NewPageIndex;

            _result = null;
            dvResult_Loc.DataSource = null;
            if (LabelLoc.Text == "20")
            {
                //  _result = null;
                //  string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
                //    _result = _basePage.CHNLSVC.CommonSearch.GetCountrySearchData(SearchParams, DropDownList9.SelectedItem.ToString(), "%" + TextBox15.Text.ToString());
                dvResult_Loc.DataSource = (DataTable)ViewState["Country"];
                dvResult_Loc.DataBind();
                TextBox15.Text = "";
                TextBox15.Focus();
                UserPopoupLocation.Show();
            }
            else if (LabelLoc.Text == "21")
            {
                dvResult_Loc.DataSource = (DataTable)ViewState["province"];
                dvResult_Loc.DataBind();
                TextBox15.Text = "";
                TextBox15.Focus();
                UserPopoupLocation.Show();
            }

            else if (LabelLoc.Text == "24")
            {

                dvResult_Loc.DataSource = (DataTable)ViewState["Town"];
                dvResult_Loc.DataBind();
                TextBox15.Text = "";
                TextBox15.Focus();
                UserPopoupLocation.Show();
            }

            else if (LabelLoc.Text == "23")
            {
                dvResult_Loc.DataSource = (DataTable)ViewState["district"];
                dvResult_Loc.DataBind();
                TextBox15.Text = "";
                TextBox15.Focus();
                UserPopoupLocation.Show();

            }


        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {


            if (TextBox16.Text == "")
            {
                displayMessage_warning("Enter valid Country");
            }

            else
            {
                ViewState["province"] = null;
                string param = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Province);
                DataTable _result = CHNLSVC.CommonSearch.GetProvinceData(param, null, null);
                dvResult_Loc.DataSource = _result;
                if (_result.Rows.Count == 0)
                {
                    displayMessage_warning("No Matching Province found");
                }
                else
                {
                    _searchDdl = "21";
                    BindUCtrlDDLData_LOC(_result);
                    dvResult_Loc.DataBind();
                    ViewState["province"] = _result;
                    LabelLoc.Text = "21";
                    TextBox15.Text = "";
                    TextBox15.Focus();
                    UserPopoupLocation.Show();
                }
            }
        }

        protected void dvResult_Loc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LabelLoc.Text == "20")
            {
                TextBox16.Text = dvResult_Loc.SelectedRow.Cells[1].Text;
                Labelcountry.Text = dvResult_Loc.SelectedRow.Cells[2].Text;

                UserPopoupLocation.Hide();
                TextBox16.ToolTip = Labelcountry.Text;

            }
            else if (LabelLoc.Text == "21")
            {
                TextBox19.Text = dvResult_Loc.SelectedRow.Cells[1].Text;
                Labelprovince.Text = dvResult_Loc.SelectedRow.Cells[2].Text;
                UserPopoupLocation.Hide();
                TextBox19.ToolTip = dvResult_Loc.SelectedRow.Cells[2].Text;
            }

            else if (LabelLoc.Text == "23")
            {
                TextBox22.Text = dvResult_Loc.SelectedRow.Cells[1].Text;
                LabelDistrict.Text = dvResult_Loc.SelectedRow.Cells[2].Text;
                UserPopoupLocation.Hide();
                TextBox22.ToolTip = dvResult_Loc.SelectedRow.Cells[2].Text;

            }

            else if (LabelLoc.Text == "24")
            {
                TextBoxtown.Text = dvResult_Loc.SelectedRow.Cells[1].Text;
                Labeltown.Text = dvResult_Loc.SelectedRow.Cells[1].Text;
                UserPopoupLocation.Hide();
                TextBoxtown.ToolTip = dvResult_Loc.SelectedRow.Cells[3].Text;

            }


        }

        public void chanel_tool()
        {
            try
            {
                DataTable sds = CHNLSVC.Inventory.getchaneldescription(TextBox1.Text, TextBox3.Text);
                if (sds.Rows.Count == 0)
                {
                    TextBox3.ToolTip = "";
                }
                else
                {
                    TextBox3.ToolTip = Convert.ToString(sds.Rows[0].Field<string>("msc_desc"));
                }
            }
            catch (Exception ex) { }

        }



        protected void LinkButtondistrict_Click(object sender, EventArgs e)
        {


            if (TextBox16.Text == "")
            {
                displayMessage_warning("Enter valid Country");
            }
            else if (TextBox19.Text == "")
            {
                displayMessage_warning("Enter Valid Province");
            }


            else
            {

                ViewState["district"] = null;
                //string param = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.District);
                //DataTable _result = CHNLSVC.CommonSearch.GetDistrictByProvinceData(param,null,null);
                DataTable _result = CHNLSVC.Inventory.getDistrictDetails(TextBox19.Text);


                dvResult_Loc.DataSource = _result;
                if (_result.Rows.Count == 0)
                {
                    displayMessage_warning("No Matching Districts Found ");
                }
                else
                {
                    _searchDdl = "23";
                    BindUCtrlDDLData_LOC(_result);
                    dvResult_Loc.DataBind();
                    ViewState["district"] = _result;
                    LabelLoc.Text = "23";
                    TextBox15.Text = "";
                    TextBox15.Focus();
                    UserPopoupLocation.Show();
                }
            }
        }


        protected void DropDownList2_Load(object sender, EventArgs e)
        {

            /*   _basePage= new Base();
               DataTable dt = _basePage.CHNLSVC.Inventory.LoacationType();
               DropDownList2.DataSource = dt;
               DropDownList2.DataValueField = "rlt_desc";
               DropDownList2.DataBind();
              */
            //     DataTable dt=
        }

        protected void DropDownList3_Load(object sender, EventArgs e)
        {
            /*   

               DataTable dt = _basePage.CHNLSVC.Inventory.GetCategeryType();
               DropDownList3.DataSource = dt;
               DropDownList3.DataValueField = "rlc_cd";
               DropDownList3.DataBind();

               */
        }

        protected void DropDownList10_Load(object sender, EventArgs e)
        {

            /* 
             DataTable dt = _basePage.CHNLSVC.Inventory.Get_Grade_types();
             DropDownList10.DataSource = dt;
             DropDownList10.DataValueField = "ml_buffer_grd";
             DropDownList10.DataBind();
             */



        }

        private void displayMessage_Success(string msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickySuccessToast", "showStickySuccessToast('" + msg + "');", true);

        }

        private void displayMessage_warning(string msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);

        }

        protected void LinkButton11_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox23.Text))
            {
                ModalPopupExtenderOutstanding.Show();
            }
            else
            {
                SaveLocation();
            }

            //   SaveLocation();           
        }
        protected void btnConOutfYes_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderOutstanding.Hide();
            SaveLocation();
        }
        protected void btnConfNoOut_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderOutstanding.Hide();
        }

        protected void btnConfClose_ClickExcel(object sender, EventArgs e)
        {
            ModalPopupExtenderOutstanding.Hide();
        }
        private void SaveLocation()
        {
            if (txtClearlconformmessageValue.Value == "1")
            {
                try
                {
                    #region validataion

                    //Dulaj 2018/Nov/30
                    #region checkContactPerson
                    SystemUser _systemUser = null;
                    TextBox23.Text = TextBox23.Text.ToUpper();
                    if (!(TextBox23.Text == "N/A" || TextBox23.Text == ""))
                    {
                        _systemUser = CHNLSVC.Security.GetUserByUserID(TextBox23.Text);
                        // Session["SearchID"] = _systemUser.Se_usr_id;
                        if (_systemUser == null)
                        {
                            //TextBox23.Text = string.Empty;
                            displayMessage_warning("Please Select Valid Contact Person.");
                            return;
                        }
                        else
                        {
                            if (_systemUser.Se_act == 0)
                            {
                                // TextBox23.Text = string.Empty;
                                displayMessage_warning("Selected user ID is Currently Inactivated!");
                                return;
                            }

                            if (_systemUser.Se_act == -1)
                            {
                                // TextBox23.Text = string.Empty;
                                displayMessage_warning("Selected user ID is Currently Locked!");
                                return;
                            }

                            if (_systemUser.Se_act == -2)
                            {
                                // TextBox23.Text = string.Empty;
                                displayMessage_warning("Selected user ID is Permanently Disabled");
                                return;
                            }
                        }
                    }
                    #endregion


                    if (string.IsNullOrEmpty(TextBox1.Text))
                    {
                        displayMessage_warning("Please select a company !"); return;
                    }
                    if (!validateinputString(TextBox1.Text))
                    {
                        displayMessage_warning("Invalid charactor found in company code.");
                        TextBox1.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(TextBox2.Text))
                    {
                        displayMessage_warning("Please select a operation !"); return;
                    }
                    if (!validateinputString(TextBox2.Text))
                    {
                        displayMessage_warning("Invalid charactor found in operation code.");
                        TextBox2.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(TextBox3.Text))
                    {
                        displayMessage_warning("Please select a channel !"); return;
                    }
                    if (!validateinputString(TextBox3.Text))
                    {
                        displayMessage_warning("Invalid charactor found in channel code.");
                        TextBox3.Focus();
                        return;
                    }

                    if (!validateinputString(TextBox4.Text))
                    {
                        displayMessage_warning("Invalid charactor found in Sub channel code.");
                        TextBox4.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(TextBox5.Text))
                    {
                        displayMessage_warning("Please enter a location code"); return;
                    }
                    if (!validateinputString(TextBox5.Text))
                    {
                        displayMessage_warning("Invalid charactor found in location code.");
                        TextBox5.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(TextBox6.Text))
                    {
                        displayMessage_warning("Please enter a ref"); return;
                    }
                    if (!validateinputString(TextBox6.Text))
                    {
                        displayMessage_warning("Invalid charactor found in ref code.");
                        TextBox6.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(TextBox7.Text))
                    {
                        displayMessage_warning("Please enter a location name"); return;
                    }
                    if (!validateinputStringWithSpace(TextBox7.Text))
                    {
                        displayMessage_warning("Invalid charactor found in location name.");
                        TextBox7.Focus();
                        return;
                    }

                    if (DropDownList2.SelectedIndex == 0)
                    {
                        displayMessage_warning("Please select a location type"); return;
                    }
                    if (DropDownList3.SelectedIndex == 0)
                    {
                        displayMessage_warning("Please select a location category"); return;
                    }
                    //if (DropDownList10.SelectedIndex == 0)
                    //{
                    //    displayMessage_warning("Please select a location grade"); return;
                    //}
                    if (CheckBox1.Checked)
                    {
                        if (string.IsNullOrEmpty(TextBox9.Text))
                        {
                            displayMessage_warning("Please select a main location code"); return;
                        }
                        if (!validateinputString(TextBox9.Text))
                        {
                            displayMessage_warning("Invalid charactor found in main location code.");
                            TextBox9.Focus();
                            return;
                        }
                    }
                    if (string.IsNullOrEmpty(TextBox14.Text))
                    {
                        displayMessage_warning("Please enter a address 1"); return;
                    }
                    if (!validateinputStringWithSpace(TextBox14.Text))
                    {
                        displayMessage_warning("Invalid charactor found in address 1.");
                        TextBox14.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(TextBox17.Text))
                    {
                        displayMessage_warning("Please enter a address 2"); return;
                    }
                    if (!validateinputStringWithSpace(TextBox17.Text))
                    {
                        displayMessage_warning("Invalid charactor found in address 2.");
                        TextBox17.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(TextBox18.Text))
                    {
                        //  displayMessage_warning("Please enter a email "); return;
                    }

                    if (string.IsNullOrEmpty(TextBox20.Text))
                    {
                        displayMessage_warning("Please enter a telephone # "); return;
                    }
                    if (!validateinputString(TextBox20.Text))
                    {
                        displayMessage_warning("Invalid charactor found in telephone no.");
                        TextBox20.Focus();
                        return;
                    }

                    //if (string.IsNullOrEmpty(TextBox13.Text))
                    //{
                    //    displayMessage_warning("Please enter a mobile # "); return;
                    //}
                    if (!validateinputString(TextBox13.Text))
                    {
                        displayMessage_warning("Invalid charactor found in mobile no.");
                        TextBox13.Focus();
                        return;
                    }

                    //if (string.IsNullOrEmpty(TextBox23.Text))
                    //{
                    //    displayMessage_warning("Please enter a contact person "); return;
                    //}
                    if (!validateinputString(TextBox23.Text))
                    {
                        displayMessage_warning("Invalid charactor found in contact person.");
                        TextBox23.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(TextBox16.Text))
                    {
                        displayMessage_warning("Please enter a country "); return;
                    }
                    if (!validateinputString(TextBox16.Text))
                    {
                        displayMessage_warning("Invalid charactor found in country code.");
                        TextBox16.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(TextBox19.Text))
                    {
                        displayMessage_warning("Please enter a province"); return;
                    }
                    if (!validateinputString(TextBox19.Text))
                    {
                        displayMessage_warning("Invalid charactor found in province code.");
                        TextBox19.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(TextBox22.Text))
                    {
                        displayMessage_warning("Please enter a district"); return;
                    }
                    if (!validateinputString(TextBox22.Text))
                    {
                        displayMessage_warning("Invalid charactor found in district code.");
                        TextBox22.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(TextBoxtown.Text))
                    {
                        displayMessage_warning("Please enter a town"); return;
                    }
                    if (!validateinputString(TextBoxtown.Text))
                    {
                        displayMessage_warning("Invalid charactor found in town code.");
                        TextBoxtown.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(TextBox26.Text))
                    {
                        displayMessage_warning("Please enter a commenced date "); return;
                    }
                    if (string.IsNullOrEmpty(TextBoxtown.Text))
                    {
                        displayMessage_warning("Please enter a town"); return;
                    }
                    if (!string.IsNullOrEmpty(TextBox10.Text))
                    {
                        if (!isValidDecimal(TextBox10.Text))
                        {
                            displayMessage_warning("Please enter a valid allow no of RCC"); return;
                        }
                        if (!(Convert.ToDecimal(TextBox10.Text) > 0))
                        {
                            displayMessage_warning("Please enter a valid allow no of RCC"); return;
                        }
                    }
                    if (DropDownList5.SelectedValue == "1")
                    {
                        if (string.IsNullOrEmpty(TextBox11.Text))
                        {
                            displayMessage_warning("Please enter insured value"); return;
                        }
                        else
                        {
                            if (!isValidDecimal(TextBox11.Text))
                            {
                                displayMessage_warning("Please enter a valid insured value"); return;
                            }
                            if (!(Convert.ToDecimal(TextBox11.Text) > 0))
                            {
                                displayMessage_warning("Please enter a valid insured value"); return;
                            }
                        }
                    }
                    if (DropDownList6.SelectedValue == "1")
                    {
                        if (string.IsNullOrEmpty(TextBox12.Text))
                        {
                            displayMessage_warning("Please enter bank gurranty value"); return;
                        }
                        else
                        {
                            if (!isValidDecimal(TextBox12.Text))
                            {
                                displayMessage_warning("Please enter a valid bank gurranty value"); return;
                            }
                            if (!(Convert.ToDecimal(TextBox12.Text) > 0))
                            {
                                displayMessage_warning("Please enter a valid bank gurranty value"); return;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(TextBox18.Text))
                    {
                        if (!isValidEmail(TextBox18.Text))
                        {
                            displayMessage_warning("Please enter a valid email "); return;
                        }
                    }
                    if (!string.IsNullOrEmpty(TextBox13.Text))
                    {
                        if (TextBox13.Text.Length > 15)
                        {
                            displayMessage_warning("Maximum 15 characters are allowed for mobile # "); return;
                        }
                    }
                    if (!string.IsNullOrEmpty(TextBox21.Text))
                    {
                        if (TextBox21.Text.Length > 15)
                        {
                            displayMessage_warning("Maximum 15 characters are allowed for fax # "); return;
                        }
                    }
                    if (!string.IsNullOrEmpty(TextBox20.Text))
                    {
                        if (TextBox20.Text.Length > 15)
                        {
                            displayMessage_warning("Maximum 15 characters are allowed for telephone # "); return;
                        }
                    }
                    if (!string.IsNullOrEmpty(TextBox26.Text))
                    {
                        if (!isValidDate(TextBox26.Text))
                        {
                            displayMessage_warning("Please enter a valid commenced date "); return;
                        }
                        //if (Convert.ToDateTime(TextBox26.Text) < DateTime.Today)
                        //{
                        //    displayMessage_warning("Please enter a valid commenced date "); return;
                        //}
                    }
                    #endregion
                    _loc = new MasterLocationNew();
                    _loc.Ml_com_cd = TextBox1.Text.ToUpper();
                    _loc.Ml_ope_cd = TextBox2.Text.ToUpper();
                    _loc.Ml_loc_cd = TextBox5.Text.ToUpper();
                    _loc.Ml_loc_desc = TextBox7.Text.ToUpper();
                    _loc.Ml_ref = TextBox6.Text.ToUpper();
                    _loc.Ml_add1 = TextBox14.Text.ToUpper();
                    _loc.Ml_add2 = TextBox17.Text.ToUpper();
                    _loc.Ml_country_cd = TextBox16.Text.ToUpper();
                    _loc.Ml_province_cd = TextBox19.Text.ToUpper();
                    _loc.Ml_distric_cd = TextBox22.Text.ToUpper();
                    _loc.Ml_town_cd = TextBoxtown.Text.ToUpper();
                    _loc.Ml_tel = TextBox20.Text;
                    _loc.Ml_fax = TextBox21.Text;
                    _loc.Ml_email = TextBox18.Text;
                    //_loc.Ml_web = "";
                    _loc.Ml_contact = TextBox23.Text.ToUpper();

                    _loc.Ml_cate_1 = DropDownList3.SelectedIndex > 0 ? DropDownList3.SelectedValue : "";
                    _loc.Ml_cate_2 = TextBox3.Text.ToUpper();
                    _loc.Ml_cate_3 = TextBox4.Text.ToUpper();
                    _loc.Ml_buffer_grd = DropDownList10.SelectedIndex > 0 ? DropDownList10.SelectedValue : "";
                    _loc.Ml_is_sub_loc = CheckBox1.Checked ? 1 : 0; ;
                    _loc.Ml_main_loc_cd = TextBox9.Text.ToUpper();
                    _loc.Ml_is_online = DropDownList4.SelectedIndex > 0 ? Convert.ToInt32(DropDownList4.SelectedValue) : 0;
                    _loc.Ml_manager_cd = "N/A";
                    _loc.Ml_fwsale_qty = (Int32)ConvertIntZero(TextBox10.Text);
                    _loc.Ml_suspend = Convert.ToInt32(DropDownList8.SelectedValue);
                    _loc.Ml_act = Convert.ToInt32(DropDownList1.SelectedValue);
                    _loc.Ml_cre_by = Session["UserID"].ToString();
                    _loc.Ml_cre_dt = DateTime.Now;
                    _loc.Ml_mod_by = Session["UserID"].ToString();
                    _loc.Ml_mod_dt = DateTime.Now;
                    _loc.Ml_session_id = Session["SessionID"].ToString();
                    _loc.Ml_anal1 = "SCM2";
                    //_loc.Ml_anal2 = "";
                    _loc.Ml_anal3 = 0;
                    _loc.Ml_anal4 = 0;
                    //_loc.Ml_anal5 = "";
                    //_loc.Ml_anal6 = "";
                    _loc.Ml_loc_tp = DropDownList2.SelectedValue;
                    _loc.Ml_allow_bin = chkMaintaionBin.Checked ? 1 : 0; ;
                    _loc.Ml_def_pc = string.IsNullOrEmpty(TextBox8.Text) ? "N/A" : TextBox8.Text.ToUpper();
                    _loc.Ml_sev_chnl = TextBox3.Text.ToUpper();
                    _loc.Ml_auto_ain = chkAutoIn.Checked ? 1 : 0;
                    _loc.Ml_fx_loc = TextBox25.Text.ToUpper();
                    //_loc.Ml_scm2_st_dt = "";
                    _loc.Ml_is_chk_man_doc = 1;
                    _loc.Ml_mobi = TextBox13.Text;
                    _loc.Ml_comm_dt = Convert.ToDateTime(TextBox26.Text);
                    _loc.Ml_app_stk_val = ConvertDesZero(TextBox11.Text);
                    _loc.Ml_bank_grnt_val = ConvertIntZero(TextBox12.Text);
                    _loc.Ml_wh_com = DropDownList7.SelectedIndex > 0 ? DropDownList7.SelectedValue : "";
                    _loc.Ml_wh_cd = DropDownList11.SelectedIndex > 0 ? DropDownList11.SelectedValue : "";
                    _loc.Ml_is_serial = CheckBox3.Checked ? 1 : 0;
                    _loc.Ml_is_pda = CheckBoxpda.Checked ? 1 : 0;
                    _loc.Ml_auto_ain = chkAutoIn.Checked ? 1 : 0;
                    if (_loc != null)
                    {
                        Int32 _effect = 0;
                        string err = "";
                        MasterLocationNew _masterLocation = CHNLSVC.General.GetMasterLocation(new MasterLocationNew()
                        {
                            Ml_com_cd = TextBox1.Text,
                            Ml_loc_cd = TextBox5.Text.ToUpper(),
                            Ml_act = 1
                        });
                        _effect = CHNLSVC.General.UpdateLocationMasterNew(_loc, err);
                        //List<MasterLocationNew> masterlocationList = new List<MasterLocationNew>();
                        //masterlocationList.Add(_loc);
                        //CHNLSVC.General.Updatelogdetails(masterlocationList);

                        if (_effect == 1)
                        {
                            ClearAllData();
                            if (_masterLocation != null)
                            {
                                Msg("Location details updated successfully !!!", "S");
                            }
                            else
                            {
                                Msg("Location details saved successfully !!!", "S");
                            }
                        }
                        else if (_effect == 0)
                        {
                            //Msg("Error Occurred while processing !!! ", "E");
                        }
                        else
                        {
                            Msg("Error Occurred while processing !!! ", "E");
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
            }
        }
        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "1")
            {
                Response.Redirect(Request.RawUrl);
            }
            else { }
        }

        protected void LinkButtontown_Click(object sender, EventArgs e)
        {
            if (TextBox16.Text == "")
            {
                displayMessage_warning("Enter valid Country");
            }
            else if (TextBox19.Text == "")
            {
                displayMessage_warning("Enter Valid Province");
            }
            else if (TextBox22.Text == "")
            {
                displayMessage_warning("Enter Valid District");
            }

            else
            {

                ViewState["Town"] = null;
                string country = TextBox16.Text;
                string province = TextBox19.Text;
                string district = TextBox22.Text;
                DataTable _result = CHNLSVC.Inventory.getTownDetails(country, province, district);

                dvResult_Loc.DataSource = _result;

                if (_result.Rows.Count == 0)
                {
                    displayMessage_warning("No Matching Towns found");

                }
                else
                {
                    _searchDdl = "24";
                    BindUCtrlDDLData_LOC(_result);
                    dvResult_Loc.DataBind();
                    ViewState["Town"] = _result;
                    LabelLoc.Text = "24";
                    TextBox15.Text = "";
                    TextBox15.Focus();
                    UserPopoupLocation.Show();
                    //dvResult_Loc.DataSource
                }
            }
        }



        protected void LinkButton8_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "")
            {
                errorDiv.Visible = false;
                displayMessage_warning("Enter Company Code");
            }
            else
            {

                ViewState["opcd"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OPE);
                DataTable dt = CHNLSVC.CommonSearch.SearchOperation(para, null, null);
                dvResult.DataSource = dt;
                dvResult.DataBind();
                BindUCtrlDDLData(dt);
                Label8.Text = "70";
                ViewState["opcd"] = dt;
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                UserPopoup.Show();
            }


        }

        protected void TextBox5_TextChanged(object sender, EventArgs e)
        {
            BindGradeTp();
            //commDtSet.Visible = false;
            if (TextBox1.Text == "")
            {
                displayMessage_warning("Please select a company code");
                TextBox5.Text = "";
                return;
            }
            if (!validateinputString(TextBox5.Text))
            {
                displayMessage_warning("Invalid charactor found in location code.");
                TextBox5.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(TextBox5.Text))
            {
                MasterLocationNew maLocation = CHNLSVC.General.GetMasterLocation(new MasterLocationNew() { Ml_com_cd = TextBox1.Text, Ml_loc_cd = TextBox5.Text.ToUpper(), Ml_act = 0 });
                if (maLocation != null)
                {
                    LoadData(maLocation);
                }
            }
            else
            {
                ClearAllData();
            }
        }

        private void LoadData(MasterLocationNew maLocation)
        {
            try
            {
                TextBoxtown.Text = maLocation.Ml_town_cd;
                // TextBox1.Text=loc.Ml_com_cd;					
                TextBox2.Text = maLocation.Ml_ope_cd;
                TextBox2_TextChanged(null, null);
                TextBox3.Text = maLocation.Ml_cate_2;
                TextBox3_TextChanged(null, null);
                BindGradeTp();
                //  TextBox5.Text=loc.Ml_loc_cd;
                TextBox7.Text = maLocation.Ml_loc_desc;
                TextBox6.Text = maLocation.Ml_ref;
                TextBox14.Text = maLocation.Ml_add1;
                TextBox17.Text = maLocation.Ml_add2;
                TextBox16.Text = maLocation.Ml_country_cd;
                TextBox19.Text = maLocation.Ml_province_cd;
                TextBox22.Text = maLocation.Ml_distric_cd;
                TextBoxtown.Text = maLocation.Ml_town_cd;
                TextBox20.Text = maLocation.Ml_tel;
                TextBox21.Text = maLocation.Ml_fax;
                TextBox18.Text = maLocation.Ml_email;
                //_loc.Ml_web;
                TextBox23.Text = maLocation.Ml_contact;
                DropDownList3.SelectedIndex = DropDownList3.Items.IndexOf(DropDownList3.Items.FindByValue(maLocation.Ml_cate_1.ToString()));
                //TextBox3.Text = maLocation.Ml_cate_2;
                //TextBox3_TextChanged(null,null);
                TextBox4.Text = maLocation.Ml_cate_3;
                TextBox4_TextChanged(null, null);
                //_loc.Ml_buffer_grd;
                CheckBox1.Checked = maLocation.Ml_is_sub_loc == 1 ? true : false;
                TextBox9.Text = maLocation.Ml_main_loc_cd;
                TextBox9.Enabled = CheckBox1.Checked;
                DropDownList4.SelectedIndex = DropDownList4.Items.IndexOf(DropDownList4.Items.FindByValue(maLocation.Ml_is_online.ToString()));
                //_loc.Ml_manager_cd;
                TextBox10.Text = maLocation.Ml_fwsale_qty > 0 ? maLocation.Ml_fwsale_qty.ToString() : "";
                DropDownList8.SelectedIndex = DropDownList8.Items.IndexOf(DropDownList8.Items.FindByValue(maLocation.Ml_suspend.ToString()));
                // _loc.Ml_act;
                // Session["UserID"].ToString()=_loc.Ml_cre_by;
                //_loc.Ml_anal1;
                //_loc.Ml_anal2;
                //_loc.Ml_anal3;
                //_loc.Ml_anal4;
                //_loc.Ml_anal5;
                //_loc.Ml_anal6;
                DropDownList2.SelectedIndex = DropDownList2.Items.IndexOf(DropDownList2.Items.FindByValue(maLocation.Ml_loc_tp));
                DropDownList10.SelectedIndex = DropDownList10.Items.IndexOf(DropDownList10.Items.FindByValue(maLocation.Ml_buffer_grd));
                DropDownList3.SelectedIndex = DropDownList3.Items.IndexOf(DropDownList3.Items.FindByValue(maLocation.Ml_cate_1));
                chkMaintaionBin.Checked = maLocation.Ml_allow_bin == 1 ? true : false;
                TextBox8.Text = maLocation.Ml_def_pc;
                //_loc.Ml_auto_ain;
                TextBox25.Text = maLocation.Ml_fx_loc;
                //TextBox26.Text=maLocation.Ml_scm2_st_dt.Date.ToString("dd/MMM/yyyy");
                //_loc.Ml_is_chk_man_doc;
                TextBox13.Text = maLocation.Ml_mobi;
                if (maLocation.Ml_comm_dt.ToString() == "01/Jan/0001 12:00:00 AM")
                {
                    commDtSet.Visible = true;
                }
                else
                {
                    commDtSet.Visible = false;
                }
                TextBox26.Text = maLocation.Ml_comm_dt.Date.ToString("dd/MMM/yyyy");
                //_loc.Ml_comm_dt;
                DropDownList5.SelectedIndex = maLocation.Ml_app_stk_val > 0 ? 1 : 0;
                DropDownList5_SelectedIndexChanged(null, null);
                DropDownList6.SelectedIndex = maLocation.Ml_bank_grnt_val > 0 ? 1 : 0;
                DropDownList6_SelectedIndexChanged(null, null);
                TextBox11.Text = maLocation.Ml_app_stk_val > 0 ? maLocation.Ml_app_stk_val.ToString() : "";
                TextBox12.Text = maLocation.Ml_bank_grnt_val > 0 ? maLocation.Ml_bank_grnt_val.ToString() : "";
                DropDownList7.SelectedIndex = DropDownList7.Items.IndexOf(DropDownList7.Items.FindByValue(maLocation.Ml_wh_com));
                BindWhCd();
                DropDownList11.SelectedIndex = DropDownList11.Items.IndexOf(DropDownList11.Items.FindByValue(maLocation.Ml_wh_cd));
                CheckBox3.Checked = maLocation.Ml_is_serial == 1 ? true : false;
                CheckBoxpda.Checked = maLocation.Ml_is_pda == 1 ? true : false;
                chkAutoIn.Checked = maLocation.Ml_auto_ain == 1 ? true : false;
                DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(maLocation.Ml_act.ToString()));

                //Dulaj 2018/Nov/30

                loadContactDetaisl(maLocation.Ml_contact);
                //
                // TextBox4.Text=
            }
            catch (Exception e)
            {
                Msg("Error Occurred while processing !!! ", "E");
            }
        }

        private void ClearAllData()
        {
            // TextBox1.Text = "";
            TextBox2.Text = "";
            // TextBox5.Text = "";
            TextBox7.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox14.Text = "";
            TextBox17.Text = "";
            TextBox16.Text = "";
            TextBox19.Text = "";
            TextBox22.Text = "";
            TextBoxtown.Text = "";
            TextBox20.Text = "";
            TextBox21.Text = "";
            TextBox13.Text = "";
            TextBox23.Text = "";
            CheckBox1.Checked = false;
            TextBox9.Text = "";
            TextBox10.Text = "";
            DropDownList8.SelectedIndex = 0;
            DropDownList2.SelectedIndex = 0;
            DropDownList3.SelectedIndex = 0;
            DropDownList5.SelectedIndex = 0;
            DropDownList5_SelectedIndexChanged(null, null);
            DropDownList6.SelectedIndex = 0;
            DropDownList6_SelectedIndexChanged(null, null);
            DropDownList4.SelectedIndex = 0;
            DropDownList7.SelectedIndex = 0;
            DropDownList11.SelectedIndex = 0;
            TextBox8.Text = "";
            TextBox13.Text = "";
            CheckBox3.Checked = false;
            CheckBoxpda.Checked = false;
            chkAutoIn.Checked = false;
            Labelchanel.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            Labelsubchanel.Text = "";
            TextBox18.Text = "";
            TextBox26.Text = "";
            TextBox25.Text = "";
            TextBox26.Text = "";
            TextBox26.Text = "";
            TextBox11.Text = "";
            TextBox12.Text = "";
            Labelcompanyd.Text = "";
            Labelchanel.Text = "";
            lblOperation.Text = "";
            Labelsubchanel.Text = "";
            TextBox24.Text = "";
            DropDownList10.SelectedIndex = 0;

            commDtSet.Visible = false;
        }
        protected void DropDownList7_Load(object sender, EventArgs e)
        {
            /* 
             DataTable dt = CHNLSVC.Inventory.warehouse_company();

             DropDownList7.DataSource = dt;
             DropDownList7.DataTextField = "mw_com";
             DropDownList7.DataBind();*/

        }

        public void subchaneldetails()
        {
            string SearchParams;
            DataTable _result = null;
            ViewState["subchanel"] = null;
            if (IsDisplayRawData)
            {
                SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
                IsRawData = true;
                _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, null, TextBox4.Text);
            }
            else
            {
                SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
                IsRawData = false;
                _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, "Code", TextBox4.Text);

                TextBox4.ToolTip = Convert.ToString(_result.Rows[0].Field<string>("Description"));
            }

        }



        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            TextBox9.Text = "";
            if (CheckBox1.Checked == true)
            {
                TextBox9.Enabled = true;
            }
            else
            {
                TextBox9.Enabled = false;
            }
        }

        protected void TextBox8_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (TextBox8.Text != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                    DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(para, "Code", TextBox8.Text.Trim().ToUpper());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (TextBox8.Text.Trim().ToUpper() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        TextBox8.ToolTip = toolTip;
                        TextBox8.Focus();
                    }
                    else
                    {
                        TextBox8.ToolTip = "";
                        TextBox8.Text = "";
                        TextBox8.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid profit center')", true);
                        return;
                    }
                }
                else
                {
                    TextBox8.ToolTip = "";
                }
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

        protected void TextBox9_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (TextBox9.Text.ToUpper() == TextBox5.Text.ToUpper())
                {
                    TextBox9.Text = "";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Canot select same location code !!!')", true);
                    return;
                }
                if (TextBox9.Text != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                    DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(para, "Code", "%" + TextBox9.Text.ToString());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (TextBox9.Text == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        TextBox9.ToolTip = toolTip;
                        TextBox9.Focus();
                    }
                    else
                    {
                        TextBox9.ToolTip = "";
                        TextBox9.Text = "";
                        TextBox9.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid main location code')", true);
                        return;
                    }
                }
                else
                {
                    TextBox9.ToolTip = "";
                }
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

        protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList5.SelectedValue == "1")
            {
                TextBox11.Enabled = true;
                TextBox11.Text = "";
            }
            else
            {
                TextBox11.Text = "";
                TextBox11.Enabled = false;
            }
        }

        protected void lbtnSeLocation_Click(object sender, EventArgs e)
        {

        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void dgvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void dgvResult_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private decimal ConvertDesZero(string val)
        {
            try
            {
                return Convert.ToDecimal(val);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        private Int64 ConvertIntZero(string val)
        {
            try
            {
                return Convert.ToInt64(val);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        protected void TextBox18_TextChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(TextBox18.Text))
            //{
            //    if (!IsValidEmail(TextBox18.Text))
            //    {
            //        TextBox18.Focus();
            //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid email address !!!')", true);
            //        return;
            //    }
            //}
            //else
            //{
            //    TextBox22.Focus();
            //}
        }
        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";

            System.Text.RegularExpressions.Match match = Regex.Match(email.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }

        protected void lbtnSeLocation_Click1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox1.Text))
            {
                displayMessage_warning("Please select a company !"); return;
            }
            ViewState["UserLocation"] = null;

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
            dvResult.DataSource = _result;
            dvResult.DataBind();
            ViewState["UserLocation"] = _result;
            Label8.Text = "UserLocation";
            BindUCtrlDDLData(_result);
            txtSearchbyword.Text = ""; txtSearchbyword.Focus();
            UserPopoup.Show();
        }
        protected void LinkButtonsearch_Click(object sender, EventArgs e)
        {

            //            //company details
            //            if (string.IsNullOrEmpty(TextBox1.Text))
            //            {
            //                displayMessage_warning("Please select a company !"); return;
            //            }
            //            string company = TextBox1.Text;
            //            DataTable dt = CHNLSVC.General.GetCompanyInforDT(company);
            //            GridView2.DataSource = dt;
            //            GridView2.DataBind();

            //            if (dt.Rows.Count == 0)
            //            {
            //                displayMessage_warning("Please select a valid company"); return;
            //            }
            //            else
            //            {
            //                TextBox1.ToolTip = Convert.ToString(dt.Rows[0].Field<string>("mc_desc"));
            //            }

            //            //operation code
            //            string companys = TextBox1.Text;
            //            string locationcd = TextBox5.Text.ToUpper();
            //            DataTable ds = CHNLSVC.Inventory.GetOperationCode(companys, locationcd);
            //            GridView3.DataSource = dt;
            //            GridView3.DataBind();
            //            if (ds.Rows.Count == 0)
            //            {

            //            }
            //            else
            //            {
            //                TextBox2.ToolTip = Convert.ToString(dt.Rows[0].Field<string>("mc_desc"));
            //            }

            //            //

            //            //chanel description



            //            //if (TextBox5.Text == "")
            //            //{
            //            //    displayMessage_warning("Please enter a valid location code !");

            //            //}
            //            //if (TextBox1.Text == "")
            //            //{
            //            //    displayMessage_warning("Enter valid Company");
            //            //}
            //            //else if (TextBox5.Text == "" && TextBox1.Text == "")
            //            //{

            //            //    displayMessage_warning("Enter valid data");
            //            //}

            ///*
            //            else if (TextBox2.Text == "") 
            //            {
            //                displayMessage_warning("Enter Operation Code");
            //            }
            //            else if (TextBox3.Text == "") 
            //            {
            //                displayMessage_warning("Enter valid Chanel");
            //            }
            //            else if (TextBox4.Text == "") 
            //            {
            //                displayMessage_warning("Enter valid sub channel");
            //            }
            //            */

            //            if (!string.IsNullOrEmpty(TextBox5.Text))
            //            {
            //                try
            //                {

            //                    DataTable _result = CHNLSVC.Inventory.GetLocationDetails(TextBox1.Text, TextBox5.Text.ToUpper());
            //                    GridView1.DataSource = _result;
            //                    GridView1.DataBind();
            //                    string chanel = Convert.ToString(_result.Rows[0].Field<string>("ML_CATE_2"));
            //                    string subchanel = Convert.ToString(_result.Rows[0].Field<string>("ML_CATE_3"));
            //                    string reference = Convert.ToString(_result.Rows[0].Field<string>("ml_ref"));
            //                    string address1 = Convert.ToString(_result.Rows[0].Field<string>("ml_add1"));
            //                    string address2 = Convert.ToString(_result.Rows[0].Field<string>("ml_add2"));
            //                    string country = Convert.ToString(_result.Rows[0].Field<string>("ml_country_cd"));
            //                    string province = Convert.ToString(_result.Rows[0].Field<string>("ml_province_cd"));
            //                    string town = Convert.ToString(_result.Rows[0].Field<string>("ml_town_cd"));

            //                    string contact_person = Convert.ToString(_result.Rows[0].Field<string>("ml_contact"));

            //                    string LocationDescription = Convert.ToString(_result.Rows[0].Field<string>("ml_loc_desc"));
            //                    string email = Convert.ToString(_result.Rows[0].Field<string>("ml_email"));

            //                    //string moby = Convert.ToString(_result.Rows[0].Field<string>(""));
            //                    string fax = Convert.ToString(_result.Rows[0].Field<string>("ml_fax"));
            //                    string Tel = Convert.ToString(_result.Rows[0].Field<string>("ml_tel"));
            //                    string district = Convert.ToString(_result.Rows[0].Field<string>("ml_distric_cd"));

            //                    string operationAdminTeam = Convert.ToString(_result.Rows[0].Field<string>("ML_OPE_CD"));
            //                    string profitcenter = Convert.ToString(_result.Rows[0].Field<string>("ML_DEF_PC"));
            //                    string mobile = Convert.ToString(_result.Rows[0].Field<string>("ML_MOBI"));
            //                    int allonoofRecords = _result.Rows[0].Field<Int16>("ML_FWSALE_QTY");
            //                    string F_asset = Convert.ToString(_result.Rows[0].Field<string>("ML_FX_LOC"));
            //                    int anal4 = _result.Rows[0].Field<Int16>("ML_ANAL4");
            //                    int serial = _result.Rows[0].Field<Int16>("ml_is_serial");

            //                    //   decimal? approvedstockvalue =Convert.ToDecimal(_result.Rows[0].Field<decimal?>("ML_APP_STK_VAL"));
            //                    // Int32  approve=
            //                    //     int? bankgurranty = _result.Rows[0].Field<Int16?>("ML_BANK_GRNT_VAL");
            //                    int? suspended = _result.Rows[0].Field<Int16?>("ML_SUSPEND");
            //                    int? auto_ain = _result.Rows[0].Field<Int16?>("ml_auto_ain");

            //                    string locationType = Convert.ToString(_result.Rows[0].Field<string>("ML_LOC_TP"));
            //                    string categery = Convert.ToString(_result.Rows[0].Field<string>("ml_cate_1"));
            //                    string grade = Convert.ToString(_result.Rows[0].Field<string>("ML_BUFFER_GRD"));
            //                    string main_location_cd = Convert.ToString(_result.Rows[0].Field<string>("ml_main_loc_cd"));
            //                    //     int suspended=_result.Rows[0].Field<Int16>("ml_suspend");
            //                    int online = _result.Rows[0].Field<Int16>("ml_is_online");
            //                    int sub_Location = _result.Rows[0].Field<Int16>("ml_is_sub_loc");
            //                    int status = _result.Rows[0].Field<Int16>("ml_act");

            //                    int pda = _result.Rows[0].Field<Int16>("ML_IS_PDA");
            //                    CheckBoxpda.Checked = pda == 1 ? true : false;
            //                    try
            //                    {
            //                        string warehousecp = Convert.ToString(_result.Rows[0].Field<string>("ml_wh_com"));
            //                        string warehousecode = Convert.ToString(_result.Rows[0].Field<string>("ml_wh_cd"));
            //                        DropDownList7.SelectedIndex = DropDownList7.Items.IndexOf(DropDownList7.Items.FindByValue(warehousecp));
            //                        DropDownList11.SelectedIndex = DropDownList11.Items.IndexOf(DropDownList11.Items.FindByValue(warehousecode));
            //                    }
            //                    catch (Exception ex) { }

            //                    DropDownList2.SelectedIndex = DropDownList2.Items.IndexOf(DropDownList2.Items.FindByValue(locationType));
            //                    DropDownList3.SelectedIndex = DropDownList3.Items.IndexOf(DropDownList3.Items.FindByValue(categery));
            //                    DropDownList10.SelectedIndex = DropDownList10.Items.IndexOf(DropDownList10.Items.FindByValue(grade));
            //                    DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(status.ToString()));
            //                    CheckBox3.Checked = serial == 0 ? false : true;
            //                    CheckBox1.Checked = sub_Location == 0 ? false : true;
            //                    DropDownList4.SelectedIndex = DropDownList4.Items.IndexOf(DropDownList4.Items.FindByValue(online.ToString()));
            //                    DropDownList8.SelectedIndex = DropDownList8.Items.IndexOf(DropDownList8.Items.FindByValue(suspended.ToString()));
            //                    DropDownList5.SelectedIndex = DropDownList5.Items.IndexOf(DropDownList5.Items.FindByValue(anal4.ToString()));

            //                    TextBox9.Text = main_location_cd;
            //                    TextBox3.Text = chanel;
            //                    TextBox4.Text = subchanel;
            //                    TextBox6.Text = reference;
            //                    TextBox14.Text = address1;
            //                    TextBox17.Text = address2;
            //                    TextBox16.Text = country;
            //                    TextBox19.Text = province;
            //                    TextBox18.Text = email;
            //                    TextBox21.Text = fax;
            //                    TextBox22.Text = district;
            //                    TextBox20.Text = Tel;
            //                    TextBox2.Text = operationAdminTeam;
            //                    TextBox8.Text = profitcenter;
            //                    TextBox10.Text =allonoofRecords>0? allonoofRecords.ToString():"";
            //                    TextBox25.Text = F_asset;
            //                    //   TextBox12.Text = bankgurranty.ToString() ;
            //                    //  TextBox11.Text = approvedstockvalue.ToString();
            //                    TextBox7.Text = LocationDescription;
            //                    DropDownList2.SelectedIndex = DropDownList2.Items.IndexOf(DropDownList2.Items.FindByValue(locationType));
            //                    //DropDownList3.SelectedItem.Text = categery;
            //                    DropDownList10.SelectedItem.Text = grade;
            //                    TextBox13.Text = mobile;
            //                    TextBoxtown.Text = town;
            //                    TextBox23.Text = contact_person;



            //                    if (auto_ain != null)
            //                    {
            //                        //   CheckBox2.Checked = true;
            //                    }
            //                    // TextBox26.Text = cdate;
            //                    // TextBox8.Text=



            //                    //
            //                    //DataTable dt = CHNLSVC.Inventory.LoacationType();
            //                    //DropDownList2.DataSource = dt;

            //                    //DropDownList2.DataBind();
            //                    chanel_tool();
            //                    subchaneldetails();
            //                    country_details();
            //                    provincedetails();
            //                    DistrictDetails();
            //                }
            //                catch (Exception ex)
            //                {

            //                    displayMessage_warning("No Location Found");
            //                }
            //            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            Labelcompanyd.Text = "";

            if (string.IsNullOrEmpty(TextBox1.Text))
            {
                displayMessage_warning("Please select a company"); return;
            }
            if (!validateinputString(TextBox1.Text))
            {
                displayMessage_warning("Invalid charactor found in company.");
                TextBox1.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(TextBox1.Text))
            {
                bool b = false;
                string desc = "";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company);
                DataTable _result = new DataTable();
                _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, "CODE", TextBox1.Text);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (TextBox1.Text == row["Code"].ToString())
                        {
                            desc = row["Description"].ToString();
                            b = true; break;
                        }
                    }
                }
                if (b)
                {
                    Labelcompanyd.Text = desc;
                }
                else
                {
                    TextBox1.Text = "";
                    TextBox1.Focus();
                    Labelcompanyd.Text = "";
                    displayMessage_warning("Please select a valid company"); return;
                }
            }
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            lblOperation.Text = "";

            if (string.IsNullOrEmpty(TextBox2.Text))
            {
                displayMessage_warning("Please select a operation"); return;
            }
            if (!validateinputString(TextBox2.Text))
            {
                displayMessage_warning("Invalid charactor found in operation code.");
                TextBox2.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(TextBox2.Text))
            {
                bool b = false;
                string desc = "";
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OPE);
                DataTable _result = CHNLSVC.CommonSearch.SearchOperation(para, "CODE", TextBox2.Text.ToUpper());
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (TextBox2.Text.ToUpper() == row["Code"].ToString())
                        {
                            desc = row["Description"].ToString();
                            b = true; break;
                        }
                    }
                }
                if (b)
                {
                    lblOperation.Text = desc;
                }
                else
                {
                    TextBox2.Text = "";
                    TextBox2.Focus();
                    lblOperation.Text = "";
                    displayMessage_warning("Please select a valid operation"); return;
                }
            }
        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {
            Labelchanel.Text = "";
            while (DropDownList10.Items.Count > 1)
            {
                DropDownList10.Items.RemoveAt(1);
            }
            if (!validateinputString(TextBox3.Text))
            {
                displayMessage_warning("Invalid charactor found in channel code.");
                TextBox3.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(TextBox3.Text))
            {
                bool b = false;
                string desc = "";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                DataTable _result = new DataTable();
                _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, null, null);
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (TextBox3.Text == row["Code"].ToString())
                        {
                            desc = row["Description"].ToString();
                            b = true; break;
                        }
                    }
                }
                if (b)
                {
                    Labelchanel.Text = desc;
                }
                else
                {
                    TextBox3.Text = "";
                    TextBox3.Focus();
                    Labelchanel.Text = "";
                    displayMessage_warning("Please select a valid channel"); return;
                }
                BindGradeTp();
            }
        }

        protected void TextBox4_TextChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(TextBox4.Text))
            {
                displayMessage_warning("Please select a sub channel"); return;
            }
            if (!validateinputString(TextBox4.Text))
            {
                displayMessage_warning("Invalid charactor found in sub channel.");
                TextBox4.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(TextBox4.Text))
            {
                //Labelsubchanel.Text = "";
                bool b = false;
                string desc = "";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
                DataTable _result = new DataTable();
                //Add by Lakshan as per the Asanka 08 Nov 2016
                if (SearchParams.Contains(TextBox3.Text))
                {
                    if (!string.IsNullOrEmpty(TextBox3.Text))
                    {
                        SearchParams = SearchParams.Replace(TextBox3.Text, "");
                    }
                }
                _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, "CODE", "%" + TextBox4.Text.ToUpper().Trim().ToString());
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (TextBox4.Text.ToUpper().Trim() == row["Code"].ToString())
                        {
                            desc = row["Description"].ToString();
                            b = true; break;
                        }
                    }
                }
                if (b)
                {
                    Labelsubchanel.Text = desc;
                }
                else
                {
                    TextBox4.Text = "";
                    TextBox4.Focus();
                    Labelsubchanel.Text = "";
                    displayMessage_warning("Please select a valid channel !!!"); return;
                }
            }
        }

        protected void lbtnSeMainLoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TextBox5.Text))
                {
                    displayMessage_warning("Please enter a location code !!!"); return;
                }
                if (!CheckBox1.Checked)
                {
                    displayMessage_warning("Please select maintain sub location !!!"); return;
                }
                ViewState["UserLocation"] = null;

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);

                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                _result.AsEnumerable().Where(r => r.Field<string>("Code") == TextBox5.Text.ToUpper()).ToList().ForEach(row => row.Delete());
                dvResult.DataSource = _result;
                dvResult.DataBind();
                ViewState["UserLocation"] = _result;
                Label8.Text = "MainUserLocation";
                BindUCtrlDDLData(_result);
                txtSearchbyword.Text = ""; txtSearchbyword.Focus();
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void DropDownList6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList6.SelectedValue == "1")
            {
                TextBox12.Enabled = true;
                TextBox12.Text = "";
            }
            else
            {
                TextBox12.Text = "";
                TextBox12.Enabled = false;
            }
        }

        private bool isValidDecimal(string txt)
        {
            decimal x;
            return decimal.TryParse(txt, out x);
        }
        private bool isValidInt(string txt)
        {
            Int32 x;
            return Int32.TryParse(txt, out x);
        }
        private bool isValidDate(string txt)
        {
            DateTime x;
            return DateTime.TryParse(txt, out x);
        }
        public static bool isValidEmail(string email)
        {
            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";

            System.Text.RegularExpressions.Match match = Regex.Match(email.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }
        public bool IsValidNIC(string nic)
        {
            string pattern = @"^[0-9]{9}[V,X]{1}$";

            System.Text.RegularExpressions.Match match = Regex.Match(nic.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }

        protected void DropDownList7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList7.SelectedIndex > 0)
            {
                BindWhCd();
            }
        }
        private void Msg(string msgText, string msgType)
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



        protected void lbtnSeCat_Click(object sender, EventArgs e)
        {

        }

        protected void txtOtherCat_TextChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtOtherCat.Text))
            //{
            //    bool b = false;
            //    string desc = "";
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
            //    DataTable _result = new DataTable();
            //    _result = CHNLSVC.CommonSearch.SearchRefLocCate3(SearchParams, "CODE", txtOtherCat.Text.ToUpper());
            //    foreach (DataRow row in _result.Rows)
            //    {
            //        if (!string.IsNullOrEmpty(row["Code"].ToString()))
            //        {
            //            if (txtOtherCat.Text.ToUpper() == row["Code"].ToString())
            //            {
            //                desc = row["Description"].ToString();
            //                b = true; break;
            //            }
            //        }
            //    }
            //    if (b)
            //    {
            //        txtOtherCat.ToolTip = desc;
            //    }
            //    else
            //    {
            //        txtOtherCat.Text = "";
            //        txtOtherCat.Focus();
            //        txtOtherCat.ToolTip = "";
            //        displayMessage_warning("Please select a valid channel !!!"); return;
            //    }
            //}
        }

        protected void txtChannelCat_TextChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtChannelCat.Text))
            //{
            //    bool b = false;
            //    string desc = "";
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Channel);
            //    DataTable _result = new DataTable();
            //    _result = CHNLSVC.CommonSearch.SearchMstChnl(SearchParams, "CODE", txtChannelCat.Text.ToUpper());
            //    foreach (DataRow row in _result.Rows)
            //    {
            //        if (!string.IsNullOrEmpty(row["Code"].ToString()))
            //        {
            //            if (txtChannelCat.Text.ToUpper() == row["Code"].ToString())
            //            {
            //                desc = row["Description"].ToString();
            //                b = true; break;
            //            }
            //        }
            //    }
            //    if (b)
            //    {
            //        txtChannelCat.ToolTip = desc;
            //    }
            //    else
            //    {
            //        txtChannelCat.Text = "";
            //        txtChannelCat.Focus();
            //        txtChannelCat.ToolTip = "";
            //        displayMessage_warning("Please select a valid channel !!!"); return;
            //    }
            //}
        }

        protected void lbtnSeChannel_Click(object sender, EventArgs e)
        {
            //ViewState["Channel"] = null;
            //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Channel);
            //DataTable _result = CHNLSVC.CommonSearch.SearchMstChnl(SearchParams, null, null);
            //dvResult.DataSource = _result;
            //dvResult.DataBind();
            //ViewState["Channel"] = _result;
            //Label8.Text = "Channel";
            //BindUCtrlDDLData(_result);
            //txtSearchbyword.Text = ""; txtSearchbyword.Focus();
            //UserPopoup.Show();
        }
        protected void lbtnSeOther_Click(object sender, EventArgs e)
        {
            //ViewState["CAT_Sub3"] = null;
            //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
            //DataTable _result = CHNLSVC.CommonSearch.SearchRefLocCate3(SearchParams, null, null);
            //dvResult.DataSource = _result;
            //dvResult.DataBind();
            //ViewState["CAT_Sub3"] = _result;
            //Label8.Text = "CAT_Sub3";
            //BindUCtrlDDLData(_result);
            //txtSearchbyword.Text = ""; txtSearchbyword.Focus();
            //UserPopoup.Show();
        }


        protected void TextBox16_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBox16.Text))
            {
                //Labelsubchanel.Text = "";
                bool b = false;
                string desc = "";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
                DataTable _result = CHNLSVC.CommonSearch.GetCountrySearchData(SearchParams, "Code", TextBox16.Text.ToUpper());
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (TextBox16.Text.ToUpper() == row["Code"].ToString())
                        {
                            desc = row["Description"].ToString();
                            b = true; break;
                        }
                    }
                }
                if (b)
                {
                    TextBox16.ToolTip = desc;
                }
                else
                {
                    TextBox16.Text = "";
                    TextBox16.Focus();
                    TextBox16.ToolTip = "";
                    displayMessage_warning("Please select a valid country !!!"); return;
                }
            }
        }
        protected void TextBox19_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBox19.Text))
            {
                //Labelsubchanel.Text = "";
                bool b = false;
                string desc = "";
                string param = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Province);
                DataTable _result = CHNLSVC.CommonSearch.GetProvinceData(param, "Code", TextBox19.Text.ToUpper());
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Code"].ToString()))
                    {
                        if (TextBox19.Text.ToUpper() == row["Code"].ToString())
                        {
                            desc = row["Description"].ToString();
                            b = true; break;
                        }
                    }
                }
                if (b)
                {
                    TextBox19.ToolTip = desc;
                }
                else
                {
                    TextBox19.Text = "";
                    TextBox19.Focus();
                    TextBox19.ToolTip = "";
                    displayMessage_warning("Please select a valid province !!!"); return;
                }
            }
        }

        protected void TextBox22_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBox22.Text))
            {
                //Labelsubchanel.Text = "";
                bool b = false;
                string desc = "";
                DataTable _result = CHNLSVC.Inventory.getDistrictDetails(TextBox19.Text.ToUpper());
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["District Code"].ToString()))
                    {
                        if (TextBox22.Text.ToUpper() == row["District Code"].ToString())
                        {
                            desc = row["Description"].ToString();
                            b = true; break;
                        }
                    }
                }
                if (b)
                {
                    TextBox22.ToolTip = desc;
                }
                else
                {
                    TextBox22.Text = "";
                    TextBox22.Focus();
                    TextBox22.ToolTip = "";
                    displayMessage_warning("Please select a valid district !!!"); return;
                }
            }
        }

        protected void TextBoxtown_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBoxtown.Text))
            {
                bool b = false;
                string desc = "";
                DataTable _result = CHNLSVC.Inventory.getTownDetails(TextBox16.Text.ToUpper(), TextBox19.Text.ToUpper(), TextBox22.Text.ToUpper());
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Town Code"].ToString()))
                    {
                        if (TextBoxtown.Text.ToUpper() == row["Town Code"].ToString())
                        {
                            desc = row["Description"].ToString();
                            b = true; break;
                        }
                    }
                }
                if (b)
                {
                    TextBoxtown.ToolTip = desc;
                }
                else
                {
                    TextBoxtown.Text = "";
                    TextBoxtown.Focus();
                    TextBoxtown.ToolTip = "";
                    displayMessage_warning("Please select a valid town !!!"); return;
                }
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            lblExcelUploadError.Visible = false;
            lblExcelUploadError.Text = "";
            _showExcelPop = true;
            popupExcel.Show();
        }

        protected void lbtnExcelUploadClose_Click(object sender, EventArgs e)
        {
            _showExcelPop = false;
            popupExcel.Hide();
        }

        protected void lbtnUploadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                lblExcelUploadError.Visible = true;
                lblExcelUploadError.Text = "";
                if (fileUploadExcel.HasFile)
                {
                    string FileName = Path.GetFileName(fileUploadExcel.PostedFile.FileName);
                    string Extension = Path.GetExtension(fileUploadExcel.PostedFile.FileName);

                    if (Extension == ".xls" || Extension == ".XLS" || Extension == ".xlsx" || Extension == ".XLSX")
                    {

                    }
                    else
                    {
                        lblExcelUploadError.Visible = true;
                        lblExcelUploadError.Text = "Please select a valid excel (.xls or .xlsx) file";
                        _showExcelPop = true;
                        popupExcel.Show();
                        return;
                    }

                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                    string ValidateFilePath = Server.MapPath(FolderPath + FileName);
                    fileUploadExcel.SaveAs(ValidateFilePath);
                    _filPath = ValidateFilePath;
                    _showExcelPop = false;
                    popupExcel.Hide();

                    lblProcess.Visible = true;
                    lblProcess.Text = "Excel file upload completed. Do you want to process ? ";
                    _showProcPop = true;
                    popupExcProc.Show();

                }
                else
                {
                    lblProcess.Visible = false;
                    lblProcess.Text = "";
                    lblerror.Visible = true;
                    lblerror.Text = "Please select the correct upload file path !";
                    _showProcPop = true;
                    popupExcProc.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                lblExcelUploadError.Visible = true;
                lblExcelUploadError.Text = "Error Occurred :" + ex.Message;
            }
        }

        protected void lbtnProcClose_Click(object sender, EventArgs e)
        {
            _showExcelPop = false;
            popupExcProc.Hide();
        }

        protected void lbtnExcelProcess_Click(object sender, EventArgs e)
        {
            try
            {
                List<MasterLocationNew> _locmasterdet = new List<MasterLocationNew>();
                string _error = "";
                lblExcelUploadError.Visible = false;
                lblExcelUploadError.Text = "";
                hdfSaveTp.Value = "excel";
                if (string.IsNullOrEmpty(_filPath))
                {
                    lblExcelUploadError.Visible = true;
                    lblExcelUploadError.Text = "File Path Invalid Please Upload ";
                    _showExcelPop = true;
                    popupExcel.Show();
                    return;
                }
                DataTable[] GetExecelTbl = LoadValidateData(_filPath, out _error);
                if (GetExecelTbl == null)
                {
                    lblExcelUploadError.Visible = true;
                    lblExcelUploadError.Text = _error;
                    _showExcelPop = true;
                    popupExcel.Show();
                    return;
                }
                DataTable _dtExData = GetExecelTbl[0];
                if (_dtExData.Rows.Count < 1 && _dtExData.Columns.Count < 12)
                {
                    lblExcelUploadError.Visible = true;
                    lblExcelUploadError.Text = "Excel Data Invalid Please check Excel File and Upload";
                    _showExcelPop = true;
                    popupExcel.Show();
                    return;
                }
                _locmasterdet = new List<MasterLocationNew>();
                List<MasterLocationNew> tmpErrList = new List<MasterLocationNew>();
                DateTime _tmpDec = DateTime.Now;
                for (int i = 1; i < _dtExData.Rows.Count; i++)
                {
                    MasterLocationNew _locmasterdetob = new MasterLocationNew();
                    _locmasterdetob.Ml_com_cd = _dtExData.Rows[i][0].ToString();
                    _locmasterdetob.Ml_loc_cd = _dtExData.Rows[i][1].ToString();
                    if (!string.IsNullOrEmpty(_dtExData.Rows[i][2].ToString()))
                    {
                        _locmasterdetob.Ml_app_stk_val = Convert.ToDecimal(_dtExData.Rows[i][2].ToString());
                    }
                    if (!string.IsNullOrEmpty(_dtExData.Rows[i][3].ToString()))
                    {
                        _locmasterdetob.Ml_bank_grnt_val = Convert.ToDecimal(_dtExData.Rows[i][3].ToString());
                    }
                    _locmasterdetob.Ml_mod_by = Session["UserID"].ToString();
                    _locmasterdetob.Ml_mod_dt = DateTime.Now;
                    _locmasterdet.Add(_locmasterdetob);
                }
                if (_locmasterdet != null)
                {
                    int _effect = 0;

                    _effect = CHNLSVC.General.Updatelogdetails(_locmasterdet);
                    if (_effect == 1)
                    {
                        int _res = 0;

                        _res = CHNLSVC.General.uploadlocmasterdet(_locmasterdet);
                        if (_locmasterdet.Count < 1)
                        {
                            Msg("Please add details !!!", "S"); return;
                        }
                        if (_res == 1)
                        {
                            Msg("Successfully Updated!", "S");
                            // ClearPageAll();
                        }
                    }
                }
            }


            catch (Exception ex)
            {
                _showExcelPop = true;
                popupExcel.Show();
                lblExcelUploadError.Visible = true;
                lblExcelUploadError.Text = "Excel  Data Invalid Please check Excel File and Upload";
            }
        }


        public DataTable[] LoadValidateData(string FileName, out string _error)
        {
            _error = "";
            #region Excel Process
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DataTable Tax = new DataTable();
            using (OleDbConnection cn = new OleDbConnection { ConnectionString = ConnectionString(FileName, "No") })
            {
                try
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
                    cmdExcel.CommandText = "SELECT F1,F2,F3,F4 From [" + SheetName + "]";
                    oda.SelectCommand = cmdExcel;
                    oda.Fill(Tax);
                }
                catch (Exception ex)
                {
                    _error = ex.Message;
                    return new DataTable[] { Tax };
                }
                return new DataTable[] { Tax };
            }
            #endregion
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
        public bool validateinputString(string input)
        {
            Match match = Regex.Match(input, @"([~!@#$%^&*]+)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return false;
            }
            return true;
        }
        public bool validateinputStringWithSpace(string input)
        {
            Match match = Regex.Match(input, @"([~!@#$%^&*]+)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return false;
            }
            return true;
        }

        protected void TextBox6_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(TextBox6.Text))
            {
                displayMessage_warning("Invalid charactor found in ref code.");
                TextBox6.Focus();
                return;
            }
        }

        protected void TextBox7_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(TextBox7.Text))
            {
                displayMessage_warning("Invalid charactor found in loc code.");
                TextBox7.Focus();
                return;
            }
        }

        protected void TextBox14_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(TextBox14.Text))
            {
                displayMessage_warning("Invalid charactor found in address 1.");
                TextBox14.Focus();
                return;
            }
        }

        protected void TextBox17_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputStringWithSpace(TextBox17.Text))
            {
                displayMessage_warning("Invalid charactor found in address 2.");
                TextBox17.Focus();
                return;
            }
        }
        //Added By Dulaj 2018-Nov-30
        protected void ImgbtnUID_Click(object sender, EventArgs e)
        {

            // WarningUser.Visible = false;
            // SuccessUser.Visible = false;
            errorDiv.Visible = false;
            successDiv.Visible = false;
            dvResult.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SystemUser);
            _result = CHNLSVC.CommonSearch.Get_All_Users_Loc(SearchParams, null, null);
            dvResult.DataSource = _result;
            dvResult.DataBind();
            ViewState["contactuser"] = _result;
            BindUCtrlDDLData(_result);

            UserPopoup.Show();

            Label8.Text = "178";
        }

        protected void txtID_TextChanged(object sender, EventArgs e)
        {
            SystemUser _systemUser = null;
            TextBox23.Text = TextBox23.Text.ToUpper();
            if (!(TextBox23.Text == "N/A" || TextBox23.Text == ""))
            {
                _systemUser = CHNLSVC.Security.GetUserByUserID(TextBox23.Text);
                // Session["SearchID"] = _systemUser.Se_usr_id;
                if (_systemUser == null)
                {
                    TextBox23.Text = string.Empty;
                    TextBox18.Text = string.Empty;
                    TextBox13.Text = string.Empty;
                    TextBox24.Text = string.Empty;
                    displayMessage_warning("Please Select Valid Contact Person.");
                    return;
                }
                else
                {
                    if (_systemUser.Se_act == 0)
                    {
                        TextBox23.Text = string.Empty;
                        TextBox18.Text = string.Empty;
                        TextBox13.Text = string.Empty;
                        TextBox24.Text = string.Empty;
                        displayMessage_warning("Selected user ID is Currently Inactivated!");
                        return;
                    }
                    else if (_systemUser.Se_act == -1)
                    {
                        TextBox23.Text = string.Empty;
                        TextBox18.Text = string.Empty;
                        TextBox13.Text = string.Empty;
                        TextBox24.Text = string.Empty;
                        displayMessage_warning("Selected user ID is Currently Locked!");
                        return;

                    }
                    else if (_systemUser.Se_act == -2)
                    {
                        TextBox23.Text = string.Empty;
                        TextBox18.Text = string.Empty;
                        TextBox13.Text = string.Empty;
                        TextBox24.Text = string.Empty;
                        displayMessage_warning("Selected user ID is Permanently Disabled!");
                        return;

                    }
                    else
                    {
                        TextBox18.Text = _systemUser.se_Email;
                        TextBox13.Text = _systemUser.se_Mob;
                        TextBox24.Text = _systemUser.Se_usr_name;
                    }
                }
            }
            if (TextBox23.Text == "")
            {
                TextBox18.Text = string.Empty;
                TextBox13.Text = string.Empty;
                TextBox24.Text = string.Empty;

            }

        }

        private void loadContactDetaisl(string userID)
        {
            SystemUser _systemUser = null;
            userID = userID.ToUpper();
            _systemUser = CHNLSVC.Security.GetUserByUserID(userID);
            // Session["SearchID"] = _systemUser.Se_usr_id;
            if (_systemUser != null)
            {
                TextBox18.Text = _systemUser.se_Email;
                TextBox13.Text = _systemUser.se_Mob;
                TextBox24.Text = _systemUser.Se_usr_name;
            }

        }

    }
}