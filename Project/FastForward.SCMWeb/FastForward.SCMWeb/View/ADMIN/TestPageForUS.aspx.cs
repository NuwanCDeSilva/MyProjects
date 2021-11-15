using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.ADMIN
{
    public partial class TestPageForUS : Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            //ucPaymodes.CustomerCode = "CONT-669809";
            if (!IsPostBack)
            {
                loadPaymenttypes();
                ucPaymodes.TotalAmount = Convert.ToDecimal("1000.00");
                ucPaymodes.PaidAmountLabel.Text = "0.00";
                ucPaymodes.InvoiceType = "DEBT";
                ucPaymodes.Customer_Code = "ABCR1S109";
            }
        }

        private void loadPaymenttypes()
        {
            //ucPaymodes.PayMode.Items.Clear();
            //BasePage basePage = new BasePage();
            //List<PaymentType> _paymentTypeRef = basePage.CHNLSVC.Sales.GetPossiblePaymentTypes_new("ABL", "PC", "PC", "DEBT", DateTime.Now);
            //List<string> payTypes = new List<string>();
            ////payTypes.Add("");
            //if (_paymentTypeRef != null && _paymentTypeRef.Count > 0)
            //{
            //    foreach (PaymentType pt in _paymentTypeRef)
            //    {
            //        payTypes.Add(pt.Stp_pay_tp);
            //    }
            //}
            //ucPaymodes.PayMode.DataSource = payTypes;
            //ucPaymodes.PayMode.DataBind();
            //ucPaymodes.PayMode.SelectedIndex = 0;

            ////if (payTypes[0].ToString().ToUpper() == "CASH")
            ////{
            ////    mltPaymentDetails.ActiveViewIndex = 0;
            ////}
            ////else if (payTypes[0].ToString().ToUpper() == "CRCD")
            ////{
            ////    mltPaymentDetails.ActiveViewIndex = 1;
            ////}
            ////else if (payTypes[0].ToString().ToUpper() == "CHEQUE")
            ////{
            ////    mltPaymentDetails.ActiveViewIndex = 2;
            ////}

            ucPaymodes.PayModeCombo.Items.Insert(0, new ListItem(" - - Select - - ", "0"));
            ucPaymodes.PayModeCombo.Items.Insert(1, new ListItem("CASH", "CASH"));
            ucPaymodes.PayModeCombo.Items.Insert(2, new ListItem("ADVAN", "ADVAN"));
            ucPaymodes.PayModeCombo.Items.Insert(3, new ListItem("CRCD", "CRCD"));
            ucPaymodes.PayModeCombo.Items.Insert(4, new ListItem("CRNOTE", "CRNOTE"));
            ucPaymodes.PayModeCombo.Items.Insert(5, new ListItem("CHEQUE", "CHEQUE"));
            ucPaymodes.PayModeCombo.Items.Insert(6, new ListItem("GVO", "GVO"));
            ucPaymodes.PayModeCombo.Items.Insert(7, new ListItem("GVS", "GVS"));
            ucPaymodes.PayModeCombo.Items.Insert(8, new ListItem("LORE", "LORE"));
            ucPaymodes.PayModeCombo.Items.Insert(9, new ListItem("BANKSLIP", "BANK_SLIP"));
            ucPaymodes.PayModeCombo.Items.Insert(10, new ListItem("STAR", "STAR_PO"));
            ucPaymodes.PayModeCombo.SelectedValue = "0";

            

        }

        //try
        //{
        //    pnlDef.Visible = false;
        //    pnlCash.Visible = false;
        //    PnlAdvan.Visible = false;
        //    pnlCrcd.Visible = false;
        //    pnlCrnote.Visible = false;
        //    pnlCheque.Visible = false;
        //    pnlGvo.Visible = false;
        //    pnlGvs.Visible = false;
        //    pnlLore.Visible = false;
        //    pnlBankSlip.Visible = false;
        //    pnlStar.Visible = false;

        //    if (ddlPayMode.SelectedValue == "0")
        //        pnlDef.Visible = true;
        //    if (ddlPayMode.SelectedValue == "CASH")
        //        pnlCash.Visible = true;
        //    if (ddlPayMode.SelectedValue == "ADVAN")
        //        PnlAdvan.Visible = true;
        //    if (ddlPayMode.SelectedValue == "CRCD")
        //        pnlCrcd.Visible = true;
        //    if (ddlPayMode.SelectedValue == "CRNOTE")
        //        pnlCrnote.Visible = true;
        //    if (ddlPayMode.SelectedValue == "CHEQUE")
        //        pnlCheque.Visible = true;
        //    if (ddlPayMode.SelectedValue == "GVO")
        //        pnlGvo.Visible = true;
        //    if (ddlPayMode.SelectedValue == "GVS")
        //        pnlGvs.Visible = true;
        //    if (ddlPayMode.SelectedValue == "LORE")
        //        pnlLore.Visible = true;
        //    if (ddlPayMode.SelectedValue == "BANKSLIP")
        //        pnlBankSlip.Visible = true;
        //    if (ddlPayMode.SelectedValue == "STAR")
        //        pnlStar.Visible = true;
        //}
        //catch (Exception ex)
        //{
        //    lblWarning.Text = "Error Occurred while processing...  " + ex;
        //    divWarning.Visible = true;
        //}
    }
}