using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using FF.BusinessObjects;
using FF.WebERPClient.UserControls;
using System.Data;
using System.Text;
using System.Globalization;

namespace FF.WebERPClient.Inventory_Module
{
    public partial class RCC_Page : BasePage
    {
        public uc_MsgInfo MasterMsgInfoUCtrl
        {
            get
            {
                return (uc_MsgInfo)Master.FindControl("uc_MasterMsgInfo");
            }
        }

        static int RccStage;
        static string RccNo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtRccDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                txtCloseDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                txtcomp.Text = GlbUserComCode;
                txtloc.Text = GlbUserDefLoca;
                IsAllowBackDateForModule(GlbUserComCode, GlbUserDefLoca, string.Empty, Page, string.Empty, imgDate, lblDispalyInfor);

                //txtSerial.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnSerial, ""));
                //txtItem.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnItem, ""));
                txtManualRef.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnManual, ""));

                BindRCC();
                BindRCCDef();

                ddlCriteria.Items.Add("Serial");
                ddlCriteria.Items.Add("Warranty No");
                ddlCriteria.Items.Add("Item");
                ddlCriteria.Items.Add("Document No");
            }
        }

        protected void IsValidManualNo(object sender, EventArgs e)
        {
            if (txtManualRef.Text != "")
            {
                Boolean _IsValid = CHNLSVC.Inventory.IsValidManualDocument(GlbUserComCode, GlbUserDefLoca, "MDOC_RCC", Convert.ToInt32(txtManualRef.Text));
                if (_IsValid == false)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid Manual Document Number !");
                    txtManualRef.Text = "";
                    txtManualRef.Focus();
                }
            }
            else
            {
                if (chkManual.Checked == true)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid Manual Document Number !");
                    txtManualRef.Focus();
                }
            }
        }

        #region loading functions
        protected void LoadRCCDetails()
        {
            RCC _RCC = null;
            _RCC = CHNLSVC.Inventory.GetRccByNo(ddlRccNo.SelectedValue);

            txtRccDate.Text = _RCC.Inr_dt.ToShortDateString();
            txtManualRef.Text = _RCC.Inr_manual_ref;

            txtAccNo.Text = (_RCC.Inr_acc_no == null) ? string.Empty : _RCC.Inr_acc_no;
            txtAddr.Text = (_RCC.Inr_addr == null) ? string.Empty : _RCC.Inr_addr;
            txtCusName.Text = (_RCC.Inr_cust_name == null) ? string.Empty : _RCC.Inr_cust_name;
            txtInvDate.Text = (_RCC.Inr_inv_dt.ToString() == null) ? string.Empty : _RCC.Inr_inv_dt.ToString("dd/MMM/yyyy");
            txtInvNo.Text = (_RCC.Inr_inv_no == null) ? string.Empty : _RCC.Inr_inv_no;
            txtTelNo.Text = (_RCC.Inr_tel == null) ? string.Empty : _RCC.Inr_tel;

            txtEasyLoc.Text = (_RCC.Inr_easy_loc == null) ? string.Empty : _RCC.Inr_easy_loc;
            txtInsp.Text = (_RCC.Inr_insp_by == null) ? string.Empty : _RCC.Inr_insp_by;
            txtItem.Text = (_RCC.Inr_itm == null) ? string.Empty : _RCC.Inr_itm;
            txtItemDesn.Text = "";
            txtModel.Text = "";
            txtRem.Text = (_RCC.Inr_rem1 == null) ? string.Empty : _RCC.Inr_rem1;
            txtSerial.Text = (_RCC.Inr_ser == null) ? string.Empty : _RCC.Inr_ser;
            txtWarranty.Text = (_RCC.Inr_warr == null) ? string.Empty : _RCC.Inr_warr;
            ddlAcc.SelectedValue = _RCC.Inr_accessories;
            ddlCond.SelectedValue = _RCC.Inr_condition;
            ddlDefect.SelectedValue = _RCC.Inr_def_cd;

            ddlAgent.SelectedValue = _RCC.Inr_agent;
            ddlColMethod.SelectedValue = _RCC.Inr_col_method;
            ddlRccSubType.SelectedValue = _RCC.Inr_sub_tp;
            ddlRccType.SelectedValue = _RCC.Inr_tp;

            txtJob1.Text = (_RCC.Inr_jb_no == null) ? string.Empty : _RCC.Inr_jb_no;
            txtJob2.Text = "";
            txtOrdNo.Text = (_RCC.Inr_anal1 == null) ? string.Empty : _RCC.Inr_anal1;
            txtDispatchNo.Text = (_RCC.Inr_anal2 == null) ? string.Empty : _RCC.Inr_anal2;

            pnl_Item.Update();
            pnl_Job.Update();
            pnl_CustDet.Update();

            RccStage = _RCC.Inr_stage;
            RccNo = _RCC.Inr_no;

            ddlReason.SelectedValue = (_RCC.Inr_repair_stus == "") ? "-1" : _RCC.Inr_repair_stus;
            ddlRetCond.SelectedValue = (_RCC.Inr_ret_condition == "") ? "-1" : _RCC.Inr_ret_condition;
            chkRepaired.Checked = _RCC.Inr_is_repaired;
            ddlClosureType.SelectedValue = (_RCC.Inr_closure_tp == "") ? "-1" : _RCC.Inr_closure_tp;

        }
        #endregion

        #region Binding Functions
        private void BindRCC()
        {
            ddlRccNo.Items.Clear();
            ddlRccNo.Items.Add(new ListItem("--Select RCC No--", "-1"));
            ddlRccNo.DataSource = CHNLSVC.Inventory.GetRCC();
            ddlRccNo.DataTextField = "Inr_no";
            ddlRccNo.DataValueField = "Inr_no";
            ddlRccNo.DataBind();

        }

        private void BindRCCDef()
        {
            ddlRccType.Items.Clear();
            ddlRccType.Items.Add(new ListItem("--Select Type--", "-1"));
            ddlRccType.DataSource = CHNLSVC.General.GetAllType("RCC");
            ddlRccType.DataTextField = "Mtp_desc";
            ddlRccType.DataValueField = "Mtp_cd";
            ddlRccType.DataBind();

            ddlColMethod.Items.Clear();
            ddlColMethod.Items.Add(new ListItem("--Select Collection method--", "-1"));
            ddlColMethod.DataSource = CHNLSVC.Inventory.GetRCCDef("COLMETHOD");
            ddlColMethod.DataTextField = "ird_desc";
            ddlColMethod.DataValueField = "ird_cd";
            ddlColMethod.DataBind();

            ddlAcc.Items.Clear();
            ddlAcc.Items.Add(new ListItem("--Select Accessories--", "-1"));
            ddlAcc.DataSource = CHNLSVC.Inventory.GetRCCDef("ACC");
            ddlAcc.DataTextField = "ird_desc";
            ddlAcc.DataValueField = "ird_cd";
            ddlAcc.DataBind();

            ddlCond.Items.Clear();
            ddlCond.Items.Add(new ListItem("--Select Condition--", "-1"));
            ddlCond.DataSource = CHNLSVC.Inventory.GetRCCDef("COND");
            ddlCond.DataTextField = "ird_desc";
            ddlCond.DataValueField = "ird_cd";
            ddlCond.DataBind();

            ddlDefect.Items.Clear();
            ddlDefect.Items.Add(new ListItem("--Select Defect--", "-1"));
            ddlDefect.DataSource = CHNLSVC.Inventory.GetRCCDef("DEF");
            ddlDefect.DataTextField = "ird_desc";
            ddlDefect.DataValueField = "ird_cd";
            ddlDefect.DataBind();

            ddlReason.Items.Clear();
            ddlReason.Items.Add(new ListItem("--Select Reason--", "-1"));
            ddlReason.DataSource = CHNLSVC.Inventory.GetRCCDef("REPDET");
            ddlReason.DataTextField = "ird_desc";
            ddlReason.DataValueField = "ird_cd";
            ddlReason.DataBind();

            ddlRetCond.Items.Clear();
            ddlRetCond.Items.Add(new ListItem("--Select Condition--", "-1"));
            ddlRetCond.DataSource = CHNLSVC.Inventory.GetRCCDef("RETCON");
            ddlRetCond.DataTextField = "ird_desc";
            ddlRetCond.DataValueField = "ird_cd";
            ddlRetCond.DataBind();

            ddlClosureType.Items.Clear();
            ddlClosureType.Items.Add(new ListItem("--Select Closure Type--", "-1"));
            ddlClosureType.DataSource = CHNLSVC.Inventory.GetRCCDef("CLOSE");
            ddlClosureType.DataTextField = "ird_desc";
            ddlClosureType.DataValueField = "ird_cd";
            ddlClosureType.DataBind();

            ddlAgent.Items.Clear();
            ddlAgent.Items.Add(new ListItem("--Select service agent--", "-1"));
            ddlAgent.DataSource = CHNLSVC.Inventory.GetServiceAgent("SA");
            ddlAgent.DataTextField = "mbe_cd";
            ddlAgent.DataValueField = "mbe_cd";
            ddlAgent.DataBind();


        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

        protected void LoadSerial(object sender, EventArgs e)
        {
            Int32 _Seqno = Convert.ToInt32(gvSerial.SelectedDataKey[4]);
            Int32 _itmline = Convert.ToInt32(gvSerial.SelectedDataKey[5]);
            Int32 _batchline = Convert.ToInt32(gvSerial.SelectedDataKey[6]);
            Int32 _serline = Convert.ToInt32(gvSerial.SelectedDataKey[7]);

            txtItem.Text = Convert.ToString(gvSerial.SelectedDataKey[3]);
            txtSerial.Text = Convert.ToString(gvSerial.SelectedDataKey[0]);
            txtWarranty.Text = Convert.ToString(gvSerial.SelectedDataKey[1]);

            GetItemData();
            GetInvoiceDetails(_Seqno,_itmline,_batchline,_serline);

            txtSearch.Text = "";
        }

        protected void gvSerial_OnRowBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (divserial.Visible)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {

                        e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(
                                    gvSerial,
                                    String.Concat("Select$", e.Row.RowIndex),
                                    true);
                    }
                }
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            if (ddlRccType.SelectedValue.Equals("-1"))
            {
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select the RCC type");
                lblMsg.Text = "Please select the RCC type";
                gvSerial.DataSource = null;
            }
            else
            {
                if (ddlRccType.SelectedValue != "STK")       //customer item
                {
                    if (chkOthLoc.Checked == false)         //same location sale
                    {
                        gvSerial.DataSource = CHNLSVC.Inventory.GetRCCSerialSearchData(GlbUserComCode, GlbUserDefLoca, 1, 0, txtSearch.Text, ddlCriteria.SelectedValue);
                    }
                    else   //other location sale
                    {
                        gvSerial.DataSource = CHNLSVC.Inventory.GetRCCSerialSearchData(GlbUserComCode, GlbUserDefLoca, 0, 0, txtSearch.Text, ddlCriteria.SelectedValue);
                    }
                }
                else
                {
                    gvSerial.DataSource = CHNLSVC.Inventory.GetRCCSerialSearchData(GlbUserComCode, GlbUserDefLoca, 0, 1, txtSearch.Text, ddlCriteria.SelectedValue);
                }
            }

            MPESerial.Show();
            gvSerial.DataBind();
        }

        private void BindRCCSubType()
        {
            ddlRccSubType.Items.Clear();
            ddlRccSubType.Items.Add(new ListItem("--Select Sub type--", "-1"));
            ddlRccSubType.DataSource = CHNLSVC.General.GetAllSubTypes(ddlRccType.SelectedValue);
            ddlRccSubType.DataTextField = "Mstp_desc";
            ddlRccSubType.DataValueField = "Mstp_cd";
            ddlRccSubType.DataBind();

            if (ddlRccType.SelectedValue == "STK")
            {
                chkOthLoc.Checked = false;
                chkOthLoc.Enabled = false;
            }
            else
            {
                chkOthLoc.Enabled = true;
            }

        }
        #endregion

        #region UI control events
        protected void btnNew_Click(object sender, EventArgs e)
        {
            ClearRccComplete();
            ClearRccItem();
            ClearRccJob();
            ClearRccRepair();

            ddlRccNo.SelectedIndex = 0;
            ddlRccNo.Enabled = false;

            chkRaise.Enabled = true;
            chkRaise.Checked = true;

            chkComplete.Checked = false;
            chkComplete.Enabled = false;

            chkRepair.Checked = false;
            chkRepair.Enabled = false;

            chkOpen.Checked = false;
            chkOpen.Enabled = false;

            pnlJob.Enabled = false;

            pnlChkBoxes.Update();
            RccStage = 0;

            txtRccDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            btnUpdate.Enabled = true;
        }

        protected void ddlRccNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRCCDetails();
            Set_check_Controls(RccStage);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SaveRCC();
        }

        protected void ddlRccType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRCCSubType();

            txtAccNo.Text = string.Empty;
            txtAddr.Text = string.Empty;
            txtCusName.Text = string.Empty;
            txtInvNo.Text = string.Empty;
            txtInvDate.Text = string.Empty;
            txtTelNo.Text = string.Empty;
            pnl_CustDet.Update();

            ddlRccNo.SelectedIndex = 0;
            ddlAgent.SelectedIndex = 0;
            ddlColMethod.SelectedIndex = 0;

            txtEasyLoc.Text = string.Empty;
            txtInsp.Text = "";
            txtItem.Text = string.Empty;
            txtItemDesn.Text = "";
            txtModel.Text = "";
            txtRem.Text = "";
            txtSerial.Text = "";
            txtWarranty.Text = "";
            ddlAcc.SelectedIndex = 0;
            ddlCond.SelectedIndex = 0;
            ddlDefect.SelectedIndex = 0;
            pnl_Item.Update();

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearRccItem();
            ClearRccJob();
            ClearRccRepair();
            ClearRccComplete();
            ddlRccNo.Enabled = true;

            chkOpen.Checked = false;
            chkRaise.Checked = false;
            chkComplete.Checked = false;
            chkRepair.Checked = false;

            chkOpen.Enabled = false;
            chkRaise.Enabled = false;
            chkComplete.Enabled = false;
            chkRepair.Enabled = false;
            pnlChkBoxes.Update();


            txtRccDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
        }

        protected void chkManual_CheckedChanged(object sender, EventArgs e)
        {
            if (chkManual.Checked == true)
            {
                txtManualRef.Enabled = true;
                Int32 _NextNo = CHNLSVC.Inventory.GetNextManualDocNo(GlbUserComCode, GlbUserDefLoca, "MDOC_RCC");
                if (_NextNo != 0)
                {
                    txtManualRef.Text = _NextNo.ToString();
                }
                else
                {
                    txtManualRef.Text = "";
                }
            }

            else
            {
                txtManualRef.Text = string.Empty;
                txtManualRef.Enabled = false;
            }
        }


        protected void imgbtnItem_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable dataSource = CHNLSVC.CommonSearch.GetItemSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtItem.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();

        }


        #endregion

        #region Clear functions
        private void ClearRccItem()
        {
            txtAccNo.Text = string.Empty;
            txtAddr.Text = string.Empty;
            txtCusName.Text = string.Empty;
            txtInvNo.Text = string.Empty;
            txtInvDate.Text = string.Empty;
            txtTelNo.Text = string.Empty;
            pnl_CustDet.Update();

            ddlRccNo.SelectedIndex = 0;
            ddlAgent.SelectedIndex = 0;
            ddlColMethod.SelectedIndex = 0;
            ddlRccSubType.SelectedIndex = 0;
            ddlRccType.SelectedIndex = 0;
            txtManualRef.Text = "";
            chkManual.Checked = false;

            txtEasyLoc.Text = string.Empty;
            txtInsp.Text = "";
            txtItem.Text = string.Empty;
            txtItemDesn.Text = "";
            txtModel.Text = "";
            txtRem.Text = "";
            txtSerial.Text = "";
            txtWarranty.Text = "";
            ddlAcc.SelectedIndex = 0;
            ddlCond.SelectedIndex = 0;
            ddlDefect.SelectedIndex = 0;
            pnl_Item.Update();

        }

        private void ClearRccJob()
        {
            txtJob1.Text = "";
            txtJob2.Text = "";
            txtOrdNo.Text = "";
            txtDispatchNo.Text = "";
            pnl_Job.Update();
        }

        private void ClearRccRepair()
        {
            ddlReason.SelectedIndex = 0;
            ddlRetCond.SelectedIndex = 0;
            txtRepairRem.Text = "";
            txtRetDate.Text = "";
            chkRepaired.Checked = false;
        }

        private void ClearRccComplete()
        {
            ddlClosureType.SelectedIndex = 0;
            txtCompleteRem.Text = "";
            txtCloseDate.Text = "";
        }

        #endregion

        #region save function
        private void SaveRCC()
        {
            try
            {
                if (IsAllowBackDateForModule(GlbUserComCode, GlbUserDefLoca, string.Empty, Page, txtRccDate.Text, imgDate, lblDispalyInfor) == true)
                {
                    throw new UIValidationException("Back date not allow for selected date!");
                }

                if (RccStage == 0)
                {
                    if (ddlRccNo.Enabled == true)
                    {
                        throw new UIValidationException("Please press New button");
                    }
                    if ((chkManual.Checked == true) && (txtManualRef.Text == ""))
                    {
                        throw new UIValidationException("Please Enter Manual Reference Number");
                    }
                    if (ddlAcc.SelectedValue.Equals("-1"))
                    {
                        throw new UIValidationException("Please select Accessory");
                    }
                    if (ddlColMethod.SelectedValue.Equals("-1"))
                    {
                        throw new UIValidationException("Please select the collection method");
                    }
                    if (ddlRccType.SelectedValue.Equals("-1"))
                    {
                        throw new UIValidationException("Please select RCC Type");
                    }
                    if (ddlRccSubType.SelectedValue.Equals("-1"))
                    {
                        throw new UIValidationException("Please select RCC Sub Type");
                    }
                    if (ddlAgent.SelectedValue.Equals("-1"))
                    {
                        throw new UIValidationException("Please select Agent");
                    }
                    if (ddlDefect.SelectedValue.Equals("-1"))
                    {
                        throw new UIValidationException("Please select Defect Type");
                    }
                    if (ddlCond.SelectedValue.Equals("-1"))
                    {
                        throw new UIValidationException("Please select Condition");
                    }
                    if (txtEasyLoc.Text == "")
                    {
                        throw new UIValidationException("Please enter easy location");
                    }
                    if (txtItem.Text == "" || txtSerial.Text == "" || txtItemDesn.Text == "")
                    {
                        throw new UIValidationException("Please select product details");
                    }
                    if (txtSerial.Text != "" && txtSerial.Text != "NA" && txtSerial.Text != "N/A")
                    {
                        Boolean _IsSerialFound = CHNLSVC.Inventory.IsRccSerialFound(txtItem.Text, txtSerial.Text);
                        if (_IsSerialFound == true)
                        {
                            throw new UIValidationException("Another RCC found for this serial number");
                        }

                    }
                }
                if (RccStage == 1)
                {
                    if (txtJob1.Text == "")
                    {
                        throw new UIValidationException("Please enter the job number");
                    }
                }
                if (RccStage == 2)
                {
                    if (ddlReason.SelectedValue.Equals("-1") && chkRepaired.Checked == false)
                    {
                        throw new UIValidationException("Please select the reason");
                    }
                    if (ddlRetCond.SelectedValue.Equals("-1"))
                    {
                        throw new UIValidationException("Please select the return condition");
                    }


                }
                if (RccStage == 3)
                {
                    if (ddlClosureType.SelectedValue.Equals("-1"))
                    {
                        throw new UIValidationException("Please select closure type");
                    }
                }

                RCC _RCC = new RCC();
                string _RccNo = "";

                if (RccStage != 0)
                {
                    _RCC.Inr_no = ddlRccNo.SelectedValue; ;
                }
                _RCC.Inr_com_cd = GlbUserComCode;
                _RCC.Inr_loc_cd = GlbUserDefLoca;
                _RCC.Inr_dt = Convert.ToDateTime(txtRccDate.Text);
                _RCC.Inr_is_manual = chkManual.Checked;
                _RCC.Inr_manual_ref = txtManualRef.Text;
                _RCC.Inr_tp = ddlRccType.SelectedValue;
                _RCC.Inr_sub_tp = ddlRccSubType.SelectedValue;
                _RCC.Inr_agent = ddlAgent.SelectedValue;
                _RCC.Inr_col_method = ddlColMethod.SelectedValue;
                _RCC.Inr_inv_no = txtInvNo.Text;
                _RCC.Inr_acc_no = txtAccNo.Text;
                if (RccStage == 0 && ddlRccType.SelectedValue != "STK")
                {
                    _RCC.Inr_inv_dt = Convert.ToDateTime(txtInvDate.Text);
                }
                else
                {
                    _RCC.Inr_inv_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                }
                //_RCC.Inr_oth_doc_no = 
                //_RCC.Inr_oth_doc_dt = 
                //_RCC.Inr_cust_cd = 
                _RCC.Inr_cust_name = txtCusName.Text;
                _RCC.Inr_addr = txtAddr.Text;
                _RCC.Inr_tel = txtTelNo.Text;
                _RCC.Inr_itm = txtItem.Text;
                _RCC.Inr_ser = txtSerial.Text;
                _RCC.Inr_warr = txtWarranty.Text;
                _RCC.Inr_def_cd = ddlDefect.SelectedValue;
                //_RCC.Inr_def = 
                _RCC.Inr_condition = ddlCond.SelectedValue;
                _RCC.Inr_accessories = ddlAcc.SelectedValue;
                _RCC.Inr_easy_loc = txtEasyLoc.Text;
                _RCC.Inr_insp_by = txtInsp.Text;
                _RCC.Inr_rem1 = txtRem.Text;
                //_RCC.Inr_def_rem = 
                _RCC.Inr_is_jb_open = false;
                _RCC.Inr_jb_no = txtJob1.Text;
                _RCC.Inr_open_by = GlbUserName;
                //_RCC.Inr_jb_rem = 
                _RCC.Inr_is_repaired = chkRepaired.Checked;
                if (RccStage == 2)
                {
                    _RCC.Inr_repair_stus = ddlReason.SelectedValue;
                }
                _RCC.Inr_rem2 = txtRepairRem.Text;
                _RCC.Inr_is_returned = false;
                if (_RCC.Inr_stage == 2)
                {
                    _RCC.Inr_return_dt = Convert.ToDateTime(txtRetDate.Text);
                }
                if (RccStage == 2)
                {
                    _RCC.Inr_ret_condition = ddlRetCond.SelectedValue;
                }
                //_RCC.Inr_hollogram_no = txt
                _RCC.Inr_is_replace = false;
                _RCC.Inr_is_resell = false;
                _RCC.Inr_is_ret = false;
                _RCC.Inr_is_dispose = false;
                _RCC.Inr_acknoledge_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                _RCC.Inr_is_complete = false;
                _RCC.Inr_complete_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                _RCC.Inr_complete_by = GlbUserName;
                _RCC.Inr_rem3 = txtCompleteRem.Text;
                _RCC.Inr_closure_tp = ddlClosureType.SelectedValue;
                _RCC.Inr_stage = 1;
                _RCC.Inr_stus = "p";
                _RCC.Inr_cre_by = GlbUserName;
                _RCC.Inr_cre_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                _RCC.Inr_mod_by = GlbUserName;
                _RCC.Inr_mod_dt = Convert.ToDateTime(DateTime.Now.Date).Date;
                _RCC.Inr_session_id = GlbUserSessionID;
                _RCC.Inr_anal1 = txtOrdNo.Text;
                _RCC.Inr_anal2 = txtDispatchNo.Text;
                //_RCC.Inr_anal3 = 
                //_RCC.Inr_anal4 = 
                //_RCC.Inr_anal5 = 
                //_RCC.Inr_anal6 = 

                if (RccStage == 0)
                {
                    MasterAutoNumber masterAuto = new MasterAutoNumber();
                    masterAuto.Aut_cate_cd = GlbUserDefLoca;
                    masterAuto.Aut_cate_tp = "LOC";
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "RCC";
                    masterAuto.Aut_start_char = "RCC";
                    masterAuto.Aut_year = null;

                    int row_aff = CHNLSVC.Inventory.SaveRCC(_RCC, masterAuto, out _RccNo);

                    if (chkManual.Checked == true)
                    {
                        CHNLSVC.Inventory.UpdateManualDocNo(GlbUserDefLoca, "MDOC_RCC", Convert.ToInt32(txtManualRef.Text));
                    }
                    chkRaise.Checked = false;
                    chkRaise.Enabled = false;
                }
                if (RccStage == 1)
                {
                    int row_aff = CHNLSVC.Inventory.Update_RCC_JobOpen(_RCC);
                    chkOpen.Checked = false;
                    chkOpen.Enabled = false;
                    _RccNo = ddlRccNo.SelectedValue;
                }
                if (RccStage == 2)
                {
                    int row_aff = CHNLSVC.Inventory.Update_RCC_Repair(_RCC);
                    chkRepair.Checked = false;
                    chkRepair.Enabled = false;
                    _RccNo = ddlRccNo.SelectedValue;
                }
                if (RccStage == 3)
                {
                    int row_aff = CHNLSVC.Inventory.Update_RCC_complete(_RCC);
                    chkComplete.Checked = false;
                    chkComplete.Enabled = false;
                    _RccNo = ddlRccNo.SelectedValue;
                }


                BindRCC();

                string Msg = "<script>alert('Successfully Updated! RCC No. : " + _RccNo + "');window.location = 'RCC_Page.aspx';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                //ClearRccItem();
                //ClearRccJob();
                //ClearRccRepair();
                //ClearRccComplete();
            }
            catch (UIValidationException ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.ErrorMessege);
            }
            catch (Exception e)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, e.Message);
            }
        }
        #endregion

        #region settingup functions
        private void Set_check_Controls(int _stage)
        {
            chkOpen.Enabled = false;
            chkRaise.Enabled = false;
            chkComplete.Enabled = false;
            chkRepair.Enabled = false;

            chkOpen.Checked = false;
            chkRaise.Checked = false;
            chkComplete.Checked = false;
            chkRepair.Checked = false;
            btnUpdate.Enabled = true;
            pnlJob.Enabled = false;

            switch (_stage)
            {
                case 1:
                    {
                        chkOpen.Enabled = true;
                        chkOpen.Checked = true;
                        pnlJob.Enabled = true;
                        break;
                    }

                case 2:
                    {
                        chkRepair.Enabled = true;
                        chkRepair.Checked = true;
                        break;
                    }
                case 3:
                    {
                        chkComplete.Enabled = true;
                        chkComplete.Checked = true;
                        break;
                    }
                default:
                    {
                        chkOpen.Enabled = true;
                        chkOpen.Checked = true;
                        chkRepair.Enabled = true;
                        chkRepair.Checked = true;
                        chkComplete.Enabled = true;
                        chkComplete.Checked = true;
                        chkRaise.Enabled = true;
                        chkRaise.Checked = true;
                        btnUpdate.Enabled = false;
                        break;
                    }
            }
            pnlChkBoxes.Update();

        }
        #endregion


        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.AvailableSerial:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefLoca + seperator + txtItem.Text + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.IssuedSerial:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefLoca + seperator + txtItem.Text + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void GetItemData()
        {
            MasterItem _Item = null;
            if (txtItem.Text == "")
            {
                txtItemDesn.Text = "";
                txtModel.Text = "";
                return;
            }
            _Item = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItem.Text);
            if (_Item != null)
            {
                txtItemDesn.Text = _Item.Mi_shortdesc;
                txtModel.Text = _Item.Mi_model;
            }
            else
            {
                txtItemDesn.Text = "";
                txtModel.Text = "";
            }

        }


        protected void GetInvoiceDetails(Int32 _seq,Int32 _itmLine,Int32 _batchLine,Int32 _serLine)
        {

            DataTable _DT = null;

            txtInvNo.Text = "";
            txtAccNo.Text = "";
            pnl_CustDet.Update();

            if (chkOthLoc.Checked == false)
            {
                _DT = CHNLSVC.Inventory.GetIssuedWarranty(_seq,_itmLine,_batchLine,_serLine);
                if (_DT.Rows.Count > 0)
                {
                    txtInvNo.Text = _DT.Rows[0]["ith_oth_docno"].ToString();
                    txtAccNo.Text = _DT.Rows[0]["ith_acc_no"].ToString();
                    pnl_CustDet.Update();
                }

            }
            if (txtInvNo.Text != "")
            {
                DataTable _dtSales = null;
                _dtSales = CHNLSVC.Sales.GetInvDet(txtInvNo.Text);
                if (_dtSales.Rows.Count > 0)
                {
                    txtCusName.Text = _dtSales.Rows[0]["sah_cus_name"].ToString();
                    txtAddr.Text = _dtSales.Rows[0]["sah_cus_add1"].ToString();
                    txtInvDate.Text =Convert.ToDateTime( _dtSales.Rows[0]["sah_dt"]).ToShortDateString();
                }
            }
        }



    }
}