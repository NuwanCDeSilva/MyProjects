using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Configuration;
using System.Text.RegularExpressions;
using FF.BusinessObjects;


namespace FF.WindowsERPClient.Finance
{
    public partial class FineChargesExeclUpload : Base
    {
        public List<FineCharges> _lstfinecharge = new List<FineCharges>();
        public FineChargesExeclUpload()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to Save ?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No) return;

            #region Save The Excel Data
            string _msg = string.Empty;

            int row_aff = CHNLSVC.Financial.SaveFineChargesExecl(_lstfinecharge, out _msg);

            if (row_aff == 1)
            {
                MessageBox.Show("Successfully Saved", "Fine Charges", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtExeclUpload.Text = "";
                dgvFinesetup.DataSource = null;
                dgvFinesetup.Rows.Clear();
                _lstfinecharge = new List<FineCharges>();
            }
            else if (row_aff == 5)
            {
                MessageBox.Show("Some Records are already Inserted ", "Fine Charges", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (_msg.Contains("EMS.UK_GFCFINECODE"))
                {
                    _msg = "Fine Charge Code Cannot Be Non Numeric";
                }
                MessageBox.Show("Terminate " + _msg, "Fine Charges", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion
        }

        private void btnSearchExclpath_Click(object sender, EventArgs e)
        {
            try
            {
                txtExeclUpload.Text = string.Empty;
                openFileDialog1.InitialDirectory = @"C:\";
                openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.FileName = string.Empty;
                openFileDialog1.ShowDialog();
                string[] _obj = openFileDialog1.FileName.Split('\\');
                txtExeclUpload.Text = openFileDialog1.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //CHNLSVC.CloseChannel();
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                # region validation

                if (string.IsNullOrEmpty(txtExeclUpload.Text))
                {
                    MessageBox.Show("Please select upload file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtExeclUpload.Clear();
                    txtExeclUpload.Focus();
                    return;
                }

                System.IO.FileInfo fileObj = new System.IO.FileInfo(txtExeclUpload.Text);

                if (fileObj.Exists == false)
                {
                    MessageBox.Show("Selected file does not exist at the following path.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }
                #endregion

                #region open excel
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
                    MessageBox.Show("Please Select Only Execl Format", "Fine Charges", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                conStr = String.Format(conStr, txtExeclUpload.Text, 0);
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable _dt = new DataTable();
                cmdExcel.Connection = connExcel;

                //Get the name of First Sheet
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

                #endregion

                string comcode = "";
                string pccode = "";
                string finedate = "";
                string description = "";
                decimal amount = 0;
                int ismailsend = 0;
                string status = "A";
                string finecode = "";
                string userid = "";
                

              
                Int32 _currentRow = 0;

                StringBuilder _errorLst = new StringBuilder();
                if (_dt == null || _dt.Rows.Count <= 0) { MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            
                if (_dt.Rows.Count > 0)
                {
                    if (MessageBox.Show("Are you sure to Upload ?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No) return;

                    // Empty the list.
                    _lstfinecharge = new List<FineCharges>();

                    foreach (DataRow _dr in _dt.Rows)
                    {
                        decimal val = 0;
                        _currentRow = _currentRow + 1;
                        bool chkval = decimal.TryParse(_dr[4].ToString(),out val);

                        if(!chkval)
                        {
                            MessageBox.Show("Please Enter Numeric Value " + _currentRow + " ", "Fine Charges", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        comcode = _dr[0].ToString().Trim();
                        pccode = _dr[1].ToString().Trim();
                        finecode = _dr[2].ToString().Trim();
                        description = _dr[3].ToString().Trim();
                        amount = val;
                        userid = _dr[5].ToString().Trim();
                        finedate = _dr[6].ToString().Trim();                                                                 

                        #region item validation

                        if (string.IsNullOrEmpty(comcode))
                        {
                            MessageBox.Show("Please Enter CompanyCode. " + _currentRow + " ", "Fine Charges", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        if (!string.IsNullOrEmpty(comcode))
                        {
                            bool result = CHNLSVC.General.CheckCompany(comcode);
                            if (!result)
                            {
                                MessageBox.Show("Please Select Valid CompanyCode. " + _currentRow + " ", "Fine Charges", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }                         
                        }

                        if (string.IsNullOrEmpty(pccode))
                        {
                            MessageBox.Show("Please Enter PC Code. " + _currentRow + " ", "Fine Charges", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        if (!string.IsNullOrEmpty(pccode))
                        {
                            bool result = CHNLSVC.Sales.IsvalidPC(comcode, pccode);
                            if (!result)
                            {
                                MessageBox.Show("Please Select Valid PC Code. " + _currentRow + " ", "Fine Charges", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }

                        if (string.IsNullOrEmpty(finedate))
                        {
                            MessageBox.Show("Invalid Enter Date'" + _currentRow + "", "Fine Charges", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        if (string.IsNullOrEmpty(description))
                        {
                            MessageBox.Show("Invalid Enter Description'" + _currentRow + "", "Fine Charges", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        if (string.IsNullOrEmpty(userid))
                        {
                            MessageBox.Show(" User ID Cannot Be Null" + _currentRow + "", "Fine Charges", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        

                        #endregion

                        #region Add Correct Items to List
                        FineCharges objfine = new FineCharges();
                        objfine.comcode = comcode;
                        objfine.pccode = pccode;
                        objfine.finecode = finecode;
                        objfine.finedate = Convert.ToDateTime(finedate);
                        objfine.remarks = description;
                        objfine.amount = amount;
                        objfine.ismailsend = 0;
                        objfine.createby = userid;
                        objfine.ismailsend = ismailsend;
                        objfine.status = status;
                        objfine.rate = "0";
                        objfine.balance = amount;

                        _lstfinecharge.Add(objfine);
                        #endregion


                        if (dgvFinesetup.DataSource != null)
                        {
                            dgvFinesetup.DataSource = null;
                        }

                        dgvFinesetup.AutoGenerateColumns = false;
                        dgvFinesetup.DataSource = _lstfinecharge;
                        dgvFinesetup.Refresh();
                        txtExeclUpload.Text = "";
                    }
                
                }
                  
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                //CHNLSVC.CloseChannel();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvFinesetup.DataSource = null;
            dgvFinesetup.Rows.Clear();
            _lstfinecharge = new List<FineCharges>();
            txtExeclUpload.Text = "";
        }

     
    }
}
