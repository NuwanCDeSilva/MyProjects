using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.UserControls
{
    public partial class ucTransportMethode : System.Web.UI.UserControl
    {
        #region properties
        public bool _serPopShow
        {
            get { if (Session["_serPopShow"] != null) { return (bool)Session["_serPopShow"]; } else { return false; } }
            set { Session["_serPopShow"] = value; }
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
        private TransportMethod _tMethod
        {
            get { if (Session["_tMethod"] != null) { return (TransportMethod)Session["_tMethod"]; } else { return  new TransportMethod(); } }
            set { Session["_tMethod"] = value; }
        }
        private List<TransportMethod> _tMethods
        {
            get { if (Session["_tMethods"] != null) { return (List<TransportMethod>)Session["_tMethods"]; } else { return new List<TransportMethod>(); } }
            set { Session["_tMethods"] = value; }
        }
        private TransportParty _tParty
        {
            get { if (Session["_tParty"] != null) { return (TransportParty)Session["_tParty"]; } else { return new TransportParty(); } }
            set { Session["_tParty"] = value; }
        }
        private List<TransportParty> _tPartys
        {
            get { if (Session["_tPartys"] != null) { return (List<TransportParty>)Session["_tPartys"]; } else { return new List<TransportParty>(); } }
            set { Session["_tPartys"] = value; }
        }
        private Transport _tra
        {
            get { if (Session["_tra"] != null) { return (Transport)Session["_tra"]; } else { return new Transport(); } }
            set { Session["_tra"] = value; }
        }
        //private List<Transport> _traList
        //{
        //    get { if (Session["_traList"] != null) { return (List<Transport>)Session["_traList"]; } else { return new List<Transport>(); } }
        //    set { Session["_traList"] = value; }
        //}
       // private List<Transport> _traList = new List<Transport>();
        protected List<Transport> _traList
        {
            get
            {
                if (Session["_traList"]==null)
                {
                    return new List<Transport>();
                }
                else
                {
                    return (List<Transport>)Session["_traList"];
                } 

            }
            set { Session["_traList"] = value; }
        }
        public DropDownList DdlTransportMe
        {
            get { return ddlTransportMe; }
            set { ddlTransportMe = value; }
        }
        public DropDownList DdlServiceBy
        {
            get { return ddlServiceBy; }
            set { ddlServiceBy = value; }
        }
        public string LblSubLvl
        {
            get { return lblSubLvl.Text; }
            set { lblSubLvl.Text = value; }
        }
        public string TxtSubLvl
        {
            get { return txtSubLvl.Text; }
            set { txtSubLvl.Text = value; }
        }
       
        public string LblNxtLvlD1
        {
            get { return lblNxtLvlD1.Text; }
            set { lblNxtLvlD1.Text = value; }
        }
        public string TxtNxtLvlD1
        {
            get { return txtNxtLvlD1.Text; }
            set { txtNxtLvlD1.Text = value; }
        }
        public string LblNxtLvlD2
        {
            get { return lblNxtLvlD2.Text; }
            set { lblNxtLvlD2.Text = value; }
        }
        public string TxtNxtLvlD2
        {
            get { return txtNxtLvlD2.Text; }
            set { txtNxtLvlD2.Text = value; }
        }
        public string TxtRemarks
        {
            get { return txtRemarks.Text; }
            set { txtRemarks.Text = value; }
        }
        public string TxtNoOfPacking
        {
            get { return txtNoOfPacking.Text; }
            set { txtNoOfPacking.Text = value; }
        }
        public GridView DgvTrns
        {
            get { return dgvTrns; }
            set { dgvTrns = value; }
        }
        private string transActionNo { get; set; }
        public string TransActionNo
        {
            get { return transActionNo; }
            set { transActionNo = value; }
        }
        private string docNo { get; set; }
        public string DocNo
        {
            get { return docNo; }
            set { docNo = value; }
        }

        #endregion
        Base _base = new Base();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _tMethod = new TransportMethod();
                _tMethods = new List<TransportMethod>();
                _tParty = new TransportParty();
                _tPartys = new List<TransportParty>();
                _tra = new Transport();

                BindTransMethode();
                ddlTransportMe.SelectedIndex = ddlTransportMe.Items.IndexOf(ddlTransportMe.Items.FindByText("COURIER"));
                BindTransService();
                LoadLabelData();
                SHowSearchButton();
                dgvTrns.DataSource = new int[] { };
                dgvTrns.DataBind();
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
            }
        }

        private void BindTransMethode()
        {
            _tMethods = new List<TransportMethod>();
            _tMethod = new TransportMethod();
            _tMethods = _base.CHNLSVC.General.GET_TRANS_METH(_tMethod);
            ddlTransportMe.DataSource = _tMethods.OrderBy(c => c.Rtm_tp);
            ddlTransportMe.DataTextField = "Rtm_tp";
            ddlTransportMe.DataValueField = "rtm_seq";
            ddlTransportMe.DataBind();
        }

        private void BindTransService()
        {
            _tPartys = new List<TransportParty>();
            _tParty = new TransportParty() { Rtnp_tnpt_seq = Convert.ToInt32(ddlTransportMe.SelectedValue), Mbe_com = Session["UserCompanyCode"].ToString() };
            _tPartys = _base.CHNLSVC.General.GET_TRANSPORT_PTY(_tParty);
            while (ddlServiceBy.Items.Count > 1)
            {
                ddlServiceBy.Items.RemoveAt(1);
            }
            ddlServiceBy.DataSource = _tPartys.Where(c => c.Mbe_name != string.Empty && c.Mbe_name != null).OrderBy(c => c.Mbe_name);
            ddlServiceBy.DataTextField = "mbe_name";
            ddlServiceBy.DataValueField = "rtnp_pty_cd";
            ddlServiceBy.DataBind();
        }

        private void LoadLabelData()
        {
            lblSubLvl.Text = "";
            txtSubLvl.Text = "";
            lblSubLvl.Visible = false;
            txtSubLvl.Visible = false;

            lblNxtLvlD1.Text = "";
            txtNxtLvlD1.Text = "";
            lblNxtLvlD1.Visible = false;
            txtNxtLvlD1.Visible = false;

            lblNxtLvlD2.Text = "";
            txtNxtLvlD2.Text = "";
            lblNxtLvlD2.Visible = false;
            txtNxtLvlD2.Visible = false;

            if (ddlTransportMe.SelectedIndex != -1)
            {
                _tMethod = new TransportMethod() { Rtm_tp = ddlTransportMe.SelectedItem.Text };
                _tMethods = new List<TransportMethod>();
                _tMethods = _base.CHNLSVC.General.GET_TRANS_METH(_tMethod);
                if (_tMethods != null)
                {
                    if (_tMethods.Count > 0)
                    {
                        if (_tMethods[0].Rtm_sub_lvl == 1)
                        {
                            if (!string.IsNullOrEmpty(_tMethods[0].Rtm_sub_dis))
                            {
                                lblSubLvl.Text = _tMethods[0].Rtm_sub_dis;
                                lblSubLvl.Visible = true;
                                txtSubLvl.Visible = true;
                            }
                        }

                        if (_tMethods[0].Rtm_nxt_lvl == 1)
                        {
                            if (!string.IsNullOrEmpty(_tMethods[0].Rtm_disp_1))
                            {
                                lblNxtLvlD1.Text = _tMethods[0].Rtm_disp_1;
                                lblNxtLvlD1.Visible = true;
                                txtNxtLvlD1.Visible = true;
                            }
                            if (!string.IsNullOrEmpty(_tMethods[0].Rtm_disp_2))
                            {
                                lblNxtLvlD2.Text = _tMethods[0].Rtm_disp_2;
                                lblNxtLvlD2.Visible = true;
                                txtNxtLvlD2.Visible = true;
                            }
                        }
                    }
                }

            }
        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            #region Validate
            if (DdlTransportMe.SelectedIndex == -1)
            {
                DispMsg("Please select a transport methode !!!", "W"); return;
            }
            if (DdlServiceBy.SelectedIndex < 1)
            {
                DispMsg("Please select a service by !!!", "W"); return;
            }
            if (txtSubLvl.Visible && string.IsNullOrEmpty(txtSubLvl.Text))
            {
                DispMsg("Please enter " + lblSubLvl.Text + " !!!", "W"); return;
            }
            //if (string.IsNullOrEmpty(txtRefNo.Text))
            //{
            //    DispMsg("Please enter ref # ", "W"); return;
            //}
            if (txtNxtLvlD1.Visible && string.IsNullOrEmpty(txtNxtLvlD1.Text))
            {
                DispMsg("Please enter " + lblNxtLvlD1.Text + " !!!", "W"); return;
            }
            if (txtNxtLvlD2.Visible && string.IsNullOrEmpty(txtNxtLvlD2.Text))
            {
                DispMsg("Please enter " + lblNxtLvlD2.Text + " !!!", "W"); return;
            }
            if (string.IsNullOrEmpty(txtNoOfPacking.Text))
            {
                DispMsg("Please enter the no of packing required !!!", "W"); return;
            }
            Int32 noOfPack = 0;
            if (!Int32.TryParse(txtNoOfPacking.Text, out noOfPack))
            {
                DispMsg("Please enter valid no packing !!!", "W"); return;
            }
            //if (!string.IsNullOrEmpty(txtNxtLvlD1.Text))
            //{
            //    bool _vehicle = validateVehicle(txtVehicle.Text);
            //}
            if (ddlTransportMe.SelectedItem.Text=="BY VEHICLE")
            {
                if (!string.IsNullOrEmpty(txtNxtLvlD1.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Driver);
                    _serData = _base.CHNLSVC.CommonSearch.SearchEmployeeByType(_para, "CODE", txtNxtLvlD1.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtNxtLvlD1.Text.ToUpper().Trim(), "CODE", "Name");
                    txtNxtLvlD1.ToolTip = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtNxtLvlD1.Text = string.Empty;
                        txtNxtLvlD1.Focus();
                        DispMsg("Please enter valid driver !");
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(txtNxtLvlD2.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Helper);
                    _serData = _base.CHNLSVC.CommonSearch.SearchEmployeeByType(_para, "CODE", txtNxtLvlD2.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtNxtLvlD2.Text.ToUpper().Trim(), "CODE", "Name");
                    txtNxtLvlD2.ToolTip = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtNxtLvlD2.Text = string.Empty;
                        txtNxtLvlD2.Focus();
                        DispMsg("Please enter valid helper !");
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(txtSubLvl.Text))
                {
                    FleetVehicleMaster _tmpObj = new FleetVehicleMaster();
                    _tmpObj.Fv_reg_no = txtSubLvl.Text.Trim().ToUpper();
                    FleetVehicleMaster _fleet = new FleetVehicleMaster();
                    _fleet = _base.CHNLSVC.General.GET_FLT_VEHICLE_DATA(_tmpObj).FirstOrDefault();
                    if (_fleet != null)
                    {
                        if (_fleet.Fv_com != Session["UserCompanyCode"].ToString())
                        {
                            string _msg = "Cannot view details ! Entered Reg # belongs to " + _fleet.Fv_com + " !";
                            DispMsg(_msg); txtSubLvl.Text = ""; txtSubLvl.Focus(); return;
                        }
                    }
                }

            }
            #endregion
            #region add by lakshan auto no genarate
            bool _autoSlipgen = false;
            string _autoNoCurrCd = "";
            MasterAutoNumber _AutoNo = new MasterAutoNumber();
            if (ddlServiceBy.SelectedIndex > 0)
            {
                List<MasterBusinessEntity> _busEntList = _base.CHNLSVC.Inventory.GET_MST_BUSENTITY_DATA(Session["UserCompanyCode"].ToString(), ddlServiceBy.SelectedValue, "");
                var _busEnt = _busEntList.FirstOrDefault();
                if (_busEnt != null)
                {
                    if (_busEnt.Mbe_curr_slip_gen == 1)
                    {
                        _autoSlipgen = true;
                        _AutoNo = new MasterAutoNumber();
                        _AutoNo.Aut_cate_cd = Session["UserDefLoca"].ToString();
                        _AutoNo.Aut_cate_tp = "LOC";
                        _AutoNo.Aut_direction = 0;
                        _AutoNo.Aut_modify_dt = null;
                        _AutoNo.Aut_moduleid = "CURR";
                      //  _AutoNo.Aut_start_char = "CURR";
                        _AutoNo.Aut_start_char = _busEnt.Mbe_curr_slip_cd.ToUpper();
                        DateTime date = DateTime.Now;
                        _AutoNo.Aut_year = date.Year;
                        _autoNoCurrCd = _busEnt.Mbe_curr_slip_cd;
                    }
                }
            }
            #endregion
            Int32 row = 0;
            Int32 _rowCount = 0;
            if (_traList!=null)
            {
                if (_traList.Count>0)
                {
                    row = _traList.Max(c => c._grdRowNo);
                }
            }
            while (noOfPack>0)
            {
                noOfPack--;
                _tra = new Transport();
                //_tra.itrn_seq                       
                _tra.Itrn_com = Session["UserCompanyCode"].ToString();
                _tra.Itrn_loc = Session["UserDefLoca"].ToString();
               // _tra.Itrn_trns_no = transActionNo;
                _tra.Itrn_ref_doc = docNo;
                _tra.Itrn_trns_method = ddlTransportMe.SelectedItem.Text;
                _tra.Itrn_trns_pty_tp = ddlTransportMe.SelectedItem.Text;
                _tra.Itrn_trns_pty_cd = ddlServiceBy.SelectedValue;
                _tra.Itrn_ref_no = txtSubLvl.Text;
                _tra.Itrn_rmk = txtRemarks.Text;
                _tra.Itrn_stus = "A";
                _tra.Itrn_cre_by = Session["UserID"].ToString();
                _tra.Itrn_cre_dt = DateTime.Now;
                _tra.Itrn_cre_session = Session["SessionID"].ToString();
                _tra.Itrn_mod_by = Session["UserID"].ToString();
                _tra.Itrn_mod_dt = DateTime.Now;
                _tra.Itrn_mod_session = Session["SessionID"].ToString();
                _tra.Itrn_anal1 = txtNxtLvlD1.Text;
                _tra.Itrn_anal2 = txtNxtLvlD2.Text;
                _tra._grdRowNo = row;
                _tra.Tmp_slip_cd = "";
                #region add auto parameters
                if (_autoSlipgen)
                {
                    if (_traList.Count>0)
                    {
                        var _tmpTraList = _traList.Where(c => c.Itrn_trns_pty_cd == _tra.Itrn_trns_pty_cd).ToList();
                        if (_tmpTraList!=null)
                        {
                            if (_tmpTraList.Count>0)
                            {
                                _rowCount = _tmpTraList.Count;
                            }
                        }
                        
                    }
                    _tra.Mbe_curr_slip_cd = _autoNoCurrCd;
                    Int32 _autoNo = _base.CHNLSVC.Inventory.GetAutoNumber(_AutoNo.Aut_moduleid, Convert.ToInt16(_AutoNo.Aut_direction), _AutoNo.Aut_start_char,
                            _AutoNo.Aut_cate_tp, _AutoNo.Aut_cate_cd, _AutoNo.Aut_modify_dt, _AutoNo.Aut_year).Aut_number;
                    _autoNo = _autoNo + _rowCount;
                    string _documentNo = Session["UserDefLoca"].ToString() + "-" + _tra.Mbe_curr_slip_cd + "-" + "CO" + "-" + Convert.ToString(DateTime.Today.Year).Remove(0, 2) + "-" + _autoNo.ToString("00000", CultureInfo.InvariantCulture);
                    _tra.Tmp_slip_cd = _documentNo;
                    _tra.MstAuto = _AutoNo;
                    _tra.Slip_no_auto_gen = true;
                    _tra.Itrn_ref_no = _tra.Tmp_slip_cd;
                }
                #endregion
                _traList.Add(_tra);
                row++;
            }
           
            dgvTrns.DataSource = new int[] { };
            if (_traList.Count > 0)
            {
                var _ds = _traList.Where(c => c.Itrn_stus == "A").ToList();
                if (_ds != null)
                {
                    DgvTrns.DataSource = _ds;
                }
            } dgvTrns.DataBind();
            //Label lblHedSubLvl = dgvTrns.HeaderRow.FindControl("lblHedSubLvl") as Label;
            //Label lblHedNxtLvlD1 = dgvTrns.HeaderRow.FindControl("lblHedNxtLvlD1") as Label;
            //Label lblHedNxtLvlD2 = dgvTrns.HeaderRow.FindControl("lblHedNxtLvlD2") as Label;
            //lblHedSubLvl.Text = "Ref #";
            //lblHedNxtLvlD1.Text = lblNxtLvlD1.Text;
            //lblHedNxtLvlD2.Text = lblNxtLvlD2.Text;

            ClearUcTrans();
        }

        public void ClearUcTrans()
        {
            ddlTransportMe.SelectedIndex = 0;
            ddlTransportMe_SelectedIndexChanged(null,null);
            BindTransService();
            ddlServiceBy.SelectedIndex = 0;
            txtSubLvl.Text = "";
            txtRemarks.Text = "";
            txtNxtLvlD1.Text = "";
            txtNxtLvlD2.Text = "";
            txtNoOfPacking.Text = "";
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

        protected void ddlTransportMe_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLabelData();
            BindTransService();
            SHowSearchButton();
        }
        private void SHowSearchButton()
        {
            if (ddlTransportMe.SelectedItem.Text=="BY VEHICLE")
            {
                lbtnSeVehicle.Visible = true;
                lbtnSeDriver.Visible = true;
                lbtnSeHelper.Visible = true;
            }
            else
            {
                lbtnSeVehicle.Visible = false;
                lbtnSeDriver.Visible = false;
                lbtnSeHelper.Visible = false;
            }
        }
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                DgvTrns.DataSource = new int[] { };
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                if (_traList != null)
                {
                    if (row != null)
                    {
                        Label lblRowNo = (Label)row.FindControl("lblRowNo");
                        Int32 _rowNo = Convert.ToInt32(lblRowNo.Text);
                        var v = _traList.Where(c => c._grdRowNo == _rowNo).FirstOrDefault();
                        if (v != null)
                        {
                            v.Itrn_stus = "C";
                        }
                    }
                    var _ds = _traList.Where(c => c.Itrn_stus == "A").ToList();
                    if (_ds != null)
                    {
                        DgvTrns.DataSource = _ds;
                    }
                }
                DgvTrns.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnClearData_Click(object sender, EventArgs e)
        {
            ClearUcTrans();
            _traList = new List<Transport>();
            DgvTrns.DataSource = new int[] { };
            DgvTrns.DataBind();
        }

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {

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
        protected void txtSubLvl_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlTransportMe.SelectedItem.Text == "BY VEHICLE")
                {
                    if (!string.IsNullOrEmpty(txtSubLvl.Text))
                    {
                        FleetVehicleMaster _tmpObj = new FleetVehicleMaster();
                        _tmpObj.Fv_reg_no = txtSubLvl.Text.Trim().ToUpper();
                        FleetVehicleMaster _fleet = new FleetVehicleMaster();
                        _fleet = _base.CHNLSVC.General.GET_FLT_VEHICLE_DATA(_tmpObj).FirstOrDefault();
                        if (_fleet != null)
                        {
                            if (_fleet.Fv_com != Session["UserCompanyCode"].ToString())
                            {
                                string _msg = "Cannot view details ! Entered Reg # belongs to " + _fleet.Fv_com + " !";
                                DispMsg(_msg); txtSubLvl.Text = ""; txtSubLvl.Focus(); return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtNxtLvlD1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlTransportMe.SelectedItem.Text == "BY VEHICLE")
                {
                    txtNxtLvlD1.ToolTip = string.Empty;
                    if (!string.IsNullOrEmpty(txtNxtLvlD1.Text))
                    {
                        _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Driver);
                        _serData = _base.CHNLSVC.CommonSearch.SearchEmployeeByType(_para, "CODE", txtNxtLvlD1.Text.ToUpper().Trim());
                        DataAvailable(_serData, txtNxtLvlD1.Text.ToUpper().Trim(), "CODE", "Name");
                        txtNxtLvlD1.ToolTip = _ava ? _toolTip : "";
                        if (!_ava)
                        {
                            txtNxtLvlD1.Text = string.Empty;
                            txtNxtLvlD1.Focus();
                            DispMsg("Please enter valid driver !");
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtNxtLvlD2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlTransportMe.SelectedItem.Text == "BY VEHICLE")
                {
                    txtNxtLvlD2.ToolTip = string.Empty;
                    if (!string.IsNullOrEmpty(txtNxtLvlD2.Text))
                    {
                        _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Helper);
                        _serData = _base.CHNLSVC.CommonSearch.SearchEmployeeByType(_para, "CODE", txtNxtLvlD2.Text.ToUpper().Trim());
                        DataAvailable(_serData, txtNxtLvlD2.Text.ToUpper().Trim(), "CODE", "Name");
                        txtNxtLvlD2.ToolTip = _ava ? _toolTip : "";
                        if (!_ava)
                        {
                            txtNxtLvlD2.Text = string.Empty;
                            txtNxtLvlD2.Focus();
                            DispMsg("Please enter valid helper !");
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            _serPopShow = false;
            PopupSearch.Hide();
        }

        protected void txtSerByKey_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (_serType == "RegistrationDet")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RegistrationDet);
                    _serData = _base.CHNLSVC.CommonSearch.SearchFleetRegistrationNO(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "EetimateByJob")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EetimateByJob);
                    _serData = _base.CHNLSVC.CommonSearch.Get_Service_Estimates_ByJob(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "Helper")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Helper);
                    _serData = _base.CHNLSVC.CommonSearch.SearchEmployeeByType(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "Driver")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Driver);
                    _serData = _base.CHNLSVC.CommonSearch.SearchEmployeeByType(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
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
        protected string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RegistrationDet:
                    {
                        // paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtPartyCd.Text.Trim().ToUpper() + seperator + txtPartyVal.Text.Trim().ToUpper() + seperator);
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Helper:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "HELPER" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Driver:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "DRIVER" + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
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
                if (_serType == "RegistrationDet")
                {
                    txtSubLvl.Text = code;
                    txtSubLvl_TextChanged(null, null);
                }
                else if (_serType == "Helper")
                {
                    txtNxtLvlD2.Text = code;
                    txtNxtLvlD2_TextChanged(null, null);
                }
                else if (_serType == "Driver")
                {
                    txtNxtLvlD1.Text = code;
                    txtNxtLvlD1_TextChanged(null, null);
                }
                _serPopShow = false;
                PopupSearch.Hide();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeVehicle_Click(object sender, EventArgs e)
        {
            _serData = new DataTable();
            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RegistrationDet);
            _serData = _base.CHNLSVC.CommonSearch.SearchFleetRegistrationNO(_para, null, null);
            LoadSearchPopup("RegistrationDet", "REGISTER NO", "ASC");
        }

        protected void lbtnSeDriver_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Driver);
                _serData = _base.CHNLSVC.CommonSearch.SearchEmployeeByType(_para, null, null);
                LoadSearchPopup("Driver", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeHelper_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Helper);
                _serData = _base.CHNLSVC.CommonSearch.SearchEmployeeByType(_para, null, null);
                LoadSearchPopup("Helper", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtSubLvl_TextChanged1(object sender, EventArgs e)
        {
            try
            {
                if (ddlTransportMe.SelectedItem.Text == "BY VEHICLE")
                {
                    txtSubLvl.ToolTip = string.Empty;
                    if (!string.IsNullOrEmpty(txtSubLvl.Text))
                    {
                        _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RegistrationDet);
                        _serData = _base.CHNLSVC.CommonSearch.SearchFleetRegistrationNO(_para, "REGISTER NO", txtSubLvl.Text.ToUpper().Trim());
                        DataAvailable(_serData, txtSubLvl.Text.ToUpper().Trim(), "REGISTER NO", "Model");
                        txtSubLvl.ToolTip = _ava ? _toolTip : "";
                        if (!_ava)
                        {
                            txtSubLvl.Text = string.Empty;
                            txtSubLvl.Focus();
                            DispMsg("Please enter valid vehicle !");
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void ddlServiceBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlServiceBy.SelectedIndex>0)
                {
                    List<MasterBusinessEntity> _busEntList = _base.CHNLSVC.Inventory.GET_MST_BUSENTITY_DATA(Session["UserCompanyCode"].ToString(), ddlServiceBy.SelectedValue, "");
                    var _busEnt = _busEntList.FirstOrDefault();
                    if (_busEnt!=null)
                    {
                        if (_busEnt.Mbe_curr_slip_gen==1)
                        {
                            txtSubLvl.Enabled = false;
                            MasterAutoNumber _AutoNo = new MasterAutoNumber();
                            _AutoNo.Aut_cate_cd = Session["UserDefLoca"].ToString();
                            _AutoNo.Aut_cate_tp = "LOC";
                            _AutoNo.Aut_direction = 0;
                            _AutoNo.Aut_modify_dt = null;
                            _AutoNo.Aut_moduleid = "CURR";
                           // _AutoNo.Aut_start_char = "CURR";
                            _AutoNo.Aut_start_char = _busEnt.Mbe_curr_slip_cd.ToUpper();
                            DateTime date = DateTime.Now;
                            _AutoNo.Aut_year = date.Year;
                            Int32 _autoNo = _base.CHNLSVC.Inventory.GetAutoNumber(_AutoNo.Aut_moduleid, Convert.ToInt16(_AutoNo.Aut_direction), _AutoNo.Aut_start_char, 
                                _AutoNo.Aut_cate_tp, _AutoNo.Aut_cate_cd, _AutoNo.Aut_modify_dt, _AutoNo.Aut_year).Aut_number;
                            //ABS-CURR-17-0001
                            Int32 _rowCount = 0;
                            if (_traList.Count > 0)
                            {
                                //_rowCount = _traList.Count;
                                if (_traList.Count > 0)
                                {
                                    var _tmpTraList = _traList.Where(c => c.Itrn_trns_pty_cd == ddlServiceBy.SelectedValue).ToList();
                                    if (_tmpTraList != null)
                                    {
                                        if (_tmpTraList.Count > 0)
                                        {
                                            _rowCount = _tmpTraList.Count;
                                        }
                                    }

                                }
                            }
                            _autoNo = _autoNo + _rowCount;
                            string _documentNo = Session["UserDefLoca"].ToString() + "-" + _busEnt.Mbe_curr_slip_cd + "-"+"CO" + "-" + Convert.ToString(date.Year).Remove(0, 2) + "-" + _autoNo.ToString("00000", CultureInfo.InvariantCulture);
                            txtSubLvl.Text = _documentNo;
                        }
                        else
                        {
                            txtSubLvl.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }
    }
}