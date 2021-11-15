using FastForward.SCMPDA.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Globalization;
using FF.BusinessObjects.General;
using System.Text;
namespace FastForward.SCMPDA
{
    public partial class CreateOutJobNew : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string company = Session["UserCompanyName"].ToString();
                    string autonumber = string.Empty;
                    string doctype = (string)Session["DOCTYPE"];
                    string iscreatejob = Session["CreateJobNumber"] as string;
                    string currjobno = string.Empty;
                    string ischeckbuttinclick = (string)Session["CHECKBUTTON"];
                    PopupConfBox.Hide();
                    if (doctype == "AOD" || doctype == "PDA" || doctype == "MRNA" || doctype=="DO")
                    {
                        btnProcess.Visible = true;
                        btnPrint.Visible = false;
                    }
                    else
                    {
                        btnProcess.Visible = false;
                        btnPrint.Visible = false;
                    }
                    string warecom = (string)Session["WAREHOUSE_COMPDA"];
                    string wareloc = (string)Session["WAREHOUSE_LOCPDA"];
                    string loadingpoint = (string)Session["LOADING_POINT"];
                    Session["PrintDoc"] = null;
                    if (Request.QueryString["JobNo"] != null)
                    {
                        currjobno = Request.QueryString["JobNo"].ToString();
                    }

                    if (!string.IsNullOrEmpty(ischeckbuttinclick))
                    {
                        string _backsessionjob = (string)Session["DOCNO"];
                        currjobno = _backsessionjob;
                    }

                    if (!string.IsNullOrEmpty(currjobno))
                    {
                        Session["DOCNO"] = currjobno;
                        lbljobno.Text = currjobno ;
                    }
                    else
                    {
                        Int32 serialno = 0;
                        serialno = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), doctype, 1, Session["UserCompanyName"].ToString());
                        Session["SEQNO"] = serialno;
                        autonumber = Session["UserDefLoca"].ToString() + "-" + doctype + serialno + Session["ToLocation"];
                        Session["DOCNO"] = autonumber;
                        lbljobno.Text =  autonumber;
                    }
                    Int32 userseq = Convert.ToInt32(Session["SEQNO"].ToString());
                    MasterLocation locDetails = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString());
                    Session["LOCISSERIAL"] = locDetails.Ml_is_serial.ToString();
                    txtitemcode.Focus();
                    //serialdv.Visible = false;
                    //serial2dv.Visible = false;
                    //serial3dv.Visible = false;
                    //nonserdv.Visible = false;
                    //LoadScannedItemQty();
                    loadScanedQty();
                    getTotalDocQty();
                    GetLastScanSerial();
                    //LoadGrid();
                    itemlistdiv.Visible = false;
                    DataTable dtserials = new DataTable();
                    dtserials.Columns.AddRange(new DataColumn[1] { new DataColumn("No") });
                    ViewState["SERIALTABLE"] = dtserials;
                    chkallitems.Checked = true;
                    chkallbin.Checked = true;
                    chkallstatus.Checked = true;
                    Session["FINISHED"]="0";
                    DataTable dtdoccheck = CHNLSVC.Inventory.IsDocAvailableWithSeq(Session["UserCompanyName"].ToString(), userseq, Session["UserDefLoca"].ToString(), warecom, wareloc, loadingpoint);
                    if (iscreatejob == "CreateJobNumber")
                    {
                        outLoc.Text = (string)Session["ToLocation"];
                   }else{

                       outLoc.Text = dtdoccheck.Rows[0]["TUH_REC_LOC"].ToString();
                   }
                    Session["Error"] = string.Empty;
                    if (dtdoccheck.Rows.Count > 0 && dtdoccheck.Rows[0]["TUS_FIN_STUS"].ToString() == "1")
                    {
                        Session["FINISHED"] = "1";
                        divalert.Visible = true;
                        lblalert.Text = "Already finished document.";
                        Session["Error"] = "Already finished document.";
                        return;
                    }
                    if (dtdoccheck.Rows.Count > 0)
                    {
                        Session["DOCNO"] = dtdoccheck.Rows[0]["TUH_DOC_NO"].ToString();
                        Session["DOCTYPE"] = dtdoccheck.Rows[0]["TUH_DOC_TP"].ToString();
                        doctype = (string)Session["DOCTYPE"];
                    }
                    string uperror = string.Empty;
                    if (doctype == "DO" && dtdoccheck.Rows.Count > 0 && dtdoccheck.Rows[0]["TUH_IS_TAKE_RES"].ToString() == "1")
                    {
                        bool updateReservation = CHNLSVC.Inventory.updateItemReservations(userseq, "0", Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(),Session["GlbUserSessionID"].ToString(), out uperror);
                        if (!updateReservation)
                        {
                            divalert.Visible = true;
                            lblalert.Text = uperror;
                            Session["Error"] = uperror;
                            return;
                        }
                    }

                    if (doctype == "SOA" || doctype == "SO")
                    {
                        InventoryRequest _invreqSup = new InventoryRequest();
                        if (doctype == "SOA")
                        {
                            InventoryRequest _invreqSupSo = CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = Session["DOCNO"].ToString() }).FirstOrDefault();
                            _invreqSup = CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = _invreqSupSo.Itr_ref }).FirstOrDefault();
                        }
                        if (doctype == "SO")
                        {
                            _invreqSup = CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = Session["DOCNO"].ToString() }).FirstOrDefault();
                        }

                        if (_invreqSup != null)
                        {
                            if (_invreqSup.Itr_tp == "SO")
                            {
                                MasterBusinessEntity _mstBusEntity = CHNLSVC.Sales.GetCustomerProfileByCom(_invreqSup.Itr_bus_code, null, null, null, null,
                                    company);
                                if (_mstBusEntity != null)
                                {
                                    if (_mstBusEntity.Mbe_is_suspend == true)
                                    {
                                        divalert.Visible = true;
                                        lblalert.Text = "This customer already suspended !";
                                        return;
                                    }
                                }
                            }
                        }

                    }
                    if (doctype == "DO" && Session["DOCNO"].ToString()!="")
                    {
                        DataTable dt = CHNLSVC.Sales.GetSalesHdr(Session["DOCNO"].ToString());
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["SAH_INV_NO"] != DBNull.Value && dt.Rows[0]["SAH_CUS_CD"] != DBNull.Value)
                            {
                                MasterBusinessEntity _mstBusEntity = CHNLSVC.Sales.GetCustomerProfileByCom(dt.Rows[0]["SAH_CUS_CD"].ToString(), null, null, null, null,
                                   company);
                                if (_mstBusEntity != null)
                                {
                                    if (_mstBusEntity.Mbe_is_suspend == true)
                                    {
                                        divalert.Visible = true;
                                        lblalert.Text = "This customer already suspended !";
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void SetScrollTop()
        {
            try
            {
                Page.SetFocus(this.dvoutjobs.ClientID);
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
        }
        protected void lbtnalert_Click(object sender, EventArgs e)
        {
            try
            {
                divalert.Visible = false;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
        }

        protected void lbtninfo_Click(object sender, EventArgs e)
        {
            try
            {
                Divinfo.Visible = false;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            try
            {
                Session["CHECKBUTTON"] = null;
                string isCurrent = (string)Session["CurrentJobb"];
                if (isCurrent == "CurrentJobb")
                {
                    string doctype = (string)Session["DOCTYPE"];
                    Response.Redirect("CurrentJobs.aspx?DocType=" + doctype, false);
                }
                else
                {
                    Response.Redirect("CreateJob.aspx");
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
        }

        private void DivsHide()
        {
            try
            {
                divalert.Visible = false;
                Divinfo.Visible = false;
                divokjob.Visible = false;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
        }

        private void ItemValidateAndSetStatus()
        {
            try
            {
                DataTable dtitemdata = CHNLSVC.Inventory.GetItemData(txtitemcode.Text.ToUpper().Trim());

                if (dtitemdata.Rows.Count > 0)
                {
                    foreach (DataRow ddritem in dtitemdata.Rows)
                    {
                        string isserialized1 = ddritem["mi_is_ser1"].ToString();
                        string isserialized2 = ddritem["mi_is_ser2"].ToString();
                        string isserialized3 = ddritem["mi_is_ser3"].ToString();

                        Session["SERIALIZED"] = isserialized1;
                        Session["SER2"] = isserialized2;
                        Session["SER3"] = isserialized3;
                        Session["ISITEMACTIVE"] = ddritem["mi_act"].ToString();

                        Session["UOM"] = ddritem["mi_itm_uom"].ToString();
                        Session["ITEM"] = txtitemcode.Text.ToUpper().Trim();

                        if (isserialized1 == "1")
                        {
                            txtqty.ReadOnly = true;
                        }
                        else
                        {
                            txtqty.ReadOnly = false;
                        }

                        if (isserialized3 == "1")
                        {
                            //serialdv.Visible = true;
                            //serial2dv.Visible = true;
                            //serial3dv.Visible = true;
                            //nonserdv.Visible = false;
                        }
                        else if (isserialized2 == "1")
                        {
                            //serialdv.Visible = true;
                            //serial2dv.Visible = true;
                            //serial3dv.Visible = false;
                            //nonserdv.Visible = false;
                        }
                        else if (isserialized1 == "1")
                        {
                            //serialdv.Visible = true;
                            //serial2dv.Visible = false;
                            //serial3dv.Visible = false;
                            //nonserdv.Visible = false;
                        }
                        else
                        {
                            //serialdv.Visible = false;
                            //serial2dv.Visible = false;
                            //serial3dv.Visible = false;
                            //nonserdv.Visible = true;
                        }
                    }
                }
                else
                {
                    //serialdv.Visible = false;
                    //serial2dv.Visible = false;
                    //serial3dv.Visible = false;
                    //nonserdv.Visible = false;

                    divalert.Visible = true;
                    //lblalert.Text = ex.Message;
                    lblalert.Text = "Invalid item code.";
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        protected void txtitemcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DivsHide();
                Session["SERIALIZED"] = null;
                string iscurrent = (string)Session["CurrentJobb"];
                string docNo = (string)Session["DOCNO"];
                StockQty.Text = "0";
                Int32 userseq = Convert.ToInt32(Session["SEQNO"].ToString());
                if (iscurrent == "CurrentJobb")
                {
                    DataTable itemValidate = CHNLSVC.Inventory.GetItemDataInTempPickItemWithSeq(txtitemcode.Text.ToUpper().Trim(), userseq);
                    if (itemValidate.Rows.Count == 0)
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Please enter valid item code in document.!";
                        txtitemcode.Text = "";
                        txtitemcode.Focus();
                        
                        return;
                    }
                    decimal totalPickItmQty = 0;
                    decimal totalReqItmQty = 0;
                    if (itemValidate.Rows.Count > 0)
                    {
                        foreach (DataRow clm in itemValidate.Rows)
                        {
                            totalPickItmQty = totalPickItmQty + Convert.ToDecimal(clm["TUI_PIC_ITM_QTY"].ToString());
                            totalReqItmQty = totalReqItmQty + Convert.ToDecimal(clm["TUI_REQ_ITM_QTY"].ToString());
                        }
                    }
                    itmScnQty.Text = totalPickItmQty.ToString();
                    itmDocQty.Text = totalReqItmQty.ToString();
                    if (totalReqItmQty == 0)
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Item request qunatity is zero.!";
                        txtitemcode.Text = "";
                        txtitemcode.Focus();
                        
                        return;
                    }
                    if (totalPickItmQty == totalReqItmQty)
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Cannot exceed request item quantity.!";
                        //txtitemcode.Text = "";
                        txtitemcode.Focus();
                        return;
                    }
                }
                else
                {
                    DataTable itemValidate = CHNLSVC.Inventory.GetItemDataInTempPickItemWithSeq(txtitemcode.Text.ToUpper().Trim(), userseq);
                    if (itemValidate.Rows.Count > 0)
                    {
                        decimal totalPickItmQty = 0;
                        decimal totalReqItmQty = 0;
                        if (itemValidate.Rows.Count > 0)
                        {
                            foreach (DataRow clm in itemValidate.Rows)
                            {
                                totalPickItmQty = totalPickItmQty + Convert.ToDecimal(clm["TUI_PIC_ITM_QTY"].ToString());
                                totalReqItmQty = totalReqItmQty + Convert.ToDecimal(clm["TUI_REQ_ITM_QTY"].ToString());
                            }
                        }
                        itmScnQty.Text = totalPickItmQty.ToString();
                        itmDocQty.Text = totalReqItmQty.ToString();
                    }
                    else
                    {
                        itmScnQty.Text = "0";
                        itmDocQty.Text = "0";
                    }
                }
                ItemValidateAndSetStatus();
                string isser = (string)Session["SERIALIZED"];
                string locserialcheck = (string)Session["LOCISSERIAL"];
                if (isser != "1" || locserialcheck == "False")
                {
                    DataTable dtbincode = CHNLSVC.Inventory.LoadDistinctBins(Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString(), txtitemcode.Text.ToUpper().Trim());
                    if (isser == "1" && locserialcheck == "False" && dtbincode.Rows.Count == 0)
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Item stock not available.";
                        //txtitemcode.Text = "";
                        txtitemcode.Focus();

                        return;
                    }

                    if (dtbincode.Rows.Count > 0)
                    {
                        ddlbincode.DataSource = dtbincode;
                        ddlbincode.DataTextField = "INB_BIN";
                        ddlbincode.DataValueField = "INB_BIN";
                        ddlbincode.DataBind();

                        if (ddlbincode.Items.Count > 1)
                        {
                            ddlbincode.Items.Insert(0, new ListItem("Select", ""));
                        }
                        ddlbincode_SelectedIndexChanged(null, null);
                    }
                }
                if (txtitemcode.Text != "")
                {
                    decimal itemStkQty = CHNLSVC.Inventory.getTotalStockQty(txtitemcode.Text.Trim().ToString(), Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString());
                    StockQty.Text = itemStkQty.ToString();
                }
                else
                {
                    StockQty.Text = "0";
                }
                if (isser == "1")
                {
                    txtqty.Text = "";
                    txtserialnumber1.Focus();
                }
                else
                {
                    txtbarcode.Focus();
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
        }

        protected void chkblkserial_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DivsHide();
                //if (chkblkserial.Checked == true)
                //{
                //    txtitemcode.ReadOnly = true;
                //}
                //else
                //{
                //    txtitemcode.ReadOnly = false;
                //}
                txtserialnumber1.Text = "";
                txtserialnumber1.Focus();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
        }

        protected void btkreset_Click(object sender, EventArgs e)
        {
            try
            {
                txtitemcode.Text = string.Empty;
                //chkblkserial.Checked = false;
                txtserialnumber1.Text = string.Empty;
                txtserialnumber2.Text = string.Empty;
                txtserialnum3.Text = string.Empty;
                txtitemcode.ReadOnly = false;
                lblscqty.Text = string.Empty;
                lstscnserial.Text = string.Empty;
                Session["LASTSCANSERIAL"] = null;
                Session["ITEM"] = null;
                Session["ITEMSTATUS"] = null;
                chkallitems.Checked = true;
                //chkbulkitems.Checked = false;
                chkallstatus.Checked = true;
                chkallbin.Checked = true;
                if (ddlitmstatus.SelectedIndex != -1)
                {
                    ddlitmstatus.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
        }
        private void CheckStockAvailability()
        {
            try
            {
                DivsHide();
                string isseriaitem = (string)Session["SERIALIZED"];

                if (isseriaitem == "1")
                {
                    DataTable dtserialavailable = CHNLSVC.Inventory.GetItemSerialAvailability(txtitemcode.Text.ToUpper().Trim(), txtserialnumber1.Text.Trim());

                    if (dtserialavailable.Rows.Count > 0)
                    {
                        Divinfo.Visible = true;
                        lblinfo.Text = "Serial is available in stock !!!";
                        return;
                    }
                    else
                    {
                        divalert.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private bool ValidateItems()
        {
            Int32 userseq = Convert.ToInt32(Session["SEQNO"].ToString());
            string doctype = (string)Session["DOCTYPE"];
            if (string.IsNullOrEmpty(txtitemcode.Text.Trim()))
            {
                divalert.Visible = true;
                lblalert.Text = "Please scan/enter item code !!!";
                txtitemcode.Focus();
                //SetScrollTop();
                return false;
            }
            
            if (txtitemcode.Text.ToString() != "" && txtserialnumber1.Text.ToString() != "")
            {
                if (txtitemcode.Text.ToString() == txtserialnumber1.Text.ToString())
                {
                    divalert.Visible = true;
                    txtserialnumber1.Text = "";
                    lblalert.Text = "Item code scan as serial!";
                    txtserialnumber1.Focus();
                    return false;

                }
            }
            string iscurrent = (string)Session["CurrentJobb"];
            string isseriaitem = (string)Session["SERIALIZED"];
            decimal qty=(txtqty.Text.Trim()!="")?Convert.ToDecimal(txtqty.Text.Trim()):0;
            DataTable itemValidate=new DataTable("tbl");
            if (iscurrent == "CurrentJobb") {
                string docNo = (string)Session["DOCNO"];
                itemValidate = CHNLSVC.Inventory.GetItemDataInTempPickItemWithSeq(txtitemcode.Text.ToUpper().Trim(), userseq);
                if (itemValidate.Rows.Count == 0)
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please enter valid item code in document.!";
                    txtitemcode.Text = "";
                    txtitemcode.Focus();
                    return false;
                }
                decimal totalPickItmQty=0;
                decimal totalReqItmQty=0;
                foreach (DataRow clm in itemValidate.Rows)
                {
                    totalPickItmQty = totalPickItmQty + Convert.ToDecimal(clm["TUI_PIC_ITM_QTY"].ToString());
                    totalReqItmQty = totalReqItmQty + Convert.ToDecimal(clm["TUI_REQ_ITM_QTY"].ToString());
                }
                if (totalReqItmQty == 0)
                {
                    divalert.Visible = true;
                    lblalert.Text = "Item request qunatity is zero.!";
                    txtitemcode.Text = "";
                    txtitemcode.Focus();
                    return false;
                }
                
                if (isseriaitem == "1")
                {
                    if (totalPickItmQty + 1 > totalReqItmQty)
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Cannot exceed request item quantity.!";
                        txtitemcode.Text = "";
                        txtitemcode.Focus();
                        return false;
                    }
                }
                else {
                    if (totalPickItmQty + qty > totalReqItmQty)
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Cannot exceed request item quantity.!";
                        txtqty.Text = "";
                        txtqty.Focus();
                        return false;
                    }
                }
            }
            string locserialcheck = (string)Session["LOCISSERIAL"];

            if (doctype != "STJO")
            {

                DataTable dtcurrstock = CHNLSVC.Inventory.CheckCurrentStockBalance(Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString(), txtitemcode.Text.ToUpper().Trim());

                if (dtcurrstock.Rows.Count == 0)
                {
                    divalert.Visible = true;
                    lblalert.Text = "No stock balance available !!!";
                    txtitemcode.Focus();
                    //SetScrollTop();
                    return false;
                }
                else
                {
                    foreach (DataRow ddritemrow in dtcurrstock.Rows)
                    {
                        Session["STOCKBALANCE"] = ddritemrow["INL_QTY"].ToString();
                    }
                }
            }
            string isitemactive = (string)Session["ISITEMACTIVE"];
            //Int16 actval = dtitemdata.Rows[0].Field<Int16>(6);
            //string isitemactive = dtitemdata.Rows[0][""].ToString();
            if (isitemactive != "1")
            {
                divalert.Visible = true;
                lblalert.Text = "Item is not active or scaned maximum quantity!";
                txtitemcode.Text = "";
                txtitemcode.Focus();
                //SetScrollTop();
                return false;
            }

            //string isseriaitem = (string)Session["SERIALIZED"];

            if (Convert.ToInt32(isseriaitem) == 0)
            {
                if (ddlbincode.SelectedItem !=null && ddlbincode.SelectedItem.Text == "Select")
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please select bin code !!!";
                    ddlbincode.Focus();
                    //SetScrollTop();
                    return false;
                }
            }

            if (Convert.ToInt32(isseriaitem) == 0)
            {
                if (ddlbincode.SelectedItem !=null && ddlitmstatus.SelectedItem.Text == "Select")
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please select item status !!!";
                    ddlitmstatus.Focus();
                    //SetScrollTop();
                    return false;
                }
            }

            if (isseriaitem == "1")
            {
                if (string.IsNullOrEmpty(txtserialnumber1.Text.Trim()))
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please scan serial number 1 !!!";
                    txtserialnumber1.Focus();
                    //SetScrollTop();
                    return false;
                }

                string serial2 = (string)Session["SER2"];
                if (serial2 == "1")
                {
                    if (string.IsNullOrEmpty(txtserialnumber2.Text.Trim()))
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Please scan serial number 2 !!!";
                        txtserialnumber2.Focus();
                        //SetScrollTop();
                        return false;
                    }
                }

                string serial3 = (string)Session["SER3"];
                if (serial3 == "1")
                {
                    if (string.IsNullOrEmpty(txtserialnum3.Text.Trim()))
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Please scan serial number 3 !!!";
                        txtserialnum3.Focus();
                        //SetScrollTop();
                        return false;
                    }
                }
                if (lbldocqty.Text.ToString() != "")
                {
                    if (lbldocqty.Text.ToString() != "0")
                    {
                        if (Convert.ToInt32(lblscqty.Text.ToString()) + 1 > Convert.ToInt32(lbldocqty.Text.ToString()))
                        {
                            divalert.Visible = true;
                            lblalert.Text = "You Cannot Exceed Doc Quantity ";
                            return false;
                        }
                    }
                   
                }
                string _userSeqNo = (string)Session["SEQNO"].ToString();
                DataTable temppickitems = CHNLSVC.Inventory.GetTepItems(Convert.ToInt32(_userSeqNo), txtitemcode.Text.ToUpper().Trim().ToString(), ddlitmstatus.SelectedValue.ToString());
                if (temppickitems.Rows.Count > 0)
                {
                    foreach (DataRow dtrw in temppickitems.Rows)
                    {
                        if (dtrw["TUI_PIC_ITM_CD"].ToString() == "0" || dtrw["TUI_PIC_ITM_CD"] == null)
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Invalid base item line !";
                            return false;
                        }
                    }
                }
               
                string stus = string.Empty;
                string bin = string.Empty;
                
                DataTable dtserialstus = new DataTable();
                if (locserialcheck == "True")
                {
                     if (txtserialnumber1.Visible == true)
                    {
                        dtserialstus = CHNLSVC.Inventory.LoadSerialStatus(Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString(), txtitemcode.Text.ToUpper().Trim(), txtserialnumber1.Text.Trim(), txtserialnumber2.Text.Trim(), txtserialnum3.Text.Trim(), 1);
                        if (dtserialstus.Rows.Count == 0)
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Invalid or already scanned serial number/numbers !!!";
                            txtserialnumber1.Text = "";
                            txtserialnumber1.Focus();
                            //SetScrollTop();
                            return false;
                        }
                        else
                        {

                            foreach (DataRow DDR1 in dtserialstus.Rows)
                            {
                                bin = DDR1["INS_BIN"].ToString();

                                stus = DDR1["INS_ITM_STUS"].ToString();


                            }
                            ddlbincode.DataSource = dtserialstus;
                            ddlbincode.DataTextField = "INS_BIN";
                            ddlbincode.DataValueField = "INS_BIN";
                            ddlbincode.DataBind();

                            ddlitmstatus.DataSource = dtserialstus;
                            ddlitmstatus.DataTextField = "INS_ITM_STUS";
                            ddlitmstatus.DataValueField = "INS_ITM_STUS";
                            ddlitmstatus.DataBind();

                            //ddlbincode.SelectedValue = bin;
                            //ddlbincode_SelectedIndexChanged(null, null);
                            //ddlitmstatus.SelectedValue = stus;
                            LoadCurrentQtyofBin();
                        }
                    }
                     else if ((txtserialnumber2.Visible == true) && (txtserialnumber1.Visible == true))
                     {
                         dtserialstus = CHNLSVC.Inventory.LoadSerialStatus(Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString(), txtitemcode.Text.ToUpper().Trim(), txtserialnumber1.Text.Trim(), txtserialnumber2.Text.Trim(), txtserialnum3.Text.Trim(), 2);
                         if (dtserialstus.Rows.Count == 0)
                         {
                             divalert.Visible = true;
                             lblalert.Text = "Invalid or already scanned serial number/numbers !!!";
                             txtserialnumber1.Text = "";
                             txtserialnumber1.Focus();
                             // SetScrollTop();
                             return false;
                         }
                         else
                         {
                             foreach (DataRow DDR2 in dtserialstus.Rows)
                             {
                                 stus = DDR2["INS_ITM_STUS"].ToString();
                                 bin = DDR2["INS_BIN"].ToString();
                             }
                             ddlbincode.DataSource = dtserialstus;
                             ddlbincode.DataTextField = "INS_BIN";
                             ddlbincode.DataValueField = "INS_BIN";
                             ddlbincode.DataBind();

                             ddlitmstatus.DataSource = dtserialstus;
                             ddlitmstatus.DataTextField = "INS_ITM_STUS";
                             ddlitmstatus.DataValueField = "INS_ITM_STUS";
                             ddlitmstatus.DataBind();

                             ddlbincode.SelectedValue = bin;
                             ddlbincode_SelectedIndexChanged(null, null);
                             ddlitmstatus.SelectedValue = stus;
                             LoadCurrentQtyofBin();
                         }
                     }
                     else if ((txtserialnum3.Visible == true) && (txtserialnumber2.Visible == true) && (txtserialnumber1.Visible == true))
                    {
                        dtserialstus = CHNLSVC.Inventory.LoadSerialStatus(Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString(), txtitemcode.Text.ToUpper().Trim(), txtserialnumber1.Text.Trim(), txtserialnumber2.Text.Trim(), txtserialnum3.Text.Trim(), 3);
                        if (dtserialstus.Rows.Count == 0)
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Invalid or already scanned serial number/numbers !!!";
                            txtserialnumber1.Text = "";
                            txtserialnumber1.Focus();
                            // SetScrollTop();
                            return false;
                        }
                        else
                        {
                            foreach (DataRow DDR3 in dtserialstus.Rows)
                            {
                                stus = DDR3["INS_ITM_STUS"].ToString();
                                bin = DDR3["INS_BIN"].ToString();
                            }

                            ddlbincode.SelectedValue = bin;
                            ddlitmstatus.SelectedValue = stus;
                        }
                    }
                    
                    

                    if ((!string.IsNullOrEmpty(txtserialnumber1.Text.Trim())) && (!string.IsNullOrEmpty(txtserialnumber2.Text.Trim())))
                    {
                        if (txtserialnumber1.Text.Trim() == txtserialnumber2.Text.Trim())
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Serial 1 & Serial 2 are equal !!!";
                            txtserialnumber2.Focus();
                            //SetScrollTop();
                            return false;
                        }
                    }

                    if ((!string.IsNullOrEmpty(txtserialnumber1.Text.Trim())) && (!string.IsNullOrEmpty(txtserialnum3.Text.Trim())))
                    {
                        if (txtserialnumber1.Text.Trim() == txtserialnum3.Text.Trim())
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Serial 1 & Serial 3 are equal !!!";
                            txtserialnum3.Focus();
                            //SetScrollTop();
                            return false;
                        }
                    }

                    if ((!string.IsNullOrEmpty(txtserialnumber2.Text.Trim())) && (!string.IsNullOrEmpty(txtserialnum3.Text.Trim())))
                    {
                        if (txtserialnumber2.Text.Trim() == txtserialnum3.Text.Trim())
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Serial 2 & Serial 3 are equal !!!";
                            txtserialnum3.Focus();
                            //SetScrollTop();
                            return false;
                        }
                    }
                }
                
                if (!string.IsNullOrEmpty(txtserialnumber1.Text) && iscurrent == "CurrentJobb")
                {
                    if (doctype == "MRNA" || doctype == "SOA")
                    {
                        if (dtserialstus.Rows.Count > 0 && itemValidate.Rows.Count > 0)
                        {
                            if (itemValidate.Rows.Count == 1)
                            {
                                if (dtserialstus.Rows[0]["INS_ITM_STUS"].ToString() != itemValidate.Rows[0]["TUI_REQ_ITM_STUS"].ToString())
                                {
                                    divalert.Visible = true;
                                    lblalert.Text = "Serial status miss match with item status.";
                                    txtserialnumber1.Text = "";
                                    txtserialnumber1.Focus();
                                    //SetScrollTop();
                                    return false;
                                }
                            }
                            else
                            {
                                bool validItm = false;
                                foreach (DataRow rws in itemValidate.Rows)
                                {
                                    if (dtserialstus.Rows[0]["INS_ITM_STUS"].ToString() == rws["TUI_REQ_ITM_STUS"].ToString())
                                    {
                                        validItm = true;
                                    }
                                }
                                if (validItm == false)
                                {
                                    divalert.Visible = true;
                                    lblalert.Text = "Serial status miss match with item status.";
                                    txtserialnumber1.Text = "";
                                    txtserialnumber1.Focus();
                                    return false;
                                }
                            }
                        }
                    }
                }
                DataTable dt = (DataTable)ViewState["SERIALTABLE"];

                foreach (DataRow ddrcache in dt.Rows)
                {
                    if ((ddrcache["No"].ToString() == txtserialnumber1.Text.Trim()) || (ddrcache["No"].ToString() == txtserialnumber2.Text.Trim()) || (ddrcache["No"].ToString() == txtserialnum3.Text.Trim()))
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Some serial/serials have already scanned !!!";
                        txtserialnumber1.Text = "";
                        txtserialnumber1.Focus();
                        //SetScrollTop();
                        return false;
                    }
                }
               
                //ViewState["SERIALTABLE"] = dt;

                //if (dt.Rows.Count > 0)
                //{
                //    DataTable dtcopy = ViewState["SERIALTABLE"] as DataTable;

                //    if (!string.IsNullOrEmpty(txtserialnumber1.Text.Trim()))
                //    {
                //        dtcopy.Rows.Add(txtserialnumber1.Text.Trim());
                //    }

                //    if (!string.IsNullOrEmpty(txtserialnumber2.Text.Trim()))
                //    {
                //        dtcopy.Rows.Add(txtserialnumber2.Text.Trim());
                //    }

                //    if (!string.IsNullOrEmpty(txtserialnum3.Text.Trim()))
                //    {
                //        dtcopy.Rows.Add(txtserialnum3.Text.Trim());
                //    }

                //    dtcopy.Merge(dt);
                //    ViewState["SERIALTABLE"] = dtcopy;
                //}
                //else
                //{
                //    DataTable dtoriginal = ViewState["SERIALTABLE"] as DataTable;

                //    if (!string.IsNullOrEmpty(txtserialnumber1.Text.Trim()))
                //    {
                //        dtoriginal.Rows.Add(txtserialnumber1.Text.Trim());
                //    }

                //    if (!string.IsNullOrEmpty(txtserialnumber2.Text.Trim()))
                //    {
                //        dtoriginal.Rows.Add(txtserialnumber2.Text.Trim());
                //    }

                //    if (!string.IsNullOrEmpty(txtserialnum3.Text.Trim()))
                //    {
                //        dtoriginal.Rows.Add(txtserialnum3.Text.Trim());
                //    }

                //    ViewState["SERIALTABLE"] = dtoriginal;
                //}
            }
            else
            {
                if ((string.IsNullOrEmpty(txtbarcode.Text.Trim())) && (string.IsNullOrEmpty(txtqty.Text.Trim())))
                {
                    if (string.IsNullOrEmpty(txtqty.Text.Trim()))
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Please enter qty or scan barcode !!!";
                        txtqty.Focus();
                        //SetScrollTop();
                        return false;
                    }
                }

                if ((string.IsNullOrEmpty(txtbarcode.Text.Trim())) && (!string.IsNullOrEmpty(txtqty.Text.Trim())))
                {
                    if (Convert.ToDecimal(txtqty.Text.Trim()) == 0)
                    {
                        divalert.Visible = true;
                        lblalert.Text = "0 qty is not allowed !!!";
                        txtqty.Focus();
                        //SetScrollTop();
                        return false;
                    }
                }

                if ((string.IsNullOrEmpty(txtbarcode.Text.Trim())) && (!string.IsNullOrEmpty(txtqty.Text.Trim())))
                {
                    if (Convert.ToDecimal(txtqty.Text.Trim()) < 0)
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Minus values are not allowed !!!";
                        txtqty.Focus();
                        //SetScrollTop();
                        return false;
                    }
                }

                string uom = (string)Session["UOM"];
                DataTable dtuom = CHNLSVC.Inventory.IsItemUOMDecimalAllow(uom);
                if (dtuom.Rows.Count > 0)
                {
                    foreach (DataRow ddritemuom in dtuom.Rows)
                    {
                        string isdecimalallow = ddritemuom["msu_isdecimal"].ToString();

                        if (isdecimalallow != "1")
                        {
                            Int32 Check_Integer;

                            if (string.IsNullOrEmpty(txtbarcode.Text.Trim()))
                            {
                                if (!Int32.TryParse(txtqty.Text.Trim(), out Check_Integer))
                                {
                                    divalert.Visible = true;
                                    lblalert.Text = "Not allowed decimal qty for this item !!!";
                                    txtqty.Focus();
                                   // SetScrollTop();
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            LoadCurrentQtyofBin();
            string Currentstockofbin = (string)Session["QTYOFBIN"];
            Decimal CurrentStock = 0;

            if (!string.IsNullOrEmpty(Currentstockofbin))
            {
                 CurrentStock = Convert.ToDecimal(Currentstockofbin);
            }
            else
            {
                 CurrentStock = Convert.ToDecimal(0);
            }
            Decimal _qty = 0;

            string warecom = (string)Session["WAREHOUSE_COMPDA"];
            string wareloc = (string)Session["WAREHOUSE_LOCPDA"];
            string loadingpoint = (string)Session["LOADING_POINT"];

            DataTable dtscannedqty = CHNLSVC.Inventory.CalculateScannedQtyWithSeq(txtitemcode.Text.ToUpper().Trim(), ddlitmstatus.SelectedValue, Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString(), ddlbincode.SelectedValue, warecom, wareloc, loadingpoint, Convert.ToInt32(Session["SEQNO"].ToString()));
            if (locserialcheck == "True")
            {
                if (dtscannedqty.Rows.Count > 0)
                {
                    foreach (DataRow ddritemuom in dtscannedqty.Rows)
                    {
                        if (!string.IsNullOrEmpty(ddritemuom["TOTQTY"].ToString()))
                        {
                            _qty = Convert.ToDecimal(ddritemuom["TOTQTY"].ToString());
                        }
                    }
                }

                if ((CurrentStock == _qty) || (CurrentStock < _qty))
                {
                    divalert.Visible = true;
                    lblalert.Text = "No further stock balance available !!!";
                    txtitemcode.Focus();
                    //SetScrollTop();
                    return false;
                }
            }
            string isseriaitemdup = (string)Session["SERIALIZED"];
            if (isseriaitemdup == "0")
            {
                if (!string.IsNullOrEmpty(txtbarcode.Text))
                {
                    txtqty.Text = "1";
                    //abc
                }
                if (CurrentStock < Convert.ToDecimal(txtqty.Text.Trim()))
                {
                    divalert.Visible = true;
                    lblalert.Text = "No stock balance available for entered quantity !";
                    //txtitemcode.Focus();
                    //SetScrollTop();
                    txtqty.Focus();
                    return false;
                }
            }
           string _seqNo = (string)Session["SEQNO"].ToString();
           DataTable tempItms = CHNLSVC.Inventory.GetTepItems(Convert.ToInt32(_seqNo), txtitemcode.Text.ToUpper().Trim().ToString(), ddlitmstatus.SelectedValue.ToString());
           if (tempItms.Rows.Count == 0 && isseriaitemdup=="0" && !string.IsNullOrEmpty(txtbarcode.Text.ToString()))
           {
               divalert.Visible = true;
               lblalert.Text = "Please select valid status in document!";
               txtqty.Focus();
               return false;
           }
            return true;
        }
        private void updateItemQty()
        {
            try
            {
                string docNo = (string)Session["DOCNO"];
                Int32 userseq = Convert.ToInt32(Session["SEQNO"].ToString());
                DataTable itemValidate = CHNLSVC.Inventory.GetItemDataInTempPickItemWithSeq(txtitemcode.Text.ToUpper().Trim(), userseq);

                decimal totalPickItmQty = 0;
                decimal totalReqItmQty = 0;
                decimal qty = (txtqty.Text.Trim() != "") ? Convert.ToDecimal(txtqty.Text.Trim()) : 0;
                if (itemValidate.Rows.Count > 0)
                {
                    foreach (DataRow clm in itemValidate.Rows)
                    {
                        totalPickItmQty = totalPickItmQty + Convert.ToDecimal(clm["TUI_PIC_ITM_QTY"].ToString());
                        totalReqItmQty = totalReqItmQty + Convert.ToDecimal(clm["TUI_REQ_ITM_QTY"].ToString());
                    }
                }
                itmScnQty.Text = totalPickItmQty.ToString();
                itmDocQty.Text = totalReqItmQty.ToString();
                if (txtitemcode.Text != "")
                {
                    decimal itemStkQty = CHNLSVC.Inventory.getTotalStockQty(txtitemcode.Text.Trim().ToString(), Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString());
                    StockQty.Text = itemStkQty.ToString();
                }
                else
                {
                    StockQty.Text = "0";
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void Clear()
        {
            try
            {
                txtserialnumber1.Text = string.Empty;
                txtserialnumber2.Text = string.Empty;
                txtserialnum3.Text = string.Empty;
                txtbarcode.Text = string.Empty;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
        }
        private void SaveData()
        {
            try
            {
                if (!string.IsNullOrEmpty(Session["Error"].ToString()))
                {
                    divalert.Visible = true;
                    lblalert.Text = Session["Error"].ToString();
                    return;
                }
                string glberror="";
                bool hadRec = false;
                string locserialcheck = (string)Session["LOCISSERIAL"];
                string receiveLoc = (string)Session["ToLocation"];

                string _scanDocument = (string)Session["DOCNO"];
                string DocumentType = (string)Session["DOCTYPE"];
                string _userSeqNo = (string)Session["SEQNO"].ToString();
                string docdirection = (string)Session["DOCDIRECTION"];
                string isseriaitem = (string)Session["SERIALIZED"];

                string warecom = (string)Session["WAREHOUSE_COMPDA"];
                string wareloc = (string)Session["WAREHOUSE_LOCPDA"];
                string loadingpoint = (string)Session["LOADING_POINT"];
                string iscurrent = Session["CurrentJobb"] as string;

                string company= Session["UserCompanyCode"].ToString();
                string location=Session["UserDefLoca"].ToString();
                //string iscurrentjob = Session["CurrentJobb"] as string;
                string itm_desc = "";
                string itm_model = "";
                string itm_brand = "";

                string focusSer = "";
                if (warecom == "" || wareloc == "" || loadingpoint == "") {
                    divalert.Visible = true;
                    lblalert.Text = "Invalid loading bay,Wherehouse location,company!";
                    return;
                }
                if (txtserialnumber1.Text != "")
                {
                    txtitemcode.Text = "";
                    txtserialnumber2.Text = "";
                    txtserialnum3.Text = "";
                    txtqty.Text = "";
                    txtbarcode.Text = "";
                    //txtqty.Visible = false;
                    //txtbarcode.Visible = false;
                    string error = "";
                    DataTable ser1Data = CHNLSVC.Inventory.getAllSerialDetails(company, location, txtserialnumber1.Text.ToString(), "", "", out error);
                    if (ser1Data == null || ser1Data.Rows.Count == 0)
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Invalid serial #1.Please check before scan.";
                        txtserialnumber1.Focus();
                        return;
                    }
                    else
                    {
                        if (ser1Data.Rows.Count > 1)
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Multiple item or serial available for this serial.";
                            txtserialnumber1.Focus();
                            return;
                        }
                        else
                        {
                            if (ser1Data.Rows.Count > 0)
                            {
                                if (ser1Data.Rows[0]["INS_AVAILABLE"] != DBNull.Value)
                                {
                                    if (ser1Data.Rows[0]["INS_AVAILABLE"].ToString() == "-1")
                                    {
                                        divalert.Visible = true;
                                        lblalert.Text = "Already scanned serial #1.";
                                        txtserialnumber1.Focus();
                                        return;
                                    }
                                }
                            }

                            if (ser1Data.Rows[0]["INS_ITM_CD"] != DBNull.Value)
                            {
                                txtitemcode.Text = ser1Data.Rows[0]["INS_ITM_CD"].ToString();
                            }
                            else
                            {
                                divalert.Visible = true;
                                lblalert.Text = "No items found for this serial.";
                                txtserialnumber1.Focus();
                                return;
                            }
                            if (ser1Data.Rows[0]["INS_SER_1"] != DBNull.Value)
                            {
                                txtserialnumber1.Text = ser1Data.Rows[0]["INS_SER_1"].ToString();
                            }
                            if (ser1Data.Rows[0]["INS_SER_2"] != DBNull.Value)
                            {
                                if (ser1Data.Rows[0]["INS_SER_2"].ToString() != "N/A")
                                txtserialnumber2.Text = ser1Data.Rows[0]["INS_SER_2"].ToString();
                            }
                            if (ser1Data.Rows[0]["INS_SER_3"] != DBNull.Value)
                            {
                                if (ser1Data.Rows[0]["INS_SER_3"].ToString() != "N/A")
                                txtserialnum3.Text = ser1Data.Rows[0]["INS_SER_3"].ToString();
                            }
                            focusSer = "SER1";
                        }
                    }
                    txtitemcode_TextChanged(null, null);
                    isseriaitem = (string)Session["SERIALIZED"];
                }
                else if (txtserialnumber2.Text != "")
                {
                    txtitemcode.Text = "";
                    txtserialnumber1.Text = "";
                    txtserialnum3.Text = "";
                    txtqty.Text = "";
                    txtbarcode.Text = "";
                    //txtqty.Visible = false;
                    //txtbarcode.Visible = false;
                    string error = "";
                    DataTable serData = CHNLSVC.Inventory.getAllSerialDetails(company, location, "", txtserialnumber2.Text.ToString(), "", out error);
                    if (serData == null || serData.Rows.Count == 0)
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Invalid serial #2.Please check before scan.";
                        txtserialnumber1.Focus();
                        return;
                    }
                    else
                    {
                        if (serData.Rows.Count > 1)
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Multiple item or serial available for this serial.";
                            txtserialnumber1.Focus();
                            return;
                        }
                        else
                        {
                            if (serData.Rows.Count > 0)
                            {
                                if (serData.Rows[0]["INS_AVAILABLE"] != DBNull.Value)
                                {
                                    if (serData.Rows[0]["INS_AVAILABLE"].ToString() == "-1")
                                    {
                                        divalert.Visible = true;
                                        lblalert.Text = "Already scanned serial #2.";
                                        txtserialnumber1.Focus();
                                        return;
                                    }
                                }
                            }

                            if (serData.Rows[0]["INS_ITM_CD"] != DBNull.Value)
                            {
                                txtitemcode.Text = serData.Rows[0]["INS_ITM_CD"].ToString();
                            }
                            else
                            {
                                divalert.Visible = true;
                                lblalert.Text = "No items found for this serial.";
                                txtserialnumber2.Focus();
                                return;
                            }
                            if (serData.Rows[0]["INS_SER_1"] != DBNull.Value)
                            {
                                txtserialnumber1.Text = serData.Rows[0]["INS_SER_1"].ToString();
                            }
                            if (serData.Rows[0]["INS_SER_2"] != DBNull.Value)
                            {
                                if (serData.Rows[0]["INS_SER_2"].ToString()!="N/A")
                                    txtserialnumber2.Text = serData.Rows[0]["INS_SER_2"].ToString();
                            }
                            if (serData.Rows[0]["INS_SER_3"] != DBNull.Value)
                            {
                                if (serData.Rows[0]["INS_SER_3"].ToString() != "N/A")
                                txtserialnum3.Text = serData.Rows[0]["INS_SER_3"].ToString();
                            }
                            focusSer = "SER2";
                        }
                    }
                    txtitemcode_TextChanged(null, null);
                    isseriaitem = (string)Session["SERIALIZED"];
                }
                if (DocumentType == "SOA" || DocumentType == "SO")
                {
                    InventoryRequest _invreqSup = new InventoryRequest();
                    if (DocumentType == "SOA")
                    {
                        InventoryRequest _invreqSupSo = CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = _scanDocument }).FirstOrDefault();
                        _invreqSup = CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = _invreqSupSo.Itr_ref }).FirstOrDefault();
                    }
                    if (DocumentType == "SO")
                    {
                        _invreqSup = CHNLSVC.Inventory.GET_INT_REQ_DATA_NEW(new InventoryRequest() { Itr_req_no = _scanDocument }).FirstOrDefault();
                    }
                    
                    if (_invreqSup != null)
                    {
                        if (_invreqSup.Itr_tp == "SO")
                        {
                            MasterBusinessEntity _mstBusEntity = CHNLSVC.Sales.GetCustomerProfileByCom(_invreqSup.Itr_bus_code, null, null, null, null,
                                Session["UserCompanyCode"].ToString());
                            if (_mstBusEntity != null)
                            {
                                if (_mstBusEntity.Mbe_is_suspend == true)
                                {
                                    divalert.Visible = true;
                                    lblalert.Text = "This customer already suspended !";
                                    return;
                                }
                            }
                        }
                    }
                }
                if (DocumentType == "DO" && _scanDocument != "")
                {
                    DataTable dat = CHNLSVC.Sales.GetSalesHdr(_scanDocument);
                    if (dat.Rows.Count > 0)
                    {
                        if (dat.Rows[0]["SAH_INV_NO"] != DBNull.Value && dat.Rows[0]["SAH_CUS_CD"] != DBNull.Value)
                        {
                            MasterBusinessEntity _mstBusEntity = CHNLSVC.Sales.GetCustomerProfileByCom(dat.Rows[0]["SAH_CUS_CD"].ToString(), null, null, null, null,
                               company);
                            if (_mstBusEntity != null)
                            {
                                if (_mstBusEntity.Mbe_is_suspend == true)
                                {
                                    divalert.Visible = true;
                                    lblalert.Text = "This customer already suspended !";
                                    return;
                                }
                            }
                        }
                    }
                }
                if (string.IsNullOrEmpty(txtitemcode.Text.Trim()))
                {
                   txtitemcode_TextChanged(null, null);
                   
                }
                if (locserialcheck == "False")
                {
                    ddlitmstatus.SelectedIndex = ddlitmstatus.Items.IndexOf(ddlitmstatus.Items.FindByValue("GOD"));
                }
                if (!string.IsNullOrEmpty(txtbarcode.Text.Trim().ToString()) && isseriaitem !="1")
                {
                    if (txtbarcode.Text.Trim().ToString() != txtitemcode.Text.Trim().ToString())
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Invalid scan.Please scan item code for barcode!";
                        txtbarcode.Text = string.Empty;
                        txtbarcode.Focus();
                        return;
                    }
                }
                bool isvalid;
                DivsHide();
                
                isvalid = ValidateItems();


                if (isvalid == false)
                {
                    return;
                }
                else {
                    DataTable dtitemdata = CHNLSVC.Inventory.GetItemData(txtitemcode.Text.ToUpper().Trim());

                    if (dtitemdata.Rows.Count == 0)
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Invalid item code !!!";
                        txtitemcode.Text = string.Empty;
                        txtitemcode.Focus();
                        return;
                    }
                    itm_desc=dtitemdata.Rows[0]["MI_SHORTDESC"].ToString();
                    itm_model=dtitemdata.Rows[0]["MI_MODEL"].ToString();
                    itm_brand = dtitemdata.Rows[0]["MI_BRAND"].ToString();
                }
                //if (Convert.ToInt32(lblscqty.Text.ToString())+1 > Convert.ToInt32(lbldocqty.Text.ToString()))
                //{
                //    lblalert.Text = "You Cannot Exceed Doc Quantity";
                //    return;
                //}

                

                
                Int32 val = 0;
                Int32 valserial = 0;
                Int32 valitem = 0;
                Int32 result = 0;

                decimal picqty = 0;
                decimal reqqty = 0;

                bool isinorout = true;
                string serialidforitem = CHNLSVC.Inventory.GetSerialID().ToString();

                ReptPickHeader _inputReptPickHeader = new ReptPickHeader();
                ReptPickItems _items = new ReptPickItems();
                ReptPickItems _itemslines = new ReptPickItems();
                ReptPickSerials _inputReptPickSerials = new ReptPickSerials();

                Decimal qtyforscan = 1;
                DataTable temppickitems = CHNLSVC.Inventory.GetTepItems(Convert.ToInt32(_userSeqNo), txtitemcode.Text.ToUpper().Trim().ToString(), ddlitmstatus.SelectedValue.ToString());
                
                if (isseriaitem != "1")
                {
                    if (string.IsNullOrEmpty(txtbarcode.Text.Trim()))
                    {
                        qtyforscan = (txtqty.Text!="")? Convert.ToDecimal(txtqty.Text.Trim()): 0;
                    }
                }
                if (txtbarcode.Text.ToString() == "" && txtqty.Text.ToString() == "" && txtserialnumber1.Text.ToString() == "")
                {
                    return;
                }
                if (docdirection == "0" || DocumentType=="STJO")
                {
                    isinorout = false;
                }

                if (isseriaitem != "1")
                {
                    serialidforitem = "0";
                }
                if (iscurrent == "CurrentJobb")
                {
                     if (temppickitems.Rows.Count > 0)
                    {
                        foreach (DataRow dtrw in temppickitems.Rows)
                        {
                            if (dtrw["TUI_PIC_ITM_CD"].ToString() == "0" || dtrw["TUI_PIC_ITM_CD"] == null)
                            {
                                divalert.Visible = true;
                                lblalert.Text = "Invalid base item line !";
                                return;
                            }
                        }
                    }
                     else
                     {
                         if (DocumentType == "DO")
                         {
                             temppickitems = CHNLSVC.Inventory.GetTepItemsBYCode(Convert.ToInt32(_userSeqNo), txtitemcode.Text.ToUpper().Trim().ToString());
                         }
                     }
                }

                if (isseriaitem != "1" && iscurrent == "CurrentJobb")
                {
                    if (string.IsNullOrEmpty(txtbarcode.Text.Trim()))
                    {
                        qtyforscan = Convert.ToDecimal(txtqty.Text.Trim());
                        
                        if (temppickitems.Rows.Count > 0)
                        {
                            foreach (DataRow row in temppickitems.Rows)
                            {
                                picqty += (row["TUI_PIC_ITM_QTY"] == null) ? 0 : Convert.ToDecimal(row["TUI_PIC_ITM_QTY"].ToString());
                                reqqty += (row["TUI_REQ_ITM_QTY"] == null) ? 0 : Convert.ToDecimal(row["TUI_REQ_ITM_QTY"].ToString());
                            }
                        }

                        if (reqqty < qtyforscan + picqty)
                        {
                            divalert.Visible = true;
                            txtqty.Text = "";
                            lblalert.Text = "Cannot exceed item request quantity !";
                            txtqty.Focus();
                            return;
                        }
                    }
                }

                DataTable dtserials = CHNLSVC.Inventory.LoadAllSerials(Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString(), ddlbincode.SelectedValue, txtitemcode.Text.ToUpper().Trim(), ddlitmstatus.SelectedValue, txtserialnumber1.Text.Trim());
                if (dtserials.Rows.Count > 0)
                {
                    foreach (DataRow ddrserials in dtserials.Rows)
                    {
                        txtserialnumber1.Text = ddrserials["ins_ser_1"].ToString();
                        txtserialnumber2.Text = ddrserials["ins_ser_2"].ToString();
                        txtserialnum3.Text = ddrserials["ins_ser_3"].ToString();
                    }
                }

                DataTable dtsericlavailable = CHNLSVC.Inventory.IsSavedSerialAvailable(txtitemcode.Text.ToUpper().Trim(), txtserialnumber1.Text.Trim(), warecom, wareloc, loadingpoint);

                if (dtsericlavailable.Rows.Count > 0)
                {
                    divalert.Visible = true;
                    lblalert.Text = "Serial has already scanned !!!";
                    //SetScrollTop();
                    return;
                }
                if (isseriaitem != "1")
                {
                    string error = String.Empty;
                    decimal stockAvailable = CHNLSVC.Inventory.checkStockAvailabilityOfItem(txtitemcode.Text.Trim(), Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString(), ddlbincode.SelectedValue, ddlitmstatus.SelectedValue, qtyforscan, out error);
                    if (error != "")
                    {
                        divalert.Visible = true;
                        lblalert.Text = error;
                        return;
                    }
                    if (stockAvailable - qtyforscan < 0)
                    {
                        divalert.Visible = true;
                        lblalert.Text = "No bin stock available for this item";
                        return;
                    }
                }
                /* CHECK INR SER */
                 // DataTable inrserialdata = CHNLSVC.Inventory.getINRSerial(txtserialnumber1.Text.Trim().ToString(), txtitemcode.Text.ToUpper().Trim().ToString());
                    string inrdocno = "";
                    Int32 serialid = 0;
                    Int32 inritemline = 0;
                    Int32 inrbatchline = 0;
                    Int32 inrserialline = 0;
                    decimal inrunitcost = 0;
                    
                    //if (inrserialdata.Rows.Count > 0)
                    //{
                    //    inrdocno = inrserialdata.Rows[0]["ins_doc_no"].ToString();
                    //    // serial id
                    //   // serialid = Convert.ToInt32(inrserialdata.Rows[0]["ins_ser_id"]);
                    //}
                    //rukshan function
                    //nuwan
                    Int32 tus_seq_no = 0;
                    DateTime tus_doc_dt = DateTime.MinValue;
                    string tus_warr_no = string.Empty;
                    Int32 tus_warr_period = 0;
                    string tus_orig_grncom = string.Empty;
                    string tus_orig_grnno = string.Empty;
                    DateTime tus_orig_grndt = DateTime.MinValue;
                    string tus_orig_supp = string.Empty;
                    string tus_exist_grncom = string.Empty;
                    string tus_exist_grnno = string.Empty;
                    DateTime tus_exist_grndt = DateTime.MinValue;
                    string tus_exist_supp = string.Empty;
                    string tus_ageloc = string.Empty;
                    DateTime tus_ageloc_dt = DateTime.MinValue;


                    
                    if (isseriaitem == "1" && locserialcheck == "True")
                    {
                        DataTable inrserialdata = CHNLSVC.Inventory.GET_INR_SER(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), null, txtitemcode.Text.ToUpper().Trim().ToString(), ddlitmstatus.SelectedValue, txtserialnumber1.Text.Trim().ToString());
                        if (inrserialdata.Rows.Count > 0)
                        {
                            serialid = Convert.ToInt32(inrserialdata.Rows[0]["ins_ser_id"]);
                            inrdocno = inrserialdata.Rows[0]["ins_doc_no"].ToString();
                            inritemline = Convert.ToInt32(inrserialdata.Rows[0]["ins_itm_line"]);
                            inrbatchline = Convert.ToInt32(inrserialdata.Rows[0]["ins_batch_line"]);
                            inrserialline = Convert.ToInt32(inrserialdata.Rows[0]["ins_ser_line"]);
                            inrunitcost = Convert.ToDecimal(inrserialdata.Rows[0]["ins_unit_cost"]);

                            tus_seq_no=Convert.ToInt32(inrserialdata.Rows[0]["ins_seq_no"].ToString());
                            tus_doc_dt=(inrserialdata.Rows[0]["ins_doc_dt"].ToString()!="")?Convert.ToDateTime(inrserialdata.Rows[0]["ins_doc_dt"].ToString()):DateTime.MinValue;
                            tus_warr_no=inrserialdata.Rows[0]["ins_warr_no"].ToString();
                            tus_warr_period=Convert.ToInt32(inrserialdata.Rows[0]["ins_warr_period"].ToString());
                            tus_orig_grncom=inrserialdata.Rows[0]["ins_orig_grncom"].ToString();
                            tus_orig_grnno=inrserialdata.Rows[0]["ins_orig_grnno"].ToString();
                            tus_orig_grndt=(inrserialdata.Rows[0]["ins_orig_grndt"].ToString()!="")?Convert.ToDateTime(inrserialdata.Rows[0]["ins_orig_grndt"].ToString()):DateTime.MinValue;
                            tus_orig_supp=inrserialdata.Rows[0]["ins_orig_supp"].ToString();
                            tus_exist_grncom=inrserialdata.Rows[0]["ins_exist_grncom"].ToString();
                            tus_exist_grnno=inrserialdata.Rows[0]["ins_exist_grnno"].ToString();
                            tus_exist_grndt=(inrserialdata.Rows[0]["ins_exist_grndt"].ToString()!="")?Convert.ToDateTime(inrserialdata.Rows[0]["ins_exist_grndt"].ToString()):DateTime.MinValue;
                            tus_exist_supp=inrserialdata.Rows[0]["ins_exist_supp"].ToString();
                            tus_ageloc=inrserialdata.Rows[0]["ins_ageloc"].ToString();
                            tus_ageloc_dt =(inrserialdata.Rows[0]["ins_ageloc_dt"].ToString()!="")?Convert.ToDateTime(inrserialdata.Rows[0]["ins_ageloc_dt"].ToString()):DateTime.MinValue;
                        }
                        else
                        {
                            divalert.Visible = true;
                            txtserialnumber1.Text = "";
                            lblalert.Text = "Not available serial id !!!";
                            return;
                        }

                    }
                    else {
                        if(isseriaitem=="1"){
                            //ddlitmstatus.SelectedValue = "GOD";
                        }
                    }
                    if (isseriaitem == "1" && txtserialnumber1.Text.ToString().Trim()=="")
                    {
                        divalert.Visible = true;
                        txtserialnumber1.Text = "";
                        lblalert.Text = "Not available serial id !!!";
                        return;
                    }

                    if (txtitemcode.Text.ToString() != "" && txtserialnumber1.Text.ToString() != "")
                    {
                        if (txtitemcode.Text.ToString() == txtserialnumber1.Text.ToString())
                        {
                            divalert.Visible = true;
                            txtserialnumber1.Text = "";
                            
                            lblalert.Text = "Item code scan as serial!";
                            txtserialnumber1.Focus();
                            return;

                        }
                    }

                #region Header

                    DataTable dtdoccheck = CHNLSVC.Inventory.IsDocAvailableWithSeq(Session["UserCompanyName"].ToString(), Convert.ToInt32(_userSeqNo), Session["UserDefLoca"].ToString(), warecom, wareloc, loadingpoint);

                    if (iscurrent == "CurrentJobb" && dtdoccheck.Rows.Count == 0)
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Invalid document.(Invalid wharehouse locations)";
                        return;
                    }
                    else
                    {
                        if (dtdoccheck.Rows.Count > 0)
                        {
                            if (Session["UserDefLoca"].ToString() != dtdoccheck.Rows[0]["TUH_USR_LOC"].ToString())
                            {
                                divalert.Visible = true;
                                lblalert.Text = "Document scan using invalid location.";
                                return;
                            }

                        }
                    }
                if (dtdoccheck.Rows.Count > 0 && dtdoccheck.Rows[0]["TUS_FIN_STUS"].ToString() == "1")
                {
                    divalert.Visible = true;
                    lblalert.Text = "Already finished document.";
                    return;
                }
                if (dtdoccheck.Rows.Count == 0)
                {
                    if (string.IsNullOrEmpty(loadingpoint))
                    {
                        warecom = string.Empty;
                        wareloc = string.Empty;
                    }


                    _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(_userSeqNo);
                    _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyName"].ToString();
                    _inputReptPickHeader.Tuh_session_id = (string)Session["GlbUserSessionID"];
                    _inputReptPickHeader.Tuh_rec_loc = receiveLoc;
                    

                    if (Session["Doctype"].ToString() == "AOD" && (iscurrent != "CurrentJobb" || iscurrent !=null))
                    {
                        _inputReptPickHeader.Tuh_doc_tp = "PDA";
                    }
                    else
                    {
                        _inputReptPickHeader.Tuh_doc_tp = Session["Doctype"].ToString();
                    }

                   
                    _inputReptPickHeader.Tuh_direct = isinorout;
                    _inputReptPickHeader.Tuh_ischek_itmstus = false;
                    _inputReptPickHeader.Tuh_ischek_simitm = false;
                    _inputReptPickHeader.Tuh_ischek_reqqty = false;
                    _inputReptPickHeader.Tuh_doc_no = _scanDocument;
                    _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                    _inputReptPickHeader.Tuh_wh_com = warecom;
                    _inputReptPickHeader.Tuh_wh_loc = wareloc;
                    _inputReptPickHeader.Tuh_load_bay = loadingpoint;
                    _inputReptPickHeader.Tuh_isdirect = true;
                    //val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);

                    //if (Convert.ToInt32(val) == -1)
                    //{
                    //    divalert.Visible = true;
                    //    lblalert.Text = "Error in processing !!!";
                    //    return;
                    //}
                }

               
                #endregion

                #region Items

                Int32 rownumber = 0;
                Int32 rowvalline = 0;
               
                string linenumber = "";

                if (temppickitems.Rows.Count > 0)
                {
                    foreach (DataRow dtrw in temppickitems.Rows)
                    {
                        if (dtrw["TUI_PIC_ITM_CD"].ToString() != "0" && ((dtrw["TUI_REQ_ITM_QTY"].ToString() != dtrw["TUI_PIC_ITM_QTY"].ToString()) || (dtrw["TUI_REQ_ITM_QTY"].ToString() == "0" && dtrw["TUI_PIC_ITM_QTY"].ToString() == "0")))
                        {
                            linenumber = dtrw["tui_pic_itm_cd"].ToString();
                            if (linenumber == "") linenumber = "0";
                            hadRec = true;
                            break;
                        }
                    }
                    
                }
                else {
                    linenumber = "1";
                }
                if (temppickitems.Rows.Count > 0)
                {
                    if (iscurrent == "CurrentJobb" && linenumber == "")
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Invalid item serial status or cannot exceed request quantity.";
                        return;
                    }
                }
                string iscreatejob = Session["CreateJobNumber"] as string;
                if (iscreatejob == "CreateJobNumber")
                {
                    DataTable dtrownum = CHNLSVC.Inventory.LoadCurrentRowNumber(Convert.ToInt32(_userSeqNo), warecom, wareloc, loadingpoint);
                    foreach (DataRow ddrrownum in dtrownum.Rows)
                    {
                        rownumber = Convert.ToInt32(ddrrownum["RowCount"].ToString()) + 1;
                    }
                    if (hadRec == true)
                    {
                        rownumber = Convert.ToInt32(linenumber);
                    }
                    linenumber = rownumber.ToString();
                }
                _items.Tui_usrseq_no = Convert.ToInt32(_userSeqNo);
                _items.Tui_req_itm_cd = txtitemcode.Text.ToUpper().Trim();
                _items.Tui_req_itm_stus = ddlitmstatus.SelectedValue;
                if (isseriaitem == "1")
                {
                    qtyforscan = 1;
                }
                _items.Tui_pic_itm_qty = qtyforscan;
                _items.Tui_pic_itm_cd =linenumber;//rownumber.ToString(); by nuwan
                _items.Tui_pic_itm_stus = ddlitmstatus.SelectedValue;//(locserialcheck == "False") ? "GOD" : string.Empty;
                _items.Tui_grn = string.Empty;
                _items.Tui_batch = string.Empty;
                _items.Tui_sup = string.Empty;
                if (ddlitmstatus.SelectedValue.ToString()=="")
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please select item status.";
                    return;
                    
                }
                //if (isseriaitem != "1" && temppickitems.Rows.Count > 1 && iscurrent == "CurrentJobb")
                //{
                //    decimal tempScnQty = qtyforscan;
                //    foreach (DataRow dtrw in temppickitems.Rows)
                //    {
                //        if (tempScnQty == 0)
                //        {
                //            break;
                //        }
                //        if ((Convert.ToDecimal(dtrw["TUI_REQ_ITM_QTY"].ToString()) - Convert.ToDecimal(dtrw["TUI_PIC_ITM_QTY"].ToString()) != 0))
                //        {
                //            if (tempScnQty > 0 && ((Convert.ToDecimal(dtrw["TUI_REQ_ITM_QTY"].ToString()) - Convert.ToDecimal(dtrw["TUI_PIC_ITM_QTY"].ToString())) < tempScnQty))
                //            {
                //                _items.Tui_pic_itm_qty = Convert.ToDecimal(dtrw["TUI_REQ_ITM_QTY"].ToString()) - Convert.ToDecimal(dtrw["TUI_PIC_ITM_QTY"].ToString());
                //                _items.Tui_pic_itm_cd = dtrw["tui_pic_itm_cd"].ToString();
                //                tempScnQty = tempScnQty - _items.Tui_pic_itm_qty;//Convert.ToDecimal(dtrw["TUI_REQ_ITM_QTY"].ToString()) - Convert.ToDecimal(dtrw["TUI_PIC_ITM_QTY"].ToString());
                //            }
                //            else
                //            {
                //                _items.Tui_pic_itm_qty = tempScnQty;
                //                _items.Tui_pic_itm_cd = dtrw["tui_pic_itm_cd"].ToString();
                //                tempScnQty = tempScnQty - _items.Tui_pic_itm_qty;
                //            }
                //            valitem = CHNLSVC.Inventory.UpdatePickItemStockInOut(_items);
                //        }
                //    }
                //    // reqqty < qtyforscan + picqty
                //}
                //else
                //{
                //    valitem = CHNLSVC.Inventory.UpdatePickItemStockInOut(_items);
                //}




               //valitem = CHNLSVC.Inventory.UpdatePickItem(_items);
                //valitem = CHNLSVC.Inventory.UpdatePickItemStockInOut(_items);

               
               if (iscreatejob == "CreateJobNumber")
               {

                   _itemslines.Tui_pic_itm_cd = linenumber.ToString();
                   _itemslines.Tui_usrseq_no = Convert.ToInt32(_userSeqNo);
                   _itemslines.Tui_req_itm_cd = txtitemcode.Text.ToUpper().Trim();
                   _itemslines.Tui_req_itm_stus = ddlitmstatus.SelectedValue;

                   //rowvalline = CHNLSVC.Inventory.UpdatePickItemLine(_itemslines);
                   //if (Convert.ToInt32(rowvalline) == -1)
                   //{
                   //    divalert.Visible = true;
                   //    lblalert.Text = "Error in processing !";
                   //    return;
                   //}
               }

               
                //DataTable dtrownum = CHNLSVC.Inventory.LoadCurrentRowNumber(Convert.ToInt32(_userSeqNo),warecom,wareloc,loadingpoint);
                //foreach (DataRow ddrrownum in dtrownum.Rows)
                //{
                //    rownumber = Convert.ToInt32(ddrrownum["RowCount"].ToString());
                //}

              

                //_itemslines.Tui_pic_itm_cd = rownumber.ToString();
                //_itemslines.Tui_usrseq_no = Convert.ToInt32(_userSeqNo);
                //_itemslines.Tui_req_itm_cd = txtitemcode.Text.ToUpper().Trim();
                //_itemslines.Tui_req_itm_stus = ddlitmstatus.SelectedValue;

                //rowvalline = CHNLSVC.Inventory.UpdatePickItemLine(_itemslines);

                //if (Convert.ToInt32(valitem) == -1)
                //{
                //    divalert.Visible = true;
                //    lblalert.Text = "Error in processing !!!";
                //    return;
                //}

                //if (Convert.ToInt32(rowvalline) == -1)
                //{
                //    divalert.Visible = true;
                //    lblalert.Text = "Error in processing !!!";
                //    return;
                //}

                #endregion
               

                #region Serials

                if (dtsericlavailable.Rows.Count == 0 || isseriaitem != "1")
                {
                    //if (temppickitems.Rows.Count > 0)
                    //{
                    //    foreach (DataRow dtrw in temppickitems.Rows)
                    //    {
                    //        if (dtrw["TUI_PIC_ITM_CD"].ToString() != "0" && (dtrw["TUI_REQ_ITM_QTY"].ToString() != dtrw["TUI_PIC_ITM_QTY"].ToString()))
                    //        {
                    //            linenumber = dtrw["tui_pic_itm_cd"].ToString();
                    //            if (linenumber == "") linenumber = "0";
                    //            break;
                    //        }
                    //    }
                    //}
                    //else {
                        //if (temppickitems.Rows.Count > 0)
                        //{
                        //    foreach (DataRow dtrw in temppickitems.Rows)
                        //    {
                        //        if (dtrw["TUI_PIC_ITM_CD"].ToString() != "0" && ((dtrw["TUI_REQ_ITM_QTY"].ToString() != dtrw["TUI_PIC_ITM_QTY"].ToString()) || (dtrw["TUI_REQ_ITM_QTY"].ToString() == "0" && dtrw["TUI_PIC_ITM_QTY"].ToString() == "0")))
                        //        {
                        //            linenumber = dtrw["tui_pic_itm_cd"].ToString();
                        //            if (linenumber == "") linenumber = "0";
                        //            break;
                        //        }
                        //    }


                        //}
                        //else
                        //{
                        //    linenumber = "1";
                        //}
                    //}
                    DataTable tempdoctype = CHNLSVC.Inventory.GetTempPickDocTypes(Convert.ToInt32(docdirection));
                    int i = 0;
                    string remark = "";
                    if (tempdoctype.Rows.Count >0)
                    {
                        foreach (DataRow droptype in tempdoctype.Rows)
                        {
                            if (tempdoctype.Rows[i]["tdt_tp"].ToString() == Session["Doctype"].ToString())
                            {
                                remark = tempdoctype.Rows[i]["tdt_main_tp"].ToString();
                            }
                            i++;
                        }
                    }
                   

                    _inputReptPickSerials.Tus_usrseq_no = Convert.ToInt32(_userSeqNo);
                    _inputReptPickSerials.Tus_doc_no = inrdocno;
                    _inputReptPickSerials.Tus_seq_no = Convert.ToInt32(_userSeqNo);

                    if (inritemline == 0) inritemline = Convert.ToInt32(linenumber);

                    _inputReptPickSerials.Tus_itm_line = Convert.ToInt32(inritemline);
                    _inputReptPickSerials.Tus_com = Session["UserCompanyName"].ToString();
                    _inputReptPickSerials.Tus_loc = Session["UserDefLoca"].ToString();
                    _inputReptPickSerials.Tus_bin = string.Empty;
                    _inputReptPickSerials.Tus_itm_cd = txtitemcode.Text.ToUpper().Trim();
                    _inputReptPickSerials.Tus_itm_stus = ddlitmstatus.SelectedValue;
                    if (isseriaitem == "1")
                    {
                        qtyforscan = 1;
                    }
                    _inputReptPickSerials.Tus_qty = qtyforscan;
                    _inputReptPickSerials.Tus_ser_id = 0; /*Convert.ToInt32(serialidforitem)*/
                    _inputReptPickSerials.Tus_ser_remarks = remark;
                    _inputReptPickSerials.Tus_batch_line =Convert.ToInt32( inrbatchline);
                    _inputReptPickSerials.Tus_ser_line = Convert.ToInt32(inrserialline);
                    _inputReptPickSerials.Tus_unit_cost = inrunitcost;
                    _inputReptPickSerials.Tus_seq_no = tus_seq_no;
                    _inputReptPickSerials.Tus_doc_dt = tus_doc_dt;
                    _inputReptPickSerials.Tus_warr_no = tus_warr_no;
                    _inputReptPickSerials.Tus_warr_period = tus_warr_period;
                    _inputReptPickSerials.Tus_orig_grncom = tus_orig_grncom;
                    _inputReptPickSerials.Tus_orig_grnno = tus_orig_grnno;
                    _inputReptPickSerials.Tus_orig_grndt = tus_orig_grndt;
                    _inputReptPickSerials.Tus_orig_supp = tus_orig_supp;
                    _inputReptPickSerials.Tus_exist_grncom = tus_exist_grncom;
                    _inputReptPickSerials.Tus_exist_grnno = tus_exist_grnno;
                    _inputReptPickSerials.Tus_exist_grndt = tus_exist_grndt;
                    _inputReptPickSerials.Tus_exist_supp = tus_exist_supp;
                    _inputReptPickSerials.Tus_ageloc = tus_ageloc;
                    _inputReptPickSerials.Tus_ageloc_dt = tus_ageloc_dt;

                    _inputReptPickSerials.Tus_itm_desc=itm_desc;
                    _inputReptPickSerials.Tus_itm_model=itm_model;
                    _inputReptPickSerials.Tus_itm_brand = itm_brand;


                    if (iscurrent != null && iscurrent == "CurrentJobb")
                    {
                        DataTable dtDocDetails = CHNLSVC.Inventory.getCurrentDocumentDetailsSeq(Session["UserCompanyName"].ToString(), Convert.ToInt32(_userSeqNo), Session["UserDefLoca"].ToString(), warecom, wareloc, loadingpoint);
                        if (dtDocDetails.Rows.Count > 0 && temppickitems.Rows.Count > 0)
                        {
                            if (dtDocDetails.Rows[0]["TUH_IS_TAKE_RES"] !=null && dtDocDetails.Rows[0]["TUH_IS_TAKE_RES"].ToString() == "1")
                                 _inputReptPickSerials.Tus_resqty = _inputReptPickSerials.Tus_qty;
                        }

                        if (Session["Doctype"].ToString() == "DO" && dtDocDetails.Rows[0]["TUH_IS_TAKE_RES"] !=null && dtDocDetails.Rows[0]["TUH_IS_TAKE_RES"].ToString() == "1")
                           {
                               DataTable itemDet = CHNLSVC.Inventory.getItemDetWithStatus(Convert.ToInt32(_userSeqNo), ddlitmstatus.SelectedValue,txtitemcode.Text.ToString().Trim());
                               if (itemDet.Rows.Count > 0)
                               {
                                   Int32 resqtyal = 0;
                                   Int32 pcqty = 0;
                                   foreach(DataRow row in itemDet.Rows){
                                       if (row["TUI_BATCH"].ToString() == "0")
                                       {
                                           resqtyal +=  Convert.ToInt32(row["TUI_RES_QTY"].ToString());
                                           pcqty +=Convert.ToInt32(row["TUI_PIC_ITM_QTY"].ToString());
                                       }
                                   }
                                   if (resqtyal - pcqty <=0)
                                   {
                                       divalert.Visible = true;
                                       lblalert.Text = "No available reserved quantity for this item.";
                                       return;
                                   }
                               }
                           }

                    }
                   

                  //  string existseialno = "";
                   
                        if (isseriaitem == "1")
                        {
                            //get exist serial no
                            //DataTable existserial = CHNLSVC.Inventory.getExistingSerial(txtserialnumber1.Text.ToString(), txtitemcode.Text.ToString());

                            //if (existserial.Rows.Count > 0)
                            //{
                            //    existseialno = existserial.Rows[0]["SerialNo"].ToString();


                            //    _inputReptPickSerials.Tus_ser_id = Convert.ToInt32(existseialno);
                            //    // update reversed 1
                            //    int seout = CHNLSVC.Inventory.UpdateExistingSerialRecived(existserial.Rows[0]["DocNo"].ToString(), Convert.ToInt32(existserial.Rows[0]["SerialNo"]));
                            //}
                            //else
                            //{
                            //    lblalert.Text = "Not Available";
                            //    return;
                            //}

                            _inputReptPickSerials.Tus_ser_id =serialid;
                        }
                        else
                        {
                            _inputReptPickSerials.Tus_ser_id = 0;
                        }

                    


                    _inputReptPickSerials.Tus_ser_1 =(isseriaitem == "1")? txtserialnumber1.Text.Trim():"N/A";
                    _inputReptPickSerials.Tus_ser_2 = txtserialnumber2.Text.Trim();
                    _inputReptPickSerials.Tus_ser_3 = txtserialnum3.Text.Trim();
                    _inputReptPickSerials.Tus_bin = ddlbincode.SelectedValue;
                    _inputReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
                    _inputReptPickSerials.Tus_session_id = (string)Session["GlbUserSessionID"];
                    if (dtdoccheck.Rows.Count > 0)
                    {
                        _inputReptPickSerials.Tus_base_doc_no = dtdoccheck.Rows[0]["TUH_DOC_NO"].ToString();
                    }
                    else
                    {
                        _inputReptPickSerials.Tus_base_doc_no = _scanDocument;
                    }
                    // _scanDocument; /*_userSeqNo.ToString()*/
                    _inputReptPickSerials.Tus_base_itm_line = Convert.ToInt32(linenumber);
                    LoadExpiryDate();
                    DataTable expstus = CHNLSVC.Inventory.getItemExpStatus(txtitemcode.Text.Trim());
                    if (expstus.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(txtexpirydate.Text) && txtexpirydate.Text != "01/Jan/1901" && expstus.Rows[0]["MI_IS_EXP_DT"].ToString()=="1")
                        {
                            _inputReptPickSerials.Tus_exp_dt = Convert.ToDateTime(txtexpirydate.Text);
                        }
                        else
                        {
                            _inputReptPickSerials.Tus_exp_dt = DateTime.MinValue.Date;
                        }
                    }
                    else
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Invalid item code";
                        return;

                    }
                    //if (isseriaitem != "1" && temppickitems.Rows.Count > 1 && iscurrent == "CurrentJobb")
                    //{
                    //    decimal tempScnQty=qtyforscan;
                    //     foreach (DataRow dtrw in temppickitems.Rows)
                    //     {
                    //         if (tempScnQty == 0) {
                    //             break;
                    //         }
                    //         if ((Convert.ToDecimal(dtrw["TUI_REQ_ITM_QTY"].ToString()) - Convert.ToDecimal(dtrw["TUI_PIC_ITM_QTY"].ToString()) != 0))
                    //         {
                    //             if (tempScnQty > 0 && ((Convert.ToDecimal(dtrw["TUI_REQ_ITM_QTY"].ToString()) - Convert.ToDecimal(dtrw["TUI_PIC_ITM_QTY"].ToString())) < tempScnQty))
                    //             {
                    //                 _inputReptPickSerials.Tus_qty = Convert.ToDecimal(dtrw["TUI_REQ_ITM_QTY"].ToString()) - Convert.ToDecimal(dtrw["TUI_PIC_ITM_QTY"].ToString());
                    //                 _inputReptPickSerials.Tus_base_itm_line = Convert.ToInt32(dtrw["tui_pic_itm_cd"].ToString());
                    //                 tempScnQty = tempScnQty - _inputReptPickSerials.Tus_qty;//Convert.ToDecimal(dtrw["TUI_REQ_ITM_QTY"].ToString()) - Convert.ToDecimal(dtrw["TUI_PIC_ITM_QTY"].ToString());
                    //             }
                    //             else
                    //             {
                    //                 _inputReptPickSerials.Tus_qty = tempScnQty;
                    //                 _inputReptPickSerials.Tus_base_itm_line = Convert.ToInt32(dtrw["tui_pic_itm_cd"].ToString());
                    //                 tempScnQty = tempScnQty - _inputReptPickSerials.Tus_qty;
                    //             }
                    //             DataTable dtDocDetails = CHNLSVC.Inventory.getCurrentDocumentDetails(Session["UserCompanyName"].ToString(), _scanDocument, Session["UserDefLoca"].ToString(), warecom, wareloc, loadingpoint);
                    //             if (dtDocDetails.Rows.Count > 0 && temppickitems.Rows.Count > 0)
                    //             {
                    //                 if (dtDocDetails.Rows[0]["TUH_IS_TAKE_RES"] != null && dtDocDetails.Rows[0]["TUH_IS_TAKE_RES"].ToString() == "1")
                    //                     _inputReptPickSerials.Tus_resqty = _inputReptPickSerials.Tus_qty;
                    //             }

                    //             _inputReptPickSerials.Tus_itm_line = Convert.ToInt32(dtrw["tui_pic_itm_cd"].ToString());
                    //             valserial = CHNLSVC.Inventory.SavePickedItemSerialsPDA(_inputReptPickSerials);
                    //         }
                    //     }
                    //   // reqqty < qtyforscan + picqty
                    //}
                    //else {
                    //    valserial = CHNLSVC.Inventory.SavePickedItemSerialsPDA(_inputReptPickSerials);
                    //}

                    //if (Convert.ToInt32(valserial) == -1)
                    //{
                    //    divalert.Visible = true;
                    //    lblalert.Text = "Error in processing !!!";
                    //    return;
                    //}
                    //  Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(company, location, txtitemcode.Text.ToUpper().Trim().ToString(), Convert.ToInt32(serialid), -1);
                }

                Int32 eff = CHNLSVC.Inventory.saveStockOutDetails(dtdoccheck, _inputReptPickHeader, isseriaitem, temppickitems, 
                    iscurrent, qtyforscan, _items, iscreatejob, _itemslines,dtsericlavailable,_inputReptPickSerials,
                    company, location, txtitemcode.Text.ToUpper().Trim().ToString(),serialid,_scanDocument,warecom,wareloc,loadingpoint,locserialcheck,
                    out glberror);
                if (eff == -1 || glberror != "")
                {
                    divalert.Visible = true;
                    lblalert.Text = glberror;
                    return;
                }

                #endregion

                //result = CHNLSVC.Inventory.SaveAllItemsSerials(_inputReptPickHeader, _items, _itemslines, _inputReptPickSerials);
                //if (Convert.ToInt32(result) == -1)
                //{
                //    divalert.Visible = true;
                //    lblalert.Text = "Error in processing !!!";
                //    return;
                //}
                DataTable dt = (DataTable)ViewState["SERIALTABLE"];
                ViewState["SERIALTABLE"] = dt;

                if (dt.Rows.Count > 0)
                {
                    DataTable dtcopy = ViewState["SERIALTABLE"] as DataTable;

                    if (!string.IsNullOrEmpty(txtserialnumber1.Text.Trim()))
                    {
                        dtcopy.Rows.Add(txtserialnumber1.Text.Trim());
                    }

                    if (!string.IsNullOrEmpty(txtserialnumber2.Text.Trim()))
                    {
                        dtcopy.Rows.Add(txtserialnumber2.Text.Trim());
                    }

                    if (!string.IsNullOrEmpty(txtserialnum3.Text.Trim()))
                    {
                        dtcopy.Rows.Add(txtserialnum3.Text.Trim());
                    }

                    dtcopy.Merge(dt);
                    ViewState["SERIALTABLE"] = dtcopy;
                }
                else
                {
                    DataTable dtoriginal = ViewState["SERIALTABLE"] as DataTable;

                    if (!string.IsNullOrEmpty(txtserialnumber1.Text.Trim()))
                    {
                        dtoriginal.Rows.Add(txtserialnumber1.Text.Trim());
                    }

                    if (!string.IsNullOrEmpty(txtserialnumber2.Text.Trim()))
                    {
                        dtoriginal.Rows.Add(txtserialnumber2.Text.Trim());
                    }

                    if (!string.IsNullOrEmpty(txtserialnum3.Text.Trim()))
                    {
                        dtoriginal.Rows.Add(txtserialnum3.Text.Trim());
                    }

                    ViewState["SERIALTABLE"] = dtoriginal;
                }

                if (isseriaitem == "1")
                {
                    divokjob.Visible = true;

                    if ((!string.IsNullOrEmpty(txtserialnumber1.Text.Trim())) && (!string.IsNullOrEmpty(txtserialnumber2.Text.Trim())) && (!string.IsNullOrEmpty(txtserialnum3.Text.Trim())))
                    {
                        lblokjob.Text = "Good Scan : " + txtserialnumber1.Text.Trim() + "" + " ; " + txtserialnumber2.Text.Trim() + "" + " ; " + txtserialnum3.Text.Trim();
                    }
                    else if ((!string.IsNullOrEmpty(txtserialnumber1.Text.Trim())) && (!string.IsNullOrEmpty(txtserialnumber2.Text.Trim())))
                    {
                        lblokjob.Text = "Good Scan : " + txtserialnumber1.Text.Trim() + "" + " ; " + txtserialnumber2.Text.Trim() + "";
                    }
                    else if (!string.IsNullOrEmpty(txtserialnumber1.Text.Trim()))
                    {
                        lblokjob.Text = "Good Scan : " + txtserialnumber1.Text.Trim() + "";
                    }

                    //if (chkblkserial.Checked == false)
                    //{
                    //    txtitemcode.Text = string.Empty;
                    //}
                    txtserialnumber1.Focus();
                }
                else
                {
                    divokjob.Visible = true;
                    lblokjob.Text = "Good Scan !!!";

                    //if (chkbulkitems.Checked == false)
                    //{
                    //    txtitemcode.Text = string.Empty;
                    //}
                    txtqty.Text = string.Empty;

                    if (!string.IsNullOrEmpty(txtbarcode.Text.Trim()))
                    {
                        txtbarcode.Focus();
                    }
                    else
                    {
                        txtqty.Focus();
                    }
                }
                //LoadScannedItemQty();
                loadScanedQty();
                getTotalDocQty();
                GetLastScanSerial();
                updateItemQty();
                //LoadGrid();
                itemlistdiv.Visible = false;
                Clear();

                if (focusSer == "SER1")
                {
                    DataTable tbl = new DataTable("tbl");
                    txtitemcode.Text = "";
                    txtserialnumber1.Text = "";
                    txtserialnumber2.Text = "";
                    txtserialnum3.Text = "";
                    txtbarcode.Text = "";
                    txtqty.Text = "";
                    ddlitmstatus.DataSource = tbl;
                    ddlbincode.DataSource = tbl;
                    txtserialnumber1.Focus();
                }
                else if (focusSer == "SER2")
                {
                    DataTable tbl = new DataTable("tbl");
                    txtitemcode.Text = "";
                    txtserialnumber1.Text = "";
                    txtserialnumber2.Text = "";
                    txtserialnum3.Text = "";
                    txtbarcode.Text = "";
                    txtqty.Text = "";
                    ddlitmstatus.DataSource = tbl;
                    ddlbincode.DataSource = tbl;
                    txtserialnumber2.Focus();
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "scrollTop();HideLabelAuto();", true);
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = ex.Message.ToString();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        protected void btnok_Click(object sender, EventArgs e)
        {
            try
            {

                Session["LastSerialNo"] = txtserialnumber1.Text;
                if (Session["Doctype"].ToString() == "STJO")
                {
                    SaveData();
                }
                else
                {
                    CheckStockAvailability();
                    SaveData();
                }
               
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
        }

        protected void btncheck_Click(object sender, EventArgs e)
        {
            try
            {
                DivsHide();

                string passitem = string.Empty;
                string passbin = string.Empty;
                string passitemstatus = string.Empty;

                if (chkallitems.Checked == false)
                {
                    if (string.IsNullOrEmpty(txtitemcode.Text.Trim()))
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Please enter item code !!!";
                        txtitemcode.Focus();
                        SetScrollTop();
                        return;
                    }
                    passitem = txtitemcode.Text.ToUpper().Trim();
                }

                if (chkallbin.Checked == false)
                {
                    if (!string.IsNullOrEmpty(ddlbincode.Text))
                    {
                        if (ddlbincode.SelectedItem.Text == "Select")
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Please select bin code !!!";
                            ddlbincode.Focus();
                            SetScrollTop();
                            return;
                        }
                        passbin = ddlbincode.SelectedValue;
                    }
                }

                if (chkallstatus.Checked == false)
                {
                    if (!string.IsNullOrEmpty(ddlbincode.Text))
                    {
                        if (ddlitmstatus.SelectedItem.Text == "Select")
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Please select item status !!!";
                            ddlitmstatus.Focus();
                            SetScrollTop();
                            return;
                        }
                        passitemstatus = ddlitmstatus.SelectedValue;
                    }
                }

                Session["CHECKBUTTON"] = "1";
                Response.Redirect("CheckScannedStock.aspx?Item=" + passitem + "&Bin=" + passbin + "&ItemStatus=" + passitemstatus, false);
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
        }

        protected void chkbulkitems_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DivsHide();
                //if (chkbulkitems.Checked == true)
                //{
                //    txtitemcode.ReadOnly = true;
                //}
                //else
                //{
                //    txtitemcode.ReadOnly = false;
                //}
                txtbarcode.Focus();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
        }

        protected void ddlbincode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DivsHide();
                string isser = (string)Session["SERIALIZED"];
                string doctype = (string)Session["DOCTYPE"];
                string docno = (string)Session["DOCNO"];
                string locserialcheck = (string)Session["LOCISSERIAL"];
                if (isser == "1")
                {
                    txtserialnumber1.Focus();
                }
                else
                {
                    txtbarcode.Focus();
                }

                DataTable dtitmstatus = CHNLSVC.Inventory.LoadItemStatusOfBins(Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString(), txtitemcode.Text.ToUpper().Trim(), ddlbincode.SelectedValue,docno,doctype);
                if (dtitmstatus.Rows.Count > 0)
                {
                    ddlitmstatus.DataSource = dtitmstatus;
                    ddlitmstatus.DataTextField = "Status";
                    ddlitmstatus.DataValueField = "inb_itm_stus";
                    ddlitmstatus.DataBind();

                    if (ddlitmstatus.Items.Count > 1)
                    {
                        ddlitmstatus.Items.Insert(0, new ListItem("Select", ""));   
                    }
                    ddlitmstatus_SelectedIndexChanged(null, null);
                }
                else
                {
                    if (locserialcheck == "False")
                    {
                        dtitmstatus.Rows.Add("GOD", "GOOD");
                        ddlitmstatus.DataSource = dtitmstatus;
                        ddlitmstatus.DataTextField = "Status";
                        ddlitmstatus.DataValueField = "inb_itm_stus";
                        ddlitmstatus.DataBind();

                        if (ddlitmstatus.Items.Count > 1)
                        {
                            ddlitmstatus.Items.Insert(0, new ListItem("Select", ""));
                        }
                        ddlitmstatus_SelectedIndexChanged(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
        }


        private void LoadScannedItemQty()
        {
            try
            {
                string seqnoforitm = (string)Session["SEQNO"].ToString();
                string warecom = (string)Session["WAREHOUSE_COMPDA"];
                string wareloc = (string)Session["WAREHOUSE_LOCPDA"];
                string loadingpoint = (string)Session["LOADING_POINT"];

                DataTable dttotqty = CHNLSVC.Inventory.GetItemTotalQty(Convert.ToInt32(seqnoforitm),warecom,wareloc,loadingpoint);

                if (dttotqty.Rows.Count > 0)
                {
                    foreach (DataRow ddrtotitem in dttotqty.Rows)
                    {
                        lblscqty.Text = ddrtotitem["TOTQTY"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void GetLastScanSerial()
        {
            try
            {
                string _lstscanserial = (string)Session["LASTSCANSERIAL"];

                if (!string.IsNullOrEmpty(txtserialnum3.Text.Trim()))
                {
                    _lstscanserial = txtserialnum3.Text.Trim();
                }
                else if (!string.IsNullOrEmpty(txtserialnumber2.Text.Trim()))
                {
                    _lstscanserial = txtserialnumber2.Text.Trim();
                }
                else if (!string.IsNullOrEmpty(txtserialnumber1.Text.Trim()))
                {
                    _lstscanserial = txtserialnumber1.Text.Trim();
                }
                Session["LASTSCANSERIAL"] = _lstscanserial;
              //  lstscnserial.Text = _lstscanserial;
                lstscnserial.Text = Session["LastSerialNo"] as string;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
        }
        protected void ddlitmstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DivsHide();
                string isser = (string)Session["SERIALIZED"];
                if (isser == "1")
                {
                    txtserialnumber1.Focus();
                }
                else
                {
                    txtbarcode.Focus();
                }
                Session["ITEMSTATUS"] = ddlitmstatus.SelectedValue;
                LoadCurrentQtyofBin();
                //LoadExpiryDate();
                //LoadScannedItemQty();
                loadScanedQty();
                getTotalDocQty();
                
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
        }

        private void SessionClear()
        {
            try
            {
                Session["LOCCHANGED"] = null;
                //Session["DOCDIRECTION"] = null;
                //Session["DOCTYPE"] = null;
                Session["SEQNO"] = null;
                Session["SERIALIZED"] = null;
                Session["UOM"] = null;
                Session["DOCNO"] = null;
                Session["SER2"] = null;
                Session["SER3"] = null;
                Session["ISITEMACTIVE"] = null;
                Session["ITEM"] = null;
                Session["ITEMSTATUS"] = null;
                Session["LASTSCANSERIAL"] = null;
                Session["CHECKBUTTON"] = null;
                Session["SELECTED_JOB"] = null;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
        }
        protected void btnfinish_Click(object sender, EventArgs e)
        {
            try
            {
                string finishStus=(string)Session["FINISHED"];
                if (finishStus == "0")
                {
                    string docno = (string)Session["DOCNO"];
                    Int32 docseqno = Convert.ToInt32(Session["SEQNO"].ToString());
                    string doctype = (string)Session["DOCTYPE"];
                    string iscreatejob =(string)Session["CreateJobNumber"];
                    if (docno != "")
                    {
                        if (iscreatejob == "CreateJobNumber" && doctype == "AOD")
                        {
                            doctype = "PDA";
                        }
                        DateTime nowdate = DateTime.Now;
                        string error = string.Empty;
                        Int32 eff = CHNLSVC.Inventory.updateDocumentFinishStatusSeq(docseqno, doctype, 1, nowdate, out error);
                        if (eff == 1 && error == "")
                        {
                            ViewState["SERIALTABLE"] = null;
                            DivsHide();
                            Clear();
                            SessionClear();
                            Response.Redirect("CurrentJobs.aspx?DocType=" + doctype);
                        }
                        else
                        {
                            if (error != "")
                            {
                                divalert.Visible = true;
                                lblalert.Text = error;
                                return;
                            }
                            else
                            {
                                divalert.Visible = true;
                                lblalert.Text = "Unable to finish job.";
                                return;
                            }
                        }
                    }
                    else
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Invalid document number.";
                        return;
                    }
                }
                else {
                    divalert.Visible = true;
                    lblalert.Text = "Already finished document.";
                    return;
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
        }

        protected void chkallitems_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DivsHide();
                if (chkallitems.Checked == true)
                {
                    txtitemcode.Text = string.Empty;
                    txtitemcode.ReadOnly = true;
                }
                else
                {
                    txtitemcode.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
        }

        protected void chkallbin_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DivsHide();
                if (chkallbin.Checked == true)
                {
                    ddlbincode.Enabled = false;
                }
                else
                {
                    ddlbincode.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
        }

        protected void chkallstatus_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DivsHide();
                if (chkallstatus.Checked == true)
                {
                    ddlitmstatus.Enabled = false;
                }
                else
                {
                    ddlitmstatus.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
        }

        private void LoadCurrentQtyofBin()
        {
            try
            {
                DataTable dtqty = CHNLSVC.Inventory.LoadItemQtyOfBins(Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString(), txtitemcode.Text.ToUpper().Trim(), ddlbincode.SelectedValue, ddlitmstatus.SelectedValue);
                if (dtqty.Rows.Count > 0)
                {
                    string qty = string.Empty;
                    foreach (DataRow ddr in dtqty.Rows)
                    {
                        qty = ddr[0].ToString();
                        Session["QTYOFBIN"] = qty;
                    }
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void LoadExpiryDate()
        {
            try
            {
                DataTable dtexp = CHNLSVC.Inventory.LoadItemExpDate(Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString(), txtitemcode.Text.ToUpper().Trim(), ddlbincode.SelectedValue);
                if (dtexp.Rows.Count > 0)
                {
                    foreach (DataRow ddr in dtexp.Rows)
                    {
                        if (!string.IsNullOrEmpty(ddr[0].ToString()))
                        {
                            DateTime expdt = Convert.ToDateTime(ddr[0].ToString());
                            string epdatetext = expdt.ToString("dd/MMM/yyyy");
                            txtexpirydate.Text = epdatetext;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void loadScanedQty()
        {
            try
            {
                if (lbljobno.Text != "")
                {
                    Int32 userseq = Convert.ToInt32(Session["SEQNO"].ToString());
                    DataTable dttotqty = CHNLSVC.Inventory.GetItemTotalScanedQtySeq(userseq);
                    if (dttotqty.Rows.Count > 0)
                    {
                        foreach (DataRow ddrtotitem in dttotqty.Rows)
                        {
                            lblscqty.Text = (ddrtotitem["SEQ_QTY"].ToString() != "") ? ddrtotitem["SEQ_QTY"].ToString() : "0";
                        }
                    }
                }
                else
                {
                    lblalert.Text = "Invalid document number.";
                }
                
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
        }
        private void getTotalDocQty()
        {
            try
            {
                if ((String)Session["DOCNO"] != "")
                {
                    Int32 seqno = Convert.ToInt32(Session["SEQNO"].ToString());
                    DataTable dtdoccount = CHNLSVC.Inventory.GetItemTotalDocumentQtySeq(seqno);

                    foreach (DataRow ddr in dtdoccount.Rows)
                    {
                        lbldocqty.Text = ddr["DOC_QTY"].ToString();
                    }
                }
                else
                {
                    lblalert.Text = "Invalid document number.";
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        //protected void btItmCdClk_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
                

        //    }
        //    catch (Exception ex)
        //    {
        //        divalert.Visible = true;
        //        //lblalert.Text = ex.Message;
        //        lblalert.Text = "Server connection error.";
        //    }
        //}
        protected void btnitmcheck_Click(object sender, EventArgs e)
        {
            try
            {
                //Response.Redirect("CheckJobItems.aspx", false);
                itemlistdiv.Visible = true;
                LoadGrid();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
        }
        protected void LoadGrid()
        {
            Int32 seqno = Convert.ToInt32(Session["SEQNO"].ToString());
            DataTable dtitems = CHNLSVC.Inventory.loadDocumentItemsSeq(seqno);
            grdjobitems.DataSource = null;
            grdjobitems.DataBind();

            grdjobitems.DataSource = dtitems;
            grdjobitems.DataBind();
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                Session["PrintDoc"] = null;
                string company = Session["UserCompanyName"].ToString();
                string userId = Session["UserID"].ToString();
                string doctype = (string)Session["DOCTYPE"];
                string docno = (string)Session["DOCNO"];
                string locserialcheck = (string)Session["LOCISSERIAL"];
                lblalert.Text = "";
                lblokjob.Text = "";
                string error = string.Empty;
                InvoiceHeader _invoiceheader = new InvoiceHeader();
                InventoryHeader _inventoryHeader = new InventoryHeader();
                MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
                MasterAutoNumber _inventoryAuto = new MasterAutoNumber();
                List<ReptPickSerialsSub> _reptPickSerialsSub = new List<ReptPickSerialsSub>();
                List<Transport> _traList = new List<Transport>();
                ReptPickHeader tempHdr = new ReptPickHeader();
                string iscurrent = Session["CurrentJobb"] as string;
                if (iscurrent == null && doctype == "AOD")
                {
                    doctype = "PDA";
                }
                tempHdr = CHNLSVC.Inventory.getTemporyHeaderDetails(docno, doctype, company, out error);
                if (tempHdr.Tuh_doc_no != null)
                {
                    if (tempHdr.Tuh_isdirect == true)
                    {
                        if (doctype == "AOD")
                        {
                            doctype = "PDA";
                        }
                    }
                    if (doctype == "MRNA" || doctype == "PDA")
                    {
                        #region AOD genarate
                        if (!CHNLSVC.Security.Is_OptionPerimitted(company, userId, 16070))
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Sorry, You have no permission to genarate AOD Out.( Advice: Required permission code : 16070) !";
                            return;
                        }
                        else
                        {

                            string sessionId = (string)Session["GlbUserSessionID"];
                            string _message = string.Empty;
                            string _genInventoryDoc = string.Empty;
                            string _genSalesDoc = string.Empty;
                            bool _isGRANfromDIN = false;
                            bool _isGRAN = false;


                            if (error != "")
                            {
                                divalert.Visible = true;
                                lblalert.Text = error;
                                return;
                            }
                            if (tempHdr != null && tempHdr.Tuh_doc_no != null)
                            {
                                List<ReptPickSerials> _reptPickSerials = new List<ReptPickSerials>();
                                _reptPickSerials = CHNLSVC.Inventory.getScanedSerials(tempHdr.Tuh_usrseq_no, out error);
                                if (error != "")
                                {
                                    divalert.Visible = true;
                                    lblalert.Text = error;
                                    return;
                                }
                                else
                                {
                                    if (_reptPickSerials.Count > 0)
                                    {
                                        #region Check Duplicate Serials
                                        var _dup = _reptPickSerials.Where(x => x.Tus_ser_id != 0).Select(y => y.Tus_ser_id).ToList();

                                        string _duplicateItems = string.Empty;
                                        bool _isDuplicate = false;
                                        if (_dup != null)
                                            if (_dup.Count > 0)
                                                foreach (Int32 _id in _dup)
                                                {
                                                    Int32 _counts = _reptPickSerials.Where(x => x.Tus_ser_id == _id).Select(y => y.Tus_ser_id).Count();
                                                    if (_counts > 1)
                                                    {
                                                        _isDuplicate = true;
                                                        var _item = _reptPickSerials.Where(x => x.Tus_ser_id == _id).Select(x => x.Tus_itm_cd).Distinct();
                                                        foreach (string _str in _item)
                                                            if (string.IsNullOrEmpty(_duplicateItems))
                                                                _duplicateItems = _str;
                                                            else
                                                                _duplicateItems += "," + _str;
                                                    }
                                                }
                                        if (_isDuplicate)
                                        {
                                            string msg = "Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems;
                                            divalert.Visible = true;
                                            lblalert.Text = msg;
                                            return;
                                        }
                                        #endregion

                                        #region Inventory AutoNumber

                                        _inventoryAuto.Aut_cate_cd = Session["UserDefLoca"].ToString();
                                        _inventoryAuto.Aut_cate_tp = "LOC";
                                        _inventoryAuto.Aut_direction = null;
                                        _inventoryAuto.Aut_modify_dt = null;
                                        _inventoryAuto.Aut_moduleid = string.Empty;
                                        _inventoryAuto.Aut_start_char = string.Empty;
                                        _inventoryAuto.Aut_modify_dt = null;
                                        _inventoryAuto.Aut_year = DateTime.Now.Year;
                                        #endregion
                                        bool _isInvalidDocument = false;

                                        #region Inventory Header Value Assign
                                        _inventoryHeader.Ith_acc_no = string.Empty;
                                        _inventoryHeader.Ith_anal_1 = string.Empty;
                                        _inventoryHeader.Ith_anal_10 = (tempHdr.Tuh_isdirect) ? true : false;//Direct AOD
                                        _inventoryHeader.Ith_anal_11 = false;
                                        _inventoryHeader.Ith_anal_12 = false;
                                        _inventoryHeader.Ith_anal_2 = string.Empty;
                                        _inventoryHeader.Ith_anal_3 = string.Empty;
                                        _inventoryHeader.Ith_anal_4 = string.Empty;
                                        _inventoryHeader.Ith_anal_5 = string.Empty;
                                        _inventoryHeader.Ith_anal_6 = 0;
                                        _inventoryHeader.Ith_anal_7 = 0;
                                        _inventoryHeader.Ith_anal_8 = DateTime.Now.Date;
                                        _inventoryHeader.Ith_anal_9 = DateTime.Now.Date;
                                        _inventoryHeader.Ith_bus_entity = string.Empty;
                                        _inventoryHeader.Ith_cate_tp = string.Empty;
                                        _inventoryHeader.Ith_channel = string.Empty;
                                        _inventoryHeader.Ith_com = company;
                                        _inventoryHeader.Ith_com_docno = string.Empty;
                                        _inventoryHeader.Ith_cre_by = userId;
                                        _inventoryHeader.Ith_cre_when = DateTime.Now.Date;
                                        _inventoryHeader.Ith_del_add1 = string.Empty;
                                        _inventoryHeader.Ith_del_add2 = string.Empty;
                                        _inventoryHeader.Ith_del_code = string.Empty;
                                        _inventoryHeader.Ith_del_party = string.Empty;
                                        _inventoryHeader.Ith_del_town = string.Empty;
                                        _inventoryHeader.Ith_direct = false;
                                        _inventoryHeader.Ith_doc_date = DateTime.Now.Date;
                                        _inventoryHeader.Ith_doc_no = string.Empty;
                                        _inventoryHeader.Ith_doc_tp = string.Empty;
                                        _inventoryHeader.Ith_doc_year = DateTime.Now.Date.Date.Year;
                                        _inventoryHeader.Ith_entry_no = docno;
                                        _inventoryHeader.Ith_entry_tp = string.Empty;
                                        _inventoryHeader.Ith_git_close = false;
                                        _inventoryHeader.Ith_git_close_date = DateTime.Now.Date;
                                        _inventoryHeader.Ith_git_close_doc = string.Empty;
                                        _inventoryHeader.Ith_is_manual = false;
                                        _inventoryHeader.Ith_isprinted = false;
                                        _inventoryHeader.Ith_job_no = "";
                                        _inventoryHeader.Ith_loading_point = string.Empty;
                                        _inventoryHeader.Ith_loading_user = string.Empty;
                                        _inventoryHeader.Ith_loc = Session["UserDefLoca"].ToString();
                                        _inventoryHeader.Ith_manual_ref = "";
                                        _inventoryHeader.Ith_mod_by = Session["UserID"].ToString();
                                        _inventoryHeader.Ith_mod_when = DateTime.Now.Date;
                                        _inventoryHeader.Ith_noofcopies = 0;
                                        _inventoryHeader.Ith_oth_loc = tempHdr.Tuh_rec_loc;
                                        _inventoryHeader.Ith_oth_docno = (tempHdr.Tuh_isdirect) ? string.Empty : docno;
                                        _inventoryHeader.Ith_remarks = "";
                                        _inventoryHeader.Ith_sbu = string.Empty;
                                        //_inventoryHeader.Ith_seq_no = 0; removed by Chamal 12-05-2013
                                        _inventoryHeader.Ith_session_id = Session["GlbUserSessionID"].ToString();
                                        _inventoryHeader.Ith_stus = "A";
                                        _inventoryHeader.Ith_sub_tp = string.Empty;
                                        _inventoryHeader.Ith_vehi_no = string.Empty;
                                        _inventoryHeader.Ith_oth_com = tempHdr.Tuh_rec_com;
                                        _inventoryHeader.Ith_anal_1 = _isInvalidDocument == true ? "1" : "0";
                                        _inventoryHeader.Ith_anal_2 = string.Empty;

                                        _inventoryHeader.Ith_sub_tp = doctype;
                                        _inventoryHeader.Ith_vehi_no = "";//add rukshan 06/jan/2016
                                        _inventoryHeader.Ith_anal_3 = "";//add rukshan 06/jan/2016

                                        #endregion
                                        if (_inventoryHeader.Ith_sub_tp == "PDA")
                                        {
                                            _reptPickSerials.ToList().ForEach(c => c.Tus_new_remarks = "AOD-OUT");
                                        }

                                        MasterLocation _outLoc = CHNLSVC.General.GetLocationByLocCode(_inventoryHeader.Ith_com, _inventoryHeader.Ith_loc);
                                        MasterLocation _inLoc = CHNLSVC.General.GetLocationByLocCode(_inventoryHeader.Ith_oth_com, _inventoryHeader.Ith_oth_loc);
                                        #region Cheak Request Data valid 22 Oct 2016
                                        if (_inventoryHeader.Ith_sub_tp == "MRNA")
                                        {
                                            InventoryRequest _invReq = CHNLSVC.Inventory.GET_INT_REQ_DATA(new InventoryRequest() { Itr_req_no = _inventoryHeader.Ith_oth_docno }).FirstOrDefault();
                                            if (_invReq != null)
                                            {
                                                _inventoryHeader.Ith_oth_loc = (_inventoryHeader.Ith_oth_loc != "") ? tempHdr.Tuh_rec_loc : _invReq.Itr_rec_to;
                                                _inventoryHeader.Ith_oth_com = (_inventoryHeader.Ith_oth_com != "") ? tempHdr.Tuh_rec_com : _invReq.Itr_issue_com;
                                                List<MasterItem> _mstItemList = new List<MasterItem>();
                                                List<InventoryRequestItem> _invReqItemList = CHNLSVC.Inventory.GET_INT_REQ_ITM_DATA(new InventoryRequestItem() { Itri_seq_no = _invReq.Itr_seq_no });
                                                var _pickSerialDataList = _reptPickSerials.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus }).
                                                    Select(group => new { Peo = group.Key, theCount = group.Sum(c => c.Tus_qty) });
                                                var _invReqDataList = _invReqItemList.GroupBy(x => new { x.Itri_itm_cd, x.Itri_itm_stus }).
                                                    Select(group => new { Peo = group.Key, theCount = group.Sum(c => c.Itri_bqty) });
                                                foreach (var _serData in _pickSerialDataList)
                                                {
                                                    foreach (var _reqData in _invReqDataList)
                                                    {
                                                        if (_serData.Peo.Tus_itm_cd == _reqData.Peo.Itri_itm_cd && _serData.Peo.Tus_itm_stus == _reqData.Peo.Itri_itm_stus)
                                                        {
                                                            if (_serData.theCount > _reqData.theCount)
                                                            {
                                                                _mstItemList.Add(new MasterItem() { Mi_cd = _reqData.Peo.Itri_itm_cd });
                                                            }
                                                        }
                                                    }
                                                }
                                                if (_mstItemList.Count > 0)
                                                {
                                                    string _itms = "";
                                                    foreach (var _mstItem in _mstItemList)
                                                    {
                                                        _itms = string.IsNullOrEmpty(_itms) ? _mstItem.Mi_cd : _itms + ", " + _mstItem.Mi_cd;
                                                    }

                                                    divalert.Visible = true;
                                                    lblalert.Text = "Request item quantity exceed ";
                                                    return;
                                                }

                                                _inventoryHeader.Ith_gen_frm = "PDA";
                                                _inventoryHeader.TMP_IS_RES_UPDATE = true;
                                                _inventoryHeader.TMP_UPDATE_BASE_ITM = true;
                                                _inventoryHeader.TMP_CHK_SER_IS_AVA = true;
                                                _inventoryHeader.TMP_CHK_LOC_BAL = true;
                                                if (_inventoryHeader.Ith_sub_tp == "MRNA")
                                                {
                                                    foreach (var _rptSer in _reptPickSerials)
                                                    {
                                                        _rptSer.Tus_resqty = _rptSer.Tus_qty;
                                                    }
                                                }
                                            }
                                        }
                                        #endregion

                                        #region add validation for allocation data chk 09 Jan 2016
                                        if (_inventoryHeader.Ith_sub_tp != "MRNA")
                                        {
                                            if (_inventoryHeader.Ith_sub_tp != "INTR")
                                            {
                                                _inventoryHeader.TMP_IS_ALLOCATION = true;
                                                List<ReptPickSerials> _allocationErrList = CHNLSVC.Inventory.AllocationDataValidateAodOut(_reptPickSerials, _inLoc, Session["UserDefLoca"].ToString());
                                                if (_allocationErrList.Count > 0)
                                                {
                                                    divalert.Visible = true;
                                                    lblalert.Text = _allocationErrList[0].Tmp_err_msg;
                                                    return;
                                                }
                                            }
                                        }

                                        #endregion
                                    }
                                    else
                                    {
                                        divalert.Visible = true;
                                        lblalert.Text = "Please scan serial for genarate AOD Document.";
                                        return;
                                    }
                                }
                                if (_inventoryHeader.Ith_oth_com == null || _inventoryHeader.Ith_oth_com == string.Empty)
                                {
                                    divalert.Visible = true;
                                    lblalert.Text = "Other company cannot be empty.";
                                    return;
                                }
                                InventoryRequest _invReq1 = CHNLSVC.Inventory.GET_INT_REQ_DATA(new InventoryRequest { Itr_req_no = _inventoryHeader.Ith_oth_docno, Itr_tp = doctype }).FirstOrDefault();
                                if (_invReq1 != null)
                                {
                                    List<InventoryRequestItem> _invReqItmList = CHNLSVC.Inventory.GET_INT_REQ_ITM_DATA(new InventoryRequestItem() { Itri_seq_no = _invReq1.Itr_seq_no });
                                    foreach (var item in _invReqItmList)
                                    {
                                        foreach (var v in _reptPickSerials)
                                        {
                                            if (item.Itri_line_no == v.Tus_base_itm_line)
                                            {
                                                v.Tus_res_no = string.IsNullOrEmpty(item.Itri_res_no) ? null : item.Itri_res_no == "N/A" ? null : item.Itri_res_no;
                                                v.Tus_res_line = item.Itri_res_line;
                                                v.Tus_resqty = item.Itri_res_qty > 0 ? v.Tus_qty : 0;
                                            }
                                        }
                                    }
                                    //    if (!_resNoAva)
                                    //    {
                                    //        DispMsg("Please check the reservation data !");
                                    //        btnSave.Enabled = true;
                                    //        btnSave.CssClass = "buttonUndocolorLeft floatRight";
                                    //        btnSave.OnClientClick = "SaveConfirma();";
                                    //        return;
                                    //    }
                                    //    if (!_resBalAva)
                                    //    {
                                    //        DispMsg("Please check the reservation qty!");
                                    //        btnSave.Enabled = true;
                                    //        btnSave.CssClass = "buttonUndocolorLeft floatRight";
                                    //        btnSave.OnClientClick = "SaveConfirma();";
                                    //        return;
                                    //    }
                                }
                                Int32 _effect = CHNLSVC.Inventory.SaveCommonOutWardEntry(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), tempHdr.Tuh_rec_com, tempHdr.Tuh_doc_no, _inventoryHeader, _inventoryAuto, _invoiceheader, _invoiceAuto, _reptPickSerials, _reptPickSerialsSub, out _message, out _genSalesDoc, out _genInventoryDoc, _isGRAN, _isGRANfromDIN, _traList);
                                string Msg = string.Empty;

                                if (!string.IsNullOrEmpty(_genInventoryDoc))
                                    _genInventoryDoc.Trim().Remove(_genInventoryDoc.Length - 1);
                                if (!string.IsNullOrEmpty(_genSalesDoc))
                                    _genSalesDoc.Trim().Remove(_genSalesDoc.Length - 1);
                                if (_effect > 0)
                                {
                                    Session["PrintDoc"] = _genInventoryDoc;
                                    string msg = "Successfully processed. Document No(s) - " + _genInventoryDoc + "  " + _genSalesDoc;
                                    divokjob.Visible = true;
                                    lblokjob.Text = msg;
                                    if (doctype == "AOD" || doctype == "PDA" || doctype == "MRNA")
                                    {
                                        if (Session["PrintDoc"] != null)
                                        {
                                            btnProcess.Visible = false;
                                            btnPrint.Visible = true;
                                        }
                                    }
                                    else
                                    {
                                        btnProcess.Visible = false;
                                        btnPrint.Visible = false;
                                    }

                                    lblMssg.Text = "Do you want print now?";
                                    PopupConfBox.Show();
                                }
                                else
                                {
                                    if (_message.Contains("CHK_INLFREEQTY"))
                                    {
                                        divalert.Visible = true;
                                        lblalert.Text = "There is no free stock balance available. Please check the stock balances.";
                                        return;
                                    }
                                    else if (_message.Contains("CHK_INLRESQTY"))
                                    {
                                        divalert.Visible = true;
                                        lblalert.Text = "There is no reserved stock available. Please check the stock balances.";
                                        return;
                                    }
                                    else if (_message.Contains("NO_STOCK_BALANCE"))
                                    {
                                        divalert.Visible = true;
                                        lblalert.Text = "There is no free stock available. Please check the stock balances.[Batch]";
                                        return;
                                    }
                                    else if (_message.Contains("CHK_INLQTY"))
                                    {
                                        divalert.Visible = true;
                                        lblalert.Text = "There is no stock available. Please check the stock balances.";
                                        return;
                                    }
                                    else if (_message.Contains("CHK_INBFREEQTY"))
                                    {
                                        divalert.Visible = true;
                                        lblalert.Text = "There is no bin stock available.";
                                        return;
                                    }
                                    else if (_message.Contains("CHK_ITRIBQTY"))
                                    {
                                        divalert.Visible = true;
                                        lblalert.Text = "There is no request balance available. Please check the request balances.";
                                        return;
                                    }
                                    else if (_message.Contains("CHK_INBQTY"))
                                    {
                                        divalert.Visible = true;
                                        lblalert.Text = "Please check the stock balances.[Batch]";
                                        return;
                                    }
                                    else if (!string.IsNullOrEmpty(_message))
                                    {
                                        divalert.Visible = true;
                                        lblalert.Text = _message;
                                        return;
                                    }
                                    else
                                    {
                                        //DisplayMessage("Please check the issues of " + _message);
                                        divalert.Visible = true;
                                        lblalert.Text = "Error occurred while processing";
                                        return;
                                    }

                                }
                            }
                            else
                            {
                                divalert.Visible = true;
                                lblalert.Text = "Invalid document number.Unable to genarate AOD Out document";
                                return;
                            }
                        }
                        #endregion AOD genarate
                    }
                    else if (doctype == "DO")
                    {
                        #region DO start
                        string location = Session["UserDefLoca"].ToString();
                        DateTime DoDate = DateTime.Now;
                        if (!CHNLSVC.Security.Is_OptionPerimitted(company, userId, 16070))
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Sorry, You have no permission to genarate DO.( Advice: Required permission code : 16070) !";
                            return;
                        }
                        InvoiceHeader invHdr = CHNLSVC.Inventory.GetInvoiceDetailForPdaDO(docno, company, out error);
                        if (invHdr.Sah_inv_no != null)
                        {
                            string profitcenter = invHdr.Sah_pc;
                            int resultDate = DateTime.Compare(invHdr.Sah_dt.Date, DoDate.Date);
                            if (resultDate > 0)
                            {
                                divalert.Visible = true;
                                lblalert.Text = "Delivery date should be greater than or equal to invoice date.!";
                                return;
                            }
                            decimal _maxDO = 0;
                            decimal _maxDoDays = 0;
                            Boolean _isOk = false;
                            HpSystemParameters _SystemPara = new HpSystemParameters();
                            _SystemPara = CHNLSVC.Sales.GetSystemParameter("PC", profitcenter, "HSDO", DoDate.Date);
                            if (_SystemPara.Hsy_cd != null)
                            {
                                _maxDO = _SystemPara.Hsy_val;
                            }
                            if (_SystemPara.Hsy_cd == null)
                            {
                                _SystemPara = CHNLSVC.Sales.GetSystemParameter("SCHNL", /*BaseCls.GlbDefSubChannel*/null, "HSDO", DoDate.Date);
                                if (_SystemPara.Hsy_cd != null)
                                {
                                    _maxDO = _SystemPara.Hsy_val;
                                }
                            }
                            if (_SystemPara.Hsy_cd == null)
                            {
                                _SystemPara = CHNLSVC.Sales.GetSystemParameter("COM", company, "HSDO", DoDate.Date);
                                if (_SystemPara.Hsy_cd != null)
                                {
                                    _maxDO = _SystemPara.Hsy_val;
                                }
                            }
                            if (_maxDO > 0)
                            {
                                //maximum DO days
                                _SystemPara = CHNLSVC.Sales.GetSystemParameter("PC", profitcenter, "HSMAXDO", DoDate.Date);
                                if (_SystemPara.Hsy_cd != null)
                                {
                                    _maxDoDays = _SystemPara.Hsy_val;
                                }
                                if (_SystemPara.Hsy_cd == null)
                                {
                                    _SystemPara = CHNLSVC.Sales.GetSystemParameter("SCHNL", /*BaseCls.GlbDefSubChannel*/null, "HSMAXDO", DoDate.Date);
                                    if (_SystemPara.Hsy_cd != null)
                                    {
                                        _maxDoDays = _SystemPara.Hsy_val;
                                    }
                                }
                                if (_SystemPara.Hsy_cd == null)
                                {
                                    _SystemPara = CHNLSVC.Sales.GetSystemParameter("COM", company, "HSMAXDO", DoDate.Date);
                                    if (_SystemPara.Hsy_cd != null)
                                    {
                                        _maxDoDays = _SystemPara.Hsy_val;
                                    }
                                }
                                decimal _totsaleqty = 0;
                                int _effc = CHNLSVC.Financial.GetTotSadQty(company, BaseCls.GlbUserDefProf, out _totsaleqty);

                                DataTable _dt = CHNLSVC.Financial.IsDoDaysExceed(company, BaseCls.GlbUserDefProf, Convert.ToInt32(_maxDoDays));
                                if (_maxDoDays > 0)
                                {
                                    if (_dt.Rows.Count > 0)
                                    {
                                        divalert.Visible = true;
                                        lblalert.Text = "Exceed the allowed no of days from delivery for " + _dt.Rows[0]["sah_inv_no"].ToString() + ". Please contact Registration Department.";
                                        return;
                                    }

                                }
                            }

                            List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                            InventoryHeader invHdrNew = new InventoryHeader();
                            string documntNo = "";
                            Int32 result = -99;

                            Int32 _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("DO", company, docno, 0);
                            reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(company, location, userId, _userSeqNo, "DO");

                            if (reptPickSerialsList == null)
                            {
                                divalert.Visible = true;
                                lblalert.Text = "No delivery items found!";
                                return;
                            }
                            else
                            {
                                string _invNo = docno.ToUpper().Trim();
                                List<InvoiceItem> _invItms = CHNLSVC.Sales.GetAllInvoiceItems(_invNo);
                                if (_invItms != null)
                                {
                                    var _invItmsWithRes = _invItms.Where(c => !string.IsNullOrEmpty(c.Sad_res_no) && c.Sad_res_no != "N/A" && c.Sad_res_no != "PROMO_VOU").ToList();
                                    if (_invItmsWithRes != null && _invItmsWithRes.Count > 0)
                                    {
                                        foreach (var item in _invItmsWithRes)
                                        {
                                            foreach (var _pickSer in reptPickSerialsList)
                                            {
                                                if (_pickSer.Tus_itm_cd == item.Sad_itm_cd && _pickSer.Tus_base_itm_line == item.Sad_itm_line)
                                                {
                                                    _pickSer.Tus_resqty = _pickSer.Tus_qty;
                                                }
                                            }
                                        }
                                        invHdrNew.UpdateResLog = true;
                                    }
                                }
                            }
                            #region Check Registration Txn Serials
                            bool _isRegTxnFound = false;
                            List<VehicalRegistration> _tmpReg = new List<VehicalRegistration>();
                            _tmpReg = CHNLSVC.Sales.CheckVehRegTxn(company, BaseCls.GlbUserDefProf, docno, String.Empty);
                            if (_tmpReg != null && _tmpReg.Count > 0) _isRegTxnFound = true;

                            if (_isRegTxnFound == true)
                            {
                                foreach (ReptPickSerials _serRow in reptPickSerialsList)
                                {
                                    MasterItem _itm = CHNLSVC.Inventory.GetItem("", _serRow.Tus_itm_cd);
                                    if (_itm.Mi_need_reg == true)
                                    {
                                        int _countReg = _tmpReg.Where(x => x.P_srvt_itm_cd == _serRow.Tus_itm_cd && x.P_svrt_engine == _serRow.Tus_ser_1).Count();
                                        if (_countReg <= 0)
                                        {
                                            divalert.Visible = true;
                                            lblalert.Text = "Invliad delivery item/serial [" + _serRow.Tus_itm_cd + "] - [" + _serRow.Tus_ser_1 + "]";
                                            return;
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region Check Reference Date and the Doc Date

                            if (IsReferancedDocDateAppropriate(reptPickSerialsList, DoDate.Date) == false)
                            {
                                return;
                            }

                            #endregion Check Reference Date and the Doc Date

                            #region Check Duplicate Serials

                            var _dup = reptPickSerialsList.Where(x => x.Tus_ser_1 != "N/A").Select(y => y.Tus_ser_id).ToList();

                            string _duplicateItems = string.Empty;
                            bool _isDuplicate = false;
                            if (_dup != null)
                                if (_dup.Count > 0)
                                    foreach (Int32 _id in _dup)
                                    {
                                        Int32 _counts = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(y => y.Tus_ser_id).Count();
                                        if (_counts > 1)
                                        {
                                            _isDuplicate = true;
                                            var _item = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(x => x.Tus_itm_cd).Distinct();
                                            foreach (string _str in _item)
                                                if (string.IsNullOrEmpty(_duplicateItems))
                                                    _duplicateItems = _str;
                                                else
                                                    _duplicateItems += "," + _str;
                                        }
                                    }
                            if (_isDuplicate)
                            {
                                divalert.Visible = true;
                                lblalert.Text = "Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems;
                                return;
                            }

                            #endregion Check Duplicate Serials

                            #region Check Consignment Item has Quotation(s) :: Chamal 18-Sep-2013

                            //add by darshana on 12-Mar-2014 - To Gold operation totally operate as consignment base and no need to generate grn.
                            MasterCompany _masterComp = null;
                            _masterComp = CHNLSVC.General.GetCompByCode(company);

                            if (_masterComp.Mc_anal13 == 0)
                            {
                                if (CHNLSVC.Inventory.Check_Cons_Item_has_Quo(company, DoDate.Date, reptPickSerialsList, out documntNo) < 0)
                                {
                                    divalert.Visible = true;
                                    lblalert.Text = documntNo+ " Quotation not define" ;
                                    return;
                                }
                            }

                            //add by Chamal on 22-Aug-2014
                            var _consSupp = from _ListConsSupp in reptPickSerialsList
                                            where _ListConsSupp.Tus_itm_stus == "CONS"
                                            group _ListConsSupp by new { _ListConsSupp.Tus_orig_supp } into list
                                            select new { supp = list.Key.Tus_orig_supp };
                            foreach (var listsSupp in _consSupp)
                            {
                                MasterBusinessEntity _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(company, listsSupp.supp.ToString(), null, null, "S");
                                if (_supDet == null)
                                {
                                    divalert.Visible = true;
                                    lblalert.Text = "Cannot find supplier details.";
                                    return;
                                }
                            }

                            #endregion Check Consignment Item has Quotation(s) :: Chamal 18-Sep-2013

                            List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                            reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "DO");

                            DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(company, location);
                            foreach (DataRow r in dt_location.Rows)
                            {
                                // Get the value of the wanted column and cast it to string
                                invHdrNew.Ith_sbu = (string)r["ML_OPE_CD"];
                                if (System.DBNull.Value != r["ML_CATE_2"])
                                {
                                    invHdrNew.Ith_channel = (string)r["ML_CATE_2"];
                                }
                                else
                                {
                                    invHdrNew.Ith_channel = string.Empty;
                                }

                                if (System.DBNull.Value != r["ML_CATE_1"])
                                {
                                    invHdrNew.Ith_cate_tp = (string)r["ML_CATE_1"];
                                }
                                else
                                {
                                    invHdrNew.Ith_cate_tp = invHdr.Sah_inv_tp;
                                }

                            }
                            #region Fill DO Header

                            invHdrNew.Ith_loc = location;
                            invHdrNew.Ith_com = company;
                            invHdrNew.Ith_doc_tp = "DO";
                            invHdrNew.Ith_doc_date = DoDate.Date;
                            invHdrNew.Ith_doc_year = DoDate.Date.Year;
                            invHdrNew.Ith_sub_tp = "DPS";
                            invHdrNew.Ith_is_manual = false;
                            invHdrNew.Ith_stus = "A";
                            invHdrNew.Ith_cre_by = userId;
                            invHdrNew.Ith_mod_by = userId;
                            invHdrNew.Ith_direct = false;
                            invHdrNew.Ith_session_id = (string)Session["GlbUserSessionID"]; 
                            invHdrNew.Ith_manual_ref ="";
                            invHdrNew.Ith_vehi_no = "";
                            invHdrNew.Ith_remarks = "Genarate from PDA";
                            invHdrNew.Ith_anal_1 = _userSeqNo.ToString();
                            invHdrNew.Ith_oth_docno = docno;
                            invHdrNew.Ith_entry_no = docno;
                            invHdrNew.Ith_bus_entity = invHdr.Sah_cus_cd;
                            invHdrNew.Ith_del_add1 = invHdr.Sah_cus_add1;
                            invHdrNew.Ith_del_add2 = invHdr.Sah_cus_add1;
                            invHdrNew.Ith_acc_no = invHdr.Sah_acc_no;
                            invHdrNew.Ith_pc = invHdr.Sah_pc;

                            #endregion Fill DO Header

                            MasterAutoNumber masterAutoNum = new MasterAutoNumber();
                            masterAutoNum.Aut_cate_cd = location;
                            masterAutoNum.Aut_cate_tp = "LOC";
                            masterAutoNum.Aut_direction = 0;
                            masterAutoNum.Aut_moduleid = "DO";
                            masterAutoNum.Aut_start_char = "DO";
                            masterAutoNum.Aut_year = DoDate.Date.Year;
                            List<ReptPickSerials> reptPickSerialsListGRN = new List<ReptPickSerials>();
                            List<ReptPickSerialsSub> reptPickSubSerialsListGRN = new List<ReptPickSerialsSub>();
                            if (reptPickSerialsList != null)
                            { reptPickSerialsListGRN = reptPickSerialsList; }

                            if (reptPickSubSerialsList != null)
                            { reptPickSubSerialsListGRN = reptPickSubSerialsList; }

                            InventoryHeader _invHeaderGRN = null;
                            MasterAutoNumber _masterAutoGRN = null;
                            string documntNoGRN = "";
                            bool IsGrn = false;
                            if (IsGrn)
                            {
                                _invHeaderGRN = new InventoryHeader();
                                _invHeaderGRN.Ith_com = company;
                                _invHeaderGRN.Ith_loc = location;
                                _invHeaderGRN.Ith_doc_date = DoDate.Date;
                                _invHeaderGRN.Ith_doc_year = DoDate.Date.Year;
                                _invHeaderGRN.Ith_direct = true;
                                _invHeaderGRN.Ith_doc_tp = "GRN";
                                _invHeaderGRN.Ith_cate_tp = "NOR";
                                _invHeaderGRN.Ith_sub_tp = "LOCAL";
                                //_invHeader.Ith_bus_entity = lblSupplierCode.Text;
                                _invHeaderGRN.Ith_is_manual = true;
                                _invHeaderGRN.Ith_manual_ref = "";
                                _invHeaderGRN.Ith_remarks = "Genarate from PDA";
                                _invHeaderGRN.Ith_stus = "A";
                                _invHeaderGRN.Ith_cre_by = userId;
                                _invHeaderGRN.Ith_cre_when = DateTime.Now;
                                _invHeaderGRN.Ith_mod_by = userId;
                                _invHeaderGRN.Ith_mod_when = DateTime.Now;
                                _invHeaderGRN.Ith_session_id = (string)Session["GlbUserSessionID"];
                                _invHeaderGRN.Ith_oth_docno = docno;
                                _invHeaderGRN.Ith_entry_no = docno;

                                _masterAutoGRN = new MasterAutoNumber();
                                _masterAutoGRN.Aut_cate_cd = location;
                                _masterAutoGRN.Aut_cate_tp = "LOC";
                                _masterAutoGRN.Aut_direction = null;
                                _masterAutoGRN.Aut_modify_dt = null;
                                _masterAutoGRN.Aut_moduleid = "GRN";
                                _masterAutoGRN.Aut_number = 0;
                                _masterAutoGRN.Aut_start_char = "GRN";
                                _masterAutoGRN.Aut_year = _invHeaderGRN.Ith_doc_date.Date.Year;

                                //result = CHNLSVC.Inventory.DeliveryOrder_Auto(invHdr, reptPickSerialsList, reptPickSubSerialsList, masterAutoNum, out documntNo, _invHeaderGRN, reptPickSerialsListGRN, reptPickSubSerialsListGRN, _masterAutoGRN, out documntNoGRN);
                            }
                            invHdrNew.Ith_gen_frm = "PDA";
                            result = CHNLSVC.Inventory.DeliveryOrderEntry(invHdrNew, reptPickSerialsList, reptPickSubSerialsList, masterAutoNum, out documntNo, _invHeaderGRN, reptPickSerialsListGRN, reptPickSubSerialsListGRN, _masterAutoGRN, out  documntNoGRN, IsGrn, null);

                            if (result != -99 && result >= 0)
                            {
                                Session["PrintDoc"] = documntNo;
                                divokjob.Visible = true;
                                lblokjob.Text = "Delivery Order Note Successfully Saved! Document No : " + documntNo ;
                                if (doctype == "DO")
                                {
                                    if (Session["PrintDoc"] != null)
                                    {
                                        btnProcess.Visible = false;
                                        btnPrint.Visible = true;
                                    }
                                }
                                else
                                {
                                    btnProcess.Visible = false;
                                    btnPrint.Visible = false;
                                }

                                lblMssg.Text = "Do you want print now?";
                                PopupConfBox.Show();
                            }
                            else
                            {
                                if (documntNo.Contains("EMS.CHK_INLFREEQTY"))
                                {
                                    divalert.Visible = true;
                                    lblalert.Text ="There is no free stock balance available." + "\n" + "Please check the stock balances.";
                                    return;
                                }
                                else if (documntNo.Contains("EMS.CHK_INBFREEQTY"))
                                {
                                    divalert.Visible = true;
                                    lblalert.Text = "There is no free stock balance available." + "\n" + "Please check the stock balances.";
                                    return;
                                }
                                else
                                {
                                    divalert.Visible = true;
                                    lblalert.Text = "Please check the issues of ";
                                    return;
                                }
                            }

                        }
                        else
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Invalid invoice number.Unable to genarate DO";
                            return;
                        }
                    }
                    else
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Invalid document number.Unable to genarate AOD Out document";
                        return;
                    }
                        #endregion DO end
                }
                else
                {
                    divalert.Visible = true;
                    lblalert.Text = "Invalid document number.";
                    return;
                }
               
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message.ToString();
                //lblalert.Text = "Server connection error.";
            }
        }
        protected void btnNo_Click(object sender, EventArgs e)
        { }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string printdoc = (Session["PrintDoc"] != null) ? Session["PrintDoc"].ToString() : "";
                string warecom = (string)Session["WAREHOUSE_COMPDA"];
                string wareloc = (string)Session["WAREHOUSE_LOCPDA"];
                if (printdoc != "")
                {
                    string company = Session["UserCompanyName"].ToString();
                    string userId = Session["UserID"].ToString();
                    string doctype = (string)Session["DOCTYPE"];
                    string docno = (string)Session["DOCNO"];
                    string sessionId = Session["GlbUserSessionID"].ToString();
                    string error = string.Empty;
                    string iscurrent = Session["CurrentJobb"] as string;
                    string loadingpoint = (string)Session["LOADING_POINT"];
                    if (iscurrent == null && doctype == "AOD")
                    {
                        doctype = "PDA";
                    }
                    if (doctype == "MRNA" || doctype == "PDA")
                    {
                        if (!CHNLSVC.Security.Is_OptionPerimitted(company, userId, 16070))
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Sorry, You have no permission to genarate AOD Out.( Advice: Required permission code : 16070) !";
                            return;
                        }
                        else
                        {
                            Int32 eff = CHNLSVC.Inventory.addDocumentPrintNew(printdoc, userId, doctype, sessionId, loadingpoint,wareloc, out error);
                            if (error != "")
                            {
                                divalert.Visible = true;
                                lblalert.Text = error;
                                return;
                            }
                            else
                            {
                                Session["CHECKBUTTON"] = null;
                                if (iscurrent == "CurrentJobb")
                                {
                                    Response.Redirect("CurrentJobs.aspx?DocType=" + doctype, false);
                                }
                                else
                                {
                                    Response.Redirect("CreateJob.aspx");
                                }
                            }
                        }
                    }
                    
                }
                else
                {
                    divalert.Visible = true;
                    lblalert.Text = "Invalid document number.Unable to print.";
                    return;
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message.ToString();
            }
        }
        public bool IsReferancedDocDateAppropriate(List<ReptPickSerials> _reptPrickSerialList, DateTime _processDate)
        {
            bool _appropriate = true;
            if (_reptPrickSerialList != null)
            {
                var _isInAppropriate = _reptPrickSerialList.Where(x => x.Tus_doc_dt.Date > _processDate.Date).ToList();
                if (_isInAppropriate == null || _isInAppropriate.Count <= 0) _appropriate = true;
                else _appropriate = false;
                if (_appropriate == false)
                {
                    StringBuilder _documents = new StringBuilder();
                    foreach (ReptPickSerials _one in _isInAppropriate)
                    {
                        if (string.IsNullOrEmpty(_documents.ToString()))
                            _documents.Append(_one.Tus_doc_no + "- dated " + _one.Tus_doc_dt.ToShortDateString() + " where item " + _one.Tus_itm_cd + "/n");
                        else
                            _documents.Append(" and " + _one.Tus_doc_no + "- dated " + _one.Tus_doc_dt.ToShortDateString() + " where item " + _one.Tus_itm_cd + "/n");
                    }
                    divalert.Visible = true;
                    lblalert.Text ="The Inward documents " + _documents.ToString() + " equal or grater than to a this Outward document " + _processDate.Date.ToShortDateString() + " date!";
                  
                }
            }
            return _appropriate;
        }
        protected void btnTracker_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["DOCNO"] != null)
                {
                    string docno = Session["DOCNO"].ToString();
                    Response.Redirect("SerialTracker.aspx?docno=" + docno, false);
                }
                else
                {
                    Response.Redirect("SerialTracker.aspx", false);
                }
                

            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message.ToString();
            }
        }
    }
}