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
using System.IO;


namespace FF.WindowsERPClient.Finance
{
    //sp_getInvoiceItem_CommisDet
    //pkg_search.sp_searchsalesdocbyinvoicetype -UPDATE

    public partial class CommissionChange : Base
    {
        private List<InvoiceHeader> changedInvoicesList;

        public List<InvoiceHeader> ChangedInvoicesList
        {
            get { return changedInvoicesList; }
            set { changedInvoicesList = value; }
        }
        //public List<InvoiceHeader> ChangedInvoicesList
        //{
        //    get { return (List<InvoiceHeader>)Session["ChangedInvoicesList"]; }
        //    set { Session["ChangedInvoicesList"] = value; }
        //}

        public CommissionChange()
        {
            InitializeComponent();
            ucProfitCenterSearch1.Company = BaseCls.GlbUserComCode;
            ucProfitCenterSearch1.ChangeCompany(false);
            ucProfitCenterSearch1.TextBoxCompany_Leave(null, null);

            ChangedInvoicesList = new List<InvoiceHeader>();
            txtType.Text = "CS";
            txtType.Focus();
            
        }

        private void btnAddPc_Click(object sender, EventArgs e)
        {
            try
            {
                string com = ucProfitCenterSearch1.Company;
                string chanel = ucProfitCenterSearch1.Channel;
                string subChanel = ucProfitCenterSearch1.SubChannel;
                string area = ucProfitCenterSearch1.Area;
                string region = ucProfitCenterSearch1.Regien;
                string zone = ucProfitCenterSearch1.Zone;
                string pc = ucProfitCenterSearch1.ProfitCenter;

                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com, chanel, subChanel, area, region, zone, pc);

                grvProfCents.DataSource = null;
                grvProfCents.AutoGenerateColumns = false;
                grvProfCents.DataSource = dt;

                this.btnAllPc_Click(sender, e);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnProcessCirc_Click(object sender, EventArgs e)
        {
            try
            {
                //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "COMMI"))
                //{

                //}
                //else
                //{
                //    MessageBox.Show("Permission Not Granted!\n( Advice: Reqired permission code :COMMI )", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}

                //Add by Chamal 28-Aug-2013
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10041))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Sorry, You have no permission for change commission!\n( Advice: Required permission code :10041)", "Change Commission", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                List<string> pc_list = GetSelectedPCList();
                if (pc_list.Count < 1)
                {
                    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter profit center(s)!");
                    MessageBox.Show("Please select profit center(s)!");
                    return;
                }
                if (MessageBox.Show("Are you sure to process on all selected profit centers?", "Circular Commission Change", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                //Int32 eff= CHNLSVC.Sales.circularWise_Commission_change(GlbUserComCode, pc_list, DateTime.Now.Date, txtCircularName.Text.Trim().ToUpper(), null);
                DateTime dtfrm = Convert.ToDateTime(txtFrm_dt.Value).Date;
                DateTime dtTo = Convert.ToDateTime(txtTo_dt.Value).Date;

                Double no_ofDays = (dtTo - dtfrm).TotalDays;
                List<DateTime> dates = new List<DateTime>();
                dates.Add(dtfrm);
                for (int i = 0; i < no_ofDays; i++)
                {
                    dates.Add(dtfrm.AddDays(1));
                }

                //DateTime DT = DateTime.Now.Date.AddDays(-4);
                Int32 eff = CHNLSVC.Sales.Process_Commission_change_at_PC(BaseCls.GlbUserComCode, pc_list, dates);
                if (eff > 0)
                {
                    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Succussfylly changed!");
                    MessageBox.Show("Succussfylly changed!");
                    return;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private List<string> GetSelectedPCList()
        {
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvProfCents.Rows)
            {
                 DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                 if (Convert.ToBoolean(chk.Value) == true)
                 {
                     list.Add(dgvr.Cells[1].Value.ToString());
                 }
                
            }
            return list;
        }

        private void btnFinalize_Click(object sender, EventArgs e)
        {
            try
            {
                //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "COMMI"))
                //{
                //}
                //else
                //{
                //    MessageBox.Show("Permission Not Granted!\n( Advice: Reqired permission code :COMMI )", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}

                //Add by Chamal 28-Aug-2013
                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10041))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Sorry, You have no permission for change commission!\n( Advice: Required permission code :10041)", "Change Commission", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // this.MasterMsgInfoUCtrl.Clear();
                try
                {
                    DateTime dtfrm_ = Convert.ToDateTime(txtFrm_dt.Value).Date;
                    DateTime dtTo_ = Convert.ToDateTime(txtTo_dt.Value).Date;
                }
                catch (Exception ex)
                {
                    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter valid dates!");
                    MessageBox.Show("Enter valid dates!");
                    return;
                }
                if (MessageBox.Show("Are you sure to finalize all selected profit centers?", "Finalize Circular Commission Change", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                DateTime dtfrm = Convert.ToDateTime(txtFrm_dt.Value).Date;
                DateTime dtTo = Convert.ToDateTime(txtTo_dt.Value).Date;

                Double no_ofDays = (dtTo - dtfrm).TotalDays;
                List<DateTime> dates = new List<DateTime>();
                dates.Add(dtfrm);
                for (int i = 0; i < no_ofDays; i++)
                {
                    dates.Add(dtfrm.AddDays(1));
                }
                List<string> list = new List<string>();

                foreach (DataGridViewRow dgvr in grvProfCents.Rows)
                {
                    // CheckBox chkSelect = (CheckBox)gvr.FindControl("chekPc");
                    DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        list.Add(dgvr.Cells[1].Value.ToString());
                    }
                }
                //-------------------------------------------
                string company = ucProfitCenterSearch1.Company;
                try
                {
                    foreach (DateTime dt in dates)
                    {
                        foreach (string pc in list)
                        {
                            Int32 eff = CHNLSVC.Financial.ProcessFinalizeDayEnd(dt, company, pc);
                        }

                    }
                    //  this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Succussfylly Finalized!");
                    MessageBox.Show("Succussfylly Finalized!");
                    // string Msg = "<script>alert('Succussfylly Finalized!');</script>";
                    // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                    ClearScreen();
                }
                catch (Exception ex)
                {
                    //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Failed to Finalize!");
                    MessageBox.Show("Failed to Finalize!");
                    //string Msg = "<script>alert('Failed to Finalize!');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                }
            }
            catch (Exception ex)
            {

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
                if (ChangedInvoicesList.Count > 0)
                {
                    if (MessageBox.Show("Finalization not done yet.\nDo you still want to clear?", "Information", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }

                }
                CommissionChange formnew = new CommissionChange();
                formnew.MdiParent = this.MdiParent;
                formnew.Location = this.Location;
                formnew.Show();
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void ImgBtnAddInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                lblInvoiceDate.Text = "";
                lblInvNo.Text = txtInvoiceNo.Text.Trim();
                txtItmCode.Text = "";
                txtDescript.Text = "";
                txtComAmt.Text = "";
                txtNetVal.Text = "";

                DataTable emptyDt = new DataTable();
                grvPaymodeComm.DataSource = null;
                grvPaymodeComm.AutoGenerateColumns = false;
                grvPaymodeComm.DataSource = emptyDt;

                grvInvItems.DataSource = null;
                grvInvItems.AutoGenerateColumns = false;
                grvInvItems.DataSource = emptyDt;

                DataTable dt = CHNLSVC.Sales.Get_invoiceItemsForCommis(txtInvoiceNo.Text.Trim().ToUpper(), null);
                if (dt.Rows.Count > 0)
                {
                    InvoiceHeader invoiceHdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoiceNo.Text.Trim());
                    if (invoiceHdr != null)
                    {
                        if (BaseCls.GlbUserDefProf != invoiceHdr.Sah_pc)
                        {
                            // string Msg = "<script>alert('Invoice belongs to another profit center!');</script>";
                            MessageBox.Show("Invoice belongs to another profit center!");
                            // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                            return;
                        }
                        else
                        {
                            if ("CS" != invoiceHdr.Sah_inv_tp && "CRED" != invoiceHdr.Sah_inv_tp)
                            {
                                //MessageBox.Show("This is a Hire Sale invoice. Cannot change commission!");
                                MessageBox.Show("This is not a Cash/Credit sale invoice. Cannot change commission!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                                return;
                            }
                            Boolean isPeriodClose = CHNLSVC.Financial.IsPeriodClosed(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "FIN_REM", invoiceHdr.Sah_dt);
                            if (isPeriodClose == true)
                            {
                                // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Period Closed!");
                                MessageBox.Show("Period Closed!");
                                return;
                            }
                            lblInvoiceDate.Text = invoiceHdr.Sah_dt.ToShortDateString();

                            grvInvItems.DataSource = null;
                            grvInvItems.AutoGenerateColumns = false;
                            grvInvItems.DataSource = dt;
                            //grvInvItems.DataBind();
                        }
                    }
                    else
                    {
                        // string Msg = "<script>alert('Invoice could not be found!');</script>";
                        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                        // MessageBox.Show("Period Closed!");
                        MessageBox.Show("Invoice could not be found!.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    DataTable emptyDt_ = new DataTable();
                    grvInvItems.AutoGenerateColumns = false;
                    grvInvItems.DataSource = emptyDt_;
                    // grvInvItems.DataBind();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        #region Searchin
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Circular:
                    {
                        paramsText.Append("" + seperator + "Circular" + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.Promotion:
                //    {
                //        paramsText.Append(txtCircular.Text.Trim().ToUpper() + seperator + "Promotion" + seperator);
                //        break;
                //    }
                //Sales_SubType
                case CommonUIDefiniton.SearchUserControlType.Sales_Type:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Sales_SubType:
                    {
                        paramsText.Append(txtType.Text.Trim().ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SalesInvoice:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + txtType.Text.Trim().ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvSalesInvoice:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CashInvoice:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator + txtType.Text.Trim().ToUpper() + seperator + 1 + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion
        private void ImgBtnType_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_Type);
                DataTable _result = CHNLSVC.General.GetSalesTypes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtType;
                _CommonSearch.ShowDialog();
                txtType.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void ImgBtnSubType_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Sales_SubType);
                DataTable _result = CHNLSVC.CommonSearch.Get_sales_subtypes(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSubType;
                _CommonSearch.ShowDialog();
                txtSubType.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void ImgBtnInvoiceNo_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoice);
                // DataTable _result = CHNLSVC.CommonSearch.GetInvoiceSearchData(_CommonSearch.SearchParams, null, null); //GetInvoiceByInvType

                //  _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesInvoice);
                //  DataTable _result = CHNLSVC.CommonSearch.GetAllInvoiceSearchData(_CommonSearch.SearchParams, null, null);

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CashInvoice);
                DataTable _result = CHNLSVC.CommonSearch.GetInvoiceByInvType(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtInvoiceNo;
                _CommonSearch.ShowDialog();
                txtInvoiceNo.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void grvInvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Int32 rowIndex = e.RowIndex;
                if (e.ColumnIndex == 0 && e.RowIndex != -1)
                {
                    //GridViewRow row = grvInvItems.SelectedRow;
                    DataGridViewRow row = grvInvItems.Rows[rowIndex];

                    string ItemCode = row.Cells["sad_itm_cd"].Value.ToString();
                    string Descrtipt = row.Cells["mi_shortdesc"].Value.ToString();
                    txtItmCode.Text = ItemCode;
                    txtDescript.Text = Descrtipt;
                    txtComAmt.Text = row.Cells["comm_amt"].Value.ToString();
                    txtNetVal.Text = row.Cells["net_value"].Value.ToString();

                    DataTable dt = CHNLSVC.Sales.Get_Paymodes_ofItemsForCommis(lblInvNo.Text.ToUpper(), ItemCode);
                    if (dt.Rows.Count > 0)
                    {
                        grvPaymodeComm.DataSource = null;
                        grvPaymodeComm.AutoGenerateColumns = false;
                        grvPaymodeComm.DataSource = dt;
                        //grvPaymodeComm.DataBind();
                    }
                    else
                    {
                        DataTable emptyDt = new DataTable();
                        grvPaymodeComm.DataSource = null;
                        grvPaymodeComm.AutoGenerateColumns = false;
                        grvPaymodeComm.DataSource = emptyDt;
                        //grvPaymodeComm.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.ImgBtnInvoiceNo_Click(sender, e);
            }
        }

        private void txtInvoiceNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==(char)Keys.Enter)
            {
                this.ImgBtnAddInvoice_Click(sender, e);
            }
        }

        private void grvPaymodeComm_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Int32 rowIndex = e.RowIndex;

                if (e.ColumnIndex == 7 && e.RowIndex != -1)
                {
                    //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "COMMI"))
                    //{

                    //}
                    //else
                    //{
                    //    MessageBox.Show("Permission Not Granted!\n( Advice: Reqired permission code :COMMI )", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}

                    //Add by Chamal 28-Aug-2013
                    if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10041))
                    {
                        this.Cursor = Cursors.Default;
                        MessageBox.Show("Sorry, You have no permission for change commission!\n( Advice: Required permission code :10041)", "Change Commission", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    DataGridViewRow row = grvPaymodeComm.Rows[rowIndex];
                    //  TextBox finalcommRt = (TextBox)grvPaymodeComm.Rows[rowIndex].FindControl("txtFinCommRt");
                    //  Decimal finComm_Rt = Convert.ToDecimal(finalcommRt.Text);
                    Decimal finComm_Rt;
                    try
                    {
                        finComm_Rt = Convert.ToDecimal(row.Cells["sac_comm_rate_final"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Enter valid rate.");
                        return;
                    }
                    if (finComm_Rt > 100 || finComm_Rt < 0)
                    {
                        MessageBox.Show("Invalid rate!");
                        return;
                    }
                    if (MessageBox.Show("Are you sure to change commission?", "Change Commission", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                    // TextBox finalcommAmt = (TextBox)grvPaymodeComm.Rows[rowIndex].FindControl("txtFinCommAmt");
                    Decimal finalcommAmt = Convert.ToDecimal(row.Cells["sac_comm_amt_final"].Value.ToString());

                    Decimal calOn = Convert.ToDecimal(row.Cells["sac_calc_on"].Value.ToString());
                    Decimal finComm_Amt = calOn * (finComm_Rt / 100);
                    finComm_Amt = Math.Round(finComm_Amt, 2);
                    // finalcommAmt.Text = finComm_Amt.ToString();  
                    row.Cells["sac_comm_amt_final"].Value = finComm_Amt.ToString();

                    //  Label commLine = (Label)grvPaymodeComm.Rows[rowIndex].FindControl("lblCommLine");
                    //  Label InvoiceNo = (Label)grvPaymodeComm.Rows[rowIndex].FindControl("lblInvoiceNo");
                    //  Label ItemLine = (Label)grvPaymodeComm.Rows[rowIndex].FindControl("lblItemLine");

                    string itemCode = Convert.ToString(row.Cells["sac_itm_cd"].Value.ToString());
                    Int32 comm_line = Convert.ToInt32(row.Cells["sac_comm_line"].Value.ToString());//Convert.ToInt32(commLine.Text);
                    string invoiceNo = Convert.ToString(row.Cells["sac_invoice_no"].Value.ToString()); //InvoiceNo.Text;
                    Int32 itm_line = Convert.ToInt32(row.Cells["sac_itm_line"].Value.ToString());//Convert.ToInt32(ItemLine.Text);

                    Int32 eff = CHNLSVC.Sales.UpdateCommissionLine(invoiceNo, itemCode, itm_line, comm_line, finComm_Rt, finComm_Amt);
                    // Int32 eff = 1;
                    if (eff > 0)
                    {
                        try
                        {
                            InvoiceHeader invoiceHdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(invoiceNo);
                            Boolean found = false;
                            foreach (DataGridViewRow grvRow in grvChangedInvoices.Rows)
                            {
                                if (invoiceNo == grvRow.Cells["Sah_inv_no"].Value.ToString())
                                {
                                    found = true;
                                }
                            }
                            if (found == false)
                            {
                                ChangedInvoicesList.Add(invoiceHdr);
                                grvChangedInvoices.DataSource = null;
                                grvChangedInvoices.AutoGenerateColumns = false;
                                grvChangedInvoices.DataSource = ChangedInvoicesList;
                            }

                        }
                        catch (Exception ex)
                        {

                        }
                        //string Msg = "<script>alert('Commission Changed!');</script>";
                        MessageBox.Show("Commission Changed!");
                        //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnFinalSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to finalize?", "Change Commission", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                List<string> dayList = new List<string>();
                foreach (InvoiceHeader inv in ChangedInvoicesList)
                {
                    string date = inv.Sah_dt.ToShortDateString();

                    if (dayList.Contains(date) == false)
                    {
                        dayList.Add(date);
                    }
                }
                Int32 eff2 = 0;
                foreach (string date in dayList)
                {
                    eff2 = CHNLSVC.Financial.ProcessFinalizeDayEnd(Convert.ToDateTime(date), BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                }
                if (eff2 > 0)
                {
                    // string Msg = "<script>alert('Succussfylly Finalized!');</script>";
                    MessageBox.Show("Succussfylly Finalized!");
                    // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                    ClearScreen();
                }
                else
                {
                    // string Msg = "<script>alert('Failed to Finalize!');</script>";
                    MessageBox.Show("Failed to Finalize!");
                    // ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtInvoiceNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ImgBtnInvoiceNo_Click(sender, e);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ClearScreen()
        {
            lblInvoiceDate.Text = "";
            lblInvNo.Text = "";
            txtItmCode.Text = "";
            txtDescript.Text = "";
            txtComAmt.Text = "";
            txtNetVal.Text = "";
            txtType.Text = "CS";
            txtSubType.Text = "";
            txtInvoiceNo.Text="";

            DataTable emptyDt = new DataTable();
            grvInvItems.DataSource = null;
            grvInvItems.AutoGenerateColumns = false;
            grvInvItems.DataSource = emptyDt;
            
            grvPaymodeComm.DataSource = null;
            grvPaymodeComm.AutoGenerateColumns = false;
            grvPaymodeComm.DataSource = emptyDt;

            grvChangedInvoices.DataSource = null;
            grvChangedInvoices.AutoGenerateColumns = false;
            grvChangedInvoices.DataSource = emptyDt;

            ChangedInvoicesList = new List<InvoiceHeader>();
            //-----------------------------------
            txtFrm_dt.Value = CHNLSVC.Security.GetServerDateTime().Date;
            txtTo_dt.Value = txtFrm_dt.Value;
            grvProfCents.DataSource = null;
            grvProfCents.AutoGenerateColumns = false;
            grvProfCents.DataSource = emptyDt;
            ucProfitCenterSearch1.Company = BaseCls.GlbUserComCode;
            ucProfitCenterSearch1.TextBoxCompany_Leave(null, null);
            ucProfitCenterSearch1.Channel = "";
            ucProfitCenterSearch1.SubChannel = "";
            ucProfitCenterSearch1.Area = "";
            ucProfitCenterSearch1.Regien = "";
            ucProfitCenterSearch1.Zone = "";
            ucProfitCenterSearch1.ProfitCenter = "";
        }

        private void CommissionChange_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ChangedInvoicesList.Count>0)
            {
                if (MessageBox.Show("Do you want to Finalize before closing?", "Finalize before closing", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    e.Cancel = true;
                }

            }
        }
        private void txtType_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ImgBtnType_Click(sender, e);
        }
        private void txtSubType_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ImgBtnSubType_Click(sender, e);
        }
        private void txtType_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F2)
            {
                this.ImgBtnType_Click(sender, e);
            }            
        }
        private void txtSubType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.ImgBtnSubType_Click(sender, e);
            }
        }

        private void txtType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtType.Text=="HS")
                {
                    txtType.Text = "";
                    MessageBox.Show("Hire Sale invoices are not allowed.","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    txtType.Focus();
                    return;
                }
                txtSubType.Focus();
            }
        }

        private void txtSubType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtInvoiceNo.Focus();
            }
        }

        private void btnAllPc_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvProfCents.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvProfCents.EndEdit();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnNonePc_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvProfCents.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvProfCents.EndEdit();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnClearPc_Click(object sender, EventArgs e)
        {
            DataTable emptyDt = new DataTable();
            grvProfCents.DataSource = null;
            grvProfCents.AutoGenerateColumns = false;
            grvProfCents.DataSource = emptyDt;
        }

        private void txtType_Leave(object sender, EventArgs e)
        {
            //this.txtType_KeyPress(null, null);
            if (txtType.Text == "HS")
            {
                txtType.Text = "";
                MessageBox.Show("Hire Sale invoices are not allowed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtType.Focus();
                return;
            }
            txtSubType.Focus();
        }

        private void CommissionChange_Load(object sender, EventArgs e)
        {
           
        }        
    }
}
