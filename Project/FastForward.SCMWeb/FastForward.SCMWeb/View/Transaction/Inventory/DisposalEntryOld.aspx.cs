using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using System.IO;
using FF.BusinessObjects.General;
using FF.BusinessObjects.InventoryNew;
using System.Configuration;
using System.Data.OleDb;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.View.Reports.Inventory;
using FastForward.SCMWeb.View.Reports.Sales;
using System.Drawing;
using FastMember;

namespace FastForward.SCMWeb.View.Transaction.Inventory
{
    public partial class DisposalEntryOld : Base
    {
        protected List<ReptPickSerials> scanSerList { get { return (List<ReptPickSerials>)Session["scanSerList"]; } set { Session["scanSerList"] = value; } }
        protected DisposalHeader oDisposalHeader { get { return (DisposalHeader)Session["oDisposalHeader"]; } set { Session["oDisposalHeader"] = value; } }
        protected List<DisposalLocation> oDisposalLocations { get { return (List<DisposalLocation>)Session["oDisposalLocations"]; } set { Session["oDisposalLocations"] = value; } }
        protected List<DispCurrentLocation> oStockAtLocations { get { return (List<DispCurrentLocation>)Session["oStockAtLocations"]; } set { Session["oStockAtLocations"] = value; } }
        protected List<DisposalCurrStatus> oItemStatus { get { return (List<DisposalCurrStatus>)Session["oItemStatus"]; } set { Session["oItemStatus"] = value; } }
        protected List<DisposalItem> oItem { get { return (List<DisposalItem>)Session["oItem"]; } set { Session["oItem"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Clear();
                dgvPendingJobs.DataSource = new int[] { };
                dgvPendingJobs.DataBind();

                dgvLocations.DataSource = new int[] { };
                dgvLocations.DataBind();

                dgvStatus.DataSource = new int[] { };
                dgvStatus.DataBind();

                dgvStockAt.DataSource = new int[] { };
                dgvStockAt.DataBind();

                grdDspItms.DataSource = new int[] { };
                grdDspItms.DataBind();

                grdPymntDet.DataSource = new int[] { };
                grdPymntDet.DataBind();

                grdPrintDoc.DataSource = new int[] { };
                grdPrintDoc.DataBind();

                txtValidFrom.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
                txtValidTo.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                txtDate.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                //BindUserCompanyItemStatusDDLData(ddlStatusToBeChange);

                ucPayModes1.InvoiceType = "DISP";
                ucPayModes1.LoadPayModes();
                //grdSerial.DataSource = new int[] { };
                //grdSerial.DataBind();
                //grdDspItms.Columns[8].Visible = false;
                PopulateLoadingBays();
                btnJobCreate.Enabled = true;
                btnUpdate.Enabled = false;
                btnUpdate.OnClientClick = null;


                txtJobAmount.ReadOnly = false;

                //ucOutScan.adjustmentTypeValue = "+";
                //ViewState["adjustmentTypeValue"] = "+";
            }
            //ucPayModes1.InvoiceType = "DISP";
            //ucPayModes1.LoadPayModes();

        }
        protected void Clear()
        {
            if (Session["UserID"] == null || Session["UserCompanyCode"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            //ucOutScan.isApprovalSend = false;
            oDisposalHeader = new DisposalHeader();
            oDisposalLocations = new List<DisposalLocation>();
            scanSerList = new List<ReptPickSerials>();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "CollapseHide()", true);

            txtFrom.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
            txtTo.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtJobNo.Text = "";

            txtDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtJobNumberPending.Text = "";
            txtValidFrom.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
            txtValidTo.Text = DateTime.Now.ToString("dd/MMM/yyyy");

            txtReference.Text = "";
            txtRemark.Text = "";
            txtLocation.Text = "";
            txtLocDesc.Text = "";

            btnSaveDispItm.Enabled = false;

            dgvPendingJobs.DataSource = new int[] { };
            dgvPendingJobs.DataBind();

            dgvLocations.DataSource = new int[] { };
            dgvLocations.DataBind();

            dgvStatus.DataSource = new int[] { };
            dgvStatus.DataBind();

            dgvStockAt.DataSource = new int[] { };
            dgvStockAt.DataBind();

            grdDspItms.DataSource = new int[] { };
            grdDspItms.DataBind();

            grdPymntDet.DataSource = new int[] { };
            grdPymntDet.DataBind();

            grdPrintDoc.DataSource = new int[] { };
            grdPrintDoc.DataBind();

            Session["oDisposalHeader"] = null;
            Session["oDisposalLocations"] = null;
            Session["oStockAtLocations"] = null;
            Session["oItemStatus"] = null;
            Session["oItem"] = null;
            ViewState["SerialList"] = null;

            lblJobNumber.ToolTip = "";

            Session["GlbModuleName"] = "m_Trans_Inventory_DisposalEntry";

            DataTable dtchk_warehouse_availability = CHNLSVC.Inventory.CheckWareHouseAvailability(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
            if (dtchk_warehouse_availability.Rows.Count > 0)
            {
                chkSendToPDA.Enabled = true;

                foreach (DataRow ddrware in dtchk_warehouse_availability.Rows)
                {
                    Session["WAREHOUSE_COM"] = ddrware["ml_wh_com"].ToString();
                    Session["WAREHOUSE_LOC"] = ddrware["ml_wh_cd"].ToString();
                }
            }
            else
            {
                chkSendToPDA.Enabled = false;
            }

            //cpe.Collapsed = false;

            btnDispose.Enabled = true;
            btnDispose.CssClass = "buttonUndocolor";
            btnDispose.OnClientClick = "return confSave()";


            //btnUpdate.Enabled = true;
            //btnUpdate.CssClass = "buttonUndocolor";
            //btnUpdate.OnClientClick = "return confSave()";

            btnJobCreate.Enabled = true;
            btnUpdate.Enabled = false;
            btnUpdate.OnClientClick = null;
            btnUpdate.ForeColor = Color.Purple;
            btnApprove.Enabled = true;
            btnApprove.OnClientClick = "return confApprove()";
            btnApprove.ForeColor = Color.Purple;
            btnDispose.Enabled = true;
            btnDispose.OnClientClick = "return confDispose()";
            btnDispose.ForeColor = Color.Purple;

            btnPaymnt.Enabled = true;

            txtJobAmount.ReadOnly = false;
            //if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10119))
            //{
            //    cpe.Collapsed = true;
            //    pnlJobSave.Enabled = true;

            //    btnSave.Enabled = true;
            //    btnSave.CssClass = "buttonUndocolor";
            //    btnSave.OnClientClick = "return confSave()";
            //    pnlUserControl.Enabled = false;
            //}

            //if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16034))
            //{
            //    cpe.Collapsed = true;
            //    pnlJobSave.Enabled = true;
            //    pnlUserControl.Enabled = true;

            //    btnSave.Enabled = true;
            //    btnSave.CssClass = "buttonUndocolor";
            //    btnSave.OnClientClick = "return confSave()";



            //    ddlPaymentType.SelectedValue = "POD";
            //    ddlPaymentType.Enabled = false;
            //}

            txtStatus.Text = "";
            txtJobAmount.Text = "";
            txtItmDispLoc.Text = "";
            txtLocation.Text = "";
            txtQty.Text = "";
            txtSerialI.Text = "";
            txtSerialII.Text = "";
            txtSerialIII.Text = "";
            textCust.Text = "";
            txtSerialI.ReadOnly = false;
            txtSerialII.ReadOnly = false;
            txtSerialIII.ReadOnly = false;
            txtQty.ReadOnly = false;

            txtValidFrom.ReadOnly = false;
            txtValidTo.ReadOnly = false;
            textCust.ReadOnly = false;
            txtReference.ReadOnly = false;
            txtRemark.ReadOnly = false;

            ucPayModes1.MainGrid.DataSource = new List<RecieptItem>();
            ucPayModes1.MainGrid.DataBind();
            ucPayModes1.PaidAmountLabel.Text = "0.00";
            ucPayModes1.MainGrid.DataSource = new int[] { };
            ucPayModes1.MainGrid.DataBind();

            //grdDspItms.Columns[0].Visible = true;
            //dgvLocations.Columns[2].Visible = true;
            //dgvStockAt.Columns[2].Visible = true;
            //dgvStatus.Columns[0].Visible = true;
        }

        protected void Item_Clear()
        {
            txtItemCode.Text = "";
            txtSerialI.Text = "";
            txtSerialII.Text = "";
            txtSerialIII.Text = "";
            txtStatus.Text = "";
            ddlStatus.Items.Clear();
            txtQty.Text = "";
            txtItmDispLoc.Text = "";
            lblDescription.Text = "";
            lblItmDispLoc.Text = "";
            lblModel.Text = "";
            lblBrand.Text = "";
            lblPart.Text = "";
            txtSerialI.ReadOnly = false;
            txtSerialII.ReadOnly = false;
            txtSerialIII.ReadOnly = false;
            txtQty.ReadOnly = false;
            txtItmcurLoc.Text = "";
            txtItmcurLoc.ReadOnly = false;
        }

        protected void btnPaymnt_Click(object sender, EventArgs e)
        {
            if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16060))
            {
                if (Convert.ToDateTime(txtValidTo.Text) < DateTime.Today)
                {
                    DisplayMessage("Valid date is expired!!!");
                    return;
                }
                if (string.IsNullOrEmpty(txtJobNo.Text))
                {
                    DisplayMessage("Select a job number!!!");
                    return;
                }
                ucPayModes1.payModeClear();
                ucPayModes1.PageClear();
                ucPayModes1.MainGrid.DataSource = new int[] { };
                ucPayModes1.MainGrid.DataBind();
                ucPayModes1.RecieptItemList = null;
                if (!string.IsNullOrEmpty(txtJobNo.Text))
                {
                    ucPayModes1.lblBalanceAmountPub.Text = (0.00m).ToString();
                    DisposalHeader jobEntry = CHNLSVC.Inventory.GET_DISPOSAL_JOB_HEADER(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtJobNo.Text, "ALL");
                    if (jobEntry != null)
                    {
                        if (jobEntry.Dh_stus == "A")
                        {
                            List<RecieptHeader> recDetList = CHNLSVC.Sales.GetRecieptByRefDoc(Session["UserCompanyCode"].ToString(), jobEntry.Dh_doc_no);
                            decimal paidAmount = 0.00m;
                            foreach (RecieptHeader rcHead in recDetList)
                            {
                                paidAmount += rcHead.Sar_tot_settle_amt;
                            }


                            mpPayment.Show();

                            ucPayModes1.TotalAmount = Convert.ToDecimal(txtJobAmount.Text);
                            //ucPayModes1.Amount.Text = (Convert.ToDecimal(txtJobAmount.Text) - paidAmount).ToString();
                            ucPayModes1.PaidAmountLabel.Text = paidAmount.ToString();
                            ucPayModes1.lblBalanceAmountPub.Text = (Convert.ToDecimal(txtJobAmount.Text) - paidAmount).ToString();
                            ucPayModes1.DropdownChange();


                            string c = ucPayModes1.PayModeCombo.SelectedValue;




                        }
                        else
                        {
                            DisplayMessage("Job no is not approved!!");
                            // Clear();
                            txtJobNo.Focus();
                            return;
                        }
                    }
                    else
                    {
                        DisplayMessage("Job no is invalid. Please check the Job No");
                        txtJobNo.Text = "";
                        txtJobNo.Focus();
                        return;
                    }

                }
                else
                {
                    DisplayMessage("Please select a job no.!!");
                    txtJobNo.Text = "";
                    txtJobNo.Focus();
                    return;
                }
            }
            else
            {
                DisplayMessage("Sorry, You have no permission to make a reciept! - ( Advice: Required permission code : 16060)");
            }

        }

        protected void btnAClose_Click(object sender, EventArgs e)
        {

            mpPayment.Hide();
        }

        protected void btnLocation_Click(object sender, EventArgs e)
        {
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
            DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "dispatchLocation";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpSearchAdance.Show();
            txtSearchbyword.Focus();
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InsuCom:
                    {
                        paramsText.Append("INS" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InsuPolicy:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator); break;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DisposalJOb:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator); break;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocSubType:
                    {
                        paramsText.Append("ADJ" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes:
                    {
                        paramsText.Append(Session["_SerialSearchType"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + txtItemCode.Text.ToUpper() + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial:
                //    {
                //        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + txtOtherRef.Text);
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.NIC:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Customer:
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

        public void BindUCtrlDDLData2(DataTable _dataSource)
        {
            this.ddlSearchbykey2.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey2.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey2.SelectedIndex = 0;
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                if (lblvalue.Text == "dispatchLocation")
                {
                    txtLocation.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtLocation_TextChanged(null, null);
                    BindLocation();
                }
                if (lblvalue.Text == "stockAtLocation")
                {
                    txtStockAt.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtStockAt_TextChanged(null, null);
                }

                if (lblvalue.Text == "itemStatus")
                {
                    txtStatus.Text = grdResult.SelectedRow.Cells[2].Text;
                    txtStatus_TextChanged(null, null);
                }
                if (lblvalue.Text == "itemSearch")
                {
                    txtItemCode.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtItemCode_TextChanged(null, null);
                }
                if (lblvalue.Text == "itmDispLocation")
                {
                    txtItmDispLoc.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtItmDispLoc_TextChanged(null, null);
                }
                if (lblvalue.Text == "custSearch")
                {
                    textCust.Text = grdResult.SelectedRow.Cells[5].Text;
                    textCust_TextChanged(null, null);
                }
                if (lblvalue.Text == "itmSerialI")
                {
                    txtSerialI.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtSerialI_TextChanged(null, null);
                }

                ViewState["SEARCH"] = null;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
            }
        }

        protected void grdResult2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                if (lblvalue2.Text == "DisposalJOb")
                {
                    txtJobNo.Text = grdResult2.SelectedRow.Cells[1].Text;
                    txtJobNo_TextChanged(null, null);
                    //BindLocation();
                }


                ViewState["SEARCH"] = null;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
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
                mpSearchAdance.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
            }
        }

        protected void grdResult2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdResult2.PageIndex = e.NewPageIndex;
                grdResult2.DataSource = null;
                grdResult2.DataSource = (DataTable)ViewState["SEARCH2"];
                grdResult2.DataBind();
                mpSearch.Show();
                txtSearchbyword2.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
            }
        }

        private void DisplayMessage(String Msg, Int32 option, Exception ex = null)
        {
            Msg = Msg.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ").Replace("\r", "");
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + Msg + "');", true);
                WriteErrLog(Msg, ex);
            }
        }

        private void WriteErrLog(string err, Exception ex)
        {
            using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/data.txt"), true))
            {
                if (ex == null)
                {
                    _testData.WriteLine(DateTime.Now.ToString("dd/MMM/yyyy  hh:mm:ss tt") + " - " + GetIPAddress() + " | " + err + "   || Exception : " + err);
                }
                else
                {
                    _testData.WriteLine(DateTime.Now.ToString("dd/MMM/yyyy  hh:mm:ss tt") + " - " + GetIPAddress() + " | " + err + "   || Exception : " + ex.Message);

                }
            }
        }

        private string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        protected void txtLocation_TextChanged(object sender, EventArgs e)
        {
            BindLocation();
            DataTable _tbl = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), txtLocation.Text.ToUpper().Trim());
            if (_tbl == null || _tbl.Rows.Count <= 0)
            {
                DisplayMessage("Dispatch location is invalid. Please check the location.");
                txtLocation.Text = "";
                txtLocation.Focus();
                return;
            }
            else
            {
                txtLocDesc.Text = _tbl.Rows[0]["ML_LOC_DESC"].ToString();
                // btnAddLocation.Focus();
            }
        }
        private void DisplayMessage(String Msg)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
        }

        private void BindLocation()
        {
            dgvLocations.DataSource = oDisposalLocations;
            dgvLocations.DataBind();
        }
        private void BindStockAtLocation()
        {
            dgvStockAt.DataSource = oStockAtLocations;
            dgvStockAt.DataBind();
        }

        private void BindItemStatus()
        {
            dgvStatus.DataSource = oItemStatus;
            dgvStatus.DataBind();
        }

        private void ClearLocLine()
        {

            txtLocation.Text = "";
            txtLocDesc.Text = "";
            txtLocation.Focus();
        }

        private void ClearStockAtLocLine()
        {

            txtStockAt.Text = "";
            txtStockAtDesc.Text = "";
            txtStockAt.Focus();
        }

        private void ClearItemStatus()
        {

            txtStatus.Text = "";
            txtStatusCd.Text = "";
            txtStatus.Focus();
        }
        protected void btnAddLocation_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLocation.Text.Trim()))
            {
                bool isExist = false;
                List<DisposalLocation> disposalLocation = new List<DisposalLocation>();
                if (Session["oDisposalLocations"] != null)
                {
                    disposalLocation = (List<DisposalLocation>)Session["oDisposalLocations"];

                    if (disposalLocation.FindAll(x => x.Dl_loc == txtLocation.Text.Trim()).Count > 0)
                    {
                        isExist = true;
                    }

                }
                if (!isExist)
                {
                    DisposalLocation oItem = new DisposalLocation();
                    oItem.Dl_act = 1;
                    oItem.Dl_cre_by = Session["UserID"].ToString();
                    oItem.Dl_cre_dt = DateTime.Now;
                    oItem.Dl_cre_session = Session["SessionID"].ToString();
                    oItem.Dl_doc_no = txtJobNo.Text.Trim();
                    oItem.Dl_loc = txtLocation.Text.ToUpper().Trim();
                    oItem.Dl_loc_Desc = txtLocDesc.Text.Trim();
                    oItem.Dl_seq = 0;


                    disposalLocation.Add(oItem);
                    Session["oDisposalLocations"] = disposalLocation;
                    BindLocation();
                    ClearLocLine();
                }
                else
                {
                    DisplayMessage("Disposal location code already exists!!");
                    txtLocation.Text = "";
                    txtLocDesc.Text = "";
                    txtLocation.Focus();
                }

            }
            else
            {
                DisplayMessage("Enter Disposal location code");
                txtLocation.Focus();
            }
        }

        protected void txtStockAt_TextChanged(object sender, EventArgs e)
        {
            BindStockAtLocation();
            DataTable _tbl = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), txtStockAt.Text.ToUpper().Trim());
            if (_tbl == null || _tbl.Rows.Count <= 0)
            {
                DisplayMessage("Stock At location is invalid. Please check the location.");
                txtStockAt.Text = "";
                txtStockAt.Focus();
                return;
            }
            else
            {
                txtStockAtDesc.Text = _tbl.Rows[0]["ML_LOC_DESC"].ToString();
                // btnAddLocation.Focus();
            }
        }

        protected void lbtnAddStockAt_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStockAt.Text.Trim()))
            {
                List<DispCurrentLocation> stockAtLocation = new List<DispCurrentLocation>();
                bool ifExist = false;
                if (Session["oStockAtLocations"] != null)
                {
                    stockAtLocation = (List<DispCurrentLocation>)Session["oStockAtLocations"];
                    if (stockAtLocation.FindAll(x => x.Idc_loc == txtStockAt.Text.Trim()).Count > 0)
                    {
                        ifExist = true;
                    }
                }
                if (!ifExist)
                {
                    DispCurrentLocation oItem = new DispCurrentLocation();
                    oItem.Idc_act = 1;
                    oItem.Idc_cre_by = Session["UserID"].ToString();
                    oItem.Idc_cre_dt = DateTime.Now;
                    //oItem.Dl_cre_session = Session["SessionID"].ToString();
                    oItem.Idc_doc_no = txtJobNo.Text.Trim();
                    oItem.Idc_loc = txtStockAt.Text.ToUpper().Trim();
                    oItem.Idc_loc_Desc = txtStockAtDesc.Text.Trim();
                    oItem.Idc_seq = 0;


                    stockAtLocation.Add(oItem);
                    Session["oStockAtLocations"] = stockAtLocation;
                    BindStockAtLocation();
                    ClearStockAtLocLine();
                }
                else
                {
                    DisplayMessage("Current location code already exists!!");
                    txtStockAt.Text = "";
                    txtStockAtDesc.Text = "";
                    txtStockAt.Focus();
                }

            }
            else
            {
                DisplayMessage("Enter Stock At location code");
                txtStockAt.Focus();
            }
        }

        protected void lbtnStockAt_Click(object sender, EventArgs e)
        {
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
            DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "stockAtLocation";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpSearchAdance.Show();
            txtSearchbyword.Focus();
        }

        protected void txtStatus_TextChanged(object sender, EventArgs e)
        {
            BindItemStatus();
            DataTable _tbl = CHNLSVC.CommonSearch.GetItmStusByCompany(Session["UserCompanyCode"].ToString());
            bool ifexist = false;

            foreach (DataRow dr in _tbl.Rows)
            {
                if (dr["MIS_DESC"].ToString() == txtStatus.Text.ToUpper())
                {
                    ifexist = true;
                    txtStatusCd.Text = dr["MIS_CD"].ToString();
                    break;
                }
            }

            if (!ifexist)
            {
                DisplayMessage("Item status is invalid. Please check the status.");
                txtStatus.Text = "";
                txtStatus.Focus();
                return;
            }
            else
            {
                //txtStatus.Text = _tbl.Rows[0]["MIS_DESC"].ToString();
                // btnAddLocation.Focus();
            }
        }

        protected void lbtnAddStatus_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStatus.Text.Trim()))
            {
                List<DisposalCurrStatus> itemStatus = new List<DisposalCurrStatus>();
                bool ifExist = false;
                if (Session["oItemStatus"] != null)
                {
                    itemStatus = (List<DisposalCurrStatus>)Session["oItemStatus"];
                    if (itemStatus.FindAll(x => x.Ids_stus == txtStatusCd.Text.Trim()).Count > 0)
                    {
                        ifExist = true;
                    }

                }
                if (!ifExist)
                {
                    DisposalCurrStatus oStatus = new DisposalCurrStatus();
                    oStatus.Ids_act = 1;
                    oStatus.Ids_cre_by = Session["UserID"].ToString(); ;
                    oStatus.Ids_cre_dt = DateTime.Now;
                    oStatus.Ids_doc_no = txtJobNo.Text;
                    oStatus.Ids_stus = txtStatusCd.Text;
                    oStatus.Ids_seq = 0;
                    oStatus.Ids_Stus_desc = txtStatus.Text.ToUpper();

                    itemStatus.Add(oStatus);
                    Session["oItemStatus"] = itemStatus;
                    BindItemStatus();
                    ClearItemStatus();
                }
                else
                {
                    DisplayMessage("Status already exists!!");
                    txtStatus.Text = "";
                    txtStatusCd.Text = "";
                    txtStatus.Focus();
                }
            }
            else
            {
                DisplayMessage("Enter Item Status");
                txtStatus.Focus();
            }
        }

        protected void lbtnStatus_Click(object sender, EventArgs e)
        {
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
            DataTable result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "itemStatus";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpSearchAdance.Show();
            txtSearchbyword.Focus();
        }

        protected void lbnJobNo_Click(object sender, EventArgs e)
        {
            txtFDate.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
            txtTDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            ViewState["SEARCH2"] = null;
            txtSearchbyword2.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DisposalJOb);
            DataTable result = CHNLSVC.CommonSearch.SEARCH_DISPOSAL_JOB(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
            grdResult2.DataSource = result;
            grdResult2.DataBind();
            lblvalue2.Text = "DisposalJOb";
            BindUCtrlDDLData2(result);
            ViewState["SEARCH2"] = result;
            mpSearch.Show();
            txtSearchbyword2.Focus();
        }

        protected void txtJobNo_TextChanged(object sender, EventArgs e)
        {
            DisposalHeader jobEntry = CHNLSVC.Inventory.GET_DISPOSAL_JOB_HEADER(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtJobNo.Text, "ALL");
            if (jobEntry != null)
            {
                oDisposalHeader = jobEntry;
                txtJobAmount.Text = Convert.ToDecimal(jobEntry.Dh_max_val).ToString("N2");
                txtRemark.Text = jobEntry.Dh_rmk.ToString();
                txtReference.Text = jobEntry.Dh_ref_no.ToString();
                textCust.Text = jobEntry.Dh_cus_cd.ToString();
                seqNo.Value = jobEntry.Dh_seq.ToString();
                txtValidFrom.Text = jobEntry.Dh_frm_dt.ToString("dd/MMM/yyyy");
                txtValidTo.Text = jobEntry.Dh_to_dt.ToString("dd/MMM/yyyy");
                txtDate.Text = jobEntry.Dh_doc_dt.ToString("dd/MMM/yyyy");
                ucPayModes1.TotalAmount = Convert.ToDecimal(txtJobAmount.Text);

                decimal paidAmt = 0.0m;
                List<RecieptHeader> recDetList1 = CHNLSVC.Sales.GetRecieptByRefDoc(Session["UserCompanyCode"].ToString(), jobEntry.Dh_doc_no);
                List<RecieptHeader> recDetList = new List<RecieptHeader>();
                if (recDetList1 != null)
                {
                    if (recDetList1.Count > 0)
                    {

                        foreach (RecieptHeader rHead in recDetList1)
                        {
                            paidAmt += rHead.Sar_tot_settle_amt;
                        }
                        if (paidAmt >= Convert.ToDecimal(txtJobAmount.Text))
                        {
                            btnPaymnt.Enabled = false;
                        }
                        else
                        {
                            btnPaymnt.Enabled = true;
                        }
                        recDetList = recDetList1.OrderByDescending(x => x.Sar_create_when).ToList();
                    }
                }
                else
                {
                    btnPaymnt.Enabled = true;
                }
                grdPymntDet.DataSource = recDetList;
                grdPymntDet.DataBind();

                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16059))
                {
                    txtJobAmount.ReadOnly = true;
                }
                else
                {
                    txtJobAmount.ReadOnly = false;
                }

                List<DisposalLocation> oDispLoc = CHNLSVC.Inventory.GET_DISPOSAL_LOC_LIST(jobEntry.Dh_seq, jobEntry.Dh_doc_no);
                Session["oDisposalLocations"] = oDispLoc;
                dgvLocations.DataSource = oDispLoc;
                dgvLocations.DataBind();

                List<DispCurrentLocation> oCurrLoc = CHNLSVC.Inventory.GET_DISPOSAL_CURR_LOC_LIST(jobEntry.Dh_seq, jobEntry.Dh_doc_no);
                Session["oStockAtLocations"] = oCurrLoc;
                dgvStockAt.DataSource = oCurrLoc;
                dgvStockAt.DataBind();

                List<DisposalCurrStatus> oCurrStus = CHNLSVC.Inventory.GET_DISPOSAL_CURR_STUS_LIST(jobEntry.Dh_seq, jobEntry.Dh_doc_no);
                Session["oItemStatus"] = oCurrStus;
                dgvStatus.DataSource = oCurrStus;
                dgvStatus.DataBind();

                btnSaveDispItm.Enabled = true;

                List<DisposalItem> oDispItems = CHNLSVC.Inventory.GET_DISPOSAL_ITM_LIST_WEB(jobEntry.Dh_seq, jobEntry.Dh_doc_no, true);
                Session["oItem"] = oDispItems;

                List<ReptPickSerials> scanSerList = new List<ReptPickSerials>();
                scanSerList = CHNLSVC.Sales.GetSerialByBaseDoc2(Session["UserCompanyCode"].ToString(), jobEntry.Dh_doc_no);

                List<string> ScannedSerials = new List<string>();
                List<string> ItemSerials = new List<string>();


                if (oDispItems != null)
                {
                    if (oDispItems.Count > 0)
                    {
                        foreach (DisposalItem itmSer in oDispItems)
                        {
                            ItemSerials.Add(itmSer.Idd_ser_1);
                        }
                    }
                }

                if (scanSerList != null)
                {
                    if (scanSerList.Count > 0)
                    {
                        foreach (ReptPickSerials scanSer in scanSerList)
                        {
                            ScannedSerials.Add(scanSer.Tus_ser_1);
                        }

                        List<string> result = new List<string>();
                        result = ScannedSerials.Except(ItemSerials).ToList();

                        if (result.Count > 0)
                        {

                            DisplayMessage("Scanned serials not allowed for disposal!!!");
                            return;

                        }
                    }
                }


                if (scanSerList != null)
                {
                    if (scanSerList.Count > 0)
                    {
                        foreach (DisposalItem itm in oDispItems)
                        {
                            List<ReptPickSerials> itmSerList = new List<ReptPickSerials>();
                            MasterItem mstItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Idd_itm_cd);

                            if (mstItm.Mi_is_ser1 == 1)
                            {
                                itmSerList = scanSerList.Where(x => x.Tus_itm_cd == itm.Idd_itm_cd && x.Tus_itm_stus == itm.Idd_stus && x.Tus_qty == itm.Idd_qty && x.Tus_ser_id == itm.Idd_ser_id && x.Tus_ser_1 == itm.Idd_ser_1 && x.Tus_loc == itm.Idd_cur_loc).ToList();
                            }
                            else
                            {
                                itmSerList = scanSerList.Where(x => x.Tus_itm_cd == itm.Idd_itm_cd && x.Tus_itm_stus == itm.Idd_stus && x.Tus_qty <= itm.Idd_qty && x.Tus_loc == itm.Idd_cur_loc).ToList();
                            }
                            if (itmSerList != null)
                            {
                                if (itmSerList.Count == 1)
                                {
                                    itm.Idd_scan_stus = 1;
                                    itm.Idd_scan_qty = scanSerList.Where(x => x.Tus_itm_cd == itm.Idd_itm_cd && x.Tus_itm_stus == itm.Idd_stus && x.Tus_qty <= itm.Idd_qty && x.Tus_loc == itm.Idd_cur_loc).FirstOrDefault().Tus_qty;
                                }
                            }
                        }
                    }
                }

                if (oDispItems != null)
                {
                    grdDspItms.DataSource = oDispItems;
                    if (oDispItems.FirstOrDefault().Idd_stus == "DISPO")
                    {
                        btnChngStus.Enabled = false;
                    }
                    else
                    {
                        btnChngStus.Enabled = true;
                    }
                }
                else
                {
                    grdDspItms.DataSource = new int[] { };
                }
                grdDspItms.DataBind();

                if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16057))
                {
                    grdDspItms.Columns[7].Visible = true;
                }
                else
                {
                    grdDspItms.Columns[7].Visible = false;
                }

                if (!string.IsNullOrEmpty(txtJobNo.Text))
                {
                    btnJobCreate.Enabled = false;
                    btnUpdate.Enabled = true;
                }
                if (jobEntry.Dh_stus == "A")
                {
                    txtJobAmount.ReadOnly = true;
                    txtRemark.ReadOnly = true;
                    txtReference.ReadOnly = true;
                    textCust.ReadOnly = true;
                    txtValidFrom.ReadOnly = true;
                    txtValidTo.ReadOnly = true;

                    btnChngStus.Enabled = false;
                    btnSaveDispItm.Enabled = false;
                    btnUpdate.Enabled = false;
                    btnUpdate.OnClientClick = null;
                    btnUpdate.OnClientClick = null;
                    btnUpdate.ForeColor = Color.Gray;
                    //btnCheckCost.Enabled = false;
                    btnApprove.Enabled = false;
                    btnApprove.OnClientClick = null;
                    btnApprove.ForeColor = Color.Gray;
                    btnDispose.Enabled = true;
                    btnDispose.ForeColor = Color.Purple;
                    btnDispose.OnClientClick = "return confDispose()";

                    //txtValidFromCal.TargetControlID = "";
                    //txtValidToCal.TargetControlID = "";
                    grdDspItms.Columns[0].Visible = false;
                    dgvLocations.Columns[2].Visible = false;
                    dgvStockAt.Columns[2].Visible = false;
                    dgvStatus.Columns[0].Visible = false;

                }

                else if (jobEntry.Dh_stus == "F")
                {
                    txtJobAmount.ReadOnly = true;
                    txtRemark.ReadOnly = true;
                    txtReference.ReadOnly = true;
                    textCust.ReadOnly = true;
                    txtValidFrom.ReadOnly = true;
                    txtValidTo.ReadOnly = true;

                    btnChngStus.Enabled = false;
                    btnSaveDispItm.Enabled = false;
                    btnUpdate.Enabled = false;
                    btnUpdate.OnClientClick = null;
                    btnUpdate.ForeColor = Color.Gray;
                    btnCheckCost.Enabled = false;
                    btnApprove.Enabled = false;
                    btnApprove.OnClientClick = null;
                    btnApprove.ForeColor = Color.Gray;
                    btnDispose.Enabled = false;
                    btnDispose.OnClientClick = null;
                    btnDispose.ForeColor = Color.Gray;
                    btnPaymnt.Enabled = false;

                    //txtValidFromCal.TargetControlID = "";
                    //txtValidToCal.TargetControlID = "";
                    grdDspItms.Columns[0].Visible = false;
                    dgvLocations.Columns[2].Visible = false;
                    dgvStockAt.Columns[2].Visible = false;
                    dgvStatus.Columns[0].Visible = false;
                }

                else
                {
                    txtJobAmount.ReadOnly = false;
                    txtRemark.ReadOnly = false;
                    txtReference.ReadOnly = false;
                    textCust.ReadOnly = false;
                    txtValidFrom.ReadOnly = false;
                    txtValidTo.ReadOnly = false;

                    btnChngStus.Enabled = true;
                    btnSaveDispItm.Enabled = true;
                    btnUpdate.Enabled = true;
                    btnUpdate.OnClientClick = "return confUpdate()";
                    btnUpdate.ForeColor = Color.Purple;
                    btnCheckCost.Enabled = true;
                    btnApprove.Enabled = true;
                    btnApprove.OnClientClick = "return confApprove()";
                    btnApprove.ForeColor = Color.Purple;
                    btnDispose.Enabled = true;
                    btnDispose.OnClientClick = "return confDispose()";
                    btnDispose.ForeColor = Color.Purple;
                    btnPaymnt.Enabled = true;

                    //txtValidFromCal.TargetControlID = "";
                    //txtValidToCal.TargetControlID = "";
                    grdDspItms.Columns[0].Visible = true;
                    dgvLocations.Columns[2].Visible = true;
                    dgvStockAt.Columns[2].Visible = true;
                    dgvStatus.Columns[0].Visible = true;
                }
            }
            else
            {
                DisplayMessage("Job no is invalid. Please check the Job No");
                txtJobNo.Text = "";
                txtJobNo.Focus();
                btnJobCreate.Enabled = true;
                btnUpdate.Enabled = false;
                btnUpdate.OnClientClick = null;
                return;
                //txtLocDesc.Text = _tbl.Rows[0]["ML_LOC_DESC"].ToString();
                // btnAddLocation.Focus();
            }
        }

        protected void lbtnItemCode_Click(object sender, EventArgs e)
        {
            Session["SP"] = "show";
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = string.Empty;
            DataTable result = new DataTable();
            SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
            lblvalue.Text = "itemSearch";
            grdResult.DataSource = result;
            grdResult.DataBind();
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpSearchAdance.Show();
            txtSearchbyword.Focus();
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void chkOnlyQty_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCode.Text.Trim()))
            {
                MasterItem item = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItemCode.Text);
                if (item != null)
                {
                    lblDescription.Text = item.Mi_longdesc;
                    lblModel.Text = item.Mi_model;
                    lblBrand.Text = item.Mi_brand;
                    lblPart.Text = item.Mi_part_no;
                    if (item.Mi_is_ser1 == 0)
                    {
                        txtSerialI.Text = "N/A";
                        txtSerialI.ReadOnly = true;
                        txtQty.Text = "";
                        txtQty.ReadOnly = false;
                        BindUserCompanyItemStatusDDLData(ddlStatus);
                        txtItmcurLoc.Text = "";
                        txtItmcurLoc.ReadOnly = false;
                    }
                    else
                    {
                        txtQty.Text = "1";
                        txtQty.ReadOnly = true;
                        BindUserCompanyItemStatusDDLData(ddlStatus);
                    }
                    if (item.Mi_is_ser2 == 0)
                    {
                        txtSerialII.ReadOnly = true;
                    }
                    if (!item.Mi_is_ser3)
                    {
                        txtSerialIII.ReadOnly = true;
                    }
                }
                else
                {
                    DisplayMessage("Item code is invalid. Please check the item.");
                    txtItemCode.Text = "";
                    txtItemCode.Focus();
                    return;
                }
            }
            else
            {
                DisplayMessage("Please enter an item code");
                txtItemCode.Text = "";
                txtItemCode.Focus();
                return;
            }
        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            string doc_tp = "DISP";
            string seqNo = "0";

            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                DisposalHeader jobEntry = CHNLSVC.Inventory.GET_DISPOSAL_JOB_HEADER(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtJobNo.Text, "ALL");
                if (jobEntry != null)
                {
                    if (jobEntry.Dh_stus == "A")
                    {
                        DisplayMessage("Cannot add items to approved job.!!");
                        Item_Clear();
                        return;
                    }
                }
            }

            MasterLocation _mstLocation = CHNLSVC.General.GetLocationInfor(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
            if (_mstLocation != null)
            {
                if (!_mstLocation.Ml_is_serial)
                {
                    int _usrSeq = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(doc_tp, Session["UserCompanyCode"].ToString(), seqNo, 0);
                    if (_usrSeq != -1)
                    {
                        ReptPickHeader _ReptPickHeader = CHNLSVC.Inventory.GetAllScanSerialParameters(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), _usrSeq, doc_tp);
                        if (_ReptPickHeader != null)
                        {
                            if (!string.IsNullOrEmpty(_ReptPickHeader.Tuh_wh_com) && !string.IsNullOrEmpty(_ReptPickHeader.Tuh_wh_loc) && !string.IsNullOrEmpty(_ReptPickHeader.Tuh_load_bay))
                            {
                                DisplayMessage("Serials are already picked from PDA !");
                                return;
                            }
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(txtItemCode.Text))
            {
                DisplayMessage("Please select the item code");
                return;
            }

            List<DisposalCurrStatus> allowdStus = new List<DisposalCurrStatus>();
            allowdStus = (List<DisposalCurrStatus>)Session["oItemStatus"];

            List<DisposalCurrStatus> statusList = allowdStus.Where(x => x.Ids_Stus_desc == ddlStatus.SelectedItem.ToString()).ToList();

            if (statusList.Count <= 0)
            {
                DisplayMessage("Status " + ddlStatus.SelectedItem.ToString() + " not allowed for disposal");
                return;
            }

            MasterItem _itms = new MasterItem();
            _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItemCode.Text.ToUpper().Trim());

            if (_itms.Mi_is_ser1 == 1)
            {
                if (string.IsNullOrEmpty(txtSerialI.Text.ToString()) && !chkOnlyQty.Checked)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the serial number…!')", true);
                    txtSerialI.Focus();
                    return;
                }

            }

            if (string.IsNullOrEmpty(txtItemCode.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select the item...!');", true);
                txtItemCode.Focus();
                return;
            }
            //if (string.IsNullOrEmpty(ddlStatus.Text.ToString()))
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select the status...!');", true);
            //    ddlStatus.Focus();
            //    return;
            //}
            if (string.IsNullOrEmpty(txtQty.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select the quantity...!');", true);
                txtQty.Focus();
                return;
            }
            AddItemGrid(txtItemCode.Text, txtSerialI.Text, txtItmDispLoc.Text.ToUpper(), ddlStatus.SelectedItem.ToString(), txtItmcurLoc.Text.ToUpper(), txtQty.Text);
        }

        protected void AddItemGrid(string item, string serial, string itmDispLoc, string stusDesc, string Scurloc, string DQty, bool _chksts = false)
        {
            MasterItem msitem = new MasterItem();
            List<ReptPickSerials> TempItmDet_list = new List<ReptPickSerials>();
            List<DisposalItem> _dispItmList = new List<DisposalItem>();
            List<ReptPickSerials> _tempDispListx = new List<ReptPickSerials>();
            _tempDispListx = (List<ReptPickSerials>)ViewState["SerialList"];
            MasterItem msitem2 = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), item);

            string stus = string.Empty;
            if (!_chksts)
            {
                DataTable _tbl = CHNLSVC.Inventory.GetAllCompanyStatus(Session["UserCompanyCode"].ToString());
                var filtered = _tbl.AsEnumerable().Where(r => r.Field<String>("MIS_DESC") == stusDesc);
                stus = filtered.FirstOrDefault().Field<String>("MIC_CD");
            }
            else
            {
                stus = stusDesc;
            }

            if (Session["oItem"] != null)
            {
                _dispItmList = (List<DisposalItem>)Session["oItem"];
            }

            if (!string.IsNullOrEmpty(itmDispLoc))
            {
                if (msitem2.Mi_is_ser1 == 1)
                {
                    var _serAva = _dispItmList.Where(c => c.Idd_itm_cd == item && c.Idd_ser_1 == serial).FirstOrDefault();
                    if (_serAva != null)
                    {
                        DisplayMessage("This searial is already in the disposal list!!");
                        Item_Clear();
                        return;
                    }
                    //foreach (DisposalItem itm in _dispItmList)
                    //{
                    //    if (itm.Idd_itm_cd == item && itm.Idd_ser_1 == serial)
                    //    {
                    //        DisplayMessage("This searial is already in the disposal list!!");
                    //        Item_Clear();
                    //        return;
                    //    }
                    //}
                }



                List<DispCurrentLocation> _currLocList = new List<DispCurrentLocation>();
                _currLocList = (List<DispCurrentLocation>)Session["oStockAtLocations"];

                msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), item);

                if (msitem.Mi_is_ser1 == 1)
                {
                    #region is serialized item
                    TempItmDet_list = new List<ReptPickSerials>();
                    foreach (DispCurrentLocation _currLoc in _currLocList)
                    {
                        List<ReptPickSerials> _tempSer = CHNLSVC.Inventory.Search_by_serial(Session["UserCompanyCode"].ToString(), _currLoc.Idc_loc, item, string.Empty, serial, string.Empty);
                        TempItmDet_list.AddRange(_tempSer);
                    }


                    TempItmDet_list = TempItmDet_list.Where(x => x.Tus_ser_1 == serial).ToList();
                    if (TempItmDet_list.Count > 0)
                    {
                        if (_tempDispListx != null)
                        {
                            if (_tempDispListx.Where(x => x.Tus_itm_cd == TempItmDet_list.FirstOrDefault().Tus_itm_cd && x.Tus_itm_stus == TempItmDet_list.FirstOrDefault().Tus_itm_stus && x.Tus_loc == TempItmDet_list.FirstOrDefault().Tus_loc && x.Tus_ser_1 == TempItmDet_list.FirstOrDefault().Tus_ser_1).ToList().Count > 0)
                            {
                                DisplayMessage("This Serial is already added");
                                Item_Clear();
                                txtSerialI.Focus();
                                return;
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region is non serialized item
                    TempItmDet_list = new List<ReptPickSerials>();
                    List<ReptPickSerials> _tempSer = CHNLSVC.Inventory.GetSerialDataForDisposalEntry(Session["UserCompanyCode"].ToString(), Scurloc, item, stus, "N/A", "", 0);
                    List<ReptPickSerials> _tempSer2 = new List<ReptPickSerials>();
                    _tempSer.ToList().ForEach(c => c.Tus_ser_id = 0);
                    _tempSer2 = _tempSer.Where(x => x.Tus_itm_stus == stus).ToList();

                    List<DisposalItem> nonSerDspItm = _dispItmList.Where(x => x.Idd_itm_cd == item && x.Idd_stus_desc == stusDesc && x.Idd_cur_loc == Scurloc).ToList();
                    if (nonSerDspItm != null)
                    {
                        if (nonSerDspItm.Count > 0)
                        {
                            //if ((_tempSer2.Count - nonSerDspItm.FirstOrDefault().Idd_qty) >= Convert.ToInt32(DQty))
                            //{
                            //    nonSerDspItm.FirstOrDefault().Idd_qty += Convert.ToDecimal(DQty);
                            //    _dispItmList.Where(x => x.Idd_itm_cd == item && x.Idd_stus_desc == stusDesc && x.Idd_cur_loc == Scurloc).FirstOrDefault().Idd_qty = nonSerDspItm.FirstOrDefault().Idd_qty;
                            //    _tempSer2.FirstOrDefault().Tus_qty = Convert.ToDecimal(nonSerDspItm.FirstOrDefault().Idd_qty);
                            //    TempItmDet_list.Add(_tempSer2.FirstOrDefault());
                            //    //TempItmDet_list.AddRange(_tempSer.Take(Convert.ToInt32(nonSerDspItm.FirstOrDefault().Idd_qty)));


                            //}
                            //else
                            //{
                            //    DisplayMessage("Not enough quantity to dispose");
                            //    return;
                            //}
                            nonSerDspItm.FirstOrDefault().Idd_qty += Convert.ToDecimal(DQty);
                            _dispItmList.Where(x => x.Idd_itm_cd == item && x.Idd_stus_desc == stusDesc && x.Idd_cur_loc == Scurloc).FirstOrDefault().Idd_qty = nonSerDspItm.FirstOrDefault().Idd_qty;
                            _tempSer2.FirstOrDefault().Tus_qty = Convert.ToDecimal(nonSerDspItm.FirstOrDefault().Idd_qty);
                            TempItmDet_list.Add(_tempSer2.FirstOrDefault());

                        }
                        else
                        {
                            if (_tempSer2.Count > 0)
                            {
                                InventoryLocation _invBal = CHNLSVC.Inventory.GET_INR_LOC_BALANCE(new InventoryLocation()
                                {
                                    Inl_com = Session["UserCompanyCode"].ToString(),
                                    Inl_loc = Scurloc,
                                    Inl_itm_cd = item,
                                    Inl_itm_stus = stus
                                });
                                decimal _avaQty = 0;
                                if (_invBal != null)
                                {
                                    _avaQty = _invBal.Inl_free_qty;
                                }
                                if (_avaQty < Convert.ToDecimal(DQty))
                                {
                                    DisplayMessage("Not enough quantity to dispose");
                                    return;
                                }
                                else
                                {
                                    if (_tempSer2[0] != null)
                                    {
                                        _tempSer2[0].Tus_qty = Convert.ToDecimal(DQty);
                                        _tempSer2[0].Tus_seq_no = 0;
                                        _tempSer2[0].Tus_itm_line = 0;
                                        _tempSer2[0].Tus_batch_line = 0;
                                        _tempSer2[0].Tus_ser_line = 0;
                                        _tempSer2[0].Tus_ser_id = 0;
                                        TempItmDet_list.Add(_tempSer2[0]);
                                    }
                                    else
                                    {
                                        DisplayMessage("Not enough quantity to dispose");
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                DisplayMessage("No qty in given status");
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (_tempSer2.Count > 0)
                        {
                            if (_tempSer2.Count > Convert.ToDecimal(DQty))
                            {
                                _tempSer2.FirstOrDefault().Tus_qty = Convert.ToDecimal(DQty);
                                TempItmDet_list.Add(_tempSer2.FirstOrDefault());
                                //TempItmDet_list.AddRange(_tempSer.Take(Convert.ToDecimal(DQty)));
                            }

                            else
                            {
                                DisplayMessage("Not enough quantity to dispose");
                                return;
                            }

                        }
                    }
                    #endregion
                }
                if (TempItmDet_list.Count == 1)
                {
                    #region MyRegion
                    TempItmDet_list.First().Tus_ageloc = itmDispLoc;
                    TempItmDet_list.First().Tus_itm_model = msitem.Mi_model;
                    TempItmDet_list.First().Tus_itm_brand = msitem.Mi_brand;
                    TempItmDet_list.First().Tus_itm_stus_Desc = stusDesc;
                    TempItmDet_list.First().Tus_qty = Convert.ToDecimal(DQty);
                    //GridView grid3 = (GridView)up2.FindControl("grdDspItms");
                    //grd3.
                    //grid3.DataBind();
                    List<ReptPickSerials> _tempDispList = new List<ReptPickSerials>();
                    _tempDispList = (List<ReptPickSerials>)ViewState["SerialList"];



                    if (_tempDispList != null)
                    {
                        _tempDispList.AddRange(TempItmDet_list);
                    }

                    List<DisposalItem> nonSerExist = new List<DisposalItem>();
                    if (msitem.Mi_is_ser1 == 0)
                    {
                        nonSerExist = _dispItmList.Where(x => x.Idd_itm_cd == item && x.Idd_stus_desc == stusDesc && x.Idd_cur_loc == Scurloc).ToList();
                    }


                    if (nonSerExist != null)
                    {
                        if (nonSerExist.Count > 0)
                        {

                        }
                        else
                        {
                            int i = 0;
                            List<DisposalItem> DspItm = new List<DisposalItem>();
                            DspItm = CHNLSVC.Inventory.GET_DISPOSAL_ITM_LIST(Convert.ToInt32(seqNo.Value), txtJobNo.Text, false);
                            if (DspItm != null)
                            {
                                i = CHNLSVC.Inventory.GET_DISPOSALITM_LINE(Convert.ToInt32(seqNo.Value)) + 1;
                            }
                            else if (_dispItmList.Count > 0)
                            {
                                i = _dispItmList.Count + 1;
                            }
                            else
                            {
                                i = 1;
                            }

                            List<DisposalItem> DspItm2 = new List<DisposalItem>();
                            foreach (ReptPickSerials ser in TempItmDet_list)
                            {
                                DisposalItem dispItm = new DisposalItem();
                                if (string.IsNullOrEmpty(seqNo.Value))
                                {
                                    dispItm.Idd_seq = 0;
                                }
                                else
                                {
                                    dispItm.Idd_seq = Convert.ToInt32(seqNo.Value);
                                }

                                dispItm.Idd_act = 1;
                                dispItm.Idd_cre_by = Session["UserID"].ToString();
                                dispItm.Idd_cur_loc = ser.Tus_loc;
                                dispItm.Idd_cre_dt = DateTime.Now;
                                dispItm.Idd_disp_loc = ser.Tus_ageloc;
                                dispItm.Idd_itm_brand = ser.Tus_itm_brand;
                                dispItm.Idd_itm_cd = ser.Tus_itm_cd;
                                dispItm.Idd_itm_model = ser.Tus_itm_model;
                                dispItm.Idd_job_no = txtJobNo.Text;
                                dispItm.Idd_line = i;
                                dispItm.Idd_mod_by = Session["UserID"].ToString();
                                dispItm.Idd_mod_dt = DateTime.Now;
                                dispItm.Idd_ser_1 = ser.Tus_ser_1;
                                dispItm.Idd_ser_2 = ser.Tus_ser_2;
                                dispItm.Idd_ser_id = ser.Tus_ser_id;
                                if (ser.Tus_new_status.ToString() != "1")
                                {
                                    dispItm.Idd_stus = ser.Tus_new_status;
                                    dispItm.Idd_stus_desc = ser.Tus_new_status_Desc;
                                }
                                else
                                {
                                    dispItm.Idd_stus = ser.Tus_itm_stus;
                                    dispItm.Idd_stus_desc = ser.Tus_itm_stus_Desc;
                                }
                                dispItm.Idd_unit_cost = ser.Tus_unit_cost.ToString();
                                dispItm.Idd_qty = ser.Tus_qty;
                                dispItm.Idd_bin_cd = ser.Tus_bin;
                                dispItm.Idd_base_Seq = ser.Tus_seq_no;

                                DspItm2.Add(dispItm);
                                i++;

                            }

                            _dispItmList.AddRange(DspItm2);
                        }
                    }

                    else
                    {
                        int i = 0;
                        List<DisposalItem> DspItm = new List<DisposalItem>();
                        DspItm = CHNLSVC.Inventory.GET_DISPOSAL_ITM_LIST(Convert.ToInt32(seqNo.Value), txtJobNo.Text, false);
                        if (DspItm != null)
                        {
                            i = CHNLSVC.Inventory.GET_DISPOSALITM_LINE(Convert.ToInt32(seqNo.Value)) + 1;
                        }
                        else if (_dispItmList.Count > 0)
                        {
                            i = _dispItmList.Count + 1;
                        }
                        else
                        {
                            i = 1;
                        }

                        List<DisposalItem> DspItm2 = new List<DisposalItem>();
                        foreach (ReptPickSerials ser in TempItmDet_list)
                        {
                            DisposalItem dispItm = new DisposalItem();
                            if (string.IsNullOrEmpty(seqNo.Value))
                            {
                                dispItm.Idd_seq = 0;
                            }
                            else
                            {
                                dispItm.Idd_seq = Convert.ToInt32(seqNo.Value);
                            }

                            dispItm.Idd_act = 1;
                            dispItm.Idd_cre_by = Session["UserID"].ToString();
                            dispItm.Idd_cur_loc = ser.Tus_loc;
                            dispItm.Idd_cre_dt = DateTime.Now;
                            dispItm.Idd_disp_loc = ser.Tus_ageloc;
                            dispItm.Idd_itm_brand = ser.Tus_itm_brand;
                            dispItm.Idd_itm_cd = ser.Tus_itm_cd;
                            dispItm.Idd_itm_model = ser.Tus_itm_model;
                            dispItm.Idd_job_no = txtJobNo.Text;
                            dispItm.Idd_line = i;
                            dispItm.Idd_mod_by = Session["UserID"].ToString();
                            dispItm.Idd_mod_dt = DateTime.Now;
                            dispItm.Idd_ser_1 = ser.Tus_ser_1;
                            dispItm.Idd_ser_2 = ser.Tus_ser_2;
                            dispItm.Idd_ser_id = ser.Tus_ser_id;
                            if (ser.Tus_new_status.ToString() != "1")
                            {
                                dispItm.Idd_stus = ser.Tus_new_status;
                                dispItm.Idd_stus_desc = ser.Tus_new_status_Desc;
                            }
                            else
                            {
                                dispItm.Idd_stus = ser.Tus_itm_stus;
                                dispItm.Idd_stus_desc = ser.Tus_itm_stus_Desc;
                            }
                            dispItm.Idd_unit_cost = ser.Tus_unit_cost.ToString();
                            dispItm.Idd_qty = ser.Tus_qty;
                            dispItm.Idd_bin_cd = ser.Tus_bin;
                            dispItm.Idd_base_Seq = ser.Tus_seq_no;

                            DspItm2.Add(dispItm);
                            i++;

                        }

                        _dispItmList.AddRange(DspItm2);
                    }
                    #endregion
                    if (_dispItmList != null)
                    {
                        if (_dispItmList.Count < 1000)
                        {
                            grdDspItms.DataSource = _dispItmList;
                            grdDspItms.DataBind();
                            //grdDspItms.Columns[7].Visible = true;
                            //grdDspItms.Columns[8].Visible = false;
                            if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16057))
                            {
                                grdDspItms.Columns[7].Visible = true;
                            }
                            else
                            {
                                grdDspItms.Columns[7].Visible = false;
                            }
                        }
                    }
                    Session["oItem"] = _dispItmList;
                    ViewState["SerialList"] = _tempDispList;
                    Item_Clear();
                    return;
                }
            }
            else
            {
                DisplayMessage("Enter a disposal location!!");
                txtItmDispLoc.Text = "";
                txtItmDispLoc.Focus();
            }

        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            if (hdfSaveCon.Value == "No")
            {
                return;
            }
            Item_Clear();
        }

        protected void grdLbtnDltItm_Click(object sender, EventArgs e)
        {
            if (hdfSaveCon.Value == "No")
            {
                return;
            }
            bool success = false;

            if (grdDspItms.Rows.Count == 0) return;

            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;

            List<ReptPickSerials> _serList = (List<ReptPickSerials>)ViewState["SerialList"];

            if (row != null)
            {
                string itmCd = (row.FindControl("grdLblItm") as Label).Text;
                string itmser1 = (row.FindControl("grdLblSerilaI") as Label).Text;
                string itmStus = (row.FindControl("grdLblStus") as Label).Text;
                string itmSerID = (row.FindControl("grdLblSerilaID") as Label).Text;
                if (oItem.FindAll(x => x.Idd_itm_cd == itmCd.Trim() && x.Idd_ser_id == Convert.ToInt32(itmSerID.Trim()) && x.Idd_ser_1 == itmser1.Trim() && x.Idd_stus_desc == itmStus.Trim()).Count > 0)
                {
                    oItem.RemoveAll(x => x.Idd_itm_cd == itmCd.Trim() && x.Idd_ser_id == Convert.ToInt32(itmSerID.Trim()) && x.Idd_ser_1 == itmser1.Trim() && x.Idd_stus_desc == itmStus.Trim());
                    //_serList.RemoveAll(x => x.Tus_itm_cd == itmCd.Trim() && x.Tus_ser_id== Convert.ToInt32(itmSerID.Trim()) && x.Tus_ser_1 == itmser1.Trim() && x.Tus_itm_stus_Desc == itmStus.Trim());
                    Session["oItem"] = oItem;
                    ViewState["SerialList"] = _serList;
                    grdDspItms.DataSource = oItem;
                    grdDspItms.DataBind();
                    if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16057))
                    {
                        grdDspItms.Columns[7].Visible = true;
                    }
                    else
                    {
                        grdDspItms.Columns[7].Visible = false;
                    }
                    success = true;
                }
            }
            if (success)
            {
                DisplayMessage("Disposal item removed successfully. Plese update the job to make the removal permenant", 3);
            }
        }

        protected void lbtnItmDispLoc_Click(object sender, EventArgs e)
        {
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
            DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "itmDispLocation";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpSearchAdance.Show();
            txtSearchbyword.Focus();
        }

        protected void lbtnSerialI_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItemCode.Text))
                Session["_SerialSearchType"] = "SER1_WOITEM";
            else
                Session["_SerialSearchType"] = "SER1_WITEM";

            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
            DataTable result = CHNLSVC.CommonSearch.Search_inr_ser_infor(SearchParams, null, null);
            int count = result.Rows.Count;
            for (int x = count - 1; x >= 500; x--)
            {
                DataRow dr = result.Rows[x];
                dr.Delete();
            }
            result.AcceptChanges();
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "itmSerialI";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpSearchAdance.Show();
            txtSearchbyword.Focus();
        }

        protected void txtItmDispLoc_TextChanged(object sender, EventArgs e)
        {
            List<DisposalLocation> oDispLoc = (List<DisposalLocation>)Session["oDisposalLocations"];
            bool isDispLoc = false;

            foreach (DisposalLocation dispLoc in oDispLoc)
            {
                if (dispLoc.Dl_loc == txtItmDispLoc.Text.ToUpper())
                {
                    isDispLoc = true;
                    break;
                }
            }

            if (isDispLoc)
            {
                DataTable _tbl = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), txtItmDispLoc.Text.ToUpper().Trim());
                if (_tbl == null || _tbl.Rows.Count <= 0)
                {
                    DisplayMessage("Item dispatch location is invalid. Please check the location.");
                    txtItmDispLoc.Text = "";
                    txtItmDispLoc.Focus();
                    return;
                }
                else
                {
                    lblItmDispLoc.Text = _tbl.Rows[0]["ML_LOC_DESC"].ToString();
                    // btnAddLocation.Focus();
                }
            }
            else
            {
                DisplayMessage("Not an allowed Disposal Location");
                txtItmDispLoc.Text = "";
                txtItmDispLoc.Focus();
                return;
            }
        }

        protected void txtSerialI_TextChanged(object sender, EventArgs e)
        {

            string seqNo = "0";
            string doc_tp = "DISP";
            MasterLocation _mstLocation = CHNLSVC.General.GetLocationInfor(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
            if (_mstLocation != null)
            {
                if (!_mstLocation.Ml_is_serial)
                {
                    int _usrSeq = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(doc_tp, Session["UserCompanyCode"].ToString(), seqNo, 0);
                    if (_usrSeq != -1)
                    {
                        ReptPickHeader _ReptPickHeader = CHNLSVC.Inventory.GetAllScanSerialParameters(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), _usrSeq, doc_tp);
                        if (_ReptPickHeader != null)
                        {
                            if (!string.IsNullOrEmpty(_ReptPickHeader.Tuh_wh_com) && !string.IsNullOrEmpty(_ReptPickHeader.Tuh_wh_loc) && !string.IsNullOrEmpty(_ReptPickHeader.Tuh_load_bay))
                            {
                                DisplayMessage("Serials are already picked from PDA !");
                                return;
                            }
                        }
                    }
                }
            }

            string itemCode = txtItemCode.Text.ToUpper();

            List<DispCurrentLocation> oCurrLoc = new List<DispCurrentLocation>();
            oCurrLoc = (List<DispCurrentLocation>)Session["oStockAtLocations"];
            List<DisposalLocation> oDispLoc = new List<DisposalLocation>();
            oDispLoc = (List<DisposalLocation>)Session["oDisposalLocations"];
            List<DisposalCurrStatus> oStus = new List<DisposalCurrStatus>();
            oStus = (List<DisposalCurrStatus>)Session["oItemStatus"];

            if (oCurrLoc == null)
            {
                DisplayMessage("Enter stock at locations!!!");
                txtStockAt.Text = "";
                txtStockAt.Focus();
                return;
            }

            if (oDispLoc == null)
            {
                DisplayMessage("Enter disposal locations!!!");
                txtLocation.Text = "";
                txtLocation.Focus();
                return;
            }

            if (oStus == null)
            {
                DisplayMessage("Enter allowed item status!!!");
                txtStatus.Text = "";
                txtStatus.Focus();
                return;
            }

            List<InventorySerialN> _invSerList = new List<InventorySerialN>();

            foreach (DispCurrentLocation loc in oCurrLoc)
            {
                List<InventorySerialN> _serList = CHNLSVC.Inventory.Get_INR_SER_DATA(new InventorySerialN()
                {
                    Ins_com = Session["UserCompanyCode"].ToString(),
                    Ins_loc = loc.Idc_loc,
                    Ins_itm_cd = string.IsNullOrEmpty(itemCode) ? null : itemCode,
                    Ins_ser_1 = txtSerialI.Text.Trim(),
                    Ins_available = -2
                }).ToList();

                _invSerList.AddRange(_serList);
            }

            if (_invSerList.Count < 1)
            {
                DisplayMessage("Serial location is not allowed for Disposal", 2);
                txtSerialI.Text = "";
                txtSerialI.Focus();
                return;
            }



            var stusFiltered = _invSerList
                   .Where(x => oStus.Any(y => y.Ids_stus == x.Ins_itm_stus));

            List<InventorySerialN> _invSerList2 = stusFiltered.AsEnumerable().ToList();

            if (_invSerList2.Count < 1)
            {
                DisplayMessage("Serial status not allowed for Disposal");
                return;
            }



            //var curLocFiltered = _invSerList2
            //    .Where(x => oCurrLoc.Any(y => y.Idc_loc == x.Ins_loc));

            //List<InventorySerialN> _invSerList3 = curLocFiltered.AsEnumerable().ToList();

            //if (_invSerList3.Count < 1)
            //{
            //    DisplayMessage("Serial location is not allowed for Disposal");
            //    return;
            //}

            bool _serPick = false;
            bool _serNotAva = false;
            if (_invSerList == null)
            {
                DisplayMessage("This serial number is not available! Please check again.! "); txtSerialI.Text = ""; return;
            }
            if (_invSerList2 != null)
            {
                foreach (var item in _invSerList2)
                {
                    if (item.Ins_available == -1)
                    {
                        _serPick = true;
                        break;
                    }
                }
            }
            if (_invSerList2.Count == 1)
            {
                MasterItem item = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _invSerList2.First().Ins_itm_cd);
                if (item != null)
                {
                    // ddlStatus.Text = _invSerList.First().Ins_itm_stus;
                    txtItemCode.Text = item.Mi_cd;
                    lblDescription.Text = item.Mi_longdesc;
                    lblModel.Text = item.Mi_model;
                    lblBrand.Text = item.Mi_brand;
                    lblPart.Text = item.Mi_part_no;
                    if (item.Mi_is_ser1 == 0)
                    {
                        txtSerialI.ReadOnly = true;
                        txtQty.Text = "";
                        txtQty.ReadOnly = false;
                        txtItmcurLoc.Text = "";
                        txtItmcurLoc.ReadOnly = false;
                    }
                    else
                    {
                        txtQty.Text = "1";
                        txtQty.ReadOnly = true;
                        txtItmcurLoc.Text = _invSerList2.FirstOrDefault().Ins_loc;
                        txtItmcurLoc.ReadOnly = true;
                    }
                    if (item.Mi_is_ser2 == 0)
                    {
                        txtSerialII.ReadOnly = true;
                    }
                    if (!item.Mi_is_ser3)
                    {
                        txtSerialIII.ReadOnly = true;
                    }

                }
            }

            if (_serPick)
            {
                DisplayMessage("This serial number is already picked ! "); txtSerialI.Text = ""; return;
            }



            DataTable dtserials = CHNLSVC.Inventory.GET_INR_SER(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), null, itemCode, ddlStatus.SelectedValue, txtSerialI.Text.Trim());
            dtserials.Clear();

            foreach (DispCurrentLocation loc in oCurrLoc)
            {
                DataTable dt = CHNLSVC.Inventory.GET_INR_SER(Session["UserCompanyCode"].ToString(), loc.Idc_loc, null, itemCode, ddlStatus.SelectedValue, txtSerialI.Text.Trim());
                dtserials.Merge(dt);
            }

            Session["SELECTED_SERIAL_CHSTO"] = txtSerialI.Text.Trim();
            if (dtserials.Rows.Count <= 0)
            {
                DisplayMessage("This serial number is not available! Please check again.!");
                txtSerialI.Text = string.Empty;
                txtSerialI.Focus();
                return;
            }
            else
            {
                foreach (DataRow dr in dtserials.Rows)
                {
                    txtSerialII.Text = dr["INS_SER_2"].ToString();
                    txtSerialIII.Text = dr["INS_SER_3"].ToString();
                }
                ddlStatus.DataSource = dtserials;
                ddlStatus.DataTextField = "mis_desc";
                ddlStatus.DataValueField = "INS_ITM_STUS";
                ddlStatus.DataBind();
                //ddlStatusToBeChange.SelectedValue = ddlStatus.SelectedValue;
            }
        }

        protected void btnChngStus_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                DisplayMessage("Select a job number!!!");
                return;
            }
            if (hdfSaveCon.Value == "No")
            {
                return;
            }
            if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16059))
            {
                if (Convert.ToDateTime(txtValidTo.Text) < DateTime.Today)
                {
                    DisplayMessage("Valid date is expired!!!");
                    return;
                }
                List<ReptPickSerials> _serList = new List<ReptPickSerials>();

                List<DispCurrentLocation> _currLocList = new List<DispCurrentLocation>();
                _currLocList = (List<DispCurrentLocation>)Session["oStockAtLocations"];

                List<DisposalItem> oItem = new List<DisposalItem>();
                oItem = (List<DisposalItem>)Session["oItem"];

                if (oItem == null)
                {
                    DisplayMessage("No items to change status!!!");
                    return;
                }
                if (oItem != null)
                {
                    if (oItem.Count <= 0)
                    {
                        DisplayMessage("No items to change status!!!");
                        return;
                    }
                }

                List<ReptPickSerials> TempItmDet_list;
                List<ReptPickSerials> TempItmDet_list2 = new List<ReptPickSerials>();


                foreach (DisposalItem itm in oItem)
                {
                    string _outStatus = itm.Idd_stus;
                    MasterItem mstItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Idd_itm_cd);
                    //LRPStatus Prob modiufy by subodana agnt

                    if (Session["UserCompanyCode"].ToString() == "ABL")
                    {
                        itm.Idd_stus = "DISPO";
                        itm.Idd_stus_desc = "DISPOSAL";
                    }
                    else if (Session["UserCompanyCode"].ToString() == "LRP")
                    {
                        itm.Idd_stus = "DISPLP";
                        itm.Idd_stus_desc = "DISPOSAL LP";
                    }
                    else
                    {
                        DisplayMessage("Canot Change Staus For this company");
                        return;
                    }

                    itm.Idd_act = 1;
                    TempItmDet_list = new List<ReptPickSerials>();

                    // List<ReptPickSerials> _tempSer = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), itm.Idd_cur_loc, itm.Idd_itm_cd, string.Empty, string.Empty);
                    // TempItmDet_list.AddRange(_tempSer);

                    if (mstItm.Mi_is_ser1 == 1)
                    {
                        //List<ReptPickSerials> _tempSer = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), itm.Idd_cur_loc, itm.Idd_itm_cd, string.Empty, string.Empty);
                        List<ReptPickSerials> _tempSer = CHNLSVC.Inventory.Search_by_serial(Session["UserCompanyCode"].ToString(), itm.Idd_cur_loc, itm.Idd_itm_cd, string.Empty, itm.Idd_ser_1, string.Empty);
                        ReptPickSerials TempItmDet = _tempSer.Where(x => x.Tus_ser_1 == itm.Idd_ser_1).ToList().FirstOrDefault();
                        TempItmDet_list2.Add(TempItmDet);
                    }
                    else
                    {

                        //List<ReptPickSerials> _tempSer = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), itm.Idd_cur_loc, itm.Idd_itm_cd, string.Empty, string.Empty);
                        ReptPickSerials _tempSer = CHNLSVC.Inventory.GetSerialDataForDisposalEntry(Session["UserCompanyCode"].ToString(), itm.Idd_cur_loc, itm.Idd_itm_cd, _outStatus, "N/A", "", 0).FirstOrDefault();
                        if (_tempSer != null)
                        {
                            _tempSer.Tus_qty = Convert.ToDecimal(itm.Idd_qty);
                            _tempSer.Tus_seq_no = 0;
                            _tempSer.Tus_itm_line = 0;
                            _tempSer.Tus_batch_line = 0;
                            _tempSer.Tus_ser_line = 0;
                            _tempSer.Tus_ser_id = 0;
                            TempItmDet_list2.Add(_tempSer);
                        }
                        //List<ReptPickSerials> tempNonSerList = _tempSer.Take(Convert.ToInt32(itm.Idd_qty)).ToList();
                        // TempItmDet_list2.AddRange(tempNonSerList);
                    }

                }

                Session["oItem"] = oItem;

                //int effect = CHNLSVC.Inventory.Save_Disposal_Items(oItem);

                string error = string.Empty;

                //if (effect == 1)
                //{
                if (TempItmDet_list2 != null)
                {
                    foreach (ReptPickSerials serial in TempItmDet_list2)
                    {

                        if (Session["UserCompanyCode"].ToString() == "ABL")
                        {
                            serial.Tus_new_status = "DISPO";
                            serial.Tus_new_status_Desc = "DISPOSAL";
                        }
                        else if (Session["UserCompanyCode"].ToString() == "LRP")
                        {
                            serial.Tus_new_status = "DISPLP";
                            serial.Tus_new_status_Desc = "DISPOSAL LP";
                        }
                        else
                        {
                            DisplayMessage("Canot Change Staus For this company");
                            return;
                        }
                    }

                    grdDspItms.DataSource = oItem;
                    grdDspItms.DataBind();
                    // grdDspItms.Columns[7].Visible = false;
                    //grdDspItms.Columns[8].Visible = true;
                    if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16057))
                    {
                        grdDspItms.Columns[7].Visible = true;
                    }
                    else
                    {
                        grdDspItms.Columns[7].Visible = false;
                    }
                    ViewState["SerialList"] = TempItmDet_list2;

                    foreach (DispCurrentLocation _curLoc in _currLocList)
                    {
                        var result = TempItmDet_list2.OfType<ReptPickSerials>().Where(s => s.Tus_loc == _curLoc.Idc_loc);
                        List<ReptPickSerials> _locSer = result.AsEnumerable().ToList();
                        if (_locSer.Count > 0)
                        {
                            ChangeItemStatus(_curLoc.Idc_loc, _locSer);
                        }

                    }




                    DisplayMessage("Status changed to 'DISPOSAL'");
                }
                else
                {
                    DisplayMessage("No items/Serials found!!");
                }
                //}
                //else
                //{
                //    DisplayMessage("Cannot change item status");
                //}
            }
            else
            {
                DisplayMessage("Sorry, You have no permission to change item status! - ( Advice: Required permission code : 16059)");
            }
        }

        protected void ChangeItemStatus(string location, List<ReptPickSerials> locSerList)
        {

            List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
            List<InventoryRequestItem> ScanItemList = new List<InventoryRequestItem>();
            DateTime date1 = Convert.ToDateTime(txtDate.Text);
            DateTime date2 = date1.Date;
            string documntNo_minus = "";
            string documntNo_plus = "";
            string error = string.Empty;

            #region Check Duplicate Serials
            if (locSerList != null)
            {
                var _dup = locSerList.Where(x => x.Tus_ser_1 != "N/A" && !string.IsNullOrEmpty(x.Tus_ser_1)).Select(y => y.Tus_ser_id).ToList();

                string _duplicateItems = string.Empty;
                bool _isDuplicate = false;
                if (_dup != null)
                    if (_dup.Count > 0)
                        foreach (Int32 _id in _dup)
                        {
                            Int32 _counts = locSerList.Where(x => x.Tus_ser_id == _id).Select(y => y.Tus_ser_id).Count();
                            if (_counts > 1)
                            {
                                _isDuplicate = true;
                                var _item = locSerList.Where(x => x.Tus_ser_id == _id).Select(x => x.Tus_itm_cd).Distinct();
                                foreach (string _str in _item)
                                    if (string.IsNullOrEmpty(_duplicateItems))
                                        _duplicateItems = _str;
                                    else
                                        _duplicateItems += "," + _str;
                            }
                        }
                if (_isDuplicate)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Following item serials are duplicating. Please remove the duplicated serials !!!')", true);
                    return;
                }
            }
            if (locSerList != null)
            {
                var _dup = locSerList.Where(x => x.Tus_ser_1 != "N/A" && !string.IsNullOrEmpty(x.Tus_ser_1)).Select(y => y.Tus_ser_id).ToList();

                string _duplicateItems = string.Empty;
                bool _isDuplicate = false;
                if (_dup != null)
                    if (_dup.Count > 0)
                        foreach (Int32 _id in _dup)
                        {
                            Int32 _counts = locSerList.Where(x => x.Tus_ser_id == _id).Select(y => y.Tus_ser_id).Count();
                            if (_counts > 1)
                            {
                                _isDuplicate = true;
                                var _item = locSerList.Where(x => x.Tus_ser_id == _id).Select(x => x.Tus_itm_cd).Distinct();
                                foreach (string _str in _item)
                                    if (string.IsNullOrEmpty(_duplicateItems))
                                        _duplicateItems = _str;
                                    else
                                        _duplicateItems += "," + _str;
                            }
                        }
                if (_isDuplicate)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Following item serials are duplicating. Please remove the duplicated serials !!!')", true);
                    return;
                }
            }
            #endregion

            //#region Check Serial Scan or not

            //if (grdDspItms == null)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No items found !!!')", true);
            //    return;
            //}
            //if (grdDspItms.Rows.Count == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No items found !!!')", true);
            //    return;
            //}
            //foreach (GridViewRow row in this.grdDspItms.Rows)
            //{
            //    MasterItem _itms = new MasterItem();

            //    Label _item = (Label)row.FindControl("lblitri_itm_cd");
            //    Label _pickitemqty = (Label)row.FindControl("lblitri_qty");

            //    _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item.Text.Trim());
            //    if (_itms.Mi_is_ser1 == 1)
            //    {
            //        if (Convert.ToDecimal(_pickitemqty.Text) == 0)
            //        {
            //            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Serial number is not picked for item  " + _itms.Mi_cd + " !!!')", true);
            //            return;
            //        }
            //    }
            //}

            //#endregion



            InventoryHeader _hdrMinus = new InventoryHeader();
            #region Fill InventoryHeader
            DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), location);
            foreach (DataRow r in dt_location.Rows)
            {
                _hdrMinus.Ith_sbu = (string)r["ML_OPE_CD"];
                if (System.DBNull.Value != r["ML_CATE_2"])
                {
                    _hdrMinus.Ith_channel = (string)r["ML_CATE_2"];
                }
                else
                {
                    _hdrMinus.Ith_channel = string.Empty;
                }
            }

            _hdrMinus.Ith_acc_no = "STATUS_CHANGE";
            _hdrMinus.Ith_anal_1 = "";
            _hdrMinus.Ith_anal_2 = "";
            _hdrMinus.Ith_anal_3 = "";
            _hdrMinus.Ith_anal_4 = "";
            _hdrMinus.Ith_anal_5 = "";
            _hdrMinus.Ith_anal_6 = 0;
            _hdrMinus.Ith_anal_7 = 0;
            _hdrMinus.Ith_anal_8 = DateTime.MinValue;
            _hdrMinus.Ith_anal_9 = DateTime.MinValue;
            _hdrMinus.Ith_anal_10 = false;
            _hdrMinus.Ith_anal_11 = false;
            _hdrMinus.Ith_anal_12 = false;
            _hdrMinus.Ith_bus_entity = "";
            _hdrMinus.Ith_cate_tp = "STUS";
            _hdrMinus.Ith_com = Session["UserCompanyCode"].ToString();
            _hdrMinus.Ith_com_docno = "";
            _hdrMinus.Ith_cre_by = Session["UserID"].ToString();
            _hdrMinus.Ith_cre_when = DateTime.Now;
            _hdrMinus.Ith_del_add1 = "";
            _hdrMinus.Ith_del_add2 = "";
            _hdrMinus.Ith_del_code = "";
            _hdrMinus.Ith_del_party = "";
            _hdrMinus.Ith_del_town = "";
            _hdrMinus.Ith_direct = false;
            _hdrMinus.Ith_doc_date = Convert.ToDateTime(txtDate.Text);
            _hdrMinus.Ith_doc_no = string.Empty;
            _hdrMinus.Ith_doc_tp = "ADJ";
            _hdrMinus.Ith_doc_year = date2.Year;
            //_hdrMinus.Ith_entry_no = txtOtherRef.Text.ToString().Trim();
            _hdrMinus.Ith_entry_tp = "STTUS";
            _hdrMinus.Ith_git_close = true;
            _hdrMinus.Ith_git_close_date = DateTime.MinValue;
            _hdrMinus.Ith_git_close_doc = BaseCls.GlbDefaultBin;
            _hdrMinus.Ith_isprinted = false;
            _hdrMinus.Ith_is_manual = false;
            _hdrMinus.Ith_job_no = string.Empty;
            _hdrMinus.Ith_loading_point = string.Empty;
            _hdrMinus.Ith_loading_user = string.Empty;
            _hdrMinus.Ith_loc = location;
            _hdrMinus.Ith_manual_ref = txtReference.Text.Trim();
            _hdrMinus.Ith_mod_by = Session["UserID"].ToString();
            _hdrMinus.Ith_mod_when = DateTime.Now;
            _hdrMinus.Ith_noofcopies = 0;
            _hdrMinus.Ith_oth_loc = string.Empty;
            _hdrMinus.Ith_oth_docno = "N/A";
            _hdrMinus.Ith_remarks = txtRemark.Text;
            _hdrMinus.Ith_session_id = Session["SessionID"].ToString();
            _hdrMinus.Ith_stus = "A";
            _hdrMinus.Ith_sub_tp = "SYS";
            _hdrMinus.Ith_vehi_no = string.Empty;
            #endregion
            MasterAutoNumber _autonoMinus = new MasterAutoNumber();
            #region Fill MasterAutoNumber
            _autonoMinus.Aut_cate_cd = location;
            _autonoMinus.Aut_cate_tp = "LOC";
            _autonoMinus.Aut_direction = null;
            _autonoMinus.Aut_modify_dt = null;
            _autonoMinus.Aut_moduleid = "ADJ";
            _autonoMinus.Aut_number = 5;
            _autonoMinus.Aut_start_char = "ADJ";
            _autonoMinus.Aut_year = null;
            #endregion
            InventoryHeader _hdrPlus = new InventoryHeader();
            #region Fill InventoryHeader
            _hdrPlus.Ith_channel = _hdrMinus.Ith_channel;
            _hdrPlus.Ith_sbu = _hdrMinus.Ith_sbu;
            _hdrPlus.Ith_acc_no = "STATUS_CHANGE";
            _hdrPlus.Ith_anal_1 = "";
            _hdrPlus.Ith_anal_2 = "";
            _hdrPlus.Ith_anal_3 = "";
            _hdrPlus.Ith_anal_4 = "";
            _hdrPlus.Ith_anal_5 = "";
            _hdrPlus.Ith_anal_6 = 0;
            _hdrPlus.Ith_anal_7 = 0;
            _hdrPlus.Ith_anal_8 = DateTime.MinValue;
            _hdrPlus.Ith_anal_9 = DateTime.MinValue;
            _hdrPlus.Ith_anal_10 = false;
            _hdrPlus.Ith_anal_11 = false;
            _hdrPlus.Ith_anal_12 = false;
            _hdrPlus.Ith_bus_entity = "";
            _hdrPlus.Ith_cate_tp = "STTUS";
            _hdrPlus.Ith_com = Session["UserCompanyCode"].ToString();
            _hdrPlus.Ith_com_docno = "";
            _hdrPlus.Ith_cre_by = Session["UserID"].ToString();
            _hdrPlus.Ith_cre_when = DateTime.Now;
            _hdrPlus.Ith_del_add1 = "";
            _hdrPlus.Ith_del_add2 = "";
            _hdrPlus.Ith_del_code = "";
            _hdrPlus.Ith_del_party = "";
            _hdrPlus.Ith_del_town = "";
            _hdrPlus.Ith_direct = true;
            _hdrPlus.Ith_doc_date = Convert.ToDateTime(txtDate.Text);
            _hdrPlus.Ith_doc_no = string.Empty;
            _hdrPlus.Ith_doc_tp = "ADJ";
            _hdrPlus.Ith_doc_year = date2.Year;
            // _hdrPlus.Ith_entry_no = txtOtherRef.Text.ToString().Trim();
            _hdrPlus.Ith_entry_tp = "STTUS";
            _hdrPlus.Ith_git_close = true;
            _hdrPlus.Ith_git_close_date = DateTime.MinValue;
            _hdrPlus.Ith_git_close_doc = string.Empty;
            _hdrPlus.Ith_isprinted = false;
            _hdrPlus.Ith_is_manual = false;
            _hdrPlus.Ith_job_no = string.Empty;
            _hdrPlus.Ith_loading_point = string.Empty;
            _hdrPlus.Ith_loading_user = string.Empty;
            _hdrPlus.Ith_loc = location;
            _hdrPlus.Ith_manual_ref = txtReference.Text.Trim();
            _hdrPlus.Ith_mod_by = Session["UserID"].ToString();
            _hdrPlus.Ith_mod_when = DateTime.Now;
            _hdrPlus.Ith_noofcopies = 0;
            _hdrPlus.Ith_oth_loc = string.Empty;
            _hdrPlus.Ith_oth_docno = "N/A";
            _hdrPlus.Ith_remarks = txtRemark.Text;
            _hdrPlus.Ith_session_id = Session["SessionID"].ToString();
            _hdrPlus.Ith_stus = "A";
            _hdrPlus.Ith_sub_tp = "STTUS";
            _hdrPlus.Ith_vehi_no = string.Empty;
            #endregion
            MasterAutoNumber _autonoPlus = new MasterAutoNumber();
            #region Fill MasterAutoNumber
            _autonoPlus.Aut_cate_cd = location;
            _autonoPlus.Aut_cate_tp = "LOC";
            _autonoPlus.Aut_direction = null;
            _autonoPlus.Aut_modify_dt = null;
            _autonoPlus.Aut_moduleid = "ADJ";
            _autonoPlus.Aut_number = 5;
            _autonoPlus.Aut_start_char = "ADJ";
            _autonoPlus.Aut_year = null;
            #endregion

            #region Status Change Adj- >>>> Adj+


            error = CHNLSVC.Inventory.InventoryStatusChangeEntry(_hdrMinus, _hdrPlus, locSerList, reptPickSubSerialsList, _autonoMinus, _autonoPlus, ScanItemList, out documntNo_minus, out documntNo_plus);

            if ((string.IsNullOrEmpty(error)) || (error.Contains("|")))
            {
                List<DisposalItem> oItem = new List<DisposalItem>();
                oItem = (List<DisposalItem>)Session["oItem"];

                int effect = CHNLSVC.Inventory.Save_Disposal_Items(oItem, false);

                string okmsg = "Successfully Saved! document no (-ADJ) : " + documntNo_minus + " and document no (+ADJ) :" + documntNo_plus;


                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + okmsg + "')", true);

                //Clear();


                //if (MessageBox.Show("Successfully Saved! document no (-ADJ) : " + documntNo_minus + " and document no (+ADJ) :" + documntNo_plus + "\nDo you want to print this?", "Process Completed : STTUS", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                //{
                //    BaseCls.GlbReportTp = "OUTWARD";
                //    Reports.Inventory.ReportViewerInventory _viewMinus = new Reports.Inventory.ReportViewerInventory();
                //    if (Session["UserCompanyCode"].ToString() == "SGL") 
                //        _viewMinus.GlbReportName = "SOutward_Docs.rpt";
                //    else if (BaseCls.GlbDefChannel == "AUTO_DEL") 
                //        _viewMinus.GlbReportName = "Dealer_Outward_Docs.rpt";
                //    else _viewMinus.GlbReportName = "Outward_Docs.rpt";
                //    _viewMinus.GlbReportDoc = documntNo_minus;
                //    _viewMinus.Show();
                //    _viewMinus = null;

                //    BaseCls.GlbReportTp = "INWARD";
                //    Reports.Inventory.ReportViewerInventory _viewPlus = new Reports.Inventory.ReportViewerInventory();
                //    if (Session["UserCompanyCode"].ToString() == "SGL") 
                //        _viewPlus.GlbReportName = "Inward_Docs.rpt";
                //    else if (BaseCls.GlbDefChannel == "AUTO_DEL") 
                //        _viewPlus.GlbReportName = "Dealer_Inward_Docs.rpt";
                //    else _viewPlus.GlbReportName = "Inward_Docs.rpt";
                //    _viewPlus.GlbReportDoc = documntNo_plus;
                //    _viewPlus.Show();
                //    _viewPlus = null;
                //}
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                //error = error.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
                //if (error == "ERROR   ORA-02290  check constraint (EMS.CHK_INLFREEQTY) violated ORA-06512  at  EMS.SP_UPDATE_INRLOC , line 31 ORA-06512  at line 1")
                //{
                //    string fronError = "Enterd items/Serials are reserved !";
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + fronError + "')", true);
                //}
                //else
                if (error.Contains("item/Serial  reserved !"))
                {

                    grdDspItms.DataSource = null;
                    grdDspItms.DataBind();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + error + "')", true);
                }
                else
                {
                    grdDspItms.DataSource = null;
                    grdDspItms.DataBind();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + "Error" + "')", true);
                }



            }

            #endregion
        }

        protected void btnSaveDispItm_Click(object sender, EventArgs e)
        {
            if (hdfSaveCon.Value == "No")
            {
                return;
            }
            if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16034))
            {
                if (Convert.ToDateTime(txtValidTo.Text) < DateTime.Today)
                {
                    DisplayMessage("Valid date is expired!!!");
                    return;
                }
                List<DisposalItem> oDispItems = CHNLSVC.Inventory.GET_DISPOSAL_ITM_LIST(Convert.ToInt32(seqNo.Value), txtJobNo.Text, false);

                List<DisposalItem> _dispItmList = new List<DisposalItem>();
                _dispItmList = (List<DisposalItem>)Session["oItem"];

                //var a = _dispItmList.Where(p => !oDispItems.Any(p2 => p2.Idd_seq == p.Idd_seq && p2.Idd_line == p.Idd_line)); ;
                //List<DisposalItem> x = a.AsEnumerable().ToList();
                List<DisposalItem> _removedItmList = new List<DisposalItem>();
                if (oDispItems != null)
                {
                    _removedItmList = oDispItems.Where(p => !_dispItmList.Any(p2 => p2.Idd_seq == p.Idd_seq && p2.Idd_line == p.Idd_line)).ToList();
                }

                if (_dispItmList.Count > 0)
                {

                    foreach (DisposalItem item in _dispItmList)
                    {
                        item.Idd_act = 1;
                    }
                    if (_removedItmList.Count > 0)
                    {
                        foreach (DisposalItem itm in _removedItmList)
                        {
                            itm.Idd_act = 0;
                            _dispItmList.RemoveAll(x => x.Idd_itm_cd == itm.Idd_itm_cd && x.Idd_line == itm.Idd_line);
                        }
                        int result2 = CHNLSVC.Inventory.Save_Disposal_Items(_removedItmList, true);
                    }
                    // _dispItmList = _dispItmList.Remove(_removedItmList);
                    int result = CHNLSVC.Inventory.Save_Disposal_Items(_dispItmList, false);

                    if (result == 1)
                    {
                        DisplayMessage("Disposal Item Save successfully", 3);
                    }
                }
                //}
                //else
                //{
                //    DisplayMessage("Items are already added to this job no");
                //}

            }
            else
            {
                DisplayMessage("Sorry, You have no permission to save disposal items! - ( Advice: Required permission code : 16034)");
                return;
            }

        }

        private void BindUserCompanyItemStatusDDLData(DropDownList ddl)
        {
            DataTable _tbl = CHNLSVC.Inventory.GetAllCompanyStatus(Session["UserCompanyCode"].ToString());

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
            if (ddlStatus.SelectedValue == "")
            {
                ddl.SelectedValue = "GOD";
            }
            else
            {
                ddl.SelectedValue = ddlStatus.SelectedValue;
            }

        }

        protected void btnJobCreate_Click(object sender, EventArgs e)
        {
            if (hdfSaveCon.Value == "No")
            {
                return;
            }
            if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10119))
            {
                if (Convert.ToDateTime(txtValidTo.Text) < DateTime.Today)
                {
                    DisplayMessage("Valid date is expired!!!");
                    return;
                }
                if (string.IsNullOrEmpty(textCust.Text))
                {
                    DisplayMessage("Please enter a customer");
                    textCust.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtJobAmount.Text))
                {
                    DisplayMessage("Please enter a valid amount");
                    txtJobAmount.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtRemark.Text))
                {
                    DisplayMessage("Please add the remark");
                    txtRemark.Focus();
                    return;
                }

                if (txtRemark.Text.Length > 150)
                {
                    DisplayMessage("Remark length is " + txtRemark.Text.Length.ToString() + ". Please reduce.");
                    txtLocation.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtReference.Text))
                {
                    DisplayMessage("Please add the reference");
                    txtReference.Focus();
                    return;
                }

                if (txtReference.Text.Length > 100)
                {
                    DisplayMessage("Reference length is " + txtReference.Text.Length.ToString() + ". Please reduce.");
                    txtLocation.Focus();
                    return;
                }

                if (oDisposalLocations == null)
                {
                    DisplayMessage("Add disposal locations");
                    txtLocation.Focus();
                    return;
                }

                if (oStockAtLocations.Count == null)
                {
                    DisplayMessage("Add Stock At locations");
                    txtLocation.Focus();
                    return;
                }

                if (oItemStatus.Count == null)
                {
                    DisplayMessage("Add item status locations");
                    txtLocation.Focus();
                    return;
                }
                #region JobNo
                if (string.IsNullOrEmpty(txtJobNo.Text))
                {
                    oDisposalHeader = new DisposalHeader();
                    oDisposalHeader.Dh_seq = 0;
                    oDisposalHeader.Dh_doc_no = string.Empty;
                }
                //else
                //{
                //    oDisposalHeader.Dh_doc_no = txtJobNo.Text.Trim();
                //}
                #endregion

                InventoryHeader inHeader = new InventoryHeader();
                List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                string documntNo = "";
                Int32 result = -99;
                string err = string.Empty;
                string docNo = string.Empty;
                bool IsTemp = false;

                MasterAutoNumber mastAutoNo = new MasterAutoNumber();
                MasterAutoNumber masterAuto = new MasterAutoNumber();

                if (oDisposalHeader.Dh_com != null)
                {
                    oDisposalHeader.Dh_frm_dt = Convert.ToDateTime(txtValidFrom.Text.Trim());
                    oDisposalHeader.Dh_to_dt = Convert.ToDateTime(txtValidTo.Text.Trim());
                    oDisposalHeader.Dh_ref_no = txtReference.Text.Trim();
                    oDisposalHeader.Dh_max_val = Convert.ToDecimal(txtJobAmount.Text.Trim());
                    oDisposalHeader.Dh_chk_max_val = 1;//(chkRestrict.Checked) ? 1 : 0;
                    oDisposalHeader.Dh_chg = 0;
                    oDisposalHeader.Dh_pay_mtd = "";//ddlPaymentType.SelectedValue.ToString();
                    oDisposalHeader.Dh_recipt_no = "";//txtReceipt.Text.Trim();
                    oDisposalHeader.Dh_rmk = txtRemark.Text.Trim();
                    oDisposalHeader.Dh_mod_by = Session["UserID"].ToString();
                    oDisposalHeader.Dh_mod_dt = DateTime.Now;
                    oDisposalHeader.Dh_mod_session = Session["SessionID"].ToString();
                    oDisposalHeader.Dh_cus_cd = textCust.Text;
                }
                else
                {
                    #region DisposalHeader
                    oDisposalHeader.Dh_com = Session["UserCompanyCode"].ToString();
                    oDisposalHeader.Dh_pc = Session["UserDefProf"].ToString();
                    oDisposalHeader.Dh_doc_dt = Convert.ToDateTime(txtDate.Text.Trim());
                    oDisposalHeader.Dh_frm_dt = Convert.ToDateTime(txtValidFrom.Text.Trim());
                    oDisposalHeader.Dh_to_dt = Convert.ToDateTime(txtValidTo.Text.Trim());
                    oDisposalHeader.Dh_ref_no = txtReference.Text.Trim();
                    oDisposalHeader.Dh_max_val = Convert.ToDecimal(txtJobAmount.Text.Trim());
                    oDisposalHeader.Dh_chk_max_val = 1;//(chkRestrict.Checked) ? 1 : 0;
                    oDisposalHeader.Dh_chg = 0;
                    oDisposalHeader.Dh_pay_mtd = "";//ddlPaymentType.SelectedValue.ToString();
                    oDisposalHeader.Dh_recipt_no = "";//txtReceipt.Text.Trim();
                    oDisposalHeader.Dh_rmk = txtRemark.Text.Trim();
                    oDisposalHeader.Dh_stus = "P";
                    oDisposalHeader.Dh_anal_1 = string.Empty;
                    oDisposalHeader.Dh_anal_2 = string.Empty;
                    oDisposalHeader.Dh_anal_3 = string.Empty;
                    oDisposalHeader.Dh_anal_4 = string.Empty;
                    oDisposalHeader.Dh_anal_5 = string.Empty;
                    oDisposalHeader.Dh_cre_by = Session["UserID"].ToString();
                    oDisposalHeader.Dh_cre_dt = DateTime.Now;
                    oDisposalHeader.Dh_cre_session = Session["SessionID"].ToString();
                    oDisposalHeader.Dh_mod_by = Session["UserID"].ToString();
                    oDisposalHeader.Dh_mod_dt = DateTime.Now;
                    oDisposalHeader.Dh_mod_session = Session["SessionID"].ToString();
                    oDisposalHeader.Dh_cus_cd = textCust.Text;
                    #endregion


                    #region Fill MasterAutoNumber

                    masterAuto.Aut_cate_cd = Session["UserDefLoca"].ToString();
                    masterAuto.Aut_cate_tp = "LOC";
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "DISPO";
                    masterAuto.Aut_number = 5;//what is Aut_number
                    masterAuto.Aut_start_char = "DISPO";
                    masterAuto.Aut_year = null;

                    #endregion



                    mastAutoNo.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                    mastAutoNo.Aut_cate_tp = "COM";
                    mastAutoNo.Aut_direction = null;
                    mastAutoNo.Aut_modify_dt = null;
                    mastAutoNo.Aut_moduleid = "DISPOJ";
                    mastAutoNo.Aut_start_char = "DISPOJ";
                    mastAutoNo.Aut_year = Convert.ToInt32(DateTime.Now.Year);
                }

                result = CHNLSVC.Inventory.DisposalAdjustmentWithJobSaveNew(inHeader, reptPickSerialsList, reptPickSubSerialsList, masterAuto, out documntNo, oDisposalHeader, oDisposalLocations, oStockAtLocations, oItemStatus, oItem, mastAutoNo, out err, out docNo, IsTemp);

                string Doc_ADJ = documntNo;
                string Doc_DISP = docNo;

                if (result > 0)
                {
                    string msg = string.Empty;

                    if (string.IsNullOrEmpty(txtJobNo.Text))
                    {
                        if (!string.IsNullOrEmpty(Doc_ADJ))
                        {
                            msg = "Saved. Disposal Number : " + Doc_DISP + ". Document Number : " + Doc_ADJ;
                        }
                        else
                        {
                            msg = "Saved. Disposal Number : " + Doc_DISP + ".";
                            //sendApprovedMails(Doc_DISP);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Doc_ADJ))
                        {
                            msg = "Updated the disposal number : " + Doc_DISP + ". Document Number : " + Doc_ADJ;
                        }
                        else
                        {
                            msg = "Updated the disposal number : " + Doc_DISP + ".";
                        }
                    }

                    DisplayMessage(msg, 3);

                    Clear();
                    return;
                }
                else
                {
                    //DisplayMessage("Error occurred while processing.");
                    DisplayMessage(documntNo, 4);
                }
            }
            else
            {
                DisplayMessage("Sorry, You have no permission to create job! - ( Advice: Required permission code : 10119)");
                return;
            }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            Item_Clear();
        }

        protected void lbtnCust_Click(object sender, EventArgs e)
        {
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "custSearch";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpSearchAdance.Show();
            txtSearchbyword.Focus();
        }

        protected void textCust_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textCust.Text.Trim()))
            {
                MasterBusinessEntity customer = CHNLSVC.Sales.GetCustomerProfile(textCust.Text, null, null, null, null);
                if (customer.Mbe_cd != null)
                {

                }
                else
                {
                    DisplayMessage("Customer is invalid. Please check the code.");
                    textCust.Text = "";
                    textCust.Focus();
                    return;
                }
            }
            else
            {
                DisplayMessage("Please enter a cutomer code");
                textCust.Text = "";
                textCust.Focus();
                return;
            }
        }

        //private void SaveReceiptHeader()
        //{
        //    Int32 row_aff = 0;
        //    string _msg = string.Empty;
        //    decimal _valPd = 0;
        //    ReptPickHeader _SerHeader = new ReptPickHeader();
        //    List<ReptPickSerials> _tempSerialSave = new List<ReptPickSerials>();
        //    List<VehicalRegistration> _tempRegSave = new List<VehicalRegistration>();
        //    List<VehicleInsuarance> _tempInsSave = new List<VehicleInsuarance>();




        //    RecieptHeader _ReceiptHeader = new RecieptHeader();
        //    _ReceiptHeader.Sar_seq_no = CHNLSVC.Inventory.GetSerialID(); //CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "RECEIPT", 1, Session["UserCompanyCode"].ToString());
        //    _ReceiptHeader.Sar_com_cd = Session["UserCompanyCode"].ToString();
        //    _ReceiptHeader.Sar_receipt_type = txtRecType.Text.Trim();
        //    _ReceiptHeader.Sar_receipt_no = _ReceiptHeader.Sar_seq_no.ToString();// txtRecNo.Text.Trim();
        //    if (txtRecType.Text == "ADVAN")
        //    {
        //        _ReceiptHeader.Sar_prefix = comboBoxPrefix.SelectedValue.ToString();
        //    }
        //    else
        //    {
        //        _ReceiptHeader.Sar_prefix = txtDivision.Text.Trim();
        //    }
        //    if (string.IsNullOrEmpty(txtManual.Text))
        //    {
        //        txtManual.Text = "0";
        //    }
        //    else
        //    {
        //        _ReceiptHeader.Sar_manual_ref_no = txtManual.Text.Trim();
        //    }

        //    _ReceiptHeader.Sar_receipt_date = Convert.ToDateTime(dtpRecDate.Text).Date;
        //    _ReceiptHeader.Sar_direct = true;
        //    _ReceiptHeader.Sar_acc_no = "";
        //    if (chkOth.Checked == true)
        //    {
        //        _ReceiptHeader.Sar_is_oth_shop = true;
        //        _ReceiptHeader.Sar_oth_sr = txtOthSR.Text;
        //    }
        //    else
        //    {
        //        _ReceiptHeader.Sar_is_oth_shop = false;
        //        _ReceiptHeader.Sar_oth_sr = "";
        //    }
        //    _ReceiptHeader.Sar_profit_center_cd = Session["UserDefProf"].ToString();
        //    _ReceiptHeader.Sar_debtor_cd = txtCusCode.Text.Trim();
        //    _ReceiptHeader.Sar_debtor_name = txtCusName.Text.Trim();
        //    _ReceiptHeader.Sar_debtor_add_1 = txtCusAdd1.Text.Trim();
        //    _ReceiptHeader.Sar_debtor_add_2 = txtCusAdd2.Text.Trim();
        //    _ReceiptHeader.Sar_tel_no = "";
        //    _ReceiptHeader.Sar_mob_no = txtMobile.Text.Trim();
        //    _ReceiptHeader.Sar_nic_no = txtNIC.Text.Trim();
        //    _ReceiptHeader.Sar_tot_settle_amt = Convert.ToDecimal(ucPayModes1.PaidAmountLabel.Text);
        //    _ReceiptHeader.Sar_comm_amt = 0;
        //    _ReceiptHeader.Sar_is_mgr_iss = false;
        //    _ReceiptHeader.Sar_esd_rate = 0;
        //    _ReceiptHeader.Sar_wht_rate = 0;
        //    _ReceiptHeader.Sar_epf_rate = 0;
        //    _ReceiptHeader.Sar_currency_cd = "LKR";
        //    _ReceiptHeader.Sar_uploaded_to_finance = false;
        //    _ReceiptHeader.Sar_act = true;
        //    _ReceiptHeader.Sar_direct_deposit_bank_cd = "";
        //    _ReceiptHeader.Sar_direct_deposit_branch = "";
        //    _ReceiptHeader.Sar_remarks = txtRemarks.Text.Trim();
        //    _ReceiptHeader.Sar_is_used = false;

        //    if (txtRecType.Text == "DISP")
        //    {
        //        _ReceiptHeader.Sar_ref_doc = txtDisposalJob.Text.Trim();
        //    }
        //    _ReceiptHeader.Sar_ser_job_no = "";
        //    _ReceiptHeader.Sar_used_amt = 0;



        //    _ReceiptHeader.Sar_create_by = Session["UserID"].ToString();
        //    _ReceiptHeader.Sar_mod_by = Session["UserID"].ToString();
        //    _ReceiptHeader.Sar_session_id = Session["SessionID"].ToString();
        //    _ReceiptHeader.Sar_anal_1 = cmbDistrict.Text;
        //    _ReceiptHeader.Sar_anal_2 = txtProvince.Text.Trim();

        //    if (radioButtonManual.Checked == true)
        //    {
        //        _ReceiptHeader.Sar_anal_3 = "MANUAL";
        //        _ReceiptHeader.Sar_anal_8 = 1;
        //    }
        //    else
        //    {
        //        _ReceiptHeader.Sar_anal_3 = "SYSTEM";
        //        _ReceiptHeader.Sar_anal_8 = 0;
        //    }

        //    _ReceiptHeader.Sar_anal_4 = txtSalesEx.Text;
        //    _ReceiptHeader.Sar_anal_5 = 0;
        //    _ReceiptHeader.Sar_anal_6 = 0;
        //    _ReceiptHeader.Sar_anal_7 = 0;
        //    _ReceiptHeader.Sar_anal_9 = 0;
        //    _ReceiptHeader.SAR_VALID_TO = _ReceiptHeader.Sar_receipt_date.AddDays(Convert.ToDouble(_valPd));
        //    //_ReceiptHeader.Sar_scheme = lblSchme.Text;
        //    _ReceiptHeader.Sar_inv_type = lblSalesType.Text;

        //    List<RecieptItem> _ReceiptDetailsSave = new List<RecieptItem>();
        //    Int32 _line = 0;
        //    foreach (RecieptItem line in ucPayModes1.RecieptItemList)
        //    {
        //        line.Sard_seq_no = _ReceiptHeader.Sar_seq_no;
        //        _line = _line + 1;
        //        line.Sard_line_no = _line;
        //        _ReceiptDetailsSave.Add(line);
        //    }

        //    MasterAutoNumber masterAuto = new MasterAutoNumber();
        //    masterAuto.Aut_cate_cd = Session["UserDefProf"].ToString();
        //    masterAuto.Aut_cate_tp = "PC";
        //    masterAuto.Aut_direction = null;
        //    masterAuto.Aut_modify_dt = null;
        //    masterAuto.Aut_moduleid = "RECEIPT";
        //    masterAuto.Aut_number = 5;//what is Aut_number
        //    masterAuto.Aut_start_char = txtDivision.Text.Trim();
        //    masterAuto.Aut_year = null;

        //    DataTable _pcInfo = new DataTable();
        //    _pcInfo = CHNLSVC.Sales.GetProfitCenterTable(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());


        //    MasterAutoNumber masterAutoRecTp = new MasterAutoNumber();
        //    masterAutoRecTp.Aut_cate_cd = Session["UserDefProf"].ToString();
        //    masterAutoRecTp.Aut_cate_tp = "PC";
        //    masterAutoRecTp.Aut_direction = null;
        //    masterAutoRecTp.Aut_modify_dt = null;

        //    if (_pcInfo.Rows[0]["mpc_ope_cd"].ToString() == "INV_LRP" && Session["UserCompanyCode"].ToString() == "LRP")
        //    {
        //        masterAutoRecTp.Aut_moduleid = "REC_LRP";
        //    }
        //    else
        //    {
        //        masterAutoRecTp.Aut_moduleid = "RECEIPT";
        //    }
        //    masterAutoRecTp.Aut_number = 5;//what is Aut_number
        //    masterAutoRecTp.Aut_start_char = txtRecType.Text.Trim();
        //    masterAutoRecTp.Aut_year = null;

        //    //if (dgvItem.Rows.Count > 0)
        //    //{
        //    //    _SerHeader.Tuh_usrseq_no = CHNLSVC.Inventory.GetSerialID();  //CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "RECEIPT", 0, Session["UserCompanyCode"].ToString());
        //    //    _SerHeader.Tuh_usr_id = Session["UserID"].ToString();
        //    //    _SerHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
        //    //    _SerHeader.Tuh_session_id = Session["SessionID"].ToString();
        //    //    _SerHeader.Tuh_cre_dt = Convert.ToDateTime(dtpRecDate.Text).Date;
        //    //    if (_invType == "HS")
        //    //    {
        //    //        _SerHeader.Tuh_doc_tp = "RECEIPT";
        //    //    }
        //    //    else
        //    //    {
        //    //        _SerHeader.Tuh_doc_tp = "DO";
        //    //    }
        //    //    _SerHeader.Tuh_direct = false;
        //    //    if (_isRes == false)
        //    //    {
        //    //        _SerHeader.Tuh_ischek_itmstus = false;
        //    //    }
        //    //    else
        //    //    {
        //    //        _SerHeader.Tuh_ischek_itmstus = true;
        //    //    }
        //    //    _SerHeader.Tuh_ischek_simitm = true;
        //    //    _SerHeader.Tuh_ischek_reqqty = true;


        //    //    if (txtRecType.Text == "VHREG" || txtRecType.Text == "VHINS")
        //    //    {
        //    //        _SerHeader.Tuh_doc_no = _invNo;
        //    //    }
        //    //    else
        //    //    {
        //    //        _SerHeader.Tuh_doc_no = "na";
        //    //    }


        //    //    foreach (ReptPickSerials line in _ResList)
        //    //    {
        //    //        line.Tus_usrseq_no = _SerHeader.Tuh_usrseq_no;
        //    //        line.Tus_cre_by = Session["UserID"].ToString();
        //    //        _tempSerialSave.Add(line);
        //    //    }
        //    //}


        //    string QTNum;

        //    List<RecieptHeader> otest = new List<RecieptHeader>();
        //    otest.Add(_ReceiptHeader);
        //    DataTable dt = GlobalMethod.ToDataTable(_ReceiptDetailsSave);
        //    DataTable d2t = GlobalMethod.ToDataTable(otest);

        //    row_aff = (Int32)CHNLSVC.Sales.SaveNewReceipt(_ReceiptHeader, _ReceiptDetailsSave, masterAuto, _SerHeader, _tempSerialSave, _tempRegSave, _tempInsSave, _sheduleDetails, null, masterAutoRecTp, _gvDetails, out QTNum);

        //    if (radioButtonManual.Checked == true)
        //    {
        //        if (Session["UserDefLoca"].ToString() != Session["UserDefProf"].ToString())
        //        {
        //            CHNLSVC.Inventory.UpdateManualDocNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "MDOC_AVREC", Convert.ToInt32(txtManual.Text), QTNum);
        //        }
        //    }

        //    if (radioButtonSystem.Checked == true)
        //    {
        //        if (Session["UserDefLoca"].ToString() != Session["UserDefProf"].ToString())
        //        {
        //            CHNLSVC.Inventory.UpdateManualDocNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "SDOC_AVREC", Convert.ToInt32(txtManual.Text), QTNum);
        //        }
        //    }

        //    if (row_aff == 1)
        //    {
        //        if (radioButtonManual.Checked == true)
        //        {
        //            string msg = "Successfully created.Receipt No: " + QTNum;
        //            DisplayMessage(msg, 3);
        //            Clear();
        //            btnSave.Enabled = true;

        //            //Immediate Print after Save  -- Lakshika
        //            Session["GlbReportType"] = "";
        //            Session["documntNo"] = QTNum;//txtRecNo.Text;
        //            Session["GlbReportName"] = "ReceiptPrints_n.rpt";
        //            BaseCls.GlbReportHeading = "Receipt Print Report";

        //            string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
        //            string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

        //            clsSales obj = new clsSales();
        //            obj.Receipt_print_n(Session["documntNo"].ToString());
        //            PrintPDF(targetFileName, obj.recreport1_n);

        //            string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
        //            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
        //            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);


        //            // return;
        //        }
        //        else
        //        {
        //            string msg = "Successfully created.Receipt No: " + QTNum;
        //            DisplayMessage(msg, 3);
        //            Clear();
        //            btnSave.Enabled = true;

        //            //Immediate Print after Save -- Lakshika
        //            Session["GlbReportType"] = "";
        //            Session["documntNo"] = QTNum;//txtRecNo.Text;
        //            Session["GlbReportName"] = "ReceiptPrints_n.rpt";
        //            BaseCls.GlbReportHeading = "Receipt Print Report";

        //            string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
        //            string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

        //            clsSales obj = new clsSales();
        //            obj.Receipt_print_n(Session["documntNo"].ToString());
        //            PrintPDF(targetFileName, obj.recreport1_n);

        //            string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
        //            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
        //            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

        //            // return;
        //        }
        //    }
        //    else
        //    {
        //        if (!string.IsNullOrEmpty(QTNum))
        //        {
        //            DisplayMessage(QTNum, 4);
        //            btnSave.Enabled = true;
        //            setButtionEnable(true, btnSave, "confSave");


        //        }
        //        else
        //        {
        //            DisplayMessage("Creation Fail.");
        //            btnSave.Enabled = true;
        //        }
        //    }
        //}

        protected void btnSearchSearchPending_Click(object sender, EventArgs e)
        {
            DisposalHeader oFilter = new DisposalHeader();
            oFilter.Dh_com = Session["UserCompanyCode"].ToString();
            oFilter.Dh_pc = Session["UserDefProf"].ToString();
            oFilter.Dh_stus = "P";
            oFilter.Dh_frm_dt = Convert.ToDateTime(txtFrom.Text.Trim());
            oFilter.Dh_to_dt = Convert.ToDateTime(txtTo.Text.Trim());
            oFilter.Dh_doc_no = txtJobNumberPending.Text.Trim();

            List<DisposalHeader> oDisposalHeader = CHNLSVC.Inventory.GET_DISPOSAL_JOBS(oFilter);
            oFilter.Dh_stus = "S";
            if (oDisposalHeader == null)
            {
                oDisposalHeader = new List<DisposalHeader>();
            }

            List<DisposalHeader> oDisposalHeader2 = CHNLSVC.Inventory.GET_DISPOSAL_JOBS(oFilter);
            if (oDisposalHeader2 != null && oDisposalHeader2.Count > 0)
            {
                oDisposalHeader.AddRange(CHNLSVC.Inventory.GET_DISPOSAL_JOBS(oFilter));
            }

            oFilter.Dh_stus = "A";
            List<DisposalHeader> oDisposalHeader3 = CHNLSVC.Inventory.GET_DISPOSAL_JOBS(oFilter);
            if (oDisposalHeader3 != null && oDisposalHeader3.Count > 0)
            {
                oDisposalHeader.AddRange(oDisposalHeader3);
            }

            if (oDisposalHeader != null && oDisposalHeader.Count > 0)
            {
                oDisposalHeader = oDisposalHeader.OrderBy(x => x.Dh_doc_dt).ToList();
                dgvPendingJobs.DataSource = oDisposalHeader;
                dgvPendingJobs.DataBind();
            }
            else
            {
                DisplayMessage("Cannot find pending jobs");
                return;
            }
        }

        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Session["SHWSECH"] = "123";
                mpSearchAdance.Show();
                if (!string.IsNullOrEmpty(txtSearchbyword.Text))
                {
                    FilterData();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
            }
        }


        private void FilterData()
        {
            try
            {
                if (lblvalue.Text == "dispatchLocation")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "dispatchLocation";
                    ViewState["SEARCH"] = _result;
                    mpSearchAdance.Show();
                    return;
                }
                else if (lblvalue.Text == "stockAtLocation")
                {

                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "stockAtLocation";
                    ViewState["SEARCH"] = _result;
                    mpSearchAdance.Show();
                    return;
                }
                else if (lblvalue.Text == "itemStatus")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "itemStatus";
                    ViewState["SEARCH"] = result;
                    mpSearchAdance.Show();
                    return;
                }
                else if (lblvalue.Text == "itemSearch")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    lblvalue.Text = "itemSearch";
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpSearchAdance.Show();
                    return;
                }

                else if (lblvalue.Text == "itmDispLocation")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "itmDispLocation";
                    ViewState["SEARCH"] = result;
                    mpSearchAdance.Show();
                    return;
                }
                else if (lblvalue.Text == "custSearch")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, ddlSearchbykey.SelectedValue, txtSearchbyword.Text, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "custSearch";
                    ViewState["SEARCH"] = result;
                    mpSearchAdance.Show();
                    return;
                }
                else if (lblvalue.Text == "itmSerialI")
                {
                    if (string.IsNullOrEmpty(txtItemCode.Text))
                        Session["_SerialSearchType"] = "SER1_WOITEM";
                    else
                        Session["_SerialSearchType"] = "SER1_WITEM";

                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
                    DataTable result = CHNLSVC.CommonSearch.Search_inr_ser_infor(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    int count = result.Rows.Count;
                    for (int x = count - 1; x >= 500; x--)
                    {
                        DataRow dr = result.Rows[x];
                        dr.Delete();
                    }
                    result.AcceptChanges();
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "itmSerialI";
                    ViewState["SEARCH"] = result;
                    mpSearchAdance.Show();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
                mpSearchAdance.Show();
            }
        }

        protected void grdLbtnDltLoc_Click(object sender, EventArgs e)
        {
            bool success = false;

            if (dgvLocations.Rows.Count == 0) return;

            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;

            if (row != null)
            {
                string loc_cd = (row.FindControl("lblDL_LOC") as Label).Text;
                if (oDisposalLocations.FindAll(x => x.Dl_loc == loc_cd.Trim()).Count > 0)
                {
                    oDisposalLocations.RemoveAll(x => x.Dl_loc == loc_cd.Trim());
                    Session["oDisposalLocations"] = oDisposalLocations;
                    BindLocation();
                    success = true;
                }
            }
            if (success)
            {
                DisplayMessage("Disposal location removed successfully");
            }

        }


        protected void grdLbtnDltStus_Click(object sender, EventArgs e)
        {
            bool success = false;

            if (dgvStatus.Rows.Count == 0) return;

            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;

            if (row != null)
            {
                string stus_cd = (row.FindControl("lblStusCd") as Label).Text;
                if (oItemStatus.FindAll(x => x.Ids_stus == stus_cd.Trim()).Count > 0)
                {
                    oItemStatus.RemoveAll(x => x.Ids_stus == stus_cd.Trim());
                    Session["oItemStatus"] = oItemStatus;
                    BindItemStatus();
                    success = true;
                }
            }
            if (success)
            {
                DisplayMessage("current status removed successfully");
            }
        }

        protected void grdLbtnDltCLoc_Click(object sender, EventArgs e)
        {
            bool success = false;

            if (dgvStockAt.Rows.Count == 0) return;

            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;

            if (row != null)
            {
                string cur_loc = (row.FindControl("lblIDC_LOC") as Label).Text;
                if (oStockAtLocations.FindAll(x => x.Idc_loc == cur_loc.Trim()).Count > 0)
                {
                    oStockAtLocations.RemoveAll(x => x.Idc_loc == cur_loc.Trim());
                    Session["oStockAtLocations"] = oStockAtLocations;
                    BindStockAtLocation();
                    success = true;
                }
            }
            if (success)
            {
                DisplayMessage("current location removed successfully");
            }
        }

        protected void txtJobAmount_TextChanged(object sender, EventArgs e)
        {

            decimal amt = Convert.ToDecimal(txtJobAmount.Text.Trim());
            if (amt < 0.0m)
            {
                DisplayMessage("Enter valid amount");
                txtJobAmount.Text = "";
                txtJobAmount.Focus();
                return;

            }
            else
            {
                txtJobAmount.Text = amt.ToString("N2");
            }


        }

        protected void btnCheckCost_Click(object sender, EventArgs e)
        {
            if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16057))
            {
                decimal totalCost = 0.00m;
                List<DisposalItem> _dispItmList = new List<DisposalItem>();
                if (Session["oItem"] != null)
                {
                    _dispItmList = (List<DisposalItem>)Session["oItem"];

                    foreach (DisposalItem item in _dispItmList)
                    {
                        totalCost += (Convert.ToDecimal(item.Idd_unit_cost) * item.Idd_qty);
                    }

                }
                DisplayMessage("Cost: " + totalCost.ToString("N2"));
            }
            else
            {
                DisplayMessage("Sorry, You have no permission to check the cost! - ( Advice: Required permission code : 16060)");
            }
        }

        protected void btnPendingJObSelect_Click(object sender, EventArgs e)
        {
            if (chkSendToPDA.Checked)
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                LinkButton btnPendingJObSelect = dr.FindControl("btnPendingJObSelect") as LinkButton;
                Label lblDH_DOC_DT = dr.FindControl("lblDH_DOC_DT") as Label;
                if (check_PDAProcess(btnPendingJObSelect.Text, lblDH_DOC_DT.Text))
                {
                    MPPDA.Show();
                }
            }
            else
            {

                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                LinkButton btnPendingJObSelect = dr.FindControl("btnPendingJObSelect") as LinkButton;
                txtJobNo.Text = btnPendingJObSelect.Text.Trim();
                txtJobNo_TextChanged(null, null);
                cpe.Collapsed = true;
                cpe.ClientState = "true";
                DisplayMessage("Job no : " + txtJobNo.Text + " is loaded successfully!!", 3);
            }

        }

        private bool check_PDAProcess(string JobNumber, string doc_date)
        {
            bool status = true;
            string _supplier = string.Empty;
            string _subdoc = string.Empty;
            string location = string.Empty;

            string OutwardNo = JobNumber;
            string outwrddate = doc_date;

            string warehousecom = (string)Session["WAREHOUSE_COM"];
            string warehouseloc = (string)Session["WAREHOUSE_LOC"];

            DataTable dtdoccheck = CHNLSVC.Inventory.IsDocNoAvailable(OutwardNo, "DISP", 1, Session["UserCompanyCode"].ToString());

            txtdocname.Text = JobNumber;

            if (dtdoccheck.Rows.Count > 1)
            {
                DisplayMessage("Selected document is already send to PDA");
                return status = false; ;
            }

            return status;
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                DisplayMessage("Select a job number!!!");
                return;
            }
            if (hdfSaveCon.Value == "No")
            {
                return;
            }
            if (Convert.ToDateTime(txtValidTo.Text) < DateTime.Today)
            {
                DisplayMessage("Valid date is expired!!!");
                return;
            }
            if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16058))
            {
                DisposalHeader jobEntry = CHNLSVC.Inventory.GET_DISPOSAL_JOB_HEADER(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtJobNo.Text, "ALL");
                if (jobEntry != null)
                {
                    if (jobEntry.Dh_stus == "P")
                    {
                        jobEntry.Dh_stus = "A";
                        jobEntry.Dh_mod_by = Session["UserID"].ToString();
                        jobEntry.Dh_mod_dt = DateTime.Now;
                        jobEntry.Dh_mod_session = Session["SessionID"].ToString();
                        Int32 rtnSeqNo = CHNLSVC.Inventory.Save_Disposal_Hdr(jobEntry);
                        if (rtnSeqNo == jobEntry.Dh_seq)
                        {
                            DisplayMessage("Job is approved successfully!", 3);
                            txtJobNo_TextChanged(null, null);
                            return;
                        }


                    }
                }
            }
            else
            {
                DisplayMessage("Sorry, You have no permission to approve job! - ( Advice: Required permission code : 16058)");
            }
        }

        private void PopulateLoadingBays()
        {
            try
            {
                DataTable dtbays = CHNLSVC.Inventory.LoadLoadingBays(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "LB");

                if (dtbays.Rows.Count > 0)
                {
                    ddlloadingbay.DataSource = dtbays;
                    ddlloadingbay.DataTextField = "mws_res_name";
                    ddlloadingbay.DataValueField = "mws_res_cd";
                    ddlloadingbay.DataBind();
                }

                ddlloadingbay.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private Int32 GenerateNewUserSeqNo(string DocumentType, string _scanDocument)
        {
            Int32 generated_seq = 0;
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), DocumentType, 0, Session["UserCompanyCode"].ToString());//direction always =1 for this method                    //assign user_seqno

            if (generated_seq > 0)
            {
                return generated_seq;
            }
            else
            {
                return 0;
            }
        }

        protected void btnsend_Click(object sender, EventArgs e)
        {

            Int32 val = 0;
            string warehousecom = (string)Session["WAREHOUSE_COM"];
            string warehouseloc = (string)Session["WAREHOUSE_LOC"];
            if (string.IsNullOrEmpty(txtdocname.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the document no !!!')", true);
                txtdocname.Focus();
                MPPDA.Show();
                return;
            }

            if (ddlloadingbay.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the loading bay !!!')", true);
                ddlloadingbay.Focus();
                MPPDA.Show();
                return;
            }

            List<InventoryRequestItem> _tmpReqItm = new List<InventoryRequestItem>();
            _tmpReqItm = Session["oItem"] as List<InventoryRequestItem>;
            Int32 _isResQty = 0;
            if (_tmpReqItm != null)
            {
                if (_tmpReqItm.Count > 0)
                {
                    decimal _resQty = _tmpReqItm.Where(c => c.Itri_res_qty > 0).Sum(c => c.Itri_res_qty);
                    decimal _sendQty = _tmpReqItm.Where(c => c.Itri_qty > 0).Sum(c => c.Itri_res_qty);
                    if (_resQty > 0)
                    {
                        _isResQty = 1;
                    }
                }
            }
            #region Add by Lakshan to chk doc already send or not 01 Oct 2016
            ReptPickHeader _tmpPickHdr = new ReptPickHeader();
            bool _docAva = false;
            _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
            {
                Tuh_doc_no = txtdocname.Text,
                Tuh_direct = false,
                Tuh_doc_tp = "DISP",
                Tuh_usr_com = Session["UserCompanyCode"].ToString()
            }).FirstOrDefault();

            DisposalHeader jobEntry = CHNLSVC.Inventory.GET_DISPOSAL_JOB_HEADER(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtdocname.Text, "ALL");
            if (_tmpPickHdr != null)
            {
                List<ReptPickSerials> _repSerList = CHNLSVC.Inventory.GET_TEMP_PICK_SER_DATA(new ReptPickSerials() { Tus_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                if (_repSerList != null)
                {
                    if (_repSerList.Count > 0)
                    {
                        _docAva = true;
                    }
                }
                List<ReptPickItems> _repItmList = CHNLSVC.Inventory.GET_TEMP_PICK_ITM_DATA(new ReptPickItems() { Tui_usrseq_no = _tmpPickHdr.Tuh_usrseq_no });
                if (_repItmList != null)
                {
                    if (_repItmList.Count > 0)
                    {
                        _docAva = true;
                    }
                }
            }
            if (_docAva)
            {
                string _msg = "Document has already sent to PDA";// or has alread processed by User : " + _tmpPickHdr.Tuh_usr_id + " in loading bay " + _tmpPickHdr.Tuh_load_bay;
                DisplayMessage(_msg);
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('+_msg+')", true);
                return;
            }
            #endregion
            //
            /*11 Oct 2016 Add by Lakshan */
            InventoryRequest _reqData = CHNLSVC.Inventory.GET_INT_REQ_DATA(new InventoryRequest()
            {
                Itr_req_no = txtdocname.Text,
                Itr_stus = "A",
                Itr_tp = "DISP",
            }).FirstOrDefault();
            Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("DISP", Session["UserCompanyCode"].ToString(), txtdocname.Text, 0);
            if (user_seq_num == -1)
            {
                user_seq_num = GenerateNewUserSeqNo("DISP", txtdocname.Text);
                if (user_seq_num > 0)
                {
                    ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                    _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                    _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                    _inputReptPickHeader.Tuh_doc_tp = "DISP";
                    _inputReptPickHeader.Tuh_direct = false;
                    _inputReptPickHeader.Tuh_ischek_itmstus = false;
                    _inputReptPickHeader.Tuh_ischek_simitm = false;
                    _inputReptPickHeader.Tuh_ischek_reqqty = false;
                    _inputReptPickHeader.Tuh_doc_no = txtdocname.Text.Trim();
                    _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                    _inputReptPickHeader.Tuh_wh_com = warehousecom;
                    _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                    _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;
                    _inputReptPickHeader.Tuh_is_take_res = _isResQty == 1 ? true : false;
                    if (_reqData != null)
                    {
                        _inputReptPickHeader.Tuh_rec_loc = _reqData.Itr_rec_to;
                    }
                    val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);

                    if (val == -1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        CHNLSVC.CloseChannel();
                        return;
                    }
                }
            }
            else
            {
                ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                _inputReptPickHeader.Tuh_doc_no = txtdocname.Text.Trim();
                _inputReptPickHeader.Tuh_doc_tp = "DISP";
                _inputReptPickHeader.Tuh_direct = false;
                _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                _inputReptPickHeader.Tuh_wh_com = warehousecom;
                _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;
                _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                _inputReptPickHeader.Tuh_is_take_res = _isResQty == 1 ? true : false;
                if (_reqData != null)
                {
                    _inputReptPickHeader.Tuh_rec_loc = _reqData.Itr_rec_to;
                }
                val = CHNLSVC.Inventory.UpdatePickHeader(_inputReptPickHeader);

                if (Convert.ToInt32(val) == -1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    CHNLSVC.CloseChannel();
                    return;
                }
            }

            DataTable dtchkitm = CHNLSVC.Inventory.CheckItemsScannedStatus(user_seq_num);

            if (dtchkitm.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Document has already been sent to PDA or has already been processed !')", true);
                return;
            }
            else
            {
                List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                List<DisposalItem> _select = new List<DisposalItem>();
                _select = CHNLSVC.Inventory.GET_DISPOSAL_ITM_LIST(jobEntry.Dh_seq, jobEntry.Dh_doc_no, false);
                if (_select != null)
                {
                    foreach (DisposalItem _itm in _select)
                    {
                        ReptPickItems _reptitm = new ReptPickItems();
                        _reptitm.Tui_usrseq_no = Convert.ToInt32(user_seq_num);
                        _reptitm.Tui_req_itm_qty = _itm.Idd_qty;
                        _reptitm.Tui_req_itm_cd = _itm.Idd_itm_cd;
                        _reptitm.Tui_req_itm_stus = _itm.Idd_stus;
                        _reptitm.Tui_pic_itm_cd = _itm.Idd_line.ToString();
                        // _reptitm.Tui_pic_itm_cd = Convert.ToString(_itm.Itri_line_no);//Darshana request add by rukshan
                        // _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                        _reptitm.Tui_pic_itm_qty = 0;
                        _saveonly.Add(_reptitm);


                    }
                    val = CHNLSVC.Inventory.SavePickedItems(_saveonly);
                }
                else
                {
                    DisplayMessage("Please Save Items First!!!");
                    MPPDA.Hide();
                    return;
                }

            }
            if (val == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Document sent to PDA Successfully !')", true);
                MPPDA.Hide();
            }
        }

        protected void btnDispose_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(txtValidTo.Text) < DateTime.Today)
            {
                DisplayMessage("Valid date is expired!!!");
                return;
            }
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                DisplayMessage("Select a job number!!!");
                return;
            }
            if (hdfSaveCon.Value == "No")
            {
                return;
            }

            List<DisposalLocation> _dspLocList = new List<DisposalLocation>();
            _dspLocList = (List<DisposalLocation>)Session["oDisposalLocations"];

            bool isDispLoc = false;

            foreach (DisposalLocation loc in _dspLocList)
            {
                if (Session["UserDefLoca"].ToString() == loc.Dl_loc)
                {
                    isDispLoc = true;
                    break;
                }
            }

            if (!isDispLoc)
            {
                DisplayMessage("Please log into a disposal location!!!");
                return;
            }

            DisposalHeader jobEntry = CHNLSVC.Inventory.GET_DISPOSAL_JOB_HEADER(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtJobNo.Text, "ALL");

            List<RecieptHeader> recDetList = CHNLSVC.Sales.GetRecieptByRefDoc(Session["UserCompanyCode"].ToString(), jobEntry.Dh_doc_no);
            decimal paidAmt = 0.00m;
            if (recDetList != null)
            {
                if (recDetList.Count > 0)
                {
                    grdPymntDet.DataSource = recDetList;
                    grdPymntDet.DataBind();
                    foreach (RecieptHeader rec in recDetList)
                    {
                        paidAmt += rec.Sar_tot_settle_amt;
                    }
                    if (paidAmt < Convert.ToDecimal(txtJobAmount.Text))
                    {
                        DisplayMessage("Customer has not completed the payment");
                        return;
                    }
                }
                else
                {
                    DisplayMessage("Customer has not done the payment");
                    return;
                }
            }

            List<ReptPickSerials> _serList = new List<ReptPickSerials>();

            List<DisposalItem> oItem = new List<DisposalItem>();
            oItem = (List<DisposalItem>)Session["oItem"];

            List<DisposalItem> outDispItm = new List<DisposalItem>();
            if (oItem != null)
            {
                if (oItem.Count > 0)
                {
                    if (chkDspsNotScan.Checked)
                    {
                        outDispItm = oItem.Where(x => x.Idd_act == 1 && x.Idd_scan_stus == 1).ToList();
                    }
                    else
                    {
                        outDispItm = oItem.Where(x => x.Idd_act == 1).ToList();
                    }

                }
            }

            List<ReptPickSerials> TempItmDet_list;
            List<ReptPickSerials> TempItmDet_list2 = new List<ReptPickSerials>();

            if (outDispItm != null)
            {
                if (outDispItm.Count > 0)
                {
                    bool _nonSerQtyExceed = false;
                    foreach (DisposalItem itm in outDispItm)
                    {
                        _nonSerQtyExceed = false;
                        TempItmDet_list = new List<ReptPickSerials>();

                        List<ReptPickSerials> _tempSer = new List<ReptPickSerials>();
                        if (chkJOB.Checked)
                        {
                            //if (itm.Idd_scan_stus == 1)
                            //{
                            //    MasterLocation mstLoc = CHNLSVC.General.GetLocationInfor(Session["UserCompanyCode"].ToString(), itm.Idd_disp_loc);
                            //    _tempSer = CHNLSVC.Inventory.LoadSavedSerialsList(txtJobNo.Text, Session["UserCompanyCode"].ToString(), itm.Idd_cur_loc, itm.Idd_itm_cd, string.Empty, itm.Idd_stus, mstLoc.Ml_wh_com, mstLoc.Ml_wh_cd, string.Empty);

                            //}
                            //else
                            //{
                            //    _tempSer = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), itm.Idd_cur_loc, itm.Idd_itm_cd, string.Empty, string.Empty);
                            //}
                            //_tempSer = scanSerList;
                            _tempSer = scanSerList.Where(c => c.Tus_itm_cd == itm.Idd_itm_cd).ToList();
                            DataTable table1 = new DataTable();
                            using (var reader = ObjectReader.Create(_tempSer))
                            {
                                table1.Load(reader);
                            }
                        }
                        else
                        {

                            if (itm.Idd_scan_stus == 1)
                            {
                                MasterLocation mstLoc = CHNLSVC.General.GetLocationInfor(Session["UserCompanyCode"].ToString(), itm.Idd_disp_loc);
                                if (itm.Idd_ser_1 == "N/A")
                                {
                                    decimal _serPickQty = scanSerList.Where(c => c.Tus_itm_cd == itm.Idd_itm_cd).Sum(c => c.Tus_qty);
                                    if (TempItmDet_list != null)
                                    {
                                        decimal pickQty = TempItmDet_list.Where(c => c.Tus_itm_cd == itm.Idd_itm_cd).Sum(c => c.Tus_qty);
                                        if (pickQty < _serPickQty)
                                        {
                                            ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "", itm.Idd_itm_cd, itm.Idd_ser_id);
                                            _tempSer.Add(_reptPickSerial_);
                                        }
                                        else
                                        {
                                            _nonSerQtyExceed = true;
                                        }
                                    }
                                    else
                                    {
                                        if (_serPickQty > 0)
                                        {
                                            ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "", itm.Idd_itm_cd, itm.Idd_ser_id);
                                            _tempSer.Add(_reptPickSerial_);
                                        }
                                    }
                                }
                                else
                                {
                                    _tempSer = CHNLSVC.Inventory.LoadSavedSerialsList(txtJobNo.Text, Session["UserCompanyCode"].ToString(), itm.Idd_cur_loc, itm.Idd_itm_cd, string.Empty, itm.Idd_stus, mstLoc.Ml_wh_com, mstLoc.Ml_wh_cd, string.Empty);
                                }

                            }
                            else
                            {
                                //CHNLSVC.Inventory.GetSerialDataForDisposalEntry(Session["UserCompanyCode"].ToString(), itm.Idd_cur_loc, itm.Idd_itm_cd, _outStatus, "N/A", "", 0).FirstOrDefault();
                                // _tempSer = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), itm.Idd_cur_loc, itm.Idd_itm_cd, string.Empty, string.Empty);
                                // ReptPickSerials _pickSer = CHNLSVC.Inventory.GetSerialDataForDisposalEntry(Session["UserCompanyCode"].ToString(), itm.Idd_cur_loc, itm.Idd_itm_cd, itm.Idd_stus, "N/A", "", 0).FirstOrDefault();
                                ReptPickSerials _pickSer = CHNLSVC.Inventory.GetSerialDataForDisposalEntry(Session["UserCompanyCode"].ToString(), itm.Idd_cur_loc, itm.Idd_itm_cd, itm.Idd_stus, itm.Idd_ser_1, "", 0).FirstOrDefault();
                                if (_pickSer != null)
                                {
                                    MasterItem _mstItm = CHNLSVC.General.GetItemMaster(itm.Idd_itm_cd);
                                    if (_mstItm != null)
                                    {
                                        if (_mstItm.Mi_is_ser1 == 1)
                                        {
                                            _tempSer.Add(_pickSer);
                                        }
                                        else
                                        {
                                            _pickSer.Tus_qty = Convert.ToDecimal(itm.Idd_qty);
                                            _pickSer.Tus_seq_no = 0;
                                            _pickSer.Tus_itm_line = 0;
                                            _pickSer.Tus_batch_line = 0;
                                            _pickSer.Tus_ser_line = 0;
                                            _pickSer.Tus_ser_id = 0;
                                            _tempSer.Add(_pickSer);
                                        }
                                    }
                                }
                            }
                        }
                        if (itm.Idd_ser_1 == "N/A")
                        {
                            decimal _serPickQty = scanSerList.Where(c => c.Tus_itm_cd == itm.Idd_itm_cd).Sum(c => c.Tus_qty);
                            decimal pickQty = TempItmDet_list2.Where(c => c.Tus_itm_cd == itm.Idd_itm_cd).Sum(c => c.Tus_qty);
                            if (pickQty > _serPickQty)
                            {
                                _nonSerQtyExceed = true;
                            }
                        }
                        if (!_nonSerQtyExceed)
                        {
                            TempItmDet_list.AddRange(_tempSer);
                        }

                        List<ReptPickSerials> TempItmDet = new List<ReptPickSerials>();
                        MasterItem mstItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Idd_itm_cd);
                        if (mstItm.Mi_is_ser1 == 1)
                        {
                            TempItmDet = TempItmDet_list.Where(x => x.Tus_ser_1 == itm.Idd_ser_1).ToList();
                            if (TempItmDet.Count > 0)
                            {
                                TempItmDet_list2.Add(TempItmDet.FirstOrDefault());
                            }
                        }
                        else
                        {
                            if (chkDspsNotScan.Checked)
                            {
                                TempItmDet = TempItmDet_list.Where(a => a.Tus_ser_1 == "N/A" && a.Tus_ser_id == 0 && a.Tus_itm_cd == itm.Idd_itm_cd).ToList();
                                TempItmDet = TempItmDet.Take(Convert.ToInt32(itm.Idd_scan_qty)).ToList();
                            }
                            else
                            {
                                //TempItmDet = TempItmDet_list.Where(a => a.Tus_ser_1 == "N/A" && a.Tus_ser_id == 0).ToList();
                                //TempItmDet = TempItmDet.Take(Convert.ToInt32(itm.Idd_scan_qty)).ToList();
                                //TempItmDet = TempItmDet_list.Where(a => a.Tus_itm_stus == "DISPO").ToList();
                                //TempItmDet = TempItmDet.Take(Convert.ToInt32(itm.Idd_qty)).ToList();
                                TempItmDet.AddRange(TempItmDet_list);
                            }
                            if (TempItmDet.Count > 0)
                            {
                                TempItmDet_list2.AddRange(TempItmDet);
                            }
                        }


                    }
                }
                else
                {
                    DisplayMessage("No items to Dispose!!!");
                    return;
                }
            }
            else
            {
                DisplayMessage("No items to Dispose!!!");
                return;
            }



            foreach (DisposalLocation dispLoc in _dspLocList)
            {
                List<ReptPickSerials> TempItmLocDet = TempItmDet_list2.Where(x => x.Tus_loc == dispLoc.Dl_loc).ToList();
                _serList.AddRange(TempItmLocDet);
            }
            if (TempItmDet_list2.Count != _serList.Count)
            {
                DisplayMessage("All the items are have not recieved to disposal locations");
                return;
            }
            else
            {
                bool IsTemp = false;
                int _direction = 0;
                #region Check Duplicate Serials

                if (TempItmDet_list2 != null && TempItmDet_list2.Count > 0)
                {
                    var _dup = TempItmDet_list2.Where(x => x.Tus_ser_1 != "N/A" && x.Tus_ser_1 != "").Select(y => y.Tus_ser_id).ToList();

                    string _duplicateItems = string.Empty;
                    bool _isDuplicate = false;
                    if (_dup != null)
                        if (_dup.Count > 0)
                            foreach (Int32 _id in _dup)
                            {
                                Int32 _counts = TempItmDet_list2.Where(x => x.Tus_ser_id == _id).Select(y => y.Tus_ser_id).Count();
                                DataTable table2 = new DataTable();
                                using (var reader = ObjectReader.Create(TempItmDet_list2))
                                {
                                    table2.Load(reader);
                                }
                                if (_counts > 1)
                                {
                                    _isDuplicate = true;
                                    var _item = TempItmDet_list2.Where(x => x.Tus_ser_id == _id).Select(x => x.Tus_itm_cd).Distinct();
                                    foreach (string _str in _item)
                                        if (string.IsNullOrEmpty(_duplicateItems))
                                            _duplicateItems = _str;
                                        else
                                            _duplicateItems += "," + _str;
                                }
                            }
                    if (_isDuplicate)
                    {
                        DisplayMessage("Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems);
                        return;
                    }
                }
                #endregion
                InventoryHeader inHeader = new InventoryHeader();
                #region Fill InventoryHeader
                DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                foreach (DataRow r in dt_location.Rows)
                {
                    // Get the value of the wanted column and cast it to string
                    inHeader.Ith_sbu = (string)r["ML_OPE_CD"];
                    if (System.DBNull.Value != r["ML_CATE_2"])
                    {
                        inHeader.Ith_channel = (string)r["ML_CATE_2"];
                    }
                    else
                    {
                        inHeader.Ith_channel = string.Empty;
                    }
                }
                inHeader.Ith_acc_no = "STOCK_ADJ";
                inHeader.Ith_anal_1 = "";

                inHeader.Ith_anal_3 = "";
                inHeader.Ith_anal_4 = "";
                inHeader.Ith_anal_5 = "";
                inHeader.Ith_anal_6 = 0;
                inHeader.Ith_anal_7 = 0;
                inHeader.Ith_anal_8 = DateTime.MinValue;
                inHeader.Ith_anal_9 = DateTime.MinValue;
                //if (Session["DocType"].ToString() == "TempDoc")
                //{
                //    inHeader.Ith_anal_10 = true;
                //    inHeader.Ith_anal_2 = txtJobNumber.Text;
                //}
                //else

                {
                    inHeader.Ith_anal_10 = false;
                    inHeader.Ith_anal_2 = "";
                }

                inHeader.Ith_anal_11 = false;
                inHeader.Ith_anal_12 = false;
                inHeader.Ith_bus_entity = "";
                inHeader.Ith_cate_tp = "DISPO";
                inHeader.Ith_com = Session["UserCompanyCode"].ToString();
                inHeader.Ith_com_docno = "";
                inHeader.Ith_cre_by = Session["UserID"].ToString();
                inHeader.Ith_cre_when = DateTime.Now;
                inHeader.Ith_del_add1 = "";
                inHeader.Ith_del_add2 = "";
                inHeader.Ith_del_code = "";
                inHeader.Ith_del_party = "";
                inHeader.Ith_del_town = "";

                inHeader.Ith_direct = false;
                inHeader.Ith_doc_tp = "ADJ";

                inHeader.Ith_doc_date = DateTime.Today;
                inHeader.Ith_doc_no = string.Empty;

                inHeader.Ith_doc_year = DateTime.Today.Year;
                if (!IsTemp)
                {
                    if (Session["PDADocNo"] != null)
                    {
                        inHeader.Ith_entry_no = Session["PDADocNo"].ToString();
                    }
                    else
                    {
                        inHeader.Ith_entry_no = string.Empty;
                    }
                }
                inHeader.Ith_entry_tp = "DISPO";
                inHeader.Ith_git_close = true;
                inHeader.Ith_git_close_date = DateTime.MinValue;
                inHeader.Ith_git_close_doc = string.Empty;
                inHeader.Ith_isprinted = false;
                inHeader.Ith_is_manual = false;
                inHeader.Ith_job_no = string.Empty;
                inHeader.Ith_loading_point = string.Empty;
                inHeader.Ith_loading_user = string.Empty;
                inHeader.Ith_loc = Session["UserDefLoca"].ToString();
                inHeader.Ith_manual_ref = txtReference.Text.Trim();
                inHeader.Ith_mod_by = Session["UserID"].ToString();
                inHeader.Ith_mod_when = DateTime.Now;
                inHeader.Ith_noofcopies = 0;
                inHeader.Ith_oth_loc = string.Empty;
                inHeader.Ith_oth_docno = txtReference.Text.Trim();
                inHeader.Ith_remarks = txtRemark.Text;

                //inHeader.Ith_seq_no = 6; removed by Chamal 12-05-2013
                inHeader.Ith_session_id = Session["SessionID"].ToString();
                inHeader.Ith_stus = "A";
                inHeader.Ith_sub_tp = "DISPO";
                inHeader.Ith_vehi_no = string.Empty;
                inHeader.Ith_anal_3 = "";//ddlDeliver.SelectedItem.Text;

                #endregion

                int _line = 0;
                #region Update some serial items
                if (TempItmDet_list2 != null)
                {
                    if (_direction == 1)
                    {
                        foreach (var _seritem in TempItmDet_list2)
                        {
                            _seritem.Tus_exist_grncom = Session["UserCompanyCode"].ToString();
                            _seritem.Tus_exist_grndt = Convert.ToDateTime(txtDate.Text).Date;
                            _seritem.Tus_orig_grncom = Session["UserCompanyCode"].ToString();
                            _seritem.Tus_orig_grndt = Convert.ToDateTime(txtDate.Text).Date;
                        }
                    }
                    else if (_direction == 0)
                    {
                        foreach (var _ADJSer in TempItmDet_list2)
                        {
                            _line = _line + 1;
                            _ADJSer.Tus_base_itm_line = _line;
                        }
                    }
                }
                #endregion

                MasterAutoNumber masterAuto = new MasterAutoNumber();
                #region Fill MasterAutoNumber

                masterAuto.Aut_cate_cd = Session["UserDefLoca"].ToString();
                masterAuto.Aut_cate_tp = "LOC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "DISPO";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "DISPO";
                masterAuto.Aut_year = null;

                #endregion
                List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                string documntNo = "";
                string docNo = "";
                string err = "";

                List<DisposalItem> AllDispItem = new List<DisposalItem>();
                AllDispItem = CHNLSVC.Inventory.GET_DISPOSAL_ITM_LIST(Convert.ToInt32(seqNo.Value), jobEntry.Dh_doc_no, false);
                List<DisposalItem> activeList = new List<DisposalItem>();
                if (AllDispItem != null)
                {
                    activeList = AllDispItem.Where(x => x.Idd_act == 1).ToList();
                }
                bool ifAllDispose = false;
                ifAllDispose = activeList.Except(outDispItm).Any();

                if (!ifAllDispose)
                {
                    jobEntry.Dh_stus = "F";
                }
                bool check = chkDspsNotScan.Checked;
                if (chkDspsNotScan.Checked == false)
                {
                    check = false;
                    var scanqty = outDispItm.Sum(a => a.Idd_scan_qty);
                    if (scanqty > 0)
                    {
                        DisplayMessage("Please Check Only Scan ", 2);
                        return;
                    }
                }
                decimal _dispCount = 0;
                if (TempItmDet_list2 != null)
                {
                    _dispCount = TempItmDet_list2.Sum(c => c.Tus_qty);
                }

                int result = CHNLSVC.Inventory.DisposalAdjustmentSave(inHeader, TempItmDet_list2, reptPickSubSerialsList, masterAuto, jobEntry, outDispItm, out docNo, out err, false, check);

                string Doc_ADJ = documntNo;
                string Doc_DISP = docNo;

                if (result > 0)
                {
                    string msg = string.Empty;

                    //if (string.IsNullOrEmpty(txtJobNo.Text))
                    //{
                    if (!string.IsNullOrEmpty(Doc_ADJ))
                    {
                        msg = "Saved. Disposal Number : " + Doc_DISP + ". Document Number : " + Doc_ADJ;
                    }
                    else
                    {
                        msg = "Saved. Disposal Number : " + Doc_DISP + ".";
                        //sendApprovedMails(Doc_DISP);
                    }

                    Session["GlbReportType"] = "SCM1_DISPOSAL";
                    BaseCls.GlbReportDoc = Doc_DISP;
                    BaseCls.GlbReportHeading = "OUTWARD DOC";
                    Session["GlbReportName"] = "Outward_Docs.rpt";
                    Session["GlbReportName"] = "Outward_Docs.rpt";

                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                    clsInventory obj = new clsInventory();
                    obj.printOutwardDocs(Session["GlbReportName"].ToString(), Doc_DISP);
                    PrintPDF(targetFileName, obj._outdoc);

                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    //}
                    //else
                    //{
                    //    if (!string.IsNullOrEmpty(Doc_ADJ))
                    //    {
                    //        msg = "Updated the disposal number : " + Doc_DISP + ". Document Number : " + Doc_ADJ;
                    //    }
                    //    else
                    //    {
                    //        msg = "Updated the disposal number : " + Doc_DISP + ".";
                    //    }
                    //}

                    DisplayMessage(msg, 3);

                    Clear();
                    return;
                }
                else
                {
                    string msg = docNo;
                    DisplayMessage(msg, 2);
                    return;
                }
            }


        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (hdfSaveCon.Value == "No")
            {
                return;
            }
            if (Convert.ToDateTime(txtValidTo.Text) < DateTime.Today)
            {
                DisplayMessage("Valid date is expired!!!");
                return;
            }
            btnJobCreate_Click(null, null);
        }

        protected void jobNoSearchlbtn_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DisposalJOb);
            DataTable result = CHNLSVC.CommonSearch.SEARCH_DISPOSAL_JOB(SearchParams, ddlSearchbykey2.SelectedValue, txtSearchbyword2.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
            grdResult2.DataSource = result;
            grdResult2.DataBind();
            lblvalue2.Text = "DisposalJOb";
            BindUCtrlDDLData2(result);
            //ViewState["SEARCH2"] = result;
            mpSearch.Show();
            txtSearchbyword2.Focus();
        }

        protected void txtSearchbyword2_TextChanged(object sender, EventArgs e)
        {
            jobNoSearchlbtn_Click(null, null);
        }

        protected void lbtnSearch2_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "custSearch")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, ddlSearchbykey.SelectedValue, txtSearchbyword.Text, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                grdResult.DataSource = result;
                grdResult.DataBind();
                BindUCtrlDDLData(result);
                mpSearchAdance.Show();
                txtSearchbyword.Focus();
            }
            if (lblvalue.Text == "dispatchLocation")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, ddlSearchbykey.SelectedValue, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                BindUCtrlDDLData(result);
                mpSearchAdance.Show();
                txtSearchbyword.Focus();
            }
            if (lblvalue.Text == "stockAtLocation")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, ddlSearchbykey.SelectedValue, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                BindUCtrlDDLData(result);
                mpSearchAdance.Show();
                txtSearchbyword.Focus();
            }
            if (lblvalue.Text == "itemStatus")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(SearchParams, ddlSearchbykey.SelectedValue, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                BindUCtrlDDLData(result);
                mpSearchAdance.Show();
                txtSearchbyword.Focus();
            }
            if (lblvalue.Text == "itemSearch")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, ddlSearchbykey.SelectedValue, txtSearchbyword.Text);
                grdResult.DataBind();
                BindUCtrlDDLData(result);
                mpSearchAdance.Show();
                txtSearchbyword.Focus();
            }
            if (lblvalue.Text == "itmDispLocation")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, ddlSearchbykey.SelectedValue, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                BindUCtrlDDLData(result);
                mpSearchAdance.Show();
                txtSearchbyword.Focus();
            }
            if (lblvalue.Text == "itmSerialI")
            {
                if (string.IsNullOrEmpty(txtItemCode.Text))
                    Session["_SerialSearchType"] = "SER1_WOITEM";
                else
                    Session["_SerialSearchType"] = "SER1_WITEM";

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
                DataTable result = CHNLSVC.CommonSearch.Search_inr_ser_infor(SearchParams, ddlSearchbykey.SelectedValue, txtSearchbyword.Text);
                int count = result.Rows.Count;
                for (int x = count - 1; x >= 500; x--)
                {
                    DataRow dr = result.Rows[x];
                    dr.Delete();
                }
                result.AcceptChanges();

                grdResult.DataSource = result;
                grdResult.DataBind();
                BindUCtrlDDLData(result);
                mpSearchAdance.Show();
                txtSearchbyword.Focus();
            }
        }

        protected void lbtnupload_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                DisposalHeader jobEntry = CHNLSVC.Inventory.GET_DISPOSAL_JOB_HEADER(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtJobNo.Text, "ALL");
                if (jobEntry != null)
                {
                    if (jobEntry.Dh_stus == "A")
                    {
                        DisplayMessage("Cannot add items to approved job.!!");
                        Item_Clear();
                        return;
                    }
                }
            }

            List<DispCurrentLocation> _currLocList = new List<DispCurrentLocation>();
            _currLocList = (List<DispCurrentLocation>)Session["oStockAtLocations"];

            List<DisposalLocation> _DspLocList = new List<DisposalLocation>();
            _DspLocList = (List<DisposalLocation>)Session["oDisposalLocations"];

            List<DisposalCurrStatus> _currStusList = new List<DisposalCurrStatus>();
            _currStusList = (List<DisposalCurrStatus>)Session["oItemStatus"];

            if (_currLocList == null)
            {
                DisplayMessage("Add stock at locations!!!");
                txtStockAt.Focus();
                return;
            }

            if (_DspLocList == null)
            {
                DisplayMessage("Add disposal locations!!!");
                txtLocation.Focus();
                return;
            }

            if (_currStusList == null)
            {
                DisplayMessage("Add allowed status!!!");
                txtStatus.Focus();
                return;
            }
            Label3.Text = "";
            excelUpload.Show();
        }

        protected void btnupload_Click(object sender, EventArgs e)
        {
            Label3.Text = "";

            if (fileupexcelupload.HasFile)
            {
                string FileName = Path.GetFileName(fileupexcelupload.PostedFile.FileName);
                string Extension = Path.GetExtension(fileupexcelupload.PostedFile.FileName);
                if (Extension == ".xls" || Extension == ".XLS" || Extension == ".xlsx" || Extension == ".XLSX")
                {

                }
                else
                {
                    Label3.Visible = true;
                    Label3.Text = "Please select a valid excel (.xls) file";
                    DisplayMessage("Please select a valid excel (.xls) file", 2);
                    excelUpload.Show();
                    return;
                }
                if (Extension != ".xls")
                {

                    //Label3.Visible = true;
                    //Label3.Text = "Please select a valid excel (.xls) file";
                    //DisplayMessage("Please select a valid excel (.xls) file", 2);
                    //excelUpload.Show();
                    //return;
                }
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                //LoadData(FolderPath + FileName);
                string FilePath = Server.MapPath(FolderPath + FileName);
                fileupexcelupload.SaveAs(FilePath);
                Session["FilePath"] = FilePath;
                string value = string.Empty;
                ExcelProcess(FilePath, 1, out value);

                if (value == "4")
                {
                    if (!string.IsNullOrEmpty(excelErrorValue.Value))
                    {
                        string _ser = excelErrorValue.Value;
                        Label3.Visible = true;
                        Label3.Text = "Serial " + _ser + " not found";
                        excelUpload.Show();
                        excelErrorValue.Value = "";
                        List<DisposalItem> dItmList = CHNLSVC.Inventory.GET_DISPOSAL_ITM_LIST_WEB(Convert.ToInt32(seqNo.Value), txtJobNo.Text, false);
                        grdDspItms.DataSource = dItmList;
                        grdDspItms.DataBind();
                        Session["oItem"] = dItmList;
                        return;
                    }
                }

                if (value == "5")
                {
                    if (!string.IsNullOrEmpty(excelErrorValue.Value))
                    {
                        string _dspLoc = excelErrorValue.Value;
                        Label3.Visible = true;
                        Label3.Text = "Location " + _dspLoc + " is not allowed for disposal";
                        excelUpload.Show();
                        excelErrorValue.Value = "";
                        List<DisposalItem> dItmList = CHNLSVC.Inventory.GET_DISPOSAL_ITM_LIST_WEB(Convert.ToInt32(seqNo.Value), txtJobNo.Text, false);
                        grdDspItms.DataSource = dItmList;
                        grdDspItms.DataBind();
                        Session["oItem"] = dItmList;
                        return;
                    }
                }

                if (value == "6")
                {
                    if (!string.IsNullOrEmpty(excelErrorValue.Value))
                    {
                        string _dspStus = excelErrorValue.Value;
                        Label3.Visible = true;
                        Label3.Text = "Status " + _dspStus + " is not allowed for disposal";
                        excelUpload.Show();
                        excelErrorValue.Value = "";
                        List<DisposalItem> dItmList = CHNLSVC.Inventory.GET_DISPOSAL_ITM_LIST_WEB(Convert.ToInt32(seqNo.Value), txtJobNo.Text, false);
                        grdDspItms.DataSource = dItmList;
                        grdDspItms.DataBind();
                        Session["oItem"] = dItmList;
                        return;
                    }
                }
                if (value == "7")
                {
                    string _dspItm = excelErrorValue.Value;
                    Label3.Visible = true;
                    Label3.Text = "Item " + _dspItm + " has not enough qty to dispose";
                    excelUpload.Show();
                    excelErrorValue.Value = "";
                    List<DisposalItem> dItmList = CHNLSVC.Inventory.GET_DISPOSAL_ITM_LIST_WEB(Convert.ToInt32(seqNo.Value), txtJobNo.Text, false);
                    grdDspItms.DataSource = dItmList;
                    grdDspItms.DataBind();
                    Session["oItem"] = dItmList;
                    return;
                }
                if (value == "8")
                {
                    string _dspItm = excelErrorValue.Value;
                    Label3.Visible = true;
                    Label3.Text = "Item " + _dspItm + " does not contain a serial";
                    excelUpload.Show();
                    excelErrorValue.Value = "";
                    List<DisposalItem> dItmList = CHNLSVC.Inventory.GET_DISPOSAL_ITM_LIST_WEB(Convert.ToInt32(seqNo.Value), txtJobNo.Text, false);
                    grdDspItms.DataSource = dItmList;
                    grdDspItms.DataBind();
                    Session["oItem"] = dItmList;
                    return;
                }
                if (value == "9")
                {
                    string _dspItm = excelErrorValue.Value;
                    Label3.Visible = true;
                    Label3.Text = "Item " + _dspItm + " cannot have decimal quantity";
                    excelUpload.Show();
                    excelErrorValue.Value = "";
                    List<DisposalItem> dItmList = CHNLSVC.Inventory.GET_DISPOSAL_ITM_LIST_WEB(Convert.ToInt32(seqNo.Value), txtJobNo.Text, false);
                    grdDspItms.DataSource = dItmList;
                    grdDspItms.DataBind();
                    Session["oItem"] = dItmList;
                    return;
                }
                if (value == "10")
                {
                    string _dspItm = excelErrorValue.Value;
                    Label3.Visible = true;
                    Label3.Text = "Item " + _dspItm + " cannot have quantity more than 1";
                    excelUpload.Show();
                    excelErrorValue.Value = "";
                    List<DisposalItem> dItmList = CHNLSVC.Inventory.GET_DISPOSAL_ITM_LIST_WEB(Convert.ToInt32(seqNo.Value), txtJobNo.Text, false);
                    grdDspItms.DataSource = dItmList;
                    grdDspItms.DataBind();
                    Session["oItem"] = dItmList;
                    return;
                }
                if (value == "11")
                {
                    string _dspItm = excelErrorValue.Value;
                    Label3.Visible = true;
                    Label3.Text = "Item " + _dspItm + " cannot have quantity less than or equal 0";
                    excelUpload.Show();
                    excelErrorValue.Value = "";
                    List<DisposalItem> dItmList = CHNLSVC.Inventory.GET_DISPOSAL_ITM_LIST_WEB(Convert.ToInt32(seqNo.Value), txtJobNo.Text, false);
                    grdDspItms.DataSource = dItmList;
                    grdDspItms.DataBind();
                    Session["oItem"] = dItmList;
                    return;
                }

                if (value == "12")
                {
                    string _dspItm = excelErrorValue.Value;
                    Label3.Visible = true;
                    Label3.Text = "Item " + _dspItm + " have no quantity in given status and location";
                    excelUpload.Show();
                    excelErrorValue.Value = "";
                    List<DisposalItem> dItmList = CHNLSVC.Inventory.GET_DISPOSAL_ITM_LIST_WEB(Convert.ToInt32(seqNo.Value), txtJobNo.Text, false);
                    grdDspItms.DataSource = dItmList;
                    grdDspItms.DataBind();
                    Session["oItem"] = dItmList;
                    return;
                }
                if (value == "13")
                {
                    string _dspItm = excelErrorValue.Value;
                    Label3.Visible = true;
                    Label3.Text = "Can not upload over 500 items";
                    excelUpload.Show();
                    excelErrorValue.Value = "";
                    List<DisposalItem> dItmList = CHNLSVC.Inventory.GET_DISPOSAL_ITM_LIST_WEB(Convert.ToInt32(seqNo.Value), txtJobNo.Text, false);
                    grdDspItms.DataSource = dItmList;
                    grdDspItms.DataBind();
                    Session["oItem"] = dItmList;
                    return;
                }
            }
            else
            {
                DisplayMessage("Please select the correct upload file path", 2);
                Label3.Visible = true;
                Label3.Text = "Please select the correct upload file path";
                excelUpload.Show();
                return;

            }
        }

        private void ExcelProcess(string FilePath, int option, out string value)
        {
            Int32 seqno = -1;
            DataTable[] GetExecelTbl = LoadData(FilePath);

            List<DispCurrentLocation> _currLocList = new List<DispCurrentLocation>();
            _currLocList = (List<DispCurrentLocation>)Session["oStockAtLocations"];

            List<DisposalLocation> _DspLocList = new List<DisposalLocation>();
            _DspLocList = (List<DisposalLocation>)Session["oDisposalLocations"];

            List<DisposalCurrStatus> _currStusList = new List<DisposalCurrStatus>();
            _currStusList = (List<DisposalCurrStatus>)Session["oItemStatus"];
            DataTable _tbl = CHNLSVC.Inventory.GetAllCompanyStatus(Session["UserCompanyCode"].ToString());
            if (GetExecelTbl != null)
            {
                if (GetExecelTbl[0].Rows.Count > 0)
                {
                    #region data table create
                    if (GetExecelTbl[0].Rows.Count > 501)
                    {
                      //  value = "13";
                      //  excelErrorValue.Value = "You Cannot Upload over 500 items!!";
                      //  return;
                    }
                    #region status data validate
                    DataView view = new DataView(GetExecelTbl[0]);
                    //DataTable distinctValues = view.ToTable(true, "Status");
                    //foreach (DataRow _row in distinctValues.Rows)
                    //{
                    //    var filtered = _tbl.AsEnumerable().Where(r => r.Field<String>("MIS_DESC") == _row[0].ToString());
                    //    if (filtered==null)
                    //    {
                    //        value = "13";
                    //        excelErrorValue.Value = "Invalid item status description " + _row[0].ToString();
                    //        return;
                    //    }
                    //}
                    #endregion
                    DataTable _dtNew = new DataTable();
                    _dtNew.Columns.Add("Item", typeof(string));
                    _dtNew.Columns.Add("Serial", typeof(string));
                    _dtNew.Columns.Add("Status", typeof(string));
                    _dtNew.Columns.Add("Qty", typeof(decimal));
                    _dtNew.Columns.Add("CurLoc", typeof(string));
                    _dtNew.Columns.Add("DipLoc", typeof(string));
                    DataRow _dr = _dtNew.NewRow();
                    MasterItem _mstItem = new MasterItem();

                    decimal _tmpQty = 0, _pickQty = 0, _docQty = 0;

                    for (int i = 1; i < GetExecelTbl[0].Rows.Count; i++)
                    {
                        _dr = _dtNew.NewRow();
                        _dr["Item"] = GetExecelTbl[0].Rows[i][0].ToString();
                        _dr["Serial"] = GetExecelTbl[0].Rows[i][1].ToString();
                        _dr["Status"] = GetExecelTbl[0].Rows[i][2].ToString();
                        _dr["Qty"] = decimal.TryParse(GetExecelTbl[0].Rows[i][3].ToString(), out _tmpQty) ? Convert.ToDecimal(GetExecelTbl[0].Rows[i][3].ToString()) : 0;
                        _dr["CurLoc"] = GetExecelTbl[0].Rows[i][4].ToString();
                        _dr["DipLoc"] = GetExecelTbl[0].Rows[i][5].ToString();
                        _mstItem = CHNLSVC.General.GetItemMaster(GetExecelTbl[0].Rows[i][0].ToString());
                        if (_mstItem != null)
                        {
                            if (_mstItem.Mi_is_ser1 != 1)
                            {
                                #region non serialized
                                //bool contains = _dtNew.AsEnumerable().Any(row => _dr["Item"].ToString() == row.Field<String>("Item")
                                // && _dr["Serial"].ToString() == row.Field<String>("Serial")
                                // && _dr["Status"].ToString() == row.Field<String>("Status")
                                // );
                                var _tmpRow = (from x in _dtNew.AsEnumerable()
                                               where x.Field<string>("Item") == _dr["Item"].ToString()
                                               where x.Field<string>("Serial") == _dr["Serial"].ToString()
                                               where x.Field<string>("Status") == _dr["Status"].ToString()
                                               select x).ToList();
                                bool _dataAvailable = false;
                                if (_tmpRow != null)
                                {
                                    if (_tmpRow.Count > 0)
                                    {
                                        _dataAvailable = true;
                                    }
                                }
                                if (_dataAvailable)
                                {
                                    _pickQty = decimal.TryParse(_tmpRow[0]["Qty"].ToString(), out _tmpQty) ? Convert.ToDecimal(_tmpRow[0]["Qty"].ToString()) : 0;
                                    _docQty = decimal.TryParse(_dr["Qty"].ToString(), out _tmpQty) ? Convert.ToDecimal(_dr["Qty"].ToString()) : 0;
                                    _pickQty = _pickQty + _docQty;
                                    _tmpRow[0]["Qty"] = _pickQty;
                                }
                                else
                                {
                                    _dtNew.Rows.Add(_dr);
                                }
                                #endregion
                            }
                            else
                            {
                                #region serialized item
                                _dtNew.Rows.Add(_dr);
                                #endregion
                            }
                        }
                    }
                    #endregion
                    // _lstTaxDet = ViewState["_lstTaxDet"] as List<mst_itm_tax_structure_det>;

                    #region loop excel data
                    for (int i = 0; i < _dtNew.Rows.Count; i++)
                    {
                        try
                        {
                            if (_dtNew.Rows[i][0].ToString().Trim() != "")
                            {
                                List<ReptPickSerials> TempItmDet_list = new List<ReptPickSerials>();
                                string item = _dtNew.Rows[i][0].ToString();
                                string serialI = _dtNew.Rows[i][1].ToString();
                                string status = _dtNew.Rows[i][2].ToString();
                                string qty = _dtNew.Rows[i][3].ToString();
                                string cSLoc = _dtNew.Rows[i][4].ToString();
                                string dspLoc = _dtNew.Rows[i][5].ToString();
                                if (serialI == "N/A")
                                {
                                    int x = 0;
                                }
                                var filtered = _tbl.AsEnumerable().Where(r => r.Field<String>("MIS_DESC") == status);
                                string stusCd = filtered.FirstOrDefault().Field<String>("MIC_CD");

                                MasterItem msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), item);

                                if (msitem.Mi_is_ser1 == 1)
                                {
                                    #region serialized
                                    decimal qtyVal;
                                    Decimal.TryParse(qty, out qtyVal);
                                    if (qtyVal % 1 != 0.00m)
                                    {
                                        value = "9";
                                        excelErrorValue.Value = item;
                                        return;
                                    }

                                    if (string.IsNullOrEmpty(serialI))
                                    {
                                        value = "8";
                                        excelErrorValue.Value = item;
                                        return;
                                    }
                                    foreach (DispCurrentLocation _currLoc in _currLocList)
                                    {
                                        List<ReptPickSerials> _tempSer = CHNLSVC.Inventory.Search_by_serial(Session["UserCompanyCode"].ToString(), _currLoc.Idc_loc, item, string.Empty, serialI, string.Empty);
                                        TempItmDet_list.AddRange(_tempSer);
                                    }

                                    if (qty == "1")
                                    {
                                        TempItmDet_list = TempItmDet_list.Where(x => x.Tus_ser_1 == serialI).ToList();
                                    }
                                    else
                                    {
                                        value = "10";
                                        excelErrorValue.Value = item;
                                        return;
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region non ser
                                    decimal qtyVal;
                                    Decimal.TryParse(qty, out qtyVal);
                                    if (qtyVal % 1 != 0.00m)
                                    {
                                        value = "9";
                                        excelErrorValue.Value = item;
                                        return;
                                    }

                                    if (qtyVal <= 0.00m)
                                    {
                                        value = "11";
                                        excelErrorValue.Value = item;
                                        return;
                                    }
                                    TempItmDet_list = new List<ReptPickSerials>();
                                    //List<ReptPickSerials> _tempSer = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), cSLoc, item, string.Empty, string.Empty);
                                    List<ReptPickSerials> _tempSer = CHNLSVC.Inventory.GetSerialDataForDisposalEntry(Session["UserCompanyCode"].ToString(), cSLoc, item, stusCd, "N/A", "", 0);
                                    List<ReptPickSerials> _tempSer2 = new List<ReptPickSerials>();
                                    if (_tempSer.Count > 0)
                                    {

                                        _tempSer2 = _tempSer.Where(x => x.Tus_itm_stus == stusCd && x.Tus_loc == cSLoc).ToList();
                                    }

                                    if (_tempSer2.Count > 0)
                                    {
                                        InventoryLocation _invBal = CHNLSVC.Inventory.GET_INR_LOC_BALANCE(new InventoryLocation()
                                        {
                                            Inl_com = Session["UserCompanyCode"].ToString(),
                                            Inl_loc = cSLoc,
                                            Inl_itm_cd = item,
                                            Inl_itm_stus = stusCd
                                        });
                                        decimal _avaQty = 0;
                                        if (_invBal != null)
                                        {
                                            _avaQty = _invBal.Inl_free_qty;
                                        }
                                        if (_avaQty < Convert.ToInt32(qty))
                                        {
                                            value = "7";
                                            excelErrorValue.Value = item;
                                            return;
                                        }
                                        else
                                        {
                                            if (_tempSer2[0] != null)
                                            {
                                                _tempSer2[0].Tus_qty = Convert.ToDecimal(qty);
                                                _tempSer2[0].Tus_seq_no = 0;
                                                _tempSer2[0].Tus_itm_line = 0;
                                                _tempSer2[0].Tus_batch_line = 0;
                                                _tempSer2[0].Tus_ser_line = 0;
                                                _tempSer2[0].Tus_ser_id = 0;
                                                TempItmDet_list.Add(_tempSer2[0]);
                                            }
                                            else
                                            {
                                                value = "7";
                                                excelErrorValue.Value = item;
                                                return;
                                            }
                                            //_tempSer2.FirstOrDefault().Tus_qty = Convert.ToDecimal(qty);
                                            //TempItmDet_list.Add(_tempSer2.FirstOrDefault());
                                        }
                                    }
                                    else
                                    {
                                        value = "12";
                                        excelErrorValue.Value = item;
                                        return;
                                    }
                                    #endregion
                                }

                                if (TempItmDet_list.Count == 0)
                                {
                                    value = "4";
                                    excelErrorValue.Value = serialI;
                                    return;
                                }

                                List<DisposalLocation> dspDet = _DspLocList.Where(x => x.Dl_loc == dspLoc).ToList();
                                if (dspDet.Count < 1)
                                {
                                    value = "5";
                                    excelErrorValue.Value = dspLoc;
                                    return;
                                }

                                List<DisposalCurrStatus> stusDet = _currStusList.Where(x => x.Ids_Stus_desc == status).ToList();
                                if (stusDet.Count < 1)
                                {
                                    value = "6";
                                    excelErrorValue.Value = status;
                                    return;
                                }

                                AddItemGrid(item, serialI, dspLoc, status, cSLoc, qty);

                            }

                        }
                        catch (Exception ex)
                        {
                            DisplayMessage("Excel  Data Invalid Please check Excel File and Upload ", 2);
                            Label3.Visible = true;
                            Label3.Text = "Excel  Data Invalid Please check Excel File and Upload";
                            excelUpload.Show();
                            value = "3";
                            return;
                        }
                    #endregion
                    }
                }
            }
            value = "1";
        }

        public DataTable[] LoadData(string FileName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DataTable Tax = new DataTable();

            using (OleDbConnection cn = new OleDbConnection { ConnectionString = ConnectionString(FileName, "No") })
            {
                cn.Open();
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dtExcelSchema;
                cmdExcel.Connection = cn;

                dtExcelSchema = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                cn.Close();

                //Read Data from First Sheet
                cn.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(Tax);


                return new DataTable[] { Tax };
            }
        }

        public string ConnectionString(string FileName, string Header)
        {
            OleDbConnectionStringBuilder Builder = new OleDbConnectionStringBuilder();
            if (System.IO.Path.GetExtension(FileName).ToUpper() == ".XLS")
            {
                Builder.Provider = "Microsoft.Jet.OLEDB.4.0";
                Builder.Add("Extended Properties", string.Format("Excel 8.0;IMEX=1;HDR={0};", Header));
            }
            else
            {
                Builder.Provider = "Microsoft.ACE.OLEDB.12.0";
                Builder.Add("Extended Properties", string.Format("Excel 12.0;IMEX=1;HDR={0};", Header));
            }

            Builder.DataSource = FileName;

            return Builder.ConnectionString;
        }

        protected void txtValidTo_TextChanged(object sender, EventArgs e)
        {
            DateTime toDate = Convert.ToDateTime(txtValidTo.Text);
            DateTime frmate = Convert.ToDateTime(txtValidFrom.Text);
            int result = DateTime.Compare(toDate, frmate);

            if (result < 0)
            {
                DisplayMessage("Cannot select a day earlier than from date!");
                txtValidTo.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                return;
            }
        }

        protected void txtValidFrom_TextChanged(object sender, EventArgs e)
        {
            DateTime toDate = Convert.ToDateTime(txtValidTo.Text);
            DateTime frmate = Convert.ToDateTime(txtValidFrom.Text);
            int result = DateTime.Compare(toDate, frmate);

            if (result < 0)
            {
                DisplayMessage("Cannot select a day later than today!");
                txtValidFrom.Text = DateTime.Today.ToString("dd/MMM/yyyy");
                return;
            }
        }

        protected void txtItmcurLoc_TextChanged(object sender, EventArgs e)
        {
            List<DispCurrentLocation> _currLocList = new List<DispCurrentLocation>();
            _currLocList = (List<DispCurrentLocation>)Session["oStockAtLocations"];

            List<DispCurrentLocation> _det = _currLocList.Where(x => x.Idc_loc == txtItmcurLoc.Text.ToUpper()).ToList();
            if (_det.Count < 1)
            {
                DisplayMessage(txtItmcurLoc.Text + " is not allowed stock at location");
                txtItmcurLoc.Text = "";
                txtItmcurLoc.Focus();
                return;
            }
        }

        private void SaveReceiptHeader()
        {
            Int32 row_aff = 0;
            string _msg = string.Empty;
            decimal _valPd = 0;
            ReptPickHeader _SerHeader = new ReptPickHeader();
            List<ReptPickSerials> _tempSerialSave = new List<ReptPickSerials>();
            List<VehicalRegistration> _tempRegSave = new List<VehicalRegistration>();
            List<VehicleInsuarance> _tempInsSave = new List<VehicleInsuarance>();

            List<RecieptHeader> _recDetList = new List<RecieptHeader>();
            decimal curPayment = 0.00m;
            _recDetList = CHNLSVC.Sales.GetRecieptByRefDoc(Session["UserCompanyCode"].ToString(), txtJobNo.Text);
            if (_recDetList != null)
            {
                if (_recDetList.Count > 0)
                {
                    foreach (RecieptHeader recHdr in _recDetList)
                    {
                        curPayment += recHdr.Sar_tot_settle_amt;
                    }
                }
            }
            decimal curPaid = Convert.ToDecimal(ucPayModes1.PaidAmountLabel.Text);
            if (curPayment > 0.00m)
            {
                ucPayModes1.Amount.Text = (Math.Abs(curPayment - curPaid)).ToString();
            }
            else
            {
                ucPayModes1.Amount.Text = curPaid.ToString();
            }

            MasterBusinessEntity custDetails = CHNLSVC.Sales.GetCustomerProfileByCom(textCust.Text.Trim(), null, null, null, null, Session["UserCompanyCode"].ToString());

            RecieptHeader _ReceiptHeader = new RecieptHeader();
            _ReceiptHeader.Sar_seq_no = CHNLSVC.Inventory.GetSerialID(); //CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "RECEIPT", 1, Session["UserCompanyCode"].ToString());
            _ReceiptHeader.Sar_com_cd = Session["UserCompanyCode"].ToString();
            _ReceiptHeader.Sar_receipt_type = "DISPOSAL";
            _ReceiptHeader.Sar_receipt_no = _ReceiptHeader.Sar_seq_no.ToString();// txtRecNo.Text.Trim();
            _ReceiptHeader.Sar_prefix = "DIS";
            if (string.IsNullOrEmpty(txtReference.Text))
            {
                txtReference.Text = "0";
            }
            else
            {
                _ReceiptHeader.Sar_manual_ref_no = txtReference.Text.Trim();
            }

            _ReceiptHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text).Date;
            _ReceiptHeader.Sar_direct = true;
            _ReceiptHeader.Sar_acc_no = "";

            _ReceiptHeader.Sar_is_oth_shop = false;
            _ReceiptHeader.Sar_oth_sr = "";
            _ReceiptHeader.Sar_profit_center_cd = Session["UserDefProf"].ToString();
            _ReceiptHeader.Sar_debtor_cd = textCust.Text.Trim();
            _ReceiptHeader.Sar_debtor_name = custDetails.Mbe_name;
            _ReceiptHeader.Sar_debtor_add_1 = custDetails.Mbe_add1;
            _ReceiptHeader.Sar_debtor_add_2 = custDetails.Mbe_add2;
            _ReceiptHeader.Sar_tel_no = custDetails.Mbe_tel;
            _ReceiptHeader.Sar_mob_no = custDetails.Mbe_mob;
            _ReceiptHeader.Sar_nic_no = custDetails.Mbe_nic;


            _ReceiptHeader.Sar_tot_settle_amt = Convert.ToDecimal(ucPayModes1.Amount.Text);
            _ReceiptHeader.Sar_comm_amt = 0;
            _ReceiptHeader.Sar_is_mgr_iss = false;
            _ReceiptHeader.Sar_esd_rate = 0;
            _ReceiptHeader.Sar_wht_rate = 0;
            _ReceiptHeader.Sar_epf_rate = 0;
            _ReceiptHeader.Sar_currency_cd = "LKR";
            _ReceiptHeader.Sar_uploaded_to_finance = false;
            _ReceiptHeader.Sar_act = true;
            _ReceiptHeader.Sar_direct_deposit_bank_cd = "";
            _ReceiptHeader.Sar_direct_deposit_branch = "";
            _ReceiptHeader.Sar_remarks = txtRemark.Text.Trim();
            _ReceiptHeader.Sar_is_used = false;
            _ReceiptHeader.Sar_ref_doc = txtJobNo.Text.Trim();
            _ReceiptHeader.Sar_ser_job_no = "";
            _ReceiptHeader.Sar_used_amt = 0;

            _ReceiptHeader.Sar_create_by = Session["UserID"].ToString();
            _ReceiptHeader.Sar_mod_by = Session["UserID"].ToString();
            _ReceiptHeader.Sar_session_id = Session["SessionID"].ToString();
            _ReceiptHeader.Sar_anal_1 = "";
            _ReceiptHeader.Sar_anal_2 = "";

            //if (radioButtonManual.Checked == true)
            //{
            //    _ReceiptHeader.Sar_anal_3 = "MANUAL";
            //    _ReceiptHeader.Sar_anal_8 = 1;
            //}
            //else
            //{
            //    _ReceiptHeader.Sar_anal_3 = "SYSTEM";
            //    _ReceiptHeader.Sar_anal_8 = 0;
            //}

            _ReceiptHeader.Sar_anal_4 = "";
            _ReceiptHeader.Sar_anal_5 = 0;
            _ReceiptHeader.Sar_anal_6 = 0;
            _ReceiptHeader.Sar_anal_7 = 0;
            _ReceiptHeader.Sar_anal_9 = 0;
            _ReceiptHeader.SAR_VALID_TO = _ReceiptHeader.Sar_receipt_date.AddDays(Convert.ToDouble(_valPd));
            //_ReceiptHeader.Sar_scheme = lblSchme.Text;
            _ReceiptHeader.Sar_inv_type = "";

            List<RecieptItem> _ReceiptDetailsSave = new List<RecieptItem>();
            Int32 _line = 0;
            foreach (RecieptItem line in ucPayModes1.RecieptItemList)
            {
                line.Sard_seq_no = _ReceiptHeader.Sar_seq_no;
                _line = _line + 1;
                line.Sard_line_no = _line;
                _ReceiptDetailsSave.Add(line);
            }

            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_cd = Session["UserDefProf"].ToString();
            masterAuto.Aut_cate_tp = "PC";
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "RECEIPT";
            masterAuto.Aut_number = 5;//what is Aut_number
            masterAuto.Aut_start_char = "DISP";
            masterAuto.Aut_year = null;

            DataTable _pcInfo = new DataTable();
            _pcInfo = CHNLSVC.Sales.GetProfitCenterTable(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());


            MasterAutoNumber masterAutoRecTp = new MasterAutoNumber();
            masterAutoRecTp.Aut_cate_cd = Session["UserDefProf"].ToString();
            masterAutoRecTp.Aut_cate_tp = "PC";
            masterAutoRecTp.Aut_direction = null;
            masterAutoRecTp.Aut_modify_dt = null;

            if (_pcInfo.Rows[0]["mpc_ope_cd"].ToString() == "INV_LRP" && Session["UserCompanyCode"].ToString() == "LRP")
            {
                masterAutoRecTp.Aut_moduleid = "REC_LRP";
            }
            else
            {
                masterAutoRecTp.Aut_moduleid = "RECEIPT";
            }
            masterAutoRecTp.Aut_number = 5;//what is Aut_number
            masterAutoRecTp.Aut_start_char = "DISP";
            masterAutoRecTp.Aut_year = null;

            //if (dgvItem.Rows.Count > 0)
            //{
            //    _SerHeader.Tuh_usrseq_no = CHNLSVC.Inventory.GetSerialID();  //CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "RECEIPT", 0, Session["UserCompanyCode"].ToString());
            //    _SerHeader.Tuh_usr_id = Session["UserID"].ToString();
            //    _SerHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
            //    _SerHeader.Tuh_session_id = Session["SessionID"].ToString();
            //    _SerHeader.Tuh_cre_dt = Convert.ToDateTime(dtpRecDate.Text).Date;
            //    if (_invType == "HS")
            //    {
            //        _SerHeader.Tuh_doc_tp = "RECEIPT";
            //    }
            //    else
            //    {
            //        _SerHeader.Tuh_doc_tp = "DO";
            //    }
            //    _SerHeader.Tuh_direct = false;
            //    if (_isRes == false)
            //    {
            //        _SerHeader.Tuh_ischek_itmstus = false;
            //    }
            //    else
            //    {
            //        _SerHeader.Tuh_ischek_itmstus = true;
            //    }
            //    _SerHeader.Tuh_ischek_simitm = true;
            //    _SerHeader.Tuh_ischek_reqqty = true;


            //    if (txtRecType.Text == "VHREG" || txtRecType.Text == "VHINS")
            //    {
            //        _SerHeader.Tuh_doc_no = _invNo;
            //    }
            //    else
            //    {
            //        _SerHeader.Tuh_doc_no = "na";
            //    }


            //    foreach (ReptPickSerials line in _ResList)
            //    {
            //        line.Tus_usrseq_no = _SerHeader.Tuh_usrseq_no;
            //        line.Tus_cre_by = Session["UserID"].ToString();
            //        _tempSerialSave.Add(line);
            //    }
            //}

            string QTNum;

            List<RecieptHeader> otest = new List<RecieptHeader>();
            otest.Add(_ReceiptHeader);
            DataTable dt = GlobalMethod.ToDataTable(_ReceiptDetailsSave);
            DataTable d2t = GlobalMethod.ToDataTable(otest);

            List<HpSheduleDetails> _sheduleDetails = new List<HpSheduleDetails>();
            List<GiftVoucherPages> _gvDetails = new List<GiftVoucherPages>();

            row_aff = (Int32)CHNLSVC.Sales.SaveNewReceipt(_ReceiptHeader, _ReceiptDetailsSave, masterAuto, _SerHeader, _tempSerialSave, _tempRegSave, _tempInsSave, _sheduleDetails, null, masterAutoRecTp, _gvDetails, out QTNum);

            //if (radioButtonManual.Checked == true)
            //{
            //    if (Session["UserDefLoca"].ToString() != Session["UserDefProf"].ToString())
            //    {
            //        CHNLSVC.Inventory.UpdateManualDocNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "MDOC_AVREC", Convert.ToInt32(txtReference.Text), QTNum);
            //    }
            //}

            //if (radioButtonSystem.Checked == true)
            //{
            //    if (Session["UserDefLoca"].ToString() != Session["UserDefProf"].ToString())
            //    {
            //        CHNLSVC.Inventory.UpdateManualDocNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "SDOC_AVREC", Convert.ToInt32(txtReference.Text), QTNum);
            //    }
            //}

            if (row_aff == 1)
            {
                //if (radioButtonManual.Checked == true)
                //{
                string msg = "Successfully created.Receipt No: " + QTNum;
                DisplayMessage(msg, 3);
                mpPayment.Hide();
                //Clear_Data();
                //btnSave.Enabled = true;
                DisposalHeader jobEntry = new DisposalHeader();
                jobEntry = CHNLSVC.Inventory.GET_DISPOSAL_JOB_HEADER(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtJobNo.Text, "ALL");
                //jobEntry.Dh_recipt_no = QTNum;
                jobEntry.Dh_chg += Convert.ToDecimal(ucPayModes1.Amount.Text);
                jobEntry.Dh_mod_by = Session["UserID"].ToString();
                jobEntry.Dh_mod_dt = DateTime.Now;
                jobEntry.Dh_mod_session = Session["SessionID"].ToString();
                CHNLSVC.Inventory.Save_Disposal_Hdr(jobEntry);

                //Immediate Print after Save  -- Lakshika
                Session["GlbReportType"] = "";
                Session["documntNo"] = QTNum;//txtRecNo.Text;
                Session["GlbReportName"] = "ReceiptPrints_n.rpt";
                BaseCls.GlbReportHeading = "Receipt Print Report";

                string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                clsSales obj = new clsSales();
                obj.Receipt_print_n(Session["documntNo"].ToString());
                PrintPDF(targetFileName, obj.recreport1_n);

                string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);


                // return;
                // }
                //else
                //{
                //    string msg = "Successfully created.Receipt No: " + QTNum;
                //    DisplayMessage(msg, 3);
                //    //Clear_Data();
                //    //btnSave.Enabled = true;

                //    //Immediate Print after Save -- Lakshika
                //    Session["GlbReportType"] = "";
                //    Session["documntNo"] = QTNum;//txtRecNo.Text;
                //    Session["GlbReportName"] = "ReceiptPrints_n.rpt";
                //    BaseCls.GlbReportHeading = "Receipt Print Report";

                //    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                //    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                //    //clsSales obj = new clsSales();
                //    //obj.Receipt_print_n(Session["documntNo"].ToString());
                //    //PrintPDF(targetFileName, obj.recreport1_n);

                //    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                //    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                //    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                //    // return;
                //}
            }
            else
            {
                if (!string.IsNullOrEmpty(QTNum))
                {
                    DisplayMessage(QTNum, 4);
                    //btnSave.Enabled = true;
                    //setButtionEnable(true, btnSave, "confSave");


                }
                else
                {
                    DisplayMessage("Creation Fail.");
                    //btnSave.Enabled = true;
                }
            }
        }

        protected void lbtnRecSave_Click(object sender, EventArgs e)
        {
            decimal _wkNo = 0;
            int _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(txtDate.Text).Date, out _wkNo, Session["UserCompanyCode"].ToString());

            if (_weekNo == 0)
            {
                DisplayMessage("Week Definition is still not setup for current date.Please contact retail accounts dept.");
                return;
            }
            bool _allowCurrentTrans = false;


            //if (!IsAllowBackDateForModuleNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserDefProf"].ToString(), this.Page, txtDate.Text, btnValidTo, lblBackDateInfor, "m_Trans_Sales_SalesInvoice", out  _allowCurrentTrans))
            //{
            //    if (Convert.ToDateTime(txtDate.Text).Date != DateTime.Now.Date)
            //    {
            //        //dtpRecDate.Enabled = true;
            //        DisplayMessage("Back date not allow for selected date!");
            //        txtDate.Focus();
            //        return;
            //    }
            //}
            //else
            //{
            //    //dtpRecDate.Enabled = true;
            //    DisplayMessage("Back date not allow for selected date!");
            //    txtDate.Focus();
            //    return;
            //}

            SaveReceiptHeader();
        }

        protected bool IsAllowBackDateForModuleNew(string _com, string _loc, string _pc, System.Web.UI.Page _page, string _backdate, LinkButton _imgcontrol, Label _lblcontrol, String moduleName, out bool _allowCurrentTrans)
        {
            string _outmsg = "";
            BackDates _bdt = new BackDates();
            _imgcontrol.Visible = false;
            _allowCurrentTrans = false;
            string _filename = string.Empty;

            if (String.IsNullOrEmpty(moduleName))
            {
                _filename = _page.AppRelativeVirtualPath.Substring(_page.AppRelativeVirtualPath.LastIndexOf("/") + 1).Replace(".aspx", "").ToUpper();
            }
            else
            {
                _filename = moduleName;
            }
            bool _isAllow = CHNLSVC.General.IsAllowBackDateForModule(_com, _loc, _pc, _filename, _backdate, out _bdt);
            if (_isAllow == true)
            {
                _imgcontrol.Visible = true;
                if (_bdt != null) _outmsg = "This module back dated. Valid period is from " + _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy") + " date to " + _bdt.Gad_act_to_dt.Date.ToString("dd/MMM/yyyy") + " date.";
            }

            if (_bdt == null)
            {
                _allowCurrentTrans = true;
            }
            else
            {
                if (_bdt.Gad_alw_curr_trans == true)
                {
                    _allowCurrentTrans = true;
                }
            }

            //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _outmsg);
            _lblcontrol.Text = _outmsg;

            return _isAllow;
        }

        protected void btnSchAdvClose_Click(object sender, EventArgs e)
        {
            Session["SEARCH"] = null;
            txtSearchbyword.Text = "";
            mpSearchAdance.Hide();
        }

        protected void lbtnClose_mpSearch_Click(object sender, EventArgs e)
        {
            Session["SEARCH2"] = null;
            txtSearchbyword2.Text = "";
            mpSearch.Hide();
        }

        public void PrintPDF(string targetFileName, ReportDocument _rpt)
        {
            try
            {
                ReportDocument rptDoc = (ReportDocument)_rpt;
                DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                diskOpts.DiskFileName = targetFileName;
                rptDoc.ExportOptions.DestinationOptions = diskOpts;
                rptDoc.Export();

                rptDoc.Close();
                rptDoc.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                DisplayMessage("Select a job number!!!");
                return;
            }

            List<InventoryHeader> adjustmentWithJobList = new List<InventoryHeader>();
            adjustmentWithJobList = CHNLSVC.Inventory.GetINTHDRByDispDoc(Session["UserCompanyCode"].ToString(), txtJobNo.Text);

            if (adjustmentWithJobList != null)
            {
                if (adjustmentWithJobList.Count < 1)
                {
                    DisplayMessage("No documnets to print!!!");
                    return;
                }
            }

            grdPrintDoc.DataSource = adjustmentWithJobList;
            grdPrintDoc.DataBind();

            lblPrint.Text = "doc_print";
            mpPrintPanel.Show();
        }

        protected void grdPymntDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdPymntDet.PageIndex = e.NewPageIndex;
                grdPymntDet.DataSource = null;
                List<RecieptHeader> recDetList = CHNLSVC.Sales.GetRecieptByRefDoc(Session["UserCompanyCode"].ToString(), txtJobNo.Text);
                grdPymntDet.DataSource = recDetList;
                grdPymntDet.DataBind();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
            }
        }


        protected void grdDspItms_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdDspItms.PageIndex = e.NewPageIndex;
                grdDspItms.DataSource = null;
                grdDspItms.DataSource = (List<DisposalItem>)Session["oItem"];
                grdDspItms.DataBind();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
            }
        }

        protected void grdLbtnprint_Click(object sender, EventArgs e)
        {
            if (hdfSaveCon.Value == "No")
            {
                return;
            }
            bool success = false;

            if (grdPrintDoc.Rows.Count == 0) return;

            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;

            if (row != null)
            {
                string invHdrDocNo = (row.FindControl("lblDocNo") as Label).Text;


                if (lblPrint.Text == "doc_print")
                {
                    Session["GlbReportType"] = "SCM1_DISPOSAL";
                    BaseCls.GlbReportDoc = invHdrDocNo;
                    BaseCls.GlbReportHeading = "OUTWARD DOC";
                    Session["GlbReportName"] = "Outward_Docs.rpt";
                    Session["GlbReportName"] = "Outward_Docs.rpt";

                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                    clsInventory obj = new clsInventory();
                    obj.printOutwardDocs(Session["GlbReportName"].ToString(), invHdrDocNo);
                    PrintPDF(targetFileName, obj._outdoc);

                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                    success = true;
                }
                else if (lblPrint.Text == "ser_print")
                {
                    Session["GlbReportType"] = "";
                    Session["documntNo"] = invHdrDocNo;
                    Session["GlbReportName"] = "serial_items.rpt";
                    BaseCls.GlbReportHeading = "Item Serials Report";

                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                    clsInventory obj = new clsInventory();
                    obj.get_Item_Serials(invHdrDocNo, Session["UserID"].ToString(), Session["UserDefLoca"].ToString());
                    PrintPDF(targetFileName, obj._serialItems);

                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                    success = true;
                }
            }
            if (success)
            {
                mpPrintPanel.Hide();
            }
        }

        protected void grdPrintDoc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdPrintDoc.PageIndex = e.NewPageIndex;
                grdPrintDoc.DataSource = null;
                List<InventoryHeader> adjustmentWithJobList = new List<InventoryHeader>();
                adjustmentWithJobList = CHNLSVC.Inventory.GetINTHDRByDispDoc(Session["UserCompanyCode"].ToString(), txtJobNo.Text);
                grdPrintDoc.DataSource = adjustmentWithJobList;
                grdPrintDoc.DataBind();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
            }
        }

        protected void btnSerialPrint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNo.Text))
            {
                DisplayMessage("Select a job number!!!");
                return;
            }

            List<InventoryHeader> adjustmentWithJobList = new List<InventoryHeader>();
            adjustmentWithJobList = CHNLSVC.Inventory.GetINTHDRByDispDoc(Session["UserCompanyCode"].ToString(), txtJobNo.Text);

            if (adjustmentWithJobList != null)
            {
                if (adjustmentWithJobList.Count < 1)
                {
                    DisplayMessage("No documnets to print!!!");
                    return;
                }
            }

            grdPrintDoc.DataSource = adjustmentWithJobList;
            grdPrintDoc.DataBind();

            lblPrint.Text = "ser_print";
            mpPrintPanel.Show();
        }

        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            ViewState["SEARCH2"] = null;
            txtSearchbyword2.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DisposalJOb);
            DataTable result = CHNLSVC.CommonSearch.SEARCH_DISPOSAL_JOB(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
            grdResult2.DataSource = result;
            grdResult2.DataBind();
            lblvalue2.Text = "DisposalJOb";
            BindUCtrlDDLData2(result);
            ViewState["SEARCH2"] = result;
            mpSearch.Show();
            txtSearchbyword2.Focus();
        }

        protected void grdDspItms_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string item = e.Row.Cells[1].Text;
                string serial = e.Row.Cells[4].Text;
                foreach (Button button in e.Row.Cells[0].Controls.OfType<Button>())
                {
                    if (button.CommandName == "Delete")
                    {
                        button.Attributes["onclick"] = "if(!confirm('Do you want to delete item " + item + " with serial " + serial + " ?')){ return false; };";
                    }
                }
            }
        }

        protected void grdDspItms_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            bool success = false;

            int index = Convert.ToInt32(e.RowIndex);

            List<ReptPickSerials> _serList = (List<ReptPickSerials>)ViewState["SerialList"];
            GridViewRow row = this.grdDspItms.Rows[index];
            if (row != null)
            {
                string itmCd = (row.FindControl("grdLblItm") as Label).Text;
                string itmser1 = (row.FindControl("grdLblSerilaI") as Label).Text;
                string itmStus = (row.FindControl("grdLblStus") as Label).Text;
                string itmSerID = (row.FindControl("grdLblSerilaID") as Label).Text;
                if (oItem.FindAll(x => x.Idd_itm_cd == itmCd.Trim() && x.Idd_ser_id == Convert.ToInt32(itmSerID.Trim()) && x.Idd_ser_1 == itmser1.Trim() && x.Idd_stus_desc == itmStus.Trim()).Count > 0)
                {
                    oItem.RemoveAll(x => x.Idd_itm_cd == itmCd.Trim() && x.Idd_ser_id == Convert.ToInt32(itmSerID.Trim()) && x.Idd_ser_1 == itmser1.Trim() && x.Idd_stus_desc == itmStus.Trim());
                    //_serList.RemoveAll(x => x.Tus_itm_cd == itmCd.Trim() && x.Tus_ser_id== Convert.ToInt32(itmSerID.Trim()) && x.Tus_ser_1 == itmser1.Trim() && x.Tus_itm_stus_Desc == itmStus.Trim());
                    Session["oItem"] = oItem;
                    ViewState["SerialList"] = _serList;
                    grdDspItms.DataSource = oItem;
                    grdDspItms.DataBind();
                    if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16057))
                    {
                        grdDspItms.Columns[7].Visible = true;
                    }
                    else
                    {
                        grdDspItms.Columns[7].Visible = false;
                    }
                    success = true;
                }
            }
            if (success)
            {
                DisplayMessage("Disposal item removed successfully. Please update the job to make the removal permanent", 3);
            }
        }

        protected void chkJOB_CheckedChanged(object sender, EventArgs e)
        {
            if (chkJOB.Checked == true)
            {
                List<ReptPickHeader> _hdr = new List<ReptPickHeader>();
                ReptPickHeader _obj = new ReptPickHeader();
                _obj.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                _obj.Tuh_usr_id = "";
                _obj.Tuh_doc_tp = "DISP";
                _obj.Tuh_isdirect = true;
                _obj.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                _hdr = CHNLSVC.Inventory.GetReportPickHdrDetails(_obj);
                //List<ReptPickSerials> _doitemserials = new List<ReptPickSerials>();
                //_doitemserials = Session["gvSerData"] as List<ReptPickSerials>;
                //txtpdaloc.Text = Session["UserDefLoca"].ToString();
                ////if (_doitemserials.Count == 0)
                ////{
                //////    string _Msg = "Only DO Items can be sent to PDA";
                ////    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);

                ////    return;
                ////}
                //txtdocname.Text = txtInvoice.Text;
                grdPDACurrentJob.DataSource = _hdr;
                grdPDACurrentJob.DataBind();
                MPMULTJobs.Show();
            }
            else
            {
                MPPDA.Hide();
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow dgvr in grdPDACurrentJob.Rows)
            {
                string _doc = (dgvr.FindControl("col_doc") as Label).Text;

                string _seq = (dgvr.FindControl("col_SEQ") as Label).Text;

                CheckBox chk = (CheckBox)dgvr.FindControl("chk_job");
                if (chk.Checked)
                {
                    // seqNo.Value = _seq;
                    Get_selectedjob(_seq, _doc);

                }


            }
        }
        private void Get_selectedjob(string _seq, string _doc)
        {
            Session.Add("PDADocNo", _doc);
            // List<ReptPickSerials> scanSerList = new List<ReptPickSerials>();
            scanSerList = CHNLSVC.Sales.Get_TEMP_PICK_SER_BY_loc(Session["UserCompanyCode"].ToString(), _doc, Session["UserDefLoca"].ToString());
            //if (_seriallist != null)
            //{
            //    foreach (ReptPickSerials _serial in _seriallist)
            //    {
            //        AddItemGrid(_serial.Tus_itm_cd, _serial.Tus_ser_1, txtLocation.Text, _serial.Tus_itm_stus, Session["UserDefLoca"].ToString(), _serial.Tus_qty.ToString(),true);
            //    }

            //}
            List<DisposalItem> oDispItems = new List<DisposalItem>();
            oDispItems = Session["oItem"] as List<DisposalItem>;
            if (scanSerList != null)
            {
                if (scanSerList.Count > 0)
                {
                    string _itm = string.Empty;
                    var _scanItems = scanSerList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                    foreach (var _ser in _scanItems)
                    {
                        var _filter = oDispItems.Where(x => x.Idd_itm_cd == _ser.Peo.Tus_itm_cd && x.Idd_stus == _ser.Peo.Tus_itm_stus).ToList();
                        if (_filter.Count == 0)
                        {
                            _itm = _itm + "," + _ser.Peo.Tus_itm_cd;
                        }
                    }
                    if (!string.IsNullOrEmpty(_itm))
                    {
                        string _Msg = "New Item-" + _itm;
                        DisplayMessage(_Msg);
                        return;
                    }
                    foreach (DisposalItem itm in oDispItems)
                    {
                        List<ReptPickSerials> itmSerList = new List<ReptPickSerials>();
                        MasterItem mstItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Idd_itm_cd);

                        if (mstItm.Mi_is_ser1 == 1)
                        {
                            itmSerList = scanSerList.Where(x => x.Tus_itm_cd == itm.Idd_itm_cd && x.Tus_itm_stus == itm.Idd_stus && x.Tus_qty == itm.Idd_qty && x.Tus_ser_id == itm.Idd_ser_id && x.Tus_ser_1 == itm.Idd_ser_1 && x.Tus_loc == itm.Idd_cur_loc).ToList();
                        }
                        else
                        {
                            itmSerList = scanSerList.Where(x => x.Tus_itm_cd == itm.Idd_itm_cd && x.Tus_itm_stus == itm.Idd_stus && x.Tus_qty <= itm.Idd_qty && x.Tus_loc == itm.Idd_cur_loc).ToList();
                        }
                        if (itmSerList != null)
                        {
                            if (itmSerList.Count == 1)
                            {
                                itm.Idd_scan_stus = 1;
                                itm.Idd_scan_qty = scanSerList.Where(x => x.Tus_itm_cd == itm.Idd_itm_cd && x.Tus_itm_stus == itm.Idd_stus && x.Tus_qty <= itm.Idd_qty && x.Tus_loc == itm.Idd_cur_loc).FirstOrDefault().Tus_qty;

                            }
                            else
                            {
                                if (mstItm.Mi_is_ser1 == 0)
                                {
                                    decimal disposumqty = oDispItems.Where(a => a.Idd_itm_cd == itm.Idd_itm_cd && a.Idd_stus == itm.Idd_stus && a.Idd_act == 1).Sum(a => a.Idd_qty);
                                    itmSerList = scanSerList.Where(x => x.Tus_itm_cd == itm.Idd_itm_cd && x.Tus_itm_stus == itm.Idd_stus).ToList();
                                    decimal sum_ser_qty = itmSerList.Sum(a => a.Tus_qty);
                                    if (disposumqty < sum_ser_qty)
                                    {
                                        string _Msg = "Item exceeded -" + itm.Idd_itm_cd;
                                        DisplayMessage(_Msg);
                                        return;
                                    }
                                }

                            }
                        }
                        //else
                        //{
                        //    string _Msg = "Item exceeded -" + itm.Idd_itm_cd;
                        //    DisplayMessage(_Msg);
                        //    return;
                        //}
                    }
                }
            }
            if (oDispItems != null)
            {
                grdDspItms.DataSource = oDispItems;
                if (oDispItems.FirstOrDefault().Idd_stus == "DISPO")
                {
                    btnChngStus.Enabled = false;
                }
                else
                {
                    btnChngStus.Enabled = true;
                }
            }
            else
            {
                grdDspItms.DataSource = new int[] { };
            }
            grdDspItms.DataBind();

        }
    }


}