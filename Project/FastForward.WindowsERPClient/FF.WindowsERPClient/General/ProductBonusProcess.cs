using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.General
{
    public partial class ProductBonusProcess : Base
    {
        public ProductBonusProcess()
        {
            InitializeComponent();
            dtFrom.Value = Convert.ToDateTime(DateTime.Now).Date;
            dtTo.Value = Convert.ToDateTime(DateTime.Now).Date;
        }

        private void btnCircular_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.IncentiveCirc);
            DataTable _result = CHNLSVC.CommonSearch.GetSearchProdBonusCircular(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCircular;
            _CommonSearch.ShowDialog();
            txtCircular.Focus();
            txtCircularNo_Leave(null, null);

        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.IncentiveCirc:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        private void txtCircularNo_Leave(object sender, EventArgs e)
        {
            bindSchemes();
        }

        private void lnkAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem Item in lstLocations.Items)
            {
                Item.Checked = true;
            }
        }

        private void lnkNone_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem Item in lstLocations.Items)
            {
                Item.Checked = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //this.Cursor = Cursors.WaitCursor;
            //int X = CHNLSVC.Financial.ProcessProductBonus(BaseCls.GlbUserComCode, txtCircular.Text, cmbScheme.SelectedValue.ToString(), Convert.ToDateTime(dtFrom.Value).Date, Convert.ToDateTime(dtTo.Value).Date, BaseCls.GlbUserID);
            //this.Cursor = Cursors.Default;
            Int32 X = 0;
            btnSave.Enabled = false;
            string strPara = BaseCls.GlbUserID + ' ' + cmbScheme.SelectedValue.ToString() + ' ' + Convert.ToDateTime( dtFrom.Value.Date) + ' ' + Convert.ToDateTime( dtTo.Value.Date);
            string _path = Application.StartupPath;
            //System.Diagnostics.Process.Start(_path + "\\Prod_Bonus.exe", strPara);
            System.Diagnostics.Process.Start("\\\\192.168.1.225\\SCM2_Othershop\\Prod_Bonus.exe", strPara);
            

        XX:
            //Int16 is_Finish = CHNLSVC.Financial.Is_Prod_Bonus_Finish(BaseCls.GlbUserID);
            //if (is_Finish == 0)
            //{
            //    goto XX;
            //}
            btnSave.Enabled = true;
         //   MessageBox.Show("Successfully Completed.", "Product Bonus", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bindSchemes()
        {
            // cmbScheme.Items.Clear();
            cmbScheme.DataSource = null;
            cmbScheme.DataSource = CHNLSVC.General.GetIncentiveSchemes(txtCircular.Text);
            cmbScheme.DisplayMember = "INC_REF";
            cmbScheme.ValueMember = "INC_REF";
            cmbScheme.SelectedIndex = -1;
        }

        private void Process()
        {
            /*
             01get all filtered invoices
                 -get do invoice items and invoice no for period
                 -check discount
                 -check promation
                 insert into temp list

             02.get all filtered REVERSE
                 -get srn invoice items and invoice no for period
                 -check discount
                 -check promation
                 insert into temp list

             get all employees
             03.run process1 to mgr,EXE
             run process1 for other

             process1:
             get defintion for emp type
             call commission
                insert to temp LISTS

             04.run additionl calculations
                -get definition
                -update temp LISTS

             05.save to DB

             commission
             get sum amount for employee 
             check definition is in BT(before target)-comm=amount*BT or value
             if amount>target
             comm=amount*tar or value
             check for slab 
             comm=comm+amount*tar or value 
             */
            try
            {

                //validate
                if (txtCircular.Text == "")
                {
                    MessageBox.Show("Please select circular to process", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DateTime _from = new DateTime(Convert.ToInt32(udYear.Value), Convert.ToInt32(udMonth.Value), 1);
                DateTime _to = new DateTime(Convert.ToInt32(udYear.Value), Convert.ToInt32(udMonth.Value), DateTime.DaysInMonth(Convert.ToInt32(udYear.Value), Convert.ToInt32(udMonth.Value)));
                if (_from < dtFrom.Value || _to > dtTo.Value)
                {
                    MessageBox.Show("Please select month within valid range", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<string> _pcList = new List<string>();
                foreach (ListViewItem Item in lstLocations.Items)
                {
                    if (Item.Checked)
                    {
                        _pcList.Add(Item.Text);
                    }
                }
                if (_pcList.Count <= 0)
                {
                    MessageBox.Show("Please select at least one profit center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<EliteCommission> _errors = new List<EliteCommission>();
                string err;
                CHNLSVC.Sales.EliteCommissionProcess(txtCircular.Text, _pcList, _from, _to, BaseCls.GlbUserComCode, Convert.ToInt32(udYear.Value), Convert.ToInt32(udMonth.Value), BaseCls.GlbUserID, DateTime.Now, out _errors, false, out err);
                if (_errors != null && _errors.Count > 0)
                {
                    //view message
                    pnlMain.Enabled = false;
                    pnlPopUp.Visible = true;

                    grvErrors.AutoGenerateColumns = false;
                    grvErrors.DataSource = _errors;

                    if (MessageBox.Show("Errors occurred while processing!\nDo you want to continue process?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        CHNLSVC.Sales.EliteCommissionProcess(txtCircular.Text, _pcList, _from, _to, BaseCls.GlbUserComCode, Convert.ToInt32(udYear.Value), Convert.ToInt32(udMonth.Value), BaseCls.GlbUserID, DateTime.Now, out _errors, true, out err);
                        pnlMain.Enabled = true;
                        pnlPopUp.Visible = false;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    if (err == "")
                    {
                        MessageBox.Show("Process Completed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnClear_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Errors occurred while processing!\n" + err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erroe Occured while processing\n" + ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CHNLSVC.CloseChannel(); 
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            while (this.Controls.Count > 0)
            {
                Controls[0].Dispose();
            }
            InitializeComponent();

            this.Cursor = Cursors.Default;
        }

        private void btnPopUpClose_Click(object sender, EventArgs e)
        {
            pnlMain.Enabled = true;
            pnlPopUp.Visible = false;
        }

        private void cmbScheme_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }


}
