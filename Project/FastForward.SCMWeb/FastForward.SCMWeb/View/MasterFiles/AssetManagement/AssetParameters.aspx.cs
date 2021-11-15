using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.InventoryNew;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace FastForward.SCMWeb.View.MasterFiles.AssetManagemenr
{
    public partial class AssetMaster : Base
    {
        List<REF_ITM_CATE1> _lstcate1 = new List<REF_ITM_CATE1>();
        List<MasterItemSubCate> _lstcate2 = new List<MasterItemSubCate>();
        List<REF_ITM_CATE3> _lstcate3 = new List<REF_ITM_CATE3>();
        List<REF_ITM_CATE4> _lstcate4 = new List<REF_ITM_CATE4>();
        List<REF_ITM_CATE5> _lstcate5 = new List<REF_ITM_CATE5>();
        List<MasterItemBrand> _lstbrand = new List<MasterItemBrand>();
        List<MasterItemModel> _lstmodel = new List<MasterItemModel>();
        List<MasterUOM> _lstuom = new List<MasterUOM>();
        List<MasterColor> _lstclr = new List<MasterColor>();


        List<REF_ITM_CATE1> _lstcate1del = new List<REF_ITM_CATE1>();
        List<MasterItemSubCate> _lstcate2del = new List<MasterItemSubCate>();
        List<REF_ITM_CATE3> _lstcate3del = new List<REF_ITM_CATE3>();
        List<REF_ITM_CATE4> _lstcate4del = new List<REF_ITM_CATE4>();
        List<REF_ITM_CATE5> _lstcate5del = new List<REF_ITM_CATE5>();
        List<MasterItemBrand> _lstbranddel = new List<MasterItemBrand>();
        List<MasterItemModel> _lstmodeldel = new List<MasterItemModel>();
        List<MasterUOM> _lstuomdel = new List<MasterUOM>();
        List<MasterColor> _lstclrdel = new List<MasterColor>();
        Boolean _IsCat = false;
        Hashtable hashtable = new Hashtable();
        Hashtable hashtable2 = new Hashtable();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageClear();
                if (Session["UserCompanyCode"] != null)
                    LoadDepreciationMethods();
            }
            else
            {
                if (Session["popup"] == "true")
                {
                    UserPopoup.Show();
                    Session["popup"] = "";
                }
            }
        }

        private void LoadDepreciationMethods()
        {
            DataTable _result = CHNLSVC.Inventory.GetDepreciationMethods(Session["UserCompanyCode"].ToString());
            DepriciationDropDownList.DataSource = _result;
            DepriciationDropDownList.DataTextField = "DESCRIPTION";
            DepriciationDropDownList.DataValueField = "CODE";
            DepriciationDropDownList.DataBind();

            //Load Asset Parameter Table
            DataTable _assetParameters = CHNLSVC.Inventory.GetAssetParameters(Session["UserCompanyCode"].ToString());
            GridViewAssetParameters.DataSource = _assetParameters;
            GridViewAssetParameters.DataBind();

            Session["assetParameterTable"] = _assetParameters;

            //Load BaseDropDownList
            // BaseDropDownList.Items.Insert(0, new ListItem("All", ""));
            BaseDropDownList.Items.Insert(0, new ListItem("Rate", "PER"));
            BaseDropDownList.Items.Insert(1, new ListItem("Year", "YE"));
            depRateDiv.InnerText = "Rate";


            //BaseDropDownList.SelectedValue = "0";

            //Load Disposal or mark
            
            DisposalOrmarkDropDownList.Items.Insert(0, new ListItem("Yes", "1"));
            DisposalOrmarkDropDownList.Items.Insert(1, new ListItem("No", "0"));

            DataTable _serData = new DataTable();

            _serData = CHNLSVC.Inventory.GetAllCompanyStatus(Session["UserCompanyCode"].ToString());


            StatusDropDownList.DataTextField = "MIS_DESC";
            StatusDropDownList.DataValueField = "MIC_CD";

            StatusDropDownList.DataSource = _serData;
            StatusDropDownList.DataBind();


        }
        protected void basedropdownlistChange(object sender, EventArgs e)
        {
            if (BaseDropDownList.SelectedValue == "PER")
            {
                depRateDiv.InnerText = "Rate";
            }
            else
            {
                depRateDiv.InnerText = "Year";
            }

        }
        protected void AddAssetToGrid_Click(object sender, EventArgs e)
        {
            if (DepricationValue.Text.Equals(""))
            {
                DisplayMessage("Please Enter Deprication Value", 2);
                return;
            }
            if (AssetCat01TextBox.Text.Equals(""))
            {
                DisplayMessage("Please select a category 01", 2);
            }
            if (AssetCat02TextBox.Text.Equals(""))
            {
                DisplayMessage("Please select sub category", 2);
            }

            DataTable _assetParaTable = Session["assetParameterTable"] as DataTable;
            DataRow dr = null;

            dr = _assetParaTable.NewRow();

            string cat01 = AssetCat01TextBox.Text.ToUpper();
            string cat02 = AssetCat02TextBox.Text.ToUpper();
            string depMethod = DepriciationDropDownList.SelectedValue.ToString();
            string basedOn = BaseDropDownList.SelectedValue.ToString();
            string depValue = DepricationValue.Text;
            int markOrDisposal = Convert.ToInt32(DisposalOrmarkDropDownList.SelectedValue.ToString());
            string status = StatusDropDownList.SelectedValue.ToString();

            foreach (DataRow dataRow in _assetParaTable.Rows)
            {
                if ((dataRow["map_cat1"].ToString().Equals(cat01)) && (dataRow["map_cat2"].ToString().Equals(cat02)) && (dataRow["map_stus"].ToString().Equals(status)))
                {
                    DisplayMessage("Cannot save same category with same status", 2);
                    return;
                }

            }

            dr["map_dep_mth"] = depMethod;
            dr["map_cat1"] = cat01;
            dr["map_cat2"] = cat02;
            dr["map_dep_base"] = basedOn;
            dr["map_dep_val"] = depValue;
            dr["map_stus"] = status;
            dr["map_dis"] = markOrDisposal;

            _assetParaTable.Rows.Add(dr);
            Session["assetParameterTable"] = _assetParaTable;
            GridViewAssetParameters.DataSource = _assetParaTable;
            GridViewAssetParameters.DataBind();

            AssetCat01TextBox.Text = string.Empty;
            AssetCat02TextBox.Text = string.Empty;
            DepricationValue.Text = string.Empty;
            AssetCat01DesTextBox.Text = string.Empty;
            AssetCat02DesTextBox.Text = string.Empty;

        }
        private void pageClear()
        {
            Session["_IsCat"] = "false";
            _lstcate1 = CHNLSVC.General.GetItemCate1();
            grdCate1.DataSource = null;
            grdCate1.DataSource = new List<REF_ITM_CATE1>();
            grdCate1.DataSource = _lstcate1;
            grdCate1.DataBind();
            ViewState["_lstcate1"] = _lstcate1;
            grdCate2.DataSource = new List<MasterItemSubCate>();
            grdCate2.DataBind();
            grdCate2.Visible = false;
            lbnodat.Visible = true;
            //grdCate3.DataSource = new int[] { };
            //grdCate3.DataBind();
            //grdCate3.Visible = false;
            //lbCate3no.Visible = true;
            //grdCate4.DataSource = new int[] { };
            //grdCate4.DataBind();
            //grdCate4.Visible = false;
            //lbCate4no.Visible = true;
            // grdCate5.DataSource = new int[] { };
            //    grdCate5.DataBind();
            //    grdCate5.Visible = false;
            //    lbCate5no.Visible = true;
            txtCate1.Text = string.Empty;
            txtCate1Des.Text = string.Empty;

            hashtable.Add("_cat1tag", 0);
            hashtable.Add("_cat2tag", 0);
            hashtable.Add("_cat3tag", 0);
            hashtable.Add("_cat4tag", 0);
            hashtable.Add("_cat5tag", 0);
            hashtable.Add("_brandtag", 0);
            hashtable.Add("_uomtag", 0);
            hashtable.Add("_colortag", 0);
            hashtable.Add("_modeltag", 0);

            Session["hashtable"] = hashtable;

            hashtable2.Add("_cat1", string.Empty);
            hashtable2.Add("_cat2", string.Empty);
            hashtable2.Add("_cat3", string.Empty);
            hashtable2.Add("_cat4", string.Empty);

            Session["hashtable2"] = hashtable2;

            Session["countlsit"] = "";

        }


        #region commen parameter
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            bool _IsCat = Convert.ToBoolean(Session["_IsCat"].ToString());
            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.ModelMaster:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {

                        paramsText.Append(txtCate1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(txtCate1.Text + seperator + txtCate2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ItemBrand:
                    {

                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.masterCat1:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.masterCat1.ToString() + seperator + "" + seperator + "CAT_Main" + seperator);


                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat2:
                    {

                        //  if (_IsCat == true)
                        //  {
                        //      paramsText.Append(txtMainCat.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.masterCat2.ToString() + seperator + "" + seperator + "CAT_Sub1" + seperator);
                        // }

                        // else
                        // {
                        paramsText.Append(txtCate1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.masterCat2.ToString() + seperator + "" + seperator + "CAT_Sub1" + seperator);

                        //  }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat3:
                    {
                        //if (_IsCat == true)
                        //{
                        //    paramsText.Append(txtMainCat.Text + seperator + txtCat1.Text + seperator + CommonUIDefiniton.SearchUserControlType.masterCat3.ToString() + seperator + "" + seperator + "CAT_Sub2" + seperator);
                        //}
                        //else
                        //{
                        paramsText.Append(txtCate1.Text + seperator + txtCate2.Text + seperator + CommonUIDefiniton.SearchUserControlType.masterCat3.ToString() + seperator + "" + seperator + "CAT_Sub2" + seperator);

                        // }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat4:
                    {
                       
                            paramsText.Append(txtCate1.Text + seperator + txtCate2.Text + seperator + txtCate3.Text + seperator + CommonUIDefiniton.SearchUserControlType.masterCat4.ToString() + seperator + "CAT_Sub3" + seperator);
                       
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.masterCat5:
                //    {
                //        if (_IsCat == true)
                //        {
                //            paramsText.Append(txtMainCat.Text + seperator + txtCat1.Text + seperator + txtCat2.Text + seperator + txtCat3.Text + seperator + "CAT_Sub4" + seperator);
                //        }
                //        else
                //        {
                //            paramsText.Append(txtCate1.Text + seperator + txtCate2.Text + seperator + txtCate3.Text + seperator + txtCate4.Text + seperator + "CAT_Sub4" + seperator);
                //        }
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.masterColor:
                    {
                        paramsText.Append("");
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.masterUOM:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AssetCat1:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.masterCat1.ToString() + seperator + "" + seperator + "CAT_Main" + seperator);


                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AssetCat2:
                    {
                        paramsText.Append(AssetCat01TextBox.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.masterCat2.ToString() + seperator + "" + seperator + "CAT_Sub1" + seperator);

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
        #endregion

        #region Modalpopup
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string ID = grdResult.SelectedRow.Cells[1].Text;
            string Des = grdResult.SelectedRow.Cells[2].Text;
            if (lblvalue.Text == "MCode")
            {
                txtCate1.Text = ID;
                txtcatsearch.Text = ID;
                txtCate1Des.Text = Des;
                lblvalue.Text = "";
                _lstcate1 = ViewState["_lstcate1"] as List<REF_ITM_CATE1>;

                var _filter = _lstcate1.Where(X => X.Ric1_cd == txtcatsearch.Text.ToUpper()).ToList();
                if (_filter.Count > 0)
                {
                    grdCate1.DataSource = _filter;
                    grdCate1.DataBind();
                }
                else
                {
                    grdCate1.DataSource = _lstcate1;
                    grdCate1.DataBind();
                    DisplayMessage("No main categorization code found", 2);
                }
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "Cate2")
            {
                txtCate2.Text = ID;
                txtCate2Des.Text = Des;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "Cate3")
            {
                txtCate3.Text = ID;
                txtCate3Des.Text = Des;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "Cate4")
            {
                txtCate4.Text = ID;
                txtCate4Des.Text = Des;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            //if (lblvalue.Text == "Cate5")
            //{
            //    txtCate5.Text = ID;
            //    txtCate5Des.Text = Des;
            //    lblvalue.Text = "";
            //    UserPopoup.Hide();
            //    return;
            //}

            if (lblvalue.Text == "masterCat1")
            {
                // txtMainCat.Text = ID;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "masterCat2")
            {
                //txtCat1.Text = ID;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "masterCat3")
            {
                //  txtCat2.Text = ID;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "masterCat4")
            {
                //  txtCat3.Text = ID;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "masterCat5")
            {
                //  txtCat4.Text = ID;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "ItemBrand")
            {
                txtBrand.Text = ID;
                txtBrandName.Text = Des;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "masterUOM")
            {
                txtUOM.Text = ID;
                txtUOMDes.Text = Des;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "masterColor")
            {
                txtColor.Text = ID;
                txtColorDes.Text = Des;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            //
            if (lblvalue.Text == "assetcat01")
            {
                AssetCat01TextBox.Text = ID;
                AssetCat01DesTextBox.Text = Des;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "assetcat02")
            {
                AssetCat02TextBox.Text = ID;
                AssetCat02DesTextBox.Text = Des;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "MCode")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Cate2")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Cate3")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat3);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Cate4")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat4);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Model")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                DataTable _result = CHNLSVC.CommonSearch.GetAllModels(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "masterCat1")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "masterCat2")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "masterCat3")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat3);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "masterCat4")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat4);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "masterCat5")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat5);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }

            if (lblvalue.Text == "ItemBrand")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "masterUOM")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "masterColor")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterColor);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchColorMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "assetcat01")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AssetCat1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "assetcat02")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AssetCat2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }

        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "MCode")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }//assetcat01
            if (lblvalue.Text == "Cate2")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Cate3")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat3);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Cate4")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat4);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Model")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "masterCat1")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "masterCat2")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "masterCat3")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat3);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "masterCat4")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat4);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "masterCat5")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat5);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "ItemBrand")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "masterUOM")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "masterColor")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterColor);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchColorMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "assetcat01")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AssetCat1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }//assetcat01
            if (lblvalue.Text == "assetcat02")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AssetCat2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
        }


        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "MCode")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                Session["popup"] = "true";
                return;
            }
            if (lblvalue.Text == "Cate2")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                Session["popup"] = "true";
                return;
            }
            if (lblvalue.Text == "Cate2")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat3);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                Session["popup"] = "true";
                return;
            }
            if (lblvalue.Text == "Cate4")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat4);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                Session["popup"] = "true";
                return;
            }
            if (lblvalue.Text == "Model")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                Session["popup"] = "true";
                return;
            }
            if (lblvalue.Text == "masterCat1")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                Session["popup"] = "true";
                return;
            }
            if (lblvalue.Text == "masterCat2")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                Session["popup"] = "true";
                return;
            }
            if (lblvalue.Text == "masterCat3")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat3);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                Session["popup"] = "true";
                return;
            }
            if (lblvalue.Text == "masterCat4")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat4);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                Session["popup"] = "true";
                return;
            }
            if (lblvalue.Text == "masterCat5")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat5);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                Session["popup"] = "true";
                return;
            }
            if (lblvalue.Text == "ItemBrand")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                Session["popup"] = "true";
                return;
            }
            if (lblvalue.Text == "masterUOM")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                Session["popup"] = "true";
                return;
            }
            if (lblvalue.Text == "masterColor")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterColor);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchColorMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                Session["popup"] = "true";
                return;
            }
            if (lblvalue.Text == "assetcat01")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AssetCat1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                Session["popup"] = "true";
                return;
            }//assetcat01
            if (lblvalue.Text == "assetcat02")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AssetCat2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                Session["popup"] = "true";
                return;
            }
        }


        #endregion

        #region Maincatergory
        protected void lbtnCate1_Click(object sender, EventArgs e)
        {
            try
            {
                Session["_IsCat"] = "false";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "MCode";
                UserPopoup.Show();
            }

            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }
        protected void lbtnCate1_asset_Click(object sender, EventArgs e)
        {
            try
            {
                Session["_IsCat"] = "false";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "assetcat01";
                UserPopoup.Show();
            }

            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtCate1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCate1.Text))
            {
                DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(txtCate1.Text);
                if (_categoryDet != null && _categoryDet.Rows.Count > 0)
                {
                    txtCate1Des.Text = _categoryDet.Rows[0]["RIC1_DESC"].ToString();
                }
            }
        }//
        protected void Assetcat01_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(AssetCat01TextBox.Text))
            {
                DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(AssetCat01TextBox.Text.ToUpper());
                if (_categoryDet != null && _categoryDet.Rows.Count > 0)
                {
                    AssetCat01DesTextBox.Text = _categoryDet.Rows[0]["RIC1_DESC"].ToString();
                }
                else
                {
                    AssetCat01TextBox.Text = string.Empty;
                    AssetCat01DesTextBox.Text = string.Empty;
                    DisplayMessage("Invalid category", 2);
                }

            }
            else
            {
                AssetCat01DesTextBox.Text = string.Empty;
            }
        }

        protected void lbtnAddCate1_Click(object sender, EventArgs e)
        {
            Session["countlsit"] = "";
            _lstcate1 = ViewState["_lstcate1"] as List<REF_ITM_CATE1>;

            if (string.IsNullOrEmpty(txtCate1.Text))
            {
                DisplayMessage("Please Enter The Item Category ", 2);
                return;
            }

            if (string.IsNullOrEmpty(txtCate1Des.Text))
            {
                DisplayMessage("Please Enter Item Category Description", 2);
                return;
            }
            if (_lstcate1 != null)
            {
                var result = _lstcate1.SingleOrDefault(x => x.Ric1_cd == txtCate1.Text);
                if (result != null)
                {
                    result.Ric1_act = (check_active_cat1.Checked == true) ? true : false;// true;                   
                    // _cat1.Value = txtCate1.Text;
                    hashtable2 = Session["hashtable2"] as Hashtable;

                    hashtable2["_cat1"] = txtCate1.Text;
                    Session["hashtable2"] = hashtable2;

                    grdCate1.DataSource = _lstcate1;
                    grdCate1.DataBind();
                    txtCate1.Text = "";
                    txtCate1Des.Text = "";
                    check_active_cat1.Checked = false;
                    ViewState["_lstcate1"] = _lstcate1;
                    Session["countlsit"] = "yes";
                    DisplayMessage("This category already exist", 2);
                    return;
                }

            }
            else
            {
                _lstcate1 = new List<REF_ITM_CATE1>();
            }
            int _count = _lstcate1.Count;
            Session["countlsit"] = _count;
            REF_ITM_CATE1 _cate1 = new REF_ITM_CATE1();
            _cate1.Ric1_cd = txtCate1.Text;
            _cate1.Ric1_desc = txtCate1Des.Text;
            _cate1.Ric1_act = (check_active_cat1.Checked == true) ? true : false;// true;
            _cate1.RIC1_CRE_BY = Session["UserID"].ToString();
            _cate1.RIC1_MOD_BY = Session["UserID"].ToString();


            _lstcate1.Add(_cate1);

            grdCate1.DataSource = null;
            grdCate1.AutoGenerateColumns = false;
            grdCate1.DataSource = new List<REF_ITM_CATE1>();
            grdCate1.DataSource = _lstcate1;
            grdCate1.DataBind();
            hashtable2 = Session["hashtable2"] as Hashtable;

            hashtable2["_cat1"] = txtCate1.Text;
            Session["hashtable2"] = hashtable2;
            // _cat1.Value = txtCate1.Text;

            txtCate1.Text = "";
            txtCate1Des.Text = "";
            DisplayMessage("Successfully Added", 3);

            //_cat1tag.Value = "1";
            hashtable = Session["hashtable"] as Hashtable;

            hashtable["_cat1tag"] = 1;
            Session["hashtable"] = hashtable;
            Session["countlsit"] = "yes";

        }

        protected void lbtnClear1_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "Yes")
            {
                txtCate1.Text = "";
                txtCate1Des.Text = "";
                // _cat1.Value = string.Empty;
                hashtable2 = Session["hashtable2"] as Hashtable;

                hashtable2["_cat1"] = string.Empty;
                Session["hashtable2"] = hashtable2;
            }
        }

        protected void lbtnDeletecat1_Click(object sender, EventArgs e)
        {
            bool _IsDeleted = false;
            _lstcate1 = ViewState["_lstcate1"] as List<REF_ITM_CATE1>;
            _lstcate1del = ViewState["_lstcate1del"] as List<REF_ITM_CATE1>;
            if (txtDeleteconformmessageValue.Value == "No")
            {
                return;
            }
            if (_lstcate1del == null)
            {
                _lstcate1del = new List<REF_ITM_CATE1>();
            }
            foreach (GridViewRow _row in grdCate1.Rows)
            {
                CheckBox checkbox = _row.FindControl("chk_ReqItem") as CheckBox;
                if (checkbox.Checked == true)
                {
                    string type = (_row.FindControl("colmCode") as Label).Text;

                    _lstcate1del.AddRange(_lstcate1.Where(x => x.Ric1_cd == type));

                    _lstcate1.RemoveAll(x => x.Ric1_cd == type);
                    grdCate1.DataSource = _lstcate1;
                    grdCate1.DataBind();
                    ViewState["_lstcate1"] = _lstcate1;

                    hashtable = Session["hashtable"] as Hashtable;
                    hashtable["_cat1tag"] = 1;
                    Session["hashtable"] = hashtable;
                    _IsDeleted = true;
                    ViewState["_lstcate1del"] = _lstcate1del;
                    Session["countlsit"] = "true";

                }
            }
            if (_IsDeleted == false)
            {
                DisplayMessage("Please select the Main category to delete", 2);
            }
        }

        protected void lbtnSave1_Click(object sender, EventArgs e)
        {
            if (Session["countlsit"].ToString() == "")
            {
                DisplayMessage("No any modifications or new record to save", 2);
                return;
            }
            // _lstcate1 = ViewState["_lstcate1"] as List<REF_ITM_CATE1>;
            foreach (GridViewRow _row in grdCate1.Rows)
            {
                REF_ITM_CATE1 _cate1 = new REF_ITM_CATE1();
                Label code = (Label)_row.FindControl("colmCode");
                Label dec = (Label)_row.FindControl("colmDes");
                CheckBox IsDefault = (CheckBox)_row.FindControl("colmStataus");
                _cate1.Ric1_cd = code.Text;
                _cate1.Ric1_desc = dec.Text;
                _cate1.Ric1_act = (IsDefault.Checked == true) ? true : false;// true;
                _cate1.RIC1_CRE_BY = Session["UserID"].ToString();
                _cate1.RIC1_MOD_BY = Session["UserID"].ToString();
                _lstcate1.Add(_cate1);

            }

            if (_lstcate1 == null)
            {
                DisplayMessage("Please select Main items to save", 2);
                return;

            }

            if (_lstcate1.Count == 0)
            {
                DisplayMessage("Please select Main items to save", 2);
                return;
            }

            //if (string.IsNullOrEmpty(txtCate1.Text))
            //{
            //    DisplayMessage("Please Add Item Main Category", 2);                  
            //    return;
            //}

            //if (string.IsNullOrEmpty(txtCate1Des.Text))
            //{
            //    DisplayMessage("Please Add Item Main Category Description", 2);                       
            //    return;
            //}
            hashtable = Session["hashtable"] as Hashtable;
            int _cat1tag = (int)hashtable["_cat1tag"];

            //if (_cat1tag == 0)
            //{
            //    DisplayMessage("No any modificatoin or new record to save", 2);                   
            //    return;

            //}
            // int oldcount 
            //int _count = _lstcate1.Count;
            // Session["countlsit"] = _count;

            _lstcate1del = ViewState["_lstcate1del"] as List<REF_ITM_CATE1>;
            if (_lstcate1del == null)
            {
                _lstcate1del = new List<REF_ITM_CATE1>();
            }
            string _err;
            int row_aff = CHNLSVC.General.SaveItemCate1(_lstcate1, _lstcate1del, out _err);
            if (row_aff == 1)
            {
                DisplayMessage("Successfully Saved", 3);
                // _cat1tag.Value = "0";
                hashtable = Session["hashtable"] as Hashtable;

                hashtable["_cat1tag"] = 0;
                Session["hashtable"] = hashtable;
            }
            else
            {
                DisplayMessage("Terminate", 2);

            }
        }

        protected void lbtnCat1Select_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grdCate1.Rows.Count; i++)
            {
                grdCate1.Rows[i].BackColor = System.Drawing.Color.White;
            }
            if (grdCate1.Rows.Count == 0) return;

            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                CheckBox _itemCode = (row.FindControl("colmStataus") as CheckBox);
                if (_itemCode.Checked == false)
                {
                    DisplayMessage("The code is inactive", 2);
                    return;
                }
                row.BackColor = System.Drawing.Color.LightCyan;

                hashtable2 = Session["hashtable2"] as Hashtable;
                string _cat1 = (row.FindControl("colmCode") as Label).Text;
                _lstcate2 = CHNLSVC.General.GetItemCate2(_cat1);
                grdCate2.DataSource = null;
                grdCate2.DataSource = new List<MasterItemSubCate>();
                grdCate2.DataSource = _lstcate2;
                grdCate2.DataBind();
                ViewState["_lstcate2"] = _lstcate2;
                hashtable2["_cat1"] = _cat1;
                hashtable2["_cat2"] = "";
                hashtable2["_cat3"] = "";
                hashtable2["_cat4"] = "";
                Session["hashtable2"] = hashtable2;
                grdCate2.Visible = true;
                lbnodat.Visible = false;
            }
        }

        protected void colmStataus_Click(object sender, EventArgs e)
        {
            Session["countlsit"] = "yes";
        }
        #endregion

        #region Sub Categorization

        protected void lbtnAddCate3_Click(object sender, EventArgs e)
        {

            Session["countlsit"] = "";
            _lstcate3 = ViewState["_lstcate3"] as List<REF_ITM_CATE3>;
            hashtable2 = Session["hashtable2"] as Hashtable;
            if (string.IsNullOrEmpty(txtCate3.Text))
            {
                DisplayMessage("Please Enter Item Category", 2);
                return;
            }

            if (string.IsNullOrEmpty(txtCate3Des.Text))
            {
                DisplayMessage("Please Enter Item Category Description", 2);
                return;
            }
            string _cat1 = (string)hashtable2["_cat1"];
            if (string.IsNullOrEmpty(_cat1))
            {
                DisplayMessage("Please Select Main Item Category", 2);
                return;
            }
            string _cat2 = (string)hashtable2["_cat2"];
            if (string.IsNullOrEmpty(_cat2))
            {
                DisplayMessage("Please Select Sub Item Category", 2);
                return;
            }
            if (_lstcate3 != null)
            {
                //REF_ITM_CATE3 result
                var result = _lstcate3.SingleOrDefault(x => x.Ric3_cd == txtCate3.Text && x.Ric3_cd1 == _cat1 && x.Ric3_cd2 == _cat2);
                if (result != null)
                {
                    // result.Ric2_act=(check_active_cat3.Checked == true) ? true : false;// true;       
                    //// hashtable2["_cat3"] = txtCate3.Text;
                    //// Session["hashtable2"] = hashtable2;
                    // //_cat2 = txtCate2.Text;
                    // grdCate3.DataSource = _lstcate3;
                    // grdCate3.DataBind();
                    // txtCate3.Text = "";
                    // txtCate3Des.Text = "";
                    // check_active_cat3.Checked = false;
                    // ViewState["_lstcate3"] = _lstcate3;

                    // Session["countlsit"] = "yes";
                    DisplayMessage("This category already exist", 2);
                    return;
                }
            }

            else
            {
                _lstcate3 = new List<REF_ITM_CATE3>();
            }

            REF_ITM_CATE3 _cate3 = new REF_ITM_CATE3();
            _cate3.Ric3_cd = txtCate3.Text;
            _cate3.Ric2_desc = txtCate3Des.Text;
            _cate3.Ric3_cd1 = _cat1;
            _cate3.Ric3_cd2 = _cat2;
            _cate3.Ric2_act = (check_active_cat3.Checked == true) ? true : false;// true;       //true;
            _cate3.RIC3_CRE_BY = Session["UserID"].ToString();
            _cate3.RIC3_MOD_BY = Session["UserID"].ToString();
            _lstcate3.Add(_cate3);

            grdCate3.DataSource = null;
            grdCate3.DataSource = new List<REF_ITM_CATE3>();
            grdCate3.DataSource = _lstcate3;
            grdCate3.DataBind();
            ViewState["_lstcate3"] = _lstcate3;
            // _cat3 = txtCate3.Text;
            hashtable2["_cat3"] = txtCate3.Text;
            Session["hashtable2"] = hashtable2;

            txtCate3.Text = "";
            txtCate3Des.Text = "";
            DisplayMessage("Successfully Added", 3);
            hashtable = Session["hashtable"] as Hashtable;
            hashtable["_cat3tag"] = 1;
            Session["hashtable"] = hashtable;

            Session["countlsit"] = "yes";
        }
        protected void labtnSave3_Click(object sender, EventArgs e)
        {
            if (Session["countlsit"].ToString() == "")
            {
                DisplayMessage("No any modifications or new record to save", 2);
                return;
            }
            hashtable2 = Session["hashtable2"] as Hashtable;
            //    _lstcate3 = ViewState["_lstcate3"] as List<REF_ITM_CATE3>;
            string _cat2 = (string)hashtable2["_cat2"];
            string _cat1 = (string)hashtable2["_cat1"];

            foreach (GridViewRow _row in grdCate3.Rows)
            {
                REF_ITM_CATE3 _cate3 = new REF_ITM_CATE3();
                Label code = (Label)_row.FindControl("colmCode_Cate3");
                Label dec = (Label)_row.FindControl("colmDes_Cate3");
                CheckBox IsDefault = (CheckBox)_row.FindControl("colmStataus_Cate3");

                _cate3.Ric3_cd = code.Text;
                _cate3.Ric2_desc = dec.Text;
                _cate3.Ric3_cd1 = _cat1;
                _cate3.Ric3_cd2 = _cat2;
                _cate3.Ric2_act = (IsDefault.Checked == true) ? true : false;// true;       //true;
                _cate3.RIC3_CRE_BY = Session["UserID"].ToString();
                _cate3.RIC3_MOD_BY = Session["UserID"].ToString();
                _lstcate3.Add(_cate3);
            }
            if (_lstcate3 == null)
            {
                DisplayMessage("selct Item Range 1 to save", 2);
                return;

            }

            if (_lstcate3.Count == 0)
            {
                DisplayMessage("Select Item Range 1 to save", 2);
                return;
            }
            if (!string.IsNullOrEmpty(txtCate3.Text))
            {
                DisplayMessage("Please add item range 1", 2);
                return;
            }

            if (!string.IsNullOrEmpty(txtCate3Des.Text))
            {
                DisplayMessage("Please add item range 1 Description", 2);
                return;
            }
            //if (_cat3tag == 0)
            //{
            //    MessageBox.Show("No any modificatoin or new record to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;

            //}
            _lstcate3del = ViewState["_lstcate3del"] as List<REF_ITM_CATE3>;
            if (_lstcate3del == null)
            {
                _lstcate3del = new List<REF_ITM_CATE3>();
            }
            string _err;
            int row_aff = CHNLSVC.General.SaveItemCate3(_lstcate3, _lstcate3del, out _err);
            if (row_aff == 1)
            {
                DisplayMessage("Successfully Saved", 3);
                hashtable = Session["hashtable"] as Hashtable;
                hashtable["_cat3tag"] = 0;
                Session["hashtable"] = hashtable;
                //_cat3tag = 0;
            }
            else
            {
                DisplayMessage("Terminate", 2);

            }
        }
        protected void lbtnClear3_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "Yes")
            {
                txtCate3.Text = "";
                txtCate3Des.Text = "";
            }
        }
        protected void lbtnDeletecat3_Click(object sender, EventArgs e)
        {
            bool _IsDeleted = false;
            _lstcate3 = ViewState["_lstcate3"] as List<REF_ITM_CATE3>;
            _lstcate3del = ViewState["_lstcate3del"] as List<REF_ITM_CATE3>;
            if (_lstcate3del == null)
            {
                _lstcate3del = new List<REF_ITM_CATE3>();
            }
            if (txtDeleteconformmessageValue.Value == "No")
            {
                return;
            }
            foreach (GridViewRow _row in grdCate3.Rows)
            {
                CheckBox checkbox = _row.FindControl("chk_ReqItem_cat3") as CheckBox;
                if (checkbox.Checked == true)
                {
                    string type = (_row.FindControl("colmCode_Cate3") as Label).Text;
                    _lstcate3del.AddRange(_lstcate3.Where(x => x.Ric3_cd == type));
                    _lstcate3.RemoveAll(x => x.Ric3_cd == type);
                    grdCate3.DataSource = _lstcate3;
                    grdCate3.DataBind();
                    // _cat3tag = 1;

                    ViewState["_lstcate3"] = _lstcate3;
                    ViewState["_lstcate3del"] = _lstcate3del;
                    hashtable = Session["hashtable"] as Hashtable;
                    hashtable["_cat3tag"] = 1;
                    Session["hashtable"] = hashtable;
                    _IsDeleted = true;
                    Session["countlsit"] = "true";
                }
            }
            if (_IsDeleted == false)
            {
                DisplayMessage("Please select the Item Range 1 to delete", 2);
            }
        }
        protected void lbtnCat3Select_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grdCate3.Rows.Count; i++)
            {
                grdCate3.Rows[i].BackColor = System.Drawing.Color.White;
            }
            if (grdCate3.Rows.Count == 0) return;

            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                CheckBox _itemCode = (row.FindControl("colmStataus_Cate3") as CheckBox);
                if (_itemCode.Checked == false)
                {
                    DisplayMessage("The code is inactive", 2);
                    return;
                }
                row.BackColor = System.Drawing.Color.LightCyan;
                hashtable2 = Session["hashtable2"] as Hashtable;
                string _cat1 = (string)hashtable2["_cat1"];
                string _cat2 = (string)hashtable2["_cat2"];
                string _cat3 = (row.FindControl("colmCode_Cate3") as Label).Text;
                _lstcate4 = CHNLSVC.General.GetItemCate4(_cat1, _cat2, _cat3);

                grdCate4.DataSource = null;
                grdCate4.DataSource = new List<REF_ITM_CATE4>();
                grdCate4.DataSource = _lstcate4;
                grdCate4.DataBind();
                hashtable2["_cat3"] = _cat3;
                hashtable2["_cat4"] = "";
                ViewState["_lstcate4"] = _lstcate4;
                Session["hashtable2"] = hashtable2;
                grdCate4.Visible = true;
                lbCate4no.Visible = false;
            }
        }
        //#region Item Range 2
        //protected void txtCate4_TextChanged(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txtCate4.Text))
        //    {
        //        REF_ITM_CATE4 subCate = CHNLSVC.General.GetItemCategory4(txtCate4.Text);
        //        if (subCate != null)
        //        {
        //            txtCate4Des.Text = subCate.Ric4_desc;
        //        }
        //    }
        //}

        //protected void lbtnCate4_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Session["_IsCat"] = "false";
        //        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat4);
        //        DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
        //        grdResult.DataSource = _result;
        //        grdResult.DataBind();
        //        BindUCtrlDDLData(_result);
        //        lblvalue.Text = "Cate4";
        //        UserPopoup.Show();
        //    }

        //    catch (Exception ex)
        //    {
        //        string _Msg = "Error Occurred while processing search customer";
        //        DisplayMessage(_Msg, 4);
        //    }
        //}

        //protected void lbtnAddCate4_Click(object sender, EventArgs e)
        //{
        //    Session["countlsit"] = "";
        //    _lstcate4 = ViewState["_lstcate4"] as List<REF_ITM_CATE4>;
        //    hashtable2 = Session["hashtable2"] as Hashtable;
        //    if (string.IsNullOrEmpty(txtCate4.Text))
        //    {
        //        DisplayMessage("Please Enter The Item Category ", 2);
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(txtCate4Des.Text))
        //    {
        //        DisplayMessage("Please Enter The Item Category Description  ", 2);
        //        return;
        //    }
        //    string _cat1 = (string)hashtable2["_cat1"];
        //    if (string.IsNullOrEmpty(_cat1))
        //    {
        //        DisplayMessage("Please Select The Main Item Category ", 2);
        //        return;
        //    }
        //    string _cat2 = (string)hashtable2["_cat2"];
        //    if (string.IsNullOrEmpty(_cat2))
        //    {
        //        DisplayMessage("Please Select The Sub Item Category", 2);
        //        return;
        //    }
        //    string _cat3 = (string)hashtable2["_cat3"];
        //    if (string.IsNullOrEmpty(_cat3))
        //    {
        //        DisplayMessage("Please Select Item Range 1", 2);
        //        return;
        //    }

        //    if (_lstcate4 != null)
        //    {
        //        //REF_ITM_CATE4 result 
        //        var result = _lstcate4.SingleOrDefault(x => x.Ric4_cd == txtCate4.Text && x.Ric4_cd1 == _cat1 && x.Ric4_cd2 == _cat2 && x.Ric4_cd3 == _cat3);
        //        if (result != null)
        //        {
        //            // result.Ric4_act = (check_active_cat4.Checked == true) ? true : false;// true;       
        //            //// _cat4 = txtCate4.Text;
        //            // grdCate4.DataSource = _lstcate4;
        //            // grdCate4.DataBind();
        //            // check_active_cat4.Checked = false;
        //            // txtCate4.Text = "";
        //            // txtCate4Des.Text = "";
        //            // ViewState["_lstcate4"] = _lstcate4;
        //            // Session["countlsit"] = "yes";
        //            DisplayMessage("This category already exist", 2);
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        _lstcate4 = new List<REF_ITM_CATE4>();
        //    }

        //    REF_ITM_CATE4 _cate4 = new REF_ITM_CATE4();
        //    _cate4.Ric4_cd = txtCate4.Text;
        //    _cate4.Ric4_desc = txtCate4Des.Text;
        //    _cate4.Ric4_cd1 = _cat1;
        //    _cate4.Ric4_cd2 = _cat2;
        //    _cate4.Ric4_cd3 = _cat3;
        //    _cate4.Ric4_act = (check_active_cat4.Checked == true) ? true : false;// true;  
        //    _cate4.RIC4_CRE_BY = Session["UserID"].ToString();
        //    _cate4.RIC4_MOD_BY = Session["UserID"].ToString();
        //    _lstcate4.Add(_cate4);

        //    grdCate4.DataSource = null;
        //    grdCate4.DataSource = new List<REF_ITM_CATE4>();
        //    grdCate4.DataSource = _lstcate4;
        //    grdCate4.DataBind();
        //    hashtable2["_cat4"] = txtCate4.Text;
        //    Session["hashtable2"] = hashtable2;

        //    // _cat4 = txtCate4.Text;
        //    txtCate4.Text = "";
        //    txtCate4Des.Text = "";

        //    DisplayMessage("Successfully Added", 3);
        //    hashtable = Session["hashtable"] as Hashtable;
        //    hashtable["_cat4tag"] = 1;
        //    Session["hashtable"] = hashtable;
        //    ViewState["_lstcate4"] = _lstcate4;
        //    //_cat4tag = 1;
        //    Session["countlsit"] = "yes";
        //}

        //protected void labtnSave4_Click(object sender, EventArgs e)
        //{
        //    if (Session["countlsit"].ToString() == "")
        //    {
        //        DisplayMessage("No any modifications or new record to save", 2);
        //        return;
        //    }
        //    hashtable2 = Session["hashtable2"] as Hashtable;
        //    //_lstcate4 = ViewState["_lstcate4"] as List<REF_ITM_CATE4>;
        //    string _cat2 = (string)hashtable2["_cat2"];
        //    string _cat1 = (string)hashtable2["_cat1"];
        //    string _cat3 = (string)hashtable2["_cat3"];
        //    MasterItemSubCate _cate2 = new MasterItemSubCate();
        //    foreach (GridViewRow _row in grdCate4.Rows)
        //    {

        //        Label code = (Label)_row.FindControl("colmCode_Cate4");
        //        Label dec = (Label)_row.FindControl("colmDes_Cate4");
        //        CheckBox IsDefault = (CheckBox)_row.FindControl("colmStataus_Cate4");

        //        REF_ITM_CATE4 _cate4 = new REF_ITM_CATE4();
        //        _cate4.Ric4_cd = code.Text;
        //        _cate4.Ric4_desc = dec.Text;
        //        _cate4.Ric4_cd1 = _cat1;
        //        _cate4.Ric4_cd2 = _cat2;
        //        _cate4.Ric4_cd3 = _cat3;
        //        _cate4.Ric4_act = (IsDefault.Checked == true) ? true : false;// true; 
        //        _cate4.RIC4_CRE_BY = Session["UserID"].ToString();
        //        _cate4.RIC4_MOD_BY = Session["UserID"].ToString();
        //        _lstcate4.Add(_cate4);
        //    }
        //    if (_lstcate4 == null)
        //    {
        //        DisplayMessage("Select Item Range 2 to save", 2);
        //        return;

        //    }
        //    if (_lstcate4.Count == 0)
        //    {
        //        DisplayMessage("Select Item Range 2 to save", 2);
        //        return;

        //    }
        //    if (!string.IsNullOrEmpty(txtCate4.Text))
        //    {
        //        DisplayMessage("Please add item range 2", 2);
        //        return;
        //    }

        //    if (!string.IsNullOrEmpty(txtCate4Des.Text))
        //    {
        //        DisplayMessage("Please add item range 2Description", 2);
        //        return;
        //    }
        //    //if (_cat4tag == 0)
        //    //{
        //    //    MessageBox.Show("No any modificatoin or new record to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    //    return;

        //    //}
        //    _lstcate4del = ViewState["_lstcate4del"] as List<REF_ITM_CATE4>;
        //    if (_lstcate4del == null)
        //    {
        //        _lstcate4del = new List<REF_ITM_CATE4>();
        //    }
        //    string _err;
        //    int row_aff = CHNLSVC.General.SaveItemCate4(_lstcate4, _lstcate4del, out _err);
        //    if (row_aff == 1)
        //    {
        //        DisplayMessage("Successfully Saved", 3);
        //        hashtable = Session["hashtable"] as Hashtable;
        //        hashtable["_cat4tag"] = 0;
        //        Session["hashtable"] = hashtable;

        //    }
        //    else
        //    {
        //        DisplayMessage("Terminate", 2);
        //    }
        //}

        //protected void lbtnDeletecat4_Click(object sender, EventArgs e)
        //{
        //    bool _IsDeleted = false;
        //    _lstcate4 = ViewState["_lstcate4"] as List<REF_ITM_CATE4>;
        //    _lstcate4del = ViewState["_lstcate4del"] as List<REF_ITM_CATE4>;
        //    if (_lstcate4del == null)
        //    {
        //        _lstcate4del = new List<REF_ITM_CATE4>();
        //    }
        //    if (txtDeleteconformmessageValue.Value == "No")
        //    {
        //        return;
        //    }
        //    foreach (GridViewRow _row in grdCate4.Rows)
        //    {
        //        CheckBox checkbox = _row.FindControl("chk_ReqItem_cat4") as CheckBox;
        //        if (checkbox.Checked == true)
        //        {
        //            string type = (_row.FindControl("colmCode_Cate4") as Label).Text;
        //            _lstcate4del.AddRange(_lstcate4.Where(x => x.Ric4_cd == type));

        //            _lstcate4.RemoveAll(x => x.Ric4_cd == type);
        //            grdCate4.DataSource = _lstcate4;
        //            grdCate4.DataBind();
        //            // _cat3tag = 1;

        //            ViewState["_lstcate4"] = _lstcate4;
        //            ViewState["_lstcate4del"] = _lstcate4del;
        //            hashtable = Session["hashtable"] as Hashtable;
        //            hashtable["_cat4tag"] = 1;
        //            Session["hashtable"] = hashtable;
        //            _IsDeleted = true;
        //            Session["countlsit"] = "true";
        //        }
        //    }
        //    if (_IsDeleted == false)
        //    {
        //        DisplayMessage("Please select the Item Range 2 to delete", 2);
        //    }
        //}

        //protected void lbtnClear4_Click(object sender, EventArgs e)
        //{
        //    if (txtClearlconformmessageValue.Value == "Yes")
        //    {
        //        txtCate4.Text = "";
        //        txtCate4Des.Text = "";
        //    }
        //}
        //protected void colmStataus_Cate4_Click(object sender, EventArgs e)
        //{
        //    Session["countlsit"] = "yes";
        //}
        //protected void lbtnCat4Select_Click(object sender, EventArgs e)
        //{
        //    for (int i = 0; i < grdCate4.Rows.Count; i++)
        //    {
        //        grdCate4.Rows[i].BackColor = System.Drawing.Color.White;
        //    }
        //    if (grdCate4.Rows.Count == 0) return;

        //    var lb = (LinkButton)sender;
        //    var row = (GridViewRow)lb.NamingContainer;
        //    if (row != null)
        //    {
        //        CheckBox _itemCode = (row.FindControl("colmStataus_Cate4") as CheckBox);
        //        if (_itemCode.Checked == false)
        //        {
        //            DisplayMessage("The code is inactive", 2);
        //            return;
        //        }
        //        row.BackColor = System.Drawing.Color.LightCyan;
        //        hashtable2 = Session["hashtable2"] as Hashtable;
        //        string _cat1 = (string)hashtable2["_cat1"];
        //        string _cat2 = (string)hashtable2["_cat2"];
        //        string _cat3 = (string)hashtable2["_cat3"];
        //        string _cat4 = (row.FindControl("colmCode_Cate4") as Label).Text;
        //        _lstcate5 = CHNLSVC.General.GetItemCate5(_cat1, _cat2, _cat3, _cat4);

        //        grdCate5.DataSource = null;
        //        grdCate5.DataSource = new List<REF_ITM_CATE5>();
        //        grdCate5.DataSource = _lstcate5;
        //        grdCate5.DataBind();
        //        hashtable2["_cat4"] = _cat4;
        //        ViewState["_lstcate5"] = _lstcate5;
        //        Session["hashtable2"] = hashtable2;
        //        grdCate5.Visible = true;
        //        lbCate5no.Visible = false;
        //    }
        //}

        //#endregion

        //#region Item Range 3
        //protected void txtCate5_TextChanged(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txtCate5.Text))
        //    {
        //        REF_ITM_CATE5 subCate = CHNLSVC.General.GetItemCategory5(txtCate5.Text);
        //        if (subCate != null)
        //        {
        //            txtCate5Des.Text = subCate.Ric5_desc;
        //        }
        //    }
        //}

        //protected void lbtnCate5_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Session["_IsCat"] = "false";
        //        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat5);
        //        DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
        //        grdResult.DataSource = _result;
        //        grdResult.DataBind();
        //        BindUCtrlDDLData(_result);
        //        lblvalue.Text = "Cate5";
        //        UserPopoup.Show();
        //    }

        //    catch (Exception ex)
        //    {
        //        string _Msg = "Error Occurred while processing search customer";
        //        DisplayMessage(_Msg, 4);
        //    }
        //}

        //protected void lbtnAddCate5_Click(object sender, EventArgs e)
        //{
        //    Session["countlsit"] = "";
        //    _lstcate5 = ViewState["_lstcate5"] as List<REF_ITM_CATE5>;
        //    hashtable2 = Session["hashtable2"] as Hashtable;
        //    if (string.IsNullOrEmpty(txtCate5.Text))
        //    {
        //        DisplayMessage("Please Enter the Item Category", 2);
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(txtCate5Des.Text))
        //    {
        //        DisplayMessage("Please enter the item category description  ", 2);
        //        return;
        //    }
        //    string _cat1 = (string)hashtable2["_cat1"];
        //    if (string.IsNullOrEmpty(_cat1))
        //    {
        //        DisplayMessage("Please select thhe main item category ", 2);
        //        return;
        //    }
        //    string _cat2 = (string)hashtable2["_cat2"];
        //    if (string.IsNullOrEmpty(_cat2))
        //    {
        //        DisplayMessage("Please select the sub item category", 2);
        //        return;
        //    }
        //    string _cat3 = (string)hashtable2["_cat3"];
        //    if (string.IsNullOrEmpty(_cat3))
        //    {
        //        DisplayMessage("Please select the item range 1", 2);
        //        return;
        //    }
        //    string _cat4 = (string)hashtable2["_cat4"];
        //    if (string.IsNullOrEmpty(_cat4))
        //    {
        //        DisplayMessage("Please select the item range 2", 2);
        //        return;
        //    }
        //    if (_lstcate5 != null)
        //    {
        //        //REF_ITM_CATE5 result
        //        var result = _lstcate5.SingleOrDefault(x => x.Ric5_cd == txtCate5.Text && x.Ric5_cd1 == _cat1 && x.Ric5_cd2 == _cat2 && x.Ric5_cd3 == _cat3 && x.Ric5_cd4 == _cat4);
        //        if (result != null)
        //        {
        //            //result.Ric5_act = (check_active_cat5.Checked == true) ? true : false;// true; 
        //            //txtCate5.Text = "";
        //            //txtCate5Des.Text = "";
        //            //check_active_cat5.Checked = false;
        //            //grdCate5.DataSource = _lstcate5;
        //            //grdCate5.DataBind();
        //            //ViewState["_lstcate5"] = _lstcate5;
        //            //Session["countlsit"] = "yes";
        //            DisplayMessage("This category already exist", 2);
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        _lstcate5 = new List<REF_ITM_CATE5>();
        //    }
        //    REF_ITM_CATE5 _cate5 = new REF_ITM_CATE5();
        //    _cate5.Ric5_cd = txtCate5.Text;
        //    _cate5.Ric5_desc = txtCate5Des.Text;
        //    _cate5.Ric5_cd1 = _cat1;
        //    _cate5.Ric5_cd2 = _cat2;
        //    _cate5.Ric5_cd3 = _cat3;
        //    _cate5.Ric5_cd4 = _cat4;
        //    _cate5.Ric5_act = (check_active_cat5.Checked == true) ? true : false;// true; 
        //    _cate5.RIC5_CRE_BY = Session["UserID"].ToString();
        //    _cate5.RIC5_MOD_BY = Session["UserID"].ToString();

        //    _lstcate5.Add(_cate5);

        //    grdCate5.DataSource = null;
        //    grdCate5.DataSource = new List<REF_ITM_CATE5>();
        //    grdCate5.DataSource = _lstcate5;
        //    grdCate5.DataBind();
        //    // _cat5 = txtCate5.Text;
        //    txtCate5.Text = "";
        //    txtCate5Des.Text = "";
        //    DisplayMessage("Successfully Added", 3);
        //    hashtable = Session["hashtable"] as Hashtable;
        //    hashtable["_cat5tag"] = 1;
        //    Session["hashtable"] = hashtable;
        //    ViewState["_lstcate5"] = _lstcate5;
        //    Session["countlsit"] = "yes";
        //}

        //protected void labtnSave5_Click(object sender, EventArgs e)
        //{
        //    if (Session["countlsit"].ToString() == "")
        //    {
        //        DisplayMessage("No any modifications or new record to save", 2);
        //        return;
        //    }
        //    hashtable2 = Session["hashtable2"] as Hashtable;
        //    //_lstcate5 = ViewState["_lstcate5"] as List<REF_ITM_CATE5>;
        //    string _cat2 = (string)hashtable2["_cat2"];
        //    string _cat1 = (string)hashtable2["_cat1"];
        //    string _cat3 = (string)hashtable2["_cat3"];
        //    string _cat4 = (string)hashtable2["_cat4"];

        //    foreach (GridViewRow _row in grdCate5.Rows)
        //    {
        //        REF_ITM_CATE5 _cate5 = new REF_ITM_CATE5();
        //        Label code = (Label)_row.FindControl("colmCode_Cate5");
        //        Label dec = (Label)_row.FindControl("colmDes_Cate5");
        //        CheckBox IsDefault = (CheckBox)_row.FindControl("colmStataus_Cate5");

        //        _cate5.Ric5_cd = code.Text;
        //        _cate5.Ric5_desc = dec.Text;
        //        _cate5.Ric5_cd1 = _cat1;
        //        _cate5.Ric5_cd2 = _cat2;
        //        _cate5.Ric5_cd3 = _cat3;
        //        _cate5.Ric5_cd4 = _cat4;
        //        _cate5.Ric5_act = (IsDefault.Checked == true) ? true : false;// true; 
        //        _cate5.RIC5_CRE_BY = Session["UserID"].ToString();
        //        _cate5.RIC5_MOD_BY = Session["UserID"].ToString();
        //        _lstcate5.Add(_cate5);
        //    }
        //    if (_lstcate5 == null)
        //    {
        //        DisplayMessage("Select Item Range 3 to save", 2);
        //        return;

        //    }
        //    if (_lstcate5.Count == 0)
        //    {
        //        DisplayMessage("Select Item Range 3 to save", 2);
        //        return;
        //    }

        //    //if (_cat5tag == 0)
        //    //{
        //    //    MessageBox.Show("No any modificatoin or new record to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    //    return;

        //    //}
        //    _lstcate5del = ViewState["_lstcate5del"] as List<REF_ITM_CATE5>;
        //    if (_lstcate5del == null)
        //    {
        //        _lstcate5del = new List<REF_ITM_CATE5>();
        //    }
        //    string _err;
        //    int row_aff = CHNLSVC.General.SaveItemCate5(_lstcate5, _lstcate5del, out _err);
        //    if (row_aff == 1)
        //    {
        //        DisplayMessage("Successfully Saved", 3);
        //        hashtable = Session["hashtable"] as Hashtable;
        //        hashtable["_cat5tag"] = 0;
        //        Session["hashtable"] = hashtable;

        //    }
        //    else
        //    {
        //        DisplayMessage("Terminate", 2);
        //    }
        //}

        //protected void lbtnClear5_Click(object sender, EventArgs e)
        //{
        //    if (txtClearlconformmessageValue.Value == "Yes")
        //    {
        //        txtCate5.Text = "";
        //        txtCate5Des.Text = "";
        //    }
        //}

        //protected void lbtnDeletecat5_Click(object sender, EventArgs e)
        //{
        //    bool _IsDeleted = false;
        //    _lstcate5 = ViewState["_lstcate5"] as List<REF_ITM_CATE5>;
        //    _lstcate5del = ViewState["_lstcate5del"] as List<REF_ITM_CATE5>;
        //    if (_lstcate5del == null)
        //    {
        //        _lstcate5del = new List<REF_ITM_CATE5>();
        //    }
        //    if (txtDeleteconformmessageValue.Value == "No")
        //    {
        //        return;
        //    }
        //    foreach (GridViewRow _row in grdCate4.Rows)
        //    {
        //        CheckBox checkbox = _row.FindControl("chk_ReqItem_cat5") as CheckBox;
        //        if (checkbox.Checked == true)
        //        {
        //            string type = (_row.FindControl("colmCode_Cate5") as Label).Text;
        //            _lstcate5del.AddRange(_lstcate5.Where(x => x.Ric5_cd == type));
        //            _lstcate5.RemoveAll(x => x.Ric5_cd == type);
        //            grdCate5.DataSource = _lstcate5del;
        //            grdCate5.DataBind();
        //            // _cat3tag = 1;

        //            ViewState["_lstcate5"] = _lstcate5;
        //            ViewState["_lstcate5del"] = _lstcate5del;
        //            hashtable = Session["hashtable"] as Hashtable;
        //            hashtable["_cat5tag"] = 1;
        //            Session["hashtable"] = hashtable;
        //            _IsDeleted = true;
        //            Session["countlsit"] = "true";
        //        }
        //    }
        //    if (_IsDeleted == false)
        //    {
        //        DisplayMessage("Please select the Item Range 3 to delete", 2);
        //    }
        //}

        //protected void colmStataus_Cate5_Click(object sender, EventArgs e)
        //{
        //    Session["countlsit"] = "yes";
        //}
        //#endregion


        protected void lbtnCate2_Asset_Click(object sender, EventArgs e)
        {
            try
            {
                if (AssetCat01TextBox.Text.Equals(string.Empty))
                {
                    DisplayMessage("Please Select Categor01", 2);
                    return;
                }

                Session["_IsCat"] = "false";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AssetCat2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "assetcat02";
                UserPopoup.Show();
            }

            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }
        protected void lbtndepDelete_Click(object sender, EventArgs e)
        {
            if (txtDeleteconformmessageValue.Value == "No")
            {
                return;
            }
            if (GridViewAssetParameters.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string cat01 = (row.FindControl("lbl_map_cat1") as Label).Text;
                string cat02 = (row.FindControl("lbl_map_cat2") as Label).Text;
                string status = (row.FindControl("lbl_map_stus") as Label).Text;

                MasterAssetParameter mstpara = new MasterAssetParameter();
                mstpara.map_com = Session["UserCompanyCode"].ToString();
                mstpara.map_cat1 = cat01;
                mstpara.map_cat2 = cat02;
                // mstpara.map_dep_mth = dr["map_dep_mth"].ToString();
                ///  mstpara.map_dep_val = Convert.ToInt32(dr["map_dep_val"].ToString());
                //  mstpara.map_dep_base = dr["map_dep_base"].ToString();
                //  mstpara.map_stus =  dr["map_stus"].ToString();
                //  mstpara.map_dis =  Convert.ToInt16(dr["map_dis"].ToString());
                //  mstpara.map_cre_by = Session["UserID"].ToString();
                //   mstpara.map_cre_dt = DateTime.Now;
                mstpara.map_act = 0;
                mstpara.map_stus = status;
                mstpara.map_mod_by = Session["UserID"].ToString();
                mstpara.map_mod_dt = DateTime.Now;
                Int32 saved = CHNLSVC.Inventory.UpdateMasterAssetPrameter(mstpara);

                DisplayMessage("Parameter is deleted!", 3);
                CHNLSVC.Inventory.GetAssetParameters(Session["UserCompanyCode"].ToString());

                DataTable _assetParameters = CHNLSVC.Inventory.GetAssetParameters(Session["UserCompanyCode"].ToString());
                GridViewAssetParameters.DataSource = _assetParameters;
                GridViewAssetParameters.DataBind();
                Session["assetParameterTable"] = _assetParameters;
            }
        }
        protected void txtCate2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCate2.Text))
            {
                MasterItemSubCate subCate = CHNLSVC.Sales.GetItemSubCate(txtCate2.Text);
                if (subCate != null)
                {
                    txtCate2Des.Text = subCate.Ric2_desc;
                }
            }
        }
        protected void AssetTextCate2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(AssetCat02TextBox.Text))
            {
                MasterItemSubCate subCate = CHNLSVC.Sales.GetItemSubCate(AssetCat02TextBox.Text.ToUpper());
                if (subCate.Ric2_cd != null)
                {
                    AssetCat02DesTextBox.Text = subCate.Ric2_desc;
                }
                else
                {
                    DisplayMessage("Subcategory is not valid", 2);
                    AssetCat02DesTextBox.Text = string.Empty;
                }
            }
            else
            {
                AssetCat02DesTextBox.Text = string.Empty;
            }
        }

        protected void lbtnAddCate2_Click(object sender, EventArgs e)
        {

            Session["countlsit"] = "";
            _lstcate2 = ViewState["_lstcate2"] as List<MasterItemSubCate>;
            hashtable2 = Session["hashtable2"] as Hashtable;
            if (string.IsNullOrEmpty(txtCate2.Text))
            {
                DisplayMessage("Please Enter Item Category", 2);
                return;
            }

            if (string.IsNullOrEmpty(txtCate2Des.Text))
            {
                DisplayMessage("Please Enter Item Category Description", 2);
                return;
            }

            string _cat1 = (string)hashtable2["_cat1"];
            if (string.IsNullOrEmpty(_cat1))
            {
                DisplayMessage("Please Select Main Item Category", 2);
                return;
            }
            if (_lstcate2 != null)
            {
                //MasterItemSubCate resul
                var result = _lstcate2.SingleOrDefault(x => x.Ric2_cd == txtCate2.Text && x.Ric2_cd1 == _cat1);
                if (result != null)
                {
                    //// hashtable2 = Session["hashtable2"] as Hashtable;
                    // result.Ric2_act = (check_active_cat2.Checked == true) ? true : false;// true;        
                    // grdCate2.DataSource = _lstcate2;
                    // grdCate2.DataBind();
                    // txtCate2.Text = "";
                    // txtCate2Des.Text = "";
                    // check_active_cat2.Checked = false;
                    // ViewState["_lstcate2"] = _lstcate2;

                    // Session["countlsit"] = "yes";
                    // //_cat2 = txtCate2.Text;
                    DisplayMessage("This category already exist", 2);
                    return;
                }
            }

            else
            {
                _lstcate2 = new List<MasterItemSubCate>();
            }

            MasterItemSubCate _cate2 = new MasterItemSubCate();
            _cate2.Ric2_cd = txtCate2.Text;
            _cate2.Ric2_desc = txtCate2Des.Text;
            _cate2.Ric2_cd1 = _cat1;
            _cate2.Ric2_act = (check_active_cat2.Checked == true) ? true : false;// true;       //true;
            _cate2.RIC2_CRE_BY = Session["UserID"].ToString();
            _cate2.RIC2_MOD_BY = Session["UserID"].ToString();

            _lstcate2.Add(_cate2);

            grdCate2.DataSource = null;
            grdCate2.DataSource = new List<MasterItemSubCate>();
            grdCate2.DataSource = _lstcate2;
            grdCate2.DataBind();
            ViewState["_lstcate2"] = _lstcate2;
            hashtable2["_cat2"] = txtCate2.Text;
            Session["hashtable2"] = hashtable2;
            // _cat2 = txtCate2.Text;
            txtCate2.Text = "";
            txtCate2Des.Text = "";
            DisplayMessage("Successfully Added", 3);
            hashtable = Session["hashtable"] as Hashtable;
            hashtable["_cat2tag"] = 1;
            Session["hashtable"] = hashtable;

            Session["countlsit"] = "yes";
        }

        protected void lbtnClear2_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "Yes")
            {
                txtCate2.Text = "";
                txtCate2Des.Text = "";
            }

        }

        protected void lbtnSave2_Click(object sender, EventArgs e)
        {
            if (Session["countlsit"].ToString() == "")
            {
                DisplayMessage("No any modifications or new record to save", 2);
                return;
            }
            hashtable2 = Session["hashtable2"] as Hashtable;
            //_lstcate2 = ViewState["_lstcate2"] as List<MasterItemSubCate>;
            string _cat1 = (string)hashtable2["_cat1"];

            foreach (GridViewRow _row in grdCate2.Rows)
            {
                MasterItemSubCate _cate2 = new MasterItemSubCate();
                Label code = (Label)_row.FindControl("colmCode_Cate2");
                Label dec = (Label)_row.FindControl("colmDes_Cate2");
                CheckBox IsDefault = (CheckBox)_row.FindControl("colmStataus_Cate2");
                _cate2.Ric2_cd = code.Text;
                _cate2.Ric2_desc = dec.Text;
                _cate2.Ric2_cd1 = _cat1;
                _cate2.Ric2_act = (IsDefault.Checked == true) ? true : false;// true;
                _cate2.RIC2_CRE_BY = Session["UserID"].ToString();
                _cate2.RIC2_MOD_BY = Session["UserID"].ToString();
                _lstcate2.Add(_cate2);

            }
            if (_lstcate2 == null)
            {
                DisplayMessage("Please Select sub items to save", 2);
                return;

            }


            if (_lstcate2.Count == 0)
            {
                DisplayMessage("Please Select sub items to save", 2);
                return;
            }


            //if (_cat2tag == 0)
            //{
            //    MessageBox.Show("No any modificatoin or new record to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;

            //}
            _lstcate2del = ViewState["_lstcate2del"] as List<MasterItemSubCate>;
            if (_lstcate2del == null)
            {
                _lstcate2del = new List<MasterItemSubCate>();
            }
            string _err;
            int row_aff = CHNLSVC.General.SaveItemCate2(_lstcate2, _lstcate2del, out _err);
            if (row_aff == 1)
            {
                DisplayMessage("Successfully Saved", 3);
                // _cat1tag.Value = "0";
                hashtable = Session["hashtable"] as Hashtable;
                hashtable["_cat2tag"] = 0;
                Session["hashtable"] = hashtable;

            }
            else
            {
                DisplayMessage("Terminate", 2);

            }
        }
        protected void lbtnSaveDep_Click(object sender, EventArgs e)
        {
            Int32 checkexists = 0;
            DataTable dt = Session["assetParameterTable"] as DataTable;
            if (dt == null) { DisplayMessage("No any modifications to save", 4); return; }
            foreach (DataRow dr in dt.Rows)
            {
                MasterAssetParameter mstpara = new MasterAssetParameter();
                mstpara.map_com = Session["UserCompanyCode"].ToString();
                mstpara.map_cat1 = dr["map_cat1"].ToString();
                mstpara.map_cat2 = dr["map_cat2"].ToString();
                mstpara.map_dep_mth = dr["map_dep_mth"].ToString();
                mstpara.map_dep_val = Convert.ToDecimal(dr["map_dep_val"].ToString());
                mstpara.map_dep_base = dr["map_dep_base"].ToString();
                mstpara.map_stus = dr["map_stus"].ToString();
                mstpara.map_dis = Convert.ToInt16(dr["map_dis"].ToString());
                mstpara.map_cre_by = Session["UserID"].ToString();
                mstpara.map_cre_dt = DateTime.Now;
                mstpara.map_act = 1;
                mstpara.map_mod_by = Session["UserID"].ToString();
                mstpara.map_mod_dt = DateTime.Now;
                mstpara.map_session = Session["SessionID"].ToString();

                //Check Params 
                DataTable assets = CHNLSVC.Inventory.Check_Already_savedMethods(mstpara.map_cat1, mstpara.map_cat2, mstpara.map_stus);
                //
                if (assets.Rows.Count < 1)
                {
                    Int32 saved = CHNLSVC.Inventory.SaveMasterAssetPrameter(mstpara);
                    if (saved > 0)
                    {
                        //DisplayMessage("Asset Parameters successfully saved!", 3);
                        checkexists++;
                    }
                    else
                    {
                        DisplayMessage("Asset parameters are not saved!", 4);
                    }
                }


            }
            if (dt.Rows.Count < 1)
            {
                DisplayMessage("No Parameters to save!", 4);
            }
            else
            {
                if (checkexists > 0)
                {
                    DisplayMessage("Asset Parameters successfully saved!", 3);
                }
                else
                {
                    DisplayMessage("No any modification to save!", 2);
                }
            }

        }
        protected void lbtnCate2_Click(object sender, EventArgs e)
        {
            try
            {
                Session["_IsCat"] = "false";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Cate2";
                UserPopoup.Show();
            }

            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnDeletecat2_Click(object sender, EventArgs e)
        {
            bool _IsDeleted = false;
            _lstcate2 = ViewState["_lstcate2"] as List<MasterItemSubCate>;
            _lstcate2del = ViewState["_lstcate2del"] as List<MasterItemSubCate>;
            if (txtDeleteconformmessageValue.Value == "No")
            {
                return;
            }
            if (_lstcate2del == null)
            {
                _lstcate2del = new List<MasterItemSubCate>();
            }
            foreach (GridViewRow _row in grdCate2.Rows)
            {
                CheckBox checkbox = _row.FindControl("chk_ReqItem_cat2") as CheckBox;
                if (checkbox.Checked == true)
                {
                    string type = (_row.FindControl("colmCode_Cate2") as Label).Text;


                    _lstcate2del.AddRange(_lstcate2.Where(x => x.Ric2_cd == type));
                    _lstcate2.RemoveAll(x => x.Ric2_cd == type);

                    grdCate2.DataSource = _lstcate2;
                    grdCate2.DataBind();

                    ViewState["_lstcate2"] = _lstcate2;
                    ViewState["_lstcate2del"] = _lstcate2del;
                    hashtable = Session["hashtable"] as Hashtable;
                    hashtable["_cat2tag"] = 1;
                    Session["hashtable"] = hashtable;
                    _IsDeleted = true;
                    Session["countlsit"] = "true";
                }
            }
            if (_IsDeleted == false)
            {
                DisplayMessage("Please select the sub category to delete", 2);
            }
        }

        protected void lbtnCat2Select_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grdCate2.Rows.Count; i++)
            {
                grdCate2.Rows[i].BackColor = System.Drawing.Color.White;
            }
            if (grdCate2.Rows.Count == 0) return;

            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                CheckBox _itemCode = (row.FindControl("colmStataus_Cate2") as CheckBox);
                if (_itemCode.Checked == false)
                {
                    DisplayMessage("The code is inactive", 2);
                    return;
                }
                row.BackColor = System.Drawing.Color.LightCyan;
                hashtable2 = Session["hashtable2"] as Hashtable;
                string _cat1 = (string)hashtable2["_cat1"];

                string _cat2 = (row.FindControl("colmCode_Cate2") as Label).Text;
                _lstcate3 = CHNLSVC.General.GetItemCate3(_cat1, _cat2);

                grdCate3.DataSource = null;
                grdCate3.DataSource = new List<REF_ITM_CATE3>();
                grdCate3.DataSource = _lstcate3;
                grdCate3.DataBind();
                hashtable2["_cat2"] = _cat2;
                hashtable2["_cat3"] = "";
                hashtable2["_cat4"] = "";
                ViewState["_lstcate3"] = _lstcate3;
                Session["hashtable2"] = hashtable2;
                grdCate3.Visible = true;
                lbCate3no.Visible = false;
            }
        }


        protected void colmStataus_Cate2_Click(object sender, EventArgs e)
        {
            Session["countlsit"] = "yes";
        }
        #endregion

        #region Item Range 1
        protected void lbtntCate3_Click(object sender, EventArgs e)
        {
            try
            {
                Session["_IsCat"] = "false";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat3);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Cate3";
                UserPopoup.Show();
                Session["_IsCat"] = "false";
            }

            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtCate3_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCate3.Text))
            {
                REF_ITM_CATE3 subCate = CHNLSVC.General.GetItemCategory3(txtCate3.Text);
                if (subCate != null)
                {
                    txtCate3Des.Text = subCate.Ric2_desc;
                }
            }
        }

        //protected void labtnSave3_Click(object sender, EventArgs e)
        //{
        //    if (Session["countlsit"].ToString() == "")
        //    {
        //        DisplayMessage("No any modifications or new record to save", 2);
        //        return;
        //    }
        //    hashtable2 = Session["hashtable2"] as Hashtable;
        //    //    _lstcate3 = ViewState["_lstcate3"] as List<REF_ITM_CATE3>;
        //    string _cat2 = (string)hashtable2["_cat2"];
        //    string _cat1 = (string)hashtable2["_cat1"];

        //    foreach (GridViewRow _row in grdCate3.Rows)
        //    {
        //        REF_ITM_CATE3 _cate3 = new REF_ITM_CATE3();
        //        Label code = (Label)_row.FindControl("colmCode_Cate3");
        //        Label dec = (Label)_row.FindControl("colmDes_Cate3");
        //        CheckBox IsDefault = (CheckBox)_row.FindControl("colmStataus_Cate3");

        //        _cate3.Ric3_cd = code.Text;
        //        _cate3.Ric2_desc = dec.Text;
        //        _cate3.Ric3_cd1 = _cat1;
        //        _cate3.Ric3_cd2 = _cat2;
        //        _cate3.Ric2_act = (IsDefault.Checked == true) ? true : false;// true;       //true;
        //        _cate3.RIC3_CRE_BY = Session["UserID"].ToString();
        //        _cate3.RIC3_MOD_BY = Session["UserID"].ToString();
        //        _lstcate3.Add(_cate3);
        //    }
        //    if (_lstcate3 == null)
        //    {
        //        DisplayMessage("selct Item Range 1 to save", 2);
        //        return;

        //    }

        //    if (_lstcate3.Count == 0)
        //    {
        //        DisplayMessage("Select Item Range 1 to save", 2);
        //        return;
        //    }
        //    if (!string.IsNullOrEmpty(txtCate3.Text))
        //    {
        //        DisplayMessage("Please add item range 1", 2);
        //        return;
        //    }

        //    if (!string.IsNullOrEmpty(txtCate3Des.Text))
        //    {
        //        DisplayMessage("Please add item range 1 Description", 2);
        //        return;
        //    }
        //    //if (_cat3tag == 0)
        //    //{
        //    //    MessageBox.Show("No any modificatoin or new record to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    //    return;

        //    //}
        //    _lstcate3del = ViewState["_lstcate3del"] as List<REF_ITM_CATE3>;
        //    if (_lstcate3del == null)
        //    {
        //        _lstcate3del = new List<REF_ITM_CATE3>();
        //    }
        //    string _err;
        //    int row_aff = CHNLSVC.General.SaveItemCate3(_lstcate3, _lstcate3del, out _err);
        //    if (row_aff == 1)
        //    {
        //        DisplayMessage("Successfully Saved", 3);
        //        hashtable = Session["hashtable"] as Hashtable;
        //        hashtable["_cat3tag"] = 0;
        //        Session["hashtable"] = hashtable;
        //        //_cat3tag = 0;
        //    }
        //    else
        //    {
        //        DisplayMessage("Terminate", 2);

        //    }
        //}

        //protected void lbtnClear3_Click(object sender, EventArgs e)
        //{
        //    if (txtClearlconformmessageValue.Value == "Yes")
        //    {
        //        txtCate3.Text = "";
        //        txtCate3Des.Text = "";
        //    }
        //}

        //protected void lbtnAddCate3_Click(object sender, EventArgs e)
        //{

        //    Session["countlsit"] = "";
        //    _lstcate3 = ViewState["_lstcate3"] as List<REF_ITM_CATE3>;
        //    hashtable2 = Session["hashtable2"] as Hashtable;
        //    if (string.IsNullOrEmpty(txtCate3.Text))
        //    {
        //        DisplayMessage("Please Enter Item Category", 2);
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(txtCate3Des.Text))
        //    {
        //        DisplayMessage("Please Enter Item Category Description", 2);
        //        return;
        //    }
        //    string _cat1 = (string)hashtable2["_cat1"];
        //    if (string.IsNullOrEmpty(_cat1))
        //    {
        //        DisplayMessage("Please Select Main Item Category", 2);
        //        return;
        //    }
        //    string _cat2 = (string)hashtable2["_cat2"];
        //    if (string.IsNullOrEmpty(_cat2))
        //    {
        //        DisplayMessage("Please Select Sub Item Category", 2);
        //        return;
        //    }
        //    if (_lstcate3 != null)
        //    {
        //        //REF_ITM_CATE3 result
        //        var result = _lstcate3.SingleOrDefault(x => x.Ric3_cd == txtCate3.Text && x.Ric3_cd1 == _cat1 && x.Ric3_cd2 == _cat2);
        //        if (result != null)
        //        {
        //            // result.Ric2_act=(check_active_cat3.Checked == true) ? true : false;// true;       
        //            //// hashtable2["_cat3"] = txtCate3.Text;
        //            //// Session["hashtable2"] = hashtable2;
        //            // //_cat2 = txtCate2.Text;
        //            // grdCate3.DataSource = _lstcate3;
        //            // grdCate3.DataBind();
        //            // txtCate3.Text = "";
        //            // txtCate3Des.Text = "";
        //            // check_active_cat3.Checked = false;
        //            // ViewState["_lstcate3"] = _lstcate3;

        //            // Session["countlsit"] = "yes";
        //            DisplayMessage("This category already exist", 2);
        //            return;
        //        }
        //    }

        //    else
        //    {
        //        _lstcate3 = new List<REF_ITM_CATE3>();
        //    }

        //    REF_ITM_CATE3 _cate3 = new REF_ITM_CATE3();
        //    _cate3.Ric3_cd = txtCate3.Text;
        //    _cate3.Ric2_desc = txtCate3Des.Text;
        //    _cate3.Ric3_cd1 = _cat1;
        //    _cate3.Ric3_cd2 = _cat2;
        //    _cate3.Ric2_act = (check_active_cat3.Checked == true) ? true : false;// true;       //true;
        //    _cate3.RIC3_CRE_BY = Session["UserID"].ToString();
        //    _cate3.RIC3_MOD_BY = Session["UserID"].ToString();
        //    _lstcate3.Add(_cate3);

        //    grdCate3.DataSource = null;
        //    grdCate3.DataSource = new List<REF_ITM_CATE3>();
        //    grdCate3.DataSource = _lstcate3;
        //    grdCate3.DataBind();
        //    ViewState["_lstcate3"] = _lstcate3;
        //    // _cat3 = txtCate3.Text;
        //    hashtable2["_cat3"] = txtCate3.Text;
        //    Session["hashtable2"] = hashtable2;

        //    txtCate3.Text = "";
        //    txtCate3Des.Text = "";
        //    DisplayMessage("Successfully Added", 3);
        //    hashtable = Session["hashtable"] as Hashtable;
        //    hashtable["_cat3tag"] = 1;
        //    Session["hashtable"] = hashtable;

        //    Session["countlsit"] = "yes";
        //}

        //protected void lbtnDeletecat3_Click(object sender, EventArgs e)
        //{
        //    bool _IsDeleted = false;
        //    _lstcate3 = ViewState["_lstcate3"] as List<REF_ITM_CATE3>;
        //    _lstcate3del = ViewState["_lstcate3del"] as List<REF_ITM_CATE3>;
        //    if (_lstcate3del == null)
        //    {
        //        _lstcate3del = new List<REF_ITM_CATE3>();
        //    }
        //    if (txtDeleteconformmessageValue.Value == "No")
        //    {
        //        return;
        //    }
        //    foreach (GridViewRow _row in grdCate3.Rows)
        //    {
        //        CheckBox checkbox = _row.FindControl("chk_ReqItem_cat3") as CheckBox;
        //        if (checkbox.Checked == true)
        //        {
        //            string type = (_row.FindControl("colmCode_Cate3") as Label).Text;
        //            _lstcate3del.AddRange(_lstcate3.Where(x => x.Ric3_cd == type));
        //            _lstcate3.RemoveAll(x => x.Ric3_cd == type);
        //            grdCate3.DataSource = _lstcate3;
        //            grdCate3.DataBind();
        //            // _cat3tag = 1;

        //            ViewState["_lstcate3"] = _lstcate3;
        //            ViewState["_lstcate3del"] = _lstcate3del;
        //            hashtable = Session["hashtable"] as Hashtable;
        //            hashtable["_cat3tag"] = 1;
        //            Session["hashtable"] = hashtable;
        //            _IsDeleted = true;
        //            Session["countlsit"] = "true";
        //        }
        //    }
        //    if (_IsDeleted == false)
        //    {
        //        DisplayMessage("Please select the Item Range 1 to delete", 2);
        //    }
        //}

        //protected void lbtnCat3Select_Click(object sender, EventArgs e)
        //{
        //    for (int i = 0; i < grdCate3.Rows.Count; i++)
        //    {
        //        grdCate3.Rows[i].BackColor = System.Drawing.Color.White;
        //    }
        //    if (grdCate3.Rows.Count == 0) return;

        //    var lb = (LinkButton)sender;
        //    var row = (GridViewRow)lb.NamingContainer;
        //    if (row != null)
        //    {
        //        CheckBox _itemCode = (row.FindControl("colmStataus_Cate3") as CheckBox);
        //        if (_itemCode.Checked == false)
        //        {
        //            DisplayMessage("The code is inactive", 2);
        //            return;
        //        }
        //        row.BackColor = System.Drawing.Color.LightCyan;
        //        hashtable2 = Session["hashtable2"] as Hashtable;
        //        string _cat1 = (string)hashtable2["_cat1"];
        //        string _cat2 = (string)hashtable2["_cat2"];
        //        string _cat3 = (row.FindControl("colmCode_Cate3") as Label).Text;
        //        _lstcate4 = CHNLSVC.General.GetItemCate4(_cat1, _cat2, _cat3);

        //        grdCate4.DataSource = null;
        //        grdCate4.DataSource = new List<REF_ITM_CATE4>();
        //        grdCate4.DataSource = _lstcate4;
        //        grdCate4.DataBind();
        //        hashtable2["_cat3"] = _cat3;
        //        hashtable2["_cat4"] = "";
        //        ViewState["_lstcate4"] = _lstcate4;
        //        Session["hashtable2"] = hashtable2;
        //        grdCate4.Visible = true;
        //        lbCate4no.Visible = false;
        //    }
        //}

        protected void colmStataus_Cate3_Click(object sender, EventArgs e)
        {
            Session["countlsit"] = "yes";
        }
        #endregion

        //#region Item Range 2
        //protected void txtCate4_TextChanged(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txtCate4.Text))
        //    {
        //        REF_ITM_CATE4 subCate = CHNLSVC.General.GetItemCategory4(txtCate4.Text);
        //        if (subCate != null)
        //        {
        //            txtCate4Des.Text = subCate.Ric4_desc;
        //        }
        //    }
        //}

        //protected void lbtnCate4_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Session["_IsCat"] = "false";
        //        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat4);
        //        DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
        //        grdResult.DataSource = _result;
        //        grdResult.DataBind();
        //        BindUCtrlDDLData(_result);
        //        lblvalue.Text = "Cate4";
        //        UserPopoup.Show();
        //    }

        //    catch (Exception ex)
        //    {
        //        string _Msg = "Error Occurred while processing search customer";
        //        DisplayMessage(_Msg, 4);
        //    }
        //}

        ////protected void lbtnAddCate4_Click(object sender, EventArgs e)
        ////{
        ////    Session["countlsit"] = "";
        ////    _lstcate4 = ViewState["_lstcate4"] as List<REF_ITM_CATE4>;
        ////    hashtable2 = Session["hashtable2"] as Hashtable;
        ////    if (string.IsNullOrEmpty(txtCate4.Text))
        ////    {
        ////        DisplayMessage("Please Enter The Item Category ", 2);
        ////        return;
        ////    }

        ////    if (string.IsNullOrEmpty(txtCate4Des.Text))
        ////    {
        ////        DisplayMessage("Please Enter The Item Category Description  ", 2);
        ////        return;
        ////    }
        ////    string _cat1 = (string)hashtable2["_cat1"];
        ////    if (string.IsNullOrEmpty(_cat1))
        ////    {
        ////        DisplayMessage("Please Select The Main Item Category ", 2);
        ////        return;
        ////    }
        ////    string _cat2 = (string)hashtable2["_cat2"];
        ////    if (string.IsNullOrEmpty(_cat2))
        ////    {
        ////        DisplayMessage("Please Select The Sub Item Category", 2);
        ////        return;
        ////    }
        ////    string _cat3 = (string)hashtable2["_cat3"];
        ////    if (string.IsNullOrEmpty(_cat3))
        ////    {
        ////        DisplayMessage("Please Select Item Range 1", 2);
        ////        return;
        ////    }

        ////    if (_lstcate4 != null)
        ////    {
        ////        //REF_ITM_CATE4 result 
        ////        var result = _lstcate4.SingleOrDefault(x => x.Ric4_cd == txtCate4.Text && x.Ric4_cd1 == _cat1 && x.Ric4_cd2 == _cat2 && x.Ric4_cd3 == _cat3);
        ////        if (result != null)
        ////        {
        ////            // result.Ric4_act = (check_active_cat4.Checked == true) ? true : false;// true;       
        ////            //// _cat4 = txtCate4.Text;
        ////            // grdCate4.DataSource = _lstcate4;
        ////            // grdCate4.DataBind();
        ////            // check_active_cat4.Checked = false;
        ////            // txtCate4.Text = "";
        ////            // txtCate4Des.Text = "";
        ////            // ViewState["_lstcate4"] = _lstcate4;
        ////            // Session["countlsit"] = "yes";
        ////            DisplayMessage("This category already exist", 2);
        ////            return;
        ////        }
        ////    }
        ////    else
        ////    {
        ////        _lstcate4 = new List<REF_ITM_CATE4>();
        ////    }

        ////    REF_ITM_CATE4 _cate4 = new REF_ITM_CATE4();
        ////    _cate4.Ric4_cd = txtCate4.Text;
        ////    _cate4.Ric4_desc = txtCate4Des.Text;
        ////    _cate4.Ric4_cd1 = _cat1;
        ////    _cate4.Ric4_cd2 = _cat2;
        ////    _cate4.Ric4_cd3 = _cat3;
        ////    _cate4.Ric4_act = (check_active_cat4.Checked == true) ? true : false;// true;  
        ////    _cate4.RIC4_CRE_BY = Session["UserID"].ToString();
        ////    _cate4.RIC4_MOD_BY = Session["UserID"].ToString();
        ////    _lstcate4.Add(_cate4);

        ////    grdCate4.DataSource = null;
        ////    grdCate4.DataSource = new List<REF_ITM_CATE4>();
        ////    grdCate4.DataSource = _lstcate4;
        ////    grdCate4.DataBind();
        ////    hashtable2["_cat4"] = txtCate4.Text;
        ////    Session["hashtable2"] = hashtable2;

        ////    // _cat4 = txtCate4.Text;
        ////    txtCate4.Text = "";
        ////    txtCate4Des.Text = "";

        ////    DisplayMessage("Successfully Added", 3);
        ////    hashtable = Session["hashtable"] as Hashtable;
        ////    hashtable["_cat4tag"] = 1;
        ////    Session["hashtable"] = hashtable;
        ////    ViewState["_lstcate4"] = _lstcate4;
        ////    //_cat4tag = 1;
        ////    Session["countlsit"] = "yes";
        ////}

        ////protected void labtnSave4_Click(object sender, EventArgs e)
        ////{
        ////    if (Session["countlsit"].ToString() == "")
        ////    {
        ////        DisplayMessage("No any modifications or new record to save", 2);
        ////        return;
        ////    }
        ////    hashtable2 = Session["hashtable2"] as Hashtable;
        ////    //_lstcate4 = ViewState["_lstcate4"] as List<REF_ITM_CATE4>;
        ////    string _cat2 = (string)hashtable2["_cat2"];
        ////    string _cat1 = (string)hashtable2["_cat1"];
        ////    string _cat3 = (string)hashtable2["_cat3"];
        ////    MasterItemSubCate _cate2 = new MasterItemSubCate();
        ////    foreach (GridViewRow _row in grdCate4.Rows)
        ////    {

        ////        Label code = (Label)_row.FindControl("colmCode_Cate4");
        ////        Label dec = (Label)_row.FindControl("colmDes_Cate4");
        ////        CheckBox IsDefault = (CheckBox)_row.FindControl("colmStataus_Cate4");

        ////        REF_ITM_CATE4 _cate4 = new REF_ITM_CATE4();
        ////        _cate4.Ric4_cd = code.Text;
        ////        _cate4.Ric4_desc = dec.Text;
        ////        _cate4.Ric4_cd1 = _cat1;
        ////        _cate4.Ric4_cd2 = _cat2;
        ////        _cate4.Ric4_cd3 = _cat3;
        ////        _cate4.Ric4_act = (IsDefault.Checked == true) ? true : false;// true; 
        ////        _cate4.RIC4_CRE_BY = Session["UserID"].ToString();
        ////        _cate4.RIC4_MOD_BY = Session["UserID"].ToString();
        ////        _lstcate4.Add(_cate4);
        ////    }
        ////    if (_lstcate4 == null)
        ////    {
        ////        DisplayMessage("Select Item Range 2 to save", 2);
        ////        return;

        ////    }
        ////    if (_lstcate4.Count == 0)
        ////    {
        ////        DisplayMessage("Select Item Range 2 to save", 2);
        ////        return;

        ////    }
        ////    if (!string.IsNullOrEmpty(txtCate4.Text))
        ////    {
        ////        DisplayMessage("Please add item range 2", 2);
        ////        return;
        ////    }

        ////    if (!string.IsNullOrEmpty(txtCate4Des.Text))
        ////    {
        ////        DisplayMessage("Please add item range 2Description", 2);
        ////        return;
        ////    }
        ////    //if (_cat4tag == 0)
        ////    //{
        ////    //    MessageBox.Show("No any modificatoin or new record to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
        ////    //    return;

        ////    //}
        ////    _lstcate4del = ViewState["_lstcate4del"] as List<REF_ITM_CATE4>;
        ////    if (_lstcate4del == null)
        ////    {
        ////        _lstcate4del = new List<REF_ITM_CATE4>();
        ////    }
        ////    string _err;
        ////    int row_aff = CHNLSVC.General.SaveItemCate4(_lstcate4, _lstcate4del, out _err);
        ////    if (row_aff == 1)
        ////    {
        ////        DisplayMessage("Successfully Saved", 3);
        ////        hashtable = Session["hashtable"] as Hashtable;
        ////        hashtable["_cat4tag"] = 0;
        ////        Session["hashtable"] = hashtable;

        ////    }
        ////    else
        ////    {
        ////        DisplayMessage("Terminate", 2);
        ////    }
        ////}

        ////protected void lbtnDeletecat4_Click(object sender, EventArgs e)
        ////{
        ////    bool _IsDeleted = false;
        ////    _lstcate4 = ViewState["_lstcate4"] as List<REF_ITM_CATE4>;
        ////    _lstcate4del = ViewState["_lstcate4del"] as List<REF_ITM_CATE4>;
        ////    if (_lstcate4del == null)
        ////    {
        ////        _lstcate4del = new List<REF_ITM_CATE4>();
        ////    }
        ////    if (txtDeleteconformmessageValue.Value == "No")
        ////    {
        ////        return;
        ////    }
        ////    foreach (GridViewRow _row in grdCate4.Rows)
        ////    {
        ////        CheckBox checkbox = _row.FindControl("chk_ReqItem_cat4") as CheckBox;
        ////        if (checkbox.Checked == true)
        ////        {
        ////            string type = (_row.FindControl("colmCode_Cate4") as Label).Text;
        ////            _lstcate4del.AddRange(_lstcate4.Where(x => x.Ric4_cd == type));

        ////            _lstcate4.RemoveAll(x => x.Ric4_cd == type);
        ////            grdCate4.DataSource = _lstcate4;
        ////            grdCate4.DataBind();
        ////            // _cat3tag = 1;

        ////            ViewState["_lstcate4"] = _lstcate4;
        ////            ViewState["_lstcate4del"] = _lstcate4del;
        ////            hashtable = Session["hashtable"] as Hashtable;
        ////            hashtable["_cat4tag"] = 1;
        ////            Session["hashtable"] = hashtable;
        ////            _IsDeleted = true;
        ////            Session["countlsit"] = "true";
        ////        }
        ////    }
        ////    if (_IsDeleted == false)
        ////    {
        ////        DisplayMessage("Please select the Item Range 2 to delete", 2);
        ////    }
        ////}

        ////protected void lbtnClear4_Click(object sender, EventArgs e)
        ////{
        ////    if (txtClearlconformmessageValue.Value == "Yes")
        ////    {
        ////        txtCate4.Text = "";
        ////        txtCate4Des.Text = "";
        ////    }
        ////}
        ////protected void colmStataus_Cate4_Click(object sender, EventArgs e)
        ////{
        ////    Session["countlsit"] = "yes";
        ////}
        ////protected void lbtnCat4Select_Click(object sender, EventArgs e)
        ////{
        ////    for (int i = 0; i < grdCate4.Rows.Count; i++)
        ////    {
        ////        grdCate4.Rows[i].BackColor = System.Drawing.Color.White;
        ////    }
        ////    if (grdCate4.Rows.Count == 0) return;

        ////    var lb = (LinkButton)sender;
        ////    var row = (GridViewRow)lb.NamingContainer;
        ////    if (row != null)
        ////    {
        ////        CheckBox _itemCode = (row.FindControl("colmStataus_Cate4") as CheckBox);
        ////        if (_itemCode.Checked == false)
        ////        {
        ////            DisplayMessage("The code is inactive", 2);
        ////            return;
        ////        }
        ////        row.BackColor = System.Drawing.Color.LightCyan;
        ////        hashtable2 = Session["hashtable2"] as Hashtable;
        ////        string _cat1 = (string)hashtable2["_cat1"];
        ////        string _cat2 = (string)hashtable2["_cat2"];
        ////        string _cat3 = (string)hashtable2["_cat3"];
        ////        string _cat4 = (row.FindControl("colmCode_Cate4") as Label).Text;
        ////        _lstcate5 = CHNLSVC.General.GetItemCate5(_cat1, _cat2, _cat3, _cat4);

        ////        grdCate5.DataSource = null;
        ////        grdCate5.DataSource = new List<REF_ITM_CATE5>();
        ////        grdCate5.DataSource = _lstcate5;
        ////        grdCate5.DataBind();
        ////        hashtable2["_cat4"] = _cat4;
        ////        ViewState["_lstcate5"] = _lstcate5;
        ////        Session["hashtable2"] = hashtable2;
        ////        grdCate5.Visible = true;
        ////        lbCate5no.Visible = false;
        ////    }
        ////}

        //#endregion

        //#region Item Range 3
        //protected void txtCate5_TextChanged(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txtCate5.Text))
        //    {
        //        REF_ITM_CATE5 subCate = CHNLSVC.General.GetItemCategory5(txtCate5.Text);
        //        if (subCate != null)
        //        {
        //            txtCate5Des.Text = subCate.Ric5_desc;
        //        }
        //    }
        //}



        //protected void lbtnAddCate5_Click(object sender, EventArgs e)
        //{
        //    Session["countlsit"] = "";
        //    _lstcate5 = ViewState["_lstcate5"] as List<REF_ITM_CATE5>;
        //    hashtable2 = Session["hashtable2"] as Hashtable;
        //    if (string.IsNullOrEmpty(txtCate5.Text))
        //    {
        //        DisplayMessage("Please Enter the Item Category", 2);
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(txtCate5Des.Text))
        //    {
        //        DisplayMessage("Please enter the item category description  ", 2);
        //        return;
        //    }
        //    string _cat1 = (string)hashtable2["_cat1"];
        //    if (string.IsNullOrEmpty(_cat1))
        //    {
        //        DisplayMessage("Please select thhe main item category ", 2);
        //        return;
        //    }
        //    string _cat2 = (string)hashtable2["_cat2"];
        //    if (string.IsNullOrEmpty(_cat2))
        //    {
        //        DisplayMessage("Please select the sub item category", 2);
        //        return;
        //    }
        //    string _cat3 = (string)hashtable2["_cat3"];
        //    if (string.IsNullOrEmpty(_cat3))
        //    {
        //        DisplayMessage("Please select the item range 1", 2);
        //        return;
        //    }
        //    string _cat4 = (string)hashtable2["_cat4"];
        //    if (string.IsNullOrEmpty(_cat4))
        //    {
        //        DisplayMessage("Please select the item range 2", 2);
        //        return;
        //    }
        //    if (_lstcate5 != null)
        //    {
        //        //REF_ITM_CATE5 result
        //        var result = _lstcate5.SingleOrDefault(x => x.Ric5_cd == txtCate5.Text && x.Ric5_cd1 == _cat1 && x.Ric5_cd2 == _cat2 && x.Ric5_cd3 == _cat3 && x.Ric5_cd4 == _cat4);
        //        if (result != null)
        //        {
        //            //result.Ric5_act = (check_active_cat5.Checked == true) ? true : false;// true; 
        //            //txtCate5.Text = "";
        //            //txtCate5Des.Text = "";
        //            //check_active_cat5.Checked = false;
        //            //grdCate5.DataSource = _lstcate5;
        //            //grdCate5.DataBind();
        //            //ViewState["_lstcate5"] = _lstcate5;
        //            //Session["countlsit"] = "yes";
        //            DisplayMessage("This category already exist", 2);
        //            return;
        //        }
        //    }
        //    else
        //    {
        //        _lstcate5 = new List<REF_ITM_CATE5>();
        //    }
        //    REF_ITM_CATE5 _cate5 = new REF_ITM_CATE5();
        //    _cate5.Ric5_cd = txtCate5.Text;
        //    _cate5.Ric5_desc = txtCate5Des.Text;
        //    _cate5.Ric5_cd1 = _cat1;
        //    _cate5.Ric5_cd2 = _cat2;
        //    _cate5.Ric5_cd3 = _cat3;
        //    _cate5.Ric5_cd4 = _cat4;
        //    _cate5.Ric5_act = (check_active_cat5.Checked == true) ? true : false;// true; 
        //    _cate5.RIC5_CRE_BY = Session["UserID"].ToString();
        //    _cate5.RIC5_MOD_BY = Session["UserID"].ToString();

        //    _lstcate5.Add(_cate5);

        //    grdCate5.DataSource = null;
        //    grdCate5.DataSource = new List<REF_ITM_CATE5>();
        //    grdCate5.DataSource = _lstcate5;
        //    grdCate5.DataBind();
        //    // _cat5 = txtCate5.Text;
        //    txtCate5.Text = "";
        //    txtCate5Des.Text = "";
        //    DisplayMessage("Successfully Added", 3);
        //    hashtable = Session["hashtable"] as Hashtable;
        //    hashtable["_cat5tag"] = 1;
        //    Session["hashtable"] = hashtable;
        //    ViewState["_lstcate5"] = _lstcate5;
        //    Session["countlsit"] = "yes";
        //}

        //protected void labtnSave5_Click(object sender, EventArgs e)
        //{
        //    if (Session["countlsit"].ToString() == "")
        //    {
        //        DisplayMessage("No any modifications or new record to save", 2);
        //        return;
        //    }
        //    hashtable2 = Session["hashtable2"] as Hashtable;
        //    //_lstcate5 = ViewState["_lstcate5"] as List<REF_ITM_CATE5>;
        //    string _cat2 = (string)hashtable2["_cat2"];
        //    string _cat1 = (string)hashtable2["_cat1"];
        //    string _cat3 = (string)hashtable2["_cat3"];
        //    string _cat4 = (string)hashtable2["_cat4"];

        //    foreach (GridViewRow _row in grdCate5.Rows)
        //    {
        //        REF_ITM_CATE5 _cate5 = new REF_ITM_CATE5();
        //        Label code = (Label)_row.FindControl("colmCode_Cate5");
        //        Label dec = (Label)_row.FindControl("colmDes_Cate5");
        //        CheckBox IsDefault = (CheckBox)_row.FindControl("colmStataus_Cate5");

        //        _cate5.Ric5_cd = code.Text;
        //        _cate5.Ric5_desc = dec.Text;
        //        _cate5.Ric5_cd1 = _cat1;
        //        _cate5.Ric5_cd2 = _cat2;
        //        _cate5.Ric5_cd3 = _cat3;
        //        _cate5.Ric5_cd4 = _cat4;
        //        _cate5.Ric5_act = (IsDefault.Checked == true) ? true : false;// true; 
        //        _cate5.RIC5_CRE_BY = Session["UserID"].ToString();
        //        _cate5.RIC5_MOD_BY = Session["UserID"].ToString();
        //        _lstcate5.Add(_cate5);
        //    }
        //    if (_lstcate5 == null)
        //    {
        //        DisplayMessage("Select Item Range 3 to save", 2);
        //        return;

        //    }
        //    if (_lstcate5.Count == 0)
        //    {
        //        DisplayMessage("Select Item Range 3 to save", 2);
        //        return;
        //    }

        //    //if (_cat5tag == 0)
        //    //{
        //    //    MessageBox.Show("No any modificatoin or new record to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    //    return;

        //    //}
        //    _lstcate5del = ViewState["_lstcate5del"] as List<REF_ITM_CATE5>;
        //    if (_lstcate5del == null)
        //    {
        //        _lstcate5del = new List<REF_ITM_CATE5>();
        //    }
        //    string _err;
        //    int row_aff = CHNLSVC.General.SaveItemCate5(_lstcate5, _lstcate5del, out _err);
        //    if (row_aff == 1)
        //    {
        //        DisplayMessage("Successfully Saved", 3);
        //        hashtable = Session["hashtable"] as Hashtable;
        //        hashtable["_cat5tag"] = 0;
        //        Session["hashtable"] = hashtable;

        //    }
        //    else
        //    {
        //        DisplayMessage("Terminate", 2);
        //    }
        //}

        //protected void lbtnClear5_Click(object sender, EventArgs e)
        //{
        //    if (txtClearlconformmessageValue.Value == "Yes")
        //    {
        //        txtCate5.Text = "";
        //        txtCate5Des.Text = "";
        //    }
        //}

        //protected void lbtnDeletecat5_Click(object sender, EventArgs e)
        //{
        //    bool _IsDeleted = false;
        //    _lstcate5 = ViewState["_lstcate5"] as List<REF_ITM_CATE5>;
        //    _lstcate5del = ViewState["_lstcate5del"] as List<REF_ITM_CATE5>;
        //    if (_lstcate5del == null)
        //    {
        //        _lstcate5del = new List<REF_ITM_CATE5>();
        //    }
        //    if (txtDeleteconformmessageValue.Value == "No")
        //    {
        //        return;
        //    }
        //    foreach (GridViewRow _row in grdCate4.Rows)
        //    {
        //        CheckBox checkbox = _row.FindControl("chk_ReqItem_cat5") as CheckBox;
        //        if (checkbox.Checked == true)
        //        {
        //            string type = (_row.FindControl("colmCode_Cate5") as Label).Text;
        //            _lstcate5del.AddRange(_lstcate5.Where(x => x.Ric5_cd == type));
        //            _lstcate5.RemoveAll(x => x.Ric5_cd == type);
        //            grdCate5.DataSource = _lstcate5del;
        //            grdCate5.DataBind();
        //            // _cat3tag = 1;

        //            ViewState["_lstcate5"] = _lstcate5;
        //            ViewState["_lstcate5del"] = _lstcate5del;
        //            hashtable = Session["hashtable"] as Hashtable;
        //            hashtable["_cat5tag"] = 1;
        //            Session["hashtable"] = hashtable;
        //            _IsDeleted = true;
        //            Session["countlsit"] = "true";
        //        }
        //    }
        //    if (_IsDeleted == false)
        //    {
        //        DisplayMessage("Please select the Item Range 3 to delete", 2);
        //    }
        //}

        //protected void colmStataus_Cate5_Click(object sender, EventArgs e)
        //{
        //    Session["countlsit"] = "yes";
        //}
        //#endregion

        #region Model
        protected void lbtnSearchModel_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
                DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Model";
                UserPopoup.Show();
            }

            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }
        protected void txtModel_TextChanged(object sender, EventArgs e)
        {
            //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
            //DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, "Code", txtModel.Text);

            //if (_result.Rows.Count > 0)
            //{
            //   // txtModelDes.Text = _result.Rows[0]["Description"].ToString();
            //   // _lstmodel = ViewState["_lstmodel"] as List<MasterItemModel>;
            //    if (_lstmodel.Count > 0)
            //    {
            //        var _filter = _lstmodel.SingleOrDefault(x => x.Mm_cd == txtModel.Text);
            //        if (_filter != null)
            //        {
            //            txtMainCat.Text = _filter.Mm_cat1;
            //            txtCat1.Text = _filter.Mm_cat2;
            //            txtCat2.Text = _filter.Mm_cat3;
            //            txtCat3.Text = _filter.Mm_cat4;
            //            txtCat4.Text = _filter.Mm_cat5;

            //        }
            //    }
            //}

            //string modelCode = txtModel.Text.Trim().ToUpper();

            //List<MasterItemModel> _lstmodeltem = new List<MasterItemModel>();
            //List<MasterItemModel> _lstmodel = new List<MasterItemModel>();
            //_lstmodeltem = CHNLSVC.General.GetItemModel(modelCode);
            //if (_lstmodeltem != null)
            //{
            //    _lstmodel = _lstmodeltem.Where(x => x.Mm_cd == modelCode).ToList();
            //    txtMainCat.Text = _lstmodel[0].Mm_cat1;
            //    txtCat1.Text = _lstmodel[0].Mm_cat2;
            //    txtCat2.Text = _lstmodel[0].Mm_cat3;
            //    txtCat3.Text = _lstmodel[0].Mm_cat4;
            //    txtCat4.Text = _lstmodel[0].Mm_cat5;
            //    txtModelDes.Text = _lstmodel[0].Mm_desc;
            //}
        }
        //protected void txtMainCat_TextChanged(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(txtMainCat.Text))
        //    {
        //        return;
        //    }

        //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
        //    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);

        //    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtMainCat.Text.Trim()).ToList();
        //    if (_validate == null || _validate.Count <= 0)
        //    {
        //        DisplayMessage("Invalid Category", 2);
        //        txtMainCat.Text = string.Empty; ;
        //        txtMainCat.Focus();
        //        return;
        //    }
        //}

        protected void lbtnSrch_mainCat_Click(object sender, EventArgs e)
        {
            try
            {
                Session["_IsCat"] = "true";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "masterCat1";
                UserPopoup.Show();
            }

            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }

        //protected void txtCat1_TextChanged(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(txtCat1.Text))
        //    {
        //        return;
        //    }

        //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
        //    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);

        //    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtCat1.Text.Trim()).ToList();
        //    if (_validate == null || _validate.Count <= 0)
        //    {
        //        DisplayMessage("Invalid Category", 2);
        //        txtCat1.Text = string.Empty;
        //        txtCat1.Focus();
        //        return;
        //    }
        //}

        protected void lbtnSrch_cat1_Click(object sender, EventArgs e)
        {
            try
            {
                Session["_IsCat"] = "true";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "masterCat2";
                UserPopoup.Show();
            }

            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }

        //protected void txtCat2_TextChanged(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(txtCat2.Text))
        //    {
        //        return;
        //    }

        //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat3);
        //    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);

        //    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtCat2.Text.Trim()).ToList();
        //    if (_validate == null || _validate.Count <= 0)
        //    {
        //        DisplayMessage("Invalid Category", 2);
        //        txtCat2.Text = string.Empty;
        //        txtCat2.Focus();
        //        return;
        //    }
        //}

        protected void lbtnSrch_cat2_Click(object sender, EventArgs e)
        {
            try
            {
                Session["_IsCat"] = "true";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat3);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "masterCat3";
                UserPopoup.Show();
            }

            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }

        //protected void txtCat3_TextChanged(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(txtCat3.Text))
        //    {
        //        return;
        //    }
        //    Session["_IsCat"] = "true";
        //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat4);
        //    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);

        //    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtCat3.Text.Trim()).ToList();
        //    if (_validate == null || _validate.Count <= 0)
        //    {
        //        DisplayMessage("Invalid Category", 2);
        //        txtCat3.Text = string.Empty;
        //        txtCat3.Focus();
        //        return;
        //    }
        //}

        protected void lbtnSrch_cat3_Click(object sender, EventArgs e)
        {
            try
            {
                Session["_IsCat"] = "true";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat4);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "masterCat4";
                UserPopoup.Show();
            }

            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }

        //protected void txtCat4_TextChanged(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(txtCat4.Text))
        //    {
        //        return;
        //    }

        //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat5);
        //    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);

        //    var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtCat4.Text.Trim()).ToList();
        //    if (_validate == null || _validate.Count <= 0)
        //    {
        //        DisplayMessage("Invalid Category", 2);
        //        txtCat4.Text = string.Empty;
        //        txtCat4.Focus();
        //        return;
        //    }
        //}

        protected void lbtnSrch_cat4_Click(object sender, EventArgs e)
        {
            try
            {
                Session["_IsCat"] = "true";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat5);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "masterCat5";
                UserPopoup.Show();
            }

            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            //pageClear();
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
        //protected void lbtnAddModel_Click(object sender, EventArgs e)
        //{
        //    _lstmodel = ViewState["_lstmodel"] as List<MasterItemModel>;
        //    Session["countlsit"] = "";
        //    if (string.IsNullOrEmpty(txtModel.Text))
        //    {
        //        DisplayMessage("Please enter the item category ", 2);
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(txtModelDes.Text))
        //    {
        //        DisplayMessage("Please enter the Item category description  ", 2);
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(txtMainCat.Text))
        //    {
        //        DisplayMessage("Please select the main item category ", 2);
        //        return;
        //    }

        //    if (string.IsNullOrEmpty(txtCat1.Text))
        //    {
        //        DisplayMessage("Please select sub category 1", 2);
        //        return;
        //    }


        //    if (string.IsNullOrEmpty(txtCat2.Text))
        //    {
        //        DisplayMessage("Please select the sub category 2", 2);
        //        return;
        //    }
        //    //if (string.IsNullOrEmpty(txtCat3.Text))
        //    //{
        //    //    DisplayMessage("Please select the sub category 3", 2);                     
        //    //    return;
        //    //}

        //    //if (string.IsNullOrEmpty(txtCat4.Text))
        //    //{
        //    //    DisplayMessage("Please Select the sub category 4", 2);   
        //    //    return;
        //    //}
        //    if (_lstmodel != null)
        //    {
        //        //MasterItemModel result
        //        var result = _lstmodel.SingleOrDefault(x => x.Mm_cd == txtModel.Text);
        //        if (result != null)
        //        {
        //            result.Mm_act = (chk_Active_model.Checked == true) ? true : false;// true;  
        //            result.Mm_desc = txtModelDes.Text;
        //            grdModel.DataSource = _lstmodel;
        //            grdModel.DataBind();
        //            //txtModel.Text = "";
        //            //txtModelDes.Text = "";
        //            //chk_Active_model.Checked = false;
        //            ViewState["_lstmodel"] = _lstmodel;
        //            //Session["countlsit"] = "yes";
        //            // DisplayMessage("This model  already exist", 2);   
        //            return;
        //        }



        //    }
        //    else
        //    {
        //        _lstmodel = new List<MasterItemModel>();
        //    }

        //    MasterItemModel _model = new MasterItemModel();
        //    _model.Mm_cd = txtModel.Text;
        //    _model.Mm_desc = txtModelDes.Text;
        //    _model.Mm_act = (chk_Active_model.Checked == true) ? true : false;// true;  
        //    _model.Mm_cat1 = txtMainCat.Text;
        //    _model.Mm_cat2 = txtCat1.Text;
        //    _model.Mm_cat3 = txtCat2.Text;
        //    _model.Mm_cat4 = txtCat3.Text;
        //    _model.Mm_cat5 = txtCat4.Text;
        //    _model.Mm_cre_by = Session["UserID"].ToString();
        //    _model.Mm_cre_dt = System.DateTime.Now;
        //    _model.Mm_mod_by = Session["UserID"].ToString();
        //    _model.Mm_mod_dt = System.DateTime.Now;

        //    _lstmodel.Add(_model);


        //    grdModel.DataSource = null;
        //    grdModel.DataSource = new List<MasterItemModel>();
        //    grdModel.DataSource = _lstmodel;
        //    grdModel.DataBind();

        //    ViewState["_lstmodel"] = _lstmodel;
        //    txtModel.Text = "";
        //    txtModelDes.Text = "";

        //    txtMainCat.Text = "";
        //    txtCat1.Text = "";
        //    txtCat2.Text = "";
        //    txtCat3.Text = "";
        //    txtCat4.Text = "";

        //    DisplayMessage("Successfully Added", 3);

        //    hashtable = Session["hashtable"] as Hashtable;
        //    hashtable["_modeltag"] = 1;
        //    Session["hashtable"] = hashtable;
        //    Session["countlsit"] = "yes";
        //}

        //protected void lblmodelgrid_Click(object sender, EventArgs e)
        //{
        //    _lstmodel = CHNLSVC.General.GetItemModel();
        //    grdModel.DataSource = null;
        //    grdModel.DataSource = new List<MasterItemModel>();
        //    grdModel.DataSource = _lstmodel;
        //    grdModel.DataBind();
        //    ViewState["_lstmodel"] = _lstmodel;
        //}

        //protected void lbtnClearModel_Click(object sender, EventArgs e)
        //{
        //    if (txtClearlconformmessageValue.Value == "Yes")
        //    {
        //        txtModel.Text = "";
        //        txtModelDes.Text = "";
        //        txtMainCat.Text = "";
        //        txtCat1.Text = "";
        //        txtCat2.Text = "";
        //        txtCat3.Text = "";
        //        txtCat4.Text = "";
        //    }
        //}

        //protected void lbtnSaveModel_Click(object sender, EventArgs e)
        //{
        //    if (Session["countlsit"].ToString() == "")
        //    {
        //        DisplayMessage("No any modifications or new record to save", 2);
        //        return;
        //    }
        //    // _lstmodel = ViewState["_lstmodel"] as List<MasterItemModel>;
        //    foreach (GridViewRow _row in grdModel.Rows)
        //    {
        //        Label code = (Label)_row.FindControl("colmMm_cd_model");
        //        Label dec = (Label)_row.FindControl("colmMm_desc_model");
        //        Label Cat_1 = (Label)_row.FindControl("Cat_1");
        //        Label Cat_2 = (Label)_row.FindControl("Cat_2");
        //        Label Cat_3 = (Label)_row.FindControl("Cat_3");
        //        Label Cat_4 = (Label)_row.FindControl("Cat_4");
        //        Label Cat_5 = (Label)_row.FindControl("Cat_5");
        //        CheckBox IsDefault = (CheckBox)_row.FindControl("colmMm_act");


        //        MasterItemModel _model = new MasterItemModel();
        //        _model.Mm_cd = code.Text;
        //        _model.Mm_desc = dec.Text;
        //        _model.Mm_act = (IsDefault.Checked == true) ? true : false;// true;  
        //        _model.Mm_cat1 = Cat_1.Text;
        //        _model.Mm_cat2 = Cat_2.Text;
        //        _model.Mm_cat3 = Cat_3.Text;
        //        _model.Mm_cat4 = Cat_4.Text;
        //        _model.Mm_cat5 = Cat_5.Text;
        //        _model.Mm_cre_by = Session["UserID"].ToString();
        //        _model.Mm_mod_by = Session["UserID"].ToString();
        //        _lstmodel.Add(_model);
        //    }
        //    if (_lstmodel == null)
        //    {
        //        DisplayMessage("Please Enter model to save", 2);
        //        return;
        //    }

        //    if (_lstmodel.Count == 0)
        //    {
        //        DisplayMessage("Please Enter model to save", 2);
        //        return;
        //    }

        //    if (!string.IsNullOrEmpty(txtModel.Text))
        //    {
        //        DisplayMessage("Please add model", 2);
        //        return;
        //    }
        //    if (!string.IsNullOrEmpty(txtModelDes.Text))
        //    {
        //        DisplayMessage("Please add Description", 2);
        //        return;
        //    }

        //    if (!string.IsNullOrEmpty(txtMainCat.Text))
        //    {
        //        DisplayMessage("Please add Main category", 2);
        //        return;
        //    }

        //    if (!string.IsNullOrEmpty(txtCat1.Text))
        //    {
        //        DisplayMessage("Please add Sub category 1", 2);
        //        return;
        //    }


        //    if (!string.IsNullOrEmpty(txtCat2.Text))
        //    {
        //        DisplayMessage("Please add Sub category 2", 2);
        //        return;
        //    }
        //    if (!string.IsNullOrEmpty(txtCat3.Text))
        //    {
        //        DisplayMessage("Please add Sub category 3", 2);
        //        return;
        //    }

        //    if (!string.IsNullOrEmpty(txtCat4.Text))
        //    {
        //        DisplayMessage("Please add Sub category 4", 2);
        //        return;
        //    }

        //    //if (_modeltag == 0)
        //    //{
        //    //    MessageBox.Show("No any modificatoin or new record to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    //    return;

        //    //}
        //    _lstmodeldel = ViewState["_lstmodeldel"] as List<MasterItemModel>;
        //    if (_lstmodeldel == null)
        //    {
        //        _lstmodeldel = new List<MasterItemModel>();
        //    }
        //    string _msg = string.Empty;

        //    //Lakshika - Pass null for Replaced Model List
        //    int row_aff = CHNLSVC.General.SaveItemModel(_lstmodel, _lstmodeldel, null, null, null, null, out _msg);
        //    if (row_aff == 1)
        //    {
        //        DisplayMessage("Successfully Saved", 3);
        //        hashtable = Session["hashtable"] as Hashtable;
        //        hashtable["_modeltag"] = 0;
        //        Session["hashtable"] = hashtable;
        //    }
        //    else
        //    {
        //        DisplayMessage("Terminate", 2);
        //    }
        //}

        protected void colmMm_act_Click(object sender, EventArgs e)
        {
            Session["countlsit"] = "yes";
        }

        //protected void lbtnmodelDelete_Click(object sender, EventArgs e)
        //{


        //    _lstmodeldel = ViewState["_lstmodeldel"] as List<MasterItemModel>;
        //    _lstmodel = ViewState["_lstmodel"] as List<MasterItemModel>;
        //    if (txtDeleteconformmessageValue.Value == "No")
        //    {
        //        return;
        //    }
        //    if (_lstmodeldel == null)
        //    {
        //        _lstmodeldel = new List<MasterItemModel>();
        //    }
        //    foreach (GridViewRow _row in grdModel.Rows)
        //    {
        //        CheckBox checkbox = _row.FindControl("chk_ReqItem_model") as CheckBox;
        //        if (checkbox.Checked == true)
        //        {
        //            string type = (_row.FindControl("colmMm_cd_model") as Label).Text;
        //            _lstmodeldel.AddRange(_lstmodel.Where(x => x.Mm_cd == type));
        //            _lstmodel.RemoveAll(x => x.Mm_cd == type);

        //            grdModel.DataSource = _lstmodel;
        //            grdModel.DataBind();
        //            ViewState["_lstmodel"] = _lstmodel;
        //            hashtable = Session["hashtable"] as Hashtable;
        //            hashtable["_modeltag"] = 1;
        //            Session["hashtable"] = hashtable;
        //            //  _brandtag = 1;

        //            ViewState["_lstmodeldel"] = _lstmodeldel;

        //            Session["countlsit"] = "yes";
        //        }
        //    }
        //}

        #endregion

        #region Brand
        protected void lbtnBrand_Click(object sender, EventArgs e)
        {
            _lstbrand = CHNLSVC.General.GetItemBrand();
            grdBrand.DataSource = null;
            grdBrand.DataSource = new List<MasterItemBrand>();
            grdBrand.DataSource = _lstbrand;
            grdBrand.DataBind();
            ViewState["_lstbrand"] = _lstbrand;
        }

        protected void lbtnSearchBrand_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "ItemBrand";
                UserPopoup.Show();
            }

            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnAddBrand_Click(object sender, EventArgs e)
        {

            Session["countlsit"] = "";
            _lstbrand = ViewState["_lstbrand"] as List<MasterItemBrand>;
            hashtable2 = Session["hashtable2"] as Hashtable;
            if (string.IsNullOrEmpty(txtBrand.Text))
            {
                DisplayMessage("Please select brand", 2);
                return;
            }
            if (string.IsNullOrEmpty(txtBrandName.Text))
            {
                DisplayMessage("Please Enter The brand description  ", 2);
                return;
            }

            if (_lstbrand != null)
            {
                //MasterItemBrand result
                var result = _lstbrand.Find(x => x.Mb_cd == txtBrand.Text);
                if (result != null)
                {
                    result.Mb_act = (chk_Active_brand.Checked == true) ? true : false;// true;   
                    grdBrand.DataSource = _lstbrand;
                    grdBrand.DataBind();
                    ViewState["_lstbrand"] = _lstbrand;
                    txtBrand.Text = "";
                    txtBrandName.Text = "";
                    chk_Active_brand.Checked = false;

                    Session["countlsit"] = "yes";
                    //DisplayMessage("This brand  already exist", 2);                    
                    return;
                }
            }
            else
            {
                _lstbrand = new List<MasterItemBrand>();
            }

            MasterItemBrand _brand = new MasterItemBrand();
            _brand.Mb_cd = txtBrand.Text;
            _brand.Mb_desc = txtBrandName.Text;
            _brand.Mb_act = (chk_Active_brand.Checked == true) ? true : false;// true;   
            _brand.Mb_cre_by = Session["UserID"].ToString();
            _brand.Mb_cre_dt = System.DateTime.Now;
            _brand.Mb_mod_by = Session["UserID"].ToString();
            _brand.Mb_mod_dt = System.DateTime.Now;

            _lstbrand.Add(_brand);

            grdBrand.DataSource = null;
            grdBrand.DataSource = new List<MasterItemBrand>();
            grdBrand.DataSource = _lstbrand;
            grdBrand.DataBind();
            ViewState["_lstbrand"] = _lstbrand;

            txtBrand.Text = "";
            txtBrandName.Text = "";
            DisplayMessage("Successfully Added", 3);

            //_cat1tag.Value = "1";
            hashtable = Session["hashtable"] as Hashtable;

            hashtable["_brandtag"] = 1;
            Session["hashtable"] = hashtable;

            Session["countlsit"] = "yes";

        }

        protected void lbtnSaveBrand_Click(object sender, EventArgs e)
        {
            if (Session["countlsit"].ToString() == "")
            {
                DisplayMessage("No any modifications or new record to save", 2);
                return;
            }

            //_lstbrand = ViewState["_lstbrand"] as List<MasterItemBrand>;
            foreach (GridViewRow _row in grdBrand.Rows)
            {
                Label code = (Label)_row.FindControl("colmMb_cd_brand");
                Label dec = (Label)_row.FindControl("colmMb_desc_model");
                CheckBox IsDefault = (CheckBox)_row.FindControl("colmMb_act");

                MasterItemBrand _brand = new MasterItemBrand();
                _brand.Mb_cd = code.Text;
                _brand.Mb_desc = dec.Text;
                _brand.Mb_act = (IsDefault.Checked == true) ? true : false;// true; 
                _brand.Mb_cre_by = Session["UserID"].ToString();
                _brand.Mb_mod_by = Session["UserID"].ToString();
                _lstbrand.Add(_brand);
            }
            if (_lstbrand == null)
            {
                DisplayMessage("Please Enter brand to save", 2);
                return;
            }

            if (_lstbrand.Count == 0)
            {
                DisplayMessage("Please Enter brand to save", 2);
                return;
            }
            //if (_brandtag == 0)
            //{
            //    MessageBox.Show("No any modificatoin or new record to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;

            //}
            _lstbranddel = ViewState["_lstbranddel"] as List<MasterItemBrand>;
            if (_lstbranddel == null)
            {
                _lstbranddel = new List<MasterItemBrand>();
            }
            string _msg = string.Empty;
            int row_aff = CHNLSVC.General.SaveItemBrand(_lstbrand, _lstbranddel, out _msg);
            if (row_aff == 1)
            {
                DisplayMessage("Successfully Saved", 3);
                hashtable = Session["hashtable"] as Hashtable;
                hashtable["_brandtag"] = 0;
                Session["hashtable"] = hashtable;

            }
            else
            {
                DisplayMessage("Terminate", 2);
            }
        }

        protected void lbtnClearBrand_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "Yes")
            {
                txtBrand.Text = "";
                txtBrandName.Text = "";
            }
        }
        protected void lbtnDeleteBrand_Click(object sender, EventArgs e)
        {
            bool _Ischeck = false;
            _lstbrand = ViewState["_lstbrand"] as List<MasterItemBrand>;
            _lstbranddel = ViewState["_lstbranddel"] as List<MasterItemBrand>;
            if (txtDeleteconformmessageValue.Value == "No")
            {
                return;
            }
            if (_lstbranddel == null)
            {
                _lstbranddel = new List<MasterItemBrand>();
            }
            foreach (GridViewRow _row in grdBrand.Rows)
            {
                CheckBox checkbox = _row.FindControl("chk_ReqItem_Brnad") as CheckBox;
                if (checkbox.Checked == true)
                {
                    string type = (_row.FindControl("colmMb_cd_brand") as Label).Text;
                    _lstbranddel.AddRange(_lstbrand.Where(x => x.Mb_cd == type));
                    _lstbrand.RemoveAll(x => x.Mb_cd == type);
                    grdBrand.DataSource = _lstbrand;
                    grdBrand.DataBind();
                    ViewState["_lstbrand"] = _lstbrand;
                    hashtable = Session["hashtable"] as Hashtable;
                    hashtable["_brandtag"] = 1;
                    Session["hashtable"] = hashtable;
                    //  _brandtag = 1;
                    Session["countlsit"] = "yes";
                    ViewState["_lstbranddel"] = _lstbranddel;
                    _Ischeck = true;

                }
            }
            if (_Ischeck == false)
            {
                DisplayMessage("Please select the brand to delete", 2);
            }
        }

        protected void colmMb_act_Click(object sender, EventArgs e)
        {
            Session["countlsit"] = "yes";
        }
        #endregion

        #region UOM
        protected void lbtnUOM_Click(object sender, EventArgs e)
        {
            _lstuom = CHNLSVC.General.GetItemUOM();
            grdUOM.DataSource = null;
            grdUOM.DataSource = new List<MasterUOM>();
            grdUOM.DataSource = _lstuom;
            grdUOM.DataBind();
            ViewState["_lstuom"] = _lstuom;
        }
        protected void lbtnSearchUOM_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "masterUOM";
                UserPopoup.Show();
            }

            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnAddUOM_Click(object sender, EventArgs e)
        {
            Session["countlsit"] = "";
            _lstuom = ViewState["_lstuom"] as List<MasterUOM>;
            hashtable2 = Session["hashtable2"] as Hashtable;
            if (string.IsNullOrEmpty(txtUOM.Text))
            {
                DisplayMessage("Please enter UOM", 2);
                return;
            }
            if (string.IsNullOrEmpty(txtUOMDes.Text))
            {
                DisplayMessage("Please enter description", 2);
                return;
            }
            if (_lstuom != null)
            {
                //MasterUOM result
                var result = _lstuom.Find(x => x.Msu_cd == txtUOM.Text);
                if (result != null)
                {
                    //result.Msu_act = (chk_Active_uom.Checked == true) ? true : false;// true;       
                    //grdUOM.DataSource = _lstuom;
                    //grdUOM.DataBind();
                    //ViewState["_lstuom"] = _lstuom;
                    //txtUOMDes.Text = "";
                    //txtUOM.Text = "";
                    //chk_Active_uom.Checked = false;
                    //Session["countlsit"] = "yes";
                    DisplayMessage("This UOM already exist", 2);
                    return;
                }
            }
            else
            {
                _lstuom = new List<MasterUOM>();
            }

            MasterUOM _uom = new MasterUOM();
            _uom.Msu_cd = txtUOM.Text;
            _uom.Msu_desc = txtUOMDes.Text;
            _uom.Msu_act = (chk_Active_uom.Checked == true) ? true : false;// true; 
            _uom.Msu_cre_by = Session["UserID"].ToString();
            _uom.Msu_cre_dt = System.DateTime.Now;
            _uom.Msu_mod_by = Session["UserID"].ToString();
            _uom.Msu_mod_dt = System.DateTime.Now;
            _uom.Msu_session_id = Session["SessionID"].ToString();
            _lstuom.Add(_uom);

            grdUOM.DataSource = null;
            grdUOM.DataSource = new List<MasterUOM>();
            grdUOM.DataSource = _lstuom;
            grdUOM.DataBind();
            ViewState["_lstuom"] = _lstuom;
            txtUOMDes.Text = "";
            txtUOM.Text = "";
            //_uomtag = 1;

            DisplayMessage("Successfully Added", 3);
            hashtable = Session["hashtable"] as Hashtable;

            hashtable["_uomtag"] = 1;
            Session["hashtable"] = hashtable;
            Session["countlsit"] = "yes";

        }

        protected void lbtnSaveUOM_Click(object sender, EventArgs e)
        {
            if (Session["countlsit"].ToString() == "")
            {
                DisplayMessage("No any modifications or new record to save", 2);
                return;
            }

            // _lstuom = ViewState["_lstuom"] as List<MasterUOM>;
            foreach (GridViewRow _row in grdUOM.Rows)
            {
                Label code = (Label)_row.FindControl("colmMsu_cd");
                Label dec = (Label)_row.FindControl("colmMsu_desc");
                CheckBox IsDefault = (CheckBox)_row.FindControl("colmMsu_act");

                MasterUOM _uom = new MasterUOM();
                _uom.Msu_cd = code.Text;
                _uom.Msu_desc = dec.Text;
                _uom.Msu_act = (IsDefault.Checked == true) ? true : false;// true; 
                _uom.Msu_cre_by = Session["UserID"].ToString();
                _uom.Msu_mod_by = Session["UserID"].ToString();
                _lstuom.Add(_uom);
            }
            if (_lstuom.Count == null)
            {
                DisplayMessage("Please enter the UOM to save", 2);
                return;
            }

            if (_lstuom.Count == 0)
            {
                DisplayMessage("Please enter the UOM to save", 2);
                return;
            }
            if (!string.IsNullOrEmpty(txtUOM.Text))
            {
                DisplayMessage("Please add UOM", 2);
                return;
            }
            if (!string.IsNullOrEmpty(txtUOMDes.Text))
            {
                DisplayMessage("Please add Description", 2);
                return;
            }
            //if (_uomtag == 0)
            //{
            //    MessageBox.Show("No any modificatoin or new record to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;

            //}
            _lstuomdel = ViewState["_lstuomdel"] as List<MasterUOM>;
            if (_lstuomdel == null)
            {
                _lstuomdel = new List<MasterUOM>();
            }
            string _msg = string.Empty;
            int row_aff = CHNLSVC.General.SaveItemUOM(_lstuom, _lstuomdel, out _msg);
            if (row_aff == 1)
            {

                DisplayMessage("Successfully Saved", 3);
                hashtable = Session["hashtable"] as Hashtable;
                hashtable["_uomtag"] = 0;
                Session["hashtable"] = hashtable;
            }
            else
            {
                DisplayMessage("Terminate", 2);
            }





        }

        protected void lbtnClearUOM_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "Yes")
            {
                txtUOMDes.Text = "";
                txtUOM.Text = "";
            }
        }
        protected void lbtnDeleteUOM_Click(object sender, EventArgs e)
        {
            bool _IsDeleted = false;
            _lstuom = ViewState["_lstuom"] as List<MasterUOM>;
            _lstuomdel = ViewState["_lstuomdel"] as List<MasterUOM>;
            if (txtDeleteconformmessageValue.Value == "No")
            {
                return;
            }
            if (_lstuomdel == null)
            {
                _lstuomdel = new List<MasterUOM>();
            }
            foreach (GridViewRow _row in grdUOM.Rows)
            {
                CheckBox checkbox = _row.FindControl("chk_ReqItem_UOM") as CheckBox;
                if (checkbox.Checked == true)
                {
                    string type = (_row.FindControl("colmMsu_cd") as Label).Text;

                    _lstuomdel.AddRange(_lstuom.Where(x => x.Msu_cd == type));
                    _lstuom.RemoveAll(x => x.Msu_cd == type);
                    // _uomtag = 1;

                    grdUOM.DataSource = _lstuom;
                    grdUOM.DataBind();

                    ViewState["_lstuom"] = _lstuom;
                    hashtable = Session["hashtable"] as Hashtable;
                    hashtable["_uomtag"] = 1;
                    Session["hashtable"] = hashtable;
                    //  _brandtag = 1;
                    ViewState["_lstuomdel"] = _lstuomdel;
                    Session["countlsit"] = "yes";
                    _IsDeleted = true;

                }
            }
            if (_IsDeleted == false)
            {
                DisplayMessage("Please select the UOM to delete", 2);
            }
        }
        protected void colmMsu_act_Click(object sender, EventArgs e)
        {
            Session["countlsit"] = "yes";
        }
        #endregion

        #region Color
        protected void lbtnColor_Click(object sender, EventArgs e)
        {
            _lstclr = CHNLSVC.General.GetItemColor();
            grdColor.DataSource = null;
            grdColor.DataSource = new List<MasterColor>();
            grdColor.DataSource = _lstclr;
            grdColor.DataBind();

            ViewState["_lstclr"] = _lstclr;
        }

        protected void lbtnSrhClr_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterColor);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchColorMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "masterColor";
                UserPopoup.Show();
            }

            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnAddColor_Click(object sender, EventArgs e)
        {

            Session["countlsit"] = "";
            _lstclr = ViewState["_lstclr"] as List<MasterColor>;
            hashtable2 = Session["hashtable2"] as Hashtable;
            if (string.IsNullOrEmpty(txtColor.Text))
            {
                DisplayMessage("Please enter color", 2);
                return;
            }
            if (string.IsNullOrEmpty(txtColorDes.Text))
            {
                DisplayMessage("Please enter Description", 2);
                return;
            }
            if (_lstclr != null)
            {
                //MasterColor result
                var result = _lstclr.SingleOrDefault(x => x.Clr_cd == txtColor.Text);
                if (result != null)
                {
                    //result.Clr_stus = (chk_act_color.Checked == true) ? true : false;// true;  
                    //grdColor.DataSource = _lstclr;
                    //grdColor.DataBind();
                    //ViewState["_lstclr"] = _lstclr;
                    //txtColorDes.Text = "";
                    //txtColor.Text = "";
                    //chk_act_color.Checked = false;

                    //Session["countlsit"] = "yes";
                    DisplayMessage("This color  already exist", 2);
                    return;
                }
            }
            else
            {
                _lstclr = new List<MasterColor>();
            }

            MasterColor _clor = new MasterColor();
            _clor.Clr_cd = txtColor.Text;
            _clor.Clr_desc = txtColorDes.Text;
            _clor.Clr_stus = (chk_act_color.Checked == true) ? true : false;// true;  
            _clor.Clr_cre_by = Session["UserID"].ToString();
            _clor.Clr_cre_dt = System.DateTime.Now;
            _clor.Clr_mod_by = Session["UserID"].ToString();
            _clor.Clr_mod_dt = System.DateTime.Now;
            _lstclr.Add(_clor);

            grdColor.DataSource = null;
            grdColor.DataSource = new List<MasterColor>();
            grdColor.DataSource = _lstclr;
            grdColor.DataBind();
            ViewState["_lstclr"] = _lstclr;
            txtColorDes.Text = "";
            txtColor.Text = "";
            DisplayMessage("Successfully Added", 3);
            hashtable = Session["hashtable"] as Hashtable;

            hashtable["_colortag"] = 1;
            Session["hashtable"] = hashtable;

            Session["countlsit"] = "yes";
        }

        protected void lbtnSaveColor_Click(object sender, EventArgs e)
        {
            if (Session["countlsit"].ToString() == "")
            {
                DisplayMessage("No any modifications or new record to save", 2);
                return;
            }
            // _lstclr = ViewState["_lstclr"] as List<MasterColor>;
            hashtable2 = Session["hashtable2"] as Hashtable;
            foreach (GridViewRow _row in grdColor.Rows)
            {
                Label code = (Label)_row.FindControl("colmClr_cd");
                Label dec = (Label)_row.FindControl("colmClr_desc");
                CheckBox IsDefault = (CheckBox)_row.FindControl("colmClr_stus");
                MasterColor _clor = new MasterColor();
                _clor.Clr_cd = code.Text;
                _clor.Clr_desc = dec.Text;
                _clor.Clr_stus = (IsDefault.Checked == true) ? true : false;// true;  
                _clor.Clr_cre_by = Session["UserID"].ToString();
                _clor.Clr_mod_by = Session["UserID"].ToString();
                _lstclr.Add(_clor);


            }
            if (_lstclr.Count == null)
            {
                DisplayMessage("Please Enter color to save", 2);
                return;
            }

            if (_lstclr.Count == 0)
            {
                DisplayMessage("Please Enter color to save", 2);
                return;
            }


            //if (_colortag == 0)
            //{
            //    MessageBox.Show("No any modificatoin or new record to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;

            //}
            _lstclrdel = ViewState["_lstclrdel"] as List<MasterColor>;
            if (_lstclrdel == null)
            {
                _lstclrdel = new List<MasterColor>();
            }
            string _msg = string.Empty;
            int row_aff = CHNLSVC.General.SaveItemColor(_lstclr, _lstclrdel, out _msg);
            if (row_aff == 1)
            {

                DisplayMessage("Successfully Saved", 3);
                hashtable = Session["hashtable"] as Hashtable;
                hashtable["_colortag"] = 0;
                Session["hashtable"] = hashtable;
            }
            else
            {
                DisplayMessage("Terminate", 3);
            }
        }

        protected void lbtnClearColor_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "Yes")
            {
                txtColorDes.Text = "";
                txtColor.Text = "";
            }
        }
        protected void lbtnDeleteColor_Click(object sender, EventArgs e)
        {
            bool _Isdelete = false;
            _lstclr = ViewState["_lstclr"] as List<MasterColor>;
            _lstclrdel = ViewState["_lstclrdel"] as List<MasterColor>;
            if (txtDeleteconformmessageValue.Value == "No")
            {
                return;
            }
            if (_lstclrdel == null)
            {
                _lstclrdel = new List<MasterColor>();
            }
            foreach (GridViewRow _row in grdColor.Rows)
            {
                CheckBox checkbox = _row.FindControl("chk_ReqItem_Color") as CheckBox;
                if (checkbox.Checked == true)
                {
                    string type = (_row.FindControl("colmClr_cd") as Label).Text;

                    _lstclrdel.AddRange(_lstclr.Where(x => x.Clr_cd == type));
                    _lstclr.RemoveAll(x => x.Clr_cd == type);
                    grdColor.DataSource = _lstclr;
                    grdColor.DataBind();
                    ViewState["_lstclr"] = _lstclr;
                    hashtable = Session["hashtable"] as Hashtable;
                    hashtable["_colortag"] = 1;
                    Session["hashtable"] = hashtable;
                    //  _brandtag = 1;
                    ViewState["_lstclrdel"] = _lstclrdel;
                    Session["countlsit"] = "yes";

                    _Isdelete = true;
                }
            }
            if (_Isdelete == false)
            {
                DisplayMessage("Please select the Color to delete", 2);
            }
        }
        protected void colmClr_stus_Click(object sender, EventArgs e)
        {
            Session["countlsit"] = "yes";
        }
        #endregion

        protected void btnClose_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            UserPopoup.Hide();
        }

        protected void txtBrand_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
            DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(SearchParams, "Code", txtBrand.Text);
            if (_result.Rows.Count > 0)
            {
                txtBrandName.Text = _result.Rows[0]["Description"].ToString();
            }
        }

        protected void txtUOM_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, "Code", txtUOM.Text);
            if (_result.Rows.Count > 0)
            {
                txtUOMDes.Text = _result.Rows[0]["Description"].ToString();
            }
        }

        protected void txtColor_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterColor);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchColorMaster(SearchParams, "CODE", txtColor.Text);
            if (_result.Rows.Count > 0)
            {
                txtColorDes.Text = _result.Rows[0]["DESCRIPTION"].ToString();
            }
        }

        protected void txtcatsearch_TextChanged(object sender, EventArgs e)
        {
            _lstcate1 = ViewState["_lstcate1"] as List<REF_ITM_CATE1>;

            var _filter = _lstcate1.Where(X => X.Ric1_cd == txtcatsearch.Text.ToUpper()).ToList();
            if (_filter.Count > 0)
            {
                grdCate1.DataSource = _filter;
                grdCate1.DataBind();
            }
            else
            {
                grdCate1.DataSource = _lstcate1;
                grdCate1.DataBind();
                DisplayMessage("No main categorization code found", 2);
            }
        }
        #region Item Range 2
        protected void txtCate4_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCate4.Text))
            {
                REF_ITM_CATE4 subCate = CHNLSVC.General.GetItemCategory4(txtCate4.Text);
                if (subCate != null)
                {
                    txtCate4Des.Text = subCate.Ric4_desc;
                }
            }
        }

        protected void lbtnCate4_Click(object sender, EventArgs e)
        {
            try
            {
                Session["_IsCat"] = "false";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat4);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Cate4";
                UserPopoup.Show();
            }

            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnAddCate4_Click(object sender, EventArgs e)
        {
            Session["countlsit"] = "";
            _lstcate4 = ViewState["_lstcate4"] as List<REF_ITM_CATE4>;
            hashtable2 = Session["hashtable2"] as Hashtable;
            if (string.IsNullOrEmpty(txtCate4.Text))
            {
                DisplayMessage("Please Enter The Item Category ", 2);
                return;
            }

            if (string.IsNullOrEmpty(txtCate4Des.Text))
            {
                DisplayMessage("Please Enter The Item Category Description  ", 2);
                return;
            }
            string _cat1 = (string)hashtable2["_cat1"];
            if (string.IsNullOrEmpty(_cat1))
            {
                DisplayMessage("Please Select The Main Item Category ", 2);
                return;
            }
            string _cat2 = (string)hashtable2["_cat2"];
            if (string.IsNullOrEmpty(_cat2))
            {
                DisplayMessage("Please Select The Sub Item Category", 2);
                return;
            }
            string _cat3 = (string)hashtable2["_cat3"];
            if (string.IsNullOrEmpty(_cat3))
            {
                DisplayMessage("Please Select Item Range 1", 2);
                return;
            }

            if (_lstcate4 != null)
            {
                //REF_ITM_CATE4 result 
                var result = _lstcate4.SingleOrDefault(x => x.Ric4_cd == txtCate4.Text && x.Ric4_cd1 == _cat1 && x.Ric4_cd2 == _cat2 && x.Ric4_cd3 == _cat3);
                if (result != null)
                {
                    // result.Ric4_act = (check_active_cat4.Checked == true) ? true : false;// true;       
                    //// _cat4 = txtCate4.Text;
                    // grdCate4.DataSource = _lstcate4;
                    // grdCate4.DataBind();
                    // check_active_cat4.Checked = false;
                    // txtCate4.Text = "";
                    // txtCate4Des.Text = "";
                    // ViewState["_lstcate4"] = _lstcate4;
                    // Session["countlsit"] = "yes";
                    DisplayMessage("This category already exist", 2);
                    return;
                }
            }
            else
            {
                _lstcate4 = new List<REF_ITM_CATE4>();
            }

            REF_ITM_CATE4 _cate4 = new REF_ITM_CATE4();
            _cate4.Ric4_cd = txtCate4.Text;
            _cate4.Ric4_desc = txtCate4Des.Text;
            _cate4.Ric4_cd1 = _cat1;
            _cate4.Ric4_cd2 = _cat2;
            _cate4.Ric4_cd3 = _cat3;
            _cate4.Ric4_act = (check_active_cat4.Checked == true) ? true : false;// true;  
            _cate4.RIC4_CRE_BY = Session["UserID"].ToString();
            _cate4.RIC4_MOD_BY = Session["UserID"].ToString();
            _lstcate4.Add(_cate4);

            grdCate4.DataSource = null;
            grdCate4.DataSource = new List<REF_ITM_CATE4>();
            grdCate4.DataSource = _lstcate4;
            grdCate4.DataBind();
            hashtable2["_cat4"] = txtCate4.Text;
            Session["hashtable2"] = hashtable2;

            // _cat4 = txtCate4.Text;
            txtCate4.Text = "";
            txtCate4Des.Text = "";

            DisplayMessage("Successfully Added", 3);
            hashtable = Session["hashtable"] as Hashtable;
            hashtable["_cat4tag"] = 1;
            Session["hashtable"] = hashtable;
            ViewState["_lstcate4"] = _lstcate4;
            //_cat4tag = 1;
            Session["countlsit"] = "yes";
        }

        protected void labtnSave4_Click(object sender, EventArgs e)
        {
            if (Session["countlsit"].ToString() == "")
            {
                DisplayMessage("No any modifications or new record to save", 2);
                return;
            }
            hashtable2 = Session["hashtable2"] as Hashtable;
            //_lstcate4 = ViewState["_lstcate4"] as List<REF_ITM_CATE4>;
            string _cat2 = (string)hashtable2["_cat2"];
            string _cat1 = (string)hashtable2["_cat1"];
            string _cat3 = (string)hashtable2["_cat3"];
            MasterItemSubCate _cate2 = new MasterItemSubCate();
            foreach (GridViewRow _row in grdCate4.Rows)
            {

                Label code = (Label)_row.FindControl("colmCode_Cate4");
                Label dec = (Label)_row.FindControl("colmDes_Cate4");
                CheckBox IsDefault = (CheckBox)_row.FindControl("colmStataus_Cate4");

                REF_ITM_CATE4 _cate4 = new REF_ITM_CATE4();
                _cate4.Ric4_cd = code.Text;
                _cate4.Ric4_desc = dec.Text;
                _cate4.Ric4_cd1 = _cat1;
                _cate4.Ric4_cd2 = _cat2;
                _cate4.Ric4_cd3 = _cat3;
                _cate4.Ric4_act = (IsDefault.Checked == true) ? true : false;// true; 
                _cate4.RIC4_CRE_BY = Session["UserID"].ToString();
                _cate4.RIC4_MOD_BY = Session["UserID"].ToString();
                _lstcate4.Add(_cate4);
            }
            if (_lstcate4 == null)
            {
                DisplayMessage("Select Item Range 2 to save", 2);
                return;

            }
            if (_lstcate4.Count == 0)
            {
                DisplayMessage("Select Item Range 2 to save", 2);
                return;

            }
            if (!string.IsNullOrEmpty(txtCate4.Text))
            {
                DisplayMessage("Please add item range 2", 2);
                return;
            }

            if (!string.IsNullOrEmpty(txtCate4Des.Text))
            {
                DisplayMessage("Please add item range 2Description", 2);
                return;
            }
            //if (_cat4tag == 0)
            //{
            //    MessageBox.Show("No any modificatoin or new record to save", "Item Categorization", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;

            //}
            _lstcate4del = ViewState["_lstcate4del"] as List<REF_ITM_CATE4>;
            if (_lstcate4del == null)
            {
                _lstcate4del = new List<REF_ITM_CATE4>();
            }
            string _err;
            int row_aff = CHNLSVC.General.SaveItemCate4(_lstcate4, _lstcate4del, out _err);
            if (row_aff == 1)
            {
                DisplayMessage("Successfully Saved", 3);
                hashtable = Session["hashtable"] as Hashtable;
                hashtable["_cat4tag"] = 0;
                Session["hashtable"] = hashtable;

            }
            else
            {
                DisplayMessage("Terminate", 2);
            }
        }

        protected void lbtnDeletecat4_Click(object sender, EventArgs e)
        {
            bool _IsDeleted = false;
            _lstcate4 = ViewState["_lstcate4"] as List<REF_ITM_CATE4>;
            _lstcate4del = ViewState["_lstcate4del"] as List<REF_ITM_CATE4>;
            if (_lstcate4del == null)
            {
                _lstcate4del = new List<REF_ITM_CATE4>();
            }
            if (txtDeleteconformmessageValue.Value == "No")
            {
                return;
            }
            foreach (GridViewRow _row in grdCate4.Rows)
            {
                CheckBox checkbox = _row.FindControl("chk_ReqItem_cat4") as CheckBox;
                if (checkbox.Checked == true)
                {
                    string type = (_row.FindControl("colmCode_Cate4") as Label).Text;
                    _lstcate4del.AddRange(_lstcate4.Where(x => x.Ric4_cd == type));

                    _lstcate4.RemoveAll(x => x.Ric4_cd == type);
                    grdCate4.DataSource = _lstcate4;
                    grdCate4.DataBind();
                    // _cat3tag = 1;

                    ViewState["_lstcate4"] = _lstcate4;
                    ViewState["_lstcate4del"] = _lstcate4del;
                    hashtable = Session["hashtable"] as Hashtable;
                    hashtable["_cat4tag"] = 1;
                    Session["hashtable"] = hashtable;
                    _IsDeleted = true;
                    Session["countlsit"] = "true";
                }
            }
            if (_IsDeleted == false)
            {
                DisplayMessage("Please select the Item Range 2 to delete", 2);
            }
        }

        protected void lbtnClear4_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "Yes")
            {
                txtCate4.Text = "";
                txtCate4Des.Text = "";
            }
        }
        protected void colmStataus_Cate4_Click(object sender, EventArgs e)
        {
            Session["countlsit"] = "yes";
        }
        protected void lbtnCat4Select_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < grdCate4.Rows.Count; i++)
            {
                grdCate4.Rows[i].BackColor = System.Drawing.Color.White;
            }
            if (grdCate4.Rows.Count == 0) return;

            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                CheckBox _itemCode = (row.FindControl("colmStataus_Cate4") as CheckBox);
                if (_itemCode.Checked == false)
                {
                    DisplayMessage("The code is inactive", 2);
                    return;
                }
                row.BackColor = System.Drawing.Color.LightCyan;
                hashtable2 = Session["hashtable2"] as Hashtable;
                string _cat1 = (string)hashtable2["_cat1"];
                string _cat2 = (string)hashtable2["_cat2"];
                string _cat3 = (string)hashtable2["_cat3"];
                string _cat4 = (row.FindControl("colmCode_Cate4") as Label).Text;
                _lstcate5 = CHNLSVC.General.GetItemCate5(_cat1, _cat2, _cat3, _cat4);

                //grdCate5.DataSource = null;
                //grdCate5.DataSource = new List<REF_ITM_CATE5>();
                //grdCate5.DataSource = _lstcate5;
                //grdCate5.DataBind();
                //hashtable2["_cat4"] = _cat4;
                //ViewState["_lstcate5"] = _lstcate5;
                //Session["hashtable2"] = hashtable2;
                //grdCate5.Visible = true;
                //lbCate5no.Visible = false;
            }
        }

        #endregion
    }
}