using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Data;
using System.Text;


namespace FF.WebERPClient.HP_Module
{
    public partial class CashConversion : BasePage
    {

        #region properties

        public DataTable Party
        {
            get { return (DataTable)ViewState["Party_Type"]; }
            set { ViewState["Party_Type"] = value; }
        }

        public DataTable Price_book
        {
            get { return (DataTable)ViewState["Price_book"]; }
            set { ViewState["Price_book"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CreateParty();
                CreatePricBook();
                LoadSchemeCategory(DropDownListSchemeCategory);
                LoadPriceBook(DropDownListPriceBook);
                LoadPriceBook(DropDownListPBConvertable);
            }
            DropDownListPriceBook.Attributes.Add("onmouseover", "this.title=this.options[this.selectedIndex].title");
        }

        #region datatable create

        private void CreateParty()
        {
            Party = new DataTable();
            Party.Columns.Add("P_Type", typeof(string));
            Party.Columns.Add("P_Code", typeof(string));
        }

        private void CreatePricBook()
        {
            Price_book = new DataTable();
            Price_book.Columns.Add("PB", typeof(string));
            Price_book.Columns.Add("PB_Lvl", typeof(string));
        }

        #endregion

        #region data bindings

        private void LoadPriceBook(DropDownList DropDownListPriceBook)
        {
            List<PriceBookRef> _priceBook = CHNLSVC.Sales.GetPriceBooklist(GlbUserComCode);
            DropDownListPriceBook.Items.Clear();
            DropDownListPriceBook.Items.Add(new ListItem("", "-1"));
            if(_priceBook.Count>0){
                foreach (PriceBookRef _pb in _priceBook) {
                    DropDownListPriceBook.Items.Add(new ListItem(_pb.Sapb_pb + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - _pb.Sapb_pb.Length)) + "-" + _pb.Sapb_desc, _pb.Sapb_pb));
                }
            }
            DropDownListPriceBook.DataBind();
            foreach (ListItem _listItem in this.DropDownListPriceBook.Items)
            {

                _listItem.Attributes.Add("title", _listItem.Text);
            }
        }

        private void LoadSchemeCategory(DropDownList DropDownListSchemeCategory)
        {
            DataTable dt = CHNLSVC.Sales.GetSAllchemeCategoryies("%");
            DropDownListSchemeCategory.Items.Clear();
            DropDownListSchemeCategory.Items.Add(new ListItem("", "-1"));
            foreach (DataRow dr in dt.Rows)
            {
                DropDownListSchemeCategory.Items.Add(new ListItem(dr["HSC_CD"].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr["HSC_CD"].ToString().Length)) + "-" + dr["HSC_DESC"].ToString(), dr["HSC_CD"].ToString()));
            }
            DropDownListSchemeCategory.DataBind();
        }

        private string AddHtmlSpaces(int length)
        {
            string space = "";
            for (int i = 0; i < length; i++)
            {
                space = space + " &nbsp;";
            }
            return space;
        }

        #endregion

        #region index_change

        protected void DropDownListSchemeCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<HpSchemeType> _schemeList = CHNLSVC.Sales.GetSchemeTypeByCategory(DropDownListSchemeCategory.SelectedValue);
            DropDownListSchemeType.Items.Clear();
            DropDownListSchemeType.Items.Add(new ListItem("", "-1"));
            if (_schemeList.Count > 0)
            {
                foreach (HpSchemeType scTy in _schemeList)
                {
                    DropDownListSchemeType.Items.Add(new ListItem(scTy.Hst_cd + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - scTy.Hst_cd.Length)) + "-" + scTy.Hst_desc, scTy.Hst_cd));
                }
            }
            DropDownListSchemeType.DataBind();
            CheckBoxAll.Checked = false;
        }

        protected void DropDownListSchemeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable datasource = CHNLSVC.Sales.GetSchemes("TYPE", DropDownListSchemeType.SelectedValue);

            //LoadGrid(GridViewSchemes, datasource);
            LoadList(ListBoxSchemes, datasource, "HSD_CD", "HSD_DESC");
            CheckBoxAll.Checked = false;
        }

        protected void DropDownListPriceBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            //List<PriceBookLevelRef> _PbLevel = CHNLSVC.Sales.GetPriceLevelList(GlbUserComCode, DropDownListPriceBook.SelectedValue, null);
            //DropDownListPBLevel.Items.Clear();
            //DropDownListPBLevel.Items.Add(new ListItem("", "-1"));
            //if (_PbLevel.Count > 0) {
            //    foreach (PriceBookLevelRef pbLv in _PbLevel)
            //    {
            //        DropDownListPBLevel.Items.Add(new ListItem(pbLv.Sapl_pb_lvl_cd + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - pbLv.Sapl_pb_lvl_cd.Length)) + "-" + pbLv.Sapl_pb_lvl_desc, pbLv.Sapl_pb_lvl_cd));                 
            //    }
            //}

            //DropDownListPBLevel.DataBind();
            //foreach (ListItem _listItem in this.DropDownListPBLevel.Items)
            //{
            //    _listItem.Attributes.Add("title", _listItem.Text);
            //}
            //DropDownListPBLevel.Attributes.Add("onmouseover", "this.title=this.options[this.selectedIndex].title");

            CreatePricBook();
            List<PriceBookLevelRef> _PbLevel = CHNLSVC.Sales.GetPriceLevelList(GlbUserComCode, DropDownListPriceBook.SelectedValue, null);
            ListBoxPriceLevel.Items.Clear();
            if (_PbLevel.Count > 0)
            {
                foreach (PriceBookLevelRef pbLv in _PbLevel)
                {
                    DataRow dr = Price_book.NewRow();
                    dr[0] = DropDownListPriceBook.SelectedValue;
                    dr[1] = pbLv.Sapl_pb_lvl_cd;
                    Price_book.Rows.Add(dr);
                    ListBoxPriceLevel.Items.Add(new ListItem(pbLv.Sapl_pb_lvl_cd + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - pbLv.Sapl_pb_lvl_cd.Length)) + "-" + pbLv.Sapl_pb_lvl_desc, pbLv.Sapl_pb_lvl_cd));
                }
                foreach (ListItem li in ListBoxPriceLevel.Items)
                {
                    li.Selected = true;
                }
            }
        }

        protected void DropDownListPartyTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreateParty();
            DataTable dt = new DataTable();
            if (DropDownListPartyTypes.SelectedValue == "COM")
            {
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue == "CHNL")
            {
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue == "SCHNL")
            {
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue == "GPC")
            {
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OPE);
                dt = CHNLSVC.CommonSearch.GetOPE(MasterCommonSearchUCtrl.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue == "AREA")
            {
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue == "REGION")
            {
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue == "ZONE")
            {
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue == "PC")
            {
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            }
            LoadList(ListBoxParty, dt, "Code", "Description");
            foreach (DataRow dr in dt.Rows)
            {
                DataRow drT = Party.NewRow();
                drT[0] = DropDownListPartyTypes.SelectedValue;
                drT[1] = dr[0].ToString();
                Party.Rows.Add(drT);

            }
        }

        #endregion

        #region save/cancel/clear

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HP_Module/CashConversion.aspx", false);
        }

        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (ListBoxParty.Items.Count > 0 && ListBoxPriceLevel.Items.Count > 0 && ListBoxSchemes.Items.Count > 0)
            {
                #region validation

                if (TextBoxFomAmo.Text == "") {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter from amount");
                    return;
                }
                if (TextBoxToAmo.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter to amount");
                    return;
                }
                if (Convert.ToDecimal(TextBoxFomAmo.Text) > Convert.ToDecimal(TextBoxToAmo.Text)) {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From value has to be smller than to value");
                    return;
                }
                if (TextBoxFromDate.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter account from date");
                    return;
                }
                if (TextBoxToDate.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter account to date");
                    return;
                }
                if (Convert.ToDateTime(TextBoxFromDate.Text) > Convert.ToDateTime(TextBoxToDate.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Account Period From date has to be smller than Account Period to date");
                    return;
                }
                if (TextBoxFromDy.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter from period");
                    return;
                }
                if (TextBoxToDy.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter to period");
                    return;
                }
                if (Convert.ToInt32(TextBoxFromDy.Text) > Convert.ToInt32(TextBoxToDy.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From period from has to be smller than period to");
                    return;
                }
                if (DropDownListPBConvertable.SelectedValue=="-1")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select convertable price book");
                    return;
                }
                if (TextBoxConvertionupto.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter conversion end date");
                    return;
                }
                //if((TextBoxServiceChgRt.Text))
                #endregion


                string _schenmeCode = string.Empty;
                string _partyType = string.Empty;
                string _partyCode = string.Empty;
                string _pb = string.Empty;
                string _pbLvl = string.Empty;

                //get scheme code string
                foreach (ListItem li in ListBoxSchemes.Items)
                {
                    if (li.Selected)
                    {
                        _schenmeCode = _schenmeCode + li.Value + ",";

                    }
                }
                _schenmeCode = _schenmeCode.Substring(0, _schenmeCode.Length - 1);

                //get party type and party code strings
                foreach (DataRow dr in Party.Rows)
                {
                    _partyType = _partyType + dr[0].ToString() + ",";
                    _partyCode = _partyCode + dr[1].ToString() + ",";
                }
                _partyType = _partyType.Substring(0, _partyType.Length - 1);
                _partyCode = _partyCode.Substring(0, _partyCode.Length - 1);

                foreach (DataRow dr in Price_book.Rows)
                {
                    _pb = _pb + dr[0].ToString() + ",";
                    _pbLvl = _pbLvl + dr[1].ToString() + ",";
                }
                _pb = _pb.Substring(0, _pb.Length - 1);
                _pbLvl = _pbLvl.Substring(0, _pbLvl.Length - 1);

                CashConversionDefinition _cashdefi = new CashConversionDefinition();
                _cashdefi.Hcc_from_cre_dt = Convert.ToDateTime(TextBoxFromDate.Text);
                _cashdefi.Hcc_to_cre_dt = Convert.ToDateTime(TextBoxToDate.Text);

                _cashdefi.Hcc_from_pd = Convert.ToInt32(TextBoxFromDy.Text);
                _cashdefi.Hcc_to_pd = Convert.ToInt32(TextBoxToDy.Text);

                _cashdefi.Hcc_from_val = Convert.ToDecimal(TextBoxFomAmo.Text);
                _cashdefi.Hcc_to_val = Convert.ToDecimal(TextBoxToAmo.Text);

                _cashdefi.Hcc_chk_on = DropDownListCheckOn.SelectedValue;
                _cashdefi.Hcc_cal_on = DropDownListCalculateOnType.SelectedValue;
                _cashdefi.Hcc_ser_chg_rt = Convert.ToDecimal(TextBoxServiceChgRt.Text);
                _cashdefi.Hcc_ser_chg_val = Convert.ToDecimal(TextBoxServiceChargeAmo.Text);
                _cashdefi.Hcc_add_cal_on = DropDownListAddCharType.SelectedValue;
                _cashdefi.Hcc_add_chg_rt = Convert.ToDecimal(TextBoxAddAmo.Text);
                _cashdefi.Hcc_add_chg_val = Convert.ToDecimal(TextBoxAddRate.Text);
                _cashdefi.Hcc_pb_conv = DropDownListPBConvertable.SelectedValue;
                _cashdefi.Hcc_cre_by = GlbUserName;
                _cashdefi.Hcc_cre_dt = DateTime.Now;


                CHNLSVC.Sales.SaveCashConv(_schenmeCode, _partyType, _partyCode, _pb, _pbLvl, _cashdefi);
                string Msg = "<script>alert('Record Insert Sucessfully!!');window.location = 'CashConversion.aspx';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            else {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select atleaset one Scheme,Business Code and Price book level to continue.");
            }
        }

        #endregion

        #region data load

        private void LoadGrid(GridView GridView, DataTable datasource)
        {
            GridView.DataSource = datasource;
            GridView.DataBind();
        }

        private void LoadList(ListBox listBox, DataTable _result,string _code,string _des)
        {
            listBox.Items.Clear();
            if (_result.Rows.Count > 0)
            {
                foreach (DataRow dr in _result.Rows)
                {
                    listBox.Items.Add(new ListItem(dr[_code].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[_code].ToString().Length)) + "-" + dr[_des].ToString(), dr[_code].ToString()));
                }
                foreach (ListItem li in listBox.Items) {
                    li.Selected = true;
                }
            }

        }

        #endregion

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append(GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator +""+ seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OPE:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        #region listbox clear

        protected void LinkButtonSheme_Click(object sender, EventArgs e)
        {
            ListBoxSchemes.Items.Clear();
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            ListBoxParty.Items.Clear();
        }

        protected void LinkButtonClearPb_Click(object sender, EventArgs e)
        {
            ListBoxPriceLevel.Items.Clear();
        }

        #endregion

        #region checbox_checked

        protected void CheckBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxAll.Checked)
            {
                //all categories
                if (DropDownListSchemeCategory.SelectedValue == "-1")
                {
                    List<HpSchemeType> _schemeList = new List<HpSchemeType>();
                    foreach (ListItem li in DropDownListSchemeCategory.Items)
                    {
                        List<HpSchemeType> _schemeListTemp = CHNLSVC.Sales.GetSchemeTypeByCategory(li.Value);
                        _schemeList.AddRange(_schemeListTemp);
                    }
                    DataTable _result = new DataTable();
                    foreach (HpSchemeType scTy in _schemeList)
                    {
                        DataTable dt = CHNLSVC.Sales.GetSchemes("TYPE", scTy.Hst_cd);
                        _result.Merge(dt);
                    }
                    //LoadGrid(GridViewSchemes, _result);
                    LoadList(ListBoxSchemes, _result, "HSD_CD", "HSD_DESC");
                    //set ddl selected value
                    DropDownListSchemeCategory.SelectedIndex = -1;
                    DropDownListSchemeType.SelectedIndex = -1;
                }
                //all types
                else
                {
                    List<HpSchemeType> _schemeList = CHNLSVC.Sales.GetSchemeTypeByCategory(DropDownListSchemeCategory.SelectedValue);
                    DataTable _result = new DataTable();
                    foreach (HpSchemeType scTy in _schemeList)
                    {
                        DataTable dt = CHNLSVC.Sales.GetSchemes("TYPE", scTy.Hst_cd);
                        _result.Merge(dt);
                    }
                    ///LoadGrid(GridViewSchemes, _result);
                    LoadList(ListBoxSchemes, _result, "HSD_CD", "HSD_DESC");
                    //set ddl selected value
                    DropDownListSchemeType.SelectedIndex = -1;

                }
            }
        }

        protected void CheckBoxPartyAll_CheckedChanged(object sender, EventArgs e)
        {
            CreateParty();
            DataTable dt = new DataTable();
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
            dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            foreach (DataRow dr in dt.Rows)
            {
                DataRow drT = Party.NewRow();
                drT[0] = "LOC";
                drT[1] = dr[0].ToString();
                Party.Rows.Add(drT);
                ListBoxParty.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            }
            dt.Rows.Clear();
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
            dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            foreach (DataRow dr in dt.Rows)
            {
                DataRow drT = Party.NewRow();
                drT[0] = "ZONE";
                drT[1] = dr[0].ToString();
                Party.Rows.Add(drT);
                ListBoxParty.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            }
            dt.Rows.Clear();
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
            dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            foreach (DataRow dr in dt.Rows)
            {
                DataRow drT = Party.NewRow();
                drT[0] = "REGION";
                drT[1] = dr[0].ToString();
                Party.Rows.Add(drT);
                ListBoxParty.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            }
            dt.Rows.Clear();
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
            dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            foreach (DataRow dr in dt.Rows)
            {
                DataRow drT = Party.NewRow();
                drT[0] = "SCHNL";
                drT[1] = dr[0].ToString();
                Party.Rows.Add(drT);
                ListBoxParty.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            }
            dt.Rows.Clear();
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
            dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            foreach (DataRow dr in dt.Rows)
            {
                DataRow drT = Party.NewRow();
                drT[0] = "CHNL";
                drT[1] = dr[0].ToString();
                Party.Rows.Add(drT);
                ListBoxParty.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            }
            dt.Rows.Clear();
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OPE);
            dt = CHNLSVC.CommonSearch.GetOPE(MasterCommonSearchUCtrl.SearchParams, null, null);
            foreach (DataRow dr in dt.Rows)
            {
                DataRow drT = Party.NewRow();
                drT[0] = "GPC";
                drT[1] = dr[0].ToString();
                Party.Rows.Add(drT);
                ListBoxParty.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            }
            dt.Rows.Clear();
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
            dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            foreach (DataRow dr in dt.Rows)
            {
                DataRow drT = Party.NewRow();
                drT[0] = "COM";
                drT[1] = dr[0].ToString();
                Party.Rows.Add(drT);
                ListBoxParty.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            }

            foreach (ListItem li in ListBoxParty.Items)
            {
                li.Selected = true;
            }
            DropDownListPartyTypes.SelectedIndex = -1;
        }

        protected void CheckBoxPriceBookAll_CheckedChanged(object sender, EventArgs e)
        {
            CreatePricBook();
            ListBoxPriceLevel.Items.Clear();
            List<PriceBookRef> _priceBook = CHNLSVC.Sales.GetPriceBooklist(GlbUserComCode);
            if (_priceBook.Count > 0)
            {
                foreach (PriceBookRef _pb in _priceBook)
                {
                    List<PriceBookLevelRef> _PbLevel = CHNLSVC.Sales.GetPriceLevelList(GlbUserComCode, _pb.Sapb_pb, null);
                    if (_PbLevel.Count > 0)
                    {
                        foreach (PriceBookLevelRef pbLv in _PbLevel)
                        {
                            DataRow dr = Price_book.NewRow();
                            dr[0] = _pb.Sapb_pb;
                            dr[1] = pbLv.Sapl_pb_lvl_cd;
                            Price_book.Rows.Add(dr);
                            ListBoxPriceLevel.Items.Add(new ListItem(pbLv.Sapl_pb_lvl_cd + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - pbLv.Sapl_pb_lvl_cd.Length)) + "-" + pbLv.Sapl_pb_lvl_desc, pbLv.Sapl_pb_lvl_cd));
                        }
                    }
                }
                foreach (ListItem li in ListBoxPriceLevel.Items)
                {
                    li.Selected = true;
                }
                DropDownListPriceBook.SelectedIndex = -1;
            }
        }

        #endregion

        #region radio_checked

        protected void RadioButtonRate_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonRate.Checked)
            {
                TextBoxServiceChgRt.Enabled = true;
                TextBoxServiceChargeAmo.Enabled = false;
                TextBoxServiceChargeAmo.Text = "0";
            }
            else {
                TextBoxServiceChgRt.Enabled = false;
                TextBoxServiceChargeAmo.Enabled = true;
                TextBoxServiceChgRt.Text = "0";
            }
        }

        protected void RadioButtonAmount_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonAmount.Checked)
            {
                TextBoxServiceChgRt.Enabled = false;
                TextBoxServiceChargeAmo.Enabled = true;
                TextBoxServiceChgRt.Text = "0";
            }
            else
            {
                TextBoxServiceChgRt.Enabled = true;
                TextBoxServiceChargeAmo.Enabled = false;
                TextBoxServiceChargeAmo.Text = "0";
            }
        }

        protected void RadioButtonAddrate_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonAddrate.Checked)
            {
                TextBoxAddRate.Enabled = true;
                TextBoxAddAmo.Enabled = false;
                TextBoxAddAmo.Text = "0";
            }
            else
            {
                TextBoxAddRate.Enabled = false;
                TextBoxAddAmo.Enabled = true;
                TextBoxAddRate.Text = "0";
            }
        }

        protected void RadioButtonAddAmo_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonAddAmo.Checked)
            {
                TextBoxAddRate.Enabled = false;
                TextBoxAddAmo.Enabled = true;
                TextBoxAddRate.Text = "0";
            }
            else
            {
                TextBoxAddRate.Enabled = true;
                TextBoxAddAmo.Enabled = false;
                TextBoxAddAmo.Text = "0";
            }
        }


        #endregion

    }
}