using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
//using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Enquiries.Imports
{

    public partial class ShipmentTracker : BasePage
    {
        private DataTable _dtSearch
        {
            get { if (Session["_dtSearch"] != null) { return (DataTable)Session["_dtSearch"]; } else { return new DataTable(); } }
            set { Session["_dtSearch"] = value; }
        }

        string search;
        private string selectLocation = "";
        string Select_company = "";

        string from = "";
        string to = "";
        Base _basePage;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["SearchPopUp"] = "Hide";
                if (Session["UserCompanyCode"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                //    TextBoxcmpny.ReadOnly = true;
                Session["allsearch"] = "hello";
                Labelmycompany.Text = TextBoxcmpny.Text;
                pageclear();
                Select_company = Session["UserCompanyCode"].ToString();
                Session["SelectCompany"] = Select_company;
                TextBoxcmpny.ToolTip = Labeldescription.Text;
                //userPermision
                //     TextBoxBL.ReadOnly = true;
                // TextBoxfrom.ReadOnly = true;
                //
                
                //       TextBoxcmpny.Text = Session["UserCompanyCode"].ToString();
                CheckBoxal.Checked = true;
                CheckBoxExcepcted.Checked = true;
                CheckBoxExcepcted.Enabled = true;
                //  getdetails();
                //        TextBoxcmpny.Enabled = false;

                /*          DateTime fromdt = DateTime.Now.AddMonths(-1);
                          TextBoxfrom.Text = fromdt.ToString("dd/MMM/yyyy");

                          DateTime todate = DateTime.Now;
                          TextBoxto.Text = todate.ToString("dd/MMM/yyyy");

                          txtFDate.Text = fromdt.ToString("dd/MMM/yyyy");
                          txtTDate.Text = todate.ToString("dd/MMM/yyyy");



                          TextBox7.Text = fromdt.ToString("dd/MMM/yyyy");
                          TextBox8.Text = todate.ToString("dd/MMM/yyyy");
                          */

                //  LoadItems();
                //  Load_Container_Count();

                //    Load_GRN();

                defaultdate();
                LinkButtonSearch_Click(null, null);

            }


            else
            {
                dvGrn.UseAccessibleHeader = true;

                //This will add the <thead> and <tbody> elements
                // dvGrn.HeaderRow.TableSection = TableRowSection.TableHeader;
                if (Session["IsSearch"] != null && Convert.ToBoolean(Session["IsSearch"]) == true)
                {
                    Session["IsSearch"] = null;
                    UserBL.Show();
                    TextBox1.Focus();
                    UserBL.Hide();
                }
                else if (Session["IsSearchs"] != null && Convert.ToBoolean(Session["IsSearchs"]) == true)
                {
                    Session["IsSearchs"] = null;
                    //UserPopoup.Show();
                    ModalPopupExtenderCompany.Show();
                    TextBoxcm.Focus();
                }
                if (Session["SearchPopUp"] != null)
                {
                    if (Session["SearchPopUp"].ToString() == "Show")
                    {
                        popUpSearchModel.Show();
                    }
                    else
                    {
                        popUpSearchModel.Hide();
                        Session["SearchPopUp"] = "Hide";
                    }
                }
                if (Session["CusdecUpdate"] != null)
                {
                    if (Session["CusdecUpdate"].ToString() == "Show")
                    {
                        ModalPopupExtenderUpdateCusdec.Show();
                    }
                    else
                    {
                        ModalPopupExtenderUpdateCusdec.Hide();
                        Session["CusdecUpdate"] = "Hide";
                    }
                }
            }



        }


        private void defaultdate()
        {
            TextBoxfrom.Text = DateTime.Now.AddDays(-7).ToString("dd/MMM/yyyy");
            TextBoxto.Text = DateTime.Now.ToString("dd/MMM/yyyy");

            TextBox7.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
            TextBox8.Text = DateTime.Now.ToString("dd/MMM/yyyy");

        }


        private void Load_GRN()
        {
            string company = "AAL";
            string doc_no = "AAL-PUR000013";
            _basePage = new Base();

            dt = _basePage.CHNLSVC.CommonSearch.GetGrnDt(company, doc_no, 0);
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }

        private void LoadItems(string doc_no, string _toBond)
        {


            _basePage = new Base();
            ViewState["SEARCHS"] = null;
            DataTable dt = _basePage.CHNLSVC.CommonSearch.BL_Items(doc_no, _toBond);

            int i = 0;
            foreach (var dtrow in dt.Rows)
            {
                if (dt.Rows[i]["IBI_TP"].ToString() == "C")
                {
                    dt.Rows[i]["IBI_TP"] = "CHAR";
                }
                if (dt.Rows[i]["IBI_TP"].ToString() == "F")
                {
                    dt.Rows[i]["IBI_TP"] = "FOC";
                }
                i++;
            }
            dvGrn.DataSource = dt;
            dvGrn.DataBind();
            ViewState["SEARCHS"] = dt;
        }
        public void LoadBLDateInfo()
        {
            _basePage = new Base();
            ViewState["dates"] = null;
            String company = "ABL";

            DataTable dt = _basePage.CHNLSVC.CommonSearch.GetSearchBLByDate(company);
            grdResultDate.DataSource = dt;
            grdResultDate.DataBind();
            ViewState["dates"] = dt;

        }


        private void Load_Container_Count(string blno)
        {
            _basePage = new Base();
            try
            {
                DataTable dt = _basePage.CHNLSVC.CommonSearch.Container_Count(blno);

                GRNCount.DataSource = dt;
                GRNCount.DataBind();
            }
            catch (Exception ex)
            {
                displaynotification("Containers not found");

            }

        }

        public void LoadDateRange()
        {
            _basePage = new Base();
            string company = "ABL";
            DataTable dt = _basePage.CHNLSVC.CommonSearch.FilterDateRange(company, txtFDate.Text, txtTDate.Text);
            grdResultDate.DataSource = dt;
            grdResultDate.DataBind();


        }



        private void pageclear()
        {
            gvBL.DataSource = new int[] { };
            gvBL.DataBind();

            dvGrn.DataSource = new int[] { };
            dvGrn.DataBind();

            GRNCount.DataSource = new int[] { };
            GRNCount.DataBind();

            dvResult.DataSource = new int[] { };
            dvResult.DataBind();


            gvMultipleItem.DataSource = new int[] { };
            gvMultipleItem.DataBind();

            GridView2.DataSource = new int[] { };
            GridView2.DataBind();

        }
        //call inside the popup call
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            //_basePage = new Base();
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        //  CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company.ToString();
                        paramsText.Append(TextBoxcmpny.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BLHeader:
                    {
                        // paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserSBU"].ToString() + seperator);
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserSBU"].ToString() + seperator + -1 + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(txtMainCategory.Text.ToUpper() + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtMainCategory.Text.ToUpper() + seperator + txtSubCategory.Text.ToUpper() + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        //

        // 
        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }

            this.ddlSearchbykey.SelectedIndex = 0;
        }


        public void BindUCtrlDDLData_new(DataTable _dataSource)
        {
            this.DropDownListcomp.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.DropDownListcomp.Items.Add(col.ColumnName);
            }

            this.DropDownListcomp.SelectedIndex = 0;
        }

        //


        //
        public void BindUCtrlDDLDatas(DataTable _dataSource)
        {
            this.DropDownList1.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.DropDownList1.Items.Add(col.ColumnName);
            }

            this.DropDownList1.SelectedIndex = 0;
        }

        //







        protected void LinkButton1_Click(object sender, EventArgs e)
        {


            _basePage = new Base();
            //ViewState["result"] = null;
            ////Loc_HIRC_Company
            //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
            //// DataTable _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, null, null);
            //DataTable _result = _basePage.CHNLSVC.CommonSearch.GetCompanySearchDetails(SearchParams, null, null);
            //dvResult.DataSource = _result;
            //dvResult.DataBind();
            //lblvalue.Text = "Company";
            //BindUCtrlDDLData(_result);
            //UserPopoup.Show();
            //ViewState["result"] = _result;
            //Session["IsSearchs"] = true;


            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
            DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
            grdResult.DataSource = _result;
            grdResult.DataBind();
            lblvalue.Text = "Company";
            BindUCtrlDDLData(_result);
            serchpopup.Show();
            return;
        }

        DataTable data = null;
        bool status = false;

        //permision base enable
        protected void disableControls()
        {
            //int count = int.Parse(gvBL.Rows.Count.ToString());
            int count = gvBL.Rows.Count;
            for (int i = 0; i < count; i++)
            {

                //  CheckBox cb = (CheckBox)GridView1.Rows[i].Cells[0].FindControl("CheckBox1");
                TextBox tb = (TextBox)gvBL.Rows[i].Cells[10].FindControl("TextBox6");
                tb.Enabled = false;
                TextBox tb1 = (TextBox)gvBL.Rows[i].Cells[10].FindControl("dochandovertxt");
                tb1.Enabled = false;
                TextBox tb2 = (TextBox)gvBL.Rows[i].Cells[10].FindControl("clearancetxt");
                tb2.Enabled = false;

                TextBox tb3 = (TextBox)gvBL.Rows[i].Cells[10].FindControl("ActualEtatxt");
                tb3.Enabled = false;

                LinkButton lb = (LinkButton)gvBL.Rows[i].Cells[10].FindControl("clearancebtn");
                lb.Visible = false;

                LinkButton lb1 = (LinkButton)gvBL.Rows[i].Cells[10].FindControl("dochandoverbtn");
                lb1.Visible = false;

                LinkButton lb2 = (LinkButton)gvBL.Rows[i].Cells[10].FindControl("actualetabtn");
                lb2.Visible = false;


            }
        }


        protected void EnableControls()
        {

            int count = gvBL.Rows.Count;
            for (int i = 0; i < count; i++)
            {


                TextBox tb = (TextBox)gvBL.Rows[i].Cells[10].FindControl("TextBox6");
                tb.Enabled = true;
                TextBox tb1 = (TextBox)gvBL.Rows[i].Cells[10].FindControl("dochandovertxt");
                tb1.Enabled = true;
                TextBox tb2 = (TextBox)gvBL.Rows[i].Cells[10].FindControl("clearancetxt");
                tb2.Enabled = true;

                TextBox tb3 = (TextBox)gvBL.Rows[i].Cells[10].FindControl("ActualEtatxt");
                tb3.Enabled = true;



                LinkButton lb = (LinkButton)gvBL.Rows[i].Cells[10].FindControl("clearancebtn");
                if (lb != null)
                {
                    lb.Visible = true;
                }

                LinkButton lb1 = (LinkButton)gvBL.Rows[i].Cells[10].FindControl("dochandoverbtn");
                if (lb1 != null)
                {
                    lb1.Visible = true;
                }

                LinkButton lb2 = (LinkButton)gvBL.Rows[i].Cells[10].FindControl("actualetabtn");
                if (lb2 != null)
                {
                    lb2.Visible = true;
                }



            }
        }


        //

        //new edit

        private void getdetails()
        {
            _basePage = new Base();
            TextBoxcmpny.Text = "ABL";
            ViewState["BLckeck"] = null;
            //gvBL.PageSize = 7;
            status = true;
            TextBoxcmpny.Text = string.Empty;
            TextBoxcmpny.Enabled = true;

            data = _basePage.CHNLSVC.CommonSearch.GetBLALL(status);
            if (data != null)
            {
                AddAccualLeadTime(data);
                AddError(data);
            }
            //   gvBL.PageSize = 5;
            gvBL.DataSource = data;
            gvBL.DataBind();
            ViewState["BLckeck"] = data;


            _basePage = new Base();
            //if (_basePage.CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10120))
            //{
            //    EnableControls();
            //}
            //else 
            //{
            //    disableControls();
            //}

        }

        //



        //protected void LinkButton1_Click(object sender, EventArgs e)
        //{

        //    _basePage = new Base();
        //    if (CheckBoxal.Checked == true)
        //    {

        //        TextBoxcmpny.Text = "ABL";
        //        ViewState["BLckeck"] = null;
        //        //gvBL.PageSize = 7;
        //        status = true;
        //        TextBoxcmpny.Text = string.Empty;
        //        TextBoxcmpny.Enabled = true;

        //        data = _basePage.CHNLSVC.CommonSearch.GetBLALL(status);
        //        //   gvBL.PageSize = 5;
        //        gvBL.DataSource = data;
        //        gvBL.DataBind();
        //        ViewState["BLckeck"] = data;

        //        //tool tip

        //        string toolTip="";
        //        foreach (DataRow row in data.Rows)
        //        {
        //          //  Label Entry = (Label)row.findcon("TextBox6");
        //            toolTip = row["IB_SUPP_CD"].ToString();
        //        }

        //      //  string toolTip = "";


        //      foreach (DataRow row in data.Rows)
        //      {
        //           // for (int i = 0; i < gvBL.Rows.Count; i++) { 

        //            DateTime etd = data.Rows[0].Field<DateTime>("IB_ETD");
        //            string etd_s = etd.ToString("dd/MMM/yyy");

        //            DateTime eta = data.Rows[0].Field<DateTime>("ib_act_eta");
        //            string eta_s = eta.ToString("dd/MMM/yyy");
        //            int lead=0;
        //                try
        //            {
        //                 lead = data.Rows[0].Field<int>("IB_ACT_LEAD");
        //            }
        //            catch (Exception ex) {
        //                lead = 0;
        //            }
        //                double lead_d=Convert.ToDouble(lead);
        //            double days = (Convert.ToDateTime(eta) - Convert.ToDateTime(etd)).TotalDays;


        //          //  row.DefaultCellStyle.BackColor = Color.Red;

        //            if (lead_d < days)
        //            {
        //                gvBL.Rows[0].BackColor = ColorTranslator.FromHtml("#DDC9C9");
        //            }
        //            else
        //            {
        //                gvBL.Rows[0].BackColor = ColorTranslator.FromHtml("#D5E8D7");
        //            }


        //        //    }

        //        }


        //        //





        //        _basePage = new Base();
        //        if (_basePage.CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10120))
        //        {
        //            EnableControls();


        //        }
        //        else if (!_basePage.CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10120))
        //        {
        //            disableControls();
        //        }

        //    }
        //    else
        //    {
        //        TextBoxcmpny.Enabled = true;

        //        gvBL.DataSource = null;
        //        gvBL.DataBind();

        //    }



        //}

        private void LoadBLs()
        {
            _basePage = new Base();
            ViewState["bls"] = null;
            DataTable dt = _basePage.CHNLSVC.CommonSearch.LoadDstinctBL_No();
            BLLoad.DataSource = dt;
            BLLoad.DataBind();
            BindUCtrlDDLDatas(dt);
            ViewState["bls"] = dt;

        }

        public bool IsRawData = false;
        DataTable _result;


        protected void ImgSearch_Click(object sender, EventArgs e)
        {
            /* _basePage = new Base();
            
                 _result = null;
                 string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                 _result = _basePage.CHNLSVC.CommonSearch.GetCompanySearchDetails(SearchParams, cmbSearchbykey.SelectedValue, "%"+txtSearchbyword.Text);
                 dvResult.DataSource = _result;
                 dvResult.DataBind();
                 UserPopoup.Show();
                */


            _basePage = new Base();
            if (cmbSearchbykey.SelectedValue == "Code")
            {

                //Label8.Text == "Code"
                ViewState["result"] = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                _result = _basePage.CHNLSVC.CommonSearch.GetCompanySearchDetails(SearchParams, cmbSearchbykey.SelectedValue, txtSearchbyword.Text.Trim());
                dvResult.DataSource = _result;
                dvResult.DataBind();
                ViewState["result"] = _result;
                UserPopoup.Show();



            }
            else if (cmbSearchbykey.SelectedValue == "Description")
            {
                ViewState["result"] = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                _result = _basePage.CHNLSVC.CommonSearch.GetCompanySearchDetails(SearchParams, cmbSearchbykey.SelectedValue, txtSearchbyword.Text.Trim());
                dvResult.DataSource = _result;
                dvResult.DataBind();
                ViewState["result"] = _result;
                UserPopoup.Show();


            }






        }
        public void displayMessages(string msg)
        {
            //  ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickySuccessToast", "showStickySuccessToast('" + msg + "')", true);

        }

        DataTable dt = null;
        protected void LinkButtonsearch2_Click(object sender, EventArgs e)
        {
            _basePage = new Base();
            if (!string.IsNullOrEmpty(TextBoxcmpny.Text) && !CheckBoxal.Checked)
            {
                DataTable com = _basePage.CHNLSVC.General.GetCompanyInforDT(TextBoxcmpny.Text.ToUpper());
                if (com.Rows.Count == 0)
                {
                    displaynotification("Please select a valid company"); TextBoxcmpny.Text = ""; TextBoxcmpny.Focus(); return;
                }
            }
            if (!string.IsNullOrEmpty(TextBoxBL.Text))
            {
                ImportsBLHeader oheader = _basePage.CHNLSVC.Financial.GET_IMP_BL_BY_BL(Session["UserCompanyCode"].ToString(), TextBoxBL.Text);
                if (oheader == null)
                {
                    displaynotification("Please select a valid bl #"); TextBoxBL.Text = ""; TextBoxBL.Focus(); return;
                }
            }

            string blno = !string.IsNullOrEmpty(TextBoxBL.Text) ? TextBoxBL.Text : null;
            string company = !string.IsNullOrEmpty(TextBoxcmpny.Text) ? TextBoxcmpny.Text.ToUpper() : null;

            ViewState["gvBlDetails"] = null;

            dt = _basePage.CHNLSVC.CommonSearch.GetBLDetails(company, blno);
            if (dt != null)
            {
                AddAccualLeadTime(dt);
                AddError(dt);
                gvBL.DataSource = dt;
                gvBL.DataBind();
                ViewState["gvBlDetails"] = dt;
                Load_Container_Count(blno);
            }
        }

        protected void TextBoxcmpny_TextChanged(object sender, EventArgs e)
        {
        }

        protected void LinkButtonSearch_Click(object sender, EventArgs e)
        {
            //   LoadBLDateInfo();
            //   Usersppp.Show();

            //    UserDPopoup.Show();

            //string blno = TextBoxBL.Text;
            //string companys = TextBoxcmpny.Text;

            //if (companys == "" && blno != "")
            //{

            //    displaynotification("Please select the company");

            //}
            //if (companys != "" && blno == "")
            //{
            //    displaynotification("Please select the BL number");
            //}
            //if (companys == "" && blno == "")
            //{
            //    displaynotification("Enter the Valid Company and BL# Details");
            //}




            Session["allsearch"] = "hello";
            DataTable ds = null;
            ViewState["dateRange"] = null;
            ViewState["allsearch_bl"] = null;
            _basePage = new Base();
            string company = string.IsNullOrEmpty(TextBoxcmpny.Text) ? Session["UserCompanyCode"].ToString() : TextBoxcmpny.Text.ToUpper().Trim();

            //DateTime dateAndTime = Convert.ToDateTime(TextBoxfrom.Text);
            //DateTime dateValue;
            //string date = dateAndTime.ToString("dd/MMM/yyyy");
            //

            //gridviewcolor();

            // string dateValue;
            //if (DateTime.TryParse(date, out dateValue))
            //{

            DateTime dateValue;

            string from = TextBoxfrom.Text;
            string to = TextBoxto.Text;

            double days = (Convert.ToDateTime(TextBoxto.Text) - Convert.ToDateTime(TextBoxfrom.Text)).TotalDays;

            if (TextBoxfrom.Text == "" || TextBoxto.Text == "")
            {
                displaynotification("Enter Valid date Range");
            }
            else if (TextBoxfrom.Text == "" && TextBoxto.Text == "")
            {
                displaynotification("Enter Valid date Range");
            }


            if (DateTime.TryParse(from, out dateValue) && DateTime.TryParse(to, out dateValue))
            {
                ds = _basePage.CHNLSVC.CommonSearch.SearchAllBL(company, TextBoxBL.Text, TextBoxfrom.Text, TextBoxto.Text, TextBox7.Text, TextBox8.Text);
                //Added By Dulaj 2018/Dec/28 Check Pending ToBond
                if (ds != null)
                {
                    foreach (DataRow drtobond in ds.Rows)
                    {
                        DataTable toBondSts = _basePage.CHNLSVC.Inventory.LoadCusdecDatabyDoc(drtobond["ib_doc_no"].ToString());
                        if (toBondSts != null)
                        {
                            if (toBondSts.Rows.Count > 0)
                            {
                                if (toBondSts.Rows[0]["cuh_stus"].ToString().Equals("P"))
                                {
                                    drtobond["ib_ref_no"] = "N/A";
                                }
                            }
                        }
                    }
                }
                //
                if (ds != null)
                {
                    AddAccualLeadTime(ds);
                    AddError(ds);
                }
                if (ds.Rows.Count > 0)
                {
                    DataView dv = ds.DefaultView;
                    dv.Sort = "IB_ETA,IB_BL_NO DESC";
                    ds = dv.ToTable();
                }
                gvBL.DataSource = ds;
                gvBL.DataBind();
                ViewState["allsearch_bl"] = ds;
                ViewState["dateRange"] = ds;
                ViewState["BLckeck"] = ds;
                Load_Container_Count(TextBoxBL.Text);

            }
            else
            {
                displaynotification("Enter Valid date Range");
            }



            /*       ds = _basePage.CHNLSVC.CommonSearch.SearchAllBL(company, TextBoxBL.Text, TextBoxfrom.Text, TextBoxto.Text, TextBox7.Text, TextBox8.Text);
                   gvBL.DataSource = ds;
                   gvBL.DataBind();
                   ViewState["allsearch_bl"] = ds;
                   ViewState["dateRange"] = ds;

                   Load_Container_Count(TextBoxBL.Text);*/



            //   DataTable dt = _basePage.CHNLSVC.CommonSearch;


            //}
            //else
            //{
            //    string msg = "Invalid date format";
            //    ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "')", true);
            //}


        }

        private void AddAccualLeadTime(DataTable _dt)
        {
            _basePage = new Base();
            foreach (DataRow dr in _dt.Rows)
            {
                Int32 _deff = 0;
                if (!string.IsNullOrEmpty(dr["ib_doc_no"].ToString()) && !string.IsNullOrEmpty(dr["ib_act_eta"].ToString())
                    && (string.IsNullOrEmpty(dr["ib_act_lead"].ToString()) || dr["ib_act_lead"].ToString() == "0"))
                {

                    DateTime _ipPiDt = new DateTime();
                    DateTime _ibActEta = new DateTime();
                    DateTime.TryParse(dr["ib_act_eta"].ToString(), out _ibActEta);
                    DataTable _blItem = _basePage.CHNLSVC.CommonSearch.GetBlItmByDocNo(dr["ib_doc_no"].ToString());
                    if (_blItem != null)
                    {
                        if (_blItem.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(_blItem.Rows[0]["IP_PI_DT"].ToString()))
                            {
                                //if (dr["ib_doc_no"].ToString() == "ABL(INV)-SI-16-0011")
                                //{
                                //    string s = "";
                                //}
                                DateTime.TryParse(_blItem.Rows[0]["IP_PI_DT"].ToString(), out _ipPiDt);
                                TimeSpan ts = _ibActEta.Subtract(_ipPiDt);
                                _deff = ts.Days;
                                dr["ib_act_lead"] = _deff;
                            }
                        }

                    }
                }

            }
        }
        private void AddError(DataTable _dt)
        {
            foreach (DataRow dr in _dt.Rows)
            {
                Int32 _stLeadTime = 0;
                if (!string.IsNullOrEmpty(dr["Ib_standard_lead"].ToString()))
                {
                    Int32.TryParse(dr["Ib_standard_lead"].ToString(), out _stLeadTime);
                }
                Int32 _actLeadTime = 0;
                if (!string.IsNullOrEmpty(dr["ib_act_lead"].ToString()))
                {
                    Int32.TryParse(dr["ib_act_lead"].ToString(), out _actLeadTime);
                }
                dr["Ib_error_lead"] = (_actLeadTime - _stLeadTime).ToString();
            }
        }
        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            //filter data by date range
            LoadDateRange();
            Usersppp.Show();

            TextBoxfrom.Text = txtFDate.Text;
            TextBoxto.Text = txtTDate.Text;

            Session["frm"] = txtFDate.Text;
            Session["to"] = txtTDate.Text;
        }

        protected void btnDClose_Click(object sender, EventArgs e)
        {
            //  TextBoxfrom.Text = txtFDate.Text;
            //   TextBoxto.Text = txtTDate.Text;
        }
        DataTable ds;
        protected void Searchbtn_Click(object sender, EventArgs e)
        {
            DateTime dt;

            string from = TextBoxfrom.Text;
            string to = TextBoxto.Text;

            string c_from = TextBox7.Text;
            string c_to = TextBox8.Text;
            //gridviewcolor();
            try
            {
                //    DataTable com = _basePage.CHNLSVC.Inventory.GetCompany_D(TextBoxcmpny.Text);
                //DataTable com = _basePage.CHNLSVC.General.GetCompanyInforDT(TextBoxcmpny.Text);
                //if (com.Rows.Count == 0)
                //{
                //    displaynotification("Please select a valid company");
                //}
            }
            catch (Exception ex) { }



            // if (((from == "" && to == "") || (from == "" || to == "")) || ((c_from == "" && c_to == "") || (c_from == "" || c_to == ""))) 
            if (((from == "" && to == "") || (c_from == "" && c_to == "")) || ((from == "" && to == "") && (c_from == "" && c_to == "")))
            {

                Session["allsearch"] = "hello";
                ViewState["allsearch_bl"] = null;
                _basePage = new Base();
                string company = TextBoxcmpny.Text;
                ds = _basePage.CHNLSVC.CommonSearch.SearchAllBL(company, TextBoxBL.Text, TextBoxfrom.Text, TextBoxto.Text, TextBox7.Text, TextBox8.Text);
                if (ds != null)
                {
                    AddAccualLeadTime(ds);
                    AddError(ds);
                }
                gvBL.DataSource = ds;
                gvBL.DataBind();
                ViewState["allsearch_bl"] = ds;
                ViewState["BLckeck"] = ds;
                Load_Container_Count(TextBoxBL.Text);

            }

            else if ((DateTime.TryParse(from, out dt) && DateTime.TryParse(to, out dt)) || (DateTime.TryParse(c_from, out dt) && DateTime.TryParse(c_to, out dt)))
            {

                Session["allsearch"] = "hello";
                ViewState["allsearch_bl"] = null;
                _basePage = new Base();
                string company = string.IsNullOrEmpty(TextBoxcmpny.Text) ? Session["UserCompanyCode"].ToString() : TextBoxcmpny.Text.Trim().ToUpper();
                if (!CheckBoxPendeing.Checked)
                {
                    ds = _basePage.CHNLSVC.CommonSearch.GetShipmentTrackerDatByClearenseDate(new ImportsBLHeader() { Ib_com = company }, Convert.ToDateTime(TextBox7.Text), Convert.ToDateTime(TextBox8.Text));
                }
                if (CheckBoxPendeing.Checked)
                {
                    ds = _basePage.CHNLSVC.CommonSearch.GetShipmentTrackerDatByClearenseDatePending(new ImportsBLHeader() { Ib_com = company }, Convert.ToDateTime(TextBox7.Text), Convert.ToDateTime(TextBox8.Text));
                }
                if (CheckBoxActual.Checked)
                {
                    ds = _basePage.CHNLSVC.CommonSearch.GetShipmentTrackerDatByClearenseDateActual(new ImportsBLHeader() { Ib_com = company }, Convert.ToDateTime(TextBox7.Text), Convert.ToDateTime(TextBox8.Text));
                }
                //ds = _basePage.CHNLSVC.CommonSearch.SearchAllBL(company, TextBoxBL.Text, TextBoxfrom.Text, TextBoxto.Text, TextBox7.Text, TextBox8.Text);
                if (ds != null)
                {
                    //var results = (from myRow in ds.AsEnumerable()
                    //              where (myRow.Field<DateTime>("IB_DOC_CLEAR_DT") >= Convert.ToDateTime(TextBox7.Text) && myRow.Field<DateTime>("IB_DOC_CLEAR_DT") <= Convert.ToDateTime(TextBox8.Text))
                    //              select myRow).ToList();

                    ViewState["BLckeck"] = ds;
                    AddAccualLeadTime(ds);
                    AddError(ds);
                    gvBL.DataSource = ds;
                    gvBL.DataBind();
                    /// ViewState["allsearch_bl"] = ds;
                    Load_Container_Count(TextBoxBL.Text);
                }

            }
            else
            {

                displaynotification("Enter Valid Date Range");
            }
        }

        protected void dvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxcmpny.Text = dvResult.SelectedRow.Cells[1].Text;
            LabelBLs.Text = dvResult.SelectedRow.Cells[1].Text;
            Labeldescription.Text = dvResult.SelectedRow.Cells[2].Text;
            TextBoxcmpny.ToolTip = Labeldescription.Text;
            ViewState["state"] = LabelBLs.Text;
        }

        protected void grdResultDate_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (TextBoxfrom.Text == "" && TextBoxto.Text == "")
            {
                TextBoxfrom.Text = grdResultDate.SelectedRow.Cells[3].Text;
                TextBoxto.Text = grdResultDate.SelectedRow.Cells[3].Text;
            }
        }

        protected void dvGrn_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

            // SIPopup.Show();
        }

        protected void dvGrn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvGrn.PageIndex = e.NewPageIndex;
            dvGrn.DataSource = null;
            dvGrn.DataSource = (DataTable)ViewState["SEARCHS"];
            dvGrn.DataBind();
            // SIPopup.Show();
            //  txtSearchbyword.Focus();
        }

        protected void dvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvResult.PageIndex = e.NewPageIndex;
            dvResult.DataSource = null;
            dvResult.DataSource = (DataTable)ViewState["result"];
            dvResult.DataBind();
            UserPopoup.Show();
        }

        protected void grdResultDate_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            grdResultDate.PageIndex = e.NewPageIndex;
            grdResultDate.DataSource = null;
            grdResultDate.DataSource = (DataTable)ViewState["dates"];
            grdResultDate.DataBind();
            Usersppp.Show();


        }

        protected void LinkButtonBLLoad_Click(object sender, EventArgs e)
        {

            // LoadBLs();

            _basePage = new Base();
            ViewState["bls"] = null;
            // DataTable dt = _basePage.CHNLSVC.CommonSearch.LoadDstinctBL_No();
            // BLLoad.DataSource = dt;
            // BLLoad.DataBind();
            // BindUCtrlDDLDatas(dt);
            // ViewState["bls"] = dt;

            // UserBL.Show();
            //// ViewState["bls"] = true;
            // Session["IsSearch"] = true;
            // // ModalPopupExtenderBL.show();



            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
            DataTable _result = _basePage.CHNLSVC.CommonSearch.SearchBLHeaderWithSeq(SearchParams, null, null);
            if (_result.Rows.Count > 0)
            {
                DataView dv = _result.DefaultView;
                dv.Sort = "DATE ASC";
                _result = dv.ToTable();
            }
            grdResult.DataSource = _result;
            grdResult.DataBind();
            lblvalue.Text = "BL";
            BindUCtrlDDLData(_result);
            serchpopup.Show();
            return;
        }

        protected void BLLoad_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BLLoad.PageIndex = e.NewPageIndex;
            BLLoad.DataSource = null;
            BLLoad.DataSource = (DataTable)ViewState["bls"];
            BLLoad.DataBind();
            UserBL.Show();

        }

        protected void BLLoad_SelectedIndexChanged(object sender, EventArgs e)
        {

            LabelBLs.Text = BLLoad.SelectedRow.Cells[1].Text;
            TextBoxBL.Text = BLLoad.SelectedRow.Cells[1].Text;


        }

        public void displaynotification(string msg)
        {

            //  ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "')", true);

        }

        protected void dvResult_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //      TextBoxcmpny.Text = dvResult.SelectedRow.Cells[1].Text;

        }

        protected void gvBL_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // DataTable dt = _basePage.CHNLSVC.CommonSearch.UpdateBLDetails_shipping();

            if (e.CommandName == "AddToCart")
            {

                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button clicked 
                // by the user from the Rows collection.      
                GridViewRow row = gvBL.Rows[index];

                // Calculate the new price.
                //TextBox Entry = (TextBox)row.FindControl("TextBox6");
                //TextBox handover = (TextBox)row.FindControl("dochandovertxt");
                //TextBox clearance = (TextBox)row.FindControl("clearancetxt");
                //TextBox actualEta = (TextBox)row.FindControl("ActualEtatxt");
                //TableCell blno = row.Cells[1];
                //      string bl_value = gvBL.SelectedRow.Cells[1].Text;

                string Entry = (row.FindControl("TextBox6") as TextBox).Text;
                string handover = (row.FindControl("dochandovertxt") as TextBox).Text;
                string clearance = (row.FindControl("clearancetxt") as TextBox).Text;
                string actualEta = (row.FindControl("ActualEtatxt") as TextBox).Text;
                string blno = (row.FindControl("ib_bl_no") as Label).Text;
                string p_loc = (row.FindControl("txtLoc") as TextBox).Text.ToUpper();
                string lblActTime = (row.FindControl("lblActTime") as TextBox).Text.ToUpper();
                _basePage = new Base();
                MasterLocationNew _mstLoc = _basePage.CHNLSVC.General.GetMasterLocation(new MasterLocationNew() { Ml_loc_cd = p_loc, Ml_com_cd = Session["UserCompanyCode"].ToString(), Ml_act = 1 });
                if (_mstLoc == null)
                {
                    displaynotification("Please enter a valid location code"); return;
                }

                string bl = blno;
                Int32 d = 0;
                Int32 _actLeadTime = Int32.TryParse(lblActTime, out d) ? d : 0;


                string doc_handOver_date = handover;
                string clearance_day = clearance;
                string entryno = Entry;
                string actual = actualEta;

                int effect = 0;
                DateTime dateValue;
                _basePage = new Base();


                //   _basePage = new Base();
                try
                {
                    if (DateTime.TryParse(doc_handOver_date, out dateValue) || doc_handOver_date == "")
                    {
                        if (DateTime.TryParse(actual, out dateValue) || actual == "")
                        {
                            if (DateTime.TryParse(clearance_day, out dateValue) || clearance_day == "")
                            {


                                effect = _basePage.CHNLSVC.CommonSearch.UpdateBLDetails_shipping(clearance_day, entryno, Convert.ToDateTime(doc_handOver_date), actual, bl, p_loc, _actLeadTime);


                                //  gvBL.DataSource = dt;
                                //  gvBL.DataBind();

                                string msgg = " B/L Number:    " + bl + "  is updated successfully";
                                //  ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);
                                ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickySuccessToast", "showStickySuccessToast('" + msgg + "')", true);

                            }
                            else
                            {
                                displaynotification("Enter valid data");

                            }
                        }




                        else
                        {

                            displaynotification("Enter valid data");
                        }

                    }

                    else
                    {
                        string msg = "Enter valid data";
                        //  ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);
                        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "')", true);

                    }

                }
                catch (Exception ex)
                {
                    string msg = "Enter valid data";
                    //  ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);
                    ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "')", true);


                }

            }
            //Added By Dulaj 2018-Dec-06

            if (e.CommandName == "UpdateCusdec")
            {

                ModalPopupExtenderUpdateCusdec.Show();
                Session["CusdecUpdate"] = "Show";
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button clicked 
                // by the user from the Rows collection.      
                GridViewRow row = gvBL.Rows[index];

                // Calculate the new price.
                //TextBox Entry = (TextBox)row.FindControl("TextBox6");
                //TextBox handover = (TextBox)row.FindControl("dochandovertxt");
                //TextBox clearance = (TextBox)row.FindControl("clearancetxt");
                //TextBox actualEta = (TextBox)row.FindControl("ActualEtatxt");
                //TableCell blno = row.Cells[1];
                //      string bl_value = gvBL.SelectedRow.Cells[1].Text;

                string Entry = (row.FindControl("lblToBond") as Label).Text;
                //string handover = (row.FindControl("dochandovertxt") as TextBox).Text;
                //string clearance = (row.FindControl("clearancetxt") as TextBox).Text;
                //string actualEta = (row.FindControl("ActualEtatxt") as TextBox).Text;
                //string blno = (row.FindControl("ib_bl_no") as Label).Text;
                //string p_loc = (row.FindControl("txtLoc") as TextBox).Text.ToUpper();
                //string lblActTime = (row.FindControl("lblActTime") as TextBox).Text.ToUpper();
                //string doc_siNo = (row.FindControl("ib_doc_no") as Label).Text;
                if (string.IsNullOrEmpty(Entry))
                {
                    ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('To bond number is empty')", true);
                    ModalPopupExtenderUpdateCusdec.Hide();
                    Session["CusdecUpdate"] = "Hide";
                    return;
                }
                DataTable dt = CHNLSVC.Financial.GetCusdecHDRDataShipment(Entry);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string clearBy = dt.Rows[0]["CLEAR BY"].ToString();
                        string model = dt.Rows[0]["MODEL"].ToString();
                        string cif = dt.Rows[0]["CIF"].ToString();
                        string rmk = dt.Rows[0]["REMARK"].ToString();
                        string clrUser = dt.Rows[0]["CLEARD USER"].ToString();
                        string ClrDate = dt.Rows[0]["CLEAR DATE"].ToString();
                        string entry = dt.Rows[0]["ENTRY"].ToString();
                        string exchnge = dt.Rows[0]["EXCHANGE RT"].ToString();
                        string total_amt = dt.Rows[0]["TOTAL AMOUNT"].ToString();
                        string fileRecDate = dt.Rows[0]["FILE RECEVIED DT"].ToString();
                        string ActualClearDt = dt.Rows[0]["ACTUAL CLR DT"].ToString();
                        if (!(string.IsNullOrEmpty(fileRecDate)))
                        {
                            if (Convert.ToDateTime(fileRecDate) == DateTime.MinValue)
                            {
                                fileRecDate = string.Empty;
                            }
                        }
                        if (!(string.IsNullOrEmpty(ActualClearDt)))
                        {
                            if (Convert.ToDateTime(ActualClearDt) == DateTime.MinValue)
                            {
                                ActualClearDt = string.Empty;
                            }
                            else
                            {
                                DateTime dtactual = Convert.ToDateTime(ActualClearDt);
                                ActualClearDt = dtactual.ToString("dd/MMM/yyyy");
                            }
                        }
                        
                        TextBox2.Text = ActualClearDt;
                        TextBox3.Text = fileRecDate;
                        if (cif == "")
                        {
                            if (exchnge != "" && total_amt != "")
                            {
                                decimal exRt = Convert.ToDecimal(exchnge);
                                decimal tot_val = Convert.ToDecimal(total_amt);
                                cif = Math.Round((exRt * tot_val), 2).ToString();
                            }
                        }

                        TextBoxCusDocNo.Text = Entry;
                        TextBoxCleardBy.Text = clearBy;
                        TextBoxCusdecModal.Text = model;
                        TextBoxCIF.Text = cif;
                        TextBoxEntryNo.Text = entry;
                        TextBoxRemark.Text = rmk;
                    }
                }
                // clearCusdecPopup();           
            }



        }

        protected void gvBL_PageIndexChanged(object sender, EventArgs e)
        {

        }
        protected void closeCusdecsearch(object sender, EventArgs e)//To visible closed popup
        {
            //ModalPopupExtenderSearchCusdec.Hide();
        }


        public void gridviewcolor()
        {

            for (int i = 0; i < gvBL.Rows.Count; i++)
            {

                //  DateTime etd = data.Rows[0].Field<DateTime>("IB_ETD");
                DateTime etd = Convert.ToDateTime(gvBL.Rows[i].Cells[7].Text);

                DateTime eta = Convert.ToDateTime(gvBL.Rows[i].Cells[8].Text.ToString());


                int lead = 0;

                try
                {
                    lead = int.Parse(gvBL.Rows[i].Cells[14].Text);
                }
                catch (Exception ex)
                {
                    lead = 0;
                }

                double lead_d = Convert.ToDouble(lead);
                double days = (eta - etd).TotalDays;
                if (lead_d < days)
                {

                    gvBL.Rows[i].BackColor = Color.LightCyan;
                }
                else
                {
                    gvBL.Rows[i].BackColor = Color.LightGreen;
                    // gvBL.Rows[0].BackColor = ColorTranslator.FromHtml("#D5E8D7");
                    // Row.BackColor = System.Drawing.Color.Green;
                }
                //  string etd_s = etd.ToString("dd/MMM/yyy");

                //    DateTime eta = data.Rows[0].Field<DateTime>("ib_act_eta");
                //    string eta_s = eta.ToString("dd/MMM/yyy");
                //    int lead = 0;



            }

        }

        protected void gvBL_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string search = Session["allsearch"].ToString();
            //search = "hello";
            if (CheckBoxal.Checked == true)
            {
                gvBL.PageIndex = e.NewPageIndex;
                gvBL.DataSource = null;
                gvBL.DataSource = (DataTable)ViewState["BLckeck"];
                gvBL.DataBind();

                //color the grid
                //  gridviewcolor();



            }

            else if (search != "")
            {
                gvBL.PageIndex = e.NewPageIndex;
                gvBL.DataSource = null;
                gvBL.DataSource = (DataTable)ViewState["allsearch_bl"];
                gvBL.DataBind();

                //  gridviewcolor();

            }

            else
            {

                gvBL.PageIndex = e.NewPageIndex;
                gvBL.DataSource = null;
                gvBL.DataSource = (DataTable)ViewState["dateRange"];
                gvBL.DataBind();

                //gridviewcolor();
            }



        }

        protected void gvBL_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckBox1.Checked = false;
            CheckBox2.Checked = false;
            CheckBox3.Checked = false;
            CheckBox4.Checked = false;
            CheckBox5.Checked = false;
            _basePage = new Base();


            //foreach (GridViewRow item in gvBL.Rows)
            //{
            //    item.BackColor = System.Drawing.Color.Transparent;
            //    //  item.BackColor = System.Drawing.Color.DarkOrange;
            //}
            string index = gvBL.SelectedRow.RowIndex.ToString();
            //if (gvBL.SelectedRow.Cells[0].Text == "")
            //{
            //    //GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            //    //   row.BackColor = System.Drawing.Color.LightCyan;

            //}

            string doc_no;

            //Lable_doc_no.Text = gvBL.SelectedRow.Cells[3].Text;
            // GridViewRow row = gvBL.SelectedRow;
            string doc = (gvBL.SelectedRow.FindControl("ib_doc_no") as Label).Text;
            string lblToBond = (gvBL.SelectedRow.FindControl("lblToBond") as Label).Text;
            // Label doc = (Label)row.FindControl("ib_doc_no");
            Lable_doc_no.Text = doc;
            doc_no = doc;//gvBL.SelectedRow.Cells[3].Text;
            DataTable dt = _basePage.CHNLSVC.CommonSearch.BL_Items(doc_no, lblToBond);
            int i = 0;
            foreach (var dtrow in dt.Rows)
            {
                if (dt.Rows[i]["IBI_TP"].ToString() == "C")
                {
                    dt.Rows[i]["IBI_TP"] = "CHAR";
                }
                if (dt.Rows[i]["IBI_TP"].ToString() == "F")
                {
                    dt.Rows[i]["IBI_TP"] = "FOC";
                }
                i++;
            }

            dvGrn.DataSource = dt;
            dvGrn.DataBind();

            // gridviewcolor();
            //  GridViewRow drTemop = gvBL.Rows[index];

            //gvBL.SelectedIndex = System.Drawing.Color.Blue;
            int sIndex = gvBL.SelectedIndex;
            //if (gvBL.Rows[sIndex].BackColor == System.Drawing.Color.Red)
            //{
            //    gvBL.Rows[sIndex].BackColor = System.Drawing.Color.White;
            //}
            //else
            //{
            //    gvBL.Rows[sIndex].BackColor = System.Drawing.Color.LightCyan;
            //}  



            try
            {
                //  DataTable ds = _basePage.CHNLSVC.Inventory.getGrn_details(doc_no);
                //DataTable ds = _basePage.CHNLSVC.CommonSearch.GetGrnDt("ABL", doc_no, 0);
                //chg by lakshan 16Oct2017
                DataTable ds = _basePage.CHNLSVC.CommonSearch.GetGrnDt(Session["UserCompanyCode"].ToString(), doc_no, 0);
                if (ds.Rows.Count == 0)
                {
                    CheckBox3.Checked = false;
                }
                else
                {
                    CheckBox3.Checked = true;
                }
                //  DataTable result = _basePage.CHNLSVC.CommonSearch.GetDemurage_Para(Session["UserCompanyCode"].ToString(), doc_no, Convert.ToDateTime(DateTime.Now), Convert.ToDateTime(DateTime.Now));
                ImportsCostHeader _cstHdr = new ImportsCostHeader();
                _cstHdr = _basePage.CHNLSVC.Financial.GET_IMP_CST_HDR_BY_DOC(doc_no, null);

                if (_cstHdr != null)
                {
                    if (_cstHdr.Ich_actl == 0)
                    {
                        CheckBox5.Checked = false;
                    }
                    else
                    {
                        CheckBox5.Checked = true;
                    }
                }
                else
                {
                    CheckBox5.Checked = false;
                }
            }
            catch (Exception ex)
            {

            }
            if (dt != null)
            {
                if (dt.Rows.Count < 1)
                {
                    displaynotification("No item details found !!!"); return;
                }
            }
            else
            {
                displaynotification("No item details found !!!"); return;
            }
            string pinumber = Convert.ToString(dt.Rows[0].Field<string>("ibi_pi_no"));


            LoadItems(doc_no, lblToBond);
            Load_Container_Count(doc_no);



            // string blnumber = gvBL.SelectedRow.Cells[1].Text;
            // string cusdec = gvBL.SelectedRow.Cells[12].Text;
            //TextBox Entry = (TextBox)row.FindControl("TextBox6");
            string blnumber = (gvBL.SelectedRow.FindControl("ib_bl_no") as Label).Text;
            string cusdec = (gvBL.SelectedRow.FindControl("lblToBond") as Label).Text;
            if (blnumber != "")
            {
                CheckBox2.Checked = true;
            }
            else
            {
                CheckBox2.Checked = false;
            }
            if (cusdec != "")
            {
                CheckBox4.Checked = true;
            }
            else
            {
                CheckBox4.Checked = false;
            }
            if (pinumber != "")
            {
                CheckBox1.Checked = true;
            }
            else
            {
                CheckBox1.Checked = false;
            }
        }


        /*
         protected void btngvbl_Click(object sender, EventArgs e)
         {

             foreach (GridViewRow item in gvBL.Rows)
             {
                 item.BackColor = System.Drawing.Color.Black;
             }
         }
         */
        protected void dvGrn_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow row = dvGrn.SelectedRow;
            Label rrdp = (Label)row.FindControl("ibi_line");

            DataTable dt = null;
            string company = TextBoxcmpny.Text;
            string doc_no = Lable_doc_no.Text;
            _basePage = new Base();
            //   string company = "AAL";
            //   string doc_no = "AAL-PUR000013";
            string item_lineno = rrdp.Text;//dvGrn.SelectedRow.Cells[8].Text;
            dt = _basePage.CHNLSVC.CommonSearch.GetGrnDt(company, doc_no, Convert.ToInt32(item_lineno));
            gvMultipleItem.DataSource = dt;
            gvMultipleItem.DataBind();


            if (dt.Rows.Count == 0)
            {

                string msg = "There is no GRN made for Item " + dvGrn.SelectedRow.Cells[1].Text;
                //  ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);
                ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "')", true);


            }
            else
            {

                multiplepopup.Show();

            }
            //     UserPopoup_check.Show();

        }

        protected void LinkButtonBLNOS_Click(object sender, EventArgs e)
        {
            _basePage = new Base();
            DataTable dt = null;
            if (DropDownList1.SelectedIndex == 0)
            {

                dt = _basePage.CHNLSVC.Inventory.SortBLDetails(TextBox1.Text, "", "", "");
                BLLoad.DataSource = dt;
                BLLoad.DataBind();
                UserBL.Show();
            }
            else if (DropDownList1.SelectedIndex == 1)
            {
                dt = _basePage.CHNLSVC.Inventory.SortBLDetails("", TextBox1.Text, "", "");
                BLLoad.DataSource = dt;
                BLLoad.DataBind();
                UserBL.Show();
            }

            else if (DropDownList1.SelectedIndex == 2)
            {
                dt = _basePage.CHNLSVC.Inventory.SortBLDetails("", "", TextBox1.Text, "");
                BLLoad.DataSource = dt;
                BLLoad.DataBind();
                UserBL.Show();

            }

            else if (DropDownList1.SelectedIndex == 3)
            {
                dt = _basePage.CHNLSVC.Inventory.SortBLDetails("", "", "", TextBox1.Text);
                BLLoad.DataSource = dt;
                BLLoad.DataBind();
                UserBL.Show();
            }
            // SortBLDetails

        }

        protected void gvBL_DataBound(object sender, EventArgs e)
        {


        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            /*     _basePage = new Base();
                 ViewState["result"] = null;
                 //Loc_HIRC_Company
                 string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                 // DataTable _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, null, null);
                 DataTable _result = _basePage.CHNLSVC.CommonSearch.GetCompanySearchDetails(SearchParams, null, null);
                 dvResult.DataSource = _result;
                 dvResult.DataBind();
                 // Label8.Text = "35";
                 BindUCtrlDDLData(_result);


                 UserPopoup.Show();
                 ViewState["result"] = _result;
                 Session["IsSearchs"] = true;
             */
        }

        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            _basePage = new Base();
            ViewState["result"] = null;
            //Loc_HIRC_Company
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
            // DataTable _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, null, null);
            DataTable _result = _basePage.CHNLSVC.CommonSearch.GetCompanySearchDetails(SearchParams, null, null);
            GridView2.DataSource = _result;
            GridView2.DataBind();
            // Label8.Text = "35";
            //  BindUCtrlDDLData(_result);
            BindUCtrlDDLData_new(_result);

            ModalPopupExtenderCompany.Show();
            ViewState["result"] = _result;
            Session["IsSearchs"] = true;

        }

        protected void LinkButtoncompany_Click(object sender, EventArgs e)
        {
            _basePage = new Base();
            if (DropDownListcomp.SelectedValue == "Code")
            {

                //Label8.Text == "Code"
                ViewState["result"] = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                _result = _basePage.CHNLSVC.CommonSearch.GetCompanySearchDetails(SearchParams, DropDownListcomp.SelectedValue, TextBoxcm.Text.Trim());
                GridView2.DataSource = _result;
                GridView2.DataBind();
                ViewState["result"] = _result;
                ModalPopupExtenderCompany.Show();



            }
            else if (DropDownListcomp.SelectedValue == "Description")
            {
                ViewState["result"] = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                _result = _basePage.CHNLSVC.CommonSearch.GetCompanySearchDetails(SearchParams, DropDownListcomp.SelectedValue, TextBoxcm.Text.Trim());
                GridView2.DataSource = _result;
                GridView2.DataBind();
                ViewState["result"] = _result;
                ModalPopupExtenderCompany.Show();


            }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBoxcmpny.Text = GridView2.SelectedRow.Cells[1].Text;
            LabelBLs.Text = GridView2.SelectedRow.Cells[1].Text;
            Labeldescription.Text = GridView2.SelectedRow.Cells[2].Text;
            TextBoxcmpny.ToolTip = Labeldescription.Text;
            ViewState["state"] = LabelBLs.Text;
        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            GridView2.DataSource = null;
            GridView2.DataSource = (DataTable)ViewState["result"];
            GridView2.DataBind();
            ModalPopupExtenderCompany.Show();
        }

        protected void gvBL_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    // e.Row.Cells[3].ToolTip = (e.Row.DataItem as DataRowView)["supp_name"].ToString();
                    // e.Row.Cells[4].ToolTip = (e.Row.DataItem as DataRowView)["agent_name"].ToString();

                    DateTime ETD = Convert.ToDateTime((e.Row.DataItem as DataRowView)["IB_ETD"].ToString());
                    DateTime ETA = Convert.ToDateTime((e.Row.DataItem as DataRowView)["ib_eta"].ToString());
                    TextBox ATA = e.Row.FindControl("ActualEtatxt") as TextBox;
                    if (ATA.Text != "")
                    {
                        DateTime ATAD = Convert.ToDateTime(ATA.Text);

                        double ETD_ETA = (ETD - ETA).TotalDays;
                        double ETD_ATA = (ATAD - ETD).TotalDays;

                        double value = ETD_ATA - ETD_ETA;
                        if (value > 0)
                        {
                            e.Row.BackColor = System.Drawing.Color.LightCoral;
                        }
                        else
                        {
                            e.Row.BackColor = System.Drawing.Color.LightGreen;
                        }

                        if (_basePage.CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10120))
                        {
                            EnableControls();
                        }
                        else
                        {
                            disableControls();
                        }
                    }
                    else
                    {
                        e.Row.BackColor = System.Drawing.Color.White;
                    }
                    // gridviewcolor();

                    //gridv
                    /*     DateTime etd = data.Rows[0].Field<DateTime>("IB_ETD");
                         string etd_s = etd.ToString("dd/MMM/yyy");

                         DateTime eta = data.Rows[0].Field<DateTime>("ib_act_eta");
                         string eta_s = eta.ToString("dd/MMM/yyy");
                         int lead = 0;

                         try
                         {
                             lead = data.Rows[0].Field<int>("IB_ACT_LEAD");
                         }
                         catch (Exception ex)
                         {
                             lead = 0;
                         }
                         double lead_d = Convert.ToDouble(lead);
                         double days = (Convert.ToDateTime(eta) - Convert.ToDateTime(etd)).TotalDays;

                         if (lead_d < days)
                         {
                             //gvBL.Rows[0].BackColor = ColorTranslator.FromHtml("#DDC9C9");
                             e.Row.BackColor = System.Drawing.Color.Red;
                         }
                         else
                         {
                            // gvBL.Rows[0].BackColor = ColorTranslator.FromHtml("#D5E8D7");
                             e.Row.BackColor = System.Drawing.Color.Green;
                         }

                         */



                }
            }
            catch (Exception ex) { }



            //   string name = gvBL.SelectedRow.Cells[0].Text;

            //Accessing TemplateField Column controls
            //    string country = (gvBL.SelectedRow.FindControl("lblCountry") as Label).Text;












        }
        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "Yes")
            {
                try
                {
                    Response.Redirect(Request.RawUrl, false);
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                }
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
            }
            txtClearlconformmessageValue.Value = "";
        }


        #region Modalpopup
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword2.ClientID + "').value = '';", true);
            string ID = grdResult.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "Company")
            {
                TextBoxcmpny.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "BL")
            {
                string BL = grdResult.SelectedRow.Cells[2].Text;
                TextBoxBL.Text = BL;
                lblvalue.Text = "";
                return;
            }
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            _basePage = new Base();
            grdResult.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "Company")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                serchpopup.Show();
                return;
            }
            if (lblvalue.Text == "BL")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                DataTable _result = _basePage.CHNLSVC.CommonSearch.SearchBLHeaderWithSeq(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                serchpopup.Show();
                return;
            }
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Company")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedValue, "%" + txtSearchbyword2.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                serchpopup.Show();
                return;
            }
            if (lblvalue.Text == "BL")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                _basePage = new Base();
                DataTable _result = _basePage.CHNLSVC.CommonSearch.SearchBLHeaderWithSeq(SearchParams, ddlSearchbykey.SelectedValue, "%" + txtSearchbyword2.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                serchpopup.Show();
                return;
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            _basePage = new Base();
            if (lblvalue.Text == "Company")
            {

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword2.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                serchpopup.Show();
                return;
            }
            if (lblvalue.Text == "BL")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                DataTable _result = _basePage.CHNLSVC.CommonSearch.SearchBLHeaderWithSeq(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword2.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                serchpopup.Show();
                return;
            }


        }
        #endregion

        protected void CheckBoxal_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxal.Checked)
            {
                TextBoxcmpny.Text = "";
            }
            _basePage = new Base();
            if (CheckBoxal.Checked == true)
            {
                ViewState["BLckeck"] = null;
                //gvBL.PageSize = 7;
                status = true;
                //  TextBoxcmpny.Text = string.Empty;
                // TextBoxcmpny.Enabled = true;

                data = _basePage.CHNLSVC.CommonSearch.GetBLALL(status);
                if (data != null)
                {
                    AddAccualLeadTime(data);
                    AddError(data);
                }
                //   gvBL.PageSize = 5;
                gvBL.DataSource = data;
                gvBL.DataBind();
                ViewState["BLckeck"] = data;

                //tool tip

                string toolTip = "";
                foreach (DataRow row in data.Rows)
                {
                    //  Label Entry = (Label)row.findcon("TextBox6");
                    toolTip = row["IB_SUPP_CD"].ToString();
                }




                _basePage = new Base();
                if (_basePage.CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10120))
                {
                    EnableControls();


                }
                else if (!_basePage.CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10120))
                {
                    disableControls();
                }

            }
            else
            {
                TextBoxcmpny.Enabled = true;

                gvBL.DataSource = new int[] { };
                gvBL.DataBind();

            }



        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void txtLoc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _basePage = new Base();
                var lb = (TextBox)sender;
                var row = (GridViewRow)lb.NamingContainer;
                TextBox txtLoc = (TextBox)row.FindControl("txtLoc");
                MasterLocationNew _mstLoc = _basePage.CHNLSVC.General.GetMasterLocation(new MasterLocationNew() { Ml_loc_cd = txtLoc.Text.ToUpper(), Ml_com_cd = Session["UserCompanyCode"].ToString(), Ml_act = 1 });
                if (_mstLoc == null)
                {
                    displaynotification("Please enter a valid location code"); txtLoc.Text = ""; txtLoc.ToolTip = ""; txtLoc.Focus(); return;
                }
                else
                {
                    txtLoc.ToolTip = _mstLoc.Ml_loc_desc;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        protected void lbtnPopUpSearch_Click(object sender, EventArgs e)
        {
            lblErr.Visible = false;
            txtItemCode.Text = "";
            txtModel.Text = "";
            txtBrand.Text = "";
            txtMainCategory.Text = "";
            txtSubCategory.Text = "";
            txtItemRange.Text = "";
            lbtnClearItemData_Click(null, null);
            popUpSearchModel.Show();
            Session["SearchPopUp"] = "Show";
        }

        public void BindUCtrlDdlSeByKey(DataTable _dataSource)
        {
            this.ddlSeByKey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSeByKey.Items.Add(col.ColumnName);
            }

            this.ddlSeByKey.SelectedIndex = 0;
        }
        protected void lBtnItemCode_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Item";
                _dtSearch = new DataTable();
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                _dtSearch = CHNLSVC.CommonSearch.GetItemSearchData(para, null, null);
                dgvSearch.DataSource = new int[] { };
                if (_dtSearch.Rows.Count > 0)
                {
                    dgvSearch.DataSource = _dtSearch;
                    BindUCtrlDdlSeByKey(_dtSearch);
                }
                else
                {
                    dgvSearch.DataSource = new int[] { };
                }
                dgvSearch.DataBind();
                txtSearch.Text = "";
                txtSearch.Focus();
                ItemPopup.Show();
                if (dgvSearch.PageIndex > 0)
                { dgvSearch.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        protected void lBtnModel_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Model";
                _dtSearch = new DataTable();
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                _dtSearch = CHNLSVC.CommonSearch.GetAllModels(para, null, null);
                dgvSearch.DataSource = new int[] { };
                if (_dtSearch.Rows.Count > 0)
                {
                    dgvSearch.DataSource = _dtSearch;
                    BindUCtrlDdlSeByKey(_dtSearch);
                }
                else
                {
                    dgvSearch.DataSource = new int[] { };
                }
                dgvSearch.DataBind();
                txtSearch.Text = "";
                txtSearch.Focus();
                ItemPopup.Show();
                if (dgvSearch.PageIndex > 0)
                { dgvSearch.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        protected void lBtnBrand_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Brand";
                _dtSearch = new DataTable();
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                _dtSearch = CHNLSVC.CommonSearch.GetItemBrands(para, null, null);
                dgvSearch.DataSource = new int[] { };
                if (_dtSearch.Rows.Count > 0)
                {
                    dgvSearch.DataSource = _dtSearch;
                    BindUCtrlDdlSeByKey(_dtSearch);
                }
                else
                {
                    dgvSearch.DataSource = new int[] { };
                }
                dgvSearch.DataBind();
                txtSearch.Text = "";
                txtSearch.Focus();
                ItemPopup.Show();
                if (dgvSearch.PageIndex > 0)
                { dgvSearch.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        protected void lBtnMainCategory_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CAT_Main";
                _dtSearch = new DataTable();
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                _dtSearch = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                dgvSearch.DataSource = new int[] { };
                if (_dtSearch.Rows.Count > 0)
                {
                    dgvSearch.DataSource = _dtSearch;
                    BindUCtrlDdlSeByKey(_dtSearch);
                }
                else
                {
                    dgvSearch.DataSource = new int[] { };
                }
                dgvSearch.DataBind();
                txtSearch.Text = "";
                txtSearch.Focus();
                ItemPopup.Show();
                if (dgvSearch.PageIndex > 0)
                { dgvSearch.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void lBtnSubCategory_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CAT_Sub1";
                _dtSearch = new DataTable();
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                _dtSearch = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                dgvSearch.DataSource = new int[] { };
                if (_dtSearch.Rows.Count > 0)
                {
                    dgvSearch.DataSource = _dtSearch;
                    BindUCtrlDdlSeByKey(_dtSearch);
                }
                else
                {
                    dgvSearch.DataSource = new int[] { };
                }
                dgvSearch.DataBind();
                txtSearch.Text = "";
                txtSearch.Focus();
                ItemPopup.Show();
                if (dgvSearch.PageIndex > 0)
                { dgvSearch.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        protected void lBtnItemRange_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CAT_Sub2";
                _dtSearch = new DataTable();
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                _dtSearch = CHNLSVC.CommonSearch.GetCat_SearchData(para, null, null);
                dgvSearch.DataSource = new int[] { };
                if (_dtSearch.Rows.Count > 0)
                {
                    dgvSearch.DataSource = _dtSearch;
                    BindUCtrlDdlSeByKey(_dtSearch);
                }
                else
                {
                    dgvSearch.DataSource = new int[] { };
                }
                dgvSearch.DataBind();
                txtSearch.Text = "";
                txtSearch.Focus();
                ItemPopup.Show();
                if (dgvSearch.PageIndex > 0)
                { dgvSearch.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtItemCode.Text.ToUpper().Trim() != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(para, "Item", txtItemCode.Text.ToUpper().Trim());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Item"].ToString()))
                        {
                            if (txtItemCode.Text.ToUpper().Trim() == row["Item"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    txtItemCode.ToolTip = b2 ? toolTip : "";
                    lblItem.Text = b2 ? toolTip : "";
                    if (!b2)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid item code !!!')", true);
                        txtItemCode.Text = "";
                        txtItemCode.Focus();
                        return;
                    }
                }
                else
                {
                    txtItemCode.ToolTip = "";
                    txtModel.Focus();
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtModel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtModel.Text.ToUpper().Trim() != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                    DataTable _result = CHNLSVC.CommonSearch.GetAllModels(para, "CODE", txtModel.Text.ToUpper().Trim());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtModel.Text.ToUpper().Trim() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    txtModel.ToolTip = b2 ? toolTip : "";
                    lblModel.Text = b2 ? toolTip : "";
                    if (!b2)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid model !!!')", true);
                        txtModel.Text = "";
                        txtModel.Focus();
                        return;
                    }
                }
                else
                {
                    txtModel.ToolTip = "";
                    txtBrand.Focus();
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtBrand_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBrand.Text.ToUpper().Trim() != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(para, "CODE", txtBrand.Text.ToUpper().Trim());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtBrand.Text.ToUpper().Trim() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    txtBrand.ToolTip = b2 ? toolTip : "";
                    lblBrand.Text = b2 ? toolTip : "";
                    if (!b2)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid brand !!!')", true);
                        txtBrand.Text = "";
                        txtBrand.Focus();
                        return;
                    }
                }
                else
                {
                    txtBrand.ToolTip = "";
                    txtBrand.Focus();
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtMainCategory_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMainCategory.Text.ToUpper().Trim() != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, "CODE", txtMainCategory.Text.ToUpper().Trim());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtMainCategory.Text.ToUpper().Trim() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    txtMainCategory.ToolTip = b2 ? toolTip : "";
                    lblMainCategory.Text = b2 ? toolTip : "";
                    if (!b2)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid category 1 !!!')", true);
                        txtMainCategory.Text = "";
                        txtMainCategory.Focus();
                        return;
                    }
                }
                else
                {
                    txtMainCategory.ToolTip = "";
                    txtSubCategory.Focus();
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtSubCategory_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSubCategory.Text.ToUpper().Trim() != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, "CODE", txtSubCategory.Text.ToUpper().Trim());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtSubCategory.Text.ToUpper().Trim() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    txtSubCategory.ToolTip = b2 ? toolTip : "";
                    lblSubCategory.Text = b2 ? toolTip : "";
                    if (!b2)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid category 2 !!!')", true);
                        txtSubCategory.Text = "";
                        txtSubCategory.Focus();
                        return;
                    }
                }
                else
                {
                    txtSubCategory.ToolTip = "";
                    txtItemRange.Focus();
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtItemRange_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtItemRange.Text.ToUpper().Trim() != "")
                {
                    bool b2 = false;
                    string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(para, "CODE", txtItemRange.Text.ToUpper().Trim());
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Code"].ToString()))
                        {
                            if (txtItemRange.Text.ToUpper().Trim() == row["Code"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    txtItemRange.ToolTip = b2 ? toolTip : "";
                    lblItemRange.Text = b2 ? toolTip : "";
                    if (!b2)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid category 3 !!!')", true);
                        txtItemRange.Text = "";
                        txtItemRange.Focus();
                        return;
                    }
                }
                else
                {
                    txtItemRange.ToolTip = "";
                    lbtnSearchByCat.Focus();
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtPopDocClose_Click(object sender, EventArgs e)
        {
            popUpSearchModel.Hide();
            Session["SearchPopUp"] = "Hide";
            if (Session["CusdecUpdate"].ToString() == "Show")
            {
                ModalPopupExtenderUpdateCusdec.Show();
                Session["CusdecUpdate"] = "Show";

            }
        }
        protected void lbtCusDecClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderUpdateCusdec.Hide();
            Session["CusdecUpdate"] = "Hide";
        }

        protected void lbtnSearchPopUp_Click(object sender, EventArgs e)
        {
            _dtSearch = new DataTable();
            string para = "";
            if (lblSearchType.Text == "Item")
            {
                para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                _dtSearch = CHNLSVC.CommonSearch.GetItemSearchData(para, ddlSeByKey.SelectedValue, "%" + txtSearch.Text);
            }
            else if (lblSearchType.Text == "Model")
            {
                para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                _dtSearch = CHNLSVC.CommonSearch.GetAllModels(para, ddlSeByKey.SelectedValue, "%" + txtSearch.Text);
            }
            else if (lblSearchType.Text == "Brand")
            {
                para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                _dtSearch = CHNLSVC.CommonSearch.GetItemBrands(para, ddlSeByKey.SelectedValue, "%" + txtSearch.Text);
            }
            else if (lblSearchType.Text == "CAT_Main")
            {
                para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                _dtSearch = CHNLSVC.CommonSearch.GetCat_SearchData(para, ddlSeByKey.SelectedValue, "%" + txtSearch.Text);
            }
            else if (lblSearchType.Text == "CAT_Sub1")
            {
                para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                _dtSearch = CHNLSVC.CommonSearch.GetCat_SearchData(para, ddlSeByKey.SelectedValue, "%" + txtSearch.Text);
            }
            else if (lblSearchType.Text == "CAT_Sub2")
            {
                para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                _dtSearch = CHNLSVC.CommonSearch.GetCat_SearchData(para, ddlSeByKey.SelectedValue, "%" + txtSearch.Text);
            }
            else if (lblSearchType.Text == "CusdecModel")
            {
                string SearchParams = SetCommonSearchInitialParametersCusdec(1);
                _dtSearch = CHNLSVC.CommonSearch.Search_Bond_Itm_Model(SearchParams, ddlSeByKey.SelectedValue, "%" + txtSearch.Text);
            }
            else if (lblSearchType.Text == "CusdecEmplyee")
            {
                string SearchParams = SetCommonSearchInitialParametersCusdec(1);
                _dtSearch = CHNLSVC.CommonSearch.Search_Wharf_Employee(SearchParams, ddlSeByKey.SelectedValue, "%" + txtSearch.Text);
            }
            else
            {
                _dtSearch = new DataTable();
            }
            dgvSearch.DataSource = new int[] { };
            if (_dtSearch.Rows.Count > 0)
            {
                dgvSearch.DataSource = _dtSearch;
                BindUCtrlDdlSeByKey(_dtSearch);
            }
            dgvSearch.DataBind();
            txtSearch.Text = "";
            txtSearch.Focus();
            ItemPopup.Show();
            if (dgvSearch.PageIndex > 0)
            { dgvSearch.SetPageIndex(0); }
        }

        protected void dgvSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvSearch.PageIndex = e.NewPageIndex;
            dgvSearch.DataSource = new int[] { };
            if (_dtSearch != null)
            {
                if (_dtSearch.Rows.Count > 0)
                {
                    dgvSearch.DataSource = _dtSearch;
                }
            }
            dgvSearch.DataBind();
            txtSearch.Text = string.Empty;
            txtSearch.Focus();
            ItemPopup.Show();
        }

        protected void dgvSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string code = dgvSearch.SelectedRow.Cells[1].Text;
                // string desc = dgvSearch.SelectedRow.Cells[2].Text;

                if (lblSearchType.Text == "CusdecEmplyee")
                {
                    TextBoxCleardBy.Text = code;
                    ModalPopupExtenderUpdateCusdec.Show();
                    Session["CusdecUpdate"] = "Show";

                }
                if (lblSearchType.Text == "CusdecModel")
                {
                    TextBoxCusdecModal.Text = code;
                    ModalPopupExtenderUpdateCusdec.Show();
                    Session["CusdecUpdate"] = "Show";

                }
                if (lblSearchType.Text == "CusdecEmplyee")
                {
                    TextBoxCleardBy.Text = code;
                    ModalPopupExtenderUpdateCusdec.Show();
                    Session["CusdecUpdate"] = "Show";
                }
                if (lblSearchType.Text == "Item")
                {
                    txtItemCode.Text = code;
                    txtItemCode_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "Model")
                {
                    txtModel.Text = code;
                    txtModel_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "Brand")
                {
                    txtBrand.Text = code;
                    txtBrand_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "CAT_Main")
                {
                    txtMainCategory.Text = code;
                    txtMainCategory_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "CAT_Sub1")
                {
                    txtSubCategory.Text = code;
                    txtSubCategory_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "CAT_Sub2")
                {
                    txtItemRange.Text = code;
                    txtItemRange_TextChanged(null, null);
                }
                ModalPopupExtenderUpdateCusdec.Show();
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void lbtnSearchByCat_Click(object sender, EventArgs e)
        {
            string _itm = string.IsNullOrEmpty(txtItemCode.Text) ? null : txtItemCode.Text.Trim();
            string _model = string.IsNullOrEmpty(txtModel.Text) ? null : txtModel.Text.Trim();
            string _brand = string.IsNullOrEmpty(txtBrand.Text) ? null : txtBrand.Text.Trim();
            string _mainCat = string.IsNullOrEmpty(txtMainCategory.Text) ? null : txtMainCategory.Text.Trim();
            string _subCat1 = string.IsNullOrEmpty(txtSubCategory.Text) ? null : txtSubCategory.Text.Trim();
            string _subCat2 = string.IsNullOrEmpty(txtItemRange.Text) ? null : txtItemRange.Text.Trim();
            string msg = "";
            ImportsBLItems _impBlItm = new ImportsBLItems()
            {
                Ibi_itm_cd = _itm
            };
            MasterItem _item = new MasterItem()
            {
                Mi_model = _model,
                Mi_brand = _brand,
                Mi_cate_1 = _mainCat,
                Mi_cate_2 = _subCat1,
                Mi_cate_3 = _subCat2
            };

            if (_itm == null && _model == null && _brand == null && _mainCat == null && _subCat1 == null && _subCat2 == null)
            {
                msg = "Please enter search details !!!";
                lblErr.Text = msg;
                lblErr.Visible = true;
                popUpSearchModel.Show();
                //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "')", true);
            }
            else
            {
                lblErr.Visible = false;
                DataTable _dt = CHNLSVC.Inventory.GetBLDetailsByModel(_impBlItm, _item);
                if (_dt != null)
                {
                    AddAccualLeadTime(_dt);
                    AddError(_dt);
                    gvBL.DataSource = _dt;
                    gvBL.DataBind();
                    ViewState["gvBlDetails"] = _dt;
                    popUpSearchModel.Hide();
                    Session["SearchPopUp"] = "Hide";
                }
            }

        }

        protected void lbtnClearItemData_Click(object sender, EventArgs e)
        {
            txtItemCode.Text = "";
            lblItem.Text = "";
            txtBrand.Text = "";
            lblBrand.Text = "";
            txtModel.Text = "";
            lblModel.Text = "";
            txtMainCategory.Text = "";
            lblMainCategory.Text = "";
            txtSubCategory.Text = "";
            lblSubCategory.Text = "";
            txtItemRange.Text = "";
            lblItemRange.Text = "";
        }
        //Dulaj 2018/Dec/06
        protected void txtcleardBy_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParametersCusdec(1);
                _dtSearch = CHNLSVC.CommonSearch.Search_Wharf_Employee(SearchParams, "CODE", TextBoxCleardBy.Text);
                if (_dtSearch.Rows.Count < 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('Cleard User is not valid!');", true);
                    TextBoxCleardBy.Text = "";
                }
                ModalPopupExtenderUpdateCusdec.Show();
                Session["CusdecUpdate"] = "Show";
            }
            catch (Exception ee)
            {
                ModalPopupExtenderUpdateCusdec.Show();
                Session["CusdecUpdate"] = "Show";
            }
        }
        protected void txtmodal_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParametersCusdec(1);
            _dtSearch = CHNLSVC.CommonSearch.Search_Bond_Itm_Model(SearchParams, "MODEL", TextBoxCusdecModal.Text);
            if (_dtSearch.Rows.Count < 1)
            {
                ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('Model is not valid!');", true);
                TextBoxCusdecModal.Text = "";
            }
            ModalPopupExtenderUpdateCusdec.Show();
            Session["CusdecUpdate"] = "Show";
        }
        protected void txtCif_TextChanged(object sender, EventArgs e)
        {
            ModalPopupExtenderUpdateCusdec.Show();
            Session["CusdecUpdate"] = "Show";
        }
        protected void txtentryNo_TextChanged(object sender, EventArgs e)
        {
            ModalPopupExtenderUpdateCusdec.Show();
            Session["CusdecUpdate"] = "Show";

        }
        protected void txtrmk_TextChanged(object sender, EventArgs e)
        {
            ModalPopupExtenderUpdateCusdec.Show();
            Session["CusdecUpdate"] = "Show";

        }

        protected void lbtnSearchCleard_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CusdecEmplyee";
                _dtSearch = new DataTable();
                string SearchParams = SetCommonSearchInitialParametersCusdec(1);
                _dtSearch = CHNLSVC.CommonSearch.Search_Wharf_Employee(SearchParams, null, null);
                dgvSearch.DataSource = new int[] { };
                if (_dtSearch.Rows.Count > 0)
                {
                    dgvSearch.DataSource = _dtSearch;
                    BindUCtrlDdlSeByKey(_dtSearch);
                }
                else
                {
                    dgvSearch.DataSource = new int[] { };
                }
                dgvSearch.DataBind();
                txtSearch.Text = "";
                txtSearch.Focus();
                ItemPopup.Show();
                if (dgvSearch.PageIndex > 0)
                { dgvSearch.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }



        }
        protected void lbtnSearchmodal_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "CusdecModel";
                _dtSearch = new DataTable();
                string SearchParams = SetCommonSearchInitialParametersCusdec(1);
                _dtSearch = CHNLSVC.CommonSearch.Search_Bond_Itm_Model(SearchParams, null, null);
                dgvSearch.DataSource = new int[] { };
                if (_dtSearch.Rows.Count > 0)
                {
                    dgvSearch.DataSource = _dtSearch;
                    BindUCtrlDdlSeByKey(_dtSearch);
                }
                else
                {
                    dgvSearch.DataSource = new int[] { };
                }
                dgvSearch.DataBind();
                txtSearch.Text = "";
                txtSearch.Focus();
                ItemPopup.Show();
                if (dgvSearch.PageIndex > 0)
                { dgvSearch.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }

        }
        protected void lbtnupdateCusdec_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16133))
            {
                string msg = "Sorry, You have no permission to update this! (Advice: Required permission code :16133)";
                ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "')", true);
                ModalPopupExtenderUpdateCusdec.Show();
                return;
            }

            if (TextBoxCleardBy.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('Cleard User is empty!');", true);
                return;
            }

            if (TextBoxCIF.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('CIF is empty!');", true);
                return;
            }

            //file rec TextBox3,actualTextBox2
            DateTime actualDt = DateTime.MinValue;
            DateTime fileRecdt = DateTime.MinValue;
            if (!string.IsNullOrEmpty(TextBox2.Text))
            {
                actualDt = Convert.ToDateTime(TextBox2.Text);
            }
            if (!string.IsNullOrEmpty(TextBox3.Text))
            {
                fileRecdt = Convert.ToDateTime(TextBox3.Text);
            }
            int effect = CHNLSVC.Financial.UPDATE_CUS_DEC_HDR(TextBoxCusDocNo.Text, TextBoxCusdecModal.Text, TextBoxCleardBy.Text, Convert.ToDecimal(TextBoxCIF.Text), TextBoxRemark.Text, Session["UserID"].ToString(), TextBoxEntryNo.Text, actualDt, fileRecdt);
            if (effect > 0)
            {
                displayMessages("Successfully Updated!");
                ModalPopupExtenderUpdateCusdec.Hide();
                Session["CusdecUpdate"] = "Hide";
                clearCusdecPopup();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('Not Updated');", true);
                ModalPopupExtenderUpdateCusdec.Show();
                Session["CusdecUpdate"] = "Show";
            }


        }
        private void clearCusdecPopup()
        {
            TextBoxCusDocNo.Text = "";
            TextBoxCleardBy.Text = "";
            TextBoxCusdecModal.Text = "";
            TextBoxCIF.Text = "";
            TextBoxEntryNo.Text = "";
            TextBoxRemark.Text = "";
        }
        private string SetCommonSearchInitialParametersCusdec(int _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case 1:
                    {
                        paramsText.Append(TextBoxCusDocNo.Text + seperator);
                        break;
                    }
                case 2:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }


                default:
                    break;
            }
            return paramsText.ToString();
        }

        protected void ddlCudecChnaged(object sender, EventArgs e)
        {

        }


        protected void dgvSearch_SelectedIndexChangedCusdec(object sender, EventArgs e)
        {
            string code = "";//GridViewResult.SelectedRow.Cells[1].Text;
            if (lblSearchType.Text == "CusdecModel")
            {
                TextBoxCusdecModal.Text = code;
            }
            if (lblSearchType.Text == "CusdecEmplyee")
            {
                TextBoxCleardBy.Text = code;
                //txtcleardBy_TextChanged(null,null);
            }
            //Page_Load(null,null);
        }
        protected void searchCusdecTextChanged(object sender, EventArgs e)
        {
            if (lblSearchType.Text == "CusdecModel")
            {
                string SearchParams = SetCommonSearchInitialParametersCusdec(1);
                //  _dtSearch = CHNLSVC.CommonSearch.Search_Bond_Itm_Model(SearchParams, cmbSearchbykeyCusdec.SelectedValue.ToString(), "%" + SearchCusdecTextBox.Text);

            }
            else
            {
                string SearchParams = SetCommonSearchInitialParametersCusdec(1);
                //   _dtSearch = CHNLSVC.CommonSearch.Search_Wharf_Employee(SearchParams, cmbSearchbykeyCusdec.SelectedValue.ToString(), "%" + SearchCusdecTextBox.Text);
            }
        }
        protected void CheckBoxatual_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxActual.Checked)
            {
                CheckBoxExcepcted.Checked = false;
                CheckBoxPendeing.Checked = false;
            }
            else
            {
                CheckBoxExcepcted.Checked = true;
            }

        }
        protected void CheckBoxexpec_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxExcepcted.Checked)
            {
                CheckBoxActual.Checked = false;
                CheckBoxExcepcted.Checked = true;
            }          

        }
        protected void CheckBoxpending_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxPendeing.Checked)
            {
                CheckBoxActual.Checked = false;
                CheckBoxExcepcted.Checked = true;
            }
            else
            {
                CheckBoxExcepcted.Checked = false;
                CheckBoxPendeing.Checked = false;
            }

        }

    }
}


