using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FF.BusinessObjects;
using FF.Interfaces;

namespace FF.WindowsERPClient.Enquiries.Inventory
{
    public partial class RequestApprovalStatus : Base
    {
        public RequestApprovalStatus()
        {
            InitializeComponent();
        }

        #region Common Searching Area
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

               
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion


        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear_Data();
        }

        private void Clear_Data()
        {
            txtReqType.Text = "";
            dateTimePickerFrom.Value = CHNLSVC.Security.GetServerDateTime().Date; //CHNLSVC.Security.GetServerDateTime().Date.AddMonths(-1).Date;
            dateTimePickerTo.Value = CHNLSVC.Security.GetServerDateTime().Date;
            cmbStatus.Text = "ALL";

            gvHdrDet.AutoGenerateColumns = false;
            gvHdrDet.DataSource = new DataTable();

            dgvReqDet.AutoGenerateColumns = false;
            dgvReqDet.DataSource = new DataTable();

        }

        private void txtReqType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dateTimePickerFrom.Focus();
                }
                else if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ApprovePermCode);
                    DataTable _result = CHNLSVC.CommonSearch.Search_Approve_Permissions(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtReqType;
                    _CommonSearch.ShowDialog();
                    txtReqType.Select();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        private void btnSearchReq_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ApprovePermCode);
                DataTable _result = CHNLSVC.CommonSearch.Search_Approve_Permissions(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtReqType;
                _CommonSearch.ShowDialog();
                txtReqType.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        private void txtReqType_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ApprovePermCode);
                DataTable _result = CHNLSVC.CommonSearch.Search_Approve_Permissions(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtReqType;
                _CommonSearch.ShowDialog();
                txtReqType.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        private void txtReqType_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtReqType.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ApprovePermCode);
                DataTable _result = CHNLSVC.CommonSearch.Search_Approve_Permissions(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("PERMISSION_CODE") == txtReqType.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid request type", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtReqType.Clear();
                    txtReqType.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        private void RequestApprovalStatus_Load(object sender, EventArgs e)
        {
            Clear_Data();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime _frmDt = dateTimePickerFrom.Value;// CHNLSVC.Security.GetServerDateTime().Date.AddMonths(-1).Date;
                DateTime _toDt = dateTimePickerTo.Value.Date.AddDays(+1).Date;
                string _status = "";

                if (cmbStatus.Text == "PENDING")
                {
                    _status = "P";
                }
                else if (cmbStatus.Text == "APPROVED")
                {
                    _status = "A";
                }
                else if (cmbStatus.Text == "REJECT")
                {
                    _status = "R";
                }
                else
                {
                    _status = "";
                }

                DataTable _reqAppDet = new DataTable();
                _reqAppDet = CHNLSVC.General.SearchRequestApprovalDetails(BaseCls.GlbUserComCode, BaseCls.GlbUserID, txtReqType.Text, _frmDt.Date, _toDt.Date, _status, "LOC");

                DataTable _reqAppDetPC = new DataTable();
                _reqAppDetPC = CHNLSVC.General.SearchRequestApprovalDetails(BaseCls.GlbUserComCode, BaseCls.GlbUserID, txtReqType.Text, _frmDt.Date, _toDt.Date, _status, "PC");

                foreach (DataRow _x in _reqAppDet.Rows)
                {
                    string _pc = _x["GRAH_LOC"].ToString();
                    string _ref = _x["GRAH_REF"].ToString();
                    string _type = _x["GRAH_APP_TP"].ToString();

                    foreach (DataRow _y in _reqAppDetPC.Rows)
                    {
                        if (_y["GRAH_LOC"].ToString() == _pc && _y["GRAH_REF"].ToString() == _ref && _y["GRAH_APP_TP"].ToString() == _type)
                        {
                            _reqAppDetPC.Rows.Remove(_y);
                            goto L5;
                        }

                    }
                L5: int i = 1;
                    //_reqAppDetPC.Rows.Add(_x);
                }

                _reqAppDet.Merge(_reqAppDetPC);


                gvHdrDet.AutoGenerateColumns = false;
                gvHdrDet.DataSource = new DataTable();
                gvHdrDet.DataSource = _reqAppDet;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        private void gvHdrDet_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string _reqNo = "";
                dgvReqDet.AutoGenerateColumns = false;
                dgvReqDet.DataSource = new DataTable();

                _reqNo = gvHdrDet.Rows[e.RowIndex].Cells["grah_ref"].Value.ToString();

                DataTable _reqDetbyRef = new DataTable();
                _reqDetbyRef = CHNLSVC.General.SearchrequestAppDetByRef(_reqNo);

                dgvReqDet.AutoGenerateColumns = false;
                dgvReqDet.DataSource = new DataTable();
                dgvReqDet.DataSource = _reqDetbyRef;

                //kapila 4/7/2014 log header
                DataTable _reqlog = new DataTable();
                _reqlog = CHNLSVC.General.GetReqAppStatusLog(_reqNo);

                grvLogHdr.AutoGenerateColumns = false;
                grvLogHdr.DataSource = new DataTable();
                grvLogHdr.DataSource = _reqlog;

            }

            catch (Exception ex)
            {
                MessageBox.Show("Error occured while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        private void grvLogHdr_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //kapila 4/7/2014 log in items
            DataTable _reqInItm = new DataTable();
            _reqInItm = CHNLSVC.General.GetReqAppStatusLogInItems(grvLogHdr.Rows[e.RowIndex].Cells["REFNO"].Value.ToString(), Convert.ToInt32(grvLogHdr.Rows[e.RowIndex].Cells["LEVEL"].Value));

            gvrIn.AutoGenerateColumns = false;
            gvrIn.DataSource = new DataTable();
            gvrIn.DataSource = _reqInItm;

            //log out items
            DataTable _reqOutItm = new DataTable();
            _reqOutItm = CHNLSVC.General.GetReqAppStatusLogOutItems(grvLogHdr.Rows[e.RowIndex].Cells["REFNO"].Value.ToString(), Convert.ToInt32(grvLogHdr.Rows[e.RowIndex].Cells["LEVEL"].Value));

            gvrOut.AutoGenerateColumns = false;
            gvrOut.DataSource = new DataTable();
            gvrOut.DataSource = _reqOutItm;
        }


      
    }
}
