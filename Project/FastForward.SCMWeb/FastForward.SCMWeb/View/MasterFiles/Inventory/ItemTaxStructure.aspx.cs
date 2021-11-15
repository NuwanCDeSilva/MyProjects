using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.MasterFiles.Inventory
{
    public partial class Item_Tax_Structure : BasePage
    {
        List<mst_itm_tax_structure_det> _lstTaxDet = new List<mst_itm_tax_structure_det>();
        mst_itm_tax_structure_hdr _taxHdr = new mst_itm_tax_structure_hdr();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null || Session["UserCompanyCode"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                pageClear();
                BindCombo();
            }
        }

        private void pageClear()
        {
            chklstboxcom.Items.Clear();
            chklstbox.Items.Clear();
            txtFromDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
            txtToDate.Text = "31/Jan/2999";
            txtCompany.Text = "";
            txtTax.Text = "";
            txtStruc.Text = "";
            txtDes.Text = "";
            ddlwStatus.SelectedIndex = -1;
           // ddlTaxCode.SelectedIndex = 0;
            grdTax.DataSource = null;
            grdTax.DataSource = new List<mst_itm_tax_structure_det>();
            grdTax.DataBind();
            _lstTaxDet = new List<mst_itm_tax_structure_det>();
            _taxHdr = new mst_itm_tax_structure_hdr();
            ddlTaxCodeSts.SelectedIndex = 0;
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
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

        #region Modalpopup
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string ID = grdResult.SelectedRow.Cells[1].Text;
            string Des = grdResult.SelectedRow.Cells[2].Text;
            if (lblvalue.Text == "masterTax")
            {
                txtStruc.Text = ID;
                txtDes.Text = Des;
                FillGrid(txtStruc.Text);
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
            if (lblvalue.Text == "Company")
            {
                txtCompany.Text = ID;
                lblvalue.Text = "";
                UserPopoup.Hide();
                return;
            }
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;
            if (lblvalue.Text == "masterTax")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterTax);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchTaxMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Company")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "masterTax")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterTax);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchTaxMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Company")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {

            if (lblvalue.Text == "masterTax")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterTax);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchTaxMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "Company")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }


        }
        #endregion
        private void DispMsg(string msgText, string msgType = "")
        {
            msgText = msgText.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");

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
        }
        private void DisplayMessage(String Msg, Int32 option)
        {
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

        protected void txtStruc_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStruc.Text))
            {
                chklstboxcom.Items.Clear();
                chklstbox.Items.Clear();
                FillGrid(txtStruc.Text);
            }
        }
        private void FillGrid(string _code)
        {
            List<mst_itm_tax_structure_hdr> _lstTaxhdr = new List<mst_itm_tax_structure_hdr>();
            _lstTaxhdr = CHNLSVC.General.GetItemTaxStructureHeader(_code);
            if (_lstTaxhdr != null && _lstTaxhdr.Count > 0)
            {
                txtDes.Text = _lstTaxhdr[0].Ish_des;

                _lstTaxDet = CHNLSVC.General.GetItemTaxStructureWeb(_code);
                if (_lstTaxDet != null && _lstTaxDet.Count > 0)
                {

                  // List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                   // oItemStaus = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
                 //   DataTable _status = CHNLSVC.Inventory.GetItemStatusMaster("ALL", "ALL");
                    DataTable _status = new DataTable();
                     //List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
                    //if (_lstTaxDet != null)
                    //{
                    //    foreach (mst_itm_tax_structure_det itemSer in _lstTaxDet)
                    //    {
                    //        if (oItemStaus != null && oItemStaus.Count > 0)
                    //        {
                    //            itemSer.Its_stus_Des = oItemStaus.Find(x => x.Mis_cd == itemSer.Its_stus).Mis_desc;
                    //        }
                    //    }
                    //}
                     if (_lstTaxDet != null)
                     {
                         foreach (mst_itm_tax_structure_det itemSer in _lstTaxDet)
                         {
                             _status = CHNLSVC.Inventory.GetItemStatusMaster(itemSer.Its_stus, null);
                             foreach (DataRow _row in _status.Rows)
                             {
                                 if (itemSer.Its_stus == _row[0].ToString())
                                 {
                                     itemSer.Its_stus_Des = _row[1].ToString();
                                 }
                             }

                         }
                     }
                    grdTax.DataSource = null;
                    grdTax.DataSource = new List<mst_itm_cust>();
                    grdTax.DataSource = _lstTaxDet;
                    grdTax.DataBind();
                    ViewState["_lstTaxDet"] = _lstTaxDet;
                }
            }
            else
            {
                DisplayMessage("Invalid Tax structure", 2);
                //MessageBox.Show("Invalid Tax structure", "Item Tax Structure", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtStruc.Text = "";
                txtStruc.Focus();
            }
        }
        protected void lbtnsrhStuc_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterTax);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchTaxMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
               // _result.Columns.Remove("Description");
                BindUCtrlDDLData(_result);
                lblvalue.Text = "masterTax";
                UserPopoup.Show();
            }

            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtCompany_TextChanged(object sender, EventArgs e)
        {
            if (txtCompany.Text.Trim() != "")
            {
                //TODO: LOAD COMPANY DESCRIPTION
                MasterCompany com = CHNLSVC.General.GetCompByCode(txtCompany.Text.Trim());
                if (com == null)
                {
                    DisplayMessage("Invalid Company", 2);                  
                    txtCompany.Text = "";
                    txtCompany.Focus();
                    return;
                }

            }
        }

        protected void lbtnSearchreCom_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);

                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Company";
                UserPopoup.Show();
            }

            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing search customer";
                DisplayMessage(_Msg, 4);
            }
        }

        void BindCombo()
        {


          //  DataTable _status = CHNLSVC.Inventory.GetItemStatusMaster("ALL", "ALL");
           // List<MasterItemStatus> oItemStaus = new List<MasterItemStatus>();
            DataTable oItemStaus = CHNLSVC.General.GetItemStatusByCom(Session["UserCompanyCode"].ToString());

            if (oItemStaus != null && oItemStaus.Rows.Count > 0)
            {
                if (oItemStaus.Rows.Count > 0)
                {
                    DataView dv = oItemStaus.DefaultView;
                    dv.Sort = "mis_desc ASC";
                    oItemStaus = dv.ToTable();
                }
                    ddlwStatus.DataSource = oItemStaus;
                    ddlwStatus.DataTextField = "mis_desc";
                    ddlwStatus.DataValueField = "Mis_cd";
                    ddlwStatus.DataBind();

                    ddlwStatus.Items.Insert(0, "--Select--");
                    ddlwStatus.SelectedIndex = -1;
                
                
               


            }
            DataTable _TaxCodes = CHNLSVC.General.GetTaxCode();
            if (_TaxCodes != null && _TaxCodes.Rows.Count > 0)
            {
                while (ddlTaxCodeSts.Items.Count > 1)
                {
                    ddlTaxCodeSts.Items.RemoveAt(1);
                }
                ddlTaxCodeSts.DataSource = _TaxCodes;//mtc_rt
                ddlTaxCodeSts.DataTextField = "mtc_tax_rt_cd";
                ddlTaxCodeSts.DataValueField = "mtc_tax_rt_cd";
                //ddlTaxCodeSts.DataTextField = "mtc_tax_rt_cd";
                //ddlTaxCodeSts.DataValueField = "mtc_tax_cd";
                ddlTaxCodeSts.DataBind();
                ddlTaxCodeSts.SelectedIndex = 0;
            }

        }

        protected void lbtnAddstatus_Click(object sender, EventArgs e)
        {
            _lstTaxDet = ViewState["_lstTaxDet"] as List<mst_itm_tax_structure_det>;
           
            if (!IsNumeric(txtTax.Text.Trim(), NumberStyles.Float))
            {
                DisplayMessage("Please enter valid tax rate", 2);
                txtTax.Focus();
                return;
            }



            if (ddlTaxCodeSts.SelectedIndex == 0)
            {
                DisplayMessage("Please select Tax type", 2);
                return;
            }
            if (string.IsNullOrEmpty(txtTax.Text))
            {
                DisplayMessage("Please enter Tax rate", 2);                        
                return;
            }
            //if ((Session["Multiplestatus"] == "true") && (Session["Multiplecom"] == "true"))
            //{
            //    DisplayMessage("canot user both multiple company and multiple status", 2);
            //    return;
            //}
            if ((Session["Multiplecom"] == "true") && (Session["Multiplestatus"] == "true"))
            {
                #region multiple company
                foreach (ListItem Item in chklstboxcom.Items)
                {
                    if (Item.Selected == true)
                    {

                        if (_lstTaxDet != null)
                        {
                            foreach (ListItem Item_st in chklstbox.Items)
                            {
                                if (Item_st.Selected == true)
                                {
                                    string Status = string.Empty; ;
                                    DataTable _status = CHNLSVC.Inventory.GetItemStatusMaster("ALL", "ALL");
                                    if ((_status != null) && (_status.Rows.Count > 0))
                                    {
                                        DataRow[] drs = _status.Select("MIS_DESC='" + Item_st.Text + "'");
                                        Status = drs[0][0].ToString();
                                    }
                                    var result = _lstTaxDet.SingleOrDefault(x => x.Its_com == Item.Text && x.Its_stus == Status &&
                                        x.Its_tax_cd == GetTaxData(ddlTaxCodeSts.SelectedValue).Mtc_tax_cd);
                                    if (result != null)
                                    {
                                        result.Its_act = (check_active.Checked == true) ? true : false;
                                        result.Its_valid_to = Convert.ToDateTime(txtToDate.Text);
                                        result.Its_tax_rate = Convert.ToDecimal(txtTax.Text);
                                        DisplayMessage("This Tax definition already available", 2);

                                    }
                                    else
                                    {


                                        mst_itm_tax_structure_det _itm = new mst_itm_tax_structure_det();
                                        _itm.Its_com = Item.Text;
                                        _itm.Its_stus = Status;
                                        _itm.Its_stus_Des = Item_st.Text;
                                        _itm.Its_tax_cd = GetTaxData(ddlTaxCodeSts.SelectedValue).Mtc_tax_cd;
                                        _itm.Its_tax_rate = Convert.ToDecimal(txtTax.Text);
                                        _itm.Its_taxrate_cd = GetTaxData(ddlTaxCodeSts.SelectedValue).Mtc_tax_rt_cd;
                                        // _itm.Its_act = true;
                                        _itm.Its_act = (check_active.Checked == true) ? true : false;
                                        _itm.Its_valid_from = Convert.ToDateTime(txtFromDate.Text);
                                        _itm.Its_valid_to = Convert.ToDateTime(txtToDate.Text);
                                        _lstTaxDet.Add(_itm);


                                    }
                                }
                            }
                        }
                        else
                        {
                            _lstTaxDet = new List<mst_itm_tax_structure_det>();
                            foreach (ListItem Item_st in chklstbox.Items)
                            {
                                if (Item_st.Selected == true)
                                {
                                    string Status = string.Empty; ;
                                    DataTable _status = CHNLSVC.Inventory.GetItemStatusMaster("ALL", "ALL");
                                    if ((_status != null) && (_status.Rows.Count > 0))
                                    {
                                        DataRow[] drs = _status.Select("MIS_DESC='" + Item_st.Text + "'");
                                        Status = drs[0][0].ToString();
                                    }

                                    mst_itm_tax_structure_det _itm = new mst_itm_tax_structure_det();
                                    _itm.Its_com = Item.Text;
                                    _itm.Its_stus = Status;
                                    _itm.Its_stus_Des = Item_st.Text;
                                    _itm.Its_tax_cd = GetTaxData(ddlTaxCodeSts.SelectedValue).Mtc_tax_cd;
                                    _itm.Its_tax_rate = Convert.ToDecimal(txtTax.Text);
                                    _itm.Its_taxrate_cd = GetTaxData(ddlTaxCodeSts.SelectedValue).Mtc_tax_rt_cd;

                                    // _itm.Its_act = true;
                                    _itm.Its_act = (check_active.Checked == true) ? true : false;
                                    _itm.Its_valid_from = Convert.ToDateTime(txtFromDate.Text);
                                    _itm.Its_valid_to = Convert.ToDateTime(txtToDate.Text);
                                    _lstTaxDet.Add(_itm);

                                }

                            }

                        }

                    }

                }
                #endregion
                ViewState["_lstTaxDet"] = _lstTaxDet;
                grdTax.DataSource = null;
                grdTax.DataSource = new List<mst_itm_tax_structure_det>();
                grdTax.DataSource = _lstTaxDet;
                grdTax.DataBind();
                txtCompany.Text = "";
                ddlwStatus.SelectedIndex = -1;
                ddlwStatus.SelectedIndex = -1;
                txtTax.Text = "";
                txtCompany.Focus();
                return;
            }
            else if (Session["Multiplestatus"] == "true")
            {
                #region multiple status
                foreach (ListItem Item in chklstbox.Items)
                {
                    if (Item.Selected == true)
                    {
                        string Status = string.Empty; ;
                        DataTable _status = CHNLSVC.Inventory.GetItemStatusMaster("ALL", "ALL");
                        if ((_status != null) && (_status.Rows.Count > 0))
                        {
                            DataRow[] drs = _status.Select("MIS_DESC='" + Item.Text + "'");
                            Status = drs[0][0].ToString();
                        }
                        if (_lstTaxDet != null)
                        {

                            var result = _lstTaxDet.SingleOrDefault(x => x.Its_com == txtCompany.Text && x.Its_stus == Status &&
                                x.Its_tax_cd == GetTaxData(ddlTaxCodeSts.SelectedValue).Mtc_tax_cd);
                            if (result != null)
                            {
                                result.Its_act = (check_active.Checked == true) ? true : false;
                                result.Its_valid_to = Convert.ToDateTime(txtToDate.Text);
                                result.Its_tax_rate = Convert.ToDecimal(txtTax.Text);
                                DisplayMessage("This Tax definition already available",2);

                            }
                            else
                            {
                                

                                mst_itm_tax_structure_det _itm = new mst_itm_tax_structure_det();
                                _itm.Its_com = txtCompany.Text;
                                _itm.Its_stus = Status;
                                _itm.Its_stus_Des = Item.Text;
                                _itm.Its_tax_cd = GetTaxData(ddlTaxCodeSts.SelectedValue).Mtc_tax_cd;
                                _itm.Its_tax_rate = Convert.ToDecimal(txtTax.Text);
                                _itm.Its_taxrate_cd = GetTaxData(ddlTaxCodeSts.SelectedValue).Mtc_tax_rt_cd;
                                // _itm.Its_act = true;
                                _itm.Its_act = (check_active.Checked == true) ? true : false;
                                _itm.Its_valid_from = Convert.ToDateTime(txtFromDate.Text);
                                _itm.Its_valid_to = Convert.ToDateTime(txtToDate.Text);
                                _lstTaxDet.Add(_itm);


                            }
                        }
                        else
                        {
                            _lstTaxDet = new List<mst_itm_tax_structure_det>();

                            mst_itm_tax_structure_det _itm = new mst_itm_tax_structure_det();
                            _itm.Its_com = txtCompany.Text;
                            _itm.Its_stus = Status;
                            _itm.Its_stus_Des = Item.Text;
                            _itm.Its_tax_cd = GetTaxData(ddlTaxCodeSts.SelectedValue).Mtc_tax_cd;
                            _itm.Its_tax_rate = Convert.ToDecimal(txtTax.Text);
                            _itm.Its_taxrate_cd = GetTaxData(ddlTaxCodeSts.SelectedValue).Mtc_tax_rt_cd;
                            // _itm.Its_act = true;
                            _itm.Its_act = (check_active.Checked == true) ? true : false;
                            _itm.Its_valid_from = Convert.ToDateTime(txtFromDate.Text);
                            _itm.Its_valid_to = Convert.ToDateTime(txtToDate.Text);
                            _lstTaxDet.Add(_itm);

                            
                        }

                    }

                }
                #endregion
                ViewState["_lstTaxDet"] = _lstTaxDet;
                grdTax.DataSource = null;
                grdTax.DataSource = new List<mst_itm_tax_structure_det>();
                grdTax.DataSource = _lstTaxDet;
                grdTax.DataBind();
                txtCompany.Text = "";
                ddlwStatus.SelectedIndex = -1;
                ddlwStatus.SelectedIndex = -1;
                txtTax.Text = "";
                txtCompany.Focus();
                return;
            }
            else if (Session["Multiplecom"] == "true")
            {
                #region multiple company
                foreach (ListItem Item in chklstboxcom.Items)
                {
                    if (Item.Selected == true)
                    {

                        if (_lstTaxDet != null)
                        {

                            var result = _lstTaxDet.SingleOrDefault(x => x.Its_com == Item.Text && x.Its_stus == ddlwStatus.SelectedValue &&
                                x.Its_tax_cd == GetTaxData(ddlTaxCodeSts.SelectedValue).Mtc_tax_cd);
                            if (result != null)
                            {
                                result.Its_act = (check_active.Checked == true) ? true : false;
                                result.Its_valid_to = Convert.ToDateTime(txtToDate.Text);
                                result.Its_tax_rate = Convert.ToDecimal(txtTax.Text);
                                DisplayMessage("This Tax definition already available", 2);

                            }
                            else
                            {


                                mst_itm_tax_structure_det _itm = new mst_itm_tax_structure_det();
                                _itm.Its_com = Item.Text;
                                _itm.Its_stus = ddlwStatus.SelectedValue;
                                _itm.Its_stus_Des = ddlwStatus.Text;
                                _itm.Its_tax_cd = GetTaxData(ddlTaxCodeSts.SelectedValue).Mtc_tax_cd;
                                _itm.Its_tax_rate = Convert.ToDecimal(txtTax.Text);
                                _itm.Its_taxrate_cd = GetTaxData(ddlTaxCodeSts.SelectedValue).Mtc_tax_rt_cd;
                                // _itm.Its_act = true;
                                _itm.Its_act = (check_active.Checked == true) ? true : false;
                                _itm.Its_valid_from = Convert.ToDateTime(txtFromDate.Text);
                                _itm.Its_valid_to = Convert.ToDateTime(txtToDate.Text);
                                _lstTaxDet.Add(_itm);


                            }
                        }
                        else
                        {
                            _lstTaxDet = new List<mst_itm_tax_structure_det>();

                            mst_itm_tax_structure_det _itm = new mst_itm_tax_structure_det();
                            _itm.Its_com = Item.Text;
                            _itm.Its_stus = ddlwStatus.SelectedValue;
                            _itm.Its_stus_Des = ddlwStatus.Text;
                            _itm.Its_tax_cd = GetTaxData(ddlTaxCodeSts.SelectedValue).Mtc_tax_cd;
                            _itm.Its_tax_rate = Convert.ToDecimal(txtTax.Text);
                            _itm.Its_taxrate_cd = GetTaxData(ddlTaxCodeSts.SelectedValue).Mtc_tax_rt_cd;
                            // _itm.Its_act = true;
                            _itm.Its_act = (check_active.Checked == true) ? true : false;
                            _itm.Its_valid_from = Convert.ToDateTime(txtFromDate.Text);
                            _itm.Its_valid_to = Convert.ToDateTime(txtToDate.Text);
                            _lstTaxDet.Add(_itm);


                        }

                    }

                }
                #endregion
                ViewState["_lstTaxDet"] = _lstTaxDet;
                grdTax.DataSource = null;
                grdTax.DataSource = new List<mst_itm_tax_structure_det>();
                grdTax.DataSource = _lstTaxDet;
                grdTax.DataBind();
                txtCompany.Text = "";
                ddlwStatus.SelectedIndex = -1;
                ddlwStatus.SelectedIndex = -1;
                txtTax.Text = "";
                txtCompany.Focus();
                return;
            }      
            else
            {
                if (string.IsNullOrEmpty(txtCompany.Text))
                {
                    DisplayMessage("Please enter company", 2);
                    return;
                }
                if (ddlwStatus.SelectedIndex == 0)
                {
                    DisplayMessage("Please select item status", 2);
                    return;
                }
                if (_lstTaxDet != null)
                {
                    var result = _lstTaxDet.SingleOrDefault(x => x.Its_com == txtCompany.Text && x.Its_stus == ddlwStatus.SelectedValue &&
                        x.Its_tax_cd == GetTaxData(ddlTaxCodeSts.SelectedValue).Mtc_tax_cd);
                    if (result != null)
                    {
                        result.Its_act = (check_active.Checked == true) ? true : false;
                        result.Its_valid_to = Convert.ToDateTime(txtToDate.Text);
                        result.Its_tax_rate = Convert.ToDecimal(txtTax.Text);
                        DisplayMessage("This Tax definition already available", 2);

                        grdTax.DataSource = _lstTaxDet;
                        grdTax.DataBind();
                        return;
                    }
                }
                else
                {
                    _lstTaxDet = new List<mst_itm_tax_structure_det>();
                }
                mst_itm_tax_structure_det _itm = new mst_itm_tax_structure_det();
                _itm.Its_com = txtCompany.Text;
                _itm.Its_stus = ddlwStatus.SelectedValue.ToString();
                _itm.Its_stus_Des = ddlwStatus.Text;
                _itm.Its_tax_cd = GetTaxData(ddlTaxCodeSts.SelectedValue).Mtc_tax_cd;
                _itm.Its_tax_rate = Convert.ToDecimal(txtTax.Text);
                _itm.Its_taxrate_cd = GetTaxData(ddlTaxCodeSts.SelectedValue).Mtc_tax_rt_cd;

                // _itm.Its_act = true;
                _itm.Its_act = (check_active.Checked == true) ? true : false;
                _itm.Its_valid_from = Convert.ToDateTime(txtFromDate.Text);
                _itm.Its_valid_to = Convert.ToDateTime(txtToDate.Text);
                _lstTaxDet.Add(_itm);

                grdTax.DataSource = null;
                grdTax.DataSource = new List<mst_itm_tax_structure_det>();
                grdTax.DataSource = _lstTaxDet;
                grdTax.DataBind();

                ViewState["_lstTaxDet"] = _lstTaxDet;
                txtCompany.Text = "";
                ddlwStatus.SelectedIndex = -1;
                ddlwStatus.SelectedIndex = -1;
                txtTax.Text = "";
                txtCompany.Focus();
            }

        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            _lstTaxDet = ViewState["_lstTaxDet"] as List<mst_itm_tax_structure_det>;
            if (!string.IsNullOrEmpty(txtStruc.Text))
            {
                List<mst_itm_tax_structure_hdr> _lstTaxhdr = new List<mst_itm_tax_structure_hdr>();
                _lstTaxhdr = CHNLSVC.General.GetItemTaxStructureHeader(txtStruc.Text);
                if (_lstTaxhdr != null && _lstTaxhdr.Count > 0)
                {
                    _taxHdr.Ish_stuc_seq = _lstTaxhdr[0].Ish_stuc_seq;

                }
            }
            if (_lstTaxDet == null)
            {
                _lstTaxDet = new List<mst_itm_tax_structure_det>();
            }


            if (string.IsNullOrEmpty(txtDes.Text))
            {
                DisplayMessage("Please enter description", 2);     
                return;
            }

            if ((_lstTaxDet.Count == 0))
            {
                DisplayMessage("Please enter tax details", 2);
                return;
            }
           
           
           

            _taxHdr.Ish_com = Session["UserCompanyCode"].ToString();
            _taxHdr.Ish_cre_by = Session["UserID"].ToString();
            _taxHdr.Ish_date = Convert.ToDateTime(DateTime.Today).Date;
            _taxHdr.Ish_mod_by = Session["UserID"].ToString();
            _taxHdr.Ish_stuc_code = txtStruc.Text;
            _taxHdr.Ish_act = 1;
            _taxHdr.Ish_des = txtDes.Text;
    

            MasterAutoNumber _taxAuto = new MasterAutoNumber();
            #region Auto Number
            _taxAuto.Aut_cate_cd = Session["UserCompanyCode"].ToString();
            _taxAuto.Aut_cate_tp = "LOC";
            _taxAuto.Aut_moduleid = "TAX";
            _taxAuto.Aut_direction = 0;
            _taxAuto.Aut_start_char = "TAX";
            _taxAuto.Aut_year = DateTime.Today.Year;
            #endregion
            string _err = string.Empty;

            int row_aff = CHNLSVC.General.SaveItemTaxStructure(_taxHdr, _lstTaxDet, _taxAuto, out _err);
            if (row_aff == 1)
            {
                DisplayMessage(_err, 3);
                pageClear();
            }
            else
            {
                DispMsg(_err, "E");
            }
        }

        protected void lbtnClear2_Click(object sender, EventArgs e)
        {
            //pageClear();
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
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            bool _IsDelete = false;
            _lstTaxDet = ViewState["_lstTaxDet"] as List<mst_itm_tax_structure_det>;
            if (txtDeleteconformmessageValue.Value == "No")
            {
                return;
            }
            foreach (GridViewRow _row in grdTax.Rows)
            {
                CheckBox checkbox = _row.FindControl("chk_ReqItem") as CheckBox;
                if (checkbox.Checked == true)
                {
                    string _com = (_row.FindControl("colmCode") as Label).Text;
                    string _stataus = (_row.FindControl("colmDes") as Label).Text;
                    string _taxcode = (_row.FindControl("colTaxtype") as Label).Text;

                    _lstTaxDet.RemoveAll(x => x.Its_com == _com && x.Its_stus == _stataus && x.Its_tax_cd == _taxcode);
                    grdTax.AutoGenerateColumns = false;
                    grdTax.DataSource = new List<mst_itm_tax_structure_det>();
                    grdTax.DataSource = _lstTaxDet;
                    grdTax.DataBind();
                    ViewState["_lstTaxDet"] = _lstTaxDet;
                    _IsDelete = true;
                }
            }
            if (_IsDelete == false)
            {
                DisplayMessage("Please select the company for delete", 2);
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
          
            Label1.Visible = false;
            if (fileupexcelupload.HasFile)
            {
                string FileName = Path.GetFileName(fileupexcelupload.PostedFile.FileName);
                string Extension = Path.GetExtension(fileupexcelupload.PostedFile.FileName);

                if (Extension != ".xls" && Extension != ".XLS" && Extension != ".xlsx" && Extension != ".XLSX")
                {

                    Label1.Visible = true;
                    Label1.Text = "Please select a valid excel (.xls) file";
                    excelUpload.Show();
                    return;
                }
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                //LoadData(FolderPath + FileName);
                string FilePath = Server.MapPath(FolderPath + FileName);
                fileupexcelupload.SaveAs(FilePath);
                Session["FilePath"] = FilePath;
                divUpcompleted.Visible = true;
                DisplayMessage("Excel file upload completed. Do you want to process?", 2);
                excelUpload.Show();
                //Import_To_Grid(FilePath, Extension);
            }
            else
            {
                Label1.Visible = true;
                Label1.Text = "Please select the correct upload file path";
                DisplayMessage("Please select the correct upload file path", 2);
                excelUpload.Show();
                // divalert.Visible = true;
                // lblalert.Text = "Please select an excel file";
            }
        }
        protected void btnprocess_Click(object sender, EventArgs e)
        {
            DataTable[] GetExecelTbl = LoadData(Session["FilePath"].ToString());
            if (GetExecelTbl != null)
            {
                if (GetExecelTbl[0].Rows.Count > 0)
                {
                   // _lstTaxDet = ViewState["_lstTaxDet"] as List<mst_itm_tax_structure_det>;
                    for (int i = 1; i < GetExecelTbl[0].Rows.Count; i++)
                    {
                        try
                        {
                            mst_itm_tax_structure_det _itm = new mst_itm_tax_structure_det();
                            _itm.Its_com = GetExecelTbl[0].Rows[i][0].ToString();
                            _itm.Its_stus = GetExecelTbl[0].Rows[i][1].ToString();
                            _itm.Its_tax_cd = GetExecelTbl[0].Rows[i][2].ToString();
                            _itm.Its_tax_rate = Convert.ToDecimal(GetExecelTbl[0].Rows[i][3].ToString());                           
                            // _itm.Its_act = true;
                            _itm.Its_act =  true ;
                            _itm.Its_valid_from = Convert.ToDateTime( GetExecelTbl[0].Rows[i][4].ToString());
                            _itm.Its_valid_to = Convert.ToDateTime( GetExecelTbl[0].Rows[i][5].ToString());
                            _lstTaxDet.Add(_itm);
                            
                        }
                        catch (Exception ex)
                        {
                            DisplayMessage("Excel  Data Invalid Please check Excel File and Upload ", 2);
                            Label1.Visible = true;
                            Label1.Text = "Excel Data Invalid Please check Excel File and Upload";
                            excelUpload.Show();
                        }

                    }
                   
                    grdTax.DataSource = null;
                    grdTax.DataSource = new List<mst_itm_tax_structure_det>();
                    grdTax.DataSource = _lstTaxDet;
                    grdTax.DataBind();
                    ViewState["_lstTaxDet"] = _lstTaxDet;

                }
            }
        }
        protected void lbtnExcelUpload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDes.Text))
            {
                DisplayMessage("Please Enter Description", 2);
                return;
            }
            divUpcompleted.Visible = false;
            Label1.Visible = false;
            excelUpload.Show();
        }
        public string ConnectionString(string FileName, string Header)
        {
            OleDbConnectionStringBuilder Builder = new OleDbConnectionStringBuilder();
            if (System.IO.Path.GetExtension(FileName).ToUpper() == ".XLS")
            {
                Builder.Provider = "Microsoft.Jet.OLEDB.4.0";
                Builder.Add("Extended Properties", string.Format("Excel 8.0;IMEX=1;HDR={0};", Header));
            }
            else
            {
                Builder.Provider = "Microsoft.ACE.OLEDB.12.0";
                Builder.Add("Extended Properties", string.Format("Excel 12.0;IMEX=1;HDR={0};", Header));
            }

            Builder.DataSource = FileName;

            return Builder.ConnectionString;
        }
        public DataTable[] LoadData(string FileName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DataTable Tax = new DataTable();

            using (OleDbConnection cn = new OleDbConnection { ConnectionString = ConnectionString(FileName, "No") })
            {
                cn.Open();
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dtExcelSchema;
                cmdExcel.Connection = cn;

                dtExcelSchema = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                cn.Close();

                //Read Data from First Sheet
                cn.Open();
                cmdExcel.CommandText = "SELECT F1,F2,F3,F4,F5,F6 From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(Tax);


                return new DataTable[] { Tax };
            }
        }

        protected void lbtnMultiplestatus_Click(object sender, EventArgs e)
        {
           // chklstbox.Items.Clear();
            if (chklstbox.Items.Count < 1)
            {
                DataTable _status = CHNLSVC.Inventory.GetItemStatusMaster("ALL", "ALL");
                foreach (DataRow drow in _status.Rows)
                {
                    Session["Multiplestatus"] = "true";
                    chklstbox.Items.Add(drow["MIS_DESC"].ToString());

                }
            }
            Multiplestatus.Show();
            
            
        }
        protected void lbtnMultiplecompany_Click(object sender, EventArgs e)
        {
            if (chklstboxcom.Items.Count < 1)
            {
                //chklstboxcom.Items.Clear();
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                foreach (DataRow drow in _result.Rows)
                {
                    Session["Multiplecom"] = "true";
                    chklstboxcom.Items.Add(drow["Code"].ToString());

                }
            }
            Multiplecompany.Show();
        }

        protected void colmStataus_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (sender as CheckBox).NamingContainer as GridViewRow;
                Label colmCode = (Label)row.FindControl("colmCode");
                Label colmDes = (Label)row.FindControl("colmDes");
                Label colTaxtype = (Label)row.FindControl("colTaxtype");
                CheckBox colmStataus = (CheckBox)row.FindControl("colmStataus");
                if (ViewState["_lstTaxDet"] != null)
                {
                    List<mst_itm_tax_structure_det> _taxList = (List<mst_itm_tax_structure_det>)ViewState["_lstTaxDet"];
                    if (_taxList != null)
                    {
                        var v = _taxList.Where(c => c.Its_com == colmCode.Text && c.Its_stus == colmDes.Text && c.Its_tax_cd == colTaxtype.Text).FirstOrDefault();
                        if (v != null)
                        {
                            v.Its_act = colmStataus.Checked;
                        }
                    }
                    ViewState["_lstTaxDet"] = _taxList;
                    grdTax.DataSource = _taxList;
                    grdTax.DataBind();
                }

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message,4);
            }
        }

        protected void ddlTaxCodeSts_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlTaxCodeSts.SelectedIndex > 0)
                {
                    MST_TAX_CD _txData = GetTaxData(ddlTaxCodeSts.SelectedValue);
                    if (_txData!=null)
                    {
                        txtTax.Text = _txData.Mtc_rt.ToString("N2");
                    }

                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }
        private MST_TAX_CD GetTaxData(string _taxCd)
        {
            MST_TAX_CD _taxData = new MST_TAX_CD();
            _taxData = CHNLSVC.General.GET_MST_TAX_CD_DATA(_taxCd);
            return _taxData;
        }
    }
}