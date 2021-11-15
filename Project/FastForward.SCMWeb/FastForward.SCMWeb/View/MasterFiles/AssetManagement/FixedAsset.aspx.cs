using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.MasterFiles.AssetManagement
{
    public partial class FixedAsset : BasePage
    {
        DataTable _result;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Load += FixedAsset_Load;
                ddlReuestType.Items.Clear();
                ddlReuestType.Items.Add("FIXED ASSETS");
                ddlReuestType.Items.Add("FGAP");

                ddlAction.Items.Clear();
                ddlAction.Items.Add("New Request");
                ddlAction.Items.Add("Approve Request");
                ddlAction.Items.Add("Confirmation");
                sendlocation.Text = "";

            }
        }



            //if (!IsPostBack)
            //{
            //    sendlocation.Text = "";
            //}

        

        void FixedAsset_Load(object sender, EventArgs e)
        {




        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnView_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnClear_Click1(object sender, EventArgs e)
        {

        }

        protected void lbtnrefno_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlAction.SelectedValue.ToString() == "New Request")
                {
                    return;
                }


                ViewState["FixAssetRefNo"] = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.FixAssetRefNo);
                DataTable _result = CHNLSVC.CommonSearch.GET_FixAsset_ref(SearchParams, null, null);
                ViewState["FixAssetRefNo"] = _result;
                dvResult.DataSource = _result;
                dvResult.DataBind();
                BindUCtrlDDLData(_result);
                Label8.Text = "39";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                UserPopoup.Show();
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

        protected void lbtnSeDocType_Click(object sender, EventArgs e)
        {
            ViewState["UserLocation"] = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
            ViewState["UserLocation"] = _result;
            dvResult.DataSource = _result;
            dvResult.DataBind();
            BindUCtrlDDLData(_result);
            Label8.Text = "38";
            txtSearchbyword.Text = "";
            txtSearchbyword.Focus();
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

                case CommonUIDefiniton.SearchUserControlType.FixAssetRefNo:
                    {
                        //( p_com in NVARCHAR2,p_loc in NVARCHAR2,p_refNo in NVARCHAR2,p_type in NUMBER,p_status in NUMBER,c_data OUT sys_refcursor)
                        string Loc = sendlocation.Text.Trim().ToUpper();

                        int req = (int)(ddlReuestType.SelectedIndex);
                        Int32 type = req.ToString() == "2" ? 2 : 1;
                        if (type == 1)
                        {
                            Loc = Session["UserDefLoca"].ToString();
                        }
                        // Int32 status = rdoPending.Checked == true ? 0 : 1;
                        string act = (ddlAction.SelectedItem.ToString());
                        Int32 status = act.ToString() == "Approve Request" ? 1 : act.ToString() == "Confirmation" ? 3 : -1;
                        // Int32 status = ddlAction.SelectedValue == "Approve" ? 0 : 1;
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Loc + seperator + type + seperator + status);
                        break;
                    }
                default:
                    break;

            }

            return paramsText.ToString();

        }

        protected void ImgSearch_Click(object sender, EventArgs e)
        {
            _result = null;
            if (Label8.Text == "38")
            {
                //_result = null;
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company);
                //_result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text.ToString());
                //dvResult.DataSource = _result;
                //BindUCtrlDDLData(_result);
                //dvResult.DataBind();
                //txtSearchbyword.Text = "";
                //txtSearchbyword.Focus();
                //UserPopoup.Show();
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result2 = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text.ToString());
                dvResult.DataSource = _result2;
                dvResult.DataBind();
                BindUCtrlDDLData(_result2);
                Label8.Text = "38";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                UserPopoup.Show();

            }
            //else if (Label8.Text == "36")
            //{

            //    _result = null;
            //    if (IsDisplayRawData)
            //    {
            //        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
            //        IsRawData = true;
            //        _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
            //    }
            //    else
            //    {
            //        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
            //        IsRawData = false;
            //        _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
            //    }
            //    dvResult.DataSource = null;
            //    dvResult.DataSource = _result;
            //    dvResult.DataBind();
            //    txtSearchbyword.Text = "";
            //    txtSearchbyword.Focus();
            //    UserPopoup.Show();
            //}

            //else if (Label8.Text == "37")
            //{

            //    _result = null;

            //    if (IsDisplayRawData)
            //    {
            //        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
            //        IsRawData = true;
            //        _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
            //    }
            //    else
            //    {
            //        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
            //        IsRawData = false;
            //        //Add by Lakshan as per the Asanka 08 Nov 2016
            //        if (SearchParams.Contains(TextBox3.Text))
            //        {
            //            SearchParams = SearchParams.Replace(TextBox3.Text, "");
            //        }
            //        _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
            //    }
            //    dvResult.DataSource = null;
            //    dvResult.DataSource = _result;
            //    dvResult.DataBind();
            //    txtSearchbyword.Text = "";
            //    txtSearchbyword.Focus();
            //    UserPopoup.Show();


            //}

            if (Label8.Text == "UserLocation")
            {
                _result = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text.ToString());
                dvResult.DataSource = _result;
                BindUCtrlDDLData(_result);
                dvResult.DataBind();
                ViewState["UserLocation"] = _result;
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                UserPopoup.Show();

            }
            if (Label8.Text == "MainUserLocation")
            {
                _result = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text.ToString());
                dvResult.DataSource = _result;
                BindUCtrlDDLData(_result);
                dvResult.DataBind();
                ViewState["UserLocation"] = _result;
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                UserPopoup.Show();

            }
            if (Label8.Text == "70")
            {
                _result = null;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OPE);
                _result = CHNLSVC.CommonSearch.SearchOperation(SearchParams, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text.ToString());
                dvResult.DataSource = _result;
                BindUCtrlDDLData(_result);
                dvResult.DataBind();
                ViewState["opcd"] = _result;
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                UserPopoup.Show();

            }
            if (Label8.Text == "Channel")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Channel);
                DataTable _dt = CHNLSVC.CommonSearch.SearchMstChnl(SearchParams, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text.ToString());
                dvResult.DataSource = _dt;
                //BindUCtrlDDLData(_dt);
                dvResult.DataBind();
                ViewState["Channel"] = _dt;
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                UserPopoup.Show();

            }

            if (Label8.Text == "CAT_Sub3")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub3);
                DataTable _dt = CHNLSVC.CommonSearch.SearchRefLocCate3(SearchParams, cmbSearchbykey.SelectedValue, "%" + txtSearchbyword.Text.ToString());
                dvResult.DataSource = _dt;
                //  BindUCtrlDDLData(_dt);
                dvResult.DataBind();
                ViewState["CAT_Sub3"] = _dt;
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
                UserPopoup.Show();

            }

        }

        protected void dvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Label8.Text == "38")
            {
                sendlocation.Text = dvResult.SelectedRow.Cells[1].Text;
                sendlocation_TextChanged(null, null);
            }
            //else if (Label8.Text == "36")
            //{
            //    TextBox3.Text = dvResult.SelectedRow.Cells[1].Text;
            //    TextBox3_TextChanged(null, null);

            //}
            //else if (Label8.Text == "37")
            //{
            //    TextBox4.Text = dvResult.SelectedRow.Cells[1].Text;
            //    TextBox4_TextChanged(null, null);
            //}

            //else if (Label8.Text == "70")
            //{
            //    TextBox2.Text = dvResult.SelectedRow.Cells[1].Text;
            //    TextBox2_TextChanged(null, null);
            //}
            //else if (Label8.Text == "UserLocation")
            //{
            //    TextBox5.Text = dvResult.SelectedRow.Cells[1].Text;
            //    TextBox5_TextChanged(null, null);
            //}
            //else if (Label8.Text == "MainUserLocation")
            //{
            //    TextBox9.Text = dvResult.SelectedRow.Cells[1].Text;
            //    TextBox9_TextChanged(null, null);
            //}
            //else if (Label8.Text == "Channel")
            //{
            //    txtChannelCat.Text = dvResult.SelectedRow.Cells[1].Text;
            //    txtChannelCat_TextChanged(null, null);
            //}
            //else if (Label8.Text == "CAT_Sub3")
            //{
            //    txtOtherCat.Text = dvResult.SelectedRow.Cells[1].Text;
            //    txtOtherCat_TextChanged(null, null);
            //}
        }
        protected void dvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvResult.PageIndex = e.NewPageIndex;
            dvResult.DataSource = null;
            if (Label8.Text == "38")
            {
                dvResult.DataSource = (DataTable)ViewState["UserLocation"];
                dvResult.DataBind();
                UserPopoup.Show();
            }
        }

        protected void sendlocation_TextChanged(object sender, EventArgs e)
        {

            //if (string.IsNullOrEmpty(sendlocation.Text))
            //{
            //    displayMessage_warning("Please select a company"); return;
            //}
            //if (!validateinputString(sendlocation.Text))
            //{
            //    displayMessage_warning("Invalid charactor found in company.");
            //    sendlocation.Focus();
            //    return;
            //}

            //if (!string.IsNullOrEmpty(sendlocation.Text))
            //{
            //    bool b = false;
            //    string desc = "";
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            //    DataTable _result = new DataTable();
            //    _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "CODE", sendlocation.Text);
            //    foreach (DataRow row in _result.Rows)
            //    {
            //        if (!string.IsNullOrEmpty(row["Code"].ToString()))
            //        {
            //            if (sendlocation.Text == row["Code"].ToString())
            //            {
            //                desc = row["Description"].ToString();
            //                b = true; break;
            //            }
            //        }
            //    }
            //    if (b)
            //    {
            //        sendlocation.Text = desc;
            //    }
            //    else
            //    {
            //        sendlocation.Text = "";
            //        sendlocation.Focus();
            //        displayMessage_warning("Please select a valid company"); return;
            //    }
            //}
        }

        private void displayMessage_warning(string msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);

        }

        public bool validateinputString(string input)
        {
            Match match = Regex.Match(input, @"([~!@#$%^&*]+)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return false;
            }
            return true;
        }
    }
}