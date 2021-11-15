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

namespace FastForward.SCMWeb.View.Transaction.Sales
{
    public partial class ReservationCancellation : Base
    {
        protected string _SEQNO { get { return (string)Session["_SEQNO"]; } set { Session["_SEQNO"] = value; } }
        private bool _gridNotClear
        {
            get { if (Session["_gridNotClear"] != null) { return (bool)Session["_gridNotClear"]; } else { return false; } }
            set { Session["_gridNotClear"] = value; }
        }
        protected List<INR_RES_LOG> _resLoglist { get { return (List<INR_RES_LOG>)Session["_resLoglist"]; } set { Session["_resLoglist"] = value; } }
        //private List<INR_RES_LOG> _resLoglist
        //{
        //    get
        //    {
        //        if (Session["_resLoglist"] == null)
        //        {
        //            return new List<INR_RES_LOG>();
        //        }
        //        else
        //        {
        //            return (List<INR_RES_LOG>)Session["_resLoglist"];
        //        }
        //    }
        //    set { }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ValidateTrue();
                if (!IsPostBack)
                {
                    _gridNotClear = false;
                    PageClear();
                    PopulateddlRequestReason();
                    PopulateddlRequestType();
                    //PopulateddlRequestType();
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }

        private void PopulateddlRequestReason()
        {
            ddlRequestReason.DataSource = CHNLSVC.Sales.Select_REF_LOC_CATE1();
            ddlRequestReason.DataTextField = "RLC_DESC";
            ddlRequestReason.DataValueField = "RLC_CD";
            ddlRequestReason.DataBind();
            ddlRequestReason.Items.Insert(0, new ListItem(" - - Select - - ", "0"));
            ddlRequestReason.SelectedValue = "0";
        }
        protected void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCustomer.Text))
                {
                    txtCustomer.Text = string.Empty;
                    txtCustomer.ToolTip = string.Empty;
                   // lblWarning.Text = "Please Enter Invoice Customer.";
                    //divWarning.Visible = true;
                    DisplayMessage("Please Enter invoice customer", 2);
                    return;
                }
                DataTable dt = CHNLSVC.Financial.CheckBusentity(Session["UserCompanyCode"].ToString(), "C", null, txtCustomer.Text);

                if (dt.Rows.Count != 1)
                {
                    txtCustomer.Text = string.Empty;
                    txtCustomer.ToolTip = string.Empty;
                    //lblWarning.Text = "Invalid Invoice Customer.";
                   // divWarning.Visible = true;
                    DisplayMessage("Invalid invoice customer", 2);
                    return;
                }
                else
                {
                    txtCustomer.Text = dt.Rows[0]["CODE"].ToString();
                    txtCustomer.ToolTip = dt.Rows[0]["NAME"].ToString();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void lbtnCustomer_Click(object sender, EventArgs e)
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
                DisplayMessage(ex.ToString(), 4);
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
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void lbtnSalesExcecutive_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesExcecutive);
                DataTable result = CHNLSVC.CommonSearch.SearchExecutive(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "419";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }

        protected void lbtnSearchRequest_Click(object sender, EventArgs e)
        {
            try
            {
                txtReservNo.Text = "";
                DateTime fdate;
                DateTime tdate;
                grdItem.DataSource = new int[] { };
                grdItem.DataBind();
                if (!DateTime.TryParse(txtFromDate.Text, out fdate))
                {
                    //lblWarning.Text = "Please select from date.";
                    //divWarning.Visible = true;
                    DisplayMessage("Please select from date", 2);
                    return;
                }
                if (!DateTime.TryParse(txtToDate.Text, out tdate))
                {
                    //lblWarning.Text = "Please select to date.";
                    //divWarning.Visible = true;
                    DisplayMessage("Please select to date", 2);
                    return;
                }

                string reservationType;
                if (ddlReservationType.SelectedValue == "0")
                    reservationType = null;
                else
                    reservationType = ddlReservationType.SelectedValue;

                string customer;
                if (string.IsNullOrEmpty(txtCustomer.Text))
                    customer = null;
                else
                    customer = txtCustomer.Text;

               // List<INR_RES> inr_res = CHNLSVC.Sales.Select_INR_RES(Session["UserCompanyCode"].ToString(), Session["GlbDefChannel"].ToString(), null, reservationType, customer, fdate, tdate, "A");
                List<INR_RES> inr_res = CHNLSVC.Sales.Select_INR_RES(Session["UserCompanyCode"].ToString(), null, txtreservati.Text, reservationType, customer, fdate, tdate, "A");

                grdReservation.DataSource = inr_res.OrderBy(c=> c.IRS_RES_NO);
                grdReservation.DataBind();

                ViewState["inr_res"] = inr_res;

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void chkReservation_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)sender;
                GridViewRow grdr = (GridViewRow)chk.NamingContainer;
                int rowindex = grdr.RowIndex;
                if (chk.Checked == true)
                {
                    List<INR_RES> int_req = (List<INR_RES>)ViewState["inr_res"];
                    int seqNo = Convert.ToInt32(((Label)grdReservation.Rows[grdr.RowIndex].FindControl("lblseqNo")).Text);
                    List<INR_RES_DET> inr_res_det = CHNLSVC.Sales.GetGetReservationApprovalItem(seqNo);

                    grdItem.DataSource = inr_res_det;
                    grdItem.DataBind();

                    ViewState["RowIndex"] = grdr.RowIndex;

                }
                else
                {
                    grdItem.DataSource = new int[] { };
                    grdItem.DataBind();

                    grdItemLog.DataSource = new int[] { };
                    grdItemLog.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void grdItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    //List<INR_RES> int_req = (List<INR_RES>)ViewState["inr_res"];

            //    int seqNo = Convert.ToInt32(((Label)grdItem.SelectedRow.FindControl("lblseqNo")).Text);
            //    string itemCode = grdItem.SelectedRow.Cells[2].Text;

            //    List<INR_RES_LOG> inr_res_log = CHNLSVC.Sales.GetGetReservationlog(seqNo, itemCode);

            //    grdItemLog.DataSource = inr_res_log;
            //    grdItemLog.DataBind();

            //}
            //catch (Exception ex)
            //{
            //    DisplayMessage(ex.ToString(), 4);
            //}
        }
       

        protected void lbtnGrdSerial_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                List<INT_REQ_ITM> item = (List<INT_REQ_ITM>)ViewState["int_req_itm"];

                ViewState["Item"] = item[grdr.RowIndex].ITRI_ITM_CD;
                ViewState["Location"] = item[grdr.RowIndex].ITRI_LOC;
                ViewState["RowIndex"] = grdr.RowIndex;

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                DataTable result = CHNLSVC.CommonSearch.Search_INR_SER(SearchParams, null, null);
                grdResult1.DataSource = result;
                grdResult1.DataBind();
                lblvalue.Text = "50";
                BindUCtrlDDLData1(result);
                ViewState["SEARCH"] = result;


                List<INT_REQ_SER> req_serList = new List<INT_REQ_SER>();
                if (ViewState["int_req_ser"] != null)
                {
                    req_serList = (List<INT_REQ_SER>)ViewState["int_req_ser"];
                    req_serList = (from i in req_serList
                                   where i.ITRS_ITM_CD == ViewState["Item"].ToString()
                                   select i).ToList();
                }
                grdSerial.DataSource = req_serList;
                grdSerial.DataBind();

                PopupSerial.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void lbtngrdReservationDalete_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(txtStatus.Text))
                //{
                //    lblWarning.Text = "Can Not Delete Items.";
                //    divWarning.Visible = true;
                //    return;
                //}
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                List<INT_REQ_ITM> item = (List<INT_REQ_ITM>)ViewState["int_req_itm"];
                item.RemoveAt(grdr.RowIndex);

                //txtTotalQty.Text = item.Sum(x => x.ITRI_QTY).ToString("N2");

                ViewState["int_req_itm"] = item;
                grdReservation.DataSource = item;
                grdReservation.DataBind();

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void lbtngrdReservationEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                grdReservation.EditIndex = grdr.RowIndex;//e.NewEditIndex;
                List<INT_REQ_ITM> item = (List<INT_REQ_ITM>)ViewState["int_req_itm"];
                grdReservation.DataSource = item;
                grdReservation.DataBind();

                TextBox txtQtyEdit = ((TextBox)grdReservation.Rows[grdr.RowIndex].FindControl("txtQtyEdit"));
                txtQtyEdit.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void lbtngrdReservationCancel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                //e.Cancel = true;
                grdReservation.EditIndex = -1;
                List<INT_REQ_ITM> item = (List<INT_REQ_ITM>)ViewState["int_req_itm"];
                grdReservation.DataSource = item;
                grdReservation.DataBind();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void lbtngrdReservationUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                List<INT_REQ_ITM> item = (List<INT_REQ_ITM>)ViewState["int_req_itm"];
                decimal qty;
                decimal unitrate;

                if (!decimal.TryParse(((TextBox)grdReservation.Rows[grdr.RowIndex].FindControl("txtQtyEdit")).Text, out qty))
                {
                    //lblWarning.Text = "Please Enter valid Item Qty.";
                    //divWarning.Visible = true;
                    DisplayMessage("Please enter valid item qty", 2);
                    return;
                }

                item[grdr.RowIndex].ITRI_QTY = qty;
                grdReservation.EditIndex = -1;
                ViewState["int_req_itm"] = item;
                grdReservation.DataSource = item;
                grdReservation.DataBind();

                //txtTotalQty.Text = item.Sum(x => x.ITRI_QTY).ToString("N2");

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void lbtnSerialDalete_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!string.IsNullOrEmpty(txtStatus.Text))
                //{
                //    lblWarning.Text = "Can Not Delete Items.";
                //    divWarning.Visible = true;
                //    return;
                //}
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                List<INT_REQ_SER> item = (List<INT_REQ_SER>)ViewState["int_req_ser"];
                item.RemoveAt(grdr.RowIndex);

                ViewState["int_req_ser"] = item;

                item = (from i in item
                        where i.ITRS_ITM_CD == ViewState["Item"].ToString()
                        select i).ToList();

                grdSerial.DataSource = item;
                grdSerial.DataBind();

                PopupSerial.Show();

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        
        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
          //  return;
            string err = "";
            try
            {
                if (hdnCancel.Value == "Yes")
                {
                    _gridNotClear = true;
                    int rowindex;
                    
                    //if (ViewState["RowIndex"] == null)
                    //{
                    //   // lblWarning.Text = "Please select the reservation number";
                    //    //divWarning.Visible = true;
                    //    DisplayMessage("Please select the reservation number", 2);
                    //    return;
                    //}
                    //if (!int.TryParse((ViewState["RowIndex"].ToString()), out rowindex))
                    //{
                    //   // lblWarning.Text = "Please select the reservation number";
                    //    //divWarning.Visible = true;
                    //    DisplayMessage("Please select the reservation number", 2);
                    //    return;
                    //}

                    //int seqNo = Convert.ToInt32(((Label)grdReservation.Rows[rowindex].FindControl("lblseqNo")).Text);
                    foreach (GridViewRow _row in grdItemLog.Rows)
                    {
                        string rc = (_row.FindControl("IRL_RES_IQTY") as Label).Text;
                        if (string.IsNullOrEmpty(rc))
                        {
                            DisplayMessage("Please click update button to update the cancel qtys",2);
                            return;
                        }
                    }

                    int row_aff = 0;
                    List<INR_RES_LOG> inr_res_log = CHNLSVC.Sales.GetGetReservationlog((Convert.ToInt32(lblSeq.Text)), null);

                    List<INR_RES_LOG> updateResLogList = new List<INR_RES_LOG>();
                    foreach (GridViewRow _row in grdItemLog.Rows)
                    {
                        //Change by lakshan 18 Oct 2016 Error ocuured while enumerable single or default 
                       // var _filter = inr_res_log.SingleOrDefault(x => x.IRL_ITM_CD == _row.Cells[2].Text);

                        Label CurtLoc = (_row.Cells[1].FindControl("IRL_CURT_LOC") as Label);
                        Label ItmCd = (_row.Cells[1].FindControl("IRL_ITM_CD") as Label);
                        Label curtDocNo = (_row.Cells[5].FindControl("IRL_CURT_DOC_NO") as Label);
                        Label WIP = (_row.Cells[5].FindControl("IRL_RES_WP") as Label);
                        Label line = (_row.Cells[5].FindControl("irl_line") as Label);
                        int resWP = (WIP.Text == "") ? 0 : Convert.ToInt32(WIP.Text);
                        int resline = (line.Text == "") ? 0 : Convert.ToInt32(line.Text);
                        //INR_RES_LOG _filter = inr_res_log.FirstOrDefault(x => x.IRL_ITM_CD == ItmCd.Text && x.IRL_ACT == 1 && x.IRL_CURT_LOC == CurtLoc.Text && x.IRL_CURT_DOC_NO == curtDocNo.Text && x.IRL_RES_WP == resWP && x.IRL_LINE == resline);
                       
                        //if (_filter != null)
                        //{
                        //    Label name = (Label)_row.FindControl("IRL_RES_IQTY");
                        //    string sd = name.Text;
                        //    _filter.IRL_CAN_QTY = Convert.ToDecimal(name.Text);

                        //    if (_filter.IRL_CAN_QTY != 0)
                        //    {
                        //        if (_filter.IRL_RES_BQTY < _filter.IRL_CAN_QTY)
                        //        {
                        //            _filter.IRL_CAN_QTY = _filter.IRL_RES_BQTY;

                        //        }
                        //        updateResLogList.Add(_filter);
                        //    }
                           
                        //}
                        var _filter = inr_res_log.Where(x => x.IRL_ITM_CD == ItmCd.Text && x.IRL_ACT == 1 && x.IRL_CURT_LOC == CurtLoc.Text && x.IRL_CURT_DOC_NO == curtDocNo.Text && x.IRL_RES_WP == resWP ).ToList();

                        if (_filter != null)
                        {
                            Label name = (Label)_row.FindControl("IRL_RES_IQTY");
                            string sd = name.Text;
                            decimal _qty = 0;
                            _qty = Convert.ToDecimal(name.Text);
                  
                            foreach (INR_RES_LOG _lg in _filter)
                            {

                                if (_lg.IRL_CAN_QTY <= _lg.IRL_RES_BQTY)
                                 {
                                     
                                     if (_lg.IRL_RES_BQTY < _qty)
                                     {
                                         _qty = _qty - _lg.IRL_RES_BQTY;
                                         _lg.IRL_CAN_QTY = _lg.IRL_RES_BQTY;
                                         
                                     }
                                     else
                                     {
                                         _lg.IRL_CAN_QTY = _qty;
                                     }
                                     updateResLogList.Add(_lg);
                                 }
                            }

                        }
                    }

                    row_aff = CHNLSVC.Sales.CancelINR_RES_LOG(updateResLogList, 0, Session["UserID"].ToString(), DateTime.Now.Date);
                   
                  
                    if (row_aff == 1)
                    {
                       // lblSuccess.Text = "Successfully Canceled. Request No: ";
                       // divSuccess.Visible = true;
                        DisplayMessage("Successfully Canceled", 3);

                        #region genaret mail 13 Nov 2017 by Nuwan
                        try
                        {
                            CHNLSVC.MsgPortal.SendMailReservationCancel(updateResLogList, "CANCEL");
                        }
                        catch (Exception ex)
                        {
                            string _msg = "Successfully Canceled" + " and Email send fail.";
                            DisplayMessage(_msg,4);
                        }
                        #endregion
                        PageClear();
                        return;
                    }
                    else
                    {
                       // lblWarning.Text = "Cancelation Failed";
                       // divWarning.Visible = true;
                        DisplayMessage("Cancelation Failed", 2);
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
                    try
                    {
                        Session["ResNo"] = "";
                        txtReservNo.Text = "";
                        Response.Redirect(Request.RawUrl, false);
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                    }
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }


        private void DisplayMessage(String Msg, Int32 option)
        {
            string Msgbody = Msg.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");

            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msgbody + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msgbody + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msgbody + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + Msgbody + "');", true);
            }
        }
        private void DispMsg(string msgText, string msgType = "")
        {
            msgText = msgText.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");

            if (msgType == "N")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + msgText + "');", true);
            }
            else if (msgType == "W" || msgType == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msgText + "');", true);
            }
            else if (msgType == "S")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + msgText + "');", true);
            }
            else if (msgType == "E")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + msgText + "');", true);
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
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                ////Item
                //if (lblvalue.Text == "407")
                //{
                //    txtItem.Text = grdResult.SelectedRow.Cells[1].Text;
                //    txtItem_TextChanged(null, null);
                //}
                //RequestNo
                if (lblvalue.Text == "420")
                {
                    //txtRequestNo.Text = grdResult.SelectedRow.Cells[1].Text;
                    //txtRequestNo_TextChanged(null, null);
                }
                //Customer
                if (lblvalue.Text == "401")
                {
                    txtCustomer.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtCustomer.ToolTip = grdResult.SelectedRow.Cells[3].Text;
                    txtCustomer_TextChanged(null, null);
                }
                if (lblvalue.Text == "422")
                {
                    txtreservati.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                ////Location
                //if (lblvalue.Text == "5")
                //{
                //    txtLocation.Text = grdResult.SelectedRow.Cells[1].Text;
                //}
                //Serial
                if (lblvalue.Text == "50")
                {
                    if (!GetSerialVisibility())
                    {
                        //lblWarning.Text = "Duplicate Items Are Not Allowed.";
                        //divWarning.Visible = true;
                        PopupSerial.Show();
                        return;
                    }

                    List<INT_REQ_SER> req_serList = new List<INT_REQ_SER>();
                    INT_REQ_SER req_ser = new INT_REQ_SER();

                    if (ViewState["int_req_ser"] != null)
                    {
                        req_serList = (List<INT_REQ_SER>)ViewState["int_req_ser"];
                    }
                    int lineNo = 1;
                    if (req_serList.Count > 0)
                    {
                        if (req_serList.Exists(x => x.ITRS_ITM_CD == ViewState["Item"].ToString() && x.ITRS_SER_1 == grdResult1.SelectedRow.Cells[1].Text))
                        {
                            //lblWarning.Text = "Duplicate Items Are Not Allowed.";
                            //divWarning.Visible = true;
                            PopupSerial.Show();
                            return;
                        }
                        if (req_serList.Exists(x => x.ITRS_ITM_CD == ViewState["Item"].ToString()))
                        {
                            List<INT_REQ_SER> req = (from re in req_serList
                                                     where re.ITRS_ITM_CD == ViewState["Item"].ToString()
                                                     select re).ToList();
                            lineNo = req.Max(x => x.ITRS_SER_LINE) + 1;
                        }

                    }

                    List<INT_REQ_ITM> item = (List<INT_REQ_ITM>)ViewState["int_req_itm"];
                    int rowIndex = Convert.ToInt32(ViewState["RowIndex"]);

                    req_ser.ITRS_SEQ_NO = item[rowIndex].ITRI_SEQ_NO;
                    req_ser.ITRS_LINE_NO = item[rowIndex].ITRI_LINE_NO;
                    req_ser.ITRS_SER_LINE = lineNo;
                    req_ser.ITRS_ITM_CD = ViewState["Item"].ToString();
                    req_ser.ITRS_ITM_STUS = item[rowIndex].ITRI_ITM_STUS;
                    req_ser.ITRS_SER_1 = grdResult1.SelectedRow.Cells[1].Text;
                    req_ser.ITRS_SER_2 = grdResult1.SelectedRow.Cells[2].Text;
                    //req_ser.ITRS_SER_3        = 
                    //req_ser.ITRS_SER_4        = 
                    req_ser.ITRS_QTY = item[rowIndex].ITRI_QTY;
                    //req_ser.ITRS_IN_SEQNO     = 
                    //req_ser.ITRS_IN_DOCNO     = 
                    //req_ser.ITRS_IN_ITMLINE   = 
                    //req_ser.ITRS_IN_BATCHLINE = 
                    //req_ser.ITRS_IN_SERLINE   = 
                    //req_ser.ITRS_IN_DOCDT     = 
                    //req_ser.ITRS_TRNS_TP      = 
                    //req_ser.ITRS_RMK          = 
                    //req_ser.ITRS_SER_ID       = 
                    //req_ser.ITRS_NITM_STUS    = 

                    req_serList.Add(req_ser);

                    ViewState["int_req_ser"] = req_serList;

                    req_serList = (from i in req_serList
                                   where i.ITRS_ITM_CD == ViewState["Item"].ToString()
                                   select i).ToList();

                    grdSerial.DataSource = req_serList;
                    grdSerial.DataBind();

                    lblvalue.Text = "50";
                    PopupSerial.Show();
                }
                ViewState["SEARCH"] = null;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchbyword.Text) || !string.IsNullOrEmpty(txtSearchbyword1.Text))
                {
                    FilterData();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
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
            //Customer
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
                DataTable result = CHNLSVC.CommonSearch.Search_INT_REQ_RER(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "420";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            //Location
            else if (lblvalue.Text == "5")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "5";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            //Serial
            else if (lblvalue.Text == "50")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                DataTable result = CHNLSVC.CommonSearch.Search_INR_SER(SearchParams, ddlSearchbykey1.SelectedItem.Text, txtSearchbyword1.Text);
                grdResult1.DataSource = result;
                grdResult1.DataBind();
                lblvalue.Text = "50";
                ViewState["SEARCH"] = result;
                PopupSerial.Show();
            }
            else if (lblvalue.Text == "422")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReservationNo);
                DataTable result = CHNLSVC.CommonSearch.Search_INT_RES(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "422";
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
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item_Serials:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + ViewState["Location"] + seperator + ViewState["Item"] + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RequestNo:
                    {
                        paramsText.Append("RER" + seperator);
                        break;
                    }
                         
                case CommonUIDefiniton.SearchUserControlType.ReservationNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["GlbDefChannel"].ToString() + seperator + string.Empty + seperator);
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
        public void BindUCtrlDDLData1(DataTable _dataSource)
        {
            this.ddlSearchbykey1.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey1.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey1.SelectedIndex = 0;
        }
        private void PopulateRequestData()
        {
            //if (string.IsNullOrEmpty(txtRequestNo.Text))
            //{
            //    txtRequestNo.Text = string.Empty;
            //    lblWarning.Text = "Please Enter Request No.";
            //    divWarning.Visible = true;
            //    return;
            //}

            //INT_REQ int_req = new INT_REQ();
            //int_req = CHNLSVC.Sales.GetRequest(txtRequestNo.Text);

            //if (int_req == null)
            //{
            //    txtRequestNo.Text = string.Empty;
            //    lblWarning.Text = "Invalid Request Details.";
            //    divWarning.Visible = true;
            //    return;
            //}

            //ViewState["int_req"] = int_req;

            //DataTable dt = CHNLSVC.Sales.Select_MST_STUS(int_req.ITR_STUS);

            //txtDate.Text = int_req.ITR_DT.ToString("dd/MMM/yyyy");
            //txtExpectedDate.Text = int_req.ITR_EXP_DT.ToString("dd/MMM/yyyy");
            //txtStatus.Text = dt.Rows[0]["MSS_DESC"].ToString();
            //txtCustomer.Text = int_req.ITR_BUS_CODE;
            //txtRemarks.Text = int_req.ITR_NOTE;
            //ddlRequestType.SelectedValue = int_req.ITR_ANAL1;

            //ddlRequestReason.SelectedValue = int_req.ITR_SUB_TP;

            //List<INT_REQ_ITM> int_req_itm = new List<INT_REQ_ITM>();
            //int_req_itm = CHNLSVC.Sales.GetRequestItem(int_req.ITR_SEQ_NO);

            //if (int_req_itm.Count <= 0)
            //{
            //    txtRequestNo.Text = string.Empty;
            //    lblWarning.Text = "Invalid Request Item Details.";
            //    divWarning.Visible = true;
            //    return;
            //}

            //ViewState["DaleteVisibility"] = false;
            //grdReservation.DataSource = int_req_itm;
            //grdReservation.DataBind();
            //ViewState["int_req_itm"] = int_req_itm;
            //txtTotalQty.Text = int_req_itm.Sum(x => x.ITRI_QTY).ToString("N2");

            //List<INT_REQ_SER> int_req_ser = new List<INT_REQ_SER>();
            //int_req_ser = CHNLSVC.Sales.GetINT_REQ_SER(int_req.ITR_SEQ_NO);
            //ViewState["int_req_ser"] = int_req_ser;

            ////lbtnSave.Enabled = false;
            ////lbtnSave.OnClientClick = "return Enable();";
            ////lbtnSave.CssClass = "buttoncolor";

            //if (int_req_itm.Sum(x => x.ITRI_APP_QTY) == 0 && int_req.ITR_STUS == "P")
            //{
            //    if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16014))
            //    {
            //        //lbtnApprove.Enabled = true;
            //        //lbtnApprove.OnClientClick = "ConfirmApprove();";
            //        //lbtnApprove.CssClass = "buttonUndocolor";
            //    }
            //    if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16015))
            //    {
            //        lbtnCancel.Enabled = true;
            //        lbtnCancel.OnClientClick = "ConfirmCancel();";
            //        lbtnCancel.CssClass = "buttonUndocolor";
            //    }

            //    //lbtnUpdate.Enabled = true;
            //    //lbtnUpdate.OnClientClick = "ConfirmUpdate();";
            //    //lbtnUpdate.CssClass = "buttonUndocolor";
            //}

            //if (int_req_itm.Sum(x => x.ITRI_APP_QTY) == 0 && int_req.ITR_STUS == "A")
            //{
            //    if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16015))
            //    {
            //        lbtnCancel.Enabled = true;
            //        lbtnCancel.OnClientClick = "ConfirmCancel();";
            //        lbtnCancel.CssClass = "buttonUndocolor";
            //    }
            //}

            //lbtnAdd.Visible = false;
            //lbtnClearItem.Visible = false;

        }

        protected void ddlRequestReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateddlRequestType();
        }
        private void PopulateddlRequestType()
        {
            ddlReservationType.DataSource = CHNLSVC.Sales.Select_REF_REQ_SUBTP(ddlRequestReason.SelectedValue); 
            ddlReservationType.DataTextField = "RRS_DESC";
            ddlReservationType.DataValueField = "RRS_TP";
            ddlReservationType.DataBind();
            ddlReservationType.Items.Insert(0, new ListItem(" - - Select - - ", "0"));
            ddlReservationType.SelectedValue = "0";
        }
        public Boolean GetDaleteVisibility()
        {
            Boolean b = false;
            b = (Boolean)ViewState["DaleteVisibility"];
            return b;
        }
        public Boolean GetSerialVisibility()
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
            txtReservNo.Text = "";
            lblSeq.Text = "";
            _resLoglist = new List<INR_RES_LOG>();
            txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
            lbtnFromDate.Enabled = true;
            txtToDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            ViewState["RowIndex"] = "";
            txtCustomer.Text = string.Empty;
            //txtRemarks.Text = string.Empty;
            ddlReservationType.SelectedIndex = -1;
            //ddlRequestType.SelectedIndex = -1;
            grdItemLog.Columns[6].Visible = false;
            grdItemLog.Columns[5].Visible = true; 

            //txtItem.Text = string.Empty;
            //txtItem.ReadOnly = false;
            //txtQty.Text = string.Empty;
            //ddlStatus.SelectedIndex = -1;
            //txtLocation.Text = string.Empty;

            //lblModel.Text = string.Empty;
            //lblDescription.Text = string.Empty;
            //lblBrand.Text = string.Empty;
            //lblPartNo.Text = string.Empty;

            //lbtnAdd.Visible = true;
            //lbtnClearItem.Visible = true;
            if (!_gridNotClear)
            {
                grdReservation.DataSource = new int[] { };
                grdReservation.DataBind();
            }
           
           // grdItem.DataSource = new int[] { };
           // grdItem.DataBind();
            loaditem();
            grdItemLog.DataSource = new int[] { };
            grdItemLog.DataBind();



            grdSerial.DataSource = new int[] { };
            grdSerial.DataBind();
            grdResult.DataSource = new int[] { };
            grdResult.DataBind();
            grdResult1.DataSource = new int[] { };
            grdResult1.DataBind();

            //txtTotalQty.Text = string.Empty;

            ViewState["int_req"] = null;
            ViewState["int_req_itm"] = null;
            ViewState["int_req_ser"] = null;
            ViewState["Item"] = null;
            ViewState["Location"] = null;
            ViewState["RowIndex"] = null;
            ViewState["DaleteVisibility"] = true;

            ViewState["RowIndex"] = null;

            //lbtnSave.Enabled = true;
            //lbtnSave.OnClientClick = "ConfirmSave();";
            //lbtnSave.CssClass = "buttonUndocolor";
            //lbtnUpdate.Enabled = false;
            //lbtnUpdate.OnClientClick = "return Enable();";
            //lbtnUpdate.CssClass = "buttoncolor";
            //lbtnCancel.Enabled = true;
            //lbtnCancel.OnClientClick = "return Enable();";
            //lbtnCancel.CssClass = "buttoncolor";
            //lbtnApprove.Enabled = false;
            //lbtnApprove.OnClientClick = "return Enable();";
            //lbtnApprove.CssClass = "buttoncolor";

            //txtIrlResIQty.ReadOnly = false;
            _gridNotClear = false;
        }
        private void loaditem()
        {
            string _resno = Session["ResNo"]as string;
            INR_RES_LOG inrResLog = new INR_RES_LOG();
            inrResLog.IRL_RES_NO = _resno;
            inrResLog.IRL_ACT = 1;
            List<INR_RES_LOG> inrResLogDet = CHNLSVC.Inventory.GET_INR_RES_LOG_DATA_NEW(inrResLog);
            List<INR_RES_LOG> logList = inrResLogDet
                                    .GroupBy(u => new
                                    {
                                        u.IRL_ITM_CD,
                                        u.IRL_ITM_STUS
                                    })
                                    .Select(cl => new INR_RES_LOG
                                    {
                                        IRL_ACT = cl.First().IRL_ACT,
                                        IRL_BASE_LINE = cl.First().IRL_BASE_LINE,
                                        IRL_CAN_QTY = cl.First().IRL_CAN_QTY,
                                        IRL_CRE_BY = cl.First().IRL_CRE_BY,
                                        IRL_CRE_DT = cl.First().IRL_CRE_DT,
                                        IRL_CRE_SESSION = cl.First().IRL_CRE_SESSION,
                                        IRL_CURT_BATCH_LINE = cl.First().IRL_CURT_BATCH_LINE,
                                        IRL_CURT_COM = cl.First().IRL_CURT_COM,
                                        IRL_CURT_DOC_DT = cl.First().IRL_CURT_DOC_DT,
                                        IRL_CURT_DOC_NO = cl.First().IRL_CURT_DOC_NO,
                                        IRL_CURT_DOC_TP = cl.First().IRL_CURT_DOC_TP,
                                        IRL_CURT_ITM_LINE = cl.First().IRL_CURT_ITM_LINE,
                                        IRL_CURT_LOC = cl.First().IRL_CURT_LOC,
                                        IRL_ITM_CD = cl.First().IRL_ITM_CD,
                                        IRL_ITM_STUS = cl.First().IRL_ITM_STUS,
                                        IRL_LINE = cl.First().IRL_LINE,
                                        IRL_MOD_BY = cl.First().IRL_MOD_BY,
                                        IRL_MOD_BY_NEW = cl.First().IRL_MOD_BY_NEW,
                                        IRL_MOD_DT = cl.First().IRL_MOD_DT,
                                        IRL_MOD_SESSION = cl.First().IRL_MOD_SESSION,
                                        IRL_ORIG_BATCH_LINE = cl.First().IRL_ORIG_BATCH_LINE,
                                        IRL_ORIG_COM = cl.First().IRL_ORIG_COM,
                                        IRL_ORIG_DOC_DT = cl.First().IRL_ORIG_DOC_DT,
                                        IRL_ORIG_DOC_NO = cl.First().IRL_ORIG_DOC_NO,
                                        IRL_ORIG_DOC_TP = cl.First().IRL_ORIG_DOC_TP,
                                        IRL_ORIG_ITM_LINE = cl.First().IRL_ORIG_ITM_LINE,
                                        IRL_ORIG_LOC = cl.First().IRL_ORIG_LOC,
                                        IRL_REQ_NO = cl.First().IRL_REQ_NO,
                                        IRL_RES_LINE = cl.First().IRL_RES_LINE,
                                        IRL_RES_NO = cl.First().IRL_RES_NO,
                                        IRL_RES_WP = cl.First().IRL_RES_WP,
                                        IRL_SEQ = cl.First().IRL_SEQ,
                                        IRL_STUS_DESC = cl.First().IRL_STUS_DESC,
                                        Temp_IRL_MOD_BY = cl.First().Temp_IRL_MOD_BY,
                                        TMP_IRL_RES_BQTY = cl.First().TMP_IRL_RES_BQTY,
                                        BL_NO = cl.First().BL_NO,
                                        LOC_CD = cl.First().LOC_CD,
                                        IRL_RES_QTY = cl.Sum(c => c.IRL_RES_QTY - c.IRL_RES_IQTY),
                                        IRL_RES_BQTY = cl.Sum(c => c.IRL_RES_BQTY),
                                        IRL_RES_CQTY = cl.Sum(c => c.IRL_RES_CQTY),
                                        IRL_RES_IQTY = cl.Sum(c => c.IRL_RES_IQTY),
                                    })
                                    .ToList();

            if (logList != null)
            {
                if (logList.Count > 0)
                {
                    grdItem.DataSource = logList.OrderBy(c => c.IRL_ITM_CD);
                }
            }
            grdItem.DataBind();

        }
        protected void lbtnAddRes_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow item in grdReservation.Rows)
                {
                    item.BackColor = System.Drawing.Color.Transparent;
                }
                LinkButton lbtn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)lbtn.NamingContainer;
                grdItem.DataSource = new int[] { };
                Label lblseqNo = grdr.FindControl("lblseqNo") as Label;
                Int32 seqNo = Convert.ToInt32(lblseqNo.Text);
                int rowindex = grdr.RowIndex;
                List<INR_RES_DET> inr_res_det = CHNLSVC.Sales.GetGetReservationApprovalItem(seqNo);

                //if (inr_res_det != null)
                //{
                //    if (inr_res_det.Count > 0)
                //    {
                //        grdItem.DataSource = inr_res_det.OrderBy(c => c.IRD_ITM_CD);
                //    }
                //}

                List<INR_RES_DET> inr_res_detNew = inr_res_det.Where(x => x.IRD_RES_BQTY > 0).ToList();

                Label lblResNo = grdr.FindControl("lblResNo") as Label;

                Session["ResNo"] = lblResNo.Text;
                txtReservNo.Text = lblResNo.Text;
                grdr.BackColor = System.Drawing.Color.LightCyan;

                INR_RES_LOG inrResLog = new INR_RES_LOG();
                inrResLog.IRL_RES_NO = lblResNo.Text;
                inrResLog.IRL_ACT = 1;

                List<INR_RES_LOG> inrResLogDet = CHNLSVC.Inventory.GET_INR_RES_LOG_DATA_NEW(inrResLog);
                

                List<INR_RES_LOG> logList = inrResLogDet
                                        .GroupBy(u => new
                                                        {
                                                            u.IRL_ITM_CD,
                                                            u.IRL_ITM_STUS
                                                        })
                                        .Select(cl => new INR_RES_LOG
                                        {
                                            IRL_ACT = cl.First().IRL_ACT,
                                            IRL_BASE_LINE = cl.First().IRL_BASE_LINE,
                                            IRL_CAN_QTY = cl.First().IRL_CAN_QTY,
                                            IRL_CRE_BY = cl.First().IRL_CRE_BY,
                                            IRL_CRE_DT = cl.First().IRL_CRE_DT,
                                            IRL_CRE_SESSION = cl.First().IRL_CRE_SESSION,
                                            IRL_CURT_BATCH_LINE = cl.First().IRL_CURT_BATCH_LINE,
                                            IRL_CURT_COM = cl.First().IRL_CURT_COM,
                                            IRL_CURT_DOC_DT = cl.First().IRL_CURT_DOC_DT,
                                            IRL_CURT_DOC_NO = cl.First().IRL_CURT_DOC_NO,
                                            IRL_CURT_DOC_TP = cl.First().IRL_CURT_DOC_TP,
                                            IRL_CURT_ITM_LINE = cl.First().IRL_CURT_ITM_LINE,
                                            IRL_CURT_LOC = cl.First().IRL_CURT_LOC,
                                            IRL_ITM_CD = cl.First().IRL_ITM_CD,
                                            IRL_ITM_STUS = cl.First().IRL_ITM_STUS,
                                            IRL_LINE = cl.First().IRL_LINE,
                                            IRL_MOD_BY = cl.First().IRL_MOD_BY,
                                            IRL_MOD_BY_NEW = cl.First().IRL_MOD_BY_NEW,
                                            IRL_MOD_DT = cl.First().IRL_MOD_DT,
                                            IRL_MOD_SESSION = cl.First().IRL_MOD_SESSION,
                                            IRL_ORIG_BATCH_LINE = cl.First().IRL_ORIG_BATCH_LINE,
                                            IRL_ORIG_COM = cl.First().IRL_ORIG_COM,
                                            IRL_ORIG_DOC_DT = cl.First().IRL_ORIG_DOC_DT,
                                            IRL_ORIG_DOC_NO = cl.First().IRL_ORIG_DOC_NO,
                                            IRL_ORIG_DOC_TP = cl.First().IRL_ORIG_DOC_TP,
                                            IRL_ORIG_ITM_LINE = cl.First().IRL_ORIG_ITM_LINE,
                                            IRL_ORIG_LOC = cl.First().IRL_ORIG_LOC,
                                            IRL_REQ_NO = cl.First().IRL_REQ_NO,
                                            IRL_RES_LINE = cl.First().IRL_RES_LINE,
                                            IRL_RES_NO = cl.First().IRL_RES_NO,
                                            IRL_RES_WP = cl.First().IRL_RES_WP,
                                            IRL_SEQ = cl.First().IRL_SEQ,
                                            IRL_STUS_DESC = cl.First().IRL_STUS_DESC,
                                            Temp_IRL_MOD_BY = cl.First().Temp_IRL_MOD_BY,
                                            TMP_IRL_RES_BQTY = cl.First().TMP_IRL_RES_BQTY,
                                            BL_NO = cl.First().BL_NO,
                                            LOC_CD = cl.First().LOC_CD,
                                            IRL_RES_QTY = cl.Sum(c => c.IRL_RES_QTY - c.IRL_RES_IQTY),
                                            IRL_RES_BQTY = cl.Sum(c => c.IRL_RES_BQTY),
                                            IRL_RES_CQTY = cl.Sum(c => c.IRL_RES_CQTY),
                                            IRL_RES_IQTY = cl.Sum(c => c.IRL_RES_IQTY),
                                        })
                                        .ToList();
                //List<INR_RES_LOG> logList = groupedLogList.ToList();


                //foreach (INR_RES_DET resDet in inr_res_detNew)
                //{
                //    logList.Where(c => c.IRL_RES_NO == resDet.IRD_RES_NO && c.IRL_ITM_CD == resDet.IRD_ITM_CD).FirstOrDefault().IRL_RES_IQTY = resDet.IRD_RES_CQTY;
                //    logList.Where(c => c.IRL_RES_NO == resDet.IRD_RES_NO && c.IRL_ITM_CD == resDet.IRD_ITM_CD).FirstOrDefault().IRL_REQ_NO = resDet.IRD_RESREQ_NO;
                //    logList.Where(c => c.IRL_RES_NO == resDet.IRD_RES_NO && c.IRL_ITM_CD == resDet.IRD_ITM_CD).FirstOrDefault().IRL_STUS_DESC = resDet.MIS_DESC;
                //}

                foreach (INR_RES_LOG _logDet in logList)
                {
                    _logDet.IRL_REQ_NO = inr_res_det.Where(x => x.IRD_ITM_CD == _logDet.IRL_ITM_CD).FirstOrDefault().IRD_RESREQ_NO;

                }

                
                _resLoglist = new List<INR_RES_LOG>();
                if (logList != null)
                {
                    if (logList.Count > 0)
                    {
                        grdItem.DataSource = logList.OrderBy(c => c.IRL_ITM_CD);
                    }
                }
                grdItem.DataBind();
                BindItemGridData();

                grdItemLog.DataSource = _resLoglist;
                grdItemLog.DataBind();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        private void BindItemGridData()
        {
            foreach(GridViewRow item in grdItem.Rows)
            {
                Label lblIRD_ITM_CD = item.FindControl("lblIRD_ITM_CD") as Label;
                Label lblItmDes = item.FindControl("lblItmDes") as Label;
                Label lblItmModel = item.FindControl("lblMI_MODEL") as Label;
                MasterItem _mstItem = CHNLSVC.General.GetItemMaster(lblIRD_ITM_CD.Text);
                if (_mstItem!=null)
                {
                    lblItmDes.Text = _mstItem.Mi_shortdesc;
                    lblItmModel.Text = _mstItem.Mi_model;
                }
            }
        }
        protected void lbtnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                lblSeq.Text = "";
                LinkButton lbtn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)lbtn.NamingContainer;

                grdItemLog.Columns[6].Visible = false;
                grdItemLog.Columns[5].Visible = true; 

                Label lblseqNo = grdr.FindControl("lblseqNo") as Label;
                Label lblIRD_ITM_CD = grdr.FindControl("lblIRD_ITM_CD") as Label;
                int seqNo = Convert.ToInt32(lblseqNo.Text);
                string itemCode = lblIRD_ITM_CD.Text;

                string txtResNo = Session["ResNo"] as string;

                INR_RES_LOG resNoObj = new INR_RES_LOG();
                resNoObj.IRL_RES_NO = txtResNo;

                //List<INR_RES_LOG> inr_res_log = CHNLSVC.Sales.GetGetReservationlog(seqNo, itemCode);
                List<INR_RES_LOG> _resLogList = CHNLSVC.Inventory.GET_INR_RES_LOG_DATA_NEW(resNoObj);

                List<INR_RES_LOG> logList = _resLogList
                                        .Where(x => x.IRL_RES_BQTY > 0 && x.IRL_ACT == 1 && x.IRL_ITM_CD == itemCode)
                                        .GroupBy(u => new
                                        {
                                            u.IRL_ITM_CD,
                                            u.IRL_CURT_LOC,
                                            u.IRL_CURT_DOC_NO,
                                            u.IRL_RES_WP
                                        })
                                        .Select(cl => new INR_RES_LOG
                                        {
                                            IRL_ACT = cl.First().IRL_ACT,
                                            IRL_BASE_LINE = cl.First().IRL_BASE_LINE,
                                            IRL_CAN_QTY = cl.First().IRL_CAN_QTY,
                                            IRL_CRE_BY = cl.First().IRL_CRE_BY,
                                            IRL_CRE_DT = cl.First().IRL_CRE_DT,
                                            IRL_CRE_SESSION = cl.First().IRL_CRE_SESSION,
                                            IRL_CURT_BATCH_LINE = cl.First().IRL_CURT_BATCH_LINE,
                                            IRL_CURT_COM = cl.First().IRL_CURT_COM,
                                            IRL_CURT_DOC_DT = cl.First().IRL_CURT_DOC_DT,
                                            IRL_CURT_DOC_NO = cl.First().IRL_CURT_DOC_NO,
                                            IRL_CURT_DOC_TP = cl.First().IRL_CURT_DOC_TP,
                                            IRL_CURT_ITM_LINE = cl.First().IRL_CURT_ITM_LINE,
                                            IRL_CURT_LOC = cl.First().IRL_CURT_LOC,
                                            IRL_ITM_CD = cl.First().IRL_ITM_CD,
                                            IRL_ITM_STUS = cl.First().IRL_ITM_STUS,
                                            IRL_LINE = cl.First().IRL_LINE,
                                            IRL_MOD_BY = cl.First().IRL_MOD_BY,
                                            IRL_MOD_BY_NEW = cl.First().IRL_MOD_BY_NEW,
                                            IRL_MOD_DT = cl.First().IRL_MOD_DT,
                                            IRL_MOD_SESSION = cl.First().IRL_MOD_SESSION,
                                            IRL_ORIG_BATCH_LINE = cl.First().IRL_ORIG_BATCH_LINE,
                                            IRL_ORIG_COM = cl.First().IRL_ORIG_COM,
                                            IRL_ORIG_DOC_DT = cl.First().IRL_ORIG_DOC_DT,
                                            IRL_ORIG_DOC_NO = cl.First().IRL_ORIG_DOC_NO,
                                            IRL_ORIG_DOC_TP = cl.First().IRL_ORIG_DOC_TP,
                                            IRL_ORIG_ITM_LINE = cl.First().IRL_ORIG_ITM_LINE,
                                            IRL_ORIG_LOC = cl.First().IRL_ORIG_LOC,
                                            IRL_REQ_NO = cl.First().IRL_REQ_NO,
                                            IRL_RES_LINE = cl.First().IRL_RES_LINE,
                                            IRL_RES_NO = cl.First().IRL_RES_NO,
                                            IRL_RES_WP = cl.First().IRL_RES_WP,
                                            IRL_SEQ = cl.First().IRL_SEQ,
                                            IRL_STUS_DESC = cl.First().IRL_STUS_DESC,
                                            Temp_IRL_MOD_BY = cl.First().Temp_IRL_MOD_BY,
                                            TMP_IRL_RES_BQTY = cl.First().TMP_IRL_RES_BQTY,
                                            BL_NO = cl.First().BL_NO,
                                            LOC_CD = cl.First().LOC_CD,
                                            IRL_RES_QTY = cl.Sum(c => c.IRL_RES_QTY - c.IRL_RES_IQTY),
                                            IRL_RES_BQTY = cl.Sum(c => c.IRL_RES_BQTY),
                                            IRL_RES_CQTY = cl.Sum(c => c.IRL_RES_CQTY),
                                            IRL_RES_IQTY = cl.Sum(c => c.IRL_RES_IQTY),
                                        })
                                        .ToList();

                INR_RES_LOG _resLog = logList.FirstOrDefault();

                bool isCurLocEmpty = false;
                foreach (INR_RES_LOG log in logList)
                {
                    if (string.IsNullOrEmpty(log.IRL_CURT_LOC))
                    {
                        isCurLocEmpty = true;
                        break;
                    }
                }

                if (isCurLocEmpty)
                {
                    DisplayMessage("Reservation contains items without current location!!!", 2);
                    return;
                }

                if (logList != null)
                {
                    if (logList.Count > 0)
                    {
                        lblSeq.Text = seqNo.ToString();
                        //txtCom.Text = _resLog.IRL_CURT_COM;
                        //txtLoc.Text = _resLog.IRL_CURT_LOC;
                        //txtItm.Text = _resLog.IRL_ITM_CD;
                        //txtRes.Text = _resLog.IRL_RES_QTY.ToString("N2");
                        //txtBalQty.Text = _resLog.IRL_RES_BQTY.ToString("N2");
                        //txtCancelQty.Text = "";
                        grdItemLog.DataSource = logList;
                        grdItemLog.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }

        protected void lbtnAddQty_Click(object sender, EventArgs e)
        {
            try
            {
                bool _fondrecord = false;
                //     List<INR_RES_LOG> _tempResLog = _resLog;

                INR_RES_LOG inrResLog = new INR_RES_LOG();
                inrResLog.IRL_RES_NO = Session["ResNo"] as string;
                inrResLog.IRL_ACT = 1;
                List<INR_RES_LOG> inrResLogDet = CHNLSVC.Inventory.GET_INR_RES_LOG_DATA_NEW(inrResLog);

                List<INR_RES_LOG> logList = inrResLogDet
                                       .Where(x => x.IRL_RES_WP == 0)
                                       .GroupBy(u => new
                                       {
                                           u.IRL_ITM_CD,
                                           u.IRL_ITM_STUS
                                       })
                                       .Select(cl => new INR_RES_LOG
                                       {
                                           IRL_ACT = cl.First().IRL_ACT,
                                           IRL_BASE_LINE = cl.First().IRL_BASE_LINE,
                                           IRL_CAN_QTY = cl.First().IRL_CAN_QTY,
                                           IRL_CRE_BY = cl.First().IRL_CRE_BY,
                                           IRL_CRE_DT = cl.First().IRL_CRE_DT,
                                           IRL_CRE_SESSION = cl.First().IRL_CRE_SESSION,
                                           IRL_CURT_BATCH_LINE = cl.First().IRL_CURT_BATCH_LINE,
                                           IRL_CURT_COM = cl.First().IRL_CURT_COM,
                                           IRL_CURT_DOC_DT = cl.First().IRL_CURT_DOC_DT,
                                           IRL_CURT_DOC_NO = cl.First().IRL_CURT_DOC_NO,
                                           IRL_CURT_DOC_TP = cl.First().IRL_CURT_DOC_TP,
                                           IRL_CURT_ITM_LINE = cl.First().IRL_CURT_ITM_LINE,
                                           IRL_CURT_LOC = cl.First().IRL_CURT_LOC,
                                           IRL_ITM_CD = cl.First().IRL_ITM_CD,
                                           IRL_ITM_STUS = cl.First().IRL_ITM_STUS,
                                           IRL_LINE = cl.First().IRL_LINE,
                                           IRL_MOD_BY = cl.First().IRL_MOD_BY,
                                           IRL_MOD_BY_NEW = cl.First().IRL_MOD_BY_NEW,
                                           IRL_MOD_DT = cl.First().IRL_MOD_DT,
                                           IRL_MOD_SESSION = cl.First().IRL_MOD_SESSION,
                                           IRL_ORIG_BATCH_LINE = cl.First().IRL_ORIG_BATCH_LINE,
                                           IRL_ORIG_COM = cl.First().IRL_ORIG_COM,
                                           IRL_ORIG_DOC_DT = cl.First().IRL_ORIG_DOC_DT,
                                           IRL_ORIG_DOC_NO = cl.First().IRL_ORIG_DOC_NO,
                                           IRL_ORIG_DOC_TP = cl.First().IRL_ORIG_DOC_TP,
                                           IRL_ORIG_ITM_LINE = cl.First().IRL_ORIG_ITM_LINE,
                                           IRL_ORIG_LOC = cl.First().IRL_ORIG_LOC,
                                           IRL_REQ_NO = cl.First().IRL_REQ_NO,
                                           IRL_RES_LINE = cl.First().IRL_RES_LINE,
                                           IRL_RES_NO = cl.First().IRL_RES_NO,
                                           IRL_RES_WP = cl.First().IRL_RES_WP,
                                           IRL_SEQ = cl.First().IRL_SEQ,
                                           IRL_STUS_DESC = cl.First().IRL_STUS_DESC,
                                           Temp_IRL_MOD_BY = cl.First().Temp_IRL_MOD_BY,
                                           TMP_IRL_RES_BQTY = cl.First().TMP_IRL_RES_BQTY,
                                           BL_NO = cl.First().BL_NO,
                                           LOC_CD = cl.First().LOC_CD,
                                           IRL_RES_QTY = cl.Sum(c => c.IRL_RES_QTY- c.IRL_RES_IQTY),
                                           IRL_RES_BQTY = cl.Sum(c => c.IRL_RES_BQTY),
                                           IRL_RES_CQTY = cl.Sum(c => c.IRL_RES_CQTY),
                                           IRL_RES_IQTY = cl.Sum(c => c.IRL_RES_IQTY),
                                       })
                                       .ToList();


                INR_RES_LOG _resLog = new INR_RES_LOG();
                //List<INR_RES_LOG> inr_res_log = CHNLSVC.Sales.GetGetReservationlog(seqNo, null);
                if (logList.Count > 0)
                {
                    //var _filter = logList.Find(x => x.IRL_SEQ == Convert.ToInt32(lblSeq.Text) && x.IRL_ITM_CD == txtItm.Text
                    //    && x.IRL_CURT_COM == txtCom.Text && x.IRL_CURT_LOC == txtLoc.Text);
                    //if (_filter != null)
                    //{
                    //    //if (_filter.IRL_RES_QTY >= Convert.ToDecimal(txtCancelQty.Text))
                    //    //{
                    //    //    _filter.IRL_ORIG_COM = txtCom.Text;
                    //    //    _filter.IRL_ORIG_LOC = txtLoc.Text;
                    //    //    _filter.IRL_ITM_CD = txtItm.Text;
                    //    //    _filter.IRL_RES_QTY = Convert.ToDecimal(txtRes.Text);
                    //    //    _filter.IRL_RES_BQTY = Convert.ToDecimal(txtBalQty.Text);
                    //    //    _filter.IRL_RES_IQTY = Convert.ToDecimal(txtCancelQty.Text);
                    //    //    _resLoglist.Add(_filter);
                    //    //    _fondrecord = true;
                    //    //}
                    //    //else
                    //    //{
                    //    //    DisplayMessage("Only " + _filter.IRL_RES_QTY + " qtys are allowed for cancellation", 2);
                    //    //}
                    //}

                }
                else
                {
                    DisplayMessage("Reservation is in progress. Not allowed for cancellation", 2);
                }
                //if (_fondrecord == false)
                //{
                //    _resLog.IRL_ORIG_COM = txtCom.Text;
                //    _resLog.IRL_ORIG_LOC = txtLoc.Text;
                //    _resLog.IRL_ITM_CD = txtItm.Text;
                //    _resLog.IRL_RES_QTY = Convert.ToDecimal(txtRes.Text);
                //    _resLog.IRL_RES_BQTY = Convert.ToDecimal(txtBalQty.Text);
                //    _resLog.IRL_RES_IQTY = Convert.ToDecimal(txtCancelQty.Text);
                //    _resLoglist.Add(_resLog);
                //}

                grdItemLog.DataSource = _resLoglist;
                grdItemLog.DataBind();
                //txtCom.Text = string.Empty;
                //txtLoc.Text = string.Empty;
                //txtItm.Text = string.Empty;
                lblSeq.Visible = false;
                lblSeq.Text = string.Empty;
                //txtRes.Text = "0";
                //txtBalQty.Text = "0";
                //txtCancelQty.Text = "0";
            }

            catch (Exception ex)
            {
                DispMsg(ex.ToString(), "E");
            }
        }

        protected void lbtnUpdtQty_Click(object sender, EventArgs e)
        {

            foreach (GridViewRow row in grdItemLog.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                        TextBox usrCanclQty = (row.Cells[5].FindControl("txtIrlResIQty") as TextBox);
                        Label LblCanclQty = (row.Cells[6].FindControl("IRL_RES_IQTY") as Label);
                        LblCanclQty.Text = Convert.ToDecimal(usrCanclQty.Text).ToString("N2");
                }
            }
            grdItemLog.Columns[6].Visible = true;
            grdItemLog.Columns[5].Visible = false; 
        }

        protected void txtIrlResIQty_TextChanged(object sender, EventArgs e)
        {
            var txtbx = (TextBox)sender;
            var _row = (GridViewRow)txtbx.NamingContainer;
            if (_row != null)
            {
                Label CurtLoc = (_row.FindControl("IRL_CURT_LOC") as Label);
                TextBox usrCanclQty = (_row.FindControl("txtIrlResIQty") as TextBox);
                Label resBQty = (_row.FindControl("IRL_RES_BQTY") as Label);

                Label itmCd = (_row.FindControl("IRL_ITM_CD") as Label);
                Label WIP = (_row.FindControl("IRL_RES_WP") as Label);

                MasterItem item = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itmCd.Text);



                if (string.IsNullOrEmpty(usrCanclQty.Text))
                {
                    usrCanclQty.Text = (0.00m).ToString();
                }

                if (CurtLoc.Text == "GIT")
                {
                    DisplayMessage("Cannot cancel Items in GIT", 2);
                    usrCanclQty.Text = (0.00m).ToString();
                    usrCanclQty.ReadOnly = true;
                }

                if (WIP.Text == "1")
                {
                    DisplayMessage("Cannot cancel Items in progress", 2);
                    usrCanclQty.Text = (0.00m).ToString();
                    usrCanclQty.ReadOnly = true;
                }

                decimal result;
                if (decimal.TryParse(usrCanclQty.Text, out result))
                {
                    if ((result - Math.Round(result)) != 0)
                    {
                        if (item.Mi_is_ser1 != -1)
                        {
                            DisplayMessage("Item does not allow decimal qtys", 2);
                            usrCanclQty.Text = (0.00m).ToString();
                            return;
                        }
                    }
                }


                else
                {
                    if (Convert.ToInt32(resBQty.Text) < Convert.ToInt32(Convert.ToDouble(usrCanclQty.Text)))
                    {
                        DisplayMessage("Cannot cancel Items more than balance qty", 2);
                        usrCanclQty.Text = (0.00m).ToString();
                    }
                    else
                    {
                        decimal cancelQty = Convert.ToDecimal(usrCanclQty.Text);
                        usrCanclQty.Text = cancelQty.ToString("N2");
                    }
                }
            }

            //foreach (GridViewRow row in grdItemLog.Rows)
            //{
            //    if (row.RowType == DataControlRowType.DataRow)
            //    {
            //        Label CurtLoc = (row.Cells[1].FindControl("IRL_CURT_LOC") as Label);
            //        TextBox usrCanclQty = (row.Cells[5].FindControl("txtIrlResIQty") as TextBox);
            //        Label resBQty = (row.Cells[1].FindControl("IRL_RES_BQTY") as Label);

            //        Label itmCd = (row.Cells[1].FindControl("IRL_ITM_CD") as Label);
            //        Label WIP = (row.Cells[1].FindControl("IRL_RES_WP") as Label);

            //        MasterItem item = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itmCd.Text);

                    

            //        if (string.IsNullOrEmpty(usrCanclQty.Text))
            //        {
            //            usrCanclQty.Text = (0.00m).ToString();
            //        }
                    
            //        if (CurtLoc.Text == "GIT")
            //        {
            //            DisplayMessage("Cannot cancel Items in GIT", 2);
            //            usrCanclQty.Text = (0.00m).ToString();
            //            usrCanclQty.ReadOnly = true;
            //        }

            //        if (WIP.Text == "1")
            //        {
            //            DisplayMessage("Cannot cancel Items in progress", 2);
            //            usrCanclQty.Text = (0.00m).ToString();
            //            usrCanclQty.ReadOnly = true;
            //        }

            //        decimal result;
            //        if (decimal.TryParse(usrCanclQty.Text, out result))
            //        {
            //            if ((result - Math.Round(result)) != 0)
            //            {
            //                if (item.Mi_is_ser1 != -1)
            //                {
            //                    DisplayMessage("Item does not allow decimal qtys", 2);
            //                    usrCanclQty.Text = (0.00m).ToString();
            //                    return;
            //                }
            //            }
            //        }

                    
            //        else
            //        {
            //            if (Convert.ToInt32(resBQty.Text) < Convert.ToInt32(Convert.ToDouble(usrCanclQty.Text)))
            //            {
            //                DisplayMessage("Cannot cancel Items more than balance qty", 2);
            //                usrCanclQty.Text = (0.00m).ToString();
            //            }
            //            else
            //            {
            //                decimal cancelQty = Convert.ToDecimal(usrCanclQty.Text);
            //                usrCanclQty.Text = cancelQty.ToString("N2");
            //            }
            //        }
            //    }
            //}
        }
        protected void txtReservationNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReservationNo);
                DataTable result = CHNLSVC.CommonSearch.Search_INT_RES(SearchParams, "RESERVATION NO", txtreservati.Text);
                if (result.Rows.Count == 0)
                {
                    DisplayMessage("Please select valid reservation number", 4);
                }    
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void lbtReservationNo_Click(object sender, EventArgs e)
        {
            try
            {


                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReservationNo);
                DataTable result = CHNLSVC.CommonSearch.Search_INT_RES(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "422";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }

        protected void lbtnCancelAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnCancel.Value == "No")
                {
                    return;
                }
                if (!string.IsNullOrEmpty(txtReservNo.Text))
                {
                    bool _balAva = false;
                    bool _isWp = false;
                    List<INR_RES_LOG>  _resLogData = CHNLSVC.Inventory.GET_INR_RES_LOG_DATA_NEW(
                                    new INR_RES_LOG() { IRL_RES_NO = txtReservNo.Text.Trim(), IRL_ACT = 1 });
                    if (_resLogData != null)
                    {
                        _resLogData = _resLogData.Where(c => c.IRL_RES_BQTY > 0).ToList();
                        if (_resLogData != null)
                        {
                            if (_resLogData.Count > 0)
                            {
                                _balAva = true;
                                var _isWpList = _resLogData.Where(c => c.IRL_RES_WP == 1).ToList();
                                if (_isWpList != null)
                                {
                                    if (_isWpList.Count > 0)
                                    {
                                        _isWp = true;
                                    }
                                }
                            }
                        }
                    }
                    if (!_balAva)
                    {
                        DispMsg("Balance not available for cancel !"); return;
                    }
                    else if (_isWp)
                    {
                        DispMsg("Reservation balance are used !"); return;
                    }
                    else
                    {
                        foreach (var item in _resLogData)
                        {
                            item.Temp_IRL_MOD_BY = Session["UserID"].ToString();
                            item.IRL_CAN_QTY =  item.IRL_RES_BQTY;
                            item.IRL_MOD_DT = DateTime.Now; 
                        }
                        string _err = "";
                        Int32 row_aff = CHNLSVC.Sales.CancelReservationDocument(_resLogData, out _err);
                        if (row_aff == 1)
                        {
                            DispMsg("Successfully Canceled", "S");
                            #region genaret mail 13 Nov 2017 by Nuwan
                            try
                            {
                                CHNLSVC.MsgPortal.SendMailReservationCancel(_resLogData, "CANCEL");
                            }
                            catch (Exception ex)
                            {
                                string _msg = "Successfully Canceled" + " and Email send fail.";
                                DisplayMessage(_msg, 2);
                            }
                            #endregion
                            PageClear();
                            return;
                        }
                        else
                        {
                            DispMsg(_err, "E");
                            return;
                        }
                    }
                }
                else
                {
                    DispMsg("Please select the reservatiuon number !");
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message,"E");
            }
        }
    }
}