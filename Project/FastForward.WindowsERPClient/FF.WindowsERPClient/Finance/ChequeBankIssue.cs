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
    public partial class ChequeBankIssue : Base
    {
        Deposit_Bank_Pc_wise obj_chq;
        Deposit_Bank_Pc_wise obj_doc_pages;
        List<Deposit_Bank_Pc_wise> _lstCheq;
        List<Deposit_Bank_Pc_wise> _lstdoc_pages;
        string _refno = null;
        int lineNo = 1;
        //int strt_no = 0;
        //int end_no = 0;

        public ChequeBankIssue()
        {
            InitializeComponent();
        }

        private void LoadBankDetails(string company)
        {
            //DataTable dt = CHNLSVC.Sales.GetBankDetais(company);
            //if (dt != null && dt.Rows.Count > 0)
            //{

            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {

            //        cmbDipositAccNo.Items.Add(dt.Rows[i][0] + " || " + dt.Rows[i][1] + " || " + dt.Rows[i][2]);

            //    }
            //    cmbDipositAccNo.Text = "--Select Account No--";



            //}
            //else
            //{
            //    cmbDipositAccNo.DataSource = null;
            //}
        }


        private void AddValueToGrid()
        {
            try
            {
                
                DialogResult dgr = MessageBox.Show("Do you want to Add another records?", "Cheque Bank", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dgr == System.Windows.Forms.DialogResult.Yes)
                {

                    string bankacc = txtAccountNo.Text;
                    //string[] arr = _mids.Split(new string[] { "||" }, StringSplitOptions.None);
                    //string _acc = arr[0].Trim();

                    foreach (DataGridViewRow dgvr in dgvChqBankDets.Rows)
                    {
                        if (bankacc == Convert.ToString(dgvr.Cells["clmBankAccNo"].Value) && Convert.ToString(txtBookNo.Text.Trim()) == Convert.ToString(dgvr.Cells["clmBookNo"].Value))
                        {

                            MessageBox.Show("Thease values are Already Added.", "MID", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtAccountNo.Focus();
                            return;
                        }
                    }



                    dgvChqBankDets.Rows.Add();
                    dgvChqBankDets["clmlineno", dgvChqBankDets.Rows.Count - 1].Value = lineNo;
                    dgvChqBankDets["clmBankAccNo", dgvChqBankDets.Rows.Count - 1].Value = bankacc;
                    dgvChqBankDets["clmBookNo", dgvChqBankDets.Rows.Count - 1].Value = txtBookNo.Text.Trim();
                    dgvChqBankDets["clmStartingNo", dgvChqBankDets.Rows.Count - 1].Value = Convert.ToInt32(txtStartingNo.Text.Trim());
                    //strt_no = Convert.ToInt32(txtStartingNo.Text.Trim());
                    dgvChqBankDets["clmpages", dgvChqBankDets.Rows.Count - 1].Value = Convert.ToInt32(txtnumofpage.Text.Trim());
                    dgvChqBankDets["clmEndingNo", dgvChqBankDets.Rows.Count - 1].Value = txtEndingNo.Text;
                    //end_no = Convert.ToInt32(txtEndingNo.Text.Trim());
                    lineNo++;
                    //btnSave.Focus();
                    txtAccountNo.Focus();

                }
                else
                {
                    string bankaccx = txtAccountNo.Text;
                    foreach (DataGridViewRow dgvr in dgvChqBankDets.Rows)
                    {
                        if (bankaccx == Convert.ToString(dgvr.Cells["clmBankAccNo"].Value) && Convert.ToString(txtBookNo.Text.Trim()) == Convert.ToString(dgvr.Cells["clmBookNo"].Value))
                        {

                            MessageBox.Show("Thease values are Already Added.", "MID", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtAccountNo.Focus();
                            return;
                        }
                    }



                    dgvChqBankDets.Rows.Add();
                    dgvChqBankDets["clmlineno", dgvChqBankDets.Rows.Count - 1].Value = lineNo;
                    dgvChqBankDets["clmBankAccNo", dgvChqBankDets.Rows.Count - 1].Value = bankaccx;
                    dgvChqBankDets["clmBookNo", dgvChqBankDets.Rows.Count - 1].Value = txtBookNo.Text.Trim();
                    dgvChqBankDets["clmStartingNo", dgvChqBankDets.Rows.Count - 1].Value = Convert.ToInt32(txtStartingNo.Text.Trim());
                    //strt_no = Convert.ToInt32(txtStartingNo.Text.Trim());
                    dgvChqBankDets["clmpages", dgvChqBankDets.Rows.Count - 1].Value = Convert.ToInt32(txtnumofpage.Text.Trim());
                    dgvChqBankDets["clmEndingNo", dgvChqBankDets.Rows.Count - 1].Value = txtEndingNo.Text;
                    //end_no = Convert.ToInt32(txtEndingNo.Text.Trim());
                    lineNo++;
                    btnSave.Focus();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CHNLSVC.CloseChannel();
            }
            finally
            {

            }
        }



        private void btnAddToGrid_Click(object sender, EventArgs e)
        {
            if (txtAccountNo.Text == "")
            {
                MessageBox.Show("Please select Bank Account No", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccountNo.Focus();
                return;
            }
            if (txtBookNo.Text.Trim() == "")
            {
                MessageBox.Show("Please enter Book Number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBookNo.Focus();
                return;
            }

            if (txtStartingNo.Text.Trim() == "")
            {
                MessageBox.Show("Please enter starting Number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtStartingNo.Focus();
                return;
            }
            if (txtnumofpage.Text.Trim() == "")
            {
                MessageBox.Show("Please enter number of pages", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtnumofpage.Focus();
                return;
            }
            if (txtEndingNo.Text.Trim() == "")
            {
                MessageBox.Show("Please enter ending number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtnumofpage.Focus();
                return;
            }

            AddValueToGrid();
            txtBookNo.Text = "";
            txtStartingNo.Text = "";
            txtnumofpage.Text = "";
            txtEndingNo.Text = "";
            txtAccountNo.Text = "";
           

        }

        private void cmbDipositAccNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)Keys.None;
        }

        private void ChequeBankIssue_Load(object sender, EventArgs e)
        {
            //LoadBankDetails(BaseCls.GlbUserComCode);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBookNo.Text = "";
            txtStartingNo.Text = "";
            txtnumofpage.Text = "";
            txtEndingNo.Text = "";
            txtAccountNo.Text = "";
            dgvChqBankDets.Rows.Clear();
            lineNo = 1;
            txtAccountNo.Focus();
        }

        private void txtStartingNo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtStartingNo.Text.Trim()))
            {
                txtnumofpage.Text = "";
                txtEndingNo.Text = "";
                return;
            }

            if (string.IsNullOrEmpty(txtBookNo.Text.Trim()))
            {
                MessageBox.Show("Please enter book number.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBookNo.Text = "";
                txtBookNo.Focus();
                return;
            }

            if (!txtStartingNo.Text.All(c => Char.IsNumber(c)))
            {
                MessageBox.Show("Please enter only positive numbers", "MID", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtStartingNo.Text = "";
                txtStartingNo.Focus();
                return;
            }

            if(txtStartingNo.Text.Trim() != "" && txtnumofpage.Text.Trim() != "")
            {
                //if (Convert.ToInt32(txtStartingNo.Text.Trim()) > Convert.ToInt32(txtnumofpage.Text.Trim()))
                //{
                //    MessageBox.Show("Starting No should be less than to number of pages ", "MID", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    txtStartingNo.Text = "";
                //    txtStartingNo.Focus();
                //    return;
                //}
                //txtEndingNo.Text = (Convert.ToInt32(txtnumofpage.Text) + Convert.ToInt32(txtStartingNo.Text)).ToString();
                int endingNo = (Convert.ToInt32(txtnumofpage.Text) + Convert.ToInt32(txtStartingNo.Text));
                txtEndingNo.Text = (endingNo - 1).ToString();
            }


        }

        private void txtnumofpage_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtnumofpage.Text.Trim())) return;

            if (string.IsNullOrEmpty(txtStartingNo.Text.Trim()))
            {
                MessageBox.Show("Please enter starting number.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtnumofpage.Text = "";
                return;
            }

            if (!txtnumofpage.Text.All(c => Char.IsNumber(c)))
            {
                MessageBox.Show("Please enter only positive numbers", "MID", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtnumofpage.Text = "";
                txtnumofpage.Focus();
                return;
            }
           
            //txtEndingNo.Text = (Convert.ToInt32(txtnumofpage.Text) + Convert.ToInt32(txtStartingNo.Text)).ToString();
            int endingNo = (Convert.ToInt32(txtnumofpage.Text) + Convert.ToInt32(txtStartingNo.Text));
            txtEndingNo.Text = (endingNo - 1).ToString();
        }

        private void txtnumofpage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtnumofpage.Text.Trim()))
                {
                    txtEndingNo.Text = "";
                    txtnumofpage.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtStartingNo.Text.Trim()))
                {
                    MessageBox.Show("Please enter starting number.", "Price Book", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtnumofpage.Text = "";
                    txtEndingNo.Text = "";
                    txtStartingNo.Focus();
                    return;
                }

                if (!txtnumofpage.Text.All(c => Char.IsNumber(c)))
                {
                    MessageBox.Show("Please enter only positive numbers", "MID", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtnumofpage.Text = "";
                    txtnumofpage.Focus();
                    return;
                }
                txtEndingNo.Focus();
                //txtEndingNo.Text = (Convert.ToInt32(txtnumofpage.Text) + Convert.ToInt32(txtStartingNo.Text)).ToString();
                int endingNo = (Convert.ToInt32(txtnumofpage.Text) + Convert.ToInt32(txtStartingNo.Text));
                txtEndingNo.Text = (endingNo - 1).ToString();

            }
        }

        private void txtStartingNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtStartingNo.Text.Trim()))
                {
                    txtnumofpage.Text = "";
                    txtEndingNo.Text = "";
                    txtStartingNo.Focus();
                    return;
                }

                if (!txtStartingNo.Text.All(c => Char.IsNumber(c)))
                {
                    MessageBox.Show("Please enter only positive numbers", "MID", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtStartingNo.Text = "";
                    txtStartingNo.Focus();
                    return;
                }

                if (txtStartingNo.Text.Trim() != "" && txtnumofpage.Text.Trim() != "")
                {
                    //txtEndingNo.Text = (Convert.ToInt32(txtnumofpage.Text) + Convert.ToInt32(txtStartingNo.Text)).ToString();
                    int endingNo = (Convert.ToInt32(txtnumofpage.Text) + Convert.ToInt32(txtStartingNo.Text));
                    txtEndingNo.Text = (endingNo - 1).ToString();
                }

                txtnumofpage.Focus();

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private List<Deposit_Bank_Pc_wise> fillToCheqDets()
        {
            _lstCheq = new List<Deposit_Bank_Pc_wise>();

              //get ref no
                MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                _receiptAuto.Aut_cate_cd = "HO";
                _receiptAuto.Aut_cate_tp = "HO";
                _receiptAuto.Aut_start_char = "CHQ";
                _receiptAuto.Aut_direction = 0;
                _receiptAuto.Aut_modify_dt = null;
                _receiptAuto.Aut_moduleid = "CHQ";
                _receiptAuto.Aut_number = 0;
                _receiptAuto.Aut_year = null;//2012;                      

                _refno = CHNLSVC.Sales.GetRecieptNo(_receiptAuto);
                int line = 1;
          
                foreach (DataGridViewRow dgvr in dgvChqBankDets.Rows)
                {

                    obj_chq = new Deposit_Bank_Pc_wise();
                    obj_chq.Ref_lnk = _refno;
                    obj_chq.Line_no = line;
                    obj_chq.Mid_no = "PRN-CHQ";
                    obj_chq.Prifix = "CHQ";
                    obj_chq.SunAccNo = dgvr.Cells["clmBookNo"].Value.ToString();
                    obj_chq.Start_no = Convert.ToInt32(dgvr.Cells["clmStartingNo"].Value);
                    obj_chq.Ending_no = Convert.ToInt32(dgvr.Cells["clmEndingNo"].Value);
                    obj_chq.No_of_pages = Convert.ToInt32(dgvr.Cells["clmpages"].Value);
                    obj_chq.Create_by = BaseCls.GlbUserID;
                    obj_chq.Stus = "F";
                    obj_chq.Curnt = Convert.ToInt32(dgvr.Cells["clmStartingNo"].Value);
                    obj_chq.BankCode = dgvr.Cells["clmBankAccNo"].Value.ToString();
                    line++;

                    _lstCheq.Add(obj_chq);
                }


                if (_lstCheq.Count <= 0)
                {
                    MessageBox.Show("Please add cheque bank details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            return _lstCheq;
        }

        private List<Deposit_Bank_Pc_wise> fillToDocPages()
        {
            _lstdoc_pages = new List<Deposit_Bank_Pc_wise>();
            int line = 1;
            foreach (DataGridViewRow dgvr in dgvChqBankDets.Rows)
            {
                int strt_no = Convert.ToInt32(dgvr.Cells["clmStartingNo"].Value);
                int end_no = Convert.ToInt32(dgvr.Cells["clmEndingNo"].Value);
               

                for (int i = strt_no; i <= end_no; i++)
                {
                    obj_doc_pages = new Deposit_Bank_Pc_wise();
                    obj_doc_pages.Company = _refno;
                    obj_doc_pages.Pun_tp = line;
                    obj_doc_pages.Profit_center = "Cheque";
                    obj_doc_pages.Price_book = "PRN-CHQ";
                    obj_doc_pages.Remark = "CHQ";
                    obj_doc_pages.Page_num = i;

                    obj_doc_pages.Promo_p_book = "P";
                    obj_doc_pages.Createby = BaseCls.GlbUserID;
                    _lstdoc_pages.Add(obj_doc_pages);
                }
                line++;
            }
            

            return _lstdoc_pages;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvChqBankDets.Rows.Count > 0)
            {
                _lstCheq = new List<Deposit_Bank_Pc_wise>();
               // _lstdoc_pages = new List<Deposit_Bank_Pc_wise>();

                _lstCheq = fillToCheqDets();

                if (_lstCheq.Count > 0)
                {
                    if (MessageBox.Show("Are you sure ?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    string _error = "";
                    int result = CHNLSVC.Sales.SaveToIssueChqBank(_lstCheq, out _error);

                    int resultdoc = CHNLSVC.Sales.SaveToDocPages(fillToDocPages(), out _error);

                    if (result == -1 && resultdoc == -1)
                    {
                        MessageBox.Show("Error occurred while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Records inserted Successfully", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        btnClear_Click(null, null);
                        txtAccountNo.Focus();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please add cheque bank details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnAddToGrid.Focus();
            }
        }

        private void dgvChqBankDets_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)
            {
               dgvChqBankDets.Rows.RemoveAt(e.RowIndex);
                  
            }
        }

        private void cmbDipositAccNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBookNo.Focus();
        }

        private void txtBookNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtStartingNo.Focus();

            }
        }

        private void txtEndingNo_KeyDown(object sender, KeyEventArgs e)
        {
            btnAddToGrid.Focus();
        }

        private void txtBookNo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBookNo.Text.Trim())) return;

            if (string.IsNullOrEmpty(txtAccountNo.Text.Trim()))
            {
                MessageBox.Show("Please enter bank account number.", "Cheque Bank", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtnumofpage.Text = "";
                return;
            }

            if (!txtBookNo.Text.All(c => Char.IsNumber(c)))
            {
                MessageBox.Show("Please enter only numbers", "MID", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBookNo.Text = "";
                txtBookNo.Focus();
                return;
            }


        }

        private void dgvChqBankDets_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvChqBankDets.Rows.Count > 0)
            {
                if (e.ColumnIndex == 6)
                {
                    DialogResult dgr = MessageBox.Show("Do you want to delete this record?", "Item Condition Setup", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dgr == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    dgvChqBankDets.Rows.RemoveAt(e.RowIndex);
                   // lineNo = lineNo - 1;
                  
                }
            }


        }


        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Employee_Executive:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "SEX" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.VoucherNo:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Department:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.BankBranch:
                    {
                        //paramsText.Append(txtBankCode.Tag.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PurchaseOrder:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.JournalEntryAccount:
                    {
                       // paramsText.Append(BaseCls.GlbUserComCode + seperator + Convert.ToDateTime(dtpDate.Value.Date).Date.ToString("d") + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }


        private void ImgBtnAcc_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
            DataTable _result = CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = false;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtAccountNo;
            _CommonSearch.ShowDialog();
            txtAccountNo.Select();
        }

        private void txtAccountNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAccountNo.Text))
            {
                DataTable _result = new DataTable();
                _result = CHNLSVC.Financial.GET_ACC_DETAILS(BaseCls.GlbUserComCode, txtAccountNo.Text);
                if (_result == null || _result.Rows.Count == 0)
                {
                    MessageBox.Show("Please select a correct account no", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAccountNo.Text = "";
                    txtAccountNo.Focus();
                }
                else
                {
                   // txtAccNoDesc.Text = _result.Rows[0]["Description"].ToString();

                }
            }
        }

        private void txtAccountNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtAccountNo_DoubleClick(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtBookNo.Focus();
            }
        }

        private void txtAccountNo_DoubleClick(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BankAccount);
            DataTable _result = CHNLSVC.CommonSearch.GetBankAccounts(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = false;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtAccountNo;
            _CommonSearch.ShowDialog();
            txtAccountNo.Select();
        }
    }
}
