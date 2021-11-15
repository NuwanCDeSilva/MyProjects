using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Inventory
{
    public partial class DisposalEntry : Base
    {
        protected DisposalHeader oDisposalHeader { get { return (DisposalHeader)Session["oDisposalHeader"]; } set { Session["oDisposalHeader"] = value; } }
        protected List<DisposalLocation> oDisposalLocations { get { return (List<DisposalLocation>)Session["oDisposalLocations"]; } set { Session["oDisposalLocations"] = value; } }

        private List<InventoryRequestItem> ScanItemList = null;

        private List<string> SeqNumList = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateLoadingBays();
                loadPaymentType();
                clear();
                loadItemStatus();
            }
            else
            {
                if (Session["Ser"] != null && Session["Ser"].ToString() == "Ser")
                {
                    UserAdPopup.Show();
                }
                else
                {
                    UserAdPopup.Hide();
                }

                if (Session["mpSearchAdance"] != null && Session["mpSearchAdance"].ToString() == "mpSearchAdance")
                {
                    mpSearchAdance.Show();
                }
                else
                {
                    mpSearchAdance.Hide();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (hdfSaveCon.Value == "No")
            {
                return;
            }
            if (string.IsNullOrEmpty(txtMaxValue.Text.Trim()))
            {
                DisplayMessage("Enter maximum value");
                txtMaxValue.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtCharge.Text.Trim()))
            {
                DisplayMessage("Enter charge value");
                txtCharge.Focus();
                return;
            }
            if (ddlPaymentType.SelectedIndex == 0)
            {
                DisplayMessage("Select payment type");
                txtCharge.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtReference.Text))
            {
                DisplayMessage("Please add the reference number");
                txtReference.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtRemark.Text))
            {
                DisplayMessage("Please add the remark");
                txtReference.Focus();
                return;
            }

            if (oDisposalLocations == null || oDisposalLocations.Count == 0)
            {
                if (dgvLocations.Rows.Count > 0)
                {
                    oDisposalLocations = CHNLSVC.Inventory.GET_DISPOSAL_LOCATIONS(oDisposalHeader.Dh_seq);
                    if (oDisposalLocations == null || oDisposalLocations.Count == 0)
                    {
                        DisplayMessage("Add locations");
                        txtLocation.Focus();

                        dgvLocations.DataSource = new int[] { };
                        dgvLocations.DataBind();
                        return;
                    }
                }
                else
                {
                    DisplayMessage("Add locations");
                    txtLocation.Focus();
                    return;
                }
            }
            if (txtRemark.Text.Length > 150)
            {
                DisplayMessage("Remark length is " + txtRemark.Text.Length.ToString() + ". Please reduce.");
                txtLocation.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtJobNumber.Text))
            {
                oDisposalHeader = new DisposalHeader();
                oDisposalHeader.Dh_seq = 0;
                oDisposalHeader.Dh_doc_no = string.Empty;
            }
            else
            {
                oDisposalHeader.Dh_doc_no = txtJobNumber.Text.Trim();
            }

            if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16034))
            {
                if (ddlPaymentType.SelectedValue.ToString() == "PRD" && string.IsNullOrEmpty(txtReceipt.Text.Trim()) && string.IsNullOrEmpty(txtJobNumber.Text))
                {
                    DisplayMessage("Please select the post delivery type.");
                    //DisplayMessage("Please complete the receipt details");
                    return;
                }
            }

            if (ddlPaymentType.SelectedValue.ToString() == "PRD" && string.IsNullOrEmpty(txtReceipt.Text.Trim()) && !string.IsNullOrEmpty(txtJobNumber.Text) && grdItems.Rows.Count > 0)
            {
                DisplayMessage("Please complete the receipt details");
                return;
            }

            oDisposalHeader.Dh_com = Session["UserCompanyCode"].ToString();
            oDisposalHeader.Dh_pc = Session["UserDefProf"].ToString();
            oDisposalHeader.Dh_doc_dt = Convert.ToDateTime(txtDate.Text.Trim());
            oDisposalHeader.Dh_frm_dt = Convert.ToDateTime(txtValidFrom.Text.Trim());
            oDisposalHeader.Dh_to_dt = Convert.ToDateTime(txtValidTo.Text.Trim());
            oDisposalHeader.Dh_ref_no = txtReference.Text.Trim();
            oDisposalHeader.Dh_max_val = Convert.ToDecimal(txtMaxValue.Text.Trim());
            oDisposalHeader.Dh_chk_max_val = (chkRestrict.Checked) ? 1 : 0;
            oDisposalHeader.Dh_chg = Convert.ToDecimal(txtCharge.Text.Trim());
            oDisposalHeader.Dh_pay_mtd = ddlPaymentType.SelectedValue.ToString();
            oDisposalHeader.Dh_recipt_no = txtReceipt.Text.Trim();
            oDisposalHeader.Dh_rmk = txtRemark.Text.Trim();
            oDisposalHeader.Dh_stus = "P";
            oDisposalHeader.Dh_anal_1 = string.Empty;
            oDisposalHeader.Dh_anal_2 = string.Empty;
            oDisposalHeader.Dh_anal_3 = string.Empty;
            oDisposalHeader.Dh_anal_4 = string.Empty;
            oDisposalHeader.Dh_anal_5 = string.Empty;
            oDisposalHeader.Dh_cre_by = Session["UserID"].ToString();
            oDisposalHeader.Dh_cre_dt = DateTime.Now;
            oDisposalHeader.Dh_cre_session = Session["SessionID"].ToString();
            oDisposalHeader.Dh_mod_by = null;
            oDisposalHeader.Dh_mod_dt = null;
            oDisposalHeader.Dh_mod_session = null;

            MasterAutoNumber mastAutoNo = new MasterAutoNumber();
            mastAutoNo.Aut_cate_cd = Session["UserCompanyCode"].ToString();
            mastAutoNo.Aut_cate_tp = "COM";
            mastAutoNo.Aut_direction = null;
            mastAutoNo.Aut_modify_dt = null;
            mastAutoNo.Aut_moduleid = "DISP";
            mastAutoNo.Aut_start_char = "DISP";
            mastAutoNo.Aut_year = Convert.ToInt32(DateTime.Now.Year);

            string err = string.Empty;
            string docNo = string.Empty;

            #region AJD process

            bool IsTemp = false;
            TextBox txtSubType = new TextBox(); txtSubType.Text = "DISPO";
            TextBox txtRef = new TextBox(); txtRef.Text = txtJobNumber.Text;
            TextBox txtRemarks = new TextBox(); txtRemarks.Text = txtRemark.Text;

            if (CheckServerDateTime() == false) return;

            if (string.IsNullOrEmpty(txtSubType.Text))
            {
                DisplayMessage("Select the adjustment sub type");
                return;
            }

            if (string.IsNullOrEmpty(txtRef.Text)) txtRef.Text = "N/A";
            if (string.IsNullOrEmpty(txtOtherRef.Text)) txtOtherRef.Text = string.Empty;
            if (string.IsNullOrEmpty(txtRemarks.Text)) txtRemarks.Text = string.Empty;

            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty.ToUpper().ToString(), Session["GlbModuleName"].ToString(), txtDate, lblH1, Convert.ToDateTime(txtDate.Text).ToShortDateString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (Convert.ToDateTime(txtDate.Text).Date != DateTime.Now.Date)
                    {
                        txtDate.Enabled = true;
                        DisplayMessage("Selected date not allowed for transaction");
                        txtDate.Focus();
                        return;
                    }
                }
                else
                {
                    txtDate.Enabled = true;
                    DisplayMessage("Selected date not allowed for transaction");
                    txtDate.Focus();
                    return;
                }
            }

            List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
            List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
            string documntNo = "";
            Int32 result = -99;
            Int32 _userSeqNo = 0;
            int _direction = 0;
            _direction = 0;

            if (string.IsNullOrEmpty(txtUserSeqNo.Text))
            {
                txtUserSeqNo.Text = "0";
            }

            _userSeqNo = Convert.ToInt32(txtUserSeqNo.Text);
            reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "DISP");
            reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "DISP");

            //_userSeqNo = CHNLSVC.Inventory.Get_Scan_SeqNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "DISP", Session["UserID"].ToString(), _direction, txtUserSeqNo.Text);

            //if (_userSeqNo != -1)
            //{
            //    _userSeqNo = Convert.ToInt32(txtUserSeqNo.Text);
            //    reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "DISP");
            //    reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "DISP");
            //}

            //Check MaxValue
            if (chkRestrict.Checked && reptPickSerialsList != null)
            {
                if (!string.IsNullOrEmpty(txtMaxValue.Text))
                {
                    if (Convert.ToDecimal(txtMaxValue.Text) > 0)
                    {
                        //if (!checkMaxValus(reptPickSerialsList))
                        //{
                        //    DisplayMessage("Total item value can not exceed the maximum value.");
                        //    return;
                        //}
                    }
                    else
                    {
                        DisplayMessage("Please enter a value for max value");
                        return;
                    }
                }
                else
                {
                    DisplayMessage("Please enter the max value");
                    txtMaxValue.Focus();
                    return;
                }

            }

            #region Check Reference Date and the Doc Date

            if (_direction == 0)
            {
                if (IsReferancedDocDateAppropriate(reptPickSerialsList, Convert.ToDateTime(txtDate.Text).Date) == false)
                {
                    return;
                }
            }

            #endregion

            #region Check Duplicate Serials

            if (reptPickSerialsList != null && reptPickSerialsList.Count > 0)
            {
                var _dup = reptPickSerialsList.Where(x => x.Tus_ser_1 != "N/A" && x.Tus_ser_1 != "").Select(y => y.Tus_ser_id).ToList();

                string _duplicateItems = string.Empty;
                bool _isDuplicate = false;
                if (_dup != null)
                    if (_dup.Count > 0)
                        foreach (Int32 _id in _dup)
                        {
                            Int32 _counts = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(y => y.Tus_ser_id).Count();
                            if (_counts > 1)
                            {
                                _isDuplicate = true;
                                var _item = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(x => x.Tus_itm_cd).Distinct();
                                foreach (string _str in _item)
                                    if (string.IsNullOrEmpty(_duplicateItems))
                                        _duplicateItems = _str;
                                    else
                                        _duplicateItems += "," + _str;
                            }
                        }
                if (_isDuplicate)
                {
                    DisplayMessage("Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems);
                    return;
                }
            }
            #endregion
            InventoryHeader inHeader = new InventoryHeader();
            #region Fill InventoryHeader
            DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
            foreach (DataRow r in dt_location.Rows)
            {
                // Get the value of the wanted column and cast it to string
                inHeader.Ith_sbu = (string)r["ML_OPE_CD"];
                if (System.DBNull.Value != r["ML_CATE_2"])
                {
                    inHeader.Ith_channel = (string)r["ML_CATE_2"];
                }
                else
                {
                    inHeader.Ith_channel = string.Empty;
                }
            }
            inHeader.Ith_acc_no = "STOCK_ADJ";
            inHeader.Ith_anal_1 = "";

            inHeader.Ith_anal_3 = "";
            inHeader.Ith_anal_4 = "";
            inHeader.Ith_anal_5 = "";
            inHeader.Ith_anal_6 = 0;
            inHeader.Ith_anal_7 = 0;
            inHeader.Ith_anal_8 = DateTime.MinValue;
            inHeader.Ith_anal_9 = DateTime.MinValue;
            //if (Session["DocType"].ToString() == "TempDoc")
            //{
            //    inHeader.Ith_anal_10 = true;
            //    inHeader.Ith_anal_2 = txtJobNumber.Text;
            //}
            //else

            {
                inHeader.Ith_anal_10 = false;
                inHeader.Ith_anal_2 = "";
            }

            inHeader.Ith_anal_11 = false;
            inHeader.Ith_anal_12 = false;
            inHeader.Ith_bus_entity = "";
            inHeader.Ith_cate_tp = txtSubType.Text.ToString().Trim();
            inHeader.Ith_com = Session["UserCompanyCode"].ToString();
            inHeader.Ith_com_docno = "";
            inHeader.Ith_cre_by = Session["UserID"].ToString();
            inHeader.Ith_cre_when = DateTime.Now;
            inHeader.Ith_del_add1 = "";
            inHeader.Ith_del_add2 = "";
            inHeader.Ith_del_code = "";
            inHeader.Ith_del_party = "";
            inHeader.Ith_del_town = "";

            inHeader.Ith_direct = false;
            inHeader.Ith_doc_tp = "ADJ";

            inHeader.Ith_doc_date = Convert.ToDateTime(txtDate.Text).Date;
            inHeader.Ith_doc_no = string.Empty;

            inHeader.Ith_doc_year = Convert.ToDateTime(txtDate.Text).Year;
            if (IsTemp == true)
            {
                inHeader.Ith_entry_no = txtUserSeqNo.Text;
            }
            else
            {
                inHeader.Ith_entry_no = "";
            }
            inHeader.Ith_entry_tp = txtSubType.Text.ToString().Trim();
            inHeader.Ith_git_close = true;
            inHeader.Ith_git_close_date = DateTime.MinValue;
            inHeader.Ith_git_close_doc = string.Empty;
            inHeader.Ith_isprinted = false;
            inHeader.Ith_is_manual = false;
            inHeader.Ith_job_no = string.Empty;
            inHeader.Ith_loading_point = string.Empty;
            inHeader.Ith_loading_user = string.Empty;
            inHeader.Ith_loc = Session["UserDefLoca"].ToString();
            inHeader.Ith_manual_ref = txtRef.Text.Trim();
            inHeader.Ith_mod_by = Session["UserID"].ToString();
            inHeader.Ith_mod_when = DateTime.Now;
            inHeader.Ith_noofcopies = 0;
            inHeader.Ith_oth_loc = string.Empty;
            inHeader.Ith_oth_docno = txtOtherRef.Text.Trim();
            inHeader.Ith_remarks = txtRemarks.Text;

            //inHeader.Ith_seq_no = 6; removed by Chamal 12-05-2013
            inHeader.Ith_session_id = Session["SessionID"].ToString();
            inHeader.Ith_stus = "A";
            inHeader.Ith_sub_tp = txtSubType.Text.ToString().Trim();
            inHeader.Ith_vehi_no = string.Empty;
            inHeader.Ith_anal_3 = "";//ddlDeliver.SelectedItem.Text;

            #endregion
            MasterAutoNumber masterAuto = new MasterAutoNumber();

            #region Fill MasterAutoNumber

            masterAuto.Aut_cate_cd = Session["UserDefLoca"].ToString();
            masterAuto.Aut_cate_tp = "LOC";
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "DISPO";
            masterAuto.Aut_number = 5;//what is Aut_number
            masterAuto.Aut_start_char = "DISPO";
            masterAuto.Aut_year = null;

            #endregion

            int _line = 0;
            #region Update some serial items
            if (reptPickSerialsList != null)
            {
                if (_direction == 1)
                {
                    foreach (var _seritem in reptPickSerialsList)
                    {
                        _seritem.Tus_exist_grncom = Session["UserCompanyCode"].ToString();
                        _seritem.Tus_exist_grndt = Convert.ToDateTime(txtDate.Text).Date;
                        _seritem.Tus_orig_grncom = Session["UserCompanyCode"].ToString();
                        _seritem.Tus_orig_grndt = Convert.ToDateTime(txtDate.Text).Date;
                    }
                }
                else if (_direction == 0)
                {
                    foreach (var _ADJSer in reptPickSerialsList)
                    {
                        _line = _line + 1;
                        _ADJSer.Tus_base_itm_line = _line;
                    }
                }
            }
            #endregion


            if (reptPickSerialsList != null && reptPickSerialsList.Count > 0 && string.IsNullOrEmpty(txtReceipt.Text))
            {
                oDisposalHeader.Dh_stus = "A";
            }
            if (reptPickSerialsList != null && reptPickSerialsList.Count > 0 && !string.IsNullOrEmpty(txtReceipt.Text))
            {
                oDisposalHeader.Dh_stus = "F";
            }

            if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10119) && !string.IsNullOrEmpty(txtReceipt.Text) && txtStatus.Text == "Receipt Saved")
            {
                oDisposalHeader.Dh_stus = "S";
            }


            oDisposalHeader.Dh_anal_5 = _userSeqNo.ToString();

            result = CHNLSVC.Inventory.DisposalAdjustmentWithJobSave(inHeader, reptPickSerialsList, reptPickSubSerialsList, masterAuto, out documntNo, oDisposalHeader, oDisposalLocations, mastAutoNo, out err, out docNo, IsTemp);

            #endregion

            string Doc_ADJ = documntNo;
            string Doc_DISP = docNo;

            //Int32 result = CHNLSVC.Inventory.Save_Disposal_Job(oDisposalHeader, oDisposalLocations, mastAutoNo, out err, out docNo);
            if (result > 0)
            {
                string msg = string.Empty;

                if (string.IsNullOrEmpty(txtJobNumber.Text))
                {
                    if (!string.IsNullOrEmpty(Doc_ADJ))
                    {
                        msg = "Saved. Disposal Number : " + Doc_DISP + ". Document Number : " + Doc_ADJ;
                    }
                    else
                    {
                        msg = "Saved. Disposal Number : " + Doc_DISP + ".";
                        sendApprovedMails(Doc_DISP);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(Doc_ADJ))
                    {
                        msg = "Updated the disposal number : " + Doc_DISP + ". Document Number : " + Doc_ADJ;
                    }
                    else
                    {
                        msg = "Updated the disposal number : " + Doc_DISP + ".";
                    }
                }

                DisplayMessage(msg, 3);

                clear();
                return;
            }
            else
            {
                //DisplayMessage("Error occurred while processing.");
                DisplayMessage(documntNo, 4);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void btnJobNumber_Click(object sender, EventArgs e)
        {

        }

        private void clear()
        {
            oDisposalHeader = new DisposalHeader();
            oDisposalLocations = new List<DisposalLocation>();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "CollapseHide()", true);

            txtFrom.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
            txtTo.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtJobNumber.Text = "";

            txtDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtJobNumberPending.Text = "";
            txtValidFrom.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
            txtValidTo.Text = DateTime.Now.ToString("dd/MMM/yyyy");

            txtMaxValue.Text = "";
            txtCharge.Text = "";
            txtReference.Text = "";
            ddlPaymentType.SelectedIndex = 0;
            txtRemark.Text = "";
            txtReceipt.Text = "";
            txtLocation.Text = "";
            txtLocDesc.Text = "";

            ddlPaymentType.Enabled = true;

            dgvPendingJobs.DataSource = new int[] { };
            dgvPendingJobs.DataBind();

            dgvLocations.DataSource = new int[] { };
            dgvLocations.DataBind();

            //clearItems();

            ucOutScan.isApprovalSend = false;
            ucOutScan.adjustmentTypeValue = "-";
            ucOutScan.PNLTobechange.Visible = false;
            ucOutScan.PNLTobechange.Visible = false;
            ucOutScan.PageClear();
            ucOutScan.doc_tp = "DISP";
            ucOutScan.ScanItemList = null;
            ucOutScan.ListClear();
            ucOutScan.userSeqNo = null;

            grdItems.DataSource = new int[] { };
            grdItems.DataBind();

            grdSerial.DataSource = new int[] { };
            grdSerial.DataBind();

            lblH1.Text = "";
            lblBackDateInfor.Text = "";

            txtUserSeqNo.Text = "";
            lblJobNumber.ToolTip = "";

            Session["GlbModuleName"] = "m_Trans_Inventory_DisposalEntry";

            DataTable dtchk_warehouse_availability = CHNLSVC.Inventory.CheckWareHouseAvailability(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
            if (dtchk_warehouse_availability.Rows.Count > 0)
            {
                chkSendToPDA.Enabled = true;

                foreach (DataRow ddrware in dtchk_warehouse_availability.Rows)
                {
                    Session["WAREHOUSE_COM"] = ddrware["ml_wh_com"].ToString();
                    Session["WAREHOUSE_LOC"] = ddrware["ml_wh_cd"].ToString();
                }
            }
            else
            {
                chkSendToPDA.Enabled = false;
            }

            cpe.Collapsed = false;
            pnlJobSave.Enabled = false;
            pnlUserControl.Enabled = true;

            btnSave.Enabled = true;
            btnSave.CssClass = "buttonUndocolor";
            btnSave.OnClientClick = "return confSave()";

            btnUpdate.Enabled = false;
            btnUpdate.CssClass = "buttoncolor";
            btnUpdate.OnClientClick = "";

            if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10119))
            {
                cpe.Collapsed = true;
                pnlJobSave.Enabled = true;

                btnSave.Enabled = true;
                btnSave.CssClass = "buttonUndocolor";
                btnSave.OnClientClick = "return confSave()";
                pnlUserControl.Enabled = false;
            }

            if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16034))
            {
                cpe.Collapsed = true;
                pnlJobSave.Enabled = true;
                pnlUserControl.Enabled = true;

                btnSave.Enabled = true;
                btnSave.CssClass = "buttonUndocolor";
                btnSave.OnClientClick = "return confSave()";



                ddlPaymentType.SelectedValue = "POD";
                ddlPaymentType.Enabled = false;
            }

            txtStatus.Text = "";
            chkRestrict.Checked = false;

        }

        private void loadPaymentType()
        {
            List<ComboBoxObject> oItems = new List<ComboBoxObject>();
            ComboBoxObject oSelect = new ComboBoxObject();
            oSelect.Text = "Select";
            oSelect.Value = "0";
            oItems.Add(oSelect);

            ComboBoxObject oItem1 = new ComboBoxObject();
            oItem1.Text = "Prior Delivery";
            oItem1.Value = "PRD";
            oItems.Add(oItem1);

            ComboBoxObject oItem2 = new ComboBoxObject();
            oItem2.Text = "Post Delivery";
            oItem2.Value = "POD";
            oItems.Add(oItem2);

            ddlPaymentType.DataSource = oItems;
            ddlPaymentType.DataTextField = "Text";
            ddlPaymentType.DataValueField = "Value";
            ddlPaymentType.DataBind();
        }

        #region Search

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InsuCom:
                    {
                        paramsText.Append("INS" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InsuPolicy:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator); break;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DisposalJOb:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator); break;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocSubType:
                    {
                        paramsText.Append("ADJ" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + txtOtherRef.Text);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void btnSchAdvClose_Click(object sender, EventArgs e)
        {
            Session["mpSearchAdance"] = null;
            txtSearchbyword.Text = "";
            mpSearchAdance.Hide();
        }

        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Session["SHWSECH"] = "123";
                mpSearchAdance.Show();
                if (!string.IsNullOrEmpty(txtSearchbyword.Text))
                {
                    FilterData();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
            }
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                if (lblvalue.Text == "Location")
                {
                    txtLocation.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtLocation_TextChanged(null, null);
                }
                if (lblvalue.Text == "DisposalJOb")
                {
                    txtJobNumber.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtJobNumber_TextChanged(null, null);
                }

                ViewState["SEARCH"] = null;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
            }
        }

        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdResult.PageIndex = e.NewPageIndex;
                grdResult.DataSource = null;
                grdResult.DataSource = (DataTable)ViewState["SEARCH"];
                grdResult.DataBind();
                mpSearchAdance.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
            }
        }

        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }

        private void FilterData()
        {
            try
            {
                if (lblvalue.Text == "Location")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "Location";
                    ViewState["SEARCH"] = _result;
                    mpSearchAdance.Show();
                    return;
                }
                else if (lblvalue.Text == "DisposalJOb")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DisposalJOb);
                    DataTable _result = CHNLSVC.CommonSearch.SEARCH_DISPOSAL_JOB(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString(), Convert.ToDateTime(txtFDate.Text.Trim()).Date, Convert.ToDateTime(txtTDate.Text.Trim()).Date);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "DisposalJOb";
                    ViewState["SEARCH"] = _result;
                    mpSearchAdance.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
                mpSearchAdance.Show();
            }
        }

        #endregion

        private void DisplayMessage(String Msg, Int32 option, Exception ex = null)
        {
            Msg = Msg.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ").Replace("\r", "");
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + Msg + "');", true);
                WriteErrLog(Msg, ex);
            }
        }

        private void DisplayMessage(String Msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
        }

        private void DisplayMessageJS(String Msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + Msg + "');", true);
        }

        private void WriteErrLog(string err, Exception ex)
        {
            using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/data.txt"), true))
            {
                if (ex == null)
                {
                    _testData.WriteLine(DateTime.Now.ToString("dd/MMM/yyyy  hh:mm:ss tt") + " - " + GetIPAddress() + " | " + err + "   || Exception : " + err);
                }
                else
                {
                    _testData.WriteLine(DateTime.Now.ToString("dd/MMM/yyyy  hh:mm:ss tt") + " - " + GetIPAddress() + " | " + err + "   || Exception : " + ex.Message);

                }
            }
        }

        private string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        private void selDatePanal(bool isEnable)
        {
            if (isEnable)
            {
                pnlSearchByDate.Enabled = true;
                lbtnTDate.CssClass = "buttonUndocolor";
                lbtnFDate.CssClass = "buttonUndocolor";
                CalendarExtender1.Enabled = true;
                CalendarExtender3.Enabled = true;
            }
            else
            {
                pnlSearchByDate.Enabled = false;
                lbtnTDate.CssClass = "buttoncolor";
                lbtnFDate.CssClass = "buttoncolor";
                CalendarExtender1.Enabled = false;
                CalendarExtender3.Enabled = false;
            }
        }

        protected void btnLocation_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Location";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearchAdance.Show();
                txtSearchbyword.Focus();
                selDatePanal(false);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
            }
        }

        protected void txtLocation_TextChanged(object sender, EventArgs e)
        {
            DataTable _tbl = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), txtLocation.Text.ToUpper().Trim());
            if (_tbl == null || _tbl.Rows.Count <= 0)
            {
                DisplayMessage("Dispatch location is invalid. Please check the location.");
                txtLocation.Text = "";
                txtLocation.Focus();
                return;
            }
            else
            {
                txtLocDesc.Text = _tbl.Rows[0]["ML_LOC_DESC"].ToString();
                btnAddLocation.Focus();
            }
        }

        protected void btnAddLocation_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLocation.Text.Trim()))
            {
                DisposalLocation oItem = new DisposalLocation();
                oItem.Dl_act = 1;
                oItem.Dl_cre_by = Session["UserID"].ToString();
                oItem.Dl_cre_dt = DateTime.Now;
                oItem.Dl_cre_session = Session["SessionID"].ToString();
                oItem.Dl_doc_no = txtJobNumber.Text.Trim();
                oItem.Dl_loc = txtLocation.Text.ToUpper().Trim();
                oItem.Dl_loc_Desc = txtLocDesc.Text.Trim();
                oItem.Dl_seq = 0;

                oDisposalLocations.Add(oItem);
                BindLocation();
                ClearLocLine();
            }
            else
            {
                DisplayMessage("Enter location code");
                txtLocation.Focus();
            }
        }

        private void BindLocation()
        {
            dgvLocations.DataSource = oDisposalLocations;
            dgvLocations.DataBind();
        }

        private void ClearLocLine()
        {

            txtLocation.Text = "";
            txtLocDesc.Text = "";
            txtLocation.Focus();
        }

        private void getJobDetails(string status)
        {
            oDisposalHeader = CHNLSVC.Inventory.GET_DISPOSAL_JOB_HEADER(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtJobNumber.Text.Trim(), status);
            if (oDisposalHeader == null || string.IsNullOrEmpty(oDisposalHeader.Dh_doc_no))
            {
                DisplayMessage("Enter valid job number");
                txtJobNumber.Text = "";
                txtJobNumber.Focus();
                return;
            }
            else
            {
                LoadAllDetails();

                DataTable dtdoccheck = CHNLSVC.Inventory.IsDocNoAvailable(oDisposalHeader.Dh_doc_no, "DISP", 0, Session["UserCompanyCode"].ToString());
                if (dtdoccheck != null && dtdoccheck.Rows.Count > 0)
                {
                    txtUserSeqNo.Text = dtdoccheck.Rows[0]["tuh_usrseq_no"].ToString();
                    lblJobNumber.ToolTip = txtUserSeqNo.Text;
                    //Session["GEN_SEQ"] = txtUserSeqNo.Text;
                    //Session["POPUP_CRN_RETURN"] = txtUserSeqNo.Text;
                    ucOutScan.userSeqNo = txtUserSeqNo.Text;
                }
                else
                {
                    txtUserSeqNo.Text = "";
                    lblJobNumber.ToolTip = txtUserSeqNo.Text;
                }
                if (!btnSave.Enabled && txtStatus.Text == "Approved")
                {
                    LoadAdjestmentDetails();
                }
                else
                {
                    LoadItems(txtUserSeqNo.Text);
                }

                if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16034) && !string.IsNullOrEmpty(txtJobNumber.Text) && grdItems.Rows.Count == 0)
                {
                    ddlPaymentType.Enabled = true;
                }
                if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16034) && !string.IsNullOrEmpty(txtJobNumber.Text) && grdItems.Rows.Count > 0 && !string.IsNullOrEmpty(txtReceipt.Text))
                {
                    ddlPaymentType.Enabled = false;
                }

                if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16034) && !string.IsNullOrEmpty(txtJobNumber.Text) && !string.IsNullOrEmpty(txtReceipt.Text))
                {
                    ddlPaymentType.Enabled = false;
                }
                if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16034) && !string.IsNullOrEmpty(txtJobNumber.Text) && txtStatus.Text != "Approved")
                {
                    btnUpdate.Enabled = true;
                    btnUpdate.CssClass = "buttonUndocolor";
                    btnUpdate.OnClientClick = "return confirm('Do you want to update')";
                }

                if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10119) && !string.IsNullOrEmpty(txtReceipt.Text) && txtStatus.Text == "Receipt Saved")
                {
                    ddlPaymentType.Enabled = false;
                }
            }
        }

        private void LoadAllDetails()
        {
            //btnSave.Enabled = true;
            //btnSave.OnClientClick = "return confSave();";
            //btnSave.CssClass = "buttonUndocolor";

            txtJobNumber.Text = oDisposalHeader.Dh_doc_no;
            txtDate.Text = oDisposalHeader.Dh_doc_dt.ToString("dd/MMM/yyyy");
            txtValidFrom.Text = oDisposalHeader.Dh_frm_dt.ToString("dd/MMM/yyyy");
            txtValidTo.Text = oDisposalHeader.Dh_to_dt.ToString("dd/MMM/yyyy");
            txtReference.Text = oDisposalHeader.Dh_ref_no;
            txtMaxValue.Text = oDisposalHeader.Dh_max_val.ToString("N2");
            if (oDisposalHeader.Dh_chk_max_val == 1)
            {
                chkRestrict.Checked = true;
            }
            else
            {
                chkRestrict.Checked = false;
            }
            txtCharge.Text = oDisposalHeader.Dh_chg.ToString("N2");
            ddlPaymentType.SelectedValue = oDisposalHeader.Dh_pay_mtd;
            txtReceipt.Text = oDisposalHeader.Dh_recipt_no;
            txtRemark.Text = oDisposalHeader.Dh_rmk;

            oDisposalLocations = CHNLSVC.Inventory.GET_DISPOSAL_LOCATIONS(oDisposalHeader.Dh_seq);
            if (oDisposalLocations != null && oDisposalLocations.Count > 0)
            {
                BindLocation();
            }

            if (oDisposalHeader.Dh_stus == "P")
            {
                txtStatus.Text = "Pending";
            }
            else if (oDisposalHeader.Dh_stus == "A")
            {
                txtStatus.Text = "Approved";
                btnSave.Enabled = false;
                btnSave.OnClientClick = "";
                btnSave.CssClass = "buttoncolor";

                btnUpdate.Enabled = false;
                btnUpdate.OnClientClick = "";
                btnUpdate.CssClass = "buttoncolor";
            }
            else if (oDisposalHeader.Dh_stus == "F")
            {
                txtStatus.Text = "Finished";
            }
            else if (oDisposalHeader.Dh_stus == "S")
            {
                txtStatus.Text = "Receipt Saved";
            }

            if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16034) && !string.IsNullOrEmpty(txtReceipt.Text))
            {
                ddlPaymentType.Enabled = false;
            }
            else
            {
                ddlPaymentType.Enabled = true;
            }


        }

        protected void btnDeleteLocation_Click(object sender, EventArgs e)
        {
            GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
            Label lblDL_LOC = dr.FindControl("lblDL_LOC") as Label;

            if (oDisposalLocations.FindAll(x => x.Dl_loc == lblDL_LOC.Text.Trim()).Count > 0)
            {
                oDisposalLocations.RemoveAll(x => x.Dl_loc == lblDL_LOC.Text.Trim());
            }

            BindLocation();
            ClearLocLine();
        }

        protected void btnJobnumber_Click1(object sender, EventArgs e)
        {
            try
            {
                txtFDate.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
                txtTDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DisposalJOb);
                DataTable result = CHNLSVC.CommonSearch.SEARCH_DISPOSAL_JOB(SearchParams, null, null, DateTime.Now, DateTime.Now);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "DisposalJOb";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearchAdance.Show();
                txtSearchbyword.Focus();
                selDatePanal(true);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
                mpSearchAdance.Show();
            }
        }

        protected void txtJobNumber_TextChanged(object sender, EventArgs e)
        {
            getJobDetails("ALL");
        }

        protected void btnSearchSearchPending_Click(object sender, EventArgs e)
        {
            DisposalHeader oFilter = new DisposalHeader();
            oFilter.Dh_com = Session["UserCompanyCode"].ToString();
            oFilter.Dh_pc = Session["UserDefProf"].ToString();
            oFilter.Dh_stus = "P";
            oFilter.Dh_frm_dt = Convert.ToDateTime(txtFrom.Text.Trim());
            oFilter.Dh_to_dt = Convert.ToDateTime(txtTo.Text.Trim());
            oFilter.Dh_doc_no = txtJobNumberPending.Text.Trim();

            List<DisposalHeader> oDisposalHeader = CHNLSVC.Inventory.GET_DISPOSAL_JOBS(oFilter);
            oFilter.Dh_stus = "S";
            if (oDisposalHeader == null)
            {
                oDisposalHeader = new List<DisposalHeader>();
            }

            List<DisposalHeader> oDisposalHeader2 = CHNLSVC.Inventory.GET_DISPOSAL_JOBS(oFilter);
            if (oDisposalHeader2 != null && oDisposalHeader2.Count > 0)
            {
                oDisposalHeader.AddRange(CHNLSVC.Inventory.GET_DISPOSAL_JOBS(oFilter));
            }

            oFilter.Dh_stus = "A";
            List<DisposalHeader> oDisposalHeader3 = CHNLSVC.Inventory.GET_DISPOSAL_JOBS(oFilter);
            if (oDisposalHeader3 != null && oDisposalHeader3.Count > 0)
            {
                oDisposalHeader.AddRange(oDisposalHeader3);
            }

            if (oDisposalHeader != null && oDisposalHeader.Count > 0)
            {
                oDisposalHeader = oDisposalHeader.OrderBy(x => x.Dh_doc_dt).ToList();
                dgvPendingJobs.DataSource = oDisposalHeader;
                dgvPendingJobs.DataBind();
            }
            else
            {
                DisplayMessage("Cannot find pending jobs");
                return;
            }
        }

        protected void btnPendingJObSelect_Click(object sender, EventArgs e)
        {
            if (!chkSendToPDA.Checked)
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                LinkButton btnPendingJObSelect = dr.FindControl("btnPendingJObSelect") as LinkButton;
                txtJobNumber.Text = btnPendingJObSelect.Text.Trim();
                getJobDetails("ALL");
            }
            else
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                LinkButton btnPendingJObSelect = dr.FindControl("btnPendingJObSelect") as LinkButton;
                Label lblDH_DOC_DT = dr.FindControl("lblDH_DOC_DT") as Label;
                if (check_PDAProcess(btnPendingJObSelect.Text, lblDH_DOC_DT.Text))
                {
                    mpSendToPDA.Show();
                }
            }
        }

        #region Item add
        protected void btnAddSerials_Click(object sender, EventArgs e)
        {

        }

        protected void btnItemRemove_Click(object sender, EventArgs e)
        {

        }

        protected void btnItemGridSelect_Click(object sender, EventArgs e)
        {

        }

        protected void btnChangeItemStatus_Click(object sender, EventArgs e)
        {

        }

        protected void btnSerialRemove_Click(object sender, EventArgs e)
        {

        }

        protected void btnChangeSerialStatus_Click(object sender, EventArgs e)
        {

        }
        #endregion

        protected void lbtnGrdSerial_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdItems.Rows.Count == 0) return;

                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                if (row != null)
                {
                    string _itemCode = (row.FindControl("lblitri_itm_cd") as Label).Text;
                    string _itemStatus = (row.FindControl("lblitri_itm_stus") as Label).Text;
                    int _lineNo = Convert.ToInt32((row.FindControl("lblitri_line_no") as Label).Text);
                    if (string.IsNullOrEmpty(_itemCode)) return;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                    DataTable _result = CHNLSVC.Inventory.GetSupplierSerial(SearchParams, "Item", _itemCode);
                    grdAdSearch.DataSource = _result;
                    grdAdSearch.DataBind();

                    _result.Columns.RemoveAt(0);
                    _result.Columns.RemoveAt(2);
                    _result.Columns.RemoveAt(2);
                    _result.Columns.RemoveAt(2);
                    _result.Columns.RemoveAt(2);
                    _result.Columns.RemoveAt(2);
                    _result.Columns.RemoveAt(2);
                    _result.Columns.RemoveAt(2);
                    _result.Columns.RemoveAt(2);
                    _result.Columns.RemoveAt(2);
                    _result.Columns.RemoveAt(2);
                    _result.Columns.RemoveAt(1);
                    BindUCtrlDDLData3(_result);
                    Session["Ser"] = "Ser";
                    lblAvalue.Text = "Serial";
                    UserAdPopup.Show();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        protected void lbtngrdItemsDalete_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdItems.Rows.Count == 0) return;

                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                if (row != null)
                {
                    string _itemCode = (row.FindControl("lblitri_itm_cd") as Label).Text;
                    string _itemStatus = (row.FindControl("lblitri_itm_stus") as Label).Text;
                    int _lineNo = Convert.ToInt32((row.FindControl("lblitri_line_no") as Label).Text);
                    if (string.IsNullOrEmpty(_itemCode)) return;
                    OnRemoveFromItemGrid(_itemCode, _itemStatus, _lineNo);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        protected void lbtngrdSerialDalete_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdSerial.Rows.Count == 0) return;

                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;

                if (row != null)
                {
                    string _item = (row.FindControl("ser_Item") as Label).Text;
                    string _serialID = (row.FindControl("ser_SerialID") as Label).Text;
                    string _status = (row.FindControl("ser_Status") as Label).Text;
                    string serialI = (row.FindControl("ser_Serial1") as Label).Text;
                    string ItemLine = (row.FindControl("lbltus_itm_line") as Label).Text;
                    if (string.IsNullOrEmpty(_item)) return;
                    OnRemoveFromSerialGrid(_item, Convert.ToInt32(_serialID), _status, serialI, ItemLine);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        public void BindUCtrlDDLData3(DataTable _dataSource)
        {
            this.ddlSearchbykeyA.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykeyA.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykeyA.SelectedIndex = 0;
        }

        #region Advanced Search
        protected void btnAClose_Click(object sender, EventArgs e)
        {
            Session["Ser"] = null;
            txtSearchbywordA.Text = "";
            mpSearchAdance.Hide();
        }

        protected void txtSearchbywordA_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Adv")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                DataTable _result = CHNLSVC.Inventory.GetSupplierSerial(SearchParams, ddlSearchbykeyA.SelectedItem.Text, txtSearchbywordA.Text);
                grdAdSearch.DataSource = _result;
                grdAdSearch.DataBind();
                lblAvalue.Text = "Adv";
                Session["Adv"] = "true";
                UserAdPopup.Show();
                return;
            }
        }

        protected void lbtnSearchA_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "Adv")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                DataTable _result = CHNLSVC.Inventory.GetSupplierSerial(SearchParams, ddlSearchbykeyA.SelectedItem.Text, txtSearchbywordA.Text);
                grdAdSearch.DataSource = _result;
                grdAdSearch.DataBind();
                lblAvalue.Text = "Adv";
                UserAdPopup.Show();
                return;
            }
        }

        protected void btnAdvanceAddItem_Click(object sender, EventArgs e)
        {
            // Add Item ...
            foreach (GridViewRow dgvr in grdAdSearch.Rows)
            {
                CheckBox chk = (CheckBox)dgvr.FindControl("selectchk");
                if (chk.Checked)
                {
                    string _item = (dgvr.FindControl("Col_ItemCode") as Label).Text;
                    string _UnitCost = (dgvr.FindControl("Col_ins_unit_cost") as Label).Text;
                    string _status = (dgvr.FindControl("Col_ins_itm_stus") as Label).Text;
                    string _qty = (dgvr.FindControl("Col_inb_qty") as Label).Text;
                    string _serial = (dgvr.FindControl("Col_Serial1") as Label).Text;
                    txtOtherRef.Text = (dgvr.FindControl("Col_Supplier") as Label).Text;
                    txtOtherRef.Enabled = false;
                    bool _balnce = CheckItem(_item);
                    if (_balnce == true)
                    {
                        AddItem(_item, _UnitCost, _status, _qty, txtUserSeqNo.Text, _serial);
                    }

                }
            }
            // Add serial ...Save TEMP_PICK_SER
            foreach (GridViewRow dgvr in grdAdSearch.Rows)
            {
                CheckBox chk = (CheckBox)dgvr.FindControl("selectchk");
                if (chk.Checked)
                {
                    string _item = (dgvr.FindControl("Col_ItemCode") as Label).Text;
                    string _UnitCost = (dgvr.FindControl("Col_ins_unit_cost") as Label).Text;
                    string _status = (dgvr.FindControl("Col_ins_itm_stus") as Label).Text;
                    string _qty = (dgvr.FindControl("Col_inb_qty") as Label).Text;
                    string _serial = (dgvr.FindControl("Col_Serial1") as Label).Text;
                    bool _balnce = CheckItem(_item);
                    //if (_balnce == true)
                    //{
                    //    AddSerials(_item, _serial, txtUserSeqNo.Text);
                    //}
                }
            }
        }

        protected void grdAdSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
            //string Name = grdAdSearch.SelectedRow.Cells[1].Text;
        }

        protected void grdAdSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAdSearch.PageIndex = e.NewPageIndex;

            if (lblAvalue.Text == "Adv-ser")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                DataTable _result = CHNLSVC.Inventory.GetSupplierSerial(SearchParams, ddlSearchbykeyA.SelectedItem.Text, txtSearchbywordA.Text);
                grdAdSearch.DataSource = _result;
                grdAdSearch.DataBind();
                UserAdPopup.Show();
            }
            if (lblvalue.Text == "Adv")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                DataTable _result = CHNLSVC.Inventory.GetSupplierSerial(SearchParams, null, null);
                grdAdSearch.DataSource = _result;
                grdAdSearch.DataBind();
                UserAdPopup.Show();
                return;
            }
        }

        #endregion

        protected bool CheckItem(string _item)
        {
            try
            {
                DataTable _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, string.Empty);

                if (_inventoryLocation == null || _inventoryLocation.Rows.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No stock balance available');", true);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        protected void AddItem(string _item, string _UnitCost, string _status, string _qty, string _UserSeqNo, string _serial)
        {
            try
            {
                MasterItem _itms = new MasterItem();
                _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                InventoryRequestItem _itm = new InventoryRequestItem();
                ScanItemList = ViewState["ScanItemList"] as List<InventoryRequestItem>;
                if (ScanItemList != null)
                {
                    if (ScanItemList.Count > 0)
                    {
                        var _duplicate = from _ls in ScanItemList
                                         where _ls.Itri_itm_cd == _item && _ls.Itri_itm_stus == _status
                                         select _ls;

                        if (_duplicate != null)
                            if (_duplicate.Count() > 0)
                            {
                                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected item already available.');", true);
                                //return;
                            }
                            else
                            {
                                var _maxline = (from _ls in ScanItemList
                                                select _ls.Itri_line_no).Max();
                                _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                                _itm.Itri_itm_cd = _item;
                                _itm.Itri_itm_stus = _status;
                                _itm.Itri_line_no = Convert.ToInt32(_maxline) + 1;
                                _itm.Itri_qty = 0;
                                _itm.Mi_longdesc = _itms.Mi_longdesc;
                                _itm.Mi_model = _itms.Mi_model;
                                _itm.Mi_brand = _itms.Mi_brand;
                                //Added by Prabhath on 17/12/2013 ************* start **************
                                _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                                //Added by Prabhath on 17/12/2013 ************* end **************
                                ScanItemList.Add(_itm);
                            }

                    }
                    else
                    {
                        _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                        _itm.Itri_itm_cd = _item;
                        _itm.Itri_itm_stus = _status;
                        _itm.Itri_line_no = 1;
                        _itm.Itri_qty = 0;
                        _itm.Mi_longdesc = _itms.Mi_longdesc;
                        _itm.Mi_model = _itms.Mi_model;
                        _itm.Mi_brand = _itms.Mi_brand;
                        //Added by Prabhath on 17/12/2013 ************* start **************
                        _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                        //Added by Prabhath on 17/12/2013 ************* end **************
                        ScanItemList = new List<InventoryRequestItem>();
                        ScanItemList.Add(_itm);
                    }

                }
                else
                {
                    _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                    _itm.Itri_itm_cd = _item;
                    _itm.Itri_itm_stus = _status;
                    _itm.Itri_line_no = 1;
                    _itm.Itri_qty = 0;
                    _itm.Mi_longdesc = _itms.Mi_longdesc;
                    _itm.Mi_model = _itms.Mi_model;
                    _itm.Mi_brand = _itms.Mi_brand;
                    //Added by Prabhath on 17/12/2013 ************* start **************
                    _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                    //Added by Prabhath on 17/12/2013 ************* end **************
                    ScanItemList = new List<InventoryRequestItem>();
                    ScanItemList.Add(_itm);
                }



                if (string.IsNullOrEmpty(_UserSeqNo))
                {
                    GenerateNewUserSeqNo();
                }

                List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                foreach (InventoryRequestItem _addedItem in ScanItemList)
                {
                    ReptPickItems _reptitm = new ReptPickItems();
                    _reptitm.Tui_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text);
                    _reptitm.Tui_req_itm_qty = _addedItem.Itri_app_qty;
                    _reptitm.Tui_req_itm_cd = _addedItem.Itri_itm_cd;
                    _reptitm.Tui_req_itm_stus = _addedItem.Itri_itm_stus;
                    _reptitm.Tui_pic_itm_cd = Convert.ToString(_addedItem.Itri_line_no);
                    _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
                    _reptitm.Tui_pic_itm_qty = _addedItem.Itri_qty;
                    _saveonly.Add(_reptitm);
                }
                CHNLSVC.Inventory.SavePickedItems(_saveonly);

                grdItems.DataSource = null;
                grdItems.DataSource = ScanItemList;
                grdItems.DataBind();
                ViewState["ScanItemList"] = ScanItemList;

                grdSerial.DataSource = ScanItemList;
                grdSerial.DataBind();

                gridBtnSet();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);

                CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private Int32 GenerateNewUserSeqNo()
        {
            Int32 generated_seq = 0;
            Int16 _direction = 0;
           
            generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "DISP", _direction, Session["UserCompanyCode"].ToString());//direction always =1 for this method                    //assign user_seqno

            ucOutScan.userSeqNo = generated_seq.ToString();
            ucOutScan.ScanDocument = txtJobNumber.Text.Trim();
            ucOutScan.doc_tp = "DISP";

            ReptPickHeader RPH = new ReptPickHeader();
            RPH.Tuh_doc_tp = "DISP";
            RPH.Tuh_cre_dt = DateTime.Today;
            RPH.Tuh_ischek_itmstus = true;
            RPH.Tuh_ischek_reqqty = true;
            RPH.Tuh_ischek_simitm = true;
            RPH.Tuh_session_id = Session["SessionID"].ToString();
            RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
            RPH.Tuh_usr_id = Session["UserID"].ToString();
            RPH.Tuh_usrseq_no = generated_seq;
            if (_direction == 1)
            {
                RPH.Tuh_direct = true;
            }
            else
            {
                RPH.Tuh_direct = false;
            }
            RPH.Tuh_doc_no = txtJobNumber.Text.Trim();
            int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
            if (affected_rows == 1)
            {
                txtUserSeqNo.Text = generated_seq.ToString();
                lblJobNumber.ToolTip = txtUserSeqNo.Text;
                return generated_seq;
            }
            else
            {
                return 0;
            }
        }

        protected void OnRemoveFromItemGrid(string _itemCode, string _itemStatus, int _lineNo)
        {
            try
            {
                bool status = CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32(txtUserSeqNo.Text), _itemCode, _itemStatus, _lineNo, 1);

                string err = string.Empty;
                ReptPickSerials _scanserNew = new ReptPickSerials();
                _scanserNew.Tus_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text);
                _scanserNew.Tus_itm_cd = _itemCode;
                _scanserNew.Tus_itm_stus = _itemStatus;
                _scanserNew.Tus_itm_line = Convert.ToInt32("-1");
                Int32 esrt = CHNLSVC.Inventory.DeletePickSerByItemAndItemLine(_scanserNew, out err);

                //List<ReptPickSerials> _list = new List<ReptPickSerials>();
                //_list = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), "DISP");
                //if (_list != null)
                //    if (_list.Count > 0)
                //    {
                //        var _delete = (from _lst in _list
                //                       where _lst.Tus_itm_cd == _itemCode && _lst.Tus_itm_stus == _itemStatus && _lst.Tus_base_itm_line == _lineNo
                //                       select _lst).ToList();

                //        foreach (ReptPickSerials _ser in _delete)
                //        {
                //            Delete_Serials(_itemCode, _itemStatus, _ser.Tus_ser_id, _ser.Tus_ser_1);
                //        }
                //        //}
                //    }

                //ScanItemList.RemoveAll(x => x.Itri_itm_cd == _item && x.Itri_itm_stus == _itmStatus);
                LoadItems(txtUserSeqNo.Text);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void Delete_Serials(string _itemCode, string _itemStatus, Int32 _serialID, string serialI)
        {
            MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itemCode);
            if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
            {
                bool res1 = CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), Convert.ToInt32(_serialID), _itemCode, serialI);
                bool res2 = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itemCode, _serialID, 1);
            }
            else
            {
                CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), _itemCode, _itemStatus);
            }
        }

        private void LoadItems(string _seqNo)
        {
            try
            {
                int _direction = 0;

                _direction = 0;

                Int32 user_seq_num = CHNLSVC.Inventory.Get_Scan_SeqNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "DISP", Session["UserID"].ToString(), _direction, _seqNo);
                //if (user_seq_num == -1)
                //{
                //    user_seq_num = GenerateNewUserSeqNo();
                //}
                if (txtUserSeqNo.Text == "")
                {
                    if (user_seq_num == -1)
                    {
                        user_seq_num = GenerateNewUserSeqNo();
                    }
                }
                else
                {
                    user_seq_num = Convert.ToInt32(txtUserSeqNo.Text);
                }

                List<ReptPickSerials> serList = new List<ReptPickSerials>();
                serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "DISP");

                List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                List<ReptPickItems> _reptItems = new List<ReptPickItems>();
                _reptItems = CHNLSVC.Inventory.GetAllScanRequestItemsList(user_seq_num);
                foreach (ReptPickItems _reptitem in _reptItems)
                {
                    //var count = count(from ser in serList
                    //            where ser.Tus_itm_cd == _reptitem.Tui_req_itm_cd &&  ser.Tus_itm_stus == _reptitem.Tui_req_itm_stus                                
                    //            select new
                    //            {
                    //                ser
                    //            });
                    decimal count = 0;
                    if (serList == null)
                        count = 0;
                    else
                        count = serList.Where(x => x.Tus_itm_cd == _reptitem.Tui_req_itm_cd && x.Tus_itm_stus == _reptitem.Tui_req_itm_stus).Sum(z => z.Tus_qty);

                    InventoryRequestItem _itm = new InventoryRequestItem();
                    MasterItem _itms = new MasterItem();
                    _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _reptitem.Tui_req_itm_cd);
                    _itm.Itri_app_qty = _reptitem.Tui_req_itm_qty;
                    _itm.Itri_itm_cd = _reptitem.Tui_req_itm_cd;
                    _itm.Itri_itm_stus = _reptitem.Tui_req_itm_stus;
                    _itm.Itri_line_no = Convert.ToInt32(_reptitem.Tui_pic_itm_cd.ToString());
                    _itm.Itri_qty = 0;
                    _itm.Itri_app_qty = Convert.ToInt32(count);
                    _itm.Mi_longdesc = _itms.Mi_longdesc;
                    _itm.Mi_model = _itms.Mi_model;
                    _itm.Mi_brand = _itms.Mi_brand;
                    _itm.Itri_unit_price = Convert.ToDecimal(_reptitem.Tui_pic_itm_stus.ToString());
                    _itm.Itri_supplier = _reptitem.Tui_sup;
                    _itm.Itri_batchno = _reptitem.Tui_batch;
                    _itm.Itri_grnno = _reptitem.Tui_grn;
                    _itm.Itri_grndate = _reptitem.Tui_grn_dt;
                    _itm.Itri_expdate = _reptitem.Tui_exp_dt;
                    _itmList.Add(_itm);
                }
                ScanItemList = _itmList.OrderBy(o => o.Itri_line_no).ToList(); ;


                //ViewState["ScanItemList"] = ScanItemList;


                List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                _serList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "DISP");
                if (_serList != null)
                {
                    //if (_direction == 0)
                    //{
                    //    //var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                    //    var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                    //    foreach (var itm in _scanItems)
                    //    {
                    //        foreach (GridViewRow row in grdItems.Rows)
                    //        {
                    //            if (itm.Peo.Tus_itm_cd == ((Label)row.FindControl("lblitri_itm_cd")).Text.ToString() && itm.Peo.Tus_base_itm_line == Convert.ToInt32(((Label)row.FindControl("lblitri_line_no")).Text.ToString()))
                    //            {
                    //                ((Label)row.FindControl("lblitri_app_qty")).Text = Convert.ToDecimal(itm.theCount).ToString(); // Current scan qty
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                    //    foreach (var itm in _scanItems)
                    //    {
                    //        foreach (GridViewRow row in grdItems.Rows)
                    //        {
                    //            if (itm.Peo.Tus_itm_cd == ((Label)row.FindControl("lblitri_itm_cd")).Text.ToString() && itm.Peo.Tus_itm_line == Convert.ToInt32(((Label)row.FindControl("lblitri_line_no")).Text.ToString()))
                    //            {
                    //                ((Label)row.FindControl("lblitri_app_qty")).Text = Convert.ToDecimal(itm.theCount).ToString(); // Current scan qty
                    //            }
                    //        }
                    //    }
                    //}
                    foreach (ReptPickSerials item in _serList)
                    {
                        item.Tus_itm_stus_Desc = getItemStatusDesc(item.Tus_itm_stus);
                    }
                    grdSerial.AutoGenerateColumns = false;
                    grdSerial.DataSource = _serList;
                    grdSerial.DataBind();
                    ViewState["SerialList"] = _serList;
                    ucOutScan.PickSerial = _serList;

                    //var _grnPickSerials = from p in GRNPickSerials
                    //  where p.Tus_itm_stus == "GDLP"
                    //  group p by new { p.Tus_itm_cd, p.Tus_unit_cost, p.Tus_base_doc_no, p.Tus_base_itm_line } into list
                    //  select new { itemcode = list.Key.Tus_itm_cd, unitprice = list.Key.Tus_unit_cost, pono = list.Key.Tus_base_doc_no, polineno = list.Key.Tus_base_itm_line, poqty = list.Sum(p => p.Tus_qty) };

                    //var test = from p in _serList
                    //           group p by new
                    //           {
                    //               p.Tus_usrseq_no,
                    //               p.Tus_doc_no,
                    //               p.Tus_seq_no,
                    //               p.Tus_itm_cd,
                    //               p.Tus_itm_stus,
                    //               p.Tus_ser_id,
                    //               p.Tus_ser_1,
                    //               p.Tus_ser_2,
                    //               p.Tus_ser_3,
                    //               p.Tus_itm_model,
                    //               p.Tus_itm_stus_Desc,
                    //               p.Tus_unit_cost,
                    //               p.Tus_bin,
                    //               p.Tus_base_doc_no
                    //           } into list
                    //           select new
                    //           {
                    //               Tus_usrseq_no = list.Key.Tus_usrseq_no,
                    //               Tus_doc_no = list.Key.Tus_doc_no,
                    //               Tus_seq_no = list.Key.Tus_seq_no,
                    //               Tus_itm_cd = list.Key.Tus_itm_cd,
                    //               Tus_itm_stus = list.Key.Tus_itm_stus,
                    //               Tus_ser_id = list.Key.Tus_ser_id,
                    //               Tus_ser_1 = list.Key.Tus_ser_1,
                    //               Tus_ser_2 = list.Key.Tus_ser_2,
                    //               Tus_ser_3 = list.Key.Tus_ser_3,
                    //               Tus_itm_model = list.Key.Tus_itm_model,
                    //               Tus_itm_stus_desc = list.Key.Tus_itm_stus_Desc,
                    //               Tus_unit_cost = list.Key.Tus_unit_cost,
                    //               Tus_bin = list.Key.Tus_bin,
                    //               Tus_base_doc_no = list.Key.Tus_base_doc_no,
                    //               Tus_base_itm_line = 0,
                    //               Tus_qty = list.Sum(p => p.Tus_qty)
                    //           };

                    //grdSerial.DataSource = test;
                    //grdSerial.DataBind();

                    ucOutScan.ScanItemList = ScanItemList;
                    ucOutScan.PickSerial = _serList;
                }
                else
                {
                    List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                    grdSerial.AutoGenerateColumns = false;
                    grdSerial.DataSource = emptyGridList;
                    grdSerial.DataBind();
                    ViewState["SerialList"] = emptyGridList;
                    ucOutScan.PickSerial = emptyGridList;
                }
                //gvItems.AutoGenerateColumns = false;
                //gvItems.DataSource = gvItems;

                List<InventoryRequestItem> ScanItemListTemp = new List<InventoryRequestItem>();

                if (ScanItemList != null && ScanItemList.Count > 0)
                {
                    foreach (InventoryRequestItem item in ScanItemList)
                    {
                        item.Itri_qty = ScanItemList.Where(t => t.Itri_itm_cd == item.Itri_itm_cd && t.Itri_itm_stus == item.Itri_itm_stus).Sum(x => x.Itri_app_qty);

                        item.Itri_itm_stus_desc = getItemStatusDesc(item.Itri_itm_stus);
                    }
                }

                ScanItemList.RemoveAll(x => x.Itri_app_qty == 0 && x.Itri_qty == 0);

                ucOutScan.ScanItemList = ScanItemList;
                grdItems.AutoGenerateColumns = false;
                grdItems.DataSource = ScanItemList;
                grdItems.DataBind();
                gridBtnSet();

                //Coment by Chamal 30-Aug-2016
                //if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10119) && grdItems.Rows.Count > 0)
                //{
                //    btnSave.Enabled = false;
                //    btnSave.OnClientClick = "";
                //    btnSave.CssClass = "buttoncolor";

                //    btnUpdate.Enabled = false;
                //    btnUpdate.OnClientClick = "";
                //    btnUpdate.CssClass = "buttoncolor";
                //}

                if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16034) && ddlPaymentType.SelectedValue == "POD" && !string.IsNullOrEmpty(txtJobNumber.Text) && grdItems.Rows.Count > 0)
                {
                    ddlPaymentType.Enabled = false;
                }
                else
                {
                    ddlPaymentType.Enabled = true;
                }

                if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16034) && !string.IsNullOrEmpty(txtJobNumber.Text) && !string.IsNullOrEmpty(txtReceipt.Text))
                {
                    ddlPaymentType.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        protected void OnRemoveFromSerialGrid(string _item, int _serialID, string _status, string serialI, string itmLine)
        {
            try
            {
                MasterItem _masterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                if (_masterItem.Mi_is_ser1 == 1 || _masterItem.Mi_is_ser1 == 0)
                {
                    if (_masterItem.Mi_is_ser1 == 1)
                    {
                        bool result = CHNLSVC.Inventory.Del_temp_pick_ser(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), Convert.ToInt32(_serialID), _item, serialI);
                        bool result2 = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, _serialID, 1);
                    }
                    else
                    {
                        string err = string.Empty;
                        ReptPickSerials _scanserNew = new ReptPickSerials();
                        _scanserNew.Tus_usrseq_no = Convert.ToInt32(txtUserSeqNo.Text);
                        _scanserNew.Tus_itm_cd = _item;
                        _scanserNew.Tus_itm_stus = _status;
                        _scanserNew.Tus_itm_line = Convert.ToInt32(itmLine);
                        Int32 esrt = CHNLSVC.Inventory.DeletePickSerByItemAndItemLine(_scanserNew, out err);
                    }

                    bool resr = CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32(txtUserSeqNo.Text), _item, _status, 0, 1);
                }
                else
                {
                    CHNLSVC.Inventory.DeleteTempPickSerialByItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Convert.ToInt32(txtUserSeqNo.Text), _item, _status);
                    string err = string.Empty;
                    //Boolean resr = CHNLSVC.Inventory.DeleteTempItem(Convert.ToInt32(txtUserSeqNo.Text), _item, _status, 0, 1, out err);
                    bool resr = CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32(txtUserSeqNo.Text), _item, _status, 0, 1);
                }
                LoadItems(txtUserSeqNo.Text);

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void Process(bool IsTemp)
        {
            try
            {
                TextBox txtSubType = new TextBox(); txtSubType.Text = "NOR";
                TextBox txtRef = new TextBox(); txtRef.Text = txtJobNumber.Text;
                TextBox txtRemarks = new TextBox(); txtRemarks.Text = txtRemark.Text;

                if (CheckServerDateTime() == false) return;

                if (string.IsNullOrEmpty(txtSubType.Text))
                {
                    DisplayMessage("Select the adjustment sub type");
                    return;
                }

                if (string.IsNullOrEmpty(txtRef.Text)) txtRef.Text = "N/A";
                if (string.IsNullOrEmpty(txtOtherRef.Text)) txtOtherRef.Text = string.Empty;
                if (string.IsNullOrEmpty(txtRemarks.Text)) txtRemarks.Text = string.Empty;

                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty.ToUpper().ToString(), Session["GlbModuleName"].ToString(), txtDate, lblH1, Convert.ToDateTime(txtDate.Text).ToShortDateString(), out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (Convert.ToDateTime(txtDate.Text).Date != DateTime.Now.Date)
                        {
                            txtDate.Enabled = true;
                            DisplayMessage("Selected date not allowed for transaction");
                            txtDate.Focus();
                            return;
                        }
                    }
                    else
                    {
                        txtDate.Enabled = true;
                        DisplayMessage("Selected date not allowed for transaction");
                        txtDate.Focus();
                        return;
                    }
                }

                List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                List<ReptPickSerialsSub> reptPickSubSerialsList = new List<ReptPickSerialsSub>();
                string documntNo = "";
                Int32 result = -99;
                Int32 _userSeqNo = 0;
                int _direction = 0;
                _direction = 0;

                _userSeqNo = CHNLSVC.Inventory.Get_Scan_SeqNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "ADJ", Session["UserID"].ToString(), _direction, txtUserSeqNo.Text);

                _userSeqNo = Convert.ToInt32(txtUserSeqNo.Text);

                reptPickSerialsList = CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), _userSeqNo, "ADJ");

                reptPickSubSerialsList = CHNLSVC.Inventory.GetAllScanSubSerialsList(_userSeqNo, "ADJ");

                #region Check Reference Date and the Doc Date

                if (_direction == 0)
                {
                    if (IsReferancedDocDateAppropriate(reptPickSerialsList, Convert.ToDateTime(txtDate.Text).Date) == false)
                    {
                        return;
                    }
                }

                #endregion

                #region Check Duplicate Serials

                if (reptPickSerialsList != null)
                {
                    var _dup = reptPickSerialsList.Where(x => x.Tus_ser_1 != "N/A").Select(y => y.Tus_ser_id).ToList();

                    string _duplicateItems = string.Empty;
                    bool _isDuplicate = false;
                    if (_dup != null)
                        if (_dup.Count > 0)
                            foreach (Int32 _id in _dup)
                            {
                                Int32 _counts = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(y => y.Tus_ser_id).Count();
                                if (_counts > 1)
                                {
                                    _isDuplicate = true;
                                    var _item = reptPickSerialsList.Where(x => x.Tus_ser_id == _id).Select(x => x.Tus_itm_cd).Distinct();
                                    foreach (string _str in _item)
                                        if (string.IsNullOrEmpty(_duplicateItems))
                                            _duplicateItems = _str;
                                        else
                                            _duplicateItems += "," + _str;
                                }
                            }
                    if (_isDuplicate)
                    {
                        DisplayMessage("Following item serials are duplicating. Please remove the duplicated serials. " + _duplicateItems);
                        return;
                    }
                }
                #endregion
                InventoryHeader inHeader = new InventoryHeader();
                #region Fill InventoryHeader
                DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                foreach (DataRow r in dt_location.Rows)
                {
                    // Get the value of the wanted column and cast it to string
                    inHeader.Ith_sbu = (string)r["ML_OPE_CD"];
                    if (System.DBNull.Value != r["ML_CATE_2"])
                    {
                        inHeader.Ith_channel = (string)r["ML_CATE_2"];
                    }
                    else
                    {
                        inHeader.Ith_channel = string.Empty;
                    }
                }
                inHeader.Ith_acc_no = "STOCK_ADJ";
                inHeader.Ith_anal_1 = "";

                inHeader.Ith_anal_3 = "";
                inHeader.Ith_anal_4 = "";
                inHeader.Ith_anal_5 = "";
                inHeader.Ith_anal_6 = 0;
                inHeader.Ith_anal_7 = 0;
                inHeader.Ith_anal_8 = DateTime.MinValue;
                inHeader.Ith_anal_9 = DateTime.MinValue;
                if (Session["DocType"].ToString() == "TempDoc")
                {
                    inHeader.Ith_anal_10 = true;
                    inHeader.Ith_anal_2 = txtJobNumber.Text;
                }
                else
                {
                    inHeader.Ith_anal_10 = false;
                    inHeader.Ith_anal_2 = "";
                }

                inHeader.Ith_anal_11 = false;
                inHeader.Ith_anal_12 = false;
                inHeader.Ith_bus_entity = "";
                inHeader.Ith_cate_tp = txtSubType.Text.ToString().Trim();
                inHeader.Ith_com = Session["UserCompanyCode"].ToString();
                inHeader.Ith_com_docno = "";
                inHeader.Ith_cre_by = Session["UserID"].ToString();
                inHeader.Ith_cre_when = DateTime.Now;
                inHeader.Ith_del_add1 = "";
                inHeader.Ith_del_add2 = "";
                inHeader.Ith_del_code = "";
                inHeader.Ith_del_party = "";
                inHeader.Ith_del_town = "";

                inHeader.Ith_direct = false;
                inHeader.Ith_doc_tp = "ADJ";

                inHeader.Ith_doc_date = Convert.ToDateTime(txtDate.Text).Date;
                inHeader.Ith_doc_no = string.Empty;

                inHeader.Ith_doc_year = Convert.ToDateTime(txtDate.Text).Year;
                if (IsTemp == true)
                {
                    inHeader.Ith_entry_no = txtUserSeqNo.Text;
                }
                else
                {
                    inHeader.Ith_entry_no = "";
                }
                inHeader.Ith_entry_tp = txtSubType.Text.ToString().Trim();
                inHeader.Ith_git_close = true;
                inHeader.Ith_git_close_date = DateTime.MinValue;
                inHeader.Ith_git_close_doc = string.Empty;
                inHeader.Ith_isprinted = false;
                inHeader.Ith_is_manual = false;
                inHeader.Ith_job_no = string.Empty;
                inHeader.Ith_loading_point = string.Empty;
                inHeader.Ith_loading_user = string.Empty;
                inHeader.Ith_loc = Session["UserDefLoca"].ToString();
                inHeader.Ith_manual_ref = txtRef.Text.Trim();
                inHeader.Ith_mod_by = Session["UserID"].ToString();
                inHeader.Ith_mod_when = DateTime.Now;
                inHeader.Ith_noofcopies = 0;
                inHeader.Ith_oth_loc = string.Empty;
                inHeader.Ith_oth_docno = txtOtherRef.Text.Trim();
                inHeader.Ith_remarks = txtRemarks.Text;

                //inHeader.Ith_seq_no = 6; removed by Chamal 12-05-2013
                inHeader.Ith_session_id = Session["SessionID"].ToString();
                inHeader.Ith_stus = "A";
                inHeader.Ith_sub_tp = txtSubType.Text.ToString().Trim();
                inHeader.Ith_vehi_no = string.Empty;
                inHeader.Ith_anal_3 = "";//ddlDeliver.SelectedItem.Text;

                #endregion
                MasterAutoNumber masterAuto = new MasterAutoNumber();

                #region Fill MasterAutoNumber

                masterAuto.Aut_cate_cd = Session["UserDefLoca"].ToString();
                masterAuto.Aut_cate_tp = "LOC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "ADJ";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "ADJ";
                masterAuto.Aut_year = null;

                #endregion

                int _line = 0;
                #region Update some serial items
                if (reptPickSerialsList != null)
                {
                    if (_direction == 1)
                    {
                        foreach (var _seritem in reptPickSerialsList)
                        {
                            _seritem.Tus_exist_grncom = Session["UserCompanyCode"].ToString();
                            _seritem.Tus_exist_grndt = Convert.ToDateTime(txtDate.Text).Date;
                            _seritem.Tus_orig_grncom = Session["UserCompanyCode"].ToString();
                            _seritem.Tus_orig_grndt = Convert.ToDateTime(txtDate.Text).Date;
                        }
                    }
                    else if (_direction == 0)
                    {
                        foreach (var _ADJSer in reptPickSerialsList)
                        {
                            _line = _line + 1;
                            _ADJSer.Tus_base_itm_line = _line;
                        }
                    }
                }
                #endregion

                result = CHNLSVC.Inventory.ADJMinus(inHeader, reptPickSerialsList, reptPickSubSerialsList, masterAuto, out documntNo, IsTemp);

                if (result != -99 && result >= 0)
                {
                    string _msg = "Successfully Saved! Document No : " + documntNo;
                    DisplayMessage(_msg, 3);
                    clear();

                }
                else
                {
                    string _msg = documntNo + " Process Terminated : ADJ";
                    DisplayMessage(_msg, 4);
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(err.Message, 4, err);
            }
        }

        protected void btnClosePDA_Click(object sender, EventArgs e)
        {

        }

        protected void ddlloadingbay_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnsend_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtdocname.Text.Trim()))
                {
                    DisplayMessage("Please enter the document no.");
                    txtdocname.Focus();
                    mpSendToPDA.Show();
                    return;
                }

                if (ddlloadingbay.SelectedIndex == 0)
                {
                    DisplayMessage("Please select the loading bay.");
                    ddlloadingbay.Focus();
                    mpSendToPDA.Show();
                    return;
                }

                Int32 _userSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "DISP", 0, Session["UserCompanyCode"].ToString());
                string _userid = (string)Session["UserID"];
                Int32 val = 0;

                string warehousecom = (string)Session["WAREHOUSE_COM"];
                string warehouseloc = (string)Session["WAREHOUSE_LOC"];

                if (warehousecom == "" || warehouseloc == "") {
                    DisplayMessage("Please setup warehouse company and location.");
                    ddlloadingbay.Focus();
                    mpSendToPDA.Show();
                    return;
                }
                DataTable dtdoccheck = CHNLSVC.Inventory.IsDocNoAvailable(txtdocname.Text.Trim(), "DISP", 0, Session["UserCompanyCode"].ToString());

                if (dtdoccheck.Rows.Count > 0)
                {
                    foreach (DataRow ddr in dtdoccheck.Rows)
                    {
                        string seqNo = ddr["tuh_usrseq_no"].ToString();
                        _userSeqNo = Convert.ToInt32(seqNo);
                    }
                }

                if (dtdoccheck.Rows.Count == 0)
                {
                    ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                    _inputReptPickHeader.Tuh_usrseq_no = Convert.ToInt32(_userSeqNo);
                    _inputReptPickHeader.Tuh_usr_id = _userid;
                    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    _inputReptPickHeader.Tuh_session_id = Session["SessionID"].ToString();
                    _inputReptPickHeader.Tuh_doc_tp = "DISP";
                    //_inputReptPickHeader.Tuh_direct = false;
                    _inputReptPickHeader.Tuh_direct = false;//Lakshan as per the chamal...
                    _inputReptPickHeader.Tuh_isdirect = true;//Nuwan
                    _inputReptPickHeader.Tuh_ischek_itmstus = false;
                    _inputReptPickHeader.Tuh_ischek_simitm = false;
                    _inputReptPickHeader.Tuh_ischek_reqqty = false;
                    _inputReptPickHeader.Tuh_doc_no = txtdocname.Text.Trim();
                    _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                    _inputReptPickHeader.Tuh_wh_com = warehousecom;
                    _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                    _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;

                    val = CHNLSVC.Inventory.SavePickedHeader(_inputReptPickHeader);

                    if (Convert.ToInt32(val) == -1)
                    {
                        DisplayMessage("Error Occurred while processing");
                        return;
                    }
                }
                else
                {
                    ReptPickHeader _inputReptPickHeader = new ReptPickHeader();

                    _inputReptPickHeader.Tuh_doc_no = txtdocname.Text.Trim();
                    _inputReptPickHeader.Tuh_doc_tp = "DISP";
                    //_inputReptPickHeader.Tuh_direct = false;
                    _inputReptPickHeader.Tuh_direct = true;//Lakshan as per the chamal...
                    _inputReptPickHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    _inputReptPickHeader.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                    _inputReptPickHeader.Tuh_wh_com = warehousecom;
                    _inputReptPickHeader.Tuh_wh_loc = warehouseloc;
                    _inputReptPickHeader.Tuh_load_bay = ddlloadingbay.SelectedValue;

                    val = CHNLSVC.Inventory.UpdatePickHeader(_inputReptPickHeader);

                    if (Convert.ToInt32(val) == -1)
                    {
                        DisplayMessage("Error Occurred while processing.");
                        CHNLSVC.CloseChannel();
                        return;
                    }
                }

                if (Convert.ToInt32(val) == -1)
                {
                    DisplayMessage("Error Occurred while processing");
                    return;
                }
                else
                {
                    DisplayMessage("Successfully sent.");
                    mpSendToPDA.Hide();
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                mpSendToPDA.Show();
            }
        }

        private void PopulateLoadingBays()
        {
            try
            {
                DataTable dtbays = CHNLSVC.Inventory.LoadLoadingBays(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "LB");

                if (dtbays.Rows.Count > 0)
                {
                    ddlloadingbay.DataSource = dtbays;
                    ddlloadingbay.DataTextField = "mws_res_name";
                    ddlloadingbay.DataValueField = "mws_res_cd";
                    ddlloadingbay.DataBind();
                }

                ddlloadingbay.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private bool check_PDAProcess(string JobNumber, string doc_date)
        {
            bool status = true;
            string _supplier = string.Empty;
            string _subdoc = string.Empty;
            string location = string.Empty;

            string OutwardNo = JobNumber;
            string outwrddate = doc_date;

            string warehousecom = (string)Session["WAREHOUSE_COM"];
            string warehouseloc = (string)Session["WAREHOUSE_LOC"];

            DataTable dtdoccheck = CHNLSVC.Inventory.IsDocNoAvailable(OutwardNo, "DISP", 1, Session["UserCompanyCode"].ToString());

            txtdocname.Text = JobNumber;

            if (dtdoccheck.Rows.Count > 1)
            {
                DisplayMessage("Selected document is already send to PDA");
                return status = false; ;
            }

            return status;
        }

        private void clearItems()
        {
            for (int i = 0; i < grdItems.Rows.Count; i++)
            {
                var row = (GridViewRow)grdItems.Rows[i];
                if (row != null)
                {
                    string _itemCode = (row.FindControl("lblitri_itm_cd") as Label).Text;
                    string _itemStatus = (row.FindControl("lblitri_itm_stus") as Label).Text;
                    int _lineNo = Convert.ToInt32((row.FindControl("lblitri_line_no") as Label).Text);
                    if (string.IsNullOrEmpty(_itemCode)) return;
                    OnRemoveFromItemGrid(_itemCode, _itemStatus, _lineNo);
                }
            }
        }

        protected void btnAddLocationnew_Click(object sender, EventArgs e)
        {
            btnAddLocation_Click(null, null);
        }

        public void gridBtnSet()
        {
            for (int i = 0; i < grdItems.Rows.Count; i++)
            {
                GridViewRow dr = grdItems.Rows[i];
                LinkButton lbtnGrdSerial = dr.FindControl("lbtnGrdSerial") as LinkButton;
                Label lblitri_itm_cd = dr.FindControl("lblitri_itm_cd") as Label;

                MasterItem _itms = new MasterItem();
                _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), lblitri_itm_cd.Text.Trim());
                if (_itms.Mi_is_ser1 == 0)
                {
                    lbtnGrdSerial.Visible = false;
                }
            }
        }

        protected void btnSetGrid_Click(object sender, EventArgs e)
        {
            gridBtnSet();
        }

        private void loadItemStatus()
        {
            List<MasterItemStatus> oMasterItemStatuss = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
            Session["ItemStatus"] = oMasterItemStatuss;
        }

        private string getItemStatusDesc(string stis)
        {
            List<MasterItemStatus> oStatuss = (List<MasterItemStatus>)Session["ItemStatus"];
            if (oStatuss.FindAll(x => x.Mis_cd == stis).Count > 0)
            {
                stis = oStatuss.Find(x => x.Mis_cd == stis).Mis_desc;
            }
            return stis;
        }

        private bool checkMaxValus(List<ReptPickSerials> reptPickSerialsList)
        {
            bool status = false;
            decimal TotalValue = 0;
            decimal MaxValue = 0;

            MaxValue = Convert.ToDecimal(txtMaxValue.Text);

            if (reptPickSerialsList.Count > 0)
            {
                TotalValue = reptPickSerialsList.Sum(x => x.Tus_qty * x.Tus_unit_cost);
            }
            if (TotalValue > MaxValue)
            {
                status = false;
            }
            else
            {
                status = true;
            }
            return status;
        }

        private void LoadAdjestmentDetails()
        {
            DataTable OItems = new DataTable();
            DataTable oSerials = CHNLSVC.Inventory.GET_DISPOSAL_SERIALS(txtJobNumber.Text.Trim(), out   OItems);
            if (oSerials != null && oSerials.Rows.Count > 0)
            {
                List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                List<ReptPickItems> _reptItems = new List<ReptPickItems>();
                for (int i = 0; i < OItems.Rows.Count; i++)
                {
                    InventoryRequestItem _itm = new InventoryRequestItem();
                    MasterItem _itms = new MasterItem();
                    _itms = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), OItems.Rows[i]["itb_itm_cd"].ToString());
                    _itm.Itri_app_qty = Convert.ToDecimal(OItems.Rows[i]["itb_qty"].ToString());
                    _itm.Itri_itm_cd = OItems.Rows[i]["itb_itm_cd"].ToString();
                    _itm.Itri_itm_stus = OItems.Rows[i]["itb_itm_stus"].ToString();
                    _itm.Itri_line_no = Convert.ToInt32(OItems.Rows[i]["itb_base_itmline"].ToString());
                    _itm.Itri_qty = 0;
                    _itm.Itri_app_qty = Convert.ToInt32(OItems.Rows[i]["itb_qty"].ToString());
                    _itm.Mi_longdesc = _itms.Mi_longdesc;
                    _itm.Mi_model = _itms.Mi_model;
                    _itm.Mi_brand = _itms.Mi_brand;
                    _itm.Itri_unit_price = Convert.ToDecimal(OItems.Rows[i]["itb_unit_cost"].ToString());
                    _itmList.Add(_itm);
                }
                ScanItemList = _itmList.OrderBy(o => o.Itri_line_no).ToList(); ;
                ucOutScan.ScanItemList = ScanItemList;
                if (ScanItemList != null && ScanItemList.Count > 0)
                {
                    foreach (InventoryRequestItem item in ScanItemList)
                    {
                        item.Itri_qty = ScanItemList.Where(t => t.Itri_itm_cd == item.Itri_itm_cd && t.Itri_itm_stus == item.Itri_itm_stus).Sum(x => x.Itri_app_qty);

                        item.Itri_itm_stus_desc = getItemStatusDesc(item.Itri_itm_stus);
                    }
                }

                ucOutScan.ScanItemList = ScanItemList;
                grdItems.AutoGenerateColumns = false;
                grdItems.DataSource = ScanItemList;
                grdItems.DataBind();
                gridBtnSet();

                List<ReptPickSerials> serList = new List<ReptPickSerials>();
                foreach (DataRow _row in oSerials.Rows)
                {
                    MasterItem _item = new MasterItem();
                    _item = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), (String)_row["ITS_ITM_CD"]);
                    ReptPickSerials _one = new ReptPickSerials();
                    _one.Tus_batch_line = Convert.ToInt16(_row["ITS_BATCH_LINE"]);
                    _one.Tus_cre_dt = System.DateTime.Now;
                    _one.Tus_cross_batchline = Convert.ToInt16(_row["ITS_BATCH_LINE"]);
                    _one.Tus_cross_itemline = Convert.ToInt16(_row["ITS_ITM_LINE"]);
                    _one.Tus_cross_seqno = Convert.ToInt32(_row["ITS_SEQ_NO"]);
                    _one.Tus_cross_serline = Convert.ToInt16(_row["ITS_SER_LINE"]);
                    _one.Tus_doc_dt = Convert.ToDateTime(_row["ITS_DOC_DT"]);
                    _one.Tus_doc_no = Convert.ToString(_row["ITS_DOC_NO"]);
                    _one.Tus_exist_grncom = _row["ITS_EXIST_GRNCOM"] == DBNull.Value ? string.Empty : (String)_row["ITS_EXIST_GRNCOM"];
                    _one.Tus_exist_grnno = _row["ITS_EXIST_GRNNO"] == DBNull.Value ? string.Empty : (String)_row["ITS_EXIST_GRNNO"];
                    _one.Tus_exist_grndt = _row["ITS_EXIST_GRNDT"] == DBNull.Value ? DateTime.MinValue : (DateTime)_row["ITS_EXIST_GRNDT"];
                    _one.Tus_exist_supp = _row["ITS_EXIST_SUPP"] == DBNull.Value ? string.Empty : (String)_row["ITS_EXIST_SUPP"];
                    _one.Tus_itm_stus = (String)_row["ITS_ITM_STUS"];
                    _one.Tus_itm_cd = (String)_row["ITS_ITM_CD"];
                    _one.Tus_itm_desc = _item.Mi_longdesc;
                    _one.Tus_itm_line = Convert.ToInt16(_row["ITS_ITM_LINE"]);
                    _one.Tus_itm_model = _item.Mi_model;
                    _one.Tus_new_remarks = String.Empty;
                    _one.Tus_new_status = String.Empty;
                    _one.Tus_qty = 1;
                    _one.Tus_seq_no = 0;
                    _one.Tus_ser_1 = _row["ITS_SER_1"] == DBNull.Value ? string.Empty : (String)_row["ITS_SER_1"];
                    _one.Tus_ser_2 = _row["ITS_SER_2"] == DBNull.Value ? string.Empty : (String)_row["ITS_SER_2"];
                    _one.Tus_ser_3 = _row["ITS_SER_3"] == DBNull.Value ? string.Empty : (String)_row["ITS_SER_3"];
                    _one.Tus_ser_4 = _row["ITS_SER_4"] == DBNull.Value ? string.Empty : (String)_row["ITS_SER_4"];
                    _one.Tus_ser_id = Convert.ToInt32(_row["ITS_SER_ID"]);
                    _one.Tus_ser_line = Convert.ToInt16(_row["ITS_SER_LINE"]);
                    _one.Tus_serial_id = "1";
                    _one.Tus_unit_cost = Convert.ToDecimal(_row["ITS_UNIT_COST"]);
                    _one.Tus_usrseq_no = -1;
                    _one.Tus_warr_no = _row["ITS_WARR_NO"] == DBNull.Value ? string.Empty : (String)_row["ITS_WARR_NO"];
                    _one.Tus_warr_period = Convert.ToInt16(_row["ITS_WARR_PERIOD"]);
                    serList.Add(_one);
                }

                foreach (ReptPickSerials item in serList)
                {
                    item.Tus_itm_stus_Desc = getItemStatusDesc(item.Tus_itm_stus);
                }
                grdSerial.AutoGenerateColumns = false;
                grdSerial.DataSource = serList;
                grdSerial.DataBind();
                ViewState["SerialList"] = serList;
                ucOutScan.PickSerial = serList;
                ucOutScan.ScanItemList = ScanItemList;
                ucOutScan.PickSerial = serList;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtJobNumber.Text))
            {
                DisplayMessage("Select a job number");
                txtJobNumber.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtMaxValue.Text.Trim()))
            {
                DisplayMessage("Enter maximum value");
                txtMaxValue.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtCharge.Text.Trim()))
            {
                DisplayMessage("Enter charge value");
                txtCharge.Focus();
                return;
            }
            if (ddlPaymentType.SelectedIndex == 0)
            {
                DisplayMessage("Select payment type");
                txtCharge.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtReference.Text))
            {
                DisplayMessage("Please add the reference number");
                txtReference.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtRemark.Text))
            {
                DisplayMessage("Please add the remark");
                txtReference.Focus();
                return;
            }

            if (oDisposalLocations == null || oDisposalLocations.Count == 0)
            {
                if (dgvLocations.Rows.Count > 0)
                {
                    oDisposalLocations = CHNLSVC.Inventory.GET_DISPOSAL_LOCATIONS(oDisposalHeader.Dh_seq);
                    if (oDisposalLocations == null || oDisposalLocations.Count == 0)
                    {
                        DisplayMessage("Add locations");
                        txtLocation.Focus();

                        dgvLocations.DataSource = new int[] { };
                        dgvLocations.DataBind();
                        return;
                    }
                }
                else
                {
                    DisplayMessage("Add locations");
                    txtLocation.Focus();
                    return;
                }
            }
            if (txtRemark.Text.Length > 150)
            {
                DisplayMessage("Remark length is " + txtRemark.Text.Length.ToString() + ". Please reduce.");
                txtLocation.Focus();
                return;
            }

            oDisposalHeader.Dh_doc_no = txtJobNumber.Text.Trim();

            if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16034))
            {
                if (ddlPaymentType.SelectedValue.ToString() == "PRD" && string.IsNullOrEmpty(txtReceipt.Text.Trim()) && string.IsNullOrEmpty(txtJobNumber.Text))
                {
                    DisplayMessage("Please select the post delivery type.");
                    //DisplayMessage("Please complete the receipt details");
                    return;
                }
            }

            if (ddlPaymentType.SelectedValue.ToString() == "PRD" && string.IsNullOrEmpty(txtReceipt.Text.Trim()) && !string.IsNullOrEmpty(txtJobNumber.Text) && grdItems.Rows.Count > 0)
            {
                DisplayMessage("Please complete the receipt details");
                return;
            }

            oDisposalHeader.Dh_com = Session["UserCompanyCode"].ToString();
            oDisposalHeader.Dh_pc = Session["UserDefProf"].ToString();
            oDisposalHeader.Dh_doc_dt = Convert.ToDateTime(txtDate.Text.Trim());
            oDisposalHeader.Dh_frm_dt = Convert.ToDateTime(txtValidFrom.Text.Trim());
            oDisposalHeader.Dh_to_dt = Convert.ToDateTime(txtValidTo.Text.Trim());
            oDisposalHeader.Dh_ref_no = txtReference.Text.Trim();
            oDisposalHeader.Dh_max_val = Convert.ToDecimal(txtMaxValue.Text.Trim());
            oDisposalHeader.Dh_chk_max_val = (chkRestrict.Checked) ? 1 : 0;
            oDisposalHeader.Dh_chg = Convert.ToDecimal(txtCharge.Text.Trim());
            oDisposalHeader.Dh_pay_mtd = ddlPaymentType.SelectedValue.ToString();
            oDisposalHeader.Dh_recipt_no = txtReceipt.Text.Trim();
            oDisposalHeader.Dh_rmk = txtRemark.Text.Trim();
            oDisposalHeader.Dh_stus = "P";
            oDisposalHeader.Dh_anal_1 = string.Empty;
            oDisposalHeader.Dh_anal_2 = string.Empty;
            oDisposalHeader.Dh_anal_3 = string.Empty;
            oDisposalHeader.Dh_anal_4 = string.Empty;
            oDisposalHeader.Dh_anal_5 = string.Empty;
            oDisposalHeader.Dh_cre_by = Session["UserID"].ToString();
            oDisposalHeader.Dh_cre_dt = DateTime.Now;
            oDisposalHeader.Dh_cre_session = Session["SessionID"].ToString();
            oDisposalHeader.Dh_mod_by = null;
            oDisposalHeader.Dh_mod_dt = null;
            oDisposalHeader.Dh_mod_session = null;

            MasterAutoNumber mastAutoNo = new MasterAutoNumber();
            mastAutoNo.Aut_cate_cd = Session["UserCompanyCode"].ToString();
            mastAutoNo.Aut_cate_tp = "COM";
            mastAutoNo.Aut_direction = 0;
            mastAutoNo.Aut_modify_dt = null;
            mastAutoNo.Aut_moduleid = "DISP";
            mastAutoNo.Aut_start_char = "DISP";
            mastAutoNo.Aut_year = Convert.ToInt32(DateTime.Now.Year);

            string err = string.Empty;
            string docNo = string.Empty;

            string Doc_DISP = docNo;

            if (txtStatus.Text == "Pending")
            {
                oDisposalHeader.Dh_stus = "P";
            }
            if (!string.IsNullOrEmpty(txtReceipt.Text))
            {
                oDisposalHeader.Dh_stus = "S";
            }

            Int32 result = CHNLSVC.Inventory.Save_Disposal_Job(oDisposalHeader, oDisposalLocations, mastAutoNo, out err, out docNo);
            if (result > 0)
            {
                string msg = string.Empty;

                if (string.IsNullOrEmpty(txtJobNumber.Text))
                {
                    msg = "Saved. Disposal Number : " + Doc_DISP + ".";
                }
                else
                {
                    msg = "Updated the disposal number : " + Doc_DISP + ".";
                }

                DisplayMessage(msg, 3);

                clear();
                return;
            }
            else
            {
                DisplayMessage(err, 4);
            }
        }

        private void sendApprovedMails(string DisposalNumber)
        {
            try
            {
                string err = string.Empty;
                string BccAdd = string.Empty;
                string SMSBody = string.Empty;

                List<REF_ALERT_SETUP> oItems = CHNLSVC.General.GET_REF_ALERT_SETUP(Session["UserCompanyCode"].ToString(), "SBU", Session["UserSBU"].ToString(), "m_Trans_Inventory_DisposalEntry", "P");
                if (oItems != null && oItems.Count > 0)
                {
                    string msgBody = PopulateBody("~/Common/template.html");
                    foreach (REF_ALERT_SETUP item in oItems)
                    {
                        if (item.Rals_via_email == 1)
                        {
                            SystemUser oSecUser = CHNLSVC.Security.GetUserByUserID(item.Rals_to_user);
                            SystemUser oAppUser = CHNLSVC.Security.GetUserByUserID(Session["UserID"].ToString());
                            if (oSecUser != null && !string.IsNullOrEmpty(oSecUser.Se_usr_id) && !string.IsNullOrEmpty(oSecUser.se_Email))
                            {
                                string content = "Disposal job created. Job number : " + DisposalNumber;
                                msgBody = msgBody.Replace("[Receiver]", oSecUser.Se_usr_name);
                                msgBody = msgBody.Replace("[content]", content);
                                msgBody = msgBody.Replace("[Header]", "Disposal Job Creation");

                                Int32 result = CHNLSVC.MsgPortal.SendEMail_HTML(oSecUser.se_Email, "Approval Note", msgBody, "", out err, BccAdd);
                            }
                            if (oSecUser != null && !string.IsNullOrEmpty(oSecUser.Se_usr_id) && !string.IsNullOrEmpty(oSecUser.se_Mob))
                            {
                                string content = "Disposal job created. Job number : " + DisposalNumber;
                                OutSMS _out = new OutSMS();
                                _out.Msg = content;
                                _out.Msgstatus = 0;
                                _out.Msgtype = "S";
                                _out.Receivedtime = DateTime.Now;
                                _out.Receiver = "";
                                _out.Receiverphno = oSecUser.se_Mob;
                                _out.Senderphno = oSecUser.se_Mob;
                                _out.Refdocno = "0";
                                _out.Sender = "Message Agent";
                                _out.Createtime = DateTime.Now;
                                int resultSms = CHNLSVC.General.SaveSMSOut(_out);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private string PopulateBody(string path)
        {
            string body = File.ReadAllText(Server.MapPath(path));
            return body;
        }
    }
}