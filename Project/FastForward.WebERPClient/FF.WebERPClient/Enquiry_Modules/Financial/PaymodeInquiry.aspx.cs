using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using FF.BusinessObjects;
using System.Drawing;

namespace FF.WebERPClient.Enquiry_Modules.Financial
{
    public partial class PaymodeInquiry : BasePage
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            txtDocNo.Attributes.Add("onkeypress", "return fun1(event,'" + btnGetDocDet.ClientID + "')");
            txtDocNo.Attributes.Add("onblur", "return onblurFire(event,'" + btnGetDocDet.ClientID + "')");

            if (!IsPostBack)
            {
                clearGrids();
                List<PaymentTypeRef> paymodes = CHNLSVC.Sales.GetAllPaymentType(GlbUserComCode, GlbUserDefProf, string.Empty);
                if (paymodes != null)
                {
                    foreach (PaymentTypeRef pt in paymodes)
                    {
                        ddlPayMode.Items.Add(new ListItem(pt.Sapt_desc, pt.Sapt_cd));
                    }

                }
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Enquiry_Modules/Financial/PaymodeInquiry.aspx");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }
        private void clearGrids()
        {
            DataTable dt = new DataTable();
            grvPopUpSelect.DataSource = dt;
            grvPopUpSelect.DataBind();

            grvPaymentDocDet.DataSource = dt;
            grvPaymentDocDet.DataBind();

            grvInvoiceDet.DataSource = dt;
            grvInvoiceDet.DataBind();

            grvReceptDet.DataSource = dt;
            grvReceptDet.DataBind();

            Panel_RtnChq.Visible = false;

        }
        protected void btnGetDocDet_Click(object sender, EventArgs e)
        {
            clearGrids();
            string payType = ddlPayMode.SelectedValue;
            DataTable dt = new DataTable();
            try
            {
                dt = CHNLSVC.Sales.GetPaymodeDetail(GlbUserComCode, payType, txtDocNo.Text.Trim(), string.Empty, string.Empty);
                grvPopUpSelect.DataSource = dt;
                grvPopUpSelect.DataBind();
               // ModalPopupExtItem.Show();
            }
            catch (Exception ex)
            {

            }
            
            if (dt != null)
            {
                if (dt.Rows.Count > 1)
                {
                    ModalPopupExtItem.Show();//Show ModalPopUp to select one record.
                }
                else if (dt.Rows.Count==1)
                {
                    grvPaymentDocDet.DataSource = dt;
                    grvPaymentDocDet.DataBind();
                    //-------------------------------
                    string docType = ddlPayMode.SelectedValue;
                    string DocNo = txtDocNo.Text.Trim();

                    string PC = string.Empty;
                    string date = string.Empty;
                    string bankCd = string.Empty;
                    string cardType = string.Empty;
                    GridViewRow row = grvPaymentDocDet.Rows[0];
                    if (docType == "CHEQUE" || docType == "CRCD")
                    {
                        PC = row.Cells[0].Text.Trim();
                        date = row.Cells[1].Text.Trim();
                        bankCd = row.Cells[2].Text.Trim();
                        if (docType == "CRCD")
                        {
                            cardType = row.Cells[3].Text.Trim();
                        }
                        if (docType == "CHEQUE")
                        {
                            DataTable dt_retCheques = CHNLSVC.Sales.GetReturnCheque_detWithPayments(DocNo, bankCd);
                            grvReturnChequeDet.DataSource = dt_retCheques;
                            grvReturnChequeDet.DataBind();

                            Panel_RtnChq.Visible = true;
                        }
                    }
                    DataTable invList = CHNLSVC.Sales.getInvoicesBased_onPayType(GlbUserComCode, DocNo, docType, bankCd, cardType);
                    grvInvoiceDet.DataSource = invList;
                    grvInvoiceDet.DataBind();

                    DataTable ReceiptList = CHNLSVC.Sales.get_Receipts_BasedonPayType(GlbUserComCode, DocNo, docType, bankCd, cardType);
                    grvReceptDet.DataSource = ReceiptList;
                    grvReceptDet.DataBind();
                }
            }




        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void grvPopUpSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TODO:FILL GRIDS
            string docType = ddlPayMode.SelectedValue;
            string DocNo = txtDocNo.Text.Trim();

            string PC = string.Empty;
            string date = string.Empty;
            string bankCd = string.Empty;
            string cardType = string.Empty;
            GridViewRow row = grvPopUpSelect.SelectedRow;
            if (docType == "CHEQUE" || docType == "CRCD")
            {
                PC = row.Cells[1].Text.Trim();
                date = row.Cells[2].Text.Trim();
                bankCd = row.Cells[3].Text.Trim();
                if (docType == "CRCD")
                {
                    cardType = row.Cells[4].Text.Trim();
                }

            }
            DataTable invList = CHNLSVC.Sales.getInvoicesBased_onPayType(GlbUserComCode, DocNo, docType, bankCd, cardType);
            grvInvoiceDet.DataSource = invList;
            grvInvoiceDet.DataBind();

            DataTable ReceiptList = CHNLSVC.Sales.get_Receipts_BasedonPayType(GlbUserComCode, DocNo, docType, bankCd, cardType);
            grvReceptDet.DataSource = ReceiptList;
            grvReceptDet.DataBind();

            grvPaymentDocDet.DataSource = CHNLSVC.Sales.GetPaymodeDetail(GlbUserComCode, docType, txtDocNo.Text.Trim(), bankCd, cardType);
            grvPaymentDocDet.DataBind();
        }

        protected void grvReceptDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblIssuedBy = (Label)e.Row.FindControl("lblIssuedBy");
                if (lblIssuedBy.Text == "Manager")
                {
                    e.Row.BackColor = Color.LightSalmon;
                }
                else
                {
                    //  e.Row.BackColor = Color.LightCyan;
                    e.Row.BackColor = Color.DarkSeaGreen;
                }
            }
        }

        protected void grvPaymentDocDet_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try {
                int NoOfheaderCells = grvPaymentDocDet.HeaderRow.Cells.Count;
                for (int i = 0; i < NoOfheaderCells; i++)
                {
                    if (grvPaymentDocDet.HeaderRow.Cells[i].Text == "Total" )
                    {
                        if (e.Row.RowType == DataControlRowType.DataRow)
                        {
                            int rowindex = e.Row.RowIndex;
                            e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                            //e.Row.HorizontalAlign = HorizontalAlign.Right;
                        }

                    }
                    else
                    {
                        if (e.Row.RowType == DataControlRowType.DataRow)
                        {
                            int rowindex = e.Row.RowIndex;
                            e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                            //e.Row.HorizontalAlign = HorizontalAlign.Right;
                        }
                    }

                    //----------------------------
                    if (grvPaymentDocDet.HeaderRow.Cells[i].Text == "Date")
                    {
                        if (e.Row.RowType == DataControlRowType.DataRow)
                        {
                            int rowindex = e.Row.RowIndex;
                            e.Row.Cells[i].Text= Convert.ToDateTime(e.Row.Cells[i].Text).ToShortDateString();
                        }
                    }

                    if (grvPaymentDocDet.HeaderRow.Cells[i].Text == "Total")
                    {
                        if (e.Row.RowType == DataControlRowType.DataRow)
                        {
                            int rowindex = e.Row.RowIndex;
                            //e.Row.Cells[i].Text = Convert.ToDateTime(e.Row.Cells[i].Text).ToShortDateString();
                            e.Row.Cells[i].Text = string.Format("{0:n2}", Convert.ToDecimal(e.Row.Cells[i].Text));
                        }
                    }
                }
            }
            catch(Exception ex){
            
            }
            
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.DocNo:
                    {
                        paramsText.Append(ddlPayMode.SelectedValue + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }
        protected void ImgBtnDocSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
            DataTable dataSource = CHNLSVC.CommonSearch.Get_DocNum_ByRefNo(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtDocNo.ClientID; 
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearGrids();
            txtDocNo.Text = string.Empty;

        }
    }
}