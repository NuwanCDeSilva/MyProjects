using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using System.Linq;
using FF.WindowsERPClient.Reports.Inventory;

namespace FF.WindowsERPClient.Enquiries.Inventory
{
    public partial class SubLocationStock : Base
    {

        public SubLocationStock()
        {
            InitializeComponent();
            txtMLoc.Text = BaseCls.GlbUserDefLoca;
            txtMLoc_Leave(null, null);

        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (m.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = m.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }
            base.WndProc(ref m);
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.InsuCom:
                    {
                        paramsText.Append("INS" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SubLoc:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
     
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ServiceJobDetails:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.EMP_ALL:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DefectTypes:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Designation:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town_new:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Mobile:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceEstimate:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "2,3,5,6" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                    break;
            }

            return paramsText.ToString();
        }


        private void btnCloseFrom_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearAll()
        {

            txtMLoc.Clear();

            dtpDate.Value = DateTime.Today;
            dgvMain.DataSource = new List<Service_TempIssue>();
        }

        private void btn_srch_sub_loc_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SubLoc);
            DataTable _result = CHNLSVC.CommonSearch.SearchSubLocationData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSubLoc;
            _CommonSearch.ShowDialog();
            txtSubLoc.Select();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _dt = CHNLSVC.MsgPortal.getSubLocationStock(BaseCls.GlbUserComCode, txtMLoc.Text, txtSubLoc.Text, Convert.ToInt32(chkOprTil.Checked), Convert.ToDateTime(dtpDate.Value).Date, BaseCls.GlbUserID,0);
                dgvMain.AutoGenerateColumns = false;
                dgvMain.DataSource = _dt;

                decimal sum = Convert.ToDecimal(_dt.Compute("SUM(sls_stk_val)", string.Empty));
                txtStkValLoc.Text = FormatToCurrency(sum.ToString());

                decimal sum1 = Convert.ToDecimal(_dt.Compute("SUM(sls_ins_val)", string.Empty));
                txtInsValLoc.Text = FormatToCurrency(sum1.ToString());
            }
            catch (Exception err)
            { }
        }

        private void chkOprTil_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOprTil.Checked == true)
                dtpDate.Enabled = true;
            else
                dtpDate.Enabled = false;
        }

        private void chkSLoc_CheckedChanged(object sender, EventArgs e)
        {
            if(chkSLoc.Checked==true)
            {
                txtSubLoc.Text = "";
                txtSubLoc.Enabled = false;
                btn_srch_sub_loc.Enabled = false;
            }
            else
            {
                txtSubLoc.Enabled = true;
                btn_srch_sub_loc.Enabled = true;
            }
        }

        private void chkMLoc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMLoc.Checked == true)
            {
                txtMLoc.Text = "";
                txtMLoc.Enabled = false;
                btn_srch_loc.Enabled = false;
            }
            else
            {
                txtMLoc.Enabled = true;
                btn_srch_loc.Enabled = true;
            }
        }

        private void dgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex != -1)
                {
                    string _mLoc = dgvMain.Rows[e.RowIndex].Cells["sls_mloc"].Value.ToString();
                    string _sLoc = dgvMain.Rows[e.RowIndex].Cells["sls_sloc"].Value.ToString();

                    txtInsMloc.Text = _mLoc;
                    txtInsSloc.Text = _sLoc;

                    DataTable _dt = CHNLSVC.Inventory.Get_TempIssued_Items(BaseCls.GlbUserComCode, _mLoc, _sLoc, "TMPI");
                    grvItems.AutoGenerateColumns = false;
                    grvItems.DataSource = _dt;

                    decimal sum1 = Convert.ToDecimal(_dt.Compute("SUM(ins_unit_cost)", string.Empty));
                    txtStkValItm.Text = FormatToCurrency(sum1.ToString());

                    loadInsu_history(_mLoc, _sLoc);

                    DataTable _dtSubLoc = CHNLSVC.Sales.getSubLocationByCode(BaseCls.GlbUserComCode, _mLoc, _sLoc);
                    if(_dtSubLoc.Rows.Count>0)
                    {
                        dtpInsDt.Value = Convert.ToDateTime(_dtSubLoc.Rows[0]["MSL_INSU_DT"]);
                        txtInsVal.Text = _dtSubLoc.Rows[0]["MSL_INSU_VAL"].ToString();
                        txtRef.Text = _dtSubLoc.Rows[0]["MSL_COVER_REF"].ToString();
                    }


                }
            }
            catch (Exception err)
            { }
            
        }

        private void loadInsu_history(string mloc,string sloc)
        {
            DataTable _dtInsu = CHNLSVC.Financial.GetInsuHistory(BaseCls.GlbUserComCode, mloc, sloc);
            grvInsu.AutoGenerateColumns = false;
            grvInsu.DataSource = _dtInsu;
        }

        private void btnInsuClose_Click(object sender, EventArgs e)
        {
            pnlInsu.Visible = false;
        }

        private void btnUpd_Click(object sender, EventArgs e)
        {
            pnlInsu.Visible = true;
        }

        private void btn_srch_ins_Com_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsuCom);
            DataTable _result = CHNLSVC.CommonSearch.GetInsuCompany(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtInsComp;
            _CommonSearch.ShowDialog();
            txtInsComp.Select();
        }

        private void txtInsComp_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInsComp.Text))
            {
                MasterOutsideParty _OutPartyDetails = new MasterOutsideParty();
                _OutPartyDetails = CHNLSVC.Sales.GetOutSidePartyDetails(txtInsComp.Text.Trim(), "INS");

                if (_OutPartyDetails.Mbi_cd == null)
                {
                    MessageBox.Show("Invalid insuarance company.", "Sub Location Stock", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtInsComp.Text = "";
                    txtInsComp.Focus();
                    return;
                }
            }
        }

        private void btnUpdIns_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10128))
            {
                MessageBox.Show("Sorry, You have no permission to update the insurance details!\n( Advice: Required permission code :10128 )","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtInsMloc.Text) || string.IsNullOrEmpty(txtInsSloc.Text))
            {
                MessageBox.Show("Please select the Main/Sub location", "Sub Location Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtInsComp.Text) )
            {
                MessageBox.Show("Please select the insurance company", "Sub Location Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtRef.Text))
            {
                MessageBox.Show("Please select the cover note reference", "Sub Location Stock", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            MasterInsuHistory _insuHis = new MasterInsuHistory();
            _insuHis.Mih_com = BaseCls.GlbUserComCode;
            _insuHis.Mih_cre_by = BaseCls.GlbUserID;
            _insuHis.Mih_ins_amt = Convert.ToDecimal(txtInsVal.Text);
            _insuHis.Mih_ins_com = txtInsComp.Text;
            _insuHis.Mih_ins_dt = Convert.ToDateTime(dtpInsDt.Value).Date;
            _insuHis.Mih_ins_ref = txtRef.Text;
            _insuHis.Mih_mloc = txtInsMloc.Text;
            _insuHis.Mih_mod_by = BaseCls.GlbUserID;
            _insuHis.Mih_sloc = txtInsSloc.Text;

            int _eff = CHNLSVC.Financial.SaveInsuHistory(_insuHis);

            loadInsu_history(txtInsMloc.Text, txtInsSloc.Text);

            MessageBox.Show("Successfully saved.", "Sub Location Stock", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtRef.Text = "";
            txtInsMloc.Text = "";
            txtInsSloc.Text = "";
            txtInsVal.Text = "0.00";
            txtInsComp.Text = "";
            
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
          
        }

        private void clear()
        {
            txtRef.Text = "";
            txtInsMloc.Text = "";
            txtInsSloc.Text = "";
            txtInsVal.Text = "0.00";
            txtInsComp.Text = "";
            grvInsu.DataSource = null;
            grvItems.DataSource = null;
            dgvMain.DataSource = null;
            txtStkValItm.Text = "0.00";
            txtStkValLoc.Text = "0.00";
            txtInsValLoc.Text = "0.00";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            
        }

        private void txtMLoc_Leave(object sender, EventArgs e)
        {
            lblMloc.Text = "";
            if (!string.IsNullOrEmpty(txtMLoc.Text))
            {
                DataTable _dt = CHNLSVC.Sales.getLocDesc(BaseCls.GlbUserComCode, "LOC", txtMLoc.Text);
                if (_dt == null || _dt.Rows.Count <= 0)
                {
                    MessageBox.Show("Invalid location", "Delivery Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMLoc.Clear();
                    txtMLoc.Focus();
                    return;
                }
                else
                    lblMloc.Text = _dt.Rows[0]["descp"].ToString();
            }
        }

        private void txtSubLoc_Leave(object sender, EventArgs e)
        {
            lblSloc.Text = "";
            if (!string.IsNullOrEmpty(txtSubLoc.Text))
            {
                DataTable _dt = CHNLSVC.Sales.getSubLocationByCode(BaseCls.GlbUserComCode, txtMLoc.Text, txtSubLoc.Text);
                if (_dt == null || _dt.Rows.Count <= 0)
                {
                    MessageBox.Show("Invalid Sub location", "Delivery Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSubLoc.Clear();
                    txtSubLoc.Focus();
                    return;
                }
                else
                    lblSloc.Text = _dt.Rows[0]["MSL_SLOC_DESC"].ToString();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _dt = CHNLSVC.MsgPortal.getSubLocationStock(BaseCls.GlbUserComCode, txtMLoc.Text, txtSubLoc.Text, Convert.ToInt32(chkOprTil.Checked), Convert.ToDateTime(dtpDate.Value).Date, BaseCls.GlbUserID,0);
                dgvMain.AutoGenerateColumns = false;
                dgvMain.DataSource = _dt;

                decimal sum = Convert.ToDecimal(_dt.Compute("SUM(sls_stk_val)", string.Empty));
                txtStkValLoc.Text = FormatToCurrency(sum.ToString());

                decimal sum1 = Convert.ToDecimal(_dt.Compute("SUM(sls_ins_val)", string.Empty));
                txtInsValLoc.Text = FormatToCurrency(sum1.ToString());
            }
            catch (Exception err)
            { }
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear the screen?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                clear();
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            BaseCls.GlbReportDoc = txtMLoc.Text;
            BaseCls.GlbReportDoc1 = txtSubLoc.Text;
            if (chkOprTil.Checked)
                BaseCls.GlbReportParaLine1 = 1;
            else
                BaseCls.GlbReportParaLine1 = 0;

            BaseCls.GlbReportToDate = DateTime.Now.Date;
            if (chkWithDet.Checked == true)
            {
                BaseCls.GlbReportHeading = "Sub Location Stock Detail Report";
                BaseCls.GlbReportParaLine2 = 1;
            }
            else
            {
                BaseCls.GlbReportHeading = "Sub Location Stock Summary Report";
                BaseCls.GlbReportParaLine2 = 0;
            }

            ReportViewerInventory _view = new ReportViewerInventory();
            BaseCls.GlbReportName = "SubLocationStockVal.rpt";
            _view.GlbReportName = "SubLocationStockVal.rpt";
            _view.Show();
            _view = null;
        }

        private void txtInsComp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_ins_Com_Click(null, null);
        }

        private void txtInsComp_DoubleClick(object sender, EventArgs e)
        {
            btn_srch_ins_Com_Click(null, null);
        }

        private void txtInsVal_Leave(object sender, EventArgs e)
        {
            if (txtInsVal.Text != "")
            {
                decimal val;
                if (!decimal.TryParse(txtInsVal.Text, out val))
                {
                    MessageBox.Show("Invalid insurance value", "Sub Location Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtInsVal.Text = "0.00";
                    txtInsVal.Focus();
                    return;
                }
                else if (Convert.ToDecimal(txtInsVal.Text) < 0)
                {
                    MessageBox.Show("Invalid insurance value", "Sub Location Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtInsVal.Focus();
                    return;
                }
            }
        }

    }
}