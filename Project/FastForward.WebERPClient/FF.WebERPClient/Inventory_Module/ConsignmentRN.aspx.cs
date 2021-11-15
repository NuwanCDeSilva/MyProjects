using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Collections;
using FF.BusinessObjects;
using FF.Interfaces;

namespace FF.WebERPClient
{
    public partial class WebForm5 : BasePage
    {
        static List<ReptPickSerialsSub> list_ReptPickSerialsSubList = new List<ReptPickSerialsSub>();
        static List<ReptPickSerials> list_GetAllScanSerialsList = new List<ReptPickSerials>();
        static List<string> seqNumList = new List<string>();
        static List<string> seqNumList_out = new List<string>();
        static List<string> status_list = new List<string>();
        static List<ReptPickSerials> reptPickSerials_list = new List<ReptPickSerials>();//new items added to grid
        static bool isManualscan;
        static int generated_seq = -1;

        static List<ReptPickSerials> serial_list = new List<ReptPickSerials>();//to store get popup serch result

        static List<ReptPickSerials> list_NEWGetAllScanSerialsList = new List<ReptPickSerials>();

        protected void Page_Load(object sender, EventArgs e)
        {
            txtItemCd.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnItemCd.ClientID + "')");

            if (!IsPostBack)
            {
                isManualscan = true;
                //generated_seq =-1;
                seqNumList_out = CHNLSVC.Inventory.GetAll_Adj_SeqNums_for_user(GlbUserName, "ADJ-C", 0, GlbUserComCode);//for OUT(status changes comes as outs)
                seqNumList_out[0] = "New Scan";
                ddlScanBatches.DataSource = seqNumList_out;
                ddlScanBatches.DataBind();

                List<string> bincode_list = new List<string>();
                bincode_list = CHNLSVC.Inventory.GetAll_binCodes_for_loc(GlbUserComCode, GlbUserDefLoca);

                ddlBinCode.DataSource = bincode_list;
                ddlBinCode.DataBind();

                List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                emptyGridList.Add(new ReptPickSerials());
                GridViewSerials.DataSource = emptyGridList;
                GridViewSerials.DataBind();

                GridViewItems.DataSource = emptyGridList;
                GridViewItems.DataBind();
                TabContainer1.ActiveTabIndex = 0;

            }

            OnBlur();
        }

        protected void btnPopupOk_Click(object sender, EventArgs e)
        {
            string itemCode = lblPopupItemCode.Text.Trim();
            string binCode = lblPopupBinCode.Text.Trim();
            Int32 userseq_no;
           // generated_seq = -1;
            if (ddlScanBatches.SelectedIndex != 0)
            {
                userseq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
                lblSeqno.Text = "";
            }
            
            //else if (generated_seq > 0)
            //{
            //    userseq_no = generated_seq;
            //    //newly added 12
            //    ddlScanBatches.SelectedValue = generated_seq.ToString();
            //    lblSeqno.Text = generated_seq.ToString();
            //}
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
                RPH.Tuh_doc_tp = "ADJ-C";
                RPH.Tuh_cre_dt = DateTime.Today;//might change //Calendar-SelectedDate;
                RPH.Tuh_ischek_itmstus = true;//might change 
                RPH.Tuh_ischek_reqqty = true;//might change
                RPH.Tuh_ischek_simitm = true;//might change
                RPH.Tuh_session_id = GlbUserSessionID;
                RPH.Tuh_usr_com = GlbUserComCode;//might change 
                RPH.Tuh_usr_id = GlbUserName;
                RPH.Tuh_usrseq_no = generated_seq;
                
                RPH.Tuh_direct = false; //direction always (-) for Consignment return

                //write entry to TEMP_PICK_HDR
                int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
                seqNumList_out = CHNLSVC.Inventory.GetAll_Adj_SeqNums_for_user(GlbUserName, "ADJ-C", 0, GlbUserComCode);//for OUT(status changes comes as outs)
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
                    GlbSerialScanDocumentType = "ADJ-C";
                    GlbSerialScanDirection = 0;
                    GlbSerialScanReturnUrl = HttpContext.Current.Request.Url.AbsoluteUri;

                    //finally redirect to scanSerial page
                }
            }

            MasterItem msitem = new MasterItem();
            msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
            if (msitem.Mi_is_ser1 == 0)
            {
                int rowCount = 0;

                foreach (GridViewRow gvr in this.GridPopup.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
                    // string serial_1 = (string)gvr.Cells[1].Text.Trim();
                    Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                    if (chkSelect.Checked)
                    {
                        // Boolean update = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serial_1, -1);
                        //-------------
                       // Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, serial_1);
                        ReptPickSerials _reptPickSerial_nonSer = CHNLSVC.Inventory.Get_all_details_on_serialID(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, serID);
                        ////if (ddlItemStatus.SelectedValue != "--select--")
                        ////{

                        ////    _reptPickSerial_nonSer.Tus_new_status = ddlItemStatus.SelectedValue;
                        ////    //TODO:assign the new status-**done
                        ////    //assign to the TUS_NEW_ITM_STUS;
                        ////}

                        _reptPickSerial_nonSer.Tus_new_status = lblItemStatus.Text.Trim();//1 status always= CONSG
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
                        // _reptPickSerial_nonSer.Tus_new_status = itemStatus;
                        MasterItem mi = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
                        _reptPickSerial_nonSer.Tus_itm_desc = mi.Mi_shortdesc;
                        _reptPickSerial_nonSer.Tus_itm_model = mi.Mi_model;
                        //enter row into TEMP_PICK_SER
                        Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_nonSer, null);

                        rowCount++;
                       // isManualscan = true;

                    }

                }
                if (rowCount > 0)
                {   //create a dummy to represent the sum
                    ReptPickSerials reptPickSerial_dummy = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, "N/A");
                    reptPickSerial_dummy.Tus_usrseq_no = userseq_no;
                    reptPickSerial_dummy.Tus_ser_id = 0;
                   // reptPickSerial_dummy.Tus_ser_1 = "_";
                    reptPickSerial_dummy.Tus_ser_1 = GlbDummySerial1;
                    //reptPickSerial_dummy.Tus_new_status = ddlItemStatus.SelectedValue;
                    reptPickSerial_dummy.Tus_cre_by = GlbUserName;
                    MasterItem mi = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
                    reptPickSerial_dummy.Tus_itm_desc = mi.Mi_longdesc;
                    reptPickSerial_dummy.Tus_itm_model = mi.Mi_model;
                    reptPickSerial_dummy.Tus_qty = rowCount;
                   
                    reptPickSerial_dummy.Tus_com = GlbUserComCode;
                    reptPickSerial_dummy.Tus_loc = GlbUserDefLoca;
                    reptPickSerial_dummy.Tus_bin = lblPopupBinCode.Text;
                    reptPickSerial_dummy.Tus_itm_cd = itemCode;

                   // ReptPickSerials check_reptPickSerial_dummy = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, "_");
                    List<ReptPickSerials> temp_pick_ser = new List<ReptPickSerials>();
                    temp_pick_ser = CHNLSVC.Inventory.Get_TEMP_PICK_SER(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, 0, GlbDummySerial1, itemCode, binCode);
                    if (temp_pick_ser != null)
                    {
                        foreach (ReptPickSerials check_reptPickSerial_dummy in temp_pick_ser)//this temp_pick_ser list contains only 1 row
                        {

                            //// // int chek_dum_ser_id= check_reptPickSerial_dummy.Tus_ser_id;
                           
                            //reptPickSerial_dummy.Tus_qty = check_reptPickSerial_dummy.Tus_qty + rowCount;
                            // Update_temp_pick_serdummy
                             Decimal newQTY= check_reptPickSerial_dummy.Tus_qty + rowCount;
                             Boolean affectedrows = CHNLSVC.Inventory.Update_temp_pick_serdummy(GlbUserName, GlbUserComCode, GlbUserDefLoca, userseq_no, GlbDummySerial1, itemCode, binCode, newQTY);
                            // Boolean affected = CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, userseq_no, 0);//delete existing dummy
                            //delete existing dummy (delete only the dummy, not all non serials along with it)
                           // Boolean affectedrows = CHNLSVC.Inventory.Del_temp_pick_serdummy(GlbUserComCode, GlbUserDefLoca, userseq_no, 0, itemCode, binCode);//neeed to be modified(the sp. Add user,ser_1)

                        }

                    }
                    else
                        {
                         Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(reptPickSerial_dummy, null);
                        }
                    

                    //if (check_reptPickSerial_dummy != null)//if there is a dummy for this item already (need to increase the qty of dummy)
                    //{
                    //    //// // int chek_dum_ser_id= check_reptPickSerial_dummy.Tus_ser_id;

                    //    reptPickSerial_dummy.Tus_qty = check_reptPickSerial_dummy.Tus_qty + rowCount;
                    //    // Boolean affected = CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, userseq_no, 0);//delete existing dummy
                    //    Boolean affectedrows = CHNLSVC.Inventory.Del_temp_pick_serdummy(GlbUserComCode, GlbUserDefLoca, userseq_no, 0, itemCode, binCode);
                    //}
                    //else
                    //{
                    //    reptPickSerial_dummy.Tus_qty = rowCount;
                    //}
                    
                    //Add new dummy with updated QTY
                   

                   

                   
                }
                //to view in Serial grid
                //----add--
              //++  ////ReptPickSerials check_reptPickSerial_dummy = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, "_");
                ////if (check_reptPickSerial_dummy != null)//if there is a dummy for this item already (need to increase the qty of dummy)
                ////{
                //// // int chek_dum_ser_id= check_reptPickSerial_dummy.Tus_ser_id;

                ////   // check_reptPickSerial_dummy.Tus_qty = check_reptPickSerial_dummy.Tus_qty + rowCount;
                ////    ReptPickSerials reptPickSerial_dummy = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, "N/A");
                ////    reptPickSerial_dummy.Tus_usrseq_no = userseq_no;
                ////    reptPickSerial_dummy.Tus_ser_id = 0;
                ////    reptPickSerial_dummy.Tus_ser_1 = "_";
                ////    //reptPickSerial_dummy.Tus_new_status = ddlItemStatus.SelectedValue;
                ////    reptPickSerial_dummy.Tus_cre_by = GlbUserName;
                ////    MasterItem mi = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
                ////    reptPickSerial_dummy.Tus_itm_desc = mi.Mi_longdesc;
                ////    reptPickSerial_dummy.Tus_itm_model = mi.Mi_model;
                ////    reptPickSerial_dummy.Tus_qty = check_reptPickSerial_dummy.Tus_qty + rowCount;
                ////    //Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(reptPickSerial_dummy, null);
                ////    Boolean affected = CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, userseq_no, 0);//delete existing dummy
                ////    Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(reptPickSerial_dummy, null);//add new dummy

                ////}
                //++
                //else//no dummy exists 
                //{ }
                //----add--
                

                List<ReptPickSerials> AllserList = new List<ReptPickSerials>();
                AllserList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ-C");
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
                    GridViewSerials.DataSource = finalserList;
                    // GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ");
                    GridViewSerials.DataBind();

                    //~to view in Items grid
                    var items = finalserList.GroupBy(x => x.Tus_itm_cd).ToList();
                    List<ReptPickSerials> SummaryList = new List<ReptPickSerials>();
                    foreach (var s in items)
                    {
                        Int32 count = 0;
                        foreach (ReptPickSerials r in AllserList)
                        {
                            if (s.Key == r.Tus_itm_cd && r.Tus_ser_1 != GlbDummySerial1)
                            {
                                count++;
                                //ReptPickSerials rps = new ReptPickSerials();
                            }
                        }
                        ReptPickSerials rps = new ReptPickSerials();
                        MasterItem msit = new MasterItem();
                        msit = CHNLSVC.Inventory.GetItem(GlbUserComCode, s.Key);
                        rps.Tus_itm_cd = s.Key;
                        rps.Tus_itm_model = msit.Mi_model;
                        rps.Tus_itm_desc = msit.Mi_longdesc;
                        rps.Tus_qty = count;//changed
                        rps.Tus_itm_stus = "CONSG";
                        SummaryList.Add(rps);

                    }
                    GridViewItems.DataSource = SummaryList;
                    GridViewItems.DataBind();
                    //~
                   
                }
                else
                {
                    List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                    emptyGridList.Add(new ReptPickSerials());
                    GridViewSerials.DataSource = emptyGridList;
                    GridViewSerials.DataBind();

                }


            }
            else if (msitem.Mi_is_ser1 == 1)
            {
                foreach (GridViewRow gvr in this.GridPopup.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");
                    string serial_1 = (string)gvr.Cells[1].Text.Trim();

                    if (chkSelect.Checked)
                    {
                        // Boolean update = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serial_1, -1);
                        //-------------
                        ReptPickSerials _reptPickSerial = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, serial_1);
                        ////if (ddlItemStatus.SelectedValue != "--select--")
                        ////{
                        ////    //TODO:assign the new status
                        ////    _reptPickSerial.Tus_new_status = ddlItemStatus.SelectedValue;
                        ////    //assign to the TUS_NEW_ITM_STUS;
                        ////}

                        _reptPickSerial.Tus_new_status = lblItemStatus.Text.Trim();//2 status always= CONSG

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

                    }



                }
                //to view in Serials grid
                List<ReptPickSerials> AllserList = new List<ReptPickSerials>();
                AllserList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ-C");
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
                    GridViewSerials.DataSource = finalserList;
                    // GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ-C");
                    GridViewSerials.DataBind();


                    //view in Items grid
                    
                    //GridViewItems.DataSource = finalserList.GroupBy(x => x.Tus_itm_cd, x => x.Tus_bin).ToList();
                    //GridViewItems.DataBind();

                    var items = finalserList.GroupBy(x => x.Tus_itm_cd).ToList();
                    List<ReptPickSerials> SummaryList = new List<ReptPickSerials>();
                    foreach (var s in items)
                    {
                        Int32 count = 0;
                        foreach (ReptPickSerials r in AllserList)
                        {
                            if (s.Key == r.Tus_itm_cd && r.Tus_ser_1 != GlbDummySerial1)
                            {
                                count++;
                                //ReptPickSerials rps = new ReptPickSerials();
                            }
                        }
                        ReptPickSerials rps = new ReptPickSerials();
                        MasterItem msit = new MasterItem();
                        msit = CHNLSVC.Inventory.GetItem(GlbUserComCode, s.Key);
                        rps.Tus_itm_cd = s.Key;
                        rps.Tus_itm_model = msit.Mi_model;
                        rps.Tus_itm_desc = msit.Mi_longdesc;
                        rps.Tus_qty = count;
                        rps.Tus_itm_stus = "CONSG";
                        SummaryList.Add(rps);

                    }
                    GridViewItems.DataSource = SummaryList;
                    GridViewItems.DataBind();
                    //***********
                }
                else
                {
                    List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                    emptyGridList.Add(new ReptPickSerials());
                    GridViewSerials.DataSource = emptyGridList;
                    GridViewSerials.DataBind();

                }
                
            }
            txtPopupSearchSer.Text = null;
            isManualscan = true;
            
        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {
            txtPopupSearchSer.Text = null;
        }

        protected void btnPopupSarch_Click(object sender, EventArgs e)
        {
           if (ddlPopupSerial.SelectedValue == "Serial No 1")
            {
                string serial_no = txtPopupSearchSer.Text.Trim();
                //call query.
                string serch_serial = txtPopupSearchSer.Text.Trim() + "%";
                string bin = lblPopupBinCode.Text.Trim();
                serial_list = CHNLSVC.Inventory.Search_by_serial(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text.Trim(), bin, serch_serial, null);
            //    serial_list = CHNLSVC.Inventory.GET_ser_for_ItmCD_Supplier(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, supplierCD);
               List<ReptPickSerials> GridList = new List<ReptPickSerials>();
                foreach(ReptPickSerials r in serial_list)
                {
                    if (r.Tus_orig_supp.Trim() == txtSupplierCd.Text.Trim())
                    {
                        GridList.Add(r);
                    }
                    
                }
                GridPopup.DataSource = GridList;
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
                List<ReptPickSerials> GridList = new List<ReptPickSerials>();
                foreach (ReptPickSerials r in serial_list)
                {
                    if (r.Tus_orig_supp.Trim() == txtSupplierCd.Text.Trim())
                    {
                        GridList.Add(r);
                    }

                }
                GridPopup.DataSource = GridList;
                
                GridPopup.DataBind();

                ModalPopupExtItem.Show();
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select serial type from drop down!");

            }
        }

        protected void btnPopupAutoSelect_Click(object sender, EventArgs e)
        {
           
           // if (txtPopupQty.Text != null || txtPopupQty.Text != "" || serial_list != null)
            if (!string.IsNullOrEmpty(txtPopupQty.Text) && serial_list != null)
            {
                //try
                //{
                //    Decimal _userSeqNo_ = Convert.ToDecimal(txtPopupQty.Text);
                //}
                //catch (Exception ex)
                //{
                //    return;
                //}
                Int32 qty = Convert.ToInt32((txtPopupQty.Text));

               
                if (qty > serial_list.Count)
                {
                    lblPopupAmt.Text = "Qty in hand is " + serial_list.Count + ". You can't exceed qty!";
                    txtPopupQty.Text = "0";
                    
                    String script = @"<script language=""Javascript"">
                        function TestJScript()
                        {
                        alert('Just for testing');
                        }
                        </script>";
                    
                    //System.Web.UI.Page.RegisterClientScriptBlock("experiment", script);
                    Page.RegisterClientScriptBlock("uncheking", script);
                      btnPopupAutoSelect.Attributes.Add("onClick", "TestJScript()");


                }
            }

            ModalPopupExtItem.Show();
        }

        //protected void btnAddSearch_Click(object sender, EventArgs e)
        //{
        //    ModalPopupExtItem.Show(); 
        //}



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
            MasterCommonSearchUCtrl.ReturnResultControl = txtItemCd.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        protected void ImageBtnSupplier_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
            DataTable dataSource = CHNLSVC.CommonSearch.GetSupplierData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtSupplierCd.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        protected void btnAddSearch_Click(object sender, EventArgs e)
        {
            int user_seq_no = -1;  
            string supplierCD = txtSupplierCd.Text.Trim();
            
            if (supplierCD == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter Supplier code!");
                return;
            }
            if (ddlBinCode.SelectedIndex == 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select Bin code!");
                return;
            }
            else if (txtItemCd.Text == ""||txtItemCd.Text==null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select Item code!");
                return;
            }

            string binCode = ddlBinCode.SelectedValue.ToString();
            string itemCode = txtItemCd.Text;
           // string itemStatus = ddlItemStatus.SelectedValue.ToString();
           // Decimal qty = 1; //Convert.ToDecimal(txtQty.Text);
            string item_description = lblDiscription.Text;

            lblPopupItemCode.Text = txtItemCd.Text;
            lblPopupBinCode.Text = ddlBinCode.SelectedValue;

           // MasterItem msitem = new MasterItem();
            MasterItem msitem = null;
            msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
            if (msitem==null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter valid item code!");
                return;
            }
            if (msitem.Mi_is_ser1 == 0)//check whether it is a non-sirialized item
            {
                lblPopQty.Visible = true;
                txtPopupQty.Visible = true;
            }
            else
            {
                lblPopQty.Visible = true;
                txtPopupQty.Visible = true;
            }
           // serial_list = CHNLSVC.Inventory.Get_all_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca, binCode, itemCode);
            serial_list = CHNLSVC.Inventory.GET_ser_for_ItmCD_Supplier(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, supplierCD);
            
            GridPopup.DataSource = serial_list;
            GridPopup.DataBind();
            if (serial_list != null)
            {
                lblPopupAmt.Text = "Qty in hand: " + serial_list.Count.ToString();
            }
            else
            {
                lblPopupAmt.Text = "Qty in hand: " + "0";
            }
            txtPopupSearchSer.Text = null;
            ModalPopupExtItem.Show(); 
            
        }

        protected void btnSerchSer1_Click(object sender, EventArgs e)
        {
            string serial1 = txtSerial1.Text.Trim();
            string supplierCD = txtSupplierCd.Text.Trim();
            if (supplierCD=="")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter Supplier code!");
                return;
            }
            DataTable dt_serchSerial = new DataTable();
            try
            {
                dt_serchSerial = CHNLSVC.Inventory.getDetail_on_serial_Supplier(GlbUserComCode, GlbUserDefLoca, supplierCD, serial1, null);//this returns table of INR_SER
                if (dt_serchSerial.Rows.Count < 1)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No item available with this serial number under the supplier!");
                    return;
                }
                if (dt_serchSerial.Rows.Count == 1)
                {
                    foreach (DataRow r in dt_serchSerial.Rows)
                    {
                        // Get the value of the wanted column and cast it to string
                        //from INR_SER
                        string bin_code = (string)r["INS_BIN"];
                        Decimal unitCost = (Decimal)r["INS_UNIT_COST"];
                        string itemCode = (string)r["INS_ITM_CD"];
                        string itemStatus = (string)r["INS_ITM_STUS"];
                        string itemLocation = Convert.ToString(r["INS_LOC"]);
                        if (System.DBNull.Value != r["INS_SER_2"])
                        {
                            string serial2 = (string)r["INS_SER_2"];
                            txtSerial2.Text = serial2;
                        }

                        ddlBinCode.SelectedValue = bin_code;
                        txtItemCd.Text = itemCode;
                       // ddlItemStatus.SelectedValue = itemStatus;
                        MasterMsgInfoUCtrl.Clear();

                        lblDiscription.Text =" Current Status:" + itemStatus + ", " + "Bin:" + bin_code + " Location:" + itemLocation;
                    }

                }
                else if (dt_serchSerial.Rows.Count > 1)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "More than one item exist for this serial number!");
                    return;
                }
            }
            catch (Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Sorry, cannot proceed with this serial number!");
                return;
            }
        }

        protected void ddlScanBatches_SelectedIndexChanged(object sender, EventArgs e)
        {      //added on 5-6-2012
            txtSupplierCd.Text="";
            txtSerial1.Text="";
            txtSerial2.Text="";
            txtItemCd.Text="";
            lblDiscription.Text = "";
      
           
            //generated_seq =-1;

            ////////////////////////////
            if (ddlScanBatches.SelectedIndex == 0)
            {
                generated_seq = -1;
                isManualscan = true;
                //btnScanPick.Enabled = true;
                //Panel_itemDesc.Enabled = true;
                //btnScanPick.Text = "Scan";
                //show empty line
                List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                emptyGridList.Add(new ReptPickSerials());
                GridViewSerials.DataSource = emptyGridList;
                GridViewSerials.DataBind();
                lblSeqno.Text = "";
               
                // GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, -1, "ADJ-C");
                // GridViewChanged_items.DataBind();
                return;
            }
            else if (ddlScanBatches.SelectedIndex != 0)
            {   //if the user wants to view scanned batch in grid
                //Panel_itemDesc.Enabled = true;
                //btnScanPick.Text = "Pick";
                isManualscan = false;
                //btnScanPick.Enabled = false;
                int seq_num = int.Parse(ddlScanBatches.SelectedValue);

                //to view in grid
                List<ReptPickSerials> AllserList = new List<ReptPickSerials>();
               AllserList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, seq_num, "ADJ-C");
                AllserList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, seq_num, "ADJ-C");
                List<ReptPickSerials> finalserList = new List<ReptPickSerials>();
                if (AllserList != null)
                {
                    foreach (ReptPickSerials rps in AllserList)
                    {
                        if (rps.Tus_ser_1 != "N/A" )
                        {
                            finalserList.Add(rps);
                        }
                    }
                    list_GetAllScanSerialsList = finalserList;
                    //list_ReptPickSerialsSubList = CHNLSVC.Inventory.GetAllScanSubSerialsList(seq_num, "ADJ");
                    list_ReptPickSerialsSubList = CHNLSVC.Inventory.GetAllScanSubSerialsList(seq_num, "ADJ-C");
                    if (list_GetAllScanSerialsList != null)
                    {
                        list_NEWGetAllScanSerialsList = list_GetAllScanSerialsList;
                        GridViewSerials.DataSource = list_NEWGetAllScanSerialsList;
                        GridViewSerials.DataBind();

                        //finalserList
                        var items = finalserList.GroupBy(x => x.Tus_itm_cd).ToList();
                        
                        List<ReptPickSerials> SummaryList = new List<ReptPickSerials>();
                        foreach (var s in items)
                        {
                            Int32 count = 0;
                            foreach (ReptPickSerials r in AllserList)
                            {
                                if (s.Key == r.Tus_itm_cd  && r.Tus_ser_1!=GlbDummySerial1)
                                {
                                    count++;
                                    //ReptPickSerials rps = new ReptPickSerials();
                                }
                            }
                            ReptPickSerials rps = new ReptPickSerials();
                            MasterItem msitem = new MasterItem();
                            msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, s.Key);
                            rps.Tus_itm_cd = s.Key;
                            rps.Tus_itm_model = msitem.Mi_model;
                            rps.Tus_itm_desc = msitem.Mi_longdesc;
                            rps.Tus_qty = count;
                            rps.Tus_itm_stus = "CONSG";
                            SummaryList.Add(rps);
                            
                        }
                        GridViewItems.DataSource = SummaryList;
                        GridViewItems.DataBind();
                        
                    }
                    else
                    {
                        List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                        emptyGridList.Add(new ReptPickSerials());
                        GridViewSerials.DataSource = emptyGridList;
                        GridViewSerials.DataBind();

                        GridViewItems.DataSource = emptyGridList;
                        GridViewItems.DataBind();
                        TabContainer1.ActiveTabIndex = 0;
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No records found!");
                    }
                }
                else
                {
                    list_GetAllScanSerialsList = AllserList;
                 
                   // list_ReptPickSerialsSubList = CHNLSVC.Inventory.GetAllScanSubSerialsList(seq_num, "ADJ");
                    list_ReptPickSerialsSubList = CHNLSVC.Inventory.GetAllScanSubSerialsList(seq_num, "ADJ-C");
                    if (list_GetAllScanSerialsList != null)
                    {
                        list_NEWGetAllScanSerialsList = list_GetAllScanSerialsList;
                        GridViewSerials.DataSource = list_NEWGetAllScanSerialsList;
                        GridViewSerials.DataBind();
                    }
                    else
                    {
                        List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                        emptyGridList.Add(new ReptPickSerials());
                        GridViewSerials.DataSource = emptyGridList;
                        GridViewSerials.DataBind();
                        GridViewItems.DataSource = emptyGridList;
                        GridViewItems.DataBind();
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No records found!");
                    }
                }
               
            }
            
        }

        //protected void Button4_Click(object sender, EventArgs e)
        //{

        //}

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string supplierCD = txtSupplierCd.Text.Trim();

            if (supplierCD == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter Supplier code!");
                return;
            }
            if (ddlBinCode.SelectedIndex == 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select Bin code!");
                return;
            }
            else if (txtItemCd.Text == "" || txtItemCd.Text == null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select Item code!");
                return;
            }

            MasterItem mstitem = null;
            mstitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItemCd.Text.Trim());
            if (mstitem == null)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select valid Item code!");
                return;
            }

            int userseq_no = -1;
            string serial_1 = txtSerial1.Text.Trim();
            string serial_2 = txtSerial2.Text.Trim();
            string binCode = ddlBinCode.SelectedValue.ToString();
            string itemCode = txtItemCd.Text.Trim();
            string itemStatus = lblItemStatus.Text.Trim();
            Decimal qty = 1;//Convert.ToDecimal(txtQty.Text);
            ReptPickSerials _reptPickSerial = new ReptPickSerials();
            List<ReptPickSerials> ser_list = new List<ReptPickSerials>();
            string item_description = lblDiscription.Text;

            if (ddlScanBatches.SelectedIndex != 0) //if the user wants to add to an existing batch
            {
                userseq_no = Convert.ToInt32(ddlScanBatches.SelectedValue);
            }

            else//if the user wants to add to new batch
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
                RPH.Tuh_doc_tp = "ADJ-C";
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
                seqNumList_out = CHNLSVC.Inventory.GetAll_Adj_SeqNums_for_user(GlbUserName, "ADJ-C", 0, GlbUserComCode);// OUT
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
                    GlbSerialScanDocumentType = "ADJ-C";
                    GlbSerialScanDirection = 0;
                    GlbSerialScanReturnUrl = HttpContext.Current.Request.Url.AbsoluteUri;

                    //finally redirect to scanSerial page
                }

            }

            MasterItem msitem = new MasterItem();
            msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
            

            if (msitem.Mi_is_ser1 == 0)//check whether it is a non-sirialized item
            {
                lblPopQty.Visible = true;
                txtPopupQty.Visible = true;
                lblPopupItemCode.Text = txtItemCd.Text.Trim();
                lblPopupBinCode.Text = lblPopupBinCode.Text;
               // serial_list = CHNLSVC.Inventory.Get_all_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca, binCode, itemCode);
                serial_list = CHNLSVC.Inventory.GET_ser_for_ItmCD_Supplier(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, supplierCD);
                GridPopup.DataSource = serial_list;
                GridPopup.DataBind();
                if (serial_list != null)
                {
                    lblPopupAmt.Text = "Qty in hand: " + serial_list.Count.ToString();
                }
                else
                {
                    lblPopupAmt.Text = "Qty in hand: " + "0";
                }
                txtPopupSearchSer.Text = null;
                ModalPopupExtItem.Show();

            }
            else //it is a sirialized item
            {
                if (serial_1 == null || serial_1 == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter Serial No1 number first!");
                    return;
                }
                else
                {
                    if (ddlBinCode.SelectedIndex == 0)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select Bin code!");
                        return;
                    }
                    else if (txtItemCd.Text =="")
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select Item code!");
                        return;
                    }
                    _reptPickSerial = CHNLSVC.Inventory.Get_all_details_on_serial(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, serial_1); //==>
                   
                    ser_list.Add(_reptPickSerial);
                }

                // DateTime date = Convert.ToDateTime(txtDate.Text);
                // string Manual_ref = txtManualRefNo.Text;
                // string remarks = txtRemarks.Text;
                lblPopQty.Visible = false;
                txtPopupQty.Visible = false;


                if (_reptPickSerial.Tus_ser_1 == null)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter valid Serial Number for the Supplier!");
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

                //Update_inrser_INS_AVAILABLE
                Boolean update = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serial_1, -1);
                //enter row into TEMP_PICK_SER
                Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial, null);

                isManualscan = true;
                reptPickSerials_list.Add(_reptPickSerial);
               
                 //to view in Serials grid
                List<ReptPickSerials> AllserList = new List<ReptPickSerials>();
                AllserList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ-C");
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
                    GridViewSerials.DataSource = finalserList;
                    // GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ-C");
                    GridViewSerials.DataBind();

                    //view in Items grid

                    //GridViewItems.DataSource = finalserList.GroupBy(x => x.Tus_itm_cd, x => x.Tus_bin).ToList();
                    var items = finalserList.GroupBy(x => x.Tus_itm_cd).ToList();
                    List<ReptPickSerials> SummaryList = new List<ReptPickSerials>();
                    foreach (var s in items)
                    {
                        Int32 count = 0;
                        foreach (ReptPickSerials r in finalserList)
                        {
                            if (s.Key == r.Tus_itm_cd)
                            {
                                count++;
                            }
                        }
                        ReptPickSerials rps = new ReptPickSerials();
                        MasterItem msit = new MasterItem();
                        msit = CHNLSVC.Inventory.GetItem(GlbUserComCode, s.Key);
                        rps.Tus_itm_cd = s.Key;
                        rps.Tus_itm_model = msit.Mi_model;
                        rps.Tus_itm_desc = msit.Mi_longdesc;
                        rps.Tus_qty = count;
                        rps.Tus_itm_stus = "CONSG";
                        SummaryList.Add(rps);
                    }
                    GridViewItems.DataSource = SummaryList;
                    GridViewItems.DataBind();
                }
                else { 
                
                    //show a empty row in both grid veiws.
                }

                //clear the panel
                txtSupplierCd.Text = null;
                txtSerial1.Text=null;
                txtSerial2.Text = null;
                ddlBinCode.SelectedIndex = 0;
                txtItemCd.Text = null;
                lblDiscription.Text = null;
            }

        }

        protected void btnSerchSer2_Click(object sender, EventArgs e)
        {
            string serial_2 = txtSerial2.Text.Trim();
            string supplierCD = txtSupplierCd.Text.Trim();
            if (supplierCD == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please enter Supplier code!");
                return;
            }
            DataTable dt_serchSerial = new DataTable();
            try
            {
                dt_serchSerial = CHNLSVC.Inventory.getDetail_on_serial_Supplier(GlbUserComCode, GlbUserDefLoca, supplierCD, null, serial_2);//this returns table of INR_SER
                if (dt_serchSerial.Rows.Count < 1)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No item available with this serial number under the supplier!");
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
                        txtItemCd.Text = itemCode;
                        // ddlItemStatus.SelectedValue = itemStatus;
                        MasterMsgInfoUCtrl.Clear();

                        lblDiscription.Text =  " Current Status:" + itemStatus + ", " + "Bin:" + bin_code;
                    }

                }
                else if (dt_serchSerial.Rows.Count > 1)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "More than one item exist for this serial number!");
                    return;
                }
            }
            catch (Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Sorry, cannot proceed with this serial number!");
                return;
            }
        }

        protected void btnSerialDel_Click(object sender, EventArgs e)
        {
            //delete all cheked items from the list.(remove from grid)
                foreach (GridViewRow gvr in this.GridViewSerials.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("SelectCheck");
                   
                   // Int32 usrseqNo = Convert.ToInt32(gvr.Cells[11].Text);
                    Label lblusrseqNo = (Label)gvr.Cells[11].FindControl("UserSeqNo");
                    Int32 usrseqNo = Convert.ToInt32(lblusrseqNo.Text);

                    //Int32 ser_id = Convert.ToInt32(gvr.Cells[12].Text);
                    Label serID = (Label)gvr.Cells[12].FindControl("Serial_ID_");
                    Int32 ser_id = Convert.ToInt32(serID.Text);
                   // Int32 ser_id = Convert.ToInt32(gvr.Cells[12].Text);
                    string serial_1 = gvr.Cells[1].Text;
                    
                    string itemCode = gvr.Cells[3].Text;
                    string bincode = gvr.Cells[4].Text;
                    if (chkSelect.Checked)
                    {
                        //if (serial_1 == "_")
                        if (serial_1 == GlbDummySerial1)
                        {
                            List<ReptPickSerials> NA_list = new List<ReptPickSerials>();

                            NA_list = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, usrseqNo, "ADJ-C");
                            if (NA_list != null)
                            {
                                foreach (ReptPickSerials r in NA_list)
                                {
                                    if (r.Tus_bin == bincode && r.Tus_usrseq_no == usrseqNo && r.Tus_ser_1 == "N/A" && r.Tus_itm_cd == itemCode)
                                    {
                                        Int32 serialID = r.Tus_ser_id;
                                        Boolean update_av = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serialID, 1);
                                        // Boolean update = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serial_1, 1);
                                    }
                                }
                            }

                            //delete both dummy and relevent rows in temp_pick_ser
                            Boolean affectedrows = CHNLSVC.Inventory.Del_temp_pick_serdummy(GlbUserComCode, GlbUserDefLoca, usrseqNo, ser_id, itemCode, bincode);
                        }
                        else
                        {
                            //remove from TEMP_PICK_SER
                            Boolean affected = CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, usrseqNo, ser_id);
                        }
                        //remove from TEMP_PICK_SER
                        // Boolean affected = CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, usrseqNo, ser_id);
                        //Update_inrser_INS_AVAILABLE
                        Boolean update = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serial_1, 1);
                    }
                   
                    //to view in grid
                    List<ReptPickSerials> AllserList = new List<ReptPickSerials>();
                    AllserList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, usrseqNo, "ADJ-C");
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
                        GridViewSerials.DataSource = finalserList;
                        // GridViewChanged_items.DataSource = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, userseq_no, "ADJ");
                        GridViewSerials.DataBind();

                        //view in Items grid

                        //GridViewItems.DataSource = finalserList.GroupBy(x => x.Tus_itm_cd, x => x.Tus_bin).ToList();
                        if (finalserList != null || finalserList.Count != 0)//items are in the list
                        {
                            var items = finalserList.GroupBy(x => x.Tus_itm_cd).ToList();
                            List<ReptPickSerials> SummaryList = new List<ReptPickSerials>();
                            foreach (var s in items)
                            {
                                Int32 count = 0;
                                foreach (ReptPickSerials r in finalserList)
                                {
                                    if (s.Key == r.Tus_itm_cd)
                                    {
                                        count++;
                                    }
                                }
                                ReptPickSerials rps = new ReptPickSerials();
                                MasterItem msit = new MasterItem();
                                msit = CHNLSVC.Inventory.GetItem(GlbUserComCode, s.Key);
                                rps.Tus_itm_cd = s.Key;
                                rps.Tus_itm_model = msit.Mi_model;
                                rps.Tus_itm_desc = msit.Mi_longdesc;
                                rps.Tus_qty = count;
                                rps.Tus_itm_stus = "CONSG";
                                SummaryList.Add(rps);
                            }
                            GridViewItems.DataSource = SummaryList;
                            GridViewItems.DataBind();
                        }
                        else {
                            List<ReptPickSerials> SummaryList = new List<ReptPickSerials>();
                            ReptPickSerials rps = new ReptPickSerials();
                            //MasterItem msit = new MasterItem();
                            //msit = CHNLSVC.Inventory.GetItem(GlbUserComCode, s.Key);
                            rps.Tus_itm_cd = "";
                            rps.Tus_itm_model = "";
                            rps.Tus_itm_desc = "";
                            rps.Tus_qty =0;
                            rps.Tus_itm_stus = "";
                            SummaryList.Add(rps);

                            GridViewItems.DataSource = SummaryList;
                            GridViewItems.DataBind();
                        }
                      
                    }
                    else //if finalserList ==null
                    {
                        List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                        emptyGridList.Add(new ReptPickSerials());
                        GridViewSerials.DataSource = emptyGridList;
                        GridViewSerials.DataBind();

                        List<ReptPickSerials> SummaryList_empty = new List<ReptPickSerials>();
                        ReptPickSerials rps = new ReptPickSerials();

                        rps.Tus_itm_cd = "";
                        rps.Tus_itm_model = "";
                        rps.Tus_itm_desc = "";
                        rps.Tus_qty = 0;
                        rps.Tus_itm_stus = "";
                        SummaryList_empty.Add(rps);
                        GridViewItems.DataSource = SummaryList_empty;
                        GridViewItems.DataBind();
                    }
                    
                }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            

           

            try
            {
                Int32 _userSeqNo_ = Convert.ToInt32(ddlScanBatches.SelectedValue);
            }
            catch (Exception ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Select batch to save!");
                return;
            }
            Int32 _userSeqNo=Convert.ToInt32(ddlScanBatches.SelectedValue);

            List<ReptPickSerials> PickSerialList = new List<ReptPickSerials>();
            PickSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, _userSeqNo, "ADJ-C");
            if (PickSerialList == null || PickSerialList.Count==0)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cannot Save empty batch!");
                return;
            }
            
            InventoryHeader _invHeader = new InventoryHeader();
                _invHeader.Ith_com = GlbUserComCode;
                _invHeader.Ith_loc = GlbUserDefLoca;

                try
                {
                        DateTime frmdt = Convert.ToDateTime(txtDate.Text);
                }
                catch (Exception ex)
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Date format is incorrec!");
                    return;
                }
                DateTime _docDate = Convert.ToDateTime(txtDate.Text);
                _invHeader.Ith_doc_date = _docDate;
                _invHeader.Ith_doc_year = _docDate.Year;
                _invHeader.Ith_direct = true;
                _invHeader.Ith_doc_tp = "ADJ";
                _invHeader.Ith_cate_tp = "CONSIGN";
                //_invHeader.Ith_sub_tp = "CONSIGN";
                //_invHeader.Ith_bus_entity = lblSupplierCode.Text;
                _invHeader.Ith_is_manual = true;
                _invHeader.Ith_manual_ref = txtManualRefNo.Text;
                _invHeader.Ith_remarks = txtRemarks.Text;
                _invHeader.Ith_stus = "A";
                _invHeader.Ith_cre_by = GlbUserName;
                _invHeader.Ith_cre_when = DateTime.Now;
                _invHeader.Ith_mod_by = GlbUserName;
                _invHeader.Ith_mod_when = DateTime.Now;
                _invHeader.Ith_session_id = GlbUserSessionID;
                //_invHeader.Ith_oth_docno = lblRequestNo.Text;

            List<ReptPickSerials> _reptPickSerialList=new List<ReptPickSerials>();
             _reptPickSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, _userSeqNo, "ADJ-C");
           
            List<ReptPickSerialsSub> _reptPickSerialsSub=new List<ReptPickSerialsSub>();
            _reptPickSerialsSub=CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "ADJ-C");
            
              MasterAutoNumber _masterAuto = new MasterAutoNumber();
                _masterAuto.Aut_cate_cd = GlbUserDefLoca;
                _masterAuto.Aut_cate_tp = "LOC";
                _masterAuto.Aut_direction = null;
                _masterAuto.Aut_modify_dt = null;
                _masterAuto.Aut_moduleid = "CONS";
                _masterAuto.Aut_number = 0;
                _masterAuto.Aut_start_char = "CONS";
                _masterAuto.Aut_year = null;


            string DocNo = "";
            Int32 r=-99;
            r = CHNLSVC.Inventory.ConsignmentReturn(_invHeader, _reptPickSerialList, _reptPickSerialsSub, _masterAuto, out DocNo);
            
           if (r!=-99)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Saved! Consignment Return Document No is: " + DocNo);
            }
            else
            {
              MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Not Saved! ");
            }

           //added on 5-6-2012----clear the screen.
           txtSupplierCd.Text = "";
           txtSerial1.Text = "";
           txtSerial2.Text = "";
           txtItemCd.Text = "";
           lblDiscription.Text = "";

           isManualscan = true;
           //generated_seq =-1;
           seqNumList_out = CHNLSVC.Inventory.GetAll_Adj_SeqNums_for_user(GlbUserName, "ADJ-C", 0, GlbUserComCode);//for OUT(status changes comes as outs)
           seqNumList_out[0] = "New Scan";
           ddlScanBatches.DataSource = seqNumList_out;
           ddlScanBatches.DataBind();

           List<string> bincode_list = new List<string>();
           bincode_list = CHNLSVC.Inventory.GetAll_binCodes_for_loc(GlbUserComCode, GlbUserDefLoca);

           ddlBinCode.DataSource = bincode_list;
           ddlBinCode.DataBind();

           isManualscan = true;
           //generated_seq =-1;
           seqNumList_out = CHNLSVC.Inventory.GetAll_Adj_SeqNums_for_user(GlbUserName, "ADJ-C", 0, GlbUserComCode);//for OUT(status changes comes as outs)
           seqNumList_out[0] = "New Scan";
           ddlScanBatches.DataSource = seqNumList_out;
           ddlScanBatches.DataBind();

           List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
           emptyGridList.Add(new ReptPickSerials());
           GridViewSerials.DataSource = emptyGridList;
           GridViewSerials.DataBind();

           GridViewItems.DataSource = emptyGridList;
           GridViewItems.DataBind();
            ///////////clear the screen/////////////////
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Inventory_Module/ConsignmentRN.aspx");
        }

        protected void btnValidate_Supplier_Click(object sender, EventArgs e)
        {
            List<MasterBusinessEntity> suppliersList = CHNLSVC.Inventory.GetServiceAgent("S");
            if (suppliersList != null || suppliersList.Count == 0)
            {
                var supcd = (from sp in suppliersList
                             where sp.Mbe_cd == txtSupplierCd.Text.Trim()
                             select sp).Count();
                if (supcd < 1)
                {
                    txtSupplierCd.Text = "";
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Valid Supplier code!");
                    return;
                }

            }
        }

        private void OnBlur()
        {
            txtSupplierCd.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnValidate_Supplier, ""));
        
        }
    }

}
