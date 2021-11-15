using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Data;
using System.Text;

namespace FF.WebERPClient.Inventory_Module
{
    public partial class ManualDocReceived : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadRefDetails(0);
                txtRecDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                BindEmptyData();
            }
        }

        #region UI events

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            btnConfirm.Enabled = false;
            SaveManualDocument();
            btnConfirm.Enabled = true;
        }
        protected void ddlRef_SelectedIndexChanged(object sender, EventArgs e)
        {
            //save to temporary table
            string _DocStatus = string.Empty;
            if (chkTrans.Checked == true)
            {
                _DocStatus = "F";
            }
            else
            {
                _DocStatus = "P";
            }
            Int16 _userSeqNo = CHNLSVC.Inventory.SavePickedManualDocDetail(ddlRef.SelectedValue, GlbUserDefLoca, GlbUserName, _DocStatus);

            LoadManualDocDetails();
        }
        protected void imgbtnTLoc_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
            DataTable dataSource = CHNLSVC.CommonSearch.GetLocationSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtTransLoc.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();

        }
        protected void btnTrans_Click(object sender, EventArgs e)
        {
            btnTrans.Enabled = false;
            TransferManualDocument();
            btnTrans.Enabled = true;
        }
        protected void gvManualDocDet_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "DELETEITEM":
                    {
                        ImageButton imgbtndelAllSerial = (ImageButton)e.CommandSource;
                        string _selectedItemDetails = imgbtndelAllSerial.CommandArgument;
                        DeleteSelectedItem(_selectedItemDetails);

                        LoadManualDocDetails();

                        break;
                    }
            }
        }
        protected void chkTrans_CheckedChanged(object sender, EventArgs e)
        {
            LoadRefDetails(Convert.ToInt16(chkTrans.Checked));
        }

        #endregion

        #region user functions
        private void BindEmptyData()
        {
            DataTable DT = null;
            gvManualDocDet.DataSource = DT;
            gvManualDocDet.DataBind();
            //pnlManualDocDet.Update();
        }

        private void LoadRefDetails(Int16 IsTrans)
        {
            ddlRef.Items.Clear();
            ddlRef.Items.Add(new ListItem("--Select Ref No--", "-1"));
            DataTable _tbl = CHNLSVC.Inventory.GetManualDocs(GlbUserDefLoca, IsTrans);
            ddlRef.DataSource = _tbl;
            ddlRef.DataTextField = "MDD_REF";
            ddlRef.DataValueField = "MDD_REF";
            ddlRef.DataBind();

            //gvManualDocDet.DataSource = null;
            //gvManualDocDet.DataBind();
            //pnlManualDocDet.Update();

        }

        private void DeleteSelectedItem(string _selectedItemDetails)
        {
            string[] arr = _selectedItemDetails.Split(new string[] { "|" }, StringSplitOptions.None);
            Int32 _Bkno = Convert.ToInt32(arr[0]);
            string _user = arr[1];
            string _prefix = arr[2];

            CHNLSVC.Inventory.Delete_Selected_Item_Line(_Bkno,_prefix, _user);

        }

        private void SaveManualDocument()
        {
            try
            {
                if (ddlRef.SelectedValue.Equals("-1"))
                {
                    throw new UIValidationException("Please select reference no");
                }
                if (chkTrans.Checked == true)
                {
                    throw new UIValidationException("Cannot confirm. This is already confirmed");
                }
                string _AdjNumber = string.Empty;
                Int32 rows_inserted = 0;
                Int32 _userSeqNo = 0;

                //InventoryHeader _invHeader = new InventoryHeader();
                //DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(GlbUserComCode, GlbUserDefLoca);
                //foreach (DataRow r in dt_location.Rows)
                //{
                //    // Get the value of the wanted column and cast it to string
                //    _invHeader.Ith_sbu = (string)r["ML_OPE_CD"];
                //    if (System.DBNull.Value != r["ML_CATE_2"])
                //    {
                //        _invHeader.Ith_channel = (string)r["ML_CATE_2"];
                //    }
                //    else
                //    {
                //        _invHeader.Ith_channel = string.Empty;
                //    }
                //}

                //_invHeader.Ith_com = GlbUserComCode;
                //_invHeader.Ith_loc = GlbUserDefLoca;
                //DateTime _docDate = Convert.ToDateTime(txtRecDate.Text);
                //_invHeader.Ith_doc_date = _docDate;
                //_invHeader.Ith_doc_year = _docDate.Year;
                //_invHeader.Ith_direct = true;
                //_invHeader.Ith_doc_tp = "ADJ";
                //_invHeader.Ith_cate_tp = "NOR";
                //_invHeader.Ith_sub_tp = "NOR";
                //_invHeader.Ith_bus_entity = "";
                //_invHeader.Ith_is_manual = false;
                //_invHeader.Ith_manual_ref = "";
                ////_invHeader.Ith_remarks = txtRemarks.Text;
                //_invHeader.Ith_stus = "A";
                //_invHeader.Ith_cre_by = GlbUserName;
                //_invHeader.Ith_cre_when = DateTime.Now;
                //_invHeader.Ith_mod_by = GlbUserName;
                //_invHeader.Ith_mod_when = DateTime.Now;
                //_invHeader.Ith_session_id = GlbUserSessionID;
                //_invHeader.Ith_oth_docno = "";

                //MasterAutoNumber _masterAuto = new MasterAutoNumber();
                //_masterAuto.Aut_cate_cd = GlbUserDefLoca;
                //_masterAuto.Aut_cate_tp = "LOC";
                //_masterAuto.Aut_direction = null;
                //_masterAuto.Aut_modify_dt = null;
                //_masterAuto.Aut_moduleid = "ADJ";
                //_masterAuto.Aut_number = 0;
                //_masterAuto.Aut_start_char = "ADJ";
                //_masterAuto.Aut_year = null;

                _userSeqNo = CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "ADJ", 1, GlbUserComCode);

                String _defBin = CHNLSVC.Inventory.GetDefaultBinCode(GlbUserComCode, GlbUserDefLoca);

                //GET manual document serial list
                int Z = CHNLSVC.Inventory.GetManualDocSerialList(ddlRef.SelectedValue, GlbUserName, _userSeqNo, _defBin, GlbUserComCode, GlbUserDefLoca);

                //rows_inserted = CHNLSVC.Inventory.SaveManualDocReceipt(_invHeader, _reptPickSerialList, null, _masterAuto, out _AdjNumber);

                //Int16 rws = CHNLSVC.Inventory.SaveManualDocPages(_reptPickSerialList, ddlRef.SelectedValue);
                int rws = CHNLSVC.Inventory.SaveManualDocPages(_userSeqNo, ddlRef.SelectedValue);

                CHNLSVC.Inventory.UpdateManualDocs(ddlRef.SelectedValue, GlbUserName);

                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Manual Document Receive Receipt " + _AdjNumber + " Sucessfully saved.");
                ClearData();

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
        private void TransferManualDocument()
        {
            try
            {
                if (chkTrans.Checked == false)
                {
                    throw new UIValidationException("Cannot transfer. This is not confirmed yet");
                }
                if (chkTrans.Checked == false)
                {
                    throw new UIValidationException("Please select transfer location");
                }
                if (txtTransLoc.Text == "")
                {
                    throw new UIValidationException("Please select transfer location");
                }

                String _defBin = CHNLSVC.Inventory.GetDefaultBinCode(GlbUserComCode, GlbUserDefLoca);
                string _AdjNumber = string.Empty;

                //put adj OUT
                List<ReptPickSerials> _list = CHNLSVC.Inventory.Get_all_Serial_details(ddlRef.SelectedValue, GlbUserName, GlbUserComCode, GlbUserDefLoca);

                InventoryHeader _invHeader = new InventoryHeader();
                _invHeader.Ith_com = GlbUserComCode;
                _invHeader.Ith_loc = GlbUserDefLoca;
                DateTime _docDate = Convert.ToDateTime(txtRecDate.Text);
                _invHeader.Ith_doc_date = _docDate;
                _invHeader.Ith_doc_year = _docDate.Year;
                _invHeader.Ith_direct = false;
                _invHeader.Ith_doc_tp = "ADJ";
                _invHeader.Ith_cate_tp = "NOR";
                _invHeader.Ith_sub_tp = "NOR";
                _invHeader.Ith_bus_entity = "";
                _invHeader.Ith_is_manual = false;
                _invHeader.Ith_manual_ref = "";
                //_invHeader.Ith_remarks = txtRemarks.Text;
                _invHeader.Ith_stus = "A";
                _invHeader.Ith_cre_by = GlbUserName;
                _invHeader.Ith_cre_when = DateTime.Now;
                _invHeader.Ith_mod_by = GlbUserName;
                _invHeader.Ith_mod_when = DateTime.Now;
                _invHeader.Ith_session_id = GlbUserSessionID;
                _invHeader.Ith_oth_docno = "";

                MasterAutoNumber _masterAuto = new MasterAutoNumber();
                _masterAuto.Aut_cate_cd = GlbUserDefLoca;
                _masterAuto.Aut_cate_tp = "LOC";
                _masterAuto.Aut_direction = null;
                _masterAuto.Aut_modify_dt = null;
                _masterAuto.Aut_moduleid = "ADJ";
                _masterAuto.Aut_number = 0;
                _masterAuto.Aut_start_char = "ADJ";
                _masterAuto.Aut_year = null;


                string _message = string.Empty;
                string _genInventoryDoc = string.Empty;
                string _genSalesDoc = string.Empty;
                int rows_inserted = 0;

                //Process adj minus
                rows_inserted = CHNLSVC.Inventory.Manual_Doc_Transfer(_invHeader, _list, null, _masterAuto, out _AdjNumber);

                CHNLSVC.Inventory.UpdateTransferStatus(ddlRef.SelectedValue, GlbUserName, txtTransLoc.Text);

                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully transfered " + ddlRef.SelectedValue);
                ClearData();

            }
            catch (UIValidationException ex)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.ErrorMessege);
            }
            catch (Exception e1)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, e1.Message);
            }
        }
        private void LoadManualDocDetails()
        {
            gvManualDocDet.DataSource = CHNLSVC.Inventory.GetManualDocDet(GlbUserName);
            gvManualDocDet.DataBind();
            pnlManualDocDet.Update();
        }
        private void ClearData()
        {
            LoadRefDetails(0);
            chkTrans.Checked = false;
            txtTransLoc.Text = "";
            pnlFooter.Update();
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


                default:
                    break;
            }

            return paramsText.ToString();
        }
    }
}