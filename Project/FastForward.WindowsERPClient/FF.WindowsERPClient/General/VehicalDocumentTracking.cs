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
    public partial class VehicalDocumentTracking : Base
    {

        bool isStatusView = true;

        public VehicalDocumentTracking()
        {
            InitializeComponent();
        }

        #region search
        private void btnChannel_Click(object sender, EventArgs e)
        {
            try
            {
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
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSubChannel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtChanel.Text))
                {
                    MessageBox.Show("Please select channel.", "Document Tracking", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSChanel.Text = "";
                    txtChanel.Focus();
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
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
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
                else {
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
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
                else {
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnChassis_Click(object sender, EventArgs e)
        {
            try
            {
                if (isStatusView)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocProChassis);
                    DataTable _result = CHNLSVC.CommonSearch.GetDocProChassis(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtChassisNo;
                    _CommonSearch.txtSearchbyword.Text = txtChassisNo.Text;
                    _CommonSearch.ShowDialog();
                    txtChassisNo.Focus();
                }
                else {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocProChassis);
                    DataTable _result = CHNLSVC.CommonSearch.GetVehicalRMVNotSendChassis(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtChassisNo;
                    _CommonSearch.txtSearchbyword.Text = txtChassisNo.Text;
                    _CommonSearch.ShowDialog();
                    txtChassisNo.Focus();
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
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
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnVehNo_Click(object sender, EventArgs e)
        {
            try
            {
                if (isStatusView)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 4;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocProChassis);
                    DataTable _result = CHNLSVC.CommonSearch.GetDocProChassis(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtVehNo;
                    _CommonSearch.txtSearchbyword.Text = txtVehNo.Text;
                    _CommonSearch.ShowDialog();
                    txtVehNo.Focus();
                }
                else
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 4;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocProChassis);
                    DataTable _result = CHNLSVC.CommonSearch.GetVehicalRMVNotSendChassis(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtVehNo;
                    _CommonSearch.txtSearchbyword.Text = txtVehNo.Text;
                    _CommonSearch.ShowDialog();
                    txtVehNo.Focus();
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
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
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {

                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {

                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;

                    }
                case CommonUIDefiniton.SearchUserControlType.DocProInvoiceNo: {

                    if (grpPC.Enabled && lstPC.Items.Count > 0)
                    {
                        string pcList="";
                        foreach (ListViewItem Item in lstPC.Items)
                        {
                            if (Item.Checked) {
                                pcList = pcList +","+ Item.Text;
                            }
                            
                        }
                        if (pcList != "")
                        {
                            paramsText.Append(pcList + seperator + BaseCls.GlbUserComCode);
                        }
                        else {
                            paramsText.Append(BaseCls.GlbUserDefLoca + seperator + BaseCls.GlbUserComCode);
                        }
                    }
                    else {
                        paramsText.Append(BaseCls.GlbUserDefLoca+seperator+BaseCls.GlbUserComCode);
                    }
                    break;
                }
                case CommonUIDefiniton.SearchUserControlType.DocProEngine:
                    {

                        if (grpPC.Enabled && lstPC.Items.Count > 0)
                        {
                            string pcList = "";
                            foreach (ListViewItem Item in lstPC.Items)
                            {
                                if (Item.Checked)
                                {
                                    pcList = pcList + "," + Item.Text;
                                }

                            }
                            if (pcList != "")
                            {
                                paramsText.Append(pcList + seperator + BaseCls.GlbUserComCode);
                            }
                            else
                            {
                                paramsText.Append(BaseCls.GlbUserDefLoca + seperator + BaseCls.GlbUserComCode);
                            }
                        }
                        else
                        {
                            paramsText.Append(BaseCls.GlbUserDefLoca + seperator + BaseCls.GlbUserComCode);
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocProChassis:
                    {

                        if (grpPC.Enabled && lstPC.Items.Count > 0)
                        {
                            string pcList = "";
                            foreach (ListViewItem Item in lstPC.Items)
                            {
                                if (Item.Checked)
                                {
                                    pcList = pcList + "," + Item.Text;
                                }

                            }
                            if (pcList != "")
                            {
                                paramsText.Append(pcList + seperator + BaseCls.GlbUserComCode);
                            }
                            else
                            {
                                paramsText.Append(BaseCls.GlbUserDefLoca + seperator + BaseCls.GlbUserComCode);
                            }
                        }
                        else
                        {
                            paramsText.Append(BaseCls.GlbUserDefLoca + seperator + BaseCls.GlbUserComCode);
                        }
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        } 
        #endregion

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                //lstPC.Clear();
                
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(BaseCls.GlbUserComCode, txtChanel.Text, txtSChanel.Text, null, null, null, txtPC.Text);
                foreach (DataRow drow in dt.Rows)
                {
                    lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                }

                if (dt.Rows.Count <= 0) {
                    MessageBox.Show("No profit centers found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                txtChanel.Text = "";
                txtSChanel.Text = "";
                txtPC.Text = "";
                txtPC.Focus();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
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

        private void btnPcClear_Click(object sender, EventArgs e)
        {
            txtChanel.Text = "";
            txtSChanel.Text = "";
            txtPC.Text = "";
            lstPC.Clear();
            txtChanel.Focus();
        }

        private void VehicalDocumentTracking_Load(object sender, EventArgs e)
        {
            //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "SCM2I"))
            //{
            //    grpPC.Enabled = true;
            //}
            //else
            //    grpPC.Enabled = false;

            try
            {
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10006))
                {
                    grpPC.Enabled = true;
                }
                else
                {
                    grpPC.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }


        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedIndex == 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    DataTable _result = new DataTable();
                    string invoice = (txtInvoiceNo.Text != "") ? invoice = txtInvoiceNo.Text : "%";
                    string engine = (txtEngineNo.Text != "") ? engine = txtEngineNo.Text : "%";
                    string chassis = (txtChassisNo.Text != "") ? chassis = txtChassisNo.Text : "%";
                    string reciept = (txtReciept.Text != "") ? reciept = txtReciept.Text : "%";
                    string vehNo = (txtVehNo.Text != "") ? vehNo = txtVehNo.Text : "%";

                    if (grpPC.Enabled && lstPC.Items.Count > 0)
                    {
                        string pcList = "";
                        foreach (ListViewItem Item in lstPC.Items)
                        {
                            if (Item.Checked)
                            {
                                pcList = pcList + "," + Item.Text;
                            }

                        }
                        if (pcList != "")
                        {
                            _result = CHNLSVC.Sales.GetVehicalDocumentProcess(BaseCls.GlbUserComCode, pcList, invoice, engine, chassis,reciept,vehNo);
                        }
                        else
                        {
                            _result = CHNLSVC.Sales.GetVehicalDocumentProcess(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, invoice, engine, chassis, reciept,vehNo);
                        }
                    }
                    else
                    {
                        _result = CHNLSVC.Sales.GetVehicalDocumentProcess(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, invoice, engine, chassis, reciept,vehNo);
                    }
                    if (_result.Rows.Count <= 0)
                    {
                        MessageBox.Show("No Data Found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        
                    }
                    grvDocumentHeader.AutoGenerateColumns = false;
                    grvDocumentHeader.DataSource = _result;

                    grvDocumentDetails.DataSource = null;
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    this.Cursor = Cursors.WaitCursor;
                    DataTable _result = new DataTable();
                    string invoice = (txtInvoiceNo.Text != "") ? invoice = txtInvoiceNo.Text : "%";
                    string engine = (txtEngineNo.Text != "") ? engine = txtEngineNo.Text : "%";
                    string chassis = (txtChassisNo.Text != "") ? chassis = txtChassisNo.Text : "%";
                    string reciept = (txtReciept.Text != "") ? reciept = txtReciept.Text : "%";
                    string vehNo = (txtVehNo.Text != "") ? vehNo = txtVehNo.Text : "%";

                    if (grpPC.Enabled && lstPC.Items.Count > 0)
                    {
                        string pcList = "";
                        foreach (ListViewItem Item in lstPC.Items)
                        {
                            if (Item.Checked)
                            {
                                pcList = pcList + "," + Item.Text;
                            }

                        }
                        if (pcList != "")
                        {
                            _result = CHNLSVC.Sales.GetVehicalRMVNotSendDetails(BaseCls.GlbUserComCode, pcList, invoice, engine, chassis, reciept,vehNo);
                        }
                        else
                        {
                            _result = CHNLSVC.Sales.GetVehicalRMVNotSendDetails(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, invoice, engine, chassis, reciept,vehNo);
                        }
                    }
                    else
                    {
                        _result = CHNLSVC.Sales.GetVehicalRMVNotSendDetails(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, invoice, engine, chassis, reciept,vehNo);
                    }
                    if (_result.Rows.Count <= 0) {
                        MessageBox.Show("No Data Found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                       
                    }
                    grvRMVDocument.AutoGenerateColumns = false;
                    grvRMVDocument.DataSource = _result;

                    
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                isStatusView = true;
            }
            else
                isStatusView = false;
        }

        private void grvDocumentHeader_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    int seq = Convert.ToInt32(grvDocumentHeader.Rows[e.RowIndex].Cells[13].Value);
                    DataTable _dt = CHNLSVC.Sales.GetVehicalDocummentDetail(seq);
                    grvDocumentDetails.AutoGenerateColumns = false;
                    grvDocumentDetails.DataSource = _dt;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
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
                this.Cursor = Cursors.WaitCursor;
                while (this.Controls.Count > 0)
                {
                    Controls[0].Dispose();
                }
                InitializeComponent();
                isStatusView = true;
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10006))
                {
                    grpPC.Enabled = true;
                }
                else
                {
                    grpPC.Enabled = false;
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
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
            if (e.KeyCode == Keys.Enter) {
                txtSChanel.Focus();
            }
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
            if (e.KeyCode == Keys.Enter)
            {
                txtChassisNo.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnEngine_Click(null, null);
            }
        }

        private void txtChassisNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtVehNo.Focus();
            }
            if (e.KeyCode == Keys.F2) {
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
            if (e.KeyCode == Keys.Enter) {
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
            if (e.KeyCode == Keys.Enter) {
                toolStrip1.Focus();
                btnView.Select();
            }
            if (e.KeyCode == Keys.F2) {
                btnVehNo_Click(null, null);
            }
        }

        private void txtVehNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnVehNo_Click(null, null);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
           
                //update temporary table
                update_PC_List();

                BaseCls.GlbReportHeading = "PENDING REGISTRATION RECEIPTS";

                Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                BaseCls.GlbReportName = "Not_Reg_Vehicles_Report.rpt";
                _view.Show();
                _view = null;
            
        }

        private void update_PC_List()
        {
            string _tmpPC = "";
            BaseCls.GlbReportProfit = "";

            Boolean _isPCFound = false;
            Int32 del = CHNLSVC.Sales.Delete_TEMP_PC_LOC(BaseCls.GlbUserID, BaseCls.GlbUserComCode, null, null);

            foreach (ListViewItem Item in lstPC.Items)
            {
                string pc = Item.Text;

                if (Item.Checked == true)
                {
                    Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC(BaseCls.GlbUserID, BaseCls.GlbUserComCode, pc, null);

                    _isPCFound = true;
                    if (string.IsNullOrEmpty(BaseCls.GlbReportProfit))
                    {
                        BaseCls.GlbReportProfit = pc;
                    }
                    else
                    {
                        //BaseCls.GlbReportProfit = BaseCls.GlbReportProfit + "," + pc;
                        BaseCls.GlbReportProfit = "All Locations Based on User Rights";
                    }
                }
            }

            if (_isPCFound == false)
            {
                BaseCls.GlbReportProfit = txtPC.Text;
                Int32 effect = CHNLSVC.Sales.Save_TEMP_PC_LOC(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, null);
            }
        }
    }
}
