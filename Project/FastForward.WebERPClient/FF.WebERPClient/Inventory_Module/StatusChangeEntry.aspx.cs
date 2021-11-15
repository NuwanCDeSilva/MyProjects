using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Data;
using System.Globalization;
using System.Text;

namespace FF.WebERPClient
{
    public partial class WebForm4 : BasePage
    {
        static List<ReptPickSerialsSub> list_ReptPickSerialsSubList = new List<ReptPickSerialsSub>();
        static List<ReptPickSerials> list_GetAllScanSerialsList = new List<ReptPickSerials>();
        static List<string> seqNumList = new List<string>();
        static List<string> seqNumList_out = new List<string>();
        static List<string> status_list = new List<string>();
        static List<ReptPickSerials> reptPickSerials_list = new List<ReptPickSerials>();//new items added to grid
        bool isManualscan;
        static int generated_seq=-1;

       static List<ReptPickSerials> serial_list = new List<ReptPickSerials>();//to store get popup serch result
        
        static  List<ReptPickSerials> list_NEWGetAllScanSerialsList = new List<ReptPickSerials>();
          

        protected void Page_Load(object sender, EventArgs e)
        {
           // txtItemCD.Attributes.Add("onKeyup", "return clickButton(event,'" + txtItemCD.ClientID + "')");
            //txtItemDesc.Text = "";

            if(!IsPostBack)
            {
               Panel_itemDesc.Enabled = true;
                isManualscan = true;
                panelDisposeAll.Visible = false;

                //generated_seq =-1;
                seqNumList_out = CHNLSVC.Inventory.GetAll_Adj_SeqNums_for_user(GlbUserName, "ADJ-S", 0, GlbUserComCode);//for OUT(status changes comes as outs)
                
                //-----added on 29-5-2012------------------------------------
                //List<Int32> seqNumList_out2 = new List<Int32>();
                //foreach (string sn in seqNumList_out)
                //{
                //    Int32 num = Convert.ToInt32(sn);
                //    seqNumList_out2.Add(num);
                //}
                
                //seqNumList_out2 = (from num in seqNumList_out2
                //                  orderby num  //ascending
                //                  select num).ToList();

                
                //foreach (Int32 sn in seqNumList_out2)
                //{
                //    string num = Convert.ToString(sn);
                //    seqNumList_out.Add(num);
                //}

                seqNumList_out = (from num in seqNumList_out
                                   orderby num  //ascending
                                   select num).ToList();
                //------------------------------------------------------------
                seqNumList_out[0] = "New Scan";
                ddlScanBatches.DataSource = seqNumList_out;
                ddlScanBatches.DataBind();

              //  List<string> itemcode_list = new List<string>();
                //  itemcode_list = CHNLSVC.Inventory.Get_all_Itemcodes(); //commented on 26-6-2012
                //  itemcode_list[0] = "--select--"; //commented on 26-6-2012
              //  ddlItemCodes.DataSource = itemcode_list;
              //  ddlItemCodes.DataBind();

                List<string> bincode_list = new List<string>();
                //bincode_list.Add("--select--");
                bincode_list = CHNLSVC.Inventory.GetAll_binCodes_for_loc(GlbUserComCode, GlbUserDefLoca);
               
                //bincode_list[0] = "select";
                
                ddlBinCode.DataSource = bincode_list;
                ddlBinCode.DataBind();

                Dictionary<string, string> status_list = CHNLSVC.Inventory.Get_all_ItemSatus();
                status_list.Add("--select--", "--select--");
                ddlItemStatus.DataSource = status_list.Keys;
                ddlItemStatus.DataBind();
                ddlItemStatus.SelectedValue = "--select--";
                //status_list.Insert(index, key); 
               // status_list=CHNLSVC.Inventory.Get_all_ItemSatus();
                //   List<string> status_codes = status_list[Keys];

                ddlStatusNew.DataSource = status_list.Keys;
                ddlStatusNew.DataBind();
                ddlStatusNew.SelectedValue = "--select--";

                List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                emptyGridList.Add(new ReptPickSerials());
                GridViewChanged_items.DataSource = emptyGridList;
                GridViewChanged_items.DataBind();

                //DateTime thisDate = DateTime.Now;
                //DateTime date = new DateTime(thisDate.Year, thisDate.Month, thisDate.Day);
                //CultureInfo culture = new CultureInfo("pt-BR");
                //Console.WriteLine(thisDate.ToString("d", culture));
                //txtDate.Text = thisDate.ToString("d", culture);
                //Edit Chamal 15-11-2012
                txtDate.Text = DateTime.Now.ToString("dd/MMM/yyyy"); 
                //try {
                //    MasterItem msitem = new MasterItem();
                //    msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, ddlItemCodes.SelectedValue.ToString());
                //    lblItemCD.Text = msitem.Mi_longdesc;
                //}
                //catch(Exception ex){
                //    lblItemCD.Text = "";
                //}
                //BindJavaScripts();


                //Check back date (Add Chamal 14-11-2012)
                IsAllowBackDateForModule(GlbUserComCode, GlbUserDefLoca, string.Empty, Page, string.Empty, imgRequestDate, lblDispalyInfor);


            }
        }

        protected void btnBatchPick_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {
         
        }

        protected void btnAllDispose_Click(object sender, EventArgs e)
        {
            panelDisposeAll.Visible = true;
            lbl_Location.Text = GlbUserDefLoca;
            //call all dispose method.
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            generated_seq = -1;
            Response.Redirect("~/Inventory_Module/StatusChangeEntry.aspx");
        }

        protected void btnDisposeOk_Click(object sender, EventArgs e)
        {
            //delete all from 
            //
        }
        protected void  btnDisposeCancel_Click(object sender, EventArgs e)
        {
            panelDisposeAll.Visible = false;
        }

        protected void ddlScanBatches_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get all scan batches for selected batch and load to grid.
           // int seq_num = int.Parse(ddlScanBatches.SelectedValue);
            if (ddlScanBatches.SelectedIndex == 0)
            {
                generated_seq = -1;
                isManualscan = true;
                btnScanPick.Enabled = true;
                Panel_itemDesc.Enabled = true;
                btnScanPick.Text = "Scan";
                //show empty line
                List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                emptyGridList.Add(new ReptPickSerials());
                GridViewChanged_items.DataSource = emptyGridList;
                GridViewChanged_items.DataBind();
                lblSeqno.Text = "";
               // GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, -1, "ADJ");
               // GridViewChanged_items.DataBind();
                
               // return;
                Response.Redirect("~/Inventory_Module/StatusChangeEntry.aspx");
            }
            else if (ddlScanBatches.SelectedIndex != 0)
            {   //if the user wants to view scanned batch in grid
                Panel_itemDesc.Enabled = true;
                btnScanPick.Text = "Pick";
                isManualscan = false;
                btnScanPick.Enabled = false;
                int seq_num = int.Parse(ddlScanBatches.SelectedValue);
            
                //to view in grid--------
      /*          List<ReptPickSerials> AllserList = new List<ReptPickSerials>();
                AllserList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, seq_num, "ADJ");
                List<ReptPickSerials> finalserList = new List<ReptPickSerials>();
                if (finalserList.Count == 0) {
                    GridViewChanged_items.DataSource = finalserList;
                    // GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ");
                    GridViewChanged_items.DataBind();
                }
                else if (finalserList.Count>0)
                {

                    foreach (ReptPickSerials rps in AllserList)
                    {
                        if (rps.Tus_ser_1 != "N/A")
                        {
                            finalserList.Add(rps);
                        }
                    }

                    GridViewChanged_items.DataSource = finalserList;
                    // GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ");
                    GridViewChanged_items.DataBind();
                }
                
          */      
                //----------------------
                //+++++++++++++++++
                //to view in grid
                List<ReptPickSerials> AllserList = new List<ReptPickSerials>();
                AllserList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, seq_num, "ADJ-S");
                List<ReptPickSerials> finalserList = new List<ReptPickSerials>();
                if (AllserList != null)
                {
                    foreach (ReptPickSerials rps in AllserList)
                    {
                        if (rps.Tus_ser_1 != "N/A")
                        {
                            finalserList.Add(rps);
                        }
                    }
                    //**********************************************************************************
                    List<ReptPickSerials> AllserList_Nonser = new List<ReptPickSerials>();
                    foreach (ReptPickSerials rps in AllserList)
                    {
                        if (rps.Tus_ser_1 == "N/A")
                        {
                            AllserList_Nonser.Add(rps);
                        }
                    }
                    var _tbitems = from _pickSerials in AllserList_Nonser
                                   group _pickSerials by new { _pickSerials.Tus_ser_1, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_bin, _pickSerials.Tus_itm_stus, _pickSerials.Tus_new_status, _pickSerials.Tus_new_remarks } into itm
                                   select new { Tus_ser_1 = itm.Key.Tus_ser_1, Tus_itm_model=itm.Key.Tus_itm_model, Tus_itm_cd = itm.Key.Tus_itm_cd, itm.Key.Tus_itm_desc, itm.Key.Tus_bin, Tus_itm_stus = itm.Key.Tus_itm_stus, Tus_new_status = itm.Key.Tus_new_status, Tus_qty = itm.Sum(p => p.Tus_qty), Tus_new_remarks = itm.Key.Tus_new_remarks };

                    //  List<ReptPickSerials> _assignList = new List<ReptPickSerials>();

                    foreach (var _lists in _tbitems)
                    {
                        ReptPickSerials _one = new ReptPickSerials();
                        _one.Tus_ser_1 = _lists.Tus_ser_1;
                        _one.Tus_itm_cd = _lists.Tus_itm_cd;
                        _one.Tus_itm_stus = _lists.Tus_itm_stus;
                        _one.Tus_new_status = _lists.Tus_new_status;
                        _one.Tus_qty = _lists.Tus_qty;
                        _one.Tus_usrseq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
                        _one.Tus_new_remarks = _lists.Tus_new_remarks;

                        _one.Tus_itm_model = _lists.Tus_itm_model;
                        _one.Tus_bin = _lists.Tus_bin;
                        _one.Tus_itm_desc = _lists.Tus_itm_desc;
                        // _assignList.Add(_one);
                        finalserList.Add(_one);

                    }

                    //************************************************************************************ 
                    list_GetAllScanSerialsList = finalserList;
                    list_ReptPickSerialsSubList = CHNLSVC.Inventory.GetAllScanSubSerialsList(seq_num, "ADJ-S");
                    if (list_GetAllScanSerialsList != null)
                    {
                        list_NEWGetAllScanSerialsList = list_GetAllScanSerialsList;
                        GridViewChanged_items.DataSource = list_NEWGetAllScanSerialsList;
                        GridViewChanged_items.DataBind();
                    }
                    else
                    {
                        List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                        emptyGridList.Add(new ReptPickSerials());
                        GridViewChanged_items.DataSource = emptyGridList;
                        GridViewChanged_items.DataBind();
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No records found!");
                    }
                }
                else
                {
                    list_GetAllScanSerialsList = AllserList;
                    list_ReptPickSerialsSubList = CHNLSVC.Inventory.GetAllScanSubSerialsList(seq_num, "ADJ-S");
                    if (list_GetAllScanSerialsList != null)
                    {
                        list_NEWGetAllScanSerialsList = list_GetAllScanSerialsList;
                        GridViewChanged_items.DataSource = list_NEWGetAllScanSerialsList;
                        GridViewChanged_items.DataBind();
                    }
                    else
                    {
                        List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                        emptyGridList.Add(new ReptPickSerials());
                        GridViewChanged_items.DataSource = emptyGridList;
                        GridViewChanged_items.DataBind();
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No records found!");
                    }
                }
               // GridViewChanged_items.DataSource = finalserList;
                // GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ");
               // GridViewChanged_items.DataBind();
                //***********

                //+++++++++++++++++
               // list_GetAllScanSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, seq_num, "ADJ");
                

               
                
             //GridViewChanged_items.FindControl("ddlNewStatus").DataBind(); //.Cells[0].FindControl("SelectCheck");

            }
            lblNewStatus.Text = "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try 
            {
            //    DateTime date_ = Convert.ToDateTime(txtDate.Text);

            if (txtDate.Text=="" )//|| txtManualRefNo.Text=="")
            {
               // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please fill Date and Manual Ref");
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please fill Date.");
                return;
            }
            if (IsDate(txtDate.Text, DateTimeStyles.None) == false)
            {
                txtDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invald Date.");
                return;
            }
     
          // string serial_1= txtSerial1.Text;
          // string serial_2 = txtSerial2.Text;
          // string binCode = ddlBinCode.SelectedValue.ToString();
           //string itemCode = txtItemCode.Text.Trim();
           string itemStatus = ddlItemStatus.SelectedValue.ToString();
          //Decimal qty=Convert.ToDecimal(txtQty.Text);
         //string item_description = "";//lblDiscription.Text;

           DateTime date = Convert.ToDateTime(txtDate.Text);
           string Manual_ref = txtManualRefNo.Text;
           string Remarks = "";
           if (txtRemarks.Text != "")
           {
               Remarks = txtRemarks.Text;
           }
           
//            public Int16 InventoryStatusChange(InventoryHeader _inventoryMovementHeaderMinus, InventoryHeader _inventoryMovementHeaderPlus, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumberMinus, MasterAutoNumber _masterAutoNumberPlus);
           int seq_num = -1;
           if (generated_seq > 0)
           {
               seq_num = generated_seq;
           }
           else
           {
               if (ddlScanBatches.SelectedIndex==0)
               {
                   return;
               }
               seq_num = Convert.ToInt32(ddlScanBatches.SelectedValue);
           }


           if (IsAllowBackDateForModule(GlbUserComCode, GlbUserDefLoca, string.Empty, Page, txtDate.Text, imgRequestDate, lblDispalyInfor) == false)
           {
               if (Convert.ToDateTime(txtDate.Text).Date != DateTime.Now.Date)
               {
                   MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Back date not allow for selected date!");
                   return;
               }
           }

          //List<ReptPickSerialsSub> list_ReptPickSerialsSubList = new List<ReptPickSerialsSub>();
          // List<ReptPickSerials> list_GetAllScanSerialsList = new List<ReptPickSerials>();
           List<ReptPickSerials> _reptPickSerials = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, seq_num, "ADJ-S");

            foreach(ReptPickSerials rp in _reptPickSerials)
            {
                if(rp.Tus_new_status=="-"||rp.Tus_new_status=="")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please make sure whether status has been changed in all scanned items!");
                    return;
                }
                if (rp.Tus_new_status == rp.Tus_itm_stus)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Make sure New Status is not equal to the Current Status, in all scanned items!");
                    return;
                }
            }
           list_GetAllScanSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, seq_num, "ADJ-S");
           list_ReptPickSerialsSubList = CHNLSVC.Inventory.GetAllScanSubSerialsList(seq_num, "ADJ-S");
           
            //InventoryStatusChange(InventoryHeader _inventoryMovementHeaderMinus, InventoryHeader _inventoryMovementHeaderPlus, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumberMinus, MasterAutoNumber _masterAutoNumberPlus);
           InventoryHeader invHdr_min = new InventoryHeader();
           invHdr_min.Ith_com = GlbUserComCode;
           //  invHdr_min.Ith_sbu has been assigned later
           // invHdr_min.Ith_channel has been assigned later
           invHdr_min.Ith_loc = GlbUserDefLoca;
           invHdr_min.Ith_cate_tp = "STUS";
           invHdr_min.Ith_is_manual = false;
           invHdr_min.Ith_stus = "A";
           invHdr_min.Ith_cre_by = GlbUserName;
           invHdr_min.Ith_mod_by = GlbUserName;
           invHdr_min.Ith_mod_when =Convert.ToDateTime( txtDate.Text);
           invHdr_min.Ith_direct = false;
           invHdr_min.Ith_session_id = GlbUserSessionID;
           invHdr_min.Ith_manual_ref = txtManualRefNo.Text;
           invHdr_min.Ith_remarks = Remarks;

           invHdr_min.Ith_doc_year = Convert.ToDateTime(txtDate.Text).Year;
           invHdr_min.Ith_doc_date = Convert.ToDateTime(txtDate.Text);
           invHdr_min.Ith_doc_tp = "ADJ";
           invHdr_min.Ith_sub_tp = "STUS";
           invHdr_min.Ith_entry_tp = "STUS";
           
         
          InventoryHeader invHdr_plus = new InventoryHeader();
          
          DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(GlbUserComCode, GlbUserDefLoca);
          foreach (DataRow r in dt_location.Rows)
          {
              // Get the value of the wanted column and cast it to string
              string sbu = (string)r["ML_OPE_CD"];
              invHdr_plus.Ith_sbu = sbu;
              invHdr_min.Ith_sbu = sbu;
              string chennel = "";
              if (System.DBNull.Value != r["ML_CATE_2"])
              {
                  
                  chennel = (string)r["ML_CATE_2"];
              }
               
              invHdr_plus.Ith_channel = chennel;
              invHdr_min.Ith_channel = chennel;
          }
          invHdr_plus.Ith_loc = GlbUserDefLoca;
           invHdr_plus.Ith_com = GlbUserComCode;
           invHdr_plus.Ith_cate_tp = "STUS";
           invHdr_plus.Ith_is_manual = false;
           invHdr_plus.Ith_stus = "A";
           invHdr_plus.Ith_cre_by = GlbUserName;
           invHdr_plus.Ith_mod_by = GlbUserName;
           invHdr_plus.Ith_mod_when = Convert.ToDateTime(txtDate.Text);
           invHdr_plus.Ith_direct = true;
           invHdr_plus.Ith_session_id = GlbUserSessionID;
           invHdr_plus.Ith_manual_ref = txtManualRefNo.Text;
           invHdr_plus.Ith_remarks = Remarks;

           invHdr_plus.Ith_doc_year = Convert.ToDateTime(txtDate.Text).Year;
           invHdr_plus.Ith_doc_date = Convert.ToDateTime(txtDate.Text);
           invHdr_plus.Ith_doc_tp = "ADJ";
           invHdr_plus.Ith_sub_tp = "STUS";
           invHdr_plus.Ith_entry_tp = "STUS";
           //MasterAutoNumber masterAuto = new MasterAutoNumber();
           //masterAuto.Aut_cate_cd = GlbUserDefLoca;
           //masterAuto.Aut_cate_tp = "LOC";
           //masterAuto.Aut_direction = null;
           //masterAuto.Aut_modify_dt = null;
           //masterAuto.Aut_moduleid = "ADJ";
           //masterAuto.Aut_number = 5;//what is Aut_number
           //masterAuto.Aut_start_char = "ADJ";
           //masterAuto.Aut_year = null;
           ////  masterAuto.Aut_year = Convert.ToDateTime(txtDate_.Text).Year;


           MasterAutoNumber masterAuto_min = new MasterAutoNumber();
           masterAuto_min.Aut_cate_cd = GlbUserDefLoca;
           masterAuto_min.Aut_cate_tp = "LOC";
           masterAuto_min.Aut_direction =null;
           masterAuto_min.Aut_modify_dt = null;
           masterAuto_min.Aut_moduleid = "ADJ";
           masterAuto_min.Aut_number = 5;//what is Aut_number
           masterAuto_min.Aut_start_char = "ADJ";
           masterAuto_min.Aut_year = null;
           //masterAuto_min.Aut_modify_dt = null;//Convert.ToDateTime(txtDate.Text) ;
           //masterAuto_min.Aut_year = Convert.ToDateTime(txtDate.Text).Year;

           MasterAutoNumber masterAuto_plus = new MasterAutoNumber();
           masterAuto_plus.Aut_cate_cd = GlbUserDefLoca;
           masterAuto_plus.Aut_cate_tp = "LOC";
           masterAuto_plus.Aut_direction = null;
           masterAuto_plus.Aut_modify_dt = null;
           masterAuto_plus.Aut_moduleid = "ADJ";
           masterAuto_plus.Aut_number = 5;//what is Aut_number
           masterAuto_plus.Aut_start_char = "ADJ";
           masterAuto_plus.Aut_year = null;



           //masterAuto_plus.Aut_modify_dt = null;//Convert.ToDateTime(txtDate.Text);
          // masterAuto_plus.Aut_year = Convert.ToDateTime(txtDate.Text).Year;//Convert.ToDateTime(txtDate.Text).Year;

          
           string _minusDocNo = null; string _plusDocNo = null; //Add Chamal 15-05-2012
           // TODO --> use this two variables for dispaly to use system generated document nos of end this process

           Int16 affected = CHNLSVC.Inventory.InventoryStatusChange(invHdr_min, invHdr_plus, list_GetAllScanSerialsList, list_ReptPickSerialsSubList, masterAuto_min, masterAuto_plus, out _minusDocNo, out _plusDocNo);
           MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Saved! ADJ(-) Doc No. : " + _minusDocNo + ".      ADJ(+) Doc No. :" + _plusDocNo );

          

           generated_seq = -1;

           List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
           emptyGridList.Add(new ReptPickSerials());
           GridViewChanged_items.DataSource = emptyGridList;
           GridViewChanged_items.DataBind();

           seqNumList_out = CHNLSVC.Inventory.GetAll_Adj_SeqNums_for_user(GlbUserName, "ADJ-S", 0, GlbUserComCode);//for OUT(status changes comes as outs)

           seqNumList_out = (from num in seqNumList_out
                             orderby num  //ascending
                             select num).ToList();
          
           seqNumList_out[0] = "New Scan";
           ddlScanBatches.DataSource = seqNumList_out;
           ddlScanBatches.DataBind();
           clearscreen();


            //return;
            //string Msg = "<script>alert('Successfully Saved! ADJ(-) Document No. : " + _minusDocNo + ". ADJ(+) Document No. :" + _plusDocNo + "' );</script>";
           //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            catch(Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Critical, ex.Message);
                return;
            }
        }

        protected void btnScanPick_Click(object sender, EventArgs e)
        {
            ////go for new scan. It should be a ADJ(-)
            
            //isManualscan = false;
            //if (ddlScanBatches.SelectedIndex != 0)
            //{
            //    Panel_itemDesc.Enabled = false;
            //    return;
            //}
            //Panel_itemDesc.Enabled = true;
            // generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "ADJ", 1, GlbUserComCode);//direction always =1 for this method
             
            //ReptPickHeader RPH = new ReptPickHeader();
            //RPH.Tuh_doc_tp = "ADJ";
            //RPH.Tuh_cre_dt = DateTime.Today;//might change //Calendar-SelectedDate;
            //RPH.Tuh_ischek_itmstus = true;//might change 
            //RPH.Tuh_ischek_reqqty = true;//might change
            //RPH.Tuh_ischek_simitm = true;//might change
            //RPH.Tuh_session_id = GlbUserSessionID;
            //RPH.Tuh_usr_com = GlbUserComCode;//might change 
            //RPH.Tuh_usr_id = GlbUserName;
            //RPH.Tuh_usrseq_no = generated_seq;

            //RPH.Tuh_direct = false; //direction always (-) for change status

            ////write entry to TEMP_PICK_HDR
            //int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
            //seqNumList_out = CHNLSVC.Inventory.GetAll_Adj_SeqNums_for_user(GlbUserName, "ADJ", 0, GlbUserComCode);//for OUT(status changes comes as outs)
            //seqNumList_out[0] = "New Scan";
            //ddlScanBatches.DataSource = seqNumList_out;
            //ddlScanBatches.DataBind();
            //if (affected_rows > 0)
            //{
            //    lblSeqno.Text = generated_seq.ToString();
            //    //set session variables
            //    GlbSerialScanUserSeqNo = generated_seq;
            //    GlbSerialScanDocumentType = "ADJ";
            //    GlbSerialScanDirection = 0;
            //    GlbSerialScanReturnUrl = HttpContext.Current.Request.Url.AbsoluteUri;

            //    //finally redirect to scanSerial page
            //}    
               
        }

        protected void btnDeleteSelect_Click(object sender, EventArgs e)
        {
            //delete all cheked items from the list.(remove from grid)
            string s = "select list:";
            //List<ReptPickSerials> list_NEWGetAllScanSerialsList = new List<ReptPickSerials>();
            //list_NEWGetAllScanSerialsList = list_GetAllScanSerialsList;
            if (isManualscan == false)
            {
               // int i = 0;
                foreach (GridViewRow gvr in this.GridViewChanged_items.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("SelectCheck");
                    //Int32 usrseqNo = list_NEWGetAllScanSerialsList[i].Tus_usrseq_no;
                    Int32 usrseqNo = Convert.ToInt32(gvr.Cells[11].Text);
                    // Int32 ser_id = list_NEWGetAllScanSerialsList[i].Tus_ser_id;
                   
                    Int32 ser_id ;//= Convert.ToInt32(gvr.Cells[12].Text);
                    
                    Label lbl_serID   =    (Label)gvr.FindControl("ser_id");
                    ser_id = Convert.ToInt32(lbl_serID.Text.Trim());
                    
                    //string serial_1 = list_NEWGetAllScanSerialsList[i].Tus_ser_1;
                    string serial_1 = gvr.Cells[1].Text;
                    // string itemCode = list_NEWGetAllScanSerialsList[i].Tus_itm_cd;
                    string itemCode = gvr.Cells[3].Text;
                    string bincode=gvr.Cells[4].Text;
                    
                    string current_status=gvr.Cells[7].Text;
                    string new_status=gvr.Cells[9].Text;
                    if (new_status.Trim() == "")
                    { new_status = ""; } 

                    if (chkSelect.Checked)
                    {
                        //if (serial_1 == "_")
                        if (serial_1 == "N/A")
                        {
                            //+
                            List<ReptPickSerials> NA_list = new List<ReptPickSerials>();
                            Int32 user_seq_no=Convert.ToInt32(ddlScanBatches.SelectedValue);
                            NA_list = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, user_seq_no, "ADJ-S");
                            //+++++++++++++++++++++++++++
                            List<ReptPickSerials> AllserList_Nonser_del = new List<ReptPickSerials>();
                            foreach (ReptPickSerials rps in NA_list)
                            {
                                if (rps.Tus_ser_1 == "N/A")
                                {
                                    AllserList_Nonser_del.Add(rps);
                                }
                            }
                            var _tbitems_del = from _pickSerials in AllserList_Nonser_del
                                               group _pickSerials by new { _pickSerials.Tus_ser_1, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_bin, _pickSerials.Tus_itm_stus, _pickSerials.Tus_new_status, _pickSerials.Tus_ser_id } into itm
                                               select new { Tus_ser_1 = itm.Key.Tus_ser_1, Tus_itm_model = itm.Key.Tus_itm_model, Tus_itm_cd = itm.Key.Tus_itm_cd, itm.Key.Tus_itm_desc, itm.Key.Tus_bin, Tus_itm_stus = itm.Key.Tus_itm_stus, Tus_new_status = itm.Key.Tus_new_status, Tus_ser_id = itm.Key.Tus_ser_id };

                            List<Int32> serIDs_ = new List<Int32>();
                            foreach (var _lists in _tbitems_del)
                            {
                                if (itemCode == _lists.Tus_itm_cd && bincode == _lists.Tus_bin && current_status == _lists.Tus_itm_stus && new_status == _lists.Tus_new_status)
                                {
                                    serIDs_.Add(_lists.Tus_ser_id);
                                }
                                    

                            }

                            if (serIDs_ != null)
                            {
                                foreach (Int32 serid in serIDs_)
                                {
                                   
                                       
                                        Boolean update_av = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serid, 1);
                                        // Boolean update = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serial_1, 1);
                                        Boolean affec = CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, user_seq_no, serid);
                                     
                                }
                            }
                            //++++++++++++++++++++++++++
                            #region
                            //commented after non-ser modifications
                          //  if (NA_list != null)
                          //  {
                          //      foreach (ReptPickSerials r in NA_list)
                          //      {
                          //          if (r.Tus_bin == bincode && r.Tus_usrseq_no == usrseqNo && r.Tus_ser_1 == "N/A" && r.Tus_itm_cd == itemCode)
                          //          {
                          //              Int32 serialID = r.Tus_ser_id;
                          //              Boolean update_av = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serialID, 1);
                          //              // Boolean update = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serial_1, 1);
                          //             // Boolean affected = CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, user_seq_no, ser_id);
                          //          }
                          //      }
                          //  }
                          //  //+
                          
                          ////  Boolean affectedrows = CHNLSVC.Inventory.Del_temp_pick_serdummy(GlbUserComCode, GlbUserDefLoca, usrseqNo, ser_id, itemCode, bincode);
                          //  Boolean affectedrows = CHNLSVC.Inventory.Del_temp_pick_serdummy(GlbUserComCode, GlbUserDefLoca, user_seq_no, ser_id, itemCode, bincode);
                          // // Boolean affected = CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, usrseqNo, ser_id);//removes the dummy
                          //    Boolean affected = CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, user_seq_no, ser_id);
                            #endregion
                        }
                        else
                        {
                            //remove from TEMP_PICK_SER
                            Boolean affected = CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, usrseqNo, ser_id);
                            Boolean update = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serial_1, 1);
                        }
                       
                       
                    }
                   // GridViewChanged_items.DataSource = list_GetAllScanSerialsList;// CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, usrseqNo, "ADJ");
                    //++++++++++++++++++++
                    //to view in grid
                    Int32 user_seqNo = Convert.ToInt32(ddlScanBatches.SelectedValue);
                    List<ReptPickSerials> AllserList = new List<ReptPickSerials>();
                    AllserList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, usrseqNo, "ADJ-S");
                    List<ReptPickSerials> finalserList = new List<ReptPickSerials>();
                    if (AllserList != null)
                    {
                        foreach (ReptPickSerials rps in AllserList)
                        {
                            if (rps.Tus_ser_1 != "N/A")
                            {
                                finalserList.Add(rps);
                            }
                        }
                        //**********************************************************************************
                        List<ReptPickSerials> AllserList_Nonser = new List<ReptPickSerials>();
                        foreach (ReptPickSerials rps in AllserList)
                        {
                            if (rps.Tus_ser_1 == "N/A")
                            {
                                AllserList_Nonser.Add(rps);
                            }
                        }
                        var _tbitems = from _pickSerials in AllserList_Nonser
                                       group _pickSerials by new { _pickSerials.Tus_ser_1, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_bin, _pickSerials.Tus_itm_stus, _pickSerials.Tus_new_status, _pickSerials.Tus_new_remarks } into itm
                                       select new { Tus_ser_1 = itm.Key.Tus_ser_1, Tus_itm_model = itm.Key.Tus_itm_model, Tus_itm_cd = itm.Key.Tus_itm_cd, itm.Key.Tus_itm_desc, itm.Key.Tus_bin, Tus_itm_stus = itm.Key.Tus_itm_stus, Tus_new_status = itm.Key.Tus_new_status, Tus_qty = itm.Sum(p => p.Tus_qty), Tus_new_remarks = itm.Key.Tus_new_remarks };

                        //  List<ReptPickSerials> _assignList = new List<ReptPickSerials>();

                        foreach (var _lists in _tbitems)
                        {
                            ReptPickSerials _one = new ReptPickSerials();
                            _one.Tus_ser_1 = _lists.Tus_ser_1;
                            _one.Tus_itm_cd = _lists.Tus_itm_cd;
                            _one.Tus_itm_stus = _lists.Tus_itm_stus;
                            _one.Tus_new_status = _lists.Tus_new_status;
                            _one.Tus_qty = _lists.Tus_qty;
                            _one.Tus_usrseq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
                            _one.Tus_new_remarks = _lists.Tus_new_remarks;

                            _one.Tus_itm_model = _lists.Tus_itm_model;
                            _one.Tus_bin = _lists.Tus_bin;
                            _one.Tus_itm_desc = _lists.Tus_itm_desc;
                            // _assignList.Add(_one);
                            finalserList.Add(_one);

                        }

                        //************************************************************************************ 
                        GridViewChanged_items.DataSource = finalserList;
                        // GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ");
                        GridViewChanged_items.DataBind();
                    }
                    else
                    {
                        List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                        emptyGridList.Add(new ReptPickSerials());
                        GridViewChanged_items.DataSource = emptyGridList;
                        GridViewChanged_items.DataBind();
                        //GridViewChanged_items.DataSource = AllserList;
                        // GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ");
                      //  GridViewChanged_items.DataBind();
                    }
                    //+++++++++++++++++++++
                    
                }
               
            }
            else if (isManualscan == true)
            {
               // int i = 0;
                foreach (GridViewRow gvr in this.GridViewChanged_items.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("SelectCheck");
                    Int32 usrseqNo = Convert.ToInt32(gvr.Cells[11].Text);

                    //Int32 ser_id = Convert.ToInt32(gvr.Cells[12].Text);
                    Int32 ser_id;//= Convert.ToInt32(gvr.Cells[12].Text);

                    Label lbl_serID = (Label)gvr.FindControl("ser_id");
                    ser_id = Convert.ToInt32(lbl_serID.Text.Trim());
                    string bincode=(gvr.Cells[4].Text);
                    string serial_1 = gvr.Cells[1].Text;

                    string itemCode = gvr.Cells[3].Text;

                    string current_status = gvr.Cells[7].Text;
                    string new_status = gvr.Cells[9].Text;

                    if (chkSelect.Checked )
                    {
                        //Int32 usrseqNo= reptPickSerials_list[i].Tus_usrseq_no;
                        //Int32 ser_id=reptPickSerials_list[i].Tus_ser_id;
                        //string serial_1=reptPickSerials_list[i].Tus_ser_1;
                        //string itemCode=reptPickSerials_list[i].Tus_itm_cd;
                        if (serial_1 == "N/A")
                        {
                            //update in inr_ser
                           //+
                            List<ReptPickSerials> NA_list = new List<ReptPickSerials>();
                            Int32 user_seq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
                            NA_list = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, user_seq_no, "ADJ-S");
                            //+++++++++++++++++++++++++++
                            List<ReptPickSerials> AllserList_Nonser_del = new List<ReptPickSerials>();
                            foreach (ReptPickSerials rps in NA_list)
                            {
                                if (rps.Tus_ser_1 == "N/A")
                                {
                                    AllserList_Nonser_del.Add(rps);
                                }
                            }
                            var _tbitems_del = from _pickSerials in AllserList_Nonser_del
                                               group _pickSerials by new { _pickSerials.Tus_ser_1, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_bin, _pickSerials.Tus_itm_stus, _pickSerials.Tus_new_status, _pickSerials.Tus_ser_id } into itm
                                               select new { Tus_ser_1 = itm.Key.Tus_ser_1, Tus_itm_model = itm.Key.Tus_itm_model, Tus_itm_cd = itm.Key.Tus_itm_cd, itm.Key.Tus_itm_desc, itm.Key.Tus_bin, Tus_itm_stus = itm.Key.Tus_itm_stus, Tus_new_status = itm.Key.Tus_new_status, Tus_ser_id = itm.Key.Tus_ser_id };

                            List<Int32> serIDs_ = new List<Int32>();
                            foreach (var _lists in _tbitems_del)
                            {
                                if (itemCode == _lists.Tus_itm_cd && bincode == _lists.Tus_bin && current_status == _lists.Tus_itm_stus && new_status == _lists.Tus_new_status)
                                {
                                    serIDs_.Add(_lists.Tus_ser_id);
                                }

                            }

                            if (serIDs_ != null)
                            {
                                foreach (Int32 serid in serIDs_)
                                {


                                    Boolean update_av = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serid, 1);
                                    // Boolean update = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serial_1, 1);
                                    Boolean affec = CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, user_seq_no, serid);

                                }
                            }
                        }
                        else   
                        { 
                            //remove from TEMP_PICK_SER
                            Boolean affected = CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, usrseqNo, ser_id);
                            Boolean update = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serial_1, 1);
                        }
                       
                       //Update_inrser_INS_AVAILABLE

                    }
                    //GridViewChanged_items.DataSource = reptPickSerials_list;
                    //++++++++++++++++
                    //to view in grid
                    Int32 user_seqNo = Convert.ToInt32(ddlScanBatches.SelectedValue);
                    List<ReptPickSerials> AllserList = new List<ReptPickSerials>();
                    AllserList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, user_seqNo, "ADJ-S");
                    List<ReptPickSerials> finalserList = new List<ReptPickSerials>();
                    if (AllserList != null)
                    {
                        foreach (ReptPickSerials rps in AllserList)
                        {
                            if (rps.Tus_ser_1 != "N/A")
                            {
                                finalserList.Add(rps);
                            }
                        }
                        //**********************************************************************************
                        List<ReptPickSerials> AllserList_Nonser = new List<ReptPickSerials>();
                        foreach (ReptPickSerials rps in AllserList)
                        {
                            if (rps.Tus_ser_1 == "N/A")
                            {
                                AllserList_Nonser.Add(rps);
                            }
                        }
                        var _tbitems = from _pickSerials in AllserList_Nonser
                                       group _pickSerials by new { _pickSerials.Tus_ser_1, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_bin, _pickSerials.Tus_itm_stus, _pickSerials.Tus_new_status, _pickSerials.Tus_new_remarks } into itm
                                       select new { Tus_ser_1 = itm.Key.Tus_ser_1, Tus_itm_model = itm.Key.Tus_itm_model, Tus_itm_cd = itm.Key.Tus_itm_cd, itm.Key.Tus_itm_desc, itm.Key.Tus_bin, Tus_itm_stus = itm.Key.Tus_itm_stus, Tus_new_status = itm.Key.Tus_new_status, Tus_qty = itm.Sum(p => p.Tus_qty), Tus_new_remarks = itm.Key.Tus_new_remarks };

                        //  List<ReptPickSerials> _assignList = new List<ReptPickSerials>();

                        foreach (var _lists in _tbitems)
                        {
                            ReptPickSerials _one = new ReptPickSerials();
                            _one.Tus_ser_1 = _lists.Tus_ser_1;
                            _one.Tus_itm_cd = _lists.Tus_itm_cd;
                            _one.Tus_itm_stus = _lists.Tus_itm_stus;
                            _one.Tus_new_status = _lists.Tus_new_status;
                            _one.Tus_qty = _lists.Tus_qty;
                            _one.Tus_usrseq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
                            _one.Tus_new_remarks = _lists.Tus_new_remarks;

                            _one.Tus_itm_model = _lists.Tus_itm_model;
                            _one.Tus_bin = _lists.Tus_bin;
                            _one.Tus_itm_desc = _lists.Tus_itm_desc;
                            // _assignList.Add(_one);
                            finalserList.Add(_one);

                        }

                        //************************************************************************************ 
                        GridViewChanged_items.DataSource = finalserList;
                        // GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ");
                        GridViewChanged_items.DataBind();
                    }
                    else
                    {
                        List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                        emptyGridList.Add(new ReptPickSerials());
                        GridViewChanged_items.DataSource = emptyGridList;
                        GridViewChanged_items.DataBind();
                        //GridViewChanged_items.DataSource = AllserList;
                        // GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ");
                        //  GridViewChanged_items.DataBind();
                    }
                    //+++++++++++++++++
                  //  GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, usrseqNo, "ADJ");
                  //  GridViewChanged_items.DataBind();
                   // i++;
                }
              
            }
        }

        



        protected void btnUpdateSelect_Click(object sender, EventArgs e)
        {
            if (ddlStatusNew.SelectedValue == "--select--")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select new status");
                return;
            }
            if (ddlStatusNew.SelectedValue == "CONS")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot change status to Consignment. Select a different status!");
                ddlStatusNew.SelectedValue = "--select--";
                return;
            }

           string newStat = ddlStatusNew.SelectedValue;
           //get the serial id AND new Remarks into the dictionary
           Dictionary<string,string> changed_serials=new Dictionary<string,string>();
           // int i=0;
           foreach (GridViewRow gvr in this.GridViewChanged_items.Rows)
           {
               CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("SelectCheck");
               Int32 usrseqNo = Convert.ToInt32(gvr.Cells[11].Text);

               Int32 ser_id;//= Convert.ToInt32(gvr.Cells[12].Text);

               Label lbl_serID = (Label)gvr.FindControl("ser_id");
               ser_id = Convert.ToInt32(lbl_serID.Text.Trim());

               string serial_1 = gvr.Cells[1].Text;

               string itemCode = gvr.Cells[3].Text;
               string current_status = gvr.Cells[7].Text;
               string new_status = gvr.Cells[9].Text;



               if (chkSelect.Checked)
               {
                   if (current_status == ddlStatusNew.SelectedValue)
                   {
                       MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "For all selected items, the New status should be different form the Current status!");
                       return;
                   }
               }
           }

           foreach (GridViewRow gvr in this.GridViewChanged_items.Rows)
           {
               CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("SelectCheck");
               Int32 usrseqNo = Convert.ToInt32(gvr.Cells[11].Text);

               //Int32 ser_id = Convert.ToInt32(gvr.Cells[12].Text);
               Int32 ser_id;//= Convert.ToInt32(gvr.Cells[12].Text);

               Label lbl_serID = (Label)gvr.FindControl("ser_id");
               ser_id = Convert.ToInt32(lbl_serID.Text.Trim());
               string bincode = (gvr.Cells[4].Text);
               string serial_1 = gvr.Cells[1].Text;

               string itemCode = gvr.Cells[3].Text;
               string current_status = gvr.Cells[7].Text;
               string new_status = gvr.Cells[9].Text;

               

               if (chkSelect.Checked&& isManualscan==false)
               {//------
                   //list_NEWGetAllScanSerialsList[i].Tus_itm_stus = ddlStatusNew.SelectedValue;//change the new status

                   TextBox newRemarksBox = (TextBox)gvr.Cells[8].FindControl("txtNewRemark");//new remark for the serial id
                  
                  

                  //call update method
                   Boolean affected = CHNLSVC.Inventory.Update_TEMP_PICK_SER(GlbUserComCode, GlbUserDefLoca, usrseqNo, ser_id, ddlStatusNew.SelectedValue, newRemarksBox.Text);
               
                   GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, usrseqNo, "ADJ-S");
                   GridViewChanged_items.DataBind();

                   if (serial_1=="N/A")//change the status if it is non serial 
                   {
                       List<ReptPickSerials> AllserList_Nonser = new List<ReptPickSerials>();
                       AllserList_Nonser = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, usrseqNo, "ADJ-S");

                       List<ReptPickSerials> to_del_list = new List<ReptPickSerials>();
                       foreach (ReptPickSerials rps in AllserList_Nonser)
                       {
                           if (rps.Tus_ser_1 == "N/A")
                           {
                               to_del_list.Add(rps);
                           }
                       }

                       var _tbitems_chn = from _pickSerials in to_del_list
                                          group _pickSerials by new { _pickSerials.Tus_ser_1, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_bin, _pickSerials.Tus_itm_stus, _pickSerials.Tus_new_status, _pickSerials.Tus_ser_id } into itm
                                          select new { Tus_ser_1 = itm.Key.Tus_ser_1, Tus_itm_model = itm.Key.Tus_itm_model, Tus_itm_cd = itm.Key.Tus_itm_cd, itm.Key.Tus_itm_desc, itm.Key.Tus_bin, Tus_itm_stus = itm.Key.Tus_itm_stus, Tus_new_status = itm.Key.Tus_new_status, Tus_ser_id = itm.Key.Tus_ser_id };

                       List<Int32> serIDs_ = new List<Int32>();
                       foreach (var _lists in _tbitems_chn)
                       {
                           if (itemCode == _lists.Tus_itm_cd && bincode==_lists.Tus_bin && current_status == _lists.Tus_itm_stus && new_status == _lists.Tus_new_status)
                           {
                               serIDs_.Add(_lists.Tus_ser_id);
                               
                           }

                       }
                       foreach (Int32 ser_ in serIDs_)
                       {
                           Boolean aff = CHNLSVC.Inventory.Update_TEMP_PICK_SER(GlbUserComCode, GlbUserDefLoca, usrseqNo, ser_, ddlStatusNew.SelectedValue, newRemarksBox.Text);
                       }


                   }

                   ////////////////////////////////////////////////////added on***************************************
                   //to view in grid
                   List<ReptPickSerials> AllserList = new List<ReptPickSerials>();
                   AllserList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, usrseqNo, "ADJ-S");
                   List<ReptPickSerials> finalserList = new List<ReptPickSerials>();
                   if (AllserList != null)
                   {
                       foreach (ReptPickSerials rps in AllserList)
                       {
                           if (rps.Tus_ser_1 != "N/A")
                           {
                               finalserList.Add(rps);
                           }
                       }
                       //**********************************************************************************
                       List<ReptPickSerials> AllserList_Nonser = new List<ReptPickSerials>();
                       foreach (ReptPickSerials rps in AllserList)
                       {
                           if (rps.Tus_ser_1 == "N/A")
                           {
                               AllserList_Nonser.Add(rps);
                           }
                       }
                       var _tbitems = from _pickSerials in AllserList_Nonser
                                      group _pickSerials by new { _pickSerials.Tus_ser_1, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_bin, _pickSerials.Tus_itm_stus, _pickSerials.Tus_new_status, _pickSerials.Tus_new_remarks } into itm
                                      select new { Tus_ser_1 = itm.Key.Tus_ser_1, Tus_itm_model = itm.Key.Tus_itm_model, Tus_itm_cd = itm.Key.Tus_itm_cd, itm.Key.Tus_itm_desc, itm.Key.Tus_bin, Tus_itm_stus = itm.Key.Tus_itm_stus, Tus_new_status = itm.Key.Tus_new_status, Tus_qty = itm.Sum(p => p.Tus_qty), Tus_new_remarks = itm.Key.Tus_new_remarks };

                       //  List<ReptPickSerials> _assignList = new List<ReptPickSerials>();

                       foreach (var _lists in _tbitems)
                       {
                           ReptPickSerials _one = new ReptPickSerials();
                           _one.Tus_ser_1 = _lists.Tus_ser_1;
                           _one.Tus_itm_cd = _lists.Tus_itm_cd;
                           _one.Tus_itm_stus = _lists.Tus_itm_stus;
                           _one.Tus_new_status = _lists.Tus_new_status;
                           _one.Tus_qty = _lists.Tus_qty;
                           _one.Tus_usrseq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
                           _one.Tus_new_remarks = _lists.Tus_new_remarks;

                           _one.Tus_itm_model = _lists.Tus_itm_model;
                           _one.Tus_bin = _lists.Tus_bin;
                           _one.Tus_itm_desc = _lists.Tus_itm_desc;
                         
                           // _assignList.Add(_one);
                           finalserList.Add(_one);

                       }

                       //************************************************************************************ 
                       GridViewChanged_items.DataSource = finalserList;
                       // GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ");
                       GridViewChanged_items.DataBind();
                       //***********
                   }
                   else
                   {
                       List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                       emptyGridList.Add(new ReptPickSerials());
                       GridViewChanged_items.DataSource = emptyGridList;
                       GridViewChanged_items.DataBind();

                       // GridViewChanged_items.DataSource = AllserList;
                       // // GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ");
                       // GridViewChanged_items.DataBind();
                   }
                   ////////////////////////////////////////////////////added on***************************************
               }
              else if (chkSelect.Checked && isManualscan==true)
               {//------
                   TextBox newRemarksBox = (TextBox)gvr.Cells[8].FindControl("txtNewRemark");//new remark for the serial id
                   //call update method
                   Boolean affected = CHNLSVC.Inventory.Update_TEMP_PICK_SER(GlbUserComCode, GlbUserDefLoca, usrseqNo, ser_id, ddlStatusNew.SelectedValue, newRemarksBox.Text);

                   GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, usrseqNo, "ADJ-S");
                   GridViewChanged_items.DataBind();

                   if (serial_1 == "N/A")//change the status if it is non serial 
                   {
                       List<ReptPickSerials> AllserList_Nonser = new List<ReptPickSerials>();
                       AllserList_Nonser = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, usrseqNo, "ADJ-S");

                       List<ReptPickSerials> to_del_list = new List<ReptPickSerials>();
                       foreach (ReptPickSerials rps in AllserList_Nonser)
                       {
                           if (rps.Tus_ser_1 == "N/A")
                           {
                               to_del_list.Add(rps);
                           }
                       }

                       var _tbitems_chn = from _pickSerials in to_del_list
                                          group _pickSerials by new { _pickSerials.Tus_ser_1, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_bin, _pickSerials.Tus_itm_stus, _pickSerials.Tus_new_status, _pickSerials.Tus_ser_id } into itm
                                          select new { Tus_ser_1 = itm.Key.Tus_ser_1, Tus_itm_model = itm.Key.Tus_itm_model, Tus_itm_cd = itm.Key.Tus_itm_cd, itm.Key.Tus_itm_desc, itm.Key.Tus_bin, Tus_itm_stus = itm.Key.Tus_itm_stus, Tus_new_status = itm.Key.Tus_new_status, Tus_ser_id = itm.Key.Tus_ser_id };

                       List<Int32> serIDs_ = new List<Int32>();
                       
                       foreach (var _lists in _tbitems_chn)
                       {
                           if (itemCode == _lists.Tus_itm_cd && bincode == _lists.Tus_bin && current_status == _lists.Tus_itm_stus && new_status == _lists.Tus_new_status)
                           {
                               //serIDs_.Add(_lists.Tus_ser_id);
                               //Boolean aff = CHNLSVC.Inventory.Update_TEMP_PICK_SER(GlbUserComCode, GlbUserDefLoca, usrseqNo, ser_id, ddlStatusNew.SelectedValue, newRemarksBox.Text);
                           }

                       }
                       foreach (Int32 ser_ in serIDs_)
                       {
                           Boolean aff = CHNLSVC.Inventory.Update_TEMP_PICK_SER(GlbUserComCode, GlbUserDefLoca, usrseqNo, ser_, ddlStatusNew.SelectedValue, newRemarksBox.Text);
                       }

                   }
                   ////////////////////////////////////////////////////added on***************************************
                   //to view in grid
                   List<ReptPickSerials> AllserList = new List<ReptPickSerials>();
                   AllserList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, usrseqNo, "ADJ-S");
                   List<ReptPickSerials> finalserList = new List<ReptPickSerials>();
                   if (AllserList != null)
                   {
                       foreach (ReptPickSerials rps in AllserList)
                       {
                           if (rps.Tus_ser_1 != "N/A")
                           {
                               finalserList.Add(rps);
                           }
                       }
                       //**********************************************************************************
                       List<ReptPickSerials> AllserList_Nonser = new List<ReptPickSerials>();
                       foreach (ReptPickSerials rps in AllserList)
                       {
                           if (rps.Tus_ser_1 == "N/A")
                           {
                               AllserList_Nonser.Add(rps);
                           }
                       }
                       var _tbitems = from _pickSerials in AllserList_Nonser
                                      group _pickSerials by new { _pickSerials.Tus_ser_1, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_bin, _pickSerials.Tus_itm_stus, _pickSerials.Tus_new_status, _pickSerials.Tus_new_remarks } into itm
                                      select new { Tus_ser_1 = itm.Key.Tus_ser_1, Tus_itm_model = itm.Key.Tus_itm_model, Tus_itm_cd = itm.Key.Tus_itm_cd, itm.Key.Tus_itm_desc, itm.Key.Tus_bin, Tus_itm_stus = itm.Key.Tus_itm_stus, Tus_new_status = itm.Key.Tus_new_status, Tus_qty = itm.Sum(p => p.Tus_qty), Tus_new_remarks = itm.Key.Tus_new_remarks };

                       //  List<ReptPickSerials> _assignList = new List<ReptPickSerials>();

                       foreach (var _lists in _tbitems)
                       {
                           ReptPickSerials _one = new ReptPickSerials();
                           _one.Tus_ser_1 = _lists.Tus_ser_1;
                           _one.Tus_itm_cd = _lists.Tus_itm_cd;
                           _one.Tus_itm_stus = _lists.Tus_itm_stus;
                           _one.Tus_new_status = _lists.Tus_new_status;
                           _one.Tus_qty = _lists.Tus_qty;
                           _one.Tus_usrseq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
                           _one.Tus_new_remarks = _lists.Tus_new_remarks;

                           _one.Tus_itm_model = _lists.Tus_itm_model;
                           _one.Tus_bin = _lists.Tus_bin;
                           _one.Tus_itm_desc = _lists.Tus_itm_desc;
                           // _assignList.Add(_one);
                           finalserList.Add(_one);

                       }

                       //************************************************************************************ 
                       GridViewChanged_items.DataSource = finalserList;
                       // GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ");
                       GridViewChanged_items.DataBind();
                       //***********
                   }
                   else
                   {
                       List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                       emptyGridList.Add(new ReptPickSerials());
                       GridViewChanged_items.DataSource = emptyGridList;
                       GridViewChanged_items.DataBind();

                       // GridViewChanged_items.DataSource = AllserList;
                       // // GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ");
                       // GridViewChanged_items.DataBind();
                   }
                   ////////////////////////////////////////////////////added on***************************************
               }
              // i++;
               //---

               
           }
          
        }

        protected void GridViewChanged_items_PageIndexChanging2(object sender, GridViewPageEventArgs e)
        {
            GridViewChanged_items.PageIndex = e.NewPageIndex;
            GridViewChanged_items.DataSource = list_NEWGetAllScanSerialsList;
            GridViewChanged_items.DataBind();
        }

        protected void btnAddSearch_Click(object sender, EventArgs e)
        {
            int user_seq_no = -1;
            if (ddlBinCode.SelectedIndex == 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select Bin code!");
                return;
            }
            //else if (ddlItemCodes.SelectedIndex == 0)
            else if(txtItemCD.Text=="")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter Item code!");
                return;
            }
            if (ddlStatusNew.SelectedValue == "CONS")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot change status to Consignment. Select a different status!");
                ddlItemStatus.SelectedValue = "--select--";
                return;
            }
       ////commented since user seq num is not need be generated
       //    if(ddlScanBatches.SelectedIndex!=0)
       //    {
       //        user_seq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
       //    }
       //    else if (generated_seq > 0)
       //    {
       //        user_seq_no = generated_seq;
       //    }
       //    else {
       //        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select a Bath or Scan new Bath!");
       //        //return;

       //        //go for new scan. It should be a ADJ(-)
       //        isManualscan = false;
       //        if (ddlScanBatches.SelectedIndex != 0)
       //        {
       //            Panel_itemDesc.Enabled = false;
       //            return;
       //        }
       //        Panel_itemDesc.Enabled = true;
       //        generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "ADJ", 1, GlbUserComCode);//direction always =1 for this method

       //        ReptPickHeader RPH = new ReptPickHeader();
       //        RPH.Tuh_doc_tp = "ADJ";
       //        RPH.Tuh_cre_dt = DateTime.Today;//might change //Calendar-SelectedDate;
       //        RPH.Tuh_ischek_itmstus = true;//might change 
       //        RPH.Tuh_ischek_reqqty = true;//might change
       //        RPH.Tuh_ischek_simitm = true;//might change
       //        RPH.Tuh_session_id = GlbUserSessionID;
       //        RPH.Tuh_usr_com = GlbUserComCode;//might change 
       //        RPH.Tuh_usr_id = GlbUserName;
       //        RPH.Tuh_usrseq_no = generated_seq;

       //        RPH.Tuh_direct = false; //direction always (-) for change status

       //        //write entry to TEMP_PICK_HDR
       //        int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
       //        seqNumList_out = CHNLSVC.Inventory.GetAll_Adj_SeqNums_for_user(GlbUserName, "ADJ", 0, GlbUserComCode);//for OUT(status changes comes as outs)
       //        seqNumList_out[0] = "New Scan";
       //        ddlScanBatches.DataSource = seqNumList_out;
       //        ddlScanBatches.DataBind();
       //        if (affected_rows > 0)
       //        {
       //            lblSeqno.Text = generated_seq.ToString();
       //            //set session variables
       //            GlbSerialScanUserSeqNo = generated_seq;
       //            GlbSerialScanDocumentType = "ADJ";
       //            GlbSerialScanDirection = 0;
       //            GlbSerialScanReturnUrl = HttpContext.Current.Request.Url.AbsoluteUri;

       //            //finally redirect to scanSerial page
       //        }    
               
                
       //    }
       //    //commented since user seq num is not need be generated  
            
           // string serial_1 = txtSerial1.Text;
           // string serial_2 = txtSerial2.Text;
            string binCode = ddlBinCode.SelectedValue.ToString();
          //  string itemCode = ddlItemCodes.SelectedValue;
            string itemCode = txtItemCD.Text.Trim();
            string itemStatus = ddlItemStatus.SelectedValue.ToString();
            Decimal qty = 1; //Convert.ToDecimal(txtQty.Text);
            string item_description = lblDiscription.Text;

           // DateTime date = Convert.ToDateTime(txtDate.Text);
           // string Manual_ref = txtManualRefNo.Text;
            //string remarks = txtRemarks.Text;


          // lblPopupItemCode.Text = ddlItemCodes.SelectedValue;
            lblPopupItemCode.Text = txtItemCD.Text.Trim();
           lblPopupBinCode.Text = ddlBinCode.SelectedValue;
           
           MasterItem msitem = new MasterItem();
           msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
           if (msitem.Mi_is_ser1 == 0)//check whether it is a non-sirialized item
           //change msitem.Mi_is_ser1 == false
           {
               lblPopQty.Visible = true;
               txtPopupQty.Visible = true;
               
           }
           else {
               lblPopQty.Visible = true;
               txtPopupQty.Visible = true;
           }
           serial_list = CHNLSVC.Inventory.Get_all_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca,binCode, itemCode);
           //if (serial_list != null)
           //{
           //    lblPopupAmt.Text = "Qty in hand: "+ serial_list.Count.ToString();
           //}
           //else
           //{
           //    lblPopupAmt.Text = "Qty in hand: " + "0";
           //}
           //GridPopup.DataSource = serial_list;
           //GridPopup.DataBind();

           //ModalPopupExtItem.Show();
            #region if want to stop CONS

           serial_list = CHNLSVC.Inventory.Get_all_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca, binCode, itemCode);
           List<ReptPickSerials> GridBindList = new List<ReptPickSerials>();
           foreach (ReptPickSerials rp in serial_list)
           {
               if (rp.Tus_itm_stus != "CONS")
               {
                   GridBindList.Add(rp);
               }

           }

           if (GridBindList != null)
           {
               lblPopupAmt.Text = "Qty in hand: " + GridBindList.Count.ToString();
           }
           else
           {
               lblPopupAmt.Text = "Qty in hand: " + "0";
           }
           GridPopup.DataSource = GridBindList;
           GridPopup.DataBind();

           ModalPopupExtItem.Show(); 
            #endregion
        
        }

        protected void btnSearchSerial1_Click(object sender, EventArgs e)
        {
            string serial1=txtSerial1.Text.Trim();
            DataTable dt_serchSerial = new DataTable();
            try
            {
                dt_serchSerial = CHNLSVC.Inventory.getDetail_on_serial1(GlbUserComCode, GlbUserDefLoca, serial1);//this returns table of INR_SER
                if (dt_serchSerial==null)//dt_serchSerial.Rows.Count < 1
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No item available with this serial number!");
                    return;
                }
                if (dt_serchSerial.Rows.Count == 1)
                {
                    foreach (DataRow r in dt_serchSerial.Rows)
                    {
                        // Get the value of the wanted column and cast it to string
                        string bin_code = (string)r["INS_BIN"];
                        Decimal unitCost=(Decimal)r["INS_UNIT_COST"];
                        string itemCode = (string)r["INS_ITM_CD"];
                        string itemStatus = (string)r["INS_ITM_STUS"];
                        if (itemStatus=="CONS")
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Status of a Consignment item cannot be changed!");
                            return;
                        }

                        if (System.DBNull.Value != r["INS_SER_2"])
                        {
                            string serial2 = (string)r["INS_SER_2"];
                            txtSerial2.Text = serial2;
                        }

                        ddlBinCode.SelectedValue = bin_code;
                      //  ddlItemCodes.SelectedValue = itemCode; //commented on 26-6-2012
                        txtItemCD.Text = itemCode;
                       // ddlItemStatus.SelectedValue = itemStatus;
                        MasterMsgInfoUCtrl.Clear();
                        MasterItem msitem = new MasterItem();
                        msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
                        Int32 isSerialized= msitem.Mi_is_ser1;
                        string Serialized_status = "";
                        if (isSerialized == 1)
                        {
                             Serialized_status = "Serialized Item";
                        }
                        else
                        {
                            Serialized_status = "Non Serialized Item";
                        }
                        lblDiscription.Text = "Item Details...:  " +  " Current Status:" + itemStatus + ", " + "Bin:" + bin_code + ", " + Serialized_status;
                    }

                }
                else if (dt_serchSerial.Rows.Count > 1)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "More than one item exist for this serial number!");
                    return;
                }
                else if (dt_serchSerial.Rows.Count == 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No item available with this serial number for scanning!");
                    return;
                }
            }
            catch(Exception ex ){
                  MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Sorry, cannot proceed with this serial number!");
                    return;
            }
            
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (ddlBinCode.SelectedIndex == 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select Bin code!");
                return;
            }
           // else if (ddlItemCodes.SelectedIndex == 0)
            else if (txtItemCD.Text.Trim()=="")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter Item code!");
                return;
            }
            if (ddlStatusNew.SelectedValue == "CONS")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot change status to Consignment. Select a different status!");
                ddlItemStatus.SelectedValue = "--select--";
                return;
            }
            int userseq_no=-1;
            string serial_1 = txtSerial1.Text.Trim();
            string serial_2 = txtSerial2.Text.Trim();
            string binCode = ddlBinCode.SelectedValue.ToString();
           // string itemCode = ddlItemCodes.SelectedValue;
            string itemCode = txtItemCD.Text.Trim();
            string itemStatus = ddlItemStatus.SelectedValue.ToString();
            Decimal qty = 1;//Convert.ToDecimal(txtQty.Text);
            ReptPickSerials _reptPickSerial=new ReptPickSerials();
            List<ReptPickSerials> ser_list = new List<ReptPickSerials>();
            string item_description = lblDiscription.Text;
            
            if (ddlScanBatches.SelectedIndex != 0)
            {
                userseq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
            }
           
            else if (generated_seq > 0)
            {
                userseq_no = generated_seq;
                //newly added 12
                ddlScanBatches.SelectedValue = generated_seq.ToString();
                lblSeqno.Text = generated_seq.ToString();
            }
           
           
                    else
                    {
                        // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select a Batch or Scan new Bath!");
                        // return;
                        
                       //go for new scan. It should be a ADJ(-)
                       // isManualscan = false;
                        if (ddlScanBatches.SelectedIndex != 0)
                        {
                            Panel_itemDesc.Enabled = false;
                            return;
                        }
                        Panel_itemDesc.Enabled = true;
                        generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "ADJ", 1, GlbUserComCode);//direction always =1 for this method

                        ReptPickHeader RPH = new ReptPickHeader();
                        RPH.Tuh_doc_tp = "ADJ-S";
                        RPH.Tuh_cre_dt = DateTime.Today;//might change //Calendar-SelectedDate;
                        RPH.Tuh_ischek_itmstus = true;//might change 
                        RPH.Tuh_ischek_reqqty = true;//might change
                        RPH.Tuh_ischek_simitm = true;//might change
                        RPH.Tuh_session_id = GlbUserSessionID;
                        RPH.Tuh_usr_com = GlbUserComCode;//might change 
                        RPH.Tuh_usr_id = GlbUserName;
                        RPH.Tuh_usrseq_no = generated_seq;

                        RPH.Tuh_direct = false; //direction always (-) for change status

                        //write entry to TEMP_PICK_HDR
                        int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
                        seqNumList_out = CHNLSVC.Inventory.GetAll_Adj_SeqNums_for_user(GlbUserName, "ADJ-S", 0, GlbUserComCode);//for OUT(status changes comes as outs)
                        seqNumList_out[0] = "New Scan";
                        ddlScanBatches.DataSource = seqNumList_out;
                        ddlScanBatches.DataBind();

                        //****new add
                        ddlScanBatches.SelectedValue = generated_seq.ToString();
                        lblSeqno.Text = generated_seq.ToString();
                        if (affected_rows > 0)
                        {
                            lblSeqno.Text = generated_seq.ToString();
                            //set session variables
                            GlbSerialScanUserSeqNo = generated_seq;
                            GlbSerialScanDocumentType = "ADJ-S";
                            GlbSerialScanDirection = 0;
                            GlbSerialScanReturnUrl = HttpContext.Current.Request.Url.AbsoluteUri;

                            //finally redirect to scanSerial page
                        }    
               
                
                      
                    }
            
            MasterItem msitem = new MasterItem();
            msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
            if (msitem.Mi_is_ser1 == 0)//check whether it is a non-sirialized item
                //change msitem.Mi_is_ser1 == false
            {               
                lblPopQty.Visible = true;
                txtPopupQty.Visible = true;
                //serial_list = CHNLSVC.Inventory.Get_all_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca,binCode, itemCode);

                ////serial_list = CHNLSVC.Inventory.Get_all_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca, binCode, itemCode);
                //if (serial_list != null)
                //{
                //    lblPopupAmt.Text = "Qty in hand: " + serial_list.Count.ToString();
                //}
                //else
                //{
                //    lblPopupAmt.Text = "Qty in hand: " + "0";
                //}
                //GridPopup.DataSource = serial_list;
                //GridPopup.DataBind();

                //ModalPopupExtItem.Show();

                #region if want to stop CONS

                serial_list = CHNLSVC.Inventory.Get_all_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca, binCode, itemCode);
                List<ReptPickSerials> GridBindList = new List<ReptPickSerials>();
                foreach (ReptPickSerials rp in serial_list)
                {
                    if (rp.Tus_itm_stus != "CONS")
                    {
                        GridBindList.Add(rp);
                    }

                }

                if (GridBindList != null)
                {
                    lblPopupAmt.Text = "Qty in hand: " + GridBindList.Count.ToString();
                }
                else
                {
                    lblPopupAmt.Text = "Qty in hand: " + "0";
                }
                GridPopup.DataSource = GridBindList;
                GridPopup.DataBind();

                ModalPopupExtItem.Show(); 
                #endregion

            }
            else//if it is a sirialized item
            {
                if (serial_1 == null || serial_1 == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter Serial number first!");
                    return;
                }
                else
                {
                    if (ddlBinCode.SelectedIndex == 0)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select Bin code!");
                        return;
                    }
                   // else if (ddlItemCodes.SelectedIndex == 0)
                    else if (txtItemCD.Text.Trim()=="")
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter Item code!");
                        return;
                    }
                    _reptPickSerial = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, serial_1);
                    ser_list.Add(_reptPickSerial);
                }

                // DateTime date = Convert.ToDateTime(txtDate.Text);
                // string Manual_ref = txtManualRefNo.Text;
                // string remarks = txtRemarks.Text;
                lblPopQty.Visible = false;
                txtPopupQty.Visible = false;


                if (_reptPickSerial.Tus_ser_1 == null)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter valid Serial Number!");
                    return;
                }
                //_reptPickSerial.Tus_itm_stus = itemStatus;//assign the new status
                _reptPickSerial.Tus_cre_by = GlbUserName;
                _reptPickSerial.Tus_new_status = itemStatus;
                MasterItem mi = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
                _reptPickSerial.Tus_itm_desc = mi.Mi_shortdesc;
                _reptPickSerial.Tus_itm_model = mi.Mi_model;
               
                //GetItem(string _company, string _item);
                if (ddlScanBatches.SelectedIndex == 0)
                {
                    _reptPickSerial.Tus_usrseq_no = generated_seq;
                    userseq_no = generated_seq;
                }
                else
                {
                    _reptPickSerial.Tus_usrseq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
                    userseq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
                }

                if (ddlItemStatus.SelectedValue != "--select--")
                {

                    _reptPickSerial.Tus_new_status = ddlItemStatus.SelectedValue;//new status
                    //TODO:assign the new status-**done
                    //assign to the TUS_NEW_ITM_STUS;
                }
                else
                {
                    _reptPickSerial.Tus_new_status = "-";
                }
               
                //Update_inrser_INS_AVAILABLE
                Boolean update = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serial_1, -1);
                //enter row into TEMP_PICK_SER
                Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial, null);

                isManualscan = true;
                reptPickSerials_list.Add(_reptPickSerial);
                //GridViewChanged_items.DataSource = reptPickSerials_list;

                //GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ-S");
                //GridViewChanged_items.DataBind();

                /////////////////////////////////////////////////////////////////////////////////////////////////////
                //to view in grid
                Int32 user_seqNo = Convert.ToInt32(ddlScanBatches.SelectedValue);
                List<ReptPickSerials> AllserList = new List<ReptPickSerials>();
                AllserList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, user_seqNo, "ADJ-S");
                List<ReptPickSerials> finalserList = new List<ReptPickSerials>();
                if (AllserList != null)
                {
                    foreach (ReptPickSerials rps in AllserList)
                    {
                        if (rps.Tus_ser_1 != "N/A")
                        {
                            finalserList.Add(rps);
                        }
                    }
                    //**********************************************************************************
                    List<ReptPickSerials> AllserList_Nonser = new List<ReptPickSerials>();
                    foreach (ReptPickSerials rps in AllserList)
                    {
                        if (rps.Tus_ser_1 == "N/A")
                        {
                            AllserList_Nonser.Add(rps);
                        }
                    }
                    var _tbitems = from _pickSerials in AllserList_Nonser
                                   group _pickSerials by new { _pickSerials.Tus_ser_1, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_bin, _pickSerials.Tus_itm_stus, _pickSerials.Tus_new_status, _pickSerials.Tus_new_remarks } into itm
                                   select new { Tus_ser_1 = itm.Key.Tus_ser_1, Tus_itm_model = itm.Key.Tus_itm_model, Tus_itm_cd = itm.Key.Tus_itm_cd, itm.Key.Tus_itm_desc, itm.Key.Tus_bin, Tus_itm_stus = itm.Key.Tus_itm_stus, Tus_new_status = itm.Key.Tus_new_status, Tus_qty = itm.Sum(p => p.Tus_qty), Tus_new_remarks = itm.Key.Tus_new_remarks };

                    //  List<ReptPickSerials> _assignList = new List<ReptPickSerials>();

                    foreach (var _lists in _tbitems)
                    {
                        ReptPickSerials _one = new ReptPickSerials();
                        _one.Tus_ser_1 = _lists.Tus_ser_1;
                        _one.Tus_itm_cd = _lists.Tus_itm_cd;
                        _one.Tus_itm_stus = _lists.Tus_itm_stus;
                        _one.Tus_new_status = _lists.Tus_new_status;
                        _one.Tus_qty = _lists.Tus_qty;
                        _one.Tus_usrseq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
                        _one.Tus_new_remarks = _lists.Tus_new_remarks;

                        _one.Tus_itm_model = _lists.Tus_itm_model;
                        _one.Tus_bin = _lists.Tus_bin;
                        _one.Tus_itm_desc = _lists.Tus_itm_desc;
                        // _assignList.Add(_one);
                        finalserList.Add(_one);

                    }

                    GridViewChanged_items.DataSource = finalserList;
                   
                    GridViewChanged_items.DataBind();
                }
                else
                {
                    List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                    emptyGridList.Add(new ReptPickSerials());
                    GridViewChanged_items.DataSource = emptyGridList;
                    GridViewChanged_items.DataBind();
                    
                }

                //////////////////////////////////////////////////////////////////////////////////////////////////////
                txtSerial1.Text=null;

                txtSerial2.Text=null;

              //  ddlItemCodes.Text=null;
                txtItemCD.Text = "";
                ddlBinCode.Text=null;

                lblDiscription.Text = null;
            }


        }

        protected void ddlItemCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
           // lblPopupItemCode.Text = ddlItemCodes.SelectedValue;
          //  lblPopupBinCode.Text = ddlBinCode.SelectedValue;

            //txtItemDesc.Text = ddlItemCodes.SelectedValue.Trim();
            //MasterItem msitem = new MasterItem();
            //msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, ddlItemCodes.SelectedValue.ToString());
            //txtItemDesc.Text = msitem.Mi_longdesc;
        }

        protected void btnPopupOk_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> changed_serials = new Dictionary<string, string>();
           // int i = 0;
            string itemCode = lblPopupItemCode.Text.Trim();
            string binCode = lblPopupBinCode.Text.Trim();
            Int32 userseq_no;
            ////if (ddlScanBatches.SelectedIndex == 0)
            ////{
            ////    userseq_no = generated_seq;
            ////}
            ////else
            ////{
            ////    userseq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
            ////}
            if (ddlScanBatches.SelectedIndex != 0)
            {
                userseq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
                lblSeqno.Text = "";
            }

            else if (generated_seq > 0)
            {
                userseq_no = generated_seq;
                //newly added 12
                ddlScanBatches.SelectedValue = generated_seq.ToString();
                lblSeqno.Text = generated_seq.ToString();
            }
            else
            {
                // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select a Batch or Scan new Bath!");
                // return;

                //go for new scan. It should be a ADJ(-)
                // isManualscan = false;
                if (ddlScanBatches.SelectedIndex != 0)
                {
                    Panel_itemDesc.Enabled = false;
                    return;
                }
                Panel_itemDesc.Enabled = true;
                generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "ADJ", 1, GlbUserComCode);//direction always =1 for this method
               //assign user_seqno
                userseq_no = generated_seq;
                ReptPickHeader RPH = new ReptPickHeader();
                RPH.Tuh_doc_tp = "ADJ-S";
                RPH.Tuh_cre_dt = DateTime.Today;//might change //Calendar-SelectedDate;
                RPH.Tuh_ischek_itmstus = true;//might change 
                RPH.Tuh_ischek_reqqty = true;//might change
                RPH.Tuh_ischek_simitm = true;//might change
                RPH.Tuh_session_id = GlbUserSessionID;
                RPH.Tuh_usr_com = GlbUserComCode;//might change 
                RPH.Tuh_usr_id = GlbUserName;
                RPH.Tuh_usrseq_no = generated_seq;

                RPH.Tuh_direct = false; //direction always (-) for change status

                //write entry to TEMP_PICK_HDR
                int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
                seqNumList_out = CHNLSVC.Inventory.GetAll_Adj_SeqNums_for_user(GlbUserName, "ADJ-S", 0, GlbUserComCode);//for OUT(status changes comes as outs)
                seqNumList_out[0] = "New Scan";
                ddlScanBatches.DataSource = seqNumList_out;
                ddlScanBatches.DataBind();

                //****new add
                ddlScanBatches.SelectedValue = generated_seq.ToString();
                lblSeqno.Text = generated_seq.ToString();
                if (affected_rows > 0)
                {
                    lblSeqno.Text = generated_seq.ToString();
                    //set session variables
                    GlbSerialScanUserSeqNo = generated_seq;
                    GlbSerialScanDocumentType = "ADJ-S";
                    GlbSerialScanDirection = 0;
                    GlbSerialScanReturnUrl = HttpContext.Current.Request.Url.AbsoluteUri;

                    //finally redirect to scanSerial page
                }
            }

            MasterItem msitem = new MasterItem();
            msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
            if (msitem.Mi_is_ser1 == 0 || msitem.Mi_is_ser1 == -1) //if non serial
            {  
                
                int rowCount = 0;

                foreach (GridViewRow gvr in this.GridPopup.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
                    // string serial_1 = (string)gvr.Cells[1].Text.Trim();
                    Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                    string currentStatus= gvr.Cells[3].Text;
                    if (chkSelect.Checked)
                    {
                        if (ddlItemStatus.SelectedValue == gvr.Cells[3].Text)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Items that are having same status for Current status and New Status, cannot be added!");
                            continue; 
                        }
                        // Boolean update = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serial_1, -1);
                        //-------------
                        ReptPickSerials _reptPickSerial_nonSer = CHNLSVC.Inventory.Get_all_details_on_serialID(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, serID);
                        if (ddlItemStatus.SelectedValue != "--select--")
                        {

                            _reptPickSerial_nonSer.Tus_new_status = ddlItemStatus.SelectedValue;
                            //TODO:assign the new status-**done
                            //assign to the TUS_NEW_ITM_STUS;
                        }
                        else {
                           
                            _reptPickSerial_nonSer.Tus_new_status = "-";
                        }

                        _reptPickSerial_nonSer.Tus_itm_stus = currentStatus;
                        _reptPickSerial_nonSer.Tus_cre_by = GlbUserName;
                        if (ddlScanBatches.SelectedIndex == 0)
                        {
                            _reptPickSerial_nonSer.Tus_usrseq_no = generated_seq;
                            // userseq_no= generated_seq;
                        }
                        else
                        {
                            _reptPickSerial_nonSer.Tus_usrseq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
                            //  userseq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
                        }
                        //Update_inrser_INS_AVAILABLE
                        Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serID, -1);
                        //  Boolean update_inr_ser = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, "N/A", -1);
                        _reptPickSerial_nonSer.Tus_cre_by = GlbUserName;
                        _reptPickSerial_nonSer.Tus_session_id = GlbUserSessionID;
                        // _reptPickSerial_nonSer.Tus_new_status = itemStatus;
                        MasterItem mi = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
                        _reptPickSerial_nonSer.Tus_itm_desc = mi.Mi_shortdesc;
                        _reptPickSerial_nonSer.Tus_itm_model = mi.Mi_model;
                        //enter row into TEMP_PICK_SER
                        Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_nonSer, null);
                       
                        rowCount++;
                        isManualscan = true;

                    }

                }
                #region create dummy

                //if (rowCount > 0)
                //{   //create a dummy to represent the sum
                //    ReptPickSerials reptPickSerial_dummy = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, "N/A");
                //    reptPickSerial_dummy.Tus_usrseq_no = userseq_no;
                //    reptPickSerial_dummy.Tus_ser_id = 0;
                //    reptPickSerial_dummy.Tus_ser_1 = "_";
                    
                //    //reptPickSerial_dummy.Tus_new_status = ddlItemStatus.SelectedValue;
                //    reptPickSerial_dummy.Tus_cre_by = GlbUserName;
                //    reptPickSerial_dummy.Tus_session_id = GlbUserSessionID;
                    
                //    reptPickSerial_dummy.Tus_bin = binCode;
                //    reptPickSerial_dummy.Tus_itm_cd = itemCode;
                //    reptPickSerial_dummy.Tus_loc = GlbUserDefLoca;
                //    reptPickSerial_dummy.Tus_com = GlbUserComCode;
                   
                //    if (ddlItemStatus.SelectedValue != "--select--")
                //    {
                //         reptPickSerial_dummy.Tus_new_status = ddlItemStatus.SelectedValue;
                //        //assign to the TUS_NEW_ITM_STUS;
                //    }
                //    MasterItem mi = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
                //    reptPickSerial_dummy.Tus_itm_desc = mi.Mi_shortdesc;
                //    reptPickSerial_dummy.Tus_itm_model = mi.Mi_model;
                //    reptPickSerial_dummy.Tus_qty = rowCount;
                   
                //    Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(reptPickSerial_dummy, null);
                //}
                #endregion create dummy



                //to view in grid
                List<ReptPickSerials> AllserList = new List<ReptPickSerials>();
                AllserList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ-S");
                List<ReptPickSerials> finalserList = new List<ReptPickSerials>();
                if (AllserList != null)
                {
                    foreach (ReptPickSerials rps in AllserList)
                    {
                        if (rps.Tus_ser_1 != "N/A")
                        {
                            finalserList.Add(rps);
                        }
                    }
                   
//**********************************************************************************
                    List<ReptPickSerials> AllserList_Nonser = new List<ReptPickSerials>();
                    foreach (ReptPickSerials rps in AllserList)
                    {
                        if (rps.Tus_ser_1 == "N/A")
                        {
                            AllserList_Nonser.Add(rps);
                        }
                    }
                    var _tbitems = from _pickSerials in AllserList_Nonser
                                   group _pickSerials by new { _pickSerials.Tus_ser_1, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_bin, _pickSerials.Tus_itm_stus, _pickSerials.Tus_new_status, _pickSerials.Tus_new_remarks } into itm
                                   select new { Tus_ser_1 = itm.Key.Tus_ser_1, Tus_itm_model = itm.Key.Tus_itm_model, Tus_itm_cd = itm.Key.Tus_itm_cd, itm.Key.Tus_itm_desc, itm.Key.Tus_bin, Tus_itm_stus = itm.Key.Tus_itm_stus, Tus_new_status = itm.Key.Tus_new_status, Tus_qty = itm.Sum(p => p.Tus_qty), Tus_new_remarks = itm.Key.Tus_new_remarks };

                  //  List<ReptPickSerials> _assignList = new List<ReptPickSerials>();

                    foreach (var _lists in _tbitems)
                    {
                        ReptPickSerials _one = new ReptPickSerials();
                        _one.Tus_ser_1 = _lists.Tus_ser_1;
                        _one.Tus_itm_cd = _lists.Tus_itm_cd;
                        _one.Tus_itm_stus = _lists.Tus_itm_stus;
                        _one.Tus_new_status = _lists.Tus_new_status;
                        _one.Tus_qty = _lists.Tus_qty;
                        _one.Tus_usrseq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
                        _one.Tus_new_remarks = _lists.Tus_new_remarks;

                        _one.Tus_itm_model = _lists.Tus_itm_model;
                        _one.Tus_bin = _lists.Tus_bin;
                        _one.Tus_itm_desc = _lists.Tus_itm_desc;
                       // _assignList.Add(_one);
                        finalserList.Add(_one);

                    }

 //************************************************************************************                                         


                    GridViewChanged_items.DataSource = finalserList;
                    // GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ");
                    GridViewChanged_items.DataBind();
                    //***********
                }
                else
                {
                    List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                    emptyGridList.Add(new ReptPickSerials());
                    GridViewChanged_items.DataSource = emptyGridList;
                    GridViewChanged_items.DataBind();

                   // GridViewChanged_items.DataSource = AllserList;
                  //  // GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ");
                   // GridViewChanged_items.DataBind();
                }
                

            }
            else //  when (msitem.Mi_is_ser1 != 0)
            {
                foreach (GridViewRow gvr in this.GridPopup.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
                    string serial_1 = (string)gvr.Cells[1].Text.Trim();

                    if (chkSelect.Checked)
                    {
                        // Boolean update = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serial_1, -1);
                        //-------------
                        if (ddlItemStatus.SelectedValue == gvr.Cells[3].Text)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Items that are having same status for Current status and New Status, cannot be added!");
                            continue; 
                        }
                        ReptPickSerials _reptPickSerial = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, serial_1);
                        if (ddlItemStatus.SelectedValue != "--select--")
                        {
                            //TODO:assign the new status
                            _reptPickSerial.Tus_new_status = ddlItemStatus.SelectedValue;
                            //assign to the TUS_NEW_ITM_STUS;
                        }
                        else
                        {

                            _reptPickSerial.Tus_new_status = "-";
                        }
                        _reptPickSerial.Tus_cre_by = GlbUserName;
                        if (ddlScanBatches.SelectedIndex == 0)
                        {
                            _reptPickSerial.Tus_usrseq_no = generated_seq;
                            // userseq_no= generated_seq;
                        }
                        else
                        {
                            _reptPickSerial.Tus_usrseq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
                            //  userseq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
                        }
                        //Update_inrser_INS_AVAILABLE
                        Boolean update_inr_ser = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serial_1, -1);
                        _reptPickSerial.Tus_cre_by = GlbUserName;
                        // _reptPickSerial.Tus_new_status = itemStatus;
                        MasterItem mi = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
                        _reptPickSerial.Tus_itm_desc = mi.Mi_shortdesc;
                        _reptPickSerial.Tus_itm_model = mi.Mi_model;
                        //enter row into TEMP_PICK_SER
                        Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial, null);

                        isManualscan = true;

                        //GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ");
                        //GridViewChanged_items.DataBind();

                        // reptPickSerials_list.Add(_reptPickSerial);
                        // GridViewChanged_items.DataSource = reptPickSerials_list;

                     }


                    //i++;
                    //---

                }
                //to view in grid
                List<ReptPickSerials> AllserList = new List<ReptPickSerials>();
                AllserList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ-S");
                List<ReptPickSerials> finalserList = new List<ReptPickSerials>();
                if (AllserList != null)
                {
                    foreach (ReptPickSerials rps in AllserList)
                    {
                        if (rps.Tus_ser_1 != "N/A")
                        {
                            finalserList.Add(rps);
                        }
                    }
                    //**********************************************************************************
                    List<ReptPickSerials> AllserList_Nonser = new List<ReptPickSerials>();
                    foreach (ReptPickSerials rps in AllserList)
                    {
                        if (rps.Tus_ser_1 == "N/A")
                        {
                            AllserList_Nonser.Add(rps);
                        }
                    }
                    var _tbitems = from _pickSerials in AllserList_Nonser
                                   group _pickSerials by new { _pickSerials.Tus_ser_1, _pickSerials.Tus_itm_model, _pickSerials.Tus_itm_cd, _pickSerials.Tus_itm_desc, _pickSerials.Tus_bin, _pickSerials.Tus_itm_stus, _pickSerials.Tus_new_status, _pickSerials.Tus_new_remarks } into itm
                                   select new { Tus_ser_1 = itm.Key.Tus_ser_1, Tus_itm_model = itm.Key.Tus_itm_model, Tus_itm_cd = itm.Key.Tus_itm_cd, itm.Key.Tus_itm_desc, itm.Key.Tus_bin, Tus_itm_stus = itm.Key.Tus_itm_stus, Tus_new_status = itm.Key.Tus_new_status, Tus_qty = itm.Sum(p => p.Tus_qty), Tus_new_remarks = itm.Key.Tus_new_remarks };

                    //  List<ReptPickSerials> _assignList = new List<ReptPickSerials>();

                    foreach (var _lists in _tbitems)
                    {
                        ReptPickSerials _one = new ReptPickSerials();
                        _one.Tus_ser_1 = _lists.Tus_ser_1;
                        _one.Tus_itm_cd = _lists.Tus_itm_cd;
                        _one.Tus_itm_stus = _lists.Tus_itm_stus;
                        _one.Tus_new_status = _lists.Tus_new_status;
                        _one.Tus_qty = _lists.Tus_qty;
                        _one.Tus_usrseq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
                        _one.Tus_new_remarks = _lists.Tus_new_remarks;

                        _one.Tus_itm_model = _lists.Tus_itm_model;
                        _one.Tus_bin = _lists.Tus_bin;
                        _one.Tus_itm_desc = _lists.Tus_itm_desc;
                        // _assignList.Add(_one);
                        finalserList.Add(_one);

                    }

                    //************************************************************************************  
                    GridViewChanged_items.DataSource = finalserList;
                    // GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ");
                    GridViewChanged_items.DataBind();
                    //***********
                }
                else
                {
                    List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                    emptyGridList.Add(new ReptPickSerials());
                    GridViewChanged_items.DataSource = emptyGridList;
                    GridViewChanged_items.DataBind();

                   // GridViewChanged_items.DataSource = AllserList;
                   // // GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ");
                   // GridViewChanged_items.DataBind();
                }
                //*************
                //GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ");
                //GridViewChanged_items.DataBind();
            }



            isManualscan = true;
            

           
            
        }
        protected void btnPopupSarch_Click(object sender, EventArgs e)
        {
            //List<ReptPickSerials> serial_list = new List<ReptPickSerials>();
           
            if (ddlPopupSerial.SelectedValue == "Serial No 1")
            {
                string serial_no = txtPopupSearchSer.Text.Trim();
                //call query.
                string serch_serial = txtPopupSearchSer.Text.Trim() + "%";
                string bin = lblPopupBinCode.Text.Trim();
                serial_list = CHNLSVC.Inventory.Search_by_serial(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text.Trim(), bin, serch_serial, null);
                //GridPopup.DataSource = serial_list;
                //GridPopup.DataBind();
                lblPopupMsg.Text = "";

                //ModalPopupExtItem.Show(); 
                List<ReptPickSerials> GridBindList = new List<ReptPickSerials>();
                foreach (ReptPickSerials rp in serial_list)
                {
                    if (rp.Tus_itm_stus != "CONS")
                    {
                        GridBindList.Add(rp);
                    }

                }

                if (GridBindList != null)
                {
                    lblPopupAmt.Text = "Qty in hand: " + GridBindList.Count.ToString();
                }
                else
                {
                    lblPopupAmt.Text = "Qty in hand: " + "0";
                }
                GridPopup.DataSource = GridBindList;
                GridPopup.DataBind();

                ModalPopupExtItem.Show(); 

            }
            else if (ddlPopupSerial.SelectedValue == "Serial No 2")
            {
                string serial_no = txtPopupSearchSer.Text.Trim();
                //call query.
                string serch_serial = txtPopupSearchSer.Text.Trim() + "%";
                string bin = lblPopupBinCode.Text.Trim();
                serial_list = CHNLSVC.Inventory.Search_by_serial(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text.Trim(), bin, null, serch_serial);
                //GridPopup.DataSource = serial_list;
                //GridPopup.DataBind();
                lblPopupMsg.Text = "";

               // ModalPopupExtItem.Show();

                /////
                List<ReptPickSerials> GridBindList = new List<ReptPickSerials>();
                foreach (ReptPickSerials rp in serial_list)
                {
                    if (rp.Tus_itm_stus != "CONS")
                    {
                        GridBindList.Add(rp);
                    }

                }

                if (GridBindList != null)
                {
                    lblPopupAmt.Text = "Qty in hand: " + GridBindList.Count.ToString();
                }
                else
                {
                    lblPopupAmt.Text = "Qty in hand: " + "0";
                }
                GridPopup.DataSource = GridBindList;
                GridPopup.DataBind();

                ModalPopupExtItem.Show(); 
            }
            else
            {
               // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select serial type from drop down!");
                lblPopupMsg.Text = "Select serial type first!";
                ModalPopupExtItem.Show();
            }

        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void btnGridOK_Click(object sender, EventArgs e)
        {
            //GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, -1, "ADJ");
            //GridViewChanged_items.DataBind();

            //List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
            //emptyGridList.Add(new ReptPickSerials());
            //GridViewChanged_items.DataSource = emptyGridList;
            //GridViewChanged_items.DataBind();
            Response.Redirect("~/Inventory_Module/StatusChangeEntry.aspx");
        }

        protected void btnSearchSerial2_Click(object sender, EventArgs e)
        {
            string serial_2 = txtSerial2.Text.Trim();
          //  string supplierCD = txtSupplierCd.Text.Trim();
            
            DataTable dt_serchSerial = new DataTable();
            try
            {
                dt_serchSerial = CHNLSVC.Inventory.getDetail_on_serial_Supplier(GlbUserComCode, GlbUserDefLoca, null, null, serial_2);//this returns table of INR_SER
                if (dt_serchSerial.Rows.Count < 1)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No item available with this serial number!");
                    return;
                }
                if (dt_serchSerial.Rows.Count == 1)
                {
                    foreach (DataRow r in dt_serchSerial.Rows)
                    {
                        // Get the value of the wanted column and cast it to string
                        string bin_code = (string)r["INS_BIN"];
                        Decimal unitCost = (Decimal)r["INS_UNIT_COST"];
                        string itemCode = (string)r["INS_ITM_CD"];
                        string itemStatus = (string)r["INS_ITM_STUS"];
                        if (System.DBNull.Value != r["INS_SER_1"])
                        {
                            string serial1 = (string)r["INS_SER_1"];
                            txtSerial1.Text = serial1;
                        }

                        ddlBinCode.SelectedValue = bin_code;
                      //  txtItemCd.Text = itemCode;
                        txtItemCD.Text = itemCode;
                        // ddlItemStatus.SelectedValue = itemStatus;
                        MasterMsgInfoUCtrl.Clear();

                        lblDiscription.Text = "Item Details...:  "+ " Current Status:" + itemStatus + ", " + "Bin:" + bin_code;
                    }

                }
                else if (dt_serchSerial.Rows.Count > 1)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "More than one item exist for this serial number!");
                    return;
                }
                else if (dt_serchSerial.Rows.Count == 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No item available with this serial number for scanning!");
                    return;
                }
            }
            catch (Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Sorry, cannot proceed with this serial number!");
                return;
            }
        }

        protected void btnPopupDeselect_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in this.GridPopup.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
                string serial_1 = (string)gvr.Cells[1].Text.Trim();

                if (chkSelect.Checked)
                {
                    chkSelect.Checked = false;

                   
                }
            }
        }

        protected void GridViewChanged_items_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnPopupAutoSelect_Click(object sender, EventArgs e)
        {
           // txtSerial1.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnSerial.ClientID + "')");
           
            //if (txtPopupQty.Text != null ||txtPopupQty.Text != "" || serial_list!=null)
            if (!string.IsNullOrEmpty(txtPopupQty.Text) && serial_list != null)
            {
                try {
                        Int32 qty = Convert.ToInt32((txtPopupQty.Text));
                
                        if (qty> serial_list.Count)
                        {
                            lblPopupAmt.Text = "Qty in hand is " + serial_list.Count + ". You can't exceed qty!";
                        }
                }
                catch(Exception ex){
                    lblPopupAmt.Text = "Qty not in correct format!";
                }
                
            }
            
            ModalPopupExtItem.Show();
        }

        

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
                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

         
        protected void imgBtnItmCDSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable dataSource = CHNLSVC.CommonSearch.GetItemSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
          //  MasterCommonSearchUCtrl.ReturnResultControl = ddlItemCodes.ClientID; //txtItemCD.ClientID;
            MasterCommonSearchUCtrl.ReturnResultControl = txtItemCD.ClientID; //txtItemCD.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
           
        }
      
        protected void CheckBox1_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox ck1 = (CheckBox)sender;

            GridViewRow gdrow = (GridViewRow)ck1.NamingContainer;
            System.Drawing.Color orgcol = gdrow.BackColor;
            if (((CheckBox)gdrow.FindControl("SelectCheck")).Checked)
            {
                gdrow.BackColor = System.Drawing.Color.LightSkyBlue;
            }
            else
            {
                gdrow.BackColor = System.Drawing.Color.Transparent;
                //gdrow.BackColor = orgcol;
            } 

        }

        protected void ddlItemStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCompanyItemStatusData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = ddlItemStatus.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgBtnStatusNew_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCompanyItemStatusData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = ddlStatusNew.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        private void clearscreen()
        { 
            txtSerial1.Text="";
            txtSerial2.Text ="";

            ddlScanBatches.SelectedIndex=0;
            ddlBinCode.SelectedIndex=0;
           // ddlItemCodes.SelectedIndex = 0;
            txtItemCD.Text = "";
            ddlItemStatus.SelectedValue = "--select--";
            ddlStatusNew.SelectedValue = "--select--";
            txtManualRefNo.Text="";
            txtRemarks.Text="";
        }

        protected void txtItemCD_TextChanged(object sender, EventArgs e)
        {
            lblPopupItemCode.Text = txtItemCD.Text.Trim();
            lblPopupBinCode.Text = ddlBinCode.SelectedValue;
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }
        //private void BindJavaScripts()
        //{

        //    txtItemDesc.Attributes.Add("onblur", "GetItemData()");
        //   // txtInvoiceNo.Attributes.Add("onblur", "GetInvoiceData()");
        //}

        //protected void txtItemDesc_TextChanged(object sender, EventArgs e)
        //{
        //   // ddlItemCodes.SelectedValue = txtItemDesc.Text.Trim();
        //    MasterItem msitem = new MasterItem();
        //    msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItemDesc.Text.Trim());
        //    txtItemDesc.Text = ddlItemCodes.Text.Trim();
            
            
        //}

      


    

        
    }
}