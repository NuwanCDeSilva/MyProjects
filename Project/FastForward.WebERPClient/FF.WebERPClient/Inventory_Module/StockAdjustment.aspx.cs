using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Data;
using System.Globalization;

namespace FF.WebERPClient
{
    public partial class WebForm2 : BasePage
    { 
        static List<ReptPickSerialsSub> list_ReptPickSerialsSubList = new List<ReptPickSerialsSub>();
        static List<ReptPickSerials> list_GetAllScanSerialsList = new List<ReptPickSerials>();
        static List<string> seqNumList = new List<string>();
        static  List<string> seqNumList_out = new List<string>();
      
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSaveAdj.Visible = false;
            //if (GlbSerialScanUserSeqNo <=0)
            //{
            //    ddlScanBatches.SelectedValue = GlbSerialScanUserSeqNo.ToString();
            //}
            
            if (!IsPostBack)
            {
              
                /* have to bind data to ddl -->scan batches(taken from the table)*/

                //Calendar1.Visible = false;

               // FF.Interfaces.IInventory p;
                btnSaveAdj.Visible = false;
                Panel1.Enabled = false;
                List<string> sl = new List<string>();
                sl.Add("--select--");

                sl = CHNLSVC.Inventory.GetAll_Adj_SubTypes();
                sl.Add("");
               
                ddlAdjSubTyepe.DataSource = sl;
                ddlAdjSubTyepe.DataBind();
                 ddlAdjSubTyepe.SelectedValue="";
                 
                //-------------------------------
                 //txtAdjustmentNo.Text = "";
                 DateTime thisDate = DateTime.Now;
                 DateTime date = new DateTime(thisDate.Year, thisDate.Month, thisDate.Day);
                 CultureInfo culture = new CultureInfo("pt-BR");
                 Console.WriteLine(thisDate.ToString("d", culture));
                 txtDate_.Text = thisDate.ToString("d", culture);
                 //txtDate_.Text = DateTime.Now.ToString();
                 //txtManualRefNo.Text = "";
                 //txtRemarks.Text = "";
                 ddlAdjBased.SelectedIndex = 0;

                //commented by prabhath due to compile error on my version
                // seqNumList = CHNLSVC.Inventory.GetAll_Adj_SeqNums_for_user(GlbUserName, "ADJ", 1, "ABA");
                
                seqNumList = CHNLSVC.Inventory.GetAll_Adj_SeqNums_for_user(GlbUserName, "ADJ", 1, GlbUserComCode);//for IN 
                seqNumList_out = CHNLSVC.Inventory.GetAll_Adj_SeqNums_for_user(GlbUserName, "ADJ", 0, GlbUserComCode);//for OUT
                foreach (string st in seqNumList_out)
                {
                    if(st!="")
                        seqNumList.Add(st);
                }
                seqNumList[0] = "New Scan";
                ddlScanBatches.DataSource = seqNumList;
                ddlScanBatches.DataBind();
                //  ddlScanBatches.Items.Insert(0, new ListItem("Please select a country", ""));
                //added on 1-6-2012
                if (GlbSerialScanUserSeqNo <= 0)
                {
                    ddlScanBatches.SelectedValue = GlbSerialScanUserSeqNo.ToString();
                }
            }
            if (ddlScanBatches.SelectedIndex != 0)
            {
                btnSerialScan.Visible = false;
            }

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) //skip header row 
            {
                DropDownList ddOpp = (DropDownList)e.Row.FindControl("ddlStatus");
                // List<string> status = new List<string>();
                // status.Add("GOOD");
                // status.Add("BAD");
                // ddOpp.DataSource = status;
                //ddOpp.DataBind();
                if (ddOpp != null)
                {
                    ddOpp.Items.Add("Good");
                    ddOpp.Items.Add("Bad");
                    //ddOpp.DataSource = status;
                    //ddOpp.DataTextField = "Status";
                    //ddOpp.DataValueField = "StatusID";
                    //ddOpp.DataBind();
                }

                e.Row.Cells[1].FindControl("ddlStatus").DataBind();

                // BindOpponentDD(ddOpp);
            }
        }

        protected void btnScanSerial_Click(object sender, EventArgs e)
        {
            //generate serial num
            //int seq_num = 10023; //hard coded for the moment.
            //redirect to SerialScan.aspx
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
           
          //  MasterMsgInfoUCtrl.Clear();
            if (ddlScanBatches.SelectedIndex==0)
            {
                Panel1.Enabled = false;
                Panel1.Visible = true;
                //btnSerialScan.Visible = false;
                btnSerialScan.Visible = true;
                ddlAdjType_.Enabled = true;
                ddlAdjType_.SelectedIndex = 2;
                return;
            }
            else if (ddlScanBatches.SelectedIndex != 0)
            {   //if the user wants a new scan.
                Panel1.Enabled = true;
                btnSerialScan.Visible = false;
                ddlAdjType_.Enabled = false;
            }
            int seq_num = int.Parse(ddlScanBatches.SelectedValue);

            // gridShowAdjustedData.DataSource = CHNLSVC.Inventory.GetAllScanSerials(GlbUserComCode, GlbUserDefLoca, GlbUserName, seq_num, "ADJ");
            //List<ReptPickSerialsSub> list = CHNLSVC.Inventory.GetAllScanSubSerialsList(seq_num, "ADJ");

            //  List<ReptPickSerialsSub> list = CHNLSVC.Inventory.GetAllScanSerials(GlbUserComCode, GlbUserDefLoca, GlbUserName, seq_num, "ADJ");
            //  gridShowAdjustedData.DataSource = list;
            list_GetAllScanSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, seq_num, "ADJ");
            list_ReptPickSerialsSubList = CHNLSVC.Inventory.GetAllScanSubSerialsList(seq_num, "ADJ");

            gridShowAdjustedData.DataSource = list_GetAllScanSerialsList;
            // gridShowAdjustedData.DataSource = list_ReptPickSerialsSubList;
            //gridShowAdjustedData.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, seq_num, "ADJ");

            gridShowAdjustedData.DataBind();
            // lblScanBatch.Text = "Scan batch: " + seq_num;
            string msg_selectedValue="value=" + ddlScanBatches.SelectedValue;
            lblBatchSeq.Text = msg_selectedValue;
            if (seqNumList_out.Count > 0)
            {
                ddlAdjType_.SelectedIndex = 0;
                // ddlAdjType.DataBind();
                lblBatchSeq.Text = msg_selectedValue + "->IN";
                lblDirect.Text = "IN:" + ddlScanBatches.SelectedValue;
                foreach (string sn in seqNumList_out)
                {
                    if (ddlScanBatches.SelectedValue.ToString() == sn)
                    {
                        ddlAdjType_.SelectedIndex = 1;
                        //ddlAdjType.DataBind();
                        lblBatchSeq.Text = msg_selectedValue + "->OUT";
                        lblDirect.Text = "OUT:" + ddlScanBatches.SelectedValue;
                    }
                    else
                    {
                    }
                    //  ddlAdjType.DataBind();
                }                       
            }
            else
            {
                ddlAdjType_.SelectedIndex = 0;
               // ddlAdjType.DataBind();
                lblBatchSeq.Text = msg_selectedValue + "->IN";
                lblDirect.Text = "IN:" + ddlScanBatches.SelectedValue;
            }

            Panel1.Visible = true;
           //-> btnSaveAdj.Visible = true;

            //ddlAdjType_.Enabled = true;

            //txtAdjustmentNo.Text = "";
            //string date = DateTime.Now.ToString();
            //txtDate_.Text = String.Format("{0:dd/mm/yyyy}", date);

            DateTime thisDate = DateTime.Now;
            DateTime date = new DateTime(thisDate.Year, thisDate.Month,thisDate.Day);
            CultureInfo culture = new CultureInfo("pt-BR");
            Console.WriteLine(thisDate.ToString("d", culture));
            txtDate_.Text = thisDate.ToString("d", culture);

            txtManualRefNo.Text = "N/A";
            txtRemarks.Text = "";
            ddlAdjBased.SelectedIndex = 0;
            ddlAdjSubTyepe.SelectedValue = "";

            //ADDED BY SACHITH
            //SET VISIBL ADD COST BUTTON
            if (ddlAdjType_.SelectedIndex == 0)
            {
                DropDownListItemCode.Items.Clear();
                DropDownListItemCode.Items.Add(new ListItem("", "-1"));
                DropDownListItemCode.DataSource = list_GetAllScanSerialsList;
                DropDownListItemCode.DataTextField = "Tus_itm_cd";
                DropDownListItemCode.DataValueField = "Tus_itm_cd";
                DropDownListItemCode.DataBind();
                DropDownListItemCode_SelectedIndexChanged(null, null);
                ButtonAddCost.Visible = true;
            }
            else
            {
                ButtonAddCost.Visible = false;
            }
            //END
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //if (Calendar1.Visible == false)
            //    Calendar1.Visible = true;
            //else
            //    Calendar1.Visible = false;
        }

        //protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        //{
        //    this.txtDate.Text = this.Calendar1.SelectedDate.ToString();
        //    Calendar1.Visible = false;
        //}

        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    //

        //}

        //protected void btnSave_Click(object sender, EventArgs e)
        //{   ReptPickSerials rps=new ReptPickSerials();
        //   // rps.Tus_batch_line=1001;//replace withbatch num

        //    //call -->CHNLSVC.Inventory.SaveAllScanSerials(

        //}

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSerialScan_Click(object sender, EventArgs e)
        {
            if (ddlScanBatches.SelectedIndex != 0)
            { return; }
            if (ddlAdjType_.SelectedIndex==2)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select Adjustment Type!");
                return;
            }
            //  int generated_seq = CHNLSVC.Inventory.Generate_new_seq_num("ADMIN", "ADJ", 1, "ABA");
            int generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "ADJ", 1, GlbUserComCode);
           
            //string direction=ddlAdjType.SelectedValue.Trim();         

            // lblNewSeq.Text = generated_seq.ToString();

            ReptPickHeader RPH = new ReptPickHeader();

            RPH.Tuh_doc_tp = "ADJ";
            RPH.Tuh_cre_dt = DateTime.Today;// Convert.ToDateTime(txtDate_.Text);//Calendar-SelectedDate;
            RPH.Tuh_ischek_itmstus = true;//change 
            RPH.Tuh_ischek_reqqty = true;//change
            RPH.Tuh_ischek_simitm = true;//change
            RPH.Tuh_session_id = GlbUserSessionID;
            RPH.Tuh_usr_com = GlbUserComCode;//change 
            RPH.Tuh_usr_id = GlbUserName;
            RPH.Tuh_usrseq_no = generated_seq;

            if (ddlAdjType_.SelectedValue == "+ADJ")
            {
                RPH.Tuh_direct = true;
                lblNewScanDirect.Text = "in";
                //write entry to TEMP_PICK_HDR
                int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
                if (affected_rows > 0)
                {
                    //set session variables
                    GlbSerialScanUserSeqNo = generated_seq;
                    GlbSerialScanDocumentType = "ADJ";
                    GlbSerialScanDirection = 1;
                    GlbSerialScanReturnUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                    GlbSerialScanRequestNo = null;
                    //finally redirect to scanSerial page
                    Response.Redirect("~/Inventory_Module/ItemScan.aspx");
                }
            }
            else if (ddlAdjType_.SelectedValue == "- ADJ")
            {
                RPH.Tuh_direct = false;
                lblNewScanDirect.Text = "out";
                //write entry to TEMP_PICK_HDR
                int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
                if (affected_rows > 0)
                {
                    //set session variables
                    GlbSerialScanUserSeqNo = generated_seq;
                    GlbSerialScanDocumentType = "ADJ";
                    GlbSerialScanDirection = 0;
                    GlbSerialScanReturnUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                    GlbSerialScanRequestNo = null;
                    //finally redirect to scanSerial page
                    Response.Redirect("~/Inventory_Module/ItemScan.aspx");
                }
            }

            //lblNumOfrows.Text = affected_rows.ToString();
        }

        protected void btnSaveAdj_Click(object sender, EventArgs e)
        {
            if (gridShowAdjustedData.Rows.Count != 0 && ddlScanBatches.SelectedIndex != 0)
            {
               
                Panel1.Visible = true;
               //-> btnSaveAdj.Visible = false;
                ddlScanBatches.Enabled =false;

               // txtAdjustmentNo.Text = null;
                txtManualRefNo.Text = null;
                txtRemarks.Text = null;
                
                // ddlAdjBased.SelectedValue;
                //ddlAdjSubTyepe.SelectedValue;
                // ddlAdjType.SelectedValue;
                ddlAdjType_.Enabled = false;
            }


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlAdjType_.Enabled = true;
           // btnSerialScan.Visible = true;
            Panel1.Visible = true;
          //->  btnSaveAdj.Visible = true;
            ddlScanBatches.Enabled = true;
           // txtAdjustmentNo.Text = "";
            //txtDate_.Text = DateTime.Now.ToString();
            DateTime thisDate = DateTime.Now;
            DateTime date = new DateTime(thisDate.Year, thisDate.Month, thisDate.Day);
            CultureInfo culture = new CultureInfo("pt-BR");
            Console.WriteLine(thisDate.ToString("d", culture));
            txtDate_.Text = thisDate.ToString("d", culture);

            txtManualRefNo.Text = "N/A";
            txtRemarks.Text = "";
            ddlAdjBased.SelectedIndex = 0;
            ddlAdjSubTyepe.SelectedValue = "";
            gridShowAdjustedData.DataSource = null;
            gridShowAdjustedData.DataBind();

            Panel1.Enabled = false;
            Panel1.Visible = true;
            //btnSerialScan.Visible = false;
            btnSerialScan.Visible = true;
            ddlAdjType_.Enabled = true;
            ddlAdjType_.SelectedIndex = 2;

            ddlScanBatches.SelectedIndex = 0;
            Response.Redirect("~/Inventory_Module/StockAdjustment.aspx");
        }

        protected void btnFinalSave_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtSupplier.Text))
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the supplier code!");
            //    txtSupplier.Focus();
            //    return;
            //}
            if(ddlAdjSubTyepe.SelectedValue=="")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select Adjustment sub type!");
                return;
            }
            //if (ddlAdjBased.SelectedValue == "")
            //{
            //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select Adjustment base!");
            //    return;
            //}
            if (list_GetAllScanSerialsList == null || list_GetAllScanSerialsList.Count < 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot save empty scanned batch!");
                return;
            }
           

            //string AdjNumber = txtAdjustmentNo.Text.Trim();
            string AdjNumber = "";
            string manualNum = txtManualRefNo.Text.Trim();
            string remarks = txtRemarks.Text.Trim();
            string adj_base = ddlAdjBased.SelectedValue;
            string adj_sub_type = ddlAdjSubTyepe.SelectedValue;
            string adj_type = ddlAdjType_.SelectedValue;
            InventoryHeader inHeader = new InventoryHeader();

            
                inHeader.Ith_acc_no = "4554545";
                inHeader.Ith_anal_1 = "";
                // inHeader.Ith_anal_10 = true;
                // inHeader.Ith_anal_11 = true;
                // inHeader.Ith_anal_12 = true;
                inHeader.Ith_anal_2 = "";
                inHeader.Ith_anal_3 = "";
                inHeader.Ith_anal_4 = "";
                inHeader.Ith_anal_5 = "";
                inHeader.Ith_anal_6 = 0;
                inHeader.Ith_anal_7 = 0;
                inHeader.Ith_anal_8 = DateTime.MinValue;
                inHeader.Ith_anal_9 = DateTime.MinValue;
                inHeader.Ith_bus_entity = "";
                inHeader.Ith_cate_tp = ddlAdjSubTyepe.SelectedValue.ToString();
                inHeader.Ith_channel = "";
                inHeader.Ith_com = GlbUserComCode;

                //inHeader.Ith_com ="";
                inHeader.Ith_com_docno = "12121";
                inHeader.Ith_cre_by = "1010";
                inHeader.Ith_cre_when = DateTime.MinValue;
                inHeader.Ith_del_add1 = "";
                inHeader.Ith_del_add2 = "";
                inHeader.Ith_del_code = "";

                inHeader.Ith_del_party = "";
                inHeader.Ith_del_town = "";


                inHeader.Ith_direct = true;
                //  inHeader.Ith_direct =true;
                inHeader.Ith_doc_date = DateTime.Today;
                //  inHeader.Ith_doc_date  =DateTime.MinValue;
                inHeader.Ith_doc_no = "DPS32-ADJ-12-588888";

                inHeader.Ith_doc_tp = "ADJ";
                //   inHeader.Ith_doc_tp ="";
                inHeader.Ith_doc_year = DateTime.Today.Year;
                inHeader.Ith_entry_no = "121";
                inHeader.Ith_entry_tp = "1212";
                inHeader.Ith_git_close = true;
                inHeader.Ith_git_close_date = DateTime.MinValue;
                inHeader.Ith_git_close_doc = "1";
                inHeader.Ith_isprinted = true;
                inHeader.Ith_is_manual = true;
                inHeader.Ith_job_no = "1212";
                inHeader.Ith_loading_point = "211";
                inHeader.Ith_loading_user = "12";
                inHeader.Ith_loc = GlbUserDefLoca;
                if (txtManualRefNo.Text == null || txtManualRefNo.Text == "")
                {
                    inHeader.Ith_manual_ref = "N/A";
                }
                else {
                    inHeader.Ith_manual_ref = txtManualRefNo.Text.Trim();
                }
                inHeader.Ith_manual_ref = txtManualRefNo.Text.Trim();
                // inHeader.Ith_manual_ref ="";
                inHeader.Ith_mod_by = "ADMIN";
                inHeader.Ith_mod_when = DateTime.MinValue;
                inHeader.Ith_noofcopies = 2;
                inHeader.Ith_oth_loc = "oth loc";

                inHeader.Ith_remarks = txtRemarks.Text;
                // inHeader.Ith_remarks ="";
                inHeader.Ith_sbu = "INV";
                inHeader.Ith_seq_no = 6;
                //inHeader.Ith_seq_no =54;
                inHeader.Ith_session_id = GlbUserSessionID;
                inHeader.Ith_stus = "A";
                inHeader.Ith_sub_tp = "CONSIGN"; //ddlAdjSubTyepe.SelectedValue.ToString();
                // inHeader.Ith_sub_tp ="";
                inHeader.Ith_vehi_no = "";
                //*********************************

                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = GlbUserDefLoca;
                masterAuto.Aut_cate_tp = "LOC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "ADJ";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "ADJ";
                masterAuto.Aut_year = null;
              //  masterAuto.Aut_year = Convert.ToDateTime(txtDate_.Text).Year;

                //Call Inward save for the ADJ+
                int rows_inserted = 0;
                List<ReptPickSerials> check2 = list_GetAllScanSerialsList;
                List<ReptPickSerialsSub> check1 = list_ReptPickSerialsSubList;
                if (ddlAdjType_.SelectedValue == "+ADJ")
                {
                    rows_inserted = CHNLSVC.Inventory.ADJPlus(inHeader, list_GetAllScanSerialsList, list_ReptPickSerialsSubList, masterAuto, out AdjNumber);
                    // Int16 SaveInwardScanSerial(InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub);
                    //string Msg = "<script>alert('Successfully Saved! Adjustment Document No. : " + AdjNumber + "');</script>";
                    //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                    
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Saved. Adjustment Number is: " + AdjNumber);
                    lblrowsInserted.Text = rows_inserted.ToString();
                   //-> btnSaveAdj.Visible = false;
                    ddlScanBatches.Enabled = true;
                   // btnSerialScan.Visible = true;
                    
                    //txtAdjustmentNo.Text = null;
                    txtManualRefNo.Text = null;
                    txtRemarks.Text = null;
                   // Panel1.Visible = false;
                    gridShowAdjustedData.DataSource = null;
                    gridShowAdjustedData.DataBind();
                    ///////////////////////////////////////////////

                    ////////////
                    ddlAdjType_.Enabled = true;

                    ddlScanBatches.Enabled = true;

                    DateTime thisDate = DateTime.Now;
                    DateTime date = new DateTime(thisDate.Year, thisDate.Month, thisDate.Day);
                    CultureInfo culture = new CultureInfo("pt-BR");
                    Console.WriteLine(thisDate.ToString("d", culture));
                    txtDate_.Text = thisDate.ToString("d", culture);

                    txtManualRefNo.Text = "N/A";
                    txtRemarks.Text = "";
                    ddlAdjBased.SelectedIndex = 0;
                    ddlAdjSubTyepe.SelectedValue = "";

                    ddlScanBatches.SelectedIndex = 0;
                    Panel1.Enabled = false;
                    ddlAdjType_.SelectedIndex = 2;
                    btnSerialScan.Visible = true;
                    ddlAdjType_.Enabled = true;
                    lblSubTpDesc.Text = null;

                    seqNumList = CHNLSVC.Inventory.GetAll_Adj_SeqNums_for_user(GlbUserName, "ADJ", 1, GlbUserComCode);//for IN 
                    seqNumList_out = CHNLSVC.Inventory.GetAll_Adj_SeqNums_for_user(GlbUserName, "ADJ", 0, GlbUserComCode);//for OUT
                    foreach (string st in seqNumList_out)
                    {
                        if (st != "")
                            seqNumList.Add(st);
                    }
                    seqNumList[0] = "New Scan";
                    ddlScanBatches.DataSource = seqNumList;
                    ddlScanBatches.DataBind();
                    //Response.Redirect("~/Inventory_Module/StockAdjustment.aspx");  
                }
            else if (ddlAdjType_.SelectedValue == "- ADJ")
            {
                inHeader.Ith_direct = false;

                rows_inserted = CHNLSVC.Inventory.ADJMinus(inHeader, list_GetAllScanSerialsList, list_ReptPickSerialsSubList, masterAuto, out AdjNumber);
               
                //string Msg = "<script>alert('Successfully Saved! Adjustment Document No. : " + AdjNumber + "');</script>";
                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Saved. Adjustment Document No. is: " + AdjNumber);
                lblrowsInserted.Text = rows_inserted.ToString();
                //->btnSaveAdj.Visible = false;
                ddlScanBatches.Enabled = true;

               //btnSerialScan.Visible = false;
                //txtAdjustmentNo.Text = null;
                txtManualRefNo.Text = null;
                txtRemarks.Text = null;
                lblSubTpDesc.Text = null;
                //Panel1.Visible = false;
                gridShowAdjustedData.DataSource = null;
                gridShowAdjustedData.DataBind();
                
                ////////////
                ddlAdjType_.Enabled = true;
               
                ddlScanBatches.Enabled = true;
               
                DateTime thisDate = DateTime.Now;
                DateTime date = new DateTime(thisDate.Year, thisDate.Month, thisDate.Day);
                CultureInfo culture = new CultureInfo("pt-BR");
                Console.WriteLine(thisDate.ToString("d", culture));
                txtDate_.Text = thisDate.ToString("d", culture);

                txtManualRefNo.Text = "N/A";
                txtRemarks.Text = "";
                ddlAdjBased.SelectedIndex = 0;
                ddlAdjSubTyepe.SelectedValue = "";

                ddlScanBatches.SelectedIndex = 0;
                Panel1.Enabled = false;
                ddlAdjType_.SelectedIndex = 2;
                btnSerialScan.Visible = true;
                ddlAdjType_.Enabled = true;
                lblSubTpDesc.Text = null;

                seqNumList = CHNLSVC.Inventory.GetAll_Adj_SeqNums_for_user(GlbUserName, "ADJ", 1, GlbUserComCode);//for IN 
                seqNumList_out = CHNLSVC.Inventory.GetAll_Adj_SeqNums_for_user(GlbUserName, "ADJ", 0, GlbUserComCode);//for OUT
                foreach (string st in seqNumList_out)
                {
                    if (st != "")
                        seqNumList.Add(st);
                }
                seqNumList[0] = "New Scan";
                ddlScanBatches.DataSource = seqNumList;
                ddlScanBatches.DataBind();
                //Response.Redirect("~/Inventory_Module/StockAdjustment.aspx");  
            }
             
        }

        protected void ddlAdjType__SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlScanBatches.SelectedIndex != 0)
            {
                //ddlAdjType_.Enabled = false;
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Not allowed to change the direction!");
                if (ddlAdjType_.SelectedIndex == 0)
                    ddlAdjType_.SelectedIndex = 1;

                else if (ddlAdjType_.SelectedIndex == 1)
                    ddlAdjType_.SelectedIndex = 0;
                //txtSupplier.Focus();
                return;
            }
            else if (ddlScanBatches.SelectedIndex == 0)
            {
                btnSerialScan.Visible = true;
               // ddlAdjType_.Enabled = true;

                //ADDED BY SACHITH 2012/09/21
                //VIEW ADD COST BUTTON
                if (ddlAdjType_.SelectedIndex == 0)
                {
                   
                    ButtonAddCost.Visible = true;
                }
                else
                {
                    ButtonAddCost.Visible = false;
                }
                //END
            }
                
        }

        protected void gridShowAdjustedData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridShowAdjustedData.PageIndex = e.NewPageIndex;
            gridShowAdjustedData.DataSource = list_GetAllScanSerialsList;
            gridShowAdjustedData.DataBind();
        }

        protected void ddlAdjSubTyepe_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSubTpDesc.Text = CHNLSVC.Inventory.Get_Adj_SubTypes_description(ddlAdjSubTyepe.SelectedValue.Trim());
            if (ddlAdjSubTyepe.SelectedValue.Trim()=="")
            {
                lblSubTpDesc.Text = "";
            }
        }

        //ADDED BY SACHITH

        protected void DropDownListItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListStatus.Items.Clear();
            DropDownListStatus.Items.Add(new ListItem("", "-1"));
            DropDownListStatus.DataSource = CHNLSVC.Inventory.GetTemSerStatus(GlbUserComCode, GlbUserDefLoca, DropDownListItemCode.SelectedValue,Convert.ToInt32(ddlScanBatches.SelectedValue));
            DropDownListStatus.DataTextField = "TUS_ITM_STUS";
            DropDownListStatus.DataValueField = "TUS_ITM_STUS";
            DropDownListStatus.DataBind();
            DropDownListStatus_SelectedIndexChanged(null, null);

        }

        protected void DropDownListStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewPopUp.DataSource = CHNLSVC.Inventory.GetTemSerByCodeStatus(GlbUserComCode, GlbUserDefLoca, DropDownListItemCode.SelectedValue, DropDownListStatus.SelectedValue,Convert.ToInt32( ddlScanBatches.SelectedValue));
            GridViewPopUp.DataBind();
            if (HiddenFieldCusCrePopUpStats.Value == "1")
                ModalPopupExtender1.Show();
        }

        protected void ButtonApply_Click(object sender, EventArgs e)
        {
            int result = 0;
            for (int i = 0; i < GridViewPopUp.Rows.Count; i++) {
                GridViewRow gvRow= GridViewPopUp.Rows[i];
                bool _check = ((CheckBox)(gvRow.FindControl("CheckBoxGridSelect"))).Checked;
                if (_check)
                {
                    if (TextBoxUnitCost.Text == "")
                    {
                        string Msg = "<script>alert('Please enter unit cost');</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                        ModalPopupExtender1.Show();
                    }
                    else
                    {
                        string _item = GridViewPopUp.Rows[i].Cells[1].Text;
                        string _itemStatus = GridViewPopUp.Rows[i].Cells[2].Text;
                        string _itemSerial = GridViewPopUp.Rows[i].Cells[3].Text;
                        result = CHNLSVC.Inventory.UpdateUnitCost(GlbUserComCode, GlbUserDefLoca, _item, _itemStatus, _itemSerial, Convert.ToDecimal(TextBoxUnitCost.Text),Convert.ToInt32( ddlScanBatches.SelectedValue));
                    }
                }
            }
            if (result > 0)
            {
                ModalPopupExtender1.Hide();
                GridViewPopUp.DataSource = CHNLSVC.Inventory.GetTemSerByCodeStatus(GlbUserComCode, GlbUserDefLoca, DropDownListItemCode.SelectedValue, DropDownListStatus.SelectedValue, Convert.ToInt32(ddlScanBatches.SelectedValue));
                GridViewPopUp.DataBind();
                string Msg = "<script>alert('Records updated Sucessfully');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            else {
                //string Msg = "<script>alert('Nothing Updated');</script>";
                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            TextBoxUnitCost.Text = "";
            HiddenFieldCusCrePopUpStats.Value = "0";
        }

        //END


        //protected void Button2_Click(object sender, EventArgs e)
        //{
        //    int generated_seq = CHNLSVC.Inventory.Generate_new_seq_num("ADMIN", "ADJ", 1, GlbUserComCode);

        //}







    }
}
