using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;

namespace FF.AbansTours.UserControls
{
    public partial class uc_CostingSheet : System.Web.UI.UserControl
    {
        BasePage _basepage = null;
        List<QUO_COST_HDR> oHeader = null;
        List<QUO_COST_DET> oMainItems = null;

        public AjaxControlToolkit.ModalPopupExtender mpEnquiryOut
        {
            get { return this.mpEnquiry; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Convert.ToString(Session["UserID"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserCompanyCode"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserDefProf"])) &&
                      !String.IsNullOrEmpty(Convert.ToString(Session["UserDefLoca"])))
                {
                    if (!IsPostBack)
                    {
                        _basepage = new BasePage();
                        if (Session["CustCode"] != null)
                        {
                            lblClient.Text = Session["CustCode"].ToString();
                        }
                        loadCostCate();
                        clearALl();
                    }
                }
                else
                {
                    //string gotoURL = "http://" + System.Web.HttpContext.Current.Request.Url.Host + @"/loginNew.aspx";
                    string gotoURL = "login.aspx";
                    Response.Write("<script>window.open('" + gotoURL + "','_parent');</script>");
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        protected void btnClose_Click(object sender, ImageClickEventArgs e)
        {
            mpEnquiry.Hide();
        }

        protected void btnAddtoGrid_Click(object sender, ImageClickEventArgs e)
        {
            if (oMainItems == null)
            {
                oMainItems = new List<QUO_COST_DET>();
            }
            QUO_COST_DET oItem = new QUO_COST_DET();
            oItem.QCD_SEQ = 0;
            oItem.QCD_COST_NO = string.Empty;
            oItem.QCD_CAT = ddlCostType.SelectedValue.ToString();
            oItem.QCD_SUB_CATE = string.Empty;
            oItem.QCD_DESC = txtCostSubType.Text;
            oItem.QCD_CURR = "USD";
            oItem.QCD_EX_RATE = 100;
            oItem.QCD_QTY = Convert.ToInt32(txtPAX.Text);
            oItem.QCD_UNIT_COST = Convert.ToDecimal(txtUSD.Text);
            oItem.QCD_TAX = Convert.ToDecimal(txtTAX.Text);
            oItem.QCD_TOT_COST = oItem.QCD_UNIT_COST * oItem.QCD_QTY;
            oItem.QCD_TOT_LOCAL = oItem.QCD_TOT_COST * oItem.QCD_EX_RATE;
            oItem.QCD_MARKUP = 0;
            oItem.QCD_AF_MARKUP = oItem.QCD_TOT_LOCAL - (oItem.QCD_TOT_LOCAL * oItem.QCD_MARKUP);
            oItem.QCD_RMK = txtRemark.Text;
            oMainItems.Add(oItem);
            mpEnquiry.Show();
            bindData();
        }

        private void DisplayMessages(string message)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnTexxt_Click(object sender, EventArgs e)
        {
        }

        private void loadCostCate()
        {
            List<MST_COST_CATE> oCate = _basepage.CHNLSVC.Tours.GET_COST_CATE(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
            ddlCostType.DataSource = oCate;
            ddlCostType.DataTextField = "MCC_DESC";
            ddlCostType.DataValueField = "MCC_CD";
            ddlCostType.DataBind();
        }

        private void clearALl()
        {
            oHeader = new List<QUO_COST_HDR>();
            oMainItems = new List<QUO_COST_DET>();
        }

        private void bindData()
        {
            if (oMainItems.Count > 0)
            {
                dgvCostSheet.DataSource = oMainItems;
                dgvCostSheet.DataBind();
            }
        }

        private bool validateADd()
        {
            bool status = true;

            if (String.IsNullOrEmpty(txtReffNum.Text))
            {
                DisplayMessages("Enter Reference Number");
                status = false;
                txtReffNum.Focus();
                return status;
            }

            decimal asd1;
            if (String.IsNullOrEmpty(txtPAX.Text) && decimal.TryParse(txtPAX.Text, out asd1))
            {
                DisplayMessages("Enter Reference Number");
                status = false;
                txtPAX.Focus();
                return status;
            }
            if (String.IsNullOrEmpty(txtCostSubType.Text))
            {
                DisplayMessages("Sub Type");
                status = false;
                txtCostSubType.Focus();
                return status;
            }

            if (String.IsNullOrEmpty(txtUSD.Text) && decimal.TryParse(txtUSD.Text, out asd1))
            {
                DisplayMessages("Enter Fare USD");
                status = false;
                txtUSD.Focus();
                return status;
            }
            if (String.IsNullOrEmpty(txtTAX.Text) && decimal.TryParse(txtTAX.Text, out asd1))
            {
                DisplayMessages("Enter TAX");
                status = false;
                txtTAX.Focus();
                return status;
            }
            if (String.IsNullOrEmpty(txtTotal.Text) && decimal.TryParse(txtTotal.Text, out asd1))
            {
                DisplayMessages("Enter Total");
                status = false;
                txtTotal.Focus();
                return status;
            }
            if (String.IsNullOrEmpty(txtTotalLKR.Text) && decimal.TryParse(txtTotalLKR.Text, out asd1))
            {
                DisplayMessages("Enter Total(LKR)");
                status = false;
                txtTotalLKR.Focus();
                return status;
            }
            if (String.IsNullOrEmpty(txtRemark.Text) )
            {
                DisplayMessages("Enter remark");
                status = false;
                txtRemark.Focus();
                return status;
            }

            return status;
        }

    }
}