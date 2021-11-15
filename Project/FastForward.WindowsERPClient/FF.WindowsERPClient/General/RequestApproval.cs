using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.BusinessObjects.Sales;

namespace FF.WindowsERPClient.General
{
    public partial class RequestApproval : Base
    {
        
        DataTable dt;
        DataTable dttemp;
        DataTable dtsearch;
        string txtDiscount = "";
        string txtDiscountValue = "";
        public RequestApproval()
        {
            InitializeComponent();
        }

        private void cmbrequesttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            textSearchLoc.Enabled = true;
            btnSearchLocation.Enabled = true;
            textCustomerCode.Enabled = true;
            btnSearchCusCode.Enabled = true;
            btnSearch.Enabled = true;
            dtdate.Enabled = true;
            checkSelectAll.Enabled = true;
            btnSearch.Enabled = true;
            //btnSearch_Click(sender, e);
           
        }

        private void gridloadDiscntCashCreditSales_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            gridloadcustomerinfo.Columns["cusDisRate"].DefaultCellStyle.Format = "N2";
            gridloadcustomerinfo.Columns["cusDiscValue"].DefaultCellStyle.Format = "N2";
            gridloadcustomerinfo.Columns["cusDisRate"].DefaultCellStyle.BackColor = Color.Gold;
            gridloadcustomerinfo.Columns["cusDiscValue"].DefaultCellStyle.BackColor = Color.Gold;
           string Ref = gridloadDiscntCashCreditSales.Rows[e.RowIndex].Cells["lblRefference"].Value.ToString();
           string SeqNo = gridloadDiscntCashCreditSales.Rows[e.RowIndex].Cells["lblSeqNo"].Value.ToString();
           lblCusName.Text = gridloadDiscntCashCreditSales.Rows[e.RowIndex].Cells["Customer"].Value.ToString();
           lblCustomerCode.Text = gridloadDiscntCashCreditSales.Rows[e.RowIndex].Cells["CusCode"].Value.ToString();
           
           dttemp = new DataTable();
           dttemp= dt.Select("sgdd_req_ref = '" + Ref + "' AND sgdd_seq = '" + SeqNo + "'").CopyToDataTable();
           gridloadcustomerinfo.AutoGenerateColumns = false;
           gridloadcustomerinfo.DataSource = dttemp;
           if (dttemp.Rows[0]["sgdd_disc_val"].ToString() == "0")
           {

               //gridloadcustomerinfo.Rows[0].Cells["cusDiscValue"].ReadOnly = true;
           }
           if (dttemp.Rows[0]["sgdd_disc_rt"].ToString() == "0")
           {

               //gridloadcustomerinfo.Rows[0].Cells["cusDisRate"].ReadOnly = true;
           }
           pnldiscountforcashcredit.Visible = true;
           gridloadDiscntCashCreditSales.Enabled = false;
           lbladdress.Text = dttemp.Rows[0]["address"].ToString();
        }

        //private void buttonApprove_Click(object sender, EventArgs e)
        //{
        //    if (MessageBox.Show("Are you sure,you want to Approve ?", "Approve", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
        //    gridloadcustomerinfo.Columns["cusDisRate"].DefaultCellStyle.BackColor = Color.Gold;
        //    gridloadcustomerinfo.Columns["cusDiscValue"].DefaultCellStyle.BackColor = Color.Gold;
        //    Boolean validate = checkValidation();
        //    if (validate == true)
        //    {
        //         txtDiscount = gridloadcustomerinfo.Rows[0].Cells["cusDisRate"].Value.ToString();
        //         txtDiscountValue = gridloadcustomerinfo.Rows[0].Cells["cusDiscValue"].Value.ToString();
        //        string seq = gridloadcustomerinfo.Rows[0].Cells["Seq"].Value.ToString();
        //        int result = CHNLSVC.Sales.UpdateDiscountDefintionStatus(BaseCls.GlbUserID.ToString(), Convert.ToDecimal(txtDiscount), 1, seq, Convert.ToDecimal(txtDiscountValue));
        //        updatePopUpCollector(false);
        //        if (result == 1)
        //        {
        //            MessageBox.Show("Successfully Approved..", "Discount Request’", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            pnldiscountforcashcredit.Visible = false;
        //            gridloadDiscntCashCreditSales.Enabled = true;
        //        }
        //        else
        //        {
        //            MessageBox.Show("Error occur while processing.", "Discount Request’", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //}
        private bool checkValidation()
        {
            bool validate = true;
            if (gridloadcustomerinfo.Rows[0].Cells["cusDisRate"].Value == null && gridloadcustomerinfo.Rows[0].Cells["cusDiscValue"].Value ==null)
            {
                MessageBox.Show("Discount Rate and Value cannot be null", "Value", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gridloadcustomerinfo.Columns["cusDisRate"].DefaultCellStyle.BackColor = Color.LightGray;
                gridloadcustomerinfo.Columns["cusDiscValue"].DefaultCellStyle.BackColor = Color.LightGray;
                validate = false;
            }
            //else if (gridloadcustomerinfo.Rows[0].Cells["cusDisRate"].Value == null && validate == true)
            //{
            //    MessageBox.Show("Discount Rate  cannot be null", "MID", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    gridloadcustomerinfo.Columns["cusDisRate"].DefaultCellStyle.BackColor = Color.LightGray;
            //    validate = false;
            //}
            //else if (gridloadcustomerinfo.Rows[0].Cells["cusDiscValue"].Value == null && validate == true)
            //{
            //    MessageBox.Show("Discount Value cannot be null", "MID", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    gridloadcustomerinfo.Columns["cusDiscValue"].DefaultCellStyle.BackColor = Color.LightGray;
            //    validate = false;
            //}
            else if (gridloadcustomerinfo.Rows[0].Cells["cusDisRate"].Value.ToString() != "0" && gridloadcustomerinfo.Rows[0].Cells["cusDiscValue"].Value.ToString() != "0" && validate == true)
            {
                MessageBox.Show("Select only one of discount type", "Request Approval’", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gridloadcustomerinfo.Columns["cusDisRate"].DefaultCellStyle.BackColor = Color.LightGray;
                gridloadcustomerinfo.Columns["cusDiscValue"].DefaultCellStyle.BackColor = Color.LightGray;
                validate = false;
            }
            else if (!txtDiscount.All(c => Char.IsDigit(c)) || Convert.ToDecimal(txtDiscount) < 0 && validate == true)
            {
                MessageBox.Show("Value need to be numeric and greater than 0", "Discount Request’", MessageBoxButtons.OK, MessageBoxIcon.Information);
                validate = false;
            }
            else if (!txtDiscount.All(c => Char.IsDigit(c)) || Convert.ToDecimal(txtDiscount) < 0 || Convert.ToDecimal(txtDiscount) > 100 && validate == true)
            {
                MessageBox.Show("Value need to be numeric and between 0-100", "Discount Request’", MessageBoxButtons.OK, MessageBoxIcon.Information);
                validate = false;
            }
            else if (!txtDiscountValue.All(c => Char.IsDigit(c)) || Convert.ToDecimal(txtDiscountValue) < 0 && validate == true)
            {
                MessageBox.Show("Value need to be numeric and greater than 0", "Discount Request’", MessageBoxButtons.OK, MessageBoxIcon.Information);
                validate = false;
            }
            return validate;
            
        }
        private void updatePopUpCollector(bool isApprove)
        {
            if (dttemp != null)
            {
                

                string message = string.Empty;

                if (isApprove)
                {
                    message = "APPROVED DISCOUNT:" + dttemp.Rows[0]["sgdd_seq"].ToString() +
                              " Discount request by " + dttemp.Rows[0]["sgdd_cre_by"].ToString() +
                              ", customer:" + dttemp.Rows[0]["mpc_desc"].ToString() +
                              " for item(s) Rate: " + dttemp.Rows[0]["sgdd_disc_rt"].ToString();
                }
                else
                {
                    message = "REJECTED DISCOUNT:" + dttemp.Rows[0]["sgdd_seq"].ToString() +
                             " Discount request by " + dttemp.Rows[0]["sgdd_cre_by"].ToString() +
                             ", customer:" + dttemp.Rows[0]["mpc_desc"].ToString() +
                             " for item(s) Rate: " + dttemp.Rows[0]["sgdd_disc_rt"].ToString();
                }

                PopupCollect oPopupCollect = new PopupCollect();
                oPopupCollect.Doc_type = "DISCOUNT";
                oPopupCollect.Cur_date = DateTime.Now;
                oPopupCollect.Sender = "WEB";
                oPopupCollect.Receiver = dttemp.Rows[0]["sgdd_cre_by"].ToString();
                oPopupCollect.Message = message;
                oPopupCollect.Doc_no = dttemp.Rows[0]["sgdd_req_ref"].ToString();
                oPopupCollect.Other_doc_no = string.Empty;
                oPopupCollect.Direction = "1";
                oPopupCollect.Isvisible = false;
                oPopupCollect.Create_user = BaseCls.GlbUserID.ToString();
                oPopupCollect.Create_when = DateTime.Now;
                oPopupCollect.Modify_by = BaseCls.GlbUserID.ToString();
                oPopupCollect.Modify_when = DateTime.Now;
                oPopupCollect.Remarks = string.Empty;
                oPopupCollect.Company = BaseCls.GlbUserID.ToString();
                oPopupCollect.Location = dttemp.Rows[0]["sgdd_pc"].ToString();
                oPopupCollect.Channel = string.Empty;
                oPopupCollect.Doc_status = string.Empty;

                int result = CHNLSVC.Sales.InsertIntoSCM_POPUP_COLECTOR(oPopupCollect);

                DiscountReqLog oDiscountReqLog = new DiscountReqLog();
                oDiscountReqLog.REF_SEQ = Convert.ToInt32(dttemp.Rows[0]["sgdd_seq"].ToString());
                oDiscountReqLog.REF_ITM = dttemp.Rows[0]["sgdd_itm"].ToString();
                oDiscountReqLog.REF_REQ_REF = dttemp.Rows[0]["sgdd_req_ref"].ToString();
                oDiscountReqLog.REF_DISC_RT = Convert.ToDecimal(dttemp.Rows[0]["sgdd_disc_rt"].ToString());
                oDiscountReqLog.REF_CRE_BY = dttemp.Rows[0]["sgdd_cre_by"].ToString();
                oDiscountReqLog.REF_CRE_DT = Convert.ToDateTime(dttemp.Rows[0]["sgdd_cre_dt"].ToString());
                oDiscountReqLog.REF_MOD_BY = BaseCls.GlbUserID.ToString();
                oDiscountReqLog.REF_MOD_DT = DateTime.Now;
                oDiscountReqLog.REF_DISC_VALUE = Convert.ToDecimal(dttemp.Rows[0]["sgdd_disc_val"].ToString());

                if (isApprove)
                {
                    oDiscountReqLog.REF_STATUS = 1;
                }
                else
                {
                    oDiscountReqLog.REF_STATUS = 0;
                }

                int effect = CHNLSVC.Sales.InsetInto_DicountRqstLog(oDiscountReqLog);
            }
        }

        //private void btnReject_Click(object sender, EventArgs e)
        //{
        //    if (MessageBox.Show("Are you sure,you want to Reject ?", "Reject", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
        //    gridloadcustomerinfo.Columns["cusDisRate"].DefaultCellStyle.BackColor = Color.Gold;
        //    gridloadcustomerinfo.Columns["cusDiscValue"].DefaultCellStyle.BackColor = Color.Gold;
        //    Boolean validate = checkValidation();
        //    if (validate == true)
        //    {
        //        string txtDiscount = gridloadcustomerinfo.Rows[0].Cells["cusDisRate"].Value.ToString();
        //        string txtDiscountValue = gridloadcustomerinfo.Rows[0].Cells["cusDiscValue"].Value.ToString();
        //        string seq = gridloadcustomerinfo.Rows[0].Cells["Seq"].Value.ToString();
        //        int result = CHNLSVC.Sales.UpdateDiscountDefintionStatus(BaseCls.GlbUserID.ToString(), Convert.ToDecimal(txtDiscount), -1, seq, Convert.ToDecimal(txtDiscountValue));
        //        updatePopUpCollector(false);
        //        if (result == 1)
        //        {
        //            MessageBox.Show("Successfully Rejected..", "Discount Request’", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            pnldiscountforcashcredit.Visible = false;
        //            gridloadDiscntCashCreditSales.Enabled = true;
        //        }
        //        else
        //        {
        //            MessageBox.Show("Error occur while processing.", "Discount Request’", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //}

        private void btnPromoVouClose_Click(object sender, EventArgs e)
        {
            pnldiscountforcashcredit.Visible = false;
            gridloadDiscntCashCreditSales.Enabled = true;
        }

        private void btnSearchLocation_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textSearchLoc;
                //_CommonSearch.txtSearchbyword.Text = txtPC.Text;
                _CommonSearch.ShowDialog();
                textSearchLoc.Focus();
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
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {

            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                       // paramsText.Append(BaseCls.GlbUserComCode + seperator + selectPC + seperator + "" + seperator);
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
                        //paramsText.Append(BaseCls.GlbUserComCode + seperator + selectPC + seperator);
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

        private void btnSearchCusCode_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textCustomerCode;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                textCustomerCode.Select();
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

        private void textCustomerCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textCustomerCode;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                textCustomerCode.Select();
            }
        }

        private void textSearchLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = textSearchLoc;
                //_CommonSearch.txtSearchbyword.Text = txtPC.Text;
                _CommonSearch.ShowDialog();
                textSearchLoc.Focus();
            }
        }

        //private void btnSearch_Click(object sender, EventArgs e)
        //{
        //    string ReqType = cmbrequesttype.Text.Trim();
        //    dt = new DataTable();
        //    dt = CHNLSVC.Sales.GetDiscountRequest(BaseCls.GlbUserComCode.ToString(), "", "", BaseCls.GlbUserID.ToString());
        //    gridloadDiscntCashCreditSales.Visible = true;
        //    dtsearch = new DataTable();
        //    DataRow[] foundRows;
        //    string expression = "SGDD_PC like '%" + textSearchLoc.Text + "%' AND  SGDD_CRE_DT = '" + dtdate.Value.Date + "' AND SGDD_CUST_CD like '%" + textCustomerCode.Text + "%'";
        //    foundRows=dt.Select(expression);
        //    if(foundRows.Count()>0 && checkSelectAll.Checked==false)
        //    {
        //    dtsearch = foundRows.CopyToDataTable<DataRow>();
        //    gridloadDiscntCashCreditSales.AutoGenerateColumns = false;
        //    gridloadDiscntCashCreditSales.DataSource = dtsearch;
        //    }
        //    else if (checkSelectAll.Checked == true)
        //    {
        //        gridloadDiscntCashCreditSales.AutoGenerateColumns = false;
        //        gridloadDiscntCashCreditSales.DataSource = dt;
        //    }
            
        //}

        private void checkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSelectAll.Checked == true)
            {
                textSearchLoc.Text = "";
                textCustomerCode.Text = "";
                btnSearchCusCode.Enabled = false;
                btnSearchLocation.Enabled = false;
                textSearchLoc.Enabled = false;
                textCustomerCode.Enabled = false;
                dtdate.Enabled = false;

            }
            else if (checkSelectAll.Checked == false)
            {
                btnSearchCusCode.Enabled = true;
                btnSearchLocation.Enabled = true;
                textSearchLoc.Enabled = true;
                textCustomerCode.Enabled = true;
                dtdate.Enabled = true;
                dtdate.Text = DateTime.Now.ToString();
            }
        }

        private void buttonApprove_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure,you want to Approve ?", "Approve", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            gridloadcustomerinfo.Columns["cusDisRate"].DefaultCellStyle.BackColor = Color.Gold;
            gridloadcustomerinfo.Columns["cusDiscValue"].DefaultCellStyle.BackColor = Color.Gold;
            txtDiscount = gridloadcustomerinfo.Rows[0].Cells["cusDisRate"].Value.ToString();
            txtDiscountValue = gridloadcustomerinfo.Rows[0].Cells["cusDiscValue"].Value.ToString();
            Boolean validate = checkValidation();
            if (validate == true)
            {
                txtDiscount = gridloadcustomerinfo.Rows[0].Cells["cusDisRate"].Value.ToString();
                txtDiscountValue = gridloadcustomerinfo.Rows[0].Cells["cusDiscValue"].Value.ToString();
                string seq = gridloadcustomerinfo.Rows[0].Cells["Seq"].Value.ToString();
                int result = CHNLSVC.Sales.UpdateDiscountDefintionStatus(BaseCls.GlbUserID.ToString(), Convert.ToDecimal(txtDiscount), 1, seq, Convert.ToDecimal(txtDiscountValue));
                updatePopUpCollector(false);
                if (result == 1)
                {
                    MessageBox.Show("Successfully Approved..", "Discount Request’", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pnldiscountforcashcredit.Visible = false;
                    gridloadDiscntCashCreditSales.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Error occur while processing.", "Discount Request’", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnReject_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure,you want to Reject ?", "Reject", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            gridloadcustomerinfo.Columns["cusDisRate"].DefaultCellStyle.BackColor = Color.Gold;
            gridloadcustomerinfo.Columns["cusDiscValue"].DefaultCellStyle.BackColor = Color.Gold;
            Boolean validate = checkValidation();
            if (validate == true)
            {
                string txtDiscount = gridloadcustomerinfo.Rows[0].Cells["cusDisRate"].Value.ToString();
                string txtDiscountValue = gridloadcustomerinfo.Rows[0].Cells["cusDiscValue"].Value.ToString();
                string seq = gridloadcustomerinfo.Rows[0].Cells["Seq"].Value.ToString();
                int result = CHNLSVC.Sales.UpdateDiscountDefintionStatus(BaseCls.GlbUserID.ToString(), Convert.ToDecimal(txtDiscount), -1, seq, Convert.ToDecimal(txtDiscountValue));
                updatePopUpCollector(false);
                if (result == 1)
                {
                    MessageBox.Show("Successfully Rejected..", "Discount Request’", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pnldiscountforcashcredit.Visible = false;
                    gridloadDiscntCashCreditSales.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Error occur while processing.", "Discount Request’", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
                lblUsrMsg.Visible = false;
                string ReqType = cmbrequesttype.Text.Trim();
                dt = new DataTable();
                dt = CHNLSVC.Sales.GetDiscountRequest(BaseCls.GlbUserComCode.ToString(), "", "", BaseCls.GlbUserID.ToString());
                if (dt.Rows.Count > 0)
                {
                    gridloadDiscntCashCreditSales.Visible = true;
                    dtsearch = new DataTable();
                    DataRow[] foundRows;
                    string expression = "SGDD_PC like '%" + textSearchLoc.Text + "%' AND  SGDD_CRE_DT = '" + dtdate.Value.Date + "' AND SGDD_CUST_CD like '%" + textCustomerCode.Text + "%'";
                    foundRows = dt.Select(expression);
                    if (foundRows.Count() > 0 && checkSelectAll.Checked == false)
                    {
                        dtsearch = foundRows.CopyToDataTable<DataRow>();
                        gridloadDiscntCashCreditSales.AutoGenerateColumns = false;
                        gridloadDiscntCashCreditSales.DataSource = dtsearch;
                    }
                    else if (checkSelectAll.Checked == true)
                    {
                        gridloadDiscntCashCreditSales.AutoGenerateColumns = false;
                        gridloadDiscntCashCreditSales.DataSource = dt;
                    }
                    else
                    {
                        lblUsrMsg.Visible = true;
                        gridloadDiscntCashCreditSales.AutoGenerateColumns = false;
                        gridloadDiscntCashCreditSales.DataSource = dtsearch;
                    }
                }
                else
                {
                    lblUsrMsg.Visible = true;
                    gridloadDiscntCashCreditSales.AutoGenerateColumns = false;
                    gridloadDiscntCashCreditSales.DataSource = dt;

                }
            
        }
        

       

       

        
    }
}
