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

namespace FF.WindowsERPClient.General
{
    public partial class VehicleInsuDefinitionNew : Base
    {
        /*
         * This codeing is dulicated from VehicleInsuDefinition due to several changes.
         * Originally written by Sachith @ web and transfer by shani to windows
         * Modified area. 
         * Get the snipperts from Commision definition - instructed by Dilanda
         *  1. Business Hierarchy
         *  2. Item Detail (Brand/Model/Main Category etc.)
         *  
         *  No Changes for the Save logics and other function
         *  
         */
        DataTable select_PC_List = new DataTable();
        DataTable select_ITEMS_List = new DataTable();
        Int32 hiddenFieldRowCount;
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
        public VehicleInsuDefinitionNew()
        {
            InitializeComponent();
            HiddenFieldRowCount = 0;
            Company = BaseCls.GlbUserComCode;
            BindCombos();
            CreateDtColumn();
            LoadInsCombos(true, true);
            GridBind(GridViewFinal, Main);
            TextBoxVal.Text = string.Format("{0:n2}", 0);
            panel_newPolicy.Visible = false;
            panel_newCom.Visible = false;
            DropDownListPeriod.SelectedItem = "12";
            BindPartyType();
            try
            {
                DropDownListSType.SelectedIndex = 0;
            }
            catch (Exception EX)
            {
            }
        }
        private void GridBind(DataGridView gv, DataTable dt)
        {
            gv.DataSource = null;
            gv.AutoGenerateColumns = false;
            gv.DataSource = dt;

        }
        private void LoadInsCombos(bool _isCompany, bool _isPollicy)
        {
            if (_isCompany)
            {
                DataTable _tbl1 = CHNLSVC.General.GetInsuranceCompanies();
                //DropDownListInsCom.DataSource = _tbl1;
                //DropDownListInsCom.DisplayMember = "MBI_DESC";
                //DropDownListInsCom.ValueMember = "MBI_CD";
                //// DropDownListInsCom.DataBind();
                //// DropDownListInsCom.SelectedIndex = -1;
                gvInsuCompany.AutoGenerateColumns = false;
                gvInsuCompany.DataSource = _tbl1;
                //try
                //{
                //    DropDownListInsCom.SelectedIndex = 0;
                //}
                //catch (Exception EX)
                //{
                //}
            }

            if (_isPollicy)
            {
                DataTable _tbl2 = CHNLSVC.General.GetInsurancePolicies();
                //DropDownListInsPol.DataSource = CHNLSVC.General.GetInsurancePolicies();
                //DropDownListInsPol.DisplayMember = "SVIP_POLC_DESC";
                //DropDownListInsPol.ValueMember = "SVIP_POLC_CD";
                ////DropDownListInsPol.DataBind();
                //// DropDownListInsPol.SelectedIndex = -1;
                //try
                //{
                //    DropDownListInsCom.SelectedIndex = 0;
                //}
                //catch (Exception EX)
                //{
                //}
                gvInsuPolicy.AutoGenerateColumns = false;
                gvInsuPolicy.DataSource = _tbl2;
            }

        }
        private void CreateDtColumn()
        {
            Main = new DataTable();
            Main.Columns.Add("Party", typeof(List<MasterProfitCenter>));
            Main.Columns.Add("Cat", typeof(List<InsuItem>));
            Main.Columns.Add("From", typeof(DateTime));
            Main.Columns.Add("To", typeof(DateTime));
            Main.Columns.Add("Sales_Type", typeof(string));
            Main.Columns.Add("Sales_Type_desc", typeof(string));
            Main.Columns.Add("Ins_Com", typeof(string));
            Main.Columns.Add("Ins_Com_DESC", typeof(string));
            Main.Columns.Add("Ins_Pol", typeof(string));
            Main.Columns.Add("Ins_Pol_DESC", typeof(string));
            Main.Columns.Add("Value", typeof(decimal));
            Main.Columns.Add("Ins_Period", typeof(decimal));
            Main.Columns.Add("IsRate", typeof(decimal));
            Main.Columns.Add("Fromvalue", typeof(decimal));
            Main.Columns.Add("Tovalue", typeof(decimal));
        }
        private void BindCombos()
        {

            DataTable datasource2 = CHNLSVC.General.GetSalesTypes("", null, null);

            DropDownListSType.DataSource = datasource2;
            DropDownListSType.DisplayMember = "srtp_desc";
            DropDownListSType.ValueMember = "srtp_cd";
            DropDownListSType.SelectedIndex = -1;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            VehicleInsuDefinitionNew formnew = new VehicleInsuDefinitionNew();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }
        private PriceBookLevelRef _priceBookLevelRef = null;

        private void getitems(object sender, EventArgs e)
        {
            bool _isSerPB = false;
            if (string.IsNullOrEmpty(txtPriceBook.Text) || string.IsNullOrEmpty(txtLevel.Text))
            {
                MessageBox.Show("Please select the price book/level", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //kapila 15/6/2015 check PB is serialized or not
            _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, txtPriceBook.Text, txtLevel.Text);
            if (_priceBookLevelRef.Sapl_is_serialized == true)
                _isSerPB = true;

            string _selected = string.Empty;
            _selected = txtBrand.Text.Trim() + "|" + txtCate1.Text.Trim() + "|" + txtCate2.Text.Trim() + "|" + txtModel.Text.Trim() + "|" + TextBoxItem.Text.Trim() + "|" + txtSerial.Text.Trim() + "|" + txtCircular.Text.Trim() + "|" + txtPromotion.Text.Trim();
            string[] _split = _selected.Split('|');
            int _count = 0;
            foreach (string _n in _split)
            {
                if (!string.IsNullOrEmpty(_n))
                    _count++;
            }

            if (_count == 0)
            {
                MessageBox.Show("Please select the any criteria", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            string selection = string.Empty;
            string _val = string.Empty;

            if (!string.IsNullOrEmpty(TextBoxItem.Text)) selection = "ITEM";
            if (!string.IsNullOrEmpty(txtBrand.Text)) selection = "ITEM";
            if (!string.IsNullOrEmpty(txtCate1.Text)) selection = "ITEM";
            if (!string.IsNullOrEmpty(txtCate2.Text)) selection = "ITEM";
            if (!string.IsNullOrEmpty(txtModel.Text)) selection = "ITEM";
            if (_isSerPB) selection = "SERIAL";  //kapila 15/6/2015
            if (!string.IsNullOrEmpty(txtCircular.Text)) selection = "CIRCULER";
            if (!string.IsNullOrEmpty(txtPromotion.Text)) selection = "PROMOTION";


            List<CashCommissionDetailRef> addList = new List<CashCommissionDetailRef>();
            DataTable dt = CHNLSVC.Sales.GetInsuCriteria(BaseCls.GlbUserComCode, selection, TextBoxItem.Text.Trim(), txtBrand.Text.Trim(), txtModel.Text.Trim(), txtCate1.Text.Trim(), txtCate2.Text.Trim(), txtSerial.Text.Trim(), txtCircular.Text.Trim(), txtPromotion.Text.Trim());

            System.Data.DataColumn newColumn = new System.Data.DataColumn("Value", typeof(System.Decimal));
            newColumn.DefaultValue = 0;
            dt.Columns.Add(newColumn);

            //System.Data.DataColumn serial = new System.Data.DataColumn("Serial", typeof(System.String));
            //serial.DefaultValue = txtSerial.Text.Trim();
            //dt.Columns.Add(serial);

            System.Data.DataColumn promotion = new System.Data.DataColumn("Promotion", typeof(System.String));
            promotion.DefaultValue = txtPromotion.Text.Trim();
            dt.Columns.Add(promotion);

            System.Data.DataColumn Circuler = new System.Data.DataColumn("Circuler", typeof(System.String));
            Circuler.DefaultValue = txtPromotion.Text.Trim();
            dt.Columns.Add(Circuler);

            DataTable dataSource2 = new DataTable();
            dataSource2.Columns.Add("CODE", typeof(System.String));
            dataSource2.Columns.Add("DESCRIPT", typeof(System.String));
            dataSource2.Columns.Add("Value", typeof(System.Decimal));
            dataSource2.Columns.Add("serial", typeof(System.String));

            //System.Data.DataColumn serial1 = new System.Data.DataColumn("Serial", typeof(System.String));
            //serial1.DefaultValue = txtSerial.Text.Trim();
            //dataSource2.Columns.Add(serial1);

            System.Data.DataColumn promotion1 = new System.Data.DataColumn("Promotion", typeof(System.String));
            promotion1.DefaultValue = txtPromotion.Text.Trim();
            dataSource2.Columns.Add(promotion1);

            System.Data.DataColumn Circuler1 = new System.Data.DataColumn("Circuler", typeof(System.String));
            Circuler1.DefaultValue = txtPromotion.Text.Trim();
            dataSource2.Columns.Add(Circuler1);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow drr in dt.Rows)
                {
                    string itmcd = drr["CODE"].ToString();
                    string descirption = drr["DESCRIPT"].ToString();
                    string serial = drr["SER"].ToString();
                    string _book = txtPriceBook.Text.Trim();
                    string _level = txtLevel.Text.Trim();
                    string _promotion = txtPromotion.Text.Trim();
                    decimal _price = -1;

                    DataTable _value = null;
                    if (_isSerPB)
                    {
                        _value = CHNLSVC.Sales.GetAllPriceSerialData(_book, _level, itmcd, TextBoxFrom.Value.Date, null, serial);
                        if (_value == null || _value.Rows.Count <= 0)
                           continue;


                        if (_value != null && _value.Rows.Count > 0)
                            _price = _value.Rows[0].Field<decimal>("sars_itm_price");
                    }
                    else
                    {
                        _value = CHNLSVC.Sales.GetPriceForItem(_book, _level, itmcd, TextBoxFrom.Value.Date, _promotion, txtCircular.Text.Trim());
                        if (_value == null || _value.Rows.Count <= 0)
                            continue;

                        if (_value != null && _value.Rows.Count > 0)
                            _price = _value.Rows[0].Field<decimal>("sapd_itm_price");
                    }

                    List<MasterItemTax> _tx = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, itmcd, "GOD", "VAT", string.Empty);
                    if (_tx != null && _tx.Count > 0 && _price != -1)
                        _price = _price * ((100 + _tx[0].Mict_tax_rate) / 100);
                    drr.SetField("Value", Math.Round(_price));


                    var _duplicate = from _dup in select_ITEMS_List.AsEnumerable()
                                     where _dup["code"].ToString() == itmcd && _dup["serial"].ToString() == serial
                                     select _dup;

                    if (_duplicate.Count() == 0)
                    {
                        DataRow DR2 = dataSource2.NewRow();
                        DR2["CODE"] = itmcd;
                        DR2["DESCRIPT"] = descirption;
                        DR2["Value"] = Math.Round(_price);
                        //if (!string.IsNullOrEmpty(txtSerial.Text)) DR2["Serial"] = txtSerial.Text.Trim();
                        DR2["serial"] = serial;
                        if (!string.IsNullOrEmpty(txtPromotion.Text)) DR2["Promotion"] = txtPromotion.Text.Trim();
                        if (!string.IsNullOrEmpty(txtCircular.Text)) DR2["Circuler"] = txtCircular.Text.Trim();
                        dataSource2.Rows.Add(DR2);
                    }
                }
            }
            else
            {
                    MessageBox.Show("No data found for the selected criteria", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            select_ITEMS_List.Merge(dataSource2);
            GridAll_Items.DataSource = null;
            GridAll_Items.AutoGenerateColumns = false;
            GridAll_Items.DataSource = select_ITEMS_List;


            this.btnAllItem_Click(sender, e);
            GridViewFinal.DataSource = null;
            GridViewFinal.AutoGenerateColumns = false;
            txtBrand.Clear();
            txtCate1.Clear();
            txtCate2.Clear();
            txtModel.Clear();
            TextBoxItem.Clear();
            txtSerial.Clear();
            txtCircular.Clear();
            txtPromotion.Clear();

        }
        private void getitemsAdditional(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtPriceBook.Text) || string.IsNullOrEmpty(txtLevel.Text))
            //{
            //    MessageBox.Show("Please select the price book/level", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            string _selected = string.Empty;
            _selected = txtBrand.Text.Trim() + "|" + txtCate1.Text.Trim() + "|" + txtCate2.Text.Trim() + "|" + txtModel.Text.Trim() + "|" + TextBoxItem.Text.Trim() + "|" + txtSerial.Text.Trim() + "|" + txtCircular.Text.Trim() + "|" + txtPromotion.Text.Trim();
            string[] _split = _selected.Split('|');
            int _count = 0;
            foreach (string _n in _split)
            {
                if (!string.IsNullOrEmpty(_n))
                    _count++;
            }

            if (_count == 0)
            {
                MessageBox.Show("Please select the any criteria", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            string selection = string.Empty;
            string _val = string.Empty;

            if (!string.IsNullOrEmpty(TextBoxItem.Text)) selection = "ITEM";
            if (!string.IsNullOrEmpty(txtBrand.Text)) selection = "ITEM";
            if (!string.IsNullOrEmpty(txtCate1.Text)) selection = "ITEM";
            if (!string.IsNullOrEmpty(txtCate2.Text)) selection = "ITEM";
            if (!string.IsNullOrEmpty(txtModel.Text)) selection = "ITEM";
            if (!string.IsNullOrEmpty(txtSerial.Text)) selection = "SERIAL";
            if (!string.IsNullOrEmpty(txtCircular.Text)) selection = "CIRCULER";
            if (!string.IsNullOrEmpty(txtPromotion.Text)) selection = "PROMOTION";


            List<CashCommissionDetailRef> addList = new List<CashCommissionDetailRef>();
            DataTable dt = CHNLSVC.Sales.GetInsuCriteriaAdditional(BaseCls.GlbUserComCode, selection, TextBoxItem.Text.Trim(), txtBrand.Text.Trim(), txtModel.Text.Trim(), txtCate1.Text.Trim(), txtCate2.Text.Trim(), txtSerial.Text.Trim(), txtCircular.Text.Trim(), txtPromotion.Text.Trim(), Convert.ToInt32(cmbDef.SelectedValue));
            System.Data.DataColumn newColumn = new System.Data.DataColumn("Value", typeof(System.Decimal));
            newColumn.DefaultValue = 0;
            dt.Columns.Add(newColumn);
            System.Data.DataColumn serial = new System.Data.DataColumn("Serial", typeof(System.String));
            serial.DefaultValue = txtSerial.Text.Trim();
            dt.Columns.Add(serial);
            System.Data.DataColumn promotion = new System.Data.DataColumn("Promotion", typeof(System.String));
            promotion.DefaultValue = txtPromotion.Text.Trim();
            dt.Columns.Add(promotion);
            System.Data.DataColumn Circuler = new System.Data.DataColumn("Circuler", typeof(System.String));
            Circuler.DefaultValue = txtPromotion.Text.Trim();
            dt.Columns.Add(Circuler);

            DataTable dataSource2 = new DataTable();
            dataSource2.Columns.Add("CODE", typeof(System.String));
            dataSource2.Columns.Add("DESCRIPT", typeof(System.String));
            dataSource2.Columns.Add("DESCRIPT2", typeof(System.String));
            dataSource2.Columns.Add("Value", typeof(System.Decimal));
            dataSource2.Columns.Add("selType", typeof(System.Int32));
            System.Data.DataColumn serial1 = new System.Data.DataColumn("Serial", typeof(System.String));
            serial1.DefaultValue = txtSerial.Text.Trim();
            dataSource2.Columns.Add(serial1);
            System.Data.DataColumn promotion1 = new System.Data.DataColumn("Promotion", typeof(System.String));
            promotion1.DefaultValue = txtPromotion.Text.Trim();
            dataSource2.Columns.Add(promotion1);
            System.Data.DataColumn Circuler1 = new System.Data.DataColumn("Circuler", typeof(System.String));
            Circuler1.DefaultValue = txtPromotion.Text.Trim();
            dataSource2.Columns.Add(Circuler1);

            foreach (DataRow drr in dt.Rows)
            {
                string itmcd = drr["CODE"].ToString();
                string descirption = drr["DESCRIPT"].ToString();
                string descirption2 = drr["DESCRIPT2"].ToString();
                string _book = txtPriceBook.Text.Trim();
                string _level = txtLevel.Text.Trim();
                string _promotion = txtPromotion.Text.Trim();

                Int32 _type = Convert.ToInt32(drr["selType"].ToString());


                decimal _price = -1;
                if (!string.IsNullOrEmpty(txtPriceBook.Text) && string.IsNullOrEmpty(txtLevel.Text))
                {
                    // DataTable _value = CHNLSVC.Sales.GetPriceForItem(_book, _level, itmcd, DateTime.Now.Date, _promotion, txtCircular.Text.Trim());
                    DataTable _value = CHNLSVC.Sales.GetPriceForItem(_book, _level, itmcd, TextBoxFrom.Value.Date, _promotion, txtCircular.Text.Trim());
                    if (_value == null || _value.Rows.Count <= 0)
                        continue;
                    if (_value != null && _value.Rows.Count > 0)
                        _price = _value.Rows[0].Field<decimal>("sapd_itm_price");
                    List<MasterItemTax> _tx = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, itmcd, "GOD", "VAT", string.Empty);
                    if (_tx != null && _tx.Count > 0 && _price != -1)
                        _price = _price * ((100 + _tx[0].Mict_tax_rate) / 100);
                    drr.SetField("Value", Math.Round(_price));


                }
                var _duplicate = from _dup in select_ITEMS_List.AsEnumerable()
                                 where _dup["code"].ToString() == itmcd
                                 select _dup;
                if (_duplicate.Count() == 0)
                {
                    DataRow DR2 = dataSource2.NewRow();
                    DR2["CODE"] = itmcd;
                    DR2["DESCRIPT"] = descirption;
                    DR2["DESCRIPT2"] = descirption2;
                    DR2["Value"] = Math.Round(_price);
                    if (!string.IsNullOrEmpty(txtSerial.Text)) DR2["Serial"] = txtSerial.Text.Trim();
                    if (!string.IsNullOrEmpty(txtPromotion.Text)) DR2["Promotion"] = txtPromotion.Text.Trim();
                    if (!string.IsNullOrEmpty(txtCircular.Text)) DR2["Circuler"] = txtCircular.Text.Trim();
                    DR2["selType"] = _type;
                    dataSource2.Rows.Add(DR2);
                }
            }
            select_ITEMS_List.Merge(dataSource2);
            GridAll_Items.DataSource = null;
            GridAll_Items.AutoGenerateColumns = false;
            GridAll_Items.DataSource = select_ITEMS_List;
            this.btnAllItem_Click(sender, e);
            GridViewFinal.DataSource = null;
            GridViewFinal.AutoGenerateColumns = false;
            txtBrand.Clear();
            txtCate1.Clear();
            txtCate2.Clear();
            txtModel.Clear();
            TextBoxItem.Clear();
            txtSerial.Clear();
            txtCircular.Clear();
            txtPromotion.Clear();

        }
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (cmbInsType.SelectedValue != "ADINS")
            {
                getitems(sender, e);
            }
            else
            {
                getitemsAdditional(sender, e);
            }
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

       private List<InsuItem> GetSelectedItemsList()
        {
            List<InsuItem> list = new List<InsuItem>();
            
            foreach (DataGridViewRow dgvr in GridAll_Items.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    InsuItem _one = new InsuItem();
                    _one.Circuler = dgvr.Cells[7].Value.ToString();
                    _one.Item = dgvr.Cells[1].Value.ToString();
                    _one.Promotion = dgvr.Cells[6].Value.ToString();
                    _one.Serial = dgvr.Cells[5].Value.ToString();
                    _one.Value = Convert.ToDecimal(dgvr.Cells[4].Value);
                    if (cmbInsType.SelectedValue == "ADINS")
                    {
                        if (Convert.ToInt32(dgvr.Cells[8].Value.ToString()) == 1)
                        {
                            _one.Cat1 = string.Empty;
                            _one.Cat2 = string.Empty;
                            _one.Brand = string.Empty;
                        }
                        else if (Convert.ToInt32(dgvr.Cells[8].Value.ToString()) == 2)
                        {
                            _one.Cat1 = dgvr.Cells[2].Value.ToString();
                            _one.Cat2 = string.Empty;
                            _one.Brand = dgvr.Cells[1].Value.ToString();
                            _one.Item = string.Empty;
                        }

                        else if (Convert.ToInt32(dgvr.Cells[8].Value.ToString()) == 3)
                        {
                            _one.Cat1 = dgvr.Cells[2].Value.ToString();
                            _one.Cat2 = dgvr.Cells[3].Value.ToString();
                            _one.Brand = dgvr.Cells[1].Value.ToString();
                            _one.Item = string.Empty;
                        }
                        else if (Convert.ToInt32(dgvr.Cells[8].Value.ToString()) == 4)
                        {
                            _one.Cat1 = dgvr.Cells[1].Value.ToString();
                            _one.Cat2 = string.Empty;
                            _one.Brand = string.Empty;
                            _one.Item = string.Empty;
                        }

                        else if (Convert.ToInt32(dgvr.Cells[8].Value.ToString()) == 5)
                        {
                            _one.Cat1 = dgvr.Cells[1].Value.ToString();
                            _one.Cat2 = dgvr.Cells[2].Value.ToString();
                            _one.Brand = string.Empty;
                            _one.Item = string.Empty;
                        }
                        else
                        {
                            _one.Cat1 = string.Empty;
                            _one.Cat2 = string.Empty;
                            _one.Brand = string.Empty;

                        }

                    }

                    list.Add(_one);
                }

               }
           
            
            return list;
        }
        private void ButtonAdd_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(Convert.ToString(DropDownListPartyTypes.SelectedValue)))
            {
                MessageBox.Show("Please select the Business Hierarchy", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(Convert.ToString(txtCirlur.Text)))
            {
                MessageBox.Show("Please Enter Circular No", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<string> PC_list = new List<string>();
            if (PCList == null || PCList.Rows.Count <= 0)
            {
                MessageBox.Show("Please select the profit center/channel/sub channel which need to allocate", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            foreach (DataRow r in PCList.Rows)
                PC_list.Add(r.Field<string>("Code"));

            if (PC_list.Count < 1)
            {
                MessageBox.Show("Please select profit center(s)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (true)
            {
                
            }
            if (cmbInsType.SelectedValue != "TNSPT")
            {
                if (chkIsRate.Checked == true)
                {
                    if (Convert.ToDouble(TextBoxVal.Text) > 100)
                    {
                        MessageBox.Show("Rate should lessser than or equal to 100", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TextBoxVal.Clear();
                        TextBoxVal.Focus();
                        return;
                    }
                    if (Convert.ToDouble(TextBoxVal.Text) <= 0)
                    {
                        MessageBox.Show("Rate should higher than  to zero(0)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TextBoxVal.Clear();
                        TextBoxVal.Focus();
                        return;
                    }
                }
                if (chkIsRate.Checked == false)
                {
                    if (Convert.ToDouble(TextBoxVal.Text) > 100)
                    {
                        MessageBox.Show("Rate should lessser than or equal to 100", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TextBoxVal.Clear();
                        TextBoxVal.Focus();
                        return;
                    }
                    if (Convert.ToDouble(TextBoxVal.Text) < 0)
                    {
                        MessageBox.Show("Rate should higher than  to zero(0)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TextBoxVal.Clear();
                        TextBoxVal.Focus();
                        return;
                    }
                }
            }
            else if (Convert.ToDouble(TextBoxVal.Text) < 0)
            {
                MessageBox.Show("Rate should higher than  to zero(0)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxVal.Clear();
                TextBoxVal.Focus();
                return;
            }
           
            List<InsuItem> ITEMS_list = GetSelectedItemsList();

            if (cmbInsType.SelectedValue != "TNSPT")

            {
                if (ITEMS_list.Count < 1)
                {
                    MessageBox.Show("Please select item(s)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            //----------------------------------------------------------------------------------------------------------
            //if (DropDownListInsCom.SelectedIndex == -1 || DropDownListInsPol.SelectedIndex == -1 || DropDownListSType.SelectedIndex == -1)
            //{
            //    MessageBox.Show("Please enter complete details!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            try
            {
                Convert.ToDecimal(TextBoxVal.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter valid insurance value.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Convert.ToDecimal(TextBoxVal.Text) < 0)
            {
                MessageBox.Show("Value should be greater than 0.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Convert.ToDecimal(DropDownListPeriod.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please select period.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //----------------------------------------------------------------------------------------------------------

            var _one = from DataGridViewRow r in gvInsuCompany.Rows where Convert.ToBoolean(r.Cells["ic_select"].Value) == true select r;
            var _two = from DataGridViewRow r in gvInsuPolicy.Rows where Convert.ToBoolean(r.Cells["ip_select"].Value) == true select r;

            if (cmbInsType.SelectedValue != "TNSPT")
            {
                if (_one == null || _one.Count() <= 0)
                {
                    MessageBox.Show("Please select the insurance company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (cmbInsType.SelectedValue != "TNSPT")
            {
                if (_two == null || _two.Count() <= 0)
                {
                    MessageBox.Show("Please select the insurance policy", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            //Tharanga 12/05/2017
            if (cmbInsType.SelectedValue == "TNSPT")
            {
               
                    DataRow dr = Main.NewRow();
                    List<MasterProfitCenter> pcs = new List<MasterProfitCenter>();
                    List<InsuItem> items = ITEMS_list;//new List<string>();
                    foreach (string pc_ in PC_list)
                    {
                        MasterProfitCenter pc = new MasterProfitCenter();
                        pc.Mpc_cd = pc_;
                        //user change company after search ???
                        pc.Mpc_com = Company;
                        pcs.Add(pc);
                    }
                    dr["Party"] = pcs;
                    dr["Cat"] = items;
                    try
                    {
                        dr["From"] = Convert.ToDateTime(TextBoxFrom.Text);
                        dr["To"] = Convert.ToDateTime(TextBoxTo.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Please select From and To  dates", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (Convert.ToDateTime(dr["From"]).Date > Convert.ToDateTime(dr["To"]).Date)
                    {
                        MessageBox.Show("From date should be less than to date!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    dr["Sales_Type"] = DropDownListSType.SelectedValue;
                    dr["Sales_Type_desc"] = DropDownListSType.Text;
                   // dr["Ins_Com"] = rC.Cells["ic_code"].Value; //DropDownListInsCom.SelectedValue;
                   // dr["Ins_Pol"] = rI.Cells["ip_code"].Value; //DropDownListInsPol.SelectedValue;
                    dr["Ins_Period"] = 0;
                    //----------------------
                    //MasterOutsideParty mstCom = CHNLSVC.Sales.GetOutSidePartyDetails(Convert.ToString(rC.Cells["ic_code"].Value), string.Empty);
                    //if (mstCom != null)
                    //{
                    //    dr["Ins_Com_DESC"] = mstCom.Mbi_desc;
                    //}
                    //InsuarancePolicy mstPol = CHNLSVC.Sales.GetInusPolicy(Convert.ToString(rI.Cells["ip_code"].Value));
                    //if (mstCom != null)
                    //{
                    //    dr["Ins_Pol_DESC"] = mstPol.Svip_polc_desc;
                    //}
                    //----------------------
                    try
                    {
                        dr["Value"] = Convert.ToDecimal(TextBoxVal.Text);
                        if (chkIsRate.Checked == true)
                        {
                            dr["IsRate"] = Convert.ToDecimal(1);
                        }

                        else
                        {
                            dr["IsRate"] = Convert.ToDecimal(0);
                        }

                        if (!string.IsNullOrEmpty(txtFrom.Text))
                        {
                            dr["Fromvalue"] = Convert.ToDecimal(txtFrom.Text);
                        }
                        else
                        {
                            dr["Fromvalue"] = 0;
                        }

                        if (!string.IsNullOrEmpty(txtTo.Text))
                        {
                            dr["Tovalue"] = Convert.ToDecimal(txtTo.Text);
                        }
                        else
                        {
                            dr["Tovalue"] = 99999999;
                        }

                    }
                    catch (Exception)
                    {
                        // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Value has to be number");
                        MessageBox.Show("Invalid Ins.Value!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TextBoxVal.Focus();
                        return;
                    }
                    foreach (DataRow drr in Main.Rows)
                    {
                        DateTime frm = Convert.ToDateTime(drr["From"].ToString());
                        DateTime to = Convert.ToDateTime(drr["To"].ToString());



                        string com = drr["Ins_Com"].ToString();
                        string poli = drr["Ins_Pol"].ToString();
                        string period = drr["Ins_Period"].ToString();
                        string saletp = drr["Sales_Type"].ToString();
                        

                    }
                    Main.Rows.Add(dr);
                    GridBind(GridViewFinal, Main);
                    HiddenFieldRowCount = Main.Rows.Count;
                    TextBoxFrom.Value = DateTime.Now.Date;
                    TextBoxTo.Value = DateTime.Now.Date;
                    TextBoxVal.Text = string.Format("{0:n2}", 0);


                }


            
            //////////////////////////////////////////////////////////////END tharanga
            foreach (DataGridViewRow rC in _one)
            {
                foreach (DataGridViewRow rI in _two)
                {
                    DataRow dr = Main.NewRow();
                    List<MasterProfitCenter> pcs = new List<MasterProfitCenter>();
                    List<InsuItem> items = ITEMS_list;//new List<string>();
                    foreach (string pc_ in PC_list)
                    {
                        MasterProfitCenter pc = new MasterProfitCenter();
                        pc.Mpc_cd = pc_;
                        //user change company after search ???
                        pc.Mpc_com = Company;
                        pcs.Add(pc);
                    }
                    dr["Party"] = pcs;
                    dr["Cat"] = items;
                    try
                    {
                        dr["From"] = Convert.ToDateTime(TextBoxFrom.Text);
                        dr["To"] = Convert.ToDateTime(TextBoxTo.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Please select From and To  dates", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (Convert.ToDateTime(dr["From"]).Date > Convert.ToDateTime(dr["To"]).Date)
                    {
                        MessageBox.Show("From date should be less than to date!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    dr["Sales_Type"] = DropDownListSType.SelectedValue;
                    dr["Sales_Type_desc"] = DropDownListSType.Text;
                    dr["Ins_Com"] = rC.Cells["ic_code"].Value; //DropDownListInsCom.SelectedValue;
                    dr["Ins_Pol"] = rI.Cells["ip_code"].Value; //DropDownListInsPol.SelectedValue;
                    dr["Ins_Period"] = DropDownListPeriod.Text;
                    //----------------------
                    MasterOutsideParty mstCom = CHNLSVC.Sales.GetOutSidePartyDetails(Convert.ToString(rC.Cells["ic_code"].Value), string.Empty);
                    if (mstCom != null)
                    {
                        dr["Ins_Com_DESC"] = mstCom.Mbi_desc;
                    }
                    InsuarancePolicy mstPol = CHNLSVC.Sales.GetInusPolicy(Convert.ToString(rI.Cells["ip_code"].Value));
                    if (mstCom != null)
                    {
                        dr["Ins_Pol_DESC"] = mstPol.Svip_polc_desc;
                    }
                    //----------------------
                    try
                    {
                        dr["Value"] = Convert.ToDecimal(TextBoxVal.Text);
                        if (chkIsRate.Checked == true)
                        {
                            dr["IsRate"] = Convert.ToDecimal(1);
                        }

                        else
                        {
                            dr["IsRate"] = Convert.ToDecimal(0);
                        }

                        if (!string.IsNullOrEmpty(txtFrom.Text))
                        {
                            dr["Fromvalue"] = Convert.ToDecimal(txtFrom.Text);
                        }
                        else
                        {
                            dr["Fromvalue"] = 0;
                        }

                        if (!string.IsNullOrEmpty(txtTo.Text))
                        {
                            dr["Tovalue"] = Convert.ToDecimal(txtTo.Text);
                        }
                        else
                        {
                            dr["Tovalue"] = 99999999;
                        }






                    }
                    catch (Exception)
                    {
                        // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Value has to be number");
                        MessageBox.Show("Invalid Ins.Value!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TextBoxVal.Focus();
                        return;
                    }
                    //*****************add----------------------------------------
                    foreach (DataRow drr in Main.Rows)
                    {
                        DateTime frm = Convert.ToDateTime(drr["From"].ToString());
                        DateTime to = Convert.ToDateTime(drr["To"].ToString());



                        string com = drr["Ins_Com"].ToString();
                        string poli = drr["Ins_Pol"].ToString();
                        string period = drr["Ins_Period"].ToString();
                        string saletp = drr["Sales_Type"].ToString();
                        // Int32 count=0;
                        if (frm == Convert.ToDateTime(TextBoxFrom.Text) && to == Convert.ToDateTime(TextBoxTo.Text) && com == Convert.ToString(rC.Cells["ic_code"].Value) && poli == Convert.ToString(rI.Cells["ip_code"].Value) && period == DropDownListPeriod.Text && DropDownListSType.SelectedValue.ToString() == saletp && txtFrom.Text == drr["fromValue"].ToString() && txtTo.Text == drr["tovalue"].ToString())
                        {
                            MessageBox.Show("Definition already added!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        //if (_duplicate.Count() == 0)
                        //{
                        //    DataRow DR2 = dataSource2.NewRow();
                        //    DR2["mi_cd"] = itmcd;
                        //    DR2["mi_shortdesc"] = descirption;
                        //    dataSource2.Rows.Add(DR2);
                        //}
                    }

                    //************************************************************
                    Main.Rows.Add(dr);
                }
            }

            GridBind(GridViewFinal, Main);
            HiddenFieldRowCount = Main.Rows.Count;
            TextBoxFrom.Value = DateTime.Now.Date;
            TextBoxTo.Value = DateTime.Now.Date;
            TextBoxVal.Text = string.Format("{0:n2}", 0);

            if (Main.Rows.Count > 0)
            {
                btnAddItem.Enabled = false;
                GridAll_Items.Columns["chkItm"].ReadOnly = true;
                btnAllItem.Enabled = false;
                btnNonItem.Enabled = false;
                btnClearItem.Enabled = false;
                pnlLoc.Enabled = false;
            }
            else
            {

                btnAddItem.Enabled = true;
                GridAll_Items.Columns["chkItm"].ReadOnly = false;
                btnAllItem.Enabled = true;
                btnNonItem.Enabled = true;
                btnClearItem.Enabled = true;
                pnlLoc.Enabled = true;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Main.Rows.Count > 0)
            {
                if (cmbInsType.SelectedValue == "VHINS")
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11063))
                    {
                        MessageBox.Show("You don't have permission. Permission code : " + 11063, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                if (cmbInsType.SelectedValue == "ADINS")
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11064))
                    {
                        MessageBox.Show("You don't have permission. Permission code : " + 11064, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                if (cmbInsType.SelectedValue == "TNSPT")
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11070))
                    {
                        MessageBox.Show("You don't have permission. Permission code : " + 11070, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtCirlur.Text))
                {
                    MessageBox.Show("Enter circular number", "Vehicle Insurance Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCirlur.Focus();
                    return;
                }

                if (CHNLSVC.General.CheckInsCircular(BaseCls.GlbUserComCode, txtCirlur.Text) == true)
                {
                    MessageBox.Show("Circular already available", "Vehicle Insurance Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCirlur.Focus();
                    return;
                }



                if (MessageBox.Show("Are you sure to save?", "Confirm Save", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                try
                {
                    //main table
                    if (cmbInsType.SelectedValue == "TNSPT")
                    {
                        #region
                        for (int i = 0; i < Main.Rows.Count; i++)
                        {
                            List<MasterProfitCenter> pcsTem = (List<MasterProfitCenter>)Main.Rows[i][0];
                            List<InsuItem> itemsTem = (List<InsuItem>)Main.Rows[i][1];
                            //divide pcs list into parts(200 item) 
                            for (int j = 0; j < pcsTem.Count; )
                            {
                                List<MasterProfitCenter> pcs = new List<MasterProfitCenter>();
                                if (pcsTem.Count > (j + 200))
                                {
                                    pcs = pcsTem.GetRange(j, 200);
                                    j = j + 200;
                                }
                                else
                                {
                                    pcs = pcsTem.GetRange(j, pcsTem.Count - j);
                                    j = j + (pcsTem.Count - j);
                                }
                                //divide items list into parts(200 item)

                                int a = CHNLSVC.General.SaveVehicalTransportDefinitionNew(pcs, Convert.ToDateTime(Main.Rows[i][2]), Convert.ToDateTime(Main.Rows[i][3]), Main.Rows[i][4].ToString(), Convert.ToDecimal(Main.Rows[i]["Value"]), BaseCls.GlbUserID, Main.Rows[i]["Ins_Com"].ToString(), Main.Rows[i]["Ins_Pol"].ToString(), Convert.ToInt32(Main.Rows[i]["Ins_Period"].ToString()), CheckBoxMandatory.Checked, Convert.ToString(DropDownListPartyTypes.SelectedValue), txtPriceBook.Text.Trim(), txtLevel.Text.Trim(), txtSerial.Text.Trim(), txtPromotion.Text.Trim(), txtCircular.Text.Trim(), Convert.ToInt32(Main.Rows[i]["IsRate"].ToString()), Convert.ToInt32(Main.Rows[i]["Fromvalue"].ToString()), Convert.ToInt32(Main.Rows[i]["Tovalue"].ToString()), cmbInsType.SelectedValue.ToString(), txtCirlur.Text);

                                //for (int k = 0; k < itemsTem.Count; )
                                //{
                                //    if (itemsTem.Count > (k + 50))
                                //    {
                                //        List<InsuItem> items = itemsTem.GetRange(k, 50);
                                //        //send 200 pcs and 200 items at once
                                //        CHNLSVC.General.SaveVehicalInsuranceDefinitionNew(pcs, items, Convert.ToDateTime(Main.Rows[i][2]), Convert.ToDateTime(Main.Rows[i][3]), Main.Rows[i][4].ToString(), Convert.ToDecimal(Main.Rows[i]["Value"]), BaseCls.GlbUserID, Main.Rows[i]["Ins_Com"].ToString(), Main.Rows[i]["Ins_Pol"].ToString(), Convert.ToInt32(Main.Rows[i]["Ins_Period"].ToString()), CheckBoxMandatory.Checked, Convert.ToString(DropDownListPartyTypes.SelectedValue), txtPriceBook.Text.Trim(), txtLevel.Text.Trim(), txtSerial.Text.Trim(), txtPromotion.Text.Trim(), txtCircular.Text.Trim(), Convert.ToInt32(Main.Rows[i]["IsRate"].ToString()), Convert.ToInt32(Main.Rows[i]["Fromvalue"].ToString()), Convert.ToInt32(Main.Rows[i]["Tovalue"].ToString()), cmbInsType.SelectedValue.ToString(), txtCirlur.Text);
                                //        k = k + 50;
                                //    }
                                //    else
                                //    {
                                //        List<InsuItem> items = itemsTem.GetRange(k, itemsTem.Count - k);
                                //        CHNLSVC.General.SaveVehicalInsuranceDefinitionNew(pcs, items, Convert.ToDateTime(Main.Rows[i][2]), Convert.ToDateTime(Main.Rows[i][3]), Main.Rows[i][4].ToString(), Convert.ToDecimal(Main.Rows[i]["Value"]), BaseCls.GlbUserID, Main.Rows[i]["Ins_Com"].ToString(), Main.Rows[i]["Ins_Pol"].ToString(), Convert.ToInt32(Main.Rows[i]["Ins_Period"].ToString()), CheckBoxMandatory.Checked, Convert.ToString(DropDownListPartyTypes.SelectedValue), txtPriceBook.Text.Trim(), txtLevel.Text.Trim(), txtSerial.Text.Trim(), txtPromotion.Text.Trim(), txtCircular.Text.Trim(), Convert.ToInt32(Main.Rows[i]["IsRate"].ToString()), Convert.ToInt32(Main.Rows[i]["Fromvalue"].ToString()), Convert.ToInt32(Main.Rows[i]["Tovalue"].ToString()), cmbInsType.SelectedValue.ToString(), txtCirlur.Text);
                                //        k = itemsTem.Count;
                                //    }
                                //}
                            }
                           
                        }
                        #endregion

                    }
                    else
                    {
                        #region
                        for (int i = 0; i < Main.Rows.Count; i++)
                        {
                            List<MasterProfitCenter> pcsTem = (List<MasterProfitCenter>)Main.Rows[i][0];
                            List<InsuItem> itemsTem = (List<InsuItem>)Main.Rows[i][1];
                            //divide pcs list into parts(200 item) 
                            for (int j = 0; j < pcsTem.Count; )
                            {
                                List<MasterProfitCenter> pcs = new List<MasterProfitCenter>();
                                if (pcsTem.Count > (j + 200))
                                {
                                    pcs = pcsTem.GetRange(j, 200);
                                    j = j + 200;
                                }
                                else
                                {
                                    pcs = pcsTem.GetRange(j, pcsTem.Count - j);
                                    j = j + (pcsTem.Count - j);
                                }
                                //divide items list into parts(200 item)

                                for (int k = 0; k < itemsTem.Count; )
                                {
                                    if (itemsTem.Count > (k + 50))
                                    {
                                        List<InsuItem> items = itemsTem.GetRange(k, 50);
                                        //send 200 pcs and 200 items at once
                                        CHNLSVC.General.SaveVehicalInsuranceDefinitionNew(pcs, items, Convert.ToDateTime(Main.Rows[i][2]), Convert.ToDateTime(Main.Rows[i][3]), Main.Rows[i][4].ToString(), Convert.ToDecimal(Main.Rows[i]["Value"]), BaseCls.GlbUserID, Main.Rows[i]["Ins_Com"].ToString(), Main.Rows[i]["Ins_Pol"].ToString(), Convert.ToInt32(Main.Rows[i]["Ins_Period"].ToString()), CheckBoxMandatory.Checked, Convert.ToString(DropDownListPartyTypes.SelectedValue), txtPriceBook.Text.Trim(), txtLevel.Text.Trim(), txtSerial.Text.Trim(), txtPromotion.Text.Trim(), txtCircular.Text.Trim(), Convert.ToInt32(Main.Rows[i]["IsRate"].ToString()), Convert.ToInt32(Main.Rows[i]["Fromvalue"].ToString()), Convert.ToInt32(Main.Rows[i]["Tovalue"].ToString()), cmbInsType.SelectedValue.ToString(), txtCirlur.Text);
                                        k = k + 50;
                                    }
                                    else
                                    {
                                        List<InsuItem> items = itemsTem.GetRange(k, itemsTem.Count - k);
                                        CHNLSVC.General.SaveVehicalInsuranceDefinitionNew(pcs, items, Convert.ToDateTime(Main.Rows[i][2]), Convert.ToDateTime(Main.Rows[i][3]), Main.Rows[i][4].ToString(), Convert.ToDecimal(Main.Rows[i]["Value"]), BaseCls.GlbUserID, Main.Rows[i]["Ins_Com"].ToString(), Main.Rows[i]["Ins_Pol"].ToString(), Convert.ToInt32(Main.Rows[i]["Ins_Period"].ToString()), CheckBoxMandatory.Checked, Convert.ToString(DropDownListPartyTypes.SelectedValue), txtPriceBook.Text.Trim(), txtLevel.Text.Trim(), txtSerial.Text.Trim(), txtPromotion.Text.Trim(), txtCircular.Text.Trim(), Convert.ToInt32(Main.Rows[i]["IsRate"].ToString()), Convert.ToInt32(Main.Rows[i]["Fromvalue"].ToString()), Convert.ToInt32(Main.Rows[i]["Tovalue"].ToString()), cmbInsType.SelectedValue.ToString(), txtCirlur.Text);
                                        k = itemsTem.Count;
                                    }
                                }
                            }
                            // ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Records added sucessfully!');window.location='VehicalInsuranceDefinition.aspx'", true);
                            //MessageBox.Show("Successfully saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //this.btnClear_Click(null, null);
                            //MessageBox.Show("Successfully saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        #endregion
                    }
                    this.btnClear_Click(null, null);
                    MessageBox.Show("Successfully saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception er)
                {
                    // string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = '../Default.aspx';</script>";
                    // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                    MessageBox.Show("System Error Occur. : " + er.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
               

            }

        }
        private void linkLabelNewCom_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TextBoxInsName.Text = "";
            TextBoxInsCode.Text = "";
            panel_newCom.Visible = true;
            panel_newCom.Location = new Point(274, 255);

            panel_newPolicy.Visible = false;
            DataTable dtCom = CHNLSVC.General.GetInsuranceCompanies();
            LoadGrid(dtCom, GridViewInsCompany);
        }
        private void btnNewComClose_Click(object sender, EventArgs e)
        {
            panel_newCom.Visible = false;
        }
        private void btnNewCom_Click(object sender, EventArgs e)
        {
            MasterOutsideParty _outPar = new MasterOutsideParty();
            _outPar.Mbi_cd = TextBoxInsCode.Text;
            _outPar.Mbi_desc = TextBoxInsName.Text;
            _outPar.Mbi_tp = "INS";
            _outPar.Mbi_cre_when = CHNLSVC.Security.GetServerDateTime().Date;//DateTime.Now;
            _outPar.Mbi_act = true;
            try
            {
                if (MessageBox.Show("Are you sure to save?", "Confirm save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                int retVal = CHNLSVC.General.SaveInsuranceCompany(_outPar);
                if (retVal == -999)
                {
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Company alredy excists!');", true);
                    MessageBox.Show("Company alredy excists!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DataTable dtCom = CHNLSVC.General.GetInsuranceCompanies();
                LoadGrid(dtCom, GridViewInsCompany);
                LoadInsCombos(true, false);
                // ModalPopupExtender2.Show();
            }
            catch (Exception er)
            {
                // string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = '../Default.aspx';</script>";
                //  ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                MessageBox.Show("System Error Occur. : " + er.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void LoadGrid(DataTable source, DataGridView gv)
        {
            gv.DataSource = null;
            gv.AutoGenerateColumns = false;
            gv.DataSource = source;
            // gv.DataBind();
        }
        private void btnCloseNewPoli_Click(object sender, EventArgs e)
        {
            panel_newPolicy.Visible = false;
        }
        private void linkLabelNewPoli_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TextBoxPType.Text = "";
            panel_newPolicy.Visible = true;
            panel_newPolicy.Location = new Point(274, 255);

            panel_newCom.Visible = false;
            DataTable dtPol = CHNLSVC.General.GetInsurancePolicies();
            LoadGrid(dtPol, GridViewPolicy);
        }
        private void btnNewPolicy_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to save?", "Confirm save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                int retVal = CHNLSVC.General.SaveInsurancePolicy(TextBoxPType.Text, BaseCls.GlbUserID);
                if (retVal == -999)
                {
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Ploicy alredy excists!');", true);
                    MessageBox.Show("Ploicy alredy excists!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DataTable dtPol = CHNLSVC.General.GetInsurancePolicies();
                LoadGrid(dtPol, GridViewPolicy);
                LoadInsCombos(false, true);
                //ModalPopupExtender1.Show();
            }
            catch (Exception er)
            {
                //string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = '../Default.aspx';</script>";

                MessageBox.Show("System Error Occur. : " + er.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnModel_Click(object sender, EventArgs e)
        {

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
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {

                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPriceBook.Text.Trim() + seperator);
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
                        paramsText.Append("" + seperator + "" + seperator + BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "Loc" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item_Serials:
                    {
                        paramsText.Append(TextBoxItem.Text + seperator + "" + seperator + BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "Loc" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Circular:
                    {
                        paramsText.Append("" + seperator + "Circular" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Promotion:
                    {
                        paramsText.Append("" + seperator + "Promotion" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EmployeeCate:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Sales_Type:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CashCommissionCircular:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
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
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PromotionalDiscount:
                    {
                        paramsText.Append(DateTime.Now.ToString("dd/MM/yyyy") + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        private void btnItem_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = TextBoxItem;
                _CommonSearch.txtSearchbyword.Text = TextBoxItem.Text;
                _CommonSearch.ShowDialog();
                TextBoxItem.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }
        private void GridViewFinal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                if (MessageBox.Show("Are you sure to delete?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                Int32 rowIndex = e.RowIndex;
                Main.Rows.RemoveAt(e.RowIndex);
                LoadGrid(Main, GridViewFinal);
                HiddenFieldRowCount = Main.Rows.Count;

                if (Main.Rows.Count > 0)
                {
                    btnAddItem.Enabled = false;
                    GridAll_Items.Columns["chkItm"].ReadOnly = true;
                    btnAllItem.Enabled = false;
                    btnNonItem.Enabled = false;
                    btnClearItem.Enabled = false;
                }
                else
                {
                    btnAddItem.Enabled = true;
                    GridAll_Items.Columns["chkItm"].ReadOnly = false;
                    panel_itms.Enabled = true;
                    btnAllItem.Enabled = true;
                    btnNonItem.Enabled = true;
                    btnClearItem.Enabled = true;
                }
            }
        }
        public void BindPartyType()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            PartyTypes.Add("CHNL", "Channel");
            PartyTypes.Add("SCHNL", "Sub Channel");
            PartyTypes.Add("PC", "PC");
            DropDownListPartyTypes.DataSource = new BindingSource(PartyTypes, null);
            DropDownListPartyTypes.DisplayMember = "Value";
            DropDownListPartyTypes.ValueMember = "Key";

            Dictionary<string, string> InsType = new Dictionary<string, string>();
            InsType.Add("VHINS", "Vehicle Insurance");
            InsType.Add("ADINS", "Smart Insurance");
            InsType.Add("TNSPT", "Transport Charges");


            cmbInsType.DataSource = new BindingSource(InsType, null);
            cmbInsType.DisplayMember = "Value";
            cmbInsType.ValueMember = "Key";


            Dictionary<string, string> defType = new Dictionary<string, string>();
            defType.Add("1", "Item Code");
            defType.Add("2", "Brand code and Main category code");
            defType.Add("3", "Brand code, Main category code and Category code");
            defType.Add("4", "Main category code only");
            defType.Add("5", "Main category and Category code");
            cmbDef.DataSource = new BindingSource(defType, null);
            cmbDef.DisplayMember = "Value";
            cmbDef.ValueMember = "Key";


        }
        private void btnHierachySearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbInsType.SelectedValue == "TNSPT")
                {
                    Base _basePage = new Base();
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    DataTable dt = new DataTable();
                    DataTable _result = new DataTable();
                    if (cmbnushir.SelectedValue.ToString() == "COM")
                    {
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    }
                    else if (cmbnushir.SelectedValue.ToString() == "CHNL")
                    {
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    }
                    else if (cmbnushir.SelectedValue.ToString() == "SCHNL")
                    {
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    }

                    else if (cmbnushir.SelectedValue.ToString() == "AREA")
                    {
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    }
                    else if (cmbnushir.SelectedValue.ToString() == "REGION")
                    {
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    }
                    else if (cmbnushir.SelectedValue.ToString() == "ZONE")
                    {
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    }
                    else if (cmbnushir.SelectedValue.ToString() == "LOC")
                    {
                        //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                        //_result = _basePage.CHNLSVC.General.GetSCM2LocationByCompany(BaseCls.GlbUserComCode);
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                        _result = _basePage.CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                       
                       
                       
                    }
                    else if (cmbnushir.SelectedValue.ToString() == "GPC")
                    {
                        // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GPC);
                        //  _result = _basePage.CHNLSVC.CommonSearch.Get_GPC(_CommonSearch.SearchParams, null, null);
                        _result = CHNLSVC.General.Get_GET_GPC("", "");
                    }

                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtHierchCode;
                    _CommonSearch.ShowDialog();
                    txtHierchCode.Focus();
                }
                else
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
                    _result = CHNLSVC.General.Get_GET_GPC("", "");
                }

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtHierchCode;
                _CommonSearch.ShowDialog();
                txtHierchCode.Focus();
            }
        }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void btnSearchPB_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPriceBook;
                _CommonSearch.txtSearchbyword.Text = txtPriceBook.Text;
                _CommonSearch.ShowDialog();
                txtPriceBook.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void btnSearchPriceLvl_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLevel;
                _CommonSearch.txtSearchbyword.Text = txtLevel.Text;
                _CommonSearch.ShowDialog();
                txtLevel.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        DataTable PCList = null;
        string _PreviousType = string.Empty;
        private void btnAddPartys_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbInsType.SelectedValue == "TNSPT")
                {
                    load_data();
                }
                else
                {
                    if (!string.IsNullOrEmpty(_PreviousType) && _PreviousType != Convert.ToString(DropDownListPartyTypes.SelectedValue)) PCList = new DataTable();
                    Base _basePage = new Base();
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    if (PCList == null) PCList = new DataTable();
                    DataTable dt = new DataTable();
                    DataTable _result = new DataTable();

                    if (PCList != null && PCList.Rows.Count > 0)
                    {
                        var _dup = PCList.AsEnumerable().Where(x => x.Field<string>("Code") == txtHierchCode.Text.Trim()).ToList();
                        if (_dup != null && _dup.Count > 0)
                        {
                            MessageBox.Show("This code already available", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    if (DropDownListPartyTypes.SelectedValue.ToString() == "COM")
                    {
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                        if (txtHierchCode.Text != "")
                        {
                            _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                        }
                    }
                    else if (DropDownListPartyTypes.SelectedValue.ToString() == "CHNL")
                    {
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                        if (txtHierchCode.Text != "")
                        {
                            _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                        }

                    }
                    else if (DropDownListPartyTypes.SelectedValue.ToString() == "SCHNL")
                    {
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                        if (txtHierchCode.Text != "")
                        {
                            _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                        }
                    }

                    else if (DropDownListPartyTypes.SelectedValue.ToString() == "AREA")
                    {
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                        if (txtHierchCode.Text != "")
                        {
                            _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                        }
                    }
                    else if (DropDownListPartyTypes.SelectedValue.ToString() == "REGION")
                    {
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                        if (txtHierchCode.Text != "")
                        {
                            _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                        }
                    }
                    else if (DropDownListPartyTypes.SelectedValue.ToString() == "ZONE")
                    {
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                        if (txtHierchCode.Text != "")
                        {
                            _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                        }
                    }
                    else if (DropDownListPartyTypes.SelectedValue.ToString() == "PC")
                    {
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                        if (txtHierchCode.Text != "")
                        {
                            _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                        }
                    }
                    else if (DropDownListPartyTypes.SelectedValue.ToString() == "GPC")
                    {
                        _result = CHNLSVC.General.Get_GET_GPC("", "");
                        if (txtHierchCode.Text != "")
                        {
                            _result = _basePage.CHNLSVC.General.Get_GET_GPC(txtHierchCode.Text, "");
                        }
                    }
                    if (_result == null || _result.Rows.Count <= 0)
                    {
                        MessageBox.Show("Invalid search term, no result found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (string.IsNullOrEmpty(_PreviousType)) PCList = new DataTable();
                    PCList.Merge(_result);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = PCList;
                    grvParty.DataSource = null;
                    grvParty.AutoGenerateColumns = false;
                    grvParty.DataSource = _source;
                    _PreviousType = Convert.ToString(DropDownListPartyTypes.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void btnClearPrtys_Click(object sender, EventArgs e)
        {
            PCList = new DataTable();
            grvParty.AutoGenerateColumns = false;
            grvParty.DataSource = new DataTable();
        }

        private void load_data()
        {
            Base _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            DataTable dt = new DataTable();
            DataTable _result = new DataTable();

            if (cmbnushir.SelectedValue.ToString() == "COM")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                if (txtHierchCode.Text != "")
                {
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                }
            }
            else if (cmbnushir.SelectedValue.ToString() == "CHNL")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                if (txtHierchCode.Text != "")
                {
                    _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                }

            }
            else if (cmbnushir.SelectedValue.ToString() == "LOC")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                _result = _basePage.CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                if (txtHierchCode.Text != "")
                {
                    _result = _basePage.CHNLSVC.CommonSearch.Get_LOC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtHierchCode.Text);
                }

            }
          
           
            if (string.IsNullOrEmpty(_PreviousType)) PCList = new DataTable();
            PCList.Merge(_result);
            BindingSource _source = new BindingSource();
            _source.DataSource = PCList;
            grvParty.DataSource = null;
            grvParty.AutoGenerateColumns = false;
            grvParty.DataSource = _source;
            _PreviousType = Convert.ToString(cmbnushir.SelectedValue);
        }
        private void btnBrand_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void btnMainCat_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void btnCat_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void btnSerial_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 6;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSerialSearchData(_CommonSearch.SearchParams);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerial;
                _CommonSearch.txtSearchbyword.Text = txtSerial.Text;
                _CommonSearch.ShowDialog();
                txtSerial.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void btnCircular_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circular);
                DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCircular;
                _CommonSearch.txtSearchbyword.Text = txtCircular.Text;
                _CommonSearch.ShowDialog();
                txtCircular.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void btnPromation_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
                DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPromotion;
                _CommonSearch.txtSearchbyword.Text = txtPromotion.Text;
                _CommonSearch.ShowDialog();
                txtPromotion.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void DropDownListPartyTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            PCList = new DataTable();
            BindingSource _source = new BindingSource();
            _source.DataSource = PCList;
            grvParty.DataSource = null;
            grvParty.AutoGenerateColumns = false;
            grvParty.DataSource = _source;
        }
        private void txtPriceBook_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPriceBook.Text)) return;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
                var _exist = _result.AsEnumerable().Where(x => x.Field<string>("book") == txtPriceBook.Text.Trim());
                if (_exist == null || _exist.Count() <= 0)
                {
                    MessageBox.Show("Book is invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPriceBook.Clear();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void txtLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtLevel.Text)) return;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
                var _exist = _result.AsEnumerable().Where(x => x.Field<string>("price level") == txtLevel.Text.Trim());
                if (_exist == null || _exist.Count() <= 0)
                {
                    MessageBox.Show("Price level is invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtLevel.Clear();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnModel_Click_1(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void grvParty_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grvParty.RowCount > 0)
                if (e.RowIndex != -1)
                    if (e.ColumnIndex == 0)
                    {
                        if (MessageBox.Show("Do you need to remove this", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                        if (PCList != null && PCList.Rows.Count > 0)
                        {
                            PCList.Rows.RemoveAt(e.RowIndex);
                            BindingSource _source = new BindingSource();
                            _source.DataSource = PCList;
                            grvParty.DataSource = null;
                            grvParty.AutoGenerateColumns = false;
                            grvParty.DataSource = _source;
                        }
                    }
        }
        private void txtHierchCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btnHierachySearch_Click(null, null);
        }
        private void DropDownListPartyTypes_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtHierchCode.Focus();
        }
        private void txtPriceBook_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtLevel.Focus();
            if (e.KeyCode == Keys.F2) btnSearchPB_Click(null, null);
        }
        private void txtLevel_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtBrand.Focus();
            if (e.KeyCode == Keys.F2) btnSearchPriceLvl_Click(null, null);
        }
        private void txtHierchCode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnHierachySearch_Click(null, null);
        }
        private void txtPriceBook_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearchPB_Click(null, null);
        }
        private void txtLevel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearchPriceLvl_Click(null, null);
        }
        private void txtBrand_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtCate1.Focus();
            if (e.KeyCode == Keys.F2) btnBrand_Click(null, null);
        }
        private void txtCate1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btnMainCat_Click(null, null);
            if (e.KeyCode == Keys.Enter) txtCate2.Focus();
        }
        private void txtCate2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btnCat_Click(null, null);
            if (e.KeyCode == Keys.Enter) txtModel.Focus();
        }
        private void txtModel_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btnModel_Click_1(null, null);
            if (e.KeyCode == Keys.Enter) TextBoxItem.Focus();
        }
        private void TextBoxItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btnItem_Click(null, null);
            if (e.KeyCode == Keys.Enter) txtSerial.Focus();
        }
        private void txtSerial_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btnSerial_Click(null, null);
            if (e.KeyCode == Keys.Enter) txtCircular.Focus();
        }
        private void txtCircular_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btnCircular_Click(null, null);
            if (e.KeyCode == Keys.Enter) txtPromotion.Focus();
        }
        private void txtPromotion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btnPromation_Click(null, null);
            if (e.KeyCode == Keys.Enter) btnSave.Select();
        }
        private void txtBrand_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnBrand_Click(null, null);
        }
        private void txtCate1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnMainCat_Click(null, null);
        }
        private void txtCate2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnCat_Click(null, null);
        }
        private void txtModel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnModel_Click_1(null, null);
        }
        private void TextBoxItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnItem_Click(null, null);
        }
        private void txtSerial_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSerial_Click(null, null);
        }
        private void txtCircular_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnCircular_Click(null, null);
        }
        private void txtPromotion_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPromation_Click(null, null);
        }
        private void TextBoxFrom_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) TextBoxTo.Focus();
        }
        private void TextBoxTo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) DropDownListPeriod.Focus();
        }
        private void DropDownListPeriod_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) TextBoxVal.Focus();
        }
        private void TextBoxVal_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) DropDownListSType.Focus();
        }
        private void DropDownListSType_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ButtonAdd.Focus();
        }

        private void TextBoxVal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                IsDecimalAllow(true, sender, e);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void cmbInsType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbInsType.SelectedValue == "ADINS")
            {
                pnlAdd.Visible = true;
                // pnlPrice.Visible = false;
                chkIsRate.Visible = true;
                chkIsRate.Checked = false;
                pnlRange.Visible = true;
                panel1.Enabled = true;
                panel_newPolicy.Enabled = true;
                groupBox2.Enabled = true;
                groupBox1.Enabled = true;
                CheckBoxMandatory.Enabled = true;
                chkIsRate.Enabled = true;
                DropDownListPeriod.Enabled = true;
                txtFrom.Enabled = true;
                txtTo.Enabled = true;
                label16.Text = "Ins. Value";
                DropDownListPartyTypes.Enabled = true;
                DropDownListSType.Enabled = true;
                pnlPrice.Enabled = true;
                DropDownListPeriod.Enabled=true;
                cmbnushir.Visible = false;
            }

            else if (cmbInsType.SelectedValue == "TNSPT")
            {
                panel1.Enabled= false;
                panel_newPolicy.Enabled = false;
                groupBox2.Enabled= false;
                groupBox1.Enabled = false; 
                CheckBoxMandatory.Enabled= false;
                chkIsRate.Enabled= false;
                DropDownListPeriod.Enabled= false;
                txtFrom.Enabled= false;
                txtTo.Enabled = false;
                label16.Text = "Trans. Value";
                DropDownListPartyTypes.DisplayMember = "PC";
                DropDownListPartyTypes.SelectedValue = "PC";
                pnlPrice.Enabled = false;
                DropDownListPartyTypes.Enabled = false;
                DropDownListSType.SelectedValue = 0;
                cmbnushir.Visible = true;
                DropDownListPeriod.SelectedValue = 0;
                DropDownListSType.Enabled = false;
                txtFrom.Text = "";
                txtTo.Text = "";
                bind_Combo_DropDownListPartyTypes();

                // pnlPrice.Visible = false;
                //chkIsRate.Visible = true;
                //chkIsRate.Checked = false;
                //pnlRange.Visible = true;
            }
            else
            {
                pnlAdd.Visible = false;
                chkIsRate.Visible = false;
                chkIsRate.Checked = false;
                pnlRange.Visible = false;
                label16.Text = "Ins. Value";
                DropDownListPartyTypes.Enabled = true;
                DropDownListPeriod.Enabled=true;
                DropDownListSType.Enabled = true; 
                pnlPrice.Enabled = true;
              //  pnlPrice.Visible = true;
                cmbnushir.Visible = false;
            }
        }
        private void bind_Combo_DropDownListPartyTypes()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();

            PartyTypes.Add("COM", "Company");
            PartyTypes.Add("CHNL", "Channel");
            PartyTypes.Add("LOC", "Location");

            cmbnushir.DataSource = new BindingSource(PartyTypes, null);
            cmbnushir.DisplayMember = "Value";
            cmbnushir.ValueMember = "Key";
        }
        private void cmbDef_SelectedIndexChanged(object sender, EventArgs e)
        {
            select_ITEMS_List = new DataTable();
            GridAll_Items.DataSource = null;
        }

        private void chkIsRate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsRate.Checked == true)
            {
                if (Convert.ToDouble(TextBoxVal.Text) > 100)
                {
                    MessageBox.Show("Rate should lessser than or equal to 100", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TextBoxVal.Clear();
                    TextBoxVal.Focus();
                    return;
                }
                if (Convert.ToDouble(TextBoxVal.Text) <= 0)
                {
                    MessageBox.Show("Rate should higher than  to zero(0)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TextBoxVal.Clear();
                    TextBoxVal.Focus();
                    return;
                }
            }
        }

        private void TextBoxVal_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBoxVal_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxVal.Text))
            {
                return;
            }
            if (chkIsRate.Checked == true)
            {
                if (Convert.ToDouble(TextBoxVal.Text) > 100)
                {
                    MessageBox.Show("Rate should lessser than or equal to 100", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TextBoxVal.Clear();
                    TextBoxVal.Focus();
                    return;
                }
                if (Convert.ToDouble(TextBoxVal.Text) <= 0)
                {
                    MessageBox.Show("Rate should higher than  to zero(0)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TextBoxVal.Clear();
                    TextBoxVal.Focus();
                    return;
                }
            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                IsDecimalAllow(true, sender, e);

            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                IsDecimalAllow(true, sender, e);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtFrom_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtTo.Focus();
            }
        }

        private void txtTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ButtonAdd.Focus();
            }
        }

        private void txtTo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTo.Text) && string.IsNullOrEmpty(txtFrom.Text))
            {
                if (Convert.ToDouble(txtTo.Text) <= Convert.ToDouble(txtFrom.Text))
                {
                    MessageBox.Show("To value must be higher than from value", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTo.Clear();
                    txtTo.Focus();
                    return;
                }
            }
        }

        private void txtFrom_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTo.Text) && string.IsNullOrEmpty(txtFrom.Text))
            {
                if (Convert.ToDouble(txtTo.Text) <= Convert.ToDouble(txtFrom.Text))
                {
                    MessageBox.Show("From value must be lesser than To value", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFrom.Clear();
                    txtFrom.Focus();
                    return;
                }
            }
        }

        private void txtTo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtModel_TextChanged(object sender, EventArgs e)
        {

        }

        private void grvParty_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void GridViewFinal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmbInsType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCirlur.Focus();
            }
        }

        private void txtCirlur_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtHierchCode.Focus();
            }
        }

        private void DropDownListPartyTypes_DropDown(object sender, EventArgs e)
        {

        }

        private void txtHierchCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPriceBook.Focus();
            }
        }

        private void txtPriceBook_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtLevel.Focus();
            }
        }

        private void txtLevel_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                TextBoxFrom.Focus();
            }
        }

        private void TextBoxFrom_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                TextBoxTo.Focus();
            }
        }

        private void TextBoxTo_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                DropDownListPeriod.Focus();
            }
        }

        private void DropDownListPeriod_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                TextBoxVal.Focus();
            }
        }

        private void TextBoxVal_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                DropDownListSType.Focus();
            }
        }

        private void DropDownListSType_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtFrom.Focus();
            }
        }

        private void txtBrand_TextChanged(object sender, EventArgs e)
        {

        }

        private void GridAll_Items_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        
    }
}
