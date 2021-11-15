using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.PI
{
    public partial class PerformaInvoice : Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ValidateTrue();
                if (!IsPostBack)
                {
                    PageClear();

                    DdlPaymentTermsDataPopulate();
                    DdlTradeTermsDataPopulate();
                    ddlSubPaymentTerms.Items.Insert(0, new ListItem(" - Select - ", "0"));
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnPINo_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PINO);
                DataTable result = CHNLSVC.CommonSearch.SEARCH_PINO_PI(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "400";
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
        protected void lbtnOrderPlanNo_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OrderPlanNo);
                DataTable result = CHNLSVC.CommonSearch.SearchOrderPlanNo(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "403";
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
        protected void ddlPaymentTerms_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DdlSubPaymentTermsDataPopulate("IPM", ddlPaymentTerms.Text);
                txtAccount.Text = string.Empty;
                ddlSubPaymentTerms.Focus();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void ddlTradeTerms_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GrdElementDataPopulate(Session["UserCompanyCode"].ToString(), "TOT", ddlTradeTerms.SelectedItem.Value);
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void chkOrderPeriod_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
                if (string.IsNullOrEmpty(txtOrderPlanNo.Text))
                {
                    lblWarning.Text = "Please select Order Plan No.";
                    divWarning.Visible = true;
                    return;
                }
                if (string.IsNullOrEmpty(grdOrderPeriod.Rows[row.RowIndex].Cells[1].Text))
                {
                    lblWarning.Text = "Invalid Year.";
                    divWarning.Visible = true;
                    return;
                }
                HiddenField hdnOrderPeriod = (HiddenField)grdOrderPeriod.Rows[row.RowIndex].FindControl("hdnOrderPeriod");
                if (string.IsNullOrEmpty(hdnOrderPeriod.Value))
                {
                    lblWarning.Text = "Invalid Month.";
                    divWarning.Visible = true;
                    return;
                }
                CheckBox chk = (CheckBox)grdOrderPeriod.Rows[row.RowIndex].FindControl("chkOrderPeriod");
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();

                dt1.Columns.Add("IOI_ITM_CD", typeof(string));
                dt1.Columns.Add("IOI_DESC", typeof(string));
                dt1.Columns.Add("IOI_MODEL", typeof(string));
                dt1.Columns.Add("IOI_COLOR", typeof(string));
                //dt1.Columns.Add("IOI_PARTNO", typeof(string));
                dt1.Columns.Add("IOI_UOM", typeof(string));
                dt1.Columns.Add("IOI_ITM_TP", typeof(string));
                dt1.Columns.Add("IOI_BAL_QTY", typeof(double));
                dt1.Columns.Add("IOI_UNIT_RT", typeof(double));
                dt1.Columns.Add("ITEMVLUE", typeof(decimal));
                dt1.Columns.Add("IOI_YY", typeof(int));
                dt1.Columns.Add("IOI_MM", typeof(int));
                dt1.Columns.Add("LINE", typeof(int));
                dt1.Columns.Add("KIT", typeof(Int64));
                dt1.Columns.Add("KITCODE", typeof(string));
                dt1.Columns.Add("OP_LINE", typeof(int));
                dt1.Columns.Add("DELETE", typeof(decimal));

                if (chk.Checked)
                {
                    dt = CHNLSVC.Financial.GetOrderItem(txtOrderPlanNo.Text, Convert.ToInt32(grdOrderPeriod.Rows[row.RowIndex].Cells[1].Text), Convert.ToInt32(hdnOrderPeriod.Value));
                    if (ViewState["OrderItem"] != null)
                    {
                        dt1 = (DataTable)ViewState["OrderItem"];
                       // dt1.Columns.Add("IOI_PARTNO", typeof(string));

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow dr = dt1.NewRow();
                            dr["IOI_ITM_CD"] = dt.Rows[i]["IOI_ITM_CD"].ToString();
                            dr["IOI_DESC"] = dt.Rows[i]["IOI_DESC"].ToString();
                            dr["IOI_MODEL"] = dt.Rows[i]["IOI_MODEL"].ToString();
                            dr["IOI_COLOR"] = dt.Rows[i]["IOI_COLOR"].ToString();
                            //dr["IOI_PARTNO"] = dt.Rows[i]["IOI_COLOR"].ToString();                           
                            dr["IOI_UOM"] = dt.Rows[i]["IOI_UOM"].ToString();
                            dr["IOI_ITM_TP"] = dt.Rows[i]["IOI_ITM_TP"].ToString();
                            dr["IOI_BAL_QTY"] = dt.Rows[i]["IOI_BAL_QTY"];
                            dr["IOI_UNIT_RT"] = dt.Rows[i]["IOI_UNIT_RT"];
                            dr["ITEMVLUE"] = dt.Rows[i]["ITEMVLUE"]; //Convert.ToDouble(id.IPI_QTY) * Convert.ToDouble(id.IPI_UNIT_RT);
                            dr["IOI_YY"] = 0;
                            dr["IOI_MM"] = 0;
                            dr["LINE"] = dt.Rows[i]["LINE"];
                            dr["KIT"] = dt.Rows[i]["KIT"];
                            dr["KITCODE"] = "";
                            dr["OP_LINE"] = 1;//For check updated or not
                            dr["DELETE"] = 1;//For New Item
                            dt1.Rows.Add(dr);
                        }


                        //dt.Merge((DataTable)ViewState["OrderItem"]);
                        //dt.Merge(dt1);
                    }
                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DataRow dr = dt1.NewRow();
                            dr["IOI_ITM_CD"] = dt.Rows[i]["IOI_ITM_CD"].ToString();
                            dr["IOI_DESC"] = dt.Rows[i]["IOI_DESC"].ToString();
                            dr["IOI_MODEL"] = dt.Rows[i]["IOI_MODEL"].ToString();
                            dr["IOI_COLOR"] = dt.Rows[i]["IOI_COLOR"].ToString();
                            //dr["IOI_PARTNO"] = dt.Rows[i]["MI_PART_NO"].ToString();
                            dr["IOI_UOM"] = dt.Rows[i]["IOI_UOM"].ToString();
                            dr["IOI_ITM_TP"] = dt.Rows[i]["IOI_ITM_TP"].ToString();
                            dr["IOI_BAL_QTY"] = dt.Rows[i]["IOI_BAL_QTY"];
                            dr["IOI_UNIT_RT"] = dt.Rows[i]["IOI_UNIT_RT"];
                            dr["ITEMVLUE"] = dt.Rows[i]["ITEMVLUE"]; //Convert.ToDouble(id.IPI_QTY) * Convert.ToDouble(id.IPI_UNIT_RT);
                            dr["IOI_YY"] = 0;
                            dr["IOI_MM"] = 0;
                            dr["LINE"] = dt.Rows[i]["LINE"];
                            dr["KIT"] = dt.Rows[i]["KIT"];
                            dr["KITCODE"] = "";
                            dr["OP_LINE"] = 1;//For check updated or not
                            dr["DELETE"] = 1;//For New Item
                            dt1.Rows.Add(dr);
                        }
                    }

                    DataView dv = dt1.DefaultView;
                    dv.Sort = "IOI_ITM_CD";
                    DataTable sortedDT = dv.ToTable();

                    ViewState["OrderItem"] = sortedDT;
                    grdInvoiceDetails.DataSource = sortedDT;
                    grdInvoiceDetails.DataBind();
                }
                else
                {
                    dt1 = (DataTable)ViewState["OrderItem"];
                    int count = dt1.Rows.Count;
                    int i = 0;
                    while (i < dt1.Rows.Count)
                    {
                        DataRow dr = dt1.Rows[i];
                        //if ((grdInvoiceDetails.Rows[i].Cells[9].Text == grdOrderPeriod.Rows[row.RowIndex].Cells[1].Text) &&
                        //        (grdInvoiceDetails.Rows[i].Cells[10].Text == hdnOrderPeriod.Value))
                        //    dt.Rows.Remove(dr);
                        if ((dt1.Rows[i][9].ToString() == grdOrderPeriod.Rows[row.RowIndex].Cells[1].Text) &&
                                (dt1.Rows[i][10].ToString() == hdnOrderPeriod.Value))
                            dt1.Rows.Remove(dr);
                        else
                            i = i + 1;
                    }
                    DataView dv = dt1.DefaultView;
                    dv.Sort = "IOI_ITM_CD";
                    DataTable sortedDT = dv.ToTable();

                    ViewState["OrderItem"] = sortedDT;
                    grdInvoiceDetails.DataSource = sortedDT;
                    grdInvoiceDetails.DataBind();
                }

                GridInvoiceDetailsTotal(dt1);
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnModel_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportModel);
                DataTable result = CHNLSVC.CommonSearch.SearchModel(SearchParams);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "406";
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
        protected void lbtnItem_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportItem);
                DataTable result = CHNLSVC.CommonSearch.SearchItem(SearchParams, null, null);
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
                if (string.IsNullOrEmpty ( txtSupplier.Text.ToString()))
                {
                    string msg = "Please Select Supplier";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                    return;
                }
                decimal _qty = 0;
                if (!decimal.TryParse(txtQty.Text, out _qty))
                {
                    string msg = "Please enter valid quentity ! ";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                    return;
                }
                if (_qty <= 0)
                {
                    string msg = "Please enter valid quentity ! ";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                    return;
                }
                string _item = txtItem.Text.ToString();
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                 DataTable _result = CHNLSVC.Inventory.GetSuppierItem(SearchParams, "ITEM", "%" + _item);
                 if (_result.Rows.Count > 0)
                 {
                     if (string.IsNullOrEmpty(txtItem.Text))
                     {
                         lblWarning.Text = "Please select Item.";
                         divWarning.Visible = true;
                         //ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Please select Item.');", true);
                         return;

                     }
                     if (string.IsNullOrEmpty(txtColour.Text))
                     {
                         lblWarning.Text = "Please enter Colour.";
                         divWarning.Visible = true;
                         return;
                     }
                     if (string.IsNullOrEmpty(txtQty.Text))
                     {
                         lblWarning.Text = "Please enter qty.";
                         divWarning.Visible = true;
                         return;
                     }
                     if (string.IsNullOrEmpty(txtUnitRate.Text))
                     {
                         lblWarning.Text = "Please enter UnitRate.";
                         divWarning.Visible = true;
                         return;
                     }

                     double qty;
                     double unitrate;

                     DataTable dt = new DataTable();
                     dt.Columns.Add("IOI_ITM_CD", typeof(string));
                     dt.Columns.Add("IOI_DESC", typeof(string));
                     dt.Columns.Add("IOI_MODEL", typeof(string));
                     dt.Columns.Add("IOI_COLOR", typeof(string));
                     //dt.Columns.Add("MI_PART_NO", typeof(string));
                     dt.Columns.Add("IOI_UOM", typeof(string));
                     dt.Columns.Add("IOI_ITM_TP", typeof(string));
                     dt.Columns.Add("IOI_BAL_QTY", typeof(decimal));
                     dt.Columns.Add("IOI_UNIT_RT", typeof(decimal));
                     dt.Columns.Add("ITEMVLUE", typeof(decimal));
                     dt.Columns.Add("LINE", typeof(int));
                     dt.Columns.Add("KIT", typeof(int));
                     dt.Columns.Add("KITCODE", typeof(string));
                     dt.Columns.Add("OP_LINE", typeof(int));
                     dt.Columns.Add("DELETE", typeof(int));

                     if (ViewState["KitItem"].ToString() == "1")
                     {
                         DataTable dtKitH = new DataTable();
                         dtKitH.Columns.Add("IPK_ITM_CD", typeof(string));
                         dtKitH.Columns.Add("IOI_ITM_TP", typeof(string));
                         dtKitH.Columns.Add("IPK_QTY", typeof(double));
                         dtKitH.Columns.Add("IPK_UOM", typeof(string));
                         dtKitH.Columns.Add("IOI_UNIT_RT", typeof(double));
                         dtKitH.Columns.Add("IPK_OP_LINE", typeof(int));
                         dtKitH.Columns.Add("DELETE", typeof(int));

                         DataRow drH = dtKitH.NewRow();
                         drH["IPK_ITM_CD"] = txtItem.Text;
                         drH["IOI_ITM_TP"] = ddlItemType.SelectedItem.Value;
                         drH["IPK_QTY"] = Convert.ToDouble(txtQty.Text);
                         drH["IPK_UOM"] = lblUOM.Text;
                         drH["IOI_UNIT_RT"] = Convert.ToDouble(0);
                         drH["IPK_OP_LINE"] = 0;
                         drH["DELETE"] = 1;//For new item check delete 
                         dtKitH.Rows.Add(drH);

                         DataTable dtKitHMerge;
                         if (ViewState["PIKit"] != null)
                         {
                             dtKitHMerge = (DataTable)ViewState["PIKit"];
                             DataView dv = new DataView(dtKitHMerge);
                             dv.RowFilter = " IOI_ITM_TP like '%" + txtItem.Text + "%'";
                             if (dv.Count > 0)
                             {
                                 lblWarning.Text = "Duplicate Kit Items Are Not Allowed.";
                                 divWarning.Visible = true;
                                 return;
                             }

                             dtKitHMerge.Merge((DataTable)ViewState["PIKit"]);
                         }

                         DataTable dtKit = CHNLSVC.Financial.GetKitItem(txtItem.Text);
                         int count = dtKit.Rows.Count;
                         for (int i = 0; i < count; i++)
                         {
                             DataRow dr = dt.NewRow();
                             dr["IOI_ITM_CD"] = dtKit.Rows[i]["MI_CD"].ToString();
                             dr["IOI_DESC"] = dtKit.Rows[i]["MI_SHORTDESC"].ToString();
                             dr["IOI_MODEL"] = dtKit.Rows[i]["MI_MODEL"].ToString();
                             dr["IOI_COLOR"] = dtKit.Rows[i]["MI_COLOR_INT"].ToString();
                             //dr["MI_PART_NO"] = dtKit.Rows[i]["MI_COLOR_INT"].ToString();
                             dr["IOI_UOM"] = dtKit.Rows[i]["MI_ITM_UOM"].ToString();
                             dr["IOI_ITM_TP"] = dtKit.Rows[i]["MI_ITM_TP"].ToString();
                             dr["IOI_BAL_QTY"] = dtKit.Rows[i]["MIKC_NO_OF_UNIT"].ToString();
                             dr["IOI_UNIT_RT"] = dtKit.Rows[i]["MIKC_COST"].ToString();
                             dr["ITEMVLUE"] = (Convert.ToDouble(dtKit.Rows[i]["MIKC_NO_OF_UNIT"].ToString()) * Convert.ToDouble(dtKit.Rows[i]["MIKC_COST"].ToString()));
                             dr["LINE"] = dtKit.Rows[i]["MIKC_SEQ_NO"].ToString();
                             dr["KITCODE"] = txtItem.Text;
                             dr["KIT"] = 1;
                             dr["OP_LINE"] = 0;//For OP seq no to pi item table
                             dr["DELETE"] = 1;//For new item check delete 
                             dt.Rows.Add(dr);
                         }
                     }
                     else
                     {
                         if (!double.TryParse(txtQty.Text, out qty))
                         {
                             lblWarning.Text = "Please Enter valid Item Qty.";
                             divWarning.Visible = true;
                             return;
                         }
                         if (!double.TryParse(txtUnitRate.Text, out unitrate))
                         {
                             lblWarning.Text = "Please Enter valid Item Unit Rate.";
                             divWarning.Visible = true;
                             return;
                         }

                         DataRow dr = dt.NewRow();
                         dr["IOI_ITM_CD"] = txtItem.Text;
                         dr["IOI_DESC"] = lblDescription.Text;
                         dr["IOI_MODEL"] = txtModel.Text;
                         dr["IOI_COLOR"] = txtColour.Text;
                         //dr["MI_PART_NO"] = txtColour.Text;
                         dr["IOI_UOM"] = lblUOM.Text;
                         dr["IOI_ITM_TP"] = ddlItemType.SelectedItem.Value;
                         dr["IOI_BAL_QTY"] = qty;
                         dr["IOI_UNIT_RT"] = unitrate;
                         dr["ITEMVLUE"] = (qty * unitrate);
                         dr["LINE"] = 0;
                         dr["KITCODE"] = string.Empty;
                         dr["KIT"] = 0;
                         dr["OP_LINE"] = 0;//For OP seq no to pi item table
                         dr["DELETE"] = 1;//For new item check delete delete
                         dt.Rows.Add(dr);
                     }

                     DataTable dts;
                     if (ViewState["OrderItem"] != null)
                     {
                         dts = (DataTable)ViewState["OrderItem"];
                         DataView dv = new DataView(dts);
                         dv.RowFilter = " IOI_ITM_CD like '%" + txtItem.Text + "%'";
                         if (dv.Count > 0)
                         {
                             DataView dcheckqty = new DataView(dv.ToTable());
                             dcheckqty.RowFilter = "IOI_UNIT_RT = '" + txtUnitRate.Text + "'";
                             if (dcheckqty.Count == 0)
                             {
                                // lblWarning.Text = "You are not allowed to add this item with a different unit rate";
                                // divWarning.Visible = true;
                             }
                             else
                             {
                                 lblMssg.Text = "Do you want to update same item";
                                 dupicatepopup.Show();
                             }

                            // return;
                         }

                         dt.Merge((DataTable)ViewState["OrderItem"]);
                         dts.Merge(dt);

                     }

                     DataView dv2 = dt.DefaultView;
                     dv2.Sort = "IOI_ITM_CD";
                     DataTable sortedDT = dv2.ToTable();

                     ViewState["OrderItem"] = sortedDT;
                     grdInvoiceDetails.DataSource = sortedDT;
                     grdInvoiceDetails.DataBind();

                     GridInvoiceDetailsTotal(sortedDT);

                     txtModel.Text = string.Empty;
                     txtModel.ReadOnly = false;
                     lbtnModel.Enabled = true;
                     txtItem.Text = string.Empty;
                     ddlItemType.SelectedIndex = 0;
                     txtColour.Text = string.Empty;
                     txtQty.Text = string.Empty;
                     txtUnitRate.Text = string.Empty;
                     lblDescription.Text = string.Empty;
                     lblBrand.Text = string.Empty;
                     lblUOM.Text = string.Empty;
                     ViewState["KitItem"] = 0;
                 }
                 else
                 {
                     string msg = "Please allocate this item : " + _item + " for supplier";
                     ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                 }

               
            }
            catch (Exception ex)
            {
                //lblWarning.Text = "Error Occurred while processing...  " + ex;
                //divWarning.Visible = true;
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
                if (dt.Rows[0]["MI_ITM_TP"].ToString() == "K")
                {
                    ViewState["KitItem"] = 1;
                }
                txtModel.Text = dt.Rows[0]["MI_MODEL"].ToString();
                lblDescription.Text = dt.Rows[0]["MI_SHORTDESC"].ToString();
                lblBrand.Text = dt.Rows[0]["MI_BRAND"].ToString();
                lblUOM.Text = dt.Rows[0]["MI_ITM_UOM"].ToString();
                txtColour.Text = dt.Rows[0]["MI_COLOR_INT"].ToString();
                ddlItemType.SelectedValue = dt.Rows[0]["MI_ITM_TP"].ToString();

                txtModel.ReadOnly = true;
                lbtnModel.Enabled = false;
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
                txtModel.Text = string.Empty;
                txtModel.ReadOnly = false;
                lbtnModel.Enabled = true;
                txtItem.Text = string.Empty;
                ddlItemType.SelectedIndex = -1;
                txtColour.Text = string.Empty;
                txtQty.Text = string.Empty;
                txtUnitRate.Text = string.Empty;

                lblDescription.Text = string.Empty;
                lblBrand.Text = string.Empty;
                lblUOM.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtngrdInvoiceDetailsEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                grdInvoiceDetails.EditIndex = grdr.RowIndex;//e.NewEditIndex;
                grdInvoiceDetails.DataSource = (DataTable)ViewState["OrderItem"];
                grdInvoiceDetails.DataBind();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtngrdInvoiceDetailsCancel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                //e.Cancel = true;
                grdInvoiceDetails.EditIndex = -1;
                grdInvoiceDetails.DataSource = (DataTable)ViewState["OrderItem"];
                grdInvoiceDetails.DataBind();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtngrdInvoiceDetailsUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                DataTable dt = (DataTable)ViewState["OrderItem"];
                double qty = 0;
                double unitrate = 0;

                
                if (!double.TryParse(((TextBox)grdInvoiceDetails.Rows[grdr.RowIndex].FindControl("txtOrderQty")).Text, out qty))
                {
                    grdInvoiceDetails.EditIndex = grdr.RowIndex;//e.NewEditIndex;
                    grdInvoiceDetails.DataSource = (DataTable)ViewState["OrderItem"];
                    grdInvoiceDetails.DataBind();

                    lblWarning.Text = "Please Enter valid Item Qty.";
                    divWarning.Visible = true;
                    return;
                }
                if (!double.TryParse(((TextBox)grdInvoiceDetails.Rows[grdr.RowIndex].FindControl("txtUnitPrice")).Text, out unitrate))
                {
                    grdInvoiceDetails.EditIndex = grdr.RowIndex;//e.NewEditIndex;
                    grdInvoiceDetails.DataSource = (DataTable)ViewState["OrderItem"];
                    grdInvoiceDetails.DataBind();

                    lblWarning.Text = "Please Enter valid Item Unit Rate.";
                    divWarning.Visible = true;
                    return;
                }
                TextBox txtOrderQty = grdr.FindControl("txtOrderQty") as TextBox;
                decimal tmpDec = 0;
                if (decimal.TryParse(txtOrderQty.Text, out tmpDec))
                {
                    if (tmpDec <= 0)
                    {
                        grdInvoiceDetails.EditIndex = grdr.RowIndex;//e.NewEditIndex;
                        grdInvoiceDetails.DataSource = (DataTable)ViewState["OrderItem"];
                        grdInvoiceDetails.DataBind();

                        lblWarning.Text = "Please Enter valid Item Qty.";
                        divWarning.Visible = true;
                        return;
                    }
                }
                if (Convert.ToDouble(dt.Rows[grdr.RowIndex]["IOI_BAL_QTY"].ToString()) < qty)
                {
                    grdInvoiceDetails.EditIndex = grdr.RowIndex;//e.NewEditIndex;
                    grdInvoiceDetails.DataSource = (DataTable)ViewState["OrderItem"];
                    grdInvoiceDetails.DataBind();

                    lblWarning.Text = "Order qty less than or equal PI qty.";
                    divWarning.Visible = true;
                    return;
                }

                dt.Rows[grdr.RowIndex]["IOI_BAL_QTY"] = qty;
                dt.Rows[grdr.RowIndex]["IOI_UNIT_RT"] = unitrate;
                dt.Rows[grdr.RowIndex]["ITEMVLUE"] = qty * unitrate;
                if (Convert.ToInt32(dt.Rows[grdr.RowIndex]["OP_LINE"]) == 0)
                    dt.Rows[grdr.RowIndex]["OP_LINE"] = 1;
                grdInvoiceDetails.EditIndex = -1;
                ViewState["OrderItem"] = dt;
                grdInvoiceDetails.DataSource = dt;
                grdInvoiceDetails.DataBind();

                GridInvoiceDetailsTotal(dt);
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtngrdInvoiceDetailsDalete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                DataTable dt = (DataTable)ViewState["OrderItem"];
                dt.Rows.RemoveAt(grdr.RowIndex);

                ViewState["OrderItem"] = dt;
                grdInvoiceDetails.DataSource = dt;
                grdInvoiceDetails.DataBind();

                GridInvoiceDetailsTotal(dt);
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnDelAllItem_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                DataTable dt = new DataTable();


                ViewState["OrderItem"] = dt;
                grdInvoiceDetails.DataSource = dt;
                grdInvoiceDetails.DataBind();

                GridInvoiceDetailsTotal(dt);
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnBank_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16009))
                {
                    lblWarning.Text = "Sorry, You have no permission to add bank to this PI.( Advice: Required permission code : 16009)";
                    divWarning.Visible = true;
                    return;
                }

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Bank);
                DataTable result = CHNLSVC.CommonSearch.SearchBank(SearchParams);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "21";
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
        protected void lbtnAccount_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16009))
                {
                    lblWarning.Text = "Sorry, You have no permission to add Account to this PI.( Advice: Required permission code : 16009)";
                    divWarning.Visible = true;
                    return;
                }
                if (string.IsNullOrEmpty(txtBank.Text))
                {
                    lblWarning.Text = "Please select Bank.";
                    divWarning.Visible = true;
                    return;
                }
                if (ddlPaymentTerms.SelectedItem.Text == " - Select - ")
                {
                    lblWarning.Text = "Please select Payment Terms.";
                    divWarning.Visible = true;
                    return;
                }

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
                DataTable result = CHNLSVC.CommonSearch.GetBankAccountFacility(SearchParams, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "22";
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
        protected void txtETD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SetETADate();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void ddlPortofOrigin_TextChanged(object sender, EventArgs e)
        {
            try
            {
                SetETADate();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void txtElementValue_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                GrdElementTotal();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnKitItem_Click(object sender, EventArgs e)
        {
            try
            {
                grdkititems.DataSource = (DataTable)ViewState["KITItemsTable"];
                grdkititems.DataBind();
                mpkit.Show();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnExcelUpload_Click(object sender, EventArgs e)
        {
            try
            {
                mpexcel.Show();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnApprove_Click(object sender, EventArgs e)
        {
            if (hdnApprove.Value == "Yes")
            {
                try
                {
                    if (string.IsNullOrEmpty(txtPINo.Text.Trim()))
                    {
                        lblWarning.Text = "Please select a PI No";
                        divWarning.Visible = true;
                        txtPINo.Focus();
                        return;
                    }

                    if (txtPIStatus.Text == "C")
                    {
                        lblWarning.Text = "Selected PI is already cancelled.";
                        divWarning.Visible = true;
                        return;
                    }

                    if (txtPIStatus.Text == "A")
                    {
                        lblWarning.Text = "Selected PI is already approved.";
                        divWarning.Visible = true;
                        return;
                    }

                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16011))
                    {
                        divWarning.Visible = true;
                        lblWarning.Text = "Sorry, You have no permission to approve this order.( Advice: Required permission code : 16011)";
                        return;
                    }

                    int _App = CHNLSVC.Financial.UPDATE_PI_STATUS(Session["UserCompanyCode"].ToString(), txtPINo.Text.Trim(), "A", Session["UserID"].ToString());

                    if (_App == 1)
                    {
                        PageClear();
                        divSuccess.Visible = true;
                        lblSuccess.Text = "PI approved";
                        return;
                    }
                    else
                    {
                        divWarning.Visible = true;
                        lblWarning.Text = "PI not approved. Please re-try.";
                        return;
                    }
                    //OrderPlanHeader OrderPlanHeader = new OrderPlanHeader();

                    //OrderPlanHeader.IO_OP_NO = txtordno.Text.Trim();
                    //OrderPlanHeader.IO_STUS = "A";
                    //OrderPlanHeader.IO_MOD_BY = (string)Session["UserID"];;
                    //OrderPlanHeader.IO_MOD_DT = CHNLSVC.Security.GetServerDateTime();

                    //Int32 outputresult = CHNLSVC.Financial.UpdateOPStatus(OrderPlanHeader);

                    //if (outputresult == 1)
                    //{
                    //    divSuccess.Visible = false;
                    //    divok.Visible = true;
                    //    lblok.Text = "Successfully approved";
                    //    Clear();
                    //}
                    //else
                    //{
                    //    divalert.Visible = true;
                    //    lblalert.Text = "Error Occurred while processing";
                    //}

                }
                catch (Exception ex)
                {
                    lblWarning.Text = "Error Occurred while processing...  " + ex;
                    divWarning.Visible = true;
                }
            }
        }
        protected void lbtnuploadexcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileupexcelupload.HasFile)
                {
                    string FileName = Path.GetFileName(fileupexcelupload.PostedFile.FileName);
                    string Extension = Path.GetExtension(fileupexcelupload.PostedFile.FileName);

                    if (Extension == ".xls" || Extension == ".xlsx")
                    {
                        
                    }
                    else
                    {
                        lblWarning.Text = "Please select a valid excel (.xls) file";
                        divWarning.Visible = true;
                        return;
                    }
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                    string FilePath = Server.MapPath(FolderPath + FileName);
                    fileupexcelupload.SaveAs(FilePath);
                    Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text);
                }
                else
                {
                    lblWarning.Text = "Please select an excel file";
                    divWarning.Visible = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        private bool isPeriodSelect()
        {
            bool status = false;

            for (int i = 0; i < grdOrderPeriod.Rows.Count; i++)
            {
                GridViewRow row = (GridViewRow)grdOrderPeriod.Rows[i];
                CheckBox cb1 = (CheckBox)row.FindControl("chkOrderPeriod");
                if (cb1.Checked)
                {
                    status = true;
                    break;
                }
            }

            return status;
        }
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnSave.Value == "Yes")
                {
                    //if (ddlPaymentTerms.SelectedItem.Text == " - Select - ")
                    //{
                    //   // lblWarning.Text = "Please select Payment Terms.";
                    //   // divWarning.Visible = true;
                    //   // return;
                    //    ddlPaymentTerms.SelectedValue = string.Empty; 
                    //}
                    //if (ddlSubPaymentTerms.SelectedItem.Text == " - Select - ")
                    //{
                    //    lblWarning.Text = "Please select Sub Payment Terms.";
                    //    divWarning.Visible = true;
                    //    return;
                    //}
                    if (string.IsNullOrEmpty(txtCreaditPeriod.Text))
                    {
                        txtCreaditPeriod.Text = "0";
                    }
                    if (string.IsNullOrEmpty(txtSupplier.Text))
                    {
                        lblWarning.Text = "Please select Supplier.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (ddlTradeTerms.SelectedItem.Text == " - - Select - - ")
                    {
                        lblWarning.Text = "Please select Trade Terms.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (ddlModeofShipment.SelectedItem.Text == " - - Select - - ")
                    {
                        lblWarning.Text = "Please select Mode Of Shipment.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (ddlPortofOrigin.SelectedItem.Text == " - - Select - - ")
                    {
                        lblWarning.Text = "Please select Port Of Origin.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtManualRefNo.Text))
                    {
                        lblWarning.Text = "Please enter Manual RefNo.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (grdInvoiceDetails.Rows.Count < 1)
                    {
                        lblWarning.Text = "Please enter Items.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (grdElement.Rows.Count < 1)
                    {
                        lblWarning.Text = "Cost Element not set.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(lblTotalAmount.Text))
                    {
                        lblWarning.Text = "Invalid Total Amount.";
                        divWarning.Visible = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(txtOrderPlanNo.Text))
                    {
                        lblWarning.Text = "Please select valid order plan.";
                        divWarning.Visible = true;
                        return;
                    }
                    //if (string.IsNullOrEmpty(txtRemark.Text))
                    //{
                    //    lblWarning.Text = "Please enter remarks.";
                    //    divWarning.Visible = true;
                    //    txtRemark.Focus();
                    //    return;
                    //}
                    if (!isPeriodSelect())
                    {
                        lblWarning.Text = "Please select valid period.";
                        divWarning.Visible = true;
                        return;
                    }



                    string msg;
                    string SBU_Character = Session["UserSBU"].ToString();
                    MasterAutoNumber mastAutoNo = new MasterAutoNumber();
                    mastAutoNo.Aut_cate_cd = Session["UserCompanyCode"].ToString() + (SBU_Character);
                    mastAutoNo.Aut_cate_tp = "COM";
                    mastAutoNo.Aut_direction = null;
                    mastAutoNo.Aut_modify_dt = null;
                    mastAutoNo.Aut_moduleid = "PI";
                    mastAutoNo.Aut_start_char = "PI";
                    mastAutoNo.Aut_year = DateTime.Now.Year;

                    ImportPIHeader pih = new ImportPIHeader();
                    pih.IP_SEQ_NO = 0;
                    pih.IP_PI_NO = "";
                    pih.IP_OP_NO = txtOrderPlanNo.Text.Trim();
                    pih.IP_REF_NO = txtManualRefNo.Text.Trim();
                    pih.IP_PI_DT = Convert.ToDateTime(txtPIDate.Text.Trim());
                    pih.IP_COM = Session["UserCompanyCode"].ToString();
                    pih.IP_SBU = Session["UserSBU"].ToString();
                    pih.IP_SUPP = txtSupplier.Text.Trim();
                    pih.IP_TP = "S";
                    pih.IP_FRM_PORT = ddlPortofOrigin.SelectedItem.Value;
                    pih.IP_TO_PORT = "CMB";
                    pih.IP_RMK = txtRemark.Text;
                    pih.IP_CUR = ViewState["SupplerCurrency"].ToString();
                    pih.IP_EX_RT = Convert.ToDouble(ViewState["ExchangRate"].ToString());
                    pih.IP_TOP_CAT = "TOT";
                    pih.IP_TOP = ddlTradeTerms.SelectedItem.Value;
                    pih.IP_TOS = ddlModeofShipment.SelectedItem.Value;
                    pih.IP_ETA_DT = Convert.ToDateTime(txtETA.Text);
                    pih.IP_IS_KIT = 0;
                    pih.IP_STUS = "S";
                    pih.IP_AMD_SEQ = 0;
                    pih.IP_TOT_QTY = Convert.ToDouble(txtTotalOrderQty.Text);
                    pih.IP_TOT_AMT = Convert.ToDouble(lblTotalAmount.Text);
                    pih.IP_BANK_CD = txtBank.Text.Trim();
                    pih.IP_BANK_ACC_NO = txtAccount.Text.Trim();
                    pih.IP_ANAL_1 = Convert.ToInt32(txtCreaditPeriod.Text);
                    pih.IP_ANAL_2 = Convert.ToInt32(chkPartialShipment.Checked).ToString();
                    pih.IP_IMP_CAT = "IPM";
                    pih.IP_IMP_TP = ddlPaymentTerms.SelectedItem.Value;
                    if (!string.IsNullOrEmpty(ddlSubPaymentTerms.SelectedItem.Text))
                    {
                        pih.IP_IMP_STP = ddlSubPaymentTerms.SelectedItem.Value;
                    }
                    pih.IP_CRE_BY = Session["UserID"].ToString();
                    pih.IP_CRE_DT = CHNLSVC.Security.GetServerDateTime();
                    pih.IP_MOD_BY = Session["UserID"].ToString();
                    pih.IP_MOD_DT = CHNLSVC.Security.GetServerDateTime();
                    pih.IP_SESSION_ID = Session["SessionID"].ToString();

                    DataTable dtItem = (DataTable)ViewState["OrderItem"];
                    DataTable PIKit = (DataTable)ViewState["PIKit"];
                    DataTable PICost = (DataTable)ViewState["PICost"];
                    int outCount = CHNLSVC.Financial.SavePI(pih, dtItem, PIKit, PICost, mastAutoNo, out msg);

                    if (outCount > 0)
                    {
                        PageClear();
                        lblSuccess.Text = msg + " " + " PI No Successfully saved.";
                        divSuccess.Visible = true;
                        txtPINo.Text = msg;
                        txtPINo.Text = string.Empty;
                    }
                    else
                    {
                        lblAlert.Text = " PI Not created.";
                        divAlert.Visible = true;

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

                    if (string.IsNullOrEmpty(txtPIStatus.Text))
                    {
                        divWarning.Visible = true;
                        lblWarning.Text = "Pls. recall valid PI";
                        return;
                    }

                    if (txtPIStatus.Text == "C")
                    {
                        divWarning.Visible = true;
                        lblWarning.Text = "Selected PI is already cancelled";
                        return;
                    }

                    if (txtPIStatus.Text == "A")
                    {
                        divWarning.Visible = true;
                        lblWarning.Text = "Selected PI is already approved";
                        return;
                    }



                    string msg;
                    ImportPIHeader pihGet = (ImportPIHeader)ViewState["pih"];

                    ImportPIHeader pih = new ImportPIHeader();
                    pih.IP_SEQ_NO = pihGet.IP_SEQ_NO;
                    pih.IP_PI_NO = txtPINo.Text;
                    pih.IP_OP_NO = txtOrderPlanNo.Text.Trim();
                    pih.IP_REF_NO = txtManualRefNo.Text.Trim();
                    pih.IP_PI_DT = pihGet.IP_PI_DT;
                    pih.IP_COM = Session["UserCompanyCode"].ToString();
                    pih.IP_SBU = Session["UserSBU"].ToString();
                    pih.IP_SUPP = txtSupplier.Text.Trim();
                    pih.IP_TP = "S";
                    pih.IP_FRM_PORT = ddlPortofOrigin.SelectedItem.Value;
                    pih.IP_TO_PORT = "CMB";
                    pih.IP_RMK = txtRemark.Text;
                    pih.IP_CUR = ViewState["SupplerCurrency"].ToString();
                    pih.IP_EX_RT = Convert.ToDouble(ViewState["ExchangRate"].ToString());
                    pih.IP_TOP_CAT = "TOT";
                    pih.IP_TOP = ddlTradeTerms.SelectedItem.Value;
                    pih.IP_TOS = ddlModeofShipment.SelectedItem.Value;
                    pih.IP_ETA_DT = Convert.ToDateTime(txtETA.Text);
                    pih.IP_IS_KIT = 0;
                    pih.IP_STUS = "S";
                    pih.IP_AMD_SEQ = 0;
                    pih.IP_TOT_QTY = Convert.ToDouble(txtTotalOrderQty.Text);
                    pih.IP_TOT_AMT = Convert.ToDouble(txtTotalOrderValue.Text);
                    pih.IP_BANK_CD = txtBank.Text;
                    pih.IP_BANK_ACC_NO = txtAccount.Text;
                    pih.IP_ANAL_1 = Convert.ToInt32(txtCreaditPeriod.Text);
                    pih.IP_ANAL_2 = Convert.ToInt32(chkPartialShipment.Checked).ToString();
                    pih.IP_IMP_CAT = "IPM";
                    pih.IP_IMP_TP = ddlPaymentTerms.SelectedItem.Value;
                    pih.IP_IMP_STP = ddlPaymentTerms.SelectedItem.Value;//Add SubTerms
                    pih.IP_CRE_BY = Session["UserID"].ToString();
                    pih.IP_CRE_DT = CHNLSVC.Security.GetServerDateTime();
                    pih.IP_MOD_BY = Session["UserID"].ToString();
                    pih.IP_MOD_DT = CHNLSVC.Security.GetServerDateTime();
                    pih.IP_SESSION_ID = Session["SessionID"].ToString();

                    DataTable dtItem = (DataTable)ViewState["OrderItem"];
                    DataTable PICost = (DataTable)ViewState["PICost"];
                    int outCount = CHNLSVC.Financial.UpdatePI(pih, dtItem, PICost, out msg);

                    if (outCount > 0)
                    {
                        lblSuccess.Text = msg + " " + " PI No Successfully updated.";
                        divSuccess.Visible = true;
                        txtPINo.Text = "";
                        PageClear();
                        
                    }
                    else
                    {
                        lblSuccess.Text = msg + " " + " PI No not updated.Pls. re-try";
                        divSuccess.Visible = true;
                        //txtPINo.Text = msg;
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
            try
            {
                if (hdnCancel.Value == "Yes")
                {
                    if (string.IsNullOrEmpty(txtPIStatus.Text))
                    {
                        divWarning.Visible = true;
                        lblWarning.Text = "Sorry, cannot find valid PI status.Pls. re-call the PI and re-try.";
                        return;
                    }

                    if (txtPIStatus.Text == "C")
                    {
                        divWarning.Visible = true;
                        lblWarning.Text = "Sorry, selected PI is already cancelled.";
                        return;
                    }

                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16010))
                    {
                        divWarning.Visible = true;
                        lblWarning.Text = "Sorry, You have no permission to approve this order.( Advice: Required permission code : 16010)";
                        return;
                    }

                    int _cancel = CHNLSVC.Financial.UPDATE_PI_STATUS(Session["UserCompanyCode"].ToString(), txtPINo.Text.Trim(), "C", Session["UserID"].ToString());

                    if (_cancel == 1)
                    {
                        lblSuccess.Text = txtPINo.Text.Trim() + " " + " PI No Successfully cancelled.";
                        divSuccess.Visible = true;
                        PageClear();
                    }
                    else
                    {
                        lblWarning.Text = "PI not cancel. Pls. re-try.";
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
                    //CHNLSVC.General.Common_send_Email();
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
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
        protected void ImageSearch_Click(object sender, EventArgs e)
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
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                //PINo
                if (lblvalue.Text == "400")
                {
                    txtPINo.Text = grdResult.SelectedRow.Cells[1].Text;
                    PopulatePiData();
                }
                //Supplier
                if (lblvalue.Text == "401")
                {
                    txtSupplier.Text = grdResult.SelectedRow.Cells[1].Text;
                    //lblSupplier.Text = grdResult.SelectedRow.Cells[2].Text;
                    DdlPortofOriginDataPopulate();

                    List<MasterBusinessEntity> supplierlistedit = CHNLSVC.Sales.GetCustomerDetailList(Session["UserCompanyCode"].ToString(), txtSupplier.Text, string.Empty, string.Empty, "S");
                    if (supplierlistedit != null || supplierlistedit.Count > 1)
                    {
                        foreach (var _nicCust in supplierlistedit)
                        {
                            ViewState["SupplerCurrency"] = _nicCust.Mbe_cur_cd;
                            lblTotalOrderValue.Text = _nicCust.Mbe_cur_cd;
                            lblSCurrancy.Text = _nicCust.Mbe_cur_cd;

                        }
                    }
                    ViewState["ExchangRate"] = 0;
                    MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                    MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(Session["UserCompanyCode"].ToString(), ViewState["SupplerCurrency"].ToString(), DateTime.Now, _pc.Mpc_def_exrate, string.Empty);
                    if (_exc1 != null)
                    {
                        ViewState["ExchangRate"] = _exc1.Mer_bnkbuy_rt;
                        lblERate.Text = _exc1.Mer_bnkbuy_rt.ToString();
                    }

                }
                //OrderPlanNo
                if (lblvalue.Text == "403")
                {
                    txtOrderPlanNo.Text = grdResult.SelectedRow.Cells[1].Text;
                    GetOrderPeriod();
                    ViewState["Update"] = 1;
                }
                //Bank
                if (lblvalue.Text == "21")
                {
                    txtBank.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtAccount.Text = string.Empty;
                }
                //Account
                if (lblvalue.Text == "22")
                {
                    txtAccount.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                //Model
                if (lblvalue.Text == "406")
                {
                    txtModel.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtItem.Text = string.Empty;
                }
                //Item
                if (lblvalue.Text == "407")
                {
                    txtItem.Text = grdResult.SelectedRow.Cells[2].Text;
                    DataTable dt = CHNLSVC.Financial.GetItemDetails(txtItem.Text);

                    if (dt.Rows[0]["MI_ITM_TP"].ToString() == "K")
                    {
                        ViewState["KitItem"] = 1;
                    }
                    txtModel.Text = dt.Rows[0]["MI_MODEL"].ToString();
                    lblDescription.Text = dt.Rows[0]["MI_SHORTDESC"].ToString();
                    lblBrand.Text = dt.Rows[0]["MI_BRAND"].ToString();
                    lblUOM.Text = dt.Rows[0]["MI_ITM_UOM"].ToString();
                    txtColour.Text = dt.Rows[0]["MI_COLOR_EXT"].ToString();
                    ddlItemType.SelectedValue = dt.Rows[0]["MI_ITM_TP"].ToString();

                    txtModel.ReadOnly = true;
                    lbtnModel.Enabled = false;
                }
                ViewState["SEARCH"] = null;
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




        private void DdlPaymentTermsDataPopulate()
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PaymentTerms);
            DataTable result = CHNLSVC.CommonSearch.SearchTraderTerms(SearchParams, null, null);
            ddlPaymentTerms.DataSource = result;
            ddlPaymentTerms.DataTextField = "CODE";
            ddlPaymentTerms.DataValueField = "CODE";
            ddlPaymentTerms.DataBind();
            ddlPaymentTerms.Items.Insert(0, new ListItem(" - Select - ", "0"));
        }
        private void DdlSubPaymentTermsDataPopulate(string pcad, string pcd)
        {
            DataTable result = CHNLSVC.CommonSearch.GetPaymentSubTerm(pcad, pcd);
            ddlSubPaymentTerms.DataSource = result;
            ddlSubPaymentTerms.DataTextField = "mcas_tp";
            ddlSubPaymentTerms.DataValueField = "mcas_tp";
            ddlSubPaymentTerms.DataBind();
            if (result.Rows.Count == 0)
            {
                ddlSubPaymentTerms.Items.Insert(0, new ListItem(" - Select - ", "0"));
            }
        }
        private void DdlTradeTermsDataPopulate()
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TradeTerms);
            DataTable result = CHNLSVC.CommonSearch.SearchTraderTerms(SearchParams, null, null);
            ddlTradeTerms.DataSource = result;
            ddlTradeTerms.DataTextField = "CODE";
            ddlTradeTerms.DataValueField = "CODE";
            ddlTradeTerms.DataBind();
            ddlTradeTerms.Items.Insert(0, new ListItem(" - - Select - - ", "0"));
            ddlTradeTerms.SelectedValue = "0";
        }
        private void DdlPortofOriginDataPopulate()
        {
            if (string.IsNullOrEmpty(txtSupplier.Text))
            {
                lblWarning.Text = "Invalid Supplier.";
                divWarning.Visible = true;
                return;
            }
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TradeTerms);
            DataTable result = CHNLSVC.Financial.GetSupplierPorts(Session["UserCompanyCode"].ToString(), txtSupplier.Text);
            ddlPortofOrigin.DataSource = result;
            ddlPortofOrigin.DataTextField = "mspr_frm_port";
            ddlPortofOrigin.DataValueField = "mspr_frm_port";
            ddlPortofOrigin.DataBind();
            ddlPortofOrigin.Items.Insert(0, new ListItem(" - - Select - - ", "0"));
            txtETD.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
            txtETA.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
        }
        private void GrdElementDataPopulate(string com, string cat, string tp)
        {
            if (string.IsNullOrEmpty(tp))
            {
                lblWarning.Text = "Invalid TradeTerms.";
                divWarning.Visible = true;
                return;
            }
            DataTable dt = CHNLSVC.Financial.GetCost(com, cat, tp);
            if (dt.Rows.Count <= 0)
            {
                //ddlTradeTerms.SelectedIndex = -1;
                //lblWarning.Text = "Please Select Valid Trade Terms.";
                //divWarning.Visible = true;
                grdElement.DataSource = new int[] { };
                grdElement.DataBind();
                return;
            }

            dt.Columns.Add("Value", typeof(decimal));
            dt.Columns.Add("Enable", typeof(bool));
            int count = dt.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                if (dt.Rows[i]["CODE"].ToString() == "COST")
                {
                    dt.Rows[i]["Enable"] = 1;
                }
                else
                    dt.Rows[i]["Enable"] = 0;
            }

            ViewState["PICost"] = dt;

            grdElement.DataSource = dt;
            grdElement.DataBind();

            if (ViewState["OrderItem"] != null)
            {
                DataTable dtInvoiceDetails = (DataTable)ViewState["OrderItem"];
                GridInvoiceDetailsTotal(dtInvoiceDetails);
            }
        }
        private void GridInvoiceDetailsTotal(DataTable dt)
        {
            if (grdInvoiceDetails.Rows.Count <= 0)
            {
                txtTotalOrderQty.Text = "0.00";
                txtTotalOrderValue.Text = "0.00";
                foreach (GridViewRow gr in grdElement.Rows)
                {
                    if (gr.Cells[1].Text == "ITEM COST")
                    {
                        ((TextBox)gr.Cells[0].FindControl("txtElementValue")).Text = txtTotalOrderValue.Text.ToString();
                    }
                }
                GrdElementTotal();
            }
            else
            {
                double totalQty;
                double totalAmount;
                if (!double.TryParse(dt.Compute("sum(IOI_BAL_QTY)", "").ToString(), out totalQty))
                {
                    lblWarning.Text = "Invalid Total Order Qty.";
                    divWarning.Visible = true;
                    return;
                }
                else
                    txtTotalOrderQty.Text = totalQty.ToString("N2");
                if (!double.TryParse(dt.Compute("sum(ITEMVLUE)", "").ToString(), out totalAmount))
                {
                    lblWarning.Text = "Invalid Total Order Value.";
                    divWarning.Visible = true;
                    return;
                }
                else
                {
                    txtTotalOrderValue.Text = totalAmount.ToString("N5");
                    foreach (GridViewRow gr in grdElement.Rows)
                    {
                        if (gr.Cells[1].Text == "ITEM COST")
                        {
                            ((TextBox)gr.Cells[0].FindControl("txtElementValue")).Text = totalAmount.ToString("N5");
                        }
                    }

                    GrdElementTotal();
                }
            }
        }
        private void Import_To_Grid(string FilePath, string Extension, string isHDR)
        {
            try
            {
                string conStr = "";
                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07
                        conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }
                conStr = String.Format(conStr, FilePath, isHDR);
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();

                dt1.Columns.Add("IOI_ITM_CD", typeof(string));
                dt1.Columns.Add("IOI_DESC", typeof(string));
                dt1.Columns.Add("IOI_MODEL", typeof(string));
                dt1.Columns.Add("IOI_COLOR", typeof(string));
                dt1.Columns.Add("IOI_UOM", typeof(string));
                dt1.Columns.Add("IOI_ITM_TP", typeof(string));
                dt1.Columns.Add("IOI_BAL_QTY", typeof(decimal));
                dt1.Columns.Add("IOI_UNIT_RT", typeof(decimal));
                dt1.Columns.Add("ITEMVLUE", typeof(decimal));
                dt1.Columns.Add("IOI_YY", typeof(int));
                dt1.Columns.Add("IOI_MM", typeof(int));
                dt1.Columns.Add("LINE", typeof(int));
                dt1.Columns.Add("KIT", typeof(int));
                dt1.Columns.Add("KITCODE", typeof(string));
                dt1.Columns.Add("OP_LINE", typeof(int));
                dt1.Columns.Add("DELETE", typeof(int));

                cmdExcel.Connection = connExcel;

                //Get the name of First Sheet
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                //Read Data from First Sheet
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt1.NewRow();
                    dr["IOI_ITM_CD"] = dt.Rows[i]["ITEMCODE"].ToString();
                    DataTable dtItm = CHNLSVC.Financial.GetItemDetails(dt.Rows[i]["ITEMCODE"].ToString());

                    if (dtItm.Rows.Count <= 0)
                    {
                        txtItem.Text = string.Empty;
                        lblWarning.Text = "Invalid Item.";
                        divWarning.Visible = true;
                        return;
                    }
                    //if (dt.Rows[0]["MI_ITM_TP"].ToString() == "K")
                    //{
                    //    ViewState["KitItem"] = 1;
                    //}
                    //txtModel.Text = 
                    //lblDescription.Text = 
                    //lblBrand.Text = dtItm.Rows[0]["MI_BRAND"].ToString();
                    //lblUOM.Text = 
                    //txtColour.Text = 
                    //ddlItemType.SelectedValue = 

                    dr["IOI_DESC"] = dtItm.Rows[0]["MI_SHORTDESC"].ToString();
                    dr["IOI_MODEL"] = dtItm.Rows[0]["MI_MODEL"].ToString();
                    dr["IOI_COLOR"] = dtItm.Rows[0]["MI_COLOR_INT"].ToString();
                    dr["IOI_UOM"] = dtItm.Rows[0]["MI_ITM_UOM"].ToString();
                    dr["IOI_ITM_TP"] = dtItm.Rows[0]["MI_ITM_TP"].ToString();
                    dr["IOI_BAL_QTY"] = dt.Rows[i]["QTY"];
                    dr["IOI_UNIT_RT"] = dt.Rows[i]["UNITRATE"];
                    dr["ITEMVLUE"] = dt.Rows[i]["ITEMVLUE"]; //
                    dr["IOI_YY"] = 0;
                    dr["IOI_MM"] = 0;
                    dr["LINE"] = 1;
                    dr["KIT"] = 0;
                    dr["KITCODE"] = "";
                    dr["OP_LINE"] = 1;//For check updated or not
                    dr["DELETE"] = 1;//For New Item
                    dt1.Rows.Add(dr);
                }
                DataView dv = dt1.DefaultView;
                dv.Sort = "IOI_ITM_CD";
                DataTable sortedDT = dv.ToTable();

                //Bind Data to GridView
                grdInvoiceDetails.DataSource = sortedDT;
                grdInvoiceDetails.DataBind();

                //CalculateTotQty();
                //CalculateTotValue();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        private void GrdElementTotal()
        {
            double total = 0;
            double amount = 0;
            if (grdElement.Rows.Count <= 0 && double.TryParse(txtTotalOrderValue.Text, out total))
            {
                lblTotalAmount.Text = total.ToString("N2");
            }
            else
            {
                DataTable dtPICost = (DataTable)ViewState["PICost"];
                foreach (GridViewRow gr in grdElement.Rows)
                {
                    if (!string.IsNullOrEmpty(((TextBox)gr.Cells[2].FindControl("txtElementValue")).Text))
                    {
                        if (double.TryParse(((TextBox)gr.Cells[2].FindControl("txtElementValue")).Text, out amount))
                        {
                            total = total + amount;
                            ((TextBox)gr.Cells[2].FindControl("txtElementValue")).Text = amount.ToString("N5");

                            dtPICost.Rows[gr.RowIndex]["Value"] = amount.ToString("N5");
                            lblElemTotal.Text = total.ToString("N5");
                        }
                        else
                        {
                            ((TextBox)gr.Cells[2].FindControl("txtElementValue")).Text = "0.00";
                            dtPICost.Rows[gr.RowIndex]["Value"] = "0.00";
                            lblWarning.Text = "Please enter valid amount";
                            divWarning.Visible = true;
                            return;
                        }
                    }
                    else
                    {
                        ((TextBox)gr.Cells[2].FindControl("txtElementValue")).Text = "0.00";
                        dtPICost.Rows[gr.RowIndex]["Value"] = "0.00";
                    }
                }
                lblTotalAmount.Text = total.ToString("N5");
            }
        }
        private void PopulatePiData()
        {
            ImportPIHeader pih = CHNLSVC.Financial.GET_IMP_PI(txtPINo.Text);

            txtPIDate.Text = pih.IP_PI_DT.ToString("dd/MMM/yyyy");
            ddlPaymentTerms.SelectedValue = pih.IP_IMP_TP;
            DdlSubPaymentTermsDataPopulate("IPM", ddlPaymentTerms.Text);
            ddlSubPaymentTerms.SelectedValue = pih.IP_IMP_STP;
            txtCreaditPeriod.Text = pih.IP_ANAL_1.ToString();
            txtRemark.Text = pih.IP_RMK;

            txtOrderPlanNo.Text = pih.IP_OP_NO;
            grdOrderPeriod.DataSource = CHNLSVC.Financial.GetOrderPeriodALL(txtOrderPlanNo.Text);
            grdOrderPeriod.DataBind();

            DataTable dtOrder = CHNLSVC.CommonSearch.SP_Search_Order(txtOrderPlanNo.Text);
            if (dtOrder.Rows.Count > 0)
            {
                //lblSupplier.Text = dtOrder.Rows[0]["IO_SUPP"].ToString();
                txtOrderRefNo.Text = dtOrder.Rows[0]["IO_REF_NO"].ToString();
            }
            txtSupplier.Text = pih.IP_SUPP;
            DdlPortofOriginDataPopulate();
            ddlTradeTerms.SelectedValue = pih.IP_TOP;
            GrdElementDataPopulate(Session["UserCompanyCode"].ToString(), "TOT", ddlTradeTerms.SelectedItem.Value);
            ddlModeofShipment.SelectedValue = pih.IP_TOS;
            ddlPortofOrigin.SelectedValue = pih.IP_FRM_PORT;

            txtManualRefNo.Text = pih.IP_REF_NO;
            chkPartialShipment.Checked = Convert.ToBoolean(Convert.ToInt32(pih.IP_ANAL_2));
            txtETA.Text = pih.IP_ETA_DT.ToString("dd/MMM/yyyy");
            DataTable dtETA = CHNLSVC.Financial.GetSupplierETA(Session["UserCompanyCode"].ToString(), txtSupplier.Text, ddlPortofOrigin.SelectedItem.Value);
            int days = Convert.ToInt32(dtETA.Rows[0]["mspr_lead_time"]);
            txtETD.Text = Convert.ToDateTime(pih.IP_ETA_DT).AddDays(-days).ToString("dd/MMM/yyyy");

            ViewState["SupplerCurrency"] = pih.IP_CUR;
            ViewState["ExchangRate"] = pih.IP_EX_RT;
            lblSCurrancy.Text = pih.IP_CUR.ToString();
            lblERate.Text = pih.IP_EX_RT.ToString();

            txtTotalOrderQty.Text = pih.IP_TOT_QTY.ToString("N2");
            txtTotalOrderValue.Text = pih.IP_TOT_AMT.ToString("N5");
            txtPIStatus.Text = pih.IP_STUS;
            if (pih.IP_STUS == "A")
            {
                txtStatus.Text ="APPROVED";
            }
            else if (pih.IP_STUS == "C")
            {
                txtStatus.Text = "CANCELLED";
            }
            else if (pih.IP_STUS =="F")
            {
                txtStatus.Text = "FINISHED";
            }
            else
            {
                txtStatus.Text = "PENDING";
            }
            txtBank.Text = pih.IP_BANK_CD;
            txtAccount.Text = pih.IP_BANK_ACC_NO;

            List<ImportPIDetails> pid = CHNLSVC.Financial.GET_IMP_PIITEM(txtPINo.Text);

            DataTable dt = new DataTable();
            dt.Columns.Add("IOI_ITM_CD", typeof(string));
            dt.Columns.Add("IOI_DESC", typeof(string));
            dt.Columns.Add("IOI_MODEL", typeof(string));
            dt.Columns.Add("IOI_COLOR", typeof(string));
            dt.Columns.Add("IOI_UOM", typeof(string));
            dt.Columns.Add("IOI_ITM_TP", typeof(string));
            dt.Columns.Add("IOI_BAL_QTY", typeof(decimal));
            dt.Columns.Add("IOI_UNIT_RT", typeof(decimal));
            dt.Columns.Add("ITEMVLUE", typeof(decimal));
            dt.Columns.Add("IOI_YY", typeof(int));
            dt.Columns.Add("IOI_MM", typeof(int));
            dt.Columns.Add("LINE", typeof(int));
            dt.Columns.Add("KIT", typeof(int));
            dt.Columns.Add("KITCODE", typeof(string));
            dt.Columns.Add("OP_LINE", typeof(int));
            dt.Columns.Add("DELETE", typeof(int));

            foreach (ImportPIDetails id in pid)
            {
                DataRow dr = dt.NewRow();
                dr["IOI_ITM_CD"] = id.IPI_ITM_CD;
                dr["IOI_DESC"] = id.IPI_DESC;
                dr["IOI_MODEL"] = id.IPI_MODEL;
                dr["IOI_COLOR"] = id.IPI_COLOR;
                dr["IOI_UOM"] = id.IPI_UOM;
                dr["IOI_ITM_TP"] = id.IPI_ITM_TP;
                dr["IOI_BAL_QTY"] = id.IPI_BAL_QTY;
                dr["IOI_UNIT_RT"] = id.IPI_UNIT_RT;
                dr["ITEMVLUE"] = Convert.ToDouble(id.IPI_QTY) * Convert.ToDouble(id.IPI_UNIT_RT);
                dr["IOI_YY"] = 0;
                dr["IOI_MM"] = 0;
                dr["LINE"] = id.IPI_LINE;
                dr["KIT"] = ViewState["KitItem"];
                dr["KITCODE"] = "";
                dr["OP_LINE"] = 0;//For check updated or not
                dr["DELETE"] = 0;//For New Item
                dt.Rows.Add(dr);
            }


            DataTable dtCost = CHNLSVC.Financial.GET_IMP_PI_COST(txtPINo.Text);

            DataTable dtC = new DataTable();
            dtC.Columns.Add("CODE", typeof(string));
            dtC.Columns.Add("DESCRIPTION", typeof(string));
            dtC.Columns.Add("ELEMENT CATERGORY", typeof(string));
            dtC.Columns.Add("ELEMENT TYPE", typeof(string));
            dtC.Columns.Add("Value", typeof(decimal));
            dtC.Columns.Add("Enable", typeof(bool));

            for (int i = 0; i < dtCost.Rows.Count; i++)
            {
                DataRow drC = dtC.NewRow();
                drC["CODE"] = dtCost.Rows[i]["IPC_ELE_CD"].ToString();
                drC["DESCRIPTION"] = dtCost.Rows[i]["IPC_ELE_CD"].ToString();
                drC["ELEMENT CATERGORY"] = dtCost.Rows[i]["IPC_ELE_CAT"].ToString();
                drC["ELEMENT TYPE"] = dtCost.Rows[i]["IPC_ELE_TP"].ToString();
                drC["Value"] = Convert.ToDecimal(dtCost.Rows[i]["IPC_AMT"].ToString());
                if (dtCost.Rows[i]["IPC_ELE_CD"].ToString() == "COST")
                {
                    drC["Enable"] = 1;
                }
                else
                    drC["Enable"] = 0;
                dtC.Rows.Add(drC);
            }

            grdElement.DataSource = dtC;
            grdElement.DataBind();

            DataView dv = dt.DefaultView;
            dv.Sort = "IOI_ITM_CD";
            DataTable sortedDT = dv.ToTable();

            grdInvoiceDetails.DataSource = sortedDT;
            grdInvoiceDetails.DataBind();
            ViewState["Details"] = sortedDT;
            ViewState["pih"] = pih;
            ViewState["OrderItem"] = sortedDT;

            GridInvoiceDetailsTotal(sortedDT);
            lbtnCancel.Enabled = true;
            lbtnApprove.Enabled = true;
            lbtnUpdate.Enabled = true;
            lbtnSave.Visible = false;
            
        }
        private void SetETADate()
        {
            DateTime eta;
            if (string.IsNullOrEmpty(txtSupplier.Text))
            {
                lblWarning.Text = "Please select supplier.";
                divWarning.Visible = true;
                txtETA.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                return;
            }
            if (ddlPortofOrigin.SelectedItem.Text == " - - Select - - ")
            {
                lblWarning.Text = "Please select Port of Origin.";
                divWarning.Visible = true;
                txtETA.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                return;
            }
            if (DateTime.TryParse(txtETD.Text, out eta))
            {
                DataTable dt = CHNLSVC.Financial.GetSupplierETA(Session["UserCompanyCode"].ToString(), txtSupplier.Text, ddlPortofOrigin.SelectedItem.Value);
                int days = Convert.ToInt32(dt.Rows[0]["mspr_lead_time"]);
                txtETA.Text = Convert.ToDateTime(eta).AddDays(days).ToString("dd/MMM/yyyy");
            }
            else
            {
                lblWarning.Text = "Please select valid ETD.";
                divWarning.Visible = true;
                txtETA.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
                return;
            }
        }
        private void GetOrderPeriod()
        {
            if (string.IsNullOrEmpty(txtOrderPlanNo.Text))
            {
                lblWarning.Text = "Please select Order Plan No.";
                divWarning.Visible = true;

                txtSupplier.Text = string.Empty;
                txtOrderPlanNo.Text = string.Empty;
                ddlTradeTerms.SelectedIndex = -1;
                ddlModeofShipment.SelectedIndex = -1;
                ddlPortofOrigin.SelectedIndex = -1;
                txtETA.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");

                ViewState["OrderItem"] = null;
                grdInvoiceDetails.DataSource = new int[] { };
                grdInvoiceDetails.DataBind();
                return;
            }
            else
            {
                DataTable dt = CHNLSVC.CommonSearch.SP_Search_Order(txtOrderPlanNo.Text);
                if (dt.Rows.Count <= 0)
                {
                    lblWarning.Text = "Invalid Order Plan Details.";
                    divWarning.Visible = true;
                    txtOrderPlanNo.Text = "";
                    return;
                }

                txtSupplier.Text = dt.Rows[0]["IO_SUPP"].ToString();
                //lblSupplier.Text = dt.Rows[0]["IO_SUPP"].ToString();
                lbtnSupplier.Enabled = false;
                DdlPortofOriginDataPopulate();
                txtOrderRefNo.Text = dt.Rows[0]["IO_REF_NO"].ToString();
                ddlTradeTerms.SelectedValue = dt.Rows[0]["IO_TOP"].ToString();
                GrdElementDataPopulate(Session["UserCompanyCode"].ToString(), "TOT", ddlTradeTerms.SelectedItem.Value);
                ddlModeofShipment.SelectedValue = dt.Rows[0]["IO_TOS"].ToString();
                ddlPortofOrigin.SelectedValue = dt.Rows[0]["IO_FRM_PORT"].ToString();
                txtETA.Text = Convert.ToDateTime(dt.Rows[0]["IO_ETA_DT"]).ToString("dd/MMM/yyyy");

                ViewState["SupplerCurrency"] = dt.Rows[0]["IO_CUR"].ToString();
                ViewState["ExchangRate"] = dt.Rows[0]["IO_EX_RT"].ToString();
                lblTotalOrderValue.Text = ViewState["SupplerCurrency"].ToString();
                //lblCurrency.Text = ViewState["SupplerCurrency"].ToString();

                lblSCurrancy.Text = dt.Rows[0]["IO_CUR"].ToString();
                lblERate.Text = dt.Rows[0]["IO_EX_RT"].ToString();

                grdOrderPeriod.DataSource = CHNLSVC.Financial.GetOrderPeriod(txtOrderPlanNo.Text);
                grdOrderPeriod.DataBind();

                ViewState["OrderItem"] = null;
                grdInvoiceDetails.DataSource = new int[] { };
                grdInvoiceDetails.DataBind();

                txtModel.Text = string.Empty;
                txtModel.ReadOnly = false;
                lbtnModel.Enabled = true;
                txtItem.Text = string.Empty;
                ddlItemType.SelectedIndex = -1;
                txtColour.Text = string.Empty;
                txtQty.Text = string.Empty;
                txtUnitRate.Text = string.Empty;

                lblDescription.Text = string.Empty;
                lblBrand.Text = string.Empty;
                lblUOM.Text = string.Empty;

                txtTotalOrderQty.Text = string.Empty;
                txtTotalOrderValue.Text = string.Empty;

                lblTotalAmount.Text = string.Empty;

                grdElement.DataSource = new int[] { };
                grdElement.DataBind();

                GrdElementDataPopulate(Session["UserCompanyCode"].ToString(), "TOT", ddlTradeTerms.SelectedItem.Value);
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
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "S" + seperator + "I" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PINO:
                    {
                        paramsText.Append(BaseCls.GlbPINo + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TradeTerms:
                    {
                        paramsText.Append("TOT" + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PaymentTerms:
                    {
                        paramsText.Append("IPM" + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OrderPlanNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append("BANK" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtBank.Text + seperator + ddlPaymentTerms.Text);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ImportModel:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ImportItem:
                    {
                        paramsText.Append(txtModel.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(txtSupplier.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
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
            //Order Plan No
            else if (lblvalue.Text == "403")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable result = CHNLSVC.CommonSearch.SearchOrderPlanNo(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "403";
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
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
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
            if (string.IsNullOrEmpty(Session["UserSBU"].ToString()))
            {
                lblSbuMsg1.Text = "SBU (Strategic Business) is not allocate for your login ID.";
                lblSbuMsg2.Text = "There is not setup default SBU (Sttre Buds Unit) for your login ID.";
                SbuPopup.Show();
            }
            lblSCurrancy.Text = string.Empty;
            lblERate.Text = string.Empty;
            txtPINo.Text = string.Empty;
            txtStatus.Text = string.Empty;
            txtPIDate.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
            txtOrderPlanNo.Text = string.Empty;
            ddlPaymentTerms.SelectedIndex = -1;
            ddlSubPaymentTerms.SelectedIndex = -1;
            txtCreaditPeriod.Text = string.Empty;
            txtRemark.Text = string.Empty;
            lblTotalOrderValue.Text = string.Empty;
            txtPIStatus.Text = string.Empty;
            txtOrderPlanNo.Text = string.Empty;
            grdOrderPeriod.DataSource = new int[] { };
            grdOrderPeriod.DataBind();

            txtOrderRefNo.Text = string.Empty;
            txtSupplier.Text = string.Empty;
            lbtnSupplier.Enabled = true;
            ddlTradeTerms.SelectedIndex = -1;
            ddlModeofShipment.SelectedIndex = -1;
            ddlPortofOrigin.SelectedIndex = -1;
            //   ddlPortofOrigin.Items.Insert(0, new ListItem(" - - Select - - ", "0"));

            txtManualRefNo.Text = string.Empty;
            //lblSupplier.Text = string.Empty;
            chkPartialShipment.Checked = false;
            txtETA.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
            txtETD.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");

            txtModel.Text = string.Empty;
            txtModel.ReadOnly = false;
            lbtnModel.Enabled = true;
            txtItem.Text = string.Empty;
            ddlItemType.SelectedIndex = -1;
            txtColour.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtUnitRate.Text = string.Empty;

            lblDescription.Text = string.Empty;
            lblBrand.Text = string.Empty;
            lblUOM.Text = string.Empty;

            grdInvoiceDetails.DataSource = new int[] { };
            grdInvoiceDetails.DataBind();

            txtTotalOrderQty.Text = string.Empty;
            txtTotalOrderValue.Text = string.Empty;

            lblTotalAmount.Text = string.Empty;

            txtBank.Text = string.Empty;
            txtAccount.Text = string.Empty;
            lbtnBank.Enabled = true;
            lbtnBank.Enabled = true;

            grdElement.DataSource = new int[] { };
            grdElement.DataBind();

            lbtnUpdate.Enabled = false;
            lbtnCancel.Enabled = false;
            lbtnApprove.Enabled = false;
            lbtnSave.Visible = true;

            ViewState["OrderItem"] = null;
            ViewState["SupplerCurrency"] = null;
            ViewState["ExchangRate"] = null;
            ViewState["KitItem"] = 0;
            ViewState["PIKit"] = null;
            ViewState["PICost"] = null;

            ddlPaymentTerms.Focus();

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = string.Empty;
        }


        protected void ExportToExcel()
        {
            DataTable dt = ViewState["Details"] as DataTable;
            if (dt.Rows.Count == 0)
            {
                lblWarning.Text = "Please select PI ";
                divWarning.Visible = true;
                return;
            }
            DataTable ExcelDatatabl = new DataTable();
            ExcelDatatabl.Columns.Add("Item Code", typeof(string));
            ExcelDatatabl.Columns.Add("Description", typeof(string));
            ExcelDatatabl.Columns.Add("Model", typeof(string));
            ExcelDatatabl.Columns.Add("Colour", typeof(string));
            ExcelDatatabl.Columns.Add("UOM", typeof(string));
            ExcelDatatabl.Columns.Add("Type", typeof(string));
            ExcelDatatabl.Columns.Add("Order Qty", typeof(decimal));
            ExcelDatatabl.Columns.Add("Unit Price", typeof(decimal));
            ExcelDatatabl.Columns.Add("Value", typeof(decimal));
            foreach (DataRow row in dt.Rows)
            {
                DataRow dr = ExcelDatatabl.NewRow();
                dr["Item Code"] = row["IOI_ITM_CD"].ToString();
                dr["Description"] = row["IOI_DESC"].ToString();
                dr["Model"] = row["IOI_MODEL"].ToString();
                dr["Colour"] = row["IOI_COLOR"].ToString();
                dr["UOM"] = row["IOI_UOM"].ToString();
                dr["Type"] = row["IOI_ITM_TP"].ToString();
                dr["Order Qty"] = row["IOI_BAL_QTY"].ToString();
                dr["Unit Price"] = row["IOI_UNIT_RT"].ToString();
                dr["Value"] = row["ITEMVLUE"].ToString();
                ExcelDatatabl.Rows.Add(dr);
            }

            DataGrid GridView1 = new DataGrid();
            GridView1.DataSource = ExcelDatatabl;
            GridView1.DataBind();

            Response.Clear();
            Response.Buffer = true;

            Response.AddHeader("content-disposition",
             "attachment;filename=Performa Invoice.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            GridView1.AllowPaging = false;
            GridView1.DataBind();

            GridView1.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
     
        protected void lbtnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel();
           // string sFileName = "BooksList-" + System.DateTime.Now.Date + ".xls";
           // sFileName = sFileName.Replace("/", "");
           // DataTable dt = ViewState["Details"] as DataTable;
           // DataGrid dg = new DataGrid();
           // dg.DataSource = dt;
           // dg.DataBind();

           //// ExportToWord();
           // Response.ClearContent();
           // Response.Buffer = true;
           // Response.AddHeader("content-disposition", "attachment; filename=" + sFileName);
           // Response.ContentType = "application/vnd.ms-excel";
           // EnableViewState = false;

           // System.IO.StringWriter objSW = new System.IO.StringWriter();
           // System.Web.UI.HtmlTextWriter objHTW = new System.Web.UI.HtmlTextWriter(objSW);

           // dg.HeaderStyle.Font.Bold = true;     // SET EXCEL HEADERS AS BOLD.
           // dg.RenderControl(objHTW);

           // // STYLE THE SHEET AND WRITE DATA TO IT.
           // Response.Write("<style> TABLE { border:dotted 1px #999; } " +
           //     "TD { border:dotted 1px #D5D5D5; text-align:center } </style>");
           // Response.Write(objSW.ToString());

           // // ADD A ROW AT THE END OF THE SHEET SHOWING A RUNNING TOTAL OF PRICE.
           // //Response.Write("<table><tr><td><b>Total: </b></td><td></td><td><b>" +
           // //    dTotalPrice.ToString("N2") + "</b></td></tr></table>");
           
           // Response.End();
           // dg = null;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           DataTable dts = (DataTable)ViewState["OrderItem"];
           foreach (DataRow _row in dts.Rows)
           {
               if (_row["IOI_ITM_CD"].ToString() == txtItem.Text)
               {
                   _row["IOI_BAL_QTY"] = Convert.ToDecimal(txtQty.Text)+ Convert.ToDecimal(_row["IOI_BAL_QTY"] .ToString());
               }
           }
           grdInvoiceDetails.DataSource = dts;
           grdInvoiceDetails.DataBind();

           ViewState["OrderItem"] = dts;
           dupicatepopup.Hide();
        }
        protected void Button2_Click(object sender, EventArgs e)
        {

            dupicatepopup.Hide();
            
        }

        protected void txtPINo_TextChanged(object sender, EventArgs e)
        {
            lblWarning.Text = "";
            divWarning.Visible = false;
            if (!string.IsNullOrEmpty(txtPINo.Text))
            {
                ImportPIHeader pih = CHNLSVC.Financial.GET_IMP_PI(txtPINo.Text);
                if (pih==null)
                {
                    lblWarning.Text = "Please enter valid pi # !!!";
                    divWarning.Visible = true;
                    txtPINo.Text = "";
                    return;
                }
                else
                {
                    PopulatePiWithPiNO(pih);
                }
            }
        }

        private void PopulatePiWithPiNO(ImportPIHeader pih)
        {
            txtPIDate.Text = pih.IP_PI_DT.ToString("dd/MMM/yyyy");
            ddlPaymentTerms.SelectedValue = pih.IP_IMP_TP;
            DdlSubPaymentTermsDataPopulate("IPM", ddlPaymentTerms.Text);
            ddlSubPaymentTerms.SelectedValue = pih.IP_IMP_STP;
            txtCreaditPeriod.Text = pih.IP_ANAL_1.ToString();
            txtRemark.Text = pih.IP_RMK;

            txtOrderPlanNo.Text = pih.IP_OP_NO;
            grdOrderPeriod.DataSource = CHNLSVC.Financial.GetOrderPeriodALL(txtOrderPlanNo.Text);
            grdOrderPeriod.DataBind();

            DataTable dtOrder = CHNLSVC.CommonSearch.SP_Search_Order(txtOrderPlanNo.Text);
            if (dtOrder.Rows.Count > 0)
            {
                //lblSupplier.Text = dtOrder.Rows[0]["IO_SUPP"].ToString();
                txtOrderRefNo.Text = dtOrder.Rows[0]["IO_REF_NO"].ToString();
            }
            txtSupplier.Text = pih.IP_SUPP;
            DdlPortofOriginDataPopulate();
            ddlTradeTerms.SelectedValue = pih.IP_TOP;
            GrdElementDataPopulate(Session["UserCompanyCode"].ToString(), "TOT", ddlTradeTerms.SelectedItem.Value);
            ddlModeofShipment.SelectedValue = pih.IP_TOS;
            ddlPortofOrigin.SelectedValue = pih.IP_FRM_PORT;

            txtManualRefNo.Text = pih.IP_REF_NO;
            chkPartialShipment.Checked = Convert.ToBoolean(Convert.ToInt32(pih.IP_ANAL_2));
            txtETA.Text = pih.IP_ETA_DT.ToString("dd/MMM/yyyy");
            DataTable dtETA = CHNLSVC.Financial.GetSupplierETA(Session["UserCompanyCode"].ToString(), txtSupplier.Text, ddlPortofOrigin.SelectedItem.Value);
            int days = Convert.ToInt32(dtETA.Rows[0]["mspr_lead_time"]);
            txtETD.Text = Convert.ToDateTime(pih.IP_ETA_DT).AddDays(-days).ToString("dd/MMM/yyyy");

            ViewState["SupplerCurrency"] = pih.IP_CUR;
            ViewState["ExchangRate"] = pih.IP_EX_RT;
            txtTotalOrderQty.Text = pih.IP_TOT_QTY.ToString("N2");
            txtTotalOrderValue.Text = pih.IP_TOT_AMT.ToString("N5");
            txtPIStatus.Text = pih.IP_STUS;
            if (pih.IP_STUS == "A")
            {
                txtStatus.Text = "APPROVED";
            }
            else if (pih.IP_STUS == "C")
            {
                txtStatus.Text = "CANCELLED";
            }
            else if (pih.IP_STUS == "F")
            {
                txtStatus.Text = "FINISHED";
            }
            else
            {
                txtStatus.Text = "PENDING";
            }
            txtBank.Text = pih.IP_BANK_CD;
            txtAccount.Text = pih.IP_BANK_ACC_NO;

            List<ImportPIDetails> pid = CHNLSVC.Financial.GET_IMP_PIITEM(txtPINo.Text);

            DataTable dt = new DataTable();
            dt.Columns.Add("IOI_ITM_CD", typeof(string));
            dt.Columns.Add("IOI_DESC", typeof(string));
            dt.Columns.Add("IOI_MODEL", typeof(string));
            dt.Columns.Add("IOI_COLOR", typeof(string));
            dt.Columns.Add("IOI_UOM", typeof(string));
            dt.Columns.Add("IOI_ITM_TP", typeof(string));
            dt.Columns.Add("IOI_BAL_QTY", typeof(decimal));
            dt.Columns.Add("IOI_UNIT_RT", typeof(decimal));
            dt.Columns.Add("ITEMVLUE", typeof(decimal));
            dt.Columns.Add("IOI_YY", typeof(int));
            dt.Columns.Add("IOI_MM", typeof(int));
            dt.Columns.Add("LINE", typeof(int));
            dt.Columns.Add("KIT", typeof(int));
            dt.Columns.Add("KITCODE", typeof(string));
            dt.Columns.Add("OP_LINE", typeof(int));
            dt.Columns.Add("DELETE", typeof(int));

            foreach (ImportPIDetails id in pid)
            {
                DataRow dr = dt.NewRow();
                dr["IOI_ITM_CD"] = id.IPI_ITM_CD;
                dr["IOI_DESC"] = id.IPI_DESC;
                dr["IOI_MODEL"] = id.IPI_MODEL;
                dr["IOI_COLOR"] = id.IPI_COLOR;
                dr["IOI_UOM"] = id.IPI_UOM;
                dr["IOI_ITM_TP"] = id.IPI_ITM_TP;
                dr["IOI_BAL_QTY"] = id.IPI_BAL_QTY;
                dr["IOI_UNIT_RT"] = id.IPI_UNIT_RT;
                dr["ITEMVLUE"] = Convert.ToDouble(id.IPI_QTY) * Convert.ToDouble(id.IPI_UNIT_RT);
                dr["IOI_YY"] = 0;
                dr["IOI_MM"] = 0;
                dr["LINE"] = id.IPI_LINE;
                dr["KIT"] = ViewState["KitItem"];
                dr["KITCODE"] = "";
                dr["OP_LINE"] = 0;//For check updated or not
                dr["DELETE"] = 0;//For New Item
                dt.Rows.Add(dr);
            }


            DataTable dtCost = CHNLSVC.Financial.GET_IMP_PI_COST(txtPINo.Text);

            DataTable dtC = new DataTable();
            dtC.Columns.Add("CODE", typeof(string));
            dtC.Columns.Add("DESCRIPTION", typeof(string));
            dtC.Columns.Add("ELEMENT CATERGORY", typeof(string));
            dtC.Columns.Add("ELEMENT TYPE", typeof(string));
            dtC.Columns.Add("Value", typeof(decimal));
            dtC.Columns.Add("Enable", typeof(bool));

            for (int i = 0; i < dtCost.Rows.Count; i++)
            {
                DataRow drC = dtC.NewRow();
                drC["CODE"] = dtCost.Rows[i]["IPC_ELE_CD"].ToString();
                drC["DESCRIPTION"] = dtCost.Rows[i]["IPC_ELE_CD"].ToString();
                drC["ELEMENT CATERGORY"] = dtCost.Rows[i]["IPC_ELE_CAT"].ToString();
                drC["ELEMENT TYPE"] = dtCost.Rows[i]["IPC_ELE_TP"].ToString();
                drC["Value"] = Convert.ToDecimal(dtCost.Rows[i]["IPC_AMT"].ToString());
                if (dtCost.Rows[i]["IPC_ELE_CD"].ToString() == "COST")
                {
                    drC["Enable"] = 1;
                }
                else
                    drC["Enable"] = 0;
                dtC.Rows.Add(drC);
            }

            grdElement.DataSource = dtC;
            grdElement.DataBind();

             DataView dv = dt.DefaultView;
             dv.Sort = "IOI_ITM_CD";
             DataTable sortedDT = dv.ToTable();

            grdInvoiceDetails.DataSource = sortedDT;
            grdInvoiceDetails.DataBind();
            ViewState["Details"] = sortedDT;
            ViewState["pih"] = pih;
            ViewState["OrderItem"] = sortedDT;

            GridInvoiceDetailsTotal(dt);
            lbtnCancel.Enabled = true;
            lbtnApprove.Enabled = true;
            lbtnUpdate.Enabled = true;
            lbtnSave.Visible = false;
            
        }

        protected void txtOrderPlanNo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOrderPlanNo.Text))
            {
                    GetOrderPeriod();
                    ViewState["Update"] = 1;
            }
        }

        protected void btnSbu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/ADMIN/Home.aspx");
        }

      
    }
}