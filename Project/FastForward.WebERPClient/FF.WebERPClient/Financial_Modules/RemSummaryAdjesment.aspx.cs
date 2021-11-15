using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using FF.WebERPClient;
using FF.BusinessObjects;

namespace FF.WebERPClient.Financial_Modules
{
    public partial class RemSummaryAdjesment : BasePage
    {

        #region properties

        public bool CanUpdate {
            get { return (bool)ViewState["CanUpdate"]; }
            set { ViewState["CanUpdate"] = value; }
        }
        public bool IsExcess
        {
            get { return (bool)ViewState["IsExcess"]; }
            set { ViewState["IsExcess"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                string month="";
                switch (DateTime.Now.Month)
                {
                    case 1:
                        month = "January";
                        break;
                    case 2:
                        month = "February";
                        break;
                    case 3:
                        month = "March";
                        break;
                    case 4:
                        month = "April";
                        break;
                    case 5:
                        month = "May";
                        break;
                    case 6:
                        month = "June";
                        break;
                    case 7:
                        month =  "July";
                        break;
                    case 8:
                        month = "August";
                        break;
                    case 9:
                        month =  "September";
                        break;
                    case 10:
                        month = "October";
                        break;
                    case 11:
                        month = "November";
                        break;
                    case 12:
                        month = "December";
                        break;
                    default:
                        break;
                }
                TextBoxMonth.Text = month;

                CanUpdate = false;
                IsExcess = false;
                BindSections(DropDownListSection);
                TextBoxRemitanceDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                
            }
            BindGridView();
        }

        private void BindSections(DropDownList DropDownListSection)
        {
            DropDownListSection.Items.Clear();
            DropDownListSection.Items.Add(new ListItem("--Select Section--", "-1"));
            DropDownListSection.DataSource = CHNLSVC.Financial.GetSection();
            DropDownListSection.DataTextField = "rss_desc";
            DropDownListSection.DataValueField = "rss_cd";
            DropDownListSection.DataBind();
            DropDownListSection.Items.Remove(new ListItem("Remittance Details", "05"));
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Financial_Modules/RemSummaryAdjesment.aspx", false);
        }

        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            #region validation

            decimal tem;

            if (DropDownListRemitType.SelectedValue == "-1")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select remitance type");
                return;
            }
            if (DropDownListSection.SelectedValue == "-1")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select remitance section");
                return;
            }
            if (TextBoxFinalAmount.Text == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter final amount");
                return;
            }
            if (!decimal.TryParse(TextBoxFinalAmount.Text, out tem))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter final amount in correctly");
                return;
            }
            ;
            if (CHNLSVC.Financial.IsPeriodClosed(GlbUserComCode, GlbUserDefProf, "FIN_REM", Convert.ToDateTime(TextBoxRemitanceDate.Text)))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Period Close");
                return;
            }


            #endregion

            if (IsExcess)
            {
                if ( Convert.ToDecimal(TextBoxFinalAmount.Text) > Convert.ToDecimal(LabelExcess.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Fine charge can not exceed Pre. Day Excess amount");
                    return;
                }
                //insert new record
                RemitanceSummaryDetail _remDet = new RemitanceSummaryDetail();
                _remDet.Rem_dt = Convert.ToDateTime(TextBoxRemitanceDate.Text).Date;
                decimal week = 0;
                CHNLSVC.General.GetWeek(Convert.ToDateTime(TextBoxRemitanceDate.Text).Date, out week);
                _remDet.Rem_week = week.ToString();
                _remDet.Rem_com = GlbUserComCode;
                _remDet.Rem_pc = GlbUserDefProf;
                _remDet.Rem_cre_by = GlbUserName;
                _remDet.Rem_cre_dt = DateTime.Now;
                _remDet.Rem_rmk = TextBoxFinalComment.Text;
                _remDet.Rem_rmk_fin = TextBoxFinalComment.Text;
                _remDet.Rem_val = Convert.ToDecimal(TextBoxFinalAmount.Text);
                _remDet.Rem_val_final = Convert.ToDecimal(TextBoxFinalAmount.Text);
                //TODO: Need section and type codes
                _remDet.Rem_sec = DropDownListSection.SelectedValue;
                _remDet.Rem_cd = DropDownListRemitType.SelectedValue;
                _remDet.Rem_sh_desc = DropDownListRemitType.SelectedItem.Text;
                _remDet.Rem_lg_desc = DropDownListRemitType.SelectedItem.Text;
                CanUpdate = false;
                IsExcess = false;

                int result = CHNLSVC.Financial.SaveRemSummaryDetails(_remDet);
                if (result > 0)
                {
                    string Msg = "<script>alert('Record Insert Sucessfully!!');window.location = 'RemSummaryAdjesment.aspx';</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                }
                else
                {
                    string Msg = "<script>alert('Error occurded while processing!!')</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                }
            }
            else if (CanUpdate)
            {

                RemitanceSummaryDetail _remDet = new RemitanceSummaryDetail();
                _remDet.Rem_com = GlbUserComCode;
                _remDet.Rem_pc = GlbUserDefProf;
                _remDet.Rem_dt = Convert.ToDateTime(TextBoxRemitanceDate.Text).Date;
                _remDet.Rem_sec = DropDownListSection.SelectedValue;
                _remDet.Rem_cd = DropDownListRemitType.SelectedValue;
                _remDet.Rem_val_final = Convert.ToDecimal(TextBoxFinalAmount.Text);
                _remDet.Rem_rmk_fin = TextBoxFinalComment.Text;
                _remDet.Rem_val = Convert.ToDecimal(TextBoxOriginalAmount.Text);
                _remDet.Rem_rmk = TextBoxOriginalComment.Text;

                int result = CHNLSVC.Financial.UpdateRemitanceAdjusment(_remDet);
                if (result > 0)
                {
                    string Msg = "<script>alert('Record Updated Sucessfully!!');window.location = 'RemSummaryAdjesment.aspx';</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                }
                else
                {
                    string Msg = "<script>alert('Error occurded while processing!!')</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                }
                //set bool value
                CanUpdate = false;
            }
            else {
                //insert new record
                RemitanceSummaryDetail _remDet = new RemitanceSummaryDetail();
                _remDet.Rem_dt = Convert.ToDateTime(TextBoxRemitanceDate.Text).Date;
                decimal week=0; 
                 CHNLSVC.General.GetWeek(Convert.ToDateTime(TextBoxRemitanceDate.Text).Date, out week);
                 _remDet.Rem_week = week.ToString();
                _remDet.Rem_cre_by = GlbUserName;
                _remDet.Rem_cre_dt = DateTime.Now;
                _remDet.Rem_rmk_fin = TextBoxFinalComment.Text;
                _remDet.Rem_val_final = Convert.ToDecimal(TextBoxFinalAmount.Text);
                _remDet.Rem_sec = DropDownListSection.SelectedValue;
                _remDet.Rem_cd = DropDownListRemitType.SelectedValue;
                _remDet.Rem_com = GlbUserComCode;
                _remDet.Rem_pc = GlbUserDefProf;
                _remDet.Rem_sh_desc = DropDownListRemitType.SelectedItem.Text;
                _remDet.Rem_lg_desc = DropDownListRemitType.SelectedItem.Text;
                CanUpdate = false;
                IsExcess = false;

                int result = CHNLSVC.Financial.SaveRemSummaryDetails(_remDet);
                if (result > 0)
                {
                    string Msg = "<script>alert('Record Insert Sucessfully!!');window.location = 'RemSummaryAdjesment.aspx';</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                }
                else
                {
                    string Msg = "<script>alert('Error occurded while processing!!')</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                }
            }
        }
          
        protected void DropDownListSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRemitTypes(DropDownListSection.SelectedValue);
        }

        private void LoadRemitTypes(string _sec)
        {
            DropDownListRemitType.Items.Clear();
            DropDownListRemitType.Items.Add(new ListItem("--Select Type--", "-1"));
            DropDownListRemitType.DataSource = CHNLSVC.Financial.get_rem_type_by_sec(_sec, 0);
            DropDownListRemitType.DataTextField = "rsd_desc";
            DropDownListRemitType.DataValueField = "rsd_cd";
            DropDownListRemitType.DataBind();
        }

        protected void DropDownListRemitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //insert excess process
            //section cd Summary and rem type Fine Charges 
            //Remitance date has to equal DateTime.Now
            if (DropDownListSection.SelectedValue == "03" && DropDownListRemitType.SelectedValue == "027")
            {
                //today
                if (Convert.ToDateTime(TextBoxRemitanceDate.Text).Date == DateTime.Now.Date)
                {
                    DivViewAmo.Visible = true;
                    decimal value = 0;
                    CHNLSVC.Financial.GetPrvDayExcess(DateTime.Now, GlbUserDefProf, out value);
                    LabelExcess.Text = value.ToString();
                    TextBoxOriginalAmount.Text = "";
                    TextBoxFinalAmount.Text = value.ToString();
                    IsExcess = true;
                    CanUpdate = true;
                }
                //back date
                else if(CHNLSVC.General.IsAllowBackDate(GlbUserComCode,GlbUserDefLoca,GlbUserDefProf,TextBoxRemitanceDate.Text)){
                    DivViewAmo.Visible = true;
                    decimal value = 0;
                    CHNLSVC.Financial.GetPrvDayExcess(Convert.ToDateTime(TextBoxRemitanceDate.Text), GlbUserDefProf, out value);
                    LabelExcess.Text = value.ToString();

                    TextBoxOriginalAmount.Text = "";
                    TextBoxFinalAmount.Text = value.ToString();
                    IsExcess = true;
                    CanUpdate = true;
                }
            }
            //update process
            else
            {
                if (!(DropDownListSection.SelectedValue == "03" && DropDownListRemitType.SelectedValue == "027"))
                {
                    RemitanceSummaryDetail _remDet = CHNLSVC.Financial.GetRemitanceAdjesmentDetails(GlbUserComCode, GlbUserDefProf, Convert.ToDateTime(TextBoxRemitanceDate.Text), DropDownListSection.SelectedValue, DropDownListRemitType.SelectedValue);
                    if (_remDet != null)
                    {
                        CanUpdate = true;
                        TextBoxOriginalAmount.Text = _remDet.Rem_val.ToString();
                        TextBoxFinalAmount.Text = _remDet.Rem_val_final.ToString();
                        TextBoxFinalComment.Text = _remDet.Rem_rmk_fin;
                        TextBoxOriginalComment.Text = _remDet.Rem_rmk;
                    }
                    else
                    {
                        CanUpdate = false;
                        TextBoxOriginalAmount.Text = "";
                        TextBoxFinalAmount.Text = "";
                        TextBoxOriginalComment.Text = "";
                        TextBoxFinalComment.Text = "";
                    }
                }
                DivViewAmo.Visible = false;
                TextBoxOriginalComment.Enabled = false;
                TextBoxOriginalAmount.Enabled = false;
                IsExcess = false;
            }
        }

        private void BindGridView()
        {
            List<RemitanceSummaryDetail> _remsumdetTem = CHNLSVC.Financial.GetRemitanceSumDetailAdjusment(Convert.ToDateTime(TextBoxRemitanceDate.Text), GlbUserDefProf);
            if (_remsumdetTem != null)
            {
                List<RemitanceSummaryDetail> _remsumdet = new List<RemitanceSummaryDetail>();
                //loop for remove section 5 ,records
                foreach (RemitanceSummaryDetail _rem in _remsumdetTem)
                {
                    if (_rem.Rem_sec != "05") {
                        _remsumdet.Add(_rem);
                    }
                }
                gvRemLimit.DataSource = _remsumdet;
                gvRemLimit.DataBind();
            }
            else
            {
                BindEmptyData();
            }
        }

        private void BindEmptyData()
        {
            gvRemLimit.DataSource = CHNLSVC.Financial.GetRemSumLimitations("", "", "", "");
            gvRemLimit.DataBind();
        }

        protected void LinkButtonView_Click(object sender, EventArgs e)
        {
            DivViewAmo.Visible = false;
            IsExcess = false;
            CanUpdate = false;
            TextBoxOriginalComment.Enabled = false;
            TextBoxOriginalAmount.Enabled = false;
            BindGridView();
            BindSections(DropDownListSection);
            LoadRemitTypes(DropDownListSection.SelectedValue);
            TextBoxOriginalAmount.Text = "";
            TextBoxFinalAmount.Text = "";
            TextBoxOriginalComment.Text = "";
            TextBoxFinalComment.Text = "";
        }

        protected void Finalize_Click(object sender, EventArgs e)
        {
            RemitanceStatus _remStaus = new RemitanceStatus();
            _remStaus.Gpac_com = GlbUserComCode;
            _remStaus.Gpac_pc = GlbUserDefProf;
            int month = 0;
            switch (TextBoxMonth.Text) { 
                case "January":
                    month = 1;
                    break;
                case "February":
                    month = 2;
                    break;
                case "March":
                    month=3;
                    break;
                case "April":
                    month = 4;
                    break;
                case "May":
                    month = 5;
                    break;
                case "June":
                    month = 6;
                    break;
                case "July":
                    month = 7;
                    break;
                case "August":
                    month = 8;
                    break;
                case "September":
                    month = 9;
                    break;
                case "October":
                    month = 10;
                    break;
                case "November":
                    month=11;
                    break;
                case "December":
                    month = 12;
                    break;
                default:
                    break;
            }
            
            

            DateTime _date = new DateTime(Convert.ToInt32(TextBoxYear.Text),month,1);
            
            _remStaus.Gpac_stus = true;
            _remStaus.Gpac_cre_by = GlbUserName;
            _remStaus.Gpac_cre_dt = DateTime.Now;
            _date=_date.AddDays(DateTime.DaysInMonth(_date.Year,_date.Month)-1);
            _remStaus.Gpac_op_dt = _date.Date;
            _remStaus.Gpac_tp = "FIN_REM";

            int result = CHNLSVC.Financial.SaveRemitanceStatus(_remStaus);

            if (result > 0)
            {
                string Msg = "<script>alert('Sucessfully Finalize!!');window.location = 'RemSummaryAdjesment.aspx';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            else
            {
                string Msg = "<script>alert('Error occurded while processing!!')</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
        }
    }
}