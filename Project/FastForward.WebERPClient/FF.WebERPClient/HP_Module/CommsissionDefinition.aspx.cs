using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using FF.BusinessObjects;
using Oracle.DataAccess.Client;

namespace FF.WebERPClient.HP_Module
{   

    public partial class CommsissionDefinition : BasePage
    {
        public List<PriceBookLevelRef> PBList
        {
            get { return (List<PriceBookLevelRef>)Session["PBList"]; }
            set { Session["PBList"] = value; }
        }
        public List<CashCommissionDetailRef> ExcecList
        {
            get { return (List<CashCommissionDetailRef>)Session["ExcecList"]; }
            set { Session["ExcecList"] = value; }
        }
        public List<string> ClonePcList
        {
            get { return (List<string>)Session["ClonePcList"]; }
            set { Session["ClonePcList"] = value; }
        }
        public List<CashCommissionDetailRef> ItemBrandCat_List
        {
            get { return (List<CashCommissionDetailRef>)Session["ItemBrandCat_List"]; }
            set { Session["ItemBrandCat_List"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           // txtAmtComAmt.Text = txtOthComAmt.Text.Trim() + txtOthComRt.Text.Trim();
           // uc_ProfitCenterSearch1.Descript_Visible(false);
            if(!IsPostBack)
            {
               // uc_ProfitCenterSearch1.Descript_Visible(false);
                Initialize();
                txtFromDt.Text = DateTime.Now.ToShortDateString();
                txtToDt.Text = DateTime.Now.ToShortDateString();
                resetCommisionTextBoxes();
            }
        }
        private void resetCommisionTextBoxes()
        {
            txtCashComRt.Text= Convert.ToString(0);
            txtCashComAmt.Text = Convert.ToString(0); 

            txtCrCdComRt.Text= Convert.ToString(0);
            txtCrCdComAmt.Text= Convert.ToString(0);

            txtCrCdProComRt.Text= Convert.ToString(0);
            txtCrCdProComAmt.Text= Convert.ToString(0);

            txtChqComRt.Text= Convert.ToString(0); 
            txtChqComAmt.Text= Convert.ToString(0); 

            txtGVComRt.Text= Convert.ToString(0); 
            txtGVComAmt.Text= Convert.ToString(0); 

            txtDBCComRt.Text= Convert.ToString(0); 
            txtDBCComAmt.Text= Convert.ToString(0); 

            txtOthComRt.Text= Convert.ToString(0); 
            txtOthComAmt.Text= Convert.ToString(0); 

           // txtAmtComRt.Text= Convert.ToString(0); 
           // txtAmtComAmt.Text= Convert.ToString(0); 
            //------------------------------------------------------------------------------------
            txtExCashComRt.Text= Convert.ToString(0); 
            txtExCashComAmt.Text= Convert.ToString(0); 

            txtExCrCdComRt.Text= Convert.ToString(0); 
            txtExCrCdComAmt.Text= Convert.ToString(0); 

            txtExCrCdProComRt.Text= Convert.ToString(0); 
            txtExCrCdProComAmt.Text= Convert.ToString(0); 

            txtExChqComRt.Text= Convert.ToString(0); 
            txtExChqComAmt.Text= Convert.ToString(0); 

            txtExGVComRt.Text= Convert.ToString(0); 
            txtExGVComAmt.Text= Convert.ToString(0); 

            txtExDBCComRt.Text= Convert.ToString(0); 
            txtExDBCComAmt.Text= Convert.ToString(0); 

            txtExOthComRt.Text= Convert.ToString(0); 
            txtExOthComAmt.Text= Convert.ToString(0); 

           // txtExAmtComRt.Text= Convert.ToString(0);
           // txtExAmtComAmt.Text= Convert.ToString(0);
        }
        private void Initialize()
        {
            DataTable dt = new DataTable();
            grvExcecutive.DataSource = dt;
            grvExcecutive.DataBind();
            grvPB_PBL.DataSource = dt;
            grvPB_PBL.DataBind();

            grvItmSelect.DataSource = dt;
            grvItmSelect.DataBind();
            //---------------------
            PBList = new List<PriceBookLevelRef>();
            ExcecList = new List<CashCommissionDetailRef>();
            ItemBrandCat_List = new List<CashCommissionDetailRef>();
            ClonePcList = new List<string>();
            grvClonePc.DataSource = ClonePcList;
            grvClonePc.DataBind();

        }
        protected void btnAddToPC_list_Click(object sender, ImageClickEventArgs e)
        {
            string com = uc_ProfitCenterSearch1.Company;
            string chanel = uc_ProfitCenterSearch1.Channel;
            string subChanel = uc_ProfitCenterSearch1.SubChannel;
            string area = uc_ProfitCenterSearch1.Area;
            string region = uc_ProfitCenterSearch1.Region;
            string zone = uc_ProfitCenterSearch1.Zone;
            string pc = uc_ProfitCenterSearch1.ProfitCenter;

            DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com, chanel, subChanel, area, region, zone, pc);
            grvProfCents.DataSource = dt;
            grvProfCents.DataBind();

        }
     
        protected void btnNone_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in grvProfCents.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                chkSelect.Checked = false;
            }
        }
        protected void btnClearPcList_Click(object sender, EventArgs e)
        {
            grvProfCents.DataSource = null;
            grvProfCents.DataBind();
        }
        protected void grvProfCents_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chekMand = (CheckBox)e.Row.FindControl("chekPc");
                if (!chekMand.Checked)
                {
                    chekMand.Checked = true;
                }
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HP_Module/CommsissionDefinition.aspx");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in grvProfCents.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                chkSelect.Checked = true;
            }
        }
        #region Searchin
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {


                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {

                        paramsText.Append(GlbUserComCode + seperator + txtPriceBook.Text.Trim() + seperator);
                        // paramsText.Append(GlbUserComCode + seperator + "%" + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Circular:
                    {
                        paramsText.Append("" + seperator + "Circular" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Promotion:
                    {
                        paramsText.Append(txtCircular.Text.Trim().ToUpper() + seperator + "Promotion" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EmployeeCate:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EmployeeEPF:
                    {
                        paramsText.Append(GlbUserComCode + seperator + txtExcecType.Text.Trim().ToUpper());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Sales_Type:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion
      
       
        protected void imgBtnLevelSearch_Click1(object sender, ImageClickEventArgs e)
        {
            //PriceLevel
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPriceLevelByBookData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtLevel.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgBtnPBsearch_Click1(object sender, ImageClickEventArgs e)
        {
            
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPriceBook.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        
        }

        protected void ImgBtnAddPB_Click(object sender, ImageClickEventArgs e)
        {
            List<PriceBookLevelRef> pbLIST = new List<PriceBookLevelRef>();
            pbLIST=(CHNLSVC.Sales.GetPriceLevelList(GlbUserComCode, txtPriceBook.Text.Trim().ToUpper(), txtLevel.Text.Trim().ToUpper()));

            pbLIST.RemoveAll(x => x.Sapl_act == false);
            var distinctList = pbLIST.GroupBy(x => x.Sapl_pb_lvl_cd)
                         .Select(g => g.First())
                         .ToList();

            PBList.AddRange(distinctList);
           // PriceBookLevelRef pb = new PriceBookLevelRef();
           // pb.Sapl_pb=txtPriceBook.Text.Trim();
           // pb.Sapl_pb_lvl_cd= txtLevel.Text.Trim();
           // pb.Sequence=
           // PBList.Add(pb);
            grvPB_PBL.DataSource = PBList;
            grvPB_PBL.DataBind();
        }

        protected void grvPB_PBL_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 rowIndex = e.RowIndex;            
            string PBook = Convert.ToString(grvPB_PBL.Rows[rowIndex].Cells[1].Text);
            string PBLevel = Convert.ToString(grvPB_PBL.Rows[rowIndex].Cells[2].Text);
            //PBList.RemoveAll(x => x.Sapl_pb == PBook && x.Sapl_pb_lvl_cd == PBLevel);
            PBList.RemoveAt(rowIndex);
            grvPB_PBL.DataSource = PBList;
            grvPB_PBL.DataBind();            
           
        }

        protected void grvExcecutive_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 rowIndex = e.RowIndex;
            string ExcecType = Convert.ToString(grvExcecutive.Rows[rowIndex].Cells[1].Text);
            string ExcecCode = Convert.ToString(grvExcecutive.Rows[rowIndex].Cells[2].Text);
            ExcecList.RemoveAt(rowIndex);
            grvExcecutive.DataSource = ExcecList;
            grvExcecutive.DataBind();
        }

        protected void ImgBtnAddExcec_Click(object sender, ImageClickEventArgs e)
        {
            CashCommissionDetailRef ccd = new CashCommissionDetailRef();
            ccd.Sccd_exec_tp= txtExcecType.Text.Trim();
            ccd.Sccd_exec_cd = txtExcecCd.Text.Trim();
            ExcecList.Add(ccd);
            grvExcecutive.DataSource = ExcecList;
            grvExcecutive.DataBind();
        }

        protected void ImgBtnBrand_Click(object sender, ImageClickEventArgs e)
        {
           // _basePage = new BasePage();
           // Page pp = (Page)this.Page;
           // uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCat_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtBrand.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
            //CheckBoxBrand.Checked = true;


        }
       
        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCat_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtCate1.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        protected void ImgBtnSubCat_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCat_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtCate2.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
       

        protected void ImgBtnItem_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable dataSource = CHNLSVC.CommonSearch.GetItemSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtItemCD.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImgBtnAddCat_Click(object sender, ImageClickEventArgs e)
        { 
            //if(txtSerial.Text.Trim()!=string.Empty)
            //{
            //    grvItmSelect.HeaderRow.Cells[1].Text = "Serial #";
            //    List<string>
            //    grvItmSelect
            //}
            //if (txtSerial.Text.Trim() != string.Empty)
            //{
            //    grvItmSelect.HeaderRow.Cells[1].Text = "Promotion Code";
            //    return;
            //}
            if (ddlSelectCat.SelectedValue == "BRAND_CATE1" || ddlSelectCat.SelectedValue == "BRAND_CATE2")
            {
                if(txtBrand.Text==string.Empty)
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Specify brand also!");
                    return;
                }
            }
            string selection =ddlSelectCat.SelectedValue;
            DataTable dt = CHNLSVC.Sales.GetBrandsCatsItems(selection, txtBrand.Text.Trim(), txtCate1.Text.Trim(), txtCate2.Text.Trim(), null, txtItemCD.Text.Trim(), txtSerial.Text.Trim(), txtCircular.Text.Trim(), txtPromotion.Text.Trim());
                        
            if (dt.Rows.Count > 0)
            {                
                grvItmSelect.HeaderRow.Cells[1].Text = ddlSelectCat.SelectedItem.Text;
            }
            List<CashCommissionDetailRef> addList = new List<CashCommissionDetailRef>();
            foreach(DataRow dr in dt.Rows)
            {
                string code= dr["code"].ToString();
                string brand = txtBrand.Text;
                CashCommissionDetailRef obj = new CashCommissionDetailRef(); //for display purpose
                if (ddlSelectCat.SelectedValue == "BRAND_CATE1" || ddlSelectCat.SelectedValue == "BRAND_CATE2")
                {
                    obj.Sccd_brd = brand;
                }
                else
                {
                    obj.Sccd_brd = "N/A";
                }
                               
                obj.Sccd_itm = code;
                try
                { 
                    obj.Sccd_ser = dr["descript"].ToString();
                }
                catch(Exception ex)
                {
                    obj.Sccd_ser = "";
                }
               
                var _duplicate = from _dup in ItemBrandCat_List
                                 where _dup.Sccd_itm == obj.Sccd_itm && _dup.Sccd_brd == obj.Sccd_brd 
                                 select _dup;
                if (_duplicate.Count() == 0)
                {
                    addList.Add(obj);
                }                      
                
            }
            ItemBrandCat_List.AddRange(addList);
            
            grvItmSelect.DataSource = ItemBrandCat_List;
            grvItmSelect.DataBind();
            if (dt.Rows.Count > 0)
            {
              grvItmSelect.HeaderRow.Cells[1].Text = ddlSelectCat.SelectedItem.Text; 
            }
                     

        }
        private List<PriceBookLevelRef>  GetSelectedPriceBookList()
        {
           // List<PriceBookLevelRef> list = new List<PriceBookLevelRef>();
           return PBList;
        }
        private List<CashCommissionDetailRef> GetSelectedExcecutiveList()
        {           
            return ExcecList;
        }
        private List<string> GetSelectedPCList()
        {
            List<string> list = new List<string>();
            foreach (GridViewRow gvr in grvProfCents.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                if (chkSelect.Checked)
                {
                    list.Add(gvr.Cells[0].Text);
                }
            }
            return list;
        }
        private List<CashCommissionDetailRef> GetSelected_BrandCateItemList()
        {
            //List<CashCommissionDetailRef> list = new List<CashCommissionDetailRef>();
            List<string> list = new List<string>();
            
            foreach (GridViewRow gvr in grvItmSelect.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkCateSelect");
                string ITM_CD = gvr.Cells[1].Text.Trim();
                string brand_ = gvr.Cells[2].Text.Trim();
                //if (chkSelect.Checked)
                //{
                    
                //    list.Add(gvr.Cells[1].Text);
                //}
                if (chkSelect.Checked==false)
                {
                    ItemBrandCat_List.RemoveAll(x => x.Sccd_itm == ITM_CD && x.Sccd_brd == brand_);
                  
                }
            }
            return ItemBrandCat_List;
        }
        protected void btnProcess_Click(object sender, EventArgs e)
        {
            #region validation 07-01-2013
            Decimal r1= Convert.ToDecimal(txtCashComRt.Text);
            Decimal r2 = Convert.ToDecimal(txtCrCdComRt.Text);
            Decimal r3 = Convert.ToDecimal(txtCrCdProComRt.Text);
            Decimal r4 = Convert.ToDecimal(txtChqComRt.Text);
            Decimal r5 = Convert.ToDecimal(txtGVComRt.Text);
            Decimal r6 = Convert.ToDecimal(txtDBCComRt.Text);
            Decimal r7 = Convert.ToDecimal(txtOthComRt.Text);

            Decimal r8 = Convert.ToDecimal(txtExCashComRt.Text);
            Decimal r9 = Convert.ToDecimal(txtExCrCdComRt.Text);
            Decimal r10 = Convert.ToDecimal(txtExCrCdProComRt.Text);
            Decimal r11 = Convert.ToDecimal(txtExChqComRt.Text);
            Decimal r12 = Convert.ToDecimal(txtExGVComRt.Text);
            Decimal r13 = Convert.ToDecimal(txtExDBCComRt.Text);
            Decimal r14 = Convert.ToDecimal(txtExOthComRt.Text);

            Decimal tot = r1 + r2 + r3 + r4 + r5 + r6 + r7 + r8 + r9 + r10 + r11 + r12 + r13 + r14;
            if (tot>1400)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid 'Rate'found. Any rate should be less or equel to 100!");
                return;
            }
            #endregion

            List<string> Selected_PC_List = new List<string>();
            Selected_PC_List = GetSelectedPCList();
            //if (Selected_PC_List.Count<1)
            //{
            //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Define Price Books/Levels!");

            //    string Msg = "<script>alert('Successfully Saved!' );</script>";
            //    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            //    return;
            //}

            List<PriceBookLevelRef> Selected_PBook_List = new List<PriceBookLevelRef>();
            Selected_PBook_List = GetSelectedPriceBookList();

            List<CashCommissionDetailRef> Selected_ExcecutiveList = new List<CashCommissionDetailRef>();
            Selected_ExcecutiveList = GetSelectedExcecutiveList();

            List<CashCommissionDetailRef> Selected_BrandCatItmList = new List<CashCommissionDetailRef>() ;
            Selected_BrandCatItmList = GetSelected_BrandCateItemList();
            string ItmSelectType= grvItmSelect.HeaderRow.Cells[1].Text ;
            if (ItmSelectType.ToUpper()!="VALUE")//
            {
                try
                {
                    ItmSelectType = ddlSelectCat.Items.FindByText(ItmSelectType).Value;
                    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please compleate necessary data!");
                }
                catch (Exception EX)
                {
                    return;
                }
            }
            
            //DataTable dt_itms = CHNLSVC.Sales.GetBrandsCatsItems(selection, txtBrand.Text.Trim(), txtCate1.Text.Trim(), txtCate2.Text.Trim(), null, txtItemCD.Text.Trim());
            //foreach (DataRow dr in dt_itms.Rows)
            //{ 
                
            //}
            Dictionary<string, Decimal> commission_values = getCommissionValues();
            DateTime frmDT = Convert.ToDateTime(txtFromDt.Text.Trim());
            DateTime toDT = Convert.ToDateTime(txtToDt.Text.Trim());

            //------------------------------------------------------------
            MasterAutoNumber masterAuto = new MasterAutoNumber();
           // masterAuto.Aut_cate_cd = GlbUserName;
            //masterAuto.Aut_cate_tp = "PC";
            //masterAuto.Aut_direction = 1;
           // masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "COMMIS";
            masterAuto.Aut_number = 0;//what is Aut_number
            masterAuto.Aut_start_char = "COMM";
           // masterAuto.Aut_year = null;


            //MasterAutoNumber _auto = new MasterAutoNumber();
            ////_auto.Aut_cate_cd = _businessEntity.Mbe_cre_pc;//_invoiceHeader.Sah_pc;
            ////_auto.Aut_cate_tp = "PRO";
            //_auto.Aut_moduleid = "CUS";
            //_auto.Aut_number = 0;
            //_auto.Aut_start_char = "CONT";
            //MasterAutoNumber _auto = new MasterAutoNumber();
            //_auto.Aut_cate_cd = _invoiceHeader.Sah_pc;
            //_auto.Aut_cate_tp = "PRO";
            //_auto.Aut_moduleid = "CUS";
            //_auto.Aut_number = 0;
            //_auto.Aut_start_char = "CONT";

            //_MainTxnAuto = new MasterAutoNumber();
            //_MainTxnAuto.Aut_cate_cd = GlbUserDefProf;
            //_MainTxnAuto.Aut_cate_tp = "PC";
            //_MainTxnAuto.Aut_direction = 1;
            //_MainTxnAuto.Aut_modify_dt = null;
            //_MainTxnAuto.Aut_moduleid = "HP";
            //_MainTxnAuto.Aut_number = 0;
            //_MainTxnAuto.Aut_start_char = "HPT";
            //_MainTxnAuto.Aut_year = Convert.ToDateTime(DateTime.Now).Year;
            //------------------------------------------------------------
            CashCommissionHeaderRef commHeader = new CashCommissionHeaderRef();
            commHeader.Scch_cd = null;
            commHeader.Scch_circular = txtCircularCd.Text.Trim().ToUpper();
            commHeader.Scch_cre_by = GlbUserName;
            commHeader.Scch_cre_dt = DateTime.Now.Date;
            commHeader.Scch_desc = txtCircularDes.Text.Trim().ToUpper();
            commHeader.Scch_sale_tp = txtCircularType.Text.Trim().ToUpper();
            //commHeader.Scch_seq = 0;
            string comm_code = "";
            Int32 effect = CHNLSVC.Sales.saveTempTablesForCommision(commHeader, Selected_PC_List, Selected_PBook_List, Selected_ExcecutiveList, Selected_BrandCatItmList, ItmSelectType, GlbUserName, txtCircularCd.Text.Trim().ToUpper(), commission_values, frmDT, toDT, masterAuto, out comm_code);

            if (effect>0)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Process Completed!");

               // string Msg = "<script>alert('Process Completed!' );</script>";
                string Msg = "<script>alert('Process Completed! Commission Code: " + comm_code + "');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                return;
            }
        }
        protected Dictionary<string, Decimal> getCommissionValues()
        {
            Dictionary<string, Decimal> commissionValues = new Dictionary<string, decimal>();
            commissionValues.Add("cashRt", Convert.ToDecimal(txtCashComRt.Text.Trim()));
            commissionValues.Add("cashAmt", Convert.ToDecimal(txtCashComAmt.Text.Trim()));

            commissionValues.Add("credCardRt", Convert.ToDecimal(txtCrCdComRt.Text.Trim()));
            commissionValues.Add("credCardAmt", Convert.ToDecimal(txtCrCdComAmt.Text.Trim()));

            commissionValues.Add("credCardProRt", Convert.ToDecimal(txtCrCdProComRt.Text.Trim()));
            commissionValues.Add("credCardProAmt", Convert.ToDecimal(txtCrCdProComAmt.Text.Trim()));

            commissionValues.Add("chequeRt", Convert.ToDecimal(txtChqComRt.Text.Trim()));
            commissionValues.Add("chequeAmt", Convert.ToDecimal(txtChqComAmt.Text.Trim()));

            commissionValues.Add("gvRt", Convert.ToDecimal(txtGVComRt.Text.Trim()));
            commissionValues.Add("gvAmt", Convert.ToDecimal(txtGVComAmt.Text.Trim()));

            commissionValues.Add("dbcRt", Convert.ToDecimal(txtDBCComRt.Text.Trim()));
            commissionValues.Add("dbcAmt", Convert.ToDecimal(txtDBCComAmt.Text.Trim()));

            commissionValues.Add("otherRt", Convert.ToDecimal(txtOthComRt.Text.Trim()));
            commissionValues.Add("otherAmt", Convert.ToDecimal(txtOthComAmt.Text.Trim()));

           commissionValues.Add("amountRt", Convert.ToDecimal("0"));
           commissionValues.Add("amountAmt", Convert.ToDecimal("0"));
            //------------------------------------------------------------------------------------
            commissionValues.Add("EXcashRt", Convert.ToDecimal(txtExCashComRt.Text.Trim()));
            commissionValues.Add("EXcashAmt", Convert.ToDecimal(txtExCashComAmt.Text.Trim()));

            commissionValues.Add("EXcredCardRt", Convert.ToDecimal(txtExCrCdComRt.Text.Trim()));
            commissionValues.Add("EXcredCardAmt", Convert.ToDecimal(txtExCrCdComAmt.Text.Trim()));

            commissionValues.Add("EXcredCardProRt", Convert.ToDecimal(txtExCrCdProComRt.Text.Trim()));
            commissionValues.Add("EXcredCardProAmt", Convert.ToDecimal(txtExCrCdProComAmt.Text.Trim()));

            commissionValues.Add("EXchequeRt", Convert.ToDecimal(txtExChqComRt.Text.Trim()));
            commissionValues.Add("EXchequeAmt", Convert.ToDecimal(txtExChqComAmt.Text.Trim()));

            commissionValues.Add("EXgvRt", Convert.ToDecimal(txtExGVComRt.Text.Trim()));
            commissionValues.Add("EXgvAmt", Convert.ToDecimal(txtExGVComAmt.Text.Trim()));

            commissionValues.Add("EXdbcRt", Convert.ToDecimal(txtExDBCComRt.Text.Trim()));
            commissionValues.Add("EXdbcAmt", Convert.ToDecimal(txtExDBCComAmt.Text.Trim()));

            commissionValues.Add("EXotherRt", Convert.ToDecimal(txtExOthComRt.Text.Trim()));
            commissionValues.Add("EXotherAmt", Convert.ToDecimal(txtExOthComAmt.Text.Trim()));

            commissionValues.Add("EXamountRt",Convert.ToDecimal("0"));
            commissionValues.Add("EXamountAmt", Convert.ToDecimal("0"));

            return commissionValues;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HP_Module/CommsissionDefinition.aspx");
        }

        protected void btnCatNone_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in grvItmSelect.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkCateSelect");
                chkSelect.Checked = false;
            }
        }

        protected void btnCatAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in grvItmSelect.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkCateSelect");
                chkSelect.Checked = true;
            }
        }

        protected void btnCatClear_Click(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            ItemBrandCat_List.Clear();
            grvItmSelect.DataSource = ItemBrandCat_List;
            grvItmSelect.DataBind();
        }

        protected void ImgBtnCloneAdd_Click(object sender, ImageClickEventArgs e)
        {
            ClonePcList.Add(txtCloneAddPc.Text.Trim().ToUpper());
            grvClonePc.DataSource = ClonePcList;
            grvClonePc.DataBind();

            ModalPopupCloneCommsis_.Show();
        }

        protected void btnClone_Click(object sender, EventArgs e)
        {
            txtCloneAddPc.Text = string.Empty;
            txtClonePC.Text = string.Empty;
            ClonePcList = new List<string>();
            grvClonePc.DataSource = ClonePcList;
            grvClonePc.DataBind();

            ModalPopupCloneCommsis_.Show();           
            
        }

        protected void btnProcessClone_Click(object sender, EventArgs e)
        {
            this.MasterMsgInfoUCtrl.Clear();
            if (ClonePcList.Count > 0)
            {
                Int32 eff = CHNLSVC.Sales.Save_CloneCommissions(txtClonePC.Text.Trim().ToUpper(), ClonePcList, GlbUserName);
                if (eff > 0)
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cloning Compleated Successfully!");

                    // string Msg = "<script>alert('Process Completed!' );</script>";
                    string Msg = "<script>alert('Cloning Compleated Successfully!');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                }
                else
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Sorry. Failed to compleate. Please try again!");
                }
            }
            else
            {
                string Msg = "<script>alert('Please add profit centers to the cloning list.');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                return;
            }
        }

        protected void grvClonePc_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Int32 rowIndex = e.RowIndex;
            //string clone = Convert.ToString(grvClonePc.Rows[rowIndex].Cells[1].Text);
            //string ExcecCode = Convert.ToString(grvExcecutive.Rows[rowIndex].Cells[2].Text);
            //ExcecList.RemoveAt(rowIndex);
            //grvExcecutive.DataSource = ExcecList;
            //grvExcecutive.DataBind();
        }

        protected void ImgDelClone_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnCloneClose_Click(object sender, EventArgs e)
        {
              
        }

        protected void btnCloneClear_Click(object sender, EventArgs e)
        {

        }

        protected void ImgBtnCircular_Click(object sender, ImageClickEventArgs e)
        {
           // ClearItem();
           // _basePage = new BasePage();
           //// Page pp = (Page)this.Page;
            //uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circular);
            DataTable dataSource1 = CHNLSVC.CommonSearch.GetPromotionSearch(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource1);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource1);
            MasterCommonSearchUCtrl.ReturnResultControl = txtCircular.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
            

        }

        protected void ImgBtnSerial_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
            DataTable dataSource1 = CHNLSVC.CommonSearch.GetItemSerialSearchData(MasterCommonSearchUCtrl.SearchParams);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource1);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource1);
            MasterCommonSearchUCtrl.ReturnResultControl = txtSerial.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImgBtnPromo_Click(object sender, ImageClickEventArgs e)
        {
            //ClearItem();
           // _basePage = new BasePage();
            //Page pp = (Page)this.Page;
            //uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
            DataTable dataSource1 = CHNLSVC.CommonSearch.GetPromotionSearch(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource1);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource1);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPromotion.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
           // CheckBoxPromation.Checked = true;
        }

        protected void grvItmSelect_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (ddlSelectCat.SelectedValue == "BRAND_CATE1" || ddlSelectCat.SelectedValue == "BRAND_CATE2")
            //{
            //    //e.Row.Cells[2].Text = txtBrand.Text;
            //}
        }

        protected void ddlSelectCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemBrandCat_List.Clear();
            grvItmSelect.DataSource = ItemBrandCat_List;
            grvItmSelect.DataBind();
        }

        protected void ImgBtnExecTp_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EmployeeCate);
            DataTable dataSource1 = CHNLSVC.CommonSearch.Get_employee_categories(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource1);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource1);
            MasterCommonSearchUCtrl.ReturnResultControl = txtExcecType.ClientID; 
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImgBtnExecCd_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EmployeeEPF);
            DataTable dataSource1 = CHNLSVC.CommonSearch.Get_employee_EPF(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource1);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource1);
            MasterCommonSearchUCtrl.ReturnResultControl = txtExcecCd.ClientID; 
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImgBtnType_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_Type);
            DataTable dataSource1 = CHNLSVC.General.GetSalesTypes(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource1);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource1);
            MasterCommonSearchUCtrl.ReturnResultControl = txtCircularType.ClientID; 
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
      
    }
}