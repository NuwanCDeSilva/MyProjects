using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FF.BusinessObjects;

namespace FF.WindowsERPClient.General
{
    public partial class ManualDocumentCancel : Base
    {
        #region properties

        DataTable TemDocTable;

        #endregion


        public ManualDocumentCancel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void ManualDocumentCancel_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewDocuments.AutoGenerateColumns = false;
                cmbDocumentType.Select();
                BindUserItemStatusDDLData(cmbDocumentType);
                cmbDocumentType_SelectionChangeCommitted(null, null);
                CreateTableColumns();
                dataGridViewFinalDocuments.AutoGenerateColumns = false;
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

        private void CreateTableColumns()
        {
            TemDocTable = new DataTable();
            TemDocTable.Columns.Add("BOOKNO", typeof(string));
            TemDocTable.Columns.Add("PREFIX", typeof(string));
            TemDocTable.Columns.Add("START", typeof(int));
            TemDocTable.Columns.Add("END", typeof(int));
            TemDocTable.Columns.Add("DES", typeof(bool));
            TemDocTable.Columns.Add("ITEM_CODE", typeof(string));
            TemDocTable.Columns.Add("REMARK", typeof(string));
        }

        private void BindUserItemStatusDDLData(ComboBox ddl)
        {
            DataTable itemTable = CHNLSVC.Inventory.Get_all_Items();
            //for (int i = itemTable.Rows.Count - 1; i >= 0; i--)
            //{
            //    DataRow dr = itemTable.Rows[i];
            //    if (dr["mi_cd"].ToString() == "HPRM" || dr["mi_cd"].ToString() == "HPRS")
            //        dr.Delete();
            //}

            ComboBoxDraw(itemTable, ddl, "MI_CD", "MI_SHORTDESC");
        }

        private void ComboBoxDraw(DataTable table, ComboBox combo, string code, string desc)
        {

            combo.DataSource = table;
            combo.DisplayMember = desc;
            combo.ValueMember = code;

            // Enable the owner draw on the ComboBox.
            combo.DrawMode = DrawMode.OwnerDrawVariable;
            // Handle the DrawItem event to draw the items.
            combo.DrawItem += delegate(object cmb, DrawItemEventArgs args)
            {

                // Draw the default background
                args.DrawBackground();


                // The ComboBox is bound to a DataTable,
                // so the items are DataRowView objects.
                DataRowView drv = (DataRowView)combo.Items[args.Index];

                // Retrieve the value of each column.
                string id = drv[code].ToString();
                string name = drv[desc].ToString();

                // Get the bounds for the first column
                Rectangle r1 = args.Bounds;
                r1.Width /= 2;

                // Draw the text on the first column
                using (SolidBrush sb = new SolidBrush(args.ForeColor))
                {
                    args.Graphics.DrawString(id, args.Font, sb, r1);
                }

                // Draw a line to isolate the columns 
                using (Pen p = new Pen(Color.Black))
                {
                    args.Graphics.DrawLine(p, r1.Right - 5, 0, r1.Right - 5, r1.Bottom);
                }

                // Get the bounds for the second column
                Rectangle r2 = args.Bounds;
                r2.X = args.Bounds.Width / 2;
                r2.Width /= 2;

                // Draw the text on the second column
                using (SolidBrush sb = new SolidBrush(args.ForeColor))
                {
                    args.Graphics.DrawString(name, args.Font, sb, r2);
                }

            };
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
            if (dataGridViewDocuments.SelectedRows.Count < 0)
            {
                MessageBox.Show("Please select a Document", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrEmpty(txtStart.Text))
            {
                MessageBox.Show("Please select a Document", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrEmpty(txtEnd.Text)) {
                MessageBox.Show("Please enter end document number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (dataGridViewDocuments.Rows[dataGridViewDocuments.SelectedRows[0].Index].Cells[4].Value.ToString() != txtStart.Text) {
                MessageBox.Show("Please select a Document from grid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
                string bookNo = dataGridViewDocuments.Rows[dataGridViewDocuments.SelectedRows[0].Index].Cells[1].Value.ToString();
                string prefix = dataGridViewDocuments.Rows[dataGridViewDocuments.SelectedRows[0].Index].Cells[2].Value.ToString();
                int startValue = Convert.ToInt32(dataGridViewDocuments.Rows[dataGridViewDocuments.SelectedRows[0].Index].Cells[4].Value);
                int endRange = Convert.ToInt32(dataGridViewDocuments.Rows[dataGridViewDocuments.SelectedRows[0].Index].Cells[5].Value);
                int endValue = Convert.ToInt32(txtEnd.Text);
                if (endValue >= startValue && endValue < endRange)
                {
                    DataRow[] dr = TemDocTable.Select(string.Format("{0} = {1}", "BOOKNO", bookNo));
                    if (dr.Length <= 0)
                    {
                        DataRow dataRow = TemDocTable.NewRow();
                        dataRow[0] = bookNo;
                        dataRow[1] = prefix;
                        dataRow[2] = startValue;
                        dataRow[3] = endValue;
                        dataRow[4] = false;
                        dataRow[5] = cmbDocumentType.SelectedValue.ToString();
                        dataRow[6] = txtRemark.Text;
                        TemDocTable.Rows.Add(dataRow);
                        FinalGridDataBind();
                        
                    }
                    else
                        MessageBox.Show("Item already exists","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                //user enter last value
                else if (endValue >= startValue && endValue == endRange)
                {
                    DataRow[] dr = TemDocTable.Select(string.Format("{0} ={1}", "BOOKNO", bookNo));
                    if (dr.Length <= 0)
                    {
                        DataRow dataRow = TemDocTable.NewRow();
                        dataRow[0] = bookNo;
                        dataRow[1] = prefix;
                        dataRow[2] = startValue;
                        dataRow[3] = endValue;
                        dataRow[4] = true;
                        dataRow[5] = cmbDocumentType.SelectedValue.ToString();
                        dataRow[6] = txtRemark.Text;
                        TemDocTable.Rows.Add(dataRow);
                        FinalGridDataBind();
                    }
                    else
                        MessageBox.Show("Item already exists","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("End number not in range","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                txtEnd.Text = string.Empty;
                txtStart.Text = "";
                txtRemark.Text = "";
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

        private void FinalGridDataBind()
        {
            dataGridViewFinalDocuments.AutoGenerateColumns = false;
            dataGridViewFinalDocuments.DataSource = TemDocTable;
        }

        private void btnCancelDocument_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                CancelDocument();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void CancelDocument()
        {
            try
            {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                if (cmbDocumentType.SelectedValue == null)
                {
                    MessageBox.Show("Please select document type", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (TemDocTable.Rows.Count > 0)
                {
                    string _err = "";
                    TemDocTable.TableName = "table";
                    //save to log
                    try
                    {
                        CHNLSVC.CommonSearch.StartTimeModule("CD", "", DateTime.Now, BaseCls.GlbUserDefLoca, BaseCls.GlbUserComCode, BaseCls.GlbUserID, DateTime.Now.Date);
                    }
                    catch (Exception)
                    {
                        CHNLSVC.CloseChannel();
                    }
                    bool sucess = CHNLSVC.Inventory.CancelMannualDocument(TemDocTable, BaseCls.GlbUserDefProf, BaseCls.GlbUserDefLoca, BaseCls.GlbUserID, BaseCls.GlbUserComCode, cmbDocumentType.SelectedValue.ToString(), out _err);
                    if (sucess)
                    {
                        MessageBox.Show("Successfully saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Save unsuccessful\n" + _err, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    dataGridViewDocuments.DataSource = CHNLSVC.Inventory.Get_all_manual_docs_by_type(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, cmbDocumentType.SelectedValue.ToString());
                    FinalGridDataBind();
                    Clear();
                }
                else
                {
                    MessageBox.Show("Nothing to cancel", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Error occurred while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
            //cancel process
            /*
            if (TemDocTable.Rows.Count > 0)
            {
                for (int i = 0; i < TemDocTable.Rows.Count; i++)
                {
                    try
                    {
                        if (Convert.ToBoolean(TemDocTable.Rows[i][4]))
                        {
                            if (TemDocTable.Rows[i][5].ToString() == "HPRS" || TemDocTable.Rows[i][5].ToString() == "HPRM")
                            {

                                int start = Convert.ToInt32(TemDocTable.Rows[i][2]);
                                int end = Convert.ToInt32(TemDocTable.Rows[i][3]);
                                //save reciept and reciept item for every cancel page no
                                for (; end >= start; start++)
                                {
                                    MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                                    _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                                    _receiptAuto.Aut_cate_tp = "PC";
                                    _receiptAuto.Aut_start_char = TemDocTable.Rows[i][5].ToString();
                                    _receiptAuto.Aut_direction = 1;
                                    _receiptAuto.Aut_modify_dt = null;
                                    _receiptAuto.Aut_moduleid = "HP";
                                    _receiptAuto.Aut_number = 0;
                                    _receiptAuto.Aut_year = null;
                                    string _cusNo = CHNLSVC.Sales.GetRecieptNo(_receiptAuto);

                                    //insert reciept
                                    int seqNo = CHNLSVC.Inventory.GetSerialID();
                                    RecieptHeader recieptHeadder = new RecieptHeader();
                                    recieptHeadder.Sar_seq_no = seqNo;
                                    recieptHeadder.Sar_prefix = TemDocTable.Rows[i][1].ToString();
                                    recieptHeadder.Sar_manual_ref_no = start.ToString();
                                    recieptHeadder.Sar_receipt_no = _cusNo;
                                    recieptHeadder.Sar_com_cd = BaseCls.GlbUserComCode;
                                    recieptHeadder.Sar_receipt_type = TemDocTable.Rows[i][5].ToString();
                                    recieptHeadder.Sar_receipt_date = DateTime.Now;
                                    recieptHeadder.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                                    recieptHeadder.Sar_debtor_name = "CASH";
                                    recieptHeadder.Sar_remarks = "Cancel";
                                    recieptHeadder.Sar_act = false;
                                    recieptHeadder.Sar_mod_by = BaseCls.GlbUserID;
                                    recieptHeadder.Sar_mod_when = DateTime.Now;
                                    CHNLSVC.Sales.SaveReceiptHeader(recieptHeadder);

                                    //insert reciept item
                                    RecieptItem recieptItem = new RecieptItem();
                                    recieptItem.Sard_seq_no = seqNo;
                                    recieptItem.Sard_line_no = 1;
                                    recieptItem.Sard_ref_no = start.ToString();
                                    recieptItem.Sard_receipt_no = _cusNo;
                                    recieptItem.Sard_pay_tp = "CASH";
                                    recieptItem.Sard_settle_amt = 0;
                                    CHNLSVC.Sales.SaveReceiptItem(recieptItem);
                                }

                            }
                            CHNLSVC.Inventory.UpdateGntManDocDt(BaseCls.GlbUserDefLoca, TemDocTable.Rows[i][5].ToString(), "0", Convert.ToInt32(TemDocTable.Rows[i][3]), TemDocTable.Rows[i][1].ToString(), TemDocTable.Rows[i][0].ToString());
                            CHNLSVC.Inventory.UpdateGntManDocPages(TemDocTable.Rows[i][1].ToString(), BaseCls.GlbUserDefLoca, cmbDocumentType.SelectedValue.ToString(), Convert.ToInt32(TemDocTable.Rows[i][2]), Convert.ToInt32(TemDocTable.Rows[i][3]), BaseCls.GlbUserID);
                        }
                        else
                        {
                            if (TemDocTable.Rows[i][5].ToString() == "HPRS" || TemDocTable.Rows[i][5].ToString() == "HPRM")
                            {
                                int start = Convert.ToInt32(TemDocTable.Rows[i][2]);
                                int end = Convert.ToInt32(TemDocTable.Rows[i][3]);
                                //save reciept and reciept item for every cancel page no
                                for (; end >= start; start++)
                                {
                                    MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                                    _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                                    _receiptAuto.Aut_cate_tp = "PC";
                                    _receiptAuto.Aut_start_char = TemDocTable.Rows[i][5].ToString();
                                    _receiptAuto.Aut_direction = 1;
                                    _receiptAuto.Aut_modify_dt = null;
                                    _receiptAuto.Aut_moduleid = "HP";
                                    _receiptAuto.Aut_number = 0;
                                    _receiptAuto.Aut_year = null;
                                    string _cusNo = CHNLSVC.Sales.GetRecieptNo(_receiptAuto);
                                    //get seq no
                                    int seqNo = CHNLSVC.Inventory.GetSerialID();
                                    //insert reciept
                                    RecieptHeader recieptHeadder = new RecieptHeader();
                                    recieptHeadder.Sar_seq_no = seqNo;//start
                                    recieptHeadder.Sar_prefix = TemDocTable.Rows[i][1].ToString();
                                    recieptHeadder.Sar_receipt_no = _cusNo;
                                    recieptHeadder.Sar_manual_ref_no = start.ToString();
                                    recieptHeadder.Sar_com_cd = BaseCls.GlbUserComCode;
                                    recieptHeadder.Sar_receipt_type = TemDocTable.Rows[i][5].ToString();
                                    recieptHeadder.Sar_receipt_date = DateTime.Now;
                                    recieptHeadder.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                                    recieptHeadder.Sar_debtor_name = "CASH";
                                    recieptHeadder.Sar_remarks = "Cancel";
                                    recieptHeadder.Sar_act = false;
                                    recieptHeadder.Sar_mod_by = BaseCls.GlbUserID;
                                    recieptHeadder.Sar_mod_when = DateTime.Now;
                                    CHNLSVC.Sales.SaveReceiptHeader(recieptHeadder);

                                    //insert reciept item
                                    RecieptItem recieptItem = new RecieptItem();
                                    recieptItem.Sard_seq_no = seqNo;//start
                                    recieptItem.Sard_line_no = 1;
                                    recieptItem.Sard_ref_no = start.ToString();
                                    recieptItem.Sard_receipt_no = _cusNo;
                                    recieptItem.Sard_pay_tp = "CASH";
                                    recieptItem.Sard_settle_amt = 0;
                                    CHNLSVC.Sales.SaveReceiptItem(recieptItem);
                                }
                            }
                            //update using and used value
                            CHNLSVC.Inventory.UpdateGntManDocDt(BaseCls.GlbUserDefLoca, TemDocTable.Rows[i][5].ToString(), "1", Convert.ToInt32(TemDocTable.Rows[i][3]), TemDocTable.Rows[i][1].ToString(), TemDocTable.Rows[i][0].ToString());
                            CHNLSVC.Inventory.UpdateGntManDocPages(TemDocTable.Rows[i][1].ToString(), BaseCls.GlbUserDefLoca, cmbDocumentType.SelectedValue.ToString(), Convert.ToInt32(TemDocTable.Rows[i][2]), Convert.ToInt32(TemDocTable.Rows[i][3]), BaseCls.GlbUserID);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error occured while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                dataGridViewDocuments.DataSource = CHNLSVC.Inventory.Get_all_manual_docs_by_type(BaseCls.GlbUserDefLoca, cmbDocumentType.SelectedValue.ToString());
                FinalGridDataBind();
                Clear();
                MessageBox.Show("Records Cancel sucessully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else {
                MessageBox.Show("Nothing to cancel","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
             */
        }
        
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
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

        private void Clear()
        {
            txtEnd.Text = "";
            txtStart.Text = "";
            BindUserItemStatusDDLData(cmbDocumentType);
            cmbDocumentType_SelectionChangeCommitted(null, null);
            dataGridViewFinalDocuments.DataSource = null;
            CreateTableColumns();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbDocumentType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                dataGridViewDocuments.AutoGenerateColumns = false;
                if (cmbDocumentType.SelectedValue != null && cmbDocumentType.SelectedValue.ToString() != "")
                {
                    dataGridViewDocuments.DataSource = CHNLSVC.Inventory.Get_all_manual_docs_by_type(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, cmbDocumentType.SelectedValue.ToString());
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

        private void dataGridViewDocuments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (cmbDocumentType.SelectedValue != null && cmbDocumentType.SelectedValue.ToString() != "")
                {
                    if (cmbDocumentType.SelectedValue.ToString() == "HPRM" || cmbDocumentType.SelectedValue.ToString() == "HPRS") {
                        MessageBox.Show("HPRM and HPRS manual numbers can not cancel", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                txtStart.Text = dataGridViewDocuments.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
        }

        private void dataGridViewFinalDocuments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 5) {
                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    //delect row
                    TemDocTable.Rows.RemoveAt(e.RowIndex);
                    FinalGridDataBind();
                }
            }
        }

        private void txtEnd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAdd.Focus();
        }

        private void ManualDocumentCancel_FormClosing(object sender, FormClosingEventArgs e)
        {
            GC.Collect();
        }
    }
}
