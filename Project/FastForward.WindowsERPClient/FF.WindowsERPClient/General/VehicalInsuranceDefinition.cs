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
    public partial class VehicalInsuranceDefinition : Base
    {
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

        public VehicalInsuranceDefinition()
        {
            InitializeComponent();
            InitializeComponent();
            HiddenFieldRowCount = 0;
            Company = BaseCls.GlbUserComCode;
            BindCombos();
            CreateTableMain();
            GridBind(GridViewFinal, Main);
            TextBoxVal.Text = string.Format("{0:n2}", 0);           
            ucProfitCenterSearch1.Company = BaseCls.GlbUserComCode;

           
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
            DataTable datasource2 = CHNLSVC.General.GetSalesTypes("", null, null);           
            DropDownListSType.DataSource = datasource2;
            DropDownListSType.DisplayMember = "srtp_desc";
            DropDownListSType.ValueMember = "srtp_cd";
            DropDownListSType.SelectedIndex = -1;
        }

        //*******************************************************
        private void LoadInsCombos()
        {
            DropDownListInsCom.DataSource = CHNLSVC.General.GetInsuranceCompanies();
            DropDownListInsCom.DisplayMember = "MBI_DESC";
            DropDownListInsCom.ValueMember = "MBI_CD";
           // DropDownListInsCom.DataBind();
            DropDownListInsCom.SelectedIndex = -1;

            DropDownListInsPol.DataSource = CHNLSVC.General.GetInsurancePolicies();
            DropDownListInsPol.DisplayMember = "SVIP_POLC_DESC";
            DropDownListInsPol.ValueMember = "SVIP_POLC_CD";
           // DropDownListInsPol.DataBind();
            DropDownListInsPol.SelectedIndex = -1;
        }
    }
}
