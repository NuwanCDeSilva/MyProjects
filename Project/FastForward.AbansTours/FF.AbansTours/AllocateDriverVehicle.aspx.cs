using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FF.AbansTours
{
    public partial class AllocateDriverVehicle : BasePage
    {
        string _userid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    PopulateDropDowns();
                    chkactive.Checked = true;
                    DateTime fromdate = DateTime.Now;
                    DateTime todate = DateTime.Now;
                    txtfrom.Text = fromdate.ToString("dd/MMM/yyyy");
                    txtto.Text = fromdate.ToString("dd/MMM/yyyy"); 
                }
                gridallocations.SelectedIndexChanged += this.gridallocations_SelectedIndexChanged;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private bool ValidateAllocation()
        {
            if (ddlvehicle.SelectedIndex == 0)
            {
                Div1.Visible = true;
                Label1.Text = "Please select a vehicle !!!";
                return false;
            }

            if (ddldriver.SelectedIndex == 0)
            {
                Div1.Visible = true;
                Label1.Text = "Please select a driver !!!";
                return false;
            }

            if (string.IsNullOrEmpty(txtfrom.Text))
            {
                Div1.Visible = true;
                Label1.Text = "Please select from date !!!";
                return false;
            }

            if (string.IsNullOrEmpty(txtto.Text))
            {
                Div1.Visible = true;
                Label1.Text = "Please select to date !!!";
                return false;
            }

            if (Convert.ToDateTime(txtfrom.Text) > Convert.ToDateTime(txtto.Text))
            {
                Div1.Visible = true;
                Label1.Text = "Please select valid from to date !!!";
                return false;
            }
            return true;
        }

        private void PopulateDropDowns()
        {
            try
            {
                DataTable dtvehicle = CHNLSVC.Tours.SP_TOURS_GET_VEHICLE(base.GlbUserDefProf);
                ddlvehicle.DataSource = dtvehicle;
                ddlvehicle.DataTextField = "MFA_REGNO";
                ddlvehicle.DataValueField = "MFA_REGNO";

                if (dtvehicle.Rows.Count > 0)
                {
                    ddlvehicle.DataBind();
                }
                ddlvehicle.Items.Insert(0, new ListItem("Select", ""));
                ddlvehicle.SelectedIndex = 0;

                DataTable dtdriver = CHNLSVC.Tours.SP_TOURS_GET_DRIVER(base.GlbUserDefProf);
                ddldriver.DataSource = dtdriver;
                ddldriver.DataTextField = "MEMP_FIRST_NAME";
                ddldriver.DataValueField = "MPE_EPF";

                if (dtdriver.Rows.Count > 0)
                {
                    ddldriver.DataBind();
                }
                ddldriver.Items.Insert(0, new ListItem("Select", ""));
                ddldriver.SelectedIndex = 0;
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
                ddlvehicle.SelectedIndex = 0;
                ddldriver.SelectedIndex = 0;
                txtfrom.Text = string.Empty;
                txtto.Text = string.Empty;
                chkactive.Checked = false;

                DateTime fromdate = DateTime.Now;
                DateTime todate = DateTime.Now;
                txtfrom.Text = fromdate.ToString("dd/MMM/yyyy");
                txtto.Text = fromdate.ToString("dd/MMM/yyyy");
                chkactive.Checked = true;
                Div1.Visible = false;

                gridallocations.DataSource = null;
                gridallocations.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DivAsk.Visible = false;
                Div1.Visible = false;
                bool isvalid = ValidateAllocation();
                if (isvalid == false)
                {
                    return;
                }

                bool isoverlap = CheckOverLapDates();
                if (isoverlap == true)
                {
                    Div1.Visible = true;
                    Label1.Text = "Either this vehicle or driver has another active allocation with in this period !!!";
                    return;
                }
                _userid = (string)Session["UserID"];
                Int32 checkvalue = 0;
                if (chkactive.Checked)
                {
                    checkvalue = 1;
                }
                else
                {
                    checkvalue = 0;
                }

                string drivercom = (string)Session["DRIVERCOM"];

                string p_mfd_seq = (string)Session["SEQNO"];
                string p_mfd_veh_no = ddlvehicle.SelectedValue.ToUpper();
                string p_mfd_dri = ddldriver.SelectedValue;
                Int32 p_mfd_act = checkvalue;
                DateTime p_mfd_frm_dt = Convert.ToDateTime(txtfrom.Text.Trim());
                DateTime p_mfd_to_dt = Convert.ToDateTime(txtto.Text.Trim());
                string p_mfd_cre_by = _userid;
                DateTime p_mfd_cre_dt = CHNLSVC.Security.GetServerDateTime();
                string p_mfd_mod_by = _userid;
                DateTime p_mfd_mod_dt = CHNLSVC.Security.GetServerDateTime();
                string p_mfd_com = drivercom;
                string p_mfd_pc = base.GlbUserDefProf;

                Int32 result = CHNLSVC.Tours.sp_tours_update_driver_alloc(p_mfd_seq, p_mfd_veh_no, p_mfd_dri, p_mfd_act, p_mfd_frm_dt, p_mfd_to_dt, p_mfd_cre_by, p_mfd_cre_dt, p_mfd_mod_by, p_mfd_mod_dt, p_mfd_com, p_mfd_pc);

                if (result == 1)
                {
                    Div1.Visible = false;
                    DivAsk.Visible = true;
                    lblAsk.Text = "Successfully saved !!!";
                    
                    GetAllocations();
                    btnaddnew.Visible = false;
                    ddlvehicle.Enabled = true;
                    ddldriver.Enabled = true;
                    Clear();
                    Session["SEQNO"] = null;
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

        protected void lbtColse_Click(object sender, EventArgs e)
        {
            DivAsk.Visible = false;
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Div1.Visible = false;
        }

        protected void ddldriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtdrivercom = CHNLSVC.Tours.SP_TOURS_GET_DRIVER_COM(ddldriver.SelectedValue);
                if (dtdrivercom.Rows.Count > 0)
                {
                    foreach (DataRow item in dtdrivercom.Rows)
                    {
                        Session["DRIVERCOM"] = item[0].ToString();
                    }
                }
                GetAllocations();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetAllocations()
        {
            try
            {
                DataTable dtallocations;
                dtallocations = CHNLSVC.Tours.SP_TOURS_GET_ALLOCATIONS(ddlvehicle.SelectedValue, ddldriver.SelectedValue);
                gridallocations.DataSource = dtallocations;

                if (dtallocations.Rows.Count > 0)
                {
                    gridallocations.DataBind();
                }
                else
                {
                    gridallocations.DataSource = null;
                    gridallocations.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlvehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetAllocations();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gridallocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            DivAsk.Visible = false;
            Div1.Visible = false;
            try
            {
                Int32 isactive;
                string pvehicle = gridallocations.SelectedRow.Cells[1].Text.ToUpper();
                string pdriver = gridallocations.SelectedRow.Cells[3].Text;
                string isactivetext = gridallocations.SelectedRow.Cells[4].Text;
                string p_mfd_seq_no = gridallocations.SelectedRow.Cells[7].Text;

                Session["SEQNO"] = p_mfd_seq_no;

                if (isactivetext == "Active")
                {
                    isactive = 1;
                }
                else
                {
                    isactive = 0;
                }
                DateTime pfrom = Convert.ToDateTime(gridallocations.SelectedRow.Cells[5].Text);
                DateTime pto = Convert.ToDateTime(gridallocations.SelectedRow.Cells[6].Text);

                if (ddlvehicle.Items.FindByText(pvehicle) == null)
                {
                    Div1.Visible = true;
                    Label1.Text = "Vehicle is deactivated !!!";
                    return;
                }

                if (ddldriver.Items.FindByValue(pdriver) == null)
                {
                    Div1.Visible = true;
                    Label1.Text = "Driver is deactivated !!!";
                    return;
                }


                DataTable dtallocatefleet = CHNLSVC.Tours.sp_tours_get_Selected_alloc(pvehicle,pdriver,isactive,pfrom,pto);
                if (dtallocatefleet.Rows.Count > 0)
                {
                    foreach (DataRow item in dtallocatefleet.Rows)
                    {
                        ddlvehicle.SelectedValue = pvehicle;
                        ddldriver.SelectedValue = pdriver;

                        string fromdate = pfrom.ToString("dd/MMM/yyyy");
                        txtfrom.Text = fromdate;

                        string todate = pto.ToString("dd/MMM/yyyy");
                        txtto.Text = todate;

                        if (isactivetext == "Active")
                        {
                            chkactive.Checked = true;
                        }
                        else
                        {
                            chkactive.Checked = false;
                        }
                    }
                }
                ddldriver.Enabled = false;
                ddlvehicle.Enabled = false;
                btnaddnew.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CheckOverLapDates()
        {
            try
            {
                DateTime fromdate = Convert.ToDateTime(txtfrom.Text.Trim());
                DateTime todate = Convert.ToDateTime(txtto.Text.Trim());
                string p_mfd_seq_no = (string)Session["SEQNO"];

                DataTable dtoverlapdates =  new DataTable();

                if (string.IsNullOrEmpty(p_mfd_seq_no))
                {
                    dtoverlapdates = CHNLSVC.Tours.SP_TOURS_GET_ALL_OVERLAP_DATES(ddlvehicle.SelectedValue, ddldriver.SelectedValue, fromdate, todate);
                }
                else
                {
                    dtoverlapdates = CHNLSVC.Tours.SP_TOURS_GET_OVERLAP_DATES(ddlvehicle.SelectedValue, ddldriver.SelectedValue, fromdate, todate, p_mfd_seq_no);
                }

                
                if (dtoverlapdates.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnaddnew_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                ddldriver.Enabled = true;
                ddlvehicle.Enabled = true;
                btnaddnew.Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}