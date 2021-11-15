using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View
{
    public partial class AdminSite : System.Web.UI.MasterPage
    {
        Base _base = new Base();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            // {

            // }
            try
            {
                string SystemType = ConfigurationManager.AppSettings["SystemType"].ToString();
                if (SystemType == "0")
                {
                    divLive.Visible = true;
                    divlive2.Visible = true;
                    divlive3.Visible = false;
                }
                else
                {
                    divLive.Visible = true;
                    divlive3.Visible = true;
                    divlive2.Visible = false;
                }
                BasePage _BasePage = new BasePage();


                //added by Dulaj 01-Feb-2018
                // if ("~" + Request.Url.PathAndQuery.ToString() != Convert.ToString("~/View/ADMIN/Home.aspx"))
                // {
                if (!IsPostBack)
                {
                    if (Session["UserID"] != null)
                    {
                        Int32 checkUrl = _BasePage.CHNLSVC.Security.CheckUrl("~" + Request.Url.PathAndQuery.ToString());
                        //Int32 checkUrl = 1;
                        if (checkUrl != 0)
                        {
                            Int32 _checkPermission = _BasePage.CHNLSVC.Security.CheckMenuPermission(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), "~" + Request.Url.PathAndQuery);
                            if (_checkPermission == 0)
                            {
                                Response.Redirect("~/View/ADMIN/Home.aspx");
                            }
                        }
                        //Added By Dulaj block mrna aod grn 2019/Feb 10
                        //if (Request.Url.PathAndQuery.ToString().Contains("/View/Transaction/Inventory/GoodsReceivedNote.aspx"))
                        //{
                        //    if(_BasePage.CHNLSVC.MsgPortal.blockGRAN(Session["UserCompanyCode"].ToString()))
                        //    {
                        //        Response.Redirect("~/View/ADMIN/Home.aspx");
                        //    }
                        //}
                        //if (Request.Url.PathAndQuery.ToString().Contains("View/Transaction/Inventory/MaterialRequisitionNote.aspx?REQTYPE=MRN"))
                        //{
                        //    if (_BasePage.CHNLSVC.MsgPortal.blockMRN(Session["UserCompanyCode"].ToString()))
                        //    {
                        //        Response.Redirect("~/View/ADMIN/Home.aspx");
                        //    }
                        //}
                        //if (Request.Url.PathAndQuery.ToString().Contains("View/Transaction/Inventory/StockTransferOutwardEntry.aspx"))
                        //{
                        //    //if (_BasePage.CHNLSVC.MsgPortal.blockAOD(Session["UserCompanyCode"].ToString()))
                        //    //{
                        //        //Response.Redirect("~/View/ADMIN/Home.aspx");
                        //    //}
                        //}
                        //if (Request.Url.PathAndQuery.ToString().Contains("/View/Transaction/Inventory/InterCompanyInWardEntry.aspx"))
                        //{
                        //    //if (_BasePage.CHNLSVC.MsgPortal.blockAODIN(Session["UserCompanyCode"].ToString()))
                        //    //{
                        //        //Response.Redirect("~/View/ADMIN/Home.aspx");
                        //    //}
                        //}
                        ///View/Transaction/Inventory/StockTransferOutwardEntry.aspx
                        ///View/Transaction/Inventory/MaterialRequisitionNote.aspx?REQTYPE=MRN
                    }
                }
                // }

                if (Convert.ToString(Session["UserID"]) != "ADMIN")
                {
                    string _expMsg = "Current Session is expired or has been closed by administrator!";
                    if (_BasePage.CHNLSVC.Security.IsSessionExpired((string)Session["SessionID"], (string)Session["UserID"], (string)Session["UserCompanyCode"], out _expMsg) == true)
                    {
                        lblSbuMsg1.Text = _expMsg;
                        SbuPopup.Show();
                    }
                }


                if (!String.IsNullOrEmpty(Convert.ToString(Session["UserID"])) &&
                   !String.IsNullOrEmpty(Convert.ToString(Session["UserCompanyCode"])) &&
                   !String.IsNullOrEmpty(Convert.ToString(Session["UserCompanyName"])) &&
                   !String.IsNullOrEmpty(Convert.ToString(Session["UserIP"])) &&
                    !String.IsNullOrEmpty(Convert.ToString(Session["UserComputer"])))
                {
                    setMainMenuItems();
                    tsslLoc.Text = Session["UserDefLoca"].ToString();
                    tsslPC.Text = Session["UserDefProf"].ToString();// Session["UserDefProf"].ToString();
                    lbtnSbu.Text = Session["UserSBU"].ToString();
                    tssCom.Text = Session["UserCompanyCode"].ToString();
                    if (lbtnSbu.Text == "")
                    {
                        divSbu.Visible = false;
                    }
                    //lbluserrId.Text = Session["UserID"].ToString();
                    lblUSerID1.Text = "USER ID - " + Session["UserID"].ToString() + " - ";
                    DataTable dt = _BasePage.CHNLSVC.Inventory.GetUserNameByUserID(Session["UserID"].ToString());
                    if (dt.Rows.Count > 0)
                    {
                        lblUserNm.Text = dt.Rows[0]["se_usr_name"].ToString() + " ";
                    }
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void setMainMenuItems()
        {
            BasePage _BasePage = new BasePage();
            List<Main_menu_items> oMain_menu_items = new List<Main_menu_items>();
            List<Main_menu_items> oMain_menu_items_temp = _BasePage.CHNLSVC.Security.GetUserSystemMenusNew(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString());
            // oMain_menu_items = oMain_menu_items_temp.FindAll(x => x.SSM_ORDER_ID != 0);
            oMain_menu_items = oMain_menu_items_temp.FindAll(x => x.SSM_ANAL4 != "");
            //  mnMainMenu.DataSource = oMain_menu_items;
            if (oMain_menu_items != null && oMain_menu_items.Count > 0)
            {
                List<Main_menu_items> oLevel1s = oMain_menu_items.FindAll(x => x.SSM_ANAL1.ToUpper() == "M").OrderBy(y => y.SSM_ORDER_ID).ToList();
                foreach (Main_menu_items oLevel1 in oLevel1s)
                {
                    MenuItem i1 = new MenuItem();
                    i1.Text = oLevel1.SSM_DISP_NAME;
                    i1.Value = oLevel1.SSM_NAME;
                    i1.NavigateUrl = oLevel1.SSM_ANAL3;

                    if (oMain_menu_items.FindAll(x => x.SSM_ANAL1 == i1.Value).Count > 0)
                    {
                        List<Main_menu_items> oLevel2s = oMain_menu_items.FindAll(x => x.SSM_ANAL1 == i1.Value).OrderBy(y => y.SSM_ORDER_ID).ToList();
                        foreach (Main_menu_items oLevel2 in oLevel2s)
                        {
                            MenuItem i2 = new MenuItem();
                            i2.Text = oLevel2.SSM_DISP_NAME;
                            i2.Value = oLevel2.SSM_NAME;
                            i2.NavigateUrl = oLevel2.SSM_ANAL3;

                            if (oMain_menu_items.FindAll(x => x.SSM_ANAL1 == i2.Value).Count > 0)
                            {
                                List<Main_menu_items> oLevel3s = oMain_menu_items.FindAll(x => x.SSM_ANAL1 == i2.Value).OrderBy(y => y.SSM_ORDER_ID).ToList();
                                foreach (Main_menu_items oLevel3 in oLevel3s)
                                {
                                    MenuItem i3 = new MenuItem();
                                    i3.Text = oLevel3.SSM_DISP_NAME;
                                    i3.Value = oLevel3.SSM_NAME;
                                    i3.NavigateUrl = oLevel3.SSM_ANAL3;

                                    if (oMain_menu_items.FindAll(x => x.SSM_ANAL1 == i3.Value).Count > 0)
                                    {
                                        List<Main_menu_items> oLevel4s = oMain_menu_items.FindAll(x => x.SSM_ANAL1 == i3.Value).OrderBy(y => y.SSM_ORDER_ID).ToList();
                                        foreach (Main_menu_items oLevel4 in oLevel4s)
                                        {
                                            MenuItem i4 = new MenuItem();
                                            i4.Text = oLevel4.SSM_DISP_NAME;
                                            i4.Value = oLevel4.SSM_NAME;
                                            i4.NavigateUrl = oLevel4.SSM_ANAL3;

                                            if (oMain_menu_items.FindAll(x => x.SSM_ANAL1 == i4.Value).Count > 0)
                                            {
                                                //added by Chamal 01-Mar-2016
                                                List<Main_menu_items> oLevel5s = oMain_menu_items.FindAll(x => x.SSM_ANAL1 == i4.Value).OrderBy(y => y.SSM_ORDER_ID).ToList();
                                                foreach (Main_menu_items oLevel5 in oLevel5s)
                                                {
                                                    MenuItem i5 = new MenuItem();
                                                    i5.Text = oLevel5.SSM_DISP_NAME;
                                                    i5.Value = oLevel5.SSM_NAME;
                                                    i5.NavigateUrl = oLevel5.SSM_ANAL3;

                                                    i4.ChildItems.Add(i5);
                                                }
                                            }

                                            i3.ChildItems.Add(i4);
                                        }
                                    }

                                    i2.ChildItems.Add(i3);
                                }
                            }
                            i1.ChildItems.Add(i2);
                        }
                    }
                    mnMainMenu.Items.Add(i1);
                }
            }
        }

        protected void m_System_Exit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSavelconformmessageValue.Value == "No")
                {
                    return;
                }
                //if (BaseCls.GlbIsExit == false)
                //{
                _base.CHNLSVC.CloseChannel();
                _base.CHNLSVC.Security.ExitLoginSession(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), Session["SessionID"].ToString());
                //BaseCls.GlbIsExit = true;
                //}
                Session.RemoveAll();
                Session.Clear();
                Response.Redirect("~/Login.aspx");
            }
            catch (Exception err)
            {
                _base.CHNLSVC.CloseChannel();
                Response.Redirect("~/Error.aspx");
            }
        }

        protected void LinkButtonLoc_Click(object sender, EventArgs e)
        {
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            DataTable _result = _base.CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            BindUCtrlDDLData(_result);
            Label1.Text = "5";
            UserPopoup.Show();
            if (!string.IsNullOrEmpty(txtVal.Text))
            {
                if (_base.CheckLocation(Session["UserCompanyCode"].ToString(), txtVal.Text.ToString()) == false)
                {
                    //MessageBox.Show("Selected location " + txtVal.Text.ToString() + " is invalid or inactivated!", "Invalid Location", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
                    return;
                }
                Session["UserDefLoca"] = txtVal.Text;
                tsslLoc.Text = txtVal.Text;
                _base.LoadLocationDetail();
                txtVal.Text = "";
                txtSearchbyword.Focus();
                //CloseAll();
            }
            _base.CHNLSVC.CloseAllChannels();
            // GetNotificationDetails();
        }

        protected void LinkButtonCom_Click(object sender, EventArgs e)
        {
        }
        protected void LinkButtonPro_Click(object sender, EventArgs e)
        {
            dvResultUser.DataSource = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
            DataTable _result = _base.CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, null, null);
            dvResultUser.DataSource = _result;
            dvResultUser.DataBind();
            BindUCtrlDDLData(_result);
            Label1.Text = "6";
            UserPopoup.Show();
            if (!string.IsNullOrEmpty(txtVal.Text))
            {
                if (_base.CheckProfitCenter(Session["UserCompanyCode"].ToString(), txtVal.Text.ToString()) == false)
                {
                    //  MessageBox.Show("Selected profit center " + txtVal.Text.ToString() + " is invalid or inactivated!", "Invalid Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Session["UserDefProf"] = txtVal.Text;
                tsslPC.Text = txtVal.Text;
                _base.LoadProfitCenterDetail();
                txtVal.Text = "";
                txtSearchbyword.Focus();
                //CloseAll();
            }
            _base.CHNLSVC.CloseAllChannels();
        }
        protected void dvResultUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string Des = dvResultUser.SelectedRow.Cells[2].Text;
            string name = dvResultUser.SelectedRow.Cells[1].Text;
            if (Label1.Text == "5")
            {
                Session["UserDefLoca"] = name;
                tsslLoc.Text = name;
                _base.LoadLocationDetail();
            }
            else if (Label1.Text == "6")
            {
                Session["UserDefProf"] = name;
                _base.LoadProfitCenterDetail();
                tsslPC.Text = name;
            }
            else if (Label1.Text == "SBU")
            {
                Session["UserSBU"] = name;
                lbtnSbu.Text = name;
            }
            List<PriceDefinitionRef> _PriceDefinitionRef = _base.CHNLSVC.Sales.GetPriceDefinitionByBookAndLevel(Session["UserCompanyCode"].ToString(), string.Empty, string.Empty, string.Empty, Session["UserDefProf"].ToString());
            Session["_PriceDefinitionRef_1"] = _PriceDefinitionRef;
            MasterProfitCenter _ctn = _base.CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
            Session["MasterProfitCenter_1"] = _ctn;

            DataTable ChannelDefinition_ = _base.CHNLSVC.Inventory.GetChannelDetail(Session["UserCompanyCode"].ToString(), _ctn.Mpc_chnl);
            Session["ChannelDefinition_"] = ChannelDefinition_;

            bool IsSaleValueRoundUp = _base.CHNLSVC.General.IsSaleFigureRoundUp(Session["UserCompanyCode"].ToString());
            Session["IsSaleValueRoundUp_1"] = IsSaleValueRoundUp;

            MasterLocation oLocation = _base.CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
            Session["GlbDefChannel"] = oLocation.Ml_cate_2;

            BaseCls.GlbDefaultBin = _base.CHNLSVC.Inventory.Get_defaultBinCDWeb(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
            Session["GlbDefaultBin"] = BaseCls.GlbDefaultBin;

            UserPopoup.CancelControlID = "btnClose";
            Response.Redirect(Request.RawUrl, false);
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            if (Label1.Text == "5")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = _base.CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
            }
            else if (Label1.Text == "6")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = _base.CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
            }
            else if (Label1.Text == "SBU")
            {
                DataTable SBUTbl = _base.CHNLSVC.Security.GetSBU_User(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), txtSearchbyword.Text);
                SBUTbl.Columns.RemoveAt(1);
                SBUTbl.Columns.RemoveAt(1);
                SBUTbl.Columns[0].ColumnName = "Code";
                dvResultUser.DataSource = SBUTbl;
                dvResultUser.DataBind();
            }
            UserPopoup.Show();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Label1.Text == "5")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = _base.CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());

                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
            }
            else if (Label1.Text == "6")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = _base.CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
            }
            else if (Label1.Text == "SBU")
            {
                DataTable SBUTbl = _base.CHNLSVC.Security.GetSBU_User(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), txtSearchbyword.Text);
                SBUTbl.Columns.RemoveAt(1);
                SBUTbl.Columns.RemoveAt(1);
                SBUTbl.Columns[0].ColumnName = "Code";
                dvResultUser.DataSource = SBUTbl;
                dvResultUser.DataBind();
            }
            UserPopoup.Show();
        }
        protected void dvResultUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvResultUser.PageIndex = e.NewPageIndex;
            if (Label1.Text == "5")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = _base.CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
            }
            else if (Label1.Text == "6")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = _base.CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, null, null);
                dvResultUser.DataSource = _result;
                dvResultUser.DataBind();
            }
            else if (Label1.Text == "SBU")
            {
                DataTable SBUTbl = _base.CHNLSVC.Security.GetSBU_User(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), null);
                SBUTbl.Columns.RemoveAt(1);
                SBUTbl.Columns.RemoveAt(1);
                SBUTbl.Columns[0].ColumnName = "Code";
                dvResultUser.DataSource = SBUTbl;
                dvResultUser.DataBind();
            }
            UserPopoup.Show();
        }

        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.cmbSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.cmbSearchbykey.Items.Add(col.ColumnName);
            }

            this.cmbSearchbykey.SelectedIndex = 0;
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
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void lbtnSbu_Click(object sender, EventArgs e)
        {
            DataTable SBUTbl = _base.CHNLSVC.Security.GetSBU_User(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), null);
            SBUTbl.Columns.RemoveAt(0);
            SBUTbl.Columns.RemoveAt(1);
            SBUTbl.Columns.RemoveAt(1);
            SBUTbl.Columns[0].ColumnName = "Code";
            dvResultUser.DataSource = SBUTbl;
            dvResultUser.DataBind();
            Label1.Text = "SBU";
            BindUCtrlDDLData(SBUTbl);
            UserPopoup.Show();
        }
        protected void btnLoginExpired_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }

        protected void lbtnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/ADMIN/Home.aspx");
        }
    }
}