using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.Data.OleDb;
using FF.BusinessObjects;
using FF.BusinessObjects.Services;
using System.Text.RegularExpressions;

namespace FF.WindowsERPClient.Services
{
    public partial class ServiceAreaSetup : Base
    {
        
        public ServiceAreaSetup()
        {
            InitializeComponent();
        }
        
         
        private List<string> _itemLst = null;
        List<MasterItemSubCate> _lstcate2 = new List<MasterItemSubCate>();
        DataTable oDataTable = new DataTable();
        int effrect = 0;
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.ServiceAgent:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.serch_service_loc:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                    
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void btnSerchTown_Click(object sender, EventArgs e)
        {
            try
            {
                lblTown.Text = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 3;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
                DataTable _result = CHNLSVC.CommonSearch.GetTown(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtTown;
                _CommonSearch.ShowDialog();
                txtTown.Select();
                DataTable odt = CHNLSVC.General.GetTownByCode(txtTown.Text);
                if (odt.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid town code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTown.Text = "";
                    lblTown.Text = "";
                    return;
                }
                else
                {
                    lblTown.Text = odt.Rows[0]["mt_desc"].ToString();
                }


              
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("An error occurred while searching town details" + Environment.NewLine + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearch_ServiceCeneter_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.serch_service_loc);
                DataTable _result = CHNLSVC.CommonSearch.Get_scv_location_BY_LOCTIONTABLE(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSeviceCenter;
                _CommonSearch.ShowDialog();

                DataTable _dtserLoc = CHNLSVC.Sales.GetServiceAgentbyLoc(BaseCls.GlbUserComCode, txtSeviceCenter.Text);
                if (_dtserLoc.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid service center code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSeviceCenter.Text = "";
                    lblServiceCenter.Text = "";
                    return;
                }
                else
                {
                    lblServiceCenter.Text = _dtserLoc.Rows[0]["mbe_name"].ToString();
                }

               
                txtSeviceCenter.Select();
                loadSeviceCenter();

              
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("An error occurred while searching town details" + Environment.NewLine + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearchFile_spv_Click(object sender, EventArgs e)
        {
            txtbrows.Text = string.Empty;
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "Excel|*.xls|Excel 2010|*.xlsx";
            //openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.ShowDialog();
            string[] _obj = openFileDialog1.FileName.Split('\\');
            txtbrows.Text = openFileDialog1.FileName;
        }

        private void btnUploadFile_spv_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to save?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {


                    #region Upload Excel
                    string _msg = string.Empty;
                    ReminderLetter _ltr = new ReminderLetter();


                    BaseCls.GlbReportName = "Check_MC_CD";
                    _ltr.Hrl_com = BaseCls.GlbUserComCode;
                    _ltr.Hrl_cre_by = BaseCls.GlbUserID;
                    _ltr.Hrl_cre_dt = DateTime.Now;
                    _ltr.Hrl_pc = BaseCls.GlbUserDefProf.ToString();


                    if (string.IsNullOrEmpty(txtbrows.Text))
                    {
                        MessageBox.Show("Please select upload file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtbrows.Clear();
                        txtbrows.Focus();
                        return;
                    }

                    System.IO.FileInfo fileObj = new System.IO.FileInfo(txtbrows.Text);
                    if (fileObj.Exists == false)
                    {
                        MessageBox.Show("Selected file does not exist at the following path.", "Price Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //btnGvBrowse.Focus();
                        return;
                    }

                    string Extension = fileObj.Extension;

                    string conStr = "";

                    if (Extension.ToUpper() == ".XLS")
                    {

                        conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"]
                                 .ConnectionString;
                    }
                    else if (Extension.ToUpper() == ".XLSX")
                    {
                        conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"]
                                  .ConnectionString;

                    }
                    else
                    {
                        MessageBox.Show("Invalid upload file Format", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtbrows.Text = "";
                        return;
                    }
                    string _excelConnectionString = ConfigurationManager.ConnectionStrings[Extension == ".XLS" ? "ConStringExcel03" : "ConStringExcel07"].ConnectionString;


                    _excelConnectionString = String.Format(conStr, txtbrows.Text, "YES");
                    OleDbConnection connExcel = new OleDbConnection(_excelConnectionString);
                    OleDbCommand cmdExcel = new OleDbCommand();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    DataTable _dt = new DataTable();
                    cmdExcel.Connection = connExcel;

                    connExcel.Open();
                    DataTable dtExcelSchema;
                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    connExcel.Close();

                    connExcel.Open();
                    cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                    oda.SelectCommand = cmdExcel;
                    oda.Fill(_dt);
                    connExcel.Close();

                    _itemLst = new List<string>();
                    StringBuilder _errorLst = new StringBuilder();
                    if (_dt == null || _dt.Rows.Count <= 0) { MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                    #endregion

                    if (_dt.Rows.Count > 0)
                    {
                        foreach (DataRow _dr in _dt.Rows)
                        {

                            DataTable _result = CHNLSVC.CommonSearch.GetServiceCenterDetails(BaseCls.GlbUserComCode, _dr[0].ToString().Trim(), null, BaseCls.GlbReportName = "Check_SVC_Center_CD");
                            if (_result.Rows.Count < 1)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("and invalid Service Center Code - " + _dr[0].ToString());
                                else _errorLst.Append(" and invalid Service Center Code  - " + _dr[0].ToString());
                                goto Show_Error;

                            }

                            DataTable oTownCD = CHNLSVC.CommonSearch.GetServiceCenterDetails(null, null, _dr[1].ToString().Trim(), BaseCls.GlbReportName = "Cheak_Town_CD");
                            if (oTownCD.Rows.Count < 1)
                            {
                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("and invalid Town Code - " + _dr[1].ToString());
                                else _errorLst.Append(" and invalid Town Code  - " + _dr[1].ToString());
                                goto Show_Error;

                            }
                        }

                        if (_dt.Rows.Count > 0)
                        {
                            List<ServiceAreaS> _lsit = new List<ServiceAreaS>();
                            
                            foreach (DataRow _dr in _dt.Rows)
                            {
                                ServiceAreaS _ServiceAreaS = new ServiceAreaS();
                                _ServiceAreaS.SSA_COM = BaseCls.GlbUserComCode;
                                _ServiceAreaS.SSA_SER_LOC = _dr["Service Location"] == DBNull.Value ? string.Empty : _dr["Service Location"].ToString().Trim();
                                _ServiceAreaS.SSA_TOWN_CD = _dr["Town Code"] == DBNull.Value ? string.Empty : _dr["Town Code"].ToString().Trim();
                                _ServiceAreaS.SSA_ACT = 1;
                                _ServiceAreaS.SSA_CRE_BY = BaseCls.GlbUserID;
                                _ServiceAreaS.SSA_CRE_DT = DateTime.Now;
                                _ServiceAreaS.SSA_MOD_BY = BaseCls.GlbUserID;
                                _ServiceAreaS.SSA_MOD_DT = DateTime.Now;
                                _lsit.Add(_ServiceAreaS);

                            }
                            int effrect = CHNLSVC.CustService.SaveserviceAreas(_lsit);
                            if (effrect == 1)
                            {
                                MessageBox.Show("Successfully Uploaded! ", "Service Centers Areas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtbrows.Clear();
                                txtbrows.Focus();
                                return;


                            }
                        }
                    }
                Show_Error:
                    if (!string.IsNullOrEmpty(_errorLst.ToString()))
                    {
                        if (MessageBox.Show("Following discrepancies found when checking the file.\n" + _errorLst.ToString() + ".\n Do you need to continue anyway?", "Discrepancies", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            ////  _itemLst = new List<string>();
                        }
                    }

                }
                catch (Exception)
                {

                    throw;
                }

            }
        }    
        public void loadSeviceCenter()
        {

            ReminderLetter _ltr = new ReminderLetter();
            _ltr.Hrl_com = BaseCls.GlbUserComCode;
           
            DataGridView _grdServiceDetails = new DataGridView();
            grdServiceDetails.DataSource = null;
            DataTable oDataTable = new DataTable();
            grdServiceDetails.Rows.Clear();
            oDataTable = CHNLSVC.CommonSearch.GetServiceCenterDetails(_ltr.Hrl_com, txtSeviceCenter.Text.ToString(),txtTown.Text.Trim(), null);
            if (oDataTable.Rows.Count > 0)
            {

                for (int count = 0; count < oDataTable.Rows.Count; count++)
                {

                    grdServiceDetails.Rows.Add();
                    grdServiceDetails.Rows[count].Cells["Service_Center_code"].Value = oDataTable.Rows[count]["SSA_SER_LOC"].ToString();
                    grdServiceDetails.Rows[count].Cells["Service_Center"].Value = oDataTable.Rows[count]["ML_LOC_DESC"].ToString();

                    grdServiceDetails.Rows[count].Cells["Town_Code"].Value = oDataTable.Rows[count]["SSA_TOWN_CD"].ToString();
                    grdServiceDetails.Rows[count].Cells["Town"].Value = oDataTable.Rows[count]["MT_DESC"].ToString();
                    grdServiceDetails.Rows[count].Cells["States"].Value = oDataTable.Rows[count]["States"].ToString();
                    grdServiceDetails.Rows[count].Cells["SVC_ID"].Value = oDataTable.Rows[count]["SSA_SER_LOC"].ToString();

                }  
              

            }
        }

        private void ServiceAreaSetup_Load(object sender, EventArgs e)
        {
            BaseCls.GlbReportName = "Load Compny";
            loadSeviceCenter();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadSeviceCenter();
        }
        public void clear()
        {
            lblTown.Text = "";
            lblServiceCenter.Text = "";
            txtSeviceCenter.Text = "";
            txtTown.Text = "";
            chkInactive.Checked = false;
            grdServiceDetails.Rows.Clear();
            txtbrows.Text = "";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            List<ServiceAreaS> _lsit = new List<ServiceAreaS>();
                    ServiceAreaS _ServiceAreaS = new ServiceAreaS();
            try
            {
                Regex r = new Regex("^[a-zA-Z0-9]*$");

                if (!Regex.IsMatch(txtSeviceCenter.Text, "^[a-zA-Z0-9]*$"))
                {
                    MessageBox.Show("Please enter valid center code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtSeviceCenter.Text.Trim()=="")
                {
                    MessageBox.Show("Please enter center code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;  
                }
                if (!Regex.IsMatch(txtbrows.Text, "^[a-zA-Z0-9]*$"))
                {
                    MessageBox.Show("Please enter valid Town code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtTown.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter Town code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DataTable oTownCD = CHNLSVC.CommonSearch.GetServiceCenterDetails(null, null, txtTown.Text.ToString().Trim(), BaseCls.GlbReportName = "Cheak_Town_CD");
                if (oTownCD.Rows.Count < 1)
                {
                    MessageBox.Show("Please enter a valid Town code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;

                }
                DataTable _result = CHNLSVC.CommonSearch.GetServiceCenterDetails(BaseCls.GlbUserComCode, txtSeviceCenter.Text.ToString().Trim(), null,"Check_SVC_Center_CD");
                if (_result.Rows.Count < 1)
                {
                    MessageBox.Show("Please enter a valid service center code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;

                }

              
                if (chkInactive.Checked)
                {
                    _ServiceAreaS.SSA_COM = BaseCls.GlbUserComCode;
                    _ServiceAreaS.SSA_SER_LOC = txtSeviceCenter.Text.Trim();
                    _ServiceAreaS.SSA_TOWN_CD = txtTown.Text.Trim();
                    _ServiceAreaS.SSA_ACT = 1;
                    _ServiceAreaS.SSA_CRE_BY = BaseCls.GlbUserID;
                    _ServiceAreaS.SSA_CRE_DT = DateTime.Now;
                    _ServiceAreaS.SSA_MOD_BY = BaseCls.GlbUserID;
                    _ServiceAreaS.SSA_MOD_DT = DateTime.Now;
                    _lsit.Add(_ServiceAreaS);

                    effrect = CHNLSVC.CustService.SaveserviceAreas(_lsit);
                  
                }
                else
                {

                   
                    _ServiceAreaS.SSA_COM = BaseCls.GlbUserComCode;
                    _ServiceAreaS.SSA_SER_LOC = txtSeviceCenter.Text.Trim();
                    _ServiceAreaS.SSA_TOWN_CD = txtTown.Text.Trim();
                    _ServiceAreaS.SSA_ACT = 0;
                    _ServiceAreaS.SSA_CRE_BY = BaseCls.GlbUserID;
                    _ServiceAreaS.SSA_CRE_DT = DateTime.Now;
                    _ServiceAreaS.SSA_MOD_BY = BaseCls.GlbUserID;
                    _ServiceAreaS.SSA_MOD_DT = DateTime.Now;
                    _lsit.Add(_ServiceAreaS);

                    effrect = CHNLSVC.CustService.SaveserviceAreas(_lsit);
                }
                if (effrect==1)
                {
                     MessageBox.Show("Successfully Updated! ", "Service Cernter Area", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     txtbrows.Text = "";
                     clear();

                }
                //loadSeviceCenter();
                    
            }
            catch (Exception)
            {
                
                throw;
            }

           
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to Clear?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                clear();
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            loadSeviceCenter();
        }

        private void grdServiceDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = grdServiceDetails.CurrentRow.Index;
            string states = grdServiceDetails.Rows[rowindex].Cells["States"].Value.ToString();
            try
            {
                if (states=="Active")
                {
                    chkInactive.Checked = true;
                }
                else
                    chkInactive.Checked = false;
                lblServiceCenter.Text = grdServiceDetails.Rows[rowindex].Cells["Service_Center"].Value.ToString();
                lblTown.Text = grdServiceDetails.Rows[rowindex].Cells["Town"].Value.ToString();
                txtSeviceCenter.Text = grdServiceDetails.Rows[rowindex].Cells["SVC_ID"].Value.ToString();
                txtTown.Text = grdServiceDetails.Rows[rowindex].Cells["Town_Code"].Value.ToString();
            }
            catch (Exception)
            {

                throw;
            }

             
          
        }

        private void txtSeviceCenter_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void txtTown_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void txtSeviceCenter_KeyPress(object sender, KeyPressEventArgs e)
        {
          

          
        }

        private void txtTown_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                BaseCls.GlbReportName = "Load Town Code";
                    loadSeviceCenter();
            }
            
        }

        private void grdServiceDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {

                try
                {
                    lblServiceCenter.Text = grdServiceDetails.Rows[0].Cells["SVC_ID"].Value.ToString();
                    lblTown.Text = grdServiceDetails.Rows[0].Cells["Town_Code"].Value.ToString();
                    txtSeviceCenter.Text = grdServiceDetails.Rows[0].Cells["Service_Center"].Value.ToString();
                    txtTown.Text = grdServiceDetails.Rows[0].Cells["Town"].Value.ToString();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void txtSeviceCenter_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceAgent);
                DataTable _result = CHNLSVC.CommonSearch.GetServiceAgent(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSeviceCenter;
                _CommonSearch.ShowDialog();
                txtSeviceCenter.Select();
                loadSeviceCenter();

                if (!string.IsNullOrEmpty(txtSeviceCenter.ToString()))
                {
                    if (_result.Rows.Count > 0)
                    {
                        //  lblServiceCenter.Text = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtSeviceCenter.Text.Trim()).Select(x => x.Field<string>("Discription")).First().ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("An error occurred while searching town details" + Environment.NewLine + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void txtTown_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                lblTown.Text = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 3;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
                DataTable _result = CHNLSVC.CommonSearch.GetTown(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtTown;
                _CommonSearch.ShowDialog();
                txtTown.Select();

                if (!string.IsNullOrEmpty(txtTown.ToString()))
                {
                    if (_result.Rows.Count > 0)
                    {
                        // lblTown.Text = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtTown.Text.Trim()).Select(x => x.Field<string>("TOWN")).First().ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("An error occurred while searching town details" + Environment.NewLine + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void txtTown_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTown.Text))
            {
                DataTable odt = CHNLSVC.General.GetTownByCode(txtTown.Text);
                if (odt.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid town code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTown.Text = "";
                    return;
                }
                else
                {
                   // txtTown.Text = odt.Rows[0]["mt_desc"].ToString();
                }
                loadSeviceCenter();
            }
            
        }

        private void txtSeviceCenter_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSeviceCenter.Text))
            {
                DataTable _dtserLoc = CHNLSVC.Sales.GetServiceAgentbyLoc(BaseCls.GlbUserComCode, txtSeviceCenter.Text);
                if (_dtserLoc.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid service center code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSeviceCenter.Text = "";
                    lblServiceCenter.Text = "";
                    return;
                }
                else
                {
                    lblServiceCenter.Text = _dtserLoc.Rows[0]["mbe_name"].ToString();
                }  
            }
           
        }

        private void txtSeviceCenter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearch_ServiceCeneter_Click(null, null);
            }
        }

        private void txtTown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSerchTown_Click(null, null);
            }
        }

       
    }
}
