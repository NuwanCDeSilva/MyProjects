using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Globalization;
using System.Transactions;
using FF.WebERPClient.UserControls;

namespace FF.WebERPClient.HP_Module
{
    public partial class SchemeCreation : BasePage
    {

        protected HpSchemeType _HPSchemeType
        {
            get { return (HpSchemeType)Session["_HPSchemeType"]; }
            set { Session["_HPSchemeType"] = value; }
        }

        protected HpSchemeDetails _HPSchemeDetails
        {
            get { return (HpSchemeDetails)Session["_HPSchemeDetails"]; }
            set { Session["_HPSchemeDetails"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlCate.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnSchCate, ""));
                ddlType.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnSchTP, ""));
                txtTerm.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnTerm, ""));

                _HPSchemeType = new HpSchemeType();
                _HPSchemeDetails = new HpSchemeDetails();
                Clear_Data();
            }
        }

        protected void imgNewType_Click(object sender, ImageClickEventArgs e)
        {
            btnSchSave.Enabled = true;
            txtType.Text = "";
            txtDesc.Text = "";
            txtAnnualRate.Text = "";
            txtType.Focus();
        }

        protected void Clear_Data()
        {
            LoadCate(ddlCate);
            btnSchSave.Enabled = false;
            txtType.Text = "";
            txtDesc.Text = "";
            txtAnnualRate.Text = "";
            txtTerm.Text = "";
            txtSchCode.Text = "";
            txtSchDesc.Text = "";
            chkGroup.Checked = false;
            chkInsu.Checked = false;
            txtIntRt.Text = "";
            txtAddrent.Text = "";
            divShedule.Visible = false;
        }


        private void LoadCate(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem(string.Empty, "-1"));
            ddl.DataSource = CHNLSVC.Sales.GetSAllchemeCategoryies(string.Empty);
            ddl.DataTextField = "hsc_desc";
            ddl.DataValueField = "hsc_cd";
            ddl.DataBind();
        }

        private void LoadType(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem(string.Empty, "-1"));
            ddl.DataSource = CHNLSVC.Sales.GetSchemeTypeByCategory(ddlCate.SelectedValue);
            ddl.DataTextField = "hst_desc";
            ddl.DataValueField = "hst_cd";
            ddl.DataBind();
        }

        private void LoadSchemeTypeDetails(string _tpCD)
        {
            _HPSchemeType = new HpSchemeType();

            _HPSchemeType = CHNLSVC.Sales.getSchemeType(_tpCD);

            if (_HPSchemeType != null)
            {
                txtType.Text = _HPSchemeType.Hst_cd;
                txtDesc.Text = _HPSchemeType.Hst_desc;
                txtAnnualRate.Text = _HPSchemeType.Hst_def_intr.ToString();
                chkActive.Checked = _HPSchemeType.Hst_act;
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid scheme type.");
                txtType.Text = "";
                txtDesc.Text = "";
                txtAnnualRate.Text = "";
                chkActive.Checked = false;
                return;
            }
        }

        protected void CheckExsistTerm(object sender, EventArgs e)
        {
            Int32 _term = 0;
            decimal _intRt = 0;
            _HPSchemeDetails = new HpSchemeDetails();

            if ((!string.IsNullOrEmpty(txtTerm.Text)) && (!string.IsNullOrEmpty(txtType.Text)))
            {
                _term =Convert.ToInt32(txtTerm.Text);

                txtSchCode.Text = txtType.Text + "" + _term.ToString("00");

                // load scheme details if exsisting scheme
                _HPSchemeDetails = CHNLSVC.Sales.getSchemeDetByCode(txtSchCode.Text.Trim());

                if (_HPSchemeDetails.Hsd_cd != null)
                {
                    chkInsu.Checked = _HPSchemeDetails.Hsd_has_insu;
                    chkGroup.Checked = _HPSchemeDetails.Hsd_alw_gs;
                    txtSchDesc.Text = _HPSchemeDetails.Hsd_desc;
                    txtIntRt.Text = _HPSchemeDetails.Hsd_intr_rt.ToString();
                    txtAddrent.Text = _HPSchemeDetails.Hsd_noof_addrnt.ToString();
                }
                else
                {
                    chkInsu.Checked = false;
                    chkGroup.Checked = false;
                    _intRt = Convert.ToDecimal(txtAnnualRate.Text) / 12 * Convert.ToDecimal(txtTerm.Text);
                    txtIntRt.Text = _intRt.ToString("0.0000");
                    txtAddrent.Text = "";
                    txtSchDesc.Text = txtDesc.Text + " " + txtTerm.Text + " months";
                }
                

            }
        }

        protected void ddlCate_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadType(ddlType);
        }

        protected void LoadSchTP(object sender, EventArgs e)
        {
            LoadType(ddlType);
        }

        protected void LoadSchTPDetails(object sender, EventArgs e)
        {
            LoadSchemeTypeDetails(ddlType.SelectedValue);
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSchemeTypeDetails(ddlType.SelectedValue);
        }

        protected void chkUsrDef_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUsrDef.Checked == true)
            {
                divShedule.Visible = true;
            }
            else
            {
                divShedule.Visible = false;
            }
        }
    }
}