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
    public partial class VoucherEnquiry : Base
    {
        public VoucherEnquiry()
        {
            InitializeComponent();
        }

        private void grvHeader_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {

                    List<VoucherDetails> _details = CHNLSVC.Financial.GetVoucherDetail(grvHeader.Rows[e.RowIndex].Cells[1].Value.ToString());

                    grvDetails.AutoGenerateColumns = false;
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _details;
                    grvDetails.DataSource = _source;

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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                int status = 0;
                int print = 0;
                if (chkStatus.Checked)
                {
                    if (rdoActive.Checked)
                    {
                        status = 1;
                    }
                    else
                    {
                        status = 0;
                    }
                }
                else
                    status = 3;

                if (chkPrint.Checked)
                {
                    if (rdoPrinted.Checked)
                    {
                        print = 1;
                    }
                    else
                    {
                        print = 0;
                    }
                }
                else
                    print = 3;

                string vou = "";
                if (txtRef.Text == "")
                    vou = "N/A";
                else
                    vou = txtRef.Text;


                List<VoucherHeader> _list = CHNLSVC.Financial.GetVoucherSearch(status, print, BaseCls.GlbUserComCode, dateTimePickerFrom.Value.Date, dateTimePickerTo.Value.Date, vou);
                grvHeader.AutoGenerateColumns = false;
                BindingSource _source = new BindingSource();
                _source.DataSource = _list;
                grvHeader.DataSource = _source;
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
            try
            {
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                rdoActive.Checked = false;
                rdoCancel.Checked = false;
                rdoNonPrinted.Checked = false;
                rdoPrinted.Checked = false;
                dateTimePickerFrom.Value = _date;
                dateTimePickerTo.Value = _date;

                rdoActive.Checked = true;
                rdoPrinted.Checked = true;
                grvDetails.DataSource = null;
                grvHeader.DataSource = null;

                pnl1.Enabled = true;
                pnl2.Enabled = true;
                chkPrint.Checked = true;
                chkStatus.Checked = true;
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

        private void VoucherEnquiry_Load(object sender, EventArgs e)
        {
            try
            {
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                dateTimePickerFrom.Value = _date.AddDays(-1);
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

        private void chkStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (chkStatus.Checked)
            { pnl1.Enabled = true; }
            else
            { pnl1.Enabled = false; }
        }

        private void chkPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPrint.Checked)
            { pnl2.Enabled = true; }
            else
            { pnl2.Enabled = false; }
        }

        private void btnVouNo_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.VoucherNo);
                DataTable _result = CHNLSVC.CommonSearch.GetVoucheNos(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRef;
                _CommonSearch.ShowDialog();
                txtRef.Select();
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
                case CommonUIDefiniton.SearchUserControlType.VoucherNo:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
    }
}
