using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using FF.BusinessObjects;
using System.Globalization;

namespace FF.WebERPClient.Enquiry_Modules.Sales
{
    public partial class PriceDetails : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //  sp_getpricedetail
            #region

            List<RecieptItem> _recieptItems = new List<RecieptItem>();
            RecieptItem r1 = new RecieptItem(); //r1.Sard_seq_no = 1;
            RecieptItem r2 = new RecieptItem(); r2.Sard_seq_no = 2;
            RecieptItem r3 = new RecieptItem(); r3.Sard_seq_no = 3;
            RecieptItem r4 = new RecieptItem(); r4.Sard_seq_no = 4;
            RecieptItem r5 = new RecieptItem(); r5.Sard_seq_no = 5;
            RecieptItem r6 = new RecieptItem(); r6.Sard_seq_no = 6;

            _recieptItems.Add(r1);
            //_recieptItems.Add(r2);
            //_recieptItems.Add(r3);
            //_recieptItems.Add(r4);
            //_recieptItems.Add(r5);
            //_recieptItems.Add(r6);
            //    grvPriceDetail.DataSource = _recieptItems;
            //    grvPriceDetail.DataBind();

            if (!IsPostBack)
            {
                //  txtItemCode.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnSItemcd.ClientID + "')");
                txtCustomer.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnCustSearch.ClientID + "')");
                txtPriceBook.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnPBsearch.ClientID + "')");
                txtLevel.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnLevelSearch.ClientID + "')");
                txtSchemeCD.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnSchemSearch.ClientID + "')");
                txtType.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnTypeSearch.ClientID + "')");

               // txtFromDt.Text = DateTime.MinValue.Date.ToShortDateString().ToString();
                txtToDt.Text = DateTime.Now.ToShortDateString().ToString();
                txtFromDt.Text = DateTime.Now.AddYears(-1).ToShortDateString().ToString();
                txtAsAtDt.Text = DateTime.Now.ToShortDateString().ToString();
                rdoDateRange.Checked = true;
                div_Asat.Visible = false;
                divFromDate.Visible = true;
                divTodate.Visible = true;
                divSchemeCD.Visible = false;

                DataTable dt = new DataTable();
                grvPriceDetail.DataSource = dt;
                grvPriceDetail.DataBind();

                grvSchems.DataSource = dt;
                grvSchems.DataBind();

                
                
                
            }

            #endregion


        }

        protected void grvPriceDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvPriceDetail.PageIndex = e.NewPageIndex;
            grvPriceDetail.DataSource = null;
            Search();
            grvPriceDetail.DataBind();
        }

        protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            lblDescription.Text = "";
            lblModal.Text = "";
            lblVatRate.Text = "";

            if (rdoDateRange.Checked)
            {
                if (txtFromDt.Text == "" || txtToDt.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter date range!");
                    return;
                }
            }
            else
            {
                if (txtAsAtDt.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter 'As at' Date!");
                    return;
                }
            }

            if (rdoDateRange.Checked)
            {
                Search();//bind the price detail grid
            }
            else
            {
                Search_AsAt();
            }
           
            
            if (chkScheme.Checked)
            {
                if (txtItemCode.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter Item Code!");
                    return;
                }
                if (txtItemCode.Text.Trim() != "")
                {
                    //    DataTable EnqTblItmCD = CHNLSVC.Sales.EnquirePriceDetails(GlbUserName, GlbUserComCode, GlbUserDefProf, "ABANS", "A", "", "N/A", "N/A", fdt, tdt, "N/A", "DVD", "DVD", "N/A", 0);

                    MasterItem mstItm = new MasterItem();
                    mstItm = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItemCode.Text.Trim().ToUpper());
                    if (mstItm != null)
                    {
                        lblDescription.Text = mstItm.Mi_longdesc;
                        lblModal.Text = mstItm.Mi_model;
                        Decimal VAT_RATE = CHNLSVC.Sales.GET_Item_vat_Rate(GlbUserComCode, mstItm.Mi_cd, "VAT");
                        lblVatRate.Text = VAT_RATE.ToString() + "%";

                    }
                    else
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Incorrect item code!");
                        return;
                    }
                }
                if (txtAsAtDt.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter As at date!");
                    return;
                }

                try { Convert.ToDateTime(txtAsAtDt.Text); }
                catch (Exception ex)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter an As at date!");
                    return;
                }

                string entered_schemeCD = null;
                if (txtSchemeCD.Text.Trim() != "")
                    entered_schemeCD = txtSchemeCD.Text.Trim();

                string entered_PriceBook = null;
                if (txtPriceBook.Text.Trim() != "")
                    entered_PriceBook = txtPriceBook.Text.Trim();

                string entered_Level = null;
                if (txtLevel.Text.Trim() != "")
                    entered_Level = txtLevel.Text.Trim();

                DataTable pcHerachyTB = new DataTable();
                List<HpSchemeDefinition> Final_schemaList = new List<HpSchemeDefinition>();

              
                pcHerachyTB = CHNLSVC.Sales.Get_hpHierachy(GlbUserDefProf);
              
                if (pcHerachyTB.Rows.Count > 0)
                {
                    MasterItem mstItm = new MasterItem();
                    mstItm = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItemCode.Text.Trim().ToUpper());


                    
                    foreach (DataRow pcH in pcHerachyTB.Rows)
                    {
                        string party_tp = Convert.ToString(pcH["MPI_CD"]);
                        string party_cd = Convert.ToString(pcH["MPI_VAL"]);

                        List<HpSchemeDefinition> schemsList = new List<HpSchemeDefinition>();

                        //schemsList = CHNLSVC.Sales.get_hp_Schemes(Convert.ToDateTime(txtAsAtDt.Text), txtItemCode.Text.Trim(), party_tp, party_cd, mstItm.Mi_brand, mstItm.Mi_cate_1, mstItm.Mi_cate_2);

                        schemsList = CHNLSVC.Sales.get_HP_Schemes(Convert.ToDateTime(txtAsAtDt.Text), txtItemCode.Text.Trim(), party_tp, party_cd, mstItm.Mi_brand, mstItm.Mi_cate_1, mstItm.Mi_cate_2, entered_schemeCD, entered_PriceBook, entered_Level);
                        Final_schemaList.AddRange(schemsList);

                    }
                }
                //foreach (HpSchemeDefinition shc in Final_schemaList)
                //{
                    if (txtPriceBook.Text.Trim() != "")
                    {
                        Final_schemaList.RemoveAll(x => x.Hpc_pb != txtPriceBook.Text.Trim());
                        //if (shc.Hpc_pb != txtPriceBook.Text.Trim())
                        //{
                        //    Final_schemaList.Remove(shc);
                        //}
                    }
                    if(txtLevel.Text.Trim()!="")
                    {
                        Final_schemaList.RemoveAll(x => x.Hpc_pb_lvl != txtLevel.Text.Trim());
                        //if (shc.Hpc_pb_lvl != txtLevel.Text.Trim())
                        //{
                        //    Final_schemaList.Remove(shc);
                        //}
                    }
                    if (txtCircular.Text.Trim() != "")
                    {
                        Final_schemaList.RemoveAll(x => x.Hpc_cir_no != txtCircular.Text.Trim());
                        //if (shc.Hpc_cir_no != txtCircular.Text.Trim())
                        //{
                        //    Final_schemaList.Remove(shc);
                        //}
                    }
                   
                //}

                grvSchems.DataSource = Final_schemaList;
                grvSchems.DataBind();
            }

        }

        protected void Search()
        {
            if (txtPriceBook.Text == "" && txtLevel.Text == "" && txtItemCode.Text == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "'Enter Item Code' or 'Price Book' or 'Level'!");
                return;
            }
            #region search
            if (txtItemCode.Text.Trim() != "")
            {
                //    DataTable EnqTblItmCD = CHNLSVC.Sales.EnquirePriceDetails(GlbUserName, GlbUserComCode, GlbUserDefProf, "ABANS", "A", "", "N/A", "N/A", fdt, tdt, "N/A", "DVD", "DVD", "N/A", 0);

                MasterItem mstItm = new MasterItem();
                mstItm = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItemCode.Text.Trim().ToUpper());
                if (mstItm != null)
                {
                    lblDescription.Text = mstItm.Mi_longdesc;
                    lblModal.Text = mstItm.Mi_model;
                    Decimal VAT_RATE = CHNLSVC.Sales.GET_Item_vat_Rate(GlbUserComCode, mstItm.Mi_cd, "VAT");
                    lblVatRate.Text = VAT_RATE.ToString() + "%";

                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Incorrect item code!");
                    return;
                }
            }
            string entered_PriceBook = null;
            if (txtPriceBook.Text.Trim() != "")
                entered_PriceBook = txtPriceBook.Text.Trim();

            string entered_Level = null;
            if (txtLevel.Text.Trim() != "")
                entered_Level = txtLevel.Text.Trim();

            string entered_Circular = null;
            if (txtCircular.Text.Trim() != "")
                entered_Circular = txtCircular.Text.Trim();

            string entered_ItemCD = txtItemCode.Text.Trim();
            if (txtItemCode.Text.Trim() != "")
                entered_ItemCD = txtItemCode.Text.Trim();

            string entered_Customer = null;
            if (txtCustomer.Text.Trim() != "")
                entered_Customer = txtCustomer.Text.Trim();


            string entered_Type = null;
            if (txtType.Text.Trim() != "")
                entered_Type = txtType.Text.Trim();

            Int32 _Type = -1;
            if (txtType.Text.Trim() != "")
                try {
                    _Type = Convert.ToInt32(txtType.Text.Trim());
                }
                catch(Exception ex)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter valid number for Type!");
                    return;
                }
          

            string entered_cate1 = null;
            if (txtCate1.Text.Trim() != "")
                entered_cate1 = txtCate1.Text.Trim();

            string entered_cate2 = null;
            if (txtCate2.Text.Trim() != "")
                entered_cate2 = txtCate2.Text.Trim();

            string entered_cate3 = null;
            if (txtCate3.Text.Trim() != "")
                entered_cate3 = txtCate3.Text.Trim();

            DateTime fdt = DateTime.MinValue.Date;
            DateTime tdt = DateTime.Now.Date;
            try
            {
                if (txtFromDt.Text.Trim() != "")
                    fdt = Convert.ToDateTime(txtFromDt.Text.Trim()).Date;
                if (txtToDt.Text.Trim() != "")
                    tdt = Convert.ToDateTime(txtToDt.Text.Trim()).Date;
            }
            catch (Exception ex)
            {

                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter valid date!");
                return;
            }
            try
            {
                DataTable EnqTbl = CHNLSVC.Sales.EnquirePriceDetails(GlbUserName, GlbUserComCode, GlbUserDefProf, entered_PriceBook, entered_Level, entered_ItemCD, entered_Customer, entered_Type, fdt, tdt, entered_Circular, entered_cate1, entered_cate2, entered_cate3, _Type);
                grvPriceDetail.DataSource = EnqTbl;
                grvPriceDetail.DataBind();
            }
            catch(Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter another searching parameter!");
                return;
            }
           
            // DataTable EnqTbl = CHNLSVC.Sales.EnquirePriceDetails(GlbUserName, GlbUserComCode, GlbUserDefProf, entered_PriceBook, entered_Level, entered_ItemCD, entered_Customer, entered_Type, fdt, tdt, entered_Circular, "DVD", "DVD", null, 0);
            // DataTable EnqTbl = CHNLSVC.Sales.EnquirePriceDetails(GlbUserName,GlbUserComCode, GlbUserDefProf, "ABANS", "A", "LGDVD270", null, null, fdt, tdt, null,"DVD","DVD",null,0);
            //    DataTable EnqTbl = CHNLSVC.Sales.EnquirePriceDetails(GlbUserName, GlbUserComCode, GlbUserDefProf, "ABANS", "MSR" ,null, null, null, fdt, tdt, null, null, null, null, 0);
           

           

            #endregion
        }
        protected void Search_AsAt()
        {
            if (txtPriceBook.Text == "" && txtLevel.Text == "" && txtItemCode.Text == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "'Enter Item Code' or 'Price Book' or 'Level'!");
                return;
            }
            #region search
            if (txtItemCode.Text.Trim() != "")
            {
                //    DataTable EnqTblItmCD = CHNLSVC.Sales.EnquirePriceDetails(GlbUserName, GlbUserComCode, GlbUserDefProf, "ABANS", "A", "", "N/A", "N/A", fdt, tdt, "N/A", "DVD", "DVD", "N/A", 0);

                MasterItem mstItm = new MasterItem();
                mstItm = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItemCode.Text.Trim().ToUpper());
                if (mstItm != null)
                {
                    lblDescription.Text = mstItm.Mi_longdesc;
                    lblModal.Text = mstItm.Mi_model;
                    Decimal VAT_RATE = CHNLSVC.Sales.GET_Item_vat_Rate(GlbUserComCode, mstItm.Mi_cd, "VAT");
                    lblVatRate.Text = VAT_RATE.ToString() + "%";

                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Incorrect item code!");
                    return;
                }
            }
            string entered_PriceBook = null;
            if (txtPriceBook.Text.Trim() != "")
                entered_PriceBook = txtPriceBook.Text.Trim();

            string entered_Level = null;
            if (txtLevel.Text.Trim() != "")
                entered_Level = txtLevel.Text.Trim();

            string entered_Circular = null;
            if (txtCircular.Text.Trim() != "")
                entered_Circular = txtCircular.Text.Trim();

            string entered_ItemCD = txtItemCode.Text.Trim();
            if (txtItemCode.Text.Trim() != "")
                entered_ItemCD = txtItemCode.Text.Trim();

            string entered_Customer = null;
            if (txtCustomer.Text.Trim() != "")
                entered_Customer = txtCustomer.Text.Trim();


            string entered_Type = null;
            if (txtType.Text.Trim() != "")
                entered_Type = txtType.Text.Trim();

            Int32 _Type = -1;
            if (txtType.Text.Trim() != "")
                _Type = Convert.ToInt32(txtType.Text.Trim());

            string entered_cate1 = null;
            if (txtCate1.Text.Trim() != "")
                entered_cate1 = txtCate1.Text.Trim();

            string entered_cate2 = null;
            if (txtCate2.Text.Trim() != "")
                entered_cate2 = txtCate2.Text.Trim();

            string entered_cate3 = null;
            if (txtCate3.Text.Trim() != "")
                entered_cate3 = txtCate3.Text.Trim();

            //DateTime fdt = DateTime.MinValue.Date;
            //DateTime tdt = DateTime.Now.Date;
            DateTime asatDate = DateTime.Now.Date;
            try
            {
                if (txtAsAtDt.Text.Trim() != "")
                    asatDate = Convert.ToDateTime(txtFromDt.Text.Trim()).Date;
            }
            catch (Exception ex)
            {

                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter valid 'As at' date!");
                return;
            }
            try {
                //EnquirePriceDetails_forAsAtDate
                DataTable EnqTbl = CHNLSVC.Sales.EnquirePriceDetails_forAsAtDate(GlbUserName, GlbUserComCode, GlbUserDefProf, entered_PriceBook, entered_Level, entered_ItemCD, entered_Customer, entered_Type, asatDate, entered_Circular, entered_cate1, entered_cate2, entered_cate3, _Type);

                //DataTable EnqTbl = CHNLSVC.Sales.EnquirePriceDetails(GlbUserName, GlbUserComCode, GlbUserDefProf, entered_PriceBook, entered_Level, entered_ItemCD, entered_Customer, entered_Type, fdt, tdt, entered_Circular, entered_cate1, entered_cate2, entered_cate3, _Type);

                grvPriceDetail.DataSource = EnqTbl;
                grvPriceDetail.DataBind();
            }
            catch(Exception ex){
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter another searching parameter!");
                return;
            }
           

            if (txtItemCode.Text.Trim() != "")
            {
                MasterItem mstItm = new MasterItem();
                mstItm = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItemCode.Text.Trim().ToUpper());
                lblDescription.Text = mstItm.Mi_longdesc;
                lblModal.Text = mstItm.Mi_model;
              
            }

            #endregion
        }
        #region Searchin
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
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
                case CommonUIDefiniton.SearchUserControlType.Scheme:
                    {
                        paramsText.Append(txtSchemeCD.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceType:
                    {
                        //paramsText.Append(txtType.Text.Trim() + seperator + "%" + seperator);
                        paramsText.Append("%" + seperator);
                        break;
                    }
                   // 
                //case CommonUIDefiniton.SearchUserControlType.AvailableSerial:
                //    {
                //        paramsText.Append(GlbUserComCode + seperator + "RWHAE" + seperator + "LGDVD270" + seperator);
                //        break;
                //    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        protected void imgBtnSItemcd_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable dataSource = CHNLSVC.CommonSearch.GetItemSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtItemCode.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        //Modal pop up
        protected void ShowCombineItems(int _pbSeqNo, string _mainItemCD, string _mSerial)
        {
            List<PriceCombinedItemRef> _tempPriceCombinItem = new List<PriceCombinedItemRef>();
            _tempPriceCombinItem = new List<PriceCombinedItemRef>();
            _tempPriceCombinItem = CHNLSVC.Sales.GetPriceCombinedItem(_pbSeqNo, _mainItemCD, string.Empty);
            if (_tempPriceCombinItem != null)
            {
                grvPopUpCombines.DataSource = _tempPriceCombinItem;
                grvPopUpCombines.DataBind();
                Panel_popUp.Visible = true;
                ModalPopupExtItem.Show();
                //ModalPopupExtItem.Show();
                // Panel_PopUp.s
                return;
            }
            else
            {
                // divPopPriceItemCombination.Visible = false;
                // lblMsg.Text = "There is no such combine items pick";
                //  MPEPopup.Show();
                //  return;
            }
        }

        protected void grvPriceDetail_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //string _payType = (string)gvPayment.DataKeys[row_id][0];
            //  decimal _settleAmount = (decimal)gvPayment.DataKeys[row_id][1];

            //ModalPopupExtItem.Show();
        }


        protected void grvPriceDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int row_id = e.RowIndex;
            Int32 seqNo = Convert.ToInt32(grvPriceDetail.DataKeys[row_id]["SARPT_INDI"]);
            Int32 pB_seqNo = Convert.ToInt32(grvPriceDetail.DataKeys[row_id]["SAPD_SEQ_NO"]);
            string itmeCode = Convert.ToString(grvPriceDetail.DataKeys[row_id]["sapd_itm_cd"]);
            if (seqNo != 0)
            {
                ShowCombineItems(pB_seqNo, itmeCode, "");
            }
            // ShowCombineItems(pB_seqNo, itmeCode, "");
            // txtCate1.Text = pB_seqNo.ToString();
        }

        protected void rdoWithHistory_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoWithHistory.Checked)
            {
                div_Asat.Visible = true;
                divFromDate.Visible = false;
                divTodate.Visible = false;
            }
            else
            {
               div_Asat.Visible = false;
               divFromDate.Visible = true;
               divTodate.Visible = true;
            }
        }

        protected void rdoDateRange_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDateRange.Checked)
            {
                divFromDate.Visible = true;
                divTodate.Visible = true;
                div_Asat.Visible = false;
            }
            else
            {
                divFromDate.Visible = false;
                divTodate.Visible = false;
                div_Asat.Visible = true;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Enquiry_Modules/Sales/PriceDetails.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        protected void grvPriceDetail_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {

        }

        protected void imgBtnCustSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCustomerGenaral(MasterCommonSearchUCtrl.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtCustomer.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgBtnPBsearch_Click(object sender, ImageClickEventArgs e)
        {

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPriceBook.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgBtnLevelSearch_Click(object sender, ImageClickEventArgs e)
        {  //PriceLevel
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPriceLevelByBookData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtLevel.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgBtnSchemSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Scheme);
            DataTable dataSource = CHNLSVC.CommonSearch.Get_SchemesCD_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtSchemeCD.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgBtnTypeSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceType);
            DataTable dataSource = CHNLSVC.CommonSearch.Get_PriceTypes_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtType.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void chkScheme_CheckedChanged(object sender, EventArgs e)
        {
            if (chkScheme.Checked)
            {
                divSchemeCD.Visible = true;
                divpadding.Visible = false;
            }
            else
            {
                divSchemeCD.Visible = false;
                divpadding.Visible = true;
            }
        }

        protected void imgBtnCircularSarch_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void grvPriceDetail_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt_tosort= (DataTable)(grvPriceDetail.DataSource);

            
            //DataView dv = dt_tosort.DefaultView;  
            //  //apply the sort on CustomerSurname column  
            //   dv.Sort = "Item Code";  
            //   //save our newly ordered results back into our datatable  
            //   dt_tosort = dv.ToTable();

            //string filterExp = "Status = 'Active'";
            //string sortExp = "Item Code";

            // dt_tosort.Select(filterExp, sortExp, DataViewRowState.CurrentRows);

            //   // dt_tosort.Select("Item Code","ASC");
            //   grvPriceDetail.DataSource = dt_tosort.Select(;
            //   grvPriceDetail.DataBind();

        }

        
    }
}