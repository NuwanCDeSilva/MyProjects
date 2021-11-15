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
    public partial class EliteCommissionProcess : Base
    {
        public EliteCommissionProcess()
        {
            InitializeComponent();
        }

        private void btnCircular_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EliteCircular);
                DataTable _result = CHNLSVC.CommonSearch.GetSearchEliteCommCircular(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCircularNo;
                _CommonSearch.txtSearchbyword.Text = txtCircularNo.Text;
                _CommonSearch.ShowDialog();
                txtCircularNo.Focus();
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
                case CommonUIDefiniton.SearchUserControlType.EliteCircular:
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
            try
            {
                if (string.IsNullOrEmpty(txtCircularNo.Text))
                {
                    return;
                }
                lstLocations.Items.Clear();
                //validate circular no
                List<FF.BusinessObjects.EliteCommissionDefinition> _definition = CHNLSVC.Sales.GetEliteCommissionDefinition(txtCircularNo.Text);
                if (_definition != null)
                {
                    dtFrom.Value = _definition[0].Saec_valid_from;
                    dtTo.Value = _definition[0].Saec_valid_to;
                }
                //load locations
                List<EliteCommissionPrty> _prty = CHNLSVC.Sales.GetEliteCommissionLocation(txtCircularNo.Text);
                foreach (EliteCommissionPrty prty in _prty)
                {
                    MasterProfitCenter _mstPc = CHNLSVC.General.GetPCByPCCode(BaseCls.GlbUserComCode, prty.Saec_prt_cd);
                    if(_mstPc!=null){
                    if(_mstPc.Mpc_chnl=="DUTY_FREE")
                        grpExchange.Visible=true;
                    }
                    lstLocations.Items.Add(prty.Saec_prt_cd);

                }

                //load details
                List<EliteCommissionDetail> _detail = CHNLSVC.Sales.GetEliteCommissionDetailsByCircular(txtCircularNo.Text);

                //load mgr
                if (_detail != null && _detail.Count > 0)
                {
                    List<EliteCommissionDetail> _mgr = (from _res in _detail
                                                        where _res.Saec_emp_type == "SRMGR"
                                                        select _res).ToList<EliteCommissionDetail>();

                    grvMgr.AutoGenerateColumns = false;
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _mgr;
                    grvMgr.DataSource = _source;
                    grvMgr.Enabled = false;


                    List<EliteCommissionDetail> _Exe = (from _res in _detail
                                                        where _res.Saec_emp_type == "SALEX"
                                                        select _res).ToList<EliteCommissionDetail>();

                    grvExe.AutoGenerateColumns = false;
                    BindingSource _source1 = new BindingSource();
                    _source1.DataSource = _Exe;
                    grvExe.DataSource = _source1;
                    grvExe.Enabled = false;

                    //load cashier
                    List<EliteCommissionDetail> _Cashier = (from _res in _detail
                                                            where _res.Saec_emp_type == "CASHIER"
                                                            select _res).ToList<EliteCommissionDetail>();

                    grvCashier.AutoGenerateColumns = false;
                    BindingSource _source2 = new BindingSource();
                    _source2.DataSource = _Cashier;
                    grvCashier.DataSource = _source2;
                    grvCashier.Enabled = false;

                    //load helper
                    List<EliteCommissionDetail> _Helper = (from _res in _detail
                                                           where _res.Saec_emp_type == "HELPER"
                                                           select _res).ToList<EliteCommissionDetail>();

                    grvHelper.AutoGenerateColumns = false;
                    BindingSource _source3 = new BindingSource();
                    _source3.DataSource = _Helper;
                    grvHelper.DataSource = _source3;
                    grvHelper.Enabled = false;

                    //load co-head
                    List<EliteCommissionDetail> _Cohead = (from _res in _detail
                                                           where _res.Saec_emp_type == "CH_HEAD"
                                                           select _res).ToList<EliteCommissionDetail>();

                    grvCoHead.AutoGenerateColumns = false;
                    BindingSource _source4 = new BindingSource();
                    _source4.DataSource = _Cohead;
                    grvCoHead.DataSource = _source4;
                    grvCoHead.Enabled = false;

                    
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
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Process();
                this.Cursor = Cursors.Default;
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
                if (txtCircularNo.Text == "")
                {
                    MessageBox.Show("Please select circular to process", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DateTime _from =DateTime.Now;
                DateTime _to =DateTime.Now;
                if (rdoMonth.Checked)
                {
                    _from = new DateTime(Convert.ToInt32(udYear.Value), Convert.ToInt32(udMonth.Value), 1);
                    _to = new DateTime(Convert.ToInt32(udYear.Value), Convert.ToInt32(udMonth.Value), DateTime.DaysInMonth(Convert.ToInt32(udYear.Value), Convert.ToInt32(udMonth.Value)));
                }
                else {
                    _from = dtFrom1.Value.Date;
                    _to = dtTo1.Value.Date;
                }
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
                if (!grpExchange.Visible)
                {
                    List<EliteCommission> _errors = new List<EliteCommission>();
                    string err;
                    CHNLSVC.Sales.EliteCommissionProcess(txtCircularNo.Text, _pcList, _from, _to, BaseCls.GlbUserComCode, Convert.ToInt32(udYear.Value), Convert.ToInt32(udMonth.Value), BaseCls.GlbUserID, DateTime.Now, out _errors, false, out err);
                    if (_errors != null && _errors.Count > 0)
                    {
                        //view message
                        pnlMain.Enabled = false;
                        pnlPopUp.Visible = true;

                        grvErrors.AutoGenerateColumns = false;
                        grvErrors.DataSource = _errors;

                        if (MessageBox.Show("Errors occurred while processing!\nDo you want to continue process?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            CHNLSVC.Sales.EliteCommissionProcess(txtCircularNo.Text, _pcList, _from, _to, BaseCls.GlbUserComCode, Convert.ToInt32(udYear.Value), Convert.ToInt32(udMonth.Value), BaseCls.GlbUserID, DateTime.Now, out _errors, true, out err);
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
                else {
                    List<EliteCommission> _errors = new List<EliteCommission>();
                    string err;
                    decimal _exgRt = Convert.ToDecimal(txtExgRate.Text);
                    CHNLSVC.Sales.EliteCommissionProcessWithExchangeRate(txtCircularNo.Text, _pcList, _from, _to, BaseCls.GlbUserComCode, Convert.ToInt32(udYear.Value), Convert.ToInt32(udMonth.Value), BaseCls.GlbUserID, DateTime.Now, out _errors, false, out err,_exgRt);
                    if (_errors != null && _errors.Count > 0)
                    {
                        //view message
                        pnlMain.Enabled = false;
                        pnlPopUp.Visible = true;

                        grvErrors.AutoGenerateColumns = false;
                        grvErrors.DataSource = _errors;

                        if (MessageBox.Show("Errors occurred while processing!\nDo you want to continue process?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            CHNLSVC.Sales.EliteCommissionProcess(txtCircularNo.Text, _pcList, _from, _to, BaseCls.GlbUserComCode, Convert.ToInt32(udYear.Value), Convert.ToInt32(udMonth.Value), BaseCls.GlbUserID, DateTime.Now, out _errors, true, out err);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erroe Occured while processing\n"+ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void rdoMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoMonth.Checked)
            {
                grpDtRange.Enabled = false;
                grpMonth.Enabled = true;
            }
            else {
                grpDtRange.Enabled = true;
                grpMonth.Enabled = false;
            }
        }

        private void rdoDtRange_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDtRange.Checked)
            {
                grpDtRange.Enabled = true;
                grpMonth.Enabled = false;
            }
            else
            {
                grpDtRange.Enabled = false;
                grpMonth.Enabled = true;
            }
        }

        private void txtExgRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
    }

  
}
