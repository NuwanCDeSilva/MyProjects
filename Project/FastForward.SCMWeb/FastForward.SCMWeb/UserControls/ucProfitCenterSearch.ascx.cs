using FastForward.SCMWeb.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.UserControls
{
    public partial class ucProfitCenterSearch : System.Web.UI.UserControl
    {
        private bool _isDisplayRawData = false;
        Base _basePage;
        #region Properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Company
        {
            get { return txtCompany.Text.ToUpper(); }
            set { txtCompany.Text = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CompanyDes
        {
            get { return txtCompanyDes.Text.ToUpper(); }
            set { txtCompanyDes.Text = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Channel
        {
            get { return txtChannel.Text.ToUpper(); }
            set { txtChannel.Text = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SubChannel
        {
            get { return txtSubChannel.Text.ToUpper(); }
            set { txtSubChannel.Text = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Area
        {
            get { return txtArea.Text.ToUpper(); }
            set { txtArea.Text = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Regien
        {
            get { return txtRegion.Text.ToUpper(); }
            set { txtRegion.Text = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Zone
        {
            get { return txtZone.Text.ToUpper(); }
            set { txtZone.Text = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ProfitCenter
        {
            get { return txtProfitCenter.Text.ToUpper(); }
            set { txtProfitCenter.Text = value; }
        }


        //Lakshan 
        public TextBox TxtCompany
        {
            get { return txtCompany; }
            set { txtCompany = value; }
        }
        public LinkButton lbtnCompany
        {
            get { return imgbtnCompany; }
            set { imgbtnCompany = value; }
        }
        public TextBox TxtChanel
        {
            get { return txtChannel; }
            set { txtChannel = value; }
        }
        public LinkButton lbtnChanel
        {
            get { return imgbtnChannel; }
            set { imgbtnChannel = value; }
        }
        public TextBox TxtSubChanel
        {
            get { return txtSubChannel; }
            set { txtSubChannel = value; }
        }
        public LinkButton lbtnSubChanel
        {
            get { return imgbtnSubChannel; }
            set { imgbtnSubChannel = value; }
        }
        public TextBox TxtAreya
        {
            get { return txtArea; }
            set { txtArea = value; }
        }
        public LinkButton lbtnAreya
        {
            get { return imgbtnArea; }
            set { imgbtnArea = value; }
        }
      
        public LinkButton lbtnRegion
        {
            get { return imgbtnRegion; }
            set { imgbtnRegion = value; }
        }
        public TextBox TxtRegion
        {
            get { return txtRegion; }
            set { txtRegion = value; }
        }
        public LinkButton lbtnZone
        {
            get { return imgbtnZone; }
            set { imgbtnZone = value; }
        }
        public TextBox TxtZone
        {
            get { return txtZone; }
            set { txtZone = value; }
        }
        public LinkButton lbtnLocation
        {
            get { return imgbtnProfitCenter; }
            set { imgbtnProfitCenter = value; }
        }
        public TextBox TxtProfCenter
        {
            get { return txtProfitCenter; }
            set { txtProfitCenter = value; }
        }
        public LinkButton lbtnImgSearch
        {
            get { return ImgSearch; }
            set { ImgSearch = value; }
        }
        public TextBox txtSearch
        {
            get { return txtSearchbyword; }
            set { txtSearchbyword = value; }
        }

        public DropDownList cmbSearchby
        {
            get { return cmbSearchbykey; }
            set { cmbSearchbykey = value; }
        }
        //End Lakshan
        //ucPCSE001
        #region DisplayAllPc
        private bool _isAllProfitCenter = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAllProfitCenter
        {
            set { _isAllProfitCenter = value; }
        }
        public event EventHandler TextBoxLostFocus;
        private void ucProfitCenterSearch_TextBoxLostFocus(object sender, EventArgs e)
        {

        }
        #endregion

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Session["Profitsearch"] == null))
            {
                if (IsPostBack)
                {
                    if (Session["ISShow"] == "Y")
                    {
                        UserPopoup.Show();
                        Session["Profitsearch"] = null;
                        dvResult.DataSource = null;
                        Session["DataSource"] = null;
                    }
                    else
                    {
                        UserPopoup.Hide();
                    }
                }
            }
        }
        protected void imgbtnCompany_Click(object sender, EventArgs e)
        {
            try
            {
                errorDiv.Visible = false;
                successDiv.Visible = false;
                //if (txtCompany.Text.Trim() == "")
                //{
                //    //MessageBox.Show("Enter Company Code");
                //    lblWarn.Text = "Enter Company Code.";
                //    errorDiv.Visible = true;
                //    return;
                //}
                _basePage = new Base();

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                dvResult.DataSource = _result;
                Session["DataSource"] = _result;
                dvResult.DataBind();
                Label8.Text = "69";
                BindUCtrlDDLData(_result);
                UserPopoup.Show(); Session["ISShow"] = "Y";
            }
            catch (Exception ex)
            {
                errorDiv.Visible = true;
                lblWarn.Text = ex.Message;
            }
        }
        protected void imgbtnChannel_Click(object sender, EventArgs e)
        {
            errorDiv.Visible = false;
            successDiv.Visible = false;
            try
            {
                if (txtCompany.Text.ToUpper().Trim() == "")
                {
                    lblWarn.Text = "Enter Company Code.";
                    errorDiv.Visible = true;
                    return;
                }
                _basePage = new Base();
                DataTable _result = null;
                if (IsDisplayRawData)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    IsDisplayRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, null, null);
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    IsDisplayRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                }

                dvResult.DataSource = _result;
                Session["DataSource"] = _result;
                dvResult.DataBind();
                Label8.Text = "70";
                BindUCtrlDDLData(_result);
                UserPopoup.Show(); Session["ISShow"] = "Y";
            }
            catch (Exception ex)
            {
                errorDiv.Visible = true;
                lblWarn.Text = ex.Message;
            }
        }
        protected void imgbtnSubChannel_Click(object sender, EventArgs e)
        {
            errorDiv.Visible = false;
            successDiv.Visible = false;
            try
            {
                if (txtCompany.Text.ToUpper().Trim() == "")
                {
                    lblWarn.Text = "Enter Company Code.";
                    errorDiv.Visible = true;
                    return;
                }
                _basePage = new Base();
                DataTable _result = null;
                if (IsDisplayRawData)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    IsDisplayRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, null, null);
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    IsDisplayRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                }

                dvResult.DataSource = _result;
                Session["DataSource"] = _result;
                dvResult.DataBind();
                Label8.Text = "71";
                BindUCtrlDDLData(_result);
                UserPopoup.Show(); Session["ISShow"] = "Y";
            }
            catch (Exception ex)
            {
                errorDiv.Visible = true;
                lblWarn.Text = ex.Message;
            }
        }
        protected void imgbtnArea_Click(object sender, EventArgs e)
        {
            errorDiv.Visible = false;
            successDiv.Visible = false;
            try
            {
                if (txtCompany.Text.ToUpper().Trim() == "")
                {
                    lblWarn.Text = "Enter Company Code.";
                    errorDiv.Visible = true;
                    return;
                }
                _basePage = new Base();
                DataTable _result = null;
                if (IsDisplayRawData)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                    IsDisplayRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, null, null);
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                    IsDisplayRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                }

                dvResult.DataSource = _result;
                Session["DataSource"] = _result;
                dvResult.DataBind();
                Label8.Text = "72";
                BindUCtrlDDLData(_result);
                UserPopoup.Show(); Session["ISShow"] = "Y";
            }
            catch (Exception ex)
            {
                errorDiv.Visible = true;
                lblWarn.Text = ex.Message;
            }
        }
        protected void imgbtnRegion_Click(object sender, EventArgs e)
        {
            errorDiv.Visible = false;
            successDiv.Visible = false;
            try
            {
                if (txtCompany.Text.ToUpper().Trim() == "")
                {
                    lblWarn.Text = "Enter Company Code.";
                    errorDiv.Visible = true;
                    return;
                }
                _basePage = new Base();
                DataTable _result = null;
                if (IsDisplayRawData)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                    IsDisplayRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, null, null);
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                    IsDisplayRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                }

                dvResult.DataSource = _result;
                Session["DataSource"] = _result;
                dvResult.DataBind();
                Label8.Text = "73";
                BindUCtrlDDLData(_result);
                UserPopoup.Show(); Session["ISShow"] = "Y";
            }
            catch (Exception ex)
            {
                errorDiv.Visible = true;
                lblWarn.Text = ex.Message;
            }
        }
        protected void imgbtnZone_Click(object sender, EventArgs e)
        {
            errorDiv.Visible = false;
            successDiv.Visible = false;
            try
            {
                if (txtCompany.Text.ToUpper().Trim() == "")
                {
                    lblWarn.Text = "Enter Company Code.";
                    errorDiv.Visible = true;
                    return;
                }
                _basePage = new Base();
                DataTable _result = null;
                if (IsDisplayRawData)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                    IsDisplayRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, null, null);
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                    IsDisplayRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                }

                dvResult.DataSource = _result;
                Session["DataSource"] = _result;
                dvResult.DataBind();
                Label8.Text = "74";
                BindUCtrlDDLData(_result);
                UserPopoup.Show(); Session["ISShow"] = "Y";
            }
            catch (Exception ex)
            {
                errorDiv.Visible = true;
                lblWarn.Text = ex.Message;
            }
        }
        protected void imgbtnProfitCenter_Click(object sender, EventArgs e)
        {
            errorDiv.Visible = false;
            successDiv.Visible = false;
            try
            {
                if (txtCompany.Text.ToUpper().Trim() == "")
                {
                    lblWarn.Text = "Enter Company Code.";
                    errorDiv.Visible = true;
                    return;
                }
                _basePage = new Base();
                DataTable _result = null;
                if (IsDisplayRawData)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                    IsDisplayRawData = true;
                    _result = _basePage.CHNLSVC.CommonSearch.GetPC_SearchData(SearchParams, null, null);
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                    IsDisplayRawData = false;
                    _result = _basePage.CHNLSVC.CommonSearch.GetPC_SearchData(SearchParams, null, null);
                }

                dvResult.DataSource = _result;
                Session["DataSource"] = _result;
                dvResult.DataBind();
                Label8.Text = "75";
                BindUCtrlDDLData(_result);
                UserPopoup.Show(); Session["ISShow"] = "Y";
            }
            catch (Exception ex)
            {
                errorDiv.Visible = true;
                lblWarn.Text = ex.Message;
            }
        }
        protected void ImgSearch_Click(object sender, EventArgs e)
        {
            errorDiv.Visible = false;
            successDiv.Visible = false;
            try
            {
                _basePage = new Base();
                if (Label8.Text == "69")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    dvResult.DataSource = _result;
                    Session["DataSource"] = _result;
                    dvResult.DataBind();
                    // UserPopoup.Show();

                }
                if (Label8.Text == "70")
                {
                    if (IsDisplayRawData)
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();

                    }
                    else
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();

                    }
                }
                if (Label8.Text == "71")
                {
                    if (IsDisplayRawData)
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();

                    }
                    else
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();

                    }
                }
                if (Label8.Text == "72")
                {
                    if (IsDisplayRawData)
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();

                    }
                    else
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();

                    }
                }
                if (Label8.Text == "73")
                {
                    if (IsDisplayRawData)
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();+

                    }
                    else
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();

                    }
                }
                if (Label8.Text == "74")
                {
                    if (IsDisplayRawData)
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();

                    }
                    else
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();

                    }
                }
                if (Label8.Text == "75")
                {
                    if (IsDisplayRawData)
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();

                    }
                    else
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.GetPC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();

                    }
                }
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                errorDiv.Visible = true;
                lblWarn.Text = ex.Message;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            errorDiv.Visible = false;
            successDiv.Visible = false;
            try
            {
                _basePage = new Base();
                if (Label8.Text == "69")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    dvResult.DataSource = _result;
                    Session["DataSource"] = _result;
                    dvResult.DataBind();
                    // UserPopoup.Show();
                }
                if (Label8.Text == "70")
                {
                    if (IsDisplayRawData)
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();
                    }
                    else
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();
                    }
                }
                if (Label8.Text == "71")
                {
                    if (IsDisplayRawData)
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();
                    }
                    else
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();
                    }
                }
                if (Label8.Text == "72")
                {
                    if (IsDisplayRawData)
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();
                    }
                    else
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();
                    }
                }
                if (Label8.Text == "73")
                {
                    if (IsDisplayRawData)
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();
                    }
                    else
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();
                    }
                }
                if (Label8.Text == "74")
                {
                    if (IsDisplayRawData)
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();
                    }
                    else
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();
                    }
                }
                if (Label8.Text == "75")
                {
                    if (IsDisplayRawData)
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();
                    }
                    else
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.GetPC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();
                    }
                }
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                errorDiv.Visible = true;
                lblWarn.Text = ex.Message;
            }
        }

        protected void txtCompany_TextChanged(object sender, EventArgs e)
        {
            try
            {
                errorDiv.Visible = false;
                successDiv.Visible = false;
                if (txtCompany.Text.ToUpper().Trim() == "")
                {
                    lblWarn.Text = "Enter Company Code.";
                    errorDiv.Visible = true;
                    return;
                }
                if (string.IsNullOrEmpty(txtCompany.Text))
                {
                    txtCompany.Text = string.Empty;
                    txtCompanyDes.Text = string.Empty;
                    return;
                }

                _basePage = new Base();
                if (!_basePage.CHNLSVC.General.CheckCompany(txtCompany.Text.ToUpper().Trim()))
                {
                    lblWarn.Text = "Please check the company.";
                    errorDiv.Visible = true;
                    txtCompany.Text = string.Empty;
                    txtCompanyDes.Text = string.Empty;
                    return;
                }
                txtCompanyDes.Text = Get_pc_HIRC_SearchDesc(69, txtCompany.Text.ToUpper().ToUpper());
                txtCompanyDes.ToolTip = txtCompanyDes.Text;
                txtChannel.Focus();
            }
            catch (Exception ex)
            {
                errorDiv.Visible = true;
                lblWarn.Text = ex.Message;
            }
        }
        protected void txtChannel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                errorDiv.Visible = false;
                successDiv.Visible = false;
                if (txtCompany.Text.ToUpper().Trim() == "")
                {
                    lblWarn.Text = "Enter Company Code.";
                    errorDiv.Visible = true;
                    return;
                }
                if (string.IsNullOrEmpty(txtChannel.Text))
                {
                    txtChannelDes.Text = string.Empty;
                    txtChannelDes.Text = string.Empty;
                    return;
                }

                _basePage = new Base();
                if (!_basePage.CHNLSVC.General.CheckChannel(txtCompany.Text.ToUpper().Trim(), txtChannel.Text.ToUpper().Trim().ToUpper()))
                {
                    lblWarn.Text = "Please check the channel.";
                    errorDiv.Visible = true;
                    txtChannel.Text = string.Empty;
                    txtChannelDes.Text = string.Empty;
                    return;
                }
                txtChannelDes.Text = Get_pc_HIRC_SearchDesc(70, txtChannel.Text.ToUpper().ToUpper());
                txtChannelDes.ToolTip = txtChannelDes.Text;
                txtSubChannel.Focus();
            }
            catch (Exception ex)
            {
                errorDiv.Visible = true;
                lblWarn.Text = ex.Message;
            }
        }
        protected void txtSubChannel_TextChanged(object sender, EventArgs e)
        {
            try
            {
                errorDiv.Visible = false;
                successDiv.Visible = false;
                if (txtCompany.Text.ToUpper().Trim() == "")
                {
                    lblWarn.Text = "Enter Company Code.";
                    errorDiv.Visible = true;
                    return;
                }
                if (string.IsNullOrEmpty(txtSubChannel.Text))
                {
                    txtSubChannel.Text = string.Empty;
                    txtSubChannelDes.Text = string.Empty;
                    return;
                }

                _basePage = new Base();
                if (!_basePage.CHNLSVC.General.CheckSubChannel(txtCompany.Text.ToUpper().Trim(), txtSubChannel.Text.ToUpper().Trim().ToUpper()))
                {
                    lblWarn.Text = "Please check the Subchannel.";
                    errorDiv.Visible = true;
                    txtSubChannel.Text = string.Empty;
                    txtSubChannelDes.Text = string.Empty;
                    return;
                }
                txtSubChannelDes.Text = Get_pc_HIRC_SearchDesc(71, txtSubChannel.Text.ToUpper());
                txtSubChannelDes.ToolTip = txtSubChannelDes.Text;
                txtArea.Focus();
            }
            catch (Exception ex)
            {
                errorDiv.Visible = true;
                lblWarn.Text = ex.Message;
            }
        }
        protected void txtArea_TextChanged(object sender, EventArgs e)
        {
            try
            {
                errorDiv.Visible = false;
                successDiv.Visible = false;
                if (txtCompany.Text.ToUpper().Trim() == "")
                {
                    lblWarn.Text = "Enter Company Code.";
                    errorDiv.Visible = true;
                    return;
                }
                if (string.IsNullOrEmpty(txtArea.Text))
                {
                    txtArea.Text = string.Empty;
                    txtAreaDes.Text = string.Empty;
                    return;
                }

                _basePage = new Base();
                if (!_basePage.CHNLSVC.General.CheckArea(txtCompany.Text.ToUpper().Trim(), txtArea.Text.ToUpper().Trim().ToUpper()))
                {
                    lblWarn.Text = "Please check the area.";
                    errorDiv.Visible = true;
                    txtArea.Text = string.Empty;
                    txtAreaDes.Text = string.Empty;
                    return;
                }
                txtAreaDes.Text = Get_pc_HIRC_SearchDesc(72, txtArea.Text.ToUpper().ToUpper());
                txtAreaDes.ToolTip = txtAreaDes.Text;
                txtRegion.Focus();
            }
            catch (Exception ex)
            {
                errorDiv.Visible = true;
                lblWarn.Text = ex.Message;
            }
        }
        protected void txtRegion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                errorDiv.Visible = false;
                successDiv.Visible = false;
                if (txtCompany.Text.Trim() == "")
                {
                    lblWarn.Text = "Enter Company Code.";
                    errorDiv.Visible = true;
                    return;
                }
                if (string.IsNullOrEmpty(txtRegion.Text))
                {
                    txtRegion.Text = string.Empty;
                    txtRegionDes.Text = string.Empty;
                    return;
                }

                _basePage = new Base();
                if (!_basePage.CHNLSVC.General.CheckRegion(txtCompany.Text.ToUpper().Trim(), txtRegion.Text.ToUpper().Trim().ToUpper()))
                {
                    lblWarn.Text = "Please check the region.";
                    errorDiv.Visible = true;
                    txtRegion.Text = string.Empty;
                    txtRegionDes.Text = string.Empty;
                    return;
                }
                txtRegionDes.Text = Get_pc_HIRC_SearchDesc(73, txtRegion.Text.ToUpper().ToUpper());
                txtRegionDes.ToolTip = txtRegionDes.Text;
                txtZone.Focus();
            }
            catch (Exception ex)
            {
                errorDiv.Visible = true;
                lblWarn.Text = ex.Message;
            }
        }
        protected void txtZone_TextChanged(object sender, EventArgs e)
        {
            try
            {
                errorDiv.Visible = false;
                successDiv.Visible = false;
                if (txtCompany.Text.Trim() == "")
                {
                    lblWarn.Text = "Enter Company Code.";
                    errorDiv.Visible = true;
                    return;
                }
                if (string.IsNullOrEmpty(txtZone.Text))
                {
                    txtZone.Text = string.Empty;
                    txtZoneDes.Text = string.Empty;
                    return;
                }

                _basePage = new Base();
                if (!_basePage.CHNLSVC.General.CheckZone(txtCompany.Text.ToUpper().Trim(), txtZone.Text.ToUpper().Trim().ToUpper()))
                {
                    lblWarn.Text = "Please check the zone.";
                    errorDiv.Visible = true;
                    txtZone.Text = string.Empty;
                    txtZoneDes.Text = string.Empty;
                    return;
                }
                txtZoneDes.Text = Get_pc_HIRC_SearchDesc(74, txtZone.Text.ToUpper().ToUpper());
                txtZoneDes.ToolTip = txtZoneDes.Text;
                txtProfitCenter.Focus();
            }
            catch (Exception ex)
            {
                errorDiv.Visible = true;
                lblWarn.Text = ex.Message;
            }
        }
        protected void txtProfitCenter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                errorDiv.Visible = false;
                successDiv.Visible = false;
                if (txtCompany.Text.Trim() == "")
                {
                    lblWarn.Text = "Enter Company Code.";
                    errorDiv.Visible = true;
                    return;
                }
                if (string.IsNullOrEmpty(txtProfitCenter.Text))
                {
                    txtProfitCenter.Text = string.Empty;
                    txtProfitCenterDes.Text = string.Empty;
                    return;
                }

                _basePage = new Base();
                if (!_basePage.CHNLSVC.General.CheckProfitCenter(txtCompany.Text.ToUpper().Trim(), txtProfitCenter.Text.Trim().ToUpper()))
                {
                    lblWarn.Text = "Please check the profit center.";
                    errorDiv.Visible = true;
                    txtProfitCenter.Text= string.Empty;
                    txtProfitCenterDes.Text = string.Empty;
                    return;
                }
                txtProfitCenterDes.Text = Get_pc_HIRC_SearchDesc(75, txtProfitCenter.Text.ToUpper().ToUpper());
                txtProfitCenterDes.ToolTip = txtProfitCenterDes.Text;
            }
            catch (Exception ex)
            {
                errorDiv.Visible = true;
                lblWarn.Text = ex.Message;
            }
        }


        public bool IsDisplayRawData
        {
            get { return _isDisplayRawData; }
            set { _isDisplayRawData = value; }
        }
        protected void dvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserPopoup.Hide();
            Session["ISShow"] = null;
            Session["Profitsearch"] = null;

            string name = dvResult.SelectedRow.Cells[1].Text;
            string Des = dvResult.SelectedRow.Cells[2].Text;
            if (Label8.Text == "69")
            {
                txtCompany.Text = name;
                txtCompanyDes.Text = Des;
                txtCompanyDes.ToolTip = Des;
                errorDiv.Visible = false;
            }
            if (Label8.Text == "70")
            {
                txtChannel.Text = name;
                txtChannelDes.Text = Des;
                txtChannelDes.ToolTip = Des;
                errorDiv.Visible = false;
            }
            if (Label8.Text == "71")
            {
                txtSubChannel.Text = name;
                txtSubChannelDes.Text = Des;
                txtSubChannelDes.ToolTip = Des;
                errorDiv.Visible = false;
            }
            if (Label8.Text == "72")
            {
                txtArea.Text = name;
                txtAreaDes.Text = Des;
                txtAreaDes.ToolTip = Des;
                errorDiv.Visible = false;
            }
            if (Label8.Text == "73")
            {
                txtRegion.Text = name;
                txtRegionDes.Text = Des;
                txtRegionDes.ToolTip = Des;
                errorDiv.Visible = false;
            }
            if (Label8.Text == "74")
            {
                txtZone.Text = name;
                txtZoneDes.Text = Des;
                txtZoneDes.ToolTip = Des;
                errorDiv.Visible = false;
            }
            if (Label8.Text == "75")
            {
                txtProfitCenter.Text = name;
                txtProfitCenterDes.Text = Des;
                txtProfitCenterDes.ToolTip = Des;
                errorDiv.Visible = false;
            }
            Label8.Text = "";
           
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            _basePage = new Base();
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(txtCompany.Text.ToUpper() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        //ucPCSE002
                        if (_isDisplayRawData == false)
                            paramsText.Append(txtCompany.Text.ToUpper() + seperator + txtChannel.Text.ToUpper() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        else
                            paramsText.Append(txtCompany.Text.ToUpper() + seperator + string.Empty + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        //ucPCSE002
                        if (_isDisplayRawData == false)
                            paramsText.Append(txtCompany.Text.ToUpper() + seperator + txtChannel.Text.ToUpper() + seperator + txtSubChannel.Text.ToUpper() + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        else
                            paramsText.Append(txtCompany.Text.ToUpper() + seperator + string.Empty + seperator + string.Empty + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        //ucPCSE002
                        if (_isDisplayRawData == false)
                            paramsText.Append(txtCompany.Text.ToUpper() + seperator + txtChannel.Text.ToUpper() + seperator + txtSubChannel.Text.ToUpper() + seperator + txtArea.Text.ToUpper() + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        else
                            paramsText.Append(txtCompany.Text.ToUpper() + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        //ucPCSE002
                        if (_isDisplayRawData == false)
                            paramsText.Append(txtCompany.Text.ToUpper() + seperator + txtChannel.Text.ToUpper() + seperator + txtSubChannel.Text.ToUpper() + seperator + txtArea.Text.ToUpper() + seperator + txtRegion.Text.ToUpper() + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        else
                            paramsText.Append(txtCompany.Text.ToUpper() + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(txtCompany.Text.ToUpper() + seperator + txtChannel.Text.ToUpper() + seperator + txtSubChannel.Text.ToUpper() + seperator + txtArea.Text.ToUpper() + seperator + txtRegion.Text.ToUpper() + seperator + txtZone.Text.ToUpper() + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(txtCompany.Text.ToUpper() + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        //paramsText.Append(txtCompany.Text.Trim() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            _basePage = new Base();
            try
            {
                _basePage = new Base();
                if (Label8.Text == "69")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    dvResult.DataSource = _result;
                    Session["DataSource"] = _result;
                    dvResult.DataBind();
                    // UserPopoup.Show();
                    Session["Profitsearch"] = "search";
                }
                if (Label8.Text == "70")
                {
                    if (IsDisplayRawData)
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();
                        Session["Profitsearch"] = "search";
                    }
                    else
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();
                        Session["Profitsearch"] = "search";
                    }
                }
                if (Label8.Text == "71")
                {
                    if (IsDisplayRawData)
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();
                        Session["Profitsearch"] = "search";
                    }
                    else
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();
                        Session["Profitsearch"] = "search";
                    }
                }
                if (Label8.Text == "72")
                {
                    if (IsDisplayRawData)
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();
                        Session["Profitsearch"] = "search";
                    }
                    else
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();
                        Session["Profitsearch"] = "search";
                    }
                }
                if (Label8.Text == "73")
                {
                    if (IsDisplayRawData)
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();
                        Session["Profitsearch"] = "search";
                    }
                    else
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();
                        Session["Profitsearch"] = "search";
                    }
                }
                if (Label8.Text == "74")
                {
                    if (IsDisplayRawData)
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();
                        Session["Profitsearch"] = "search";
                    }
                    else
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();
                        Session["Profitsearch"] = "search";
                    }
                }
                if (Label8.Text == "75")
                {
                    if (IsDisplayRawData)
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();
                        Session["Profitsearch"] = "search";
                    }
                    else
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                        DataTable _result = _basePage.CHNLSVC.CommonSearch.GetPC_SearchData(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                        dvResult.DataSource = _result;
                        Session["DataSource"] = _result;
                        dvResult.DataBind();
                        // UserPopoup.Show();
                        Session["Profitsearch"] = "search";
                    }
                }
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                errorDiv.Visible = true;
                lblWarn.Text = ex.Message;
            }
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
        public string Get_pc_HIRC_SearchDesc(int i, string _code)
        {
            //if (i > 68 || i < 76)
            //{
            //    return null;
            //}
            ChannelOperator chnlOpt = new ChannelOperator();
            CommonUIDefiniton.SearchUserControlType _type = (CommonUIDefiniton.SearchUserControlType)i;

            Base _basePage = new Base();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            _basePage = new Base();
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(txtCompany.Text.ToUpper() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(txtCompany.Text.ToUpper() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(txtCompany.Text.ToUpper() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(txtCompany.Text.ToUpper() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(txtCompany.Text.ToUpper() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(txtCompany.Text.ToUpper() + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }

                default:
                    break;
            }

            DataTable dt = chnlOpt.CommonSearch.Get_PC_HIRC_SearchData(paramsText.ToString(), "CODE", _code);
            if (dt == null)
            {
                return null;
            }
            if (dt.Rows.Count <= 0 || dt == null || dt.Rows.Count > 1)
                return null;
            else
                return dt.Rows[0][1].ToString();
        }

        public void ClearAllTextBoxs(bool loadCom = false)
        {
            txtCompany.Text = string.Empty;
            txtCompanyDes.Text = string.Empty;
            txtChannel.Text = string.Empty;
            txtChannelDes.Text = string.Empty;
            txtSubChannel.Text = string.Empty;
            txtSubChannelDes.Text = string.Empty;
            txtArea.Text = string.Empty;
            txtAreaDes.Text = string.Empty;
            txtRegion.Text = string.Empty;
            txtRegionDes.Text = string.Empty;
            txtZone.Text = string.Empty;
            txtZoneDes.Text = string.Empty;
            txtProfitCenter.Text = string.Empty;
            txtProfitCenterDes.Text = string.Empty;
            if (loadCom)
            {
                txtCompany.Text = Session["UserCompanyCode"].ToString();
                txtCompany_TextChanged(null, null);
            }
        }

        protected void dvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dvResult.PageIndex = e.NewPageIndex;
            DataTable dt = (DataTable)Session["DataSource"];
            dvResult.DataSource = dt;
            dvResult.DataBind();
            UserPopoup.Show();
        }

        //protected void btnClose_Click(object sender, EventArgs e)
        //{
        //    txtSearchbyword.Text = "";
        //    UserPopoup.Hide();
        //}
    }
}