using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using FF.BusinessObjects;

namespace FF.WebERPClient.Test
{
    public class PrintTest1:BasePage
    {
        private Font printFont;
        private string headding;

        public string Headding
        {
            get { return headding; }
            set { headding = value; }
        }
        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        private int pageNo;

        public int PageNo
        {
            get { return pageNo; }
            set { pageNo = value; }
        }
        private string cusname;

        public string Cusname
        {
            get { return cusname; }
            set { cusname = value; }
        }
        private string add1;

        public string Add1
        {
            get { return add1; }
            set { add1 = value; }
        }
        private string add2;

        public string Add2
        {
            get { return add2; }
            set { add2 = value; }
        }
        private string refNo;

        public string RefNo
        {
            get { return refNo; }
            set { refNo = value; }
        }
        private string date;

        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        private string location;

        public string Location
        {
            get { return location; }
            set { location = value; }
        }
        string cusCode;

        public string CusCode
        {
            get { return cusCode; }
            set { cusCode = value; }
        }
        string inv_no;

        public string Inv_no
        {
            get { return inv_no; }
            set { inv_no = value; }
        }



        public bool SetVariables(string _invno)
        {
            try
            {
                DataTable dt = CHNLSVC.Sales.GetInvRep(_invno);
                string inv_tp = dt.Rows[0]["SAH_INV_TP"].ToString();
                int isVat = 0;
                Inv_no = _invno;
                //isVat=Convert.ToInt32(dt.Rows[0]["SAH_IS_SVAT"]);
                if (inv_tp == "CRED")
                    Headding = "CREDIT SALE";
                else if (inv_tp == "CS")
                {
                    if (isVat == 0)
                        Headding = "CASH INVOICE";
                    else
                        Headding = "TAX INVOICE";
                }
                else if (inv_tp == "HS")
                    Headding = "HIRE SALES DELIVERY ORDER";
                else
                    Headding = "CASH INVOICE";

                Address = dt.Rows[0]["MPC_ADD_1"].ToString() + dt.Rows[0]["MPC_ADD_2"].ToString();
                CusCode = dt.Rows[0]["SAH_CUS_CD"].ToString();
                Cusname = dt.Rows[0]["SAH_CUS_NAME"].ToString();
                Add1 = dt.Rows[0]["SAH_CUS_ADD1"].ToString();
                Add2 = dt.Rows[0]["SAH_CUS_ADD2"].ToString();
                RefNo = dt.Rows[0]["SAH_INV_NO"].ToString();
                Date = dt.Rows[0]["SAH_DT"].ToString();
                Location = dt.Rows[0]["SAH_PC"].ToString() + dt.Rows[0]["MPC_DESC"].ToString();
                Print();
                return true;
            }
            catch (Exception) {
                return false;
            }
        }


        public void Print()
        {
            TimeSpan start1 = DateTime.Now.TimeOfDay;
            System.Drawing.Printing.PrintDocument pp = new System.Drawing.Printing.PrintDocument();
            printFont = new Font("Arial", 8);
            PrintDocument pd = new PrintDocument();
            pp.PrintPage += new PrintPageEventHandler
               (this.pd_PrintPage);
            PaperSize pz = new PaperSize();
            pp.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("PaperA4", 826, 1169);
            pp.DefaultPageSettings.Margins.Left = 0;
            pp.DefaultPageSettings.Margins.Top = 0;
            pp.DefaultPageSettings.Landscape = true;
            TimeSpan end1 = DateTime.Now.TimeOfDay;
            TimeSpan diiff = end1 - start1;
            if (Session["defaultPrinter"] != null)
                pp.PrinterSettings.PrinterName = Session["defaultPrinter"].ToString();
            else
                return;
            TimeSpan end3 = DateTime.Now.TimeOfDay;
            TimeSpan diiff2 = end3 - start1;
            pp.Print();
            TimeSpan end2 = DateTime.Now.TimeOfDay;
            TimeSpan diiff1 = end2 - start1;
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            float yPos = 0;
            float leftMargin = ev.MarginBounds.Left;
            float rightMargin = ev.MarginBounds.Right;
            float topMargin = ev.MarginBounds.Top;
            string line = null;
            //ev.PageSettings.PaperSize.Height=;
            //ev.PageSettings.PaperSize.Width=;
            ev.Graphics.DrawString(Headding, printFont, Brushes.Black, leftMargin, yPos + 45, new StringFormat());
            ev.Graphics.DrawString(Address, printFont, Brushes.Black, (rightMargin - leftMargin) / 2, yPos + 45, new StringFormat());
            //loc sec
            ev.Graphics.DrawString(RefNo, printFont, Brushes.Black, rightMargin + 65, yPos, new StringFormat());
            ev.Graphics.DrawString(Date, printFont, Brushes.Black, rightMargin + 65, yPos + 15, new StringFormat());
            ev.Graphics.DrawString(Location, printFont, Brushes.Black, rightMargin + 65, yPos + 30, new StringFormat());

            //customer sec
            ev.Graphics.DrawString("Customer", printFont, Brushes.Black, leftMargin, yPos + 75, new StringFormat());
            ev.Graphics.DrawString(":", printFont, Brushes.Black, leftMargin + 61, yPos + 75, new StringFormat());
            ev.Graphics.DrawString(Cusname, printFont, Brushes.Black, leftMargin + 65, yPos + 75, new StringFormat());
            ev.Graphics.DrawString(Add1, printFont, Brushes.Black, leftMargin + 65, yPos + 85, new StringFormat());
            ev.Graphics.DrawString(Add2, printFont, Brushes.Black, leftMargin + 65, yPos + 105, new StringFormat());

            //item des
            ev.Graphics.DrawString("Item Code", printFont, Brushes.Black, leftMargin, yPos + 125, new StringFormat());
            ev.Graphics.DrawString("Description", printFont, Brushes.Black, leftMargin + 100, yPos + 125, new StringFormat());
            ev.Graphics.DrawString("Model", printFont, Brushes.Black, leftMargin + 300, yPos + 125, new StringFormat());
            ev.Graphics.DrawString("VAT", printFont, Brushes.Black, leftMargin + 360, yPos + 125, new StringFormat());
            ev.Graphics.DrawString("Qty", printFont, Brushes.Black, leftMargin + 420, yPos + 125, new StringFormat());
            ev.Graphics.DrawString("Unit Price", printFont, Brushes.Black, leftMargin + 500, yPos + 125, new StringFormat());
            ev.Graphics.DrawString("Discount", printFont, Brushes.Black, leftMargin + 600, yPos + 125, new StringFormat());
            ev.Graphics.DrawString("Total", printFont, Brushes.Black, leftMargin + 660, yPos + 125, new StringFormat());
            DataTable dt = CHNLSVC.Sales.GetInvRep(Inv_no);
            foreach (DataRow dr in dt.Rows)
            {
                
                    //item detail loop
                    yPos =yPos+   20;
                    ev.Graphics.DrawString(dt.Rows[0]["SAD_ITM_CD"].ToString(), printFont, Brushes.Black, leftMargin, yPos + 145, new StringFormat());
                    ev.Graphics.DrawString(dt.Rows[0]["MI_SHORTDESC"].ToString(), printFont, Brushes.Black, leftMargin + 100, yPos + 145, new StringFormat());
                    ev.Graphics.DrawString(dt.Rows[0]["MI_MODEL"].ToString(), printFont, Brushes.Black, leftMargin + 300, yPos  + 145, new StringFormat());
                    ev.Graphics.DrawString(dt.Rows[0]["SAD_ITM_TAX_AMT"].ToString(), printFont, Brushes.Black, leftMargin + 360, yPos + 145, new StringFormat());
                    ev.Graphics.DrawString(dt.Rows[0]["SAD_QTY"].ToString(), printFont, Brushes.Black, leftMargin + 420, yPos + 145, new StringFormat());
                    ev.Graphics.DrawString(dt.Rows[0]["SAD_UNIT_RT"].ToString(), printFont, Brushes.Black, leftMargin + 500, yPos + 145, new StringFormat());
                    ev.Graphics.DrawString(dt.Rows[0]["SAD_DISC_AMT"].ToString(), printFont, Brushes.Black, leftMargin + 600, yPos + 145, new StringFormat());
                    ev.Graphics.DrawString(dt.Rows[0]["SAD_TOT_AMT"].ToString(), printFont, Brushes.Black, leftMargin + 660, yPos+ 145, new StringFormat());
                
               
                 //serial loop
                //ev.Graphics.DrawString(dt.Rows[0]["MI_SHORTDESC"].ToString(), printFont, Brushes.Black, leftMargin + 100, yPos + 145, new StringFormat());
                //ev.Graphics.DrawString(dt.Rows[0]["SAD_ITM_TAX_AMT"].ToString(), printFont, Brushes.Black, leftMargin + 360, yPos + 145, new StringFormat());
                //ev.Graphics.DrawString(dt.Rows[0]["SAD_ITM_TAX_AMT"].ToString(), printFont, Brushes.Black, leftMargin + 360, yPos + 145, new StringFormat());
                //ev.Graphics.DrawString(dt.Rows[0]["SAD_ITM_TAX_AMT"].ToString(), printFont, Brushes.Black, leftMargin + 360, yPos + 145, new StringFormat());

                //ev.Graphics.DrawString(dt.Rows[0]["SAD_ITM_TAX_AMT"].ToString(), printFont, Brushes.Black, leftMargin + 360, yPos + 145, new StringFormat());
                //ev.Graphics.DrawString(dt.Rows[0]["SAD_ITM_TAX_AMT"].ToString(), printFont, Brushes.Black, leftMargin + 360, yPos + 145, new StringFormat());
                //ev.Graphics.DrawString(dt.Rows[0]["SAD_ITM_TAX_AMT"].ToString(), printFont, Brushes.Black, leftMargin + 360, yPos + 145, new StringFormat());
                //ev.Graphics.DrawString(dt.Rows[0]["SAD_ITM_TAX_AMT"].ToString(), printFont, Brushes.Black, leftMargin + 360, yPos + 145, new StringFormat());
            }
            //customer NIC  and speical promotion remark
            ev.Graphics.DrawString("Customer NIC", printFont, Brushes.Black, leftMargin + 100, yPos + 175, new StringFormat());
            ev.Graphics.DrawString("Special Promotion Remark", printFont, Brushes.Black, leftMargin + 550, yPos + 175, new StringFormat());

            ev.Graphics.DrawString("Remarks", printFont, Brushes.Black, leftMargin, yPos + 200, new StringFormat());
            ev.Graphics.DrawString(":", printFont, Brushes.Black, leftMargin + 150, yPos + 200, new StringFormat());

            //line
            Pen pen = new Pen(Brushes.Black);
            ev.Graphics.DrawLine(pen, 360, yPos + 175, 750, yPos + 175);
            ev.Graphics.DrawString("Sub Total", printFont, Brushes.Black, 360, yPos + 180, new StringFormat());
            ev.Graphics.DrawString("VAT", printFont, Brushes.Black, 360, yPos + 195, new StringFormat());
            ev.Graphics.DrawString("Total Invoice Amount", printFont, Brushes.Black, 360, yPos + 225, new StringFormat());
            //line
            Pen pen1 = new Pen(Brushes.Black);
            pen1.Width = 2;
            ev.Graphics.DrawLine(pen1, 360, yPos + 175 + 75, 750, yPos + 175 + 75);
            ev.Graphics.DrawLine(pen1, 360, yPos + 175 + 80, 750, yPos + 175 + 80);

            ev.Graphics.DrawString("Cash Payment:", printFont, Brushes.Black, leftMargin, yPos + 380, new StringFormat());
            ev.Graphics.DrawString("7,450.21", printFont, Brushes.Black, leftMargin + 150, yPos + 380, new StringFormat());
        }

    }
}