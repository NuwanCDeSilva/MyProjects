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
    public partial class SalesOrderRequest : Base
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
        protected void txtRequestNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRequestNo.Text))
                {
                    txtRequestNo.Text = string.Empty;
                    lblWarning.Text = "Please Enter Request No.";
                    divWarning.Visible = true;
                    return;
                }
                if (string.IsNullOrEmpty(txtSalesExcecutive.Text))
                {
                    txtRequestNo.Text = string.Empty;
                    lblWarning.Text = "Invalid Sales Excecutive.";
                    divWarning.Visible = true;
                    return;
                }
                DataTable dt = CHNLSVC.Sales.Check_INT_REQ(txtRequestNo.Text, "SOREQ", txtSalesExcecutive.Text);

                if (dt.Rows.Count != 1)
                {
                    txtRequestNo.Text = string.Empty;
                    lblWarning.Text = "Invalid Request No.";
                    divWarning.Visible = true;
                    return;
                }
                else
                {
                    txtRequestNo.Text = dt.Rows[0]["ITR_REQ_NO"].ToString();
                    PopulateRequestData();
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtRequestNo_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RequestNo);
                DataTable result = CHNLSVC.CommonSearch.Search_INT_REQ(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "420";
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
        protected void txtRequestType_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRequestType.Text))
                {
                    txtRequestType.Text = string.Empty;
                    lblWarning.Text = "Please enter the request type.";
                    divWarning.Visible = true;
                    return;
                }
                DataTable dt = CHNLSVC.Sales.CheckRequestType(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtRequestType.Text);

                if (dt.Rows.Count != 1)
                {
                    txtRequestType.Text = string.Empty;
                    lblWarning.Text = "Invalid request type.";
                    divWarning.Visible = true;
                    return;
                }
                else
                {
                    txtRequestType.Text = dt.Rows[0]["REQUEST TYPE"].ToString();                    
                }                
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnRequestType_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RequestType);
                DataTable result = CHNLSVC.CommonSearch.SearchRequestType(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "418";
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
        protected void txtSalesExcecutive_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSalesExcecutive.Text))
                {
                    txtSalesExcecutive.Text = string.Empty;
                    txtSalesExcecutiveName.Text = string.Empty;
                    lblWarning.Text = "Please Enter Sales Excecutive.";
                    divWarning.Visible = true;
                    return;
                }
                DataTable dt = CHNLSVC.Sales.CheckExecutive(txtSalesExcecutive.Text);

                if (dt.Rows.Count != 1)
                {
                    txtSalesExcecutive.Text = string.Empty;
                    txtSalesExcecutiveName.Text = string.Empty;
                    lblWarning.Text = "Invalid Sales Excecutive.";
                    divWarning.Visible = true;                    
                    return;
                }
                else
                {
                    txtSalesExcecutive.Text = dt.Rows[0]["EMPLOYEE ID"].ToString();
                    txtSalesExcecutiveName.Text = dt.Rows[0]["USER NAME"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        //protected void lbtnSalesExcecutive_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ViewState["SEARCH"] = null;
        //        txtSearchbyword.Text = string.Empty;
        //        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesExcecutive);
        //        DataTable result = CHNLSVC.CommonSearch.SearchExecutive(SearchParams, null, null);
        //        grdResult.DataSource = result;
        //        grdResult.DataBind();
        //        lblvalue.Text = "419";
        //        BindUCtrlDDLData(result);
        //        ViewState["SEARCH"] = result;
        //        UserPopup.Show();
        //    }
        //    catch (Exception ex)
        //    {
        //        lblWarning.Text = "Error Occurred while processing...  " + ex;
        //        divWarning.Visible = true;
        //    }
        //}
        protected void txtInvoiceCustomer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtInvoiceCustomer.Text))
                {
                    txtInvoiceCustomer.Text = string.Empty;
                    txtInvoiceCustomerName.Text = string.Empty;
                    lblWarning.Text = "Please Enter Invoice Customer.";
                    divWarning.Visible = true;
                    return;
                }
                DataTable dt = CHNLSVC.Financial.CheckBusentity(Session["UserCompanyCode"].ToString(), "C", null, txtInvoiceCustomer.Text);

                if (dt.Rows.Count != 1)
                {
                    txtInvoiceCustomer.Text = string.Empty;
                    txtInvoiceCustomerName.Text = string.Empty;
                    lblWarning.Text = "Invalid invoice customer.";
                    divWarning.Visible = true;
                    return;
                }
                else
                {
                    txtInvoiceCustomer.Text = dt.Rows[0]["CODE"].ToString();
                    txtInvoiceCustomerName.Text = dt.Rows[0]["NAME"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnInvoiceCustomer_Click(object sender, EventArgs e)
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
                    lblWarning.Text = "Invalid item.";
                    divWarning.Visible = true;
                    return;
                }
                else
                {
                    DataTable dtItem = CHNLSVC.Financial.GetItemDetails(txtItem.Text);

                    txtModel.Text = dtItem.Rows[0]["MI_MODEL"].ToString();
                    txtDescription.Text = dtItem.Rows[0]["MI_SHORTDESC"].ToString();
                    //lblBrand.Text = dtItem.Rows[0]["MI_BRAND"].ToString();
                    //lblPartNo.Text = dtItem.Rows[0]["MI_PART_NO"].ToString();
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
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportItem);
                DataTable result = CHNLSVC.CommonSearch.SearchItemforchange(SearchParams, null, null);
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
        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    txtQty.Text = string.Empty;
                    txtUnitPrice.Text = string.Empty;
                    lblWarning.Text = "Please enter item details.";
                    divWarning.Visible = true;
                    return;
                }
                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    txtValue.Text = string.Empty;
                    lblWarning.Text = "Please enter Qty.";
                    divWarning.Visible = true;
                    return;
                }
                decimal qty;
                decimal unitPrice;

                string uom = (string)ViewState["UOM"];
                DataTable dt = CHNLSVC.Inventory.IsItemUOMDecimalAllow(uom);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string isdecimalallow = dr["msu_isdecimal"].ToString();
                        if (isdecimalallow != "1")
                        {
                            int qtyInt;
                            if (int.TryParse(txtQty.Text.Trim(), out qtyInt))
                            {                                
                                if (!decimal.TryParse(txtUnitPrice.Text, out unitPrice))
                                {
                                    txtQty.Text = qtyInt.ToString();
                                    txtUnitPrice.Focus();
                                    return;
                                }

                                txtQty.Text = qtyInt.ToString();
                                txtUnitPrice.Text = unitPrice.ToString("N2");
                                txtValue.Text = (qtyInt * unitPrice).ToString("N2");
                                lbtnAdd.Focus();
                            }
                            else 
                            {
                                txtQty.Text = string.Empty;
                                lblWarning.Text = "Cannot add decimal values for item quantities.";
                                divWarning.Visible = true;
                                txtQty.Focus();
                                return;
                            }
                        }
                        else
                        {
                            if (!decimal.TryParse(txtQty.Text, out qty))
                            {
                                txtQty.Focus();
                                return;
                            }
                            if (!decimal.TryParse(txtUnitPrice.Text, out unitPrice))
                            {
                                txtQty.Text = qty.ToString("N2");
                                txtUnitPrice.Focus();
                                return;
                            }

                            txtQty.Text = qty.ToString("N2");
                            txtUnitPrice.Text = unitPrice.ToString("N2");
                            txtValue.Text = (qty * unitPrice).ToString("N2");
                            lbtnAdd.Focus();
                        }
                    }
                }                
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
                    txtQty.Text = string.Empty;
                    txtUnitPrice.Text = string.Empty;
                    lblWarning.Text = "Please enter Item.";
                    divWarning.Visible = true;
                    return;
                }
                if (string.IsNullOrEmpty(txtUnitPrice.Text))
                {
                    txtValue.Text = string.Empty;
                    lblWarning.Text = "Please enter UnitPrice.";
                    divWarning.Visible = true;
                    return;
                }
                decimal qty;
                decimal unitPrice;
                if (!decimal.TryParse(txtUnitPrice.Text, out unitPrice))
                {
                    txtUnitPrice.Focus();
                    txtUnitPrice.Text = string.Empty;
                    lblWarning.Text = "Please enter valid unit price.";
                    divWarning.Visible = true;
                    return;
                }

                string uom = (string)ViewState["UOM"];
                DataTable dt = CHNLSVC.Inventory.IsItemUOMDecimalAllow(uom);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string isdecimalallow = dr["msu_isdecimal"].ToString();
                        if (isdecimalallow != "1")
                        {
                            int qtyInt;
                            if (int.TryParse(txtQty.Text.Trim(), out qtyInt))
                            {
                                txtQty.Text = qtyInt.ToString();
                                txtUnitPrice.Text = unitPrice.ToString("N2");
                                txtValue.Text = (qtyInt * unitPrice).ToString("N2");
                                lbtnAdd.Focus();
                            }
                            else
                            {
                                txtQty.Text = string.Empty;
                                lblWarning.Text = "Cannot add decimal values for item quantities.";
                                divWarning.Visible = true;
                                txtQty.Focus();
                                return;
                            }
                        }
                        else
                        {
                            if (!decimal.TryParse(txtQty.Text, out qty))
                            {
                                txtQty.Focus();
                                return;
                            }                            

                            txtQty.Text = qty.ToString("N2");
                            txtUnitPrice.Text = unitPrice.ToString("N2");
                            txtValue.Text = (qty * unitPrice).ToString("N2");
                            lbtnAdd.Focus();
                        }
                    }
                }
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
                    lblWarning.Text = "Please enter item details.";
                    divWarning.Visible = true;
                    return;
                }
                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    lblWarning.Text = "Please enter quantity.";
                    divWarning.Visible = true;
                    return;
                }
                if (string.IsNullOrEmpty(txtUnitPrice.Text))
                {
                    lblWarning.Text = "Please enter unit price.";
                    divWarning.Visible = true;
                    return;
                }

                List<INT_REQ_ITM> int_req_itm = new List<INT_REQ_ITM>();

                if (ViewState["int_req_itm"] != null)
                {
                    int_req_itm = (List<INT_REQ_ITM>)ViewState["int_req_itm"];
                }

                int seqno = 0;
                int lineNo = 1;
                if (int_req_itm.Count > 0)
                {
                    if (int_req_itm.Exists(x => x.ITRI_ITM_CD == txtItem.Text))
                    {
                        lblWarning.Text = "Duplicate items are not allowed.";
                        divWarning.Visible = true;
                        return;
                    }

                    seqno = int_req_itm[0].ITRI_SEQ_NO;

                    lineNo = int_req_itm.Max(x => x.ITRI_LINE_NO) + 1;
                }                

                INT_REQ_ITM item = new INT_REQ_ITM();
                item.ITRI_SEQ_NO     = 0;
                item.ITRI_LINE_NO    = lineNo;  
                item.ITRI_ITM_CD     = txtItem.Text;
                item.DESCRIPTION     = txtDescription.Text;
                item.MODEL           = txtModel.Text;

               

                string com = Session["UserCompanyCode"].ToString();
                if (com == "LRP")
                {
                    item.ITRI_ITM_STUS = "GDLP";
                }
                else
                {
                    item.ITRI_ITM_STUS = "GOD";
                }
                item.ITRI_QTY        = Convert.ToDecimal(txtQty.Text); 
                item.ITRI_UNIT_PRICE = Convert.ToDecimal(txtUnitPrice.Text);
                item.AMOUNT          = Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text); 
                item.ITRI_APP_QTY    = Convert.ToDecimal(0); 
                item.ITRI_RES_NO     = ""; 
                item.ITRI_NOTE       = ""; 
                item.ITRI_MITM_CD    = ""; 
                item.ITRI_MITM_STUS  = ""; 
                item.ITRI_MQTY       = Convert.ToDecimal(0); 
                item.ITRI_BQTY       = Convert.ToDecimal(0); 
                item.ITRI_ITM_COND   = ""; 
                item.ITRI_JOB_NO     = ""; 
                item.ITRI_JOB_LINE   = 0;

                int_req_itm.Add(item);

                grdItem.DataSource = int_req_itm;
                grdItem.DataBind();

                txtTotalAmount.Text = int_req_itm.Sum(x => x.AMOUNT).ToString("N2");

                ViewState["int_req_itm"] = int_req_itm;

                txtItem.Text = string.Empty;
                txtItem.ReadOnly = false;
                txtQty.Text = string.Empty;
                txtUnitPrice.Text = string.Empty;
                txtValue.Text = string.Empty;

                txtModel.Text = string.Empty;
                txtDescription.Text = string.Empty;
                
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
                txtQty.Text = string.Empty;
                txtUnitPrice.Text = string.Empty;
                txtValue.Text = string.Empty;

                txtModel.Text = string.Empty;
                txtDescription.Text = string.Empty;                
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnGrdItemDalete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtRequestNo.Text))
                {
                    lblWarning.Text = "Can not delete items.";
                    divWarning.Visible = true;
                    return;
                }

                if (hdnItemDelete.Value == "Yes")
                {
                    LinkButton btn = (LinkButton)sender;
                    GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                    List<INT_REQ_ITM> item = (List<INT_REQ_ITM>)ViewState["int_req_itm"];
                    item.RemoveAt(grdr.RowIndex);

                    txtTotalAmount.Text = item.Sum(x => x.AMOUNT).ToString("N2");

                    ViewState["int_req_itm"] = item;
                    grdItem.DataSource = item;
                    grdItem.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnGrdItemEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                grdItem.EditIndex = grdr.RowIndex;//e.NewEditIndex;
                List<INT_REQ_ITM> item = (List<INT_REQ_ITM>)ViewState["int_req_itm"];
                grdItem.DataSource = item;
                grdItem.DataBind();

                TextBox txtQtyEdit = ((TextBox)grdItem.Rows[grdr.RowIndex].FindControl("txtQtyEdit"));
                txtQtyEdit.Focus();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnGrdItemCancel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                //e.Cancel = true;
                grdItem.EditIndex = -1;
                List<INT_REQ_ITM> item = (List<INT_REQ_ITM>)ViewState["int_req_itm"];
                grdItem.DataSource = item;
                grdItem.DataBind();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnGrdItemUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                List<INT_REQ_ITM> item = (List<INT_REQ_ITM>)ViewState["int_req_itm"];

                decimal qty;
                decimal unitrate;

                if (!decimal.TryParse(((TextBox)grdItem.Rows[grdr.RowIndex].FindControl("txtUnitPrice")).Text, out unitrate))
                {
                    lblWarning.Text = "Please enter valid item unit price.";
                    divWarning.Visible = true;
                    return;
                }

                DataTable dtItem = CHNLSVC.Financial.GetItemDetails(((Label)grdItem.Rows[grdr.RowIndex].FindControl("lblItemCodeEdit")).Text);
                ViewState["UOM"] = dtItem.Rows[0]["MI_ITM_UOM"].ToString();
               
                string uom = (string)ViewState["UOM"];
                DataTable dt = CHNLSVC.Inventory.IsItemUOMDecimalAllow(uom);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string isdecimalallow = dr["msu_isdecimal"].ToString();
                        if (isdecimalallow != "1")
                        {
                            int qtyInt;
                            if (int.TryParse(((TextBox)grdItem.Rows[grdr.RowIndex].FindControl("txtQtyEdit")).Text, out qtyInt))
                            {
                                item[grdr.RowIndex].ITRI_QTY = qtyInt;
                                item[grdr.RowIndex].ITRI_UNIT_PRICE = unitrate;
                                item[grdr.RowIndex].AMOUNT = qtyInt * unitrate;
                                grdItem.EditIndex = -1;
                                ViewState["int_req_itm"] = item;
                                grdItem.DataSource = item;
                                grdItem.DataBind();

                                txtTotalAmount.Text = item.Sum(x => x.AMOUNT).ToString("N2");
                            }
                            else
                            {
                                lblWarning.Text = "Not allowed decimal qty for this item.";
                                divWarning.Visible = true;
                                return;
                            }
                        }
                        else
                        {
                            if (!decimal.TryParse(((TextBox)grdItem.Rows[grdr.RowIndex].FindControl("txtQtyEdit")).Text, out qty))
                            {
                                lblWarning.Text = "Please Enter valid Item Qty.";
                                divWarning.Visible = true;
                                return;
                            }

                            item[grdr.RowIndex].ITRI_QTY = qty;
                            item[grdr.RowIndex].ITRI_UNIT_PRICE = unitrate;
                            item[grdr.RowIndex].AMOUNT = qty * unitrate;
                            grdItem.EditIndex = -1;
                            ViewState["int_req_itm"] = item;
                            grdItem.DataSource = item;
                            grdItem.DataBind();

                            txtTotalAmount.Text = item.Sum(x => x.AMOUNT).ToString("N2");
                        }
                    }
                }

                //if (!decimal.TryParse(((TextBox)grdItem.Rows[grdr.RowIndex].FindControl("txtQtyEdit")).Text, out qty))
                //{
                //    lblWarning.Text = "Please Enter valid Item Qty.";
                //    divWarning.Visible = true;
                //    return;
                //}
                
                //item[grdr.RowIndex].ITRI_QTY = qty;
                //item[grdr.RowIndex].ITRI_UNIT_PRICE = unitrate;
                //item[grdr.RowIndex].AMOUNT = qty * unitrate;
                //grdItem.EditIndex = -1;
                //ViewState["int_req_itm"] = item;
                //grdItem.DataSource = item;
                //grdItem.DataBind();

                //txtTotalAmount.Text = item.Sum(x => x.AMOUNT).ToString("N2");

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
                    if (!DateTime.TryParse(txtExpectedDate.Text, out exDate))
                    {
                        lblWarning.Text = "Please select valid Expected Date.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (date.Date > exDate.Date)
                    {
                        lblWarning.Text = "Date less than or equal to Expected date.";
                        divWarning.Visible = true;
                        return;
                    }                    
                    if (string.IsNullOrEmpty(txtRequestType.Text))
                    {
                        lblWarning.Text = "Please enter the request type.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtRefNo.Text))
                    {
                        lblWarning.Text = "Please enter the reference number.";
                        divWarning.Visible = true;
                        return;
                    }
                    //if (string.IsNullOrEmpty(txtSalesExcecutive.Text))
                    //{
                    //    lblWarning.Text = "Please enter Sales Excecutive.";
                    //    divWarning.Visible = true;
                    //    return;
                    //}
                    if (string.IsNullOrEmpty(txtInvoiceCustomer.Text))
                    {
                        lblWarning.Text = "Please enter Invoice Customer.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (grdItem.Rows.Count == 0)
                    {
                        lblWarning.Text = "Please enter Items.";
                        divWarning.Visible = true;
                        return;
                    }

                    MasterAutoNumber masterAuto = new MasterAutoNumber();
                    masterAuto.Aut_cate_cd = Session["UserDefProf"].ToString();
                    masterAuto.Aut_cate_tp = txtSalesExcecutive.Text; 
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "SOREQ";
                    masterAuto.Aut_number = 5;
                    masterAuto.Aut_start_char = "SOREQ";
                    masterAuto.Aut_year = null;

                    INT_REQ int_req = new INT_REQ();
                    int_req.ITR_SEQ_NO         = 0;
                    int_req.ITR_COM            = Session["UserCompanyCode"].ToString(); 
                    int_req.ITR_REQ_NO         = ""; 
                    int_req.ITR_TP             = "SOREQ"; 
                    int_req.ITR_SUB_TP         = txtRequestType.Text; 
                    int_req.ITR_LOC            = Session["UserDefProf"].ToString(); 
                    int_req.ITR_REF            = txtRefNo.Text; 
                    int_req.ITR_DT             = date; 
                    int_req.ITR_EXP_DT         = exDate; 
                    int_req.ITR_STUS           = "A"; 
                    int_req.ITR_JOB_NO         = ""; 
                    int_req.ITR_BUS_CODE       = txtInvoiceCustomer.Text; 
                    int_req.ITR_NOTE           = txtRemarks.Text; 
                    int_req.ITR_ISSUE_FROM     = ""; 
                    int_req.ITR_REC_TO         = ""; 
                    int_req.ITR_DIRECT         = 0; 
                    int_req.ITR_COUNTRY_CD     = ""; 
                    int_req.ITR_TOWN_CD        = ""; 
                    int_req.ITR_CUR_CODE       = ""; 
                    int_req.ITR_EXG_RATE       = Convert.ToDecimal(0); 
                    int_req.ITR_COLLECTOR_ID   = ""; 
                    int_req.ITR_COLLECTOR_NAME = ""; 
                    int_req.ITR_ACT            = 1; 
                    int_req.ITR_CRE_BY         = Session["UserID"].ToString(); 
                    int_req.ITR_CRE_DT         = DateTime.Now; 
                    int_req.ITR_MOD_BY         = ""; 
                    int_req.ITR_MOD_DT         = DateTime.Now; 
                    int_req.ITR_SESSION_ID     = Session["SessionID"].ToString(); 
                    int_req.ITR_ANAL1          = ""; 
                    int_req.ITR_ANAL2          = ""; 
                    int_req.ITR_ANAL3          = txtSalesExcecutive.Text; 
                    int_req.ITR_ISSUE_COM      = ""; 
                    int_req.ITR_GRAN_OPT1      = 0; 
                    int_req.ITR_GRAN_OPT2      = 0; 
                    int_req.ITR_GRAN_OPT3      = 0; 
                    int_req.ITR_GRAN_OPT4      = 0; 
                    int_req.ITR_GRAN_NSTUS     = ""; 
                    int_req.ITR_GRAN_APP_BY    = ""; 
                    int_req.ITR_GRAN_NARRT     = ""; 
                    int_req.ITR_GRAN_APP_NOTE  = ""; 
                    int_req.ITR_GRAN_APP_STUS  = ""; 
                    int_req.ITR_JOB_LINE       = 0; 
                    int_req.ITR_APP_BY         = Session["UserID"].ToString();  
                    int_req.ITR_APP_DT         = DateTime.Now;


                    List<INT_REQ_ITM> int_req_itm = (List<INT_REQ_ITM>)ViewState["int_req_itm"];

                    int row_aff = 0;
                    string msg;

                    row_aff = CHNLSVC.Sales.SaveSalesOrderRequest(int_req, int_req_itm, masterAuto, out msg);

                    if (row_aff >= 1)
                    {
                        lblSuccess.Text = "Successfully created the sales order request number: " + msg;
                        divSuccess.Visible = true;
                        PageClear();
                        return;
                    }
                    else
                    {
                        lblWarning.Text = "Insert Fail ...  .";
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
                    if (!DateTime.TryParse(txtExpectedDate.Text, out exDate))
                    {
                        lblWarning.Text = "Please select valid Expected Date.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtRequestType.Text))
                    {
                        lblWarning.Text = "Please enter the request type.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtRefNo.Text))
                    {
                        lblWarning.Text = "Please enter reference number.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtSalesExcecutive.Text))
                    {
                        lblWarning.Text = "Please enter Sales Excecutive.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtInvoiceCustomer.Text))
                    {
                        lblWarning.Text = "Please enter Invoice Customer.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (grdItem.Rows.Count == 0)
                    {
                        lblWarning.Text = "Please enter Items.";
                        divWarning.Visible = true;
                        return;
                    }

                    INT_REQ int_req = new INT_REQ();

                    INT_REQ int_reqUpdate = new INT_REQ();
                    int_reqUpdate = (INT_REQ)ViewState["int_req"];

                    int_req.ITR_SEQ_NO = int_reqUpdate.ITR_SEQ_NO;
                    int_req.ITR_COM = Session["UserCompanyCode"].ToString();
                    int_req.ITR_REQ_NO = txtRequestNo.Text;
                    int_req.ITR_TP = "SOREQ";
                    int_req.ITR_SUB_TP = txtRequestType.Text;
                    int_req.ITR_LOC = Session["UserDefProf"].ToString();
                    int_req.ITR_REF = txtRefNo.Text;
                    int_req.ITR_DT = date;
                    int_req.ITR_EXP_DT = exDate;
                    int_req.ITR_STUS = "A";
                    int_req.ITR_JOB_NO = "";
                    int_req.ITR_BUS_CODE = txtInvoiceCustomer.Text;
                    int_req.ITR_NOTE = txtRemarks.Text;
                    int_req.ITR_ISSUE_FROM = "";
                    int_req.ITR_REC_TO = "";
                    int_req.ITR_DIRECT = 0;
                    int_req.ITR_COUNTRY_CD = "";
                    int_req.ITR_TOWN_CD = "";
                    int_req.ITR_CUR_CODE = "";
                    int_req.ITR_EXG_RATE = Convert.ToDecimal(0);
                    int_req.ITR_COLLECTOR_ID = "";
                    int_req.ITR_COLLECTOR_NAME = "";
                    int_req.ITR_ACT = 1;
                    int_req.ITR_CRE_BY = Session["UserID"].ToString();
                    int_req.ITR_CRE_DT = DateTime.Now;
                    int_req.ITR_MOD_BY = "";
                    int_req.ITR_MOD_DT = DateTime.Now;
                    int_req.ITR_SESSION_ID = Session["SessionID"].ToString();
                    int_req.ITR_ANAL1 = "";
                    int_req.ITR_ANAL2 = "";
                    int_req.ITR_ANAL3 = txtSalesExcecutive.Text;
                    int_req.ITR_ISSUE_COM = "";
                    int_req.ITR_GRAN_OPT1 = 0;
                    int_req.ITR_GRAN_OPT2 = 0;
                    int_req.ITR_GRAN_OPT3 = 0;
                    int_req.ITR_GRAN_OPT4 = 0;
                    int_req.ITR_GRAN_NSTUS = "";
                    int_req.ITR_GRAN_APP_BY = "";
                    int_req.ITR_GRAN_NARRT = "";
                    int_req.ITR_GRAN_APP_NOTE = "";
                    int_req.ITR_GRAN_APP_STUS = "";
                    int_req.ITR_JOB_LINE = 0;
                    int_req.ITR_APP_BY = Session["UserID"].ToString();
                    int_req.ITR_APP_DT = DateTime.Now;


                    List<INT_REQ_ITM> int_req_itm = (List<INT_REQ_ITM>)ViewState["int_req_itm"];

                    int row_aff = 0;
                    string msg;

                    row_aff = CHNLSVC.Sales.UpdateSalesOrderRequest(int_req, int_req_itm, out msg);
                    
                    if (row_aff >= 1)
                    {
                        lblSuccess.Text = "Successfully updated the sales order request number : " + txtRequestNo.Text;
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
                    if (string.IsNullOrEmpty(txtRequestNo.Text))
                    {
                        txtRequestNo.Text = string.Empty;
                        lblWarning.Text = "Please Enter Request No.";
                        divWarning.Visible = true;
                        return;
                    }

                    int row_aff = CHNLSVC.Sales.UpdateStatus_INT_REQ(txtRequestNo.Text, "C");

                    if (row_aff == 1)
                    {
                        lblSuccess.Text = "Successfully canceled the sales order request number: " + txtRequestNo.Text;
                        divSuccess.Visible = true;
                        PageClear();
                        return;
                    }
                    else
                    {
                        lblWarning.Text = "Cancel Fail ...  .";
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
                //RequestType
                if (lblvalue.Text == "418")
                {
                    txtRequestType.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                //SearchExecutive
                if (lblvalue.Text == "419")
                {
                    txtSalesExcecutive.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtSalesExcecutiveName.Text = grdResult.SelectedRow.Cells[3].Text;
                    txtSalesExcecutive_TextChanged(null, null);
                }
                //InvoiceCustomer
                if (lblvalue.Text == "401")
                {
                    txtInvoiceCustomer.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtInvoiceCustomerName.Text = grdResult.SelectedRow.Cells[3].Text;
                    txtInvoiceCustomer_TextChanged(null, null);
                }
                //RequestNo
                if (lblvalue.Text == "420")
                {
                    txtRequestNo.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtRequestNo_TextChanged(null, null);
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
                DataTable result = CHNLSVC.CommonSearch.SearchItemforchange(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "407";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            //RequestType
            else if (lblvalue.Text == "418")
            {               
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RequestType);
                DataTable result = CHNLSVC.CommonSearch.SearchRequestType(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "418";
                ViewState["SEARCH"] = result;
                UserPopup.Show(); 
            }
            //SearchExecutive
            else if (lblvalue.Text == "419")
            {               
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesExcecutive);
                DataTable result = CHNLSVC.CommonSearch.SearchExecutive(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "419";
                ViewState["SEARCH"] = result;
                UserPopup.Show(); 
            }
            //InvoiceCustomer
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
            //RequestNo
            else if (lblvalue.Text == "420")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RequestNo);
                DataTable result = CHNLSVC.CommonSearch.Search_INT_REQ(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "420";
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
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "C" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RequestType:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SalesExcecutive:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RequestNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + "SOREQ" + seperator + txtSalesExcecutive.Text + seperator);
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
        private void PopulateRequestData()
        {
            if (string.IsNullOrEmpty(txtRequestNo.Text))
            {
                txtRequestNo.Text = string.Empty;
                lblWarning.Text = "Please Enter Request No.";
                divWarning.Visible = true;
                return;
            }

            INT_REQ int_req = new INT_REQ();
            int_req = CHNLSVC.Sales.GetRequest(txtRequestNo.Text);

            if (int_req == null)
            {
                txtRequestNo.Text = string.Empty;
                lblWarning.Text = "Invalid Request Details.";
                divWarning.Visible = true;
                return;
            }

            ViewState["int_req"] = int_req;

            txtDate.Text = int_req.ITR_DT.ToString("dd/MMM/yyyy");
            txtExpectedDate.Text = int_req.ITR_EXP_DT.ToString("dd/MMM/yyyy");
            txtRequestType.Text = int_req.ITR_SUB_TP;
            txtRefNo.Text = int_req.ITR_REF;
            txtSalesExcecutive.Text = int_req.ITR_ANAL3;
            txtInvoiceCustomer.Text = int_req.ITR_BUS_CODE;
            txtRemarks.Text = int_req.ITR_NOTE;
            if (int_req.ITR_STUS == "A")
            {
                txtOrderStatus.Text = "APPROVED";
            }
            else if (int_req.ITR_STUS == "C")
            {
                txtOrderStatus.Text = "CANCELLED";
            }
            else if (int_req.ITR_STUS == "P" || int_req.ITR_STUS =="S")
            {
                txtOrderStatus.Text = "PENDING";
            }
            else if (int_req.ITR_STUS == "F")
            {
                txtOrderStatus.Text = "FINALIZED";
            }
            else
            {
                txtOrderStatus.Text = "";
            }
            txtOrdStus.Text = int_req.ITR_STUS;

            txtInvoiceCustomer_TextChanged(null, null);

            List<INT_REQ_ITM> int_req_itm = new List<INT_REQ_ITM>();
            int_req_itm = CHNLSVC.Sales.GetRequestItem(int_req.ITR_SEQ_NO);

            if (int_req_itm.Count <=0)
            {
                txtRequestNo.Text = string.Empty;
                lblWarning.Text = "Invalid Request Item Details.";
                divWarning.Visible = true;
                return;
            }

            ViewState["DaleteVisibility"] = false;

            grdItem.DataSource = int_req_itm;
            grdItem.DataBind();

            ViewState["int_req_itm"] = int_req_itm;

            txtTotalAmount.Text = int_req_itm.Sum(x => x.AMOUNT).ToString("N2");            

            lbtnSave.Enabled = false;
            lbtnSave.OnClientClick = "return Enable();";
            lbtnSave.CssClass = "buttoncolor";

            lbtnUpdate.Enabled = false;
            lbtnUpdate.OnClientClick = "return Enable();";
            lbtnUpdate.CssClass = "buttoncolor";
            lbtnCancel.Enabled = false;
            lbtnCancel.OnClientClick = "return Enable();";
            lbtnCancel.CssClass = "buttoncolor";

            if (int_req_itm.Sum(x => x.ITRI_APP_QTY) == 0 && int_req.ITR_STUS == "A")
            {
                lbtnUpdate.Enabled = true;
                lbtnUpdate.OnClientClick = "ConfirmUpdate();";
                lbtnUpdate.CssClass = "buttonUndocolor";

                lbtnCancel.Enabled = true;
                lbtnCancel.OnClientClick = "ConfirmCancel();";
                lbtnCancel.CssClass = "buttonUndocolor";
            }

            lbtnAdd.Visible = false;
            lbtnClearItem.Visible = false;
            ItemAdd.Visible = false;

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
        public void SalesExcecutive()
        {
            try
            {
                DataTable dt = CHNLSVC.Sales.Select_EMP_ID(Session["UserID"].ToString());
                if (dt.Rows.Count > 0)
                {
                    txtSalesExcecutive.Text = dt.Rows[0]["EMPLOYEE ID"].ToString();
                    txtSalesExcecutiveName.Text = dt.Rows[0]["USER NAME"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        private void PageClear()
        {
            txtRequestNo.Text = string.Empty;

            txtDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lbtnDate.Enabled = true;
            txtExpectedDate.Text = DateTime.Now.AddMonths(1).ToString("dd/MMM/yyyy");

            txtRequestType.Text = string.Empty;
            txtRefNo.Text = string.Empty;

            txtSalesExcecutive.Text = string.Empty;
            txtSalesExcecutiveName.Text = string.Empty;
            txtInvoiceCustomer.Text = string.Empty;
            txtInvoiceCustomerName.Text = string.Empty;

            SalesExcecutive();
            txtSalesExcecutive.ReadOnly = true;
            //lbtnSalesExcecutive.Enabled = true;
            txtInvoiceCustomer.Text = string.Empty;
            txtOrderStatus.Text = "NEW REQUEST";
            txtOrdStus.Text = "";
            txtItem.Text = string.Empty;
            txtItem.ReadOnly = false;
            txtQty.Text = string.Empty;
            txtUnitPrice.Text = string.Empty;

            txtModel.Text = string.Empty;
            txtDescription.Text = string.Empty;

            lbtnAdd.Visible = true;
            lbtnClearItem.Visible = true;
            ItemAdd.Visible = true;

            grdItem.DataSource = new int[] { };
            grdItem.DataBind();

            txtRemarks.Text = string.Empty;
            txtTotalAmount.Text = string.Empty;

            ViewState["int_req"] = null;
            ViewState["int_req_itm"] = null;
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

        }        

    }
}