using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.WindowsERPClient.Reports.Inventory;
using System.IO;
using System.Data.OleDb;
using System.Configuration;

namespace FF.WindowsERPClient.Sales
{
    public partial class InvoiceCustomerChang : Base
    {
        public InvoiceCustomerChang()
        { 
            InitializeComponent();
        }

        private void btnGetInvoices_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = CHNLSVC.Sales.GetInvoicesToChangeCust(BaseCls.GlbUserComCode, dtpFromDate.Value.Date, dtpToDate.Value.Date, BaseCls.GlbUserDefProf, txtFindCustomer.Text, txtFindInvoiceNo.Text, 0);
                if (dt.Rows.Count > 0)
                {
                    dvPendingInvoices.AutoGenerateColumns = false;
                    dvPendingInvoices.DataSource = dt;
                }
                else
                {
                    dt = null;
                    dvPendingInvoices.AutoGenerateColumns = false;
                    dvPendingInvoices.DataSource = dt;
                    MessageBox.Show("No  invoices found!", "Forward Sales", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Customer_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtFindCustomer;
                _CommonSearch.ShowDialog();
                txtFindCustomer.Select();
                CHNLSVC.CloseAllChannels();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearch_Invoice_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoice);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtFindInvoiceNo;
                _CommonSearch.ShowDialog();
                txtFindInvoiceNo.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #region Common Searching Area

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.SalesOrder:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "SO" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceType:
        
                case CommonUIDefiniton.SearchUserControlType.InvSalesInvoice:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }
             
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
               

        
                default:
                    break;
            }

            return paramsText.ToString();
        }

        #endregion Common Searching Area

        private void dvPendingInvoices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dvPendingInvoices.ColumnCount > 0 && e.RowIndex >= 0)
            {   
                txtInv.Text = dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_INV_NO"].Value.ToString();
                txtCustCode.Text = dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_CUS_CD"].Value.ToString();
                txtCustName.Text = dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_CUS_NAME"].Value.ToString();
                txtCustAddress1.Text = dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_CUS_ADD1"].Value.ToString();
                txtCustAddress2.Text = dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_CUS_ADD2"].Value.ToString();
                cmbInvType.Text = dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_INV_TP"].Value.ToString();
            }
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_CommonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCustCode;
                _CommonSearch.ShowDialog();
                txtCustCode.Select();
                CHNLSVC.CloseAllChannels();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSavePromo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to save ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }
            if (string.IsNullOrEmpty(txtInv.Text))
            {
                MessageBox.Show("Select Invoice", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(string.IsNullOrEmpty(   txtCustCode.Text) )
            {
                MessageBox.Show("Select Customer", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            int _effect = CHNLSVC.Sales.Update_CustomerName(txtInv.Text , txtCustCode.Text , txtCustName.Text  ,txtCustAddress1.Text , txtCustAddress2.Text, BaseCls.GlbUserComCode,BaseCls.GlbUserID);
            if (_effect > 0)
                
             MessageBox.Show( "Successfully updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 txtCustCode.Text ="";
                 txtCustName.Text ="";
                 txtCustAddress1.Text ="";
                 txtCustAddress2.Text ="";
                  txtInv.Text = "";
                  cmbInvType.Text = "";
                  DataTable dt = CHNLSVC.Sales.GetInvoicesToChangeCust(BaseCls.GlbUserComCode, dtpFromDate.Value.Date, dtpToDate.Value.Date, BaseCls.GlbUserDefLoca, txtFindCustomer.Text, txtFindInvoiceNo.Text, 0);
                  if (dt.Rows.Count > 0)
                  {
                      dvPendingInvoices.AutoGenerateColumns = false;
                      dvPendingInvoices.DataSource = dt;
                  }
                  else
                  {
                      dt = null;
                      dvPendingInvoices.AutoGenerateColumns = false;
                      dvPendingInvoices.DataSource = dt;
                  //    MessageBox.Show("No  invoices found!", "Forward Sales", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                  }

            if (_effect <= 0)
                MessageBox.Show("Process terminated!. ", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dvPendingInvoices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        
        }

        private void txtCustName_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void btnClearPromo_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            InvoiceCustomerChang _frm = new InvoiceCustomerChang();
            _frm.MdiParent = this.MdiParent;
            _frm.Location = this.Location;
            _frm.GlbModuleName = this.GlbModuleName;//Added by Nadeeka 13-07-2015(Only this line)
            _frm.Show();

            this.Close();
        }

        private void btncustcre_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                General.CustomerCreation _CusCre = new General.CustomerCreation();
                _CusCre._isFromOther = true;
                _CusCre.obj_TragetTextBox = txtCustCode;
                this.Cursor = Cursors.Default;
                _CusCre.ShowDialog();
                txtCustCode.Select();
                
            }
            catch (Exception ex)
            { txtCustCode.Clear(); this.Cursor = Cursors.Default;   }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtCustCode_Leave(object sender, EventArgs e)
        {  MasterBusinessEntity _masterBusinessCompany = null;
            if (string.IsNullOrEmpty(txtCustCode.Text)) return;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (cmbInvType.Text.Trim() == "CRED" && txtCustCode.Text.Trim() == "CASH")
                {
                    this.Cursor = Cursors.Default;

                    {
                        txtCustCode.Text = "";
                        MessageBox.Show("You can not select customer as CASH, because your invoice type is " + cmbInvType.Text, "Credit Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                   
                    txtCustCode.Focus();
                    return;
                }
                _masterBusinessCompany = new MasterBusinessEntity();
                if (!string.IsNullOrEmpty(txtCustCode.Text))
                    //_masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustCode.Text, string.Empty, string.Empty, "C");
                    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCustCode.Text, null, null, null, null, BaseCls.GlbUserComCode);

                if (_masterBusinessCompany.Mbe_cd != null)
                {
                    if (_masterBusinessCompany.Mbe_act == false)
                    {
                        this.Cursor = Cursors.Default;

                        {
                            txtCustCode.Text = "";
                            MessageBox.Show("This customer already inactive. Please contact accounts dept.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                        txtCustCode.Focus();
                        return;
                    }
                    else
                    {   txtCustName.Text =_masterBusinessCompany.Mbe_name;
                        txtCustAddress1.Text =_masterBusinessCompany.Mbe_add1;
                        txtCustAddress2.Text = _masterBusinessCompany.Mbe_add2;
                    }

                    DataTable _table = CHNLSVC.Sales.GetCustomerAllowInvoiceType(BaseCls.GlbUserComCode, txtCustCode.Text.Trim());
                    if (_table != null && _table.Rows.Count > 0)
                    {
                        var _isAvailable = _table.AsEnumerable().Where(x => x.Field<string>("mbsa_sa_tp") == cmbInvType.Text.Trim()).ToList();
                        if (_isAvailable == null || _isAvailable.Count <= 0)
                        {
                            this.Cursor = Cursors.Default;

                            {
                                txtCustCode.Text = "";
                                MessageBox.Show("Customer is not allow for enter transaction under selected invoice type.", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                           
                            txtCustCode.Focus();
                            return;
                        }
                    }
                    else if (cmbInvType.Text != "CS")
                    {
                        this.Cursor = Cursors.Default;

                        {
                            txtCustCode.Text = "";
                            MessageBox.Show("Selected Customer is not allow for enter transaction under selected invoice type.", "Invoice Type", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                       
                        txtCustCode.Focus();
                        return;
                    }

                    if (_masterBusinessCompany.Mbe_cd == "CASH")
                    {
                        txtCustCode.Text = _masterBusinessCompany.Mbe_cd;
                      
                        //ClearCustomer(false);
                    }
                    else
                    {
                   
                    }

          
                }
                else
                {
                    txtCustCode.Text = "";
                            MessageBox.Show("Invalid customer.", "Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);  

                        txtCustCode.Focus();
                        return;
                }
               
            }
            catch (Exception ex)
            {   }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void cmbInvType_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFindInvoiceNo_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_Invoice_Click(null, null);
        }

        private void txtFindCustomer_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_Customer_Click(null, null);
        }

        private void txtFindCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F2)
            {
                btnSearch_Customer_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                btnGetInvoices_Click(null, null);
            }
        }

        private void txtFindInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearch_Invoice_Click(null, null);
            }
            else if(e.KeyCode == Keys.Enter)
            {
                btnGetInvoices_Click(null, null);
            }
        }

        private void txtFindCustomer_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFindInvoiceNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFindCustomer_Leave(object sender, EventArgs e)
        {

        }
        
    }
}
