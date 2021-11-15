using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FF.BusinessObjects;

namespace FF.WindowsERPClient.Finance
{
    public partial class VoucherPrintingExpenseDefinition : Base
    {

        #region variables

        List<MasterCompany> CompanyList = new List<MasterCompany>();
        List<VoucherPrintExpenseDefinition> ExpenseList = new List<VoucherPrintExpenseDefinition>();

        #endregion

        public VoucherPrintingExpenseDefinition()
        {
            InitializeComponent();
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you want to Quit", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime _date = CHNLSVC.Security.GetServerDateTime();

                #region validation

                if (CompanyList.Count <= 0)
                {
                    if (txtCompany.Text != "")
                    {
                        //validate company
                        MasterCompany _com = CHNLSVC.General.GetCompByCode(txtCompany.Text);
                        if (_com == null)
                        {
                            MessageBox.Show("Invalid Company Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        //show error
                        MessageBox.Show("At lease one company needed to process", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (txtExpense.Text == "")
                {
                    MessageBox.Show("Expense code cannot be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //if (txtCategory.Text == "") {
                //    MessageBox.Show("Category code cannot be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                if (txtAmount.Text != "")
                {
                    decimal val;
                    if (!decimal.TryParse(txtAmount.Text, out val))
                    {
                        //show error
                        MessageBox.Show("Amount has to be number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }
                if (txtAmount.Text == "")
                {
                    MessageBox.Show("Amount cannot be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dateTimePickerFrom.Value > dateTimePickerTo.Value)
                {
                    MessageBox.Show("From date has to be less than to date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //check duplicate
                List<VoucherPrintExpenseDefinition> _duplicate = (from _res in ExpenseList
                                                                  where _res.Gved_expe_cd == txtExpense.Text
                                                                  select _res).ToList<VoucherPrintExpenseDefinition>();
                if (_duplicate != null && _duplicate.Count > 0)
                {
                    MessageBox.Show("Expense code can not duplicate", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!chkActive.Checked)
                {
                    if (MessageBox.Show("Expense definition you adding is inactive\nDo you want to proceed?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                }

                //add to list
                VoucherPrintExpenseDefinition _expense = new VoucherPrintExpenseDefinition();
                _expense.Gved_expe_cd = txtExpense.Text.ToUpper();
                _expense.Gved_expe_cat = txtCategory.Text.ToUpper();
                _expense.Gved_expe_val = Convert.ToDecimal(txtAmount.Text);
                _expense.Gved_expe_desc = txtDescription.Text.ToUpper();
                _expense.Gved_expe_from_dt = dateTimePickerFrom.Value.Date;
                _expense.Gved_expe_to_dt = dateTimePickerTo.Value.Date;
                _expense.Gved_stus = chkActive.Checked;
                _expense.Gved_cre_by = BaseCls.GlbUserID;
                _expense.Gved_cre_dt = _date;
                _expense.Gved_mod_by = BaseCls.GlbUserID;
                _expense.Gved_mod_dt = _date;
                if (chkDeduc.Checked)
                    _expense.Gved_expe_cat = "MINUS";
                else
                    _expense.Gved_expe_cat = "PLUS";

                if (rbnInternal.Checked)
                {
                    _expense.Gved_vou_tp = 1;
                }
                else
                {
                    _expense.Gved_vou_tp = 2;
                }

                ExpenseList.Add(_expense);

                grvDetails.AutoGenerateColumns = false;
                BindingSource _source = new BindingSource();
                _source.DataSource = ExpenseList;
                grvDetails.DataSource = _source;

                ClearAdd();
                #endregion
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

        private void ClearAdd()
        {
            txtExpense.Text = "";
            txtCategory.Text = "";
            txtAmount.Text = "";
            txtDescription.Text = "";
            chkActive.Checked = true;
        }

        private void btnCompany_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCompany;
                _CommonSearch.txtSearchbyword.Text = txtCompany.Text;
                _CommonSearch.ShowDialog();
                txtCompany.Focus();
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

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(txtCompany.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InternalVoucherExpense:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
            }
            return paramsText.ToString();
        }

        private void btnAddCom_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCompany.Text != "")
                {
                    MasterCompany _com = CHNLSVC.General.GetCompByCode(txtCompany.Text);
                    if (_com != null)
                    {
                        //check for duplicates
                        List<MasterCompany> _duplicate = (from _res in CompanyList
                                                          where _res.Mc_cd == txtCompany.Text
                                                          select _res).ToList();

                        if (_duplicate == null || _duplicate.Count <= 0)
                        {
                            CompanyList.Add(_com);
                            txtCompany.Text = "";
                            LoadExpense();
                        }
                        else
                        {
                            MessageBox.Show("Duplicate Company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Company code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //gridview data bind
                    grvCompany.AutoGenerateColumns = false;
                    BindingSource _sourec = new BindingSource();
                    _sourec.DataSource = CompanyList;
                    grvCompany.DataSource = _sourec;
                }
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            DateTime _date = CHNLSVC.Security.GetServerDateTime();
            CompanyList = new List<MasterCompany>();
            ExpenseList = new List<VoucherPrintExpenseDefinition>();

            txtCompany.Text = "";
            txtExpense.Text = "";
            txtCategory.Text = "";
            txtDescription.Text = "";
            txtAmount.Text = "";

            dateTimePickerFrom.Value = _date;
            dateTimePickerTo.Value = _date;

            grvCompany.DataSource = null;
            grvDetails.DataSource = null;
        }

        private void grvCompany_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            CompanyList.RemoveAt(e.RowIndex);
                            BindingSource _source = new BindingSource();
                            _source.DataSource = CompanyList;
                            grvCompany.DataSource = _source;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                }
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //save function
                if (ExpenseList.Count > 0)
                {
                    try
                    {
                        DateTime _date = CHNLSVC.Security.GetServerDateTime();
                        List<VoucherPrintExpenseDefinition> _expenseList = new List<VoucherPrintExpenseDefinition>();
                        if (CompanyList.Count > 0)
                        {
                            foreach (MasterCompany _com in CompanyList)
                            {

                                foreach (VoucherPrintExpenseDefinition _ex in ExpenseList)
                                {
                                    VoucherPrintExpenseDefinition _tem = new VoucherPrintExpenseDefinition();
                                    _tem.Gved_com = _com.Mc_cd;
                                    _tem.Gved_expe_desc = _ex.Gved_expe_desc;
                                    _tem.Gved_expe_val = _ex.Gved_expe_val;
                                    _tem.Gved_expe_cat = _ex.Gved_expe_cat;
                                    _tem.Gved_expe_cd = _ex.Gved_expe_cd;
                                    _tem.Gved_cre_by = BaseCls.GlbUserID;
                                    _tem.Gved_cre_dt = _date;
                                    _tem.Gved_mod_by = BaseCls.GlbUserID;
                                    _tem.Gved_mod_dt = _date;
                                    _tem.Gved_expe_from_dt = _ex.Gved_expe_from_dt;
                                    _tem.Gved_expe_to_dt = _ex.Gved_expe_to_dt;
                                    _tem.Gved_stus = _ex.Gved_stus;
                                    _tem.Gved_vou_tp = _ex.Gved_vou_tp;
                                    //ExpenseList[i].Gved_com = _com.Mc_cd;
                                    //_exp.Gved_com = _com.Mc_cd;
                                    _expenseList.Add(_tem);
                                }
                            }
                        }
                        else
                        {
                            foreach (VoucherPrintExpenseDefinition _ex in ExpenseList)
                            {
                                VoucherPrintExpenseDefinition _tem = new VoucherPrintExpenseDefinition();
                                _tem.Gved_com = txtCompany.Text;
                                _tem.Gved_expe_desc = _ex.Gved_expe_desc;
                                _tem.Gved_expe_val = _ex.Gved_expe_val;
                                _tem.Gved_expe_cat = _ex.Gved_expe_cat;
                                _tem.Gved_expe_cd = _ex.Gved_expe_cd;
                                _tem.Gved_cre_by = BaseCls.GlbUserID;
                                _tem.Gved_cre_dt = _date;
                                _tem.Gved_mod_by = BaseCls.GlbUserID;
                                _tem.Gved_mod_dt = _date;
                                _tem.Gved_expe_from_dt = _ex.Gved_expe_from_dt;
                                _tem.Gved_expe_to_dt = _ex.Gved_expe_to_dt;
                                _tem.Gved_stus = _ex.Gved_stus;
                                _tem.Gved_vou_tp = _ex.Gved_vou_tp;
                                //ExpenseList[i].Gved_com = _com.Mc_cd;
                                //_exp.Gved_com = _com.Mc_cd;
                                _expenseList.Add(_tem);
                            }
                        }

                        int result = CHNLSVC.Financial.SaveVoucherExpenseDefinition(_expenseList);
                        if (result > 0)
                        {
                            MessageBox.Show("Saving Process Completed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Clear();
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Error occurred while processing...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error occurred while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CHNLSVC.CloseChannel(); 
                        return;
                    }
                }
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

        private void grvDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            ExpenseList.RemoveAt(e.RowIndex);

                            BindingSource _source = new BindingSource();
                            _source.DataSource = ExpenseList;
                            grvDetails.DataSource = _source;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
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

        private void txtExpense_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtExpense.Text != "")
                {
                    if (CompanyList.Count > 0 || txtCompany.Text != "")
                    {
                        LoadExpense();
                    }
                    else
                    {
                        LoadCompanies();
                        LoadExpense();
                    }
                }
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

        private void LoadCompanies()
        {
            List<VoucherPrintExpenseDefinition> _expense = CHNLSVC.Financial.GetVoucherExpenseByCode(txtExpense.Text);
            if (_expense != null && _expense.Count > 0)
            {
                CompanyList = new List<MasterCompany>();
                foreach (VoucherPrintExpenseDefinition _exp in _expense)
                {
                    MasterCompany _company = CHNLSVC.General.GetCompByCode(_exp.Gved_com);
                    CompanyList.Add(_company);
                }
                grvCompany.AutoGenerateColumns = false;
                BindingSource _source = new BindingSource();
                _source.DataSource = CompanyList;
                grvCompany.DataSource = _source;
            }
        }

        private void LoadExpense()
        {
            if (CompanyList.Count > 0)
            {
                foreach (MasterCompany _com in CompanyList)
                {
                    VoucherPrintExpenseDefinition _vou = CHNLSVC.Financial.GetVoucherExpense(_com.Mc_cd, txtExpense.Text);
                    if (_vou != null)
                    {
                        //load details
                        txtCategory.Text = _vou.Gved_expe_cat;
                        txtDescription.Text = _vou.Gved_expe_desc;
                        txtAmount.Text = _vou.Gved_expe_val.ToString();
                        dateTimePickerFrom.Value = _vou.Gved_expe_from_dt;
                        dateTimePickerTo.Value = _vou.Gved_expe_to_dt;
                        chkActive.Checked = _vou.Gved_stus;
                    }
                }
            }
            else
            {
                if (txtCompany.Text != null)
                {
                    VoucherPrintExpenseDefinition _vou = CHNLSVC.Financial.GetVoucherExpense(txtCompany.Text, txtExpense.Text);
                    if (_vou != null)
                    {
                        //load details
                        txtCategory.Text = _vou.Gved_expe_cat;
                        txtDescription.Text = _vou.Gved_expe_desc;
                        txtAmount.Text = _vou.Gved_expe_val.ToString();
                        dateTimePickerFrom.Value = _vou.Gved_expe_from_dt;
                        dateTimePickerTo.Value = _vou.Gved_expe_to_dt;
                        chkActive.Checked = _vou.Gved_stus;
                    }
                }
            }
        }

        private void txtCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                txtExpense.Focus();
            }
            if (e.KeyCode == Keys.F2)
                btnCompany_Click(null, null);
        }

        private void txtExpense_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDescription.Focus();
        }

        private void txtCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dateTimePickerFrom.Focus();
        }

        private void dateTimePickerFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dateTimePickerTo.Focus();
        }

        private void dateTimePickerTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDescription.Focus();
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAmount.Focus();
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                chkActive.Focus();
        }

        private void chkActive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAdd.Focus();
        }

        private void txtCompany_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnCompany_Click(null, null);
        }

        private void VoucherPrintingExpenseDefinition_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                dateTimePickerTo.Value = _date.AddDays(1);
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

        private void txtCompany_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCompany.Text != "")
                {
                    MasterCompany _com = CHNLSVC.General.GetCompByCode(txtCompany.Text);
                    if (_com != null)
                    {
                        LoadExpense();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Company code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
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

        private void btnExpense_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InternalVoucherExpense);
                DataTable _result = CHNLSVC.CommonSearch.GetInternalVoucherExpense(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtExpense;
                _CommonSearch.txtSearchbyword.Text = txtExpense.Text;
                _CommonSearch.ShowDialog();
                txtExpense.Focus();
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
    }
}
