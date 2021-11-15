using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Data;
using System.Text;

namespace FF.AbansTours.UserControls
{
    public partial class uc_NewOutItems : System.Web.UI.UserControl
    {
        BasePage _basePage = null;
        List<MasterItemStatus> _lstItemStatus;
        DataTable dt;
        List<HpSchemeDefinition> _SchemeDefinition;
        HpSchemeDetails _SchemeDetails;


        public event EventHandler EditItems;
         
        protected void Page_Load(object sender, EventArgs e)
        {

            //if (Session["UserID"] == null || Session["_InvoiceNo"] == null || Session["UserCompanyCode"] == null || Session["ACC_Date"] == null)
            //{
            //    Session["Redirect"] = "0";
            //    Session["ShortAccsess"] = "0";
            //    Response.Redirect("~/Login.aspx", false);
            //}
            //else
            //{


                if (!IsPostBack)
                {
                    //BindJavaScripts();
                    //uc_CompanySearch1.Company = _basePage.GlbUserComCode;
                    //uc_CompanySearch1.CompanyDes = _basePage.GlbUserComDesc;

                }

                txtItemCode.Attributes.Add("onKeyup", "return clickButton(event,'" + SearchItems.ClientID + "')");
            //}
        }
        private string getProfitCenters()
        {
            _basePage = new BasePage();
            string _prof_cen = null;
           
         
                dt = _basePage.CHNLSVC.Sales.Get_Prf_Cent_Det(Session["_InvoiceNo"].ToString());
                if (dt != null)
                {
                    _prof_cen = dt.Rows[0]["SAH_PC"].ToString();
                }
           
            return _prof_cen;

        }
        private string getLocation(string prof)
        {
            _basePage = new BasePage();
            string _loc = null;
            dt = _basePage.CHNLSVC.Sales.Get_Loc_Det(Session["UserCompanyCode"].ToString(), prof);
            if (dt != null)
            {
                _loc = dt.Rows[0]["mpc_def_loc"].ToString();
            }
            return _loc;
        }


        public void PopUpNewOutItems()
        {
            //if (Session["UserID"] == null || Session["_InvoiceNo"] == null || Session["UserCompanyCode"] == null || Session["ACC_Date"] == null)
            //{
            //    Session["Redirect"] = "0";
            //    Session["ShortAccsess"] = "0";
            //    Response.Redirect("~/Login.aspx", false);
            //}
            //else
            //{
                Panel1.Visible = true;
                Panel1.Style.Add("display", "block");
                OuterGrid.Style.Add("display", "block");
                InnerGird.Style.Add("display", "block");
                string profitCenter = getProfitCenters();
                ViewState["PrfCenter"] = profitCenter;
                string loc = getLocation(profitCenter);
                ViewState["Location"] = loc;
                DataRow drpb = getPb_level_Book();
                ViewState["pb_level"] = drpb["sad_pb_lvl"].ToString();
                ViewState["pb_book"] = drpb["sad_pbook"].ToString();
                DateTime _accDate = Convert.ToDateTime(Session["ACC_Date"]);
                ViewState["AccDate"] = _accDate;

                fillStatus(Session["UserCompanyCode"].ToString());
            //}
        }

        private DataRow getPb_level_Book()
        {
            _basePage = new BasePage();
            DataTable dt = _basePage.CHNLSVC.Sales.Get_Pblevel_book(Session["_InvoiceNo"].ToString());
            DataRow dr = null;

            if (dt != null)
            {
                dr = dt.Rows[0];

            }
           
            return dr;

        }


        private void fillStatus(string comcode)
        {
            _basePage = new BasePage();
            _lstItemStatus = _basePage.CHNLSVC.General.GetAllStockTypes(comcode);

            var _status = from _List in _lstItemStatus
                                 where _List.Mis_cd != "CONS"
                                 orderby _List.Mis_cd
                                 select new MasterItemStatus
                                 {
                                     Mis_cd = _List.Mis_cd,
                                     Mis_desc = _List.Mis_desc
                                    
                                 };


            ddlStatus.DataSource = _status;
            ddlStatus.DataTextField = "Mis_desc";
            ddlStatus.DataValueField = "Mis_desc";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("`Select Status`", "0"));
            //ddlStatus.Items.Insert(0, new ListItem("GOOD", "0"));
            ddlStatus.SelectedIndex = 0;


        }

        private void fillSerials(string _comcode, string _loc, string _item, string _status)
        {

           _basePage = new BasePage();
           //dt = _basePage.CHNLSVC.Sales.GetSerialsForItem(_comcode, _item, _status);
           dt = _basePage.CHNLSVC.Inventory.GetSerialBalance_Curr(_comcode, _loc, _item, _status, "");
           if (dt != null && dt.Rows.Count > 0)
           {
               ddlSerial.DataSource = dt;
               ddlSerial.DataTextField = "SERIALNO";
               ddlSerial.DataValueField = "SERIALNO";
               ddlSerial.DataBind();
               ddlSerial.Items.Insert(0, new ListItem("", "0"));
               ddlSerial.SelectedIndex = 0;
           }
           else
           {
               //ddlSerial.Items.Add("");
               ddlSerial.Items.Insert(0, new ListItem("", "0"));
               ddlSerial.SelectedIndex = 0;
           }


        }




        #region Events
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if(ddlStatus.SelectedIndex == 0)
            {
                lblmsg.Text = "Please select a Status";
                return;
            }


            if (txtItemCode.Text.Length > 0)
            {
                _basePage = new BasePage();
                string _item_desc = _basePage.CHNLSVC.Inventory.getItemDescription(txtItemCode.Text.Trim());
                if (_item_desc != null)
                {
                    lblmsg.Visible = false;
                    EditItems(this, e);
                    ClearFileds();
                    Panel1.Visible = false;
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Invalid ItemCode..Try Again.";
                }
            }
            else
            {
                //lblmsg.Text = "Please Enter Item Code";
                txtItemCode.Focus();
            }

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFileds();

        }


        private void ClearFileds()
        {
            txtItemCode.Text = "";
            txtPrice.Text = "";
            //txtscheme.Text = "";
            ddlSerial.SelectedIndex = -1;
            ddlStatus.SelectedIndex = -1;
            lblmsg.Text = "";
            ddlSerial.DataSource = null;
            ddlSerial.DataBind();
            //ddlStatus.Items.Clear();
            ddlSerial.Items.Clear();
            ddlCurrScm.Items.Clear();
            ddlAccScm.Items.Clear();
            lblmsg.Visible = false;
            


        }

        private void BindJavaScripts()
        {
            txtItemCode.Attributes.Add("onblur", "GetItemData()");
          
        }


        #region Common Search Methods
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            _basePage = new BasePage();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(_basePage.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(_basePage.GlbUserName + seperator + _basePage.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        //paramsText.Append(TextBoxCompany.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        //paramsText.Append(TextBoxCompany.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        //paramsText.Append(TextBoxMain.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                       // paramsText.Append(TextBoxMain.Text + seperator + TextBoxSub.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Inventory_Tracker:
                    {
                        //string status = null;
                        //if (DDLStatus.SelectedItem == null)
                        //    status = "";
                        //else
                        //    status = DDLStatus.SelectedItem.Value;

                        //if (!TextBoxCompany.Enabled && Uc_CompanySearch1.ProfitCenter != string.Empty)
                        //{
                        //    paramsText.Append(TextBoxCode.Text.ToUpper() + seperator + TextBoxModel.Text.ToUpper() + seperator + status + seperator + Uc_CompanySearch1.Company.ToUpper() + seperator + TextBoxMain.Text.ToUpper() + seperator + TextBoxSub.Text.ToUpper() + seperator + Uc_CompanySearch1.Channel.ToUpper() + seperator + Uc_CompanySearch1.SubChannel.ToUpper() + seperator + Uc_CompanySearch1.Area.ToUpper() + seperator + Uc_CompanySearch1.Region.ToUpper() + seperator + Uc_CompanySearch1.Zone.ToUpper() + seperator + "Loc" + seperator + Uc_CompanySearch1.ProfitCenter.ToUpper() + seperator + TextBoxRange.Text.ToUpper() + seperator);
                        //    // GridViewItemDetails.EmptyDataText="No Data Found\n"+TextBoxCode.Text + "," + TextBoxModel.Text + "," +status + "," + Uc_CompanySearch1.Company + "," + TextBoxMain.Text + "," + TextBoxSub.Text + "," + Uc_CompanySearch1.Channel + seperator + Uc_CompanySearch1.SubChannel + seperator + Uc_CompanySearch1.Area + seperator + Uc_CompanySearch1.Region + seperator + Uc_CompanySearch1.Zone + seperator + "Loc" + seperator + Uc_CompanySearch1.ProfitCenter + seperator + TextBoxRange.Text + seperator;
                        //    // GridViewItemDetails.EmptyDataText = "No Data Found<br><b>For Query</b><br>" +
                        //    //        "Code  :" + TextBoxCode.Text + "<br>" + "Model :" + TextBoxModel.Text + "<br>" + "Status   :" + status + "<br>" + "Company :" + Uc_CompanySearch1.Company + "<br>" + "Main Category    :" + TextBoxMain.Text + "<br>" + "Sub Category  :" + TextBoxSub.Text + "<br>" + "Channel    :" + Uc_CompanySearch1.Channel + "<br>" + "Sub Channel  :" + Uc_CompanySearch1.SubChannel + "<br>" + "Area  :" + Uc_CompanySearch1.Area + "<br>" + "Region  :" + Uc_CompanySearch1.Region + "<br>" + "Zone  :" + Uc_CompanySearch1.Zone   + "<br>" + "Location    :" + Uc_CompanySearch1.ProfitCenter + "<br>" + "Range   :" + TextBoxRange.Text ;
                        //}
                        //else if (!TextBoxLoc.Enabled)
                        //{
                        //    paramsText.Append(TextBoxCode.Text.ToUpper() + seperator + TextBoxModel.Text.ToUpper() + seperator + status + seperator + Uc_CompanySearch1.Company.ToUpper() + seperator + TextBoxMain.Text.ToUpper() + seperator + TextBoxSub.Text.ToUpper() + seperator + Uc_CompanySearch1.Channel.ToUpper() + seperator + Uc_CompanySearch1.SubChannel.ToUpper() + seperator + Uc_CompanySearch1.Area.ToUpper() + seperator + Uc_CompanySearch1.Region.ToUpper() + seperator + Uc_CompanySearch1.Zone.ToUpper() + seperator + "No_Loc" + seperator + "" + seperator + TextBoxRange.Text.ToUpper() + seperator);
                        //    //GridViewItemDetails.EmptyDataText = "No Data Found<br><b>For Query</b><br>" +
                        //    // "Code  :" + TextBoxCode.Text + "<br>" + "Model :" + TextBoxModel.Text + "<br>" + "Status   :" + status + "<br>" + "Company :" + Uc_CompanySearch1.Company + "<br>" + "Main Category    :" + TextBoxMain.Text + "<br>" + "Sub Category  :" + TextBoxSub.Text + "<br>" + "Channel    :" + Uc_CompanySearch1.Channel + "<br>" + "Sub Channel  :" + Uc_CompanySearch1.SubChannel + "<br>" + "Area  :" + Uc_CompanySearch1.Area + "<br>" + "Region  :" + Uc_CompanySearch1.Region + "<br>" + "Zone  :" + Uc_CompanySearch1.Zone   + "<br>" + "Location    :" + Uc_CompanySearch1.ProfitCenter + "<br>" + "Range   :" + TextBoxRange.Text ;
                        //}
                        //else
                        //{

                        //    paramsText.Append(TextBoxCode.Text + seperator + TextBoxModel.Text.ToUpper() + seperator + status + seperator + TextBoxCompany.Text.ToUpper() + seperator + TextBoxMain.Text.ToUpper() + seperator + TextBoxSub.Text.ToUpper() + seperator + Uc_CompanySearch1.Channel.ToUpper() + seperator + Uc_CompanySearch1.SubChannel.ToUpper() + seperator + Uc_CompanySearch1.Area.ToUpper() + seperator + Uc_CompanySearch1.Region.ToUpper() + seperator + Uc_CompanySearch1.Zone.ToUpper() + seperator + "Loc" + seperator + TextBoxLoc.Text.ToUpper() + seperator + TextBoxRange.Text.ToUpper() + seperator);
                        //    // GridViewItemDetails.EmptyDataText = "No Data Found<br><b>For Query</b><br>" +
                        //    //   "Code  :" + TextBoxCode.Text + "<br>" + "Model :" + TextBoxModel.Text + "<br>" + "Status   :" + status + "<br>" + "Company :" + TextBoxCompany.Text + "<br>" + "Main Category    :" + TextBoxMain.Text + "<br>" + "Sub Category  :" + TextBoxSub.Text + "<br>" + "Channel    :" + Uc_CompanySearch1.Channel + "<br>" + "Sub Channel  :" + Uc_CompanySearch1.SubChannel + "<br>" + "Area  :" + Uc_CompanySearch1.Area + "<br>" + "Region  :" + Uc_CompanySearch1.Region + "<br>" + "Zone  :" + Uc_CompanySearch1.Zone   + "<br>" + "Location    :" + Uc_CompanySearch1.ProfitCenter + "<br>" + "Range   :" + TextBoxRange.Text ;
                        //}
                        break;
                    }
                //load empty grid
                case CommonUIDefiniton.SearchUserControlType.Item_Serials:
                    {
                        paramsText.Append("-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "Loc" + seperator + "-999" + seperator + "-999" + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }
        #endregion



        protected void Exit_Click(object sender, EventArgs e)
        {
            ClearFileds();
            Panel1.Visible = false;

        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtItemCode.Text.Length > 0)
            {
                if (ddlStatus.SelectedIndex != -1)
                {
                    fillSerials(Session["UserCompanyCode"].ToString(), ViewState["Location"].ToString(), txtItemCode.Text, ddlStatus.SelectedValue);
                }
            }
            else
            {

                txtItemCode.Focus();
                txtItemCode.ToolTip = "Please Enter Item Code???";
            }
        }
        #endregion

        protected void SearchItems_Click(object sender, ImageClickEventArgs e)
        {
            _basePage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
           DataTable dataSource = _basePage.CHNLSVC.CommonSearch.GetItemSearchData(ucc.SearchParams, null, null);
           ucc.BindUCtrlDDLData(dataSource);
           ucc.BindUCtrlGridData(dataSource);
           ucc.ReturnResultControl = txtItemCode.ClientID;
           ucc.UCModalPopupExtender.Show();



        }

        #region LoadCurrentScheme
        protected void LoadCurrentScheme()
        {
            string item_code = txtItemCode.Text.Trim();
            string promocode = "";
            List<MasterSalesPriorityHierarchy> _Saleshir = _basePage.CHNLSVC.Sales.GetSalesPriorityHierarchy(Session["UserCompanyCode"].ToString(), ViewState["PrfCenter"].ToString(), CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
            string _channel = "";
            string _typeChnl = "SCHNL";
            DateTime currentDate = DateTime.Now.Date;
            ViewState["currentDate"] = currentDate;

            if (_Saleshir.Count > 0)
            {
                _channel = (from _lst in _Saleshir
                            where _lst.Mpi_cd == "SCHNL"
                            select _lst.Mpi_val).ToList<string>()[0];
            }

            string _type = "PC";
            string _value = ViewState["PrfCenter"].ToString();


            string _item = "";
            string _brand = "";
            string _mainCat = "";
            string _subCat = "";
            string _pb = "";
            string _lvl = "";

            _SchemeDefinition = new List<HpSchemeDefinition>();

            MasterItem _masterItemDetails = new MasterItem();
            _masterItemDetails = _basePage.CHNLSVC.Sales.getMasterItemDetails(Session["UserCompanyCode"].ToString(), item_code, 1);

            _item = _masterItemDetails.Mi_cd;
            _brand = _masterItemDetails.Mi_brand;
            _mainCat = _masterItemDetails.Mi_cate_1;
            _subCat = _masterItemDetails.Mi_cate_2;
            _pb = ViewState["pb_book"].ToString();
            _lvl = ViewState["pb_level"].ToString();


            List<HpSchemeDefinition> _processList = new List<HpSchemeDefinition>();


            if (!string.IsNullOrEmpty(promocode))
            {
                //get details according to selected promotion code
                List<HpSchemeDefinition> _def4 = _basePage.CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, Convert.ToDateTime(ViewState["currentDate"]), null, null, null, null, null, promocode);
                if (_def4 != null)
                {
                    _processList.AddRange(_def4);

                }

                List<HpSchemeDefinition> _defChnl4 = _basePage.CHNLSVC.Sales.GetAllSchemeNew(_typeChnl, _channel, _pb, _lvl, Convert.ToDateTime(ViewState["currentDate"]), null, null, null, null, null, promocode);
                if (_defChnl4 != null)
                {
                    _processList.AddRange(_defChnl4);
                }
            }
            else
            {
                //get details from item
                List<HpSchemeDefinition> _def = _basePage.CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, Convert.ToDateTime(ViewState["currentDate"]), _item, null, null, null, null, null);
                if (_def != null)
                {
                    _processList.AddRange(_def);

                }

                List<HpSchemeDefinition> _defChnl = _basePage.CHNLSVC.Sales.GetAllSchemeNew(_typeChnl, _channel, _pb, _lvl, Convert.ToDateTime(ViewState["currentDate"]), _item, null, null, null, null, null);
                if (_defChnl != null)
                {
                    _processList.AddRange(_defChnl);
                }



                //get details according to main category
                List<HpSchemeDefinition> _def1 = _basePage.CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, Convert.ToDateTime(ViewState["currentDate"]), null, _brand, _mainCat, null, null, null);
                if (_def1 != null)
                {
                    _processList.AddRange(_def1);

                }
                List<HpSchemeDefinition> _defChnl1 = _basePage.CHNLSVC.Sales.GetAllSchemeNew(_typeChnl, _channel, _pb, _lvl, Convert.ToDateTime(ViewState["currentDate"]), null, _brand, _mainCat, null, null, null);
                if (_defChnl1 != null)
                {
                    _processList.AddRange(_defChnl1);
                }


                //get details according to sub category
                List<HpSchemeDefinition> _def2 = _basePage.CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, Convert.ToDateTime(ViewState["currentDate"]), null, _brand, null, _subCat, null, null);
                if (_def2 != null)
                {
                    _processList.AddRange(_def2);

                }
                List<HpSchemeDefinition> _defChnl2 = _basePage.CHNLSVC.Sales.GetAllSchemeNew(_typeChnl, _channel, _pb, _lvl, Convert.ToDateTime(ViewState["currentDate"]), null, _brand, null, _subCat, null, null);
                if (_defChnl2 != null)
                {
                    _processList.AddRange(_defChnl2);
                }

                //get details according to price book and level
                List<HpSchemeDefinition> _def3 = _basePage.CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, Convert.ToDateTime(ViewState["currentDate"]), null, null, null, null, null, null);
                if (_def3 != null)
                {
                    _processList.AddRange(_def3);

                }
                List<HpSchemeDefinition> _defChnl3 = _basePage.CHNLSVC.Sales.GetAllSchemeNew(_typeChnl, _channel, _pb, _lvl, Convert.ToDateTime(ViewState["currentDate"]), null, null, null, null, null, null);
                if (_defChnl3 != null)
                {
                    _processList.AddRange(_defChnl3);
                }

            }

            List<HpSchemeDefinition> _newList = new List<HpSchemeDefinition>();

            if (_SchemeDefinition != null && _SchemeDefinition.Count > 0)
            {
                _newList = _SchemeDefinition;
                _SchemeDefinition = new List<HpSchemeDefinition>();
                foreach (HpSchemeDefinition i in _processList)
                {
                    List<HpSchemeDefinition> _select = (from _lst in _newList
                                                        where _lst.Hpc_sch_cd == i.Hpc_sch_cd && i.Hpc_is_alw == true
                                                        select _lst).ToList();

                    if (_select.Count > 0)
                    {
                        _SchemeDefinition.AddRange(_select);
                    }
                    else
                    {
                        _SchemeDefinition.RemoveAll(item => item.Hpc_sch_cd == i.Hpc_sch_cd);
                    }
                }

            }
            else
            {
                _SchemeDefinition.AddRange(_processList);
            }


            var _record = (from _lst in _SchemeDefinition
                           where _lst.Hpc_is_alw == false
                           select _lst).ToList().Distinct();

            foreach (HpSchemeDefinition j in _record)
            {
                _SchemeDefinition.RemoveAll(item => item.Hpc_sch_cd == j.Hpc_sch_cd && item.Hpc_seq <= j.Hpc_seq);

            }

            var _newRecord = (from _lst in _SchemeDefinition
                              select _lst.Hpc_sch_cd).ToList().Distinct();

            List<MasterSalesPriorityHierarchy> _Saleshir1 = _basePage.CHNLSVC.Sales.GetSalesPriorityHierarchy(Session["UserCompanyCode"].ToString(), ViewState["PrfCenter"].ToString(), CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

            if (_Saleshir1.Count > 0)
            {

                foreach (var j in _newRecord)
                {
                    foreach (MasterSalesPriorityHierarchy _one in _Saleshir1)
                    {
                        _type = _one.Mpi_cd;
                        _value = _one.Mpi_val;

                        _SchemeDetails = _basePage.CHNLSVC.Sales.getSchemeDetails(_type, _value, 1, j);

                        if (_SchemeDetails.Hsd_cd != null)
                        {

                            goto L000;

                        }

                    }

                L000:
                    if (_SchemeDetails.Hsd_cd == null)
                    {
                        _SchemeDefinition.RemoveAll(item => item.Hpc_sch_cd == j);
                    }

                }
            }


            var _final = (from _lst in _SchemeDefinition
                          select _lst.Hpc_sch_cd).ToList().Distinct();

            if (_final.Count() > 0)
            {

                ddlCurrScm.DataSource = _final.ToList();
                ddlCurrScm.DataBind();
                ddlCurrScm.Items.Insert(0, new ListItem("", "0"));
                ddlCurrScm.SelectedIndex = 0;

            }
            else
            {
                // MessageBox.Show("Scheme details not found.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        #endregion


        #region LoadScm
        protected void LoadScheme()
        {
            string item_code = txtItemCode.Text.Trim();
                string promocode = "";
                List<MasterSalesPriorityHierarchy> _Saleshir = _basePage.CHNLSVC.Sales.GetSalesPriorityHierarchy(Session["UserCompanyCode"].ToString(), ViewState["PrfCenter"].ToString(), CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                string _channel = "";
                string _typeChnl = "SCHNL";


                if (_Saleshir.Count > 0)
                {
                    _channel = (from _lst in _Saleshir
                                where _lst.Mpi_cd == "SCHNL"
                                select _lst.Mpi_val).ToList<string>()[0];
                }

                string _type = "PC";
                string _value = ViewState["PrfCenter"].ToString();


                string _item = "";
                string _brand = "";
                string _mainCat = "";
                string _subCat = "";
                string _pb = "";
                string _lvl = "";

                _SchemeDefinition = new List<HpSchemeDefinition>();

                MasterItem _masterItemDetails = new MasterItem();
                _masterItemDetails = _basePage.CHNLSVC.Sales.getMasterItemDetails(Session["UserCompanyCode"].ToString(), item_code, 1);

                _item = _masterItemDetails.Mi_cd;
                _brand = _masterItemDetails.Mi_brand;
                _mainCat = _masterItemDetails.Mi_cate_1;
                _subCat = _masterItemDetails.Mi_cate_2;
                _pb = ViewState["pb_book"].ToString();
                _lvl = ViewState["pb_level"].ToString();


                List<HpSchemeDefinition> _processList = new List<HpSchemeDefinition>();


                if (!string.IsNullOrEmpty(promocode))
                {
                    //get details according to selected promotion code
                    List<HpSchemeDefinition> _def4 = _basePage.CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, Convert.ToDateTime(ViewState["AccDate"]), null, null, null, null, null, promocode);
                    if (_def4 != null)
                    {
                        _processList.AddRange(_def4);

                    }

                    List<HpSchemeDefinition> _defChnl4 = _basePage.CHNLSVC.Sales.GetAllSchemeNew(_typeChnl, _channel, _pb, _lvl, Convert.ToDateTime(ViewState["AccDate"]), null, null, null, null, null, promocode);
                    if (_defChnl4 != null)
                    {
                        _processList.AddRange(_defChnl4);
                    }
                }
                else
                {
                    //get details from item
                    List<HpSchemeDefinition> _def = _basePage.CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, Convert.ToDateTime(ViewState["AccDate"]), _item, null, null, null, null, null);
                    if (_def != null)
                    {
                        _processList.AddRange(_def);

                    }

                    List<HpSchemeDefinition> _defChnl = _basePage.CHNLSVC.Sales.GetAllSchemeNew(_typeChnl, _channel, _pb, _lvl, Convert.ToDateTime(ViewState["AccDate"]), _item, null, null, null, null, null);
                    if (_defChnl != null)
                    {
                        _processList.AddRange(_defChnl);
                    }



                    //get details according to main category
                    List<HpSchemeDefinition> _def1 = _basePage.CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, Convert.ToDateTime(ViewState["AccDate"]), null, _brand, _mainCat, null, null, null);
                    if (_def1 != null)
                    {
                        _processList.AddRange(_def1);

                    }
                    List<HpSchemeDefinition> _defChnl1 = _basePage.CHNLSVC.Sales.GetAllSchemeNew(_typeChnl, _channel, _pb, _lvl, Convert.ToDateTime(ViewState["AccDate"]), null, _brand, _mainCat, null, null, null);
                    if (_defChnl1 != null)
                    {
                        _processList.AddRange(_defChnl1);
                    }


                    //get details according to sub category
                    List<HpSchemeDefinition> _def2 = _basePage.CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, Convert.ToDateTime(ViewState["AccDate"]), null, _brand, null, _subCat, null, null);
                    if (_def2 != null)
                    {
                        _processList.AddRange(_def2);

                    }
                    List<HpSchemeDefinition> _defChnl2 = _basePage.CHNLSVC.Sales.GetAllSchemeNew(_typeChnl, _channel, _pb, _lvl, Convert.ToDateTime(ViewState["AccDate"]), null, _brand, null, _subCat, null, null);
                    if (_defChnl2 != null)
                    {
                        _processList.AddRange(_defChnl2);
                    }

                    //get details according to price book and level
                    List<HpSchemeDefinition> _def3 = _basePage.CHNLSVC.Sales.GetAllSchemeNew(_type, _value, _pb, _lvl, Convert.ToDateTime(ViewState["AccDate"]), null, null, null, null, null, null);
                    if (_def3 != null)
                    {
                        _processList.AddRange(_def3);

                    }
                    List<HpSchemeDefinition> _defChnl3 = _basePage.CHNLSVC.Sales.GetAllSchemeNew(_typeChnl, _channel, _pb, _lvl, Convert.ToDateTime(ViewState["AccDate"]), null, null, null, null, null, null);
                    if (_defChnl3 != null)
                    {
                        _processList.AddRange(_defChnl3);
                    }

                }

                List<HpSchemeDefinition> _newList = new List<HpSchemeDefinition>();

                if (_SchemeDefinition != null && _SchemeDefinition.Count > 0)
                {
                    _newList = _SchemeDefinition;
                    _SchemeDefinition = new List<HpSchemeDefinition>();
                    foreach (HpSchemeDefinition i in _processList)
                    {
                        List<HpSchemeDefinition> _select = (from _lst in _newList
                                                            where _lst.Hpc_sch_cd == i.Hpc_sch_cd && i.Hpc_is_alw == true
                                                            select _lst).ToList();

                        if (_select.Count > 0)
                        {
                            _SchemeDefinition.AddRange(_select);
                        }
                        else
                        {
                            _SchemeDefinition.RemoveAll(item => item.Hpc_sch_cd == i.Hpc_sch_cd);
                        }
                    }

                }
                else
                {
                    _SchemeDefinition.AddRange(_processList);
                }


                var _record = (from _lst in _SchemeDefinition
                               where _lst.Hpc_is_alw == false
                               select _lst).ToList().Distinct();

                foreach (HpSchemeDefinition j in _record)
                {
                    _SchemeDefinition.RemoveAll(item => item.Hpc_sch_cd == j.Hpc_sch_cd && item.Hpc_seq <= j.Hpc_seq);

                }

                var _newRecord = (from _lst in _SchemeDefinition
                                  select _lst.Hpc_sch_cd).ToList().Distinct();

                List<MasterSalesPriorityHierarchy> _Saleshir1 = _basePage.CHNLSVC.Sales.GetSalesPriorityHierarchy(Session["UserCompanyCode"].ToString(), ViewState["PrfCenter"].ToString(), CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                if (_Saleshir1.Count > 0)
                {

                    foreach (var j in _newRecord)
                    {
                        foreach (MasterSalesPriorityHierarchy _one in _Saleshir1)
                        {
                            _type = _one.Mpi_cd;
                            _value = _one.Mpi_val;

                            _SchemeDetails = _basePage.CHNLSVC.Sales.getSchemeDetails(_type, _value, 1, j);

                            if (_SchemeDetails.Hsd_cd != null)
                            {

                                goto L000;

                            }

                        }

                    L000:
                        if (_SchemeDetails.Hsd_cd == null)
                        {
                            _SchemeDefinition.RemoveAll(item => item.Hpc_sch_cd == j);
                        }

                    }
                }


                var _final = (from _lst in _SchemeDefinition
                              select _lst.Hpc_sch_cd).ToList().Distinct();

                if (_final.Count() > 0)
                {

                    ddlAccScm.DataSource = _final.ToList();
                    ddlAccScm.DataBind();
                    ddlAccScm.Items.Insert(0, new ListItem("", "0"));
                    ddlAccScm.SelectedIndex = 0;


                }
                else
                {
                    // MessageBox.Show("Scheme details not found.", "Account creation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }


        }
        #endregion

        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            if (txtItemCode.Text.Length > 0)
            {
                lblmsg.Visible = false;
                ddlStatus.SelectedIndex = 0;
                ddlSerial.Items.Clear();
                txtPrice.Text = "";
                if (ddlStatus.SelectedIndex != -1)
                {
                    fillSerials(Session["UserCompanyCode"].ToString(), ViewState["Location"].ToString(), txtItemCode.Text, ddlStatus.SelectedValue);
                }
              
                    _basePage = new BasePage();
                    string _item_desc = _basePage.CHNLSVC.Inventory.getItemDescription(txtItemCode.Text.Trim());
                    if (_item_desc != null)
                    {
                        lblmsg.Visible = false;
                        LoadScheme();
                        LoadCurrentScheme();
                    }
                    else
                    {
                        ddlAccScm.Items.Clear();
                        ddlCurrScm.Items.Clear();
                        lblmsg.Visible = true;
                        lblmsg.Text = "Invalid ItemCode..Try Again.";
                    }
                


                //LoadScheme();
                //LoadCurrentScheme();

            }
            else
            {

                txtItemCode.Focus();
                txtItemCode.ToolTip = "Please Enter Item Code???";
                //txtItemCode.CssClass = "error";
                txtItemCode.BackColor = System.Drawing.Color.Black;
            }

          




        }
    }
}