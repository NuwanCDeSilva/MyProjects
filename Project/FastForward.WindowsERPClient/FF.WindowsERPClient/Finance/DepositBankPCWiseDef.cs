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
    public partial class DepositBankPCWiseDef : Base
    {
        Deposit_Bank_Pc_wise objDepositBank;
        List<Deposit_Bank_Pc_wise> _lstDeposit;
        List<Deposit_Bank_Pc_wise> _lstPayMode;
        //bool chkPay;
        //bool chk;
        

        public DepositBankPCWiseDef()
        {
            InitializeComponent();
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
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GPC:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OPE:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
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
            if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPS"))
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

        private void button1_Click(object sender, EventArgs e)
        {
            lstPC.Clear();
        }

        private void txtComp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtComp;
                _CommonSearch.ShowDialog();
                txtComp.Select();
            }
        }

        private void txtChanel_MouseDoubleClick(object sender, MouseEventArgs e)
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
        }

        private void txtChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtChanel_MouseDoubleClick(null, null);
            }
        }

        private void txtSChanel_MouseDoubleClick(object sender, MouseEventArgs e)
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
        }

        private void txtSChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtSChanel_MouseDoubleClick(null, null);
            }
        }

        private void txtArea_MouseDoubleClick(object sender, MouseEventArgs e)
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
        }

        private void txtArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtArea_MouseDoubleClick(null, null);
                
            }
        }

        private void txtRegion_MouseDoubleClick(object sender, MouseEventArgs e)
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
        }

        private void txtRegion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtRegion_MouseDoubleClick(null, null);
            }
        }

        private void txtZone_MouseDoubleClick(object sender, MouseEventArgs e)
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

        }

        private void txtZone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtZone_MouseDoubleClick(null, null);
            }
        }

        private void txtPC_MouseDoubleClick(object sender, MouseEventArgs e)
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

        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtPC_MouseDoubleClick(null, null);
            }
        }


        private void LoadPayModes()
        {
            DataTable dt = CHNLSVC.Sales.GetPayModeDetas();
            if (dt != null && dt.Rows.Count > 0)
            {
                dgvPayModeDet.DataSource = dt;
            }
            else
            {
                dgvPayModeDet.Visible = false;
            }
        }

        private void LoadBankDetails(string company)
        {
            DataTable dt = CHNLSVC.Sales.GetBankDetais(company);
            if (dt != null && dt.Rows.Count > 0)
            {
                dgvBankDetails.DataSource = dt;
            }
            else
            {
                dgvBankDetails.Visible = false;
            }
        }


        #region Form_Load
        private void DepositBankPCWiseDef_Load(object sender, EventArgs e)
        {
            txtComp.Text = BaseCls.GlbUserComCode;
            LoadPayModes();
            LoadBankDetails(BaseCls.GlbUserComCode);
          
          
        }
        #endregion

        private List<Deposit_Bank_Pc_wise> fillToDepositBankDetails()
        {
            _lstDeposit = new List<Deposit_Bank_Pc_wise>();
            _lstPayMode = new List<Deposit_Bank_Pc_wise>();

            List<string> _pcList = new List<string>();

            foreach (ListViewItem itm in lstPC.Items)
            {
                if (itm.Checked)
                {
                    _pcList.Add(itm.Text);
                }
            }

              //save pc
            foreach (string _pc in _pcList)
            {

                //save Pay Mode
                foreach(DataGridViewRow gv in dgvPayModeDet.Rows)
                {
                    bool chkPay = Convert.ToBoolean(gv.Cells["isSelect"].Value);
                    if (chkPay)
                    {
                        objDepositBank = new Deposit_Bank_Pc_wise();
                        objDepositBank.Pay_mode_code = gv.Cells["clmPaymodeCode"].Value.ToString();
                        _lstPayMode.Add(objDepositBank);


                        //save Bank Details
                        foreach (DataGridViewRow dgvr in dgvBankDetails.Rows)
                        {

                            //bool chkPay = Convert.ToBoolean(gv.Cells["isSelect"].Value);
                            //if (chkPay)
                            //{
                                bool chk = Convert.ToBoolean(dgvr.Cells["option"].Value);
                                if (chk)
                                {

                                    objDepositBank = new Deposit_Bank_Pc_wise();
                                    objDepositBank.Company = BaseCls.GlbUserComCode;
                                    objDepositBank.Profit_center = _pc;
                                    objDepositBank.Pay_mode_code = gv.Cells["clmPaymodeCode"].Value.ToString();
                                    objDepositBank.BankCode = dgvr.Cells["clmBankCode"].Value.ToString();
                                    objDepositBank.BankName = dgvr.Cells["clmBankName"].Value.ToString();
                                    objDepositBank.SunAccNo = dgvr.Cells["clmSunAccNum"].Value.ToString();
                                    objDepositBank.BankAccNo = dgvr.Cells["clmBankAccNo"].Value.ToString();
                                    if (Convert.ToBoolean(dgvr.Cells["Default"].Value) == true)
                                    {
                                        objDepositBank.Default_val = 1;
                                    }
                                    else
                                    {
                                        objDepositBank.Default_val = 0;
                                    }
                                    objDepositBank.Createby = BaseCls.GlbUserID;
                                    objDepositBank.CreateDateTime = DateTime.Now;
                                    objDepositBank.Modifyby = BaseCls.GlbUserID;

                                    _lstDeposit.Add(objDepositBank);


                                }


                            //}
                            //else
                            //{
                            //    //MessageBox.Show("");
                            //}


                        }

                    }

                }

            }

            if (_pcList.Count <= 0)
            {
                MessageBox.Show("Please select applicable profit center(s)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else if (_lstPayMode.Count <= 0)
            {
                MessageBox.Show("Please select pay mode(s)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
            else if (_lstDeposit.Count <= 0)
            {
                 MessageBox.Show("Please select deposit account(s)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }



            return _lstDeposit;
        }


        private void dgvBankDetails_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            UpdateCellValue(e.RowIndex);
        }

        private void UpdateCellValue(int CurrentRowIndex)
        {
             for (int row = 0; row < dgvBankDetails.Rows.Count; row++)
             {
                 if (CurrentRowIndex != row)
                 {

                     dgvBankDetails.Rows[row].Cells["Default"].Value = false;
                 }
                 else
                 {
                     dgvBankDetails.Rows[row].Cells["Default"].Value = true;
                 }
             }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtArea.Text = "";
            txtChanel.Text = "";
            txtComp.Text = "";
            txtCompAddr.Text = "";
            txtCompDesc.Text = "";
            txtPC.Text = "";
            txtPCDesn.Text = "";
            txtRegion.Text = "";
            txtSChanel.Text = "";
            txtZone.Text = "";
            lstPC.Items.Clear();

            //dgvBankDetails.DataSource = null;
           // dgvPayModeDet.DataSource = null;
            txtComp.Text = BaseCls.GlbUserComCode;
            foreach (DataGridViewRow gvr in dgvPayModeDet.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gvr.Cells["isSelect"];
                chk.Value = false;
            
            }
            foreach (DataGridViewRow gvr in dgvBankDetails.Rows)
            {
                DataGridViewCheckBoxCell chkB = (DataGridViewCheckBoxCell)gvr.Cells["option"];
                DataGridViewCheckBoxCell chkDef = (DataGridViewCheckBoxCell)gvr.Cells["Default"];
                chkB.Value = false;
                chkDef.Value = false;
                
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _lstDeposit = new List<Deposit_Bank_Pc_wise>();

            #region validation

            if (lstPC.Items.Count <= 0)
            {
                MessageBox.Show("Please select applicable profit center(s)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

            _lstDeposit = fillToDepositBankDetails();

            

            if (_lstDeposit.Count > 0)
            {
                if (MessageBox.Show("Are you sure ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                string _error = "";
                int result = CHNLSVC.Sales.SaveToDipositBankDet(_lstDeposit, out _error);
                if (result == -1)
                {
                    MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    MessageBox.Show("Records inserted Successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnClear_Click(null, null);
                }
            }



        }

        private void LoadBanks()
        {
            DataTable dt = CHNLSVC.Sales.LoadBankNewDets();
            if (dt != null && dt.Rows.Count > 0)
            {
                //cmbBankName.Items.Insert(0, "Select a Bank");
                cmbBankName.DataSource = dt;
                cmbBankName.DisplayMember = "MBI_DESC";
                cmbBankName.ValueMember = "MBI_ID";
                cmbBankName.Text = "---Select a Bank---";
                
                //cmbBankName.SelectedIndex = 0;

            }
            else
            {
                cmbBankName.DataSource = null;
            }
        }

        private void btnClose_new_Click(object sender, EventArgs e)
        {
            txtlegerNo.Text = "";
            txtBankAccNo.Text = "";
            cmbBankName.DataSource = null;
            pnlCreateNew.Visible = false;
        }

        private void btnClearNew_Click(object sender, EventArgs e)
        {
            txtlegerNo.Text = "";
            txtBankAccNo.Text = "";
           // cmbBankName.SelectedIndex = 0;
            cmbBankName.Text = "---Select a Bank---";
        }

        private Deposit_Bank_Pc_wise FillToNewBankDetails()
        {
            objDepositBank = new Deposit_Bank_Pc_wise();
            objDepositBank.Company = BaseCls.GlbUserComCode;
            objDepositBank.BankCode = cmbBankName.SelectedValue.ToString();
            objDepositBank.BankName = cmbBankName.Text;
            objDepositBank.SunAccNo = txtlegerNo.Text.Trim();
            objDepositBank.BankAccNo = txtBankAccNo.Text.Trim();
            objDepositBank.Createby = BaseCls.GlbUserID;
            objDepositBank.Modifyby = BaseCls.GlbUserID;

            return objDepositBank;
        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            #region validation

            if (cmbBankName.Text == "---Select a Bank---")
            {
                MessageBox.Show("Please select Company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtlegerNo.Text.Trim() == "")
            {
                MessageBox.Show("Please enter Leger Account No", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtBankAccNo.Text.Trim() == "")
            {
                MessageBox.Show("Please enter Bank Account No", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #endregion

            try
            {
                if (MessageBox.Show("Are you sure ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;


                string _error = "";
                int result = CHNLSVC.Sales.InsertNewBankDets(FillToNewBankDetails(), out _error);
                if (result == -1)
                {
                    MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    MessageBox.Show("Records inserted Successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnClearNew_Click(null, null);
                    cmbBankName.DataSource = null;
                    LoadBankDetails(BaseCls.GlbUserComCode);
                    pnlCreateNew.Visible = false;

                }
            }
            catch (Exception ex)
            {
                
                //throw ex;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                CHNLSVC.CloseChannel();
            }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            //pnlCreateNew.Top = 360;
            //pnlCreateNew.Left = 507;
            pnlCreateNew.Visible = true;
            LoadBanks();
           
        }

        private void cmbBankName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)Keys.None;
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked == true)
            {
                foreach (DataGridViewRow gvr in dgvPayModeDet.Rows)
                {
                    
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gvr.Cells["isSelect"];
                    chk.Value = true;
                    lblSelect.Text = "Deselect All";
                }
            }
            else
            {
                foreach (DataGridViewRow gvr in dgvPayModeDet.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gvr.Cells["isSelect"];
                    chk.Value = false;
                    lblSelect.Text = "Select All";
                }
            }
        }

        private void chkBankAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBankAll.Checked == true)
            {
                foreach (DataGridViewRow gvr in dgvBankDetails.Rows)
                {

                    DataGridViewCheckBoxCell chkB = (DataGridViewCheckBoxCell)gvr.Cells["option"];
                    chkB.Value = true;
                    lblBankSelect.Text = "Deselect All";
                }
            }
            else
            {
                foreach (DataGridViewRow gvr in dgvBankDetails.Rows)
                {
                    DataGridViewCheckBoxCell chkB = (DataGridViewCheckBoxCell)gvr.Cells["option"];
                    chkB.Value = false;
                    lblBankSelect.Text = "Select All";
                }
            }
        }









    }
}
