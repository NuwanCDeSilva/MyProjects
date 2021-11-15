using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FF.AbansTours.Enquiry_Module
{
    public partial class Cost_and_Prfitability_Tracker : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                pageClear();

            }
            else
            {

            }
        }
        List<QUO_COST_HDR>  _cost_Hdr_list_Details = new List<QUO_COST_HDR>();
        List<QUO_COST_HDR> _cost_Hdr_list_summery = new List<QUO_COST_HDR>();
        decimal[] yValues;
        string[] xValues;
        decimal[] yValues2;
        string[] xValues2;
        private void pageClear()
        {
            grdsummery.DataSource = new int[] { };
            grdsummery.DataBind();
            txtFromDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            txtToDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            yValues = new decimal[] { };
            xValues = new string[] { };
            yValues2 = new decimal[] { };
            xValues2 = new string[] { };
        }

        protected void lbtnSearchall_Click(object sender, EventArgs e)
        {
            int index = 0;
          
            if (chkDetails.Checked == true)
            {
                _cost_Hdr_list_Details = CHNLSVC.Tours.GET_COST_PRFITABILITY_DETAILS(GlbUserComCode, txtProc.Text, txtReqNo.Text, txtCostsheet.Text, txtcustomer.Text, txtcatergory.Text, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text)); 
                pnlDetails.Visible = true;
                pnlSuumery.Visible = false;
                
                foreach (QUO_COST_HDR _cost in _cost_Hdr_list_Details)
                {
                    decimal GP = _cost.QCH_TOT_VALUE - _cost.QCH_TOT_COST_LOCAL;
                    _cost.QCH_GP = GP;
                    if (_cost.QCH_TOT_COST_LOCAL != 0)
                    {
                        decimal Pre = (_cost.QCH_TOT_COST_LOCAL / _cost.QCH_TOT_VALUE) * 100;
                        string newpre = DoFormat(Pre);
                        _cost.QCH_GP_Pre = newpre + "%";
                    }
                   

                }
                grdDetails.DataSource = _cost_Hdr_list_Details;
                grdDetails.DataBind();
                _cost_Hdr_list_summery = CHNLSVC.Tours.GET_COST_PRFITABILITY(GlbUserComCode, txtProc.Text, txtReqNo.Text, txtCostsheet.Text, txtcustomer.Text, txtcatergory.Text, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
            }
            else if (chkSummery.Checked == true)
            {
                _cost_Hdr_list_summery = CHNLSVC.Tours.GET_COST_PRFITABILITY(GlbUserComCode, txtProc.Text, txtReqNo.Text, txtCostsheet.Text, txtcustomer.Text, txtcatergory.Text, Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text));
                pnlDetails.Visible = false;
                pnlSuumery.Visible = true;
                foreach (QUO_COST_HDR _cost in _cost_Hdr_list_summery)
                {
                    decimal GP = _cost.QCH_TOT_VALUE - _cost.QCH_TOT_COST_LOCAL;
                    _cost.QCH_GP = GP;
                    decimal Pre = (_cost.QCH_TOT_COST_LOCAL / _cost.QCH_TOT_VALUE) * 100;
                    string newpre = DoFormat(Pre);
                    _cost.QCH_GP_Pre = newpre + "%";


                }
                grdsummery.DataSource = _cost_Hdr_list_summery;
                grdsummery.DataBind();
            }
            else
            {
                return;
            }



            //if (_cost_Hdr_list_summery != null)
            //{
            //    xValues = new string[_cost_Hdr_list_summery.Count + 1];
            //    yValues = new decimal[_cost_Hdr_list_summery.Count + 1];
            //    xValues2 = new string[_cost_Hdr_list_summery.Count + 1];
            //    yValues2 = new decimal[_cost_Hdr_list_summery.Count + 1];
            //    foreach (QUO_COST_HDR _cost in _cost_Hdr_list_summery)
            //    {
            //     //   decimal GP = _cost.QCH_TOT_VALUE - _cost.QCH_TOT_COST_LOCAL;
            //      //  _cost.QCH_GP = GP;
            //        decimal Pre = (_cost.QCH_TOT_COST_LOCAL / _cost.QCH_TOT_VALUE) * 100;
            //        string newpre = DoFormat(Pre);
            //       // _cost.QCH_GP_Pre = newpre + "%";

            //        yValues[index] = Convert.ToDecimal(newpre);
            //        xValues[index] = _cost.QCH_SBU;

            //        yValues2[index] = _cost.QCH_TOT_VALUE;
            //        xValues2[index] = _cost.QCH_SBU;
            //        index++;

            //    }
            //}

            decimal totalGP = _cost_Hdr_list_summery.Sum(item => item.QCH_GP);
            decimal totalRevenu = _cost_Hdr_list_summery.Sum(item => item.QCH_TOT_VALUE);
            xValues = new string[_cost_Hdr_list_summery.Count + 1];
            yValues = new decimal[_cost_Hdr_list_summery.Count + 1];
            xValues2 = new string[_cost_Hdr_list_summery.Count + 1];
            yValues2 = new decimal[_cost_Hdr_list_summery.Count + 1];
            foreach (QUO_COST_HDR _cost in _cost_Hdr_list_summery)
            {
                decimal GP = _cost.QCH_TOT_VALUE - _cost.QCH_TOT_COST_LOCAL;
                _cost.QCH_GP = GP;
                decimal Pre = totalGP - _cost.QCH_GP;
                string newpre = DoFormat(Pre);
                // _cost.QCH_GP_Pre = newpre + "%";

                yValues[index] = _cost.QCH_GP;
                xValues[index] = _cost.QCH_SBU;

                decimal _rev = totalRevenu - _cost.QCH_TOT_COST;
                string new_rev = DoFormat(_rev);
                yValues2[index] = _cost.QCH_TOT_VALUE;
                xValues2[index] = _cost.QCH_SBU;
                index++;
            }

            chart();
            chart2();
          


           
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


        private void chart()
        {
            if (xValues != null)
            {

               // Image1.Visible = false;
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
      
            }
        }

        private void chart2()
        {
            if (xValues != null)
            {

                // Image1.Visible = false;
                Chart2.Visible = true;
                System.Web.UI.DataVisualization.Charting.Series series = new System.Web.UI.DataVisualization.Charting.Series("Pie");
                //  yValues = new double[] { 65.62, 75.54, 60.45, 34.73, 85.42 };
                // string[] xValues = { "France", "Canada", "Germany", "USA", "Italy" };
                Chart2.Series["Default"].Points.DataBindXY(xValues2, yValues2);

                // Set Pie chart type
                Chart2.Series["Default"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;

                Chart2.Series["Default"]["PointWidth"] = "0.5";// Show data points labels
                Chart2.Series["Default"]["PieLabelStyle"] = "Outside";
                Chart2.Series["Default"].IsValueShownAsLabel = true;// Set data points label style

                Chart2.Series["Default"]["BarLabelStyle"] = "Center";// Show chart as 3D
                // Chart1.Series["Default"]["PieStartAngle"] = "90";

                Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;// Draw chart as 3D

                Chart2.Series["Default"]["DrawingStyle"] = "Cylinder";

            }
        }

        #region Modalpopup
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string ID = grdResult.SelectedRow.Cells[1].Text;

            if (lblvalue.Text == "profit center")
            {
                txtProc.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "req")
            {
                txtReqNo.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "customer")
            {
                txtcustomer.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "cost sheet")
            {
                txtCostsheet.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "service code")
            {
                txtcatergory.Text = ID;
                lblvalue.Text = "";
                return;
            }
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "profit center")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "req")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EnquirySearch);
                DataTable result = CHNLSVC.CommonSearch.SEARCH_ENQUIRY(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "customer")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                DataTable result = CHNLSVC.CommonSearch.GetCustomerCommon(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "cost sheet")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EnquirySearch);
                DataTable result = CHNLSVC.Tours.Get_COST_HDR_NO(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "service code")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EnquirySearch);
                DataTable result = CHNLSVC.Tours.Get_SERVICE_CODE(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopoup.Show();
            }
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "profit center")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "req")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EnquirySearch);
                DataTable result = CHNLSVC.CommonSearch.SEARCH_ENQUIRY(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "customer")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                DataTable result = CHNLSVC.CommonSearch.GetCustomerCommon(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "cost sheet")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EnquirySearch);
                DataTable result = CHNLSVC.Tours.Get_COST_HDR_NO(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "service code")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EnquirySearch);
                DataTable result = CHNLSVC.Tours.Get_SERVICE_CODE(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopoup.Show();
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {

            if (lblvalue.Text == "profit center")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "req")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EnquirySearch);
                DataTable result = CHNLSVC.CommonSearch.SEARCH_ENQUIRY(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "customer")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                DataTable result = CHNLSVC.CommonSearch.GetCustomerCommon(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "cost sheet")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EnquirySearch);
                DataTable result = CHNLSVC.Tours.Get_COST_HDR_NO(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "service code")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EnquirySearch);
                DataTable result = CHNLSVC.Tours.Get_SERVICE_CODE(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopoup.Show();
            }
        }
        #endregion
        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
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
                        paramsText.Append(GlbUserName + seperator + GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(GlbUserName + seperator + GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EnquirySearch:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        protected void imgbtnProNo_Click(object sender, ImageClickEventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
            DataTable result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            BindUCtrlDDLData(result);
            lblvalue.Text = "profit center";
            UserPopoup.Show();
        }

        protected void imgbtnReqno_Click(object sender, ImageClickEventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EnquirySearch);
            DataTable result = CHNLSVC.CommonSearch.SEARCH_ENQUIRY(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            BindUCtrlDDLData(result);
            lblvalue.Text = "req";
            UserPopoup.Show();
        }

        protected void imgbtncustomer_Click(object sender, ImageClickEventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
            DataTable result = CHNLSVC.CommonSearch.GetCustomerCommon(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            BindUCtrlDDLData(result);
            lblvalue.Text = "customer";
            UserPopoup.Show();
        }

        protected void imgbtnCostS_Click(object sender, ImageClickEventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EnquirySearch);
            DataTable result = CHNLSVC.Tours.Get_COST_HDR_NO(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            BindUCtrlDDLData(result);
            lblvalue.Text = "cost sheet";
            UserPopoup.Show();
        }

        protected void imgbtncatergory_Click(object sender, ImageClickEventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EnquirySearch);
            DataTable result = CHNLSVC.Tours.Get_SERVICE_CODE(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            BindUCtrlDDLData(result);
            lblvalue.Text = "service code";
            UserPopoup.Show();
        }
    }
}