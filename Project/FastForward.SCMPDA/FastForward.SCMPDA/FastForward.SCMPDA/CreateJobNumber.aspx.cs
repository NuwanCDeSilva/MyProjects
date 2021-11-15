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
using FF.BusinessObjects.InventoryNew;

namespace FastForward.SCMPDA
{
    public partial class CreateJobNumber : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string autonumber = string.Empty;
                    string doctype = (string)Session["DOCTYPE"];
                    string currjobno = string.Empty;
                    string ischeckbuttinclick = (string)Session["CHECKBUTTON"];
                    if (doctype == "AOD" || doctype == "GRN" || doctype=="SRN")
                    {
                        btnProcess.Visible = true;
                    }
                    else
                    {
                        btnProcess.Visible = false;
                    }
                    btnPrint.Visible = false;

                    string warecom = (string)Session["WAREHOUSE_COMPDA"];
                    string wareloc = (string)Session["WAREHOUSE_LOCPDA"];
                    string loadingpoint = (string)Session["LOADING_POINT"];
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
                        lbljobno.Text =  currjobno ;
                    }
                    else
                    {
                        Int32 serialno = 0;
                        serialno = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), doctype, 1, Session["UserCompanyName"].ToString());
                        Session["SEQNO"] = serialno;
                        autonumber = Session["UserDefLoca"].ToString() + "-" + doctype + serialno + Session["ToLocation"];
                        Session["DOCNO"] = autonumber;
                        lbljobno.Text = autonumber ;
                    }
                    Int32 userseq = Convert.ToInt32(Session["SEQNO"].ToString());
                    PopulateDropDowns();
                    txtitemcode.Focus();
                    serialdv.Visible = false;
                    serial2dv.Visible = false;
                    serial3dv.Visible = false;
                    nonserdv.Visible = false;
                    entrynodv.Visible = false;
                    //LoadItemQty();
                    loadScanedQty();
                    getTotalDocQty();
                    GetLastScanSerial();
                    grdjobitems.DataSource = new int[] { };
                    grdjobitems.DataBind();
                    //LoadGrid();
                    itemlistdiv.Visible = false;
                    Session["PrintDoc"] = null;
                    DataTable dtserials = new DataTable();
                    dtserials.Columns.AddRange(new DataColumn[1] { new DataColumn("No") });
                    ViewState["SERIALTABLE"] = dtserials;
                    chkallitems.Checked = true;
                    chkallbin.Checked = true;
                    chkallstatus.Checked = true;
                    chkallbinAll.Checked = true;
                    Session["FINISHED"] = "0";

                    DataTable dtdoccheck = CHNLSVC.Inventory.IsDocAvailableWithSeq(Session["UserCompanyName"].ToString(), userseq, Session["UserDefLoca"].ToString(), warecom, wareloc, loadingpoint);
                    if (dtdoccheck.Rows.Count > 0 && dtdoccheck.Rows[0]["TUS_FIN_STUS"].ToString() == "1")
                    {
                        Session["FINISHED"] = "1";
                        divalert.Visible = true;
                        lblalert.Text = "Already finished document.";
                        return;
                    }
                    Session["DocHasSer"] = "False";
                    if (dtdoccheck.Rows.Count > 0 && doctype=="AOD")
                    {
                        decimal sercount = CHNLSVC.Inventory.getDocumentSerialCount(currjobno);
                        if (sercount > 0)
                        {
                            Session["DocHasSer"] = "True";
                        }
                    }
                    if (dtdoccheck.Rows.Count > 0)
                    {
                        Session["DOCNO"] = dtdoccheck.Rows[0]["TUH_DOC_NO"].ToString();
                        Session["DOCTYPE"] = dtdoccheck.Rows[0]["TUH_DOC_TP"].ToString();
                        doctype = (string)Session["DOCTYPE"];
                    }
                    if (Session["DOCTYPE"].ToString() == "GRN")
                    {
                        entrynodv.Visible = true;
                    }
                }
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

        private void SetScrollTop()
        {
            try
            {
                Page.SetFocus(this.dvcreatejobnumber.ClientID);
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = ex.Message.ToString();
            }
        }

        private void PopulateDropDowns()
        {
            try
            {
                //DataTable _CompanyItemStatus = CacheLayer.Get<DataTable>(CacheLayer.Key.CompanyItemStatus.ToString());
                DataTable _CompanyItemStatus = (DataTable)Session["dtstatus"];
                if (_CompanyItemStatus.Rows.Count > 0)
                {
                    ddlitmstatus.DataSource = _CompanyItemStatus;
                    ddlitmstatus.DataTextField = "MIS_DESC";
                    ddlitmstatus.DataValueField = "MIC_CD";
                    ddlitmstatus.DataBind();
                }
                ddlitmstatus.Items.Insert(0, new ListItem("Select", ""));
                ddlitmstatus.SelectedValue = "GOD";
                Session["ITEMSTATUS"] = ddlitmstatus.SelectedValue;

                //DataTable dtbincode = CHNLSVC.Inventory.LoadBinCode(Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString());
               // DataTable dtbincode = (DataTable)Session["dtbincode"];
                //if (dtbincode.Rows.Count > 0)
                //{
                //    ddlbincode.DataSource = dtbincode;
                //    ddlbincode.DataTextField = "ibn_bin_cd";
                //    ddlbincode.DataValueField = "ibn_bin_cd";
                //    ddlbincode.DataBind();

                //    ddlbincode.Items.Insert(0, new ListItem("Select", ""));
                //    ddlbincode.SelectedIndex = 0;
                //    DataRow[] result = dtbincode.Select("IBN_IS_DEF = 1");
                //    //Int32 isdef = 0;
                //    string defcode = result[0][0].ToString();
                //    if (defcode == "")
                //    {
                //        divalert.Visible = true;
                //        //lblalert.Text = ex.Message;
                //        lblalert.Text = "Please set default bin.";
                //    }
                //    ddlbincode.SelectedValue = defcode;
                //    //string defcode = string.Empty;

                //    //foreach (DataRow ddr in dtbincode.Rows)
                //    //{
                //    //    isdef = Convert.ToInt32(ddr[1].ToString());

                //    //    if (isdef == 1)
                //    //    {
                //    //        defcode = ddr[0].ToString();
                //    //        ddlbincode.SelectedValue = defcode;
                //    //    }
                //    //}
                //}
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message.ToString();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
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
                lblalert.Text = ex.Message;
            }
        }

        private void DivsHide()
        {
            try
            {
                itemlistdiv.Visible = false;
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
                            serialdv.Visible = true;
                            serial2dv.Visible = true;
                            serial3dv.Visible = true;
                            nonserdv.Visible = false;
                        }
                        else if (isserialized2 == "1")
                        {
                            serialdv.Visible = true;
                            serial2dv.Visible = true;
                            serial3dv.Visible = false;
                            nonserdv.Visible = false;
                        }
                        else if (isserialized1 == "1")
                        {
                            serialdv.Visible = true;
                            serial2dv.Visible = false;
                            serial3dv.Visible = false;
                            nonserdv.Visible = false;
                        }
                        else
                        {
                            serialdv.Visible = false;
                            serial2dv.Visible = false;
                            serial3dv.Visible = false;
                            nonserdv.Visible = true;
                        }
                    }
                }
                else
                {
                    serialdv.Visible = false;
                    serial2dv.Visible = false;
                    serial3dv.Visible = false;
                    nonserdv.Visible = false;
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
        
        protected void btItmCdClk_Click(object sender, EventArgs e)
        {
            try
            {
                //DivsHide();
                //Session["SERIALIZED"] = null;
                //string iscurrent = (string)Session["CurrentJobb"];
                //if (iscurrent == "CurrentJobb")
                //{
                //    string docNo = (string)Session["DOCNO"];
                //    DataTable itemValidate = CHNLSVC.Inventory.GetItemDataInTempPickItem(txtitemcode.Text.ToUpper().Trim(), docNo);
                //    if (itemValidate.Rows.Count == 0)
                //    {
                //        divalert.Visible = true;
                //        lblalert.Text = "Please enter valid item code in document.!";
                //        txtitemcode.Text = "";
                //        txtitemcode.Focus();
                //        return;
                //    }
                //    decimal totalPickItmQty = 0;
                //    decimal totalReqItmQty = 0;
                //    if (itemValidate.Rows.Count > 0)
                //    {
                //        foreach (DataRow clm in itemValidate.Rows)
                //        {
                //            totalPickItmQty = totalPickItmQty + Convert.ToDecimal(clm["TUI_PIC_ITM_QTY"].ToString());
                //            totalReqItmQty = totalReqItmQty + Convert.ToDecimal(clm["TUI_REQ_ITM_QTY"].ToString());
                //        }
                //    }
                //    itmScnQty.Text = totalPickItmQty.ToString();
                //    itmDocQty.Text = totalReqItmQty.ToString();
                //    if (totalReqItmQty == 0)
                //    {
                //        divalert.Visible = true;
                //        lblalert.Text = "Item request qunatity is zero.!";
                //        txtitemcode.Focus();
                //        return;
                //    }
                //    if (totalPickItmQty == totalReqItmQty)
                //    {
                //        divalert.Visible = true;
                //        lblalert.Text = "Cannot exceed request item quantity.!";
                //        txtqty.Text = "";
                //        txtqty.Focus();
                //        return;
                //    }
                //}
                //ItemValidateAndSetStatus();
                //if (!ValidateItems()) {
                //    return;
                //}
                ////LoadItemQty();
                //loadScanedQty();
                ////getTotalDocQty();
                //string isser = (string)Session["SERIALIZED"];
                //if (isser == "1")
                //{
                //    txtserialnumber1.Focus();
                //}
                //else
                //{
                //    txtbarcode.Focus();
                //}
                
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
        }
        protected void txtitemcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Int32 userseq = Convert.ToInt32(Session["SEQNO"].ToString());
                DivsHide();
                Session["SERIALIZED"] = null;
                string iscurrent = (string)Session["CurrentJobb"];
                string docNo = (string)Session["DOCNO"];
                string doctype = (string)Session["DOCTYPE"];
                if (iscurrent == "CurrentJobb")
                {
                   
                    DataTable itemValidate = CHNLSVC.Inventory.GetItemDataInTempPickItemWithSeq(txtitemcode.Text.ToUpper().Trim(), userseq);
                    if (itemValidate.Rows.Count == 0 && doctype!="GRN")
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
                    if (totalReqItmQty == 0 && doctype!="GRN")
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Item request qunatity is zero.!";
                        txtitemcode.Focus();
                        return;
                    }
                    if (totalPickItmQty == totalReqItmQty)
                    {
                        if (doctype != "GRN")
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Cannot exceed request item quantity or item status missmatch.!";
                            txtqty.Text = "";
                            txtqty.Focus();
                            return;
                        }
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
                    else {
                        itmScnQty.Text = "0";
                        itmDocQty.Text = "0";
                        binScnQty.Text = "0";
                    }
                }
                ItemValidateAndSetStatus();
                if (!ValidateItems())
                {
                    return;
                }
                if (txtBinCode.Text == "")
                {
                    DataTable dtbincode = (DataTable)Session["dtbincode"];
                    if (dtbincode.Rows.Count > 0)
                    {
                        DataRow[] result = dtbincode.Select("IBN_IS_DEF = 1");
                        string defcode = result[0][0].ToString();
                        if (defcode == "")
                        {
                            divalert.Visible = true;
                            //lblalert.Text = ex.Message;
                            txtBinCode.Text = "";
                            lblalert.Text = "Please enter valid bin.";
                            txtBinCode.Focus();
                            return;
                        }
                        txtBinCode.Text = defcode.ToString();
                        if (txtBinCode.Text == "")
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Please enter bin code !";
                            txtBinCode.Focus();
                            //SetScrollTop();
                            return;
                        }
                    }
                }
                else
                {
                    try
                    {
                        DataTable dtbincode = (DataTable)Session["dtbincode"];
                        if (dtbincode.Rows.Count > 0)
                        {
                            bool contains = dtbincode.AsEnumerable().Any(row => txtBinCode.Text.ToString() == row.Field<String>("IBN_BIN_CD"));
                            if (contains == false)
                            {
                                divalert.Visible = true;
                                //lblalert.Text = ex.Message;
                                txtBinCode.Text = "";
                                lblalert.Text = "Please enter valid bin.";
                                txtBinCode.Focus();
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        divalert.Visible = true;
                        txtBinCode.Text = "";
                        lblalert.Text = "Please enter valid bin code";
                        txtBinCode.Focus();
                        return;
                    }
                }
                //LoadItemQty();
                loadScanedQty();
                //getTotalDocQty();
                string isser = (string)Session["SERIALIZED"];
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
            catch (Exception ex) {
                divalert.Visible = true;
                //        //lblalert.Text = ex.Message;
                lblalert.Text = "Server connection error.";
            }
        }
        //protected void txtitemcode_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DivsHide();
        //        Session["SERIALIZED"] = null;

        //        ItemValidateAndSetStatus();

        //        //LoadItemQty();
        //        loadScanedQty(); 

        //        string isser = (string)Session["SERIALIZED"];
        //        if (isser == "1")
        //        {
        //            txtserialnumber1.Focus();
        //        }
        //        else
        //        {
        //            txtbarcode.Focus();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        divalert.Visible = true;
        //        //lblalert.Text = ex.Message;
        //        lblalert.Text = "Server connection error.";
        //    }
        //}

        protected void chkblkserial_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DivsHide();
                txtitemcode.Focus();
                //if (chkblkserial.Checked == true)
                //{
                //    txtitemcode.ReadOnly = true;
                //}
                //else
                //{
                //    txtitemcode.ReadOnly = false;
                //}
                //txtserialnumber1.Focus();
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
                ddlitmstatus.SelectedIndex = 2;
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
                //chkbulkitems.Checked = true;
                chkallstatus.Checked = true;
                chkallbin.Checked = true;
                txtitemcode.Focus();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = ex.Message.ToString();
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
                        lblinfo.Text = "Serial is available in stock !";
                     //   txtserialnumber1.Text = string.Empty;
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
                lblalert.Text = ex.Message.ToString();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void CheckCom()
        {
            CheckBoxQR.Checked = false;
            qrDiv.Visible = false;
            qrDivDrop.Visible = false;
        }
        protected void chkqr_CheckedChanged(object sender, EventArgs e)
        {
            if (!CheckBoxQR.Checked)
            {
                qrDivDrop.Visible = false;
            }
            else
            {
                qrDivDrop.Visible = true;
            }
        }
        protected void txtSerial1_TextChanged(object sender, EventArgs e)
        {
           
            if (CheckBoxQR.Checked == true)
            {
                int method = Convert.ToInt32(DropDownListQRCom.SelectedValue.ToString());
                if (method != 0)
                {
                    string newSerial = GetSerial(txtserialnumber1.Text, method);
                    if (!(string.IsNullOrEmpty(newSerial)))
                    {
                        txtserialnumber1.Text = newSerial.ToUpper();
                    }
                }
                else
                {
                    lblalert.Text = "Please select a company";
                    txtserialnumber1.Text = string.Empty;
                    return;
                }
            }
            else
            {
                DataTable paraTB = CHNLSVC.Inventory.getMstSysPara(Session["UserCompanyCode"].ToString(), "COM", Session["UserCompanyCode"].ToString(), "SERLENRES", Session["UserCompanyCode"].ToString());
                if (paraTB != null)
                {

                    if (paraTB.Rows.Count > 0)
                    {
                        if (!(string.IsNullOrEmpty(paraTB.Rows[0]["MSP_REST_VAL"].ToString())))
                        {
                            int lenght = 0;
                            lenght = Convert.ToInt32(paraTB.Rows[0]["MSP_REST_VAL"].ToString());
                            if (txtserialnumber1.Text.Trim().Length > lenght)
                            {
                                divalert.Visible = true;
                                lblalert.Text = "Serial length is more than " + lenght+ "Please use QR method";
                                txtserialnumber1.Text = string.Empty;
                                return;
                            }
                        }
                    }
                }     
            }
        }
        //Dulaj 2018/Oct/12
        private string GetSerial(string scannedCode, int method)
        {
            string serial = scannedCode;
            //   if (serDet != null)
            //   {
            //   if (serDet.Rows.Count > 0)
            //{
            DataTable qrMethods = new DataTable();

            if (method != 0)
            {
                qrMethods = CHNLSVC.Inventory.GetQRMethod(Session["UserCompanyCode"].ToString(), method);
                if (qrMethods != null)
                {
                    if (qrMethods.Rows.Count > 0)
                    {
                        Int32 serialPosition = Convert.ToInt32(qrMethods.Rows[0]["MQM_SERIAL_POS"].ToString());
                        string separator = qrMethods.Rows[0]["MQM_CHAR"].ToString();
                        bool checkqr = scannedCode.Contains(separator);
                        if (checkqr)
                        {
                            string[] tokens = scannedCode.Split(new[] { separator }, StringSplitOptions.None);
                            serial = tokens[serialPosition];
                        }
                    }
                }
            }
            // }
            //    }
            return serial;
        }
        private bool ValidateItems()
        {
            string iscurrent = (string)Session["CurrentJobb"];
            string isseriaitem = (string)Session["SERIALIZED"];
            string doctype = (string)Session["DOCTYPE"];
            Int32 userseq = Convert.ToInt32(Session["SEQNO"].ToString());
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

            if (iscurrent == "CurrentJobb")
            {
                string docNo = (string)Session["DOCNO"];
                DataTable itemValidate = CHNLSVC.Inventory.GetItemDataInTempPickItemWithSeq(txtitemcode.Text.ToUpper().Trim(), userseq);
                if (itemValidate.Rows.Count == 0 && doctype!="GRN")
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please enter valid item code in document.!";
                    txtitemcode.Text = "";
                    txtitemcode.Focus();
                    return false;
                }
                if (itemValidate.Rows.Count > 0 && txtserialnumber1.Text.ToString() != "" && doctype=="AOD")
                {
                    bool has = false;
                    foreach (DataRow row in itemValidate.Rows)
                    {
                        if (Convert.ToInt32(row["TUI_REQ_ITM_QTY"]) > 0)
                        {
                            if (row["TUI_REQ_ITM_STUS"].ToString() == ddlitmstatus.SelectedValue.ToString())
                            {
                                has = true;
                            }
                        }
                    }
                    if (!has)
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Mismatch in selected status.!";
                        if (isseriaitem == "1")
                        {
                            txtserialnumber1.Focus();
                        }
                        else {
                            txtqty.Focus();
                        }
                        return false;
                    }
                }
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
                if (totalReqItmQty == 0 && doctype!="GRN")
                {
                    divalert.Visible = true;
                    lblalert.Text = "Item request qunatity is zero.!";
                    txtitemcode.Focus();
                    return false;
                }

                if (isseriaitem == "1")
                {
                    if (totalPickItmQty + 1 > totalReqItmQty)
                    {
                        if (doctype != "GRN")
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Cannot exceed request item quantity or item status missmatch.!";
                            txtqty.Text = "";
                            txtqty.Focus();
                            return false;
                        }
                    }
                }
                else
                {
                    if (totalPickItmQty + qty > totalReqItmQty)
                    {
                        if (doctype != "GRN")
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Cannot exceed request item quantity  or item status missmatch.!";
                            txtqty.Text = "";
                            txtqty.Focus();
                            return false;
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(txtitemcode.Text.Trim()))
            {
                divalert.Visible = true;
                lblalert.Text = "Please scan/enter item code !";
                txtitemcode.Focus();
                //SetScrollTop();
                return false;
            }

            DataTable dtitemdata = CHNLSVC.Inventory.GetItemData(txtitemcode.Text.ToUpper().Trim());

            if (dtitemdata.Rows.Count == 0)
            {
                divalert.Visible = true;
                lblalert.Text = "Invalid item code !";
                txtitemcode.Text = string.Empty;
                txtitemcode.Focus();
                //SetScrollTop();
                return false;
            }

           string isitemactive = (string)Session["ISITEMACTIVE"];
            //Int16 actval = dtitemdata.Rows[0].Field<Int16>(6);
            //string isitemactive = actval.ToString();
            if (isitemactive !="1")
            {
                divalert.Visible = true;
                lblalert.Text = "Item is not active !";
                txtitemcode.Text = string.Empty;
                txtitemcode.Focus();
                //SetScrollTop();
                return false;
            }

            //if (ddlbincode.SelectedIndex == 0)
            //{
            //    divalert.Visible = true;
            //    lblalert.Text = "Please select bin code !";
            //    ddlbincode.Focus();
            //    //SetScrollTop();
            //    return false;
            //}
            
            
            if (ddlitmstatus.SelectedIndex == 0)
            {
                divalert.Visible = true;
                lblalert.Text = "Please select item status !";
                ddlitmstatus.Focus();
                //SetScrollTop();
                return false;
            }

            //string isseriaitem = (string)Session["SERIALIZED"];
            //if (isseriaitem == "1")
            //{
            //    //if (string.IsNullOrEmpty(txtserialnumber1.Text.Trim()))
            //    if (Session["LastSerialNo"] as string=="")
            //    {
            //        divalert.Visible = true;
            //        lblalert.Text = "Please scan serial number 1 !";
            //        txtserialnumber1.Focus();
            //        //SetScrollTop();
            //        return false;
            //    }

            //    string serial2 = (string)Session["SER2"];
            //    if (serial2 == "1")
            //    {
            //        if (string.IsNullOrEmpty(txtserialnumber2.Text.Trim()))
            //        {
            //            divalert.Visible = true;
            //            lblalert.Text = "Please scan serial number 2 !";
            //            txtserialnumber2.Focus();
            //            //SetScrollTop();
            //            return false;
            //        }
            //    }

            //    string serial3 = (string)Session["SER3"];
            //    if (serial3 == "1")
            //    {
            //        if (string.IsNullOrEmpty(txtserialnum3.Text.Trim()))
            //        {
            //            divalert.Visible = true;
            //            lblalert.Text = "Please scan serial number 3 !";
            //            txtserialnum3.Focus();
            //            //SetScrollTop();
            //            return false;
            //        }
            //    }

            //    if ((!string.IsNullOrEmpty(txtserialnumber1.Text.Trim())) && (!string.IsNullOrEmpty(txtserialnumber2.Text.Trim())))
            //    {
            //        if (txtserialnumber1.Text.Trim() == txtserialnumber2.Text.Trim())
            //        {
            //            divalert.Visible = true;
            //            lblalert.Text = "Serial 1 & Serial 2 are equal !";
            //            txtserialnumber2.Text = String.Empty;
            //            txtserialnumber2.Focus();
            //            //SetScrollTop();
            //            return false;
            //        } 
            //    }

            //    if ((!string.IsNullOrEmpty(txtserialnumber1.Text.Trim())) && (!string.IsNullOrEmpty(txtserialnum3.Text.Trim())))
            //    {
            //        if (txtserialnumber1.Text.Trim() == txtserialnum3.Text.Trim())
            //        {
            //            divalert.Visible = true;
            //            lblalert.Text = "Serial 1 & Serial 3 are equal !";
            //            txtserialnum3.Text = String.Empty;
            //            txtserialnum3.Focus();
            //            //SetScrollTop();
            //            return false;
            //        }
            //    }

            //    if ((!string.IsNullOrEmpty(txtserialnumber2.Text.Trim())) && (!string.IsNullOrEmpty(txtserialnum3.Text.Trim())))
            //    {
            //        if (txtserialnumber2.Text.Trim() == txtserialnum3.Text.Trim())
            //        {
            //            divalert.Visible = true;
            //            lblalert.Text = "Serial 2 & Serial 3 are equal !";
            //            txtserialnum3.Text = String.Empty;
            //            txtserialnum3.Focus();
            //            //SetScrollTop();
            //            return false;
            //        }
            //    }

            //    if (lbldocqty.Text.ToString() != "")
            //    {
            //        if (lbldocqty.Text.ToString() != "0")
            //        {
            //            if (Convert.ToInt32(lblscqty.Text.ToString()) + 1 > Convert.ToInt32(lbldocqty.Text.ToString()))
            //            {
            //                divalert.Visible = true;
            //                lblalert.Text = "You Cannot Exceed Doc Quantity ";
            //                return false;
            //            }
            //        }

            //    }

                
            //    DataTable dt = (DataTable)ViewState["SERIALTABLE"];
            //    if (dt != null)
            //    {
            //        foreach (DataRow ddrcache in dt.Rows)
            //        {
            //            if ((ddrcache["No"].ToString() == txtserialnumber1.Text.Trim()) || (ddrcache["No"].ToString() == txtserialnumber2.Text.Trim()) || (ddrcache["No"].ToString() == txtserialnum3.Text.Trim()))
            //            {
            //                divalert.Visible = true;
            //                lblalert.Text = "Some serial/serials have already scanned !";
            //                txtserialnumber1.Text = String.Empty;
            //                txtserialnumber1.Focus();
            //                //SetScrollTop();
            //                return false;
            //            }
            //        }
            //    }
               

            //    ViewState["SERIALTABLE"] = dt;

            //    if (dt !=null)
            //    {
            //        DataTable dtcopy = ViewState["SERIALTABLE"] as DataTable;

            //        if (!string.IsNullOrEmpty(txtserialnumber1.Text.Trim()))
            //        {
            //            dtcopy.Rows.Add(txtserialnumber1.Text.Trim());
            //        }

            //        if (!string.IsNullOrEmpty(txtserialnumber2.Text.Trim()))
            //        {
            //            dtcopy.Rows.Add(txtserialnumber2.Text.Trim());
            //        }

            //        if (!string.IsNullOrEmpty(txtserialnum3.Text.Trim()))
            //        {
            //            dtcopy.Rows.Add(txtserialnum3.Text.Trim());
            //        }

            //        dtcopy.Merge(dt);
            //        ViewState["SERIALTABLE"] = dtcopy;
            //    }
            //    else
            //    {
            //        DataTable dtserials = new DataTable();
            //        dtserials.Columns.AddRange(new DataColumn[1] { new DataColumn("No") });
            //        ViewState["SERIALTABLE"] = dtserials;
            //        DataTable dtoriginal = ViewState["SERIALTABLE"] as DataTable;
                   
            //        if (!string.IsNullOrEmpty(txtserialnumber1.Text.Trim()))
            //        {
            //            dtoriginal.Rows.Add(txtserialnumber1.Text.Trim());
            //        }

            //        if (!string.IsNullOrEmpty(txtserialnumber2.Text.Trim()))
            //        {
            //            dtoriginal.Rows.Add(txtserialnumber2.Text.Trim());
            //        }

            //        if (!string.IsNullOrEmpty(txtserialnum3.Text.Trim()))
            //        {
            //            dtoriginal.Rows.Add(txtserialnum3.Text.Trim());
            //        }

            //        ViewState["SERIALTABLE"] = dtoriginal;
            //    }
            //}
            //else
            //{
                //if ((string.IsNullOrEmpty(txtbarcode.Text.Trim())) && (string.IsNullOrEmpty(txtqty.Text.Trim())))
                //{
                //    if (string.IsNullOrEmpty(txtqty.Text.Trim()))
                //    {
                //        divalert.Visible = true;
                //        lblalert.Text = "Please enter qty or scan barcode !";
                //        txtqty.Focus();
                //        //SetScrollTop();
                //        return false;
                //    }
                //}

                //if ((string.IsNullOrEmpty(txtbarcode.Text.Trim())) && (!string.IsNullOrEmpty(txtqty.Text.Trim())))
                //{
                //    if (Convert.ToDecimal(txtqty.Text.Trim()) == 0)
                //    {
                //        divalert.Visible = true;
                //        lblalert.Text = "0 qty is not allowed !";
                //        txtqty.Focus();
                //        //SetScrollTop();
                //        return false;
                //    }
                //}

                //if ((string.IsNullOrEmpty(txtbarcode.Text.Trim())) && (!string.IsNullOrEmpty(txtqty.Text.Trim())))
                //{
                //    if (Convert.ToDecimal(txtqty.Text.Trim()) < 0)
                //    {
                //        divalert.Visible = true;
                //        lblalert.Text = "Minus values are not allowed !";
                //        txtqty.Text=String.Empty;
                //        txtqty.Focus();
                //        //SetScrollTop();
                //        return false;
                //    }
                //}

                //string uom = (string)Session["UOM"];
                //DataTable dtuom = CHNLSVC.Inventory.IsItemUOMDecimalAllow(uom);
                //if (dtuom.Rows.Count > 0)
                //{
                //    foreach (DataRow ddritemuom in dtuom.Rows)
                //    {
                //        string isdecimalallow = ddritemuom["msu_isdecimal"].ToString();

                //        if (isdecimalallow != "1")
                //        {
                //            Int32 Check_Integer;

                //            if (string.IsNullOrEmpty(txtbarcode.Text.Trim()))
                //            {
                //                if (!Int32.TryParse(txtqty.Text.Trim(), out Check_Integer))
                //                {
                //                    divalert.Visible = true;
                //                    lblalert.Text = "Not allowed decimal qty for this item !";
                //                    txtqty.Text=String.Empty;
                //                    txtqty.Focus();
                //                    //SetScrollTop();
                //                    return false;
                //                }
                //            }
                //        }
                //    }
                //}
            //}
            return true;
        }

        private bool validateSerial()
        {
            string isseriaitem = (string)Session["SERIALIZED"];
            string doctype = (string)Session["DOCTYPE"];
            string docno = (string)Session["DOCNO"];
           
            if (isseriaitem == "1")
            {
                //if (string.IsNullOrEmpty(txtserialnumber1.Text.Trim()))
                if (Session["LastSerialNo"] as string == "")
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please scan serial number 1 !";
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
                        lblalert.Text = "Please scan serial number 2 !";
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
                        lblalert.Text = "Please scan serial number 3 !";
                        txtserialnum3.Focus();
                        //SetScrollTop();
                        return false;
                    }
                }

                if ((!string.IsNullOrEmpty(txtserialnumber1.Text.Trim())) && (!string.IsNullOrEmpty(txtserialnumber2.Text.Trim())))
                {
                    if (txtserialnumber1.Text.Trim() == txtserialnumber2.Text.Trim())
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Serial 1 & Serial 2 are equal !";
                        txtserialnumber2.Text = String.Empty;
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
                        lblalert.Text = "Serial 1 & Serial 3 are equal !";
                        txtserialnum3.Text = String.Empty;
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
                        lblalert.Text = "Serial 2 & Serial 3 are equal !";
                        txtserialnum3.Text = String.Empty;
                        txtserialnum3.Focus();
                        //SetScrollTop();
                        return false;
                    }
                }

                if (lbldocqty.Text.ToString() != "")
                {
                    if (lbldocqty.Text.ToString() != "0")
                    {
                        if (Convert.ToInt32(lblscqty.Text.ToString()) + 1 > Convert.ToInt32(lbldocqty.Text.ToString()) && doctype!="GRN")
                        {
                            divalert.Visible = true;
                            lblalert.Text = "You Cannot Exceed Doc Quantity ";
                            return false;
                        }
                    }

                }
                string serialCheck = (string)Session["DocHasSer"];
                if (serialCheck == "True")
                {
                    if (Session["Doctype"].ToString() == "AOD" && isseriaitem == "1")
                    {
                        //get exist serial no
                        DataTable existserial = CHNLSVC.Inventory.getExistingSerial(txtserialnumber1.Text.ToString(), txtitemcode.Text.ToString(),docno,ddlitmstatus.SelectedValue.ToString());
                        if (existserial.Rows.Count <= 0)
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Please scan valid serial number or invalid status.";
                            return false;
                        }
                    }
                }

                DataTable dt = (DataTable)ViewState["SERIALTABLE"];
                if (dt != null)
                {
                    foreach (DataRow ddrcache in dt.Rows)
                    {
                        if ((ddrcache["No"].ToString() == txtserialnumber1.Text.Trim()) || (ddrcache["No"].ToString() == txtserialnumber2.Text.Trim()) || (ddrcache["No"].ToString() == txtserialnum3.Text.Trim()))
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Some serial/serials have already scanned !";
                            txtserialnumber1.Text = String.Empty;
                            txtserialnumber1.Focus();
                            //SetScrollTop();
                            return false;
                        }
                    }
                }


                //ViewState["SERIALTABLE"] = dt;

                //if (dt != null)
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
                //    DataTable dtserials = new DataTable();
                //    dtserials.Columns.AddRange(new DataColumn[1] { new DataColumn("No") });
                //    ViewState["SERIALTABLE"] = dtserials;
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

            return true;
        }
        private bool validateQuantity() {
            if ((string.IsNullOrEmpty(txtbarcode.Text.Trim())) && (string.IsNullOrEmpty(txtqty.Text.Trim())))
            {
                if (string.IsNullOrEmpty(txtqty.Text.Trim()))
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please enter qty or scan barcode !";
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
                    lblalert.Text = "0 qty is not allowed !";
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
                    lblalert.Text = "Minus values are not allowed !";
                    txtqty.Text = String.Empty;
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
                                lblalert.Text = "Not allowed decimal qty for this item !";
                                txtqty.Text = String.Empty;
                                txtqty.Focus();
                                //SetScrollTop();
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        private void updateItemQty() {
            try
            {
                string docNo = (string)Session["DOCNO"];
                string isseriaitem = (string)Session["SERIALIZED"];
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
               
            }
            catch (Exception ex) { 
            }
        }
        private void Clear(bool check=true)
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
                lblalert.Text = ex.Message.ToString();
            }
        }
        private void SaveData()
        {
            try
            {
               // txtitemcode_TextChanged(null, null);
                string locserialcheck = (string)Session["LOCISSERIAL"];
                string _scanDocument = (string)Session["DOCNO"];
                string DocumentType = (string)Session["DOCTYPE"];
                string doctp= Session["Doctype"].ToString();
                string _userSeqNo = (string)Session["SEQNO"].ToString();
                string docdirection = (string)Session["DOCDIRECTION"];
                string isseriaitem = (string)Session["SERIALIZED"];

                string warecom = (string)Session["WAREHOUSE_COMPDA"];
                string wareloc = (string)Session["WAREHOUSE_LOCPDA"];
                string loadingpoint = (string)Session["LOADING_POINT"];
                string iscurrent = Session["CurrentJobb"] as string;

                string company = Session["UserCompanyCode"].ToString();
                string location = Session["UserDefLoca"].ToString();
                string glberror = "";
                Int32 existseialno =0;
                string existsdocno = "";
                if (warecom == "" || wareloc == "" || loadingpoint == "")
                {
                    divalert.Visible = true;
                    lblalert.Text = "Invalid loading bay,Wherehouse location,company setup!";
                    return;
                }
                
                bool isvalid;
                DivsHide();
                if (Session["Doctype"].ToString() == "STJO")
                {
                    isvalid = true;
                }
                else
                {
                    isvalid = ValidateItems();
                }
                if (isvalid == false)
                {
                    return;
                }
                bool binValid = validateBinCode();
                if (binValid == false)
                {
                    return;
                }
                if (isseriaitem == "1") {
                    bool validserial= validateSerial();
                    if (validserial == false) {
                        return;
                    }
                }
                 string errormsg = string.Empty;
                if (doctp == "SRN" && txtitemcode.Text.ToString() != "")
                {
                    bool validateSerialinDo = CHNLSVC.Inventory.validateSRNSerialInDo(_scanDocument, txtitemcode.Text.ToString(), isseriaitem, txtserialnumber1.Text, out errormsg);
                    if (!validateSerialinDo)
                    {
                        divalert.Visible = true;
                        lblalert.Text = errormsg;
                        return;
                    }
                }
                if (isseriaitem != "1") {
                    bool validserial = validateQuantity();
                    if (validserial == false)
                    {
                        return;
                    }
                }
                string iscreatejob=Session["CreateJobNumber"] as string;
                string iscurrentjobs = Session["CurrentJobb"] as string;
                DataTable temppickitems = CHNLSVC.Inventory.GetTepItems(Convert.ToInt32(_userSeqNo), txtitemcode.Text.ToUpper().Trim().ToString(), ddlitmstatus.SelectedValue.ToString());
                if (iscurrentjobs== "CurrentJobb")
                {
                    string _userSeqNonew = (string)Session["SEQNO"].ToString();
                    if (temppickitems.Rows.Count > 0)
                    {
                        foreach (DataRow dtrw in temppickitems.Rows)
                        {
                            if (dtrw["TUI_PIC_ITM_CD"].ToString() == "0" || dtrw["TUI_PIC_ITM_CD"] == null)
                            {
                                if (doctp != "GRN")
                                {
                                    divalert.Visible = true;
                                    lblalert.Text = "Invalid base item line !";
                                    return;
                                }
                            }
                        }
                    }
                    else {
                        if (doctp == "AOD")
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Cannot exceed request item quantity or item status missmatch.";
                            return;
                        }
                    }
                }
                if (isseriaitem == "1")
                {
                    DataTable validateSerialExists = CHNLSVC.Inventory.chechIsAvailableSerial(txtitemcode.Text.ToUpper().Trim(), txtserialnumber1.Text.Trim());
                    if (validateSerialExists.Rows.Count > 0)
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Cannot scan already exists serials.";
                        return;
                    }
                }
                
                Int32 val = 0;
                Int32 valserial = 0;
                Int32 valitem = 0;
                Int32 result = 0;

                ReptPickHeader _inputReptPickHeader = new ReptPickHeader();
                ReptPickItems _items = new ReptPickItems();
                ReptPickItems _itemslines = new ReptPickItems();
                ReptPickSerials _inputReptPickSerials = new ReptPickSerials();
                Int32 existserialcount = 0;
                bool isinorout = true;
                string serialidforitem = CHNLSVC.Inventory.GetSerialID().ToString();

                Decimal qtyforscan = 1;
               // DataTable temppickitems1 = CHNLSVC.Inventory.GetTepItems(Convert.ToInt32(_userSeqNo), txtitemcode.Text.ToUpper().Trim().ToString(), ddlitmstatus.SelectedValue.ToString());
                if (isseriaitem != "1" && iscurrentjobs == "CurrentJobb")
                {

                    if (string.IsNullOrEmpty(txtbarcode.Text.Trim()))
                    {
                        qtyforscan = Convert.ToDecimal(txtqty.Text.Trim());
                        decimal picqty = 0;
                        decimal reqqty = 0;
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
                            if (doctp != "SRN" && doctp != "GRN" && doctp != "ADJ")
                            {
                                divalert.Visible = true;
                                txtqty.Text = "";
                                lblalert.Text = "Cannot exceed item request quantity !";
                                txtqty.Focus();
                                return;
                            }
                        }
                    }
                }
                if ((isseriaitem != "1" && iscreatejob == "CreateJobNumber") && (!string.IsNullOrEmpty(txtqty.Text)))
                {
                    qtyforscan = Convert.ToDecimal(txtqty.Text.Trim());
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
                if (txtbarcode.Text.ToString() == "" && txtqty.Text.ToString() == "" && txtserialnumber1.Text.ToString() == "")
                {
                    return;

                }
                //string inrdocno = "";
                //Int32 serialid = 0;
                //Int32 inritemline = 0;
                //Int32 inrbatchline = 0;
                //Int32 inrserialline = 0;
                //decimal inrunitcost = 0;

                
                if (docdirection == "0")
                {
                    isinorout = false;
                }

                if (isseriaitem != "1")
                {
                    serialidforitem = "0";
                }


                DataTable dtsericlavailable = CHNLSVC.Inventory.IsSavedSerialAvailable(txtitemcode.Text.ToUpper().Trim(), txtserialnumber1.Text.Trim(), warecom, wareloc, loadingpoint);
                if (dtsericlavailable.Rows.Count > 0)
                {
                    divalert.Visible = true;
                    lblalert.Text = "Serial has already scanned !";
                    txtserialnumber1.Text = String.Empty;
                    txtserialnumber1.Focus();
                    //SetScrollTop();
                    return;
                }
               
                #region Header

                DataTable dtdoccheck = CHNLSVC.Inventory.IsDocAvailableWithSeq(Session["UserCompanyName"].ToString(),Convert.ToInt32(_userSeqNo), Session["UserDefLoca"].ToString(), warecom, wareloc, loadingpoint);
                if (dtdoccheck.Rows.Count > 0)
                {
                    if (_scanDocument != dtdoccheck.Rows[0]["TUH_DOC_NO"].ToString())
                    {
                        Session["DOCNO"] = dtdoccheck.Rows[0]["TUH_DOC_NO"].ToString();
                        Session["DOCTYPE"] = dtdoccheck.Rows[0]["TUH_DOC_TP"].ToString();
                        _scanDocument = dtdoccheck.Rows[0]["TUH_DOC_NO"].ToString();
                    }
                    if (Session["UserDefLoca"].ToString() != dtdoccheck.Rows[0]["TUH_USR_LOC"].ToString())
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Document scan using invalid location.";
                        return;
                    }
                }

                if (iscurrent == "CurrentJobb" && dtdoccheck.Rows.Count == 0)
                {
                    divalert.Visible = true;
                    lblalert.Text = "Invalid document.(Invalid wharehouse locations)";
                    return;
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
                    _inputReptPickHeader.Tuh_doc_tp = Session["Doctype"].ToString();
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
                    //    lblalert.Text = "Error in processing !";
                    //    return;
                    //}
                }
                #endregion
                 Int32 rownumber = 0;
                Int32 rowvalline = 0;
                bool hadRec = false;
                #region Items
                    
                    string linenumber = "";

                    if (temppickitems.Rows.Count > 0)
                    {
                        if (doctp != "GRN")
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
                            foreach (DataRow dtrw in temppickitems.Rows)
                            {
                                if (dtrw["TUI_PIC_ITM_CD"].ToString() != "0" && ((dtrw["TUI_REQ_ITM_QTY"].ToString() != dtrw["TUI_PIC_ITM_QTY"].ToString()) || (dtrw["TUI_REQ_ITM_QTY"].ToString() == "0" && dtrw["TUI_PIC_ITM_QTY"].ToString() == "0")))
                                {
                                    linenumber = dtrw["tui_pic_itm_cd"].ToString();
                                    if (linenumber == "") linenumber = "0";
                                    hadRec = true;
                                    break;
                                }
                                else if (dtrw["TUI_PIC_ITM_CD"].ToString() != "0" && ((dtrw["TUI_REQ_ITM_QTY"].ToString() == dtrw["TUI_PIC_ITM_QTY"].ToString()) || (dtrw["TUI_REQ_ITM_QTY"].ToString() == "0" && dtrw["TUI_PIC_ITM_QTY"].ToString() == "0")))
                                {
                                    linenumber = dtrw["tui_pic_itm_cd"].ToString();
                                    if (linenumber == "") linenumber = "0";
                                    hadRec = true;
                                    break;
                                }
                                else
                                {
                                    if (dtrw["TUI_PIC_ITM_CD"].ToString() != "0")
                                    {
                                        linenumber = dtrw["tui_pic_itm_cd"].ToString();
                                        if (linenumber == "") linenumber = "0";
                                        hadRec = true;
                                        break;
                                    }
                                    else
                                    {
                                        linenumber = "0";
                                        break;
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        if (/*doctp == "SRN" ||*/ doctp == "GRN" || doctp == "ADJ")
                        {
                            DataTable othstusitem = CHNLSVC.Inventory.GetTepItemsInOtherStatus(Convert.ToInt32(_userSeqNo), txtitemcode.Text.ToUpper().Trim().ToString());
                            if (othstusitem.Rows.Count > 0)
                            {
                                 linenumber = othstusitem.Rows[0]["TUI_PIC_ITM_CD"].ToString();
                            }
                            else {
                                if (doctp != "GRN")
                                {
                                    linenumber = "1";
                                }
                                else {
                                    linenumber = "0";
                                }
                            }
                        }
                        else {
                            linenumber = "1";
                        }
                    }
                    if (doctp == "SRN")
                    {
                        string error = "";
                        DataTable srnLinenum = CHNLSVC.Inventory.getSRNItemLineno(Convert.ToInt32(_userSeqNo), txtitemcode.Text.ToUpper().Trim().ToString(), ddlitmstatus.SelectedValue,out error);
                        if (srnLinenum.Rows.Count>0)
                        {
                            linenumber = srnLinenum.Rows[0]["TUI_PIC_ITM_CD"].ToString();
                        }
                        else
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Invalid item or item quantity ecxeed.";
                            return;
                        }
                    }

                    _items.Tui_usrseq_no = Convert.ToInt32(_userSeqNo);
                    _items.Tui_req_itm_cd = txtitemcode.Text.ToUpper().Trim();
                    _items.Tui_req_itm_stus = ddlitmstatus.SelectedValue;
                    if (isseriaitem == "1")
                    {
                        qtyforscan = 1;
                    }
                    _items.Tui_pic_itm_qty = qtyforscan;
                    _items.Tui_pic_itm_cd = linenumber;//rownumber.ToString();
                    _items.Tui_pic_itm_stus = string.Empty;
                    _items.Tui_grn = string.Empty;
                    _items.Tui_batch = string.Empty;
                    _items.Tui_sup = string.Empty;
                    //if (isseriaitem != "1" && temppickitems.Rows.Count > 1 && iscurrentjobs == "CurrentJobb")
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
                    //valitem = CHNLSVC.Inventory.UpdatePickItemStockInOut(_items);

                   
                
                if (iscreatejob == "CreateJobNumber")
                {
                    DataTable dtrownum = CHNLSVC.Inventory.LoadCurrentRowNumber(Convert.ToInt32(_userSeqNo), warecom, wareloc, loadingpoint);
                    foreach (DataRow ddrrownum in dtrownum.Rows)
                    {
                        rownumber = Convert.ToInt32(ddrrownum["RowCount"].ToString())+1;
                    }
                    if (hadRec == true)
                    {
                        rownumber = Convert.ToInt32(linenumber);
                    }
                    linenumber = rownumber.ToString();

                    _itemslines.Tui_pic_itm_cd = rownumber.ToString();
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

                

                //if (Convert.ToInt32(valitem) == -1)
                //{
                //    divalert.Visible = true;
                //    lblalert.Text = "Error in processing !";
                //    return;
                //}

               

                #endregion

                #region Serials



                if (dtsericlavailable.Rows.Count == 0 || isseriaitem != "1")
                {
                    string iscurrentjob = Session["CurrentJobb"] as string;

                    if (iscurrentjob == "CurrentJobb")
                    {
                        if (temppickitems.Rows.Count > 0)
                        {
                            string ROWNO = temppickitems.Rows[0]["tui_pic_itm_cd"].ToString();
                            if (ROWNO == "") ROWNO = "1";

                            rownumber = Convert.ToInt32(ROWNO);
                        }
                    }
                   
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
                    //else
                    //{
                    //    linenumber = "1";
                    //}
                    DataTable tempdoctype = CHNLSVC.Inventory.GetTempPickDocTypes(Convert.ToInt32(docdirection));
                    int i = 0;
                    string remark = "";
                    foreach (DataRow droptype in tempdoctype.Rows)
                    {
                        if (tempdoctype.Rows[i]["tdt_tp"].ToString() == Session["Doctype"].ToString())
                        {
                            remark = tempdoctype.Rows[i]["tdt_main_tp"].ToString();
                        }
                        i++;
                    }

                    _inputReptPickSerials.Tus_usrseq_no = Convert.ToInt32(_userSeqNo);
                    _inputReptPickSerials.Tus_doc_no = _scanDocument;
                    _inputReptPickSerials.Tus_itm_line = Convert.ToInt32(linenumber);
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
                    _inputReptPickSerials.Tus_ser_id = Convert.ToInt32(serialidforitem);
                    _inputReptPickSerials.Tus_ser_remarks = remark;

                    
                    //_inputReptPickSerials.Tus_batch_line = CHNLSVC.Inventory.Get_Int_Ser()
                    

                    if (Session["Doctype"].ToString() == "AOD")
                    {
                        if (isseriaitem=="1")
                        {
                            string docno = (string)Session["DOCNO"];
                            //get exist serial no
                            DataTable existserial = CHNLSVC.Inventory.getExistingSerial(txtserialnumber1.Text.ToString(), txtitemcode.Text.ToString(), docno,ddlitmstatus.SelectedValue.ToString());
                            existserialcount = existserial.Rows.Count;
                            if (existserial.Rows.Count >0)
                            {
                                existseialno = Convert.ToInt32(existserial.Rows[0]["its_ser_id"].ToString());

                                _inputReptPickSerials.Tus_ser_id = Convert.ToInt32(existseialno);
                                _inputReptPickSerials.Tus_batch_line = Convert.ToInt32(existserial.Rows[0]["its_batch_line"]);
                                _inputReptPickSerials.Tus_ser_line = Convert.ToInt32(existserial.Rows[0]["its_ser_line"]);
                                _inputReptPickSerials.Tus_unit_cost = Convert.ToDecimal(existserial.Rows[0]["its_unit_cost"]);
                                _inputReptPickSerials.Tus_seq_no = Convert.ToInt32(existserial.Rows[0]["its_seq_no"].ToString());
                                _inputReptPickSerials.Tus_doc_dt = (existserial.Rows[0]["its_doc_dt"].ToString() != "") ? Convert.ToDateTime(existserial.Rows[0]["its_doc_dt"].ToString()) : DateTime.MinValue;
                                _inputReptPickSerials.Tus_warr_no = existserial.Rows[0]["its_warr_no"].ToString();
                                _inputReptPickSerials.Tus_warr_period = Convert.ToInt32(existserial.Rows[0]["its_warr_period"].ToString());
                                _inputReptPickSerials.Tus_orig_grncom = existserial.Rows[0]["its_orig_grncom"].ToString();
                                _inputReptPickSerials.Tus_orig_grnno = existserial.Rows[0]["its_orig_grnno"].ToString();
                                _inputReptPickSerials.Tus_orig_grndt = (existserial.Rows[0]["its_orig_grndt"].ToString() != "") ? Convert.ToDateTime(existserial.Rows[0]["its_orig_grndt"].ToString()) : DateTime.MinValue;
                                _inputReptPickSerials.Tus_orig_supp = existserial.Rows[0]["its_orig_supp"].ToString();
                                _inputReptPickSerials.Tus_exist_grncom = existserial.Rows[0]["its_exist_grncom"].ToString();
                                _inputReptPickSerials.Tus_exist_grnno = existserial.Rows[0]["its_exist_grnno"].ToString();
                                _inputReptPickSerials.Tus_exist_grndt = (existserial.Rows[0]["its_exist_grndt"].ToString() != "") ? Convert.ToDateTime(existserial.Rows[0]["its_exist_grndt"].ToString()) : DateTime.MinValue;
                                _inputReptPickSerials.Tus_exist_supp = existserial.Rows[0]["its_exist_supp"].ToString();
                                _inputReptPickSerials.Tus_ageloc = existserial.Rows[0]["its_ageloc"].ToString();
                                _inputReptPickSerials.Tus_ageloc_dt = (existserial.Rows[0]["its_ageloc_dt"].ToString() != "") ? Convert.ToDateTime(existserial.Rows[0]["its_ageloc_dt"].ToString()) : DateTime.MinValue;
                                existsdocno = existserial.Rows[0]["its_doc_no"].ToString();
                                // update reversed 1
                                //int seout = CHNLSVC.Inventory.UpdateExistingSerialRecived(existserial.Rows[0]["its_doc_no"].ToString(), Convert.ToInt32(existserial.Rows[0]["its_ser_id"]), 1);
                            }
                        }
                        else
                        {
                            _inputReptPickSerials.Tus_ser_id = 0;
                        }

                    }
                    else if (Session["Doctype"].ToString() == "SRN")
                    {
                        if (isseriaitem == "1")
                        {
                            DataTable existserial = CHNLSVC.Inventory.getDOSerialData(_scanDocument, txtitemcode.Text.ToString(), txtserialnumber1.Text);
                            existserialcount = existserial.Rows.Count;
                            if (existserial.Rows.Count > 0)
                            {
                                existseialno = Convert.ToInt32(existserial.Rows[0]["its_ser_id"].ToString());

                                _inputReptPickSerials.Tus_ser_id = Convert.ToInt32(existseialno);
                                _inputReptPickSerials.Tus_batch_line = Convert.ToInt32(existserial.Rows[0]["its_batch_line"]);
                                _inputReptPickSerials.Tus_ser_line = Convert.ToInt32(existserial.Rows[0]["its_ser_line"]);
                                _inputReptPickSerials.Tus_unit_cost = Convert.ToDecimal(existserial.Rows[0]["its_unit_cost"]);
                                _inputReptPickSerials.Tus_seq_no = Convert.ToInt32(existserial.Rows[0]["its_seq_no"].ToString());
                                _inputReptPickSerials.Tus_doc_dt = (existserial.Rows[0]["its_doc_dt"].ToString() != "") ? Convert.ToDateTime(existserial.Rows[0]["its_doc_dt"].ToString()) : DateTime.MinValue;
                                _inputReptPickSerials.Tus_warr_no = existserial.Rows[0]["its_warr_no"].ToString();
                                _inputReptPickSerials.Tus_warr_period = Convert.ToInt32(existserial.Rows[0]["its_warr_period"].ToString());
                                _inputReptPickSerials.Tus_orig_grncom = existserial.Rows[0]["its_orig_grncom"].ToString();
                                _inputReptPickSerials.Tus_orig_grnno = existserial.Rows[0]["its_orig_grnno"].ToString();
                                _inputReptPickSerials.Tus_orig_grndt = (existserial.Rows[0]["its_orig_grndt"].ToString() != "") ? Convert.ToDateTime(existserial.Rows[0]["its_orig_grndt"].ToString()) : DateTime.MinValue;
                                _inputReptPickSerials.Tus_orig_supp = existserial.Rows[0]["its_orig_supp"].ToString();
                                _inputReptPickSerials.Tus_exist_grncom = existserial.Rows[0]["its_exist_grncom"].ToString();
                                _inputReptPickSerials.Tus_exist_grnno = existserial.Rows[0]["its_exist_grnno"].ToString();
                                _inputReptPickSerials.Tus_exist_grndt = (existserial.Rows[0]["its_exist_grndt"].ToString() != "") ? Convert.ToDateTime(existserial.Rows[0]["its_exist_grndt"].ToString()) : DateTime.MinValue;
                                _inputReptPickSerials.Tus_exist_supp = existserial.Rows[0]["its_exist_supp"].ToString();
                                _inputReptPickSerials.Tus_ageloc = existserial.Rows[0]["its_ageloc"].ToString();
                                _inputReptPickSerials.Tus_ageloc_dt = (existserial.Rows[0]["its_ageloc_dt"].ToString() != "") ? Convert.ToDateTime(existserial.Rows[0]["its_ageloc_dt"].ToString()) : DateTime.MinValue;
                                existsdocno = existserial.Rows[0]["its_doc_no"].ToString();
                            }
                            else
                            {
                                divalert.Visible = true;
                                lblalert.Text = "Invalid serial or item.Not contain in Dilivery order.";
                                return; 
                            }
                        }
                    }
                    if (Session["Doctype"].ToString() == "GRN" || Session["Doctype"].ToString()== "ADJ")
                    {
                      _inputReptPickSerials.Tus_ser_id =  Convert.ToInt32(serialidforitem);
                    }
                   

                    _inputReptPickSerials.Tus_ser_1 =(isseriaitem == "1") ? txtserialnumber1.Text.Trim():"N/A";
                    _inputReptPickSerials.Tus_ser_2 = txtserialnumber2.Text.Trim();
                    _inputReptPickSerials.Tus_ser_3 = txtserialnum3.Text.Trim();
                    _inputReptPickSerials.Tus_bin = txtBinCode.Text.ToString();//ddlbincode.SelectedValue;
                    _inputReptPickSerials.Tus_cre_by = Session["UserID"].ToString();
                    _inputReptPickSerials.Tus_session_id = (string)Session["GlbUserSessionID"];
                    _inputReptPickSerials.Tus_base_doc_no = _scanDocument;
                    _inputReptPickSerials.Tus_base_itm_line = Convert.ToInt32(linenumber);
                    _inputReptPickSerials.Tus_new_status = ddlitmstatus.SelectedValue;
                    if (temppickitems.Rows.Count > 0 && temppickitems.Rows[0]["TUI_REQ_ITM_QTY"].ToString() != "0")
                    {
                        _inputReptPickSerials.Tus_new_itm_cd = txtitemcode.Text.ToString().Trim();
                    }
                    //if (isseriaitem != "1" && temppickitems.Rows.Count > 1 && iscurrentjob == "CurrentJobb")
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
                    //                _inputReptPickSerials.Tus_qty = Convert.ToDecimal(dtrw["TUI_REQ_ITM_QTY"].ToString()) - Convert.ToDecimal(dtrw["TUI_PIC_ITM_QTY"].ToString());
                    //                _inputReptPickSerials.Tus_base_itm_line = Convert.ToInt32(dtrw["tui_pic_itm_cd"].ToString());
                    //                tempScnQty = tempScnQty - _inputReptPickSerials.Tus_qty;//Convert.ToDecimal(dtrw["TUI_REQ_ITM_QTY"].ToString()) - Convert.ToDecimal(dtrw["TUI_PIC_ITM_QTY"].ToString());
                    //            }
                    //            else
                    //            {
                    //                _inputReptPickSerials.Tus_qty = tempScnQty;
                    //                _inputReptPickSerials.Tus_base_itm_line = Convert.ToInt32(dtrw["tui_pic_itm_cd"].ToString());
                    //                tempScnQty = tempScnQty - _inputReptPickSerials.Tus_qty;
                    //            }
                    //            DataTable dtDocDetails = CHNLSVC.Inventory.getCurrentDocumentDetails(Session["UserCompanyName"].ToString(), _scanDocument, Session["UserDefLoca"].ToString(), warecom, wareloc, loadingpoint);
                    //            if (dtDocDetails.Rows.Count > 0)
                    //            {
                    //                if (dtDocDetails.Rows[0]["TUH_IS_TAKE_RES"] != null && dtDocDetails.Rows[0]["TUH_IS_TAKE_RES"].ToString() == "1")
                    //                    _inputReptPickSerials.Tus_resqty = _inputReptPickSerials.Tus_qty;
                    //            }

                    //            _inputReptPickSerials.Tus_itm_line = Convert.ToInt32(dtrw["tui_pic_itm_cd"].ToString());
                    //            valserial = CHNLSVC.Inventory.SavePickedItemSerialsPDA(_inputReptPickSerials);
                    //        }
                    //    }
                    //    // reqqty < qtyforscan + picqty
                    //}
                    //else
                    //{
                    //    valserial = CHNLSVC.Inventory.SavePickedItemSerialsPDA(_inputReptPickSerials);
                    //}
                    //valserial = CHNLSVC.Inventory.SavePickedItemSerialsPDA(_inputReptPickSerials);

                    //if (Convert.ToInt32(valserial) == -1)
                    //{
                    //    divalert.Visible = true;
                    //    lblalert.Text = "Error in processing !";
                    //    return;
                    //}
                }
                Int32 eff = CHNLSVC.Inventory.saveStockInDetails(dtdoccheck, _inputReptPickHeader, isseriaitem, temppickitems, iscurrentjobs,
                    qtyforscan, _items, iscreatejob,dtsericlavailable, _itemslines,doctp,existsdocno,existseialno,_inputReptPickSerials ,company,location,
                    _scanDocument, warecom, wareloc, loadingpoint, existserialcount, out glberror);
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

                if (dt != null)
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
                    DataTable dtserials = new DataTable();
                    dtserials.Columns.AddRange(new DataColumn[1] { new DataColumn("No") });
                    ViewState["SERIALTABLE"] = dtserials;
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
                        //txtitemcode.Text = string.Empty;
                    txtserialnumber1.Focus();
                    //}
                    //else {
                    //    txtserialnumber1.Focus();
                    //}
                    
                }
                else
                {
                    divokjob.Visible = true;
                    lblokjob.Text = "Good Scan !";

                    //if (chkbulkitems.Checked == false)
                    //{
                        //txtitemcode.Text = string.Empty;
                    //}
                    txtqty.Text = string.Empty;
                    txtqty.Focus();
                    //if (!string.IsNullOrEmpty(txtbarcode.Text.Trim()))
                    //{
                    //    txtbarcode.Focus();
                    //}
                    //else
                    //{
                    //    if (chkbulkitems.Checked == false)
                    //    {
                    //        txtitemcode.Focus();
                    //    }
                    //    else {
                    //        txtqty.Focus();
                    //    }
                    //}
                }
                //LoadItemQty();
                loadScanedQty();
                getTotalDocQty();
                GetLastScanSerial();
                LoadGrid();
                updateItemQty();
                Clear();
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
                if (CheckBoxQR.Checked == true)
                {
                    int method = Convert.ToInt32(DropDownListQRCom.SelectedValue.ToString());
                    if (method == 0)
                    {
                        lblalert.Text = "Please select a company";
                        return;
                    }
                }
                else
                {
                    DataTable paraTB = CHNLSVC.Inventory.getMstSysPara(Session["UserCompanyCode"].ToString(), "COM", Session["UserCompanyCode"].ToString(), "SERLENRES", Session["UserCompanyCode"].ToString());
                    if (paraTB != null)
                    {

                        if (paraTB.Rows.Count > 0)
                        {
                            if (!(string.IsNullOrEmpty(paraTB.Rows[0]["MSP_REST_VAL"].ToString())))
                            {
                                int lenght = 0;
                                lenght = Convert.ToInt32(paraTB.Rows[0]["MSP_REST_VAL"].ToString());
                                if (txtserialnumber1.Text.Trim().Length > lenght)
                                {
                                    divalert.Visible = true;
                                    lblalert.Text = "Serial length is more than " + lenght + "Please use QR method";
                                    txtserialnumber1.Text = string.Empty;
                                    return;
                                }
                            }
                        }
                    }  
                }

                string val = txtserialnumber1.Text.ToString();
                Session["LastSerialNo"] = val;
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
                lblalert.Text = ex.Message.ToString();
            }
        }
        protected void btnitmcheck_Click(object sender, EventArgs e) {
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
                lblalert.Text = ex.Message.ToString();
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
                        lblalert.Text = "Please enter item code !";
                        txtitemcode.Focus();
                        //SetScrollTop();
                        return;
                    }
                    passitem = txtitemcode.Text.ToUpper().Trim();
                }

                if (chkallbin.Checked == false)
                {
                    //if (ddlbincode.SelectedIndex == 0)
                    //{
                    //    divalert.Visible = true;
                    //    lblalert.Text = "Please select bin code !";
                    //    ddlbincode.Focus();
                    //    //SetScrollTop();
                    //    return;
                    //}
                    //passbin = ddlbincode.SelectedValue;
                    if (txtBinCode.Text.ToString() == "")
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Please enter bin code !";
                        txtBinCode.Focus();
                        //SetScrollTop();
                        return;
                    }
                    passbin = txtBinCode.Text.ToString();

                }

                if (chkallstatus.Checked == false)
                {
                    if (ddlitmstatus.SelectedIndex == 0)
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Please select item status !";
                        ddlitmstatus.Focus();
                        //SetScrollTop();
                        return;
                    }
                    passitemstatus = ddlitmstatus.SelectedValue;
                }

                Session["CHECKBUTTON"] = "1";
                Response.Redirect("CheckScannedStock.aspx?Item="+passitem+"&Bin="+passbin+"&ItemStatus="+passitemstatus,false);
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = ex.Message.ToString();
            }
        }

        protected void chkbulkitems_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                DivsHide();
                txtitemcode.Focus();
                //if (chkbulkitems.Checked == true)
                //{
                //    txtitemcode.ReadOnly = true;
                //}
                //else
                //{
                //    txtitemcode.ReadOnly = false;
                //}
                //txtbarcode.Focus();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = ex.Message.ToString();
            }
        }

        protected void ddlbincode_SelectedIndexChanged(object sender, EventArgs e)
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
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = ex.Message.ToString();
            }
        }
        private void loadScanedQty() {
            try
            {
                Int32 seq = Convert.ToInt32(Session["SEQNO"].ToString());
                if (lbljobno.Text != "")
                {
                    DataTable dttotqty = CHNLSVC.Inventory.GetItemTotalScanedQtySeq(seq);
                    if (dttotqty.Rows.Count > 0)
                    {
                        foreach (DataRow ddrtotitem in dttotqty.Rows)
                        {
                            lblscqty.Text = (ddrtotitem["SEQ_QTY"].ToString() != "") ? ddrtotitem["SEQ_QTY"].ToString() : "0";
                        }
                    }
                }
                else {
                    lblalert.Text = "Invalid document number.";
                }
                string docNo= (string)Session["DOCNO"];
                Int32 binQty = 0;
                if (txtBinCode.Text != "")
                {
                    binQty = CHNLSVC.Inventory.getItemBinScanQtySeq(txtitemcode.Text.Trim().ToString(), txtBinCode.Text.ToString(), seq);
                }
                binScnQty.Text = binQty.ToString();
            }
            catch (Exception ex) {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text =ex.Message.ToString();
            }
        }

        //private void LoadItemQty()
        //{
        //    try
        //    {
        //        string seqnoforitm = (string)Session["SEQNO"].ToString();
        //        string warecom = (string)Session["WAREHOUSE_COMPDA"];
        //        string wareloc = (string)Session["WAREHOUSE_LOCPDA"];
        //        string loadingpoint = (string)Session["LOADING_POINT"];
        //        DataTable dttotqty = CHNLSVC.Inventory.GetItemTotalQty(Convert.ToInt32(seqnoforitm),warecom,wareloc,loadingpoint);

        //        if (dttotqty.Rows.Count > 0)
        //        {
        //            foreach (DataRow ddrtotitem in dttotqty.Rows)
        //            {
        //                lblscqty.Text = (ddrtotitem["TOTQTY"].ToString()!="")?ddrtotitem["TOTQTY"].ToString():"0"; 
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        divalert.Visible = true;
        //        lblalert.Text = ex.Message;
        //    }
        //    finally
        //    {
        //        CHNLSVC.CloseAllChannels();
        //    }
        //}

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
                lblalert.Text = ex.Message.ToString();
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
                //LoadItemQty();
                loadScanedQty();
                getTotalDocQty();
                Session["ITEMSTATUS"] = ddlitmstatus.SelectedValue;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = ex.Message.ToString();
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
                Session["LastSerialNo"] = null;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = ex.Message.ToString();
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
                    Int32 docseq = Convert.ToInt32(Session["SEQNO"].ToString());
                    string doctype = (string)Session["DOCTYPE"];
                    if (docno != "")
                    {
                        DateTime nowdate = DateTime.Now;
                        string error = string.Empty;
                        Int32 eff = CHNLSVC.Inventory.updateDocumentFinishStatusSeq(docseq, doctype, 1, nowdate, out error);
                        if (eff == 1 && error == "")
                        {
                            ViewState["SERIALTABLE"] = null;
                            DivsHide();
                            Clear();
                            SessionClear();
                            Response.Redirect("CurrentJobs.aspx?DocType=" + doctype, false);
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
                else
                {
                    divalert.Visible = true;
                    lblalert.Text = "Already finished document.";
                    return;
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                //lblalert.Text = ex.Message;
                lblalert.Text = ex.Message.ToString();
            }
        }

        protected void chkallitems_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    DivsHide();
            //    if (chkallitems.Checked == true)
            //    {
            //        txtitemcode.Text = string.Empty;
            //        txtitemcode.ReadOnly = true;
            //    }
            //    else
            //    {
            //        txtitemcode.ReadOnly = false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    divalert.Visible = true;
            //    lblalert.Text = ex.Message;
            //}
        }

        protected void chkallbin_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    DivsHide();
            //    if (chkallbin.Checked == true)
            //    {
            //        ddlbincode.Enabled = false;
            //    }
            //    else
            //    {
            //        ddlbincode.Enabled = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    divalert.Visible = true;
            //    lblalert.Text = ex.Message;
            //}
        }

        protected void chkallstatus_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    DivsHide();
            //    if (chkallstatus.Checked == true)
            //    {
            //        ddlitmstatus.Enabled = false;
            //    }
            //    else
            //    {
            //        ddlitmstatus.Enabled = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    divalert.Visible = true;
            //    lblalert.Text = ex.Message;
            //}
        }
        private void getTotalDocQty()
        {
            try
            {
                if ((String)Session["DOCNO"] != "")
                {
                    Int32 seqNo = Convert.ToInt32(Session["SEQNO"].ToString());
                    DataTable dtdoccount = CHNLSVC.Inventory.GetItemTotalDocumentQtySeq(seqNo);

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
                lblalert.Text = ex.Message.ToString();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        protected void LoadGrid()
        {
            string docno = Session["DOCNO"].ToString();
            Int32 seqNo = Convert.ToInt32(Session["SEQNO"].ToString());
            DataTable dtitems = CHNLSVC.Inventory.loadDocumentItemsSeq(seqNo);
            grdjobitems.DataSource = null;
            grdjobitems.DataBind();

            grdjobitems.DataSource = dtitems;
            grdjobitems.DataBind();
        }
        protected void txtBinCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtbincode = (DataTable)Session["dtbincode"];
                if (dtbincode.Rows.Count > 0)
                {
                    bool contains = dtbincode.AsEnumerable().Any(row => txtBinCode.Text.ToString() == row.Field<String>("IBN_BIN_CD"));
                    if (contains == false)
                    {
                        divalert.Visible = true;
                        //lblalert.Text = ex.Message;
                        txtBinCode.Text = "";
                        lblalert.Text = "Please enter valid bin.";
                        txtBinCode.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                txtBinCode.Text = "";
                lblalert.Text = "Please enter valid bin code";
                txtBinCode.Focus();
                return;
            }
        }
        private bool validateBinCode()
        {
            try
            {
                string docNo = (string)Session["DOCNO"];
                Int32 seqNo = Convert.ToInt32(Session["SEQNO"].ToString());
                if (txtBinCode.Text == "") {
                    divalert.Visible = true;
                    //lblalert.Text = ex.Message;
                    txtBinCode.Text = "";
                    lblalert.Text = "Please enter bin code.";
                    return false;
                }
                DataTable dtbincode = (DataTable)Session["dtbincode"];
                if (dtbincode.Rows.Count > 0)
                {
                    bool contains = dtbincode.AsEnumerable().Any(row => txtBinCode.Text.ToString() == row.Field<String>("IBN_BIN_CD"));
                    if (contains == false)
                    {
                        divalert.Visible = true;
                        //lblalert.Text = ex.Message;
                        txtBinCode.Text = "";
                        lblalert.Text = "Please enter valid bin.";
                        return false;
                    }
                }
                else {
                    divalert.Visible = true;
                    //lblalert.Text = ex.Message;
                    txtBinCode.Text = "";
                    lblalert.Text = "No bins for this location.";
                    return false;
                }
                Int32 binQty = 0;
                if (txtBinCode.Text != "")
                {
                    binQty = CHNLSVC.Inventory.getItemBinScanQtySeq(txtitemcode.Text.Trim().ToString(), txtBinCode.Text.ToString(), seqNo);
                }
                binScnQty.Text = binQty.ToString();
                return true;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message.ToString();
                return false;
            }
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
                string location=(string)Session["UserDefLoca"];
                lblalert.Text = "";
                lblokjob.Text = "";
                string sessionid = (string)Session["GlbUserSessionID"];
                string error = string.Empty;
                string userid = (string)Session["UserID"];
                ReptPickHeader tempHdr = new ReptPickHeader();
                tempHdr = CHNLSVC.Inventory.getTemporyHeaderDetails(docno, doctype, company, out error);
                
                if (tempHdr.Tuh_isdirect == true)
                {
                    if (doctype == "AOD")
                    {
                        doctype = "PDA";
                    }
                }
                if (doctype == "AOD" || doctype == "PDA")
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(company, userId, 16070))
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Sorry, You have no permission to genarate AOD In.( Advice: Required permission code : 16070) !";
                        return;
                    }
                    #region AOD Genarate
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
                            List<ReptPickItems> _reptPickItem = new List<ReptPickItems>();
                            _reptPickItem = CHNLSVC.Inventory.getScanedItems(tempHdr.Tuh_usrseq_no, out error);
                            if (error != "")
                            {
                                divalert.Visible = true;
                                lblalert.Text = error;
                                return;
                            }
                            else
                            {
                                if (_reptPickItem.Count > 0)
                                {
                                    InventoryHeader existsInvHdr = CHNLSVC.Inventory.getIntHdrData(tempHdr.Tuh_doc_no, location,company, out error);
                                    if (error != "")
                                    {
                                        divalert.Visible = true;
                                        lblalert.Text = error;
                                        return;
                                    }
                                    else
                                    {
                                        if (existsInvHdr.Ith_doc_no != null)
                                        {
                                            string issuecompany = existsInvHdr.Ith_com;
                                            string pendocno = existsInvHdr.Ith_doc_no;

                                            InventoryHeader invHdr = new InventoryHeader();
                                            invHdr.Ith_loc = location;
                                            invHdr.Ith_com = company;
                                            invHdr.Ith_oth_docno = pendocno;

                                           // invHdr.Ith_entry_no = pendocno;
                                            invHdr.Ith_doc_date = DateTime.Now.Date;
                                            invHdr.Ith_doc_year = DateTime.Now.Year;

                                            if (doctype == "AOD")
                                            {
                                                invHdr.Ith_doc_tp = "AOD";
                                                invHdr.Ith_cate_tp = "NOR";
                                                invHdr.Ith_sub_tp = "NOR";
                                            }
                                            invHdr.Ith_job_no = pendocno;
                                            PurchaseOrder _pohdr = CHNLSVC.Inventory.GetPurchaseOrderHeaderDetails(company, invHdr.Ith_oth_docno);
                                            PurchaseOrderDetail _poone = new PurchaseOrderDetail();
                                            List<PurchaseOrderDetail> _poLst = new List<PurchaseOrderDetail>();
                                            string _supplierclaimcode = string.Empty;

                                            invHdr.Ith_is_manual = false;
                                            invHdr.Ith_stus = "A";
                                            invHdr.Ith_cre_by = userId;
                                            invHdr.Ith_mod_by = userId;
                                            invHdr.Ith_direct = true;
                                            invHdr.Ith_session_id = sessionid;
                                            invHdr.Ith_manual_ref = "N/A";
                                            invHdr.Ith_remarks = "PDA GENARATE";
                                            invHdr.Ith_gen_frm = "PDA";
                                            invHdr.Ith_vehi_no = "";
                                            invHdr.Ith_bus_entity = doctype == "DO" ? _pohdr.Poh_supp : string.Empty;
                                            invHdr.Ith_oth_com = existsInvHdr.Ith_com;
                                            invHdr.Ith_oth_loc = existsInvHdr.Ith_loc;
                                            invHdr.Ith_entry_no = existsInvHdr.Ith_entry_no;
                                            invHdr.Ith_pc = Session["UserDefProf"].ToString();
                                            Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(doctype, company, pendocno, 1);
                                            List<ReptPickSerials> PickSerialsList = new List<ReptPickSerials>();
                                            PickSerialsList = _reptPickSerials;
                                            if (PickSerialsList == null || PickSerialsList.Count==0)
                                            {
                                                divalert.Visible = true;
                                                lblalert.Text = "Please select an outward document number!.";
                                                return;
                                            }


                                            int _updateDate = CHNLSVC.Inventory.ChangeScanSerialDocDate(invHdr.Ith_oth_com, invHdr.Ith_com, invHdr.Ith_doc_tp, pendocno, invHdr.Ith_doc_date.Date, userId);

                                            PickSerialsList.ForEach(x => x.Tus_doc_dt = invHdr.Ith_doc_date.Date);
                                            List<ReptPickSerialsSub> reptPickSerials_SubList = new List<ReptPickSerialsSub>();
                                            reptPickSerials_SubList = CHNLSVC.Inventory.GetAllScanSubSerialsList(user_seq_num, doctype);
                                            MasterAutoNumber masterAutoNum = new MasterAutoNumber();
                                            masterAutoNum.Aut_cate_cd = Session["UserDefLoca"].ToString(); masterAutoNum.Aut_cate_tp = "LOC"; masterAutoNum.Aut_direction = 1;
                                            masterAutoNum.Aut_modify_dt = null; masterAutoNum.Aut_year = DateTime.Now.Year;

                                            string documntNo = string.Empty;
                                            Int32 result = -99;
                                            bool _isok = IsUserProcessed(user_seq_num, pendocno);
                                            if (!_isok)
                                            {
                                                try
                                                {
                                                    #region Check receving serials are duplicating :: Chamal 08-May-2014
                                                    string _err = string.Empty;
                                                    if (CHNLSVC.Inventory.CheckDuplicateSerialFound(invHdr.Ith_com, invHdr.Ith_loc, PickSerialsList, out _err) <= 0)
                                                    {
                                                        divalert.Visible = true;
                                                        string msg2 = "These serial(s) are already available in your location !!!";
                                                        lblalert.Text = msg2;
                                                        return;
                                                    }
                                                    #endregion

                                                    if (doctype == "AOD")
                                                    {
                                                        masterAutoNum.Aut_moduleid = "AOD";
                                                        masterAutoNum.Aut_start_char = "AOD";
                                                        System.Threading.Thread.Sleep(1000);
                                                        #region cost calculate
                                                        // Add by Lakshan
                                                        MasterLocation _mstLoc = CHNLSVC.General.GetLocationInfor(existsInvHdr.Ith_loc.Trim().ToUpper(), existsInvHdr.Ith_loc.Trim().ToUpper());
                                                        
                                                        #endregion
                                                      
                                                        invHdr.Ith_acc_no = "SCMWEB";
                                                        int _usrSeq = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(doctype, company, invHdr.Ith_oth_docno, 1);
                                                        if (_usrSeq != -1)
                                                        {
                                                            ReptPickHeader _ReptPickHeader = CHNLSVC.Inventory.GetReportTempPickHdr(new ReptPickHeader()
                                                            {
                                                                Tuh_doc_no = invHdr.Ith_oth_docno,
                                                                Tuh_usr_com = company,
                                                                Tuh_doc_tp = doctype,
                                                                Tuh_isdirect = true,
                                                                Tuh_usrseq_no = _usrSeq,
                                                            }).FirstOrDefault();
                                                            if (_ReptPickHeader != null)
                                                            {
                                                                if (!string.IsNullOrEmpty(_ReptPickHeader.Tuh_wh_com) && !string.IsNullOrEmpty(_ReptPickHeader.Tuh_wh_loc) && !string.IsNullOrEmpty(_ReptPickHeader.Tuh_load_bay))
                                                                {
                                                                    invHdr.Ith_loading_point = _ReptPickHeader.Tuh_load_bay;
                                                                }
                                                            }
                                                        }
                                                        #region validate aod out com/location
                                                        InventoryHeader _invHdrOth = CHNLSVC.Inventory.GET_INT_HDR_DATA(new InventoryHeader()
                                                        {
                                                            Ith_doc_no = invHdr.Ith_oth_docno,
                                                            Ith_loc = invHdr.Ith_oth_loc,
                                                            Ith_com = invHdr.Ith_oth_com
                                                        }).FirstOrDefault();
                                                        if (_invHdrOth.Ith_oth_loc != Session["UserDefLoca"].ToString())
                                                        {
                                                            divalert.Visible = true;
                                                            lblalert.Text = "Please check the AOD IN location !.";
                                                            return;
                                                        }
                                                        if (_invHdrOth.Ith_oth_com != Session["UserCompanyCode"].ToString())
                                                        {
                                                            divalert.Visible = true;
                                                            lblalert.Text = "Please check the AOD IN Company !.";
                                                            return;
                                                        }
                                                        #endregion
                                                        #region Check Scan Completed
                                                        ReptPickHeader _tmpPickHdr = new ReptPickHeader();
                                                        _tmpPickHdr = CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                                                        {
                                                            Tuh_doc_no = invHdr.Ith_oth_docno,
                                                            Tuh_doc_tp = "AOD",
                                                            Tuh_direct = true,
                                                            Tuh_usr_com = company
                                                        }).FirstOrDefault();
                                                        //if (_tmpPickHdr != null)
                                                        //{
                                                        //    if (!string.IsNullOrEmpty(_tmpPickHdr.Tuh_wh_com) && !string.IsNullOrEmpty(_tmpPickHdr.Tuh_wh_loc) && !string.IsNullOrEmpty(_tmpPickHdr.Tuh_wh_com))
                                                        //    {
                                                        //        if (_tmpPickHdr.Tuh_fin_stus != 1)
                                                        //        {
                                                        //            divalert.Visible = true;
                                                        //            lblalert.Text = "Scanning is not completed for the selected document !.";
                                                        //            return;
                                                        //        }
                                                        //    }
                                                        //}
                                                        #endregion
                                                        #region validate aod in and aod out
                                                        List<InventoryBatchN> _aodBatchData = CHNLSVC.Inventory.Get_Int_Batch(invHdr.Ith_oth_docno);
                                                        if (_aodBatchData != null)
                                                        {
                                                            if (_aodBatchData.Count > 0)
                                                            {
                                                                foreach (var _serData in PickSerialsList)
                                                                {
                                                                    foreach (var _batchData in _aodBatchData)
                                                                    {
                                                                        if (_serData.Tus_itm_cd == _batchData.Inb_itm_cd && _serData.Tus_itm_stus == _batchData.Inb_itm_stus)
                                                                        {
                                                                            _serData.Tus_is_valid_ser = 1;
                                                                        }
                                                                    }
                                                                }
                                                                var _invalidSerList = PickSerialsList.Where(c => c.Tus_is_valid_ser == 0).ToList();
                                                                if (_invalidSerList != null)
                                                                {
                                                                    if (_invalidSerList.Count > 0)
                                                                    {
                                                                        divalert.Visible = true;
                                                                        lblalert.Text = "Selected item is different from the requested item !.";
                                                                        return;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        #endregion
                                                        invHdr.TMP_IS_RES_UPDATE = true;
                                                        invHdr.TMP_CHK_SER_IS_AVA = true;
                                                        invHdr.TMP_PROJECT_NAME = "SCMWEB";
                                                        result = CHNLSVC.Inventory.AODReceipt(invHdr, PickSerialsList, reptPickSerials_SubList, masterAutoNum, out documntNo, false);
                                                      
                                                        #region genarate mail
                                                       // CHNLSVC.MsgPortal.GenarateAodInwardMailAndSMS(Session["UserCompanyCode"].ToString(), documntNo);
                                                        #endregion
                                                        if (result != -99 && result > 0)
                                                        {
                                                            PickSerialsList.ForEach(x => x.Tus_com = issuecompany);
                                                            string _refdc = pendocno;
                                                            if (doctype != "AOD")
                                                            {
                                                                CHNLSVC.Inventory.SetOffRefDocumentSerial(PickSerialsList, _refdc);
                                                            }
                                                            btnPrint.Visible = true;
                                                            btnProcess.Visible = false;
                                                            Session["PrintDoc"] = documntNo;
                                                            string Msg = "AOD Receipt Successfully Saved! Document No. : " + documntNo + "";
                                                             divokjob.Visible = true;
                                                            lblokjob.Text = Msg;
                                                            if (Session["PrintDoc"] != null)
                                                            {
                                                                btnProcess.Visible = false;
                                                                btnPrint.Visible = true;
                                                            }
                                                            lblMssg.Text = "Do you want print now?";
                                                            PopupConfBox.Show();  

                                                        }
                                                        else
                                                        {
                                                            btnPrint.Visible = false;
                                                            btnProcess.Visible = true;
                                                            string Msg = "" + documntNo;
                                                            Msg = Msg.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
                                                            if (Msg.Contains("FK_INBBIN"))
                                                            {
                                                                divalert.Visible = true;
                                                                lblalert.Text = "Picked serial bin code is invalid !.";
                                                                return;
                                                            }
                                                            if (Msg.Contains("CHK_ITBBALQTY1"))
                                                            {
                                                                divalert.Visible = true;
                                                                lblalert.Text = "Please check the inventory balances!.";
                                                                return;
                                                            }
                                                            if (Msg.Contains("Aod out item quentity exceed"))
                                                            {
                                                                divalert.Visible = true;
                                                                lblalert.Text = "AOD Out item quantity cannot be exceeded !";
                                                                return;
                                                            }
                                                            CHNLSVC.CloseChannel();
                                                        }
                                                    }
                                                }catch(Exception ex){
                                                    divalert.Visible = true;
                                                    lblalert.Text = ex.Message.ToString();
                                                    return;
                                                }
                                                finally
                                                {
                                                    CHNLSVC.CloseAllChannels();
                                                }
                                            }
                                            else
                                            {
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            divalert.Visible = true;
                                            lblalert.Text = "Invalid out document number.";
                                            return;
                                        }
                                    }
                                    
                                }
                                else
                                {
                                    divalert.Visible = true;
                                    lblalert.Text = "Invalid scaned item details.";
                                    return;
                                }
                            }
                        }
                        else
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Please scan serial for genarate AOD Document.";
                            return;
                        }
                    }
                    #endregion AOD Genarate
                }
                else if (doctype == "GRN")
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(company, userId, 16070))
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Sorry, You have no permission to genarate GRN.( Advice: Required permission code : 16070) !";
                        return;
                    }
                    if (txtEntryNo.Text == "")
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Please enter entry number to process.";
                        return;
                    }
                    #region GRN Genarate
                    List<PurchaseOrderDelivery> PODeliveryList = new List<PurchaseOrderDelivery>();
                    bool _invalidLoc = false;
                    PODeliveryList = CHNLSVC.Inventory.GetConsignmentItemDetails(Session["UserCompanyCode"].ToString(), docno, null);
                    if (PODeliveryList != null)
                    {
                        if (PODeliveryList.Count > 0)
                        {
                            var _po = PODeliveryList.Where(c => c.Podi_loca != Session["UserDefLoca"].ToString()).ToList();
                            if (_po != null)
                            {
                                if (_po.Count > 0)
                                {
                                    _invalidLoc = true;
                                }
                            }
                        }
                    }
                    if (_invalidLoc)
                    {
                        lblerror.Text = "Invalid location find...!!! ";
                        lbldataMsg.Text = PODeliveryList.FirstOrDefault().Podi_loca;
                        lblmsg.Text = "Do you want to continue ?";
                        PoConfBox.Show();
                    }
                    else
                    {
                        Process(docno);
                    }

                    #endregion GRN Genarate
                }else if(doctype=="SRN")
                {
                    #region SRN Genarate
                     RequestApprovalHeader _ReqInsHdr = new RequestApprovalHeader();
                     List<RequestApprovalDetail> _ReqInsDet = new List<RequestApprovalDetail>();
                     if (!CHNLSVC.Security.Is_OptionPerimitted(company, userId, 16070))
                     {
                         divalert.Visible = true;
                         lblalert.Text = "Sorry, You have no permission to genarate SRN.( Advice: Required permission code : 16070) !";
                         return;
                     }

                     _ReqInsHdr = CHNLSVC.Inventory.getApprovedRequestDetails(docno, out error);
                     if (error == "")
                     {
                         if (_ReqInsHdr.Grah_ref != null)
                         {
                             _ReqInsDet = CHNLSVC.Inventory.getApprovedRequestItemDetails(_ReqInsHdr.Grah_ref, out error);
                             if (error == "")
                             {
                                 if (_ReqInsDet.Count > 0)
                                 {
                                     if (!CHNLSVC.Security.Is_OptionPerimitted(company, userId, 10110)) 
                                     {
                                         string _Msg = "Sorry, You have no permission for reverse!( Advice: Required permission code :10110)";
                                         divalert.Visible = true;
                                         lblalert.Text = _Msg;
                                         return;
                                     }
                                     InvoiceHeader invHdr = CHNLSVC.Inventory.GetInvoiceDetailForPdaSrn(_ReqInsHdr.Grah_fuc_cd, _ReqInsHdr.Grah_oth_pc, _ReqInsHdr.Grah_com,out error);

                                     if (error != "")
                                     {
                                         divalert.Visible = true;
                                         lblalert.Text = error;
                                         return;

                                     }
                                     if (string.IsNullOrEmpty(invHdr.Sah_cus_cd))
                                     {
                                         string _Msg = "Invoice customer is missing.";
                                         divalert.Visible = true;
                                         lblalert.Text = _Msg;
                                         return;
                                     }
                                     if (invHdr.Sah_stus != "D" && invHdr.Sah_stus != "A")
                                     {
                                         divalert.Visible = true;
                                         lblalert.Text = "Invalid invoice status.Status is : " + invHdr.Sah_stus;
                                         return;
                                     }
                                     if (_ReqInsHdr.Grah_app_stus != "A")
                                     {
                                         divalert.Visible = true;
                                         lblalert.Text = "Please approve reversal request before reverse the invoice.Request no : " + _ReqInsHdr.Grah_ref;
                                         return;
                                     }
                                     /*List<ReptPickSerials> _reptPickSerials = new List<ReptPickSerials>();
                                     _reptPickSerials = CHNLSVC.Inventory.getScanedSerials(tempHdr.Tuh_usrseq_no, out error);
                                     if (error != "")
                                     {
                                         divalert.Visible = true;
                                         lblalert.Text = error;
                                         return;

                                     }
                                     if (_reptPickSerials.Count == 0)
                                     {
                                         divalert.Visible = true;
                                         lblalert.Text = "Please pick serial before reverse invoice";
                                         return;
                                     }
                                     List<ReptPickSerials> _doitemserials = _reptPickSerials;
                                     List<RequestAppAddDet>  _repAddDet = ViewState["_repAddDet"] as List<RequestAppAddDet>;
                                     List<RequestApprovalSerials>  _ReqAppSer = ViewState["_ReqAppSer"] as List<RequestApprovalSerials>;*/
                                     Load_InvoiceDetails(invHdr.Sah_pc, docno, _ReqInsHdr.Grah_ref, company, location, userid, sessionid);
                                     string pc = invHdr.Sah_pc;
                                     List<ReptPickSerials> _doitemserials = ViewState["RevsFilterSerial"] as List<ReptPickSerials>;
                                     List<RequestAppAddDet>  _repAddDet = ViewState["_repAddDet"] as List<RequestAppAddDet>;
                                     List<RequestApprovalSerials>  _ReqAppSer = ViewState["_ReqAppSer"] as List<RequestApprovalSerials>;

                                     List<RequestApprovalHeader> _pendingRequest = CHNLSVC.General.GetPendingSRNRequest(company, pc, docno, "ARQT014");
                                     if (_pendingRequest != null && _pendingRequest.Count > 0)
                                     {
                                         
                                         List<RequestApprovalHeader> _pen = (from _res in _pendingRequest
                                                                             where _res.Grah_app_stus == "P"
                                                                             select _res).ToList<RequestApprovalHeader>();
                                         if (_pen != null && _pen.Count > 0)
                                         {
                                             string _Msg = "This invoice has pending request, Please approve pending request";
                                             divalert.Visible = true;
                                             lblalert.Text = _Msg;
                                             return;

                                         }
                                     }

                                     List<InvoiceItem> _InvDetailList = ViewState["_InvDetailList"] as List<InvoiceItem>;
                                     List<InvoiceItem> _temp = new List<InvoiceItem>();
                                     InvoiceItem _orgInvDet = new InvoiceItem();
                                     _temp = _InvDetailList;
                                     string _revItem = "";
                                     string _delItem = "";
                                     Int32 _line = 0;
                                     decimal _rtnQty = 0;
                                     decimal _invQty = 0;
                                     decimal _doQty = 0;
                                     decimal _curRtnQty = 0;

                                     foreach (InvoiceItem itm in _temp)
                                     {
                                         if (!string.IsNullOrEmpty(itm.Sad_sim_itm_cd))
                                         {
                                             _delItem = itm.Sad_sim_itm_cd;
                                         }
                                         else
                                         {
                                             _delItem = itm.Sad_itm_cd;
                                         }
                                         _line = itm.Sad_itm_line;
                                         _rtnQty = itm.Sad_srn_qty;
                                         _revItem = itm.Sad_itm_cd;

                                         _orgInvDet = CHNLSVC.Sales.GetInvDetByLine(itm.Sad_inv_no, _revItem, _line);

                                         if (_orgInvDet.Sad_inv_no == null)
                                         {
                                             string _Msg = "Cannot load item details in invoice. " + _revItem;
                                             return;
                                         }

                                         _invQty = _orgInvDet.Sad_qty;
                                         _doQty = _orgInvDet.Sad_do_qty;
                                         _curRtnQty = _orgInvDet.Sad_srn_qty;

                                         if (_invQty < _rtnQty)
                                         {
                                             string _Msg = "You are going to revers more then invoice qty.Item : " + _revItem + "Inv. Qty : " + _invQty + "Rtn. Qty : " + _rtnQty;
                                             divalert.Visible = true;
                                             lblalert.Text = _Msg;
                                             return;
                                         }

                                         if ((_invQty - _curRtnQty) < _rtnQty)
                                         {
                                             string _Msg = "You are going to revers more then current available qty.Item : " + _revItem + "Inv. Qty : " + _invQty + "Current Rtn. Qty : " + _rtnQty + "Already Rtn. Qty : " + _curRtnQty;
                                             divalert.Visible = true;
                                             lblalert.Text = _Msg;
                                             return;
                                         }

                                         MasterItem _itmDet = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _revItem);
                                         _ReqAppSer = new List<RequestApprovalSerials>();
                                         DataTable _rtnItmtype = new DataTable();
                                         _rtnItmtype = CHNLSVC.Sales.GetItemTp(_itmDet.Mi_itm_tp);
                                         List<RequestApprovalSerials> _tmpRepSer = new List<RequestApprovalSerials>();
                                         DataTable _tbl = CHNLSVC.General.Get_gen_reqapp_ser(company, _ReqInsHdr.Grah_ref, out _tmpRepSer);
                                         var _filter = _tmpRepSer.Where(x => x.Gras_anal5 != "").ToList();
                                         if (_filter.Count > 0)
                                         {
                                             ViewState["_repSer"] = _filter;
                                         }
                                         
                                         if (_doitemserials == null)
                                         {
                                             _doitemserials = new List<ReptPickSerials>();
                                         }
                                         if (_doitemserials.Count > 0 && _doitemserials != null)
                                         {
                                             List<RequestApprovalSerials>  _repSer = ViewState["_repSer"] as List<RequestApprovalSerials>;
                                             if (_repSer == null)
                                             {
                                                 _repSer = new List<RequestApprovalSerials>();
                                             }
                                             Int32 _line1 = 0;
                                             foreach (ReptPickSerials ser in _doitemserials)
                                             {
                                                 _line1 = _line1 + 1;
                                                 RequestApprovalSerials _tempReqAppSer = new RequestApprovalSerials();
                                                 _tempReqAppSer.Gras_ref = null;
                                                 _tempReqAppSer.Gras_line = _line1;
                                                 _tempReqAppSer.Gras_anal1 = ser.Tus_doc_no;
                                                 _tempReqAppSer.Gras_anal2 = ser.Tus_itm_cd;
                                                 _tempReqAppSer.Gras_anal3 = ser.Tus_ser_1;
                                                 _tempReqAppSer.Gras_anal4 = ser.Tus_ser_2;
                                                 _tempReqAppSer.Gras_anal5 = "";
                                                 if (_repSer.Count > 0)
                                                 {
                                                     foreach (RequestApprovalSerials _tmp in _repSer)
                                                     {
                                                         if (_tmp.Gras_anal2 == ser.Tus_itm_cd && _tmp.Gras_anal3 == ser.Tus_ser_1)
                                                         {
                                                             _tempReqAppSer.Gras_anal5 = _tmp.Gras_anal5;

                                                         }
                                                     }
                                                 }
                                                 _tempReqAppSer.Gras_anal6 = ser.Tus_ser_id;
                                                 _tempReqAppSer.Gras_anal7 = ser.Tus_base_itm_line;
                                                 _tempReqAppSer.Gras_anal8 = 0;
                                                 _tempReqAppSer.Gras_anal9 = 0;
                                                 _tempReqAppSer.Gras_anal10 = 0;

                                                 _ReqAppSer.Add(_tempReqAppSer);
                                             }
                                         }



                                         if (_rtnItmtype.Rows.Count > 0 && _rtnItmtype != null)
                                         {
                                             if (Convert.ToInt16(_rtnItmtype.Rows[0]["mstp_is_inv"]) == 1)
                                             {
                                                 if (_ReqAppSer.Count > 0)
                                                 {
                                                     decimal _serCount = _ReqAppSer.Where(X => X.Gras_anal2 == _delItem && X.Gras_anal7 == _line).Count();

                                                     if (_doQty < _serCount)
                                                     {
                                                         string _Msg = "You are going to revers more than deliverd qty.Item : " + _revItem + "Del. Qty : " + _doQty + "Serial : " + _serCount;
                                                         divalert.Visible = true;
                                                         lblalert.Text = _Msg;
                                                         return;
                                                     }

                                                     //imagin previous srns are having
                                                     if (_rtnQty != _serCount)
                                                     {
                                                         if (_invQty - _doQty == 0)
                                                         {
                                                             if (_rtnQty != _serCount)
                                                             {
                                                                 string _Msg = "Mismatch found : Only " + _serCount + " Serials picked for " + _rtnQty + " units going to return for item : " + _delItem;
                                                                 divalert.Visible = true;
                                                                 lblalert.Text = _Msg;
                                                                 return;
                                                             }

                                                             if (_rtnQty < _serCount)
                                                             {
                                                                 string _Msg = "Mismatch found : Only " + _serCount + " Serials picked for " + _rtnQty + " units going to return for item : " + _delItem;
                                                                 divalert.Visible = true;
                                                                 lblalert.Text = _Msg;
                                                                 return;
                                                             }
                                                         }
                                                     }
                                                 }
                                             }
                                         }
                                     }

                                     save_reverse(_InvDetailList,_ReqInsHdr,invHdr,tempHdr);
                                 }
                                 else
                                 {
                                     divalert.Visible = true;
                                     lblalert.Text = "No request item found for invoice reversal approved request.";
                                     return;
                                 }
                             }
                             else
                             {
                                 divalert.Visible = true;
                                 lblalert.Text = error;
                                 return;
                             }
                         }
                         else
                         {
                             divalert.Visible = true;
                             lblalert.Text = "Can't find approved SRN request for invoice.";
                             return;
                         }
                     }
                     else
                     {
                         divalert.Visible = true;
                         lblalert.Text = error;
                         return ;
                     }


                    #endregion SRN Genarate
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message.ToString();
                //lblalert.Text = "Server connection error.";
            }
        }

        private bool Load_InvoiceDetails(string _pc,string invNo,string reqNo,string company,string retLoc,string userid,string sessionid)
        {

            try
            {
                decimal _unitAmt = 0;
                decimal _disAmt = 0;
                decimal _taxAmt = 0;
                decimal _totAmt = 0;
                string _type = "";
                List<InvoiceItem> _paramInvoiceItems = new List<InvoiceItem>();
                List<InvoiceItem> _InvList = new List<InvoiceItem>();
                List<ReptPickSerials> _tempDOSerials = new List<ReptPickSerials>();
                List<VehicalRegistration> _tempReg = new List<VehicalRegistration>();
                List<ReptPickSerials>  _doitemserials = new List<ReptPickSerials>();
                List<VehicalRegistration>  _regDetails = new List<VehicalRegistration>();
                List<InvoiceItem> _InvDetailList = new List<InvoiceItem>();
                _type = null;


                _paramInvoiceItems = CHNLSVC.Sales.GetRevDetailsFromRequest(invNo, _type, company, _pc, reqNo);
                
                if (_paramInvoiceItems.Count > 0)
                {
                    List<InvoiceItem> _BACKITEM = new List<InvoiceItem>();
                    _BACKITEM = CHNLSVC.Sales.GetInvoiceItems(invNo);
                    foreach (InvoiceItem item in _paramInvoiceItems)
                    {
                        decimal qty = _BACKITEM.Find(x => x.Sad_itm_line == item.Sad_itm_line).Sad_qty;
                        //_unitAmt = (Convert.ToDecimal(item.Sad_unit_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(item.Sad_srn_qty));
                        //_disAmt = (Convert.ToDecimal(item.Sad_disc_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(item.Sad_srn_qty));
                        //_taxAmt = (Convert.ToDecimal(item.Sad_itm_tax_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(item.Sad_srn_qty));
                        //_totAmt = (Convert.ToDecimal(item.Sad_tot_amt) / Convert.ToDecimal(item.Sad_qty) * Convert.ToDecimal(item.Sad_srn_qty));
                        _unitAmt = (Convert.ToDecimal(item.Sad_unit_amt) / Convert.ToDecimal(qty) * Convert.ToDecimal(item.Sad_srn_qty));
                        _disAmt = (Convert.ToDecimal(item.Sad_disc_amt) / Convert.ToDecimal(qty) * Convert.ToDecimal(item.Sad_srn_qty));
                        _taxAmt = (Convert.ToDecimal(item.Sad_itm_tax_amt) / Convert.ToDecimal(qty) * Convert.ToDecimal(item.Sad_srn_qty));
                        _totAmt = (Convert.ToDecimal(item.Sad_tot_amt) / Convert.ToDecimal(qty) * Convert.ToDecimal(item.Sad_srn_qty));

                        item.Sad_unit_amt = Convert.ToDecimal(_unitAmt);
                        item.Sad_disc_amt = Convert.ToDecimal(_disAmt);
                        item.Sad_itm_tax_amt = Convert.ToDecimal(_taxAmt);
                        item.Sad_tot_amt = Convert.ToDecimal(_totAmt);
                        item.Cus_ITM_QTY = qty;

                        _InvList.Add(item);


                        List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                        oItemStaus = CHNLSVC.General.GetAllStockTypes(company);
                        if (oItemStaus != null && oItemStaus.Count > 0)
                        {
                            item.Sad_itm_stus_desc = oItemStaus.Find(x => x.Mis_cd == item.Sad_itm_stus).Mis_desc;
                        }

                    }
                     DataTable dtbincode = (DataTable)Session["dtbincode"];
                     string defcode = "";
                     if (dtbincode.Rows.Count > 0)
                     {
                         DataRow[] result = dtbincode.Select("IBN_IS_DEF = 1");
                          defcode = result[0][0].ToString();
                     }
                    _tempDOSerials = CHNLSVC.Inventory.GetRevReqSerial(company, retLoc, userid, sessionid,defcode, invNo, reqNo);
                    _doitemserials.AddRange(_tempDOSerials);


                }
                else
                {

                    string _Msg = "Cannot find details for the sales reversal";
                    divalert.Visible = true;
                    lblalert.Text = _Msg;
                    return false;

                }

                _InvDetailList = _InvList;
                var newList = _InvDetailList.OrderBy(x => x.Sad_itm_line).ToList();
                MasterItem _item = new MasterItem();
                foreach (var item in newList)
                {

                    _item = new MasterItem();
                    _item = CHNLSVC.Inventory.GetItem(company, item.Sad_itm_cd);
                    if (_item != null)
                    {
                        if (_item.Mi_model != null)
                        {
                            newList.Where(c => c.Sad_itm_cd == _item.Mi_cd).FirstOrDefault().Mi_model = _item.Mi_model;
                        }
                    }
                }
                ViewState["_InvDetailList"] = _InvDetailList;
                if (_doitemserials != null)
                {
                    int i = 1;
                    foreach (ReptPickSerials _Itm in _doitemserials)
                    {
                        _Itm.Tus_temp_itm_line = i;
                        List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                        oItemStaus = CHNLSVC.General.GetAllStockTypes(company);
                        if (oItemStaus != null && oItemStaus.Count > 0)
                        {
                            _Itm.Tus_itm_stus_Desc = oItemStaus.Find(x => x.Mis_cd == _Itm.Tus_itm_stus).Mis_desc;
                            if ((_Itm.Tus_appstatus != "") && (_Itm.Tus_appstatus != null))
                            {
                                _Itm.Tus_new_status_Desc = oItemStaus.Find(x => x.Mis_cd == _Itm.Tus_appstatus).Mis_desc;
                            }
                        }
                        i++;
                    }



                    Session["gvSerData"] = _doitemserials;
                     ViewState["RevsFilterSerial"] = _doitemserials;

                    ViewState["_olddoitemserials"] = _doitemserials;
                    ViewState["_doitemserials"] = _doitemserials;
                    ViewState["_doitemserials_backup"] = _doitemserials;

                }
                return true;
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        public void btnOkGrn_Click(object sender, EventArgs e)
        {
            string docno = (string)Session["DOCNO"];
            Process(docno);
        }
        public void btnNoGrn_Click(object sender, EventArgs e)
        {

        }
        public void Process(string docno)
        {
            #region start grn process
            ImpCusdecHdr _cusHdr = new ImpCusdecHdr();
            _cusHdr = CHNLSVC.Financial.GET_CUSTDEC_HDR_BY_DOC(docno);
            string entryNo = "";
            DateTime date = DateTime.Now.Date;
            string supplier = "";
            string supDesc = "";
            string refNo = "";
            DateTime clrDate = DateTime.MinValue;
            DateTime poDt = DateTime.MinValue;
            DateTime docDate = DateTime.Now;
            string rmark = "";
            string company=Session["UserCompanyCode"].ToString();
            string locserialcheck = (string)Session["LOCISSERIAL"];
            if (_cusHdr != null)
            {
                if (_cusHdr.CUH_CUSDEC_ENTRY_NO != null)
                {
                    entryNo  = _cusHdr.CUH_CUSDEC_ENTRY_NO;
                }
            }
            entryNo = txtEntryNo.Text.Trim();
            PurchaseOrder poDet = CHNLSVC.Inventory.GetPurchaseOrderHeaderDetails(company, docno);
            if (poDet.Poh_doc_no != null)
            {
                supplier = poDet.Poh_supp;
                refNo = poDet.Poh_ref;
                clrDate = DateTime.Now.Date;
                poDt = poDet.Poh_dt;
                rmark = poDet.Poh_remarks;

                ImportsCostHeader _cstHdr = CHNLSVC.Financial.GetImpCstHdrForGrn(docno.Trim().ToUpper());
                if (_cstHdr != null)
                {
                    if (_cstHdr.Ich_actl != 1)
                    {
                        string msg = "Please check the cost data.";
                        divalert.Visible = true;
                        lblalert.Text = msg;
                        return;
                    }
                }
                if (string.IsNullOrEmpty(refNo))
                {
                    string msg = "Please enter po reference number.";
                    divalert.Visible = true;
                    lblalert.Text = msg;
                    return;
                }
                if (string.IsNullOrEmpty(entryNo))
                {
                    string msg = "Please enter entry number.";
                    divalert.Visible = true;
                    lblalert.Text = msg;
                    return;
                }
                List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                InventoryHeader invHdr = new InventoryHeader();
                string documntNo = "";
                Int32 result = -99;

                Int32 _userSeqNo = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("GRN", company, docno, 1);
                reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(company, Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "GRN");

                if (reptPickSerialsList == null)
                {
                    string msg = "No scanned items found.";
                    divalert.Visible = true;
                    lblalert.Text = msg;
                    return;
                }
                List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "GRN");
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
                    string msg = "Following item serials are duplicating. Please remove the duplicated serials.";
                    divalert.Visible = true;
                    lblalert.Text = msg;
                    return;
                }
                #endregion Check Duplicate Serials

                reptPickSerialsList.ForEach(i => { i.Tus_exist_grncom = company; i.Tus_exist_grndt = poDt.Date; i.Tus_exist_supp = supplier; i.Tus_orig_grncom = company; i.Tus_orig_grndt = poDt.Date; i.Tus_orig_supp = supplier; });

                List<PurchaseOrderDelivery> _purchaseOrderDeliveryList = new List<PurchaseOrderDelivery>();
                _purchaseOrderDeliveryList = CHNLSVC.Inventory.GetConsignmentItemDetails(company, docno, Session["UserDefLoca"].ToString());
                List<PurchaseOrderDelivery> _purchaseOrderDeliveryList_2 = new List<PurchaseOrderDelivery>();

                InventoryHeader _invHeader = new InventoryHeader();

                DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                foreach (DataRow r in dt_location.Rows)
                {
                    //// Get the value of the wanted column and cast it to string
                    _invHeader.Ith_sbu = (string)r["ML_OPE_CD"];
                    if (System.DBNull.Value != r["ML_CATE_2"]) _invHeader.Ith_channel = (string)r["ML_CATE_2"]; else _invHeader.Ith_channel = string.Empty;
                }

                _invHeader.Ith_com = Session["UserCompanyCode"].ToString();
                _invHeader.Ith_loc = Session["UserDefLoca"].ToString();
                _invHeader.Ith_doc_date = docDate.Date;
                _invHeader.Ith_doc_year = docDate.Date.Year;
                _invHeader.Ith_direct = true;
                _invHeader.Ith_doc_tp = "GRN";
               
                    _invHeader.Ith_entry_no = entryNo;
                    _invHeader.Ith_git_close_date = clrDate.Date; //Added by Chamal 08-Sep-2016
                    _invHeader.Ith_git_close_doc = _invHeader.Ith_entry_no;
                
                if (poDet.Poh_tp == "I")
                {
                    _invHeader.Ith_cate_tp = "IMPORTS";
                    _invHeader.Ith_sub_tp = "IMPORTS";
                }
                else
                {
                    _invHeader.Ith_cate_tp = "LOCAL";
                    _invHeader.Ith_sub_tp = "LOCAL";
                }
                _invHeader.Ith_bus_entity = supplier;
                if (string.IsNullOrEmpty(refNo))
                { _invHeader.Ith_is_manual = false; }
                else { _invHeader.Ith_is_manual = true; }
                // if (chkManualRef.Checked == true) _invHeader.Ith_is_manual = true; else _invHeader.Ith_is_manual = false;
                _invHeader.Ith_manual_ref = refNo;
                _invHeader.Ith_remarks = rmark;
                _invHeader.Ith_stus = "A";
                _invHeader.Ith_cre_by = Session["UserID"].ToString();
                _invHeader.Ith_cre_when = DateTime.Now;
                _invHeader.Ith_mod_by = Session["UserID"].ToString();
                _invHeader.Ith_mod_when = DateTime.Now;
                _invHeader.Ith_session_id = Session["GlbUserSessionID"].ToString();
                _invHeader.Ith_oth_docno =docno;
                _invHeader.Ith_anal_10 = true;
                _invHeader.Ith_anal_2 = string.Empty;
                
                MasterAutoNumber _masterAuto = new MasterAutoNumber();
                _masterAuto.Aut_cate_cd = Session["UserDefLoca"].ToString(); ;
                _masterAuto.Aut_cate_tp = "LOC";
                _masterAuto.Aut_direction = null;
                _masterAuto.Aut_modify_dt = null;
                _masterAuto.Aut_moduleid = "GRN";
                _masterAuto.Aut_number = 0;
                _masterAuto.Aut_start_char = "GRN";
                _masterAuto.Aut_year = _invHeader.Ith_doc_date.Date.Year;

                //Add by Chamal 23-May-2014
                int _updateDate = CHNLSVC.Inventory.ChangeScanSerialDocDate(_invHeader.Ith_oth_com, _invHeader.Ith_com, _invHeader.Ith_doc_tp, docno, _invHeader.Ith_doc_date.Date, Session["UserID"].ToString());
                reptPickSerialsList.ForEach(x => x.Tus_doc_dt = _invHeader.Ith_doc_date.Date);
                if (_invHeader.Ith_doc_tp == "GRN")
                {
                    if (_invHeader.Ith_oth_com == "ABL" && _invHeader.Ith_com == "LRP")
                    {
                        reptPickSerialsList.ForEach(x => x.Tus_orig_grndt = _invHeader.Ith_doc_date.Date);
                        reptPickSerialsList.ForEach(x => x.Tus_exist_grndt = _invHeader.Ith_doc_date.Date);
                    }
                    if (_invHeader.Ith_oth_com == "SGL" && _invHeader.Ith_com == "SGD")
                    {
                        reptPickSerialsList.ForEach(x => x.Tus_orig_grndt = _invHeader.Ith_doc_date.Date);
                        reptPickSerialsList.ForEach(x => x.Tus_exist_grndt = _invHeader.Ith_doc_date.Date);
                    }
                }

                _invHeader.TMP_IS_ALLOCATION = false;

                if (locserialcheck=="0")
                    {
                        foreach (ReptPickSerials item in reptPickSerialsList)
                        {
                            item.Tus_ser_id = 0;
                        }
                        //reptPickSerialsList = new List<ReptPickSerials>();
                    }

                    string ib_doc_no = string.Empty;
                    string ib_bl_no = string.Empty;
                    DateTime? ib_bl_dt = null;
                    string ibi_fin_no = string.Empty;
                    string if_doc_no = string.Empty;
                    string if_file_no = string.Empty;
                    DateTime? if_doc_dt = null;
                    string To_bond = string.Empty;
                    DataTable dtbldata = CHNLSVC.Inventory.LoadBLData(docno.Trim());

                    if (dtbldata.Rows.Count > 0)
                    {
                        foreach (DataRow ddr in dtbldata.Rows)
                        {
                            ib_doc_no = ddr["ib_doc_no"].ToString();
                            ib_bl_no = ddr["ib_bl_no"].ToString();

                            if (!string.IsNullOrEmpty(ddr["ib_bl_dt"].ToString()))
                            {
                                ib_bl_dt = Convert.ToDateTime(ddr["ib_bl_dt"].ToString());
                            }
                            if (!string.IsNullOrEmpty(ddr["ib_ref_no"].ToString()))
                            {
                                To_bond = ddr["ib_ref_no"].ToString();
                            }
                        }
                    }

                    DataTable dtfinnum = CHNLSVC.Inventory.LoadFinDataNumber(ib_doc_no);
                    if (dtfinnum.Rows.Count > 0)
                    {
                        foreach (DataRow ddr1 in dtfinnum.Rows)
                        {
                            ibi_fin_no = ddr1["ibi_fin_no"].ToString();
                        }
                    }

                    DataTable dtfindata = CHNLSVC.Inventory.LoadFinData(ibi_fin_no);

                    if (dtfindata.Rows.Count > 0)
                    {
                        foreach (DataRow ddr2 in dtfindata.Rows)
                        {
                            if_doc_no = ddr2["if_doc_no"].ToString();
                            if_file_no = ddr2["if_file_no"].ToString();

                            if (!string.IsNullOrEmpty(ddr2["if_doc_dt"].ToString()))
                            {
                                if_doc_dt = Convert.ToDateTime(ddr2["if_doc_dt"].ToString());
                            }
                        }
                    }
                    MasterLocation _MasterLocation = new MasterLocation();
                    List<PurchaseOrderDetail> _polist = CHNLSVC.Inventory.GetPOItemsList(_invHeader.Ith_oth_docno);

                    _MasterLocation = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    foreach (ReptPickSerials itemwarr in reptPickSerialsList)
                    {
                        itemwarr.Tus_orig_grndt = _invHeader.Ith_doc_date;

                        itemwarr.IRSM_SYS_BLNO = ib_doc_no;
                        itemwarr.IRSM_BLNO = ib_bl_no;

                        if (string.IsNullOrEmpty(ib_bl_dt.ToString()))
                        {
                            itemwarr.IRSM_BL_DT = Convert.ToDateTime(DateTime.MinValue);
                        }
                        else
                        {
                            itemwarr.IRSM_BL_DT = Convert.ToDateTime(ib_bl_dt.ToString());
                        }

                        itemwarr.IRSM_SYS_FIN_NO = ibi_fin_no;
                        itemwarr.IRSM_FIN_NO = if_file_no;

                        if (string.IsNullOrEmpty(if_doc_dt.ToString()))
                        {
                            itemwarr.IRSM_FIN_DT = Convert.ToDateTime(DateTime.MinValue);
                        }
                        else
                        {
                            itemwarr.IRSM_FIN_DT = Convert.ToDateTime(if_doc_dt.ToString());
                        }

                        itemwarr.Tus_orig_grnno = "";


                        if (_MasterLocation.Ml_cate_1 == "DFS")
                        {
                            _invHeader.Tobond = To_bond;
                        }

                        if (_polist != null)
                        {
                            if (_polist.Count > 0)
                            {
                                var _filtercost = _polist.Where(x => x.Pod_line_no == itemwarr.Tus_itm_line).ToList();
                                if (_filtercost.Count > 0)
                                {
                                    decimal _cost = _filtercost[0].Pod_act_unit_price;
                                    //if (itemwarr.Tus_unit_cost == 0)
                                    // {
                                    itemwarr.Tus_unit_cost = _cost;
                                    itemwarr.Tus_unit_price = _cost;
                                    //}
                                    //else if (_cost != itemwarr.Tus_unit_cost)
                                    //{
                                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('unit price missing');", true);
                                    //    return;
                                    //}
                                }
                            }
                        }
                    }
                    _invHeader.TMP_IS_BATCH_NO_NOT_UPDATE = true;
                    _invHeader.TMP_IS_ALLOCATION = true;
                    _invHeader.Ith_gen_frm = "PDA";
                    result = CHNLSVC.Inventory.ConsignmentReceipt(_invHeader, reptPickSerialsList, reptPickSubSerialsList, _masterAuto, _purchaseOrderDeliveryList, out documntNo);

                    SerialMasterLog SerialMaster = new SerialMasterLog();
                    SerialMaster.Irsm_orig_grn_no = documntNo;
                    SerialMaster.Irsm_doc_dt = DateTime.Now.Date;
                    SerialMaster.Irsm_com = Session["UserCompanyCode"].ToString();
                    SerialMaster.Irsm_loc = Session["UserDefLoca"].ToString();
                    SerialMaster.Irsm_doc_no = documntNo;

                    Int32 UpdateSerMaster = CHNLSVC.Inventory.UpdateSerialMaster(SerialMaster);
                    if (result != -99 && result >= 0)
                    {
                        string _msg = "Successfully Saved GRN! Document No : " + documntNo;
                        divokjob.Visible = true;
                        lblokjob.Text = _msg;
                        Session["documntNo"] = documntNo;
                        string _document = Session["documntNo"].ToString();

                        btnPrint.Visible = true;
                        btnProcess.Visible = false;
                        Session["PrintDoc"] = documntNo;
                        divokjob.Visible = true;
                        lblokjob.Text = _msg;
                        if (Session["PrintDoc"] != null)
                        {
                            btnProcess.Visible = false;
                            btnPrint.Visible = true;
                        }
                        lblMssg.Text = "Do you want print now?";
                        PopupConfBox.Show();  
                    }
                    else
                    {
                         divokjob.Visible = true;
                         lblokjob.Text = documntNo;
                    }

            }
            else
            {
                string msg = "Invalid document number.";
                divalert.Visible = true;
                lblalert.Text = msg;
                return;
            }
            #endregion end grn process
        }
        public bool IsUserProcessed(Int32 _seqno, string _document)
        {
            bool _isUserProcessed = false;
            bool _isProcess = CHNLSVC.Inventory.CheckCompanyMulti(Session["UserCompanyCode"].ToString());
            if (_isProcess == false) { _isUserProcessed = false; return _isUserProcessed; }

            string _error = string.Empty;
            DataTable _tbl = CHNLSVC.Inventory.GetProcessUser(_seqno, _document, Session["UserCompanyCode"].ToString());
            if (_tbl == null || _tbl.Rows.Count <= 0) return _isUserProcessed;
            string _user = _tbl.Rows[0].Field<string>("tuh_pro_user");
            if (string.IsNullOrEmpty(_user))
            {
                _isUserProcessed = false;
                try
                {
                    Int32 _effect = CHNLSVC.Inventory.UpdateProcessUser(Session["UserID"].ToString(), _seqno, _document, Session["UserCompanyCode"].ToString(), out _error);
                    if (_effect == -1)
                    {
                        _isUserProcessed = true;
                        divalert.Visible = true;
                        lblalert.Text = "Error generated while processing.";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    _isUserProcessed = true;
                    _error = ex.Message;
                    divalert.Visible = true;
                    lblalert.Text = _error;
                    return false;
                }
            }
            else
            {
                if (_user == Session["UserID"].ToString()) _isUserProcessed = false;
                else
                {
                    DataTable _us = CHNLSVC.Inventory.GetUserNameByUserID(_user);
                    string _name = string.Empty;
                    if (_us != null && _us.Rows.Count > 0)
                    {
                        _name = _us.Rows[0].Field<string>("SE_USR_NAME");
                    }
                    _isUserProcessed = true;

                    string msg = "This document is processing by the user " + _user + "-" + _name;
                    divalert.Visible = true;
                    lblalert.Text = msg;
                    return false;
                }
            }
            return _isUserProcessed;
        }
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
                    if (doctype == "AOD" || doctype == "GRN" || doctype=="SRN")
                    {
                        if (!CHNLSVC.Security.Is_OptionPerimitted(company, userId, 16070))
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Sorry, You have no permission to genarate AOD Out.( Advice: Required permission code : 16070) !";
                            return;
                        }
                        else
                        {
                            Int32 eff = CHNLSVC.Inventory.addDocumentPrintNew(printdoc, userId, doctype, sessionId, loadingpoint, wareloc, out error);
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
        protected void btnNo_Click(object sender, EventArgs e)
        { }
        private void save_reverse(List<InvoiceItem> _InvDetailList, RequestApprovalHeader _ReqInsHdr, InvoiceHeader invHdr, ReptPickHeader tempHdr)
        {

            string _msg = "";
            decimal _retVal = 0;
            Boolean _isOthRev = false;
            string _orgPC = "";
            string company = Session["UserCompanyName"].ToString();
            string userId = Session["UserID"].ToString();
            string doctype = (string)Session["DOCTYPE"];
            string docno = (string)Session["DOCNO"];
            string location = (string)Session["UserDefLoca"];
            string pc = _ReqInsHdr.Grah_oth_pc;
            DateTime SRNDate = DateTime.Now;
            string sessionid = (string)Session["GlbUserSessionID"];
            string invoiceNo = _ReqInsHdr.Grah_fuc_cd;
            if (_InvDetailList.Count <= 0)
            {
                string _Msg = "Cannot find reverse details.";
                divalert.Visible = true;
                lblalert.Text = _Msg;
                return;
            }


             _isOthRev = false;

             _orgPC = pc;

            decimal _wkNo = 0;
            int _weekNo = CHNLSVC.General.GetWeek(DateTime.Now.Date, out _wkNo, company);

            if (_weekNo == 0)
            {
                string _Msg = "Week Definition is still not setup for current date.Please contact retail accounts dept.";
                divalert.Visible = true;
                lblalert.Text = _Msg;
                return;
            }

            bool _allowCurrentTrans = false;
            //if (IsAllowBackDateForModule(company, string.Empty, pc, Session["GlbModuleName"].ToString(), SRNDate, lblBackDateInfor, txtSRNDate.Text, out _allowCurrentTrans) == false)
            //{
            //    if (_allowCurrentTrans == true)
            //    {
            //        if (Convert.ToDateTime(txtSRNDate.Text).Date != DateTime.Now.Date)
            //        {
            //            string _Msg = "Back date not allow for selected date!";
            //            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
            //            txtSRNDate.Focus();
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        string _Msg = "Back date not allow for selected date!";
            //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
            //        txtSRNDate.Focus();
            //        return;
            //    }
            //}

            _retVal = 0;

            foreach (InvoiceItem tmpItem in _InvDetailList)
            {

                //unit price
                tmpItem.Sad_unit_amt = tmpItem.Sad_unit_rt * tmpItem.Sad_srn_qty;
                //Cus_ITM_QTY invoice qty
                tmpItem.Sad_itm_tax_amt = (tmpItem.Sad_itm_tax_amt / tmpItem.Sad_qty) * tmpItem.Sad_srn_qty;
                tmpItem.Sad_disc_amt = (tmpItem.Sad_disc_amt / tmpItem.Sad_qty) * tmpItem.Sad_srn_qty;
                tmpItem.Sad_disc_rt = (tmpItem.Sad_disc_rt / tmpItem.Sad_qty) * tmpItem.Sad_srn_qty;
                tmpItem.Sad_tot_amt = (tmpItem.Sad_tot_amt / tmpItem.Sad_qty) * tmpItem.Sad_srn_qty;
                //
                _retVal = _retVal + tmpItem.Sad_tot_amt;
            }

            /*  ANAL 8*/
            #region anal 8
            decimal _totRetAmt = 0;
            decimal _crAmt = 0;
            decimal _outAmt = 0;
            decimal _preRevAmt = 0;
            decimal _preCrAmt = 0;
            decimal _balCrAmt = 0;
            DataTable _revAmt = CHNLSVC.Sales.GetTotalRevAmtByInv(invoiceNo);
            if (_revAmt.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(_revAmt.Rows[0]["tot_rtn"].ToString()))
                {
                    _preRevAmt = Convert.ToDecimal(_revAmt.Rows[0]["tot_rtn"]);
                }
            }

            DataTable _preCr = CHNLSVC.Sales.GetTotalCRAmtByInv(invoiceNo);
            if (_preCr.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(_preCr.Rows[0]["tot_Cr"].ToString()))
                {
                    _preCrAmt = Convert.ToDecimal(_preCr.Rows[0]["tot_Cr"]);
                }
            }

            foreach (InvoiceItem _temp in _InvDetailList)
            {
                _totRetAmt = _totRetAmt + _temp.Sad_tot_amt;
            }
            _outAmt = invHdr.Sah_anal_7 - invHdr.Sah_anal_8;
            _outAmt = invHdr.Sah_anal_7 - _preRevAmt - _totRetAmt;// -Convert.ToDecimal(lblTotPayAmt.Text);

            _balCrAmt = invHdr.Sah_anal_8 - _preCrAmt;

            if (_balCrAmt < 0)
            {
                _balCrAmt = 0;
            }

            if (_outAmt > 0)
            {
                _crAmt = _balCrAmt - _outAmt;
            }
            else
            {
                if (_totRetAmt <= _balCrAmt)
                {
                    _crAmt = _totRetAmt;
                }
                else
                {
                    _crAmt = _balCrAmt;
                }
            }

            #endregion
            ////////////////////////////////////////////////////////////////


            InvoiceHeader _invheader = new InvoiceHeader();

            //_invheader.Sah_com = gvInvoice.Rows[i].Cells[2].Text;
            _invheader.Sah_com = company;
            _invheader.Sah_cre_by = userId;
            _invheader.Sah_cre_when = DateTime.Now;
            _invheader.Sah_currency = invHdr.Sah_currency;
            _invheader.Sah_cus_add1 = invHdr.Sah_cus_add1;// lblInvCusAdd1.Text.Trim();
            _invheader.Sah_cus_add2 = invHdr.Sah_cus_add2;// lblInvCusAdd2.Text.Trim();
            _invheader.Sah_cus_cd = invHdr.Sah_cus_cd;// txtCusCode.Text.Trim();
            _invheader.Sah_cus_name = invHdr.Sah_cus_name;// lblInvCusName.Text.Trim();
            _invheader.Sah_d_cust_add1 = invHdr.Sah_d_cust_add1;// _dCusAdd1;
            _invheader.Sah_d_cust_add2 = invHdr.Sah_d_cust_add2;// _dCusAdd2;
            _invheader.Sah_d_cust_cd = invHdr.Sah_d_cust_cd;//_dCusCode;
            _invheader.Sah_direct = false;
            _invheader.Sah_dt = SRNDate.Date;
            _invheader.Sah_epf_rt = 0;
            _invheader.Sah_esd_rt = 0;
            _invheader.Sah_ex_rt = invHdr.Sah_ex_rt;
            _invheader.Sah_ex_rt = invHdr.Sah_ex_rt;
            _invheader.Sah_inv_no = "na";
            _invheader.Sah_inv_sub_tp = "REV";
            _invheader.Sah_inv_tp = invHdr.Sah_inv_tp;
            _invheader.Sah_inv_tp = invHdr.Sah_inv_tp;
            _invheader.Sah_is_acc_upload = false;
            _invheader.Sah_man_cd = invHdr.Sah_man_cd;
            _invheader.Sah_man_ref = invHdr.Sah_man_ref.Trim();
            _invheader.Sah_manual = false;
            _invheader.Sah_mod_by = userId;
            _invheader.Sah_mod_when = DateTime.Now;
            _invheader.Sah_pc = pc; //Session["UserDefProf"].ToString();
            _invheader.Sah_pdi_req = 0;
            _invheader.Sah_ref_doc = invoiceNo.Trim();
            _invheader.Sah_remarks = _ReqInsHdr.Grah_remaks;
            _invheader.Sah_sales_chn_cd = "";
            _invheader.Sah_sales_chn_man = "";
            _invheader.Sah_sales_ex_cd = invHdr.Sah_sales_ex_cd;
            _invheader.Sah_sales_region_cd = "";
            _invheader.Sah_sales_region_man = "";
            _invheader.Sah_sales_sbu_cd = "";
            _invheader.Sah_sales_sbu_man = "";
            _invheader.Sah_sales_str_cd = "";
            _invheader.Sah_sales_zone_cd = "";
            _invheader.Sah_sales_zone_man = "";
            _invheader.Sah_seq_no = 1;
            _invheader.Sah_session_id = sessionid;
            _invheader.Sah_structure_seq = "";
            _invheader.Sah_stus = "A";
            _invheader.Sah_town_cd = "";
            _invheader.Sah_tp = "INV";
            _invheader.Sah_wht_rt = 0;
            _invheader.Sah_tax_inv = invHdr.Sah_tax_inv;
            _invheader.Sah_anal_5 = invHdr.Sah_anal_5;
            _invheader.Sah_anal_3 = invHdr.Sah_anal_3;
            _invheader.Sah_anal_4 = "ARQT014";
            _invheader.Sah_anal_8 = _crAmt;
            _invheader.Sah_anal_7 = _retVal;
            _invheader.Sah_anal_2 = invHdr.Sah_anal_2;

            List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();


            _invHdr = CHNLSVC.Sales.GetPendingInvoices(company, null, invHdr.Sah_cus_cd, invoiceNo, "C", SRNDate.ToString(), SRNDate.ToString());
            


            if (_invHdr != null)
            {
                if (_invHdr.Count > 0)
                {
                    _invheader.Sah_currency = _invHdr[0].Sah_currency;
                    _invheader.Sah_cus_cd = _invHdr[0].Sah_cus_cd;
                    _invheader.Sah_d_cust_add1 = _invHdr[0].Sah_d_cust_add1;
                    _invheader.Sah_d_cust_add2 = _invHdr[0].Sah_d_cust_add2;
                    _invheader.Sah_man_cd = _invHdr[0].Sah_man_cd;
                    _invheader.Sah_pdi_req = _invHdr[0].Sah_pdi_req;
                    _invheader.Sah_remarks = _invHdr[0].Sah_remarks;
                    _invheader.Sah_tax_inv = _invHdr[0].Sah_tax_inv;
                    _invheader.Sah_is_svat = _invHdr[0].Sah_is_svat;
                    _invheader.Sah_tax_exempted = _invHdr[0].Sah_tax_exempted;
                    //add by lakshan 13 Mar 2017 as per the dharshana
                    _invheader.Sah_man_ref = _invHdr[0].Sah_man_ref;
                    _invheader.Sah_currency = _invHdr[0].Sah_currency;
                    _invheader.Sah_town_cd = _invHdr[0].Sah_town_cd;
                    _invheader.Sah_d_cust_cd = _invHdr[0].Sah_d_cust_cd;
                    _invheader.Sah_d_cust_add1 = _invHdr[0].Sah_d_cust_add1;
                    _invheader.Sah_d_cust_add2 = _invHdr[0].Sah_d_cust_add2;
                    _invheader.Sah_man_cd = _invHdr[0].Sah_man_cd;
                    //_invheader.Sah_anal_2 = _invHdr[0].Sah_anal_2;
                    _invheader.Sah_acc_no = _invHdr[0].Sah_acc_no;
                    _invheader.Sah_d_cust_name = _invHdr[0].Sah_d_cust_name;
                    //  _invheader.Sah_inv_no = _invHdr[0].Sah_inv_no;
                }
            }



            MasterAutoNumber _invoiceAuto = new MasterAutoNumber();
            _invoiceAuto.Aut_cate_cd = _ReqInsHdr.Grah_oth_pc;
            _invoiceAuto.Aut_cate_tp = "PC";
            _invoiceAuto.Aut_direction = 0;
            _invoiceAuto.Aut_modify_dt = null;
            _invoiceAuto.Aut_moduleid = "REV";
            _invoiceAuto.Aut_number = 0;
            if (company == "LRP")
            {
                _invoiceAuto.Aut_start_char = "RINREV";
            }
            else if (company== "AIS")
            {
                _invoiceAuto.Aut_start_char = "AINREV";
            }
            else
            {
                _invoiceAuto.Aut_start_char = "INREV";
            }
            _invoiceAuto.Aut_year = null;

            InventoryHeader _inventoryHeader = new InventoryHeader();
            MasterAutoNumber _SRNAuto = new MasterAutoNumber();
            string error = string.Empty;
            List<ReptPickSerials> _doitemserials = new List<ReptPickSerials>();
            _doitemserials = CHNLSVC.Inventory.getScanedSerials(tempHdr.Tuh_usrseq_no, out error);
            //inventory document
            if (error != "")
            {
                divalert.Visible = true;
                lblalert.Text = error;
                return;
            }
            if (_doitemserials != null)
            {
                if (_doitemserials.Count > 0)
                {
                    _inventoryHeader.Ith_com = company;
                    _inventoryHeader.Ith_loc = location;
                    DateTime _docDate = SRNDate.Date;
                    _inventoryHeader.Ith_doc_date = _docDate;
                    _inventoryHeader.Ith_doc_year = _docDate.Year;
                    _inventoryHeader.Ith_direct = true;
                    _inventoryHeader.Ith_doc_tp = "SRN";
                    _inventoryHeader.Ith_cate_tp = invHdr.Sah_inv_sub_tp;
                    _inventoryHeader.Ith_bus_entity = invHdr.Sah_cus_cd;
                    _inventoryHeader.Ith_is_manual = false;
                    _inventoryHeader.Ith_manual_ref = "";
                    _inventoryHeader.Ith_sub_tp = "NOR";
                    _inventoryHeader.Ith_remarks =_ReqInsHdr.Grah_remaks;
                    _inventoryHeader.Ith_stus = "A";
                    _inventoryHeader.Ith_cre_by = userId;
                    _inventoryHeader.Ith_cre_when = DateTime.Now;
                    _inventoryHeader.Ith_mod_by = userId;
                    _inventoryHeader.Ith_mod_when = DateTime.Now;
                    _inventoryHeader.Ith_session_id =sessionid;
                    _inventoryHeader.Ith_pc = _ReqInsHdr.Grah_oth_pc;
                    _inventoryHeader.Ith_oth_loc = null;// _ReqInsHdr.Grah_oth_loc;


                    _SRNAuto.Aut_cate_cd = location;
                    _SRNAuto.Aut_cate_tp = "LOC";
                    _SRNAuto.Aut_direction = 1;
                    _SRNAuto.Aut_modify_dt = null;
                    _SRNAuto.Aut_moduleid = "SRN";
                    _SRNAuto.Aut_number = 0;
                    _SRNAuto.Aut_start_char = "SRN";
                    _SRNAuto.Aut_year = SRNDate.Year;
                }
            }

            List<RecieptHeader> _regReversReceiptHeader = new List<RecieptHeader>();
            MasterAutoNumber _regRevReceipt = new MasterAutoNumber();

            

            List<RecieptHeader> _insReversReceiptHeader = new List<RecieptHeader>();
            MasterAutoNumber _insRevReceipt = new MasterAutoNumber();

            

            string _ReversNo = "";
            string _crednoteNo = ""; //add by chamal 05-12-2012
            if (_doitemserials != null)
            {
                foreach (ReptPickSerials _ser in _doitemserials)
                {
                    string _newStus = "";
                    DataTable _dt = CHNLSVC.General.SearchrequestAppDetByRef(_ReqInsHdr.Grad_ref);
                    var _newStus1 = (from _res in _dt.AsEnumerable()
                                     where _res["grad_anal2"].ToString() == _ser.Tus_itm_cd
                                     select _res["grad_anal8"].ToString()).ToList();
                    if (_newStus1 != null)
                    {
                        if (_newStus1.Count > 0)
                        {
                            _newStus = _newStus1[0];

                            string _stus;
                            string _itm = _ser.Tus_itm_cd;
                            string _orgStus = _ser.Tus_itm_stus;
                            string _serial = _ser.Tus_ser_1;
                            if (!string.IsNullOrEmpty(_newStus))
                            {
                                _stus = _newStus;

                                DataTable dt = CHNLSVC.Inventory.GetItemStatusMaster(_orgStus, null);
                                if (dt.Rows.Count > 0)
                                {
                                    DataTable dt1 = CHNLSVC.Inventory.GetItemStatusMaster(_stus, null);
                                    if (dt1.Rows.Count > 0)
                                    {
                                        if (dt.Rows[0]["mis_is_lp"].ToString() != dt1.Rows[0]["mis_is_lp"].ToString())
                                        {

                                            string _Msg = "Cannot raised different type of status.[Import & Local]. Check the item " + _itm + " and Serial " + _serial;
                                            divalert.Visible = true;
                                            lblalert.Text = _Msg;
                                            return;
                                        }
                                    }
                                }
                            }
                        }

                    }

                    if (_ser.Tus_ser_1 != "N/A")
                    {
                        List<ReptPickSerials> dOSERILA = new List<ReptPickSerials>();
                        dOSERILA = CHNLSVC.Inventory.GetInvoiceSerialForReversalBYSerial(company, location, userId, sessionid, _ser.Tus_bin, _ser.Tus_base_doc_no, _ser.Tus_ser_1);
                        if (dOSERILA != null)
                        {
                            if (dOSERILA.Count > 0)
                            {
                                _ser.Tus_doc_no = dOSERILA[0].Tus_doc_no;
                            }
                        }
                    }
                    InventoryHeader _inventoryHdr = CHNLSVC.Inventory.Get_Int_Hdr(_ser.Tus_doc_no);
                    //get difinition
                    List<InventoryCostRate> _costList = CHNLSVC.Inventory.GetInventoryCostRate(company, "RVR", _newStus, (((SRNDate.Year - _ser.Tus_doc_dt.Year) * 12) + SRNDate.Month - _ser.Tus_doc_dt.Month), _ser.Tus_itm_stus);
                    if (_costList != null && _costList.Count > 0)
                    {
                        if (_costList[0].Rcr_rt == 0)
                        {
                            _ser.Tus_unit_cost = _ser.Tus_unit_cost - _costList[0].Rcr_val;
                            _ser.Tus_unit_cost = Math.Round(_ser.Tus_unit_cost, 4);
                        }
                        else
                        {
                            _ser.Tus_unit_cost = _ser.Tus_unit_cost - ((_ser.Tus_unit_cost * _costList[0].Rcr_rt) / 100);
                            _ser.Tus_unit_cost = Math.Round(_ser.Tus_unit_cost, 4);
                        }
                    }

                    if (!string.IsNullOrEmpty(_newStus))
                    {
                        _ser.Tus_itm_stus = _newStus;
                    }

                }
            }

            #region Check receving serials are duplicating :: Chamal 08-May-2014
            string _err = string.Empty;
            if (_doitemserials != null)
            {
                if (CHNLSVC.Inventory.CheckDuplicateSerialFound(_inventoryHeader.Ith_com, _inventoryHeader.Ith_loc, _doitemserials, out _err) <= 0)
                {
                    string _Msg = _err.ToString();
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                    divalert.Visible = true;
                    lblalert.Text = _Msg;
                    return;
                }
                //    if (CHNLSVC.Inventory.CheckSerialFoundDO(_inventoryHeader.Ith_com, _inventoryHeader.Ith_entry_no, _doitemserials, out _err) <= 0)
                //    {
                //        string _Msg = _err.ToString();
                //        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
                //        DisplayMessage(_Msg, 1);

                //        return;
                //}
            }
            #endregion

            int reptSeqNo = 0;
            if (_doitemserials != null)
            {
                if (_doitemserials.Count > 0)
                {
                    reptSeqNo = _doitemserials[0].Tus_usrseq_no;
                }
            }
            else
            {
                _doitemserials = new List<ReptPickSerials>();
            }
            bool _serialsNotAva = false;
            string _itmCd = ""; Int32 _invLine = 0;
            decimal _selectedSerialCount = 0, _balQty = 0;
            foreach (var item in _InvDetailList)
            {
                if (_doitemserials != null)
                {
                    var vList = _doitemserials.Where(c => c.Tus_itm_cd == item.Sad_itm_cd && c.Tus_base_itm_line == item.Sad_itm_line).ToList();
                    if (vList != null)
                    {
                        _selectedSerialCount = vList.Sum(c => c.Tus_qty);
                    }
                }
                if (item.Sad_do_qty > 0)
                {
                    _balQty = item.Sad_qty - item.Sad_do_qty;
                    if (_balQty <= 0)
                    {
                        if (item.Sad_srn_qty > _selectedSerialCount)
                        {
                            _itmCd = item.Sad_itm_cd;
                            _invLine = item.Sad_itm_line;
                            _serialsNotAva = true;
                            break;
                        }
                    }
                    else
                    {
                        if (item.Sad_srn_qty > _balQty)
                        {
                            _itmCd = item.Sad_itm_cd;
                            _invLine = item.Sad_itm_line;
                            _serialsNotAva = true;
                            break;
                        }
                    }
                }
            }
            if (_serialsNotAva)
            {
                string _Msg = "Already DO has be made for item " + _itmCd + " in line # " + _invLine.ToString() + ". Therefore please select the reversal serial !"; 
                divalert.Visible = true;
                lblalert.Text = _Msg;
                return;
            }
            foreach (var item in _doitemserials)
            {
                item.Tus_orig_grndt = _inventoryHeader.Ith_doc_date;
            }
            _inventoryHeader.Ith_gen_frm = "PDA";
            int effect = CHNLSVC.Sales.SaveReversalNew(_invheader, _InvDetailList, _invoiceAuto, false, out _ReversNo, _inventoryHeader, _doitemserials, null, _SRNAuto, _regRevReceipt, _regReversReceiptHeader, null, false, _insRevReceipt, _insReversReceiptHeader, null, false, _isOthRev, Session["UserDefProf"].ToString(), null, null, null, null, null, false, out _crednoteNo);



            if (effect == 1)
            {
                if (reptSeqNo != 0)
                {
                    CHNLSVC.Inventory.DeleteTempPickObjs(reptSeqNo);
                }

                string _Msg = string.Empty;
                if (string.IsNullOrEmpty(_crednoteNo))
                {
                    _Msg = "Successfully created.Reversal No: " + _ReversNo;
                }
                else
                {
                    _Msg = "Successfully created.Reversal No   : " + _ReversNo + "  " + "SRN No   :" + "  " + _crednoteNo;
                }
                divokjob.Visible = true;
                lblokjob.Text = _Msg;
                Session["documntNo"] = _ReversNo;

                btnPrint.Visible = true;
                btnProcess.Visible = false;
                Session["PrintDoc"] = _ReversNo;
                if (Session["PrintDoc"] != null)
                {
                    btnProcess.Visible = false;
                    btnPrint.Visible = true;
                }
                lblMssg.Text = "Do you want print now?";
                PopupConfBox.Show();  


            }
            else
            {
                if (!string.IsNullOrEmpty(_ReversNo))
                {

                    //string _Msg = "_ReversNo";
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _ReversNo + "');", true);
                 
                    divokjob.Visible = true;
                    lblokjob.Text = _ReversNo;
                    return;

                }
                else
                {

                    string _Msg = "Creation Fail.";
                    divalert.Visible = true;
                    lblalert.Text = _Msg;
                    return;

                }

            }

        }


    }
}