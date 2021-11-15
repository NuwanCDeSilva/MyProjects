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
using System.Text;
using System.Transactions;

namespace FF.WebERPClient
{
    public partial class InvStockAdjustment : BasePage
    {
        protected List<ReptPickSerialsSub> list_ReptPickSerialsSubList
        {
            get { return (List<ReptPickSerialsSub>)Session["list_ReptPickSerialsSubList"]; }
            set { Session["list_ReptPickSerialsSubList"] = value; }
        }
        protected List<ReptPickSerials> list_GetAllScanSerialsList
        {
            get { return (List<ReptPickSerials>)Session["list_GetAllScanSerialsList"]; }
            set { Session["list_GetAllScanSerialsList"] = value; }
        }
        protected List<ReptPickItems> ScanItemList
        {
            get { return (List<ReptPickItems>)Session["ScanItemList"]; }
            set { Session["ScanItemList"] = value; }
        }
        protected List<string> seqNumList
        {
            get { return (List<string>)Session["seqNumList"]; }
            set { Session["seqNumList"] = value; }
        }
        protected List<string> seqNumList_out
        {
            get { return (List<string>)Session["seqNumList_out"]; }
            set { Session["seqNumList_out"] = value; }
        }
        protected Int32 UserSeqNo
        {
            get { return (Int32)Session["UserSeqNo"]; }
            set { Session["UserSeqNo"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                list_ReptPickSerialsSubList = new List<ReptPickSerialsSub>();
                list_GetAllScanSerialsList = new List<ReptPickSerials>();
                ScanItemList = new List<ReptPickItems>();
                seqNumList = new List<string>();
                seqNumList_out = new List<string>();
                UserSeqNo = 0;

                gridShowAdjustedData.DataSource = list_GetAllScanSerialsList;
                gridShowAdjustedData.DataBind();

                gvItem.DataSource = ScanItemList;
                gvItem.DataBind();

                List<string> sl = new List<string>();
                sl.Add("--select--");

                sl = CHNLSVC.Inventory.GetAll_Adj_SubTypes();
                sl.Add("");

                ddlAdjSubTyepe.DataSource = sl;
                ddlAdjSubTyepe.DataBind();
                ddlAdjSubTyepe.SelectedValue = "";


                DateTime thisDate = DateTime.Now;
                DateTime date = new DateTime(thisDate.Year, thisDate.Month, thisDate.Day);
                CultureInfo culture = new CultureInfo("pt-BR");
                Console.WriteLine(thisDate.ToString("d", culture));
                txtDate_.Text = thisDate.ToString("d", culture);

                ddlAdjBased.SelectedIndex = 0;

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
                //  ddlScanBatches.Items.Insert(0, new ListItem("Please select a country", ""));
                //added on 1-6-2012
                if (GlbSerialScanUserSeqNo <= 0)
                {
                    ddlScanBatches.SelectedValue = GlbSerialScanUserSeqNo.ToString();
                }
                txtItem.Attributes.Add("onKeyup", "return clickButton(event,'" + imgBtnItem.ClientID + "')");
                txtItem.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnItem, ""));
            }
            if (ddlScanBatches.SelectedIndex != 0)
            {
                btnSerialScan.Visible = false;
            }

        }

        #region Common Searching Area
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
            MasterCommonSearchUCtrl.ReturnResultControl = txtItem.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        #endregion

        #region Other Old Function and Procedures
        //EDITED
        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlScanBatches.SelectedIndex == 0)
            {
                btnSerialScan.Visible = true;
                ddlAdjType_.Enabled = true;
                ddlAdjType_.SelectedIndex = 2;
                return;
            }
            else if (ddlScanBatches.SelectedIndex != 0)
            {
                btnSerialScan.Visible = false;
                ddlAdjType_.Enabled = false;
            }
            UserSeqNo = int.Parse(ddlScanBatches.SelectedValue);


            list_GetAllScanSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, UserSeqNo, "ADJ");
            list_ReptPickSerialsSubList = CHNLSVC.Inventory.GetAllScanSubSerialsList(UserSeqNo, "ADJ");
            ScanItemList = CHNLSVC.Inventory.GetAllScanRequestItemsList(UserSeqNo);


            gridShowAdjustedData.DataSource = list_GetAllScanSerialsList;
            gridShowAdjustedData.DataBind();

            gvItem.DataSource = ScanItemList;
            gvItem.DataBind();


            string msg_selectedValue = "value=" + ddlScanBatches.SelectedValue;
            lblBatchSeq.Text = msg_selectedValue;
            if (seqNumList_out.Count > 0)
            {
                ddlAdjType_.SelectedIndex = 0;
                lblBatchSeq.Text = msg_selectedValue + "->IN";
                lblDirect.Text = "IN:" + ddlScanBatches.SelectedValue;
                foreach (string sn in seqNumList_out)
                {
                    if (ddlScanBatches.SelectedValue.ToString() == sn)
                    {
                        ddlAdjType_.SelectedIndex = 1;
                        lblBatchSeq.Text = msg_selectedValue + "->OUT";
                        lblDirect.Text = "OUT:" + ddlScanBatches.SelectedValue;
                    }
                    else
                    {
                    }
                }
            }
            else
            {
                ddlAdjType_.SelectedIndex = 0;
                lblBatchSeq.Text = msg_selectedValue + "->IN";
                lblDirect.Text = "IN:" + ddlScanBatches.SelectedValue;
            }

            DateTime thisDate = DateTime.Now;
            DateTime date = new DateTime(thisDate.Year, thisDate.Month, thisDate.Day);
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

        //EDITED
        protected void btnSerialScan_Click(object sender, EventArgs e)
        {
            if (ddlScanBatches.SelectedIndex != 0)
            { return; }
            if (ddlAdjType_.SelectedIndex == 2)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select Adjustment Type!");
                return;
            }

            int generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "ADJ", 1, GlbUserComCode);

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
                    UserSeqNo = generated_seq;
            }
            else if (ddlAdjType_.SelectedValue == "- ADJ")
            {
                RPH.Tuh_direct = false;
                lblNewScanDirect.Text = "out";
                //write entry to TEMP_PICK_HDR
                int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
                if (affected_rows > 0)
                    UserSeqNo = generated_seq;
            }

            seqNumList = CHNLSVC.Inventory.GetAll_Adj_SeqNums_for_user(GlbUserName, "ADJ", 1, GlbUserComCode);//for IN 
            seqNumList_out = CHNLSVC.Inventory.GetAll_Adj_SeqNums_for_user(GlbUserName, "ADJ", 0, GlbUserComCode);//for OUT
            foreach (string st in seqNumList_out)
            {
                if (st != "")
                    seqNumList.Add(st);
            }
            ddlScanBatches.DataSource = seqNumList;
            ddlScanBatches.DataBind();
            ddlScanBatches.SelectedValue = Convert.ToString(UserSeqNo);
            DropDownList3_SelectedIndexChanged(null, null);

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlAdjType_.Enabled = true;
            // btnSerialScan.Visible = true;
            //Panel1.Visible = true;
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

            //Panel1.Enabled = false;
            //Panel1.Visible = true;
            //btnSerialScan.Visible = false;
            btnSerialScan.Visible = true;
            ddlAdjType_.Enabled = true;
            ddlAdjType_.SelectedIndex = 2;

            ddlScanBatches.SelectedIndex = 0;
            Response.Redirect("~/Inventory_Module/StockAdjustment.aspx");
        }

        protected void btnFinalSave_Click(object sender, EventArgs e)
        {

            if (ddlAdjSubTyepe.SelectedValue == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Select Adjustment sub type!");
                return;
            }

            if (list_GetAllScanSerialsList == null || list_GetAllScanSerialsList.Count < 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Cannot save empty scanned batch!");
                return;
            }

            string AdjNumber = "";
            string manualNum = txtManualRefNo.Text.Trim();
            string remarks = txtRemarks.Text.Trim();
            string adj_base = ddlAdjBased.SelectedValue;
            string adj_sub_type = ddlAdjSubTyepe.SelectedValue;
            string adj_type = ddlAdjType_.SelectedValue;
            InventoryHeader inHeader = new InventoryHeader();


            inHeader.Ith_acc_no = "";
            inHeader.Ith_anal_1 = "";
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
            inHeader.Ith_com_docno = "";
            inHeader.Ith_cre_by = GlbUserName;
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
            inHeader.Ith_doc_no = string.Empty;

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
            else
            {
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
                //Panel1.Enabled = false;
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
                //Panel1.Enabled = false;
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
            if (ddlAdjSubTyepe.SelectedValue.Trim() == "")
            {
                lblSubTpDesc.Text = "";
            }
        }

        //ADDED BY SACHITH

        protected void DropDownListItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListStatus.Items.Clear();
            DropDownListStatus.Items.Add(new ListItem("", "-1"));
            DropDownListStatus.DataSource = CHNLSVC.Inventory.GetTemSerStatus(GlbUserComCode, GlbUserDefLoca, DropDownListItemCode.SelectedValue, Convert.ToInt32(ddlScanBatches.SelectedValue));
            DropDownListStatus.DataTextField = "TUS_ITM_STUS";
            DropDownListStatus.DataValueField = "TUS_ITM_STUS";
            DropDownListStatus.DataBind();
            DropDownListStatus_SelectedIndexChanged(null, null);

        }

        protected void DropDownListStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewPopUp.DataSource = CHNLSVC.Inventory.GetTemSerByCodeStatus(GlbUserComCode, GlbUserDefLoca, DropDownListItemCode.SelectedValue, DropDownListStatus.SelectedValue, Convert.ToInt32(ddlScanBatches.SelectedValue));
            GridViewPopUp.DataBind();
            if (HiddenFieldCusCrePopUpStats.Value == "1")
                ModalPopupExtender1.Show();
        }

        protected void ButtonApply_Click(object sender, EventArgs e)
        {
            int result = 0;
            for (int i = 0; i < GridViewPopUp.Rows.Count; i++)
            {
                GridViewRow gvRow = GridViewPopUp.Rows[i];
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
                        result = CHNLSVC.Inventory.UpdateUnitCost(GlbUserComCode, GlbUserDefLoca, _item, _itemStatus, _itemSerial, Convert.ToDecimal(TextBoxUnitCost.Text), Convert.ToInt32(ddlScanBatches.SelectedValue));
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
            else
            {
                //string Msg = "<script>alert('Nothing Updated');</script>";
                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            TextBoxUnitCost.Text = "";
            HiddenFieldCusCrePopUpStats.Value = "0";
        }
        #endregion

        #region Item Add and Bind
        private void ClearItemDetail()
        {
            txtItem.Text = string.Empty;
            txtQty.Text = string.Empty;
            lblModel.Text = string.Empty;
            ddlStatus.Items.Clear();
            txtUnitCost.Text = string.Empty;
        }
        protected void AddItem(object sender, EventArgs e)
        {
            if (UserSeqNo == 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "You have to click on New Serial Scan button or pick already scan document from the dropdown list");
                return;
            }

            if (ddlAdjType_.SelectedValue.ToString() == "+ADJ")
            {
                if (string.IsNullOrEmpty(txtUnitCost.Text.Trim()))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the unit cost");
                    txtUnitCost.Focus();
                    return;
                }

                if (!IsNumeric(txtUnitCost.Text, NumberStyles.Currency))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the valid unit cost");
                    txtUnitCost.Focus();
                    return;
                }
            }


            if (string.IsNullOrEmpty(txtItem.Text.Trim()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the item");
                txtItem.Focus();
                return;
            }

            //Load Item details
            MasterItem _masterItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text.Trim());

            if (string.IsNullOrEmpty(ddlStatus.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the status");
                ddlStatus.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtQty.Text))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the qty");
                txtQty.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ddlAdjType_.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select adjestment type");
                ddlAdjType_.Focus();
                return;
            }

            //check for the location balance.
            if (ddlAdjType_.SelectedValue.ToString() == "- ADJ")
            {
                List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), ddlStatus.SelectedValue.ToString());
                decimal _formQty = Convert.ToDecimal(txtQty.Text);
                foreach (InventoryLocation _loc in _inventoryLocation)
                {
                    if (_formQty > _loc.Inl_free_qty)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please check the inventory balance!");
                        txtQty.Focus();
                        return;
                    }
                }
            }

            if (ScanItemList != null)
                if (ScanItemList.Count > 0)
                {
                    var _duplicate = from _ls in ScanItemList
                                     where _ls.Tui_req_itm_cd == txtItem.Text.Trim() && _ls.Tui_req_itm_stus == ddlStatus.SelectedValue.ToString() && _ls.Tui_pic_itm_stus == txtUnitCost.Text.Trim()
                                     select _ls;

                    if (_duplicate != null)
                        if (_duplicate.Count() > 0)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Selected item already available");
                            return;
                        }

                    var _maxline = (from _ls in ScanItemList
                                    select Convert.ToInt32(_ls.Tui_pic_itm_cd)).Max();

                    ReptPickItems _itm = new ReptPickItems();
                    _itm.Tui_usrseq_no = UserSeqNo;
                    _itm.Tui_req_itm_qty = Convert.ToDecimal(txtQty.Text);
                    _itm.Tui_req_itm_cd = txtItem.Text.Trim();
                    _itm.Tui_req_itm_stus = ddlStatus.SelectedValue.ToString();
                    _itm.Tui_pic_itm_cd = Convert.ToString(Convert.ToInt32(_maxline) + 1);
                    if (ddlAdjType_.SelectedValue.ToString() == "+ADJ")
                        _itm.Tui_pic_itm_stus = txtUnitCost.Text;
                    else
                        _itm.Tui_pic_itm_stus = "0";
                    _itm.Tui_pic_itm_qty = 0;

                    List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                    _saveonly.Add(_itm);
                    CHNLSVC.Inventory.SavePickedItems(_saveonly);
                }
                else
                {
                    ReptPickItems _itm = new ReptPickItems();
                    _itm.Tui_usrseq_no = UserSeqNo;
                    _itm.Tui_req_itm_qty = Convert.ToDecimal(txtQty.Text);
                    _itm.Tui_req_itm_cd = txtItem.Text.Trim();
                    _itm.Tui_req_itm_stus = ddlStatus.SelectedValue.ToString();
                    _itm.Tui_pic_itm_cd = "1";
                    _itm.Tui_pic_itm_qty = 0;
                    if (ddlAdjType_.SelectedValue.ToString() == "+ADJ")
                        _itm.Tui_pic_itm_stus = txtUnitCost.Text;
                    else
                        _itm.Tui_pic_itm_stus = "0";

                    List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                    _saveonly.Add(_itm);
                    CHNLSVC.Inventory.SavePickedItems(_saveonly);
                }

            ScanItemList = CHNLSVC.Inventory.GetAllScanRequestItemsList(UserSeqNo);
            gvItem.DataSource = ScanItemList;
            gvItem.DataBind();

            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Added Successfully!");

            ClearItemDetail();
        }

        protected void BindItemStatus(string _company, string _location, string _bin, string _item, string _serial)
        {
            ddlStatus.Items.Clear();
            DataTable _tbl = CHNLSVC.Inventory.GetAvailableItemStatus(_company, _location, _bin, _item, _serial);
            ddlStatus.DataSource = _tbl;
            ddlStatus.DataTextField = "ins_itm_stus";
            ddlStatus.DataValueField = "ins_itm_stus";
            ddlStatus.DataBind();


        }
        protected void BindItem(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string _item = e.Row.Cells[0].Text.Trim();
                MasterItem _i = CHNLSVC.Inventory.GetItem(GlbUserComCode, _item);
                Label _desc = e.Row.FindControl("lblgvItemDesc") as Label;
                Label _model = e.Row.FindControl("lblgvItemModel") as Label;
                _desc.Text = _i.Mi_longdesc;
                _model.Text = _i.Mi_model;
                if (ddlAdjType_.SelectedValue.ToString() == "+ADJ")
                    e.Row.Cells[6].Visible = true;
                else
                    e.Row.Cells[6].Visible = false;
            }
        }
        #endregion
        #region Check Item
        protected void CheckItem()
        {
            if (string.IsNullOrEmpty(txtItem.Text)) return;

            MasterItem _masterItem = new MasterItem();

            _masterItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text);
            if (_masterItem != null)
                lblModel.Text = "Description : " + _masterItem.Mi_longdesc + "    Model : " + _masterItem.Mi_model;

            if (ddlAdjType_.SelectedValue.ToString() == "ADJ-")
            {
                List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), ddlStatus.SelectedValue.ToString());
                if (_inventoryLocation == null) { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No stock balance available"); return; }

                var _status = from _ls in _inventoryLocation
                              select _ls.Inl_itm_stus;
                ddlStatus.DataSource = _status;
                ddlStatus.DataBind();
            }
            else
            {
                List<MasterCompanyItemStatus> _sts = CHNLSVC.Inventory.GetAllCompanyStatuslist(GlbUserComCode);
                ddlStatus.DataSource = _sts.Select(x => x.Mic_cd).ToList();
                ddlStatus.DataBind();

            }
        }
        protected void CheckItem(object sender, EventArgs e)
        {
            CheckItem();
        }
        #endregion
        #region Check Qty
        protected void CheckQty()
        {
            if (string.IsNullOrEmpty(txtQty.Text)) return;

            if (string.IsNullOrEmpty(txtItem.Text.Trim()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the item");
                txtItem.Focus();
                return;
            }

            if (string.IsNullOrEmpty(ddlStatus.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the status");
                ddlStatus.Focus();
                return;
            }

            if (ddlAdjType_.SelectedValue.ToString() == "ADJ-")
            {
                MasterItem _masterItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text.Trim());
                if (_masterItem != null)
                {
                    //check for the location balance.
                    List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(GlbUserComCode, GlbUserDefLoca, txtItem.Text.Trim(), ddlStatus.SelectedValue.ToString());

                    if (_inventoryLocation.Count == 1)
                    {
                        foreach (InventoryLocation _loc in _inventoryLocation)
                        {
                            //SetItemBalance(_loc);
                            decimal _formQty = Convert.ToDecimal(txtQty.Text);
                            if (_formQty > _loc.Inl_free_qty)
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please check the inventory balance!");
                                txtQty.Text = string.Empty;
                                txtQty.Focus();
                                return;
                            }
                        }
                    }
                }
            }



        }
        protected void CheckQty(object sender, EventArgs e)
        {
            CheckQty();
        }
        #endregion
        public List<ReptPickSerials> serial_list
        {
            get { return (List<ReptPickSerials>)ViewState["ReptPickSerials"]; }
            set { ViewState["ReptPickSerials"] = value; }
        }
        protected void gvItem_SelectedIndexChanged(object sender, EventArgs e)//pick button is a select link
        {
            if (string.IsNullOrEmpty(ddlAdjType_.SelectedValue.ToString()))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the adjustment type");
                return;
            }

            if (ddlAdjType_.SelectedValue.ToString() == "- ADJ")
            {

                GridViewRow gvr = gvItem.SelectedRow;
                string ScannedDoQty = gvr.Cells[5].Text;
               

                lblScanQty.Text = string.IsNullOrEmpty(ScannedDoQty) ? "0" : ScannedDoQty;
                lblInvoiceQty.Text = gvItem.SelectedRow.Cells[4].Text.ToString();
                string _itmStatus = gvItem.SelectedRow.Cells[3].Text.ToString();

                string longDiscript = gvItem.SelectedRow.Cells[1].Text.ToString();
                lblPopupItemCode.Text = gvItem.SelectedRow.Cells[0].Text.ToString();
                hdnInvoiceLineNo.Value = gvItem.SelectedDataKey[3].ToString();

                ReptPickItems h = new ReptPickItems();

                divPopupImg.Visible = false;
                lblpopupMsg.Text = string.Empty;

                if (lblPopupItemCode.Text != "&nbsp;")
                {
                    lblPopupItemCode.Text = lblPopupItemCode.Text;
                    lblPopupBinCode.Visible = false;
                    txtPopupQty.Visible = true;

                    MasterItem msitem = new MasterItem();
                    msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, lblPopupItemCode.Text);

                    if (msitem.Mi_is_ser1 == 0)//check whether it is a non-sirialized item // (msitem.Mi_is_ser1 == false)
                    {
                        serial_list = new List<ReptPickSerials>();
                        serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text, "-1", _itmStatus);

                        GridPopup.DataSource = serial_list;
                        GridPopup.DataBind();

                        ModalPopupExtItem.Show();
                    }
                    else if (msitem.Mi_is_ser1 == 1) //serial
                    {
                        serial_list = new List<ReptPickSerials>();
                        serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text, "-1", _itmStatus);

                        GridPopup.DataSource = serial_list;
                        GridPopup.DataBind();

                        ModalPopupExtItem.Show();
                    }
                    else if (msitem.Mi_is_ser1 == -1)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Non serial, decimal allow");
                        serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text, "-1", _itmStatus);

                        GridPopup.DataSource = serial_list;
                        GridPopup.DataBind();

                        ModalPopupExtItem.Show();
                    }

                }
            }
            else if (ddlAdjType_.SelectedValue.ToString() == "+ADJ")
            {
                GridViewRow gvr = gvItem.SelectedRow;
                string ScannedDoQty = gvr.Cells[5].Text;
                string UnitCost = gvr.Cells[6].Text;

                lblScanQty.Text = string.IsNullOrEmpty(ScannedDoQty) ? "0" : ScannedDoQty;
                lblInvoiceQty.Text = gvItem.SelectedRow.Cells[4].Text.ToString();
                string _itmStatus = gvItem.SelectedRow.Cells[3].Text.ToString();
                lblActQty_.Text = gvr.Cells[5].Text;

                string longDiscript = gvItem.SelectedRow.Cells[1].Text.ToString();
                lblPopupItemCode.Text = gvItem.SelectedRow.Cells[0].Text.ToString();
                hdnInvoiceLineNo.Value = gvItem.SelectedDataKey[3].ToString();

                ReptPickItems h = new ReptPickItems();

                divPopupImg.Visible = false;
                lblpopupMsg.Text = string.Empty;


                int editRowIndex = gvr.RowIndex;

                string _selectedItemDetails = lblPopupItemCode.Text.Trim() + "|" + UnitCost + "|" + hdnInvoiceLineNo.Value + "|" + lblInvoiceQty.Text + "|" + _itmStatus;
                LoadSerialModal(_selectedItemDetails, editRowIndex);

            }

        }
        #region Scan Serial For ADJ -
        protected void btnPopupOk_Click(object sender, EventArgs e)
        {

            Int32 generated_seq = UserSeqNo;

            string itemCode = lblPopupItemCode.Text.Trim();

            Int32 num_of_checked_itms = 0;
            foreach (GridViewRow gvr in this.GridPopup.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");

                if (chkSelect.Checked)
                {
                    num_of_checked_itms++;
                }


            }
            Decimal pending_amt = Convert.ToDecimal(lblInvoiceQty.Text.ToString()) - Convert.ToDecimal(lblScanQty.Text.ToString());
            if (num_of_checked_itms > pending_amt)
            {
                divPopupImg.Visible = true;
                lblpopupMsg.Text = "Can't exceed Request Qty!";
                ModalPopupExtItem.Show();
                return;
            }

            MasterItem msitem = new MasterItem();
            msitem = CHNLSVC.Inventory.GetItem(GlbUserComCode, itemCode);
            if (msitem.Mi_is_ser1 == 1)
            {
                int rowCount = 0;

                foreach (GridViewRow gvr in this.GridPopup.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");

                    Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                    if (chkSelect.Checked)
                    {

                        string binCode = gvr.Cells[5].Text;
                        ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, serID);

                        _reptPickSerial_.Tus_cre_by = GlbUserName;

                        _reptPickSerial_.Tus_usrseq_no = generated_seq;

                        //Update_inrser_INS_AVAILABLE
                        Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serID, -1);
                        _reptPickSerial_.Tus_cre_by = GlbUserName;

                        _reptPickSerial_.Tus_base_doc_no = Convert.ToString(UserSeqNo);
                        _reptPickSerial_.Tus_base_itm_line = Convert.ToInt16(hdnInvoiceLineNo.Value);


                        _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                        _reptPickSerial_.Tus_itm_model = msitem.Mi_model;

                        //enter row into TEMP_PICK_SER
                        CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                        List<ReptPickItems> _o = new List<ReptPickItems>();
                        ReptPickItems _I = new ReptPickItems();
                        _I.Tui_req_itm_cd = itemCode;
                        _I.Tui_req_itm_stus = _reptPickSerial_.Tus_itm_stus;
                        _I.Tui_pic_itm_qty = 1;
                        _I.Tui_usrseq_no = UserSeqNo;
                        _o.Add(_I);
                        CHNLSVC.Inventory.SavePickedItems(_o);

                        rowCount++;
                        //isManualscan = true;

                    }

                }
            }

            else if (msitem.Mi_is_ser1 == 0)
            {
                int rowCount = 0;

                List<ReptPickSerials> actual_non_ser_List = new List<ReptPickSerials>();

                foreach (GridViewRow gvr in this.GridPopup.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");

                    Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                    if (chkSelect.Checked)
                    {
                        string binCode = gvr.Cells[5].Text;

                        ReptPickSerials _reptPickSerial_nonSer = CHNLSVC.Inventory.Get_all_details_on_serialID(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, serID);

                        _reptPickSerial_nonSer.Tus_cre_by = GlbUserName;

                        _reptPickSerial_nonSer.Tus_usrseq_no = generated_seq;
                        //Update_inrser_INS_AVAILABLE
                        Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serID, -1);
                        //  Boolean update_inr_ser = CHNLSVC.Inventory.Update_inrser_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, "N/A", -1);
                        _reptPickSerial_nonSer.Tus_cre_by = GlbUserName;

                        _reptPickSerial_nonSer.Tus_base_doc_no = Convert.ToString(UserSeqNo);
                        _reptPickSerial_nonSer.Tus_base_itm_line = Convert.ToInt16(hdnInvoiceLineNo.Value);

                        _reptPickSerial_nonSer.Tus_itm_desc = msitem.Mi_shortdesc;
                        _reptPickSerial_nonSer.Tus_itm_model = msitem.Mi_model;

                        _reptPickSerial_nonSer.Tus_new_remarks = string.Empty;
                        //enter row into TEMP_PICK_SER
                        CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_nonSer, null);

                        List<ReptPickItems> _o = new List<ReptPickItems>();
                        ReptPickItems _I = new ReptPickItems();
                        _I.Tui_req_itm_cd = itemCode;
                        _I.Tui_req_itm_stus = _reptPickSerial_nonSer.Tus_itm_stus;
                        _I.Tui_req_itm_qty = 1;
                        _I.Tui_usrseq_no = UserSeqNo;
                        _o.Add(_I);
                        CHNLSVC.Inventory.SavePickedItems(_o);

                        rowCount++;
                        //isManualscan = true;
                        actual_non_ser_List.Add(_reptPickSerial_nonSer);

                        //  binCD = _reptPickSerial_nonSer.Tus_bin;//the last items's bin will be assigned at last
                    }

                }
            }
            //------------------------------------------------------------------------------------------------------------------------------
            else if (msitem.Mi_is_ser1 == -1)
            {
                int rowCount = 0;

                List<ReptPickSerials> actual_non_ser_List = new List<ReptPickSerials>();

                foreach (GridViewRow gvr in this.GridPopup.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");

                    Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                    if (chkSelect.Checked)
                    {
                        string binCode = gvr.Cells[5].Text;

                        ReptPickSerials _reptPickSerial_nonSer = CHNLSVC.Inventory.Get_all_details_on_serialID(GlbUserComCode, GlbUserDefLoca, binCode, itemCode, serID);



                        _reptPickSerial_nonSer.Tus_cre_by = GlbUserName;

                        _reptPickSerial_nonSer.Tus_usrseq_no = generated_seq;


                        Decimal pending_amt_ = Convert.ToDecimal(lblInvoiceQty.Text.ToString()) - Convert.ToDecimal(lblScanQty.Text.ToString());
                        //only updated if the whole amount is finished.
                        if ((Convert.ToDecimal(lblScanQty.Text.ToString()) + num_of_checked_itms) - pending_amt_ == 0)
                        {
                            //Update_inrser_INS_AVAILABLE
                            Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, itemCode, serID, -1);
                        }


                        _reptPickSerial_nonSer.Tus_cre_by = GlbUserName;

                        _reptPickSerial_nonSer.Tus_base_doc_no = Convert.ToString(UserSeqNo);
                        _reptPickSerial_nonSer.Tus_base_itm_line = Convert.ToInt16(hdnInvoiceLineNo.Value);

                        _reptPickSerial_nonSer.Tus_itm_desc = msitem.Mi_shortdesc;
                        _reptPickSerial_nonSer.Tus_itm_model = msitem.Mi_model;
                        _reptPickSerial_nonSer.Tus_qty = Convert.ToDecimal(txtPopupQty.Text.Trim());
                        //TODO: if the item IN document GRN ->PRN else DO

                        CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_nonSer, null);

                        List<ReptPickItems> _o = new List<ReptPickItems>();
                        ReptPickItems _I = new ReptPickItems();
                        _I.Tui_req_itm_cd = itemCode;
                        _I.Tui_req_itm_stus = _reptPickSerial_nonSer.Tus_itm_stus;
                        _I.Tui_req_itm_qty = 1;
                        _I.Tui_usrseq_no = UserSeqNo;
                        _o.Add(_I);
                        CHNLSVC.Inventory.SavePickedItems(_o);

                        rowCount++;

                        actual_non_ser_List.Add(_reptPickSerial_nonSer);
                    }
                }
            }
            list_GetAllScanSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, UserSeqNo, "ADJ");
            list_ReptPickSerialsSubList = CHNLSVC.Inventory.GetAllScanSubSerialsList(UserSeqNo, "ADJ");
            ScanItemList = CHNLSVC.Inventory.GetAllScanRequestItemsList(UserSeqNo);


            gridShowAdjustedData.DataSource = list_GetAllScanSerialsList;
            gridShowAdjustedData.DataBind();

            gvItem.DataSource = ScanItemList;
            gvItem.DataBind();

        }

        protected void btnPopupCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnPopupSarch_Click(object sender, EventArgs e)
        {
            //List<ReptPickSerials> serial_list = new List<ReptPickSerials>();

            if (ddlPopupSerial.SelectedValue == "Serial 1")
            {
                string serial_no = txtPopupSearchSer.Text.Trim();
                //call query.
                string serch_serial = txtPopupSearchSer.Text.Trim() + "%";
                string bin = lblPopupBinCode.Text.Trim();
                lblPopupItemCode.Text = lblPopupItemCode.Text;
                serial_list = CHNLSVC.Inventory.Search_by_serial(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text.Trim(), null, serch_serial, null);
                GridPopup.DataSource = serial_list;
                GridPopup.DataBind();

                ModalPopupExtItem.Show();


            }
            else if (ddlPopupSerial.SelectedValue == "Serial 2")
            {
                string serial_no = txtPopupSearchSer.Text.Trim();
                //call query.
                string serch_serial = txtPopupSearchSer.Text.Trim() + "%";
                string bin = lblPopupBinCode.Text.Trim();
                lblPopupItemCode.Text = lblPopupItemCode.Text;
                serial_list = CHNLSVC.Inventory.Search_by_serial(GlbUserComCode, GlbUserDefLoca, lblPopupItemCode.Text.Trim(), null, null, serch_serial);
                GridPopup.DataSource = serial_list;
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
            string itemCode = lblPopupItemCode.Text.Trim();

            Int32 num_of_checked_itms = 0;
            foreach (GridViewRow gvr in this.GridPopup.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("checkPopup");

                if (chkSelect.Checked)
                {
                    num_of_checked_itms++;
                }


            }
            Decimal pending_amt = Convert.ToDecimal(lblInvoiceQty.Text.ToString()) - Convert.ToDecimal(lblScanQty.Text.ToString());
            if ((Convert.ToDecimal(lblScanQty.Text.ToString()) + num_of_checked_itms) > pending_amt)
            {
                Decimal availability = Convert.ToDecimal(lblInvoiceQty.Text.ToString()) - (Convert.ToDecimal(lblScanQty.Text.ToString()));
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Can't exceed Invoice Qty! Can add only " + availability + " itmes more.");
                return;
            }

            ModalPopupExtItem.Show();
        }

        protected void OnRemoveFromItemGrid(object sender, GridViewDeleteEventArgs e)
        {
            int row_id = e.RowIndex;

            if (string.IsNullOrEmpty(gvItem.DataKeys[row_id][0].ToString())) return;

            string _item = (string)gvItem.DataKeys[row_id][0];
            string _itmStatus = (string)gvItem.DataKeys[row_id][1];
            decimal _qty = (decimal)gvItem.DataKeys[row_id][2];

            List<ReptPickSerials> _list = new List<ReptPickSerials>();
            List<ReptPickItems> _listItem = new List<ReptPickItems>();
            _list = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, UserSeqNo, "ADJ");
            _listItem = CHNLSVC.Inventory.GetAllScanRequestItemsList(UserSeqNo);

            if (_list != null)
                if (_list.Count > 0)
                {
                    var _delete = (from _lst in _list
                                   where _lst.Tus_itm_cd == _item && _lst.Tus_itm_stus == _itmStatus
                                   select _lst).ToList();

                    foreach (ReptPickSerials _ser in _delete)
                    {
                        string _items = _ser.Tus_itm_cd;
                        Int32 _serialID = _ser.Tus_ser_id;
                        string _bin = _ser.Tus_bin;
                        string serial_1 = _ser.Tus_ser_1;

                        MasterItem _masterItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, _item);

                        if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                        {

                            CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, UserSeqNo, Convert.ToInt32(_serialID));
                            CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, _item, _serialID, 1);
                        }
                        else
                        {
                            CHNLSVC.Inventory.DeleteTempPickSerialByItem(GlbUserComCode, GlbUserDefLoca, UserSeqNo, _item, _itmStatus);

                        }
                    }



                }
            CHNLSVC.Inventory.DeleteTempPickItembyItem(UserSeqNo, _item, _itmStatus);

            list_GetAllScanSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, UserSeqNo, "ADJ");
            list_ReptPickSerialsSubList = CHNLSVC.Inventory.GetAllScanSubSerialsList(UserSeqNo, "ADJ");
            ScanItemList = CHNLSVC.Inventory.GetAllScanRequestItemsList(UserSeqNo);


            gridShowAdjustedData.DataSource = list_GetAllScanSerialsList;
            gridShowAdjustedData.DataBind();

            gvItem.DataSource = ScanItemList;
            gvItem.DataBind();

            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfuly removed!");
        }
        protected void OnRemoveFromSerialGrid(object sender, GridViewDeleteEventArgs e)
        {
            int row_id = e.RowIndex;

            if (string.IsNullOrEmpty(gridShowAdjustedData.DataKeys[row_id][0].ToString())) return;

            string _item = (string)gridShowAdjustedData.DataKeys[row_id][0];
            string _status = (string)gridShowAdjustedData.DataKeys[row_id][1];
            Int32 _serialID = (Int32)gridShowAdjustedData.DataKeys[row_id][2];
            string _bin = (string)gridShowAdjustedData.DataKeys[row_id][3];
            string serial_1 = (string)gridShowAdjustedData.DataKeys[row_id][4];

            MasterItem _masterItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, _item);

            if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
            {
                CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, UserSeqNo, Convert.ToInt32(_serialID));
                CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(GlbUserComCode, GlbUserDefLoca, _item, _serialID, 1);
            }
            else
            {
                CHNLSVC.Inventory.DeleteTempPickSerialByItem(GlbUserComCode, GlbUserDefLoca, UserSeqNo, _item, _status);
            }

            CHNLSVC.Inventory.UpdateTempPickItem(UserSeqNo, _item, _status, 1);

            list_GetAllScanSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, UserSeqNo, "ADJ");
            list_ReptPickSerialsSubList = CHNLSVC.Inventory.GetAllScanSubSerialsList(UserSeqNo, "ADJ");
            ScanItemList = CHNLSVC.Inventory.GetAllScanRequestItemsList(UserSeqNo);

            gridShowAdjustedData.DataSource = list_GetAllScanSerialsList;
            gridShowAdjustedData.DataBind();

            gvItem.DataSource = ScanItemList;
            gvItem.DataBind();

            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfuly removed!");
        }
        #endregion
        #region  Scan Serial For ADJ+
        private void LoadSerialModal(string _selectedItemDetails, int _gvRowIndex)
        {
            string[] arr = _selectedItemDetails.Split(new string[] { "|" }, StringSplitOptions.None);
            string _selectedItemCode = arr[0];
            hdnUnitPrice.Value = arr[1];
            hdnLineNo.Value = arr[2];
            lblReqQty.Text = arr[3];
           
            hdngvRowIndex.Value = _gvRowIndex.ToString();
            ddlmpeItemStatus.Items.Clear();
            ddlmpeItemStatus.Items.Add(arr[4]);
            //---added by shani----+
            txtActualQty.Text = "";
            txtSerialNo1.Text = "";
            txtSerialNo2.Text = "";
            txtSerialNo3.Text = "";
            //---------------------+
            MasterItem _selectedItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, _selectedItemCode);

            //Set Serial modal popup data.
            lblmpeItemCode.Text = _selectedItem.Mi_cd + " - " + _selectedItem.Mi_longdesc + " - " + _selectedItem.Mi_model + " - " + _selectedItem.Mi_brand;

            //Load bin codes.
            List<string> bincodes = CHNLSVC.Inventory.GetBinCodesforInventoryInward(GlbUserComCode, GlbUserDefLoca);
            ddlmpeBinCode.DataSource = bincodes;
            ddlmpeBinCode.DataBind();

            ////-----added by shani----------+
            //if (bincodes.Count == 2)
            //{
            //    ddlmpeBinCode.SelectedValue = bincodes[1].ToString();
            //    ddlmpeBinCode.Enabled = false;
            //}
            //else
            //{

            //}
            ////-----------------------------+
            //Load default bincode
            string default_bin = CHNLSVC.Inventory.Get_default_binCD(GlbUserComCode, GlbUserDefLoca);
            if (default_bin != null)
            {
                ddlmpeBinCode.SelectedValue = default_bin;
            }

            gvItemSerials.DataSource = null;
            gvItemSerials.DataBind();

            if (_selectedItem.Mi_is_ser1 == 1) //Is Serilize item.
            {
                divSerial.Visible = true;
                divNonSerial.Visible = false;

                //Set Serial number textboxes.
                txtSerialNo1.Enabled = (_selectedItem.Mi_is_ser1 == 1) ? true : false;
                txtSerialNo2.Enabled = true;// _selectedItem.Mi_is_ser2;
                txtSerialNo3.Enabled = true; //_selectedItem.Mi_is_ser3;

                //Disable actual qty textbox.
                //txtActualQty.Visible = false;
            }
            else if ((_selectedItem.Mi_is_ser1 == 0) || (_selectedItem.Mi_is_ser1 == -1)) //Is non Serilize item.
            {
                //divSerial.Visible = false; //commented by shani
                //-------------added by shani----------------+
                divSerial.Visible = true;

                txtSerialNo1.Enabled = false;
                txtSerialNo2.Enabled = false;
                txtSerialNo3.Enabled = false;
                //-------------------------------------------+
                divNonSerial.Visible = true;
                //Enable actual qty textbox.
                //txtActualQty.Enabled = true;


            }


            //Get the user seq no for selected requestNo.
            int _userSeqNo = UserSeqNo;

            //Bind if there are any existing serials in DB.
            if (_userSeqNo > 0)
            {
                //Load all scan serial list.
                List<ReptPickSerials> _reptPickSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, _userSeqNo, "ADJ");

                if ((_reptPickSerialList != null) && (_reptPickSerialList.Count > 0))
                {
                    //Edit Code : 0001
                    List<ReptPickSerials> _resultSerialList = _reptPickSerialList.Where(x => x.Tus_itm_cd.Equals(_selectedItemCode) && x.Tus_itm_line == Convert.ToInt32(hdnLineNo.Value.ToString())).ToList();
                    gvItemSerials.DataSource = _resultSerialList;
                    gvItemSerials.DataBind();
                }
            }

            serialmdpExtender.Show();

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddItemQuantites();

        }

        private void AddItemQuantites()
        {
            //Edit Code : 0001
            try
            {
                if (ddlmpeBinCode.SelectedItem.Text.ToUpper().Equals("--SELECT--"))
                    throw new UIValidationException("Please select Bin.");

                string _selectedReqNo = Convert.ToString(UserSeqNo);
                string[] arr = lblmpeItemCode.Text.Split(new string[] { " - " }, StringSplitOptions.None);
                string _selectedItemCode = arr[0];
                string _selectedItemDesc = arr[1];
                string _selectedItemModel = arr[2];
                string _selectedItemBrand = arr[3];

                int _userSeqNo = 0;
                _userSeqNo = UserSeqNo;

                using (TransactionScope _tr = new TransactionScope())
                {
                    //Get the selected Item
                    MasterItem _selectedItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, _selectedItemCode);
                    string _binCode = ddlmpeBinCode.SelectedValue;
                    string _itemStatus = ddlmpeItemStatus.SelectedValue;

                    if (_selectedItem.Mi_is_ser1 == 1) //(Serialize Item = 1)
                    {
                        if (string.IsNullOrEmpty(txtSerialNo1.Text))
                            throw new UIValidationException("Please enter Serial No 1.");

                        string _serialNo1 = (txtSerialNo1.Enabled) ? txtSerialNo1.Text.Trim() : string.Empty;
                        string _serialNo2 = (txtSerialNo2.Enabled) ? txtSerialNo2.Text.Trim() : string.Empty;
                        string _serialNo3 = (txtSerialNo3.Enabled) ? txtSerialNo3.Text.Trim() : string.Empty;

                        if ((CHNLSVC.Inventory.IsExistInSerialMaster(GlbUserComCode, _selectedItemCode, _serialNo1)) > 0)
                            throw new UIValidationException("Serial No1 is already exist.");

                        if ((CHNLSVC.Inventory.IsExistInTempPickSerial(GlbUserComCode, _userSeqNo.ToString(), _selectedItemCode, _serialNo1)) > 0)
                            throw new UIValidationException("Serial No1 is already in use. Enter with different Serial No1");//exists in the temp-pick-ser

                        //Write to the Picked items serilals table.
                        ReptPickSerials _inputReptPickSerials = new ReptPickSerials();

                        _inputReptPickSerials.Tus_usrseq_no = _userSeqNo;
                        _inputReptPickSerials.Tus_doc_no = _selectedReqNo;
                        _inputReptPickSerials.Tus_seq_no = 0;
                        _inputReptPickSerials.Tus_itm_line = 0;
                        _inputReptPickSerials.Tus_batch_line = 0;
                        _inputReptPickSerials.Tus_ser_line = 0;
                        _inputReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
                        _inputReptPickSerials.Tus_com = GlbUserComCode;
                        _inputReptPickSerials.Tus_loc = GlbUserDefLoca;
                        _inputReptPickSerials.Tus_bin = _binCode;
                        _inputReptPickSerials.Tus_itm_cd = _selectedItemCode;
                        _inputReptPickSerials.Tus_itm_stus = _itemStatus;
                        _inputReptPickSerials.Tus_unit_cost = string.IsNullOrEmpty(hdnUnitPrice.Value) ? 0 : Convert.ToDecimal(hdnUnitPrice.Value);
                        _inputReptPickSerials.Tus_unit_price = 0;
                        _inputReptPickSerials.Tus_qty = 1;
                        _inputReptPickSerials.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();
                        _inputReptPickSerials.Tus_ser_1 = _serialNo1;
                        _inputReptPickSerials.Tus_ser_2 = _serialNo2;
                        _inputReptPickSerials.Tus_ser_3 = _serialNo3;
                        _inputReptPickSerials.Tus_itm_desc = _selectedItemDesc;
                        _inputReptPickSerials.Tus_itm_model = _selectedItemModel;
                        _inputReptPickSerials.Tus_itm_brand = _selectedItemBrand;
                        _inputReptPickSerials.Tus_itm_line = string.IsNullOrEmpty(hdnLineNo.Value) ? 0 : Convert.ToInt32(hdnLineNo.Value);
                        _inputReptPickSerials.Tus_cre_by = GlbUserName;
                        _inputReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
                        _inputReptPickSerials.Tus_session_id = GlbUserSessionID;

                        _inputReptPickSerials.Tus_itm_desc = _selectedItemDesc;
                        _inputReptPickSerials.Tus_itm_model = _selectedItemModel;
                        _inputReptPickSerials.Tus_itm_brand = _selectedItemBrand;

                        List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, _userSeqNo, "ADJ");

                        var serCount = 0;
                        if (_resultItemsSerialList != null)
                        {
                            serCount = (from c in _resultItemsSerialList
                                        where c.Tus_itm_cd == _selectedItemCode && c.Tus_itm_line == Convert.ToInt32(hdnLineNo.Value)
                                        select c).Count();
                        }

                        if (serCount < Convert.ToDecimal(lblReqQty.Text.Trim()))
                        {
                            CHNLSVC.Inventory.SaveAllScanSerials(_inputReptPickSerials, null);
                            List<ReptPickItems> _o = new List<ReptPickItems>();
                            ReptPickItems _I = new ReptPickItems();
                            _I.Tui_req_itm_cd = _selectedItemCode;
                            _I.Tui_req_itm_stus = _itemStatus;
                            _I.Tui_pic_itm_qty = 1;
                            _I.Tui_usrseq_no = UserSeqNo;
                            _o.Add(_I);
                            CHNLSVC.Inventory.SavePickedItems(_o);

                        }
                        else
                            throw new UIValidationException("Cannot exceed the required Qty!");

                        List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, _userSeqNo, "ADJ");
                        gvItemSerials.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(_selectedItemCode) && x.Tus_itm_line == Convert.ToInt32(hdnLineNo.Value)).ToList();
                        gvItemSerials.DataBind();

                        SubmitItemSerialData();


                    }
                    else if (_selectedItem.Mi_is_ser1 == 0) //(Non serialize Item = 0)
                    {
                        if (string.IsNullOrEmpty(txtActualQty.Text))
                            throw new UIValidationException("Please enter Actual Qty.");

                        Decimal difference;
                        try
                        {
                            try
                            {
                                difference = Convert.ToDecimal(lblReqQty.Text) - Convert.ToDecimal(lblActQty_.Text);
                            }
                            catch (Exception e)
                            {
                                throw new UIValidationException("Enter a valid Quantity.");
                            }

                        }
                        catch (Exception e)
                        {
                            throw new UIValidationException("Enter a valid Quantity.");
                        }

                        if (difference < Convert.ToDecimal(txtActualQty.Text))
                        {
                            throw new UIValidationException("Cannot Add more than Required Qty.");
                        }



                        int _actualQty = Convert.ToInt32(txtActualQty.Text.Trim());

                        for (int i = 0; i < _actualQty; i++)
                        {
                            //Write to the Picked items serilals table.
                            ReptPickSerials _newReptPickSerials = new ReptPickSerials();

                            _newReptPickSerials.Tus_usrseq_no = _userSeqNo;
                            _newReptPickSerials.Tus_doc_no = _selectedReqNo;
                            _newReptPickSerials.Tus_seq_no = 0;
                            _newReptPickSerials.Tus_itm_line = 0;
                            _newReptPickSerials.Tus_batch_line = 0;
                            _newReptPickSerials.Tus_ser_line = 0;
                            _newReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
                            _newReptPickSerials.Tus_com = GlbUserComCode;
                            _newReptPickSerials.Tus_loc = GlbUserDefLoca;
                            _newReptPickSerials.Tus_bin = _binCode;
                            _newReptPickSerials.Tus_itm_cd = _selectedItemCode;
                            _newReptPickSerials.Tus_itm_stus = _itemStatus;
                            _newReptPickSerials.Tus_unit_cost = string.IsNullOrEmpty(hdnUnitPrice.Value) ? 0 : Convert.ToDecimal(hdnUnitPrice.Value);
                            _newReptPickSerials.Tus_unit_price = 0;
                            _newReptPickSerials.Tus_qty = 1;
                            _newReptPickSerials.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();
                            _newReptPickSerials.Tus_ser_1 = "N/A";
                            _newReptPickSerials.Tus_ser_2 = "N/A";
                            _newReptPickSerials.Tus_ser_3 = "N/A";
                            _newReptPickSerials.Tus_itm_desc = _selectedItemDesc;
                            _newReptPickSerials.Tus_itm_model = _selectedItemModel;
                            _newReptPickSerials.Tus_itm_brand = _selectedItemBrand;
                            _newReptPickSerials.Tus_itm_line = string.IsNullOrEmpty(hdnLineNo.Value) ? 0 : Convert.ToInt32(hdnLineNo.Value);
                            _newReptPickSerials.Tus_cre_by = GlbUserName;
                            _newReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
                            _newReptPickSerials.Tus_session_id = GlbUserSessionID;

                            _newReptPickSerials.Tus_itm_desc = _selectedItemDesc;
                            _newReptPickSerials.Tus_itm_model = _selectedItemModel;
                            _newReptPickSerials.Tus_itm_brand = _selectedItemBrand;

                            List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, _userSeqNo, "ADJ");
                            var serCount = 0;
                            if (_resultItemsSerialList != null)
                            {
                                serCount = (from c in _resultItemsSerialList
                                            where c.Tus_itm_cd == _selectedItemCode && c.Tus_itm_line == Convert.ToInt32(hdnLineNo.Value)
                                            select c).Count();
                            }

                            if (serCount < Convert.ToInt32(lblReqQty.Text.Trim()))
                            {
                                CHNLSVC.Inventory.SaveAllScanSerials(_newReptPickSerials, null);
                                List<ReptPickItems> _o = new List<ReptPickItems>();
                                ReptPickItems _I = new ReptPickItems();
                                _I.Tui_req_itm_cd = _selectedItemCode;
                                _I.Tui_req_itm_stus = _itemStatus;
                                _I.Tui_pic_itm_qty = 1;
                                _I.Tui_usrseq_no = UserSeqNo;
                                _o.Add(_I);
                                CHNLSVC.Inventory.SavePickedItems(_o);

                                lblActQty_.Text = (Convert.ToDecimal(lblActQty_.Text) + 1).ToString();
                                _newReptPickSerials = null;

                                //Bind it to the Serial list grid. (Bind serials relavant to only selected item code)
                                List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, _userSeqNo, "ADJ");
                                gvItemSerials.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(_selectedItemCode) && x.Tus_itm_line == Convert.ToInt32(hdnLineNo.Value)).ToList();
                                gvItemSerials.DataBind();

                                SubmitItemSerialData();
                                serialmdpExtender.Show();
                            }
                            else
                                throw new UIValidationException("Cannot exceed the required Qty!");

                        }
                    }
                    else if (_selectedItem.Mi_is_ser1 == -1) //(Non serialize decimal Item = -1))
                    {
                        if (string.IsNullOrEmpty(txtActualQty.Text))
                            throw new UIValidationException("Please enter Actual Qty.");

                        int _actualQty = Convert.ToInt32(txtActualQty.Text.Trim());

                        //Write to the Picked items serilals table.
                        ReptPickSerials _decimalReptPickSerials = new ReptPickSerials();

                        _decimalReptPickSerials.Tus_usrseq_no = _userSeqNo;
                        _decimalReptPickSerials.Tus_doc_no = _selectedReqNo;
                        _decimalReptPickSerials.Tus_seq_no = 0;
                        _decimalReptPickSerials.Tus_itm_line = 0;
                        _decimalReptPickSerials.Tus_batch_line = 0;
                        _decimalReptPickSerials.Tus_ser_line = 0;
                        _decimalReptPickSerials.Tus_doc_dt = DateTime.Now.Date;
                        _decimalReptPickSerials.Tus_com = GlbUserComCode;
                        _decimalReptPickSerials.Tus_loc = GlbUserDefLoca;
                        _decimalReptPickSerials.Tus_bin = _binCode;
                        _decimalReptPickSerials.Tus_itm_cd = _selectedItemCode;
                        _decimalReptPickSerials.Tus_itm_stus = _itemStatus;
                        _decimalReptPickSerials.Tus_unit_cost = string.IsNullOrEmpty(hdnUnitPrice.Value) ? 0 : Convert.ToDecimal(hdnUnitPrice.Value);
                        _decimalReptPickSerials.Tus_unit_price = 0;
                        _decimalReptPickSerials.Tus_qty = _actualQty;
                        //_decimalReptPickSerials.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();
                        _decimalReptPickSerials.Tus_ser_1 = "N/A";
                        _decimalReptPickSerials.Tus_ser_2 = "N/A";
                        _decimalReptPickSerials.Tus_ser_3 = "N/A";
                        _decimalReptPickSerials.Tus_itm_desc = _selectedItemDesc;
                        _decimalReptPickSerials.Tus_itm_model = _selectedItemModel;
                        _decimalReptPickSerials.Tus_itm_brand = _selectedItemBrand;
                        _decimalReptPickSerials.Tus_itm_line = string.IsNullOrEmpty(hdnLineNo.Value) ? 0 : Convert.ToInt32(hdnLineNo.Value);
                        _decimalReptPickSerials.Tus_cre_by = GlbUserName;
                        _decimalReptPickSerials.Tus_cre_dt = DateTime.Now.Date;
                        _decimalReptPickSerials.Tus_session_id = GlbUserSessionID;

                        _decimalReptPickSerials.Tus_itm_desc = _selectedItemDesc;
                        _decimalReptPickSerials.Tus_itm_model = _selectedItemModel;
                        _decimalReptPickSerials.Tus_itm_brand = _selectedItemBrand;

                        //Save to the temp table.

                        //CHNLSVC.Inventory.SavePickedSerialsDecimalItems(_decimalReptPickSerials); //commented by shani
                        //_decimalReptPickSerials = null; //commented by shani


                        List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, _userSeqNo, "ADJ");


                        //var serCount = (from c in _resultItemsSerialList
                        //                select c).Count();

                        //for non serial, decimal allowed
                        var serCount_2 = (from c in _resultItemsSerialList
                                          select c.Tus_qty).Sum();

                        if (serCount_2 < Convert.ToInt32(lblReqQty.Text.Trim()))
                        {
                            //CHNLSVC.Inventory.SaveAllScanSerials(_inputReptPickSerials, null);
                            CHNLSVC.Inventory.SaveAllScanSerials(_decimalReptPickSerials, null);
                            List<ReptPickItems> _o = new List<ReptPickItems>();
                            ReptPickItems _I = new ReptPickItems();
                            _I.Tui_req_itm_cd = _selectedItemCode;
                            _I.Tui_req_itm_stus = _itemStatus;
                            _I.Tui_pic_itm_qty = 1;
                            _I.Tui_usrseq_no = UserSeqNo;
                            _o.Add(_I);
                            CHNLSVC.Inventory.SavePickedItems(_o);
                            _decimalReptPickSerials = null;

                            //Bind it to the Serial list grid. (Bind serials relavant to only selected item code)
                            List<ReptPickSerials> _ResultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, _userSeqNo, "ADJ");
                            gvItemSerials.DataSource = _ResultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(_selectedItemCode) && x.Tus_itm_line == Convert.ToInt32(hdnLineNo.Value)).ToList();
                            gvItemSerials.DataBind();

                            SubmitItemSerialData();
                        }
                        else
                            throw new UIValidationException("Cannot exceed the required Qty!");
                    }

                    _tr.Complete();
                }
            }
            catch (UIValidationException uiex)
            {
                this.uc_SerialPopUpMsgInfo.SetMessage(CommonUIDefiniton.MessageType.Error, uiex.ErrorMessege);
            }
            catch (Exception ex)
            {
                this.uc_SerialPopUpMsgInfo.SetMessage(CommonUIDefiniton.MessageType.Error, ex.Message);
            }
            this.ClearModalWindow();
            string default_bin = CHNLSVC.Inventory.Get_default_binCD(GlbUserComCode, GlbUserDefLoca);
            if (default_bin != null)
            {
                ddlmpeBinCode.SelectedValue = default_bin;
            }
            serialmdpExtender.Show();



        }

        private void ClearModalWindow()
        {
            txtActualQty.Text = string.Empty;
            txtSerialNo1.Text = string.Empty;
            txtSerialNo2.Text = string.Empty;
            txtSerialNo3.Text = string.Empty;

            ddlmpeBinCode.SelectedIndex = 0;
            ddlmpeItemStatus.SelectedIndex = 0;
        }

        private void SubmitItemSerialData()
        {
            list_GetAllScanSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, UserSeqNo, "ADJ");
            list_ReptPickSerialsSubList = CHNLSVC.Inventory.GetAllScanSubSerialsList(UserSeqNo, "ADJ");
            ScanItemList = CHNLSVC.Inventory.GetAllScanRequestItemsList(UserSeqNo);


            gridShowAdjustedData.DataSource = list_GetAllScanSerialsList;
            gridShowAdjustedData.DataBind();

            gvItem.DataSource = ScanItemList;
            gvItem.DataBind();

            //pnlReceiptItemDetails.GroupingText = "Receipt Item Details : Request No.  " + lblRequestNo.Text;
            serialmdpExtender.Hide();
        }

        protected void gvItemSerials_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                //Edit Code : 0001
                case "DELETEITEM":
                    {
                        ImageButton imgbtndelSerial = (ImageButton)e.CommandSource;
                        string _selectedItemDetails = imgbtndelSerial.CommandArgument;
                        string _lineNo;
                        DeleteSelectedItem(_selectedItemDetails, out _lineNo);

                        GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                        string itemcd = row.Cells[0].Text;

                        string[] arr = _selectedItemDetails.Split(new string[] { "|" }, StringSplitOptions.None);
                        string _selectedItemCode = arr[1];

                        Int32 usrSeqNo = UserSeqNo;
                        List<ReptPickSerials> _resultItemsSerialList = CHNLSVC.Inventory.GetAllScanSerialsList(GlbUserComCode, GlbUserDefLoca, GlbUserName, usrSeqNo, "ADJ");

     
                        if (_resultItemsSerialList != null)
                        {
                            gvItemSerials.DataSource = _resultItemsSerialList.Where(x => x.Tus_itm_cd.Equals(_selectedItemCode) && x.Tus_itm_line == Convert.ToInt32(hdnLineNo.Value)).ToList();
                            gvItemSerials.DataBind();
                        }
                        else
                        {
                            gvItemSerials.DataSource = null;
                            gvItemSerials.DataBind();
                        }
 
                        SubmitItemSerialData();
                        serialmdpExtender.Show();
 
                        string default_bin = CHNLSVC.Inventory.Get_default_binCD(GlbUserComCode, GlbUserDefLoca);
                        if (default_bin != null)
                        {
                            ddlmpeBinCode.SelectedValue = default_bin;
                        }
                        break;
                    }
            }
        }

        private void DeleteSelectedItem(string _selectedItemDetails, out string _LineNo)
        {
            string[] arr = _selectedItemDetails.Split(new string[] { "|" }, StringSplitOptions.None);
            string _serialId = arr[0];
            string _itemCode = arr[1];
            string _serialNo1 = arr[2];
            string _binCode = arr[3];
            string _lineNo = hdnInvoiceLineNo.Value;
            string _status = arr[4];
  
            //Get the user seq no for selected requestNo.
            int _userSeqNo = UserSeqNo;

            MasterItem _selectedItem = CHNLSVC.Inventory.GetItem(GlbUserComCode, _itemCode);

            if (_selectedItem.Mi_is_ser1 == 1)  //Delete serialize item.
            {
                CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, _userSeqNo, Convert.ToInt32(_serialId));
                CHNLSVC.Inventory.UpdateTempPickItem(UserSeqNo, _itemCode, _status, 1);
            }

            else if ((_selectedItem.Mi_is_ser1 == 0))  //Delete non-serialize item.
            {
                CHNLSVC.Inventory.Del_temp_pick_ser(GlbUserComCode, GlbUserDefLoca, _userSeqNo, Convert.ToInt32(_serialId));
                CHNLSVC.Inventory.UpdateTempPickItem(UserSeqNo, _itemCode, _status, 1);
                lblActQty_.Text = (Convert.ToDecimal(lblActQty_.Text) - 1).ToString();
            }
            else if ((_selectedItem.Mi_is_ser1 == -1))  //Delete non-serialize item.
            {
                CHNLSVC.Inventory.Del_temp_pick_serdummy(GlbUserComCode, GlbUserDefLoca, _userSeqNo, Convert.ToInt32(_serialId), _itemCode, _binCode);
                CHNLSVC.Inventory.UpdateTempPickItem(UserSeqNo, _itemCode, _status, 1);
            }

            _LineNo = _lineNo;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SubmitItemSerialData();
        }
        #endregion
    }
}
