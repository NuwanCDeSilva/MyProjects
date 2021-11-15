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
    public partial class AgreementTracker : Base
    {
        HPAgreementTracker objAgreementTracker;
        List<HPAgreementTracker> _lstAgreementTracker;
        string selectPC = BaseCls.GlbUserDefProf;
        DataTable dt;
        public AgreementTracker()
        {
            InitializeComponent();
            textProfitCenter.Text = BaseCls.GlbUserDefProf;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            try
            {
                SearchData();
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
       
        
        private void SearchData()
        {

            dataGridViewAccountDetails.AutoGenerateColumns = false;
            //dataGridViewAccountDetails.Columns["chkDateReceivedtoHO"].DefaultCellStyle.BackColor = Color.LightGray;
            //dataGridViewAccountDetails.Columns["DateRecervedtoHO"].DefaultCellStyle.BackColor = Color.LightGray;
            //dataGridViewAccountDetails.Columns["chkOtherClosedtYPE"].DefaultCellStyle.BackColor = Color.LightGray;
            //dataGridViewAccountDetails.Columns["cmbOtherClosedType"].DefaultCellStyle.BackColor = Color.LightGray;
            //dataGridViewAccountDetails.Columns["chkRecevedAgainDate"].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            //dataGridViewAccountDetails.Columns["ReceivedAgainDate"].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            //dataGridViewAccountDetails.Columns["Remark"].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            //dataGridViewAccountDetails.Columns["ReturnDate"].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            //dataGridViewAccountDetails.Columns["chkReturnDate"].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            //dataGridViewAccountDetails.Columns["chkCheckDate"].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            //dataGridViewAccountDetails.Columns["checkedDate"].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            HPAgreementTracker _hpagreementtracker = new HPAgreementTracker();
            _hpagreementtracker.Acc_no_from = 0;
            _hpagreementtracker.Acc_no_to = 9999999;

            Boolean validate = checkValidation();
            if (validate == true)
            {
                if (textProfitCenter.Text != "")
                {
                    _hpagreementtracker.Prft_center = textProfitCenter.Text;
                }
                else
                {
                    _hpagreementtracker.Prft_center = "%";
                }
                //if (textDOCNo.Text != "")
                if(!string.IsNullOrWhiteSpace(textDOCNo.Text))
                {
                    _hpagreementtracker.Doc_no = textDOCNo.Text;
                }
                else
                {
                    _hpagreementtracker.Doc_no = "%";
                }
                //if (textPODNo.Text != "")
                if (!string.IsNullOrWhiteSpace(textPODNo.Text))
                {
                    _hpagreementtracker.Pod_no = textPODNo.Text;
                }
                else
                {
                    _hpagreementtracker.Pod_no = "%";
                }
                //if (textAccountNumber.Text != "")
                if (!string.IsNullOrWhiteSpace(textAccountNumber.Text))
                {
                    _hpagreementtracker.Acc_no_from = Convert.ToInt32(textAccountNumber.Text);
                }
                //if (textAccountNumberTo.Text != "")
                if (!string.IsNullOrWhiteSpace(textAccountNumberTo.Text))
                {
                    _hpagreementtracker.Acc_no_to = Convert.ToInt32(textAccountNumberTo.Text);
                }
                _hpagreementtracker.Ischeck = "ALL";
                //Showroom sent SR//Received to H/O  HO/Check CHK/Return Agreement RA
                if (checkShowroomSent.Checked == true) _hpagreementtracker.Ischeck = "SHW";
                if (checkReceivedtoHO.Checked == true) _hpagreementtracker.Ischeck = "HOR";
                if (checkisCheck.Checked == true) _hpagreementtracker.Ischeck = "CHK";
                if (checkRtnAgreemnt.Checked == true) _hpagreementtracker.Ischeck = "RA";
                if (checkisnotcheck.Checked == true) _hpagreementtracker.Ischeck = "NCHK";
                _hpagreementtracker.Date_from = dtpFromDate.Value.Date;
                _hpagreementtracker.Date_to = dtpToDate.Value.Date;
                if (!string.IsNullOrWhiteSpace(textPODNo.Text) || !string.IsNullOrWhiteSpace(textDOCNo.Text))
                {

                    _hpagreementtracker.Date_from = Convert.ToDateTime("01/01/0001");
                    _hpagreementtracker.Date_to = Convert.ToDateTime("01/01/3999"); 
                }
                if (!string.IsNullOrWhiteSpace(textAccountNumberTo.Text) || !string.IsNullOrWhiteSpace(textAccountNumber.Text))
                {
                    _hpagreementtracker.Date_from = Convert.ToDateTime("01/01/0001");
                    _hpagreementtracker.Date_to = Convert.ToDateTime("01/01/3999"); 
                }
                 dt = new DataTable();

                dt = CHNLSVC.Sales.SearchAgreementTrackingData(_hpagreementtracker.Prft_center, _hpagreementtracker.Doc_no, _hpagreementtracker.Pod_no, _hpagreementtracker.Date_from, _hpagreementtracker.Date_to, _hpagreementtracker.Acc_no_from, _hpagreementtracker.Acc_no_to, _hpagreementtracker.Ischeck);
                
                if (dt.Rows.Count > 0)
                {
                    //btnSave.Enabled = true;
                    dataGridViewAccountDetails.DataSource = dt;
                   
                    DataTable dttemp = dt;
                    int rowcount = 0;
                    foreach (DataGridViewRow dgvr in dataGridViewAccountDetails.Rows)
                    {
                        string Acc_no = Convert.ToString(dgvr.Cells["AccNo"].Value);
                        dataGridViewAccountDetails.Rows[rowcount].Cells["ShowrmRecevd"].Value = "NO";
                        
                        dataGridViewAccountDetails.Rows[rowcount].Cells["chkYesNo"].Value = "NO";
                        dataGridViewAccountDetails.Rows[rowcount].Cells["OthrClsTypeYesNo"].Value = "NO";
                        dataGridViewAccountDetails.Rows[rowcount].Cells["ReturnYesNo"].Value = "NO";
                        dataGridViewAccountDetails.Rows[rowcount].Cells["RecAgainYesNo"].Value = "NO";
                        dataGridViewAccountDetails.Rows[rowcount].Cells["chkRecevedAgainDate"].ReadOnly = true;
                        //dt = CHNLSVC.Sales.GetHeadofficeRecdata(Acc_no);
                        checkboxSelectioncheck();
                        if (dt.Rows.Count > 0)
                        {
                            dataGridViewAccountDetails.Visible = true;
                            panel2.Visible = true;
                            string expression = "AGR_ACCOUNT like '%" + Acc_no + "%'";
                            DataRow[] foundRows = dt.Select(expression);
                            DataRow dr = foundRows[0];
                            if (dr["HO_REC"].ToString() == "Y")
                            {
                                dataGridViewAccountDetails.Rows[rowcount].Cells["chkDateReceivedtoHO"].ReadOnly = true;
                                dataGridViewAccountDetails.Rows[rowcount].Cells["chkDateReceivedtoHO"].Value = true;
                                dataGridViewAccountDetails.Rows[rowcount].Cells["DateRecervedtoHO"].Value = dr["AGR_DATE_RECEIVED"].ToString();
                                dataGridViewAccountDetails.Rows[rowcount].Cells["ShowrmRecevd"].Value = "YES";
                                dataGridViewAccountDetails.Rows[rowcount].Cells["ShowrmRecevd"].ReadOnly = true;
                            }
                            //dgvr.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                            string checkdate = dr["AGR_MODIFIEDDATE"].ToString();
                            if (checkdate != "31-DEC-2999")
                            {
                                dataGridViewAccountDetails.Rows[rowcount].Cells["chkCheckDate"].ReadOnly = true;
                                dataGridViewAccountDetails.Rows[rowcount].Cells["chkCheckDate"].Value = true;
                                dataGridViewAccountDetails.Rows[rowcount].Cells["checkedDate"].Value = dr["AGR_MODIFIEDDATE"].ToString();
                                dataGridViewAccountDetails.Rows[rowcount].Cells["chkYesNo"].Value = "YES";
                                
                            }
                            string OthClsDate = dr["AGR_CLOSUREDATE"].ToString();
                            string OthClsType = dr["AGR_CLOSURETYPE"].ToString();
                            if (OthClsDate != "31-DEC-2999")
                            {
                                dataGridViewAccountDetails.Rows[rowcount].Cells["chkOtherClosedtYPE"].ReadOnly = true;
                                dataGridViewAccountDetails.Rows[rowcount].Cells["chkOtherClosedtYPE"].Value = true;
                                DataGridViewComboBoxCell ddl = new DataGridViewComboBoxCell();
                                ddl = (DataGridViewComboBoxCell)dataGridViewAccountDetails.Rows[rowcount].Cells["cmbOtherClosedType"];
                                dataGridViewAccountDetails.Rows[rowcount].Cells["OthrClsTypeYesNo"].Value = "YES";
                                ddl.Items.Add(OthClsType);
                                ddl.ReadOnly = true;
                                if (ddl.Items.Count > 0) ddl.Value = ddl.Items[0].ToString();

                            }
                            string datertn = dr["AGR_DATERETURNED"].ToString();
                            if (datertn != "31-DEC-2999")
                            {
                                dataGridViewAccountDetails.Rows[rowcount].Cells["chkReturnDate"].ReadOnly = true;
                                dataGridViewAccountDetails.Rows[rowcount].Cells["chkRecevedAgainDate"].ReadOnly = false;
                                dataGridViewAccountDetails.Rows[rowcount].Cells["chkOtherClosedtYPE"].ReadOnly = true;
                                dataGridViewAccountDetails.Rows[rowcount].Cells["chkReturnDate"].Value = true;
                                dataGridViewAccountDetails.Rows[rowcount].Cells["ReturnDate"].Value = dr["AGR_DATERETURNED"].ToString();
                                dataGridViewAccountDetails.Rows[rowcount].Cells["ReturnYesNo"].Value = "YES";
                                DataTable dtremark = CHNLSVC.Sales.GetReturnType(Acc_no);
                                DataGridViewComboBoxCell ddl = new DataGridViewComboBoxCell();
                                ddl = (DataGridViewComboBoxCell)dataGridViewAccountDetails.Rows[rowcount].Cells["Remark"];
                                DataRow drtype;
                                if (dtremark.Rows.Count > 0)
                                {
                                    drtype = dtremark.Rows[0];
                                
                                string type = drtype["AGRD_DOC_NAME"].ToString();
                                ddl.Items.Add(type);
                                ddl.ReadOnly = true;
                                if (ddl.Items.Count > 0) ddl.Value = ddl.Items[0].ToString();
                                }
                            }
                            if (dr["AGR_STUS"].ToString() == "F")
                            {
                                dataGridViewAccountDetails.Rows[rowcount].Cells["chkDateReceivedtoHO"].ReadOnly = true;
                                dataGridViewAccountDetails.Rows[rowcount].Cells["chkCheckDate"].ReadOnly = true;
                                dataGridViewAccountDetails.Rows[rowcount].Cells["chkOtherClosedtYPE"].ReadOnly = true;
                                dataGridViewAccountDetails.Rows[rowcount].Cells["chkReturnDate"].ReadOnly = true;
                                dataGridViewAccountDetails.Rows[rowcount].Cells["chkRecevedAgainDate"].ReadOnly = true;
                            }
                                

                        }
                        rowcount++;
                        
                    }
                   
                    if (dt.Rows.Count == 0)
                    {
                        dataGridViewAccountDetails.DataSource = dttemp;
                        
                    }
                    ValidateRow();
                }
                else
                {
                    dataGridViewAccountDetails.DataSource = dt;
                    MessageBox.Show("No data found...", "Agreement Tracker", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //panel2.Visible = false;
                    //dataGridViewAccountDetails.Visible = false;
                    btnSave.Enabled = false;
                }
            }

        }
       
        private bool checkValidation()
        {
        bool validate = true;
        if (!textAccountNumber.Text.All(c => Char.IsNumber(c)) && !textAccountNumberTo.Text.All(c => Char.IsNumber(c)))
        {
            MessageBox.Show("Please enter only numbers", "Account Number", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textAccountNumber.Text = "";
            textAccountNumber.Focus();
            textAccountNumberTo.Text = "";
            validate = false;
        }

        else if (!textAccountNumber.Text.All(c => Char.IsNumber(c)) && validate == true)
             {
                 MessageBox.Show("Please enter only numbers", "Account Number", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textAccountNumber.Text = "";
                textAccountNumber.Focus();
                validate = false;
             }
        else if (!textAccountNumberTo.Text.All(c => Char.IsNumber(c)) && validate == true)
             {
                 MessageBox.Show("Please enter only numbers", "Account Number", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textAccountNumberTo.Text = "";
            textAccountNumberTo.Focus();
            validate = false;
              }
        else if (textProfitCenter.Text == string.Empty)
              {
                  MessageBox.Show("Please select a profit center", "Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  textProfitCenter.Focus();
                  validate = false;
              }
        else if (string.IsNullOrWhiteSpace(textProfitCenter.Text))
              {
               MessageBox.Show("Please select a profit center(Space Not Allowed)", "Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Information);
               textProfitCenter.Focus();
               validate = false;      
               }
            return validate;
         }

        private void checkboxSelectioncheck()
        {
           
        }

        //private void dataGridViewAccountDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    //dataGridViewAccountDetails_CurrentCellDirtyStateChanged(null,null);
        //    btnSave.Enabled = true;
        //    int index = dataGridViewAccountDetails.CurrentRow.Index;
        //    //foreach (DataGridViewRow dgvr in dataGridViewAccountDetails.Rows)
        //    //{
        //    if (e.ColumnIndex == dataGridViewAccountDetails.Columns["ShowrmRecevd"].Index)
        //    {

        //        DataGridViewCheckBoxCell ch4 = new DataGridViewCheckBoxCell();
        //        ch4 = (DataGridViewCheckBoxCell)dataGridViewAccountDetails.Rows[dataGridViewAccountDetails.CurrentRow.Index].Cells["chkDateReceivedtoHO"];

        //        // Boolean val = Convert.ToBoolean(ch4.Value);
        //        if (ch4.ReadOnly == false)
        //        {

        //            if (dataGridViewAccountDetails.Rows[Convert.ToInt32(e.RowIndex)].Cells["ShowrmRecevd"].Value.ToString() == "YES")
        //            {
        //                dataGridViewAccountDetails.Rows[index].Cells["DateRecervedtoHO"].Value = DBNull.Value;
        //                dataGridViewAccountDetails.Rows[index].Cells["chkCheckDate"].ReadOnly = false;
        //                dataGridViewAccountDetails.Rows[index].Cells["chkOtherClosedtYPE"].ReadOnly = false;
        //                dataGridViewAccountDetails.Rows[index].Cells["chkReturnDate"].ReadOnly = false;
        //                dataGridViewAccountDetails.Rows[index].Cells["chkRecevedAgainDate"].ReadOnly = false;
        //                dataGridViewAccountDetails.Rows[index].Cells["ShowrmRecevd"].Value = "No";
        //            }
        //            else
        //            {
        //                dataGridViewAccountDetails.Rows[index].Cells["DateRecervedtoHO"].Value = DateTime.Now.ToString("dd/MMM/yyyy");
        //                ch4.Selected = true;
        //                dataGridViewAccountDetails.Rows[index].Cells["chkCheckDate"].ReadOnly = true;
        //                dataGridViewAccountDetails.Rows[index].Cells["chkOtherClosedtYPE"].ReadOnly = true;
        //                dataGridViewAccountDetails.Rows[index].Cells["chkReturnDate"].ReadOnly = true;
        //                dataGridViewAccountDetails.Rows[index].Cells["chkRecevedAgainDate"].ReadOnly = true;
        //                dataGridViewAccountDetails.Rows[index].Cells["ShowrmRecevd"].Value = "Yes";
        //                // MessageBox.Show("First update Recieved Date,then Save & Continue", "Received to Head Office", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            }

        //        }
        //    }
        //    else if (e.ColumnIndex == dataGridViewAccountDetails.Columns["chkCheckDate"].Index)
        //    {
        //        DataGridViewCheckBoxCell ch6 = new DataGridViewCheckBoxCell();
        //        ch6 = (DataGridViewCheckBoxCell)dataGridViewAccountDetails.Rows[dataGridViewAccountDetails.CurrentRow.Index].Cells["chkCheckDate"];
        //        if (ch6.ReadOnly == false)
        //        {

        //            if (ch6.Value == null)
        //            {
        //                ch6.Value = true;
        //            }
        //            switch (ch6.Value.ToString())
        //            {
        //                case "False":
        //                    dataGridViewAccountDetails.Rows[index].Cells["checkedDate"].Value = DBNull.Value;
        //                    break;
        //                case "True":
        //                    dataGridViewAccountDetails.Rows[index].Cells["checkedDate"].Value = DateTime.Now.ToString("dd/MMM/yyyy");
        //                    ch6.Selected = true;
        //                    ch6.Selected = true;
        //                    break;
        //            }
        //        }
        //    }
        //    else if (e.ColumnIndex == dataGridViewAccountDetails.Columns["chkOtherClosedtYPE"].Index)
        //    {
        //        DataGridViewCheckBoxCell ch8 = new DataGridViewCheckBoxCell();
        //        ch8 = (DataGridViewCheckBoxCell)dataGridViewAccountDetails.Rows[dataGridViewAccountDetails.CurrentRow.Index].Cells["chkOtherClosedtYPE"];
        //        DataGridViewCheckBoxCell ch6 = new DataGridViewCheckBoxCell();
        //        ch6 = (DataGridViewCheckBoxCell)dataGridViewAccountDetails.Rows[dataGridViewAccountDetails.CurrentRow.Index].Cells["chkCheckDate"];

        //        if (ch8.ReadOnly == false)
        //        {
        //            if (ch8.Value == null)
        //            {
        //                ch8.Value = true;
        //            }
        //            switch (ch8.Value.ToString())
        //            {
        //                case "False":
        //                    break;
        //                case "True":
        //                    string type = "OT";
        //                    dt = new DataTable();
        //                    dt = CHNLSVC.Sales.LoadOtherClosedTypes(type);
        //                    DataGridViewComboBoxCell ddl = new DataGridViewComboBoxCell();
        //                    ddl = (DataGridViewComboBoxCell)dataGridViewAccountDetails.Rows[dataGridViewAccountDetails.CurrentRow.Index].Cells["cmbOtherClosedType"];
        //                    ddl.DataSource = dt;
        //                    ddl.DisplayMember = "HPD_DESC";
        //                    ch8.Selected = true;
        //                    dataGridViewAccountDetails.Rows[index].Cells["checkedDate"].Value = DateTime.Now.ToString("dd/MMM/yyyy");
        //                    ch6.Selected = true;
        //                    break;
        //            }
        //        }
        //    }
        //    else if (e.ColumnIndex == dataGridViewAccountDetails.Columns["chkReturnDate"].Index)
        //    {
        //        DataGridViewCheckBoxCell ch10 = new DataGridViewCheckBoxCell();
        //        ch10 = (DataGridViewCheckBoxCell)dataGridViewAccountDetails.Rows[dataGridViewAccountDetails.CurrentRow.Index].Cells["chkReturnDate"];
        //        if (ch10.ReadOnly == false)
        //        {
        //            if (ch10.Value == null)
        //            {
        //                ch10.Value = true;
        //            }
        //            switch (ch10.Value.ToString())
        //            {
        //                case "False":
        //                    break;
        //                case "True":
        //                    string type = "HP";
        //                    dt = new DataTable();
        //                    dt = CHNLSVC.Sales.LoadOtherClosedTypes(type);
        //                    DataGridViewComboBoxCell ddl = new DataGridViewComboBoxCell();
        //                    ddl = (DataGridViewComboBoxCell)dataGridViewAccountDetails.Rows[dataGridViewAccountDetails.CurrentRow.Index].Cells["Remark"];
        //                    dataGridViewAccountDetails.Rows[index].Cells["ReturnDate"].Value = DateTime.Now.ToString("dd/MMM/yyyy");
        //                    // ddl.Items.Insert(0, "--Select--");
        //                    ddl.DataSource = dt;
        //                    ddl.DisplayMember = "HPD_DESC";
        //                    ch10.Selected = true;

        //                    break;
        //            }
        //        }
        //        }
        //        else if (e.ColumnIndex == dataGridViewAccountDetails.Columns["chkRecevedAgainDate"].Index)
        //        {
        //            DataGridViewCheckBoxCell ch13 = new DataGridViewCheckBoxCell();
        //            ch13 = (DataGridViewCheckBoxCell)dataGridViewAccountDetails.Rows[dataGridViewAccountDetails.CurrentRow.Index].Cells["chkRecevedAgainDate"];
        //            if (ch13.ReadOnly == false)
        //            {
        //                if (ch13.Value == null)
        //                {
        //                    ch13.Value = true;
        //                }
        //                switch (ch13.Value.ToString())
        //                {
        //                    case "False":
        //                        break;
        //                    case "True":
        //                        dataGridViewAccountDetails.Rows[index].Cells["ReceivedAgainDate"].Value = DateTime.Now.ToString("dd/MMM/yyyy");
        //                        ch13.Selected = true;
        //                        break;
        //                }
        //            }
        //        }
               
        //    //}
        //}

        private void AnalyzeGrid()
        {
            //int rowCount = 0;
            //foreach (DataGridViewRow dgvr in dataGridViewAccountDetails.Rows)
            //{
            //    objAgreementTracker = new HPAgreementTracker();
            //    DataGridViewComboBoxCell cmbOthclsType = new DataGridViewComboBoxCell();
            //    cmbOthclsType = (DataGridViewComboBoxCell)dgvr.Cells["cmbOtherClosedType"];
            //    DataGridViewComboBoxCell cmbreturn = new DataGridViewComboBoxCell();
            //    cmbreturn = (DataGridViewComboBoxCell)dgvr.Cells["Remark"];
            //    DataGridViewCheckBoxCell chkRectoHO = dgvr.Cells["chkDateReceivedtoHO"] as DataGridViewCheckBoxCell;
            //    DataGridViewCheckBoxCell chkAgrchk = dgvr.Cells["chkCheckDate"] as DataGridViewCheckBoxCell;
            //    DataGridViewCheckBoxCell chkOthType = dgvr.Cells["chkOtherClosedtYPE"] as DataGridViewCheckBoxCell;
            //    DataGridViewCheckBoxCell chkRtnDate = dgvr.Cells["chkReturnDate"] as DataGridViewCheckBoxCell;
            //    DataGridViewCheckBoxCell chkRtnRcvDate = dgvr.Cells["chkRecevedAgainDate"] as DataGridViewCheckBoxCell;
            //    if (Convert.ToBoolean(chkRectoHO.Value) == false)
            //    {
            //        dataGridViewAccountDetails.Rows[rowCount].Cells["DateRecervedtoHO"].Value = DBNull.Value;
            //        dataGridViewAccountDetails.Rows[rowCount].Cells["chkCheckDate"].ReadOnly = false;
            //        dataGridViewAccountDetails.Rows[rowCount].Cells["chkOtherClosedtYPE"].ReadOnly = false;
            //        dataGridViewAccountDetails.Rows[rowCount].Cells["chkReturnDate"].ReadOnly = false;
            //        dataGridViewAccountDetails.Rows[rowCount].Cells["chkRecevedAgainDate"].ReadOnly = false;
            //    }
            //    rowCount++;
            //}
            for (int i = 0; i < dataGridViewAccountDetails.Rows.Count; i++)
            {
                if (dataGridViewAccountDetails.Rows[i].Cells["chkDateReceivedtoHO"].Value == "") dataGridViewAccountDetails.Rows[i].Cells["chkDateReceivedtoHO"].Value = false;
                if (dataGridViewAccountDetails.Rows[i].Cells["chkDateReceivedtoHO"].Value != null && Convert.ToBoolean(dataGridViewAccountDetails.Rows[i].Cells["chkDateReceivedtoHO"].Value) == true)
                {
                   
                }
              
            }
        }

        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    if (MessageBox.Show("Are you sure ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
        //    if (dataGridViewAccountDetails.Rows.Count > 0)
        //    {
        //        _lstAgreementTracker = fillToAgreementTrackerDetails();
        //        if (_lstAgreementTracker.Count <= 0)
        //        {
        //            MessageBox.Show("Please select at least 1 item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }

        //        string _error = "";
        //        int result = CHNLSVC.Sales.UpdateToAgreementTracker(_lstAgreementTracker, out _error);
        //        if (result == -1)
        //        {
        //            MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }
        //        else
        //        {
        //            MessageBox.Show("Successfully Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            //btnClear_Click(null, null);
        //        }



        //    }
        //    else
        //    {
        //        MessageBox.Show("Please search items to continue the process", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //}
        private List<HPAgreementTracker> fillToAgreementTrackerDetails()
        {
            _lstAgreementTracker = new List<HPAgreementTracker>();
            foreach (DataGridViewRow dgvr in dataGridViewAccountDetails.Rows)
            {
                objAgreementTracker = new HPAgreementTracker();
                DataGridViewComboBoxCell cmbOthclsType = new DataGridViewComboBoxCell();
                cmbOthclsType = (DataGridViewComboBoxCell)dgvr.Cells["cmbOtherClosedType"];
                DataGridViewComboBoxCell cmbreturn = new DataGridViewComboBoxCell();
                cmbreturn = (DataGridViewComboBoxCell)dgvr.Cells["Remark"];
                DataGridViewCheckBoxCell chkRectoHO = dgvr.Cells["chkDateReceivedtoHO"] as DataGridViewCheckBoxCell;
                DataGridViewCheckBoxCell chkAgrchk = dgvr.Cells["chkCheckDate"] as DataGridViewCheckBoxCell;
                DataGridViewCheckBoxCell chkOthType = dgvr.Cells["chkOtherClosedtYPE"] as DataGridViewCheckBoxCell;
                DataGridViewCheckBoxCell chkRtnDate = dgvr.Cells["chkReturnDate"] as DataGridViewCheckBoxCell;
                DataGridViewCheckBoxCell chkRtnRcvDate = dgvr.Cells["chkRecevedAgainDate"] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chkOthType.Value) == true && cmbOthclsType.Value == null)
                {
                    MessageBox.Show("Select Other Close Type", "Other Close Type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
                if (Convert.ToBoolean(chkRectoHO.Value) == false && (Convert.ToBoolean(chkAgrchk.Value) == true || Convert.ToBoolean(chkOthType.Value) == true || Convert.ToBoolean(chkRtnDate.Value) == true || Convert.ToBoolean(chkRtnRcvDate.Value) == true))
                {
                    MessageBox.Show("Select Other Close Type", "Other Close Type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                }
                //if (Convert.ToBoolean(chkRtnDate.Value) == true && cmbreturn.Value == null)
                //{
                //    MessageBox.Show("Select Type of Return Remark", "Return Remark", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    break;
                //}
                //Initial insert into hpt_accounts_agreement tbl
                if (Convert.ToBoolean(chkRectoHO.Value) == true && Convert.ToBoolean(chkAgrchk.Value) == false && Convert.ToBoolean(chkOthType.Value) == false && Convert.ToBoolean(chkRtnDate.Value) == false)
                {
                    objAgreementTracker.Process_type = "DateRecToHO";
                    objAgreementTracker.Acc_no = Convert.ToString(dgvr.Cells["AccNo"].Value);
                    objAgreementTracker.Insert_date = Convert.ToDateTime(dgvr.Cells["DateRecervedtoHO"].Value);
                    objAgreementTracker.Insert_user_com = BaseCls.GlbUserComCode;
                    _lstAgreementTracker.Add(objAgreementTracker);
                   // MessageBox.Show("Recored only update Received to HO stage", "Return Remark", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //Agreement check stage, set status to F due to return option uncheck
                else if (Convert.ToBoolean(chkRectoHO.Value) == true && Convert.ToBoolean(chkAgrchk.Value) == true && Convert.ToBoolean(chkOthType.Value) == false && Convert.ToBoolean(chkRtnDate.Value) == false)
                {
                    objAgreementTracker.Process_type = "ConfirmedChecked";
                    objAgreementTracker.Acc_no = Convert.ToString(dgvr.Cells["AccNo"].Value);
                    objAgreementTracker.Insert_date = Convert.ToDateTime(dgvr.Cells["checkedDate"].Value);
                    objAgreementTracker.Insert_user_com = BaseCls.GlbUserComCode;
                    _lstAgreementTracker.Add(objAgreementTracker);
                }
                //Agreement Other Close type, set status to F due to return option uncheck
                else if (Convert.ToBoolean(chkRectoHO.Value) == true && Convert.ToBoolean(chkOthType.Value) == true && Convert.ToBoolean(chkRtnDate.Value) == false)
                {
                    DataGridViewComboBoxCell ddl = new DataGridViewComboBoxCell();
                    ddl = (DataGridViewComboBoxCell)dgvr.Cells["cmbOtherClosedType"];
                    if (ddl.Value != null)
                    {
                        objAgreementTracker.Process_type = "ConfirmedOtherClsType";
                        objAgreementTracker.Acc_no = Convert.ToString(dgvr.Cells["AccNo"].Value);
                        objAgreementTracker.Insert_date = Convert.ToDateTime(DateTime.Now.ToString("dd/MMM/yyyy"));
                        objAgreementTracker.Insert_user_com = BaseCls.GlbUserComCode;
                        objAgreementTracker.Oth_close_typ = ddl.Value.ToString();
                        _lstAgreementTracker.Add(objAgreementTracker);
                    }
                }
                //Agreement Return stage ,set status to P due to return option uncheck all date value rollback to 31/DEC/2999
                else if (Convert.ToBoolean(chkRectoHO.Value) == true && Convert.ToBoolean(chkRtnDate.Value) == true && Convert.ToBoolean(chkRtnRcvDate.Value) == false)
                {
                    DataGridViewComboBoxCell ddl = new DataGridViewComboBoxCell();
                    ddl = (DataGridViewComboBoxCell)dgvr.Cells["Remark"];
                    if (ddl.Value != null)
                    {
                        objAgreementTracker.Process_type = "ReturnAgreement";
                        objAgreementTracker.Acc_no = Convert.ToString(dgvr.Cells["AccNo"].Value);
                        objAgreementTracker.Insert_date = Convert.ToDateTime(dgvr.Cells["ReturnDate"].Value);
                        objAgreementTracker.Insert_user_com = BaseCls.GlbUserComCode;
                        objAgreementTracker.Oth_close_typ = ddl.Value.ToString();
                        objAgreementTracker.Checkdate = Convert.ToDateTime(dgvr.Cells["checkedDate"].Value);
                        _lstAgreementTracker.Add(objAgreementTracker);
                    }
                }
                //Agreement Return stage with Other close type ,set status to P due to return option uncheck all date value rollback to 31/DEC/2999
                //else if (Convert.ToBoolean(chkRectoHO.Value) == true && Convert.ToBoolean(chkOthType.Value) == true && Convert.ToBoolean(chkRtnDate.Value) == true && Convert.ToBoolean(chkRtnRcvDate.Value) == false)
                //{
                //    DataGridViewComboBoxCell ddl = new DataGridViewComboBoxCell();
                //    ddl = (DataGridViewComboBoxCell)dgvr.Cells["Remark"];
                //    DataGridViewComboBoxCell ddlOthCls = new DataGridViewComboBoxCell();
                //    ddlOthCls = (DataGridViewComboBoxCell)dgvr.Cells["cmbOtherClosedType"];
                //    if (ddl.Value != null && ddlOthCls.Value != null)
                //    {
                //        objAgreementTracker.Process_type = "ReturnAgreementwithClsType";
                //        objAgreementTracker.Acc_no = Convert.ToString(dgvr.Cells["AccNo"].Value);
                //        objAgreementTracker.Insert_date = Convert.ToDateTime(dgvr.Cells["ReturnDate"].Value);
                //        objAgreementTracker.Insert_user_com = BaseCls.GlbUserComCode;
                //        objAgreementTracker.Oth_close_typ = ddl.Value.ToString();
                //        objAgreementTracker.Remark = ddlOthCls.Value.ToString();
                //        objAgreementTracker.ClsTypeDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MMM/yyyy"));
                //        objAgreementTracker.Checkdate = Convert.ToDateTime(dgvr.Cells["checkedDate"].Value);
                //        _lstAgreementTracker.Add(objAgreementTracker);
                //    }
                //    else
                //    {
                //        MessageBox.Show("You have to select both of Closer type and return type,others updated", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        
                //    }
                //}
                //Agreement Received Again stage ,set recored into Received to HO stage
                else if (Convert.ToBoolean(chkRectoHO.Value) == true  && Convert.ToBoolean(chkRtnDate.Value) == true && Convert.ToBoolean(chkRtnRcvDate.Value) == true)
                {
                    
                        objAgreementTracker.Process_type = "ReceivedAgain";
                        objAgreementTracker.Acc_no = Convert.ToString(dgvr.Cells["AccNo"].Value);
                        objAgreementTracker.Insert_date = Convert.ToDateTime(dgvr.Cells["ReceivedAgainDate"].Value);
                        objAgreementTracker.Insert_user_com = BaseCls.GlbUserComCode;
                       _lstAgreementTracker.Add(objAgreementTracker);
                    
                }
                //Initially return stage
                else if (Convert.ToBoolean(chkRectoHO.Value) == true && Convert.ToBoolean(chkAgrchk.Value) == true && Convert.ToBoolean(chkRtnDate.Value) == true)
                {

                    //objAgreementTracker.Process_type = "Return";
                    //objAgreementTracker.Acc_no = Convert.ToString(dgvr.Cells["AccNo"].Value);
                    //objAgreementTracker.Insert_date = Convert.ToDateTime(dgvr.Cells["ReceivedAgainDate"].Value);
                    //objAgreementTracker.Insert_user_com = BaseCls.GlbUserComCode;
                    //_lstAgreementTracker.Add(objAgreementTracker);

                }
             }
            return _lstAgreementTracker;
        }
        
        private void dataGridViewAccountDetails_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            
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
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + selectPC + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AccountDate:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + selectPC + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.Scheme:
                //    {
                //        paramsText.Append(txtSchemeCD_MM_new.Text.Trim() + seperator);
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.NIC:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }
      

        private void BtnSearcgAccNo_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AccountDate);
            DataTable _result = CHNLSVC.CommonSearch.GetHpAccountDateSearchData(_CommonSearch.SearchParams, null, null, DateTime.Now.Date.AddMonths(-1), DateTime.Now.Date);
            _CommonSearch.dtpFrom.Value = DateTime.Now.Date.AddMonths(-1);
            _CommonSearch.dtpTo.Value = DateTime.Now.Date;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtAcc_no;
            //_commonSearch.IsSearchEnter = true; 
            this.Cursor = Cursors.Default;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtAcc_no.Select();

        }

        //private void txtAcc_no_TextChanged(object sender, EventArgs e)
        //{
        //    string _account = txtAcc_no.Text;
        //    if (txtAcc_no.Text.Length > 0)
        //    {
               
        //        BindAccountItem(_account);

        //    }
        //}
        private void BindAccountItem(string _account,string _inv_no)
        {
            if (!string.IsNullOrWhiteSpace(textProfitCenter.Text))
            {
                selectPC = textProfitCenter.Text;
            }
            else
            {
                selectPC = BaseCls.GlbUserDefProf;
            }
           // selectPC = "AA2MW";
            List<InvoiceItem> _itemList = new List<InvoiceItem>();
            List<InvoiceHeader> _invoice = CHNLSVC.Sales.GetInvoiceByAccountNo(BaseCls.GlbUserComCode, selectPC, _account);
            InvoiceHeader _hdrs = null;
            BindGurantors(_account);
            //Account_Load();
            //if (_invoice != null)
            //    if (_invoice.Count > 0)
            //        _hdrs = CHNLSVC.Sales.GetInvoiceHeaderDetails(_invoice[0].Sah_inv_no);
            _hdrs = CHNLSVC.Sales.GetInvoiceHeaderDetails_AGR(_inv_no);
            
            DataTable _table = CHNLSVC.Sales.GetInvoiceItemDetails(BaseCls.GlbUserComCode, selectPC, _account);
            if (_table.Rows.Count > 0)
            {
                //_table = SetEmptyRow(_table);
                gvATradeItem.AutoGenerateColumns = false;
                gvATradeItem.DataSource = _table;
            }
            else if (_invoice != null)
                if (_invoice.Count > 0)
                {
                    _invoice = _invoice.OrderByDescending(x => x.Sah_direct).ToList();
                    #region New Method
                    var _sales = from _lst in _invoice
                                 where _lst.Sah_direct == true
                                 select _lst;

                    foreach (InvoiceHeader _hdr in _sales)
                    {
                        string _invoiceno = _hdr.Sah_inv_no;
                        List<InvoiceItem> _invItem = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_invoiceno);
                        var _forwardSale = from _lst in _invItem
                                           where _lst.Sad_qty - _lst.Sad_do_qty > 0
                                           select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_qty - _lst.Sad_do_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = true };

                        var _deliverdSale = from _lst in _invItem
                                            where _lst.Sad_do_qty - _lst.Sad_srn_qty > 0
                                            select new InvoiceItem { Sad_alt_itm_cd = _lst.Sad_alt_itm_cd, Sad_alt_itm_desc = _lst.Sad_alt_itm_desc, Sad_comm_amt = _lst.Sad_comm_amt, Sad_disc_amt = _lst.Sad_disc_amt, Sad_disc_rt = _lst.Sad_disc_rt, Sad_do_qty = _lst.Sad_do_qty, Sad_fws_ignore_qty = _lst.Sad_fws_ignore_qty, Sad_inv_no = _lst.Sad_inv_no, Sad_tot_amt = _lst.Sad_tot_amt, Sad_srn_qty = _lst.Sad_srn_qty, Sad_qty = _lst.Sad_do_qty - _lst.Sad_srn_qty, Sad_is_promo = _lst.Sad_is_promo, Sad_warr_remarks = _lst.Sad_warr_remarks, Sad_warr_period = _lst.Sad_warr_period, Sad_uom = _lst.Sad_uom, Sad_warr_based = _lst.Sad_warr_based, Sad_unit_rt = _lst.Sad_unit_rt, Sad_unit_amt = _lst.Sad_unit_amt, Sad_seq_no = _lst.Sad_seq_no, Sad_seq = _lst.Sad_seq, Sad_res_no = _lst.Sad_res_no, Sad_res_line_no = _lst.Sad_res_line_no, Sad_promo_cd = _lst.Sad_promo_cd, Sad_print_stus = _lst.Sad_print_stus, Sad_pbook = _lst.Sad_pbook, Sad_pb_price = _lst.Sad_pb_price, Sad_pb_lvl = _lst.Sad_pb_lvl, Sad_outlet_dept = _lst.Sad_outlet_dept, Sad_merge_itm = _lst.Sad_merge_itm, Sad_job_no = _lst.Sad_job_no, Sad_job_line = _lst.Sad_job_line, Sad_itm_tp = _lst.Sad_itm_tp, Sad_itm_tax_amt = _lst.Sad_itm_tax_amt, Sad_itm_stus = _lst.Sad_itm_stus, Sad_itm_seq = _lst.Sad_itm_seq, Sad_itm_line = _lst.Sad_itm_line, Sad_itm_cd = _lst.Sad_itm_cd, Mi_longdesc = _lst.Mi_longdesc, Mi_brand = _lst.Mi_brand, Mi_model = _lst.Mi_model, Mi_act = false };

                        if (_forwardSale.Count() > 0)
                            _itemList.AddRange(_forwardSale);

                        if (_deliverdSale.Count() > 0)
                            _itemList.AddRange(_deliverdSale);
                    }
                    #endregion
                    //gvATradeItem.AutoGenerateColumns = false;
                    //gvATradeItem.DataSource = _itemList;
                }

            if (_hdrs != null)
            {
                BindCustomerDetails(_hdrs);
                Account_Load();
                pnlacccustomerview.Visible = true;
                pnlinvoiceview.Visible = true;
                pnlinvoiceview.Visible = true;
            }
            else
            {
                BindCustomerDetails(new InvoiceHeader());
            }



        }
        private void BindCustomerDetails(InvoiceHeader _hdr)
        {
            lblhirenic.Text = string.Empty;
            lblhirename.Text = string.Empty;
            lblhireaddrs.Text = string.Empty;
            lblHirerealNIC.Text = string.Empty;
            if (_hdr != null)
            {
                lblhirenic.Text = _hdr.Sah_cus_cd;
                lblhirename.Text = _hdr.Sah_cus_name;
                lblhireaddrs.Text = _hdr.Sah_d_cust_add1 + " " + _hdr.Sah_d_cust_add2;
                lblHirerealNIC.Text = _hdr.Sah_Nic;
            }
        }
        private void BindGurantors(string AccNo)
        {
             dt = CHNLSVC.Sales.Get_gurantors(AccNo);
            if (dt.Rows.Count > 0)
            {
                grvGuarantors.AutoGenerateColumns = false;
                grvGuarantors.DataSource = dt;
                label14.Visible = true;
                
                //DataRow dr1 = dt.Rows[0];
                //label24.Visible = true;
                //lblfstgrntrnic.Visible = true;
                //lblfsrtgrantername.Visible = true;
                //lblfrstgrnaddress.Visible = true;
                //lblfstgrntrnic.Text = dr1["mbe_nic"].ToString();
                //lblfsrtgrantername.Text = dr1["mbe_name"].ToString();
                //lblfrstgrnaddress.Text = dr1["mbe_add1"].ToString() + "" + dr1["mbe_add2"].ToString();
                //if (dt.Rows.Count > 1)
                //{
                    
                //    DataRow dr2 = dt.Rows[1];
                //    label25.Visible = true;
                //    label14.Visible = true;
                //    secgrnternic.Visible = true;
                //    secgurantername.Visible = true;
                //    secgurnteraddress.Visible = true;
                //    secgrnternic.Text = dr2["mbe_nic"].ToString();
                //    secgurantername.Text = dr2["mbe_name"].ToString();
                //    secgurnteraddress.Text = dr2["mbe_add1"].ToString() + "" + dr1["mbe_add2"].ToString();
                //}
            }
        }
        private void Account_Load()
        {
            
           
            //------------------------------
            DateTime today = CHNLSVC.Security.GetServerDateTime().Date;
            Decimal bal = CHNLSVC.Sales.Get_AccountBalance(today, txtAcc_no.Text.Trim());
            lblbalance.Text = string.Format("{0:n2}", bal);
            //-------------------
            HpAccount Acc = CHNLSVC.Sales.GetHP_Account_onAccNo(txtAcc_no.Text.Trim());
            lblstatus.Text = Acc.Hpa_stus == "A" ? "Active" : (Acc.Hpa_stus == "C" ? "Close" : (Acc.Hpa_stus == "R" ? "Revert" : "NOT FOUND"));
            lblhirevalue.Text = string.Format("{0:n2}", Acc.Hpa_hp_val);//Acc.Hpa_hp_val;
           
        }

        private void imgBtnPC_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textProfitCenter;
                //_CommonSearch.txtSearchbyword.Text = txtPC.Text;
                _CommonSearch.ShowDialog();
                textProfitCenter.Focus();
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

        private void checkShowroomSent_CheckedChanged(object sender, EventArgs e)
        {
            if (checkShowroomSent.Checked == true)
            {
                checkReceivedtoHO.Checked = false;
                checkisCheck.Checked = false;
                checkRtnAgreemnt.Checked = false;
                checkisnotcheck.Checked = false;
            }
        }

        private void checkReceivedtoHO_CheckedChanged(object sender, EventArgs e)
        {
            if (checkReceivedtoHO.Checked == true)
            {
                checkShowroomSent.Checked = false;
                checkisCheck.Checked = false;
                checkRtnAgreemnt.Checked = false;
                checkisnotcheck.Checked = false;
            }
        }

        private void checkisCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (checkisCheck.Checked == true)
            {
                checkShowroomSent.Checked = false;
                checkReceivedtoHO.Checked = false;
                checkRtnAgreemnt.Checked = false;
                checkisnotcheck.Checked = false;
            }
        }

        private void checkRtnAgreemnt_CheckedChanged(object sender, EventArgs e)
        {
            if (checkRtnAgreemnt.Checked == true)
            {
                checkShowroomSent.Checked = false;
                checkReceivedtoHO.Checked = false;
                checkisCheck.Checked = false;
                checkisnotcheck.Checked = false;
            }
        }

        private void textProfitCenter_DoubleClick(object sender, EventArgs e)
        {
            imgBtnPC_Click(null, null);
        }

        private void textProfitCenter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                imgBtnPC_Click(null, null);
            }
            else
                if (e.KeyCode == Keys.Enter)
                    btnSearch.Focus();
        }

        private void txtAcc_no_DoubleClick(object sender, EventArgs e)
        {
            BtnSearcgAccNo_Click(null, null);
        }

        private void txtAcc_no_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                BtnSearcgAccNo_Click(null, null);
            }
           
        }

        //private void btnCancel_Click(object sender, EventArgs e)
        //{
        //    textProfitCenter.Text = "";
        //    textDOCNo.Text = "";
        //    textPODNo.Text = "";
        //    textAccountNumber.Text = "";
        //    textAccountNumberTo.Text = "";
        //    dtpFromDate.Text = DateTime.Now.ToString();
        //    dtpToDate.Text = DateTime.Now.ToString();
        //    txtAcc_no.Text = "";
        //    pnlacccustomerview.Visible = false;
        //    pnlinvoiceview.Visible = false;
        //    btnSave.Enabled = false;
        //    textProfitCenter.Focus();
           
            

            
        //}

        private void checkisnotcheck_CheckedChanged(object sender, EventArgs e)
        {
            if (checkisnotcheck.Checked == true)
            {
                checkShowroomSent.Checked = false;
                checkReceivedtoHO.Checked = false;
                checkRtnAgreemnt.Checked = false;
                checkisCheck.Checked = false;
            }
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            try
            {

                btnSave.Enabled = false; 
                SearchData();
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

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            if (dataGridViewAccountDetails.Rows.Count > 0)
            {
                Boolean validate = checkValidationRec();
                if (validate == true)
                {
                    _lstAgreementTracker = fillToAgreementTrackerDetails();
                    if (_lstAgreementTracker.Count <= 0)
                    {
                        MessageBox.Show("No selected record for Save,Please update at least one.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string _error = "";
                    int result = CHNLSVC.Sales.UpdateToAgreementTracker(_lstAgreementTracker,BaseCls.GlbUserID, out _error);
                    if (result == -1)
                    {
                        MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Successfully Saved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SearchData();
                        btnSave.Enabled = false;
                        //btnClear_Click(null, null);
                    }



                }
                
            }
            else
            {
                MessageBox.Show("Please search items to continue the process", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool checkValidationRec()
        {
            bool validate = true;
             _lstAgreementTracker = new List<HPAgreementTracker>();
             foreach (DataGridViewRow dgvr in dataGridViewAccountDetails.Rows)
             {
                 objAgreementTracker = new HPAgreementTracker();
                 DataGridViewComboBoxCell cmbOthclsType = new DataGridViewComboBoxCell();
                 cmbOthclsType = (DataGridViewComboBoxCell)dgvr.Cells["cmbOtherClosedType"];
                 DataGridViewComboBoxCell cmbreturn = new DataGridViewComboBoxCell();
                 cmbreturn = (DataGridViewComboBoxCell)dgvr.Cells["Remark"];
                 DataGridViewCheckBoxCell chkRectoHO = dgvr.Cells["chkDateReceivedtoHO"] as DataGridViewCheckBoxCell;
                 DataGridViewCheckBoxCell chkAgrchk = dgvr.Cells["chkCheckDate"] as DataGridViewCheckBoxCell;
                 DataGridViewCheckBoxCell chkOthType = dgvr.Cells["chkOtherClosedtYPE"] as DataGridViewCheckBoxCell;
                 DataGridViewCheckBoxCell chkRtnDate = dgvr.Cells["chkReturnDate"] as DataGridViewCheckBoxCell;
                 DataGridViewCheckBoxCell chkRtnRcvDate = dgvr.Cells["chkRecevedAgainDate"] as DataGridViewCheckBoxCell;
                 
                 if (Convert.ToBoolean(chkRectoHO.Value) == false && (Convert.ToBoolean(chkAgrchk.Value) == true || Convert.ToBoolean(chkOthType.Value) == true || Convert.ToBoolean(chkRtnDate.Value) == true || Convert.ToBoolean(chkRtnRcvDate.Value) == true) && validate==true)
                 {
                     MessageBox.Show("First need to check Received Date", "Agreement Tracker", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     validate = false;
                     break;
                 }
                 if (Convert.ToBoolean(chkOthType.Value) == true && cmbOthclsType.Value == null && validate == true)
                 {
                     MessageBox.Show("Select Other Close Type", "Other Close Type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     validate = false;
                     break;
                 }
                 if (Convert.ToBoolean(chkRectoHO.Value) == true && Convert.ToBoolean(chkOthType.Value) == true && Convert.ToBoolean(chkRtnDate.Value) == true && validate == true)
                 {
                     MessageBox.Show("You can't update return type with Other Closer Type", "Other Close Type", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     validate = false;
                     break;
                 }
                 if (Convert.ToBoolean(chkRtnDate.Value) == true && Convert.ToBoolean(chkRtnRcvDate.Value) == false && validate == true)
                 {
                     DataGridViewComboBoxCell ddl = new DataGridViewComboBoxCell();
                     ddl = (DataGridViewComboBoxCell)dgvr.Cells["Remark"];
                     if(ddl.Value==null)
                     {
                         MessageBox.Show("Please select return remark", "Return Remark", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                         validate = false;
                         break;
                     }
                 }
                 if (Convert.ToBoolean(chkRectoHO.Value) == true && Convert.ToBoolean(chkOthType.Value) == true && Convert.ToBoolean(chkRtnDate.Value) == true && Convert.ToBoolean(chkRtnRcvDate.Value) == false && validate == true)
                 {
                     MessageBox.Show("Cannot continue return agreement within select Other Closer Type", "Return Agreement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     validate = false;
                     break;
                 }
                 if (Convert.ToBoolean(chkRectoHO.Value) == true && Convert.ToBoolean(chkOthType.Value) == true  && Convert.ToBoolean(chkRtnRcvDate.Value) == true && validate == true)
                 {
                     MessageBox.Show("Cannot Continue...", "Process Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     validate = false;
                     break;
                 }
               }
             return validate;
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure ?", "Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //textProfitCenter.Text = "";
            //textDOCNo.Text = "";
            //textPODNo.Text = "";
            //textAccountNumber.Text = "";
            //textAccountNumberTo.Text = "";
            //dtpFromDate.Text = DateTime.Now.ToString();
            //dtpToDate.Text = DateTime.Now.ToString();
            txtAcc_no.Text = "";
            pnlacccustomerview.Visible = false;
            pnlinvoiceview.Visible = false;
            textProfitCenter.Focus();
            SearchData();
            btnSave.Enabled = false;
        }

        private void txtAcc_no_KeyPress(object sender, KeyPressEventArgs e)
        {
               if (e.KeyChar == (char)Keys.Enter)
            {
                #region
                if (string.IsNullOrEmpty(txtAcc_no.Text)) return;
                //Clear_Agreenement(false, false);
                HpAccount _ac = CHNLSVC.Sales.GetHP_Account_onAccNo(txtAcc_no.Text);
                if (_ac == null || string.IsNullOrEmpty(_ac.Hpa_com)) { MessageBox.Show("Please select the valid account no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); //Clear_Agreenement(false, true); return; 
                return;
                }
               // txtUnitPrice.Text = string.Format("{0:n2}", _ac.Hpa_cash_val);

                BindAccountItem(txtAcc_no.Text, _ac.Hpa_invc_no);
                Account_Load();

                #endregion
            }
        }

        private void btnAccount_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textProfitCenter.Text))
            {
                selectPC = textProfitCenter.Text;
            }
            else
            {
                selectPC = BaseCls.GlbUserDefProf;
            }
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.IsSearchEnter = true;  
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.HpAccount);
                DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtAcc_no;
                _CommonSearch.ShowDialog();
                txtAcc_no.Select();

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

        private void txtAcc_no_Leave(object sender, EventArgs e)
        {
            if (txtAcc_no.Text != "")
                LoadAccount();
        }

        private void LoadAccount()
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AnalyzeGrid();
        }

       

      

        private void dataGridViewAccountDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //dataGridViewAccountDetails_CurrentCellDirtyStateChanged(null,null);
            btnSave.Enabled = true;
            int index = dataGridViewAccountDetails.CurrentRow.Index;
            //foreach (DataGridViewRow dgvr in dataGridViewAccountDetails.Rows)
            //{
            if (e.ColumnIndex == dataGridViewAccountDetails.Columns["ShowrmRecevd"].Index)
            {
               
                DataGridViewCheckBoxCell ch4 = new DataGridViewCheckBoxCell();
                ch4 = (DataGridViewCheckBoxCell)dataGridViewAccountDetails.Rows[dataGridViewAccountDetails.CurrentRow.Index].Cells["chkDateReceivedtoHO"];
                string dd = dataGridViewAccountDetails.Rows[Convert.ToInt32(e.RowIndex)].Cells["ShowrmRecevd"].Value.ToString();
                // Boolean val = Convert.ToBoolean(ch4.Value);
                if (ch4.ReadOnly == false)
                {

                    if (dataGridViewAccountDetails.Rows[Convert.ToInt32(e.RowIndex)].Cells["ShowrmRecevd"].Value.ToString() == "YES")
                    {
                        dataGridViewAccountDetails.Rows[index].Cells["DateRecervedtoHO"].Value = DBNull.Value;
                        dataGridViewAccountDetails.Rows[index].Cells["chkCheckDate"].ReadOnly = false;
                        dataGridViewAccountDetails.Rows[index].Cells["chkOtherClosedtYPE"].ReadOnly = false;
                        dataGridViewAccountDetails.Rows[index].Cells["chkReturnDate"].ReadOnly = false;
                        dataGridViewAccountDetails.Rows[index].Cells["chkRecevedAgainDate"].ReadOnly = false;
                        dataGridViewAccountDetails.Rows[index].Cells["ShowrmRecevd"].Value = "NO";
                        ch4.Value = false;
                    }
                    else
                    {
                        dataGridViewAccountDetails.Rows[index].Cells["DateRecervedtoHO"].Value = DateTime.Now.ToString("dd/MMM/yyyy");
                        ch4.Value = true;
                        dataGridViewAccountDetails.Rows[index].Cells["chkCheckDate"].ReadOnly = true;
                        dataGridViewAccountDetails.Rows[index].Cells["chkOtherClosedtYPE"].ReadOnly = true;
                        dataGridViewAccountDetails.Rows[index].Cells["chkReturnDate"].ReadOnly = true;
                        dataGridViewAccountDetails.Rows[index].Cells["chkRecevedAgainDate"].ReadOnly = true;
                        dataGridViewAccountDetails.Rows[index].Cells["ShowrmRecevd"].Value = "YES";
                        // MessageBox.Show("First update Recieved Date,then Save & Continue", "Received to Head Office", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
            else if (e.ColumnIndex == dataGridViewAccountDetails.Columns["chkYesNo"].Index)
            {
                DataGridViewCheckBoxCell ch6 = new DataGridViewCheckBoxCell();
                ch6 = (DataGridViewCheckBoxCell)dataGridViewAccountDetails.Rows[dataGridViewAccountDetails.CurrentRow.Index].Cells["chkCheckDate"];
                if (ch6.ReadOnly == false)
                {

                    if (dataGridViewAccountDetails.Rows[Convert.ToInt32(e.RowIndex)].Cells["chkYesNo"].Value.ToString() == "YES")
                    {
                    dataGridViewAccountDetails.Rows[index].Cells["checkedDate"].Value = DBNull.Value;
                    dataGridViewAccountDetails.Rows[index].Cells["chkYesNo"].Value = "NO";
                    ch6.Value = false;
                    }
                    else
                    {       
                    dataGridViewAccountDetails.Rows[index].Cells["checkedDate"].Value = DateTime.Now.ToString("dd/MMM/yyyy");
                    dataGridViewAccountDetails.Rows[index].Cells["chkYesNo"].Value = "YES";
                    ch6.Value = true;
                    }
                }
            }
            else if (e.ColumnIndex == dataGridViewAccountDetails.Columns["OthrClsTypeYesNo"].Index)
            {
                DataGridViewCheckBoxCell ch8 = new DataGridViewCheckBoxCell();
                ch8 = (DataGridViewCheckBoxCell)dataGridViewAccountDetails.Rows[dataGridViewAccountDetails.CurrentRow.Index].Cells["chkOtherClosedtYPE"];
                DataGridViewCheckBoxCell ch6 = new DataGridViewCheckBoxCell();
                ch6 = (DataGridViewCheckBoxCell)dataGridViewAccountDetails.Rows[dataGridViewAccountDetails.CurrentRow.Index].Cells["chkCheckDate"];
                DataGridViewCheckBoxCell ch10 = new DataGridViewCheckBoxCell();
                ch10 = (DataGridViewCheckBoxCell)dataGridViewAccountDetails.Rows[dataGridViewAccountDetails.CurrentRow.Index].Cells["chkReturnDate"];
                if (ch8.ReadOnly == false)
                {

                    if (dataGridViewAccountDetails.Rows[Convert.ToInt32(e.RowIndex)].Cells["OthrClsTypeYesNo"].Value.ToString() == "NO")
                    {
                        ch10.ReadOnly = true;
                        string type = "OT";
                        dt = new DataTable();
                        dt = CHNLSVC.Sales.LoadOtherClosedTypes(type);
                        DataGridViewComboBoxCell ddl = new DataGridViewComboBoxCell();
                        ddl = (DataGridViewComboBoxCell)dataGridViewAccountDetails.Rows[dataGridViewAccountDetails.CurrentRow.Index].Cells["cmbOtherClosedType"];
                        ddl.DataSource = dt;
                        ddl.DisplayMember = "HPD_DESC";
                        ch8.Value = true;
                        ddl.ReadOnly = false;
                        
                        dataGridViewAccountDetails.Rows[index].Cells["checkedDate"].Value = DateTime.Now.ToString("dd/MMM/yyyy");
                        ch6.Selected = true;
                        dataGridViewAccountDetails.Rows[index].Cells["chkYesNo"].Value = "YES";
                        dataGridViewAccountDetails.Rows[index].Cells["OthrClsTypeYesNo"].Value = "YES";
                    }
                    else
                    {
                        ch10.ReadOnly = false;
                        DataGridViewComboBoxCell ddl = new DataGridViewComboBoxCell();
                        ddl = (DataGridViewComboBoxCell)dataGridViewAccountDetails.Rows[dataGridViewAccountDetails.CurrentRow.Index].Cells["cmbOtherClosedType"];
                        ddl.Value = DBNull.Value;
                        ddl.ReadOnly = true;
                        ch8.Value = false;
                        dataGridViewAccountDetails.Rows[index].Cells["checkedDate"].Value = DBNull.Value;
                        ch6.Value =false ;
                        dataGridViewAccountDetails.Rows[index].Cells["chkYesNo"].Value = "NO";
                        dataGridViewAccountDetails.Rows[index].Cells["OthrClsTypeYesNo"].Value = "NO";
                    }
                    
                }
            }
            else if (e.ColumnIndex == dataGridViewAccountDetails.Columns["ReturnYesNo"].Index)
            {
                DataGridViewCheckBoxCell ch10 = new DataGridViewCheckBoxCell();
                ch10 = (DataGridViewCheckBoxCell)dataGridViewAccountDetails.Rows[dataGridViewAccountDetails.CurrentRow.Index].Cells["chkReturnDate"];
                DataGridViewCheckBoxCell ch8 = new DataGridViewCheckBoxCell();
                ch8 = (DataGridViewCheckBoxCell)dataGridViewAccountDetails.Rows[dataGridViewAccountDetails.CurrentRow.Index].Cells["chkOtherClosedtYPE"];
                DataGridViewCheckBoxCell ch13 = new DataGridViewCheckBoxCell();
                ch13 = (DataGridViewCheckBoxCell)dataGridViewAccountDetails.Rows[dataGridViewAccountDetails.CurrentRow.Index].Cells["chkRecevedAgainDate"];
                DataGridViewCheckBoxCell ch6 = new DataGridViewCheckBoxCell();
                ch6 = (DataGridViewCheckBoxCell)dataGridViewAccountDetails.Rows[dataGridViewAccountDetails.CurrentRow.Index].Cells["chkCheckDate"];
                if (ch10.ReadOnly == false)
                {

                    if (dataGridViewAccountDetails.Rows[Convert.ToInt32(e.RowIndex)].Cells["ReturnYesNo"].Value.ToString() == "NO")
                    {
                        string type = "HA";
                        ch8.ReadOnly = true;
                        //ch13.ReadOnly = false;
                        dt = new DataTable();
                        dt = CHNLSVC.Sales.LoadOtherClosedTypes(type);
                        DataGridViewComboBoxCell ddl = new DataGridViewComboBoxCell();
                        ddl = (DataGridViewComboBoxCell)dataGridViewAccountDetails.Rows[dataGridViewAccountDetails.CurrentRow.Index].Cells["Remark"];
                        dataGridViewAccountDetails.Rows[index].Cells["ReturnDate"].Value = DateTime.Now.ToString("dd/MMM/yyyy");
                        // ddl.Items.Insert(0, "--Select--");
                        ddl.DataSource = dt;
                        ddl.DisplayMember = "HPD_DESC";
                        ch10.Value = true;
                        ddl.ReadOnly = false;
                        dataGridViewAccountDetails.Rows[index].Cells["ReturnYesNo"].Value = "YES";
                        dataGridViewAccountDetails.Rows[index].Cells["checkedDate"].Value = DateTime.Now.ToString("dd/MMM/yyyy");
                        dataGridViewAccountDetails.Rows[index].Cells["chkYesNo"].Value = "YES";
                        ch6.Selected = true;
                    }
                    else
                    {
                        //ch13.ReadOnly = true;
                        ch8.ReadOnly = false;
                        DataGridViewComboBoxCell ddl = new DataGridViewComboBoxCell();
                        ddl = (DataGridViewComboBoxCell)dataGridViewAccountDetails.Rows[dataGridViewAccountDetails.CurrentRow.Index].Cells["Remark"];
                        dataGridViewAccountDetails.Rows[index].Cells["ReturnDate"].Value = DBNull.Value;
                        // ddl.Items.Insert(0, "--Select--");
                        ddl.Value = DBNull.Value;
                        ch10.Value = false;
                        ddl.ReadOnly = true;
                        dataGridViewAccountDetails.Rows[index].Cells["ReturnYesNo"].Value = "NO";
                        dataGridViewAccountDetails.Rows[index].Cells["checkedDate"].Value = DBNull.Value;
                        ch6.Selected = false;
                    }
                            
                    
                }
            }
            else if (e.ColumnIndex == dataGridViewAccountDetails.Columns["RecAgainYesNo"].Index)
            {
                DataGridViewCheckBoxCell ch13 = new DataGridViewCheckBoxCell();
                ch13 = (DataGridViewCheckBoxCell)dataGridViewAccountDetails.Rows[dataGridViewAccountDetails.CurrentRow.Index].Cells["chkRecevedAgainDate"];
                if (ch13.ReadOnly == false)
                {

                    if (dataGridViewAccountDetails.Rows[Convert.ToInt32(e.RowIndex)].Cells["RecAgainYesNo"].Value.ToString() == "NO")
                    {
                        dataGridViewAccountDetails.Rows[index].Cells["ReceivedAgainDate"].Value = DateTime.Now.ToString("dd/MMM/yyyy");
                        ch13.Value = true;
                        dataGridViewAccountDetails.Rows[index].Cells["RecAgainYesNo"].Value = "YES";
                    }
                    else
                    {
                        dataGridViewAccountDetails.Rows[index].Cells["ReceivedAgainDate"].Value = DBNull.Value;
                        ch13.Value = false;
                        dataGridViewAccountDetails.Rows[index].Cells["RecAgainYesNo"].Value = "NO";
                    }
                }
            }
        }

        private void dataGridViewAccountDetails_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //for (int row = 0; row < dataGridViewAccountDetails.Rows.Count; row++)
            //{
            //    for (int i = 0; i < dataGridViewAccountDetails.Rows.Count - 1; i++)
            //    {

            //        if (i != e.RowIndex)
            //        {

            //            if (dataGridViewAccountDetails.Rows[row].Cells["AccNo"].Value.ToString() == dataGridViewAccountDetails.Rows[i].Cells["AccNo"].Value.ToString() && i != row)
            //            {
                           
                           
            //            }

            //        }

            //    }
            //}
        }

        private void ValidateRow()
        {
            for (int row = 0; row < dataGridViewAccountDetails.Rows.Count; row++)
            {
                for (int i = 0; i <= dataGridViewAccountDetails.Rows.Count - 1; i++)
                {
                if (dataGridViewAccountDetails.Rows[row].Cells["AccNo"].Value.ToString() == dataGridViewAccountDetails.Rows[i].Cells["AccNo"].Value.ToString() && i > row)
                    {
                        dataGridViewAccountDetails.Rows.RemoveAt(i);
                    }
                }
            }
        }

        private void DeleteRow(int row)
        {
            dataGridViewAccountDetails.Rows.RemoveAt(row);
        }
        

     

        
    }
}

