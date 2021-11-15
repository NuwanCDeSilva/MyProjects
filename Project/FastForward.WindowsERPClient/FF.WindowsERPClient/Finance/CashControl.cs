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

namespace FF.WindowsERPClient.Finance
{
    public partial class CashControl : Base
    {
        public CashControl()
        {
            InitializeComponent();
            dtDate.Value = Convert.ToDateTime("01" + "/" + DateTime.Now.Date.Month + "/" + DateTime.Now.Date.Year).Date;
            txtAdjPC.Text = BaseCls.GlbUserDefProf;
            //load_grid_data();
            get_cash_control();
            lstPC.Items.Add(BaseCls.GlbUserDefProf);
            txtComp.Text = BaseCls.GlbUserComCode;
            GetCompanyDet(null, null);
        }

        protected void GetCompanyDet(object sender, EventArgs e)
        {
            try
            {
                MasterCompany _masterComp = null;
                _masterComp = CHNLSVC.General.GetCompByCode(txtComp.Text);
                if (_masterComp != null)
                {
                    txtCompDesc.Text = _masterComp.Mc_desc;
                    txtCompAddr.Text = _masterComp.Mc_add1 + _masterComp.Mc_add2;
                }
                else
                {
                    txtCompDesc.Text = "";
                    txtCompAddr.Text = "";
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Financial Reports Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void get_cash_control()
        {
            DateTime _tmpMonth = Convert.ToDateTime("01" + "/" + dtDate.Value.Date.Month + "/" + dtDate.Value.Date.Year).Date;
            DataTable _dt = CHNLSVC.Financial.GetcashControl(BaseCls.GlbUserComCode, txtAdjPC.Text, _tmpMonth);
            if (_dt.Rows.Count > 0)
            {
                if (_dt.Rows[0]["ccst_stus"].ToString() == "F")
                {
                    lblStus.Text = "FINALIZED";
                    btnAddAdj.Enabled = false;
                    //grvAdj.Enabled = false;

                }
                else
                {
                    lblStus.Text = "PENDING";
                }
            }
            else
            {
                lblStus.Text = "PENDING";
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(txtComp.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + txtRegion.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + txtRegion.Text + seperator + txtZone.Text + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
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

        private void txtChanel_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtChanel;
            _CommonSearch.ShowDialog();
            txtChanel.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "CHNL", txtChanel.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtChnlDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtSChanel_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSChanel;
            _CommonSearch.ShowDialog();
            txtSChanel.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "SCHNL", txtSChanel.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtSChnlDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtArea_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtArea;
            _CommonSearch.ShowDialog();
            txtArea.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "AREA", txtArea.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtAreaDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtRegion_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtRegion;
            _CommonSearch.ShowDialog();
            txtRegion.Select();


            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "REGION", txtRegion.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtRegDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtZone_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtZone;
            _CommonSearch.ShowDialog();
            txtZone.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "ZONE", txtZone.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtZoneDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtPC_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPC;
            _CommonSearch.ShowDialog();
            txtPC.Select();

            load_PCDesc();
        }

        private void load_PCDesc()
        {
            txtPCDesn.Text = "";
            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "PC", txtPC.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtPCDesn.Text = row2["descp"].ToString();
            }
        }

        private void txtSChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtSChanel_DoubleClick(null, null);
            }
        }

        private void txtArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtArea_DoubleClick(null, null);
            }
        }

        private void txtRegion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtRegion_DoubleClick(null, null);
            }
        }

        private void txtZone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtZone_DoubleClick(null, null);
            }
        }
        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtPC_DoubleClick(null, null);
            }
            if (e.KeyCode == Keys.Enter)
            {
                load_PCDesc();
            }
        }
        private void txtChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtChanel_DoubleClick(null, null);
            }
        }

        private void btnClearPC_Click(object sender, EventArgs e)
        {
            lstPC.Clear();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstPC.Items)
            {
                Item.Checked = true;
            }
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstPC.Items)
            {
                Item.Checked = false;
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            string com = txtComp.Text;
            string chanel = txtChanel.Text;
            string subChanel = txtSChanel.Text;
            string area = txtArea.Text;
            string region = txtRegion.Text;
            string zone = txtZone.Text;
            string pc = txtPC.Text;

            lstPC.Clear();
            string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPS"))
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10044))
            {
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(com, chanel, subChanel, area, region, zone, pc);
                foreach (DataRow drow in dt.Rows)
                {
                    lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                }
            }
            else
            {
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep(BaseCls.GlbUserID, com, chanel, subChanel, area, region, zone, pc);
                foreach (DataRow drow in dt.Rows)
                {
                    lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                }
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
                _CommonSearch.obj_TragetTextBox = txtAdjPC;
                _CommonSearch.ShowDialog();
                txtAdjPC.Select();

                get_cash_control();
                load_grid_data();

                lstPC.Clear();
                lstPC.Items.Add(txtAdjPC.Text);
                lstPC.Items[0].Checked = true;


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

        private void btnAddAdj_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblAdj.Text))
            {
                MessageBox.Show("Please select the adjustment type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAdjCode.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtAdjPC.Text))
            {
                MessageBox.Show("Please select the profit center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAdjPC.Focus();
                return;
            }
            if (Convert.ToDecimal(txtAmt.Text) < 0)
            {
                MessageBox.Show("Invalid Amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmt.Focus();
                return;
            }
            //foreach (DataGridViewRow row in grvAdj.Rows)
            //{
            //    if (row.Cells[5].Value.ToString().Equals(txtAdjCode.Text))
            //    {
            //        MessageBox.Show("Already available", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //}
            if (MessageBox.Show("Are you sure ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            dtDate.Value = Convert.ToDateTime("01" + "/" + dtDate.Value.Date.Month + "/" + dtDate.Value.Date.Year).Date;

            RemitanceSummaryDetail _remSumDet = new RemitanceSummaryDetail();

            DataTable dtESD_EPF_WHT = new DataTable();
            dtESD_EPF_WHT = CHNLSVC.Sales.Get_ESD_EPF_WHT(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(dtDate.Text).Date);

            Decimal ESD_rt = 0; Decimal EPF_rt = 0; Decimal WHT_rt = 0;
            if (dtESD_EPF_WHT.Rows.Count > 0)
            {
                ESD_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_ESD"]);
                EPF_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_EPF"]);
                WHT_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_WHT"]);

            }

            _remSumDet.Rem_com = BaseCls.GlbUserComCode;
            _remSumDet.Rem_pc = txtAdjPC.Text;
            _remSumDet.Rem_dt = Convert.ToDateTime(dtDate.Text).Date;
            _remSumDet.Rem_sec = "CASHCONTRL";
            _remSumDet.Rem_cd = txtAdjCode.Text;
            _remSumDet.Rem_sh_desc = lblAdj.Text.ToString();
            _remSumDet.Rem_lg_desc = lblAdj.Text.ToString();
            if (optCr.Checked == true)
            {
                _remSumDet.Rem_val = Convert.ToDecimal(txtAmt.Text) * -1;
                _remSumDet.Rem_val_final = Convert.ToDecimal(txtAmt.Text) * -1;
            }
            else
            {
                _remSumDet.Rem_val = Convert.ToDecimal(txtAmt.Text);
                _remSumDet.Rem_val_final = Convert.ToDecimal(txtAmt.Text);
            }
            _remSumDet.Rem_week = ("1S").ToString();
            _remSumDet.Rem_ref_no = "";
            _remSumDet.Rem_rmk = txtAdjRem.Text;
            _remSumDet.Rem_cr_acc = "";
            _remSumDet.Rem_db_acc = "";
            _remSumDet.Rem_cre_by = BaseCls.GlbUserID;
            _remSumDet.Rem_cre_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
            _remSumDet.Rem_is_sos = false;
            _remSumDet.Rem_is_dayend = false;
            _remSumDet.Rem_is_sun = false;
            _remSumDet.Rem_del_alw = false;
            _remSumDet.Rem_is_rem_sum = false;
            _remSumDet.Rem_cat = 12;
            _remSumDet.Rem_add = 0;
            _remSumDet.Rem_ded = 0;
            _remSumDet.Rem_net = 0;
            _remSumDet.Rem_epf = EPF_rt;
            _remSumDet.Rem_esd = ESD_rt;
            _remSumDet.Rem_wht = WHT_rt;
            _remSumDet.Rem_add_fin = 0;
            _remSumDet.Rem_ded_fin = 0;
            _remSumDet.Rem_net_fin = 0;
            _remSumDet.Rem_rmk_fin = "";
            _remSumDet.Rem_bnk_cd = "";
            _remSumDet.REM_REM_MONTH = Convert.ToDateTime("31/Dec/9999");

            _remSumDet.REM_CHQNO = "";
            _remSumDet.REM_CHQ_BANK_CD = "";
            _remSumDet.REM_CHQ_BRANCH = "";
            _remSumDet.REM_CHQ_DT = Convert.ToDateTime("31/Dec/2099");
            _remSumDet.REM_DEPOSIT_BANK_CD = "";
            _remSumDet.REM_DEPOSIT_BRANCH = "";

            int row_aff = CHNLSVC.Financial.SaveRemSummaryDetails(_remSumDet);

            load_grid_data();

            MessageBox.Show("Successfully added!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtAdjCode.Text = "";
            lblAdj.Text = "";
            txtAdjRem.Text = "";
            txtAmt.Text = "";
        }

        private void load_grid_data()
        {
            DateTime _tmpMonth = Convert.ToDateTime("01" + "/" + dtDate.Value.Date.Month + "/" + dtDate.Value.Date.Year).Date;
            DataTable _dt = CHNLSVC.Financial.GetcashControlAdj(BaseCls.GlbUserComCode, txtAdjPC.Text, _tmpMonth);
            grvAdj.AutoGenerateColumns = false;
            grvAdj.DataSource = _dt;

        }
        private void txtAdjPC_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAdjPC.Text))
            {
                Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(BaseCls.GlbUserComCode, txtAdjPC.Text);
                if (_IsValid == false)
                {
                    MessageBox.Show("Invalid Profit Center.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtAdjPC.Focus();
                    return;
                }
                get_cash_control();
                load_grid_data();
            }
        }

        private void load_adj_desc()
        {
            if (!string.IsNullOrEmpty(txtAdjCode.Text))
            {
                DataTable _DT = CHNLSVC.Sales.Load_Adj_Acc_Details("CASHCONTRL", txtAdjCode.Text);
                if (_DT.Rows.Count > 0)
                    lblAdj.Text = _DT.Rows[0]["SAJD_DESC"].ToString();
                else
                {
                    MessageBox.Show("Invalid Adjustment type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtAdjCode.Text = "";
                    lblAdj.Text = "";
                    txtAdjCode.Focus();
                }
            }
        }

        private void btn_srch_adj_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AdjType);
                DataTable _result = CHNLSVC.CommonSearch.searchFinAdjTypeData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAdjCode;
                _CommonSearch.ShowDialog();
                txtAdjCode.Select();
                load_adj_desc();

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

        private void txtAdjCode_Leave(object sender, EventArgs e)
        {
            load_adj_desc();
        }

        private void CashControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtAmt_Leave(object sender, EventArgs e)
        {
            decimal val;
            if (!decimal.TryParse(txtAmt.Text, out val))
            {
                MessageBox.Show("Amount has to be in number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmt.Focus();
                return;
            }
        }

        private void grvAdj_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (lblStus.Text=="FINALIZED")
                {
                    MessageBox.Show("Already finalized", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DateTime _tmpMonth = Convert.ToDateTime("01" + "/" + dtDate.Value.Date.Month + "/" + dtDate.Value.Date.Year).Date;
                    Int32 _x = CHNLSVC.Financial.DeleteRemSummary(BaseCls.GlbUserComCode, txtAdjPC.Text, _tmpMonth, "CASHCONTRL", grvAdj.Rows[e.RowIndex].Cells[6].Value.ToString());

                    load_grid_data();
                }
            }
        }

        private void dtDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime _procesMonth = new DateTime(dtDate.Value.Year, dtDate.Value.Month, 1);
            lblNote.Text = "This will be cancell all finalization from " + (_procesMonth).ToShortDateString();


            get_cash_control();
            if (!string.IsNullOrEmpty(txtAdjPC.Text))
                load_grid_data();
        }

        private void btnFinalize_Click(object sender, EventArgs e)
        {
            Boolean _isSel = false;
            if (lstPC.Items.Count == 0)
            {
                MessageBox.Show("Please select the profit center(s)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPC.Focus();
                return;
            }

            DateTime _tmpMonth = Convert.ToDateTime("01" + "/" + dtDate.Value.Date.Month + "/" + dtDate.Value.Date.Year).Date;

            this.Cursor = Cursors.WaitCursor;
            foreach (ListViewItem Item in lstPC.Items)
            {
                string pc = Item.Text;

                if (Item.Checked == true)
                {
                    _isSel = true;
                    Boolean _isFin = CHNLSVC.Financial.IsCashControlFinalized(BaseCls.GlbUserComCode, pc, _tmpMonth.AddMonths(-1));
                    if (_isFin == false)
                    {
                        MessageBox.Show("Process halted...\n" + pc + " is not finalized previous month", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;                
                    }
                    Boolean _isFinal = CHNLSVC.Financial.IsCashControlFinalized(BaseCls.GlbUserComCode, pc, _tmpMonth);
                    if (_isFinal == true)
                    {
                        MessageBox.Show("Process halted...\n" + pc + " is already finalized for the month", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    //kapila 1/4/2017
                    string _excsStatus = "";
                    string _ID = "";
                    DataTable DT = CHNLSVC.Financial.GetExcsStatus(pc, _tmpMonth, out _excsStatus, out _ID);
                    if (_excsStatus=="P")
                    {
                        MessageBox.Show("Process halted...\n" + pc + " is not confirmed the excess/short for the month", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
            }
            if (_isSel == false)
            {
                MessageBox.Show("Please select the profit center(s)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Cursor = Cursors.Default;
                return;
            }
            this.Cursor = Cursors.Default;

            if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            this.Cursor = Cursors.WaitCursor;
            foreach (ListViewItem Item in lstPC.Items)
            {
                string pc = Item.Text;

                if (Item.Checked == true)
                {
                    Int32 _eff = CHNLSVC.Financial.UpdateCashControl(BaseCls.GlbUserComCode, pc, _tmpMonth);
                    lblStus.Text = "FINALIZED";
                }
            }
            this.Cursor = Cursors.Default;

            MessageBox.Show("Successfully finalized!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void txtAdjPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnProfitCenter_Click(null, null);
        }

        private void txtAdjCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_srch_adj_Click(null, null);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            clsFinanceRep objHp = new clsFinanceRep();

            if (string.IsNullOrEmpty(txtAdjPC.Text))
            {
                    MessageBox.Show("Select the profit center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtAdjPC.Focus();
                    return;
            }
            DateTime _tmpMonth = Convert.ToDateTime("01" + "/" + dtDate.Value.Date.Month + "/" + dtDate.Value.Date.Year).Date;

            DateTime firstOfNextMonth = new DateTime(_tmpMonth.Year, _tmpMonth.Month, 1).AddMonths(1);
            DateTime lastOfThisMonth = firstOfNextMonth.AddDays(-1);

            //check whether last month is finalized
            Boolean _isFin = CHNLSVC.Financial.IsCashControlFinalized(BaseCls.GlbUserComCode, txtAdjPC.Text, _tmpMonth.AddMonths(-1));
            if (_isFin == false)
            {
                MessageBox.Show("Previous month is not finalized!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            MasterCompany _masterComp = new MasterCompany();
            _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
            if (_masterComp != null)
            {
                BaseCls.GlbReportComp = _masterComp.Mc_desc;
                BaseCls.GlbReportCompAddr = _masterComp.Mc_add1 + _masterComp.Mc_add2;
            }
            else
            {
                BaseCls.GlbReportComp = "";
                BaseCls.GlbReportCompAddr = "";
            }

            BaseCls.GlbReportProfit = txtAdjPC.Text;
            BaseCls.GlbReportCompCode = BaseCls.GlbUserComCode;

            BaseCls.GlbReportFromDate = _tmpMonth;
            BaseCls.GlbReportToDate = lastOfThisMonth;
            BaseCls.GlbReportName = "CashControl.rpt";
            BaseCls.GlbReportYear = dtDate.Value.Year;
            BaseCls.GlbReportMonth = dtDate.Value.Month;
            if (lblStus.Text == "FINALIZED")
                BaseCls.GlbReportStrStatus = "F";
            else
                BaseCls.GlbReportStrStatus = "P";

            objHp.CashControl();

            MasterCompany _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
            string _path = _MasterComp.Mc_anal6;
            objHp._cashControl.ExportToDisk(ExportFormatType.Excel, _path + "CashControl" + BaseCls.GlbUserID + ".xls");

            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;
            string workbookPath = _path + "CashControl" + BaseCls.GlbUserID + ".xls";
            Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(workbookPath,
                    0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                    true, false, 0, true, false, false);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAdjCode.Text = "";
            txtAdjPC.Text = "";
            txtAdjRem.Text = "";
            grvAdj.DataSource = null;
            txtRem.Text = "";
            lblAdj.Text = "";
            lblStus.Text = "";

        }

        private void txtAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtAdjPC.Text, "RMVCSHCNT"))
            {
                if (MessageBox.Show("Are you sure ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                DateTime _procesMonth = new DateTime(dtDate.Value.Year, dtDate.Value.Month, 1);
                Int32 _eff = CHNLSVC.Financial.CancelCashControlFinal(BaseCls.GlbUserComCode, txtAdjPC.Text, _procesMonth, BaseCls.GlbUserID);
                MessageBox.Show("Successfully Cancelled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Access Denied !. \n Permission code : RMVCSHCNT", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lstPC.Items.Count==0)
            {
                MessageBox.Show("Select the profit center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            string _errLocList = "";
            DateTime _tmpMonth = Convert.ToDateTime("01" + "/" + dtDate.Value.Date.Month + "/" + dtDate.Value.Date.Year).Date;

            DateTime firstOfNextMonth = new DateTime(_tmpMonth.Year, _tmpMonth.Month, 1).AddMonths(1);
            DateTime lastOfThisMonth = firstOfNextMonth.AddDays(-1);

            //check whether last month is processed
            DataTable _isFin = new DataTable();
            foreach (ListViewItem Item in lstPC.Items)
            {
                string pc = Item.Text;

                if (Item.Checked == true)
                {
                    _isFin = CHNLSVC.Financial.GetcashControl(BaseCls.GlbUserComCode, pc, _tmpMonth.AddMonths(-1));
                    if (_isFin.Rows.Count==0)
                    {

                        if (!string.IsNullOrEmpty(_errLocList))
                            _errLocList = _errLocList + ", " + pc;
                        else
                            _errLocList = pc;
                    }
                }
            }

            if (!string.IsNullOrEmpty(_errLocList))
            {
                MessageBox.Show("Following location(s) not finalized previous month\n" + _errLocList, "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string _Stus = "";
            DataTable cashcontrol = null;

            BaseCls.GlbReportFromDate = _tmpMonth;
            BaseCls.GlbReportToDate = lastOfThisMonth;
            BaseCls.GlbReportYear = dtDate.Value.Year;
            BaseCls.GlbReportMonth = dtDate.Value.Month;

            foreach (ListViewItem Item in lstPC.Items)
            {
                string pc = Item.Text;

                if (Item.Checked == true)
                {
                    DataTable _dt = CHNLSVC.Financial.GetcashControl(BaseCls.GlbUserComCode, pc, _tmpMonth);
                    if (_dt.Rows.Count > 0)
                    {
                        if (_dt.Rows[0]["ccst_stus"].ToString() == "F")
                            _Stus = "F";
                        else
                            _Stus = "P";
                    }
                    else
                        _Stus = "P";

                    cashcontrol = CHNLSVC.Financial.CashControlPrint(BaseCls.GlbUserID, BaseCls.GlbUserComCode, pc, BaseCls.GlbReportYear, BaseCls.GlbReportMonth, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, _Stus);
                }

            }
            MessageBox.Show("Successfully processed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
