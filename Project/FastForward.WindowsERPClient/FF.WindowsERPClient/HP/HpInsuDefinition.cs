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
    public partial class HpInsuDefinition : FF.WindowsERPClient.Base
    {
        //seq_hpr_insu   =NEW SEQUENCE
        //sp_save_hp_insu_sch =UPDATE
        //sp_getschmecat  =UPDATE
        //sp_getscheme_type_by_cat =update
        public HpInsuDefinition()
        {
            InitializeComponent();
           // LoadSchemeType();
            LoadSchemeCategory();
            bind_Combo_DropDownListPartyTypes();
            bind_Combo_DropDownListCalculateOnType();
            bind_Combo_DropDownListCheckOn();
            TextBoxToDate.Value = Convert.ToDateTime("31-Dec-2999").Date;
            //99999999  TextBoxValueTo
            TextBoxValueTo.Text = string.Format("{0:n2}", 99999999);
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
        private void LoadSchemeCategory()
        {
            DataTable dt = CHNLSVC.Sales.GetSAllchemeCategoryies("%");
            DropDownListSchemeCategory.DataSource = dt;
            DropDownListSchemeCategory.DisplayMember = "HSC_DESC";
            DropDownListSchemeCategory.ValueMember = "HSC_CD";
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

            chkAllSchm.Checked = false;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            HpInsuDefinition formnew = new HpInsuDefinition();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

        private void DropDownListSchemeCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnClearScheme_Click(null, null);
            LoadSchemeType();
        }

        private void btnClearScheme_Click(object sender, EventArgs e)
        {
            checkBox_SCHEM.Checked = false;
            grvSchemes.DataSource = null;
            grvSchemes.AutoGenerateColumns = false;
        }

        private void DropDownListSchemeType_SelectedIndexChanged(object sender, EventArgs e)
        {
           // this.btnClearScheme_Click(null, null);
            this.btnAddSchemes_Click(null, null);
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

        private void btnClearHirchy_Click(object sender, EventArgs e)
        {
            checkBox_HIERACHY.Checked = false;
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
            
            if (_result==null)
            {
                //MessageBox.Show("No data found!", "Data not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
             if (_result.Rows.Count==0)
            {
               // MessageBox.Show("No data found!","Data not found",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
        }
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
            List<string> _schemlist = get_selected_Schemes();
            if (_schemlist.Count < 1)
            {
                MessageBox.Show("Please select scheme(s)!", "Schemes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> _hiecList = get_selected_hierchyTypes();
            if (_hiecList.Count < 1)
            {
                MessageBox.Show("Please select business hierarchy code(s)!", "Hierarchy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                TextBoxValueFrom.Focus();
                Convert.ToDecimal(TextBoxValueFrom.Text.Trim());

                TextBoxValueTo.Focus();
                Convert.ToDecimal(TextBoxValueTo.Text.Trim());
               
            }
            catch(Exception EX){
                MessageBox.Show("Please enter valid 'From value' and 'To value'!", "Values", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //-------------------------
            try
            {
                TextBoxInsuRate.Focus();
                Convert.ToDecimal(TextBoxInsuRate.Text.Trim());

                TextBoxInsuAmt.Focus();
                Convert.ToDecimal(TextBoxInsuAmt.Text.Trim());

            }
            catch (Exception EX)
            {
                MessageBox.Show("Please enter valid 'Insurance rate/amount'!", "Insurance rate/amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //-------------------------
            try
            {
                TextBoxComRate.Focus();
                Convert.ToDecimal(TextBoxComRate.Text.Trim());

                TextBoxComValue.Focus();
                Convert.ToDecimal(TextBoxComValue.Text.Trim());

            }
            catch (Exception EX)
            {
                MessageBox.Show("Please enter valid 'Commission rate/amount'!", "Commission rate/amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                TextBoxVat.Focus();
                Convert.ToDecimal(TextBoxVat.Text.Trim());              

            }
            catch (Exception EX)
            {
                MessageBox.Show("Please enter valid 'Vat Rate'!", "Vat Rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //****************************************************************************************************************
            //#region validation

            //if (GridViewScheme.Rows.Count <= 0)
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please add scheme codes");
            //    return;
            //}
            //if (GridViewPC.Rows.Count <= 0)
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please add profit centers");
            //    return;
            //}
            //if (TextBoxFromDate.Text == "")
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select from date");
            //    return;
            //}
            //if (TextBoxTodate.Text == "")
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select to date");
            //    return;
            //}
            if (Convert.ToDateTime(TextBoxFromDate.Text) > Convert.ToDateTime(TextBoxToDate.Text))
            {

                //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From date has to be smaller than to date");
                MessageBox.Show("From date cannot be greater than to date!", "Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
                   
            if (Convert.ToDecimal(TextBoxValueFrom.Text.Trim()) < 0 || Convert.ToDecimal(TextBoxValueTo.Text.Trim()) < 0)
            {
                MessageBox.Show("'From' and 'To' values cannot be minus!", "Invalid values", MessageBoxButtons.OK, MessageBoxIcon.Warning);                
                return;
            }

            if (Convert.ToDecimal(TextBoxValueFrom.Text) > Convert.ToDecimal(TextBoxValueTo.Text))
            {
                //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From value has to be smaller than to value");
                MessageBox.Show("'From value' cannot be greater than 'To value'!", "Invalid values", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToDecimal(TextBoxVat.Text.Trim()) < 0)
            {
                MessageBox.Show("Vat Rate cannot be a minus value!", "Invalid rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
                             
            //#endregion

            HPInsuranceScheme _scheme = new HPInsuranceScheme();
            _scheme.Hpi_cal_on = DropDownListCalculateOnType.SelectedValue.ToString();
            _scheme.Hpi_chk_on = DropDownListCheckOn.SelectedValue.ToString();
            _scheme.Hpi_cre_by = BaseCls.GlbUserID;
            _scheme.Hpi_cre_dt = DateTime.Now;
            _scheme.Hpi_from_dt = Convert.ToDateTime(TextBoxFromDate.Text);
            _scheme.Hpi_to_dt = Convert.ToDateTime(TextBoxToDate.Text);
            _scheme.Hpi_from_val = Convert.ToDecimal(TextBoxValueFrom.Text);
            _scheme.Hpi_to_val = Convert.ToDecimal(TextBoxValueTo.Text);
            _scheme.Hpi_vat_rt = Convert.ToDecimal(TextBoxVat.Text);
            _scheme.Hpi_is_comp = chkIsComp.Checked;

            _scheme.Hpi_pty_tp = DropDownListPartyTypes.SelectedValue.ToString(); //NOT SURE
            //_scheme.Hpi_comm = Convert.ToDecimal(TextBoxCommission.Text);}
            
            //if (TextBoxComRate.Text != "")
            if (rdoInsuRate.Checked == true)
            {
                _scheme.Hpi_ins_val = Convert.ToDecimal(TextBoxInsuRate.Text);
                _scheme.Hpi_ins_isrt = true;
            }
            else
            {
                _scheme.Hpi_ins_val = Convert.ToDecimal(TextBoxInsuAmt.Text);
                _scheme.Hpi_ins_isrt = false;
            }

            if (rdoCommRate.Checked == true)
            {
                _scheme.Hpi_comm_isrt = true;
                _scheme.Hpi_comm = Convert.ToDecimal(TextBoxComRate.Text);
            }
            else
            {
                _scheme.Hpi_comm_isrt = false;
                _scheme.Hpi_comm = Convert.ToDecimal(TextBoxComValue.Text);

            }

            //List<string> _pcList = new List<string>();
            //foreach (GridViewRow gr in GridViewPC.Rows)
            //{
            //    _pcList.Add(gr.Cells[1].Text);
            //}
            List<string> _pcList = get_selected_hierchyTypes();
            //-----------------------------------------------------
            //List<string> _schList = new List<string>();
            //foreach (GridViewRow gr in GridViewScheme.Rows)
            //{
            //    _schList.Add(gr.Cells[1].Text);
            //}
            List<string> _schList = get_selected_Schemes();

            if (MessageBox.Show("Are you sure to save ?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int result = CHNLSVC.Sales.SaveHPInsurance(_pcList, _schList, _scheme);
            if (result > 0)
            {
                MessageBox.Show("Sucessfully saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //string Msgg = "<script>alert('Records Insert Sucessfully');window.location ='DiriyaDefinition.aspx'</script>";
                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);
                this.btnClear_Click(null, null);
            }
            else
            {
                MessageBox.Show("Not saved!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);            
            }
            //-----------------------------------------------------------------------------
            //if (MessageBox.Show("Do you want to clear screen?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //{
            //    return;
            //}
            //else
            //{
            //    this.btnClear_Click(null, null);
            //}
            //this.btnClear_Click(null, null);
        }

        private void rdoInsuAmt_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoInsuAmt.Checked)
            {
                TextBoxInsuRate.Enabled = false;
                TextBoxInsuAmt.Enabled = true;
                TextBoxInsuRate.Text = "0.00";
            }
            else
            {
                TextBoxInsuRate.Enabled = true;
                TextBoxInsuAmt.Enabled = false;
                TextBoxInsuRate.Text = "0.00";
            }

        }

        private void rdoInsuRate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoInsuAmt.Checked)
            {
                TextBoxInsuRate.Enabled = false;
                TextBoxInsuAmt.Enabled = true;
                TextBoxInsuRate.Text = "0.00";
                TextBoxInsuAmt.Text = "0.00";//
            }
            else
            {
                TextBoxInsuRate.Enabled = true;
                TextBoxInsuAmt.Enabled = false;
                TextBoxInsuRate.Text = "0.00";
                TextBoxInsuAmt.Text = "0.00";//
            }
        }

        private void rdoCommRate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCommRate.Checked == true)
            {
                TextBoxComRate.Enabled = true;
                TextBoxComValue.Enabled = false;
                TextBoxComValue.Text = "0.00";
                TextBoxComRate.Text = "0.00";
            }
            else if (rdoCommRate.Checked == false)
            {
                TextBoxComRate.Enabled = false;
                TextBoxComValue.Enabled = true;
                TextBoxComRate.Text = "0.00";
                TextBoxComValue.Text = "0.00";
            }
        }

        private void rdoCommAmt_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoInsuAmt.Checked == true)
            //{
            //    TextBoxComRate.Enabled = false;
            //    TextBoxComValue.Enabled = true;
            //    TextBoxComRate.Text = "0.00";
            //}
            //else if (rdoInsuAmt.Checked == false)
            //{
            //    TextBoxComRate.Enabled = true;
            //    TextBoxComValue.Enabled = false;
            //    TextBoxComRate.Text = "0.00";
            //}
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            string pty_tp = "";
            if (DropDownListPartyTypes.SelectedIndex!=-1)
            {
                pty_tp = DropDownListPartyTypes.SelectedValue.ToString();
            }
            //-------------------------------------------------------------
            DataTable dt = new DataTable();

            List<string> _list_pty_cd = get_selected_hierchyTypes();
            if (_list_pty_cd.Count > 0)
            {
                foreach (string p_cd in _list_pty_cd)
                {
                    List<string> _schList = get_selected_Schemes();

                    if (_schList.Count > 0)
                    {
                        foreach (string schemeCd in _schList)
                        {
                            DataTable dt2 = CHNLSVC.General.Get_hpr_insu(schemeCd, pty_tp, p_cd);
                            dt.Merge(dt2);
                        }
                    }
                    else
                    {
                        DataTable dt1 = CHNLSVC.General.Get_hpr_insu("", pty_tp, p_cd);
                        dt.Merge(dt1);
                    
                    }
                }
            }
            else
            {
                List<string> _schList = get_selected_Schemes();

                if (_schList.Count > 0)
                {
                    foreach (string schemeCd in _schList)
                    {
                        DataTable dt2 = CHNLSVC.General.Get_hpr_insu(schemeCd, pty_tp, "");
                        dt.Merge(dt2);
                    }
                }
                else
                {
                    DataTable dt1 = CHNLSVC.General.Get_hpr_insu("", pty_tp, "");
                    dt.Merge(dt1);
                }               
            }
            grvReceptDet.DataSource = null;
            grvReceptDet.AutoGenerateColumns = false;
            grvReceptDet.DataSource = dt;
          

            if (dt.Rows.Count==0)
            {
                MessageBox.Show("No reords found!","Data not found",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            foreach (DataGridViewRow dgvr in grvReceptDet.Rows)
            {
                if (dgvr.Cells["hpi_ins_isrt"].Value.ToString() == "1")
                {
                    
                    dgvr.Cells["InsuRt"].Value = dgvr.Cells["hpi_ins_val"].Value.ToString() + "%";
                    
                    dgvr.Cells["hpi_ins_val"].Value = 0.00;
                }
                else if (dgvr.Cells["hpi_ins_isrt"].Value.ToString() == "0")
                {

                    dgvr.Cells["InsuRt"].Value = "0" + "%";

                    //dgvr.Cells["hpi_ins_val"].Value = 0.00;
                }
                //CommRt

                if (dgvr.Cells["hpi_comm_isrt"].Value.ToString() == "1")
                {
                    dgvr.Cells["CommRt"].Value = dgvr.Cells["hpi_comm"].Value.ToString() + "%";

                    dgvr.Cells["hpi_comm"].Value = 0.00;
                }
                else if (dgvr.Cells["hpi_comm_isrt"].Value.ToString() == "0")
                {
                    dgvr.Cells["CommRt"].Value = "0" + "%";

                    //dgvr.Cells["hpi_ins_val"].Value = 0.00;
                }
               
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

        private void checkBox_HIERACHY_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_HIERACHY.Checked == true)
            {
                this.btnAll_Hirchy_Click(null, null);
            }
            else
            {
                this.btnNon_Hierachy_Click(null, null);
            }
        }

        private void chkAllSchm_CheckedChanged(object sender, EventArgs e)
        {
            
            //if (chkAllSchm.Checked == false)
            //{
            //    this.btnClearScheme_Click(null, null);
            //}
            DropDownListSchemeType.SelectedIndex = -1;
            
        }

        private void DropDownListPartyTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnClearHirchy_Click(null, null);
            this.btnAddPartys_Click(null, null);
        }

        private void TextBoxFromDate_ValueChanged(object sender, EventArgs e)
        {

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

        private void txtHierchCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtHierchCode.Focus();
                if (grvParty.Rows.Count>0)
                {
                    foreach(DataGridViewRow dgvr in grvParty.Rows)
                    {
                        if (dgvr.Cells["party_Code"].Value.ToString() == txtHierchCode.Text.Trim())
                        {
                            DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dgvr.Cells[0];
                            chk.Value = true;
                            dgvr.Selected = true;
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

        private void btnSelect_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in grvParty.Rows)
            { 
            
            }

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
        }

        private void TextBoxFromDate_Leave(object sender, EventArgs e)
        {
            if (TextBoxToDate.Value < TextBoxFromDate.Value)
            {
                MessageBox.Show("To date should be greater than From date'", "Valid Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
        }

        private void TextBoxToDate_Leave(object sender, EventArgs e)
        {
            if (TextBoxToDate.Value < TextBoxFromDate.Value)
            {
                MessageBox.Show("To date should be greater than From date'", "Valid Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
              
            }
        }

        private void TextBoxValueFrom_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(TextBoxValueFrom.Text.Trim()) < 0)
            {
                MessageBox.Show("Invalid value.\n(Cannot be minus)", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxValueFrom.Focus();
                return;
            }
            else
            {
                // TextBoxServiceChgRt.Focus();
            }
            TextBoxValueTo.Focus();
        }

        private void TextBoxValueTo_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(TextBoxValueTo.Text.Trim()) < 0)
            {
                MessageBox.Show("Invalid value.\n(Cannot be minus)", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxValueTo.Focus();
                return;
            }
            if (Convert.ToDecimal(TextBoxValueTo.Text.Trim()) < Convert.ToDecimal(TextBoxValueFrom.Text.Trim()))
            {
                MessageBox.Show("To value must be greater than From value", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxValueTo.Focus();
                return;
            }
            TextBoxInsuRate.Focus();
        }

        private void TextBoxInsuRate_Leave(object sender, EventArgs e)
        {
            //TextBoxInsuRate
            try
            {
                Convert.ToDecimal(TextBoxInsuRate.Text.Trim());
            }
            catch (Exception ex)
            {

                MessageBox.Show("Enter valid rate", "Invalid Rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxInsuRate.Text = "";
                TextBoxInsuRate.Focus();
                return;
            }
            //---------------
            if (Convert.ToDecimal(TextBoxInsuRate.Text.Trim()) > 100)
            {
                MessageBox.Show("Invalid rate.\n(Should be less than or equal to 100).", "Invalid rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxInsuRate.Focus();
                return;
            }
            if (Convert.ToDecimal(TextBoxInsuRate.Text.Trim()) < 0)
            {
                MessageBox.Show("Invalid rate.\n(Cannot be minus)", "Invalid rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxInsuRate.Focus();
                return;
            }
            TextBoxComRate.Focus();
        }

        private void TextBoxComValue_Leave(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDecimal(TextBoxComValue.Text.Trim());
            }
            catch (Exception ex)
            {

                MessageBox.Show("Enter valid amount", "Invalid amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxComValue.Text = "";
                TextBoxComValue.Focus();
                return;
            }


            if (Convert.ToDecimal(TextBoxComValue.Text.Trim()) < 0)
            {
                MessageBox.Show("Invalid amount.\n(Cannot be minus)", "Invalid amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxComValue.Focus();
                return;
            }
           // DropDownListCheckOn.Focus();
            DropDownListCheckOn.DroppedDown = true;
        }

        private void TextBoxInsuAmt_Leave(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDecimal(TextBoxInsuAmt.Text.Trim());
            }
            catch (Exception ex)
            {

                MessageBox.Show("Enter valid rate", "Invalid Rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxInsuAmt.Text = "";
                TextBoxInsuAmt.Focus();
                return;
            }
            //---------------

            if (Convert.ToDecimal(TextBoxInsuAmt.Text.Trim()) < 0)
            {
                MessageBox.Show("Invalid rate.\n(Cannot be minus)", "Invalid rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxInsuAmt.Focus();
                return;
            }
            TextBoxComRate.Focus();
        }

        private void TextBoxComRate_Leave(object sender, EventArgs e)
        {
           // DropDownListCheckOn.Focus();
           // DropDownListCheckOn.DroppedDown = true;
            try
            {
                Convert.ToDecimal(TextBoxComRate.Text.Trim());
            }
            catch (Exception ex)
            {

                MessageBox.Show("Enter valid rate", "Invalid rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxComRate.Text = "";
                TextBoxComRate.Focus();
                return;
            }
            if (Convert.ToDecimal(TextBoxComRate.Text.Trim()) >100)
            {
                MessageBox.Show("Invalid rate.\n(Should be less than or equal to 100)", "Invalid amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxComRate.Focus();
                return;
            }

            if (Convert.ToDecimal(TextBoxComRate.Text.Trim()) < 0)
            {
                MessageBox.Show("Invalid rate.\n(Cannot be minus)", "Invalid rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxComRate.Focus();
                return;
            }
            DropDownListCheckOn.DroppedDown = true;
        }

        private void DropDownListCheckOn_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void DropDownListCheckOn_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DropDownListCalculateOnType.Focus();
            DropDownListCalculateOnType.DroppedDown = true;
        }

        private void DropDownListCalculateOnType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            TextBoxVat.Focus();
        }

        private void TextBoxVat_Leave(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDecimal(TextBoxVat.Text.Trim());
            }
            catch (Exception ex)
            {

                MessageBox.Show("Enter valid rate", "Invalid Rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxVat.Text = "";
                TextBoxVat.Focus();
                return;
            }
            //---------------

            if (Convert.ToDecimal(TextBoxVat.Text.Trim()) < 0)
            {
                MessageBox.Show("Invalid rate.\n(Cannot be minus)", "Invalid rate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxVat.Focus();
                return;
            }
            
        }

        private void TextBoxValueFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxValueTo.Focus();
            }
        }

        private void TextBoxValueTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxInsuRate.Focus();
            }
        }

        private void TextBoxInsuRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxComRate.Focus();
            }
        }

        private void TextBoxInsuAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxComRate.Focus();
            }
        }

        private void TextBoxComRate_KeyPress(object sender, KeyPressEventArgs e)
        {            
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.TextBoxComRate_Leave(null, null);
             //   DropDownListCheckOn.Focus();
              //  DropDownListCheckOn.DroppedDown = true;
            }           
        }

        private void TextBoxComValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.TextBoxComValue_Leave(null, null);

                //DropDownListCheckOn.Focus();
                //DropDownListCheckOn.DroppedDown = true;
            }           
        }

        private void TextBoxVat_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.TextBoxVat_Leave(null, null);
            //TextBoxVat_Leave
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
                TextBoxValueFrom.Focus();
            }
        }
    }
}
