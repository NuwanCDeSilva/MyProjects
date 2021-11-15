using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.InventoryNew;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Sales
{
    public partial class ReservationApproval : Base
    {
        bool _serPopAppAll
        {
            get { if (Session["_serPopAppAll"] != null) { return (bool)Session["_serPopAppAll"]; } else { return false; } }
            set { Session["_serPopAppAll"] = value; }
        }
        InventoryRequest _invReq
        {
            get { if (Session["_invReqResApp"] != null) { return (InventoryRequest)Session["_invReqResApp"]; } else { return new InventoryRequest(); } }
            set { Session["_invReqResApp"] = value; }
        }
        bool _serPopShow
        {
            get { if (Session["_serPopShowResApp"] != null) { return (bool)Session["_serPopShowResApp"]; } else { return false; } }
            set { Session["_serPopShowResApp"] = value; }
        }
        DataTable _serData
        {
            get { if (Session["_serData"] != null) { return (DataTable)Session["_serData"]; } else { return new DataTable(); } }
            set { Session["_serData"] = value; }
        }
        string _serType
        {
            get { if (Session["_serType"] != null) { return (string)Session["_serType"]; } else { return ""; } }
            set { Session["_serType"] = value; }
        }
        string _toolTip
        {
            get { if (Session["_toolTip"] != null) { return (string)Session["_toolTip"]; } else { return ""; } }
            set { Session["_toolTip"] = value; }
        }
        bool _ava
        {
            get { if (Session["_ava"] != null) { return (bool)Session["_ava"]; } else { return false; } }
            set { Session["_ava"] = value; }
        }
        string _para = "";
        protected List<InventoryRequestItem> _reqItmList {
            set { Session["_reqItmListResApp"] = value; }
            get
            {
                if (Session["_reqItmListResApp"]!=null)
                {
                    return (List<InventoryRequestItem>)Session["_reqItmListResApp"];
                }
                else
                {
                    return new List<InventoryRequestItem>();
                }
            }
        }
        protected bool Is_select { get { return (bool)Session["Is_select"]; } set { Session["Is_select"] = value; } }
        protected string LocstatusDES { get { return (string)Session["LocstatusDES"]; } set { Session["LocstatusDES"] = value; } }
        protected List<MasterItemStatus> oMasterItemStatuss { get { return (List<MasterItemStatus>)Session["oMasterItemStatuss"]; } set { Session["oMasterItemStatuss"] = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ValidateTrue();
                if (!IsPostBack)
                {
                    PageClear();
                    PopulateWarehouseType();
                }
                else
                {
                    if (_serPopShow)
                    {
                        PopupSearch.Show();
                    }
                    else
                    {
                        PopupSearch.Hide();
                    }
                    if (_serPopAppAll)
                    {
                        popAppAllRes.Show();
                    }
                    else
                    {
                        popAppAllRes.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                // lblWarning.Text = "Error Occurred while processing...  " + ex;
                //divWarning.Visible = true;

            }
        }


        protected void txtReservationNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(txtReservationNo.Text))
                //{
                //    txtReservationNo.Text = string.Empty;
                //    lblWarning.Text = "Please Enter Reservation No.";
                //    divWarning.Visible = true;
                //    return;
                //}
                //if (string.IsNullOrEmpty(txtSalesExcecutive.Text))
                //{
                //    txtReservationNo.Text = string.Empty;
                //    lblWarning.Text = "Invalid Sales Excecutive.";
                //    divWarning.Visible = true;
                //    return;
                //}
                //DataTable dt = CHNLSVC.Sales.Check_INT_REQ(txtReservationNo.Text, "SOREQ", txtSalesExcecutive.Text);

                //if (dt.Rows.Count != 1)
                //{
                //    txtReservationNo.Text = string.Empty;
                //    lblWarning.Text = "Invalid Reservation No.";
                //    divWarning.Visible = true;
                //    return;
                //}
                //else
                //{
                //    txtReservationNo.Text = dt.Rows[0]["ITR_REQ_NO"].ToString();
                //    PopulateReservationData();
                //}
            }
            catch (Exception ex)
            {
                // lblWarning.Text = "Error Occurred while processing...  " + ex;
                // divWarning.Visible = true;
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void lbtReservationNo_Click(object sender, EventArgs e)
        {
            try
            {
                if (Regex.IsMatch(txtFDate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$"))
                {

                }
                else
                {
                    txtFDate.Text = DateTime.Now.Date.AddMonths(-1).ToString("dd/MMM/yyyy");

                }
                if (Regex.IsMatch(txtTDate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$"))
                {

                }
                else
                {
                    txtTDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");

                }
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReservationNo);
                DataTable result = CHNLSVC.CommonSearch.SearchResvationApproveNo(SearchParams, null, null, Convert.ToDateTime(DateTime.Now).Date.AddMonths(-1), Convert.ToDateTime(DateTime.Now).Date);
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "422";
                BindUCtrlDDLData2(result);
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();



                //ViewState["SEARCH"] = null;
                //txtSearchbyword.Text = string.Empty;
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReservationNo);
                //DataTable result = CHNLSVC.CommonSearch.Search_INT_RES(SearchParams, null, null);
                //grdResult.DataSource = result;
                //grdResult.DataBind();
                //lblvalue.Text = "422";
                //BindUCtrlDDLData(result);
                //ViewState["SEARCH"] = result;
                //UserPopup.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        public void BindUCtrlDDLData2(DataTable _dataSource)
        {
            this.ddlSearchbykeyD.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykeyD.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykeyD.SelectedIndex = 0;
        }
        protected void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCustomer.Text))
                {
                    txtCustomer.Text = string.Empty;
                    lblCustomerName.Text = string.Empty;
                    return;
                }
                DataTable dt = CHNLSVC.Financial.CheckBusentity(Session["UserCompanyCode"].ToString(), "C", null, txtCustomer.Text);

                if (dt.Rows.Count != 1)
                {
                    txtCustomer.Text = string.Empty;
                    lblCustomerName.Text = string.Empty;
                    // lblWarning.Text = "Invalid invoice customer.";
                    // divWarning.Visible = true;
                    DisplayMessage("Invalid customer.", 2);
                    return;
                }
                else
                {
                    txtCustomer.Text = dt.Rows[0]["CODE"].ToString();
                    lblCustomerName.Text = dt.Rows[0]["NAME"].ToString();
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
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "13";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void lbtnChannel_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable result = CHNLSVC.Inventory.GetChannelDetail(Session["UserCompanyCode"].ToString(), null);
                DataView dv = new DataView(result);
                dv.RowFilter = "msc_act=1";
                result = dv.ToTable();
                result.Columns.RemoveAt(0);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);

                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns["msc_cd"].ColumnName = "Code";
                result.Columns["msc_desc"].ColumnName = "Description";
                grdResult.DataSource = result;
                grdResult.DataBind();
                BindUCtrlDDLData(result);
                ddlSearchbykey.Items.FindByText("Description").Enabled = false;
                lblvalue.Text = "channel";

                ViewState["SEARCH"] = result;

                UserPopup.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void txtChannel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtChannel.Text))
                {
                    txtChannel.Text = string.Empty;
                    return;
                }
                DataTable dt = CHNLSVC.Inventory.GetChannelDetail(Session["UserCompanyCode"].ToString(), txtChannel.Text);

                if (dt.Rows.Count != 1)
                {
                    txtChannel.Text = string.Empty;
                    //lblWarning.Text = "Invalid Channel.";
                    // divWarning.Visible = true;
                    DisplayMessage("Invalid Channel.", 2);
                    return;
                }
                //else
                //{
                //    txtChannel.Text = dt.Rows[0]["CODE"].ToString();
                //}
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void lbtnProfitCenter_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable result = CHNLSVC.CommonSearch.GetPC_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "75";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void txtProfitCenter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtProfitCenter.Text))
                {
                    txtProfitCenter.Text = string.Empty;
                    return;
                }

                //DataTable dt = CHNLSVC.Inventory.GetChannelDetail(Session["UserCompanyCode"].ToString(), txtChannel.Text);

                if (!CHNLSVC.General.CheckProfitCenter(Session["UserCompanyCode"].ToString(), txtProfitCenter.Text.Trim().ToUpper()))
                {
                    txtChannel.Text = string.Empty;
                    //lblWarning.Text = "Invalid ProfitCenter.";
                    //divWarning.Visible = true;
                    DisplayMessage("Invalid ProfitCenter.", 2);
                    return;
                }
                //else
                //{
                //    txtChannel.Text = dt.Rows[0]["CODE"].ToString();
                //}
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
                DateTime fdate;
                DateTime tdate;
                if (!DateTime.TryParse(txtFromDate.Text, out fdate))
                {
                    // lblWarning.Text = "Invalid FromDate";
                    // divWarning.Visible = true;
                    DisplayMessage("Invalid from date.", 2);
                    return;
                }
                if (!DateTime.TryParse(txtToDate.Text, out tdate))
                {
                    // lblWarning.Text = "Invalid ToDate";
                    //  divWarning.Visible = true;
                    DisplayMessage("Invalid to date.", 2);
                    return;
                }

                List<INT_REQ> int_req = CHNLSVC.Sales.Search_INT_REQ(Session["UserCompanyCode"].ToString(), txtCustomer.Text, txtChannel.Text, txtProfitCenter.Text, fdate, tdate);
                int_req = int_req.OrderByDescending(x=>(x.ITR_DT)).ToList();
                int_req = int_req.OrderByDescending(x => (x.ITR_REQ_NO)).ToList();
                if (grdRequest != null)
                {
                    ViewState["int_req"] = int_req;

                    grdRequest.DataSource = int_req;
                    grdRequest.DataBind();

                }
                else
                {
                    grdRequest.DataSource = new int[] { };
                    grdRequest.DataBind();
                }
                Session["BL"] = "";
                ViewState["Location"] = "";
                //txtFromDate.Text = string.Empty;
               // txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
                //txtToDate.Text = string.Empty;
                //txtToDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                lvltypevalue.Text = string.Empty;
                lblreservationtype.Text = string.Empty;
                txtReservationNo.Text = string.Empty;
                lblCustomer.Text = string.Empty;
                txtReservationDate.Text = string.Empty;
                txtReservationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");

                grdRequestItem.DataSource = new int[] { };
                grdRequestItem.DataBind();

                rbtnInStock.Checked = true;
                Panel2.Visible = true;
                rbtnPenddingOrders.Checked = false;
                Panel1.Visible = false;
                Session["inr_res_det"] = null;
                Session["itemCode"] = "";
                ddlWarehouseType.SelectedIndex = -1;
                ddlOrderType.SelectedIndex = -1;
                txtSupplier.Text = string.Empty;
                txtOrderNo.Text = string.Empty;
                txtIBFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
                txtIBToDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                grdReservationItems.DataSource = new int[] { };
                grdReservationItems.DataBind();

                grdInventoryBalance.DataSource = new int[] { };
                grdInventoryBalance.DataBind();


            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }

        protected void grdRequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grdPendingOrderBalance.DataSource = new int[] { };
                grdPendingOrderBalance.DataBind();
                List<INT_REQ> int_req = (List<INT_REQ>)ViewState["int_req"];
                string reqNo = grdRequest.SelectedRow.Cells[2].Text;
                string Type = grdRequest.SelectedRow.Cells[6].Text;
                lblreservationtype.Text = CHNLSVC.Sales.GetDescription(Type);
                lvltypevalue.Text = Type;

                ViewState["RequestNo"] = reqNo;
                Session["inr_res_det"] = null;

                Session["inr_res_log"] = null;
                IEnumerable<string> req = (from i in int_req
                                           where i.ITR_REQ_NO == reqNo
                                           select i.ITR_BUS_CODE);

                //if (grdReservationItems.Rows.Count > 0 && req.ElementAt(0) != lblCustomer.Text)
                //{
                //    //lblWarning.Text = "Please select a valid customer";
                //    //divWarning.Visible = true;

                //    return;
                //}
                grdReservationItems.DataSource = new int[] { };
                grdReservationItems.DataBind();
                grdInventoryBalance.DataSource = new int[] { };
                grdInventoryBalance.DataBind();

                lblCustomer.Text = req.ElementAt(0);

                int j = 0;
                int k = 0;
                decimal Totalallqty = 0;
                decimal BeforetotAppqty = 0;
                DataTable dt = CHNLSVC.Sales.SELECT_INT_REQ_ITMbyREQ_NO(reqNo);
                DataTable apptotData = CHNLSVC.Sales.GetTotalApproveQty(reqNo);

                foreach (DataRow dtrow in dt.Rows)
                {
                    Totalallqty = Totalallqty + Convert.ToDecimal(dt.Rows[j]["ITRI_QTY"].ToString());
                    k = 0;
                    foreach (DataRow totrow in apptotData.Rows)
                    {

                        if (dt.Rows[j]["ITRI_ITM_CD"].ToString() == apptotData.Rows[k]["IRD_ITM_CD"].ToString())
                        {
                            dt.Rows[j]["ITRI_APP_QTY"] = Convert.ToDecimal(apptotData.Rows[k]["APPQTY"].ToString());
                            BeforetotAppqty = BeforetotAppqty + Convert.ToDecimal(apptotData.Rows[k]["APPQTY"].ToString());
                        }
                        k++;
                    }
                    j++;
                }

                Session["Totalallqty"] = Totalallqty;
                Session["BeforetotAppqty"] = BeforetotAppqty;


                grdRequestItem.DataSource = new int[] { };
                grdRequestItem.DataBind();

                grdRequestItem.DataSource = dt;
                grdRequestItem.DataBind();

                ViewState["int_req_itm"] = dt;

                grdInventoryBalance.DataSource = new int[] { };
                grdInventoryBalance.DataBind();
                ddlWarehouseType.SelectedIndex = -1;
                ddlOrderType.SelectedIndex = -1;
                txtSupplier.Text = string.Empty;
                txtOrderNo.Text = string.Empty;
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["ITR_EXP_DT"] != DBNull.Value)
                    {
                        txtExpectedDate.Text = Convert.ToDateTime(dt.Rows[0]["ITR_EXP_DT"].ToString()).ToString("dd/MMM/yyyy");
                    }
                }
                txtIBFromDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                txtIBToDate.Text = DateTime.Now.AddMonths(3).ToString("dd/MMM/yyyy");
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void chkRequestItem_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)sender;
                GridViewRow grdr = (GridViewRow)chk.NamingContainer;
                int rowindex = grdr.RowIndex;
                if (chk.Checked == true)
                {
                    if (chk.Checked == true)
                    {
                        string itemCode = grdRequestItem.Rows[grdr.RowIndex].Cells[2].Text;
                        Session["itemCode"] = itemCode;
                        string code = ddlWarehouseType.SelectedValue;
                        if (code == "0")
                            code = null;
                        DataTable dt = CHNLSVC.Sales.Select_InventoryBalance(code, itemCode, Session["UserCompanyCode"].ToString());
                        if (dt.Rows.Count > 0)
                        {
                            DataView dv = dt.DefaultView;
                            dv.Sort = "ML_LOC_DESC ASC";
                            dt = dv.ToTable();
                        }
                        grdInventoryBalance.DataSource = dt;
                        grdInventoryBalance.DataBind();
                        Is_select = false;
                    }
                    if (rbtnPenddingOrders.Checked == true)
                    {

                    }

                    if (ViewState["rowindex"] != null)
                    {
                        ((CheckBox)grdRequestItem.Rows[Convert.ToInt32(ViewState["rowindex"])].FindControl("chkRequestItem")).Checked = false;
                    }
                    ViewState["rowindex"] = rowindex;
                }
                else
                {
                    grdInventoryBalance.DataSource = new int[] { };
                    grdInventoryBalance.DataBind();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void rbtnInStock_CheckedChanged(object sender, EventArgs e)
        {
            rbtnPenddingOrders.Checked = false;
            Panel2.Visible = true;
            Panel1.Visible = false;
            grdInventoryBalance.Visible = true;
            grdPendingOrderBalance.Visible = false;
            grdInventoryBalance.DataSource = new int[] { };
            grdInventoryBalance.DataBind();
        }
        protected void rbtnPenddingOrders_CheckedChanged(object sender, EventArgs e)
        {
            txtCustomer.Text = string.Empty;
            txtChannel.Text = string.Empty;
            txtProfitCenter.Text = string.Empty;
            txtFromDate.Text = (DateTime.Now).AddMonths(-1).ToString("dd/MMM/yyyy");
            txtToDate.Text = (DateTime.Now).ToString("dd/MMM/yyyy");
            grdInventoryBalance.Visible = false;
            grdPendingOrderBalance.Visible = true;
            grdPendingOrderBalance.DataSource = new int[] { };
            grdPendingOrderBalance.DataBind();

            rbtnInStock.Checked = false;
            Panel2.Visible = false;
            Panel1.Visible = true;
        }
        protected void lbtnGrdSerial_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;

                DataTable dt = (DataTable)ViewState["int_req_itm"];
                ViewState["Item"] = dt.Rows[grdr.RowIndex]["ITRI_ITM_CD"].ToString();
                int seqNo = Convert.ToInt32((dt.Rows[grdr.RowIndex]["ITRI_SEQ_NO"].ToString()));

                //ViewState["Location"] = item[grdr.RowIndex].ITRI_LOC;
                ViewState["RowIndex"] = grdr.RowIndex;

                List<INT_REQ_SER> int_req_ser = new List<INT_REQ_SER>();
                int_req_ser = CHNLSVC.Sales.GetINT_REQ_SER(seqNo);
                ViewState["int_req_ser"] = int_req_ser;

                //ViewState["SEARCH"] = null;
                //txtSearchbyword.Text = string.Empty;
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                //DataTable result = CHNLSVC.CommonSearch.Search_INR_SER(SearchParams, null, null);
                //grdResult1.DataSource = result;
                //grdResult1.DataBind();
                //lblvalue.Text = "50";
                //BindUCtrlDDLData1(result);
                //ViewState["SEARCH"] = result;


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
        protected void lbtnInStock_Click(object sender, EventArgs e)
        {
            try
            {
                string itemCode = Session["itemCode"].ToString();
                if (itemCode == "")
                {
                    // lblWarning.Text = "Please select item";
                    // divWarning.Visible = true;
                    DisplayMessage("Please select item", 2);
                    return;
                }
                string code = ddlWarehouseType.SelectedValue;
                if (code == "0")
                    code = null;

                grdInventoryBalance.DataSource = CHNLSVC.Sales.Select_InventoryBalance(code, itemCode);
                grdInventoryBalance.DataBind();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void lbtnSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "16";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void txtSupplier_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSupplier.Text))
                {
                    txtSupplier.Text = string.Empty;
                    return;
                }

                string orderType = ddlOrderType.SelectedValue;
                if (orderType == "0")
                    orderType = string.Empty;
                DataTable dt = CHNLSVC.Financial.CheckBusentity(Session["UserCompanyCode"].ToString(), "S", orderType, txtSupplier.Text);

                if (dt.Rows.Count != 1)
                {
                    txtSupplier.Text = string.Empty;
                    //lblWarning.Text = "Invalid Supplier.";
                    // divWarning.Visible = true;
                    DisplayMessage("Invalid supplier", 2);
                    return;
                }
                else
                {
                    txtCustomer.Text = dt.Rows[0]["CODE"].ToString();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void lbtnOrderNo_Click(object sender, EventArgs e)
        {
            try
            {
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GRNItem);
                //DataTable _result = CHNLSVC.Inventory.GetGRNDocNo(SearchParams, Convert.ToDateTime(txtIBFromDate.Text), Convert.ToDateTime(txtIBToDate.Text), null, null);
                //_result.Columns["Order #"].SetOrdinal(0);

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl);
                DataTable _result = CHNLSVC.Inventory.GET_GetTobond_BL_RES(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "OrderNo";
                BindUCtrlDDLData(_result);
                UserPopup.Show();
                ViewState["SEARCH"] = _result;
                return;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void rbtnInventoryBalance_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //var radBtn = (RadioButton)sender;
                //var row = (GridViewRow)radBtn.NamingContainer;

                //Label lblCode = row.FindControl("lblCode") as Label;
                //Label lblINL_FREE_QTY = row.FindControl("lblINL_FREE_QTY") as Label;

                RadioButton rbtn = (RadioButton)sender;
                GridViewRow grdr = (GridViewRow)rbtn.NamingContainer;
                ViewState["Location"] = ((Label)(grdInventoryBalance.Rows[grdr.RowIndex].FindControl("lblCode"))).Text;
             
                ViewState["FreeQty"] = grdInventoryBalance.Rows[grdr.RowIndex].Cells[5].Text;
                LocstatusDES = ((Label)(grdInventoryBalance.Rows[grdr.RowIndex].FindControl("lblStatus"))).Text;
                //  DataTable dt =(DataTable) grdInventoryBalance.DataSource;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        private void loadItemStatus()
        {
            oMasterItemStatuss = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
        }
        protected void rbtnOrderBalance_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RadioButton rbtn = (RadioButton)sender;
                GridViewRow grdr = (GridViewRow)rbtn.NamingContainer;
                Session["BL"] = ((Label)(grdPendingOrderBalance.Rows[grdr.RowIndex].FindControl("col_ib_bl_no"))).Text;
                string qty = ((Label)(grdPendingOrderBalance.Rows[grdr.RowIndex].FindControl("col_ibi_qty"))).Text;
                string reqqty = ((Label)(grdPendingOrderBalance.Rows[grdr.RowIndex].FindControl("col_ibi_req_qty"))).Text;
                string resqty = ((Label)(grdPendingOrderBalance.Rows[grdr.RowIndex].FindControl("col_ibi_req_res_qty"))).Text;
               
                decimal Qtyreq = (reqqty == "") ? 0 : Convert.ToDecimal(reqqty);
                decimal Qtyres = (resqty == "") ? 0 : Convert.ToDecimal(resqty);
                decimal freeqty = Convert.ToDecimal(qty.ToString()) - Qtyres;
                ViewState["FreeQty"] = freeqty.ToString();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void lbtnPenddingOrders_Click(object sender, EventArgs e)
        {
            try
            {
                string itemCode = Session["itemCode"].ToString();
                if (itemCode == "")
                {
                    // lblWarning.Text = "Please select item";
                    // divWarning.Visible = true;
                    DisplayMessage("Please select item", 2);
                    return;
                }
                string code = ddlWarehouseType.SelectedValue;
                if (code == "0")
                    code = null;
                if (txtSupplier.Text != "")
                {
                    DataTable POTbl = CHNLSVC.Sales.GETRES_PO_DETAILS(Session["UserCompanyCode"].ToString(), Session["UserSBU"].ToString(), txtSupplier.Text, "", Convert.ToDateTime(txtIBFromDate.Text), Convert.ToDateTime(txtIBToDate.Text), itemCode);
                    grdPendingOrderBalance.DataSource = POTbl;
                    grdPendingOrderBalance.DataBind();
                }
                else if (txtOrderNo.Text != "")
                {
                    DataTable POTbl = CHNLSVC.Sales.GETRES_PO_DETAILS(Session["UserCompanyCode"].ToString(), Session["UserSBU"].ToString(), "", txtOrderNo.Text, Convert.ToDateTime(txtIBFromDate.Text), Convert.ToDateTime(txtIBToDate.Text), itemCode);
                    grdPendingOrderBalance.DataSource = POTbl;
                    grdPendingOrderBalance.DataBind();
                }
                else
                {
                    DataTable POTbl = CHNLSVC.Sales.GETRES_PO_DETAILS(Session["UserCompanyCode"].ToString(), Session["UserSBU"].ToString(), "", "", Convert.ToDateTime(txtIBFromDate.Text), Convert.ToDateTime(txtIBToDate.Text), itemCode);
                    grdPendingOrderBalance.DataSource = POTbl;
                    grdPendingOrderBalance.DataBind();
                }


            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void lbtnRequestItemAdd_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)lbtn.NamingContainer;
                string itemCode = grdRequestItem.Rows[grdr.RowIndex].Cells[2].Text;
                string appqty = grdRequestItem.Rows[grdr.RowIndex].Cells[9].Text;
                string totqty = grdRequestItem.Rows[grdr.RowIndex].Cells[8].Text;
                string _itmStus = grdRequestItem.Rows[grdr.RowIndex].Cells[7].Text;
               // string loc = grdRequestItem.Rows[grdr.RowIndex].Cells[10].Text;
                Label lblitri_loc = grdr.FindControl("lblitri_loc") as Label;
                Label lblitri_com = grdr.FindControl("lblitri_com") as Label;
                decimal qty;
                decimal freeQty;
                string _locD = lblitri_loc.Text;
                string _comD = lblitri_com.Text;

         
                if (string.IsNullOrEmpty(((TextBox)grdRequestItem.Rows[grdr.RowIndex].FindControl("txtReqQty")).Text))
                {
                    //lblWarning.Text = "Please enter approve quantity.";
                    // divWarning.Visible = true;
                    DisplayMessage("Please enter approve quantity", 2);
                    return;
                }

                if (!decimal.TryParse(((TextBox)grdRequestItem.Rows[grdr.RowIndex].FindControl("txtReqQty")).Text, out qty))
                {
                    //lblWarning.Text = "Invalid qty.";
                    // divWarning.Visible = true;
                    DisplayMessage("Invalid qty", 2);
                    return;
                }

                if ((ViewState["Location"] == null) && (Session["BL"] == null))
                {
                    // lblWarning.Text = "Please select location.";
                    // divWarning.Visible = true;
                    DisplayMessage("Please select location or BL", 2);
                    return;
                }

                if (ViewState["FreeQty"] == null)
                {
                    //lblWarning.Text = "Invalid Free Qty.";
                    //divWarning.Visible = true;
                    DisplayMessage("Please select a free location", 2);
                    return;
                }

                if (!decimal.TryParse(ViewState["FreeQty"].ToString(), out freeQty))
                {
                    // lblWarning.Text = "Invalid Free Qty.";
                    //divWarning.Visible = true;
                    DisplayMessage("Invalid free qty", 2);
                    return;
                }
                string status = string.Empty;
                if (LocstatusDES == "" && rbtnPenddingOrders.Checked != true)
                {
                    DisplayMessage("Please selcet location", 2);
                    return;
                    //LocstatusDES = ((Label)grdRequestItem.Rows[grdr.RowIndex].FindControl("lblCode")).Text;
                }
                else
                {
                    if (oMasterItemStatuss != null)
                        {
                            if (oMasterItemStatuss.Count > 0)
                            {
                                if (rbtnPenddingOrders.Checked)
                                {
                                    MasterItemStatus oStatus = oMasterItemStatuss.Find(x => x.Mis_desc == _itmStus);
                                    if (oStatus != null)
                                    {
                                        status = oStatus.Mis_cd;
                                    }
                                }
                                else
                                {
                                    MasterItemStatus oStatus = oMasterItemStatuss.Find(x => x.Mis_desc == LocstatusDES);
                                    if (oStatus != null)
                                    {
                                        status = oStatus.Mis_cd;
                                    }
                                }
                            }
                        }
                    
                }
                string _location = ViewState["Location"].ToString();
                MasterLocation _MasterLocation = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), _location);
                if (Is_select == false)
                {
                   
                    if (_MasterLocation.Ml_cate_1 == "DFS")
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl_2);
                        DataTable _result = CHNLSVC.Inventory.GET_GetTobond_BL_RES(SearchParams, null, null);
                        if (_result.Rows.Count > 0)
                        {
                            _result.Columns.Remove("ib_doc_rec_dt");
                            grdResult.DataSource = _result;
                            grdResult.DataBind();
                            lblvalue.Text = "Blsearch";
                            BindUCtrlDDLData(_result);
                            UserPopup.Show();
                            ViewState["SEARCH"] = _result;
                            TextBox TEXT = (TextBox)grdRequestItem.Rows[grdr.RowIndex].FindControl("txtReqQty");
                            Session["appqty"] = TEXT.Text;
                            return;
                        }

                    }
                }

                decimal qtynew = Convert.ToDecimal(qty);
                decimal totappqty = Convert.ToDecimal(appqty);

                if ((totappqty + qtynew) > Convert.ToDecimal(totqty))
                {
                    // lblWarning.Text = "Invalid Free Qty.";
                    //divWarning.Visible = true;
                    DisplayMessage("Approve qty can not exceed Total qty ", 2);
                    return;
                }



                if (freeQty < qty)
                {
                    //lblWarning.Text = "free quantity  must be less than or equal to request quantity.";
                    //divWarning.Visible = true;
                    DisplayMessage(" Approve quantity must be less than or equal to free quantity", 2);
                    return;
                }
                if (qty == 0)
                {
                    //lblWarning.Text = "free quantity  must be less than or equal to request quantity.";
                    //divWarning.Visible = true;
                    DisplayMessage("You cannot add zero quantity", 2);
                    return;
                }
                List<INR_RES_DET> inr_res_detList = new List<INR_RES_DET>();

                if (Session["inr_res_det"] != null)
                {
                    inr_res_detList = (List<INR_RES_DET>)Session["inr_res_det"];
                }

                int seqno = 0;
                int lineNo = 1;
                if (inr_res_detList.Count > 0)
                {
                    if (inr_res_detList.Exists(x => x.IRD_ITM_CD == itemCode))
                    {
                        //lblWarning.Text = "Duplicate Items Are Not Allowed.";
                        //divWarning.Visible = true;
                        DisplayMessage("Duplicate items are not allowed", 2);
                        return;
                    }

                    seqno = inr_res_detList[0].IRD_SEQ;

                    lineNo = inr_res_detList.Max(x => x.IRD_LINE) + 1;
                }


                INR_RES_DET inr_res_det = new INR_RES_DET();
                #region  Check BL Balance and break Item Qty and set job_line by subo
                if (_MasterLocation.Ml_cate_1 == "DFS")
                {
                    List<ImportsBLItems> _blitems = new List<ImportsBLItems>();

                    _blitems = CHNLSVC.Inventory.GET_GetTobond_BL_Itm("", Session["BL"].ToString(), itemCode);
                    if (_blitems == null)
                    {
                        _blitems = CHNLSVC.Inventory.GET_GetTobond_BL_Itm(Session["BL"].ToString(), "", itemCode);
                    }
                    List<ImportsBLItems> _blitems2 = _blitems.Where(a => (a.Ibi_qty - a.Ibi_req_qty) >= qty).ToList();

                    if (_blitems2 != null)
                    {
                        if (_blitems2.Count > 0)
                        {
                            inr_res_det.IRD_SI_LINE = _blitems2.First().Ibi_line;
                            inr_res_det.IRD_SI_NO = _blitems2.First().Ibi_doc_no;
                        }
                        else
                        {
                            var _sum = _blitems.Sum(a => (a.Ibi_qty - a.Ibi_req_qty));
                            if (_sum >= qty)
                            {
                                _blitems = _blitems.OrderByDescending(a => (a.Ibi_qty - a.Ibi_req_qty)).ToList();
                                DisplayMessage("Please Try Save Qty: " + (_blitems.First().Ibi_qty - _blitems.First().Ibi_req_qty) + " First", 2);
                                return;
                            }
                            else
                            {
                                DisplayMessage("cannot exceed bl qty ", 2);
                                return;
                            }
                        }
                    }
                    else
                    {
                        var _sum = _blitems.Sum(a => (a.Ibi_qty - a.Ibi_req_qty));
                        if (_sum >= qty)
                        {
                            _blitems = _blitems.OrderByDescending(a => (a.Ibi_qty - a.Ibi_req_qty)).ToList();
                            DisplayMessage("Please Try Save Qty: " + (_blitems.First().Ibi_qty - _blitems.First().Ibi_req_qty) + " First", 2);
                            return;
                        }
                        else
                        {
                            DisplayMessage("cannot exceed bl qty ", 2);
                            return;
                        }
                    }
                }
                #endregion



               
                inr_res_det.IRD_SEQ = 0;
                inr_res_det.IRD_RES_NO = "";
                inr_res_det.IRD_LINE = lineNo;
                inr_res_det.IRD_ITM_CD = itemCode;
                inr_res_det.IRD_ITM_STUS = status;
                inr_res_det.IRD_RES_QTY = qty;
                inr_res_det.IRD_RES_BQTY = qty;
                inr_res_det.IRD_RES_CQTY = 0;
                inr_res_det.IRD_REQ_BQTY = 0;
                inr_res_det.IRD_REQ_CQTY = 0;
                inr_res_det.IRD_ACT = 1;
                inr_res_det.IRD_RESREQ_NO = ViewState["RequestNo"].ToString();
                inr_res_det.IRD_RESREQ_LINE = Convert.ToInt32(((Label)grdRequestItem.Rows[grdr.RowIndex].FindControl("lblLineNo")).Text);
                inr_res_det.MI_MODEL = grdRequestItem.Rows[grdr.RowIndex].Cells[5].Text;
                inr_res_det.MIS_DESC = LocstatusDES;//grdRequestItem.Rows[grdr.RowIndex].Cells[7].Text;

                if (Session["BL"].ToString() != "")
                {
                    inr_res_det.BL_NO = Session["BL"].ToString();
                }
                else
                {
                    if (_MasterLocation.Ml_cate_1 == "DFS")
                    {
                        DisplayMessage("Please select a BL Number !!!", 2); return;
                    }
                }

                //Check grn status validation - req by lakshan
                if (CHNLSVC.Financial.isstatusvalidation(Session["UserCompanyCode"].ToString(), inr_res_det.IRD_ITM_CD, inr_res_det.IRD_ITM_STUS, Session["BL"].ToString(), inr_res_det.IRD_RES_QTY) == false)
                {
                    DisplayMessage("No Free Qty  " + inr_res_det.IRD_ITM_STUS + " Status", 2);
                    return;
                }

                if (rbtnInStock.Checked)
                {
                    bool _select = false;


                    string _loc = "";
                    foreach (GridViewRow rw in grdInventoryBalance.Rows)
                    {
                        RadioButton rbtnInventoryBalance = rw.FindControl("rbtnInventoryBalance") as RadioButton;
                        Label lblCode = rw.FindControl("lblCode") as Label;
                        if (rbtnInventoryBalance.Checked)
                        {
                            _select = true;
                            _loc = lblCode.Text.Trim();
                            break;
                        }
                    }
                    if (!_select)
                    {
                        DisplayMessage("Please select a location !!!", 2); return;
                    }
                    inr_res_det.LOC_CD = _loc;
                }
                if (rbtnPenddingOrders.Checked)
                {
                    bool _select = false;
                    Int32 _line = 0;
                    foreach (GridViewRow rw in grdPendingOrderBalance.Rows)
                    {
                        RadioButton rbtnOrderBalance = rw.FindControl("rbtnOrderBalance") as RadioButton;
                        Label lblibi_line = rw.FindControl("lblibi_line") as Label;
                        if (rbtnOrderBalance.Checked)
                        {
                            _select = true;
                            _line = Convert.ToInt32(lblibi_line.Text.Trim());
                            break;
                        }
                    }
                    if (!_select)
                    {
                        DisplayMessage("Please select a bl # !!!", 2); return;
                    }
                    inr_res_det.IRD_RESREQ_LINE = _line;
                }
                inr_res_detList.Add(inr_res_det);

                grdReservationItems.DataSource = inr_res_detList;
                grdReservationItems.DataBind();

                Session["inr_res_det"] = inr_res_detList;

                List<INR_RES_LOG> inr_res_logList = new List<INR_RES_LOG>();

                if (Session["inr_res_log"] != null)
                {
                    inr_res_logList = (List<INR_RES_LOG>)Session["inr_res_log"];
                }

                string doc_pt = "";
                if (rbtnInStock.Checked == true)
                    doc_pt = "INV";
                if (rbtnPenddingOrders.Checked == true)
                    doc_pt = "PO";

                INR_RES_LOG inr_res_log = new INR_RES_LOG();
                inr_res_log.IRL_SEQ = 0;
                inr_res_log.IRL_RES_NO = "";
                inr_res_log.IRL_RES_LINE = lineNo;
                inr_res_log.IRL_LINE = lineNo;
                inr_res_log.IRL_ITM_CD = itemCode;
                inr_res_log.IRL_ITM_STUS = status;
                inr_res_log.IRL_RES_QTY = qty;
                inr_res_log.IRL_RES_BQTY = qty;
                inr_res_log.IRL_RES_IQTY = 0;
                inr_res_log.IRL_ORIG_DOC_TP = doc_pt;
                inr_res_log.IRL_ORIG_DOC_NO = "";//TODO
                inr_res_log.IRL_ORIG_DOC_DT = DateTime.Now.Date;//TODO
                inr_res_log.IRL_ORIG_ITM_LINE = 0;//TODO
                //inr_res_log.IRL_ORIG_BATCH_LINE = 
                inr_res_log.IRL_ORIG_COM = Session["UserCompanyCode"].ToString();
                if (ViewState["Location"].ToString() != "")
                {
                    inr_res_log.IRL_ORIG_LOC = ViewState["Location"].ToString();
                }
                //inr_res_log.IRL_CURT_DOC_TP =
                //if (Session["BL"].ToString() != "")
                //{
                //    inr_res_log.IRL_CURT_DOC_NO = Session["BL"].ToString(); 
                //}  
                if (Session["BL"].ToString() != "")
                {
                    inr_res_log.BL_NO = Session["BL"].ToString();
                }
                if (rbtnInStock.Checked)
                {
                    bool _select = false;
                    string _loc = "";
                    foreach (GridViewRow rw in grdInventoryBalance.Rows)
                    {
                        RadioButton rbtnInventoryBalance = rw.FindControl("rbtnInventoryBalance") as RadioButton;
                        Label lblCode = rw.FindControl("lblCode") as Label;
                        if (rbtnInventoryBalance.Checked)
                        {
                            _select = true;
                            _loc = lblCode.Text.Trim();
                            break;
                        }
                    }
                    if (!_select)
                    {
                        DisplayMessage("Please select a location !!!", 2); return;
                    }
                    inr_res_log.LOC_CD = _loc;
                }
                //inr_res_log.IRL_CURT_DOC_DT =
                //inr_res_log.IRL_CURT_ITM_LINE =
                //inr_res_log.IRL_CURT_BATCH_LINE =
                inr_res_log.IRL_CURT_COM =_comD;
                inr_res_log.IRL_CURT_LOC = _locD;
                //inr_res_log.IRL_BASE_LINE = 
                inr_res_log.IRL_ACT = 1;
                inr_res_log.IRL_CRE_BY = Session["UserID"].ToString();
                inr_res_log.IRL_CRE_DT = DateTime.Now;
                inr_res_log.IRL_CRE_SESSION = Session["SessionID"].ToString();
                //inr_res_log.IRL_MOD_BY = ;
                //inr_res_log.IRL_MOD_DT = ;
                //inr_res_log.IRL_MOD_SESSION = ;  

                inr_res_logList.Add(inr_res_log);
                Session["inr_res_log"] = inr_res_logList;
                TextBox TEXTqty = (TextBox)grdRequestItem.Rows[grdr.RowIndex].FindControl("txtReqQty");
                TEXTqty.Text = string.Empty;
                Session["BL"] = "";
                // grdPendingOrderBalance.Enabled = false;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void lbtnGrdItemDalete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDeleteconformmessageValue.Value == "No")
                {
                    return;
                }
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                List<INR_RES_DET> item = (List<INR_RES_DET>)Session["inr_res_det"];
                List<INR_RES_LOG> item_lg = (List<INR_RES_LOG>)Session["inr_res_log"];
                item.RemoveAt(grdr.RowIndex);
                item_lg.RemoveAt(grdr.RowIndex);
                Session["inr_res_det"] = item;
                Session["inr_res_log"] = item_lg;
                grdReservationItems.DataSource = item;
                grdReservationItems.DataBind();

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
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
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnSave.Value == "Yes")
                {
                    DateTime date;
                    DateTime exDate;
                    if (!DateTime.TryParse(txtReservationDate.Text, out date))
                    {
                        // lblWarning.Text = "Please select reservation date.";
                        // divWarning.Visible = true;
                        DisplayMessage("Please select reservation date", 2);
                        return;
                    }

                    if (string.IsNullOrEmpty(lblCustomer.Text))
                    {
                        // lblWarning.Text = "Please select a valid customer…! ";
                        //divWarning.Visible = true;
                        //DisplayMessage("Please select a valid customer", 2);
                        //return;
                    }
                    if (string.IsNullOrEmpty(lvltypevalue.Text))
                    {
                        //lblWarning.Text = "Please select the reservation type";
                        //divWarning.Visible = true;
                        DisplayMessage("Please select the reservation type", 2);
                        return;
                    }
                    if (grdRequestItem.Rows.Count == 0)
                    {
                        //lblWarning.Text = "Please enter Items.";
                        //divWarning.Visible = true;
                        DisplayMessage("Please enter Items", 2);
                        return;
                    }

                    MasterAutoNumber masterAuto = new MasterAutoNumber();
                    masterAuto.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                    masterAuto.Aut_cate_tp = Session["UserCompanyCode"].ToString();  //Session["UserDefLoca"].ToString(); Edit by Chamal 24-Aug-2016
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "RES";
                    masterAuto.Aut_number = 5;
                    masterAuto.Aut_start_char = "RES";
                    masterAuto.Aut_year = Convert.ToInt32(DateTime.Now.ToString("yy")); ;

                    INR_RES inr_res = new INR_RES();

                    inr_res.IRS_SEQ = 0;
                    inr_res.IRS_COM = Session["UserCompanyCode"].ToString();
                    MasterLocation _mstLoc = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    if (_mstLoc != null)
                    {
                        inr_res.IRS_CHNL = _mstLoc.Ml_cate_2;
                    }
                    if (string.IsNullOrEmpty(inr_res.IRS_CHNL))
                    {
                        DisplayMessage("Invalid channel", 2);
                        return;
                    }
                    //inr_res.IRS_CHNL = Session["GlbDefChannel"].ToString();//BaseCls.GlbDefChannel;
                    inr_res.IRS_RES_NO = "";
                    inr_res.IRS_RES_DT = Convert.ToDateTime(txtReservationDate.Text).Date;
                    inr_res.IRS_RES_TP = lvltypevalue.Text;//ddlReservationType.SelectedItem.Value;     
                    inr_res.IRS_CUST_TP = "C";
                    inr_res.IRS_CUST_CD = string.IsNullOrEmpty(lblCustomer.Text) ? "N/A" : lblCustomer.Text;
                    inr_res.IRS_STUS = "A";
                    inr_res.IRS_RMK = "";
                    inr_res.IRS_EXPTED_DT = Convert.ToDateTime(txtExpectedDate.Text);
                    string BL = Session["BL"].ToString();
                    if (BL != "")
                    {
                        inr_res.IRS_ANAL_1 = Session["BL"].ToString();
                    }

                    //inr_res.IRS_ANAL_2      =        
                    //inr_res.IRS_ANAL_3      =        
                    //inr_res.IRS_ANAL_4      =        
                    //inr_res.IRS_ANAL_5      =        
                    inr_res.IRS_CRE_BY = Session["UserID"].ToString();
                    inr_res.IRS_CRE_DT = DateTime.Now;
                    inr_res.IRS_CRE_SESSION = Session["SessionID"].ToString();
                    //inr_res.IRS_CNL_BY      =        
                    //inr_res.IRS_CNL_DT      =        
                    //inr_res.IRS_CNL_SESSION =        
                    //inr_res.IRS_MOD_BY      =        
                    //inr_res.IRS_MOD_DT      =        
                    //inr_res.IRS_MOD_SESSION =        

                    List<INR_RES_DET> inr_res_det = (List<INR_RES_DET>)Session["inr_res_det"];

                    List<INR_RES_LOG> inr_res_log = (List<INR_RES_LOG>)Session["inr_res_log"];

                    int row_aff = 0;
                    string msg;
                    string finaldoc;
                    decimal Totalallqty = Convert.ToDecimal(Session["Totalallqty"].ToString());
                    decimal BeforetotAppqty = Convert.ToDecimal(Session["BeforetotAppqty"].ToString());
                    inr_res.checkbaseitem = true;
                    if (inr_res_log != null)
                    {
                        foreach (var reslogstatus in inr_res_log)
                        {
                            if (reslogstatus.IRL_ITM_STUS == null || reslogstatus.IRL_ITM_STUS == "")
                            {
                                DisplayMessage("Session Issue!  Please Reset Page and Add Again!!", 2);
                                return;
                            }
                        }
                    }
                    else
                    {
                        DisplayMessage("Session Issue!  Please Reset Page and Add Again!!", 2);
                        return;
                    }

                    row_aff = CHNLSVC.Sales.SaveReservationApprovalNew(inr_res, inr_res_det, inr_res_log, masterAuto, out msg, out finaldoc, Totalallqty, BeforetotAppqty,rbtnPenddingOrders.Checked,"");

                    if (row_aff >= 1)
                    {
                        // lblSuccess.Text = "! Successfully created the reservation approval number :" + msg;
                        // divSuccess.Visible = true;
                        string _msgbody = "Successfully created the reservation approval number :" + finaldoc;
                        DisplayMessage(_msgbody, 3);
                        PageClear();
                        #region genaret mail 11 Jan 2017 by lakshan
                        try
                        {
                            CHNLSVC.MsgPortal.SendMailReservationApprove(inr_res.IRS_COM, finaldoc);
                        }
                        catch (Exception ex)
                        {
                             _msgbody = "Successfully created the reservation approval number :" + finaldoc+" and Email send fail.";
                            DisplayMessage(_msgbody, 3);
                        }

                        #endregion
                        return;
                    }
                    else
                    {
                        if (msg.Contains("IRS_CHNL"))
                        {
                            DisplayMessage("Please check channel", 2);
                        }
                        if (msg.Contains("CHK_INLFREEQTY"))
                        {
                            DisplayMessage("Free Qty is not available in location :" + inr_res.IRS_ANAL_2, 1);
                        }
                        else
                        {
                            string Msg = msg.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
                            DisplayMessage(Msg, 4);
                            return;
                        }
                      
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
            }
        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnUpdate.Value == "Yes")
                {
                    //DateTime date;
                    //DateTime exDate;
                    //if (!DateTime.TryParse(txtDate.Text, out date))
                    //{
                    //    lblWarning.Text = "Please select valid Date.";
                    //    divWarning.Visible = true;
                    //    return;
                    //}
                    //if (!DateTime.TryParse(txtExpectedDate.Text, out exDate))
                    //{
                    //    lblWarning.Text = "Please select valid Expected Date.";
                    //    divWarning.Visible = true;
                    //    return;
                    //}
                    //if (string.IsNullOrEmpty(txtReservationType.Text))
                    //{
                    //    lblWarning.Text = "Please enter Reservation Type.";
                    //    divWarning.Visible = true;
                    //    return;
                    //}
                    //if (string.IsNullOrEmpty(txtRefNo.Text))
                    //{
                    //    lblWarning.Text = "Please enter Ref#.";
                    //    divWarning.Visible = true;
                    //    return;
                    //}
                    //if (string.IsNullOrEmpty(txtSalesExcecutive.Text))
                    //{
                    //    lblWarning.Text = "Please enter Sales Excecutive.";
                    //    divWarning.Visible = true;
                    //    return;
                    //}
                    //if (string.IsNullOrEmpty(txtInvoiceCustomer.Text))
                    //{
                    //    lblWarning.Text = "Please enter Invoice Customer.";
                    //    divWarning.Visible = true;
                    //    return;
                    //}
                    //if (grdItem.Rows.Count == 0)
                    //{
                    //    lblWarning.Text = "Please enter Items.";
                    //    divWarning.Visible = true;
                    //    return;
                    //}

                    INR_RES inr_res = new INR_RES();

                    inr_res.IRS_COM = Session["UserCompanyCode"].ToString();
                    //inr_res.IRS_CHNL = BaseCls.GlbDefChannel;
                    inr_res.IRS_RES_NO = txtReservationNo.Text;
                    inr_res.IRS_EXPTED_DT = Convert.ToDateTime(txtExpectedDate.Text);
                    //inr_res.IRS_RES_TP = "";
                    //inr_res.IRS_CUST_TP = "";
                    //inr_res.IRS_CUST_CD = lblCustomer.Text;
                    //inr_res.IRS_STUS = "S";
                    //inr_res.IRS_RMK = "";
                    ////inr_res.IRS_ANAL_1      =        
                    ////inr_res.IRS_ANAL_2      =        
                    ////inr_res.IRS_ANAL_3      =        
                    ////inr_res.IRS_ANAL_4      =        
                    ////inr_res.IRS_ANAL_5      =        
                    //inr_res.IRS_CRE_BY = Session["UserID"].ToString();
                    //inr_res.IRS_CRE_DT = DateTime.Now;
                    //inr_res.IRS_CRE_SESSION = Session["SessionID"].ToString();
                    ////inr_res.IRS_CNL_BY      =        
                    ////inr_res.IRS_CNL_DT      =        
                    ////inr_res.IRS_CNL_SESSION =        
                    inr_res.IRS_MOD_BY = Session["UserID"].ToString();
                    inr_res.IRS_MOD_DT = DateTime.Now;
                    inr_res.IRS_MOD_SESSION = Session["SessionID"].ToString();

                    //inr_res = Session["inr_res"] as INR_RES;

                    List<INR_RES_DET> inr_res_det = (List<INR_RES_DET>)Session["inr_res_det"];

                    int row_aff = 0;
                    string msg;

                    row_aff = CHNLSVC.Sales.UpdateReservationApprovalNew(inr_res, inr_res_det, out msg);

                    if (row_aff >= 1)
                    {
                        // lblSuccess.Text = "Successfully Updated. Reservation Approval No: " + txtReservationNo.Text;
                        // divSuccess.Visible = true;
                        string _msgbody = "Successfully Updated. Reservation Approval No: " + txtReservationNo.Text;
                        DisplayMessage(_msgbody, 3);
                        #region genaret mail 11 Jan 2017 by lakshan
                        try
                        {
                            CHNLSVC.MsgPortal.SendMailReservationUpdate(txtReservationNo.Text, inr_res.IRS_COM, txtExpectedDate.Text);
                        }
                        catch (Exception ex)
                        {
                            DisplayMessage(_msgbody+" and Email send fail.", 3);
                        }
                        #endregion
                        PageClear();
                        return;
                    }
                    else
                    {
                        //lblWarning.Text = "Updated Fail ...  .";
                        // divWarning.Visible = true;
                        DisplayMessage(msg, 3);
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
            try
            {
                Response.Redirect("~/View/Transaction/Sales/ReservationCancellation.aspx");
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.ToString(), 4);
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
                DisplayMessage(ex.ToString(), 4);
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
                //ReservationNo
                if (lblvalue.Text == "Blsearch")
                {
                   
                    string blqty = grdResult.SelectedRow.Cells[7].Text;
                    decimal BALANCE = Convert.ToDecimal(grdResult.SelectedRow.Cells[9].Text);                  
                    string appqty = Session["appqty"].ToString();
                    string blstatus = grdResult.SelectedRow.Cells[14].Text;
                    if (string.IsNullOrEmpty(LocstatusDES))
                    {
                        string Msg = "please select the location";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + Msg + "');", true);

                        return;
                    }
                    if (BALANCE < Convert.ToDecimal(appqty))
                    {
                        string Msg = "You cannot exceed quantity";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + Msg + "');", true);
                       
                        return;
                    }
                    if (LocstatusDES != blstatus)
                    {
                        string Msg = "please select valid status Tobond";
                      
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + Msg + "');", true);
                        return;
                    }
                    Session["BL"] = grdResult.SelectedRow.Cells[3].Text;
                    Is_select = true;
                }
                //ReservationNo
                if (lblvalue.Text == "422")
                {
                    txtReservationNo.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtReservationNo_TextChanged(null, null);
                    PopulateReservationData();
                }
                //Customer
                if (lblvalue.Text == "13")
                {
                    txtCustomer.Text = grdResult.SelectedRow.Cells[1].Text;
                    lblCustomerName.Text = grdResult.SelectedRow.Cells[2].Text;
                    txtCustomer_TextChanged(null, null);
                }
                //Channel
                if (lblvalue.Text == "channel")
                {
                    txtChannel.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtChannel_TextChanged(null, null);
                }
                //ProfitCenter
                if (lblvalue.Text == "75")
                {
                    txtProfitCenter.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtProfitCenter_TextChanged(null, null);
                }
                //Supplier
                if (lblvalue.Text == "16")
                {
                    txtSupplier.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtSupplier_TextChanged(null, null);
                }
                if (lblvalue.Text == "OrderNo")
                {
                    txtOrderNo.Text = grdResult.SelectedRow.Cells[1].Text;

                }

                ViewState["SEARCH"] = null;
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
                DisplayMessage(ex.ToString(), 4);
            }
        }
        private void FilterData()
        {
            if (lblvalue.Text == "Blsearch")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl_2);
                DataTable _result = CHNLSVC.Inventory.GET_GetTobond_BL_RES(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "Blsearch";
                ViewState["SEARCH"] = _result;
                UserPopup.Show();
            }
            if (lblvalue.Text == "OrderNo")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Tobond_bl);
                DataTable _result = CHNLSVC.Inventory.GET_GetTobond_BL_RES(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "OrderNo";
                ViewState["SEARCH"] = _result;
                UserPopup.Show();
            }
            //Item
            else if (lblvalue.Text == "407")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportItem);
                DataTable result = CHNLSVC.CommonSearch.SearchItemforchange(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "407";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            //Customer
            else if (lblvalue.Text == "13")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "13";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            //Channel

            //ProfitCenters
            else if (lblvalue.Text == "75")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable result = CHNLSVC.CommonSearch.GetPC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "75";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            else if (lblvalue.Text == "Location")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable result = CHNLSVC.CommonSearch.SearchLocationByLocationCategory(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text+"%");
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "75";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            //Supplier
            else if (lblvalue.Text == "16")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "16";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            else if (lblvalue.Text == "Location")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable result = CHNLSVC.CommonSearch.SearchLocationByLocationCategory(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text+"%");
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "16";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            //ReservationNo
            else if (lblvalue.Text == "422")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable result = CHNLSVC.CommonSearch.Search_INT_RES(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "422";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            else if (lblvalue.Text == "channel")
            {
                DataTable result = CHNLSVC.Inventory.GetChannelDetail(Session["UserCompanyCode"].ToString(), txtSearchbyword.Text);
                DataView dv = new DataView(result);
                dv.RowFilter = "msc_act=1";
                result = dv.ToTable();
                result.Columns.RemoveAt(0);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);

                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns.RemoveAt(2);
                result.Columns["msc_cd"].ColumnName = "Code";
                result.Columns["msc_desc"].ColumnName = "Description";

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
                case CommonUIDefiniton.SearchUserControlType.Tobond_bl_2:
                    {

                        string _tp = null;
                        string _datetype = "0";
                        string itemCode = Session["itemCode"].ToString();
                        int cusdec = 1;
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserSBU"].ToString() + seperator + cusdec + seperator + itemCode + seperator + _tp +seperator+ _datetype);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Tobond_bl:
                    {
                       
                        string _tp = null;
                        string _datetype = "0";
                        string itemCode = Session["itemCode"].ToString();
                        int cusdec = 0;
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserSBU"].ToString() + seperator + cusdec + seperator + itemCode + seperator + _tp + seperator + _datetype);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "C" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        string orderType = ddlOrderType.SelectedValue;
                        if (orderType == "0")
                            orderType = string.Empty;
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "S" + seperator + orderType + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GRNItem:
                    {
                        paramsText.Append("PO" + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ReservationNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["GlbDefChannel"].ToString() + seperator + string.Empty + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "DPS" + seperator + string.Empty + seperator);
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
                DisplayMessage(ex.ToString(), 4);
            }
        }
        private void PopulateddlRequestType()
        {
            ddlReservationType.DataSource = CHNLSVC.Sales.Select_REF_REQ_SUBTP("RER");
            ddlReservationType.DataTextField = "RRS_DESC";
            ddlReservationType.DataValueField = "RRS_TP";
            ddlReservationType.DataBind();
            ddlReservationType.Items.Insert(0, new ListItem(" - - Select - - ", "0"));
            ddlReservationType.SelectedValue = "0";
        }
        private void PopulateReservationData()
        {
            if (string.IsNullOrEmpty(txtReservationNo.Text))
            {
                //lblWarning.Text = "Please Enter Reservation No.";
                //divWarning.Visible = true;
                DisplayMessage("Please Enter Reservation No", 2);
                return;
            }

            INR_RES inr_res = new INR_RES();
            inr_res = CHNLSVC.Sales.GetReservationApprovalNew(txtReservationNo.Text);
            if (inr_res == null)
            {
                txtReservationNo.Text = string.Empty;
                // lblWarning.Text = "Invalid Reservation Details.";
                // divWarning.Visible = true;
                DisplayMessage("Invalid Reservation Details", 2);
                return;
            }
            Session["inr_res"] = inr_res;
            lblCustomer.Text = inr_res.IRS_CUST_CD;
            txtReservationDate.Text = inr_res.IRS_RES_DT.ToString("dd/MMM/yyyy");
            txtExpectedDate.Text = inr_res.IRS_EXPTED_DT.ToString("dd/MMM/yyyy");
            string des = CHNLSVC.Sales.GetDescription(inr_res.IRS_RES_TP);
            lvltypevalue.Text = inr_res.IRS_RES_TP;
            lblreservationtype.Text = des;
            List<INT_REQ> int_req = CHNLSVC.Sales.GetINT_REQList(inr_res.IRS_SEQ);
            ViewState["int_req"] = int_req;
            grdRequest.DataSource = int_req;
            grdRequest.DataBind();

            List<INR_RES_DET> inr_res_det = new List<INR_RES_DET>();
            inr_res_det = CHNLSVC.Sales.GetGetReservationApprovalItem(inr_res.IRS_SEQ);
            grdReservationItems.DataSource = inr_res_det;
            grdReservationItems.DataBind();
            Session["inr_res_det"] = inr_res_det;

            List<INR_RES_LOG> inr_res_log = new List<INR_RES_LOG>();
            inr_res_log = CHNLSVC.Sales.GetGetReservationlog(inr_res.IRS_SEQ, null);
            Session["inr_res_log"] = inr_res_log;

            lbtnSave.Enabled = true;
            lbtnSave.OnClientClick = "return Enable();";
            lbtnSave.CssClass = "buttoncolor";
            lbtnUpdate.Enabled = true;
            lbtnUpdate.OnClientClick = "ConfirmUpdate();";
            lbtnUpdate.CssClass = "buttonUndocolor";
            Session["IsUpdate"] = "1";
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
            //grdItem.DataSource = int_req_itm;
            //grdItem.DataBind();
            //ViewState["int_req_itm"] = int_req_itm;
            //txtTotalQty.Text = int_req_itm.Sum(x => x.ITRI_QTY).ToString("N2");

            //List<INT_REQ_SER> int_req_ser = new List<INT_REQ_SER>();
            //int_req_ser = CHNLSVC.Sales.GetINT_REQ_SER(int_req.ITR_SEQ_NO);
            //ViewState["int_req_ser"] = int_req_ser;

            //lbtnSave.Enabled = false;
            //lbtnSave.OnClientClick = "return Enable();";
            //lbtnSave.CssClass = "buttoncolor";

            //if (int_req_itm.Sum(x => x.ITRI_APP_QTY) == 0 && int_req.ITR_STUS == "P")
            //{
            //    if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16014))
            //    {
            //        lbtnApprove.Enabled = true;
            //        lbtnApprove.OnClientClick = "ConfirmApprove();";
            //        lbtnApprove.CssClass = "buttonUndocolor";
            //    }
            //    if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16015))
            //    {
            //        lbtnCancel.Enabled = true;
            //        lbtnCancel.OnClientClick = "ConfirmCancel();";
            //        lbtnCancel.CssClass = "buttonUndocolor";
            //    }

            //    lbtnUpdate.Enabled = true;
            //    lbtnUpdate.OnClientClick = "ConfirmUpdate();";
            //    lbtnUpdate.CssClass = "buttonUndocolor";
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
        private void PopulateWarehouseType()
        {
            DataTable dt = CHNLSVC.Sales.Select_REF_LOC_TP(null);
            ddlWarehouseType.DataSource = dt;
            ddlWarehouseType.DataTextField = "RLT_DESC";
            ddlWarehouseType.DataValueField = "RLT_CD";
            ddlWarehouseType.DataBind();
            ddlWarehouseType.Items.Insert(0, new ListItem(" - - ALL - - ", "0"));
            ddlWarehouseType.SelectedValue = "0";
        }
        public Boolean GetDaleteVisibility()
        {
            Boolean b = true;
            //b = (Boolean)ViewState["DaleteVisibility"];
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
            _invReq = new InventoryRequest();
            _reqItmList = new List<InventoryRequestItem>();
            _serPopAppAll = false;
            _serPopShow = false;
            if (string.IsNullOrEmpty(Session["UserSBU"].ToString()))
            {
                lblSbuMsg1.Text = "SBU (Strategic Business) is not allocate for your login ID.";
                lblSbuMsg2.Text = "There is not setup default SBU (Sttre Buds Unit) for your login ID.";
                SbuPopup.Show();
            }
            oMasterItemStatuss = new List<MasterItemStatus>();
            loadItemStatus();
            LocstatusDES = "";
            Is_select = false;
            Session["BL"] = "";
            ViewState["Location"] = "";
            txtCustomer.Text = string.Empty;
            lblCustomerName.Text = string.Empty;
            txtChannel.Text = string.Empty;
            txtProfitCenter.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
            txtToDate.Text = string.Empty;
            txtToDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            lvltypevalue.Text = string.Empty;
            lblreservationtype.Text = string.Empty;
            grdRequest.DataSource = new int[] { };
            grdRequest.DataBind();

            txtReservationNo.Text = string.Empty;
            lblCustomer.Text = string.Empty;
            txtReservationDate.Text = string.Empty;
            txtReservationDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtExpectedDate.Text = string.Empty;
            grdRequestItem.DataSource = new int[] { };
            grdRequestItem.DataBind();

            rbtnInStock.Checked = true;
            Panel2.Visible = true;
            rbtnPenddingOrders.Checked = false;
            Panel1.Visible = false;

            Session["inr_res_det"] = null;
            Session["itemCode"] = "";
            ddlWarehouseType.SelectedIndex = -1;

            ddlOrderType.SelectedIndex = -1;
            txtSupplier.Text = string.Empty;
            txtOrderNo.Text = string.Empty;
            txtIBFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
            txtIBToDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");

            grdReservationItems.DataSource = new int[] { };
            grdReservationItems.DataBind();

            grdInventoryBalance.DataSource = new int[] { };
            grdInventoryBalance.DataBind();

            PopulateddlRequestType();

            lbtnSave.Enabled = true;
            lbtnSave.OnClientClick = "ConfirmSave();";
            lbtnSave.CssClass = "buttonUndocolor";
            lbtnUpdate.Enabled = false;
            lbtnUpdate.OnClientClick = "return Enable();";
            lbtnUpdate.CssClass = "buttoncolor";
            //lbtnCancel.Enabled = false;
            //lbtnCancel.OnClientClick = "return Enable();";
            //lbtnCancel.CssClass = "buttoncolor";
            // Session.Clear();
            grdPendingOrderBalance.DataSource = new int[] { };
            grdPendingOrderBalance.DataBind();
            if (rbtnPenddingOrders.Checked)
            {
                grdPendingOrderBalance.Visible = true;
            }
            else
            {
                grdPendingOrderBalance.Visible = false;
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
        }

        protected void btnDClose_Click(object sender, EventArgs e)
        {

            txtSearchbywordD.Text = "";
            UserDPopoup.Hide();
        }

        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "422")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReservationNo);
                DataTable result = CHNLSVC.CommonSearch.SearchResvationApproveNo(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "422";
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbywordD.Focus();
            }
        }

        protected void txtSearchbywordD_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnSearchD_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "422")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReservationNo);
                DataTable result = CHNLSVC.CommonSearch.SearchResvationApproveNo(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "422";
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbywordD.Focus();
            }
        }

        protected void grdResultD_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Name = grdResultD.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "422")
            {
                txtReservationNo.Text = grdResultD.SelectedRow.Cells[1].Text;
                PopulateReservationData();
                lblvalue.Text = "";
            }
        }

        protected void grdResultD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (lblvalue.Text == "422")
            {
                grdResultD.PageIndex = e.NewPageIndex;
                grdResultD.DataSource = null;
                grdResultD.DataSource = (DataTable)ViewState["SEARCH"];
                grdResultD.DataBind();
                UserDPopoup.Show();
            }
        }
        protected void btnSbu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/ADMIN/Home.aspx");
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
        protected void lbtnShowAppAll_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow _row = (GridViewRow)btn.NamingContainer;
                Label lblITR_REQ_NO = _row.FindControl("lblITR_REQ_NO") as Label;
                _invReq = CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no=lblITR_REQ_NO.Text.Trim() }).FirstOrDefault();
                if (_invReq!=null)
                {
                    if (_invReq.Itr_sub_tp!="DPS")
                    {
                        DispMsg("Please select the DPS type reservation !"); return;
                    }
                    _reqItmList = CHNLSVC.Inventory.GET_INT_REQ_ITM_BY_SEQ(_invReq.Itr_seq_no);
                    dgvAppAllRes.DataSource = new int[] { };
                    if (_reqItmList.Count>0)
                    {
                       
                        dgvAppAllRes.DataSource = _reqItmList; 
                    }
                    dgvAppAllRes.DataBind();
                    _serPopAppAll = true;
                    popAppAllRes.Show();
                }
                else
                {
                    DispMsg("Please select the valid request # !");
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        

        protected void lbtnAppAllResCls_Click(object sender, EventArgs e)
        {
            _serPopAppAll = false;
            popAppAllRes.Hide();
        }
        private void DataAvailable(DataTable _dt, string _valText, string _valCol, string _valToolTipCol = "")
        {
            _ava = false;
            _toolTip = string.Empty;
            foreach (DataRow row in _dt.Rows)
            {
                if (!string.IsNullOrEmpty(row[_valCol].ToString()))
                {
                    if (_valText == row[_valCol].ToString())
                    {
                        _ava = true;
                        if (!string.IsNullOrEmpty(_valToolTipCol))
                        {
                            _toolTip = row[_valToolTipCol].ToString();
                        }
                        break;
                    }
                }
            }
        }
        protected void txtAllresLoc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtAllresLoc.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtAllresLoc.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    _serData = CHNLSVC.CommonSearch.SearchLocationByLocationCategory(_para, "CODE", txtAllresLoc.Text.ToUpper().Trim().ToUpper());
                    DataAvailable(_serData, txtAllresLoc.Text.ToUpper().Trim(), "CODE", "Description");
                    txtAllresLoc.ToolTip = _ava ? _toolTip : "";
                    if (_ava)
                    {
                        //txtYearFrom.Focus();
                    }
                    else
                    {
                        txtAllresLoc.Text = string.Empty;
                        txtAllresLoc.Focus();
                        DispMsg("Please enter valid location !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        private void OrderSearchGridData(string _colName, string _ordTp)
        {
            try
            {
                if (_serData.Rows.Count > 0)
                {
                    DataView dv = _serData.DefaultView;
                    string dvSortStr = _colName + " " + _ordTp;
                    dv.Sort = dvSortStr;
                    _serData = dv.ToTable();
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }
        public void BindDdlSerByKey(DataTable _dataSource)
        {
            if (_dataSource.Columns.Contains("From Date"))
            {
                _dataSource.Columns.Remove("From Date");
            }
            if (_dataSource.Columns.Contains("To Date"))
            {
                _dataSource.Columns.Remove("To Date");
            }
            this.ddlSerByKey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSerByKey.Items.Add(col.ColumnName);
            }

            this.ddlSerByKey.SelectedIndex = 0;
        }
        private void LoadSearchPopup(string serType, string _colName, string _ordTp, bool _isOrder = true)
        {
            if (_isOrder)
            {
                OrderSearchGridData(_colName, _ordTp);
            }
            try
            {
                dgvResult.DataSource = new int[] { };
                dgvResult.DataBind();
                if (_serData != null)
                {
                    if (_serData.Rows.Count > 0)
                    {
                        dgvResult.DataSource = _serData;
                        dgvResult.DataBind();
                        BindDdlSerByKey(_serData);
                    }
                }
                txtSerByKey.Text = "";
                txtSerByKey.Focus();
                _serType = serType;
                PopupSearch.Show();
                _serPopShow = true;
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }
        protected void lbtnAllResSerLoc_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                _serData = CHNLSVC.CommonSearch.SearchLocationByLocationCategory(_para, null, null);
                LoadSearchPopup("Location", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message,"E");
            }
        }

        protected void lbtnClsClose_Click(object sender, EventArgs e)
        {
            _serPopShow = false;
            PopupSearch.Show();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (_serType == "Location")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    _serData = CHNLSVC.CommonSearch.SearchLocationByLocationCategory(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim()+"%");
                    OrderSearchGridData("CODE", "ASC");
                } 
                dgvResult.DataSource = new int[] { };
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
                if (_serData.Rows.Count > 0)
                {
                    
                    dgvResult.DataSource = _serData;
                }
                //txtSerByKey.Text = "";
                txtSerByKey.Focus();
                dgvResult.DataBind();
                PopupSearch.Show();
                _serPopShow = true;
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void dgvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvResult.PageIndex = e.NewPageIndex;
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                }
                else
                {
                    dgvResult.DataSource = new int[] { };
                }
                dgvResult.DataBind();
                txtSerByKey.Text = string.Empty;
                txtSerByKey.Focus();
                PopupSearch.Show();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void dgvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string code = dgvResult.SelectedRow.Cells[1].Text;
                if (_serType == "Location")
                {
                    txtAllresLoc.Text = code;
                    txtAllresLoc_TextChanged(null, null);
                } _serPopShow = false;
                PopupSearch.Hide();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnAppAllCls_Click(object sender, EventArgs e)
        {
            try
            {
                List<TmpValidation> _tmpValList = new List<TmpValidation>();
                TmpValidation _tmpVal = new TmpValidation();
                InventoryLocation _tmpInrLoc = new InventoryLocation();
                InventoryLocation _inrLocErr = new InventoryLocation();
                InventoryLocation _inrLocBal = new InventoryLocation();
                INR_RES _inrRes = new INR_RES();
                INR_RES_DET _inrResDet = new INR_RES_DET();
                INR_RES_LOG _inrResLog = new INR_RES_LOG();
                bool _balAva = false;
                foreach (var item in _reqItmList)
                {
                    _tmpInrLoc = new InventoryLocation()
                    {
                        Inl_com = Session["UserCompanyCode"].ToString(),
                        Inl_loc = txtAllresLoc.Text.Trim().ToUpper(),
                        Inl_itm_cd = item.Itri_itm_cd,
                        Inl_itm_stus = item.Itri_itm_stus
                    };
                    _inrLocBal = CHNLSVC.Inventory.GET_INR_LOC_BALANCE(_tmpInrLoc);
                    if (_inrLocBal==null)
                    {
                        _tmpVal.Inl_itm_cd = item.Itri_itm_cd;
                        _tmpVal.Inl_itm_stus = item.Itri_itm_stus;
                        _tmpVal.errorMsg = "Balance not available !";
                        _tmpValList.Add(_tmpVal);
                    }
                    else
                    {
                        if (_inrLocBal.Inl_qty < item.Itri_bqty)
                        {
                             _tmpVal.Inl_itm_cd = item.Itri_itm_cd;
                             _tmpVal.Inl_itm_stus = item.Itri_itm_stus;
                             _tmpVal.errorMsg = "Balance not available !";
                             _tmpValList.Add(_tmpVal);
                        }
                    }
                }
                if (_tmpValList.Count > 0)
                {
                    DispMsg("Balance not available ! "); return;
                }
                #region auto no
                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                masterAuto.Aut_cate_tp = Session["UserCompanyCode"].ToString();  //Session["UserDefLoca"].ToString(); Edit by Chamal 24-Aug-2016
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "RES";
                masterAuto.Aut_number = 5;
                masterAuto.Aut_start_char = "RES";
                masterAuto.Aut_year = Convert.ToInt32(DateTime.Now.ToString("yy")); ;
                #endregion
                #region create res hdr
                _inrRes = new INR_RES();
                _inrRes.IRS_COM = Session["UserCompanyCode"].ToString();
                MasterLocation _mstLoc = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), txtAllresLoc.Text.ToUpper());
                if (_mstLoc == null)
                {
                    DispMsg("Please select the valid location !"); return;
                }
                _inrRes.IRS_CHNL = _mstLoc.Ml_cate_2;
                //inr_res.IRS_CHNL = Session["GlbDefChannel"].ToString();//BaseCls.GlbDefChannel;
                _inrRes.IRS_RES_DT = DateTime.Today;
                _inrRes.IRS_RES_TP = _invReq.Itr_anal1;
                _inrRes.IRS_CUST_TP = "C";
                _inrRes.IRS_CUST_CD = string.IsNullOrEmpty(_invReq.Itr_bus_code) ? "N/A" : _invReq.Itr_bus_code;
                _inrRes.IRS_STUS = "A";
                _inrRes.IRS_RMK = "";
                _inrRes.IRS_ANAL_2 = "APP_ALL";
                _inrRes.IRS_CRE_BY = Session["UserID"].ToString();
                _inrRes.IRS_CRE_DT = DateTime.Now;
                _inrRes.IRS_CRE_SESSION = Session["SessionID"].ToString();
                #endregion
                _inrRes.Inr_res_det = new List<INR_RES_DET>();
                _inrRes.Inr_res_log = new List<INR_RES_LOG>();
                Int32 _lineNo = 0;
                foreach (var item in _reqItmList)
                {
                    #region res det
                    _inrResDet = new INR_RES_DET();
                    if ( _inrRes.Inr_res_det.Count>0)
                    {
                        var _itmAva = _inrRes.Inr_res_det.Where(c => c.IRD_ITM_CD == item.Itri_itm_cd).FirstOrDefault();
                        if (_itmAva!=null)
                        {
                            DispMsg("Item already available ! "+ item.Itri_itm_cd ); return;
                        }
                        _lineNo = _inrRes.Inr_res_det.Count + 1;
                    }
                    else
                    {
                        _lineNo = 1;
                    }
                    _inrResDet.IRD_SEQ = 0;
                    _inrResDet.IRD_RES_NO = "";
                    _inrResDet.IRD_LINE = _lineNo;
                    _inrResDet.IRD_ITM_CD = item.Itri_itm_cd;
                    _inrResDet.IRD_ITM_STUS = item.Itri_itm_stus;
                    _inrResDet.IRD_RES_QTY = item.Itri_bqty;
                    _inrResDet.IRD_RES_BQTY = item.Itri_bqty;
                    _inrResDet.IRD_RES_CQTY = 0;
                    _inrResDet.IRD_REQ_BQTY = 0;
                    _inrResDet.IRD_REQ_CQTY = 0;
                    _inrResDet.IRD_ACT = 1;
                    _inrResDet.IRD_RESREQ_NO = _invReq.Itr_req_no;
                    _inrResDet.IRD_RESREQ_LINE = item.Itri_line_no;
                    _inrResDet.MI_MODEL = item.Mi_model;
                    _inrResDet.MIS_DESC = item.Mi_shortdesc;
                    _inrResDet.LOC_CD = txtAllresLoc.Text.Trim().ToUpper();
                    _inrRes.Inr_res_det.Add(_inrResDet);
                    #endregion
                    #region res_log
                    _inrResLog = new INR_RES_LOG();
                    _inrResLog.IRL_SEQ = 0;
                    _inrResLog.IRL_RES_NO = "";
                    _inrResLog.IRL_RES_LINE = _lineNo;
                    _inrResLog.IRL_LINE = 1;
                    _inrResLog.IRL_ITM_CD = item.Itri_itm_cd;
                    _inrResLog.IRL_ITM_STUS = item.Itri_itm_stus;
                    _inrResLog.IRL_RES_QTY = item.Itri_bqty;
                    _inrResLog.IRL_RES_BQTY = item.Itri_bqty;
                    _inrResLog.IRL_RES_IQTY = 0;
                    _inrResLog.IRL_ORIG_DOC_TP = "INV";
                    _inrResLog.IRL_ORIG_DOC_NO = "";//TODO
                    _inrResLog.IRL_ORIG_DOC_DT = DateTime.Now.Date;//TODO
                    _inrResLog.IRL_ORIG_ITM_LINE = 0;//TODO
                    _inrResLog.IRL_ORIG_COM = Session["UserCompanyCode"].ToString();
                    _inrResLog.IRL_ORIG_LOC = txtAllresLoc.Text.ToUpper();
                    _inrResLog.LOC_CD = txtAllresLoc.Text.ToUpper();
                    _inrResLog.IRL_CURT_DOC_DT = DateTime.Today;
                    _inrResLog.IRL_CURT_COM = Session["UserCompanyCode"].ToString(); ;
                    _inrResLog.IRL_CURT_LOC = txtAllresLoc.Text.ToUpper(); 
                    _inrResLog.IRL_ACT = 1;
                    _inrResLog.IRL_CRE_BY = Session["UserID"].ToString();
                    _inrResLog.IRL_CRE_DT = DateTime.Now;
                    _inrResLog.IRL_CRE_SESSION = Session["SessionID"].ToString();
                    _inrRes.Inr_res_log.Add(_inrResLog);
                    #endregion
                }
                string _err = "", _finDoc="";
                Int32 _res = CHNLSVC.Sales.SaveReservationApprovalAll(_inrRes, masterAuto, out _err, out _finDoc);
                if (_res>0)
                {
                    string _msgbody = "Successfully created the reservation approval number : " + _finDoc;
                    DisplayMessage(_msgbody, 3);
                    PageClear();
                    _serPopAppAll = false;
                    popAppAllRes.Hide();
                    #region genaret mail 11 Jan 2017 by lakshan
                    try
                    {
                        CHNLSVC.MsgPortal.SendMailReservationApprove(_inrRes.IRS_COM, _finDoc);
                    }
                    catch (Exception ex)
                    {
                        DisplayMessage(_msgbody+" and Email send fail", 3);
                        PageClear();
                    }
                    #endregion
                }
                else
                {
                    if (_err.Contains("IRS_CHNL"))
                    {
                        DisplayMessage("Please check channel", 2);
                    }
                    if (_err.Contains("CHK_INLFREEQTY"))
                    {
                        DispMsg("Free Qty is not available in location : " + _inrRes.IRS_ANAL_2);
                    }
                    else
                    {
                        DispMsg(_err, "E");
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }
    }
}