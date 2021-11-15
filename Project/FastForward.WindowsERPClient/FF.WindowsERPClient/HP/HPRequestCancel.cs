using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.WindowsERPClient.Reports.Finance;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms.Internal.Win32;
using Excel = Microsoft.Office.Interop.Excel;

namespace FF.WindowsERPClient.HP
{
    public partial class HPRequestCancel : Base
    {
        public HPRequestCancel()
        {
            InitializeComponent();
            //txtPC.Text = BaseCls.GlbUserDefProf;
            dtFrom.Value = Convert.ToDateTime(DateTime.Now.Date);
            dtTo.Value = Convert.ToDateTime(DateTime.Now.Date);

        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.ApprovePermCode:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AdjType:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "CASHCONTRL" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnProfitCenter_Click(null, null);
            }
        }


        private void btnProfitCenter_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPC;
                _CommonSearch.ShowDialog();
                chkPc.Checked = false;
                txtPC.Select();

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


        private void load_grid_data()
        {
            DataTable _dt = null;
            _dt = CHNLSVC.Financial.GetApprovedRequests(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(dtFrom.Value), Convert.ToDateTime(dtTo.Value), Convert.ToInt32(chkDt.Checked), txtReqTp.Text);
            grvApprovedReq.AutoGenerateColumns = false;
            grvApprovedReq.DataSource = _dt;

            if (_dt.Rows.Count == 0)
                lblUsrMsg.Visible = true;
            else
                lblUsrMsg.Visible = false;

        }


        private void load_adj_desc()
        {
            if (!string.IsNullOrEmpty(txtReqTp.Text))
            {
                DataTable _DT = CHNLSVC.Sales.Load_Adj_Acc_Details("CASHCONTRL", txtReqTp.Text);
                if (_DT.Rows.Count > 0)
                    lblAdj.Text = _DT.Rows[0]["SAJD_DESC"].ToString();
                else
                {
                    MessageBox.Show("Invalid Adjustment type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtReqTp.Text = "";
                    lblAdj.Text = "";
                    txtReqTp.Focus();
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (chkDt.Checked == true)
            {
                if (dtFrom.Value.Date > dtTo.Value.Date)
                { MessageBox.Show("Please check the date range.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            }


            load_grid_data();
        }

        private void chkPc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPc.Checked == true)
            {
                txtPC.Text = "";
                txtPC.Enabled = false;
            }
            else
            {
                txtPC.Enabled = true;
            }
        }

        private void chkReqTp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReqTp.Checked == true)
            {
                txtReqTp.Text = "";
                txtReqTp.Enabled = false;
            }
            else
            {
                txtReqTp.Enabled = true;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            string _err = "";
            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            foreach (DataGridViewRow row in grvApprovedReq.Rows)
            {
                DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    //check permission
                    if (CHNLSVC.Financial.CheckAppReqCancelPerm(BaseCls.GlbUserID, Convert.ToString(row.Cells["grah_app_tp"].Value)) == true)
                    {
                        Int32 _eff = CHNLSVC.Financial.CancelApprovedRequest(Convert.ToString(row.Cells["Grah_ref"].Value), BaseCls.GlbUserID);
                    }
                    else
                    {
                        MessageBox.Show("Access Denied for " + Convert.ToString(row.Cells["grah_app_tp"].Value), "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                }
            }
            MessageBox.Show("Successfully Cancelled !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnClear_Click(null, null);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DataTable _dt = null;
            grvApprovedReq.AutoGenerateColumns = false;
            grvApprovedReq.DataSource = _dt;

            txtPC.Text = "";
            txtReqTp.Text = "";
            dtFrom.Value = Convert.ToDateTime(DateTime.Now.Date);
            dtTo.Value = Convert.ToDateTime(DateTime.Now.Date);
        }

        private void btn_srch_adj_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ApprovePermCode);
            DataTable _result = CHNLSVC.CommonSearch.Search_Approve_Permissions(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtReqTp;
            _CommonSearch.ShowDialog();
            chkReqTp.Checked = false;
            txtReqTp.Select();
        }

        private void chkDt_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDt.Checked == true)
            {
                dtFrom.Enabled = true;
                dtTo.Enabled = true;
            }
            else
            {
                dtFrom.Enabled = false;
                dtTo.Enabled = false;
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAll.Checked == true)
                {
                    foreach (DataGridViewRow row in grvApprovedReq.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = true;
                    }
                    grvApprovedReq.EndEdit();

                }
                else
                {
                    foreach (DataGridViewRow row in grvApprovedReq.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = false;
                    }
                    grvApprovedReq.EndEdit();

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtPC_Leave(object sender, EventArgs e)
        {
            if (chkPc.Checked == false)
            {
                if (!string.IsNullOrEmpty(txtPC.Text))
                {
                    Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(BaseCls.GlbUserComCode, txtPC.Text);
                    if (_IsValid == false)
                    {
                        MessageBox.Show("Invalid Profit Center.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPC.Focus();
                        return;
                    }
                }
            }
        }

        private void grvApprovedReq_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void txtPC_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnProfitCenter_Click(null, null);
        }

        private void txtPC_DoubleClick(object sender, EventArgs e)
        {
            btnProfitCenter_Click(null, null);
        }

        private void txtReqTp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnProfitCenter_Click(null, null);
        }

        private void txtReqTp_DoubleClick(object sender, EventArgs e)
        {
            btnProfitCenter_Click(null, null);
        }

        private void txtReqTp_Leave(object sender, EventArgs e)
        {
            if (chkReqTp.Checked == false)
            {
                if (!string.IsNullOrEmpty(txtReqTp.Text))
                {
                    if (CHNLSVC.Financial.isValidReqType(txtReqTp.Text) == false)
                    {
                        MessageBox.Show("Invalid Request Type.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtReqTp.Focus();
                        return;
                    }
                }
            }
        }

        private void grvApprovedReq_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (grvApprovedReq.IsCurrentCellDirty)
                grvApprovedReq.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }


    }
}
