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
using System.Diagnostics;

namespace FF.WindowsERPClient.HP
{
    public partial class EcdDefinition :Base
    {
        public EcdDefinition()
        {
            InitializeComponent();
            LoadSchemeCategory();
            LoadPriceBook();           
            bind_Combo_DropDownListPartyTypes();
            TextBoxToDate.Value= Convert.ToDateTime("31-Dec-2999").Date;
            DropDownListSchemeCategory.SelectedIndex = -1;
            DropDownListSchemeType.SelectedIndex = -1;
           // DropDownListPriceBook.SelectedIndex = -1;
           // DropDownListPartyTypes.SelectedIndex = -1;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            EcdDefinition formnew = new EcdDefinition();
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
        }
        private void LoadPriceBook()
        {
            List<PriceBookRef> _priceBook = CHNLSVC.Sales.GetPriceBooklist(BaseCls.GlbUserComCode);
            DropDownListPriceBook.DataSource = _priceBook;
            DropDownListPriceBook.DisplayMember = "Sapb_desc";
            DropDownListPriceBook.ValueMember = "Sapb_pb";
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

        private void DropDownListSchemeCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnClearScheme_Click(null, null);
            LoadSchemeType();
        }
        private void LoadSchemeType()
        {
            if (DropDownListSchemeCategory.SelectedIndex==-1)
            {
                return;
            }
            List<HpSchemeType> _schemeList = CHNLSVC.Sales.GetSchemeTypeByCategory(DropDownListSchemeCategory.SelectedValue.ToString());
            if (_schemeList==null)
            {
                DropDownListSchemeType.DataSource = null;
                return;
            }

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
        }
        private void btnClearScheme_Click(object sender, EventArgs e)
        {
            checkBox_SCEME.Checked = false;
            grvSchemes.DataSource = null;
            grvSchemes.AutoGenerateColumns = false;
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

        private void btnClearPB_Click(object sender, EventArgs e)
        {
            checkBox_PB.Checked = false;
            grvPriceLevel.DataSource = null;
            grvPriceLevel.AutoGenerateColumns = false;
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

        private void btnClearHirchy_Click(object sender, EventArgs e)
        {
            checkBox_HIERCHY.Checked = false;
            grvParty.DataSource = null;
            grvParty.AutoGenerateColumns = false;
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
        //------------------------------------------------------------------------
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
        //private Dictionary<string,string> get_selected_priceBooksLevels_DIC()
        //{
        //    grvPriceLevel.EndEdit();
        //    Dictionary<string, string> list = new Dictionary<string,string>();
        //    foreach (DataGridViewRow dgvr in grvPriceLevel.Rows)
        //    {
        //        DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
        //        if (Convert.ToBoolean(chk.Value) == true)
        //        {
        //            list.Add(dgvr.Cells[1].Value.ToString(),dgvr.Cells[2].Value.ToString());
        //        }
        //    }
        //    return list;
        //}
        private void btnAddSchemes_Click(object sender, EventArgs e)
        {
            if (DropDownListSchemeCategory.SelectedIndex == -1)
            {
                return;
            }
            if (CheckBoxAll.Checked == true)
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
                CheckBoxAll.Checked = false;

                grvSchemes.DataSource = null;
                grvSchemes.AutoGenerateColumns = false;
                grvSchemes.DataSource = datasource;
            }
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
                    //--------------------------------------------------------------------------------------------
                    foreach(PriceBookLevelRef pbl in _PbLevel)
                    {
                        var _duplicate = from _dup in _PbLevel_All
                                         where _dup.Sapl_pb == pb.Sapb_pb && _dup.Sapl_pb_lvl_cd == pbl.Sapl_pb_lvl_cd
                                         select _dup;
                        if (_duplicate.Count() == 0)
                        {
                            //addList.Add(obj);
                            _PbLevel_All.Add(pbl);
                        } 
                    }  
                   // _PbLevel_All.AddRange(_PbLevel);
                   //--------------------------------------------------------------------------------------------
                    grvPriceLevel.DataSource = null;
                    grvPriceLevel.AutoGenerateColumns = false;
                    //grvPriceLevel.DataSource = _PbLevel;
                }
                grvPriceLevel.DataSource = _PbLevel_All;
            }
            else
            {
                List<PriceBookLevelRef> _PbLevel = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, DropDownListPriceBook.SelectedValue.ToString(), null);
                //--------------------------------------------------------------------------------------------
                List<PriceBookLevelRef> _PbLevel_All = new List<PriceBookLevelRef>();
                foreach (PriceBookLevelRef pbl in _PbLevel)
                {
                    var _duplicate = from _dup in _PbLevel_All
                                     where _dup.Sapl_pb == DropDownListPriceBook.SelectedValue.ToString() && _dup.Sapl_pb_lvl_cd == pbl.Sapl_pb_lvl_cd
                                     select _dup;
                    if (_duplicate.Count() == 0)
                    {
                        //addList.Add(obj);
                        _PbLevel_All.Add(pbl);
                    }
                }
                // _PbLevel_All.AddRange(_PbLevel);
                //--------------------------------------------------------------------------------------------
                grvPriceLevel.DataSource = null;
                grvPriceLevel.AutoGenerateColumns = false;
                grvPriceLevel.DataSource = _PbLevel_All;//_PbLevel;
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
            grvParty.DataSource = null;
            grvParty.AutoGenerateColumns = false;
            grvParty.DataSource = _result;
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

        private void DropDownListSchemeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnClearScheme_Click(null, null);
            this.btnAddSchemes_Click(null, null);
        }

       
        private void checkBox_SCEME_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_SCEME.Checked == true)
            {
                this.btnAll_Schemes_Click(null, null);
            }
            else
            {
                this.btnNon_Schemes_Click(null, null);
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

        private void checkBox_PB_CheckedChanged(object sender, EventArgs e)
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
                            MessageBox.Show("Selected!","Select",MessageBoxButtons.OK,MessageBoxIcon.Information);
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<string> list_schems = get_selected_Schemes();
            List<string> list_hierchyTpCode = get_selected_hierchyCodes();
            List<string> list_hierchyTp = get_selected_hierchyTypes();
            List<string> list_PB = get_selected_priceBooks();
            List<string> list_PB_LVL = get_selected_priceBooksLevels();
            if (list_schems.Count < 1 || list_hierchyTp.Count < 1 || list_PB_LVL.Count < 1)
            {
                MessageBox.Show("Please select atleaset one Scheme,Business Code and Price book level to continue.", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string commit = "";//Commitment (CP - Covered cash price, CS - covered cash price and service charge, AL - any)
            string ecdBase = "";//TI - Total Interest, FI - future interest, CI interest in closing balance, FR future rental balance, CB - Closing balance
            string effAccTp = "";//Affective Account types (AR - Arrears accounts, GD - good accounts, AL - any)
            string effAccCreatDt = "";//Affective creation date type (BC - Before given date,AC - after given date, AL - any)
            bool isECD_rate = true;
            Decimal ecdRateORVal = 0;//rate or amount
            //-------
            try {
                ecdRateORVal = Convert.ToDecimal(txtECDrt_amt.Text.Trim());
            }
            catch(Exception ex){

                MessageBox.Show("Please enter a valid ECD rate/amount ", "Invalid valid ECD rate/amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //-----------------------------------
            if (ecdRateORVal < 0)
            {
                MessageBox.Show("ECD rate/amount should be greater than 0. ", "Invalid valid ECD rate/amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ecdRateORVal > 100 && rdoECDrt.Checked == true)
            {
                MessageBox.Show("ECD rate cannot be greater than 100. ", "Invalid valid ECD rate/amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //---------------------------------------
            if (TextBoxFromDate.Value.Date > TextBoxToDate.Value.Date)
            {
                MessageBox.Show("From date should be less than To date ", "Invalid dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (TextBoxFromDate.Value.Date < CHNLSVC.Security.GetServerDateTime().Date)
            {
                MessageBox.Show("From date cannot be less than current date", "Valid Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // TextBoxFromDate.Focus();
                return;
            }

            try
            {
                Convert.ToInt32(TextBoxValueFrom.Text);
                Convert.ToInt32(TextBoxValueTo.Text);
                Convert.ToInt32(txtActFrm.Text);
                Convert.ToInt32(txtActTo.Text);

            }
            catch(Exception ex){
                MessageBox.Show("Enter Valid Periods", "Invalid Periods", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToInt32(TextBoxValueFrom.Text) > Convert.ToInt32(TextBoxValueTo.Text))
            {
                MessageBox.Show("From period should be less than To period", "Invalid Periods", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //-------
            if (rdoECDrt.Checked == true)
            {
                isECD_rate = true;
            }
            else
            {
                isECD_rate = false;
            }
            //-----
            if (rdoIntrestBase_FI.Checked==true)
            {
                ecdBase = "FI";
            }
            else if (rdoIntrestBase_CI.Checked == true)
            {
                ecdBase = "CI";
            }
            else if (rdoIntrestBase_FR.Checked==true)
            {
                ecdBase = "FR";
            }
            else if (rdoIntrestBase_CB.Checked==true)
            {
                ecdBase = "CB";
            }
            //----------

            if (rdoEAT_AR.Checked == true)
            {
                effAccTp = "AR";
            }
            else if (rdoEAT_GD.Checked == true)
            {
                effAccTp = "GD";
            }
            else if (rdoEAT_AL.Checked == true)
            {
                effAccTp = "AL";
            }
           //------------
            if (rdoECreDt_BC.Checked == true)
            {
                effAccCreatDt = "BC";
            }
            else if (rdoECreDt_AC.Checked == true)
            {
                effAccCreatDt = "AC";
            }
            else if (rdoECreDt_AL.Checked == true)
            {
                effAccCreatDt = "AL";
            }

            //------------
            if (rdoECDrstr_CP.Checked == true)
            {
                commit = "CP";
            }
            else if (rdoECDrstr_CS.Checked == true)
            {
                commit = "CS";
            }
            else if (rdoECDrstr_AL.Checked == true)
            {
                commit = "AL";
            }

            EarlyClosingDiscount ECD = new EarlyClosingDiscount();
            //ECD.Hed_acc_no;
            ECD.Hed_comit = commit;
            ECD.Hed_cre_by=BaseCls.GlbUserID;
            ECD.Hed_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;
            ECD.Hed_ecd_base = ecdBase;
            //ECD.Hed_ecd_cls_val= ;
            ECD.Hed_ecd_is_rt = isECD_rate;
            //ECD.Hed_ecd_val =;
            ECD.Hed_eff_acc_tp = effAccTp;
            ECD.Hed_eff_cre_dt = effAccCreatDt;
            ECD.Hed_eff_dt = date_effCreDt.Value.Date;
            ECD.Hed_from_dt = TextBoxFromDate.Value.Date;
            ECD.Hed_from_pd = Convert.ToInt32(TextBoxValueFrom.Text);
            //kapila 29/9/2015
            ECD.HED_NOOF_AC_FRM = Convert.ToInt32(txtActFrm.Text);
            ECD.HED_NOOF_AC_TO = Convert.ToInt32(txtActTo.Text);
            //ECD.Hed_is_prt;
            //ECD.Hed_is_use;
            // ECD.Hed_pb= DropDownListPriceBook.SelectedValue.ToString();   //ASSIGNED LATER IN SERVICE
            //ECD.Hed_pb_lvl; ASSIGNED LATER IN SERVICE

            //ECD.Hed_prt_by;
            //ECD.Hed_prt_dt;
            //ECD.Hed_pty_cd; = ASSIGNED LATER IN SERVICE
            ECD.Hed_pty_tp= DropDownListPartyTypes.SelectedValue.ToString(); 
            //ECD.Hed_sch_cd;  ASSIGNED LATER IN SERVICE
            //ECD.Hed_seq;
            ECD.Hed_to_dt = TextBoxToDate.Value.Date;
            ECD.Hed_to_pd = Convert.ToInt32(TextBoxValueTo.Text);
            ECD.Hed_tp= "N";
            //ECD.Hed_use_dt;
            ECD.Hed_val = ecdRateORVal;
            //ECD.Hed_vou_no;

            //kapila 10/3/2015
            if (optFlat.Checked == true)
                ECD.HED_IS_REDUCE_BAL = 0;
            else
                ECD.HED_IS_REDUCE_BAL = 1;

            if (MessageBox.Show("Are you sure to save ?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            Int32 eff = CHNLSVC.Sales.Save_ECD_definition(list_hierchyTp, list_schems,list_PB, list_PB_LVL, ECD);

            this.Cursor = Cursors.Default;

            if (eff > 0)
            {
                MessageBox.Show("Sucessfully Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.btnClear_Click(null, null);
            }
            else
            {
                MessageBox.Show("Not Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtECDrt_amt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxFromDate.Focus();
            }
        }

        private void txtECDrt_amt_Leave(object sender, EventArgs e)
        {
            if (rdoECDrt.Checked==true)
            {
                if (Convert.ToDecimal(txtECDrt_amt.Text.Trim()) > 100)
                {
                    MessageBox.Show("Invalid rate.\n(Should be less than or equal to 100)", "Invalid rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtECDrt_amt.Focus();
                    return;
                }
                if (Convert.ToDecimal(txtECDrt_amt.Text.Trim()) < 0)
                {
                    MessageBox.Show("Invalid rate.\n(Cannot be minus)", "Invalid rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtECDrt_amt.Focus();
                    return;
                }
            }           
            else
            {
                if (Convert.ToDecimal(txtECDrt_amt.Text.Trim()) < 0)
                {
                    MessageBox.Show("Invalid rate.\n(Cannot be minus)", "Invalid amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtECDrt_amt.Focus();
                    return;
                }
            }
        }

        private void rdoECDrt_CheckedChanged(object sender, EventArgs e)
        {
            txtECDrt_amt.Text="0.00";
            txtECDrt_amt.Focus();
        }
        private void rdoECDamt_CheckedChanged(object sender, EventArgs e)
        {
            txtECDrt_amt.Text = "0.00";
            txtECDrt_amt.Focus();
        }

        private void TextBoxFromDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxToDate.Focus();
            }
        }


        private void btnApply_Click(object sender, EventArgs e)
        {
            grvECD_Def.DataSource = null;
            grvECD_Def.AutoGenerateColumns = false;

            List<string> list_schems = get_selected_Schemes();
            List<string> list_hierchyTpCode = get_selected_hierchyCodes();
            List<string> list_hierchyTp = get_selected_hierchyTypes();
            List<string> list_PB = get_selected_priceBooks();
            List<string> list_PB_LVL = get_selected_priceBooksLevels();

            //Dictionary<string,string> dict_PB_LVL= get_selected_priceBooksLevels_DIC();

            if (list_schems.Count < 1 || list_hierchyTp.Count < 1 || list_PB_LVL.Count < 1)
            {
                MessageBox.Show("Please select atleaset one Scheme,Business Code and Price book level to continue.", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string commit = "";//Commitment (CP - Covered cash price, CS - covered cash price and service charge, AL - any)
            string ecdBase = "";//TI - Total Interest, FI - future interest, CI interest in closing balance, FR future rental balance, CB - Closing balance
            string effAccTp = "";//Affective Account types (AR - Arrears accounts, GD - good accounts, AL - any)
            string effAccCreatDt = "";//Affective creation date type (BC - Before given date,AC - after given date, AL - any)
            bool isECD_rate = true;
            Decimal ecdRateORVal = 0;//rate or amount
            //-------
            try
            {
                ecdRateORVal = Convert.ToDecimal(txtECDrt_amt.Text.Trim());
            }
            catch (Exception ex)
            {

                MessageBox.Show("Please enter a valid ECD rate/amount ", "Invalid valid ECD rate/amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //--------
            try
            {
                Convert.ToInt32(TextBoxValueFrom.Text);
                Convert.ToInt32(TextBoxValueTo.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter Valid Periods", "Invalid Periods", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToInt32(TextBoxValueFrom.Text) > Convert.ToInt32(TextBoxValueTo.Text))
            {
                MessageBox.Show("From period should be less than To period", "Invalid Periods", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToInt32(txtActFrm.Text) > Convert.ToInt32(txtActTo.Text))
            {
                MessageBox.Show("No of A/C From should be less than To", "Invalid Periods", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //-------
            //-------
            if (rdoECDrt.Checked == true)
            {
                isECD_rate = true;
            }
            else
            {
                isECD_rate = false;
            }
            //-----
            if (rdoIntrestBase_FI.Checked == true)
            {
                ecdBase = "FI";
            }
            else if (rdoIntrestBase_CI.Checked == true)
            {
                ecdBase = "CI";
            }
            else if (rdoIntrestBase_FR.Checked == true)
            {
                ecdBase = "FR";
            }
            else if (rdoIntrestBase_CB.Checked == true)
            {
                ecdBase = "CB";
            }
            //----------

            if (rdoEAT_AR.Checked == true)
            {
                effAccTp = "AR";
            }
            else if (rdoEAT_GD.Checked == true)
            {
                effAccTp = "GD";
            }
            else if (rdoEAT_AL.Checked == true)
            {
                effAccTp = "AL";
            }
            //------------
            if (rdoECreDt_BC.Checked == true)
            {
                effAccCreatDt = "BC";
            }
            else if (rdoECreDt_AC.Checked == true)
            {
                effAccCreatDt = "AC";
            }
            else if (rdoECreDt_AL.Checked == true)
            {
                effAccCreatDt = "AL";
            }

            //------------
            if (rdoECDrstr_CP.Checked == true)
            {
                commit = "CP";
            }
            else if (rdoECDrstr_CS.Checked == true)
            {
                commit = "CS";
            }
            else if (rdoECDrstr_AL.Checked == true)
            {
                commit = "AL";
            }
            

            List<EarlyClosingDiscount> ECD_LIST = new List<EarlyClosingDiscount>();

            this.Cursor = Cursors.WaitCursor;

            foreach (string st in list_hierchyTp)
            {
                foreach (string st1 in list_schems)
                {
                    //foreach (string pbl in list_PB_LVL) //PB LEVELS dict_PB_LVL  
                    for(int i=0 ; i<list_PB_LVL.Count; i++)
                    {                    
                  
                        EarlyClosingDiscount ECD = new EarlyClosingDiscount();
                        //ECD.Hed_acc_no;
                        ECD.Hed_comit = commit;
                        ECD.Hed_cre_by = BaseCls.GlbUserID;
                        ECD.Hed_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;
                        ECD.Hed_ecd_base = ecdBase;
                        //ECD.Hed_ecd_cls_val= ;
                        ECD.Hed_ecd_is_rt = isECD_rate;
                        //ECD.Hed_ecd_val =;
                        ECD.Hed_eff_acc_tp = effAccTp;
                        ECD.Hed_eff_cre_dt = effAccCreatDt;
                        ECD.Hed_eff_dt = date_effCreDt.Value.Date;
                        ECD.Hed_from_dt = TextBoxFromDate.Value.Date;
                        ECD.Hed_from_pd = Convert.ToInt32(TextBoxValueFrom.Text);
                        //kapila 29/9/2015
                        ECD.HED_NOOF_AC_FRM = Convert.ToInt32(txtActFrm.Text);
                        ECD.HED_NOOF_AC_TO = Convert.ToInt32(txtActTo.Text);

                        //ECD.Hed_from_pd;
                        //ECD.Hed_is_prt;
                        //ECD.Hed_is_use;
                        ECD.Hed_pb = list_PB[i].ToString(); //DropDownListPriceBook.SelectedValue.ToString();**
                        //ECD.Hed_pb_lvl; ASSIGNED LATER IN SERVICE
                        //ECD.Hed_prt_by;
                        //ECD.Hed_prt_dt;
                        //ECD.Hed_pty_cd; = ASSIGNED LATER IN SERVICE
                        ECD.Hed_pty_tp = DropDownListPartyTypes.SelectedValue.ToString();
                        //ECD.Hed_sch_cd;  ASSIGNED LATER IN SERVICE
                        //ECD.Hed_seq;
                        ECD.Hed_to_dt = TextBoxToDate.Value.Date;
                        ECD.Hed_to_pd = Convert.ToInt32(TextBoxValueTo.Text);
                        //ECD.Hed_to_pd;
                        ECD.Hed_tp = "N";
                        //ECD.Hed_use_dt;
                        ECD.Hed_val = ecdRateORVal;
                        //ECD.Hed_vou_no;
                        //----------------------------------------
                        ECD.Hed_pty_cd = st;
                        ECD.Hed_sch_cd = st1;
                        ECD.Hed_pb_lvl = list_PB_LVL[i].ToString();//pbl
                        //kapila 10/3/2015
                        if (optFlat.Checked == true)
                            ECD.HED_IS_REDUCE_BAL = 0;
                        else
                            ECD.HED_IS_REDUCE_BAL = 1;
                        ECD_LIST.Add(ECD);                                               
                    }
                }
            }

            grvECD_Def.DataSource = null;
            grvECD_Def.AutoGenerateColumns = false;
            grvECD_Def.DataSource = ECD_LIST;

            foreach (DataGridViewRow dgvr in grvECD_Def.Rows)
            {
                if (dgvr.Cells["Hed_ecd_is_rt"].Value.ToString().ToUpper() == "TRUE")
                {
                    //dgvr.Cells["hpi_ins_val"].Value.ToString() + "%";
                   // dgvr.Cells["Hed_val2"].Value = Convert.ToDecimal(0);
                    //ecdValue
                    dgvr.Cells["ecdRate"].Value = dgvr.Cells["Hed_val"].Value.ToString() + "%";
                    dgvr.Cells["ecdValue"].Value = string.Format("{0:n2}", 0); 
                }
                else 
                {
                   // dgvr.Cells["Hed_val"].Value = Convert.ToDecimal(0);
                    dgvr.Cells["ecdValue"].Value = string.Format("{0:n2}", dgvr.Cells["Hed_val"].Value);//dgvr.Cells["Hed_val"].Value.ToString();
                    dgvr.Cells["ecdRate"].Value = 0.00;
                                   
                }              
            }
            this.Cursor = Cursors.Default;
        }

        private void TextBoxFromDate_Leave(object sender, EventArgs e)
        {
            if (TextBoxToDate.Value < TextBoxFromDate.Value)
            {
                MessageBox.Show("To date should be greater than From date'", "Valid Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // TextBoxFromDate.Focus();
            }
            if (TextBoxFromDate.Value.Date < CHNLSVC.Security.GetServerDateTime().Date)
            {
                MessageBox.Show("From date cannot be less than current date", "Valid Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // TextBoxFromDate.Focus();
            }
        }

        private void TextBoxToDate_Leave(object sender, EventArgs e)
        {
            if (TextBoxToDate.Value < TextBoxFromDate.Value)
            {
                MessageBox.Show("To date should be greater than From date'", "Valid Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // TextBoxFromDate.Focus();
            }
        }

        private void btnClearGrid_Click(object sender, EventArgs e)
        {
            grvECD_Def.DataSource = null;
            grvECD_Def.AutoGenerateColumns = false;
        }


    }
}
