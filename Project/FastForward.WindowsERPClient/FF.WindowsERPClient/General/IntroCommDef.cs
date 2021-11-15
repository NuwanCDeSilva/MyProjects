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

namespace FF.WindowsERPClient.General
{
    public partial class IntroCommDef : Base
    {
        DataTable PCList = null;

        List<PromotComItem> _itemList;
        List<PromotComSch> _schList;
        List<PromotComSaleTp> _saleTpList;
        List<PromotComParty> _partyList;
        List<PromotComDet> _detList;

        Int32 _seqNo = 0;
        Boolean _isUpdate = false;


        public IntroCommDef()
        {
            InitializeComponent();
            LoadSchemeCategory();
            LoadSchemeType();
            BindPartyType();
            BindSalesTypes();

            _itemList = new List<PromotComItem>();
            _schList = new List<PromotComSch>();
            _partyList = new List<PromotComParty>();
            _detList = new List<PromotComDet>();

        }

        private void BindSalesTypes()
        {
            //  grvPayTp.DataSource = CHNLSVC.Financial.GetAllMainInvType();
        }
        public void BindPartyType()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            PartyTypes.Add("COM", "Company");
            PartyTypes.Add("SCHNL", "Sub Channel");
            PartyTypes.Add("PC", "PC");

            DropDownListPartyTypes.DataSource = new BindingSource(PartyTypes, null);
            DropDownListPartyTypes.DisplayMember = "Value";
            DropDownListPartyTypes.ValueMember = "Key";
        }

        private void LoadSchemeCategory()
        {
            DataTable dt = CHNLSVC.Sales.GetSAllchemeCategoryies("%");
            DropDownListSchemeCategory.DataSource = dt;
            DropDownListSchemeCategory.DisplayMember = "HSC_DESC";
            DropDownListSchemeCategory.ValueMember = "HSC_CD";
            DropDownListSchemeCategory.SelectedIndex = -1;
        }

        private void LoadSchemeType()
        {
            if (DropDownListSchemeCategory.SelectedIndex != -1)
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

                chkAllSchm.Checked = false;
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {

                        paramsText.Append(txtCate1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator);
                        break;
                    }
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
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Promot_Comm:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }




        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear_Data();
        }
        private void Clear_Data()
        {

            IntroCommDef formnew = new IntroCommDef();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

        private void DropDownListSchemeCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSchemeType();
        }

        private void chkAllSchm_CheckedChanged(object sender, EventArgs e)
        {
            DropDownListSchemeType.SelectedIndex = -1;
        }

        private void btnAddSchemes_Click(object sender, EventArgs e)
        {
            if (DropDownListSchemeCategory.SelectedIndex == -1)
            {
                return;
            }
            if (chkAllSchm.Checked == true)
            {
                DropDownListSchemeType.SelectedIndex = -1;
            }
            if (chkAllSchm.Checked == true)
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
                    grvSchemes.DataSource = null;
                    grvSchemes.AutoGenerateColumns = false;

                    return;
                }
                DataTable datasource = CHNLSVC.Sales.GetSchemes("TYPE", DropDownListSchemeType.SelectedValue.ToString());
                //  LoadList(ListBoxSchemes, datasource, "HSD_CD", "HSD_DESC");
                chkAllSchm.Checked = false;

                grvSchemes.DataSource = null;
                grvSchemes.AutoGenerateColumns = false;
                grvSchemes.DataSource = datasource;
            }
        }

        private void checkBox_SCHEM_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_SCHEM.Checked == true)
            {
                this.btnAll_Schemes_Click(null, null);
            }
            else
            {
                this.btnNon_Schemes_Click(null, null);
            }
        }

        private void btnClearHirchy_Click(object sender, EventArgs e)
        {
            PCList = new DataTable();
            grvParty.AutoGenerateColumns = false;
            grvParty.DataSource = null;

        }

        private void btnClearScheme_Click(object sender, EventArgs e)
        {
            checkBox_SCHEM.Checked = false;
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

        private void btnAddCat_Click(object sender, EventArgs e)
        {
            try
            {
                List<PromotComItem> Item_List = new List<PromotComItem>();
                if (cmbSelectCat.SelectedItem == null || cmbSelectCat.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("Please select Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE1" || cmbSelectCat.SelectedItem.ToString() == "BRAND_CATE2")
                {
                    if (txtBrand.Text == string.Empty)
                    {
                        MessageBox.Show("Specify brand also!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                string selection = "";
                if (cmbSelectCat.SelectedItem.ToString() == "MAIN CATEGORY")
                {
                    selection = "CATE1";
                }
                else if (cmbSelectCat.SelectedItem.ToString() == "CATEGORY")
                {
                    selection = "CATE2";
                }
                else if (cmbSelectCat.SelectedItem.ToString() == "BRAND & M.CATEGORY")
                {
                    selection = "BRAND_CATE1";
                }
                else if (cmbSelectCat.SelectedItem.ToString() == "BRAND & CATEGORY")
                {
                    selection = "BRAND_CATE2";
                }
                else if (cmbSelectCat.SelectedItem.ToString() == "MODEL")
                {
                    selection = "MODEL";
                }
                else
                {
                    selection = cmbSelectCat.SelectedItem.ToString();
                }

                List<PromotComItem> addList = new List<PromotComItem>();

                if (selection != "PROMOTIONL DISCOUNT")
                {



                    DataTable dt = CHNLSVC.Sales.GetBrandsCatsItems(selection, txtBrand.Text.Trim(), txtCate1.Text.Trim(), txtCate2.Text.Trim(), txtModel.Text, txtItemCD.Text.Trim(), "", "", "");


                    //if (dt.Rows.Count > 0)
                    //{
                    //    dataGridViewItem.Columns[1].HeaderText = cmbSelectCat.SelectedItem.ToString();
                    //}

                    foreach (DataRow dr in dt.Rows)
                    {
                        string code = dr["code"].ToString();
                        string brand = txtBrand.Text;
                        PromotComItem obj = new PromotComItem(); //for display purpose
                        //if (selection == "BRAND_CATE1" || selection == "BRAND_CATE2")
                        //{
                        //    obj.Sccd_brd = brand;
                        //}
                        //else
                        //{
                        //    obj.Sccd_brd = "N/A";
                        //}
                        obj.Hpci_tp = cmbSelectCat.SelectedItem.ToString();
                        obj.Hpci_cd = code;
                        obj.Hpci_brnd = dr["brand"].ToString();
                        //try
                        //{
                        //    obj.Sccd_ser = dr["descript"].ToString();
                        //}
                        //catch (Exception)
                        //{
                        //    obj.Sccd_ser = "";
                        //}

                        var _duplicate = from _dup in Item_List
                                         where _dup.Hpci_cd == obj.Hpci_cd && _dup.Hpci_tp == obj.Hpci_tp
                                         select _dup;
                        if (_duplicate.Count() == 0)
                        {
                            addList.Add(obj);
                        }

                    }
                    //if (dt.Rows.Count > 0)
                    //{
                    //    dataGridViewItem.Columns[2].HeaderText = cmbSelectCat.SelectedItem.ToString();
                    //}
                }
                //PROMOTIONL DISCOUNT
                else
                {

                }
                Item_List.AddRange(addList);
                BindingSource source = new BindingSource();
                source.DataSource = Item_List;
                dataGridViewItem.AutoGenerateColumns = false;
                dataGridViewItem.DataSource = source;

                foreach (DataGridViewRow gr in dataGridViewItem.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridViewItem.Rows[gr.Index].Cells[0];
                    chk.Value = "true";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void cmbSelectCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtItemCD.Text = "";
            txtCate1.Text = "";
            txtCate2.Text = "";

            dataGridViewItem.DataSource = null;
            _itemList = new List<PromotComItem>();
        }

        private void btnAddPartys_Click(object sender, EventArgs e)
        {
            try
            {
                Base _basePage = new Base();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();


                DataTable dt = new DataTable();
                DataTable _result = new DataTable();
                if (DropDownListPartyTypes.SelectedValue.ToString() == "COM")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                    }
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "CHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                    }
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "SCHNL")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                    }
                }

                else if (DropDownListPartyTypes.SelectedValue.ToString() == "AREA")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                    }
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "REGION")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                    }
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "ZONE")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (txtHierchCode.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                    }
                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "PC")
                {
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                    if (txtHierchCode.Text != "")
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                    }

                }
                else if (DropDownListPartyTypes.SelectedValue.ToString() == "GPC")
                {
                    _result = CHNLSVC.General.Get_GET_GPC("", "");
                    if (txtHierchCode.Text != "")
                    {
                        _result = _basePage.CHNLSVC.General.Get_GET_GPC(txtHierchCode.Text, "");
                    }
                }
                if (_result == null || _result.Rows.Count <= 0)
                {
                    MessageBox.Show("Invalid search term, no result found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                foreach (DataGridViewRow row in grvParty.Rows)
                {
                    if (row.Cells[1].Value.ToString().Equals(txtHierchCode.Text))
                    {
                        MessageBox.Show("Already Added", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                PCList.Merge(_result);

                BindingSource _source = new BindingSource();
                _source.DataSource = PCList;

                grvParty.DataSource = null;
                grvParty.AutoGenerateColumns = false;
                grvParty.DataSource = _source;
                // chkPCAll.Checked = true;
                //AllClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnHierachySearch_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCirc.Text == "")
            {
                MessageBox.Show("Please enter Circular code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCirc.Focus();
                return;
            }

            if (TextBoxFromDate.Value.Date > TextBoxToDate.Value.Date)
            {
                MessageBox.Show("Invalid date range", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToDateTime(TextBoxToDate.Value.Date) < DateTime.Now.Date)
            {
                MessageBox.Show("Invalid to date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if (grvDet.Rows.Count == 0)
            //{
            //    MessageBox.Show("Please enter commission details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            if (grvParty.Rows.Count == 0)
            {
                MessageBox.Show("Please select party", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (grvSchemes.Rows.Count == 0)
            {
                MessageBox.Show("Please select schemes", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dataGridViewItem.Rows.Count == 0)
            {
                MessageBox.Show("Please select items", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (MessageBox.Show("Are you sure ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            string _appPrd = "";
            Int32 _line = 1;
            PromotComHdr _comHdr = new PromotComHdr();
            _comHdr.Hpch_act = true;

            if (cmbAppParty.Text == "Daily") _appPrd = "D";
            if (cmbAppParty.Text == "Weekly") _appPrd = "W";
            if (cmbAppParty.Text == "Monthly") _appPrd = "M";
            if (cmbAppParty.Text == "Yearly") _appPrd = "Y";

            _comHdr.Hpch_app = _appPrd;
            _comHdr.Hpch_circular = txtCirc.Text;
            _comHdr.Hpch_com = BaseCls.GlbUserComCode;
            //  _comHdr.Hpch_com_amt = Convert.ToDecimal(TextBoxInsuAmt.Text);
            //  _comHdr.Hpch_com_rt = Convert.ToDecimal(TextBoxInsuRate.Text);
            _comHdr.Hpch_cre_by = BaseCls.GlbUserID;
            _comHdr.Hpch_from_dt = Convert.ToDateTime(TextBoxFromDate.Text);
            _comHdr.Hpch_mod_by = BaseCls.GlbUserID;
            _comHdr.Hpch_to_dt = Convert.ToDateTime(TextBoxToDate.Text);

            //_schList = new List<PromotComSch>();
            //foreach (DataGridViewRow row in grvSchDet.Rows)
            //{




            //}

            _line = 1;
            _partyList = new List<PromotComParty>();
            foreach (DataGridViewRow row in grvParty.Rows)
            {
                PromotComParty _objPart = new PromotComParty();
                _objPart.Hpcp_com = BaseCls.GlbUserComCode;
                _objPart.Hpcp_line = _line;
                _objPart.Hpcp_pty_cd = row.Cells["party_code"].Value.ToString();
                _objPart.Hpcp_pty_tp = DropDownListPartyTypes.SelectedValue.ToString();
                _partyList.Add(_objPart);
                _line++;

            }


            _line = 1;
            _itemList = new List<PromotComItem>();
            foreach (DataGridViewRow gvr in dataGridViewItem.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gvr.Cells[0];
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    PromotComItem _objItm = new PromotComItem();
                    _objItm.Hpci_cd = gvr.Cells["HPCI_CD"].Value.ToString();
                    _objItm.Hpci_line = _line;
                    _objItm.Hpci_tp = gvr.Cells["HPCI_TP"].Value.ToString();
                    _objItm.Hpci_brnd = gvr.Cells["HPCI_BRND"].Value.ToString();
                    _itemList.Add(_objItm);
                    _line++;
                }

            }

            Int32 _eff = CHNLSVC.Financial.SavePromotCommDefi(_comHdr, _itemList, _schList, _partyList, null);
            if (_eff == 1)
            {
                MessageBox.Show("Successfully updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                IntroCommDef formnew = new IntroCommDef();
                formnew.MdiParent = this.MdiParent;
                formnew.Location = this.Location;
                formnew.Show();
                this.Close();
            }
            else
                MessageBox.Show("Not Updated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void DropDownListPartyTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            PCList = new DataTable();
        }

        private void btnBrand_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBrand;
                _CommonSearch.txtSearchbyword.Text = txtBrand.Text;
                _CommonSearch.ShowDialog();
                txtBrand.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnMainCat_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCate1;
                _CommonSearch.txtSearchbyword.Text = txtCate1.Text;
                _CommonSearch.ShowDialog();
                txtCate1.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnCat_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCate2;
                _CommonSearch.txtSearchbyword.Text = txtCate2.Text;
                _CommonSearch.ShowDialog();
                txtCate2.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemCD;
                _CommonSearch.txtSearchbyword.Text = txtItemCD.Text;
                _CommonSearch.ShowDialog();
                txtItemCD.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void TextBoxInsuRate_Leave(object sender, EventArgs e)
        {

        }

        private void TextBoxInsuAmt_Leave(object sender, EventArgs e)
        {

        }

        private void btn_srch_model_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                DataTable _result = CHNLSVC.CommonSearch.GetAllModels(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtModel; //txtBox;
                _CommonSearch.ShowDialog();
                txtModel.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSrchCirc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promot_Comm);
                DataTable _result = CHNLSVC.CommonSearch.SearchPromoCommDefData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCirc;
                _CommonSearch.txtSearchbyword.Text = txtCirc.Text;
                _CommonSearch.ShowDialog();
                txtCirc.Focus();

                load_details(txtCirc.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void load_details(string _circ)
        {
            // grvPayTp.Enabled = true;
            panel8.Enabled = true;
            pnlSch.Enabled = true;
            btnAddCat.Enabled = true;
            pnlComm.Enabled = true;
            TextBoxFromDate.Enabled = true;
            btnAddPartys.Enabled = true;
            dataGridViewItem.Enabled = true;
            cmbSelectCat.Enabled = true;
            _isUpdate = false;
            _seqNo = 0;
            btnUpd.Enabled = false;
            btnSave.Enabled = true;

            if (!string.IsNullOrEmpty(txtCirc.Text))
            {
                DataTable _dtHdr = CHNLSVC.Financial.GetPromotCommHdr(BaseCls.GlbUserComCode, txtCirc.Text);
                if (_dtHdr.Rows.Count > 0)
                {
                    _isUpdate = true;
                    _seqNo = Convert.ToInt32(_dtHdr.Rows[0]["hpch_seq"]);
                    DataTable _dtItm = CHNLSVC.Financial.GetPromotCommDet(_seqNo);
                    DataTable _dtSch = CHNLSVC.Financial.GetPromotCommSch(_seqNo);
                    DataTable _dtPrty = CHNLSVC.Financial.GetPromotCommParty(_seqNo);
                    //  DataTable _dtSaleTp = CHNLSVC.Financial.GetPromotCommSaleTp(_seqNo);
                    //DataTable _dtDet = CHNLSVC.Financial.GetPromotCommDetails(_seqNo);

                    //grvDet.AutoGenerateColumns = false;
                    //grvDet.DataSource = _dtDet;

                    grvParty.AutoGenerateColumns = false;
                    grvParty.DataSource = _dtPrty;

                    dataGridViewItem.AutoGenerateColumns = false;
                    dataGridViewItem.DataSource = _dtItm;

                    grvSchDet.AutoGenerateColumns = false;
                    grvSchDet.DataSource = _dtSch;

                    TextBoxFromDate.Value = Convert.ToDateTime(_dtHdr.Rows[0]["HPCH_FROM_DT"]);
                    TextBoxToDate.Value = Convert.ToDateTime(_dtHdr.Rows[0]["HPCH_TO_DT"]);

                    if (_dtHdr.Rows[0]["HPCH_APP"].ToString() == "D") cmbAppParty.SelectedIndex = 0;
                    if (_dtHdr.Rows[0]["HPCH_APP"].ToString() == "W") cmbAppParty.SelectedIndex = 1;
                    if (_dtHdr.Rows[0]["HPCH_APP"].ToString() == "M") cmbAppParty.SelectedIndex = 2;
                    if (_dtHdr.Rows[0]["HPCH_APP"].ToString() == "Y") cmbAppParty.SelectedIndex = 3;


                    //   grvPayTp.Enabled = false;
                    panel8.Enabled = false;
                    pnlSch.Enabled = false;
                    btnAddCat.Enabled = false;
                    pnlComm.Enabled = false;
                    TextBoxFromDate.Enabled = false;
                    btnAddPartys.Enabled = false;
                    dataGridViewItem.Enabled = false;
                    cmbSelectCat.Enabled = false;

                    btnUpd.Enabled = true;
                    btnSave.Enabled = false;
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
            MasterCompany _masterComp = new MasterCompany();
            _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
            if (_masterComp != null)
            {
                BaseCls.GlbReportComp = _masterComp.Mc_desc;
            }
            _view.GlbReportName = "IntroComm.rpt";
            BaseCls.GlbReportName = "IntroComm.rpt";
            BaseCls.GlbReportDoc = txtCirc.Text;
            BaseCls.GlbReportParaLine1 = _seqNo;
            _view.Show();
            _view = null;
        }

        private void txtCirc_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCirc.Text))
            {
                load_details(txtCirc.Text);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAddCom_Click(object sender, EventArgs e)
        {


            PromotComDet _objDet = new PromotComDet();
            _objDet.Hpcd_com_amt = Convert.ToDecimal(TextBoxInsuAmt.Text);
            _objDet.Hpcd_com_rt = Convert.ToDecimal(TextBoxInsuRate.Text);
            _objDet.Hpcd_from_qty = Convert.ToInt32(txtFromQty.Text);
            _objDet.Hpcd_to_qty = Convert.ToInt32(txtToQty.Text);
            _detList.Add(_objDet);


            grvDet.DataSource = null;
            grvDet.AutoGenerateColumns = false;
            grvDet.DataSource = _detList;
        }

        private void txtFromQty_Leave(object sender, EventArgs e)
        {

        }

        private void txtToQty_Leave(object sender, EventArgs e)
        {

        }

        private void txtCirc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCirc_Leave(null, null);
        }

        private void btnItemClear_Click(object sender, EventArgs e)
        {
            dataGridViewItem.AutoGenerateColumns = false;
            dataGridViewItem.DataSource = null;
        }

        private void btnItemNone_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridViewItem.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                dataGridViewItem.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void grvDet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _detList.RemoveAt(e.RowIndex);
                    grvDet.DataSource = _detList;
                    grvDet.AutoGenerateColumns = false;
                }
            }
        }

        private void btnUpd_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            int _eff = CHNLSVC.Financial.UpdatePromotCommHdr(txtCirc.Text, Convert.ToDateTime(TextBoxToDate.Value), BaseCls.GlbUserID);

            if (_eff > 0)
            {
                MessageBox.Show("Successfully updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Not Updated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            IntroCommDef formnew = new IntroCommDef();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

        private void btnAddSch_Click(object sender, EventArgs e)
        {
            if (IsNumeric(txtFromQty.Text) == false)
            {
                MessageBox.Show("Please select the valid from qty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtFromQty.Text = "0";
                txtFromQty.Focus();
                return;
            }
            if (IsNumeric(txtToQty.Text) == false)
            {
                MessageBox.Show("Please select the valid to qty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtToQty.Text = "0";
                txtToQty.Focus();
                return;
            }
            if (IsNumeric(TextBoxInsuAmt.Text) == false)
            {
                MessageBox.Show("Please select the valid amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TextBoxInsuAmt.Text = "0";
                TextBoxInsuAmt.Focus();
                return;
            }
            if (IsNumeric(TextBoxInsuRate.Text) == false)
            {
                MessageBox.Show("Please select the valid rate", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                TextBoxInsuRate.Text = "0";
                TextBoxInsuRate.Focus();
                return;
            }
            if (string.IsNullOrEmpty(TextBoxInsuRate.Text))
            {
                MessageBox.Show("Invalid commission rate", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxInsuRate.Text = "0";
                TextBoxInsuRate.Focus();
                return;
            }
            if (string.IsNullOrEmpty(TextBoxInsuAmt.Text))
            {
                MessageBox.Show("Invalid commission amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxInsuAmt.Text = "0";
                TextBoxInsuAmt.Focus();
                return;
            }
            if (Convert.ToDecimal(TextBoxInsuRate.Text) > 100)
            {
                MessageBox.Show("Invalid commission rate", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxInsuRate.Text = "0";
                TextBoxInsuRate.Focus();
                return;
            }
            if (Convert.ToDecimal(TextBoxInsuRate.Text) < 0)
            {
                MessageBox.Show("Invalid commission rate", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxInsuRate.Text = "0";
                TextBoxInsuRate.Focus();
                return;
            }
            if (Convert.ToDecimal(TextBoxInsuAmt.Text) < 0)
            {
                MessageBox.Show("Invalid commission amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxInsuAmt.Text = "0";
                TextBoxInsuAmt.Focus();
                return;
            }
            if (Convert.ToDecimal(txtFromQty.Text) < 0)
            {
                MessageBox.Show("Invalid from quantity", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFromQty.Text = "0";
                txtFromQty.Focus();
                return;
            }
            if (Convert.ToDecimal(txtToQty.Text) < 0)
            {
                MessageBox.Show("Invalid to quantity", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtToQty.Text = "0";
                txtToQty.Focus();
                return;
            }
            if (Convert.ToInt32(txtFromQty.Text) > Convert.ToInt32(txtToQty.Text))
            {
                MessageBox.Show("From qty. cannot exceed To qty. Please enter correct definitions", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFromQty.Text = "0";
                txtFromQty.Focus();
                return;
            }

            foreach (DataGridViewRow row1 in grvSchemes.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row1.Cells[0];
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    foreach (DataGridViewRow row in grvSchDet.Rows)
                    {
                        if (row.Cells[1].Value.ToString().Equals(row1.Cells["HSC_CD"].Value.ToString()) && row.Cells[2].Value.ToString().Equals(txtFromQty.Text))
                        {
                            MessageBox.Show("Already added the scheme " + row1.Cells["HSC_CD"].Value.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
            }

            int _line = grvSchDet.Rows.Count + 1;
            foreach (DataGridViewRow row in grvSchemes.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    PromotComSch _objSch = new PromotComSch();
                    _objSch.Hpcs_line = _line;
                    _objSch.Hpcs_sch_cd = row.Cells["HSC_CD"].Value.ToString();
                    _objSch.Hpcs_com_amt = Convert.ToDecimal(TextBoxInsuAmt.Text);
                    _objSch.Hpcs_com_rt = Convert.ToDecimal(TextBoxInsuRate.Text);
                    _objSch.Hpcs_from_qty = Convert.ToInt32(txtFromQty.Text);
                    _objSch.Hpcs_to_qty = Convert.ToInt32(txtToQty.Text);
                    _schList.Add(_objSch);
                    _line++;

                }
            }
            grvSchDet.AutoGenerateColumns = false;
            grvSchDet.DataSource = new List<PromotComSch>();
            grvSchDet.DataSource = _schList;
        }

        private void grvSchDet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _schList.RemoveAt(e.RowIndex);
                    grvSchDet.DataSource = new List<PromotComSch>();
                    grvSchDet.DataSource = _schList;
                    grvSchDet.AutoGenerateColumns = false;
                }
            }
        }

        private void IntroCommDef_KeyPress(object sender, KeyPressEventArgs e)
        {
        
        }

        private void IntroCommDef_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
                SendKeys.Send("{TAB}");

        }
    }
}

