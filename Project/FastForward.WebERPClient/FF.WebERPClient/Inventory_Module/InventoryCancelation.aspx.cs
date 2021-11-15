using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Data;
using System.Text;
using System.Globalization;
using System.IO;

namespace FF.WebERPClient.Inventory_Module
{
    public partial class InventoryCancelation : BasePage
    {

        protected Int16 Term
        {
            get { return (Int16)Session["Term"]; }
            set { Session["Term"] = value; }
        }
        protected List<HpSheduleDetails> CurrentSchedule
        {
            get { return (List<HpSheduleDetails>)Session["CurrentSchedule"]; }
            set { Session["CurrentSchedule"] = value; }
        }
        protected List<HpSheduleDetails> NewSchedule
        {
            get { return (List<HpSheduleDetails>)Session["NewSchedule"]; }
            set { Session["NewSchedule"] = value; }
        }

        #region Bind Data

        private void BindRequestTypesDDLData(DropDownList ddlType)
        {
            ddlType.Items.Clear();
            List<MasterType> _masterType = CHNLSVC.General.GetOutwardTypes();
            _masterType.Add(new MasterType { Mtp_cd = "-1", Mtp_desc = "" });
            ddlType.DataSource = _masterType.OrderBy(items => items.Mtp_desc);
            ddlType.DataTextField = "Mtp_desc";
            ddlType.DataValueField = "Mtp_cd";
            ddlType.DataBind();
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindRequestTypesDDLData(ddlDocType); //Load pending outward entries
                txtDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");

            }
        }

        protected void ddlDocNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlDocType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #region Cancel Button
        protected void btnCancel_Click(object sender, EventArgs e)
        {
      
        }
        #endregion




    }
}