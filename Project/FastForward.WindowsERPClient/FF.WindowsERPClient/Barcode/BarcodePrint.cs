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
using System.IO.Ports;

namespace FF.WindowsERPClient.Barcode
{
    public partial class BarcodePrint : Form
    {
        public BarcodePrint()
        {
            InitializeComponent();
        }
        //Rukshan
        BarcodeLib.Barcode b = new BarcodeLib.Barcode();
        int repeatTimes = 0;
        int repeatTimes4 = 0;
        int repeatTimes3 = 0;
        int repeatTimes2 = 0;
        int repeatTimes1 = 0;
        int noofPages = 0;
        int lastpageitems = 0;
        
        int BWidth = 0;
        int BHeight = 0;
        
        Image PrintImage5;
        Image PrintImage4;
        Image PrintImage3;
        Image PrintImage2;
        Image PrintImage1;
        
        private void BarcodePrint_Load(object sender, EventArgs e)
        {
            this.cmbLabel.SelectedIndex = 0;
            this.cmbA4.SelectedIndex = 0;
            this.cbEncodeType.SelectedIndex = 19;
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


            txtitemCode.Text = "ASW3225WTWH";
            txtBatchCode.Text = "012555";
            txtSerialNo.Text = "2552244225224";
            txtCompany.Text = "ABSTRACT";
            txtItemName.Text = "Shirt OAKWOOD DarkGreen-XL";
            txtPrice.Text = "RS: 1400.00";
            txtData.Text = txtitemCode.Text;
            
        }
        private void btnEncode_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            int W = Convert.ToInt32(BWidth);
            int H = Convert.ToInt32(BHeight);
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
                    BarcodepictureBox.Image = b.Encode(type, this.txtData.Text.Trim(), this.btnForeColor.BackColor, this.btnBackColor.BackColor, W, H);


                    //===================================

                    //show the encoding time
                    this.lblEncodingTime.Text = "(" + Math.Round(b.EncodingTime, 0, MidpointRounding.AwayFromZero).ToString() + "ms)";

                    txtEncoded.Text = b.EncodedValue;

                    // tsslEncodedType.Text = "Encoding Type: " + b.EncodedType.ToString();
                }//if

                //reposition the barcode image to the middle
                barcode.Location = new Point((this.barcode.Location.X + this.barcode.Width / 2) - barcode.Width / 2, (this.barcode.Location.Y + this.barcode.Height / 2) - barcode.Height / 2);
            }//try
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }//catch
        }

        private void cmbA4_SelectionChangeCommitted(object sender, EventArgs e)
        {
           
            if (cmbA4.Text == "265 x 143")
            {
                BWidth = 258;
                BHeight = 43;
            }
        }

        private void radioButtonA4_CheckedChanged(object sender, EventArgs e)
        {
            cmbLabel.SelectedIndex = 0;
            cmbA4.Enabled = true;
            cmbLabel.Enabled = false;
        }

        private void radioButtonLabel_CheckedChanged(object sender, EventArgs e)
        {
            cmbA4.SelectedIndex = 0;
            cmbA4.Enabled = false;
            cmbLabel.Enabled = true;
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

        private void btnText_Click(object sender, EventArgs e)
        {
            errorProvider1.Dispose();
            if ((cmbA4.SelectedIndex > 0) || (cmbLabel.SelectedIndex > 0))
            {


                btnEncode_Click(null, null);
                Image EncodeImage = BarcodepictureBox.Image;
                // EncodeImage.Save(@"D:\\Barcode4.jpg");
                Image BlankImage = DrawFilledRectangle(EncodeImage.Width + 7, EncodeImage.Height + 100);

                using (Graphics graphics = Graphics.FromImage(BlankImage))
                {
                    graphics.FillRectangle(Brushes.White, 0, 0, BlankImage.Width, BlankImage.Height);
                }
                //BlankImage.Save(@"D:\\BlankImage.jpg");
                //System.Drawing.Image image2 = System.Drawing.Image.FromFile(@"D:\\BlankImage.jpg");
                Bitmap EncodeImagewithText = new Bitmap(BlankImage.Width, BlankImage.Height);

                using (Graphics g = Graphics.FromImage(EncodeImagewithText))
                {
                    g.DrawImage(EncodeImage, new Point(20, 60));
                    g.DrawImage(BlankImage, EncodeImage.Width, 0);
                    //  g.DrawImage(BlankImage, 0, (BlankImage.Height / 2) - (Height / 2 + 5));
                    // g.DrawImage(EncodeImage, (BlankImage.Width / 2) - (Width / 2 + 5), (BlankImage.Height / 2) - (Height / 2 + 5));
                }





                using (Graphics graphics = Graphics.FromImage(EncodeImagewithText))
                {
                    if (checkBoxCompany.Checked)
                    {
                        using (Font arialFont = new Font("Arial", 18))
                        {
                            int x = 70, y = 30;
                            DrawRotatedTextAt(graphics, 0, txtCompany.Text,
                                x, y, arialFont, Brushes.Black);
                        }
                    }
                    if (checkBoxItem.Checked)
                    {
                        using (Font arialFont = new Font("Arial", 12))
                        {
                            int x = 10, y = 110;
                            DrawRotatedTextAt(graphics, 0, txtItemName.Text,
                                x, y, arialFont, Brushes.Black);
                        }
                    }
                    if (checkBoxPrint.Checked)
                    {
                        using (Font arialFont = new Font("Arial", 10))
                        {
                            int x = 28, y = 25;
                            DrawRotatedTextAt(graphics, 90, txtPrice.Text,
                                x, y, arialFont, Brushes.Black);
                        }
                    }
                }


                Image test = Transparent2Color(EncodeImagewithText, Color.White);

                //test.Save(@"D:\\Barcode2.jpg");
                BarcodepictureBox.Image = test;
            }
            else
            {
                errorProvider1.SetError(cmbLabel, "Please select Size");
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
        private void DrawRotatedTextAt(Graphics gr, float angle,string txt, int x, int y, Font the_font, Brush the_brush)
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
              if (!(BarcodepictureBox.Image == null))
                {
                    if (radioButtonFullpage.Checked)
                    {
                        //BarcodeReport bp = new BarcodeReport();
                        // bp.Show();
                        print();
                    }
                    if (radioButtonPrintValue.Checked)
                    {

                        print();
                    }
                }
                else
                {

                }     
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


        private void radioButtonFullpage_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonFullpage.Checked)
            {
                txtPrintValue.Enabled = false;
            }
        }

        private void radioButtonPrintValue_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonPrintValue.Checked)
            {
                txtPrintValue.Enabled = true;
            }

        }
       
        private void print()
        {
            if (cmbA4.Text == "265 x 143")
            {
                if (radioButtonFullpage.Checked)
                {
                    repeatTimes = 5;
                    repeatTimes4 = 4;
                    repeatTimes3 = 3;
                    repeatTimes2 = 2;
                    repeatTimes1 = 1;
                    Image();
                    printSetting();
                    repeatTimes = 0;
                }
                else if (radioButtonPrintValue.Checked)
                {                                    
                    
                    repeatTimes = 5;
                    repeatTimes4 = 4;
                    repeatTimes3 = 3;
                    repeatTimes2 = 2;
                    repeatTimes1 = 1;
                    
                    decimal repeatTimest = int.Parse(txtPrintValue.Text);
                    decimal s = (repeatTimest % 20);
                    decimal ss = (repeatTimest / 20);
                    //string[] words = s.Split('.');

                    noofPages = Convert.ToInt32(ss);
                    lastpageitems = Convert.ToInt32(s);
                    Image();
                    printSetting();
                }
            }
            else if (cmbLabel.Text == "265 x 143")
            {
                //if (radioButtonFullpage.Checked)
                //{
                //}
                //else if (radioButtonPrintValue.Checked)
                //{
                decimal repeatTimest = int.Parse(txtPrintValue.Text);
                
                noofPages = Convert.ToInt32(repeatTimest);
               
                    repeatTimes1 = 1;
                    LabelImage();
                    printSettingLabel();
                //}
               // BarcodeReport bp = new BarcodeReport();
               // bp.Show();
            }

            


        }
        private void LabelImage()
        {
            Image imageSource1 = BarcodepictureBox.Image;
            Bitmap myCombinationImage1 = new Bitmap(imageSource1.Width, imageSource1.Height * repeatTimes1);
            using (Graphics graphics = Graphics.FromImage(myCombinationImage1))
            {
                Point newLocation = new Point(0, 0);
                for (int imageIndex = 0; imageIndex < repeatTimes1; imageIndex++)
                {
                    graphics.DrawImage(imageSource1, newLocation);
                    newLocation = new Point(newLocation.X, newLocation.Y + imageSource1.Height);
                }
            }
            PrintImage1 = myCombinationImage1;
        }
        private void Image()
        {
            Image imageSource = BarcodepictureBox.Image;//or your resource..
            Image imageSource4 = BarcodepictureBox.Image;
            Image imageSource3 = BarcodepictureBox.Image;
            Image imageSource2 = BarcodepictureBox.Image;
            Image imageSource1 = BarcodepictureBox.Image;

            Bitmap myCombinationImage = new Bitmap(imageSource.Width, imageSource.Height * repeatTimes);
            using (Graphics graphics = Graphics.FromImage(myCombinationImage))
            {
                Point newLocation = new Point(0, 0);
                for (int imageIndex = 0; imageIndex < repeatTimes; imageIndex++)
                {
                    graphics.DrawImage(imageSource, newLocation);
                    newLocation = new Point(newLocation.X, newLocation.Y + imageSource.Height);
                }
            }

            Bitmap myCombinationImage4 = new Bitmap(imageSource4.Width, imageSource4.Height * repeatTimes4);
            using (Graphics graphics = Graphics.FromImage(myCombinationImage4))
            {
                Point newLocation = new Point(0, 0);
                for (int imageIndex = 0; imageIndex < repeatTimes4; imageIndex++)
                {
                    graphics.DrawImage(imageSource4, newLocation);
                    newLocation = new Point(newLocation.X, newLocation.Y + imageSource4.Height);
                }
            }
            Bitmap myCombinationImage3 = new Bitmap(imageSource3.Width, imageSource3.Height * repeatTimes3);
            using (Graphics graphics = Graphics.FromImage(myCombinationImage3))
            {
                Point newLocation = new Point(0, 0);
                for (int imageIndex = 0; imageIndex < repeatTimes3; imageIndex++)
                {
                    graphics.DrawImage(imageSource3, newLocation);
                    newLocation = new Point(newLocation.X, newLocation.Y + imageSource3.Height);
                }
            }
            Bitmap myCombinationImage2 = new Bitmap(imageSource2.Width, imageSource2.Height * repeatTimes2);
            using (Graphics graphics = Graphics.FromImage(myCombinationImage2))
            {
                Point newLocation = new Point(0, 0);
                for (int imageIndex = 0; imageIndex < repeatTimes2; imageIndex++)
                {
                    graphics.DrawImage(imageSource2, newLocation);
                    newLocation = new Point(newLocation.X, newLocation.Y + imageSource2.Height);
                }
            }
            Bitmap myCombinationImage1 = new Bitmap(imageSource1.Width, imageSource1.Height * repeatTimes1);
            using (Graphics graphics = Graphics.FromImage(myCombinationImage1))
            {
                Point newLocation = new Point(0, 0);
                for (int imageIndex = 0; imageIndex < repeatTimes1; imageIndex++)
                {
                    graphics.DrawImage(imageSource1, newLocation);
                    newLocation = new Point(newLocation.X, newLocation.Y + imageSource1.Height);
                }
            }
            PrintImage5 = myCombinationImage;
            PrintImage4 = myCombinationImage4;
            PrintImage3 = myCombinationImage3;
            PrintImage2 = myCombinationImage2;
            PrintImage1 = myCombinationImage1;



        }
        private void printSetting()
        {
                 
            PrintDocument doc = new PrintDocument();
            //doc.PrinterSettings.Copies = 5;
            doc.PrintPage += this.printDocument1_PrintPage;

            PaperSize ps = new PaperSize();
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
        private void printSettingLabel()
        {
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += this.LabelprintDocument_PrintPage;
            
            PaperSize paperSize = new PaperSize("MyCustomSize", 266, 145); //numbers are optional

            paperSize.RawKind = (int)PaperKind.Custom;

            doc.DefaultPageSettings.PaperSize = paperSize;


            //doc.DefaultPageSettings.PaperSize = ps;
            doc.DefaultPageSettings.Landscape = false;
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

           
        }
        int nPage = 0; int k = 0;
        private const int d=0;
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            //Font font = new Font("Courier New", 12);

            //e.PageSettings.PaperSize = new PaperSize("A4", 850, 1100);

            //float pageWidth = e.PageSettings.PrintableArea.Width;
            //float pageHeight = e.PageSettings.PrintableArea.Height;

            //System.Drawing.Image img = System.Drawing.Image.FromFile("D:\\Foto.jpg");


            Point loc = new Point(4, 20);
            Point loc2 = new Point(292, 20);
            Point loc3 = new Point(592, 20);
            Point loc4 = new Point(882, 20);
            // Point[] points = new Point[] { new Point { X = 50, Y = 50 }, new Point { X = 100, Y = 100 }, new Point { X = 110, Y = 110 } };
            if (radioButtonFullpage.Checked)
            {
                // e.HasMorePages = true;
                e.Graphics.DrawImage(PrintImage5, loc);
                e.Graphics.DrawImage(PrintImage5, loc2);
                e.Graphics.DrawImage(PrintImage5, loc3);
                e.Graphics.DrawImage(PrintImage5, loc4);
            }
            else if (radioButtonPrintValue.Checked)
            {
                //int i = 0;
                //e.Graphics.DrawImage(PrintImage, loc);
                //e.Graphics.DrawImage(PrintImage, loc2);
                //e.Graphics.DrawImage(PrintImage, loc3);
                //e.Graphics.DrawImage(PrintImage, loc4);
                //while (i < repeatTimes1)
                //{
                //    e.HasMorePages = true;

                //    i++;
                //}
                //e.HasMorePages = false;

              if(nPage < noofPages){
                switch (nPage)
                {
                    //case 0:
                    //    e.Graphics.DrawImage(PrintImage, loc);
                    //    e.Graphics.DrawImage(PrintImage, loc2);
                    //    e.Graphics.DrawImage(PrintImage, loc3);
                    //    e.Graphics.DrawImage(PrintImage, loc4);
                    //    break;
                    //case 1:
                    //    e.Graphics.DrawImage(PrintImage, loc);
                    //    e.Graphics.DrawImage(PrintImage, loc2);
                    //    e.Graphics.DrawImage(PrintImage, loc3);
                    //    e.Graphics.DrawImage(PrintImage, loc4);
                    //    break;
                    default:
                        e.Graphics.DrawImage(PrintImage5, loc);
                        e.Graphics.DrawImage(PrintImage5, loc2);
                        e.Graphics.DrawImage(PrintImage5, loc3);
                        e.Graphics.DrawImage(PrintImage5, loc4);
                        break;
                }
              }
                ++nPage;


                if (nPage < noofPages)
                    e.HasMorePages = true;
                else
                {
                   // e.HasMorePages = true;

                    switch (k)
                    {
                        default:
                            if (lastpageitems == 19)
                            {
                                 e.Graphics.DrawImage(PrintImage5, loc);
                                 e.Graphics.DrawImage(PrintImage5, loc2);
                                 e.Graphics.DrawImage(PrintImage5, loc3);
                                 e.Graphics.DrawImage(PrintImage4, loc4);
                            }
                            else if (lastpageitems == 18)
                            {
                                 e.Graphics.DrawImage(PrintImage5, loc);
                                 e.Graphics.DrawImage(PrintImage5, loc2);
                                 e.Graphics.DrawImage(PrintImage5, loc3);
                                 e.Graphics.DrawImage(PrintImage3, loc4);
                            }
                            else if (lastpageitems == 17)
                            {
                                 e.Graphics.DrawImage(PrintImage5, loc);
                                 e.Graphics.DrawImage(PrintImage5, loc2);
                                 e.Graphics.DrawImage(PrintImage5, loc3);
                                 e.Graphics.DrawImage(PrintImage2, loc4);
                            }
                            else if (lastpageitems == 16)
                            {
                                 e.Graphics.DrawImage(PrintImage5, loc);
                                 e.Graphics.DrawImage(PrintImage5, loc2);
                                 e.Graphics.DrawImage(PrintImage5, loc3);
                                 e.Graphics.DrawImage(PrintImage1, loc4);
                            }
                            else if (lastpageitems == 15)
                            {
                                 e.Graphics.DrawImage(PrintImage5, loc);
                                 e.Graphics.DrawImage(PrintImage5, loc2);
                                 e.Graphics.DrawImage(PrintImage5, loc3);
                            }
                            else if (lastpageitems == 14)
                            {
                                 e.Graphics.DrawImage(PrintImage5, loc);
                                 e.Graphics.DrawImage(PrintImage5, loc2);
                                 e.Graphics.DrawImage(PrintImage4, loc3);
                            }
                            else if (lastpageitems == 13)
                            {
                                 e.Graphics.DrawImage(PrintImage5, loc);
                                 e.Graphics.DrawImage(PrintImage5, loc2);
                                 e.Graphics.DrawImage(PrintImage3, loc3);
                            }
                            else if (lastpageitems == 12)
                            {
                                 e.Graphics.DrawImage(PrintImage5, loc);
                                 e.Graphics.DrawImage(PrintImage5, loc2);
                                 e.Graphics.DrawImage(PrintImage2, loc3);
                            }
                            else if (lastpageitems == 12)
                            {
                                 e.Graphics.DrawImage(PrintImage5, loc);
                                 e.Graphics.DrawImage(PrintImage5, loc2);
                                 e.Graphics.DrawImage(PrintImage1, loc3);
                            }
                            else if (lastpageitems == 9)
                            {
                                 e.Graphics.DrawImage(PrintImage5, loc);
                                 e.Graphics.DrawImage(PrintImage4, loc2);
                            }
                            else if (lastpageitems == 8)
                            {
                                 e.Graphics.DrawImage(PrintImage5, loc);
                                 e.Graphics.DrawImage(PrintImage3, loc2);
                            }
                            else if (lastpageitems == 7)
                            {
                                 e.Graphics.DrawImage(PrintImage5, loc);
                                 e.Graphics.DrawImage(PrintImage2, loc2);
                            }
                            else if (lastpageitems == 6)
                            {
                                 e.Graphics.DrawImage(PrintImage5, loc);
                                 e.Graphics.DrawImage(PrintImage1, loc2);
                            }
                            
                            else if (lastpageitems == 5)
                            {
                                e.Graphics.DrawImage(PrintImage5, loc);
                            }
                            else if (lastpageitems == 4)
                            {
                                e.Graphics.DrawImage(PrintImage4, loc);
                            }
                            else if (lastpageitems == 3)
                            {
                                e.Graphics.DrawImage(PrintImage3, loc);
                            }
                            else if (lastpageitems == 2)
                            {
                                e.Graphics.DrawImage(PrintImage2, loc);
                            }
                            else if (lastpageitems == 1)
                            {
                                e.Graphics.DrawImage(PrintImage1, loc);
                            }

                            break;
                    }
                   

                    if (k == 0)
                        e.HasMorePages = true;
                    else
                        e.HasMorePages = false;
                    k++;
                }
                

            }
        }
      
      

        private void cmbLabel_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbA4.SelectedIndex = 0;
            if (cmbLabel.Text == "265 x 143")
            {
                BWidth = 258;
                BHeight = 43;
            }
        }

        private void LabelprintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            //PrintDocument pd = new PrintDocument();
            //PaperSize paperSize = new PaperSize("MyCustomSize", 2, 2); //numbers are optional

            //paperSize.RawKind = (int)PaperKind.Custom;

            //pd.DefaultPageSettings.PaperSize = paperSize;

          //  e.PageSettings.PaperSize = new PaperSize("Label", 10, 50);
            Point loc = new Point(0, 2);

            if (nPage < noofPages)
            {
                switch (nPage)
                {
                    default:
                     e.Graphics.DrawImage(PrintImage1, loc);
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

        private void cmbA4_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbLabel.SelectedIndex = 0;
        }

        private void btnForeColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog cdialog = new ColorDialog())
            {
                cdialog.AnyColor = true;
                if (cdialog.ShowDialog() == DialogResult.OK)
                {
                    this.b.ForeColor = cdialog.Color;
                    this.btnForeColor.BackColor = cdialog.Color;
                }//if
            }//using
        }

        private void btnBackColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog cdialog = new ColorDialog())
            {
                cdialog.AnyColor = true;
                if (cdialog.ShowDialog() == DialogResult.OK)
                {
                    this.b.BackColor = cdialog.Color;
                    this.btnBackColor.BackColor = cdialog.Color;
                }//if
            }//using
        }

        private void radioButtonItemcode_CheckedChanged(object sender, EventArgs e)
        {
            txtData.Text = txtitemCode.Text;
        }

        private void radioButtonItemandB_CheckedChanged(object sender, EventArgs e)
        {
            txtData.Text = txtitemCode.Text + "/" + txtBatchCode.Text;
        }

        private void radioButtonserialno_CheckedChanged(object sender, EventArgs e)
        {
            txtData.Text = txtSerialNo.Text ;
        }

        private void button1_Click(object sender, EventArgs e)
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

        public void SendReceiveData()
        {
            SerialPort SerialObj = new SerialPort();
            byte[] cmdByteArray = new byte[1];
            SerialObj.DiscardInBuffer();
            SerialObj.DiscardOutBuffer();

            //send         
            cmdByteArray[0] = 0x7a;
            SerialObj.Write(cmdByteArray, 0, 1);

           // CommTimer tmrComm = new CommTimer();
           // tmrComm.Start(4000);
            //while ((SerialObj.BytesToRead == 0) && (tmrComm.timedout == false))
            //{
            //    Application.DoEvents();
            //}
            if (SerialObj.BytesToRead > 0)
            {
                byte[] inbyte = new byte[1];
                SerialObj.Read(inbyte, 0, 1);
                if (inbyte.Length > 0)
                {
                    byte value = (byte)inbyte.GetValue(0);
                    //do other necessary processing you may want. 
                }
            }
           // tmrComm.tmrComm.Dispose();
            SerialObj.DiscardInBuffer();
            SerialObj.DiscardOutBuffer();
            SerialObj.Close();
        }

      
    }
}
