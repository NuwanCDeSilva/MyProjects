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
    public partial class VehicleInsuDefinition : Base
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
        public VehicleInsuDefinition()
        {
            InitializeComponent();
            HiddenFieldRowCount = 0;
            Company = BaseCls.GlbUserComCode;
            BindCombos();
            CreateDtColumn();
            LoadInsCombos(true, true);
            LoadCombos(-5);
            GridBind(GridViewFinal, Main);
            TextBoxVal.Text = string.Format("{0:n2}", 0);
            ucProfitCenterSearch1.Company = BaseCls.GlbUserComCode;

            panel_newPolicy.Visible = false;
            panel_newCom.Visible = false;

            DropDownListPeriod.SelectedItem = "12";
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
            Main.Columns.Add("Cat", typeof(List<String>));
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
            VehicleInsuDefinition formnew = new VehicleInsuDefinition();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
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

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            //*************************************************************************************************************************
            //string main_category = txtCate1.Text.Trim().ToUpper() == "" ? "%" : txtCate1.Text.Trim().ToUpper();
            //string sub_category = txtCate2.Text.Trim().ToUpper() == "" ? "%" : txtCate2.Text.Trim().ToUpper();
            //string model = txtModel.Text.Trim().ToUpper() == "" ? "%" : txtModel.Text.Trim().ToUpper();
            //string brand = txtBrand.Text.Trim().ToUpper() == "" ? "%" : txtBrand.Text.Trim().ToUpper();
            string itemCode = TextBoxItem.Text.Trim().ToUpper() == "" ? "%" : TextBoxItem.Text.Trim().ToUpper();
            //*************************************************************************************************************************
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
            GridAll_Items.DataSource = null;
            GridAll_Items.AutoGenerateColumns = false;
            GridAll_Items.DataSource = select_ITEMS_List;
            this.btnAllItem_Click(sender, e);

            GridViewFinal.DataSource = null;
            GridViewFinal.AutoGenerateColumns = false;
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
                    list.Add(dgvr.Cells[1].Value.ToString());
                }

            }
            return list;
        }
        private void ButtonAdd_Click(object sender, EventArgs e)
        {

            List<string> PC_list = GetSelectedPCList();
            if (PC_list.Count < 1)
            {
                MessageBox.Show("Please select profit center(s)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> ITEMS_list = GetSelectedItemsList();
            if (ITEMS_list.Count < 1)
            {
                MessageBox.Show("Please select item(s)", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
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

            if (_one == null || _one.Count() <= 0)
            {
                MessageBox.Show("Please select the insurance company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_two == null || _two.Count() <= 0)
            {
                MessageBox.Show("Please select the insurance policy", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (DataGridViewRow rC in _one)
            {
                foreach (DataGridViewRow rI in _two)
                {
                    DataRow dr = Main.NewRow();
                    List<MasterProfitCenter> pcs = new List<MasterProfitCenter>();
                    List<string> items = ITEMS_list;//new List<string>();
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
                    MasterOutsideParty mstCom = CHNLSVC.Sales.GetOutSidePartyDetails( Convert.ToString (rC.Cells["ic_code"].Value), string.Empty);
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
                        if (frm == Convert.ToDateTime(TextBoxFrom.Text) && to == Convert.ToDateTime(TextBoxTo.Text) && com == Convert.ToString(rC.Cells["ic_code"].Value) && poli == Convert.ToString(rI.Cells["ip_code"].Value) && period == DropDownListPeriod.Text && DropDownListSType.SelectedValue.ToString() == saletp)
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
                btnAddPc.Enabled = false;
                btnAddItem.Enabled = false;
                grvProfCents.Columns["chkPC"].ReadOnly = true;
                GridAll_Items.Columns["chkItm"].ReadOnly = true;


                btnAllPc.Enabled = false;
                btnNonPc.Enabled = false;
                btnClearPc.Enabled = false;
                btnAllItem.Enabled = false;
                btnNonItem.Enabled = false;
                btnClearItem.Enabled = false;
            }
            else
            {
                btnAddPc.Enabled = true;
                btnAddItem.Enabled = true;
                grvProfCents.Columns["chkPC"].ReadOnly = false;
                GridAll_Items.Columns["chkItm"].ReadOnly = false;

                btnAllPc.Enabled = true;
                btnNonPc.Enabled = true;
                btnClearPc.Enabled = true;
                btnAllItem.Enabled = true;
                btnNonItem.Enabled = true;
                btnClearItem.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Main.Rows.Count > 0)
            {
                if (MessageBox.Show("Are you sure to Save?", "Confirm Save", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                try
                {
                    //main table
                    for (int i = 0; i < Main.Rows.Count; i++)
                    {
                        List<MasterProfitCenter> pcsTem = (List<MasterProfitCenter>)Main.Rows[i][0];
                        List<string> itemsTem = (List<string>)Main.Rows[i][1];
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
                                    List<string> items = itemsTem.GetRange(k, 50);
                                    //send 200 pcs and 200 items at once
                                    CHNLSVC.General.SaveVehicalInsuranceDefinition(pcs, items, Convert.ToDateTime(Main.Rows[i][2]), Convert.ToDateTime(Main.Rows[i][3]), Main.Rows[i][4].ToString(), Convert.ToDecimal(Main.Rows[i]["Value"]), BaseCls.GlbUserID, Main.Rows[i]["Ins_Com"].ToString(), Main.Rows[i]["Ins_Pol"].ToString(), Convert.ToInt32(Main.Rows[i]["Ins_Period"].ToString()), CheckBoxMandatory.Checked);
                                    k = k + 50;
                                }
                                else
                                {
                                    List<string> items = itemsTem.GetRange(k, itemsTem.Count - k);
                                    CHNLSVC.General.SaveVehicalInsuranceDefinition(pcs, items, Convert.ToDateTime(Main.Rows[i][2]), Convert.ToDateTime(Main.Rows[i][3]), Main.Rows[i][4].ToString(), Convert.ToDecimal(Main.Rows[i]["Value"]), BaseCls.GlbUserID, Main.Rows[i]["Ins_Com"].ToString(), Main.Rows[i]["Ins_Pol"].ToString(), Convert.ToInt32(Main.Rows[i]["Ins_Period"].ToString()), CheckBoxMandatory.Checked);
                                    k = k + (itemsTem.Count - k);
                                }
                            }
                        }
                        // ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Records added sucessfully!');window.location='VehicalInsuranceDefinition.aspx'", true);
                        //MessageBox.Show("Successfully saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    MessageBox.Show("Successfully saved!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception er)
                {
                    // string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = '../Default.aspx';</script>";
                    // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                    MessageBox.Show("System Error Occur. : " + er.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                this.btnClear_Click(null, null);
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

        private void btnBrand_Click(object sender, EventArgs e)
        {

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
                // // DropDownListCat.Items.Clear();
                // // DropDownListCat.Items.Add(new ListItem("ALL", "%")); TODO
                // // DropDownListSCat.Items.Clear();
                ////  DropDownListIRange.Items.Clear();
                //  // DropDownListSCat.Items.Add(new ListItem("ALL", "%")); TODO
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
                // DropDownListSCat.Items.Clear();
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
                //  DropDownListBrand.DataBind();
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
                //   DropDownListCat.DataBind();
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
                //  DropDownListSCat.DataBind();
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

                // DropDownListIRange.DataBind();
                _brand = (!string.IsNullOrEmpty(DropDownListBrand.SelectedValue.ToString())) ? (DropDownListBrand.SelectedValue.ToString()) : "";
                _Mcat = (!string.IsNullOrEmpty(DropDownListCat.SelectedValue.ToString())) ? (DropDownListCat.SelectedValue.ToString()) : "";
                _Scat = (!string.IsNullOrEmpty(DropDownListSCat.SelectedValue.ToString())) ? (DropDownListSCat.SelectedValue.ToString()) : "";
                _Range = (!string.IsNullOrEmpty(DropDownListIRange.SelectedValue.ToString())) ? (DropDownListIRange.SelectedValue.ToString()) : "";

            }
        }

        private void DropDownListBrand_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadCombos(1);
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                //case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                //    {
                //        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                //        break;
                //    }

                //case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                //    {
                //        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator);
                //        break;
                //    }
                //case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                //    {

                //        paramsText.Append(txtCate1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator);
                //        break;
                //    }

                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.Brand:
                //    {
                //        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                //        break;
                //    }
                //case CommonUIDefiniton.SearchUserControlType.Model:
                //    {
                //        paramsText.Append("");
                //        break;
                //    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        private void btnItem_Click(object sender, EventArgs e)
        {
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

        private void DropDownListCat_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadCombos(2);
        }

        private void DropDownListSCat_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadCombos(3);
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
                    btnAddPc.Enabled = false;
                    btnAddItem.Enabled = false;
                    grvProfCents.Columns["chkPC"].ReadOnly = true;
                    GridAll_Items.Columns["chkItm"].ReadOnly = true;

                    btnAllPc.Enabled = false;
                    btnNonPc.Enabled = false;
                    btnClearPc.Enabled = false;
                    btnAllItem.Enabled = false;
                    btnNonItem.Enabled = false;
                    btnClearItem.Enabled = false;
                }
                else
                {
                    btnAddPc.Enabled = true;
                    btnAddItem.Enabled = true;
                    grvProfCents.Columns["chkPC"].ReadOnly = false;
                    GridAll_Items.Columns["chkItm"].ReadOnly = false;
                    panel_itms.Enabled = true;
                    panel_pc.Enabled = true;

                    btnAllPc.Enabled = true;
                    btnNonPc.Enabled = true;
                    btnClearPc.Enabled = true;
                    btnAllItem.Enabled = true;
                    btnNonItem.Enabled = true;
                    btnClearItem.Enabled = true;
                }
            }
        }

    }
}
