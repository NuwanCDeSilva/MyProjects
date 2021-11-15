using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using FF.BusinessObjects;
using Oracle.DataAccess.Client;
using System.Drawing;

namespace FF.WebERPClient.Inventory_Module
{
    public partial class InvalidTransactionCorrection :BasePage
    {
        public InventoryHeader DocHdr
        {
            get { return (InventoryHeader)ViewState["DocHdr"]; }
            set { ViewState["DocHdr"] = value; }
        }
        public string OutwardNo
        {
            get { return Session["OutwardNo"].ToString(); }
            set { Session["OutwardNo"] = value; }
        }
        public List<ReptPickSerials> PickSerialsList
        {
            get { return (List<ReptPickSerials>)ViewState["ScanItemSerList"]; }
            set { ViewState["ScanItemSerList"] = value; }
        }
        public List<ReptPickSerialsSub> reptPickSerials_SubList
        {
            get { return (List<ReptPickSerialsSub>)ViewState["reptPickSerials_SubList"]; }
            set { ViewState["reptPickSerials_SubList"] = value; }
        }
        public Int32 UserSeqNo
        {
            get { return Convert.ToInt32(Session["UserSeqNo"]); }
            set { Session["UserSeqNo"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtDocNum.Attributes.Add("onkeypress", "return fun1(event,'" + btnGetDocDet.ClientID + "')");

            if(!IsPostBack)
            {
                DataTable dt = new DataTable();
                grvItemDet.DataSource = dt;
                grvItemDet.DataBind();
                grvSerialDet.DataSource = dt;
                grvSerialDet.DataBind();

                DocHdr =null;
                OutwardNo = string.Empty;
                PickSerialsList = new List<ReptPickSerials>();
                reptPickSerials_SubList = new List<ReptPickSerialsSub>();
                UserSeqNo = 0;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
           
            Response.Redirect("~/HP_Module/CommissionChange.aspx");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }
             

        protected void btnGetDocDet_Click(object sender, EventArgs e)
        {
            lblIssuedLoc.Text = string.Empty;
            lblInvalidLoc.Text = string.Empty;
            lblIssuedDt.Text = string.Empty;
            lblStatus.Text = string.Empty;
            
            DataTable dt = new DataTable();
            grvItemDet.DataSource = dt;
            grvItemDet.DataBind();

            grvSerialDet.DataSource = dt;
            grvSerialDet.DataBind();
            //-------------------------------------------------------------
            string DocNo = txtDocNum.Text.Trim();
            OutwardNo = DocNo;
            InventoryHeader invHdr = CHNLSVC.Inventory.Get_Int_Hdr(DocNo);
            if (invHdr.Ith_direct==true)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Not an AOD-OUT document!");
                return;
            }
            lblIssuedLoc.Text = invHdr.Ith_loc;
            lblInvalidLoc.Text = invHdr.Ith_oth_loc;
            lblIssuedDt.Text = invHdr.Ith_doc_date.ToShortDateString();
            lblStatus.Text = invHdr.Ith_stus;
            if (invHdr.Ith_stus!="A")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Document's status indicates that it cannot be processed further!");
                lblStatus.ForeColor = Color.Red;
                return;
            }
            DocHdr = invHdr;//CHNLSVC.Inventory.Get_Int_Hdr(DocNo);
            lblIssuedLoc.Text = DocHdr.Ith_loc;
            lblInvalidLoc.Text = DocHdr.Ith_oth_loc;
            lblIssuedDt.Text = DocHdr.Ith_doc_date.ToShortDateString();
            lblStatus.Text = DocHdr.Ith_stus;
            //-------------------------------------------------------------
            DataTable dt_itms = CHNLSVC.Inventory.Get_Int_Itm(DocNo);
            grvItemDet.DataSource = dt_itms;
            grvItemDet.DataBind();

        }

        protected void grvItemDet_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grvItemDet.SelectedRow;
            string DocNo =txtDocNum.Text.Trim();
            string ItemCode=  row.Cells[1].Text;
            DataTable dt_serials = CHNLSVC.Inventory.Get_SerialOfDoc(DocNo, ItemCode);
            grvSerialDet.DataSource = dt_serials;
            grvSerialDet.DataBind();
        }

        protected void setHiddenFields()
        {         
            MasterLocation _mstLoc = CHNLSVC.General.GetLocationInfor(DocHdr.Ith_com, lblInvalidLoc.Text.Trim());
            if (_mstLoc != null)
            {
                if (_mstLoc.Ml_allow_bin == false)
                {
                    hdnAllowBin.Value = "0";
                }
                else
                {
                    hdnAllowBin.Value = "1";
                }
            }
            //String _defBin = CHNLSVC.Inventory.GetDefaultBinCode(GlbUserComCode, GlbUserDefLoca);
            String _defBin = CHNLSVC.Inventory.GetDefaultBinCode(DocHdr.Ith_com, lblInvalidLoc.Text.Trim());
            if (!string.IsNullOrEmpty(_defBin))
            {
                hdnAllowBin.Value = _defBin;
            }
            else
            {
                string Msg = "<script>alert('Default Bin Not Setup For Location : " + lblInvalidLoc.Text.Trim() + "')</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                return;
            }

        }
        protected void BindOutwardItems()
        {
            string OutwardType = "AOD";
            PickSerialsList = null;
            ReptPickHeader _reptPickHdr = new ReptPickHeader();

            Int32 _seq = CHNLSVC.Inventory.GetRequestUserSeqNo(DocHdr.Ith_com, OutwardNo);

            UserSeqNo = _seq;

            _reptPickHdr.Tuh_direct = true;
            _reptPickHdr.Tuh_doc_no = OutwardNo; //Outward Doc No
            _reptPickHdr.Tuh_doc_tp = OutwardType; //Doc Type
            _reptPickHdr.Tuh_ischek_itmstus = false;
            _reptPickHdr.Tuh_ischek_reqqty = true;
            _reptPickHdr.Tuh_ischek_simitm = false;
            _reptPickHdr.Tuh_session_id = GlbUserSessionID;//Session ID
            _reptPickHdr.Tuh_usr_com = GlbUserComCode; //Company
            _reptPickHdr.Tuh_usr_id = GlbUserName; //User Name
            _reptPickHdr.Tuh_usrseq_no = _seq;

           // List<ReptPickSerials> PickSerials = CHNLSVC.Inventory.GetOutwarditems(GlbUserDefLoca, hdnAllowBin.Value, _reptPickHdr);
            List<ReptPickSerials> PickSerials = CHNLSVC.Inventory.GetOutwarditems(lblInvalidLoc.Text.Trim(), hdnAllowBin.Value, _reptPickHdr);

            if (PickSerials != null)
            {
                PickSerialsList = PickSerials;
            }
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            MasterMsgInfoUCtrl.Clear();
            setHiddenFields();
            BindOutwardItems();//gets the  PickSerialsList
            string new_AOD_OUT_Doc = string.Empty;
         
            Int32 effect= Correction_Process(out new_AOD_OUT_Doc);
            if (effect > 0)
            {
                string Msg1 = "<script>alert('AOD Receipt Successfully corrected! New AOD-OUT Document No. :" + new_AOD_OUT_Doc + "')</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);

                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "AOD Receipt Successfully corrected! New AOD-OUT Document No :" + new_AOD_OUT_Doc);
                return;
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Error Occured. Failed to correct the document!");
                return;
            }
           
        }
        #region COMMENTED
        //protected void Process_AOD_IN_and_OUT_1(out string _genInventoryDoc,out InventoryHeader outHdr)
        //{
        //    if (DocHdr==null)
        //    {
        //        _genInventoryDoc = string.Empty;
        //        outHdr = null;
        //        return;
        //    }
        //    string OutwardNo = DocHdr.Ith_doc_no;//is this right??
        //    //Process
        //    //InventoryHeader invHdr = new InventoryHeader();
        //    InventoryHeader invHdr = DocHdr;
        //    invHdr.Ith_loc = lblInvalidLoc.Text.Trim();//GlbUserDefLoca;
        //    //invHdr.Ith_com = GlbUserComCode;
        //    invHdr.Ith_oth_docno = OutwardNo;
        //    invHdr.Ith_doc_date = DateTime.Now.Date; //Convert.ToDateTime(txtDate.Text).Date;
        //    invHdr.Ith_doc_year = DateTime.Now.Date.Year;  //Convert.ToDateTime(txtDate.Text).Year;            
        //    invHdr.Ith_doc_tp = "AOD";
        //    invHdr.Ith_cate_tp = "NOR";

        //    //invHdr.Ith_oth_com =
        //    invHdr.Ith_is_manual = false;
        //    invHdr.Ith_stus = "A";
        //    invHdr.Ith_cre_by = GlbUserName;
        //    invHdr.Ith_mod_by = GlbUserName;
        //    invHdr.Ith_direct = true;
        //    invHdr.Ith_session_id = GlbUserSessionID;
        //    invHdr.Ith_manual_ref = "N/A";
        //    invHdr.Ith_remarks = txtRemarks.Text;
        //   // invHdr.Ith_vehi_no = txtVehicle.Text;
        //    invHdr.Ith_sub_tp = "HO";  //HEAD OFFICE
        //   // invHdr.Ith_oth_com = txtIssueCom.Text;
        //    invHdr.Ith_oth_loc = lblIssuedLoc.Text.Trim();//txtIssueLoca.Text;

        //    //Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(OutwardType, GlbUserComCode, OutwardNo, 1);
        //    Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("AOD", invHdr.Ith_com, OutwardNo, 1);            
                      
        //    reptPickSerials_SubList = CHNLSVC.Inventory.GetAllScanSubSerialsList(user_seq_num, "AOD");

        //    MasterAutoNumber masterAutoNum = new MasterAutoNumber();
        //    masterAutoNum.Aut_cate_cd = lblInvalidLoc.Text.Trim(); 
        //    masterAutoNum.Aut_cate_tp = "LOC";
        //    masterAutoNum.Aut_direction = 1;
        //    masterAutoNum.Aut_modify_dt = null;
        //    masterAutoNum.Aut_year = DateTime.Now.Year;
        //    masterAutoNum.Aut_moduleid = "AOD";
        //    masterAutoNum.Aut_start_char = "AOD";

        //    string documntNo = string.Empty;

        //    Int32 result = -99;            

        //    result = CHNLSVC.Inventory.AODReceipt(invHdr, PickSerialsList, reptPickSerials_SubList, masterAutoNum, out documntNo);

        //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "AOD-In Successfully Saved! Document No. : " + documntNo);

        //    if (result != -99 && result > 0)
        //    {
        //        string Msg1 = "<script>alert('AOD Receipt Successfully Saved! Document No. : " + documntNo + "')</script>";
        //        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);
                
        //        #region AOD OUT

        //        string _message = string.Empty;
        //        _genInventoryDoc = string.Empty;
        //        string _genSalesDoc = string.Empty;

        //        //Process
        //        //new values to header.  (From Invalid Loc to Issued Loc)
                
        //        invHdr.Ith_doc_no = documntNo;
        //        invHdr.Ith_loc = lblInvalidLoc.Text.Trim();//lblIssuedLoc.Text.Trim();
        //       // invHdr.Ith_oth_docno = documntNo;
        //        invHdr.Ith_direct = false;
        //        invHdr.Ith_oth_loc = lblIssuedLoc.Text.Trim();//lblInvalidLoc.Text.Trim();             

        //        #region Inventory AutoNumber
        //        MasterAutoNumber _inventoryAuto = new MasterAutoNumber();
        //        _inventoryAuto.Aut_cate_cd = lblInvalidLoc.Text.Trim();//GlbUserDefLoca;
        //        _inventoryAuto.Aut_cate_tp = "LOC";
        //        _inventoryAuto.Aut_direction = null;
        //        _inventoryAuto.Aut_modify_dt = null;
        //        _inventoryAuto.Aut_moduleid = string.Empty;
        //        _inventoryAuto.Aut_start_char = string.Empty;
        //        _inventoryAuto.Aut_modify_dt = null;
        //        _inventoryAuto.Aut_year = DateTime.Now.Year;//Convert.ToDateTime(txtDate.Text).Year;
        //        #endregion
                              
        //        foreach(ReptPickSerials RPS in PickSerialsList)
        //        {
        //            RPS.Tus_doc_dt = DateTime.Now.Date; ;
        //            //RPS.Tus_doc_no=;
        //            //RPS.Tus_loc = lblInvalidLoc.Text.Trim();
        //            //RPS.Tus_out_date=DateTime.Now.Date;
        //            //RPS.Tus_bin = hdnAllowBin.Value;
        //            RPS.Tus_new_remarks = "AOD-OUT";
                    
        //        }
        //        //Int32 _effect = CHNLSVC.Inventory.SaveCommonOutWardEntry(GlbUserComCode, GlbUserDefProf, invHdr.Ith_com, null, invHdr, _inventoryAuto, null, null, PickSerialsList, reptPickSerials_SubList, out _message, out _genSalesDoc, out _genInventoryDoc, false, false);
        //        Int32 _effect = CHNLSVC.Inventory.SaveAOD_OutWardEntry(GlbUserComCode, GlbUserDefProf, invHdr.Ith_com, invHdr, _inventoryAuto, PickSerialsList, reptPickSerials_SubList, true, out _message, out _genInventoryDoc);
                               
        //        string Msg2 = string.Empty;

        //        if (_effect == -1)
        //        {
                   
        //            outHdr = null;
        //            Msg2 = "alert('" + _message + "'); ";
                   
        //        }
        //        else
        //        {
        //            outHdr = invHdr;
        //            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfuly processed!. Document No(s) - " + _genInventoryDoc + ", " + _genSalesDoc);
        //            Msg2 = "alert('Successfuly processed!. Document No(s) :" + _genInventoryDoc + "," + _genSalesDoc + "'); ";
        //            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg2, false);
        //            //--------------------------------------------------------------------------------------------------------
                  
                   
        //            //Process****************************************************************************************************************
        //            //new out values to header. (From Issued Loc to Correct Loc)
        //            invHdr.Ith_stus = "A";
        //            invHdr.Ith_doc_no = documntNo;
        //            invHdr.Ith_loc = lblIssuedLoc.Text.Trim();//lblIssuedLoc.Text.Trim();
        //           // invHdr.Ith_oth_docno = documntNo;
        //            invHdr.Ith_direct = false;
        //            invHdr.Ith_oth_loc = txtCorrectLoc.Text.Trim();//lblInvalidLoc.Text.Trim();   

        //            //MasterAutoNumber _inventoryAuto = new MasterAutoNumber();
        //            _inventoryAuto.Aut_cate_cd = lblIssuedLoc.Text.Trim();//GlbUserDefLoca;                    

        //            string _message2 = string.Empty;
        //            string _genInventoryDoc2 = string.Empty;
                    
        //            _effect = CHNLSVC.Inventory.SaveAOD_OutWardEntry(GlbUserComCode, GlbUserDefProf, invHdr.Ith_com, invHdr, _inventoryAuto, PickSerialsList, reptPickSerials_SubList, false, out _message2, out _genInventoryDoc2);                                       
        //            //---------------------------------------------------------------------------------------------------------
        //            Msg2 = "alert('Successfuly processed!. Document No(s) :" + _genInventoryDoc + "," + _genSalesDoc + "'); ";
        //            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg2 + "Issued loc out= " + Msg2, false);
        //        }
        //        #endregion
        //    }
        //    else
        //    {
        //        _genInventoryDoc = string.Empty;
        //        outHdr = null;
        //        string Msg = "<script>alert('Sorry, not Saved inward entry1!');</script>";
        //        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
        //    }
        //}
    
        #endregion

        protected Int32 Correction_Process(out string new_AOD_OUT_Doc)
        {
            Int32 effect = 0;
            new_AOD_OUT_Doc = string.Empty;
            if (DocHdr == null)
            {
                new_AOD_OUT_Doc = string.Empty;
                return effect;
            }
            string OutwardNo = DocHdr.Ith_doc_no;//is this right??
            //Process
            //InventoryHeader invHdr = new InventoryHeader();
            InventoryHeader invHdr = DocHdr;
            invHdr.Ith_loc = lblInvalidLoc.Text.Trim();//GlbUserDefLoca;
            //invHdr.Ith_com = GlbUserComCode;
            invHdr.Ith_oth_docno = OutwardNo;
            invHdr.Ith_doc_date = DateTime.Now.Date; //Convert.ToDateTime(txtDate.Text).Date;
            invHdr.Ith_doc_year = DateTime.Now.Date.Year;  //Convert.ToDateTime(txtDate.Text).Year;            
            invHdr.Ith_doc_tp = "AOD";
            invHdr.Ith_cate_tp = "NOR";

            //invHdr.Ith_oth_com =
            invHdr.Ith_is_manual = false;
            invHdr.Ith_stus = "A";
            invHdr.Ith_cre_by = GlbUserName;
            invHdr.Ith_mod_by = GlbUserName;
            invHdr.Ith_direct = true;
            invHdr.Ith_session_id = GlbUserSessionID;
            invHdr.Ith_manual_ref = "N/A";
            invHdr.Ith_remarks = txtRemarks.Text;
            // invHdr.Ith_vehi_no = txtVehicle.Text;
            invHdr.Ith_sub_tp = "HO";  //HEAD OFFICE
            // invHdr.Ith_oth_com = txtIssueCom.Text;
            invHdr.Ith_oth_loc = lblIssuedLoc.Text.Trim();

         
            Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("AOD", invHdr.Ith_com, OutwardNo, 1);

            reptPickSerials_SubList = CHNLSVC.Inventory.GetAllScanSubSerialsList(user_seq_num, "AOD");

            MasterAutoNumber masterAutoNum = new MasterAutoNumber();
            masterAutoNum.Aut_cate_cd = lblInvalidLoc.Text.Trim();
            masterAutoNum.Aut_cate_tp = "LOC";
            masterAutoNum.Aut_direction = 1;
            masterAutoNum.Aut_modify_dt = null;
            masterAutoNum.Aut_year = DateTime.Now.Year;
            masterAutoNum.Aut_moduleid = "AOD";
            masterAutoNum.Aut_start_char = "AOD";

            string documntNo = string.Empty;
         
            #region AOD OUT

            #region Inventory AutoNumber (FOR AOD-OUT)
            MasterAutoNumber _inventoryAuto = new MasterAutoNumber();
            _inventoryAuto.Aut_cate_cd = lblInvalidLoc.Text.Trim();//GlbUserDefLoca;
            _inventoryAuto.Aut_cate_tp = "LOC";
            _inventoryAuto.Aut_direction = null;
            _inventoryAuto.Aut_modify_dt = null;
            _inventoryAuto.Aut_moduleid = string.Empty;
            _inventoryAuto.Aut_start_char = string.Empty;
            _inventoryAuto.Aut_modify_dt = null;
            _inventoryAuto.Aut_year = DateTime.Now.Year;//Convert.ToDateTime(txtDate.Text).Year;
            #endregion

            foreach (ReptPickSerials RPS in PickSerialsList)
            {
                RPS.Tus_doc_dt = DateTime.Now.Date;                
            }           
            #endregion

            effect = CHNLSVC.Inventory.Correct_InvalidTransaction(lblIssuedLoc.Text.Trim(), lblInvalidLoc.Text.Trim(), txtCorrectLoc.Text.Trim(), invHdr, PickSerialsList, reptPickSerials_SubList, masterAutoNum, _inventoryAuto, out new_AOD_OUT_Doc);

            return effect;
        }


        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            BasePage _basePage = new BasePage();
            _basePage = new BasePage();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.INV_DocNo:
                    {
                        //(p_com in NVARCHAR2, p_docType in NVARCHAR2,p_direction in NUMBER,p_docDtFrom in DATE,p_docDtTo in DATE,p_otherLoc in NVARCHAR2,p_stus in NVARCHAR2, c_data OUT sys_refcursor)
                        paramsText.Append(_basePage.GlbUserComCode + seperator + "AOD" + seperator + 0 + seperator + txtFromDt.Text.Trim() + seperator+ txtToDt.Text.Trim()+ seperator+ txtInvalidLoc.Text.Trim().ToUpper()+seperator+ "A");
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }
        protected void ImgBtnDocSearch_Click(object sender, ImageClickEventArgs e)
        {
            BasePage _basePage = new BasePage();

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.INV_DocNo);
            DataTable dataSource = _basePage.CHNLSVC.CommonSearch.Search_int_hdr_Document(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtDocNum.ClientID; 
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
    }
}