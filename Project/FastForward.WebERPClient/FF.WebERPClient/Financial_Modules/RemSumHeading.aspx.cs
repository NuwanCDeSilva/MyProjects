using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;

namespace FF.WebERPClient.Financial_Modules
{
    public partial class RemSumHeading : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
                BindEmptyData();
                txtCode.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnCode, ""));
            }
        }

        private void BindData()
        {
            ddlSec.Items.Clear();
            ddlSec.Items.Add(new ListItem("--Select Section--", "-1"));
            ddlSec.DataSource = CHNLSVC.Financial.GetSection();
            ddlSec.DataTextField = "rss_desc";
            ddlSec.DataValueField = "rss_cd";
            ddlSec.DataBind();

            ddlSecDef.Items.Clear();
            ddlSecDef.Items.Add(new ListItem("--Select Section--", "-1"));
            ddlSecDef.DataSource = CHNLSVC.Financial.GetSection();
            ddlSecDef.DataTextField = "rss_desc";
            ddlSecDef.DataValueField = "rss_cd";
            ddlSecDef.DataBind();

            ddlPtyTp.Items.Clear();
            ddlPtyTp.Items.Add(new ListItem("--Select Type--", "-1"));
            ddlPtyTp.DataSource = CHNLSVC.Sales.get_hierarchy("PC");
            ddlPtyTp.DataTextField = "msph_tp";
            ddlPtyTp.DataValueField = "msph_tp";
            ddlPtyTp.DataBind();
        }

        private void LoadPartyCodes()
        {
            ddlPtyCd.Items.Clear();
            ddlPtyCd.Items.Add(new ListItem("--Select Code--", "-1"));
            ddlPtyCd.DataSource = CHNLSVC.Sales.get_pc_info_by_code(ddlPtyTp.SelectedValue);
            ddlPtyCd.DataTextField = "mpi_val";
            ddlPtyCd.DataValueField = "mpi_val";
            ddlPtyCd.DataBind();
        }

        protected void ddlPtyTp_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPartyCodes();
        }

        private void LoadRemitTypes(string _sec)
        {
            ddlRemTp.Items.Clear();
            ddlRemTp.Items.Add(new ListItem("--Select Type--", "-1"));
            ddlRemTp.DataSource = CHNLSVC.Financial.get_rem_type_by_sec(_sec,2);
            ddlRemTp.DataTextField = "rsd_desc";
            ddlRemTp.DataValueField = "rsd_cd";
            ddlRemTp.DataBind();
        }

        protected void ddlSecDef_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRemitTypes(ddlSecDef.SelectedValue);
        }

        protected void ddlRemTp_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRemSumLimitation();
        }

        protected void ddlPtyCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRemSumLimitation();
        }
        private void BindEmptyData()
        {
            gvRemSumHead.DataSource = CHNLSVC.Financial.GetRemSumHeadingBySec("");
            gvRemSumHead.DataBind();

            gvRemLimit.DataSource = CHNLSVC.Financial.GetRemSumLimitations("", "", "", "");
            gvRemLimit.DataBind();

            ddlPtyCd.Items.Clear();
            ddlPtyCd.Items.Add(new ListItem("--Select Code--", "-1"));

            ddlRemTp.Items.Clear();
            ddlRemTp.Items.Add(new ListItem("--Select Type--", "-1"));
        }

        protected void ddlSec_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRemSumHeading();
            txtCode.Text = "";
            txtDesc.Text = "";
        }

        protected void GetRemData(object sender, EventArgs e)
        {
            RemitanceSumHeading _remsumhead = null;
            if (txtCode.Text == "")
            {
                txtDesc.Text = "";
                return;
            }
            _remsumhead = CHNLSVC.Financial.GetRemitanceData(ddlSec.SelectedValue, txtCode.Text);
            if (_remsumhead != null)
            {
                txtDesc.Text = _remsumhead.Rsd_desc;
                chkStatus.Checked = (Convert.ToString(_remsumhead.Rsd_stus) == "A") ? true : false;
            }
            else
            {
                txtDesc.Text = "";
                chkStatus.Checked = false;
            }

        }

        protected void btnSaveLimit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlRemTp.SelectedValue.Equals("-1"))
                {
                    throw new UIValidationException("Please select Remitance");
                }
                if (ddlSecDef.SelectedValue.Equals("-1"))
                {
                    throw new UIValidationException("Please select Section");
                }
                if (ddlPtyCd.SelectedValue.Equals("-1"))
                {
                    throw new UIValidationException("Please select the party code");
                }
                if (ddlPtyTp.SelectedValue.Equals("-1"))
                {
                    throw new UIValidationException("Please select the party type");
                }

                RemSumDefinitions _remSumLimit = new RemSumDefinitions();
                _remSumLimit.Rsmd_pty_tp = ddlPtyTp.SelectedValue;
                _remSumLimit.Rsmd_pty_cd = ddlPtyCd.SelectedValue;
                _remSumLimit.Rsmd_from_dt = Convert.ToDateTime(txtFrom.Text);
                _remSumLimit.Rsmd_to_dt = Convert.ToDateTime(txtTo.Text);
                _remSumLimit.Rsmd_sec = ddlSecDef.SelectedValue;
                _remSumLimit.Rsmd_cd = ddlRemTp.SelectedValue;
                _remSumLimit.Rsmd_max_val = Convert.ToDecimal(txtVal.Text);
                _remSumLimit.Rsmd_cre_by = GlbUserName;
                _remSumLimit.Rsmd_cre_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                _remSumLimit.Rsmd_mod_by = GlbUserName;
                _remSumLimit.Rsmd_mod_dt = Convert.ToDateTime(DateTime.Now.Date).Date;

                int row_aff = CHNLSVC.Financial.SaveRemSumLimitations(_remSumLimit);
                BindRemSumLimitation();

                string Msg = "<script>alert('Successfully Updated!');window.location = 'RemSumHeading.aspx';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                ddlRemTp.SelectedIndex = 0;

            }
            catch (UIValidationException ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.ErrorMessege);
            }
            catch (Exception e1)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, e1.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (ddlSec.SelectedValue.Equals("-1"))
                {
                    throw new UIValidationException("Please select section");
                }
                if (txtCode.Text == "")
                {
                    throw new UIValidationException("Enter the code");
                }
                if (CHNLSVC.Financial.IsRemHeadFixed(ddlSec.SelectedValue, txtCode.Text) == true)
                {
                    throw new UIValidationException("Fixed Heading cannot be changed !");
                }
                if (txtDesc.Text == "")
                {
                    throw new UIValidationException("Enter the description");
                }

                RemitanceSumHeading _remSumHead = new RemitanceSumHeading();
                _remSumHead.Rsd_sec = ddlSec.SelectedValue;
                _remSumHead.Rsd_cd = txtCode.Text;
                _remSumHead.Rsd_desc = txtDesc.Text;
                _remSumHead.Rsd_fixed = 0;
                _remSumHead.Rsd_acc = "";
                _remSumHead.Rsd_cre_by = GlbUserName;
                _remSumHead.Rsd_cre_when = Convert.ToDateTime(DateTime.Now.Date).Date;
                _remSumHead.Rsd_mod_by = GlbUserName;
                _remSumHead.Rsd_mod_when = Convert.ToDateTime(DateTime.Now.Date).Date;
                _remSumHead.Rsd_stus = (chkStatus.Checked == true) ? "A" : "D";

                int row_aff = CHNLSVC.Financial.SaveRemSumHeading(_remSumHead);

                LoadRemSumHeading();
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Updated.");

                txtCode.Text = "";
                txtDesc.Text = "";
                chkStatus.Checked = false;
                txtCode.Focus();

            }
            catch (UIValidationException ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.ErrorMessege);
            }
            catch (Exception e1)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, e1.Message);
            }
        }


        protected void gvRemSumHead_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRemSumHead.PageIndex = e.NewPageIndex;
            this.LoadRemSumHeading();
        }

        protected void gvRemLimit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRemLimit.PageIndex = e.NewPageIndex;
            this.LoadRemSumHeading();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        protected void btnCloseLimit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        private void LoadRemSumHeading()
        {
            gvRemSumHead.DataSource = CHNLSVC.Financial.GetRemSumHeadingBySec(ddlSec.SelectedValue);
            gvRemSumHead.DataBind();

        }

        private void BindRemSumLimitation()
        {
            gvRemLimit.DataSource = CHNLSVC.Financial.GetRemSumLimitations(ddlPtyTp.SelectedValue, ddlPtyCd.SelectedValue, ddlSecDef.SelectedValue, ddlRemTp.SelectedValue);
            gvRemLimit.DataBind();

        }
    }
}