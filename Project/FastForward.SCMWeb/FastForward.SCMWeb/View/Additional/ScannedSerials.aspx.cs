using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Additional
{
    public partial class ScannedSerials : Base
    {

        protected List<InventorySerialN> InventorySerialN { get { return (List<InventorySerialN>)Session["InventorySerialN"]; } set { Session["InventorySerialN"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Clear();
                loadserial();
            }
        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "Yes")
            {
                try
                {
                    Response.Redirect(Request.RawUrl, false);
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                }
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
            }
           
        }

        private void Clear()
        {
            InventorySerialN = new List<InventorySerialN>();
        }
        private void loadserial()
        {
            InventorySerialN = CHNLSVC.Inventory.Get_Reserved_SerialsNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
            List<InventorySerialN> _seril = new List<InventorySerialN>();
            int numberOfrecords=10; // read from user
            _seril = InventorySerialN;
            var firstFiveItems = _seril.Take(50);
            grdpickserial.DataSource = firstFiveItems.ToList();
            grdpickserial.DataBind();
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
                
                default:
                    break;
            }

            return paramsText.ToString();
        }

        #region Modalpopup
        protected void btnClose_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string ID = grdResult.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "Item")
            {
                txtscanItem.Text = ID;
                txtscanItem_TextChanged(null, null);
                return;
            }
           
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;
           
          
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {

            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {

            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        private void FilterData()
        {
            if (lblvalue.Text == "Item")
            {
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(para, "Code", txtSearchbyword.Text);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
            }
 
            
        }
        #endregion

        private void DisplayMessage(String _err, Int32 option)
        {
            string Msg = _err.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + Msg + "');", true);
            }
        }

        #region Scanned serial Tab

        #region Rooting for Load Item Detail

        private bool LoadItemDetail(string _item, TextBox _txt)
        {
            bool _isValid = false;
            try
            {

                MasterItem _itemdetail = new MasterItem();

                if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserComputer"].ToString(), _item);
                if (_itemdetail == null || string.IsNullOrEmpty(_itemdetail.Mi_cd))
                {
                    DisplayMessage("Please check the item code", 2);

                    _txt.Text = string.Empty;
                    _txt.Focus();
                    _isValid = false;
                    return _isValid;
                }
                if (_itemdetail != null)
                    if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        _isValid = true;
                        string _description = _itemdetail.Mi_longdesc;
                        string _model = _itemdetail.Mi_model;
                        string _brand = _itemdetail.Mi_brand;
                        string _serialstatus = _itemdetail.Mi_is_ser1 == 1 ? "Available" : "None";

                        lblItemDescription.Text = _description;
                        lblItemModel.Text =   _model;
                        lblItemBrand.Text = _brand;
                        //lblItemSubStatus.Text = "Serial Status : " + _serialstatus;

                        //Decimal VAT_RATE = CHNLSVC.Sales.GET_Item_vat_Rate(BaseCls.GlbUserComCode, _itemdetail.Mi_cd, "VAT");
                        //lblVatRate.Text = "Imported VAT Rt. : " + VAT_RATE.ToString() + "%";
                    }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 2);
                return _isValid;
            }

            return _isValid;
        }

        #endregion Rooting for Load Item Detail


        protected void lbtnScanItem_Click(object sender, EventArgs e)
        {
            try
            {
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(para, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                UserPopoup.Show();
                lblvalue.Text = "Item";
                
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...\n" + ex.Message, 2);

            }

        }

        protected void txtscanItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtscanItem.Text))
                {
                    LoadItemDetail(txtscanItem.Text.Trim(), txtscanItem);
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 2);

            }

        }

        #endregion

        protected void lbtnDelete_TextChanged(object sender, EventArgs e)
        {
            if(txtDeleteconformmessageValue.Value=="No")
            {
                return;
            }
            int _scanSeq = 0;
            int _serID = 0;
            string _docNo = string.Empty;
            string _docType = string.Empty;
            string _scanDoc = string.Empty;
            int _ref = 0;
            for (int i = 0; i < grdpickserial.Rows.Count; i++)
            {
                CheckBox _select = (CheckBox)grdpickserial.Rows[i].FindControl("chk_Req");
                Label _docNolbl = (Label)grdpickserial.Rows[i].FindControl("col_InwardDoc");
                Label _serIDlbl = (Label)grdpickserial.Rows[i].FindControl("col_SerialID");
                if (_select.Checked)
                {
                    
                    _docNo = _docNolbl.Text;
                    _serID = Convert.ToInt32(_serIDlbl.Text);


                    DataTable _infor = CHNLSVC.Inventory.GetScanDocInfor(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _docNo, _serID);
                    if (_infor != null && _infor.Rows.Count > 0)
                    {
                        _docType = _infor.Rows[0].Field<string>("TUH_DOC_TP");
                        _scanDoc = _infor.Rows[0].Field<string>("TUH_DOC_NO");
                        lblpickdocno.Text = _scanDoc + " | " + _docType;
                        lblpickuser.Text = _infor.Rows[0].Field<string>("TUS_CRE_BY");
                        lblpickdatetime.Text = _infor.Rows[0].Field<DateTime>("TUS_CRE_DT").ToString();
                        _scanSeq = Convert.ToInt32(_infor.Rows[0].Field<Int64>("TUH_USRSEQ_NO"));
                    }
                    else
                    {
                        _scanSeq = -1;
                        lblpickdocno.Text = string.Empty;
                        lblpickuser.Text = string.Empty;
                        lblpickdatetime.Text = string.Empty;
                    }


                    #region Deleting Row

                    bool _isDelete = true;
                    if (_scanSeq == -1)
                    {
                        _isDelete = true;
                    }

                    if (_docType == "COM_OUT")
                    {
                        _isDelete = true;
                    }

                    if (_isDelete == true)
                    {
                        if (CHNLSVC.Inventory.Check_Valid_Document(Session["UserCompanyCode"].ToString(), _scanDoc, "ADV_REC"))
                        { _isDelete = false; }
                        else
                        { _isDelete = true; }
                    }

                    if (_isDelete == true)
                    {
                        if (CHNLSVC.Inventory.Check_Valid_Document(Session["UserCompanyCode"].ToString(), _scanDoc, "QUO"))
                        { _isDelete = false; }
                        else
                        { _isDelete = true; }
                    }

                    if (_isDelete == true)
                    {
                        //Add by Chamal 26-05-2014 (Vehicle Reg. Receipt)
                        if (CHNLSVC.Inventory.Check_Valid_Document(Session["UserCompanyCode"].ToString(), _scanDoc, "RECEIPT"))
                        { _isDelete = false; }
                        else
                        { _isDelete = true; }
                    }

                   
                        if (_isDelete == false)
                        {
                            DisplayMessage("Can not removed this serial!", 2);
                            
                            return;
                        }
                       
                       
                            string _err = string.Empty;
                            _ref = CHNLSVC.Inventory.RemoveReservedSerials(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _scanSeq, _docNo, _serID, Session["UserID"].ToString(), out _err);
                            if (_ref == -1)
                            {
                                DisplayMessage(_err, 4);
                                return;
                            }
                        
                        
                    

                    #endregion Deleting Row
                }
            }

        
            if (_ref == 1)
            {
                loadserial();
                DisplayMessage("Done!", 3);
                lblpickdocno.Text = string.Empty;
                lblpickuser.Text = string.Empty;
                lblpickdatetime.Text = string.Empty;
            }
        }
        protected void chk_Req_Click(object sender, EventArgs e)
        {
            
            if (grdpickserial.Rows.Count == 0) return;

            var lb = (CheckBox)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                int _scanSeq = 0;
                int _serID = 0;
                string _docNo = string.Empty;
                string _docType = string.Empty;
                string _scanDoc = string.Empty;
                string _docNolbl = (row.FindControl("col_InwardDoc") as Label).Text;
                string _serIDlbl = (row.FindControl("col_SerialID") as Label).Text;
                _docNo = _docNolbl;
                 _serID = Convert.ToInt32(_serIDlbl);


                DataTable _infor = CHNLSVC.Inventory.GetScanDocInfor(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _docNo, _serID);
                if (_infor != null && _infor.Rows.Count > 0)
                {
                    _docType = _infor.Rows[0].Field<string>("TUH_DOC_TP");
                    _scanDoc = _infor.Rows[0].Field<string>("TUH_DOC_NO");
                    lblpickdocno.Text = _scanDoc + " | " + _docType;
                    lblpickuser.Text = _infor.Rows[0].Field<string>("TUS_CRE_BY");
                    lblpickdatetime.Text = _infor.Rows[0].Field<DateTime>("TUS_CRE_DT").ToString();
                    _scanSeq = Convert.ToInt32(_infor.Rows[0].Field<Int64>("TUH_USRSEQ_NO"));
                }
                else
                {
                    _scanSeq = -1;
                    lblpickdocno.Text = string.Empty;
                    lblpickuser.Text = string.Empty;
                    lblpickdatetime.Text = string.Empty;
                }

            }
        }
        protected void lbtnview_TextChanged(object sender, EventArgs e)
        {
            List<InventorySerialN> _seril = new List<InventorySerialN>();
            if (InventorySerialN.Count > 0)
            {
                if ((!string.IsNullOrEmpty(txtscanItem.Text)) &&(!string.IsNullOrEmpty(txtserial.Text)))
                {
                    _seril = InventorySerialN.Where(x => x.Ins_ser_1 == txtserial.Text.Trim() && x.Ins_itm_cd == txtscanItem.Text.Trim().ToUpper()).ToList();
                }
                else if (!string.IsNullOrEmpty(txtscanItem.Text))
                {
                    _seril = InventorySerialN.Where(x => x.Ins_itm_cd == txtscanItem.Text.Trim().ToUpper()).ToList();

                }
                else if (!string.IsNullOrEmpty(txtserial.Text))
                {
                    _seril = InventorySerialN.Where(x => x.Ins_ser_1 == txtserial.Text.Trim().ToUpper()).ToList();
                }
               
                grdpickserial.DataSource = _seril;
                grdpickserial.DataBind();
            }
        }
    }
}