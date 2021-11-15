using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FF.BusinessObjects;

namespace FF.WindowsERPClient.HP
{
    public partial class AgrementChecklist : Base
    {
        #region variables

        List<HpAccountChecklistDet> _checlistDetails=new List<HpAccountChecklistDet>();

        #endregion

        public AgrementChecklist()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPODNo.Text))
            {
                MessageBox.Show("please enter POD number to proceed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPODNo.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtAccNo.Text))
            {
                HpAccountChecklistDet _detDt = new HpAccountChecklistDet();
                List<HpAccount> _accListDt = CHNLSVC.General.GetHpAccountsByDtRange(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, dtpFrom.Value.Date, dtpTo.Value.Date);
                if (_accListDt != null && _accListDt.Count > 0)
                {
                    foreach (HpAccount _tmpAcc in _accListDt)
                    {
                        List<HpAccountChecklistDet> _dupList = (from _res in _checlistDetails
                                                                where _res.Agrd_acc_no == _tmpAcc.Hpa_acc_no
                                                                select _res).ToList<HpAccountChecklistDet>();
                        if (_dupList != null && _dupList.Count > 0)
                        {

                        }
                        else
                        {
                            //add account to list
                            _detDt = new HpAccountChecklistDet();
                            _detDt.Agrd_acc_no = _tmpAcc.Hpa_acc_no;
                            _detDt.Agrd_doc_no = txtPODNo.Text;
                            _checlistDetails.Add(_detDt);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Cannot find account details for the selected period.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                HpAccount account = CHNLSVC.Sales.GetHP_Account_onAccNo(txtAccNo.Text);
                if (account == null)
                {
                    MessageBox.Show("Invalid Account Number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (account.Hpa_pc != BaseCls.GlbUserDefProf)
                {
                    MessageBox.Show("Can not add other pc accounts", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<HpAccountChecklistDet> _dupList = (from _res in _checlistDetails
                                                        where _res.Agrd_acc_no == txtAccNo.Text
                                                        select _res).ToList<HpAccountChecklistDet>();
                if (_dupList != null && _dupList.Count > 0)
                {
                    MessageBox.Show("Duplicate account number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //add account to list
                HpAccountChecklistDet _det = new HpAccountChecklistDet();
                _det.Agrd_acc_no = txtAccNo.Text;
                _det.Agrd_doc_no = txtPODNo.Text;
                _checlistDetails.Add(_det);
            }
            LoadAccountGrid();

            txtPODNo.ReadOnly = true;

        }

        private void LoadAccountGrid()
        {
            gvAccounts.AutoGenerateColumns = false;
            BindingSource _sou = new BindingSource();
            _sou.DataSource = _checlistDetails;
            gvAccounts.DataSource = _sou;
        }

        private void buttonSearchAcc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
                DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAccNo;
                _CommonSearch.ShowDialog();
                _CommonSearch.IsSearchEnter = true;
                txtAccNo.Select();
               
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

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + null + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AccountChecklist:
                    {
                        int isDate = 0;
                        if (dtFrom.Value.Date != DateTime.Now.Date || dtTo.Value.Date != DateTime.Now.Date) {
                            isDate = 1;
                        }

                        paramsText.Append(txtPc.Text + seperator + isDate + seperator + dtFrom.Value.ToString("dd-MMM-yyyy") + seperator + dtTo.Value.ToString("dd-MMM-yyyy") + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AccountChecklistPOD:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator +BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        private void gvAccounts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1) {
                if (e.ColumnIndex == 0) {
                    if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                        _checlistDetails.RemoveAt(e.RowIndex);

                        LoadAccountGrid();
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //validation
                if (string.IsNullOrEmpty(txtPODNo.Text))
                {
                    MessageBox.Show("Please enter pod number first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (_checlistDetails.Count <= 0)
                {
                    MessageBox.Show("Please enter atleast one account", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                HPAccountChecklistHdr _temHdr = CHNLSVC.Sales.GetPODNo(txtPODNo.Text);
                if (_temHdr != null)
                {
                    MessageBox.Show("POD Number already in the database", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //save process
                HPAccountChecklistHdr _hdr = new HPAccountChecklistHdr();
                _hdr.Agrh_pod_no = txtPODNo.Text;
                _hdr.Agrh_com = BaseCls.GlbUserComCode;
                _hdr.Agrh_pc = BaseCls.GlbUserDefProf;
                _hdr.Agrh_sw_confirm = true;
                _hdr.Agrh_cre_by = BaseCls.GlbUserID;
                _hdr.Agrh_cre_dt = DateTime.Now;
                _hdr.Agrh_mod_by = BaseCls.GlbUserID;
                _hdr.Agrh_mod_dt = DateTime.Now;
                _hdr.Agrh_dt = dtDate.Value.Date;

                //get checked account list
                List<HpAccountChecklistDet> _detList = new List<HpAccountChecklistDet>();
                gvAccounts.EndEdit();
                foreach (DataGridViewRow row in gvAccounts.Rows)
                {
                    if (row.Cells[3].Value != null)
                    {
                        if ((row.Cells[3].FormattedValue.ToString().ToUpper()) == "TRUE")
                        {
                            HpAccountChecklistDet _det = new HpAccountChecklistDet();
                            _det.Agrd_acc_no = row.Cells[2].Value.ToString();
                            _det.Agrd_sw_rec = true;
                            _det.Agrd_sw_rec_by = BaseCls.GlbUserID;
                            _det.Agrd_sw_rec_dt = dtDate.Value.Date;
                            _det.Agrd_cre_by = BaseCls.GlbUserID;
                            _det.Agrd_cre_dt = DateTime.Now;
                            _det.Agrd_mod_by = BaseCls.GlbUserID;
                            _det.Agrd_mod_dt = DateTime.Now;
                            _detList.Add(_det);
                        }
                        else
                        {
                            HpAccountChecklistDet _det = new HpAccountChecklistDet();
                            _det.Agrd_acc_no = row.Cells[2].Value.ToString();
                            _det.Agrd_sw_rec = false;
                            _det.Agrd_sw_rec_by = BaseCls.GlbUserID;
                            _det.Agrd_sw_rec_dt = dtDate.Value.Date;
                            _det.Agrd_cre_by = BaseCls.GlbUserID;
                            _det.Agrd_cre_dt = DateTime.Now;
                            _det.Agrd_mod_by = BaseCls.GlbUserID;
                            _det.Agrd_mod_dt = DateTime.Now;
                            _detList.Add(_det);
                        }
                    }
                    else
                    {
                        HpAccountChecklistDet _det = new HpAccountChecklistDet();
                        _det.Agrd_acc_no = row.Cells[2].Value.ToString();
                        _det.Agrd_sw_rec = false;
                        _det.Agrd_sw_rec_by = BaseCls.GlbUserID;
                        _det.Agrd_sw_rec_dt = dtDate.Value.Date;
                        _det.Agrd_cre_by = BaseCls.GlbUserID;
                        _det.Agrd_cre_dt = DateTime.Now;
                        _det.Agrd_mod_by = BaseCls.GlbUserID;
                        _det.Agrd_mod_dt = DateTime.Now;
                        _detList.Add(_det);
                    }

                }

                List<HpAccountChecklistDet> _swList = (from _res in _detList
                                                       where _res.Agrd_sw_rec
                                                       select _res).ToList<HpAccountChecklistDet>();
                if (_swList == null || _swList.Count <= 0) {
                    MessageBox.Show("Accounts are not selected", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //auto number
                MasterAutoNumber _auto = new MasterAutoNumber();
                _auto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _auto.Aut_cate_tp = "PC";
                _auto.Aut_direction = 1;
                _auto.Aut_modify_dt = null;
                _auto.Aut_moduleid = "AGR";
                _auto.Aut_number = 0;
                _auto.Aut_start_char = "DOC";
                _auto.Aut_year = null;

                //save
                string _error = "";
                int _result = CHNLSVC.Sales.SaveHPAcoountChecklist(_hdr, _detList, _auto, out _error);
                if (_result == -1)
                {
                    MessageBox.Show("Error occured while processing!!\n" + _error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    MessageBox.Show("Record Insert Sucessfully!!\nDocument Number - "+_error, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while processing!!\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CHNLSVC.CloseAllChannels();
            }
        }

        private void lnkClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            gvAccounts.DataSource = null;
            txtPODNo.ReadOnly = false;
        }

        private void lnkNone_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataGridViewRow gvr in gvAccounts.Rows)
            {
                DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells[3];
                chkSelect.Value = "false";
            }
        }

        private void lnkAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataGridViewRow gvr in gvAccounts.Rows)
            {
                DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells[3];
                chkSelect.Value = "true";
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AccountChecklist);
                DataTable _result = CHNLSVC.CommonSearch.GetAccountChecklist(_CommonSearch.SearchParams, null, null);
                gvDocuments.AutoGenerateColumns = false;
                gvDocuments.DataSource = _result;

                if (_result == null || _result.Rows.Count <= 0) {
                    MessageBox.Show("No data found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

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

        private void AgrementChecklist_Load(object sender, EventArgs e)
        {
            //chk permission and enable coffirm button

            txtPc.Text = BaseCls.GlbUserDefProf;
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11049))
            {
                btnConfirm.Enabled = true;
                btnSave.Enabled = false;
            }
            else
            {
                btnConfirm.Enabled = false;
                btnSave.Enabled = true;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            AgrementChecklist formnew = new AgrementChecklist();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            AgrementChecklist formnew = new AgrementChecklist();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                List<HpAccountChecklistDet> _detailsList = new List<HpAccountChecklistDet>();
                gvDocuments.EndEdit();
                foreach (DataGridViewRow row in gvDocuments.Rows)
                {
                    if (row.Cells[4].Value != null)
                    {
                        if ((row.Cells[4].FormattedValue.ToString().ToUpper()) == "TRUE")
                        {
                            HpAccountChecklistDet _det = new HpAccountChecklistDet();
                            _det.Agrd_acc_no = row.Cells[2].Value.ToString();
                            _det.Agrd_doc_no = row.Cells[1].Value.ToString();
                            _det.Agrd_ho_rec = true;
                            _det.Agrd_ho_rec_by = BaseCls.GlbUserID;
                            _det.Agrd_ho_rec_dt = dtHoRec.Value.Date;
                            _det.Agrd_mod_by = BaseCls.GlbUserID;
                            _det.Agrd_mod_dt = DateTime.Now;
                            _detailsList.Add(_det);
                        }
                        else
                        {
                            HpAccountChecklistDet _det = new HpAccountChecklistDet();
                            _det.Agrd_acc_no = row.Cells[2].Value.ToString();
                            _det.Agrd_doc_no = row.Cells[1].Value.ToString();
                            _det.Agrd_ho_rec = false;
                            _det.Agrd_ho_rec_by = BaseCls.GlbUserID;
                            _det.Agrd_ho_rec_dt = dtDate.Value.Date;
                            _det.Agrd_mod_by = BaseCls.GlbUserID;
                            _det.Agrd_mod_dt = DateTime.Now;
                            _detailsList.Add(_det);
                        }
                    }
                    else {
                        HpAccountChecklistDet _det = new HpAccountChecklistDet();
                        _det.Agrd_acc_no = row.Cells[2].Value.ToString();
                        _det.Agrd_doc_no = row.Cells[1].Value.ToString();
                        _det.Agrd_ho_rec = false;
                        _det.Agrd_ho_rec_by = BaseCls.GlbUserID;
                        _det.Agrd_ho_rec_dt = dtDate.Value.Date;
                        _det.Agrd_mod_by = BaseCls.GlbUserID;
                        _det.Agrd_mod_dt = DateTime.Now;
                        _detailsList.Add(_det);
                    }
                }
                    
                

                if (_detailsList == null || _detailsList.Count <= 0)
                {
                    MessageBox.Show("Please select atleaset one account to confirm", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string _error;

                int _result = CHNLSVC.Sales.ConfirmHPAccountChecklist(_detailsList, txtPc.Text, dtHoRec.Value, out _error,BaseCls.GlbUserComCode);
                if (_result == -1)
                {
                    MessageBox.Show("Error occured while processing!!\n" + _error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    MessageBox.Show("Record Insert Sucessfully!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while processing!!\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CHNLSVC.CloseAllChannels();
            }

        }

        private void btnPC_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPc;
                _CommonSearch.txtSearchbyword.Text = txtPc.Text;
                _CommonSearch.ShowDialog();
                txtPc.Focus();
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

        private void btnPOD_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AccountChecklistPOD);
                DataTable _result = CHNLSVC.CommonSearch.GetAccountChecklistPOD(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPOD;
                _CommonSearch.txtSearchbyword.Text = txtPOD.Text;
                _CommonSearch.ShowDialog();
                txtPOD.Focus();
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

        private void btnDOC_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AccountChecklistPOD);
                DataTable _result = CHNLSVC.CommonSearch.GetAccountChecklistPOD(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDoc;
                _CommonSearch.txtSearchbyword.Text = txtDoc.Text;
                _CommonSearch.ShowDialog();
                txtDoc.Focus();
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

        private void lnkHOClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            gvDocuments.DataSource = null;
        }

        private void lnkHONone_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataGridViewRow gvr in gvDocuments.Rows)
            {
                DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells[4];
                chkSelect.Value = "false";
            }
        }

        private void lnkHoAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataGridViewRow gvr in gvDocuments.Rows)
            {
                DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells[4];
                chkSelect.Value = "true";
            }
        }

        private void txtPODNo_Leave(object sender, EventArgs e)
        {
            //validate pod no
            HPAccountChecklistHdr _hdr = CHNLSVC.Sales.GetPODNo(txtPODNo.Text);
            if (_hdr != null)
            {
                MessageBox.Show("POD Number already in the database", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }


    }
}
