using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Enquiries.Imports
{
    public partial class Demurrage_Tracker :Base
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageClear();
            }
            else
            {
                
            }
            
        }
        List<MasterType> _type = new List<MasterType>();
        List<DemurrageTracker> _DemuList = new List<DemurrageTracker>();
        decimal[] yValues;
        string[] xValues;

        private void PageClear()
        {
            lbtnFullchart.Visible = false;
            txtFromDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            txtToDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            grdBlDetails.DataSource = new int[] { };
            grdBlDetails.DataBind();
            DemurrageDetails.DataSource = new int[] { };
            DemurrageDetails.DataBind();
            _type = new List<MasterType>();
            yValues = new decimal[] { };
            xValues = new string[] { };
            //chart();
            Image1.Visible = true;
        }

        #region Modalpopup
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string ID = grdResult.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "BLHeader")
            {
                txtBlNo.Text = ID;
                lblvalue.Text = "";
                return;
            }
           
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;

            if (lblvalue.Text == "BLHeader")
            {
                DateTime? ToO_SEND_DT = null;
                DataTable result = CHNLSVC.CommonSearch.GetDemurage_Para(Session["UserCompanyCode"].ToString(), null, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
                result.Columns.Remove("ib_supp_cd");
                result.Columns.Remove("ib_consi_cd");
                result.Columns.Remove("ib_eta");
                result.Columns.Remove("ib_etd");
                result.Columns.Remove("mbe_name");
                result.Columns.Remove("icet_actl_rt");
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
           
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {

            if (lblvalue.Text == "BLHeader")
            {
                DateTime? ToO_SEND_DT = null;
                DataTable result = CHNLSVC.CommonSearch.GetDemurage_Para(Session["UserCompanyCode"].ToString(), txtSearchbyword.Text, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
                result.Columns.Remove("ib_supp_cd");
                result.Columns.Remove("ib_consi_cd");
                result.Columns.Remove("ib_eta");
                result.Columns.Remove("ib_etd");
                result.Columns.Remove("mbe_name");
                result.Columns.Remove("icet_actl_rt");
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            
            if (lblvalue.Text == "BLHeader")
            {
                DateTime? ToO_SEND_DT = null;
                DataTable result = CHNLSVC.CommonSearch.GetDemurage_Para(Session["UserCompanyCode"].ToString(), txtSearchbyword.Text, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
                result.Columns.Remove("ib_supp_cd");
                result.Columns.Remove("ib_consi_cd");
                result.Columns.Remove("ib_eta");
                result.Columns.Remove("ib_etd");
                result.Columns.Remove("mbe_name");
                result.Columns.Remove("icet_actl_rt");
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }

        }
        #endregion
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.BLHeader:
                    {
                        // paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserSBU"].ToString() + seperator);
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserSBU"].ToString() + seperator + 0 + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
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
        private void DisplayMessage(String Msg, Int32 option)
        {
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
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

        protected void lbtnRequestNo_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime? ToO_SEND_DT = null;
                DataTable result = CHNLSVC.CommonSearch.GetDemurage_Para(Session["UserCompanyCode"].ToString(), null, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
                result.Columns.Remove("ib_supp_cd");
                result.Columns.Remove("ib_consi_cd");
                result.Columns.Remove("ib_eta");
                result.Columns.Remove("ib_etd");
                result.Columns.Remove("mbe_name");
                result.Columns.Remove("icet_actl_rt");
                grdResult.DataSource = result;
                grdResult.DataBind();
                BindUCtrlDDLData(result);
                lblvalue.Text = "BLHeader";
                UserPopoup.Show();
            }

            catch (Exception ex) 
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }

        }

        protected void txtBlNo_TextChanged(object sender, EventArgs e)
        {
              ImportsBLHeader oHeader = new ImportsBLHeader();
              oHeader = CHNLSVC.Financial.GET_BL_HEADER_BY_DOC(Session["UserCompanyCode"].ToString(), txtBlNo.Text.Trim(), "A");
              if (oHeader == null)
              {
                  string _Msg = "Please select a valid B/L number";
                  DisplayMessage(_Msg, 2);
                  return;
              }
              lbtnSearchall_Click(null, null);
              //chart();
        }

        private void Info()
        {
            DataTable _sumery = new DataTable();
             DataRow dr = null;
             _sumery.Columns.Add(new DataColumn("mtp_desc", typeof(string)));
             _sumery.Columns.Add(new DataColumn("Percentage", typeof(string)));
            string _FilterDocs = string.Empty;
            DataTable _newvalues = new DataTable();
            DataTable result = CHNLSVC.CommonSearch.GetDemurage_Para(Session["UserCompanyCode"].ToString(), txtBlNo.Text, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
            object sumObject;
            sumObject = result.Compute("Sum(ICET_ACTL_RT)", "");

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    if (string.IsNullOrWhiteSpace(_FilterDocs))
                    {
                        _FilterDocs = row["BL"].ToString();
                    }
                    else
                    {
                        _FilterDocs = _FilterDocs + "," + row["BL"].ToString();
                    }

                }
               DataTable _DemurrageCountTbl = CHNLSVC.CommonSearch.GetDemurrageCount(_FilterDocs);
               _type = CHNLSVC.General.GetAllType("DEMU");
                var result1 = from row in _DemurrageCountTbl.AsEnumerable()
                              group row by row.Field<string>("IDI_DOC_NO")
                               into grp
                             select new
                             {                                 
                                 TeamID = grp.Key,
                                 MemberCount = grp.Count()
                             };
                _newvalues= _DemurrageCountTbl;
                foreach (var t in result1)
                {
                    foreach(DataRow row in _newvalues.Rows){
                        if(row["IDI_DOC_NO"].ToString()==t.TeamID){
                            decimal Amount = Convert.ToDecimal(row["ICET_ACTL_RT"].ToString());
                            decimal newamount  = Amount / t.MemberCount;

                            row["ICET_ACTL_RT"]=newamount;
                        }
                    }
                }


              
                    Dictionary<string, decimal> dicSum = new Dictionary<string, decimal>();
                    foreach (DataRow row in _DemurrageCountTbl.Rows)
                    {
                        string group = row["MTP_DESC"].ToString();
                        decimal rate = Convert.ToDecimal(row["ICET_ACTL_RT"]);
                        if (dicSum.ContainsKey(group))
                            dicSum[group] += rate;
                           
                        else
                            dicSum.Add(group, rate);
                          
                    }
                    int index = 0;
                    decimal total = 0;
                    xValues = new string[dicSum.Count + 1];
                    yValues = new decimal[dicSum.Count + 1];
                    foreach (string g in dicSum.Keys)
                    {
                        decimal per = dicSum[g];
                        total = total + per;
                        decimal value = (per / Convert.ToDecimal(sumObject.ToString())) * 100;
                       // yValues = _DemurrageCountTbl.AsEnumerable().Select(r => r.Field<decimal>(value.ToString())).ToArray();
                        string newvalue =DoFormat(value);
                        yValues[index] = Convert.ToDecimal(newvalue);

                        
                        xValues[index] = g;
                        dr = _sumery.NewRow();
                        dr["mtp_desc"] = g;
                        dr["Percentage"] = newvalue + "%";
                        _sumery.Rows.Add(dr);
                        index++;
                        if (dicSum.Count == index)
                        {
                            decimal nonallocate = Convert.ToDecimal(sumObject.ToString()) - total;
                            decimal perc = (nonallocate / Convert.ToDecimal(sumObject.ToString())) * 100;
                            string newperc = DoFormat(perc);
                            yValues[index] = Convert.ToDecimal(newperc);

                           
                            xValues[index] = "Non-Allocated";
                            dr = _sumery.NewRow();
                            dr["mtp_desc"] = "Non-Allocated";
                            dr["Percentage"] = newperc + "%";
                            _sumery.Rows.Add(dr);
                        }
                        
                    }
             
                    //foreach (DataRow row in dt.Rows)
                    //{
                    //    decimal Blamount = Convert.ToDecimal(row["ICET_ACTL_RT"].ToString());
                    //    sumBl = Blamount + sumBl;
                    //}
                                            
                    //foreach (DataRow row in _DemurrageCountTbl.Rows)
                    //{
                       
                    //    var _advalue = _type.SingleOrDefault(x => x.Mtp_cd == row["idi_tp"].ToString());
                    //    if (_advalue != null)  
                    //    {
                    //        yValues = _DemurrageCountTbl.AsEnumerable().Select(r => r.Field<decimal>("Percentage")).ToArray();
                    //        xValues = _DemurrageCountTbl.AsEnumerable().Select(r => r.Field<string>("mtp_desc")).ToArray();

                    //        _advalue.Percentage = row["Percentage"].ToString() + "%";



                    //    }
                      
                    //}
                   


                    ViewState["yValues"] = yValues.ToArray();
                    ViewState["xValues"] = xValues.ToArray();
                    grdBlDetails.DataSource = result;
                    grdBlDetails.DataBind();
                    ViewState["BlDetails"] = result;

                    foreach (GridViewRow gvrow in grdBlDetails.Rows)
                    {
                        Label lbl = (Label)gvrow.FindControl("col_BL");
                        DataTable _checkvalue = CHNLSVC.CommonSearch.GetDemurrageCount(lbl.Text);
                        System.Data.DataColumn newColumn = new System.Data.DataColumn("Reson", typeof(System.String));
                        _checkvalue.Columns.Add(newColumn);
                        foreach (DataRow _setrow in _checkvalue.Rows)
                        {
                            var _reson = _type.SingleOrDefault(y => y.Mtp_cd == _setrow["idi_tp"].ToString());
                            _setrow["Reson"] = _reson.Mtp_desc;
                            Label _lblReson = (Label)gvrow.FindControl("col_reson");
                            _lblReson.Text = _reson.Mtp_desc;
                        }


                        GridView gv = (GridView)gvrow.FindControl("grBLDemurage");

                        gv.DataSource = _checkvalue;
                        gv.DataBind();
                     



                    }



                    DemurrageDetails.DataSource = _sumery;
                    DemurrageDetails.DataBind();

                    ViewState["_type"] = _type;

                    ddlreson.DataSource = _type;
                    ddlreson.DataTextField = "mtp_desc";
                    ddlreson.DataValueField = "mtp_cd";
                    ddlreson.DataBind();
                    // ViewState["_type"] = _type;

                    //foreach (GridViewRow gvrow1 in grdBlDetails.Rows)
                    //{
                    //    DropDownList _DropDownList = (DropDownList)gvrow1.FindControl("ddlreson");
                    //    _DropDownList.DataSource = _type;
                    //    _DropDownList.DataTextField = "mtp_desc";
                    //    _DropDownList.DataValueField = "mtp_cd";
                    //    _DropDownList.DataBind();
                    //}
                

            }
            else
            {
                DisplayMessage("No Demurrage reason", 2);
                return;
            }

        }
        public static string DoFormat(Decimal myNumber)
        {
            var s = string.Format("{0:0.00}", myNumber);

            if (s.EndsWith("00"))
            {
                return s;
            }
            else
            {
                return s;
            }
        }
        protected void lbtnSearchall_Click(object sender, EventArgs e)
        {
            Info();
            xValues = ViewState["xValues"] as string[];
            yValues = ViewState["yValues"] as decimal[];

            if (yValues == null)
            {
               // lblEmtymsg.Visible = true;
                Image1.Visible = true;
                Chart1.Visible = false;
                return;
            }
            chart();
           
        }

        private void chart()
        {
            if (xValues != null)
            {

                Image1.Visible = false;
                Chart1.Visible = true;
                System.Web.UI.DataVisualization.Charting.Series series = new System.Web.UI.DataVisualization.Charting.Series("Pie");
                //  yValues = new double[] { 65.62, 75.54, 60.45, 34.73, 85.42 };
                // string[] xValues = { "France", "Canada", "Germany", "USA", "Italy" };
                Chart1.Series["Default"].Points.DataBindXY(xValues, yValues);

                // Set Pie chart type
                Chart1.Series["Default"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;

                Chart1.Series["Default"]["PointWidth"] = "0.5";// Show data points labels
                Chart1.Series["Default"]["PieLabelStyle"] = "Outside";
                Chart1.Series["Default"].IsValueShownAsLabel = true;// Set data points label style

                Chart1.Series["Default"]["BarLabelStyle"] = "Center";// Show chart as 3D
               // Chart1.Series["Default"]["PieStartAngle"] = "90";

                Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;// Draw chart as 3D

                Chart1.Series["Default"]["DrawingStyle"] = "Cylinder";



                Chart2.Series["Default"].Points.DataBindXY(xValues, yValues);

                // Set Pie chart type
                Chart2.Series["Default"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;

                Chart2.Series["Default"]["PointWidth"] = "0.5";// Show data points labels
                Chart2.Series["Default"]["PieLabelStyle"] = "Outside";
                Chart2.Series["Default"].IsValueShownAsLabel = true;// Set data points label style

                Chart2.Series["Default"]["BarLabelStyle"] = "Center";// Show chart as 3D
                // Chart1.Series["Default"]["PieStartAngle"] = "90";

                Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;// Draw chart as 3D

                Chart2.Series["Default"]["DrawingStyle"] = "Cylinder";
                lbtnFullchart.Visible = true;
            }
        }
        protected void lbtngrdInvoiceDetailsEdit_Click(object sender, EventArgs e)
        {
           
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                grdBlDetails.EditIndex = grdr.RowIndex;


                Info();
                xValues = ViewState["xValues"] as string[];
                yValues = ViewState["yValues"] as decimal[];
                chart();
        }
        protected void lbtngrdInvoiceDetailsCancel_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow grdr = (GridViewRow)btn.NamingContainer;
            grdBlDetails.EditIndex = grdr.RowIndex;
            grdBlDetails.EditIndex = -1;

            Info();
            xValues = ViewState["xValues"] as string[];
            yValues = ViewState["yValues"] as decimal[];
            chart();
        }
        private void GetDemmurrage()
        {
          
          

          
        }

        protected void lbtnAddDemurrage_Click(object sender, EventArgs e)
        {
            if (grdBlDetails.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _BL = (row.FindControl("col_BL") as Label).Text;
                Session["_BL"] = _BL;
            }
            AddDemurrage.Show();
        }

        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
           


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtRemark.Text))
            //{
            //    DisplayMessage("Please select Zone Code", 2);
            //    return;
            //}
         
            Int32 row_aff = 0;
            DemurrageTracker _DemuObj = new DemurrageTracker();
            _DemuObj.idi_act = 1;
            _DemuObj.idi_act_by = Session["UserID"].ToString();
            _DemuObj.idi_act_dt = System.DateTime.Now;
            _DemuObj.idi_act_session = Session["SessionID"].ToString();
            _DemuObj.idi_com = Session["UserCompanyCode"].ToString();
            _DemuObj.idi_cre_by = Session["UserID"].ToString();
            _DemuObj.idi_cre_dt = System.DateTime.Now;
            _DemuObj.idi_cre_session = Session["SessionID"].ToString();
            _DemuObj.idi_doc_no = Session["_BL"].ToString();
            _DemuObj.idi_rmk = txtRemark.Text;
            _DemuObj.idi_tp = ddlreson.SelectedValue;
            _DemuList.Add(_DemuObj);


            row_aff = (Int32)CHNLSVC.General.SaveDemurrage(_DemuList);
            if (row_aff==1)
            {
                DisplayMessage("Successfully Saved", 3);
                Session["_BL"] = "";
                ddlreson.SelectedIndex = -1;
                txtRemark.Text = "";
                AddDemurrage.Hide();
            }
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
         

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddlnewstatus = (e.Row.FindControl("ddlreson2") as DropDownList);
                    List<MasterType> _type1 = new List<MasterType>();
                    _type1 = CHNLSVC.General.GetAllType("DEMU");

                    ddlnewstatus.DataSource = _type1;
                    ddlnewstatus.DataTextField = "mtp_desc";
                    ddlnewstatus.DataValueField = "mtp_cd";
                    ddlnewstatus.DataBind();
                }
                Label LblItr_bus_code = e.Row.FindControl("col_ib_supp_cd") as Label;
                Label lblItr_anal1 = e.Row.FindControl("col_sname") as Label;
                // LblItr_c_code = e.Row.FindControl("col_ib_consi_cd") as Label;
                Label consi= e.Row.FindControl("col_ib_consi_cd") as Label;
             
                MasterCompany mstcom = new MasterCompany();
                mstcom = CHNLSVC.General.GetCompByCode(consi.Text);

                string strFullText = LblItr_bus_code.Text;
                LblItr_bus_code.ToolTip = lblItr_anal1.Text;
                consi.ToolTip = mstcom.Mc_desc;
            }
               

        }

        protected void lbtngrdview_Click(object sender, EventArgs e)
        {
            if (txtSavelconformmessageValue.Value == "No")
            {
                return;
            }
            LinkButton btn = (LinkButton)sender;
            GridViewRow grdr = (GridViewRow)btn.NamingContainer;
            grdBlDetails.EditIndex = grdr.RowIndex;

            string _BL = (grdr.FindControl("col_BL") as Label).Text;
           
            string remark = (grdr.FindControl("lblremarks2") as TextBox).Text;
            string reson = (grdr.FindControl("ddlreson2") as DropDownList).Text;


            DataTable _DemurrageCountTbl = CHNLSVC.CommonSearch.GetDemurrageCount(_BL);
            if (_DemurrageCountTbl.Rows.Count > 0)
            {
                string value = reson;
                DataRow[] filteredRows = _DemurrageCountTbl.Select("IDI_TP like '%" + value + "%'");
                if (filteredRows.Length != 0)
                {
                    DisplayMessage("Selected demurrage reason is already available", 2);
                    return;
                }
            }
            Int32 row_aff = 0;
            DemurrageTracker _DemuObj = new DemurrageTracker();
            _DemuObj.idi_act = 1;
            _DemuObj.idi_act_by = Session["UserID"].ToString();
            _DemuObj.idi_act_dt = System.DateTime.Now;
            _DemuObj.idi_act_session = Session["SessionID"].ToString();
            _DemuObj.idi_com = Session["UserCompanyCode"].ToString();
            _DemuObj.idi_cre_by = Session["UserID"].ToString();
            _DemuObj.idi_cre_dt = System.DateTime.Now;
            _DemuObj.idi_cre_session = Session["SessionID"].ToString();
            _DemuObj.idi_doc_no = _BL;
            _DemuObj.idi_rmk = remark;
            _DemuObj.idi_tp = reson;
            _DemuList.Add(_DemuObj);


            row_aff = (Int32)CHNLSVC.General.SaveDemurrage(_DemuList);
            if (row_aff == 1)
            {
                DisplayMessage("Successfully Save", 3);
                Session["_BL"] = "";
                ddlreson.SelectedIndex = -1;
                txtRemark.Text = "";
                AddDemurrage.Hide();
            }

            // grdBlDetails.EditIndex = -1;
            DataTable result = ViewState["BlDetails"] as DataTable;
            grdBlDetails.DataSource = result;
            grdBlDetails.DataBind();

            Info();
            xValues = ViewState["xValues"] as string[];
            yValues = ViewState["yValues"] as decimal[];
            chart();
        }

        protected void lbtnFullchart_Click(object sender, EventArgs e)
        {
            xValues = ViewState["xValues"] as string[];
            yValues = ViewState["yValues"] as decimal[];
            chart();
            ViewChart.Show();
        }

        protected void Chart1_Click(object sender, ImageMapEventArgs e)
        {
            xValues = ViewState["xValues"] as string[];
            yValues = ViewState["yValues"] as decimal[];
            chart();
            ViewChart.Show();
        }

        protected void grdBlDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }
    }
}