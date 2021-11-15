using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FastForward.SCMPDA.Services;
using FF.BusinessObjects;

namespace FastForward.SCMPDA
{
    public partial class CheckScannedStock : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    grdscanneditems.DataSource = new int[] { };
                    grdscanneditems.DataBind();
                    LoadGrid();
                    //CalCulateTotQty();
                    totalScnQty();
                    //GetDocQty();
                    getTotalDocQty();

                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private void SetScrollTop()
        {
            try
            {
                Page.SetFocus(this.dvscanjobs.ClientID);
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void lbtndicalertclose_Click(object sender, EventArgs e)
        {
            try
            {
                divalert.Visible = false;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtndivinfoclose_Click(object sender, EventArgs e)
        {
            try
            {
                Divinfo.Visible = false;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtnok_Click(object sender, EventArgs e)
        {
            try
            {
                divok.Visible = false;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            try
            {
                string docdirection = (string)Session["DOCDIRECTION"];

                if (docdirection == "1")
                {
                    Response.Redirect("CreateJobNumber.aspx");
                }
                else
                {
                    if (Session["UserCompanyName"].ToString() == "AAL")
                    {
                        Response.Redirect("CreateOutJobNew.aspx");
                    }
                    else
                    {
                        Response.Redirect("CreateOutJob.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private void LoadGrid()
        {
            try
            {
                string _scanDocument = (string)Session["DOCNO"];
                Int32 seqno = Convert.ToInt32(Session["SEQNO"].ToString());
                string getitemvalue = Request.QueryString["Item"].ToString();
                string getbinvalue = Request.QueryString["Bin"].ToString();
                string getitestatusmvalue = Request.QueryString["ItemStatus"].ToString();
                string warecom = (string)Session["WAREHOUSE_COMPDA"];
                string wareloc = (string)Session["WAREHOUSE_LOCPDA"];
                string loadingpoint = (string)Session["LOADING_POINT"];

                DataTable dtitems = CHNLSVC.Inventory.LoadSavedSerialsSeq(seqno, Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString(), getitemvalue, getbinvalue, getitestatusmvalue, warecom, wareloc, loadingpoint);
                grdscanneditems.DataSource = null;
                grdscanneditems.DataBind();

                grdscanneditems.DataSource = dtitems;
                grdscanneditems.DataBind();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        private void totalScnQty()
        {
            try
            {
                if ((String)Session["DOCNO"] != "" && (!string.IsNullOrEmpty(Session["SEQNO"].ToString())))
                {
                    Int32 seqno = Convert.ToInt32(Session["SEQNO"].ToString());
                    DataTable dttotqty = CHNLSVC.Inventory.GetItemTotalScanedQtySeq(seqno);
                    if (dttotqty.Rows.Count > 0)
                    {
                        foreach (DataRow ddrtotitem in dttotqty.Rows)
                        {
                            lbltotqtylbl.Text = (ddrtotitem["SEQ_QTY"].ToString() != "") ? ddrtotitem["SEQ_QTY"].ToString() : "0";
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
        //private void CalCulateTotQty()
        //{
        //    try
        //    {
        //        Decimal totqty = 0;

        //        foreach (GridViewRow ddritem in grdscanneditems.Rows)
        //        {
        //           Label tb = (Label)ddritem.FindControl("lbltotqty");
        //           totqty = totqty + Convert.ToDecimal(tb.Text);
        //        }

        //        lbltotqtylbl.Text = totqty.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        divalert.Visible = true;
        //        lblalert.Text = ex.Message;
        //    }
        //}
        private void DivsHide()
        {
            try
            {
                divalert.Visible = false;
                Divinfo.Visible = false;
                divok.Visible = false;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void lbtndelete_Click(object sender, EventArgs e)
        {
            DivsHide();
            string _scanDocument = (string)Session["DOCNO"];
            Int32 docseq = Convert.ToInt32(Session["SEQNO"].ToString());
            //if (txtconfirmdelete.Value == "Yes")
            //{
                string finishStus = (string)Session["FINISHED"];
                if (finishStus == "0")
                {
                    try
                    {
                        string warecom = (string)Session["WAREHOUSE_COMPDA"];
                        string wareloc = (string)Session["WAREHOUSE_LOCPDA"];
                        string loadingpoint = (string)Session["LOADING_POINT"];
                        string company=Session["UserCompanyCode"].ToString();
                        string location=Session["UserDefLoca"].ToString();

                        string iscreatejob=(string)Session["CreateJobNumber"];
                        string docdirection = (string)Session["DOCDIRECTION"];
                        string serialCheck = (string)Session["DocHasSer"];
                        string doctyp = (string)Session["Doctype"];
                        string docno=(string)Session["DOCNO"];
                        string locserialcheck = (string)Session["LOCISSERIAL"];
                        string error = string.Empty;
                        var lb = (LinkButton)sender;
                        var row = (GridViewRow)lb.NamingContainer;
                        if (row != null)
                        {
                            string _item = (row.FindControl("lblitm") as Label).Text;
                            string _serial = (row.FindControl("lblse1") as Label).Text;
                            string _seqno = (row.FindControl("lblseq") as Label).Text;
                            string _status = (row.FindControl("lblCode") as Label).Text;
                            string _bin = (row.FindControl("lblbinno") as Label).Text;
                            string _qty = (row.FindControl("lbltotqty") as Label).Text;

                            /* CHECK INR SER */
                            // DataTable inrserialdata = CHNLSVC.Inventory.getINRSerial(txtserialnumber1.Text.Trim().ToString(), txtitemcode.Text.ToUpper().Trim().ToString());
                            string inrdocno = "";
                            Int32 serialid = 0;
                            Int32 inritemline = 0;
                            Int32 inrbatchline = 0;
                            Int32 inrserialline = 0;

                            ReptPickItems _itemsQty = new ReptPickItems();
                            ReptPickSerials _inputReptPickSerials = new ReptPickSerials();
                            ReptPickSerials _inputReptPickSerialsUpdate = new ReptPickSerials();
                            //if (inrserialdata.Rows.Count > 0)
                            //{
                            //    inrdocno = inrserialdata.Rows[0]["ins_doc_no"].ToString();
                            //    // serial id
                            //   // serialid = Convert.ToInt32(inrserialdata.Rows[0]["ins_ser_id"]);
                            //}
                            //rukshan function

                            //  DataTable inrserialdata = CHNLSVC.Inventory.GET_INR_SER(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), null, _item.ToUpper().Trim().ToString(), _status, _serial.Trim().ToString());
                            if (!string.IsNullOrEmpty(_serial) && _serial != "N/A" && _serial != "NA"

                                && _serial != "n/a" && _serial != "na")
                            {
                                DataTable inrserialdata = CHNLSVC.Inventory.getINRSerial(_serial.Trim().ToString(), _item.ToUpper().Trim().ToString());
                                if (inrserialdata.Rows.Count > 0)
                                {
                                    serialid = Convert.ToInt32(inrserialdata.Rows[0]["ins_ser_id"]);
                                    inrdocno = inrserialdata.Rows[0]["ins_doc_no"].ToString();
                                    inritemline = Convert.ToInt32(inrserialdata.Rows[0]["ins_itm_line"]);
                                    inrbatchline = Convert.ToInt32(inrserialdata.Rows[0]["ins_batch_line"]);
                                    inrserialline = Convert.ToInt32(inrserialdata.Rows[0]["ins_ser_line"]);
                                }
                            //else
                            //{
                            //    divalert.Visible = true;
                            //    lblalert.Text = "Not available serial id !!!";
                            //    return;
                            //}
                            if (serialCheck == "True" && docdirection == "1" && doctyp == "AOD")
                            {
                                DataTable IntSerData = CHNLSVC.Inventory.getINTSerial(_serial.Trim().ToString(), _item.ToUpper().Trim().ToString(), docno);
                                if (IntSerData.Rows.Count > 0)
                                {
                                    if (IntSerData.Rows[0]["its_pick"].ToString() == "1")
                                    {
                                        divalert.Visible = true;
                                        lblalert.Text = "Already picked serial.cannot delete.";
                                        return;
                                    }
                                }
                                if (IntSerData.Rows[0]["its_reversed"] == DBNull.Value || IntSerData.Rows[0]["its_reversed"].ToString() == "1")
                                {
                                    serialid = Convert.ToInt32(IntSerData.Rows[0]["ITS_SER_ID"]);
                                    inrdocno = IntSerData.Rows[0]["ITS_DOC_NO"].ToString();
                                    inritemline = Convert.ToInt32(IntSerData.Rows[0]["ITS_ITM_LINE"]);
                                    inrbatchline = Convert.ToInt32(IntSerData.Rows[0]["ITS_BATCH_LINE"]);
                                    inrserialline = Convert.ToInt32(IntSerData.Rows[0]["ITS_SER_LINE"]);
                                }
                            }
                            else if (docdirection == "1" && doctyp == "SRN")
                            {
                                DataTable IntSerData = CHNLSVC.Inventory.getINTSerialdo(docno, _item.ToUpper().Trim().ToString(), _serial.Trim().ToString());
                                if (IntSerData.Rows.Count > 0)
                                {
                                    if (IntSerData.Rows[0]["its_pick"].ToString() == "1")
                                    {
                                        divalert.Visible = true;
                                        lblalert.Text = "Already picked serial.cannot delete.";
                                        return;
                                    }
                                }
                                if (IntSerData.Rows[0]["its_reversed"] == DBNull.Value || IntSerData.Rows[0]["its_reversed"].ToString() == "1")
                                {
                                    serialid = Convert.ToInt32(IntSerData.Rows[0]["ITS_SER_ID"]);
                                    inrdocno = IntSerData.Rows[0]["ITS_DOC_NO"].ToString();
                                    inritemline = Convert.ToInt32(IntSerData.Rows[0]["ITS_ITM_LINE"]);
                                    inrbatchline = Convert.ToInt32(IntSerData.Rows[0]["ITS_BATCH_LINE"]);
                                    inrserialline = Convert.ToInt32(IntSerData.Rows[0]["ITS_SER_LINE"]);
                                }
                            }

                        }
                            if (!string.IsNullOrEmpty(_serial) && _serial != "N/A" && _serial != "NA"

                                && _serial != "n/a" && _serial != "na")
                            {
                                //ReptPickSerials _inputReptPickSerials = new ReptPickSerials();
                                _inputReptPickSerials.Tus_doc_no = _scanDocument;
                                _inputReptPickSerials.Tus_com = Session["UserCompanyName"].ToString();
                                _inputReptPickSerials.Tus_loc = Session["UserDefLoca"].ToString();
                                _inputReptPickSerials.Tus_itm_cd = _item;
                                _inputReptPickSerials.Tus_ser_1 = _serial;
                                _inputReptPickSerials.Tus_bin = _bin;
                                _inputReptPickSerials.Tus_usrseq_no = docseq;
                                //Int32 val = CHNLSVC.Inventory.DeleteItemsWIthSerials(_inputReptPickSerials);

                                //if (Convert.ToInt32(val) == -1)
                                //{
                                //    divalert.Visible = true;
                                //    lblalert.Text = "Error in processing !!!";
                                //    //SetScrollTop();
                                //    return;
                                //}
                                Int32 _itmqty = 0;
                                DataTable dtqty = CHNLSVC.Inventory.GetItemQty(Convert.ToInt32(_seqno), _scanDocument, _item, _status, warecom, wareloc, loadingpoint);
                                foreach (DataRow ddrrownumqty in dtqty.Rows)
                                {
                                    if (string.IsNullOrEmpty(ddrrownumqty["QTY"].ToString()))
                                    {
                                        _itmqty = 0;
                                    }
                                    else
                                    {
                                        _itmqty = Convert.ToInt32(ddrrownumqty["QTY"].ToString());
                                    }
                                }
                                
                                if (serialCheck == "True" && docdirection == "1" && doctyp == "AOD")
                                {
                                    //ReptPickItems _itemsQty = new ReptPickItems();
                                    //int seout = CHNLSVC.Inventory.UpdateExistingSerialRecived(_scanDocument, serialid, 0);
                                    _itemsQty.Tui_pic_itm_qty = Convert.ToDecimal(_itmqty);
                                    _itemsQty.Tui_usrseq_no = Convert.ToInt32(_seqno);
                                    _itemsQty.Tui_req_itm_cd = _item;
                                    _itemsQty.Tui_req_itm_stus = _status;
                                    //Int32 valitemqty = CHNLSVC.Inventory.UpdateQty(_itemsQty);
                                    //if (Convert.ToInt32(valitemqty) == -1)
                                    //{
                                    //    divalert.Visible = true;
                                    //    lblalert.Text = "Error in processing !!!";
                                    //    //SetScrollTop();
                                    //    return;
                                    //}
                                }
                                //if (docdirection == "0")
                                //{
                                    //ReptPickItems _itemsQty = new ReptPickItems();

                                    _itemsQty.Tui_pic_itm_qty = Convert.ToDecimal(_itmqty);
                                    _itemsQty.Tui_usrseq_no = Convert.ToInt32(_seqno);
                                    _itemsQty.Tui_req_itm_cd = _item;
                                    _itemsQty.Tui_req_itm_stus = _status;
                                    //Int32 valitemqty = CHNLSVC.Inventory.UpdateQty(_itemsQty);
                                    //Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(company, location, _item.ToUpper().Trim().ToString(), Convert.ToInt32(serialid), 1);
                                    //if (Convert.ToInt32(valitemqty) == -1)
                                    //{
                                    //    divalert.Visible = true;
                                    //    lblalert.Text = "Error in processing !!!";
                                    //    //SetScrollTop();
                                    //    return;
                                    //}
                                //}
                                Int32 eff = CHNLSVC.Inventory.deleteSeriallisezedSerial(serialCheck, docdirection, doctyp,_scanDocument, _inputReptPickSerials, _itemsQty, company, location, _item.ToUpper().Trim().ToString(), Convert.ToInt32(serialid),locserialcheck,docseq,out error);
                                if (eff == -1 || error != "")
                                {
                                    divalert.Visible = true;
                                    lblalert.Text = error;
                                    return;
                                }
                                else
                                {
                                    LoadGrid();
                                    divok.Visible = true;
                                    lblokjob.Text = "Successfully deleted !!!";
                                    //CalCulateTotQty();
                                    totalScnQty();
                                    //SetScrollTop();
                                }
                            }
                            else
                            {
                                //ReptPickSerials _inputReptPickSerialsUpdate = new ReptPickSerials();
                                _inputReptPickSerialsUpdate.Tus_seq_no = Convert.ToInt32(_seqno);
                                _inputReptPickSerialsUpdate.Tus_qty = Convert.ToDecimal(_qty);
                                _inputReptPickSerialsUpdate.Tus_doc_no = _scanDocument;
                                _inputReptPickSerialsUpdate.Tus_com = Session["UserCompanyName"].ToString();
                                _inputReptPickSerialsUpdate.Tus_loc = Session["UserDefLoca"].ToString();
                                _inputReptPickSerialsUpdate.Tus_bin = _bin;
                                _inputReptPickSerialsUpdate.Tus_itm_stus = _status;
                                _inputReptPickSerialsUpdate.Tus_itm_cd = _item;

                                //Int32 val = CHNLSVC.Inventory.UpdateSerializedItemsQty(_inputReptPickSerialsUpdate);

                                //if (Convert.ToInt32(val) == -1)
                                //{
                                //    divalert.Visible = true;
                                //    lblalert.Text = "Error in processing !!!";
                                //    //SetScrollTop();
                                //    return;
                                //}

                                

                                Int32 _itmqty = 0;
                                DataTable dtqty = CHNLSVC.Inventory.GetItemQty(Convert.ToInt32(_seqno), _scanDocument, _item, _status, warecom, wareloc, loadingpoint);
                                foreach (DataRow ddrrownumqty in dtqty.Rows)
                                {
                                    if (string.IsNullOrEmpty(ddrrownumqty["QTY"].ToString()))
                                    {
                                        _itmqty = 0;
                                    }
                                    else
                                    {
                                        _itmqty = Convert.ToInt32(ddrrownumqty["QTY"].ToString());
                                    }
                                }


                                //ReptPickItems _itemsQty = new ReptPickItems();

                                _itemsQty.Tui_pic_itm_qty = Convert.ToDecimal(_itmqty);
                                _itemsQty.Tui_usrseq_no = Convert.ToInt32(_seqno);
                                _itemsQty.Tui_req_itm_cd = _item;
                                _itemsQty.Tui_req_itm_stus = _status;
                                //Int32 valitemqty = CHNLSVC.Inventory.UpdateQty(_itemsQty);

                                //if (Convert.ToInt32(valitemqty) == -1)
                                //{
                                //    divalert.Visible = true;
                                //    lblalert.Text = "Error in processing !!!";
                                //    //SetScrollTop();
                                //    return;
                                //}
                                Int32 eff = CHNLSVC.Inventory.deleteNonSerialItems(_itemsQty, _inputReptPickSerialsUpdate, out error);
                                if (eff == -1 || error != "")
                                {
                                    divalert.Visible = true;
                                    lblalert.Text = error;
                                    return;
                                }
                                else
                                {
                                    LoadGrid();
                                    divok.Visible = true;
                                    lblokjob.Text = "One non serial item deleted !!!";
                                    //CalCulateTotQty();
                                    totalScnQty();
                                    //SetScrollTop();
                                }
                              
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        divalert.Visible = true;
                        lblalert.Text = ex.Message;
                    }
                    finally
                    {
                        CHNLSVC.CloseAllChannels();
                    }
                }
                else {
                    divalert.Visible = true;
                    lblalert.Text = "Cannot delete finished document serials";
                    //SetScrollTop();
                    return;
                }
            //}
        }
        protected void lbtndeletenonser_Click(object sender, EventArgs e)
        {
            try
            {
                DivsHide();

                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                if (row != null)
                {
                    string _item = (row.FindControl("lblitm") as Label).Text;
                    string _serial = (row.FindControl("lblse1") as Label).Text;
                    string _seqno = (row.FindControl("lblseq") as Label).Text;
                    string _status = (row.FindControl("lblCode") as Label).Text;
                    string _bin = (row.FindControl("lblbinno") as Label).Text;
                    string _qty = (row.FindControl("lbltotqty") as Label).Text;

                    string warecom = (string)Session["WAREHOUSE_COMPDA"];
                    string wareloc = (string)Session["WAREHOUSE_LOCPDA"];
                    string loadingpoint = (string)Session["LOADING_POINT"];

                    if (string.IsNullOrEmpty(_serial) || _serial == "N/A")
                    {
                        if (_qty == "0")
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Qty is 0 !!!";
                            return;
                        }
                        string _scanDocument = (string)Session["DOCNO"];

                        ReptPickSerials _inputReptPickSerialsUpdate = new ReptPickSerials();

                        _inputReptPickSerialsUpdate.Tus_seq_no = Convert.ToInt32(_seqno);
                        _inputReptPickSerialsUpdate.Tus_qty = 1;
                        _inputReptPickSerialsUpdate.Tus_doc_no = _scanDocument;
                        _inputReptPickSerialsUpdate.Tus_com = Session["UserCompanyName"].ToString();
                        _inputReptPickSerialsUpdate.Tus_loc = Session["UserDefLoca"].ToString();
                        _inputReptPickSerialsUpdate.Tus_bin = _bin;
                        _inputReptPickSerialsUpdate.Tus_itm_stus = _status;
                        _inputReptPickSerialsUpdate.Tus_itm_cd = _item;

                        Int32 val = CHNLSVC.Inventory.UpdateSerializedItemsQty(_inputReptPickSerialsUpdate);

                        if (Convert.ToInt32(val) == -1)
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Error in processing !!!";
                            SetScrollTop();
                            return;
                        }

                        LoadGrid();

                        Int32 _itmqty = 0;
                        DataTable dtqty = CHNLSVC.Inventory.GetItemQty(Convert.ToInt32(_seqno), _scanDocument, _item, _status, warecom, wareloc, loadingpoint);
                        foreach (DataRow ddrrownumqty in dtqty.Rows)
                        {
                            if (string.IsNullOrEmpty(ddrrownumqty["QTY"].ToString()))
                            {
                                _itmqty = 0;
                            }
                            else
                            {
                                _itmqty = Convert.ToInt32(ddrrownumqty["QTY"].ToString());
                            }
                        }


                        ReptPickItems _itemsQty = new ReptPickItems();

                        _itemsQty.Tui_pic_itm_qty = Convert.ToDecimal(_itmqty);
                        _itemsQty.Tui_usrseq_no = Convert.ToInt32(_seqno);
                        _itemsQty.Tui_req_itm_cd = _item;
                        _itemsQty.Tui_req_itm_stus = _status;
                        Int32 valitemqty = CHNLSVC.Inventory.UpdateQty(_itemsQty);

                        if (Convert.ToInt32(valitemqty) == -1)
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Error in processing !!!";
                            SetScrollTop();
                            return;
                        }

                        divok.Visible = true;
                        lblokjob.Text = "Successfully deleted !!!";
                        //CalCulateTotQty();
                        totalScnQty();
                        SetScrollTop();
                    }
                    else
                    {
                        divalert.Visible = true;
                        lblalert.Text = "This is not a serialized item !!!";
                        SetScrollTop();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void getTotalDocQty()
        {
            try
            {
                if ((String)Session["DOCNO"] != "" && (!string.IsNullOrEmpty(Session["SEQNO"].ToString())))
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

        //private void GetDocQty()
        //{
        //    try
        //    {
        //        Int32 seqno = 0;

        //        foreach (GridViewRow Row in grdscanneditems.Rows)
        //        {
        //             Label lblseq = (Label)Row.FindControl("lblseq");
        //             seqno = Convert.ToInt32(lblseq.Text);
        //        }

        //        DataTable dtdoccount = CHNLSVC.Inventory.GetDocQty(seqno);

        //        foreach (DataRow ddr in dtdoccount.Rows)
        //        {
        //            lbldocqty.Text = ddr["DOC_QTY"].ToString();
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
    }
}