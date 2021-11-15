using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using FF.BusinessObjects.Sales;
using System.Globalization;
using System.Data;
using FF.Interfaces;
using System.Text;


namespace FF.WebERPClient.Purchasing_Modules
{
    public partial class SupplierQuotation : BasePage
    {
       //static List<QoutationDetails> QT_itm_Det_list=null;//= new List<QoutationDetails>();//all Lines for the quotation
        public List<QoutationDetails> QT_itm_Det_list
        { // List<QoutationDetails> qT_itm_Det_list = null;
            get { return (List<QoutationDetails>)ViewState["QT_itm_Det_list"]; }
            set { ViewState["QT_itm_Det_list"] = value; }
        }
//------------------------------------------------------------------------------------   
      
       // static Int32 lineCount=0;
       // Int32 lineCount = 0;

        public Int32 lineCount
        {
            get { return Convert.ToInt32(ViewState["lineCount"]); }
            set { ViewState["lineCount"] = value; }
        }
//-------------------------------------------------------------------------------------
       // static List<QuotationHeader> Current_QT_header_List = new List<QuotationHeader>();
    
        public List<QuotationHeader> Current_QT_header_List
        {
            get { return (List<QuotationHeader>)ViewState["Current_QT_header_List"]; }
            set { ViewState["Current_QT_header_List"] = value; }
        }
//-----------------------------------------------------------------------------------------------

        //static QuotationHeader Current_QT_header= null;
       
        public QuotationHeader Current_QT_header
        {
            get { return (QuotationHeader)ViewState["Current_QT_header"]; }
            set { ViewState["Current_QT_header"] = value; }
        }

//-----------------------------------------------------------------------------------------------
      //  static List<QoutationDetails> Current_QT_itm_Det_list = new List<QoutationDetails>();//all Lines for the current item
       
        public List<QoutationDetails> Current_QT_itm_Det_list
        {
            get { return (List<QoutationDetails>)ViewState["Current_QT_itm_Det_list"]; }
            set { ViewState["Current_QT_itm_Det_list"] = value; }
        }
//------------------------------------------------------------------------------------------------
        QoutationDetails Current_itm_line = new QoutationDetails();
//------------------------------------------------------------------------------------------------
        //static Int32 currentSeqNo = -99;
       // Int32 currentSeqNo = -99;

        public Int32 currentSeqNo
        {
            get { return Convert.ToInt32(ViewState["currentSeqNo"]); }
            set { ViewState["currentSeqNo"] = value; }
        }

//------------------------------------------------------------------------------------------------
        protected void Page_Load(object sender, EventArgs e)
        {
            //for item code
            txtToAddItemCD.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnItemCd.ClientID + "')");
            //for supplier
            txtSuppCD.Attributes.Add("onKeyup", "return clickButton(event,'" + ImageBtnSupplier.ClientID + "')");
            txtVeiwSupp.Attributes.Add("onKeyup", "return clickButton(event,'" + ImgBtnVeiwSupp.ClientID + "')");
            //Current_QT_itm_Det_list = new List<QoutationDetails>();

            if (!IsPostBack)
            {
               
              // Current_QT_itm_Det_list = new List<QoutationDetails>();
               Current_QT_itm_Det_list = new List<QoutationDetails>();
               Current_QT_header_List = new List<QuotationHeader>();

               QT_itm_Det_list = new List<QoutationDetails>();//all Lines for the quotation
               Current_QT_header = new QuotationHeader();

               lineCount = 0;
               currentSeqNo = -99;
              
            }
            OnBlur();
        }

        protected void btnImg_add_Click(object sender, ImageClickEventArgs e)
        {
            if (txtToAddItemCD.Text.Trim() == "" || txtQuotPrice.Text.Trim() == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please Enter Itemcode and Unit price.");
                return;

            }
            try {
               Decimal price = Convert.ToDecimal(txtQuotPrice.Text.Trim());
            }
            catch(Exception ex){
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter valid Unit price.");
                return;
            }
            //var chek_items = (from cust in QT_itm_Det_list
            //             where cust.Qd_itm_cd == txtToAddItemCD.Text.Trim() && cust.Qd_no==txtQuotaNo.Text.Trim()
            //             select cust).ToList();
            //if (chek_items != null || chek_items.Count > 0)
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Item code is already added.");
            //    return;
            //}




            //-----To QUO_HDR table

            if (Current_QT_header == null)//if header not exists
            {
                

                if (txtSuppCD.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter Supplier Code.");
                    return;
                }
                if (txtFromDt.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter From Date");
                    return;
                }
                if (txtExpDate.Text == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter Exp. Date");
                    return;
                }
                if (ddlSuppType.SelectedIndex == 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select Type of the Quotation");
                    return;
                }

                //create a new header
               
                try
                {
                    DateTime frmDT = Convert.ToDateTime(txtFromDt.Text.Trim());
                    DateTime expDT = Convert.ToDateTime(txtExpDate.Text.Trim());

                    if (expDT < frmDT)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Exp.date should be grater than From date!");
                        return;
                    }
                    else
                    {
                        //Current_QT_header.Qh_frm_dt = Convert.ToDateTime(txtFromDt.Text.Trim()).Date;
                        //Current_QT_header.Qh_ex_dt = Convert.ToDateTime(txtExpDate.Text.Trim()).Date;
                    }

                }
                catch (Exception ex)
                {

                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Date is not in correct format.");
                    return;
                }
                Current_QT_header = new QuotationHeader();
                Current_QT_header.Qh_frm_dt = Convert.ToDateTime(txtFromDt.Text.Trim()).Date;
                Current_QT_header.Qh_ex_dt = Convert.ToDateTime(txtExpDate.Text.Trim()).Date;

                Current_QT_header.Qh_session_id = GlbUserSessionID;
                Current_QT_header.Qh_com = GlbUserComCode;
                Current_QT_header.Qh_cre_by = GlbUserName;
                Current_QT_header.Qh_cre_when = DateTime.Now;
                // Current_QT_header.Qh_no = txtQuotaNo.Text.Trim();
                Current_QT_header.Qh_party_cd = txtSuppCD.Text.Trim();
                // Current_QT_header.Qh_party_name = lblSuppName.Text.Trim();
                Current_QT_header.Qh_pc = GlbUserDefProf;
                Current_QT_header.Qh_ref = txtManRefNo.Text.Trim();
                Current_QT_header.Qh_remarks = txtNote.Text.Trim();

                // currentSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "QUO", 1, GlbUserComCode); //this is done when saving
                //Current_QT_header.Qh_seq_no = currentSeqNo;

                Current_QT_header.Qh_tp = "S";
                Current_QT_header.Qh_sub_tp = ddlSuppType.SelectedValue;

                Current_QT_header.Qh_stus = "P";
                Current_QT_header.Qh_session_id = GlbUserSessionID;
                //fill not null values
                Current_QT_header.Qh_com = GlbUserComCode;
                Current_QT_header.Qh_dt = DateTime.Now.Date;
                Current_QT_header.Qh_party_name = Current_QT_header.Qh_party_cd;//change this later according to the supplier selected.
                Current_QT_header.Qh_cre_by = GlbUserName;
                Current_QT_header.Qh_cre_when = DateTime.Now;
                Current_QT_header.Qh_mod_when = DateTime.Now;
                Current_QT_header.Qh_mod_by = GlbUserName;

                //////////////////////
                QT_itm_Det_list = new List<QoutationDetails>();
                Current_QT_itm_Det_list.Clear();
                QT_itm_Det_list.Clear(); //added on 31-6-2012
            }
            else
            {

                // Current_QT_itm_Det_list.Clear();
            }

            //-----To QUO_DET table
            QoutationDetails itm_line = new QoutationDetails();
            itm_line.Qd_itm_cd = txtToAddItemCD.Text.Trim();

            try
            {
                MasterItem msit = new MasterItem();
                msit = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtToAddItemCD.Text.Trim());

                itm_line.Qd_itm_desc = msit.Mi_longdesc;
                itm_line.Qd_nitm_desc = msit.Mi_model;// Qd_nitm_desc is assigned for the display purpose only 
            }
            catch (Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Item Code does not exists!");
                return;
            }
            if (QT_itm_Det_list != null)
            {
                foreach (QoutationDetails item in QT_itm_Det_list)
                {
                    if (item.Qd_itm_cd == txtToAddItemCD.Text.Trim())
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "The item has been already added!");
                        return;
                    }
                }
            }
            
            itm_line.Qd_no = txtQuotaNo.Text.Trim();
            itm_line.Qd_seq_no = Current_QT_header.Qh_seq_no;//or currentSeqNo
          
            itm_line.Qd_unit_price = Convert.ToDecimal(txtQuotPrice.Text.Trim());
            lineCount++;
            itm_line.Qd_line_no = lineCount;//this is the first row of grid 2

            itm_line.Qd_frm_qty = 1;

            itm_line.Qd_to_qty = 99999999;

            //not null columns
            itm_line.Qd_amt = 0;
            itm_line.Qd_dit_rt = 0;
            itm_line.Qd_dis_amt = 0;
            itm_line.Qd_itm_tax = 0;
            itm_line.Qd_tot_amt = 0;
            itm_line.Qd_pb_price = 0;
            itm_line.Qd_resline_no = 0;

            ///////////////////
            Current_itm_line = itm_line;

            QT_itm_Det_list.Add(itm_line); // add to all Lines of the quotation

            Current_QT_itm_Det_list.Clear();
            Current_QT_itm_Det_list.Add(itm_line);
            //  List<QoutationDetails> items = new List<QoutationDetails>();
            //  GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count(), theSum = group.Sum(x => x.Tus_qty) });
            var items = (QT_itm_Det_list.GroupBy(x => new { x.Qd_itm_cd }).Select(group => new { Peo = group.Key }));
            List<QoutationDetails> itemaList = new List<QoutationDetails>();
            foreach (var itm in items)
            {
                QoutationDetails it = new QoutationDetails();
                it.Qd_itm_cd = itm.Peo.Qd_itm_cd;

                MasterItem msit = new MasterItem();
                msit = CHNLSVC.Inventory.GetItem(GlbUserComCode, itm.Peo.Qd_itm_cd);

                it.Qd_itm_desc = msit.Mi_longdesc;
                it.Qd_nitm_desc = msit.Mi_model;// Qd_nitm_desc is assigned for the display purpose only 
                itemaList.Add(it);
            }
            // GridView_itm_det.DataSource = Current_QT_itm_Det_list;//change later by linq query
            GridView_itm_det.DataSource = itemaList; //Current_QT_itm_Det_list;
            GridView_itm_det.DataBind();

            Bind_Qty_det_GridView(QT_itm_Det_list, itm_line.Qd_itm_cd);
            //GridView_Qty_det.DataSource = Current_QT_itm_Det_list;
            //GridView_Qty_det.DataBind();
            txtQuotPrice.Text = null;
            Panel_QTDetails.GroupingText = itm_line.Qd_itm_cd;



        }

        protected void btnVeiwSearch_Click(object sender, EventArgs e)
        {
            GridView_Quatations.Visible = true;
            string QType = ddlVeiwQuotStat.SelectedValue.Trim();
            if (QType=="")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please Select Quotation Status. ");
                return;
            }
            string SuppCd = txtVeiwSupp.Text.Trim();
            if (SuppCd == "" )
            {
                SuppCd = null;
            }
         
           
            //DateTime fromDt;
            //DateTime toDt;
            try {
                //if (txtViewFromDT.Text == null && txtViewFromDT.Text == "")
                //{
                //    fromDt = DateTime.MinValue;
                //    toDt = DateTime.Now;
                //}
                //else
                //{
                //    fromDt = Convert.ToDateTime(Convert.ToDateTime(txtViewFromDT.Text.Trim()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                //    toDt = Convert.ToDateTime(Convert.ToDateTime(txtViewToDT.Text.Trim()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)); 
                //}
                
                //call procedure.
                List<QuotationHeader> quotList = new List<QuotationHeader>();
                //quotList = CHNLSVC.Sales.Get_all_Quotations(GlbUserComCode, GlbUserDefProf, SuppCd, QType, fromDt, toDt);
                quotList = CHNLSVC.Sales.Get_all_Quotations(GlbUserComCode, GlbUserDefProf, SuppCd, QType, txtViewFromDT.Text.Trim(), txtViewToDT.Text.Trim());

                List<QuotationHeader> quotListnew = new List<QuotationHeader>();

                //if (txtVeiwSupp.Text.Trim() != null || txtVeiwSupp.Text.Trim() != "")
                if (!string.IsNullOrEmpty(txtVeiwSupp.Text.Trim()))
                {
                    foreach (QuotationHeader hd in quotList)
                    {
                        if (hd.Qh_party_cd == txtVeiwSupp.Text.Trim())
                        {
                            quotListnew.Add(hd);
                        }
                    }

                   
                }
                else 
                {
                    quotListnew = quotList;
                }

                if (quotListnew.Count == 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No records found!");
                    GridView_Quatations.DataSource = null;
                    GridView_Quatations.DataBind();
                    GridView_Quatations.Visible = false;
                    return;
                }
               
                //List<QuotationHeader> quotListBind = new List<QuotationHeader>();
                //foreach(QuotationHeader qh in quotListnew)
                //{
                //    qh.Qh_dt = qh.Qh_dt.Date;
                //    qh.Qh_ex_dt = qh.Qh_ex_dt.Date;
                //    qh.Qh_frm_dt = qh.Qh_frm_dt.Date;
                //    quotListBind.Add(qh);
                //}
                ////GridView_Quatations.DataSource = quotListnew;
                ////GridView_Quatations.DataBind();

                GridView_Quatations.DataSource = quotListnew;
                GridView_Quatations.DataBind();

                Current_QT_header_List = quotList;

            }
            catch(Exception ex){
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Error " + ex.Message);
            }
            
           
        }

        protected void linkbtnItmCD_Click(object sender, EventArgs e)
        {
            //LinkButton linkBtn=(LinkButton) GridView_itm_det.SelectedRow.FindControl("linkbtnItmCD");
            //txtManRefNo.Text= linkBtn.Text;
        }

        protected void btnImg_addLine_Click(object sender, ImageClickEventArgs e)
        {
            if (txtToAddItemCD.Text.Trim() == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select item Code. ");
                return;
            }
            if (txtLineFromQty.Text.Trim() == "" || txtLineFromQty.Text.Trim() == "" || txtLineToQty.Text.Trim() == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please Enter all item details. ");
                return;
            }
           
           
            //-----To QUO_DET table

            var selectedItmcode = (from hd in QT_itm_Det_list
                                   where hd.Qd_itm_cd == txtToAddItemCD.Text.Trim()
                                   select hd).ToList();
            if (selectedItmcode == null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select item code first, or add new item code to the quotation. ");
                return;
            }
            //-----------Qty range validation-----------------------
            Decimal QdFrmQty;
            Decimal QdToQty;
            Decimal QdUnitPrice;
            try
            {

                QdFrmQty = Convert.ToDecimal(txtLineFromQty.Text.Trim());
                QdToQty = Convert.ToDecimal(txtLineToQty.Text.Trim());
                QdUnitPrice = Convert.ToDecimal(txtLinePrice.Text.Trim());
            }
            catch (Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Qty or unit price is not invalid!");
                return;
            }

            if (QdUnitPrice < 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Unit price cannot be negetive!");
                return;
            }

            if (QdFrmQty > QdToQty)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From Qty cannot be greater than To Qty!");//range invalid
                return;
            }
            if (QdFrmQty == QdToQty)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From Qty cannot be equal to  To Qty!");
                return;
            }

            var currrange = (from cur in Current_QT_itm_Det_list
                             where cur.Qd_frm_qty == QdFrmQty && cur.Qd_to_qty == QdToQty
                             select cur).ToList();

            if (currrange.Count > 0)// ||currrange !=null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Qty range allready added to the list!");
                return;
            }

            var curFrmqty = (from cur in Current_QT_itm_Det_list
                             where cur.Qd_frm_qty == QdFrmQty
                             select cur).ToList();

            if (curFrmqty.Count > 0)//|| curFrmqty != null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From Qty already exists!");
                return;
            }
            var curToqty = (from cur in Current_QT_itm_Det_list
                            where cur.Qd_to_qty == QdToQty
                            select cur).ToList();

            if (curToqty.Count > 0)//|| curToqty != null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "To Qty already exists!");
                return;
            }

            Decimal maxFrmQty = 0;
            Decimal maxToQty = 0;
            if (Current_QT_itm_Det_list.Count > 0)
            {
                maxFrmQty = (from c in Current_QT_itm_Det_list
                             select c.Qd_frm_qty).Max();

                maxToQty = (from c in Current_QT_itm_Det_list
                            select c.Qd_to_qty).Max();
            }
            else //then it is the first range to be added so, from qty is between 0 and 1 
            {
                if (!(QdFrmQty <= 1))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "The first range should start form 1 or less than 1!");
                    return;
                }
            }


            //var usedRange= (from cur in Current_QT_itm_Det_list
            //              where cur.Qd_frm_qty<QdFrmQty && cur.Qd_to_qty>QdToQty 
            //             select cur).ToList();
            if (QdFrmQty > maxToQty + 1)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Range not set properly!");
                return;
            }
            if (QdFrmQty < maxToQty + Convert.ToDecimal(0.000001))//if from qty not greater than even a bit than existing maxtoqty
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "The From Qty should be greater than " + maxToQty);
                return;
            }

            if (QdToQty < maxToQty)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "The From Qty should be greater than " + QdToQty);
                return;
            }
            //---------------validation------------------------

            QoutationDetails itm_line = new QoutationDetails();
            itm_line.Qd_itm_cd = txtToAddItemCD.Text.Trim();
            //itm_line.Qd_itm_desc=
            //itm_line.Qd_line_no
            //itm_line.Qd_no = txtQuotaNo.Text.Trim();-------------------------------------******added 31st
            itm_line.Qd_no = Current_QT_header.Qh_no;
            itm_line.Qd_seq_no = Current_QT_header.Qh_seq_no;//or currentSeqNo
            // itm_line.Qd_line_no = 1;//because this is the first row of grid 2
            lineCount++;
            itm_line.Qd_line_no = lineCount;
            itm_line.Qd_unit_price = Convert.ToDecimal(txtLinePrice.Text.Trim());
            itm_line.Qd_frm_qty = Convert.ToDecimal(txtLineFromQty.Text.Trim());
            itm_line.Qd_to_qty = Convert.ToDecimal(txtLineToQty.Text.Trim());

            //not null columns
            itm_line.Qd_amt = 0;
            itm_line.Qd_dit_rt = 0;
            itm_line.Qd_dis_amt = 0;
            itm_line.Qd_itm_tax = 0;
            itm_line.Qd_tot_amt = 0;
            itm_line.Qd_pb_price = 0;
            itm_line.Qd_resline_no = 0;

            ///////////////////

            if (selectedItmcode.Count == 0)
            {
                // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "first one!!!!!!!!");

            }


            //Current_QT_itm_Det_list.Clear();
            Current_itm_line = itm_line;
            Current_QT_itm_Det_list.Add(itm_line);//add to the line list.

            QT_itm_Det_list.Add(itm_line); // add to all Lines of the quotation

            //GridView_Qty_det.DataSource = Current_QT_itm_Det_list;
            //GridView_Qty_det.DataBind();
            Bind_Qty_det_GridView(QT_itm_Det_list, itm_line.Qd_itm_cd);

            Panel_QTDetails.GroupingText = "Item : " + itm_line.Qd_itm_cd;
        }

        // OnRowCommand="GridView_Quatations_RowCommand"
        // CommandName="GETQOUTATION"
        protected void GridView_Quatations_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "GETQOUTATION":
                    { 
                        // static List<QuotationHeader> Current_QT_header_List = new List<QuotationHeader>();
                        var selectedHDR = (from hd in Current_QT_header_List
                                           where hd.Qh_no ==e.CommandArgument.ToString()
                                           select hd).ToList();
                        if (selectedHDR == null || selectedHDR.Count==0)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "This Quotation does not contain any items!");
                            return;
                        }
                        Current_QT_header = selectedHDR.First(); 
                        QT_itm_Det_list = CHNLSVC.Sales.Get_all_linesForQoutation(e.CommandArgument.ToString());//all Lines for the selected quotation

                        if (QT_itm_Det_list.Count != 0 && QT_itm_Det_list != null)
                        {
                            var maxlineNo = (from c in QT_itm_Det_list
                                             select c.Qd_line_no).Max();

                            lineCount = maxlineNo;
                        }
                        else
                        { lineCount = 0; }
                      //  txtFromDt.Text = Current_QT_header.Qh_frm_dt.ToString();
                      //  txtExpDate.Text = Current_QT_header.Qh_ex_dt.ToString();

                        DateTime thisDate = Current_QT_header.Qh_frm_dt;
                        DateTime date = new DateTime(thisDate.Year, thisDate.Month, thisDate.Day);
                        CultureInfo culture = new CultureInfo("pt-BR");
                        Console.WriteLine(thisDate.ToString("d", culture));
                        txtFromDt.Text = thisDate.ToString("d", culture);

                        DateTime thisDate_ = Current_QT_header.Qh_ex_dt;
                        DateTime date_ = new DateTime(thisDate_.Year, thisDate_.Month, thisDate_.Day);
                        CultureInfo culture_ = new CultureInfo("pt-BR");
                        Console.WriteLine(thisDate_.ToString("d", culture_));
                        txtExpDate.Text = thisDate_.ToString("d", culture_);


                        txtQuotaNo.Text = Current_QT_header.Qh_no;
                        txtSuppCD.Text = Current_QT_header.Qh_party_cd;
                        lblSuppName.Text = Current_QT_header.Qh_party_name;
                        ddlSuppType.SelectedValue = Current_QT_header.Qh_sub_tp;
                        txtManRefNo.Text = Current_QT_header.Qh_ref;
                        txtNote.Text = Current_QT_header.Qh_remarks;
                        
                        txtToAddItemCD.Text="";
                        txtQuotPrice.Text = "";
                        txtLineFromQty.Text="";
                        txtLineToQty.Text="";
                        txtLinePrice.Text = "";

                        BindItemsGridView();
                        Bind_Qty_det_GridView(QT_itm_Det_list, "");

                        TabContainer1.ActiveTabIndex = 0;
                        break;
                    }
            }
        }

        protected void GridView_itm_det_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "SELECTITEMCODE":
                    { 
                        //var itm_lines=  QT_itm_Det_list.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count(), theSum = group.Sum(x => x.Tus_qty) });
                        var itm_lines=  QT_itm_Det_list.GroupBy(x => new { x.Qd_itm_cd}).Select(group => new { Peo = group.Key, theCount = group.Count()});

                        Bind_Qty_det_GridView(QT_itm_Det_list, e.CommandArgument.ToString());

                        txtToAddItemCD.Text = e.CommandArgument.ToString();

                        //var items = (from cust in QT_itm_Det_list
                        //             where cust.Qd_itm_cd == e.CommandArgument.ToString()
                        //             select cust).ToList();

                        //Current_QT_itm_Det_list = items;
                        //GridView_Qty_det.DataSource = Current_QT_itm_Det_list;
                        //GridView_Qty_det.DataBind();
               
                        break;
                    }
            }
        }



    //    OnRowCommand="GridView_Qty_det_RowCommand"
        // CommandName="UPDATEPRICE"
        protected void GridView_Qty_det_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "UPDATEPRICE":
                    {
                        GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                        TextBox txtnewPrice = (TextBox)row.FindControl("txt_unitprice");
                        //TextBox txtnewPrice = (TextBox)row.FindControl("txt_lineUnitPrice");
                        Decimal newPrice=Convert.ToDecimal(txtnewPrice.Text.Trim());
                        Label lblLineNo=(Label)row.FindControl("lblLine");
                        Int32 lineNo=Convert.ToInt32(lblLineNo.Text.Trim());

                        var di = (from d in QT_itm_Det_list where d.Qd_line_no == lineNo select d).First();

                        QT_itm_Det_list.Remove(di);
                        di.Qd_unit_price = newPrice;
                        QT_itm_Det_list.Add(di);//add the same object after updating the price

                        Bind_Qty_det_GridView(QT_itm_Det_list, di.Qd_itm_cd);
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Price updated!");

                        break;
                    }
            }
        }
        private void BindItemsGridView()
        {
            var items = (QT_itm_Det_list.GroupBy(x => new { x.Qd_itm_cd }).Select(group => new { Peo = group.Key }));
            //var items = (QT_itm_Det_list.GroupBy(x => new { x.Qd_itm_cd }).Select(group => new { Peo = group.Key }));
            List<QoutationDetails> itemaList = new List<QoutationDetails>();
            foreach (var itm in items)
            {
                QoutationDetails it = new QoutationDetails();
                it.Qd_itm_cd = itm.Peo.Qd_itm_cd;

                MasterItem msit = new MasterItem();
                msit = CHNLSVC.Inventory.GetItem(GlbUserComCode, itm.Peo.Qd_itm_cd);

                it.Qd_itm_desc = msit.Mi_longdesc;
                it.Qd_nitm_desc = msit.Mi_model;// Qd_nitm_desc is assigned for the display purpose only 
                itemaList.Add(it);
            }

            //sort the item list to display
            var itemaList_sort = (from cust in itemaList
                             orderby cust.Qd_itm_cd
                             select cust).ToList();

            GridView_itm_det.DataSource = itemaList_sort; //Current_QT_itm_Det_list;
            GridView_itm_det.DataBind();
        }

        private void Bind_Qty_det_GridView(List<QoutationDetails> QTitemaList, string currentitemCode)
        {
            var items = (from cust in QTitemaList
                         where cust.Qd_itm_cd == currentitemCode
                         orderby cust.Qd_frm_qty
                         select cust).ToList();

            Current_QT_itm_Det_list = items;
            GridView_Qty_det.DataSource = Current_QT_itm_Det_list;
            GridView_Qty_det.DataBind();

            Panel_QTDetails.GroupingText ="Item : "+ currentitemCode;
                       
        }

        protected void btnNewQuotation_Click(object sender, EventArgs e)
        {
            lineCount = 0;
            Current_QT_header.Qh_seq_no = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "QUO", 1, GlbUserComCode);
        }

        protected void btn_update_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnSave_approve_Click(object sender, EventArgs e)
        {
            if (QT_itm_Det_list == null || QT_itm_Det_list.Count < 1 ||Current_QT_header==null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot save empty quotation. Please add items.");
                return;
            }
            //write to the original tables- QUO_HDR, QUO_DET
            //generate seqno
            //if no quotation exist in QUO_HDR, then insert new row
            //else update the existing one.
            string QuotationNo=txtQuotaNo.Text.Trim();
            QuotationNo = Current_QT_header.Qh_no;
            QuotationHeader hd = CHNLSVC.Sales.Get_Quotation_HDR(QuotationNo);
            if (hd != null)
            {
                //update the existing header status to "A"
               QuotationNo=  Current_QT_header.Qh_no;
               Int32 rowsUpd= CHNLSVC.Sales.Update_Quotation_HDR_status(QuotationNo, "A");
                //delete all existing rows in quo_det and
                //insert them as new rows.
               
               CHNLSVC.Sales.Delete_Quotation_DET(QuotationNo);
               CHNLSVC.Sales.Save_QuotationDET(QT_itm_Det_list);
             //  Current_QT_header = null;

               MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Approved ");
            }

            else//if no header exists in quo_hdr 
            {
               if (QT_itm_Det_list==null)
               {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot save empty quotation. Please add items.");
                return;
               }
                Current_QT_header.Qh_stus = "A";
                Current_QT_header.Qh_seq_no = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "QUO", 1, GlbUserComCode);

                List<QoutationDetails> QT_itm_Det_list_SAVELIST = new List<QoutationDetails>();
                foreach (QoutationDetails line in QT_itm_Det_list)
                {
                    line.Qd_seq_no = Current_QT_header.Qh_seq_no;
                    QT_itm_Det_list_SAVELIST.Add(line);
                }
                Int32 rows = CHNLSVC.Sales.Save_QuotationHDR(Current_QT_header);//insert to quo_hdr
                //insert quo_det rows
                if (rows > 0)
                {
                    //Current_QT_header = null;
                }
               // CHNLSVC.Sales.Delete_Quotation_DET(QuotationNo);//not necessary to do this though
                CHNLSVC.Sales.Save_QuotationDET(QT_itm_Det_list_SAVELIST);//insert to quo_det
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
           
            if (QT_itm_Det_list == null || QT_itm_Det_list.Count < 1 || Current_QT_header == null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot save empty quotation. Please add items.");
                return;
            }

            try
            {
                DateTime date_ = Convert.ToDateTime(txtFromDt.Text.Trim());
                date_ = Convert.ToDateTime(txtExpDate.Text.Trim());
            }
            catch (Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, " Enter valid date format.");
                return;
            }
            if (txtFromDt.Text == "" || txtExpDate.Text == "")//|| txtManualRefNo.Text=="")             
            {
                // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please fill Date and Manual Ref");
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please fill Date.");
                return;
            }
            
            
            //if(txtQuotaNo.Text.Trim()=="")
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter the Quotation No.");
            //    return;
            //}
            string QuotationNo=txtQuotaNo.Text.Trim();
            //QuotationNo = Current_QT_header.Qh_no;
            
            QuotationHeader hd = CHNLSVC.Sales.Get_Quotation_HDR(QuotationNo);
            if (hd != null)
            {
                if (hd.Qh_stus == "A")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "This Quotaion is APPROVED. Modifications will not be applied! ");
                    return;
                }
                else//exists as pending quotaion 
                {
                    //so the quotaion detail can be changed.
                    CHNLSVC.Sales.Delete_Quotation_DET(QuotationNo);
                    CHNLSVC.Sales.Save_QuotationDET(QT_itm_Det_list);
                   // Current_QT_header = null;

                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Updated the quotation. ");
                    /////////////////added on 7-6-2012
                    QT_itm_Det_list = new List<QoutationDetails>();//all Lines for the quotation
                    lineCount = 0;
                    //Current_QT_header_List.Clear();
                    // Current_QT_header = new QuotationHeader();



                    Current_QT_itm_Det_list = new List<QoutationDetails>();
                    Current_itm_line = new QoutationDetails();
                    currentSeqNo = -99;

                    Bind_Qty_det_GridView(Current_QT_itm_Det_list, "");
                    BindItemsGridView();

                    txtToAddItemCD.Text = "";
                    txtQuotaNo.Text = "";
                    txtFromDt.Text = "";
                    txtExpDate.Text = "";
                    txtManRefNo.Text = "";
                    txtSuppCD.Text = "";
                    lblSuppName.Text = "";
                    txtNote.Text = "";
                    ddlSuppType.SelectedIndex = 0;

                    Current_QT_header = null; //added on 7-6-2012
                    QT_itm_Det_list = null;

                    ////////////////////////////////////////////////
                }

            }
            else//brand new quoation 
            {
                if (txtSuppCD.Text.Trim() == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Supplier code!");
                    return;
                }
                List<MasterBusinessEntity> suppliersList = CHNLSVC.Inventory.GetServiceAgent("S");
                if (suppliersList != null || suppliersList.Count == 0)
                {
                    var supcd = (from sp in suppliersList
                                 where sp.Mbe_cd == txtSuppCD.Text.Trim()
                                 select sp).Count();
                    if (supcd <1)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Valid Supplier code!");
                        lblSuppName.Text = "";
                        return;
                    }
                    else
                    {
                        var items = (suppliersList.GroupBy(x => new { x.Mbe_cd, x.Mbe_name }).Select(group => new { SupCD = group.Key.Mbe_cd, Nam = group.Key.Mbe_name }));
                        foreach (var sup in items)//has only one entry
                        {
                            if (txtSuppCD.Text.Trim() == sup.SupCD)
                            {
                                lblSuppName.Text = sup.Nam.ToString();
                            }

                        }
                    }

                }

                if(ddlSuppType.SelectedValue=="")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Select Quotation Type(Normal/Consignment)!");
                    return;
                }
                if (QT_itm_Det_list == null || QT_itm_Det_list.Count<1)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot save empty quotation. Please add items.");
                    return;
                }
                Current_QT_header.Qh_stus = "P";

                Current_QT_header.Qh_seq_no = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "QUO", 1, GlbUserComCode);
                //Current_QT_header.Qh_no = QuotationNo;
               Current_QT_header.Qh_no = Current_QT_header.Qh_seq_no.ToString();
                List<QoutationDetails> QT_itm_Det_list_SAVELIST = new List<QoutationDetails>();
                foreach(QoutationDetails line in QT_itm_Det_list)
                {
                    line.Qd_seq_no = Current_QT_header.Qh_seq_no;
                    line.Qd_no = Current_QT_header.Qh_no;
                    QT_itm_Det_list_SAVELIST.Add(line);
                }
               // Current_QT_header.Qh_com = GlbUserComCode;
                //enter new row to the quo_hdr. and rows to quo_det
                //Int32 rows = CHNLSVC.Sales.Quotation_save(Current_QT_header, QT_itm_Det_list);

                //MasterAutoNumber masterAuto = new MasterAutoNumber();
                //masterAuto.Aut_cate_cd = GlbUserDefLoca;
                //masterAuto.Aut_cate_tp = "LOC";
                //masterAuto.Aut_direction = null;
                //masterAuto.Aut_modify_dt = null;
                //masterAuto.Aut_moduleid = "QUO";
                //// masterAuto_plus.Aut_number = 5;//what is Aut_number
                //masterAuto.Aut_start_char = "QUO";
                //masterAuto.Aut_modify_dt = DateTime.Now;
                //masterAuto.Aut_year = DateTime.Now.Year;

                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = GlbUserComCode;
                masterAuto.Aut_cate_tp = "LOC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
               // masterAuto.Aut_moduleid = "ADJ";
                masterAuto.Aut_moduleid = "QUO";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "QUO";
                masterAuto.Aut_year = null;

                string QTNum;

                try
                {

                    //var ci = new CultureInfo("xx-XX");
                    //var dateTime = Convert.ToDateTime(txtFromDt.Text, ci);

                    DateTime frmDT = Convert.ToDateTime(txtFromDt.Text.Trim());
                    DateTime expDT = Convert.ToDateTime(txtExpDate.Text.Trim());

                    if (expDT < frmDT)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Exp.date should be grater than From date!");
                        return;
                    }
                   
                    else
                    {
                        Current_QT_header.Qh_frm_dt = Convert.ToDateTime(txtFromDt.Text.Trim()).Date;
                        Current_QT_header.Qh_ex_dt = Convert.ToDateTime(txtExpDate.Text.Trim()).Date;
                        
                        
                        Current_QT_header.Qh_cre_when = DateTime.Now;
                        Current_QT_header.Qh_party_cd = txtSuppCD.Text.Trim();
                        Current_QT_header.Qh_pc = GlbUserDefProf;
                        Current_QT_header.Qh_ref = txtManRefNo.Text.Trim();
                        Current_QT_header.Qh_remarks = txtNote.Text.Trim();
                        Current_QT_header.Qh_sub_tp = ddlSuppType.SelectedValue;
                        List<MasterBusinessEntity> suppliersList_ = CHNLSVC.Inventory.GetServiceAgent("S");
                        if (suppliersList_ != null || suppliersList_.Count == 0)
                        {
                            var supcd = (from sp in suppliersList_
                                         where sp.Mbe_cd == txtSuppCD.Text.Trim()
                                         select sp).Count();
                            if (supcd < 1)
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Valid Supplier code!");
                                lblSuppName.Text = "";
                                return;
                            }
                            else
                            {
                                var items = (suppliersList_.GroupBy(x => new { x.Mbe_cd, x.Mbe_name }).Select(group => new { SupCD = group.Key.Mbe_cd, Nam = group.Key.Mbe_name }));
                                foreach (var sup in items)//has only one entry
                                {
                                    if (txtSuppCD.Text.Trim() == sup.SupCD)
                                    {
                                        lblSuppName.Text = sup.Nam.ToString();
                                    }

                                }
                            }


                        }
                        Current_QT_header.Qh_tp = "S";
                        Current_QT_header.Qh_sub_tp = ddlSuppType.SelectedValue;

                        Current_QT_header.Qh_stus = "P";
                        Current_QT_header.Qh_session_id = GlbUserSessionID;
                        //fill not null values
                        Current_QT_header.Qh_com = GlbUserComCode;
                        Current_QT_header.Qh_dt = DateTime.Now.Date;
                        //Current_QT_header.Qh_party_name = Current_QT_header.Qh_party_cd;//change this later according to the supplier selected.
                        Current_QT_header.Qh_party_name = lblSuppName.Text;
                        Current_QT_header.Qh_cre_by = GlbUserName;
                        Current_QT_header.Qh_cre_when = DateTime.Now;
                        Current_QT_header.Qh_mod_when = DateTime.Now;
                        Current_QT_header.Qh_mod_by = GlbUserName;
                    }

                    //if (Convert.ToDateTime(txtFromDt.Text.Trim()).Date > Convert.ToDateTime(txtExpDate.Text.Trim()).Date)
                    //{
                    //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Exp.date should be grater than From date!");
                    //    return;
                    //}

                }
                catch (Exception ex)
                {

                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Date is not in correct format.");
                    return;
                }
               
                Int32 effect = CHNLSVC.Sales.Quotation_save(Current_QT_header, QT_itm_Det_list_SAVELIST, masterAuto,out QTNum);
                //Int32 rows = CHNLSVC.Sales.Save_QuotationHDR(Current_QT_header);//insert to quo_hdr
                //Int32 eff = CHNLSVC.Sales.Save_QuotationDET(QT_itm_Det_list_SAVELIST);//insert to quo_det
               // Current_QT_header = null;

                if (effect > 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "New Quoataion Saved Successfully! Quotation No: " + QTNum);

                    /////////////////added on 7-6-2012
                    QT_itm_Det_list = new List<QoutationDetails>();//all Lines for the quotation
                    lineCount = 0;
                    //Current_QT_header_List.Clear();
                    // Current_QT_header = new QuotationHeader();



                    Current_QT_itm_Det_list = new List<QoutationDetails>();
                    Current_itm_line = new QoutationDetails();
                    currentSeqNo = -99;

                    Bind_Qty_det_GridView(Current_QT_itm_Det_list, "");
                    BindItemsGridView();

                    txtToAddItemCD.Text = "";
                    txtQuotaNo.Text = "";
                    txtFromDt.Text = "";
                    txtExpDate.Text = "";
                    txtManRefNo.Text = "";
                    txtSuppCD.Text = "";
                    lblSuppName.Text = "";
                    txtNote.Text = "";
                    ddlSuppType.SelectedIndex = 0;

                    Current_QT_header = null; //added on 7-6-2012
                    QT_itm_Det_list = null;

                    ////////////////////////////////////////////////
                }
                else
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "New Not Quoataion Saved! ");
                }

                //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Saved the New Quotation. ");
            }

        }

        protected void btnSaveAs_pend_Click(object sender, EventArgs e)
        {
            if (txtSuppCD.Text.Trim() == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Supplier code!");
                return;
            }
            List<MasterBusinessEntity> suppliersList = CHNLSVC.Inventory.GetServiceAgent("S");
            if (suppliersList != null || suppliersList.Count == 0)
            {
                var supcd = (from sp in suppliersList
                             where sp.Mbe_cd == txtSuppCD.Text.Trim()
                             select sp).Count();
                if (supcd < 1)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Valid Supplier code!");
                    return;
                }

            }
            if (ddlSuppType.SelectedValue == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Select Quotation Type(Normal/Consignment)!");
                return;
            }
            if (QT_itm_Det_list == null || QT_itm_Det_list.Count < 1)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot save empty quotation. Please add items.");
                return;
            }

            try
            {
                DateTime date_ = Convert.ToDateTime(txtFromDt.Text);
                date_ = Convert.ToDateTime(txtExpDate.Text);
            }
            catch (Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, " Enter valid date format.");
                return;
            }


            DateTime _frmDT_ = Convert.ToDateTime(txtFromDt.Text.Trim());
            DateTime _expDT_ = Convert.ToDateTime(txtExpDate.Text.Trim());

            if (_expDT_ < _frmDT_)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Exp.date should be grater than From date!");
                return;
            }

            string QuotationNo = txtQuotaNo.Text.Trim();

            QuotationHeader hd = CHNLSVC.Sales.Get_Quotation_HDR(QuotationNo);
            if (hd != null)
            {
                if (hd.Qh_stus == "A")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "This Quotaion is APPROVED. Enter new Number to save as different Quotation.");
                    return;
                }
                else if (hd.Qh_stus == "P")//exists as pending quotaion 
                {
                    ////so the quotaion detail can be changed.
                    CHNLSVC.Sales.Delete_Quotation_DET(QuotationNo);
                   
                    ////Current_QT_header = null;
                    //List<QoutationDetails> QT_itm_Det_list_SAVELIST = new List<QoutationDetails>();
                    //foreach (QoutationDetails line in QT_itm_Det_list)
                    //{
                    //    line.Qd_seq_no = Current_QT_header.Qh_seq_no;
                    //    line.Qd_no = Current_QT_header.Qh_no;
                    //    QT_itm_Det_list_SAVELIST.Add(line);
                        
                    //}
                    //QT_itm_Det_list = QT_itm_Det_list_SAVELIST;
                    CHNLSVC.Sales.Save_QuotationDET(QT_itm_Det_list);
                    //commented on 30-5-2012
                //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter new Quoataion number to save as new. ");
                 //   return;
                }

            }

            //if()
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "This Quotation No. is existing. Please enter a new No.");
            //    return;
            //}
           

            //Current_QT_header.Qh_no = QuotationNo;
            Current_QT_header.Qh_stus = "P";

            FillNewData_HDR();//*****
            //QuotationNo = Current_QT_header.Qh_no;

            //generate new seq_no
            Current_QT_header.Qh_seq_no = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "QUO", 1, GlbUserComCode);
            Current_QT_header.Qh_no = Current_QT_header.Qh_seq_no.ToString();

            List<QoutationDetails> QT_itm_Det_list_SAVELIST = new List<QoutationDetails>();
            foreach (QoutationDetails line in QT_itm_Det_list)
            {
                line.Qd_seq_no = Current_QT_header.Qh_seq_no;
                line.Qd_no = Current_QT_header.Qh_no;
                QT_itm_Det_list_SAVELIST.Add(line);
            }
            
            QT_itm_Det_list = QT_itm_Det_list_SAVELIST;
            
            //Int32 rows = CHNLSVC.Sales.Save_QuotationHDR(Current_QT_header);
            //if (rows > 0)
            //{
            //    //Current_QT_header = null;
            //    //insert all quo_det rows
            //   // CHNLSVC.Sales.Delete_Quotation_DET(QuotationNo);//not necessary to do this though
            //   // CHNLSVC.Sales.Save_QuotationDET(QT_itm_Det_list_SAVELIST);//insert to quo_det
            //    CHNLSVC.Sales.Save_QuotationDET(QT_itm_Det_list);//insert to quo_det
            //}


            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_cd = GlbUserComCode;
            masterAuto.Aut_cate_tp = "LOC";
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "QUO";
            masterAuto.Aut_number = 5;//what is Aut_number
            masterAuto.Aut_start_char = "QUO";
            masterAuto.Aut_year = null;

            string QTNum;
            Current_QT_header.Qh_frm_dt = Convert.ToDateTime(txtFromDt.Text); 
            Current_QT_header.Qh_ex_dt = Convert.ToDateTime(txtExpDate.Text);
            Current_QT_header.Qh_party_cd = txtSuppCD.Text.Trim();
            Current_QT_header.Qh_party_name = lblSuppName.Text.Trim();
           
            Int32 effect = CHNLSVC.Sales.Quotation_save(Current_QT_header, QT_itm_Det_list_SAVELIST, masterAuto, out QTNum);
            if (effect > 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "New Quoataion Saved Successfully! Quotation No: " + QTNum);
                /////////////////added on 7-6-2012
                QT_itm_Det_list = new List<QoutationDetails>();//all Lines for the quotation
                lineCount = 0;
                //Current_QT_header_List.Clear();
                // Current_QT_header = new QuotationHeader();



                Current_QT_itm_Det_list = new List<QoutationDetails>();
                Current_itm_line = new QoutationDetails();
                currentSeqNo = -99;

                Bind_Qty_det_GridView(Current_QT_itm_Det_list, "");
                BindItemsGridView();

                txtToAddItemCD.Text = "";
                txtQuotaNo.Text = "";
                txtFromDt.Text = "";
                txtExpDate.Text = "";
                txtManRefNo.Text = "";
                txtSuppCD.Text = "";
                lblSuppName.Text = "";
                txtNote.Text = "";
                ddlSuppType.SelectedIndex = 0;

                Current_QT_header = null; //added on 7-6-2012
                QT_itm_Det_list = null;

                ////////////////////////////////////////////////
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "New Quoataion Not Saved! ");
            }
            
            
        }

        private Int32 save_new_quotation()
        {
            string QuotationNo;//= txtQuotaNo.Text.Trim();
            QuotationNo = Current_QT_header.Qh_no;

            QuotationHeader hd = CHNLSVC.Sales.Get_Quotation_HDR(QuotationNo);
            if (hd != null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "This Please Enter New Quatation No.");
                return 0;
            }
            return 1;
        
        }

        private void FillNewData_HDR()
        {
            //create a new header
            Current_QT_header.Qh_session_id = GlbUserSessionID;
            Current_QT_header.Qh_com = GlbUserComCode;
            Current_QT_header.Qh_cre_by = GlbUserName;
            Current_QT_header.Qh_cre_when = DateTime.Now;
            Current_QT_header.Qh_no = txtQuotaNo.Text.Trim();
            Current_QT_header.Qh_party_cd = txtSuppCD.Text.Trim();
            // Current_QT_header.Qh_party_name = lblSuppName.Text.Trim();
            Current_QT_header.Qh_pc = GlbUserDefProf;
            Current_QT_header.Qh_ref = txtManRefNo.Text.Trim();
            Current_QT_header.Qh_remarks = txtNote.Text.Trim();

            // currentSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "QUO", 1, GlbUserComCode); //this is done when saving
            //Current_QT_header.Qh_seq_no = currentSeqNo;

            Current_QT_header.Qh_tp = "S";
            Current_QT_header.Qh_sub_tp = ddlSuppType.SelectedValue;
            try
            {
                DateTime frmDT = Convert.ToDateTime(txtFromDt.Text.Trim());
                DateTime expDT = Convert.ToDateTime(txtExpDate.Text.Trim());
                Current_QT_header.Qh_frm_dt = frmDT;
                Current_QT_header.Qh_ex_dt = expDT;
            }
            catch (Exception ex)
            {

                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Date is not in correct format.");
                return;
            }

            Current_QT_header.Qh_stus = "P";
            Current_QT_header.Qh_session_id = GlbUserSessionID;
            //fill not null values

            Current_QT_header.Qh_com = GlbUserComCode;
            Current_QT_header.Qh_dt = DateTime.Now.Date;
            Current_QT_header.Qh_party_name = Current_QT_header.Qh_party_cd;//change this later according to the supplier selected.
            Current_QT_header.Qh_cre_by = GlbUserName;
            Current_QT_header.Qh_cre_when = DateTime.Now;
            Current_QT_header.Qh_mod_when = DateTime.Now;
            Current_QT_header.Qh_mod_by = GlbUserName;
        
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            QT_itm_Det_list=new List<QoutationDetails>();//all Lines for the quotation
            lineCount=0;
            //Current_QT_header_List.Clear();
           // Current_QT_header = new QuotationHeader();
           
           

            Current_QT_itm_Det_list=new List<QoutationDetails>();
            Current_itm_line = new QoutationDetails();
            currentSeqNo = -99;

            Bind_Qty_det_GridView(Current_QT_itm_Det_list, "");
            BindItemsGridView();

            txtQuotaNo.Text="";
            txtFromDt.Text="";
            txtExpDate.Text="";
            txtManRefNo.Text="";
            txtSuppCD.Text="";
            lblSuppName.Text="";
            txtNote.Text="";
            ddlSuppType.SelectedIndex = 0;

            Current_QT_header = null; //added on 7-6-2012
            QT_itm_Det_list = null;
            Response.Redirect("~/Purchasing_Modules/SupplierQuotation.aspx");
        }

      
        //---------------common search
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }


        protected void imgBtnItem_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable dataSource = CHNLSVC.CommonSearch.GetItemSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtToAddItemCD.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImageBtnSupplier_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
            DataTable dataSource = CHNLSVC.CommonSearch.GetSupplierData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtSuppCD.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImgBtnVeiwSupp_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
            DataTable dataSource = CHNLSVC.CommonSearch.GetSupplierData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtVeiwSupp.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void btnDeleteLast_Click(object sender, EventArgs e)
        {
            //delete the last row of the current item list. 
            //delete form QT_itm_Det_list aswell.
          
            if (Current_QT_itm_Det_list == null || Current_QT_itm_Det_list.Count==0)
            {
                return;
            }
            
            string itmCD = Current_QT_itm_Det_list[0].Qd_itm_cd;

            if (Current_QT_itm_Det_list.Count == 1)
            {
                var last_itemCD = (from cust in Current_QT_itm_Det_list select cust.Qd_itm_cd).Max();
            }

            var maxlineno=  (from cust in Current_QT_itm_Det_list select cust.Qd_line_no).Max();
            
            var new_Current_QT_itm_Det_list = Current_QT_itm_Det_list.RemoveAll(item => item.Qd_line_no == maxlineno);
            
            var new_QT_itm_Det_list = QT_itm_Det_list.RemoveAll(item => item.Qd_line_no == maxlineno && item.Qd_itm_cd == itmCD);

            Bind_Qty_det_GridView(Current_QT_itm_Det_list, itmCD);

           // BindItemsGridView();
            Panel_QTDetails.GroupingText = "Item : " + itmCD;

        }

        protected void btnValidate_Supplier_Click(object sender, EventArgs e)
        {
            List<MasterBusinessEntity> suppliersList = CHNLSVC.Inventory.GetServiceAgent("S");
            if (suppliersList != null || suppliersList.Count == 0)
            {
                var supcd = (from sp in suppliersList
                             where sp.Mbe_cd == txtSuppCD.Text.Trim()
                             select sp).Count();
                if (supcd < 1)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Valid Supplier code!");
                    lblSuppName.Text = "";
                    return;
                }
                else{
                    var items = (suppliersList.GroupBy(x => new { x.Mbe_cd ,x.Mbe_name}).Select(group => new { SupCD = group.Key.Mbe_cd, Nam=group.Key.Mbe_name }));
                    foreach (var sup in items)//has only one entry
                    {
                        if (txtSuppCD.Text.Trim()==sup.SupCD)
                        {
                            lblSuppName.Text = sup.Nam.ToString();
                        }
                        
                    }
                }
                

            }
        }

        private void OnBlur()
        {
            txtSuppCD.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnValidate_Supplier, ""));

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }
        //-----common search
       
    }
}