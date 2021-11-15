using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Globalization;
using System.Transactions;
using System.IO;
using System.Text.RegularExpressions;
using FF.BusinessObjects;

namespace FF.WebERPClient.HP_Module
{
    public partial class AccountReshedule : BasePage
    {


        protected MasterAutoNumber _ReqAppAuto
        {
            get { return (MasterAutoNumber)Session["_ReqAppAuto"]; }
            set { Session["_ReqAppAuto"] = value; }
        }

        protected RequestApprovalHeader _ReqAppHdr
        {
            get { return (RequestApprovalHeader)Session["_ReqAppHdr"]; }
            set { Session["_ReqAppHdr"] = value; }
        }

        protected RequestApprovalDetail _ReqAppDet
        {
            get { return (RequestApprovalDetail)Session["_ReqAppDet"]; }
            set { Session["_ReqAppDet"] = value; }
        }

        protected List<HpAccount> _AccountsList
        {
            get { return (List<HpAccount>)Session["_AccountsList"]; }
            set { Session["_AccountsList"] = value; }
        }

        protected List<HPResheScheme> _AllowSch
        {
            get { return (List<HPResheScheme>)Session["_AllowSch"]; }
            set { Session["_AllowSch"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //onblur
                txtAccSeq.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btn_validateAcc, ""));

                _AccountsList = new List<HpAccount>();
                _AllowSch = new List<HPResheScheme>();
                RequestApprovalHeader _ReqAppHdr = new RequestApprovalHeader();
                RequestApprovalDetail _ReqAppdet = new RequestApprovalDetail();
                MasterAutoNumber __ReqAppAuto = new MasterAutoNumber();
                Clear_Data();
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        private void Clear_Data()
        {
            txtAccSeq.Text = "";
            txtAccNo.Text = "";
            lblAccDate.Text = "";
            lblCurShe.Text = "";
            txtRemarks.Text = "";
            lblStatus.Text = "";
            txtReq.Text = "";
            txtfDate.Text = Convert.ToDateTime(DateTime.Now).Date.ToShortDateString();
            txttDate.Text = Convert.ToDateTime(DateTime.Now).Date.ToShortDateString();

            DataTable _Itemtable = new DataTable();
            gvPendingApp.DataSource = _Itemtable;
            gvPendingApp.DataBind();

            ddlScheme.DataSource = null;
            ddlScheme.DataBind();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            this.Clear_Data();
        }

        protected void btn_validateAcc_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAccSeq.Text))
            {
                ddlScheme.DataSource = null;
                ddlScheme.DataBind();

                string acc_seq = txtAccSeq.Text.Trim();
                List<HpAccount> accList = new List<HpAccount>();
                accList = CHNLSVC.Sales.GetHP_Accounts(GlbUserComCode, GlbUserDefProf, acc_seq, "A");
                _AccountsList = accList;//save in veiw state
                if (accList == null || accList.Count == 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Enter valid Account number!");
                    txtAccSeq.Text = null;
                    this.Clear_Data();
                    return;
                }
                else if (accList.Count == 1)
                {
                    //show summury
                    foreach (HpAccount ac in accList)
                    {
                        txtAccNo.Text = ac.Hpa_acc_no;
                        lblAccDate.Text = ac.Hpa_acc_cre_dt.ToShortDateString();
                        lblCurShe.Text = ac.Hpa_sch_cd;

                        //set UC values.
                        //uc_HpAccountSummary1.set_all_values(ac, GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, GlbUserDefProf);
                        //uc_HpAccountDetail1.Uc_hpa_acc_no = ac.Hpa_acc_no;
                    }
                }
                else if (accList.Count > 1)
                {
                    //show a pop up to select the account number
                    grvMpdalPopUp.DataSource = accList;
                    grvMpdalPopUp.DataBind();
                    ModalPopupExtItem.Show();
                }

                List<HPResheScheme> _def3 = CHNLSVC.Sales.getAllowSch(lblCurShe.Text);
                _AllowSch = new List<HPResheScheme>();
                if (_def3 != null)
                {
                    _AllowSch.AddRange(_def3);
                }


                var _final = (from _lst in _AllowSch
                              select _lst.Hsr_rsch_cd).ToList().Distinct();

                if (_final != null)
                {
                    ddlScheme.DataSource = _final;
                    ddlScheme.DataBind();
                }
                txtRemarks.Focus();
            }
        }

        protected void grvMpdalPopUp_SelectedIndexChanged(object sender, EventArgs e)
        {
            // string location = ddl_Location.SelectedValue;
            GridViewRow row = grvMpdalPopUp.SelectedRow;
            string accountNo = row.Cells[1].Text.Trim();
            ddlScheme.DataSource = null;
            ddlScheme.DataBind();

            txtAccNo.Text = accountNo;


            // set UC values.
            HpAccount account = new HpAccount();
            foreach (HpAccount acc in _AccountsList)
            {
                if (accountNo == acc.Hpa_acc_no)
                {
                    lblAccDate.Text = acc.Hpa_acc_cre_dt.ToShortDateString();
                    lblCurShe.Text = acc.Hpa_sch_cd;
                    account = acc;
                }
            }

            List<HPResheScheme> _def3 = CHNLSVC.Sales.getAllowSch(lblCurShe.Text);
            _AllowSch = new List<HPResheScheme>();
            if (_def3 != null)
            {
                _AllowSch.AddRange(_def3);
            }


            var _final = (from _lst in _AllowSch
                          select _lst.Hsr_rsch_cd).ToList().Distinct();

            if (_final != null)
            {
                ddlScheme.DataSource = _final;
                ddlScheme.DataBind();
            }

            //   uc_HpAccountSummary1.set_all_values(account, GlbUserDefProf, Convert.ToDateTime(txtDate.Text).Date, GlbUserDefProf);
            //   uc_HpAccountDetail1.Uc_hpa_acc_no = account.Hpa_acc_no;



        }

        protected void CollectReqApp()
        {
            _ReqAppHdr = new RequestApprovalHeader();
            _ReqAppDet = new RequestApprovalDetail();

            _ReqAppHdr.Grah_com = GlbUserComCode;
            _ReqAppHdr.Grah_loc = GlbUserDefProf;
            _ReqAppHdr.Grah_app_tp = "ARQT001";
            _ReqAppHdr.Grah_fuc_cd = txtAccNo.Text.Trim();
            _ReqAppHdr.Grah_ref = "1";
            _ReqAppHdr.Grah_oth_loc = txtAccSeq.Text.Trim();
            _ReqAppHdr.Grah_cre_by = GlbUserName;
            _ReqAppHdr.Grah_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_mod_by = GlbUserName;
            _ReqAppHdr.Grah_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_app_stus = "P";
            _ReqAppHdr.Grah_app_lvl = 0;
            _ReqAppHdr.Grah_app_by = string.Empty;
            _ReqAppHdr.Grah_app_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqAppHdr.Grah_remaks = txtRemarks.Text.Trim();


            _ReqAppDet.Grad_ref = "1";
            _ReqAppDet.Grad_line = 1;
            _ReqAppDet.Grad_req_param = "ACCOUNT_RESHEDULE";
            _ReqAppDet.Grad_val1 =Convert.ToInt32(txtAccSeq.Text);
            _ReqAppDet.Grad_val2 = 0;
            _ReqAppDet.Grad_val3 = 0;
            _ReqAppDet.Grad_val4 = 0;
            _ReqAppDet.Grad_val5 = 0;
            _ReqAppDet.Grad_anal1 = txtAccNo.Text.Trim();
            _ReqAppDet.Grad_anal2 = lblCurShe.Text.Trim();
            _ReqAppDet.Grad_anal3 = ddlScheme.Text.Trim();
            _ReqAppDet.Grad_anal4 = "";
            _ReqAppDet.Grad_anal5 = "";
            _ReqAppDet.Grad_date_param = Convert.ToDateTime(lblAccDate.Text).Date;
            _ReqAppDet.Grad_is_rt1 = false;
            _ReqAppDet.Grad_is_rt2 = false;


            _ReqAppAuto = new MasterAutoNumber();
            _ReqAppAuto.Aut_cate_cd = GlbUserDefProf;
            _ReqAppAuto.Aut_cate_tp = "PC";
            _ReqAppAuto.Aut_direction = 1;
            _ReqAppAuto.Aut_modify_dt = null;
            _ReqAppAuto.Aut_moduleid = "HP";
            _ReqAppAuto.Aut_number = 0;
            _ReqAppAuto.Aut_start_char = "HPRES";
            _ReqAppAuto.Aut_year = Convert.ToDateTime(DateTime.Now).Year;
        }

        protected void btnReq_Click(object sender, EventArgs e)
        {
            string _docNo = "";
            string _msg = string.Empty;

            CollectReqApp();

            int effet = CHNLSVC.Sales.SaveHPResheduleApp(_ReqAppHdr, _ReqAppDet, _ReqAppAuto, out _docNo);

            if (effet == 1)
            {

                string Msg = "<script>alert('Request Successfully Saved!');window.location = 'AccountReshedule.aspx';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Generated Nos: " + _docNo);
                Clear_Data();
            }
            else
            {
                if (!string.IsNullOrEmpty(_msg))
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _msg);
                }
                else
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Creation Fail.");
                }
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            gvPendingApp.DataSource = CHNLSVC.Sales.getReqbyType(GlbUserComCode, GlbUserDefProf, "ARQT001", Convert.ToDateTime(txtfDate.Text).Date, Convert.ToDateTime(txttDate.Text).Date);
            gvPendingApp.DataBind();

            if (gvPendingApp.Rows.Count == 0)
            {
                DataTable _table = new DataTable();
                gvPendingApp.DataSource = _table;
                gvPendingApp.DataBind();
            }
        }


        private void BindPendingRequestApproval(string RefNo = "")
        {
            RequestApprovalDetail _paramRequestApprovalDetails = new RequestApprovalDetail();
            List<RequestApprovalDetail> _Details = new List<RequestApprovalDetail>();
            _paramRequestApprovalDetails.Grad_ref = RefNo;

            _Details = CHNLSVC.General.GetRequestApprovalDetails(_paramRequestApprovalDetails);

            if (_Details != null)
            {
                foreach (RequestApprovalDetail Req in _Details)
                {
                    txtAccSeq.Text = Req.Grad_val1.ToString();
                    txtAccNo.Text = Req.Grad_anal1;
                    lblCurShe.Text = Req.Grad_anal2;
                    List<HPResheScheme> _def3 = CHNLSVC.Sales.getAllowSch(lblCurShe.Text);
                    _AllowSch = new List<HPResheScheme>();
                    if (_def3 != null)
                    {
                        _AllowSch.AddRange(_def3);
                    }


                    var _final = (from _lst in _AllowSch
                                  select _lst.Hsr_rsch_cd).ToList().Distinct();

                    if (_final != null)
                    {
                        ddlScheme.DataSource = _final;
                        ddlScheme.DataBind();
                    }

                    ddlScheme.Text = Req.Grad_anal3;
                    txtReq.Text = Req.Grad_ref;
                    lblAccDate.Text = Convert.ToDateTime(Req.Grad_date_param).ToShortDateString();

                }
            }

        }

        protected void gvPendingApp_Rowcommand(object sender, GridViewCommandEventArgs e)
        {
            string _docref = string.Empty;
            string _msg = string.Empty;
            string _status = string.Empty;

            switch (e.CommandName.ToUpper())
            {
                case "VIEW":
                    {
                        GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                        _docref = row.Cells[1].Text.ToString();
                        txtRemarks.Text = row.Cells[14].Text.ToString();
                        _status = row.Cells[10].Text.ToString();

                        if (_status == "P")
                        {
                            lblStatus.Text = "PENDING";
                        }
                        else if (_status == "R")
                        {
                            lblStatus.Text = "REJECT";
                        }
                        else if (_status == "A")
                        {
                            lblStatus.Text = "APPROVED";
                        }

                        
                        BindPendingRequestApproval(_docref);
                        break;
                    }
            }
        }

        protected void btnApp_Click(object sender, EventArgs e)
        {
            Int32 _rowEffect = 0;
            string _msg = string.Empty;

            if (string.IsNullOrEmpty(txtReq.Text) == true)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select request #.");
                return;
            }

            if (lblStatus.Text == "APPROVED")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Request is already approved.");
                return;
            }

            if (lblStatus.Text == "REJECT")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Request is already rejected.");
                return;
            }

            RequestApprovalHeader _RequestApprovalStatus = new RequestApprovalHeader();
            _RequestApprovalStatus.Grah_com = GlbUserComCode;
            _RequestApprovalStatus.Grah_loc = GlbUserDefProf;
            _RequestApprovalStatus.Grah_fuc_cd = txtAccNo.Text;
            _RequestApprovalStatus.Grah_ref = txtReq.Text;
            _RequestApprovalStatus.Grah_app_stus = "A";
            _RequestApprovalStatus.Grah_app_by = GlbUserName;
            _RequestApprovalStatus.Grah_app_lvl = 1;
            _rowEffect = CHNLSVC.General.UpdateApprovalStatus(_RequestApprovalStatus);


            if (_rowEffect == 1)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully approved.");
                this.Clear_Data();
                
            }
            else
            {
                if (!string.IsNullOrEmpty(_msg))
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _msg);
                }
                else
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Fail to approved.Please re-try");
                }
            }
        }

        protected void btnRej_Click(object sender, EventArgs e)
        {
            Int32 _rowEffect = 0;
            string _msg = string.Empty;

            if (string.IsNullOrEmpty(txtReq.Text) == true)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select request #.");
                return;
            }

            if (lblStatus.Text == "APPROVED")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Request is already approved.");
                return;
            }

            if (lblStatus.Text == "REJECT")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Request is already rejected.");
                return;
            }

            RequestApprovalHeader _RequestApprovalStatus = new RequestApprovalHeader();
            _RequestApprovalStatus.Grah_com = GlbUserComCode;
            _RequestApprovalStatus.Grah_loc = GlbUserDefProf;
            _RequestApprovalStatus.Grah_fuc_cd = txtAccNo.Text;
            _RequestApprovalStatus.Grah_ref = txtReq.Text;
            _RequestApprovalStatus.Grah_app_stus = "R";
            _RequestApprovalStatus.Grah_app_by = GlbUserName;
            _RequestApprovalStatus.Grah_app_lvl = 1;
            _rowEffect = CHNLSVC.General.UpdateApprovalStatus(_RequestApprovalStatus);


            if (_rowEffect == 1)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully rejected.");
                this.Clear_Data();

            }
            else
            {
                if (!string.IsNullOrEmpty(_msg))
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _msg);
                }
                else
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Fail to approved.Please re-try");
                }
            }
        }

    }
}