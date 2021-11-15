using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Data;
using System.Text;

namespace FF.WebERPClient.General_Modules
{
    public partial class ECDDefinition : BasePage
    {
        #region properties

        public List<PriceBookLevelRef> PBList
        {
            get { return (List<PriceBookLevelRef>)Session["PBList"]; }
            set { Session["PBList"] = value; }
        }

        #endregion



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                PBList = new List<PriceBookLevelRef>();

                KeyupScriptBind();
                //set the width of the uc's description textboxes
                UCWidthSet();
            }
        }

        private void UCWidthSet()
        {
            ((TextBox)uc_ProfitCenterSearchLoyaltyType.FindControl("TextBoxCompanyDes")).Width = 100;
            ((TextBox)uc_ProfitCenterSearchLoyaltyType.FindControl("TextBoxChannelDes")).Width = 100;
            ((TextBox)uc_ProfitCenterSearchLoyaltyType.FindControl("TextBoxSubChannelDes")).Width = 100;
            ((TextBox)uc_ProfitCenterSearchLoyaltyType.FindControl("TextBoxAreaDes")).Width = 100;
            ((TextBox)uc_ProfitCenterSearchLoyaltyType.FindControl("TextBoxRegionDes")).Width = 100;
            ((TextBox)uc_ProfitCenterSearchLoyaltyType.FindControl("TextBoxZoneDes")).Width = 100;
            ((TextBox)uc_ProfitCenterSearchLoyaltyType.FindControl("TextBoxLocationDes")).Width = 100;
        }

        private void KeyupScriptBind()
        {
            TextBoxSchemaCategory.Attributes.Add("onKeyup", "return clickButton(event,'" + ImageButton1.ClientID + "')");
            TextBoxSchemaType.Attributes.Add("onKeyup", "return clickButton(event,'" + ImageButton2.ClientID + "')");
            txtPriceBook.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnPBsearch.ClientID + "')");
            txtLevel.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnLevelSearch.ClientID + "')");
        }

        protected void imgBtnPBsearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPriceBook.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        #region search help
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {

                        paramsText.Append(GlbUserComCode + seperator + txtPriceBook.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Schema_category:
                    {

                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Schema_Type:
                    {

                        paramsText.Append(seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        protected void imgBtnLevelSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPriceLevelByBookData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtLevel.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImgBtnAddPB_Click(object sender, ImageClickEventArgs e)
        {
            #region validation

            if (txtPriceBook.Text == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select price book");
                return;
            }

            #endregion

            List<PriceBookLevelRef> pbLIST = new List<PriceBookLevelRef>();
            pbLIST = (CHNLSVC.Sales.GetPriceLevelList(GlbUserComCode, txtPriceBook.Text.Trim().ToUpper(), txtLevel.Text.Trim().ToUpper()));

            pbLIST.RemoveAll(x => x.Sapl_act == false);
            var distinctList = pbLIST.GroupBy(x => x.Sapl_pb_lvl_cd)
                         .Select(g => g.First())
                         .ToList();

            PBList.AddRange(distinctList);
            grvPB_PBL.DataSource = PBList;
            grvPB_PBL.DataBind();
        }

        protected void grvPB_PBL_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 rowIndex = e.RowIndex;
            string PBook = Convert.ToString(grvPB_PBL.Rows[rowIndex].Cells[1].Text);
            string PBLevel = Convert.ToString(grvPB_PBL.Rows[rowIndex].Cells[2].Text);
            PBList.RemoveAt(rowIndex);
            grvPB_PBL.DataSource = PBList;
            grvPB_PBL.DataBind();
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            #region validation

            if (TextBoxDate.Text == "") {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select creation date");
                return;
            }
            if (TextBoxFromDate.Text == "") {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select from date");
                return;
            }
            if (TextBoxToDate.Text == "") {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select to date");
                return;
            }

            DateTime _date;
            if (!DateTime.TryParse(TextBoxDate.Text, out _date))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter creation date correctly");
                return;
            }
            if (!DateTime.TryParse(TextBoxFromDate.Text, out _date)) {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter from date correctly");
                return;
            }
            if (!DateTime.TryParse(TextBoxToDate.Text, out _date)) {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter to date");
                return;
            }
            if (GridViewPC.Rows.Count <= 0) {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select profit centers");
                return;
            }
            if (GridViewSchemas.Rows.Count <= 0) {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select schema");
                return;
            }
            if (grvPB_PBL.Rows.Count <= 0) {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select price book details");
                return;
            }

            #endregion

            string _pcList = "";
            string _pbList = "";
            string _pblvlList = "";
            string _schList = "";

            //fill pc,pb,schema
            foreach (GridViewRow gvr in grvPB_PBL.Rows)
            {
                    _pbList = _pbList + gvr.Cells[1].Text + ",";
                    _pblvlList = _pblvlList + gvr.Cells[2].Text + ",";
            }
            foreach (GridViewRow gvr in GridViewSchemas.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    _schList = _schList + gvr.Cells[1].Text + ",";
                }
            }
            foreach (GridViewRow gvr in GridViewPC.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                if (chkSelect.Checked)
                {
                    _pcList = _pcList + gvr.Cells[1].Text + ",";
                }
            }

            string _ecdBase = "";
            if (RadioButtonFI.Checked) {
                _ecdBase = "FI";
            }
            else if (RadioButtonCI.Checked) {
                _ecdBase = "CI";
            }
            else if (RadioButtonFR.Checked)
            {
                _ecdBase = "FR";
            }
            else {
                _ecdBase = "TI";
            }

            string _accTp = "";

            if (RadioButtonAA.Checked) {
                _accTp = "AA";
            }
            else if (RadioButtonGA.Checked) {
                _accTp = "GA";
            }
            else //if (RadioButtonAny.Checked) 
            {
                _accTp = "AL";
            }

            string _effCr="";
            if(RadioButtonBC.Checked){
            _effCr="BC";
            }
            else if(RadioButtonAC.Checked){
            _effCr="AC";
            }
            else //if(RadioButtonDateAny.Checked)
            {
            _effCr="AL";
            }

            string _commit = "";

            if (RadioButtonCP.Checked)
            {
                _commit = "CP";
            }
            else if (RadioButtonCS.Checked)
            {
                _commit = "CP";
            }
            else {
                _commit = "AL";
            }

            string _ecdtp = "";
            if (_ecdBase == "FI" && _effCr == "AL" && _commit == "AL" && _accTp == "AL")
            {
                _ecdtp = "S";
            }
            else
                _ecdtp = "N";

            
            //remove last comma
            _pbList = _pbList.Substring(0, _pbList.Length - 1);
            _pblvlList = _pblvlList.Substring(0, _pblvlList.Length - 1);
            _schList = _schList.Substring(0, _schList.Length - 1);
            _pcList = _pcList.Substring(0, _pcList.Length - 1);

            EarlyClosingDiscount _ecd = new EarlyClosingDiscount();
            _ecd.Hed_tp = _ecdtp;
            _ecd.Hed_pty_tp = "PC";
            _ecd.Hed_cre_by = GlbUserName;
            _ecd.Hed_cre_dt = DateTime.Now;
            _ecd.Hed_from_dt = Convert.ToDateTime(TextBoxFromDate.Text);
            _ecd.Hed_to_dt = Convert.ToDateTime(TextBoxToDate.Text);
            _ecd.Hed_ecd_base = _ecdBase;
            _ecd.Hed_eff_acc_tp = _accTp;
            _ecd.Hed_eff_cre_dt = _effCr;
            _ecd.Hed_eff_dt = Convert.ToDateTime(TextBoxDate.Text);
            _ecd.Hed_comit = _commit;
            if (TextBoxRate.Enabled)
            {
                _ecd.Hed_ecd_val = Convert.ToDecimal(TextBoxRate.Text);
                _ecd.Hed_ecd_is_rt = true;
            }
            else {
                _ecd.Hed_ecd_val = Convert.ToDecimal(TextBoxValue.Text);
                _ecd.Hed_ecd_is_rt = false;
            }


            int result = CHNLSVC.Sales.SaveECDDefinition(_ecd, _pcList, _pbList, _pblvlList, _schList);

            if (result > 0)
            {
                string Msgg = "<script>alert('Records Insert Sucessfully');window.location ='ECDDefinition.aspx'</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);
            }
        }
        #region close/cancel

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("../General_Modules/ECDDefinition.aspx");
        }

        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

        #endregion

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Schema_Type);
            DataTable dataSource = CHNLSVC.CommonSearch.GetSchemaType(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxSchemaType.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Schema_category);
            DataTable dataSource = CHNLSVC.CommonSearch.GetSchemaCategory(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxSchemaCategory.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            string _type = "";
            string _cat;
            if (TextBoxSchemaType.Text == "")
            {
                _type = "%";
            }
            else
            {
                _type = TextBoxSchemaType.Text;
            }
            if (TextBoxSchemaCategory.Text == "")
            {
                _cat = "%";
            }
            else
            {
                _cat = TextBoxSchemaCategory.Text;
            }

            List<HpSchemeDetails> _schList = CHNLSVC.Sales.GetSchemaByTypeCat(_type, _cat);
            if (_schList.Count > 0)
            {
                GridViewSchemas.DataSource = _schList;
                GridViewSchemas.DataBind();
            }
            else
            {
                LoadEmptySchema();
            }
        }

        private void LoadEmptySchema()
        {
            DataTable dt = new DataTable();
            GridViewSchemas.DataSource = dt;
            GridViewSchemas.DataBind();
        }



        protected void ButtonAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in GridViewPC.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                chkSelect.Checked = true;
            }
        }

        protected void ButtonNone_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in GridViewPC.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                chkSelect.Checked = false;
            }

        }

        protected void ButtonClearPc_Click(object sender, EventArgs e)
        {
            GridViewPC.DataSource = null;
            GridViewPC.DataBind();
        }

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

        protected void ButtonSchAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in GridViewSchemas.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chkSelect");
                chkSelect.Checked = true;
            }
        }

        protected void ButtonSchNone_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in GridViewSchemas.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chkSelect");
                chkSelect.Checked = false;
            }
        }

        protected void ButtonSchClear_Click(object sender, EventArgs e)
        {
            GridViewSchemas.DataSource = null;
            GridViewSchemas.DataBind();
        }

        protected void TextBoxValue_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxValue.Text != "")
            {
                TextBoxRate.Text = "";
                TextBoxRate.Enabled = false;
            }
            else {
                TextBoxRate.Enabled = true;
            }
        }

        protected void TextBoxRate_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxRate.Text != "")
            {
                TextBoxValue.Text = "";
                TextBoxValue.Enabled = false;
            }
            else {
                TextBoxValue.Enabled = true;
               
            }
        }
    }
}