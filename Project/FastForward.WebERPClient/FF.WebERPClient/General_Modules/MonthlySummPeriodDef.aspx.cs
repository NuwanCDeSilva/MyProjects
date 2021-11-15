using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using FF.BusinessObjects;


namespace FF.WebERPClient.General_Modules
{
    public partial class MonthlySummPeriodDef : BasePage
    {
        public List<GnrWeek> GnrWeekList
        {
            get { return (List<GnrWeek>)ViewState["GnrWeekList"]; }
            set { ViewState["GnrWeekList"] = value; }
        }
        public List<GnrWeek> Show_GnrWeekList
        {
            get { return (List<GnrWeek>)ViewState["Show_GnrWeekList"]; }
            set { ViewState["Show_GnrWeekList"] = value; }
        }

        //-----------------------------------------------------------
        //ArrearsDateDef
        public List<ArrearsDateDef> ArrDateDefList
        {
            get { return (List<ArrearsDateDef>)ViewState["ArrDateDefList"]; }
            set { ViewState["ArrDateDefList"] = value; }
        }
        public List<ArrearsDateDef> Show_ArrDateDefList
        {
            get { return (List<ArrearsDateDef>)ViewState["Show_ArrDateDefList"]; }
            set { ViewState["Show_ArrDateDefList"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {           
            if(!IsPostBack)
            {
                DataTable dt = new DataTable();
                grvSoSDef.DataSource = dt;
                grvSoSDef.DataBind();

                GnrWeekList = new List<GnrWeek>();
                Show_GnrWeekList = new List<GnrWeek>();

                //----------------------------
                DataTable dt2 = new DataTable();
                grvGrace.DataSource = dt2;
                grvGrace.DataBind();

                ArrDateDefList = new List<ArrearsDateDef>();
                Show_ArrDateDefList = new List<ArrearsDateDef>();
            }
        }
        protected void btnGetDocDet_Click(object sender, EventArgs e)
        { 
        
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try { 
                DateTime fromDt1 = Convert.ToDateTime(txtFrom.Text.Trim()).Date;
                DateTime toDt1 = Convert.ToDateTime(txtTo.Text.Trim()).Date;
                DateTime MON_YR= Convert.ToDateTime(txtMonthYear_.Text.Trim());
            }
            catch(Exception EX){
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Dates with valid formats!");
                return;
            }
            Int32 WEEK = Convert.ToInt32(ddlWeek.SelectedValue);

            var _duplicate = from _dup in Show_GnrWeekList
                             where _dup.Gw_week == WEEK
                             select _dup;
            if (_duplicate.Count() > 0)
            {
                string Msg = "<script>alert('Week already added!');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                return;
            }
            
            addNewToGrid();

            DateTime fromDt = Convert.ToDateTime(txtFrom.Text.Trim()).Date;
            DateTime toDt = Convert.ToDateTime(txtTo.Text.Trim()).Date;
            int diffDays = (toDt.Date - fromDt.Date).Days;
            txtFrom.Text = fromDt.AddDays(diffDays+1).ToShortDateString();     

            DateTime lastDayoFMonth = Convert.ToDateTime(txtMonthYear_.Text.Trim()).AddMonths(1).AddDays(-1);
            txtTo.Text = lastDayoFMonth.ToShortDateString();
            
            grvSoSDef.DataSource = Show_GnrWeekList;
            grvSoSDef.DataBind();

            
        }

        private void addNewToGrid()
        {
            GnrWeek new_Week = new GnrWeek();
            new_Week.Gw_cre_by = GlbUserName;
            new_Week.Gw_cre_dt = DateTime.Now.Date;
            new_Week.Gw_from_dt = Convert.ToDateTime(txtFrom.Text.Trim()).Date;
            new_Week.Gw_to_dt = Convert.ToDateTime(txtTo.Text.Trim()).Date;
            new_Week.Gw_week = Convert.ToInt32(ddlWeek.SelectedValue);
            new_Week.Gw_month = Convert.ToDateTime(txtMonthYear_.Text.Trim()).Month;
            new_Week.Gw_year = Convert.ToDateTime(txtMonthYear_.Text.Trim()).Year;

            GnrWeekList.Add(new_Week);
            Show_GnrWeekList.Add(new_Week);
           
        }

        protected void txtMonthYear__TextChanged(object sender, EventArgs e)
        {
            string monthYear = txtMonthYear_.Text.Trim();
            try
            {
                DateTime DTmonth_ = Convert.ToDateTime(monthYear);
            }
            catch (Exception ex)
            {
                return;
            }
            DateTime DTmonth = Convert.ToDateTime(monthYear);
            GnrWeekList = new List<GnrWeek>();
            txtFrom.Text = string.Empty;
            txtTo.Text = string.Empty;

            Show_GnrWeekList = CHNLSVC.Financial.Get_ListOfWeeks_on_month(DTmonth.Month, DTmonth.Year, 0);
            if (Show_GnrWeekList == null)
            {
                Show_GnrWeekList = new List<GnrWeek>();
                txtFrom.Text = Convert.ToDateTime(txtMonthYear_.Text.Trim()).ToShortDateString();
            }
            else
            {
                int maxwEEK = (from c in Show_GnrWeekList select c.Gw_week).Max();
                foreach(GnrWeek WK in Show_GnrWeekList)
                {
                    if(WK.Gw_week ==maxwEEK)
                    {
                        txtFrom.Text = WK.Gw_to_dt.AddDays(1).ToShortDateString();
                    }
                }
                ////////////////////////////////////
                try {
                    ddlWeek.SelectedValue = (maxwEEK + 1).ToString();
                }
                catch(Exception EX){  }
            }
            grvSoSDef.DataSource = Show_GnrWeekList;
            grvSoSDef.DataBind();
           
            DateTime lastDayoFMonth = Convert.ToDateTime(txtMonthYear_.Text.Trim()).AddMonths(1).AddDays(-1);
            txtTo.Text = lastDayoFMonth.ToShortDateString();
        }

        protected void grvSoSDef_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 rowIndex= e.RowIndex;          
            Int32 Week = Convert.ToInt32(grvSoSDef.Rows[rowIndex].Cells[1].Text);
            if (GnrWeekList.Count>0)
            {
                int maxwEEK = (from c in GnrWeekList select c.Gw_week).Max();
                if (Week != maxwEEK)
                {
                    string Msg = "<script>alert('Only last record can be deleted!');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                    return;
                }
            }           
            //////////////////////////////////////////////////////////////////////////////////////////////
            var _duplicate = from _dup in GnrWeekList
                             where _dup.Gw_week == Week
                            // where _dup.Sccd_itm == obj.Sccd_itm && _dup.Sccd_brd == obj.Sccd_brd
                             select _dup;
            if (_duplicate.Count() > 0)
            {
                Show_GnrWeekList.RemoveAt(rowIndex);
                GnrWeekList.RemoveAll(x => x.Gw_week == Week);
            }
            else
            {
                string Msg = "<script>alert('Cannot delete records already saved in the database!');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            /////////////////////////////////////////////////////////////////////////////////////////////          
            
            grvSoSDef.DataSource = Show_GnrWeekList;
            grvSoSDef.DataBind(); 
        }

        protected void btnSaveSoS_Click(object sender, EventArgs e)
        {
           Int32 eff= CHNLSVC.Financial.Save_gnr_week(GnrWeekList);
           if (eff > 0)
           {
               this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Saved!");
               string Msg = "<script>alert('Successfully Saved!');</script>";
               ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

           }
           else
           {
               string Msg = "<script>alert('Error Occured. Failed to save!');</script>";
               ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
           }
        }

        protected void rdoCom_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rdoCannel_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rdoSubChannel_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rdoArea_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rdoRegion_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rdoZone_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rdoPc_CheckedChanged(object sender, EventArgs e)
        {

        }

        private List<ArrearsDateDef> getOldGraceDef(DateTime arrDate)
        {
            DateTime lastDayOFMonth = Convert.ToDateTime(txtMonthYear_gc.Text.Trim()).AddMonths(1).AddDays(-1);
            List<ArrearsDateDef> _defHeaderList = new List<ArrearsDateDef>();
            _defHeaderList = CHNLSVC.Financial.Get_ArrearsDateDef(GlbUserComCode, lastDayOFMonth, string.Empty, string.Empty);

            return _defHeaderList;         
        }

        protected void txtMonthYear_gc_TextChanged(object sender, EventArgs e)
        {           
            try {
                DateTime monthDate1 = Convert.ToDateTime(txtMonthYear_gc.Text.Trim());
            }
            catch(Exception EX){
                txtMonthYear_gc.Text = string.Empty;
                return;
            }
            ArrDateDefList = new List<ArrearsDateDef>();
            DateTime monthDate = Convert.ToDateTime(txtMonthYear_gc.Text.Trim());
            Show_ArrDateDefList= getOldGraceDef(monthDate);
            if (Show_ArrDateDefList == null)
            {
                Show_ArrDateDefList = new List<ArrearsDateDef>();
            }

            grvGrace.DataSource = Show_ArrDateDefList;
            grvGrace.DataBind();

            DateTime lastDayOFMonth = Convert.ToDateTime(txtMonthYear_gc.Text.Trim()).AddMonths(1).AddDays(-1);
            txtAsAtDt.Text = lastDayOFMonth.ToShortDateString();
            txtSuppDt.Text = string.Empty;
            txtGraceDt.Text = string.Empty;

            uc_ProfitCenterSearch1.Clear();
            uc_ProfitCenterSearch1.Company = GlbUserComCode;
        }

        protected void ImgBtnAdd_Click(object sender, ImageClickEventArgs e)
        {
            try{
                 DateTime monthDate = Convert.ToDateTime(txtMonthYear_gc.Text.Trim());
                 DateTime suppDt =Convert.ToDateTime(txtSuppDt.Text.Trim());
                 DateTime graceDt =Convert.ToDateTime(txtGraceDt.Text.Trim()); 
            }
            catch(Exception ex){

                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Dates with valid formats!");
                return;              
            }

            DateTime lastDayOFMonth = Convert.ToDateTime(txtMonthYear_gc.Text.Trim()).AddMonths(1).AddDays(-1);
            if (Convert.ToDateTime(txtSuppDt.Text.Trim()) < lastDayOFMonth)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Supplimentory date should be grater than As-at Date!");
                return;
            }
            if (Convert.ToDateTime(txtGraceDt.Text.Trim())< Convert.ToDateTime(txtSuppDt.Text.Trim()) )
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Grace date should be grater than As-at Date!");
                return;
            }

            string partyTp=string.Empty;
            string partyCd=string.Empty;
            if(rdoArea.Checked)
            {
                partyTp="AREA";
                partyCd = uc_ProfitCenterSearch1.Area;
            }
            if(rdoCannel.Checked)
            {
                partyTp="CHNL";
                partyCd = uc_ProfitCenterSearch1.Channel;
            }
            if(rdoCom.Checked)
            {
                partyTp="COM";
                partyCd = uc_ProfitCenterSearch1.Company;
            }
            if(rdoPc.Checked)
            {
                partyTp="PC";
                partyCd = uc_ProfitCenterSearch1.ProfitCenter;
            }
            if(rdoRegion.Checked)
            {
                partyTp="REG";
                partyCd = uc_ProfitCenterSearch1.Region;
            }
            if(rdoSubChannel.Checked) 
            {
                partyTp="SCHNL";
                partyCd = uc_ProfitCenterSearch1.SubChannel;
            }
            if(rdoZone.Checked) 
            {
                partyTp="ZONE";
                partyCd = uc_ProfitCenterSearch1.Zone;
            }
            if (partyTp == string.Empty)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Select Party Type!");
                return;
            }
            if (partyCd == string.Empty)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Select Party!");
                return;
            }
            ArrearsDateDef ArrDef = new ArrearsDateDef();
            ArrDef.Hadd_ars_dt = lastDayOFMonth;
            ArrDef.Hadd_cre_by = GlbUserName;
            ArrDef.Hadd_cre_dt = DateTime.Now.Date;
            ArrDef.Hadd_grc_dt = Convert.ToDateTime(txtGraceDt.Text.Trim()).Date;
            ArrDef.Hadd_pty_cd = partyCd;
            ArrDef.Hadd_pty_tp = partyTp;
            ArrDef.Hadd_sup_dt = Convert.ToDateTime(txtSuppDt.Text.Trim()).Date;

            var _duplicate = from _dup in Show_ArrDateDefList
                             where _dup.Hadd_pty_cd == partyCd && _dup.Hadd_pty_tp == partyTp
                             select _dup;
            if (_duplicate.Count() > 0)
            {
                string Msg = "<script>alert('Definition already added!');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                return;
            }
            else
            {
                ArrDateDefList.Add(ArrDef);
                Show_ArrDateDefList.Add(ArrDef);

                grvGrace.DataSource = Show_ArrDateDefList;
                grvGrace.DataBind();
            }
        }

        protected void btnSaveGrace_Click(object sender, EventArgs e)
        {
           Int32 eff= CHNLSVC.Financial.Save_hpr_ars_dt_defn(ArrDateDefList);
           if (eff > 0)
           {
               this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Saved!");
               string Msg = "<script>alert('Successfully Saved!');</script>";
               ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

           }
           else
           {
               string Msg = "<script>alert('Error Occured. Failed to save!');</script>";
               ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
              
           }
        }

        protected void grvGrace_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 rowIndex = e.RowIndex;
            string parytTp = grvGrace.Rows[rowIndex].Cells[1].Text;
            string parytCd = grvGrace.Rows[rowIndex].Cells[2].Text;
           
            
            var _duplicate = from _dup in ArrDateDefList
                             where _dup.Hadd_pty_tp == parytTp && _dup.Hadd_pty_cd == parytCd                         
                             select _dup;
            if (_duplicate.Count() >0)
            {
                Show_ArrDateDefList.RemoveAt(rowIndex);
                ArrDateDefList.RemoveAll(x => x.Hadd_pty_tp == parytTp && x.Hadd_pty_cd == parytCd);
            }
            else
            {
                string Msg = "<script>alert('Cannot delete records already saved in the database!');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            
            grvGrace.DataSource = Show_GnrWeekList;
            grvGrace.DataBind(); 
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/General_Modules/MonthlySummPeriodDef.aspx");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }
    }
}