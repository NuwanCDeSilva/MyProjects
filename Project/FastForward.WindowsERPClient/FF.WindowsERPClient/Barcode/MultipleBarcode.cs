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


namespace FF.WindowsERPClient.Barcode
{
    public partial class MultipleBarcode : Base
    {
        private Boolean _isStrucBaseTax = false;
        public MultipleBarcode()
        {
            InitializeComponent();
            //kapila 1/7/2016
            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
            if (_masterComp.MC_TAX_CALC_MTD == "1") _isStrucBaseTax = true;
        }

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
        string PrintType;
        //Point[] points = new Point[] { new Point { X = 4, Y = 20 }, new Point { X = 292, Y = 20 }, new Point { X = 592, Y = 20 }, new Point { X = 882, Y = 20 }, 
        //                                   new Point { X = 4, Y = 160 }, new Point { X = 292, Y = 160 }, new Point { X = 592, Y = 160 }, new Point { X = 882 ,Y = 160 },
        //                                   new Point { X = 4, Y = 300 }, new Point { X = 292, Y = 300 }, new Point { X = 592, Y = 300 }, new Point { X = 882, Y = 300 },
        //                                   new Point { X = 4, Y = 460 }, new Point { X = 292, Y = 460 }, new Point { X = 592, Y = 460 }, new Point { X = 882, Y = 460 },
        //                                   new Point { X = 4, Y = 600 }, new Point { X = 292, Y = 600 }, new Point { X = 592, Y = 600 }, new Point { X = 882, Y = 600 },
        //};
        //Point[] points = new Point[] { new Point { X = 4, Y = 20 }, new Point { X = 292, Y = 20 }, new Point { X = 592, Y = 20 }, new Point { X = 882, Y = 20 }, 
        //                                   new Point { X = 4, Y = 170 }, new Point { X = 292, Y = 170 }, new Point { X = 592, Y = 170 }, new Point { X = 882 ,Y = 170 },
        //                                   new Point { X = 4, Y = 320 }, new Point { X = 292, Y = 320 }, new Point { X = 592, Y = 320 }, new Point { X = 882, Y = 320 },
        //                                   new Point { X = 4, Y = 470 }, new Point { X = 292, Y = 470 }, new Point { X = 592, Y = 470 }, new Point { X = 882, Y = 470 },
        //                                   new Point { X = 4, Y = 630 }, new Point { X = 292, Y = 630 }, new Point { X = 592, Y = 630 }, new Point { X = 882, Y = 630 },
        //};


        Point[] points = new Point[] { new Point { X = 30, Y = 10 }, new Point { X = 310, Y = 10 }, new Point { X = 595, Y = 10 }, new Point { X = 860, Y = 10 }, 
                                           new Point { X = 30, Y = 160 }, new Point { X = 310, Y = 160 }, new Point { X = 595, Y = 160 }, new Point { X = 860 ,Y = 160 },
                                           new Point { X = 30, Y = 310 }, new Point { X = 310, Y = 310 }, new Point { X = 595, Y = 310 }, new Point { X = 860, Y = 310 },
                                           new Point { X = 30, Y = 460 }, new Point { X = 310, Y = 460 }, new Point { X = 595, Y = 460 }, new Point { X = 860, Y = 460 },
                                           new Point { X = 30, Y = 610 }, new Point { X = 310, Y = 610 }, new Point { X = 595, Y = 610 }, new Point { X = 860, Y = 610 },
        };

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
        int N = 0;
        ImageList ImageList = new ImageList();
        ImageList ImageList2 = new ImageList();
        string IsEncode;
        CheckBox ckBox;
        int TotalCheckBoxes = 0;
        int TotalCheckedCheckBoxes = 0;
        CheckBox HeaderCheckBox = null;
        bool IsHeaderCheckBoxClicked = false;
        private void MultipleBarcode_Load(object sender, EventArgs e)
        {
            AddHeaderCheckBox();
            BindGridView(string.Empty,0);
            GetPriceBook();
            GetDefBook();
            //  GetDefLevel();
            this.cmbLabel.SelectedIndex = 0;
            this.cmbA4.SelectedIndex = 0;
            this.cbEncodeType.SelectedIndex = 21;
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


            HeaderCheckBox.KeyUp += new KeyEventHandler(HeaderCheckBox_KeyUp);
            HeaderCheckBox.MouseClick += new MouseEventHandler(HeaderCheckBox_MouseClick);
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dgvSelectAll_CellValueChanged);
            dataGridView1.CurrentCellDirtyStateChanged += new EventHandler(dgvSelectAll_CurrentCellDirtyStateChanged);
            dataGridView1.CellPainting += new DataGridViewCellPaintingEventHandler(dgvSelectAll_CellPainting);


        }

        private void AddHeaderCheckBox()
        {
            HeaderCheckBox = new CheckBox();

            HeaderCheckBox.Size = new Size(15, 15);

            //Add the CheckBox into the DataGridView
            this.dataGridView1.Controls.Add(HeaderCheckBox);
        }
        private void dgvSelectAll_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!IsHeaderCheckBoxClicked)
                RowCheckBoxClick((DataGridViewCheckBoxCell)dataGridView1[e.ColumnIndex, e.RowIndex]);
        }

        private void dgvSelectAll_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell is DataGridViewCheckBoxCell)
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void HeaderCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            HeaderCheckBoxClick((CheckBox)sender);
        }

        private void HeaderCheckBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                HeaderCheckBoxClick((CheckBox)sender);
        }


        private void dgvSelectAll_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == 0)
                ResetHeaderCheckBoxLocation(e.ColumnIndex, e.RowIndex);
        }



        private void ResetHeaderCheckBoxLocation(int ColumnIndex, int RowIndex)
        {
            //Get the column header cell bounds
            Rectangle oRectangle = this.dataGridView1.GetCellDisplayRectangle(ColumnIndex, RowIndex, true);

            Point oPoint = new Point();

            oPoint.X = oRectangle.Location.X + (oRectangle.Width - HeaderCheckBox.Width) / 2 + 1;
            oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - HeaderCheckBox.Height) / 2 + 1;

            //Change the location of the CheckBox to make it stay on the header
            HeaderCheckBox.Location = oPoint;
        }

        private void HeaderCheckBoxClick(CheckBox HCheckBox)
        {
            IsHeaderCheckBoxClicked = true;

            foreach (DataGridViewRow Row in dataGridView1.Rows)
                ((DataGridViewCheckBoxCell)Row.Cells["chkBxSelect"]).Value = HCheckBox.Checked;

            dataGridView1.RefreshEdit();

            TotalCheckedCheckBoxes = HCheckBox.Checked ? TotalCheckBoxes : 0;

            IsHeaderCheckBoxClicked = false;
        }

        private void RowCheckBoxClick(DataGridViewCheckBoxCell RCheckBox)
        {
            if (RCheckBox != null)
            {
                //Modifiy Counter;            
                if ((bool)RCheckBox.Value && TotalCheckedCheckBoxes < TotalCheckBoxes)
                    TotalCheckedCheckBoxes++;
                else if (TotalCheckedCheckBoxes > 0)
                    TotalCheckedCheckBoxes--;

                //Change state of the header CheckBox.
                if (TotalCheckedCheckBoxes < TotalCheckBoxes)
                    HeaderCheckBox.Checked = false;
                else if (TotalCheckedCheckBoxes == TotalCheckBoxes)
                    HeaderCheckBox.Checked = true;
            }
        }


        private void GetPriceBook()
        {
            DataTable _PriceBook = CHNLSVC.Sales.GetPriceBookTable(BaseCls.GlbUserComCode, null);
            DataView dv = new DataView(_PriceBook);
            string Active = "1";
            dv.RowFilter = "SAPB_ACT ='" + Active + "'";
            cmbPriceBook.DataSource = dv;
            cmbPriceBook.DisplayMember = "SAPB_PB";
            cmbPriceBook.ValueMember = "SAPB_PB";
        }
        private void GetPriceLevel(string PriceBook)
        {

            DataTable _PriceBook = CHNLSVC.Sales.GetPriceLevelTable(BaseCls.GlbUserComCode, PriceBook, null);
            // var myResult = _PriceBook.AsEnumerable().Distinct(System.Data.DataRowComparer.Default).ToList();
            // DataTable ff = myResult.ToDataTable();
            DataTable uniqueCols = _PriceBook.DefaultView.ToTable(true, "SAPL_PB_LVL_CD", "SAPL_ACT");
            DataView dv = new DataView(uniqueCols);
            string Active = "1";
            dv.RowFilter = "SAPL_ACT ='" + Active + "'";
            cmbPriceLevel.DataSource = dv;
            cmbPriceLevel.DisplayMember = "SAPL_PB_LVL_CD";
            cmbPriceLevel.ValueMember = "SAPL_PB_LVL_CD";
        }
        private void clear()
        {
            MultipleBarcode_Load(null, null);
            lblImagecount.Text = "";
            txtitemCode.Text = "";
            txtBatchCode.Text = "";
            txtSerialNo.Text = "";
            txtData.Text = "";
            txtPrice.Text = "";
            txtCompany.Text = "";
            txtItemName.Text = "";
            txtPrintValue.Text = "";
            checkBoxPrint.Checked = false;
            checkBoxItem.Checked = false;
            checkBoxCompany.Checked = false;
            chkGenerateLabel.Checked = false;
            radioButtonA4.Checked = false;
            radioButtonLabel.Checked = false;
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
        }
        DataTable _tblold = new DataTable();
        private void BindGridView(string _itemCode,decimal _qty)
        {

            DataTable _tbl = new DataTable();
            // BaseCls.GlbReportDoc = "RAMP+GRN-15-00079";
            if (BaseCls.GlbReportDoc == "")
            {
                panel3.Visible = true;
                BaseCls.GlbReportDoc = txtGRNno.Text;
                _tbl = CHNLSVC.Inventory.GetBarcodeItemByDoc(BaseCls.GlbReportDoc, BaseCls.GlbUserComCode, _itemCode, BaseCls.GlbUserDefLoca);


            }
            else
            {
                panel3.Visible = false;
                _tbl = CHNLSVC.Inventory.GetBarcodeItemByDoc(BaseCls.GlbReportDoc, BaseCls.GlbUserComCode, _itemCode, BaseCls.GlbUserDefLoca);
            }

            if ((_itemCode != ""))
            {
                if (_tblold.Rows.Count > 0)
                {
                    DataRow[] customerRow = _tblold.Select("itb_itm_cd = '" + _itemCode + "'");
                    if (customerRow.Length > 0)
                    {
                        customerRow[0]["itb_qty"] = _qty;
                    }
                    else
                    {
                        DataRow[] customerRow2 = _tbl.Select("itb_itm_cd = '" + _itemCode + "'");
                        if (customerRow2.Length > 0)
                        {
                            customerRow2[0]["itb_qty"] = _qty;
                        }
                    }

                    // _tblold = _tbl;
                }
                else
                {
                    DataRow[] customerRow = _tbl.Select("itb_itm_cd = '" + _itemCode + "'");
                    if (customerRow.Length > 0)
                    {
                        customerRow[0]["itb_qty"] = _qty;
                    }
                }


            }
            if (_tblold.Rows.Count > 0)
            {
                var _filter = _tblold.Select("itb_itm_cd = '" + _itemCode + "'");
                if (_filter.Length > 0)
                {
                    _tbl = _tblold;
                }
                else
                {
                    _tblold.Merge(_tbl);

                    _tbl = _tblold;
                }
                // _tbl.Clear();

            }
            else
            {
                _tblold = _tbl;
            }
            dataGridView1.DataSource = _tbl;
            TotalCheckBoxes = dataGridView1.RowCount;
            txtBarcodeItem.Text = "";
            txtchangeqty.Text = "";
            TotalCheckedCheckBoxes = 0;
        }
        private DataTable GetDataSource()
        {

            //DataTable _tbl = CHNLSVC.Inventory.GetItemByDoc(BaseCls.GlbReportDoc,BaseCls.GlbUserComCode);

            DataTable dTable = new DataTable();
            DataRow dRow = null;
            DateTime dTime;
            Random rnd = new Random();
            dTable.Columns.Add("IsChecked", System.Type.GetType("System.Boolean"));
            dTable.Columns.Add("Itemcode");
            dTable.Columns.Add("Bcode");
            dTable.Columns.Add("serialno");
            dTable.Columns.Add("ItemName");
            dTable.Columns.Add("price");
            dTable.Columns.Add("Company");

            //foreach (Data)
            //{
            //    dRow = dTable.NewRow();
            //    dTime = DateTime.Now;

            //    dRow["IsChecked"] = "false";
            //    dRow["Itemcode"] = saveGRNSers.Ins_itm_cd;
            //    //dRow["Bcode"] = saveGRNSers.;
            //    dRow["serialno"] = saveGRNSers.Ins_fifo_ser_1;
            //    dRow["ItemName"] = saveGRNSers.;
            //    dRow["price"] = "1400.00";
            //    dRow["Company"] = "ABSTRACT";

            //    dTable.Rows.Add(dRow);
            //    dTable.AcceptChanges();
            //}

            return dTable;
        }
        private void radioButtonLabel_CheckedChanged(object sender, EventArgs e)
        {


            cmbA4.SelectedIndex = 0;
            //if (IsEncode == "Cmb4")
            //{
            //    MessageBox.Show("Can't select!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    // cmbLabel.SelectedIndex = 0;
            //    return;
            //}
            cmbA4.Enabled = false;
            cmbLabel.Enabled = true;
        }

        private void radioButtonA4_CheckedChanged(object sender, EventArgs e)
        {
            cmbLabel.SelectedIndex = 0;
            cmbA4.Enabled = true;
            cmbLabel.Enabled = false;
            //if (IsEncode == "lbl")
            //{
            //    MessageBox.Show("Can't select!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
        }

        private void GetDefBook()
        {
            MasterCompany _MasterCompany = new MasterCompany();

            _MasterCompany = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
            if (_MasterCompany != null)
            {
                cmbPriceBook.SelectedValue = _MasterCompany.Mc_anal7;
                GetPriceLevel(_MasterCompany.Mc_anal7);
                cmbPriceLevel.SelectedValue = _MasterCompany.Mc_anal8;
            }
        }
        private void GetDefLevel()
        {
            MasterCompany _MasterCompany = new MasterCompany();

            _MasterCompany = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
            if (_MasterCompany != null)
            {
                cmbPriceLevel.SelectedValue = _MasterCompany.Mc_anal8;
            }
        }
        private void btnEncode_Click(object sender, EventArgs e)
        {
            if (cmbA4.Text == "136 x 50")
            {
                int count = ImageList.Images.Count;
                //decimal repeatTimest = decimal.Parse(txtPrintValue.Text);
                int s = (count % 105);
                int ss = (count / 105);
                noofPages = Convert.ToInt32(ss);
                lastpageitems = Convert.ToInt32(s);
                Image136x50();
                PrintDocument doc = new PrintDocument();
                doc.PrintPage += this.printDocumentpoints136x50_PrintPage;
                PaperSize ps = new PaperSize();

                ps.RawKind = (int)PaperKind.A4;
                doc.DefaultPageSettings.PaperSize = ps;
                // doc.DefaultPageSettings.Landscape = true;

                PrintDialog dlgSettings = new PrintDialog();
                PrintPreviewDialog printPrvDlg = new PrintPreviewDialog();

                // preview the assigned document or you can create a different previewButton for it


                dlgSettings.Document = doc;
                dlgSettings.Document.PrinterSettings.PrintRange = PrintRange.AllPages;
                printPrvDlg.Document = doc;
                printPrvDlg.ShowDialog();

            }


        }

        private void EncodeImage_design(string _Encodevalue)
        {
            errorProvider1.Clear();
            int W = Convert.ToInt32(BWidth);
            int H = Convert.ToInt32(BHeight);
            //int W = Convert.ToInt32(265);
            //int H = Convert.ToInt32(145);
            b.Alignment = BarcodeLib.AlignmentPositions.CENTER;

            //barcode alignment
            switch (cbBarcodeAlign.SelectedItem.ToString().Trim().ToLower())
            {
                case "left": b.Alignment = BarcodeLib.AlignmentPositions.LEFT; break;
                case "right": b.Alignment = BarcodeLib.AlignmentPositions.RIGHT; break;
                default: b.Alignment = BarcodeLib.AlignmentPositions.CENTER; break;
            }//switch

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
                    BarcodepictureBox.Image = b.Encode(type, _Encodevalue, this.btnForeColor.BackColor, this.btnBackColor.BackColor, W, H);


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
        private void Price(string ItemCode)
        {
            decimal _price = 0;
            List<MasterItemTax> _taxs = new List<MasterItemTax>();
            if (_isStrucBaseTax == true)       //kapila 1/7/2016
            {
                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, ItemCode);
                _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, ItemCode, "GDLP", string.Empty, string.Empty, _mstItem.Mi_anal1);

                if (_taxs.Count <= 0)
                {
                    _taxs = CHNLSVC.Sales.GetItemTax_strucbase(BaseCls.GlbUserComCode, ItemCode, "GOD", string.Empty, string.Empty, _mstItem.Mi_anal1);
                }

            }
            else
            {
                _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, ItemCode, "GDLP", string.Empty, string.Empty);
                if (_taxs.Count <= 0)
                {
                    _taxs = CHNLSVC.Sales.GetItemTax(BaseCls.GlbUserComCode, ItemCode, "GOD", string.Empty, string.Empty);
                }
            }

            DataTable _tbl = CHNLSVC.Sales.GetDetailsforBarcodePrice(cmbPriceBook.SelectedValue.ToString(), cmbPriceLevel.SelectedValue.ToString(), ItemCode);
            if (_tbl.Rows.Count > 0)
            {
                if (_taxs != null && _taxs.Count > 0)
                {
                    _price = Convert.ToDecimal(_tbl.Rows[0]["sapd_itm_price"]) / 100 * (100 + _taxs[0].Mict_tax_rate);

                    txtPrice.Text = FigureRoundUp(_price, true).ToString();
                }
                else
                {
                    MessageBox.Show("Tax definition not found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                txtPrice.Text = "";

            }
        }

        private decimal FigureRoundUp(decimal value, bool _isFinal)
        {
                return RoundUpForPlace(Math.Round(value), 2);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex < 0 || e.ColumnIndex != dataGridView1.Columns["Add"].Index))
            {

                return;

            }
            else
            {
                if (e.RowIndex < 0 || e.ColumnIndex == dataGridView1.Columns["Add"].Index)
                {
                    string Itemcode = dataGridView1.Rows[e.RowIndex].Cells["col_itemcode"].Value.ToString();
                    // string Bcode = dataGridView1.Rows[e.RowIndex].Cells["col_serial"].Value.ToString();
                    string serialno = dataGridView1.Rows[e.RowIndex].Cells["col_serial"].Value.ToString();
                    string ItemName = dataGridView1.Rows[e.RowIndex].Cells["col_itemName"].Value.ToString();
                    //string price = dataGridView1.Rows[e.RowIndex].Cells["col_price"].Value.ToString();
                    string Company = dataGridView1.Rows[e.RowIndex].Cells["com_Company"].Value.ToString();
                    //string IsSerial = dataGridView1.Rows[e.RowIndex].Cells["MI_IS_SER1"].Value.ToString();
                    Price(Itemcode);

                    txtitemCode.Text = Itemcode;
                    // txtBatchCode.Text = Bcode;
                    txtSerialNo.Text = serialno;
                    txtItemName.Text = ItemName;
                    txtData.Text = serialno;
                    //txtPrice.Text = price;
                    txtCompany.Text = Company;
                    // txtPrintValue.Text = "1";

                    if (serialno == "N/A")
                    {
                        radioButtonItemcode.Visible = true;
                        radioButtonserialno.Visible = false;
                        txtData.Text = txtitemCode.Text;
                    }
                    else
                    {
                        radioButtonserialno.Visible = true;
                        radioButtonItemcode.Visible = false;
                        txtData.Text = txtSerialNo.Text;
                    }

                }
            }
        }

        private void btnText_Click(object sender, EventArgs e)
        {

            //if (string.IsNullOrEmpty(txtPrintValue.Text))
            //{

            //    MessageBox.Show("Please Select  No of Copies!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //if (string.IsNullOrEmpty(txtCompany.Text))
            //{
            //    MessageBox.Show("Please Select  Item!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //int distance;
            //if (!(int.TryParse(txtPrintValue.Text, out distance)))
            //{
            //    MessageBox.Show("only allow Numeric Value!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            if ((cmbA4.SelectedIndex > 0) || (cmbLabel.SelectedIndex > 0))
            {
                errorProvider1.SetError(cmbLabel, "Please select Size");
            }
            errorProvider1.Dispose();
            string expirDate = string.Empty;
            string IsExpir = string.Empty;
            // bool IsExpir=false ;
            string _item = string.Empty;
            string _batchno = string.Empty;
            foreach (DataGridViewRow Datarow in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(Datarow.Cells["chkBxSelect"].FormattedValue))
                {
                    txtPrice.Text = "";
                    int row = Datarow.Index;
                    string _Encodevalue = string.Empty;
                    if (Datarow.Cells[0].Value != null && Datarow.Cells[1].Value != null)
                    {
                        _item = Datarow.Cells["col_itemcode"].Value.ToString();
                        _batchno = Datarow.Cells["col_batch_no"].Value.ToString();
                        txtPrintValue.Text = Datarow.Cells["col_qty"].Value.ToString();
                        txtItemName.Text = Datarow.Cells["col_itemName"].Value.ToString();
                        txtCompany.Text = Datarow.Cells["com_Company"].Value.ToString();
                        expirDate = Datarow.Cells["col_itb_exp_dt"].Value.ToString();
                        IsExpir = Datarow.Cells["col_IsExpir"].Value.ToString();
                        string Company = Datarow.Cells["com_Company"].Value.ToString();

                        // _Encodevalue = "99999999999";
                        //_Encodevalue = 099999999999"010307720301";
                        //txtPrintValue.Text = "20";
                        Price(_item);
                        // txtPrice.Text = "1500";
                        txtCompany.Text = Company;
                        if (txtPrice.Text == "")
                        {
                            MessageBox.Show("no price define" + _item, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // btnEncode_Click(null, null);

                    if (PrintType == "265 x 143")
                    {
                        //comented by kapila on 22/2/2017
                        //if (_batchno != "0")
                        //{
                        //    _Encodevalue = _item + _batchno;
                        //}
                        //else
                        //{
                            _Encodevalue = _item;
                        //}
                        EncodeImage_design(_Encodevalue);
                        Image EncodeImage = BarcodepictureBox.Image;

                        #region 256x143
                        // EncodeImage.Save(@"D:\\Barcode4.jpg");
                        using (Graphics graphics = Graphics.FromImage(EncodeImage))
                        {
                            if (checkBoxCompany.Checked)
                            {
                                using (Font arialFont = new Font("Nottke", 12, FontStyle.Bold))
                                {
                                    int x = 100, y = 30;
                                    DrawRotatedTextAt(graphics, 0, txtCompany.Text,
                                        x, y, arialFont, Brushes.Black);
                                }
                            }
                            if (checkBoxItem.Checked)
                            {
                                using (Font arialFont = new Font("Nottke", 10, FontStyle.Bold))//optima//Courier New
                                {
                                    int x = 8, y = 130;
                                    DrawRotatedTextAt(graphics, 0, txtItemName.Text,
                                        x, y, arialFont, Brushes.Black);
                                }
                            }
                            if (checkBoxPrint.Checked)
                            {
                                using (Font arialFont = new Font("Nottke", 8, FontStyle.Bold))//Dotum
                                {
                                    txtPrice.Text = Convert.ToDecimal(txtPrice.Text).ToString("#,##0.00");
                                    int x = 10, y = 60;
                                    DrawRotatedTextAt(graphics, 0, "Rs." + txtPrice.Text,
                                        x, y, arialFont, Brushes.Black);
                                }
                            }
                            if (IsExpir == "1")
                            {
                                DateTime firstdate = Convert.ToDateTime(expirDate);
                                using (Font arialFont = new Font("Nottke", 7))//Dotum
                                {
                                    string firstDateString = firstdate.ToString("MM-dd-yyyy");
                                    int x = 150, y = 60;
                                    DrawRotatedTextAt(graphics, 0, "Ex." + firstDateString,
                                        x, y, arialFont, Brushes.Black);
                                }
                            }
                            graphics.SmoothingMode = SmoothingMode.HighQuality;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                            graphics.CompositingQuality = CompositingQuality.HighQuality;
                        }
                        // EncodeImage.Save(@"D:\\Barcode2.jpg");
                        BarcodepictureBox.Image = EncodeImage;
                        BarcodepictureBox.Height = EncodeImage.Height;
                        BarcodepictureBox.Width = EncodeImage.Width;
                        button1_Click(null, null);
                        #endregion
                        btnText.Enabled = false;
                    }
                    else if (PrintType == "136 x 50")
                    {
                        _Encodevalue = _item;
                        EncodeImage_design(_Encodevalue);
                        Image EncodeImage = BarcodepictureBox.Image;

                        #region 136x50
                        // EncodeImage.RotateFlip(RotateFlipType.Rotate90FlipXY);
                        // EncodeImage.Save(@"D:\\Barcode4.jpg");
                        using (Graphics graphics = Graphics.FromImage(EncodeImage))
                        {

                            if (checkBoxPrint.Checked)
                            {
                                using (Font arialFont = new Font("Nottke ", 7))//Dotum
                                {
                                    txtPrice.Text = Convert.ToDecimal(txtPrice.Text).ToString("#,##0.00");
                                    // int x = 0, y = 50;
                                    int x = 5, y = 1;
                                    DrawRotatedTextAt(graphics, 0, txtPrice.Text,
                                        x, y, arialFont, Brushes.Black);
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
                        }
                        //  EncodeImage.Save(@"D:\\Barcode2.jpg");
                        BarcodepictureBox.Image = EncodeImage;
                        BarcodepictureBox.Height = EncodeImage.Height;
                        BarcodepictureBox.Width = EncodeImage.Width;
                        ListImage136x50(EncodeImage);
                        #endregion
                        btnText.Enabled = false;

                    }
                    else if (PrintType == "265 x 113")
                    {
                        //if (_batchno != "0")
                        //{
                        //    _Encodevalue = _item + _batchno;
                        //}
                        //else
                        //{
                            _Encodevalue = _item;
                    //    }
                        EncodeImage_design(_Encodevalue);
                        Image EncodeImage = BarcodepictureBox.Image;

                        #region 256x143
                        //EncodeImage.Save(@"D:\\Barcode4.jpg");
                        using (Graphics graphics = Graphics.FromImage(EncodeImage))
                        {
                            if (checkBoxCompany.Checked)
                            {
                                using (Font arialFont = new Font("Nottke", 12, FontStyle.Bold))
                                {
                                    int x = 100, y = 10;
                                    DrawRotatedTextAt(graphics, 0, txtCompany.Text,
                                        x, y, arialFont, Brushes.Black);
                                }
                            }
                            if (checkBoxItem.Checked)
                            {
                                using (Font arialFont = new Font("Nottke", 10, FontStyle.Bold))//optima//Courier New
                                {
                                    int x = 5, y = 100;
                                    DrawRotatedTextAt(graphics, 0, txtItemName.Text,
                                        x, y, arialFont, Brushes.Black);
                                }
                            }
                            if (checkBoxPrint.Checked)
                            {
                                using (Font arialFont = new Font("Nottke", 10, FontStyle.Bold))//Dotum
                                {
                                    txtPrice.Text = Convert.ToDecimal(txtPrice.Text).ToString("#,##0.00");
                                    int x = 10, y = 20;
                                    DrawRotatedTextAt(graphics, 0, "Rs." + txtPrice.Text,
                                        x, y, arialFont, Brushes.Black);
                                }
                            }
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            graphics.CompositingQuality = CompositingQuality.HighQuality;
                        }

                        //EncodeImage.Save(@"D:\\Barcode2.jpg");

                        BarcodepictureBox.Image = (Bitmap)EncodeImage;
                        //button1_Click(null, null);



                        Bitmap test = (Bitmap)EncodeImage;
                        test.SetResolution(300, 300);
                        BarcodepictureBox.Image = test;
                        BarcodepictureBox.Height = EncodeImage.Height;
                        BarcodepictureBox.Width = EncodeImage.Width;
                        button1_Click(null, null);
                        // byte[] bitmap = EncodeImage;
                        // string ZPLImageDataString = BitConverter.ToString;
                        // ImageConverter converter = new ImageConverter();
                        // byte[] bitmap = (byte[])converter.ConvertTo(BarcodepictureBox.Image, typeof(byte[]));
                        // string ZPLImageDataString = BitConverter.ToString(bitmap);


                        //// string test = ASCIIEncoding.ASCII.GetString(bitmap);

                        // Byte[] buffer = new byte[ZPLImageDataString.Length];
                        // buffer = System.Text.Encoding.ASCII.GetBytes(ZPLImageDataString);

                        //PrintDialog pd = new PrintDialog();
                        //pd.PrinterSettings = new PrinterSettings();
                        //if (DialogResult.OK == pd.ShowDialog(this))
                        //{
                        //    // Send a printer-specific to the printer.

                        //}

                        #endregion
                        btnText.Enabled = false;
                    }



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


                    if (cmbA4.SelectedIndex > 0)
                    {
                        IsEncode = "Cmb4";
                        radioButtonLabel.Enabled = false;
                    }
                    else
                    {
                        IsEncode = "lbl";
                        radioButtonA4.Enabled = false;
                    }
                }
            }

        }
        public Bitmap SetImageOpacity(Image image, float opacity)
        {
            try
            {
                //create a Bitmap the size of the image provided  
                Bitmap bmp = new Bitmap(image.Width, image.Height);

                //create a graphics object from the image  
                using (Graphics gfx = Graphics.FromImage(bmp))
                {

                    //create a color matrix object  
                    ColorMatrix matrix = new ColorMatrix();

                    //set the opacity  
                    matrix.Matrix33 = opacity;

                    //create image attributes  
                    ImageAttributes attributes = new ImageAttributes();

                    //set the color(opacity) of the image  
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    //now draw the image  
                    gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
                return bmp;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        private Bitmap DrawFilledRectangle(int x, int y)
        {
            Bitmap bmp = new Bitmap(x, y);
            using (Graphics graph = Graphics.FromImage(bmp))
            {
                Rectangle ImageSize = new Rectangle(0, 0, x, y);
                graph.FillRectangle(Brushes.White, ImageSize);
            }
            return bmp;
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

        Bitmap Transparent2Color(Bitmap bmp1, Color target)
        {
            Bitmap bmp2 = new Bitmap(bmp1.Width, bmp1.Height);
            Rectangle rect = new Rectangle(Point.Empty, bmp1.Size);
            using (Graphics G = Graphics.FromImage(bmp2))
            {
                G.Clear(target);
                G.DrawImageUnscaledAndClipped(bmp1, rect);
            }
            return bmp2;
        }

        private void cmbA4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmbLabel.SelectedIndex = 0;
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
            if (cmbA4.Text == "265 x 143")
            {
                BWidth = 265;
                BHeight = 145;
                PrintType = "265 x 143";
                b.Newvalue = 80;
                b.Leftsidepadding = 0;
                b.Bottomsidepadding = 20;
                b.LabelFont = new Font("Perpetua", 10, FontStyle.Bold);
                checkBoxCompany.Visible = true;
                checkBoxItem.Visible = true;
            }
            if (cmbA4.Text == "136 x 50")
            {
                BWidth = 136;
                BHeight = 50;
                PrintType = "136 x 50";
                b.Newvalue = 18;
                b.Leftsidepadding = 0;
                b.Bottomsidepadding = 5;
                b.LabelFont = new Font("Nottke", 8, FontStyle.Regular);
                checkBoxCompany.Visible = false;
                checkBoxItem.Visible = false;
            }

        }

        private void cmbLabel_SelectedIndexChanged(object sender, EventArgs e)
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
            cmbA4.SelectedIndex = 0;
            if (cmbLabel.Text == "265 x 143")
            {
                //// BWidth = 265;
                // //BHeight = 145;
                //BWidth = 265;
                //BHeight = 113;
                BWidth = 288;
                BHeight = 113;
                PrintType = "265 x 113";
                b.Newvalue = 40;
                b.Leftsidepadding = 0;
                b.Bottomsidepadding = 15;
                b.LabelFont = new Font("Nottke", 10, FontStyle.Bold);
                checkBoxCompany.Visible = true;
                checkBoxItem.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void ListImage136x50(Image _img)
        {
            ImageList.ImageSize = new Size(136, 50);

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
        private void print()
        {
            if (PrintType == "265 x 143")
            {
                //int count = ImageList.Images.Count;
                ////decimal repeatTimest = decimal.Parse(txtPrintValue.Text);
                //int s = (count % 20);
                //int ss = (count / 20);
                //noofPages = Convert.ToInt32(ss);
                //lastpageitems = Convert.ToInt32(s);
                //Image();
                //printSetting();

                #region 265 x143
                BarcodeDataSet _DS = new BarcodeDataSet();
                DataTable dt = new DataTable();
                dt.Columns.Add("Encodevalue", typeof(string));
                dt.Columns.Add("Companyname", typeof(string));
                dt.Columns.Add("price", typeof(decimal));
                dt.Columns.Add("Itemname", typeof(string));
                dt.Columns.Add("expirdate", typeof(string));
                foreach (DataGridViewRow Datarow in dataGridView1.Rows)
                {
                    if (Convert.ToBoolean(Datarow.Cells["chkBxSelect"].FormattedValue))
                    {
                        txtPrice.Text = "";
                        int row = Datarow.Index;
                        string _Encodevalue = string.Empty;
                        string expirDate = string.Empty;
                        if (Datarow.Cells[0].Value != null && Datarow.Cells[1].Value != null)
                        {
                            string _item = Datarow.Cells["col_itemcode"].Value.ToString();
                            string _batchno = Datarow.Cells["col_batch_no"].Value.ToString();
                            txtPrintValue.Text = Datarow.Cells["col_qty"].Value.ToString();
                            txtItemName.Text = Datarow.Cells["col_itemName"].Value.ToString();
                            txtCompany.Text = Datarow.Cells["com_Company"].Value.ToString();
                            string IsExpir = Datarow.Cells["col_IsExpir"].Value.ToString();
                            if (IsExpir == "1")
                            {
                                expirDate = Datarow.Cells["col_itb_exp_dt"].Value.ToString();
                            }
                            //if (_batchno != "0")
                            //{
                            //    _Encodevalue = _item + _batchno;
                            //}
                            //else
                            //{
                                _Encodevalue = _item;
                           // }

                            // _Encodevalue = "123456789123456";

                            int printvalue = Convert.ToInt32(txtPrintValue.Text);
                            // printvalue = 20;
                            Price(_item);
                            // txtPrice.Text = "1500";
                            if (txtPrice.Text == "")
                            {
                                MessageBox.Show("no price define" + _item, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            for (int i = 0; i < printvalue; i++)
                            {
                                dt.Rows.Add(_Encodevalue, txtCompany.Text, Convert.ToDecimal(txtPrice.Text), txtItemName.Text, expirDate);
                            }

                        }
                    }

                }
                _DS.Tables.Add(dt);
                MediReport Report = new MediReport();
                Report.Database.Tables["DataTable1"].SetDataSource(dt);
                crystalReportViewer1.ReportSource = Report;
                crystalReportViewer1.Refresh();

                #endregion
                //BarcodeView256x145 ff = new BarcodeView256x145();
                //ff.Show();
                // ds.WriteXmlSchema("sample.xml");

                //transefer data to crystalreportviewer
                //CrystalReport1 cr = new CrystalReport1();


            }
            else if (PrintType == "136 x 50")
            {
                int count = ImageList.Images.Count;
                //decimal repeatTimest = decimal.Parse(txtPrintValue.Text);
                int s = (count % 100);// int s = (count % 105);
                int ss = (count / 100);// int s = (count % 105);
                noofPages = Convert.ToInt32(ss);
                lastpageitems = Convert.ToInt32(s);
                Image136x50();
                printSetting136x50();

                //#region 136*50
                //BarcodeDataSet _DS = new BarcodeDataSet();
                //DataTable dt = new DataTable();
                //dt.Columns.Add("Encodevalue", typeof(string));
                //dt.Columns.Add("Companyname", typeof(string));
                //dt.Columns.Add("price", typeof(decimal));
                //dt.Columns.Add("Itemname", typeof(string));
                //dt.Columns.Add("expirdate", typeof(string));
                //foreach (DataGridViewRow Datarow in dataGridView1.Rows)
                //{
                //    if (Convert.ToBoolean(Datarow.Cells["chkBxSelect"].FormattedValue))
                //    {
                //        int row = Datarow.Index;
                //        string _Encodevalue = string.Empty;
                //        string expirDate = string.Empty;
                //        if (Datarow.Cells[0].Value != null && Datarow.Cells[1].Value != null)
                //        {
                //            string _item = Datarow.Cells["col_itemcode"].Value.ToString();
                //            string _batchno = Datarow.Cells["col_batch_no"].Value.ToString();
                //            txtPrintValue.Text = Datarow.Cells["col_qty"].Value.ToString();
                //            txtItemName.Text = Datarow.Cells["col_itemName"].Value.ToString();
                //            txtCompany.Text = Datarow.Cells["com_Company"].Value.ToString();

                //            string IsExpir = Datarow.Cells["col_IsExpir"].Value.ToString();
                //            if (IsExpir == "1")
                //            {
                //                expirDate = Datarow.Cells["col_itb_exp_dt"].Value.ToString();
                //            }
                //          //  _Encodevalue = _item + _batchno;
                //            _Encodevalue = _item;
                //            //_Encodevalue = "123456789123456";

                //            int printvalue = Convert.ToInt32(txtPrintValue.Text);
                //           // printvalue = 105;
                //            Price(_item);
                //            //txtPrice.Text = "1500";
                //            if (txtPrice.Text == "")
                //            {
                //                MessageBox.Show("no price define" + _item, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //                return;
                //            }
                //            for (int i = 0; i < printvalue; i++)
                //            {
                //                dt.Rows.Add(_Encodevalue, txtCompany.Text, Convert.ToDecimal(txtPrice.Text), txtItemName.Text, expirDate);
                //            }


                //        }
                //    }

                //}
                //_DS.Tables.Add(dt);
                //Copy_of_MediReport Report = new Copy_of_MediReport();
                //Report.Database.Tables["DataTable1"].SetDataSource(dt);
                //crystalReportViewer1.ReportSource = Report;
                //crystalReportViewer1.Refresh();
                //#endregion
            }
            else if (PrintType == "265 x 113")
            {
                //decimal repeatTimest = int.Parse(txtPrintValue.Text);
                //int count = ImageList.Images.Count;
                //noofPages = Convert.ToInt32(count);
                //repeatTimes1 = 1;
                //LabelImage();
                //printSettingLabel();
                #region 265 x 113
                BarcodeDataSet _DS = new BarcodeDataSet();
                DataTable dt = new DataTable();
                dt.Columns.Add("Encodevalue", typeof(string));
                dt.Columns.Add("Companyname", typeof(string));
                dt.Columns.Add("price", typeof(decimal));
                dt.Columns.Add("Itemname", typeof(string));
                dt.Columns.Add("expirdate", typeof(string));
                foreach (DataGridViewRow Datarow in dataGridView1.Rows)
                {
                    if (Convert.ToBoolean(Datarow.Cells["chkBxSelect"].FormattedValue))
                    {
                        txtPrice.Text = "";
                        int row = Datarow.Index;
                        string _Encodevalue = string.Empty;
                        string expirDate = string.Empty;
                        if (Datarow.Cells[0].Value != null && Datarow.Cells[1].Value != null)
                        {
                            string _item = Datarow.Cells["col_itemcode"].Value.ToString();
                            string _batchno = Datarow.Cells["col_batch_no"].Value.ToString();
                            txtPrintValue.Text = Datarow.Cells["col_qty"].Value.ToString();
                            txtItemName.Text = Datarow.Cells["col_itemName"].Value.ToString();
                            txtCompany.Text = Datarow.Cells["com_Company"].Value.ToString();

                            string IsExpir = Datarow.Cells["col_IsExpir"].Value.ToString();
                            if (IsExpir == "1")
                            {
                                expirDate = Datarow.Cells["col_itb_exp_dt"].Value.ToString();
                            }
                            //if (_batchno != "0")
                            //{
                            //    _Encodevalue = _item + _batchno;
                            //}
                            //else
                            //{
                                _Encodevalue = _item;
                            //}
                            //_Encodevalue = "123456789123456";

                            int printvalue = Convert.ToInt32(txtPrintValue.Text);
                            //printvalue = 4;
                            Price(_item);
                            //txtPrice.Text = "1500";
                            if (txtPrice.Text == "")
                            {
                                MessageBox.Show("no price define" + _item, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            for (int i = 0; i < printvalue; i++)
                            {
                                dt.Rows.Add(_Encodevalue, txtCompany.Text, Convert.ToDecimal(txtPrice.Text), txtItemName.Text, expirDate);
                            }


                        }
                    }

                }
                _DS.Tables.Add(dt);
                BarcodeReport Report = new BarcodeReport();
                Report.Database.Tables["DataTable1"].SetDataSource(dt);
                crystalReportViewer1.ReportSource = Report;
                crystalReportViewer1.Refresh();

                #endregion
                // BarcodeView256x145 ff = new BarcodeView256x145();
                // ff.Show();
                //ds.WriteXmlSchema("sample.xml");


                // CrystalReport1 cr = new CrystalReport1();
            }
        }
        private void printSettingLabel()
        {
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += this.LabelprintDocument_PrintPage;

            PaperSize paperSize = new PaperSize("MyCustomSize", 265, 113); //numbers are optional


            paperSize.RawKind = (int)PaperKind.Custom;

            doc.DefaultPageSettings.PaperSize = paperSize;
            PrintPreviewDialog prw = new PrintPreviewDialog();
            //doc.DefaultPageSettings.PaperSize = ps;
            doc.DefaultPageSettings.Landscape = false;
            PrintDialog dlgSettings = new PrintDialog();
            dlgSettings.Document = doc;
            dlgSettings.Document.PrinterSettings.PrintRange = PrintRange.AllPages;

            //prw.Document = doc;

            //prw.Show();


            if (dlgSettings.ShowDialog() == DialogResult.OK)
            {
                doc.Print();
            }
        }
        private void LabelImage()
        {
            for (int count = 0; count < ImageList.Images.Count; count++)
            {
                Bitmap imageSource1 = new Bitmap(BarcodepictureBox.Image);

                //Bitmap myCombinationImage1 = new Bitmap(imageSource1.Width, imageSource1.Height * repeatTimes1);
                //using (Graphics graphics = Graphics.FromImage(myCombinationImage1))
                //{
                //    Point newLocation = new Point(0, 0);
                //    for (int imageIndex = 0; imageIndex < repeatTimes1; imageIndex++)
                //    {
                //        graphics.DrawImage(imageSource1, newLocation);
                //        newLocation = new Point(newLocation.X, newLocation.Y + imageSource1.Height);
                //    }
                //}
                //PrintImage1 = myCombinationImage1;
                ImageList2.ImageSize = new Size(256, 113);
                ImageList2.Images.Add(count.ToString(), imageSource1);

            }
        }
        private void Image()
        {
            ImageList2 = new ImageList();
            for (int count = 0; count < ImageList.Images.Count; count++)
            {
                Image imageSource1 = ImageList.Images[count];
                //Bitmap myCombinationImage1 = new Bitmap(imageSource1.Width, imageSource1.Height * 1);
                //using (Graphics graphics = Graphics.FromImage(myCombinationImage1))
                //{
                //    Point newLocation = new Point(0, 0);
                //    for (int imageIndex = 0; imageIndex < 1; imageIndex++)
                //    {
                //        graphics.DrawImage(imageSource1, newLocation);
                //        newLocation = new Point(newLocation.X, newLocation.Y + imageSource1.Height);
                //    }
                //}
                ImageList2.ImageSize = new Size(256, 143);
                ImageList2.Images.Add(count.ToString(), imageSource1);
                //PrintImage1 = myCombinationImage1;
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
                ImageList2.Images.Add(count.ToString(), imageSource1);


            }

        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            //if (txtEncoded.Text == "")
            //{
            //    MessageBox.Show("Please Encode Image!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //    return;
            //}
            print();
        }
        private void LabelprintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Point loc = new Point(5, -10);

            if (nPage < noofPages)
            {
                switch (nPage)
                {
                    default:
                        //e.Graphics.DrawImage(PrintImage1, loc);
                        e.Graphics.DrawImage(ImageList2.Images[ImageNo], loc);
                        // e.Graphics.DrawString()
                        ImageNo++;
                        break;
                }
            }
            ++nPage;


            if (nPage < noofPages)
                e.HasMorePages = true;
            else
            {
                e.HasMorePages = false;
            }

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if ((nPage < noofPages))
            {
                switch (nPage)
                {
                    default:
                        while (count < 20)
                        {

                            e.Graphics.DrawImage(ImageList2.Images[ImageNo], points[pointNo]);
                            count++;
                            pointNo++;
                            ImageNo++;
                        }
                        break;
                }
            }
            if (pointNo == 20)
            {
                pointNo = 0;
            }

            if (nPage < noofPages)
            {
                if (lastpageitems == 0)
                {
                    e.HasMorePages = false;
                    return;
                }
                e.HasMorePages = true;
                ++nPage;
                count = 0;
            }
            else if (lastpageitems < 20)
            {
                for (int i = 0; i < lastpageitems; i++)
                {
                    e.Graphics.DrawImage(ImageList2.Images[ImageNo], points[i]);
                    ImageNo++;

                }

            }
            else
            {
                e.HasMorePages = false;
            }
            //for (int count = 0; count < ImageList2.Images.Count; count++)
            //{

            //    e.Graphics.DrawImage(ImageList2.Images[count], points[count]);
            //}
        }

        private void printSetting()
        {

            PrintDocument doc = new PrintDocument();
            //doc.PrinterSettings.Copies = 5;
            doc.PrintPage += this.printDocument1_PrintPage;

            //PaperSize paperSize = new PaperSize("MyCustomSize", 266, 145); //numbers are optional
            //paperSize.RawKind = (int)PaperKind.Custom;
            //doc.DefaultPageSettings.PaperSize = paperSize;

            PaperSize ps = new PaperSize();
            //ps.Width = 11;
            //ps.Height = 8;
            ps.RawKind = (int)PaperKind.A4;
            doc.DefaultPageSettings.PaperSize = ps;
            doc.DefaultPageSettings.Landscape = true;

            PrintDialog dlgSettings = new PrintDialog();

            dlgSettings.Document = doc;
            dlgSettings.Document.PrinterSettings.PrintRange = PrintRange.AllPages;
            if (dlgSettings.ShowDialog() == DialogResult.OK)
            {
                doc.Print();
            }



        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // clear();
            // BaseCls.GlbReportDoc = "";
            this.Close();
            MultipleBarcode m = new MultipleBarcode();
            m.Show();

        }

        private void chkGenerateLabel_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGenerateLabel.Checked)
            {
                panel1.Enabled = true;
                return;
            }
            panel1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[1];
                chk.Value = !(chk.Value == null ? false : (bool)chk.Value); //because chk.Value is initialy null
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SerialPort sp = new SerialPort();
            sp.PortName = "COM1";
            sp.BaudRate = 9600;
            sp.Parity = Parity.None;
            sp.DataBits = 8;
            sp.StopBits = StopBits.One;
            sp.Open();
            sp.WriteLine("                                        ");
            sp.WriteLine("Hi welocme here");
        }

        private void txtPrintValue_TextChanged(object sender, EventArgs e)
        {
            int temp;
            if (int.TryParse(txtPrintValue.Text, out temp))
            {
                if (temp > 0)
                {
                    //valid number (int)
                }
                else
                {
                    MessageBox.Show("Please type valid Number!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtPrintValue.Text = "";

                    //invalid number (int)
                }
            }
        }

        private void txtPrintValue_Leave(object sender, EventArgs e)
        {
            int temp;
            if (int.TryParse(txtPrintValue.Text, out temp))
            {
                if (temp >= 0)
                {
                    //valid number (int)
                }
                else
                {
                    MessageBox.Show("Please type valid Number!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                    txtPrintValue.Text = "";
                }
            }
        }

        private void txtPrintValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Char.IsControl(e.KeyChar))
            //{
            //    e.Handled = false;
            //    return;
            //}

            if (!char.IsDigit(e.KeyChar)) e.Handled = true;         //Just Digits
            if (e.KeyChar == (char)8) e.Handled = false;
        }

        private void cmbPriceBook_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbPriceBook_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string PBook = cmbPriceBook.SelectedValue.ToString();
            GetPriceLevel(PBook);
        }

        private void cmbPriceLevel_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtitemCode.Text = "";
            txtBatchCode.Text = "";
            txtCompany.Text = "";
            txtItemName.Text = "";
            txtSerialNo.Text = "";
            txtData.Text = "";
            txtPrice.Text = "";
            txtPrintValue.Text = "";

            MessageBox.Show("Please select item agin!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string barCode = "8888888888888888";
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
            button1_Click(null, null);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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

        private void dd()
        {
            string bitmapFilePath = @"test.bmp";  // file is attached to this support article
            byte[] bitmapFileData = System.IO.File.ReadAllBytes(bitmapFilePath);
            int fileSize = bitmapFileData.Length;

            // The following is known about test.bmp.  It is up to the developer
            // to determine this information for bitmaps besides the given test.bmp.
            int bitmapDataOffset = int.Parse(bitmapFileData[10].ToString()); ;
            int width = int.Parse(bitmapFileData[18].ToString()); ;
            int height = int.Parse(bitmapFileData[22].ToString()); ;
            int bitsPerPixel = int.Parse(bitmapFileData[28].ToString()); // Monochrome image required!
            int bitmapDataLength = bitmapFileData.Length - bitmapDataOffset;
            double widthInBytes = Math.Ceiling(width / 8.0);

            // Copy over the actual bitmap data from the bitmap file.
            // This represents the bitmap data without the header information.
            byte[] bitmap = new byte[bitmapDataLength];
            Buffer.BlockCopy(bitmapFileData, bitmapDataOffset, bitmap, 0, bitmapDataLength);

            // Invert bitmap colors
            for (int i = 0; i < bitmapDataLength; i++)
            {
                bitmap[i] ^= 0xFF;
            }

            // Create ASCII ZPL string of hexadecimal bitmap data
            string ZPLImageDataString = BitConverter.ToString(bitmap);
            ZPLImageDataString = ZPLImageDataString.Replace("-", string.Empty);

            // Create ZPL command to print image
            string[] ZPLCommand = new string[4];

            ZPLCommand[0] = "^XA";
            ZPLCommand[1] = "^FO20,20";
            ZPLCommand[2] =
                "^GFA, " +
                bitmapDataLength.ToString() + "," +
                bitmapDataLength.ToString() + "," +
                widthInBytes.ToString() + "," +
                ZPLImageDataString;

            ZPLCommand[3] = "^XZ";

            // Connect to printer
            string ipAddress = "10.3.14.42";
            int port = 9100;
            System.Net.Sockets.TcpClient client =
                new System.Net.Sockets.TcpClient();
            client.Connect(ipAddress, port);
            System.Net.Sockets.NetworkStream stream = client.GetStream();

            // Send command strings to printer
            foreach (string commandLine in ZPLCommand)
            {
                stream.Write(ASCIIEncoding.ASCII.GetBytes(commandLine), 0, commandLine.Length);
                stream.Flush();
            }

            // Close connections
            stream.Close();
            client.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                // Set the printer name. 
                //pd.PrinterSettings.PrinterName = "\\NS5\hpoffice
                //pd.PrinterSettings.PrinterName = "Zebra New GK420t"               
                pd.Print();
            }
            catch (Exception ex)
            {

            }
        }

        void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            Font printFont = new Font("Free 3 of 9", 20);
            //Font printFont1 = new Font("Times New Roman", 9, FontStyle.Bold);
            SolidBrush br = new SolidBrush(Color.Black);
            ev.Graphics.DrawString("*999999999999*", printFont, br, 5, 20);
            // ev.Graphics.DrawString("*AAAAAAFFF*", printFont1, br, 10, 85);
        }

        private void btn_srch_grn_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = new DataTable();

            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GRNNo);
            _result = CHNLSVC.CommonSearch.searchGRNData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtGRNno;
            _CommonSearch.ShowDialog();
        }
        #region Common Searching Area

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

        #endregion Common Searching Area

        private void btnview_Item_Click(object sender, EventArgs e)
        {
            BindGridView(txtBarcodeItem.Text,Convert.ToDecimal(txtchangeqty.Text));
            txtBarcodeItem.Focus();
        }

        private void txtchangeqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtBarcodeItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter | e.KeyCode.Equals(Keys.Tab))
            {
                if (string.IsNullOrEmpty(txtBarcodeItem.Text.Trim())) return;

                this.Cursor = Cursors.WaitCursor;
                string _item = "";
                //kapila 14/11/2013
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
                txtchangeqty.Focus();

            }
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
        private void SystemErrorMessage(Exception ex)
        {
            CHNLSVC.CloseChannel();
            this.Cursor = Cursors.Default;
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        private void btnSearchFile_spv_Click(object sender, EventArgs e)
        {
            txtFileName.Text = string.Empty;
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "txt files (*.xls)|*.xls,*.xlsx|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.ShowDialog();
            string[] _obj = openFileDialog1.FileName.Split('\\');
            txtFileName.Text = openFileDialog1.FileName;
        }

        private void btnUploadFile_spv_Click(object sender, EventArgs e)
        {
            string _msg = string.Empty;
            Boolean _isVatClaim = false;
            string _suppTaxCate = "";
            Int32 _upLine = 0;
            Int32 _upDelLineNo = 0;

            Int32 _locaCount = 0;
            bool _isDecimalAllow = false;
            try
            {

                # region validation

                if (string.IsNullOrEmpty(txtFileName.Text))
                {
                    MessageBox.Show("Please select upload file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtFileName.Clear();
                    txtFileName.Focus();
                    return;
                }

                System.IO.FileInfo fileObj = new System.IO.FileInfo(txtFileName.Text);

                if (fileObj.Exists == false)
                {
                    MessageBox.Show("Selected file does not exist at the following path.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }
                #endregion

                #region open excel
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


                conStr = String.Format(conStr, txtFileName.Text, "NO");
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable _dt = new DataTable();
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
                oda.Fill(_dt);
                connExcel.Close();

                #endregion

                string _item = "";
                decimal _qty = 0;

                StringBuilder _errorLst = new StringBuilder();
                if (_dt == null || _dt.Rows.Count <= 0) { MessageBox.Show("The excel file is empty. Please check the file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

                if (_dt.Rows.Count > 0)
                {
                    if (MessageBox.Show("Are you sure ?", "Barcode Print", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No) return;

                    foreach (DataRow _dr in _dt.Rows)
                    {
                        _item = _dr[0].ToString().Trim();
                        _qty = Convert.ToDecimal(_dr[1]);


                        #region item validation

                        if (string.IsNullOrEmpty(_item))
                        {
                            MessageBox.Show("Please enter item.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        if (string.IsNullOrEmpty(_qty.ToString()))
                        {
                            MessageBox.Show("Please enter qty.", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        _isDecimalAllow = CHNLSVC.Inventory.IsUOMDecimalAllow(_item);
                        if (_isDecimalAllow == false) _qty = decimal.Truncate(Convert.ToDecimal(_qty));

                        #endregion

                        MasterItem _tmpItem = new MasterItem();

                        _tmpItem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                        if (_tmpItem != null)
                        {
                            BindGridView(_item, _qty);
                        }
                        else
                        {
                            MessageBox.Show("Invalid Item Code", "Purchase Order", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }


                    MessageBox.Show("Done", "Supplier Quotation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


    }
}
