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

namespace FastForward.SCMWeb.View.MasterFiles.Inventory
{
    public partial class ItemAllocationSetup : Base
    {
        List<MasterItemAllocate> _MasterItemAllocate =new List<MasterItemAllocate>();
        private List<MasterItemStatus> _stsList
        {
            get { if (Session["_stsList"] != null) { return (List<MasterItemStatus>)Session["_stsList"]; } else { return new List<MasterItemStatus>(); } }
            set { Session["_stsList"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserDefLoca"] == null  || Session["UserID"] == null || Session["UserCompanyCode"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                _stsList = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                ViewState["Temp_AlloItemDetails"] = new DataTable();
                ClearPage();
                ClearMsg();
            }
            else
            {
                if (Session["PuDocNo"].ToString() == "true")
                {
                    UserPopoup.Show();
                    UserPopoup2.Hide();
                    Session["PuDocNo"] = "";
                }
                else if (Session["GRNDocNo"].ToString() == "true")
                {
                    UserPopoup.Show();
          
                    Session["GRNDocNo"] = "";
                }
                else if (Session["Item"].ToString() == "true")
                {
                    UserPopoup2.Show();
                
                    Session["Item"] = "";
                }
                else if (Session["OrderNo"].ToString() == "true")
                {
                    UserPopoup.Show();
                    Session["OrderNo"] = "";
                }
                txtFDate.Text = Request[txtFDate.UniqueID];
                txtTDate.Text = Request[txtTDate.UniqueID];
            }
        }
        private void ClearPage()
        {
            _MasterItemAllocate = null;
            grdDocItem.DataSource = new int[] { };
            grdDocItem.DataBind();
            grdAlItem.DataSource = new int[] { };
            grdAlItem.DataBind();
           // ddlDocType.SelectedIndex = 0;
            txtlocation.Text = string.Empty;
            txtDocNo.Text = string.Empty;
            txtItemSearch.Text = string.Empty;
            optItem.Checked = false;
            optModel.Checked = false;
            txtCompany.Text = Session["UserCompanyCode"].ToString();
            txtChannel.Text = string.Empty;
            txtItem.Text = string.Empty;
            txtQty.Text = string.Empty;
            Session["PuDocNo"] = "";
            Session["GRNDocNo"] = "";
            Session["Item"] = "";
            Session["OrderNo"] = "";
            Session["ISqtyLow"] = "";
            ViewState["SaveItemAllocate"] = null;
            ViewState["Temp_ItemDetails"] = null;
            ViewState["Temp_AlloItemDetails"] = null;
            ViewState["DelItem"] = null;
        }

        private void ErrorMsg(string _Msg)
        {
            ClearMsg();
            WarningItem.Visible = true;
            lblWItem.Text = _Msg;
        }
        private void SuccessMsg(string _Msg)
        {
            ClearMsg();
            SuccessItem.Visible = true;
            lblSItem.Text = _Msg;
        }
        private void ClearMsg()
        {
            WarningItem.Visible = false;
            SuccessItem.Visible = false;
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {

            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PurchaseOrder:
                    {
                        paramsText.Append(ddlPType.SelectedValue + seperator + Session["UserCompanyCode"].ToString() );
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GRNItem:
                    {
                        paramsText.Append(ddlDocType.SelectedItem.Text + seperator + Session["UserCompanyCode"].ToString() + seperator + txtlocation.Text.Trim());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ImportModel:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ImportItem:
                    {
                        paramsText.Append(null + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + (string.IsNullOrEmpty(txtlocation.Text)?null:txtlocation.Text.Trim().ToUpper())+seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Channel:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
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
        public void BindUCtrlDDLData2(DataTable _dataSource)
        {
            this.ddlSearchbykey2.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey2.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey2.SelectedIndex = 0;
        }
     
        #region Modalpopup
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string ID = grdResult.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "PuDocNo")
            {
                string Date = grdResult.SelectedRow.Cells[3].Text;
                txtDocNo.Text = ID;
                lblvalue.Text = "";
                Session["DocDate"] = Date;
                UserPopoup.Hide();
                return;
            }
           
            if (lblvalue.Text == "GRNDocNo")
            {
                string Date = grdResult.SelectedRow.Cells[2].Text;
                txtDocNo.Text = ID;
                txtDocNo_TextChanged(null, null);
                lblvalue.Text = "";
                Session["DocDate"] = Date;
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "OrderNo")
            {
                txtItemSearch.Text = ID;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "DocNo")
            {
                txtDocNo.Text = ID;
                txtDocNo_TextChanged(null, null);
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "PuDocNo")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PurchaseOrder);
                DataTable _result = CHNLSVC.Inventory.GetPurDocNo(SearchParams, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text), null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "PuDocNo";
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "GRNDocNo")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GRNItem);
                DataTable _result = CHNLSVC.Inventory.GetGRNDocNo(SearchParams, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text), null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "GRNDocNo";
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "OrderNo")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GRNItem);
                DataTable _result = CHNLSVC.Inventory.GetGRNDocNo(SearchParams, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text), null, null);
                _result.Columns["Order #"].SetOrdinal(0);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "OrderNo";
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "OrderNo")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                DataTable _result = CHNLSVC.Inventory.GetGRNDocNo(SearchParams, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text), null, null);
                _result.Columns["Order #"].SetOrdinal(0);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "OrderNo";
                UserPopoup.Show();
                return;
            }
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "PuDocNo")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PurchaseOrder);
                DataTable _result = CHNLSVC.Inventory.GetPurDocNo(SearchParams, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text), ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "PuDocNo";
                UserPopoup.Show();
            }
            if (lblvalue.Text == "GRNDocNo")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GRNItem);
                DataTable _result = CHNLSVC.Inventory.GetGRNDocNo(SearchParams, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text), ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "GRNDocNo";              
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "OrderNo")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GRNItem);
                DataTable _result = CHNLSVC.Inventory.GetGRNDocNo(SearchParams, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text), ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                _result.Columns["Order #"].SetOrdinal(0);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "OrderNo";                
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "DocNo")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                DataTable _result = CHNLSVC.CommonSearch.SearchAodDocumentForItemAllocation(SearchParams, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text), ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "DocNo";
                UserPopoup.Show();
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "PuDocNo")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PurchaseOrder);
                DataTable _result = CHNLSVC.Inventory.GetPurDocNo(SearchParams, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text),ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "PuDocNo";
                Session["PuDocNo"] = "true";
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "GRNDocNo")
            {
                DateTime d1 = DateTime.Now;
                d1 = d1.AddMonths(-1);
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GRNItem);
                DataTable _result = CHNLSVC.Inventory.GetGRNDocNo(SearchParams, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text), ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "GRNDocNo";
                Session["GRNDocNo"] = "true";
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "OrderNo")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GRNItem);
                DataTable _result = CHNLSVC.Inventory.GetGRNDocNo(SearchParams, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text), ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                _result.Columns["Order #"].SetOrdinal(0);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "OrderNo";
                Session["OrderNo"] = "true";
                UserPopoup.Show();
                return;
            }
        }
       
        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "PuDocNo")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PurchaseOrder);
                DataTable _result = CHNLSVC.Inventory.GetPurDocNo(SearchParams, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text), null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "PuDocNo";
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "GRNDocNo")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GRNItem);
                DataTable _result = CHNLSVC.Inventory.GetGRNDocNo(SearchParams, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text), null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "GRNDocNo";
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "OrderNo")
            {
                if (txtDocNo.Text == "")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GRNItem);
                    DataTable _result = CHNLSVC.Inventory.GetGRNDocNo(SearchParams, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text), null, null);
                    _result.Columns["Order #"].SetOrdinal(0);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "OrderNo";
                    UserPopoup.Show();
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GRNItem);
                    DataTable _result = CHNLSVC.Inventory.GetGRNDocNo(SearchParams, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text), "DOCUMENT NO", txtDocNo.Text);
                    _result.Columns["Order #"].SetOrdinal(0);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "OrderNo";
                    UserPopoup.Show();
                }
              
            }
            if (lblvalue.Text == "DocNo")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                DataTable _result = CHNLSVC.CommonSearch.SearchAodDocumentForItemAllocation(SearchParams, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text), null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                lblvalue.Text = "DocNo";
                UserPopoup.Show();
                return;
            }
        }
        protected void grdResult2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string ID = grdResult2.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "loc")
            {
                txtlocation.Text = ID;
                lblvalue.Text = "";
                UserPopoup2.Hide();
                return;
            }
            if (lblvalue.Text == "Item")
            {
                txtItemSearch.Text = ID;
                lblvalue.Text = "";
                UserPopoup2.Hide();
                return;
            }
            if (lblvalue.Text == "Channel")
            {
                txtChannel.Text = ID;
                lblvalue.Text = "";
                UserPopoup2.Hide();
                return;
            }

        }

        protected void grdResult2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult2.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "loc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams,null, null);
                grdResult2.DataSource = _result;
                grdResult2.DataBind();
                UserPopoup2.Show();
                return;
            }
            if (lblvalue.Text == "Item")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportItem);
                DataTable result = CHNLSVC.CommonSearch.SearchItem(SearchParams, null, null);
                grdResult2.DataSource = result;
                grdResult2.DataBind();
                UserPopoup2.Show();
                return;
            }
            if (lblvalue.Text == "Channel")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Channel);
                DataTable result = CHNLSVC.CommonSearch.GetChanalDetailsNew(SearchParams, null, null);
                grdResult2.DataSource = result;
                grdResult2.DataBind();
                UserPopoup2.Show();
                return;
            }


            //if (lblvalue.Text == "channel")
            //{
            //    DataTable result = CHNLSVC.Inventory.GetChannelDetail(txtCompany.Text, null);
            //    DataView dv = new DataView(result);
            //    dv.RowFilter = "msc_act=1";
            //    result = dv.ToTable();
            //    result.Columns.RemoveAt(0);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns["msc_cd"].ColumnName = "Channel Code";
            //    result.Columns["msc_desc"].ColumnName = "Description";
            //    grdResult2.DataSource = result;
            //    grdResult2.DataBind();
            //    UserPopoup2.Show();
            //    return;
            //}
        }
        protected void lbtnSearch2_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "loc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey2.SelectedItem.ToString(), "%" + txtSearchbyword2.Text.ToString());
                grdResult2.DataSource = _result;
                grdResult2.DataBind();
                UserPopoup2.Show();
                return;
            }
            if (lblvalue.Text == "Item")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportItem);
                DataTable result = CHNLSVC.CommonSearch.SearchItem(SearchParams, ddlSearchbykey2.SelectedItem.ToString(), "%" + txtSearchbyword2.Text.ToString());
                grdResult2.DataSource = result;
                grdResult2.DataBind();
                UserPopoup2.Show();
                Session["Item"] = "true";
                return;
            }
            if (lblvalue.Text == "Channel")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Channel);
                DataTable _result = CHNLSVC.CommonSearch.GetChanalDetailsNew(SearchParams, ddlSearchbykey2.SelectedItem.ToString(), "%" + txtSearchbyword2.Text.ToString());
                grdResult2.DataSource = _result;
                grdResult2.DataBind();
                UserPopoup2.Show();
                return;
            }

            //if (lblvalue.Text == "channel")
            //{
            //    DataTable result = CHNLSVC.Inventory.GetChannelDetail(txtCompany.Text, txtSearchbyword2.Text.ToString());
            //    DataView dv = new DataView(result);
            //    dv.RowFilter = "msc_act=1";
            //    result = dv.ToTable();
            //    result.Columns.RemoveAt(0);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns["msc_cd"].ColumnName = "Channel Code";
            //    result.Columns["msc_desc"].ColumnName = "Description";
            //    grdResult2.DataSource = result;
            //    grdResult2.DataBind();
            //    UserPopoup2.Show();
            //    return;
            //}
        }
        protected void txtSearchbyword2_TextChanged(object sender, EventArgs e)
        {
            if (lblvalue.Text == "loc")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey2.SelectedItem.ToString(), "%" + txtSearchbyword2.Text.ToString());
                grdResult2.DataSource = _result;
                grdResult2.DataBind();
                UserPopoup2.Show();
                return;
            }
            if (lblvalue.Text == "Item")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportItem);
                DataTable result = CHNLSVC.CommonSearch.SearchItem(SearchParams, ddlSearchbykey2.SelectedItem.ToString(), "%" + txtSearchbyword2.Text.ToString());
                grdResult2.DataSource = result;
                grdResult2.DataBind();
                UserPopoup2.Show();
                return;
            }
            if (lblvalue.Text == "Channel")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Channel);
                DataTable _result = CHNLSVC.CommonSearch.GetChanalDetailsNew(SearchParams, ddlSearchbykey2.SelectedItem.ToString(), "%" + txtSearchbyword2.Text.ToString());
                grdResult2.DataSource = _result;
                grdResult2.DataBind();
                UserPopoup2.Show();
                return;
            }
            //if (lblvalue.Text == "channel")
            //{
            //    DataTable result = CHNLSVC.Inventory.GetChannelDetail(txtCompany.Text, txtSearchbyword2.Text.ToString());
            //    DataView dv = new DataView(result);
            //    dv.RowFilter = "msc_act=1";
            //    result = dv.ToTable();
            //    result.Columns.RemoveAt(0);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns.RemoveAt(2);
            //    result.Columns["msc_cd"].ColumnName = "Channel Code";
            //    result.Columns["msc_desc"].ColumnName = "Description";
            //    grdResult2.DataSource = result;
            //    grdResult2.DataBind();
            //    UserPopoup2.Show();
            //    return;
            //}
        }

        private void Save()
        {
           

        }

        #endregion
        private void calFreeQty()
        {
            decimal qty = 0;
            _MasterItemAllocate = ViewState["SaveItemAllocate"] as List<MasterItemAllocate>;
            DataTable _resultc = ViewState["Temp_ItemDetails"] as DataTable;
            if (_MasterItemAllocate == null)
            {
                _MasterItemAllocate = new List<MasterItemAllocate>();
            }
            if (_resultc != null)
            {
                foreach (DataRow dd in _resultc.Rows)
                {
                    string itemcode = dd[0].ToString();
                    string Doc = dd[7].ToString();
                    // string ILine = dd[8].ToString();
                    // string IBline = dd[9].ToString();

                    if ((itemcode == txtItem.Text) && (Doc == Session["Doc"].ToString()))
                    {
                        qty = Convert.ToDecimal(dd[6].ToString());

                    }
                }
                if (qty == 0)
                {
                    return;
                }


                MasterItemAllocate _ItemAllocate = new MasterItemAllocate();
                _ItemAllocate.Isa_act = true;
                _ItemAllocate.Isa_aloc_bqty = qty;
                _ItemAllocate.Isa_aloc_qty = qty;
                _ItemAllocate.Isa_chnl = "FREE";
                _ItemAllocate.Isa_com = txtCompany.Text;
                _ItemAllocate.Isa_cre_by = Session["UserID"].ToString();
                _ItemAllocate.Isa_cre_dt = System.DateTime.Now;
                _ItemAllocate.Isa_doc_dt = Convert.ToDateTime(Session["DocDate"].ToString());
                _ItemAllocate.Isa_doc_no = txtDocNo.Text;
                _ItemAllocate.Isa_doc_tp = ddlDocType.SelectedValue;
                _ItemAllocate.Isa_dt = System.DateTime.Now;
                _ItemAllocate.Isa_itm_cd = txtItem.Text;
                _ItemAllocate.Isa_itm_changed = false;
                _ItemAllocate.Isa_itm_stus = Session["ItemStatus"].ToString();
                _ItemAllocate.Isa_loc = string.IsNullOrEmpty(txtlocation.Text) ? Session["UserDefLoca"].ToString() : txtlocation.Text;
                _ItemAllocate.Isa_ref_no = null;
                _ItemAllocate.Isa_req_bqty = 0;
                _ItemAllocate.Isa_req_qty = 0;
                _ItemAllocate.Isa_session_id = Session["SessionID"].ToString();
                if (ddlDocType.SelectedValue == "PO")
                {
                    _ItemAllocate.Isa_tp = "p";
                }
                else if (ddlDocType.SelectedValue == "GRN")
                {
                    _ItemAllocate.Isa_tp = "I";
                }
                else
                {
                    _ItemAllocate.Isa_tp = "I";
                }
                _MasterItemAllocate.Add(_ItemAllocate);

                ViewState["SaveItemAllocate"] = _MasterItemAllocate;
            }
        }

      
        protected void lbtnWItemok_Click(object sender, EventArgs e)
        {
            ClearMsg();

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
            txtClearlconformmessageValue.Value = "";
        }

        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            bool IsDelete = false;
            if (txtCancelconformmessageValue.Value == "Yes")
            {
                List<MasterItemAllocate> _temp = new List<MasterItemAllocate>();
                foreach (GridViewRow dgvr in grdAlItem.Rows)
                {
                    CheckBox chk = (CheckBox)dgvr.FindControl("chk_Req");

                    if (chk != null & chk.Checked)
                    {
                        string _seqvalue =(dgvr.FindControl("col_seq") as Label).Text;
                        if ((_seqvalue != "") || (!string.IsNullOrEmpty(_seqvalue)))
                        {
                            Int32 _seq = Convert.ToInt32((dgvr.FindControl("col_seq") as Label).Text);
                            MasterItemAllocate _obj = new MasterItemAllocate();
                            _obj.Isa_seq = _seq;
                            _obj.Isa_act = false;
                            _obj.Isa_cnl_by = Session["UserID"].ToString();
                            _obj.Isa_cnl_dt = System.DateTime.Now;
                            _obj.Isa_cnl_session_id = Session["SessionID"].ToString();
                            if (_seq != 0)
                            {
                                _temp.Add(_obj);
                                IsDelete = true;
                            }
                        }
                        else
                        {
                            
                        }
                        
                        
                        
                    }
                }
                if (IsDelete == true)
                {
                    Int32 _out = CHNLSVC.Inventory.DeleteStockAllocate(_temp);
                    if (_out > 0)
                    {
                        _MasterItemAllocate = null;
                        SuccessMsg("Items(s) has been removed Successfully ! ");
                        txtDocNo_TextChanged(null, null);
                        //LoadAllocate(Session["ItemCode"].ToString());
                        //caluclateqty(Session["ItemCode"].ToString());
                        return;
                    }

                }
                else
                {
                    if (Session["ItemCode"] == null)
                    {
                        return;
                    }
                    LoadAllocate(Session["ItemCode"].ToString());
                    caluclateqty(Session["ItemCode"].ToString());
                }
            }
        }
        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            Int32 row_aff = 0;
            calFreeQty();
            _MasterItemAllocate = ViewState["SaveItemAllocate"] as List<MasterItemAllocate>;
            DataTable _Deletbl = ViewState["DelItem"] as DataTable;
            if (txtSavelconformmessageValue.Value == "No")
            {
                return;
            }

            row_aff = (Int32)CHNLSVC.Inventory.SaveStockAllocate(_MasterItemAllocate, _Deletbl);

            if (row_aff == 1)
            {
              //  SuccessMsg("Item(s) Allocated Successfully !");
                DispMsg("Item(s) Allocated Successfully !","S");
                ClearPage();
            }
        }

        protected void ddlDocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDocType.SelectedItem.Text == "Purchase Order")
            {
                DivType.Visible = true;
                DivLoc.Visible = false;
                DivPo.Visible = false;
                ClearPage();
                return;
            }
            if (ddlDocType.SelectedItem.Text == "GRN")
            {
                DivType.Visible = false;
                DivLoc.Visible = true;
                DivPo.Visible = false;
                ClearPage();
                return;
            }
            if (ddlDocType.SelectedItem.Text == "AOD-IN-LOCAL")
            {
                DivType.Visible = false;
                DivLoc.Visible = true;
                DivPo.Visible = false;
                ClearPage();
                return;
            }
        }

        protected void lbtnlocation_Click(object sender, EventArgs e)
        {
            if (ddlDocType.SelectedValue == "0")
            {
               // ErrorMsg("Please Select Document Type..!");
                DispMsg("Please select a Document Type !");
                return;
            }
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
            grdResult2.DataSource = _result;
            grdResult2.DataBind();
            BindUCtrlDDLData2(_result);
            lblvalue.Text = "loc";
            UserPopoup2.Show();
        }

        protected void lbtnDocNo_Click(object sender, EventArgs e)
        {

            if (ddlDocType.SelectedValue == "0")
            {
                //ErrorMsg("Please select a Document Type !");
                DispMsg("Please select a Document Type !");
                return;
            }
            if (ddlDocType.SelectedItem.Text == "Purchase Order")
            {
                if (ddlPType.SelectedValue == "0")
                {
                   // ErrorMsg("Please select a Type ! ");
                    DispMsg("Please select a Type ! ");
                    return;
                }
                DateTime d1 = DateTime.Now;
                d1 = d1.AddMonths(-1);
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PurchaseOrder);
                DataTable _result = CHNLSVC.Inventory.GetPurDocNo(SearchParams, d1, System.DateTime.Now, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                txtTDate.Text = System.DateTime.Now.ToShortDateString();
                txtFDate.Text = d1.ToShortDateString();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "PuDocNo";
                UserPopoup.Show();
            }
            if (ddlDocType.SelectedItem.Text == "GRN")
            {
                //if (txtlocation.Text == "")
                //{
                //    ErrorMsg("Please select  Location..!");
                //    return;
                //}
                DateTime d1 = DateTime.Now;
                d1 = d1.AddMonths(-1);
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GRNItem);
                DataTable _result = CHNLSVC.Inventory.GetGRNDocNo(SearchParams, d1, System.DateTime.Now, null, null);
                var sortedTable = _result;
                if (_result.Rows.Count > 0)
                {
                    sortedTable = _result.AsEnumerable()
                                .OrderByDescending(r => r.Field<String>("DOCUMENT NO"))
                                .CopyToDataTable();

                }
                
                grdResult.DataSource = sortedTable;
                grdResult.DataBind();
                txtTDate.Text = System.DateTime.Now.ToShortDateString();
                txtFDate.Text = d1.ToShortDateString();
                BindUCtrlDDLData(_result);
                ddlSearchbykey.Items.FindByText("DATE").Enabled = false;
                lblvalue.Text = "GRNDocNo";
                UserPopoup.Show();
            }
            if (ddlDocType.SelectedItem.Text == "AOD-IN-LOCAL")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                DataTable _result = CHNLSVC.CommonSearch.SearchAodDocumentForItemAllocation(SearchParams, DateTime.Today, DateTime.Today, null, null);
                var sortedTable = _result;
                if (_result.Rows.Count > 0)
                {
                    sortedTable = _result.AsEnumerable()
                                .OrderByDescending(r => r.Field<String>("DOCUMENT NO"))
                                .CopyToDataTable();

                }

                grdResult.DataSource = sortedTable;
                grdResult.DataBind();
                txtTDate.Text = DateTime.Today.ToShortDateString();
                txtFDate.Text = DateTime.Today.ToShortDateString();
                DataTable dt = new DataTable();
                dt.Columns.Add("Document No");
                BindUCtrlDDLData(dt);
                lblvalue.Text = "DocNo";
                UserPopoup.Show();
            }
        }

        protected void lbtnItem_Click(object sender, EventArgs e)
        {

            if (optItem.Checked == true)
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportItem);
                DataTable result = CHNLSVC.CommonSearch.SearchItem(SearchParams, null, null);
                grdResult2.DataSource = result;
                grdResult2.DataBind();
                BindUCtrlDDLData2(result);
                lblvalue.Text = "Item";
                UserPopoup2.Show();
                return;
            }
            if (optModel.Checked == true)
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportItem);
                DataTable result = CHNLSVC.CommonSearch.SearchItem(SearchParams, null, null);
                result.Columns["MODEL"].SetOrdinal(0);
                grdResult2.DataSource = result;
                grdResult2.DataBind();
                BindUCtrlDDLData2(result);
                lblvalue.Text = "Item";
                UserPopoup2.Show();
                return;
            }
            if (optPo.Checked == true)
            {
                if (txtDocNo.Text != "")
                {
                    DateTime d1 = DateTime.Now;
                    d1 = d1.AddMonths(-1);
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GRNItem);
                    DataTable _result = CHNLSVC.Inventory.GetGRNDocNo(SearchParams, d1, System.DateTime.Now, "DOCUMENT NO", txtDocNo.Text);
                    _result.Columns["Order #"].SetOrdinal(0);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    txtTDate.Text = System.DateTime.Now.ToShortDateString();
                    txtFDate.Text = d1.ToShortDateString();
                    BindUCtrlDDLData(_result);
                    ddlSearchbykey.Items.FindByText("DATE").Enabled = false;
                    lblvalue.Text = "OrderNo";
                    UserPopoup.Show();
                }
                else
                {
                    DateTime d1 = DateTime.Now;
                    d1 = d1.AddMonths(-1);
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GRNItem);
                    DataTable _result = CHNLSVC.Inventory.GetGRNDocNo(SearchParams, d1, System.DateTime.Now, null, null);
                    _result.Columns["Order #"].SetOrdinal(0);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    txtTDate.Text = System.DateTime.Now.ToShortDateString();
                    txtFDate.Text = d1.ToShortDateString();
                    BindUCtrlDDLData(_result);
                    ddlSearchbykey.Items.FindByText("DATE").Enabled = false;
                    lblvalue.Text = "OrderNo";
                    UserPopoup.Show();
                }
                
            }
            else
            {
              //  ErrorMsg("Please select an option in Document item search ! ");
                DispMsg("Please select an option in Document item search ! ");
                return;
            }
        }

        protected void lbtnFilter_Click(object sender, EventArgs e)
        {
            #region MyRegion
            if (ddlDocType.SelectedItem.Text == "Purchase Order")
            {
                if (string.IsNullOrEmpty(txtDocNo.Text))
                {
                   // ErrorMsg("Please select a Document # !");
                    DispMsg("Please select a Document # !");
                   
                }
                if (optItem.Checked == true)
                {
                    DataTable result = CHNLSVC.Inventory.GetPOItem(txtDocNo.Text, txtItemSearch.Text,"");
                    ViewState["ItemDetails"] = result;
                    TempDataTable();
                   
                }
                else if (optModel.Checked == true)
                {
                    DataTable result = CHNLSVC.Inventory.GetPOItem(txtDocNo.Text, "",txtItemSearch.Text);
                    ViewState["ItemDetails"] = result;
                    TempDataTable();
                    
                }
                else
                {
                    DataTable result = CHNLSVC.Inventory.GetPOItem(txtDocNo.Text, "", "");
                    ViewState["ItemDetails"] = result;
                    TempDataTable();
                   
                }

            }
            #endregion
            #region MyRegion
            if (ddlDocType.SelectedItem.Text == "GRN")
            {
                
                if (optItem.Checked == true)
                {
                    DataTable result = CHNLSVC.Inventory.GetGRNItem(txtDocNo.Text, txtItemSearch.Text,"","");
                    if ((result.Rows.Count > 0) && (result != null))
                    {
                        Session["DocDate"] = result.Rows[0][6].ToString();
                        string des = result.Rows[0][1].ToString();
                        if (des.Length > 30)
                        {
                            string maxdes = result.Rows[0][1].ToString();
                            result.Rows[0][1] = maxdes.Substring(0,30);
                        }
                    }
                  
                    ViewState["ItemDetails"] = result;
                    TempDataTable();
                    
                }
                else if (optModel.Checked == true)
                {
                    DataTable result = CHNLSVC.Inventory.GetGRNItem(txtDocNo.Text, "", txtItemSearch.Text,"");
                    if ((result.Rows.Count > 0) && (result != null))
                    {
                        Session["DocDate"] = result.Rows[0][6].ToString();
                        string des = result.Rows[0][1].ToString();
                        if (des.Length > 30)
                        {
                            string maxdes = result.Rows[0][1].ToString();
                            result.Rows[0][1] = maxdes.Substring(0, 30);
                        }
                    }
                    ViewState["ItemDetails"] = result;
                    TempDataTable();
                   
                }
                else if (optPo.Checked == true)
                {
                    DataTable result = CHNLSVC.Inventory.GetGRNItem(txtDocNo.Text, "", "", txtItemSearch.Text);
                    if ((result.Rows.Count > 0) && (result != null))
                    {
                        Session["DocDate"] = result.Rows[0][6].ToString();
                        string des = result.Rows[0][1].ToString();
                        if (des.Length > 30)
                        {
                            string maxdes = result.Rows[0][1].ToString();
                            result.Rows[0][1] = maxdes.Substring(0, 30);
                        }
                    }
                    ViewState["ItemDetails"] = result;
                    TempDataTable();
                   
                }
                else
                {
                    DataTable result = CHNLSVC.Inventory.GetGRNItem(txtDocNo.Text, "", "","");
                    if ((result.Rows.Count > 0) && (result != null))
                    {
                        Session["DocDate"] = result.Rows[0][6].ToString();
                        string des = result.Rows[0][1].ToString();
                        if (des.Length > 30)
                        {
                            string maxdes = result.Rows[0][1].ToString();
                            result.Rows[0][1] = maxdes.Substring(0, 30);
                        }
                    }
                    ViewState["ItemDetails"] = result;
                    TempDataTable();
                    
                }

            }
            #endregion
            #region MyRegion
            if (ddlDocType.SelectedItem.Text == "AOD-IN-LOCAL")
            {
                if (string.IsNullOrEmpty(txtDocNo.Text))
                {
                    // ErrorMsg("Please select a Document # !");
                    DispMsg("Please select a Document # !");
                    
                }
                if (optItem.Checked == true)
                {
                    DataTable result = CHNLSVC.Inventory.GetAodItem(txtDocNo.Text, txtItemSearch.Text, "");
                    ViewState["ItemDetails"] = result;
                    TempDataTable();
                  
                }
                else if (optModel.Checked == true)
                {
                    DataTable result = CHNLSVC.Inventory.GetAodItem(txtDocNo.Text, "", txtItemSearch.Text);
                    ViewState["ItemDetails"] = result;
                    TempDataTable();
                   
                }
                else
                {
                    DataTable result = CHNLSVC.Inventory.GetAodItem(txtDocNo.Text, "", "");
                    ViewState["ItemDetails"] = result;
                    TempDataTable();
                    
                }

            }
            #endregion
            // calFreeQty();
            DataTable _TBL = new DataTable();
            _TBL = ViewState["Temp_ItemDetails"] as DataTable;
            
            if(_TBL!=null)
            {
                if(_TBL.Rows.Count >0)
                {
                    TempDataTable();
                    foreach (DataRow item in _TBL.Rows)
                    {
                        caluclateqty(item[0].ToString());
                    }
                   

                }
            }
            DataTable _TBL2 = new DataTable();
            _TBL2 = ViewState["Temp_ItemDetails"] as DataTable;
            grdDocItem.DataSource = _TBL2;
            grdDocItem.DataBind();
        }
        protected void lbtnPOItemsAdd_Click(object sender, EventArgs e)
        {
            if (grdDocItem.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
               // grdAlItem.DataSource = new int[] { };
                //grdAlItem.DataBind();
                //ViewState["Temp_AlloItemDetails"] = "";
                //ViewState["SaveItemAllocate"] = "";
                string _ICode = (row.FindControl("col_ItemCode") as Label).Text;
                Session["ItemCode"] = _ICode;
                Session["ItemStatus"] = (row.FindControl("col_Istatus") as Label).Text;
                Session["Description"] = (row.FindControl("col_Description") as Label).Text;
                Session["Model"] = (row.FindControl("col_Model") as Label).Text;
                Session["Brand"] = (row.FindControl("col_Brand") as Label).Text;
                Session["status"] = (row.FindControl("col_Istatus") as Label).Text;
                Session["Doc"] = (row.FindControl("col_Idoc") as Label).Text;
                Session["statusName"] = (row.FindControl("col_IstatusName") as Label).Text;
                //Session["ILine"] = (row.FindControl("col_ILine") as Label).Text;
                //Session["IBLine"] = (row.FindControl("col_Ibatch") as Label).Text;
                _MasterItemAllocate = null;
                TempDataTable();
                txtItem.Text = _ICode;
                hiddQty.Value = "0";
                txtQty.Text = (row.FindControl("col_Aqty2") as Label).Text;
                decimal _qty  =Convert.ToDecimal( txtQty.Text);
                bool isInt = Convert.ToDecimal(txtQty.Text) % 1 == 0;
                if (isInt == true)
                {
                    int d = (int)Math.Round(_qty, 0);
                    txtQty.Text = d.ToString();
                }
                hiddQty.Value = txtQty.Text;
                //LoadAllocate(_ICode);
                Session["update"] = "true";
                caluclateqty(_ICode);
            }
        }

        private void caluclateqty(string _ICode)
        {
            DataTable _Temp_ItemDetails = ViewState["Temp_ItemDetails"] as DataTable;
            DataTable _Temp_AlloItemDetails = ViewState["Temp_AlloItemDetails"] as DataTable;
            decimal sum = 0;
            if (checkqty())
            {
                if (_Temp_AlloItemDetails != null)
                {
                    foreach (DataRow t in _Temp_AlloItemDetails.Rows)
                    {
                        string itemcode = t[2].ToString();
                        if (itemcode == _ICode)
                        {
                            sum += Convert.ToDecimal(t[7].ToString());
                        }

                    }
                    bool _getbalance = false;
                    //Session["ISqtyLow"] = "true";
                    foreach (DataRow trow in _Temp_ItemDetails.Rows)
                    {
                        string itemcode = trow[0].ToString();
                        decimal qty = Convert.ToDecimal(trow[6].ToString());
                        string Doc = trow[8].ToString();
                        //string ILine = trow[8].ToString();
                        //string IBline = trow[9].ToString();
                        if ((itemcode == _ICode) && (Doc == txtDocNo.Text))
                        {
                            if (!_getbalance)
                            {
                                if (sum != 0)
                                {
                                    decimal avalableQty = qty - sum;
                                    if (avalableQty < 0)
                                    {
                                        //  ErrorMsg("Please enter a quantity less than the balance quantity !");
                                        DispMsg("Please enter a quantity less than the balance quantity !");
                                        Session["ISqtyLow"] = "false";
                                        return;
                                    }
                                    trow.SetField("tqty", avalableQty);
                                }
                                if (txtQty.Text != "")
                                {
                                    decimal apqty = Convert.ToDecimal(txtQty.Text);
                                    if (qty < apqty)
                                    {
                                        //  ErrorMsg("Please enter a quantity less than the balance quantity !");
                                        DispMsg("Please enter a quantity less than the balance quantity !");
                                        Session["ISqtyLow"] = "false";
                                        return;
                                    }
                                }
                            }

                            _getbalance = true;
                        }
                    }
                }
            }
           
                grdDocItem.DataSource = _Temp_ItemDetails;
                grdDocItem.DataBind();
                ViewState["Temp_ItemDetails"] = _Temp_ItemDetails;

                grdAlItem.DataSource = _Temp_AlloItemDetails;
                grdAlItem.DataBind();
        }

        private void caluclateqtyNew(string _ICode)
        {
            DataTable _Temp_ItemDetails = ViewState["Temp_ItemDetails"] as DataTable;
            DataTable _Temp_AlloItemDetails = ViewState["Temp_AlloItemDetails"] as DataTable;
            decimal sum = 0;
            if (checkqtyNew())
            {
                if (_Temp_AlloItemDetails != null)
                {
                    foreach (DataRow t in _Temp_AlloItemDetails.Rows)
                    {
                        string itemcode = t[2].ToString();
                        if (itemcode == _ICode)
                        {
                            sum += Convert.ToDecimal(t[7].ToString());
                        }

                    }
                    bool _getitem = false;
                    foreach (DataRow trow in _Temp_ItemDetails.Rows)
                    {
                        string itemcode = trow[0].ToString();
                        decimal qty = Convert.ToDecimal(trow[6].ToString());
                        string Doc = trow[8].ToString();
                        //string ILine = trow[8].ToString();
                        //string IBline = trow[9].ToString();
                        if ((itemcode == _ICode) && (Doc == Session["Doc"].ToString()))
                        {
                            if (!_getitem)
                            {
                                if (sum != 0)
                                {
                                    decimal avalableQty = qty - sum;
                                    if (avalableQty < 0)
                                    {
                                        //  ErrorMsg("Please enter a quantity less than the balance quantity !");
                                        DispMsg("Please enter a quantity less than the balance quantity !");
                                        Session["ISqtyLow"] = "false";
                                        return;
                                    }
                                    trow.SetField("tqty", avalableQty);
                                }
                                if (txtQty.Text != "")
                                {
                                    decimal apqty = Convert.ToDecimal(txtQty.Text);
                                    if (qty < apqty)
                                    {
                                        //  ErrorMsg("Please enter a quantity less than the balance quantity !");
                                        DispMsg("Please enter a quantity less than the balance quantity !");
                                        Session["ISqtyLow"] = "false";
                                        return;
                                    }
                                }
                                _getitem = true;
                            }
                           

                        }
                    }
                }
            }

            grdDocItem.DataSource = _Temp_ItemDetails;
            grdDocItem.DataBind();
            ViewState["Temp_ItemDetails"] = _Temp_ItemDetails;


        }
        private bool checkqty()
        {
            DataTable _Temp_ItemDetails = ViewState["Temp_ItemDetails"] as DataTable;
            DataTable _Temp_AlloItemDetails = ViewState["Temp_AlloItemDetails"] as DataTable;
            decimal sum = 0;
            decimal sum2 = 0;
            if (_Temp_AlloItemDetails!=null)
            {
                foreach (DataRow t in _Temp_AlloItemDetails.Rows)
                {
                    string itemcode = t[2].ToString();
                    if ((itemcode == txtItem.Text))
                    {
                        sum += Convert.ToDecimal(t[7].ToString());
                    }

                }
                foreach (DataRow t in _Temp_ItemDetails.Rows)
                {
                    string itemcode = t[0].ToString();
                    decimal qty = Convert.ToDecimal(t[7].ToString());
                    string Doc = t[8].ToString();
                    // string ILine = t[8].ToString();
                    // string IBline = t[9].ToString();
                    if ((itemcode == txtItem.Text) && (Doc == Session["Doc"].ToString()))
                    {
                        sum2 = Convert.ToDecimal(t[7].ToString());
                    }

                }
            }
            if (sum2 < sum)
            {  
     
                return false;
            }
            return true ;
        }
        private bool checkqtyNew()
        {
            DataTable _Temp_ItemDetails = ViewState["Temp_ItemDetails"] as DataTable;
            DataTable _Temp_AlloItemDetails = ViewState["Temp_AlloItemDetails"] as DataTable;
            decimal sum = 0;
            decimal sum2 = 0;
            if (_Temp_AlloItemDetails != null)
            {
                sum = Convert.ToDecimal(txtQty.Text.ToString());
                foreach (DataRow t in _Temp_ItemDetails.Rows)
                {
                    string itemcode = t[0].ToString();
                    decimal qty = Convert.ToDecimal(t[7].ToString());
                    string Doc = t[8].ToString();
                    // string ILine = t[8].ToString();
                    // string IBline = t[9].ToString();
                    if ((itemcode == txtItem.Text) && (Doc == Session["Doc"].ToString()))
                    {
                        sum2 = Convert.ToDecimal(t[7].ToString());
                    }

                }
            }
            if (sum2 < sum)
            {
                return false;
            }
            return true;
        }
        private void LoadAllocate(string _ICode = null)
        {
            List<MasterItemAllocate> _MasterItemAllocate = null;
            _MasterItemAllocate = CHNLSVC.Inventory.GetStockAllocate(txtDocNo.Text, _ICode);
            string chanaldescr = "";
            if (_MasterItemAllocate != null && _MasterItemAllocate.Count>0)
            {
                DataRow dr = null;
                DataTable _temp = new DataTable();
                _temp.Columns.Add(new DataColumn("Channel", typeof(string)));
                _temp.Columns.Add(new DataColumn("Doc", typeof(string)));
                _temp.Columns.Add(new DataColumn("Item", typeof(string)));
                _temp.Columns.Add(new DataColumn("Des", typeof(string)));
                _temp.Columns.Add(new DataColumn("Model", typeof(string)));
                _temp.Columns.Add(new DataColumn("status", typeof(string)));
                _temp.Columns.Add(new DataColumn("statusName", typeof(string)));
                _temp.Columns.Add(new DataColumn("Aqty", typeof(decimal)));
                _temp.Columns.Add(new DataColumn("Bqty", typeof(decimal)));
                _temp.Columns.Add(new DataColumn("MRN", typeof(decimal)));
                _temp.Columns.Add(new DataColumn("seq", typeof(int)));
                _temp.Columns.Add(new DataColumn("Chnldes", typeof(string)));
                decimal Totalqty = 0;
                foreach (MasterItemAllocate _item in _MasterItemAllocate)
                {
                    string descr = "";
                    
                     DataTable cnldet = CHNLSVC.General.GetAllChannel(Session["UserCompanyCode"].ToString(), _item.Isa_chnl);
                     if (cnldet.Rows.Count > 0)
                     {
                         chanaldescr = cnldet.Rows[0][2].ToString();
                     }
                    dr = _temp.NewRow();
                    dr[0] = _item.Isa_chnl;
                    dr[1] = _item.Isa_doc_no;
                    dr[2] = _item.Isa_itm_cd;
                    MasterItem _mstItem = CHNLSVC.General.GetItemMaster(_item.Isa_itm_cd);
                    if (_mstItem != null)
                    {
                        descr = _mstItem.Mi_shortdesc.ToString();
                    }
                    if (descr.Length > 30)
                        {
                           dr[3] = descr.Substring(0,30);
                        }else
                    {
                        dr[3] = descr;
                    }
                   
                    dr[4] = _mstItem != null ? _mstItem.Mi_model : "";
                    dr[5] = _item.Isa_itm_stus;
                    dr[6] = _stsList.Where(c => c.Mis_cd == _item.Isa_itm_stus).FirstOrDefault().Mis_desc;
                    dr[7] = _item.Isa_aloc_qty;
                    dr[8] = _item.Isa_aloc_bqty;
                    dr[9] = 0;
                    dr[10] = _item.Isa_seq;
                    dr[11] = chanaldescr;
                    Session["seq"] = _item.Isa_seq;
                    Totalqty = Totalqty + _item.Isa_aloc_qty;
                    _temp.Rows.Add(dr);
                }
                if (_temp.Rows.Count>0)
                {
                    grdAlItem.DataSource = _temp;
                    grdAlItem.DataBind();
                    ViewState["Temp_AlloItemDetails"] = _temp;  
                }
                
            }
        }
        protected void lbtnchannel_Click(object sender, EventArgs e)
        {
           /* DataTable result = CHNLSVC.Inventory.GetChannelDetail(txtCompany.Text, null);
            DataView dv = new DataView(result);
            dv.RowFilter = "msc_act=1";
            result = dv.ToTable();
            result.Columns.RemoveAt(0);
            result.Columns.RemoveAt(2);
            result.Columns.RemoveAt(2);
            result.Columns.RemoveAt(2);
            result.Columns.RemoveAt(2);
            result.Columns.RemoveAt(2);
            result.Columns.RemoveAt(2);
            result.Columns.RemoveAt(2);
            result.Columns.RemoveAt(2);
            result.Columns.RemoveAt(2);
            result.Columns.RemoveAt(2);
            result.Columns.RemoveAt(2);
            result.Columns.RemoveAt(2);
            result.Columns.RemoveAt(2);
            result.Columns.RemoveAt(2);
            result.Columns.RemoveAt(2);
            result.Columns.RemoveAt(2);
            result.Columns.RemoveAt(2);
            result.Columns["msc_cd"].ColumnName = "Channel Code";
            result.Columns["msc_desc"].ColumnName = "Description";          
            grdResult2.DataSource = result;
            grdResult2.DataBind();
            BindUCtrlDDLData2(result);
            ddlSearchbykey2.Items.FindByText("Description").Enabled = false;
            lblvalue.Text = "channel";
            UserPopoup2.Show(); */

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Channel);
            DataTable _result = CHNLSVC.CommonSearch.GetChanalDetailsNew(SearchParams, null, null);
            grdResult2.DataSource = _result;
            grdResult2.DataBind();
            BindUCtrlDDLData2(_result);
            lblvalue.Text = "Channel";
            UserPopoup2.Show();
        }

        protected void lbtnItemadd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtChannel.Text))
            {
              //  ErrorMsg("Please Select Channel..!");
                DispMsg("Please Select Channel..!");
                return;
            }

            if (string.IsNullOrEmpty(txtItem.Text))
            {
               // ErrorMsg("Please Select Item..!");
                DispMsg("Please Select Item..!");
                return;
            }
            if (string.IsNullOrEmpty(txtQty.Text))
            {
              //  ErrorMsg("Please Enter Qty..!");
                DispMsg("Please Enter Qty..!");
                return;
            }
            decimal POQty = Convert.ToDecimal(hiddQty.Value);
            decimal changeqty = Convert.ToDecimal(txtQty.Text);
            if (changeqty > POQty)
            {
                DispMsg("Cannot exceed qty");
                return;
            }
             MasterItem _itemdetail = new MasterItem();
             _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text);
             if (_itemdetail != null)
             {
                 if ((_itemdetail.Mi_is_ser1 == 1) || (_itemdetail.Mi_is_ser1 == 0))
                 {
                     int i;
                     bool success = int.TryParse(txtQty.Text, out i);
                     if (!success)
                     {
                         DispMsg("Please enter valid allocation qty !!!"); return;
                     }
                 }
             }
             else
             {
                 DispMsg("Please enter valid allocation qty !!!"); return;
             }
                

            decimal bqty =0;
            DataTable _result = ViewState["Temp_ItemDetails"] as DataTable;
            DataTable _Temp_AlloItemDetails = ViewState["Temp_AlloItemDetails"] as DataTable;
            ViewState["Old_AlloItemDetails"] = _Temp_AlloItemDetails;
            if (_result.Rows.Count > 0)
            {
                grdDocItem.DataSource = _result;
                grdDocItem.DataBind();
                ViewState["Temp_ItemDetails"] = _result;
                TempAllocDataTable();
                _MasterItemAllocate = ViewState["SaveItemAllocate"] as List<MasterItemAllocate>;
               
                if (_MasterItemAllocate == null)
                {
                     _MasterItemAllocate = new List<MasterItemAllocate>();
                }


                MasterItemAllocate _ItemAllocate = new MasterItemAllocate();
                _ItemAllocate.Isa_act = true;
                _ItemAllocate.Isa_aloc_bqty = Convert.ToDecimal(txtQty.Text);
                _ItemAllocate.Isa_aloc_qty = Convert.ToDecimal(txtQty.Text);
                _ItemAllocate.Isa_chnl = txtChannel.Text;
                _ItemAllocate.Isa_com = txtCompany.Text;
                _ItemAllocate.Isa_cre_by = Session["UserID"].ToString();
                _ItemAllocate.Isa_cre_dt = System.DateTime.Now;
                _ItemAllocate.Isa_doc_dt = Convert.ToDateTime(Session["DocDate"].ToString());
                _ItemAllocate.Isa_doc_no = txtDocNo.Text;
                _ItemAllocate.Isa_doc_tp = ddlDocType.SelectedValue;
                _ItemAllocate.Isa_dt = System.DateTime.Now;
                _ItemAllocate.Isa_itm_cd = txtItem.Text;
                _ItemAllocate.Isa_itm_changed = false;
                _ItemAllocate.Isa_itm_stus = Session["ItemStatus"].ToString();
                _ItemAllocate.Isa_loc = string.IsNullOrEmpty(txtlocation.Text) ? Session["UserDefLoca"].ToString() : txtlocation.Text;
                _ItemAllocate.Isa_ref_no = null;
                _ItemAllocate.Isa_req_bqty = 0;
                _ItemAllocate.Isa_req_qty = 0;

                if (ddlDocType.SelectedValue == "PO")
                {
                    _ItemAllocate.Isa_tp = "p";
                }
                else if (ddlDocType.SelectedValue == "GRN")
                {
                    _ItemAllocate.Isa_tp = "I";
                }
                else
                {
                    _ItemAllocate.Isa_tp = "I";
                }
                _ItemAllocate.Isa_session_id = Session["SessionID"].ToString();
                caluclateqtyNew(txtItem.Text);
                if (Session["ISqtyLow"].ToString() == "false")
                {
                    Session["ISqtyLow"] = "";
                    return;
                }
                _MasterItemAllocate.Add(_ItemAllocate);
                ViewState["SaveItemAllocate"] = _MasterItemAllocate;
                txtChannel.Text = "";
                hiddQty.Value = (POQty - Convert.ToDecimal(txtQty.Text)).ToString();
                txtQty.Text = "";
                    
               
               
            }
           
        }

        private void TempDataTable()
        {
            DataRow dr = null;
            DataTable _result = ViewState["ItemDetails"] as DataTable;
            DataTable _temp = new DataTable();
            _temp.Columns.Add(new DataColumn("Itemcode", typeof(string)));
            _temp.Columns.Add(new DataColumn("Description", typeof(string)));
            _temp.Columns.Add(new DataColumn("Model", typeof(string)));
            _temp.Columns.Add(new DataColumn("Brand", typeof(string)));
            _temp.Columns.Add(new DataColumn("IStaus", typeof(string)));
            _temp.Columns.Add(new DataColumn("mis_desc", typeof(string)));
            _temp.Columns.Add(new DataColumn("qty", typeof(decimal)));
            _temp.Columns.Add(new DataColumn("tqty", typeof(decimal)));
            _temp.Columns.Add(new DataColumn("inb_doc_no", typeof(string)));
           // _temp.Columns.Add(new DataColumn("inb_itm_line", typeof(string)));
           // _temp.Columns.Add(new DataColumn("inb_batch_line", typeof(string)));

            foreach(DataRow dd in _result.Rows){
                 dr = _temp.NewRow();
                 dr[0] = dd[0].ToString();
                 dr[1] = dd[1].ToString();
                 dr[2] = dd[2].ToString();
                 dr[3] = dd[3].ToString();
                 dr[4] = dd[5].ToString();
                 dr[5] = dd[8].ToString();
                 dr[6] = dd[4].ToString();
                 dr[7] = dd[4].ToString();
                 dr[8] = dd[7].ToString();
                // dr[8] = dd[8].ToString();
                // dr[9] = dd[9].ToString();
                 _temp.Rows.Add(dr);
            }

            grdDocItem.DataSource = _temp;
            grdDocItem.DataBind();
            ViewState["Temp_ItemDetails"]= _temp;
        }

        private void TempDelItem()
        {
            bool Isalreadyadd = false;
            DataRow dr = null;
            DataTable _temDel = new DataTable();
            DataTable _DelItem = ViewState["DelItem"] as DataTable;
            if (_DelItem == null)
            {
                _DelItem = new DataTable();
                _DelItem.Columns.Add(new DataColumn("Seq", typeof(int)));
                _DelItem.Columns.Add(new DataColumn("Active", typeof(bool)));
                _DelItem.Columns.Add(new DataColumn("by", typeof(string)));
                _DelItem.Columns.Add(new DataColumn("Date", typeof(DateTime)));
                _DelItem.Columns.Add(new DataColumn("Session", typeof(string)));
                _DelItem.Columns.Add(new DataColumn("chanel", typeof(string)));
            }
            if (_DelItem != null)
            {
                foreach (DataRow r in _DelItem.Rows)
                {
                    if (txtChannel.Text == r[5].ToString())
                    {
                        Isalreadyadd = true;
                    }
                }
            }           
            if (Isalreadyadd == false)
            {
                dr = _DelItem.NewRow();
                dr[0] = Session["seq"].ToString();
                dr[1] = false;
                dr[2] = Session["UserID"].ToString();
                dr[3] = System.DateTime.Now;
                dr[4] = Session["SessionID"].ToString();
                dr[5] = txtChannel.Text;
                _DelItem.Rows.Add(dr);
            }
           
            ViewState["DelItem"] = _DelItem;
        }
        private void TempAllocDataTable()
        {
            bool _isNew = true;
            DataRow dr = null;
            string chanaldescr = "";
            DataTable _result = ViewState["Temp_AlloItemDetails"] as DataTable;
            DataTable cnldet = CHNLSVC.General.GetAllChannel(Session["UserCompanyCode"].ToString(), txtChannel.Text.ToString());
            if (cnldet.Rows.Count>0)
            {
                 chanaldescr = cnldet.Rows[0][2].ToString();
            }
            DataTable _temp = new DataTable();
            if ((_result == null) || (_result.Rows.Count==0))
            {               
                _temp.Columns.Add(new DataColumn("Channel", typeof(string)));
                _temp.Columns.Add(new DataColumn("Doc", typeof(string)));
                _temp.Columns.Add(new DataColumn("Item", typeof(string)));
                _temp.Columns.Add(new DataColumn("Des", typeof(string)));
                _temp.Columns.Add(new DataColumn("Model", typeof(string)));
                _temp.Columns.Add(new DataColumn("status", typeof(string)));
                _temp.Columns.Add(new DataColumn("statusName", typeof(string)));
                _temp.Columns.Add(new DataColumn("Aqty", typeof(decimal)));
                _temp.Columns.Add(new DataColumn("Bqty", typeof(decimal)));
                _temp.Columns.Add(new DataColumn("MRN", typeof(decimal)));
                _temp.Columns.Add(new DataColumn("seq", typeof(int)));
                _temp.Columns.Add(new DataColumn("Chnldes", typeof(string)));
            }
            if ((_result != null) && (_result.Rows.Count > 0))
            {
                foreach (DataRow row in _result.Rows)
                {
                    string channel = row[0].ToString();
                    string doc = row[1].ToString();
                    string Item = row[2].ToString();
                    if ((channel == txtChannel.Text) && (doc == txtDocNo.Text) && (Item == txtItem.Text))
                    {
                        decimal _aqty = Convert.ToDecimal(row["Aqty"].ToString());
                        decimal _newaty = _aqty + Convert.ToDecimal(txtQty.Text);
                        row.SetField("Aqty", _newaty);
                        row.SetField("Bqty", _newaty);
                        _isNew = false;
                        //if (Session["update"].ToString() == "true")
                        //{
                        //    TempDelItem();
                        //}
                       
                    }
                }
                if (_isNew == true)
                {
                    dr = _result.NewRow();
                    dr[0] = txtChannel.Text;
                    dr[1] = txtDocNo.Text;
                    dr[2] = txtItem.Text;
                    dr[3] = Session["Description"].ToString();
                    dr[4] = Session["Model"].ToString();
                    dr[5] = Session["status"].ToString();
                    dr[6] = Session["statusName"].ToString();
                    dr[7] = Convert.ToDecimal(txtQty.Text);
                    dr[8] = Convert.ToDecimal(txtQty.Text);
                    dr[9] = 0;
                    dr[10] = 0;
                    dr[11] = chanaldescr;
                    _result.Rows.Add(dr);
                }
               
                if (checkqtyNew())
                {
                    ViewState["Temp_AlloItemDetails"] = _result;
                    grdAlItem.DataSource = _result;
                    grdAlItem.DataBind();
                    return;
                }
                //DataTable _Old = ViewState["Old_AlloItemDetails"] as DataTable;
                //grdAlItem.DataSource = _Old;
                //grdAlItem.DataBind();
                Session["ISqtyLow"] = "false";
               // ErrorMsg("Please enter a quantity less than the balance quantity !");
                DispMsg("Please enter a quantity less than the balance quantity !");
                LoadAllocate(txtItem.Text);

              

               
            }
            else
            {
                dr = _temp.NewRow();
                dr[0] = txtChannel.Text;
                dr[1] = txtDocNo.Text;
                dr[2] = txtItem.Text;
                dr[3] = Session["Description"].ToString();
                dr[4] = Session["Model"].ToString();
                dr[5] = Session["status"].ToString();
                dr[6] = Session["statusName"].ToString();
                dr[7] = Convert.ToDecimal(txtQty.Text);
                dr[8] = Convert.ToDecimal(txtQty.Text);
                dr[9] = 0;
                dr[11] = chanaldescr;
                _temp.Rows.Add(dr);
               
                if (checkqty())
                {
                    ViewState["Temp_AlloItemDetails"] = _temp;
                    grdAlItem.DataSource = _temp;
                    grdAlItem.DataBind();
                    return;
                }
                //DataTable _Old = ViewState["Old_AlloItemDetails"] as DataTable;
                //grdAlItem.DataSource = _Old;
                //grdAlItem.DataBind();
                Session["ISqtyLow"] = "false";
               // ErrorMsg("Please enter a quantity less than the balance quantity !");
                DispMsg("Please enter a quantity less than the balance quantity !");
                LoadAllocate(txtItem.Text);
            }     
          
        }

        protected void optItem_CheckedChanged(object sender, EventArgs e)
        {
            txtItemSearch.Text = "";
        }

        protected void optModel_CheckedChanged(object sender, EventArgs e)
        {
            txtItemSearch.Text = "";
        }

        protected void txtlocation_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "Code", "%" + txtlocation.Text);
            if (_result.Rows.Count == 0)
            {
                //ErrorMsg("Please enter a valid location !");
                DispMsg("Please enter a valid location !");
                txtlocation.Text = "";
                return;
            }
        }

        protected void txtDocNo_TextChanged(object sender, EventArgs e)
        {
            grdAlItem.EditIndex = -1;
            ViewState["Temp_AlloItemDetails"] = new DataTable();
            ViewState["Temp_ItemDetails"] = new DataTable();
            grdDocItem.DataSource = new int[] { };
            grdAlItem.DataSource = new int[] { };
            grdDocItem.DataBind();
            grdAlItem.DataBind();
            if (string.IsNullOrEmpty(txtDocNo.Text))
            {
                return;
            }
            //DateTime d1 = DateTime.Now;
            //DateTime? dt = null;
            if (ddlDocType.SelectedValue == "0")
            {
               // ErrorMsg("Please select a Document Type !");
                DispMsg("Please select a Document Type !");
                return;
            }
            if (ddlDocType.SelectedValue == "PO")
            {
                PurchaseOrder _pordr = new PurchaseOrder();
                if (ddlPType.SelectedValue == "0")
                {
                   
                    _pordr = CHNLSVC.Inventory.GetPurchaseOrderHeaderDetails(Session["UserCompanyCode"].ToString(), txtDocNo.Text);

                    if (_pordr == null )
                    {
                       // ErrorMsg("Please enter a valid Document No..!");
                        DispMsg("Please enter a valid Document No..!");
                        txtDocNo.Text = "";
                        grdDocItem.DataSource = new int[] { };
                        grdDocItem.DataBind();
                        grdAlItem.DataSource = new int[] { };
                        grdAlItem.DataBind();
                        return;
                    }
                    if (_pordr.Poh_stus == "F")
                    {
                        DispMsg("Please enter a valid Document No..!");
                        txtDocNo.Text = "";
                        grdDocItem.DataSource = new int[] { };
                        grdDocItem.DataBind();
                        grdAlItem.DataSource = new int[] { };
                        grdAlItem.DataBind();
                        return;
                    }
                    Session["DocDate"] = _pordr.Poh_dt;
                }
                else
                {
                    _pordr = CHNLSVC.Inventory.GetPOHeader(Session["UserCompanyCode"].ToString(), txtDocNo.Text, ddlPType.SelectedValue);

                    if (_pordr == null)
                    {
                       // ErrorMsg("Please enter a valid Document No..!");
                        DispMsg("Please enter a valid Document No..!");
                        txtDocNo.Text = "";
                        grdDocItem.DataSource = new int[] { };
                        grdDocItem.DataBind();
                        grdAlItem.DataSource = new int[] { };
                        grdAlItem.DataBind();
                        return;
                    }
                    if (_pordr.Poh_stus == "F")
                    {
                        DispMsg("Please enter a valid Document No..!");
                        txtDocNo.Text = "";
                        grdDocItem.DataSource = new int[] { };
                        grdDocItem.DataBind();
                        grdAlItem.DataSource = new int[] { };
                        grdAlItem.DataBind();
                        return;
                    }
                    Session["DocDate"] = _pordr.Poh_dt;
                }
               
            }
            if (ddlDocType.SelectedValue == "GRN")
            {
                InventoryHeader _IN = new InventoryHeader();
                if (txtlocation.Text == "")
                {
                    _IN = CHNLSVC.Inventory.Get_Int_Hdr(txtDocNo.Text);
                    if (_IN == null)
                    {
                      //  ErrorMsg("Please enter a valid Document No..!");
                        DispMsg("Please enter a valid Document No..!");
                        grdDocItem.DataSource = new int[] { };
                        grdDocItem.DataBind();
                        grdAlItem.DataSource = new int[] { };
                        grdAlItem.DataBind();
                        txtDocNo.Text = "";
                        return;
                    }
                    Session["DocDate"] = _IN.Ith_doc_date;
                }
                else
                {
                    DataTable _tbl = CHNLSVC.Inventory.getDocDetByDocNo(Session["UserCompanyCode"].ToString(),txtlocation.Text,txtDocNo.Text);
                    if ((_tbl == null) || (_tbl.Rows.Count ==0))
                    {
                       // ErrorMsg("Please enter a valid Document No..!");
                        DispMsg("Please enter a valid Document No..!");
                        grdDocItem.DataSource = new int[] { };
                        grdDocItem.DataBind();
                        grdAlItem.DataSource = new int[] { };
                        grdAlItem.DataBind();
                        txtDocNo.Text = "";
                        return;
                    }
                    Session["DocDate"] = _IN.Ith_doc_date;
                }
            }
            if (ddlDocType.SelectedValue == "AOD")
            {
                InventoryHeader _IN = new InventoryHeader();
                if (txtlocation.Text == "")
                {
                    _IN = CHNLSVC.Inventory.Get_Int_Hdr(txtDocNo.Text);
                    if (_IN == null)
                    {
                        //  ErrorMsg("Please enter a valid Document No..!");
                        DispMsg("Please enter a valid Document No..!");
                        grdDocItem.DataSource = new int[] { };
                        grdDocItem.DataBind();
                        grdAlItem.DataSource = new int[] { };
                        grdAlItem.DataBind();
                        txtDocNo.Text = "";
                        return;
                    }
                    Session["DocDate"] = _IN.Ith_doc_date;
                }
                else
                {
                    DataTable _tbl = CHNLSVC.Inventory.getDocDetByDocNo(Session["UserCompanyCode"].ToString(), txtlocation.Text, txtDocNo.Text);
                    if ((_tbl == null) || (_tbl.Rows.Count == 0))
                    {
                        // ErrorMsg("Please enter a valid Document No..!");
                        DispMsg("Please enter a valid Document No..!");
                        grdDocItem.DataSource = new int[] { };
                        grdDocItem.DataBind();
                        grdAlItem.DataSource = new int[] { };
                        grdAlItem.DataBind();
                        txtDocNo.Text = "";
                        return;
                    }
                    Session["DocDate"] = _IN.Ith_doc_date;
                }
            }
            if (!string.IsNullOrEmpty(txtDocNo.Text))
            {
               
                LoadAllocate();
                lbtnFilter_Click(null, null);
            }
        }



        protected void txtChannel_TextChanged(object sender, EventArgs e)
        {
            DataTable result = CHNLSVC.Inventory.GetChannelDetail(txtCompany.Text, txtChannel.Text);
            if (result.Rows.Count == 0)
            {
               // ErrorMsg("Please enter a valid channel..!");
                DispMsg("Please enter a valid channel..!");
                txtChannel.Text = "";
                return;
            }
        }

        protected void lbtnCancelEdit_Click(object sender, EventArgs e)
        {
            try
            {
                grdAlItem.EditIndex = -1;
                this.BindPortGrid();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

       

       
        protected void BindPortGrid()
        {
            try
            {
                grdAlItem.DataSource = (DataTable)ViewState["Temp_AlloItemDetails"];
                grdAlItem.DataBind();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }
        private void DispMsg(string msgText, string msgType = "")
        {
            //msgText = msgText.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");

            if (msgType == "N")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + msgText + "');", true);
            }
            else if (msgType == "W" || msgType == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msgText + "');", true);
            }
            else if (msgType == "S")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + msgText + "');", true);
            }
            else if (msgType == "E")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + msgText + "');", true);
            }
            else if (string.IsNullOrEmpty(msgType))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msgText + "');", true);
            }
        }

        protected void lbtnUpdateEdit_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtItemDetails = ViewState["Temp_ItemDetails"] as DataTable;
                DataTable dtAllocation = ViewState["Temp_AlloItemDetails"] as DataTable;
                LinkButton btn = (LinkButton)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                bool itemAva = false;
                if (dtItemDetails!=null)
                {
                    if (dtItemDetails.Rows.Count > 0)
                    {
                        itemAva = true;
                    }
                }
                if (!itemAva)
                {
                    ViewState["Temp_AlloItemDetails"] = dtAllocation;
                    grdAlItem.EditIndex = -1;
                    this.BindPortGrid();
                    DispMsg("No document item details found !!!");  return; 
                }
                
                TextBox txtAqty = row.FindControl("txtAqty") as TextBox;
                Label col_Item = row.FindControl("col_Item") as Label;
                Label col_Aqty = row.FindControl("lblAqty") as Label;
                Label col_Bqty = row.FindControl("col_Bqty") as Label;
                Label docNo = row.FindControl("col_reqDate") as Label;
                MasterItem _itemdetail = new MasterItem();
                _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), col_Item.Text);
                if (_itemdetail != null)
                {
                    decimal _addqty = Convert.ToDecimal(txtAqty.Text);
                    if (_addqty <= 0)
                    {
                        DispMsg("Please enter valid allocation qty !!!"); return;
                    }
                    if ((_itemdetail.Mi_is_ser1 == 1)||(_itemdetail.Mi_is_ser1 == 0))
                    {
                        int i;
                        bool success = int.TryParse(txtAqty.Text, out i);
                        if (!success)
                        {
                            DispMsg("Please enter valid allocation qty !!!"); return;
                        }
                    }
                    decimal tmpDecimal = 0;
                    decimal bQty = Convert.ToDecimal(col_Bqty.Text);
                    decimal aQty = Convert.ToDecimal(col_Aqty.Text);
                    decimal availableBal = 0;
                    decimal allSum = 0;
                    foreach (DataRow dr in dtItemDetails.Rows)
                    {
                        string itemcode = dr[0].ToString();
                        if ((itemcode == col_Item.Text))
                        {
                            availableBal += Convert.ToDecimal(dr[7].ToString());
                        }
                    }
                    foreach (DataRow t in dtAllocation.Rows)
                    {
                        string itemcode = t[2].ToString();
                        if ((itemcode == txtItem.Text))
                        {
                            allSum += Convert.ToDecimal(t[7].ToString());
                        }
                    }
                    availableBal = availableBal - allSum;
                    availableBal += decimal.TryParse(col_Aqty.Text, out tmpDecimal) ? Convert.ToDecimal(col_Aqty.Text) : 0;
                    decimal appQty = 0;
                    try
                    {
                        appQty = Convert.ToDecimal(txtAqty.Text);
                    }
                    catch (Exception)
                    {
                        DispMsg("Please enter valid allocation qty !!!"); return;
                    }
                    availableBal = Convert.ToDecimal(col_Bqty.Text);
                    if (availableBal >= appQty)
                    {
                        decimal _newAppBal = bQty - aQty + appQty;
                        dtAllocation.Rows[row.RowIndex]["Aqty"] = appQty.ToString();
                        dtAllocation.Rows[row.RowIndex]["Bqty"] = _newAppBal.ToString();
                        foreach (DataRow dataRow in dtItemDetails.Rows)
                        {
                            string itemcode = dataRow[0].ToString();
                            if ((itemcode == col_Item.Text))
                            {
                                decimal avaItemBal = Convert.ToDecimal(dataRow["tqty"].ToString());
                                decimal newBal = avaItemBal + bQty - _newAppBal;
                                dataRow.SetField("tqty", newBal);
                            }
                        }
                    }
                    else
                    {
                        DispMsg("Available balance exceeded !!!"); txtAqty.Focus(); return;
                    }
                    ViewState["Temp_AlloItemDetails"] = dtAllocation;
                    grdAlItem.EditIndex = -1;
                    this.BindPortGrid();

                    ViewState["Temp_ItemDetails"] = dtItemDetails;
                    grdDocItem.DataSource = dtItemDetails;
                    grdDocItem.DataBind();
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

       
        protected void grdAlItem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grdAlItem.EditIndex = e.NewEditIndex;
                this.BindPortGrid();
                Label col_Item = (Label)grdAlItem.Rows[e.NewEditIndex].FindControl("col_Item");
                caluclateqty(col_Item.Text);
                //var lb = (LinkButton)sender;
                //var row = (GridViewRow)lb.NamingContainer;
                //Label col_Item = row.FindControl("col_Item") as Label;
                //calFreeQtyNew(col_Item.Text);
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtQty.Text))
            {
                DispMsg("Please enter the allocation qty !!!");
                txtQty.Text = "";
                txtQty.Focus();
                return;
            }
            decimal qty = Convert.ToDecimal(txtQty.Text.ToString());
            if (qty <= 0)
            {
                DispMsg("Please enter valid allocation qty !!!");
                txtQty.Text = "";
                txtQty.Focus();
                return;
            }
        }


        protected void lbtnRemove_Click(object sender, EventArgs e)
        {
            if (txtDeleteconformmessageValue.Value == "No")
            {
                return;
            }
            if (grdAlItem.Rows.Count == 0) return;
            DataTable _Temp_ItemDetails = ViewState["Temp_ItemDetails"] as DataTable;
            DataTable _Temp_AlloItemDetails = ViewState["Temp_AlloItemDetails"] as DataTable;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _chnl= (row.FindControl("col_channel") as Label).Text;
                string _doc = (row.FindControl("col_reqDate") as Label).Text;
                string _item = (row.FindControl("col_Item") as Label).Text;
                string _SUM = (row.FindControl("col_Bqty") as Label).Text;
                string _seq = (row.FindControl("col_seq") as Label).Text;
                if (string.IsNullOrEmpty(_item)) return;
                DataTable dtAllocation = ViewState["Temp_AlloItemDetails"]  as DataTable;

                if ((string.IsNullOrEmpty(_seq)) || (_seq == "0"))
                {
                    if (dtAllocation != null)
                    {
                        for (int i = dtAllocation.Rows.Count - 1; i >= 0; i--)
                        {
                            DataRow dr = dtAllocation.Rows[i];
                            if ((dr["Channel"].ToString() == _chnl) && (dr["Doc"].ToString() == _doc) && (dr["Item"].ToString() == _item))
                                dr.Delete();
                        }
                        dtAllocation.AcceptChanges();
                        grdAlItem.DataSource = dtAllocation;
                        grdAlItem.DataBind();
                        foreach (DataRow trow in _Temp_ItemDetails.Rows)
                        {
                            string itemcode = trow[0].ToString();
                            decimal qty = Convert.ToDecimal(trow[7].ToString());
                            string Doc = trow[8].ToString();
                            //string ILine = trow[8].ToString();
                            //string IBline = trow[9].ToString();
                            if ((itemcode == _item) && (Doc == _doc))
                            {
                                decimal avalableQty = qty + Convert.ToDecimal(_SUM);
                              
                                trow.SetField("tqty", avalableQty);
                            }
                        }
                        grdDocItem.DataSource = _Temp_ItemDetails;
                        grdDocItem.DataBind();
                        ViewState["Temp_ItemDetails"] = _Temp_ItemDetails;
                        ViewState["Temp_AlloItemDetails"] = dtAllocation;
                    }
                }
               
            }
        }
     
       
    }
}