using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Local_Purchasing
{
    public partial class SupplierQuotation : Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ValidateTrue();
                if (!IsPostBack)
                {
                    PageClear();
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtQuotationNo_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.QuotationHeader);
                DataTable result = CHNLSVC.CommonSearch.SearchQuotation(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                ViewState["SEARCH"] = result;
                lblvalue.Text = "408";
               // result.Columns.Remove("DATE");
                BindUCtrlDDLData(result);
               
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void chkCopyFromPreviousOrder_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkCopyFromPreviousOrder.Checked == true)
                {
                    lbtnSave.Enabled = true;
                    lbtnSave.OnClientClick = "ConfirmSave();";
                    lbtnSave.CssClass = "buttonUndocolor";
                    lbtnUpdate.Enabled = false;
                    lbtnUpdate.OnClientClick = "return Enable();";
                    lbtnUpdate.CssClass = "buttoncolor";
                }
                else
                {
                    lbtnSave.Enabled = false;
                    lbtnSave.OnClientClick = "return Enable();";
                    lbtnSave.CssClass = "buttoncolor";

                    string status = null;
                    if (ViewState["Status"] != null)
                        status = ViewState["Status"].ToString();
                    if (status == "P")
                    {
                        lbtnUpdate.Enabled = true;
                        lbtnUpdate.OnClientClick = "ConfirmUpdate();";
                        lbtnUpdate.CssClass = "buttonUndocolor";
                    }
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "401";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnCurrency_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                DataTable result = CHNLSVC.CommonSearch.GetCurrencyData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "14";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    txtItem.Text = string.Empty;
                    lblWarning.Text = "Please Enter Item Code.";
                    divWarning.Visible = true;
                    return;
                }

                DataTable dt = CHNLSVC.Financial.GetItemDetails(txtItem.Text);

                if (dt.Rows.Count <= 0)
                {
                    txtItem.Text = string.Empty;
                    lblWarning.Text = "Invalid Item.";
                    divWarning.Visible = true;
                    return;
                }
                else 
                {
                    DataTable dtItem = CHNLSVC.Financial.GetItemDetails(txtItem.Text);

                    lblModel.Text = dtItem.Rows[0]["MI_MODEL"].ToString();
                    lblDescription.Text = dtItem.Rows[0]["MI_SHORTDESC"].ToString();
                    lblBrand.Text = dtItem.Rows[0]["MI_BRAND"].ToString();
                    lblPartNo.Text = dtItem.Rows[0]["MI_PART_NO"].ToString();
                    ViewState["UOM"] = dtItem.Rows[0]["MI_ITM_UOM"].ToString();

                    txtItem.ReadOnly = true;
                }
                
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSupplier.Text == "")
                {
                    lblWarning.Text = "Please select supplier";
                    divWarning.Visible = true;
                    return;
                }
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PurchaseOrder);
                DataTable result = CHNLSVC.Inventory.GetSuppierItem(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "407";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    lblWarning.Text = "Please enter Item.";
                    divWarning.Visible = true;
                    return;
                }
                if (ddlStatus.SelectedItem.Value == "0")
                {
                    lblWarning.Text = "Please select Status.";
                    divWarning.Visible = true;
                    return;
                }
                if (string.IsNullOrEmpty(txtFromQty.Text))
                {
                    lblWarning.Text = "Please enter Qty.";
                    divWarning.Visible = true;
                    return;
                }
                if (string.IsNullOrEmpty(txtUnitPrice.Text))
                {
                    lblWarning.Text = "Please enter Unit Price.";
                    divWarning.Visible = true;
                    return;
                }

                if (string.IsNullOrEmpty(txtSupplier.Text))
                {
                    lblWarning.Text = "Please select supplier.";
                    divWarning.Visible = true;
                    return;
                }

                if (Convert.ToDecimal(txtToQty.Text) < Convert.ToDecimal(txtFromQty.Text))
                {
                    lblWarning.Text = "From qty cannot exceed To Qty.";
                    divWarning.Visible = true;
                    return;
                }

                List<QoutationDetails> _invoiceItemList = new List<QoutationDetails>();
             
                //_invoiceItemList.AddRange(_invoiceItemList);
                //_invoiceItemList = _invoiceItemList.Concat(_invoiceItemList).ToList();

                if (ViewState["OrderItem"] != null)
                {
                    _invoiceItemList = (List<QoutationDetails>)ViewState["OrderItem"];
                }

                int seqno = 0;
                string no = "";
                if (_invoiceItemList.Count > 0)
                {
                    if (_invoiceItemList.Exists(x => x.Qd_itm_cd == txtItem.Text))
                    {
                        lblWarning.Text = "Duplicate Items Are Not Allowed.";
                        divWarning.Visible = true;
                        return;
                    }

                    seqno = _invoiceItemList[0].Qd_seq_no;
                    no = _invoiceItemList[0].Qd_no; 
                }

                QoutationDetails qd = new QoutationDetails();
                qd.Qd_seq_no = seqno;
                qd.Qd_line_no = _invoiceItemList.Count + 1;
                qd.Qd_no = no;
                qd.Qd_itm_cd = txtItem.Text;
                qd.Qd_itm_desc = lblDescription.Text;
                qd.Qd_itm_stus = ddlStatus.SelectedItem.Value;
                qd.Qd_uom = "";
                qd.Qd_quo_tp = "R";
                qd.Qd_frm_qty = Convert.ToDecimal(txtFromQty.Text);
                qd.Qd_to_qty = Convert.ToDecimal(txtToQty.Text);
                qd.Qd_unit_price = Convert.ToDecimal(txtUnitPrice.Text);
                qd.Qd_amt = 0;
                qd.Qd_dit_rt = Convert.ToDecimal(0);
                qd.Qd_dis_amt = Convert.ToDecimal(0);
                qd.Qd_itm_tax = 0;
                qd.Qd_tot_amt = 0;
                qd.Qd_uom = ViewState["UOM"].ToString();
                
                _invoiceItemList.Add(qd);
                List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                if (_invoiceItemList != null)
                {
                    foreach (QoutationDetails itemSer in _invoiceItemList)
                    {
                        if (oItemStaus != null && oItemStaus.Count > 0)
                        {
                            itemSer.Mi_statusDes = oItemStaus.Find(x => x.Mis_cd == itemSer.Qd_itm_stus).Mis_desc;
                        }
                    }
                }
           
                grdSupplierQuotation.DataSource = _invoiceItemList;
                grdSupplierQuotation.DataBind();

                ViewState["OrderItem"] = _invoiceItemList;
                
                txtItem.Text = string.Empty;
                txtItem.ReadOnly = false;
                ddlStatus.SelectedIndex = -1;
                txtFromQty.Text = string.Empty;
                txtToQty.Text = string.Empty;
                txtUnitPrice.Text = string.Empty;

                lblModel.Text = string.Empty;
                lblDescription.Text = string.Empty;
                lblBrand.Text = string.Empty;
                lblPartNo.Text = string.Empty;
                ddlStatus.SelectedValue = "GDLP";
                txtItem.Focus();
              
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void txtFromQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    lblWarning.Text = "Please enter Item.";
                    divWarning.Visible = true;
                    return;
                }
                decimal qty;
                decimal unitPrice;
                decimal taxValue = 0;
                if (!decimal.TryParse(txtFromQty.Text, out qty))
                {
                    txtFromQty.Focus();
                    return;
                }
                //if (!decimal.TryParse(txtToQty.Text, out qty))
                //{
                //    txtToQty.Focus();
                //    return;
                //}
                if (!decimal.TryParse(txtUnitPrice.Text, out unitPrice))
                {
                    txtFromQty.Text = qty.ToString("N2");
                    if (string.IsNullOrEmpty(txtToQty.Text))
                    {
                        txtToQty.Focus();
                    }
                    else
                    {
                        txtUnitPrice.Focus();
                    }
                   // txtUnitPrice.Focus();
                    return;
                }
                PriceBookLevelRef _priceBookLevelRef = null;
                //_priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), itm.Sad_pbook, itm.Sad_pb_lvl);
                taxValue = (TaxCalculation(txtItem.Text, ddlStatus.SelectedValue, Convert.ToDecimal(qty), _priceBookLevelRef, Convert.ToDecimal(unitPrice), Convert.ToDecimal(0), Convert.ToDecimal(0), true, true, true, DateTime.Now.Date, "QT"));
                txtFromQty.Text = qty.ToString("N2");
                txtUnitPrice.Text = unitPrice.ToString("N2");
                txtToQty.Focus();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void txtToQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    lblWarning.Text = "Please enter Item.";
                    divWarning.Visible = true;
                    return;
                }
                decimal qty;
                decimal unitPrice;
                decimal taxValue = 0;
                //if (!decimal.TryParse(txtFromQty.Text, out qty))
                //{
                //    txtFromQty.Focus();
                //    return;
                //}
                if (!decimal.TryParse(txtToQty.Text, out qty))
                {
                    txtToQty.Focus();
                    return;
                }
                if (!decimal.TryParse(txtUnitPrice.Text, out unitPrice))
                {
                  //  txtFromQty.Text = qty.ToString("N2");
                    txtToQty.Text = qty.ToString("N2");
                    txtUnitPrice.Focus();
                    return;
                }
                PriceBookLevelRef _priceBookLevelRef = null;
                //_priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), itm.Sad_pbook, itm.Sad_pb_lvl);
                taxValue = (TaxCalculation(txtItem.Text, ddlStatus.SelectedValue, Convert.ToDecimal(qty), _priceBookLevelRef, Convert.ToDecimal(unitPrice), Convert.ToDecimal(0), Convert.ToDecimal(0), true, true, true, DateTime.Now.Date, "QT"));
                txtToQty.Text = qty.ToString("N2");
                txtUnitPrice.Text = unitPrice.ToString("N2");
                txtUnitPrice.Focus();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    lblWarning.Text = "Please enter Item.";
                    divWarning.Visible = true;
                    return;
                }
                decimal qty;
                decimal unitPrice;
                decimal taxValue = 0;
                
                if (!decimal.TryParse(txtUnitPrice.Text, out unitPrice))
                {
                    txtUnitPrice.Focus();
                    return;
                }
                if (!decimal.TryParse(txtFromQty.Text, out qty))
                {
                    txtUnitPrice.Text = unitPrice.ToString("N2");
                    txtFromQty.Focus();
                    return;
                }

                PriceBookLevelRef _priceBookLevelRef = null;
                //_priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), itm.Sad_pbook, itm.Sad_pb_lvl);
                taxValue = (TaxCalculation(txtItem.Text, ddlStatus.SelectedValue, Convert.ToDecimal(qty), _priceBookLevelRef, Convert.ToDecimal(unitPrice), Convert.ToDecimal(0), Convert.ToDecimal(0), true, true, true, DateTime.Now.Date, "QT"));
                //txtFromQty.Text = qty.ToString("N2");
                txtUnitPrice.Text = unitPrice.ToString("N2");
                lbtnAdd.Focus();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnClearItem_Click(object sender, EventArgs e)
        {
            try
            {
                txtItem.Text = string.Empty;
                txtItem.ReadOnly = false;
                ddlStatus.SelectedIndex = -1;
                txtFromQty.Text = string.Empty;
                txtToQty.Text = string.Empty;
                txtUnitPrice.Text = string.Empty;
                
                lblModel.Text = string.Empty;
                lblDescription.Text = string.Empty;
                lblBrand.Text = string.Empty;
                lblPartNo.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnSupplierQuotationDalete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtQuotationNo.Text))
                {
                    lblWarning.Text = "Can Not Delete Items.";
                    divWarning.Visible = true;
                    return;
                }

                if (hdnItemDelete.Value == "Yes")
                {

                    LinkButton btn = (LinkButton)sender;
                    GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                    List<QoutationDetails> qd = (List<QoutationDetails>)ViewState["OrderItem"];
                    qd.RemoveAt(grdr.RowIndex);
                    List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                    oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                    if (qd != null)
                    {
                        foreach (QoutationDetails itemSer in qd)
                        {
                            if (oItemStaus != null && oItemStaus.Count > 0)
                            {
                                itemSer.Mi_statusDes = oItemStaus.Find(x => x.Mis_cd == itemSer.Qd_itm_stus).Mis_desc;
                            }
                        }
                    }
           
                    ViewState["OrderItem"] = qd;
                    grdSupplierQuotation.DataSource = qd;
                    grdSupplierQuotation.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnSupplierQuotationEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                grdSupplierQuotation.EditIndex = grdr.RowIndex;//e.NewEditIndex;
                List<QoutationDetails> qd = (List<QoutationDetails>)ViewState["OrderItem"];
                List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                if (qd != null)
                {
                    foreach (QoutationDetails itemSer in qd)
                    {
                        if (oItemStaus != null && oItemStaus.Count > 0)
                        {
                            itemSer.Mi_statusDes = oItemStaus.Find(x => x.Mis_cd == itemSer.Qd_itm_stus).Mis_desc;
                        }
                    }
                }
           
                grdSupplierQuotation.DataSource = qd;
                grdSupplierQuotation.DataBind();

                TextBox txtFromQtyEdit = ((TextBox)grdSupplierQuotation.Rows[grdr.RowIndex].FindControl("txtFromQtyEdit"));
                txtFromQtyEdit.Focus();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }  
        }
        protected void lbtnSupplierQuotationCancel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                //e.Cancel = true;
                grdSupplierQuotation.EditIndex = -1;
                List<QoutationDetails> qd = (List<QoutationDetails>)ViewState["OrderItem"];
                List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                if (qd != null)
                {
                    foreach (QoutationDetails itemSer in qd)
                    {
                        if (oItemStaus != null && oItemStaus.Count > 0)
                        {
                            itemSer.Mi_statusDes = oItemStaus.Find(x => x.Mis_cd == itemSer.Qd_itm_stus).Mis_desc;
                        }
                    }
                }
           
                grdSupplierQuotation.DataSource = qd;
                grdSupplierQuotation.DataBind();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnSupplierQuotationUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                //DataTable dt = (DataTable)ViewState["OrderItem"];
                List<QoutationDetails> qd = (List<QoutationDetails>)ViewState["OrderItem"];
                decimal qty;
                decimal unitrate;

                if (!decimal.TryParse(((TextBox)grdSupplierQuotation.Rows[grdr.RowIndex].FindControl("txtFromQtyEdit")).Text, out qty))
                {
                    lblWarning.Text = "Please Enter valid Item Qty.";
                    divWarning.Visible = true;
                    return;
                }
                if (!decimal.TryParse(((TextBox)grdSupplierQuotation.Rows[grdr.RowIndex].FindControl("txtUnitPrice")).Text, out unitrate))
                {
                    lblWarning.Text = "Please Enter valid Item Unit Rate.";
                    divWarning.Visible = true;
                    return;
                }
                qd[grdr.RowIndex].Qd_frm_qty = qty;
                qd[grdr.RowIndex].Qd_unit_price = unitrate;
                qd[grdr.RowIndex].Qd_amt = qty * unitrate;
                grdSupplierQuotation.EditIndex = -1;
                ViewState["OrderItem"] = qd;
                List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                if (qd != null)
                {
                    foreach (QoutationDetails itemSer in qd)
                    {
                        if (oItemStaus != null && oItemStaus.Count > 0)
                        {
                            itemSer.Mi_statusDes = oItemStaus.Find(x => x.Mis_cd == itemSer.Qd_itm_stus).Mis_desc;
                        }
                    }
                }
           
                grdSupplierQuotation.DataSource = qd;
                grdSupplierQuotation.DataBind();

            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            } 
        }
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnSave.Value == "Yes")
                {
                    DateTime date;
                    DateTime exDate;
                    if (!DateTime.TryParse(txtDate.Text, out date))
                    {
                        lblWarning.Text = "Please select valid Date.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (!DateTime.TryParse(txtExDate.Text, out exDate))
                    {
                        lblWarning.Text = "Please select valid ExDate.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (ddlType.SelectedItem.Text == " - - Select - - ")
                    {
                        lblWarning.Text = "Please select Type.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtRefNo.Text))
                    {
                        lblWarning.Text = "Please enter Ref#.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtSupplier.Text))
                    {
                        lblWarning.Text = "Please select Supplier.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtSupplier.ToolTip))
                    {
                        lblWarning.Text = "Please select Supplier.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtCurrency.Text))
                    {
                        lblWarning.Text = "Please select Supplier Currency.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtCurrency.ToolTip))
                   {
                        lblWarning.Text = "Exchange rates not set.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (grdResult.Rows.Count == 0)
                    {
                        lblWarning.Text = "Please enter Items.";
                        divWarning.Visible = true;
                        return;
                    }

                    if (ViewState["OrderItem"] == null)
                    {
                        lblWarning.Text = "Please enter Items.";
                        divWarning.Visible = true;
                        return;
                    }

                    QuotationHeader _saveHdr = new QuotationHeader();
                    _saveHdr.Qh_seq_no = 0;
                    _saveHdr.Qh_no = null;
                    _saveHdr.Qh_com = Session["UserCompanyCode"].ToString();
                    _saveHdr.Qh_tp = "S";
                    _saveHdr.Qh_sub_tp = ddlType.SelectedItem.Value;
                    _saveHdr.Qh_dt = Convert.ToDateTime(txtDate.Text).Date;
                    _saveHdr.Qh_frm_dt = Convert.ToDateTime(txtDate.Text).Date;
                    _saveHdr.Qh_ex_dt = Convert.ToDateTime(txtExDate.Text).Date;
                    _saveHdr.Qh_ref = txtRefNo.Text;
                    _saveHdr.Qh_party_cd = txtSupplier.Text;
                    _saveHdr.Qh_party_name = txtSupplier.ToolTip;
                    _saveHdr.Qh_add1 = "";//TOADD
                    _saveHdr.Qh_add2 = "";//TOADD
                    _saveHdr.Qh_tel = "";//TOADD
                    _saveHdr.Qh_mobi = "";//TOADD
                    _saveHdr.Qh_email = "";
                    _saveHdr.Qh_remarks = txtRemark.Text;
                    _saveHdr.Qh_cur_cd = txtCurrency.Text;
                    _saveHdr.Qh_ex_rt = Convert.ToDecimal(txtCurrency.ToolTip);
                    _saveHdr.Qh_stus = "P";
                    _saveHdr.Qh_pc = Session["UserDefProf"].ToString();
                    _saveHdr.Qh_sales_ex = "";//TOADD
                    _saveHdr.Qh_is_tax = true;//TOADD
                    _saveHdr.Qh_jobno = "";
                    _saveHdr.Qh_cre_by = Session["UserID"].ToString();
                    _saveHdr.Qh_cre_when = DateTime.Now;
                    _saveHdr.Qh_mod_by = Session["UserID"].ToString();
                    _saveHdr.Qh_mod_when = DateTime.Now;
                    _saveHdr.Qh_session_id = Session["SessionID"].ToString();
                    //_saveHdr.Qh_anal_1 = "";//TOADD
                    //_saveHdr.Qh_anal_2 = "";//TOADD
                    //_saveHdr.Qh_anal_3 = "";//TOADD
                    //_saveHdr.Qh_anal_4 = "";//TOADD
                    //_saveHdr.Qh_anal_5 = 0;//TOADD
                    //_saveHdr.Qh_anal_6 = 0;//TOADD
                    //_saveHdr.Qh_anal_7 = 0;//TOADD
                    //_saveHdr.Qh_anal_8 = DateTime.Now;//TOADD
                    //_saveHdr.Qh_del_cuscd = "";//TOADD
                    //_saveHdr.Qh_del_cusname = "";//TOADD
                    //_saveHdr.Qh_del_cusadd1 = "";//TOADD
                    //_saveHdr.Qh_del_cusadd2 = "";//TOADD
                    //_saveHdr.Qh_del_custel = "";//TOADD
                    //_saveHdr.Qh_del_cusfax = "";//TOADD
                    //_saveHdr.Qh_del_cusid = "";//TOADD
                    //_saveHdr.Qh_del_cusvatreg = "";//TOADD
                    //_saveHdr.Qh_add_wararmk = "";//TOADD

                    MasterAutoNumber masterAuto = new MasterAutoNumber();
                    masterAuto.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                    masterAuto.Aut_cate_tp = "COM";
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "QUA";//SOREQ
                    //masterAuto.Aut_number = 5;
                    masterAuto.Aut_start_char = "QUA";//SOREQ
                    masterAuto.Aut_year = null;

                    string QTNum;
                    List<QoutationDetails> _invoiceItemList = new List<QoutationDetails>();
                    List<QuotationSerial> _QuoSerials = new List<QuotationSerial>();
                    ReptPickHeader _SerHeader = new ReptPickHeader();
                    List<ReptPickSerials> _tempSerialSave = new List<ReptPickSerials>();

                    _invoiceItemList = (List<QoutationDetails>)ViewState["OrderItem"];

                    int row_aff = (Int32)CHNLSVC.Sales.Quotation_save(_saveHdr, _invoiceItemList, masterAuto, _QuoSerials,null,null,null, false, _SerHeader, _tempSerialSave, out QTNum);

                    if (row_aff >= 1)
                    {
                        lblSuccess.Text = "Successfully Created. Quotation No: " + QTNum;
                        divSuccess.Visible = true;
                        PageClear();
                        return;
                    }
                    else
                    {
                        lblWarning.Text = QTNum;
                        divWarning.Visible = true;
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnUpdate.Value == "Yes")
                {
                    DateTime date;
                    DateTime exDate;
                    if (!DateTime.TryParse(txtDate.Text, out date))
                    {
                        lblWarning.Text = "Please select valid Date.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (!DateTime.TryParse(txtExDate.Text, out exDate))
                    {
                        lblWarning.Text = "Please select valid ExDate.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (ddlType.SelectedItem.Text == " - - Select - - ")
                    {
                        lblWarning.Text = "Please select Type.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtRefNo.Text))
                    {
                        lblWarning.Text = "Please enter Ref#.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtSupplier.Text))
                    {
                        lblWarning.Text = "Please select Supplier.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtSupplier.ToolTip))
                    {
                        lblWarning.Text = "Please select Supplier.";
                        divWarning.Visible = true;
                        return;
                    }

                    QuotationHeader _saveHdr = new QuotationHeader();
                    //_saveHdr.Qh_seq_no = 0;
                    _saveHdr.Qh_no = txtQuotationNo.Text;
                    //_saveHdr.Qh_com = Session["UserCompanyCode"].ToString();
                    //_saveHdr.Qh_tp = "S";
                    _saveHdr.Qh_sub_tp = ddlType.SelectedItem.Value;
                    //_saveHdr.Qh_dt = Convert.ToDateTime(txtDate.Text).Date;
                    //_saveHdr.Qh_frm_dt = Convert.ToDateTime(txtDate.Text).Date;
                    _saveHdr.Qh_ex_dt = Convert.ToDateTime(txtExDate.Text).Date;
                    _saveHdr.Qh_ref = txtRefNo.Text;
                    //_saveHdr.Qh_party_cd = txtSupplier.Text;
                    //_saveHdr.Qh_party_name = txtSupplier.ToolTip;
                    //_saveHdr.Qh_add1 = "";//TOADD
                    //_saveHdr.Qh_add2 = "";//TOADD
                    //_saveHdr.Qh_tel = "";//TOADD
                    //_saveHdr.Qh_mobi = "";//TOADD
                    //_saveHdr.Qh_email = "";
                    _saveHdr.Qh_remarks = txtRemark.Text;
                    _saveHdr.Qh_cur_cd = txtCurrency.Text;
                    _saveHdr.Qh_ex_rt = Convert.ToDecimal(txtCurrency.ToolTip);
                    _saveHdr.Qh_stus = txtQuoStus.Text.Trim();
                    //_saveHdr.Qh_pc = Session["UserDefProf"].ToString();
                    //_saveHdr.Qh_sales_ex = "";//TOADD
                    //_saveHdr.Qh_is_tax = true;//TOADD
                    //_saveHdr.Qh_jobno = "";
                    //_saveHdr.Qh_cre_by = Session["UserID"].ToString();
                    //_saveHdr.Qh_cre_when = DateTime.Now;
                    _saveHdr.Qh_mod_by = Session["UserID"].ToString();
                    _saveHdr.Qh_mod_when = DateTime.Now;
                    _saveHdr.Qh_session_id = Session["SessionID"].ToString();
                    //_saveHdr.Qh_anal_1 = "";//TOADD
                    //_saveHdr.Qh_anal_2 = "";//TOADD
                    //_saveHdr.Qh_anal_3 = "";//TOADD
                    //_saveHdr.Qh_anal_4 = "";//TOADD
                    //_saveHdr.Qh_anal_5 = 0;//TOADD
                    //_saveHdr.Qh_anal_6 = 0;//TOADD
                    //_saveHdr.Qh_anal_7 = 0;//TOADD
                    //_saveHdr.Qh_anal_8 = DateTime.Now;//TOADD
                    //_saveHdr.Qh_del_cuscd = "";//TOADD
                    //_saveHdr.Qh_del_cusname = "";//TOADD
                    //_saveHdr.Qh_del_cusadd1 = "";//TOADD
                    //_saveHdr.Qh_del_cusadd2 = "";//TOADD
                    //_saveHdr.Qh_del_custel = "";//TOADD
                    //_saveHdr.Qh_del_cusfax = "";//TOADD
                    //_saveHdr.Qh_del_cusid = "";//TOADD
                    //_saveHdr.Qh_del_cusvatreg = "";//TOADD
                    //_saveHdr.Qh_add_wararmk = "";//TOADD


                    string QTNum;
                    List<QoutationDetails> _invoiceItemList = new List<QoutationDetails>();
                    List<QuotationSerial> _QuoSerials = new List<QuotationSerial>();
                    ReptPickHeader _SerHeader = new ReptPickHeader();
                    List<ReptPickSerials> _tempSerialSave = new List<ReptPickSerials>();

                    _invoiceItemList = (List<QoutationDetails>)ViewState["OrderItem"];

                    int row_aff = (Int32)CHNLSVC.Financial.Update_QUO(_saveHdr, _invoiceItemList, out QTNum);

                    if (row_aff >= 1)
                    {
                        lblSuccess.Text = "Successfully Updated. Quotation No: " + txtQuotationNo.Text;
                        divSuccess.Visible = true;
                        PageClear();
                        return;
                    }
                    else
                    {
                        lblWarning.Text = "Updated Fail ...  .";
                        divWarning.Visible = true;
                        return;
                    }
                }
                
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }         
        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            string err = "";
            try
            {
                if (hdnCancel.Value == "Yes")
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16008))
                    {
                        lblWarning.Text = "Sorry, You have no permission to cancel this Supplier Quotation.( Advice: Required permission code : 16008)";
                        divWarning.Visible = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtQuotationNo.Text))
                    {
                        lblWarning.Text = "Please enter Quotation No.";
                        divWarning.Visible = true;
                        return;
                    }
                    
                    if (txtQuoStus.Text == "C")
                    {
                        lblWarning.Text = "Selected quotation already cancelled.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtQuoStus.Text))
                    {
                        lblWarning.Text = "Selected quotation not in pending status.";
                        divWarning.Visible = true;
                        return;
                    }

                    int count = CHNLSVC.Financial.UpdateStatus_QUO_HDR(txtQuotationNo.Text, "C", Session["UserID"].ToString(), DateTime.Now, out err);
                    if (count == 1)
                    {
                        lblSuccess.Text = "Successfully Canceled. Quotation No: " + txtQuotationNo.Text;
                        divSuccess.Visible = true;
                        PageClear();
                        return;
                    }
                    else
                    {
                        lblWarning.Text = "Cancel Fail ." + err;
                        divWarning.Visible = true;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnClear.Value == "Yes")
                {
                    PageClear();
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnApprove_Click(object sender, EventArgs e)
        {
            string err = "";
            try
            {
                if (hdnApprove.Value == "Yes")
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16007))
                    {
                        lblWarning.Text = "Sorry, You have no permission to approve this Supplier Quotation.( Advice: Required permission code : 16007)";
                        divWarning.Visible = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtQuotationNo.Text))
                    {
                        lblWarning.Text = "Please select a not approved Quotation No.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (txtQuoStus.Text == "A")
                    {
                        lblWarning.Text = "Selected quotation already approved.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (txtQuoStus.Text == "C")
                    {
                        lblWarning.Text = "Selected quotation already cancelled.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtQuoStus.Text))
                    {
                        lblWarning.Text = "Selected quotation not in pending status.";
                        divWarning.Visible = true;
                        return;
                    }

                    int count = CHNLSVC.Financial.UpdateStatus_QUO_HDR(txtQuotationNo.Text, "A", Session["UserID"].ToString(), DateTime.Now, out err);
                    if (count == 1)
                    {
                        lblSuccess.Text = "Successfully Approved. Quotation No: " + txtQuotationNo.Text;
                        divSuccess.Visible = true;
                        PageClear();
                        return;
                    }
                    else
                    {
                        lblWarning.Text = "Approned Fail ." + err;
                        divWarning.Visible = true;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }



        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                //Item
                if (lblvalue.Text == "407")
                {
                    txtItem.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtItem_TextChanged(null, null);
                }
                //Supplier
                if (lblvalue.Text == "401")
                {
                    txtSupplier.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtSupplier.ToolTip = grdResult.SelectedRow.Cells[2].Text;

                    List<MasterBusinessEntity> supplierlistedit = CHNLSVC.Sales.GetCustomerDetailList(Session["UserCompanyCode"].ToString(), txtSupplier.Text, string.Empty, string.Empty, "S");
                    if (supplierlistedit != null || supplierlistedit.Count > 1)
                    {
                        foreach (var _nicCust in supplierlistedit)
                        {
                            txtCurrency.Text = _nicCust.Mbe_cur_cd;                            
                        }
                    }
                    ViewState["ExchangRate"] = 0;
                    if (Session["UserDefProf"].ToString() == "")
                    {
                        Session["UserDefProf"] = "Com";

                        MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                        MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(Session["UserCompanyCode"].ToString(), txtCurrency.Text, DateTime.Now, _pc.Mpc_def_exrate, "");
                        if (_exc1 != null)
                        {
                            txtCurrency.ToolTip = Convert.ToDouble(_exc1.Mer_bnkbuy_rt).ToString();
                        }
                        Session["UserDefProf"] = "";
                    }
                    else
                    {
                        MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                        MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(Session["UserCompanyCode"].ToString(), txtCurrency.Text, DateTime.Now, _pc.Mpc_def_exrate, "");
                        if (_exc1 != null)
                        {
                            txtCurrency.ToolTip = Convert.ToDouble(_exc1.Mer_bnkbuy_rt).ToString();
                        }
                    }
                   
                }
                //Currency
                if (lblvalue.Text == "14")
                {
                    txtCurrency.Text = grdResult.SelectedRow.Cells[1].Text;
                    MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                    MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(Session["UserCompanyCode"].ToString(), txtCurrency.Text, DateTime.Now, _pc.Mpc_def_exrate, "");
                    if (_exc1 != null)
                    {
                        txtCurrency.ToolTip = Convert.ToDouble(_exc1.Mer_bnkbuy_rt).ToString();
                    }   
                }
                //QuotationNo
                if (lblvalue.Text == "408")
                {
                    txtQuotationNo.Text = grdResult.SelectedRow.Cells[1].Text;
                    PopulateQuotation();
                }
                ViewState["SEARCH"] = null;
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdResult.PageIndex = e.NewPageIndex;
                grdResult.DataSource = null;
                grdResult.DataSource = (DataTable)ViewState["SEARCH"];
                grdResult.DataBind();
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        private void FilterData()
        {
            //Item
            if (lblvalue.Text == "407")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportItem);
                DataTable result = CHNLSVC.CommonSearch.SearchItem(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "407";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            //Supplier
            else if (lblvalue.Text == "401")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "401";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            //Currency
            else if (lblvalue.Text == "14")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable result = CHNLSVC.CommonSearch.GetCurrencyData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "14";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            //QuotationNo
            else if (lblvalue.Text == "408")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.QuotationHeader);
                DataTable result = CHNLSVC.CommonSearch.SearchQuotation(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "408";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            else if (ViewState["SEARCH"] != null)
            {
                DataTable result = (DataTable)ViewState["SEARCH"];
                DataView dv = new DataView(result);
                string searchParameter = ddlSearchbykey.Text;
                dv.RowFilter = "" + ddlSearchbykey.Text + " like '%" + txtSearchbyword.Text + "%'";
                // dv.RowFilter = "REFERENCESNO = '" + txtSearchbyword.Text + "' ";
                if (dv.Count > 0)
                {
                    result = dv.ToTable();
                }
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopup.Show();
            }

        }



        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.SupplierImport:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "S" + seperator + "L" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PINO:
                    {
                        paramsText.Append(BaseCls.GlbPINo + seperator);
                        break;
                    }               
                case CommonUIDefiniton.SearchUserControlType.ImportModel:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Currency:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.QuotationHeader:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PurchaseOrder:
                    {
                        string type;
                    
                        paramsText.Append(txtSupplier.Text + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }
        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchbyword.Text))
                {
                    FilterData();
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        private void PopulateQuotation()
        {
            if (string.IsNullOrEmpty(txtQuotationNo.Text))
            {
                lblWarning.Text = "Please select Supplier.";
                divWarning.Visible = true;
                return;
            }

            ViewState["DaleteVisibility"] = false;

            QuotationHeader qh = new QuotationHeader();
            qh = CHNLSVC.Sales.Get_Quotation_HDR(txtQuotationNo.Text);

            txtDate.Text = qh.Qh_dt.ToString("dd/MMM/yyyy");
            txtExDate.Text = qh.Qh_ex_dt.ToString("dd/MMM/yyyy");
            ddlType.SelectedValue = qh.Qh_sub_tp;
            txtRefNo.Text = qh.Qh_ref;
            txtSupplier.Text = qh.Qh_party_cd;
            txtSupplier.ToolTip = qh.Qh_party_name;
            txtCurrency.Text = qh.Qh_cur_cd;
            txtCurrency.ToolTip = qh.Qh_ex_rt.ToString();
            txtRemark.Text = qh.Qh_remarks;
            txtQuoStus.Text = qh.Qh_stus;
            if (qh.Qh_stus == "A")
            {
                txtQuoStatus.Text = "APPROVED";
            }
            else if (qh.Qh_stus == "C")
            {
                txtQuoStatus.Text = "CANCELLED";
            }
            else if (qh.Qh_stus == "P")
            {
                txtQuoStatus.Text = "PENDING";
            }
            else if (qh.Qh_stus == "S")
            {
                txtQuoStatus.Text = "PENDING";
            }
            else
            {
                txtQuoStatus.Text = "INVALID";
            }

            lbtnSupplier.Enabled = false;
            lbtnDate.Enabled = false;

            List<QoutationDetails> qd = new List<QoutationDetails>();
            qd = CHNLSVC.Sales.Get_all_linesForQoutation(txtQuotationNo.Text);
            List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
            oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
            if (qd != null)
            {
                foreach (QoutationDetails itemSer in qd)
                {
                    if (oItemStaus != null && oItemStaus.Count > 0)
                    {
                        itemSer.Mi_statusDes = oItemStaus.Find(x => x.Mis_cd == itemSer.Qd_itm_stus).Mis_desc;
                    }
                }
            }
           
            grdSupplierQuotation.DataSource = qd;
            grdSupplierQuotation.DataBind();           

            ViewState["OrderItem"] = qd;
            ViewState["Status"] = qh.Qh_stus;            

            chkCopyFromPreviousOrder.Enabled = true;

            lbtnSave.Enabled = false;
            lbtnSave.OnClientClick = "return Enable();";
            lbtnSave.CssClass = "buttoncolor";

            if (qh.Qh_stus == "P")
            {
                lbtnUpdate.Enabled = true;
                lbtnUpdate.OnClientClick = "ConfirmUpdate();";
                lbtnUpdate.CssClass = "buttonUndocolor";

                if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16008))
                {
                    lbtnCancel.Enabled = true;
                    lbtnCancel.OnClientClick = "ConfirmCancel();";
                    lbtnCancel.CssClass = "buttonUndocolor";
                }
                if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16007))
                {
                    lbtnApprove.Enabled = true;
                    lbtnApprove.OnClientClick = "ConfirmApprove();";
                    lbtnApprove.CssClass = "buttonUndocolor";
                }
            }
            else if (qh.Qh_stus == "A")
            {
                if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16008))
                {
                    lbtnCancel.Enabled = true;
                    lbtnCancel.OnClientClick = "ConfirmCancel();";
                    lbtnCancel.CssClass = "buttonUndocolor";
                }
            }
            else if (qh.Qh_stus == "C")
            {
                lbtnSave.Enabled = false;
                lbtnSave.OnClientClick = "return Enable();";
                lbtnSave.CssClass = "buttoncolor";

                lbtnCancel.Enabled = false;
                lbtnCancel.OnClientClick = "return Enable();";
                lbtnCancel.CssClass = "buttoncolor";

                lbtnApprove.Enabled = false;
                lbtnApprove.OnClientClick = "return Enable();";
                lbtnApprove.CssClass = "buttoncolor";

                lbtnUpdate.Enabled = false;
                lbtnUpdate.OnClientClick = "return Enable();";
                lbtnUpdate.CssClass = "buttoncolor";
            }

        }        
        public Boolean GetDaleteVisibility()
        {
            Boolean b = false;
            b = (Boolean)ViewState["DaleteVisibility"];
            return b;
        }
        private void ValidateTrue()
        {
            divWarning.Visible = false;
            lblWarning.Text = "";
            divSuccess.Visible = false;
            lblSuccess.Text = "";
            divAlert.Visible = false;
            lblAlert.Text = "";
        }
        private void PageClear()
        {
            txtQuotationNo.Text = String.Empty;
            txtDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lbtnDate.Enabled = true;
            txtExDate.Text = "31/Dec/2999";
            chkCopyFromPreviousOrder.Checked = false;
            chkCopyFromPreviousOrder.Enabled = false;
            ddlType.SelectedIndex = -1;
            txtRefNo.Text = string.Empty;
            txtSupplier.Text = string.Empty;
            lbtnSupplier.Enabled = true;
            txtSupplier.ToolTip = string.Empty;
            txtRemark.Text = string.Empty;
            txtCurrency.Text = string.Empty;
            txtCurrency.ToolTip = string.Empty;
            txtQuoStus.Text = string.Empty;
            txtQuoStatus.Text = string.Empty;
            txtQuoStatus.Text = "NEW";

            txtItem.Text = string.Empty;
            txtItem.ReadOnly = false;
            ddlStatus.SelectedIndex = -1;
            txtFromQty.Text = string.Empty;
            txtToQty.Text = string.Empty;
            txtUnitPrice.Text = string.Empty;
            
            lblModel.Text = string.Empty;
            lblDescription.Text = string.Empty;
            lblBrand.Text = string.Empty;
            lblPartNo.Text = string.Empty;
            
            grdSupplierQuotation.DataSource = new int[] { };
            grdSupplierQuotation.DataBind();

            BindUserCompanyItemStatusDDLData(ddlStatus);

            ViewState["OrderItem"] = null;
            ViewState["Status"] = null;
            ViewState["DaleteVisibility"] = true;

            lbtnSave.Enabled = true;
            lbtnSave.OnClientClick = "ConfirmSave();";
            lbtnSave.CssClass = "buttonUndocolor";
            lbtnUpdate.Enabled = false;
            lbtnUpdate.OnClientClick = "return Enable();";
            lbtnUpdate.CssClass = "buttoncolor";
            lbtnCancel.Enabled = false;
            lbtnCancel.OnClientClick = "return Enable();";
            lbtnCancel.CssClass = "buttoncolor";
            lbtnApprove.Enabled = false;
            lbtnApprove.OnClientClick = "return Enable();";
            lbtnApprove.CssClass = "buttoncolor";
            
        }

        private void BindUserCompanyItemStatusDDLData(DropDownList ddl)
        {
            DataTable _tbl = CHNLSVC.Inventory.GetAllCompanyStatus(Session["UserCompanyCode"].ToString()); ;
            var _s = (from L in _tbl.AsEnumerable()
                      select new
                      {
                          MIS_DESC = L.Field<string>("MIS_DESC"),
                          MIC_CD = L.Field<string>("MIC_CD")
                      }).ToList();
            var _n = new { MIS_DESC = string.Empty, MIC_CD = string.Empty };
            _s.Insert(0, _n);
            ddl.DataSource = _s;
            ddl.DataTextField = "MIS_DESC";
            ddl.DataValueField = "MIC_CD";
            ddl.DataBind();
            ddl.SelectedValue = "GDLP";
        }

        protected void txtSupCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                if (!string.IsNullOrEmpty(txtSupplier.Text))
                {
                    if (!CHNLSVC.Inventory.IsValidSupplier(Session["UserCompanyCode"].ToString(), txtSupplier.Text, 1, "S"))
                    {
                        lblWarning.Text = "Invalid supplier code" ;
                        divWarning.Visible = true;
                        txtSupplier.Text = "";
                        txtSupplier.Focus();
                        return;
                    }
                    List<MasterBusinessEntity> supplierlistedit = CHNLSVC.Sales.GetCustomerDetailList(Session["UserCompanyCode"].ToString(), txtSupplier.Text, string.Empty, string.Empty, "S");
                    if (supplierlistedit != null || supplierlistedit.Count > 1)
                    {
                        foreach (var _nicCust in supplierlistedit)
                        {
                            txtCurrency.Text = _nicCust.Mbe_cur_cd;
                        }
                    }
                    ViewState["ExchangRate"] = 0;
                    MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                    MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(Session["UserCompanyCode"].ToString(), txtCurrency.Text, DateTime.Now, _pc.Mpc_def_exrate, string.Empty);
                    if (_exc1 != null)
                    {
                        txtCurrency.ToolTip = Convert.ToDouble(_exc1.Mer_bnkbuy_rt).ToString();
                    }      

                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;

            }
           
        }
       
    }
}