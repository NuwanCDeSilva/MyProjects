using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Net;

namespace FF.WindowsERPClient.Finance
{
    public partial class PhysicalStockVerification : Base
    {
        public PhysicalStockVerification()
        {
            InitializeComponent();
        }

        //variables
        List<PhysicalStockVerificationSerial> _serailList = new List<PhysicalStockVerificationSerial>();
        List<PhysicalStockVerificationItem> _itemList = new List<PhysicalStockVerificationItem>();
        List<PhysicalStockVerificationSerial> _itemLedgerSerials = new List<PhysicalStockVerificationSerial>();
        List<PhysicalStockVerificationSerial> _itemPhysicalSerials = new List<PhysicalStockVerificationSerial>();
        List<AuditRemarkValue> _AuditRmkList = new List<AuditRemarkValue>();
        int _serid = 0;
        int ledId = 0;
        int phyId = 0;

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                //01.get items and serials available in inventory
                List<PhysicalStockVerificationSerial> _temSerailList;
                List<PhysicalStockVerificationItem> _temItemList;
                MasterAutoNumber _auto = new MasterAutoNumber();
                _auto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                _auto.Aut_cate_tp = "LOC";
                _auto.Aut_start_char = "STJO";
                _auto.Aut_moduleid = "JOB_ST";
                _auto.Aut_direction = 0;
                string _error;
                string _job;
                int _result = CHNLSVC.Inventory.ProcessPhysicalStockVerification(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, out _temItemList, out _temSerailList, out _error, dtDate.Value.Date, BaseCls.GlbUserID, _auto,chkCommStatus.Checked==true?"Y":"N", out _job);
                if (_result == -1)
                {
                    MessageBox.Show("Error occurred while processing\n"+_error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    _itemList = _temItemList;
                    _serailList = _temSerailList;
                    txtJobNo.Text = _job;


                    _itemList = _itemList.OrderBy(x => x.Ausi_itm).ToList<PhysicalStockVerificationItem>();
                    //load report

                    //load data


                    _itemList.OrderByDescending(x => x.Ausi_itm);
                    DataTable _dt = _itemList.ToDataTable();
                    grItems.AutoGenerateColumns = false;
                    grItems.DataSource = _itemList;

                    BaseCls.GlbReportJobNo = _job;


                    BaseCls.GlbReportHeading = "PHYSICAL STOCK BALANCE COLLECTION SHEET";
                    BaseCls.GlbReportDocType = "S";
                    BaseCls.GlbReportStrStatus = "";
                    BaseCls.GlbReportType = "";
                    BaseCls.GlbReportDocMismatch = "N";

                    Reports.Audit.ReportViewerAudit _view = new Reports.Audit.ReportViewerAudit();
                    BaseCls.GlbReportName = "Audit_Phy_Stock_Bal_Coll.rpt";
                    _view.Show();
                    _view = null;

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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void grItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (e.ColumnIndex == 0)
                    {
                        PhysicalStockVerificationItem _itm = _itemList[e.RowIndex];
                        if (_itm != null)
                        {
                            MasterItem _msItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _itm.Ausi_itm);
                            txtItemCode.Text = _itm.Ausi_itm;
                            txtDescription.Text = _itm.Ausi_desc;
                            txtModel.Text = _msItem.Mi_model;
                            cmbStatus.SelectedValue = _itm.Ausi_stus;
                            txtPhyCount.Text = "0";
                            txtLedgerCount.Text = "0";
                            LoadLedgerAndPhysicalSerials(_msItem.Mi_cd,cmbStatus.SelectedValue.ToString());
                        }

                    }
                    /*
                    if (e.ColumnIndex == 1)
                    {
                        if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            _itemList.RemoveAt(e.RowIndex);
                            BindingSource _source = new BindingSource();
                            _source.DataSource = _itemList;
                            grItems.AutoGenerateColumns = false;
                            grItems.DataSource = _source;

                        }
                    }
                     */
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

        private void LoadLedgerAndPhysicalSerials(string _itm,string _itmstus)
        {

            List<PhysicalStockVerificationSerial> _ledger = CHNLSVC.Inventory.GetPhysicalLedgerSerials(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtJobNo.Text.Trim(), _itm, _itmstus, "L");
            if (_ledger != null)
            {
                _ledger = _ledger.GroupBy(x => x.Auss_ser_id).Select(x => x.First()).ToList<PhysicalStockVerificationSerial>();
                gvLedgerSerials.AutoGenerateColumns = false;
                gvLedgerSerials.DataSource = _ledger;
                txtLedgerCount.Text = _ledger.Where(x => x.Auss_type == "L").ToList<PhysicalStockVerificationSerial>().Count.ToString();
                _serailList.AddRange(_ledger);
            }
            else {
                gvLedgerSerials.DataSource = null;
            }
            List<PhysicalStockVerificationSerial> _physical = CHNLSVC.Inventory.GetPhysicalLedgerSerials(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, txtJobNo.Text.Trim(), _itm, _itmstus, "P");
            if (_physical != null)
            {
                _physical = _physical.GroupBy(x => x.Auss_ser_id).Select(x => x.First()).ToList<PhysicalStockVerificationSerial>();
                gvPhySerials.AutoGenerateColumns = false;
                gvPhySerials.DataSource = _physical;
                //txtPhyCount.Text = _physical.Where(x => x.Auss_type == "P").ToList<PhysicalStockVerificationSerial>().Count.ToString();
                int phyCou = _physical.Count;
                foreach (PhysicalStockVerificationSerial _ser in _physical)
                {
                    if (!string.IsNullOrEmpty(_ser.Auss_ref_stus))
                    {
                        List<AuditStatus> _all = CHNLSVC.Inventory.GetAllAuditStstus(BaseCls.GlbUserComCode);
                        List<AuditStatus> _stus = (from _res in _all
                                                   where _res.Auss_code == _ser.Auss_ref_stus
                                                   select _res).ToList<AuditStatus>();
                        if (_stus != null && _stus.Count > 0)
                        {
                            if (_stus[0].Auss_direction == 0)
                            {
                                //if (phyCou > 0)
                                phyCou = phyCou - 1;
                            }
                            //else if (_stus[0].Auss_direction == 1)
                            //{
                            //    phyCou = phyCou + 1;
                            //}
                            //else if (_stus[0].Auss_direction == 2)
                            //{
                            //    phyCou = phyCou + 1;
                            //}

                        }
                    }
                    //else {
                    //    phyCou = phyCou + 1;
                    //}
                }
                txtPhyCount.Text = phyCou.ToString();

                _serailList.AddRange(_physical);
            }
            else {
                gvPhySerials.DataSource = null;
            }


            //get ledger from db
            //get physical from db


            /*
            //add data to ledger and physical grid views
            List<PhysicalStockVerificationSerial> _temLedgerBalance = (from _res in _serailList
                                                                       where _res.Auss_item == txtItemCode.Text && _res.Auss_itm_stus == cmbStatus.SelectedValue.ToString() && _res.Auss_type == "L"
                                                                       select _res).ToList<PhysicalStockVerificationSerial>();
            if (_temLedgerBalance != null)
            {
                gvLedgerSerials.AutoGenerateColumns = false;
                gvLedgerSerials.DataSource = _temLedgerBalance;
                
            }
            List<PhysicalStockVerificationSerial> _temPhysicalBalance = (from _res in _serailList
                                                                         where _res.Auss_item == txtItemCode.Text && _res.Auss_itm_stus == cmbStatus.SelectedValue.ToString() && _res.Auss_type == "P"
                                                                         select _res).ToList<PhysicalStockVerificationSerial>();
            if (_temPhysicalBalance != null)
            {
                gvPhySerials.AutoGenerateColumns = false;
                gvPhySerials.DataSource = _temPhysicalBalance;
               
            }
             */
        }

        private void PhysicalStockVerification_Load(object sender, EventArgs e)
        {
            try
            {
                LoadItemStatus();
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

        private void LoadItemStatus()
        {
            DataTable dt = CHNLSVC.Inventory.GetAllCompanyStatus(BaseCls.GlbUserComCode);
            DataRow dr = dt.NewRow();
            dt.Rows.InsertAt(dr, 0);
            cmbStatus.DataSource = dt;
            cmbStatus.DisplayMember = "MIS_DESC";
            cmbStatus.ValueMember = "MIC_CD";
        }

        private void btnAddLedger_Click(object sender, EventArgs e)
        {
            try
            {

                List<PhysicalStockVerificationSerial> _temp1= (from _res in _itemLedgerSerials
                                                                   where _res.Auss_type == "L" && _res.Auss_item== txtItemCode.Text && _res.Auss_itm_stus == cmbStatus.SelectedValue.ToString()
                                                                   select _res).ToList<PhysicalStockVerificationSerial>();

                if (_temp1 != null && _temp1.Count > 0)
                {

                }
                else
                {
                    _itemLedgerSerials = new List<PhysicalStockVerificationSerial>();

                    List<PhysicalStockVerificationSerial> _temp = (from _res in _serailList
                                                                   where _res.Auss_type == "L" && _res.Auss_item == txtItemCode.Text && _res.Auss_itm_stus == cmbStatus.SelectedValue.ToString()
                                                                   select _res).ToList<PhysicalStockVerificationSerial>();
                    _temp = _temp.GroupBy(x => x.Auss_ser_id).Select(x => x.First()).ToList<PhysicalStockVerificationSerial>();

                    if (_temp != null && _temp.Count > 0)
                    {
                        foreach (PhysicalStockVerificationSerial _ser in _temp)
                        {

                            if (_ser.Auss_item == txtItemCode.Text && _ser.Auss_itm_stus == cmbStatus.SelectedValue.ToString())
                            {
                                PhysicalStockVerificationSerial _ledger = new PhysicalStockVerificationSerial();
                                _ledger.Auss_serial = _ser.Auss_serial;
                                _ledger.Auss_warranty = _ser.Auss_warranty;
                                _ledger.Auss_item = _ser.Auss_item;
                                _ledger.Auss_itm_stus = _ser.Auss_itm_stus;
                                _ledger.Auss_warranty = _ser.Auss_warranty;
                                _ledger.Auss_in_doc = _ser.Auss_in_doc;
                                _ledger.Auss_in_dt = _ser.Auss_in_dt;
                                _ledger.Auss_ser_id = _ser.Auss_ser_id;
                                _ledger.Auss_job = _ser.Auss_job;
                                _itemLedgerSerials.Add(_ledger);
                            }
                        }
                    }
                    else
                    {
                        foreach (PhysicalStockVerificationSerial _ser in _serailList)
                        {

                            if (_ser.Auss_item == txtItemCode.Text && _ser.Auss_itm_stus == cmbStatus.SelectedValue.ToString() && _ser.Auss_type != "P")
                            {
                                PhysicalStockVerificationSerial _ledger = new PhysicalStockVerificationSerial();
                                _ledger.Auss_serial = _ser.Auss_serial;
                                _ledger.Auss_warranty = _ser.Auss_warranty;
                                _ledger.Auss_item = _ser.Auss_item;
                                _ledger.Auss_itm_stus = _ser.Auss_itm_stus;
                                _ledger.Auss_warranty = _ser.Auss_warranty;
                                _ledger.Auss_in_doc = _ser.Auss_in_doc;
                                _ledger.Auss_in_dt = _ser.Auss_in_dt;
                                _ledger.Auss_ser_id = _ser.Auss_ser_id;
                                _ledger.Auss_job = _ser.Auss_job;
                                _itemLedgerSerials.Add(_ledger);
                            }
                        }
                    }

                }
                if (_itemLedgerSerials != null)
                {
                    gvItemSerials.AutoGenerateColumns = false;
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _itemLedgerSerials;
                    gvItemSerials.DataSource = _source;
                }

                _serid = 0;
                pnlMain.Enabled = false;
                popupLedger.Visible = true;
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

        private void btnClsPopUpLedger_Click(object sender, EventArgs e)
        {
            pnlMain.Enabled = true;
            popupLedger.Visible = false;

            txtLedgerCount.Text = _itemLedgerSerials.Count.ToString();

            _itemLedgerSerials.ForEach(x=>x.Auss_type="L");
        }

        private void btnAddLedgerSerial_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtLedgerSerial.Text == "") {
                    return;
                }

                //add serial to list
                ReptPickSerials _ser = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "", txtItemCode.Text, txtLedgerSerial.Text);

                List<PhysicalStockVerificationSerial> _temp = (from _res in _itemLedgerSerials
                                                               where _res.Auss_item == txtItemCode.Text && _res.Auss_itm_stus == cmbStatus.SelectedValue.ToString() && _res.Auss_ser_id == _serid
                                                               select _res).ToList<PhysicalStockVerificationSerial>();
                if (_temp == null || _temp.Count <= 0)
                {
                    PhysicalStockVerificationSerial _serial = new PhysicalStockVerificationSerial();
                    _serial.Auss_item = txtItemCode.Text;
                    _serial.Auss_serial = txtLedgerSerial.Text;
                    _serial.Auss_warranty = txtLedgerWarranty.Text;
                    _serial.Auss_item = txtItemCode.Text;
                    _serial.Auss_itm_stus = cmbStatus.SelectedValue.ToString();
                    if (_ser != null && _ser.Tus_warr_no!=null)
                    {
                        _serial.Auss_warranty = _ser.Tus_warr_no;
                        _serial.Auss_in_doc = _ser.Tus_doc_no;
                        _serial.Auss_in_dt = _ser.Tus_doc_dt;
                    }
                    _serial.Auss_ser_id = ++ledId;
                    _serial.Auss_type = "L";

                    _itemLedgerSerials.Add(_serial);
                }
                else {
                    _temp[0].Auss_serial = txtLedgerSerial.Text;
                    _temp[0].Auss_warranty = txtLedgerWarranty.Text;
                    _temp[0].Auss_item = txtItemCode.Text;
                    _temp[0].Auss_itm_stus = cmbStatus.SelectedValue.ToString();
                    if (_ser != null && _ser.Tus_warr_no!=null)
                    {
                        _temp[0].Auss_warranty = _ser.Tus_warr_no;
                        _temp[0].Auss_in_doc = _ser.Tus_doc_no;
                        _temp[0].Auss_in_dt = _ser.Tus_doc_dt;
                    }
                    _temp[0].Auss_type = "L";
                }
                _itemLedgerSerials = _itemLedgerSerials.GroupBy(x => x.Auss_ser_id).Select(x => x.First()).ToList<PhysicalStockVerificationSerial>();
                _itemPhysicalSerials = _itemPhysicalSerials.GroupBy(x => x.Auss_ser_id).Select(x => x.First()).ToList<PhysicalStockVerificationSerial>();
               
                BindingSource _source = new BindingSource();
                _source.DataSource = _itemLedgerSerials;
                gvLedgerSerials.AutoGenerateColumns = false;
                gvLedgerSerials.DataSource = _source;

                BindingSource _source1 = new BindingSource();
                _source1.DataSource = _itemLedgerSerials;
                gvItemSerials.AutoGenerateColumns = false;
                gvItemSerials.DataSource = _source1;

                txtLedgerSerial.Text = "";
                txtLedgerWarranty.Text="";
                _serid = 0;
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

        private void gvItemSerials_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (e.ColumnIndex == 0)
                    {
                        PhysicalStockVerificationSerial _serial = _itemLedgerSerials[e.RowIndex];
                        if (_serial != null)
                        {
                            txtLedgerSerial.Text = _serial.Auss_serial;
                            txtLedgerWarranty.Text = _serial.Auss_warranty;
                            _serid = _serial.Auss_ser_id;

                        }
                    }
                    if (e.ColumnIndex == 1)
                    {
                        if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            _itemLedgerSerials.RemoveAt(e.RowIndex);
                            gvItemSerials.AutoGenerateColumns = false;
                            BindingSource _source = new BindingSource();
                            _source.DataSource = _itemLedgerSerials;
                            gvItemSerials.DataSource = _source;
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //if item in list



            //not in list
            //01.Find item from item list
            //02.Update ledger and physical balance
            //03.Update serials
            List<PhysicalStockVerificationItem> _itm = (from _tem in _itemList
                                                        where _tem.Ausi_itm == txtItemCode.Text && _tem.Ausi_stus == cmbStatus.SelectedValue.ToString()
                                                        select _tem).ToList<PhysicalStockVerificationItem>();
            if (_itm != null && _itm.Count > 0)
            {
                int legCou = 0;
                int phyCou = 0;
                _itemLedgerSerials = _itemLedgerSerials.GroupBy(x => x.Auss_ser_id).Select(x => x.First()).ToList<PhysicalStockVerificationSerial>();
                _itemPhysicalSerials = _itemPhysicalSerials.GroupBy(x => x.Auss_ser_id).Select(x => x.First()).ToList<PhysicalStockVerificationSerial>();
                //update ledger serials
                foreach (PhysicalStockVerificationSerial _ser in _itemLedgerSerials)
                {
                    //chek for existence
                    //List<PhysicalStockVerificationSerial> _tem = (from _t in _serailList
                    //                                              where _t.Auss_item == txtItemCode.Text && _t.Auss_itm_stus == cmbStatus.SelectedValue.ToString() && _t.Auss_type == "L" && _t.Auss_serial==_ser.Auss_serial
                    //                                              select _t).ToList<PhysicalStockVerificationSerial>();
                    //if (_tem == null || _tem.Count<=0)
                    //{
                    if (_ser.Auss_type == "L")
                    {
                        PhysicalStockVerificationSerial _serial = new PhysicalStockVerificationSerial();
                        _serial = _ser;
                        _serial.Auss_job = _itm[0].Ausi_job;
                        _serial.Auss_seq = _itm[0].Ausi_seq;
                        //_ser.Auss_job = _itm[0].Ausi_job;
                        string _err = "";
                        legCou++;
                        int _result = CHNLSVC.Inventory.SavePhysicalStockVerificationSerial(_serial, out _err);
                        if (_result == -1)
                        {
                            MessageBox.Show("Error occured while processing\n" + _err);
                        }
                    }
                    //save ledger serials
                    //_serailList.Add(_serial);
                    //}
                }
                phyCou = _itemPhysicalSerials.Where(x => x.Auss_type == "P").ToList<PhysicalStockVerificationSerial>().Count;
                //update physical serial
                foreach (PhysicalStockVerificationSerial _ser in _itemPhysicalSerials)
                {
                    //check for existence
                    //chek for existence
                    //List<PhysicalStockVerificationSerial> _tem = (from _t in _serailList
                    //                                              where _t.Auss_item == txtItemCode.Text && _t.Auss_itm_stus == cmbStatus.SelectedValue.ToString() && _t.Auss_type == "P" && _t.Auss_serial==_ser.Auss_serial
                    //                                              select _t).ToList<PhysicalStockVerificationSerial>();
                    //if (_tem == null || _tem.Count <= 0)
                    //{
                    if (_ser.Auss_type == "P")
                    {
                        if (!string.IsNullOrEmpty(_ser.Auss_ref_stus))
                        {
                            List<AuditStatus> _all = CHNLSVC.Inventory.GetAllAuditStstus(BaseCls.GlbUserComCode);
                            List<AuditStatus> _stus = (from _res in _all
                                                       where _res.Auss_code == _ser.Auss_ref_stus
                                                       select _res).ToList<AuditStatus>();
                            if (_stus != null && _stus.Count > 0)
                            {
                                if (_stus[0].Auss_direction == 0)
                                {
                                    //if(phyCou>01)
                                    phyCou = phyCou - 1;
                                }
                                //else if (_stus[0].Auss_direction == 1)
                                //{
                                //    phyCou = phyCou + 1;
                                //}
                                //else if (_stus[0].Auss_direction == 2)
                                //{
                                //    phyCou = phyCou + 1;
                                //}
                            }
                        }
                        //else {
                        //    phyCou = phyCou + 1;
                        //}
                        PhysicalStockVerificationSerial _serial = new PhysicalStockVerificationSerial();
                        _serial = _ser;
                        _serial.Auss_job = _itm[0].Ausi_job;
                        _serial.Auss_seq = _itm[0].Ausi_seq;
                        //save physical serials
                        string _err = "";

                        int _result = CHNLSVC.Inventory.SavePhysicalStockVerificationSerial(_serial, out _err);
                        if (_result == -1)
                        {
                            MessageBox.Show("Error occurred while processing\n" + _err);
                        }
                    }
                    //_serailList.Add(_serial);
                    //}
                }

                if (_itm != null)
                {
                    //update item
                    _itm[0].Ausi_physical_qty = phyCou;
                    _itm[0].Ausi_ledger_qty = _itemLedgerSerials.Count;

                    grItems.AutoGenerateColumns = false;
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _itemList;
                    grItems.DataSource = _source;

                }
                LoadLedgerAndPhysicalSerials(txtItemCode.Text, cmbStatus.SelectedValue.ToString());
                txtLedgerCount.Text = "0";
                txtPhyCount.Text = "";

                //update itm
                CHNLSVC.Inventory.PhysicalstkUpdateItemCount(txtJobNo.Text.Trim().ToUpper(), txtItemCode.Text, cmbStatus.SelectedValue.ToString(), 0, legCou);
                CHNLSVC.Inventory.PhysicalstkUpdateItemCount(txtJobNo.Text.Trim().ToUpper(), txtItemCode.Text, cmbStatus.SelectedValue.ToString(), 1, phyCou);
            }
            else {
                int legCou = 0;
                PhysicalStockVerificationItem _item = new PhysicalStockVerificationItem();
                _item.Ausi_itm = txtItemCode.Text.Trim();
                _item.Ausi_job = _itemList[0].Ausi_job;
                _item.Ausi_seq = _itemList[0].Ausi_seq;
                _item.Ausi_stus = cmbStatus.SelectedValue.ToString();
                _item.Ausi_ledger_qty = 0;
                _item.Ausi_physical_qty = 0;
                _item.Ausi_cre_by = BaseCls.GlbUserID;
                _item.Ausi_desc = txtDescription.Text;
                _item.Ausi_cre_dt = DateTime.Now;

                _itemList.Add(_item);
                CHNLSVC.Inventory.SavePhysicalStockVerificationItem(_item);
                //add item to list
                foreach (PhysicalStockVerificationSerial _ser in _itemLedgerSerials)
                {
                    if (_ser.Auss_type == "L")
                    {
                        PhysicalStockVerificationSerial _serial = new PhysicalStockVerificationSerial();
                        _serial = _ser;
                        _serial.Auss_job = _itemList[0].Ausi_job;
                        _serial.Auss_seq = _itemList[0].Ausi_seq;
                        //_ser.Auss_job = _itm[0].Ausi_job;
                        string _err = "";
                        legCou++;
                        int _result = CHNLSVC.Inventory.SavePhysicalStockVerificationSerial(_serial, out _err);
                        if (_result == -1)
                        {
                            MessageBox.Show("Error occured while processing\n" + _err);
                        }
                    }
                    //save ledger serials
                    //_serailList.Add(_serial);
                    //}
                }
                int phyCou = _itemPhysicalSerials.Where(x => x.Auss_type == "P").ToList<PhysicalStockVerificationSerial>().Count;
                //update physical serial
                foreach (PhysicalStockVerificationSerial _ser in _itemPhysicalSerials)
                {
                    //check for existence
                    //chek for existence
                    //List<PhysicalStockVerificationSerial> _tem = (from _t in _serailList
                    //                                              where _t.Auss_item == txtItemCode.Text && _t.Auss_itm_stus == cmbStatus.SelectedValue.ToString() && _t.Auss_type == "P" && _t.Auss_serial==_ser.Auss_serial
                    //                                              select _t).ToList<PhysicalStockVerificationSerial>();
                    //if (_tem == null || _tem.Count <= 0)
                    //{
                    if (_ser.Auss_type == "P")
                    {
                        if (!string.IsNullOrEmpty(_ser.Auss_ref_stus))
                        {
                            List<AuditStatus> _all = CHNLSVC.Inventory.GetAllAuditStstus(BaseCls.GlbUserComCode);
                            List<AuditStatus> _stus = (from _res in _all
                                                       where _res.Auss_code == _ser.Auss_ref_stus
                                                       select _res).ToList<AuditStatus>();
                            if (_stus != null && _stus.Count > 0)
                            {
                                if (_stus[0].Auss_direction == 0)
                                {
                                   
                                    phyCou = phyCou - 1;
                                }
                                
                            }
                        }

                        PhysicalStockVerificationSerial _serial = new PhysicalStockVerificationSerial();
                        _serial = _ser;
                        _serial.Auss_job = _itemList[0].Ausi_job;
                        _serial.Auss_seq = _itemList[0].Ausi_seq;
                        //save physical serials
                        string _err = "";

                        int _result = CHNLSVC.Inventory.SavePhysicalStockVerificationSerial(_serial, out _err);
                        if (_result == -1)
                        {
                            MessageBox.Show("Error occurred while processing\n" + _err);
                        }
                    }
                    
                }
                List<PhysicalStockVerificationItem> _itm1 = (from _tem in _itemList
                                                            where _tem.Ausi_itm == txtItemCode.Text && _tem.Ausi_stus == cmbStatus.SelectedValue.ToString()
                                                            select _tem).ToList<PhysicalStockVerificationItem>();


                if (_itm1 != null)
                {
                    //update item
                    _itm1[0].Ausi_physical_qty = phyCou;
                    _itm1[0].Ausi_ledger_qty = _itemLedgerSerials.Count;

                    grItems.AutoGenerateColumns = false;
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _itemList;
                    grItems.DataSource = _source;

                }

                //update itm
                CHNLSVC.Inventory.PhysicalstkUpdateItemCount(txtJobNo.Text.Trim().ToUpper(), txtItemCode.Text, cmbStatus.SelectedValue.ToString(), 0, legCou);
                CHNLSVC.Inventory.PhysicalstkUpdateItemCount(txtJobNo.Text.Trim().ToUpper(), txtItemCode.Text, cmbStatus.SelectedValue.ToString(), 1, phyCou);
            
            }
        }

        private void gvPhysiclSerial_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (e.ColumnIndex == 0)
                    {
                        PhysicalStockVerificationSerial _serial = _itemPhysicalSerials[e.RowIndex];
                        if (_serial != null)
                        {
                            txtPhysicalSerial.Text = _serial.Auss_serial;
                            txtPhysicalWarranty.Text = _serial.Auss_warranty;
                            _serid = _serial.Auss_ser_id;
                        }
                    }
                    if (e.RowIndex == 6) { 
                    
                        //if item in serial list can not delete 
                        PhysicalStockVerificationSerial _serial = _itemPhysicalSerials[e.RowIndex];
                        List<PhysicalStockVerificationSerial> _temp = (from _res in _serailList
                                                                       where _res.Auss_ser_id == _serial.Auss_ser_id
                                                                       select _res).ToList<PhysicalStockVerificationSerial>();
                        if (_temp != null && _temp.Count > 0)
                        {

                        }
                        //else can
                        else {
                            if (MessageBox.Show("Are you want to delete?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) {
                                return;
                            }
                            _itemPhysicalSerials.RemoveAt(e.RowIndex);

                            gvPhysiclSerial.AutoGenerateColumns = false;
                            BindingSource _source = new BindingSource();
                            _source.DataSource = _itemPhysicalSerials;
                            gvPhysiclSerial.DataSource = _source;

                            gvPhySerials.AutoGenerateColumns = false;
                            BindingSource _source1 = new BindingSource();
                            _source1.DataSource = _itemPhysicalSerials;
                            gvPhySerials.DataSource = _source1;
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

        private void btnAddPhysicalSerial_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPhysicalSerial.Text == "") {
                    return;
                }

                //add serial to list
                ReptPickSerials _ser = CHNLSVC.Inventory.Get_all_details_on_serial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "", txtItemCode.Text, txtLedgerSerial.Text);

                //add serial to list
                List<PhysicalStockVerificationSerial> _temp = (from _res in _itemPhysicalSerials
                                                               where _res.Auss_item == txtItemCode.Text && _res.Auss_itm_stus == cmbStatus.SelectedValue.ToString()  && (_res.Auss_ser_id == _serid)
                                                               select _res).ToList<PhysicalStockVerificationSerial>();
                if (_temp == null || _temp.Count <= 0)
                {
                    PhysicalStockVerificationSerial _serial = new PhysicalStockVerificationSerial();
                    _serial.Auss_item = txtItemCode.Text;
                    _serial.Auss_serial = txtPhysicalSerial.Text;
                    _serial.Auss_warranty = txtPhysicalWarranty.Text;
                    _serial.Auss_item = txtItemCode.Text;
                    _serial.Auss_itm_stus = cmbStatus.SelectedValue.ToString();
                    if (_ser != null && _ser.Tus_warr_no!=null)
                    {
                        //_serial.Auss_warranty = _ser.Tus_warr_no;
                        _serial.Auss_in_doc = _ser.Tus_doc_no;
                        _serial.Auss_in_dt = _ser.Tus_doc_dt;
                    }
                    _serial.Auss_ser_id = ++phyId;
                    _serial.Auss_type = "P";
                    _serial.Auss_ref_stus = cmbRefStus.SelectedValue != null ? cmbRefStus.SelectedValue.ToString() : "";
                    _serial.Auss_rpt_type = cmbRepType.SelectedValue != null ? cmbRepType.SelectedValue.ToString() : "";
                    _itemPhysicalSerials.Add(_serial);
                }

                else
                {
                    _temp[0].Auss_serial = txtPhysicalSerial.Text;
                    _temp[0].Auss_warranty = txtPhysicalWarranty.Text;
                    _temp[0].Auss_ref_stus = cmbRefStus.SelectedValue != null ? cmbRefStus.SelectedValue.ToString() : "";
                    _temp[0].Auss_rpt_type = cmbRepType.SelectedValue != null ? cmbRepType.SelectedValue.ToString() : "";
                    if (_ser != null && _ser.Tus_warr_no != null)
                    {
                        _temp[0].Auss_warranty = _ser.Tus_warr_no;
                        _temp[0].Auss_in_doc = _ser.Tus_doc_no;
                        _temp[0].Auss_in_dt = _ser.Tus_doc_dt;
                    }
                    _temp[0].Auss_rmk = txtPhyRmk.Text;
                    _temp[0].Auss_type = "P";

                    List<AuditRemarkValue> _rmkVal = (from _res in _AuditRmkList
                                                      where _res.Ausv_ser_id == _temp[0].Auss_ser_id
                                                      select _res).ToList<AuditRemarkValue>();

                    if (_rmkVal != null && _rmkVal.Count > 0)
                    {
                        foreach (AuditRemarkValue _rm in _rmkVal)
                        {
                            _AuditRmkList.Remove(_rm);

                            //need to delete remark from database
                            string _error;
                            int result = CHNLSVC.Inventory.PhyscilStockRemoveRmk(_rm.Ausv_job, _rm.Ausv_ser_id, _rm.Ausv_line,out _error);
                            if (result == -1) {
                                MessageBox.Show("Error occured while processing\n" + _error+"\n"+"SERILAL ID - "+_rm.Ausv_ser_id+"\nLINE - "+_rm.Ausv_line);
                                return;
                            }
                        }
                    }
                }
                _itemLedgerSerials = _itemLedgerSerials.GroupBy(x => x.Auss_ser_id).Select(x => x.First()).ToList<PhysicalStockVerificationSerial>();
                _itemPhysicalSerials = _itemPhysicalSerials.GroupBy(x => x.Auss_ser_id).Select(x => x.First()).ToList<PhysicalStockVerificationSerial>();
                gvPhysiclSerial.AutoGenerateColumns = false;
                BindingSource _source = new BindingSource();
                _source.DataSource = _itemPhysicalSerials;
                gvPhysiclSerial.DataSource = _source;

                gvPhySerials.AutoGenerateColumns = false;
                BindingSource _source1 = new BindingSource();
                _source1.DataSource = _itemPhysicalSerials;
                gvPhySerials.DataSource = _source1;

                


                txtPhysicalSerial.Text = "";
                txtPhysicalWarranty.Text = "";
                _serid = 0;
                //update count
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

        private void btnAddPhysical_Click(object sender, EventArgs e)
        {
            try
            {

                List<PhysicalStockVerificationSerial> _temp1 = (from _res in _itemPhysicalSerials
                                                                where _res.Auss_type == "P" && _res.Auss_item == txtItemCode.Text && _res.Auss_itm_stus == cmbStatus.SelectedValue.ToString()
                                                                select _res).ToList<PhysicalStockVerificationSerial>();

                if (_temp1 != null && _temp1.Count > 0)
                {

                }
                else
                {
                    _itemPhysicalSerials = new List<PhysicalStockVerificationSerial>();

                    List<PhysicalStockVerificationSerial> _temp = (from _res in _serailList
                                                                   where _res.Auss_type == "P" && _res.Auss_item == txtItemCode.Text && _res.Auss_itm_stus == cmbStatus.SelectedValue.ToString()
                                                                   select _res).ToList<PhysicalStockVerificationSerial>();
                    _temp = _temp.GroupBy(x => x.Auss_ser_id).Select(x => x.First()).ToList<PhysicalStockVerificationSerial>();

                    if (_temp != null && _temp.Count > 0)
                    {
                        foreach (PhysicalStockVerificationSerial _ser in _temp)
                        {

                            if (_ser.Auss_item == txtItemCode.Text && _ser.Auss_itm_stus == cmbStatus.SelectedValue.ToString())
                            {
                                PhysicalStockVerificationSerial _phy = new PhysicalStockVerificationSerial();
                                _phy.Auss_serial = _ser.Auss_serial;
                                _phy.Auss_warranty = _ser.Auss_warranty;
                                _phy.Auss_item = _ser.Auss_item;
                                _phy.Auss_itm_stus = _ser.Auss_itm_stus;
                                _phy.Auss_warranty = _ser.Auss_warranty;
                                _phy.Auss_in_doc = _ser.Auss_in_doc;
                                _phy.Auss_in_dt = _ser.Auss_in_dt;
                                _phy.Auss_ref_stus = _ser.Auss_ref_stus;
                                _phy.Auss_rpt_type = _ser.Auss_rpt_type;
                                _phy.Auss_ser_id = _ser.Auss_ser_id;
                                _phy.Auss_job = _ser.Auss_job;
                                _itemPhysicalSerials.Add(_phy);
                            }
                        }
                    }
                    else
                    {
                        foreach (PhysicalStockVerificationSerial _ser in _serailList)
                        {

                            if (_ser.Auss_item == txtItemCode.Text && _ser.Auss_itm_stus == cmbStatus.SelectedValue.ToString() && _ser.Auss_type != "L")
                            {
                                PhysicalStockVerificationSerial _phy = new PhysicalStockVerificationSerial();
                                _phy.Auss_serial = _ser.Auss_serial;
                                _phy.Auss_warranty = _ser.Auss_warranty;
                                _phy.Auss_item = _ser.Auss_item;
                                _phy.Auss_itm_stus = _ser.Auss_itm_stus;
                                _phy.Auss_warranty = _ser.Auss_warranty;
                                _phy.Auss_in_doc = _ser.Auss_in_doc;
                                _phy.Auss_ref_stus = _ser.Auss_ref_stus;
                                _phy.Auss_rpt_type = _ser.Auss_rpt_type;
                                _phy.Auss_in_dt = _ser.Auss_in_dt;
                                _phy.Auss_ser_id = _ser.Auss_ser_id;
                                _phy.Auss_job = _ser.Auss_job;
                                _itemPhysicalSerials.Add(_phy);
                            }
                        }
                    }

                }
                if (_itemPhysicalSerials != null)
                {
                    gvPhysiclSerial.AutoGenerateColumns = false;
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _itemPhysicalSerials;
                    gvPhysiclSerial.DataSource = _source;
                }
                LoadRefStus();
                cmbRefStus_SelectedIndexChanged(null, null);
                _serid = 0;
                pnlMain.Enabled = false;
                popupPhysical.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

            /*
            try
            {
                _itemPhysicalSerials = new List<PhysicalStockVerificationSerial>();
                //load item serials
                //_itemPhysicalSerials = (from _itm in _serailList
                //                        where _itm.Auss_item == txtItemCode.Text && _itm.Auss_itm_stus == cmbStatus.SelectedValue.ToString()
                //                        select _itm).ToList<PhysicalStockVerificationSerial>();
                foreach (PhysicalStockVerificationSerial _ser in _serailList)
                {
                    if (_ser.Auss_item == txtItemCode.Text && _ser.Auss_itm_stus == cmbStatus.SelectedValue.ToString()  && _ser.Auss_type != "L")
                    {
                        PhysicalStockVerificationSerial _physical = new PhysicalStockVerificationSerial();
                        _physical.Auss_serial = _ser.Auss_serial;
                        _physical.Auss_warranty = _ser.Auss_warranty;
                        _physical.Auss_item = _ser.Auss_item;
                        _physical.Auss_itm_stus = _ser.Auss_itm_stus;
                        _physical.Auss_warranty = _ser.Auss_warranty;
                        _physical.Auss_in_doc = _ser.Auss_in_doc;
                        _physical.Auss_in_dt = _ser.Auss_in_dt;
                        _physical.Auss_ser_id = _ser.Auss_ser_id;
                        _physical.Auss_job = _ser.Auss_job;
                        _itemPhysicalSerials.Add(_physical);
                    }
                }

                if (_itemPhysicalSerials != null)
                {
                    gvPhysiclSerial.AutoGenerateColumns = false;
                    gvItemSerials.AutoGenerateColumns = false;
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _itemPhysicalSerials;
                    gvPhysiclSerial.DataSource = _source;
               
                }
                _serid = 0;
                LoadRefStus();

                pnlMain.Enabled = false;
                popupPhysical.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
             */
        }

        private void LoadRefStus()
        {
            try
            {
                List<AuditStatus> _stusList = CHNLSVC.Inventory.GetAllAuditStstus(BaseCls.GlbUserComCode);
                cmbRefStus.DataSource = _stusList;
                cmbRefStus.DisplayMember = "AUSS_DESC";
                cmbRefStus.ValueMember = "AUSS_CODE";
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

        private void btnClsPopUpPhysical_Click(object sender, EventArgs e)
        {

            int phyCou = _itemPhysicalSerials.Count;
            foreach (PhysicalStockVerificationSerial _ser in _itemPhysicalSerials) {
                if (!string.IsNullOrEmpty(_ser.Auss_ref_stus))
                {
                    List<AuditStatus> _all = CHNLSVC.Inventory.GetAllAuditStstus(BaseCls.GlbUserComCode);
                    List<AuditStatus> _stus = (from _res in _all
                                               where _res.Auss_code == _ser.Auss_ref_stus
                                               select _res).ToList<AuditStatus>();
                    if (_stus != null && _stus.Count > 0)
                    {
                        if (_stus[0].Auss_direction == 0)
                        {
                           // if (phyCou>0)
                            phyCou = phyCou - 1;
                        }
                        //else if (_stus[0].Auss_direction == 1)
                        //{
                        //    phyCou = phyCou + 1;
                        //}
                        //else if (_stus[0].Auss_direction == 2)
                        //{
                        //    phyCou = phyCou + 1;
                        //}

                    }
                }
                //else {
                //    phyCou = phyCou + 1;
                //}
            }

            txtPhyCount.Text = phyCou.ToString();
            pnlMain.Enabled = true;
            popupPhysical.Visible = false;
            _itemPhysicalSerials.ForEach(x=>x.Auss_type="P");
        }

        private void LoadReportTypes(bool isAll) {
            try
            {
                if (!isAll)
                {

                    List<AuditReportStatus> _stusList = CHNLSVC.Inventory.GetAllAuditReportStstus(BaseCls.GlbUserComCode, cmbRefStus.SelectedValue != null ? cmbRefStus.SelectedValue.ToString() : "");
                    cmbRepType.DataSource = _stusList;
                    cmbRepType.DisplayMember = "AURS_CODE";
                    cmbRepType.ValueMember = "AURS_CODE";
                }
                else {

                    List<AuditReportStatus> _stusList = CHNLSVC.Inventory.GetAllAuditReportStstus(BaseCls.GlbUserComCode, "%");
                    cmbRmk.DataSource = _stusList;
                    cmbRmk.DisplayMember = "aurs_desc";
                    cmbRmk.ValueMember = "AURS_CODE";
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

        private void cmbRefStus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRefStus.SelectedValue != null)
                LoadReportTypes(false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Please fill audit remarks before Confirming Process\nAudit remark fill complete?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) {
                    return;
                }
                if (!chkDBLedger.Checked) {
                    MessageBox.Show("Please select Identical Balance(DB -Ledger)","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                if (!chkDBPhysical.Checked) {
                    MessageBox.Show("Please select Identical Balance(DB-Physical)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_itemList == null || _itemList.Count <= 0)
                {
                    MessageBox.Show("No items to save", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                List<PhysicalStockVerificationSerial> _processSerialList = new List<PhysicalStockVerificationSerial>();
                if (chkDBLedger.Checked)
                {
                    //update serials
                    foreach (PhysicalStockVerificationItem _itm in _itemList)
                    {

                        List<PhysicalStockVerificationSerial> _tem = (from _res in _serailList
                                                                      where _res.Auss_item == _itm.Ausi_itm && _res.Auss_itm_stus == _itm.Ausi_stus && _res.Auss_type == "L"
                                                                      select _res).ToList<PhysicalStockVerificationSerial>();

                        if (_itm.Ausi_ledger_qty == 0 && (_tem == null || _tem.Count <= 0))
                        {
                            List<PhysicalStockVerificationSerial> _temSer = (from _res in _serailList
                                                                             where _res.Auss_item == _itm.Ausi_itm && _res.Auss_itm_stus == _itm.Ausi_stus && (_res.Auss_type != "L" || _res.Auss_type != "P")
                                                                             select _res).ToList<PhysicalStockVerificationSerial>();
                            if (_temSer != null && _temSer.Count > 0)
                            {
                                foreach (PhysicalStockVerificationSerial _ser in _temSer)
                                {
                                    PhysicalStockVerificationSerial _serial = new PhysicalStockVerificationSerial();
                                    _serial.Auss_in_doc = _ser.Auss_in_doc;
                                    _serial.Auss_in_dt = _ser.Auss_in_dt;
                                    _serial.Auss_item = _ser.Auss_item;
                                    _serial.Auss_itm_stus = _ser.Auss_itm_stus;
                                    _serial.Auss_job = _ser.Auss_job;
                                    _serial.Auss_ref_stus = _ser.Auss_ref_stus;
                                    _serial.Auss_rpt_type = _ser.Auss_rpt_type;
                                    _serial.Auss_seq = _ser.Auss_seq;
                                    _serial.Auss_ser_id = _ser.Auss_ser_id;
                                    _serial.Auss_serial = _ser.Auss_serial;
                                    _serial.Auss_type = _ser.Auss_type;
                                    _serial.Auss_warranty = _ser.Auss_warranty;


                                    _serial.Auss_type = "L";
                                    _processSerialList.Add(_serial);
                                }
                            }

                           

                        }
                        //update item ledger balance
                        _itemList.Where(x => x.Ausi_ledger_qty == 0).ToList<PhysicalStockVerificationItem>().ForEach(x => x.Ausi_ledger_qty = x.Ausi_db_qty);
                    }

                   



                }
                int cou = _serailList.Where(x => x.Auss_type == "L").ToList<PhysicalStockVerificationSerial>().Count;
                if (chkDBPhysical.Checked)
                {
                    //update serials
                    foreach (PhysicalStockVerificationItem _itm in _itemList)
                    {
                        List<PhysicalStockVerificationSerial> _tem=(from _res in _serailList
                                                                             where _res.Auss_item == _itm.Ausi_itm && _res.Auss_itm_stus == _itm.Ausi_stus &&  _res.Auss_type == "P" select _res).ToList<PhysicalStockVerificationSerial>();

                        if (_itm.Ausi_physical_qty == 0 && (_tem==null || _tem.Count<=0))
                        {
                            List<PhysicalStockVerificationSerial> _temSer = (from _res in _serailList
                                                                             where _res.Auss_item == _itm.Ausi_itm && _res.Auss_itm_stus == _itm.Ausi_stus && (_res.Auss_type != "L" || _res.Auss_type != "P")
                                                                             select _res).ToList<PhysicalStockVerificationSerial>();
                            if (_temSer != null && _temSer.Count > 0)
                            {
                                foreach (PhysicalStockVerificationSerial _ser in _temSer)
                                {
                                    PhysicalStockVerificationSerial _serial = new PhysicalStockVerificationSerial();
                                    _serial.Auss_in_doc = _ser.Auss_in_doc;
                                    _serial.Auss_in_dt = _ser.Auss_in_dt;
                                    _serial.Auss_item = _ser.Auss_item;
                                    _serial.Auss_itm_stus = _ser.Auss_itm_stus;
                                    _serial.Auss_job = _ser.Auss_job;
                                    _serial.Auss_ref_stus = _ser.Auss_ref_stus;
                                    _serial.Auss_rpt_type = _ser.Auss_rpt_type;
                                    _serial.Auss_seq = _ser.Auss_seq;
                                    _serial.Auss_ser_id = _ser.Auss_ser_id;
                                    _serial.Auss_serial = _ser.Auss_serial;
                                    _serial.Auss_type = _ser.Auss_type;
                                    _serial.Auss_warranty = _ser.Auss_warranty;
                                    _serial.Auss_type = "P";
                                    _processSerialList.Add(_serial);
                                }
                            }
                            //update item ledger balance
                            _itemList.Where(x => x.Ausi_itm == _itm.Ausi_itm && x.Ausi_stus==_itm.Ausi_stus).ToList<PhysicalStockVerificationItem>().ForEach(x => x.Ausi_physical_qty = x.Ausi_db_qty);

                        }
                    }
                    

                }
                int cou1 = _serailList.Where(x => x.Auss_type == "P").ToList<PhysicalStockVerificationSerial>().Count;
                PhsicalStockVerificationMain _main = new PhsicalStockVerificationMain();
                _main.Ausm_cre_by = BaseCls.GlbUserID;
                _main.Ausm_cre_dt = DateTime.Now;
                _main.Ausm_dt = dtDate.Value.Date;
                _main.Ausm_loc = BaseCls.GlbUserDefLoca;
                _main.Ausm_stus = true;


                //list to datatable
                DataTable _dtSerials = ConvertToDataTable( _processSerialList);
                _dtSerials.TableName = "dt";

                string _err = "";
                
                int _result = CHNLSVC.Inventory.SavePhysicalStockVerification(_main, _itemList, _dtSerials, out _err, _AuditRmkList);
                if (_result == -1)
                {
                    MessageBox.Show("Error Occurred while processing" + _err, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear_Click(null, null);
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

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            PhysicalStockVerification formnew = new PhysicalStockVerification();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

        private void btnRmks_Click(object sender, EventArgs e)
        {
            LoadReportTypes(true);
            cmbRmk.SelectedIndex = -1;
            List<PhysicalStockVerificationSerial> _temp = (from _res in _serailList
                                                           where _res.Auss_type == "P" && _res.Auss_rpt_type!=""
                                                           select _res).ToList<PhysicalStockVerificationSerial>();
            _temp = _temp.OrderBy(X => X.Auss_item).ToList<PhysicalStockVerificationSerial>();
            _temp = _temp.GroupBy(x => x.Auss_ser_id).Select(x => x.Last()).ToList<PhysicalStockVerificationSerial>();
            gvRmk.AutoGenerateColumns = false;
            BindingSource _source = new BindingSource();
            _source.DataSource = _temp;
            gvRmk.DataSource = _source;


            pnlAuditRmk.Visible = true;
            pnlAuditRmk.Width = 748;
            pnlMain.Enabled = false;

        }

        private void cmbRmk_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbRmk.SelectedValue == null)
                    return;
                List<AuditRemark> _rmkList = CHNLSVC.Inventory.GeatAuditRemarks(BaseCls.GlbUserComCode, cmbRmk.SelectedValue.ToString());

                //load items with rmk
                //List<PhysicalStockVerificationSerial> _rmkItem = (from _res in _serailList
                //                                                where _res.Auss_rpt_type == cmbRmk.SelectedValue.ToString()
                //                                               select _res).ToList<PhysicalStockVerificationSerial>();
                //BindingSource _source = new BindingSource();
                //_source.DataSource = _rmkList;
                //gvRmk.DataSource = _source;

                lblRmk1.Visible = false;
                txtRmk1.Visible = false;

                lblRmk2.Visible = false;
                txtRmk2.Visible = false;

                lblRmk3.Visible = false;
                txtRmk3.Visible = false;

                lblRmk4.Visible = false;
                txtRmk4.Visible = false;

                lblRmk5.Visible = false;
                txtRmk5.Visible = false;

                lblRmk6.Visible = false;
                txtRmk6.Visible = false;

                lblRmk7.Visible = false;
                txtRmk7.Visible = false;

                lblRmk8.Visible = false;
                txtRmk8.Visible = false;

                lblRmk9.Visible = false;
                txtRmk9.Visible = false;

                lblRmk1.Visible = false;
                txtRmk1.Visible = false;

                lblRmk10.Visible = false;
                txtRmk10.Visible = false;

                txtRmk1.Text = "";
                txtRmk2.Text = "";
                txtRmk3.Text = "";
                txtRmk4.Text = "";
                txtRmk5.Text = "";
                txtRmk6.Text = "";
                txtRmk7.Text = "";
                txtRmk8.Text = "";
                txtRmk9.Text = "";
                txtRmk10.Text = "";

                if (_rmkList != null && _rmkList.Count > 0)
                {
                    foreach (AuditRemark _rmk in _rmkList)
                    {
                        if (_rmk.Ausr_line == 1)
                        {
                            lblRmk1.Visible = true;
                            lblRmk1.Text = _rmk.Ausr_rmk;
                            txtRmk1.Visible = true;
                        }

                        if (_rmk.Ausr_line == 1)
                        {
                            lblRmk1.Visible = true;
                            lblRmk1.Text = _rmk.Ausr_rmk;
                            txtRmk1.Visible = true;
                        }
                        if (_rmk.Ausr_line == 2)
                        {
                            lblRmk2.Visible = true;
                            lblRmk2.Text = _rmk.Ausr_rmk;
                            txtRmk2.Visible = true;
                        }
                        if (_rmk.Ausr_line == 3)
                        {
                            lblRmk3.Visible = true;
                            lblRmk3.Text = _rmk.Ausr_rmk;
                            txtRmk3.Visible = true;
                        }
                        if (_rmk.Ausr_line == 4)
                        {
                            lblRmk4.Visible = true;
                            lblRmk4.Text = _rmk.Ausr_rmk;
                            txtRmk4.Visible = true;
                        }
                        if (_rmk.Ausr_line == 5)
                        {
                            lblRmk5.Visible = true;
                            lblRmk5.Text = _rmk.Ausr_rmk;
                            txtRmk5.Visible = true;
                        }
                        if (_rmk.Ausr_line == 6)
                        {
                            lblRmk6.Visible = true;
                            lblRmk6.Text = _rmk.Ausr_rmk;
                            txtRmk6.Visible = true;
                        }
                        if (_rmk.Ausr_line == 7)
                        {
                            lblRmk7.Visible = true;
                            lblRmk7.Text = _rmk.Ausr_rmk;
                            txtRmk7.Visible = true;
                        }
                        if (_rmk.Ausr_line == 8)
                        {
                            lblRmk8.Visible = true;
                            lblRmk8.Text = _rmk.Ausr_rmk;
                            txtRmk8.Visible = true;
                        }
                        if (_rmk.Ausr_line == 9)
                        {
                            lblRmk9.Visible = true;
                            lblRmk9.Text = _rmk.Ausr_rmk;
                            txtRmk9.Visible = true;
                        }
                        if (_rmk.Ausr_line == 10)
                        {
                            lblRmk10.Visible = true;
                            lblRmk10.Text = _rmk.Ausr_rmk;
                            txtRmk10.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            { CHNLSVC.CloseChannel(); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void gvRmk_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    lblItem.Text = gvRmk.Rows[e.RowIndex].Cells[1].Value.ToString();
                    lblStus.Text = gvRmk.Rows[e.RowIndex].Cells[2].Value.ToString();
                    lblSerId.Text = gvRmk.Rows[e.RowIndex].Cells[7].Value.ToString();
                    lblSerial.Text = gvRmk.Rows[e.RowIndex].Cells[3].Value.ToString();
                    cmbRmk.SelectedValue = gvRmk.Rows[e.RowIndex].Cells[6].Value.ToString();

                    if (gvRmk.Rows[e.RowIndex].Cells[6].Value.ToString() == "NOTE 03")
                    {
                        btn_Exreq.Visible = true;
                    }

                    //load previous remarks
                    List<AuditRemarkValue> _rmk = (from _res in _AuditRmkList
                                                   where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text)
                                                   select _res).ToList<AuditRemarkValue>();

                    _rmk = _rmk.GroupBy(x => new { x.Ausv_ser_id, x.Ausv_line }).Select(x => x.Last()).ToList<AuditRemarkValue>();
                    txtRmk1.Text = "";
                    txtRmk2.Text = "";
                    txtRmk3.Text = "";
                    txtRmk4.Text = "";
                    txtRmk5.Text = "";
                    txtRmk6.Text = "";
                    txtRmk7.Text = "";
                    txtRmk8.Text = "";
                    txtRmk9.Text = "";
                    txtRmk10.Text = "";



                    gvRmkVal.AutoGenerateColumns = false;
                    BindingSource _sou = new BindingSource();
                    _sou.DataSource = _rmk;
                    gvRmkVal.DataSource = _sou;
                    if (_rmk != null && _rmk.Count > 0)
                    {
                        foreach (AuditRemarkValue _rmkVal in _rmk)
                        {

                            if (_rmkVal.Ausv_line == 1)
                            {
                                txtRmk1.Text = _rmkVal.Ausv_val;
                            }
                            if (_rmkVal.Ausv_line == 2)
                            {
                                txtRmk2.Text = _rmkVal.Ausv_val;
                            }
                            if (_rmkVal.Ausv_line == 3)
                            {
                                txtRmk3.Text = _rmkVal.Ausv_val;
                            }
                            if (_rmkVal.Ausv_line == 4)
                            {
                                txtRmk4.Text = _rmkVal.Ausv_val;
                            }
                            if (_rmkVal.Ausv_line == 5)
                            {
                                txtRmk5.Text = _rmkVal.Ausv_val;
                            }
                            if (_rmkVal.Ausv_line == 6)
                            {
                                txtRmk6.Text = _rmkVal.Ausv_val;
                            }
                            if (_rmkVal.Ausv_line == 7)
                            {
                                txtRmk7.Text = _rmkVal.Ausv_val;
                            }
                            if (_rmkVal.Ausv_line == 8)
                            {
                                txtRmk8.Text = _rmkVal.Ausv_val;
                            }
                            if (_rmkVal.Ausv_line == 9)
                            {
                                txtRmk9.Text = _rmkVal.Ausv_val;
                            }
                            if (_rmkVal.Ausv_line == 10)
                            {
                                txtRmk10.Text = _rmkVal.Ausv_val;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            { CHNLSVC.CloseChannel(); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnAddRmk_Click(object sender, EventArgs e)
        {
            try
            {
                List<AuditRemarkValue> _rmkVal = (from _res in _AuditRmkList
                                                  where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text)
                                                  select _res).ToList<AuditRemarkValue>();
                if (_rmkVal != null && _rmkVal.Count > 0)
                {
                    if (txtRmk1.Text != "")
                    {
                        List<AuditRemarkValue> _rmkVal1 = (from _res in _AuditRmkList
                                                           where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text) && _res.Ausv_line == 1
                                                           select _res).ToList<AuditRemarkValue>();
                        if (_rmkVal1 != null && _rmkVal1.Count > 0)
                        {
                            _rmkVal1[0].Ausv_itm = lblItem.Text;
                            _rmkVal1[0].Ausv_itm_stus = lblStus.Text;
                            _rmkVal1[0].Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _rmkVal1[0].Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _rmkVal1[0].Ausv_job = txtJobNo.Text;
                            _rmkVal1[0].Ausv_val = txtRmk1.Text;
                            _rmkVal1[0].Ausv_line = 1;
                            _rmkVal1[0].Ausv_cre_by = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_cre_dt = DateTime.Now;

                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmkVal[0]);
                        }
                    }
                    if (txtRmk2.Text != "")
                    {
                        List<AuditRemarkValue> _rmkVal1 = (from _res in _AuditRmkList
                                                           where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text) && _res.Ausv_line == 2
                                                           select _res).ToList<AuditRemarkValue>();
                        if (_rmkVal1 != null && _rmkVal1.Count > 0)
                        {
                            _rmkVal1[0].Ausv_itm = lblItem.Text;
                            _rmkVal1[0].Ausv_itm_stus = lblStus.Text;
                            _rmkVal1[0].Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _rmkVal1[0].Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _rmkVal1[0].Ausv_job = txtJobNo.Text;
                            _rmkVal1[0].Ausv_val = txtRmk2.Text;
                            _rmkVal1[0].Ausv_line = 2;
                            _rmkVal1[0].Ausv_cre_by = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_cre_dt = DateTime.Now;
                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmkVal1[0]);
                        }
                    }
                    if (txtRmk3.Text != "")
                    {
                        List<AuditRemarkValue> _rmkVal1 = (from _res in _AuditRmkList
                                                           where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text) && _res.Ausv_line == 3
                                                           select _res).ToList<AuditRemarkValue>();
                        if (_rmkVal1 != null && _rmkVal1.Count > 0)
                        {
                            _rmkVal1[0].Ausv_itm = lblItem.Text;
                            _rmkVal1[0].Ausv_itm_stus = lblStus.Text;
                            _rmkVal1[0].Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _rmkVal1[0].Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _rmkVal1[0].Ausv_job = txtJobNo.Text;
                            _rmkVal1[0].Ausv_val = txtRmk3.Text;
                            _rmkVal1[0].Ausv_line = 3;
                            _rmkVal1[0].Ausv_cre_by = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_cre_dt = DateTime.Now;
                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmkVal1[0]);
                        }
                        // _AuditRmkList.Add(_rmk);
                    }
                    if (txtRmk4.Text != "")
                    {
                        List<AuditRemarkValue> _rmkVal1 = (from _res in _AuditRmkList
                                                           where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text) && _res.Ausv_line == 4
                                                           select _res).ToList<AuditRemarkValue>();
                        if (_rmkVal1 != null && _rmkVal1.Count > 0)
                        {
                            //AuditRemarkValue _rmk = new AuditRemarkValue();
                            _rmkVal1[0].Ausv_itm = lblItem.Text;
                            _rmkVal1[0].Ausv_itm_stus = lblStus.Text;
                            _rmkVal1[0].Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _rmkVal1[0].Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _rmkVal1[0].Ausv_job = txtJobNo.Text;
                            _rmkVal1[0].Ausv_val = txtRmk4.Text;
                            _rmkVal1[0].Ausv_line = 4;
                            _rmkVal1[0].Ausv_cre_by = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_cre_dt = DateTime.Now;
                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmkVal1[0]);
                            //_AuditRmkList.Add(_rmk);
                        }
                    }
                    if (txtRmk5.Text != "")
                    {
                        List<AuditRemarkValue> _rmkVal1 = (from _res in _AuditRmkList
                                                           where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text) && _res.Ausv_line == 5
                                                           select _res).ToList<AuditRemarkValue>();
                        if (_rmkVal1 != null && _rmkVal1.Count > 0)
                        {
                            // AuditRemarkValue _rmk = new AuditRemarkValue();
                            _rmkVal1[0].Ausv_itm = lblItem.Text;
                            _rmkVal1[0].Ausv_itm_stus = lblStus.Text;
                            _rmkVal1[0].Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _rmkVal1[0].Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _rmkVal1[0].Ausv_job = txtJobNo.Text;
                            _rmkVal1[0].Ausv_val = txtRmk5.Text;
                            _rmkVal1[0].Ausv_line = 5;
                            _rmkVal1[0].Ausv_cre_by = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_cre_dt = DateTime.Now;
                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmkVal1[0]);
                            //_AuditRmkList.Add(_rmk);
                        }
                    }
                    if (txtRmk6.Text != "")
                    {
                        List<AuditRemarkValue> _rmkVal1 = (from _res in _AuditRmkList
                                                           where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text) && _res.Ausv_line == 6
                                                           select _res).ToList<AuditRemarkValue>();
                        if (_rmkVal1 != null && _rmkVal1.Count > 0)
                        {
                            // AuditRemarkValue _rmk = new AuditRemarkValue();
                            _rmkVal1[0].Ausv_itm = lblItem.Text;
                            _rmkVal1[0].Ausv_itm_stus = lblStus.Text;
                            _rmkVal1[0].Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _rmkVal1[0].Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _rmkVal1[0].Ausv_job = txtJobNo.Text;
                            _rmkVal1[0].Ausv_val = txtRmk6.Text;
                            _rmkVal1[0].Ausv_line = 6;
                            _rmkVal1[0].Ausv_cre_by = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_cre_dt = DateTime.Now;
                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmkVal1[0]);
                            //_AuditRmkList.Add(_rmk);
                        }
                    }
                    if (txtRmk7.Text != "")
                    {
                        List<AuditRemarkValue> _rmkVal1 = (from _res in _AuditRmkList
                                                           where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text) && _res.Ausv_line == 7
                                                           select _res).ToList<AuditRemarkValue>();
                        if (_rmkVal1 != null && _rmkVal1.Count > 0)
                        {
                            //AuditRemarkValue _rmk = new AuditRemarkValue();
                            _rmkVal1[0].Ausv_itm = lblItem.Text;
                            _rmkVal1[0].Ausv_itm_stus = lblStus.Text;
                            _rmkVal1[0].Ausv_job = txtJobNo.Text;
                            _rmkVal1[0].Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _rmkVal1[0].Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _rmkVal1[0].Ausv_val = txtRmk7.Text;
                            _rmkVal1[0].Ausv_line = 7;
                            _rmkVal1[0].Ausv_cre_by = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_cre_dt = DateTime.Now;
                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmkVal1[0]);
                            //_AuditRmkList.Add(_rmk);
                        }
                    }
                    if (txtRmk8.Text != "")
                    {
                        List<AuditRemarkValue> _rmkVal1 = (from _res in _AuditRmkList
                                                           where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text) && _res.Ausv_line == 8
                                                           select _res).ToList<AuditRemarkValue>();
                        if (_rmkVal1 != null && _rmkVal1.Count > 0)
                        {
                            //AuditRemarkValue _rmk = new AuditRemarkValue();
                            _rmkVal1[0].Ausv_itm = lblItem.Text;
                            _rmkVal1[0].Ausv_itm_stus = lblStus.Text;
                            _rmkVal1[0].Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _rmkVal1[0].Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _rmkVal1[0].Ausv_job = txtJobNo.Text;
                            _rmkVal1[0].Ausv_val = txtRmk8.Text;
                            _rmkVal1[0].Ausv_line = 8;
                            _rmkVal1[0].Ausv_cre_by = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_cre_dt = DateTime.Now;
                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmkVal1[0]);
                            // _AuditRmkList.Add(_rmk);
                        }
                    }
                    if (txtRmk9.Text != "")
                    {
                        List<AuditRemarkValue> _rmkVal1 = (from _res in _AuditRmkList
                                                           where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text) && _res.Ausv_line == 9
                                                           select _res).ToList<AuditRemarkValue>();
                        if (_rmkVal1 != null && _rmkVal1.Count > 0)
                        {
                            //AuditRemarkValue _rmk = new AuditRemarkValue();
                            _rmkVal1[0].Ausv_itm = lblItem.Text;
                            _rmkVal1[0].Ausv_itm_stus = lblStus.Text;
                            _rmkVal1[0].Ausv_job = txtJobNo.Text;
                            _rmkVal1[0].Ausv_val = txtRmk9.Text;
                            _rmkVal1[0].Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _rmkVal1[0].Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _rmkVal1[0].Ausv_line = 9;
                            _rmkVal1[0].Ausv_cre_by = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_cre_dt = DateTime.Now;
                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmkVal1[0]);
                            //  _AuditRmkList.Add(_rmk);
                        }
                    }
                    if (txtRmk10.Text != "")
                    {
                        List<AuditRemarkValue> _rmkVal1 = (from _res in _AuditRmkList
                                                           where _res.Ausv_ser_id == Convert.ToInt32(lblSerId.Text) && _res.Ausv_line == 10
                                                           select _res).ToList<AuditRemarkValue>();
                        if (_rmkVal1 != null && _rmkVal1.Count > 0)
                        {
                            // AuditRemarkValue _rmk = new AuditRemarkValue();
                            _rmkVal1[0].Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                            _rmkVal1[0].Ausv_itm = lblItem.Text;
                            _rmkVal1[0].Ausv_itm_stus = lblStus.Text;
                            _rmkVal1[0].Ausv_job = txtJobNo.Text;
                            _rmkVal1[0].Ausv_val = txtRmk10.Text;
                            _rmkVal1[0].Ausv_line = 10;
                            _rmkVal1[0].Ausv_cre_by = BaseCls.GlbUserID;
                            _rmkVal1[0].Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                            _rmkVal1[0].Ausv_cre_dt = DateTime.Now;
                            CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmkVal1[0]);
                            //_AuditRmkList.Add(_rmk);
                        }
                    }
                }
                else
                {
                    if (txtRmk1.Text != "")
                    {
                        AuditRemarkValue _rmk = new AuditRemarkValue();
                        _rmk.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                        _rmk.Ausv_itm = lblItem.Text;
                        _rmk.Ausv_itm_stus = lblStus.Text;
                        _rmk.Ausv_job_seq = _itemList[0].Ausi_seq;
                        _rmk.Ausv_job = txtJobNo.Text;
                        _rmk.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                        _rmk.Ausv_job_seq = _itemList[0].Ausi_seq;
                        _rmk.Ausv_val = txtRmk1.Text;
                        _rmk.Ausv_line = 1;
                        _rmk.Ausv_cre_by = BaseCls.GlbUserID;
                        _rmk.Ausv_cre_dt = DateTime.Now;
                        _AuditRmkList.Add(_rmk);
                        CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmk);
                    }
                    if (txtRmk2.Text != "")
                    {
                        AuditRemarkValue _rmk = new AuditRemarkValue();
                        _rmk.Ausv_itm = lblItem.Text;
                        _rmk.Ausv_itm_stus = lblStus.Text;
                        _rmk.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                        _rmk.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                        _rmk.Ausv_job_seq = _itemList[0].Ausi_seq;
                        _rmk.Ausv_job = txtJobNo.Text;
                        _rmk.Ausv_val = txtRmk2.Text;
                        _rmk.Ausv_line = 2;
                        _rmk.Ausv_cre_by = BaseCls.GlbUserID;
                        _rmk.Ausv_cre_dt = DateTime.Now;
                        _AuditRmkList.Add(_rmk);
                        CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmk);
                    }
                    if (txtRmk3.Text != "")
                    {
                        AuditRemarkValue _rmk = new AuditRemarkValue();
                        _rmk.Ausv_itm = lblItem.Text;
                        _rmk.Ausv_itm_stus = lblStus.Text;
                        _rmk.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                        _rmk.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                        _rmk.Ausv_job_seq = _itemList[0].Ausi_seq;
                        _rmk.Ausv_job = txtJobNo.Text;
                        _rmk.Ausv_val = txtRmk3.Text;
                        _rmk.Ausv_line = 3;
                        _rmk.Ausv_cre_by = BaseCls.GlbUserID;
                        _rmk.Ausv_cre_dt = DateTime.Now;
                        _AuditRmkList.Add(_rmk);
                        CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmk);
                    }
                    if (txtRmk4.Text != "")
                    {
                        AuditRemarkValue _rmk = new AuditRemarkValue();
                        _rmk.Ausv_itm = lblItem.Text;
                        _rmk.Ausv_itm_stus = lblStus.Text;
                        _rmk.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                        _rmk.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                        _rmk.Ausv_job_seq = _itemList[0].Ausi_seq;
                        _rmk.Ausv_job = txtJobNo.Text;
                        _rmk.Ausv_val = txtRmk4.Text;
                        _rmk.Ausv_line = 4;
                        _rmk.Ausv_cre_by = BaseCls.GlbUserID;
                        _rmk.Ausv_cre_dt = DateTime.Now;
                        _AuditRmkList.Add(_rmk);
                        CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmk);
                    }
                    if (txtRmk5.Text != "")
                    {
                        AuditRemarkValue _rmk = new AuditRemarkValue();
                        _rmk.Ausv_itm = lblItem.Text;
                        _rmk.Ausv_itm_stus = lblStus.Text;
                        _rmk.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                        _rmk.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                        _rmk.Ausv_job_seq = _itemList[0].Ausi_seq;
                        _rmk.Ausv_job = txtJobNo.Text;
                        _rmk.Ausv_val = txtRmk5.Text;
                        _rmk.Ausv_line = 5;
                        _rmk.Ausv_cre_by = BaseCls.GlbUserID;
                        _rmk.Ausv_cre_dt = DateTime.Now;
                        _AuditRmkList.Add(_rmk);
                        CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmk);
                    }
                    if (txtRmk6.Text != "")
                    {
                        AuditRemarkValue _rmk = new AuditRemarkValue();
                        _rmk.Ausv_itm = lblItem.Text;
                        _rmk.Ausv_itm_stus = lblStus.Text;
                        _rmk.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                        _rmk.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                        _rmk.Ausv_job_seq = _itemList[0].Ausi_seq;
                        _rmk.Ausv_job = txtJobNo.Text;
                        _rmk.Ausv_val = txtRmk6.Text;
                        _rmk.Ausv_line = 6;
                        _rmk.Ausv_cre_by = BaseCls.GlbUserID;
                        _rmk.Ausv_cre_dt = DateTime.Now;
                        _AuditRmkList.Add(_rmk);
                        CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmk);
                    }
                    if (txtRmk7.Text != "")
                    {
                        AuditRemarkValue _rmk = new AuditRemarkValue();
                        _rmk.Ausv_itm = lblItem.Text;
                        _rmk.Ausv_itm_stus = lblStus.Text;
                        _rmk.Ausv_job = txtJobNo.Text;
                        _rmk.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                        _rmk.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                        _rmk.Ausv_job_seq = _itemList[0].Ausi_seq;
                        _rmk.Ausv_val = txtRmk7.Text;
                        _rmk.Ausv_line = 7;
                        _rmk.Ausv_cre_by = BaseCls.GlbUserID;
                        _rmk.Ausv_cre_dt = DateTime.Now;
                        _AuditRmkList.Add(_rmk);
                        CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmk);
                    }
                    if (txtRmk8.Text != "")
                    {
                        AuditRemarkValue _rmk = new AuditRemarkValue();
                        _rmk.Ausv_itm = lblItem.Text;
                        _rmk.Ausv_itm_stus = lblStus.Text;
                        _rmk.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                        _rmk.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                        _rmk.Ausv_job_seq = _itemList[0].Ausi_seq;
                        _rmk.Ausv_job = txtJobNo.Text;
                        _rmk.Ausv_val = txtRmk8.Text;
                        _rmk.Ausv_line = 8;
                        _rmk.Ausv_cre_by = BaseCls.GlbUserID;
                        _rmk.Ausv_cre_dt = DateTime.Now;
                        _AuditRmkList.Add(_rmk);
                        CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmk);
                    }
                    if (txtRmk9.Text != "")
                    {
                        AuditRemarkValue _rmk = new AuditRemarkValue();
                        _rmk.Ausv_itm = lblItem.Text;
                        _rmk.Ausv_itm_stus = lblStus.Text;
                        _rmk.Ausv_job = txtJobNo.Text;
                        _rmk.Ausv_val = txtRmk9.Text;
                        _rmk.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                        _rmk.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                        _rmk.Ausv_job_seq = _itemList[0].Ausi_seq;
                        _rmk.Ausv_line = 9;
                        _rmk.Ausv_cre_by = BaseCls.GlbUserID;
                        _rmk.Ausv_cre_dt = DateTime.Now;
                        _AuditRmkList.Add(_rmk);
                        CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmk);
                    }
                    if (txtRmk10.Text != "")
                    {
                        AuditRemarkValue _rmk = new AuditRemarkValue();
                        _rmk.Ausv_rpt_cd = cmbRmk.SelectedValue.ToString();
                        _rmk.Ausv_itm = lblItem.Text;
                        _rmk.Ausv_itm_stus = lblStus.Text;
                        _rmk.Ausv_job_seq = _itemList[0].Ausi_seq;
                        _rmk.Ausv_job = txtJobNo.Text;
                        _rmk.Ausv_val = txtRmk10.Text;
                        _rmk.Ausv_line = 10;
                        _rmk.Ausv_cre_by = BaseCls.GlbUserID;
                        _rmk.Ausv_ser_id = Convert.ToInt32(lblSerId.Text);
                        _rmk.Ausv_cre_dt = DateTime.Now;
                        _AuditRmkList.Add(_rmk);
                        CHNLSVC.Inventory.SavePhysicalStockVerificationRemark(_rmk);
                    }
                }

                txtRmk1.Text = "";
                txtRmk2.Text = "";
                txtRmk3.Text = "";
                txtRmk4.Text = "";
                txtRmk5.Text = "";
                txtRmk6.Text = "";
                txtRmk7.Text = "";
                txtRmk8.Text = "";
                txtRmk9.Text = "";
                txtRmk10.Text = "";
                btn_Exreq.Visible = false;
            }
            catch (Exception ex)
            { CHNLSVC.CloseChannel(); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnpopupRmk_Click(object sender, EventArgs e)
        {
            pnlAuditRmk.Visible = false;
            pnlMain.Enabled = true;
        }

        private void btnJobNo_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AuditStockVerify);
                DataTable _result = CHNLSVC.CommonSearch.GetAuditStockVerification(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtJobNo;
                _CommonSearch.ShowDialog();
                txtJobNo.Select();

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
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AuditStockVerify:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator+"1"+seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        private void txtJobNo_Leave(object sender, EventArgs e)
        {
            try
            {
                List<PhysicalStockVerificationItem> _list = CHNLSVC.Inventory.GetPhysicalVerificationItems(txtJobNo.Text);
                if (_list != null)
                {
                    List<PhysicalStockVerificationSerial> _serList = CHNLSVC.Inventory.GetPhysicalVerificationSerials(txtJobNo.Text);
                    List<AuditRemarkValue> _rmkList = CHNLSVC.Inventory.GetPhicalStockRemark(txtJobNo.Text);
                    _itemList = _list;
                    _serailList = _serList;
                    _AuditRmkList = _rmkList;
                    if (_AuditRmkList == null)
                    {
                        _AuditRmkList = new List<AuditRemarkValue>();
                    }

                    _itemList = _itemList.OrderBy(x => x.Ausi_itm).ToList<PhysicalStockVerificationItem>();
                    DataTable _dt = _itemList.ToDataTable();
                    grItems.AutoGenerateColumns = false;
                    BindingSource _ss = new BindingSource();
                    _ss.DataSource = _itemList;
                    grItems.DataSource = _itemList;
                }
            }
            catch (Exception ex)
            { CHNLSVC.CloseChannel(); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btnItemCode_Click(object sender, EventArgs e)
        {
           CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_commonSearch.SearchParams, null, null);
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtItemCode;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtItemCode.Select();
            }
            catch (Exception ex)
            { CHNLSVC.CloseChannel(); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void txtItemCode_Leave(object sender, EventArgs e)
        {
            try
            {
                MasterItem _itm = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemCode.Text.Trim());
                if (_itm != null)
                {
                    txtDescription.Text = _itm.Mi_shortdesc;
                    txtModel.Text = _itm.Mi_model;
                }
                else
                {
                    MessageBox.Show("Invalid Item Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDescription.Text = "";
                    txtModel.Text = "";
                }
            }
            catch (Exception ex)
            { CHNLSVC.CloseChannel(); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private void btn_Exreq_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CreditNoteAud);
                DataTable _result = CHNLSVC.CommonSearch.GetCreditNoteAll(_CommonSearch.SearchParams, null, null, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRmk7;
                _CommonSearch.ShowDialog();
                //txtCrNAmount.Text = _CommonSearch.GetResult(_CommonSearch.GlbSelectData, 3);                
                txtRmk7.Select();

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

        private void btn_upload_Click(object sender, EventArgs e)
        {
            if (pnl_scan_nonserial.Visible == true)
                pnl_scan_nonserial.Visible = false;
            else
                pnl_scan_nonserial.Visible = true;
        }

        private void btnBrItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "Excel files(*.xls)|*.xls,*.xlsx|All files(*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.ShowDialog();
            txtUploadItems.Text = openFileDialog1.FileName;
        }

        private void btn_NonsUpload_Click(object sender, EventArgs e)
        {
            //Sanjeewa 2016-02-11
            try
            {
                string _error = "";
                int k = -1;
                if (string.IsNullOrEmpty(txtJobNo.Text))
                {
                    MessageBox.Show("Please select Job Number.", "Non Serial Scanned Items Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtJobNo.Focus();
                    return;
                }
                           
                string _msg = string.Empty;
                StringBuilder _errorLst = new StringBuilder();                
                if (string.IsNullOrEmpty(txtUploadItems.Text))
                {
                    MessageBox.Show("Please select upload file path.", "Non Serial Scanned Items Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUploadItems.Text = "";
                    txtUploadItems.Focus();
                    return;
                }

                System.IO.FileInfo fileObj = new System.IO.FileInfo(txtUploadItems.Text);

                if (fileObj.Exists == false)
                {
                    MessageBox.Show("Selected file does not exist at the following path.", "Non Serial Scanned Items Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUploadItems.Focus();
                }

                if (optnonser.Checked)
                {
                    if (System.IO.File.Exists(txtUploadItems.Text) == true)
                    {
                        DataTable param = new DataTable();
                        DataRow dr;
                        string _itemcd;
                        param.Clear();
                        param.Columns.Add("textrec", typeof(string));

                        System.IO.StreamReader objReader;
                        objReader = new System.IO.StreamReader(txtUploadItems.Text);
                        do
                        {
                            dr = param.NewRow();
                            _itemcd = objReader.ReadLine();
                            if (_itemcd.Length == 16)
                                _itemcd = _itemcd.Substring(1, 7);
                            else if (_itemcd.Length == 15)
                                _itemcd = _itemcd.Substring(0, 7);
                            else if (_itemcd.Length == 8)
                                _itemcd = _itemcd.Substring(1, 7);
                            else if (_itemcd.Length == 20)
                                _itemcd = _itemcd.Substring(0, 12);
                            else
                                _itemcd = _itemcd;

                            dr["textrec"] = _itemcd;
                            param.Rows.Add(dr);

                        } while (objReader.Peek() != -1);
                        objReader.Close();

                        var valuation = (from b in param.AsEnumerable()
                                         group b by new { Itemcode = b["textrec"] } into g
                                         select new
                                         {
                                             Itemcode1 = g.Key.Itemcode,
                                             PhyCount = g.Count()
                                         }).ToList();
                        List<PhysicalStockVerificationItem> _itemList = new List<PhysicalStockVerificationItem>();

                        for (int i = 0; i < valuation.Count; i++)
                        {
                            PhysicalStockVerificationItem _item = new PhysicalStockVerificationItem();
                            _item.Ausi_job = txtJobNo.Text;
                            _item.Ausi_itm = valuation[i].Itemcode1.ToString();
                            _item.Ausi_stus = "GOD";
                            _item.Ausi_ledger_qty = 0;
                            _item.Ausi_physical_qty = Convert.ToDecimal(valuation[i].PhyCount.ToString());
                            _itemList.Add(_item);
                        }
                        k = CHNLSVC.Inventory.UpdatePhysicalStockItem(_itemList, out _error);
                    }
                }
                else
                {
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
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow _dr in dt.Rows)
                        {

                        }
                    }

                    k = 1;
                }

                if (k == -1)
                {
                    MessageBox.Show("Error Uploading the Text file. Error : " + _error, "Non Serial Scanned Items Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Succesfully uploaded the Text file.", "Non Serial Scanned Items Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pnl_scan_nonserial.Visible = false;
                }
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

        private void button2_Click(object sender, EventArgs e)
        {
            pnl_scan_nonserial.Visible = false;
        }

        private void txtJobNo_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
