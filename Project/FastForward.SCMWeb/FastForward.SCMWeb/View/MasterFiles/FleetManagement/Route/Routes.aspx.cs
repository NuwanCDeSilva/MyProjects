using FF.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.MasterFiles.Fleet_Management.Route
{
    public partial class Routes : BasePage
    {
        string _userid = string.Empty;
        List<int> successItems1 = new List<int>();
        List<int> successItemsexcel = new List<int>();
        string _Para = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    gvschdule.DataSource = new int[] { };
                    gvschdule.DataBind();

                    gvdetailsWh.DataSource = new int[] { };
                    gvdetailsWh.DataBind();

                    gvshowroooms.DataSource = new int[] { };
                    gvshowroooms.DataBind();

                    gvroutes.DataSource = new int[] { };
                    gvroutes.DataBind();

                    DataTable dtwarehouses = new DataTable();
                    dtwarehouses.Columns.AddRange(new DataColumn[8] { new DataColumn("frw_cd"), new DataColumn("frw_wh_com"), new DataColumn("frw_wh_cd"), new DataColumn("frw_com_cd"), new DataColumn("frw_loc_cd"), new DataColumn("frw_route_distance"), new DataColumn("frw_UOM"), new DataColumn("frw_act") });
                    ViewState["WAREHOUSES"] = dtwarehouses;
                    this.BindGrid1();

                    DataTable dtlocations = new DataTable();
                    dtlocations.Columns.AddRange(new DataColumn[7] { new DataColumn("frs_cd"), new DataColumn("frs_line"), new DataColumn("frs_com_cd"), new DataColumn("frs_loc_cd"), new DataColumn("frs_distance"),new DataColumn("frs_UOM"), new DataColumn("frs_act") });
                    ViewState["LOCATIONS"] = dtlocations;
                    this.BindGrid2();

                    DataTable dtschedule = new DataTable();
                    dtschedule.Columns.AddRange(new DataColumn[4] { new DataColumn("frsh_cd"), new DataColumn("frsh_shed_desc"), new DataColumn("frsh_shed_dt"), new DataColumn("frsh_act") });
                    ViewState["SCHEDULE"] = dtschedule;
                    this.BindGrid3();

                    DateTime orddate = DateTime.Now;
                    txtselecteddate.Text = orddate.ToString("dd/MMM/yyyy");
                    ClearRoute();
                    ClearSchedule(); 
                }

                if (this.IsPostBack)
                {
                    TabName.Value = Request.Form[TabName.UniqueID];
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

        protected void BindGrid1()
        {
            try
            {
                gvdetailsWh.DataSource = (DataTable)ViewState["WAREHOUSES"];
                gvdetailsWh.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void BindGrid2()
        {
            try
            {
                gvshowroooms.DataSource = (DataTable)ViewState["LOCATIONS"];
                gvshowroooms.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void BindGrid3()
        {
            try
            {
                DataTable dt = (DataTable)ViewState["SCHEDULE"];
                if (dt.Rows.Count > 0)
                {
                    DataView dv = dt.DefaultView;
                    dv.Sort = "frsh_shed_dt DESC";
                    dt = dv.ToTable();
                }
                gvschdule.DataSource = dt;
                gvschdule.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void lbtnsave_Click(object sender, EventArgs e)
        {
            if (txtsaveconfirm.Value == "Yes")
            {
                try
                {
                    if (gvschdule.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please add schedule dates !!!')", true);
                        txtroutcode.Focus();
                        return;
                    }

                    RouteSchedule Schedule = new RouteSchedule();
                    Int32 results = 0;
                    foreach (GridViewRow Row in gvschdule.Rows)
                    {
                        Label lblroutecode = (Label)Row.FindControl("lblroutecode");
                        Label lbldescsch = (Label)Row.FindControl("lbldescsch");
                        Label lblfrsh_shed_dt = (Label)Row.FindControl("lblfrsh_shed_dt");
                        CheckBox chkschact = (CheckBox)Row.FindControl("chkschact");

                        Int32 act = 1;

                        if (chkschact.Checked == true)
                        {
                            act = 1;
                        }
                        else
                        {
                            act = 0;
                        }

                        _userid = (string)Session["UserID"];

                        Schedule.FRSH_CD = lblroutecode.Text.Trim();
                        Schedule.FRSH_SHED_DT = Convert.ToDateTime(lblfrsh_shed_dt.Text.Trim());
                        Schedule.FRSH_SHED_DESC = lbldescsch.Text.Trim();
                        Schedule.FRSH_ACT = act;
                        Schedule.FRSH_CRE_BY = _userid;
                        Schedule.FRSH_CRE_DT = DateTime.Now;
                        Schedule.FRSH_CRE_SESSION = Session["SessionID"].ToString();
                        Schedule.FRSH_MOD_BY = _userid;
                        Schedule.FRSH_MOD_DT = DateTime.Now;
                        Schedule.FRSH_MOD_SESSION = Session["SessionID"].ToString();

                        results = CHNLSVC.General.CreateSchedule(Schedule);

                        successItems1.Add(results);
                    }

                    if (!successItems1.Contains(-1))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully saved !!!')", true);
                        ClearSchedule();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
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
        }

        protected void lbtnclear_Click(object sender, EventArgs e)
        {
            if (txtconfirmclear.Value == "Yes")
            {
                ClearSchedule();  
            }
        }

        private void ClearRoute()
        {
            txtroutecodecreate.Text = string.Empty;
            txtroudesc.Text = string.Empty;
            ddlrouttype.SelectedIndex = 0;
            chkrouteact.Checked = true;
            txtrcode.Text = txtroutecodecreate.Text.ToUpper().Trim();
            txtwhcomp.Text = string.Empty;
            txtwhcomcode.Text = string.Empty;
            chkactivewh.Checked = true;
            txtrelwhcom.Text = string.Empty;
            txtwhcompcode.Text = string.Empty;
            txtdistance.Text = string.Empty;
            gvdetailsWh.DataSource = new int[] { };
            gvdetailsWh.DataBind();

            DataTable dtlocations = new DataTable();
            dtlocations.Columns.AddRange(new DataColumn[7] { new DataColumn("frs_cd"), new DataColumn("frs_line"), new DataColumn("frs_com_cd"), new DataColumn("frs_loc_cd"), new DataColumn("frs_distance"), new DataColumn("frs_UOM"), new DataColumn("frs_act") });
            ViewState["LOCATIONS"] = dtlocations;

            gvshowroooms.DataSource = new int[] { };
            gvshowroooms.DataBind();

            txtlocrutcd.Text = txtroutecodecreate.Text.ToUpper().Trim();
            txtcompany.Text = string.Empty;
            txtloc.Text = string.Empty;
            chkshowroom.Checked = true;
            txtdisware.Text = string.Empty;
            Session["ROUTES"] = null;

            gvroutes.DataSource = new int[] { };
            gvroutes.DataBind();
        }

        private void ClearSchedule()
        {
            txtroutcode.Text = string.Empty;
            txtroutdesc.Text = string.Empty;
            gvschdule.DataSource = new int[] { };
            gvschdule.DataBind();
            chkactive.Checked = true;
            lblwarning.Text = string.Empty;

            DataTable dtschedule = new DataTable();
            dtschedule.Columns.AddRange(new DataColumn[4] { new DataColumn("frsh_cd"), new DataColumn("frsh_shed_desc"), new DataColumn("frsh_shed_dt"), new DataColumn("frsh_act") });
            ViewState["SCHEDULE"] = dtschedule;

            Session["EXCEL_SCH"] = null;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (txtsaveconfirm.Value == "Yes")
            {
                try
                {
                    if (string.IsNullOrEmpty(txtroutecodecreate.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter route code !!!')", true);
                        txtroutecodecreate.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtroudesc.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter route description !!!')", true);
                        txtroudesc.Focus();
                        return;
                    }

                    RouteHeader Header = new RouteHeader();
                    RouteWareHouse Warehouse = new RouteWareHouse();
                    RouteShowRooms ShowRoom = new RouteShowRooms();

                    DataTable dtwarehouses = (DataTable)ViewState["WAREHOUSES"];
                    dtwarehouses.TableName = "WAREHOUSES";
                    DataTable dtlocations = (DataTable)ViewState["LOCATIONS"];
                    dtlocations.TableName = "LOCATIONS";

                    _userid = (string)Session["UserID"];
                    Int32 isactive = 1;
                    if (ddlrouttype.SelectedValue=="0")
                    {
                        DispMsg("Please select the route type !"); ddlrouttype.Focus(); return;
                    }
                    if (chkrouteact.Checked == true)
                    {
                        isactive = 1;
                    }
                    else
                    {
                        isactive = 0;
                    }

                    Header.FRH_CD = txtroutecodecreate.Text.Trim();
                    Header.FRH_DESC = txtroudesc.Text.Trim();
                    Header.FRH_ACT = isactive;
                    Header.FRH_CAT = ddlrouttype.SelectedValue;
                    Header.FRH_CRE_BY = _userid;
                    Header.FRH_CRE_DT = DateTime.Now;

                    Int32 results = CHNLSVC.General.SaveFleet(Header, Warehouse, ShowRoom, dtwarehouses, dtlocations,_userid,Session["SessionID"].ToString());

                    if (results > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully saved !!!')", true);
                        ClearRoute();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        return;
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
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            if (txtconfirmclear.Value == "Yes")
            {
                ClearRoute();
            }
        }

        //protected void PrettyCalendar_SelectionChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        txtselecteddate.Text = PrettyCalendar.SelectedDate.ToString("dd/MMM/yyyy");
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
        //    }
        //}

        public DataTable RemoveDuplicateRows(DataTable dTable, string colName1)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if ((hTable.Contains(drow[colName1])))
                {
                    duplicateList.Add(drow);
                }
                else
                {
                    hTable.Add(drow[colName1], string.Empty);
                }
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
            {
                dTable.Rows.Remove(dRow);
            }
            //Datatable which contains unique records will be return as output.
            return dTable;
        }
        protected void lbtnadditems_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtroutcode.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter route code !!!')", true);
                    txtroutcode.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtroutdesc.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter route description !!!')", true);
                    txtroutdesc.Focus();
                    return;
                }

                DataTable dtsch = (DataTable)ViewState["SCHEDULE"];
                bool isactive = true;

                if (chkactive.Checked == true)
                {
                    isactive = true;
                }
                else
                {
                    isactive = false;
                }
                DataRow _dtSchNewRow = dtsch.NewRow();
                _dtSchNewRow["frsh_cd"] = txtroutcode.Text.Trim();
                _dtSchNewRow["frsh_shed_desc"] = txtroutdesc.Text.Trim();
                _dtSchNewRow["frsh_shed_dt"] = txtselecteddate.Text.Trim();
                _dtSchNewRow["frsh_act"] = isactive;
                dtsch.Rows.Add(_dtSchNewRow);
                //dtsch.Rows.Add(txtroutcode.Text.Trim(), txtroutdesc.Text.Trim(), txtselecteddate.Text.Trim(), isactive);

                DataTable uniqueColscurrency = RemoveDuplicateRows(dtsch, "frsh_shed_dt");
                if (uniqueColscurrency.Rows.Count > 0)
                {
                    DataView dv = uniqueColscurrency.DefaultView;
                    dv.Sort = "frsh_shed_dt DESC";
                    uniqueColscurrency = dv.ToTable();
                }
                gvschdule.DataSource = uniqueColscurrency;
                gvschdule.DataBind();

                ViewState["SCHEDULE"] = uniqueColscurrency;
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message,"E");
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnaddthose_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtrcode.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter route code !!!')", true);
                    txtrcode.Focus();
                    return;
                }
                else
                {
                    //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Routes);
                    //DataTable result = CHNLSVC.General.SearchRoutes(SearchParams, null, null);
                    //string searchStr = "Route like '" + txtrcode.Text.Trim() + "'";
                    //DataRow[] drPaytable = result.Select(searchStr);
                    //if (drPaytable.Length <= 0)
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid route code !!!')", true);
                    //    txtrcode.Focus();
                    //    return;
                    //}
                    if (string.IsNullOrEmpty(txtroutecodecreate.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a route code !!!')", true);
                        txtroutecodecreate.Focus();
                        return;   
                    }
                }

                if (string.IsNullOrEmpty(txtwhcomp.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter W/H Complex Company !!!')", true);
                    txtwhcomp.Focus();
                    return;
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RouteWarehouses);
                    DataTable result = CHNLSVC.General.SearchWareHouses(SearchParams, null, null);
                    string searchStr = "WH_Company like '" + txtwhcomp.Text.Trim() + "'";
                    DataRow[] drPaytable = result.Select(searchStr);
                    if (drPaytable.Length <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid  W/H Complex Company!!!')", true);
                        txtwhcomp.Focus();
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtwhcomcode.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter W/H Complex Code !!!')", true);
                    txtwhcomcode.Focus();
                    return;
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RouteWarehouses);
                    DataTable result = CHNLSVC.General.SearchWareHouses(SearchParams, null, null);
                    string searchStr = "Code like '" + txtwhcomcode.Text.Trim() + "'";
                    DataRow[] drPaytable = result.Select(searchStr);
                    if (drPaytable.Length <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid W/H Complex Code!!!')", true);
                        txtwhcomcode.Focus();
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtrelwhcom.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter Related W/H Company !!!')", true);
                    txtrelwhcom.Focus();
                    return;
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RouteLocations);
                    DataTable result = CHNLSVC.General.SearchCompanies(SearchParams, null, null);
                    string searchStr = "Company like '" + txtrelwhcom.Text.Trim() + "'";
                    DataRow[] drPaytable = result.Select(searchStr);
                    if (drPaytable.Length <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid Related W/H Company!!!')", true);
                        txtrelwhcom.Focus();
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtwhcompcode.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter W/H Company Code !!!')", true);
                    txtwhcompcode.Focus();
                    return;
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RouteLocations);
                    DataTable result = CHNLSVC.General.SearchCompanies(SearchParams, null, null);
                    string searchStr = "WH_Company_Code like '" + txtwhcompcode.Text.Trim() + "'";
                    DataRow[] drPaytable = result.Select(searchStr);
                    if (drPaytable.Length <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid W/H Company code!!!')", true);
                        txtwhcompcode.Focus();
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtdistance.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter Route Distance !!!')", true);
                    txtdistance.Focus();
                    return;
                }
                else
                {
                    decimal dis = Convert.ToDecimal(txtdistance.Text.Trim());
                    if (dis < 0.0m)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid Route Distance !!!')", true);
                        txtdistance.Focus();
                        return;
                    }
                }

                DataTable dtware = (DataTable)ViewState["WAREHOUSES"];

                bool isactive = true;
                int isAct = 0;

                if (chkactivewh.Checked == true)
                {
                    isactive = true;
                    isAct = 1;
                }
                else
                {
                    isactive = false;
                    isAct = 0;
                }


                DataTable dt = (DataTable)ViewState["WAREHOUSES"];
                bool ifExist = false;
                bool uptDis = false;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["frw_cd"].ToString() != txtrcode.Text.Trim())
                    {
                        DispMsg("Different route data found!");
                        return;
                    }
                }
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["frw_cd"].ToString() == txtrcode.Text.Trim() && dr["frw_wh_com"].ToString() == txtwhcomp.Text.Trim() && dr["frw_wh_cd"].ToString() == txtwhcomcode.Text.Trim() &&
                        dr["frw_com_cd"].ToString() == txtrelwhcom.Text.Trim() && dr["frw_loc_cd"].ToString() == txtwhcompcode.Text.Trim() 
                     )
                    {
                        if (dr["frw_route_distance"].ToString() == txtdistance.Text.Trim())
                        {
                            ifExist = true;
                            uptDis = false;
                        }
                        else
                        {
                            Label7.Text = "Do you want to update the distance ? ";
                            popupUpdateRate.Show();
                            return;

                            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "UpdateDistance()", true);
                            //if (updateDistance.Value == "Yes")
                            //{
                            //    ifExist = false;
                            //    uptDis = true;
                            //    dr["frw_route_distance"] = txtdisware.Text.Trim();
                            //}
                            //else
                            //{
                            //    ifExist = true;
                            //    uptDis = false;
                            //}

                        }
                    }
                }
                if (!ifExist)
                {
                    if (!uptDis)
                    {
                        dt.Rows.Add(txtrcode.Text.Trim(), txtwhcomp.Text.Trim(), txtwhcomcode.Text.Trim(), txtrelwhcom.Text.Trim(), txtwhcompcode.Text.Trim(), txtdistance.Text.Trim(), "KM", isactive);
                        ViewState["WAREHOUSES"] = dt;
                        this.BindGrid1();
                        txtrcode.Text = txtroutecodecreate.Text.ToUpper().Trim();
                        txtwhcomp.Text = string.Empty;
                        txtwhcomcode.Text = string.Empty;
                        txtrelwhcom.Text = string.Empty;
                        txtwhcompcode.Text = string.Empty;
                        txtdistance.Text = string.Empty;
                    }

                }

                else
                {
                    DisplayMessage("Warehouse already Exists", 2);
                    txtrcode.Text = txtroutecodecreate.Text.ToUpper().Trim();
                    txtwhcomp.Text = string.Empty;
                    txtwhcomcode.Text = string.Empty;
                    txtrelwhcom.Text = string.Empty;
                    txtwhcompcode.Text = string.Empty;
                    txtdistance.Text = string.Empty;
                }

                //dtware.Rows.Add(txtrcode.Text.Trim(), txtwhcomp.Text.Trim(), txtwhcomcode.Text.Trim(), txtrelwhcom.Text.Trim(), txtwhcompcode.Text.Trim(), isactive, txtdistance.Text.Trim());

                gvdetailsWh.DataSource = dt;
                gvdetailsWh.DataBind();

                ViewState["WAREHOUSES"] = dt;
                BindGridData("gvdetailsWh", txtrcode.Text.Trim());
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtlocrutcd.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter route code !!!')", true);
                    txtlocrutcd.Focus();
                    return;
                }
                else
                {
                    //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Routes);
                    //DataTable result = CHNLSVC.General.SearchRoutes(SearchParams, null, null);
                    //string searchStr = "Route like '" + txtlocrutcd.Text.Trim() + "'";
                    //DataRow[] drPaytable = result.Select(searchStr);
                    //if (drPaytable.Length <= 0)
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid route code !!!')", true);
                    //    txtlocrutcd.Focus();
                    //    return;
                    //}
                    if (string.IsNullOrEmpty(txtroutecodecreate.Text))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a route code !!!')", true);
                        txtroutecodecreate.Focus();
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtcompany.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter company !!!')", true);
                    txtcompany.Focus();
                    return;
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RouteLocations);
                    DataTable result = CHNLSVC.General.SearchCompanies(SearchParams, null, null);
                    string searchStr = "Company like '" + txtcompany.Text.Trim() + "'";
                    DataRow[] drPaytable = result.Select(searchStr);
                    if (drPaytable.Length <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid company code !!!')", true);
                        txtcompany.Focus();
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtloc.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter location !!!')", true);
                    txtloc.Focus();
                    return;
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "CODE", txtloc.Text.Trim().ToUpper());

                    //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RouteLocations);
                    //DataTable result = CHNLSVC.General.SearchCompanies(SearchParams, "Location", txtloc.Text.Trim().ToUpper());
                    string searchStr = "CODE like '" + txtloc.Text.Trim() + "'";
                    DataRow[] drPaytable = result.Select(searchStr);
                    //if (drPaytable.Length <= 0)
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid location code !!!')", true);
                    //    txtloc.Focus();
                    //    return;
                    //}
                }

                if (string.IsNullOrEmpty(txtdisware.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter distance !!!')", true);
                    txtdisware.Focus();
                    return;
                }

                else
                {
                    decimal dis = Convert.ToDecimal(txtdisware.Text.Trim());
                    if (dis < 0.0m)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid Route Distance !!!')", true);
                        txtdisware.Focus();
                        return;
                    }
                }

                DataTable dtshowrooms = (DataTable)ViewState["LOCATIONS"];

                bool isactive = true;

                if (chkshowroom.Checked == true)
                {
                    isactive = true;
                }
                else
                {
                    isactive = false;
                }

                DataTable dt = (DataTable)ViewState["LOCATIONS"];
                bool ifExist = false;
                bool uptDis = false;
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["frs_cd"].ToString() == txtlocrutcd.Text.Trim() && dr["frs_com_cd"].ToString() == txtcompany.Text.Trim() &&
                        dr["frs_loc_cd"].ToString() == txtloc.Text.Trim()  //&& dr["frs_act"].ToString() == isAct.ToString() 
                        )
                    {
                        ifExist = true;
                        uptDis = false;
                    }
                }
                if (!ifExist)
                {
                    Int32 max = Convert.ToInt32(dt.AsEnumerable()
                        .Max(row => row["frs_line"]));
                    max = max + 1;
                    dt.Rows.Add(txtlocrutcd.Text.Trim(), max, txtcompany.Text.Trim(), txtloc.Text.Trim(), txtdisware.Text.Trim(), "KM", isactive);
                    ViewState["LOCATIONS"] = dt;
                    this.BindGrid2();
                    txtlocrutcd.Text = txtroutecodecreate.Text.ToUpper().Trim();
                    txtcompany.Text = string.Empty;
                    txtloc.Text = string.Empty;
                    txtdisware.Text = string.Empty;
                }
                else
                {
                    lblMssg.Text = "Do you want to update distance ?";
                    PopupConfBox.Show();
                    //DisplayMessage("Location already Exists", 2);
                   // txtlocrutcd.Text = string.Empty;
                    //txtcompany.Text = string.Empty;
                    //txtloc.Text = string.Empty;
                    //txtdisware.Text = string.Empty;
                }

                //dtware.Rows.Add(txtrcode.Text.Trim(), txtwhcomp.Text.Trim(), txtwhcomcode.Text.Trim(), txtrelwhcom.Text.Trim(), txtwhcompcode.Text.Trim(), isactive, txtdistance.Text.Trim());

                gvshowroooms.DataSource = dt;
                gvshowroooms.DataBind();
                BindGridData("gvshowroooms", txtlocrutcd.Text.Trim());
                ViewState["LOCATIONS"] = dt;

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
                if (!string.IsNullOrEmpty(txtSearchbyword.Text))
                {
                    FilterData();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Routes:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RouteWarehouses:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RouteLocations:
                    {
                        string company = txtwhcomp.Text.Trim();
                        paramsText.Append(company);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        private void FilterData()
        {
            try
            {
                if (lblvalue.Text == "451")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Routes);
                    DataTable result = CHNLSVC.General.SearchRoutes(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "451";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }

                else if (lblvalue.Text == "451a")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Routes);
                    DataTable result = CHNLSVC.General.SearchRoutes(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "451a";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }

                else if (lblvalue.Text == "451b")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Routes);
                    DataTable result = CHNLSVC.General.SearchRoutes(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "451b";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }

                else if (lblvalue.Text == "452")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RouteWarehouses);
                    DataTable result = CHNLSVC.General.SearchWareHouses(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "452";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }

                else if (lblvalue.Text == "453")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RouteLocations);
                    DataTable result = CHNLSVC.General.SearchCompanies(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "453";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }

                else if (lblvalue.Text == "453a")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RouteLocations);
                    DataTable result = CHNLSVC.General.SearchCompanies(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "453a";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "UserLocation")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "UserLocation";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "UserLocationShowRooms")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "UserLocationShowRooms";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
                else if (ViewState["SEARCH"] != null)
                {
                    DataTable result = (DataTable)ViewState["SEARCH"];
                    DataView dv = new DataView(result);
                    string searchParameter = ddlSearchbykey.Text;

                    dv.RowFilter = "" + ddlSearchbykey.Text + " like '%" + txtSearchbyword.Text + "%'";

                    if (dv.Count > 0)
                    {
                        result = dv.ToTable();
                        grdResult.DataSource = result;
                    }
                    else
                    {
                        grdResult.DataSource = new DataTable();
                    }
                    grdResult.DataBind();
                    SIPopup.Show();
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

        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdResult.PageIndex = e.NewPageIndex;
                grdResult.DataSource = null;
                grdResult.DataSource = (DataTable)ViewState["SEARCH"];
                grdResult.DataBind();
                SIPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lblvalue.Text == "451")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtroutcode.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtroutdesc.Text = grdResult.SelectedRow.Cells[2].Text;

                    DataTable dtschedules = CHNLSVC.General.LoadRouteSchedules(txtroutcode.Text.Trim());

                    gvschdule.DataSource = null;
                    gvschdule.DataBind();

                    if (dtschedules.Rows.Count > 0)
                    {
                        DataView dv = dtschedules.DefaultView;
                        dv.Sort = "frsh_shed_dt DESC";
                        dtschedules = dv.ToTable();
                    }

                    ViewState["SCHEDULE"] = dtschedules;

                    gvschdule.DataSource = dtschedules;
                    gvschdule.DataBind();
                }
                else if (lblvalue.Text == "451a")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtroutecodecreate.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtroudesc.Text = grdResult.SelectedRow.Cells[2].Text;
                    txtrcode.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtlocrutcd.Text = grdResult.SelectedRow.Cells[1].Text;
                    string routetype = grdResult.SelectedRow.Cells[3].Text;
                    string status = grdResult.SelectedRow.Cells[4].Text;
                    //ddlrouttype.SelectedItem.Text = routetype;
                    ddlrouttype.SelectedIndex = ddlrouttype.Items.IndexOf(ddlrouttype.Items.FindByText(routetype));
                    if (status == "Active")
                    {
                        chkrouteact.Checked = true;
                    }
                    else
                    {
                        chkrouteact.Checked = false;
                    }

                    DataTable dtwarehouses = CHNLSVC.General.LoadRouteWareHouses(txtroutecodecreate.Text.Trim());

                    gvdetailsWh.DataSource = null;
                    gvdetailsWh.DataBind();

                    ViewState["WAREHOUSES"] = dtwarehouses;
                    

                    gvdetailsWh.DataSource = dtwarehouses;
                    gvdetailsWh.DataBind();
                    BindGridData("gvdetailsWh", txtroutecodecreate.Text.Trim());
                    DataTable dtlocations = CHNLSVC.General.LoadRouteLocations(txtroutecodecreate.Text.Trim());

                    gvshowroooms.DataSource = null;
                    gvshowroooms.DataBind();

                    ViewState["LOCATIONS"] = dtlocations;
                    

                    gvshowroooms.DataSource = dtlocations;
                    gvshowroooms.DataBind();
                    BindGridData("gvshowroooms", txtroutecodecreate.Text.Trim());
                }
                else if (lblvalue.Text == "451b")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtlocrutcd.Text = grdResult.SelectedRow.Cells[1].Text;
                }

                else if (lblvalue.Text == "451c")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtrcode.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                else if (lblvalue.Text == "452")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtwhcomp.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtwhcomcode.Text = grdResult.SelectedRow.Cells[2].Text;
                }
                else if (lblvalue.Text == "453")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtrelwhcom.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtwhcompcode.Text = grdResult.SelectedRow.Cells[2].Text;
                }
                else if (lblvalue.Text == "453a")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtcompany.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtloc.Text = grdResult.SelectedRow.Cells[3].Text;
                }
                else if (lblvalue.Text == "UserLocation")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtloc.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtloc.ToolTip = grdResult.SelectedRow.Cells[2].Text;
                }
                else if (lblvalue.Text == "UserLocationShowRooms")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtloc.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtloc.ToolTip = grdResult.SelectedRow.Cells[2].Text;
                }
            }
            catch
            {

            }
        }

        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                if ((col.ColumnName != "Route Type") && (col.ColumnName != "Status"))
                {
                    this.ddlSearchbykey.Items.Add(col.ColumnName); 
                }
                
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }
        protected void lbtnroutecod_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Routes);
                DataTable result = CHNLSVC.General.SearchRoutes(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "451";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
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

        protected void txtroutcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                RouteSchedule _shedule = CHNLSVC.General.GET_ROUTE_SHEDULE_DATA(new RouteSchedule()
                {
                    FRSH_CD = txtroutcode.Text.Trim().ToUpper()
                }).FirstOrDefault();
                if (_shedule!=null)
                {
                    txtroutdesc.Text = _shedule.FRSH_SHED_DESC;
                }
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Routes);
                //DataTable result = CHNLSVC.General.SearchRoutes(SearchParams, "Route", txtroutcode.Text);

                //foreach (DataRow item in result.Rows)
                //{
                //    if (txtroutcode.Text.ToUpper() == item["Route"].ToString().ToUpper())
                //    {
                //        txtroutdesc.Text = item["Description"].ToString();
                //    }
                //}

                txtroutcode.Text = txtroutcode.Text.ToUpper();
                grdResult.DataSource = null;
                grdResult.DataBind();

                DataTable dtschedules = CHNLSVC.General.LoadRouteSchedules(txtroutcode.Text.Trim());

                gvschdule.DataSource = null;
                gvschdule.DataBind();

                if (dtschedules.Rows.Count > 0)
                {
                    DataView dv = dtschedules.DefaultView;
                    dv.Sort = "frsh_shed_dt DESC";
                    dtschedules = dv.ToTable();
                }

                gvschdule.DataSource = dtschedules;
                gvschdule.DataBind();
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

        protected void lbtnrcd_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Routes);
                DataTable result = CHNLSVC.General.SearchRoutes(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "451a";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
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

        protected void txtroutecodecreate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtrcode.Text = "";
                txtlocrutcd.Text = "";
                if (!string.IsNullOrEmpty(txtroutecodecreate.Text))
                {
                    txtrcode.Text = txtroutecodecreate.Text.Trim().ToUpper();
                    txtlocrutcd.Text = txtroutecodecreate.Text.Trim().ToUpper();
                }
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Routes);
                DataTable result = CHNLSVC.General.SearchRoutes(SearchParams, "Route", txtroutecodecreate.Text.Trim());

                foreach (DataRow item in result.Rows)
                {
                    if (txtroutecodecreate.Text.ToUpper().Trim() == item["Route"].ToString().ToUpper())
                    {
                        txtroudesc.Text = item["Description"].ToString();
                        string routetype = item["Route Type"].ToString();
                        string status = item["Status"].ToString();
                        ddlrouttype.SelectedIndex = ddlrouttype.Items.IndexOf(ddlrouttype.Items.FindByText(routetype));
                        //ddlrouttype.SelectedItem.Text = routetype;

                        if (status == "Active")
                        {
                            chkrouteact.Checked = true;
                        }
                        else
                        {
                            chkrouteact.Checked = false;
                        }
                    }
                }

                txtroutecodecreate.Text = txtroutecodecreate.Text.ToUpper();
                grdResult.DataSource = null;
                grdResult.DataBind();

                DataTable dtwarehouses = CHNLSVC.General.LoadRouteWareHouses(txtroutecodecreate.Text.Trim());

                gvdetailsWh.DataSource = null;
                gvdetailsWh.DataBind();

                ViewState["WAREHOUSES"] = dtwarehouses;
                gvdetailsWh.DataSource = dtwarehouses;
                gvdetailsWh.DataBind();

                BindGridData("gvdetailsWh", txtroutecodecreate.Text.Trim().ToUpper());

                DataTable dtlocations = CHNLSVC.General.LoadRouteLocations(txtroutecodecreate.Text.Trim());

                gvshowroooms.DataSource = null;
                gvshowroooms.DataBind();

                gvshowroooms.DataSource = dtlocations;
                gvshowroooms.DataBind();

                ViewState["LOCATIONS"] = dtlocations;
                BindGridData("gvshowroooms", txtroutecodecreate.Text.Trim().ToUpper());
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
        
        protected void lbtnlocrcode_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Routes);
                DataTable result = CHNLSVC.General.SearchRoutes(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "451b";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
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

        protected void lbtnrucd_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Routes);
                DataTable result = CHNLSVC.General.SearchRoutes(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "451c";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
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

        protected void txtrcode_TextChanged(object sender, EventArgs e)
        {
            txtrcode.Text = txtrcode.Text.ToUpper();
            try
            {
                if (!string.IsNullOrEmpty(txtrcode.Text))
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Routes);
                    DataTable result = CHNLSVC.General.SearchRoutes(SearchParams, null, null);
                    string searchStr = "Route like '" + txtrcode.Text.Trim() + "'";
                    DataRow[] drPaytable = result.Select(searchStr);
                    if (drPaytable.Length <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid route code !!!')", true);
                        txtrcode.Text = "";
                        txtrcode.Focus();
                        return;
                    }
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtlocrutcd_TextChanged(object sender, EventArgs e)
        {
            txtlocrutcd.Text = txtlocrutcd.Text.ToUpper();
            try
            {
                if (!string.IsNullOrEmpty(txtlocrutcd.Text))
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Routes);
                    DataTable result = CHNLSVC.General.SearchRoutes(SearchParams, null, null);
                    string searchStr = "Route like '" + txtlocrutcd.Text.Trim() + "'";
                    DataRow[] drPaytable = result.Select(searchStr);
                    if (drPaytable.Length <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid route code !!!')", true);
                        txtlocrutcd.Focus();
                        txtlocrutcd.Text = "";
                        return;
                    }
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }
        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            //mpexcel.Show();
            //Session["EXCEL_SCH"] = "1";
            //Session["ROUTES"] = null;
            //pnlpopupExcelss.Visible = false;

            excelUpload2.Show();
            Label2.Visible = false;
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

                string sessiontype1 = string.Empty;
                string sessiontype2 = string.Empty;

                sessiontype1 = (string)Session["ROUTES"];
                sessiontype2 = (string)Session["EXCEL_SCH"];

                if (!string.IsNullOrEmpty(sessiontype1))
                {
                    gvroutes.DataSource = null;
                    gvroutes.DataBind();

                    gvroutes.DataSource = dt;
                    gvroutes.DataBind();
                    mpexcel.Show();
                }
                else if (!string.IsNullOrEmpty(sessiontype2))
                {
                    gvschdule.DataSource = null;
                    gvschdule.DataBind();
                    if (dt.Rows.Count > 0)
                    {
                        DataView dv = dt.DefaultView;
                        dv.Sort = "frsh_shed_dt DESC";
                        dt = dv.ToTable();
                    }


                    gvschdule.DataSource = dt;
                    gvschdule.DataBind();

                    ViewState["SCHEDULE"] = dt;
                }

                connExcel.Close();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnuploadexcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileupexcelupload.HasFile)
                {
                    string FileName = Path.GetFileName(fileupexcelupload.PostedFile.FileName);
                    string Extension = Path.GetExtension(fileupexcelupload.PostedFile.FileName);

                    if (Extension != ".xls" && Extension != ".XLS" && Extension != ".xlsx" && Extension != ".XLSX")
                    {
                        lblwarning.Text = "Please select a valid excel (.xls) file !!!";
                        mpexcel.Show();
                        return;
                    }
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                    string FilePath = Server.MapPath(FolderPath + FileName);
                    fileupexcelupload.SaveAs(FilePath);
                    Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text);
                }
                else
                {
                    lblwarning.Text = "Please select an excel file !!!";
                    mpexcel.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void LinkButton8_Click(object sender, EventArgs e)
        {
            lblwarning.Text = string.Empty;
            lblsuccess.Text = string.Empty;
        }

        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            //mpexcel.Show();
            //Session["ROUTES"] = "1";
            //Session["EXCEL_SCH"] = null;
            //pnlpopupExcelss.Visible = true;
            lblErrorMessage.Visible = false;
            Label1.Visible = false;
            excelUpload.Show();
        }

        protected void LinkButton9_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtsaveconfirm.Value == "Yes")
                {
                    foreach (GridViewRow gvrowa in gvroutes.Rows)
                    {
                        Label lblexrt = (Label)gvrowa.FindControl("lblexrt");
                        Label lblexrtdesc = (Label)gvrowa.FindControl("lblexrtdesc");
                        Label lblexrttyp = (Label)gvrowa.FindControl("lblexrttyp");
                        CheckBox chkschactexcel = (CheckBox)gvrowa.FindControl("chkschactexcel");

                        RouteHeader Header = new RouteHeader();
                        RouteWareHouse Warehouse = new RouteWareHouse();
                        RouteShowRooms ShowRoom = new RouteShowRooms();

                        DataTable dtwarehouses = (DataTable)ViewState["WAREHOUSES"];
                        DataTable dtlocations = (DataTable)ViewState["LOCATIONS"];

                        _userid = (string)Session["UserID"];
                        Int32 isactive = 1;
                        string routetype = string.Empty;

                        if (chkschactexcel.Checked == true)
                        {
                            isactive = 1;
                        }
                        else
                        {
                            isactive = 0;
                        }

                        if (lblexrttyp.Text.ToUpper() == "FLAT")
                        {
                            routetype = "F";
                        }
                        else
                        {
                            routetype = "H";
                        }

                        Header.FRH_CD = lblexrt.Text.Trim();
                        Header.FRH_DESC = lblexrtdesc.Text.Trim();
                        Header.FRH_ACT = isactive;
                        Header.FRH_CAT = routetype;
                        Header.FRH_CRE_BY = _userid;
                        Header.FRH_CRE_DT = Convert.ToDateTime(DateTime.Now.Date);

                        Int32 results = CHNLSVC.General.SaveFleet(Header, Warehouse, ShowRoom, dtwarehouses, dtlocations, _userid, Session["SessionID"].ToString());

                        successItemsexcel.Add(results);
                    }

                    if (!successItemsexcel.Contains(-1))
                    {
                        lblsuccess.Text = "Successfully saved !!!";
                        ClearRoute();
                        mpexcel.Show();
                    }
                    else
                    {
                        lblwarning.Text = "Error Occurred while processing !!!";
                        return;
                    }
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

        protected void lbtnwhcom_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RouteWarehouses);
                DataTable result = CHNLSVC.General.SearchWareHouses(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "452";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
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

        protected void txtwhcomp_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RouteWarehouses);
                DataTable result = CHNLSVC.General.SearchWareHouses(SearchParams, null, null);
                string searchStr = "WH_Company like '" + txtwhcomp.Text.Trim() + "'";
                DataRow[] drPaytable = result.Select(searchStr);
                if (drPaytable.Length <= 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid  W/H Complex Company!!!')", true);
                    txtwhcomp.Focus();
                    txtwhcomp.Text = "";
                    return;
                }

                SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RouteWarehouses);
                result = CHNLSVC.General.SearchWareHouses(SearchParams, "Warehouse Company", txtroutcode.Text);

                foreach (DataRow item in result.Rows)
                {
                    if (txtwhcomp.Text.ToUpper() == item["WH_Company"].ToString().ToUpper())
                    {
                        txtwhcomcode.Text = item["Code"].ToString();
                    }
                }

                txtwhcomp.Text = txtwhcomp.Text.ToUpper();
                txtwhcomcode.Text = txtwhcomcode.Text.ToUpper();
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }
        protected void lbtnrelwcom_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtwhcomp.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter W/H Complex Company !!!')", true);
                    txtwhcomp.Focus();
                    return;
                }

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RouteLocations);
                DataTable result = CHNLSVC.General.SearchCompanies(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "453";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
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

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RouteLocations);
                DataTable result = CHNLSVC.General.SearchCompanies(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "453a";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
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

        private void DisplayMessage(String Msg, Int32 option)
        {
            string _Msg = Msg.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + _Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _Msg + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + _Msg + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + _Msg + "');", true);
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                //DisplayMessage("Please select the routing type", 2);
                lblErrorMessage.Visible = false;
                string type = "";
                if (ddlexcelTyp.SelectedItem.Text == "Warehouse")
                {
                    type = "WH";
                }

                else if (ddlexcelTyp.SelectedItem.Text == "Location")
                {
                    type = "LOC";
                }
                else
                {
                    type = "";
                    excelUpload.Show();
                    //lblErrorMessage.Visible = true;
                    Label1.Text = "Please select the Excel Type !";
                    return;
                }

                if (!string.IsNullOrEmpty(type))
                {
                    if (fileupexcelupload.HasFile)
                    {
                        string FileName = Path.GetFileName(fileupexcelupload.PostedFile.FileName);
                        string Extension = Path.GetExtension(fileupexcelupload.PostedFile.FileName);

                        if (Extension != ".xls" && Extension != ".XLS" && Extension != ".xlsx" && Extension != ".XLSX")
                        {

                            Label1.Visible = true;
                            Label1.Text = "Please select a valid excel (.xls) file";
                            DisplayMessage("Please select a valid excel (.xls) file", 2);
                            excelUpload.Show();
                            return;
                        }
                        else
                        {
                            Label1.Visible = false;
                        }
                        string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                       
                        string FilePath = Server.MapPath(FolderPath + FileName);
                        fileupexcelupload.SaveAs(FilePath);
                        Session["FilePath"] = FilePath;
                        string value = string.Empty;

                        if (type == "WH")
                        {
                            ExcelProcessNew(FilePath, 1, out value);
                        }

                        else
                        {
                            ExcelProcess1(FilePath, 1, out value);
                        }
                        
                        if (value == "1")
                        {
                            DisplayMessage("Excel was successfully uploaded", 1);
                        }
                        else
                        {
                            DisplayMessage("Error while uploding excel", 4);
                        }
                    }
                    else
                    {
                        Label2.Visible = true;
                        Label2.Text = "Please upload the excel in order to proceed !";
                        excelUpload.Show();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                excelUpload.Show();
                return;
            }

        }

        protected void btnUpload2_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    DataTable dt = (DataTable)ViewState["SCHEDULE"];
                    for (int x = dt.Rows.Count - 1; x >= 0; x--)
                    {
                        DataRow dr = dt.Rows[x];
                        dr.Delete();
                    }
                    dt.AcceptChanges();
                    string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);

                    if (Extension != ".xls" && Extension != ".XLS" && Extension != ".xlsx" && Extension != ".XLSX")
                    {

                        Label2.Visible = true;
                        Label2.Text = "Please select a valid excel (.xls) file";
                        DisplayMessage("Please select a valid excel (.xls) file", 2);
                        excelUpload2.Show();
                        return;
                    }
                    else
                    {
                        Label2.Visible = false;
                    }
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                    string FilePath = Server.MapPath(FolderPath + FileName);
                    FileUpload1.SaveAs(FilePath);
                    Session["FilePath"] = FilePath;
                    string value = string.Empty;
                    ExcelProcess2(FilePath, 1, out value);


                    if (value == "1")
                    {
                        DisplayMessage("Excel was successfully uploaded", 1);
                    }
                    else
                    {
                        DisplayMessage("Error while uploding excel", 4);
                    }
                }
                else
                {
                    Label2.Visible = true;
                    Label2.Text = "Please select an excel file !!!";
                    excelUpload2.Show();
                    return;

                }
            }
            catch (Exception ex)
            {
                excelUpload2.Show();
                return;
            }

        }

        private void ExcelProcess(string FilePath, int option, out string value)
        {
            
            DataTable[] GetExecelTbl = LoadData(FilePath);

            if (GetExecelTbl != null)
            {
                if (GetExecelTbl[0].Rows.Count > 0)
                {
                    if (GetExecelTbl[0].Rows[0][0].ToString() != "R_CD" || GetExecelTbl[0].Rows[0][1].ToString() != "Comp_Com" ||
                        GetExecelTbl[0].Rows[0][2].ToString() != "Comp_Cd" || GetExecelTbl[0].Rows[0][3].ToString() != "Active" ||
                    GetExecelTbl[0].Rows[0][4].ToString() != "Rel_Com_cd" || GetExecelTbl[0].Rows[0][5].ToString() != "Com_cd" ||
                    GetExecelTbl[0].Rows[0][6].ToString() != "Distance" || GetExecelTbl[0].Rows[0][7].ToString() != "UOM")
                    {
                        Label1.Visible = true;
                        Label1.Text = "Excel  Data Invalid Please check Excel File and Upload";
                        excelUpload.Show();
                        value = "3";
                        return;
                    }
                    else
                    {
                        Label1.Visible = false;
                        for (int i = 1; i < GetExecelTbl[0].Rows.Count; i++)
                        {
                            try
                            {

                                string route_cd = GetExecelTbl[0].Rows[i][0].ToString();
                                string WH_comp_com = GetExecelTbl[0].Rows[i][1].ToString();
                                string WH_comp_cd = GetExecelTbl[0].Rows[i][2].ToString();
                                string active = GetExecelTbl[0].Rows[i][3].ToString();
                                string rel_WH_com = GetExecelTbl[0].Rows[i][4].ToString();
                                string WH_com_cd = GetExecelTbl[0].Rows[i][5].ToString();
                                string distance = GetExecelTbl[0].Rows[i][6].ToString();
                                string UOM = GetExecelTbl[0].Rows[i][7].ToString();

                                bool isactive = true;

                                if (active == "1")
                                {
                                    isactive = true;
                                }
                                else
                                {
                                    isactive = false;
                                }


                                DataTable dt = (DataTable)ViewState["WAREHOUSES"];
                                bool ifExist = false;
                                //foreach (DataRow dr in dt.Rows)
                                //{
                                //    if (dr["frw_cd"].ToString() == route_cd && dr["frw_wh_com"].ToString() == WH_comp_com && dr["frw_wh_cd"].ToString() == WH_comp_cd &&
                                //        dr["frw_com_cd"].ToString() == rel_WH_com && dr["frw_loc_cd"].ToString() == WH_com_cd && //dr["frw_act"].ToString() == isAct.ToString() &&
                                //     dr["frw_route_distance"].ToString() == distance)
                                //    {
                                //        ifExist = true;
                                //    }
                                //}
                                //if (!ifExist)
                                //{

                                //dt.Columns.AddRange(new DataColumn[8] { new DataColumn("frw_cd"), new DataColumn("frw_wh_com"), new DataColumn("frw_wh_cd"), new DataColumn("frw_com_cd"), new DataColumn("frw_loc_cd"), new DataColumn("frw_route_distance"), new DataColumn("frw_UOM"), new DataColumn("frw_act") });
                                
                                DataColumn[] columns = dt.Columns.Cast<DataColumn>().ToArray();
                                bool anyFieldContainsPepsi = dt.AsEnumerable()
                                    .Any(row => columns.Any(col => row["frw_cd"].ToString() == route_cd));


                                dt.Rows.Add(route_cd, WH_comp_com, WH_comp_cd, rel_WH_com, WH_com_cd, distance, UOM, isactive);
                                ViewState["WAREHOUSES"] = dt;
                                this.BindGrid1();

                                //}

                                //else
                                //{
                                //    DisplayMessage("Warehouse already Exists", 2);
                                //}

                                gvdetailsWh.DataSource = dt;
                                gvdetailsWh.DataBind();

                                //ViewState["WAREHOUSES"] = dt;

                            }
                            catch (Exception ex)
                            {
                                DisplayMessage("Excel  Data Invalid Please check Excel File and Upload ", 2);
                                Label3.Visible = true;
                                Label3.Text = "Excel  Data Invalid Please check Excel File and Upload";
                                excelUpload.Show();
                                value = "3";
                                return;
                            }

                        }
                    }

                }
            }
            value = "1";
        }

        private void ExcelProcess1(string FilePath, int option, out string value)
        {

            DataTable[] GetExecelTbl = LoadData(FilePath);
            value = "1";
            if (GetExecelTbl != null)
            {
                if (GetExecelTbl[0].Rows.Count > 0)
                {
                    if (GetExecelTbl[0].Rows[0][0].ToString() != "R_Cd" || GetExecelTbl[0].Rows[0][1].ToString() != "Com_cd" ||
                        GetExecelTbl[0].Rows[0][2].ToString() != "Loc_cd" || GetExecelTbl[0].Rows[0][3].ToString() != "Active" ||
                    GetExecelTbl[0].Rows[0][4].ToString() != "Distance" || GetExecelTbl[0].Rows[0][5].ToString() != "UOM")
                    {
                        Label1.Visible = true;
                        Label1.Text = "Excel  Data Invalid Please check Excel File and Upload";
                        excelUpload.Show();
                        value = "3";
                        return;
                    }
                    else
                    {
                        Label1.Visible = false;
                        int linenew = 0;
                        int linenewnew2 = 1;
                        for (int i = 1; i < GetExecelTbl[0].Rows.Count; i++)
                        {
                            try
                            {
                                int a = i + 1;
                               
                                string route_cd = GetExecelTbl[0].Rows[i][0].ToString();
                                if (route_cd.Trim() != txtroutecodecreate.Text.ToString())
                                {
                                    // DisplayMessage("Invalid route code, please check the excel file data ", 2);
                                    Label1.Visible = true;
                                    Label1.Text = "Invalid route code, please check the excel file data " + "Row Num:" + a ;
                                    excelUpload.Show();
                                    value = "3";
                                    gvshowroooms.DataSource = null;
                                    gvshowroooms.DataBind();
                                    break;
                                    return;
                                }

                                string com_cd = GetExecelTbl[0].Rows[i][1].ToString();
                                MasterCompany condet = CHNLSVC.General.GetCompByCode(com_cd.Trim());
                                if (condet == null)
                                {
                                    Label1.Visible = true;
                                    Label1.Text = "Invalid company code, please check the excel file data " + "Row Num:" +a;
                                    excelUpload.Show();
                                    value = "3";
                                    gvdetailsWh.DataSource = null;
                                    gvdetailsWh.DataBind();
                                    break;
                                    return;
                                }

                                string loc_cd = GetExecelTbl[0].Rows[i][2].ToString();
                                DataTable locdt = CHNLSVC.Sales.getLocDesc(com_cd, "11", loc_cd);
                                if (locdt.Rows.Count<1)
                                {
                                    Label1.Visible = true;
                                    Label1.Text = "Invalid location code, please check the excel file data " + "Row Num:" +a;
                                    excelUpload.Show();
                                    value = "3";
                                    gvdetailsWh.DataSource = null;
                                    gvdetailsWh.DataBind();
                                    break;
                                    return;
                                }
                                string active = GetExecelTbl[0].Rows[i][3].ToString();
                                if (active == "1" || active == "0")
                                {

                                }
                                else
                                {
                                    Label1.Visible = true;
                                    Label1.Text = "Invalid active/inactive data, please check the excel file data " + "Row Num:" +a;
                                    excelUpload.Show();
                                    value = "3";
                                    gvdetailsWh.DataSource = null;
                                    gvdetailsWh.DataBind();
                                    break;
                                    return;
                                }

                                string distance = GetExecelTbl[0].Rows[i][4].ToString();
                                double n;
                                bool isNumeric = double.TryParse(distance, out n);
                                if (isNumeric==false)
                                {
                                    Label1.Visible = true;
                                    Label1.Text = "Invalid distance, please check the excel file data " + "Row Num:" +a ;
                                    excelUpload.Show();
                                    value = "3";
                                    gvdetailsWh.DataSource = null;
                                    gvdetailsWh.DataBind();
                                    break;
                                    return;
                                }
                                if (Convert.ToDecimal(distance) <= 0)
                                {
                                    Label1.Visible = true;
                                    Label1.Text = "Invalid distance, please check the excel file data " + "Row Num:" +a;
                                    excelUpload.Show();
                                    value = "3";
                                    gvdetailsWh.DataSource = null;
                                    gvdetailsWh.DataBind();
                                    break;
                                    return;
                                }

                                string UOM = GetExecelTbl[0].Rows[i][5].ToString();
                                if (UOM != "KM" || UOM == "")
                                {
                                    Label1.Visible = true;
                                    Label1.Text = "Invalid UOM (Allowed UOM : KM), please check the excel file data " + "Row Num:" +a;
                                    excelUpload.Show();
                                    value = "3";
                                    gvdetailsWh.DataSource = null;
                                    gvdetailsWh.DataBind();
                                    break;
                                    return;
                                }
                                bool isactive = true;

                                if (active == "1")
                                {
                                    isactive = true;
                                }
                                else
                                {
                                    isactive = false;
                                }

                                DataTable dt = (DataTable)ViewState["LOCATIONS"];
                                bool ifExist = false;
                                foreach (DataRow dr in dt.Rows)
                                {
                                    if (dr["frs_cd"].ToString() == route_cd && dr["frs_com_cd"].ToString() == com_cd &&
                                        dr["frs_loc_cd"].ToString() == loc_cd  
                                        )
                                    {
                                        ifExist = true;
                                        dr["frs_distance"] = distance;
                                    }
                                }
                                ////&& dr["frs_line"].ToString() == txtwhcomp.Text.Trim()
                                //if (!ifExist)
                                //{

                                if (ifExist==false)
                                {
                                    DataTable maxdt = CHNLSVC.CustService.GetMaxSRnum(route_cd);
                                    if (maxdt.Rows[0][0].ToString() == "0" || maxdt.Rows[0][0].ToString() == "")
                                    {
                                        linenew = linenew + 1;
                                        dt.Rows.Add(route_cd, linenew, com_cd, loc_cd, distance, UOM, isactive);
                                    }
                                    else
                                    {
                                        linenewnew2=linenewnew2+Convert.ToInt16(maxdt.Rows[0][0].ToString());
                                        dt.Rows.Add(route_cd, linenewnew2, com_cd, loc_cd, distance, UOM, isactive);
                                    }
                                }
                               
                              
                                ViewState["LOCATIONS"] = dt;
                                this.BindGrid2();

                                //}

                                //else
                                //{
                                //    DisplayMessage("Location already Exists", 2);

                                //}


                                gvshowroooms.DataSource = dt;
                                gvshowroooms.DataBind();

                                //ViewState["LOCATIONS"] = dt;


                            }
                            catch (Exception ex)
                            {
                                DisplayMessage("Excel  Data Invalid Please check Excel File and Upload ", 2);
                                Label3.Visible = true;
                                Label3.Text = "Excel  Data Invalid Please check Excel File and Upload";
                                excelUpload.Show();
                                value = "3";
                                return;
                            }

                        }
                    }

                }
            }
           // value = "1";
        }

        private void ExcelProcess2(string FilePath, int option, out string value)
        {

            DataTable[] GetExecelTbl = LoadData(FilePath);

            if (GetExecelTbl != null)
            {
                if (GetExecelTbl[0].Rows.Count > 0)
                {
                    if (GetExecelTbl[0].Rows[0][0].ToString() != "R_Cd" || GetExecelTbl[0].Rows[0][1].ToString() != "Description" ||
                        GetExecelTbl[0].Rows[0][2].ToString() != "Date" || GetExecelTbl[0].Rows[0][3].ToString() != "Active")
                    {
                        Label2.Visible = true;
                        Label2.Text = "Excel  Data Invalid Please check Excel File and Upload";
                        excelUpload2.Show();
                        value = "3";
                        return;
                    }
                    else
                    {
                        Label2.Visible = false;
                        for (int i = 1; i < GetExecelTbl[0].Rows.Count; i++)
                        {
                            try
                            {
                                int a = i + 1;
                                string route_cd = GetExecelTbl[0].Rows[i][0].ToString();
                                if (route_cd.Trim() != txtroutcode.Text.ToString())
                                {
                                    // DisplayMessage("Invalid route code, please check the excel file data ", 2);
                                    Label2.Visible = true;
                                    Label2.Text = "Invalid route code, please check the excel file data " + "Row Num:" + a;
                                    excelUpload2.Show();
                                    value = "3";
                                    gvschdule.DataSource = null;
                                    gvschdule.DataBind();
                                    break;
                                    return;
                                }
                                string desc = GetExecelTbl[0].Rows[i][1].ToString();
                                if (desc.Trim() == "")
                                {
                                    // DisplayMessage("Invalid route code, please check the excel file data ", 2);
                                    Label2.Visible = true;
                                    Label2.Text = "Invalid description, please check the excel file data " + "Row Num:" + a;
                                    excelUpload2.Show();
                                    value = "3";
                                    gvschdule.DataSource = null;
                                    gvschdule.DataBind();
                                    break;
                                    return;
                                }

                                string date = GetExecelTbl[0].Rows[i][2].ToString();
                                DateTime temp;
                                if (DateTime.TryParse(date, out temp))
                                {

                                }
                                else
                                {
                                    Label2.Visible = true;
                                    Label2.Text = "Invalid date, please check the excel file data " + "Row Num:" + a;
                                    excelUpload2.Show();
                                    value = "3";
                                    gvschdule.DataSource = null;
                                    gvschdule.DataBind();
                                    break;
                                    return;
                                }

                                string active = GetExecelTbl[0].Rows[i][3].ToString();
                                if (active == "1" || active == "0")
                                {

                                }
                                else
                                {
                                    Label2.Visible = true;
                                    Label2.Text = "Invalid active/inactive data, please check the excel file data " + "Row Num:" + a;
                                    excelUpload2.Show();
                                    value = "3";
                                    gvschdule.DataSource = null;
                                    gvschdule.DataBind();
                                    break;
                                    return;
                                }


                                bool isactive = true;

                                if (active == "1")
                                {
                                    isactive = true;
                                }
                                else
                                {
                                    isactive = false;
                                }


                                DataTable dt = (DataTable)ViewState["SCHEDULE"];
                                bool ifExist = false;
                                //foreach (DataRow dr in dt.Rows)
                                //{
                                //    if (dr["frw_cd"].ToString() == route_cd && dr["frw_wh_com"].ToString() == WH_comp_com && dr["frw_wh_cd"].ToString() == WH_comp_cd &&
                                //        dr["frw_com_cd"].ToString() == rel_WH_com && dr["frw_loc_cd"].ToString() == WH_com_cd && //dr["frw_act"].ToString() == isAct.ToString() &&
                                //     dr["frw_route_distance"].ToString() == distance)
                                //    {
                                //        ifExist = true;
                                //    }
                                //}
                                //if (!ifExist)
                                //{
                                dt.Rows.Add(route_cd, desc, date, isactive);
                                ViewState["SCHEDULE"] = dt;
                                this.BindGrid3();

                                //}

                                //else
                                //{
                                //DisplayMessage("Warehouse already Exists", 2);
                                //}
                                if (dt.Rows.Count > 0)
                                {
                                    DataView dv = dt.DefaultView;
                                    dv.Sort = "frsh_shed_dt DESC";
                                    dt = dv.ToTable();
                                }

                                gvschdule.DataSource = dt;
                                gvschdule.DataBind();

                                ViewState["SCHEDULE"] = dt;

                            }
                            catch (Exception ex)
                            {
                                DisplayMessage("Excel  Data Invalid Please check Excel File and Upload ", 2);
                                Label2.Visible = true;
                                Label2.Text = "Excel  Data Invalid Please check Excel File and Upload";
                                excelUpload2.Show();
                                value = "3";
                                return;
                            }

                        }
                    }

                }
            }
            value = "1";
        }

        public DataTable[] LoadData(string FileName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DataTable Tax = new DataTable();

            using (OleDbConnection cn = new OleDbConnection { ConnectionString = ConnectionString(FileName, "No") })
            {
                cn.Open();
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dtExcelSchema;
                cmdExcel.Connection = cn;

                dtExcelSchema = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                cn.Close();

                //Read Data from First Sheet
                cn.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(Tax);


                return new DataTable[] { Tax };
            }
        }

        public string ConnectionString(string FileName, string Header)
        {
            OleDbConnectionStringBuilder Builder = new OleDbConnectionStringBuilder();
            if (System.IO.Path.GetExtension(FileName).ToUpper() == ".XLS")
            {
                Builder.Provider = "Microsoft.Jet.OLEDB.4.0";
                Builder.Add("Extended Properties", string.Format("Excel 8.0;IMEX=1;HDR={0};", Header));
            }
            else
            {
                Builder.Provider = "Microsoft.ACE.OLEDB.12.0";
                Builder.Add("Extended Properties", string.Format("Excel 12.0;IMEX=1;HDR={0};", Header));
            }

            Builder.DataSource = FileName;

            return Builder.ConnectionString;
        }

        protected void chkactshowroom_CheckChanged(object sender, EventArgs e)
        {
            int isact = 0;
            DataTable dt = (DataTable)ViewState["LOCATIONS"];
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            CheckBox chkactshowroom = row.FindControl("chkactshowroom") as CheckBox;
            if (chkactshowroom.Checked)
            {
                isact = 1;
            }
            else
            {
                isact = 0;
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
        protected void lbtnYes_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["LOCATIONS"];
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["frs_cd"].ToString() == txtlocrutcd.Text.Trim() && dr["frs_com_cd"].ToString() == txtcompany.Text.Trim() &&
                        dr["frs_loc_cd"].ToString() == txtloc.Text.Trim()  //&& dr["frs_act"].ToString() == isAct.ToString() 
                        )
                    {
                        dr["frs_distance"] = txtdisware.Text.Trim();
                        continue;
                    }
                }
                txtlocrutcd.Text = txtroutecodecreate.Text.ToUpper().Trim();
                txtcompany.Text = string.Empty;
                txtloc.Text = string.Empty;
                txtdisware.Text = string.Empty;
                ViewState["LOCATIONS"] = dt;
                gvshowroooms.DataSource = dt;
                gvshowroooms.DataBind();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message,"E");
            }
        }

        protected void lbtNo_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["LOCATIONS"];
            txtlocrutcd.Text = txtroutecodecreate.Text.ToUpper().Trim();
             txtcompany.Text = string.Empty;
             txtloc.Text = string.Empty;
             txtdisware.Text = string.Empty;
             gvshowroooms.DataSource = dt;
             gvshowroooms.DataBind();
        }

        protected void chkactivegrid_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //gvdetailsWh
                Int32 _isAct = 0;
                GridViewRow row = (sender as CheckBox).NamingContainer as GridViewRow;
                CheckBox chkactivegrid = row.FindControl("chkactivegrid") as CheckBox;
                Label lblruuut = row.FindControl("lblruuut") as Label; //frw_cd
                Label lblwhcom = row.FindControl("lblwhcom") as Label; //frw_wh_com
                Label lblwhcomcode = row.FindControl("lblwhcomcode") as Label; //frw_wh_cd
                Label lblrelwhcom = row.FindControl("lblrelwhcom") as Label; //frw_com_cd
                Label lblrelwhcomcode = row.FindControl("lblrelwhcomcode") as Label; //frw_loc_cd
                if (chkactivegrid.Checked)
                {
                    _isAct = 1;
                }
                else
                {
                    _isAct = 0;
                }
                DataTable _dt = (DataTable)ViewState["WAREHOUSES"];
                foreach (DataRow dr in _dt.Rows)
                {
                    if (dr["frw_cd"].ToString() == lblruuut.Text && dr["frw_wh_com"].ToString() == lblwhcom.Text && dr["frw_wh_cd"].ToString() == lblwhcomcode.Text
                        && dr["frw_com_cd"].ToString() == lblrelwhcom.Text && dr["frw_loc_cd"].ToString() == lblrelwhcomcode.Text)
                    {
                        dr["frw_act"] = _isAct;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void chkactshowroom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //gvshowroooms
                Int32 _isAct = 0;
                GridViewRow row = (sender as CheckBox).NamingContainer as GridViewRow;
                CheckBox chkactshowroom = row.FindControl("chkactshowroom") as CheckBox; //frs_act
                Label lblrouteahowroom = row.FindControl("lblrouteahowroom") as Label; //frs_cd
                Label lblline = row.FindControl("lblline") as Label; //frs_line
                if (chkactshowroom.Checked)
                {
                    _isAct = 1;
                }
                else
                {
                    _isAct = 0;
                }
                DataTable _dt = (DataTable)ViewState["LOCATIONS"];
                foreach (DataRow dr in _dt.Rows)
                {
                    if (dr["frs_cd"].ToString() == lblrouteahowroom.Text && dr["frs_line"].ToString() == lblline.Text)
                    {
                        dr["frs_act"] = _isAct;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void chkschactexcel_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //gvshowroooms
                Int32 _isAct = 0;
                GridViewRow row = (sender as CheckBox).NamingContainer as GridViewRow;
                CheckBox chkschactexcel = row.FindControl("chkschactexcel") as CheckBox; //frh_act
                Label lblexrt = row.FindControl("lblexrt") as Label; //frh_cd
                Label lblexrttyp = row.FindControl("lblexrttyp") as Label; //frh_cat
                if (chkschactexcel.Checked)
                {
                    _isAct = 1;
                }
                else
                {
                    _isAct = 0;
                }
                DataTable _dt = (DataTable)ViewState["SCHEDULE"];
                foreach (DataRow dr in _dt.Rows)
                {
                    if (dr["frh_cd"].ToString() == lblexrt.Text && dr["frh_cat"].ToString() == lblexrttyp.Text)
                    {
                        dr["frh_act"] = _isAct;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)ViewState["WAREHOUSES"];
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["frw_cd"].ToString() == txtrcode.Text.Trim() && dr["frw_wh_com"].ToString() == txtwhcomp.Text.Trim() && dr["frw_wh_cd"].ToString() == txtwhcomcode.Text.Trim() &&
                        dr["frw_com_cd"].ToString() == txtrelwhcom.Text.Trim() && dr["frw_loc_cd"].ToString() == txtwhcompcode.Text.Trim())
                    {
                        dr["frw_route_distance"] = txtdistance.Text.Trim();
                    }
                }
                txtrcode.Text = txtroutecodecreate.Text.ToUpper().Trim();
                txtwhcomp.Text = string.Empty;
                txtwhcomcode.Text = string.Empty;
                txtrelwhcom.Text = string.Empty;
                txtwhcompcode.Text = string.Empty;
                txtdistance.Text = string.Empty;
                ViewState["WAREHOUSES"] = dt;
                gvdetailsWh.DataSource = dt;
                gvdetailsWh.DataBind();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            txtrcode.Text = txtroutecodecreate.Text.ToUpper().Trim();
            txtwhcomp.Text = string.Empty;
            txtwhcomcode.Text = string.Empty;
            txtrelwhcom.Text = string.Empty;
            txtwhcompcode.Text = string.Empty;
            txtdistance.Text = string.Empty;
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "UserLocationShowRooms";
            ViewState["SEARCH"] = result;
            BindUCtrlDDLData(result);
            SIPopup.Show();
            txtSearchbyword.Focus();
        }

        protected void txtcompany_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtcompany.Text))
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RouteLocations);
                    DataTable result = CHNLSVC.General.SearchCompanies(SearchParams, null, null);
                    string searchStr = "Company like '" + txtcompany.Text.Trim() + "'";
                    DataRow[] drPaytable = result.Select(searchStr);
                    if (drPaytable.Length <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid company code !!!')", true);
                        txtcompany.Focus();
                        txtcompany.Text="";
                        return;
                    }
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }

        }

        protected void txtloc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                //DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "CODE", txtloc.Text.Trim().ToUpper());
                //string searchStr = "CODE like '" + txtloc.Text.Trim() + "'";
                //DataRow[] drPaytable = result.Select(searchStr);
                //if (drPaytable.Length <= 0)
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid location code !!!')", true);
                //    txtloc.Focus();
                //    txtloc.Text = "";
                //    return;
                //}
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtrelwhcom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtrelwhcom.Text.Trim()))
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RouteLocations);
                    DataTable result = CHNLSVC.General.SearchCompanies(SearchParams, null, null);
                    string searchStr = "Company like '" + txtrelwhcom.Text.Trim() + "'";
                    DataRow[] drPaytable = result.Select(searchStr);
                    if (drPaytable.Length <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid Related W/H Company!!!')", true);
                        txtrelwhcom.Focus();
                        txtrelwhcom.Text = "";
                        return;
                    }
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtwhcompcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtwhcompcode.Text))
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RouteLocations);
                    DataTable result = CHNLSVC.General.SearchCompanies(SearchParams, "WH_COMPANY_CODE", txtwhcompcode.Text.ToUpper().Trim());
                    string searchStr = "WH_Company_Code like '" + txtwhcompcode.Text.Trim() + "'";
                    DataRow[] drPaytable = result.Select(searchStr);
                    if (drPaytable.Length <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid W/H Company code!!!')", true);
                        txtwhcompcode.Focus();
                        txtwhcompcode.Text = "";
                        return;
                    }
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtwhcomcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtwhcomcode.Text))
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RouteWarehouses);
                    DataTable result = CHNLSVC.General.SearchWareHouses(SearchParams, null, null);
                    string searchStr = "Code like '" + txtwhcomcode.Text.Trim() + "'";
                    DataRow[] drPaytable = result.Select(searchStr);
                    if (drPaytable.Length <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid W/H Complex Code!!!')", true);
                        txtwhcomcode.Focus();
                        txtwhcomcode.Text = "";
                        return;
                    }
                }
            }
            catch (Exception)
            {
                 DispMsg("Error Occurred while processing !!!", "E");
            }
        }
        private void BindGridData(string _grdName, string _routeCode)
        {
            RouteHeader _routHdr = new RouteHeader();
            RouteHeader _tmpRoutHdr = new RouteHeader();
            RouteWareHouse _routWh = new RouteWareHouse();
            RouteWareHouse _tmpRoutWh = new RouteWareHouse();
            RouteShowRooms _routSr = new RouteShowRooms();
            RouteShowRooms _tmpRoutSr = new RouteShowRooms();

            //gvdetailsWh
            if (_grdName =="gvdetailsWh")
            {
                Label lblWhDistHdr = gvdetailsWh.HeaderRow.FindControl("lblWhDistHdr") as Label;
                _tmpRoutWh = new RouteWareHouse();
                _tmpRoutWh.FRW_CD = _routeCode;
                _routWh = CHNLSVC.General.GET_ROUTE_WAREHOUSE_DATA(_tmpRoutWh).FirstOrDefault();
                string _hdr = "";
                if (_routWh != null)
                {
                    //_hdr =_routWh.FRW_ROUTE_UOM=="M"? "M":"Km";
                  //  lblWhDistHdr.Text = "Distance (" + _hdr + ")";
                }
                else
                {
                   // lblWhDistHdr.Text = "Distance ";
                }
                
                foreach (GridViewRow grdRow in gvdetailsWh.Rows)
                {
                    Label lblruuut = grdRow.FindControl("lblruuut") as Label;
                    Label lblwhcom = grdRow.FindControl("lblwhcom") as Label;
                    Label lblrelwhcom = grdRow.FindControl("lblrelwhcom") as Label;
                    
                    _tmpRoutHdr = new RouteHeader(); _tmpRoutHdr.FRH_CD = lblruuut.Text;
                    _routHdr = CHNLSVC.General.GET_ROUTE_HDR_DATA(_tmpRoutHdr).FirstOrDefault();
                    MasterCompany condet1 = CHNLSVC.General.GetCompByCode(lblwhcom.Text);
                    MasterCompany condet2 = CHNLSVC.General.GetCompByCode(lblrelwhcom.Text);
                    if (condet1 !=null)
                    {
                        lblwhcom.ToolTip = condet1.Mc_desc;
                    }
                    if (condet2 != null)
                    {
                        lblrelwhcom.ToolTip = condet2.Mc_desc;
                    }
                    if (_routHdr != null)
                    {
                        lblruuut.ToolTip = _routHdr.FRH_DESC; 
                    }

                }
            }
            if (_grdName == "gvshowroooms")
            {
                Label lblShDistHdr = gvshowroooms.HeaderRow.FindControl("lblShDistHdr") as Label;
                _tmpRoutSr = new RouteShowRooms();
                _tmpRoutSr.FRS_CD = _routeCode;
                _routSr = CHNLSVC.General.GET_ROUTE_SHOWROOM_DATA(_tmpRoutSr).FirstOrDefault();
                string _hdr = "";
                if (_routSr != null)
                {
                   // _hdr = _routSr.FRS_UOM == "M" ? "M" : "Km";
                   // lblShDistHdr.Text = "Distance (" + _hdr + ")";
                }
                else
                {
                  //  lblShDistHdr.Text = "Distance ";
                }
                
                foreach (GridViewRow grdRow in gvshowroooms.Rows)
                {
                    Label lblrouteahowroom = grdRow.FindControl("lblrouteahowroom") as Label;
                    Label lblcompany = grdRow.FindControl("lblcompany") as Label;
                    Label lblloccc = grdRow.FindControl("lblloccc") as Label;
                    MasterCompany condet = CHNLSVC.General.GetCompByCode(lblcompany.Text);
                    DataTable locdt = CHNLSVC.Sales.getLocDesc(lblcompany.Text, "11",lblloccc.Text);
                    string locdesc = "not define";
                    string comdesc="notdefine";
                    if (locdt.Rows.Count>0)
                    {
                        locdesc = locdt.Rows[0][0].ToString();
                    }
                    if (condet.Mc_desc !=null)
                    {
                        comdesc = condet.Mc_desc;
                    }
                    _tmpRoutHdr = new RouteHeader(); _tmpRoutHdr.FRH_CD = lblrouteahowroom.Text;
                    _routHdr = CHNLSVC.General.GET_ROUTE_HDR_DATA(_tmpRoutHdr).FirstOrDefault();
                    if (_routHdr != null)
                    {
                        lblrouteahowroom.ToolTip = _routHdr.FRH_DESC;
                        lblcompany.ToolTip = comdesc;
                        lblloccc.ToolTip = locdesc;
                    }
                }
            }
        }
        private void ExcelProcessNew(string FilePath, int option, out string value)
        {
            RouteWareHouse _routWh = new RouteWareHouse();
            List<RouteWareHouse> _routWhList = new List<RouteWareHouse>();
            List<RouteWareHouse> _routWhErrList = new List<RouteWareHouse>();
            DataTable[] GetExecelTbl = LoadData(FilePath);

            if (GetExecelTbl != null)
            {
                if (GetExecelTbl[0].Rows.Count > 0)
                {
                    if (GetExecelTbl[0].Rows[0][0].ToString() != "R_CD" || GetExecelTbl[0].Rows[0][1].ToString() != "Comp_Com" ||
                        GetExecelTbl[0].Rows[0][2].ToString() != "Comp_Cd" || GetExecelTbl[0].Rows[0][3].ToString() != "Active" ||
                    GetExecelTbl[0].Rows[0][4].ToString() != "Rel_Com_cd" || GetExecelTbl[0].Rows[0][5].ToString() != "Com_cd" ||
                    GetExecelTbl[0].Rows[0][6].ToString() != "Distance" || GetExecelTbl[0].Rows[0][7].ToString() != "UOM")
                    {
                        Label1.Visible = true;
                        Label1.Text = "Excel  Data Invalid Please check Excel File and Upload";
                        excelUpload.Show();
                        value = "3";
                        return;
                    }
                    else
                    {
                        Label1.Visible = false;
                        #region Add excel data to object 
                        DataTable _dtWareHouse = (DataTable)ViewState["WAREHOUSES"];
                        for (int i = 1; i < GetExecelTbl[0].Rows.Count; i++)
                        {
                            int a = i + 1;
                            try
                            {
                                string route_cd = GetExecelTbl[0].Rows[i][0].ToString();

                                if (route_cd.Trim() != txtroutecodecreate.Text.ToString())
                                {
                                   // DisplayMessage("Invalid route code, please check the excel file data ", 2);
                                    Label1.Visible = true;
                                    Label1.Text = "Invalid route code, please check the excel file data "+ "Row Num:"+a;
                                    excelUpload.Show();
                                    value = "3";
                                    gvdetailsWh.DataSource = null;
                                    gvdetailsWh.DataBind();
                                    break;
                                    return;
                                }
                                string WH_comp_com = GetExecelTbl[0].Rows[i][1].ToString();
                                string WH_comp_cd = GetExecelTbl[0].Rows[i][2].ToString();
                                MasterCompany condet = CHNLSVC.General.GetCompByCode(WH_comp_com.Trim());
                                if (condet==null)
                                {
                                    Label1.Visible = true;
                                    Label1.Text = "Invalid company code, please check the excel file data " + "Row Num:" +a;
                                    excelUpload.Show();
                                    value = "3";
                                    gvdetailsWh.DataSource = null;
                                    gvdetailsWh.DataBind();
                                    break;
                                    return;
                                }
                                if (WH_comp_cd == "")
                                {
                                    Label1.Visible = true;
                                    Label1.Text = "Invalid warehouse code, please check the excel file data " + "Row Num:" +a;
                                    excelUpload.Show();
                                    value = "3";
                                    gvdetailsWh.DataSource = null;
                                    gvdetailsWh.DataBind();
                                    break;
                                    return;
                                }

                                string active = GetExecelTbl[0].Rows[i][3].ToString();
                                if (active == "1" || active=="0")
                                {

                                }
                                else
                                {
                                    Label1.Visible = true;
                                    Label1.Text = "Invalid active/inactive data, please check the excel file data " + "Row Num:" +a;
                                    excelUpload.Show();
                                    value = "3";
                                    gvdetailsWh.DataSource = null;
                                    gvdetailsWh.DataBind();
                                    break;
                                    return;
                                }
                                string rel_WH_com = GetExecelTbl[0].Rows[i][4].ToString();
                                string WH_com_cd = GetExecelTbl[0].Rows[i][5].ToString();
                                if (WH_com_cd == "")
                                {
                                    Label1.Visible = true;
                                    Label1.Text = "Invalid warehouse code, please check the excel file data " + "Row Num:" +a;
                                    excelUpload.Show();
                                    value = "3";
                                    gvdetailsWh.DataSource = null;
                                    gvdetailsWh.DataBind();
                                    break;
                                    return;
                                }

                                MasterCompany condet2 = CHNLSVC.General.GetCompByCode(rel_WH_com.Trim());
                                if (condet2 == null)
                                {
                                    Label1.Visible = true;
                                    Label1.Text = "Invalid company code, please check the excel file data " + "Row Num:" +a;
                                    excelUpload.Show();
                                    value = "3";
                                    gvdetailsWh.DataSource = null;
                                    gvdetailsWh.DataBind();
                                    break;
                                    return;
                                }
                                string distance = GetExecelTbl[0].Rows[i][6].ToString();
                                if (IsNumeric(distance, 0) || Convert.ToDecimal(distance)<=0)
                                {
                                    Label1.Visible = true;
                                    Label1.Text = "Invalid distance, please check the excel file data " + "Row Num:" +a;
                                    excelUpload.Show();
                                    value = "3";
                                    gvdetailsWh.DataSource = null;
                                    gvdetailsWh.DataBind();
                                    break;
                                    return;
                                }

                                string UOM = GetExecelTbl[0].Rows[i][7].ToString();
                                if (UOM != "KM" || UOM=="")
                                {
                                    Label1.Visible = true;
                                    Label1.Text = "Invalid UOM (Allowed UOM : KM), please check the excel file data " + "Row Num:" +a;
                                    excelUpload.Show();
                                    value = "3";
                                    gvdetailsWh.DataSource = null;
                                    gvdetailsWh.DataBind();
                                    break;
                                    return;
                                }

                                decimal _tmp = 0; Int32 _tmpint = 0;
                                //frw_cd , frw_wh_com , frw_wh_cd , frw_com_cd , frw_loc_cd , frw_route_distance , frw_UOM , frw_act
                                //_dtWareHouse.Rows.Add(route_cd, WH_comp_com, WH_comp_cd, rel_WH_com, WH_com_cd, distance, UOM, isactive);
                                _routWh = new RouteWareHouse();
                                _routWh.FRW_CD = route_cd;

                                _routWh.FRW_WH_COM = WH_comp_com;
                                _routWh.FRW_WH_CD = WH_comp_cd;

                                _routWh.FRW_COM_CD = rel_WH_com;
                                _routWh.FRW_LOC_CD = WH_com_cd;

                                _routWh.FRW_ACT =Int32.TryParse(active,out _tmpint)? Convert.ToInt32(active):0;
                                //_routWh.FRW_WH_COM = rel_WH_com;
                                //_routWh.FRW_WH_CD = WH_com_cd;
                                _routWh.FRW_ROUTE_DISTANCE = decimal.TryParse(distance, out _tmp) ? Convert.ToDecimal(distance) : 0;
                                _routWh.FRW_ROUTE_UOM = UOM;
                                _routWh.FRW_ACT = active == "1"?1:0;
                                _routWhList.Add(_routWh);
                            }
                            catch (Exception e)
                            {
                                DisplayMessage("Excel  Data Invalid Please check Excel File and Upload ", 2);
                                Label3.Visible = true;
                                Label3.Text = "Excel  Data Invalid Please check Excel File and Upload";
                                excelUpload.Show();
                                value = "3";
                                return;
                            }
                        }
                        #endregion
                        #region validate excel data
                       
                        #endregion  
                        if (_routWhErrList.Count > 0)
                        {

                        }
                        else
                        {
                            bool _dataExis = false;
                            foreach (var item in _routWhList)
                            {
                                _dataExis = false;
                                foreach (DataRow drRow in _dtWareHouse.Rows)
                                {
                                    if (drRow["frw_cd"].ToString() == item.FRW_CD &&
                                    drRow["frw_wh_com"].ToString() == item.FRW_WH_COM &&
                                    drRow["frw_wh_cd"].ToString() == item.FRW_WH_CD &&
                                    drRow["frw_com_cd"].ToString() == item.FRW_COM_CD &&
                                    drRow["frw_loc_cd"].ToString() == item.FRW_LOC_CD && 
                                    drRow["frw_act"].ToString() == item.FRW_ACT.ToString())
                                    {
                                        drRow["frw_route_distance"] = item.FRW_ROUTE_DISTANCE;
                                        _dataExis = true;
                                        break;
                                    }
                                }
                                if (!_dataExis)
                                {
                                    _dtWareHouse.Rows.Add(item.FRW_CD, item.FRW_WH_COM, item.FRW_WH_CD, item.FRW_COM_CD, item.FRW_LOC_CD, item.FRW_ROUTE_DISTANCE, item.FRW_ROUTE_UOM, item.FRW_ACT);
                                }
                            }
                        }
                      

                        ViewState["WAREHOUSES"] = _dtWareHouse;
                        this.BindGrid1();
                        gvdetailsWh.DataSource = _dtWareHouse;
                        gvdetailsWh.DataBind();
                        foreach (GridViewRow grdRow in gvdetailsWh.Rows)
                        {
                            Label lblruuut = grdRow.FindControl("lblruuut") as Label;
                            Label lblwhcom = grdRow.FindControl("lblwhcom") as Label;
                            Label lblrelwhcom = grdRow.FindControl("lblrelwhcom") as Label;

                            RouteHeader _tmpRoutHdr = new RouteHeader(); _tmpRoutHdr.FRH_CD = lblruuut.Text;
                            RouteHeader _routHdr = CHNLSVC.General.GET_ROUTE_HDR_DATA(_tmpRoutHdr).FirstOrDefault();
                            MasterCompany condet1 = CHNLSVC.General.GetCompByCode(lblwhcom.Text);
                            MasterCompany condet2 = CHNLSVC.General.GetCompByCode(lblrelwhcom.Text);
                            if (condet1 != null)
                            {
                                lblwhcom.ToolTip = condet1.Mc_desc;
                            }
                            if (condet2 != null)
                            {
                                lblrelwhcom.ToolTip = condet2.Mc_desc;
                            }
                            if (_routHdr != null)
                            {
                                lblruuut.ToolTip = _routHdr.FRH_DESC;
                            }

                        }
                    }
                }
            }
            value = "1";
        }
    }
}