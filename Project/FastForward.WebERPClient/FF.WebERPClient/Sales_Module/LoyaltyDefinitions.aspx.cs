using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Data;
using System.Text;
using System.Web.Security;


namespace FF.WebERPClient.Sales_Module
{
    public partial class LoyaltyDefinitions : BasePage
    {
        #region properties

        public DataTable Party
        {
            get { return (DataTable)ViewState["Party_Type"]; }
            set { ViewState["Party_Type"] = value; }
        }

        public DataTable Loyalty_PO_Party
        {
            get { return (DataTable)ViewState["Loyalty_PO_Party"]; }
            set { ViewState["Loyalty_PO_Party"] = value; }
        }

        public DataTable Redeem_Party
        {
            get { return (DataTable)ViewState["Redeem_Party"]; }
            set { ViewState["Redeem_Party"] = value; }
        }

        public DataTable Discount_Party
        {
            get { return (DataTable)ViewState["Discount_Party"]; }
            set { ViewState["Discount_Party"] = value; }
        }

        public DataTable Price_book
        {
            get { return (DataTable)ViewState["Price_book"]; }
            set { ViewState["Price_book"] = value; }
        }
        public DataTable Loyalty_Po_Price_book
        {
            get { return (DataTable)ViewState["Loyalty_Po_Price_book"]; }
            set { ViewState["Loyalty_Po_Price_book"] = value; }
        }
        public DataTable Discount_Price_book
        {
            get { return (DataTable)ViewState["Redeem_Price_book"]; }
            set { ViewState["Redeem_Price_book"] = value; }
        }
        public DataTable Redeem_Price_book
        {
            get { return (DataTable)ViewState["Redeem_Price_book"]; }
            set { ViewState["Redeem_Price_book"] = value; }
        }
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {

            //TextBoxLoyaltyPoValidFrom.Enabled = false;
            //TextBoxLoyaltyPovalidTo.Enabled = false;
            //TextBoxFromDate.Enabled = false;
            //TextBoxToDate.Enabled = false;
            //TextBoxDiscountValidFrom.Enabled = false;
            //TextBoxDiscountValidTo.Enabled = false;
            //TextBoxRedeemValidFrom.Enabled = false;
            //TextBoxRedeemValidTo.Enabled = false;


            if (!IsPostBack) {
                //data table create
                CreateTableParty();
                CreateTableLoyaltyPOParty();
                CreateTableRedeemParty();
                LoadCustomerSpecifications(DropDownList1);
                LoadCreditCard(DropDownListCardType);
                LoadSpecifications(DropDownListSpecification);
                //party type load
                //BindPartyTypes(DropDownListPrtyTyp);
                //BindPartyTypes(DropDownListLoyalPoPartyTy);
                //BindPartyTypes(DropDownListRedeemPartyType);
                //BindPartyTypes(DropDownListDiscountPartyType);
                //price book load
                BindPriceBook(DropDownListLoyaltyPoPriceBook);
                BindPriceBook(DropDownListPB);
                BindPriceBook(DropDownListRedeemPB);
                BindPriceBook(DropDownListDiscountPB);
                BindBanks(DropDownListLoyaltyPoBank);
                BindPayModes(DropDownListLoyaltyPoPayMode);
                
            }
            DropDownListLoyaltyPoPayMode.Attributes.Add("onmouseover", "this.title=this.options[this.selectedIndex].title");

            DropDownListLoyaltyPoPriceBook.Attributes.Add("onmouseover", "this.title=this.options[this.selectedIndex].title");
            DropDownListPB.Attributes.Add("onmouseover", "this.title=this.options[this.selectedIndex].title");
            DropDownListRedeemPB.Attributes.Add("onmouseover", "this.title=this.options[this.selectedIndex].title");
            DropDownListDiscountPB.Attributes.Add("onmouseover", "this.title=this.options[this.selectedIndex].title");

            DropDownListLoyaltyPoBank.Attributes.Add("onmouseover", "this.title=this.options[this.selectedIndex].title");
            //load tooltip for dropdown selected item
            //for ie8 dropdown width problem
            LoadToolTip();
        }

        #region datatable create

        private void CreateTableParty()
        {
            Party = new DataTable(); 
            Party.Columns.Add("P_Type", typeof(string));
            Party.Columns.Add("P_Code", typeof(string));
        }

        private void CreateTableLoyaltyPOParty()
        {
            Loyalty_PO_Party = new DataTable();
            Loyalty_PO_Party.Columns.Add("P_Type", typeof(string));
            Loyalty_PO_Party.Columns.Add("P_Code", typeof(string));
        }

        private void CreateTableRedeemParty()
        {
            Redeem_Party = new DataTable();
            Redeem_Party.Columns.Add("P_Type", typeof(string));
            Redeem_Party.Columns.Add("P_Code", typeof(string));
        }

        private void CreateTableDiscountParty()
        {
            Discount_Party = new DataTable();
            Discount_Party.Columns.Add("P_Type", typeof(string));
            Discount_Party.Columns.Add("P_Code", typeof(string));
        }

        private void CreatePricBook()
        {
            Price_book = new DataTable();
            Price_book.Columns.Add("PB", typeof(string));
            Price_book.Columns.Add("PB_Lvl", typeof(string));
        }

        private void CreateLoyaltyPOPricBook()
        {
            Loyalty_Po_Price_book = new DataTable();
            Loyalty_Po_Price_book.Columns.Add("PB", typeof(string));
            Loyalty_Po_Price_book.Columns.Add("PB_Lvl", typeof(string));
        }

        private void CreateDiscountPricBook()
        {
            Discount_Price_book = new DataTable();
            Discount_Price_book.Columns.Add("PB", typeof(string));
            Discount_Price_book.Columns.Add("PB_Lvl", typeof(string));
        }

        private void CreateRedeemPricBook()
        {
            Redeem_Price_book = new DataTable();
            Redeem_Price_book.Columns.Add("PB", typeof(string));
            Redeem_Price_book.Columns.Add("PB_Lvl", typeof(string));
        }

        #endregion

        #region data load

        private void LoadToolTip() {
            foreach (ListItem _listItem in DropDownListLoyaltyPoPayMode.Items)
            {

                _listItem.Attributes.Add("title", _listItem.Text);

            }
            foreach (ListItem _listItem in DropDownListLoyaltyPoPriceBook.Items)
            {

                _listItem.Attributes.Add("title", _listItem.Text);

            }
            foreach (ListItem _listItem in DropDownListPB.Items)
            {

                _listItem.Attributes.Add("title", _listItem.Text);

            }
            foreach (ListItem _listItem in DropDownListRedeemPB.Items)
            {

                _listItem.Attributes.Add("title", _listItem.Text);

            }
            foreach (ListItem _listItem in DropDownListDiscountPB.Items)
            {

                _listItem.Attributes.Add("title", _listItem.Text);

            }
            foreach (ListItem _listItem in DropDownListLoyaltyPoBank.Items)
            {

                _listItem.Attributes.Add("title", _listItem.Text);

            }
        }

        private void BindPartyTypes(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem(" ", "-1"));
            ddl.DataSource = CHNLSVC.Sales.Get_hpHierachy(GlbUserDefProf);
            ddl.DataTextField = "MPI_CD";
            ddl.DataValueField = "MPI_CD";
            ddl.DataBind();
        }

        private void BindBanks(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", "-1"));
            DataTable datasource = CHNLSVC.Financial.GetBanks();
            foreach (DataRow dr in datasource.Rows)
            {
                ddl.Items.Add(new ListItem(dr["mbi_cd"].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr["mbi_cd"].ToString().Length)) + "-" + dr["mbi_desc"].ToString(), dr["mbi_cd"].ToString()));
            }
            
        }

        private void BindPayModes(DropDownList ddl)
        {
            //ddl.Items.Clear();
            //DataTable datasource = CHNLSVC.General.GetSalesTypes("", null, null);
            //foreach (DataRow dr in datasource.Rows)
            //{
            //    ddl.Items.Add(new ListItem(dr["srtp_cd"].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr["srtp_cd"].ToString().Length)) + "-" + dr["srtp_desc"].ToString(), dr["srtp_cd"].ToString()));
            //}


            ddl.Items.Clear();
            List<PaymentTypeRef> _paymentTypeRef = CHNLSVC.Sales.GetAllPaymentType(GlbUserComCode,GlbUserDefProf,null);
            if (_paymentTypeRef != null) {
                foreach (PaymentTypeRef pr in _paymentTypeRef) {
                    ddl.Items.Add(new ListItem(pr.Sapt_cd + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - pr.Sapt_cd.Length)) + "-" + pr.Sapt_desc, pr.Sapt_cd));
                }
            }
        }

        private void BindPriceBook(DropDownList ddl)
        {
            List<PriceBookRef> _priceBook = CHNLSVC.Sales.GetPriceBooklist(GlbUserComCode);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("", "-1"));
            if (_priceBook.Count > 0)
            {
                foreach (PriceBookRef _pb in _priceBook)
                {
                    ddl.Items.Add(new ListItem(_pb.Sapb_pb + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - _pb.Sapb_pb.Length)) + "-" + _pb.Sapb_desc, _pb.Sapb_pb));
                }
            }
            ddl.DataBind();
        }

        private void LoadCustomerSpecifications(DropDownList DropDownList1)
        {
            DropDownList1.DataSource = CHNLSVC.Sales.GetLoyaltyCustomerSpecifications(TextBoxLoyalPoLoyaltyType.Text);
            DropDownList1.DataTextField = "SALCS_SPEC";
            DropDownList1.DataValueField = "SALCS_SPEC";
            DropDownList1.DataBind();
        }

        private void LoadCreditCard(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--select--", ""));
            ddl.DataSource = CHNLSVC.Sales.GetBankCC(DropDownListLoyaltyPoBank.SelectedValue);
            ddl.DataTextField = "mbct_cc_tp";
            ddl.DataValueField = "mbct_cc_tp";
            ddl.DataBind();
        }

        private void LoadList(ListBox listBox, DataTable _result, string _code, string _des)
        {
            listBox.Items.Clear();
            if (_result.Rows.Count > 0)
            {
                foreach (DataRow dr in _result.Rows)
                {
                    listBox.Items.Add(new ListItem(dr[_code].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[_code].ToString().Length)) + "-" + dr[_des].ToString(), dr[_code].ToString()));
                }
                foreach (ListItem li in listBox.Items)
                {
                    li.Selected = true;
                }
            }
        }

        #endregion

        #region save/cancel/clear


        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Sales_Module/LoyaltyDefinitions.aspx");
        }

        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {

            if (tcLoyaltyDefinition.ActiveTabIndex == 0) {

                #region validation

                if (TextBoxFromDate.Text == "") {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select from date");
                    return;
                }
                if (TextBoxToDate.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select to date");
                    return;
                }
                DateTime date;
                if (!DateTime.TryParse(TextBoxFromDate.Text, out date))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter from date correctly");
                    return;
                }
                if (!DateTime.TryParse(TextBoxToDate.Text, out date))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter to date correctly");
                    return;
                }

                if (TextBoxMemChg.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter membership charge");
                    return;
                }
                if (TextBoxRenewalChg.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter renewal charge");
                    return;
                }
                if (TextBoxValidPeriod.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter valid period in days");
                    return;
                }
                if (Convert.ToDateTime(TextBoxFromDate.Text) > Convert.ToDateTime(TextBoxToDate.Text)) {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From date has to be smaller than to date");
                    return;
                }
                if (ListBoxPBList.Items.Count <= 0) {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please add price book level");
                    return;
                }
                if (GridViewPC.Rows.Count <= 0) {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please add pc");
                    return;
                }

                #endregion

                bool isPromotion = false;
                bool isSerial = false;
                bool isItem = false;

                List<string> party = new List<string>();
                for (int i = 0; i < GridViewPC.Rows.Count; i++)
                {
                    CheckBox chkSelect = (CheckBox)GridViewPC.Rows[i].Cells[1].FindControl("chekPc");
                    if (chkSelect.Checked)
                    {
                        party.Add(GridViewPC.Rows[i].Cells[1].Text);
                    }
                }

                //save loyalty types
                LoyaltyType _loyal = new LoyaltyType();
                _loyal.Salt_loty_tp = TextBoxLoyaltyType.Text.ToUpper();
                _loyal.Salt_desc = TextBoxLoyaltyDescription.Text.ToUpper();
                _loyal.Salt_frm_dt = Convert.ToDateTime(TextBoxFromDate.Text);
                _loyal.Salt_to_dt = Convert.ToDateTime(TextBoxToDate.Text);
                _loyal.Salt_is_comp = CheckBoxIsCompl.Checked;
                _loyal.Salt_alw_multi_cdpnt = CheckBoxAllowMul.Checked;
                _loyal.Salt_memb_chg=Convert.ToDecimal(TextBoxMemChg.Text);
                _loyal.Salt_renew_chg = Convert.ToDecimal(TextBoxRenewalChg.Text);
                _loyal.Salt_valid = Convert.ToInt32(TextBoxValidPeriod.Text);
                _loyal.Salt_brand = uc_ItemDetailSearch1.BRAND;
                _loyal.Salt_cat_1 = uc_ItemDetailSearch1.CAT_1;
                _loyal.Salt_cat_2 = uc_ItemDetailSearch1.CAT_2;
                _loyal.Salt_cat_3 = uc_ItemDetailSearch1.CAT_3;
                _loyal.Salt_cre_by = GlbUserName;
                _loyal.Salt_cre_dt = DateTime.Now;
                _loyal.Salt_circ_no = uc_ItemDetailSearch1.CIRCULAR_NO;
                if (uc_ItemDetailSearch1.PROMOTION.Count > 0)
                    isPromotion = true;
                if (uc_ItemDetailSearch1.SERIAL.Count > 0)
                    isSerial = true;
                if (uc_ItemDetailSearch1.ITEM.Count > 0)
                    isItem = true;

                CHNLSVC.Sales.SaveLoyaltyType(_loyal,uc_ItemDetailSearch1.ITEM,uc_ItemDetailSearch1.SERIAL,uc_ItemDetailSearch1.PROMOTION,isItem,isSerial,isPromotion,party,Price_book);
                string Msg = "<script>alert('Record Insert Sucessfully!!');window.location = 'LoyaltyDefinitions.aspx';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                
            }
            else if (tcLoyaltyDefinition.ActiveTabIndex == 1) {

                #region validation

                if (TextBoxLoyaltyPoValidFrom.Text == "") {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter valid from date");
                    return;
                }
                if (TextBoxLoyaltyPovalidTo.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter valid to date");
                    return;
                }
                DateTime date;
                if (!DateTime.TryParse(TextBoxLoyaltyPoValidFrom.Text, out date))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter from date correctly");
                    return;
                }
                if (!DateTime.TryParse(TextBoxLoyaltyPovalidTo.Text, out date))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter to date correctly");
                    return;
                }
                if (Convert.ToDateTime(TextBoxLoyaltyPoValidFrom.Text) > Convert.ToDateTime(TextBoxLoyaltyPovalidTo.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From date has tobe smaller than to date");
                    return;
                }
                if (TextBoxLoyaltyPoValueFrom.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter from value");
                    return;
                }
                if (TextBoxLoyaltyPoValueTo.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter to value");
                    return;
                }
                if (Convert.ToDecimal(TextBoxLoyaltyPoValueFrom.Text) > Convert.ToDecimal(TextBoxLoyaltyPoValueTo.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From value has tobe smaller than to value");
                    return;
                }
                if (TextBoxLoyaltyPoQtyFrom.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter qty from");
                    return;
                }
                if (TextBoxLoyaltyPoQtyTo.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter qty to");
                    return;
                }
                if (Convert.ToInt32(TextBoxLoyaltyPoQtyFrom.Text) > Convert.ToInt32(TextBoxLoyaltyPoQtyTo.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From qty has tobe smaller than to qty");
                    return;
                }
                if (TextBoxLoyaltyPoints.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter point value");
                    return;
                }
                if (TextBoxLoyaltyPoValDiv.Text == "") {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter point divide value");
                    return;
                }
                if (Convert.ToInt32(TextBoxLoyaltyPoValDiv.Text) <= 0) {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Point divide value has to be greater than 0");
                    return;
                }
                if (ListBoxLoyalPoPBList.Items.Count <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please add price book level");
                    return;
                }
                if (GridViewPC1.Rows.Count <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please add pc");
                    return;
                }
                
                #endregion

                bool isPromotion = false;
                bool isSerial = false;
                bool isItem = false;

                List<string> party = new List<string>();
                for (int i = 0; i < GridViewPC1.Rows.Count; i++)
                {
                    CheckBox chkSelect = (CheckBox)GridViewPC1.Rows[i].Cells[1].FindControl("chekPc");
                    if (chkSelect.Checked)
                    {
                        party.Add(GridViewPC1.Rows[i].Cells[1].Text);
                    }
                }

                LoyaltyPointDefinition _loyal = new LoyaltyPointDefinition();
                _loyal.Saldf_loty_tp = TextBoxLoyalPoLoyaltyType.Text;
                _loyal.Saldf_val_frm = Convert.ToDateTime(TextBoxLoyaltyPoValidFrom.Text);
                _loyal.Saldf_val_to = Convert.ToDateTime(TextBoxLoyaltyPovalidTo.Text);
                _loyal.Saldf_value_frm = Convert.ToInt32(TextBoxLoyaltyPoValueFrom.Text);
                _loyal.Saldf_value_to = Convert.ToInt32(TextBoxLoyaltyPoValueTo.Text);
                _loyal.Saldf_qt_frm = Convert.ToInt32(TextBoxLoyaltyPoQtyFrom.Text);
                _loyal.Saldf_qt_to = Convert.ToInt32(TextBoxLoyaltyPoQtyTo.Text);
                _loyal.Saldf_pmod = DropDownListLoyaltyPoPayMode.SelectedValue;
                _loyal.Saldf_is_multi = CheckBoxLoyaltyPointsIsMul.Checked;
                if (DropDownListLoyaltyPoBank.SelectedValue == "-1")
                {
                    _loyal.Saldf_bank = "";
                }
                else
                    _loyal.Saldf_bank = DropDownListLoyaltyPoBank.SelectedValue;
                //card type

                _loyal.Saldf_pt = Convert.ToInt32(TextBoxLoyaltyPoints.Text);
                _loyal.Saldf_brand=uc_ItemDetailSearch2.BRAND;
                _loyal.Saldf_cat_1=uc_ItemDetailSearch2.CAT_1;
                _loyal.Saldf_cat_2=uc_ItemDetailSearch2.CAT_2;
                _loyal.Saldf_cat_3=uc_ItemDetailSearch2.CAT_3;
                _loyal.Saldf_circ_no=uc_ItemDetailSearch2.CIRCULAR_NO;
                _loyal.Saldf_cd_tp = DropDownListCardType.SelectedValue;
                _loyal.Saldf_cre_by=GlbUserName;
                _loyal.Saldf_cre_dt=DateTime.Now;
                if (TextBoxLoyaltyPoValDiv.Text == "")
                {
                    _loyal.Saldf_val_div = 1;
                }
                else
                    _loyal.Saldf_val_div = Convert.ToInt32(_loyal.Saldf_val_div);


                if (uc_ItemDetailSearch2.PROMOTION.Count > 0)
                    isPromotion = true;
                if (uc_ItemDetailSearch2.SERIAL.Count > 0)
                    isSerial = true;
                if (uc_ItemDetailSearch2.ITEM.Count > 0)
                    isItem = true;

                CHNLSVC.Sales.SaveLoyaltyPointDefinition(_loyal, uc_ItemDetailSearch2.ITEM, uc_ItemDetailSearch2.SERIAL, uc_ItemDetailSearch2.PROMOTION, isItem, isSerial, isPromotion, party, Price_book);
                string Msg = "<script>alert('Record Insert Sucessfully!!');window.location = 'LoyaltyDefinitions.aspx';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

            }
            else if (tcLoyaltyDefinition.ActiveTabIndex == 2) {

                #region validation

                if (TextBoxDiscountValidFrom.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter valid from date");
                    return;
                }
                if (TextBoxDiscountValidTo.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter valid to date");
                    return;
                }
                DateTime date;
                if (!DateTime.TryParse(TextBoxDiscountValidFrom.Text, out date))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter from date correctly");
                    return;
                }
                if (!DateTime.TryParse(TextBoxDiscountValidTo.Text, out date))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter to date correctly");
                    return;
                }
                if (Convert.ToDateTime(TextBoxDiscountValidFrom.Text) > Convert.ToDateTime(TextBoxDiscountValidTo.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From date has tobe smaller than to date");
                    return;
                }
                if (TextBoxDiscountPointFrom.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter point from value");
                    return;
                }
                if (TextBoxDiscountPointTo.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter point from value");
                    return;
                }
                if (Convert.ToInt32(TextBoxDiscountPointFrom.Text) > Convert.ToInt32(TextBoxDiscountPointTo.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From point has tobe smaller than to point");
                    return;
                }
                if (ListBoxDiscountPBList.Items.Count <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please add price book level");
                    return;
                }
                if (GridViewPC2.Rows.Count <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please add pc");
                    return;
                }
                #endregion
                bool isPromotion = false;
                bool isSerial = false;
                bool isItem = false;


                List<string> party = new List<string>();
                for (int i = 0; i < GridViewPC2.Rows.Count; i++)
                {
                    CheckBox chkSelect = (CheckBox)GridViewPC2.Rows[i].Cells[1].FindControl("chekPc");
                    if (chkSelect.Checked)
                    {
                        party.Add(GridViewPC2.Rows[i].Cells[1].Text);
                    }
                }

                LoyaltyPointDiscount _loyal = new LoyaltyPointDiscount();
                _loyal.Saldi_loty_tp = TextBoxDiscountLoyalType.Text;
                _loyal.Saldi_frm = Convert.ToDateTime(TextBoxDiscountValidFrom.Text);
                _loyal.Saldi_to = Convert.ToDateTime(TextBoxDiscountValidTo.Text);
                _loyal.Saldi_pt_frm = Convert.ToInt32(TextBoxDiscountPointFrom.Text);
                _loyal.Saldi_pt_to = Convert.ToInt32(TextBoxDiscountPointTo.Text);
                _loyal.Saldi_dis_rt = Convert.ToDecimal(TextBoxDiscountRate.Text);
                _loyal.Saldi_brand = uc_ItemDetailSearch3.BRAND;
                _loyal.Saldi_cat_1 = uc_ItemDetailSearch3.CAT_1;
                _loyal.Saldi_cat_2 = uc_ItemDetailSearch3.CAT_2;
                _loyal.Saldi_cat_3 = uc_ItemDetailSearch3.CAT_3;
                _loyal.Saldi_circ_no = uc_ItemDetailSearch3.CIRCULAR_NO;

                if (uc_ItemDetailSearch3.PROMOTION.Count > 0)
                    isPromotion = true;
                if (uc_ItemDetailSearch3.SERIAL.Count > 0)
                    isSerial = true;
                if (uc_ItemDetailSearch3.ITEM.Count > 0)
                    isItem = true;

                CHNLSVC.Sales.SaveLoyaltyDiscountDefinition(_loyal, uc_ItemDetailSearch3.ITEM, uc_ItemDetailSearch3.SERIAL, uc_ItemDetailSearch3.PROMOTION, isItem, isSerial, isPromotion, party, Price_book);
                string Msg = "<script>alert('Record Insert Sucessfully!!');window.location = 'LoyaltyDefinitions.aspx';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

            }
            else if (tcLoyaltyDefinition.ActiveTabIndex == 3) {

                #region validation

                if (TextBoxRedeemValidFrom.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter redeem valid from date");
                    return;
                }
                if (TextBoxRedeemValidTo.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter redeem valid to date");
                    return;
                }
                DateTime date;
                if (!DateTime.TryParse(TextBoxRedeemValidFrom.Text, out date))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter from date correctly");
                    return;
                }
                if (!DateTime.TryParse(TextBoxRedeemValidTo.Text, out date))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter to date correctly");
                    return;
                }
                if (Convert.ToDateTime(TextBoxRedeemValidFrom.Text) > Convert.ToDateTime(TextBoxRedeemValidTo.Text))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From date has tobe smaller than to date");
                    return;
                }
                if (TextBoxRedeemPoint.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter redeem point");
                    return;
                }
                if (TextBoxRedeemPointValue.Text == "") {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter redeem point value");
                    return;
                }
                if (ListBoxRedeemPBList.Items.Count <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please add price book level");
                    return;
                }
                if (GridViewPC3.Rows.Count <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please add pc");
                    return;
                }

                #endregion

                bool isPromotion = false;
                bool isSerial = false;
                bool isItem = false;

                List<string> party = new List<string>();
                for (int i = 0; i < GridViewPC3.Rows.Count; i++)
                {
                    CheckBox chkSelect = (CheckBox)GridViewPC3.Rows[i].Cells[1].FindControl("chekPc");
                    if (chkSelect.Checked)
                    {
                        party.Add(GridViewPC3.Rows[i].Cells[1].Text);
                    }
                }

                LoyaltyPointRedeemDefinition _loyal = new LoyaltyPointRedeemDefinition();
                _loyal.Salre_loty_tp = TextBoxRedeemLoyaltyType.Text;
                _loyal.Salre_frm_dt = Convert.ToDateTime(TextBoxRedeemValidFrom.Text);
                _loyal.Salre_to_dt = Convert.ToDateTime(TextBoxRedeemValidTo.Text);
                _loyal.Salre_red_pt = Convert.ToInt32(TextBoxRedeemPoint.Text);
                _loyal.Salre_pt_value = Convert.ToInt32(TextBoxRedeemPointValue.Text);
                _loyal.Salre_brand = uc_ItemDetailSearch4.BRAND;
                _loyal.Salre_cat_1 = uc_ItemDetailSearch4.CAT_1;
                _loyal.Salre_cat_2 = uc_ItemDetailSearch4.CAT_2;
                _loyal.Salre_cat_3 = uc_ItemDetailSearch4.CAT_3;
                _loyal.Salre_circ_no = uc_ItemDetailSearch4.CIRCULAR_NO;


                if (uc_ItemDetailSearch4.PROMOTION.Count > 0)
                    isPromotion = true;
                if (uc_ItemDetailSearch4.SERIAL.Count > 0)
                    isSerial = true;
                if (uc_ItemDetailSearch4.ITEM.Count > 0)
                    isItem = true;

                CHNLSVC.Sales.SaveLoyaltyRedeemDefinition(_loyal, uc_ItemDetailSearch4.ITEM, uc_ItemDetailSearch4.SERIAL, uc_ItemDetailSearch4.PROMOTION, isItem, isSerial, isPromotion, party, Price_book);
                string Msg = "<script>alert('Record Insert Sucessfully!!');window.location = 'LoyaltyDefinitions.aspx';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            
            }
            else if (tcLoyaltyDefinition.ActiveTabIndex == 4) {
                #region validation

                if (TextBoxCSLoyaltyType.Text=="") {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter Loyalty Type");
                    return;
                }
                if (TextBoxCSPointsFrom.Text == "") {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter point from value");
                    return;
                }
                if (TextBoxCSPointsTo.Text == "") {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter point to value");
                    return;
                }
                if (Convert.ToInt32(TextBoxCSPointsFrom.Text) > Convert.ToInt32(TextBoxCSPointsTo.Text)) {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From value has to be smaller than to value");
                    return;
                }
                #endregion


                LoyaltyCustomerSpecification _loyal = new LoyaltyCustomerSpecification();
                _loyal.Salcs_loty_tp = TextBoxCSLoyaltyType.Text;
                _loyal.Salcs_po_from = Convert.ToInt32(TextBoxCSPointsFrom.Text);
                _loyal.Salcs_po_to = Convert.ToInt32(TextBoxCSPointsTo.Text);
                _loyal.Salcs_spec = DropDownListSpecification.SelectedValue;
                _loyal.Salcs_cre_by = GlbUserName;
                _loyal.Salcs_cre_dt = DateTime.Now;

                CHNLSVC.Sales.SaveLoyaltyCustomerSpecification(_loyal);
                string Msg = "<script>alert('Record Insert Sucessfully!!');window.location = 'LoyaltyDefinitions.aspx';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            
            }

        }

        #endregion

        protected void CheckBoxPartyAll_CheckedChanged(object sender, EventArgs e)
        {
            //if (CheckBoxPartyAll.Checked)
            //{
            //    //party checkbox
            //    CreateTableParty();
            //    DataTable dt = new DataTable();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
            //    dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Party.NewRow();
            //        drT[0] = "LOC";
            //        drT[1] = dr[0].ToString();
            //        Party.Rows.Add(drT);
            //        ListBoxPartyCodes.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    dt.Rows.Clear();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
            //    dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Party.NewRow();
            //        drT[0] = "ZONE";
            //        drT[1] = dr[0].ToString();
            //        Party.Rows.Add(drT);
            //        ListBoxPartyCodes.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    dt.Rows.Clear();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
            //    dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Party.NewRow();
            //        drT[0] = "REGION";
            //        drT[1] = dr[0].ToString();
            //        Party.Rows.Add(drT);
            //        ListBoxPartyCodes.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    dt.Rows.Clear();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
            //    dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Party.NewRow();
            //        drT[0] = "SCHNL";
            //        drT[1] = dr[0].ToString();
            //        Party.Rows.Add(drT);
            //        ListBoxPartyCodes.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    dt.Rows.Clear();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
            //    dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Party.NewRow();
            //        drT[0] = "CHNL";
            //        drT[1] = dr[0].ToString();
            //        Party.Rows.Add(drT);
            //        ListBoxPartyCodes.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    dt.Rows.Clear();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OPE);
            //    dt = CHNLSVC.CommonSearch.GetOPE(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Party.NewRow();
            //        drT[0] = "GPC";
            //        drT[1] = dr[0].ToString();
            //        Party.Rows.Add(drT);
            //        ListBoxPartyCodes.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    dt.Rows.Clear();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
            //    dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Party.NewRow();
            //        drT[0] = "COM";
            //        drT[1] = dr[0].ToString();
            //        Party.Rows.Add(drT);
            //        ListBoxPartyCodes.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }

            //    foreach (ListItem li in ListBoxPartyCodes.Items)
            //    {
            //        li.Selected = true;
            //    }
            //    DropDownListPrtyTyp.SelectedIndex = -1;
            //}
            //else
            //    ListBoxPartyCodes.Items.Clear();
        }

        private string AddHtmlSpaces(int length)
        {
            string space = "";
            for (int i = 0; i < length; i++)
            {
                space = space + " &nbsp;";
            }
            return space;
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append(GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OPE:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loyalty_Type:
                    {
                        paramsText.Append(GlbUserDefProf + seperator + DateTime.Now.ToString("MM-dd-yyyy") + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        protected void DropDownListPrtyTyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CreateTableParty();
            //DataTable dt = AddPartyCodes(DropDownListPrtyTyp);
            //LoadList(ListBoxPartyCodes, dt, "Code", "Description");
            //foreach (DataRow dr in dt.Rows)
            //{
            //    DataRow drT = Party.NewRow();
            //    drT[0] = DropDownListPrtyTyp.SelectedValue;
            //    drT[1] = dr[0].ToString();
            //    Party.Rows.Add(drT);
            //}
        }

        private DataTable AddPartyCodes(DropDownList ddl)
        {
            //CreateTableParty();
            DataTable dt = new DataTable();
            if (ddl.SelectedValue == "COM")
            {
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            }
            else if (ddl.SelectedValue == "CHNL")
            {
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            }
            else if (ddl.SelectedValue == "SCHNL")
            {
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            }
            else if (ddl.SelectedValue == "GPC")
            {
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OPE);
                dt = CHNLSVC.CommonSearch.GetOPE(MasterCommonSearchUCtrl.SearchParams, null, null);
            }
            else if (ddl.SelectedValue == "AREA")
            {
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            }
            else if (ddl.SelectedValue == "REGION")
            {
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            }
            else if (ddl.SelectedValue == "ZONE")
            {
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            }
            else if (ddl.SelectedValue == "PC")
            {
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            }
            return dt;
        }

        protected void LinkButtonClearParty_Click(object sender, EventArgs e)
        {
            //ListBoxPartyCodes.Items.Clear();
            //CheckBoxPartyAll.Checked = false;
        }

        protected void DropDownListLoyaltyPoPriceBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreatePricBook();
            AddPBLvlToList(DropDownListLoyaltyPoPriceBook, ListBoxLoyalPoPBList);
        }

        private void AddPBLvlToList(DropDownList ddl,ListBox listbox)
        {
            //CreatePricBook();
            List<PriceBookLevelRef> _PbLevel = CHNLSVC.Sales.GetPriceLevelList(GlbUserComCode, ddl.SelectedValue, null);
            listbox.Items.Clear();
            _PbLevel.RemoveAll(x => x.Sapl_act == false);
            _PbLevel = _PbLevel.GroupBy(x => x.Sapl_pb_lvl_cd).Select(g => g.First()).ToList();
            if (_PbLevel.Count > 0)
            {
                foreach (PriceBookLevelRef pbLv in _PbLevel)
                {
                    DataRow dr = Price_book.NewRow();
                    dr[0] = listbox.SelectedValue;
                    dr[1] = pbLv.Sapl_pb_lvl_cd;
                    Price_book.Rows.Add(dr);
                    listbox.Items.Add(new ListItem(pbLv.Sapl_pb_lvl_cd + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - pbLv.Sapl_pb_lvl_cd.Length)) + "-" + pbLv.Sapl_pb_lvl_desc, pbLv.Sapl_pb_lvl_cd));
                }
                foreach (ListItem li in listbox.Items)
                {
                    li.Selected = true;
                }
            }
        }

        protected void LinkButtonLoyalPoPBClear_Click(object sender, EventArgs e)
        {
            ListBoxLoyalPoPBList.Items.Clear();
        }

        protected void CheckBoxLoyalPoPBAll_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxLoyalPoPBAll.Checked)
            {
                CreatePricBook();
                AddAllPBLvlToList(DropDownListLoyaltyPoPriceBook, ListBoxLoyalPoPBList);
            }
            else
                ListBoxLoyalPoPBList.Items.Clear();
        }

        private void AddAllPBLvlToList(DropDownList ddl,ListBox lb)
        {
            //CreatePricBook();
            lb.Items.Clear();
            List<PriceBookRef> _priceBook = CHNLSVC.Sales.GetPriceBooklist(GlbUserComCode);
            if (_priceBook.Count > 0)
            {
                foreach (PriceBookRef _pb in _priceBook)
                {
                    List<PriceBookLevelRef> _PbLevel = CHNLSVC.Sales.GetPriceLevelList(GlbUserComCode, _pb.Sapb_pb, null);
                    _PbLevel.RemoveAll(x => x.Sapl_act == false);
                    _PbLevel = _PbLevel.GroupBy(x =>new{ x.Sapl_pb_lvl_cd,x.Sapl_pb_lvl_desc}).Select(g => g.First()).ToList();
                    if (_PbLevel.Count > 0)
                    {
                        foreach (PriceBookLevelRef pbLv in _PbLevel)
                        {
                            DataRow dr = Price_book.NewRow();
                            dr[0] = _pb.Sapb_pb;
                            dr[1] = pbLv.Sapl_pb_lvl_cd;
                            Price_book.Rows.Add(dr);
                            lb.Items.Add(new ListItem(_pb.Sapb_pb+HttpUtility.HtmlDecode(AddHtmlSpaces(12 - _pb.Sapb_pb.Length)) +pbLv.Sapl_pb_lvl_cd + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - pbLv.Sapl_pb_lvl_cd.Length)) + "-" + pbLv.Sapl_pb_lvl_desc, pbLv.Sapl_pb_lvl_cd));
                        }
                    }
                }
                foreach (ListItem li in lb.Items)
                {
                    li.Selected = true;
                }
                ddl.SelectedIndex = -1;
            }
        }

        protected void DropDownListLoyalPoPartyTy_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CreateTableLoyaltyPOParty();
            //DataTable dt = AddPartyCodes(DropDownListLoyalPoPartyTy);
            //LoadList(ListBoxLoyalPoPartyList, dt, "Code", "Description");
            //foreach (DataRow dr in dt.Rows)
            //{
            //    DataRow drT = Loyalty_PO_Party.NewRow();
            //    drT[0] = DropDownListLoyalPoPartyTy.SelectedValue;
            //    drT[1] = dr[0].ToString();
            //    Loyalty_PO_Party.Rows.Add(drT);
            //}
        }

        protected void CheckBoxLoyaltyPoPartAll_CheckedChanged(object sender, EventArgs e)
        {
            //if (CheckBoxLoyaltyPoPartAll.Checked)
            //{
            //    //party checkbox
            //    CreateTableParty();
            //    DataTable dt = new DataTable();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
            //    dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Loyalty_PO_Party.NewRow();
            //        drT[0] = "LOC";
            //        drT[1] = dr[0].ToString();
            //        Loyalty_PO_Party.Rows.Add(drT);
            //        ListBoxLoyalPoPartyList.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    dt.Rows.Clear();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
            //    dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Loyalty_PO_Party.NewRow();
            //        drT[0] = "ZONE";
            //        drT[1] = dr[0].ToString();
            //        Loyalty_PO_Party.Rows.Add(drT);
            //        ListBoxLoyalPoPartyList.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    dt.Rows.Clear();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
            //    dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Loyalty_PO_Party.NewRow();
            //        drT[0] = "REGION";
            //        drT[1] = dr[0].ToString();
            //        Loyalty_PO_Party.Rows.Add(drT);
            //        ListBoxLoyalPoPartyList.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    dt.Rows.Clear();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
            //    dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Loyalty_PO_Party.NewRow();
            //        drT[0] = "SCHNL";
            //        drT[1] = dr[0].ToString();
            //        Loyalty_PO_Party.Rows.Add(drT);
            //        ListBoxLoyalPoPartyList.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    dt.Rows.Clear();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
            //    dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Loyalty_PO_Party.NewRow();
            //        drT[0] = "CHNL";
            //        drT[1] = dr[0].ToString();
            //        Loyalty_PO_Party.Rows.Add(drT);
            //        ListBoxLoyalPoPartyList.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    dt.Rows.Clear();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OPE);
            //    dt = CHNLSVC.CommonSearch.GetOPE(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Loyalty_PO_Party.NewRow();
            //        drT[0] = "GPC";
            //        drT[1] = dr[0].ToString();
            //        Loyalty_PO_Party.Rows.Add(drT);
            //        ListBoxLoyalPoPartyList.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    dt.Rows.Clear();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
            //    dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Loyalty_PO_Party.NewRow();
            //        drT[0] = "COM";
            //        drT[1] = dr[0].ToString();
            //        Loyalty_PO_Party.Rows.Add(drT);
            //        ListBoxLoyalPoPartyList.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    foreach (ListItem li in ListBoxLoyalPoPartyList.Items)
            //    {
            //        li.Selected = true;
            //    }
            //    DropDownListLoyalPoPartyTy.SelectedIndex = -1;
            //}
            //else
            //    ListBoxLoyalPoPartyList.Items.Clear();
        }

        protected void LinkButtonLoyaltyPoClear_Click(object sender, EventArgs e)
        {
           // ListBoxLoyalPoPartyList.Items.Clear();
        }

        protected void LinkButtonRedeemPartyClear_Click(object sender, EventArgs e)
        {
            //ListBoxRedeemPartyList.Items.Clear();
        }

        protected void DropDownListRedeemPartyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CreateTableRedeemParty();
            //DataTable dt = AddPartyCodes(DropDownListRedeemPartyType);
            //LoadList(ListBoxRedeemPartyList, dt, "Code", "Description");
            //foreach (DataRow dr in dt.Rows)
            //{
            //    DataRow drT = Redeem_Party.NewRow();
            //    drT[0] = DropDownListRedeemPartyType.SelectedValue;
            //    drT[1] = dr[0].ToString();
            //    Redeem_Party.Rows.Add(drT);
            //}
        }

        protected void CheckBoxRedeemPartyAll_CheckedChanged(object sender, EventArgs e)
        {
            //if (CheckBoxRedeemPartyAll.Checked)
            //{
            //    //party checkbox
            //    CreateTableRedeemParty();
            //    DataTable dt = new DataTable();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
            //    dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Redeem_Party.NewRow();
            //        drT[0] = "LOC";
            //        drT[1] = dr[0].ToString();
            //        Redeem_Party.Rows.Add(drT);
            //        ListBoxRedeemPartyList.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    dt.Rows.Clear();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
            //    dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Redeem_Party.NewRow();
            //        drT[0] = "ZONE";
            //        drT[1] = dr[0].ToString();
            //        Redeem_Party.Rows.Add(drT);
            //        ListBoxRedeemPartyList.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    dt.Rows.Clear();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
            //    dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Redeem_Party.NewRow();
            //        drT[0] = "REGION";
            //        drT[1] = dr[0].ToString();
            //        Redeem_Party.Rows.Add(drT);
            //        ListBoxRedeemPartyList.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    dt.Rows.Clear();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
            //    dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Redeem_Party.NewRow();
            //        drT[0] = "SCHNL";
            //        drT[1] = dr[0].ToString();
            //        Redeem_Party.Rows.Add(drT);
            //        ListBoxRedeemPartyList.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    dt.Rows.Clear();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
            //    dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Redeem_Party.NewRow();
            //        drT[0] = "CHNL";
            //        drT[1] = dr[0].ToString();
            //        Redeem_Party.Rows.Add(drT);
            //        ListBoxRedeemPartyList.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    dt.Rows.Clear();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OPE);
            //    dt = CHNLSVC.CommonSearch.GetOPE(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Redeem_Party.NewRow();
            //        drT[0] = "GPC";
            //        drT[1] = dr[0].ToString();
            //        Redeem_Party.Rows.Add(drT);
            //        ListBoxRedeemPartyList.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    dt.Rows.Clear();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
            //    dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Redeem_Party.NewRow();
            //        drT[0] = "COM";
            //        drT[1] = dr[0].ToString();
            //        Redeem_Party.Rows.Add(drT);
            //        ListBoxRedeemPartyList.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    foreach (ListItem li in ListBoxRedeemPartyList.Items)
            //    {
            //        li.Selected = true;
            //    }
            //    DropDownListRedeemPartyType.SelectedIndex = -1;
            //}
            //else
            //    ListBoxRedeemPartyList.Items.Clear();
        }

        protected void CheckBoxDiscountPartyAll_CheckedChanged(object sender, EventArgs e)
        {
            //if (CheckBoxDiscountPartyAll.Checked)
            //{
            //    //party checkbox
            //    CreateTableDiscountParty();
            //    DataTable dt = new DataTable();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
            //    dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Discount_Party.NewRow();
            //        drT[0] = "LOC";
            //        drT[1] = dr[0].ToString();
            //        Discount_Party.Rows.Add(drT);
            //        ListBoxDiscountPartyList.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    dt.Rows.Clear();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
            //    dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Discount_Party.NewRow();
            //        drT[0] = "ZONE";
            //        drT[1] = dr[0].ToString();
            //        Discount_Party.Rows.Add(drT);
            //        ListBoxDiscountPartyList.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    dt.Rows.Clear();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
            //    dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Discount_Party.NewRow();
            //        drT[0] = "REGION";
            //        drT[1] = dr[0].ToString();
            //        Discount_Party.Rows.Add(drT);
            //        ListBoxDiscountPartyList.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    dt.Rows.Clear();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
            //    dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Discount_Party.NewRow();
            //        drT[0] = "SCHNL";
            //        drT[1] = dr[0].ToString();
            //        Discount_Party.Rows.Add(drT);
            //        ListBoxDiscountPartyList.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    dt.Rows.Clear();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
            //    dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Discount_Party.NewRow();
            //        drT[0] = "CHNL";
            //        drT[1] = dr[0].ToString();
            //        Discount_Party.Rows.Add(drT);
            //        ListBoxDiscountPartyList.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    dt.Rows.Clear();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OPE);
            //    dt = CHNLSVC.CommonSearch.GetOPE(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Discount_Party.NewRow();
            //        drT[0] = "GPC";
            //        drT[1] = dr[0].ToString();
            //        Discount_Party.Rows.Add(drT);
            //        ListBoxDiscountPartyList.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    dt.Rows.Clear();
            //    MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
            //    dt = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        DataRow drT = Discount_Party.NewRow();
            //        drT[0] = "COM";
            //        drT[1] = dr[0].ToString();
            //        Discount_Party.Rows.Add(drT);
            //        ListBoxDiscountPartyList.Items.Add(new ListItem(dr[0].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr[0].ToString().Length)) + "-" + dr[1].ToString(), dr[0].ToString()));
            //    }
            //    foreach (ListItem li in ListBoxDiscountPartyList.Items)
            //    {
            //        li.Selected = true;
            //    }
            //    DropDownListDiscountPartyType.SelectedIndex = -1;
            //}
            //else
            //    ListBoxDiscountPartyList.Items.Clear();
        }

        protected void DropDownListDiscountPartyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CreateTableDiscountParty();
            //DataTable dt = AddPartyCodes(DropDownListDiscountPartyType);
            //LoadList(ListBoxDiscountPartyList, dt, "Code", "Description");
            //foreach (DataRow dr in dt.Rows)
            //{
            //    DataRow drT = Discount_Party.NewRow();
            //    drT[0] = DropDownListDiscountPartyType.SelectedValue;
            //    drT[1] = dr[0].ToString();
            //    Discount_Party.Rows.Add(drT);
            //}
        }

        protected void LinkButtonDiscountPartyClear_Click(object sender, EventArgs e)
        {
            //ListBoxDiscountPartyList.Items.Clear();
        }

        protected void GridViewPC_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void ButtonAll_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in GridViewPC.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                chkSelect.Checked = true;
            }
        }

        protected void ButtonClearPc_Click(object sender, EventArgs e)
        {
            GridViewPC.DataSource = null;
            GridViewPC.DataBind();
        }

        protected void ButtonNone_Click(object sender, EventArgs e)
        {

            foreach (GridViewRow gvr in GridViewPC.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                chkSelect.Checked = false;
            }
           
        }

        protected void ImageButtonAddPC_Click(object sender, ImageClickEventArgs e)
        {
            string com = uc_ProfitCenterSearchLoyaltyType.Company.ToUpper();
            string chanel = uc_ProfitCenterSearchLoyaltyType.Channel.ToUpper();
            string subChanel = uc_ProfitCenterSearchLoyaltyType.SubChannel.ToUpper();
            string area = uc_ProfitCenterSearchLoyaltyType.Area.ToUpper();
            string region = uc_ProfitCenterSearchLoyaltyType.Region.ToUpper();
            string zone = uc_ProfitCenterSearchLoyaltyType.Zone.ToUpper();
            string pc = uc_ProfitCenterSearchLoyaltyType.ProfitCenter.ToUpper();

            DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com, chanel, subChanel, area, region, zone, pc);
            GridViewPC.DataSource = dt;
            GridViewPC.DataBind();
        }

        protected void DropDownListPB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreatePricBook();
            AddPBLvlToList(DropDownListPB, ListBoxPBList);
        }

        protected void LinkButtonPBClear_Click(object sender, EventArgs e)
        {
            ListBoxPBList.Items.Clear();
        }

        protected void CheckBoxPBAll_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxPBAll.Checked)
            {
                CreatePricBook();
                AddAllPBLvlToList(DropDownListPB, ListBoxPBList);
            }
            else
                ListBoxPBList.Items.Clear();
        }

        protected void ButtonAdd1_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in GridViewPC1.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                chkSelect.Checked = true;
            }
        }

        protected void ButtonNone1_Click(object sender, EventArgs e)
        {
            GridViewPC1.DataSource = null;
            GridViewPC1.DataBind();
        }

        protected void ButtonPcClear1_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in GridViewPC1.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                chkSelect.Checked = false;
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string com = uc_ProfitCenterSearch1.Company.ToUpper();
            string chanel = uc_ProfitCenterSearch1.Channel.ToUpper();
            string subChanel = uc_ProfitCenterSearch1.SubChannel.ToUpper();
            string area = uc_ProfitCenterSearch1.Area.ToUpper();
            string region = uc_ProfitCenterSearch1.Region.ToUpper();
            string zone = uc_ProfitCenterSearch1.Zone.ToUpper();
            string pc = uc_ProfitCenterSearch1.ProfitCenter.ToUpper();

            DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com, chanel, subChanel, area, region, zone, pc);
            GridViewPC1.DataSource = dt;
            GridViewPC1.DataBind();
        }

        protected void LinkButtonRedeemPBClear_Click(object sender, EventArgs e)
        {
            ListBoxRedeemPBList.Items.Clear();
        }

        protected void CheckBoxRedeemPBAll_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxRedeemPBAll.Checked)
            {
                CreatePricBook();
                AddAllPBLvlToList(DropDownListRedeemPB, ListBoxRedeemPBList);
            }
            else
                ListBoxRedeemPBList.Items.Clear();
        }

        protected void DropDownListRedeemPB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreatePricBook();
            AddPBLvlToList(DropDownListRedeemPB, ListBoxRedeemPBList);
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            string com = uc_ProfitCenterSearch3.Company.ToUpper();
            string chanel = uc_ProfitCenterSearch3.Channel.ToUpper();
            string subChanel = uc_ProfitCenterSearch3.SubChannel.ToUpper();
            string area = uc_ProfitCenterSearch3.Area.ToUpper();
            string region = uc_ProfitCenterSearch3.Region.ToUpper();
            string zone = uc_ProfitCenterSearch3.Zone.ToUpper();
            string pc = uc_ProfitCenterSearch3.ProfitCenter.ToUpper();

            DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com, chanel, subChanel, area, region, zone, pc);
            GridViewPC3.DataSource = dt;
            GridViewPC3.DataBind();
        }

        protected void ButtonAdd3_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in GridViewPC3.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                chkSelect.Checked = true;
            }
        }

        protected void ButtonNone3_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in GridViewPC3.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                chkSelect.Checked = false;
            }
        }

        protected void ButtonPcClear3_Click(object sender, EventArgs e)
        {

            GridViewPC3.DataSource = null;
            GridViewPC3.DataBind();
        }

        protected void ButtonAdd2_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in GridViewPC2.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                chkSelect.Checked = true;
            }
        }

        protected void ButtonNone2_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in GridViewPC2.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                chkSelect.Checked = false;
            }
        }

        protected void ButtonPcClear2_Click(object sender, EventArgs e)
        {

            GridViewPC2.DataSource = null;
            GridViewPC2.DataBind();
        }

        protected void DropDownListDiscountPB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CreatePricBook();
            AddPBLvlToList(DropDownListDiscountPB, ListBoxDiscountPBList);
        }

        protected void LinkButtonDiscoutPBClear_Click(object sender, EventArgs e)
        {
            ListBoxDiscountPBList.Items.Clear();
        }

        protected void CheckBoxDiscoutPB_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxDiscoutPB.Checked)
            {
                CreatePricBook();
                AddAllPBLvlToList(DropDownListDiscountPB, ListBoxDiscountPBList);
            }
            else
                ListBoxDiscountPBList.Items.Clear();
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            string com = uc_ProfitCenterSearch2.Company.ToUpper();
            string chanel = uc_ProfitCenterSearch2.Channel.ToUpper();
            string subChanel = uc_ProfitCenterSearch2.SubChannel.ToUpper();
            string area = uc_ProfitCenterSearch2.Area.ToUpper();
            string region = uc_ProfitCenterSearch2.Region.ToUpper();
            string zone = uc_ProfitCenterSearch2.Zone.ToUpper();
            string pc = uc_ProfitCenterSearch2.ProfitCenter.ToUpper();

            DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com, chanel, subChanel, area, region, zone, pc);
            GridViewPC2.DataSource = dt;
            GridViewPC2.DataBind();
        }

        protected void ImageButtonLoyalPoLoyalty_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loyalty_Type);
            DataTable dataSource = CHNLSVC.CommonSearch.GetLoyaltyTypes(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxLoyalPoLoyaltyType.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loyalty_Type);
            DataTable dataSource = CHNLSVC.CommonSearch.GetLoyaltyTypes(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxDiscountLoyalType.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loyalty_Type);
            DataTable dataSource = CHNLSVC.CommonSearch.GetLoyaltyTypes(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxRedeemLoyaltyType.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loyalty_Type);
            DataTable dataSource = CHNLSVC.CommonSearch.GetLoyaltyTypes(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxCSLoyaltyType.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void TextBoxLoyalPoLoyaltyType_TextChanged(object sender, EventArgs e)
        {
            LoadCustomerSpecifications(DropDownList1);
        }

        protected void DropDownListLoyaltyPoBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCreditCard(DropDownListCardType);
        }

        protected void ButtonAddSpec_Click(object sender, EventArgs e)
        {
            if (TextBoxSpec.Text != "")
            {int result=CHNLSVC.Sales.SaveLoyaltySpecifications(TextBoxSpec.Text, GlbUserName, DateTime.Now);
            if (result < 0)
            {
                string Msg = "<script>alert('Error Occure while Processing!!');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                ModalPopupExtender1.Show();
            }
            }
            else
            {
                string Msg = "<script>alert('Please enter specification!!');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                ModalPopupExtender1.Show();
            }
            LoadSpecifications(DropDownListSpecification);
        }

        private void LoadSpecifications(DropDownList ddl)
        {
            ddl.DataSource = CHNLSVC.Sales.GetLoyaltySpecifications();
            ddl.DataTextField = "sarls_spec";
            ddl.DataValueField = "sarls_spec";
            ddl.DataBind();
        }

        protected void LinkButtonTemp_Click(object sender, EventArgs e)
        {
            LoadCustomerSpecifications(DropDownList1);
        }


    }
}