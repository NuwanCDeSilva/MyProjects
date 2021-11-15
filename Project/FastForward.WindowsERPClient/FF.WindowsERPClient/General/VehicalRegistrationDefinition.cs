using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data.OleDb;

namespace FF.WindowsERPClient.General
{
  
    public partial class VehicalRegistrationDefinition : Base
    {
        DataTable select_PC_List = new DataTable();
        DataTable select_ITEMS_List = new DataTable();
        Int32 hiddenFieldRowCount;
        List<PaymentTypeRef> PayType;
        List<string> termsList;
        List<string> FinalSelectSchemeList;
        List<termList> TermList;
        List<schemeList> FinalSelectSchemeList_;
        StringBuilder _errorLst = new StringBuilder();
        bool isExcelUpload = false;
       
        Base bsObj;
        public Int32 HiddenFieldRowCount
        {
            get { return hiddenFieldRowCount; }
            set { hiddenFieldRowCount = value; }
        }
        private DataTable main;

        public DataTable Main
        {
            get { return main; }
            set { main = value; }
        }
        private string company;

        public string Company
        {
            get { return company; }
            set { company = value; }
        }

        public VehicalRegistrationDefinition()
        {
            InitializeComponent();
            panel_edit.Visible = false;
            //--------------------------------------------
            LoadSchemeCategory();
            LoadPriceBook();   

            bind_Combo_DropDownListPartyTypes();
            BindSalesTypes();
            PayType = new List<PaymentTypeRef>();
            termsList = new List<string>();
            FinalSelectSchemeList = new List<string>();
            FinalSelectSchemeList_ = new List<schemeList>();
            TermList = new List<termList>();
            ddlCustTp.SelectedText = "";
            //------------------------------------------------------
            HiddenFieldRowCount = 0;
            Company = BaseCls.GlbUserComCode;
            BindCombos();
            CreateTableMain();
            GridBind(GridViewFinal, Main);
            TextBoxCvalue.Text = string.Format("{0:n2}", 0);
            TextBoxRvalue.Text = string.Format("{0:n2}", 0);
            ucProfitCenterSearch1.Company = BaseCls.GlbUserComCode;
            LoadCombos(-5);
        }
        private void LoadPriceBook()
        {
            List<PriceBookRef> _priceBook = CHNLSVC.Sales.GetPriceBooklist(BaseCls.GlbUserComCode);
            DropDownListPriceBook.DataSource = _priceBook;
            DropDownListPriceBook.DisplayMember = "Sapb_desc";
            DropDownListPriceBook.ValueMember = "Sapb_pb";
        }
        private void BindSalesTypes()
        {
            //cmbPayTP.DataSource = CHNLSVC.General.GetSalesTypes("", null, null);
            //cmbPayTP.DisplayMember = "srtp_desc";
            //cmbPayTP.ValueMember = "srtp_cd";
            List<PaymentTypeRef> list = CHNLSVC.Sales.GetAllPaymentType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, null);
            cmbPayTP.DataSource = list;//CHNLSVC.Sales.GetAllPaymentType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, null);
              //cmbPayTP.DisplayMember = "SAPT_DESC";
              //      cmbPayTP.ValueMember = "SAPT_CD";
            if (list!=null)
            {
                if (list.Count>0)
                {
                    foreach (PaymentTypeRef ptr in list)
                    {
                        ptr.Sapt_desc =  ptr.Sapt_cd+"--"+ ptr.Sapt_desc;
                    }
                    cmbPayTP.DisplayMember = "SAPT_DESC";
                    cmbPayTP.ValueMember = "SAPT_CD";
                }
            }
           
        }

        private void bind_Combo_DropDownListPartyTypes()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();

            PartyTypes.Add("GPC", "GPC");
            PartyTypes.Add("CHNL", "Channel");
            PartyTypes.Add("SCHNL", "Sub Channel");
            PartyTypes.Add("AREA", "Area");
            PartyTypes.Add("REGION", "Region");
            PartyTypes.Add("ZONE", "Zone");
            PartyTypes.Add("PC", "PC");

            DropDownListPartyTypes.DataSource = new BindingSource(PartyTypes, null);
            DropDownListPartyTypes.DisplayMember = "Value";
            DropDownListPartyTypes.ValueMember = "Key";
        }
        private void LoadSchemeCategory()
        {
            DataTable dt = CHNLSVC.Sales.GetSAllchemeCategoryies("%");
            DropDownListSchemeCategory.DataSource = dt;
            DropDownListSchemeCategory.DisplayMember = "HSC_DESC";
            DropDownListSchemeCategory.ValueMember = "HSC_CD";
        }
        private void GridBind(DataGridView gv, DataTable dt)
        {
            gv.DataSource = null;
            gv.AutoGenerateColumns = false;
            gv.DataSource = dt;

        }
        private void CreateTableMain()
        {
            Main = new DataTable();
            Main.Columns.Add("Party", typeof(List<MasterProfitCenter>));
            Main.Columns.Add("Cat", typeof(List<String>));
            Main.Columns.Add("From", typeof(DateTime));
            Main.Columns.Add("To", typeof(DateTime));
            Main.Columns.Add("Sales_Type", typeof(string));
            Main.Columns.Add("Registration_Value", typeof(Decimal));
            Main.Columns.Add("Claim_Value", typeof(Decimal));
            Main.Columns.Add("Sales_Type_desc", typeof(string));
        }
        private void BindCombos()
        {
            //DataTable datasource = CHNLSVC.General.GetPartyTypes();
            //DropDownListCompany.Items.Clear();
            //DropDownListCompany.Items.Add(new ListItem("--select--", "-1"));
            //foreach (DataRow dr in datasource.Rows)
            //{
            //    DropDownListCompany.Items.Add(new ListItem(dr["mpc_com"].ToString(), dr["mpc_com"].ToString()));
            //}

            DataTable datasource2 = CHNLSVC.General.GetSalesTypes("", null, null);
            
            //foreach (DataRow dr in datasource2.Rows)
            //{
            //    DropDownListSType.Items.Add(new ListItem(dr["srtp_cd"].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr["srtp_cd"].ToString().Length)) + "-" + dr["srtp_desc"].ToString(), dr["srtp_cd"].ToString()));
            //}
            DropDownListSType.DataSource = datasource2;

            DropDownListSType.DisplayMember = "result";
            DropDownListSType.ValueMember = "srtp_cd";
           // DropDownListSType.SelectedIndex = -1;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            VehicalRegistrationDefinition formnew = new VehicalRegistrationDefinition();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }
        private List<string> GetSelected_Hierachy_List()
        {
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvParty.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells["party_Code"].Value.ToString());
                }
            }
            return list;
        }
        private List<string> GetSelectedPCList()
        {
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvProfCents.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }

            }
            return list;
        }
        private List<string> GetSelectedItemsList()
        {
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in GridAll_Items.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells["mi_cd"].Value.ToString());
                    
                }

            }
            return list;
        }
        // Tharanga 01/jun/2017
        private List<string> GetSelectedItemsListExcel()
        {
            List<string> list = new List<string>();
            List<string> ser_No = new List<string>();
            foreach (DataGridViewRow dgvr in GridAll_Items.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells["mi_cd"].Value.ToString() + " " + dgvr.Cells["ser_no"].Value.ToString());
                   
                    
                }

            }
            return list;
        }
        private List<string> get_selected_Schemes()
        {
            grvSchemes.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvSchemes.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells["HSC_CD"].Value.ToString());
                }
            }
            return list;
        }
        private List<string> get_selected_priceBooks()
        {
            grvPriceLevel.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvPriceLevel.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }
            }
            return list;
        }
        private List<string> get_selected_priceBooksLevels()
        {
            grvPriceLevel.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvPriceLevel.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[2].Value.ToString());
                }
            }
            return list;
        }
        private List<string> get_selected_PayModes()
        {
            grvPayType.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvPayType.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    //termsList
                    list.Add(dgvr.Cells[1].Value.ToString());                   
                }
            }
            return list;
        }
        private void viewSaveList()
        {
            if (txtCircularNo.Text.Trim()=="")
            {
                MessageBox.Show("Please enter circular code!", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //-------------------------------------------------------------------------------------
            List<VehicalRegistrationDefnition> DEFN = CHNLSVC.General.Get_vehRegDefnByCircular(txtCircularNo.Text.Trim());
            if (DEFN != null)
            {
                if (DEFN.Count > 0)
                {
                    MessageBox.Show("Circular code already exists! Please enter new code.", "Cirular", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCircularNo.Focus();
                    return;
                }
            }
            //-------------------------------------------------------------------------------------
            List<string> PC_list = GetSelected_Hierachy_List();
            if (PC_list.Count < 1)
            {
                MessageBox.Show("Please select from hierarchy!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> ITEMS_list = GetSelectedItemsList();
            if (ITEMS_list.Count < 1)
            {
                MessageBox.Show("Please add and select items.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow dr = Main.NewRow();
            List<MasterProfitCenter> pcs = new List<MasterProfitCenter>();
            List<string> items = ITEMS_list;
            foreach (string pc_ in PC_list)
            {
                MasterProfitCenter pc = new MasterProfitCenter();
                pc.Mpc_cd = pc_;
                
                pc.Mpc_com = Company;
                pcs.Add(pc);
            }           
            try
            {
               Convert.ToDateTime(TextBoxFrom.Text);
               Convert.ToDateTime(TextBoxTo.Text);
            }
            catch (Exception)
            {

                MessageBox.Show("Please select From and To  dates", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToDateTime(TextBoxFrom.Text).Date > Convert.ToDateTime(TextBoxTo.Text).Date)
            {

                MessageBox.Show("From date should be less than to date!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Convert.ToDecimal(TextBoxCvalue.Text);
                Convert.ToDecimal(TextBoxRvalue.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter valid values.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToDecimal(TextBoxCvalue.Text) < 0 || Convert.ToDecimal(TextBoxCvalue.Text) < 0)
            {
                MessageBox.Show("Values should be greater than 0.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                Convert.ToDecimal(TextBoxRvalue.Text);
                Convert.ToDecimal(TextBoxCvalue.Text);
            }
            catch (Exception)
            {

                MessageBox.Show("Registration value and claim value has to be number", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtTerm.Text.Trim()=="")
            {
                txtTerm.Text = "0";
            }

            //------------------------------------------------------------------------------------
            try
            {
                Convert.ToDecimal(txtFromQty.Text);
                Convert.ToDecimal(txtToQty.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter valid qty. values.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToDecimal(txtFromQty.Text) < 0 || Convert.ToDecimal(txtToQty.Text) < 0)
            {
                MessageBox.Show("Qty cannot be less than 0.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToDecimal(txtFromQty.Text)> Convert.ToDecimal(txtToQty.Text) )
            {
                MessageBox.Show("From Qty. cannot be greater than To Qty.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //-----------------------------------------------------------------------------------
            if (Convert.ToDecimal(TextBoxCvalue.Text) > Convert.ToDecimal(TextBoxRvalue.Text))
            {
                MessageBox.Show("Cannot claim more than registration value.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //------------------------------------------------------------------------------------
            try
            {
                Convert.ToDecimal(txtInvFromVal.Text);
                Convert.ToDecimal(txtInvToVal.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter valid From Value and To Value.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToDecimal(txtInvFromVal.Text) < 0 || Convert.ToDecimal(txtInvToVal.Text) < 0)
            {
                MessageBox.Show("From/To Value cannot be less than 0.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToDecimal(txtInvFromVal.Text) > Convert.ToDecimal(txtInvToVal.Text))
            {
                MessageBox.Show("From Value cannot be greater than To Value.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime today = CHNLSVC.Security.GetServerDateTime().Date;
            DateTime newFrom = TextBoxFrom.Value.Date;
            DateTime newTo = TextBoxTo.Value.Date;

            //if (newFrom < today || newTo < today)
            //{
            //    MessageBox.Show("From and To dates cannot be less than today!", "Veiw", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //-----------------------------------------------------------------------------------
            List<string> SCHEME_list = FinalSelectSchemeList;//get_selected_Schemes();
            List<string> list_PB = get_selected_priceBooks();
            List<string> list_PB_LVL = get_selected_priceBooksLevels();

            List<string> list_PayMode_list = get_selected_PayModes();

            List<VehicalRegistrationDefnition> viewList = new List<VehicalRegistrationDefnition>();

            foreach(string hier in PC_list)
            {
                foreach (string itm in ITEMS_list)
                {
                    if (SCHEME_list.Count == 0)
                    {
                        if (list_PayMode_list.Count == 0)
                        {
                            #region -- price books
                            if (list_PB_LVL.Count == 0)
                            {
                                VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
                                VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
                                VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;//chkInvVal.Checked; //
                                VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
                                VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
                                VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
                                VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
                                VREG_DEFN.Svrd_cre_dt = DateTime.Now;
                                VREG_DEFN.Svrd_cust_tp = ddlCustTp.SelectedText.ToString();//txtCustTp.Text.Trim();
                                VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
                                VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
                                VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
                                VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
                                VREG_DEFN.Svrd_itm = itm;
                               // VREG_DEFN.Svrd_pay_tp = payMode;// cmbPayTP.SelectedValue.ToString();
                                //VREG_DEFN.Svrd_pb;
                                //VREG_DEFN.Svrd_pb_lvl;
                                VREG_DEFN.Svrd_pc = hier;
                                VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
                                //VREG_DEFN.Svrd_scheme = schm;
                                //VREG_DEFN.Svrd_seq;
                                //VREG_DEFN.Svrd_term = Convert.ToInt32(txtTerm.Text.Trim());
                                VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
                                VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
                                VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
                                VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
                                viewList.Add(VREG_DEFN);
                            }
                            else
                            {
                                for (int i = 0; i < list_PB_LVL.Count; i++)
                                {
                                    VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
                                    VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
                                    VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;//chkInvVal.Checked; //
                                    VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
                                    VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
                                    VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
                                    VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
                                    VREG_DEFN.Svrd_cre_dt = DateTime.Now;
                                    VREG_DEFN.Svrd_cust_tp = ddlCustTp.SelectedText.ToString();//txtCustTp.Text.Trim();
                                    VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
                                    VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
                                    VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
                                    VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
                                    VREG_DEFN.Svrd_itm = itm;
                                    //VREG_DEFN.Svrd_pay_tp = //cmbPayTP.SelectedValue.ToString();
                                    VREG_DEFN.Svrd_pb = list_PB[i].ToString();
                                    VREG_DEFN.Svrd_pb_lvl = list_PB_LVL[i].ToString();
                                    VREG_DEFN.Svrd_pc = hier;
                                    VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
                                    //VREG_DEFN.Svrd_scheme = schm;
                                    //VREG_DEFN.Svrd_seq;
                                    //VREG_DEFN.Svrd_term = Convert.ToInt32(txtTerm.Text.Trim());
                                    VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
                                    VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
                                    VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
                                    VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
                                    viewList.Add(VREG_DEFN);
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            foreach (string payMode in list_PayMode_list)
                            {
                                #region -- price books
                                if (list_PB_LVL.Count == 0)
                                {
                                    if (payMode == "CRCD")
                                    {
                                        if (termsList.Count == 0)
                                        {
                                            VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
                                            VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
                                            VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;// chkInvVal.Checked; //
                                            VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
                                            VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
                                            VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
                                            VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
                                            VREG_DEFN.Svrd_cre_dt = DateTime.Now;
                                            VREG_DEFN.Svrd_cust_tp = ddlCustTp.SelectedText.ToString();//txtCustTp.Text.Trim();
                                            VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
                                            VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
                                            VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
                                            VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
                                            VREG_DEFN.Svrd_itm = itm;
                                            VREG_DEFN.Svrd_pay_tp = payMode;// cmbPayTP.SelectedValue.ToString();
                                            //VREG_DEFN.Svrd_pb;
                                            //VREG_DEFN.Svrd_pb_lvl;
                                            VREG_DEFN.Svrd_pc = hier;
                                            VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
                                            //VREG_DEFN.Svrd_scheme = schm;
                                            //VREG_DEFN.Svrd_seq;
                                            VREG_DEFN.Svrd_term = 0;//Convert.ToInt32(txtTerm.Text.Trim());
                                            VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
                                            VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
                                            VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
                                            VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
                                            viewList.Add(VREG_DEFN);
                                        }
                                        else
                                        {
                                            foreach (string term in termsList)
                                            {
                                                VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
                                                VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
                                                VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;// chkInvVal.Checked; //
                                                VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
                                                VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
                                                VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
                                                VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
                                                VREG_DEFN.Svrd_cre_dt = DateTime.Now;
                                                VREG_DEFN.Svrd_cust_tp = ddlCustTp.SelectedText.ToString();//txtCustTp.Text.Trim();
                                                VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
                                                VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
                                                VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
                                                VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
                                                VREG_DEFN.Svrd_itm = itm;
                                                VREG_DEFN.Svrd_pay_tp = payMode;// cmbPayTP.SelectedValue.ToString();
                                                //VREG_DEFN.Svrd_pb;
                                                //VREG_DEFN.Svrd_pb_lvl;
                                                VREG_DEFN.Svrd_pc = hier;
                                                VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
                                                //VREG_DEFN.Svrd_scheme = schm;
                                                //VREG_DEFN.Svrd_seq;
                                                VREG_DEFN.Svrd_term = Convert.ToInt32(term);
                                                VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
                                                VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
                                                VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
                                                VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
                                                viewList.Add(VREG_DEFN);
                                            }
                                        
                                        }
                                      
                                    }
                                    else
                                    {
                                        VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
                                        VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
                                        VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;// chkInvVal.Checked; //
                                        VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
                                        VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
                                        VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
                                        VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
                                        VREG_DEFN.Svrd_cre_dt = DateTime.Now;
                                        VREG_DEFN.Svrd_cust_tp = ddlCustTp.SelectedText.ToString();//txtCustTp.Text.Trim();
                                        VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
                                        VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
                                        VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
                                        VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
                                        VREG_DEFN.Svrd_itm = itm;
                                        VREG_DEFN.Svrd_pay_tp = payMode;// cmbPayTP.SelectedValue.ToString();
                                        //VREG_DEFN.Svrd_pb;
                                        //VREG_DEFN.Svrd_pb_lvl;
                                        VREG_DEFN.Svrd_pc = hier;
                                        VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
                                        //VREG_DEFN.Svrd_scheme = schm;
                                        //VREG_DEFN.Svrd_seq;
                                        VREG_DEFN.Svrd_term = 0;//Convert.ToInt32(txtTerm.Text.Trim());
                                        VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
                                        VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
                                        VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
                                        VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
                                        viewList.Add(VREG_DEFN);
                                    }
                                  
                                }
                                else
                                {
                                    for (int i = 0; i < list_PB_LVL.Count; i++)
                                    {
                                        if (payMode == "CRCD")
                                        {
                                            if (termsList.Count == 0)
                                            {
                                                VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
                                                VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
                                                VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;//chkInvVal.Checked; //
                                                VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
                                                VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
                                                VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
                                                VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
                                                VREG_DEFN.Svrd_cre_dt = DateTime.Now;
                                                VREG_DEFN.Svrd_cust_tp = ddlCustTp.SelectedText.ToString();//txtCustTp.Text.Trim();
                                                VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
                                                VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
                                                VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
                                                VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
                                                VREG_DEFN.Svrd_itm = itm;
                                                VREG_DEFN.Svrd_pay_tp = payMode;//cmbPayTP.SelectedValue.ToString();
                                                VREG_DEFN.Svrd_pb = list_PB[i].ToString();
                                                VREG_DEFN.Svrd_pb_lvl = list_PB_LVL[i].ToString();
                                                VREG_DEFN.Svrd_pc = hier;
                                                VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
                                                //VREG_DEFN.Svrd_scheme = schm;
                                                //VREG_DEFN.Svrd_seq;
                                                VREG_DEFN.Svrd_term = 0; Convert.ToInt32(txtTerm.Text.Trim());
                                                VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
                                                VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
                                                VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
                                                VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
                                                viewList.Add(VREG_DEFN);
                                            }
                                            else
                                            {
                                                foreach (string term in termsList)
                                                {
                                                    VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
                                                    VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
                                                    VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;//chkInvVal.Checked; //
                                                    VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
                                                    VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
                                                    VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
                                                    VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
                                                    VREG_DEFN.Svrd_cre_dt = DateTime.Now;
                                                    VREG_DEFN.Svrd_cust_tp = ddlCustTp.SelectedText.ToString();//txtCustTp.Text.Trim();
                                                    VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
                                                    VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
                                                    VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
                                                    VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
                                                    VREG_DEFN.Svrd_itm = itm;
                                                    VREG_DEFN.Svrd_pay_tp = payMode;//cmbPayTP.SelectedValue.ToString();
                                                    VREG_DEFN.Svrd_pb = list_PB[i].ToString();
                                                    VREG_DEFN.Svrd_pb_lvl = list_PB_LVL[i].ToString();
                                                    VREG_DEFN.Svrd_pc = hier;
                                                    VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
                                                    //VREG_DEFN.Svrd_scheme = schm;
                                                    //VREG_DEFN.Svrd_seq;
                                                    VREG_DEFN.Svrd_term = Convert.ToInt32(term);// Convert.ToInt32(txtTerm.Text.Trim());
                                                    VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
                                                    VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
                                                    VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
                                                    VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
                                                    viewList.Add(VREG_DEFN);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
                                            VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
                                            VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;//chkInvVal.Checked; //
                                            VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
                                            VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
                                            VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
                                            VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
                                            VREG_DEFN.Svrd_cre_dt = DateTime.Now;
                                            VREG_DEFN.Svrd_cust_tp = ddlCustTp.SelectedText.ToString();//txtCustTp.Text.Trim();
                                            VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
                                            VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
                                            VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
                                            VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
                                            VREG_DEFN.Svrd_itm = itm;
                                            VREG_DEFN.Svrd_pay_tp = payMode;//cmbPayTP.SelectedValue.ToString();
                                            VREG_DEFN.Svrd_pb = list_PB[i].ToString();
                                            VREG_DEFN.Svrd_pb_lvl = list_PB_LVL[i].ToString();
                                            VREG_DEFN.Svrd_pc = hier;
                                            VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
                                            //VREG_DEFN.Svrd_scheme = schm;
                                            //VREG_DEFN.Svrd_seq;
                                            VREG_DEFN.Svrd_term = 0; Convert.ToInt32(txtTerm.Text.Trim());
                                            VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
                                            VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
                                            VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
                                            VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
                                            viewList.Add(VREG_DEFN);
                                        }
                                       
                                    }
                                }
                                #endregion
                            }
                           
                        }

                      
                        
                    }
                    else //SCEMES COUNT>0
                    {
                        foreach (string schm in SCHEME_list)
                        {
                            if (list_PayMode_list.Count == 0)
                            {
                                #region -- price books
                                if (list_PB_LVL.Count == 0)
                                {
                                    VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
                                    VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
                                    VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;//chkInvVal.Checked; //
                                    VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
                                    VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
                                    VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
                                    VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
                                    VREG_DEFN.Svrd_cre_dt = DateTime.Now;
                                    VREG_DEFN.Svrd_cust_tp = ddlCustTp.SelectedText.ToString(); //txtCustTp.Text.Trim();
                                    VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
                                    VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
                                    VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
                                    VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
                                    VREG_DEFN.Svrd_itm = itm;
                                    //VREG_DEFN.Svrd_pay_tp = payMode;//cmbPayTP.SelectedValue.ToString();
                                    //VREG_DEFN.Svrd_pb;
                                    //VREG_DEFN.Svrd_pb_lvl;
                                    VREG_DEFN.Svrd_pc = hier;
                                    VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
                                    VREG_DEFN.Svrd_scheme = schm;
                                    //VREG_DEFN.Svrd_seq;
                                    //VREG_DEFN.Svrd_term = Convert.ToInt32(txtTerm.Text.Trim());
                                    VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
                                    VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
                                    VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
                                    VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
                                    viewList.Add(VREG_DEFN);
                                }
                                else
                                {
                                    for (int i = 0; i < list_PB_LVL.Count; i++)
                                    {
                                        VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
                                        VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
                                        VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked; //chkInvVal.Checked; //
                                        VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
                                        VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
                                        VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
                                        VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
                                        VREG_DEFN.Svrd_cre_dt = DateTime.Now;
                                        VREG_DEFN.Svrd_cust_tp = ddlCustTp.SelectedText.ToString();//txtCustTp.Text.Trim();
                                        VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
                                        VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
                                        VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
                                        VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
                                        VREG_DEFN.Svrd_itm = itm;
                                        //VREG_DEFN.Svrd_pay_tp = payMode;//cmbPayTP.SelectedValue.ToString();
                                        VREG_DEFN.Svrd_pb = list_PB[i].ToString();
                                        VREG_DEFN.Svrd_pb_lvl = list_PB_LVL[i].ToString();
                                        VREG_DEFN.Svrd_pc = hier;
                                        VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
                                        VREG_DEFN.Svrd_scheme = schm;
                                        //VREG_DEFN.Svrd_seq;
                                        //VREG_DEFN.Svrd_term = Convert.ToInt32(txtTerm.Text.Trim());
                                        VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
                                        VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
                                        VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
                                        VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
                                        viewList.Add(VREG_DEFN);
                                    }
                                }
                                #endregion

                            }
                            else
                            {
                                foreach (string payMode in list_PayMode_list)
                                {
                                    #region -- price books
                                    if (list_PB_LVL.Count == 0)
                                    {
                                        VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
                                        VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
                                        VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked; //chkInvVal.Checked; //
                                        VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
                                        VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
                                        VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
                                        VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
                                        VREG_DEFN.Svrd_cre_dt = DateTime.Now;
                                        VREG_DEFN.Svrd_cust_tp = ddlCustTp.SelectedText.ToString(); //txtCustTp.Text.Trim();
                                        VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
                                        VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
                                        VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
                                        VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
                                        VREG_DEFN.Svrd_itm = itm;
                                        VREG_DEFN.Svrd_pay_tp = payMode;//cmbPayTP.SelectedValue.ToString();
                                        //VREG_DEFN.Svrd_pb;
                                        //VREG_DEFN.Svrd_pb_lvl;
                                        VREG_DEFN.Svrd_pc = hier;
                                        VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
                                        VREG_DEFN.Svrd_scheme = schm;
                                        //VREG_DEFN.Svrd_seq;
                                        VREG_DEFN.Svrd_term = Convert.ToInt32(txtTerm.Text.Trim());
                                        VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
                                        VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
                                        VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
                                        VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
                                        viewList.Add(VREG_DEFN);
                                    }
                                    else
                                    {
                                        for (int i = 0; i < list_PB_LVL.Count; i++)
                                        {
                                            VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
                                            VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
                                            VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;//chkInvVal.Checked; //
                                            VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
                                            VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
                                            VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
                                            VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
                                            VREG_DEFN.Svrd_cre_dt = DateTime.Now;
                                            VREG_DEFN.Svrd_cust_tp = ddlCustTp.SelectedText.ToString(); //txtCustTp.Text.Trim();
                                            VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
                                            VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
                                            VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
                                            VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
                                            VREG_DEFN.Svrd_itm = itm;
                                            VREG_DEFN.Svrd_pay_tp = payMode;//cmbPayTP.SelectedValue.ToString();
                                            VREG_DEFN.Svrd_pb = list_PB[i].ToString();
                                            VREG_DEFN.Svrd_pb_lvl = list_PB_LVL[i].ToString();
                                            VREG_DEFN.Svrd_pc = hier;
                                            VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
                                            VREG_DEFN.Svrd_scheme = schm;
                                            //VREG_DEFN.Svrd_seq;
                                            VREG_DEFN.Svrd_term = Convert.ToInt32(txtTerm.Text.Trim());
                                            VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
                                            VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
                                            VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
                                            VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
                                            viewList.Add(VREG_DEFN);
                                        }
                                    }
                                    #endregion
                                }
                       
                                
                            }
                        }//-----
                        
                    }
                  
                }
            }

            //-----------------------------------------------------------------------------------
            GridViewFinal_new.DataSource = null;
            GridViewFinal.AutoGenerateColumns = false;
            GridViewFinal_new.DataSource = viewList;
            
        }
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            viewSaveList();

            #region Old view           
            //List<string> PC_list = GetSelectedPCList();
            //if (PC_list.Count < 1)
            //{
            //    MessageBox.Show("Please add and select profit centers.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            

            //List<string> ITEMS_list = GetSelectedItemsList();
            //if (ITEMS_list.Count < 1)
            //{
            //    MessageBox.Show("Please add and select items.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //DataRow dr = Main.NewRow();
            //List<MasterProfitCenter> pcs = new List<MasterProfitCenter>();
            //List<string> items = ITEMS_list;//new List<string>();
            //foreach (string pc_ in PC_list)
            //{
            //    MasterProfitCenter pc = new MasterProfitCenter();
            //    pc.Mpc_cd = pc_;
            //    //user change company after search ???
            //    pc.Mpc_com = Company;
            //    pcs.Add(pc);
            //}
           
            //dr["Party"] = pcs;
            //dr["Cat"] = items;
            //try
            //{
            //    dr["From"] = Convert.ToDateTime(TextBoxFrom.Text);
            //    dr["To"] = Convert.ToDateTime(TextBoxTo.Text);
            //}
            //catch (Exception)
            //{
                
            //    MessageBox.Show("Please select From and To  dates", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //if (Convert.ToDateTime(dr["From"]).Date > Convert.ToDateTime(dr["To"]).Date)
            //{
               
            //    MessageBox.Show("From date should be less than to date!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //try
            //{
            //    Convert.ToDecimal(TextBoxCvalue.Text);
            //    Convert.ToDecimal(TextBoxRvalue.Text);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Enter valid values.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //if (Convert.ToDecimal(TextBoxCvalue.Text) < 0 || Convert.ToDecimal(TextBoxCvalue.Text) < 0)
            //{
            //    MessageBox.Show("Values should be greater than 0.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //dr["Sales_Type"] = DropDownListSType.SelectedValue;
            //dr["Sales_Type_desc"] = DropDownListSType.Text;
            //try
            //{
            //    dr["Registration_Value"] = Convert.ToDecimal(TextBoxRvalue.Text);
            //    dr["Claim_Value"] = Convert.ToDecimal(TextBoxCvalue.Text);
            //}
            //catch (Exception)
            //{
              
            //    MessageBox.Show("Registration value and claim value has to be number", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            
            //Main.Rows.Add(dr);
            //GridBind(GridViewFinal, Main);
            //HiddenFieldRowCount = Main.Rows.Count;
            //TextBoxCvalue.Text = string.Format("{0:n2}", 0);
            //TextBoxRvalue.Text = string.Format("{0:n2}", 0);
            //TextBoxFrom.Value = DateTime.Now.Date;
            //TextBoxTo.Value = DateTime.Now.Date;

            #endregion
        }

        private void btnAddPc_Click(object sender, EventArgs e)
        {
            string oldCom = Company;
            Company = ucProfitCenterSearch1.Company;
            if (oldCom != Company)
            {
                this.btnClearPc_Click(null, null);
            }
            if (ucProfitCenterSearch1.Company == "")
            {
                return;
            }
            string com = ucProfitCenterSearch1.Company;
            string chanel = ucProfitCenterSearch1.Channel;
            string subChanel = ucProfitCenterSearch1.SubChannel;
            string area = ucProfitCenterSearch1.Area;
            string region = ucProfitCenterSearch1.Regien;
            string zone = ucProfitCenterSearch1.Zone;
            string pc = ucProfitCenterSearch1.ProfitCenter;

            DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com.ToUpper(), chanel.ToUpper(), subChanel.ToUpper(), area.ToUpper(), region.ToUpper(), zone.ToUpper(), pc.ToUpper());
            select_PC_List.Merge(dt);
            grvProfCents.DataSource = null;
            grvProfCents.AutoGenerateColumns = false;
            grvProfCents.DataSource = select_PC_List;
            this.btnAllPc_Click(sender, e);

            GridViewFinal.DataSource = null;
            GridViewFinal.AutoGenerateColumns = false;
        }

        private void btnAllPc_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvProfCents.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvProfCents.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnNonPc_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvProfCents.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvProfCents.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnClearPc_Click(object sender, EventArgs e)
        {
            // DataTable emptyDt = new DataTable();
            select_PC_List = new DataTable();
            grvProfCents.DataSource = null;
            grvProfCents.AutoGenerateColumns = false;
            grvProfCents.DataSource = select_PC_List;
            //grvProfCents.DataSource = emptyDt;
        }

        private void btnAllItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in GridAll_Items.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                GridAll_Items.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnNonItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in GridAll_Items.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                GridAll_Items.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnClearItem_Click(object sender, EventArgs e)
        {
            select_ITEMS_List = new DataTable();
            GridAll_Items.DataSource = null;
            GridAll_Items.AutoGenerateColumns = false;
            GridAll_Items.DataSource = select_ITEMS_List;
        }
        private void btnAddItem_Click(object sender, EventArgs e)
        {            
            //*************************************************************************************************************************
            //string main_category = txtCate1.Text.Trim().ToUpper() == "" ? "%" : txtCate1.Text.Trim().ToUpper();
            //string sub_category = txtCate2.Text.Trim().ToUpper() == "" ? "%" : txtCate2.Text.Trim().ToUpper();
            //string model = txtModel.Text.Trim().ToUpper() == "" ? "%" : txtModel.Text.Trim().ToUpper();
            //string brand = txtBrand.Text.Trim().ToUpper() == "" ? "%" : txtBrand.Text.Trim().ToUpper();
            string itemCode = TextBoxItem.Text.Trim().ToUpper() == "" ? "%" : TextBoxItem.Text.Trim().ToUpper();
            string main_category = DropDownListCat.SelectedValue.ToString() == "" ? "%" : DropDownListCat.SelectedValue.ToString();
            string sub_category = DropDownListSCat.SelectedValue.ToString() == "" ? "%" : DropDownListSCat.SelectedValue.ToString();
            string model = DropDownListIRange.SelectedValue.ToString() == "" ? "%" : DropDownListIRange.SelectedValue.ToString();
            string brand = DropDownListBrand.SelectedValue.ToString() == "" ? "%" : DropDownListBrand.SelectedValue.ToString();
          
            DataTable dataSource = CHNLSVC.General.GetVehicalRegistrationBrand(main_category, sub_category, model, itemCode, brand, "Code");
            if (dataSource.Rows.Count <= 0)
            {
                // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No Items");
                MessageBox.Show("No Items", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //******************************************************************************************
            DataTable dataSource2 = new DataTable();
            dataSource2.Columns.Add("mi_cd");
            dataSource2.Columns.Add("mi_shortdesc");

            foreach (DataRow drr in dataSource.Rows)
            {
                string itmcd = drr["mi_cd"].ToString();
                string descirption = drr["mi_shortdesc"].ToString();
                var _duplicate = from _dup in select_ITEMS_List.AsEnumerable()
                                 where _dup["mi_cd"].ToString() == itmcd
                                 select _dup;
                if (_duplicate.Count() == 0)
                {
                    DataRow DR2 = dataSource2.NewRow();
                    DR2["mi_cd"] = itmcd;
                    DR2["mi_shortdesc"] = descirption;
                    dataSource2.Rows.Add(DR2);
                }
            }
            //******************************************************************************************
            if (select_ITEMS_List.Rows.Count == 0)
            {
                select_ITEMS_List.Merge(dataSource);
            }
            else
            {
                select_ITEMS_List.Merge(dataSource2);
            }
            //******************************************************************************************
           // select_ITEMS_List.Merge(dataSource);
            GridAll_Items.DataSource = null;
            GridAll_Items.AutoGenerateColumns = false;
            GridAll_Items.DataSource = select_ITEMS_List;
            this.btnAllItem_Click(sender, e);

            GridViewFinal.DataSource = null;
            GridViewFinal.AutoGenerateColumns = false;
            //foreach (DataRow dr in dataSource.Rows)
            //{
            //    ListBoxItems.Items.Add(new ListItem(dr["mi_cd"].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(15 - dr["mi_cd"].ToString().Length)) + "-" + dr["mi_shortdesc"].ToString(), dr["mi_cd"].ToString()));
            //}
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
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
                //case CommonUIDefiniton.SearchUserControlType.Item:
                //    {
                //        // paramsText.Append(BaseCls.GlbUserComCode + seperator);"" + seperator + ""
                //        paramsText.Append(BaseCls.GlbUserComCode + seperator+ "" + seperator + "" + seperator +"" + seperator + "" + seperator);
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                    //Tharanga
                case CommonUIDefiniton.SearchUserControlType.ItemSerNo:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca  + seperator + TextBoxItem.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                    //------------------------------------------------------------------------------------
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GPC:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OPE:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        private void btnBrand_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtBrand;
            _CommonSearch.txtSearchbyword.Text = txtBrand.Text;
            _CommonSearch.ShowDialog();
            txtBrand.Focus();
        }

        private void btnMainCat_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCate1;
            _CommonSearch.txtSearchbyword.Text = txtCate1.Text;
            _CommonSearch.ShowDialog();
            txtCate1.Focus();
        }

        private void btnCat_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
            DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCate2;
            _CommonSearch.txtSearchbyword.Text = txtCate2.Text;
            _CommonSearch.ShowDialog();
            txtCate2.Focus();
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            //_CommonSearch.ReturnIndex = 0;
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            //DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
            //_CommonSearch.dvResult.DataSource = _result;
            //_CommonSearch.BindUCtrlDDLData(_result);
            //_CommonSearch.obj_TragetTextBox = TextBoxItem;
            //_CommonSearch.txtSearchbyword.Text = TextBoxItem.Text;
            //_CommonSearch.ShowDialog();
            //TextBoxItem.Focus();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = TextBoxItem;
            _CommonSearch.ShowDialog();
            TextBoxItem.Focus();
        }

        private void btnModel_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
            DataTable _result = CHNLSVC.CommonSearch.GetAllModels(_CommonSearch.SearchParams, null, null);

            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtModel; //txtBox;
            _CommonSearch.ShowDialog();
            txtModel.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.btnSaveNew_Click(null, null);

            #region  OLD code
            //try
            //{
            //    if (Main.Rows.Count > 0)
            //    {
            //        //main table
            //        for (int i = 0; i < Main.Rows.Count; i++)
            //        {
            //            List<MasterProfitCenter> pcsTem = (List<MasterProfitCenter>)Main.Rows[i][0];
            //            List<string> itemsTem = (List<string>)Main.Rows[i][1];
            //            //divide pcs list into parts(200 item) 
            //            for (int j = 0; j < pcsTem.Count; )
            //            {
            //                List<MasterProfitCenter> pcs = new List<MasterProfitCenter>();
            //                if (pcsTem.Count > (j + 200))
            //                {
            //                    pcs = pcsTem.GetRange(j, 200);
            //                    j = j + 200;
            //                }
            //                else
            //                {
            //                    pcs = pcsTem.GetRange(j, pcsTem.Count - j);
            //                    j = j + (pcsTem.Count - j);
            //                }
            //                //divide items list into parts(200 item) 
            //                for (int k = 0; k < itemsTem.Count; )
            //                {
            //                    if (itemsTem.Count > (k + 50))
            //                    {
            //                        List<string> items = itemsTem.GetRange(k, 50);
            //                        //send 200 pcs and 200 items at once
            //                        Int32 eff = CHNLSVC.General.SaveVehicalRegistrationDefinition(pcs, items, Convert.ToDateTime(Main.Rows[i][2]), Convert.ToDateTime(Main.Rows[i][3]), Main.Rows[i][4].ToString(), Convert.ToDecimal(Main.Rows[i][5]), Convert.ToDecimal(Main.Rows[i][6]), BaseCls.GlbUserID, Convert.ToInt32(CheckBoxMandatory.Checked));
            //                        k = k + 50;
            //                    }
            //                    else
            //                    {
            //                        List<string> items = itemsTem.GetRange(k, itemsTem.Count - k);
            //                        Int32 eff = CHNLSVC.General.SaveVehicalRegistrationDefinition(pcs, items, Convert.ToDateTime(Main.Rows[i][2]), Convert.ToDateTime(Main.Rows[i][3]), Main.Rows[i][4].ToString(), Convert.ToDecimal(Main.Rows[i][5]), Convert.ToDecimal(Main.Rows[i][6]), BaseCls.GlbUserID, Convert.ToInt32(CheckBoxMandatory.Checked));
            //                        k = k + (itemsTem.Count - k);
            //                    }

            //                }
            //            }
            //            // ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Records added sucessfully!');window.location='VehicalRegistrationDefinition.aspx'", true);
            //            MessageBox.Show("Records added sucessfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            this.btnClear_Click(null, null);
            //        }

            //    }
            //    else
            //    {
            //        MessageBox.Show("Please add registration details!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    }
            //}
            //catch (Exception err)
            //{
            //    Cursor.Current = Cursors.Default;
            //    CHNLSVC.CloseChannel(); 
            //    MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            #endregion
        }

        private void GridViewFinal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Int32 rowIndex = e.RowIndex;
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                //GridViewRow row = grvInvItems.SelectedRow;
                // DataGridViewRow row = GridViewFinal.Rows[rowIndex];
                Main.Rows.RemoveAt(e.RowIndex);
                GridBind(GridViewFinal, Main);
                HiddenFieldRowCount = Main.Rows.Count;
            }
        }

        private void txtBrand_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnBrand_Click(null, null);
        }

        private void txtCate1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnMainCat_Click(null, null);
        }

        private void txtCate2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnCat_Click(null, null);
        }

        private void txtModel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnModel_Click(null, null);
        }

        private void TextBoxItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnItem_Click(null, null);
        }

        private void txtBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.btnBrand_Click(null, null);
            }
        }

        private void txtCate1_ImeModeChanged(object sender, EventArgs e)
        {

        }

        private void txtCate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.btnMainCat_Click(null, null);
            }
        }

        private void txtCate2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.btnCat_Click(null, null);
            }
        }

        private void txtModel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.btnModel_Click(null, null);
            }
        }

        private void TextBoxItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.btnItem_Click(null, null);
            }
        }

        private void txtBrand_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtCate1.Focus();
            }
        }

        private void txtCate1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtCate2.Focus();
            }
        }

        private void txtCate2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtModel.Focus();
            }
        }

        private void txtModel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxItem.Focus();
            }
        }

        private void TextBoxItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnAddItem.Select();
            }
        }

        private void TextBoxFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxTo.Focus();
            }
        }

        private void TextBoxTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxRvalue.Focus();
            }
        }
        private void TextBoxRvalue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxCvalue.Focus();
            }
        }
        private void TextBoxCvalue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                DropDownListSType.Focus();                
                DropDownListSType.DroppedDown = true;
            }
        }

        private void DropDownListSType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CheckBoxMandatory.Focus();
        }

        private void CheckBoxMandatory_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ButtonAdd.Select();
            }
        }

        private void LoadCombos(int id)
        {


            //string _brand = (!string.IsNullOrEmpty(DropDownListBrand.SelectedValue)) ? (DropDownListBrand.SelectedValue) : "";
            //string _Mcat = (!string.IsNullOrEmpty(DropDownListCat.SelectedValue)) ? (DropDownListCat.SelectedValue) : "";
            //string _Scat = (!string.IsNullOrEmpty(DropDownListSCat.SelectedValue)) ? (DropDownListSCat.SelectedValue) : "";
            //string _Range = (!string.IsNullOrEmpty(DropDownListIRange.SelectedValue)) ? (DropDownListIRange.SelectedValue) : "";
            string _brand = "";
            string _Mcat = "";
            string _Scat = "";
            string _Range = "";
            try
            {
                _brand = (string.IsNullOrEmpty(DropDownListBrand.SelectedValue.ToString()) == false) ? (DropDownListBrand.SelectedValue.ToString()) : "";
                _Mcat = (!string.IsNullOrEmpty(DropDownListCat.SelectedValue.ToString())) ? (DropDownListCat.SelectedValue.ToString()) : "";
                _Scat = (!string.IsNullOrEmpty(DropDownListSCat.SelectedValue.ToString())) ? (DropDownListSCat.SelectedValue.ToString()) : "";
                _Range = (!string.IsNullOrEmpty(DropDownListIRange.SelectedValue.ToString())) ? (DropDownListIRange.SelectedValue.ToString()) : "";
            }
            catch (Exception ex)
            {
                _brand = "";
                _Mcat = "";
                _Scat = "";
                _Range = "";
            }
            //load brand
            if (id == 1)
            {
                // DropDownListCat.Items.Clear();
                // DropDownListCat.Items.Add(new ListItem("ALL", "%")); TODO
                // DropDownListSCat.Items.Clear();
                //  DropDownListIRange.Items.Clear();
                // DropDownListSCat.Items.Add(new ListItem("ALL", "%")); TODO
                DataTable dt1 = CHNLSVC.General.GetVehicalRegistrationBrand("", "", "", "", _brand, "MCat");
                if (dt1 != null)
                {
                    if (dt1.Rows.Count > 0)
                    {
                        DataRow dr = dt1.NewRow();
                        dt1.Rows.Add(dr);
                        dr["mi_cate_1"] = "";
                    }
                }
                dt1.DefaultView.Sort = "mi_cate_1"; 
                DropDownListCat.DataSource = dt1;
                DropDownListCat.ValueMember = "mi_cate_1";
                DropDownListCat.DisplayMember = "mi_cate_1";
                DropDownListCat.SelectedValue = "";
                // DropDownListCat.DataBind();

                DataTable dt2 = CHNLSVC.General.GetVehicalRegistrationBrand("", "", "", "", _brand, "SCat");
                if (dt2 != null)
                {
                    if (dt2.Rows.Count > 0)
                    {
                        DataRow dr = dt2.NewRow();
                        dt2.Rows.Add(dr);
                        dr["mi_cate_2"] = "";
                    }
                }
                dt2.DefaultView.Sort = "mi_cate_2"; 
                DropDownListSCat.DataSource = dt2;
                DropDownListSCat.ValueMember = "mi_cate_2";
                DropDownListSCat.DisplayMember = "mi_cate_2";
                DropDownListSCat.SelectedValue = "";
                // DropDownListSCat.DataBind();
                // DropDownListIRange.Items.Add(new ListItem("ALL", "%")); TODO
                DataTable dt3 = CHNLSVC.General.GetVehicalRegistrationBrand("", "", "", "", _brand, "Range");
                if (dt3 != null)
                {
                    if (dt3.Rows.Count > 0)
                    {
                        DataRow dr = dt3.NewRow();
                        dt3.Rows.Add(dr);
                        dr["mi_model"] = "";
                    }
                }
                dt3.DefaultView.Sort = "mi_model"; 
                DropDownListIRange.DataSource = dt3;
                DropDownListIRange.ValueMember = "mi_model";
                DropDownListIRange.DisplayMember = "mi_model";
                DropDownListIRange.SelectedValue = "";
                // DropDownListIRange.DataBind();
            }
            //load cat
            else if (id == 2)
            {
                //DropDownListSCat.Items.Clear();
                //DropDownListSCat.Items.Add(new ListItem("ALL", "%")); TODO
                DataTable dt1 = CHNLSVC.General.GetVehicalRegistrationBrand(_Mcat, "", "", "", "", "SCat");
                if (dt1 != null)
                {
                    if (dt1.Rows.Count > 0)
                    {
                        DataRow dr = dt1.NewRow();
                        dt1.Rows.Add(dr);
                        dr["mi_cate_2"] = "";
                    }
                }
                dt1.DefaultView.Sort = "mi_cate_2"; 
                DropDownListSCat.DataSource = dt1;
                DropDownListSCat.ValueMember = "mi_cate_2";
                DropDownListSCat.DisplayMember = "mi_cate_2";
                DropDownListSCat.SelectedValue = "";
            }
            //load sub cat
            else if (id == 3)
            {
                //DropDownListIRange.Items.Clear();
                //DropDownListIRange.Items.Add(new ListItem("ALL", "%"));
                //DropDownListIRange.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand("", _Scat, "", "", "", "Range");
                //DropDownListIRange.DataTextField = "mi_model";
                //DropDownListIRange.DataValueField = "mi_model";
                //DropDownListIRange.DataBind();

            }

            //load  item
            else
            {
                //  DropDownListIRange.Items.Add(new ListItem("ALL", "%")); TODO
                //  DropDownListBrand.Items.Add(new ListItem("ALL", "%")); TODO
                //  DropDownListSCat.Items.Add(new ListItem("ALL", "%")); TODO
                //  DropDownListCat.Items.Add(new ListItem("ALL", "%")); TODO
                //  DropDownListIRange.Items.Add(new ListItem("ALL", "%")); TODO
                DataTable dt1 = CHNLSVC.General.GetVehicalRegistrationBrand(_Mcat, _Scat, _Range, "", _brand, "Brand");
                if (dt1 != null)
                {
                    if (dt1.Rows.Count > 0)
                    {
                        DataRow dr = dt1.NewRow();
                        dt1.Rows.Add(dr);
                        dr["mi_brand"] = "";
                    }
                }
                dt1.DefaultView.Sort = "mi_brand"; 
                DropDownListBrand.DataSource = dt1;
                DropDownListBrand.ValueMember = "mi_brand";
                DropDownListBrand.DisplayMember = "mi_brand";
                DropDownListBrand.SelectedValue = "";
                
                _brand = (!string.IsNullOrEmpty(DropDownListBrand.SelectedValue.ToString())) ? (DropDownListBrand.SelectedValue.ToString()) : "";

                DataTable dt2 = CHNLSVC.General.GetVehicalRegistrationBrand(_Mcat, _Scat, _Range, "", _brand, "MCat");
                if (dt2 != null)
                {
                    if (dt2.Rows.Count > 0)
                    {
                        DataRow dr = dt2.NewRow();
                        dt2.Rows.Add(dr);
                        dr["mi_cate_1"] = "";
                    }
                }
                dt2.DefaultView.Sort = "mi_cate_1"; 
                DropDownListCat.DataSource = dt2;
                DropDownListCat.ValueMember = "mi_cate_1";
                DropDownListCat.DisplayMember = "mi_cate_1";
                DropDownListCat.SelectedValue = "";
             
                _brand = (!string.IsNullOrEmpty(DropDownListBrand.SelectedValue.ToString())) ? (DropDownListBrand.SelectedValue.ToString()) : "";

                _Mcat = (!string.IsNullOrEmpty(DropDownListCat.SelectedValue.ToString())) ? (DropDownListCat.SelectedValue.ToString()) : "";

                DataTable dt3 = CHNLSVC.General.GetVehicalRegistrationBrand(_Mcat, _Scat, _Range, "", _brand, "SCat");
                if (dt3 != null)
                {
                    if (dt3.Rows.Count > 0)
                    {
                        DataRow dr = dt3.NewRow();
                        dt3.Rows.Add(dr);
                        dr["mi_cate_2"] = "";
                    }
                }
                dt3.DefaultView.Sort = "mi_cate_2"; 
                DropDownListSCat.DataSource = dt3;
                DropDownListSCat.ValueMember = "mi_cate_2";
                DropDownListSCat.DisplayMember = "mi_cate_2";
                DropDownListSCat.SelectedValue = "";
               
                _brand = (!string.IsNullOrEmpty(DropDownListBrand.SelectedValue.ToString())) ? (DropDownListBrand.SelectedValue.ToString()) : "";
                _Mcat = (!string.IsNullOrEmpty(DropDownListCat.SelectedValue.ToString())) ? (DropDownListCat.SelectedValue.ToString()) : "";
                _Scat = (!string.IsNullOrEmpty(DropDownListSCat.SelectedValue.ToString())) ? (DropDownListSCat.SelectedValue.ToString()) : "";

                DataTable dt4 = CHNLSVC.General.GetVehicalRegistrationBrand(_Mcat, _Scat, _Range, "", _brand, "Range");
                if (dt4 != null)
                {
                    if (dt4.Rows.Count > 0)
                    {
                        DataRow dr = dt4.NewRow();
                        dt4.Rows.Add(dr);
                        dr["mi_model"] = "";
                    }
                }
                dt4.DefaultView.Sort = "mi_model"; 
                DropDownListIRange.DataSource = dt4;
                DropDownListIRange.ValueMember = "mi_model";
                DropDownListIRange.DisplayMember = "mi_model";
                DropDownListIRange.SelectedValue = "";
               
                _brand = (!string.IsNullOrEmpty(DropDownListBrand.SelectedValue.ToString())) ? (DropDownListBrand.SelectedValue.ToString()) : "";
                _Mcat = (!string.IsNullOrEmpty(DropDownListCat.SelectedValue.ToString())) ? (DropDownListCat.SelectedValue.ToString()) : "";
                _Scat = (!string.IsNullOrEmpty(DropDownListSCat.SelectedValue.ToString())) ? (DropDownListSCat.SelectedValue.ToString()) : "";
                _Range = (!string.IsNullOrEmpty(DropDownListIRange.SelectedValue.ToString())) ? (DropDownListIRange.SelectedValue.ToString()) : "";

            }
        }

        private void DropDownListSCat_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadCombos(3);
        }

        private void DropDownListBrand_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadCombos(1);
        }

        private void DropDownListCat_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadCombos(2);
        }
        private void LoadSchemeType()
        {
            if (DropDownListSchemeCategory.SelectedIndex == -1)
            {
                return;
            }
            List<HpSchemeType> _schemeList = CHNLSVC.Sales.GetSchemeTypeByCat_com(DropDownListSchemeCategory.SelectedValue.ToString(), BaseCls.GlbUserComCode);
           // List<HpSchemeType> _schemeList = CHNLSVC.Sales.GetSchemeTypeByCategory(DropDownListSchemeCategory.SelectedValue.ToString());

          
                

            if (_schemeList == null)
            {
                DropDownListSchemeType.DataSource = null;
                return;
            }

            foreach (HpSchemeType sc in _schemeList)
            {
                string space = "";
                if ((10 - sc.Hst_cd.Length) > 0)
                {
                    for (int i = 0; i <= (10 - sc.Hst_cd.Length); i++)
                    {
                        space = space + " ";
                    }
                }
                sc.Hst_desc = sc.Hst_cd + space + "--" + sc.Hst_desc;
            }

            DropDownListSchemeType.DataSource = _schemeList;
            DropDownListSchemeType.DisplayMember = "Hst_desc";
            DropDownListSchemeType.ValueMember = "Hst_cd";

            CheckBoxAll.Checked = false;
        }
        private void DropDownListSchemeCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.btnClearScheme_Click(null, null);
            LoadSchemeType();
        }

        private void btnClearScheme_Click(object sender, EventArgs e)
        {
            FinalSelectSchemeList = new List<string>();
            FinalSelectSchemeList_ = new List<schemeList>();
            grvFinalSelectSch.DataSource = null;
            grvFinalSelectSch.DataSource = FinalSelectSchemeList_;
        }

        private void btnAll_Schemes_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvSchemes.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvSchemes.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnNon_Schemes_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvSchemes.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvSchemes.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void DropDownListSchemeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.btnClearScheme_Click(null, null);
            this.btnAddSchemes_Click(null, null);
        }

        private void btnAddSchemes_Click(object sender, EventArgs e)
        {
            if (DropDownListSchemeCategory.SelectedIndex == -1)
            {
                return;
            }
            if (CheckBoxAll.Checked == true)
            {
                DropDownListSchemeType.SelectedIndex = -1;
            }
            if (CheckBoxAll.Checked == true)
            {//tharanga
                List<HpSchemeType> _schemeList = CHNLSVC.Sales.GetSchemeTypeByCategory(DropDownListSchemeCategory.SelectedValue.ToString());
                //DataTable datasource = CHNLSVC.Sales.GetSchemes_alw_com("TYPE", DropDownListSchemeCategory.SelectedValue.ToString(), BaseCls.GlbUserComCode);
                //DropDownListSchemeType.DataSource = _schemeList;
                //DropDownListSchemeType.DisplayMember = "Hst_desc";
                //DropDownListSchemeType.ValueMember = "Hst_cd";

                DataTable dt = new DataTable();
                foreach (HpSchemeType schTp in _schemeList)
                {
                    DataTable dt1 = CHNLSVC.Sales.GetSchemes_alw_com("TYPE", schTp.Hst_cd, BaseCls.GlbUserComCode);
                    dt.Merge(dt1);
                }

                grvSchemes.DataSource = null;
                grvSchemes.AutoGenerateColumns = false;
                grvSchemes.DataSource = dt;
            }
            else
            {
                if (DropDownListSchemeType.SelectedIndex == -1)
                {
                    return;
                }
                DataTable datasource = CHNLSVC.Sales.GetSchemes_alw_com("TYPE", DropDownListSchemeType.SelectedValue.ToString(),BaseCls.GlbUserComCode);
                CheckBoxAll.Checked = false;

                grvSchemes.DataSource = null;
                grvSchemes.AutoGenerateColumns = false;
                grvSchemes.DataSource = datasource;
            }
        }

        private void DropDownListPartyTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnClearHirchy_Click(null, null);
            this.btnAddParty_Click(null, null);
        }

        private void btnClearHirchy_Click(object sender, EventArgs e)
        {
            grvParty.DataSource = null;
            grvParty.AutoGenerateColumns = false;
           
        }

        private void btnAll_Hirchy_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvParty.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvParty.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnNon_Hierachy_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvParty.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvParty.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnAddParty_Click(object sender, EventArgs e)
        {
            Base _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            DataTable dt = new DataTable();
            DataTable _result = new DataTable();
            if (DropDownListPartyTypes.SelectedValue.ToString() == "COM")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "CHNL")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "SCHNL")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }

            else if (DropDownListPartyTypes.SelectedValue.ToString() == "AREA")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "REGION")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "ZONE")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "PC")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "GPC")
            {
                // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GPC);
                //  _result = _basePage.CHNLSVC.CommonSearch.Get_GPC(_CommonSearch.SearchParams, null, null);
                _result = CHNLSVC.General.Get_GET_GPC("", "");
            }
            grvParty.DataSource = null;
            grvParty.AutoGenerateColumns = false;
            grvParty.DataSource = _result;
        }

        private void txtHierchCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtHierchCode.Focus();
                if (grvParty.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dgvr in grvParty.Rows)
                    {
                        if (dgvr.Cells["party_Code"].Value.ToString() == txtHierchCode.Text.Trim())
                        {
                            DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dgvr.Cells[0];
                            chk.Value = true;
                            dgvr.Selected = true;
                            MessageBox.Show("Selected!", "Select", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtHierchCode.Text = "";
                            //return;
                        }
                        else
                        {
                            DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                            if (Convert.ToBoolean(chk.Value) == false)
                            {
                                dgvr.Selected = false;
                            }
                        }
                    }
                }

            }
        }
     
        private void btnAddtoSaveList_Click(object sender, EventArgs e)
        {
            List<string> list_schems = get_selected_Schemes();
            FinalSelectSchemeList.AddRange(list_schems);
            foreach(string sch in list_schems)
            {
               schemeList newS=  new schemeList();
               newS.SchemeCode = sch;
               FinalSelectSchemeList_.Add(newS);
            }
            
            grvFinalSelectSch.DataSource = null;
            grvFinalSelectSch.DataSource = FinalSelectSchemeList_;
           
        }

        private void DropDownListSType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListSType.SelectedIndex==-1)
            {
                Panel_schems.Enabled=false;
                Panel_payTp.Enabled=false;
                return;
            }
            //--------------------------------------------------------
            grvSchemes.DataSource = null;
            DropDownListSchemeCategory.SelectedIndex = -1;
            DropDownListSchemeType.SelectedIndex = -1;
            
            grvPayType.DataSource = null;
            cmbPayTP.SelectedIndex = -1;


            Panel_schems.Enabled = false;
            Panel_payTp.Enabled = false;
                      
            if (DropDownListSType.SelectedValue.ToString() == "HS")//HIRE SALES
            {    
                Panel_schems.Enabled = true;
            }
            if (DropDownListSType.SelectedValue.ToString() == "CS")//CASH SALES
            {              
                if (chkAllowBaseOnPayMode.Checked==true)
                {
                    Panel_schems.Enabled = true;
                }
            }
        }

        private void btnSalesTypeAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string code = (cmbPayTP.SelectedValue != null) ? cmbPayTP.SelectedValue.ToString() : null;
                if(code==null)
                {
                    panel_terms.Visible = true;
                }
                List<PaymentTypeRef> _payMode = CHNLSVC.Sales.GetAllPaymentType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, code);
                foreach (PaymentTypeRef pay in _payMode)
                {
                    PaymentTypeRef _duplicat = PayType.Find(x => x.Sapt_cd == pay.Sapt_cd);
                    if (_duplicat == null)
                    {
                        PaymentTypeRef tem = new PaymentTypeRef();
                        tem.Sapt_cd = pay.Sapt_cd;
                        tem.Sapt_desc = pay.Sapt_desc;
                        if (pay.Sapt_cd == "CRCD")
                        {
                            tem.Sapt_cre_by = txtBank.Text;
                            tem.Sapt_mod_by = cmbCardType.Text;
                        }
                        if (pay.Sapt_cd == "LORE")
                        {
                            {
                                tem.Sapt_cre_by = txtLotyType.Text;
                                tem.Sapt_mod_by = "Classic";
                            }
                        }
                        PayType.Add(tem);
                    }
                    else
                    {
                        MessageBox.Show("Already contain " + pay.Sapt_cd + " type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }


                grvPayType.AutoGenerateColumns = false;
                BindingSource _source = new BindingSource();
                _source.DataSource = PayType;
                grvPayType.DataSource = _source;
                pnlBank.Visible = false;
                pnlLoyalty.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void cmbPayTP_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel_terms.Visible = false;
            if (cmbPayTP.SelectedValue != null && cmbPayTP.SelectedValue.ToString() == "CRCD")
            {
                panel_terms.Visible = true;
            }

          //-----------------------------------------------------------------------------------------------------------
            if (cmbPayTP.SelectedValue != null && cmbPayTP.SelectedValue.ToString() == "CRCD")
            {
                pnlBank.Visible = true;
                pnlLoyalty.Visible = false;
            }

            else if (cmbPayTP.SelectedValue != null && cmbPayTP.SelectedValue.ToString() == "LORE")
            {
                pnlLoyalty.Visible = true;
               // lblLoyalty.Text = "Point Selection";
                pnlBank.Visible = false;
            }
            else
            {
                //lblLoyalty.Text = "Quantity Selection";
                pnlLoyalty.Visible = false;
                pnlBank.Visible = false;
            }
        }

        private void chkInvVal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInvVal.Checked==true)
            {
                chkItmVal.Checked = false;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            viewSaveList();
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            List<string> ITEMS_list = null;
            List<string> ITEMS_list_excl = null;
            if (txtCircularNo.Text.Trim() == "")
            {
                MessageBox.Show("Please enter circular code!", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //-------------------------------------------------------------------------------------
            List<VehicalRegistrationDefnition> DEFN = CHNLSVC.General.Get_vehRegDefnByCircular(txtCircularNo.Text.Trim());
            if (DEFN!=null)
            {
                if (DEFN.Count>0)
                {
                    MessageBox.Show("Circular number already exist! Please enter new number.", "Cirular", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCircularNo.Focus();
                    return;
                }
            }
            //-------------------------------------------------------------------------------------

            //Tharanga
            //if (isExcelUpload == true)
            //{
            //    List<string> ITEMS_list = GetSelectedItemsList();
            //}
            List<string> PC_list = GetSelected_Hierachy_List();
            if (PC_list.Count < 1)
            {
                MessageBox.Show("Please select from hierarchy!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //ITEMS_list = GetSelectedItemsList();
            if (isExcelUpload == true)
            { ITEMS_list = GetSelectedItemsListExcel(); }
            else
            { ITEMS_list = GetSelectedItemsList(); }
           
           


            if (ITEMS_list.Count < 1 )
            {
                MessageBox.Show("Please add and select items.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow dr = Main.NewRow();
            List<MasterProfitCenter> pcs = new List<MasterProfitCenter>();
            List<string> items = ITEMS_list;
            foreach (string pc_ in PC_list)
            {
                MasterProfitCenter pc = new MasterProfitCenter();
                pc.Mpc_cd = pc_;

                pc.Mpc_com = Company;
                pcs.Add(pc);
            }
            try
            {
                Convert.ToDateTime(TextBoxFrom.Text);
                Convert.ToDateTime(TextBoxTo.Text);
            }
            catch (Exception)
            {

                MessageBox.Show("Please select From and To  dates", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToDateTime(TextBoxFrom.Text).Date > Convert.ToDateTime(TextBoxTo.Text).Date)
            {

                MessageBox.Show("From date should be less than to date!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Convert.ToDecimal(TextBoxCvalue.Text);
                Convert.ToDecimal(TextBoxRvalue.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter valid values.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToDecimal(TextBoxCvalue.Text) < 0 || Convert.ToDecimal(TextBoxCvalue.Text) < 0)
            {
                MessageBox.Show("Values should be greater than 0.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Convert.ToDecimal(TextBoxRvalue.Text);
                Convert.ToDecimal(TextBoxCvalue.Text);
            }
            catch (Exception)
            {

                MessageBox.Show("Registration value and claim value has to be number", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtTerm.Text.Trim() == "")
            {
                txtTerm.Text = "0";
            }
            #region

            
            ////-----------------------------------------------------------------------------------
            //List<string> SCHEME_list = get_selected_Schemes();

            //List<VehicalRegistrationDefnition> SaveList = new List<VehicalRegistrationDefnition>();

            //foreach (string hier in PC_list)
            //{
            //    foreach (string itm in ITEMS_list)
            //    {
            //        foreach (string schm in SCHEME_list)
            //        {
            //            VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
            //            VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
            //            VREG_DEFN.Svrd_check_on = chkInvVal.Checked; //
            //            VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
            //            VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
            //            VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
            //            VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
            //            VREG_DEFN.Svrd_cre_dt = DateTime.Now;
            //            VREG_DEFN.Svrd_cust_tp = txtCustTp.Text.Trim();
            //            VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
            //            VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
            //            VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
            //            VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
            //            VREG_DEFN.Svrd_itm = itm;
            //            VREG_DEFN.Svrd_pay_tp = cmbPayTP.SelectedValue.ToString();
            //            //VREG_DEFN.Svrd_pb;
            //            //VREG_DEFN.Svrd_pb_lvl;
            //            VREG_DEFN.Svrd_pc = hier;
            //            VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
            //            VREG_DEFN.Svrd_scheme = schm;
            //            //VREG_DEFN.Svrd_seq;
            //            VREG_DEFN.Svrd_term = Convert.ToInt32(txtTerm.Text.Trim());
            //            VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
            //            VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
            //            VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
            //            VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
            //            SaveList.Add(VREG_DEFN);
            //        }
            //    }
            //}
            #endregion
             
            //------------------------------------------------------------------------------------
            try
            {
                Convert.ToDecimal(txtFromQty.Text);
                Convert.ToDecimal(txtToQty.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter valid qty. values.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToDecimal(txtFromQty.Text) < 0 || Convert.ToDecimal(txtToQty.Text) < 0)
            {
                MessageBox.Show("Qty cannot be less than 0.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToDecimal(txtFromQty.Text) > Convert.ToDecimal(txtToQty.Text))
            {
                MessageBox.Show("From Qty. cannot be greater than To Qty.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //-----------------------------------------------------------------------------------
            if (Convert.ToDecimal(TextBoxCvalue.Text) > Convert.ToDecimal(TextBoxRvalue.Text))
            {
                MessageBox.Show("Cannot claim more than registration value.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //------------------------------------------------------------------------------------
            try
            {
                Convert.ToDecimal(txtInvFromVal.Text);
                Convert.ToDecimal(txtInvToVal.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter valid From Value and To Value.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToDecimal(txtInvFromVal.Text) < 0 || Convert.ToDecimal(txtInvToVal.Text) < 0)
            {
                MessageBox.Show("From/To Value cannot be less than 0.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToDecimal(txtInvFromVal.Text) > Convert.ToDecimal(txtInvToVal.Text))
            {
                MessageBox.Show("From Value cannot be greater than To Value.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            //-----------------------------------------------------------------------------------
            DateTime today = CHNLSVC.Security.GetServerDateTime().Date;
            DateTime newFrom = TextBoxFrom.Value.Date;
            DateTime newTo = TextBoxTo.Value.Date;

            //Sanjeewa - Modified to enter from date less than today - Requested by Dilanda
            //if (newFrom < today || newTo < today)
            //{
            //    MessageBox.Show("From and To dates cannot be less than today!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            //if (newTo < today)
            //{
            //    MessageBox.Show("To date cannot be less than today!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //-----------------------------------------------------------------------------------
                      
            List<string> SCHEME_list = FinalSelectSchemeList;// get_selected_Schemes();
            List<string> list_PB = get_selected_priceBooks();
            List<string> list_PB_LVL = get_selected_priceBooksLevels();

            List<string> list_PayMode_list = get_selected_PayModes();

            List<VehicalRegistrationDefnition> viewList = new List<VehicalRegistrationDefnition>();
            #region
            foreach (string hier in PC_list)
            {
                string itmcd ="";
                string ser_no="";
                foreach (string itm in ITEMS_list)
                {
                    if (isExcelUpload==true)
                    {
                        string[] conserno = itm.Split(' ');
                         itmcd = conserno[0].ToString().Trim();
                         ser_no = conserno[1].ToString().Trim();

                    }
                   
                   
                    if (SCHEME_list.Count == 0)
                    {
                        if (list_PayMode_list.Count == 0)
                        {
                            #region -- price books
                            if (list_PB_LVL.Count == 0)
                            {
                                VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
                                VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
                                VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;//chkInvVal.Checked; //
                                VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
                                VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
                                VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
                                VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
                                VREG_DEFN.Svrd_cre_dt = DateTime.Now;
                                VREG_DEFN.Svrd_cust_tp = ddlCustTp.SelectedText.ToString();//txtCustTp.Text.Trim();
                                VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
                                VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
                                VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
                                VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
                                VREG_DEFN.Svrd_itm = itm;
                                VREG_DEFN.Svrd_ser_1 = txtserNo.Text.Trim(); //Tharanga 2017/06/02

                                //Tharanga 2017/06/01
                                if (isExcelUpload==true)
                                {
                                    VREG_DEFN.Svrd_itm = itmcd;
                                    VREG_DEFN.Svrd_ser_1 = ser_no;
                                }
                                
                                // VREG_DEFN.Svrd_pay_tp = payMode;// cmbPayTP.SelectedValue.ToString();
                                //VREG_DEFN.Svrd_pb;
                                //VREG_DEFN.Svrd_pb_lvl;
                                VREG_DEFN.Svrd_pc = hier;
                                VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
                                //VREG_DEFN.Svrd_scheme = schm;
                                //VREG_DEFN.Svrd_seq;
                                //VREG_DEFN.Svrd_term = Convert.ToInt32(txtTerm.Text.Trim());
                                VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
                                VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
                                VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
                                VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
                                VREG_DEFN.SVRD_NO_OF_TIMES = Convert.ToInt32(txtNoofTime.Text);
                                VREG_DEFN.SVRD_NO_OF_USED = 0;
                                viewList.Add(VREG_DEFN);
                            }
                            else
                            {
                                for (int i = 0; i < list_PB_LVL.Count; i++)
                                {
                                    VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
                                    VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
                                    VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;//chkInvVal.Checked; //
                                    VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
                                    VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
                                    VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
                                    VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
                                    VREG_DEFN.Svrd_cre_dt = DateTime.Now;
                                    VREG_DEFN.Svrd_cust_tp = ddlCustTp.SelectedText.ToString();//txtCustTp.Text.Trim();
                                    VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
                                    VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
                                    VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
                                    VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
                                    VREG_DEFN.Svrd_itm = itm;
                                    //Tharanga 2017/06/01
                                    if (isExcelUpload == true)
                                    {
                                        VREG_DEFN.Svrd_itm = itmcd;
                                        VREG_DEFN.Svrd_ser_1 = ser_no;
                                    }
                                   
                                    //VREG_DEFN.Svrd_pay_tp = //cmbPayTP.SelectedValue.ToString();
                                    VREG_DEFN.Svrd_pb = list_PB[i].ToString();
                                    VREG_DEFN.Svrd_pb_lvl = list_PB_LVL[i].ToString();
                                    VREG_DEFN.Svrd_pc = hier;
                                    VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
                                    //VREG_DEFN.Svrd_scheme = schm;
                                    //VREG_DEFN.Svrd_seq;
                                    //VREG_DEFN.Svrd_term = Convert.ToInt32(txtTerm.Text.Trim());
                                    VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
                                    VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
                                    VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
                                    VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
                                    VREG_DEFN.SVRD_NO_OF_TIMES = Convert.ToInt32(txtNoofTime.Text);
                                    VREG_DEFN.SVRD_NO_OF_USED = 0;
                                    viewList.Add(VREG_DEFN);
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            foreach (string payMode in list_PayMode_list)
                            {
                                #region -- price books
                                if (list_PB_LVL.Count == 0)
                                {
                                    if (payMode == "CRCD")
                                    {
                                        if (termsList.Count == 0)
                                        {
                                            VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
                                            VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
                                            VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;// chkInvVal.Checked; //
                                            VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
                                            VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
                                            VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
                                            VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
                                            VREG_DEFN.Svrd_cre_dt = DateTime.Now;
                                            VREG_DEFN.Svrd_cust_tp = ddlCustTp.SelectedText.ToString();;//txtCustTp.Text.Trim();
                                            VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
                                            VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
                                            VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
                                            VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
                                            VREG_DEFN.Svrd_itm = itm;
                                            VREG_DEFN.Svrd_pay_tp = payMode;// cmbPayTP.SelectedValue.ToString();
                                            //VREG_DEFN.Svrd_pb;
                                            //VREG_DEFN.Svrd_pb_lvl;
                                            VREG_DEFN.Svrd_pc = hier;
                                            VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
                                            //VREG_DEFN.Svrd_scheme = schm;
                                            //VREG_DEFN.Svrd_seq;
                                            VREG_DEFN.Svrd_term = 0;//Convert.ToInt32(txtTerm.Text.Trim());
                                            VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
                                            VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
                                            VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
                                            VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
                                            VREG_DEFN.SVRD_NO_OF_TIMES = Convert.ToInt32(txtNoofTime.Text);
                                            VREG_DEFN.SVRD_NO_OF_USED = 0;
                                            viewList.Add(VREG_DEFN);
                                        }
                                        else
                                        {
                                            foreach (string term in termsList)
                                            {
                                                VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
                                                VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
                                                VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;// chkInvVal.Checked; //
                                                VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
                                                VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
                                                VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
                                                VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
                                                VREG_DEFN.Svrd_cre_dt = DateTime.Now;
                                                VREG_DEFN.Svrd_cust_tp = ddlCustTp.SelectedText.ToString();//txtCustTp.Text.Trim();
                                                VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
                                                VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
                                                VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
                                                VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
                                                VREG_DEFN.Svrd_itm = itm;
                                                VREG_DEFN.Svrd_pay_tp = payMode;// cmbPayTP.SelectedValue.ToString();
                                                //VREG_DEFN.Svrd_pb;
                                                //VREG_DEFN.Svrd_pb_lvl;
                                                VREG_DEFN.Svrd_pc = hier;
                                                VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
                                                //VREG_DEFN.Svrd_scheme = schm;
                                                //VREG_DEFN.Svrd_seq;
                                                VREG_DEFN.Svrd_term = Convert.ToInt32(term);
                                                VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
                                                VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
                                                VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
                                                VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
                                                VREG_DEFN.SVRD_NO_OF_TIMES = Convert.ToInt32(txtNoofTime.Text);
                                                VREG_DEFN.SVRD_NO_OF_USED = 0;
                                                viewList.Add(VREG_DEFN);
                                            }

                                        }

                                    }
                                    else
                                    {
                                        VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
                                        VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
                                        VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;// chkInvVal.Checked; //
                                        VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
                                        VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
                                        VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
                                        VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
                                        VREG_DEFN.Svrd_cre_dt = DateTime.Now;
                                        VREG_DEFN.Svrd_cust_tp = ddlCustTp.SelectedText.ToString(); //txtCustTp.Text.Trim();
                                        VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
                                        VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
                                        VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
                                        VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
                                        VREG_DEFN.Svrd_itm = itm;
                                        VREG_DEFN.Svrd_pay_tp = payMode;// cmbPayTP.SelectedValue.ToString();
                                        //VREG_DEFN.Svrd_pb;
                                        //VREG_DEFN.Svrd_pb_lvl;
                                        VREG_DEFN.Svrd_pc = hier;
                                        VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
                                        //VREG_DEFN.Svrd_scheme = schm;
                                        //VREG_DEFN.Svrd_seq;
                                        VREG_DEFN.Svrd_term = 0;//Convert.ToInt32(txtTerm.Text.Trim());
                                        VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
                                        VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
                                        VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
                                        VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
                                        VREG_DEFN.SVRD_NO_OF_TIMES = Convert.ToInt32(txtNoofTime.Text);
                                        VREG_DEFN.SVRD_NO_OF_USED = 0;
                                        viewList.Add(VREG_DEFN);
                                    }

                                }
                                else
                                {
                                    for (int i = 0; i < list_PB_LVL.Count; i++)
                                    {
                                        if (payMode == "CRCD")
                                        {
                                            if (termsList.Count == 0)
                                            {
                                                VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
                                                VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
                                                VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;//chkInvVal.Checked; //
                                                VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
                                                VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
                                                VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
                                                VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
                                                VREG_DEFN.Svrd_cre_dt = DateTime.Now;
                                                VREG_DEFN.Svrd_cust_tp = ddlCustTp.SelectedText.ToString();//txtCustTp.Text.Trim();
                                                VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
                                                VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
                                                VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
                                                VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
                                                VREG_DEFN.Svrd_itm = itm;
                                                VREG_DEFN.Svrd_pay_tp = payMode;//cmbPayTP.SelectedValue.ToString();
                                                VREG_DEFN.Svrd_pb = list_PB[i].ToString();
                                                VREG_DEFN.Svrd_pb_lvl = list_PB_LVL[i].ToString();
                                                VREG_DEFN.Svrd_pc = hier;
                                                VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
                                                //VREG_DEFN.Svrd_scheme = schm;
                                                //VREG_DEFN.Svrd_seq;
                                                VREG_DEFN.Svrd_term = 0; Convert.ToInt32(txtTerm.Text.Trim());
                                                VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
                                                VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
                                                VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
                                                VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
                                                VREG_DEFN.SVRD_NO_OF_TIMES = Convert.ToInt32(txtNoofTime.Text);
                                                VREG_DEFN.SVRD_NO_OF_USED = 0;
                                                viewList.Add(VREG_DEFN);
                                            }
                                            else
                                            {
                                                foreach (string term in termsList)
                                                {
                                                    VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
                                                    VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
                                                    VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;//chkInvVal.Checked; //
                                                    VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
                                                    VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
                                                    VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
                                                    VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
                                                    VREG_DEFN.Svrd_cre_dt = DateTime.Now;
                                                    VREG_DEFN.Svrd_cust_tp = ddlCustTp.SelectedText.ToString();//txtCustTp.Text.Trim();
                                                    VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
                                                    VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
                                                    VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
                                                    VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
                                                    VREG_DEFN.Svrd_itm = itm;
                                                    VREG_DEFN.Svrd_pay_tp = payMode;//cmbPayTP.SelectedValue.ToString();
                                                    VREG_DEFN.Svrd_pb = list_PB[i].ToString();
                                                    VREG_DEFN.Svrd_pb_lvl = list_PB_LVL[i].ToString();
                                                    VREG_DEFN.Svrd_pc = hier;
                                                    VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
                                                    //VREG_DEFN.Svrd_scheme = schm;
                                                    //VREG_DEFN.Svrd_seq;
                                                    VREG_DEFN.Svrd_term = Convert.ToInt32(term);// Convert.ToInt32(txtTerm.Text.Trim());
                                                    VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
                                                    VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
                                                    VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
                                                    VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
                                                    VREG_DEFN.SVRD_NO_OF_TIMES = Convert.ToInt32(txtNoofTime.Text);
                                                    VREG_DEFN.SVRD_NO_OF_USED = 0;
                                                    viewList.Add(VREG_DEFN);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
                                            VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
                                            VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;//chkInvVal.Checked; //
                                            VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
                                            VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
                                            VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
                                            VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
                                            VREG_DEFN.Svrd_cre_dt = DateTime.Now;
                                            VREG_DEFN.Svrd_cust_tp = ddlCustTp.SelectedText.ToString();// txtCustTp.Text.Trim();
                                            VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
                                            VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
                                            VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
                                            VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
                                            VREG_DEFN.Svrd_itm = itm;
                                            VREG_DEFN.Svrd_pay_tp = payMode;//cmbPayTP.SelectedValue.ToString();
                                            VREG_DEFN.Svrd_pb = list_PB[i].ToString();
                                            VREG_DEFN.Svrd_pb_lvl = list_PB_LVL[i].ToString();
                                            VREG_DEFN.Svrd_pc = hier;
                                            VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
                                            //VREG_DEFN.Svrd_scheme = schm;
                                            //VREG_DEFN.Svrd_seq;
                                            VREG_DEFN.Svrd_term = 0; Convert.ToInt32(txtTerm.Text.Trim());
                                            VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
                                            VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
                                            VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
                                            VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
                                            VREG_DEFN.SVRD_NO_OF_TIMES = Convert.ToInt32(txtNoofTime.Text);
                                            VREG_DEFN.SVRD_NO_OF_USED = 0;
                                            viewList.Add(VREG_DEFN);
                                        }

                                    }
                                }
                                #endregion
                            }

                        }



                    }
                    else //SCEMES COUNT>0
                    {
                        foreach (string schm in SCHEME_list)
                        {
                            if (list_PayMode_list.Count == 0)
                            {
                                #region -- price books
                                if (list_PB_LVL.Count == 0)
                                {
                                    VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
                                    VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
                                    VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;//chkInvVal.Checked; //
                                    VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
                                    VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
                                    VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
                                    VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
                                    VREG_DEFN.Svrd_cre_dt = DateTime.Now;
                                    VREG_DEFN.Svrd_cust_tp = ddlCustTp.SelectedText.ToString();//txtCustTp.Text.Trim();
                                    VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
                                    VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
                                    VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
                                    VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
                                    VREG_DEFN.Svrd_itm = itm;
                                    //VREG_DEFN.Svrd_pay_tp = payMode;//cmbPayTP.SelectedValue.ToString();
                                    //VREG_DEFN.Svrd_pb;
                                    //VREG_DEFN.Svrd_pb_lvl;
                                    VREG_DEFN.Svrd_pc = hier;
                                    VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
                                    VREG_DEFN.Svrd_scheme = schm;
                                    //VREG_DEFN.Svrd_seq;
                                    //VREG_DEFN.Svrd_term = Convert.ToInt32(txtTerm.Text.Trim());
                                    VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
                                    VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
                                    VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
                                    VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
                                    VREG_DEFN.SVRD_NO_OF_TIMES = Convert.ToInt32(txtNoofTime.Text);
                                    VREG_DEFN.SVRD_NO_OF_USED = 0;
                                    viewList.Add(VREG_DEFN);
                                }
                                else
                                {
                                    for (int i = 0; i < list_PB_LVL.Count; i++)
                                    {
                                        VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
                                        VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
                                        VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked; //chkInvVal.Checked; //
                                        VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
                                        VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
                                        VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
                                        VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
                                        VREG_DEFN.Svrd_cre_dt = DateTime.Now;
                                        VREG_DEFN.Svrd_cust_tp = ddlCustTp.SelectedText.ToString();//txtCustTp.Text.Trim();
                                        VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
                                        VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
                                        VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
                                        VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
                                        VREG_DEFN.Svrd_itm = itm;
                                        //VREG_DEFN.Svrd_pay_tp = payMode;//cmbPayTP.SelectedValue.ToString();
                                        VREG_DEFN.Svrd_pb = list_PB[i].ToString();
                                        VREG_DEFN.Svrd_pb_lvl = list_PB_LVL[i].ToString();
                                        VREG_DEFN.Svrd_pc = hier;
                                        VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
                                        VREG_DEFN.Svrd_scheme = schm;
                                        //VREG_DEFN.Svrd_seq;
                                        //VREG_DEFN.Svrd_term = Convert.ToInt32(txtTerm.Text.Trim());
                                        VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
                                        VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
                                        VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
                                        VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
                                        VREG_DEFN.SVRD_NO_OF_TIMES = Convert.ToInt32(txtNoofTime.Text);
                                        VREG_DEFN.SVRD_NO_OF_USED = 0;
                                        viewList.Add(VREG_DEFN);
                                    }
                                }
                                #endregion

                            }
                            else
                            {
                                foreach (string payMode in list_PayMode_list)
                                {
                                    #region -- price books
                                    if (list_PB_LVL.Count == 0)
                                    {
                                        VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
                                        VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
                                        VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked; //chkInvVal.Checked; //
                                        VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
                                        VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
                                        VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
                                        VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
                                        VREG_DEFN.Svrd_cre_dt = DateTime.Now;
                                        VREG_DEFN.Svrd_cust_tp = ddlCustTp.SelectedText.ToString();//txtCustTp.Text.Trim();
                                        VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
                                        VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
                                        VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
                                        VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
                                        VREG_DEFN.Svrd_itm = itm;
                                        VREG_DEFN.Svrd_pay_tp = payMode;//cmbPayTP.SelectedValue.ToString();
                                        //VREG_DEFN.Svrd_pb;
                                        //VREG_DEFN.Svrd_pb_lvl;
                                        VREG_DEFN.Svrd_pc = hier;
                                        VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
                                        VREG_DEFN.Svrd_scheme = schm;
                                        //VREG_DEFN.Svrd_seq;
                                        VREG_DEFN.Svrd_term = Convert.ToInt32(txtTerm.Text.Trim());
                                        VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
                                        VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
                                        VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
                                        VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
                                        VREG_DEFN.SVRD_NO_OF_TIMES = Convert.ToInt32(txtNoofTime.Text);
                                        VREG_DEFN.SVRD_NO_OF_USED = 0;
                                        viewList.Add(VREG_DEFN);
                                    }
                                    else
                                    {
                                        for (int i = 0; i < list_PB_LVL.Count; i++)
                                        {
                                            VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
                                            VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
                                            VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;//chkInvVal.Checked; //
                                            VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
                                            VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
                                            VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
                                            VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
                                            VREG_DEFN.Svrd_cre_dt = DateTime.Now;
                                            VREG_DEFN.Svrd_cust_tp = ddlCustTp.SelectedText.ToString();//txtCustTp.Text.Trim();
                                            VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
                                            VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
                                            VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
                                            VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
                                            VREG_DEFN.Svrd_itm = itm;
                                            VREG_DEFN.Svrd_pay_tp = payMode;//cmbPayTP.SelectedValue.ToString();
                                            VREG_DEFN.Svrd_pb = list_PB[i].ToString();
                                            VREG_DEFN.Svrd_pb_lvl = list_PB_LVL[i].ToString();
                                            VREG_DEFN.Svrd_pc = hier;
                                            VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
                                            VREG_DEFN.Svrd_scheme = schm;
                                            //VREG_DEFN.Svrd_seq;
                                            VREG_DEFN.Svrd_term = Convert.ToInt32(txtTerm.Text.Trim());
                                            VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
                                            VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
                                            VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
                                            VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
                                            VREG_DEFN.SVRD_NO_OF_TIMES = Convert.ToInt32(txtNoofTime.Text);
                                            VREG_DEFN.SVRD_NO_OF_USED = 0;
                                            viewList.Add(VREG_DEFN);
                                        }
                                    }
                                    #endregion
                                }


                            }
                        }//-----

                    }

                }
            }

            //-----------------------------------------------------------------------------------
            #endregion--------------------------------------END OF NEW SAVE-------------------------------------------------------------------------

            #region

           
            //foreach (string hier in PC_list)
            //{
            //    foreach (string itm in ITEMS_list)
            //    {
            //        if (SCHEME_list.Count == 0)
            //        {
            //            if (list_PayMode_list.Count == 0)
            //            {
            //                #region -- price books
            //                if (list_PB_LVL.Count == 0)
            //                {
            //                    VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
            //                    VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
            //                    VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;//chkInvVal.Checked; //
            //                    VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
            //                    VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
            //                    VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
            //                    VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
            //                    VREG_DEFN.Svrd_cre_dt = DateTime.Now;
            //                    VREG_DEFN.Svrd_cust_tp = txtCustTp.Text.Trim();
            //                    VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
            //                    VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
            //                    VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
            //                    VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
            //                    VREG_DEFN.Svrd_itm = itm;
            //                    // VREG_DEFN.Svrd_pay_tp = payMode;// cmbPayTP.SelectedValue.ToString();
            //                    //VREG_DEFN.Svrd_pb;
            //                    //VREG_DEFN.Svrd_pb_lvl;
            //                    VREG_DEFN.Svrd_pc = hier;
            //                    VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
            //                    //VREG_DEFN.Svrd_scheme = schm;
            //                    //VREG_DEFN.Svrd_seq;
            //                    //VREG_DEFN.Svrd_term = Convert.ToInt32(txtTerm.Text.Trim());
            //                    VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
            //                    VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
            //                    VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
            //                    VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
            //                    viewList.Add(VREG_DEFN);
            //                }
            //                else
            //                {
            //                    for (int i = 0; i < list_PB_LVL.Count; i++)
            //                    {
            //                        VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
            //                        VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
            //                        VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;//chkInvVal.Checked; //
            //                        VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
            //                        VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
            //                        VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
            //                        VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
            //                        VREG_DEFN.Svrd_cre_dt = DateTime.Now;
            //                        VREG_DEFN.Svrd_cust_tp = txtCustTp.Text.Trim();
            //                        VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
            //                        VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
            //                        VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
            //                        VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
            //                        VREG_DEFN.Svrd_itm = itm;
            //                        //VREG_DEFN.Svrd_pay_tp = //cmbPayTP.SelectedValue.ToString();
            //                        VREG_DEFN.Svrd_pb = list_PB[i].ToString();
            //                        VREG_DEFN.Svrd_pb_lvl = list_PB_LVL[i].ToString();
            //                        VREG_DEFN.Svrd_pc = hier;
            //                        VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
            //                        //VREG_DEFN.Svrd_scheme = schm;
            //                        //VREG_DEFN.Svrd_seq;
            //                        //VREG_DEFN.Svrd_term = Convert.ToInt32(txtTerm.Text.Trim());
            //                        VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
            //                        VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
            //                        VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
            //                        VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
            //                        viewList.Add(VREG_DEFN);
            //                    }
            //                }
            //                #endregion
            //            }
            //            else
            //            {
            //                foreach (string payMode in list_PayMode_list)
            //                {                               
            //                    #region -- price books
            //                    if (list_PB_LVL.Count == 0)
            //                    {
            //                        VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
            //                        VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
            //                        VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;//chkInvVal.Checked; //
            //                        VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
            //                        VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
            //                        VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
            //                        VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
            //                        VREG_DEFN.Svrd_cre_dt = DateTime.Now;
            //                        VREG_DEFN.Svrd_cust_tp = txtCustTp.Text.Trim();
            //                        VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
            //                        VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
            //                        VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
            //                        VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
            //                        VREG_DEFN.Svrd_itm = itm;
            //                        VREG_DEFN.Svrd_pay_tp = payMode;// cmbPayTP.SelectedValue.ToString();
            //                        //VREG_DEFN.Svrd_pb;
            //                        //VREG_DEFN.Svrd_pb_lvl;
            //                        VREG_DEFN.Svrd_pc = hier;
            //                        VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
            //                        //VREG_DEFN.Svrd_scheme = schm;
            //                        //VREG_DEFN.Svrd_seq;
            //                        VREG_DEFN.Svrd_term = Convert.ToInt32(txtTerm.Text.Trim());
            //                        VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
            //                        VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
            //                        VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
            //                        VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
            //                        viewList.Add(VREG_DEFN);
            //                    }
            //                    else
            //                    {
            //                        for (int i = 0; i < list_PB_LVL.Count; i++)
            //                        {
            //                            VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
            //                            VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
            //                            VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;//chkInvVal.Checked; //
            //                            VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
            //                            VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
            //                            VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
            //                            VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
            //                            VREG_DEFN.Svrd_cre_dt = DateTime.Now;
            //                            VREG_DEFN.Svrd_cust_tp = txtCustTp.Text.Trim();
            //                            VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
            //                            VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
            //                            VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
            //                            VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
            //                            VREG_DEFN.Svrd_itm = itm;
            //                            VREG_DEFN.Svrd_pay_tp = payMode;//cmbPayTP.SelectedValue.ToString();
            //                            VREG_DEFN.Svrd_pb = list_PB[i].ToString();
            //                            VREG_DEFN.Svrd_pb_lvl = list_PB_LVL[i].ToString();
            //                            VREG_DEFN.Svrd_pc = hier;
            //                            VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
            //                            //VREG_DEFN.Svrd_scheme = schm;
            //                            //VREG_DEFN.Svrd_seq;
            //                            VREG_DEFN.Svrd_term = Convert.ToInt32(txtTerm.Text.Trim());
            //                            VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
            //                            VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
            //                            VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
            //                            VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
            //                            viewList.Add(VREG_DEFN);
            //                        }
            //                    }
            //                    #endregion
            //                }

            //            }



            //        }
            //        else //SCEMES COUNT>0
            //        {
            //            foreach (string schm in SCHEME_list)
            //            {
            //                if (list_PayMode_list.Count == 0)
            //                {
            //                    #region -- price books
            //                    if (list_PB_LVL.Count == 0)
            //                    {
            //                        VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
            //                        VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
            //                        VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;//chkInvVal.Checked; //
            //                        VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
            //                        VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
            //                        VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
            //                        VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
            //                        VREG_DEFN.Svrd_cre_dt = DateTime.Now;
            //                        VREG_DEFN.Svrd_cust_tp = txtCustTp.Text.Trim();
            //                        VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
            //                        VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
            //                        VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
            //                        VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
            //                        VREG_DEFN.Svrd_itm = itm;
            //                        //VREG_DEFN.Svrd_pay_tp = payMode;//cmbPayTP.SelectedValue.ToString();
            //                        //VREG_DEFN.Svrd_pb;
            //                        //VREG_DEFN.Svrd_pb_lvl;
            //                        VREG_DEFN.Svrd_pc = hier;
            //                        VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
            //                        VREG_DEFN.Svrd_scheme = schm;
            //                        //VREG_DEFN.Svrd_seq;
            //                        //VREG_DEFN.Svrd_term = Convert.ToInt32(txtTerm.Text.Trim());
            //                        VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
            //                        VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
            //                        VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
            //                        VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
            //                        viewList.Add(VREG_DEFN);
            //                    }
            //                    else
            //                    {
            //                        for (int i = 0; i < list_PB_LVL.Count; i++)
            //                        {
            //                            VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
            //                            VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
            //                            VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;//chkInvVal.Checked; //
            //                            VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
            //                            VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
            //                            VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
            //                            VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
            //                            VREG_DEFN.Svrd_cre_dt = DateTime.Now;
            //                            VREG_DEFN.Svrd_cust_tp = txtCustTp.Text.Trim();
            //                            VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
            //                            VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
            //                            VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
            //                            VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
            //                            VREG_DEFN.Svrd_itm = itm;
            //                            //VREG_DEFN.Svrd_pay_tp = payMode;//cmbPayTP.SelectedValue.ToString();
            //                            VREG_DEFN.Svrd_pb = list_PB[i].ToString();
            //                            VREG_DEFN.Svrd_pb_lvl = list_PB_LVL[i].ToString();
            //                            VREG_DEFN.Svrd_pc = hier;
            //                            VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
            //                            VREG_DEFN.Svrd_scheme = schm;
            //                            //VREG_DEFN.Svrd_seq;
            //                            //VREG_DEFN.Svrd_term = Convert.ToInt32(txtTerm.Text.Trim());
            //                            VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
            //                            VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
            //                            VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
            //                            VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
            //                            viewList.Add(VREG_DEFN);
            //                        }
            //                    }
            //                    #endregion

            //                }
            //                else
            //                {
            //                    foreach (string payMode in list_PayMode_list)
            //                    {
            //                        #region -- price books
            //                        if (list_PB_LVL.Count == 0)
            //                        {
            //                            VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
            //                            VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
            //                            VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;//chkInvVal.Checked; //
            //                            VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
            //                            VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
            //                            VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
            //                            VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
            //                            VREG_DEFN.Svrd_cre_dt = DateTime.Now;
            //                            VREG_DEFN.Svrd_cust_tp = txtCustTp.Text.Trim();
            //                            VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
            //                            VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
            //                            VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
            //                            VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
            //                            VREG_DEFN.Svrd_itm = itm;
            //                            VREG_DEFN.Svrd_pay_tp = payMode;//cmbPayTP.SelectedValue.ToString();
            //                            //VREG_DEFN.Svrd_pb;
            //                            //VREG_DEFN.Svrd_pb_lvl;
            //                            VREG_DEFN.Svrd_pc = hier;
            //                            VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
            //                            VREG_DEFN.Svrd_scheme = schm;
            //                            //VREG_DEFN.Svrd_seq;
            //                            VREG_DEFN.Svrd_term = Convert.ToInt32(txtTerm.Text.Trim());
            //                            VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
            //                            VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
            //                            VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
            //                            VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
            //                            viewList.Add(VREG_DEFN);
            //                        }
            //                        else
            //                        {
            //                            for (int i = 0; i < list_PB_LVL.Count; i++)
            //                            {
            //                                VehicalRegistrationDefnition VREG_DEFN = new VehicalRegistrationDefnition();
            //                                VREG_DEFN.Svrd_alw_mult_pay = chkAllowMultiPayModes.Checked;
            //                                VREG_DEFN.Svrd_check_on = rdoChekOnInv.Checked;//chkInvVal.Checked; //
            //                                VREG_DEFN.Svrd_circular = txtCircularNo.Text.Trim().ToUpper();
            //                                VREG_DEFN.Svrd_claim_val = Convert.ToDecimal(TextBoxCvalue.Text);
            //                                VREG_DEFN.Svrd_com = DropDownListPartyTypes.SelectedValue.ToString(); //???
            //                                VREG_DEFN.Svrd_cre_by = BaseCls.GlbUserID; ;
            //                                VREG_DEFN.Svrd_cre_dt = DateTime.Now;
            //                                VREG_DEFN.Svrd_cust_tp = txtCustTp.Text.Trim();
            //                                VREG_DEFN.Svrd_from_dt = Convert.ToDateTime(TextBoxFrom.Text).Date;
            //                                VREG_DEFN.Svrd_from_qty = Convert.ToInt32(txtFromQty.Text.Trim());
            //                                VREG_DEFN.Svrd_from_val = Convert.ToDecimal(txtInvFromVal.Text.Trim());
            //                                VREG_DEFN.Svrd_is_req = CheckBoxMandatory.Checked;
            //                                VREG_DEFN.Svrd_itm = itm;
            //                                VREG_DEFN.Svrd_pay_tp = payMode;//cmbPayTP.SelectedValue.ToString();
            //                                VREG_DEFN.Svrd_pb = list_PB[i].ToString();
            //                                VREG_DEFN.Svrd_pb_lvl = list_PB_LVL[i].ToString();
            //                                VREG_DEFN.Svrd_pc = hier;
            //                                VREG_DEFN.Svrd_sale_tp = DropDownListSType.SelectedValue.ToString();
            //                                VREG_DEFN.Svrd_scheme = schm;
            //                                //VREG_DEFN.Svrd_seq;
            //                                VREG_DEFN.Svrd_term = Convert.ToInt32(txtTerm.Text.Trim());
            //                                VREG_DEFN.Svrd_to_dt = Convert.ToDateTime(TextBoxTo.Text).Date;
            //                                VREG_DEFN.Svrd_to_qty = Convert.ToInt32(txtToQty.Text.Trim());
            //                                VREG_DEFN.Svrd_to_val = Convert.ToDecimal(txtInvToVal.Text.Trim()); ;
            //                                VREG_DEFN.Svrd_val = Convert.ToDecimal(TextBoxRvalue.Text.Trim());
            //                                viewList.Add(VREG_DEFN);
            //                            }
            //                        }
            //                        #endregion
            //                    }


            //                }
            //            }//-----

            //        }

            //    }
            //}
            #endregion
            //-----------------------------------------------------------------------------------
            if (MessageBox.Show("Are you sure to save ?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            if (isExcelUpload==true)
            {
                
            }
            Int32 eff = CHNLSVC.General.SaveVehicalRegistrationDefinition_NEW(viewList);
            isExcelUpload = false;
            if (eff > 0)
            {
                MessageBox.Show("Successfully Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Not Saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DropDownListPriceBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnClearPB_Click(null, null);
            this.btnAddPB_Click(null, null);
        }

        private void btnClearPB_Click(object sender, EventArgs e)
        {
            checkBox_PB.Checked = false;
            grvPriceLevel.DataSource = null;
            grvPriceLevel.AutoGenerateColumns = false;
        }

        private void btnAll_pb_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvPriceLevel.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvPriceLevel.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnNon_pb_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvPriceLevel.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvPriceLevel.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnAddPB_Click(object sender, EventArgs e)
        {
            if (CheckBoxPriceBookAll.Checked == true)
            {
                List<PriceBookLevelRef> _PbLevel_All = new List<PriceBookLevelRef>();
                List<PriceBookRef> _priceBook = CHNLSVC.Sales.GetPriceBooklist(BaseCls.GlbUserComCode);
                foreach (PriceBookRef pb in _priceBook)
                {
                    List<PriceBookLevelRef> _PbLevel = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, pb.Sapb_pb, null);
                    //--------------------------------------------------------------------------------------------
                    foreach (PriceBookLevelRef pbl in _PbLevel)
                    {
                        var _duplicate = from _dup in _PbLevel_All
                                         where _dup.Sapl_pb == pb.Sapb_pb && _dup.Sapl_pb_lvl_cd == pbl.Sapl_pb_lvl_cd
                                         select _dup;
                        if (_duplicate.Count() == 0)
                        {
                            //addList.Add(obj);
                            _PbLevel_All.Add(pbl);
                        }
                    }
                    // _PbLevel_All.AddRange(_PbLevel);
                    //--------------------------------------------------------------------------------------------
                    grvPriceLevel.DataSource = null;
                    grvPriceLevel.AutoGenerateColumns = false;
                    //grvPriceLevel.DataSource = _PbLevel;
                }
                grvPriceLevel.DataSource = _PbLevel_All;
            }
            else
            {
                List<PriceBookLevelRef> _PbLevel = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, DropDownListPriceBook.SelectedValue.ToString(), null);
                //--------------------------------------------------------------------------------------------
                List<PriceBookLevelRef> _PbLevel_All = new List<PriceBookLevelRef>();
                foreach (PriceBookLevelRef pbl in _PbLevel)
                {
                    var _duplicate = from _dup in _PbLevel_All
                                     where _dup.Sapl_pb == DropDownListPriceBook.SelectedValue.ToString() && _dup.Sapl_pb_lvl_cd == pbl.Sapl_pb_lvl_cd
                                     select _dup;
                    if (_duplicate.Count() == 0)
                    {
                        //addList.Add(obj);
                        _PbLevel_All.Add(pbl);
                    }
                }
                // _PbLevel_All.AddRange(_PbLevel);
                //--------------------------------------------------------------------------------------------
                grvPriceLevel.DataSource = null;
                grvPriceLevel.AutoGenerateColumns = false;
                grvPriceLevel.DataSource = _PbLevel_All;//_PbLevel;
            }
        }

        private void chkAllowBaseOnPayMode_CheckedChanged(object sender, EventArgs e)
        {
            if (DropDownListSType.SelectedIndex==-1)
            {
                Panel_payTp.Enabled = false;
                return;
            }
            Panel_payTp.Enabled = false;
            if (DropDownListSType.SelectedValue.ToString() == "CS")//CASH SALES
            {
                if (chkAllowBaseOnPayMode.Checked == true)
                {
                    Panel_payTp.Enabled = true;
                }
                else
                {
                    Panel_payTp.Enabled = false;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnAddTerm_Click(object sender, EventArgs e)
        {
            try
            {
                //termsList.Add(Convert.ToInt32(txtTerm.Text.Trim()));
                termsList.Add(txtTerm.Text.Trim());
            }
            catch(Exception ex){
                MessageBox.Show("Invalid term","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            termList term = new termList();
            term.Term = txtTerm.Text.Trim();
            TermList.Add(term);
            grvTerms.DataSource = null;
            grvTerms.DataSource = TermList;

          
        }

        private void btnPT_grid_Click(object sender, EventArgs e)
        {
            PayType = new List<PaymentTypeRef>();
            grvPayType.AutoGenerateColumns = false;
            BindingSource _source = new BindingSource();
            _source.DataSource = PayType;
            grvPayType.DataSource = _source;
            pnlBank.Visible = false;
            pnlLoyalty.Visible = false;
        }
        private void visible_EditPanel()
        {           
            grvEdit.DataSource = null;
            grvEdit.AutoGenerateColumns = false;
            
            txtEditCircular.Text = "";
            dtEditNewFrom.Value = DateTime.Now.Date;
            dtEditNewTo.Value = DateTime.Now.Date;
            dtEditCurrFrom.Value = DateTime.Now.Date;
            dtEditCurrFrom.Value = DateTime.Now.Date;

            panel_edit.Location = new Point(-1, 26);
            panel_edit.Size = new System.Drawing.Size(1196, 614);
            panel_edit.Visible = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(txtEditCircular.Text.Trim()=="")
            {
                return;
            }
            //-------------------------------------------------------------------------------------
            List<VehicalRegistrationDefnition> DEFN = CHNLSVC.General.Get_vehRegDefnByCircular(txtEditCircular.Text.Trim());
            if (DEFN == null)
            {
                MessageBox.Show("Invalid Circular code.", "Cirular", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (DEFN.Count==0)
            {
                MessageBox.Show("Invalid Circular code.", "Cirular", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (DEFN !=null)
            {
                if (DEFN.Count>0)
                {
                    DateTime FROM = DEFN[0].Svrd_from_dt;
                    DateTime TO = DEFN[0].Svrd_to_dt;

                    dtEditCurrFrom.Value = FROM.Date;
                    dtEditCurrTo.Value = TO.Date;
                }
            }
            //----------------------------------------------------------------
            DateTime today = CHNLSVC.Security.GetServerDateTime().Date;
            DateTime currFrom = dtEditCurrFrom.Value.Date;
            DateTime currTo = dtEditCurrTo.Value.Date;

            //if (currFrom <= today || currTo <= today)
            //{
            //    MessageBox.Show("Cannot update.\n(Current From and To dates should be greater than today)!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //------------------------------------------------------------------------------------------
            DateTime newFrom = dtEditNewFrom.Value.Date;
            DateTime newTo = dtEditNewTo.Value.Date;
            
            if (newFrom < today || newTo < today)
            {
                MessageBox.Show("Cannot update.\n(From and To dates cannot be less than today!)!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Are you sure to save ?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            Int32 eff = CHNLSVC.General.Update_Veh_reg_defn(txtEditCircular.Text.Trim(), newFrom, newTo);
            if (eff > 0)
            {
                MessageBox.Show("Successfully updated!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.btnEdit_Click(null, null);
                return;
            }
            else
            {
                MessageBox.Show("Not updated!", "Update", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            visible_EditPanel();
        }

        private void btnGetCircular_Click(object sender, EventArgs e)
        {
            List<VehicalRegistrationDefnition> DEFN = CHNLSVC.General.Get_vehRegDefnByCircular(txtEditCircular.Text.Trim());
            grvEdit.DataSource = null;
            grvEdit.DataSource = DEFN;

            if (DEFN !=null)
            {
                if (DEFN.Count>0)
                {
                    DateTime FROM = DEFN[0].Svrd_from_dt;
                    DateTime TO = DEFN[0].Svrd_to_dt;

                    dtEditCurrFrom.Value = FROM.Date;
                    dtEditCurrTo.Value = TO.Date;
                }
            }

            //grvEdit.AutoGenerateColumns = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            panel_edit.Visible = false;
        }

        private void txtEditCircular_TextChanged(object sender, EventArgs e)
        {         
            dtEditNewFrom.Value = DateTime.Now.Date;
            dtEditNewTo.Value = DateTime.Now.Date;
            dtEditCurrFrom.Value = DateTime.Now.Date;
            dtEditCurrFrom.Value = DateTime.Now.Date;

            grvEdit.DataSource = null;
            //grvEdit.AutoGenerateColumns = false;
        }

        private void checkBox_HIERCHY_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_HIERCHY.Checked == true)
            {
                this.btnAll_Hirchy_Click(null, null);
            }
            else
            {
                this.btnNon_Hierachy_Click(null, null);
            }
        }

        private void checkBox_SCEME_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_SCEME.Checked == true)
            {
                this.btnAll_Schemes_Click(null, null);
            }
            else
            {
                this.btnNon_Schemes_Click(null, null);
            }
        }

        private void TextBoxRvalue_Leave(object sender, EventArgs e)
        {
            if (Regex.IsMatch(TextBoxRvalue.Text.Trim(), "[A-Z]+"))// if (!char.IsDigit(e.KeyChar))//|| e.KeyChar.ToString()!="."
            {
                TextBoxRvalue.Text = "";
                MessageBox.Show("Enter valid value", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //  txtBankSelling.Text = "";
                TextBoxRvalue.Focus();
            } 
        }

        private void TextBoxCvalue_Leave(object sender, EventArgs e)
        {
            if (Regex.IsMatch(TextBoxCvalue.Text.Trim(), "[A-Z]+"))// if (!char.IsDigit(e.KeyChar))//|| e.KeyChar.ToString()!="."
            {
                TextBoxCvalue.Text = "";
                MessageBox.Show("Enter valid value", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //  txtBankSelling.Text = "";
                TextBoxCvalue.Focus();
            } 
        }

        private void txtFromQty_Leave(object sender, EventArgs e)
        {
            if (Regex.IsMatch(txtFromQty.Text.Trim(), "[A-Z]+"))// if (!char.IsDigit(e.KeyChar))//|| e.KeyChar.ToString()!="."
            {
                txtFromQty.Text = "";
                MessageBox.Show("Enter valid value", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //  txtBankSelling.Text = "";
                txtFromQty.Focus();
            } 
        }

        private void txtToQty_Leave(object sender, EventArgs e)
        {
            if (Regex.IsMatch(txtToQty.Text.Trim(), "[A-Z]+"))// if (!char.IsDigit(e.KeyChar))//|| e.KeyChar.ToString()!="."
            {
                txtToQty.Text = "";
                MessageBox.Show("Enter valid value", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //  txtBankSelling.Text = "";
                txtToQty.Focus();
            } 
        }

        private void txtInvFromVal_Leave(object sender, EventArgs e)
        {
            if (Regex.IsMatch(txtInvFromVal.Text.Trim(), "[A-Z]+"))// if (!char.IsDigit(e.KeyChar))//|| e.KeyChar.ToString()!="."
            {
                txtInvFromVal.Text = "";
                MessageBox.Show("Enter valid value", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //  txtBankSelling.Text = "";
                txtInvFromVal.Focus();
            }
        }

        private void txtInvToVal_Leave(object sender, EventArgs e)
        {
            if (Regex.IsMatch(txtInvToVal.Text.Trim(), "[A-Z]+"))// if (!char.IsDigit(e.KeyChar))//|| e.KeyChar.ToString()!="."
            {
                txtInvToVal.Text = "";
                MessageBox.Show("Enter valid value", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //  txtBankSelling.Text = "";
                txtInvToVal.Focus();
            }
        }

        private void btnBrItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "Excel files(*.xls)|*.xls,*.xlsx|All files(*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.ShowDialog();
            txtUploadItems.Text = openFileDialog1.FileName;
        }

        private void btnUploadItem_Click(object sender, EventArgs e)
        {
            string _msg = string.Empty;
            if (string.IsNullOrEmpty(txtUploadItems.Text))
            {
                MessageBox.Show("Please select upload file path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUploadItems.Text = "";
                txtUploadItems.Focus();
                return;
            }



            System.IO.FileInfo fileObj = new System.IO.FileInfo(txtUploadItems.Text);

            if (fileObj.Exists == false)
            {
                MessageBox.Show("Selected file does not exist at the following path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUploadItems.Focus();
            }

            string Extension = fileObj.Extension;

            string conStr = "";

            if (Extension.ToUpper() == ".XLS")
            {

                conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"]
                         .ConnectionString;
            }
            else if (Extension.ToUpper() == ".XLSX")
            {
                conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"]
                          .ConnectionString;

            }


            conStr = String.Format(conStr, txtUploadItems.Text, "NO");
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();
            select_ITEMS_List = new DataTable();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow _dr in dt.Rows)
                {


                    DataTable _item = CHNLSVC.Sales.GetItemCode(BaseCls.GlbUserComCode, _dr[0].ToString());
                    if (_item == null || _item.Rows.Count <= 0)
                    {
                        MessageBox.Show("Invalid Item - " + _dr[0].ToString(), "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    select_ITEMS_List.Merge(_item);
                }
            }
            GridAll_Items.AutoGenerateColumns = false;
            GridAll_Items.DataSource = select_ITEMS_List;
        }

        private void btnExcelupload_Click(object sender, EventArgs e)
        {
            txtupload.Text = string.Empty;
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.ShowDialog();
            string[] _obj = openFileDialog1.FileName.Split('\\');
            txtupload.Text = openFileDialog1.FileName;
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            if (chkexle.Checked)
            {
                DropDownListPartyTypes.Enabled = false;
                DropDownListPartyTypes.SelectedValue = "PC";
                isExcelUpload = true;
                ExcelUploadProcess();
            }
           
        }

        //Tharanga 2017 may 26
        #region Excle Upload
        private void ExcelUploadProcess()
        {
          
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
             
           
            //validation
            try
            {
                System.IO.FileInfo fileObj = new System.IO.FileInfo(txtupload.Text);
                string Extension = fileObj.Extension;

                string conStr = "";

                if (Extension.ToUpper() == ".XLS")
                {

                    conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"]
                             .ConnectionString;
                }
                else if (Extension.ToUpper() == ".XLSX")
                {
                    conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"]
                              .ConnectionString;

                }
                else
                {
                    MessageBox.Show("Invalid upload file Format", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;

                }


                string _excelConnectionString = ConfigurationManager.ConnectionStrings[Extension == ".XLS" ? "ConStringExcel03" : "ConStringExcel07"].ConnectionString;

                _excelConnectionString = String.Format(conStr, txtupload.Text, "YES");
                OleDbConnection connExcel = new OleDbConnection(_excelConnectionString);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable _dt = new DataTable();
                cmdExcel.Connection = connExcel;

                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "] ";
                oda.SelectCommand = cmdExcel;
                oda.Fill(_dt);
                connExcel.Close();
                if (_dt == null || _dt.Rows.Count <= 0)
                {
                    MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            
               // DataTable oprofitCenter = new DataTable();
              //  DataGridView grvParty = new DataGridView();
                if (_dt.Rows.Count > 0)
                {
                    DataTable _temitem = new DataTable();
                    DataTable _dtTemp = new DataTable();

                    _dtTemp.Columns.Add("Code");
                    _dtTemp.Columns.Add("Description");

                    _temitem.Columns.Add("mi_cd");
                    _temitem.Columns.Add("mi_shortdesc");
                    _temitem.Columns.Add("ser_no");

                    List<MasterProfitCenter> _listpc = new List<MasterProfitCenter>();
                    List<string> ItemList = new List<string>();
                    
                    foreach (DataRow _row in _dt.Rows)
                    {
                        
                       
                        MasterProfitCenter _mstPc = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, _row["Profit Center"].ToString());

                        if (_mstPc == null )
                         {
                             if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("and invalid Service Center Code - " + _row[0].ToString());
                             else _errorLst.Append(" and invalid Service Center Code  - " + _row[0].ToString());
                             return;
                         }

                        DataTable _tblItem = CHNLSVC.Inventory.Get_Item_Infor(_row["Item Code"].ToString());
                         if (_tblItem.Rows.Count < 1)
                         {
                             if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("and invalid Service Center Code - " + _row[1].ToString());
                             else _errorLst.Append(" and invalid Service Center Code  - " + _row[1].ToString());
                             return;
                         }
                         DataRow _newDr = _dtTemp.NewRow();
                         _newDr["CODE"] = _mstPc.Mpc_cd;
                         _newDr["Description"] = _mstPc.Mpc_desc;
                         _dtTemp.Rows.Add(_newDr);


                         DataRow _newDrItem = _temitem.NewRow();
                         _newDrItem["mi_cd"] = _tblItem.Rows[0]["mi_cd"].ToString();
                         _newDrItem["mi_shortdesc"] = _tblItem.Rows[0]["mi_shortdesc"].ToString();
                         _newDrItem["ser_no"] = _row["ser no"].ToString();
                         _temitem.Rows.Add(_newDrItem);



                      }
                                     
                    grvParty.DataSource = _dtTemp;
                    GridAll_Items.DataSource = _temitem;
                    
                    
                  
                }
                CashPromotionDiscountDetail _detail = new CashPromotionDiscountDetail();
                CashPromotionDiscountHeader _discount = new CashPromotionDiscountHeader();
                int effect = 0;







            }
            //  MessageBox.Show("Header -" + hdrCou+"\n"+"Detail "+detCou+"\n"+"Item"+itmCou+"\n"+"Location"+locCou);
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }
        #endregion

        private void VehicalRegistrationDefinition_Load(object sender, EventArgs e)
        {

        }

        private void btnSer_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemSerNo);

            DataTable _result = CHNLSVC.CommonSearch.GetItemSerNo(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtserNo;
            _CommonSearch.ShowDialog();
            txtserNo.Focus();
        }

        private void label34_Leave(object sender, EventArgs e)
        {

        }

        private void txtserNo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtserNo.Text))
            {
                MessageBox.Show("Please Enter Serial Number", "Serial Number", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemSerNo);

            DataTable _result = CHNLSVC.Inventory.checkIntser(BaseCls.GlbUserComCode, TextBoxItem.Text.Trim(), txtserNo.Text.Trim());
            if (_result.Rows.Count<=0)
            {
                MessageBox.Show("Please Enter Valid Serial Number", "Serial Number", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 txtserNo.Text = "";
               return;
            }
        }

        private void txtNoofTime_Leave(object sender, EventArgs e)
        {
            if (txtNoofTime.TextLength > 4) // Dilanda Rqst
            {
                 MessageBox.Show("No of times length cannot grater than 4 digit", "Serial Number", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 txtNoofTime.Text = "";
                 txtNoofTime.Focus();
                 return;
            }
        }
    }

    public class schemeList
    {
        string schemeCode;

        public string SchemeCode
        {
            get { return schemeCode; }
            set { schemeCode = value; }
        }
    }
    public class termList
    {
        string term;
        public string Term
        {
            get { return term; }
            set { term = value; }
        }
    }
}
