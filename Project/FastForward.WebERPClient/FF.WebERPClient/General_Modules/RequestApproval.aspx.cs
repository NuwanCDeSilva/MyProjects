using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Text;
using System.Transactions;
using System.Globalization;
using System.Data;

namespace FF.WebERPClient.Genaral_Modules
{
    public partial class RequestApproval : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                DataTable _table = new DataTable();
                _table.Columns.AddRange(new DataColumn[] { new DataColumn("podi_seq_no"), new DataColumn("podi_line_no")});
                gvPendingApp.DataSource = _table;
                gvPendingApp.DataBind();
                //Ad_hoc_sessions();
                BindPendingApproval();
                divDetails.Visible = false;
                divdetbtn.Visible = false;
            }
        }

        private void Ad_hoc_sessions()
        {
            Session["UserID"] = "ADMIN";
            Session["UserCompanyCode"] = "ABL";
            Session["SessionID"] = "666";
            Session["UserDefLoca"] = "MSR16";
            Session["UserDefProf"] = "39";

        }

        #region User Defined Methods

        private void BindPendingApproval()
        {
            RequestApprovalHeader _paramApproval = new RequestApprovalHeader();

            _paramApproval.Grah_com = GlbUserComCode;
            _paramApproval.Grah_app_stus = "P";
            _paramApproval.Grah_loc = GlbUserDefProf;

           

            gvPendingApp.DataSource = CHNLSVC.General.GetAllPendingRequest(_paramApproval);
            gvPendingApp.DataBind();

            if (gvPendingApp.Rows.Count == 0)
            {
                DataTable _table = new DataTable();
              //  _table.Columns.AddRange(new DataColumn[] { new DataColumn("podi_seq_no"), new DataColumn("podi_line_no") });
                gvPendingApp.DataSource = _table;
                gvPendingApp.DataBind();
            }

        }


        private void BindPendingRequestApproval(string RefNo = "")
        {
            RequestApprovalDetail _paramRequestApprovalDetails = new RequestApprovalDetail();
            _paramRequestApprovalDetails.Grad_ref = RefNo;

            dgvPendingDetails.DataSource = CHNLSVC.General.GetRequestApprovalDetails(_paramRequestApprovalDetails);
            dgvPendingDetails.DataBind();
        }
        #endregion

        protected void gvPendingApp_Rowcommand(object sender, GridViewCommandEventArgs e)
        {
            string _com = "";
            string _loc = "";
            string _type = "";
            string _docref = "";
            Int32 _rowEffect = 0;
            string _msg = string.Empty;

            switch (e.CommandName.ToUpper())
            {
                case "APPROVE":
                    {
                        GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                        _com = row.Cells[0].Text.ToString();
                        _loc = row.Cells[1].Text.ToString();
                        _type = row.Cells[3].Text.ToString();
                        _docref = row.Cells[4].Text.ToString();

                        RequestApprovalHeader _RequestApprovalStatus = new RequestApprovalHeader();
                        _RequestApprovalStatus.Grah_com = _com;
                        _RequestApprovalStatus.Grah_loc = _loc;
                        _RequestApprovalStatus.Grah_fuc_cd = _type;
                        _RequestApprovalStatus.Grah_ref = _docref;
                        _RequestApprovalStatus.Grah_app_stus = "A";
                        _RequestApprovalStatus.Grah_app_by = GlbUserName;
                        _RequestApprovalStatus.Grah_app_lvl = 1;
                        _rowEffect = CHNLSVC.General.UpdateApprovalStatus(_RequestApprovalStatus);
                       

                        if (_rowEffect == 1)
                        {
                            this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully approved.");
                            BindPendingApproval();
                            divDetails.Visible = false;
                            divdetbtn.Visible = false;
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
                        break;
                    }

                case "REJECT":
                    {
                        GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                        _com = row.Cells[0].Text.ToString();
                        _loc = row.Cells[1].Text.ToString();
                        _type = row.Cells[3].Text.ToString();
                        _docref = row.Cells[4].Text.ToString();

                        RequestApprovalHeader _RequestApprovalStatus = new RequestApprovalHeader();
                        _RequestApprovalStatus.Grah_com = _com;
                        _RequestApprovalStatus.Grah_loc = _loc;
                        _RequestApprovalStatus.Grah_fuc_cd = _type;
                        _RequestApprovalStatus.Grah_ref = _docref;
                        _RequestApprovalStatus.Grah_app_stus = "R";
                        _RequestApprovalStatus.Grah_app_by = GlbUserName;
                        _RequestApprovalStatus.Grah_app_lvl = 1;
                        _rowEffect = CHNLSVC.General.UpdateApprovalStatus(_RequestApprovalStatus);


                        if (_rowEffect == 1)
                        {
                            this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully reject.");
                            BindPendingApproval();
                            divDetails.Visible = false;
                            divdetbtn.Visible = false;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(_msg))
                            {
                                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _msg);
                            }
                            else
                            {
                                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Fail to reject.Please re-try");
                            }
                        }

                        break;
                    }

                case "VIEW":
                    {
                        GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                        _docref = row.Cells[4].Text.ToString();
                        BindPendingRequestApproval(_docref);
                        divDetails.Visible = true;
                        divdetbtn.Visible = true;
                        break;
                    }
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            divDetails.Visible = false;
            divdetbtn.Visible = false;
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindPendingApproval();
            divDetails.Visible = false;
            divdetbtn.Visible = false;
        }

    }
}