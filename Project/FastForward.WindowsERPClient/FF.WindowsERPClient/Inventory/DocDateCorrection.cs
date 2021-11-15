using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Inventory
{
    public partial class DocDateCorrection : FF.WindowsERPClient.Base
    {
        public DocDateCorrection()
        {
            InitializeComponent();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            int _eff = CHNLSVC.Inventory.DocDateCorrection("INV", txtDocNo.Text, dtpCorrectDate.Value.Date, BaseCls.GlbUserID);
            if (_eff == 1)
            {

                MessageBox.Show("Updated!", "Process");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text)) return;
            DataTable _eligibleInvoice = CHNLSVC.Sales.GetUpdatableInvoiceforDiscounted(textBox1.Text.Trim().ToUpper());
            if (_eligibleInvoice != null && _eligibleInvoice.Rows.Count > 0)
                foreach (DataRow _r in _eligibleInvoice.Rows)
                {
                    string _invoice = Convert.ToString(_r["sah_inv_no"]);
                    string _pc = Convert.ToString(_r["sah_pc"]);
                    string _invtp = Convert.ToString(_r["sah_inv_tp"]);
                    DateTime _date = Convert.ToDateTime(_r["sah_dt"]).Date;
                    DateTime _crdate = Convert.ToDateTime(_r["sah_cre_when"]);
                    Int32 _timeno = Convert.ToInt32(_crdate.ToString("HHmmss"));
                    decimal _paidamt = Convert.ToDecimal(_r["sah_anal_7"]);
                    decimal _invoiceamt = Convert.ToDecimal(_r["sah_anal_8"]);

                    //Checking process for the actual applied discounted invoice
                    List<RecieptItem> _receiptItem = CHNLSVC.Sales.GetReceiptItemList(_invoice);
                    //if (_receiptItem != null && _receiptItem.Count > 1) continue;
                    //if (_receiptItem[0].Sard_pay_tp != "CRCD") continue;
                    if (_receiptItem[0].Sard_settle_amt != _invoiceamt) continue;

                    List<InvoiceItem> _returnlist = null;
                    bool _isDifferent = false;
                    decimal _tobepay = 0;
                    InvoiceHeader _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(_invoice);
                    List<InvoiceItem> _item = CHNLSVC.Sales.GetInvoiceDetailByInvoice(_invoice);
                    _item.ForEach(X => X.Sad_tot_amt = (X.Sad_tot_amt + X.Sad_disc_amt));
                    _item.ForEach(X => X.Mi_session_id = Convert.ToString(X.Sad_disc_rt));
                    _item.ForEach(X => X.Sad_disc_amt = 0);
                    _item.ForEach(X => X.Sad_disc_rt = 0);

                    CHNLSVC.Sales.GetGeneralPromotionDiscount(textBox1.Text.Trim(), _pc, _invtp, _timeno, Convert.ToDateTime(_date).DayOfWeek.ToString().ToUpper(), _date, _item, _receiptItem, out _returnlist, out _isDifferent, out _tobepay, _hdr);

                    if (!_isDifferent)
                    {
                        //foreach(InvoiceItem _i in  _item)
                        //{
                        //    PriceBookLevelRef _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(BaseCls.GlbUserComCode, x.Sad_pbook, x.Sad_pb_lvl);
                        //    CashGeneralEntiryDiscountDef GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(textBox1.Text.Trim(), _pc, Convert.ToDateTime(_date).Date, x.Sad_pbook, x.Sad_pb_lvl, _hdr.Sah_cus_cd, x.Sad_itm_cd, _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
                        //    if (GeneralDiscount != null) 
                        //    {
                        //        int _effect = CHNLSVC.Sales.UpdateDiscountRef(_invoice, x.Sad_itm_line, GeneralDiscount.Sgdd_seq, 0, "M");
                        //        if (_effect >= 1) MessageBox.Show(_invoice);
                        //    }
                        //}

                    }

                    if (_isDifferent)
                        foreach (InvoiceItem _one in _returnlist)
                        {
                            int _effect = CHNLSVC.Sales.UpdateDiscountRef(_invoice, _one.Sad_itm_line, _one.Sad_dis_seq, _one.Sad_dis_line, _one.Sad_dis_type);
                            if (_effect >= 1) MessageBox.Show(_invoice);

                        }

                }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void btnSearch_RecLocation_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
            DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = textBox2;
            _CommonSearch.ShowDialog();
            textBox2.Select();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            panel1.Size = new System.Drawing.Size(589, 384);
            DataTable _tbl = CHNLSVC.Inventory.GetLocationCat3(BaseCls.GlbUserComCode);
            if (_tbl != null && _tbl.Rows.Count > 0)
            {
                BindingSource _source = new BindingSource();
                _source.DataSource = _tbl;
                checkedListBox1.DataSource = _source.DataSource;
                ((ListBox)checkedListBox1).DisplayMember = "ml_cate_3";
            }
            if (panel1.Visible) panel1.Visible = false; else panel1.Visible = true;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable _tbl = CHNLSVC.Inventory.GetLocationByCat3(textBox3.Text.Trim(), BaseCls.GlbUserComCode);
            if (_tbl != null && _tbl.Rows.Count > 0)
            {
                listBox1.DataSource = _tbl;
                listBox1.DisplayMember = "Ml_loc_cd";

            }
        }private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P) if (pnlCorrection.Visible) pnlCorrection.Visible = false; else { pnlCorrection.Visible = true; pnlCorrection.Size = new Size(589, 144); }
        }

        private void btnSavePermission_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text)) { MessageBox.Show("select location"); return; }

            if (checkedListBox1.CheckedItems.Count <= 0)
            {
                MessageBox.Show("select the category", "Category", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
                {
                    object itemChecked = checkedListBox1.CheckedItems[i];
                    string _cat3 = ((DataRowView)itemChecked)[0].ToString();
                    CHNLSVC.Inventory.SaveTransactionCategory(BaseCls.GlbUserComCode, textBox2.Text.Trim(), BaseCls.GlbUserComCode, _cat3, BaseCls.GlbUserID);
                    MessageBox.Show("Done!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnCor_Click(object sender, EventArgs e)
        {
            var _select = (from DataGridViewRow r in gvCor.Rows where Convert.ToBoolean(r.Cells["Column3"].Value) select r).ToList();
            if (_select == null || _select.Count() <= 0)
            {
                MessageBox.Show("Please select the document", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach(var sel in _select )
            {
                string _error = string.Empty;
                string _inv = string .Empty ;
                int _e = CHNLSVC.Sales.DocumentInvoice(Convert.ToString(sel.Cells["Column2"].Value), Convert.ToString(sel.Cells["Column4"].Value), BaseCls.GlbUserID, Convert.ToDateTime(sel.Cells["Column1"].Value).Date, Convert.ToString(sel.Cells["Column6"].Value), string.Empty, Convert.ToString(sel.Cells["Column5"].Value), out _error, out _inv);

                if(!string.IsNullOrEmpty(_error))
                    MessageBox.Show (_error);

                if (_e != -1) return;

                MessageBox.Show(_inv);
                    
            }


        }

        private void btnCorF_Click(object sender, EventArgs e)
        {
            DataTable _tbl = CHNLSVC.Inventory.GetDocument();
            gvCor.AutoGenerateColumns = false;
            gvCor.DataSource = _tbl;

        }





    }
}
