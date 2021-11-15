using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Finance
{
    public partial class CollectionBonusVoucherUpload : Base
    {
        DateTime _final_date;
        DateTime fin_new_date;
        Deposit_Bank_Pc_wise objCollection_bonus;
        Deposit_Bank_Pc_wise _objgrid;
        List<Deposit_Bank_Pc_wise> _lstgrid;
        List<Deposit_Bank_Pc_wise> _lstCollection_bonus;
        string profitcenter;
        string vouno;


        public CollectionBonusVoucherUpload()
        {
            InitializeComponent();
            bindData();

        }

        protected void bindData()
        {


            cmbYear.Items.Add("2012");
            cmbYear.Items.Add("2013");
            cmbYear.Items.Add("2014");
            cmbYear.Items.Add("2015");
            cmbYear.Items.Add("2016");
            cmbYear.Items.Add("2017");
            cmbYear.Items.Add("2018");

            int _Year = DateTime.Now.Year;
            cmbYear.SelectedIndex = _Year % 2013 + 1;

            cmbMonth.Items.Add("January");
            cmbMonth.Items.Add("February");
            cmbMonth.Items.Add("March");
            cmbMonth.Items.Add("April");
            cmbMonth.Items.Add("May");
            cmbMonth.Items.Add("June");
            cmbMonth.Items.Add("July");
            cmbMonth.Items.Add("August");
            cmbMonth.Items.Add("September");
            cmbMonth.Items.Add("October");
            cmbMonth.Items.Add("November");
            cmbMonth.Items.Add("December");
            cmbMonth.SelectedIndex = -1;


        }



        public void bindPanelData()
        {

            cmbYearNew.Items.Add("2012");
            cmbYearNew.Items.Add("2013");
            cmbYearNew.Items.Add("2014");
            cmbYearNew.Items.Add("2015");
            cmbYearNew.Items.Add("2016");
            cmbYearNew.Items.Add("2017");
            cmbYearNew.Items.Add("2018");

            int _Year = DateTime.Now.Year;
            cmbYearNew.SelectedIndex = _Year % 2013 + 1;

            cmbMonthnew.Items.Add("January");
            cmbMonthnew.Items.Add("February");
            cmbMonthnew.Items.Add("March");
            cmbMonthnew.Items.Add("April");
            cmbMonthnew.Items.Add("May");
            cmbMonthnew.Items.Add("June");
            cmbMonthnew.Items.Add("July");
            cmbMonthnew.Items.Add("August");
            cmbMonthnew.Items.Add("September");
            cmbMonthnew.Items.Add("October");
            cmbMonthnew.Items.Add("November");
            cmbMonthnew.Items.Add("December");
            cmbMonthnew.SelectedIndex = -1;


        }



        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbYear.Text))
            {
                MessageBox.Show("Select the year");
                return;
            }
            if (string.IsNullOrEmpty(cmbMonth.Text))
            {
                MessageBox.Show("Select the Month");
                return;
            }

            txtFileName.Text = string.Empty;
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.ShowDialog();
            string[] _obj = openFileDialog1.FileName.Split('\\');
            txtFileName.Text = openFileDialog1.FileName;




        }

        private List<string> _itemLst = null;

        private void btnUpload_Click(object sender, EventArgs e)
        {
            string _msg = string.Empty;

            if (string.IsNullOrEmpty(txtFileName.Text))
            {
                MessageBox.Show("Please select upload file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFileName.Clear();
                txtFileName.Focus();
                return;
            }

            System.IO.FileInfo _fileObj = new System.IO.FileInfo(txtFileName.Text);

            if (_fileObj.Exists == false)
            {
                MessageBox.Show("Selected file does not exist at the following path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFileName.Focus();
                return;
            }

            string _extension = _fileObj.Extension;
            string _conStr = string.Empty;

            if (_extension.ToUpper() == ".XLS")
            {
                _conStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtFileName.Text + "; Extended Properties='Excel 8.0;HDR=No;'";
            }
            else if (_extension.ToUpper() == ".XLSX")
            {
                _conStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtFileName.Text + ";Extended Properties='Excel 12.0 xml;HDR=No;'";
            }
            else
            {
                MessageBox.Show("Please Select valid Ms Excel File.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Cursor.Current = Cursors.WaitCursor;

            _conStr = String.Format(_conStr, txtFileName.Text, "NO");
            OleDbConnection _connExcel = new OleDbConnection(_conStr);
            OleDbCommand _cmdExcel = new OleDbCommand();
            OleDbDataAdapter _oda = new OleDbDataAdapter();
            DataTable _dt = new DataTable();
            _cmdExcel.Connection = _connExcel;

            //Get the name of First Sheet
            _connExcel.Open();
            DataTable _dtExcelSchema;
            _dtExcelSchema = _connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string _sheetName = _dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            _connExcel.Close();

            _connExcel.Open();
            _cmdExcel.CommandText = "SELECT * From [" + _sheetName + "]";
            _oda.SelectCommand = _cmdExcel;
            _oda.Fill(_dt);
            _connExcel.Close();
            _itemLst = new List<string>();
            StringBuilder _errorLst = new StringBuilder();
            if (_dt == null || _dt.Rows.Count <= 0) { MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            if (_dt.Rows.Count > 0)
            {
                DataTable dtFiltor = _dt.Rows.Cast<DataRow>().Where(
                row => !row.ItemArray.All(field => field is System.DBNull)).CopyToDataTable();

                _lstgrid = new List<Deposit_Bank_Pc_wise>();


                foreach (DataRow _dr in dtFiltor.Rows)
                {
                    if (string.IsNullOrEmpty(_dr[1].ToString())) continue;

                    bool chk_pc = CHNLSVC.General.CheckProfitCenter(BaseCls.GlbUserComCode, _dr[1].ToString().Trim());

                    if (chk_pc == false)
                    {
                        if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("invalid Location - " + _dr[1].ToString());
                        else _errorLst.Append(" and invalid Location - " + _dr[1].ToString());
                        continue;
                    }
                    string _pc_desc = CHNLSVC.General.Get_ProfitCenter_desc(BaseCls.GlbUserComCode, _dr[1].ToString().Trim());
                    double _gross_bonus;
                    double _gross = Convert.ToDouble(_dr[3]);
                    if (_gross < 0)
                    {
                        _gross_bonus = _gross * -1;
                    }
                    else
                    {
                        _gross_bonus = _gross;
                    }

                    _dr[3] = Math.Round(_gross_bonus, 2);

                    double _deduction;
                    double _ded = Convert.ToDouble(_dr[6]);
                    if (_ded < 0)
                    {
                        _deduction = _ded * -1;
                    }
                    else
                    {
                        _deduction = _ded;
                    }

                    _dr[6] = Math.Round(_deduction, 2);

                    double _refund;
                    double _ref = Convert.ToDouble(_dr[9]);
                    if (_ref < 0)
                    {
                        _refund = _ref * -1;
                    }
                    else
                    {
                        _refund = _ref;
                    }

                    _dr[9] = Math.Round(_refund, 2);

                    double _netBonus;
                    double _net = Convert.ToDouble(_dr[10]);
                    if (_ref < 0)
                    {
                        _netBonus = _net * -1;
                    }
                    else
                    {
                        _netBonus = _net;
                    }

                    _dr[10] = Math.Round(_netBonus, 2);

                    _objgrid = new Deposit_Bank_Pc_wise();

                    _objgrid.Voucher_no = _dr[0].ToString();
                    _objgrid.Profit_center = _dr[1].ToString().Trim();
                    _objgrid.BankName = _pc_desc;
                    _objgrid.Gross_bonus_amt = Convert.ToDouble(_dr[3]);
                    _objgrid.Deduction = Convert.ToDouble(_dr[6]);
                    _objgrid.Refund = Convert.ToDouble(_dr[9]);
                    _objgrid.Net_bonus = Convert.ToDouble(_dr[10]);
                    _lstgrid.Add(_objgrid);

                }
                var sum_gross = from g in _lstgrid
                                select g.Gross_bonus_amt;
                double _total_gross = Math.Round(sum_gross.Sum(), 2);

                //var sum_deduction = dtFiltor.AsEnumerable().Sum(dr => dr.Field<double>("F7"));
                //double _total_ded = Math.Round(sum_deduction, 2);
                var sum_deduction = from d in _lstgrid
                                    select d.Deduction;
                double _total_ded = Math.Round(sum_deduction.Sum(), 2);

                var sum_refund = from r in _lstgrid
                                 select r.Refund;

                double _total_ref = Math.Round(sum_refund.Sum(), 2);
                var sum_net = from n in _lstgrid
                              select n.Net_bonus;
                double _total_net = Math.Round(sum_net.Sum(), 2);


                Cursor.Current = Cursors.Default;

                if (!string.IsNullOrEmpty(_errorLst.ToString()))
                {
                    if (MessageBox.Show("Following Profit Centers are found when checking the file.\n" + _errorLst.ToString() + ".\n Do you need to continue anyway?", "Discrepancies", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        dgvCollectionBonus.AutoGenerateColumns = false;
                        dgvCollectionBonus.DataSource = _lstgrid;
                        lblGrossBonus.Text = _total_gross.ToString("0.00");
                        lblDeduction.Text = _total_ded.ToString("0.00");
                        lblRefund.Text = _total_ref.ToString("0.00");
                        lblNetBonus.Text = _total_net.ToString("0.00");
                        //cmbMonth.Enabled = false;
                        //cmbYear.Enabled = false;
                        btnBrowse.Enabled = false;
                        btnUpload.Enabled = false;
                    }
                }
                else
                {
                    dgvCollectionBonus.AutoGenerateColumns = false;
                    dgvCollectionBonus.DataSource = _lstgrid;
                    lblGrossBonus.Text = _total_gross.ToString("0.00");
                    lblDeduction.Text = _total_ded.ToString("0.00");
                    lblRefund.Text = _total_ref.ToString("0.00");
                    lblNetBonus.Text = _total_net.ToString("0.00");
                    //cmbMonth.Enabled = false;
                    //cmbYear.Enabled = false;
                    btnBrowse.Enabled = false;
                    btnUpload.Enabled = false;
                }

            }





        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMonth.SelectedIndex = -1;
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbYear.Text))
            {
                MessageBox.Show("Select the year");
                return;
            }

            if (cmbMonth.Text != "")
            {
                string _year = cmbYear.Text;
                string _final_yr = Microsoft.VisualBasic.Strings.Right(_year, 2);
                string _month = cmbMonth.Text.ToUpper();
                string _fi_month = _month.Substring(0, 3);
                string _date = "01";

                string final_date_time = _date + "-" + _fi_month + "-" + _final_yr;
                _final_date = Convert.ToDateTime(final_date_time);
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFilds();
        }

        private void ClearFilds()
        {
            dgvCollectionBonus.DataSource = null;
            cmbMonth.SelectedIndex = -1;
            txtFileName.Text = "";
            btnUpload.Enabled = true;
            btnBrowse.Enabled = true;
            lblGrossBonus.Text = "0.00";
            lblDeduction.Text = "0.00";
            lblNetBonus.Text = "0.00";
            lblRefund.Text = "0.00";
        }


        private List<Deposit_Bank_Pc_wise> fillToCollectionBonus()
        {
            _lstCollection_bonus = new List<Deposit_Bank_Pc_wise>();
            StringBuilder _errorlist = new StringBuilder();
            foreach (DataGridViewRow dgvr in dgvCollectionBonus.Rows)
            {

                bool _isAvailable = CHNLSVC.Sales.Check_Available_Col_Bonus(BaseCls.GlbUserComCode, dgvr.Cells["clmShowroomCode"].Value.ToString(), dgvr.Cells["clmVoucherNo"].Value.ToString(), _final_date);
                if (_isAvailable == true)
                {
                    if (string.IsNullOrEmpty(_errorlist.ToString())) _errorlist.Append("Already Saved - Voucher No: " + dgvr.Cells["clmVoucherNo"].Value.ToString());
                    else _errorlist.Append(" and Already Saved - Voucher No: - " + dgvr.Cells["clmVoucherNo"].Value.ToString());
                    continue;
                }
                else
                {
                    objCollection_bonus = new Deposit_Bank_Pc_wise();
                    objCollection_bonus.Company = BaseCls.GlbUserComCode;
                    objCollection_bonus.Adj_Date = _final_date;
                    objCollection_bonus.Profit_center = dgvr.Cells["clmShowroomCode"].Value.ToString();
                    objCollection_bonus.Voucher_no = dgvr.Cells["clmVoucherNo"].Value.ToString();
                    objCollection_bonus.Gross_bonus_amt = Convert.ToDouble(dgvr.Cells["clmGross"].Value);
                    objCollection_bonus.Deduction = Convert.ToDouble(dgvr.Cells["clmDeduction"].Value);
                    objCollection_bonus.Refund = Convert.ToDouble(dgvr.Cells["clmRefund"].Value);
                    objCollection_bonus.Net_bonus = Convert.ToDouble(dgvr.Cells["clmNetBonus"].Value);
                    objCollection_bonus.Createby = BaseCls.GlbUserID;
                    objCollection_bonus.Clm_by = "";
                    _lstCollection_bonus.Add(objCollection_bonus);

                }
            }

            if (!string.IsNullOrEmpty(_errorlist.ToString()))
            {
                if (MessageBox.Show("Following Vouchers are Already Inserted when checking the file.\n" + _errorlist.ToString() + ".\n Do you need to confirm others anyway?", "Discrepancies", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    return _lstCollection_bonus;
                }


            }
            return _lstCollection_bonus;

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbYear.Text))
            {
                MessageBox.Show("Select the year");
                return;
            }
            if (string.IsNullOrEmpty(cmbMonth.Text))
            {
                MessageBox.Show("Select the Month");
                return;
            }


            if (dgvCollectionBonus.Rows.Count > 0)
            {
                List<Deposit_Bank_Pc_wise> lst_bonus = fillToCollectionBonus();
                if (lst_bonus.Count <= 0)
                {
                    MessageBox.Show("These records are already Saved to Database..Please select another month..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string _error = "";
                int result = CHNLSVC.Sales.Insert_to_Col_bonus(lst_bonus, out _error);
                if (result == -1)
                {
                    MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    MessageBox.Show("Records Confirmed Successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnClear_Click(null, null);
                }


            }
            else
            {
                MessageBox.Show("Please Upload your excel file first..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnNewEdit_Click(object sender, EventArgs e)
        {
            pnlCreateNew.Visible = true;
            bindPanelData();
            rdoprofitcenter.Checked = true;
        }

        private void SearchColl()
        {

            if (rdoprofitcenter.Checked)
            {
                if (txtprofitcenter.Text == "")
                {
                    MessageBox.Show("Please enter Profit Center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtprofitcenter.Focus();
                }
            }
            else if (rdoVour.Checked)
            {
                if (txtVou.Text == "")
                {
                    MessageBox.Show("Please enter voucher number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtVou.Focus();
                }
            }
            else if (rdoDate.Checked)
            {
                if (cmbMonthnew.SelectedIndex == -1)
                {
                    MessageBox.Show("Please enter Month and Year", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbMonthnew.Focus();
                }
            }



            DataTable dt = CHNLSVC.Sales.GetAll_Collection_Bonus(BaseCls.GlbUserComCode);
            if (txtprofitcenter.Text.Length > 0)
            {
                DataRow[] foundRows;
                DataTable dtsub = null;
                string expression = "hpbv_pc = '" + txtprofitcenter.Text.Trim() + "'";
                foundRows = dt.Select(expression);
                if (foundRows.Count() > 0)
                {
                    dtsub = foundRows.CopyToDataTable<DataRow>();
                    dgvEditBonus.AutoGenerateColumns = false;
                    dgvEditBonus.DataSource = null;
                    dgvEditBonus.DataSource = dtsub;

                }

                else
                {
                    MessageBox.Show("Records are not available", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtprofitcenter.Text = "";
                    txtprofitcenter.Focus();
                }



                //DataTable _validate = dt.AsEnumerable().Where(x => x.Field<string>("hpbv_pc") == txtprofitcenter.Text.Trim()).CopyToDataTable();
                //if (_validate != null || _validate.Rows.Count > 0)
                //{
                //    dgvEditBonus.AutoGenerateColumns = false;
                //    dgvEditBonus.DataSource = null;
                //    dgvEditBonus.DataSource = _validate;
                //}
                //else
                //{
                //    MessageBox.Show("Records are not available", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}


            }
            else if (txtVou.Text.Length > 0)
            {


                DataRow[] foundRows;
                DataTable dtsub = null;
                string expression = "hpbv_vou_no = '" + txtVou.Text.Trim() + "'";
                foundRows = dt.Select(expression);
                if (foundRows.Count() > 0)
                {
                    dtsub = foundRows.CopyToDataTable<DataRow>();
                    dgvEditBonus.AutoGenerateColumns = false;
                    dgvEditBonus.DataSource = null;
                    dgvEditBonus.DataSource = dtsub;

                }

                else
                {
                    MessageBox.Show("Records are not available", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtVou.Text = "";
                    txtVou.Focus();
                }



                //DataTable vou = dt.AsEnumerable().Where(x => x.Field<string>("hpbv_vou_no") == txtVou.Text.Trim()).CopyToDataTable();
                //if (vou != null || vou.Rows.Count > 0)
                //{
                //    dgvEditBonus.AutoGenerateColumns = false;
                //    dgvEditBonus.DataSource = null;
                //    dgvEditBonus.DataSource = vou;
                //}
                //else
                //{
                //    MessageBox.Show("Records are not available", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
            }
            else if (cmbMonthnew.SelectedIndex != -1)
            {
                DataRow[] foundRows;
                DataTable dtsub = null;
                //DataRow[] result = xData.Select("TimeStamp2 = #" + str + "#");
                string expression = "HPBV_MONTH = #" + fin_new_date.ToString() + "#";
                foundRows = dt.Select(expression);
                if (foundRows.Count() > 0)
                {
                    dtsub = foundRows.CopyToDataTable<DataRow>();
                    dgvEditBonus.AutoGenerateColumns = false;
                    dgvEditBonus.DataSource = null;
                    dgvEditBonus.DataSource = dtsub;

                }

                else
                {
                    MessageBox.Show("Records are not available", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbMonthnew.Focus();
                }


                //DataTable dte = dt.AsEnumerable().Where(x => x.Field<DateTime>("HPBV_MONTH") == fin_new_date).CopyToDataTable();
                //if (dte != null || dte.Rows.Count > 0)
                //{
                //    dgvEditBonus.AutoGenerateColumns = false;
                //    dgvEditBonus.DataSource = null;
                //    dgvEditBonus.DataSource = dte;
                //}
                //else
                //{
                //    MessageBox.Show("Records are not available", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
            }


        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchColl();
        }

        private void cmbYearNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMonthnew.SelectedIndex = -1;
        }

        private void cmbMonthnew_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbYearNew.Text))
            {
                MessageBox.Show("Select the year");
                return;
            }

            if (cmbMonthnew.Text != "")
            {
                string _year = cmbYearNew.Text;
                string _final_yr = Microsoft.VisualBasic.Strings.Right(_year, 2);
                string _month = cmbMonthnew.Text.ToUpper();
                string _fi_month = _month.Substring(0, 3);
                string _date = "01";

                string final_date_time = _date + "-" + _fi_month + "-" + _final_yr;
                fin_new_date = Convert.ToDateTime(final_date_time);
            }
        }

        private void btnClose_new_Click(object sender, EventArgs e)
        {

            txtVou.Text = "";
            txtprofitcenter.Text = "";
            pnlCreateNew.Visible = false;
            cmbMonthnew.Items.Clear();
            cmbYearNew.Items.Clear();

            cmbBonusStatus.SelectedIndex = -1;
            dgvEditBonus.DataSource = null;
            txtGross.Text = "";
            txtdeduction.Text = "";
            txtRefund.Text = "";
            txtNetbonus.Text = "";
        }

        private void rdoprofitcenter_CheckedChanged(object sender, EventArgs e)
        {
            cmbYearNew.Enabled = false;
            cmbMonthnew.Enabled = false;
            cmbMonthnew.SelectedIndex = -1;
            txtprofitcenter.Enabled = true;
            txtVou.Enabled = false;
            txtVou.Text = "";
        }

        private void rdoVour_CheckedChanged(object sender, EventArgs e)
        {
            txtVou.Enabled = true;
            txtprofitcenter.Enabled = false;
            txtprofitcenter.Text = "";
            cmbYearNew.Enabled = false;
            cmbMonthnew.Enabled = false;
            cmbMonthnew.SelectedIndex = -1;
        }

        private void rdoDate_CheckedChanged(object sender, EventArgs e)
        {
            txtVou.Enabled = false;
            txtVou.Text = "";
            txtprofitcenter.Text = "";
            txtprofitcenter.Enabled = false;
            cmbYearNew.Enabled = true;
            cmbMonthnew.Enabled = true;
        }

        private void dgvEditBonus_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                profitcenter = dgvEditBonus["clmProfitcenter", e.RowIndex].Value.ToString();
                vouno = dgvEditBonus["clmVoucher", e.RowIndex].Value.ToString();
                double gross = Convert.ToDouble(dgvEditBonus["clmGrossBonus", e.RowIndex].Value);
                txtGross.Text = gross.ToString();
                double dec = Convert.ToDouble(dgvEditBonus["clmDeduct", e.RowIndex].Value);
                txtdeduction.Text = dec.ToString();

                double refu = Convert.ToDouble(dgvEditBonus["clmRefnd", e.RowIndex].Value);
                txtRefund.Text = refu.ToString();

                double net = Convert.ToDouble(dgvEditBonus["clmNet", e.RowIndex].Value);
                txtNetbonus.Text = net.ToString();

                string status = Convert.ToString(dgvEditBonus["clmStus", e.RowIndex].Value);
                if (status == "pending Claim")
                {
                    cmbBonusStatus.Text = "Release";
                }
                else
                {
                    cmbBonusStatus.Text = "Hold";
                }

            }
        }

        private void txtdeduction_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGross.Text.Trim()))
            {
                // MessageBox.Show("Gross bonus cannot be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGross.Text = "";
                return;
            }
            if (string.IsNullOrEmpty(txtdeduction.Text.Trim()))
            {
                // MessageBox.Show("Deduction cannot be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtdeduction.Text = "";
                return;
            }
            if (string.IsNullOrEmpty(txtRefund.Text.Trim())) return;

            //if (!txtGross.Text.All(c => Char.IsNumber(c)) && !txtGross.Text.All(c => Char.IsNumber(c)))
            //{
            //    //MessageBox.Show("Please enter only numbers", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtGross.Text = "";
            //    // txtGross.Focus();
            //    return;
            //}
            //if (!txtdeduction.Text.All(c => Char.IsNumber(c)) && !txtdeduction.Text.All(c => Char.IsNumber(c)))
            //{
            //    //MessageBox.Show("Please enter only numbers", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtdeduction.Text = "";
            //    // txtGross.Focus();
            //    return;
            //}
            //if (!txtRefund.Text.All(c => Char.IsNumber(c)) && !txtRefund.Text.All(c => Char.IsNumber(c)))
            //{
            //    //MessageBox.Show("Please enter only numbers", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtRefund.Text = "";
            //    // txtGross.Focus();
            //    return;
            //}

            if (!IsNumeric(txtGross.Text))
            {
                txtGross.Text = "0";
                return;
            }


            if (!IsNumeric(txtdeduction.Text))
            {
                txtdeduction.Text = "0";
                return;
            }

            if (!IsNumeric(txtRefund.Text))
            {
                txtRefund.Text = "0";
                return;
            }


            double grs = Convert.ToDouble(txtGross.Text.Trim());
            double ded = Convert.ToDouble(txtdeduction.Text.Trim());
            double refund = Convert.ToDouble(txtRefund.Text.Trim());
            double net_bonus = grs - ded + refund;

            txtNetbonus.Text = net_bonus.ToString();
        }

        private void txtdeduction_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNetbonus.Focus();
            }
        }


        private Deposit_Bank_Pc_wise filltoEditCollection()
        {
            objCollection_bonus = new Deposit_Bank_Pc_wise();
            objCollection_bonus.Com = BaseCls.GlbUserComCode;
            objCollection_bonus.Prof_center = profitcenter;
            objCollection_bonus.Voucher_no = vouno;
            objCollection_bonus.Gross_bonus_amt = Convert.ToDouble(txtGross.Text.Trim());
            objCollection_bonus.Deduction = Convert.ToDouble(txtdeduction.Text.Trim());
            objCollection_bonus.Refund = Convert.ToDouble(txtRefund.Text.Trim());
            objCollection_bonus.Net_bonus = Convert.ToDouble(txtNetbonus.Text.Trim());
            string stus = cmbBonusStatus.Text;
            if (stus == "Hold")
            {
                objCollection_bonus.Pun_tp = 2;
            }
            else if (stus == "Release")
            {
                objCollection_bonus.Pun_tp = 0;
            }



            return objCollection_bonus;
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            #region validation

            if (txtGross.Text == "")
            {
                MessageBox.Show("Gross bonus cannot be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtdeduction.Text.Trim() == "")
            {
                MessageBox.Show("Deduction cannot be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtRefund.Text.Trim() == "")
            {
                MessageBox.Show("Refund cannot be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbBonusStatus.Text == "")
            {
                MessageBox.Show("Please select bonus status", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsNumeric(txtGross.Text))
            {
                MessageBox.Show("Please enter only numbers", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGross.Text = "0";
                txtGross.Focus();
                return;
            }

            if (!IsNumeric(txtdeduction.Text))
            {
                MessageBox.Show("Please enter only numbers", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtdeduction.Text = "0";
                txtdeduction.Focus();
                return;
            }

            if (!IsNumeric(txtRefund.Text))
            {
                MessageBox.Show("Please enter only numbers", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRefund.Text = "";
                txtRefund.Focus();
                return;
            }


            //if (!txtGross.Text.All(c => Char.IsNumber(c)) && !txtGross.Text.All(c => Char.IsNumber(c)))
            //{
            //    MessageBox.Show("Please enter only numbers", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtGross.Text = "";
            //    txtGross.Focus();
            //    return;
            //}
            //if (!txtdeduction.Text.All(c => Char.IsNumber(c)) && !txtdeduction.Text.All(c => Char.IsNumber(c)))
            //{
            //    MessageBox.Show("Please enter only numbers", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtdeduction.Text = "";
            //    txtdeduction.Focus();
            //    return;
            //}
            //if (!txtRefund.Text.All(c => Char.IsNumber(c)) && !txtRefund.Text.All(c => Char.IsNumber(c)))
            //{
            //    MessageBox.Show("Please enter only numbers", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtRefund.Text = "";
            //    txtRefund.Focus();
            //    return;
            //}

            #endregion
            if (MessageBox.Show("Are you sure want to update?", "Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            string _error = "";
            int result = CHNLSVC.Sales.UpdateToCollectionBonus(filltoEditCollection(), out _error);
            if (result == -1)
            {
                MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                MessageBox.Show("Records updated Successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnClearNew_Click(null, null);
            }




        }

        private void btnClearNew_Click(object sender, EventArgs e)
        {
            txtprofitcenter.Text = "";
            txtVou.Text = "";
            cmbMonthnew.SelectedIndex = -1;
            cmbBonusStatus.SelectedIndex = -1;
            dgvEditBonus.DataSource = null;
            txtGross.Text = "";
            txtdeduction.Text = "";
            txtRefund.Text = "";
            txtNetbonus.Text = "";


        }

        private void txtRefund_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGross.Text.Trim()))
            {
                // MessageBox.Show("Gross bonus cannot be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGross.Text = "";
                return;
            }
            if (string.IsNullOrEmpty(txtdeduction.Text.Trim()))
            {
                // MessageBox.Show("Deduction cannot be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtdeduction.Text = "";
                return;
            }
            if (string.IsNullOrEmpty(txtRefund.Text.Trim())) return;

            //if (!txtGross.Text.All(c => Char.IsNumber(c)) && !txtGross.Text.All(c => Char.IsNumber(c)))
            //{
            //    //MessageBox.Show("Please enter only numbers", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtGross.Text = "";
            //   // txtGross.Focus();
            //    return;
            //}
            //if (!txtdeduction.Text.All(c => Char.IsNumber(c)) && !txtdeduction.Text.All(c => Char.IsNumber(c)))
            //{
            //    //MessageBox.Show("Please enter only numbers", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtdeduction.Text = "";
            //    // txtGross.Focus();
            //    return;
            //}
            //if (!txtRefund.Text.All(c => Char.IsNumber(c)) && !txtRefund.Text.All(c => Char.IsNumber(c)))
            //{
            //    //MessageBox.Show("Please enter only numbers", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtRefund.Text = "";
            //    // txtGross.Focus();
            //    return;
            //}

            if (!IsNumeric(txtGross.Text))
            {
                txtGross.Text = "0";
                return;
            }


            if (!IsNumeric(txtdeduction.Text))
            {
                txtdeduction.Text = "0";
                return;
            }

            if (!IsNumeric(txtRefund.Text))
            {
                txtRefund.Text = "0";
                return;
            }


            double grs = Convert.ToDouble(txtGross.Text.Trim());
            double ded = Convert.ToDouble(txtdeduction.Text.Trim());
            double refund = Convert.ToDouble(txtRefund.Text.Trim());
            double net_bonus = grs - ded + refund;

            txtNetbonus.Text = net_bonus.ToString();
        }

        private void txtRefund_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNetbonus.Focus();
            }
        }

        private void txtGross_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGross.Text.Trim()))
            {
                //MessageBox.Show("Gross bonus cannot be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGross.Text = "";
                return;
            }
            if (string.IsNullOrEmpty(txtdeduction.Text.Trim()))
            {
                //MessageBox.Show("Deduction cannot be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtdeduction.Text = "";
                return;
            }
            if (string.IsNullOrEmpty(txtRefund.Text.Trim())) return;

            if (!IsNumeric(txtGross.Text))
            {
                txtGross.Text = "0";
                return;
            }
            //if (!txtGross.Text.All(c => Char.IsNumber(c)) && !txtGross.Text.All(c => Char.IsNumber(c)))
            //{
            //    //MessageBox.Show("Please enter only numbers", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtGross.Text = "";
            //    // txtGross.Focus();
            //    return;
            //}
            if (!IsNumeric(txtdeduction.Text))
            {
                txtdeduction.Text = "0";
                return;
            }
            //if (!txtdeduction.Text.All(c => Char.IsNumber(c)) && !txtdeduction.Text.All(c => Char.IsNumber(c)))
            //{
            //    //MessageBox.Show("Please enter only numbers", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtdeduction.Text = "";
            //    // txtGross.Focus();
            //    return;
            //}
            if (!IsNumeric(txtRefund.Text))
            {
                txtRefund.Text = "0";
                return;
            }
            //if (!txtRefund.Text.All(c => Char.IsNumber(c)) && !txtRefund.Text.All(c => Char.IsNumber(c)))
            //{
            //    //MessageBox.Show("Please enter only numbers", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtRefund.Text = "";
            //    // txtGross.Focus();
            //    return;
            //}



            double grs = Convert.ToDouble(txtGross.Text.Trim());
            double ded = Convert.ToDouble(txtdeduction.Text.Trim());
            double refund = Convert.ToDouble(txtRefund.Text.Trim());
            double net_bonus = grs - ded + refund;

            txtNetbonus.Text = net_bonus.ToString();
        }

        private void txtGross_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNetbonus.Focus();
            }
        }


    }
}
