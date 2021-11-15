using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using FF.WindowsERPClient.HP;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
//using IWshRuntimeLibrary;

namespace FF.WindowsERPClient.HP
{
    public partial class HPRemindersSMS : Base
    {
        static string GeneralText;
        static string ArrearsAccounts;
        static string MonthlyDue;
        static string HPCustomers;
        static int GvRowCount;
        static int GvAllCount;
        private DataTable select_ITEMS_List = new DataTable();
        const string InvoiceBackDateName = "HPREMINDERSMS";
        private List<HpAccount> accountsList;
        private List<Service_Reminder_Template> _templateLst;
        private DataTable _dtCont = new DataTable();

        // add by akila 2017/08/16 Allow only alpha numerics and specific symbols
        Regex AllowedCharacters = new Regex(@"[^a-z^A-Z^0-9^\s^\^_\,\.\?\%\`\!\@\&\#\'\""\[\]\{\}\=\+^\-^\/^\*^\(^\)]"); 

        public List<HpAccount> AccountsList
        {
            get { return accountsList; }
            set { accountsList = value; }
        }
        static Int32 _SeqNo;

        public HPRemindersSMS()
        {

            InitializeComponent();
            InitializeValuesNDefaultValueSet();
            LoadLanguage();
        }

        int reminderID = -1;
        private void InitializeValuesNDefaultValueSet()
        {
            BackDatePermission();
            gvCust.AutoGenerateColumns = false;
            gvFailCust.AutoGenerateColumns = false;
            grdSrvRCustomer.AutoGenerateColumns = false;


            DataTable _msgcol = CHNLSVC.Sales.GetMsgColumn();
            dgvMsgColums.DataSource = _msgcol;

            txtComp.Text = BaseCls.GlbUserComCode;


            List<MasterInvoiceType> _lst = CHNLSVC.Sales.GetAllInvoiceType();

            // @desc Author Damith 18-dec-2014
            //Load message type template
            _templateLst = new List<Service_Reminder_Template>();
            //_templateLst = CHNLSVC.CustService.GetReminderTemplate();
            txtSrvRMessage.Text = loadMsgHasTemp(getSelectedMsgType(out reminderID), out emailTemplate);
            txtSrvREmail.Text = emailTemplate;

            BindingSource _source = new BindingSource();
            _source.DataSource = _lst;
            grvTp.DataSource = _source;

            GvRowCount = 0;
            GvAllCount = 0;
            //LoadPcs();
            HPCustomers = "Dear  [cusName] , thank u for buying [product] on Hire purchase [loc] S/R on [date]  (A/c [accNo]).Tot value [total] & diriya [diriya]-[com]-0112565293";
            MonthlyDue = "Dear customer, your next due date of HP Acc. No [accNo] is on [date] .-[com]-";
            ArrearsAccounts = "Dear Customer, Plz make arrangements to settle the arrears amount of Rs. [amount]  in HP Acc. No [accNo] Immediately.-[com]-";
            GeneralText = txtMessage.Text + "[com]";
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            //LoadFail(dt1);
            //LoadGrid(dt);
            //RadioButtonListMessageType_SelectedIndexChanged(null, null);
        }

        private void LoadPcs()
        {
            try
            {
                string _masterLocation = "";
                if (optManager.Checked == false)
                {
                    _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                    if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPS"))
                    {
                        DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(BaseCls.GlbUserComCode, txtChanel.Text, txtSChanel.Text, txtArea.Text, txtRegion.Text, txtZone.Text, txtPC.Text);
                        foreach (DataRow drow in dt.Rows)
                        {
                            lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                        }
                    }
                    else
                    {
                        DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep(BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtChanel.Text, txtSChanel.Text, txtArea.Text, txtRegion.Text, txtZone.Text, txtPC.Text);
                        foreach (DataRow drow in dt.Rows)
                        {
                            lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                        }
                    }
                    if (lstPC.Items.Count > 0)
                        lstPC.Items[0].Checked = true;
                }
                else
                {
                    if (cmbType.Text == "Profit Center")
                    {
                        _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                        if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPS"))
                        {
                            DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(BaseCls.GlbUserComCode, txtChanel.Text, txtSChanel.Text, txtArea.Text, txtRegion.Text, txtZone.Text, txtPC.Text);
                            foreach (DataRow drow in dt.Rows)
                            {
                                lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                            }
                        }
                        else
                        {
                            DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep(BaseCls.GlbUserID, BaseCls.GlbUserComCode, txtChanel.Text, txtSChanel.Text, txtArea.Text, txtRegion.Text, txtZone.Text, txtPC.Text);
                            foreach (DataRow drow in dt.Rows)
                            {
                                lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                            }
                        }
                        if (lstPC.Items.Count > 0)
                            lstPC.Items[0].Checked = true;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(cmbType.Text))
                        {
                            MessageBox.Show("Please select the type", "SMS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        DataTable dt = CHNLSVC.General.sp_get_pc_prit_hierarchy(BaseCls.GlbUserComCode, cmbType.Text);
                        foreach (DataRow drow in dt.Rows)
                        {
                            lstPC.Items.Add(drow["mpi_val"].ToString());
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

        private void BackDatePermission()
        {
            bool _allowCurrentTrans = false;
            IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, InvoiceBackDateName, txtDueDt, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "A" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(txtComp.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + txtRegion.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + txtRegion.Text + seperator + txtZone.Text + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {

                        paramsText.Append(txtCate1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append("" + seperator + "" + seperator + BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "Loc" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item_Serials:
                    {
                        paramsText.Append(TextBoxItem.Text + seperator + "" + seperator + BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "Loc" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Circular:
                    {
                        paramsText.Append("" + seperator + "Circular" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Promotion:
                    {
                        paramsText.Append("" + seperator + "Promotion" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string _sale_tp = "";
                if (optCust.Checked == true && Convert.ToDateTime(txtDateFrom.Text) > Convert.ToDateTime(txtDateTo.Text))
                {
                    MessageBox.Show("From date has to be smaller than to date", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (optManager.Checked == false)
                {
                    if(!optMyAb.Checked)
                    if (lstPC.Items.Count == 0)
                    {
                        MessageBox.Show("Select Profit centers", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }


                string sendType = "";
                if (optCustomer.Checked && optCust.Checked == true)
                {
                    sendType = "Customer_HP";
                }
                else if (optGuarantor.Checked && optCust.Checked == true)
                {
                    sendType = "Guarantor_HP";
                }
                else if (optDue.Checked == true)
                    sendType = "Due";
                else if (optArr.Checked == true)
                    sendType = "Ars";
                else
                {
                    sendType = "Genaral";
                    if (optCustomer.Checked == true)
                    {
                        foreach (DataGridViewRow dgvr in grvTp.Rows)
                        {
                            DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                            if (Convert.ToBoolean(chk.Value) == true)
                                _sale_tp = _sale_tp == "" ? "^" + dgvr.Cells[1].Value.ToString() + "$" : _sale_tp + "|" + "^" + dgvr.Cells[1].Value.ToString() + "$";
                        }
                        if (string.IsNullOrEmpty(_sale_tp))
                        {
                            MessageBox.Show("Select the sales type", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }
                this.Cursor = Cursors.WaitCursor;
                List<InsuItem> ITEMS_list = new List<InsuItem>();
                DataTable dt = new DataTable();
                if (sendType == "Genaral")
                {

                    if (chkInv.Checked == true)
                    {
                        if (Convert.ToDateTime(dtpInvFrom.Text) > Convert.ToDateTime(dtpInvTo.Text))
                        {
                            MessageBox.Show("From date has to be higher than to date", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            dtpInvTo.Focus();
                            return;
                        }

                    }

                    if (chkReg.Checked == true)
                    {
                        if (Convert.ToDateTime(dtpRegFrom.Text) > Convert.ToDateTime(dtpRegTo.Text))
                        {
                            MessageBox.Show("From date has to be higher than to date", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            dtpRegTo.Focus();
                            return;
                        }
                    }

                    if (dtpcrfrom.Checked == true)
                    {
                        if (Convert.ToDateTime(dtpcrfrom.Text) > Convert.ToDateTime(dtpcrTo.Text))
                        {
                            MessageBox.Show("From date has to be higher than to date", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            dtpcrTo.Focus();
                            return;
                        }

                    }

                    if (chkRec.Checked == true)
                    {
                        if (Convert.ToDateTime(dtpcrfrom.Text) > Convert.ToDateTime(dtpcrTo.Text))
                        {
                            MessageBox.Show("From date has to be higher than to date", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            dtpcrTo.Focus();
                            return;
                        }
                    }



                    ITEMS_list = GetSelectedItemsList();
                }
                //kapila 3/8/2015
                if (sendType == "Genaral")
                {
                    if (optManager.Checked == true)    //kapila 1/8/2015
                    {
                        string _type = cmbType.Text;
                        if (lstPC.Items.Count > 0)
                        {
                            foreach (ListViewItem Item in lstPC.Items)
                            {
                                string _code = Item.Text;

                                if (Item.Checked == true)
                                {
                                    _dtCont.Merge(CHNLSVC.Sales.GetSMSManagers(BaseCls.GlbUserComCode, _type, _code));
                                }
                            }
                        }

                        this.Cursor = Cursors.Default;
                        _dtCont = _dtCont.DefaultView.ToTable(true);
                        LoadGrid(_dtCont);
                        return;
                    }
                    if (optMyAb.Checked)
                    {
                        if (cmbPrefLang.SelectedIndex == -1)
                        {
                            MessageBox.Show("From date has to be higher than to date", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            cmbPrefLang.Focus();
                            return;
                        }
                        DataTable _dtSMS = new DataTable();

                        if (chkTV.Checked) _dtSMS.Merge (CHNLSVC.General.getMyAbansSMSContacts(1, cmbPrefLang.SelectedValue.ToString(),txtPerTown.Text));
                        if (chkFR.Checked) _dtSMS.Merge(CHNLSVC.General.getMyAbansSMSContacts(2, cmbPrefLang.SelectedValue.ToString(), txtPerTown.Text));
                        if (chkWM.Checked) _dtSMS.Merge(CHNLSVC.General.getMyAbansSMSContacts(3, cmbPrefLang.SelectedValue.ToString(), txtPerTown.Text));
                        if (chkLP.Checked) _dtSMS.Merge(CHNLSVC.General.getMyAbansSMSContacts(4, cmbPrefLang.SelectedValue.ToString(), txtPerTown.Text));
                        if (chkSP.Checked) _dtSMS.Merge(CHNLSVC.General.getMyAbansSMSContacts(5, cmbPrefLang.SelectedValue.ToString(), txtPerTown.Text));
                        if (chkMO.Checked) _dtSMS.Merge(CHNLSVC.General.getMyAbansSMSContacts(6, cmbPrefLang.SelectedValue.ToString(), txtPerTown.Text));
                        if (chkDTC.Checked) _dtSMS.Merge(CHNLSVC.General.getMyAbansSMSContacts(7, cmbPrefLang.SelectedValue.ToString(), txtPerTown.Text));
                        if (chkHIFI.Checked) _dtSMS.Merge(CHNLSVC.General.getMyAbansSMSContacts(8, cmbPrefLang.SelectedValue.ToString(), txtPerTown.Text));
                        if (chkAC.Checked) _dtSMS.Merge(CHNLSVC.General.getMyAbansSMSContacts(9, cmbPrefLang.SelectedValue.ToString(), txtPerTown.Text));

                        this.Cursor = Cursors.Default;
                        _dtSMS = _dtSMS.DefaultView.ToTable(true);
                        LoadGrid(_dtSMS);
                        return;


                    }
                }


                foreach (ListViewItem Item in lstPC.Items)
                {
                    string pc = Item.Text;

                    if (Item.Checked == true)
                    {
                        if (sendType == "Due")
                            dt.Merge(CHNLSVC.Sales.GetHPCustomer(BaseCls.GlbUserComCode, pc, DateTime.Now.Date, DateTime.MaxValue, sendType, null, Convert.ToDateTime(txtDueDt.Text), DateTime.Now, DateTime.Now, 0, ITEMS_list, chkInv.Checked, dtpInvFrom.Value, dtpInvTo.Value, chkReg.Checked, dtpRegFrom.Value, dtpRegTo.Value, chkPlate.Checked, dtpcrfrom.Value, dtpcrTo.Value, chkRec.Checked, dtpnplateFrom.Value, dtpnplateTo.Value));

                        else if (sendType == "Genaral")
                            dt.Merge(CHNLSVC.Sales.GetHPCustomer(BaseCls.GlbUserComCode, pc, DateTime.MinValue, DateTime.MaxValue, sendType, _sale_tp, DateTime.Now, DateTime.Now, DateTime.Now, Convert.ToInt32(optAll.Checked), ITEMS_list, chkInv.Checked, dtpInvFrom.Value, dtpInvTo.Value, chkReg.Checked, dtpRegFrom.Value, dtpRegTo.Value, chkPlate.Checked, dtpcrfrom.Value, dtpcrTo.Value, chkRec.Checked, dtpnplateFrom.Value, dtpnplateTo.Value));

                        else if (sendType == "Ars")
                        {
                            HpAccountSummary summary = new HpAccountSummary();
                            DateTime ars_date;
                            DateTime sup_date;
                            HpAccountSummary.get_ArearsDate_SupDate(pc, Convert.ToDateTime(txtAsAtDate.Text), out ars_date, out sup_date);
                            //kapila  30/8/2013
                            DateTime _arrearsDate;
                            DateTime _date = Convert.ToDateTime(txtAsAtDate.Text).Date.AddDays(1);
                            DateTime lastDayLastMonth = new DateTime(_date.Year, _date.Month, 1);
                            lastDayLastMonth = lastDayLastMonth.AddDays(-1);
                            if (Convert.ToDateTime(txtAsAtDate.Text).Date == lastDayLastMonth)
                            {
                                _arrearsDate = Convert.ToDateTime(txtAsAtDate.Text);
                            }
                            else
                            {
                                _arrearsDate = ars_date;
                            }
                            txtArsDt.Text = (_arrearsDate).ToString();
                            dt.Merge(CHNLSVC.Sales.GetHPCustomer(BaseCls.GlbUserComCode, pc, DateTime.Now, DateTime.Now, sendType, null, Convert.ToDateTime(txtAsAtDate.Text), _arrearsDate, sup_date, 0, ITEMS_list, chkInv.Checked, dtpInvFrom.Value, dtpInvTo.Value, chkReg.Checked, dtpRegFrom.Value, dtpRegTo.Value, chkPlate.Checked, dtpcrfrom.Value, dtpcrTo.Value, chkRec.Checked, dtpnplateFrom.Value, dtpnplateTo.Value));
                        }
                        else
                            dt.Merge(CHNLSVC.Sales.GetHPCustomer(BaseCls.GlbUserComCode, pc, Convert.ToDateTime(txtDateFrom.Text), Convert.ToDateTime(txtDateTo.Text), sendType, null, DateTime.Now, DateTime.Now, DateTime.Now, 0, ITEMS_list, chkInv.Checked, dtpInvFrom.Value, dtpInvTo.Value, chkReg.Checked, dtpRegFrom.Value, dtpRegTo.Value, chkPlate.Checked, dtpcrfrom.Value, dtpcrTo.Value, chkRec.Checked, dtpnplateFrom.Value, dtpnplateTo.Value));
                    }
                }

                this.Cursor = Cursors.Default;
                //remove rows
                //arrears and due


                //due
                if (optDue.Checked == true)
                {
                    //DateTime ars_dt = DateTime.MinValue.Date;
                    //DateTime sup_dt = DateTime.MinValue.Date;
                    //processForGettingArrears(GlbUserDefProf, out ars_dt, out sup_dt, Convert.ToDateTime(TextBoxDueDt.Text));
                    //if (ars_dt == DateTime.MinValue.Date)
                    //{
                    //    ars_dt = Convert.ToDateTime(TextBoxDueDt.Text);
                    //}
                    //if (sup_dt == DateTime.MinValue.Date)
                    //{
                    //    sup_dt = Convert.ToDateTime(TextBoxDueDt.Text);
                    //}
                    //foreach (DataRow dr in dt.Rows)
                    //{

                    //    //Decimal arrears = CHNLSVC.Sales.Get_hp_TotalDue( dr["HPA_ACC_NO"].ToString(), ars_dt.Date);
                    //    decimal _monRen = CHNLSVC.Sales.Get_MonthlyRental(Convert.ToDateTime(TextBoxDueDt.Text), dr["HPA_ACC_NO"].ToString());
                    //    if ( _monRen<=0)
                    //    {
                    //        dr.Delete();
                    //    }
                    //}
                    //dt.AcceptChanges();
                }

                LoadGrid(dt);
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

        private void LoadLanguage()
        {
            DataTable _tbl = CHNLSVC.General.get_Language();
            cmbPrefLang.DataSource = _tbl;
            cmbPrefLang.DisplayMember = "mla_desc";
            cmbPrefLang.ValueMember = "mla_cd";
        }

        private List<InsuItem> GetSelectedItemsList()
        {
            List<InsuItem> list = new List<InsuItem>();
            foreach (DataGridViewRow dgvr in GridAll_Items.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    InsuItem _one = new InsuItem();
                    _one.Circuler = dgvr.Cells[6].Value.ToString();
                    _one.Item = dgvr.Cells[1].Value.ToString();
                    _one.Promotion = dgvr.Cells[5].Value.ToString();
                    _one.Serial = dgvr.Cells[4].Value.ToString();
                    _one.Value = Convert.ToDecimal(dgvr.Cells[3].Value);
                    _one.Cat1 = string.Empty;
                    _one.Cat2 = string.Empty;
                    _one.Brand = string.Empty;

                    list.Add(_one);
                }

            }
            return list;
        }
        private void LoadGrid(DataTable dt)
        {
            try
            {
                //check sms table
                //remove main grid add fail grid
                DataTable dt1 = dt.Clone();
                List<DataRow> _dupList = new List<DataRow>();
                if (optGen.Checked != true)
                {
                    if (!optMyAb.Checked)
                    {

                        foreach (DataRow dr in dt.Rows)
                        {
                            HPReminderSMS _sms = CHNLSVC.Sales.GetSMSReminder(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, dr["HPA_ACC_NO"].ToString(), DateTime.Now.Date);
                            if (_sms != null)
                            {

                                _dupList.Add(dr);
                            }
                        }
                    }

                }
                foreach (DataRow dr in _dupList)
                {
                    dt1.ImportRow(dr);
                }
                foreach (DataRow dr in _dupList)
                {
                    dt.Rows.Remove(dr);
                }

                //add by akila 2017/08/16 - remove duplicate records
                if (dt.Rows.Count > 0)
                {
                    var _distinctDate = dt.AsEnumerable().GroupBy(x => x.Field<string>("MBE_MOB")).Select(x => x.First());
                    DataTable t = new DataTable();
                    dt = _distinctDate.CopyToDataTable();
                }
                
               
                GvAllCount = dt.Rows.Count;
                GvRowCount = 0;
                gvCust.DataSource = dt;
                sendCount.Text = GvRowCount.ToString();
                lblAllCount.Text = GvAllCount.ToString();

                gvCust.Columns[8].Visible = false;

                gvCust.Columns[1].HeaderText = "Acc No";
                gvCust.Columns[2].HeaderText = "Customer Name";
                gvCust.Columns[3].HeaderText = "Product";

                if (optGen.Checked == true)
                {
                    if (optMyAb.Checked == true)
                    {
                        gvCust.Columns[1].Visible = false;
                        gvCust.Columns[3].Visible = false;
                        gvCust.Columns[4].Visible = false;
                        gvCust.Columns[6].Visible = false;
                        gvCust.Columns[7].Visible = false;
                        gvCust.Columns[2].Width = 350;
                    }
                    if (optCustomer.Checked == true)
                    {
                        gvCust.Columns[1].Visible = false;
                        gvCust.Columns[3].Visible = false;
                        gvCust.Columns[4].Visible = false;
                        gvCust.Columns[6].Visible = false;
                        gvCust.Columns[7].Visible = false;
                        gvCust.Columns[2].Width = 350;
                    }
                    if (optManager.Checked == true)
                    {
                        gvCust.Columns[1].Visible = true;
                        gvCust.Columns[2].Visible = true;
                        gvCust.Columns[3].Visible = true;
                        gvCust.Columns[1].HeaderText = "EPF No";
                        gvCust.Columns[2].HeaderText = "First Name";
                        gvCust.Columns[3].HeaderText = "Last Name";
                        gvCust.Columns[4].Visible = false;
                        gvCust.Columns[6].Visible = false;
                        gvCust.Columns[7].Visible = false;
                        gvCust.Columns[8].Visible = false;
                        gvCust.Columns[9].Visible = false;
                        gvCust.Columns[10].Visible = false;
                        gvCust.Columns[11].Visible = false;
                        gvCust.Columns[12].Visible = false;
                        gvCust.Columns[13].Visible = false;
                        gvCust.Columns[14].Visible = false;
                        gvCust.Columns[15].Visible = false;
                        gvCust.Columns[16].Visible = false;
                        gvCust.Columns[17].Visible = false;
                        gvCust.Columns[18].Visible = false;
                        //gvCust.Columns[].Width = 350;
                    }
                }
                else
                {

                    if (optArr.Checked == true)
                    {
                        
                        

                        gvCust.Columns[4].Visible = true;
                        gvCust.Columns[5].Visible = true;
                        gvCust.Columns[6].Visible = true;
                        gvCust.Columns[7].Visible = true;

                    }
                    if (optDue.Checked == true)
                    {
                        gvCust.Columns[7].Visible = true;
                        gvCust.Columns[5].Visible = true;
                        gvCust.Columns[6].Visible = true;
                    }
                    if (optDue.Checked != true)
                    {
                        gvCust.Columns[5].Visible = true;

                    }
                    if (optArr.Checked != true)
                    {
                        gvCust.Columns[4].Visible = false;

                    }
                    if (optDue.Checked != true)
                    {
                        gvCust.Columns[7].Visible = false;

                    }
                }
                gvFailCust.DataSource = dt1;
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

        private void optGen_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtMessage.Enabled = false;

                if (optGen.Checked == true)
                {
                    txtMessage.Text = GeneralText;
                    txtMessage.Enabled = true;
                    pnlAsAt.Visible = false;
                    pnlArs.Visible = false;
                    pnlRange.Visible = false;
                    pnlDue.Visible = false;
                    optGuarantor.Visible = false;
                    optManager.Visible = true;
                    cmbType.Visible = true;
                    if (optManager.Checked == true)
                    {
                        pnlSaleType.Visible = false;
                        pnlBal.Visible = false;
                    }
                    else
                    {
                        pnlSaleType.Visible = true;
                        pnlBal.Visible = true;
                    }
                    sendCount.Text = "0";
                    lblAllCount.Text = "0";
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

        private void optArr_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (optArr.Checked == true)
                {
                    txtMessage.Text = ArrearsAccounts;
                    pnlArs.Visible = true;
                    pnlAsAt.Visible = true;
                    pnlRange.Visible = false;
                    optGuarantor.Visible = false;
                    optManager.Visible = false;
                    cmbType.Visible = false;
                    pnlSaleType.Visible = false;
                    pnlDue.Visible = false;
                    pnlBal.Visible = false;
                    pnlMyAb.Visible = false;
                    //txtAsAtDate.Text = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1).ToString("dd/MMM/yyyy");
                    txtAsAtDate.Text = (DateTime.Now.Date).ToString();
                    txtArsDt.Text = (DateTime.Now.Date).ToString();
                    sendCount.Text = "0";
                    lblAllCount.Text = "0";
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

        private void optDue_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (optDue.Checked == true)
                {
                    txtMessage.Text = MonthlyDue;
                    pnlAsAt.Visible = false;
                    pnlArs.Visible = false;
                    pnlRange.Visible = false;
                    pnlDue.Visible = true;
                    optGuarantor.Visible = false;
                    optManager.Visible = false;
                    cmbType.Visible = false;
                    pnlSaleType.Visible = false;
                    pnlBal.Visible = false;
                    pnlMyAb.Visible = false;
                    DateTime today = DateTime.Today;
                    txtDueDt.Text = new DateTime(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month)).ToString("dd/MMM/yyyy");
                    sendCount.Text = "0";
                    lblAllCount.Text = "0";
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

        private void optCust_CheckedChanged(object sender, EventArgs e)
        {
            if (optCust.Checked == true)
            {
                txtMessage.Text = HPCustomers;
                pnlAsAt.Visible = false;
                pnlArs.Visible = false;
                pnlRange.Visible = true;
                pnlDue.Visible = false;
                optGuarantor.Visible = true;
                pnlSaleType.Visible = false;
                optManager.Visible = false;
                cmbType.Visible = false;
                pnlBal.Visible = false;
                pnlMyAb.Visible = false;
                sendCount.Text = "0";
                lblAllCount.Text = "0";
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstPC.Items)
            {
                Item.Checked = true;
            }
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstPC.Items)
            {
                Item.Checked = false;
            }
        }

        private bool ValidateMobileNo(string num)
        {
            int intNum = 0;
            //check only contain degits
            //if (!int.TryParse(num, out intNum))       //comented by kapila on 10/9/2015 coz +94 messages will not be sent
            //    return false;
            ////check for length
            //else
            //{
            if (num.Length < 10)
            {
                return false;
            }
            //check for first three chars
            else
            {
                string firstChar = num.Substring(0, 3);
                string firstChar_94 = num.Substring(0, 5);
                if (firstChar != "071" && firstChar != "077" && firstChar != "078" && firstChar != "072" && firstChar != "075" && firstChar != "076" && firstChar != "074" &&
                    firstChar_94 != "+9471" && firstChar_94 != "+9477" && firstChar_94 != "+9478" && firstChar_94 != "+9472" && firstChar_94 != "+9475" && firstChar_94 != "+9476" && firstChar_94 != "+9474")
                {
                    return false;
                }
            }
            //}
            return true;
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            Int32 i = 0;
            if (chkAll.Checked)
            {
                foreach (DataGridViewRow row in gvCust.Rows)
                {
                    gvCust.Rows[i].Cells["pen_select"].Value = true;
                    GvRowCount++;
                    i++;
                }
            }

            else
            {
                foreach (DataGridViewRow row in gvCust.Rows)
                {
                    gvCust.Rows[i].Cells["pen_select"].Value = false;
                    GvRowCount--;
                    i++;
                }
            }
            sendCount.Text = GvRowCount.ToString();
            lblAllCount.Text = GvAllCount.ToString();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime ars_dt = DateTime.MinValue.Date;
                DateTime sup_dt = DateTime.MinValue.Date;

                if (optGen.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtMessage.Text))
                    {
                        MessageBox.Show("Please enter a message", "SMS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                //requested by dilanda on 9/4/2014
                if (optGen.Checked == true)
                {
                    if(optMyAb.Checked)
                    {
                        if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10138))
                        {
                            MessageBox.Show("Sorry, You have no permission for sending MyAbans SMS!\n( Advice: Required permission code :10138)", "SMS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    if (optCustomer.Checked == true)
                    {
                        if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10081))
                        {
                            MessageBox.Show("Sorry, You have no permission for sending general SMS!\n( Advice: Required permission code :10081)", "SMS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    if (optManager.Checked == true)
                    {
                        if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10107))
                        {
                            MessageBox.Show("Sorry, You have no permission for sending general SMS to managers!\n( Advice: Required permission code :10107)", "SMS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }

                // add by akila 2017/08/16 Allow only alpha numerics and specific symbols
                if (!string.IsNullOrEmpty(txtMessage.Text.Trim()))
                {
                    MatchCollection matches = AllowedCharacters.Matches(txtMessage.Text.Trim());
                    if (matches != null && matches.Count > 0)
                    {
                        MessageBox.Show("The message contains invalid characters", "General Message Sending Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (MessageBox.Show("Are you sure ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                btnSend.Enabled = false;
                string message = "";
                if (optGen.Checked == true)
                    message = txtMessage.Text;
                else if (optArr.Checked == true)
                    message = ArrearsAccounts;
                else if (optDue.Checked == true)
                    message = MonthlyDue;
                else if (optCust.Checked == true)
                    message = HPCustomers;

                int result = 0;
                //save process
                DataGridViewCheckBoxCell _chk = null;
                for (int i = 0; i < gvCust.RowCount; i++)
                {
                    _chk = (DataGridViewCheckBoxCell)gvCust.Rows[i].Cells["pen_select"];

                    if (Convert.ToBoolean(_chk.Value) == true)
                    {
                        string mobilNo = Convert.ToString(gvCust.Rows[i].Cells["MBE_MOB"].Value);
                        string name = Convert.ToString(gvCust.Rows[i].Cells["mbe_name"].Value);
                        string accNo = Convert.ToString(gvCust.Rows[i].Cells["HTC_ACC_NO"].Value);
                        string product = Convert.ToString(gvCust.Rows[i].Cells["item_cd"].Value);

                        string brand = Convert.ToString(gvCust.Rows[i].Cells["colBrand"].Value);
                        string model = Convert.ToString(gvCust.Rows[i].Cells["colmodel"].Value);
                        string vehicle = Convert.ToString(gvCust.Rows[i].Cells["colVehicle"].Value);
                        string pc_name = Convert.ToString(gvCust.Rows[i].Cells["col_pc"].Value);
                        string cusadd1 = Convert.ToString(gvCust.Rows[i].Cells["colCussAdd"].Value);
                        string cusadd2 = Convert.ToString(gvCust.Rows[i].Cells["colcusadd2"].Value);
                        string pc_add1 = Convert.ToString(gvCust.Rows[i].Cells["colpcAdd"].Value);
                        string pc_add2 = Convert.ToString(gvCust.Rows[i].Cells["colpcadd2"].Value);
                        DateTime invDate = Convert.ToDateTime(gvCust.Rows[i].Cells["colInvDate"].Value);
                        string pc_phone = Convert.ToString(gvCust.Rows[i].Cells["col_pcphone"].Value);

                        bool isValid = ValidateMobileNo(mobilNo);
                        if (optArr.Checked == true)
                        {
                            message = "Dear Customer, Plz make arrangements to settle the arrears amount of Rs. [amount]  in HP Acc. No [accNo] Immediately.-[com]-";

                            string arrears = Convert.ToString(gvCust.Rows[i].Cells["Arrears"].Value);
                            message = GenarateMessage(message, name, product, BaseCls.GlbUserDefLoca, DateTime.Now, accNo, BaseCls.GlbUserComCode, "", Convert.ToDecimal(arrears), brand, model, vehicle, pc_name, cusadd1, cusadd2, pc_add1, pc_add2, invDate, pc_phone);
                        }
                        else if (optCust.Checked == true)
                        {
                            message = "Dear  [cusName] , thank u for buying [product] on Hire purchase [loc] S/R on [date]  (A/c [accNo]).Tot value [total] & diriya [diriya]-[com]-0112565293";

                            string location = BaseCls.GlbUserDefProf;
                            string acc_seq = accNo;
                            List<HpAccount> accList = new List<HpAccount>();
                            accList = CHNLSVC.Sales.GetHP_Accounts(BaseCls.GlbUserComCode, location, acc_seq, "A");
                            message = GenarateMessage(message, name, product, BaseCls.GlbUserDefLoca, DateTime.Now, accNo, BaseCls.GlbUserComCode, (accList[0].Hpa_init_ins + accList[0].Hpa_init_stm).ToString(), 0, brand, model, vehicle, pc_name, cusadd1, cusadd2, pc_add1, pc_add2, invDate, pc_phone);
                        }
                        else if (optDue.Checked == true)
                        {
                            message = "Dear customer, your next due date of HP Acc. No [accNo] is on [date] .-[com]-";
                            DateTime _due = Convert.ToDateTime(gvCust.Rows[i].Cells["Due"].Value); ;
                            //if (DateTime.TryParse(((Label)gr.FindControl("LabelDue")).Text, out _due))
                            //{
                            //    _due = Convert.ToDateTime(((Label)gr.FindControl("LabelDue")).Text);
                            message = GenarateMessage(message, name, product, BaseCls.GlbUserDefLoca, _due, accNo, BaseCls.GlbUserComCode, "", 0, brand, model, vehicle, pc_name, cusadd1, cusadd2, pc_add1, pc_add2, invDate, pc_phone);
                            //}
                            //else
                            //    isValid = false;
                        }
                        else
                            message = GenarateMessage(message, name, product, BaseCls.GlbUserDefLoca, DateTime.Now, accNo, BaseCls.GlbUserComCode, "", 0, brand, model, vehicle, pc_name, cusadd1, cusadd2, pc_add1, pc_add2, invDate, pc_phone);
                        if (isValid)
                        {
                            // updated by akila - 2018/02/14
                            bool _isRestricted = false;
                            if (optGen.Checked)
                            {
                                string _errorMessage = string.Empty;
                                _isRestricted = IsCustomerRestrictedToSendSms(mobilNo, ref _errorMessage);
                            }

                            if (!_isRestricted)
                            {
                                //send message
                                OutSMS _out = new OutSMS();

                                if (message != "")
                                    _out.Msg = message;
                                else
                                    _out.Msg = " ";

                                if (optGen.Checked == true)
                                {
                                    _out.Msgtype = "GEN";
                                    if (optManager.Checked == true)
                                        _out.Receiver = cmbType.Text;
                                    else
                                        _out.Receiver = Convert.ToString(gvCust.Rows[i].Cells["MBE_CD"].Value);
                                }
                                else
                                {
                                    _out.Msgtype = "RMD";
                                }

                                //kapila 10/9/2015
                                if (mobilNo.Substring(0, 3) == "+94")
                                {
                                    _out.Receiverphno = mobilNo;
                                    _out.Senderphno = mobilNo;
                                }
                                else
                                {
                                    if (mobilNo.Length == 10)
                                    {
                                        _out.Receiverphno = "+94" + mobilNo.Substring(1, 9);
                                        _out.Senderphno = "+94" + mobilNo.Substring(1, 9);
                                    }
                                    if (mobilNo.Length == 9)
                                    {
                                        _out.Receiverphno = "+94" + mobilNo;
                                        _out.Senderphno = "+94" + mobilNo;
                                    }
                                }


                                _out.Sender = BaseCls.GlbUserID;
                                _out.Createtime = DateTime.Now;
                                _out.Refdocno = BaseCls.GlbUserDefProf;
                                _out.Msgstatus = 0;
                                _out.Receivedtime = DateTime.Now;

                                //TODO: save SMS

                                HPReminderSMS _rmd = new HPReminderSMS();
                                _rmd.Hsrm_acc = accNo;
                                _rmd.Hsrm_com = BaseCls.GlbUserComCode;
                                _rmd.Hsrm_contact = mobilNo;
                                _rmd.Hsrm_cre_by = BaseCls.GlbUserID;
                                _rmd.Hsrm_cre_dt = DateTime.Now;
                                _rmd.Hsrm_pc = BaseCls.GlbUserDefProf;
                                _rmd.Hsrm_rmd_dt = DateTime.Now.Date;
                                _rmd.Hsrm_tp = "";
                                _rmd.Hsrm_val = 0;
                                result = CHNLSVC.Sales.SaveReminderSMS(_rmd, _out);
                            }
                        }

                    }
                }
                btnSend.Enabled = true;
                _dtCont = new DataTable();

                // if (result > 0)
                MessageBox.Show("Message sent successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private string GenarateMessage(string text, string _cusName, string _product, string _loc, DateTime _date, string _accNo, string _com, string _diriya, decimal _arrears, string brand, string model, string vehicle, string pc_name, string cusadd1, string cusadd2, string pc_add1, string pc_add2, DateTime invDate, string pc_phone)
        {
            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(_com);

            if (text.Contains("[CustomerName]"))
                text = text.Replace("[CustomerName]", _cusName);
            if (text.Contains("[product]"))
                text = text.Replace("[product]", _product);
            if (text.Contains("[location]"))
                text = text.Replace("[location]", _loc);
            if (text.Contains("[date]"))
                text = text.Replace("[date]", _date.ToShortDateString());
            if (text.Contains("[accNo]"))
                text = text.Replace("[accNo]", _accNo);
            if (text.Contains("[com]"))
                //text = text.Replace("[com]", _com);
                text = text.Replace("[com]", _masterComp.Mc_desc);  //kapila 27/11/2015
            if (text.Contains("[diriya]"))
                text = text.Replace("[diriya]", _diriya);
            if (text.Contains("[amount]"))
                text = text.Replace("[amount]", _arrears.ToString());
            if (text.Contains("[Brand]"))// Added by Nadeeka 11-04-2015
                text = text.Replace("[Brand]", brand);
            if (text.Contains("[Model]"))
                text = text.Replace("[Model]", model);
            if (text.Contains("[Vehicle]"))
                text = text.Replace("[Vehicle]", vehicle);
            if (text.Contains("[PC Name]"))
                text = text.Replace("[PC Name]", pc_name);
            if (text.Contains("[Customer Add]"))
                text = text.Replace("[Customer Add]", cusadd1 + cusadd2);
            if (text.Contains("[PC Address]"))
                text = text.Replace("[PC Address]", pc_add1 + pc_add2);
            if (text.Contains("[Invoice Date]"))
                text = text.Replace("[Invoice Date]", Convert.ToString(invDate));
            if (text.Contains("[PC Phone #]"))
                text = text.Replace("[PC Phone #]", pc_phone);
            return text;

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            HPRemindersSMS formnew = new HPRemindersSMS();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }


        private void gvCust_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            sendCount.Text = "0";
            DataGridViewCheckBoxCell _chk = null;
            for (int i = 0; i < gvCust.RowCount; i++)
            {
                _chk = (DataGridViewCheckBoxCell)gvCust.Rows[i].Cells["pen_select"];

                if (Convert.ToBoolean(_chk.Value) == true)
                {
                    sendCount.Text = (Convert.ToInt32(sendCount.Text) + 1).ToString();
                }
                else
                {
                    //sendCount.Text = (Convert.ToInt32(sendCount.Text) - 1).ToString();
                }
            }

            //Boolean _chk = false;
            ////sendCount.Text="0";
            ////Int32 _totCount = gvCust.RowCount;

            ////for (int i = 0; i < gvCust.RowCount; i++)
            ////{
            //    _chk = Convert.ToBoolean(gvCust.Rows[e.RowIndex].Cells["pen_select"].Value);

            //    if (_chk == true)
            //    {
            //        //_totCount = _totCount - 1;
            //        sendCount.Text = (Convert.ToInt32(sendCount.Text) + 1).ToString();
            //    }
            //    else
            //    {
            //        //_totCount = _totCount - 1;
            //        sendCount.Text = (Convert.ToInt32(sendCount.Text) - 1).ToString();
            //    }
            ////}
            ////sendCount.Text =_totCount.ToString();

        }

        private void chkAll_Tp_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAll_Tp.Checked == true)
                {
                    foreach (DataGridViewRow row in grvTp.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = true;
                    }
                    grvTp.EndEdit();

                }
                else
                {
                    foreach (DataGridViewRow row in grvTp.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = false;
                    }
                    grvTp.EndEdit();

                }
            }
            catch (Exception ex)
            {

            }
        }

        //--------------------------------------------
        //@start
        // Service Reminder - SMS
        // last Edit 12-12-14 by damith
        //----------------------------------------------

        //@click search button
        bool isCheckGenrlTxt = false;
        private void btnSrvRSearch_Click(object sender, EventArgs e)
        {
            grdSrvRCustomer.Rows.Clear();
            grdSrvRCustomer.Refresh();
            ServiceRreminder _serReminder = new ServiceRreminder();
            _serReminder.comCode = BaseCls.GlbUserComCode;
            _serReminder.locCode = BaseCls.GlbUserDefLoca;
            bool isCheckEstimate = radSrvREstimate.Checked;
            bool isCheckJComplet = radSrvRJComplte.Checked;
            bool isCheckGenrlTxt = radSrvRGnrlTxt.Checked;
            if (!isCheckEstimate && !isCheckJComplet && !isCheckGenrlTxt)
            {
                MessageBox.Show("Please select message type.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                _serReminder.jobNO = (!string.IsNullOrEmpty(txtSrvRJob.Text)) ? txtSrvRJob.Text : null;
                _serReminder.frmDate = Convert.ToDateTime(dteSrvRFrom.Text).Date;
                _serReminder.toDate = Convert.ToDateTime(dteSrvRTo.Text).Date;

                //@type Estimate
                if (isCheckEstimate)
                {
                    _serReminder.msgType = 1;
                    try
                    {
                        List<Service_Reminder> jobReminderLst = CHNLSVC.CustService.GetServJobReminder(_serReminder);
                        int totAccount = jobReminderLst.Count;
                        if (totAccount != 0)
                        {
                            lblTotAccount.Text = totAccount.ToString();
                            BindingSource bindSurce = new BindingSource();
                            bindSurce.DataSource = jobReminderLst;
                            grdSrvRCustomer.DataSource = bindSurce;
                        }
                        else
                        {
                            lblTotAccount.Text = "0";
                            MessageBox.Show("Please search again.data not found.", "Search Job Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    catch
                    {
                        return;
                    }
                }
                else if (isCheckJComplet)
                {
                    _serReminder.msgType = 2;
                    try
                    {
                        List<Service_Reminder> jobReminderLst = CHNLSVC.CustService.GetServJobReminder(_serReminder);
                        int totAccount = jobReminderLst.Count;
                        if (totAccount != 0)
                        {
                            lblTotAccount.Text = totAccount.ToString();
                            BindingSource bindSurce = new BindingSource();
                            bindSurce.DataSource = jobReminderLst;
                            grdSrvRCustomer.DataSource = bindSurce;
                        }
                        else
                        {
                            lblTotAccount.Text = "0";
                            MessageBox.Show("Please search again.data not found.", "Search Job Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    catch
                    {
                        return;
                    }

                }
                else if (isCheckGenrlTxt)
                {//General text
                    _serReminder.msgType = 3;
                    try
                    {
                        List<Service_Reminder> jobReminderLst = CHNLSVC.CustService.GetServJobReminder(_serReminder);
                        int totAccount = jobReminderLst.Count;
                        if (totAccount != 0)
                        {
                            lblTotAccount.Text = totAccount.ToString();
                            BindingSource bindSurce = new BindingSource();
                            bindSurce.DataSource = jobReminderLst;
                            grdSrvRCustomer.DataSource = bindSurce;
                        }
                        else
                        {
                            lblTotAccount.Text = "0";
                            MessageBox.Show("Please search again.data not found.", "Search Job Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    catch
                    {
                        return;
                    }

                }
                else
                {
                    MessageBox.Show("Please search again.data not found.", "Search Job Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }//endIF

        }

        //@Change General text state
        private void radSrvRGnrlTxt_CheckedChanged(object sender, EventArgs e)
        {
            isCheckGenrlTxt = (radSrvRGnrlTxt.Checked) ? true : false;
            switch (isCheckGenrlTxt)
            {
                case true:
                    txtSrvRMessage.Enabled = true;
                    txtSrvREmail.Enabled = true;
                    txtSrvRMessage.Focus();
                    break;
                default:
                    txtSrvRMessage.Enabled = false;
                    txtSrvREmail.Enabled = false;
                    break;
            }
        }

        //@desc message send
        private void btnSrvRSend_Click(object sender, EventArgs e)
        {
            int foundRcrd = 0;
            string jobNO = null, mobilNo = null, name = null, jobDate = null, email = null;
            List<SmsOutMember> _smsOutLst = new List<SmsOutMember>();
            List<Sms_Ref_Log> _smsRefLog = new List<Sms_Ref_Log>();
            if (grdSrvRCustomer.RowCount != 0)
            {
                for (int i = 0; i < grdSrvRCustomer.Rows.Count; i++)
                {
                    var testChk = grdSrvRCustomer.Rows[i].Cells["chkRow"];
                    jobNO = (grdSrvRCustomer.Rows[i].Cells["colJobNo"].Value != null) ? grdSrvRCustomer.Rows[i].Cells["colJobNo"].Value.ToString() : null;
                    mobilNo = (grdSrvRCustomer.Rows[i].Cells["colMobNo"].Value != null) ? grdSrvRCustomer.Rows[i].Cells["colMobNo"].Value.ToString() : null;
                    name = (grdSrvRCustomer.Rows[i].Cells["colCusNme"].Value != null) ? grdSrvRCustomer.Rows[i].Cells["colCusNme"].Value.ToString() : null;
                    jobDate = (grdSrvRCustomer.Rows[i].Cells["colJOBdte"].Value != null) ? grdSrvRCustomer.Rows[i].Cells["colJOBdte"].Value.ToString() : null;
                    email = (grdSrvRCustomer.Rows[i].Cells["colEmail"].Value != null) ? grdSrvRCustomer.Rows[i].Cells["colEmail"].Value.ToString() : null;
                    bool chkState = (bool)testChk.Value;
                    if (chkState && mobilNo != null)
                    {
                        foundRcrd = 1;
                        string ValidaMobileNumber;
                        if (IsValidMobileNo(mobilNo, out ValidaMobileNumber, jobNO, name))
                        {
                            foundRcrd = 1;
                            //int tempLineNo = 0;
                            // int.TryParse(jobLine, out tempLineNo);
                            _smsOutLst.Add(new SmsOutMember
                            {
                                SmsOutMsg = txtSrvRMessage.Text.Trim(),
                                SmsOutReciver = BaseCls.GlbUserDefProf,
                                SmsOutReciverPhNo = ValidaMobileNumber,
                                SmsOutSender = BaseCls.GlbUserID,
                                SmsOutSnderPhnNo = ValidaMobileNumber,
                                SmsOutValidPhnNo = ValidaMobileNumber,
                                //ref
                                refReminderID = reminderID,
                                refComName = BaseCls.GlbUserComCode,
                                refProfitCnter = BaseCls.GlbUserDefProf,
                                refLocation = BaseCls.GlbUserDefLoca,
                                refJobNo = jobNO,
                                refLineNo = 0,
                                refEstimateNum = "0",
                                refSmsTxt = txtSrvREmail.Text,
                                refEmail = txtSrvREmail.Text,
                                refOutSeq = 0,
                                refSmsStus = 2,
                                refEmStus = 0,
                                refCreBy = BaseCls.GlbUserID,
                                refCreDate = DateTime.Now,
                            });
                        }
                    }
                }
                if (foundRcrd == 1)
                {
                    string msg = string.Empty;
                    Int32 errroCode = CHNLSVC.CustService.SendReminderSms(_smsOutLst, out msg);
                    AddErrorLog(msg, errroCode, jobNO, name);
                    return;
                }
                else
                {
                    MessageBox.Show("Please select customer.", "Service Reminder Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select customer job details.", "Service Reminder Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
        }


        private void grdSrvRCustomer_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void ClearErrorLog()
        {
            if (lstErrorLog.Items.Count >= 5)
            {
                lstErrorLog.Items.Clear();
            }
        }

        /// <summary>
        /// Is the valid mobile no.
        /// </summary>
        /// <param name="_mobileNo">The mobile no.</param>
        /// <returns></returns>
        private bool IsValidMobileNo(string _mobile, out string MobileNo, string jobNo, string customerName)
        {
            int _cnvMobileNo = 0;
            string _mobileNo = _mobile;
            //if (_mobile.Contains("+94"))
            //{
            //    _mobileNo =  _mobile.Replace("+94", "0");
            //}
            if (int.TryParse(_mobileNo, out _cnvMobileNo) | _mobileNo.Substring(0, 1).ToString() == "+")
            {
                string lterOne = _mobileNo.Substring(0, 1).ToString();
                switch (lterOne)
                {
                    case "0":
                        if (_mobileNo.Length == 10)
                        {
                            string lterTwo = _mobileNo.Substring(0, 2).ToString();
                            if (_mobileNo.Substring(0, 2).ToString() == "07")
                            {
                                //_mobileNo = _mobileNo.Replace("0", "+94");
                                _mobileNo = _mobileNo.Substring(0, 1).Replace("0", "+94") + _mobileNo.Substring(1, 9);
                                MobileNo = _mobileNo;
                                return true;
                            }
                        }
                        break;
                    case "7":
                        if (_mobileNo.Length == 9)
                        {
                            MobileNo = "+94" + _mobileNo;
                            return true;
                        }
                        break;
                    case "+":
                        if (_mobileNo.Length == 12)
                        {
                            MobileNo = _mobileNo;
                            return true;
                        }
                        break;

                    default:
                        MobileNo = null;
                        return false;
                }
            }
            ClearErrorLog();
            AddErrorLog("invalid mobile no...", 0, jobNo, customerName);
            MobileNo = null;
            return false;
        }


        /// <summary>
        /// Adds the error log.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="errorCode">The error code.</param>
        private void AddErrorLog(string error, Int32 errorCode, string jobNo, string customerName)
        {
            ClearErrorLog();
            ListViewItem viewItem = new ListViewItem(DateTime.Now.ToShortTimeString() + " : " + error + " , " + jobNo + " , " + customerName);
            viewItem.SubItems[0].ForeColor = (errorCode <= 0) ? Color.Red : Color.Green;
            lstErrorLog.Items.Add(viewItem);
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSelectAll.Checked == true)
                {
                    lblSelectedAcc.Text = grdSrvRCustomer.Rows.Count.ToString();
                    foreach (DataGridViewRow row in grdSrvRCustomer.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = true;
                    }
                    grdSrvRCustomer.EndEdit();
                }
                else
                {
                    lblSelectedAcc.Text = "0";
                    foreach (DataGridViewRow row in grdSrvRCustomer.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        chk.Value = false;
                    }
                    grdSrvRCustomer.EndEdit();
                }
            }
            catch
            {
                return;
            }
        }

        private void grdSrvRCustomer_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (grdSrvRCustomer.IsCurrentCellDirty)
            {
                grdSrvRCustomer.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private List<Service_Reminder> srvReminderLst = new List<Service_Reminder>();
        private void grdSrvRCustomer_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (grdSrvRCustomer.Rows.Count > 0)
            {
                if (e.RowIndex != -1)
                {
                    Service_Reminder serviceReminder = srvReminderLst.Find(x => x.SjbJobNO == grdSrvRCustomer.Rows[e.RowIndex].Cells["colJobNo"].Value.ToString());
                    // oItem.ESI_ITM_Description = dgvEstimateItems.Rows[e.RowIndex].Cells["Description"].Value.ToString();
                }
            }
        }

        /// <summary>
        /// Gets the type of the selected MSG.
        /// </summary>
        /// <returns></returns>
        public string getSelectedMsgType(out int reminderID)
        {
            if (radSrvREstimate.Checked)
            {
                reminderID = 2;
                return "ESTIMATE";
            }
            else if (radSrvRJComplte.Checked)
            {
                reminderID = 3;
                return "JOB COMPLETED";
            }
            else if (radSrvRGnrlTxt.Checked)
            {
                reminderID = 1;
                return "GENERAL TEXT";
            }
            reminderID = -1;
            return null;
        }

        private string loadMsgHasTemp(string _msgTypeDesc, out string emailTemplate)
        {
            string msg = null;
            if (_templateLst.Count != 0)
            {
                var msgTemp = _templateLst.Find(x => x.Rrd_rmt_desc == _msgTypeDesc);
                emailTemplate = msgTemp.Rrd_em_tmpt_e;
                return msgTemp.Rrd_sm_tmpt_e;
            }
            else
            {
                _templateLst = new List<Service_Reminder_Template>();
                //_templateLst = CHNLSVC.CustService.GetReminderTemplate();
            }
            emailTemplate = null;
            return msg;
        }


        #region Message type changed event
        string emailTemplate = null;
        private void radSrvREstimate_CheckedChanged(object sender, EventArgs e)
        {

            txtSrvRMessage.Text = loadMsgHasTemp(getSelectedMsgType(out reminderID), out emailTemplate);
            txtSrvREmail.Text = emailTemplate;
        }

        private void radSrvRJComplte_CheckedChanged(object sender, EventArgs e)
        {
            txtSrvRMessage.Text = loadMsgHasTemp(getSelectedMsgType(out reminderID), out emailTemplate);
            txtSrvREmail.Text = emailTemplate;
        }
        #endregion

        private void ClearAll()
        {
            radSrvREstimate.Checked = true;
            dteSrvRFrom.Text = "";
            dteSrvRTo.Text = "";
            txtSrvRJob.Text = "";
            lstErrorLog.Items.Clear();
            grdSrvRCustomer.Rows.Clear();
        }

        private void btnSrvRClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }


        private void grdSrvRCustomer_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridViewCheckBoxCell _chk = null;
                lblSelectedAcc.Text = "0";
                int count = 0;
                for (int i = 0; i < grdSrvRCustomer.RowCount; i++)
                {
                    _chk = (DataGridViewCheckBoxCell)grdSrvRCustomer.Rows[i].Cells["chkRow"];
                    if ((bool)_chk.Value)
                    {
                        count++;
                    }
                    else
                    {
                        count = (count != 0) ? count-- : count;
                    }
                }
                lblSelectedAcc.Text = count.ToString();
            }
        }

        private void txtComp_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtComp_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F2)
            {
                txtComp_DoubleClick(null, null);
            }
        }

        private void txtChanel_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtChanel_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtChanel;
            _CommonSearch.ShowDialog();
            txtChanel.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "CHNL", txtChanel.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtChnlDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtSChanel_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSChanel;
            _CommonSearch.ShowDialog();
            txtSChanel.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "SCHNL", txtSChanel.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtSChnlDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtArea_DoubleClick(object sender, EventArgs e)
        {

            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtArea;
            _CommonSearch.ShowDialog();
            txtArea.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "AREA", txtArea.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtAreaDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtRegion_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtRegion;
            _CommonSearch.ShowDialog();
            txtRegion.Select();


            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "REGION", txtRegion.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtRegDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtZone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtZone_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtZone;
            _CommonSearch.ShowDialog();
            txtZone.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "ZONE", txtZone.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtZoneDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtPC_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPC_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPC;
            _CommonSearch.ShowDialog();
            txtPC.Select();

            load_PCDesc();
        }
        private void load_PCDesc()
        {
            txtPCDesn.Text = "";
            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "PC", txtPC.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtPCDesn.Text = row2["descp"].ToString();
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            LoadPcs();
        }

        private void txtComp_DoubleClick(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtComp;
            _CommonSearch.ShowDialog();
            txtComp.Select();

        }

        private void btnItemsearch_Click(object sender, EventArgs e)
        {
            pnlItem.Visible = true;
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            pnlItem.Visible = false;
        }

        private void btnBrand_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBrand;
                _CommonSearch.txtSearchbyword.Text = txtBrand.Text;
                _CommonSearch.ShowDialog();
                txtBrand.Focus();
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

        private void btnMainCat_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCate1;
                _CommonSearch.txtSearchbyword.Text = txtCate1.Text;
                _CommonSearch.ShowDialog();
                txtCate1.Focus();
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

        private void btnCat_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCate2;
                _CommonSearch.txtSearchbyword.Text = txtCate2.Text;
                _CommonSearch.ShowDialog();
                txtCate2.Focus();
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

        private void btnModel_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                DataTable _result = CHNLSVC.CommonSearch.GetAllModels(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtModel; //txtBox;
                _CommonSearch.ShowDialog();
                txtModel.Focus();
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

        private void btnItem_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = TextBoxItem;
                _CommonSearch.txtSearchbyword.Text = TextBoxItem.Text;
                _CommonSearch.ShowDialog();
                TextBoxItem.Focus();
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

        private void btnSerial_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 6;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSerialSearchData(_CommonSearch.SearchParams);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerial;
                _CommonSearch.txtSearchbyword.Text = txtSerial.Text;
                _CommonSearch.ShowDialog();
                txtSerial.Focus();
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

        private void btnCircular_Click(object sender, EventArgs e)
        {

            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circular);
                DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCircular;
                _CommonSearch.txtSearchbyword.Text = txtCircular.Text;
                _CommonSearch.ShowDialog();
                txtCircular.Focus();
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

        private void btnPromation_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
                DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPromotion;
                _CommonSearch.txtSearchbyword.Text = txtPromotion.Text;
                _CommonSearch.ShowDialog();
                txtPromotion.Focus();
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


        private void getitems(object sender, EventArgs e)
        {

            string _selected = string.Empty;
            _selected = txtBrand.Text.Trim() + "|" + txtCate1.Text.Trim() + "|" + txtCate2.Text.Trim() + "|" + txtModel.Text.Trim() + "|" + TextBoxItem.Text.Trim() + "|" + txtSerial.Text.Trim() + "|" + txtCircular.Text.Trim() + "|" + txtPromotion.Text.Trim();
            string[] _split = _selected.Split('|');
            int _count = 0;
            foreach (string _n in _split)
            {
                if (!string.IsNullOrEmpty(_n))
                    _count++;
            }

            if (_count == 0)
            {
                MessageBox.Show("Please select the any criteria", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            string selection = string.Empty;
            string _val = string.Empty;

            if (!string.IsNullOrEmpty(TextBoxItem.Text)) selection = "ITEM";
            if (!string.IsNullOrEmpty(txtBrand.Text)) selection = "ITEM";
            if (!string.IsNullOrEmpty(txtCate1.Text)) selection = "ITEM";
            if (!string.IsNullOrEmpty(txtCate2.Text)) selection = "ITEM";
            if (!string.IsNullOrEmpty(txtModel.Text)) selection = "ITEM";
            if (!string.IsNullOrEmpty(txtSerial.Text)) selection = "SERIAL";
            if (!string.IsNullOrEmpty(txtCircular.Text)) selection = "CIRCULER";
            if (!string.IsNullOrEmpty(txtPromotion.Text)) selection = "PROMOTION";


            List<CashCommissionDetailRef> addList = new List<CashCommissionDetailRef>();
            DataTable dt = CHNLSVC.Sales.GetInsuCriteria(BaseCls.GlbUserComCode, selection, TextBoxItem.Text.Trim(), txtBrand.Text.Trim(), txtModel.Text.Trim(), txtCate1.Text.Trim(), txtCate2.Text.Trim(), txtSerial.Text.Trim(), txtCircular.Text.Trim(), txtPromotion.Text.Trim());
            System.Data.DataColumn newColumn = new System.Data.DataColumn("Value", typeof(System.Decimal));
            newColumn.DefaultValue = 0;
            dt.Columns.Add(newColumn);
            System.Data.DataColumn serial = new System.Data.DataColumn("Serial", typeof(System.String));
            serial.DefaultValue = txtSerial.Text.Trim();
            dt.Columns.Add(serial);
            System.Data.DataColumn promotion = new System.Data.DataColumn("Promotion", typeof(System.String));
            promotion.DefaultValue = txtPromotion.Text.Trim();
            dt.Columns.Add(promotion);
            System.Data.DataColumn Circuler = new System.Data.DataColumn("Circuler", typeof(System.String));
            Circuler.DefaultValue = txtPromotion.Text.Trim();
            dt.Columns.Add(Circuler);

            DataTable dataSource2 = new DataTable();
            dataSource2.Columns.Add("CODE", typeof(System.String));
            dataSource2.Columns.Add("DESCRIPT", typeof(System.String));
            dataSource2.Columns.Add("Value", typeof(System.Decimal));
            System.Data.DataColumn serial1 = new System.Data.DataColumn("Serial", typeof(System.String));
            serial1.DefaultValue = txtSerial.Text.Trim();
            dataSource2.Columns.Add(serial1);
            System.Data.DataColumn promotion1 = new System.Data.DataColumn("Promotion", typeof(System.String));
            promotion1.DefaultValue = txtPromotion.Text.Trim();
            dataSource2.Columns.Add(promotion1);
            System.Data.DataColumn Circuler1 = new System.Data.DataColumn("Circuler", typeof(System.String));
            Circuler1.DefaultValue = txtPromotion.Text.Trim();
            dataSource2.Columns.Add(Circuler1);

            foreach (DataRow drr in dt.Rows)
            {
                string itmcd = drr["CODE"].ToString();
                string descirption = drr["DESCRIPT"].ToString();
                string _book = string.Empty;
                string _level = string.Empty;
                string _promotion = txtPromotion.Text.Trim();
                decimal _price = -1;
                //DataTable _value = CHNLSVC.Sales.GetPriceForItem(_book, _level, itmcd, DateTime.Now.Date, _promotion, txtCircular.Text.Trim());
                //if (_value == null || _value.Rows.Count <= 0)
                //    continue;
                //if (_value != null && _value.Rows.Count > 0)
                //    _price = _value.Rows[0].Field<decimal>("sapd_itm_price");
                //List<MasterItemTax> _tx = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, itmcd, "GOD", "VAT", string.Empty);
                //if (_tx != null && _tx.Count > 0 && _price != -1)
                //    _price = _price * ((100 + _tx[0].Mict_tax_rate) / 100);
                //drr.SetField("Value", Math.Round(_price));

                var _duplicate = from _dup in select_ITEMS_List.AsEnumerable()
                                 where _dup["code"].ToString() == itmcd
                                 select _dup;
                if (_duplicate.Count() == 0)
                {
                    DataRow DR2 = dataSource2.NewRow();
                    DR2["CODE"] = itmcd;
                    DR2["DESCRIPT"] = descirption;
                    DR2["Value"] = Math.Round(_price);
                    if (!string.IsNullOrEmpty(txtSerial.Text)) DR2["Serial"] = txtSerial.Text.Trim();
                    if (!string.IsNullOrEmpty(txtPromotion.Text)) DR2["Promotion"] = txtPromotion.Text.Trim();
                    if (!string.IsNullOrEmpty(txtCircular.Text)) DR2["Circuler"] = txtCircular.Text.Trim();
                    dataSource2.Rows.Add(DR2);
                }
            }

            select_ITEMS_List.Merge(dataSource2);
            GridAll_Items.DataSource = null;
            GridAll_Items.AutoGenerateColumns = false;
            GridAll_Items.DataSource = select_ITEMS_List;
            this.btnAllItem_Click(sender, e);

            txtBrand.Clear();
            txtCate1.Clear();
            txtCate2.Clear();
            txtModel.Clear();
            TextBoxItem.Clear();
            txtSerial.Clear();
            txtCircular.Clear();
            txtPromotion.Clear();

        }



        private void btnAllItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in GridAll_Items.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                GridAll_Items.EndEdit();



            }
            catch (Exception ex)
            {

            }
        }

        private void btnNonItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in GridAll_Items.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                GridAll_Items.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnClearItem_Click(object sender, EventArgs e)
        {
            select_ITEMS_List = new DataTable();
            GridAll_Items.DataSource = null;
            GridAll_Items.AutoGenerateColumns = false;
            GridAll_Items.DataSource = select_ITEMS_List;
        }

        private void txtBrand_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtCate1.Focus();
            if (e.KeyCode == Keys.F2) btnBrand_Click(null, null);
        }
        private void txtCate1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btnMainCat_Click(null, null);
            if (e.KeyCode == Keys.Enter) txtCate2.Focus();
        }
        private void txtCate2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btnCat_Click(null, null);
            if (e.KeyCode == Keys.Enter) txtModel.Focus();
        }
        private void txtModel_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btnModel_Click(null, null);
            if (e.KeyCode == Keys.Enter) TextBoxItem.Focus();
        }
        private void TextBoxItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btnItem_Click(null, null);
            if (e.KeyCode == Keys.Enter) txtSerial.Focus();
        }
        private void txtSerial_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btnSerial_Click(null, null);
            if (e.KeyCode == Keys.Enter) txtCircular.Focus();
        }
        private void txtCircular_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btnCircular_Click(null, null);
            if (e.KeyCode == Keys.Enter) txtPromotion.Focus();
        }
        private void txtPromotion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2) btnPromation_Click(null, null);
            if (e.KeyCode == Keys.Enter) btnSearch.Select();
        }
        private void txtBrand_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnBrand_Click(null, null);
        }
        private void txtCate1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnMainCat_Click(null, null);
        }
        private void txtCate2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnCat_Click(null, null);
        }
        private void txtModel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnModel_Click(null, null);
        }
        private void TextBoxItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnItem_Click(null, null);
        }
        private void txtSerial_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSerial_Click(null, null);
        }
        private void txtCircular_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnCircular_Click(null, null);
        }
        private void txtPromotion_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPromation_Click(null, null);
        }

        private void txtBrand_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddItems_Click(object sender, EventArgs e)
        {
            getitems(sender, e);
        }

        private void dgvMsgColums_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvMsgColums_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvMsgColums_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMsgColums.Rows.Count > 0)
            {
                GeneralText = txtMessage.Text + "[" + dgvMsgColums.Rows[e.RowIndex].Cells["col_msgdec"].Value.ToString() + " ]";
                txtMessage.Text = GeneralText;
                txtMessage.Focus();
            }
        }

        private void lstPC_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void grvTp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDateClose_Click(object sender, EventArgs e)
        {
            pnlDate.Visible = false;
        }

        private void btnDateSearch_Click(object sender, EventArgs e)
        {
            pnlDate.Visible = true;
        }

        private void chkInv_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInv.Checked == true)
            {
                dtpInvFrom.Enabled = true;
                dtpInvTo.Enabled = true;
            }
            else
            {
                dtpInvFrom.Enabled = false;
                dtpInvTo.Enabled = false;
            }
        }

        private void chkReg_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReg.Checked == true)
            {
                dtpRegFrom.Enabled = true;
                dtpRegTo.Enabled = true;
            }
            else
            {
                dtpRegFrom.Enabled = false;
                dtpRegTo.Enabled = false;
            }
        }

        private void chkRec_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRec.Checked == true)
            {
                dtpcrfrom.Enabled = true;
                dtpcrTo.Enabled = true;
            }
            else
            {
                dtpcrfrom.Enabled = false;
                dtpcrTo.Enabled = false;
            }
        }

        private void chkPlate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPlate.Checked == true)
            {
                dtpnplateFrom.Enabled = true;
                dtpnplateTo.Enabled = true;
            }
            else
            {
                dtpnplateFrom.Enabled = false;
                dtpnplateTo.Enabled = false;
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void optManager_CheckedChanged(object sender, EventArgs e)
        {
            pnlMyAb.Visible = false;
            if (optManager.Checked == true)
            {
                pnlSaleType.Visible = false;
                pnlBal.Visible = false;
                cmbType.Visible = true;
            }
            else
            {
                pnlSaleType.Visible = true;
                pnlBal.Visible = true;
                cmbType.Visible = false;
                _dtCont = new DataTable();
            } 
        }

        private void optCustomer_CheckedChanged(object sender, EventArgs e)
        {
            pnlMyAb.Visible = false;
            pnlSaleType.Visible = true;
        }

        private void btnClearPC_Click(object sender, EventArgs e)
        {
            lstPC.Clear();
            txtChanel.Text = "";
            txtSChanel.Text = "";
            txtArea.Text = "";
            txtRegion.Text = "";
            txtZone.Text = "";
            txtPC.Text = "";
            txtChnlDesc.Text = "";
            txtSChnlDesc.Text = "";
            txtAreaDesc.Text = "";
            txtZoneDesc.Text = "";
            txtRegDesc.Text = "";
            txtPCDesn.Text = "";
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstPC.Clear();
            if (cmbType.Text == "Profit Center")
            {
                txtChanel.Enabled = true;
                txtSChanel.Enabled = true;
                txtArea.Enabled = true;
                txtRegion.Enabled = true;
                txtZone.Enabled = true;
                txtPC.Enabled = true;
            }
            else
            {
                txtChanel.Enabled = false;
                txtSChanel.Enabled = false;
                txtArea.Enabled = false;
                txtRegion.Enabled = false;
                txtZone.Enabled = false;
                txtPC.Enabled = false;
            }
        }

        private void btn_Srch_chnl_Click(object sender, EventArgs e)
        {
            txtChanel_DoubleClick(null, null);
        }

        private void btn_Srch_schnl_Click(object sender, EventArgs e)
        {
            txtSChanel_DoubleClick(null, null);
        }

        private void btn_Srch_area_Click(object sender, EventArgs e)
        {
            txtArea_DoubleClick(null, null);
        }

        private void btn_Srch_reg_Click(object sender, EventArgs e)
        {
            txtRegion_DoubleClick(null, null);
        }

        private void btn_Srch_zone_Click(object sender, EventArgs e)
        {
            txtZone_DoubleClick(null, null);
        }

        private void btn_Srch_pc_Click(object sender, EventArgs e)
        {
            txtPC_DoubleClick(null, null);
        }

        private void txtChnlDesc_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBrItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "Excel files(*.xls)|*.xls,*.xlsx|All files(*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.ShowDialog();
            txtUploadItems.Text = openFileDialog1.FileName;
        }

        List<sar_pb_def_det> ErrMobile_List;
        List<sar_pb_def_det> SMSMobile_List;
        private void btnUploadItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (optcomtext.Checked == true)
                {
                    if (string.IsNullOrEmpty(txtmsg.Text))
                    {
                        MessageBox.Show("Please enter a message", "General SMS Mobile Number Upload", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }

                string _msg = string.Empty;
                StringBuilder _errorLst = new StringBuilder();
                SMSMobile_List = new List<sar_pb_def_det>();
                ErrMobile_List = new List<sar_pb_def_det>();
                if (string.IsNullOrEmpty(txtUploadItems.Text))
                {
                    MessageBox.Show("Please select upload file path.", "General SMS Mobile Number Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUploadItems.Text = "";
                    txtUploadItems.Focus();
                    return;
                }

                System.IO.FileInfo fileObj = new System.IO.FileInfo(txtUploadItems.Text);

                if (fileObj.Exists == false)
                {
                    MessageBox.Show("Selected file does not exist at the following path.", "General SMS Mobile Number Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUploadItems.Focus();
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

                conStr = String.Format(conStr, txtUploadItems.Text, "NO");
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
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
                oda.Fill(dt);
                connExcel.Close();

                grverrmobile.AutoGenerateColumns = true;
                grvmobile.AutoGenerateColumns = true;
                Decimal SMSNo = 0;
                Decimal CurrSMSNo = 0;
                HpSystemParameters _SystemPara = new HpSystemParameters();
                _SystemPara = CHNLSVC.Sales.GetSystemParameter("COM", BaseCls.GlbUserComCode, "MAXBULKSMS", Convert.ToDateTime(DateTime.Now).Date);
                if (_SystemPara.Hsy_cd != null)
                {
                    SMSNo = _SystemPara.Hsy_val;
                }
                else
                {
                    SMSNo = 1000;
                }
                //grverrmobile.DataSource = ErrMobile_List;
                //grvmobile.DataSource = SMSMobile_List;

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow _dr in dt.Rows)
                    {
                        if (string.IsNullOrEmpty(_dr[0].ToString()))
                        {
                            continue;
                        }

                        if (SMSMobile_List != null)
                        {
                            var _duplicate = from _dup in SMSMobile_List
                                             where _dup.Spdd_item == _dr[0].ToString()
                                             select _dup;

                            if (_duplicate.Count() > 0)
                            {
                                sar_pb_def_det _referr = new sar_pb_def_det();
                                _referr.Spdd_item = _dr[0].ToString();
                                _referr.Spdd_Des = "Duplicate Mobile Number";
                                ErrMobile_List.Add(_referr);

                                if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("Mobile Number  " + _dr[0].ToString() + " duplicate");
                                else _errorLst.Append(" and Mobile Number " + _dr[0].ToString() + " duplicate");
                                continue;
                            }

                            string ValidaMobileNumber;
                            if (IsValidMobileNo(_dr[0].ToString(), out ValidaMobileNumber, "", ""))
                            {
                                if (optcomtext.Checked == false)
                                {
                                    if (_dr[1].ToString() == "")
                                    {
                                        sar_pb_def_det _referr = new sar_pb_def_det();
                                        _referr.Spdd_item = _dr[0].ToString();
                                        _referr.Spdd_Des = "Message Not Available";
                                        ErrMobile_List.Add(_referr);

                                        if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("Message Not Available");
                                        else _errorLst.Append(" and Message Not Available");
                                        continue;
                                    }
                                    if (_dr[1].ToString().Length > 299)
                                    {
                                        sar_pb_def_det _referr = new sar_pb_def_det();
                                        _referr.Spdd_item = _dr[0].ToString();
                                        _referr.Spdd_Des = "Message Length Exceeds";
                                        ErrMobile_List.Add(_referr);

                                        if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("Message Length Exceeds");
                                        else _errorLst.Append(" and Message Length Exceeds");
                                        continue;
                                    }
                                    //else
                                    //{
                                    //    sar_pb_def_det _ref = new sar_pb_def_det();
                                    //    _ref.Spdd_item = ValidaMobileNumber;
                                    //    if (optcomtext.Checked == true)
                                    //    { _ref.Spdd_Des = txtmsg.Text.Trim(); }
                                    //    else { _ref.Spdd_Des = _dr[0].ToString(); }

                                    //    SMSMobile_List.Add(_ref);
                                    //}
                                }
                                CurrSMSNo = CurrSMSNo + 1;
                                if (CurrSMSNo <= SMSNo)
                                {
                                    sar_pb_def_det _ref = new sar_pb_def_det();
                                    //_ref.Spdd_active = 1;
                                    _ref.Spdd_item = ValidaMobileNumber;
                                    if (optcomtext.Checked == true)
                                    { _ref.Spdd_Des = txtmsg.Text.Trim(); }
                                    else { _ref.Spdd_Des = _dr[1].ToString(); }

                                    SMSMobile_List.Add(_ref);
                                }
                            }
                            else
                            {
                                sar_pb_def_det _referr = new sar_pb_def_det();
                                _referr.Spdd_item = _dr[0].ToString();
                                _referr.Spdd_Des = "Invalid Mobile Number";
                                ErrMobile_List.Add(_referr);
                                continue;
                            }
                        }


                    }
                }

                if (CurrSMSNo > SMSNo)
                {
                    if (MessageBox.Show("Your Message limit Exceeded. First " + Convert.ToString(SMSNo) + " numbers will be sent. Do you want to continue?", "General Text Mobile Number Upload", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    {
                        return;
                    }
                }
                grvmobile.AutoGenerateColumns = false;
                grvmobile.DataSource = SMSMobile_List;

                grverrmobile.AutoGenerateColumns = false;
                grverrmobile.DataSource = ErrMobile_List;

                MessageBox.Show("Successfully downloaded the Excel File. Press send button to send SMS.", "General Text Mobile Number Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show("Unable to upload. please select the correct file " + err.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }


        }

        private void btngensms_Click(object sender, EventArgs e)
        {
            int sentSMS = 0;
            int foundRcrd = 0;
            string mobilNo = null, msg = null;
            List<SmsOutMember> _smsOutLst = new List<SmsOutMember>();
            List<Sms_Ref_Log> _smsRefLog = new List<Sms_Ref_Log>();

            // add by akila 2017/08/16 Allow only alpha numerics and specific symbols
            if (!string.IsNullOrEmpty(txtmsg.Text.Trim()))
            {
                MatchCollection matches = AllowedCharacters.Matches(txtmsg.Text.Trim());
                if (matches != null && matches.Count > 0)
                {
                    MessageBox.Show("The message contains invalid characters", "General Message Sending Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (grvmobile.RowCount != 0)
            {
                for (int i = 0; i < grvmobile.Rows.Count; i++)
                {
                    //var testChk = grvmobile.Rows[i].Cells["genchkRow"];
                    mobilNo = (grvmobile.Rows[i].Cells["genmobile"].Value != null) ? grvmobile.Rows[i].Cells["genmobile"].Value.ToString() : null;
                    msg = (grvmobile.Rows[i].Cells["genmsg"].Value != null) ? grvmobile.Rows[i].Cells["genmsg"].Value.ToString() : null;
                    //bool chkState = (bool)testChk.Value;
                    if (mobilNo != null)
                    {
                        // updated by akila - 2018/02/14
                        string _errorMessage =string.Empty;
                        if (!IsCustomerRestrictedToSendSms(mobilNo, ref _errorMessage))
                        {
                            foundRcrd = 1;
                            string ValidaMobileNumber;
                            ValidaMobileNumber = mobilNo;
                            //if (IsValidMobileNo(mobilNo, out ValidaMobileNumber, "", ""))
                            //{
                            foundRcrd = 1;

                            OutSMS smsout = new OutSMS();
                            smsout.Msg = msg;
                            smsout.Receiver = BaseCls.GlbUserDefProf;
                            smsout.Receiverphno = ValidaMobileNumber;
                            smsout.Sender = BaseCls.GlbUserID;
                            smsout.Senderphno = ValidaMobileNumber;
                            smsout.Seqno = reminderID;
                            smsout.Msgstatus = 1;
                            smsout.Msgtype = "GEN_E";
                            smsout.Createtime = DateTime.Now;
                            smsout.comcode = "MYAB";    //kapila    22/11/2016

                            Int32 errroCode = CHNLSVC.General.SaveSMSOut(smsout);
                            if (errroCode == 0)
                            {
                                sentSMS = sentSMS + 1;
                            }
                        }
                        
                        //_smsOutLst.Add(new SmsOutMember
                        //{
                        //    SmsOutMsg = msg,
                        //    SmsOutReciver = BaseCls.GlbUserDefProf,
                        //    SmsOutReciverPhNo = ValidaMobileNumber,
                        //    SmsOutSender = BaseCls.GlbUserID,
                        //    SmsOutSnderPhnNo = ValidaMobileNumber,
                        //    SmsOutValidPhnNo = ValidaMobileNumber,
                        //    //ref
                        //    refReminderID = reminderID,
                        //    refComName = BaseCls.GlbUserComCode,
                        //    refProfitCnter = BaseCls.GlbUserDefProf,
                        //    refLocation = BaseCls.GlbUserDefLoca,                                
                        //    refLineNo = 0,
                        //    refEstimateNum = "0",
                        //    refOutSeq = 0,
                        //    refSmsStus = 2,
                        //    refEmStus = 0,
                        //    refCreBy = BaseCls.GlbUserID,
                        //    refCreDate = DateTime.Now,
                        //});
                    }
                    //}
                }
                //if (foundRcrd == 1)
                //{
                //    string msg1 = string.Empty;
                //    Int32 errroCode = CHNLSVC.General.SaveSMSOut(_smsOutLst);
                //    AddErrorLog(msg1, errroCode, "General Message", mobilNo);
                //    return;
                //}
                //else
                //{
                lblMsg.Text = "No of Messages Sent : " + sentSMS;
                MessageBox.Show("Successfully Sent the Messages.", "General Message Sending Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SMSMobile_List = new List<sar_pb_def_det>();
                ErrMobile_List = new List<sar_pb_def_det>();

                grvmobile.AutoGenerateColumns = false;
                grvmobile.DataSource = SMSMobile_List;

                return;
                //}
            }
            else
            {
                if (string.IsNullOrEmpty(txtmobile.Text))
                {
                    MessageBox.Show("No Record Found.", "General Message Sending Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    string ValidaMobileNumber;
                    if (IsValidMobileNo(txtmobile.Text, out ValidaMobileNumber, "", ""))
                    {
                        // updated by akila - 2018/02/14
                        string _errorMessage = string.Empty;
                        if (IsCustomerRestrictedToSendSms(txtmobile.Text.Trim(), ref _errorMessage))
                        {
                            foundRcrd = 1;

                            OutSMS smsout = new OutSMS();
                            smsout.Msg = txtmsg.Text;
                            smsout.Receiver = BaseCls.GlbUserDefProf;
                            smsout.Receiverphno = ValidaMobileNumber;
                            smsout.Sender = BaseCls.GlbUserID;
                            smsout.Senderphno = ValidaMobileNumber;
                            smsout.Seqno = reminderID;
                            smsout.Msgstatus = 1;
                            smsout.Msgtype = "GEN_I";
                            smsout.Createtime = DateTime.Now;

                            Int32 errroCode = CHNLSVC.General.SaveSMSOut(smsout);
                        }
                        else
                        {
                            MessageBox.Show(_errorMessage, "General Message Sending Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        ErrMobile_List = new List<sar_pb_def_det>();
                        sar_pb_def_det _referr = new sar_pb_def_det();
                        _referr.Spdd_item = txtmobile.Text;
                        _referr.Spdd_Des = "Invalid Mobile Number";
                        ErrMobile_List.Add(_referr);
                    }
                }
            }
        }

        private void btnindadd_Click(object sender, EventArgs e)
        {


        }

        private void HPRemindersSMS_Load(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10115))
            {
                (tabControl1.TabPages[2] as Control).Enabled = false;
            }
            else
            {
                (tabControl1.TabPages[2] as Control).Enabled = true;
            }
        }

        private void label53_Click(object sender, EventArgs e)
        {

        }

        private void optGuarantor_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void optMyAb_CheckedChanged(object sender, EventArgs e)
        {
            txtMessage.Enabled = true;
            pnlAsAt.Visible = false;
            pnlArs.Visible = false;
            pnlRange.Visible = false;
            pnlDue.Visible = false;
           // optGuarantor.Visible = false;
           // optManager.Visible = false;
            cmbType.Visible = false;

            pnlSaleType.Visible = false;
            pnlBal.Visible = false;
            pnlMyAb.Visible = true;
        }

        private void btn_Srch_town_Click(object sender, EventArgs e)
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

        private void txtmsg_KeyPress(object sender, KeyPressEventArgs e)
        {
            // add by akila 2017/08/16 Allow only alpha numerics and specific symbols
            MatchCollection matches = AllowedCharacters.Matches(e.KeyChar.ToString());
            if (matches != null && matches.Count > 0)
            {
                e.Handled = true;
            }
        }

        private void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            // add by akila 2017/08/16 Allow only alpha numerics and specific symbols
            MatchCollection matches = AllowedCharacters.Matches(e.KeyChar.ToString());
            if (matches != null && matches.Count > 0)
            {
                e.Handled = true;
            }
        }

        //akila 2018/02/14 - Check whether customer is restricted to send sms
        private bool IsCustomerRestrictedToSendSms(string _mobileNo, ref string _message)
        {
            _message = string.Empty;
            bool _isRestricted = false;

            try
            {
                MasterBusinessEntity _customer = new MasterBusinessEntity();
                _customer = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(BaseCls.GlbUserComCode, string.Empty, string.Empty, _mobileNo, "C");
                if ((_customer != null) && (!string.IsNullOrEmpty(_customer.Mbe_cd)))
                {
                    _isRestricted = _customer.Mbe_agre_send_sms;
                    if (_isRestricted == false)
                    {
                        _message = string.Format("Sending SMS to customer - {0} has been restricted! Mobile# {1}", _customer.Mbe_cd, _mobileNo);
                    }
                }
                else
                {
                    _isRestricted = true;
                }
            }
            catch (Exception ex)
            {
                _message = "Error occurred while validating customer details" + Environment.NewLine + ex.Message;
                _isRestricted = true;
            }

            return _isRestricted;
        }

        // end modification
        // damith 

    }
}
