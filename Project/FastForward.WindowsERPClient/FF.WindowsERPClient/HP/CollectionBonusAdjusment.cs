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

namespace FF.WindowsERPClient.HP
{
    public partial class CollectionBonusAdjusment : Base
    {
        bool canAdd;
        List<FF.BusinessObjects.CollectionBonusAdjusment> CollectionBonusAdjusmentList;
        List<FF.BusinessObjects.CollectionBonusAdjusment> PreviousAdjusment;
        List<FF.BusinessObjects.CollectionBonusAdjusment> CurrentAdjusmentList;

        public CollectionBonusAdjusment()
        {
            InitializeComponent();
            canAdd = true;
            CollectionBonusAdjusmentList = new List<FF.BusinessObjects.CollectionBonusAdjusment>();
            PreviousAdjusment = new List<FF.BusinessObjects.CollectionBonusAdjusment>();
            CurrentAdjusmentList = new List<BusinessObjects.CollectionBonusAdjusment>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int effect = 0;
            foreach (FF.BusinessObjects.CollectionBonusAdjusment col in CollectionBonusAdjusmentList) {
                effect = effect + CHNLSVC.Sales.SaveCollectionBonusAdjusment(col);
            }
            if (effect > 0)
            {
                MessageBox.Show("Sucessfully saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAll();
            }
            else {
                MessageBox.Show("Nothing Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
           
            ClearAll();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearAll()
        {
            dataGridViewCurrentAdj.AutoGenerateColumns = false;
            dataGridViewPreviousAdj.AutoGenerateColumns = false;
            txtAccountNo.Text = "";
            txtArrarsAmount.Text = "";
            txtDG.Text = "";
            txtDiriya.Text = "";
            txtDispute.Text = "";
            txtGracePeriodSettlement.Text = "";
            txtLocation.Text = "";
            txtLOD.Text = "";
            txtNetArrears.Text = "";
            txtOther.Text = "";
            txtService.Text = "";
            txtTotalAdjusment.Text = "";

            lblLocation.Text = "";

            txtDispute.Enabled = true;
            txtDiriya.Enabled = true;
            txtLOD.Enabled = true;
            txtService.Enabled = true;
            txtOther.Enabled = true;
            txtDG.Enabled = true;
            canAdd = true;
            
            CurrentAdjusmentList = new List<BusinessObjects.CollectionBonusAdjusment>();
            PreviousAdjusment = new List<BusinessObjects.CollectionBonusAdjusment>();
            CollectionBonusAdjusmentList = new List<FF.BusinessObjects.CollectionBonusAdjusment>();

            dataGridViewPreviousAdj.DataSource = null;
            txtLocation.Text = BaseCls.GlbUserDefProf;
            ValidateProfitCenter(txtLocation.Text);
            dateTimePickerMonth_ValueChanged(null, null);
            List<FF.BusinessObjects.CollectionBonusAdjusment> tem = CHNLSVC.Sales.GetCollectionBonusAdjusment(BaseCls.GlbUserComCode, txtLocation.Text, "", "ALL");
            if (tem != null && tem.Count > 0)
            {
                CurrentAdjusmentList.AddRange(tem);
            }
            LoadCurrentGrid();
        }

        private void CollectionBonusAdjusment_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewCurrentAdj.AutoGenerateColumns = false;
                dataGridViewPreviousAdj.AutoGenerateColumns = false;
                txtLocation.Select();
                txtLocation.Text = BaseCls.GlbUserDefProf;
                ValidateProfitCenter(txtLocation.Text);
                dateTimePickerMonth_ValueChanged(null, null);

                List<FF.BusinessObjects.CollectionBonusAdjusment> tem = CHNLSVC.Sales.GetCollectionBonusAdjusment(BaseCls.GlbUserComCode, txtLocation.Text, "", "ALL");
                if (tem != null && tem.Count > 0) {
                    CurrentAdjusmentList.AddRange(tem);
                }
                LoadCurrentGrid();
            }
            catch (Exception ex) { }
        }

        private void txtLocation_Leave(object sender, EventArgs e)
        {
            if (txtLocation.Text != "")
            {
                if (!ValidateProfitCenter(txtLocation.Text)) {
                    MessageBox.Show("Invalid Profit Center code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtLocation.Text = "";
                    txtLocation.Focus();
                }
            }
            if (txtLocation.Text != "" && txtAccountNo.Text != "")
                LoadAccountDetails();
        }

        private bool ValidateProfitCenter(string code)
        {
            MasterProfitCenter pc = CHNLSVC.General.GetPCByPCCode(BaseCls.GlbUserComCode, code);
            if (pc != null)
            {
                lblLocation.Text = pc.Mpc_desc;
                return true;
            }
            else
                return false;
        }

        #region search

        private void buttonSearchLocation_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
            DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtLocation;
            _CommonSearch.txtSearchbyword.Text = txtLocation.Text;
            _CommonSearch.ShowDialog();
            txtLocation.Select();
        }

        private void buttonSearchAccount_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
            DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtAccountNo;
            _CommonSearch.ShowDialog();
            if (txtAccountNo.Text != "")
            {
                LoadPreviousAdjusment(); 
                LoadAccountDetails();
            }
        }

        private void LoadAccountDetails()
        {
            DateTime _date=CHNLSVC.Security.GetServerDateTime();

            //CHECK FOR EXISTING RECORDS
            List<FF.BusinessObjects.CollectionBonusAdjusment> list = new List<BusinessObjects.CollectionBonusAdjusment>();
            if (PreviousAdjusment != null && PreviousAdjusment.Count > 0)
            {
                list = (from _res in PreviousAdjusment
                        where _res.Hcba_com == BaseCls.GlbUserComCode && _res.Hcba_pc == txtLocation.Text && _res.Hcba_bonus_dt == new DateTime(dateTimePickerMonth.Value.Year, dateTimePickerMonth.Value.Month, 1)
                        select _res).ToList<FF.BusinessObjects.CollectionBonusAdjusment>();
            }
            if (list.Count > 0)
            {
                MessageBox.Show("Previous records exists for Account No: "+txtAccountNo.Text+" for "+dateTimePickerMonth.Value.ToString("MMM/yyyy"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                canAdd = false;
                return;
            }

            //01.CHECK FOR ACCOUNT ACTIVE
            DataTable _dt = CHNLSVC.Sales.GetHp_ActiveAccounts(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, _date, _date, _date, txtAccountNo.Text, "A");
            if (_dt.Rows.Count > 0)
            {
                //02.CHECK HPT_COL_BONUS
                DataTable _col=CHNLSVC.Sales.GetCollectionBonusByDate(BaseCls.GlbUserComCode,txtLocation.Text,new DateTime(dateTimePickerMonth.Value.Year,dateTimePickerMonth.Value.Month,1));
                if (_col.Rows.Count > 0)
                {
                    //03.GET ARREARS
                    DateTime lastDay = new DateTime(dateTimePickerMonth.Value.Year, dateTimePickerMonth.Value.Month, DateTime.DaysInMonth(dateTimePickerMonth.Value.Year, dateTimePickerMonth.Value.Month));
                    HpAccountSummary summary = new HpAccountSummary();
                    decimal Arr1;
                    decimal Arr2;
                    Decimal arrVl = calculateInsDue(txtAccountNo.Text, dateTimePickerMonth.Value, _date, summary, txtLocation.Text, "INSUR", out Arr1, "");
                    Decimal arrV2 = calculateInsDue(txtAccountNo.Text, dateTimePickerMonth.Value, _date, summary, txtLocation.Text, "INSUR", out Arr2, "");
                    Decimal Arrears = HpAccountSummary.getArears(txtAccountNo.Text, summary, txtLocation.Text, _date.Date, lastDay, _date.Date);
                    decimal min = HpAccountSummary.Get_Minimum_Arrears(txtAccountNo.Text, lastDay, _date.Date, txtLocation.Text);
                    if (Arrears > 0)
                    {
                        if (Arrears >= min)
                        {
                            txtArrarsAmount.Text = Arrears.ToString();
                        }
                        else {
                            MessageBox.Show("No arrears for the account: " + txtAccountNo.Text + " as at " + dateTimePickerMonth.Value.Date, "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            canAdd = false;
                            return;
                        }

                       //04.DG1 CALCULATION
                        HpAccount acc = CHNLSVC.Sales.GetHP_Account_onAccNo(txtAccountNo.Text);
                        if (acc != null) {
                            
                                DataTable _arrears = CHNLSVC.Sales.GetArrearsScheme(_date, acc.Hpa_sch_cd);
                                if (_arrears.Rows.Count > 0)
                                {
                                    int _numofmonths = Convert.ToInt32(_arrears.Rows[0]["HARS_NO_RNT"]);
                                    DateTime _finalDate=new DateTime(dateTimePickerMonth.Value.Year,dateTimePickerMonth.Value.AddMonths(_numofmonths).Month,DateTime.DaysInMonth(dateTimePickerMonth.Value.Year,dateTimePickerMonth.Value.AddMonths(_numofmonths).Month));

                                    List<HpSheduleDetails> temp = CHNLSVC.Sales.GetHpAccountSchedule(txtAccountNo.Text);
                                    List<HpSheduleDetails> _hpSchedule = (from _res in temp
                                                                          where _res.Hts_due_dt.Month == _finalDate.Month && _res.Hts_due_dt.Year == _finalDate.Year
                                                                          select _res).ToList<HpSheduleDetails>();
                                    txtDG.Text = _hpSchedule.Sum(x => x.Hts_rnt_val).ToString(); ;

                                }
                                else { 
                                 MessageBox.Show("No Arrears Schemes Defined", "Wraning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                //05.GRACE PERIOD SETTLEMENT
                                DateTime _lastdayofnextmonth = new DateTime(dateTimePickerMonth.Value.Year, dateTimePickerMonth.Value.AddMonths(1).Month, DateTime.DaysInMonth(dateTimePickerMonth.Value.Year, dateTimePickerMonth.Value.Month));
                                if (acc.Hpa_cls_dt <= dateTimePickerGraceDate.Value.Date || (acc.Hpa_rv_dt <= dateTimePickerGraceDate.Value.Date && acc.Hpa_rls_dt > dateTimePickerGraceDate.Value.Date))
                                {
                                    txtGracePeriodSettlement.Text = (Convert.ToDecimal(txtArrarsAmount.Text) - Convert.ToDecimal(txtDG.Text)).ToString();
                                }
                                else {
                                    decimal arrears = HpAccountSummary.getArears(txtAccountNo.Text, summary, txtLocation.Text, _date.Date, _lastdayofnextmonth, dateTimePickerGraceDate.Value.Date.Date);
                                    decimal minarrears = HpAccountSummary.Get_Minimum_Arrears(txtAccountNo.Text, _lastdayofnextmonth, dateTimePickerGraceDate.Value.Date, txtLocation.Text);
                                    if (arrears - Convert.ToDecimal(txtDG.Text) >= minarrears) {
                                        txtGracePeriodSettlement.Text = "0";
                                    }
                                    else{
                                        txtGracePeriodSettlement.Text = (arrears - Convert.ToDecimal(txtDG.Text)).ToString();
                                    }
                                }
                        }
                        else {
                            MessageBox.Show("Invalid Account Number", "Wraning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            canAdd = false;
                            return;
                        }
                    }
                    else {
                        MessageBox.Show("No arrears for the account: " + txtAccountNo.Text + " as at " + lastDay.Date, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else {
                    MessageBox.Show("Data for the selected location is confirmed for the month " + dateTimePickerMonth.Value.ToString("MMMM") + " You can not modify.", "Wraning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    canAdd = false;
                    return;
                }
            }
            else {
                MessageBox.Show("Account not active", "Wraning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                canAdd = false;
                return;
            }

            //SetNetArrears();
        }

        private void SetNetArrears()
        {
            decimal val;
            if (!decimal.TryParse(txtService.Text, out val)) {
                MessageBox.Show("Please enter service amount in number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtOther.Text, out val))
            {
                MessageBox.Show("Please enter other amount in number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtNetArrears.Text, out val))
            {
                MessageBox.Show("Please enter net arrears amount in number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtDispute.Text, out val))
            {
                MessageBox.Show("Please enter dispute amount in number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtDiriya.Text, out val))
            {
                MessageBox.Show("Please enter insurance amount in number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtTotalAdjusment.Text, out val))
            {
                MessageBox.Show("Please enter total adjusment amount in number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtAccountNo.Text) || string.IsNullOrEmpty(txtLocation.Text)) {
                return;
            }

            txtTotalAdjusment.Text = (Convert.ToDecimal(txtDispute.Text) + Convert.ToDecimal(txtLOD.Text) + Convert.ToDecimal(txtDiriya.Text) + Convert.ToDecimal(txtService.Text) + Convert.ToDecimal(txtOther.Text) + Convert.ToDecimal(txtGracePeriodSettlement.Text) + Convert.ToDecimal(txtDG.Text)).ToString();
            txtNetArrears.Text = (Convert.ToDecimal(txtArrarsAmount.Text) - Convert.ToDecimal(txtTotalAdjusment.Text)).ToString();
            if (Convert.ToDecimal(txtNetArrears.Text) < 0)
            {
                MessageBox.Show("Net Arrears Cannot be less than zero", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtDispute.Enabled = true;
                txtDiriya.Enabled = true;
                txtLOD.Enabled = true;
                txtService.Enabled = true;
                txtOther.Enabled = true;
                txtDG.Enabled = true;
            }
            else if (Convert.ToDecimal(txtNetArrears.Text) <= 0 && Convert.ToDecimal(txtGracePeriodSettlement.Text) == 0)
            {
                txtDispute.Enabled = false;
                txtDiriya.Enabled = false;
                txtLOD.Enabled = false;
                txtService.Enabled = false;
                txtOther.Enabled = false;
                txtDG.Enabled = false;
            }
            else {
                txtDispute.Enabled = true;
                txtDiriya.Enabled = true;
                txtLOD.Enabled = true;
                txtService.Enabled = true;
                txtOther.Enabled = true;
                txtDG.Enabled = true;
            }
        }

        private Decimal calculateInsDue(string AccNo, DateTime hadd_ars_dt, DateTime hadd_sup_dt, HpAccountSummary SUMMARY, string selectedPC, string type_, out Decimal InsDue, string receiptNo)
        {
            Decimal temp_arrears = 0;
            Decimal TotDue = 0; //call proc 1 (sum(RNT_VAL));
            //Decimal settlement = 0;
            Decimal Arrears = 0;
            //Decimal overLimit = 0;
            DateTime lastDayOfCURMonth = GetLastDayOfPreviousMonth(hadd_ars_dt).AddMonths(1);//get last day of previous months and add 2 months to it.
            //  TotDue = SUMMARY.getTotDue(AccNo, hadd_ars_dt);
            TotDue = SUMMARY.getDueOnType(AccNo, hadd_ars_dt, type_, receiptNo, hadd_sup_dt);//hadd_sup_dt=receipt date //03/09/2012 //NEXT MONTH LAST DATE
            //settlement = SUMMARY.getArrearsSettlement(AccNo, hadd_sup_dt);//change this also
            temp_arrears = TotDue;//- settlement;
            InsDue = temp_arrears;//ASSIGN TO THE PROPERTY (out parameter)
            //---------------------------
            return Arrears;
        }

        public static DateTime GetLastDayOfPreviousMonth(DateTime startDate)
        {

            DateTime lastDayLastMonth = new DateTime(startDate.Year, startDate.Month, 1);
            lastDayLastMonth = lastDayLastMonth.AddDays(-1);

            startDate = lastDayLastMonth;

            return startDate;
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
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtLocation.Text + seperator + "A" + seperator);
                        break;
                    }
            }
            return paramsText.ToString();
        }

        private void txtLocation_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonSearchLocation_Click(null, null);
        }

        #endregion

        #region key down events

        private void txtLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                dateTimePickerMonth.Focus();
            }
            if (e.KeyCode == Keys.F2) {
                buttonSearchLocation_Click(null, null);
            }
        }

        private void dateTimePickerMonth_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter){
                dateTimePickerGraceDate.Focus();
            }
        }

        private void dateTimePickerGraceDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                txtAccountNo.Focus();
            }
        }

        private void txtAccountNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                txtGracePeriodSettlement.Focus();
            }
            if (e.KeyCode == Keys.F2) {
                buttonSearchAccount_Click(null, null);
            }
        }

        private void txtGracePeriodSettlement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                txtArrarsAmount.Focus();
            }
        }

        private void txtArrarsAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                txtDispute.Focus();
            }
        }

        private void txtDispute_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                txtService.Focus();
            }
        }

        private void txtService_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                txtTotalAdjusment.Focus();
            }
        }

        private void txtTotalAdjusment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                txtLOD.Focus();
            }
        }

        private void txtLOD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                txtDG.Focus();
            }
        }

        private void txtDG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                txtNetArrears.Focus();
            }
        }

        private void txtNetArrears_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                txtDiriya.Focus();
            }
        }

        private void txtDiriya_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                txtOther.Focus();
            }
        }

        private void txtOther_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                buttonAdd.Focus();
            }
        }


        #endregion

        private void dateTimePickerMonth_ValueChanged(object sender, EventArgs e)
        {
            DateTime _serverDate=CHNLSVC.Security.GetServerDateTime();
            dateTimePickerGraceDate.Value = _serverDate;
            DataTable hierchy_tbl = new DataTable();
            HpAccountSummary SUMMARY = new HpAccountSummary();
            hierchy_tbl = SUMMARY.getHP_Hierachy(txtLocation.Text);//call sp_get_hp_hierachy
            if (hierchy_tbl.Rows.Count > 0)
            {
                foreach (DataRow da in hierchy_tbl.Rows)
                {
                    string party_tp = Convert.ToString(da["MPI_CD"]);
                    string party_cd = Convert.ToString(da["MPI_VAL"]);
                    DataTable info_tbl = new DataTable();
                    List<ArrearsDateDef> _date = CHNLSVC.Financial.Get_ArrearsDateDef(BaseCls.GlbUserComCode, new DateTime(dateTimePickerMonth.Value.Year, dateTimePickerMonth.Value.Month, DateTime.DaysInMonth(dateTimePickerMonth.Value.Year, dateTimePickerMonth.Value.Month)), party_tp, party_cd);
                    if (_date != null && _date.Count > 0)
                    {
                        dateTimePickerGraceDate.Value = _date[0].Hadd_grc_dt;
                        return;
                    }
                }
            }
            if (dateTimePickerGraceDate.Value == _serverDate)
            {
                MessageBox.Show("No Grace date defined","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                canAdd = false;
            }
            else
                canAdd = true;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            SetNetArrears();
            if (canAdd && ValidateValues())
            {
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
               FF.BusinessObjects.CollectionBonusAdjusment _collectionBonus = new FF.BusinessObjects.CollectionBonusAdjusment();
               _collectionBonus.Hcba_pc = txtLocation.Text;
               _collectionBonus.Hcba_com = BaseCls.GlbUserComCode;
               _collectionBonus.Hcba_bonus_dt = new DateTime(dateTimePickerMonth.Value.Year, dateTimePickerMonth.Value.Month, 1);
               _collectionBonus.Hcba_dispute = Convert.ToDecimal(txtDispute.Text);
               _collectionBonus.Hcba_grce_sett = Convert.ToDecimal(txtGracePeriodSettlement.Text);
               _collectionBonus.Hcba_insu = Convert.ToDecimal(txtDiriya.Text);
               _collectionBonus.Hcba_lod = Convert.ToDecimal(txtLOD.Text);
               _collectionBonus.Hcba_net_ars = Convert.ToDecimal(txtNetArrears.Text);
               _collectionBonus.Hcba_other = Convert.ToDecimal(txtOther.Text);
               _collectionBonus.Hcba_service = Convert.ToDecimal(txtService.Text);
               _collectionBonus.Hcba_tot_adj = Convert.ToDecimal(txtTotalAdjusment.Text);
               _collectionBonus.Hcba_grce_dt = dateTimePickerGraceDate.Value.Date;
               _collectionBonus.Hcba_ars_amt = Convert.ToDecimal(txtArrarsAmount.Text);
               _collectionBonus.Hcba_ar_1mon = Convert.ToDecimal(txtDG.Text);
               _collectionBonus.Hcba_acc = txtAccountNo.Text;
               _collectionBonus.Hcba_cre_by = BaseCls.GlbUserID;
               _collectionBonus.Hcba_cre_dt = _date;

               CollectionBonusAdjusmentList.Add(_collectionBonus);
               CurrentAdjusmentList.Add(_collectionBonus);
               LoadCurrentGrid();
              
            }
            else {
                MessageBox.Show("Can not add Adjusment", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private bool ValidateValues()
        {
            if (string.IsNullOrEmpty(txtLocation.Text)) {
                MessageBox.Show("Please select Profit Center code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (string.IsNullOrEmpty(txtAccountNo.Text)) {
                MessageBox.Show("Please select Account No", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void LoadCurrentGrid()
        {
            var source = new BindingSource();
            source.DataSource = CurrentAdjusmentList;
            dataGridViewCurrentAdj.DataSource = source;
        }

        private void txtAccountNo_Leave(object sender, EventArgs e)
        {
            if (txtAccountNo.Text != "") {
                HpAccount acc = CHNLSVC.Sales.GetHP_Account_onAccNo(txtAccountNo.Text);
                if (acc != null) {
                    LoadPreviousAdjusment();
                    LoadAccountDetails();
                }
            }
            if (txtLocation.Text != "" && txtAccountNo.Text != "")
                LoadAccountDetails();
        }

        private void LoadPreviousAdjusment()
        {
            PreviousAdjusment = CHNLSVC.Sales.GetCollectionBonusAdjusment(BaseCls.GlbUserComCode, txtLocation.Text, txtAccountNo.Text,"NALL");
            var source = new BindingSource();
            source.DataSource = PreviousAdjusment;
            dataGridViewPreviousAdj.DataSource = source;
        }

        private void txtAccountNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonSearchAccount_Click(null, null);
        }

        private void txtDispute_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtGracePeriodSettlement.Text) > 0)
            {
                SetGracePeriodSettlement(txtDispute);
            }
            SetNetArrears();
        }

        private void SetGracePeriodSettlement(TextBox txtDispute)
        {
            if (Convert.ToDecimal(txtGracePeriodSettlement.Text) > 0)
            {
                txtGracePeriodSettlement.Text = (Convert.ToDecimal(txtGracePeriodSettlement.Text) - (Convert.ToDecimal(txtLOD.Text) + Convert.ToDecimal(txtDiriya.Text) + Convert.ToDecimal(txtService.Text) + Convert.ToDecimal(txtOther.Text))).ToString();
            }
            if (Convert.ToDecimal(txtGracePeriodSettlement.Text) < 0)
            {
                MessageBox.Show("Grace Period Settlement could not be less than 0", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtLOD_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtGracePeriodSettlement.Text) > 0)
            {
                SetGracePeriodSettlement(txtDispute);
            }
            SetNetArrears();
        }

        private void txtDiriya_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtGracePeriodSettlement.Text) > 0)
            {
                SetGracePeriodSettlement(txtDispute);
            }
            SetNetArrears();
        }

        private void txtService_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtGracePeriodSettlement.Text) > 0)
            {
                SetGracePeriodSettlement(txtDispute);
            }
            SetNetArrears();
        }

        private void txtOther_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtGracePeriodSettlement.Text) > 0)
            {
                SetGracePeriodSettlement(txtDispute);
            }
            SetNetArrears();
        }
        
    }
}
