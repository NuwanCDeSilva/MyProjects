using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
using FF.BusinessObjects.InventoryNew;
//using Microsoft.Office.Interop.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.MasterFiles.Inventory
{
    public partial class ModelCreation : Base
    {
        List<REF_ITM_CATE1> _lstcate1 = new List<REF_ITM_CATE1>();
        List<MasterItemModel> _lstmodel = new List<MasterItemModel>();
        List<MasterItemModel> _lstmodeldel = new List<MasterItemModel>();
        //List<mst_itm_replace> _lstreplace = new List<mst_itm_replace>();
        static List<mst_model_replace> _lstreplace = new List<mst_model_replace>();
        //List<MasterCompanyItem> _lstcomItem = new List<MasterCompanyItem>();
        //static List<mst_commodel> _lstcomModel = new List<mst_commodel>();
        private List<UnitConvert> uomlist { get { return (List<UnitConvert>)Session["uomlist"]; } set { Session["uomlist"] = value; } }
        private List<mst_commodel> _lstcomModel { get { return (List<mst_commodel>)Session["_lstcomModel"]; } set { Session["_lstcomModel"] = value; } }
        Hashtable hashtable = new Hashtable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pageClear();
                // BindCombo();
                _lstreplace = new List<mst_model_replace>();
                _lstcomModel = new List<mst_commodel>();
              
                grdModelSegmentation.DataSource = new int[] { };
                grdModelSegmentation.DataBind();

                grdBusinessEntity.DataSource = null;

                LoadDefaultValues();
            }
            else 
            {
                txtEffectiveFrom.Text = Request[txtEffectiveFrom.UniqueID];
                txtDiscontinuedDate.Text = Request[txtDiscontinuedDate.UniqueID];
            }
        
        }

        protected void txtModel_TextChanged(object sender, EventArgs e)
        {
            string modelCode = txtModel.Text.Trim().ToUpper();

            if (chkisnewitem.Checked)
            {
                if (!string.IsNullOrEmpty(txtModel.Text))
                {
                    List<MasterItemModel> _modelMasterlist = CHNLSVC.General.GetItemModel(txtModel.Text.ToString());

                    if (_modelMasterlist != null)
                    {
                        if (_modelMasterlist.Count>0)
                        {
                            txtModel.Text = ""; DispMsg("Entered  model is already exists !"); return;
                        }
                       
                    }
                }
            }
            else
            {
                //Validate Model
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
                DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, "Code", modelCode);
                int count = _result.AsEnumerable()
                   .Count(row => row.Field<string>("Code") == modelCode);

                if (count > 0)
                {
                    List<MasterItemModel> _lstmodeltem = new List<MasterItemModel>();
                    List<MasterItemModel> _lstmodel = new List<MasterItemModel>();
                    ClearTexts();
                    _lstmodeltem = CHNLSVC.General.GetItemModel(modelCode);
                    if (_lstmodeltem != null)
                    {
                        if (_lstmodeltem.Count == 1)
                        {
                            _lstmodel = _lstmodeltem.Where(x => x.Mm_cd == modelCode).ToList();

                            if (_lstmodel[0].Mm_cat1 != null)
                                txtMainCat.Text = _lstmodel[0].Mm_cat1;

                            if (_lstmodel[0].Mm_cat2 != null)
                                txtCat1.Text = _lstmodel[0].Mm_cat2;

                            if (_lstmodel[0].Mm_cat3 != null)
                                txtCat2.Text = _lstmodel[0].Mm_cat3;
                            if (_lstmodel[0].Mm_hs_cd != null)
                                txthscode.Text = _lstmodel[0].Mm_hs_cd;

                            if (_lstmodel[0].Mm_cat4 != null)
                                txtCat3.Text = _lstmodel[0].Mm_cat4;

                            if (_lstmodel[0].Mm_cat5 != null)
                                txtCat4.Text = _lstmodel[0].Mm_cat5;

                            if (_lstmodel[0].Mm_desc != null)
                                txtModelDes.Text = _lstmodel[0].Mm_desc;

                            chk_Active_model.Checked = _lstmodel[0].Mm_act;

                            chkDiscontinue.Checked = _lstmodel[0].Mm_is_dis;

                            if (_lstmodel[0].Mm_uom != "")
                                drpuom.SelectedValue = _lstmodel[0].Mm_uom;

                            if (_lstmodel[0].Mm_dis_dt != null)
                                txtDiscontinuedDate.Text = _lstmodel[0].Mm_dis_dt.ToString("dd/MMM/yyyy");

                            if (_lstmodel[0].Mm_taxstruc_cd != null)
                                txtTaxStucture.Text = _lstmodel[0].Mm_taxstruc_cd;
                                txtLifSpan.Text = _lstmodel[0].Mm_lifes.ToString();
                            if (_lstmodel[0].Mm_brand != null)
                                txtbrand.Text = _lstmodel[0].Mm_brand.ToString();

                            if (_lstmodel[0].Mm_cntry_of_orgn != null)
                                txtcountryoforign.Text = _lstmodel[0].Mm_cntry_of_orgn;

                            LoadCompanyByModel();
                            LoadReplacedModelsByModel();
                            LoadModelClassificationsByModel();
                            LoadUOM(txtModel.Text.Trim().ToUpper());
                        }
                        else
                        {
                            pageClear();
                        }



                    }
                }
                else
                {
                    DisplayMessage("Plese Enter Valid Model", 2);
                    pageClear();
                    return;
                }
            }
           

        }

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
                string _Msg = "Error Occurred while processing search model";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtMainCat_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
            int count = _result.AsEnumerable()
             .Count(row => row.Field<string>("Code") == txtMainCat.Text.ToString());
            if (count == 0)
            {
                DisplayMessage("Please Enter Valid Main Category", 2);
                txtMainCat.Text = "";
                txtMainCat.Focus();
                return;
            }
        }

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
                string _Msg = "Error Occurred while processing search category";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtCat1_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
            int count = _result.AsEnumerable()
             .Count(row => row.Field<string>("Code") == txtCat1.Text.ToString());
           if(count==0)
           {
               DisplayMessage("Please Enter Valid Cat 1",2);
               txtCat1.Text = "";
               txtCat1.Focus();
               return;
           }
        }

        protected void lbtnSrch_cat1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMainCat.Text=="")
                {
                    DisplayMessage("Please Select Main Cat!!!",2);
                    return;
                }

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
                string _Msg = "Error Occurred while processing search category";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtCat2_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat3);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
            int count = _result.AsEnumerable()
          .Count(row => row.Field<string>("Code") == txtCat2.Text.ToString());
            if (count == 0)
            {
                DisplayMessage("Please Enter Valid Cat 2", 2);
                txtCat2.Text = "";
                txtCat2.Focus();
                return;
            }
        }

        protected void lbtnSrch_cat2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCat1.Text=="")
                {
                    DisplayMessage("Plese Select Cat1!!!",2);
                    return;
                }

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
                string _Msg = "Error Occurred while processing search category";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtCat3_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat4);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
            int count = _result.AsEnumerable()
        .Count(row => row.Field<string>("Code") == txtCat3.Text.ToString());
            if (count == 0)
            {
                DisplayMessage("Please Enter Valid Cat 3", 2);
                txtCat3.Text = "";
                txtCat3.Focus();
                return;
            }
        }

        protected void lbtnSrch_cat3_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCat2.Text=="")
                {
                    DisplayMessage("Please Select Cat2 !!!",2);
                    return;
                }

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
                string _Msg = "Error Occurred while processing search category";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtCat4_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat5);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, null, null);
            int count = _result.AsEnumerable()
       .Count(row => row.Field<string>("Code") == txtCat4.Text.ToString());
            if (count == 0)
            {
                DisplayMessage("Please Enter Valid Cat 4", 2);
                txtCat4.Text = "";
                txtCat4.Focus();
                return;
            }
        }

        protected void lbtnSrch_cat4_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCat3.Text=="")
                {
                    DisplayMessage("Plese Select Cat3!!",2);
                    return;
                }

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
                string _Msg = "Error Occurred while processing search category";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnAddModel_Click(object sender, EventArgs e)
        {
            _lstmodel = ViewState["_lstmodel"] as List<MasterItemModel>;
            Session["countlsit"] = "";
           
            if (string.IsNullOrEmpty(txtModel.Text))
            {
                DisplayMessage("Please enter the item category ", 2);
                return;
            }
            if (string.IsNullOrEmpty(txtModelDes.Text))
            {
                DisplayMessage("Please enter the Item category description  ", 2);
                return;
            }

            if (string.IsNullOrEmpty(txtMainCat.Text))
            {
                DisplayMessage("Please select the main item category ", 2);
                return;
            }

            if (string.IsNullOrEmpty(txtCat1.Text))
            {
                DisplayMessage("Please select sub category 1", 2);
                return;
            }


            if (string.IsNullOrEmpty(txtCat2.Text))
            {
                DisplayMessage("Please select the sub category 2", 2);
                return;
            }
            //if (string.IsNullOrEmpty(txtCat3.Text))
            //{
            //    DisplayMessage("Please select the sub category 3", 2);                     
            //    return;
            //}

            //if (string.IsNullOrEmpty(txtCat4.Text))
            //{
            //    DisplayMessage("Please Select the sub category 4", 2);   
            //    return;
            //}
            //if (_lstmodel.Count>0)
            //{
            //    //MasterItemModel result
            //   // var result = _lstmodel.SingleOrDefault(x => x.Mm_cd == txtModel.Text);
            //    var result = _lstmodel.First(A => A.Mm_cd == txtModel.Text);
            //    if (result != null)
            //    {
            //        result.Mm_act = (chk_Active_model.Checked == true) ? true : false;// true;  
            //        result.Mm_desc = txtModelDes.Text;
                 
            //        grdModel.DataSource = _lstmodel;
            //        grdModel.DataBind();    //Lakshika

            //        //txtModel.Text = "";
            //        //txtModelDes.Text = "";
            //        //chk_Active_model.Checked = false;
            //        ViewState["_lstmodel"] = _lstmodel;
            //        //Session["countlsit"] = "yes";
            //        // DisplayMessage("This model  already exist", 2);   
            //       // return;
            //    }



            //}
            //else
            //{
            //    _lstmodel = new List<MasterItemModel>();
            //}
            _lstmodel = new List<MasterItemModel>();
            Int32 _tmpInt = 0;
            MasterItemModel _model = new MasterItemModel();
            _model.Mm_cd = txtModel.Text.ToUpper().Trim();
            _model.Mm_desc = txtModelDes.Text;
            _model.Mm_act = (chk_Active_model.Checked == true) ? true : false;// true;  
            _model.Mm_cat1 = txtMainCat.Text;
            _model.Mm_cat2 = txtCat1.Text;
            _model.Mm_cat3 = txtCat2.Text;
            _model.Mm_cat4 = txtCat3.Text;
            _model.Mm_cat5 = txtCat4.Text;
            _model.Mm_cre_by = Session["UserID"].ToString();
            _model.Mm_cre_dt = System.DateTime.Now;
            _model.Mm_mod_by = Session["UserID"].ToString();
            _model.Mm_mod_dt = System.DateTime.Now;
            _model.Mm_is_dis = (chkDiscontinue.Checked == true) ? true : false;
            _model.Mm_uom = drpuom.SelectedValue.ToString();
            _model.Mm_brand = txtbrand.Text;
            _model.Mm_cntry_of_orgn = txtcountryoforign.Text;
            _model.Mm_hs_cd = txthscode.Text;
            #region add by lakshan
            Int32 _tmp = 0; DateTime _dtTemp = DateTime.MinValue;
            _model.Mm_lifes = Int32.TryParse(txtLifSpan.Text,out _tmp)? Convert.ToInt32(txtLifSpan.Text):0;
            _model.Mm_intro_dt = DateTime.TryParse(txtIntroDate.Text, out _dtTemp)? Convert.ToDateTime(txtIntroDate.Text.Trim()):DateTime.MinValue;
            #endregion
            if (_model.Mm_is_dis)
            {
                _model.Mm_dis_dt = Convert.ToDateTime(txtDiscontinuedDate.Text);
            }
            _model.Mm_taxstruc_cd = txtTaxStucture.Text;

            _lstmodel.Add(_model);


            //grdModel.DataSource = null;
            //grdModel.DataSource = new List<MasterItemModel>();
            //grdModel.DataSource = _lstmodel;
            //grdModel.DataBind();   //Lakshika

            //ViewState["_lstmodel"] = _lstmodel;
            //txtModel.Text = "";
            //txtModelDes.Text = "";

            //txtMainCat.Text = "";
            //txtCat1.Text = "";
            //txtCat2.Text = "";
            //txtCat3.Text = "";
            //txtCat4.Text = "";

            //chkDiscontinue.Checked = false;
            //txtDiscontinuedDate.Text = DateTime.Now.ToShortDateString();
            //txtTaxStucture.Text = "";

          //  DisplayMessage("Successfully Added", 3);

           /* hashtable = Session["hashtable"] as Hashtable;
            hashtable["_modeltag"] = 1;
            Session["hashtable"] = hashtable; */
            Session["countlsit"] = "yes";
        }
        protected void lbtnSaveModel_Click(object sender, EventArgs e)
        {
            if (txtSavelconformmessageValue.Value == "No")
            {
                return;
            }
            else
            {
                #region Special Character Validation
                if (!validateinputStringWithSpace(txtrepDes.Text))
                {
                    DisplayMessage("Invalid charactor found in replace model description.", 2);
                    txtrepDes.Focus();
                    return;
                }
                if (!validateinputString(txtrepModel.Text))
                {
                    DisplayMessage("Invalid charactor found in model code.", 2);
                    txtrepModel.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtModelDes.Text))
                {
                    DisplayMessage("Invalid charactor found in model description.", 2);
                    txtModelDes.Focus();
                    return;
                }
                if (!validateinputStringWithSpace(txtModelComDes.Text))
                {
                    DisplayMessage("Invalid charactor found in company description.", 2);
                    txtModelComDes.Focus();
                    return;
                }
                #endregion
                lbtnAddModel_Click(null, null);

                if (Session["countlsit"].ToString() == "")
                {
                    DisplayMessage("No any modifications or new record to save", 2);
                    return;
                }
                if (_lstmodel == null)
                {
                    DisplayMessage("Please Enter model to save", 2);
                    return;
                }

                if (_lstmodel.Count == 0)
                {
                    DisplayMessage("Please Enter model to save", 2);
                    return;
                }
                _lstmodeldel = ViewState["_lstmodeldel"] as List<MasterItemModel>;
                if (_lstmodeldel == null)
                {
                    _lstmodeldel = new List<MasterItemModel>();
                }
                string _msg = string.Empty;

                List<BusinessEntityVal> _lstBusEntity = new List<BusinessEntityVal>();
                if (Session["BusinessEntityVal"] != null)
                {
                    _lstBusEntity = Session["BusinessEntityVal"] as List<BusinessEntityVal>;
                }

                int row_aff = CHNLSVC.General.SaveItemModelWeb(_lstmodel, _lstmodeldel, _lstreplace, _lstcomModel, _lstBusEntity, uomlist, out _msg);
                if (row_aff == 1)
                {
                    DisplayMessage(_msg, 3);
                    hashtable = Session["hashtable"] as Hashtable;
                    // hashtable["_modeltag"] = 0;
                    Session["hashtable"] = hashtable;
                    Session["BusinessEntityVal"] = null;
                    //Lakshika
                    pageClear();
                }
                else
                {
                    DispMsg(_msg, "E");
                }
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
        protected void lbtnClearModel_Click(object sender, EventArgs e)
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

        protected void lbtnmodelDelete_Click(object sender, EventArgs e)
        { }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            bool _IsCat = true; //Convert.ToBoolean(Session["_IsCat"].ToString());
            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.ModelMaster:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HsCode:
                    {
                        string toCountry = "";
                        MasterCompany COM = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                        if (COM != null)
                        {
                            toCountry = COM.Mc_anal19;
                        }
                        paramsText.Append(toCountry);
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
                       
                        //paramsText.Append(txtCate1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        //paramsText.Append(txtCate1.Text + seperator + txtCate2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
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

                        if (_IsCat == true)
                        {
                            paramsText.Append(txtMainCat.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.masterCat2.ToString() + seperator + "" + seperator + "CAT_Sub1" + seperator);
                        }

                        else
                        {
                           // paramsText.Append(txtCate1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.masterCat2.ToString() + seperator + "" + seperator + "CAT_Sub1" + seperator);

                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat3:
                    {
                        if (_IsCat == true)
                        {
                            paramsText.Append(txtMainCat.Text + seperator + txtCat1.Text + seperator + CommonUIDefiniton.SearchUserControlType.masterCat3.ToString() + seperator + "" + seperator + "CAT_Sub2" + seperator);
                        }
                        else
                        {
                           // paramsText.Append(txtCate1.Text + seperator + txtCate2.Text + seperator + CommonUIDefiniton.SearchUserControlType.masterCat3.ToString() + seperator + "" + seperator + "CAT_Sub2" + seperator);

                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat4:
                    {
                        if (_IsCat == true)
                        {
                            paramsText.Append(txtMainCat.Text + seperator + txtCat1.Text + seperator + txtCat2.Text + seperator + CommonUIDefiniton.SearchUserControlType.masterCat4.ToString() + seperator + "CAT_Sub3" + seperator);
                        }
                        else
                        {
                        //    paramsText.Append(txtCate1.Text + seperator + txtCate2.Text + seperator + txtCate3.Text + seperator + CommonUIDefiniton.SearchUserControlType.masterCat4.ToString() + seperator + "CAT_Sub3" + seperator);
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat5:
                    {
                        if (_IsCat == true)
                        {
                            paramsText.Append(txtMainCat.Text + seperator + txtCat1.Text + seperator + txtCat2.Text + seperator + txtCat3.Text + seperator + "CAT_Sub4" + seperator);
                        }
                        else
                        {
                        //    paramsText.Append(txtCate1.Text + seperator + txtCate2.Text + seperator + txtCate3.Text + seperator + txtCate4.Text + seperator + "CAT_Sub4" + seperator);
                        }
                        break;
                    }
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
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ImportAgent:
                    {
                        paramsText.Append(Convert.ToString(Session["UserCompanyCode"]) + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
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
            this.ddlSearchbykey2.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
                this.ddlSearchbykey2.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
            this.ddlSearchbykey2.SelectedIndex = 0;
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string ID = grdResult.SelectedRow.Cells[1].Text;
            string Des = grdResult.SelectedRow.Cells[2].Text;
            //if (lblvalue.Text == "MCode")
            //{
            //    txtCate1.Text = ID;
            //    txtcatsearch.Text = ID;
            //    txtCate1Des.Text = Des;
            //    lblvalue.Text = "";
            //    _lstcate1 = ViewState["_lstcate1"] as List<REF_ITM_CATE1>;

            //    var _filter = _lstcate1.Where(X => X.Ric1_cd == txtcatsearch.Text.ToUpper()).ToList();
            //    if (_filter.Count > 0)
            //    {
            //        grdCate1.DataSource = _filter;
            //        grdCate1.DataBind();
            //    }
            //    else
            //    {
            //        grdCate1.DataSource = _lstcate1;
            //        grdCate1.DataBind();
            //        DisplayMessage("No main categorization code found", 2);
            //    }
            //    UserPopoup.Hide();
            //    return;
            //}
            //if (lblvalue.Text == "Cate2")
            //{
            //    txtCate2.Text = ID;
            //    txtCate2Des.Text = Des;
            //    lblvalue.Text = "";
            //    UserPopoup.Hide();
            //    return;
            //}
            //if (lblvalue.Text == "Cate3")
            //{
            //    txtCate3.Text = ID;
            //    txtCate3Des.Text = Des;
            //    lblvalue.Text = "";
            //    UserPopoup.Hide();
            //    return;
            //}
            //if (lblvalue.Text == "Cate4")
            //{
            //    txtCate4.Text = ID;
            //    txtCate4Des.Text = Des;
            //    lblvalue.Text = "";
            //    UserPopoup.Hide();
            //    return;
            //}
            //if (lblvalue.Text == "Cate5")
            //{
            //    txtCate5.Text = ID;
            //    txtCate5Des.Text = Des;
            //    lblvalue.Text = "";
            //    UserPopoup.Hide();
            //    return;
            //}
            if (lblvalue.Text == "Model")
            {
                txtModel.Text = ID;
                txtModelDes.Text = Des;
                lblvalue.Text = "";
                LoadModelDetails();
                LoadCompanyByModel();
                LoadReplacedModelsByModel();
                LoadModelClassificationsByModel();
                LoadUOM(ID);
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "ItemBrand")
            {
                txtbrand.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "masterCat1")
            {
                txtMainCat.Text = ID;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "masterCat2")
            {
                txtCat1.Text = ID;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "masterCat3")
            {
                txtCat2.Text = ID;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "masterCat4")
            {
                txtCat3.Text = ID;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "masterCat5")
            {
                txtCat4.Text = ID;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }

            if (lblvalue.Text == "Rep.Product")
            {
                txtrepModel.Text = ID;
                txtrepDes.Text = Des;
                lblvalue.Text = "";
                txtrepItem_TextChanged(null,null);
            
            }

            if (lblvalue.Text == "Pc_HIRC_Company3")
            {
                string details = grdResult.SelectedRow.Cells[2].Text;
                txtModelCom.Text = ID;
                txtModelComDes.Text = Des;
                //txtSupCom.Text = ID;
                //txtCuscom.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "UOMCOM")
            {
                string details = grdResult.SelectedRow.Cells[2].Text;
                txtuomcom.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "masterTax")
            {
                txtTaxStucture.Text = ID;
                lblvalue.Text = "";
                txtTaxStucture_TextChanged(null, null);
                return;
            }
            if (lblvalue.Text == "HsCode")
            {
                txthscode.Text = ID;
                lblvalue.Text = "";
                //txtTaxStucture_TextChanged(null, null);
                return;
            }
            if (lblvalue.Text == "CountyOfOrigin")
            {
                txtcountryoforign.Text = ID;
                lblvalue.Text = "";
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
            if (lblvalue.Text == "HsCode")
            {
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HsCode);
                DataTable _result = CHNLSVC.CommonSearch.SearchGetHsCode(para, null, null);
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
                //DataTable _result = CHNLSVC.CommonSearch.GetAllModels(SearchParams, null, null);
                DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, null, null);
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
            if (lblvalue.Text == "CountyOfOrigin")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
                DataTable _result = CHNLSVC.CommonSearch.GetCountrySearchData(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
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

            if ((lblvalue.Text == "Pc_HIRC_Company") || (lblvalue.Text == "Pc_HIRC_Company2") || (lblvalue.Text == "Pc_HIRC_Company3")
                    || (lblvalue.Text == "Pc_HIRC_Company4") || (lblvalue.Text == "Pc_HIRC_Company5") || (lblvalue.Text == "Pc_HIRC_Company9")
                    || (lblvalue.Text == "Pc_HIRC_Company6") || (lblvalue.Text == "Pc_HIRC_Company7") || (lblvalue.Text == "Pc_HIRC_Company8") || (lblvalue.Text == "UOMCOM"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                ViewState["SEARCH"] = _result;
                return;
            }

            if (lblvalue.Text == "masterTax")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterTax);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchTaxMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
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
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnSearch2_Click(object sender, EventArgs e)
        {
            try
            {
                //For model segmentation Search Company
                //FilterData();
                if ((lblvalue.Text == "Pc_HIRC_Company") || (lblvalue.Text == "Pc_HIRC_Company2") || (lblvalue.Text == "Pc_HIRC_Company3")
                  || (lblvalue.Text == "Pc_HIRC_Company4") || (lblvalue.Text == "Pc_HIRC_Company5") || (lblvalue.Text == "Pc_HIRC_Company9")
                  || (lblvalue.Text == "Pc_HIRC_Company6") || (lblvalue.Text == "Pc_HIRC_Company7") || (lblvalue.Text == "Pc_HIRC_Company8") || (lblvalue.Text == "UOMCOM"))
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey2.SelectedItem.ToString(), "%" + txtSearchbyword2.Text.ToString());
                    grdResult2.DataSource = _result;
                    grdResult2.DataBind();
                    ModelSegPopup.Show();
                    ViewState["SEARCH"] = _result;
                    return;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnCat1Select_Click(object sender, EventArgs e)
        { }

        protected void colmStataus_Click(object sender, EventArgs e)
        {
            Session["countlsit"] = "yes";
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
            if (lblvalue.Text == "HsCode")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HsCode);
                DataTable _result = CHNLSVC.CommonSearch.SearchGetHsCode(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                Session["popup"] = "true";
                return;
            }
            if (lblvalue.Text == "Rep.Product")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
                //DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString()); Lakshika
                DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString()); 
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                ViewState["SEARCH"] = _result;
                return;
            }

            if ((lblvalue.Text == "Pc_HIRC_Company") || (lblvalue.Text == "Pc_HIRC_Company2") || (lblvalue.Text == "Pc_HIRC_Company3")
                    || (lblvalue.Text == "Pc_HIRC_Company4") || (lblvalue.Text == "Pc_HIRC_Company5") || (lblvalue.Text == "Pc_HIRC_Company9")
                    || (lblvalue.Text == "Pc_HIRC_Company6") || (lblvalue.Text == "Pc_HIRC_Company7") || (lblvalue.Text == "Pc_HIRC_Company8"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                ViewState["SEARCH"] = _result;
                return;
            }
        }

        protected void txtSearchbyword2_TextChanged(object sender, EventArgs e)
        {

            if ((lblvalue.Text == "Pc_HIRC_Company") || (lblvalue.Text == "Pc_HIRC_Company2") || (lblvalue.Text == "Pc_HIRC_Company3")
                    || (lblvalue.Text == "Pc_HIRC_Company4") || (lblvalue.Text == "Pc_HIRC_Company5") || (lblvalue.Text == "Pc_HIRC_Company9")
                    || (lblvalue.Text == "Pc_HIRC_Company6") || (lblvalue.Text == "Pc_HIRC_Company7") || (lblvalue.Text == "Pc_HIRC_Company8") || (lblvalue.Text == "UOMCOM"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey2.SelectedItem.ToString(), "%" + txtSearchbyword2.Text.ToString());
                grdResult2.DataSource = _result;
                grdResult2.DataBind();
                ModelSegPopup.Show();
                ViewState["SEARCH"] = _result;
                return;
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            txtSearchbyword2.Text = "";
            UserPopoup.Hide();
            ModelSegPopup.Hide();
        }

        public void LoadModelDetails()
        {
            ClearTexts();
            MasterItemModel _model = new MasterItemModel();
            _model = CHNLSVC.General.GetItemModel(txtModel.Text).FirstOrDefault();
            if (_model != null)
            {
                txtModelDes.Text = _model.Mm_desc;
                txtMainCat.Text = _model.Mm_cat1;
                chk_Active_model.Checked = _model.Mm_act;
                txtCat1.Text = _model.Mm_cat2;
                txtCat2.Text = _model.Mm_cat3;
                txtCat3.Text = _model.Mm_cat4;
                txtCat4.Text = _model.Mm_cat5;
                chkDiscontinue.Checked = _model.Mm_is_dis;
                
                txtDiscontinuedDate.Text = _model.Mm_dis_dt.ToShortDateString();
                txtTaxStucture.Text = _model.Mm_taxstruc_cd;
                txtbrand.Text = _model.Mm_brand;
                txtLifSpan.Text = _model.Mm_lifes.ToString();
                txtcountryoforign.Text = _model.Mm_cntry_of_orgn;   
            }

        }

        void ClearTexts()
        {
            txtModelDes.Text = "";
            txtMainCat.Text = "";
            chk_Active_model.Checked = false;
            txtCat1.Text = "";
            txtCat2.Text = "";
            txtCat3.Text = "";
            txtCat4.Text = "";
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
        protected void colmMm_act_Click(object sender, EventArgs e)
        {
            Session["countlsit"] = "yes";
        }

        protected void txtrepItem_TextChanged(object sender, EventArgs e)
        {
            if (!validateinputString(txtrepModel.Text))
            {
                DisplayMessage("Invalid charactor found in replace model code.", 2);
                txtrepModel.Focus();
                return;
            }
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
            DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, null, null);

            if (txtModel.Text.Trim().ToString() == txtrepModel.Text.Trim().ToString())
            {
                string Msg = "Entered model and the model to be replaced cannot be same !";
                //DisplayMessage("Entered model and the model to be replaced cannot be same !", 2);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
                txtrepModel.Text = "";
                txtrepDes.Text = "";
                return;
            }
        }

        protected void txtModelSegemntCompany_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
            int count = _result.AsEnumerable()
       .Count(row => row.Field<string>("Code") == txtModelSegemntCompany.Text.ToString());
            if (count == 0)
            {
                DisplayMessage("Please Enter Valid Company", 2);
                txtModelSegemntCompany.Text = "";
                txtModelSegemntCompany.Focus();
                return;
            }
        }
        protected void lbtnSerchCom_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult2.DataSource = _result;
                grdResult2.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Pc_HIRC_Company3";
                //UserPopoup2.Show();
                ModelSegPopup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }
        
        protected void lbtnSerchRep_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
               // DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(SearchParams, null, null);Lakshika
                DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Rep.Product"; // Need to get Models???
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnAddRepalced_Click(object sender, EventArgs e)
        {
           // _lstreplace = ViewState["_lstreplace"] as List<mst_model_replace>;
            if (string.IsNullOrEmpty(txtrepModel.Text))
            {
                DisplayMessage("Select replaced item", 2);
                return;
            }
            if (string.IsNullOrEmpty(ddlrepStatus.Text))
            {
                DisplayMessage("Select status", 2);
                return;
            }

            if (_lstreplace != null)
            {
               // mst_itm_replace result = _lstreplace.Find(x => x.Rpl_replaceditem == txtrepItem.Text); Lakshika
                mst_model_replace result = _lstreplace.Find(x => x.Mrpl_model == txtrepModel.Text);
                if (result != null)
                {
                    DisplayMessage("This item already exist", 2);
                    return;
                }
            }
            else
            {
               // _lstreplace = new List<mst_itm_replace>(); Lakshika
                _lstreplace = new List<mst_model_replace>();
            }

            //mst_itm_replace _itm = new mst_itm_replace(); Lakshika
            mst_model_replace _model = new mst_model_replace();
          /*  _itm.Rpl_item = txtModel.Text; //txtItem.Text;
            _itm.Rpl_replaceditem = txtrepItem.Text;
            _itm.Rpl_itemdes = txtrepDes.Text;
            _itm.Rpl_active = ddlrepStatus.SelectedItem.ToString() == "YES" ? 1 : 0;
            _itm.Rpl_active_status = ddlrepStatus.SelectedItem.ToString();
            _itm.Rpl_repl_reson = Convert.ToInt32(ddlReplaceProduct.SelectedValue);
            _itm.Rpl_effect_dt = Convert.ToDateTime(txtEffectiveFrom.Text);
            _itm.Rpl_cre_by = Session["UserID"].ToString();
            _itm.Rpl_mod_by = Session["UserID"].ToString();
            _lstreplace.Add(_itm); */ //Lakshika

            _model.Mrpl_model=txtModel.Text;
            _model.Mrpl_replacedmodel = txtrepModel.Text;
            _model.Mrpl_cre_by = Session["UserID"].ToString();
            _model.Mrpl_mod_by = Session["UserID"].ToString();
            _model.Mrpl_repl_reson = ddlReplaceProduct.SelectedItem.Value;
            
            DateTime ss = Convert.ToDateTime(txtEffectiveFrom.Text).Date;
            _model.Mrpl_effect_dt = Convert.ToDateTime(txtEffectiveFrom.Text).Date;
            _model.Mrpl_effective_dt_text = txtEffectiveFrom.Text;
           // _model.Mrpl_active = ddlrepStatus.SelectedItem.ToString() == "YES" ? 1 : 0;

            if (ddlrepStatus.SelectedItem.ToString() == "YES")
            {
                _model.Mrpl_active = true;
                _model.Mrpl_active_status = "YES";
            }
            else {
                _model.Mrpl_active = false;
                _model.Mrpl_active_status = "NO";
            }


            _lstreplace.Add(_model);

            grdRepalced.DataSource = null;
            grdRepalced.DataSource = new List<mst_itm_replace>();
            grdRepalced.DataSource = _lstreplace;
            grdRepalced.DataBind();

            ViewState["_lstreplace"] = _lstreplace;

            txtrepModel.Text = "";
            txtrepDes.Text = "";
            ddlrepStatus.SelectedIndex = -1;
        }

        private void pageClear()
        {
            txtIntroDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtDiscontinuedDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            Session["Multiplecom"] = "";
            ddlvalues();//Load dropdownList values
            _lstreplace.Clear();
            //item replace grid initialize
            grdRepalced.DataSource = new List<mst_itm_replace>();
            grdRepalced.DataBind();
            txtEffectiveFrom.Text = CHNLSVC.Security.GetServerDateTime().Date.ToShortDateString();
            txtDiscontinuedDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToShortDateString();
            //Company grid initialize
            grdComModel.DataSource = new List<mst_commodel>();
            grdComModel.DataBind();
            grdModel.DataSource = null;
            grdModel.DataSource = new List<MasterItemModel>();
            grdModel.DataSource = _lstmodel;
            grdModel.DataBind();   //Lakshika
            grduomdata.DataSource = null;
            grduomdata.DataBind();
            grdBusinessEntity.DataSource = null;
            grdBusinessEntity.DataBind();

            ViewState["_lstmodel"] = _lstmodel;
            txtModel.Text = "";
            txtModelDes.Text = "";

            txtMainCat.Text = "";
            txtCat1.Text = "";
            txtCat2.Text = "";
            txtCat3.Text = "";
            txtCat4.Text = "";
            txthscode.Text = "";
            chkDiscontinue.Checked = false;
            txtDiscontinuedDate.Text = DateTime.Now.ToShortDateString();
            txtTaxStucture.Text = "";
            chk_Active_model.Checked = true;
            Session["countlsit"] = "";
            uomlist = null;
            drpuom.Enabled = true;
            txtuomcom.Text = "";
            txtqty.Text = "";
            txtLifSpan.Text = "";
            txtbrand.Text = "";
            txtcountryoforign.Text = "";
        }

       private void ddlvalues()
        {

            ddlReplaceProduct.Items.Clear();
            ddlReplaceProduct.Items.Clear();
           //Load replace model remarks
            DataTable _REplace = CHNLSVC.CommonSearch.GET_ITM_REPL_REASON();
            ddlReplaceProduct.DataSource = _REplace;
            ddlReplaceProduct.DataTextField = "rir_desc";
            ddlReplaceProduct.DataValueField = "rir_tp";
            ddlReplaceProduct.DataBind();

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
            DataTable saleTypes = CHNLSVC.General.GetSalesTypes(SearchParams, null, null);
            ddlhpSalesAccept.DataSource = saleTypes;
            ddlhpSalesAccept.DataTextField = "srtp_desc";
            ddlhpSalesAccept.DataValueField = "srtp_cd";
            ddlhpSalesAccept.DataBind();

        }

       private void FilterData()
       {
           try
           {
               #region Filter
               if (lblvalue.Text == "masterItem")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
                   DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }
               if (lblvalue.Text == "ItemBrand")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                   DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }
               if (lblvalue.Text == "ModelMaster")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
                   DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }
               if (lblvalue.Text == "masterUOM")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
                   DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }
               if (lblvalue.Text == "masterCat1")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat1);
                   DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }
               if (lblvalue.Text == "masterCat2")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat2);
                   DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }
               if (lblvalue.Text == "masterCat3")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat3);
                   DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }
               if (lblvalue.Text == "masterCat4")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat4);
                   DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }
               if (lblvalue.Text == "masterCat5")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterCat5);
                   DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }
               if (lblvalue.Text == "masterTax")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterTax);
                   DataTable _result = CHNLSVC.CommonSearch.GetItemSearchTaxMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }
               if (lblvalue.Text == "masterColor")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterColor);
                   DataTable _result = CHNLSVC.CommonSearch.GetItemSearchColorMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }
               if (lblvalue.Text == "masterColor2")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterColor);
                   DataTable _result = CHNLSVC.CommonSearch.GetItemSearchColorMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }
               if ((lblvalue.Text == "Pc_HIRC_Company") || (lblvalue.Text == "Pc_HIRC_Company2") || (lblvalue.Text == "Pc_HIRC_Company3")
                   || (lblvalue.Text == "Pc_HIRC_Company4") || (lblvalue.Text == "Pc_HIRC_Company5") || (lblvalue.Text == "Pc_HIRC_Company9")
                   || (lblvalue.Text == "Pc_HIRC_Company6") || (lblvalue.Text == "Pc_HIRC_Company7") || (lblvalue.Text == "Pc_HIRC_Company8") || (lblvalue.Text == "UOMCOM"))
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                   DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }
               if (lblvalue.Text == "masterContry")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterContry);
                   DataTable _result = CHNLSVC.CommonSearch.GetCountrySearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }
               if (lblvalue.Text == "InvoiceItemUnAssableByModel")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
                   DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }
               if (lblvalue.Text == "Customer")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                   DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString(), CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }
               if (lblvalue.Text == "Supplier")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                   DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }
               if (lblvalue.Text == "Rep.Product")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
                   //DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString()); 
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }
               if (lblvalue.Text == "Kit")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
                   DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }
               if (lblvalue.Text == "WeightUOM")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
                   DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;

               }
               if (lblvalue.Text == "DiamentionsUOM")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
                   DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }
               if (lblvalue.Text == "ServiceUOM")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
                   DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }
               if (lblvalue.Text == "ServiceUOM2")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
                   DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }
               if (lblvalue.Text == "WarrantyUOM")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterUOM);
                   DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }
               if (lblvalue.Text == "Pc_HIRC_Location")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                   DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }

               if (lblvalue.Text == "Loc_HIRC_Channel")
               {
                   string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                   DataTable _result = CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                   grdResult.DataSource = _result;
                   grdResult.DataBind();
                   UserPopoup.Show();
                   ViewState["SEARCH"] = _result;
                   return;
               }
               #endregion
           }
           catch (Exception ex)
           {
               //divalert.Visible = true;
               DisplayMessage(ex.Message, 4);
           }
       }



       protected void txtItemCom_TextChanged(object sender, EventArgs e)
       {
           string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
           DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
           int count = _result.AsEnumerable()
       .Count(row => row.Field<string>("Code") == txtModelCom.Text.ToString());
           if (count == 0)
           {
               DisplayMessage("Please Enter Valid Company", 2);
               txtModelCom.Text = "";
               txtModelCom.Focus();
               return;
           }
       }

       protected void lbtnSearchitemCom_Click(object sender, EventArgs e)
       {
           try
           {
               string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
               DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
               grdResult.DataSource = _result;
               grdResult.DataBind();
               BindUCtrlDDLData(_result);
               lblvalue.Text = "Pc_HIRC_Company3";
               UserPopoup.Show();
           }
           catch (Exception ex)
           {
               string _Msg = "Error Occurred while processing..!";
               DisplayMessage(_Msg, 4);
           }
       }

       protected void lbtnMultipleCom_Click(object sender, EventArgs e)
       {
           chklstbox.Items.Clear();
           string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
           DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
           foreach (DataRow drow in _result.Rows)
           {
               Session["Multiplecom"] = "true";
               chklstbox.Items.Add(drow["Code"].ToString());

           }
           MultipleCom.Show();
       }

       protected void lbtnsearchComModelAdd_Click(object sender, EventArgs e)
       {
           //lbtnsearchComItemAdd_Click
           //_lstcomItem = ViewState["_lstcomItem"] as List<MasterCompanyItem>;
           //_lstcomItem = ViewState["_lstcomItem"] as List<mst_commodel>;

           if (string.IsNullOrEmpty(ddlitemStatus.SelectedItem.Text))
           {
               DisplayMessage("Select item status", 2);
               return;
           }

           if (string.IsNullOrEmpty(ddlFoc.SelectedItem.Text))
           {
               DisplayMessage("Select FOC allow or not", 2);
               return;
           }
         /*  if (string.IsNullOrEmpty(txtModelCom.Text))
           {
               DisplayMessage("Select the Company", 2);
               return;
           } */ //Lakshika 
           if (Session["Multiplecom"] == "true")
           {
               foreach (ListItem Item in chklstbox.Items)
               {
                   if (Item.Selected == true)
                   {
                       if (_lstcomModel != null)
                       {
                           mst_commodel result = _lstcomModel.Find(x => x.Mcm_com == Item.Text);
                           if (result != null)
                           {
                               DisplayMessage("This company already exist", 2);
                               return;
                           }

                       }
                       else { _lstcomModel = new List<mst_commodel>(); }
                       mst_commodel _itmMul = new mst_commodel();

                       _itmMul.Mcm_model = txtModel.Text;//txtModelCom.Text;
                       _itmMul.Mcm_com = Item.Text;
                       _itmMul.Mcm_restric_inv_tp = ddlhpSalesAccept.SelectedValue;
                       if (ddlitemStatus.SelectedItem.Text == "YES")
                       {
                           _itmMul.Mcm_act = true;
                           _itmMul.Mcm_act_status = "YES";
                       }
                       else
                       {
                           _itmMul.Mcm_act = false;
                           _itmMul.Mcm_act_status = "NO";
                       }
                       if (ddlFoc.SelectedItem.Text == "Allow")
                       {
                           _itmMul.Mcm_isfoc = true;
                           _itmMul.Mcm_isfoc_status = "YES";
                       }
                       else
                       {
                           _itmMul.Mcm_isfoc = false;
                           _itmMul.Mcm_isfoc_status = "NO";
                       }

                      // _itmMul.Mci_age_type = ddlAgecType.SelectedItem.Text;
                       string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                       DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, "Code", Item.Value);
                       if (_result.Rows.Count > 0)
                       {
                           _itmMul.Mcm_com_desc = _result.Rows[0][1].ToString();
                       }



                       _lstcomModel.Add(_itmMul);

                       grdComModel.DataSource = null;
                       grdComModel.DataSource = new List<mst_commodel>();
                       grdComModel.DataSource = _lstcomModel;
                       grdComModel.DataBind();
                       ViewState["_lstcomModel"] = _lstcomModel;




                   }
               }

               txtModelCom.Text = "";
               ddlitemStatus.SelectedIndex = -1;
               ddlFoc.SelectedIndex = -1;
               ddlAgecType.SelectedIndex = -1;
               txtModelDes.Text = "";
               Session["Multiplecom"] = "";
               return;
           }
          // Mutiple End

           //if (string.IsNullOrEmpty(ddlAgecType.SelectedItem.Text))
           //{
           //    DisplayMessage("Select agency type", 2);
           //    return;
           //}
           if (string.IsNullOrEmpty(txtModelCom.Text))
           {
               DisplayMessage("Select the Company", 2);
               return;
           } //Laskhika
           if (_lstcomModel != null)
           {
               //MasterCompanyItem result = _lstcomItem.Find(x => x.Mci_com == txtModelCom.Text);
               mst_commodel result = _lstcomModel.Find(x => x.Mcm_com == txtModelCom.Text);
               if (result != null)
               {
                   DisplayMessage("This company already exist", 2);
                   return;
               }
           }

           else
           {
              // _lstcomItem = new List<MasterCompanyItem>(); Lakshika
               _lstcomModel = new List<mst_commodel>();
           }
          // MasterCompanyItem _itm = new MasterCompanyItem();
           mst_commodel _model = new mst_commodel();

           _model.Mcm_model = txtModel.Text;
           _model.Mcm_com = txtModelCom.Text;
           _model.Mcm_com_desc = txtModelComDes.Text;

           _model.Mcm_restric_inv_tp = ddlhpSalesAccept.SelectedValue;
           if (ddlitemStatus.SelectedItem.Text == "YES")
           {
               _model.Mcm_act = true;
               _model.Mcm_act_status = "YES";
           }
           else
           {
               _model.Mcm_act = false;
               _model.Mcm_act_status = "NO";
           }
           if (ddlFoc.SelectedItem.Text == "Allow")
           {
               _model.Mcm_isfoc = true;
               _model.Mcm_isfoc_status = "YES";
           }
           else
           {
               _model.Mcm_isfoc = false;
               _model.Mcm_isfoc_status = "NO";
           }

          // _model.Mci_age_type = ddlAgecType.SelectedItem.Text;
          // _model.Mci_comDes = txtitemDes.Text; Lakshika


           _lstcomModel.Add(_model);

           grdComModel.DataSource = null;
           //grdComModel.DataSource = new List<MasterCompanyItem>();
           grdComModel.DataSource = new List<mst_commodel>();
           grdComModel.DataSource = _lstcomModel;
           grdComModel.DataBind();
           ViewState["_lstcomItem"] = _lstcomModel;

           txtModelCom.Text = "";
           ddlitemStatus.SelectedIndex = -1;
           ddlFoc.SelectedIndex = -1;
           ddlAgecType.SelectedIndex = -1;//hidden
           ddlhpSalesAccept.SelectedIndex = -1;
           txtModelComDes.Text = "";

       }

       protected void txtReCom_TextChanged(object sender, EventArgs e)
       {
       }
       protected void lbtnSearchRedeem_Click(object sender, EventArgs e)
       { }

       protected void lbtnsearchRedeemCom_Click(object sender, EventArgs e)
       { }

       protected void txtTaxStucture_TextChanged(object sender, EventArgs e)
       {
           if (string.IsNullOrEmpty(txtTaxStucture.Text))
           {
               return;
           }

           string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
           DataTable _result = CHNLSVC.CommonSearch.GetItemSearchTaxMaster(SearchParams, null, null);

           var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtTaxStucture.Text.Trim()).ToList();
           if (_validate == null || _validate.Count <= 0)
           {
               DisplayMessage("Invalid structure", 2);
               txtTaxStucture.Text = string.Empty;
               txtTaxStucture.Focus();
               return;
           }
           List<mst_itm_tax_structure_det> _lstTaxDet = new List<mst_itm_tax_structure_det>();
           //List<mst_itm_tax_structure_hdr> _lstTaxhdr = new List<mst_itm_tax_structure_hdr>();
           //_lstTaxhdr = CHNLSVC.General.GetItemTaxStructureHeader(_code);
           _lstTaxDet = CHNLSVC.General.getItemTaxStructure(txtTaxStucture.Text);
           if (_lstTaxDet != null && _lstTaxDet.Count > 0)
           {

               List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
               oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
               if (_lstTaxDet != null)
               {
                   foreach (mst_itm_tax_structure_det itemSer in _lstTaxDet)
                   {
                       if (oItemStaus != null && oItemStaus.Count > 0)
                       {
                           var nwlist = oItemStaus.Where(x => x.Mis_cd == itemSer.Its_stus).ToList();
                           // itemSer.Its_stus_Des = oItemStaus.Find(x => x.Mis_cd == itemSer.Its_stus).Mis_desc;
                           if (nwlist.Count > 0)
                           {
                               itemSer.Its_stus_Des = nwlist.First().Mis_desc;
                           }

                       }
                   }
               }

               grdTax.DataSource = _lstTaxDet;
               grdTax.DataBind();
               taxDetailspopup.Show();
           }
           else
           {
               DisplayMessage("Invalid Tax structure", 2);
               //MessageBox.Show("Invalid Tax structure", "Item Tax Structure", MessageBoxButtons.OK, MessageBoxIcon.Information);
               txtTaxStucture.Text = "";
               txtTaxStucture.Focus();
           }

       }

       protected void lbtnsrcTax_Click(object sender, EventArgs e)
       {
           try
           {
               string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterTax);
               DataTable _result = CHNLSVC.CommonSearch.GetItemSearchTaxMaster(SearchParams, null, null);
               grdResult.DataSource = _result;
               grdResult.DataBind();
               BindUCtrlDDLData(_result);
               lblvalue.Text = "masterTax";
               UserPopoup.Show();
           }
           catch (Exception ex)
           {
               string _Msg = "Error Occurred while processing..!";
               DisplayMessage(_Msg, 4);
           }
       }

       protected void lbtntxD_Click(object sender, EventArgs e)
       {
           List<mst_itm_tax_structure_det> _lstTaxDet = new List<mst_itm_tax_structure_det>();
           //List<mst_itm_tax_structure_hdr> _lstTaxhdr = new List<mst_itm_tax_structure_hdr>();
           //_lstTaxhdr = CHNLSVC.General.GetItemTaxStructureHeader(_code);
           _lstTaxDet = CHNLSVC.General.getItemTaxStructure(txtTaxStucture.Text);
           if (_lstTaxDet != null && _lstTaxDet.Count > 0)
           {

               List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
               oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
               if (_lstTaxDet != null)
               {
                   foreach (mst_itm_tax_structure_det itemSer in _lstTaxDet)
                   {
                       if (oItemStaus != null && oItemStaus.Count > 0)
                       {
                           var nwlist = oItemStaus.Where(x => x.Mis_cd == itemSer.Its_stus).ToList();
                          // itemSer.Its_stus_Des = oItemStaus.Find(x => x.Mis_cd == itemSer.Its_stus).Mis_desc;
                           if (nwlist.Count>0)
                           {
                               itemSer.Its_stus_Des = nwlist.First().Mis_desc;
                           }
                         
                       }
                   }
               }

               grdTax.DataSource = _lstTaxDet;
               grdTax.DataBind();
               taxDetailspopup.Show();
           }
           else
           {
               DisplayMessage("Invalid Tax structure", 2);
               //MessageBox.Show("Invalid Tax structure", "Item Tax Structure", MessageBoxButtons.OK, MessageBoxIcon.Information);
               txtTaxStucture.Text = "";
               txtTaxStucture.Focus();
           }




           //List<mst_itm_tax_structure_det> _lstTaxDet = new List<mst_itm_tax_structure_det>();
           //_lstTaxDet = CHNLSVC.General.getItemTaxStructure(txtTaxStucture.Text);
           //if (_lstTaxDet != null && _lstTaxDet.Count > 0)
           //{

           //    List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
           //    oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
           //    if (_lstTaxDet != null)
           //    {
           //        foreach (mst_itm_tax_structure_det itemSer in _lstTaxDet)
           //        {
           //            if (oItemStaus != null && oItemStaus.Count > 0)
           //            {
           //                itemSer.Its_stus_Des = oItemStaus.Find(x => x.Mis_cd == itemSer.Its_stus).Mis_desc;
           //            }
           //        }
           //    }

           //}
           //else
           //{
           //    DisplayMessage("Invalid Tax structure", 2);
           //    txtTaxStucture.Text = "";
           //    txtTaxStucture.Focus();
           //}
       }

       public void LoadCompanyByModel()
       {

        
           grdComModel.DataSource = null;
           _lstcomModel = CHNLSVC.General.GetCompanyByModel(txtModel.Text);
           if (_lstcomModel != null)
           {
               grdComModel.DataSource = new List<mst_commodel>();

               grdComModel.DataSource = _lstcomModel;
              

           }
           grdComModel.DataBind();
       }

       public void LoadReplacedModelsByModel()
       {

           List<mst_model_replace> _comModel = new List<mst_model_replace>();
           grdRepalced.DataSource = null;
           _comModel = CHNLSVC.General.GetReplacedModelsByModel(txtModel.Text);
           if (_comModel != null)
           {
               grdRepalced.DataSource = new List<mst_model_replace>();
               grdRepalced.DataSource = _comModel;
               
           }
           grdRepalced.DataBind();
       }

       public void LoadModelClassificationsByModel()
       {

           List<BusinessEntityVal> _bisEntity = new List<BusinessEntityVal>();
           grdBusinessEntity.DataSource = null;
           _bisEntity = CHNLSVC.General.GetModelClassificationByModel(txtModel.Text);
           if (_bisEntity != null)
           {
               grdBusinessEntity.DataSource = new List<BusinessEntityVal>();
               grdBusinessEntity.DataSource = _bisEntity;
               
           }
           grdBusinessEntity.DataBind();
       }

       protected void _Mandatory_CheckedChanged(object sender, EventArgs e)
       {

          
       }


       private void LoadDefaultValues()
       {
           List<MasterUOM> uom = CHNLSVC.General.GetItemUOM();
           if (uom.Count >0)
           {
               drpuom.DataSource = uom;
               drpuom.DataTextField = "msu_cd";
               drpuom.DataValueField = "msu_cd";
               drpuom.DataBind();
               drpuom.SelectedValue = "NOS";

               drpconuom.DataSource = uom;
               drpconuom.DataTextField = "msu_cd";
               drpconuom.DataValueField = "msu_cd";
               drpconuom.DataBind();
           }

           DataTable dt = new DataTable();
           dt = CHNLSVC.Sales.GetBusinessEntityTypes("M");
           List<FastForward.SCMWeb.View.MasterFiles.Operational.CustomerCreation.BusinessEntityTYPE> bindtypeList 
               = new List<FastForward.SCMWeb.View.MasterFiles.Operational.CustomerCreation.BusinessEntityTYPE>();

           if (dt.Rows.Count > 0)
           {
               foreach (DataRow r in dt.Rows)
               {
                   // Get the value of the wanted column and cast it to string 
                   string TP = Convert.ToString(r["RBT_TP"]);
                   string DESC = Convert.ToString(r["RBT_DESC"]); //rbv_val
                   Boolean isMandetory = Convert.ToBoolean(r["RBT_MAD"]);
                   FastForward.SCMWeb.View.MasterFiles.Operational.CustomerCreation.BusinessEntityTYPE bizTP = new FastForward.SCMWeb.View.MasterFiles.Operational.CustomerCreation.BusinessEntityTYPE();
                   if (isMandetory)
                       bizTP.IsMandatory = "True";
                   else
                       bizTP.IsMandatory = "False";
                   bizTP.TypeCD_ = TP;
                   bizTP.TypeDesctipt = DESC;
                   bindtypeList.Add(bizTP);

               }
           }

           grdModelSegmentation.DataSource = null;
           grdModelSegmentation.DataSource = new List<FastForward.SCMWeb.View.MasterFiles.Operational.CustomerCreation.BusinessEntityTYPE>();
           grdModelSegmentation.DataSource = bindtypeList;
           grdModelSegmentation.DataBind();

           foreach (DataRow row in dt.Rows)
           {
               string typeName = row["RBT_TP"].ToString();

               DataTable dtVal = new DataTable();
               dtVal = CHNLSVC.Sales.GetBusinessEntityAllValues("M", typeName);

               if ((dtVal != null) && (dtVal.Rows.Count > 0))
               {
                   foreach (GridViewRow rows in grdModelSegmentation.Rows)
                   {
                       Label sd = (Label)rows.FindControl("rbt_tp");
                       CheckBox ch = (CheckBox)rows.FindControl("_Mandatory");
                       if (sd.Text == typeName)
                       {

                           DropDownList ddlAgeVals = (DropDownList)rows.FindControl("ddlAgeVals");
                           ch.Checked = false;
                           ddlAgeVals.DataSource = dtVal;
                           ddlAgeVals.DataTextField = "RBV_VAL";
                           ddlAgeVals.DataValueField = "RBV_VAL";
                           ddlAgeVals.DataBind();
                           ddlAgeVals.Items.Insert(0, "--Select--");

                       }

                   }
               }

           }


         
       }


       protected void lbtnAddModelSegment_Click(object sender, EventArgs e)
       {
           if (string.IsNullOrEmpty(txtModel.Text))
           {
               DisplayMessage("Please enter the model ", 2);
               return;
           }

           DataTable dt = new DataTable();
           dt.Columns.AddRange(new DataColumn[2] { new DataColumn("TypeCD_"), new DataColumn("TypeDesctipt") });

           List<BusinessEntityVal> _lstBusinessEntity = new List<BusinessEntityVal>();
           BusinessEntityVal _busEntity = new BusinessEntityVal();
          
           foreach (GridViewRow row in grdModelSegmentation.Rows)
           {
               if (row.RowType == DataControlRowType.DataRow)
               {
                   CheckBox chkRow = (row.Cells[0].FindControl("_Mandatory") as CheckBox);
                   if (chkRow.Checked)
                   {
                       string rbt_tp = (row.Cells[2].FindControl("rbt_tp") as Label).Text;
                       string rbt_val = (row.Cells[2].FindControl("ddlAgeVals") as DropDownList).SelectedItem.Text;
                       int rbt_selected_index = (row.Cells[2].FindControl("ddlAgeVals") as DropDownList).SelectedIndex;
                       string rbt_com = txtModelSegemntCompany.Text;
                       //dt.Rows.Add(name, country);
                       if (rbt_selected_index <= 0)
                       {
                           DisplayMessage("Please select a model segmentation value !", 2);
                           return;
                       }
                       _busEntity.rbv_cate = "M";
                       _busEntity.rbv_tp = rbt_tp;
                       _busEntity.rbv_val = rbt_val;
                       _busEntity.rbv_com = rbt_com;
                       _busEntity.model = txtModel.Text;

                       _lstBusinessEntity.Add(_busEntity);

                       grdBusinessEntity.DataSource = null;
                       grdBusinessEntity.DataSource = new List<BusinessEntityVal>();
                       grdBusinessEntity.DataSource = _lstBusinessEntity;
                       grdBusinessEntity.DataBind();

                       Session["BusinessEntityVal"] = _lstBusinessEntity;

                   }
               }
           }
       }


       protected void grdResult2_SelectedIndexChanged(object sender, EventArgs e)
       {
           ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
           string ID = grdResult2.SelectedRow.Cells[1].Text;
           string Des = grdResult2.SelectedRow.Cells[2].Text;

           if (lblvalue.Text == "Pc_HIRC_Company3")
           {
               string details = grdResult2.SelectedRow.Cells[2].Text;
               txtModelSegemntCompany.Text = ID;
               return;
           }
           if (lblvalue.Text == "UOMCOM")
           {
               string details = grdResult2.SelectedRow.Cells[2].Text;
               txtuomcom.Text = ID;
               return;
           }

       }

       protected void grdResult2_PageIndexChanging(object sender, GridViewPageEventArgs e)
       {
           grdResult.PageIndex = e.NewPageIndex;

           if ((lblvalue.Text == "Pc_HIRC_Company") || (lblvalue.Text == "Pc_HIRC_Company2") || (lblvalue.Text == "Pc_HIRC_Company3")
                   || (lblvalue.Text == "Pc_HIRC_Company4") || (lblvalue.Text == "Pc_HIRC_Company5") || (lblvalue.Text == "Pc_HIRC_Company9")
                   || (lblvalue.Text == "Pc_HIRC_Company6") || (lblvalue.Text == "Pc_HIRC_Company7") || (lblvalue.Text == "Pc_HIRC_Company8")  || (lblvalue.Text == "UOMCOM"))
           {
               string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
               DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey2.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
               grdResult.DataSource = _result;
               grdResult.DataBind();
               UserPopoup.Show();
               ViewState["SEARCH"] = _result;
               return;
           }


       }

       protected void btnuomadd_Click(object sender, EventArgs e)
       {
           try
           {
               UnitConvert obuom = new UnitConvert();
               if (uomlist==null)
               {
                   uomlist = new List<UnitConvert>();
               }
               if (txtModel.Text.ToString()=="")
               {
                   DisplayMessage("Please enter the model ", 2);
                   return;
               }
               var count = uomlist.Where(a => (a.mmu_con_uom == drpconuom.SelectedValue.ToString() && a.mmu_com == txtuomcom.Text.ToString()) || a.mmu_com == txtuomcom.Text.ToString()).Count();
               if (count > 0)
               {
                   DisplayMessage("Already Added!!!!", 2);
                   return;
               }
               obuom.mmu_com = txtuomcom.Text.ToString();
               obuom.mmu_act = 1;
               obuom.mmu_con_uom = drpconuom.SelectedValue.ToString();
               obuom.mmu_cre_by = Session["UserID"].ToString();
               obuom.mmu_cre_dt = DateTime.Now.Date;
               obuom.mmu_mod_by = Session["UserID"].ToString();
               obuom.mmu_mod_dt = DateTime.Now.Date;
               obuom.mmu_model = txtModel.Text.ToString();
               obuom.mmu_model_uom = drpuom.SelectedValue.ToString();
               obuom.mmu_qty = Convert.ToDecimal(txtqty.Text.ToString());

               uomlist.Add(obuom);

               grduomdata.DataSource = uomlist;
               grduomdata.DataBind();
               drpuom.Enabled = false;
           }catch(Exception ex)
           {
               DisplayMessage(ex.Message,2);
           }
       }

       protected void lbtnuomcom_Click(object sender, EventArgs e)
       {
           try
           {
               string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
               DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
               grdResult.DataSource = _result;
               grdResult.DataBind();
               BindUCtrlDDLData(_result);
               lblvalue.Text = "UOMCOM";
               UserPopoup.Show();
           }
           catch (Exception ex)
           {
               string _Msg = "Error Occurred while processing..!";
               DisplayMessage(_Msg, 4);
           }
       }

       protected void btnuomdelete_Click(object sender, EventArgs e)
       {
         // uomlist = uomlist.Remove(Convert.ToInt32( e.CommandArgument));
           if (txtdelete.Value == "Yes")
           {
               GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
               Label conuom = (Label)row.FindControl("conuom");
               Label muomcom = (Label)row.FindControl("muomcom");
               var itemToRemove = uomlist.Single(r => r.mmu_con_uom == conuom.Text.ToString() && r.mmu_com == muomcom.Text.ToString());
               uomlist.Remove(itemToRemove);

               grduomdata.DataSource = uomlist;
               grduomdata.DataBind();
           }
           else
           {
               return;
           }
         
       }

       private void LoadUOM( string model)
       {
           uomlist = null;
           uomlist = CHNLSVC.General.GetModelUOM(model);
           grduomdata.DataSource = uomlist;
           grduomdata.DataBind();

       }

       protected void txtuomcom_TextChanged(object sender, EventArgs e)
       {
           string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
           DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
           int count = _result.AsEnumerable()
       .Count(row => row.Field<string>("Code") == txtuomcom.Text.ToString());
           if (count == 0)
           {
               DisplayMessage("Please Enter Valid Company", 2);
               txtuomcom.Text = "";
               txtuomcom.Focus();
               return;
           }
       }

       protected void txtqty_TextChanged(object sender, EventArgs e)
       {
           decimal n;
           bool isNumeric = decimal.TryParse(txtqty.Text.ToString(), out n);

           if (n == 0)
           {
               DisplayMessage("Plese Enter Valid Qty",2);
               txtqty.Text = "";
               txtqty.Focus();
               return;
           }
       }

       protected void txtbrand_TextChanged(object sender, EventArgs e)
       {
           if (string.IsNullOrEmpty(txtbrand.Text))
           {
               return;
           }

           string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
           DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(SearchParams, null, null);

           var _validate = _result.AsEnumerable().Where(x => x.Field<string>("Code") == txtbrand.Text.Trim()).ToList();
           if (_validate == null || _validate.Count <= 0)
           {
               DisplayMessage("Invalid brand", 2);
               txtbrand.Text = string.Empty;
               txtbrand.Focus();
               return;
           }
       }

       protected void btn_brand_Click(object sender, EventArgs e)
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
               string _Msg = "Error Occurred while processing..!";
               DisplayMessage(_Msg, 4);
           }
       }

       protected void btcomdelete_Click(object sender, EventArgs e)
       {
           if (txtdelete.Value == "Yes")
           {
               GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
               Label COM = (Label)row.FindControl("ci_com");
               var itemToRemove = _lstcomModel.Single(r => r.Mcm_com==COM.Text.ToString());
               _lstcomModel.Remove(itemToRemove);

               grdComModel.DataSource = _lstcomModel;
               grdComModel.DataBind();
           }
           else
           {
               return;
           }
       }

       protected void lbtncountry_Click(object sender, EventArgs e)
       {
           try
           {
               string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
               DataTable _result = CHNLSVC.CommonSearch.GetCountrySearchData(SearchParams, null, null);
               grdResult.DataSource = _result;
               grdResult.DataBind();
               BindUCtrlDDLData(_result);
               lblvalue.Text = "CountyOfOrigin";
               UserPopoup.Show();
           }
           catch (Exception ex)
           {
               string _Msg = "Error Occurred while processing..!";
               DisplayMessage(_Msg, 4);
           }
         
       }

       protected void txtcountryoforign_TextChanged(object sender, EventArgs e)
       {
           string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
           DataTable result = CHNLSVC.CommonSearch.GetCountrySearchData(SearchParams, "CODE", txtcountryoforign.Text.Trim());
           if (result != null)
           {
               if (result.Rows.Count==0)
               {
                   DisplayMessage("Invalid Country Code", 1);
                   txtcountryoforign.Text = "";
                   return;
               }
           }
           else
           {
               DisplayMessage("Invalid Country Code", 1);
               txtcountryoforign.Text = "";
               return;
           }
       }
       public bool validateinputString(string input)
       {
           Match match = Regex.Match(input, @"([~!@$%^&*]+)$", RegexOptions.IgnoreCase);
           if (match.Success)
           {
               return false;
           }
           return true;
       }
       public bool validateinputStringWithSpace(string input)
       {
           Match match = Regex.Match(input, @"([~!@$%^&*]+)$", RegexOptions.IgnoreCase);
           if (match.Success)
           {
               return false;
           }
           return true;
       }

       protected void txtModelDes_TextChanged(object sender, EventArgs e)
       {
           if (!validateinputStringWithSpace(txtModelDes.Text))
           {
               DisplayMessage("Invalid charactor found in model description.", 2);
               txtModelDes.Focus();
               return;
           }
       }

       protected void txtrepDes_TextChanged(object sender, EventArgs e)
       {
           if (!validateinputStringWithSpace(txtrepDes.Text))
           {
               DisplayMessage("Invalid charactor found in replace model description.", 2);
               txtrepDes.Focus();
               return;
           }
       }

       protected void txtModelComDes_TextChanged(object sender, EventArgs e)
       {
           if (!validateinputStringWithSpace(txtModelComDes.Text))
           {
               DisplayMessage("Invalid charactor found in company description.", 2);
               txtModelComDes.Focus();
               return;
           }
       }

       protected void txthscode_TextChanged(object sender, EventArgs e)
       {

       }

       protected void lbtnhssearch_Click(object sender, EventArgs e)
       {


           try
           {
               string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HsCode);
               DataTable _result = CHNLSVC.CommonSearch.SearchGetHsCode(para, null, null);
               grdResult.DataSource = _result;
               grdResult.DataBind();
               BindUCtrlDDLData(_result);
               lblvalue.Text = "HsCode";
               UserPopoup.Show();
           }
           catch (Exception ex)
           {
               string _Msg = ex.Message;
               DisplayMessage(_Msg, 4);
           }
       }
    }
}