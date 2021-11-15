using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FF.BusinessObjects;
using System.Text;

namespace FF.WebERPClient.General_Modules
{
    public partial class DiriyaDefinition : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                RadioButtonSchemeCategory_CheckedChanged(null, null);
               
                
            }
            DropDownListSchemeType.Attributes.Add("onmouseover", "this.title=this.options[this.selectedIndex].title");
            LoadToolTip();
        }

        #region All/None/Clear

        protected void ButtonAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in GridViewPC.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                chkSelect.Checked = true;
            }
        }

        protected void ButtonClearPc_Click(object sender, EventArgs e)
        {
            GridViewPC.DataSource = null;
            GridViewPC.DataBind();
        }

        protected void ButtonNone_Click(object sender, EventArgs e)
        {

            foreach (GridViewRow gvr in GridViewPC.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                chkSelect.Checked = false;
            }
        }

        protected void ButtonSchemeAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in GridViewScheme.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                chkSelect.Checked = true;
            }
        }

        protected void ButtonSchemeNone_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in GridViewScheme.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                chkSelect.Checked = false;
            }
        }

        protected void ButtonSchemeClear_Click(object sender, EventArgs e)
        {
            GridViewScheme.DataSource = null;
            GridViewScheme.DataBind();
        }

        #endregion


        protected void ImageButtonAddPC_Click(object sender, ImageClickEventArgs e)
        {
            string com = uc_ProfitCenterSearchLoyaltyType.Company.ToUpper();
            string chanel = uc_ProfitCenterSearchLoyaltyType.Channel.ToUpper();
            string subChanel = uc_ProfitCenterSearchLoyaltyType.SubChannel.ToUpper();
            string area = uc_ProfitCenterSearchLoyaltyType.Area.ToUpper();
            string region = uc_ProfitCenterSearchLoyaltyType.Region.ToUpper();
            string zone = uc_ProfitCenterSearchLoyaltyType.Zone.ToUpper();
            string pc = uc_ProfitCenterSearchLoyaltyType.ProfitCenter.ToUpper();

            DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com, chanel, subChanel, area, region, zone, pc);
            GridViewPC.DataSource = dt;
            GridViewPC.DataBind();
        }


        #region data load

        protected void GridViewPC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chekMand = (CheckBox)e.Row.FindControl("chekPc");
                if (!chekMand.Checked)
                {
                    chekMand.Checked = true;
                }
            }
        }

        private void LoadCategory()
        {
            DropDownListSchemeType.Items.Clear();
            DataTable _dt = CHNLSVC.Sales.GetSAllchemeCategoryies(null);
            foreach (DataRow dr in _dt.Rows) {
                DropDownListSchemeType.Items.Add(new ListItem(dr["HSC_CD"].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr["HSC_CD"].ToString().Length)) + "-" + dr["HSC_DESC"].ToString(), dr["HSC_CD"].ToString()));
            }
        }

        private void LoadToolTip() {

            foreach (ListItem _listItem in DropDownListSchemeType.Items)
            {

                _listItem.Attributes.Add("title", _listItem.Text);
            }
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

        private void LoadType() {
            DropDownListSchemeType.Items.Clear();
            List<HpSchemeType> _list = CHNLSVC.Sales.GetAllSchemeTypes();
            if (_list != null) {
                foreach (HpSchemeType _sc in _list) {
                    DropDownListSchemeType.Items.Add(new ListItem(_sc.Hst_cd + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - _sc.Hst_cd.Length)) + "-" + _sc.Hst_desc, _sc.Hst_cd));
                }
            }
        }

        #endregion

        #region radio checked

        protected void RadioButtonSchemeCategory_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonSchemeCategory.Checked)
            {
                LoadCategory();
                LoadToolTip();
                LoadSchemes();
            }
        }


        protected void RadioButtonSchemeType_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonSchemeType.Checked) {
                LoadType();
                LoadToolTip();
                LoadSchemes();
            }
        }

        protected void RadioButtonRate_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonRate.Checked)
            {
                TextBoxRate.Enabled = true;
                TextBoxRate.Text = "0";
                TextBoxValue.Enabled = false;
                TextBoxValue.Text = "";
            }
            else {
                TextBoxRate.Enabled = false;
                TextBoxRate.Text = "";
                TextBoxValue.Enabled = true;
                TextBoxValue.Text = "0";
            }
        }

        protected void RadioButtonValue_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonValue.Checked) {
                TextBoxValue.Enabled = true;
                TextBoxValue.Text = "0";
                TextBoxRate.Enabled = false;
                TextBoxRate.Text = "";
            }
            else {
                TextBoxRate.Enabled = true;
                TextBoxRate.Text = "0";
                TextBoxValue.Enabled = false;
                TextBoxValue.Text = "";
            }
        }

        #endregion

        #region Clear/Close

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("../General_Modules/DiriyaDefinition.aspx");
        }

        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

        #endregion

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (TabContainerMain.ActiveTabIndex == 0)
            {

                #region validation

                if (GridViewScheme.Rows.Count <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please add scheme codes");
                    return;
                }
                if (GridViewPC.Rows.Count <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please add profit centers");
                    return;
                }
                if (TextBoxFromDate.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select from date");
                    return;
                }
                if (TextBoxTodate.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select to date");
                    return;
                }
                if (Convert.ToDateTime(TextBoxFromDate.Text) > Convert.ToDateTime(TextBoxTodate.Text))
                {

                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From date has to be smaller than to date");
                    return;
                }
                decimal val;
                if (!decimal.TryParse(TextBoxValueFrom.Text, out val))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter from value");
                    return;
                }
                if (!decimal.TryParse(TextBoxValueTo.Text, out val))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter to value");
                    return;
                }
                if (!decimal.TryParse(TextBoxVat.Text, out val))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter vat value");
                    return;
                }

                if (Convert.ToDecimal(TextBoxValueFrom.Text) > Convert.ToDecimal(TextBoxValueTo.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From value has to be smaller than to value");
                    return;
                }
                if (TextBoxComValue.Text == "" && TextBoxComRate.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter commission rate or value");
                    return;
                }
                if (TextBoxRate.Text == "" && TextBoxValue.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter calculation by on rate or value");
                    return;
                }


                #endregion

                HPInsuranceScheme _scheme = new HPInsuranceScheme();
                _scheme.Hpi_cal_on = DropDownListCalcOn.SelectedValue;
                _scheme.Hpi_chk_on = DropDownListCheckOn.SelectedValue;
                _scheme.Hpi_cre_by = GlbUserName;
                _scheme.Hpi_cre_dt = DateTime.Now;
                _scheme.Hpi_from_dt = Convert.ToDateTime(TextBoxFromDate.Text);
                _scheme.Hpi_from_val = Convert.ToDecimal(TextBoxValueFrom.Text);
                _scheme.Hpi_to_val = Convert.ToDecimal(TextBoxValueTo.Text);
                _scheme.Hpi_vat_rt = Convert.ToDecimal(TextBoxVat.Text);

                //_scheme.Hpi_comm = Convert.ToDecimal(TextBoxCommission.Text);}
                if (TextBoxRate.Text != "")
                {
                    _scheme.Hpi_ins_val = Convert.ToDecimal(TextBoxRate.Text);
                    _scheme.Hpi_ins_isrt = true;
                }
                else
                {
                    _scheme.Hpi_ins_val = Convert.ToDecimal(TextBoxValue.Text);
                    _scheme.Hpi_ins_isrt = false;
                }

                if (TextBoxComRate.Text != "")
                {
                    _scheme.Hpi_comm_isrt = true;
                    _scheme.Hpi_comm = Convert.ToDecimal(TextBoxComRate.Text);
                }
                else
                {
                    _scheme.Hpi_comm_isrt = false;
                    _scheme.Hpi_comm = Convert.ToDecimal(TextBoxComValue.Text);

                }




                List<string> _pcList = new List<string>();
                foreach (GridViewRow gr in GridViewPC.Rows)
                {
                    _pcList.Add(gr.Cells[1].Text);
                }

                List<string> _schList = new List<string>();
                foreach (GridViewRow gr in GridViewScheme.Rows)
                {
                    _schList.Add(gr.Cells[1].Text);
                }

                int result = CHNLSVC.Sales.SaveHPInsurance(_pcList, _schList, _scheme);
                if (result > 0)
                {
                    string Msgg = "<script>alert('Records Insert Sucessfully');window.location ='DiriyaDefinition.aspx'</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);
                }
            }
            else {

                #region validation

                if (RadioButtonPC.Checked) {
                    if (TextBoxPC.Text == "") {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Plaease select original PC");
                        return;
                    }
                    if (ListBoxProfitCenters.Items.Count<=0)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Plaease enter transfer PCs");
                        return;
                    }
                }
                if (RadioButtonScheme.Checked)
                {
                    if (TextBoxScheme.Text == "")
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Plaease select original Scheme");
                        return;
                    }
                    if (ListBoxScheme.Items.Count <= 0)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Plaease enter transfer Schemes");
                        return;
                    }
                }

                #endregion

                if (RadioButtonPC.Checked) {
                    List<HPInsuranceScheme> _sch = CHNLSVC.Sales.GetSchemeByPCOrSchemeCode("PC", TextBoxPC.Text.ToUpper(), "PC", null, DateTime.Now.Date);
                    if (_sch.Count<=0) {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Profit Center has no definitions");
                        return;
                    }
                    else {
                        List<string> _pcs = new List<string>();
                        foreach (ListItem li in ListBoxProfitCenters.Items) {
                            _pcs.Add(li.Text);
                        }
                        List<string> _scheme = new List<string>();
                        _scheme.Add(_sch[0].Hpi_sch_cd);
                        int result = CHNLSVC.Sales.SaveHPInsurance(_pcs, _scheme, _sch[0]);
                        if (result > 0)
                        {
                            string Msgg = "<script>alert('Records Insert Sucessfully');window.location ='DiriyaDefinition.aspx'</script>";
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);
                        }
                    }
                }
                else {
                    List<HPInsuranceScheme> _sch = CHNLSVC.Sales.GetSchemeByPCOrSchemeCode("PC", GlbUserDefProf, "Scheme", TextBoxScheme.Text.ToUpper(), DateTime.Now.Date);
                    if (_sch.Count == 0)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Scheme has no definitions");
                        return;
                    }
                    else
                    {
                        List<string> _pcs = new List<string>();
                        List<string> _scheme = new List<string>();
                        foreach (ListItem li in ListBoxScheme.Items)
                        {
                            _scheme.Add(li.Text);
                        }
                        
                        _pcs.Add(_sch[0].Hpi_pty_cd);
                        int result = CHNLSVC.Sales.SaveHPInsurance(_pcs, _scheme, _sch[0]);
                        if (result > 0)
                        {
                            string Msgg = "<script>alert('Records Insert Sucessfully');window.location ='DiriyaDefinition.aspx'</script>";
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);
                        }
                    }
                }
            
            }
        }

        protected void DropDownListSchemeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSchemes();

        }

        private void LoadSchemes()
        {
            #region validation

            if (TextBoxTerm.Text == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please Enter Term");
                return;
            }

            #endregion

            if (RadioButtonSchemeCategory.Checked)
            {
                string _condition = DropDownListCoundition.SelectedValue;
                if (_condition == "&lt;=")
                {
                    _condition = "<=";
                }
                else if (_condition == "&gt;=")
                {
                    _condition = ">=";
                }

                List<HpSchemeDetails> _cshList = CHNLSVC.Sales.GetHPInsuranceSchemeCodes(DropDownListSchemeType.SelectedValue, null, "Code", Convert.ToInt32(TextBoxTerm.Text), _condition);
                GridViewScheme.DataSource = _cshList;
                GridViewScheme.DataBind();
            }
            else
            {
                string _condition = DropDownListCoundition.SelectedValue;
                if (_condition == "&lt;=")
                {
                    _condition = "<=";
                }
                else if (_condition == "&gt;=")
                {
                    _condition = ">=";
                }

                List<HpSchemeDetails> _cshList = CHNLSVC.Sales.GetHPInsuranceSchemeCodes(DropDownListSchemeType.SelectedValue, null, "Type", Convert.ToInt32(TextBoxTerm.Text), _condition);
                GridViewScheme.DataSource = _cshList;
                GridViewScheme.DataBind();
            }
        }

        protected void DropDownListCoundition_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSchemes();
        }

        protected void TextBoxTerm_TextChanged(object sender, EventArgs e)
        {
            LoadSchemes();
        }

        protected void TextBoxRate_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxRate.Text != "")
            {
                TextBoxValue.Enabled = false;
                TextBoxValue.Text = "";
            }
            else
                TextBoxValue.Enabled = true;
        }

        protected void TextBoxValue_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxValue.Text != "")
            {
                TextBoxRate.Enabled = false;
                TextBoxRate.Text = "";
            }
            else
                TextBoxRate.Enabled = true;
        }

        protected void TextBoxComValue_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxComValue.Text != "")
            {
                TextBoxComRate.Enabled = false;
                TextBoxComRate.Text = "";
            }
            else {
                TextBoxComRate.Enabled = true;
            }
        }

        protected void TextBoxComRate_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxComRate.Text != "")
            {
                TextBoxComValue.Enabled = false;
                TextBoxComValue.Text = "";
            }
            else
            {
                TextBoxComValue.Enabled = true;
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            
            //check validity
            MasterProfitCenter _pc = CHNLSVC.General.GetPCByPCCode(GlbUserComCode, TextBoxPC1.Text.ToUpper());
            if (_pc == null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid PC code");
                return;
            }
            else
            {
                ListBoxProfitCenters.Items.Add(TextBoxPC1.Text.ToUpper());
                TextBoxPC1.Text = "";
            }
        }


        protected void RadioButtonPC_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonPC.Checked)
            {
                PanelScheme.Visible = false;
                PanelPC.Visible = true;
            }
            else {
                PanelScheme.Visible = false;
                PanelPC.Visible = true;
            }
        }

        protected void RadioButtonScheme_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonScheme.Checked)
            {
                PanelPC.Visible = false;
                PanelScheme.Visible = true;
            }
            else {
                PanelPC.Visible = true;
                PanelScheme.Visible = false;
            }
        }

        protected void ImageButtonPC_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
            DataTable dataSource = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxPC.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImageButtonPC1_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
            DataTable dataSource = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxPC1.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImageButtonScheme_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Scheme);
            DataTable dataSource = CHNLSVC.CommonSearch.Get_SchemesCD_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxScheme.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImageButtonScheme1_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Scheme);
            DataTable dataSource = CHNLSVC.CommonSearch.Get_SchemesCD_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxScheme1.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
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
                        paramsText.Append(GlbUserName + seperator + GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(GlbUserName + seperator + GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Scheme:
                    {
                        paramsText.Append(TextBoxScheme.Text.Trim() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void ButtonAddScheme_Click(object sender, EventArgs e)
        {
            HpSchemeDetails _sch = CHNLSVC.Sales.getSchemeDetByCode(TextBoxScheme1.Text.ToUpper());

            if (_sch.Hsd_cd == null) {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid Scheme code");
                return;
            }
            else
            {
                ListBoxScheme.Items.Add(TextBoxScheme1.Text.ToUpper());
                TextBoxScheme1.Text = "";
            }
        }

        protected void LinkButtonRemovePC_Click(object sender, EventArgs e)
        {
            if (ListBoxProfitCenters.SelectedIndex != -1) {
                ListBoxProfitCenters.Items.RemoveAt(ListBoxProfitCenters.SelectedIndex);
            }
            else {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select item to remove");
                return;
            }
        }

        protected void LinkButtonRemoveScheme_Click(object sender, EventArgs e)
        {
            if (ListBoxScheme.SelectedIndex != -1)
            {
                ListBoxScheme.Items.RemoveAt(ListBoxScheme.SelectedIndex);
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select item to remove");
                return;
            }
        }
    }
}