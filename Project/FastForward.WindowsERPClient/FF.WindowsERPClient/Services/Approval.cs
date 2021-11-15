using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Services
{
    public partial class Approval : Base
    {
        private string gblJobnumber = string.Empty;
        private Int32 gblJobLine = -1;
        private string gblHeaderCode = string.Empty;

        private string gblAdditional1 = string.Empty;
        private string gblAdditional2 = string.Empty;

        private List<PriceDefinitionRef> _PriceDefinitionRef = null;
        private string DefaultBook = string.Empty;
        private string DefaultLevel = string.Empty;
        private List<PriceBookLevelRef> _priceBookLevelRefList = null;

        private String SerialName = string.Empty;

        public Approval()
        {
            InitializeComponent();
            dgvMainItems.AutoGenerateColumns = false;

            pnlDiscount.Size = new Size(707, 286);
        }

        private void Approval_Load(object sender, EventArgs e)
        {
            Service_Chanal_parameter _Parameters = null;
            _Parameters = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
            if (_Parameters != null)
            {
                if (_Parameters.SP_ISNEEDWIP != 1)
                {
                    MessageBox.Show("Service parameter(s) not setup!", "Default Parameter(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Enabled = false;
                }

                SerialName = _Parameters.SP_DB_SERIAL;
            }
            else
            {
                MessageBox.Show("Service parameter(s) not setup!", "Default Parameter(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Enabled = false;
            }

            fillListView();
            clearScreen();
            dgvMainItems.AutoGenerateColumns = false;
        }

        private void fillListView()
        {
            lstItems.View = View.Details;
            lstItems.FullRowSelect = true;
            lstItems.Columns.Add("  ", lstItems.Width - 10);
            ListViewGroup grp1 = new ListViewGroup("Service Job");
            lstItems.Groups.Add(grp1);
            lstItems.Items.Add(new ListViewItem(new string[] { GetEnumDesc.GetEnumDescription(CommonEnum.Job_Cancel) }, grp1));
            lstItems.Items.Add(new ListViewItem(new string[] { GetEnumDesc.GetEnumDescription(CommonEnum.Update_job_items_as_under_warranty) }, grp1));
            lstItems.Items.Add(new ListViewItem(new string[] { GetEnumDesc.GetEnumDescription(CommonEnum.Job_confirmation_cancel) }, grp1));
            lstItems.Items.Add(new ListViewItem(new string[] { GetEnumDesc.GetEnumDescription(CommonEnum.Job_FOC_approval) }, grp1));
            lstItems.Items.Add(new ListViewItem(new string[] { GetEnumDesc.GetEnumDescription(CommonEnum.Job_hold_and_re_open) }, grp1));

            ListViewGroup grp2 = new ListViewGroup("Estimate");
            lstItems.Groups.Add(grp2);
            lstItems.Items.Add(new ListViewItem(new string[] { GetEnumDesc.GetEnumDescription(CommonEnum.Job_estimate_approval) }, grp2));

            ListViewGroup grp3 = new ListViewGroup("Warranty Claims");
            lstItems.Groups.Add(grp3);
            lstItems.Items.Add(new ListViewItem(new string[] { GetEnumDesc.GetEnumDescription(CommonEnum.Customer_warranty_claim_request_approve) }, grp3));
            lstItems.Items.Add(new ListViewItem(new string[] { GetEnumDesc.GetEnumDescription(CommonEnum.Cancel_appvoed_customer_warranty_clain_request) }, grp3));

            ListViewGroup grp4 = new ListViewGroup("MRN");
            lstItems.Groups.Add(grp4);
            lstItems.Items.Add(new ListViewItem(new string[] { GetEnumDesc.GetEnumDescription(CommonEnum.MRN_Approve) }, grp4));
            lstItems.Items.Add(new ListViewItem(new string[] { GetEnumDesc.GetEnumDescription(CommonEnum.Cancel_approved_mrn) }, grp4));

            ListViewGroup grp5 = new ListViewGroup("Discount");
            lstItems.Groups.Add(grp5);
            lstItems.Items.Add(new ListViewItem(new string[] { GetEnumDesc.GetEnumDescription(CommonEnum.Discount_approval_separately_for_each_job) }, grp5));

            //ListViewGroup grp6 = new ListViewGroup("Service Agreement");
            //lstItems.Groups.Add(grp6);
            //lstItems.Items.Add(new ListViewItem(new string[] { GetEnumDesc.GetEnumDescription(CommonEnum.Service_agreement_approcal) }, grp6));

            string disPlayValue = GetEnumDesc.GetEnumDescription(CommonEnum.Job_Cancel);
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobDetails:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EMP_ALL:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DefectTypes:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Designation:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town_new:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Mobile:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceEstimate:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "1.1,2,2.1,2.2,3,5,5.1,4,4.1,3,6,6.1" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MRN:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                    break;
            }

            return paramsText.ToString();
        }

        #region Events

        private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            // GetJobDetails(0);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear the screen?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                clearScreen();
            }
        }

        private void txtJobNo_DoubleClick(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
            DateTime dtTemp = DateTime.Today.AddMonths(-1);
            DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, dtTemp, DateTime.Today);
            _CommonSearch.dtpFrom.Value = dtTemp;
            _CommonSearch.dtpTo.Value = DateTime.Today;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtJobNo;
            this.Cursor = Cursors.Default;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtJobNo.Focus();
        }

        private void txtJobNo_Leave(object sender, EventArgs e)
        {
        }

        private void txtJobNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStrip1.Focus();
                btnView.Select();
            }
            else if (e.KeyCode == Keys.F2)
            {
                txtJobNo_DoubleClick(null, null);
            }
        }

        private void btnVouNo_Click(object sender, EventArgs e)
        {
            txtJobNo_DoubleClick(null, null);
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            GetJobDetails(1);
        }

        private void dgvMainItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (dgvMainItems.Rows.Count > 0)
                {
                    for (int i = 0; i < dgvMainItems.Rows.Count; i++)
                    {
                        if (dgvMainItems.Rows[i].Cells[0].Value != null)
                        {
                            dgvMainItems.Rows[i].Cells[0].Value = false;
                        }
                    }

                    dgvMainItems.Rows[e.RowIndex].Cells[0].Value = true;

                    if (lstItems.SelectedItems[0].Text.ToUpper() != GetEnumDesc.GetEnumDescription(CommonEnum.Job_estimate_approval).ToUpper())
                    {
                        if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.MRN_Approve).ToUpper())
                        {
                            if (dgvMainItems.SelectedRows[0].Cells["STATUS"].Value.ToString() == "REJECTED")
                            {
                                MessageBox.Show("This MRN is Rejected.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            if (dgvMainItems.SelectedRows[0].Cells["STATUS"].Value.ToString() == "APPROVED")
                            {
                                MessageBox.Show("This MRN is already approved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            gblJobnumber = dgvMainItems.SelectedRows[0].Cells["ITR_JOB_NO"].Value.ToString();
                            gblJobLine = Convert.ToInt32(dgvMainItems.SelectedRows[0].Cells["ITR_JOB_LINE"].Value.ToString());

                            string MRN_Num = dgvMainItems.SelectedRows[0].Cells["ITR_REQ_NO"].Value.ToString();

                            ServicesWIP_MRN frm = new ServicesWIP_MRN(gblJobnumber, gblJobLine, MRN_Num);
                            frm.StartPosition = FormStartPosition.Manual;
                            frm.Location = new Point(this.Location.X + this.Width - 120 - frm.Width, this.Location.Y + 80);
                            frm.ShowDialog();

                            btnView_Click(null, null);
                        }
                        else if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.Cancel_approved_mrn).ToUpper())
                        {
                            gblJobnumber = dgvMainItems.SelectedRows[0].Cells["ITR_JOB_NO"].Value.ToString();
                            gblJobLine = Convert.ToInt32(dgvMainItems.SelectedRows[0].Cells["ITR_JOB_LINE"].Value.ToString());
                            gblAdditional1 = dgvMainItems.SelectedRows[0].Cells["ITR_REQ_NO"].Value.ToString();
                            gblAdditional2 = dgvMainItems.SelectedRows[0].Cells["ITR_SEQ_NO"].Value.ToString();
                        }
                        else if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.Customer_warranty_claim_request_approve).ToUpper() || lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.Cancel_appvoed_customer_warranty_clain_request).ToUpper())
                        {
                            gblJobnumber = dgvMainItems.SelectedRows[0].Cells["Insa_jb_no"].Value.ToString();
                            gblJobLine = Convert.ToInt32(dgvMainItems.SelectedRows[0].Cells["Insa_anal4"].Value.ToString());
                        }
                        else if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.Discount_approval_separately_for_each_job).ToUpper())
                        {
                            gblJobnumber = dgvMainItems.SelectedRows[0].Cells["JobNo"].Value.ToString();
                            gblJobLine = Convert.ToInt32(dgvMainItems.SelectedRows[0].Cells["JobLine"].Value.ToString());
                        }
                        else
                        {
                            gblJobnumber = dgvMainItems.SelectedRows[0].Cells["JBD_JOBNO"].Value.ToString();
                            gblJobLine = Convert.ToInt32(dgvMainItems.SelectedRows[0].Cells["JBD_JOBLINE"].Value.ToString());
                        }

                        if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.Job_confirmation_cancel).ToUpper())
                        {
                            //gblHeaderCode = dgvMainItems.SelectedRows[0].Cells["jcd_no"].Value.ToString();
                            gblHeaderCode = dgvMainItems.SelectedRows[0].Cells["Additional1"].Value.ToString();
                        }
                    }
                    else
                    {
                        gblJobnumber = dgvMainItems.SelectedRows[0].Cells["ESH_JOB_NO"].Value.ToString();
                        gblHeaderCode = dgvMainItems.SelectedRows[0].Cells["ESH_ESTNO"].Value.ToString();
                        gblJobLine = 1;
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void lstItems_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            GetJobDetails(0);
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (isAnySelected())
            {
                if (MessageBox.Show("Do you want to " + btnApprove.Text + " ?", "Service Approve", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                Int32 result = -1;
                String Error = string.Empty;
                if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.Job_Cancel).ToUpper())
                {
                    result = CHNLSVC.CustService.ServiceApprove(gblJobnumber, gblJobLine, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, cmbStatus.Text, txtRemark.Text, (Int32)CommonEnum.Job_Cancel, out Error, "", "");
                }
                else if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.Update_job_items_as_under_warranty).ToUpper())
                {
                    result = CHNLSVC.CustService.ServiceApprove(gblJobnumber, gblJobLine, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, cmbStatus.Text, txtRemark.Text, (Int32)CommonEnum.Update_job_items_as_under_warranty, out Error, "", "");
                }
                else if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.Job_confirmation_cancel).ToUpper())
                {
                    string ComfirmationNum = gblHeaderCode;
                    result = CHNLSVC.CustService.ServiceApprove(gblJobnumber, gblJobLine, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, cmbStatus.Text, txtRemark.Text, (Int32)CommonEnum.Job_confirmation_cancel, out Error, ComfirmationNum, "");
                }
                else if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.Job_FOC_approval).ToUpper())
                {
                    result = CHNLSVC.CustService.ServiceApprove(gblJobnumber, gblJobLine, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, cmbStatus.Text, txtRemark.Text, (Int32)CommonEnum.Job_FOC_approval, out Error, "", "");
                }
                else if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.Job_hold_and_re_open).ToUpper())
                {
                    result = CHNLSVC.CustService.ServiceApprove(gblJobnumber, gblJobLine, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, cmbStatus.Text, txtRemark.Text, (Int32)CommonEnum.Job_hold_and_re_open, out Error, "", "");
                }
                else if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.Job_estimate_approval).ToUpper())
                {
                    result = CHNLSVC.CustService.ServiceApprove(gblJobnumber, gblJobLine, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, cmbStatus.Text, txtRemark.Text, (Int32)CommonEnum.Job_estimate_approval, out Error, gblHeaderCode, "");
                }
                else if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.Cancel_approved_mrn).ToUpper())
                {
                    result = CHNLSVC.CustService.ServiceApprove(gblJobnumber, gblJobLine, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, cmbStatus.Text, txtRemark.Text, (Int32)CommonEnum.Cancel_approved_mrn, out Error, gblAdditional1, gblAdditional2);
                }
                else if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.Customer_warranty_claim_request_approve).ToUpper())
                {
                    result = CHNLSVC.CustService.ServiceApprove(gblJobnumber, gblJobLine, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, cmbStatus.Text, txtRemark.Text, (Int32)CommonEnum.Customer_warranty_claim_request_approve, out Error, gblAdditional1, gblAdditional2);
                }
                else if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.Discount_approval_separately_for_each_job).ToUpper())
                {
                    #region MyRegion

                    //SaveDiscount

                    if (string.IsNullOrEmpty(txtDisAmt.Text))
                    {
                        txtDisAmt.Text = "0.00";
                    }
                    if (string.IsNullOrEmpty(txtDisRate.Text))
                    {
                        txtDisRate.Text = "0.00";
                    }
                    if (txtDisRate.Text == "0.00" && txtDisAmt.Text == "0.00")
                    {
                        MessageBox.Show("Pleas enter valid discount", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (cmbBook.SelectedValue == null || cmbBook.SelectedValue.ToString() == "")
                    {
                        MessageBox.Show("Pleas select a price book", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (cmbLevel.SelectedValue == null || cmbLevel.ToString() == "")
                    {
                        MessageBox.Show("Pleas select a price level", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    Service_JOB_HDR oJOB_HDR = CHNLSVC.CustService.GetServiceJobHeader(txtJobNo.Text, BaseCls.GlbUserComCode);

                    List<Service_Confirm_detail> oService_Confirm_detail = CHNLSVC.CustService.GET_CONF_DET_BY_JOB(txtJobNo.Text);

                    CashGeneralDicountDef oItem = new CashGeneralDicountDef();
                    oItem.Sgdd_seq = 0;
                    oItem.Sgdd_circular = txtJobNo.Text;
                    oItem.Sgdd_sale_tp = cmbInvoType.SelectedValue.ToString();
                    oItem.Sgdd_com = BaseCls.GlbUserComCode;
                    oItem.Sgdd_pc = BaseCls.GlbUserDefProf;
                    oItem.Sgdd_from_dt = dtpFromDiscount.Value.Date;
                    oItem.Sgdd_to_dt = dtpToDiscount.Value.Date;
                    oItem.Sgdd_cust_cd = oJOB_HDR.SJB_B_CUST_CD;
                    oItem.Sgdd_pb = cmbBook.SelectedValue.ToString();
                    oItem.Sgdd_pb_lvl = cmbLevel.SelectedValue.ToString();
                    oItem.Sgdd_itm = dgvMainItems.SelectedRows[0].Cells["ItemCode"].Value.ToString();
                    oItem.Sgdd_alw_ser = false;
                    oItem.Sgdd_alw_pro = false;
                    oItem.Sgdd_req_ref = "";
                    oItem.Sgdd_disc_val = Convert.ToDecimal(txtDisAmt.Text);
                    oItem.Sgdd_disc_rt = Convert.ToDecimal(txtDisRate.Text);
                    oItem.Sgdd_no_of_times = 9999;
                    oItem.Sgdd_no_of_used_times = 0;
                    oItem.Sgdd_stus = true;
                    oItem.Sgdd_cre_by = BaseCls.GlbUserID;
                    oItem.Sgdd_cre_dt = DateTime.Today.Date;
                    oItem.Sgdd_mod_by = BaseCls.GlbUserID;
                    oItem.Sgdd_mod_dt = DateTime.Today.Date;
                    //result = CHNLSVC.Sales.SaveDiscount(oItem);

                    result = CHNLSVC.CustService.ServiceApporvalDiscountProcess(txtJobNo.Text.Trim(), oItem, out Error);

                    #endregion MyRegion
                }

            L1: int i = 1;
                if (result > 0)
                {
                    MessageBox.Show("Process Completed Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnView_Click(null, null);
                    return;
                }
                else
                {
                    switch (result)
                    {
                        case -1:
                            if (!string.IsNullOrEmpty(Error))
                            {
                                MessageBox.Show("Process terminated. Error :" + Error, "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            else
                            {
                                MessageBox.Show("Process terminated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                            break;

                        case -5:
                            MessageBox.Show("Process terminated.\nStock items available for the job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            break;

                        case -6:
                            MessageBox.Show("Process terminated.\nSupplier warranty claim requests available", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            break;

                        case -7:
                            MessageBox.Show("Process terminated.\nMRN available for the job", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            break;

                        case -8:
                            MessageBox.Show("Process terminated.\nItem is over warranty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            break;

                        case -9:
                            MessageBox.Show("Process terminated.\nItem is under warranty ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            break;

                        case -10:
                            MessageBox.Show("Process terminated.\nConfirmation is Invoiced", "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            break;

                        case -11:
                            if (MessageBox.Show("There are partially used MRNs.\n" + Error + "\n\nDo you want to cancel?", "Partially Use MRN", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                            {
                                result = CHNLSVC.CustService.ServiceApprove(gblJobnumber, gblJobLine, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, "PartialCancel", txtRemark.Text, (Int32)CommonEnum.Cancel_approved_mrn, out Error, gblAdditional1, "");
                            }
                            break;

                        case -12:
                            MessageBox.Show("This job is not HOLD.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;

                            goto L1;

                            break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a job number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
        }

        private void cmbInvoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPriceBook(cmbInvoType.SelectedValue.ToString());
        }

        private void cmbBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPriceLevel(cmbInvoType.SelectedValue.ToString(), cmbBook.SelectedValue.ToString());
        }

        private void txtDisRate_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDisRate.Text))
            {
                txtDisRate.Text = "0.00";
            }
        }

        private void txtDisAmt_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDisAmt.Text))
            {
                txtDisAmt.Text = "0.00";
            }
        }

        private void cmbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtpFromDiscount.Focus();
        }

        private void cmbInvoType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbBook.Focus();
            }
        }

        private void cmbBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbLevel.Focus();
            }
        }

        private void cmbLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpFromDiscount.Focus();
            }
        }

        private void dtpFromDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpTo.Focus();
            }
        }

        private void dtpToDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDisRate.Focus();
            }
        }

        private void txtDisRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDisAmt.Focus();
            }
        }

        private void txtDisAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStrip1.Focus();
                btnApprove.Select();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to close?", "Service Approve", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void txtRemark_TextChanged(object sender, EventArgs e)
        {
            if (txtRemark.Text.Length >= 199)
            {
                MessageBox.Show("Remark is too much lengthy");
                txtRemark.Focus();
            }
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRemark.Clear();
        }

        #endregion Events

        #region Methods

        private void GetJobDetails(int option)
        {
            cmbStatus.Enabled = true;
            btnApprove.Visible = true;

            label14.Visible = false;
            txtMRNNumber.Visible = false;
            btnMRNSearch.Visible = false;
            dgvMainItems.Location = new Point(270, 66);
            dgvMainItems.Size = new System.Drawing.Size(707, 244);
            pnlFitler.Size = new Size(713, 37);

            if (lstItems.SelectedItems.Count > 0 && !string.IsNullOrEmpty(lstItems.SelectedItems[0].Text))
            {
                lblFunctionName.Text = lstItems.SelectedItems[0].Text;

                pnlDiscount.Visible = false;
                dgvMainItems.Size = new Size(707, 374);

                DateTime from, to;
                from = dtpFrom.Value.Date;
                to = dtpTo.Value.Date;

                if (!String.IsNullOrEmpty(txtJobNo.Text.Trim()))
                {
                    from = DateTime.MinValue;
                    to = DateTime.MaxValue;
                }

                if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.Job_Cancel).ToUpper())
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10803))
                    {
                        MessageBox.Show("You don't have the permission for this function.\nPermission Code :- 10803", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    #region MyRegion

                    btnApprove.Text = "Cancel";
                    setGridColumns("Job Cancel");
                    String stage = string.Empty;

                    string[] item = new string[] { "Cancel" };
                    cmbStatus.DataSource = item;

                    stage = "1.1,2,2.1,2.2,3,5,5.1,4,4.1,3,6,6.1";



                    List<Service_Appove> oItems = CHNLSVC.CustService.GetJobsServiceApprove(BaseCls.GlbUserComCode, from, to, txtJobNo.Text, stage, string.Empty, BaseCls.GlbUserDefProf, (Int32)CommonEnum.Job_Cancel, BaseCls.GlbUserDefLoca);
                    if (oItems.Count > 0)
                    {
                        dgvMainItems.DataSource = new List<Service_Appove>();
                        dgvMainItems.AutoGenerateColumns = false;
                        dgvMainItems.DataSource = oItems;
                    }
                    else
                    {
                        if (option == 1)
                        {
                            if (!String.IsNullOrEmpty(txtJobNo.Text))
                            {
                                MessageBox.Show("Please select a correct job number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        clearScreen();
                        txtJobNo.Clear();
                        txtJobNo.Focus();
                        lblFunctionName.Text = lstItems.SelectedItems[0].Text;
                        return;
                    }

                    #endregion MyRegion
                }
                else if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.Update_job_items_as_under_warranty).ToUpper())
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10804))
                    {
                        MessageBox.Show("You don't have the permission for this function.\nPermission Code :- 10804", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    #region MyRegion

                    btnApprove.Text = "Update";
                    setGridColumns("Warranty Update");

                    string[] item = new string[] { "Under Warranty", "Over Warranty" };
                    cmbStatus.DataSource = item;

                    String stage = string.Empty;

                    stage = "2,2.1,3,5,5.1,4,4.1,3,6,6.1";

                    //DateTime from, to;

                    //from = dtpFrom.Value.Date;
                    //to = dtpTo.Value.Date;

                    List<Service_Appove> oItems = CHNLSVC.CustService.GetJobsServiceApprove(BaseCls.GlbUserComCode, from, to, txtJobNo.Text, stage, string.Empty, BaseCls.GlbUserDefProf, (Int32)CommonEnum.Update_job_items_as_under_warranty, BaseCls.GlbUserDefLoca);
                    if (oItems.Count > 0)
                    {
                        dgvMainItems.DataSource = new List<Service_Appove>();
                        dgvMainItems.AutoGenerateColumns = false;
                        dgvMainItems.DataSource = oItems;
                    }
                    else
                    {
                        if (option == 1)
                        {
                            if (!String.IsNullOrEmpty(txtJobNo.Text))
                            {
                                MessageBox.Show("Please select a correct job number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        clearScreen();
                        txtJobNo.Clear();
                        txtJobNo.Focus();
                        lblFunctionName.Text = lstItems.SelectedItems[0].Text;
                        return;
                    }

                    #endregion MyRegion
                }
                else if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.Job_confirmation_cancel).ToUpper())
                {
                    lblFunctionName.Text = lstItems.SelectedItems[0].Text;
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10805))
                    {
                        MessageBox.Show("You don't have the permission for this function.\nPermission Code :- 10805", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    #region MyRegion

                    btnApprove.Text = "Cancel";
                    setGridColumns("Job confirmation cancel");

                    string[] item = new string[] { "Cancel" };
                    cmbStatus.DataSource = item;

                    String stage = string.Empty;

                    stage = "7";

                    //DateTime from, to;

                    //from = dtpFrom.Value.Date;
                    //to = dtpTo.Value.Date;

                    List<Service_Appove> oItems = CHNLSVC.CustService.GetJobsServiceApprove(BaseCls.GlbUserComCode, from, to, txtJobNo.Text, stage, string.Empty, BaseCls.GlbUserDefProf, (Int32)CommonEnum.Job_confirmation_cancel, BaseCls.GlbUserDefLoca);
                    if (oItems.Count > 0)
                    {
                        dgvMainItems.DataSource = new List<Service_Appove>();
                        dgvMainItems.AutoGenerateColumns = false;
                        dgvMainItems.DataSource = oItems;
                    }
                    else
                    {
                        if (option == 1)
                        {
                            if (!String.IsNullOrEmpty(txtJobNo.Text))
                            {
                                MessageBox.Show("Please select a correct job number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        clearScreen();
                        txtJobNo.Clear();
                        txtJobNo.Focus();
                        lblFunctionName.Text = lstItems.SelectedItems[0].Text;
                        return;
                    }

                    #endregion MyRegion
                }
                else if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.Job_FOC_approval).ToUpper())
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10806))
                    {
                        MessageBox.Show("You don't have the permission for this function.\nPermission Code :- 10806", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    #region MyRegion

                    btnApprove.Text = "Process";
                    setGridColumns("Job Cancel");
                    String stage = string.Empty;

                    string[] item = new string[] { "Approve", "Reject" };
                    cmbStatus.DataSource = item;

                    stage = "2,2.1,3,5,5.1,4,4.1,3,6,6.1";

                    //DateTime from, to;

                    //from = dtpFrom.Value.Date;
                    //to = dtpTo.Value.Date;

                    List<Service_Appove> oItems = CHNLSVC.CustService.GetJobsServiceApprove(BaseCls.GlbUserComCode, from, to, txtJobNo.Text, stage, string.Empty, BaseCls.GlbUserDefProf, (Int32)CommonEnum.Job_Cancel, BaseCls.GlbUserDefLoca);
                    if (oItems.Count > 0)
                    {
                        dgvMainItems.DataSource = new List<Service_Appove>();
                        dgvMainItems.AutoGenerateColumns = false;
                        dgvMainItems.DataSource = oItems;
                    }
                    else
                    {
                        //  MessageBox.Show("Please select a correct job no", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearScreen();
                        txtJobNo.Clear();
                        txtJobNo.Focus();
                        lblFunctionName.Text = lstItems.SelectedItems[0].Text;
                        return;
                    }

                    #endregion MyRegion
                }
                else if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.Job_hold_and_re_open).ToUpper())
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10807))
                    {
                        MessageBox.Show("You don't have the permission for this function.\nPermission Code :- 10807", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    #region MyRegion

                    btnApprove.Text = "Save";
                    setGridColumns(CommonEnum.Job_hold_and_re_open.ToString());
                    String stage = string.Empty;

                    string[] item = new string[] { "Hold", "Re-Open" };
                    cmbStatus.DataSource = item;

                    stage = "2,2.1,3,5,5.1,4,4.1,3,6,6.1,13";

                    //DateTime from, to;

                    //from = dtpFrom.Value.Date;
                    //to = dtpTo.Value.Date;

                    List<Service_Appove> oItems = CHNLSVC.CustService.GetJobsServiceApprove(BaseCls.GlbUserComCode, from, to, txtJobNo.Text, stage, string.Empty, BaseCls.GlbUserDefProf, (Int32)CommonEnum.Job_hold_and_re_open, BaseCls.GlbUserDefLoca);
                    if (oItems.Count > 0)
                    {
                        dgvMainItems.DataSource = new List<Service_Appove>();
                        dgvMainItems.AutoGenerateColumns = false;
                        dgvMainItems.DataSource = oItems;
                    }
                    else
                    {
                        //  MessageBox.Show("Please select a correct job no", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearScreen();
                        txtJobNo.Clear();
                        txtJobNo.Focus();
                        lblFunctionName.Text = lstItems.SelectedItems[0].Text;
                        return;
                    }

                    #endregion MyRegion
                }
                else if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.Job_estimate_approval).ToUpper())
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10808))
                    {
                        MessageBox.Show("You don't have the permission for this function.\nPermission Code :- 10808", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    #region MyRegion

                    btnApprove.Text = "Save";
                    btnPrint.Visible = true;
                    setGridColumns(CommonEnum.Job_estimate_approval.ToString());
                    String stage = string.Empty;

                    string[] item = new string[] { "Approve", "Reject", "Customer Approve" };
                    cmbStatus.DataSource = item;

                    stage = "2,2.1,3,5,5.1,4,4.1,3,6,6.1";
                    //DateTime from, to;
                    //from = dtpFrom.Value.Date;
                    //to = dtpTo.Value.Date;

                    List<Service_Estimate_Header> oEstHeader = CHNLSVC.CustService.GetEstimateApprove(BaseCls.GlbUserComCode, from, to, txtJobNo.Text, stage, string.Empty, BaseCls.GlbUserDefProf, (Int32)CommonEnum.Job_hold_and_re_open, BaseCls.GlbUserDefLoca);
                    if (oEstHeader.Count > 0)
                    {
                        dgvMainItems.DataSource = new List<Service_Appove>();
                        dgvMainItems.AutoGenerateColumns = false;
                        dgvMainItems.DataSource = oEstHeader;
                    }
                    else
                    {
                        //  MessageBox.Show("Please select a correct job no", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearScreen();
                        txtJobNo.Clear();
                        txtJobNo.Focus();
                        lblFunctionName.Text = lstItems.SelectedItems[0].Text;
                        return;
                    }

                    #endregion MyRegion
                }
                else if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.MRN_Approve).ToUpper())
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10809))
                    {
                        MessageBox.Show("You don't have the permission for this function.\nPermission Code :- 10809", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    #region MyRegion

                    btnApprove.Text = "Open MRN";
                    setGridColumns("MRN Approve");
                    String stage = string.Empty;

                    cmbStatus.Enabled = false;
                    btnApprove.Visible = false;

                    label14.Visible = true;
                    txtMRNNumber.Visible = true;
                    btnMRNSearch.Visible = true;

                    dgvMainItems.Location = new Point(270, 82);
                    dgvMainItems.Size = new System.Drawing.Size(707, 230);
                    pnlFitler.Size = new System.Drawing.Size(713, 54);

                    List<Service_Appove_MRN> oItems = CHNLSVC.CustService.GET_MRN_FOR_JOB(txtJobNo.Text, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty);
                    if (oItems.Count > 0)
                    {
                        if (dtpFrom.Value > dtpTo.Value)
                        {
                            MessageBox.Show("Please enter a valid date range.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtpFrom.Value = dtpTo.Value;
                            return;
                        }

                        if (string.IsNullOrEmpty(txtJobNo.Text))
                        {
                            if (String.IsNullOrEmpty(txtMRNNumber.Text))
                            {
                                List<Service_Appove_MRN> oItemsNew = oItems.FindAll(x => x.ITR_DT >= dtpFrom.Value.Date && x.ITR_DT <= dtpTo.Value.Date && x.STATUS != "CANCELED");
                                dgvMainItems.DataSource = new List<Service_Appove_MRN>();
                                dgvMainItems.AutoGenerateColumns = false;
                                dgvMainItems.DataSource = oItemsNew;
                            }
                            else
                            {
                                List<Service_Appove_MRN> oItemsNew = oItems.FindAll(x => x.ITR_REQ_NO == txtMRNNumber.Text.Trim() && x.STATUS != "CANCELED");
                                dgvMainItems.DataSource = new List<Service_Appove_MRN>();
                                dgvMainItems.AutoGenerateColumns = false;
                                dgvMainItems.DataSource = oItemsNew;
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(txtMRNNumber.Text))
                            {
                                dgvMainItems.DataSource = new List<Service_Appove_MRN>();
                                dgvMainItems.AutoGenerateColumns = false;
                                dgvMainItems.DataSource = oItems.FindAll(x => x.STATUS != "CANCELED");
                            }
                            else
                            {
                                List<Service_Appove_MRN> oItemsNew = oItems.FindAll(x => x.ITR_REQ_NO == txtMRNNumber.Text.Trim() && x.STATUS != "CANCELED");
                                dgvMainItems.DataSource = new List<Service_Appove_MRN>();
                                dgvMainItems.AutoGenerateColumns = false;
                                dgvMainItems.DataSource = oItemsNew;
                            }

                        }
                    }
                    else
                    {
                        if (option == 1)
                        {
                            if (!String.IsNullOrEmpty(txtJobNo.Text))
                            {
                                MessageBox.Show("Please select a correct job number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        clearScreen();
                        txtJobNo.Clear();
                        txtJobNo.Focus();
                        lblFunctionName.Text = lstItems.SelectedItems[0].Text;
                        return;
                    }

                    #endregion MyRegion
                }
                else if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.Cancel_approved_mrn).ToUpper())
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10810))
                    {
                        MessageBox.Show("You don't have the permission for this function.\nPermission Code :- 10810", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    #region MyRegion

                    btnApprove.Text = "Cancel";
                    setGridColumns("MRN Approve");
                    String stage = string.Empty;

                    string[] item = new string[] { "Cancel" };
                    cmbStatus.DataSource = item;

                    cmbStatus.Enabled = true;
                    btnApprove.Visible = true;

                    label14.Visible = true;
                    txtMRNNumber.Visible = true;
                    btnMRNSearch.Visible = true;
                    dgvMainItems.Location = new Point(270, 82);
                    dgvMainItems.Size = new System.Drawing.Size(707, 230);
                    pnlFitler.Size = new System.Drawing.Size(713, 54);

                    //DateTime from, to;

                    //from = dtpFrom.Value.Date;
                    //to = dtpTo.Value.Date;

                    List<Service_Appove_MRN> oItems = CHNLSVC.CustService.GET_MRN_FOR_JOB(txtJobNo.Text, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty).FindAll(x => x.STATUS == "APPROVED");
                    if (oItems.Count > 0)
                    {

                        if (dtpFrom.Value > dtpTo.Value)
                        {
                            MessageBox.Show("Please enter a valid date range.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtpFrom.Value = dtpTo.Value;
                            return;
                        }

                        if (string.IsNullOrEmpty(txtJobNo.Text))
                        {
                            List<Service_Appove_MRN> oItemsNew = oItems.FindAll(x => x.ITR_DT >= dtpFrom.Value.Date && x.ITR_DT <= dtpTo.Value.Date);
                            dgvMainItems.DataSource = new List<Service_Appove_MRN>();
                            dgvMainItems.AutoGenerateColumns = false;
                            dgvMainItems.DataSource = oItemsNew.FindAll(x => x.STATUS == "APPROVED");
                        }
                        else
                        {
                            dgvMainItems.DataSource = new List<Service_Appove_MRN>();
                            dgvMainItems.AutoGenerateColumns = false;
                            dgvMainItems.DataSource = oItems.FindAll(x => x.STATUS == "APPROVED");
                        }
                        //dgvMainItems.DataSource = new List<Service_Appove_MRN>();
                        //dgvMainItems.AutoGenerateColumns = false;
                        //dgvMainItems.DataSource = oItems.FindAll(x => x.STATUS == "APPROVED");
                    }
                    else
                    {
                        if (option == 1)
                        {
                            if (!String.IsNullOrEmpty(txtJobNo.Text))
                            {
                                MessageBox.Show("Please select a correct job number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        clearScreen();
                        txtJobNo.Clear();
                        txtJobNo.Focus();
                        lblFunctionName.Text = lstItems.SelectedItems[0].Text;
                        return;
                    }

                    #endregion MyRegion
                }
                else if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.Customer_warranty_claim_request_approve).ToUpper())
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10811))
                    {
                        MessageBox.Show("You don't have the permission for this function.\nPermission Code :- 10811", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    #region MyRegion

                    btnApprove.Text = "Process";
                    setGridColumns(CommonEnum.Customer_warranty_claim_request_approve.ToString());
                    String stage = string.Empty;

                    string[] item = new string[] { "Approve", "Reject" };
                    cmbStatus.DataSource = item;

                    cmbStatus.Enabled = true;
                    btnApprove.Visible = true;

                    //DateTime from, to;

                    //from = dtpFrom.Value.Date;
                    //to = dtpTo.Value.Date;

                    List<Scv_wrrt_App> oItems = CHNLSVC.CustService.GET_SCV_CUST_WRT_CLM(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "P", txtJobNo.Text, from, to);
                    if (oItems.Count > 0)
                    {
                        dgvMainItems.DataSource = new List<Service_Appove_MRN>();
                        dgvMainItems.AutoGenerateColumns = false;
                        dgvMainItems.DataSource = oItems;
                    }
                    else
                    {
                        if (option == 1)
                        {
                            MessageBox.Show("Please select a correct job number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        clearScreen();
                        txtJobNo.Clear();
                        txtJobNo.Focus();
                        lblFunctionName.Text = lstItems.SelectedItems[0].Text;
                        return;
                    }

                    #endregion MyRegion
                }
                else if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.Cancel_appvoed_customer_warranty_clain_request).ToUpper())
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10812))
                    {
                        MessageBox.Show("You don't have the permission for this function.\nPermission Code :- 10812", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    #region MyRegion

                    btnApprove.Text = "Cancel";
                    setGridColumns(CommonEnum.Customer_warranty_claim_request_approve.ToString());
                    String stage = string.Empty;

                    string[] item = new string[] { "Cancel" };
                    cmbStatus.DataSource = item;

                    cmbStatus.Enabled = true;
                    btnApprove.Visible = true;

                    //DateTime from, to;

                    //from = dtpFrom.Value.Date;
                    //to = dtpTo.Value.Date;

                    List<Scv_wrrt_App> oItems = CHNLSVC.CustService.GET_SCV_CUST_WRT_CLM(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "P,A", txtJobNo.Text, from, to);
                    if (oItems.Count > 0)
                    {
                        dgvMainItems.DataSource = new List<Service_Appove_MRN>();
                        dgvMainItems.AutoGenerateColumns = false;
                        dgvMainItems.DataSource = oItems;
                    }
                    else
                    {
                        if (option == 1)
                        {
                            MessageBox.Show("Please select a correct job number.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        clearScreen();
                        txtJobNo.Clear();
                        txtJobNo.Focus();
                        lblFunctionName.Text = lstItems.SelectedItems[0].Text;
                        return;
                    }

                    #endregion MyRegion
                }
                else if (lstItems.SelectedItems[0].Text.ToUpper() == GetEnumDesc.GetEnumDescription(CommonEnum.Discount_approval_separately_for_each_job).ToUpper())
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10813))
                    {
                        MessageBox.Show("You don't have the permission for this function.\nPermission Code :- 10813", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    #region MyRegion

                    btnApprove.Text = "Save";
                    dgvMainItems.Size = new System.Drawing.Size(707, 244);
                    pnlDiscount.Visible = true;

                    setGridColumns("Discount Approval Separately for each job");
                    List<ComboBoxObject> oItems = CHNLSVC.CustService.GET_INV_TYPES(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
                    cmbInvoType.DataSource = oItems.FindAll(x => x.Text != "HS");
                    cmbInvoType.DisplayMember = "Text";
                    cmbInvoType.ValueMember = "Value";

                    string stage = "2,2.1,3,5,5.1,4,4.1,3,6,6.1,7";

                    if (!String.IsNullOrEmpty(txtJobNo.Text))
                    {
                        if (option == 1)
                        {
                            DataTable DtDetails = new DataTable();
                            DtDetails = CHNLSVC.CustService.GetTechAllocJobs(BaseCls.GlbUserComCode, DateTime.MinValue, DateTime.MaxValue, txtJobNo.Text, stage, 0, string.Empty, BaseCls.GlbUserDefProf);
                            dgvMainItems.DataSource = DtDetails;
                        }
                        else
                        {
                            DataTable DtDetails = new DataTable();
                            DtDetails = CHNLSVC.CustService.GetTechAllocJobs(BaseCls.GlbUserComCode, DateTime.MinValue, DateTime.MinValue, txtJobNo.Text, "", -1, string.Empty, BaseCls.GlbUserDefProf);
                            dgvMainItems.DataSource = DtDetails;
                        }
                        return;
                    }
                    else
                    {
                        GetAllJobDetails();
                    }

                    #endregion MyRegion
                }
            }
            else
            {
                //   MessageBox.Show("Please select a function from the list", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void setGridColumns(string itemName)
        {
            dgvMainItems.Columns.Clear();
            DataGridViewCheckBoxColumn select = new DataGridViewCheckBoxColumn();
            select.HeaderText = "  ";
            dgvMainItems.Columns.Insert(0, select);
            dgvMainItems.Columns[0].Width = 30;

            switch (itemName)
            {
                case "Job Cancel":

                    #region MyRegion

                    dgvMainItems.Columns.Add("JBD_SEQ_NO", "JBD_SEQ_NO");
                    dgvMainItems.Columns.Add("JBD_JOBNO", "Job Number");
                    dgvMainItems.Columns.Add("JBD_JOBLINE", "JBD_JOBLINE");
                    dgvMainItems.Columns.Add("B.SJB_DT", "Date");
                    dgvMainItems.Columns.Add("JBD_ITM_CD", "Item Code");
                    dgvMainItems.Columns.Add("JBD_SER1", SerialName);
                    dgvMainItems.Columns.Add("JBD_WARR", "JBD_WARR");
                    dgvMainItems.Columns.Add("JBD_REGNO", "JBD_REGNO");
                    dgvMainItems.Columns.Add("SJB_TOWN", "SJB_TOWN");
                    dgvMainItems.Columns.Add("SC_DIRECT", "SC_DIRECT");
                    dgvMainItems.Columns.Add("SJB_PRORITY", "SJB_PRORITY");
                    dgvMainItems.Columns.Add("SJB_CUSTEXPTDT", "SJB_CUSTEXPTDT");
                    dgvMainItems.Columns.Add("SC_DESC", "SC_DESC");
                    dgvMainItems.Columns.Add("Status", "Status");
                    dgvMainItems.Columns.Add("SJB_CUST_CD", "SJB_CUST_CD");
                    dgvMainItems.Columns.Add("SJB_CUST_NAME", "Customer");

                    dgvMainItems.Columns[0].DataPropertyName = "";

                    dgvMainItems.Columns["JBD_SEQ_NO"].DataPropertyName = "JBD_SEQ_NO";
                    dgvMainItems.Columns["JBD_JOBNO"].DataPropertyName = "JBD_JOBNO";
                    dgvMainItems.Columns["JBD_JOBLINE"].DataPropertyName = "JBD_JOBLINE";
                    dgvMainItems.Columns["B.SJB_DT"].DataPropertyName = "SJB_DT";
                    dgvMainItems.Columns["JBD_ITM_CD"].DataPropertyName = "JBD_ITM_CD";
                    dgvMainItems.Columns["JBD_SER1"].DataPropertyName = "JBD_SER1";
                    dgvMainItems.Columns["JBD_WARR"].DataPropertyName = "JBD_WARR";
                    dgvMainItems.Columns["JBD_REGNO"].DataPropertyName = "JBD_REGNO";
                    dgvMainItems.Columns["SJB_TOWN"].DataPropertyName = "SJB_TOWN";
                    dgvMainItems.Columns["SC_DIRECT"].DataPropertyName = "SC_DIRECT";
                    dgvMainItems.Columns["SJB_PRORITY"].DataPropertyName = "SJB_PRORITY";
                    dgvMainItems.Columns["SJB_CUSTEXPTDT"].DataPropertyName = "SJB_CUSTEXPTDT";
                    dgvMainItems.Columns["SC_DESC"].DataPropertyName = "SC_DESC";
                    dgvMainItems.Columns["Status"].DataPropertyName = "Status";
                    dgvMainItems.Columns["SJB_CUST_CD"].DataPropertyName = "SJB_CUST_CD";
                    dgvMainItems.Columns["SJB_CUST_NAME"].DataPropertyName = "SJB_CUST_NAME";

                    dgvMainItems.Columns["JBD_SEQ_NO"].Visible = false;
                    //dgvMainItems.Columns["JBD_JOBNO"].Visible = false;

                    dgvMainItems.Columns["JBD_JOBLINE"].Visible = false;

                    //  dgvMainItems.Columns["B.SJB_DT"].Visible = false;
                    //   dgvMainItems.Columns["JBD_ITM_CD"].Visible = false;
                    // dgvMainItems.Columns["JBD_SER1"].Visible = false;
                    dgvMainItems.Columns["JBD_WARR"].Visible = false;
                    dgvMainItems.Columns["JBD_REGNO"].Visible = false;
                    dgvMainItems.Columns["SJB_TOWN"].Visible = false;
                    dgvMainItems.Columns["SC_DIRECT"].Visible = false;
                    dgvMainItems.Columns["SJB_PRORITY"].Visible = false;
                    dgvMainItems.Columns["SJB_CUSTEXPTDT"].Visible = false;
                    dgvMainItems.Columns["SC_DESC"].Visible = false;
                    dgvMainItems.Columns["Status"].Visible = false;
                    dgvMainItems.Columns["SJB_CUST_CD"].Visible = false;
                    //    dgvMainItems.Columns["SJB_CUST_NAME"].Visible = false;

                    dgvMainItems.Columns["JBD_JOBNO"].ReadOnly = true;
                    dgvMainItems.Columns["B.SJB_DT"].ReadOnly = true;
                    dgvMainItems.Columns["JBD_ITM_CD"].ReadOnly = true;
                    dgvMainItems.Columns["JBD_SER1"].ReadOnly = true;
                    dgvMainItems.Columns["SJB_CUST_NAME"].ReadOnly = true;

                    dgvMainItems.Columns["JBD_JOBNO"].Width = 150;
                    dgvMainItems.Columns["SJB_CUST_NAME"].Width = 250;
                    dgvMainItems.Columns[0].Width = 30;

                    #endregion MyRegion

                    break;

                case "Warranty Update":

                    #region MyRegion

                    dgvMainItems.Columns.Add("JBD_SEQ_NO", "JBD_SEQ_NO");
                    dgvMainItems.Columns.Add("JBD_JOBNO", "Job Number");
                    dgvMainItems.Columns.Add("JBD_JOBLINE", "JBD_JOBLINE");
                    dgvMainItems.Columns.Add("B.SJB_DT", "Date");
                    dgvMainItems.Columns.Add("JBD_ITM_CD", "Item Code");
                    dgvMainItems.Columns.Add("JBD_SER1", SerialName);
                    dgvMainItems.Columns.Add("JBD_WARR", "JBD_WARR");

                    dgvMainItems.Columns.Add("ADDITIONAL1", "Warr. Status");

                    dgvMainItems.Columns.Add("JBD_REGNO", "JBD_REGNO");
                    dgvMainItems.Columns.Add("SJB_TOWN", "SJB_TOWN");
                    dgvMainItems.Columns.Add("SC_DIRECT", "SC_DIRECT");
                    dgvMainItems.Columns.Add("SJB_PRORITY", "SJB_PRORITY");
                    dgvMainItems.Columns.Add("SJB_CUSTEXPTDT", "SJB_CUSTEXPTDT");
                    dgvMainItems.Columns.Add("SC_DESC", "SC_DESC");
                    dgvMainItems.Columns.Add("Status", "Status");
                    dgvMainItems.Columns.Add("SJB_CUST_CD", "SJB_CUST_CD");
                    dgvMainItems.Columns.Add("SJB_CUST_NAME", "Customer");

                    dgvMainItems.Columns[0].DataPropertyName = "";

                    dgvMainItems.Columns["JBD_SEQ_NO"].DataPropertyName = "JBD_SEQ_NO";
                    dgvMainItems.Columns["JBD_JOBNO"].DataPropertyName = "JBD_JOBNO";
                    dgvMainItems.Columns["JBD_JOBLINE"].DataPropertyName = "JBD_JOBLINE";
                    dgvMainItems.Columns["B.SJB_DT"].DataPropertyName = "SJB_DT";
                    dgvMainItems.Columns["JBD_ITM_CD"].DataPropertyName = "JBD_ITM_CD";
                    dgvMainItems.Columns["JBD_SER1"].DataPropertyName = "JBD_SER1";
                    dgvMainItems.Columns["JBD_WARR"].DataPropertyName = "JBD_WARR";
                    dgvMainItems.Columns["JBD_REGNO"].DataPropertyName = "JBD_REGNO";
                    dgvMainItems.Columns["SJB_TOWN"].DataPropertyName = "SJB_TOWN";
                    dgvMainItems.Columns["SC_DIRECT"].DataPropertyName = "SC_DIRECT";
                    dgvMainItems.Columns["SJB_PRORITY"].DataPropertyName = "SJB_PRORITY";
                    dgvMainItems.Columns["SJB_CUSTEXPTDT"].DataPropertyName = "SJB_CUSTEXPTDT";
                    dgvMainItems.Columns["SC_DESC"].DataPropertyName = "SC_DESC";
                    dgvMainItems.Columns["Status"].DataPropertyName = "Status";
                    dgvMainItems.Columns["SJB_CUST_CD"].DataPropertyName = "SJB_CUST_CD";
                    dgvMainItems.Columns["SJB_CUST_NAME"].DataPropertyName = "SJB_CUST_NAME";
                    dgvMainItems.Columns["ADDITIONAL1"].DataPropertyName = "ADDITIONAL1";

                    dgvMainItems.Columns["JBD_SEQ_NO"].Visible = false;

                    dgvMainItems.Columns["JBD_JOBLINE"].Visible = false;

                    dgvMainItems.Columns["JBD_WARR"].Visible = false;
                    dgvMainItems.Columns["JBD_REGNO"].Visible = false;
                    dgvMainItems.Columns["SJB_TOWN"].Visible = false;
                    dgvMainItems.Columns["SC_DIRECT"].Visible = false;
                    dgvMainItems.Columns["SJB_PRORITY"].Visible = false;
                    dgvMainItems.Columns["SJB_CUSTEXPTDT"].Visible = false;
                    dgvMainItems.Columns["SC_DESC"].Visible = false;
                    dgvMainItems.Columns["Status"].Visible = false;
                    dgvMainItems.Columns["SJB_CUST_CD"].Visible = false;

                    dgvMainItems.Columns["JBD_JOBNO"].ReadOnly = true;
                    dgvMainItems.Columns["B.SJB_DT"].ReadOnly = true;
                    dgvMainItems.Columns["JBD_ITM_CD"].ReadOnly = true;
                    dgvMainItems.Columns["JBD_SER1"].ReadOnly = true;
                    dgvMainItems.Columns["SJB_CUST_NAME"].ReadOnly = true;
                    dgvMainItems.Columns["ADDITIONAL1"].ReadOnly = true;

                    dgvMainItems.Columns["JBD_JOBNO"].Width = 150;
                    dgvMainItems.Columns["SJB_CUST_NAME"].Width = 250;
                    dgvMainItems.Columns[0].Width = 30;

                    #endregion MyRegion

                    break;

                case "Job confirmation cancel":

                    #region MyRegion

                    dgvMainItems.Columns.Add("JBD_SEQ_NO", "JBD_SEQ_NO");

                    dgvMainItems.Columns.Add("Additional1", "Confirmation No");

                    dgvMainItems.Columns.Add("JBD_JOBNO", "Job Number");
                    dgvMainItems.Columns.Add("JBD_JOBLINE", "JBD_JOBLINE");
                    dgvMainItems.Columns.Add("B.SJB_DT", "Date");
                    dgvMainItems.Columns.Add("JBD_ITM_CD", "Item Code");
                    dgvMainItems.Columns.Add("JBD_SER1", SerialName);
                    dgvMainItems.Columns.Add("JBD_WARR", "JBD_WARR");
                    dgvMainItems.Columns.Add("JBD_REGNO", "JBD_REGNO");
                    dgvMainItems.Columns.Add("SJB_TOWN", "SJB_TOWN");
                    dgvMainItems.Columns.Add("SC_DIRECT", "SC_DIRECT");
                    dgvMainItems.Columns.Add("SJB_PRORITY", "SJB_PRORITY");
                    dgvMainItems.Columns.Add("SJB_CUSTEXPTDT", "SJB_CUSTEXPTDT");
                    dgvMainItems.Columns.Add("SC_DESC", "SC_DESC");
                    dgvMainItems.Columns.Add("Status", "Status");
                    dgvMainItems.Columns.Add("SJB_CUST_CD", "SJB_CUST_CD");
                    dgvMainItems.Columns.Add("SJB_CUST_NAME", "Customer");

                    dgvMainItems.Columns[0].DataPropertyName = "";

                    dgvMainItems.Columns["JBD_SEQ_NO"].DataPropertyName = "JBD_SEQ_NO";

                    dgvMainItems.Columns["Additional1"].DataPropertyName = "Additional1";

                    dgvMainItems.Columns["JBD_JOBNO"].DataPropertyName = "JBD_JOBNO";
                    dgvMainItems.Columns["JBD_JOBLINE"].DataPropertyName = "JBD_JOBLINE";
                    dgvMainItems.Columns["B.SJB_DT"].DataPropertyName = "SJB_DT";
                    dgvMainItems.Columns["JBD_ITM_CD"].DataPropertyName = "JBD_ITM_CD";
                    dgvMainItems.Columns["JBD_SER1"].DataPropertyName = "JBD_SER1";
                    dgvMainItems.Columns["JBD_WARR"].DataPropertyName = "JBD_WARR";
                    dgvMainItems.Columns["JBD_REGNO"].DataPropertyName = "JBD_REGNO";
                    dgvMainItems.Columns["SJB_TOWN"].DataPropertyName = "SJB_TOWN";
                    dgvMainItems.Columns["SC_DIRECT"].DataPropertyName = "SC_DIRECT";
                    dgvMainItems.Columns["SJB_PRORITY"].DataPropertyName = "SJB_PRORITY";
                    dgvMainItems.Columns["SJB_CUSTEXPTDT"].DataPropertyName = "SJB_CUSTEXPTDT";
                    dgvMainItems.Columns["SC_DESC"].DataPropertyName = "SC_DESC";
                    dgvMainItems.Columns["Status"].DataPropertyName = "Status";
                    dgvMainItems.Columns["SJB_CUST_CD"].DataPropertyName = "SJB_CUST_CD";
                    dgvMainItems.Columns["SJB_CUST_NAME"].DataPropertyName = "SJB_CUST_NAME";

                    dgvMainItems.Columns["JBD_SEQ_NO"].Visible = false;
                    //dgvMainItems.Columns["JBD_JOBNO"].Visible = false;
                    dgvMainItems.Columns["JBD_JOBLINE"].Visible = false;
                    //dgvMainItems.Columns["B.SJB_DT"].Visible = false;
                    //dgvMainItems.Columns["JBD_ITM_CD"].Visible = false;
                    //dgvMainItems.Columns["JBD_SER1"].Visible = false;
                    dgvMainItems.Columns["JBD_WARR"].Visible = false;
                    dgvMainItems.Columns["JBD_REGNO"].Visible = false;
                    dgvMainItems.Columns["SJB_TOWN"].Visible = false;
                    dgvMainItems.Columns["SC_DIRECT"].Visible = false;
                    dgvMainItems.Columns["SJB_PRORITY"].Visible = false;
                    dgvMainItems.Columns["SJB_CUSTEXPTDT"].Visible = false;
                    dgvMainItems.Columns["SC_DESC"].Visible = false;
                    dgvMainItems.Columns["Status"].Visible = false;
                    dgvMainItems.Columns["SJB_CUST_CD"].Visible = false;
                    //dgvMainItems.Columns["SJB_CUST_NAME"].Visible = false;

                    dgvMainItems.Columns["JBD_JOBNO"].ReadOnly = true;
                    dgvMainItems.Columns["B.SJB_DT"].ReadOnly = true;
                    dgvMainItems.Columns["JBD_ITM_CD"].ReadOnly = true;
                    dgvMainItems.Columns["JBD_SER1"].ReadOnly = true;
                    dgvMainItems.Columns["SJB_CUST_NAME"].ReadOnly = true;
                    dgvMainItems.Columns["Additional1"].ReadOnly = true;

                    dgvMainItems.Columns["JBD_JOBNO"].Width = 150;
                    dgvMainItems.Columns["SJB_CUST_NAME"].Width = 250;
                    dgvMainItems.Columns[0].Width = 30;
                    dgvMainItems.Columns["Additional1"].Width = 150;

                    #endregion MyRegion

                    break;

                case "Job_hold_and_re_open":

                    #region MyRegion

                    dgvMainItems.Columns.Add("JBD_SEQ_NO", "JBD_SEQ_NO");
                    dgvMainItems.Columns.Add("Additional1", "Stage");
                    dgvMainItems.Columns.Add("JBD_JOBNO", "Job Number");
                    dgvMainItems.Columns.Add("JBD_JOBLINE", "JBD_JOBLINE");
                    dgvMainItems.Columns.Add("B.SJB_DT", "Date");
                    dgvMainItems.Columns.Add("JBD_ITM_CD", "Item Code");
                    dgvMainItems.Columns.Add("JBD_SER1", SerialName);
                    dgvMainItems.Columns.Add("JBD_WARR", "JBD_WARR");
                    dgvMainItems.Columns.Add("JBD_REGNO", "JBD_REGNO");
                    dgvMainItems.Columns.Add("SJB_TOWN", "SJB_TOWN");
                    dgvMainItems.Columns.Add("SC_DIRECT", "SC_DIRECT");
                    dgvMainItems.Columns.Add("SJB_PRORITY", "SJB_PRORITY");
                    dgvMainItems.Columns.Add("SJB_CUSTEXPTDT", "SJB_CUSTEXPTDT");
                    dgvMainItems.Columns.Add("SC_DESC", "SC_DESC");
                    dgvMainItems.Columns.Add("Status", "Status");
                    dgvMainItems.Columns.Add("SJB_CUST_CD", "SJB_CUST_CD");
                    dgvMainItems.Columns.Add("SJB_CUST_NAME", "Customer");

                    dgvMainItems.Columns[0].DataPropertyName = "";

                    dgvMainItems.Columns["JBD_SEQ_NO"].DataPropertyName = "JBD_SEQ_NO";
                    dgvMainItems.Columns["Additional1"].DataPropertyName = "Additional1";
                    dgvMainItems.Columns["JBD_JOBNO"].DataPropertyName = "JBD_JOBNO";
                    dgvMainItems.Columns["JBD_JOBLINE"].DataPropertyName = "JBD_JOBLINE";
                    dgvMainItems.Columns["B.SJB_DT"].DataPropertyName = "SJB_DT";
                    dgvMainItems.Columns["JBD_ITM_CD"].DataPropertyName = "JBD_ITM_CD";
                    dgvMainItems.Columns["JBD_SER1"].DataPropertyName = "JBD_SER1";
                    dgvMainItems.Columns["JBD_WARR"].DataPropertyName = "JBD_WARR";
                    dgvMainItems.Columns["JBD_REGNO"].DataPropertyName = "JBD_REGNO";
                    dgvMainItems.Columns["SJB_TOWN"].DataPropertyName = "SJB_TOWN";
                    dgvMainItems.Columns["SC_DIRECT"].DataPropertyName = "SC_DIRECT";
                    dgvMainItems.Columns["SJB_PRORITY"].DataPropertyName = "SJB_PRORITY";
                    dgvMainItems.Columns["SJB_CUSTEXPTDT"].DataPropertyName = "SJB_CUSTEXPTDT";
                    dgvMainItems.Columns["SC_DESC"].DataPropertyName = "SC_DESC";
                    dgvMainItems.Columns["Status"].DataPropertyName = "Status";
                    dgvMainItems.Columns["SJB_CUST_CD"].DataPropertyName = "SJB_CUST_CD";
                    dgvMainItems.Columns["SJB_CUST_NAME"].DataPropertyName = "SJB_CUST_NAME";

                    dgvMainItems.Columns["JBD_SEQ_NO"].Visible = false;
                    //dgvMainItems.Columns["JBD_JOBNO"].Visible = false;
                    dgvMainItems.Columns["JBD_JOBLINE"].Visible = false;
                    //dgvMainItems.Columns["B.SJB_DT"].Visible = false;
                    //dgvMainItems.Columns["JBD_ITM_CD"].Visible = false;
                    //dgvMainItems.Columns["JBD_SER1"].Visible = false;
                    dgvMainItems.Columns["JBD_WARR"].Visible = false;
                    dgvMainItems.Columns["JBD_REGNO"].Visible = false;
                    dgvMainItems.Columns["SJB_TOWN"].Visible = false;
                    dgvMainItems.Columns["SC_DIRECT"].Visible = false;
                    dgvMainItems.Columns["SJB_PRORITY"].Visible = false;
                    dgvMainItems.Columns["SJB_CUSTEXPTDT"].Visible = false;
                    dgvMainItems.Columns["SC_DESC"].Visible = false;
                    dgvMainItems.Columns["Status"].Visible = false;
                    dgvMainItems.Columns["SJB_CUST_CD"].Visible = false;
                    //dgvMainItems.Columns["SJB_CUST_NAME"].Visible = false;

                    dgvMainItems.Columns["JBD_JOBNO"].ReadOnly = true;
                    dgvMainItems.Columns["B.SJB_DT"].ReadOnly = true;
                    dgvMainItems.Columns["JBD_ITM_CD"].ReadOnly = true;
                    dgvMainItems.Columns["JBD_SER1"].ReadOnly = true;
                    dgvMainItems.Columns["SJB_CUST_NAME"].ReadOnly = true;
                    dgvMainItems.Columns["Additional1"].ReadOnly = true;

                    dgvMainItems.Columns["JBD_JOBNO"].Width = 150;
                    dgvMainItems.Columns["SJB_CUST_NAME"].Width = 250;
                    dgvMainItems.Columns[0].Width = 30;
                    dgvMainItems.Columns["Additional1"].Width = 200;

                    #endregion MyRegion

                    break;

                case "Job_estimate_approval":

                    #region MyRegion

                    dgvMainItems.Columns.Add("ESH_SEQ_NO", "ESH_SEQ_NO");
                    dgvMainItems.Columns.Add("ESH_ESTNO", "Estimate Num");
                    dgvMainItems.Columns.Add("ESH_DT", "Date");
                    dgvMainItems.Columns.Add("ESH_JOB_NO", "Job Num");
                    dgvMainItems.Columns.Add("EST_STUS", "Status");
                    dgvMainItems.Columns.Add("EST_RMK", "Remark");
                    dgvMainItems.Columns.Add("EST_CUST_CD", "Status");

                    dgvMainItems.Columns[0].DataPropertyName = "";
                    dgvMainItems.Columns["ESH_SEQ_NO"].DataPropertyName = "ESH_SEQ_NO";
                    dgvMainItems.Columns["ESH_ESTNO"].DataPropertyName = "ESH_ESTNO";
                    dgvMainItems.Columns["ESH_DT"].DataPropertyName = "ESH_DT";
                    dgvMainItems.Columns["ESH_JOB_NO"].DataPropertyName = "ESH_JOB_NO";
                    dgvMainItems.Columns["EST_STUS"].DataPropertyName = "EST_STUS";
                    dgvMainItems.Columns["EST_RMK"].DataPropertyName = "EST_RMK";
                    dgvMainItems.Columns["EST_CUST_CD"].DataPropertyName = "EST_CUST_CD";

                    dgvMainItems.Columns["ESH_SEQ_NO"].Visible = false;
                    dgvMainItems.Columns["EST_STUS"].Visible = false;

                    dgvMainItems.Columns[0].Width = 30;
                    dgvMainItems.Columns["ESH_ESTNO"].Width = 150;
                    dgvMainItems.Columns["EST_RMK"].Width = 150;

                    dgvMainItems.Columns["ESH_SEQ_NO"].ReadOnly = true;
                    dgvMainItems.Columns["ESH_ESTNO"].ReadOnly = true;
                    dgvMainItems.Columns["ESH_DT"].ReadOnly = true;
                    dgvMainItems.Columns["ESH_JOB_NO"].ReadOnly = true;
                    dgvMainItems.Columns["EST_STUS"].ReadOnly = true;
                    dgvMainItems.Columns["EST_RMK"].ReadOnly = true;
                    dgvMainItems.Columns["EST_CUST_CD"].ReadOnly = true;

                    #endregion MyRegion

                    break;

                case "MRN Approve":

                    #region MyRegion

                    dgvMainItems.Columns.Add("ITR_SEQ_NO", "ITR_SEQ_NO");
                    dgvMainItems.Columns.Add("ITR_JOB_NO", "Job Number");
                    dgvMainItems.Columns.Add("ITR_JOB_LINE", "Job Line");
                    dgvMainItems.Columns.Add("ITR_REQ_NO", "MRN Number");
                    dgvMainItems.Columns.Add("ITR_DT", "Date");
                    dgvMainItems.Columns.Add("ITR_REF", "Manual Ref.");
                    dgvMainItems.Columns.Add("STATUS", "Status");
                    dgvMainItems.Columns.Add("ITR_ISSUE_FROM", "Issue From");

                    dgvMainItems.Columns[0].DataPropertyName = "";
                    dgvMainItems.Columns["ITR_SEQ_NO"].DataPropertyName = "ITR_SEQ_NO";
                    dgvMainItems.Columns["ITR_JOB_NO"].DataPropertyName = "ITR_JOB_NO";
                    dgvMainItems.Columns["ITR_JOB_LINE"].DataPropertyName = "ITR_JOB_LINE";
                    dgvMainItems.Columns["ITR_REQ_NO"].DataPropertyName = "ITR_REQ_NO";
                    dgvMainItems.Columns["ITR_DT"].DataPropertyName = "ITR_DT";
                    dgvMainItems.Columns["ITR_REF"].DataPropertyName = "ITR_REF";
                    dgvMainItems.Columns["STATUS"].DataPropertyName = "STATUS";
                    dgvMainItems.Columns["ITR_ISSUE_FROM"].DataPropertyName = "ITR_ISSUE_FROM";

                    dgvMainItems.Columns["ITR_SEQ_NO"].ReadOnly = true;
                    dgvMainItems.Columns["ITR_JOB_NO"].ReadOnly = true;
                    dgvMainItems.Columns["ITR_JOB_LINE"].ReadOnly = true;
                    dgvMainItems.Columns["ITR_REQ_NO"].ReadOnly = true;
                    dgvMainItems.Columns["ITR_DT"].ReadOnly = true;
                    dgvMainItems.Columns["ITR_REF"].ReadOnly = true;
                    dgvMainItems.Columns["STATUS"].ReadOnly = true;
                    dgvMainItems.Columns["ITR_ISSUE_FROM"].ReadOnly = true;

                    dgvMainItems.Columns["ITR_SEQ_NO"].Visible = false;
                    dgvMainItems.Columns["ITR_JOB_LINE"].Visible = false;

                    dgvMainItems.Columns["ITR_JOB_NO"].Width = 130;
                    dgvMainItems.Columns["ITR_REQ_NO"].Width = 130;

                    #endregion MyRegion

                    break;

                case "Customer_warranty_claim_request_approve":

                    #region MyRegion

                    dgvMainItems.Columns.Add("Insa_dt", "Date");
                    dgvMainItems.Columns.Add("Insa_jb_no", "Job Num");
                    dgvMainItems.Columns.Add("Insa_manual_ref", "Manual Ref");
                    dgvMainItems.Columns.Add("Insa_tp", "Type");
                    dgvMainItems.Columns.Add("Insa_inv_no", "Invoice Num");
                    dgvMainItems.Columns.Add("Insa_warr", "Warranty");
                    dgvMainItems.Columns.Add("Insa_cust_name", "Customer Name");
                    dgvMainItems.Columns.Add("Insa_itm", "Item");
                    dgvMainItems.Columns.Add("Insa_ser", SerialName);
                    dgvMainItems.Columns.Add("Insa_jb_rem", "Remark");
                    dgvMainItems.Columns.Add("Insa_stus", "Status");
                    dgvMainItems.Columns.Add("Insa_anal4", "Insa_anal4");

                    dgvMainItems.Columns[0].DataPropertyName = "";
                    dgvMainItems.Columns["Insa_dt"].DataPropertyName = "Insa_dt";
                    dgvMainItems.Columns["Insa_manual_ref"].DataPropertyName = "Insa_manual_ref";
                    dgvMainItems.Columns["Insa_tp"].DataPropertyName = "Insa_tp";
                    dgvMainItems.Columns["Insa_inv_no"].DataPropertyName = "Insa_inv_no";
                    dgvMainItems.Columns["Insa_warr"].DataPropertyName = "Insa_warr";
                    dgvMainItems.Columns["Insa_cust_name"].DataPropertyName = "Insa_cust_name";
                    dgvMainItems.Columns["Insa_itm"].DataPropertyName = "Insa_itm";
                    dgvMainItems.Columns["Insa_ser"].DataPropertyName = "Insa_ser";
                    dgvMainItems.Columns["Insa_jb_rem"].DataPropertyName = "Insa_jb_rem";
                    dgvMainItems.Columns["Insa_stus"].DataPropertyName = "Insa_stus";
                    dgvMainItems.Columns["Insa_jb_no"].DataPropertyName = "Insa_jb_no";
                    dgvMainItems.Columns["Insa_anal4"].DataPropertyName = "Insa_anal4";

                    dgvMainItems.Columns["Insa_dt"].ReadOnly = true;
                    dgvMainItems.Columns["Insa_manual_ref"].ReadOnly = true;
                    dgvMainItems.Columns["Insa_tp"].ReadOnly = true;
                    dgvMainItems.Columns["Insa_inv_no"].ReadOnly = true;
                    dgvMainItems.Columns["Insa_warr"].ReadOnly = false;
                    dgvMainItems.Columns["Insa_cust_name"].ReadOnly = true;
                    dgvMainItems.Columns["Insa_itm"].ReadOnly = true;
                    dgvMainItems.Columns["Insa_ser"].ReadOnly = true;
                    dgvMainItems.Columns["Insa_jb_rem"].ReadOnly = true;
                    dgvMainItems.Columns["Insa_stus"].ReadOnly = true;
                    dgvMainItems.Columns["Insa_jb_no"].ReadOnly = true;

                    dgvMainItems.Columns["Insa_anal4"].Visible = false;

                    #endregion MyRegion

                    break;

                case "Discount Approval Separately for each job":

                    #region MyRegion

                    dgvMainItems.Columns.Add("JobNo", "JobNo");
                    dgvMainItems.Columns.Add("JobLine", "JobLine");
                    dgvMainItems.Columns.Add("Date", "Date");
                    dgvMainItems.Columns.Add("ItemCode", "ItemCode");
                    dgvMainItems.Columns.Add("SerialNo", SerialName);
                    dgvMainItems.Columns.Add("WarrentyNo", "WarrentyNo");
                    dgvMainItems.Columns.Add("sjb_prority", "sjb_prority");
                    dgvMainItems.Columns.Add("SJB_TOWN", "SJB_TOWN");
                    dgvMainItems.Columns.Add("sc_desc", "sc_desc");
                    dgvMainItems.Columns.Add("sc_direct", "sc_direct");
                    dgvMainItems.Columns.Add("sc_tp", "sc_tp");
                    dgvMainItems.Columns.Add("sjb_custexptdt", "sjb_custexptdt");

                    dgvMainItems.Columns[0].DataPropertyName = "";
                    dgvMainItems.Columns["JobNo"].DataPropertyName = "jbd_jobno";
                    dgvMainItems.Columns["JobLine"].DataPropertyName = "jbd_jobline";
                    dgvMainItems.Columns["Date"].DataPropertyName = "sjb_dt";
                    dgvMainItems.Columns["ItemCode"].DataPropertyName = "jbd_itm_cd";
                    dgvMainItems.Columns["SerialNo"].DataPropertyName = "jbd_ser1";
                    dgvMainItems.Columns["WarrentyNo"].DataPropertyName = "jbd_warr";
                    dgvMainItems.Columns["sjb_prority"].DataPropertyName = "sjb_prority";
                    dgvMainItems.Columns["SJB_TOWN"].DataPropertyName = "SJB_TOWN";
                    dgvMainItems.Columns["sc_desc"].DataPropertyName = "sc_desc";
                    dgvMainItems.Columns["sc_direct"].DataPropertyName = "sc_direct";
                    dgvMainItems.Columns["sc_tp"].DataPropertyName = "sc_tp";
                    dgvMainItems.Columns["sjb_custexptdt"].DataPropertyName = "sjb_custexptdt";

                    dgvMainItems.Columns["JobNo"].ReadOnly = true;
                    dgvMainItems.Columns["JobLine"].ReadOnly = true;
                    dgvMainItems.Columns["Date"].ReadOnly = true;
                    dgvMainItems.Columns["ItemCode"].ReadOnly = true;
                    dgvMainItems.Columns["SerialNo"].ReadOnly = true;
                    dgvMainItems.Columns["WarrentyNo"].ReadOnly = true;
                    dgvMainItems.Columns["sjb_prority"].ReadOnly = true;
                    dgvMainItems.Columns["SJB_TOWN"].ReadOnly = true;
                    dgvMainItems.Columns["sc_desc"].ReadOnly = true;
                    dgvMainItems.Columns["sc_direct"].ReadOnly = true;
                    dgvMainItems.Columns["sc_tp"].ReadOnly = true;
                    dgvMainItems.Columns["sjb_custexptdt"].ReadOnly = true;

                    dgvMainItems.Columns["SJB_TOWN"].Visible = false;
                    dgvMainItems.Columns["sc_desc"].Visible = false;
                    dgvMainItems.Columns["sc_direct"].Visible = false;
                    dgvMainItems.Columns["sc_tp"].Visible = false;
                    dgvMainItems.Columns["sjb_prority"].Visible = false;
                    dgvMainItems.Columns["sjb_custexptdt"].Visible = false;

                    #endregion MyRegion

                    break;
            }
        }

        private bool isAnySelected()
        {
            bool status = false;

            if (dgvMainItems.Rows.Count > 0)
            {
                for (int i = 0; i < dgvMainItems.Rows.Count; i++)
                {
                    if (dgvMainItems.Rows[i].Cells[0].Value != null && Convert.ToBoolean(dgvMainItems.Rows[i].Cells[0].Value) == true)
                    {
                        status = true;
                        break;
                    }
                }
            }
            return status;
        }

        private bool LoadPriceBook(string _invoiceType)
        {
            bool _isAvailable = false;
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _books = _PriceDefinitionRef.Where(x => x.Sadd_doc_tp == _invoiceType).Select(x => x.Sadd_pb).Distinct().ToList();
                    _books.Add("");
                    cmbBook.DataSource = _books;
                    cmbBook.SelectedIndex = cmbBook.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultBook))
                        cmbBook.Text = DefaultBook;

                    cmbBook.Focus();
                }
                else
                    cmbBook.DataSource = null;
            else
                cmbBook.DataSource = null;

            return _isAvailable;
        }

        private bool LoadPriceLevel(string _invoiceType, string _book)
        {
            bool _isAvailable = false;
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _levels = _PriceDefinitionRef.Where(x => x.Sadd_doc_tp == _invoiceType && x.Sadd_pb == _book).Select(y => y.Sadd_p_lvl).Distinct().ToList();
                    _levels.Add("");
                    cmbLevel.DataSource = _levels;
                    cmbLevel.SelectedIndex = cmbLevel.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultLevel) && !string.IsNullOrEmpty(cmbBook.Text))
                        cmbLevel.Text = DefaultLevel;
                    _priceBookLevelRefList = CHNLSVC.Sales.GetPriceLevelList(BaseCls.GlbUserComCode, _book.Trim(), cmbLevel.Text.Trim());

                    cmbLevel.Focus();
                }
                else
                    cmbLevel.DataSource = null;
            else cmbLevel.DataSource = null;

            return _isAvailable;
        }

        private void GetAllJobDetails()
        {
            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                String stage = string.Empty;
                Int32 IsCusExpected = 0;

                stage = "2,3,5,3,6,4,12";

                DateTime from, to;

                from = Convert.ToDateTime("01-01-1111");
                to = Convert.ToDateTime("31-12-2999");

                DataTable DtDetails = new DataTable();
                DtDetails = CHNLSVC.CustService.GetTechAllocJobs(BaseCls.GlbUserComCode, from, to, txtJobNo.Text, stage, IsCusExpected, string.Empty, BaseCls.GlbUserDefProf);
                if (DtDetails.Rows.Count > 0)
                {
                    dgvMainItems.DataSource = DtDetails;
                }
                else
                {
                    MessageBox.Show("Please select a correct job no", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //clearScreen();
                    clearScreen();
                    txtJobNo.Clear();
                    txtJobNo.Focus();
                    return;
                }
            }
        }

        private void clearScreen()
        {
            txtRemark.Clear();
            txtJobNo.Clear();
            dtpTo.Value = DateTime.Today;
            dtpFrom.Value = DateTime.Today.AddDays(-30);
            gblJobnumber = string.Empty;
            gblJobLine = -1;
            dgvMainItems.DataSource = new List<Service_Appove>();
            dgvMainItems.AutoGenerateColumns = false;
            btnPrint.Visible = false;

            dtpFrom.Value = DateTime.Now.Date.AddDays(-30);
            dtpTo.Value = DateTime.Now;

            pnlDiscount.Visible = false;
            dgvMainItems.Size = new Size(707, 374);

            _PriceDefinitionRef = CacheLayer.Get<List<PriceDefinitionRef>>(CacheLayer.Key.PriceDefinition.ToString());

            lblFunctionName.Text = "";

            label14.Visible = false;
            txtMRNNumber.Visible = false;
            btnMRNSearch.Visible = false;
            dgvMainItems.Location = new Point(270, 66);
            dgvMainItems.Size = new System.Drawing.Size(707, 244);
            pnlFitler.Size = new Size(713, 37);
        }

        #endregion Methods

        private void dgvMainItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (dgvMainItems.ColumnCount > 0)
                {
                    Int32 _rowIndex = e.RowIndex;
                    Int32 _colIndex = e.ColumnIndex;

                    if (_rowIndex != -1)
                    {
                        #region Copy text

                        string _copyText = dgvMainItems.Rows[_rowIndex].Cells[_colIndex].Value.ToString();
                        Clipboard.SetText(_copyText.ToString());
                        MessageBox.Show(_copyText, "Copy to Clipboard");

                        #endregion Copy text
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel();
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFrom.Value > dtpTo.Value)
            {
                // MessageBox.Show("Please enter a valid date range.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpFrom.Value = dtpTo.Value;
            }
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFrom.Value > dtpTo.Value)
            {
                //  MessageBox.Show("Please enter a valid date range.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpTo.Value = dtpFrom.Value;
            }
        }

        private void txtMRNNumber_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRN);
                DataTable _result = CHNLSVC.CommonSearch.GetSearchMRN(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtMRNNumber;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtMRNNumber.Select();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtMRNNumber_Leave(object sender, EventArgs e)
        {

        }

        private void txtMRNNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtMRNNumber_DoubleClick(null, null);
            }
        }

        private void btnMRNSearch_Click(object sender, EventArgs e)
        {
            txtMRNNumber_DoubleClick(null, null);
        }
    }
}