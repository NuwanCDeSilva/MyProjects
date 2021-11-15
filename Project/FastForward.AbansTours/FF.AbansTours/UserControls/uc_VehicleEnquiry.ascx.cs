using MKB.TimePicker;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FF.AbansTours.UserControls
{
    public partial class uc_VehicleEnquery : System.Web.UI.UserControl
    {
        private BasePage _basePage;
        public DropDownList VehicleType
        {
            get { return ddlVehicleType;}
            set { ddlVehicleType=value; }
        }
        public DateTime ExpectedDate
        {
            get { return Convert.ToDateTime(txtExpectedDate.Text); }
            set { txtExpectedDate.Text = value.ToString("dd/MMM/yyyy"); }
        }

        public TimeSelector TmExpect
        {
            get { return tmExpect; }
            set { tmExpect = value; }
        }
       
        //public string Driver
        //{
        //    get { return txtDriver.Text; }
        //    set { txtDriver.Text = value; }
        //}
        public string Vehicle
        {
            get { return txtVehicle.Text; }
            set { txtVehicle.Text = value; }
        }

       
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["ShowUcVehicleInquiry"] = "N";
            }
           // tmExpect.SetTime(1, 23, MKB.TimePicker.TimeSelector.AmPmSpec.AM);
        }
        public void DisplayParentData()
        {
            ddlVehicleType.SelectedIndex = VehicleType.SelectedIndex;
            txtVehicle.Text = Vehicle;
            txtExpectedDate.Text = ExpectedDate.ToString("dd/MMM/yyyy");
            BindGridData();
        }

        public void BindGridData()
        {
            try
            {
                dgvInquery.DataSource = new int[] { };
                dgvNotAllocate.DataSource = new int[] { };
                dgvInquery.DataBind();
                dgvNotAllocate.DataBind();
                _basePage = new BasePage();
                string _com = (string)Session["UserCompanyCode"];
                string _pc = (string)Session["UserDefProf"];
                DateTime date = new DateTime();
                try
                {

                    TimeSpan time = new TimeSpan(0, tmExpect.Hour, tmExpect.Minute, 0);
                    DateTime dt = Convert.ToDateTime(txtExpectedDate.Text).Add(time);
                    date = Convert.ToDateTime(txtExpectedDate.Text).Add(time);
                }
                catch (Exception)
                {
                    DisplayMessages("Expected date is invalid !"); return;
                }
                string day = chkDay.Checked ? "1" : "0";
                DataTable dt1 = _basePage.CHNLSVC.CommonSearch.GetVahicleAllocationEnquiryData(_com, _pc, ddlVehicleType.SelectedValue, txtVehicle.Text,
                    date, day);
                DataTable dt2 = _basePage.CHNLSVC.CommonSearch.GetVahiclesNotAllocated(_pc);
                //dgvInquery.DataSource = new int[] { };
                //dgvNotAllocate.DataSource = new int[] { };
                if (dt1.Rows.Count > 0)
                {
                    dgvInquery.DataSource = dt1;    
                }
                if (dt2.Rows.Count > 0)
                {
                    dgvNotAllocate.DataSource = dt2;
                }
                dgvInquery.DataBind();
                dgvNotAllocate.DataBind();
            }
            catch (Exception)
            {
                DisplayMessages("Error Occurred while processing !"); return;
            }
        }
        private void DisplayMessages(string message)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            Session["ShowUcVehicleInquiry"] = "Y";
            BindGridData();
        }
    }
}