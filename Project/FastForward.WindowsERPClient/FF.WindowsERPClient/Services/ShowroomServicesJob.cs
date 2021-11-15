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

namespace FF.WindowsERPClient.Services
{
    public partial class ShowroomServicesJob : Base
    {
        //pkg_search.sp_search_AcJob
        private string BackDates_MODULE_name = "";
        private string jobStatus;

        public string JobStatus
        {
            // N- NEW, P-PENDING, A-APPROVED
            get { return jobStatus; }
            set { jobStatus = value; }
        }

        private DataTable selectedItem;

        public DataTable SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; }
        }
        private List<PaidClaimList> paidClaimedList;
        public List<PaidClaimList> PaidClaimedList
        {
            get { return paidClaimedList; }
            set { paidClaimedList = value; }
        }

        private List<AddtionalCharge> additionalChargesList;
        public List<AddtionalCharge> AdditionalChargesList
        {
            get { return additionalChargesList; }
            set { additionalChargesList = value; }
        }

        private DataTable finalChargesTypeTable;
        public DataTable FinalChargesTypeTable
        {
            get { return finalChargesTypeTable; }
            set { finalChargesTypeTable = value; }
        }

        private Decimal chargesToCustomer;
        public Decimal ChargesToCustomer
        {
            get { return chargesToCustomer; }
            set
            {
                chargesToCustomer = value;
                txtChargesToCustomer.Text = string.Format("{0:n2}", value);
            }
        }
        private Decimal chargesToManager;
        public Decimal ChargesToManager
        {
            get { return chargesToManager; }
            set
            {
                chargesToManager = value;
                txtChargesToManager.Text = string.Format("{0:n2}", value);
            }
        }

        private Decimal chargesTotal;
        public Decimal ChargesTotal
        {
            get { return chargesTotal; }
            set
            {
                chargesTotal = value;
                txtChargesTotal.Text = string.Format("{0:n2}", value);
            }
        }
        // txtCustomerPayment.Text= string.Format("{0:n2}", 0); 
        //txtManagerPayment.Text= string.Format("{0:n2}", 0); 
        //txtDueBalance.Text= string.Format("{0:n2}", 0); 
        private Decimal customerPayment;
        public Decimal CustomerPayment
        {
            get { return customerPayment; }
            set
            {
                customerPayment = value;
                txtCustomerPayment.Text = string.Format("{0:n2}", value);
            }
        }

        private Decimal managerPayment;
        public Decimal ManagerPayment
        {
            get { return managerPayment; }
            set
            {
                managerPayment = value;
                txtManagerPayment.Text = string.Format("{0:n2}", value);
            }
        }

        private Decimal dueBalance;
        public Decimal DueBalance
        {
            get { return dueBalance; }
            set
            {
                dueBalance = value;
                txtDueBalance.Text = string.Format("{0:n2}", value);
            }
        }

        private List<RecieptItem> saveRecieptItemList;

        public List<RecieptItem> SaveRecieptItemList
        {
            get { return saveRecieptItemList; }
            set { saveRecieptItemList = value; }
        }

        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        public class AddtionalCharge
        {
            public string Description { get; set; }
            public Decimal Amount { get; set; }
        }

        public class PaidClaimList
        {
            public string Paid_Claim_Party { get; set; }
            public string PartyCode { get; set; }
            public Decimal Amount { get; set; }
            public DateTime Date { get; set; }
        }
        private List<ServiceCostSheet> chargeItemsList;
        public List<ServiceCostSheet> ChargeItemsList
        {
            get { return chargeItemsList; }
            set { chargeItemsList = value; }
        }


        private void ShowroomServicesJob_Load(object sender, EventArgs e)
        {
            try
            {
                BackDates_MODULE_name = this.GlbModuleName;
                if (BackDates_MODULE_name == null)
                {
                    BackDates_MODULE_name = "m_Trans_Service_ACServiceJobs";
                }

                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, BackDates_MODULE_name, pickedDate, toolStripLabel_BD, string.Empty, out _allowCurrentTrans);
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
        public ShowroomServicesJob()
        {
            try
            {
                InitializeComponent();
                pickedDate.Enabled = true;
                pickedDate.Value = CHNLSVC.Security.GetServerDateTime().Date;

                txtJobDate.Text = pickedDate.Value.ToShortDateString();
                // txtJobDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToShortDateString();

                txtLoc.Text = BaseCls.GlbUserDefLoca;
                jobStatus = "N";//NEW

                panelNewJob.Visible = false;
                btnUpdate.Enabled = false;

                FinalChargesTypeTable = new DataTable();
                ucPayModes1.Visible = false;

                CustomerPayment = 0;
                ManagerPayment = 0;
                DueBalance = 0;

                ChargesToCustomer = 0;
                ChargesToManager = 0;
                ChargesTotal = 0;
                //----------------------------------------------------------------------------------------
                DataTable datasource = CHNLSVC.Sales.Get_all_jobTypes(BaseCls.GlbUserComCode, "AC");

                if (datasource != null && datasource.Rows.Count > 0)
                {
                    foreach (DataRow dr in datasource.Rows)
                    {
                        ComboboxItem item = new ComboboxItem();
                        item.Text = dr["sit_desc"].ToString();
                        item.Value = dr["sit_itm_tp"].ToString();

                        //  ddlJobTypes.Items.Add(item);
                    }
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
            //BackDates_MODULE_name = this.GlbModuleName;
            //if (BackDates_MODULE_name==null)
            //{
            //    BackDates_MODULE_name = "m_Trans_Service_ACServiceJobs";
            //}

            //IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, BackDates_MODULE_name, pickedDate, toolStripLabel_BD, string.Empty);
        }
        private bool IsBackDateOk()
        {
            try
            {
                bool _isOK = true;
                string currDate = Convert.ToDateTime(pickedDate.Text.Trim()).ToShortDateString();
                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, BackDates_MODULE_name, pickedDate, toolStripLabel_BD, currDate,out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (pickedDate.Value.Date != DateTime.Now.Date)
                        {
                            pickedDate.Enabled = true;
                            MessageBox.Show("Back date not allowed for selected date, for this profit center " + BaseCls.GlbUserDefLoca + "!.", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            pickedDate.Focus();
                            _isOK = false;
                            return _isOK;
                        }
                    }
                    else
                    {
                        txtInvDatePopup.Enabled = true;
                        MessageBox.Show("Back date not allow for selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtInvDatePopup.Focus();
                        return _isOK;

                    }
                }

                return _isOK;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                return false;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void txtSearchInviceNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                string invNo = txtSearchInviceNo.Text;
                DataTable dt = CHNLSVC.Sales.GetInvoiceServiceItemSerDet(invNo, null);
                if (dt == null)
                {
                    //dt = CHNLSVC.Sales.GetInvoiceServiceItemSerDet_POS(invNo, null);
                    dt = CHNLSVC.Sales.GetInvoiceServiceItemSerDet_Oth(invNo, null);
                }
                if (dt != null)
                {
                    if (dt.Rows.Count < 1)
                    {
                        //dt = CHNLSVC.Sales.GetInvoiceServiceItemSerDet_POS(invNo, null);
                        dt = CHNLSVC.Sales.GetInvoiceServiceItemSerDet_Oth(invNo, null);
                    }
                }

                //kapila 2/10/2013
                InvoiceHeader _hdrs = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtSearchInviceNo.Text);
                if (_hdrs != null)
                {
                    txtInvDatePopup.Text = _hdrs.Sah_dt.ToString("dd/MM/yyyy");
                }

                grvItemSelect.AutoGenerateColumns = false;
                grvItemSelect.DataSource = null;
                grvItemSelect.DataSource = dt;
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

        private void ddlJobTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                grvChargesPopup.DataSource = null;
                ComboboxItem ITM = (ComboboxItem)ddlJobTypes.SelectedItem;
                // MessageBox.Show(ITM.Value.ToString());
                string itemCd = "";
                string serID = null;
                Int32 COUNT = 0;
                foreach (DataGridViewRow dgvr in grvItemSelect.Rows)
                {
                    DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        COUNT = COUNT + 1;
                        itemCd = dgvr.Cells["ItmCd"].Value.ToString();
                        serID = dgvr.Cells["serialID"].Value.ToString();

                    }
                }
                if (COUNT > 1)
                {
                    MessageBox.Show("Please Select only one Item!", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (COUNT == 0)
                {
                    MessageBox.Show("Please Select An Item!", "Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                // DataTable dt = CHNLSVC.Sales.Get_item_servicChargeInfo("LGACWT2465LWN", ITM.Value.ToString());

                DataTable dt = CHNLSVC.Sales.Get_item_servicChargeInfo(itemCd, ITM.Value.ToString());
                if (dt == null)
                {
                    MessageBox.Show("No charges defined for the selected item!\nPlease contact inventory department...", "Charge Defintions", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (dt.Rows.Count < 1)
                {
                    MessageBox.Show("No charges defined for the selected item!\nPlease contact inventory department...", "Charge Defintions", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                grvChargesPopup.DataSource = null;
                cal_amount_and_vat(dt);

                //-------------SET SELECTED ITEM DETAILS-------------------------------------
                string invNo = txtSearchInviceNo.Text;

                DataTable selected_itemDetailTable = CHNLSVC.Sales.GetInvoiceServiceItemSerDet(invNo, serID);
                //**********************22-01-2013************************************
                if (selected_itemDetailTable == null)
                {
                    selected_itemDetailTable = CHNLSVC.Sales.GetInvoiceServiceItemSerDet_POS(invNo, serID);
                }
                if (selected_itemDetailTable != null)
                {
                    if (selected_itemDetailTable.Rows.Count < 1)
                    {
                        selected_itemDetailTable = CHNLSVC.Sales.GetInvoiceServiceItemSerDet_POS(invNo, serID);
                    }
                }
                //*********************************************************************
                lblSerialID.Text = serID;
                txtSerialNo.Text = serID;
                txtJobType.Text = ITM.Value.ToString();
                txtInvoiceNo.Text = txtSearchInviceNo.Text.Trim();
                foreach (DataRow dtRow in selected_itemDetailTable.Rows)
                {
                    //table contains only one row 
                    string itemCode = dtRow["irsm_itm_cd"].ToString();
                    string brand = dtRow["irsm_itm_brand"].ToString();
                    string model = dtRow["irsm_itm_model"].ToString();
                    string description = dtRow["irsm_itm_desc"].ToString();

                    string serialNo = dtRow["irsm_ser_1"].ToString();
                    string DO_number = dtRow["ith_doc_no"].ToString();
                    DateTime DO_date = Convert.ToDateTime(dtRow["ith_doc_date"].ToString());
                    string custTitle = dtRow["irsm_cust_prefix"].ToString();
                    string custName = dtRow["irsm_cust_name"].ToString();
                    string custCode = dtRow["ith_bus_entity"].ToString();

                    txtItmDescPopup.Text = description;
                    txtModelPopup.Text = model;
                    txtBrandPopup.Text = brand;
                    //txtCatePopup.Text=
                    //----------------------------------------------background text boxes----------------------------------
                    txtInvoiceNo.Text = invNo;
                    //txtCustInfo1.Text=
                    txtDO_no.Text = DO_number;
                    lblDoDate.Text = DO_date.ToShortDateString();
                    txtItemCode.Text = itemCode;
                    txtModel.Text = model;
                    //txtCategory.Text=
                    txtItmDesc.Text = description;
                    txtBrand.Text = brand;
                    txtSerialNo.Text = serialNo == "" ? serID : serialNo;
                    //txtWarrNo.Text=
                    //txtWarrRemarks.Text=
                    txtJobDate.Text = Convert.ToDateTime(pickedDate.Value).Date.ToShortDateString();//CHNLSVC.Security.GetServerDateTime().Date.ToShortDateString();
                    txtJobStatus.Text = "New Job";
                    txtJobType.Text = ddlJobTypes.SelectedText;
                    txtLoc.Text = BaseCls.GlbUserDefLoca;

                    txtCustTitlePopup.Text = custTitle;
                    txtCustInfo2Popup.Text = custName;
                    txtCustInfo1Popup.Text = custCode;

                    txtCustTitle.Text = custTitle;
                    txtCustInfo1.Text = custCode;
                    txtCustInfo2.Text = custName;

                    txtJobType.Text = ITM.Value.ToString();
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
            //---------------------------------------------------------------------------
        }

        private void cal_amount_and_vat(DataTable chargeTypesTable)
        {
            try
            {
                foreach (DataRow dtRow in chargeTypesTable.Rows)
                {
                    //TODO: CALL AMOUNT AND VAT CALCULATION METHODS.
                    string ChgItmCode = dtRow["misv_sevitm_cd"].ToString(); //dtRow["mi_cd"].ToString(); //CHANGED 

                    DataTable DT = CHNLSVC.Sales.Get_AC_chargeItem_VAT(BaseCls.GlbUserComCode, ChgItmCode, Convert.ToDateTime(pickedDate.Value).Date);
                    if (DT != null)
                    {
                        if (DT.Rows.Count > 0)
                        {
                            Decimal ChgItmPrice = Convert.ToDecimal(DT.Rows[0]["SAPD_ITM_PRICE"].ToString());//
                            Decimal ChgItmTax = Convert.ToDecimal(DT.Rows[0]["MICT_TAX_RATE"].ToString());
                            dtRow["mi_std_price"] = ChgItmPrice;
                            dtRow["mi_std_cost"] = ChgItmTax;
                            dtRow["misv_chg_pty"] = dtRow["misv_chg_pty"].ToString() == "M" ? "MANAGER" : "CUSTOMER";
                        }
                        else
                        {
                            dtRow["mi_std_price"] = 0;
                            dtRow["mi_std_cost"] = 0;
                            dtRow["misv_chg_pty"] = dtRow["misv_chg_pty"].ToString() == "M" ? "MANAGER" : "CUSTOMER";
                        }
                    }
                    else
                    {
                        dtRow["mi_std_price"] = 0;
                        dtRow["mi_std_cost"] = 0;
                        dtRow["misv_chg_pty"] = dtRow["misv_chg_pty"].ToString() == "M" ? "MANAGER" : "CUSTOMER";
                    }

                    DataTable SEVitm = CHNLSVC.Sales.GetItemCode(BaseCls.GlbUserComCode, dtRow["misv_sevitm_cd"].ToString());
                    if (SEVitm.Rows.Count>0)  //kapila
                    {
                        dtRow["mi_shortdesc"] = SEVitm.Rows[0]["mi_shortdesc"].ToString();
                    }
                }
                grvChargesPopup.AutoGenerateColumns = false;
                grvChargesPopup.DataSource = null;
                grvChargesPopup.DataSource = chargeTypesTable;
                FinalChargesTypeTable = chargeTypesTable;
                SelectedItem = chargeTypesTable;
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

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                //  FinalChargesTypeTable = new DataTable();           
                Int32 count = 0;
                List<string> selectedChargeCodes = new List<string>();
                string itemCd = "";
                foreach (DataGridViewRow dgvr in grvChargesPopup.Rows)
                {
                    DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;

                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        count = count + 1;
                        string chargeCode = dgvr.Cells["MISV_SEVITM_CD"].Value.ToString();
                        itemCd = dgvr.Cells["mi_cd"].Value.ToString();

                        selectedChargeCodes.Add(chargeCode);


                        //var query = from p in FinalChargesTypeTable.AsEnumerable()

                        //            where p.Field<string>("MISV_SEVITM_CD").Trim() == chargeCode

                        //            select p;

                        //if (query.Count<DataRow>() > 0)
                        //{
                        //    //Creating a table from the query 
                        //    FinalChargesTypeTable = new DataTable();
                        //    FinalChargesTypeTable = query.CopyToDataTable<DataRow>();
                        //}
                    }
                }
                ComboboxItem ITM = (ComboboxItem)ddlJobTypes.SelectedItem;
                try
                {
                    FinalChargesTypeTable = CHNLSVC.Sales.Get_item_servicChargeInfo_New(itemCd, ITM.Value.ToString(), selectedChargeCodes);
                }
                catch (Exception ex)
                {
                    return;
                }


                if (count < 1)
                {
                    MessageBox.Show("Please Select one or more charges!");
                    return;
                }
                //******************************************************
                chargeItemsList = new List<ServiceCostSheet>();

                foreach (DataRow dtRow in FinalChargesTypeTable.Rows)
                {
                    //dtRow["mi_std_price"] = 2500;
                    //  dtRow["mi_std_price"] = 2500;
                    // dtRow["mi_std_cost"] = 5;
                    // dtRow["misv_chg_pty"] = dtRow["misv_chg_pty"].ToString() == "M" ? "MANAGER" : "CUSTOMER";

                    DataTable SEVitm = CHNLSVC.Sales.GetItemCode(BaseCls.GlbUserComCode, dtRow["misv_sevitm_cd"].ToString());
                    string sevItmDesc="";
                    if (SEVitm.Rows.Count > 0)  //kapila
                    {
                        sevItmDesc = SEVitm.Rows[0]["mi_shortdesc"].ToString();
                    }
                    ServiceCostSheet chargeItem = new ServiceCostSheet();
                    chargeItem.Scs_act = true;
                    chargeItem.Scs_anal1 = dtRow["misv_chg_pty"].ToString() == "C" ? "CUSTOMER" : "MANAGER";
                    //chargeItem.Scs_anal2 = lblSerialID.Text;
                    //chargeItem.Scs_anal3
                    //chargeItem.Scs_anal4
                    //chargeItem.Scs_anal5

                    //Decimal VAT = Convert.ToDecimal(dtRow["mi_std_price"].ToString()) * Convert.ToDecimal(dtRow["mi_std_cost"].ToString()) / 100;

                    //Decimal VAT = Convert.ToDecimal(SEVitm.Rows[0]["mi_std_price"].ToString()) * Convert.ToDecimal(SEVitm.Rows[0]["mi_std_cost"].ToString()) / 100;
                    Decimal VAT = 0;
                    Decimal Chg_itmPrice = 0;
                    try
                    {
                        Decimal VAT_rate = CHNLSVC.Sales.GET_Item_vat_Rate(BaseCls.GlbUserComCode, dtRow["misv_sevitm_cd"].ToString(), "VAT");
                        DataTable Chg_itmPriceTB = CHNLSVC.Sales.get_priceDet_ForAC_sevChgItG(BaseCls.GlbUserComCode, dtRow["misv_sevitm_cd"].ToString(), Convert.ToDateTime(txtJobDate.Text).Date);
                        Chg_itmPrice = Convert.ToDecimal(Chg_itmPriceTB.Rows[0]["sapd_itm_price"].ToString());
                        VAT = (Chg_itmPrice * VAT_rate) / 100;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Vat and Price is not defined for charge item: " + dtRow["misv_sevitm_cd"].ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);// SAR_PB_DET, mst_itm_comtax
                        VAT = 0;
                        Chg_itmPrice = 0;
                        return;
                    }

                    chargeItem.Scs_anal6 = VAT;//VAT


                    //chargeItem.Scs_balqty
                    chargeItem.Scs_com = BaseCls.GlbUserComCode;
                    //chargeItem.Scs_consumablecd
                    chargeItem.Scs_cuscd = txtCustInfo1.Text;
                    chargeItem.Scs_cusname = txtCustInfo2.Text;
                    chargeItem.Scs_direc = "1";
                    chargeItem.Scs_docdt = Convert.ToDateTime(txtJobDate.Text).Date;
                    //chargeItem.Scs_docno
                    //chargeItem.Scs_docqty
                    //chargeItem.Scs_doctp
                    chargeItem.Scs_invoiceno = txtInvoiceNo.Text;
                    //chargeItem.Scs_isfoc
                    chargeItem.Scs_isinvoice = false;
                    //chargeItem.Scs_isrevitm
                    chargeItem.Scs_itmcd = dtRow["misv_sevitm_cd"].ToString(); //dtRow["mi_cd"].ToString();
                    chargeItem.Scs_itmdesc = sevItmDesc;//dtRow["mi_shortdesc"].ToString();
                    //chargeItem.Scs_itmstus
                    chargeItem.Scs_itmtp = "CHARGE";// txtJobType.Text;
                    chargeItem.Scs_jobclose = false;
                    //chargeItem.Scs_jobconfno
                    chargeItem.Scs_jobitmcd = txtItemCode.Text;
                    chargeItem.Scs_jobitmser = txtSerialNo.Text;
                    chargeItem.Scs_jobitmwarr = txtWarrNo.Text;
                    chargeItem.Scs_joblineno = 1;
                    //chargeItem.Scs_jobno TODO at BLL
                    //chargeItem.Scs_line = TODO at BLL
                    chargeItem.Scs_loc = BaseCls.GlbUserDefLoca;
                    //chargeItem.Scs_movelineno
                    //chargeItem.Scs_oldline
                    //chargeItem.Scs_outdocline
                    chargeItem.Scs_outdocno = txtDO_no.Text; //not sure 
                    chargeItem.Scs_qty = 1;
                    chargeItem.Scs_unitcost = Chg_itmPrice;//Convert.ToDecimal(SEVitm.Rows[0]["mi_std_price"].ToString());//Convert.ToDecimal(dtRow["mi_std_price"].ToString()); mi_std_price
                    chargeItem.Scs_totunitcost = (chargeItem.Scs_unitcost * VAT / 100) + chargeItem.Scs_unitcost;// chargeItem.Scs_unitcost + chargeItem.Scs_anal6;

                    //chargeItem.Scs_uom=;


                    chargeItemsList.Add(chargeItem);

                }
                //*********************************************************************
                grvFinalServCharges.DataSource = null;
                grvFinalServCharges.AutoGenerateColumns = false;
                // grvFinalServCharges.DataSource = FinalChargesTypeTable;
                grvFinalServCharges.DataSource = chargeItemsList;
                //---------------------------------------------------------------------------------
                CustomerPayment = 0;
                ManagerPayment = 0;
                DueBalance = 0;

                ChargesToCustomer = 0;
                ChargesToManager = 0;
                ChargesTotal = 0;

                //foreach (DataRow dtRow in FinalChargesTypeTable.Rows)
                //{
                //    Decimal Amount = Convert.ToDecimal(dtRow["mi_std_price"].ToString());
                //    Decimal Vat = Convert.ToDecimal(dtRow["mi_std_cost"].ToString());
                //    string PaidBy = dtRow["misv_chg_pty"].ToString();
                //    if (PaidBy == "C" || PaidBy == "CUSTOMER")
                //    {
                //        ChargesToCustomer = ChargesToCustomer + Amount + (Amount * Vat / 100);
                //    }
                //    else //PaidBy == "M"
                //    {
                //        ChargesToManager = ChargesToManager + Amount + (Amount * Vat / 100);
                //    }
                //}

                foreach (ServiceCostSheet scs in chargeItemsList)
                {
                    Decimal Amount = scs.Scs_unitcost;// Convert.ToDecimal(dtRow["mi_std_price"].ToString());
                    Decimal Vat = scs.Scs_anal6;//Convert.ToDecimal(dtRow["mi_std_cost"].ToString());
                    string PaidBy = scs.Scs_anal1;// dtRow["Scs_anal1"].ToString();
                    if (PaidBy == "C" || PaidBy == "CUSTOMER")
                    {
                        ChargesToCustomer = ChargesToCustomer + Amount + (Amount * Vat / 100);
                    }
                    else //PaidBy == "M"
                    {
                        ChargesToManager = ChargesToManager + Amount + (Amount * Vat / 100);
                    }
                }

                //-------------------------------------------------------------
                //foreach (DataRow dtRow in FinalChargesTypeTable.Rows)
                //{
                //    DataTable SEVitm = CHNLSVC.Sales.GetItemCode(BaseCls.GlbUserComCode, dtRow["misv_sevitm_cd"].ToString());

                //    Decimal Amount = Convert.ToDecimal(SEVitm.Rows[0]["mi_std_price"].ToString()); ;//Convert.ToDecimal(dtRow["mi_std_price"].ToString());
                //    Decimal Vat = Convert.ToDecimal(SEVitm.Rows[0]["mi_std_price"].ToString()) * Convert.ToDecimal(SEVitm.Rows[0]["mi_std_cost"].ToString()) / 100; //Convert.ToDecimal(dtRow["mi_std_cost"].ToString());
                //    string PaidBy = dtRow["misv_chg_pty"].ToString();


                //    if (PaidBy == "C" || PaidBy == "CUSTOMER")
                //    {
                //        ChargesToCustomer = ChargesToCustomer + Amount + (Amount * Vat / 100);
                //    }
                //    else //PaidBy == "M"
                //    {
                //        ChargesToManager = ChargesToManager + Amount + (Amount * Vat / 100);
                //    }
                //}
                ChargesTotal = ChargesToCustomer + ChargesToManager;

                panelNewJob.Visible = false;
                btnNew.Enabled = false;
                btnCompleate.Enabled = false;
                btnApprove.Enabled = false;
                btnReject.Enabled = false;

                btnSave.Enabled = true;
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

        private void btnAddAditional_Click(object sender, EventArgs e)
        {
            try
            {
                //if (AdditionalChargesList == null)
                //{
                //    AdditionalChargesList = new List<AddtionalCharge>();
                //}

                //AddtionalCharge adiCharge = new AddtionalCharge();
                if (JobStatus != "N")
                {
                    MessageBox.Show("Cannot add Additional Charges after job created!");
                    return;
                }

                if (txtAddiDescript.Text.Trim() == "")
                {
                    MessageBox.Show("Please Enter Description.");
                    txtAddiDescript.Focus();
                    return;
                }
                //adiCharge.Description = txtAddiDescript.Text;

                try
                {
                    Decimal AMT = Convert.ToDecimal(txtAddiAmount.Text);
                    ChargesToCustomer = ChargesToCustomer + AMT;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid Amount!");
                    txtAddiAmount.Focus();
                    return;
                }
                //AdditionalChargesList.Add(adiCharge);

                //********************************************************************
                ServiceCostSheet chargeItem = new ServiceCostSheet();
                chargeItem.Scs_act = true;
                chargeItem.Scs_anal1 = "CUSTOMER";
                //chargeItem.Scs_anal2 = lblSerialID.Text;
                //chargeItem.Scs_anal3
                //chargeItem.Scs_anal4
                chargeItem.Scs_anal5 = 0;
                //chargeItem.Scs_anal6
                //chargeItem.Scs_balqty
                chargeItem.Scs_com = BaseCls.GlbUserComCode;
                //chargeItem.Scs_consumablecd
                chargeItem.Scs_cuscd = txtCustInfo1.Text;
                chargeItem.Scs_cusname = txtCustInfo2.Text;
                chargeItem.Scs_direc = "1";
                chargeItem.Scs_docdt = Convert.ToDateTime(txtJobDate.Text).Date;
                //chargeItem.Scs_docno
                //chargeItem.Scs_docqty
                //chargeItem.Scs_doctp
                chargeItem.Scs_invoiceno = txtInvoiceNo.Text;
                chargeItem.Scs_isfoc = false;
                chargeItem.Scs_isinvoice = false;
                chargeItem.Scs_isrevitm = false;
                chargeItem.Scs_itmcd = "";
                chargeItem.Scs_itmdesc = txtAddiDescript.Text;
                //chargeItem.Scs_itmstus
                chargeItem.Scs_itmtp = "ADDITION";// txtJobType.Text;
                chargeItem.Scs_jobclose = false;
                //chargeItem.Scs_jobconfno
                chargeItem.Scs_jobitmcd = txtItemCode.Text;
                chargeItem.Scs_jobitmser = txtSerialNo.Text;
                chargeItem.Scs_jobitmwarr = txtWarrNo.Text;
                chargeItem.Scs_joblineno = 1;
                //chargeItem.Scs_jobno TODO at BLL
                //chargeItem.Scs_line = TODO at BLL
                chargeItem.Scs_loc = BaseCls.GlbUserDefLoca;
                //chargeItem.Scs_movelineno
                //chargeItem.Scs_oldline
                //chargeItem.Scs_outdocline
                chargeItem.Scs_outdocno = txtDO_no.Text; //not sure
                chargeItem.Scs_qty = 1;
                chargeItem.Scs_totunitcost = Convert.ToDecimal(txtAddiAmount.Text); //WITH TAX/VAT
                chargeItem.Scs_unitcost = Convert.ToDecimal(txtAddiAmount.Text);
                //chargeItem.Scs_uom=;

                if (chargeItemsList == null)
                { chargeItemsList = new List<ServiceCostSheet>(); };
                chargeItemsList.Add(chargeItem);
                //********************************************************************
                var _match = (from _lsst in chargeItemsList
                              where _lsst.Scs_itmtp == "ADDITION"
                              select _lsst);

                List<ServiceCostSheet> AddiList = new List<ServiceCostSheet>();

                if (_match != null)
                {
                    foreach (ServiceCostSheet _one in _match)
                    {
                        AddiList.Add(_one);
                    }
                }

                grvAdditionalCharges.AutoGenerateColumns = false;
                grvAdditionalCharges.DataSource = null;
                grvAdditionalCharges.DataSource = AddiList;

                txtAddiDescript.Text = "";
                txtAddiAmount.Text = string.Format("{0:n2}", 0);

                //txtCustomerPayment.Text= string.Format("{0:n2}", 0); 
                //txtManagerPayment.Text= string.Format("{0:n2}", 0); 
                //txtDueBalance.Text= string.Format("{0:n2}", 0);


                //txtChargesToCustomer.Text=;
                //txtChargesToManager.Text=;
                //txtChargesTotal.Text=;
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

        private void grvAdditionalCharges_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                //TODO: delete from the list
                int row_id = e.RowIndex;
                Decimal amount = (Decimal)grvAdditionalCharges.Rows[row_id].Cells["Scs_totunitcost"].Value;
                string descript = grvAdditionalCharges.Rows[row_id].Cells["Scs_itmdesc"].Value.ToString();

                // AdditionalChargesList.RemoveAll(x => x.Amount == amount && x.Description == descript);
                chargeItemsList.RemoveAll(x => x.Scs_totunitcost == amount && x.Scs_itmdesc == descript && x.Scs_itmtp == "ADDITION");
                ChargesToCustomer = ChargesToCustomer - amount;

                var _match = (from _lsst in chargeItemsList
                              where _lsst.Scs_itmtp == "ADDITION"
                              select _lsst);

                List<ServiceCostSheet> AddiList = new List<ServiceCostSheet>();

                if (_match != null)
                {
                    foreach (ServiceCostSheet _one in _match)
                    {
                        AddiList.Add(_one);
                    }
                }

                grvAdditionalCharges.AutoGenerateColumns = false;
                grvAdditionalCharges.DataSource = null;
                grvAdditionalCharges.DataSource = AddiList;
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

        private void txtAddiAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.btnAddAditional_Click(null, null);
            }

        }

        private void txtAddiDescript_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txtAddiAmount.Text = "0.00";
                txtAddiAmount.Focus();
            }
        }

        private void btnPaymentsClaims_Click(object sender, EventArgs e)
        {
            try
            {
                // txtPayAmount.Text= string.Format("{0:n2}", ChargesToCustomer);
                // lblAmountToPay.Text = string.Format("{0:n2}", ChargesToCustomer);
                rdoCustomerPayment.Checked = false;
                rdoManagerPayment.Checked = false;
                btnAddPayment.Text = "Add";

                panel_payments.Visible = true;
                ucPayModes1.Visible = false;
                txtPayAmount.Focus();

                btnUpdate.Enabled = false;
                btnSave.Enabled = false;
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

        private void btnAddPayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdoCustomerPayment.Checked == false && rdoManagerPayment.Checked == false && btnAddPayment.Text == "Add")
                {
                    MessageBox.Show("Select Customer/Manager option!");
                    return;
                }

                try
                { Convert.ToDecimal(txtPayAmount.Text); }
                catch (Exception ex)
                {
                    MessageBox.Show("Enter valid amount!");
                    txtPayAmount.Focus();
                    return;
                }

                Decimal currentlyAdded_custPay = 0;
                Decimal currentlyAdded_claims = 0;
                foreach (DataGridViewRow dgvr in grvPaid_ClaimDetails.Rows)
                {
                    try
                    {
                        Decimal amt = Convert.ToDecimal(dgvr.Cells["pc_Amount"].Value.ToString());
                        string party = dgvr.Cells["party_cd"].Value.ToString();//Paid_Claim_Party
                        if (party == "C")
                        {
                            currentlyAdded_custPay = currentlyAdded_custPay + amt;
                        }
                        else
                        {
                            currentlyAdded_claims = currentlyAdded_claims + amt;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                if (rdoManagerPayment.Checked == true && btnAddPayment.Text == "Add")
                {
                    //TODO: max claim amount.
                    Decimal maxClaimRt = 100;
                    Decimal MaximumClaimVal = 0;//(ChargesToManager - ManagerPayment);

                    if (JobStatus == "N" || JobStatus == "P")
                    {
                        maxClaimRt = CHNLSVC.Sales.Get_MaxAC_ClaimRate("ACJOB", "PC", BaseCls.GlbUserDefProf, Convert.ToDateTime(pickedDate.Value).Date);
                        MaximumClaimVal = (ChargesToManager * maxClaimRt / 100);
                        if (MaximumClaimVal < Convert.ToDecimal(txtPayAmount.Text) + currentlyAdded_claims)
                        {
                            MessageBox.Show("Maximum claim rate is " + maxClaimRt + "%");
                            // txtPayAmount.Text = "";
                            return;
                        }
                    }
                    if (JobStatus == "A")
                    {
                        if (ChargesToManager < (Convert.ToDecimal(txtPayAmount.Text) + currentlyAdded_claims))
                        {
                            MessageBox.Show("Cannot exceed the total charge amount!");
                            return;
                        }

                    }

                    PaidClaimList pc_item = new PaidClaimList();
                    pc_item.Amount = 0;
                    pc_item.Amount = Convert.ToDecimal(txtPayAmount.Text);
                    pc_item.Date = Convert.ToDateTime(pickedDate.Value).Date;//CHNLSVC.Security.GetServerDateTime().Date;
                    pc_item.Paid_Claim_Party = "Manager Claim";
                    // pc_item.PartyCode = rdoCustomerPayment.Checked == true ? "C" : "M";
                    pc_item.PartyCode = "M";

                    if (PaidClaimedList == null)
                    {
                        PaidClaimedList = new List<PaidClaimList>();
                    }
                    PaidClaimedList.Add(pc_item);

                    grvPaid_ClaimDetails.AutoGenerateColumns = false;
                    grvPaid_ClaimDetails.DataSource = null;
                    grvPaid_ClaimDetails.DataSource = PaidClaimedList;

                    // btnAddPayment.Text = "Done";
                    //btnAddPayment.Enabled = false;
                    // rdoCustomerPayment.Enabled = false;
                    // rdoManagerPayment.Enabled = false;

                    btnOkPayment.Visible = true;

                    txtPayAmount.Text = "";
                    //*****************************************************
                    return;
                }
                //-----else if (rdoCustomerPayment.Checked==true)--------------------------------------------------------
                if (ucPayModes1.Visible == true)
                {

                    ucPayModes1.Visible = false;
                    btnAddPayment.Text = "Add";
                    panel_paidClaimed.Visible = true;
                    btnOkPayment.Visible = true;
                    //------------------when click Done---------------------------------
                    SaveRecieptItemList = new List<RecieptItem>();
                    SaveRecieptItemList = ucPayModes1.RecieptItemList;
                    ucPayModes1.ClearControls();

                    if (PaidClaimedList == null)
                    {
                        PaidClaimedList = new List<PaidClaimList>();
                    }
                    PaidClaimList pc_item = new PaidClaimList();
                    pc_item.Amount = 0;
                    foreach (RecieptItem rec in SaveRecieptItemList)
                    {
                        pc_item.Amount = pc_item.Amount + rec.Sard_settle_amt;
                    }
                    pc_item.Date = Convert.ToDateTime(pickedDate.Value).Date;//CHNLSVC.Security.GetServerDateTime().Date;
                    //pc_item.Paid_Claim_Party = rdoCustomerPayment.Checked == true ? "Customer Payment" : "Manager Claim";
                    pc_item.Paid_Claim_Party = "Customer Payment";
                    pc_item.PartyCode = "C";
                    PaidClaimedList.Add(pc_item);

                    grvPaid_ClaimDetails.AutoGenerateColumns = false;
                    grvPaid_ClaimDetails.DataSource = null;
                    grvPaid_ClaimDetails.DataSource = PaidClaimedList;
                    //-----------------------------------------------------------------

                    //*****************************************************
                    if (btnAddPayment.Text == "Add")
                    {
                        btnOkPayment.Visible = true;

                        rdoCustomerPayment.Enabled = true;
                        rdoManagerPayment.Enabled = true;
                    }
                    else
                    {
                        btnOkPayment.Visible = false;
                        rdoCustomerPayment.Enabled = false;
                        rdoManagerPayment.Enabled = false;
                    }
                    //*****************************************************
                }
                else
                {
                    if (currentlyAdded_custPay + (txtPayAmount.Text == "" ? 0 : Convert.ToDecimal(txtPayAmount.Text)) > ChargesToCustomer)
                    {
                        MessageBox.Show("Cannot exceed the Customer Charge amount!");
                        return;
                    }
                    ucPayModes1.Visible = true;
                    btnAddPayment.Text = "Done";
                    panel_paidClaimed.Visible = false;
                    //------------------when click Add---------------------------------
                    ucPayModes1.InvoiceType = "CS";
                    ucPayModes1.LoadPayModes();
                    ucPayModes1.TotalAmount = txtPayAmount.Text == "" ? 0 : Convert.ToDecimal(txtPayAmount.Text);
                    ucPayModes1.PayModeCombo.Focus();
                    ucPayModes1.PayModeCombo.DroppedDown = true;
                    btnOkPayment.Visible = false;

                    //*****************************************************
                    if (btnAddPayment.Text == "Add")
                    {
                        btnOkPayment.Visible = true;

                        rdoCustomerPayment.Enabled = true;
                        rdoManagerPayment.Enabled = true;

                    }
                    else
                    {
                        btnOkPayment.Visible = false;
                        rdoCustomerPayment.Enabled = false;
                        rdoManagerPayment.Enabled = false;
                    }
                    //txtPayAmount.Text = "";
                    //*****************************************************
                    //---------------------------------------------------
                }
                //btnAddPayment.Enabled = false;
                rdoCustomerPayment.Checked = false;
                rdoManagerPayment.Checked = false;


                txtPayAmount.Text = "0.00";
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

        private void ucPayModes1_ItemAdded(object sender, EventArgs e)
        {

        }

        private void btnCancelPayment_Click(object sender, EventArgs e)
        {
            ucPayModes1.ClearControls();
            panel_payments.Visible = false;
            ucPayModes1.Visible = false;
            PaidClaimedList = new List<PaidClaimList>();
            btnUpdate.Enabled = false;
            btnSave.Enabled = false;
        }

        private void btnOkPayment_Click(object sender, EventArgs e)
        {
            try
            {
                panel_payments.Visible = false;
                ucPayModes1.Visible = false;
                if (JobStatus == "N")
                {
                    #region

                    //foreach (PaidClaimList paidItm in PaidClaimedList)
                    //{
                    //    if (paidItm.PartyCode == "C")
                    //    {
                    //        CustomerPayment = CustomerPayment + paidItm.Amount;
                    //    }
                    //    else// "M"
                    //    {
                    //        ManagerPayment = ManagerPayment + paidItm.Amount;
                    //    }
                    //}
                    //*************************
                    Decimal currentlyAdded_custPay = 0;
                    Decimal currentlyAdded_claims = 0;
                    foreach (DataGridViewRow dgvr in grvPaid_ClaimDetails.Rows)
                    {
                        try
                        {
                            Decimal amt = Convert.ToDecimal(dgvr.Cells["pc_Amount"].Value.ToString());
                            string party = dgvr.Cells["party_cd"].Value.ToString();//Paid_Claim_Party
                            if (party == "C")
                            {
                                currentlyAdded_custPay = currentlyAdded_custPay + amt;
                            }
                            else
                            {
                                currentlyAdded_claims = currentlyAdded_claims + amt;
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    CustomerPayment = currentlyAdded_custPay;
                    ManagerPayment = currentlyAdded_claims;
                    //*************************
                    DueBalance = ChargesTotal - (CustomerPayment + ManagerPayment);

                    //TODO : SET ALL CHARGES PAID/CLAIMED AMOUNTS.
                    //chargeItemsList

                    Decimal totCustPayment = 0;

                    try
                    {
                        totCustPayment = (from a in SaveRecieptItemList
                                          //where a.Scs_itmtp == "CHARGE" && a.Scs_anal1 == "CUSTOMER"
                                          // where a.Sard_settle_amt == "CUSTOMER"
                                          select a.Sard_settle_amt).Sum(); //
                    }
                    catch (Exception ex)
                    {
                        totCustPayment = 0;
                    }

                    if (chargeItemsList == null)
                    {
                        return;
                    }
                    foreach (ServiceCostSheet cs in chargeItemsList)
                    {
                        if (cs.Scs_anal1 == "CUSTOMER")
                        {
                            if (totCustPayment > cs.Scs_totunitcost)
                            {
                                cs.Scs_anal5 = cs.Scs_totunitcost;
                                totCustPayment = totCustPayment - cs.Scs_totunitcost;
                            }
                            else if (totCustPayment >= 0)
                            {
                                cs.Scs_anal5 = totCustPayment;
                                totCustPayment = 0;
                            }
                        }

                    }

                    //----------------------------------------------------------
                    Decimal totManagerClaims = 0;
                    try
                    {
                        totManagerClaims = (from a in PaidClaimedList
                                            //where a.Scs_itmtp == "CHARGE" && a.Scs_anal1 == "CUSTOMER"
                                            where a.PartyCode == "M"
                                            select a.Amount).Sum(); //
                    }
                    catch (Exception ex)
                    {
                        totManagerClaims = 0;
                    }
                    foreach (ServiceCostSheet cs in chargeItemsList)
                    {
                        if (cs.Scs_anal1 == "MANAGER")
                        {
                            if (totManagerClaims > cs.Scs_totunitcost)
                            {
                                cs.Scs_anal5 = cs.Scs_totunitcost;
                                totManagerClaims = totManagerClaims - cs.Scs_totunitcost;
                            }
                            else if (totManagerClaims >= 0)
                            {
                                cs.Scs_anal5 = totManagerClaims;
                                totManagerClaims = 0;
                            }
                        }

                    }
                    if (totManagerClaims > 0) //allocate the remain to the first
                    {
                        foreach (ServiceCostSheet cs in chargeItemsList)
                        {
                            if (cs.Scs_anal1 == "MANAGER")
                            {
                                cs.Scs_anal5 = cs.Scs_anal5 + totManagerClaims;
                                break;
                            }
                        }
                    }
                    if (totCustPayment > 0)
                    {
                        foreach (ServiceCostSheet cs in chargeItemsList)
                        {
                            if (cs.Scs_anal1 == "CUSTOMER")
                            {
                                cs.Scs_anal5 = cs.Scs_anal5 + totCustPayment;
                                break;
                            }
                        }
                    }
                    #endregion

                    btnSave.Enabled = true;
                    btnUpdate.Enabled = false;
                }
                else
                {
                    //*************************
                    Decimal currentlyAdded_custPay = 0;
                    Decimal currentlyAdded_claims = 0;
                    foreach (DataGridViewRow dgvr in grvPaid_ClaimDetails.Rows)
                    {
                        try
                        {
                            Decimal amt = Convert.ToDecimal(dgvr.Cells["pc_Amount"].Value.ToString());
                            string party = dgvr.Cells["party_cd"].Value.ToString();//Paid_Claim_Party
                            if (party == "C")
                            {
                                currentlyAdded_custPay = currentlyAdded_custPay + amt;
                            }
                            else
                            {
                                currentlyAdded_claims = currentlyAdded_claims + amt;
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    CustomerPayment = currentlyAdded_custPay;
                    ManagerPayment = currentlyAdded_claims;
                    //*************************
                    btnSave.Enabled = false;
                    btnUpdate.Enabled = true;
                }
                //btnUpdate.Enabled = true;
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

        private void btnClosePayment_Click(object sender, EventArgs e)
        {

        }

        private void txtPayAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.btnAddPayment_Click(null, null);
            }

        }

        private void rdoCustomerPayment_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCustomerPayment.Checked == true)
            {
                txtPayAmount.Text = (ChargesToCustomer - CustomerPayment).ToString();
                lblAmountToPay.Text = txtPayAmount.Text;

                btnAddPayment.Enabled = true;
            }

        }

        private void rdoManagerPayment_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoManagerPayment.Checked == true)
            {
                //txtPayAmount.Text = (ChargesToManager - ManagerPayment).ToString();
                txtPayAmount.Text = string.Empty;
                lblAmountToPay.Text = (ChargesToManager - ManagerPayment).ToString();

                btnAddPayment.Enabled = true;
            }
        }

        private void btnChgCancel_Click(object sender, EventArgs e)
        {
            panelNewJob.Visible = false;
            //clearPopUpScreen_itemDet();
            this.btnClear_Click(null, null);
            panelNewJob.Height = 0;
            panelNewJob.Width = 0;

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsBackDateOk() == false)
                {
                    pickedDate.Enabled = true;
                    return;
                }
                panelNewJob.Visible = true;
                #region Clean
                txtJonNo.Text = string.Empty;
                txtInvoiceNo.Text = string.Empty;
                txtCustInfo1.Text = string.Empty;
                txtDO_no.Text = string.Empty;
                lblDoDate.Text = string.Empty;
                txtItemCode.Text = string.Empty;
                txtModel.Text = string.Empty;
                txtCategory.Text = string.Empty;
                txtItmDesc.Text = string.Empty;
                txtBrand.Text = string.Empty;
                txtSerialNo.Text = string.Empty;
                txtWarrNo.Text = string.Empty;
                txtWarrRemarks.Text = string.Empty;
                txtJobDate.Text = pickedDate.Value.ToShortDateString();//CHNLSVC.Security.GetServerDateTime().Date.ToShortDateString();
                txtJobStatus.Text = string.Empty;
                txtJobType.Text = string.Empty;
                txtLoc.Text = BaseCls.GlbUserDefLoca;
                txtCustTitlePopup.Text = string.Empty;
                txtCustInfo2Popup.Text = string.Empty;
                txtCustInfo1Popup.Text = string.Empty;
                txtCustTitle.Text = string.Empty;
                txtCustInfo1.Text = string.Empty;
                txtCustInfo2.Text = string.Empty;
                txtJobType.Text = string.Empty;

                grvAdditionalCharges.DataSource = null;
                grvFinalServCharges.DataSource = null;
                grvPaid_ClaimDetails.DataSource = null;

                grvChargesPopup.DataSource = null;
                grvItemSelect.DataSource = null;

                btnNew.Enabled = true;
                btnCompleate.Enabled = false;
                btnApprove.Enabled = false;
                btnReject.Enabled = false;

                clearPopUpScreen_itemDet();
                clearProperties();

                panelNewJob.Visible = false;
                #endregion
                //this.btnClear_Click(null, null);
                panelNewJob.Visible = true;
                panelNewJob.Width = 1019;
                panelNewJob.Height = 480;
                panelNewJob.Visible = true;

                btnSave.Enabled = false;
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
        private void update()
        {

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region
                //try
                //{
                if (IsBackDateOk() == false)
                {
                    return;
                }
                if (CustomerPayment < ChargesToCustomer)
                {
                    MessageBox.Show("Please settle the customer charges!");
                    return;
                }
                if (string.IsNullOrEmpty(txtContP.Text))
                {
                    MessageBox.Show("Please enter contact person");
                    return;
                }
                if (string.IsNullOrEmpty(txtContNo.Text))
                {
                    MessageBox.Show("Please enter contact number");
                    return;
                }
                if (string.IsNullOrEmpty(txtServAddr.Text))
                {
                    MessageBox.Show("Please enter service address");
                    return;
                }
                if (string.IsNullOrEmpty(txtInstLoc.Text))
                {
                    MessageBox.Show("Please enter install location");
                    return;
                }
                //TODO:
                //SAVE IN RECEIPT,RECEIPT ITEM TABLES
                //SAVE IN sev_job_hdr,sev_job_det TABLES

                #region Receipt AutoNumber Value Assign
                MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                //_receiptAuto.Aut_cate_tp = "PC";
                _receiptAuto.Aut_cate_tp = "PC"; //GlbUserDefProf;
                _receiptAuto.Aut_direction = 1;
                _receiptAuto.Aut_modify_dt = null;
                _receiptAuto.Aut_moduleid = "JOB";
                _receiptAuto.Aut_number = 0;
                _receiptAuto.Aut_start_char = "SVJOB";
                _receiptAuto.Aut_year = null;

                #endregion

                #region receipt headr generation
                RecieptHeader _recHeader = new RecieptHeader();
                // _recHeader.Sar_acc_no = "";
                _recHeader.Sar_acc_no = "N/A";
                _recHeader.Sar_act = true;
                _recHeader.Sar_com_cd = BaseCls.GlbUserComCode;
                _recHeader.Sar_comm_amt = 0;
                _recHeader.Sar_create_by = BaseCls.GlbUserID;
                _recHeader.Sar_create_when = pickedDate.Value.Date;//Convert.ToDateTime(pickedDate.Value).Date; //CHNLSVC.Security.GetServerDateTime().Date;
                _recHeader.Sar_currency_cd = "LKR";
                //_recHeader.Sar_currency_cd = txtCurrency.Text;
                // _recHeader.Sar_debtor_add_1 = txtPreAdd1.Text;
                // _recHeader.Sar_debtor_add_2 = txtPreAdd2.Text;
                _recHeader.Sar_debtor_cd = txtCustInfo1.Text.Trim() == string.Empty ? "NOT FOUND" : txtCustInfo1.Text.Trim();
                _recHeader.Sar_debtor_name = txtCustInfo2.Text.Trim() == string.Empty ? "NOT FOUND" : txtCustInfo2.Text.Trim();
                _recHeader.Sar_direct = true;
                _recHeader.Sar_direct_deposit_bank_cd = "";
                _recHeader.Sar_direct_deposit_branch = "";
                _recHeader.Sar_epf_rate = 0;
                _recHeader.Sar_esd_rate = 0;
                _recHeader.Sar_is_mgr_iss = false;
                //_recHeader.Sar_is_oth_shop = false;
                _recHeader.Sar_is_oth_shop = false;
                _recHeader.Sar_remarks = "AC SERVICES JOB";
                _recHeader.Sar_is_used = false;
                //_recHeader.Sar_mob_no = txtMobile.Text;
                _recHeader.Sar_mod_by = BaseCls.GlbUserID;
                _recHeader.Sar_mod_when = pickedDate.Value.Date;////Convert.ToDateTime(pickedDate.Value).Date;//CHNLSVC.Security.GetServerDateTime().Date;
                //_recHeader.Sar_nic_no = txtNIC.Text;
                //  _recHeader.Sar_prefix = "PRFX";
                //_recHeader.Sar_prefix = ddlPrefix.SelectedValue;
                _recHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                _recHeader.Sar_receipt_date = pickedDate.Value.Date;////Convert.ToDateTime(txtJobDate.Text.Trim()).Date;
                //_recHeader.Sar_receipt_no = "na";/////////////////////TODO           
                // _recHeader.Sar_manual_ref_no = txtReceiptNo.Text; //the receipt no          
                _recHeader.Sar_receipt_type = "ACJOB";
                _recHeader.Sar_ref_doc = "";
                _recHeader.Sar_remarks = "";
                _recHeader.Sar_seq_no = 1;
                _recHeader.Sar_ser_job_no = "";
                _recHeader.Sar_session_id = BaseCls.GlbUserSessionID;
                //_recHeader.Sar_tel_no = txtMobile.Text;
                _recHeader.Sar_tot_settle_amt = Math.Round(CustomerPayment, 2);//sum_receipt_amt
                _recHeader.Sar_uploaded_to_finance = false;
                _recHeader.Sar_used_amt = 0;
                _recHeader.Sar_wht_rate = 0;
                _recHeader.Sar_anal_5 = 0;
                _recHeader.Sar_comm_amt = 0;
                _recHeader.Sar_comm_amt = 0;
                _recHeader.Sar_anal_6 = 0;
                _recHeader.Sar_anal_7 = 0;
                //Fill Aanal fields and other required fieles as necessary.
                #endregion

                #region receipt item list genereation
                List<RecieptItem> receiptItems = new List<RecieptItem>();
                receiptItems = SaveRecieptItemList;
                #endregion


                #region Job AutoNumber Value Assign
                MasterAutoNumber jobAuto = new MasterAutoNumber();
                jobAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                //_receiptAuto.Aut_cate_tp = "PC";
                jobAuto.Aut_cate_tp = "PC"; //GlbUserDefProf;
                jobAuto.Aut_direction = 1;
                jobAuto.Aut_modify_dt = null;
                jobAuto.Aut_moduleid = "JOB";
                jobAuto.Aut_number = 0;
                jobAuto.Aut_start_char = "ACJOB";
                jobAuto.Aut_year = null;

                #endregion

                #region job header generation
                //------------------Job Header Filling---------------------------------
                ServiceJobHeader sevHdr = new ServiceJobHeader();
                sevHdr.Sjb_dt = pickedDate.Value.Date;////Convert.ToDateTime(txtJobDate.Text.Trim()).Date;
                sevHdr.Sjb_chnl = "AC";
                sevHdr.Sjb_job_tp = txtJobType.Text.Trim();

                sevHdr.Sjb_com = BaseCls.GlbUserComCode;
                sevHdr.Sjb_cre_by = BaseCls.GlbUserID;
                sevHdr.Sjb_cre_dt = pickedDate.Value.Date;////Convert.ToDateTime(pickedDate.Value).Date;//CHNLSVC.Security.GetServerDateTime().Date;
                sevHdr.Sjb_pc = BaseCls.GlbUserDefProf;
                sevHdr.Sjb_loc = BaseCls.GlbUserDefLoca;
                sevHdr.Sjb_job_cat = "";
                sevHdr.Sjb_stus = "P";
                //sevHdr.Sjb_chnl=
                //sevHdr.Sjb_jobno = "12"; 
                sevHdr.Sjb_cust_cd = txtCustInfo1.Text.Trim() == string.Empty ? "NOT FOUND" : txtCustInfo1.Text.Trim();
                sevHdr.Sjb_cust_name = txtCustInfo2.Text.Trim() == string.Empty ? "NOT FOUND" : txtCustInfo2.Text.Trim();
                sevHdr.Sjb_title = txtCustTitle.Text.Trim() == "" ? "N/A" : txtCustTitle.Text.Trim();
                // sevHdr.Sjb_job_rmk = txtJobRmk.Text.Trim();
                sevHdr.Sjb_job_rmk = "A/C SERVICE JOB";
                #endregion


                #region job detail generation
                //-------------------Job Detail Filling--------------------------------
                ServiceJobDetail sevDet = new ServiceJobDetail();
                sevDet.Jbd_jobline = 1; //only one item, so one jobline
                sevDet.Jbd_itm_cd = txtItemCode.Text;
                sevDet.Jbd_itm_desc = txtItmDesc.Text;
                sevDet.Jbd_model = txtModel.Text;
                sevDet.Jbd_ser1 = txtSerialNo.Text;
                sevDet.Jbd_warr_stus = true; //TODO           

                sevDet.Jbd_warrperiod = txtWarrPeriod.Text.Trim() == "" ? 0 : Convert.ToInt32(txtWarrPeriod.Text);
                sevDet.Jbd_warr = txtWarrNo.Text.Trim();//is this ok?
                sevDet.Jbd_invoiceno = txtInvoiceNo.Text.Trim();


                #endregion

                //-----------------Save Job-----------------------------------------------
                string Job_No = "";
                string Receipt_No = "";
                //----------------Job StageLog Filling-------------------------------------------------------------------------
                ServiceJobStageLog stgLog = new ServiceJobStageLog();
                stgLog.Sjl_cre_by = BaseCls.GlbUserID;
                stgLog.Sjl_cre_dt = pickedDate.Value.Date;////Convert.ToDateTime(txtJobDate.Text.Trim()).Date;
                // stgLog.Sjl_jobno= sevHdr.Sjb_jobno;
                stgLog.Sjl_jobstage = 2;
                stgLog.Sjl_loc = BaseCls.GlbUserDefLoca;
                //stgLog.Sjl_othdocno //blank
                //stgLog.Sjl_reqno  //blank
                //stgLog.Sjl_seqno //this is auto generated by a trigger
                //-------------------------------------------------------------------------------------------------------------

                //service job allocation
                ServiceJobAlloc _serJobAlloc = new ServiceJobAlloc();

                _serJobAlloc.Gni_inv_no = txtInvoiceNo.Text.Trim();
                if (!string.IsNullOrEmpty(txtInvDatePopup.Text))
                {
                    _serJobAlloc.Gni_inv_dt = Convert.ToDateTime(txtInvDatePopup.Text).Date;
                }
                else
                {
                    _serJobAlloc.Gni_inv_dt = DateTime.Now.Date;
                }
                //_serJobAlloc.Gni_do_no = 
                //_serJobAlloc.Gni_do_dt = 
                //_serJobAlloc.Gni_do_loc = 
                _serJobAlloc.Gni_cus = txtCustInfo2.Text.Trim() == string.Empty ? "NOT FOUND" : txtCustInfo2.Text.Trim();
                _serJobAlloc.Gni_cus_add = txtCustInfo2Popup.Text;
                _serJobAlloc.Gni_itm_cd = txtItemCode.Text;
                _serJobAlloc.Gni_itm_desc = txtItmDesc.Text;
                _serJobAlloc.Gni_model = txtModel.Text;
                //_serJobAlloc.Gni_cat_1 = 
                //_serJobAlloc.Gni_cat_2 = 
                //_serJobAlloc.Gni_brnd = 
                _serJobAlloc.Gni_serial = txtSerialNo.Text;
                _serJobAlloc.Gni_wara_no = txtWarrNo.Text.Trim();
                _serJobAlloc.Gni_wara_period = txtWarrPeriod.Text.Trim() == "" ? 0 : Convert.ToInt32(txtWarrPeriod.Text);
                //_serJobAlloc.Gni_wara_rem = 
                //_serJobAlloc.Gni_alloc = 
                //_serJobAlloc.Gni_job_com = 
                _serJobAlloc.Gni_job_dt = Convert.ToDateTime(pickedDate.Value).Date;
                //_serJobAlloc.Gni_job_no = 
                _serJobAlloc.Gni_is_cancel = 0;
                //_serJobAlloc.Gni_cancel_dt = 
                //_serJobAlloc.Gni_cancel_rem = 
                //_serJobAlloc.Gni_job_st_dt = 
                //_serJobAlloc.Gni_job_ed_dt = 
                //_serJobAlloc.Gni_job_stus = 
                //_serJobAlloc.Gni_hold_by = 
                //_serJobAlloc.Gni_hold_dt = 
                //_serJobAlloc.Gni_hold_rem = 
                //_serJobAlloc.Gni_re_alloc_by = 
                //_serJobAlloc.Gni_re_alloc_dt = 
                //_serJobAlloc.Gni_re_alloc_rem = 
                //_serJobAlloc.Gni_oth_rem = 
                _serJobAlloc.Gni_is_updated = 1;
                _serJobAlloc.Gni_service_loc = BaseCls.GlbUserDefLoca;
                //_serJobAlloc.Gni_alloc_index = 
                //_serJobAlloc.Gni_is_sr_job_update = 
                _serJobAlloc.GNI_INSTALL_LOC = txtInstLoc.Text;
                _serJobAlloc.GNI_SERV_ADDR = txtServAddr.Text;
                _serJobAlloc.GNI_CONT_NO = txtContNo.Text;
                _serJobAlloc.GNI_CONT_PERSON = txtContP.Text;

                Int32 effect = CHNLSVC.Sales.Save_Ac_Service_Job_New(jobAuto, sevHdr, sevDet, _receiptAuto, _recHeader, receiptItems, chargeItemsList, stgLog,_serJobAlloc, out Job_No, out Receipt_No);
                if (effect > 0)
                {
                    RemitanceSummaryDetail remObj = generate_rem_sum_Object(ManagerPayment);
                    Int32 eff2 = CHNLSVC.Financial.Save_AC_Job_ManagerClaim_Remitance(remObj);
                    MessageBox.Show("Job Saved. Job# :" + Job_No + ",  Receipt# :" + Receipt_No);

                    //*******************************************************************************
                    //TODO: WRITE TO SHEDULE TABLE
                    if (sevHdr.Sjb_job_tp == "INST")
                    {
                        ServiceJobShedule shed = new ServiceJobShedule();
                        shed.Svjs_cre_by = BaseCls.GlbUserID;
                        shed.Svjs_cre_dt = Convert.ToDateTime(pickedDate.Value).Date;//CHNLSVC.Security.GetServerDateTime().Date;
                        shed.Svjs_inv_no = sevDet.Jbd_invoiceno;
                        shed.Svjs_itm = sevDet.Jbd_itm_cd;
                        //shed.Svjs_job_no = Job_No;
                        //shed.Svjs_seq = 
                        //shed.Svjs_term
                        shed.Svjs_ser_id = txtSerialNo.Text; //Convert.ToInt32(lblSerialID.Text);
                        shed.Svjs_shed_dt = Convert.ToDateTime(pickedDate.Value).Date;//CHNLSVC.Security.GetServerDateTime().Date;
                        shed.Svjs_stus = false;
                        try
                        {
                            Int32 eff3 = CHNLSVC.Sales.Save_AC_JobShedule(shed);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else if (sevHdr.Sjb_job_tp == "FSEV")
                    {
                        //UPDATE THE SHEDULE TABLE (job no, status)
                        try
                        {
                            Int32 eff4 = CHNLSVC.Sales.Update_AC_jobItem_shedule(sevDet.Jbd_itm_cd, txtSerialNo.Text, Job_No, true);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    //*******************************************************************************
                    this.btnClear_Click(null, null);
                    txtJonNo.Text = Job_No;

                    try
                    {
                        //TODO: PRINT RECEIPT
                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                        BaseCls.GlbReportName =(BaseCls.GlbUserComCode == "SGL")? "SServiceReceiptPrints.rpt":"ServiceReceiptPrints.rpt";
                        BaseCls.GlbReportDoc = Receipt_No;//"AAZTS-SVJOB-000033";//- Receipt #
                        _view.Show();
                        _view = null;

                    }
                    catch (Exception ex)
                    {

                    }
                }
                else if (effect == -1)
                {
                    MessageBox.Show("Error Occured!");
                }
                else if (effect == -99)
                {
                    string error_msg = Receipt_No;
                    MessageBox.Show("Create new job to save!\n" + error_msg);
                    return;
                }
                else
                {
                    MessageBox.Show("Not Saved");
                }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("New Job Details are not compleate!");
                //    return;
                //}
                #endregion
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

        private void txtJonNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    ServiceJobHeader JobHeader = CHNLSVC.Sales.Get_AC_JobHeaderOnDet(txtJonNo.Text.Trim().ToUpper(), null);
                    if (JobHeader == null)
                    {
                        MessageBox.Show("Invalid JobNo");
                        return;
                    }
                    //--------------texbox value assign--------------------------------------------
                    #region texbox value assign

                    if (JobHeader.Sjb_stus == "P")
                    {
                        txtJobStatus.Text = "Pending";
                        btnReject.Enabled = true;
                        btnApprove.Enabled = true;
                        btnCompleate.Enabled = false;
                    }
                    if (JobHeader.Sjb_stus == "A")
                    {
                        txtJobStatus.Text = "Approved";
                        btnReject.Enabled = false;
                        btnCompleate.Enabled = true;
                        btnApprove.Enabled = false;
                    }
                    if (JobHeader.Sjb_stus == "R")
                    {
                        txtJobStatus.Text = "Rejected";
                        btnReject.Enabled = false;
                        btnApprove.Enabled = false;
                        btnCompleate.Enabled = false;
                    }
                    if (JobHeader.Sjb_stus == "C")
                    {
                        txtJobStatus.Text = "Compleated";
                        btnReject.Enabled = false;
                        btnApprove.Enabled = false;
                        btnCompleate.Enabled = false;
                    }
                    txtJobDate.Text = JobHeader.Sjb_dt.ToShortDateString();
                    txtJobType.Text = JobHeader.Sjb_job_tp;
                    txtLoc.Text = JobHeader.Sjb_loc;
                    txtCustInfo1.Text = JobHeader.Sjb_cust_cd;
                    txtCustInfo2.Text = JobHeader.Sjb_cust_name;
                    txtCustTitle.Text = JobHeader.Sjb_title;

                    List<ServiceJobDetail> jobDet = CHNLSVC.Sales.Get_Sev_JobDet(JobHeader.Sjb_jobno);
                    txtBrand.Text = jobDet[0].Jbd_brand;

                    txtInvoiceNo.Text = jobDet[0].Jbd_invoiceno;
                    txtCustInfo1.Text = string.Empty;
                    txtDO_no.Text = string.Empty;
                    lblDoDate.Text = string.Empty;
                    txtItemCode.Text = jobDet[0].Jbd_itm_cd.ToString();
                    txtModel.Text = jobDet[0].Jbd_model; ;
                    //txtCategory.Text = jobDet[0].
                    txtItmDesc.Text = jobDet[0].Jbd_itm_desc;
                    txtBrand.Text = jobDet[0].Jbd_brand;
                    txtSerialNo.Text = jobDet[0].Jbd_ser1;
                    //txtWarrNo.Text = jobDet[0].war;
                    //txtWarrRemarks.Text = string.Empty;
                    txtJobType.Text = JobHeader.Sjb_job_tp;
                    #endregion
                    //--------------------------------------------------------------------------

                    if (JobHeader.Sjb_stus == "P" || JobHeader.Sjb_stus == "A")
                    {
                        jobStatus = JobHeader.Sjb_stus;

                        //   btnApprove.Enabled = true;
                        //   btnReject.Enabled = true;
                        List<ServiceCostSheet> Sev_Charges = CHNLSVC.Sales.Get_Sev_CostSheets(JobHeader.Sjb_jobno);
                        //--------charges bind---------------------------------------------------------------
                        var _match = (from _lsst in Sev_Charges
                                      where _lsst.Scs_itmtp == "ADDITION"
                                      select _lsst);

                        List<ServiceCostSheet> AddiList = new List<ServiceCostSheet>();

                        if (_match != null)
                        {
                            foreach (ServiceCostSheet _one in _match)
                            {
                                AddiList.Add(_one);
                            }
                        }

                        grvAdditionalCharges.AutoGenerateColumns = false;
                        grvAdditionalCharges.DataSource = null;
                        grvAdditionalCharges.DataSource = AddiList;

                        //---addtional charges bind-----------------------------------------------------------
                        var _matchChg = (from _lsst in Sev_Charges
                                         where _lsst.Scs_itmtp == "CHARGE"
                                         select _lsst);

                        List<ServiceCostSheet> ChgList = new List<ServiceCostSheet>();

                        if (_matchChg != null)
                        {
                            foreach (ServiceCostSheet _one in _matchChg)
                            {
                                ChgList.Add(_one);
                            }
                        }
                        grvFinalServCharges.DataSource = null;
                        grvFinalServCharges.AutoGenerateColumns = false;
                        // grvFinalServCharges.DataSource = FinalChargesTypeTable;
                        grvFinalServCharges.DataSource = ChgList;
                        //-----------------------------------------------------------------------------
                        CustomerPayment = 0;
                        ManagerPayment = 0;
                        DueBalance = 0;
                        Decimal custChg = (from a in Sev_Charges
                                           //where a.Scs_itmtp == "CHARGE" && a.Scs_anal1 == "CUSTOMER"
                                           where a.Scs_anal1 == "CUSTOMER"
                                           select a.Scs_totunitcost).Sum(); //
                        ChargesToCustomer = custChg;

                        Decimal managerChg = (from a in Sev_Charges
                                              //where a.Scs_itmtp == "CHARGE" && a.Scs_anal1 == "CUSTOMER"
                                              where a.Scs_anal1 == "MANAGER"
                                              select a.Scs_totunitcost).Sum(); //
                        ChargesToManager = managerChg;

                        ChargesTotal = ChargesToCustomer + ChargesToManager;


                        Decimal custPaid = (from a in Sev_Charges
                                            //where a.Scs_itmtp == "CHARGE" && a.Scs_anal1 == "CUSTOMER"
                                            where a.Scs_anal1 == "CUSTOMER"
                                            select a.Scs_anal5).Sum();
                        CustomerPayment = custPaid;

                        Decimal managerClaimed = (from a in Sev_Charges
                                                  //where a.Scs_itmtp == "CHARGE" && a.Scs_anal1 == "CUSTOMER"
                                                  where a.Scs_anal1 == "MANAGER"
                                                  select a.Scs_anal5).Sum();
                        ManagerPayment = managerClaimed;

                        DueBalance = ChargesTotal - ManagerPayment - CustomerPayment;


                        //-----------Paid/Claimed amounts bind-----------------------------------------------------------------
                        PaidClaimedList = new List<PaidClaimList>();
                        PaidClaimList pcdet = new PaidClaimList();
                        pcdet.PartyCode = "C";
                        pcdet.Paid_Claim_Party = "CUSTOMER PAYMENTS";
                        pcdet.Amount = CustomerPayment;
                        pcdet.Date = Sev_Charges[0].Scs_docdt;
                        PaidClaimedList.Add(pcdet);

                        PaidClaimList pcdet2 = new PaidClaimList();
                        pcdet2.PartyCode = "M";
                        pcdet2.Paid_Claim_Party = "MANAGER CLAIMS";
                        pcdet2.Amount = ManagerPayment;
                        pcdet2.Date = Sev_Charges[0].Scs_docdt;
                        PaidClaimedList.Add(pcdet2);
                        txtOldClaimAmount.Text = ManagerPayment.ToString();

                        grvPaid_ClaimDetails.DataSource = null;
                        grvPaid_ClaimDetails.AutoGenerateColumns = false;
                        grvPaid_ClaimDetails.DataSource = PaidClaimedList;
                        //-----------------------------------------------------------------------------
                        btnNew.Enabled = false;
                        btnUpdate.Enabled = true;
                        btnSave.Enabled = false;
                    }
                    else
                    {
                        //  btnApprove.Enabled = false;
                        //  btnCompleate.Enabled = false;
                        //  btnSave.Enabled = false;
                        //  btnUpdate.Enabled = true;
                        if (JobHeader.Sjb_stus == "C")
                        {
                            MessageBox.Show("This is a Compleated Job.");
                            this.btnClear_Click(null, null);
                        }
                    }

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

        private void btnClear_Click(object sender, EventArgs e)
        {
            //ShowroomServicesJob formnew = new ShowroomServicesJob();
            //formnew.MdiParent = this.MdiParent;
            //formnew.Location = this.Location;
            //formnew.Show();
            //this.Close();

            try
            {
                pickedDate.Value = CHNLSVC.Security.GetServerDateTime().Date;
                txtJobDate.Text = pickedDate.Value.ToShortDateString();
                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, BackDates_MODULE_name, pickedDate, toolStripLabel_BD, string.Empty, out _allowCurrentTrans);

                txtJonNo.Text = string.Empty;
                txtInvoiceNo.Text = string.Empty;
                txtCustInfo1.Text = string.Empty;
                txtDO_no.Text = string.Empty;
                lblDoDate.Text = string.Empty;
                txtItemCode.Text = string.Empty;
                txtModel.Text = string.Empty;
                txtCategory.Text = string.Empty;
                txtItmDesc.Text = string.Empty;
                txtBrand.Text = string.Empty;
                txtSerialNo.Text = string.Empty;
                txtWarrNo.Text = string.Empty;
                txtWarrRemarks.Text = string.Empty;
                txtJobDate.Text = CHNLSVC.Security.GetServerDateTime().Date.ToShortDateString();
                txtJobStatus.Text = string.Empty;
                txtJobType.Text = string.Empty;
                txtLoc.Text = BaseCls.GlbUserDefLoca;
                txtCustTitlePopup.Text = string.Empty;
                txtCustInfo2Popup.Text = string.Empty;
                txtCustInfo1Popup.Text = string.Empty;
                txtCustTitle.Text = string.Empty;
                txtCustInfo1.Text = string.Empty;
                txtCustInfo2.Text = string.Empty;
                txtJobType.Text = string.Empty;

                grvAdditionalCharges.DataSource = null;
                grvFinalServCharges.DataSource = null;
                grvPaid_ClaimDetails.DataSource = null;

                grvChargesPopup.DataSource = null;
                grvItemSelect.DataSource = null;

                btnNew.Enabled = true;
                btnCompleate.Enabled = false;
                btnApprove.Enabled = false;
                btnReject.Enabled = false;

                clearPopUpScreen_itemDet();
                clearProperties();

                panelNewJob.Visible = false;
                ucPayModes1.ClearControls();
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

        private void clearPopUpScreen_itemDet()
        {
            txtItmDescPopup.Text = string.Empty;
            txtModelPopup.Text = string.Empty;
            txtBrandPopup.Text = string.Empty;
            txtCatePopup.Text = string.Empty;
            txtSearchInviceNo.Text = string.Empty;
            txtInvDatePopup.Text = string.Empty;
            txtCustInfo1Popup.Text = string.Empty;
            txtCustTitlePopup.Text = string.Empty;
            txtCustInfo2Popup.Text = string.Empty;
            txtItmDescPopup.Text = string.Empty;
            txtModelPopup.Text = string.Empty;
            txtBrandPopup.Text = string.Empty;
            txtCatePopup.Text = string.Empty;
            ddlJobTypes.Items.Clear();
            grvChargesPopup.DataSource = null;
            grvItemSelect.DataSource = null;
        }
        private void clearProperties()
        {
            SelectedItem = new DataTable();
            PaidClaimedList = new List<PaidClaimList>();
            AdditionalChargesList = new List<AddtionalCharge>();
            FinalChargesTypeTable = new DataTable();
            SaveRecieptItemList = new List<RecieptItem>();
            ChargeItemsList = new List<ServiceCostSheet>();
            ChargesToCustomer = 0;
            ChargesToManager = 0;
            ChargesTotal = 0;
            CustomerPayment = 0;
            ManagerPayment = 0;
            DueBalance = 0;
            ucPayModes1.RecieptItemList = new List<RecieptItem>();
            ucPayModes1.TotalAmount = 0;
            jobStatus = "N";//NEW
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //TODO: UPDATE NEW MANAGER CLAIMS
                //this.btnClear_Click(null, null);
                if (IsBackDateOk() == false)
                {
                    return;
                }
                #region

                //TODO : SET ALL CHARGES PAID/CLAIMED AMOUNTS.
                //chargeItemsList
                Decimal totCustPayment = 0;
                if (SaveRecieptItemList == null)
                {
                    SaveRecieptItemList = new List<RecieptItem>();
                }
                try
                {
                    //totCustPayment = (from a in SaveRecieptItemList
                    //                  //where a.Scs_itmtp == "CHARGE" && a.Scs_anal1 == "CUSTOMER"
                    //                  // where a.Sard_settle_amt == "CUSTOMER"
                    //                  select a.Sard_settle_amt).Sum(); //
                    // totCustPayment = totCustPayment + CustomerPayment;

                    totCustPayment = (from a in PaidClaimedList
                                      //where a.Scs_itmtp == "CHARGE" && a.Scs_anal1 == "CUSTOMER"
                                      where a.PartyCode == "C"
                                      select a.Amount).Sum();
                    // totCustPayment = totCustPayment + CustomerPayment;
                    //CustomerPayment = totCustPayment;
                }
                catch (Exception ex)
                {
                    totCustPayment = CustomerPayment;
                }

                //if (chargeItemsList == null)
                //{
                //    chargeItemsList = CHNLSVC.Sales.Get_Sev_CostSheets(txtJonNo.Text);
                //}
                chargeItemsList = CHNLSVC.Sales.Get_Sev_CostSheets(txtJonNo.Text);

                foreach (ServiceCostSheet cs in chargeItemsList)
                {
                    if (cs.Scs_anal1 == "CUSTOMER")
                    {
                        if (totCustPayment > cs.Scs_totunitcost)
                        {
                            cs.Scs_anal5 = cs.Scs_totunitcost;
                            totCustPayment = totCustPayment - cs.Scs_totunitcost;
                        }
                        else if (totCustPayment >= 0)
                        {
                            cs.Scs_anal5 = totCustPayment;
                            totCustPayment = 0;
                        }
                    }

                }

                //----------------------------------------------------------
                Decimal totManagerClaims = 0;
                try
                {
                    totManagerClaims = (from a in PaidClaimedList
                                        //where a.Scs_itmtp == "CHARGE" && a.Scs_anal1 == "CUSTOMER"
                                        where a.PartyCode == "M"
                                        select a.Amount).Sum(); //

                    //totManagerClaims = totManagerClaims + ManagerPayment;
                    //ManagerPayment = totManagerClaims;
                }
                catch (Exception ex)
                {
                    totManagerClaims = ManagerPayment;
                }
                foreach (ServiceCostSheet cs in chargeItemsList)
                {
                    if (cs.Scs_anal1 == "MANAGER")
                    {
                        if (totManagerClaims > cs.Scs_totunitcost)
                        {
                            cs.Scs_anal5 = cs.Scs_totunitcost;
                            totManagerClaims = totManagerClaims - cs.Scs_totunitcost;
                        }
                        else if (totManagerClaims >= 0)
                        {
                            cs.Scs_anal5 = totManagerClaims;
                            totManagerClaims = 0;
                        }
                    }

                }
                #endregion

                //TODO :UPDATE CHARGE ITMES CLAIMED/PAID AMOUNTS
                Int32 eff = CHNLSVC.Sales.UpdateManagerClaims_custPayments(chargeItemsList);
                if (eff > 0)
                {
                    RemitanceSummaryDetail remObj = generate_rem_sum_Object(ManagerPayment - Convert.ToDecimal(txtOldClaimAmount.Text));
                    Int32 eff2 = CHNLSVC.Financial.Save_AC_Job_ManagerClaim_Remitance(remObj);

                    MessageBox.Show("Successfully updated!");
                }
                else
                {
                    MessageBox.Show("Not Updated!");
                }
                this.btnClear_Click(null, null);
                //this.txtJonNo_KeyPress(null, null);
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

        private void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "ACJOB") == false)
                {
                    // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Permission Denied!");
                    MessageBox.Show("Permission Denied!");
                    return;
                }
                //----------------Job StageLog Filling-------------------------------------------------------------------------
                ServiceJobStageLog stgLog = new ServiceJobStageLog();
                stgLog.Sjl_cre_by = BaseCls.GlbUserID;
                stgLog.Sjl_cre_dt = Convert.ToDateTime(txtJobDate.Text.Trim()).Date;
                // stgLog.Sjl_jobno= sevHdr.Sjb_jobno;
                stgLog.Sjl_jobstage = 3;
                stgLog.Sjl_loc = BaseCls.GlbUserDefLoca;
                //stgLog.Sjl_othdocno //blank
                //stgLog.Sjl_reqno  //blank
                //stgLog.Sjl_seqno //this is auto generated by a trigger
                //--------------------------------------------------------------------------------------------------------------

                // Int32 eff = CHNLSVC.Sales.Approve_Ac_Job(txtJonNo.Text.Trim(), "A", BaseCls.GlbUserID, CHNLSVC.Security.GetServerDateTime().Date, stgLog);
                Int32 eff = CHNLSVC.Sales.Approve_Ac_Job(txtJonNo.Text.Trim(), "A", BaseCls.GlbUserID, Convert.ToDateTime(pickedDate.Value).Date, stgLog);
                if (eff > 0)
                {
                    MessageBox.Show("Approved Successfully!");
                    this.btnClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Not Approved!");
                    this.btnClear_Click(null, null);
                }
                this.btnClear_Click(null, null);
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

        private void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtJonNo.Text.Trim() == "")
                {
                    MessageBox.Show("Enter job Number!");
                    return;
                }
                ServiceJobHeader JobHeader = CHNLSVC.Sales.Get_AC_JobHeaderOnDet(txtJonNo.Text.Trim().ToUpper(), null);
                if (JobHeader == null)
                {
                    MessageBox.Show("Invalid JobNo");
                    txtJonNo.Text = "";
                    return;
                }
                if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "ACJOB") == false)
                {
                    // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Permission Denied!");
                    MessageBox.Show("Permission Denied!");
                    return;
                }
                //----------------Job StageLog Filling-------------------------------------------------------------------------
                ServiceJobStageLog stgLog = new ServiceJobStageLog();
                stgLog.Sjl_cre_by = BaseCls.GlbUserID;
                stgLog.Sjl_cre_dt = Convert.ToDateTime(txtJobDate.Text.Trim()).Date;
                // stgLog.Sjl_jobno= sevHdr.Sjb_jobno;
                stgLog.Sjl_jobstage = 4;
                stgLog.Sjl_loc = BaseCls.GlbUserDefLoca;
                //stgLog.Sjl_othdocno //blank
                //stgLog.Sjl_reqno  //blank
                //stgLog.Sjl_seqno //this is auto generated by a trigger
                //--------------------------------------------------------------------------------------------------------------

                Int32 eff = CHNLSVC.Sales.Approve_Ac_Job(txtJonNo.Text.Trim(), "R", BaseCls.GlbUserID, Convert.ToDateTime(pickedDate.Value).Date, stgLog);
                if (eff > 0)
                {
                    MessageBox.Show("Rejected Successfully!");
                    this.btnClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Not Rejected!");
                    this.btnClear_Click(null, null);
                }
                this.btnClear_Click(null, null);
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

        private void btnCompleate_Click(object sender, EventArgs e)
        {
            try
            {
                //----------------Job StageLog Filling-------------------------------------------------------------------------
                ServiceJobStageLog stgLog = new ServiceJobStageLog();
                stgLog.Sjl_cre_by = BaseCls.GlbUserID;
                stgLog.Sjl_cre_dt = Convert.ToDateTime(txtJobDate.Text.Trim()).Date;
                // stgLog.Sjl_jobno= sevHdr.Sjb_jobno;
                stgLog.Sjl_jobstage = 5;
                stgLog.Sjl_loc = BaseCls.GlbUserDefLoca;
                //stgLog.Sjl_othdocno //blank
                //stgLog.Sjl_reqno  //blank
                //stgLog.Sjl_seqno //this is auto generated by a trigger
                //--------------------------------------------------------------------------------------------------------------

                Int32 eff = CHNLSVC.Sales.Approve_Ac_Job(txtJonNo.Text.Trim(), "C", BaseCls.GlbUserID, Convert.ToDateTime(pickedDate.Value).Date, stgLog);
                if (eff > 0)
                {
                    MessageBox.Show("Compleated Successfully!");
                    this.btnClear_Click(null, null);
                }
                else
                {
                    MessageBox.Show("Not Compleated!");
                    this.btnClear_Click(null, null);
                }

                this.btnClear_Click(null, null);
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

        private void txtJonNo_TextChanged(object sender, EventArgs e)
        {

        }
        private RemitanceSummaryDetail generate_rem_sum_Object(Decimal claimedValue)
        {

            //-----------------------------------------
            DataTable dt = CHNLSVC.Financial.GetRemSummary(BaseCls.GlbUserDefProf, Convert.ToDateTime(pickedDate.Value).Date, Convert.ToDateTime(pickedDate.Value).Date, "02");
            Decimal currentRem = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (row["rem_cd"].ToString() == "063")
                {
                    currentRem = Convert.ToDecimal(row["rem_val1"].ToString());
                }
            }
            //-----------------------------------------

            RemitanceSummaryDetail _remSumDet = new RemitanceSummaryDetail();
            Decimal _weekNo = 0;
            _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(pickedDate.Value).Date, out _weekNo, BaseCls.GlbUserComCode);

            DataTable dtESD_EPF_WHT = new DataTable();
            dtESD_EPF_WHT = CHNLSVC.Sales.Get_ESD_EPF_WHT(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(pickedDate.Value).Date);

            Decimal ESD_rt = 0; Decimal EPF_rt = 0; Decimal WHT_rt = 0;
            if (dtESD_EPF_WHT.Rows.Count > 0)
            {
                ESD_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_ESD"]);
                EPF_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_EPF"]);
                WHT_rt = Convert.ToDecimal(dtESD_EPF_WHT.Rows[0]["MPCH_WHT"]);
            }
            _remSumDet.Rem_com = BaseCls.GlbUserComCode;
            _remSumDet.Rem_pc = BaseCls.GlbUserDefProf;
            _remSumDet.Rem_dt = Convert.ToDateTime(pickedDate.Value).Date;
            _remSumDet.Rem_sec = "02";//ddlSecDef.SelectedValue;
            _remSumDet.Rem_cd = "063";//ddlRemTp.SelectedValue;
            //if (ddlSecDef.SelectedValue == "02" && ddlRemTp.SelectedValue == "013")  //col bonus
            //{
            //    _remSumDet.Rem_sh_desc = (ddlRemTp.SelectedItem.Text + "-" + txtVoucher.Text).ToString();
            //    _remSumDet.Rem_lg_desc = (ddlRemTp.SelectedItem.Text + "-" + txtVoucher.Text).ToString().ToUpper();
            //}
            //else
            //{
            // List<RemitanceSumHeading> remList=  CHNLSVC.Financial.get_rem_type_by_sec("02", 0);

            _remSumDet.Rem_sh_desc = "AC Installation and Service Manager Claim";//ddlRemTp.SelectedItem.Text;
            _remSumDet.Rem_lg_desc = "AC Installation and Service Manager Claim";//ddlRemTp.SelectedItem.Text.ToUpper();
            //}

            _remSumDet.Rem_val = claimedValue + currentRem;//Convert.ToDecimal(txtVal.Text);
            _remSumDet.Rem_val_final = claimedValue + currentRem;// Convert.ToDecimal(txtVal.Text);
            _remSumDet.Rem_week = (_weekNo + "S").ToString();
            _remSumDet.Rem_ref_no = "";
            _remSumDet.Rem_rmk = "";
            _remSumDet.Rem_cr_acc = "";
            _remSumDet.Rem_db_acc = "";
            _remSumDet.Rem_del_alw = false;
            _remSumDet.Rem_cre_by = BaseCls.GlbUserID;
            _remSumDet.Rem_cre_dt = Convert.ToDateTime(pickedDate.Value).Date;
            //if (ddlSecDef.SelectedValue == "05" && ddlRemTp.SelectedValue == "001")       //slip
            //{
            //    _remSumDet.Rem_is_sos = false;
            //}
            //else
            //{
            //    _remSumDet.Rem_is_sos = true;
            //}
            _remSumDet.Rem_is_sos = true;

            _remSumDet.Rem_is_dayend = true;
            _remSumDet.Rem_is_sun = true;
            _remSumDet.Rem_cat = 17;
            _remSumDet.Rem_add = 0;//Convert.ToDecimal(txtAdd.Text);
            _remSumDet.Rem_ded = 0;//Convert.ToDecimal(txtDeduct.Text);
            _remSumDet.Rem_net = 0;//Convert.ToDecimal(txtNet.Text);
            _remSumDet.Rem_epf = EPF_rt;
            _remSumDet.Rem_esd = ESD_rt;
            _remSumDet.Rem_wht = WHT_rt;
            _remSumDet.Rem_add_fin = 0;//Convert.ToDecimal(txtAdd.Text);
            _remSumDet.Rem_ded_fin = 0;//Convert.ToDecimal(txtDeduct.Text);
            _remSumDet.Rem_net_fin = 0;//Convert.ToDecimal(txtNet.Text);
            _remSumDet.Rem_rmk_fin = "";//txtRem.Text;
            _remSumDet.Rem_bnk_cd = "";
            _remSumDet.Rem_is_rem_sum = true;

            return _remSumDet;

        }

        private void grvItemSelect_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewCheckBoxCell chk = grvItemSelect.Rows[e.RowIndex].Cells[0] as DataGridViewCheckBoxCell;
                //  DataGridViewTextBoxCell tx = grvItemSelect.Rows[e.RowIndex].Cells[1] as DataGridViewTextBoxCell;
                //  string txt = tx.Value.ToString();

                //if (Convert.ToBoolean(chk.Value) == true)
                //{

                string itemCd = grvItemSelect.Rows[e.RowIndex].Cells["ItmCd"].Value.ToString();
                string serID = grvItemSelect.Rows[e.RowIndex].Cells["serialID"].Value.ToString();
                //-------------SET SELECTED ITEM DETAILS-------------------------------------
                string invNo = txtSearchInviceNo.Text;
                DataTable selected_itemDetailTable = CHNLSVC.Sales.GetInvoiceServiceItemSerDet(invNo, serID);
                //**********************
                if (selected_itemDetailTable == null)
                {
                    selected_itemDetailTable = CHNLSVC.Sales.GetInvoiceServiceItemSerDet_POS(invNo, serID);
                    txtSerialNo.Text = serID;
                }
                if (selected_itemDetailTable != null)
                {
                    if (selected_itemDetailTable.Rows.Count < 1)
                    {
                        selected_itemDetailTable = CHNLSVC.Sales.GetInvoiceServiceItemSerDet_POS(invNo, serID);
                        txtSerialNo.Text = serID;
                    }
                }
                //*********************************************************************              
                txtItemCode.Text = itemCd;
                lblSerialID.Text = serID;

                foreach (DataRow dtRow in selected_itemDetailTable.Rows)
                {
                    //table contains only one row 
                    string itemCode = dtRow["irsm_itm_cd"].ToString();
                    string brand = dtRow["irsm_itm_brand"].ToString();
                    string model = dtRow["irsm_itm_model"].ToString();
                    string description = dtRow["irsm_itm_desc"].ToString();

                    string serialNo = dtRow["irsm_ser_1"].ToString();
                    string DO_number = dtRow["ith_doc_no"].ToString();
                    DateTime DO_date = Convert.ToDateTime(dtRow["ith_doc_date"].ToString());
                    string custTitle = dtRow["irsm_cust_prefix"].ToString();
                    string custName = dtRow["irsm_cust_name"].ToString();
                    string custCode = dtRow["ith_bus_entity"].ToString();

                    txtItmDescPopup.Text = description;
                    txtModelPopup.Text = model;
                    txtBrandPopup.Text = brand;
                    //txtCatePopup.Text=
                    //----------------------------------------------background text boxes----------------------------------
                    txtInvoiceNo.Text = invNo;
                    //txtCustInfo1.Text=
                    txtDO_no.Text = DO_number;
                    lblDoDate.Text = DO_date.ToShortDateString();
                    txtItemCode.Text = itemCode == "" ? itemCd : itemCode;
                    txtModel.Text = model;
                    //txtCategory.Text=
                    txtItmDesc.Text = description;
                    txtBrand.Text = brand;
                    txtSerialNo.Text = serialNo;
                    //txtWarrNo.Text=
                    //txtWarrRemarks.Text=
                    txtJobDate.Text = Convert.ToDateTime(pickedDate.Value).Date.ToShortDateString();
                    txtJobStatus.Text = "New Job";
                    txtJobType.Text = ddlJobTypes.SelectedText;
                    txtLoc.Text = BaseCls.GlbUserDefLoca;

                    txtCustTitlePopup.Text = custTitle;
                    txtCustInfo2Popup.Text = custName;
                    txtCustInfo1Popup.Text = custCode;

                    txtCustTitle.Text = custTitle;
                    txtCustInfo1.Text = custCode;
                    txtCustInfo2.Text = custName;

                    //txtJobType.Text = ITM.Value.ToString();
                    //}
                }

                grvChargesPopup.DataSource = null;


                //************LOAD JOB TYPS**************************************
                ddlJobTypes.Items.Clear();
                DataTable datasource = CHNLSVC.Sales.Get_all_jobTypes(BaseCls.GlbUserComCode, "AC");

                // DataTable allShedule = CHNLSVC.Sales.Get_AC_jobItem_shedule(serID);//serialNo
                DataTable allShedule = CHNLSVC.Sales.Get_AC_jobItem_shedule(txtSerialNo.Text);
                Int32 freeCount = 0;
                if (allShedule != null)
                {
                    if (allShedule.Rows.Count > 0)
                    {
                        foreach (DataRow dtRow in allShedule.Rows)
                        {
                            //table contains only one row 
                            //string itemCode = dtRow["irsm_itm_cd"].ToString();
                            //string brand = dtRow["irsm_itm_brand"].ToString();
                            Int32 status = Convert.ToInt32(dtRow["svjs_stus"].ToString());
                            if (status == 0)
                            {
                                freeCount = freeCount + 1;
                            }
                        }
                        if (freeCount > 0)
                        {
                            //have more free services (and installation is already done)
                            foreach (DataRow dr in datasource.Rows)
                            {
                                ComboboxItem item = new ComboboxItem();
                                item.Text = dr["sit_desc"].ToString();
                                item.Value = dr["sit_itm_tp"].ToString();

                                if (item.Value.ToString() != "INST")
                                {
                                    ddlJobTypes.Items.Add(item);
                                }
                            }
                        }
                        else
                        {
                            //have No more free services (and installation is already done)
                            foreach (DataRow dr in datasource.Rows)
                            {
                                ComboboxItem item = new ComboboxItem();
                                item.Text = dr["sit_desc"].ToString();
                                item.Value = dr["sit_itm_tp"].ToString();

                                if (item.Value.ToString() != "INST" || item.Value.ToString() != "FSEV")
                                {
                                    ddlJobTypes.Items.Add(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (DataRow dr in datasource.Rows)
                        {
                            ComboboxItem item = new ComboboxItem();
                            item.Text = dr["sit_desc"].ToString();
                            item.Value = dr["sit_itm_tp"].ToString();

                            if (item.Value.ToString() != "FSEV")
                            {
                                ddlJobTypes.Items.Add(item);
                            }

                        }
                    }
                }
                else
                {
                    foreach (DataRow dr in datasource.Rows)
                    {
                        ComboboxItem item = new ComboboxItem();
                        item.Text = dr["sit_desc"].ToString();
                        item.Value = dr["sit_itm_tp"].ToString();

                        if (item.Value.ToString() != "FSEV")
                        {
                            ddlJobTypes.Items.Add(item);
                        }

                    }
                }
                //**************************************************
            }
            catch (Exception ex)
            {
                ddlJobTypes.Items.Clear();
                CHNLSVC.CloseChannel();

            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

            grvItemSelect.CommitEdit(DataGridViewDataErrorContexts.Commit);
            grvItemSelect.EndEdit();
            //---------------------------------------------------------------------------


        }


        private void grvPaid_ClaimDetails_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.ColumnIndex == 0 && e.RowIndex != -1)
            //{              

            //   if (PaidClaimedList[e.RowIndex].PartyCode!="C")
            //   {
            //       PaidClaimedList.RemoveAt(e.RowIndex);                    
            //   }
            //}
        }

        private void grvItemSelect_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grvItemSelect_SelectionChanged(object sender, EventArgs e)
        {
            grvChargesPopup.DataSource = null;
            // string _sad_itm_cd = gvATradeItem.SelectedDataKey[0].ToString();
            //string _sad_itm_cd = dgv.SelectedRows[0].Cells[0].Value.ToString();
            //-----------------------------------------------------------------------------------------

        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.AcJobNo:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceType:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceInvoice:
                    {
                        //paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator);
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvSalesInvoice:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "INV" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvDocs:
                    {
                        paramsText.Append(DateTime.MinValue.Date + seperator + CHNLSVC.Security.GetServerDateTime().Date + seperator + BaseCls.GlbUserComCode + seperator + "INV" + seperator + 1 + seperator + string.Empty + seperator + string.Empty);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }
        private void btnSearchJob_Click(object sender, EventArgs e)
        {
            try
            {
                //TextBox txtBox = new TextBox();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AcJobNo);
                DataTable _result = CHNLSVC.CommonSearch.SearchAcServicesJobs(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtJonNo;
                _CommonSearch.ShowDialog();
                txtJonNo.Select();
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkOth.Checked == true)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceInvoice);
                    //DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(_CommonSearch.SearchParams, null, null);//GetInvoiceSearchData
                    DataTable _result = CHNLSVC.CommonSearch.GetSerInvSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtSearchInviceNo;
                    _CommonSearch.ShowDialog();
                    txtSearchInviceNo.Select();
                }
                else
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoice);
                    //DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(_CommonSearch.SearchParams, null, null);//GetInvoiceSearchData
                    DataTable _result = CHNLSVC.CommonSearch.GetInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtSearchInviceNo;
                    _CommonSearch.ShowDialog();
                    txtSearchInviceNo.Select();
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

        private void grvPaid_ClaimDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {

                if (PaidClaimedList[e.RowIndex].PartyCode != "C")
                {
                    // ManagerPayment = ManagerPayment - PaidClaimedList[e.RowIndex].Amount; 
                    PaidClaimedList.RemoveAt(e.RowIndex);

                    grvPaid_ClaimDetails.AutoGenerateColumns = false;
                    grvPaid_ClaimDetails.DataSource = null;
                    grvPaid_ClaimDetails.DataSource = PaidClaimedList;

                }
            }
        }

        private void txtChargesToManager_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Decimal maxClaimRt = 100;
                maxClaimRt = CHNLSVC.Sales.Get_MaxAC_ClaimRate("ACJOB", "PC", BaseCls.GlbUserDefProf, Convert.ToDateTime(pickedDate.Value).Date);
                Decimal MaximumClaimVal = (ChargesToManager * maxClaimRt / 100);
                txtMaxClaimAmt.Text = string.Format("{0:n2}", MaximumClaimVal);
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

        private void txtSearchInviceNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    if (chkOth.Checked == true)
                    {
                        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                        _CommonSearch.ReturnIndex = 0;
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceInvoice);
                        //DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(_CommonSearch.SearchParams, null, null);//GetInvoiceSearchData
                        //DataTable _result = CHNLSVC.CommonSearch.GetInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                        DataTable _result = CHNLSVC.CommonSearch.GetSerInvSearchData(_CommonSearch.SearchParams, null, null);
                        _CommonSearch.dvResult.DataSource = _result;
                        _CommonSearch.BindUCtrlDDLData(_result);
                        _CommonSearch.obj_TragetTextBox = txtSearchInviceNo;
                        _CommonSearch.ShowDialog();
                        txtSearchInviceNo.Select();
                    }
                    else
                    {
                        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                        _CommonSearch.ReturnIndex = 0;
                        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvSalesInvoice);
                        //DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(_CommonSearch.SearchParams, null, null);//GetInvoiceSearchData
                        DataTable _result = CHNLSVC.CommonSearch.GetInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                        _CommonSearch.dvResult.DataSource = _result;
                        _CommonSearch.BindUCtrlDDLData(_result);
                        _CommonSearch.obj_TragetTextBox = txtSearchInviceNo;
                        _CommonSearch.ShowDialog();
                        txtSearchInviceNo.Select();
                    }

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

        private void txtJonNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AcJobNo);
                    DataTable _result = CHNLSVC.CommonSearch.SearchAcServicesJobs(_CommonSearch.SearchParams, null, null);
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtJonNo;
                    _CommonSearch.ShowDialog();
                    txtJonNo.Select();
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

        private void txtSearchInviceNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.button1_Click(sender, e);
        }

        private void txtJonNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.btnSearchJob_Click(sender, e);
        }

        private void pickedDate_ValueChanged(object sender, EventArgs e)
        {
            txtJobDate.Text = pickedDate.Value.ToShortDateString();
        }
    }
}
