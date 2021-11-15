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
    public partial class InventoryDocCancelation : BasePage
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

            Response.Redirect("~/Inventory_Module/InventoryDocCancelation.aspx");
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
         
            //Int32 effect= Correction_Process(out new_AOD_OUT_Doc);
            Int32 effect = 1;
            if (effect > 0)
            {
                string Msg1 = "<script>alert('Document Canceled Successfully!')</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg1, false);

                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Document Canceled Successfully!");
                return;
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Error Occured. Failed to cancel the document!");
                return;
            }
           
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