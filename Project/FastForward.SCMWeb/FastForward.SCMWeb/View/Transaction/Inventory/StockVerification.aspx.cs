using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
using FF.BusinessObjects.InventoryNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Inventory
{
    public partial class StockVerification : Base
    {
        protected List<MasterItemStatus> oMasterItemStatuss { get { return (List<MasterItemStatus>)Session["oMasterItemStatuss"]; } set { Session["oMasterItemStatuss"] = value; } }
        protected PhysicalStockVerificationHdr _PhysicalStockVerificationHdr { get { return (PhysicalStockVerificationHdr)Session["_PhysicalStockVerificationHdr"]; } set { Session["_PhysicalStockVerificationHdr"] = value; } }
        protected List<PhsicalStockVerificationMain> _PhsicalStockVerificationMain { get { return (List<PhsicalStockVerificationMain>)Session["_PhsicalStockVerificationMain"]; } set { Session["_PhsicalStockVerificationMain"] = value; } }
        protected List<ReptPickSerials> _serList { get { return (List<ReptPickSerials>)Session["_serList"]; } set { Session["_serList"] = value; } }
        protected ReptPickHeader _ReptPickHeader { get { return (ReptPickHeader)Session["_ReptPickHeader"]; } set { Session["_ReptPickHeader"] = value; } }
        protected MasterItem _Mstitem { get { return (MasterItem)Session["_Mstitem"]; } set { Session["_Mstitem"] = value; } }
        protected Int32 user_seq_num { get { return (Int32)Session["user_seq_num"]; } set { Session["user_seq_num"] = value; } }
        protected Int32 _lineNo { get { return (Int32)Session["_lineNo"]; } set { Session["_lineNo"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageClear();
            }
        }

        private void PageClear()
        {
            _serList = new List<ReptPickSerials>();
            PopulateLoadingBays();
            oMasterItemStatuss = new List<MasterItemStatus>();
            loadItemStatus();
            user_seq_num = 0;
            _lineNo = 0;
            DateTime orddate = DateTime.Now;
            txtDate.Text = orddate.ToString("dd/MMM/yyyy");
            txtfrmdate.Text = orddate.ToString("dd/MMM/yyyy");
            txttodate.Text = orddate.ToString("dd/MMM/yyyy");
            txtFDate.Text = orddate.ToString("dd/MMM/yyyy");
            txtTDate.Text = orddate.ToString("dd/MMM/yyyy");
            txtdoc.Text = string.Empty;
            txtnosubjob.Text = string.Empty;
            txtremark.Text = string.Empty;
            txtloc.Text = string.Empty;
            txtserial.Text = string.Empty;
            txtqty.Text = string.Empty;
            txtitem.Text = string.Empty;
            lblstatus.Text = string.Empty;
            lblscanQty.Text = string.Empty;
            lblItemDescription.Text = string.Empty;
            lblItemModel.Text = string.Empty;
            lblItemBrand.Text = string.Empty;
            grdsubjob.DataSource = new int[] { };
            grdsubjob.DataBind();
            grdpickserial.DataSource = new int[] { };
            grdpickserial.DataBind();
            ddlSUBJOB.DataSource = new int[] { };
            ddlSUBJOB.DataBind();
            _PhysicalStockVerificationHdr = new PhysicalStockVerificationHdr();
            _PhsicalStockVerificationMain = new List<PhsicalStockVerificationMain>();
            _ReptPickHeader = new ReptPickHeader();
            _serList = new List<ReptPickSerials>();
            _Mstitem = new MasterItem();

            txtloc.Enabled = true;
            LinkButton2.Visible = true;

            MasterLocationNew _objloc = new MasterLocationNew();
            _objloc.Ml_loc_cd = Session["UserDefLoca"].ToString();
            _objloc.Ml_act = 1;
            List<MasterLocationNew> _LOC = CHNLSVC.General.GetMasterLocations(_objloc);
            if (_LOC.Count > 0)
            {
                int _isserialMaintan = _LOC.First().Ml_is_serial;
                if (_isserialMaintan == 0)
                {
                    Session["_isserialMaintan"] = false;

                }
                else
                {
                    Session["_isserialMaintan"] = true;
                }

                int _isPDA = _LOC.First().Ml_is_pda;
                if (_isPDA == 1)
                {
                    Button1.Visible = true;
                    Session["WAREHOUSE_COM"] = _LOC.First().Ml_wh_com;
                    Session["WAREHOUSE_LOC"] = _LOC.First().Ml_wh_cd;
                }
                else
                {
                    Button1.Visible = false;
                }
            }
        }



        protected void lbtnclear_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "Yes")
            {
                try
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
                catch (Exception ex)
                {
                    //divalert.Visible = true;
                    DisplayMessage(ex.Message, 4);
                }
            }
        }

        private MasterAutoNumber GenerateMasterAutoNumber()
        {
            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_tp = "LOC";
            masterAuto.Aut_cate_cd = txtloc.Text.ToUpper().Trim();  // string.IsNullOrEmpty(Session["UserDefLoca"].ToString()) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "MTJO";
            masterAuto.Aut_number = 0;
            masterAuto.Aut_start_char = "MTJO";
            masterAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
            return masterAuto;
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
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtloc.Text.ToUpper().Trim() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        #region Modalpopup
        protected void btnClose_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string ID = grdResult.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "Customer")
            {

            }
            if (lblvalue.Text == "location")
            {
                txtloc.Text = grdResult.SelectedRow.Cells[1].Text;
            }
            if (lblvalue.Text == "Item")
            {
                txtitem.Text = grdResult.SelectedRow.Cells[1].Text;
                txtItem_TextChanged(null, null);
            }
            lblvalue.Text = "";
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "kit")
            {
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
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
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
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
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

        #endregion
        private void SaveProjectPlane()
        {
            int rowsAffected = 0;
            string _docNo = string.Empty;
            if (_PhysicalStockVerificationHdr == null)
            {
                _PhysicalStockVerificationHdr = new PhysicalStockVerificationHdr();
            }

            _PhysicalStockVerificationHdr.AUSH_COM = Session["UserCompanyCode"].ToString();
            _PhysicalStockVerificationHdr.AUSH_CRE_BY = Session["UserID"].ToString();
            _PhysicalStockVerificationHdr.AUSH_DT = Convert.ToDateTime(txtDate.Text);
            _PhysicalStockVerificationHdr.AUSH_FRM_DT = Convert.ToDateTime(txtfrmdate.Text);
            _PhysicalStockVerificationHdr.AUSH_JOB = txtdoc.Text.ToUpper().Trim();
            _PhysicalStockVerificationHdr.AUSH_LOC = txtloc.Text.ToUpper().Trim();
            _PhysicalStockVerificationHdr.AUSH_MOD_BY = Session["UserID"].ToString();
            _PhysicalStockVerificationHdr.AUSH_REM = txtremark.Text;
            _PhysicalStockVerificationHdr.AUSH_TO_DT = Convert.ToDateTime(txttodate.Text);
            _PhysicalStockVerificationHdr.AUSH_STUS = "P";
            _PhysicalStockVerificationHdr.AUSH_NO_JOB = Convert.ToInt32(txtnosubjob.Text);
            string x="";// bageta checking by Akila
            rowsAffected = CHNLSVC.Inventory.SaveStockVerification(_PhysicalStockVerificationHdr, GenerateMasterAutoNumber(), out _docNo,out x,null);

            if (rowsAffected != -1)
            {
                string Msg = "Successfully saved. " + _docNo;
                DisplayMessage(Msg, 3);
                PageClear();
            }
            else
            {
                DisplayMessage(_docNo, 4);
            }
        }

        private bool validation()
        {

            if (string.IsNullOrEmpty(txtloc.Text))
            {
                DisplayMessage("Please select location", 1);
                return false;
            }
            if (string.IsNullOrEmpty(txtnosubjob.Text))
            {
                DisplayMessage("Please type no of jobs", 1);
                return false;
            }
            if (lblstatus.Text == "FINESHED")
            {
                DisplayMessage("This document has already finished.", 1);
                return false;
            }
            if (string.IsNullOrEmpty(txtnosubjob.Text))
            {
                DisplayMessage("Please enter job", 1);
                txtnosubjob.Focus();
                return false;
            }

            int Qty = Convert.ToInt32(txtnosubjob.Text);
            if (Qty < 0)
            {
                DisplayMessage("canot add minus job", 1);
                txtnosubjob.Focus();
                return false;
            }
            return true;
        }
        #region Modal Popup 2
        protected void btnDClose_Click(object sender, EventArgs e)
        {
            txtSearchbywordD.Text = "";
            UserDPopoup.Hide();
            // UserPopup.Hide();
        }
        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "doc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                DataTable result = CHNLSVC.Inventory.SEARCH_STOCKVERF_HDR(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                grdResultD.PageIndex = 0;
                UserDPopoup.Show();
                return;
            }

        }
        protected void grdResultD_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
            string Name = grdResultD.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "doc")
            {
                GetHddr(Name);
            }


        }
        protected void grdResultD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultD.PageIndex = e.NewPageIndex;
            try
            {
                grdResultD.DataSource = null;
                DataTable _tbl = (DataTable)ViewState["SEARCH"];
                grdResultD.DataSource = _tbl;

                grdResultD.DataBind();
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbywordD.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }

        }
        protected void lbtnSearchD_Click(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }
        private void FilterData()
        {
            if (lblvalue.Text == "doc")
            {

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                DataTable result = CHNLSVC.Inventory.SEARCH_STOCKVERF_HDR(SearchParams, ddlSearchbykeyD.SelectedItem.Text, "%" + txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                UserDPopoup.Show();
                return;
            }
            if (lblvalue.Text == "location")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, "%" + txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
            if ((lblvalue.Text == "Item") )
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result2 = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result2;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                UserPopoup.Show();
                return;
            }
        }
        protected void txtSearchbywordD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
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

        #endregion
        protected void btnSearch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                txtSearchbyword.Text = "";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Item";
                BindUCtrlDDLData(result);
                txtSearchbyword.Focus();
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            if (txtSavelconformmessageValue.Value == "No")
            {
                return;
            }
            if (lblstatus.Text == "APPROVED")
            {
                DisplayMessage("This document has already approved.", 1);
                return;
            }
            if (lblstatus.Text == "FINISHED")
            {
                DisplayMessage("This document has already finished.", 1);
                return;
            }
            if (lblstatus.Text == "PENDING")
            {
                DisplayMessage("This document has already pending.", 1);
                return;
            }
            if (validation())
            {
                SaveProjectPlane();
            }

        }
        protected void lbtnjobCode_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtloc.Text))
            {
                DisplayMessage("Please select location", 1);
                return ;
            }
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
            DataTable result = CHNLSVC.Inventory.SEARCH_STOCKVERF_HDR(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
            grdResultD.DataSource = result;
            grdResultD.DataBind();
            lblvalue.Text = "doc";
            BindUCtrlDDLData2(result);
            txtSearchbyword.Focus();
            UserDPopoup.Show();

        }

        private void GetHddr(string _doc)
        {
            _PhysicalStockVerificationHdr = CHNLSVC.Inventory.GET_STOCKVERF_HDR(_doc);
            if (_PhysicalStockVerificationHdr.AUSH_JOB != null)
            {
                txtdoc.Text = _PhysicalStockVerificationHdr.AUSH_JOB;
                txtremark.Text = _PhysicalStockVerificationHdr.AUSH_REM;
                txtloc.Text = _PhysicalStockVerificationHdr.AUSH_LOC;
                txtDate.Text = _PhysicalStockVerificationHdr.AUSH_DT.ToString("dd/MMM/yyyy");
                txtfrmdate.Text = _PhysicalStockVerificationHdr.AUSH_FRM_DT.ToString("dd/MMM/yyyy");
                txttodate.Text = _PhysicalStockVerificationHdr.AUSH_TO_DT.ToString("dd/MMM/yyyy");
                txtloc.Enabled = false;
                LinkButton2.Visible = false;
                if (_PhysicalStockVerificationHdr.AUSH_STUS == "P")
                {
                    lblstatus.Text = "PENDING";
                }
                else if (_PhysicalStockVerificationHdr.AUSH_STUS == "F")
                {
                    lblstatus.Text = "FINISHED";
                }
                _PhsicalStockVerificationMain = CHNLSVC.Inventory.GET_STOCKVERF_MAIN(_doc);
                if (_PhsicalStockVerificationMain.Count > 0)
                {
                    grdsubjob.DataSource = _PhsicalStockVerificationMain; //_PhsicalStockVerificationMain.Where(x=>x.Ausm_main_job !=x.Ausm_job);
                    grdsubjob.DataBind();


                    ddlSUBJOB.DataSource = _PhsicalStockVerificationMain;
                    ddlSUBJOB.DataTextField = "ausm_job";
                    ddlSUBJOB.DataValueField = "ausm_job";
                    ddlSUBJOB.DataBind();

                    user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("STJO", Session["UserCompanyCode"].ToString(), ddlSUBJOB.Text, 1);
                    LoadPickSerial(user_seq_num, "STJO");
                    txtnosubjob.Text = (_PhsicalStockVerificationMain.Count()-1).ToString();
                }
                else
                {
                    txtnosubjob.Text = "0";
                }
            }
            else
            {
                DisplayMessage("Please select valid job number", 1);
                txtdoc.Text = string.Empty;

            }
        }
        private bool LoadItemDetail(string _item)
        {
            lblItemDescription.Text = string.Empty;
            lblItemModel.Text = string.Empty;
            lblItemBrand.Text = string.Empty;
            _Mstitem = new MasterItem();

            bool _isValid = false;

            if (!string.IsNullOrEmpty(_item))
            {
                _Mstitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
            }
            if (_Mstitem != null && !string.IsNullOrEmpty(_Mstitem.Mi_cd))
            {
                _isValid = true;
                string _description = _Mstitem.Mi_longdesc;
                string _model = _Mstitem.Mi_model;
                string _brand = _Mstitem.Mi_brand;
                string _serialstatus = _Mstitem.Mi_is_ser1 == 1 ? "Available" : "Non";

                lblItemDescription.Text = _description;
                lblItemModel.Text = _model;
                lblItemBrand.Text = _brand;
                Session["_itemSerializedStatus"] = _Mstitem.Mi_is_ser1;

                if (_Mstitem.Mi_is_ser1 == 1)
                {
                    pnlserial.Visible = true;
                    pnlnonserial.Visible = false;
                }
                else
                {
                    pnlserial.Visible = false;
                    pnlnonserial.Visible = true;
                }
            }
            else _isValid = false;
            return _isValid;
        }
      
        protected void txtItem_TextChanged(object sender, EventArgs e)
        {


            try
            {
                if (!LoadItemDetail(txtitem.Text.ToUpper()))
                {
                    DisplayMessage("Please check the item code", 1);
                    txtitem.Text = "";
                    txtitem.Focus();

                    return;
                }

               


            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }

        }
     
        protected void btnqty_Click(object sender, EventArgs e)
        {
            txtitem.Text = txtitem.Text.ToUpper().Trim();
            if (string.IsNullOrEmpty(txtitem.Text))
            {
                DisplayMessage("Please select the item", 1);
                return;
            }
            SaveTempPickSer(txtitem.Text,txtserial.Text,ddlSUBJOB.Text);
        }
        protected void lbtnviewserial_Click(object sender, EventArgs e)
        {
            if (user_seq_num < 0)
            {
                user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("STJO", Session["UserCompanyCode"].ToString(), ddlSUBJOB.Text, 0);
            }

            LoadPickSerial(user_seq_num, "STJO");
        }
        private void LoadPickSerial(int seq, string doctype)
        {
            _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), txtloc.Text, Session["UserID"].ToString(), seq, doctype);

            if (_serList != null)
            {
                var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_qty, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                foreach (var itm in _scanItems)
                {

                    if ((itm.Peo.Tus_itm_cd == txtitem.Text))
                    {
                        if (itm.Peo.Tus_qty > 1)
                        {
                            lblscanQty.Text = itm.Peo.Tus_qty.ToString();//itm.Peo.Tus_qty; // Current scan qty    

                        }
                        else
                        {
                            lblscanQty.Text = itm.theCount.ToString();//itm.Peo.Tus_qty; // Current scan qty    

                        }
                    }
                    _lineNo = _serList.Max(x => x.Tus_itm_line);


                }
                if(!string.IsNullOrEmpty(txtitem.Text))
                {
                    var _filterItem = _serList.Where(x => x.Tus_itm_cd == txtitem.Text).ToList();

                    grdpickserial.DataSource = SerialDes(_filterItem);
                    grdpickserial.DataBind();
                    if (_filterItem.Count == 0)
                    {
                        lblscanQty.Text = "0";

                    }
                }
                else
                {
                    grdpickserial.DataSource = SerialDes(_serList);
                    grdpickserial.DataBind();
                    if (_serList.Count == 0)
                    {
                        lblscanQty.Text = "0";

                    }
                }
               
               
             

              
            }
            else
            {
                grdpickserial.DataSource = new int[] { };
                grdpickserial.DataBind();
                lblscanQty.Text = "0";
            }


        }
        private void loadItemStatus()
        {
            oMasterItemStatuss = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());

        }

        protected void txtqty_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if ((_Mstitem.Mi_is_ser1 == 1) || (_Mstitem.Mi_is_ser1 == 0))
                {
                    decimal number = Convert.ToDecimal(txtqty.Text);
                    decimal fractionalPart = number % 1;
                    if (fractionalPart != 0)
                    {
                        DisplayMessage("only allow numeric value", 1);
                        return;
                    }


                }
                if (Convert.ToDecimal(txtqty.Text.Trim()) <= 0)
                {
                    DisplayMessage("Quantity should be positive value.", 1);
                    return;
                }

                SaveTempPickSer(txtitem.Text, txtqty.Text, ddlSUBJOB.Text);

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }
        private List<ReptPickSerials> SerialDes(List<ReptPickSerials> _serial)
        {
            if (oMasterItemStatuss != null && oMasterItemStatuss.Count > 0)
            {
                foreach (ReptPickSerials item in _serial)
                {
                    MasterItemStatus oStatus = oMasterItemStatuss.Find(x => x.Mis_cd == item.Tus_itm_stus);
                    if (oStatus != null)
                    {
                        item.Tus_itm_stus_Desc = oStatus.Mis_desc;
                    }
                    else
                    {
                        item.Tus_itm_stus_Desc = item.Tus_itm_stus;
                    }

                }

            }
            return _serial;
        }
        protected void OnRemoveFromSerialGrid(string _item, int _serialID, string _status)
        {
            try
            {
                MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                {
                    CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), txtloc.Text, user_seq_num, Convert.ToInt32(_serialID), null, null);
                    

                }
                else
                {
                    CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), txtloc.Text, user_seq_num, _item, _status);

                }
                LoadPickSerial(user_seq_num, "STJO");

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }

        }

        protected void lbtnser_Remove_Click(object sender, EventArgs e)
        {
            if (lblstatus.Text == "FINISHED")
            {
                DisplayMessage("This document has already fineshed.", 1);
                return;
            }
            if (txtDeleteconformmessageValue.Value == "No")
            {
                return;
            }
            if (grdpickserial.Rows.Count == 0) return;

            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _item = (row.FindControl("lbltus_itm_cd") as Label).Text;
                string _serialID = (row.FindControl("lbltus_ser_id") as Label).Text;
                string _status = (row.FindControl("lbltus_itm_stus") as Label).Text;
                string _line = (row.FindControl("lbltus_itm_line") as Label).Text;
                if (string.IsNullOrEmpty(_item)) return;
                OnRemoveFromSerialGrid(_item, Convert.ToInt32(_serialID), _status);
               // lbllastserial.Text = string.Empty;
            }
        }
        private void SaveTempPickSer(string _item, string _Serial,string _doc)
        {
            if (lblstatus.Text == "FINISHED")
            {
                DisplayMessage("This document has already fineshed.", 1);
                return;
            }

            if (user_seq_num < 0) //Create a new seq no & insert it to the scmrep.temp_pick_hdr header table.  
            {
                user_seq_num = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "STJO",1, Session["UserCompanyCode"].ToString());

                ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                _inputReptPickHeader.Tuh_direct = true;
                _inputReptPickHeader.Tuh_doc_no = _doc;
                _inputReptPickHeader.Tuh_doc_tp = "STJO";
                _inputReptPickHeader.Tuh_ischek_itmstus = false;
                _inputReptPickHeader.Tuh_ischek_reqqty = false;
                _inputReptPickHeader.Tuh_ischek_simitm = false;
                _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                _inputReptPickHeader.Tuh_usr_id = Session["UserID"].ToString();
                _inputReptPickHeader.Tuh_usrseq_no = user_seq_num;

                //Save it to the scmrep.temp_pick_hdr header table. 
                Int32 val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);
            }
            _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), txtloc.Text, Session["UserID"].ToString(), user_seq_num, "STJO");

             if (_serList != null)
             {
                 var _chkserial = _serList.Where(x => x.Tus_ser_1 == _Serial).ToList();
                 if (_chkserial.Count > 0)
                 {
                     DisplayMessage("already scan.", 1);
                     return;
                 }
             }
             else
             {
                 _serList = new List<ReptPickSerials>();
             }
             ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
             if (_Mstitem.Mi_is_ser1 == 1)
             {
                 _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serial(Session["UserCompanyCode"].ToString(), txtloc.Text, string.Empty, _item, _Serial);
                 if (_reptPickSerial_.Tus_ser_id != 0)
                 {
                     //Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _reptPickSerial_.Tus_itm_cd, Convert.ToInt32(_reptPickSerial_.Tus_ser_id), -1);
                     _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                     _reptPickSerial_.Tus_usrseq_no = user_seq_num;
                     _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                     _reptPickSerial_.Tus_base_doc_no = _doc;
                     _reptPickSerial_.Tus_base_itm_line = _lineNo + 1;//Convert.ToInt32(serial.Tus_itm_line);
                     _reptPickSerial_.Tus_itm_desc = _Mstitem.Mi_shortdesc;
                     _reptPickSerial_.Tus_itm_model = _Mstitem.Mi_model;
                     // _reptPickSerial_.Tus_doc_no = _doc;                 
                 }
                 else
                 {
                     _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                     _reptPickSerial_.Tus_usrseq_no = user_seq_num;
                     _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                     _reptPickSerial_.Tus_base_doc_no = _doc;
                     _reptPickSerial_.Tus_base_itm_line = _lineNo + 1;//Convert.ToInt32(serial.Tus_itm_line);
                     _reptPickSerial_.Tus_itm_desc = _Mstitem.Mi_shortdesc;
                     _reptPickSerial_.Tus_itm_model = _Mstitem.Mi_model;
                     _reptPickSerial_.Tus_doc_no = _doc;
                     _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                     _reptPickSerial_.Tus_loc = txtloc.Text.ToUpper().Trim();
                     _reptPickSerial_.Tus_bin = "N/A";
                     _reptPickSerial_.Tus_itm_cd = _item;
                     _reptPickSerial_.Tus_itm_stus = "GOD";
                     _reptPickSerial_.Tus_itm_line = _lineNo + 1;//Convert.ToInt32(serial.Tus_itm_line);
                     _reptPickSerial_.Tus_qty = 1;
                     _reptPickSerial_.Tus_ser_1 = _Serial;
                     _reptPickSerial_.Tus_ser_2 = "N/A";
                     _reptPickSerial_.Tus_ser_3 = "N/A";
                     _reptPickSerial_.Tus_ser_4 = "N/A";
                     _reptPickSerial_.Tus_ser_id = CHNLSVC.Inventory.GetSerialID(); 
                     _reptPickSerial_.Tus_serial_id = "0";
                     _reptPickSerial_.Tus_unit_cost = 0;
                     _reptPickSerial_.Tus_unit_price = 0;
                     _reptPickSerial_.Tus_unit_price = 0;

                 }
             }
             else
             {
                 _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                 _reptPickSerial_.Tus_usrseq_no = user_seq_num;
                 _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                 _reptPickSerial_.Tus_base_doc_no = _doc;
                 _reptPickSerial_.Tus_base_itm_line = _lineNo + 1;//Convert.ToInt32(serial.Tus_itm_line);
                 _reptPickSerial_.Tus_itm_desc = _Mstitem.Mi_shortdesc;
                 _reptPickSerial_.Tus_itm_model = _Mstitem.Mi_model;
                 _reptPickSerial_.Tus_doc_no = _doc;
                 _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                 _reptPickSerial_.Tus_loc = txtloc.Text.ToUpper().Trim();
                 _reptPickSerial_.Tus_bin = "N/A";
                 _reptPickSerial_.Tus_itm_cd = _item;
                 _reptPickSerial_.Tus_itm_stus = "GOD";
                 _reptPickSerial_.Tus_itm_line = _lineNo + 1;//Convert.ToInt32(serial.Tus_itm_line);
                 _reptPickSerial_.Tus_qty = Convert.ToDecimal(txtqty.Text);
                 _reptPickSerial_.Tus_ser_1 = "N/A";
                 _reptPickSerial_.Tus_ser_2 = "N/A";
                 _reptPickSerial_.Tus_ser_3 = "N/A";
                 _reptPickSerial_.Tus_ser_4 = "N/A";
                 _reptPickSerial_.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();
                 _reptPickSerial_.Tus_serial_id = "0";
                 _reptPickSerial_.Tus_unit_cost = 0;
                 _reptPickSerial_.Tus_unit_price = 0;
                 _reptPickSerial_.Tus_unit_price = 0;
             }
             Int32 affected_rows = 0;
            
             if (_serList.Count > 0)
             {
                 var _filter = _serList.SingleOrDefault(x => x.Tus_itm_cd == _item);
                 if (_filter != null)
                 {
                     _filter.Tus_qty = _filter.Tus_qty+_reptPickSerial_.Tus_qty;//_filter.Tus_qty+ 
                     affected_rows = CHNLSVC.Inventory.UpdateAllScanSerials(_filter);
                 }
                 else
                 {
                     affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                 }
             }
             else
             {
                 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
             }
            
             if (affected_rows >= 1)
             {
                 txtserial.Text = string.Empty;
                 txtqty.Text = string.Empty;
             }
        }

        protected void lbtnFinesh_Click(object sender, EventArgs e)
        {
            if (txtApprovalconformmessageValue.Value == "No")
            {
                return;
            }
            if (lblstatus.Text == "FINISHED")
            {
                DisplayMessage("This document has already fineshed.", 1);
                return;
            }
            if (validation())
            {
                FineshStockVerification();
           }

        }
        protected void txtlocation_TextChanged(object sender, EventArgs e)
        {
            txtloc.Text = txtloc.Text.ToUpper().Trim();
            DataTable result = new DataTable();
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "Code", txtloc.Text);
            if (result.Rows.Count == 0)
            {
                txtloc.Text = string.Empty;
                DisplayMessage("Please enter valid location", 1);
            }
        }

        protected void lbtnloc_Click(object sender, EventArgs e)
        {
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "location";
            BindUCtrlDDLData(result);
            txtSearchbyword.Focus();
            UserPopoup.Show();

        }
        protected void ddlSUBJOB_SelectedIndexChanged(object sender, EventArgs e)
        {
            user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("STJO", Session["UserCompanyCode"].ToString(), ddlSUBJOB.Text, 1);
        }
        private void FineshStockVerification()
        {
            int rowsAffected = 0;
            string _docNo = string.Empty;

            rowsAffected = CHNLSVC.Inventory.FineshStockVerification(txtdoc.Text, Session["UserCompanyCode"].ToString(),txtloc.Text,Session["UserID"].ToString(), out _docNo);

            if (rowsAffected > 0)
            {
                string Msg = "Successfully Finished. " + _docNo;
                DisplayMessage(Msg, 3);
                PageClear();
            }
            else
            {
                DisplayMessage(_docNo, 4);
            }
        }

        protected void txtdoc_TextChanged(object sender, EventArgs e)
        {

            //if (string.IsNullOrEmpty(txtloc.Text))
            //{
            //    DisplayMessage("Please select location", 1);
            //    return ;
            //}
            txtdoc.Text = txtdoc.Text.ToUpper().Trim();
            GetHddr(txtdoc.Text);
        }

        protected void lbtnFineshScan_Click(object sender, EventArgs e)
        {
            if (lblstatus.Text == "FINISHED")
            {
                DisplayMessage("This document has already fineshed.", 1);
                return;
            }
            if (txtFinesh.Value == "Yes")
            {
                try
                {
                    int result = 0;
                    string _err = string.Empty;
                    result = CHNLSVC.Inventory.updateDocumentFinishStatus(ddlSUBJOB.Text, "STJO", 1, out _err);
                    if (result > 0)
                    {
                        DisplayMessage("Successfully finish", 3);
                    }
                    else
                    {
                        DisplayMessage(_err, 4);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
            }
           

        }

        protected void chkpda_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkpda.Checked == true)
            //{
            //    txtdocname.Text = lblInvoiceNo.Text;
            //    MPPDA.Show();
            //}
            //else
            //{
            //    MPPDA.Hide();
            //}
        }
        private Int32 GenerateNewUserSeqNo(string _doc, string warehousecom, string warehouseloc)
        {
            Int32 generated_seq = 0;
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "STJO", 1, Session["UserCompanyCode"].ToString());//direction always =1 for this method                    //assign user_seqno
            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = "STJO";
            RPH.Tuh_cre_dt = DateTime.Today;//might change //Calendar-SelectedDate;
            RPH.Tuh_ischek_itmstus = true;//might change
            RPH.Tuh_ischek_reqqty = true;//might change
            RPH.Tuh_ischek_simitm = true;//might change
            RPH.Tuh_session_id = Session["SessionID"].ToString();
            RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();//might change
            RPH.Tuh_usr_id = Session["UserID"].ToString();
            RPH.Tuh_usrseq_no = generated_seq;
            RPH.Tuh_wh_com = warehousecom;
            RPH.Tuh_wh_loc = warehouseloc;
            RPH.Tuh_load_bay = ddlloadingbay.SelectedValue;
            RPH.Tuh_direct = false; //direction always (-) for change status
            RPH.Tuh_isdirect = true;
            RPH.Tuh_doc_no = _doc;
            RPH.Tuh_usr_loc = txtloc.Text.ToUpper();
            //write entry to TEMP_PICK_HDR
            int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
            CHNLSVC.CloseAllChannels();
            if (affected_rows == 1)
            {
                return generated_seq;
            }
            else
            {
                return 0;
            }
        }

        protected void btnSentScan_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtpda.Value == "No")
                {
                    return;
                }
                Int32 val = 0;
                string warehousecom = (string)Session["WAREHOUSE_COM"];
                string warehouseloc = (string)Session["WAREHOUSE_LOC"];
                if (ddlloadingbay.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the loading bay !!!')", true);
                    ddlloadingbay.Focus();
                    return;
                }
                foreach (GridViewRow _row in grdsubjob.Rows)
                {
                    CheckBox checkbox = _row.FindControl("chk_jobs") as CheckBox;
                    if (checkbox.Checked == true)
                    {
                        string _job = (_row.FindControl("lblausm_job") as Label).Text;

                        Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("STJO", Session["UserCompanyCode"].ToString(), _job, 0);
                        if (user_seq_num == -1)
                        {
                            user_seq_num = GenerateNewUserSeqNo(_job, warehousecom, warehouseloc);
                            if (user_seq_num > 0)
                            {
                                string msg = "Successfully sent to PDA";
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                            }
                         
                        }
                       else
                        {
                                  string msg = "Document has already sent to PDA or has already processed -" + _job;
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('"+msg+"')", true);
                            return;
                        //    ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                        //    _inputReptPickHeader.Tuh_doc_no = _job;
                        //    _inputReptPickHeader.Tuh_doc_tp = "STJO";
                        //    _inputReptPickHeader.Tuh_direct = false;
                        //    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                        //    _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                        //    _inputReptPickHeader.Tuh_wh_com = warehousecom;
                        //    _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                        //    _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;
                        //    _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(user_seq_num);
                        //    _inputReptPickHeader.Tuh_base_doc = _job;
                        //    //if (string.IsNullOrEmpty(_soano))
                        //    //{
                        //    //    _inputReptPickHeader.Tuh_is_take_res = true;
                        //    //}
                        //    val = CHNLSVC.Inventory.UpdatePickHeader(_inputReptPickHeader);

                        //    if (Convert.ToInt32(val) == -1)
                        //    {
                        //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);

                        //        return;
                        //    }
                        }
                       // DataTable dtchkitm = CHNLSVC.Inventory.CheckItemsScannedStatus(user_seq_num);

                       // if (dtchkitm.Rows.Count > 0)
                       // {
                      
                       // }
                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                CHNLSVC.CloseChannel();
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                CHNLSVC.CloseChannel();
            }

        }

    }
}