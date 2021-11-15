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

namespace FF.WindowsERPClient.Enquiries.Finance
{
    public partial class PaymodeInquiry : Base
    {
        //sp_get_enqiry_paytypes   =NEW
        //pkg_search.sp_search_DocsByRefNum  =UPDATE       
        //get_PaymodeBased_RECIEPTS =UPDATE
        //get_GET_PaymodeDet  =UPDATE
        //-------------------------------------
        //get_PaymodeBased_RECIEPTS  =UPDATE
        //---------------------------------
        //sp_GetRetCheque_detWithPays =NEW
        //get_PaymodeBased_INVOICES =UPDATE

        public PaymodeInquiry()
        {
            InitializeComponent();

            List<PaymentTypeRef> paymodes = CHNLSVC.Sales.Get_enqiry_paytypes(false);
            //List<PaymentTypeRef> paymodes = new List<PaymentTypeRef>();
            //List<PaymentTypeRef> paymodes_1 = CHNLSVC.Sales.GetAllPaymentType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "ADVAN");
            //List<PaymentTypeRef> paymodes_2 = CHNLSVC.Sales.GetAllPaymentType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "CHEQUE");
            //List<PaymentTypeRef> paymodes_3 = CHNLSVC.Sales.GetAllPaymentType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "CASH");
            //List<PaymentTypeRef> paymodes_4 = CHNLSVC.Sales.GetAllPaymentType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "CRCD");
            //List<PaymentTypeRef> paymodes_5 = CHNLSVC.Sales.GetAllPaymentType(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, string.Empty);

            //paymodes.AddRange(paymodes_1); 
            //paymodes.AddRange(paymodes_2);
            //paymodes.AddRange(paymodes_3);
            //paymodes.AddRange(paymodes_4);
            //paymodes.AddRange(paymodes_5);

            if (paymodes != null)
            {
                //pt.Sapt_desc, pt.Sapt_cd
                ddlPayMode.DataSource = paymodes;
                ddlPayMode.DisplayMember = "Sapt_desc";
                ddlPayMode.ValueMember = "Sapt_cd";
                ddlPayMode.SelectedIndex = -1;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            PaymodeInquiry formnew = new PaymodeInquiry();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }
        private void clear()
        {
            grvPaymentDocDet.Rows.Clear();
            grvReceptDet.Rows.Clear();
            grvInvoiceDet.Rows.Clear();
            grvPopUpSelect.Rows.Clear();
            grvReturnChequeDet.Rows.Clear();
            txtDocNo.Text = "";
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.DocNo:
                    {
                        string _pc = null;
                        if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "PMENQ") == false)
                        {
                            _pc = BaseCls.GlbUserDefProf;
                        }
                        paramsText.Append(ddlPayMode.SelectedValue + seperator + BaseCls.GlbUserComCode + seperator + _pc);
                    
                 
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }
        private void ImgBtnDocSearch_Click(object sender, EventArgs e)
        {
            if (ddlPayMode.SelectedIndex == -1)
            {
                return;
            }
            txtDocNo.Text = "";
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
            DataTable _result = CHNLSVC.CommonSearch.Get_DocNum_ByRefNo(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtDocNo;
             
            _CommonSearch.txtSearchbyword.Text ="%" + txtDocNo.Text;
            _CommonSearch.ShowDialog();
            txtDocNo.Focus();

            //MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
            //DataTable dataSource = CHNLSVC.CommonSearch.Get_DocNum_ByRefNo(MasterCommonSearchUCtrl.SearchParams, null, null);

            //MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            //MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            //MasterCommonSearchUCtrl.ReturnResultControl = txtDocNo.ClientID;
            //MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        //-------------------------------------------------------------------------------------
        private void btnGetDocDet_Click(object sender, EventArgs e)
        {
            refreshGrids();
            lblDoc.Visible = false;
            if (ddlPayMode.SelectedIndex == -1)
            {
                MessageBox.Show("Select the paymode first!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtDocNo.Text.Trim() == "")
            {
                MessageBox.Show("Select the document number!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDocNo.Focus();
                return;
            }
            this.Cursor = Cursors.WaitCursor;

            string payType = ddlPayMode.SelectedValue.ToString();
            DataTable dt = new DataTable();
            DataTable remDt = new DataTable();

            try
            {
                dt = CHNLSVC.Sales.GetPaymodeDetail(BaseCls.GlbUserComCode, "", payType, txtDocNo.Text.Trim(), string.Empty, string.Empty);
                remDt = CHNLSVC.Financial.GetChequesFromRemDet(BaseCls.GlbUserComCode, txtDocNo.Text.Trim());

                if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "PMENQ") == false)
                {
                    if (dt.Rows.Count > 0)
                    {
                        var result = from r in dt.AsEnumerable()
                                     where r.Field<string>("Profit Center") == BaseCls.GlbUserDefProf
                                     select r;
                       

                        if (result.Any())
                        {
                            DataTable dtResult = result.CopyToDataTable();
                            dt = dtResult;
                        }
                        else
                        {
                            dt = null;
                        }
                    }
                }

                if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "PMENQ") == false)
                {
                    if (remDt.Rows.Count > 0)
                    {
                        var result = from r in remDt.AsEnumerable()
                                     where r.Field<string>("REM_PC") == BaseCls.GlbUserDefProf
                                     select r;
                       

                        if (result.Any())
                        {
                            DataTable dtResult = result.CopyToDataTable();
                            remDt = dtResult;
                        }
                        else
                        {
                            remDt = null;
                        }
                    }
                }



                if (dt.Rows.Count > 0 || remDt.Rows.Count > 0)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string PC = dt.Rows[0]["Profit Center"].ToString();
                        if (PC != BaseCls.GlbUserDefProf)
                        {
                            //PMENQ
                            if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "PMENQ") == false)
                            {
                                //MessageBox.Show("Cannot view different profit center documents!\n( Advice: Reqired permission code :PMENQ)");
                                //this.Cursor = Cursors.Default;
                                //return;
                            }
                            else
                            {

                            }
                        }
                    }
                    else
                    {
                        if (dt.Rows.Count == 0 && remDt.Rows.Count == 0)
                        {
                            MessageBox.Show("No data found with this document number.", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }



                }
                else
                {
                    DataTable _dtBankRec = CHNLSVC.Financial.GetBankStmntDetails(payType, txtDocNo.Text.Trim());

                    if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "PMENQ") == false)
                    {
                        if (_dtBankRec.Rows.Count > 0)
                        {
                            var result = from r in _dtBankRec.AsEnumerable()
                                         where r.Field<string>("bstd_pc") == BaseCls.GlbUserDefProf
                                         select r;
                          

                            if (result.Any())
                            {
                                DataTable dtResult = result.CopyToDataTable();
                                _dtBankRec = dtResult;
                            }
                            else
                            {
                                _dtBankRec = null;
                            }
                        }
                    }
                    if (_dtBankRec.Rows.Count > 0)
                    {
                        grvPaymentDocDet.DataSource = _dtBankRec;
                        lblDoc.Visible = true;
                        return;
                    }
                    //else
                    //{
                    //    MessageBox.Show("No data found with this document number.", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}
                }

                //-----------------------------------------------------------------------
                grvPopUpSelect.DataSource = dt;
                dgvRemChq.DataSource = remDt;
                Decimal grandTotal = 0;
                if (dt != null)
                {
                    //if (dt.Columns["Profit Center"].ToString()!=BaseCls.GlbUserDefProf)
                    //{
                    //    //PMENQ
                    //    if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "PMENQ") == false)
                    //    {
                    //        MessageBox.Show("Cannot view different profit center documents!");
                    //        return;
                    //    }

                    //}
                    foreach (DataRow dr in dt.Rows)
                    {
                        grandTotal = grandTotal + Convert.ToDecimal(dr["Total"].ToString());
                    }
                }
                txtGrandTot.Text = string.Format("{0:n2}", grandTotal);
                // ModalPopupExtItem.Show();
            }
            catch (Exception ex)
            {

            }

            if (dt != null)
            {
                if (dt.Rows.Count > 1)
                {
                    //TODO:
                    // ModalPopupExtItem.Show();//Show ModalPopUp to select one record.
                    MessageBox.Show("Select one from the list!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    panel_advSearch.Visible = true;
                }
                else if (dt.Rows.Count == 1)
                {
                    panel_advSearch.Visible = false;

                    grvPaymentDocDet.DataSource = dt;
                    //grvPaymentDocDet.DataBind();
                    //-------------------------------
                    string docType = ddlPayMode.SelectedValue.ToString();
                    string DocNo = txtDocNo.Text.Trim();

                    string PC = string.Empty;
                    string date = string.Empty;
                    string bankCd = string.Empty;
                    string cardType = string.Empty;

                    //return;
                    if (grvPaymentDocDet.Rows.Count > 0)
                    {
                        DataGridViewRow row = grvPaymentDocDet.Rows[0];
                        if (docType == "CHEQUE" || docType == "CRCD")
                        {
                            PC = row.Cells[0].Value.ToString();//.Trim();
                            date = row.Cells[1].Value.ToString();
                            bankCd = row.Cells[2].Value.ToString();
                            if (docType == "CRCD" || docType =="DEBT")
                            {
                                cardType = row.Cells[3].Value.ToString();
                            }

                            if (docType == "CHEQUE")
                            {
                                DataTable dt_retCheques = CHNLSVC.Sales.GetReturnCheque_detWithPayments(DocNo, bankCd);

                                if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "PMENQ") == false)
                                { if (dt_retCheques.Rows.Count >0)
                                {
                                    var result = from r in dt_retCheques.AsEnumerable()
                                                 where r.Field<string>("sar_profit_center_cd") == BaseCls.GlbUserDefProf
                                                 select r;
                                   

                                    if (result.Any())
                                    {
                                        DataTable dtResult = result.CopyToDataTable();
                                        dt_retCheques = dtResult;
                                    }
                                    else
                                    {
                                        dt_retCheques = null;
                                    }
                                }
                                }


                                grvReturnChequeDet.DataSource = dt_retCheques;
                                // grvReturnChequeDet.DataBind();
                                // Panel_RtnChq.Visible = true; TODO:
                            }
                        }
                    }
                    DataTable invList = CHNLSVC.Sales.getInvoicesBased_onPayType(BaseCls.GlbUserComCode, DocNo, docType, bankCd, cardType);



                    if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "PMENQ") == false)
                    {
                        if (invList.Rows.Count > 0)
                        {
                            var result = from r in invList.AsEnumerable()
                                         where r.Field<string>("sah_pc") == BaseCls.GlbUserDefProf
                                         select r;

                            if (result.Any())
                            {
                                DataTable dtResult = result.CopyToDataTable();
                                invList = dtResult;
                            }
                            else
                            {
                                invList = null;
                            }
                        }
                    }

                    
                    grvInvoiceDet.DataSource = invList;
                    // grvInvoiceDet.DataBind();                                     

                    DataTable ReceiptList = CHNLSVC.Sales.get_Receipts_BasedonPayType(BaseCls.GlbUserComCode, DocNo, docType, bankCd, cardType);


                    if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "PMENQ") == false)
                    {
                        if (ReceiptList.Rows.Count > 0)
                        {
                            var result = from r in ReceiptList.AsEnumerable()
                                         where r.Field<string>("sar_profit_center_cd") == BaseCls.GlbUserDefProf
                                         select r;
                          

                            if (result.Any())
                            {
                                DataTable dtResult = result.CopyToDataTable();
                                ReceiptList = dtResult;
                            }
                            else
                            {
                                ReceiptList = null;
                            }
                        }
                    }


                    grvReceptDet.DataSource = ReceiptList;
                    foreach (DataGridViewRow dgvr in grvReceptDet.Rows)
                    {
                        if (dgvr.Cells["sar_is_mgr_iss"].Value.ToString() == "1")
                        {
                            dgvr.Cells["issued_by"].Value = "Manager";
                        }
                        else
                        {
                            dgvr.Cells["issued_by"].Value = "Customer";
                        }

                        string receiptTp = dgvr.Cells["SAR_RECEIPT_TYPE"].Value.ToString();
                        DataTable recTpTB = CHNLSVC.Sales.GetReceiptType(receiptTp);
                        if (recTpTB != null)
                        {
                            if (recTpTB.Rows.Count > 0)
                            {
                                string CODE = recTpTB.Rows[0]["MSRT_CD"].ToString();
                                string DESC = recTpTB.Rows[0]["MSRT_DESC"].ToString();
                                if (CODE == dgvr.Cells["SAR_RECEIPT_TYPE"].Value.ToString())
                                {
                                    //receiptTpDesc
                                    dgvr.Cells["receiptTpDesc"].Value = DESC;
                                }
                            }
                        }
                    }
                }
            }
            arrangeAlignments(grvPaymentDocDet);//grvReceptDet
            arrangeAlignments(grvReceptDet);//grvInvoiceDet
            arrangeAlignments(grvInvoiceDet);//grvPopUpSelect
            arrangeAlignments(grvPopUpSelect);//grvReturnChequeDet
            arrangeAlignments(grvReturnChequeDet);
            if (grvReturnChequeDet.Rows.Count == 0 && payType == "CHEQUE" && dgvRemChq.Rows.Count == 0)
            {
              //  MessageBox.Show("No cheque settlements!", "Cheque settlements", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.Cursor = Cursors.Default;
        }

        private void refreshGrids()
        {
            grvPaymentDocDet.DataSource = null;
            //grvPaymentDocDet.AutoGenerateColumns = true;

            grvReturnChequeDet.DataSource = null;
            // grvReturnChequeDet.AutoGenerateColumns = true;

            grvPopUpSelect.DataSource = null;
            // grvPopUpSelect.AutoGenerateColumns = true;

            grvInvoiceDet.DataSource = null;
            grvInvoiceDet.AutoGenerateColumns = false;

            grvReceptDet.DataSource = null;
            grvReceptDet.AutoGenerateColumns = false;


        }

        private void txtDocNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.btnGetDocDet_Click(sender, e);
            }
        }

        private void arrangeAlignments(DataGridView gridview)
        {
            if (gridview.Rows.Count > 0)
            {
                foreach (DataGridViewColumn col in gridview.Columns)
                {
                    if (col.HeaderText == "Total" || col.HeaderText == "Used Amount" || col.HeaderText == "Amount" || col.HeaderText == "Settle value")
                    {
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        col.DefaultCellStyle.Format = "N2";
                        // col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                        // col.HeaderCell.Style
                    }

                    if (col.HeaderText == "Date")//
                    {
                        col.DefaultCellStyle.Format = "d";
                    }
                }
            }
        }

        private void grvPopUpSelect_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                this.Cursor = Cursors.WaitCursor;

                DataGridViewRow row = grvPopUpSelect.Rows[e.RowIndex];
                string bankCd = "";
                //string brank = row.Cells["Branch"].Value.ToString();
                string cardType = "";
                string payType = ddlPayMode.SelectedValue.ToString();
                string DocNo = txtDocNo.Text.Trim();

                if (payType == "CHEQUE" || payType == "CRCD")
                {
                    bankCd = row.Cells["Bank"].Value.ToString();

                    if (payType == "CRCD")
                    {
                        cardType = row.Cells["Card Type"].Value.ToString();
                    }
                    if (payType == "CHEQUE")
                    {
                        DataTable dt_retCheques = CHNLSVC.Sales.GetReturnCheque_detWithPayments(DocNo, bankCd);
                        if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "PMENQ") == false)
                        {
                            if (dt_retCheques.Rows.Count > 0)
                            {
                                var result = from r in dt_retCheques.AsEnumerable()
                                             where r.Field<string>("sar_profit_center_cd") == BaseCls.GlbUserDefProf
                                             select r;
                               

                                if (result.Any())
                                {
                                    DataTable dtResult = result.CopyToDataTable();
                                    dt_retCheques = dtResult;
                                }
                                else
                                {
                                    dt_retCheques = null;
                                }
                            }
                        }
                        grvReturnChequeDet.DataSource = dt_retCheques;
                        // grvReturnChequeDet.DataBind();
                        // Panel_RtnChq.Visible = true; TODO:
                    }
                }

                DataTable dt = CHNLSVC.Sales.GetPaymodeDetail(BaseCls.GlbUserComCode, "", payType, txtDocNo.Text.Trim(), bankCd, cardType);
                //--------------------------------------------------
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string PC = dt.Rows[0]["Profit Center"].ToString();
                        if (PC != BaseCls.GlbUserDefProf)
                        {
                            //PMENQ
                            if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "PMENQ") == false)
                            {
                                MessageBox.Show("Cannot view different profit center documents!\n(Advice: Reqired permission code :PMENQ)");
                                this.Cursor = Cursors.Default;
                                return;
                            }

                        }
                    }

                }

                //---------------------------------------------------
                {
                    grvPaymentDocDet.DataSource = dt;


                    DataTable invList = CHNLSVC.Sales.getInvoicesBased_onPayType(BaseCls.GlbUserComCode, DocNo, payType, bankCd, cardType);
                 
                    
                    grvInvoiceDet.DataSource = invList;
                    // grvInvoiceDet.DataBind();

                    DataTable ReceiptList = CHNLSVC.Sales.get_Receipts_BasedonPayType(BaseCls.GlbUserComCode, DocNo, payType, bankCd, cardType);
                    grvReceptDet.DataSource = ReceiptList;
                    foreach (DataGridViewRow dgvr in grvReceptDet.Rows)
                    {
                        if (dgvr.Cells["sar_is_mgr_iss"].Value.ToString() == "1")
                        {
                            dgvr.Cells["issued_by"].Value = "Manager";
                        }
                        else
                        {
                            dgvr.Cells["issued_by"].Value = "Customer";
                        }
                    }
                }
                this.Cursor = Cursors.Default;
            }
        }

        private void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshGrids();
            if (ddlPayMode.SelectedValue == null)
            {
                return;
            }
             
            else if (ddlPayMode.SelectedValue.ToString() == "CHEQUE")
            {
                panel_retnCheque.Visible = true;
            }
            //else if (ddlPayMode.SelectedValue.ToString() == "CRCD")
            //{
            //    panel_retnCheque.Visible = true;
            //}
            else
            {
                panel_retnCheque.Visible = false;
            }

            txtDocNo.Text = "";
        }

        private void txtDocNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ImgBtnDocSearch_Click(null, null);
        }

        private void txtDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.ImgBtnDocSearch_Click(null, null);
            }
        }

        private void txtDocNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void grvPaymentDocDet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grvReceptDet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
