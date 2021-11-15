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
    public partial class ECDVoucher : BasePage
    {
        #region common function

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                KeyupScriptBind();
                //set the width of the uc's description textboxes
                UCWidthSet();
                LoadEmptyGrid();
                

                TextBoxVouGenTo.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                TextBoxDatePriTo.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                TextBoxDatePriFrom.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                
                LinkButtonView_Click(null, null);
                LinkButtonView1_Click(null, null);
                LoadSchemesFromPcs();
                LoadRatesForPcsAndSchmes();
                LoadVouchersFromPcSchmeAndRate();
            }
            UCWidthSet();
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
        }

        #endregion

        #region voucher definition


        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            #region validation

            DateTime date;
            if(!DateTime.TryParse(TextBoxFrom.Text,out date)){
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter From date correctly");
                return;
            }
            if (!DateTime.TryParse(TextBoxTo.Text, out date))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter To date correctly");
                return;
            }
            if (Convert.ToDateTime(TextBoxFrom.Text) > Convert.ToDateTime(TextBoxTo.Text)) {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From date has to be smaller than To date");
                return;
            }
            decimal value;
            if (!decimal.TryParse(TextBoxBalFrom.Text,out value)) {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter balance from correctly");
                return;
            }
            if (!decimal.TryParse(TextBoxBalTo.Text, out value)) {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter balance to correctly");
                return;
            }
            if (TextBoxECDRate.Enabled)
            {
                if (!decimal.TryParse(TextBoxECDRate.Text, out value))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter ECD rate correctly");
                    return;
                }
            }
            else
            {
                if (!decimal.TryParse(TextBoxECDVal.Text, out value))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter ECD value correctly");
                    return;
                }
            }
            #endregion

            string _schList = "";
            string _pcList = "";

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

            FF.BusinessObjects.ECDVoucher _vou = new BusinessObjects.ECDVoucher();
            _vou.Hvd_cre_by = GlbUserName;
            _vou.Hvd_cre_dt = DateTime.Now;
            if (TextBoxECDRate.Enabled)
            {
                _vou.Hvd_ecd_val = Convert.ToDecimal(TextBoxECDRate.Text);
                _vou.Hvd_is_rt = true;
            }
            else {
                _vou.Hvd_ecd_val = Convert.ToDecimal(TextBoxECDVal.Text);
                _vou.Hvd_is_rt = false;
            }
            _vou.Hvd_from_bal = Convert.ToDecimal(TextBoxBalFrom.Text);
            _vou.Hvd_from_dt = Convert.ToDateTime(TextBoxFrom.Text);
            _vou.Hvd_to_bal = Convert.ToDecimal(TextBoxBalTo.Text);
            _vou.Hvd_to_dt = Convert.ToDateTime(TextBoxTo.Text);

            int result = CHNLSVC.Sales.SaveECDVoucher(_vou, _pcList, _schList);

            if (result > 0)
            {
                string Msgg = "<script>alert('Records Insert Sucessfully');window.location ='ECDVoucher.aspx'</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);
            }
            else {
                string Msgg = "<script>alert('Error occured data not inserted.');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);
            }
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("../General_Modules/ECDVoucher.aspx");
        }

        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
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

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Schema_category);
            DataTable dataSource = CHNLSVC.CommonSearch.GetSchemaCategory(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxSchemaCategory.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Schema_Type);
            DataTable dataSource = CHNLSVC.CommonSearch.GetSchemaType(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxSchemaType.ClientID;
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

        private void LoadEmptySchema()
        {
            DataTable dt = new DataTable();
            GridViewSchemas.DataSource = dt;
            GridViewSchemas.DataBind();
        }

        protected void TextBoxECDVal_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxECDVal.Text != "")
            {
                TextBoxECDRate.Enabled = false;
                TextBoxECDRate.Text = "";
            }
            else {
                TextBoxECDRate.Enabled = true;
            }
        }

        protected void TextBoxECDRate_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxECDRate.Text != "")
            {
                TextBoxECDVal.Enabled = false;
                TextBoxECDVal.Text = "";
            }
            else
            {
                TextBoxECDVal.Enabled = true;
            }
        }

        protected void ImageButton3_Click1(object sender, ImageClickEventArgs e)
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


        protected void ButtonSchNone_Click1(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in GridViewSchemas.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chkSelect");
                chkSelect.Checked = false;
            }
        }

        protected void ButtonSchAll_Click1(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in GridViewSchemas.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chkSelect");
                chkSelect.Checked = true;
            }
        }

        protected void ButtonSchClear_Click1(object sender, EventArgs e)
        {
            GridViewSchemas.DataSource = null;
            GridViewSchemas.DataBind();
        }


        #endregion

        #region voucher generation

        private void LoadEmptyGrid()
        {
            DataTable _dt = new DataTable();
            GridViewVouGenPC.DataSource = _dt;
            GridViewVouGenPC.DataBind();
        }

        protected void LinkButtonView_Click(object sender, EventArgs e)
        {
            if (TextBoxVouGenTo.Text=="")
                return;

           DataTable _dt= CHNLSVC.Sales.GetECDVoucherGeneratePc(GlbUserComCode,Convert.ToDateTime(TextBoxVouGenTo.Text));
           GridViewVouGenPC.DataSource = _dt;
           GridViewVouGenPC.DataBind();
        }

        protected void ButtonProcess_Click(object sender, EventArgs e)
        {
            List<string> _pcList = new List<string>();
            

            foreach (GridViewRow gvr in GridViewVouGenPC.Rows) {
                CheckBox chk = (CheckBox)gvr.FindControl("chkSelect");
                if (chk.Checked) {
                    _pcList.Add(gvr.Cells[1].Text);
                }            
            }
            int result = CHNLSVC.Sales.ProcessECDVoucherGeneration(_pcList, Convert.ToDateTime(TextBoxVouGenTo.Text), GlbUserComCode,GlbUserName);
            if (result > 0)
            {
                string Msgg = "<script>alert('Records Updated Sucessfully');window.location ='ECDVoucher.aspx'</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);
            }
            else
            {
                string Msgg = "<script>alert('Nothing Update');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);
            }
        }

        protected void ButtonGenClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

        protected void ButtonGenClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("../General_Modules/ECDVoucher.aspx");
        }

        #endregion

        #region voucher print

        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            if (GridViewVouNo.Rows.Count <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No voucher to print");
                return;

            }
            string vounoList = "";
            foreach (GridViewRow gvr in GridViewVouNo.Rows) {
                CheckBox ck = (CheckBox)gvr.FindControl("chkSelect2");
                if (ck.Checked)
                {
                    string vouno = gvr.Cells[1].Text;
                    vounoList = vounoList + "'" + vouno + "'" + " , ";
                    int result = CHNLSVC.Sales.UpdateECDVoucherPrintStatus(vouno);
                }
            }

            //user not selected any voucher
            if (vounoList != "")
            {
                vounoList = vounoList.Substring(0, vounoList.Length - 3);

                GlbReportPath = "~\\Reports_Module\\HP_Rep\\ECDVoucher.rpt";
                GlbReportMapPath = "~/Reports_Module/HP_Rep/ECDVoucher.rpt";
                GlbSelectionFormula = "{HPR_ECD_DEFN.HED_VOU_NO} in [" + vounoList + "]";

                string Msg1 = "<script>window.open('../Reports_Module/HP_Rep/ECDVoucherPrint.aspx','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);
            }
        }

        protected void ButtonPrintClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("../General_Modules/ECDVoucher.aspx");
        }

        protected void ButtonPrintClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

        protected void LinkButtonView1_Click(object sender, EventArgs e)
        {
            DataTable _dt = CHNLSVC.Sales.GetECDNotPrintPcs(GlbUserComCode, Convert.ToDateTime(TextBoxDatePriFrom.Text), Convert.ToDateTime(TextBoxDatePriTo.Text));
            GridViewVouPrntPC.DataSource = _dt;
            GridViewVouPrntPC.DataBind();
            LoadSchemesFromPcs();
            LoadRatesForPcsAndSchmes();
            LoadVouchersFromPcSchmeAndRate();
        }

        private void LoadSchemesFromPcs()
        {

            string _pcList = "";
            foreach (GridViewRow gvr in GridViewVouPrntPC.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("chkSelect");
                if (chk.Checked)
                {
                    _pcList = _pcList + gvr.Cells[1].Text + ",";
                }
            }
            if (_pcList != "")
            {
                DataTable _dt = CHNLSVC.Sales.GetECDDefnSchemesFromPcsAndDates(Convert.ToDateTime(TextBoxDatePriFrom.Text), Convert.ToDateTime(TextBoxDatePriTo.Text), _pcList);
                GridViewSchmes.DataSource = _dt;
                GridViewSchmes.DataBind();
            }
            else {
                DataTable _dt = new DataTable();
                GridViewSchmes.DataSource = _dt;
                GridViewSchmes.DataBind();
            }
        }


        private void LoadRatesForPcsAndSchmes() {
            string _pcList = "";
            string _schList = null;
            foreach (GridViewRow gvr in GridViewVouPrntPC.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("chkSelect");
                if (chk.Checked)
                {
                    _pcList = _pcList + gvr.Cells[1].Text + ",";
                }
            }
            foreach (GridViewRow gvr in GridViewSchmes.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("chkSelect1");
                if (chk.Checked)
                {
                    _schList = _schList + gvr.Cells[1].Text + ",";
                }
            }
            if (_pcList != "")
            {
                _schList = (string.IsNullOrEmpty(_schList)) ? null : _schList;
                DataTable _dt = CHNLSVC.Sales.GetECDDefnRateFromPcAndSchmes(Convert.ToDateTime(TextBoxDatePriFrom.Text), Convert.ToDateTime(TextBoxDatePriTo.Text), _pcList, _schList);
                GridViewRate.DataSource = _dt;
                GridViewRate.DataBind();
            }
            else
            {
                DataTable _dt = new DataTable();
                GridViewRate.DataSource = _dt;
                GridViewRate.DataBind();
            }
        }

        private void LoadVouchersFromPcSchmeAndRate(){
            string _pcList = "";
            string _schList = null;
            string _rateList = null;
            foreach (GridViewRow gvr in GridViewVouPrntPC.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("chkSelect");
                if (chk.Checked)
                {
                    _pcList = _pcList + gvr.Cells[1].Text + ",";
                }
            }
            foreach (GridViewRow gvr in GridViewSchmes.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("chkSelect1");
                if (chk.Checked)
                {
                    _schList = _schList + gvr.Cells[1].Text + ",";
                }
            }
            foreach (GridViewRow gvr in GridViewRate.Rows)
            {
                CheckBox chk = (CheckBox)gvr.FindControl("chkSelect2");
                if (chk.Checked)
                {
                    _rateList = _rateList + gvr.Cells[1].Text + ",";
                }
            }
            if (_pcList != "")
            {
                _schList = (string.IsNullOrEmpty(_schList)) ? null : _schList;
                _rateList = (string.IsNullOrEmpty(_rateList)) ? null : _rateList;
                DataTable _dt = CHNLSVC.Sales.GetECDDefnVouFromPcAndSchAndRate(Convert.ToDateTime(TextBoxDatePriFrom.Text), Convert.ToDateTime(TextBoxDatePriTo.Text), _pcList, _schList, _rateList);
                GridViewVouNo.DataSource = _dt;
                GridViewVouNo.DataBind();
            }
            else {
                DataTable _dt = new DataTable();
                GridViewVouNo.DataSource = _dt;
                GridViewVouNo.DataBind();
            }
        }

        protected void chkSeclect_CheckdChanged(object sender, EventArgs e)
        {
            LoadSchemesFromPcs();
            LoadRatesForPcsAndSchmes();
            LoadVouchersFromPcSchmeAndRate();
        }

        protected void chkSeclect1_CheckdChanged(object sender, EventArgs e)
        {
            
            LoadRatesForPcsAndSchmes();
            LoadVouchersFromPcSchmeAndRate();
        }

        protected void chkSeclect2_CheckdChanged(object sender, EventArgs e)
        {
            LoadVouchersFromPcSchmeAndRate();
        }

        #endregion
    }
}