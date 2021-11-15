using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FF.BusinessObjects;

namespace FF.WindowsERPClient.Enquiries.Finance
{
    public partial class RegistrationPayTracker : Base
    {

        bool isStatusView = true;

        public RegistrationPayTracker()
        {
            InitializeComponent();
            txtPC.Text = BaseCls.GlbUserDefProf;
        }

        #region search
        private void btnChannel_Click(object sender, EventArgs e)
        {

        }

        private void btnSubChannel_Click(object sender, EventArgs e)
        {

        }

        private void btnPc_Click(object sender, EventArgs e)
        {
            try
            {

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
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {

            try
            {
                if (isStatusView)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocProInvoiceNo);
                    DataTable _result = CHNLSVC.CommonSearch.GetDocProInvoice(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtInvoiceNo;
                    _CommonSearch.txtSearchbyword.Text = txtInvoiceNo.Text;
                    _CommonSearch.ShowDialog();
                    txtInvoiceNo.Focus();
                }
                else
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocProInvoiceNo);
                    DataTable _result = CHNLSVC.CommonSearch.GetVehicalRMVNotSendInvoice(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtInvoiceNo;
                    _CommonSearch.txtSearchbyword.Text = txtInvoiceNo.Text;
                    _CommonSearch.ShowDialog();
                    txtInvoiceNo.Focus();
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnEngine_Click(object sender, EventArgs e)
        {
            try
            {
                if (isStatusView)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocProEngine);
                    DataTable _result = CHNLSVC.CommonSearch.GetDocProEngine(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtEngineNo;
                    _CommonSearch.txtSearchbyword.Text = txtEngineNo.Text;
                    _CommonSearch.ShowDialog();
                    txtEngineNo.Focus();
                }
                else
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocProEngine);
                    DataTable _result = CHNLSVC.CommonSearch.GetVehicalRMVNotSendEngine(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtEngineNo;
                    _CommonSearch.txtSearchbyword.Text = txtEngineNo.Text;
                    _CommonSearch.ShowDialog();
                    txtEngineNo.Focus();
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnChassis_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (isStatusView)
            //    {
            //        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            //        _CommonSearch.ReturnIndex = 0;
            //        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocProChassis);
            //        DataTable _result = CHNLSVC.CommonSearch.GetDocProChassis(_CommonSearch.SearchParams, null, null);
            //        _CommonSearch.dvResult.DataSource = _result;
            //        _CommonSearch.BindUCtrlDDLData(_result);
            //        _CommonSearch.obj_TragetTextBox = txtChassisNo;
            //        _CommonSearch.txtSearchbyword.Text = txtChassisNo.Text;
            //        _CommonSearch.ShowDialog();
            //        txtChassisNo.Focus();
            //    }
            //    else {
            //        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            //        _CommonSearch.ReturnIndex = 0;
            //        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocProChassis);
            //        DataTable _result = CHNLSVC.CommonSearch.GetVehicalRMVNotSendChassis(_CommonSearch.SearchParams, null, null);
            //        _CommonSearch.dvResult.DataSource = _result;
            //        _CommonSearch.BindUCtrlDDLData(_result);
            //        _CommonSearch.obj_TragetTextBox = txtChassisNo;
            //        _CommonSearch.txtSearchbyword.Text = txtChassisNo.Text;
            //        _CommonSearch.ShowDialog();
            //        txtChassisNo.Focus();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.Cursor = Cursors.Default;
            //    MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            //}
            //finally
            //{
            //    CHNLSVC.CloseAllChannels();
            //}
        }

        private void btnReciept_Click(object sender, EventArgs e)
        {
            try
            {
                if (isStatusView)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 2;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocProChassis);
                    DataTable _result = CHNLSVC.CommonSearch.GetDocProChassis(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtReciept;
                    _CommonSearch.txtSearchbyword.Text = txtReciept.Text;
                    _CommonSearch.ShowDialog();
                    txtReciept.Focus();
                }
                else
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 2;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocProChassis);
                    DataTable _result = CHNLSVC.CommonSearch.GetVehicalRMVNotSendChassis(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtReciept;
                    _CommonSearch.txtSearchbyword.Text = txtReciept.Text;
                    _CommonSearch.ShowDialog();
                    txtReciept.Focus();
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnVehNo_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (isStatusView)
            //    {
            //        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            //        _CommonSearch.ReturnIndex = 4;
            //        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocProChassis);
            //        DataTable _result = CHNLSVC.CommonSearch.GetDocProChassis(_CommonSearch.SearchParams, null, null);
            //        _CommonSearch.dvResult.DataSource = _result;
            //        _CommonSearch.BindUCtrlDDLData(_result);
            //        _CommonSearch.obj_TragetTextBox = txtVehNo;
            //        _CommonSearch.txtSearchbyword.Text = txtVehNo.Text;
            //        _CommonSearch.ShowDialog();
            //        txtVehNo.Focus();
            //    }
            //    else
            //    {
            //        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            //        _CommonSearch.ReturnIndex = 4;
            //        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocProChassis);
            //        DataTable _result = CHNLSVC.CommonSearch.GetVehicalRMVNotSendChassis(_CommonSearch.SearchParams, null, null);
            //        _CommonSearch.dvResult.DataSource = _result;
            //        _CommonSearch.BindUCtrlDDLData(_result);
            //        _CommonSearch.obj_TragetTextBox = txtVehNo;
            //        _CommonSearch.txtSearchbyword.Text = txtVehNo.Text;
            //        _CommonSearch.ShowDialog();
            //        txtVehNo.Focus();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.Cursor = Cursors.Default;
            //    MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            //}
            //finally
            //{
            //    CHNLSVC.CloseAllChannels();
            //}
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.DocProInvoiceNo:
                    {

                        paramsText.Append(BaseCls.GlbUserDefLoca + seperator + BaseCls.GlbUserComCode);

                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocProEngine:
                    {
                        paramsText.Append(BaseCls.GlbUserDefLoca + seperator + BaseCls.GlbUserComCode);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocProChassis:
                    {

                        paramsText.Append(BaseCls.GlbUserDefLoca + seperator + BaseCls.GlbUserComCode);

                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        private void btnPcClear_Click(object sender, EventArgs e)
        {

            txtPC.Text = "";

        }

        private void VehicalDocumentTracking_Load(object sender, EventArgs e)
        {
            //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "SCM2I"))
            //{
            //    grpPC.Enabled = true;
            //}
            //else
            //    grpPC.Enabled = false;




        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {

                this.Cursor = Cursors.WaitCursor;
                DataTable _result = new DataTable();
                string invoice = (txtInvoiceNo.Text != "") ? invoice = txtInvoiceNo.Text : "%";
                string engine = (txtEngineNo.Text != "") ? engine = txtEngineNo.Text : "%";
                string chassis = null;  // (txtChassisNo.Text != "") ? chassis = txtChassisNo.Text : "%";
                string reciept = (txtReciept.Text != "") ? reciept = txtReciept.Text : "%";
                string vehNo = null;  // (txtVehNo.Text != "") ? vehNo = txtVehNo.Text : "%";
                Int32 _opt = 0;

                //kapila 16/6/2017
                if (chkAllDate.Checked == false)
                {
                    Int32 _days =(dtTo.Value -dtFrom.Value).Days;
                    if (_days > 31)
                    {
                        MessageBox.Show("Cannot search more than 1 month period", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (opt1.Checked == true) _opt = 1;
                if (opt2.Checked == true) _opt = 2;
                if (opt3.Checked == true) _opt = 3;
                if (opt4.Checked == true) _opt = 4;
                if (opt5.Checked == true) _opt = 5;
                if (opt6.Checked == true) _opt = 6;
                if (opt7.Checked == true) _opt = 7;
                if (opt8.Checked == true) _opt = 8;

                _result = CHNLSVC.Financial.GetVehRegPayTracker(BaseCls.GlbUserComCode, txtPC.Text, chkAllDate.Checked == true ? 0 : 1, dtFrom.Value.Date, dtTo.Value.Date, txtInvoiceNo.Text, txtEngineNo.Text, txtCust.Text, null, txtReciept.Text, null, _opt);
                if (_result.Rows.Count <= 0)
                {
                    MessageBox.Show("No Data Found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                grvDocumentHeader.AutoGenerateColumns = false;
                grvDocumentHeader.DataSource = _result;

                if (opt9.Checked)
                {
                    Int32 _docrec = 0;
                    Int32 _docret = 0;
                    Int32 _sendrmv = 0;
                    Int32 _crrec = 0;
                    Int32 _nopltrec = 0;
                    Int32 _crcour = 0;
                    Int32 _nopltcour = 0;
                    Int32 _comp = 0;
                    Int32 _tot = 0;

                    _tot = grvDocumentHeader.Rows.Count;

                    foreach (DataGridViewRow r in grvDocumentHeader.Rows)
                    {
                        if (!string.IsNullOrEmpty(r.Cells["col1"].Value.ToString()) && string.IsNullOrEmpty(r.Cells["col3"].Value.ToString()))
                            r.DefaultCellStyle.BackColor = Color.Tomato;

                        if (!string.IsNullOrEmpty(r.Cells["col2"].Value.ToString()))
                            r.DefaultCellStyle.BackColor = Color.Orchid;

                        //send rmv
                        if (!string.IsNullOrEmpty(r.Cells["col3"].Value.ToString()) && string.IsNullOrEmpty(r.Cells["col4"].Value.ToString()))
                            r.DefaultCellStyle.BackColor = Color.PaleVioletRed;

                        //cr receive
                        if (!string.IsNullOrEmpty(r.Cells["col4"].Value.ToString()) && string.IsNullOrEmpty(r.Cells["col5"].Value.ToString()))
                            r.DefaultCellStyle.BackColor = Color.Goldenrod;
                        //no plate receive
                        if (!string.IsNullOrEmpty(r.Cells["col5"].Value.ToString()) && string.IsNullOrEmpty(r.Cells["col6"].Value.ToString()))
                            r.DefaultCellStyle.BackColor = Color.DarkSeaGreen;
                        //cr courier
                        if (!string.IsNullOrEmpty(r.Cells["col6"].Value.ToString()) && string.IsNullOrEmpty(r.Cells["col7"].Value.ToString()))
                            r.DefaultCellStyle.BackColor = Color.LawnGreen;
                        //no plate courier
                        if (!string.IsNullOrEmpty(r.Cells["col7"].Value.ToString()) && string.IsNullOrEmpty(r.Cells["col8"].Value.ToString()))
                            r.DefaultCellStyle.BackColor = Color.SteelBlue;
                        //complete
                        if (!string.IsNullOrEmpty(r.Cells["col8"].Value.ToString()))
                            r.DefaultCellStyle.BackColor = Color.Teal;

                        //----------------------------------------------------------------------------------------------------------
                        if (!string.IsNullOrEmpty(r.Cells["col1"].Value.ToString()) )
                            _docrec = _docrec + 1;

                        if (!string.IsNullOrEmpty(r.Cells["col2"].Value.ToString()))
                            _docret = _docret + 1;

                        //send rmv
                        if (!string.IsNullOrEmpty(r.Cells["col3"].Value.ToString()))
                            _sendrmv = _sendrmv + 1;

                        if (!string.IsNullOrEmpty(r.Cells["col4"].Value.ToString()) )
                            _crrec = _crrec + 1;

                        if (!string.IsNullOrEmpty(r.Cells["col5"].Value.ToString()) )
                            _nopltrec = _nopltrec + 1;

                        if (!string.IsNullOrEmpty(r.Cells["col6"].Value.ToString()) )
                            _crcour = _crcour + 1;

                        if (!string.IsNullOrEmpty(r.Cells["col7"].Value.ToString()) )
                            _nopltcour = _nopltcour + 1;

                        if (!string.IsNullOrEmpty(r.Cells["col8"].Value.ToString()))
                            _comp = _comp + 1;


                    }
                    lbl1.Text = _tot.ToString();
                    lbl2.Text = _docret + "/" + _tot.ToString();
                    lbl3.Text = _sendrmv + "/" + _tot.ToString();
                    lbl4.Text = _crrec + "/" + _sendrmv.ToString();
                    lbl5.Text = _nopltrec + "/" + _crrec.ToString();
                    lbl6.Text = _crcour + "/" + _nopltrec.ToString();
                    lbl7.Text = _nopltcour + "/" + _crcour.ToString();
                    lbl8.Text =  _comp.ToString();

                }
                grvDocumentHeader.AutoGenerateColumns = false;
                grvDocumentHeader.DataSource = _result;
                chkAllPc.Focus();

                //    grvDocumentDetails.DataSource = null;
                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
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
                this.Cursor = Cursors.WaitCursor;
                while (this.Controls.Count > 0)
                {
                    Controls[0].Dispose();
                }
                InitializeComponent();
                isStatusView = true;

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
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

        #region key down events
        private void txtChanel_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F2)
            {
                btnChannel_Click(null, null);
            }

        }

        private void txtSChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPC.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnSubChannel_Click(null, null);
            }
        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtReciept.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnPc_Click(null, null);
            }
        }

        private void txtInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtEngineNo.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnInvoice_Click(null, null);
            }
        }

        private void txtEngineNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnEngine_Click(null, null);
            }
        }

        private void txtChassisNo_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F2)
            {
                btnChassis_Click(null, null);
            }
        }
        #endregion

        private void txtChanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnChannel_Click(null, null);
        }

        private void txtSChanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSubChannel_Click(null, null);
        }

        private void txtPC_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPc_Click(null, null);
        }

        private void txtReciept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtInvoiceNo.Focus();
            }
        }

        private void txtChassisNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnChassis_Click(null, null);
        }

        private void txtEngineNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnEngine_Click(null, null);
        }

        private void txtInvoiceNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnInvoice_Click(null, null);
        }

        private void txtReciept_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnReciept_Click(null, null);
        }

        private void txtVehNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStrip1.Focus();
                btnView.Select();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnVehNo_Click(null, null);
            }
        }

        private void txtVehNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnVehNo_Click(null, null);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {



            BaseCls.GlbReportHeading = "PENDING REGISTRATION RECEIPTS";

            Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
            BaseCls.GlbReportName = "Not_Reg_Vehicles_Report.rpt";
            _view.Show();
            _view = null;

        }

    }
}
