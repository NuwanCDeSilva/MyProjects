using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Collections;

namespace FF.WindowsERPClient.HP
{
    public partial class CashConversionDefinition :Base
    {
        //sp_getschmecat =UPDATE
        //pkg_search.sp_search_GPC  =NEW
        //sp_get_GPC =NEW
        //sp_getscheme_type_by_cat  =UPDATE
        //sp_getscheme_type_by_cat  =UPDATE
        //sp_getschemes   =UPDATE BY prabath

        List<string> calOn = new List<string>();

        public CashConversionDefinition()
        {
            InitializeComponent();

            LoadSchemeCategory();
            LoadPriceBook();
            LoadPriceBook_Convertable();
            bind_Combo_DropDownListPartyTypes();
            bind_Combo_DropDownListCheckOn();
            bind_Combo_DropDownListCalculateOnType();
            bind_Combo_DropDownListAddCharType();

            TextBoxToDate.Value = Convert.ToDateTime("31-Dec-2999").Date;
        }
        private void bind_Combo_DropDownListPartyTypes()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            PartyTypes.Add("GPC", "GPC");
            PartyTypes.Add("CHNL", "Channel");
            PartyTypes.Add("SCHNL", "Sub Channel");           
            PartyTypes.Add("AREA", "Area");
            PartyTypes.Add("REGION", "Region");
            PartyTypes.Add("ZONE", "Zone");
            PartyTypes.Add("PC", "PC");

            DropDownListPartyTypes.DataSource = new BindingSource(PartyTypes, null);
            DropDownListPartyTypes.DisplayMember = "Value";
            DropDownListPartyTypes.ValueMember = "Key";
        }

        private void bind_Combo_DropDownListCheckOn()
        {           
            Dictionary<string, string> checkOn = new Dictionary<string, string>();
            checkOn.Add("UP", "Unit Price");
            checkOn.Add("AF", "Amount Finance");
            checkOn.Add("HP", "Hire Value");
          
            DropDownListCheckOn.DataSource = new BindingSource(checkOn, null);            
            DropDownListCheckOn.DisplayMember = "Value";
            DropDownListCheckOn.ValueMember = "Key";
        }
        private void bind_Combo_DropDownListCalculateOnType()
        {
            Dictionary<string, string> checkOn = new Dictionary<string, string>();
            checkOn.Add("UP", "Unit Price");
            checkOn.Add("AF", "Amount Finance");
            checkOn.Add("HP", "Hire Value");
            
            DropDownListCalculateOnType.DataSource = new BindingSource(checkOn, null);
            DropDownListCalculateOnType.DisplayMember = "Value";
            DropDownListCalculateOnType.ValueMember = "Key";
        }
        private void bind_Combo_DropDownListAddCharType()
        {
            Dictionary<string, string> checkOn = new Dictionary<string, string>();
            checkOn.Add("UP", "Unit Price");
            checkOn.Add("AF", "Amount Finance");
            checkOn.Add("HP", "Hire Value");

            DropDownListAddCharType.DataSource = new BindingSource(checkOn, null);
            DropDownListAddCharType.DisplayMember = "Value";
            DropDownListAddCharType.ValueMember = "Key";
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            CashConversionDefinition formnew = new CashConversionDefinition();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

        private void LoadSchemeCategory()
        {
            DataTable dt = CHNLSVC.Sales.GetSAllchemeCategoryies("%");
            DropDownListSchemeCategory.DataSource = dt;
            DropDownListSchemeCategory.DisplayMember = "HSC_DESC";
            DropDownListSchemeCategory.ValueMember = "HSC_CD";

            //DropDownListSchemeCategory.Items.Clear();
            //DropDownListSchemeCategory.Items.Add(new ListItem("", "-1"));
            //foreach (DataRow dr in dt.Rows)
            //{
            //    DropDownListSchemeCategory.Items.Add(new ListItem(dr["HSC_CD"].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr["HSC_CD"].ToString().Length)) + "-" + dr["HSC_DESC"].ToString(), dr["HSC_CD"].ToString()));
            //}
            //DropDownListSchemeCategory.DataBind();
        }
        private void LoadSchemeType()
        {
            List<HpSchemeType> _schemeList = CHNLSVC.Sales.GetSchemeTypeByCategory(DropDownListSchemeCategory.SelectedValue.ToString());
            foreach (HpSchemeType sc in _schemeList)
            {
                string space = "";
                if ((10 - sc.Hst_cd.Length) > 0)
                {
                    for (int i = 0; i <= (10 - sc.Hst_cd.Length); i++)
                    {
                        space = space + " ";
                    }
                }
                sc.Hst_desc = sc.Hst_cd + space + "--" + sc.Hst_desc;
            }

            DropDownListSchemeType.DataSource = _schemeList;
            DropDownListSchemeType.DisplayMember = "Hst_desc";
            DropDownListSchemeType.ValueMember = "Hst_cd";

            CheckBoxAll.Checked = false;

            //List<HpSchemeType> _schemeList = CHNLSVC.Sales.GetSchemeTypeByCategory(DropDownListSchemeCategory.SelectedValue);
            //DropDownListSchemeType.Items.Clear();
            //DropDownListSchemeType.Items.Add(new ListItem("", "-1"));
            //if (_schemeList.Count > 0)
            //{
            //    foreach (HpSchemeType scTy in _schemeList)
            //    {
            //        DropDownListSchemeType.Items.Add(new ListItem(scTy.Hst_cd + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - scTy.Hst_cd.Length)) + "-" + scTy.Hst_desc, scTy.Hst_cd));
            //    }
            //}
            //DropDownListSchemeType.DataBind();
            //CheckBoxAll.Checked = false;
        }
        private void LoadPriceBook()
        {
            List<PriceBookRef> _priceBook = CHNLSVC.Sales.GetPriceBooklist(BaseCls.GlbUserComCode);         
            DropDownListPriceBook.DataSource = _priceBook;
            DropDownListPriceBook.DisplayMember = "Sapb_desc";
            DropDownListPriceBook.ValueMember = "Sapb_pb";
            
            //DropDownListPriceBook.Items.Clear();
            //DropDownListPriceBook.Items.Add(new ListItem("", "-1"));
            //if (_priceBook.Count > 0)
            //{
            //    foreach (PriceBookRef _pb in _priceBook)
            //    {
            //        DropDownListPriceBook.Items.Add(new ListItem(_pb.Sapb_pb + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - _pb.Sapb_pb.Length)) + "-" + _pb.Sapb_desc, _pb.Sapb_pb));
            //    }
            //}
            //DropDownListPriceBook.DataBind();
            //foreach (ListItem _listItem in this.DropDownListPriceBook.Items)
            //{

            //    _listItem.Attributes.Add("title", _listItem.Text);
            //}
        }
        private void LoadPriceBook_Convertable()
        {
            List<PriceBookRef> _priceBook = CHNLSVC.Sales.GetPriceBooklist(BaseCls.GlbUserComCode);
            DropDownListPBConvertable.DataSource = _priceBook;
            DropDownListPBConvertable.DisplayMember = "Sapb_desc";
            DropDownListPBConvertable.ValueMember = "Sapb_pb";
        }
        private void DropDownListPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
              
        private void btnAddPB_Click(object sender, EventArgs e)
        {
            if (CheckBoxPriceBookAll.Checked == true)
            {
                List<PriceBookLevelRef> _PbLevel_All = new List<PriceBookLevelRef>();
                List<PriceBookRef> _priceBook = CHNLSVC.Sales.GetPriceBooklist(BaseCls.GlbUserComCode);
                foreach (PriceBookRef pb in _priceBook)
                {
                    List<PriceBookLevelRef> _PbLevel = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, pb.Sapb_pb, null);
                    _PbLevel_All.AddRange(_PbLevel);
                    grvPriceLevel.DataSource = null;
                    grvPriceLevel.AutoGenerateColumns = false;
                    //grvPriceLevel.DataSource = _PbLevel;
                }
                grvPriceLevel.DataSource = _PbLevel_All;
            }
            else
            {
                List<PriceBookLevelRef> _PbLevel = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, DropDownListPriceBook.SelectedValue.ToString(), null);
                grvPriceLevel.DataSource = null;
                grvPriceLevel.AutoGenerateColumns = false;
                grvPriceLevel.DataSource = _PbLevel;
            }
            
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GPC:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OPE:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }
        private void btnAddPartys_Click(object sender, EventArgs e)
        {
            Base  _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
     
            DataTable dt = new DataTable();
            DataTable _result = new DataTable();
            if (DropDownListPartyTypes.SelectedValue.ToString() == "COM")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "CHNL")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                 _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "SCHNL")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                 _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
           
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "AREA")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                 _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "REGION")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                 _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "ZONE")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                 _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "PC")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                 _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "GPC")
            {
               // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GPC);
              //  _result = _basePage.CHNLSVC.CommonSearch.Get_GPC(_CommonSearch.SearchParams, null, null);
               _result = CHNLSVC.General.Get_GET_GPC("", "");
            }
            grvParty.DataSource = null;
            grvParty.AutoGenerateColumns = false;
            grvParty.DataSource = _result;
        }

        private void DropDownListSchemeCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnClearScheme_Click(null, null);
            LoadSchemeType();
        }

        private void DropDownListSchemeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnClearScheme_Click(null, null);
            this.btnAddSchemes_Click(null, null);
        }
        private void btnAddSchemes_Click(object sender, EventArgs e)
        {
            //if( DropDownListSchemeType.SelectedIndex==-1)
            //{
            //    return;
            //}
            //DataTable datasource = CHNLSVC.Sales.GetSchemes("TYPE", DropDownListSchemeType.SelectedValue.ToString());

            ////  LoadList(ListBoxSchemes, datasource, "HSD_CD", "HSD_DESC");
            //CheckBoxAll.Checked = false;

            //grvSchemes.DataSource = null;
            //grvSchemes.AutoGenerateColumns = false;
            //grvSchemes.DataSource = datasource;
            if (DropDownListSchemeCategory.SelectedIndex == -1)
            {
                return;
            }
            if (CheckBoxAll.Checked==true)
            {
                DropDownListSchemeType.SelectedIndex = -1;
            }
            if (CheckBoxAll.Checked == true)
            {
                List<HpSchemeType> _schemeList = CHNLSVC.Sales.GetSchemeTypeByCategory(DropDownListSchemeCategory.SelectedValue.ToString());
                DropDownListSchemeType.DataSource = _schemeList;
                DropDownListSchemeType.DisplayMember = "Hst_desc";
                DropDownListSchemeType.ValueMember = "Hst_cd";

                DataTable dt = new DataTable();
                foreach (HpSchemeType schTp in _schemeList)
                {
                    DataTable dt1 = CHNLSVC.Sales.GetSchemes("TYPE", schTp.Hst_cd);
                    dt.Merge(dt1);
                }

                grvSchemes.DataSource = null;
                grvSchemes.AutoGenerateColumns = false;
                grvSchemes.DataSource = dt;
            }
            else
            {
                if (DropDownListSchemeType.SelectedIndex == -1)
                {
                    return;
                }
                DataTable datasource = CHNLSVC.Sales.GetSchemes("TYPE", DropDownListSchemeType.SelectedValue.ToString());
                //  LoadList(ListBoxSchemes, datasource, "HSD_CD", "HSD_DESC");
                CheckBoxAll.Checked = false;

                grvSchemes.DataSource = null;
                grvSchemes.AutoGenerateColumns = false;
                grvSchemes.DataSource = datasource;
            }

        }

        private void btnClearScheme_Click(object sender, EventArgs e)
        {
            checkBox_SCEME.Checked = false;
            grvSchemes.DataSource = null;
            grvSchemes.AutoGenerateColumns = false;
        }

        private void btnClearHirchy_Click(object sender, EventArgs e)
        {
            checkBox_HIERCHY.Checked = false;
            grvParty.DataSource = null;
            grvParty.AutoGenerateColumns = false;
        }

        private void btnClearPB_Click(object sender, EventArgs e)
        {
            checkBox_PB.Checked = false;
            grvPriceLevel.DataSource = null;
            grvPriceLevel.AutoGenerateColumns = false;
        }

        private void DropDownListPartyTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnClearHirchy_Click(null, null);
            this.btnAddPartys_Click(null, null);
        }

        private void DropDownListPriceBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnClearPB_Click(null, null);
            this.btnAddPB_Click(null, null);
        }

        private void btnAll_Schemes_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvSchemes.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvSchemes.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnNon_Schemes_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvSchemes.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvSchemes.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnAll_Hirchy_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvParty.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvParty.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnNon_Hierachy_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvParty.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvParty.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnAll_pb_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvPriceLevel.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvPriceLevel.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnNon_pb_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvPriceLevel.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvPriceLevel.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void RadioButtonRate_CheckedChanged(object sender, EventArgs e)
        {           
            if (RadioButtonRate.Checked)
            {
                TextBoxServiceChgRt.Enabled = true;
                TextBoxServiceChargeAmo.Enabled = false;
                TextBoxServiceChargeAmo.Text = "0.00";
            }
            else
            {
                TextBoxServiceChgRt.Enabled = false;
                TextBoxServiceChargeAmo.Enabled = true;
                TextBoxServiceChgRt.Text = "0.00";
            }
        }

        private void RadioButtonAmount_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonAmount.Checked)
            {
                TextBoxServiceChgRt.Enabled = false;
                TextBoxServiceChargeAmo.Enabled = true;
                TextBoxServiceChgRt.Text = "0.00";
            }
            else
            {
                TextBoxServiceChgRt.Enabled = true;
                TextBoxServiceChargeAmo.Enabled = false;
                TextBoxServiceChargeAmo.Text = "0.00";
            }


        }

        private void RadioButtonAddrate_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonAddrate.Checked)
            {
                TextBoxAddRate.Enabled = true;
                TextBoxAddAmo.Enabled = false;
                TextBoxAddAmo.Text = "0.00";
            }
            else
            {
                TextBoxAddRate.Enabled = false;
                TextBoxAddAmo.Enabled = true;
                TextBoxAddRate.Text = "0.00";
            }
        }

        private void RadioButtonAddAmo_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonAddAmo.Checked)
            {
                TextBoxAddRate.Enabled = false;
                TextBoxAddAmo.Enabled = true;
                TextBoxAddRate.Text = "0.00";
            }
            else
            {
                TextBoxAddRate.Enabled = true;
                TextBoxAddAmo.Enabled = false;
                TextBoxAddAmo.Text = "0.00";
            }
        }
        //private List<string> GetSelectedItemsList()
        //{
        //    List<string> list = new List<string>();
        //    foreach (DataGridViewRow dgvr in GridAll_Items.Rows)
        //    {
        //        DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
        //        if (Convert.ToBoolean(chk.Value) == true)
        //        {
        //            list.Add(dgvr.Cells[1].Value.ToString());
        //        }

        //    }
        //    return list;
        //}

        //**********************************************************************************************************
        private List<string> get_selected_Schemes()
        {
            grvSchemes.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvSchemes.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }
            }
            return list;
        }
        private List<string> get_selected_priceBooks()
        {
            grvPriceLevel.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvPriceLevel.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }
            }
            return list;
        }
        private List<string> get_selected_priceBooksLevels()
        {
            grvPriceLevel.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvPriceLevel.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[2].Value.ToString());
                }
            }
            return list;
        }
        private List<string> get_selected_hierchyTypes()
        {
            grvParty.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvParty.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }
            }
            return list;
        }
        private List<string> get_selected_hierchyCodes()
        {
            grvParty.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvParty.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(DropDownListPartyTypes.SelectedValue.ToString());
                }
            }
            return list;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // MessageBox.Show(DropDownListCheckOn.SelectedValue.ToString());

            #region 
            List<string> list_schems = get_selected_Schemes();
            List<string> list_hierchyTpCode = get_selected_hierchyCodes();
            List<string> list_hierchyTp = get_selected_hierchyTypes();
            List<string> list_PB = get_selected_priceBooks();
            List<string> list_PB_LVL= get_selected_priceBooksLevels();
            if (list_schems.Count > 0 && list_hierchyTp.Count > 0 && list_PB.Count > 0)
            //if (grvSchemes.Rows.Count > 0 && grvParty.Rows.Count > 0 && grvParty.Rows.Count > 0)
            {
                #region validation

                if (TextBoxFomAmo.Text == "")
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter from amount");
                    MessageBox.Show("Please enter from amount", "Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);                    
                    return;
                }
                if (TextBoxToAmo.Text == "")
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter to amount");
                    MessageBox.Show("Please enter to amount", "Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {
                    if (Convert.ToDecimal(TextBoxFomAmo.Text) > Convert.ToDecimal(TextBoxToAmo.Text))
                    {
                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From value has to be smller than to value");
                        MessageBox.Show("From value has to be smller than to value", "Value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }                
                }
                catch(Exception ex){
                    MessageBox.Show("Please enter valid 'To' and 'From' amounts", "Valid Amounts", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
              
                if (TextBoxFromDate.Text == "")
                {
                   // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter account from date");
                    MessageBox.Show("Please enter account from date", "Enter Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (TextBoxToDate.Text == "")
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter account to date");
                    MessageBox.Show("Please enter account to date", "Enter Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    if (Convert.ToDateTime(TextBoxFromDate.Text) > Convert.ToDateTime(TextBoxToDate.Text))
                    {
                        // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Account Period From date has to be smller than Account Period to date");
                        MessageBox.Show("Account Period From date has to be smller than Account Period to date", "Account Period", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                catch(Exception ex){
                    MessageBox.Show("Please enter valid 'Account Period From and To dates'", "Account Period", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
               
                if (TextBoxFromDy.Text == "")
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter from period");
                    MessageBox.Show("Please enter from period", "Period", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (TextBoxToDy.Text == "")
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter to period");
                    MessageBox.Show("Please enter to period", "Period", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    if (Convert.ToInt32(TextBoxFromDy.Text) > Convert.ToInt32(TextBoxToDy.Text))
                    {
                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From period from has to be smller than period to");
                        MessageBox.Show("From period from has to be smller than period to", "Valid Period", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                catch(Exception ex){
                    MessageBox.Show("Please enter valid To and From periods ", "Valid Period", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
               
                if (DropDownListPBConvertable.SelectedIndex == -1)
                {
                   // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select convertable price book");
                    MessageBox.Show("Please select convertable price book", "Price book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (TextBoxConvertionupto.Text == "")
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter conversion end date");
                    MessageBox.Show("Please enter conversion end date", "Enter Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //--------------
                try
                {
                    Convert.ToDecimal(TextBoxServiceChgRt.Text.Trim());
                    Convert.ToDecimal(TextBoxServiceChargeAmo.Text.Trim());
                }
                catch(Exception ex){
                    MessageBox.Show("Please enter valid 'Service Charge Rate/Amount'", "Service Charge Rate/Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                try
                {
                    Convert.ToDecimal(TextBoxAddRate.Text.Trim());
                    Convert.ToDecimal(TextBoxAddAmo.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please enter valid 'Additional Service Charge Rate/Amount'", "Additional Service Charge Rate/Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    Convert.ToDecimal(TextBoxInsRefundRt.Text.Trim());
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please enter valid 'Insurance Charge Rate'", "Insurance Charge Rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //----------------------------------------------

                if (MessageBox.Show("Are you sure to save ?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                #endregion
                string _schenmeCode = string.Empty;
                string _partyType = string.Empty;
                string _partyCode = string.Empty;
                string _pb = string.Empty;
                string _pbLvl = string.Empty;

                //*********************************************
                foreach (string schm in list_schems)
                {
                    _schenmeCode = _schenmeCode + schm + ",";                  
                }
                _schenmeCode = _schenmeCode.Substring(0, _schenmeCode.Length - 1);
                ////get scheme code string
                //foreach (ListItem li in ListBoxSchemes.Items)
                //{
                //    if (li.Selected)
                //    {
                //        _schenmeCode = _schenmeCode + li.Value + ",";

                //    }
                //}
                //_schenmeCode = _schenmeCode.Substring(0, _schenmeCode.Length - 1);
                //*********************************************


                //*********************************************
                //list_hierchyTp
                foreach (string tp in list_hierchyTp)
                {
                    _partyType = _partyType + tp + ",";
                }
                foreach (string tp in list_hierchyTpCode)
                {
                    _partyCode = _partyCode + tp + ",";
                }
                _partyType = _partyType.Substring(0, _partyType.Length - 1);
                _partyCode = _partyCode.Substring(0, _partyCode.Length - 1);
                
                ////get party type and party code strings
                //foreach (DataRow dr in Party.Rows)
                //{
                //    _partyType = _partyType + dr[0].ToString() + ",";
                //    _partyCode = _partyCode + dr[1].ToString() + ",";
                //}
                //_partyType = _partyType.Substring(0, _partyType.Length - 1);
                //_partyCode = _partyCode.Substring(0, _partyCode.Length - 1);

                //*********************************************


                //*********************************************
                foreach (string book in list_PB)
                {
                    _pb = _pb + book + ",";                    
                }

                foreach (string lvl in list_PB_LVL)
                {
                    _pbLvl = _pbLvl + lvl + ",";
                }
                _pbLvl = _pbLvl.Substring(0, _pbLvl.Length - 1);
                _pb = _pb.Substring(0, _pb.Length - 1);

                //    foreach (DataRow dr in Price_book.Rows)
                //    {
                //        _pb = _pb + dr[0].ToString() + ",";
                //        _pbLvl = _pbLvl + dr[1].ToString() + ",";
                //    }
                //    _pb = _pb.Substring(0, _pb.Length - 1);
                //    _pbLvl = _pbLvl.Substring(0, _pbLvl.Length - 1);
                //*********************************************
           

                FF.BusinessObjects.CashConversionDefinition _cashdefi = new FF.BusinessObjects.CashConversionDefinition();

                _cashdefi.Hcc_from_cre_dt = Convert.ToDateTime(TextBoxFromDate.Text);
                _cashdefi.Hcc_to_cre_dt = Convert.ToDateTime(TextBoxToDate.Text);

                _cashdefi.Hcc_from_pd = Convert.ToInt32(TextBoxFromDy.Text);
                _cashdefi.Hcc_to_pd = Convert.ToInt32(TextBoxToDy.Text);

                _cashdefi.Hcc_from_val = Convert.ToDecimal(TextBoxFomAmo.Text);
                _cashdefi.Hcc_to_val = Convert.ToDecimal(TextBoxToAmo.Text);

                _cashdefi.Hcc_chk_on = DropDownListCheckOn.SelectedValue.ToString();
                _cashdefi.Hcc_cal_on = DropDownListCalculateOnType.SelectedValue.ToString();
                if(TextBoxServiceChgRt.Enabled==true)
                {
                 _cashdefi.Hcc_ser_chg_rt = Convert.ToDecimal(TextBoxServiceChgRt.Text);
                 _cashdefi.Hcc_ser_chg_val = 0;
                }
                else
                {
                     _cashdefi.Hcc_ser_chg_rt = 0;
                     _cashdefi.Hcc_ser_chg_val = Convert.ToDecimal(TextBoxServiceChargeAmo.Text);
                }

                if (TextBoxAddRate.Enabled == true)
                {
                    _cashdefi.Hcc_add_chg_rt = Convert.ToDecimal(TextBoxAddRate.Text);
                    _cashdefi.Hcc_add_chg_val = 0;
                }
                else
                {
                    _cashdefi.Hcc_add_chg_rt = 0;
                    _cashdefi.Hcc_add_chg_val = Convert.ToDecimal(TextBoxAddAmo.Text);
                }
                               
              //  _cashdefi.Hcc_ser_chg_val = Convert.ToDecimal(TextBoxServiceChargeAmo.Text);
                _cashdefi.Hcc_add_cal_on = DropDownListAddCharType.SelectedValue.ToString();
               // _cashdefi.Hcc_add_chg_rt = Convert.ToDecimal(TextBoxAddAmo.Text);
               // _cashdefi.Hcc_add_chg_val = Convert.ToDecimal(TextBoxAddRate.Text);
                _cashdefi.Hcc_pb_conv = DropDownListPBConvertable.SelectedValue.ToString();
                _cashdefi.Hcc_cre_by = BaseCls.GlbUserID;
                _cashdefi.Hcc_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;
                _cashdefi.Hcc_cc_upto = TextBoxConvertionupto.Value.Date;
                Int32 effect = 0;
                try
                {
                     effect = CHNLSVC.Sales.SaveCashConv(_schenmeCode, _partyType, _partyCode, _pb, _pbLvl, _cashdefi);
                }
                catch(Exception EX){
                    MessageBox.Show("Saving error.\n\n" + EX.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                if (effect > 0)
                {
                    MessageBox.Show("Sucessfully Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.btnClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Not Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                //if (MessageBox.Show("Do you want to clear screen?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                //{
                //    return;
                //}
                //else
                //{
                //    this.btnClear_Click(null, null);
                //}
                
            //   // string Msg = "<script>alert('Record Insert Sucessfully!!');window.location = 'CashConversion.aspx';</script>";
            //    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            else
            {
                //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select atleaset one Scheme,Business Code and Price book level to continue.");
                MessageBox.Show("Please select atleaset one Scheme,Business Code and Price book level to continue.", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
            
         
          #endregion
        }

        private void TextBoxFromDy_Leave(object sender, EventArgs e)
        {
            //if (Regex.IsMatch(txtBankSelling.Text.Trim(), "[A-Z]+")) //should contain atleast one capital letter
            //{
            //    MessageBox.Show("Enter valid rate", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    TextBoxFromDy.Text = "0";
            //    TextBoxFromDy.Focus();
            //    return;
            //}
            try
            {
                Convert.ToInt32(TextBoxFromDy.Text.Trim());
            }
            catch(Exception ex){

                MessageBox.Show("Enter valid 'Form' period", "Period", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxFromDy.Text = "0";
                TextBoxFromDy.Focus();
                return;
            }
            if (Convert.ToInt32(TextBoxFromDy.Text.Trim())<0)
            {
                MessageBox.Show("From period cannot be minus", "Period", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxFromDy.Text = "0";
                TextBoxFromDy.Focus();
                return;
            }
        }

        private void TextBoxToDy_Leave(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(TextBoxToDy.Text.Trim());
            }
            catch (Exception ex)
            {

                MessageBox.Show("Enter valid 'To' period", "Period", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxToDy.Text = "";
                TextBoxToDy.Focus();
                return;
            }
            if (Convert.ToInt32(TextBoxToDy.Text.Trim()) < 0)
            {
                MessageBox.Show("To period cannot be minus", "Period", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //TextBoxFromDy.Text = "0";
                TextBoxFromDy.Focus();
                return;
            }
            if (Convert.ToInt32(TextBoxToDy.Text.Trim()) < Convert.ToInt32(TextBoxFromDy.Text.Trim()))
            {
                MessageBox.Show("To period should be greater than From period", "Period", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //TextBoxToDy.Text = "0";
                TextBoxToDy.Focus();
                return;
            }
        }

        private void TextBoxFomAmo_Leave(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDecimal(TextBoxFomAmo.Text.Trim());
            }
            catch (Exception ex)
            {

                MessageBox.Show("Enter valid 'From' amount", "Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxFomAmo.Text = "";
                TextBoxFomAmo.Focus();
                return;
            }
            if (Convert.ToDecimal(TextBoxFomAmo.Text.Trim()) < 0)
            {
                MessageBox.Show("From amount cannot be minus", "Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxFomAmo.Text = "0";
                TextBoxFomAmo.Focus();
                return;
            }
        }

        private void TextBoxToAmo_Leave(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDecimal(TextBoxToAmo.Text.Trim());
            }
            catch (Exception ex)
            {

                MessageBox.Show("Enter valid 'To' amount", "Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxToAmo.Text = "";
                TextBoxToAmo.Focus();
                return;
            }

            if (Convert.ToDecimal(TextBoxToAmo.Text.Trim()) < 0)
            {
                MessageBox.Show("To amount cannot be minus", "Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //TextBoxFromDy.Text = "0";
                TextBoxFromDy.Focus();
                return;
            }
            if (Convert.ToDecimal(TextBoxToAmo.Text.Trim()) < Convert.ToDecimal(TextBoxFomAmo.Text.Trim()))
            {
                MessageBox.Show("To amount should be greater than From amount", "Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //TextBoxToDy.Text = "0";
                TextBoxToDy.Focus();
                return;
            }
        }

        private void TextBoxInsRefundRt_Leave(object sender, EventArgs e)
        {
            //try
            //{
            //    Convert.ToDecimal(TextBoxInsRefundRt.Text.Trim());
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show("Enter valid 'Refund Rate'", "Refund Rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    TextBoxInsRefundRt.Text = "";
            //    TextBoxInsRefundRt.Focus();
            //    return;
            //}
            if (Convert.ToDecimal(TextBoxInsRefundRt.Text.Trim()) > 100)
            {
                MessageBox.Show("Invalid refund rate.\n(Should be less than or equal to 100)", "Refund rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxInsRefundRt.Focus();
                return;
            }
            if (Convert.ToDecimal(TextBoxInsRefundRt.Text.Trim()) < 0)
            {
                MessageBox.Show("Invalid refund rate.\n(Cannot be minus)", "Refund rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxInsRefundRt.Focus();
                return;
            }
            else
            {
               // TextBoxServiceChgRt.Focus();
            }
        }

        private void TextBoxServiceChgRt_Leave(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDecimal(TextBoxServiceChgRt.Text.Trim());
            }
            catch (Exception ex)
            {

                MessageBox.Show("Enter valid 'Service Charge Rate'", "Service Charge Rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxServiceChgRt.Text = "";
                TextBoxServiceChgRt.Focus();
                return;
            }
            //---------------
            if (Convert.ToDecimal(TextBoxServiceChgRt.Text.Trim()) > 100)
            {
                MessageBox.Show("Invalid rate.\n(Should be less than or equal to 100).", "Invalid rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxInsRefundRt.Focus();
                return;
            }
            if (Convert.ToDecimal(TextBoxServiceChgRt.Text.Trim()) < 0)
            {
                MessageBox.Show("Invalid rate.\n(Cannot be minus)", "Invalid rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxServiceChgRt.Focus();
                return;
            }
            TextBoxAddRate.Focus();
            //-------------
        }

        private void TextBoxServiceChargeAmo_Leave(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDecimal(TextBoxServiceChargeAmo.Text.Trim());
            }
            catch (Exception ex)
            {

                MessageBox.Show("Enter valid 'Service Charge Amount'", "Service Charge Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //TextBoxServiceChargeAmo.Text = "";
                TextBoxServiceChargeAmo.Focus();
                return;
            }
            if (Convert.ToDecimal(TextBoxServiceChargeAmo.Text.Trim()) < 0)
            {
                MessageBox.Show("Invalid amount.\n(Cannot be minus)", "Invalid amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxServiceChargeAmo.Focus();
                return;
            }
        }

        private void TextBoxAddRate_Leave(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDecimal(TextBoxAddRate.Text.Trim());
            }
            catch (Exception ex)
            {

                MessageBox.Show("Enter valid 'Additional Service Charge Rate'", "Additional Service Charge Rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxAddRate.Text = "";
                TextBoxAddRate.Focus();
                DropDownListAddCharType.DroppedDown = false;
                return;
            }

            if (Convert.ToDecimal(TextBoxAddRate.Text.Trim()) > 100)
            {
                MessageBox.Show("Invalid rate.\n(Should be less than or equal to 100).", "Invalid rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxAddRate.Focus();
                DropDownListAddCharType.DroppedDown = false;
                return;
            }
            if (Convert.ToDecimal(TextBoxAddRate.Text.Trim()) < 0)
            {
                MessageBox.Show("Invalid rate.\n(Cannot be minus)", "Invalid rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxAddRate.Focus();
                DropDownListAddCharType.DroppedDown = false;
                return;
            }
            DropDownListAddCharType.DroppedDown = true;
        }

        private void TextBoxAddAmo_Leave(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDecimal(TextBoxAddAmo.Text.Trim());
            }
            catch (Exception ex)
            {

                MessageBox.Show("Enter valid 'Additional Service Charge Amount'", "Additional Service Charge Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxAddAmo.Text = "";
                TextBoxAddAmo.Focus();
                return;
            }
            if (Convert.ToDecimal(TextBoxAddAmo.Text.Trim()) < 0)
            {
                MessageBox.Show("Invalid amount.\n(Cannot be minus)", "Invalid amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxAddAmo.Focus();               
                return;
            }
            DropDownListAddCharType.DroppedDown = true;
        }

        private void TextBoxFromDate_ValueChanged(object sender, EventArgs e)
        {
            //TextBoxToDate.Value = TextBoxFromDate.Value;
            //if (TextBoxToDate.Value < TextBoxFromDate.Value)
            //{
            //    MessageBox.Show("To date should be greater than From date'", "Valid Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    TextBoxToDate.Focus();
            //}
        }

        private void TextBoxToDate_ValueChanged(object sender, EventArgs e)
        {
            //if (TextBoxToDate.Value < TextBoxFromDate.Value)
            //{
            //    MessageBox.Show("To date should be greater than From date'", "Valid Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    TextBoxToDate.Focus();
            //}
        }

        private void TextBoxToDate_Leave(object sender, EventArgs e)
        {
            if (TextBoxToDate.Value < TextBoxFromDate.Value)
            {
                MessageBox.Show("To date should be greater than From date'", "Valid Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);                
               // TextBoxToDate.Focus();
                return;
            }
        }

        private void checkBox_SCEME_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_SCEME.Checked==true)
            {
                this.btnAll_Schemes_Click(null,null);
            }
            else
            {
                this.btnNon_Schemes_Click(null, null);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_PB.Checked == true)
            {
                this.btnAll_pb_Click(null, null);
            }
            else
            {
                this.btnNon_pb_Click(null, null);
            }
        }

        private void checkBox_HIERCHY_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_HIERCHY.Checked == true)
            {
                this.btnAll_Hirchy_Click(null, null);
            }
            else
            {
                this.btnNon_Hierachy_Click(null, null);
            }
        }

        private void TextBoxFromDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxToDate.Focus();
            }
           
        }

        private void TextBoxToDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxFromDy.Focus();
            }
        }

        private void TextBoxToDy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxFomAmo.Focus();
            }
        }

        private void TextBoxFomAmo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxToAmo.Focus();
            }
        }

        private void TextBoxToAmo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                DropDownListCheckOn.Focus();
                DropDownListCheckOn.DroppedDown = true;
            }
        }

        private void DropDownListCheckOn_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DropDownListCalculateOnType.Focus();
            DropDownListCalculateOnType.DroppedDown = true;
        }

        private void DropDownListCalculateOnType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            TextBoxValidUpTo.Focus();
        }

        private void TextBoxValidUpTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxInsRefundRt.Focus();
            }
        }

        private void TextBoxFromDy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxToDy.Focus();
            }
        }

        private void TextBoxServiceChgRt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxAddRate.Focus();
            }
        }

        private void TextBoxServiceChargeAmo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxAddRate.Focus();
            }
        }

        private void TextBoxAddRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                DropDownListAddCharType.Focus();
               // DropDownListAddCharType.DroppedDown = true;
            }
        }

        private void TextBoxAddAmo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                DropDownListAddCharType.Focus();
                DropDownListAddCharType.DroppedDown = true;
            }
        }

        private void DropDownListAddCharType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            TextBoxConvertionupto.Focus();
        }

        private void TextBoxInsRefundRt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                //if (Convert.ToDecimal(TextBoxInsRefundRt.Text.Trim()) > 100)
                //{
                //    MessageBox.Show("Invalid refund rate.", "Refund rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    TextBoxInsRefundRt.Focus();
                //    return;
                //}
                //if (Convert.ToDecimal(TextBoxInsRefundRt.Text.Trim()) < 0)
                //{
                //    MessageBox.Show("Invalid refund rate.\nCannot be minus.", "Refund rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    TextBoxInsRefundRt.Focus();
                //    return;
                //}
                //else
                //{
                //    TextBoxServiceChgRt.Focus();
                //}
                TextBoxServiceChgRt.Focus(); 
                //DropDownListCheckOn.Focus();
               // DropDownListCheckOn.DroppedDown = true;
            }
        }

        private void DropDownListSchemeCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DropDownListSchemeType.Focus();
            DropDownListSchemeType.DroppedDown = true;
        }

        private void CheckBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            //if (CheckBoxAll.Checked == false)
            //{
            //    this.btnClearScheme_Click(null, null);
            //}
            DropDownListSchemeType.SelectedIndex = -1;
           
        }

        private void txtHierchCode_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {
                txtHierchCode.Focus();
                if (grvParty.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dgvr in grvParty.Rows)
                    {
                        if (dgvr.Cells["party_Code"].Value.ToString() == txtHierchCode.Text.Trim())
                        {
                            DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dgvr.Cells[0];
                            chk.Value = true;
                            dgvr.Selected = true;
                            MessageBox.Show("Selected!", "Select", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtHierchCode.Text = "";
                            //return;
                        }
                        else
                        {
                            DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                            if (Convert.ToBoolean(chk.Value) == false)
                            {
                                dgvr.Selected = false;
                            }
                        }
                    }
                }

            }
        }

        private void btnHierachySearch_Click(object sender, EventArgs e)
        {
            Base _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            DataTable dt = new DataTable();
            DataTable _result = new DataTable();
            if (DropDownListPartyTypes.SelectedValue.ToString() == "COM")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "CHNL")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "SCHNL")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }

            else if (DropDownListPartyTypes.SelectedValue.ToString() == "AREA")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "REGION")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "ZONE")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "PC")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "GPC")
            {
                // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GPC);
                //  _result = _basePage.CHNLSVC.CommonSearch.Get_GPC(_CommonSearch.SearchParams, null, null);
                _result = CHNLSVC.General.Get_GET_GPC("", "");
            }

            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtHierchCode;
            _CommonSearch.ShowDialog();
            txtHierchCode.Focus();
        }

        private void TextBoxFromDate_Leave(object sender, EventArgs e)
        {            
            if (TextBoxToDate.Value < TextBoxFromDate.Value)
            {
                MessageBox.Show("To date should be greater than From date'", "Valid Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               // TextBoxFromDate.Focus();
            }
        }
        
       
    }
}
