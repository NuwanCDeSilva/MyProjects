using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using FF.BusinessObjects;
using System.IO.Ports;
using System.Drawing.Imaging;
using System.IO;
using System.Globalization;
using System.Data.OleDb;
using System.Configuration;
using FF.BusinessObjects.InventoryNew;
using FF.BusinessObjects.General;

namespace FF.WindowsERPClient.Barcode
{
    public partial class WareHouseBarCode : Base
    {
        string PrintType;
        BarcodeLib.Barcode b = new BarcodeLib.Barcode();
        int BWidth = 0;
        int BHeight = 0;
        int noofPages = 0;
        int lastpageitems = 0;
        int repeatTimes1 = 0;
        Image PrintImage1;
        int nPage = 0, k = 0;
        int pointNo = 0;
        int ImageNo = 0;
        int count = 0;
        ImageList ImageList = new ImageList();
        ImageList ImageList2 = new ImageList();
        int N = 0;

        Point[] points136x50 = new Point[] { new Point { X = 55, Y = 33 }, new Point { X = 195, Y = 33 }, new Point { X = 338, Y = 33 }, new Point { X = 480, Y = 33 },new Point { X = 620, Y = 33 } ,
                                             new Point { X = 55, Y = 84 }, new Point { X = 195, Y = 84 }, new Point { X = 338, Y = 84 }, new Point { X = 480, Y = 84 },new Point { X = 620, Y = 84 } ,
                                             new Point { X = 55, Y = 135 }, new Point { X = 195, Y = 135 }, new Point { X = 338, Y = 135 }, new Point { X = 480, Y = 135 },new Point { X = 620, Y = 135 } ,
                                             new Point { X = 55, Y = 186 }, new Point { X = 195, Y = 186 }, new Point { X = 338, Y = 186 }, new Point { X = 480, Y = 186 },new Point { X = 620, Y = 186 } ,
                                             new Point { X = 55, Y = 237 }, new Point { X = 195, Y = 237 }, new Point { X = 338, Y = 237 }, new Point { X = 480, Y = 237 },new Point { X = 620, Y = 237 } ,
                                             new Point { X = 55, Y = 288 }, new Point { X = 195, Y = 288 }, new Point { X = 338, Y = 288 }, new Point { X = 480, Y = 288 },new Point { X = 620, Y = 288 } ,                                            
                                             new Point { X = 55, Y = 339 }, new Point { X = 195, Y = 339 }, new Point { X = 338, Y = 339 }, new Point { X = 480, Y = 339 },new Point { X = 620, Y = 339 } ,
                                             new Point { X = 55, Y = 390 }, new Point { X = 195, Y = 390 }, new Point { X = 338, Y = 390 }, new Point { X = 480, Y = 390 },new Point { X = 620, Y = 390 } ,
                                             new Point { X = 55, Y = 441 }, new Point { X = 195, Y = 441 }, new Point { X = 338, Y = 441 }, new Point { X = 480, Y = 441 },new Point { X = 620, Y = 441 } ,
                                             new Point { X = 55, Y = 492 }, new Point { X = 195, Y = 492 }, new Point { X = 338, Y = 492 }, new Point { X = 480, Y = 492 },new Point { X = 620, Y = 492 } ,
                                             new Point { X = 55, Y = 543 }, new Point { X = 195, Y = 543 }, new Point { X = 338, Y = 543 }, new Point { X = 480, Y = 543 },new Point { X = 620, Y = 543 } ,
                                             new Point { X = 55, Y = 594 }, new Point { X = 195, Y = 594 }, new Point { X = 338, Y = 594 }, new Point { X = 480, Y = 594 },new Point { X = 620, Y = 594 } ,
                                             new Point { X = 55, Y = 645 }, new Point { X = 195, Y = 645 }, new Point { X = 338, Y = 645 }, new Point { X = 480, Y = 645 },new Point { X = 620, Y = 645 } ,
                                             new Point { X = 55, Y = 696 }, new Point { X = 195, Y = 696 }, new Point { X = 338, Y = 696 }, new Point { X = 480, Y = 696 },new Point { X = 620, Y = 696 } ,
                                             new Point { X = 55, Y = 747 }, new Point { X = 195, Y = 747 }, new Point { X = 338, Y = 747 }, new Point { X = 480, Y = 747 },new Point { X = 620, Y = 747 } ,
                                             new Point { X = 55, Y = 798 }, new Point { X = 195, Y = 798 }, new Point { X = 338, Y = 798 }, new Point { X = 480, Y = 798 },new Point { X = 620, Y = 798 } ,
                                             new Point { X = 55, Y = 849 }, new Point { X = 195, Y = 849 }, new Point { X = 338, Y = 849 }, new Point { X = 480, Y = 849 },new Point { X = 620, Y = 849 } ,
                                             new Point { X = 55, Y = 900 }, new Point { X = 195, Y = 900 }, new Point { X = 338, Y = 900 }, new Point { X = 480, Y = 900 },new Point { X = 620, Y = 900 } ,
                                             new Point { X = 55, Y = 951 }, new Point { X = 195, Y = 951 }, new Point { X = 338, Y = 951 }, new Point { X = 480, Y = 951 },new Point { X = 620, Y = 951 } ,
                                             new Point { X = 55, Y = 1002 }, new Point { X = 195, Y = 1002 }, new Point { X = 338, Y = 1002 }, new Point { X = 480, Y = 1002 },new Point { X = 620, Y = 1002 } ,
                                             new Point { X = 55, Y = 1053 }, new Point { X = 195, Y = 1053 }, new Point { X = 338, Y = 1053 }, new Point { X = 480, Y = 1053 },new Point { X = 620, Y = 1053 } ,
        };
        public WareHouseBarCode()
        {
            InitializeComponent();
        }       
        private void button5btnSearch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
                this.Cursor = Cursors.WaitCursor;
                _commonSearch.ReturnIndex = 0;
                _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_commonSearch.SearchParams, null, null);
                _commonSearch.dvResult.DataSource = _result;
                _commonSearch.BindUCtrlDDLData(_result);
                _commonSearch.obj_TragetTextBox = txtBarcodeItem;
                _commonSearch.IsSearchEnter = true;
                this.Cursor = Cursors.Default;
                _commonSearch.ShowDialog();
                txtBarcodeItem.Select();
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.GRNNo:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }
        private void SystemErrorMessage(Exception ex)
        {
            CHNLSVC.CloseChannel();
            this.Cursor = Cursors.Default;
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        private void chbItem_CheckedChanged(object sender, EventArgs e)
        {
            if(chbItem.Checked==true)
            {
                txtBarcodeItem.Enabled = true;
            }
            else
            {
                txtBarcodeItem.Enabled = false;
            }
        }

        private void rdbRange_CheckedChanged(object sender, EventArgs e)
        {
            if(rdbRange.Checked==true)
            {
                txtNumofSer.Enabled = true;
                txtprefix.Enabled = true;
                txtprefixnum.Enabled = true;
                txtstartserno.Enabled = true;
                txtendserno.Enabled = true;
                txtdocno.Enabled = false;
            }
            else
            {
                txtNumofSer.Enabled = false;
                txtprefix.Enabled = false;
                txtprefixnum.Enabled = false;
                txtstartserno.Enabled = false;
                txtendserno.Enabled = false;
                txtdocno.Enabled = true;
            }
        }

        private void rdbSerial_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbSerial.Checked == true)
            {
                txtNumofSer.Enabled = false;
                txtprefix.Enabled = false;
                txtprefixnum.Enabled = false;
                txtstartserno.Enabled = false;
                txtendserno.Enabled = false;
                txtdocno.Enabled = true;
            }
            else
            {
                txtNumofSer.Enabled = true;
                txtprefix.Enabled = true;
                txtprefixnum.Enabled = true;
                txtstartserno.Enabled = true;
                txtendserno.Enabled = true;
                txtdocno.Enabled = false;
            }
        }

        private void rdbDoc_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbDoc.Checked == true)
            {
                txtNumofSer.Enabled = false;
                txtprefix.Enabled = false;
                txtprefixnum.Enabled = false;
                txtstartserno.Enabled = false;
                txtendserno.Enabled = false;
                txtdocno.Enabled = true;
            }
            else
            {
                txtNumofSer.Enabled = true;
                txtprefix.Enabled = true;
                txtprefixnum.Enabled = true;
                txtstartserno.Enabled = true;
                txtendserno.Enabled = true;
                txtdocno.Enabled = false;
            }
        }

        private void txtBarcodeItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter | e.KeyCode.Equals(Keys.Tab))
            {
                if (string.IsNullOrEmpty(txtBarcodeItem.Text.Trim())) return;

                this.Cursor = Cursors.WaitCursor;
                string _item = "";
                if (txtBarcodeItem.Text.Length == 16)
                    _item = txtBarcodeItem.Text.Substring(1, 7);
                else if (txtBarcodeItem.Text.Length == 15)
                    _item = txtBarcodeItem.Text.Substring(0, 7);
                else if (txtBarcodeItem.Text.Length == 8)
                    _item = txtBarcodeItem.Text.Substring(1, 7);
                else if (txtBarcodeItem.Text.Length == 20)
                    _item = txtBarcodeItem.Text.Substring(0, 12);
                else
                    _item = txtBarcodeItem.Text;
                this.Cursor = Cursors.Default;
                //txtchangeqty.Focus();

            }
        }

        private void txtBarcodeItem_Leave(object sender, EventArgs e)
        {
            string _prefix =  DateTime.Now.Month + "" + DateTime.Now.ToString("yy") + "" + "SE0300";
            string company = BaseCls.GlbUserComCode;
            mst_itm_max_serNo _serNo = new mst_itm_max_serNo();

            _serNo = CHNLSVC.MsgPortal.ItemSerialNo(string.Empty, company);
            if(_serNo==null)
            {
                txtprefix.Text = _prefix;
                txtstartserno.Text = "1";
            }
            else
            {
                txtprefix.Text = _prefix;
                txtstartserno.Text = _serNo.mims_ser_inc_no.ToString();
            }

        }

        private void txtNumofSer_Leave(object sender, EventArgs e)
        {
            string _prefix = DateTime.Now.Month + "" + DateTime.Now.ToString("yy") + "" + "SE0300";
            string company = BaseCls.GlbUserComCode;
            mst_itm_max_serNo _serNo = new mst_itm_max_serNo();
            int serno = 0;

            _serNo = CHNLSVC.MsgPortal.ItemSerialNo(string.Empty, company);
            if (_serNo == null)
            {
                txtprefix.Text = _prefix;
                txtstartserno.Text = "1";
                if (txtNumofSer.Text != "")
                {
                    serno = ((Convert.ToInt32(txtstartserno.Text)) + (Convert.ToInt32(txtNumofSer.Text) - 1));
                    txtendserno.Text = serno.ToString();
                }
                else
                {
                    MessageBox.Show("Please enter number of Serial.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                txtprefix.Text = _prefix;
                txtstartserno.Text = _serNo.mims_ser_inc_no.ToString();
                if (txtNumofSer.Text != "")
                {
                    serno = ((Convert.ToInt32(txtstartserno.Text)) + (Convert.ToInt32(txtNumofSer.Text) - 1));
                    txtendserno.Text = serno.ToString();
                }
                else
                {
                    MessageBox.Show("Please enter number of Serial.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txtNumofSer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtstartserno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtnumofcopiesser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        private mst_itm_max_serNo SerialNoUpdate(string itmcd, Int32 incNo, string prefix)
        {
            mst_itm_max_serNo _maxSerNo = new mst_itm_max_serNo();
            _maxSerNo.mims_itm_cd = itmcd;
            _maxSerNo.mims_com_cd = BaseCls.GlbUserComCode as string;
            _maxSerNo.mims_ser_inc_no = incNo;
            _maxSerNo.mims_prefix = prefix;
            _maxSerNo.mims_cre_by = BaseCls.GlbUserID as string;
            _maxSerNo.mims_cre_dt = DateTime.Now;
            _maxSerNo.mims_mod_by = BaseCls.GlbUserID as string;
            _maxSerNo.mims_mod_dt = DateTime.Now;
            _maxSerNo.mims_cre_session_id = "";
            _maxSerNo.mims_mod_session_id = "";
            return _maxSerNo;
        }
        private void print()
        {
            Int32 numberofcop = 0;
            Int32 startno = 0;
            Int32 numofser = 0;
            string startnostr = "";
            string prefix = "";
            BarcodeDataSet _DS = new BarcodeDataSet();
            DataTable ds = new DataTable("tbl");
            ds.Columns.Add("ITEM_CD", typeof(String));
            ds.Columns.Add("SERIAL_CODE", typeof(String));
            ds.Columns.Add("ITM_DESC", typeof(String));
            ds.Columns.Add("ITM_MODEL", typeof(String));
            ds.Columns.Add("Company", typeof(String));
            ds.Columns.Add("HEADER", typeof(String));
            string company = BaseCls.GlbUserComCode;
            string comdesc = "";
            //string sel = cmbA4.SelectedValue.ToString();
            string sel = cmbA4.Text.ToString();
            string _err = string.Empty;
            MasterCompany comm = CHNLSVC.General.GetCompByCode(company);
            if (!string.IsNullOrEmpty(txtNumofSer.Text.ToString()) && !string.IsNullOrEmpty(txtprefix.Text.ToString()))
            {
                //if (company == "AAL")
                //{
                    Int32 _SerNoUpdate = CHNLSVC.MsgPortal.getItemMaxSerial(SerialNoUpdate(txtBarcodeItem.Text.ToString(), Convert.ToInt32(txtNumofSer.Text.ToString()), txtprefix.Text.ToString()), out _err);
                //}
                //else if (company == "ABL")
                //{
                //    Int32 _SerNoUpdate = CHNLSVC.MsgPortal.getItemMaxSerial(SerialNoUpdate(string.IsNullOrEmpty(collection["SerItmCd"].ToString()) ? "ABCK1800" : collection["SerItmCd"].ToString(), Convert.ToInt32(collection["NumofSer"]), collection["prefix"].ToString()), out _err);
                //}
            }
            if (sel == "---select size---")
            {
                //err.ErrorMsg = "Invalid barcode genarate type.";
                MessageBox.Show("Please select the print size.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //goto Err;
            }
            if (comm != null)
            {
                comdesc = comm.Mc_anal5;
            }
            if (!string.IsNullOrEmpty(cmbA4.ToString()))
            {
                if (txtnumofcopiesser.Text.ToString() != "")
                {
                    numberofcop = Convert.ToInt32(txtnumofcopiesser.Text.ToString());

                    if (chbItem.Checked == true)
                    {
                        if (!string.IsNullOrEmpty(txtBarcodeItem.Text.ToString()) || rdbDoc.Checked == true)
                        {
                            string itmcd = txtBarcodeItem.Text.ToString().ToUpper();
                            ApproveItemDetail _model = CHNLSVC.General.getApproveItemDetails(itmcd);
                            if (rdbRange.Checked == true)
                            {
                                if (txtNumofSer.Text.ToString() != "")
                                {
                                    numofser = Convert.ToInt32(txtNumofSer.Text.ToString());
                                    if (txtprefix.Text.ToString() != "")
                                    {
                                        prefix = txtprefix.Text.ToString();
                                        if (txtstartserno.Text.ToString() != "")
                                        {
                                            startno = Convert.ToInt32(txtstartserno.Text.ToString());
                                            startnostr = txtstartserno.Text.ToString();
                                            int num = 0;
                                            int lst = startno + numofser;
                                            string lnth = startnostr.Length.ToString();
                                            string pnum = company == "AAL" ? "00000000" : txtprefixnum.Text.ToString();
                                            for (num = startno; num < lst; num++)
                                            {
                                                string serial = prefix + num.ToString(pnum, CultureInfo.InvariantCulture);

                                                for (Int32 j = 0; j < numberofcop; j++)
                                                {
                                                    ds.Rows.Add(itmcd, serial, _model.MI_SHORTDESC, _model.MI_MODEL, comdesc, "ABANS - PRODUCT CODE");
                                                }
                                            }
                                            if (cmbA4.Text.ToString() == "60 x 15")
                                            {                                                 
                                                SerialWithItm6015 rpt = new SerialWithItm6015();
                                                //ReportDocument rptDoc = new ReportDocument();
                                                try
                                                {
                                                    _DS.Tables.Add(ds);
                                                    SerialWithItm6015 Report = new SerialWithItm6015();
                                                    Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                                    crystalReportViewer1.ReportSource = Report;
                                                    crystalReportViewer1.Refresh();
                                                }
                                                catch (Exception ex)
                                                {
                                                    throw ex;
                                                }

                                            }
                                            else if (cmbA4.Text.ToString() == "70 x 30")
                                            {
                                                SerialWithItm7030 rpt = new SerialWithItm7030();
                                                SerialWithItm7030_AAL rpt_AAL = new SerialWithItm7030_AAL();
                                                SerialWithItm7030_ABE rpt_ABE = new SerialWithItm7030_ABE();
                                                //ReportDocument rptDoc = new ReportDocument();
                                                try
                                                {
                                                    if (company == "AAL")
                                                    {
                                                        _DS.Tables.Add(ds);
                                                        SerialWithItm7030_AAL Report = new SerialWithItm7030_AAL();
                                                        Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                                        crystalReportViewer1.ReportSource = Report;
                                                        crystalReportViewer1.Refresh();
                                                    }
                                                    if (company == "ABE")
                                                    {
                                                        _DS.Tables.Add(ds);
                                                        SerialWithItm7030_ABE Report = new SerialWithItm7030_ABE();
                                                        Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                                        crystalReportViewer1.ReportSource = Report;
                                                        crystalReportViewer1.Refresh();
                                                    }
                                                    else
                                                    {
                                                        _DS.Tables.Add(ds);
                                                        SerialWithItm7030 Report = new SerialWithItm7030();
                                                        Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                                        crystalReportViewer1.ReportSource = Report;
                                                        crystalReportViewer1.Refresh();
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    throw ex;
                                                }
                                            }
                                            else if (cmbA4.Text.ToString() == "40 x 15")
                                            {
                                                SerialWithItm4015 rpt = new SerialWithItm4015();
                                                //ReportDocument rptDoc = new ReportDocument();
                                                try
                                                {
                                                    _DS.Tables.Add(ds);
                                                    SerialWithItm4015 Report = new SerialWithItm4015();
                                                    Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                                    crystalReportViewer1.ReportSource = Report;
                                                    crystalReportViewer1.Refresh();                                                   
                                                }
                                                catch (Exception ex)
                                                {
                                                    throw ex;
                                                }
                                            }
                                            else if (cmbA4.Text.ToString() == "40 x 10")
                                            {
                                                SerialWithItm4010 rpt = new SerialWithItm4010();
                                                //ReportDocument rptDoc = new ReportDocument();
                                                try
                                                {
                                                    _DS.Tables.Add(ds);
                                                    SerialWithItm4010 Report = new SerialWithItm4010();
                                                    Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                                    crystalReportViewer1.ReportSource = Report;
                                                    crystalReportViewer1.Refresh();  
                                                }
                                                catch (Exception ex)
                                                {
                                                    throw ex;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Please enter start serial number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);                                    
                                            //err.ErrorMsg = "Please enter start serial number.";
                                            //goto Err;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Please enter prefix.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);                                    
                                        //err.ErrorMsg = "Please enter prefix.";
                                        //goto Err;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Invalid number of serials.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    //err.ErrorMsg = "Invalid number of serials.";
                                    //goto Err;
                                }
                            }
                       // }
                    //}
                    else if (rdbSerial.Checked == true)
                    {
                        if (!string.IsNullOrEmpty(txtdocno.Text.ToString()))
                        {
                            string serial = txtdocno.Text.ToString();
                            for (Int32 j = 0; j < numberofcop; j++)
                            {
                                ds.Rows.Add(itmcd, serial, "", "", comdesc);
                            }

                            if (cmbA4.Text.ToString() == "60 x 15")
                            {

                                //IndiRptItm6015 rpt = new IndiRptItm6015();
                                //ReportDocument rptDoc = new ReportDocument();
                                try
                                {
                                    _DS.Tables.Add(ds);
                                    IndiRptItm6015 Report = new IndiRptItm6015();
                                    Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                    crystalReportViewer1.ReportSource = Report;
                                    crystalReportViewer1.Refresh(); 
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                            else if (cmbA4.Text.ToString() == "70 x 30")
                            {
                                //IndiRptItm7030 rpt = new IndiRptItm7030();
                                //ReportDocument rptDoc = new ReportDocument();
                                try
                                {
                                    _DS.Tables.Add(ds);
                                    IndiRptItm7030 Report = new IndiRptItm7030();
                                    Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                    crystalReportViewer1.ReportSource = Report;
                                    crystalReportViewer1.Refresh();
                                
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                            else if (cmbA4.Text.ToString() == "40 x 15")
                            {
                                //IndiRptItm4015 rpt = new IndiRptItm4015();
                                //ReportDocument rptDoc = new ReportDocument();
                                try
                                {
                                    _DS.Tables.Add(ds);
                                    IndiRptItm4015 Report = new IndiRptItm4015();
                                    Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                    crystalReportViewer1.ReportSource = Report;
                                    crystalReportViewer1.Refresh();     
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                            else if (cmbA4.Text.ToString() == "40 x 10")
                            {
                                //IndiRptItm4010 rpt = new IndiRptItm4010();
                                //ReportDocument rptDoc = new ReportDocument();
                                try
                                {
                                    _DS.Tables.Add(ds);
                                    IndiRptItm4010 Report = new IndiRptItm4010();
                                    Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                    crystalReportViewer1.ReportSource = Report;
                                    crystalReportViewer1.Refresh();       
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                        }
                        else
                        {
                            //err.ErrorMsg = "Please enter serial number.";
                            MessageBox.Show("Please enter serial number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //goto Err;
                        }
                    }
                    else if (rdbDoc.Checked == true)
                    {
                        //DOC NO FUNCTION with item
                        string doc = txtdocno.Text.ToString();
                        DataTable dtser = CHNLSVC.MsgPortal.Barcodeserdata(company, doc);
                        if (dtser.Rows.Count > 0)
                        {
                            int copys = numberofcop;
                            DataTable mdt = dtser;

                            for (int i = 2; i <= copys; i++)
                            {
                                mdt = mdt.Copy();
                                mdt.Merge(dtser);
                            }
                            int k = 0;
                            foreach (var sdt in mdt.Rows)
                            {
                                if (mdt.Rows[k]["ins_ser_1"].ToString() == "N/A")
                                {
                                    mdt.Rows[k]["ins_ser_1"] = null;
                                }
                                ds.Rows.Add(mdt.Rows[k]["ins_itm_cd"], mdt.Rows[k]["ins_ser_1"], mdt.Rows[k]["DESCRP"], mdt.Rows[k]["MODEL"], comdesc);
                                k++;
                            }
                        }


                        //if (cmbA4.Text.ToString() == "60X15" && printer1.Equals("false") && printer2.Equals("false"))
                        if (cmbA4.Text.ToString() == "60 x 15" )
                        {

                            //SerialWithItm6015 rpt = new SerialWithItm6015();
                            //ReportDocument rptDoc = new ReportDocument();
                            try
                            {
                                _DS.Tables.Add(ds);
                                SerialWithItm6015 Report = new SerialWithItm6015();
                                Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                crystalReportViewer1.ReportSource = Report;
                                crystalReportViewer1.Refresh();  
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                        //if (cmbA4.Text.ToString() == "60X15" && printer1.Equals("true"))
                        //{

                        //    SerialWithItm6015Print1 rpt = new SerialWithItm6015Print1();
                        //    ReportDocument rptDoc = new ReportDocument();
                        //    try
                        //    {
                        //        rpt.Database.Tables["SERDATA"].SetDataSource(ds);
                        //        rptDoc.Load(ReportPath + "\\ItmSer\\SerialWithItm6015Print1.rpt");
                        //        string AttachmentPath = PDFSavePath + "\\Print1-SerialWithItem60x15.pdf";
                        //        rpt.ExportToDisk(ExportFormatType.PortableDocFormat, AttachmentPath);
                        //        Response.Buffer = false;
                        //        Response.ClearContent();
                        //        Response.ClearHeaders();
                        //        this.Response.Clear();
                        //        this.Response.ContentType = "application/pdf";
                        //        Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");
                        //        return File(rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        throw ex;
                        //    }
                        //    rptDoc.Close();
                        //    rptDoc.Dispose();
                        //    rpt.Close();
                        //    rpt.Dispose();
                        //}
                        //if (cmbA4.Text.ToString() == "60X15" && printer2.Equals("true"))
                        //{

                        //    SerialWithItm6015Print2 rpt = new SerialWithItm6015Print2();
                        //    ReportDocument rptDoc = new ReportDocument();
                        //    try
                        //    {
                        //        rpt.Database.Tables["SERDATA"].SetDataSource(ds);
                        //        rptDoc.Load(ReportPath + "\\Printer2\\SerialWithItm6015Print2.rpt");
                        //        string AttachmentPath = PDFSavePath + "\\Print2-SerialWithItem60x15.pdf";
                        //        rpt.ExportToDisk(ExportFormatType.PortableDocFormat, AttachmentPath);
                        //        Response.Buffer = false;
                        //        Response.ClearContent();
                        //        Response.ClearHeaders();
                        //        this.Response.Clear();
                        //        this.Response.ContentType = "application/pdf";
                        //        Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");
                        //        return File(rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        throw ex;
                        //    }
                        //    rptDoc.Close();
                        //    rptDoc.Dispose();
                        //    rpt.Close();
                        //    rpt.Dispose();
                        //}
                        else if (cmbA4.Text.ToString() == "70 x 30")
                        {

                            try
                            {
                                if (company == "AAL")
                                {
                                    _DS.Tables.Add(ds);
                                    SerialWithItm7030_AAL Report = new SerialWithItm7030_AAL();
                                    Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                    crystalReportViewer1.ReportSource = Report;
                                    crystalReportViewer1.Refresh();  

                                }
                                else
                                {
                                    //if (printer1.Equals("false") && printer2.Equals("false"))
                                    //{

                                    _DS.Tables.Add(ds);
                                    SerialWithItm7030 Report = new SerialWithItm7030();
                                    Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                    crystalReportViewer1.ReportSource = Report;
                                    crystalReportViewer1.Refresh();

                                    //}
                                    //if (printer1.Equals("true"))
                                    //{
                                    //    SerialWithItm7030Print1 rpt = new SerialWithItm7030Print1();
                                    //    ReportDocument rptDoc = new ReportDocument();
                                    //    rpt.Database.Tables["SERDATA"].SetDataSource(ds);
                                    //    rptDoc.Load(ReportPath + "\\Printer1\\SerialWithItm7030Print1.rpt");
                                    //    string AttachmentPath = PDFSavePath + "\\Stickers-SerialWithItem70x30.pdf";
                                    //    rpt.ExportToDisk(ExportFormatType.PortableDocFormat, AttachmentPath);
                                    //    Response.Buffer = false;
                                    //    Response.ClearContent();
                                    //    Response.ClearHeaders();
                                    //    this.Response.Clear();
                                    //    this.Response.ContentType = "application/pdf";
                                    //    Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");
                                    //    return File(rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                                    //    rptDoc.Close();
                                    //    rptDoc.Dispose();
                                    //    rpt.Close();
                                    //    rpt.Dispose();
                                    //}
                                    //if (printer2.Equals("true"))
                                    //{
                                    //    SerialWithItm7030Print2 rpt = new SerialWithItm7030Print2();
                                    //    ReportDocument rptDoc = new ReportDocument();
                                    //    rpt.Database.Tables["SERDATA"].SetDataSource(ds);
                                    //    rptDoc.Load(ReportPath + "\\Printer2\\SerialWithItm7030Print2.rpt");
                                    //    string AttachmentPath = PDFSavePath + "\\Stickers-SerialWithItem70x30.pdf";
                                    //    rpt.ExportToDisk(ExportFormatType.PortableDocFormat, AttachmentPath);
                                    //    Response.Buffer = false;
                                    //    Response.ClearContent();
                                    //    Response.ClearHeaders();
                                    //    this.Response.Clear();
                                    //    this.Response.ContentType = "application/pdf";
                                    //    Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");
                                    //    return File(rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                                    //    rptDoc.Close();
                                    //    rptDoc.Dispose();
                                    //    rpt.Close();
                                    //    rpt.Dispose();
                                    //}
                                }


                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }


                        }
                        else if (cmbA4.Text.ToString() == "40 x 15")
                        {
                            //SerialWithItm4015 rpt = new SerialWithItm4015();
                            //ReportDocument rptDoc = new ReportDocument();
                            try
                            {

                                _DS.Tables.Add(ds);
                                SerialWithItm4015 Report = new SerialWithItm4015();
                                Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                crystalReportViewer1.ReportSource = Report;
                                crystalReportViewer1.Refresh();
                               
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                        //else if (cmbA4.Text.ToString() == "40X15" && (printer1.Equals("false") || printer2.Equals("true")))
                        //{
                        //    SerialWithItm4015Print1 rpt = new SerialWithItm4015Print1();
                        //    ReportDocument rptDoc = new ReportDocument();
                        //    try
                        //    {
                        //        rpt.Database.Tables["SERDATA"].SetDataSource(ds);
                        //        rptDoc.Load(ReportPath + "\\Printer1\\SerialWithItm4015Print1.rpt");
                        //        string AttachmentPath = PDFSavePath + "\\Stickers-SerialWithItem40x15.pdf";
                        //        rpt.ExportToDisk(ExportFormatType.PortableDocFormat, AttachmentPath);
                        //        Response.Buffer = false;
                        //        Response.ClearContent();
                        //        Response.ClearHeaders();
                        //        this.Response.Clear();
                        //        this.Response.ContentType = "application/pdf";
                        //        Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");
                        //        return File(rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        throw ex;
                        //    }
                        //    rptDoc.Close();
                        //    rptDoc.Dispose();
                        //    rpt.Close();
                        //    rpt.Dispose();
                        //}
                        else if (cmbA4.Text.ToString() == "40 x 10")
                        {
                            //SerialWithItm4010 rpt = new SerialWithItm4010();
                            //ReportDocument rptDoc = new ReportDocument();
                            try
                            {
                                _DS.Tables.Add(ds);
                                SerialWithItm4010 Report = new SerialWithItm4010();
                                Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                crystalReportViewer1.ReportSource = Report;
                                crystalReportViewer1.Refresh();
                           
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }

                        }
                        //else if (cmbA4.Text.ToString() == "40X10" && printer1.Equals("true"))
                        //{
                        //    SerialWithItm4010Print1 rpt = new SerialWithItm4010Print1();
                        //    ReportDocument rptDoc = new ReportDocument();
                        //    try
                        //    {
                        //        rpt.Database.Tables["SERDATA"].SetDataSource(ds);
                        //        rptDoc.Load(ReportPath + "\\Printer1\\SerialWithItm4010Print1.rpt");
                        //        string AttachmentPath = PDFSavePath + "\\Stickers-SerialWithItem40x10.pdf";
                        //        rpt.ExportToDisk(ExportFormatType.PortableDocFormat, AttachmentPath);
                        //        Response.Buffer = false;
                        //        Response.ClearContent();
                        //        Response.ClearHeaders();
                        //        this.Response.Clear();
                        //        this.Response.ContentType = "application/pdf";
                        //        Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");
                        //        return File(rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        throw ex;
                        //    }
                        //    rptDoc.Close();
                        //    rptDoc.Dispose();
                        //    rpt.Close();
                        //    rpt.Dispose();
                        //}
                        //else if (cmbA4.Text.ToString() == "40X10" && printer2.Equals("true"))
                        //{
                        //    SerialWithItm4010Print2 rpt = new SerialWithItm4010Print2();
                        //    ReportDocument rptDoc = new ReportDocument();
                        //    try
                        //    {
                        //        rpt.Database.Tables["SERDATA"].SetDataSource(ds);
                        //        rptDoc.Load(ReportPath + "\\Printer2\\SerialWithItm4010Print2.rpt");
                        //        string AttachmentPath = PDFSavePath + "\\Stickers-SerialWithItem40x10.pdf";
                        //        rpt.ExportToDisk(ExportFormatType.PortableDocFormat, AttachmentPath);
                        //        Response.Buffer = false;
                        //        Response.ClearContent();
                        //        Response.ClearHeaders();
                        //        this.Response.Clear();
                        //        this.Response.ContentType = "application/pdf";
                        //        Response.AppendHeader("Content-Disposition", "inline; filename=something.pdf");
                        //        return File(rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), "application/pdf");
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        throw ex;
                        //    }
                        //    rptDoc.Close();
                        //    rptDoc.Dispose();
                        //    rpt.Close();
                        //    rpt.Dispose();
                        //}

                    }
                    else
                    {
                            //err.ErrorMsg = "Invalid barcode genarate type.";
                            MessageBox.Show("Invalid barcode genarate type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //goto Err;
                    }

                }
                else
                {
                      //err.ErrorMsg = "Please enter item code.";
                      MessageBox.Show("Please enter item code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                      //goto Err;
                }
                }
                    else
                    {
                        //if (collection["INDIVID-SER"].ToString() != "")
                        //{
                        if (rdbSerial.Checked == true)
                            {
                                if (txtNumofSer.Text.ToString() != "")
                                {
                                    numofser = Convert.ToInt32(txtNumofSer.Text.ToString());
                                    if (txtprefix.Text.ToString() != "")
                                    {
                                        prefix = txtprefix.Text.ToString();
                                        if (txtstartserno.Text.ToString() != "")
                                        {
                                            startno = Convert.ToInt32(txtstartserno.Text.ToString());
                                            startnostr = txtstartserno.Text.ToString();
                                            int num = 0;
                                            int lst = startno + numofser;
                                            string lnth = startnostr.Length.ToString();
                                            string pnum = txtprefixnum.Text.ToString();
                                            for (num = startno; num < lst; num++)
                                            {
                                                string serial = prefix + num.ToString(pnum, CultureInfo.InvariantCulture);

                                                for (Int32 j = 0; j < numberofcop; j++)
                                                {
                                                    ds.Rows.Add("", serial, "", "", comdesc);
                                                }
                                            }
                                            if (cmbA4.Text.ToString() == "60 x 15")
                                            {

                                                //Serial6015 rpt = new Serial6015();
                                                //ReportDocument rptDoc = new ReportDocument();
                                                try
                                                {
                                                    _DS.Tables.Add(ds);
                                                    Serial6015 Report = new Serial6015();
                                                    Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                                    crystalReportViewer1.ReportSource = Report;
                                                    crystalReportViewer1.Refresh();
                                                }
                                                catch (Exception ex)
                                                {
                                                    throw ex;
                                                }
                                            }
                                            else if (cmbA4.Text.ToString() == "70 x 30")
                                            {
                                                //Serial7030 rpt = new Serial7030();
                                                //Serial7030_ABE rpt_ABE = new Serial7030_ABE();
                                                //ReportDocument rptDoc = new ReportDocument();
                                                //ReportDocument rptDoc_ABE = new ReportDocument();
                                                string AttachmentPath = string.Empty;
                                                try
                                                {
                                                    if (company == "ABE")
                                                    {
                                                        _DS.Tables.Add(ds);
                                                        Serial7030_ABE Report = new Serial7030_ABE();
                                                        Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                                        crystalReportViewer1.ReportSource = Report;
                                                        crystalReportViewer1.Refresh();
                                                    }
                                                    else
                                                    {
                                                        _DS.Tables.Add(ds);
                                                        Serial7030 Report = new Serial7030();
                                                        Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                                        crystalReportViewer1.ReportSource = Report;
                                                        crystalReportViewer1.Refresh();

                                                    }                                                    
                                                }
                                                catch (Exception ex)
                                                {
                                                    throw ex;
                                                }
                                            }
                                            else if (cmbA4.Text.ToString() == "40 x 15")
                                            {
                                                //Serial4015 rpt = new Serial4015();
                                                //ReportDocument rptDoc = new ReportDocument();
                                                try
                                                {
                                                    _DS.Tables.Add(ds);
                                                    Serial4015 Report = new Serial4015();
                                                    Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                                    crystalReportViewer1.ReportSource = Report;
                                                    crystalReportViewer1.Refresh();

                                                }
                                                catch (Exception ex)
                                                {
                                                    throw ex;
                                                }
                                            }
                                            else if (cmbA4.Text.ToString() == "40 x 10")
                                            {
                                                //Serial4010 rpt = new Serial4010();
                                                //ReportDocument rptDoc = new ReportDocument();
                                                try
                                                {
                                                    _DS.Tables.Add(ds);
                                                    Serial4010 Report = new Serial4010();
                                                    Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                                    crystalReportViewer1.ReportSource = Report;
                                                    crystalReportViewer1.Refresh();

                                                }
                                                catch (Exception ex)
                                                {
                                                    throw ex;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Please enter start serial number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);                                                                    
                                            //err.ErrorMsg = "Please enter start serial number.";
                                            //goto Err;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Please enter prefix.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);                                                                    
                                        //err.ErrorMsg = "Please enter prefix.";
                                        //goto Err;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Invalid number of serials.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);                                                                    
                                    //err.ErrorMsg = "Invalid number of serials.";
                                    //goto Err;
                                }
                            }
                        else if (rdbDoc.Checked == true)
                            {
                                if (!string.IsNullOrEmpty(txtdocno.Text.ToString()))
                                {
                                    string serial = txtdocno.Text.ToString();
                                    for (Int32 j = 0; j < numberofcop; j++)
                                    {
                                        ds.Rows.Add("", serial, "", "", comdesc);
                                    }

                                    if (cmbA4.Text.ToString() == "60 x 15")
                                    {

                                        //IndiRpt6015 rpt = new IndiRpt6015();
                                        //ReportDocument rptDoc = new ReportDocument();
                                        try
                                        {
                                            _DS.Tables.Add(ds);
                                            IndiRpt6015 Report = new IndiRpt6015();
                                            Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                            crystalReportViewer1.ReportSource = Report;
                                            crystalReportViewer1.Refresh();
                                        }
                                        catch (Exception ex)
                                        {
                                            throw ex;
                                        }

                                    }
                                    else if (cmbA4.Text.ToString() == "70 x 30")
                                    {
                                        //IndiRpt7030 rpt = new IndiRpt7030();
                                        //ReportDocument rptDoc = new ReportDocument();
                                        try
                                        {
                                            _DS.Tables.Add(ds);
                                            IndiRpt7030 Report = new IndiRpt7030();
                                            Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                            crystalReportViewer1.ReportSource = Report;
                                            crystalReportViewer1.Refresh();
                                          
                                        }
                                        catch (Exception ex)
                                        {
                                            throw ex;
                                        }

                                    }
                                    else if (cmbA4.Text.ToString() == "40 x 15")
                                    {
                                        //IndiRpt4015 rpt = new IndiRpt4015();
                                        //ReportDocument rptDoc = new ReportDocument();
                                        try
                                        {
                                            _DS.Tables.Add(ds);
                                            IndiRpt4015 Report = new IndiRpt4015();
                                            Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                            crystalReportViewer1.ReportSource = Report;
                                            crystalReportViewer1.Refresh();
                                         
                                        }
                                        catch (Exception ex)
                                        {
                                            throw ex;
                                        }
                                    }
                                    else if (cmbA4.Text.ToString() == "40 x 10")
                                    {
                                        //IndiRpt4010 rpt = new IndiRpt4010();
                                        //ReportDocument rptDoc = new ReportDocument();
                                        try
                                        {
                                            _DS.Tables.Add(ds);
                                            IndiRpt4010 Report = new IndiRpt4010();
                                            Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                            crystalReportViewer1.ReportSource = Report;
                                            crystalReportViewer1.Refresh();
                                          
                                        }
                                        catch (Exception ex)
                                        {
                                            throw ex;
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please enter serial number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);                                
                                    //err.ErrorMsg = "Please enter serial number.";
                                    //goto Err;
                                }
                            }
                            else if (rdbDoc.Checked == true)
                            {
                                //without item doc no
                                string doc = txtdocno.Text.ToString();
                                DataTable dtser = CHNLSVC.MsgPortal.Barcodeserdata(company, doc);
                                if (dtser.Rows.Count > 0)
                                {
                                    int copys = numberofcop;
                                    DataTable mdt = dtser;

                                    for (int i = 2; i <= copys; i++)
                                    {
                                        mdt = mdt.Copy();
                                        mdt.Merge(dtser);
                                    }
                                    int k = 0;
                                    foreach (var sdt in mdt.Rows)
                                    {
                                        if (mdt.Rows[k]["ins_ser_1"].ToString() == "N/A")
                                        {
                                            mdt.Rows[k]["ins_ser_1"] = null;
                                        }
                                        ds.Rows.Add(mdt.Rows[k]["ins_itm_cd"], mdt.Rows[k]["ins_ser_1"], "", "", comdesc);
                                        k++;
                                    }
                                }

                                if (cmbA4.Text.ToString() == "60 x 15")
                                {

                                    //IndiRpt6015 rpt = new IndiRpt6015();
                                    //ReportDocument rptDoc = new ReportDocument();
                                    try
                                    {
                                        _DS.Tables.Add(ds);
                                        IndiRpt6015 Report = new IndiRpt6015();
                                        Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                        crystalReportViewer1.ReportSource = Report;
                                        crystalReportViewer1.Refresh();                                        
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }
                                }
                                else if (cmbA4.Text.ToString() == "70 x 30")
                                {
                                    //IndiRpt7030 rpt = new IndiRpt7030();
                                    //ReportDocument rptDoc = new ReportDocument();
                                    try
                                    {
                                        _DS.Tables.Add(ds);
                                        IndiRpt7030 Report = new IndiRpt7030();
                                        Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                        crystalReportViewer1.ReportSource = Report;
                                        crystalReportViewer1.Refresh(); 
                                        
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }
                                }
                                else if (cmbA4.Text.ToString() == "40 x 15")
                                {
                                    //IndiRpt4015 rpt = new IndiRpt4015();
                                    //ReportDocument rptDoc = new ReportDocument();
                                    try
                                    {
                                        _DS.Tables.Add(ds);
                                        IndiRpt4015 Report = new IndiRpt4015();
                                        Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                        crystalReportViewer1.ReportSource = Report;
                                        crystalReportViewer1.Refresh(); 
                                      
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }
                                }
                                else if (cmbA4.Text.ToString() == "40 x 10")
                                {
                                    //IndiRpt4010 rpt = new IndiRpt4010();
                                    //ReportDocument rptDoc = new ReportDocument();
                                    try
                                    {
                                        _DS.Tables.Add(ds);
                                        IndiRpt4010 Report = new IndiRpt4010();
                                        Report.Database.Tables["SERDATA"].SetDataSource(ds);
                                        crystalReportViewer1.ReportSource = Report;
                                        crystalReportViewer1.Refresh();
                                       
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }
                                }




                            }
                            else
                            {
                                MessageBox.Show("Invalid barcode genarate type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                //err.ErrorMsg = "Invalid barcode genarate type.";
                                //goto Err;
                            }
                        //}
                        //else
                        //{
                        //    err.ErrorMsg = "Please select barcode type to print.";
                        //    goto Err;
                        //}

                    }
                

            }
             else
            {
                    MessageBox.Show("Please select no of copies.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

                
          }
          else
          {
               MessageBox.Show("Please select print size.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          }
            //-----------------------------
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            chbItem.Checked = false;
            txtBarcodeItem.Clear();
            txtNumofSer.Clear();
            txtprefix.Clear();
            txtprefixnum.Clear();
            txtstartserno.Clear();
            txtendserno.Clear();
            txtdocno.Clear();
            txtnumofcopiesser.Clear();

            this.Close();
            WareHouseBarCode m = new WareHouseBarCode();
            m.Show();
        }

        private void btnText_Click(object sender, EventArgs e)
        {
            //if ((cmbA4.SelectedIndex > 0) || (cmbLabel.SelectedIndex > 0))
            //{
            //    errorProvider1.SetError(cmbLabel, "Please select Size");
            //}
            //errorProvider1.Dispose();
            string expirDate = string.Empty;
            string IsExpir = string.Empty;
            // bool IsExpir=false ;
            string _item = string.Empty;
            string _item2 = string.Empty;
            string _batchno = string.Empty;
            string _Encodevalue = string.Empty;
            string _Encodevalue2 = string.Empty;
            _item = txtBarcodeItem.Text;
            _item2 = txtdocno.Text;
            //foreach (DataGridViewRow Datarow in dataGridView1.Rows)
            //{
                //if (Convert.ToBoolean(Datarow.Cells["chkBxSelect"].FormattedValue))
                //{
                    //txtPrice.Text = "";
                    //int row = Datarow.Index;
                    //string _Encodevalue = string.Empty;
                    //if (Datarow.Cells[0].Value != null && Datarow.Cells[1].Value != null)
                    //{
                    //    _item = Datarow.Cells["col_itemcode"].Value.ToString();
                    //    _batchno = Datarow.Cells["col_batch_no"].Value.ToString();
                    //    txtPrintValue.Text = Datarow.Cells["col_qty"].Value.ToString();
                    //    txtItemName.Text = Datarow.Cells["col_itemName"].Value.ToString();
                    //    txtCompany.Text = Datarow.Cells["com_Company"].Value.ToString();
                    //    expirDate = Datarow.Cells["col_itb_exp_dt"].Value.ToString();
                    //    IsExpir = Datarow.Cells["col_IsExpir"].Value.ToString();
                    //    string Company = Datarow.Cells["com_Company"].Value.ToString();

                    //    // _Encodevalue = "99999999999";
                    //    //_Encodevalue = 099999999999"010307720301";
                    //    //txtPrintValue.Text = "20";
                    //    Price(_item);
                    //    // txtPrice.Text = "1500";
                    //    txtCompany.Text = Company;
                    //    if (txtPrice.Text == "")
                    //    {
                    //        MessageBox.Show("no price define" + _item, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        return;
                    //    }
                    //}

                    // btnEncode_Click(null, null);

                    //if (PrintType == "265 x 143")
                    //{
                    //    //comented by kapila on 22/2/2017
                    //    //if (_batchno != "0")
                    //    //{
                    //    //    _Encodevalue = _item + _batchno;
                    //    //}
                    //    //else
                    //    //{
                    //    _Encodevalue = _item;
                    //    //}
                    //    EncodeImage_design(_Encodevalue);
                    //    Image EncodeImage = BarcodepictureBox.Image;

                    //    #region 256x143
                    //    // EncodeImage.Save(@"D:\\Barcode4.jpg");
                    //    using (Graphics graphics = Graphics.FromImage(EncodeImage))
                    //    {
                    //        if (checkBoxCompany.Checked)
                    //        {
                    //            using (Font arialFont = new Font("Nottke", 12, FontStyle.Bold))
                    //            {
                    //                int x = 100, y = 30;
                    //                DrawRotatedTextAt(graphics, 0, txtCompany.Text,
                    //                    x, y, arialFont, Brushes.Black);
                    //            }
                    //        }
                    //        if (checkBoxItem.Checked)
                    //        {
                    //            using (Font arialFont = new Font("Nottke", 10, FontStyle.Bold))//optima//Courier New
                    //            {
                    //                int x = 8, y = 130;
                    //                DrawRotatedTextAt(graphics, 0, txtItemName.Text,
                    //                    x, y, arialFont, Brushes.Black);
                    //            }
                    //        }
                    //        if (checkBoxPrint.Checked)
                    //        {
                    //            using (Font arialFont = new Font("Nottke", 8, FontStyle.Bold))//Dotum
                    //            {
                    //                txtPrice.Text = Convert.ToDecimal(txtPrice.Text).ToString("#,##0.00");
                    //                int x = 10, y = 60;
                    //                DrawRotatedTextAt(graphics, 0, "Rs." + txtPrice.Text,
                    //                    x, y, arialFont, Brushes.Black);
                    //            }
                    //        }
                    //        if (IsExpir == "1")
                    //        {
                    //            DateTime firstdate = Convert.ToDateTime(expirDate);
                    //            using (Font arialFont = new Font("Nottke", 7))//Dotum
                    //            {
                    //                string firstDateString = firstdate.ToString("MM-dd-yyyy");
                    //                int x = 150, y = 60;
                    //                DrawRotatedTextAt(graphics, 0, "Ex." + firstDateString,
                    //                    x, y, arialFont, Brushes.Black);
                    //            }
                    //        }
                    //        graphics.SmoothingMode = SmoothingMode.HighQuality;
                    //        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    //        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    //        graphics.CompositingQuality = CompositingQuality.HighQuality;
                    //    }
                    //    // EncodeImage.Save(@"D:\\Barcode2.jpg");
                    //    BarcodepictureBox.Image = EncodeImage;
                    //    BarcodepictureBox.Height = EncodeImage.Height;
                    //    BarcodepictureBox.Width = EncodeImage.Width;
                    //    button1_Click(null, null);
                    //    #endregion
                    //    btnText.Enabled = false;
                    //}
                    if (PrintType == "40 x 10")
                    {
                        _Encodevalue = _item;
                        _Encodevalue2 = _item2;
                        EncodeImage_design(_Encodevalue, _Encodevalue2);
                        Image EncodeImage = BarcodepictureBox.Image;

                        #region 136x50
                        if (EncodeImage != null)
                        {
                            // EncodeImage.RotateFlip(RotateFlipType.Rotate90FlipXY);
                            // EncodeImage.Save(@"D:\\Barcode4.jpg");
                            using (Graphics graphics = Graphics.FromImage(EncodeImage))
                            {

                                if (checkBoxPrint.Checked)
                                {
                                    using (Font arialFont = new Font("Nottke ", 7))//Dotum
                                    {
                                        //txtPrice.Text = Convert.ToDecimal(txtPrice.Text).ToString("#,##0.00");
                                        //// int x = 0, y = 50;
                                        //int x = 5, y = 1;
                                        //DrawRotatedTextAt(graphics, 0, txtPrice.Text,
                                        //    x, y, arialFont, Brushes.Black);


                                        //graphics.SmoothingMode = SmoothingMode.HighQuality;
                                        //graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                        //graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                        //graphics.CompositingQuality = CompositingQuality.HighQuality;
                                    }
                                }
                                if (IsExpir == "1")
                                {
                                    DateTime firstdate = Convert.ToDateTime(expirDate);
                                    using (Font arialFont = new Font("Nottke", 6))//Dotum
                                    {
                                        string firstDateString = firstdate.ToString("MM-dd-yyyy");
                                        int x = 43, y = 1;
                                        DrawRotatedTextAt(graphics, 0, "Ex." + firstDateString,
                                            x, y, arialFont, Brushes.Black);
                                        //graphics.SmoothingMode = SmoothingMode.HighQuality;
                                        //graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                        //graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                        //graphics.CompositingQuality = CompositingQuality.HighQuality;
                                    }
                                }
                                graphics.SmoothingMode = SmoothingMode.HighQuality;
                                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                graphics.CompositingQuality = CompositingQuality.HighQuality;

                                graphics.CompositingMode = CompositingMode.SourceCopy;
                                //graphics.CompositingQuality = CompositingQuality.HighQuality;
                                //graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                //graphics.SmoothingMode = SmoothingMode.HighQuality;
                                //graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            }
                            //  EncodeImage.Save(@"D:\\Barcode2.jpg");
                            BarcodepictureBox.Image = EncodeImage;
                            BarcodepictureBox.Height = EncodeImage.Height;
                            BarcodepictureBox.Width = EncodeImage.Width;
                            ListImage136x50(EncodeImage);
                        #endregion
                            btnText.Enabled = false;
                        }
                    }
                    //else if (PrintType == "265 x 113")
                    //{
                    //    //if (_batchno != "0")
                    //    //{
                    //    //    _Encodevalue = _item + _batchno;
                    //    //}
                    //    //else
                    //    //{
                    //    _Encodevalue = _item;
                    //    //    }
                    //    EncodeImage_design(_Encodevalue);
                    //    Image EncodeImage = BarcodepictureBox.Image;

                    //    #region 256x143
                    //    //EncodeImage.Save(@"D:\\Barcode4.jpg");
                    //    using (Graphics graphics = Graphics.FromImage(EncodeImage))
                    //    {
                    //        if (checkBoxCompany.Checked)
                    //        {
                    //            using (Font arialFont = new Font("Nottke", 12, FontStyle.Bold))
                    //            {
                    //                int x = 100, y = 10;
                    //                DrawRotatedTextAt(graphics, 0, txtCompany.Text,
                    //                    x, y, arialFont, Brushes.Black);
                    //            }
                    //        }
                    //        if (checkBoxItem.Checked)
                    //        {
                    //            using (Font arialFont = new Font("Nottke", 10, FontStyle.Bold))//optima//Courier New
                    //            {
                    //                int x = 5, y = 100;
                    //                DrawRotatedTextAt(graphics, 0, txtItemName.Text,
                    //                    x, y, arialFont, Brushes.Black);
                    //            }
                    //        }
                    //        if (checkBoxPrint.Checked)
                    //        {
                    //            using (Font arialFont = new Font("Nottke", 10, FontStyle.Bold))//Dotum
                    //            {
                    //                txtPrice.Text = Convert.ToDecimal(txtPrice.Text).ToString("#,##0.00");
                    //                int x = 10, y = 20;
                    //                DrawRotatedTextAt(graphics, 0, "Rs." + txtPrice.Text,
                    //                    x, y, arialFont, Brushes.Black);
                    //            }
                    //        }
                    //        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    //        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    //        graphics.CompositingQuality = CompositingQuality.HighQuality;
                    //    }

                    //    //EncodeImage.Save(@"D:\\Barcode2.jpg");

                    //    BarcodepictureBox.Image = (Bitmap)EncodeImage;
                    //    //button1_Click(null, null);



                    //    Bitmap test = (Bitmap)EncodeImage;
                    //    test.SetResolution(300, 300);
                    //    BarcodepictureBox.Image = test;
                    //    BarcodepictureBox.Height = EncodeImage.Height;
                    //    BarcodepictureBox.Width = EncodeImage.Width;
                    //    button1_Click(null, null);
                    //    // byte[] bitmap = EncodeImage;
                    //    // string ZPLImageDataString = BitConverter.ToString;
                    //    // ImageConverter converter = new ImageConverter();
                    //    // byte[] bitmap = (byte[])converter.ConvertTo(BarcodepictureBox.Image, typeof(byte[]));
                    //    // string ZPLImageDataString = BitConverter.ToString(bitmap);


                    //    //// string test = ASCIIEncoding.ASCII.GetString(bitmap);

                    //    // Byte[] buffer = new byte[ZPLImageDataString.Length];
                    //    // buffer = System.Text.Encoding.ASCII.GetBytes(ZPLImageDataString);

                    //    //PrintDialog pd = new PrintDialog();
                    //    //pd.PrinterSettings = new PrinterSettings();
                    //    //if (DialogResult.OK == pd.ShowDialog(this))
                    //    //{
                    //    //    // Send a printer-specific to the printer.

                    //    //}

                    //    #endregion
                    //    btnText.Enabled = false;
                    //}



                    // Image BlankImage = DrawFilledRectangle(EncodeImage.Width + 20, EncodeImage.Height + 100);
                    // Image BlankImage = DrawFilledRectangle(EncodeImage.Width + 20, EncodeImage.Height + 100);

                    //using (Graphics graphics = Graphics.FromImage(BlankImage))
                    //{
                    //    graphics.FillRectangle(Brushes.White,0, 0, BlankImage.Width, BlankImage.Height);
                    //}
                    //BlankImage.Save(@"D:\\BlankImage.jpg");

                    //Bitmap EncodeImagewithText = new Bitmap(BlankImage.Width, BlankImage.Height);

                    //using (Graphics g = Graphics.FromImage(EncodeImagewithText))
                    //{
                    //    g.DrawImage(EncodeImage, new Point(30, 60));//20,60
                    //    g.DrawImage(BlankImage, EncodeImage.Width, 0);

                    //}

                    //Bitmap first = new Bitmap(BlankImage);
                    //Bitmap second = SetImageOpacity(EncodeImage, 0.5f);
                    //Bitmap result = new Bitmap(Math.Max(first.Width, second.Width), Math.Max(first.Height, second.Height));
                    //Graphics testnew = Graphics.FromImage(result);
                    //testnew.DrawImageUnscaled(first, 0, 0);
                    //testnew.DrawImageUnscaled(second, 0, 0);
                    //result.Save(@"D:\\result.jpg");


                    //using (Graphics graphics = Graphics.FromImage(EncodeImage))
                    //{
                    //    if (checkBoxCompany.Checked)
                    //    {
                    //        using (Font arialFont = new Font("Century Schoolbook", 12))
                    //        {
                    //            int x = 100, y = 30;
                    //            DrawRotatedTextAt(graphics, 0, txtCompany.Text,
                    //                x, y, arialFont, Brushes.Black);
                    //        }
                    //    }
                    //    if (checkBoxItem.Checked)
                    //    {
                    //        using (Font arialFont = new Font("Courier New", 10))//optima
                    //        {
                    //            int x = 10, y = 110;
                    //            DrawRotatedTextAt(graphics, 0, txtItemName.Text,
                    //                x, y, arialFont, Brushes.Black);
                    //        }
                    //    }
                    //    if (checkBoxPrint.Checked)
                    //    {
                    //        using (Font arialFont = new Font("Microsoft Sans Serif", 10))//Dotum
                    //        {
                    //            int x = 28, y = 25;
                    //            DrawRotatedTextAt(graphics, 90, "R s."+ txtPrice.Text +"/-",
                    //                x, y, arialFont, Brushes.Black);
                    //        }
                    //    }
                    //}

                    // Image test = Transparent2Color(EncodeImagewithText, Color.White);


                    //if (cmbA4.SelectedIndex > 0)
                    //{
                    //    IsEncode = "Cmb4";
                    //    radioButtonLabel.Enabled = false;
                    //}
                    //else
                    //{
                    //    IsEncode = "lbl";
                    //    radioButtonA4.Enabled = false;
                    //}
                //}
            //}
        }
        private void EncodeImage_design(string _Encodevalue, string _Encodevalue2)
        {
            //errorProvider1.Clear();
            int W = Convert.ToInt32(BWidth);
            int H = Convert.ToInt32(BHeight);
            int W2 = Convert.ToInt32(BWidth);
            int H2 = Convert.ToInt32(BHeight);
            //int W = Convert.ToInt32(265);
            //int H = Convert.ToInt32(145);
            b.Alignment = BarcodeLib.AlignmentPositions.CENTER;
            //b.Alignment = BarcodeLib.AlignmentPositions.LEFT;

            //barcode alignment
            //switch (cbBarcodeAlign.SelectedItem.ToString().Trim().ToLower())
            //{
            //    case "left": b.Alignment = BarcodeLib.AlignmentPositions.LEFT; break;
            //    case "right": b.Alignment = BarcodeLib.AlignmentPositions.RIGHT; break;
            //    default: b.Alignment = BarcodeLib.AlignmentPositions.CENTER; break;
            //}//switch

            BarcodeLib.TYPE type = BarcodeLib.TYPE.UNSPECIFIED;
            switch (cbEncodeType.SelectedItem.ToString().Trim())
            {
                case "UPC-A": type = BarcodeLib.TYPE.UPCA; break;
                case "UPC-E": type = BarcodeLib.TYPE.UPCE; break;
                case "UPC 2 Digit Ext.": type = BarcodeLib.TYPE.UPC_SUPPLEMENTAL_2DIGIT; break;
                case "UPC 5 Digit Ext.": type = BarcodeLib.TYPE.UPC_SUPPLEMENTAL_5DIGIT; break;
                case "EAN-13": type = BarcodeLib.TYPE.EAN13; break;
                case "JAN-13": type = BarcodeLib.TYPE.JAN13; break;
                case "EAN-8": type = BarcodeLib.TYPE.EAN8; break;
                case "ITF-14": type = BarcodeLib.TYPE.ITF14; break;
                case "Codabar": type = BarcodeLib.TYPE.Codabar; break;
                case "PostNet": type = BarcodeLib.TYPE.PostNet; break;
                case "Bookland/ISBN": type = BarcodeLib.TYPE.BOOKLAND; break;
                case "Code 11": type = BarcodeLib.TYPE.CODE11; break;
                case "Code 39": type = BarcodeLib.TYPE.CODE39; break;
                case "Code 39 Extended": type = BarcodeLib.TYPE.CODE39Extended; break;
                case "Code 39 Mod 43": type = BarcodeLib.TYPE.CODE39_Mod43; break;
                case "Code 93": type = BarcodeLib.TYPE.CODE93; break;
                case "LOGMARS": type = BarcodeLib.TYPE.LOGMARS; break;
                case "MSI": type = BarcodeLib.TYPE.MSI_Mod10; break;
                case "Interleaved 2 of 5": type = BarcodeLib.TYPE.Interleaved2of5; break;
                case "Standard 2 of 5": type = BarcodeLib.TYPE.Standard2of5; break;
                case "Code 128": type = BarcodeLib.TYPE.CODE128; break;
                case "Code 128-A": type = BarcodeLib.TYPE.CODE128A; break;
                case "Code 128-B": type = BarcodeLib.TYPE.CODE128B; break;
                case "Code 128-C": type = BarcodeLib.TYPE.CODE128C; break;
                case "Telepen": type = BarcodeLib.TYPE.TELEPEN; break;
                case "FIM": type = BarcodeLib.TYPE.FIM; break;
                case "Pharmacode": type = BarcodeLib.TYPE.PHARMACODE; break;
                default: MessageBox.Show("Please specify the encoding type."); break;
            }//switch

            try
            {
                if (type != BarcodeLib.TYPE.UNSPECIFIED)
                {
                    b.IncludeLabel = this.chkGenerateLabel.Checked;

                    b.RotateFlipType = (RotateFlipType)Enum.Parse(typeof(RotateFlipType), this.cbRotateFlip.SelectedItem.ToString(), true);

                    //label alignment and position
                    switch (this.cbLabelLocation.SelectedItem.ToString().Trim().ToUpper())
                    {
                        case "BOTTOMLEFT": b.LabelPosition = BarcodeLib.LabelPositions.BOTTOMLEFT; break;
                        case "BOTTOMRIGHT": b.LabelPosition = BarcodeLib.LabelPositions.BOTTOMRIGHT; break;
                        case "TOPCENTER": b.LabelPosition = BarcodeLib.LabelPositions.TOPCENTER; break;
                        case "TOPLEFT": b.LabelPosition = BarcodeLib.LabelPositions.TOPLEFT; break;
                        case "TOPRIGHT": b.LabelPosition = BarcodeLib.LabelPositions.TOPRIGHT; break;
                        default: b.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER; break;
                    }//switch

                    //===== Encoding performed here =====
                    //barcode.BackgroundImage = b.Encode(type, this.txtData.Text.Trim(), this.btnForeColor.BackColor, this.btnBackColor.BackColor, W, H);
                   // BarcodepictureBox.Image = b.Encode(type, _Encodevalue, this.btnForeColor.BackColor, this.btnBackColor.BackColor, W, H);
                    Image image1 = b.Encode(type, _Encodevalue, this.btnForeColor.BackColor, this.btnBackColor.BackColor, W, H);
                    Image image2 = b.Encode(type, _Encodevalue2, this.btnForeColor.BackColor, this.btnBackColor.BackColor, W, H);

                    //Bitmap bitmap = new Bitmap(image1.Width + image2.Width, Math.Max(image1.Height, image2.Height));
                    Bitmap bitmap = new Bitmap(Math.Max(image1.Width, image2.Width), image1.Height + image2.Height );
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.DrawImage(image1, 0, 0);
                        //g.DrawImage(image2, image1.Width, 0);
                        g.DrawImage(image2, 0, image1.Height);
                    }
                    BarcodepictureBox.Image = bitmap;
                    //dilshan **********************
                    //float drawBorderX = 5;
                    //float drawBorderY = 5;
                    //Bitmap barCode = new Bitmap(b.Encode(type, _Encodevalue, this.btnForeColor.BackColor, this.btnBackColor.BackColor, W, H));
                    //Bitmap text = new Bitmap(b.Encode(type, _Encodevalue2, this.btnForeColor.BackColor, this.btnBackColor.BackColor, W2, H2));
                    ////Set up our two images
                    ////Bitmap barCode = Code128Rendering.MakeBarcodeImage(textBox1.Text, 2, true);
                    ////Bitmap barCode = b.Encode(type, _Encodevalue, this.btnForeColor.BackColor, this.btnBackColor.BackColor, W, H);
                    ////Bitmap text = new Bitmap(barCode.Width, 50);
                    //Graphics textGraphics = Graphics.FromImage(text);


                    ////Draw the text to the bottom image.
                    //FontStyle sitil = FontStyle.Bold;
                    //Font fonts = new Font(new FontFamily("Arial"), 10, sitil);
                    //textGraphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, text.Width, text.Height));
                    //textGraphics.DrawString(textBox1.Text, fonts, Brushes.Black, drawBorderX, drawBorderY);

                    ////Vertically concatenate the two images.
                    //Bitmap resultImage = new Bitmap(Math.Max(barCode.Width, text.Width), barCode.Height + text.Height);
                    //Graphics g = Graphics.FromImage(resultImage);
                    //g.DrawImage(barCode, 0, 0);
                    //g.DrawImage(text, 0, barCode.Height);

                    //BarcodepictureBox.Image = resultImage;
                    //******************************
                    //))))))))))))))))))
                    //))))))))))))))))))

                    //===================================

                    //show the encoding time
                    this.lblEncodingTime.Text = "(" + Math.Round(b.EncodingTime, 0, MidpointRounding.AwayFromZero).ToString() + "ms)";

                    txtEncoded.Text = b.EncodedValue;

                    // tsslEncodedType.Text = "Encoding Type: " + b.EncodedType.ToString();
                }//if

                //reposition the barcode image to the middle
                //barcode.Location = new Point((this.barcode.Location.X + this.barcode.Width / 2) - barcode.Width / 2, (this.barcode.Location.Y + this.barcode.Height / 2) - barcode.Height / 2);
            }//try
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }//catch
        }
        private void DrawRotatedTextAt(Graphics gr, float angle, string txt, int x, int y, Font the_font, Brush the_brush)
        {
            // Save the graphics state.
            GraphicsState state = gr.Save();
            gr.ResetTransform();

            // Rotate.
            gr.RotateTransform(angle);

            // Translate to desired position. Be sure to append
            // the rotation so it occurs after the rotation.
            gr.TranslateTransform(x, y, MatrixOrder.Append);

            // Draw the text at the origin.
            gr.DrawString(txt, the_font, the_brush, 0, 0);

            // Restore the graphics state.
            gr.Restore(state);
        }

        private void WareHouseBarCode_Load(object sender, EventArgs e)
        {
            //AddHeaderCheckBox();
            //BindGridView(string.Empty, 0);
            //GetPriceBook();
            //GetDefBook();
            //  GetDefLevel();
            //this.cmbLabel.SelectedIndex = 0;
            this.cmbA4.SelectedIndex = 0;
            //this.cbEncodeType.SelectedIndex = 21;
            this.cbBarcodeAlign.SelectedIndex = 0;
            this.cbLabelLocation.SelectedIndex = 0;
            this.cbRotateFlip.DataSource = System.Enum.GetNames(typeof(RotateFlipType));

            int i = 0;
            foreach (object o in cbRotateFlip.Items)
            {
                if (o.ToString().Trim().ToLower() == "rotatenoneflipnone")
                    break;
                i++;
            }//foreach
            this.cbRotateFlip.SelectedIndex = i;
            this.btnBackColor.BackColor = this.b.BackColor;
            this.btnForeColor.BackColor = this.b.ForeColor;


            //HeaderCheckBox.KeyUp += new KeyEventHandler(HeaderCheckBox_KeyUp);
            //HeaderCheckBox.MouseClick += new MouseEventHandler(HeaderCheckBox_MouseClick);
            //dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dgvSelectAll_CellValueChanged);
            //dataGridView1.CurrentCellDirtyStateChanged += new EventHandler(dgvSelectAll_CurrentCellDirtyStateChanged);
            //dataGridView1.CellPainting += new DataGridViewCellPaintingEventHandler(dgvSelectAll_CellPainting);
        }
        private void ListImage136x50(Image _img)
        {
            ImageList.ImageSize = new Size(136, 50);
            //ImageList.ImageSize = new Size(256, 256);
            txtPrintValue.Text = txtnumofcopiesser.Text;
            int value = Convert.ToInt32(txtPrintValue.Text);
            for (int i = 0; i < value; i++)
            {
                _img.RotateFlip(RotateFlipType.Rotate180FlipXY);
                ImageList.Images.Add(N.ToString(), _img);

            }

            int count = ImageList.Images.Count;

            lblImagecount.Text = count.ToString();

            Bitmap imageSource2 = new Bitmap(this.ImageList.Images[0]);
            //imageSource2.Save(@"D:\\Barcode7.jpg");

            N++;
        }

        private void cmbA4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            N = 0;
            BWidth = 0;
            BHeight = 0;
            noofPages = 0;
            lastpageitems = 0;
            repeatTimes1 = 0;
            nPage = 0; k = 0;
            pointNo = 0;
            ImageNo = 0;
            count = 0;
            if (cmbA4.Text == "40 x 10")
            {
                //BWidth = 265;
                //BHeight = 145;
                //PrintType = "40 x 10";
                //b.Newvalue = 80;
                //b.Leftsidepadding = 0;
                //b.Bottomsidepadding = 20;
                //b.LabelFont = new Font("Perpetua", 10, FontStyle.Bold);
                //checkBoxCompany.Visible = true;
                //checkBoxItem.Visible = true;
                BWidth = 290;//136;
                BHeight = 70;
                PrintType = "40 x 10";
                b.Newvalue = 18;
                b.Leftsidepadding = 0;
                b.Bottomsidepadding = 5;
                b.LabelFont = new Font("Nottke", 8, FontStyle.Regular);
                checkBoxCompany.Visible = false;
                checkBoxItem.Visible = false;
            }
            if (cmbA4.Text == "40 x 15")
            {
                BWidth = 945;
                BHeight = 355;
                PrintType = "40 x 15";
                b.Newvalue = 18;
                b.Leftsidepadding = 0;
                b.Bottomsidepadding = 5;
                b.LabelFont = new Font("Nottke", 8, FontStyle.Regular);
                checkBoxCompany.Visible = false;
                checkBoxItem.Visible = false;
            }
            if (cmbA4.Text == "60 x 15")
            {
                BWidth = 1418;
                BHeight = 355;
                PrintType = "60 x 15";
                b.Newvalue = 80;
                b.Leftsidepadding = 0;
                b.Bottomsidepadding = 20;
                b.LabelFont = new Font("Perpetua", 10, FontStyle.Bold);
                checkBoxCompany.Visible = true;
                checkBoxItem.Visible = true;
            }
            if (cmbA4.Text == "70 x 30")
            {
                BWidth = 800;
                BHeight = 350;
                PrintType = "70 x 30";
                b.Newvalue = 18;
                b.Leftsidepadding = 0;
                b.Bottomsidepadding = 5;
                b.LabelFont = new Font("Nottke", 8, FontStyle.Regular);
                checkBoxCompany.Visible = false;
                checkBoxItem.Visible = false;
            }
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            // clear();
            // BaseCls.GlbReportDoc = "";
            this.Close();
            WareHouseBarCode m = new WareHouseBarCode();
            m.Show();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printbar();
            //print();
        }
        private void printbar()
        {
            if (PrintType == "40 x 10")
            {
                int count = ImageList.Images.Count;
                //decimal repeatTimest = decimal.Parse(txtPrintValue.Text);
                int s = (count % 100);// int s = (count % 105);
                int ss = (count / 100);// int s = (count % 105);
                noofPages = Convert.ToInt32(ss);
                lastpageitems = Convert.ToInt32(s);
                Image136x50();
                printSetting136x50();


            }
        }
        private void Image136x50()
        {
            ImageList2 = new ImageList();
            for (int count = 0; count < ImageList.Images.Count; count++)
            {
                Image imageSource1 = ImageList.Images[count];
                //Bitmap myCombinationImage1 = new Bitmap(imageSource1.Width, imageSource1.Height );
                //using (Graphics graphics = Graphics.FromImage(myCombinationImage1))
                //{
                //    Point newLocation = new Point(0, 0);
                //    for (int imageIndex = 0; imageIndex < 1; imageIndex++)
                //    {
                //        graphics.DrawImage(imageSource1, newLocation);
                //        newLocation = new Point(newLocation.X, newLocation.Y + imageSource1.Height);
                //    }
                //}
                ImageList2.ImageSize = new Size(136, 50);
                ImageList2.ImageSize = new Size(256, 50);
                ImageList2.Images.Add(count.ToString(), imageSource1);

                //Image original = Image.FromFile(@"C:\path\to\some.jpg");
                //Image resized = ResizeImage(original, new Size(1024, 768));
                //MemoryStream memStream = new MemoryStream();
                //resized.Save(memStream, ImageFormat.Jpeg);
            }

        }
        private void printSetting136x50()
        {

            PrintDocument doc = new PrintDocument();
            doc.PrintPage += this.printDocumentpoints136x50_PrintPage;
            PaperSize ps = new PaperSize();
            Margins margins = new Margins(0, 0, 0, 0);


            ps.RawKind = (int)PaperKind.A4;
            doc.DefaultPageSettings.PaperSize = ps;
            // doc.DefaultPageSettings.Landscape = true;
            doc.PrinterSettings.DefaultPageSettings.Margins = margins;
            PrintDialog dlgSettings = new PrintDialog();
            PrintPreviewDialog printPrvDlg = new PrintPreviewDialog();

            // preview the assigned document or you can create a different previewButton for it


            dlgSettings.Document = doc;
            dlgSettings.Document.PrinterSettings.PrintRange = PrintRange.AllPages;
            if (dlgSettings.ShowDialog() == DialogResult.OK)
            {
                //printPrvDlg.Document = doc;
                //printPrvDlg.ShowDialog();
                doc.Print();
            }

            nPage = 0;
            count = 0;
            pointNo = 0;
            ImageNo = 0;

        }
        private void printDocumentpoints136x50_PrintPage(object sender, PrintPageEventArgs e)
        {
            if ((nPage < noofPages))
            {
                switch (nPage)
                {
                    default:
                        while (count < 100)//105
                        {

                            e.Graphics.DrawImage(ImageList2.Images[ImageNo], points136x50[pointNo]);

                            count++;
                            pointNo++;
                            ImageNo++;
                        }
                        break;
                }
            }
            if (pointNo == 100)//105
            {
                pointNo = 0;
            }

            if (nPage < noofPages)
            {
                //if last page
                if (((nPage + 1) == noofPages) && (lastpageitems == 0))
                {
                    e.HasMorePages = false;
                }
                else
                {
                    e.HasMorePages = true;
                    ++nPage;
                    count = 0;
                }


                //if (lastpageitems == 0)
                //{
                //    e.HasMorePages = false;
                //    //return;
                //}
                //else
                //{
                //    e.HasMorePages = true;
                //    ++nPage;
                //    count = 0;
                //}

            }
            else if (lastpageitems < 100)
            {
                for (int i = 0; i < lastpageitems; i++)
                {
                    e.Graphics.DrawImage(ImageList2.Images[ImageNo], points136x50[i]);
                    ImageNo++;

                }
                e.HasMorePages = false;
                return;
            }
            else
            {
                e.HasMorePages = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string barCode = txtdocno.Text;
            Bitmap bitMap = new Bitmap(barCode.Length * 42, 140);
            // Bitmap bitMap = new Bitmap(265 ,143);

            using (Graphics graphics = Graphics.FromImage(bitMap))
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                // Font oFont = new Font("IDAutomationHC39M", 16);
                // Font oFont = new Font("Code 128", 48);
                Font oFont = new Font("IDAutomationHbC128S", 16);
                PointF point = new PointF(50f, 50f);
                SolidBrush blackBrush = new SolidBrush(Color.Black);
                SolidBrush whiteBrush = new SolidBrush(Color.White);
                graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
            }
            using (Graphics graphics = Graphics.FromImage(bitMap))
            {
                if (checkBoxCompany.Checked)
                {
                    using (Font arialFont = new Font("Century Schoolbook", 12))
                    {
                        int x = 100, y = 30;
                        DrawRotatedTextAt(graphics, 0, txtCompany.Text,
                            x, y, arialFont, Brushes.Black);
                    }
                }
                if (checkBoxItem.Checked)
                {
                    using (Font arialFont = new Font("Courier New", 10))//optima
                    {
                        int x = 10, y = 110;
                        DrawRotatedTextAt(graphics, 0, txtItemName.Text,
                            x, y, arialFont, Brushes.Black);
                    }
                }
                if (checkBoxPrint.Checked)
                {
                    using (Font arialFont = new Font("Microsoft Sans Serif", 10))//Dotum
                    {
                        int x = 28, y = 25;
                        DrawRotatedTextAt(graphics, 90, "R s." + txtPrice.Text + "/-",
                            x, y, arialFont, Brushes.Black);
                    }
                }

            }
            using (MemoryStream ms = new MemoryStream())
            {

                bitMap.Save(ms, ImageFormat.Png);
                BarcodepictureBox.Image = bitMap;
                BarcodepictureBox.Height = bitMap.Height;
                BarcodepictureBox.Width = bitMap.Width;
            }
            bitMap.Save(@"D:\\Barcode8.jpg");
            button1z(null, null);
        }
        private void button1z(object sender, EventArgs e)
        {
            ImageList.ImageSize = new Size(256, BHeight);
            int value = Convert.ToInt32(txtPrintValue.Text);
            for (int i = 0; i < value; i++)
            {

                ImageList.Images.Add(N.ToString(), BarcodepictureBox.Image);
            }

            int count = ImageList.Images.Count;

            lblImagecount.Text = count.ToString();

            N++;
        }
    }
}
