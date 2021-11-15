using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FF.BusinessObjects.General;
using System.IO;
using System.Text;
using FF.BusinessObjects;

namespace FastForward.SCMWeb.View.MasterFiles.Fleet_Management
{
    public partial class FleetMaster : BasePage
    {
        FleetVehicleMaster _tmpObj = new FleetVehicleMaster();
        string _para = "";
        protected String physicalPath { get { return (String)Session["physicalPath"]; } set { Session["physicalPath"] = value; } }
        protected String virtualPath { get { return (String)Session["virtualPath"]; } set { Session["virtualPath"] = value; } }
        protected String soucePath { get { return (String)Session["soucePath"]; } set { Session["soucePath"] = value; } }
        public string ImageFolderPath { get; set; }
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
        bool _serPopShow
        {
            get { if (Session["_serPopShow"] != null) { return (bool)Session["_serPopShow"]; } else { return false; } }
            set { Session["_serPopShow"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || Session["UserCompanyCode"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                try
                {
                    ClearPage();
                    hdfCurrDate.Value = DateTime.Now.ToString("dd/MMM/yyyy");
                    UpdatePath();
                }
                catch (Exception ex)
                {
                    DispMsg("Error Occurred :" + ex.Message, "E");
                }
            }
            else
            {
                if (_serPopShow)
                {
                    PopupSearch.Show();
                }
                else
                {
                    _serPopShow = false;
                    PopupSearch.Hide();
                }
            }
        }
        private void ClearPage()
        {
            try
            {
                List<TextBox> _textBoxList = new List<TextBox>();
                GetControlList<TextBox>(Page.Controls, _textBoxList);
                foreach (var textBox in _textBoxList)
                {
                    textBox.Text = string.Empty;
                }
                _serPopShow = false;
                PopupSearch.Hide();
                txtCompany.Text = Session["UserCompanyCode"].ToString();
                txtCompany_TextChanged(null, null);
                txtRegDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                txtPurDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                string _costText = "Cost", _purText = "Purchase Price",_strCurCd="";
                MasterCompany _mstComp = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                if (_mstComp != null)
                {
                    _strCurCd = _mstComp.Mc_cur_cd;
                    _costText = "Cost (" + _strCurCd + ")";
                    _purText = "Purchase Price(" + _strCurCd + ")";
                }
                lblCost.Text = _costText;
                lblPurPrice.Text = _purText;
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }
       
        private void GetControlList<T>(ControlCollection controlCollection, List<T> resultCollection)
      where T : Control
        {
            foreach (Control control in controlCollection)
            {
                //if (control.GetType() == typeof(T))
                if (control is T) // This is cleaner
                    resultCollection.Add((T)control);

                if (control.HasControls())
                    GetControlList(control.Controls, resultCollection);
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
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCompany.Text))
                {
                    DispMsg("Please select the company !"); txtCompany.Focus(); return;
                }
                if (string.IsNullOrEmpty(txtPartyCd.Text))
                {
                   // DispMsg("Please enter the party code !"); txtPartyCd.Focus(); return;
                }
                if (string.IsNullOrEmpty(txtPartyVal.Text))
                {
                   // DispMsg("Please select the party code !"); txtPartyVal.Focus(); return;
                }
                if (string.IsNullOrEmpty(txtRegNo.Text))
                {
                    DispMsg("Please select the registration # !"); txtRegNo.Focus(); return;
                }
                if (string.IsNullOrEmpty(txtRegDate.Text))
                {
                    DispMsg("Please select the registration Date !"); txtRegDate.Focus(); return;
                }
                if (string.IsNullOrEmpty(txtModel.Text))
                {
                    DispMsg("Please enter the model !"); txtModel.Focus(); return;
                }
                if (string.IsNullOrEmpty(txtMake.Text))
                {
                    DispMsg("Please enter the make !"); txtMake.Focus(); return;
                }
                if (string.IsNullOrEmpty(txtColor.Text))
                {
                    DispMsg("Please select the color !"); txtColor.Focus(); return;
                }
                if (string.IsNullOrEmpty(txtManYear.Text))
                {
                    DispMsg("Please enter the manufacture year !"); txtManYear.Focus(); return;
                }
                _tmpObj = new FleetVehicleMaster();
                _tmpObj.Fv_reg_no = txtRegNo.Text.Trim().ToUpper();
                FleetVehicleMaster _fleet = new FleetVehicleMaster();
                _fleet = CHNLSVC.General.GET_FLT_VEHICLE_DATA(_tmpObj).FirstOrDefault();
                if (_fleet != null)
                {
                    if (ddlOwnerTp.SelectedValue=="OWN")
                    {
                        if (txtCompany.Text.Trim().ToUpper() != Session["UserCompanyCode"].ToString())
                        {
                            DispMsg("Cannot change the company ! Owner Type is Own !"); return;
                        }
                    }
                    if (txtCompany.Text.Trim().ToUpper() != _fleet.Fv_com)
                    {
                      string _msg =  "Reg # has been already created in company ("+_fleet.Fv_com+") ! Do you want to update ?";
                      lblMsg.Text = _msg;
                      SbuPopup.Show();
                    }
                    else
                    {
                        SaveData();
                    }
                }
                else
                {
                    SaveData();
                }
                
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }
        private void SaveData()
        {
            try
            {
                FleetVehicleMaster _obj = new FleetVehicleMaster();
                decimal _tmpDec = 0; Int32 _tmpInt = 0; DateTime _dtTmp = DateTime.MinValue;
                _obj.Fv_chassis_no = txtChasNo.Text;
                _obj.Fv_engine_no = txtEngNo.Text;     
                _obj.Fv_com = txtCompany.Text.Trim().ToUpper();
                //_obj.Fv_pty_val = txtPartyVal.Text.Trim().ToUpper();        
                _obj.Fv_pty_val = "N/A";
                //_obj.Fv_pty_cd = txtPartyCd.Text.Trim().ToUpper();
                _obj.Fv_pty_cd = "N/A";
                _obj.Fv_reg_no = txtRegNo.Text.Trim().ToUpper();
                _obj.Fv_reg_dt = DateTime.TryParse(txtRegDate.Text, out _dtTmp)?Convert.ToDateTime(txtRegDate.Text.Trim()):DateTime.MinValue; 
                _obj.Fv_make = txtMake.Text.Trim();
                _obj.Fv_model = txtModel.Text.Trim();
                _obj.Fv_colour = txtColor.Text.Trim();
                _obj.Fv_tans_tp = ddlTransTp.SelectedValue;
                _obj.Fv_carr_tp = txtCarryTp.Text;
                _obj.Fv_width = decimal.TryParse(txtWidth.Text, out _tmpDec) ? Convert.ToDecimal(txtWidth.Text) : 0;
                _obj.Fv_height = decimal.TryParse(txtHeight.Text, out _tmpDec) ? Convert.ToDecimal(txtHeight.Text) : 0;
                _obj.Fv_length = decimal.TryParse(txtLength.Text, out _tmpDec) ? Convert.ToDecimal(txtLength.Text) : 0;
                _obj.Fv_max_weight = decimal.TryParse(txtMaxWeight.Text, out _tmpDec) ? Convert.ToDecimal(txtMaxWeight.Text) : 0;

                _obj.Fv_driver = txtDriver.Text.Trim();
                _obj.Fv_helper = txtHelper.Text.Trim();
                _obj.Fv_dt_purchase = DateTime.TryParse(txtPurDate.Text, out _dtTmp) ? Convert.ToDateTime(txtPurDate.Text.Trim()) : DateTime.MinValue;
                _obj.Fv_pur_price = decimal.TryParse(txtPurPrice.Text, out _tmpDec) ? Convert.ToDecimal(txtPurPrice.Text) : 0;
                _obj.Fv_eng_cap = txtEngCapacity.Text;
                _obj.Fv_man_year = txtManYear.Text;
                // _obj.Fv_whl_uom        
                _obj.Fv_owner_tp  = ddlOwnerTp.SelectedValue;     
                _obj.Fv_dealer_code = txtDelerCd.Text;   
                //_obj.fv_image          
                //_obj.fv_service_seq    
                //_obj.fv_manf_fuel_cosm 
                _obj.Fv_batt_tp = txtBattryTp.Text;     
                _obj.Fv_no_batt  =Int32.TryParse(txtNoOfBattry.Text, out _tmpInt)?Convert.ToInt32(txtNoOfBattry.Text):0;      
                //_obj.Fv_owner_com      
                //_obj.Fv_man_ref_no     
                
                //_obj.Fv_vehi_cap_uom   
               // _obj.fv_vehi_weight_uom
                _obj.Fv_vehi_cost = decimal.TryParse(txtCost.Text, out _tmpDec) ? Convert.ToDecimal(txtCost.Text) : 0;
                 //_obj.fv_unweight        
              //_obj.fv_grweight       
                _obj.Fv_body_tp = ddlBodyTp.SelectedValue;
                _obj.Fv_country = txtCountry.Text;
                _obj.Fv_class = txtClass.Text;        
                _obj.Fv_tax_class = txtTaxClass.Text;
                _obj.Fv_fuel_tp = txtFuelTp.Text;
                _obj.Fv_pur_tp = ddlPurTp.SelectedValue;
                _obj.Fv_fuel_tnk_cap = decimal.TryParse(txtFuelTankCapacity.Text, out _tmpDec) ? Convert.ToDecimal(txtFuelTankCapacity.Text) : 0;
               // _obj.fv_prov           
                _obj.Fv_stus = chkStus.Checked ? 1 : 0;         
                _obj.Fv_cre_by  = Session["UserID"].ToString();      
                _obj.Fv_cre_dt  = DateTime.Now;      
                _obj.Fv_session_id = Session["SessionID"].ToString();    
                _obj.Fv_mod_by  = Session["UserID"].ToString();       
                _obj.Fv_mod_dt = DateTime.Now;
                _obj.Fv_mod_session_id = Session["SessionID"].ToString();
                _obj.Fv_rem = txtRemark.Text.Trim();
                _obj.Fv_vehi_cap_uom = ddlUomCapacity.SelectedValue;
                _obj.Fv_vehi_weight_uom = ddlUomWeight.SelectedValue;
                _obj.Fv_unweight = decimal.TryParse(txtUnloadeWeight.Text, out _tmpDec) ? Convert.ToDecimal(txtUnloadeWeight.Text) : 0;
                _obj.Fv_grweight = decimal.TryParse(txtGrosWeight.Text, out _tmpDec) ? Convert.ToDecimal(txtGrosWeight.Text) : 0;
                _obj.Fv_max_weight = decimal.TryParse(txtMaxWeight.Text, out _tmpDec) ? Convert.ToDecimal(txtMaxWeight.Text) : 0;
                _obj.Fv_vehi_capacity = _obj.Fv_width * _obj.Fv_height * _obj.Fv_length;

                _obj.Fv_ft_size = decimal.TryParse(txtFTSize.Text, out _tmpDec) ? Convert.ToDecimal(txtFTSize.Text) : 0;
                _obj.Fv_ft_qty = Int32.TryParse(txtFtQty.Text, out _tmpInt) ? Convert.ToInt32(txtFtQty.Text) : 0;

                _obj.Fv_mt_size = decimal.TryParse(txtMTSize.Text, out _tmpDec) ? Convert.ToDecimal(txtMTSize.Text) : 0;
                _obj.Fv_mt_qty = Int32.TryParse(txtMtQty.Text, out _tmpInt) ? Convert.ToInt32(txtMtQty.Text) : 0;

                _obj.Fv_rt_size = decimal.TryParse(txtRtSize.Text, out _tmpDec) ? Convert.ToDecimal(txtRtSize.Text) : 0;
                _obj.Fv_rt_qty = Int32.TryParse(txtRtQty.Text, out _tmpInt) ? Convert.ToInt32(txtRtQty.Text) : 0;
                _obj.Fv_man_ref_no = txtManRef.Text;
                _obj.Fv_vehi_cap_uom = ddlUomCapacity.SelectedValue;
                _obj.Fv_owner_com = txtCompany.Text;
                _obj.Fv_prov = txtProvince.Text.ToUpper();
                _obj.Fv_owner_com = txtOwnBy.Text.ToUpper().Trim();
                string _err="";
                Int32 _res = CHNLSVC.General.SaveFleetVehicleMaster(_obj, out _err);
                if (_res >0)
                {
                    DispMsg("Successfully Saved !","S");
                    ClearPage();
                }
                else
                {
                    DispMsg(_err, "E");
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            ClearPage();
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
                if (_serType == "Company")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                    _serData = CHNLSVC.CommonSearch.GetCompanySearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "RegistrationDet")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RegistrationDet);
                    _serData = CHNLSVC.CommonSearch.SearchFleetRegistrationNO(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "RegistrationDet")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RegistrationDet);
                    _serData = CHNLSVC.CommonSearch.SearchFleetRegistrationNO(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "Color")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Color);
                    _serData = CHNLSVC.CommonSearch.GetItemSearchColorMaster(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "Dealer")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                    _serData = CHNLSVC.CommonSearch.SearchBusEntity(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                    if (_serData.Columns.Contains("NIC"))
                    {
                        _serData.Columns.Remove("NIC");
                    }
                    if (_serData.Columns.Contains("mobile"))
                    {
                        _serData.Columns.Remove("mobile");
                    }
                }
                else if (_serType == "Helper")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Helper);
                    _serData = CHNLSVC.CommonSearch.SearchEmployeeByType(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "Driver")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Driver);
                    _serData = CHNLSVC.CommonSearch.SearchEmployeeByType(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "Country")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
                    _serData = CHNLSVC.CommonSearch.GetCountrySearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "Make")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    _serData = CHNLSVC.CommonSearch.SearchFleetMake(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "Model")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
                    _serData = CHNLSVC.CommonSearch.SearchFleetModel(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "Fuel")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
                    _serData = CHNLSVC.CommonSearch.SearchFleetFuelType(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "Battry")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
                    _serData = CHNLSVC.CommonSearch.SearchFleetBattryType(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "TaxClass")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    _serData = CHNLSVC.CommonSearch.SearchFleetTaxClass(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "VehClass")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    _serData = CHNLSVC.CommonSearch.SearchFleetVehClass(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "Province")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Province);
                    _serData = CHNLSVC.CommonSearch.GetProvinceData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "OwnCompany")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                    _serData = CHNLSVC.CommonSearch.GetCompanySearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                else if (_serType == "OwnCustomer")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    _serData = CHNLSVC.CommonSearch.SearchBusEntity(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
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
                if (_serType == "Company")
                {
                    txtCompany.Text = code;
                    txtCompany_TextChanged(null, null);
                }
                else if (_serType == "RegistrationDet")
                {
                    txtRegNo.Text = code;
                    txtRegNo_TextChanged(null, null);
                }
                else if (_serType == "Color")
                {
                    txtColor.Text = code;
                    txtColor_TextChanged(null, null);
                }
                else if (_serType == "Dealer")
                {
                    txtDelerCd.Text = code;
                    txtDelerCd_TextChanged(null, null);
                }
                else if (_serType == "Helper")
                {
                    txtHelper.Text = code;
                    txtHelper_TextChanged(null, null);
                }
                else if (_serType == "Driver")
                {
                    txtDriver.Text = code;
                    txtDriver_TextChanged(null, null);
                }
                else if (_serType == "Country")
                {
                    txtCountry.Text = code;
                    txtCountry_TextChanged(null, null);
                }
                else if (_serType == "Make")
                {
                    txtMake.Text = code;
                    txtMake_TextChanged(null, null);
                }
                else if (_serType == "Model")
                {
                    txtModel.Text = code;
                    txtModel_TextChanged(null, null);
                }
                else if (_serType == "Fuel")
                {
                    txtFuelTp.Text = code;
                    txtFuelTp_TextChanged(null, null);
                }
                else if (_serType == "Battry")
                {
                    txtBattryTp.Text = code;
                    txtBattryTp_TextChanged(null, null);
                }
                else if (_serType == "TaxClass")
                {
                    txtTaxClass.Text = code;
                    txtTaxClass_TextChanged(null, null);
                }
                else if (_serType == "VehClass")
                {
                    txtClass.Text = code;
                    txtClass_TextChanged(null, null);
                }
                else if (_serType == "CarryerTp")
                {
                    txtCarryTp.Text = code;
                    txtCarryTp_TextChanged(null, null);
                }
                else if (_serType == "Province")
                {
                    txtProvince.Text = code;
                    txtProvince_TextChanged(null, null);
                }
                else if (_serType == "OwnCompany")
                {
                    txtOwnBy.Text = code;
                    txtOwnBy_TextChanged(null, null);
                }
                else if (_serType == "OwnCustomer")
                {
                    txtOwnBy.Text = code;
                    txtOwnBy_TextChanged(null, null);
                }
                _serPopShow = false;
                PopupSearch.Hide();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeCompany_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                _serData = CHNLSVC.CommonSearch.GetCompanySearchData(_para, null, null);
                LoadSearchPopup("Company", "CODE", "ASC");
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
                case CommonUIDefiniton.SearchUserControlType.Province:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Province.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Country:
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
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "D" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "S" + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.Color:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append("");
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
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
        protected void lbtnSePartyVal_Click(object sender, EventArgs e)
        {

        }

        protected void txtCompany_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtCompany.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtCompany.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                    _serData = CHNLSVC.CommonSearch.GetCompanySearchData(_para, "CODE", txtCompany.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtCompany.Text.ToUpper().Trim(), "CODE", "Description");
                    txtCompany.ToolTip = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtCompany.Text = string.Empty;
                        txtCompany.Focus();
                        DispMsg("Please enter valid company !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtRegNo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRegNo.Text))
            {
               _tmpObj = new FleetVehicleMaster();
                _tmpObj.Fv_reg_no = txtRegNo.Text.Trim().ToUpper();
                FleetVehicleMaster _fleet = new FleetVehicleMaster();
                _fleet = CHNLSVC.General.GET_FLT_VEHICLE_DATA(_tmpObj).FirstOrDefault();
                if (_fleet != null)
                {
                    if (_fleet.Fv_com != Session["UserCompanyCode"].ToString())
                    {
                        string _msg = "Cannot view details ! Entered Reg # belongs to " + _fleet.Fv_com +" !";
                        DispMsg(_msg); txtRegNo.Text = ""; txtRegNo.Focus(); return;
                    }
                } 
            }
            LoadData();
        }
        private void ClearVehicleData()
        {
            txtCompany.Text = "";
            txtPartyCd.Text = "";
            txtPartyVal.Text = "";
            //txtRegDate.Text = "";
            txtMake.Text = "";
            txtModel.Text = "";
            txtColor.Text = "";
            txtManYear.Text = "";
            ddlTransTp.SelectedIndex = 0;
            ddlOwnerTp.SelectedIndex = 0;
            txtCarryTp.Text = "";
            txtWidth.Text = "";
            txtHeight.Text = "";
            txtLength.Text = "";
            txtMaxWeight.Text = "";
            txtDriver.Text = "";
            txtHelper.Text = "";
            txtPurDate.Text = "";
            txtPurPrice.Text = "";
            ddlPurTp.SelectedIndex = 0;
            ddlOwnerTp.SelectedIndex = 0;
            txtEngNo.Text = "";
            txtChasNo.Text = "";
           // txtOwnerTp.Text = "";
            txtDelerCd.Text = "";
            txtBattryTp.Text = "";
            txtNoOfBattry.Text = "";
            txtEngCapacity.Text = "";
            txtFuelTankCapacity.Text = "";
            txtCost.Text = "";
            //txtBodyTp.Text = "";
            ddlBodyTp.SelectedIndex = 0;
            txtCountry.Text = "";
            txtClass.Text = "";
            txtTaxClass.Text = "";
            txtRemark.Text = "";
            txtFuelTankCapacity.Text = "";
            chkStus.Checked = true;
            txtFTSize.Text = "";
            txtFtQty.Text = "";
            txtMTSize.Text = "";
            txtMtQty.Text = "";
            txtRtSize.Text = "";
            txtRtQty.Text = "";
            txtRegDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtPurDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtCompany.Text = Session["UserCompanyCode"].ToString();
        }
        private void LoadData()
        {
            _tmpObj = new FleetVehicleMaster();
           // _tmpObj.Fv_com = txtCompany.Text.Trim().ToUpper();
            //_tmpObj.Fv_pty_val = txtPartyVal.Text.Trim().ToUpper();
           // _tmpObj.Fv_pty_cd = txtPartyCd.Text.Trim().ToUpper();
            _tmpObj.Fv_reg_no = txtRegNo.Text.Trim().ToUpper();
            FleetVehicleMaster _fleet = new FleetVehicleMaster();
            _fleet = CHNLSVC.General.GET_FLT_VEHICLE_DATA(_tmpObj).FirstOrDefault();
            ClearVehicleData();
            if (_fleet != null)
            {
               //txtCompany.Text= _fleet.Fv_com;
               txtPartyCd.Text= _fleet.Fv_pty_cd;
               txtPartyVal.Text= _fleet.Fv_pty_val;
               //txtRegNo.Text= _fleet.Fv_reg_no;
               txtRegDate.Text= _fleet.Fv_reg_dt.ToString("dd/MMM/yyyy");
               txtMake.Text= _fleet.Fv_make;
               txtModel.Text= _fleet.Fv_model;
               txtColor.Text= _fleet.Fv_colour;
               txtManYear.Text= _fleet.Fv_man_year;
               ///.Text= _fleet.Fv_eng_cap;
               ddlTransTp.SelectedIndex = ddlTransTp.Items.IndexOf(ddlTransTp.Items.FindByValue(_fleet.Fv_tans_tp));
              // txtCarryTp.SelectedIndex = txtCarryTp.Items.IndexOf(txtCarryTp.Items.FindByValue(_fleet.Fv_carr_tp));
               //txtTransTp.Text= _fleet.Fv_tans_tp;
               txtCarryTp.Text = _fleet.Fv_carr_tp;
               txtWidth.Text= _fleet.Fv_width.ToString("N2");
               txtHeight.Text= _fleet.Fv_height.ToString("N2");
               txtLength.Text= _fleet.Fv_length.ToString("N2");
               txtMaxWeight.Text = _fleet.Fv_max_weight.ToString("N2");
               ///txtf.Text= _fleet.Fv_fuel_tp;
              /// txtCom.Text= _fleet.Fv_fuel_cosm;
               txtDriver.Text= _fleet.Fv_driver;
               txtHelper.Text= _fleet.Fv_helper;
              /// txtCom.Text= _fleet.Fv_curr_meter;
               ///txtCom.Text= _fleet.Fv_last_fuel_meter;
               ///txtCom.Text= _fleet.Fv_last_fuel_dt;
               txtPurDate.Text= _fleet.Fv_dt_purchase.ToString("dd/MMM/yyyy");
              /// txtCom.Text= _fleet.Fv_pur_stus;
               txtPurPrice.Text= _fleet.Fv_pur_price.ToString("N2");
              // txtPurTp.Text= _fleet.Fv_pur_tp;
               ddlPurTp.SelectedIndex = ddlPurTp.Items.IndexOf(ddlPurTp.Items.FindByValue(_fleet.Fv_pur_tp));
              /// txtCom.Text= _fleet.Fv_fuel_tnk_cap;
               txtEngNo.Text= _fleet.Fv_engine_no;
               txtChasNo.Text= _fleet.Fv_chassis_no;
               ///txtCom.Text= _fleet.Fv_whl_uom;
               ddlOwnerTp.SelectedIndex = ddlOwnerTp.Items.IndexOf(ddlOwnerTp.Items.FindByValue(_fleet.Fv_dealer_code));
               //txtOwnerTp.Text = _fleet.Fv_owner_tp;
               txtDelerCd.Text= _fleet.Fv_dealer_code;
              // /txtCom.Text= _fleet.Fv_image;
               ///txtCom.Text= _fleet.Fv_service_seq;
              /// txtCompany.Text= _fleet.Fv_manf_fuel_cosm;
               txtBattryTp.Text= _fleet.Fv_batt_tp;
              // ddlBatryTp.SelectedIndex = ddlBatryTp.Items.IndexOf(ddlBatryTp.Items.FindByValue(_fleet.Fv_batt_tp));
               txtNoOfBattry.Text= _fleet.Fv_no_batt.ToString();
              /// txtCom.Text= _fleet.Fv_owner_com;
               txtEngCapacity.Text= _fleet.Fv_eng_cap;
               txtFuelTankCapacity.Text= _fleet.Fv_fuel_tnk_cap.ToString();
              // txtCom.Text= _fleet.Fv_vehi_weight_uom;
               txtCost.Text= _fleet.Fv_vehi_cost.ToString("N2");
              // txtCom.Text= _fleet.Fv_unweight;
              // txtCom.Text= _fleet.Fv_grweight;
              // txtBodyTp.Text= _fleet.Fv_body_tp;
               ddlBodyTp.SelectedIndex = ddlBodyTp.Items.IndexOf(ddlBodyTp.Items.FindByValue(_fleet.Fv_body_tp));
               txtCountry.Text= _fleet.Fv_country;
               txtClass.Text= _fleet.Fv_class;
               txtTaxClass.Text= _fleet.Fv_tax_class;
               txtRemark.Text = _fleet.Fv_rem;
               txtFuelTankCapacity.Text = _fleet.Fv_fuel_tnk_cap.ToString();
                chkStus.Checked = _fleet.Fv_stus==1? true:false;
               // txtVehCap.Text = _fleet.Fv_vehi_capacity.ToString("N");
                txtCompany.Text = _fleet.Fv_com;
                txtCompany_TextChanged(null, null);
                txtUnloadeWeight.Text = _fleet.Fv_unweight.ToString("N2");
                txtGrosWeight.Text = _fleet.Fv_grweight.ToString("N2");
                txtMaxWeight.Text = _fleet.Fv_max_weight.ToString("N2");

                txtFTSize.Text = _fleet.Fv_ft_size.ToString("N2");
                txtFtQty.Text = _fleet.Fv_ft_qty.ToString();

                txtMTSize.Text = _fleet.Fv_mt_size.ToString("N2");
                txtMtQty.Text = _fleet.Fv_mt_qty.ToString();

                txtRtSize.Text = _fleet.Fv_rt_size.ToString("N2");
                txtRtQty.Text = _fleet.Fv_rt_qty.ToString();
                txtFuelTp.Text = _fleet.Fv_fuel_tp;
                txtManRef.Text = _fleet.Fv_man_ref_no;
            }
        }

        protected void lbtnSeRegNo_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCompany.Text))
                {
                    //DispMsg("Please select the company !"); txtCompany.Focus(); return;
                }
                if (string.IsNullOrEmpty(txtPartyCd.Text))
                {
                    //DispMsg("Please select the party code !"); txtPartyCd.Focus(); return;
                }
                if (string.IsNullOrEmpty(txtPartyVal.Text))
                {
                    //DispMsg("Please select the party value !"); txtPartyVal.Focus(); return;
                }
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RegistrationDet);
                _serData = CHNLSVC.CommonSearch.SearchFleetRegistrationNO(_para, null, null);
                LoadSearchPopup("RegistrationDet", "REGISTER NO", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeColor_Click(object sender, EventArgs e)
        {
            _serData = new DataTable();
            _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Color);
            _serData = CHNLSVC.CommonSearch.GetItemSearchColorMaster(_para, null, null);
            LoadSearchPopup("Color", "CODE", "ASC");
        }

        protected void txtColor_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtColor.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtColor.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Color);
                    _serData = CHNLSVC.CommonSearch.GetItemSearchColorMaster(_para, "CODE", txtColor.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtColor.Text.ToUpper().Trim(), "CODE", "Description");
                    txtColor.ToolTip = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtColor.Text = string.Empty;
                        txtColor.Focus();
                        DispMsg("Please enter valid color !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                SaveData();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message,"E");
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";
                SbuPopup.Hide();
            }
            catch (Exception ex)
            {
                 DispMsg(ex.Message);
            }
        }

        protected void lbtnSeHelper_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Helper);
                _serData = CHNLSVC.CommonSearch.SearchEmployeeByType(_para, null, null);
                LoadSearchPopup("Helper", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeDriver_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Driver);
                _serData = CHNLSVC.CommonSearch.SearchEmployeeByType(_para, null, null);
                LoadSearchPopup("Driver", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeDeler_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                _serData = CHNLSVC.CommonSearch.SearchBusEntity(_para, null, null);
                if (_serData.Columns.Contains("NIC"))
                {
                    _serData.Columns.Remove("NIC");
                }
                if (_serData.Columns.Contains("mobile"))
                {
                    _serData.Columns.Remove("mobile");
                }
                LoadSearchPopup("Dealer", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtDelerCd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtDelerCd.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtDelerCd.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                    _serData = CHNLSVC.CommonSearch.SearchBusEntity(_para, "CODE", txtDelerCd.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtDelerCd.Text.ToUpper().Trim(), "CODE", "Name");
                    txtDelerCd.ToolTip = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtDelerCd.Text = string.Empty;
                        txtDelerCd.Focus();
                        DispMsg("Please enter valid dealer !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtHelper_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtHelper.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtHelper.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Helper);
                    _serData = CHNLSVC.CommonSearch.SearchEmployeeByType(_para, "CODE", txtHelper.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtHelper.Text.ToUpper().Trim(), "CODE", "Name");
                    txtHelper.ToolTip = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtHelper.Text = string.Empty;
                        txtHelper.Focus();
                        DispMsg("Please enter valid dealer !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtDriver_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtDriver.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtDriver.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Driver);
                    _serData = CHNLSVC.CommonSearch.SearchEmployeeByType(_para, "CODE", txtDriver.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtDriver.Text.ToUpper().Trim(), "CODE", "Name");
                    txtDriver.ToolTip = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtDriver.Text = string.Empty;
                        txtDriver.Focus();
                        DispMsg("Please enter valid driver !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeCountry_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
                _serData = CHNLSVC.CommonSearch.GetCountrySearchData(_para, null, null);
                LoadSearchPopup("Country", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtCountry_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtCountry.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtCountry.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
                    _serData = CHNLSVC.CommonSearch.GetCountrySearchData(_para, "CODE", txtCountry.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtCountry.Text.ToUpper().Trim(), "CODE", "Description");
                    txtCountry.ToolTip = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtCountry.Text = string.Empty;
                        txtCountry.Focus();
                        DispMsg("Please enter valid country !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtModel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtModel.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtModel.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                    _serData = CHNLSVC.CommonSearch.SearchFleetModel(_para, "CODE", txtModel.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtModel.Text.ToUpper().Trim(), "CODE", "Description");
                    txtModel.ToolTip = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtModel.Text = string.Empty;
                        txtModel.Focus();
                        DispMsg("Please enter valid model !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeModel_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                _serData = CHNLSVC.CommonSearch.SearchFleetModel(_para, null, null);
                LoadSearchPopup("Model", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtMake_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtMake.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtMake.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
                    _serData = CHNLSVC.CommonSearch.SearchFleetMake(_para, "CODE", txtMake.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtMake.Text.ToUpper().Trim(), "CODE", "Description");
                    txtMake.ToolTip = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtMake.Text = string.Empty;
                        txtMake.Focus();
                        DispMsg("Please enter valid make !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSEMake_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                _serData = CHNLSVC.CommonSearch.SearchFleetMake(_para, null, null);
                LoadSearchPopup("Make", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtBattryTp_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtBattryTp.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtBattryTp.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
                    _serData = CHNLSVC.CommonSearch.SearchFleetBattryType(_para, "CODE", txtBattryTp.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtBattryTp.Text.ToUpper().Trim(), "CODE", "Description");
                    txtBattryTp.ToolTip = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtBattryTp.Text = string.Empty;
                        txtBattryTp.Focus();
                        DispMsg("Please enter valid fuel type !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeBattrTp_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                _serData = CHNLSVC.CommonSearch.SearchFleetBattryType(_para, null, null);
                LoadSearchPopup("Battry", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeFulTp_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                _serData = CHNLSVC.CommonSearch.SearchFleetFuelType(_para, null, null);
                LoadSearchPopup("Fuel", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtFuelTp_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtFuelTp.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtFuelTp.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
                    _serData = CHNLSVC.CommonSearch.SearchFleetFuelType(_para, "CODE", txtFuelTp.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtFuelTp.Text.ToUpper().Trim(), "CODE", "Description");
                    txtFuelTp.ToolTip = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtFuelTp.Text = string.Empty;
                        txtFuelTp.Focus();
                        DispMsg("Please enter valid fuel type !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtTaxClass_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtTaxClass.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtTaxClass.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Country);
                    _serData = CHNLSVC.CommonSearch.SearchFleetTaxClass(_para, "CODE", txtTaxClass.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtTaxClass.Text.ToUpper().Trim(), "CODE", "Description");
                    txtTaxClass.ToolTip = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtTaxClass.Text = string.Empty;
                        txtTaxClass.Focus();
                        DispMsg("Please enter valid tax class !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeTaxClass_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                _serData = CHNLSVC.CommonSearch.SearchFleetTaxClass(_para, null, null);
                LoadSearchPopup("TaxClass", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtClass_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtFuelTp.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtClass.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    _serData = CHNLSVC.CommonSearch.SearchFleetVehClass(_para, "CODE", txtClass.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtClass.Text.ToUpper().Trim(), "CODE", "Description");
                    txtClass.ToolTip = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtClass.Text = string.Empty;
                        txtClass.Focus();
                        DispMsg("Please enter valid vahicle class !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeClass_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                _serData = CHNLSVC.CommonSearch.SearchFleetVehClass(_para, null, null);
                LoadSearchPopup("VehClass", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtCarryTp_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtCarryTp.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtCarryTp.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    _serData = CHNLSVC.CommonSearch.SearchFleetVehCarryerTp(_para, "CODE", txtCarryTp.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtCarryTp.Text.ToUpper().Trim(), "CODE", "Description");
                    txtCarryTp.ToolTip = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtCarryTp.Text = string.Empty;
                        txtCarryTp.Focus();
                        DispMsg("Please enter valid type !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeCarrTy_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                _serData = CHNLSVC.CommonSearch.SearchFleetVehCarryerTp(_para, null, null);
                LoadSearchPopup("CarryerTp", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void txtProvince_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtProvince.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtProvince.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Province);
                    _serData = CHNLSVC.CommonSearch.GetProvinceData(_para, "CODE", txtProvince.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtProvince.Text.ToUpper().Trim(), "CODE", "Description");
                    txtProvince.ToolTip = _ava ? _toolTip : "";
                    if (!_ava)
                    {
                        txtProvince.Text = string.Empty;
                        txtProvince.Focus();
                        DispMsg("Please enter valid province !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSeProvince_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Province);
                _serData = CHNLSVC.CommonSearch.GetProvinceData(_para, null, null);
                LoadSearchPopup("Province", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

       
        protected void txtOwnBy_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtOwnBy.ToolTip = string.Empty;
                if (!string.IsNullOrEmpty(txtOwnBy.Text))
                {
                    if (ddlOwnerTp.SelectedValue == "OWN")
                    {
                        _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                        _serData = CHNLSVC.CommonSearch.GetCompanySearchData(_para, "CODE", txtOwnBy.Text.ToUpper().Trim());
                        DataAvailable(_serData, txtOwnBy.Text.ToUpper().Trim(), "CODE", "Description");
                        txtOwnBy.ToolTip = _ava ? _toolTip : "";
                        if (!_ava)
                        {
                            txtOwnBy.Text = string.Empty;
                            txtOwnBy.Focus();
                            DispMsg("Please enter valid owner !");
                            return;
                        }
                    }
                    else
                    {
                        _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                        _serData = CHNLSVC.CommonSearch.SearchBusEntity(_para, "CODE", txtOwnBy.Text.ToUpper().Trim());
                        DataAvailable(_serData, txtOwnBy.Text.ToUpper().Trim(), "CODE", "NAME");
                        txtOwnBy.ToolTip = _ava ? _toolTip : "";
                        if (!_ava)
                        {
                            txtOwnBy.Text = string.Empty;
                            txtOwnBy.Focus();
                            DispMsg("Please enter valid owner !");
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

        protected void lbtnSeOwnBy_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlOwnerTp.SelectedValue=="OWN")
                {
                    _serData = new DataTable();
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                    _serData = CHNLSVC.CommonSearch.GetCompanySearchData(_para, null, null);
                    LoadSearchPopup("OwnCompany", "CODE", "ASC");
                }
                else
                {
                    _serData = new DataTable();
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    _serData = CHNLSVC.CommonSearch.SearchBusEntity(_para, null, null);
                    LoadSearchPopup("OwnCustomer", "CODE", "ASC");
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        protected void lbtnUploadImg_Click(object sender, EventArgs e)
        {
            popImgUpload.Show();
        }

        protected void lbtnUpload_Click1(object sender, EventArgs e)
        {
            
        }

        protected void ddlOwnerTp_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtOwnBy.Text = "";
        }

        protected void lbtnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload.HasFile)
            {
                try
                {
                    if (string.IsNullOrEmpty(txtRegNo.Text))
                    {
                       // DispMsg("Please select a registration #");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('Please select a registration #');", true);
                        return;
                    }
                    string strpath = System.IO.Path.GetExtension(FileUpload.FileName);
                    if (strpath != ".jpg" && strpath != ".jpeg" && strpath != ".gif" && strpath != ".png")
                    {
                        //DispMsg("Please select a image ");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('Only image formats (jpg, png, gif) are accepted ');", true);
                        popImgUpload.Show();
                        // UserPopoup.Hide();
                        return;
                    }
                    // string fileName = Path.GetFileName(FileUpload.PostedFile.FileName);
                    string fileName = txtModel.Text.Trim() + ".png";
                    FileUpload.PostedFile.SaveAs(Server.MapPath("~/Temp/Fleet/") + fileName);
                    //BindData();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('Image successfully uploaded');", true);
                }
                catch (Exception ex)
                {
                    //log exception

                }
            }
        }

        protected void lbtnClose_Click(object sender, EventArgs e)
        {

        }
        private void UpdatePath()
        {
            //use a default path
            virtualPath = "~/Temp/Fleet";
            physicalPath = Server.MapPath(virtualPath);

            //If ImageFolderPath is specified then use that path
            if (!string.IsNullOrEmpty(ImageFolderPath))
            {
                physicalPath = Server.MapPath(ImageFolderPath);
                virtualPath = ImageFolderPath;
            }

            soucePath = "@C:/Fleet";
        }
    }
}