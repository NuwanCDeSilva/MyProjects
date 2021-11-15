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
using System.Text.RegularExpressions;

namespace FF.WindowsERPClient.Inventory
{
    public partial class DeliverySchedule : Base
    { 
        public DeliverySchedule()
        {
            InitializeComponent();
        }
        public static readonly ChannelOperator channelService = new ChannelOperator();
        private List<MasterBusinessEntity> _custList = new List<MasterBusinessEntity>();
        private MasterBusinessEntity _custProfile = new MasterBusinessEntity();
        private GroupBussinessEntity _custGroup = new GroupBussinessEntity();
        private List<InvoiceItem> invoice_items = null;
        private List<InvoiceItem> invoice_items_bind = null;
        //InventoryDoShedule invHdrDoShedule = new InventoryDoShedule ();
        List<InventoryDoShedule> _InventoryDoSheduleList = new List<InventoryDoShedule>();
      
        private string _profitCenter = "";
        private Int32 Seq_id = 0;
        private string invNo = "";
        private string Itm_Stus = "";
        private string cust_type = "";
        bool _isExsit = false;
        private string _invoiceType = "";
        private string _accNo = "";
        Int32 itmline = 0;
        Int32 effect = 0;
        public ChannelOperator CHNLSVC
        {
            get
            {
                //if (!string.IsNullOrEmpty(BaseCls.GlbUserSessionID))
                //{
                //    if (BaseCls.GlbUserID != "ADMIN")
                //    {
                //        ChannelOperator channelServiceTemp = new ChannelOperator();
                //        if (channelServiceTemp.Security.IsSessionExpired(BaseCls.GlbUserSessionID, BaseCls.GlbUserID, BaseCls.GlbUserComCode) == true)
                //        {
                //            MessageBox.Show("Current Session is expired or has been closed by administrator!", "Fast Forward - SCM-II", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //            BaseCls.GlbIsExit = true;
                //            Application.Exit();
                //            GC.Collect();
                //        }
                //        channelServiceTemp.CloseAllChannels();
                //    }
                //}
                return channelService;
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
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvSalesInvoice:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.NIC:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Mobile:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Currency:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Employee_Executive:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "SEX" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Receipt:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + CommonUIDefiniton.PayMode.ADVAN.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Group_Sale:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + 1 + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "DO" + seperator + "0" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        #endregion Common Searching Area
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

        private void txtFindCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Customer_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                btnGetInvoices.Focus();
        }

        private void txtFindCustomer_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFindCustomer.Text)) return;

                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtFindCustomer.Text, string.Empty, string.Empty, "C");
                CHNLSVC.CloseAllChannels();
                if (_masterBusinessCompany.Mbe_cd == null)
                {
                    MessageBox.Show("Please select the valid customer", "Customer Code", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFindCustomer.Text = "";
                    txtFindCustomer.Focus();
                    return;
                }
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

        private void txtFindCustomer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Customer_Click(null, null);
        }

        private void txtFindInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Invoice_Click(null, null);
            if (e.KeyCode == Keys.Enter)
                btnGetInvoices.Focus();
        }

        private void txtFindInvoiceNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFindInvoiceNo.Text)) return;
                InvoiceHeader _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtFindInvoiceNo.Text);
                if (_hdr == null)
                {
                    MessageBox.Show("Please select the valid invoice no", "Invoice No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFindInvoiceNo.Text = string.Empty;
                    txtFindInvoiceNo.Focus();
                    return;
                }
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

        private void txtFindInvoiceNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearch_Invoice_Click(null, null);
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

        private void btnGetInvoices_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = CHNLSVC.Sales.GetPendingInvoicesToDO(BaseCls.GlbUserComCode, dtpFromDate.Value.Date, dtpToDate.Value.Date, BaseCls.GlbUserDefLoca, txtFindCustomer.Text, txtFindInvoiceNo.Text, 1); // any loc 1
                

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
                    MessageBox.Show("No pending invoices found!", "Forward Sales", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dvPendingInvoices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dvPendingInvoices.ColumnCount > 0 && e.RowIndex >= 0)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    //MessageBox.Show(dvPendingInvoices.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    //MessageBox.Show(dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_INV_NO"].Value.ToString());

                    txtInvcNo.Text = "Invoice No : " + dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_INV_NO"].Value.ToString();
                    invNo = dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_INV_NO"].Value.ToString();
                    //lblInvoiceNo.Text = dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_INV_NO"].Value.ToString();
                    //txtInvcNo.Text = lblInvoiceNo.Text;
                    //lblInvoiceDate.Text = Convert.ToDateTime(dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_DT"].Value.ToString()).Date.ToString("dd/MMM/yyyy");
                    //txtInvcDate.Text = lblInvoiceDate.Text;
                   // txtCustCode.Text = dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_CUS_CD"].Value.ToString();
                  //  txtCustName.Text = dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_CUS_NAME"].Value.ToString();
                    //txtCustAddress1.Text = dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_CUS_ADD1"].Value.ToString();
                  //  txtCustAddress2.Text = dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_CUS_ADD2"].Value.ToString();
                    _profitCenter = dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_PC"].Value.ToString();
                    _invoiceType = dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_INV_TP"].Value.ToString();
                    _accNo = dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_ACC_NO"].Value.ToString();
                    // add by tharanga 2017/08/22
                    txt_d_cust_cd.Text = dvPendingInvoices.Rows[e.RowIndex].Cells["sah_d_cust_cd"].Value.ToString();
                    txt_d_cust_name.Text = dvPendingInvoices.Rows[e.RowIndex].Cells["sah_d_cust_name"].Value.ToString();
                    txt_d_cust_add1.Text = dvPendingInvoices.Rows[e.RowIndex].Cells["sah_d_cust_add1"].Value.ToString();
                    txt_d_cust_add2.Text = dvPendingInvoices.Rows[e.RowIndex].Cells["sah_d_cust_add2"].Value.ToString();
                    //sales_ex_cd = dvPendingInvoices.Rows[e.RowIndex].Cells["sah_sales_ex_cd"].Value.ToString();
                    Load_cust_details(txt_d_cust_cd.Text.Trim().ToString());
                    ////
                    //if (txtCustCode.Text == "AHDR2B0002")
                    //{ btnBOC.Visible = true; }
                    //else
                    //{ btnBOC.Visible = false; }
                    LoadInvoiceItems(dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_INV_NO"].Value.ToString(), _profitCenter);
                    DataTable shedulitm = CHNLSVC.Sales.getinvshed_item(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, dvPendingInvoices.Rows[e.RowIndex].Cells["SAH_INV_NO"].Value.ToString());
                    //if (shedulitm.Rows.Count > 0)
                    //{

                           
                    //        grd_sid_itm.AutoGenerateColumns = false;
                    //        grd_sid_itm.DataSource = shedulitm;
                    //}
                    Cursor.Current = Cursors.Default;
                }
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
        public MasterBusinessEntity GetbyCustCD(string custCD)
        {
            return CHNLSVC.Sales.GetCustomerProfileByCom(custCD, null, null, null, null, BaseCls.GlbUserComCode);
        }
        public void LoadCustProf(MasterBusinessEntity cust)
        {

            txt_d_cust_cd.Text = cust.Mbe_cd;
            txt_d_cust_name.Text = cust.Mbe_name;
            txt_d_cust_nic.Text = cust.Mbe_nic;
            txt_d_cust_mob.Text = cust.Mbe_mob;
            txt_d_cust_add1.Text = cust.Mbe_add1;
            txt_d_cust_add2.Text = cust.Mbe_add2;
            cust_type = cust.Mbe_tp;
            txt_d_cust_name.ReadOnly = true;
            txt_d_cust_nic.ReadOnly = true;
            txt_d_cust_mob.ReadOnly = true;

            txt_d_cust_cd.Enabled = false;


        }
        private void Load_cust_details(string custcode)
        {
            if (!string.IsNullOrEmpty(custcode))
            {
                //btnCreate.Enabled = true;
                MasterBusinessEntity custProf = GetbyCustCD(custcode.Trim().ToUpper());
                if (custProf.Mbe_cd != null && custProf.Mbe_act == true)
                {
                    _isExsit = true;
                    LoadCustProf(custProf);
                }
                else if (custProf.Mbe_cd != null && custProf.Mbe_act == false)
                {
                    MessageBox.Show("Customer is inactivated.Please contact accounts dept.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    LoadCustProf(custProf);
                }
                else
                {
                    if (_isExsit == true)
                    {
                        string cusCD = txt_d_cust_cd.Text.Trim().ToUpper();
                        MasterBusinessEntity cust_null = new MasterBusinessEntity();
                        LoadCustProf(cust_null);
                        txt_d_cust_cd.Text = cusCD;
                    }
                    //Check the group level



                    if (custProf.Mbe_cd == null)
                    {
                        MessageBox.Show("Invalid customer code.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_d_cust_cd.Text = "";
                    }
                }

            }


        }
        private void LoadInvoiceItems(string _invNo, string _pc)
        {
            invoice_items_bind = new List<InvoiceItem>();
            //Get Invoice Items Details
            invoice_items = CHNLSVC.Sales.GetAllSaleDocumentItemList(BaseCls.GlbUserComCode, _pc, "INV", _invNo, "A");
            Seq_id = invoice_items.FirstOrDefault().Sad_seq_no;

            if (invoice_items != null)
            {
                if (invoice_items.Count > 0)
                {
                    dvDOItems.Enabled = true;
                    //Check serial reserved for vehicle registration
                    Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("RECEIPT", BaseCls.GlbUserComCode, _invNo, 0);
                    if (user_seq_num != -1)
                    {
                        dvDOItems.AutoGenerateColumns = false;
                        dvDOItems.DataSource = invoice_items;
                        //dvDOItems.Enabled = false;
                        MessageBox.Show("Insuarance dept. still not issue cover note.", "Vehicle Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("DO", BaseCls.GlbUserComCode, _invNo, 0);
                    if (user_seq_num == -1)
                    {
                        //Generate new user seq no and add new row to pick_hdr
                        user_seq_num = GenerateNewUserSeqNo();
                    }

                    List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                    _serList = CHNLSVC.Inventory.GetAllScanSerialsList(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, user_seq_num, "DO");

                    if (_serList != null)
                    {
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                        foreach (var itm in _scanItems)
                        {
                            foreach (InvoiceItem _invItem in invoice_items)
                                if (itm.Peo.Tus_itm_cd == _invItem.Sad_itm_cd && itm.Peo.Tus_base_itm_line == _invItem.Sad_itm_line)
                                {
                                    //it.Sad_do_qty = q.theCount;
                                    //it.Sad_tot_amt = q.theCount;//assigned for the display purpose only- shown in the "DO Qty" column in grid.
                                    _invItem.Sad_srn_qty = itm.theCount; // Current scan qty
                                }
                        }
                        //dvDOSerials.AutoGenerateColumns = false;
                       // dvDOSerials.DataSource = _serList;
                    }
                    else
                    {
                        List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                        dvDOItems.AutoGenerateColumns = false;
                        grd_sid_itm.DataSource = emptyGridList;
                    }

                    //if (gvSGv.Rows.Count > 0)
                    //{
                    //    foreach (DataGridViewRow _row in gvSGv.Rows)
                    //    {
                    //        Int32 _invline = Convert.ToInt32(_row.Cells["sgv_invline"].Value);
                    //        foreach (InvoiceItem _invItem in invoice_items)
                    //            if (_invline == _invItem.Sad_itm_line)
                    //                _invItem.Sad_srn_qty += 1;
                    //    }
                    //}
                    dvDOItems.AutoGenerateColumns = false;
                    dvDOItems.DataSource = invoice_items;
                }
            }
            else
            {
                List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
               // dvDOSerials.AutoGenerateColumns = false;
                //dvDOSerials.DataSource = emptyGridList;

                InvoiceItem it = new InvoiceItem();
                MasterItem mi = new MasterItem(); //CHNLSVC.Inventory.GetItem(GlbUserComCode, it.Sad_alt_itm_cd);
                it.Sad_alt_itm_desc = "";// mi.Mi_shortdesc;
                it.Mi_model = "";// mi.Mi_model;
                it.Sad_qty = 0;
                it.Sad_tot_amt = 0;

                invoice_items_bind = new List<InvoiceItem>();

                //invoice_items.Clear();
                //invoice_items = null;
                //invoice_items_bind.Clear();
                //invoice_items_bind = null;
                invoice_items_bind.Add(it);

                dvDOItems.DataSource = invoice_items_bind;

                //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No pending items found for this invoice!");
                return;
            }

            //get all from sat_itm
        }

        #region Generate new user seq no

        private Int32 GenerateNewUserSeqNo()
        {
            Int32 generated_seq = 0;
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "DO", 1, BaseCls.GlbUserComCode);//direction always =1 for this method                    //assign user_seqno
            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = "DO";
            RPH.Tuh_cre_dt = DateTime.Today;//might change //Calendar-SelectedDate;
            RPH.Tuh_ischek_itmstus = true;//might change
            RPH.Tuh_ischek_reqqty = true;//might change
            RPH.Tuh_ischek_simitm = true;//might change
            RPH.Tuh_session_id = BaseCls.GlbUserSessionID;
            RPH.Tuh_usr_com = BaseCls.GlbUserComCode;//might change
            RPH.Tuh_usr_id = BaseCls.GlbUserID;
            RPH.Tuh_usrseq_no = generated_seq;

            RPH.Tuh_direct = false; //direction always (-) for change status
            RPH.Tuh_doc_no = txtInvcNo.Text.Trim();
            //write entry to TEMP_PICK_HDR
            int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
            CHNLSVC.CloseAllChannels();
            if (affected_rows == 1)
            {
                return generated_seq;
            }
            else
            {
                return 0;
            }
        }

        #endregion Generate new user seq no
        private void SystemErrorMessage(Exception ex)
        {
            CHNLSVC.CloseChannel();
            this.Cursor = Cursors.Default;
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        private void btnIssueLoc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtIssueLoc;
                _CommonSearch.ShowDialog();
                txtIssueLoc.Select();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtIssueLoc_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnIssueLoc_Click(null, null);
        }

        private void txtIssueLoc_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F2)
                btnIssueLoc_Click(null, null);
        }

        private void btnPerTown_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
                DataTable _result = CHNLSVC.CommonSearch.GetTown(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPerTown;
                _CommonSearch.ShowDialog();
                txtPerTown.Select();
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

        private void txtPerTown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnPerTown_Click(null, null);
        }
        private void LoadPremenentTownDetails()
        {
         

            if (!string.IsNullOrEmpty(txtPerTown.Text))
            {
                DataTable dt = new DataTable();

                dt = CHNLSVC.General.Get_DetBy_town(txtPerTown.Text.Trim().ToUpper());
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        string district = dt.Rows[0]["DISTRICT"].ToString();
                        string province = dt.Rows[0]["PROVINCE"].ToString();
                        string postalCD = dt.Rows[0]["POSTAL_CD"].ToString();
                        string countryCD = dt.Rows[0]["COUNTRY_CD"].ToString();

                    }
                    else
                    {
                        MessageBox.Show("Invalid town.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPerTown.Text = "";
                        txtPerTown.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid town.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPerTown.Text = "";
                    txtPerTown.Focus();
                }
            }
        }
        private void txtPerTown_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPerTown.Text != "")
                {
                    LoadPremenentTownDetails();
                }
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

        private void txtPerTown_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPerTown_Click(null, null); 
        }

        private void dvDOItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dvDOItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dvDOItems_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dvDOItems.ColumnCount > 0 && e.RowIndex >= 0)
                {
                    
                    Cursor.Current = Cursors.WaitCursor;
                  //  txtItmCode.Text = dvDOItems.Rows[e.RowIndex].Cells["SAD_ITM_CD"].Value.ToString();
                 //   itmReqQty.Text = dvDOItems.Rows[e.RowIndex].Cells["SAD_QTY"].Value.ToString();
                    itmline = Convert.ToInt32( dvDOItems.Rows[e.RowIndex].Cells["SAD_ITM_LINE"].Value.ToString());
                    Itm_Stus = dvDOItems.Rows[e.RowIndex].Cells["SAD_ITM_STUS"].Value.ToString();
                        
                   
                    Cursor.Current = Cursors.Default;
                }
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

        private void txtIssueLoc_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIssueLoc.Text))
            {
                DataTable _dt = CHNLSVC.Sales.getLocDesc(BaseCls.GlbUserComCode, "LOC", txtIssueLoc.Text);
                if (_dt == null || _dt.Rows.Count <= 0)
                {
                    MessageBox.Show("Invalid location", "Delivery Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIssueLoc.Clear();
                    txtIssueLoc.Focus();
                    return;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Process();
        }

        private void Process()
        {
            string error = "";
            //if (string.IsNullOrEmpty(txtItmCode.Text))
            //{
            //    MessageBox.Show("Select the Item", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //if (string.IsNullOrEmpty(itmReqQty.Text))
            //{
            //    MessageBox.Show("Select the Item quantity", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //if (string.IsNullOrEmpty(txtDelQty.Text))
            //{
            //    MessageBox.Show("Enter Deliver Item quantity", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            if (string.IsNullOrEmpty(txtIssueLoc.Text))
            {
                MessageBox.Show("Enter Deliver Location", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
              if (string.IsNullOrEmpty(txt_d_cust_cd.Text))
            {
                MessageBox.Show("Enter Deliver Customer Code", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
              if (string.IsNullOrEmpty(txt_d_cust_name.Text))
              {
                  MessageBox.Show("Enter Deliver Customer Name", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  return;
              }
              if (string.IsNullOrEmpty(txt_d_cust_add1.Text))
              {
                  MessageBox.Show("Enter Deliver Customer Name", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  return;
              }
              if (string.IsNullOrEmpty(txtPerTown.Text))
              {
                  MessageBox.Show("Enter Deliver Customer Location", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  return;
              }

              try
              {
                  effect = CHNLSVC.Inventory.Save_InventoryDoShedule(_InventoryDoSheduleList, out error);
                  if (effect != -99 && effect >= 0)
                  {
                      Cursor.Current = Cursors.Default;
                      MessageBox.Show("Delivery Shedule Successfully Saved!", "Process Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      clear();
                  }
                  else
                  {
                      MessageBox.Show(error, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }
              }
              catch (Exception err)
              {
                  Cursor.Current = Cursors.Default;
                  CHNLSVC.CloseChannel();
                  MessageBox.Show(err.Message, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
              finally
              {
                  CHNLSVC.CloseAllChannels();
              }
                  
        }

        private void pnlDelItem_Paint(object sender, PaintEventArgs e)
        {

        }
        private void clear()
        { 
        txtFindCustomer.Text="";
        txtFindInvoiceNo.Text="";
        txtInvcNo.Text="";
        txt_d_cust_cd.Text="";
        txt_d_cust_name.Text="";
        txt_d_cust_add1.Text="";
        txt_d_cust_add2.Text="";
        txtIssueLoc.Text="";
        txt_d_cust_nic.Text="";
        txt_d_cust_mob.Text="";
        txtPerTown.Text="";
        invoice_items = new  List<InvoiceItem>();
        dvDOItems.AutoGenerateColumns = false;
        dvDOItems.DataSource = invoice_items;
        _InventoryDoSheduleList = new List<InventoryDoShedule>();
        grd_sid_itm.AutoGenerateColumns = false;
        grd_sid_itm.DataSource = _InventoryDoSheduleList;
        //grd_sid_itm.DataSource = null;
        //dvDOItems.DataSource = null;
       
        }
        private void btnPromoVou_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIssueLoc.Text))
            {
                MessageBox.Show("Enter Deliver Location", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txt_d_cust_cd.Text))
            {
                MessageBox.Show("Enter Deliver Customer Code", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txt_d_cust_name.Text))
            {
                MessageBox.Show("Enter Deliver Customer Name", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txt_d_cust_add1.Text))
            {
                MessageBox.Show("Enter Deliver Customer Name", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtPerTown.Text))
            {
                MessageBox.Show("Enter Deliver Customer Location", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

           List<DataGridViewRow> rows_with_checked_column = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in dvDOItems.Rows)
            {
                if (Convert.ToBoolean(row.Cells["Select"].Value) == true)
                {
                    InventoryDoShedule invHdrDoShedule = new InventoryDoShedule();
                    List<InventoryDoShedule> templist = new List<InventoryDoShedule>();
                    templist = _InventoryDoSheduleList.Where(r => r.sid_itm_cd == row.Cells["SAD_ITM_CD"].Value.ToString() && r.sid_d_cust_cd == txt_d_cust_cd.Text.Trim().ToString()).ToList();
                    if (templist.Count > 0)
                    {
                         MessageBox.Show("Item selected with same customer", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         return;
                    }
                    invHdrDoShedule.sid_seq_no = Convert.ToInt32(row.Cells["Sad_seq_no"].Value.ToString());
                    invHdrDoShedule.sid_itm_line = Convert.ToInt32(row.Cells["SAD_ITM_LINE"].Value.ToString());

                    invHdrDoShedule.sid_inv_no = invNo;
                    invHdrDoShedule.sid_itm_cd = row.Cells["SAD_ITM_CD"].Value.ToString();
                    invHdrDoShedule.sid_itm_stus = row.Cells["SAD_ITM_STUS"].Value.ToString();
                    //invHdrDoShedule.sid_qty = Convert.ToInt32(row.Cells["PickQty"].Value.ToString());
                    invHdrDoShedule.sid_qty = Convert.ToDecimal(row.Cells["PickQty"].Value.ToString());
                    invHdrDoShedule.sid_do_qty = 0;

                    if (invHdrDoShedule.sid_qty <= 0)
                    {
                        MessageBox.Show("Enter Valid Deliver Qty", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (Convert.ToDecimal(row.Cells["PickQty"].Value.ToString()) > Convert.ToDecimal(row.Cells["Sad_qty"].Value.ToString()))
                    {
                        MessageBox.Show("Enter Valid Deliver Qty", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (invHdrDoShedule.sid_qty < invHdrDoShedule.sid_do_qty)
                    {
                        MessageBox.Show("Enter Valid Deliver Qty Location", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    invHdrDoShedule.sid_del_line = CHNLSVC.Inventory.GET_MAX_LINE_DO_SHEDULE(invHdrDoShedule.sid_seq_no, invHdrDoShedule.sid_itm_line, invHdrDoShedule.sid_inv_no, invHdrDoShedule.sid_itm_cd);
                    if (invHdrDoShedule.sid_del_line <= 0)
                    {
                        invHdrDoShedule.sid_del_line = 1;
                    }
                    else
                    { invHdrDoShedule.sid_del_line++; }
                    invHdrDoShedule.sid_del_com = BaseCls.GlbUserComCode;
                    invHdrDoShedule.sid_del_loc = txtIssueLoc.Text.Trim().ToString();
                    invHdrDoShedule.sid_d_cust_tp = cust_type;
                    invHdrDoShedule.sid_d_cust_cd = txt_d_cust_cd.Text.Trim().ToString();
                    invHdrDoShedule.sid_d_cust_name = txt_d_cust_name.Text.Trim().ToString();
                    invHdrDoShedule.sid_d_cust_add1 = txt_d_cust_add1.Text.Trim().ToString();
                    invHdrDoShedule.sid_d_cust_add2 = txt_d_cust_add2.Text.Trim().ToString();
                    invHdrDoShedule.sid_d_town = txtPerTown.Text.Trim().ToString();
                    invHdrDoShedule.sid_act = 1;
                    invHdrDoShedule.sid_cre_by = BaseCls.GlbUserID;
                    invHdrDoShedule.sid_cre_session = BaseCls.GlbUserSessionID;
                    _InventoryDoSheduleList.Add(invHdrDoShedule);
                }
               

               
            }

            if (_InventoryDoSheduleList.Count <= 0)
            {
                MessageBox.Show("Select Do Item", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_InventoryDoSheduleList.Count > 0)
            {
                List<InventoryDoShedule> _InventoryDoSheduleListnew = new List<InventoryDoShedule>();
                grd_sid_itm.AutoGenerateColumns = false;
                grd_sid_itm.DataSource = _InventoryDoSheduleListnew;
                grd_sid_itm.AutoGenerateColumns = false;
                grd_sid_itm.DataSource = _InventoryDoSheduleList;
            }
           
          
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
            btnGetInvoices_Click(null, null);
        }

        private void btnSearchFile_spv_Click(object sender, EventArgs e)
        {
            txtbrows.Text = string.Empty;
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
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

              
                    StringBuilder _errorLst = new StringBuilder();
                    if (_dt == null || _dt.Rows.Count <= 0) { MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
                    #endregion

                    if (_dt.Rows.Count > 0)
                    {
                        foreach (DataRow _dr in _dt.Rows)
                        {
                            DataTable _dtloc = CHNLSVC.Sales.getLocDesc(BaseCls.GlbUserComCode, "LOC", _dr[5].ToString().Trim().ToString());
                            if (_dtloc == null || _dtloc.Rows.Count <= 0)
                            {
                                MessageBox.Show("Invalid location", "Delivery Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtIssueLoc.Clear();
                                txtIssueLoc.Focus();
                                return;
                            }

                            _custList = CHNLSVC.Sales.GetCustomerByKeys(BaseCls.GlbUserComCode, "", _dr[0].ToString().Trim().ToString(), "", "", "", 1);
                            if (_custList == null || _custList.Count <= 1)
                             {

                                 Boolean _isValid = IsValidMobileOrLandNo(_dr[0].ToString().Trim().ToString());

                                 if (_isValid == false)
                                 {
                                     MessageBox.Show("Invalid mobile number.", "Customer Creation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                     return;
                                 }
                                 Collect_Cust(_dr[0].ToString().Trim().ToString(), _dr[1].ToString().Trim().ToString(), _dr[2].ToString().Trim().ToString());
               
                             }


                        }

                        if (_dt.Rows.Count > 0)
                        {
                            //List<ServiceAreaS> _lsit = new List<ServiceAreaS>();

                            foreach (DataRow _dr in _dt.Rows)
                            {
                                //ServiceAreaS _ServiceAreaS = new ServiceAreaS();
                                //_ServiceAreaS.SSA_COM = BaseCls.GlbUserComCode;
                                //_ServiceAreaS.SSA_SER_LOC = _dr["Service Location"] == DBNull.Value ? string.Empty : _dr["Service Location"].ToString().Trim();
                                //_ServiceAreaS.SSA_TOWN_CD = _dr["Town Code"] == DBNull.Value ? string.Empty : _dr["Town Code"].ToString().Trim();
                                //_ServiceAreaS.SSA_ACT = 1;
                                //_ServiceAreaS.SSA_CRE_BY = BaseCls.GlbUserID;
                                //_ServiceAreaS.SSA_CRE_DT = DateTime.Now;
                                //_ServiceAreaS.SSA_MOD_BY = BaseCls.GlbUserID;
                                //_ServiceAreaS.SSA_MOD_DT = DateTime.Now;
                                //_lsit.Add(_ServiceAreaS);

                            }
                           // int effrect = CHNLSVC.CustService.SaveserviceAreas(_lsit);
                            //if (effrect == 1)
                            //{
                            //    MessageBox.Show("Successfully Uploaded! ", "Service Centers Areas", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //}
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


        private void Collect_Cust(string mobile, string name, string address)
        {
            _custProfile = new MasterBusinessEntity();
            _custProfile.Mbe_acc_cd = null;
            _custProfile.Mbe_act = true;
            _custProfile.Mbe_add1 = address;
            _custProfile.Mbe_add2 = "";
            _custProfile.Mbe_agre_send_sms = false; ;
            _custProfile.Mbe_br_no = "";
            _custProfile.Mbe_cate = "";
            _custProfile.Mbe_com = BaseCls.GlbUserComCode;
            _custProfile.Mbe_contact = null;
            _custProfile.Mbe_country_cd = "";
            _custProfile.Mbe_cr_add1 = "";
            _custProfile.Mbe_cr_add2 = "";
            _custProfile.Mbe_cr_country_cd = "";
            _custProfile.Mbe_cr_distric_cd = "";
            _custProfile.Mbe_cr_email = null;
            _custProfile.Mbe_cr_fax = null;
            _custProfile.Mbe_cr_postal_cd = "";
            _custProfile.Mbe_cr_province_cd = "";
            _custProfile.Mbe_cr_tel = mobile;
            _custProfile.Mbe_cr_town_cd = "";
            _custProfile.Mbe_cre_by = BaseCls.GlbUserID;
            _custProfile.Mbe_cre_dt = Convert.ToDateTime(DateTime.Today).Date;
            _custProfile.Mbe_cre_pc = BaseCls.GlbUserDefProf;
            _custProfile.Mbe_cust_com = BaseCls.GlbUserComCode;
            _custProfile.Mbe_cust_loc = BaseCls.GlbUserDefLoca;
            _custProfile.Mbe_distric_cd = "";
            _custProfile.Mbe_dl_no = "";
            //_custProfile.Mbe_dob = Convert.ToDateTime(dtpDOB.Text).Date;
            _custProfile.Mbe_email = "";
            _custProfile.Mbe_fax = null;
            _custProfile.Mbe_ho_stus = "GOOD";
            _custProfile.Mbe_income_grup = null;
            _custProfile.Mbe_intr_com = false;
            _custProfile.Mbe_is_suspend = false;
            _custProfile.Mbe_is_svat = false;
            _custProfile.Mbe_is_tax = false;
            _custProfile.Mbe_mob = mobile;
            _custProfile.Mbe_name = name;
            _custProfile.Mbe_nic = "";
            _custProfile.Mbe_oth_id_no = null;
            _custProfile.Mbe_oth_id_tp = null;
            _custProfile.Mbe_pc_stus = "GOOD";
            _custProfile.Mbe_postal_cd = "";
            _custProfile.Mbe_pp_no = "";
            _custProfile.Mbe_province_cd = "";
            _custProfile.Mbe_sex = "";
            _custProfile.Mbe_sub_tp = null;
            _custProfile.Mbe_svat_no = "";
            _custProfile.Mbe_tax_ex = false;
            _custProfile.Mbe_tax_no = "";
            _custProfile.Mbe_tel = mobile;
            _custProfile.Mbe_town_cd = txtPerTown.Text.Trim();
            _custProfile.Mbe_tp = "C";
            _custProfile.Mbe_wr_add1 = "";
            _custProfile.Mbe_wr_add2 = "";
            _custProfile.Mbe_wr_com_name = "";
            _custProfile.Mbe_wr_country_cd = null;
            _custProfile.Mbe_wr_dept = "";
            _custProfile.Mbe_wr_designation = "";
            _custProfile.Mbe_wr_distric_cd = null;
            _custProfile.Mbe_wr_email = "";
            _custProfile.Mbe_wr_fax = "";
            _custProfile.Mbe_wr_proffesion = null;
            _custProfile.Mbe_wr_province_cd = null;
            _custProfile.Mbe_wr_tel = "";
            _custProfile.Mbe_wr_town_cd = null;
            _custProfile.MBE_FNAME = "";
            _custProfile.MBE_SNAME = "";
            _custProfile.MBE_INI = "";
            _custProfile.MBE_TIT = "";
            _custProfile.Mbe_agre_send_email = false;
            _custProfile.Mbe_cust_lang = "";
        }
        //private void Collect_GroupCust()
        //{
        //    _custGroup = new GroupBussinessEntity();
        //    _custGroup.Mbg_cd = txtCusCode.Text.Trim();
        //    _custGroup.Mbg_name = txtName.Text.Trim();
        //    _custGroup.Mbg_tit = cmbTitle.Text;
        //    _custGroup.Mbg_ini = txtInit.Text.Trim();
        //    _custGroup.Mbg_fname = txtFname.Text.Trim();
        //    _custGroup.Mbg_sname = txtSName.Text.Trim();
        //    _custGroup.Mbg_nationality = "SL";
        //    _custGroup.Mbg_add1 = txtPreAdd1.Text.Trim();
        //    _custGroup.Mbg_add2 = txtPreAdd2.Text.Trim();
        //    _custGroup.Mbg_town_cd = txtPerTown.Text.Trim();
        //    _custGroup.Mbg_distric_cd = txtPerDistrict.Text.Trim();
        //    _custGroup.Mbg_province_cd = txtPerProvince.Text.Trim();
        //    _custGroup.Mbg_country_cd = txtPerCountry.Text.Trim();
        //    _custGroup.Mbg_tel = txtPerPhone.Text.Trim();
        //    _custGroup.Mbg_fax = "";
        //    _custGroup.Mbg_postal_cd = txtPerPostal.Text.Trim();
        //    _custGroup.Mbg_mob = txtMob.Text.Trim();
        //    _custGroup.Mbg_nic = txtNIC.Text.Trim();
        //    _custGroup.Mbg_pp_no = txtPP.Text.Trim();
        //    _custGroup.Mbg_dl_no = txtDL.Text.Trim();
        //    _custGroup.Mbg_br_no = txtBR.Text.Trim();
        //    _custGroup.Mbg_email = txtPerEmail.Text.Trim();
        //    _custGroup.Mbg_contact = "";
        //    _custGroup.Mbg_act = true;
        //    _custGroup.Mbg_is_suspend = false;
        //    _custGroup.Mbg_sex = cmbSex.Text;
        //    _custGroup.Mbg_dob = Convert.ToDateTime(dtpDOB.Text).Date;
        //    _custGroup.Mbg_cre_by = BaseCls.GlbUserID;
        //    _custGroup.Mbg_mod_by = BaseCls.GlbUserID;

        //}
        public static bool IsValidMobileOrLandNo(string mobile)
        {
            string pattern = @"^[0-9]{10}$";

            System.Text.RegularExpressions.Match match = Regex.Match(mobile.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }

        private void linknewcust_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                General.CustomerCreation _CusCre = new General.CustomerCreation();
                _CusCre._isFromOther = true;
                _CusCre.obj_TragetTextBox = txt_d_cust_cd;
                this.Cursor = Cursors.Default;
                _CusCre.ShowDialog();
                txt_d_cust_cd.Select();
                //   if (chkDeliverLater.Checked) txtItem.Focus(); else txtSerialNo.Focus();
            }
            catch (Exception ex)
            { txt_d_cust_cd.Clear(); this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txt_d_cust_cd_TextChanged(object sender, EventArgs e)
        {
            Load_cust_details(txt_d_cust_cd.Text.Trim().ToString());
        }

        private void chkEditAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEditAddress.Checked == true)
            {
                txt_d_cust_add1.ReadOnly = false;
                txt_d_cust_add2.ReadOnly = false;
            }
            else
            {
                txt_d_cust_add1.ReadOnly = true;
                txt_d_cust_add2.ReadOnly = true;
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void DeliverySchedule_Load(object sender, EventArgs e)
        {
            btnGetInvoices_Click(null, null);
        }
    }
}
