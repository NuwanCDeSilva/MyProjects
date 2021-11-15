using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.InventoryNew;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Inventory
{
    public partial class BinToBinTransfer : BasePage
    {
        private string _SerialSearchType = string.Empty;
        private List<InventoryRequestItem> ScanItemList = null;
        private List<InventoryRequestItem> ScanItemListExcel = null;
        private string _receCompany = string.Empty;
        private string RequestNo = string.Empty;
        private string SelectedStatus = string.Empty;
        private MasterItem _itemdetail = null;
        bool _isDecimalAllow = false;
        private string _chargeType = string.Empty;
        private List<string> SeqNumList = null;
        private List<InventoryRequestItem> ItemList = new List<InventoryRequestItem>();

        public string textbin
        {
            get { return txtFromBin.Text; }
            set { value = txtFromBin.Text; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Clear();
                    loadSequences();
                    loadItemStatus();
                    ucOutScan.LBSERIALSEARChnew.Visible = true;
                    ucOutScan.LBSERIALSEARCh.Visible = false;
                }
                else
                {
                    if (Session["YY"] != null && Session["YY"].ToString() == "YY")
                    {
                        UserPopoup.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        #region Events

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtsaveconfirm.Value == "Yes")
            {
                Process();
            }
        }

        protected void ddlSeqNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ddlSeqNo.Text))
                {
                    txtUserSeqNo.Text = ddlSeqNo.Text;
                    LoadItems(txtUserSeqNo.Text);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        //protected void lbtnmodelfind_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DateTime d1 = DateTime.Now;
        //        d1 = d1.AddMonths(-1);
        //        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
        //        DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, d1, DateTime.Now);
        //        grdResult.DataSource = _result;
        //        grdResult.DataBind();
        //        txtTDate.Text = System.DateTime.Now.ToShortDateString();
        //        txtFDate.Text = d1.ToShortDateString();
        //        BindUCtrlDDLData(_result);
        //        lblvalue.Text = "CSSDOC";
        //        ViewState["SEARCH"] = _result;
        //        UserPopoup.Show();
        //        Session["YY"] = "YY";
        //        txtSearchbyword.Focus();
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
        //    }
        //    finally
        //    {
        //        CHNLSVC.CloseChannel();
        //    }
        //}

        protected void lbtnprintord_Click(object sender, EventArgs e)
        {
            if (txtconfirmclear.Value == "Yes")
            {
                try
                {
                    ClearAll();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
            }
        }

        protected void lbtndelete_Click(object sender, EventArgs e)
        {
            if (txtconfirmdelete.Value == "Yes")
            {
                try
                {
                    if ((btnSave.Enabled == true) && (!string.IsNullOrEmpty(txtUserSeqNo.Text.Trim())))
                    {
                        var lb = (LinkButton)sender;
                        var row = (GridViewRow)lb.NamingContainer;

                        if (row != null)
                        {
                            string _item = (row.FindControl("lblitri_itm_cd") as Label).Text;

                            if (string.IsNullOrEmpty(_item))
                            {
                                return;
                            }

                            string _itmStatus = (row.FindControl("lblcurstgrd") as Label).Text;
                            string _itmCost = (row.FindControl("lblunitcostgrd") as Label).Text;
                            string _qty = (row.FindControl("lblitri_qty") as Label).Text;
                            string _lineNo = (row.FindControl("lblitri_line_no") as Label).Text;

                            bool result = CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32(txtUserSeqNo.Text), _item, _itmStatus, Convert.ToInt32(Convert.ToDecimal(_lineNo)), 2);

                            List<ReptPickSerials> _list = new List<ReptPickSerials>();
                            _list = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), "ADJ-B");
                            if (_list != null)
                            {
                                if (_list.Count > 0)
                                {
                                    var _delete = (from _lst in _list
                                                   where _lst.Tus_itm_cd == _item && _lst.Tus_itm_stus == _itmStatus && _lst.Tus_base_itm_line == Convert.ToInt32(Convert.ToDecimal(_lineNo))
                                                   select _lst).ToList();

                                    foreach (ReptPickSerials _ser in _delete)
                                    {
                                        Delete_Serials(_item, _itmStatus, _ser.Tus_ser_id, _ser.Tus_base_itm_line);
                                    }
                                }
                            }
                            LoadItems(txtUserSeqNo.Text);
                        }
                    }
                }
                catch (Exception err)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
                finally
                {
                    CHNLSVC.CloseChannel();
                }
            }
        }

        protected void lbtndeleteserial_Click(object sender, EventArgs e)
        {
            if (txtconfirmdeleteSerial.Value == "Yes")
            {
                try
                {
                    var lb = (LinkButton)sender;
                    var row = (GridViewRow)lb.NamingContainer;

                    string _item = string.Empty;
                    string _itmStatus = string.Empty;
                    string _serialID = string.Empty;
                    string _bin = string.Empty;
                    string _serial_1 = string.Empty;
                    string _requestno = string.Empty;
                    string _lineNo = string.Empty;
                    string _itmLine = string.Empty;

                    if (row != null)
                    {
                        _item = (row.FindControl("lblitmgrd2") as Label).Text;
                        _itmStatus = (row.FindControl("lblstusgrd2") as Label).Text;
                        _serialID = (row.FindControl("lblseridgrd2") as Label).Text;
                        _bin = (row.FindControl("lblbingrd") as Label).Text;
                        _serial_1 = (row.FindControl("lblser1grd2") as Label).Text;
                        _requestno = (row.FindControl("lblrequest2") as Label).Text;
                        _lineNo = (row.FindControl("lblbaseline") as Label).Text;
                        _itmLine = (row.FindControl("lblItemline") as Label).Text;
                    }

                    if (string.IsNullOrEmpty(_item))
                    {
                        return;
                    }

                    MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);

                    if (_masterItem.Mi_is_ser1 == 1)
                    {
                        bool result = CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), Convert.ToInt32(_serialID), null, null);
                        bool result2 = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, Convert.ToInt32(_serialID), 1);

                        List<ReptPickSerials> _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), "ADJ-B");

                        Int32 result1 = CHNLSVC.Inventory.UPDATE_PICK_QTY(Convert.ToDecimal("1"), Convert.ToInt32(txtUserSeqNo.Text), _item, _itmStatus, 1);
                    }
                    else if (_masterItem.Mi_is_ser1 == -1)
                    {
                        string err = string.Empty;
                        ReptPickSerials oInItem = new ReptPickSerials();
                        oInItem.Tus_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text);
                        oInItem.Tus_itm_cd = _item;
                        oInItem.Tus_itm_stus = _itmStatus;
                        oInItem.Tus_itm_line = Convert.ToInt32(_itmLine);

                        Int32 result1 = CHNLSVC.Inventory.DeletePickSerByItemAndBaseItemLine(oInItem, out err);
                        //Int32 result = CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), _item, _itmStatus);
                    }
                    else
                    {
                        string err = string.Empty;
                        ReptPickSerials oInItem = new ReptPickSerials();
                        oInItem.Tus_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text);
                        oInItem.Tus_itm_cd = _item;
                        oInItem.Tus_itm_stus = _itmStatus;
                        oInItem.Tus_base_itm_line = Convert.ToInt32(_lineNo);

                        Int32 result1 = CHNLSVC.Inventory.DeletePickSerByItemAndBaseItemLine(oInItem, out err);
                        bool result = CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32(txtUserSeqNo.Text), _item, _itmStatus, Convert.ToInt32(_lineNo), 2);
                    }

                    //CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32(txtUserSeqNo.Text), _item, _itmStatus, Convert.ToInt32(_lineNo), 2);
                    LoadItems(txtUserSeqNo.Text);
                    loadSequences();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
                finally
                {
                    CHNLSVC.CloseChannel();
                }
            }
        }

        protected void txtFromBin_TextChanged(object sender, EventArgs e)
        {
            WarehouseBin oWarehouseBin = CHNLSVC.Inventory.GET_BIN_BY_CODE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtFromBin.Text.Trim().ToUpper().ToUpper());
            if (oWarehouseBin != null && !string.IsNullOrEmpty(oWarehouseBin.Ibn_bin_cd))
            {
                txtFromBin.Text = oWarehouseBin.Ibn_bin_cd;
                ucOutScan.BinCode = oWarehouseBin.Ibn_bin_cd;
                ucOutScan.loadBinCode();
            }
            else
            {
                DisplayMessage("Please enter a valid from BIN code");
                txtFromBin.Focus();
            }
        }

        protected void btnFromBin_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchBINCodes);
                DataTable result = CHNLSVC.CommonSearch.SEARCH_BIN(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "SearchBINCodes_From";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                selDatePanal(false);
                Session["YY"] = "YY";
                UserPopoup.Show();
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
            }
        }

        protected void txtToBin_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFromBin.Text) && txtFromBin.Text.Trim().ToUpper() == txtToBin.Text.Trim().ToUpper())
            {
                DisplayMessage("Bin code can not be same");
                txtToBin.Text = "";
                return;
            }
            else
            {
                WarehouseBin oWarehouseBin = CHNLSVC.Inventory.GET_BIN_BY_CODE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtToBin.Text.Trim().ToUpper());
                if (oWarehouseBin != null && !string.IsNullOrEmpty(oWarehouseBin.Ibn_bin_cd))
                {
                    txtToBin.Text = oWarehouseBin.Ibn_bin_cd;
                    ucOutScan.ToBinCode = oWarehouseBin.Ibn_bin_cd;
                }
                else
                {
                    DisplayMessage("Please enter a valid to BIN code");
                    txtToBin.Focus();
                }
            }
        }

        protected void btnToBin_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchBINCodes);
                DataTable result = CHNLSVC.CommonSearch.SEARCH_BIN(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "SearchBINCodes_To";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                Session["YY"] = "YY";
                UserPopoup.Show();
                selDatePanal(false);
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
            }
        }

        #endregion

        #region Search
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["YY"] = null;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                string ID = grdResult.SelectedRow.Cells[1].Text;
                if (lblvalue.Text == "CSSDOC")
                {
                    string Date = grdResult.SelectedRow.Cells[3].Text;
                    //txtDocumentNo.Text = ID;
                    lblvalue.Text = "";
                    Session["CSSDOC"] = Date;
                    //txtDocumentNo_Leave();
                    UserPopoup.Hide();
                    return;
                }
                else if (lblvalue.Text == "SearchBINCodes_From")
                {
                    txtFromBin.Text = grdResult.SelectedRow.Cells[1].Text;
                    ucOutScan.BinCode = txtFromBin.Text;
                    txtFromBin_TextChanged(null, null);
                }
                else if (lblvalue.Text == "SearchBINCodes_To")
                {
                    txtToBin.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtToBin_TextChanged(null, null);
                }
                UserPopoup.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdResult.PageIndex = e.NewPageIndex;
                if (lblvalue.Text == "CSSDOC")
                {
                    grdResult.DataSource = null;

                    grdResult.DataSource = (DataTable)ViewState["SEARCH"];
                    grdResult.DataBind();
                    UserPopoup.Show();
                    txtSearchbyword.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        //protected void lbtnDateS_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (lblvalue.Text == "CSSDOC")
        //        {
        //            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
        //            DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
        //            grdResult.DataSource = _result;
        //            grdResult.DataBind();
        //            lblvalue.Text = "CSSDOC";
        //            UserPopoup.Show();
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
        //    }
        //    finally
        //    {
        //        CHNLSVC.CloseChannel();
        //    }
        //}

        protected void btnClose_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            Session["YY"] = null;
        }

        private void FilterData()
        {
            try
            {
                if (lblvalue.Text == "CSSDOC")
                {
                    //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
                    //DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString(), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                    //grdResult.DataSource = _result;
                    //grdResult.DataBind();
                    //lblvalue.Text = "CSSDOC";
                    //Session["CSSDOC"] = "true";
                    //ViewState["SEARCH"] = _result;
                    //UserPopoup.Show();
                    //return;
                }
                else if (lblvalue.Text == "SearchBINCodes_From")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchBINCodes);
                    DataTable result = CHNLSVC.CommonSearch.SEARCH_BIN(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "SearchBINCodes_From";
                    ViewState["SEARCH"] = result;
                    UserPopoup.Show();
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "SearchBINCodes_To")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchBINCodes);
                    DataTable result = CHNLSVC.CommonSearch.SEARCH_BIN(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "SearchBINCodes_To";
                    ViewState["SEARCH"] = result;
                    UserPopoup.Show();
                    txtSearchbyword.Focus();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
            }
        }
        #endregion

        #region Methods

        #region Messages
        private void DisplayMessage(String Msg, Int32 option)
        {
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
            }
        }

        private void DisplayMessage(String Msg)
        {
            Msg = Msg.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
        }

        private void DisplayMessageJS(String Msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + Msg + "');", true);
        }

        #endregion

        protected void BindGrid()
        {
            try
            {
                grdItems.DataSource = (DataTable)ViewState["ITEMCHANGE"];
                grdItems.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void BindSerGrid()
        {
            try
            {
                grdSerial.DataSource = (DataTable)ViewState["ITEMCHANGESER"];
                grdSerial.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                return;
            }
        }

        private void Clear()
        {
            try
            {
                grdItems.DataSource = new int[] { };
                grdItems.DataBind();

                grdSerial.DataSource = new int[] { };
                grdSerial.DataBind();

                grdResult.DataSource = new int[] { };
                grdResult.DataBind();

                DateTime _ddate = DateTime.Now;
                dtpDate.Text = _ddate.ToString("dd/MMM/yyyy");

                ucOutScan.adjustmentTypeValue = "-";
                ucOutScan.doc_tp = "ADJ-B";
                EnableDeleteButton();
                ucOutScan.isApprovalSend = false;
                ucOutScan.ToBinCode = null;
                ucOutScan.BinCode = null;

                Session["CSSDOC"] = null;

                //txtDocumentNo.Text = string.Empty;
                txtManualRef.Text = string.Empty;
                txtOtherRef.Text = string.Empty;
                txtRemarks.Text = string.Empty;
                txtUserSeqNo.Text = string.Empty;

                EnableDeleteButton();
                ucOutScan.PageClear();
                ddlSeqNo.SelectedIndex = 0;
                ucOutScan.IsBinTransfer = true;

                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10113))
                {
                    btnSave.Enabled = false;
                    btnSave.OnClientClick = "";
                    btnSave.CssClass = "buttoncolor";
                }
                else
                {
                    btnSave.Enabled = true;
                    btnSave.OnClientClick = "ConfirmSave();";
                    btnSave.CssClass = "buttonUndocolor";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void EnableDeleteButton()
        {
            try
            {
                foreach (GridViewRow hiderowbtn in this.grdItems.Rows)
                {
                    LinkButton _delbutton = (LinkButton)hiderowbtn.FindControl("lbtndelete");

                    _delbutton.Enabled = true;
                    _delbutton.CssClass = "buttonUndocolor";
                    _delbutton.OnClientClick = "ConfirmDelete();";
                }

                foreach (GridViewRow hiderowbtnser in this.grdSerial.Rows)
                {
                    LinkButton _delbuttonser = (LinkButton)hiderowbtnser.FindControl("lbtndeleteserial");

                    _delbuttonser.Enabled = true;
                    _delbuttonser.CssClass = "buttonUndocolor";
                    _delbuttonser.OnClientClick = "ConfirmDeleteSerial();";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private Int32 GenerateNewUserSeqNo()
        {
            Int32 generated_seq = 0;
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "ADJ", 0, Session["UserCompanyCode"].ToString());
            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = "ADJ-B";
            RPH.Tuh_cre_dt = DateTime.Today;
            RPH.Tuh_ischek_itmstus = true;
            RPH.Tuh_ischek_reqqty = true;
            RPH.Tuh_ischek_simitm = true;
            RPH.Tuh_session_id = Session["SessionID"].ToString();
            RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
            RPH.Tuh_usr_id = Session["UserID"].ToString();
            RPH.Tuh_usrseq_no = generated_seq;
            RPH.Tuh_direct = false;
            RPH.Tuh_doc_no = generated_seq.ToString();
            int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
            if (affected_rows == 1)
            {
                txtUserSeqNo.Text = generated_seq.ToString();
                return generated_seq;
            }
            else
            {
                return 0;
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(_receCompany + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes:
                    {
                        paramsText.Append(_SerialSearchType + seperator + Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + ucOutScan.TXTItemCode.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocSubType:
                    {
                        paramsText.Append("ADJ" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SearchBINCodes:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private List<ReptPickSerials> LoadItems(string _seqNo)
        {
            List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
            List<ReptPickItems> _reptItems = new List<ReptPickItems>();
            List<ReptPickSerials> _serList = new List<ReptPickSerials>();

            Int32 user_seq_num = CHNLSVC.Inventory.Get_Scan_SeqNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "ADJ-B", Session["UserID"].ToString(), 0, _seqNo);
            if (user_seq_num == -1)
            {
                user_seq_num = GenerateNewUserSeqNo();
            }

            //Get Serials
            _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "ADJ-B");
            if (_serList != null)
            {
                foreach (ReptPickSerials item in _serList)
                {
                    item.Tus_itm_stus_Desc = getItemStatusDesc(item.Tus_itm_stus);
                    item.Tus_new_status_Desc = getItemStatusDesc(item.Tus_new_status);
                }

                if (_serList != null)
                {
                    var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                    foreach (var itm in _scanItems)
                    {
                        foreach (GridViewRow row in this.grdItems.Rows)
                        {
                            Label _item = (Label)row.FindControl("lblitri_itm_cd");
                            Label _itemline = (Label)row.FindControl("lblitri_line_no");
                            Label _pickqty = (Label)row.FindControl("lblitri_qty");

                            if (itm.Peo.Tus_itm_cd == _item.Text && itm.Peo.Tus_base_itm_line == Convert.ToInt32(Convert.ToDecimal(_itemline.Text)))
                            {
                                _pickqty.Text = Convert.ToDecimal(itm.theCount).ToString();
                            }
                        }
                    }
                    grdSerial.AutoGenerateColumns = false;
                    grdSerial.DataSource = _serList;
                    grdSerial.DataBind();

                    txtFromBin.Text = _serList[0].Tus_bin;
                    txtToBin.Text = _serList[0].Tus_bin_to;

                    ucOutScan.BinCode = _serList[0].Tus_bin;
                    ucOutScan.ToBinCode = _serList[0].Tus_bin_to;
                }
            }
            else
            {
                List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                grdSerial.AutoGenerateColumns = false;
                grdSerial.DataSource = emptyGridList;
                grdSerial.DataBind();

                grdItems.DataSource = new int[] { };
                grdItems.DataBind();
                return emptyGridList;
            }


            var _tbitems =
            from _pickSerials in _serList
            group _pickSerials by new { _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_stus, _pickSerials.Tus_new_status, _pickSerials.Tus_base_itm_line } into itm
            select new { Tus_itm_cd = itm.Key.Tus_itm_cd, Tus_itm_stus = itm.Key.Tus_itm_stus, Tus_new_status = itm.Key.Tus_new_status, itemLine = itm.Key.Tus_base_itm_line, itemqty = itm.Sum(p => p.Tus_qty) };

            // Get Items
            _reptItems = CHNLSVC.Inventory.GetAllScanRequestItemsList(user_seq_num);
            //foreach (ReptPickItems _reptitem in _reptItems)
            //{
            //    InventoryRequestItem _itm = new InventoryRequestItem();
            //    MasterItem _itms = new MasterItem();
            //    _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _reptitem.Tui_req_itm_cd);
            //    _itm.Itri_app_qty = _reptitem.Tui_req_itm_qty;
            //    _itm.Itri_itm_cd = _reptitem.Tui_req_itm_cd;
            //    _itm.Itri_itm_stus = _reptitem.Tui_req_itm_stus;
            //    //_itm.Itri_line_no = _serList.Find(x => x.Tus_itm_cd == _itm.Itri_itm_cd && x.Tus_itm_stus == _itm.Itri_itm_stus).Tus_itm_line;
            //    //_itm.Itri_note = SetNewItemStatus(_serList, _itm.Itri_itm_cd, _itm.Itri_itm_stus);
            //    if (_serList.FindAll(x => x.Tus_base_itm_line.ToString() == _reptitem.Tui_pic_itm_cd).Count > 0)
            //    {
            //        _itm.Itri_note = _serList.FindAll(x => x.Tus_base_itm_line.ToString() == _reptitem.Tui_pic_itm_cd)[0].Tus_new_status;
            //        _itm.Itri_line_no = _serList.Find(x => x.Tus_itm_cd == _itm.Itri_itm_cd && x.Tus_itm_stus == _itm.Itri_itm_stus && x.Tus_new_status == _itm.Itri_note).Tus_base_itm_line;
            //    }
            //    else
            //    {
            //        //if (_itmList.FindAll(x=>x.Itri_itm_cd == _itm.Itri_itm_cd && x.Itri_itm_stus == _itm.Itri_itm_stus && x.Itri_note ==  ))
            //        {

            //        }

            //    }

            //    //if (_itms.Mi_is_ser1 == 1)
            //    //{
            //    //    _itm.Itri_qty = 1;
            //    //}
            //    //else
            //    {
            //        decimal QtyValue = _serList.Where(x => x.Tus_itm_cd == _itm.Itri_itm_cd && x.Tus_itm_stus == _itm.Itri_itm_stus && x.Tus_new_status == _itm.Itri_note).Sum(e => e.Tus_qty);
            //        _itm.Itri_qty = QtyValue;
            //    }

            //    //Set Pick Qty
            //    //_itm.Itri_qty = SetPickQty(_serList, _itm.Itri_itm_cd, _itm.Itri_itm_stus);

            //    //Set New Item Status

            //    _itm.Mi_longdesc = _itms.Mi_longdesc;
            //    _itm.Mi_model = _itms.Mi_model;
            //    _itm.Mi_brand = _itms.Mi_brand;
            //    _itm.Itri_itm_stus_desc = getItemStatusDesc(_itm.Itri_itm_stus);
            //    _itm.Itri_note_desc = getItemStatusDesc(_itm.Itri_note);
            //    //_itm.Itri_unit_price = 0;
            //    //_itm.Itri_note = _reptitem.Tui_pic_itm_stus;

            //    _itmList.Add(_itm);
            //}

            foreach (var item in _tbitems)
            {
                InventoryRequestItem _itm = new InventoryRequestItem();
                MasterItem _itms = new MasterItem();
                _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), item.Tus_itm_cd);
                _itm.Itri_app_qty = item.itemqty;
                _itm.Itri_itm_cd = item.Tus_itm_cd;
                _itm.Itri_itm_stus = item.Tus_itm_stus;
                _itm.Itri_note = item.Tus_new_status;
                _itm.Itri_line_no = item.itemLine;
                //_itm.Itri_line_no = _serList.Find(x => x.Tus_itm_cd == _itm.Itri_itm_cd && x.Tus_itm_stus == _itm.Itri_itm_stus).Tus_itm_line;
                //_itm.Itri_note = SetNewItemStatus(_serList, _itm.Itri_itm_cd, _itm.Itri_itm_stus);
                //if (_serList.FindAll(x => x.Tus_base_itm_line.ToString() == _reptitem.Tui_pic_itm_cd).Count > 0)
                //{
                //    _itm.Itri_note = _serList.FindAll(x => x.Tus_base_itm_line.ToString() == _reptitem.Tui_pic_itm_cd)[0].Tus_new_status;
                //    _itm.Itri_line_no = _serList.Find(x => x.Tus_itm_cd == _itm.Itri_itm_cd && x.Tus_itm_stus == _itm.Itri_itm_stus && x.Tus_new_status == _itm.Itri_note).Tus_base_itm_line;
                //}
                //else
                //{
                //    //if (_itmList.FindAll(x=>x.Itri_itm_cd == _itm.Itri_itm_cd && x.Itri_itm_stus == _itm.Itri_itm_stus && x.Itri_note ==  ))
                //    {

                //    }

                //}

                //if (_itms.Mi_is_ser1 == 1)
                //{
                //    _itm.Itri_qty = 1;
                //}
                //else
                {
                    decimal QtyValue = _serList.Where(x => x.Tus_itm_cd == _itm.Itri_itm_cd && x.Tus_itm_stus == _itm.Itri_itm_stus && x.Tus_new_status == _itm.Itri_note).Sum(e => e.Tus_qty);
                    _itm.Itri_qty = QtyValue;
                }

                //Set Pick Qty
                //_itm.Itri_qty = SetPickQty(_serList, _itm.Itri_itm_cd, _itm.Itri_itm_stus);

                //Set New Item Status

                _itm.Mi_longdesc = _itms.Mi_longdesc;
                _itm.Mi_model = _itms.Mi_model;
                _itm.Mi_brand = _itms.Mi_brand;
                _itm.Itri_itm_stus_desc = getItemStatusDesc(_itm.Itri_itm_stus);
                _itm.Itri_note_desc = getItemStatusDesc(_itm.Itri_note);
                //_itm.Itri_unit_price = 0;
                //_itm.Itri_note = _reptitem.Tui_pic_itm_stus;

                _itmList.Add(_itm);
            }

            ScanItemList = _itmList;
            grdItems.AutoGenerateColumns = false;
            grdItems.DataSource = _itmList;
            grdItems.DataBind();

            ucOutScan.ScanItemList = _itmList;
            ucOutScan.PickSerial = _serList;
            ucOutScan.userSeqNo = _seqNo;
            ViewState["userSeqNo"] = _seqNo;
            return _serList;
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

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        private void Delete_Serials(string _itemCode, string _itemStatus, Int32 _serialID, Int32 baseItemLine)
        {
            try
            {
                MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itemCode);
                if (_masterItem.Mi_is_ser1 == 1)
                {
                    bool result = CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), Convert.ToInt32(_serialID), null, null);
                    bool result1 = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itemCode, _serialID, 1);
                }
                else
                {
                    string err = string.Empty;
                    ReptPickSerials oInItem = new ReptPickSerials();
                    oInItem.Tus_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text);
                    oInItem.Tus_itm_cd = _itemCode;
                    oInItem.Tus_itm_stus = _itemStatus;
                    oInItem.Tus_base_itm_line = baseItemLine;

                    Int32 result1 = CHNLSVC.Inventory.DeletePickSerByItemAndBaseItemLine(oInItem, out err);
                    //CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), _itemCode, _itemStatus);
                }
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        private void ClearAll()
        {
            try
            {
                Clear();
                btnSave.Enabled = true;
                btnSave.CssClass = "buttonUndocolor";
                btnSave.OnClientClick = "ConfirmSave();";

                grdItems.DataSource = null;
                grdSerial.DataSource = null;
                grdItems.DataBind();
                grdSerial.DataBind();
                ViewState["SerialList"] = null;
                ViewState["ScanItemList"] = null;
                ViewState["ITEMCHANGE"] = null;
                ScanItemList = null;
                ucOutScan.GridClear();
                Session["EXCEL"] = null;


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        public bool CheckServerDateTime()
        {
            if (CHNLSVC.Security.GetServerDateTime().Date != DateTime.Now.Date)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Your machine date conflict with the server date.please contact system administrator !!!')", true);
                return false;
            }

            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            TimeZone zone = TimeZone.CurrentTimeZone;
            TimeSpan offset = zone.GetUtcOffset(DateTime.Now);

            string _serverUTC = CHNLSVC.Security.GetServerTimeZoneOffset();
            string _localUTC = offset.ToString();

            if (_serverUTC != _localUTC)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Your machine time zone conflict with the server time zone.please contact system administrator !!!')", true);
                return false;
            }
            return true;
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

                    string msg = "The Inward documents " + _documents.ToString() + " equal or grater than to a this Outward document " + _processDate.Date.ToShortDateString() + " date !!!";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                }
            }
            return _appropriate;
        }

        private void Process()
        {
            string error = string.Empty;
            try
            {
                if (grdItems.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please add items !!!')", true);
                    ucOutScan.TXTItemCode.Focus();
                    return;
                }

                DateTime dt1 = Convert.ToDateTime(dtpDate.Text);
                DateTime dt2 = dt1.Date;

                if (CheckServerDateTime() == false)
                {
                    return;
                }

                if (string.IsNullOrEmpty(txtManualRef.Text))
                {
                    txtManualRef.Text = "N/A";
                }

                ////bool _allowCurrentTrans = false;
                ////if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty.ToUpper().ToString(), this.GlbModuleName, dtpDate, lblH1, dtpDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
                ////{
                ////if (_allowCurrentTrans == true)
                ////{

                if (dt2 != DateTime.Now.Date)
                {
                    dtpDate.Enabled = true;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date not allowed for transaction !!!')", true);
                    dtpDate.Focus();
                    return;
                }

                List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                string documntNo_minus = "";
                string documntNo_plus = "";

                Int32 _userSeqNo = Convert.ToInt32(txtUserSeqNo.Text);

                string isexcelupload = (string)Session["EXCEL"];

                if (string.IsNullOrEmpty(isexcelupload))
                {
                    reptPickSerialsList = LoadItems(_userSeqNo.ToString());
                }

                if (txtFromBin.Text.Trim().ToUpper() == txtToBin.Text.Trim().ToUpper())
                {
                    DisplayMessage("From and to BIN codes cannot be same");
                    return;
                }

                #region Check Duplicate Serials
                if (reptPickSerialsList != null)
                {
                    var _dup = reptPickSerialsList.Where(x => x.Tus_ser_1 != "N/A" && x.Tus_ser_id != 0).Select(y => y.Tus_ser_id).ToList();

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
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Following item serials are duplicating. Please remove the duplicated serials !!!')", true);
                        return;
                    }
                }
                #endregion

                reptPickSerialsList = LoadItems(_userSeqNo.ToString());
                reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "ADJ-B");

                #region Check Serial Scan or not

                if (grdItems == null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No items found !!!')", true);
                    return;
                }
                if (grdItems.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No items found !!!')", true);
                    return;
                }
                foreach (GridViewRow row in this.grdItems.Rows)
                {
                    MasterItem _itms = new MasterItem();

                    Label _item = (Label)row.FindControl("lblitri_itm_cd");
                    Label _pickitemqty = (Label)row.FindControl("lblitri_qty");

                    _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item.Text.Trim());
                    if (_itms.Mi_is_ser1 == 1)
                    {
                        if (Convert.ToDecimal(_pickitemqty.Text) == 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Serial number is not picked for item  " + _itms.Mi_cd + " !!!')", true);
                            return;
                        }
                    }
                }

                #endregion

                #region Check Referance Date and the Doc Date

                if (IsReferancedDocDateAppropriate(reptPickSerialsList, dt2) == false)
                {
                    return;
                }
                #endregion

                InventoryHeader _hdrMinus = new InventoryHeader();
                #region Fill InventoryHeader
                DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
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

                _hdrMinus.Ith_acc_no = "BIN TO BIN";
                _hdrMinus.Ith_anal_1 = "";
                _hdrMinus.Ith_anal_2 = "";
                _hdrMinus.Ith_anal_3 = "";
                _hdrMinus.Ith_anal_4 = "";
                _hdrMinus.Ith_anal_5 = "";
                _hdrMinus.Ith_anal_6 = _userSeqNo;
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
                _hdrMinus.Ith_doc_date = Convert.ToDateTime(dtpDate.Text);
                _hdrMinus.Ith_doc_no = string.Empty;
                _hdrMinus.Ith_doc_tp = "ADJ";
                _hdrMinus.Ith_doc_year = dt2.Year;
                _hdrMinus.Ith_entry_no = txtOtherRef.Text.ToString().Trim();
                _hdrMinus.Ith_entry_tp = "STUS";
                _hdrMinus.Ith_git_close = true;
                _hdrMinus.Ith_git_close_date = DateTime.MinValue;
                _hdrMinus.Ith_git_close_doc = BaseCls.GlbDefaultBin;
                _hdrMinus.Ith_isprinted = false;
                _hdrMinus.Ith_is_manual = false;
                _hdrMinus.Ith_job_no = string.Empty;
                _hdrMinus.Ith_loading_point = string.Empty;
                _hdrMinus.Ith_loading_user = string.Empty;
                _hdrMinus.Ith_loc = Session["UserDefLoca"].ToString();
                _hdrMinus.Ith_manual_ref = txtManualRef.Text.Trim();
                _hdrMinus.Ith_mod_by = Session["UserID"].ToString();
                _hdrMinus.Ith_mod_when = DateTime.Now;
                _hdrMinus.Ith_noofcopies = 0;
                _hdrMinus.Ith_oth_loc = string.Empty;
                _hdrMinus.Ith_oth_docno = "N/A";
                _hdrMinus.Ith_remarks = txtRemarks.Text;
                _hdrMinus.Ith_session_id = Session["SessionID"].ToString();
                _hdrMinus.Ith_stus = "A";
                _hdrMinus.Ith_sub_tp = "BTB";
                _hdrMinus.Ith_vehi_no = string.Empty;
                #endregion
                MasterAutoNumber _autonoMinus = new MasterAutoNumber();
                #region Fill MasterAutoNumber
                _autonoMinus.Aut_cate_cd = Session["UserDefLoca"].ToString();
                _autonoMinus.Aut_cate_tp = "LOC";
                _autonoMinus.Aut_direction = null;
                _autonoMinus.Aut_modify_dt = null;
                _autonoMinus.Aut_moduleid = "BTB";
                _autonoMinus.Aut_number = 5;
                _autonoMinus.Aut_start_char = "BTB";
                _autonoMinus.Aut_year = null;
                #endregion
                InventoryHeader _hdrPlus = new InventoryHeader();
                #region Fill InventoryHeader
                _hdrPlus.Ith_channel = _hdrMinus.Ith_channel;
                _hdrPlus.Ith_sbu = _hdrMinus.Ith_sbu;
                _hdrPlus.Ith_acc_no = "BIN TO BIN";
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
                _hdrPlus.Ith_cate_tp = "STUS";
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
                _hdrPlus.Ith_doc_date = Convert.ToDateTime(dtpDate.Text);
                _hdrPlus.Ith_doc_no = string.Empty;
                _hdrPlus.Ith_doc_tp = "ADJ";
                _hdrPlus.Ith_doc_year = dt2.Year;
                _hdrPlus.Ith_entry_no = txtOtherRef.Text.ToString().Trim();
                _hdrPlus.Ith_entry_tp = "STUS";
                _hdrPlus.Ith_git_close = true;
                _hdrPlus.Ith_git_close_date = DateTime.MinValue;
                _hdrPlus.Ith_git_close_doc = string.Empty;
                _hdrPlus.Ith_isprinted = false;
                _hdrPlus.Ith_is_manual = false;
                _hdrPlus.Ith_job_no = string.Empty;
                _hdrPlus.Ith_loading_point = string.Empty;
                _hdrPlus.Ith_loading_user = string.Empty;
                _hdrPlus.Ith_loc = Session["UserDefLoca"].ToString();
                _hdrPlus.Ith_manual_ref = txtManualRef.Text.Trim();
                _hdrPlus.Ith_mod_by = Session["UserID"].ToString();
                _hdrPlus.Ith_mod_when = DateTime.Now;
                _hdrPlus.Ith_noofcopies = 0;
                _hdrPlus.Ith_oth_loc = string.Empty;
                _hdrPlus.Ith_oth_docno = "N/A";
                _hdrPlus.Ith_remarks = txtRemarks.Text;
                _hdrPlus.Ith_session_id = Session["SessionID"].ToString();
                _hdrPlus.Ith_stus = "A";
                _hdrPlus.Ith_sub_tp = "BTB";
                _hdrPlus.Ith_vehi_no = string.Empty;
                #endregion
                MasterAutoNumber _autonoPlus = new MasterAutoNumber();
                #region Fill MasterAutoNumber
                _autonoPlus.Aut_cate_cd = Session["UserDefLoca"].ToString();
                _autonoPlus.Aut_cate_tp = "LOC";
                _autonoPlus.Aut_direction = null;
                _autonoPlus.Aut_modify_dt = null;
                _autonoPlus.Aut_moduleid = "BTB";
                _autonoPlus.Aut_number = 5;
                _autonoPlus.Aut_start_char = "BTB";
                _autonoPlus.Aut_year = null;
                #endregion

                #region Status Change Adj- >>>> Adj+
                DataTable dtTemp = GlobalMethod.ToDataTable(ScanItemList);

                //Set costValues temp method

                error = CHNLSVC.Inventory.InventoryStatusChangeEntry(_hdrMinus, _hdrPlus, reptPickSerialsList, reptPickSubSerialsList, _autonoMinus, _autonoPlus, ScanItemList, out documntNo_minus, out documntNo_plus, true,false,true);

                if (string.IsNullOrEmpty(error))
                {
                    string okmsg = "Successfully Saved! document no (-ADJ) : " + documntNo_minus + " and document no (+ADJ) :" + documntNo_plus;

                    DisplayMessage(okmsg, 3);

                    clear_save();
                    //ClearAll();
                }
                else
                {
                    DisplayMessage(error);
                }

                #endregion
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message);
            }
        }

        private void SaveListInExcel()
        {
            try
            {
                Int32 seqno = 0;

                if (grdItems.Rows.Count == 0)
                {
                    return;
                }

                if (string.IsNullOrEmpty(txtUserSeqNo.Text.Trim()))
                {
                    seqno = GenerateNewUserSeqNo();
                    txtUserSeqNo.Text = seqno.ToString();
                }

                DataTable dt = (DataTable)ViewState["ITEMCHANGE"];

                if (dt.Rows.Count > 0)
                {
                    ScanItemListExcel = GlobalMethod.ToGenericList<InventoryRequestItem>(dt, InventoryRequestItem.ConverterForCommonOutCopy);
                }

                List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                foreach (InventoryRequestItem _addedItem in ScanItemListExcel)
                {
                    ReptPickItems _reptitm = new ReptPickItems();
                    _reptitm.Tui_usrseq_no = Convert.ToInt32(seqno);
                    _reptitm.Tui_req_itm_qty = _addedItem.Itri_app_qty;
                    _reptitm.Tui_req_itm_cd = _addedItem.Itri_itm_cd;
                    _reptitm.Tui_req_itm_stus = _addedItem.Itri_itm_stus;
                    _reptitm.Tui_pic_itm_cd = Convert.ToString(_addedItem.Itri_line_no);
                    _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                    _reptitm.Tui_pic_itm_qty = _addedItem.Itri_qty;
                    _reptitm.Tui_sup = _addedItem.Itri_mitm_cd;
                    _saveonly.Add(_reptitm);
                }

                CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32(seqno), string.Empty, string.Empty, 0, 3);

                CHNLSVC.Inventory.SavePickedItems(_saveonly);

                //AddItem(txtItemCode.Text, _itms.Mi_std_cost.ToString(), ddlStatus.SelectedItem.Value, txtQty.Text, (ViewState["userSeqNo"].ToString()), txtSerialI.Text);

                //AddSerials(txtItemCode.Text, txtSerialI.Text, (ViewState["userSeqNo"].ToString()));

                //foreach (DataRow ddr in dt.Rows)
                //{
                //    ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                //    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                //    _reptPickSerial_.Tus_usrseq_no = Convert.ToInt32(seqno);
                //    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                //    _reptPickSerial_.Tus_base_doc_no = "";
                //    _reptPickSerial_.Tus_base_itm_line = 0;
                //    _reptPickSerial_.Tus_itm_desc = ddr["mi_longdesc"].ToString();
                //    _reptPickSerial_.Tus_itm_model = ddr["mi_model"].ToString();
                //    _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                //    _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                //    _reptPickSerial_.Tus_bin = BaseCls.GlbDefaultBin;
                //    _reptPickSerial_.Tus_itm_cd =  ddr["itri_itm_cd"].ToString();
                //    _reptPickSerial_.Tus_itm_stus = ddr["itri_itm_stus"].ToString();
                //    _reptPickSerial_.Tus_qty = Convert.ToDecimal(ddr["itri_app_qty"].ToString());
                //    _reptPickSerial_.Tus_ser_1 = ddr["itri_itm_stus"].ToString();
                //    _reptPickSerial_.Tus_ser_2 = ddr["itri_itm_stus"].ToString();
                //    _reptPickSerial_.Tus_ser_3 = ddr["itri_itm_stus"].ToString();
                //    _reptPickSerial_.Tus_ser_4 = "N/A";
                //    _reptPickSerial_.Tus_ser_id = 0;
                //    _reptPickSerial_.Tus_serial_id = "0";
                //    _reptPickSerial_.Tus_unit_cost = 0;
                //    _reptPickSerial_.Tus_unit_price = 0;
                //    _reptPickSerial_.Tus_unit_price = 0;
                //    _reptPickSerial_.Tus_new_itm_cd = txtItemToBeChange.Text;
                //    _reptPickSerial_.Tus_new_status = ddlStatusToBeChange.SelectedItem.Value;
                //    Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null); 
                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        private void Import_To_Grid(string FilePath, string Extension, string isHDR)
        {
            try
            {
                string conStr = "";
                switch (Extension)
                {
                    case ".xls":
                        conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx":
                        conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }
                conStr = String.Format(conStr, FilePath, isHDR);
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                cmdExcel.Connection = connExcel;
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);

                DataTable dtitemsexcel = new DataTable();
                dtitemsexcel.Columns.AddRange(new DataColumn[9] { new DataColumn("itri_itm_cd"), new DataColumn("mi_longdesc"), new DataColumn("mi_model"), new DataColumn("itri_itm_stus"), new DataColumn("itri_note"), new DataColumn("itri_unit_price"), new DataColumn("itri_app_qty"), new DataColumn("itri_qty"), new DataColumn("itri_line_no") });

                Int32 seqno = GenerateNewUserSeqNo();
                txtUserSeqNo.Text = seqno.ToString();

                foreach (DataRow item in dt.Rows)
                {
                    string _item = item["Item Code"].ToString();

                    List<string> duplicte = new List<string>();

                    foreach (GridViewRow grv in grdItems.Rows)
                    {
                        Label _itemlabel = (Label)grv.FindControl("lblitri_itm_cd");
                        duplicte.Add(_itemlabel.Text);
                    }

                    bool hasCar = duplicte.Contains(_item);

                    if (hasCar == true)
                    {
                        continue;
                    }


                    MasterItem _itemdetail = new MasterItem();
                    _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                    string _description = _itemdetail.Mi_longdesc;
                    string _model = _itemdetail.Mi_model;
                    decimal _price = _itemdetail.Mi_itmtot_cost;
                    int index = dt.Rows.IndexOf(item);

                    DataTable dtstatus = CHNLSVC.Sales.GetItemStatusVal(item["Item Status"].ToString());
                    DataTable dtstatusnew = CHNLSVC.Sales.GetItemStatusVal(item["New Item Status"].ToString());

                    string _itemval = string.Empty;
                    string _itemvalnew = string.Empty;

                    foreach (DataRow itemval in dtstatus.Rows)
                    {
                        _itemval = itemval["MIS_CD"].ToString();
                    }

                    foreach (DataRow itemvalnew in dtstatusnew.Rows)
                    {
                        _itemvalnew = itemvalnew["MIS_CD"].ToString();
                    }

                    dtitemsexcel.Rows.Add(_item, _description, _model, _itemval, _itemvalnew, _price, item["Qty"].ToString(), item["Qty"].ToString(), index + 1);

                    AddItem(_item, _price.ToString(), _itemval, item["Qty"].ToString(), seqno.ToString(), item["Serial No"].ToString(), _itemvalnew, item["New Item Code"].ToString(), index + 1);
                }


                DataTable dtitemsserexcel = new DataTable();
                dtitemsserexcel.Columns.AddRange(new DataColumn[13] { new DataColumn("tus_itm_cd"), new DataColumn("tus_itm_model"), new DataColumn("tus_itm_stus"), new DataColumn("tus_new_status"), new DataColumn("tus_qty"), new DataColumn("tus_ser_1"), new DataColumn("tus_ser_2"), new DataColumn("tus_ser_3"), new DataColumn("tus_bin"), new DataColumn("tus_new_remarks"), new DataColumn("tus_ser_id"), new DataColumn("tus_base_doc_no"), new DataColumn("tus_base_itm_line") });

                foreach (DataRow item in dt.Rows)
                {
                    string _item = item["Item Code"].ToString();

                    List<string> duplicteser = new List<string>();

                    foreach (GridViewRow grvser in grdSerial.Rows)
                    {
                        Label _itemlabelser = (Label)grvser.FindControl("lblitmgrd2");
                        duplicteser.Add(_itemlabelser.Text);
                    }

                    bool hasCarser = duplicteser.Contains(_item);

                    if (hasCarser == true)
                    {
                        continue;
                    }

                    MasterItem _itemdetail = new MasterItem();
                    _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                    string _description = _itemdetail.Mi_longdesc;
                    string _model = _itemdetail.Mi_model;
                    decimal _price = _itemdetail.Mi_itmtot_cost;
                    int index = dt.Rows.IndexOf(item);

                    DataTable dtstatus = CHNLSVC.Sales.GetItemStatusVal(item["Item Status"].ToString());
                    DataTable dtstatusnew = CHNLSVC.Sales.GetItemStatusVal(item["New Item Status"].ToString());

                    string _itemval = string.Empty;
                    string _itemvalnew = string.Empty;

                    foreach (DataRow itemval in dtstatus.Rows)
                    {
                        _itemval = itemval["MIS_CD"].ToString();
                    }

                    foreach (DataRow itemvalnew in dtstatusnew.Rows)
                    {
                        _itemvalnew = itemvalnew["MIS_CD"].ToString();
                    }

                    List<ReptPickSerials> Tempserial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, string.Empty, string.Empty);
                    Int32 _serialid = 0;

                    foreach (ReptPickSerials serial in Tempserial_list)
                    {
                        if (serial.Tus_ser_1 == item["Serial No"].ToString())
                        {
                            _serialid = serial.Tus_ser_id;
                        }
                    }

                    dtitemsserexcel.Rows.Add(_item, _model, _itemval, _itemvalnew, item["Qty"].ToString(), item["Serial No"].ToString(), "", "", item["Bin"].ToString(), "", _serialid, seqno, index + 1);
                    AddSerials(_item, item["Serial No"].ToString(), seqno.ToString(), _itemvalnew, item["New Item Code"].ToString(), item["Bin"].ToString(), index + 1, _itemval, Convert.ToDecimal(item["Qty"].ToString()));
                }

                connExcel.Close();

                DataTable dtcopy = new DataTable();
                DataTable dtoriginal = new DataTable();
                DataTable dtvwstatetbl = ViewState["ITEMCHANGE"] as DataTable;

                if ((ViewState["ITEMCHANGE"] != null) && (dtvwstatetbl.Rows.Count != 0))
                {
                    dtcopy = ViewState["ITEMCHANGE"] as DataTable;
                    dtcopy.Merge(dtitemsexcel, true, MissingSchemaAction.Ignore);
                    ViewState["ITEMCHANGE"] = dtcopy;

                    grdItems.DataSource = dtcopy;
                    grdItems.DataBind();
                }
                else
                {
                    dtoriginal = dtitemsexcel.Copy();
                    ViewState["ITEMCHANGE"] = dtoriginal;

                    grdItems.DataSource = dtoriginal;
                    grdItems.DataBind();
                }

                DataTable dtcopySerial = new DataTable();
                DataTable dtoriginalSerial = new DataTable();
                DataTable dtvwstatetblSerial = ViewState["ITEMCHANGESER"] as DataTable;

                if ((ViewState["ITEMCHANGESER"] != null) && (dtvwstatetblSerial.Rows.Count != 0))
                {
                    dtcopySerial = ViewState["ITEMCHANGESER"] as DataTable;
                    dtcopySerial.Merge(dtitemsserexcel, true, MissingSchemaAction.Ignore);
                    ViewState["ITEMCHANGESER"] = dtcopySerial;

                    grdSerial.DataSource = dtcopySerial;
                    grdSerial.DataBind();
                }
                else
                {
                    dtoriginalSerial = dtitemsserexcel.Copy();
                    ViewState["ITEMCHANGESER"] = dtoriginalSerial;

                    grdSerial.DataSource = dtoriginalSerial;
                    grdSerial.DataBind();
                }
                Session["EXCEL"] = "Yes";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void AddItem(string _item, string _UnitCost, string _status, string _qty, string _UserSeqNo, string _serial, string _newstatus, string _newitem, Int32 LineNo)
        {
            try
            {
                MasterItem _itms = new MasterItem();
                _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                InventoryRequestItem _itm = new InventoryRequestItem();
                ScanItemList = ViewState["ScanItemList"] as List<InventoryRequestItem>;

                if (ScanItemList != null)
                {
                    if (ScanItemList.Count > 0)
                    {
                        _itm.Itri_itm_cd = _item;
                        _itm.Itri_itm_stus = _status;
                        _itm.Itri_line_no = Convert.ToInt32(LineNo);
                        _itm.Itri_qty = Convert.ToDecimal(_qty);
                        _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                        _itm.Itri_bqty = Convert.ToDecimal(_qty);

                        _itm.Mi_longdesc = _itms.Mi_longdesc;
                        _itm.Mi_model = _itms.Mi_model;
                        _itm.Mi_brand = _itms.Mi_brand;
                        _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);

                        _itm.Itri_note = _newstatus;
                        _itm.Itri_mitm_cd = _newitem;

                        ScanItemList.Add(_itm);
                    }
                    else
                    {
                        _itm.Itri_itm_cd = _item;
                        _itm.Itri_itm_stus = _status;
                        _itm.Itri_line_no = Convert.ToInt32(LineNo);
                        _itm.Itri_qty = Convert.ToDecimal(_qty);

                        _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                        _itm.Itri_bqty = Convert.ToDecimal(_qty);

                        _itm.Mi_longdesc = _itms.Mi_longdesc;
                        _itm.Mi_model = _itms.Mi_model;
                        _itm.Mi_brand = _itms.Mi_brand;
                        _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);

                        _itm.Itri_note = _newstatus;
                        _itm.Itri_mitm_cd = _newitem;

                        ScanItemList = new List<InventoryRequestItem>();
                        ScanItemList.Add(_itm);
                    }
                }
                else
                {
                    _itm.Itri_itm_cd = _item;
                    _itm.Itri_itm_stus = _status;
                    _itm.Itri_line_no = Convert.ToInt32(LineNo);
                    _itm.Itri_qty = Convert.ToDecimal(_qty);

                    _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                    _itm.Itri_bqty = Convert.ToDecimal(_qty);

                    _itm.Mi_longdesc = _itms.Mi_longdesc;
                    _itm.Mi_model = _itms.Mi_model;
                    _itm.Mi_brand = _itms.Mi_brand;
                    _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);

                    _itm.Itri_note = _newstatus;
                    _itm.Itri_mitm_cd = _newitem;

                    ScanItemList = new List<InventoryRequestItem>();
                    ScanItemList.Add(_itm);
                }

                List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                foreach (InventoryRequestItem _addedItem in ScanItemList)
                {
                    ReptPickItems _reptitm = new ReptPickItems();
                    _reptitm.Tui_usrseq_no = Convert.ToInt32(_UserSeqNo);
                    _reptitm.Tui_req_itm_qty = _addedItem.Itri_app_qty;
                    _reptitm.Tui_req_itm_cd = _addedItem.Itri_itm_cd;
                    _reptitm.Tui_req_itm_stus = _addedItem.Itri_itm_stus;
                    _reptitm.Tui_pic_itm_cd = Convert.ToString(_addedItem.Itri_itm_cd);
                    _reptitm.Tui_pic_itm_stus = _addedItem.Itri_itm_stus;
                    _reptitm.Tui_pic_itm_qty = _addedItem.Itri_qty;
                    _saveonly.Add(_reptitm);
                }
                CHNLSVC.Inventory.SavePickedItems(_saveonly);
                ViewState["ScanItemList"] = ScanItemList;
                ItemList = ScanItemList;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void AddSerials(string _item, string _Serial, string _Seqno, string _newstatus, string _newitem, string _bin, Int32 _lineno, string _curstus, decimal _qty)
        {
            Int32 generated_seq = -1;
            MasterItem msitem = new MasterItem();
            msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
            List<ReptPickSerials> Tempserial_list = new List<ReptPickSerials>();

            if (msitem.Mi_is_ser1 == 1)
            {
                Tempserial_list = new List<ReptPickSerials>();
                Tempserial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, string.Empty, string.Empty);

                Tempserial_list = Tempserial_list.Where(x => x.Tus_ser_1 == _Serial).ToList();
            }
            else
            {
                ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                Tempserial_list = new List<ReptPickSerials>();
                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                _reptPickSerial_.Tus_usrseq_no = generated_seq;
                _reptPickSerial_.Tus_seq_no = generated_seq;
                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                _reptPickSerial_.Tus_doc_no = txtUserSeqNo.Text.Trim();
                _reptPickSerial_.Tus_base_itm_line = _lineno;
                _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                _reptPickSerial_.Tus_bin = _bin;
                _reptPickSerial_.Tus_itm_cd = _item;
                _reptPickSerial_.Tus_itm_stus = _curstus;
                _reptPickSerial_.Tus_qty = Convert.ToDecimal(_qty);
                _reptPickSerial_.Tus_ser_1 = _Serial;
                _reptPickSerial_.Tus_ser_2 = "N/A";
                _reptPickSerial_.Tus_ser_3 = "N/A";
                _reptPickSerial_.Tus_ser_4 = "N/A";
                _reptPickSerial_.Tus_ser_id = 0;
                _reptPickSerial_.Tus_serial_id = "0";
                _reptPickSerial_.Tus_unit_cost = 0;
                _reptPickSerial_.Tus_unit_price = 0;
                _reptPickSerial_.Tus_unit_price = 0;
                _reptPickSerial_.Tus_job_no = "";
                _reptPickSerial_.Tus_job_line = _lineno;
                _reptPickSerial_.Tus_new_status = _newstatus;
                Tempserial_list.Add(_reptPickSerial_);
            }

            ScanItemList = ViewState["ScanItemList"] as List<InventoryRequestItem>;

            //var serialLine = (from _ls in ScanItemList
            //                  where _ls.Itri_itm_cd == _item
            //                  select _ls.Itri_line_no).Max();


            if (_Serial == "N/A" || _Serial == "")
            {

            }
            Int32 user_seq_num = -1;
            foreach (ReptPickSerials serial in Tempserial_list)
            {
                #region PRN
                user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("ADJ-B", Session["UserCompanyCode"].ToString(), _Seqno, 0);
                if (user_seq_num != -1)
                {
                    generated_seq = user_seq_num;
                }
                else
                {
                    generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "ADJ-B", 1, Session["UserCompanyCode"].ToString());

                    ReptPickHeader RPH = new ReptPickHeader();
                    RPH.Tuh_doc_tp = "ADJ-B";
                    RPH.Tuh_cre_dt = DateTime.Today;
                    RPH.Tuh_ischek_itmstus = true;
                    RPH.Tuh_ischek_reqqty = true;
                    RPH.Tuh_ischek_simitm = true;
                    RPH.Tuh_session_id = Session["SessionID"].ToString();
                    RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    RPH.Tuh_usr_id = Session["UserID"].ToString();
                    RPH.Tuh_usrseq_no = generated_seq;

                    RPH.Tuh_direct = false;
                    RPH.Tuh_doc_no = _Seqno;
                    int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
                }

                if (msitem.Mi_is_ser1 != -1)
                {
                    int rowCount = 0;


                    ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), serial.Tus_bin, serial.Tus_itm_cd, Convert.ToInt32(serial.Tus_ser_id));

                    Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), serial.Tus_itm_cd, Convert.ToInt32(serial.Tus_ser_id), -1);

                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_usrseq_no = generated_seq;
                    _reptPickSerial_.Tus_seq_no = generated_seq;
                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                    _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                    _reptPickSerial_.Tus_doc_no = txtUserSeqNo.Text.Trim();
                    _reptPickSerial_.Tus_itm_cd = _item;
                    _reptPickSerial_.Tus_base_doc_no = _Seqno;
                    _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(_lineno);//Convert.ToInt32(serial.Tus_itm_line);
                    _reptPickSerial_.Tus_itm_line = Convert.ToInt32(_lineno);//Convert.ToInt32(serial.Tus_itm_line);
                    _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                    _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                    _reptPickSerial_.Tus_job_no = serial.Tus_job_no;//JobNo;
                    _reptPickSerial_.Tus_job_line = serial.Tus_job_line;
                    _reptPickSerial_.Tus_bin = _bin;
                    _reptPickSerial_.Tus_itm_stus = serial.Tus_itm_stus;
                    _reptPickSerial_.Tus_ser_1 = _Serial;
                    _reptPickSerial_.Tus_ser_2 = "N/A";
                    _reptPickSerial_.Tus_ser_3 = "N/A";
                    _reptPickSerial_.Tus_ser_4 = "N/A";
                    _reptPickSerial_.Tus_orig_grncom = Session["UserCompanyCode"].ToString();
                    _reptPickSerial_.Tus_session_id = Session["SessionID"].ToString();

                    _reptPickSerial_.Tus_new_itm_cd = _newitem;//newitemCode;
                    _reptPickSerial_.Tus_new_status = _newstatus;//newItemStatus;       

                    Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);

                    rowCount++;
                }
                else
                {
                    ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_usrseq_no = generated_seq;
                    _reptPickSerial_.Tus_seq_no = generated_seq;
                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_base_doc_no = _Seqno;
                    _reptPickSerial_.Tus_doc_no = txtUserSeqNo.Text.Trim();
                    _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(_lineno); //Convert.ToInt32(serial.Tus_itm_line);
                    _reptPickSerial_.Tus_itm_line = Convert.ToInt32(_lineno);//Convert.ToInt32(serial.Tus_itm_line);
                    _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                    _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                    _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                    _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                    _reptPickSerial_.Tus_bin = _bin;
                    _reptPickSerial_.Tus_itm_cd = serial.Tus_itm_cd;
                    _reptPickSerial_.Tus_itm_stus = serial.Tus_itm_stus;
                    _reptPickSerial_.Tus_qty = Convert.ToDecimal(serial.Tus_qty);
                    _reptPickSerial_.Tus_ser_1 = _Serial;
                    _reptPickSerial_.Tus_ser_2 = "N/A";
                    _reptPickSerial_.Tus_ser_3 = "N/A";
                    _reptPickSerial_.Tus_ser_4 = "N/A";
                    _reptPickSerial_.Tus_ser_id = 0;
                    _reptPickSerial_.Tus_serial_id = "0";
                    _reptPickSerial_.Tus_unit_cost = 0;
                    _reptPickSerial_.Tus_unit_price = 0;
                    _reptPickSerial_.Tus_unit_price = 0;
                    _reptPickSerial_.Tus_job_no = serial.Tus_job_no;
                    _reptPickSerial_.Tus_job_line = serial.Tus_job_line;
                    _reptPickSerial_.Tus_session_id = Session["SessionID"].ToString();

                    _reptPickSerial_.Tus_new_itm_cd = _newitem;//newitemCode;
                    _reptPickSerial_.Tus_new_status = _newstatus;//newItemStatus;

                    Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                }

                #endregion
            }
            ViewState["SerialList"] = Tempserial_list;
        }

        private void loadSequences()
        {
            SeqNumList = CHNLSVC.Inventory.Get_User_Seq_Batch(Session["UserID"].ToString(), "ADJ-B", 0, Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
            ddlSeqNo.DataSource = SeqNumList;
            ddlSeqNo.DataBind();
        }

        private decimal SetPickQty(List<ReptPickSerials> _serList, String ItemCode, String ItemStatus)
        {
            decimal PickQty = 0;

            if (_serList.FindAll(x => x.Tus_itm_cd == ItemCode && x.Tus_itm_stus == ItemStatus).Count > 0)
            {
                PickQty = _serList.FindAll(x => x.Tus_itm_cd == ItemCode && x.Tus_itm_stus == ItemStatus).Count;
            }

            return PickQty;
        }

        private string SetNewItemStatus(List<ReptPickSerials> _serList, String ItemCode, String ItemStatus)
        {
            string NewItemStatus = string.Empty;

            if (_serList.FindAll(x => x.Tus_itm_cd == ItemCode && x.Tus_itm_stus == ItemStatus).Count > 0)
            {
                NewItemStatus = _serList.FindAll(x => x.Tus_itm_cd == ItemCode && x.Tus_itm_stus == ItemStatus)[0].Tus_new_status;
            }

            return NewItemStatus;
        }

        private void selDatePanal(bool isEnable)
        {
            //if (isEnable)
            //{
            //    pnlSearchByDate.Enabled = true;
            //    lbtnTDate.CssClass = "buttonUndocolor";
            //    lbtnFDate.CssClass = "buttonUndocolor";
            //    CalendarExtender1.Enabled = true;
            //    CalendarExtender3.Enabled = true;
            //    lbtnDateS.CssClass = "buttonUndocolor";
            //}
            //else
            //{
            //    pnlSearchByDate.Enabled = false;
            //    lbtnTDate.CssClass = "buttoncolor";
            //    lbtnFDate.CssClass = "buttoncolor";
            //    CalendarExtender1.Enabled = false;
            //    CalendarExtender3.Enabled = false;
            //    lbtnDateS.CssClass = "buttoncolor";
            //}
        }


        #endregion

        private void clear_save()
        {
            grdItems.DataSource = new int[] { };
            grdItems.DataBind();

            grdSerial.DataSource = new int[] { };
            grdSerial.DataBind();

            grdResult.DataSource = new int[] { };
            grdResult.DataBind();

            DateTime _ddate = DateTime.Now;
            dtpDate.Text = _ddate.ToString("dd/MMM/yyyy");

            txtManualRef.Text = string.Empty;
            txtOtherRef.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtUserSeqNo.Text = string.Empty;

            txtToBin.Text = "";
            txtFromBin.Text = "";

            ucOutScan.adjustmentTypeValue = "-";
            ucOutScan.doc_tp = "ADJ-B";
            EnableDeleteButton();
            ucOutScan.isApprovalSend = false;
            ucOutScan.ToBinCode = null;
            ucOutScan.BinCode = null;

            Session["CSSDOC"] = null;

            //txtDocumentNo.Text = string.Empty;
            txtManualRef.Text = string.Empty;
            txtOtherRef.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtUserSeqNo.Text = string.Empty;

            EnableDeleteButton();
            ucOutScan.PageClear();
            ddlSeqNo.SelectedIndex = 0;
            ucOutScan.IsBinTransfer = true;

        }

        private void loadItemStatus()
        {
            List<MasterItemStatus> oMasterItemStatuss = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
            Session["ItemStatus"] = oMasterItemStatuss;
        }

        private string getItemStatusDesc(string stis)
        {
            if (!string.IsNullOrEmpty(stis))
            {
                List<MasterItemStatus> oStatuss = (List<MasterItemStatus>)Session["ItemStatus"];
                stis = oStatuss.Find(x => x.Mis_cd == stis).Mis_desc;
            }
            return stis;
        }


    }
}