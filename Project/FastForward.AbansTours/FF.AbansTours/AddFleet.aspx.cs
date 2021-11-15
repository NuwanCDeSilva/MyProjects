using FF.AbansTours.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FF.AbansTours
{
    public partial class AddFleet : BasePage
    {
        string _userid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    PopulateDropDowns();
                    ddlStatus.SelectedValue = "1";
                }

                txtRegistrationNo.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ValidateFleet()
        {
            int selectedCount = clstprofitcenter.Items.Cast<ListItem>().Count(li => li.Selected);
            if (string.IsNullOrEmpty(txtRegistrationNo.Text))
            {
                Div1.Visible = true;
                Label1.Text = "Please enter registration No !!!";
                return false;
            }
            if (string.IsNullOrEmpty(txtBrand.Text))
            {
                Div1.Visible = true;
                Label1.Text = "Please enter brand !!!";
                return false;
            }
            if (string.IsNullOrEmpty(txtModel.Text))
            {
                Div1.Visible = true;
                Label1.Text = "Please enter model !!!";
                return false;
            }
            if (ddlType.SelectedIndex == 0)
            {
                Div1.Visible = true;
                Label1.Text = "Select type !!!";
                return false;
            }
            if (string.IsNullOrEmpty(txtSIPPCode.Text))
            {
                Div1.Visible = true;
                Label1.Text = "Please enter SIPP code !!!";
                return false;
            }
            if (string.IsNullOrEmpty(txtStartMeter.Text))
            {
                Div1.Visible = true;
                Label1.Text = "Please enter start meter !!!";
                return false;
            }
            if (string.IsNullOrEmpty(txttourdate.Text))
            {
                Div1.Visible = true;
                Label1.Text = "Please select tour selected date !!!";
                return false;
            }
            if (ddlOwner.SelectedIndex == 0)
            {
                Div1.Visible = true;
                Label1.Text = "Please select owner !!!";
                return false;
            }
            if (string.IsNullOrEmpty(txtOwnerName.Text))
            {
                Div1.Visible = true;
                Label1.Text = "Please enter owner name !!!";
                return false;
            }
            if (string.IsNullOrEmpty(txtOwnerContactNo.Text))
            {
                Div1.Visible = true;
                Label1.Text = "Please enter owner contact No !!!";
                return false;
            }
            if (string.IsNullOrEmpty(txtLastServiceMeter.Text))
            {
                Div1.Visible = true;
                Label1.Text = "Please enter Last service meter !!!";
                return false;
            }
            
            if (string.IsNullOrEmpty(txtTouristBoardRegno.Text))
            {
                Div1.Visible = true;
                Label1.Text = "Please enter tourist board regno !!!";
                return false;
            }
            if (string.IsNullOrEmpty(rbtislease.SelectedValue))
            {
                Div1.Visible = true;
                Label1.Text = "Please select is vehicle lease or not !!!";
                return false;
            }
            if (string.IsNullOrEmpty(txtInsurenceExpDate.Text))
            {
                Div1.Visible = true;
                Label1.Text = "Please select insurance exp date !!!";
                return false;
            }
            if (string.IsNullOrEmpty(txtRevenueExpDate.Text))
            {
                Div1.Visible = true;
                Label1.Text = "Please select revenue exp date !!!";
                return false;
            }
            if (ddlFuelType.SelectedIndex == 0)
            {
                Div1.Visible = true;
                Label1.Text = "Please select fuel type !!!";
                return false;
            }
            
            if (string.IsNullOrEmpty(txtengcapacity.Text))
            {
                Div1.Visible = true;
                Label1.Text = "Please enter engine capacity !!!";
                return false;
            }
            if (string.IsNullOrEmpty(txtnoofseats.Text))
            {
                Div1.Visible = true;
                Label1.Text = "Please enter number of seats !!!";
                return false;
            }
            if (selectedCount == 0)
            {
                Div1.Visible = true;
                Label1.Text = "Please select at least 1 profit center !!!";
                return false;
            }

            if (ddlStatus.SelectedIndex == 0)
            {
                Div1.Visible = true;
                Label1.Text = "Please select status !!!";
                return false;
            }
            return true;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DivAsk.Visible = false;
                Div1.Visible = false;
                DivInfo.Visible = false;
                bool isvalid = ValidateFleet();
                if (isvalid == false)
                {
                    return;
                }

                _userid = (string)Session["UserID"];
                string p_mstf_regno = txtRegistrationNo.Text.Trim().ToUpper();
                DateTime p_mstf_dt = Convert.ToDateTime(txttourdate.Text.Trim());
                string p_mstf_brd = txtBrand.Text.Trim();
                string p_mstf_model = txtModel.Text.Trim();
                string p_mstf_veh_tp = ddlType.SelectedValue;
                string p_mstf_sipp_cd = txtSIPPCode.Text.Trim();
                Int32 p_mstf_st_meter = Convert.ToInt32(txtStartMeter.Text.Trim());
                string p_mstf_own = ddlOwner.SelectedValue;
                string p_mstf_own_nm = txtOwnerName.Text.Trim();
                Int32 p_mstf_own_cont = Convert.ToInt32(txtOwnerContactNo.Text.Trim());
                Int32 p_mstf_lst_sermet = Convert.ToInt32(txtLastServiceMeter.Text.Trim());
                string p_mstf_tou_regno = txtTouristBoardRegno.Text.Trim();
                Int32 p_mstf_is_lease = Convert.ToInt32(rbtislease.SelectedValue);
                DateTime p_mstf_insu_exp = Convert.ToDateTime(txtInsurenceExpDate.Text.Trim());
                DateTime p_mstf_reg_exp = Convert.ToDateTime(txtRevenueExpDate.Text.Trim());
                string p_mstf_fual_tp = ddlFuelType.SelectedValue;
                Int32 p_mstf_act = Convert.ToInt32(ddlStatus.SelectedValue);
                string p_mstf_cre_by = _userid;
                DateTime p_mstf_cre_dt = CHNLSVC.Security.GetServerDateTime();
                string p_mstf_mod_by = _userid;
                DateTime p_mstf_mod_dt = CHNLSVC.Security.GetServerDateTime();
                string p_mstf_engin_cap = txtengcapacity.Text.Trim();
                Int32 p_mstf_noof_seat = Convert.ToInt32(txtnoofseats.Text.Trim());

                System.Threading.Thread.Sleep(1000);

                Int32 result = CHNLSVC.Tours.sp_tours_update_fleet(p_mstf_regno, p_mstf_dt, p_mstf_brd, p_mstf_model, p_mstf_veh_tp, p_mstf_sipp_cd, p_mstf_st_meter, p_mstf_own, p_mstf_own_nm, p_mstf_own_cont, p_mstf_lst_sermet, p_mstf_tou_regno, p_mstf_is_lease, p_mstf_insu_exp, p_mstf_reg_exp, p_mstf_fual_tp, p_mstf_act, _userid, p_mstf_cre_dt, p_mstf_mod_by, p_mstf_mod_dt, p_mstf_engin_cap, p_mstf_noof_seat, null, null, null);

                SaveProfitCenters();

                if (result == 1)
                {
                    DivAsk.Visible = true;
                    Div1.Visible = false;
                    lblAsk.Text = "Successfully saved !!!";
                    txtRegistrationNo.Text = string.Empty;
                    Clear();
                }
                else
                {
                    Div1.Visible = true;
                    Label1.Text = "Not saved !!!";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PopulateDropDowns()
        {
            try
            {
                DataTable dtvehicletype = CHNLSVC.Tours.Get_Vehicle_Type();
                ddlType.DataSource = dtvehicletype;
                ddlType.DataTextField = "MVT_DESC";
                ddlType.DataValueField = "MVT_CD";

                if (dtvehicletype.Rows.Count > 0)
                {
                    ddlType.DataBind();
                }
                ddlType.Items.Insert(0, new ListItem("Select", ""));
                ddlType.SelectedIndex = 0;

                DataTable dtprofitcenter = CHNLSVC.Tours.Get_mst_profit_center(base.GlbUserComCode);
                clstprofitcenter.DataSource = dtprofitcenter;
                clstprofitcenter.DataTextField = "MPC_CD";
                clstprofitcenter.DataValueField = "MPC_CD";

                if (dtprofitcenter.Rows.Count > 0)
                {
                    clstprofitcenter.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtRegistrationNo.Text = string.Empty;
                Div1.Visible = false;
                DivAsk.Visible = false;
                DivInfo.Visible=false;
                Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Clear()
        {
            try
            {
                txtBrand.Text = string.Empty;
                txtModel.Text = string.Empty;
                ddlType.SelectedIndex = 0;
                txtSIPPCode.Text = string.Empty;
                txtStartMeter.Text = string.Empty;
                ddlOwner.SelectedValue = "Select";
                txtOwnerName.Text = string.Empty;
                txtOwnerContactNo.Text = string.Empty;
                txtLastServiceMeter.Text = string.Empty;
                txtTouristBoardRegno.Text = string.Empty;
                txtInsurenceExpDate.Text = string.Empty;
                txtRevenueExpDate.Text = string.Empty;
                ddlFuelType.SelectedValue = "Select";
                ddlStatus.SelectedValue = "Select";
                txtengcapacity.Text = string.Empty;
                txtnoofseats.Text = string.Empty;
                txttourdate.Text = string.Empty;

                int count = clstprofitcenter.Items.Count;
                for (int i = 0; i < count; i++)
                {
                    if (clstprofitcenter.Items[i].Selected == true)
                    {
                        clstprofitcenter.Items[i].Selected = false;
                    }
                }

                rbtislease.ClearSelection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lbtColse_Click(object sender, EventArgs e)
        {
            DivAsk.Visible = false;
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Div1.Visible = false;
        }

        protected void txtRegistrationNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Div1.Visible = false;
                DivAsk.Visible = false;
                DataTable dtfleet = CHNLSVC.Tours.Get_Fleet(txtRegistrationNo.Text.Trim());
                if (dtfleet.Rows.Count > 0)
                {
                    foreach (DataRow item in dtfleet.Rows)
                    {
                        txtBrand.Text = item[3].ToString();
                        txtModel.Text = item[4].ToString();
                        ddlType.SelectedValue = item[5].ToString();
                        txtSIPPCode.Text = item[6].ToString();
                        txtStartMeter.Text = item[7].ToString();
                        ddlOwner.SelectedValue = item[8].ToString();
                        txtOwnerName.Text = item[9].ToString();
                        txtOwnerContactNo.Text = item[10].ToString();
                        txtLastServiceMeter.Text = item[11].ToString();
                        txtTouristBoardRegno.Text = item[12].ToString();
                        rbtislease.SelectedValue = item[13].ToString();
                        ddlFuelType.SelectedValue = item[16].ToString();
                        ddlStatus.SelectedValue = item[17].ToString();
                        txtengcapacity.Text = item[22].ToString();
                        txtnoofseats.Text = item[23].ToString();

                        DateTime tourdate = Convert.ToDateTime(item[2].ToString());
                        string tourdatetext = tourdate.ToString("dd/MMM/yyyy");
                        txttourdate.Text = tourdatetext;

                        DateTime Insuredate = Convert.ToDateTime(item[14].ToString());
                        string Insuredatetxt = Insuredate.ToString("dd/MMM/yyyy");
                        txtInsurenceExpDate.Text = Insuredatetxt;

                        DateTime revexdate = Convert.ToDateTime(item[15].ToString());
                        string revexdatetxt = revexdate.ToString("dd/MMM/yyyy");
                        txtRevenueExpDate.Text = revexdatetxt;
                    }
                    LoadProfitCenters();
                    DivInfo.Visible = true;
                    lblinfo.Text = "Recod alredy there";
                    txtRegistrationNo.Focus();
                }
                else
                {
                    Clear();
                    ddlStatus.SelectedValue = "1";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DivInfo.Visible = false;
        }

        private void SaveProfitCenters()
        {
            try
            {
                foreach (ListItem item in clstprofitcenter.Items)
                {
                    _userid = (string)Session["UserID"];
                    string p_mfa_regno = txtRegistrationNo.Text.Trim();
                    string p_mfa_cre_by = _userid;
                    DateTime p_mfa_cre_dt = CHNLSVC.Security.GetServerDateTime();
                    string p_mfa_mod_by = _userid;
                    DateTime p_mfa_mod_dt = CHNLSVC.Security.GetServerDateTime();
                    Int32 result;
                    if (item.Selected)
                    {
                        string selectedValue = item.Value;
                        string p_mfa_pc = selectedValue;
                        Int32 p_mfa_act = 1;
                        result = CHNLSVC.Tours.sp_tours_update_fleet_alloc(p_mfa_regno, p_mfa_pc, p_mfa_act, p_mfa_cre_by, p_mfa_cre_dt, p_mfa_mod_by, p_mfa_mod_dt);
                    }
                    else
                    {
                        string unselectedValue = item.Value;
                        string p_mfa_pc = unselectedValue;
                        Int32 p_mfa_act = 0;
                        DataTable dtfleetalloc = CHNLSVC.Tours.sp_tours_get_fleet_alloc(txtRegistrationNo.Text.Trim(), p_mfa_pc);
                        if (dtfleetalloc.Rows.Count > 0)
                        {
                            result = CHNLSVC.Tours.sp_tours_update_fleet_alloc(p_mfa_regno, p_mfa_pc, p_mfa_act, p_mfa_cre_by, p_mfa_cre_dt, p_mfa_mod_by, p_mfa_mod_dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadProfitCenters()
        {
            try
            {
                int count = clstprofitcenter.Items.Count;
                for (int i = 0; i < count; i++)
                {
                    if (clstprofitcenter.Items[i].Selected == true)
                    {
                        clstprofitcenter.Items[i].Selected = false;
                    }
                }
                DataTable dtfleetallocset = CHNLSVC.Tours.sp_tours_get_fleet_alloc2(txtRegistrationNo.Text.Trim());
                if (dtfleetallocset.Rows.Count > 0)
                {
                    foreach (DataRow item in dtfleetallocset.Rows)
                    {
                        foreach (ListItem itemlist in clstprofitcenter.Items)
                        {
                            try
                            {
                                clstprofitcenter.Items.FindByValue(item[2].ToString()).Selected = true;
                            }
                            catch 
                            {
                                continue;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.DriverTBS:
                    {
                        paramsText.Append(base.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(base.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllVehicles:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void imgbtnveclecode_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BasePage basepage = new BasePage();
                Page page = (Page)this.Page;
                uc_CommonSearch ucc = (uc_CommonSearch)page.Master.FindControl("uc_CommonSearchMaster");
                ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllVehicles);
                DataTable dataSource = basepage.CHNLSVC.CommonSearch.SearchAllVehicles(ucc.SearchParams, null, null);
                ucc.BindUCtrlDDLData(dataSource);
                ucc.BindUCtrlGridData(dataSource);
                ucc.ReturnResultControl = txtRegistrationNo.ClientID;
                ucc.UCModalPopupExtender.Show();
                bool enable = false;
                ucc.DateEnable(enable);
                txtRegistrationNo.Focus();
            }
            catch (Exception er)
            {
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('" + er.Message + "');", true);
            }
        }

        protected void ddlOwner_TextChanged(object sender, EventArgs e)
        {
            if (ddlOwner.Text == "Internal")
            {
                txtOwnerName.Text = "Abans Tours";
            }
            else if (ddlOwner.Text == "Kangaroo")
            {
                txtOwnerName.Text = "Kangaroo Cabs";
            }
            else if (ddlOwner.Text == "Europ")
            {
                txtOwnerName.Text = "Europ Car";
            }
            else if (ddlOwner.Text == "External")
            {
                txtOwnerName.Text = "";
            }
        }
    }
}