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
using System.Diagnostics;
using FF.WindowsERPClient.Reports.Sales;
using FF.WindowsERPClient.Reports.HP;

namespace FF.WindowsERPClient.General
{

    /// <summary>
    /// written by sachith
    ///Create Date : 2013/01/23
    /// </summary>
    public partial class ReprintDocuments : Base
    {
        #region properies

        public string _docType;
        public string _status;
        public string _reqDocType;

        #endregion


        public ReprintDocuments()
        {
            InitializeComponent();

            _docType = "";
            _status = "P";
            _reqDocType = "";

            MasterCompany _mastercompany = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());

            opt5.Text = ConvertTo_ProperCase(_mastercompany.Mc_anal3.ToString()) + " Cash Memo";
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedIndex == 0)
                {
                    SetFirstTabButtons();
                }
                else
                {
                    SetSecondTabButtons();
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

        #region grid data bind events
        //Udesh Add string _company parameter - 08-Oct-2018
        private void BindDocNumbersGridData(string _type, string _loc, string _pc, string _company, DateTime _from, DateTime _to)
        {
            txtDocDate.Text = "";
            txtDocNo.Text = "";

            DataTable _tbl = CHNLSVC.General.GetReprintDocs(_type, _loc, _pc, _company, Convert.ToDateTime(_from), Convert.ToDateTime(_to).AddDays(1));

            if (_tbl.Rows.Count <= 0)
            {

                //var _tblItems =
                //   from dr in _tbl.AsEnumerable()
                //   group dr by new { docno = dr["docno"], docdate = dr["docdate"] } into item
                //   select new
                //   {
                //       docno = item.Key.docno,
                //       docdate = item.Key.docdate,

                //   };

                gvDocs.DataSource = null;// _tblItems;
            }
            else
            {
                //gvDocs.DataSource = CHNLSVC.General.GetReprintDocs(_type, _loc, _pc, Convert.ToDateTime(_from), Convert.ToDateTime(_to).AddDays(1));// Commented By Udesh - 08-Oct-2018
                gvDocs.DataSource = _tbl; //Added By Udesh - 08-Oct-2018
            }
        }

        private void BindRequestedDocDetailsGridData(string _loc, string _stus, DateTime _from, DateTime _to)
        {

            DataTable _tbl = CHNLSVC.General.GetRequestedReprintDocs(_loc, _stus, Convert.ToDateTime(_from), Convert.ToDateTime(_to).AddDays(1));

            if (_tbl.Rows.Count <= 0)
            {

                //var _tblItems =
                //   from dr in _tbl.AsEnumerable()
                //   group dr by new { DRP_DOC_NO = dr["DRP_DOC_NO"], DRP_DOC_DT = dr["DRP_DOC_DT"], DRP_TP = dr["DRP_TP"], DRP_REQ_DT = dr["DRP_REQ_DT"], DRP_STUS = dr["DRP_STUS"], DRP_PRINTED = dr["DRP_PRINTED"] } into item
                //   select new
                //   {
                //       DRP_DOC_NO = item.Key.DRP_DOC_NO,
                //       DRP_DOC_DT = item.Key.DRP_DOC_DT,
                //       DRP_TP = item.Key.DRP_TP,
                //       DRP_REQ_DT = item.Key.DRP_REQ_DT,
                //       DRP_STUS = item.Key.DRP_STUS,
                //       DRP_PRINTED = item.Key.DRP_PRINTED,

                //   };

                gvReqDocs.DataSource = null;// _tblItems;
            }
            else
            {
                //DataTable tem = CHNLSVC.General.GetRequestedReprintDocs(_loc, _stus, Convert.ToDateTime(_from), Convert.ToDateTime(_to).AddDays(1));// Commented By Udesh - 08-Oct-2018
                var _s = (from L in _tbl.AsEnumerable()
                         where L["DRP_TP"].ToString() == _docType
                         select new
                      {
                          LOC = L.Field<string>("DRP_LOC"),
                          DOC_NO = L.Field<string>("DRP_DOC_NO"),
                          DOC_DATE = L.Field<DateTime>("DRP_DOC_DT"),
                          DOC_TYPE = L.Field<string>("DRP_TP"),
                          DOC_REQUEST = L.Field<DateTime>("DRP_REQ_DT"),
                          DOC_STATUS = L.Field<string>("DRP_STUS"),
                          DOC_PRINTED = L.Field<decimal>("DRP_PRINTED"),
                          DRP_STUS_CHANGE_BY=L.Field<string>("DRP_STUS_CHANGE_BY")
                      }).ToList();
                var bind=new BindingSource();
                bind.DataSource=_s;
                gvReqDocs.DataSource = bind;
            }
        }

        private void BindAllDocDetailsGridData(string _loc, string _status, DateTime _from, DateTime _to)
        {

            DataTable _tbl = CHNLSVC.General.GetRequestedReprintDocs(_loc, _status, Convert.ToDateTime(_from), Convert.ToDateTime(_to).AddDays(1));

            if (_tbl.Rows.Count <= 0)
            {

                //var _tblItems =
                //   from dr in _tbl.AsEnumerable()
                //   group dr by new { DRP_LOC = dr["DRP_LOC"], DRP_DOC_NO = dr["DRP_DOC_NO"], DRP_DOC_DT = dr["DRP_DOC_DT"], DRP_TP = dr["DRP_TP"], DRP_REQ_DT = dr["DRP_REQ_DT"], DRP_STUS = dr["DRP_STUS"], DRP_PRINTED = dr["DRP_PRINTED"] } into item
                //   select new
                //   {
                //       DRP_LOC = item.Key.DRP_LOC,
                //       DRP_DOC_NO = item.Key.DRP_DOC_NO,
                //       DRP_DOC_DT = item.Key.DRP_DOC_DT,
                //       DRP_TP = item.Key.DRP_TP,
                //       DRP_REQ_DT = item.Key.DRP_REQ_DT,
                //       DRP_STUS = item.Key.DRP_STUS,
                //       DRP_PRINTED = item.Key.DRP_PRINTED,

                //   };

                gvAllDocs.DataSource = null;//_tblItems;
            }
            else
            {
                //gvAllDocs.DataSource = CHNLSVC.General.GetRequestedReprintDocs(_loc, _status, Convert.ToDateTime(_from), Convert.ToDateTime(_to).AddDays(1));// Commented By Udesh - 08-Oct-2018
                gvAllDocs.DataSource = _tbl;//Added By Udesh - 08-Oct-2018
            }
        }

        #endregion

        #region radio button check

        private void opt1_CheckedChanged(object sender, EventArgs e)
        {
            if (opt1.Checked)
            {
                _docType = "CS";
                View();
            }
        }
        private void opt30_CheckedChanged(object sender, EventArgs e)
        {
            if (opt30.Checked)
            {
                _docType = "QUO";
                View();
            }
        }

        private void opt2_CheckedChanged(object sender, EventArgs e)
        {
            if (opt2.Checked)
            {
                _docType = "CSREV";
                View();
            }
        }

        private void opt3_CheckedChanged(object sender, EventArgs e)
        {
            if (opt3.Checked)
            { _docType = "CR";
            View();
            }
        }

        private void opt4_CheckedChanged(object sender, EventArgs e)
        {
            if (opt4.Checked)
            {
                _docType = "CRREV";
                View();
            }
        }

        private void opt5_CheckedChanged(object sender, EventArgs e)
        {
            if (opt5.Checked)
            {
                _docType = "DIR";
                View();
            }
        }

        private void opt6_CheckedChanged(object sender, EventArgs e)
        {
            if (opt6.Checked)
            {
                _docType = "HP";
                View();
            }
        }

        private void opt7_CheckedChanged(object sender, EventArgs e)
        {
            if (opt7.Checked)
            {
                _docType = "HPREV";
                View();
            }
        }

        private void opt8_CheckedChanged(object sender, EventArgs e)
        {
            if (opt8.Checked)
            {
                _docType = "DEBT";
                View();
            }
        }

        private void opt9_CheckedChanged(object sender, EventArgs e)
        {
            if (opt9.Checked)
            {
                _docType = "OUT";
                View();
            }
        }

        private void opt10_CheckedChanged(object sender, EventArgs e)
        {
            if (opt10.Checked)
            {
                _docType = "IN";
                View();
            }
        }
        private void opt25_CheckedChanged(object sender, EventArgs e)
        {
            if (opt25.Checked)
            {
                _docType = "ACSER";
                View();
            }
        }

        private void opt11_CheckedChanged(object sender, EventArgs e)
        {
            if (opt11.Checked)
            {
                _docType = "ADVREC";
                View();
            }
        }

        private void opt12_CheckedChanged(object sender, EventArgs e)
        {
            if (opt12.Checked)
            {
                _docType = "REC";
                View();
            }
        }

        private void opt29_CheckedChanged(object sender, EventArgs e)
        {
            if (opt29.Checked)
            {
                _docType = "DFINV";
                View();
            }
        }

        private void opt13_CheckedChanged(object sender, EventArgs e)
        {
            if (opt13.Checked)
            {
                _docType = "HPAGR";
                View();
            }
        }

        private void opt14_CheckedChanged(object sender, EventArgs e)
        {
            if (opt14.Checked)
            {
                _docType = "WAREXT";
                View();
            }
        }

        private void opt15_CheckedChanged(object sender, EventArgs e)
        {
            if (opt15.Checked)
            {
                _docType = "MRN";
                View();
            }
        }

        private void opt16_CheckedChanged(object sender, EventArgs e)
        {
            if (opt16.Checked) {
                _docType = "RCC";
                View();
            }
        }

        private void opt17_CheckedChanged(object sender, EventArgs e)
        {
            if (opt17.Checked) {
                _docType = "INTR";
                View();
            }
        }

        private void opt18_CheckedChanged(object sender, EventArgs e)
        {
            if (opt18.Checked) { 
            
            }
        }

        private void opt19_CheckedChanged(object sender, EventArgs e)
        {
            if (opt19.Checked)
            {
                _docType = "FGAP";
                View();
            }
        }

        private void rdo20_CheckedChanged(object sender, EventArgs e)
        {
            if (rdo20.Checked)
            {
                _docType = "FIXEDASS";
                View();
            }
        }

       

        private void opt22_CheckedChanged(object sender, EventArgs e)
        {
            if (opt22.Checked)
            {
                _docType = "GRAN";
                View();
            }
        }

        private void opt23_CheckedChanged(object sender, EventArgs e)
        {
            if (opt23.Checked)
            {
                _docType = "DIN";
                View();
            }
        }

        private void opt24_CheckedChanged(object sender, EventArgs e)
        {
            if (opt24.Checked)
            {
                _docType = "VEHJOB";
                View();
            }
        }

        private void opt32_CheckedChanged(object sender, EventArgs e)
        {
            if (opt32.Checked)
            {
                _docType = "HPRS";
                View();
            }
        }
        private void opt33_CheckedChanged(object sender, EventArgs e)
        {
            if (opt33.Checked)
            {
                _docType = "RVT";
                View();
            }
        }

        private void opt26_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void opt27_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void opt28_CheckedChanged(object sender, EventArgs e)
        {
            if (opt28.Checked)
            {
                _docType = "COVER";
                View();
            }
        }

        private void opt31_CheckedChanged(object sender, EventArgs e)
        {
            if (opt31.Checked)
            {
                _docType = "EXC";
                View();
            }
        }

        private void optALL_CheckedChanged(object sender, EventArgs e)
        {
            if (optALL.Checked)
                _status = "ALL";
        }

        private void optPending_CheckedChanged(object sender, EventArgs e)
        {
            if (optPending.Checked)
                _status = "P";
        }

        private void optApp_CheckedChanged(object sender, EventArgs e)
        {
            if (optApp.Checked)
                _status = "A";
        }

        private void optRej_CheckedChanged(object sender, EventArgs e)
        {
            if (optRej.Checked)
                _status = "R";
        }



        #endregion

        #region button methods

        /// <summary>
        /// save reprint entry
        /// </summary>
        private void Save()
        {

            if (txtDocNo.Text == "")
            {
                MessageBox.Show("Please select a Document!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtReason.Text == "") {
                MessageBox.Show("Please enter reason before save.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                if (_date.Date != DateTime.Now.Date)
                {
                    MessageBox.Show("Your system date is not equal to server date! \nPlease contact system administrator....", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;

                Reprint_Docs _reprint = new Reprint_Docs();

                _reprint.Drp_tp = _docType;
                _reprint.Drp_doc_no = txtDocNo.Text;
                _reprint.Drp_doc_dt = Convert.ToDateTime(txtDocDate.Text);
                _reprint.Drp_loc = _masterLocation;
                _reprint.Drp_req_dt = _date;
                _reprint.Drp_is_add_pending = 0;
                _reprint.Drp_stus = "P";
                _reprint.Drp_app_dt = _date;
                _reprint.Drp_can_dt = _date;
                _reprint.Drp_rej_dt = _date;
                _reprint.Drp_stus_change_by = "";
                _reprint.Drp_printed = 0;
                _reprint.Drp_print_dt = _date;
                _reprint.Drp_reason = txtReason.Text;

                //quotation can print without approval
                bool isApp = false;
                if (_docType == "QUO") {
                    _reprint.Drp_stus = "A";
                    MessageBox.Show("Successfully Approved!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    isApp = true;
                }

                int X = CHNLSVC.General.SaveReprintDocRequest(_reprint);

                if (X == 9999) {
                    MessageBox.Show("Can not add request, Pending request exists", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //check param and send to approval
                double allowPeriod = 0;
                decimal numPrint = 0;
                List<Hpr_SysParameter> _list = CHNLSVC.Sales.GetAll_hpr_Para("REPERIOD", "COM", BaseCls.GlbUserComCode);
                if (_list != null && _list.Count > 0)
                {
                    allowPeriod = (double)_list[0].Hsy_val;
                }
                List<Hpr_SysParameter> _list1 = CHNLSVC.Sales.GetAll_hpr_Para("RENUM", "COM", BaseCls.GlbUserComCode);
                if (_list1 != null && _list1.Count > 0)
                {
                    numPrint = _list1[0].Hsy_val;
                }
                int docCount = CHNLSVC.General.GetReprintDocCount(txtDocNo.Text);
                docCount = docCount + 1;

                //get server date time
                
                //check doc type and get created date
                if (_docType == "CS" || _docType == "DFINV")
                {
                    InvoiceHeader _inv = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtDocNo.Text);
                    if (DateTime.Now <= (_inv.Sah_cre_when.AddMinutes(allowPeriod)) && numPrint >= docCount)
                    {
                        isApp = true;
                        //process approval
                        UpdateStatus(txtDocNo.Text);
                        SavePrint(txtDocNo.Text, _docType);
                    }
                }
                else if (_docType == "CSREV")
                {
                    InvoiceHeader _inv = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtDocNo.Text);
                    if (DateTime.Now <= (_inv.Sah_cre_when.AddMinutes(allowPeriod)) && numPrint >= docCount)
                    {
                        isApp = true;
                        //process approval
                        UpdateStatus(txtDocNo.Text);
                        SavePrint(txtDocNo.Text, _docType);
                    }
                }
                else if (_docType == "CR")
                {
                    InvoiceHeader _inv = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtDocNo.Text);
                    if (DateTime.Now <= (_inv.Sah_cre_when.AddMinutes(allowPeriod)) && numPrint >= docCount)
                    {
                        isApp = true;
                        //process approval
                        UpdateStatus(txtDocNo.Text);
                        SavePrint(txtDocNo.Text, _docType);
                    }
                }
                else if (_docType == "CRAFSL")
                {
                    _reprint.Drp_stus = "A";
                    MessageBox.Show("Successfully Approved!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    isApp = true;
                }
                //else if (_docType == "CRAFSL")
                //{
                //    InvoiceHeader _inv = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtDocNo.Text);
                //    if (DateTime.Now <= (_inv.Sah_cre_when.AddMinutes(allowPeriod)) && numPrint >= docCount)
                //    {
                //        isApp = true;
                //        //process approval
                //        UpdateStatus(txtDocNo.Text);
                //        SavePrint(txtDocNo.Text, _docType);
                //    }
                //}
                else if (_docType == "CRREV")
                {
                    InvoiceHeader _inv = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtDocNo.Text);
                    if (DateTime.Now <= (_inv.Sah_cre_when.AddMinutes(allowPeriod)) && numPrint >= docCount)
                    {
                        isApp = true;
                        //process approval
                        UpdateStatus(txtDocNo.Text);
                        SavePrint(txtDocNo.Text, _docType);
                    }
                }
                else if (_docType == "DIR")
                {
                    DataTable dt = CHNLSVC.Sales.GetInsurance(txtDocNo.Text);
                    DateTime date = Convert.ToDateTime(dt.Rows[dt.Rows.Count - 1]["hti_cre_dt"]);
                    if (DateTime.Now <= (date.AddMinutes(allowPeriod)) && numPrint >= docCount)
                    {
                        isApp = true;
                        //process approval
                        UpdateStatus(txtDocNo.Text);
                        SavePrint(txtDocNo.Text, _docType);
                    }
                }
                else if (_docType == "QUO")
                {
                    QuotationHeader _quo = CHNLSVC.Sales.Get_Quotation_HDR(txtDocNo.Text);
                    DateTime date = Convert.ToDateTime(_quo.Qh_cre_when);
                    if (DateTime.Now <= (date.AddMinutes(allowPeriod)) && numPrint >= docCount)
                    {
                        isApp = true;
                        //process approval
                        UpdateStatus(txtDocNo.Text);
                        SavePrint(txtDocNo.Text, _docType);
                    }
                }

                else if (_docType == "HP")
                {
                    InvoiceHeader _inv = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtDocNo.Text);
                    if (DateTime.Now <= (_inv.Sah_cre_when.AddMinutes(allowPeriod)) && numPrint >= docCount)
                    {
                        isApp = true;
                        //process approval
                        UpdateStatus(txtDocNo.Text);
                        SavePrint(txtDocNo.Text, _docType);
                    }
                }
                else if (_docType == "HPAGR")
                {
                    HpAccount _acc = CHNLSVC.Sales.GetHP_Account_onAccNo(txtDocNo.Text);
                    if (_acc != null)
                    {
                        InvoiceHeader _inv = CHNLSVC.Sales.GetInvoiceHeaderDetails(_acc.Hpa_invc_no);
                        if (DateTime.Now <= (_inv.Sah_cre_when.AddMinutes(allowPeriod)) && numPrint >= docCount)
                        {
                            isApp = true;
                            //process approval
                            UpdateStatus(txtDocNo.Text);
                            SavePrint(txtDocNo.Text, _docType);
                        }
                    }
                }
                else if (_docType == "HPREV")
                {
                    InvoiceHeader _inv = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtDocNo.Text);
                    if (DateTime.Now <= (_inv.Sah_cre_when.AddMinutes(allowPeriod)) && numPrint >= docCount)
                    {
                        isApp = true;
                        //process approval
                        UpdateStatus(txtDocNo.Text);
                        SavePrint(txtDocNo.Text, _docType);
                    }
                }
                else if (_docType == "ADVREC")
                {
                    RecieptHeader _rec = CHNLSVC.Sales.GetReceiptHeader(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtDocNo.Text);
                    if (DateTime.Now <= (_rec.Sar_create_when.AddMinutes(allowPeriod)) && numPrint >= docCount)
                    {
                        isApp = true;
                        //process approval
                        UpdateStatus(txtDocNo.Text);
                        SavePrint(txtDocNo.Text, _docType);
                    }
                }
                else if (_docType == "REC" || _docType == "DEBT" /*|| _docType=="HPRS"*/) //HP Reciept can not auto approve
                {
                    RecieptHeader _rec = CHNLSVC.Sales.GetReceiptHeader(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtDocNo.Text);
                    if (DateTime.Now <= (_rec.Sar_create_when.AddMinutes(allowPeriod)) && numPrint >= docCount)
                    {
                        isApp = true;
                        //process approval
                        UpdateStatus(txtDocNo.Text);
                        SavePrint(txtDocNo.Text, _docType);
                    }
                }
                else if (_docType == "WAREXT" || _docType == "FGAP")
                {
                    RecieptHeader _rec = CHNLSVC.Sales.GetReceiptHeader(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtDocNo.Text);
                    if (DateTime.Now <= (_rec.Sar_create_when.AddMinutes(allowPeriod)) && numPrint >= docCount)
                    {
                        isApp = true;
                        //process approval
                        UpdateStatus(txtDocNo.Text);
                        SavePrint(txtDocNo.Text, _docType);
                    }
                }
                else if (_docType == "MRN" || _docType == "INTR" || _docType == "GRAN" || _docType == "DIN")
                {
                    InventoryRequest tem = new InventoryRequest();
                    tem.Itr_req_no = txtDocNo.Text;
                    InventoryRequest _inv = CHNLSVC.Inventory.GetInventoryRequestData(tem);
                    if (DateTime.Now <= (_inv.Itr_cre_dt.AddMinutes(allowPeriod)) && numPrint >= docCount)
                    {
                        isApp = true;
                        //process approval
                        UpdateStatus(txtDocNo.Text);
                        SavePrint(txtDocNo.Text, _docType);
                    }

                }
                else if (_docType == "RCC")
                {

                    RCC _rcc = CHNLSVC.Inventory.GetRccByNo(txtDocNo.Text);
                    if (DateTime.Now <= (_rcc.Inr_cre_dt.AddMinutes(allowPeriod)) && numPrint >= docCount)
                    {
                        isApp = true;
                        //process approval
                        UpdateStatus(txtDocNo.Text);
                        SavePrint(txtDocNo.Text, _docType);
                    }
                }

                else if (_docType == "VEHJOB" && _docType == "ACJOB")
                {
                    DataTable dt = CHNLSVC.Sales.GetReprintSevericeJob("SEV", txtDocNo.Text);
                    DateTime date = Convert.ToDateTime(dt.Rows[dt.Rows.Count - 1]["SJB_DT"]);
                    if (DateTime.Now <= (date.AddMinutes(allowPeriod)) && numPrint >= docCount)
                    {
                        isApp = true;
                        //process approval
                        UpdateStatus(txtDocNo.Text);
                        SavePrint(txtDocNo.Text, _docType);
                    }

                }

                else if (_docType == "COVER")
                {
                    List<FF.BusinessObjects.VehicleInsuarance> _ins = CHNLSVC.Sales.GetVehicalInsByRefNo(txtDocNo.Text);
                    if (DateTime.Now <= (_ins[0].Svit_cre_dt.AddMinutes(allowPeriod)) && numPrint >= docCount)
                    {
                        isApp = true;
                        //process approval
                        UpdateStatus(txtDocNo.Text);
                        SavePrint(txtDocNo.Text, _docType);
                    }
                }

                else if (_docType == "HPAGR")
                {
                    HpAccount _acc = CHNLSVC.Sales.GetHP_Account_onAccNo(txtDocNo.Text);
                    if (DateTime.Now <= (_acc.Hpa_acc_cre_dt.AddMinutes(allowPeriod)) && numPrint >= docCount)
                    {
                        isApp = true;
                        //process approval
                        UpdateStatus(txtDocNo.Text);
                        SavePrint(txtDocNo.Text, _docType);
                    }
                }

                else if (_docType == "FIXEDASS")
                {
                    DataTable dt = CHNLSVC.Inventory.GetAdhochdrTable(txtDocNo.Text);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (DateTime.Now.Date <= (Convert.ToDateTime(dt.Rows[0]["IADH_APP_DT"]).AddMinutes(allowPeriod)) && numPrint > docCount)
                        {
                            isApp = true;
                            //process approval
                            UpdateStatus(txtDocNo.Text);
                            SavePrint(txtDocNo.Text, _docType);
                        }
                    }
                }
                else if (_docType == "OUT" || _docType == "IN" || _docType=="EXC" || _docType=="RVT")
                {
                    InventoryHeader inv = CHNLSVC.Inventory.Get_Int_Hdr(txtDocNo.Text);
                    if (DateTime.Now <= (inv.Ith_cre_when.AddMinutes(allowPeriod)) && numPrint >= docCount)
                    {
                        isApp = true;
                        //process approval
                        UpdateStatus(txtDocNo.Text);
                        SavePrint(txtDocNo.Text, _docType);
                    }

                }
                else if (_docType == "SMTINS")      //kapila 18/4/2015
                {
                    DataTable dt = CHNLSVC.Sales.GetReceiptByRecNo(txtDocNo.Text);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (DateTime.Now.Date <= (Convert.ToDateTime(dt.Rows[0]["sar_create_when"]).AddMinutes(allowPeriod)) && numPrint > docCount)
                        {
                            isApp = true;
                            //process approval
                            UpdateStatus(txtDocNo.Text);
                            SavePrint(txtDocNo.Text, _docType);
                        }
                    }
                }



                BindRequestedDocDetailsGridData(BaseCls.GlbUserDefLoca, "ALL", dateTimePickerFromDate.Value.Date, dateTimePickerToDate.Value.Date);
                BindRequestedDocDetailsGridData(BaseCls.GlbUserDefLoca, "ALL", dateTimePickerFromDate.Value.Date, dateTimePickerToDate.Value.Date);

                txtDocNo.Text = "";
                txtDocDate.Text = "";
                txtReason.Text = "";
                if (!isApp && X > 0)
                {
                    MessageBox.Show("Successfully Saved!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
        }

        private Service_Chanal_parameter _scvParam = null;

        private void Print()
        {            
            if (txtReqDocNo.Text == "")
            {
                MessageBox.Show("Please select a Document!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //GlbDocNosList = txtReqDocNo.Text;
            try
            {
                int X = CHNLSVC.General.UpdatePrintStatus(txtReqDocNo.Text);
                BaseCls.GlbReportName = string.Empty;
                GlbReportName = string.Empty;
                
                _scvParam = CacheLayer.Get<Service_Chanal_parameter>(CacheLayer.Key.ChannelParameter.ToString());
                //if (_scvParam == null)
                //{
                if (_reqDocType == "CS" || _reqDocType == "CSREV" || _reqDocType == "HP" || _reqDocType == "HPREV" || _reqDocType == "CR" || _reqDocType == "CRREV" || _reqDocType == "POCA" || _reqDocType == "POCR")
                    {
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        if (BaseCls.GlbUserComCode == "SGL")
                        {

                            //TODO: NEED REPORT
                            BaseCls.GlbReportName = string.Empty;
                            GlbReportName = string.Empty;
                            Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                            BaseCls.GlbReportTp = "INV";
                            _view.GlbReportName = "InvoiceHalfPrints.rpt";
                            BaseCls.GlbReportName = "InvoiceHalfPrints.rpt";
                            _view.GlbReportDoc = txtReqDocNo.Text;
                            _view.Show();
                            _view = null;
                        }
                        else
                        {
                            //Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                            //_view.GlbReportName = "InvoiceHalfPrints.rpt";
                            //_view.GlbReportDoc = txtReqDocNo.Text;
                            //_view.Show();
                            //_view = null;
                            bool _isPrintElite = false;
                            InvoiceHeader _inv = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtReqDocNo.Text);
                            if (_inv == null)
                            {
                                MessageBox.Show("Invoice print invoice null error");
                            }
                            MasterBusinessEntity _itm = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, _inv.Sah_cus_cd, string.Empty, string.Empty, "C");
                            if (_itm == null)
                            {
                                MessageBox.Show("Invoice print customer null error");
                            }
                            //get permission
                            bool _permission = CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11055);


                            MasterProfitCenter _MasterProfitCenter = CacheLayer.Get<MasterProfitCenter>(CacheLayer.Key.ProfitCenter.ToString());
                            { BaseCls.GlbReportTp = "INV";
                            if (_MasterProfitCenter.Mpc_chnl.Trim() == "ELITE" || _MasterProfitCenter.Mpc_chnl.Trim() == "RRC1" || _MasterProfitCenter.Mpc_chnl.Trim() == "RRE2" || _MasterProfitCenter.Mpc_chnl.Trim() == "APPLE" || _MasterProfitCenter.Mpc_chnl.Trim() == "APPIST") 
                                { 
                                    BaseCls.GlbReportDoc = txtReqDocNo.Text;
                                    clsSalesRep objSales = new clsSalesRep();
                                    if (objSales.checkIsDirectPrint() == true && objSales.removeIsDirectPrint() == false && _inv.Sah_is_svat == false)
                                    {
                                        objSales.InvoicePrint_Direct();
                                        _isPrintElite = true;
                                    }
                                    else
                                    {
                                        ReportViewer _view = new ReportViewer();
                                        BaseCls.GlbReportName = string.Empty;
                                        GlbReportName = string.Empty;
                                        _view.GlbReportName = string.Empty;
                                        _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "InvoiceHalfPrints.rpt" : BaseCls.GlbUserComCode == "GCL" ? "InvoicePrints_Gold.rpt" : "InvoiceHalfPrints.rpt";
                                        _view.GlbReportDoc = txtReqDocNo.Text;
                                        _view.Show();
                                        _view = null;
                                        _isPrintElite = true;
                                    }
                                } 
                            }
                            if (!_permission)
                            {
                                //AUTO_DEL
                                {
                                    if (_MasterProfitCenter.Mpc_chnl.Trim() == "AUTO_DEL")
                                    {
                                        if (_reqDocType == "CR")
                                        {
                                            ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty;
                                            GlbReportName = string.Empty; _view.GlbReportName = string.Empty;
                                            _view.GlbReportName = "DealerCreditInvoicePrints.rpt";
                                            BaseCls.GlbReportName = "DealerCreditInvoicePrints.rpt";
                                            _view.GlbReportDoc = txtReqDocNo.Text;
                                            BaseCls.GlbReportDoc = txtReqDocNo.Text; //DealerCreditInvoicePrints
                                            _view.Show(); _view = null; _isPrintElite = true;
                                        }
                                        else
                                        {
                                            ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _view.GlbReportName = "InvoiceHalfPrints.rpt"; BaseCls.GlbReportName = "InvoiceHalfPrints.rpt"; _view.GlbReportDoc = txtReqDocNo.Text; BaseCls.GlbReportDoc = txtReqDocNo.Text;
                                            _view.Show(); _view = null; _isPrintElite = true;
                                        }
                                    }
                                }
                            }
                            if (!_isPrintElite)
                            {
                                #region new code
                                if (_itm.Mbe_sub_tp != "C.") //updated by akila 2017/11/29
                                {
                                    if (_itm.Mbe_cate == "LEASE")
                                    {
                                        ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; BaseCls.GlbReportTp = "INV"; _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "InvoiceHalfPrints.rpt" : BaseCls.GlbUserComCode == "GCL" ? "InvoicePrints_Gold.rpt" : BaseCls.GlbUserComCode == "AST" ? "InvoicePrint_AST.rpt" : "InvoicePrintTax.rpt"; _view.GlbReportDoc = txtReqDocNo.Text; _view.Show(); _view = null;
                                        if (_itm.Mbe_cate == "LEASE") { ReportViewer _viewt = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _viewt.GlbReportName = string.Empty; _viewt.GlbReportName = "InvoicePrint_insus.rpt"; _viewt.GlbReportDoc = txtReqDocNo.Text; _viewt.Show(); _viewt = null; }
                                    }
                                    else
                                    {
                                        if (_inv.Sah_tax_inv == false)
                                        { ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; BaseCls.GlbReportTp = "INV"; _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "InvoiceHalfPrints.rpt" : BaseCls.GlbUserComCode == "GCL" ? "InvoicePrints_Gold.rpt" : "InvoiceHalfPrints.rpt"; _view.GlbReportDoc = txtReqDocNo.Text; _view.Show(); _view = null; }
                                        else
                                        {
                                            ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; BaseCls.GlbReportTp = "INV"; _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "InvoiceHalfPrints.rpt" : BaseCls.GlbUserComCode == "GCL" ? "InvoicePrints_Gold.rpt" : BaseCls.GlbUserComCode == "AST" ? "InvoicePrint_AST.rpt" : "InvoiceHalfPrints.rpt"; _view.GlbReportDoc = txtReqDocNo.Text; _view.Show(); _view = null;
                                            if (_itm.Mbe_cate == "LEASE") { ReportViewer _viewt = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _viewt.GlbReportName = string.Empty; _viewt.GlbReportName = "InvoicePrintTax_insus.rpt"; _viewt.GlbReportDoc = txtReqDocNo.Text; _viewt.Show(); _viewt = null; }
                                        }
                                    }
                                }
                                else
                                {
                                    //Dealer
                                    ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; BaseCls.GlbReportTp = "INV"; _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "InvoiceHalfPrints.rpt" : BaseCls.GlbUserComCode == "GCL" ? "InvoicePrints_Gold.rpt" : BaseCls.GlbUserComCode == "AST" ? "InvoicePrint_AST.rpt" : "InvoicePrintTax.rpt"; _view.GlbReportDoc = txtReqDocNo.Text; _view.Show(); _view = null;

                                    List<FF.BusinessObjects.RecieptItem> _itms = CHNLSVC.Sales.GetReceiptItemList(txtReqDocNo.Text.Trim());
                                    if (_itms == null || _itms.Count <= 0)
                                    {
                                        MessageBox.Show("Invoice print reciept itms null error");
                                    }

                                    if (_itms != null && _itms.Count > 0)
                                    {
                                        if (_itm.Mbe_cate == "LEASE") { ReportViewer _viewt = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _viewt.GlbReportName = string.Empty; _viewt.GlbReportName = "InvoicePrint_insus.rpt"; _viewt.GlbReportDoc = txtReqDocNo.Text; _viewt.Show(); _viewt = null; }
                                    }                                    
                                }
                                #endregion


                                #region old code
                                //if (_itm.Mbe_sub_tp != "C.")
                                //{                                   
                                //    //Showroom
                                //    //========================= INVOCIE  CASH/CREDIT/ HIRE 
                                //    if (_inv.Sah_tax_inv == false)
                                //    {
                                //        //if (BaseCls.GlbUserComCode == "AAL" && BaseCls.GlbDefChannel == "MVEHI" || BaseCls.GlbDefChannel == "APTL") //tHARANGA 2017/06/28
                                //        //{
                                //        //    ReportViewer _view = new ReportViewer();
                                //        //    _view.GlbReportName = "InvoiceAuto.rpt";
                                //        //    BaseCls.GlbReportTp = "INV";
                                //        //    _view.GlbReportDoc = txtReqDocNo.Text;
                                //        //    _view.GlbSerial = null;
                                //        //    _view.GlbWarranty = null;
                                //        //    _view.Show();
                                //        //    _view = null;

                                //        //}
                                //        //else
                                //        //{
                                //            BaseCls.GlbReportName = string.Empty;
                                //            GlbReportName = string.Empty;
                                //            ReportViewer _view = new ReportViewer();
                                //            BaseCls.GlbReportTp = "INV";
                                //            _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "InvoiceHalfPrints.rpt" : BaseCls.GlbUserComCode == "GCL" ? "InvoicePrints_Gold.rpt" : "InvoiceHalfPrints.rpt";
                                //            _view.GlbReportDoc = txtReqDocNo.Text;
                                //            _view.Show();
                                //            _view = null;
                                //        //}
                                //    }
                                //    else
                                //    {
                                //        //Add Code by Chamal 27/04/2013
                                //        //====================  TAX INVOICE
                                //        BaseCls.GlbReportName = string.Empty;
                                //        GlbReportName = string.Empty;
                                //        ReportViewer _view = new ReportViewer();
                                //        _view.GlbReportName = "InvoicePrintTax.rpt";
                                //        _view.GlbReportDoc = txtReqDocNo.Text;
                                //        _view.Show();
                                //        _view = null;

                                //        if (_itm.Mbe_cate == "LEASE")
                                //        {
                                //            BaseCls.GlbReportName = string.Empty;
                                //            GlbReportName = string.Empty;
                                //            ReportViewer _viewt = new ReportViewer();
                                //            _viewt.GlbReportName = "InvoicePrintTax_insus.rpt";
                                //            _viewt.GlbReportDoc = txtReqDocNo.Text;
                                //            _viewt.Show();
                                //            _viewt = null;
                                //        }

                                //        //====================  TAX INVOICE

                                //    }
                                //}
                                //else
                                //{


                                //    //if (BaseCls.GlbUserComCode == "AAL" && BaseCls.GlbDefChannel == "MVEHI" || BaseCls.GlbDefChannel == "APTL") //tHARANGA 2017/06/28
                                //    //{
                                //    //    ReportViewer _view = new ReportViewer();
                                //    //    _view.GlbReportName = "InvoiceAuto.rpt";
                                //    //    BaseCls.GlbReportTp = "INV";
                                //    //    _view.GlbReportDoc = txtReqDocNo.Text;
                                //    //    _view.GlbSerial = null;
                                //    //    _view.GlbWarranty = null;
                                //    //    _view.Show();
                                //    //    _view = null;

                                //    //}
                                //    //else
                                //    //{
                                //        //Dealer
                                //        BaseCls.GlbReportName = string.Empty;
                                //        GlbReportName = string.Empty;
                                //        ReportViewer _view = new ReportViewer();
                                //        _view.GlbReportName = "InvoicePrints.rpt";
                                //        _view.GlbReportDoc = txtReqDocNo.Text;
                                //        _view.Show();
                                //        _view = null;

                                //    //}
                                //    List<FF.BusinessObjects.RecieptItem> _itms = CHNLSVC.Sales.GetReceiptItemList(txtReqDocNo.Text.Trim());
                                //    if (_itms == null || _itms.Count <= 0)
                                //    {
                                //        MessageBox.Show("Invoice print reciept itms null error");
                                //    }
                                //    if (_itms != null)
                                //        if (_itms.Count > 0)
                                //            if (_itm.Mbe_cate == "LEASE")
                                //            {
                                //                BaseCls.GlbReportName = string.Empty;
                                //                GlbReportName = string.Empty;
                                //                ReportViewer _viewt = new ReportViewer();
                                //                _viewt.GlbReportName = "InvoicePrint_insus.rpt";
                                //                _viewt.GlbReportDoc = txtReqDocNo.Text;
                                //                _viewt.Show();
                                //                _viewt = null;

                                //            }
                                //}
                            #endregion

                            }
                        }

                        if (_reqDocType == "CS" || _reqDocType == "HP" || _reqDocType == "CR")
                        {
                            //Tharanga 2017/06/16
                            string invNo = txtReqDocNo.Text;
                            DataTable odt = new DataTable();
                            odt = CHNLSVC.Sales.get_sar_provou_tp(BaseCls.GlbUserComCode, invNo);
                            if (odt.Rows.Count > 0)
                            {
                                ReportViewer _view1 = new ReportViewer();
                                BaseCls.GlbReportName = string.Empty;
                                _view1.GlbReportName = string.Empty;
                                BaseCls.GlbReportTp = "Print voucher separately";
                                _view1.GlbReportName = "giftvoucher.rpt";
                                _view1.GlbReportDoc = txtReqDocNo.Text;
                                _view1.GlbReportProfit = BaseCls.GlbUserDefProf;
                                _view1.Show();
                                _view1 = null;
                            }
                        }

                    }
                //}
                //else
                //{
                //    BaseCls.GlbReportTp = "JOBINV";
                //    BaseCls.GlbReportName = string.Empty;
                //    GlbReportName = string.Empty;
                //    ReportViewer _viewsvc = new ReportViewer();
                //    _viewsvc.GlbReportName = "Job_Invoice.rpt";
                //    _viewsvc.GlbReportDoc = txtReqDocNo.Text;
                //    _viewsvc.Show();
                //    _viewsvc = null;
                //}
                    if (_reqDocType == "CRAFSL")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    BaseCls.GlbReportTp = "CRAFSL";
                    ReportViewer _insu = new ReportViewer();
                    _insu.GlbReportName = "DealerCreditInvoicePrints_AFSL.rpt";
                    _insu.GlbReportDoc = txtReqDocNo.Text;
                    _insu.Show();
                    _insu = null;
                }
                if (_reqDocType == "DIR")
                {
                    BaseCls.GlbReportDoc = txtReqDocNo.Text;
                    clsHpSalesRep objHp = new clsHpSalesRep();
                    clsSalesRep objSales = new clsSalesRep();
                    if (objSales.checkIsDirectPrint() == true && objSales.removeIsDirectPrint() == false)
                    {
                        objHp.InsurancePrint_Direct();
                    }
                    else
                    {
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        BaseCls.GlbReportTp = "INSUR";
                        ReportViewer _insu = new ReportViewer();
                        _insu.GlbReportName = "InsurancePrint.rpt";
                        _insu.GlbReportDoc = txtReqDocNo.Text;
                        _insu.Show();
                        _insu = null;
                    }
                }

                if (_reqDocType == "QUO")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    BaseCls.GlbReportDoc = txtReqDocNo.Text;
                    BaseCls.GlbReportComp = BaseCls.GlbUserComCode;
                    BaseCls.GlbReportName = "Quotation_RepPrint.rpt";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    _view.GlbReportName = "QUOTATION";
                    _view.Show();
                    _view = null;
                }

                if (_reqDocType == "EXC")
                {
                    BaseCls.GlbReportTp = "ERN";
                    if (BaseCls.GlbUserComCode == "SGL")
                    {
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        _view.GlbReportName = "Exchange_Docs.rpt";
                        BaseCls.GlbReportName = "Exchange_Docs.rpt";
                        BaseCls.GlbReportDoc = txtReqDocNo.Text;
                        _view.Show();
                        _view = null;
                    }
                    else
                    { 
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        _view.GlbReportName = "Exchange_Docs.rpt";
                        BaseCls.GlbReportName = "Exchange_Docs.rpt";
                        BaseCls.GlbReportDoc = txtReqDocNo.Text;
                        _view.Show();
                        _view = null;
                    }

                }

                if (_reqDocType == "ADVREC")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    BaseCls.GlbReportTp = "ADVREC";
                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    _view.GlbReportName = "ReceiptPrints_n.rpt";
                    BaseCls.GlbReportName = "ReceiptPrints_n.rpt";
                    _view.GlbReportDoc = txtReqDocNo.Text;
                    _view.Show();
                    _view = null;
                }

                if (_reqDocType == "SMTINS")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                  //  BaseCls.GlbReportTp = "REC";
                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    _view.GlbReportName = "ReceiptPrints.rpt";
                    BaseCls.GlbReportName = "ReceiptPrints.rpt";
                    _view.GlbReportDoc = txtReqDocNo.Text;
                    _view.Show();
                    _view = null;
                }

                if (_reqDocType == "DFINV")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    ReportViewer _view = new ReportViewer();
                    if (BaseCls.GlbUserComCode == "SGL")
                    {
                        BaseCls.GlbReportName = "sInvoiceDutyFree.rpt";//Add by Chamal 04-Mar-2014
                    }
                    else
                    {
                        if (BaseCls.GlbUserComCode == "EDL")
                        {
                            BaseCls.GlbReportName = "InvoiceDutyFreeEdison.rpt";
                        }
                        else
                        {
                            BaseCls.GlbReportName = "InvoiceDutyFree.rpt";
                        }
                    }
                    _view.GlbReportDoc = txtReqDocNo.Text;

                    _view.Show();
                    _view = null;
                }
                if (_reqDocType == "DEBT")
                {
                    RecieptHeader _rec = CHNLSVC.Sales.GetReceiptHeader(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtReqDocNo.Text);
                    if (_rec == null)
                    {
                        MessageBox.Show("Debt print reciept null error");
                    }
                    MasterBusinessEntity _itm = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, _rec.Sar_debtor_cd, string.Empty, string.Empty, "C");
                    if (_itm == null)
                    {
                        MessageBox.Show("Debt print customer null error");
                    }
                    if (_itm.Mbe_sub_tp != "C.")
                    {
                        //Showroom
                        //========================= INVOCIE  CASH/CREDIT/ HIRE 
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        BaseCls.GlbReportTp = "REC";
                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                        _view.GlbReportName = "ReceiptPrints.rpt";
                        BaseCls.GlbReportName = "ReceiptPrints.rpt";
                        _view.GlbReportDoc = txtReqDocNo.Text;
                        _view.Show();
                        _view = null;
                    }
                    else
                    {
                        //Dealer
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                        _view.GlbReportName = "ReceiptPrints.rpt";
                        BaseCls.GlbReportName = "ReceiptPrints.rpt";
                        _view.GlbReportDoc = txtReqDocNo.Text;
                        _view.Show();
                        _view = null;
                    }
                }

                if (_reqDocType == "REC")
                {
                    if (BaseCls.GlbDefChannel == "AUTO_DEL")
                    {
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                  
                        //get permission
                        //bool _permission = CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11055);
                        //if (!_permission)
                        //{// Done By Nadeeka 27-may-2014
                           
                        //    _view.GlbReportName = "ConsignmentReceiptPrint.rpt";
                        //    BaseCls.GlbReportName = "ConsignmentReceiptPrint.rpt";
                        //}
                        //else
                        //{                         
                        //    _view.GlbReportName = "DealerReceiptPrint.rpt";
                        //    BaseCls.GlbReportName = "DealerReceiptPrint.rpt";
                        //}
                        // Commented by Nadeeka 21-05-2015 (Requested by Isuru)
                        _view.GlbReportName = "ReceiptPrints.rpt";
                        BaseCls.GlbReportName = "ReceiptPrints.rpt";

                        _view.GlbReportDoc = txtReqDocNo.Text;
                        _view.Show();
                        _view = null;
                    }
                    else
                    {
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        BaseCls.GlbReportTp = "REC";
                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                        _view.GlbReportName = "ReceiptPrints.rpt";
                        BaseCls.GlbReportName = "ReceiptPrints.rpt";
                        _view.GlbReportDoc = txtReqDocNo.Text;
                        _view.Show();
                        _view = null;
                    }

                }
                if (_reqDocType == "HPRS")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    BaseCls.GlbReportName = string.Empty;
                    BaseCls.GlbReportDoc = txtReqDocNo.Text; ;

                    clsHpSalesRep objHp = new clsHpSalesRep();
                    if (objHp.checkIsDirectPrint() == true)
                    {
                        objHp.HPRecPrint_Direct();
                    }
                    else
                    {
                        Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                        BaseCls.GlbReportName = "HPReceiptPrint.rpt";
                        _view.Show();
                        _view = null;
                    }
                }

                if (_reqDocType == "HPAGR")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;

                    BaseCls.GlbReportDoc = txtReqDocNo.Text;
                    BaseCls.GlbReportName = "HPAgreemnt_Print.rpt";

                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    _view.GlbReportName = "HP AGREEMENT PRINT";
                    _view.Show();
                    _view = null;

                }
                
                if (_reqDocType == "OUT" || _reqDocType == "IN")
                {
                    if (_reqDocType == "OUT") { BaseCls.GlbReportTp = "OUTWARD"; } else { BaseCls.GlbReportTp = "INWARD"; }//Sanjeewa
                    string doc_tp = CHNLSVC.Inventory.GetInvDocType(txtReqDocNo.Text);

                    if (_reqDocType == "OUT")
                    {
                        if (doc_tp == "DO")
                        {
                            if (BaseCls.GlbUserComCode == "AAL")
                            {
                                BaseCls.GlbReportTp = "OUTWARDDELCONF";
                            }
                            else if (BaseCls.GlbUserComCode == "AAAA")//Tharanga 2017/07/14 Add ABE compny
                            {
                                BaseCls.GlbReportTp = "OUTWARD_DO";
                            }
                            else
                            {
                                BaseCls.GlbReportTp = "OUTWARD";
                            }
                        }
                        else
                        {
                            BaseCls.GlbReportTp = "OUTWARD";
                        }
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        _view.GlbReportName = "Outward_Docs.rpt";
                        BaseCls.GlbReportName = "Outward_Docs.rpt";
                        _view.GlbReportDoc = txtReqDocNo.Text;
                        _view.Show();
                        _view = null;
                    }
                    else
                    {
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        if (BaseCls.GlbUserComCode == "AAL")
                        {
                            if (doc_tp == "GRN")
                            {
                                BaseCls.GlbReportTp = "GRN";
                                _view.GlbReportName = "GRNreport.rpt";
                                BaseCls.GlbReportName = "GRNreport.rpt";
                            }
                            else
                            {
                                //if (BaseCls.GlbUserComCode == "AAL" && BaseCls.GlbDefChannel == "MVEHI" || BaseCls.GlbDefChannel == "APTL") //tHARANGA 2017/06/28
                                //{
                                //    BaseCls.GlbReportTp = "INWARD";
                                //    _view.GlbReportName = "Inward_Docauto.rpt";
                                //    BaseCls.GlbReportName = "Inward_Docauto.rpt";
                                   
                                //}
                                //else
                                //{
                                    BaseCls.GlbReportTp = "INWARD";
                                    _view.GlbReportName = "Inward_Docs.rpt";
                                    BaseCls.GlbReportName = "Inward_Docs.rpt";
                                //}
                            }
                        }
                        else
                        {
                            if (doc_tp == "AOD" && BaseCls.GlbUserDefLoca == "ATMWH")
                            {
                                BaseCls.GlbReportTp = "AODOUTWARD";
                                _view.GlbReportName = "Inward_Docs.rpt";
                                BaseCls.GlbReportName = "Inward_Docs.rpt";
                            }
                            else
                            {
                                BaseCls.GlbReportTp = "INWARD";
                                _view.GlbReportName = "Inward_Docs.rpt";
                                BaseCls.GlbReportName = "Inward_Docs.rpt";
                            }
                          
                        }
                        //_view.GlbReportName = "Inward_Docs.rpt";
                        // BaseCls.GlbReportName = "Inward_Docs.rpt";
                        
                        _view.GlbReportDoc = txtReqDocNo.Text;
                        _view.Show();
                        _view = null;

                        if (BaseCls.GlbUserDefLoca=="ES001") // add by tharanga reprint SRN with amount
                        {
                            BaseCls.GlbReportTp = "ERN";
                            Reports.Inventory.ReportViewerInventory _viewNEW = new Reports.Inventory.ReportViewerInventory();
                            _viewNEW.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SExchange_Docs.rpt" : "Exchange_Docs.rpt";
                            BaseCls.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SExchange_Docs.rpt" : "Exchange_Docs.rpt";
                            BaseCls.GlbReportDoc = txtReqDocNo.Text;
                            _viewNEW.Show();
                            _viewNEW = null;
                        } 
                      
                       

                    }

                    //if (BaseCls.GlbUserComCode == "SGL")
                    //{
                    //    //TODO: NEED REPORT                        
                    //    if (_reqDocType == "OUT")
                    //    {
                    //        BaseCls.GlbReportName = string.Empty;
                    //        GlbReportName = string.Empty;
                    //        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    //        _view.GlbReportName = "Outward_Docs.rpt";
                    //        BaseCls.GlbReportName = "Outward_Docs.rpt";
                    //        _view.GlbReportDoc = txtReqDocNo.Text;
                    //        _view.Show();
                    //        _view = null;
                    //    }
                    //    if (_reqDocType == "IN")
                    //    {
                    //        BaseCls.GlbReportName = string.Empty;
                    //        GlbReportName = string.Empty;
                    //        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    //        _view.GlbReportName = "Inward_Docs.rpt";
                    //        BaseCls.GlbReportName = "Inward_Docs.rpt";
                    //        _view.GlbReportDoc = txtReqDocNo.Text;
                    //        _view.Show();
                    //        _view = null;
                    //    }
                    //}
                    //else if (BaseCls.GlbDefChannel == "AUTO_DEL")
                    //{
                    //    //TODO: NEED REPORT
                    //    if (_reqDocType == "OUT")
                    //    {
                    //        BaseCls.GlbReportTp = "OUTWARDDELCONF";
                    //        BaseCls.GlbReportName = string.Empty;
                    //        GlbReportName = string.Empty;
                    //        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    //        _view.GlbReportName = "Dealer_Outward_Docs.rpt";
                    //        BaseCls.GlbReportName = "Dealer_Outward_Docs.rpt";
                    //        _view.GlbReportDoc = txtReqDocNo.Text;
                    //        _view.Show();
                    //        _view = null;
                    //    }
                    //    if (_reqDocType == "IN")
                    //    {
                    //        BaseCls.GlbReportName = string.Empty;
                    //        GlbReportName = string.Empty;
                    //        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    //        _view.GlbReportName = "Dealer_Inward_Docs.rpt";
                    //        BaseCls.GlbReportName = "Dealer_Inward_Docs.rpt";
                    //        _view.GlbReportDoc = txtReqDocNo.Text;
                    //        _view.Show();
                    //        _view = null;
                    //    }
                    //}
                    //else
                    //{
                    //    if (_reqDocType == "OUT")
                    //    {
                    //        BaseCls.GlbReportName = string.Empty;
                    //        GlbReportName = string.Empty;
                    //        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    //        _view.GlbReportName = "Outward_Docs.rpt";
                    //        BaseCls.GlbReportName = "Outward_Docs.rpt";
                    //        _view.GlbReportDoc = txtReqDocNo.Text;
                    //        _view.Show();
                    //        _view = null;

                    //        if (BaseCls.GlbUserComCode == "AAL")
                    //        {
                    //            BaseCls.GlbReportTp = "OUTWARDDELCONF";

                    //            BaseCls.GlbReportName = string.Empty;
                    //            GlbReportName = string.Empty;
                    //            Reports.Inventory.ReportViewerInventory _view1 = new Reports.Inventory.ReportViewerInventory();
                    //            _view1.GlbReportName = "Outward_Docs_Del_Conf.rpt";
                    //            BaseCls.GlbReportName = "Outward_Docs_Del_Conf.rpt";
                    //            _view1.GlbReportDoc = txtReqDocNo.Text;
                    //            _view1.Show();
                    //            _view1 = null;
                    //        }
                    //    }
                    //    if (_reqDocType == "IN")
                    //    {
                    //        BaseCls.GlbReportName = string.Empty;
                    //        GlbReportName = string.Empty;
                    //        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    //        _view.GlbReportName = "Inward_Docs.rpt";
                    //        BaseCls.GlbReportName = "Inward_Docs.rpt";
                    //        _view.GlbReportDoc = txtReqDocNo.Text;
                    //        _view.Show();
                    //        _view = null;
                    //    }
                    //}
                }
                if (_reqDocType == "WAREXT" || _reqDocType == "FGAP")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    BaseCls.GlbReportTp = "REC";
                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    _view.GlbReportName = "ReceiptPrints.rpt";
                    BaseCls.GlbReportName = "ReceiptPrints.rpt";
                    _view.GlbReportDoc = txtReqDocNo.Text;
                    _view.Show();
                    _view = null;
                }
                //Estimate section Added By Udesh - 08-Oct-2018
                if (_reqDocType == "Estimate")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    FF.WindowsERPClient.Reports.Service.ReportViewerSVC _viewsvc = new FF.WindowsERPClient.Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportTp = "EST";
                    _viewsvc.GlbReportName = "Job_Estimate.rpt";
                    BaseCls.GlbReportDoc = txtReqDocNo.Text.Trim();
                    _viewsvc.Show();
                    _viewsvc = null;
                }
                if (_reqDocType == "MRN" || _reqDocType == "INTR")
                {
                    if (BaseCls.GlbUserComCode == "SGL")
                    {
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        _view.GlbReportName = "SMRNRepPrints.rpt";
                        BaseCls.GlbReportName = "SMRNRepPrints.rpt";
                        _view.GlbReportDoc = txtReqDocNo.Text;
                        _view.Show();
                        _view = null;
                    }
                    else if (BaseCls.GlbDefChannel == "AUTO_DEL")
                    {
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        _view.GlbReportName = "Dealer_MRNRepPrints.rpt";
                        BaseCls.GlbReportName = "Dealer_MRNRepPrints.rpt";
                        _view.GlbReportDoc = txtReqDocNo.Text;
                        _view.Show();
                        _view = null;
                    }
                    else
                    {
                        DataTable _dt = CHNLSVC.Inventory.GetJobNoByReqNo(txtReqDocNo.Text);
                        if (_dt.Rows.Count > 0)
                            BaseCls.GlbReportJobNo = _dt.Rows[0]["itr_job_no"].ToString();

                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        _view.GlbReportName = "MRNRepPrints.rpt";
                        BaseCls.GlbReportName = "MRNRepPrints.rpt";
                        _view.GlbReportDoc = txtReqDocNo.Text;
                        _view.Show();
                        _view = null;
                    }
                }
                if (_reqDocType == "RCC")
                {
                    BaseCls.GlbReportTp = "RCC";
                    if (BaseCls.GlbUserComCode == "SGL")
                    {
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                        _view.GlbReportName = "SRCCPrint_New.rpt";
                        BaseCls.GlbReportName = "SRCCPrint_New.rpt";
                        _view.GlbReportDoc = txtReqDocNo.Text;
                        BaseCls.GlbReportDoc = txtReqDocNo.Text;
                        _view.Show();
                        _view = null;
                    }
                    else
                    {
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                        _view.GlbReportName = "RCCPrint_New.rpt";
                        BaseCls.GlbReportName = "RCCPrint_New.rpt";
                        _view.GlbReportDoc = txtReqDocNo.Text;
                        BaseCls.GlbReportDoc = txtReqDocNo.Text;
                        _view.Show();
                        _view = null;
                    }
                }
                if (_reqDocType == "RCCACK")
                {
                    BaseCls.GlbReportTp = "RCCACK";
                    if (BaseCls.GlbUserComCode == "SGL")
                    {
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                        _view.GlbReportName = "SRCCPrint_Ack.rpt";
                        BaseCls.GlbReportName = "SRCCPrint_Ack.rpt";
                        _view.GlbReportDoc = txtReqDocNo.Text;
                        BaseCls.GlbReportDoc = txtReqDocNo.Text;
                        _view.Show();
                        _view = null;
                    }
                    else
                    {
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                        _view.GlbReportName = "RCCPrint_Ack.rpt";
                        BaseCls.GlbReportName = "RCCPrint_Ack.rpt";
                        _view.GlbReportDoc = txtReqDocNo.Text;
                        BaseCls.GlbReportDoc = txtReqDocNo.Text;
                        _view.Show();
                        _view = null;
                    }
                }
                if (_reqDocType == "FIXEDASS")
                {
                    if (BaseCls.GlbUserComCode == "SGL")
                    {
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        _view.GlbReportName = "SFixedAssetTransferNotes.rpt";
                        BaseCls.GlbReportName = "SFixedAssetTransferNotes.rpt";
                        _view.GlbReportDoc = txtReqDocNo.Text;
                        _view.Show();
                        _view = null;
                    }
                    else
                    {
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        _view.GlbReportName = "FixedAssetTransferNotes.rpt";
                        BaseCls.GlbReportName = "FixedAssetTransferNotes.rpt";
                        _view.GlbReportDoc = txtReqDocNo.Text;
                        _view.Show();
                        _view = null;
                    }
                }
                if (_reqDocType == "GRAN")
                {
                    BaseCls.GlbReportTp = "GRAN";
                    if (BaseCls.GlbUserComCode == "SGL")
                    {
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        _view.GlbReportName = "SGRANPrints.rpt";
                        BaseCls.GlbReportName = "SGRANPrints.rpt";
                        _view.GlbReportDoc = txtReqDocNo.Text;
                        _view.Show();
                        _view = null;
                    }
                    else
                    {
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        _view.GlbReportName = "GRANPrints.rpt";
                        BaseCls.GlbReportName = "GRANPrints.rpt";
                        _view.GlbReportDoc = txtReqDocNo.Text;
                        _view.Show();
                        _view = null;
                    }
                }

                if (_reqDocType == "DIN")
                {
                    BaseCls.GlbReportTp = "DIN";
                    if (BaseCls.GlbUserComCode == "SGL")
                    {
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        _view.GlbReportName = "SDINPrints.rpt";
                        BaseCls.GlbReportName = "SDINPrints.rpt";
                        _view.GlbReportDoc = txtReqDocNo.Text;
                        _view.Show();
                        _view = null;
                    }
                    else if (BaseCls.GlbDefChannel == "AUTO_DEL")
                    {
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        _view.GlbReportName = "Dealer_DINPrints.rpt";
                        BaseCls.GlbReportName = "Dealer_DINPrints.rpt";
                        _view.GlbReportDoc = txtReqDocNo.Text;
                        _view.Show();
                        _view = null;
                    }
                    else
                    {
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        _view.GlbReportName = "DINPrints.rpt";
                        BaseCls.GlbReportName = "DINPrints.rpt";
                        _view.GlbReportDoc = txtReqDocNo.Text;
                        _view.Show();
                        _view = null;
                    }
                }
                if (_reqDocType == "VEHJOB")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportCompCode = BaseCls.GlbUserComCode;
                    BaseCls.GlbReportName = "ServiceJobCard.rpt";
                    BaseCls.GlbReportDoc = txtReqDocNo.Text;
                    _view.Show();
                    _view = null;
                }
                if (_reqDocType == "ACSER")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    FF.BusinessObjects.RecieptHeader _rec = CHNLSVC.Sales.GetAcJobReciept(txtReqDocNo.Text, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = (BaseCls.GlbUserComCode == "SGL") ? "SServiceReceiptPrints.rpt" : "ServiceReceiptPrints.rpt";
                    BaseCls.GlbReportDoc = _rec.Sar_receipt_no;
                    _view.Show();
                    _view = null;
                }
                if (_reqDocType == "COVER")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    string _insComp = "";

                    List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetInsuranceByRef( txtReqDocNo.Text);
                    _insComp = _vehins[0].Svit_ins_com;

                    if (_insComp == "CN001")
                    {
                        BaseCls.GlbReportName = "InsuranceCoverNote.rpt";
                    }
                    if (_insComp == "MBSL")
                    {
                        BaseCls.GlbReportName = "InsuranceCoverNoteMBSL.rpt";
                    }
                    if (_insComp == "UAL01")
                    {
                        BaseCls.GlbReportName = "InsuranceCoverNoteUMS.rpt";
                    }
                    if (_insComp == "JS001")
                    {
                        BaseCls.GlbReportName = "InsuranceCoverNoteJS.rpt";
                    }
                    if (_insComp == "AIA")
                    {
                        BaseCls.GlbReportName = "InsuranceCoverNoteAIA.rpt";
                    }
                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    //BaseCls.GlbReportName = "InsuranceCoverNote.rpt";
                    BaseCls.GlbReportDoc = txtReqDocNo.Text;
                    _view.Show();
                    _view = null;
                }
                if (_reqDocType == "RVT")
                {
                    BaseCls.GlbReportTp = "RVT";
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    _view.GlbReportName = "RevertSRN.rpt";
                    BaseCls.GlbReportName = "RevertSRN.rpt";
                    _view.GlbReportDoc = txtReqDocNo.Text;
                    BaseCls.GlbReportDoc = txtReqDocNo.Text;
                    _view.Show();
                    _view = null;

                }

                //Tharindu 2017-11-17
                if (_reqDocType == "AODACK")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();

                    BaseCls.GlbReportTp = "AODACK";
                    _view.GlbReportName = "Inward_Docs_ack.rpt";
                    BaseCls.GlbReportName = "Inward_Docs_ack.rpt";
                    _view.GlbReportDoc = txtReqDocNo.Text;
                    _view.Show();
                    _view = null;
                }

                BindRequestedDocDetailsGridData(BaseCls.GlbUserDefLoca, "ALL", dateTimePickerFromDate.Value.Date, dateTimePickerToDate.Value.Date);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while processing\n" + ex.Message + "\n" + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        private void Reject()
        {
            if (txtDocNoApp.Text == "")
            {
                MessageBox.Show("Please select a Document!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int X = CHNLSVC.General.UpdateReprintApproval(txtDocNoApp.Text, "R", BaseCls.GlbUserName);
                string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                BindAllDocDetailsGridData(_masterLocation, _status, dateTimePickerAppFrom.Value.Date, dateTimePickerAppTo.Value.Date);

                txtDocNoApp.Text = "";
                txtDocDateApp.Text = "";
                MessageBox.Show("Successfully Rejected!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        private void Cancel()
        {
            if (txtDocNo.Text == "")
            {
                MessageBox.Show("Please select a Document!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int X = CHNLSVC.General.UpdateReprintApproval(txtDocNo.Text, "C", BaseCls.GlbUserName);
                string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                BindAllDocDetailsGridData(_masterLocation, _status, dateTimePickerAppFrom.Value.Date, dateTimePickerAppTo.Value.Date);

                txtDocNoApp.Text = "";
                txtDocDateApp.Text = "";
                MessageBox.Show("Successfully Cancelled!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        private void Approve()
        {
            if (txtDocNoApp.Text == "")
            {
                MessageBox.Show("Please select a Document!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                int X = CHNLSVC.General.UpdateReprintApproval(txtDocNoApp.Text, "A", BaseCls.GlbUserName);
                string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                BindAllDocDetailsGridData(_masterLocation, _status, dateTimePickerAppFrom.Value.Date, dateTimePickerAppTo.Value.Date);

                txtDocNoApp.Text = "";
                txtDocDateApp.Text = "";
                MessageBox.Show("Successfully Approved!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        private void ClearControls()
        {

            if (tabControl1.SelectedTab == tabPage1)
            {
                //radio buttons
                opt1.Checked = false;
                opt2.Checked = false;
                opt3.Checked = false;
                opt4.Checked = false;
                opt5.Checked = false;
                opt6.Checked = false;
                opt7.Checked = false;
                opt8.Checked = false;
                opt9.Checked = false;
                opt10.Checked = false;
                opt11.Checked = false;
                opt12.Checked = false;
                opt13.Checked = false;
                opt14.Checked = false;
                opt15.Checked = false;
                opt16.Checked = false;
                opt17.Checked = false;
                opt18.Checked = false;
                opt19.Checked = false;
                rdo20.Checked = false;
                opt37.Checked = false;
                opt38.Checked = false;// Added By Udesh - 08-Oct-2018
                chkLocAll.Checked = false;

                //date picker
                dateTimePickerFromDate.Value = DateTime.Now.Date;
                dateTimePickerToDate.Value = DateTime.Now.Date;

                //gridviews
                gvDocs.DataSource = null;
                gvReqDocs.DataSource = null;

                //text boxes
                txtDocDate.Text = "";
                txtDocNo.Text = "";
                txtReason.Text = "";

                txtReqDocNo.Text = "";
                txtReqDocDate.Text = "";

                _docType = "";
            }
            else
            {
                //date picker
                dateTimePickerAppFrom.Value = DateTime.Now.Date;
                dateTimePickerAppTo.Value = DateTime.Now.Date;

                //radio button
                optALL.Checked = false;
                optPending.Checked = false;
                optRej.Checked = false;
                optApp.Checked = false;

                gvAllDocs.DataSource = null;

                //text box
                txtDocDateApp.Text = "";
                txtDocNoApp.Text = "";
                txtLocationSearch.Text = "";
                _status = "";
            }
            CheckPermission();
        }

        private void UpdateStatus(string docNo)
        {
            try
            {
                int X = CHNLSVC.General.UpdateReprintApproval(docNo, "A", BaseCls.GlbUserName);

                MessageBox.Show("Successfully Approved!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        private void SavePrint(string docNo, string _type)
        {
            try
            {
                int X = CHNLSVC.General.UpdatePrintStatus(docNo);
                BaseCls.GlbReportName = string.Empty;
                GlbReportName = string.Empty;
                if (_type == "CS" || _type == "CSREV" || _type == "HP" || _type == "HPREV" || _type == "CR" || _type == "CRREV" || _type == "POCA" || _type == "POCR")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    if (BaseCls.GlbUserComCode == "SGL")
                    {

                        //TODO: NEED REPORT
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                        BaseCls.GlbReportTp = "INV";
                        _view.GlbReportName = "InvoiceHalfPrints.rpt";
                        BaseCls.GlbReportName = "InvoiceHalfPrints.rpt";
                        _view.GlbReportDoc = docNo;
                        _view.Show();
                        _view = null;
                    }
                    else
                    {
                        //Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                        //_view.GlbReportName = "InvoiceHalfPrints.rpt";
                        //_view.GlbReportDoc = docNo;
                        //_view.Show();
                        //_view = null;

                        bool _isPrintElite = false;
                        InvoiceHeader _inv = CHNLSVC.Sales.GetInvoiceHeaderDetails(docNo);
                        if (_inv == null)
                        {
                            MessageBox.Show("Invoice print invoice null error");
                        }
                        MasterBusinessEntity _itm = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, _inv.Sah_cus_cd, string.Empty, string.Empty, "C");
                        if (_itm == null)
                        {
                            MessageBox.Show("Invoice print customer null error");
                        }
                        //get permission
                        bool _permission = CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11055);
                        MasterProfitCenter _MasterProfitCenter = CacheLayer.Get<MasterProfitCenter>(CacheLayer.Key.ProfitCenter.ToString());
                        { BaseCls.GlbReportTp = "INV";
                        if (_MasterProfitCenter.Mpc_chnl.Trim() == "ELITE" || _MasterProfitCenter.Mpc_chnl.Trim() == "RRC1" || _MasterProfitCenter.Mpc_chnl.Trim() == "RRE2" || _MasterProfitCenter.Mpc_chnl.Trim() == "APPLE" || _MasterProfitCenter.Mpc_chnl.Trim() == "APPIST") 
                            {
                                BaseCls.GlbReportDoc = docNo;
                                clsSalesRep objSales = new clsSalesRep();
                                if (objSales.checkIsDirectPrint() == true && objSales.removeIsDirectPrint() == false && _inv.Sah_is_svat == false)
                                {
                                    objSales.InvoicePrint_Direct();
                                    _isPrintElite = true;
                                }
                                else
                                {
                                    ReportViewer _view = new ReportViewer();
                                    BaseCls.GlbReportName = string.Empty;
                                    GlbReportName = string.Empty;
                                    _view.GlbReportName = string.Empty;
                                    _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "InvoiceHalfPrints.rpt" : BaseCls.GlbUserComCode == "GCL" ? "InvoicePrints_Gold.rpt" : "InvoiceHalfPrints.rpt";
                                    _view.GlbReportDoc = docNo;
                                    BaseCls.GlbReportDoc = txtReqDocNo.Text;
                                    _view.Show(); _view = null;
                                    _isPrintElite = true;
                                }
                            } 
                        }
                        if (!_permission)
                        {
                            //AUTO_DEL
                          //  { if (_MasterProfitCenter.Mpc_chnl.Trim() == "AUTO_DEL") { ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _view.GlbReportName = "DealerInvoicePrints.rpt"; BaseCls.GlbReportName = "DealerInvoicePrints.rpt"; _view.GlbReportDoc = docNo; _view.Show(); _view = null; _isPrintElite = true; } }
                            if (_MasterProfitCenter.Mpc_chnl.Trim() == "AUTO_DEL")
                            {
                                if (_reqDocType == "CR")
                                {
                                    ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _view.GlbReportName = "DealerCreditInvoicePrints.rpt"; BaseCls.GlbReportName = "DealerCreditInvoicePrints.rpt"; _view.GlbReportDoc = docNo; BaseCls.GlbReportDoc = docNo; //DealerCreditInvoicePrints
                                    _view.Show(); _view = null; _isPrintElite = true;
                                }
                                else
                                {
                                    ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; _view.GlbReportName = "InvoiceHalfPrints.rpt"; BaseCls.GlbReportName = "InvoiceHalfPrints.rpt"; _view.GlbReportDoc = docNo; BaseCls.GlbReportDoc = docNo;
                                    _view.Show(); _view = null; _isPrintElite = true;
                                }
                            }
                           
                        }
                        if (!_isPrintElite)
                        {
                            if (_itm.Mbe_sub_tp != "C.") //updated by akila 2017/11/29
                            {
                                if (_itm.Mbe_cate == "LEASE")
                                {
                                    ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; BaseCls.GlbReportTp = "INV"; _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "InvoiceHalfPrints.rpt" : BaseCls.GlbUserComCode == "GCL" ? "InvoicePrints_Gold.rpt" : BaseCls.GlbUserComCode == "AST" ? "InvoicePrint_AST.rpt" : "InvoicePrintTax.rpt"; _view.GlbReportDoc = docNo; _view.Show(); _view = null;
                                    if (_itm.Mbe_cate == "LEASE") { ReportViewer _viewt = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _viewt.GlbReportName = string.Empty; _viewt.GlbReportName = "InvoicePrint_insus.rpt"; _viewt.GlbReportDoc = docNo; _viewt.Show(); _viewt = null; }
                                }
                                else
                                {
                                    if (_inv.Sah_tax_inv == false)
                                    { ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; BaseCls.GlbReportTp = "INV"; _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "InvoiceHalfPrints.rpt" : BaseCls.GlbUserComCode == "GCL" ? "InvoicePrints_Gold.rpt" : "InvoiceHalfPrints.rpt"; _view.GlbReportDoc = docNo; _view.Show(); _view = null; }
                                    else
                                    {
                                        ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; BaseCls.GlbReportTp = "INV"; _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "InvoiceHalfPrints.rpt" : BaseCls.GlbUserComCode == "GCL" ? "InvoicePrints_Gold.rpt" : BaseCls.GlbUserComCode == "AST" ? "InvoicePrint_AST.rpt" : "InvoiceHalfPrints.rpt"; _view.GlbReportDoc = docNo; _view.Show(); _view = null;
                                        if (_itm.Mbe_cate == "LEASE") { ReportViewer _viewt = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _viewt.GlbReportName = string.Empty; _viewt.GlbReportName = "InvoicePrintTax_insus.rpt"; _viewt.GlbReportDoc = docNo; _viewt.Show(); _viewt = null; }
                                    }
                                }
                            }
                            else
                            {
                                //Dealer
                                ReportViewer _view = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _view.GlbReportName = string.Empty; BaseCls.GlbReportTp = "INV"; _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "InvoiceHalfPrints.rpt" : BaseCls.GlbUserComCode == "GCL" ? "InvoicePrints_Gold.rpt" : BaseCls.GlbUserComCode == "AST" ? "InvoicePrint_AST.rpt" : "InvoicePrintTax.rpt"; _view.GlbReportDoc = docNo; _view.Show(); _view = null;
                                if (_itm.Mbe_cate == "LEASE") { ReportViewer _viewt = new ReportViewer(); BaseCls.GlbReportName = string.Empty; GlbReportName = string.Empty; _viewt.GlbReportName = string.Empty; _viewt.GlbReportName = "InvoicePrint_insus.rpt"; _viewt.GlbReportDoc = docNo; _viewt.Show(); _viewt = null; }
                            }

                            //if (_itm.Mbe_sub_tp != "C.")
                            //{


                            //    //Showroom
                            //    //========================= INVOCIE  CASH/CREDIT/ HIRE 
                            //    if (_inv.Sah_tax_inv == false)
                            //    {
                            //        //if (BaseCls.GlbUserComCode == "AAL" && BaseCls.GlbDefChannel == "MVEHI" || BaseCls.GlbDefChannel == "APTL") //tHARANGA 2017/06/28
                            //        //{
                            //        //    ReportViewer _view = new ReportViewer();
                            //        //    _view.GlbReportName = "InvoiceAuto.rpt";
                            //        //    BaseCls.GlbReportTp = "INV";
                            //        //    _view.GlbReportDoc = docNo;
                            //        //    _view.GlbSerial = null;
                            //        //    _view.GlbWarranty = null;
                            //        //    _view.Show();
                            //        //    _view = null;

                            //        //}
                            //        //else
                            //        //{
                            //        BaseCls.GlbReportName = string.Empty;
                            //        GlbReportName = string.Empty;
                            //        ReportViewer _view = new ReportViewer();
                            //        BaseCls.GlbReportTp = "INV";
                            //        _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "InvoiceHalfPrints.rpt" : BaseCls.GlbUserComCode == "GCL" ? "InvoicePrints_Gold.rpt" : "InvoiceHalfPrints.rpt";
                            //        _view.GlbReportDoc = docNo;
                            //        _view.Show();
                            //        _view = null;
                            //        //}
                            //    }
                            //    else
                            //    {
                            //        //Add Code by Chamal 27/04/2013
                            //        //====================  TAX INVOICE
                            //        BaseCls.GlbReportName = string.Empty;
                            //        GlbReportName = string.Empty;
                            //        ReportViewer _view = new ReportViewer();
                            //        _view.GlbReportName = "InvoicePrintTax.rpt";
                            //        _view.GlbReportDoc = docNo;
                            //        _view.Show();
                            //        _view = null;

                            //        if (_itm.Mbe_cate == "LEASE")
                            //        {
                            //            BaseCls.GlbReportName = string.Empty;
                            //            GlbReportName = string.Empty;
                            //            ReportViewer _viewt = new ReportViewer();
                            //            _viewt.GlbReportName = "InvoicePrintTax_insus.rpt";
                            //            _viewt.GlbReportDoc = docNo;
                            //            _viewt.Show();
                            //            _viewt = null;
                            //        }

                            //        //====================  TAX INVOICE

                            //    }
                            //}
                            //else
                            //{


                            //    //Dealer
                            //    //if (BaseCls.GlbUserComCode == "AAL" && BaseCls.GlbDefChannel == "MVEHI" || BaseCls.GlbDefChannel == "APTL") //tHARANGA 2017/06/28
                            //    //{
                            //    //    ReportViewer _view = new ReportViewer();
                            //    //    _view.GlbReportName = "InvoiceAuto.rpt";
                            //    //    BaseCls.GlbReportTp = "INV";
                            //    //    _view.GlbReportDoc = docNo;
                            //    //    _view.GlbSerial = null;
                            //    //    _view.GlbWarranty = null;
                            //    //    _view.Show();
                            //    //    _view = null;

                            //    //}
                            //    //else
                            //    //{
                            //    BaseCls.GlbReportName = string.Empty;
                            //    GlbReportName = string.Empty;
                            //    ReportViewer _view = new ReportViewer();
                            //    _view.GlbReportName = "InvoicePrints.rpt";
                            //    _view.GlbReportDoc = docNo;
                            //    _view.Show();
                            //    _view = null;
                            //    //  }
                            //    List<FF.BusinessObjects.RecieptItem> _itms = CHNLSVC.Sales.GetReceiptItemList(docNo);
                            //    if (_itms == null || _itms.Count <= 0)
                            //    {
                            //        MessageBox.Show("Invoice print reciept itms null error");
                            //    }
                            //    if (_itms != null)
                            //        if (_itms.Count > 0)
                            //            if (_itm.Mbe_cate == "LEASE")
                            //            {
                            //                BaseCls.GlbReportName = string.Empty;
                            //                GlbReportName = string.Empty;
                            //                ReportViewer _viewt = new ReportViewer();
                            //                _viewt.GlbReportName = "InvoicePrint_insus.rpt";
                            //                _viewt.GlbReportDoc = docNo;
                            //                _viewt.Show();
                            //                _viewt = null;

                            //            }
                            //}
                        }
                    }

                    if (_reqDocType == "CS" || _reqDocType == "HP" || _reqDocType == "CR")
                    {
                        //Tharanga 2017/06/16
                        string invNo = docNo;
                        DataTable odt = new DataTable();
                        odt = CHNLSVC.Sales.get_sar_provou_tp(BaseCls.GlbUserComCode, invNo);
                        if (odt.Rows.Count > 0)
                        {
                            ReportViewer _view1 = new ReportViewer();
                            BaseCls.GlbReportName = string.Empty;
                            _view1.GlbReportName = string.Empty;
                            BaseCls.GlbReportTp = "Print voucher separately";
                            _view1.GlbReportName = "giftvoucher.rpt";
                            BaseCls.GlbReportName = "giftvoucher.rpt";
                            _view1.GlbReportDoc = docNo;
                            _view1.GlbReportProfit = BaseCls.GlbUserDefProf;
                            _view1.Show();
                            _view1 = null;
                        }
                    }
                }

                if (_type == "CRAFSL")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    BaseCls.GlbReportTp = "CRAFSL";
                    ReportViewer _insu = new ReportViewer();
                    _insu.GlbReportName = "DealerCreditInvoicePrints_AFSL.rpt";
                    _insu.GlbReportDoc = docNo;
                    _insu.Show();
                    _insu = null;
                }
                if (_type == "DIR")
                {
                    BaseCls.GlbReportDoc = docNo;
                    clsHpSalesRep objHp = new clsHpSalesRep();
                    clsSalesRep objSales = new clsSalesRep();
                    if (objSales.checkIsDirectPrint() == true && objSales.removeIsDirectPrint() == false)
                    {
                        objHp.InsurancePrint_Direct();
                    }
                    else
                    {
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        BaseCls.GlbReportTp = "INSUR";
                        ReportViewer _insu = new ReportViewer();
                        _insu.GlbReportName = "InsurancePrint.rpt";
                        _insu.GlbReportDoc = docNo;
                        _insu.Show();
                        _insu = null;
                    }
                }
                if (_type == "QUO")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    BaseCls.GlbReportDoc = docNo;
                    BaseCls.GlbReportName = "Quotation_RepPrint.rpt";

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    _view.GlbReportName = "QUOTATION";
                    _view.Show();
                    _view = null;
                }
                if (_reqDocType == "HPRS")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    BaseCls.GlbReportName = string.Empty;
                    BaseCls.GlbReportDoc = docNo; ;

                    clsHpSalesRep objHp = new clsHpSalesRep();
                    if (objHp.checkIsDirectPrint() == true)
                    {
                        objHp.HPRecPrint_Direct();
                    }
                    else
                    {
                        Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                        BaseCls.GlbReportName = "HPReceiptPrint.rpt";
                        _view.Show();
                        _view = null;
                    }
                }
                if (_type == "DFINV")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    ReportViewer _view = new ReportViewer();
                    if (BaseCls.GlbUserComCode == "SGL")
                    {
                        BaseCls.GlbReportName = "sInvoiceDutyFree.rpt";//Add by Chamal 04-Mar-2014
                    }
                    else
                    {
                        BaseCls.GlbReportName = "InvoiceDutyFree.rpt";
                    }
                    _view.GlbReportDoc = docNo;

                    _view.Show();
                    _view = null;
                }
                if (_type == "ADVREC")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    BaseCls.GlbReportTp = "ADVREC";
                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    _view.GlbReportName = "ReceiptPrints_n.rpt";
                    BaseCls.GlbReportName = "ReceiptPrints_n.rpt";
                    _view.GlbReportDoc = docNo;
                    _view.Show();
                    _view = null;
                }
                if (_type == "SMTINS")  //kapila 18/4/2015
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    //  BaseCls.GlbReportTp = "REC";
                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    _view.GlbReportName = "ReceiptPrints.rpt";
                    BaseCls.GlbReportName = "ReceiptPrints.rpt";
                    _view.GlbReportDoc = txtReqDocNo.Text;
                    _view.Show();
                    _view = null;
                }

                if (_type == "DEBT")
                {
                    RecieptHeader _rec = CHNLSVC.Sales.GetReceiptHeader(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, docNo);
                    if (_rec == null)
                    {
                        MessageBox.Show("Debt print reciept null error");
                    }
                    MasterBusinessEntity _itm = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, _rec.Sar_debtor_cd, string.Empty, string.Empty, "C");
                    if (_itm == null)
                    {
                        MessageBox.Show("Debt print customer null error");
                    }
                    if (_itm.Mbe_sub_tp != "C.")
                    {
                        //Showroom
                        //========================= INVOCIE  CASH/CREDIT/ HIRE 
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        BaseCls.GlbReportTp = "REC";
                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                        _view.GlbReportName = "ReceiptPrints.rpt";
                        BaseCls.GlbReportName = "ReceiptPrints.rpt";
                        _view.GlbReportDoc = docNo;
                        _view.Show();
                        _view = null;
                    }
                    else
                    {
                        //Dealer
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                        _view.GlbReportName = "ReceiptPrints.rpt";//ReceiptPrintDealers
                        BaseCls.GlbReportName = "ReceiptPrints.rpt";
                        _view.GlbReportDoc = docNo;
                        _view.Show();
                        _view = null;
                    }
                }

                if (_type == "REC")
                {
                    if (BaseCls.GlbDefChannel == "AUTO_DEL")
                    {
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                        _view.GlbReportName = "ReceiptPrints.rpt";//DealerReceiptPrint
                        BaseCls.GlbReportName = "ReceiptPrints.rpt";
                        _view.GlbReportDoc = docNo;
                        _view.Show();
                        _view = null;
                    }
                    else
                    {
                        BaseCls.GlbReportName = string.Empty;
                        GlbReportName = string.Empty;
                        BaseCls.GlbReportTp = "REC";
                        Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                        _view.GlbReportName = "ReceiptPrints.rpt";
                        BaseCls.GlbReportName = "ReceiptPrints.rpt";
                        _view.GlbReportDoc = docNo;
                        _view.Show();
                        _view = null;
                    }
                }

                if (_type == "EXC")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    BaseCls.GlbReportTp = "ERN";
                    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "Exchange_Docs.rpt" : "Exchange_Docs.rpt";
                    BaseCls.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "Exchange_Docs.rpt" : "Exchange_Docs.rpt";
                    BaseCls.GlbReportDoc = docNo;
                    _view.Show();
                    _view = null;

                }

                if (_type == "HPAGR")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;


                    BaseCls.GlbReportDoc = docNo;
                    BaseCls.GlbReportName = "HPAgreemnt_Print.rpt";

                    Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                    _view.GlbReportName = "HP AGREEMENT PRINT";
                    _view.Show();
                    _view = null;

                }

                if (_type == "OUT" || _type == "IN")
                {
                    if (_reqDocType == "OUT") { BaseCls.GlbReportTp = "OUTWARD"; } else { BaseCls.GlbReportTp = "INWARD"; }//Sanjeewa
                    if (BaseCls.GlbUserComCode == "SGL")
                    {
                        //TODO: NEED REPORT
                        if (_type == "OUT")
                        {
                            BaseCls.GlbReportName = string.Empty;
                            GlbReportName = string.Empty;
                            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                            _view.GlbReportName = "Outward_Docs.rpt";
                            BaseCls.GlbReportName = "Outward_Docs.rpt";
                            _view.GlbReportDoc = docNo;
                            _view.Show();
                            _view = null;
                        }
                        if (_type == "IN")
                        {
                            BaseCls.GlbReportName = string.Empty;
                            GlbReportName = string.Empty;
                            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                            if (BaseCls.GlbUserComCode == "AAL")
                            {
                                if (_type == "GRN")
                                {
                                    BaseCls.GlbReportTp = "GRN";
                                    _view.GlbReportName = "GRNreport.rpt";
                                    BaseCls.GlbReportName = "GRNreport.rpt";
                                }
                                else
                                {
                                    BaseCls.GlbReportTp = "INWARD";
                                    _view.GlbReportName = "Inward_Docs.rpt";
                                    BaseCls.GlbReportName = "Inward_Docs.rpt";
                                }
                            }
                            BaseCls.GlbReportTp = "INWARD";
                            _view.GlbReportName = "Inward_Docs.rpt";
                            BaseCls.GlbReportName = "Inward_Docs.rpt";
                           
                            _view.GlbReportDoc = docNo;
                            _view.Show();
                            _view = null;
                        }
                    }
                    else if (BaseCls.GlbDefChannel == "AUTO_DEL")
                    {
                        //TODO: NEED REPORT
                        if (_type == "OUT")
                        {
                            BaseCls.GlbReportName = string.Empty;
                            GlbReportName = string.Empty;
                            BaseCls.GlbReportTp = "OUTWARDDELCONF";
                            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                            _view.GlbReportName = "Dealer_Outward_Docs.rpt";
                            BaseCls.GlbReportName = "Dealer_Outward_Docs.rpt";
                            _view.GlbReportDoc = docNo;
                            _view.Show();
                            _view = null;
                        }
                        if (_type == "IN")
                        {
                            BaseCls.GlbReportName = string.Empty;
                            GlbReportName = string.Empty;
                            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                            _view.GlbReportName = "Dealer_Inward_Docs.rpt";
                            BaseCls.GlbReportName = "Dealer_Inward_Docs.rpt";
                            _view.GlbReportDoc = docNo;
                            _view.Show();
                            _view = null;
                        }
                    }
                    else
                    {
                        if (_type == "OUT")
                        {
                            BaseCls.GlbReportName = string.Empty;
                            GlbReportName = string.Empty;
                            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                            _view.GlbReportName = "Outward_Docs.rpt";
                            BaseCls.GlbReportName = "Outward_Docs.rpt";
                            _view.GlbReportDoc = docNo;
                            _view.Show();
                            _view = null;
                            if (BaseCls.GlbUserComCode == "AAL")
                            {
                                BaseCls.GlbReportTp = "OUTWARDDELCONF";

                                BaseCls.GlbReportName = string.Empty;
                                GlbReportName = string.Empty;
                                Reports.Inventory.ReportViewerInventory _view1 = new Reports.Inventory.ReportViewerInventory();
                                _view1.GlbReportName = "Outward_Docs_Del_Conf.rpt";
                                BaseCls.GlbReportName = "Outward_Docs_Del_Conf.rpt";
                                _view1.GlbReportDoc = txtReqDocNo.Text;
                                _view1.Show();
                                _view1 = null;
                            }
                        }
                        if (_type == "IN")
                        {
                            Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                            //if (BaseCls.GlbUserComCode == "AAL" && BaseCls.GlbDefChannel == "MVEHI" || BaseCls.GlbDefChannel == "APTL") //tHARANGA 2017/06/28
                            //{
                            //    Reports.Inventory.ReportViewerInventory _insu = new Reports.Inventory.ReportViewerInventory();
                            //    BaseCls.GlbReportName = string.Empty;
                            //    _insu.GlbReportName = string.Empty;
                            //    BaseCls.GlbReportTp = "INWARD";
                            //    _insu.GlbReportName = "Inward_Docauto.rpt";
                            //    _insu.GlbReportDoc = docNo;
                            //    _insu.Show();
                            //    _insu = null;
                            //}
                            //else
                            //{
                            if (BaseCls.GlbUserComCode == "AAL" && BaseCls.GlbUserDefLoca == "AH04A")
                            {
                                BaseCls.GlbReportTp = "GRN";
                                _view.GlbReportName = "GRNreport.rpt";
                                BaseCls.GlbReportName = "GRNreport.rpt";
                            }
                            else
                            {
                                BaseCls.GlbReportName = string.Empty;
                                GlbReportName = string.Empty;
                               
                                _view.GlbReportName = "Inward_Docs.rpt";
                                BaseCls.GlbReportName = "Inward_Docs.rpt";
                            }
                                _view.GlbReportDoc = docNo;
                                _view.Show();
                                _view = null;
                           // }
                        }
                    }
                }
                if (_type == "WAREXT" || _type == "FGAP")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    BaseCls.GlbReportTp = "REC";
                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    _view.GlbReportName = "ReceiptPrints.rpt";
                    BaseCls.GlbReportName = "ReceiptPrints.rpt";
                    _view.GlbReportDoc = docNo;
                    _view.Show();
                    _view = null;
                }
                if (_type == "MRN" || _type == "INTR")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SMRNRepPrints.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_MRNRepPrints.rpt" : "MRNRepPrints.rpt";
                    BaseCls.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SMRNRepPrints.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_MRNRepPrints.rpt" : "MRNRepPrints.rpt";
                    _view.GlbReportDoc = docNo;
                    _view.Show();
                    _view = null;
                }
                if (_type == "RCC")
                {
                    BaseCls.GlbReportTp = "RCC";
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SRCCPrint_New.rpt" : "RCCPrint_New.rpt";
                    BaseCls.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SRCCPrint_New.rpt" : "RCCPrint_New.rpt";
                    _view.GlbReportDoc = docNo;
                    BaseCls.GlbReportDoc = docNo;
                    _view.Show();
                    _view = null;
                }
                if (_type == "GRAN")
                {
                    BaseCls.GlbReportTp = "GRAN";
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SGRANPrints.rpt" : "GRANPrints.rpt";
                    BaseCls.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SGRANPrints.rpt" : "GRANPrints.rpt";
                    _view.GlbReportDoc = docNo;
                    _view.Show();
                    _view = null;
                }

                if (_type == "DIN")
                {
                    BaseCls.GlbReportTp = "DIN";
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SDINPrints.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_DINPrints.rpt" : "DINPrints.rpt";
                    BaseCls.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "SDINPrints.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_DINPrints.rpt" : "DINPrints.rpt";
                    _view.GlbReportDoc = docNo;
                    _view.Show();
                    _view = null;
                }
                if (_reqDocType == "VEHJOB")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    Reports.Service.ReportViewerSVC _view = new Reports.Service.ReportViewerSVC();
                    BaseCls.GlbReportCompCode = BaseCls.GlbUserComCode;
                    BaseCls.GlbReportName = "ServiceJobCard.rpt";
                    BaseCls.GlbReportDoc = docNo;
                    _view.Show();
                    _view = null;
                }
                if (_reqDocType == "ACSER")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    FF.BusinessObjects.RecieptHeader _rec = CHNLSVC.Sales.GetAcJobReciept(docNo, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    BaseCls.GlbReportName = (BaseCls.GlbUserComCode == "SGL") ? "SServiceReceiptPrints.rpt" : "ServiceReceiptPrints.rpt";
                    BaseCls.GlbReportDoc = _rec.Sar_receipt_no;//"AAZTS-SVJOB-000033";//- Receipt #
                    _view.Show();
                    _view = null;
                }
                if (_reqDocType == "COVER")
                {
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;

                    string _insComp = "";

                    List<FF.BusinessObjects.VehicleInsuarance> _vehins = CHNLSVC.General.GetInsuranceByRef( docNo);
                    _insComp = _vehins[0].Svit_ins_com;

                    if (_insComp == "CN001")
                    {
                        BaseCls.GlbReportName = "InsuranceCoverNote.rpt";
                    }
                    if (_insComp == "MBSL")
                    {
                        BaseCls.GlbReportName = "InsuranceCoverNoteMBSL.rpt";
                    }
                    if (_insComp == "UAL01")
                    {
                        BaseCls.GlbReportName = "InsuranceCoverNoteUMS.rpt";
                    }
                    if (_insComp == "JS001")
                    {
                        BaseCls.GlbReportName = "InsuranceCoverNoteJS.rpt";
                    }
                    if (_insComp == "AIA")
                    {
                        BaseCls.GlbReportName = "InsuranceCoverNoteAIA.rpt";
                    }

                    Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();
                    //BaseCls.GlbReportName = "InsuranceCoverNote.rpt";
                    BaseCls.GlbReportDoc = docNo;
                    _view.Show();
                    _view = null;
                }
                if (_type == "RVT")
                {
                    BaseCls.GlbReportTp = "RVT";
                    BaseCls.GlbReportName = string.Empty;
                    GlbReportName = string.Empty;
                    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                    _view.GlbReportName = "RevertSRN.rpt";
                    BaseCls.GlbReportName = "RevertSRN.rpt";
                    _view.GlbReportDoc = docNo;
                    BaseCls.GlbReportDoc = docNo;
                    _view.Show();
                    _view = null;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while processing\n" + ex.Message + "\n" + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
        }

        private void View()
        {
            Cursor.Current = Cursors.WaitCursor;// Added By Udesh - 08-Oct-2018
            try // Try Catch block Added By Udesh - 08-Oct-2018
            {

                if (_docType == "")
                {
                    MessageBox.Show("Please select Document Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DateTime fromdate;
                DateTime todate;
                fromdate = dateTimePickerFromDate.Value;
                todate = dateTimePickerToDate.Value;

                if (fromdate > todate)
                {
                    MessageBox.Show("To date cannot be less than from date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                BindDocNumbersGridData(_docType, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, BaseCls.GlbUserComCode, dateTimePickerFromDate.Value.Date, dateTimePickerToDate.Value.Date);
                BindRequestedDocDetailsGridData(BaseCls.GlbUserDefLoca, "ALL", dateTimePickerFromDate.Value.Date, dateTimePickerToDate.Value.Date);
            }
            catch
            {

            }
            Cursor.Current = Cursors.Default;// Added By Udesh - 08-Oct-2018
        }

        #endregion

        #region  button events

        private void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                Approve();
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

        private void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                Reject();
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Print();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Cancel();
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
                ClearControls();
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11041))
                {
                    pnlLoacationSearch.Visible = true;
                }
                else
                {
                    pnlLoacationSearch.Visible = false;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonView_Click(object sender, EventArgs e)
        {
            try
            {
                View();
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



        private void buttonAppView_Click(object sender, EventArgs e)
        {
            try
            {
                if (_status == "")
                {
                    MessageBox.Show("Please select status", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string _masterLocation = "";
                if (pnlLoacationSearch.Visible && txtLocationSearch.Text != "")
                {
                    _masterLocation = txtLocationSearch.Text;
                }
                else if (pnlLoacationSearch.Visible && chkLocAll.Checked)
                {
                    _masterLocation = "ALL";
                }
                else
                {
                    _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                }


                BindAllDocDetailsGridData(_masterLocation, _status, dateTimePickerAppFrom.Value.Date, dateTimePickerAppTo.Value.Date);
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        #endregion

        #region key down events

        private void dateTimePickerFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePickerToDate.Focus();
            }
        }

        private void dateTimePickerToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                opt1.Focus();
            }
        }

        private void opt13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonView.Focus();
            }
        }

        #endregion

        #region gv selection change events

        private void gvAllDocs_SelectionChanged(object sender, EventArgs e)
        {
            if (gvAllDocs.SelectedRows.Count > 0)
            {
                txtDocNoApp.Text = gvAllDocs.Rows[gvAllDocs.SelectedRows[0].Index].Cells[1].Value.ToString();
                txtDocDateApp.Text = Convert.ToDateTime(gvAllDocs.Rows[gvAllDocs.SelectedRows[0].Index].Cells[2].Value).Date.ToString("dd/MM/yyyy");
            }
        }

        private void gvReqDocs_SelectionChanged(object sender, EventArgs e)
        {
            if (gvReqDocs.SelectedRows.Count > 0)
            {
                txtReqDocNo.Text = gvReqDocs.Rows[gvReqDocs.SelectedRows[0].Index].Cells[0].Value.ToString();
                txtReqDocDate.Text = Convert.ToDateTime(gvReqDocs.Rows[gvReqDocs.SelectedRows[0].Index].Cells[3].Value).Date.ToString("dd/MM/yyyy");
                _reqDocType = gvReqDocs.Rows[gvReqDocs.SelectedRows[0].Index].Cells[2].Value.ToString();
                if (gvReqDocs.Rows[gvReqDocs.SelectedRows[0].Index].Cells[4].Value.ToString() == "A" && Convert.ToInt32(gvReqDocs.Rows[gvReqDocs.SelectedRows[0].Index].Cells[5].Value) == 0)
                {
                    btnPrint.Enabled = true;
                }
                else
                {
                    btnPrint.Enabled = false;
                }
            }
        }

        private void gvDocs_SelectionChanged(object sender, EventArgs e)
        {
            if (gvDocs.SelectedRows.Count > 0)
            {
                txtDocNo.Text = gvDocs.Rows[gvDocs.SelectedRows[0].Index].Cells[0].Value.ToString();
                txtDocDate.Text = Convert.ToDateTime(gvDocs.Rows[gvDocs.SelectedRows[0].Index].Cells[2].Value).ToString();
            }
        }

        #endregion

        #region button visible set

        private void SetSecondTabButtons()
        {
            btnApprove.Visible = true;
            btnReject.Visible = true;

            btnSave.Visible = false;
            btnPrint.Visible = false;
            btnCancel.Visible = false;

            toolStripSeparator3.Visible = true;
            toolStripSeparator2.Visible = true;

            toolStripSeparator4.Visible = false;
            toolStripSeparator5.Visible = false;
            toolStripSeparator6.Visible = false;
        }

        private void SetFirstTabButtons()
        {
            btnSave.Visible = true;
            btnPrint.Visible = true;
            btnCancel.Visible = false;

            btnApprove.Visible = false;
            btnReject.Visible = false;

            toolStripSeparator4.Visible = false;
            toolStripSeparator5.Visible = true;
            toolStripSeparator6.Visible = true;

            toolStripSeparator3.Visible = false;
            toolStripSeparator2.Visible = false;
        }

        #endregion

        private void ReprintDocuments_Load(object sender, EventArgs e)
        {

            gvDocs.AutoGenerateColumns = false;
            gvReqDocs.AutoGenerateColumns = false;
            gvAllDocs.AutoGenerateColumns = false;
            CheckPermission();
            dateTimePickerFromDate.Focus();
            try
            {
                double allowPeriod = 0;
                decimal numPrint = 0;
                List<Hpr_SysParameter> _list = CHNLSVC.Sales.GetAll_hpr_Para("REPERIOD", "COM", BaseCls.GlbUserComCode);
                if (_list != null && _list.Count > 0)
                {
                    allowPeriod = (double)_list[0].Hsy_val;
                }
                List<Hpr_SysParameter> _list1 = CHNLSVC.Sales.GetAll_hpr_Para("RENUM", "COM", BaseCls.GlbUserComCode);
                if (_list1 != null && _list1.Count > 0)
                {
                    numPrint = _list1[0].Hsy_val;
                }

                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11041))
                {
                    pnlLoacationSearch.Visible = true;
                }
                else
                {
                    pnlLoacationSearch.Visible = false;
                }


                lblMessage.Text = "You can take upto " + numPrint + " reprint  copies within " + allowPeriod.ToString() + " minutes without Approvel";
                DateTime _date = CHNLSVC.Security.GetServerDateTime().Date;
                dateTimePickerAppFrom.Value = _date.AddDays(-1);
                dateTimePickerFromDate.Value = _date.AddDays(-1);
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

        private void CheckPermission()
        {
            //check for user permission
            string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "DOCRP"))
            {
                btnApprove.Enabled = true;
                btnReject.Enabled = true;
            }
            else
            {
                btnApprove.Enabled = false;
                btnReject.Enabled = false;
            }
        }

        private void btnLoactionSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLocationSearch;
                _CommonSearch.txtSearchbyword.Text = txtLocationSearch.Text;
                _CommonSearch.ShowDialog();
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



        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        private void chkLocAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLocAll.Checked)
            {
                txtLocationSearch.Text = "";

            }
            else {
                txtLocationSearch.Text = "";
            }
        }

        private void opt34_CheckedChanged(object sender, EventArgs e)
        {
            if (opt34.Checked)
            {
                _docType = "SMTINS";
                View();
            }
        }

        private void opt35_CheckedChanged(object sender, EventArgs e)
        {
            if (opt35.Checked)
            {
                _docType = "RCCACK";
                View();
            }
        }

        private void opt36_CheckedChanged(object sender, EventArgs e)
        {
            if (opt36.Checked)
            {
                _docType = "CRAFSL";
                View();
            }
        }

        private void opt37_CheckedChanged(object sender, EventArgs e)
        {
            if (opt37.Checked)
            {
                _docType = "AODACK";
                View();
            }
        }

        // Added By Udesh - 08-Oct-2018
        private void opt38_CheckedChanged(object sender, EventArgs e)
        {
            if (opt38.Checked)
            {
                _docType = "Estimate";
                View();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        



        

        



       


    }
}
