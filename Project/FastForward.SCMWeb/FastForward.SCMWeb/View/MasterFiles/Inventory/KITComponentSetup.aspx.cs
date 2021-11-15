using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
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
    public partial class KITComponentSetup : Base
    {
        protected List<MasterKitComFineItem> _ComFineItem { get { return (List<MasterKitComFineItem>)Session["_ComFineItem"]; } set { Session["_ComFineItem"] = value; } }
        protected List<mst_itm_fg_cost> _mstcostlist { get { return (List<mst_itm_fg_cost>)Session["_mstcostlist"]; } set { Session["_mstcostlist"] = value; } }
        protected List<ItemKitComponent> _ItemKitComponent { get { return (List<ItemKitComponent>)Session["_ItemKitComponent"]; } set { Session["_ItemKitComponent"] = value; } }
        protected MasterItem _itemdetail { get { return (MasterItem)Session["_itemdetail"]; } set { Session["_itemdetail"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageClear();
            }
        }
        private void PageClear()
        {
            txtkititemcode.Text = string.Empty;
            _mstcostlist = new List<mst_itm_fg_cost>();
            _ItemKitComponent = new List<ItemKitComponent>();
            _itemdetail = new MasterItem();
            _ComFineItem = new List<MasterKitComFineItem>();
            //ADDED BY DULAJ 2018/Mar/02
            grdKitcomp.DataSource = null;
            grdKitcomp.DataBind();
            lblkitDes.Text = string.Empty;
            kitdetadddes.Text = string.Empty;
            lblItemDescription.Text = string.Empty;
            lblItemModel.Text = string.Empty;
            lblItemBrand.Text = string.Empty;
            txtitm.Text = string.Empty;
            txtnounit.Text = string.Empty;
            txtcost.Text = string.Empty;
            txtseq.Text = string.Empty;
            txtseq.Attributes.Add("readonly", "readonly");
            txtfitem.Text = string.Empty;
            txtfcost.Text = string.Empty;
            txtkitcode.Text = string.Empty;
            grdfitem.DataSource = null;
            grdfitem.DataBind();
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.InvoiceWithDate:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "C" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceType:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "PRO" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item_2:
                    {
                        paramsText.Append(txtkititemcode.Text + seperator);
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

            if (lblvalue.Text == "kit")
            {
                txtkititemcode.Text = grdResult.SelectedRow.Cells[1].Text;
                lblkitDes.Text = grdResult.SelectedRow.Cells[2].Text;
                GetComItem(txtkititemcode.Text);
                GetFineshitem(txtkititemcode.Text);
            }
            if (lblvalue.Text == "kitcode")
            {
                txtkitcode.Text = grdResult.SelectedRow.Cells[1].Text;
                kitdetadddes.Text = grdResult.SelectedRow.Cells[2].Text;
                //lblkitDes.Text = grdResult.SelectedRow.Cells[2].Text;
                //GetComItem(txtkitcode.Text);
                //  GetFineshitem(txtkititemcode.Text);
            }
            if (lblvalue.Text == "Item")
            {
                txtitm.Text = grdResult.SelectedRow.Cells[1].Text;
                LoadItemDetail(txtitm.Text);
            }
            if (lblvalue.Text == "Item2")
            {
                txtfitem.Text = grdResult.SelectedRow.Cells[1].Text;
            }
            lblvalue.Text = "";
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;
            try
            {
                grdResult.PageIndex = e.NewPageIndex;
                grdResult.DataSource = null;
                grdResult.DataSource = (DataTable)ViewState["SEARCH"];
                grdResult.DataBind();
                UserPopoup.Show();
                Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
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
            if (lblvalue.Text == "kit")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                DataTable result = CHNLSVC.CommonSearch.GetKitItemforInvoiceSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, "%" + txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "kitcode")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                DataTable result = CHNLSVC.CommonSearch.GetKitItemforInvoiceSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, "%" + txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
                return;
            }
            if ((lblvalue.Text == "Item") || (lblvalue.Text == "Item2"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result2 = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result2;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                ViewState["SEARCH"] = result2;
                UserPopoup.Show();
                return;
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
            }
        }
        protected void txtkititemcode_TextChanged(object sender, EventArgs e)
        {
            txtkititemcode.Text = txtkititemcode.Text.Trim().ToUpper();
            if (!string.IsNullOrEmpty(txtkititemcode.Text))
            {
                _itemdetail = CHNLSVC.General.GetItemMaster(txtkititemcode.Text.Trim().ToUpper());
                if (_itemdetail != null && _itemdetail.Mi_cd != null)
                {
                    if (_itemdetail.Mi_act == false)
                    {
                        DisplayMessage("KIT item code Inactive", 2);
                        txtkititemcode.Text = string.Empty;
                        return;
                    }
                    MasterCompanyItem _comitm = new MasterCompanyItem();
                    _comitm = CHNLSVC.Sales.GetAllCompanyItems(Session["UserCompanyCode"].ToString(), txtkititemcode.Text.Trim().ToUpper(), 1);
                    if (_comitm.Mci_itm_cd == null)
                    {
                        DisplayMessage("company not assign this item", 2);
                        txtkititemcode.Text = string.Empty;
                        lblkitDes.Text = string.Empty;
                    }
                    if (_itemdetail.Mi_itm_tp == "K")
                    {
                        lblkitDes.Text = _itemdetail.Mi_longdesc;
                        GetComItem(txtkititemcode.Text);
                        GetFineshitem(txtkititemcode.Text);
                    }
                    else
                    {
                        DisplayMessage("Please enter a valid kIT item code", 2);
                        txtkititemcode.Text = string.Empty;
                    }

                }
                else
                {
                    DisplayMessage("Please enter a valid kIT item code", 2);
                    txtkititemcode.Text = string.Empty;
                }

            }
        }

        //Dulaj 2018/Nov/06
        protected void txtkitcode_TextChanged(object sender, EventArgs e)
        {
            txtkitcode.Text = txtkitcode.Text.Trim().ToUpper();
            if (!string.IsNullOrEmpty(txtkitcode.Text))
            {
                _itemdetail = CHNLSVC.General.GetItemMaster(txtkitcode.Text.Trim().ToUpper());
                if (_itemdetail != null && _itemdetail.Mi_cd != null)
                {
                    if (_itemdetail.Mi_act == false)
                    {
                        DisplayMessage("KIT item code Inactive", 2);
                        txtkitcode.Text = string.Empty;
                        return;
                    }
                    MasterCompanyItem _comitm = new MasterCompanyItem();
                    _comitm = CHNLSVC.Sales.GetAllCompanyItems(Session["UserCompanyCode"].ToString(), txtkitcode.Text.Trim().ToUpper(), 1);
                    if (_comitm.Mci_itm_cd == null)
                    {
                        DisplayMessage("company not assign this item", 2);
                        txtkitcode.Text = string.Empty;
                        //lblkitDes.Text = string.Empty;
                        kitdetadddes.Text = string.Empty;
                    }
                    if (_itemdetail.Mi_itm_tp == "K")
                    {
                        kitdetadddes.Text = _itemdetail.Mi_longdesc;
                      //  GetComItem(txtkitcode.Text);
                       // GetFineshitem(txtkitcode.Text);
                    }
                    else
                    {
                        DisplayMessage("Please enter a valid kIT item code", 2);
                        txtkitcode.Text = string.Empty;
                    }

                }
                else
                {
                    DisplayMessage("Please enter a valid kIT item code", 2);
                    txtkitcode.Text = string.Empty;
                }

            }
        }

        protected void lbtnkititemcode_Click(object sender, EventArgs e)
        {
            try
            {


                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable result = CHNLSVC.CommonSearch.GetKitItemforInvoiceSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "kitcode";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }


        protected void lbtnkititem_Click(object sender, EventArgs e)
        {
            try
            {


                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceWithDate);
                DataTable result = CHNLSVC.CommonSearch.GetKitItemforInvoiceSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "kit";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        private void GetComItem(string _kitcode)
        {
            ItemKitComponent _kit = new ItemKitComponent();
            _kit.MIKC_ITM_CODE_MAIN = _kitcode;
            _ItemKitComponent = CHNLSVC.Inventory.GetItemKitComponent_ProductPlane(_kit, Session["UserDefLoca"].ToString());
            if (_ItemKitComponent.Count > 0)
            {
                foreach (ItemKitComponent _com in _ItemKitComponent)
                {
                    if (_com.MIKC_ITM_TYPE == "M") { _com.TYPE = "MAIN"; } else { _com.TYPE = "COM"; }
                    if (_com.MIKC_ACTIVE == 1) { _com.ACTIVEDES = "ACTIVE"; } else { _com.ACTIVEDES = "INACTIVE"; }
                    if (_com.MIKC_CHG_MAIN_SERIAL == 1) { _com.CHANGITEM = "YES"; } else { _com.CHANGITEM = "NO"; }
                    if (_com.MIKC_ISSCAN == 1) { _com.ISSCAN = "YES"; } else { _com.ISSCAN = "NO"; }
                }
            }
            grdKitcomp.DataSource = _ItemKitComponent;
            grdKitcomp.DataBind();

        }
        private List<ItemKitComponent> GetComItemByKitCd(string _kitcode)
        {
            ItemKitComponent _kit = new ItemKitComponent();
            List<ItemKitComponent> _ItemKitComponent1 = new List<ItemKitComponent>();
            _kit.MIKC_ITM_CODE_MAIN = _kitcode;
            _ItemKitComponent1 = CHNLSVC.Inventory.GetItemKitComponent_ProductPlane(_kit, Session["UserDefLoca"].ToString());
            if (_ItemKitComponent.Count > 0)
            {
                foreach (ItemKitComponent _com in _ItemKitComponent)
                {
                    if (_com.MIKC_ITM_TYPE == "M") { _com.TYPE = "MAIN"; } else { _com.TYPE = "COM"; }
                    if (_com.MIKC_ACTIVE == 1) { _com.ACTIVEDES = "ACTIVE"; } else { _com.ACTIVEDES = "INACTIVE"; }
                    if (_com.MIKC_CHG_MAIN_SERIAL == 1) { _com.CHANGITEM = "YES"; } else { _com.CHANGITEM = "NO"; }
                    if (_com.MIKC_ISSCAN == 1) { _com.ISSCAN = "YES"; } else { _com.ISSCAN = "NO"; }
                }
            }
            return _ItemKitComponent1;
        }

        protected void btnSearch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                txtSearchbyword.Text = "";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Item";
                BindUCtrlDDLData(result);
                txtSearchbyword.Focus();
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }
        private bool LoadItemDetail(string _item)
        {
            lblItemDescription.Text = string.Empty;
            lblItemModel.Text = string.Empty;
            lblItemBrand.Text = string.Empty;
            _itemdetail = new MasterItem();

            bool _isValid = false;

            if (!string.IsNullOrEmpty(_item))
            {
                _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
            }
            if (_itemdetail != null && !string.IsNullOrEmpty(_itemdetail.Mi_cd))
            {
                _isValid = true;
                string _description = _itemdetail.Mi_longdesc;
                string _model = _itemdetail.Mi_model;
                string _brand = _itemdetail.Mi_brand;
                string _serialstatus = _itemdetail.Mi_is_ser1 == 1 ? "Available" : "Non";

                lblItemDescription.Text = _description;
                lblItemModel.Text = _model;
                lblItemBrand.Text = _brand;
                Session["_itemSerializedStatus"] = _itemdetail.Mi_is_ser1;
            }
            else _isValid = false;
            return _isValid;
        }
        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!LoadItemDetail(txtitm.Text.ToUpper()))
                {
                    DisplayMessage("Please check the item code", 1);
                    txtitm.Text = "";
                    txtitm.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }

        }

        protected void lbtnAddItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtitm.Text))
                {
                    DisplayMessage("Please enter item code", 1);
                    txtitm.Focus();
                    return;
                }
                //Added By Dulaj 2018/Feb/28
                else
                {
                    Int32 value;
                    if (!Int32.TryParse(txtnounit.Text, out value))
                    {
                        int checkDecimal = CHNLSVC.Inventory.CheckItemUnitsIsDecimal(txtitm.Text);
                        if (checkDecimal != 1)
                        {
                            DisplayMessage("Please enter valid no of units", 4);
                            return;
                        }

                    }
                    ///////////////////

                }
                if (string.IsNullOrEmpty(txtnounit.Text))
                {
                    DisplayMessage("Please enter no of unit", 1);
                    txtnounit.Focus();
                    txtnounit.Text = "0";
                    return;
                }
                if (string.IsNullOrEmpty(txtcost.Text))
                {
                    DisplayMessage("Please enter unit cost", 1);
                    txtcost.Focus();
                    txtcost.Text = "0";
                    return;
                }
                //if (string.IsNullOrEmpty(txtseq.Text))
                //{
                //    DisplayMessage("Please enter seq", 1);
                //    txtcost.Focus();
                //    txtcost.Text = "0";
                //    return;
                //}
                if (string.IsNullOrEmpty(txtscanseq.Text))
                {
                    DisplayMessage("Please enter scan seq", 1);
                    txtcost.Focus();
                    txtcost.Text = "0";
                    return;
                }
                decimal noofunit = Convert.ToDecimal(txtnounit.Text);
                decimal cost = Convert.ToDecimal(txtcost.Text);
                // int seq = Convert.ToInt32(txtseq.Text);
                int scseq = Convert.ToInt32(txtscanseq.Text);

                if (noofunit <= 0)
                {
                    DisplayMessage("Cannot add zero or minus qty", 1);
                    txtnounit.Focus();
                    return;
                }
                if (cost < 0)
                {
                    DisplayMessage("Cannot add  minus cost", 1);
                    txtcost.Focus();
                    return;
                }
                //if ((seq < 0) || (scseq < 0))
                //{
                //    DisplayMessage("Cannot add  minus value", 1);

                //    return;
                //}
                if (ddlscan.SelectedItem.ToString().Equals("NO") && Convert.ToInt32(txtscanseq.Text) != 0)
                {
                    DisplayMessage("Scan sequence should be zero", 1);
                    return;
                }

                if (_ItemKitComponent.Count > 0)
                {
                    var _filter = _ItemKitComponent.Where(x => x.MIKC_ITM_CODE_COMPONENT == txtitm.Text.ToUpper().Trim() && (x.MIKC_ACTIVE == 1 || x.MIKC_ACTIVE == 0)).ToList();
                    if (_filter.Count > 0)
                    {
                        foreach (ItemKitComponent item in _filter)
                        {
                            item.MIKC_NO_OF_UNIT = noofunit;
                            item.MIKC_COST = cost;
                            // item.MIKC_SEQ_NO = Convert.ToInt32(txtseq.Text);
                            item.MIKC_ITM_TYPE = ddltype.SelectedValue;
                            item.MIKC_ITEM_CATE = "1";
                            if (ddltype.SelectedValue == "M")
                            {
                                item.TYPE = "MAIN";
                            }
                            else
                            {
                                item.TYPE = "COM";
                            }
                            item.MIKC_CHG_MAIN_SERIAL = Convert.ToInt32(ddlmain.SelectedValue);
                            item.CHANGITEM = ddlmain.SelectedItem.ToString();
                            item.MIKC_ISSCAN = Convert.ToInt32(ddlscan.SelectedValue);
                            item.ISSCAN = ddlscan.SelectedItem.ToString();
                            item.MIKC_COST_METHOD = "AMT";
                            if (!txtscanseq.Text.Equals(""))
                            { item.MIKC_SCAN_SEQ = Convert.ToInt32(txtscanseq.Text); }
                            item.MIKC_LAST_MODIFY_BY = Session["UserID"].ToString();
                            item.ACTIVEDES = ddlstatus.SelectedItem.ToString();
                            item.MIKC_LAST_MODIFY_WHEN = DateTime.Now;
                            if (item.ACTIVEDES.Equals("ACTIVE"))
                            {
                                item.MIKC_ACTIVE = 1;
                            }
                            else
                            {
                                item.MIKC_ACTIVE = 0;
                            }
                            // grdKitcomp.DataSource = _ItemKitComponent.Where(x => x.MIKC_ACTIVE == 1).ToList();
                            grdKitcomp.DataSource = _ItemKitComponent;
                            grdKitcomp.DataBind();
                            txtitm.Text = string.Empty;
                            txtnounit.Text = string.Empty;
                            txtcost.Text = string.Empty;
                            txtseq.Text = string.Empty;
                            txtscanseq.Text = string.Empty;
                            ddlscan.SelectedValue = "0";
                            ddlmain.SelectedValue = "0";
                            ddlstatus.SelectedValue = "0";
                            ddltype.SelectedValue = "M";
                        }

                        DisplayMessage("Successfully Updated", 3);
                        lblItemDescription.Text = "";
                        lblItemModel.Text = "";
                        lblItemBrand.Text = "";
                        return;
                    }
                }

                ItemKitComponent _kit = new ItemKitComponent();
                _kit.MIKC_ITM_CODE_COMPONENT = txtitm.Text.ToUpper().Trim();
                _kit.MIKC_ITM_CODE_MAIN = txtkititemcode.Text;
                _kit.MIKC_DESC_COMPONENT = lblItemDescription.Text;
                _kit.MIKC_NO_OF_UNIT = noofunit;
                _kit.MIKC_COST = cost;
                if (_ItemKitComponent.Count < 1)
                {
                    _kit.MIKC_SEQ_NO = 1;
                }
                else
                {
                    _kit.MIKC_SEQ_NO = Convert.ToInt16(_ItemKitComponent.Max(x => x.MIKC_SEQ_NO).ToString()) + 1;
                }

                _kit.MIKC_ITM_TYPE = ddltype.SelectedValue;
                _kit.MIKC_ITEM_CATE = "1";
                _kit.MIKC_CREATE_BY = Session["UserID"].ToString();
                if (ddltype.SelectedValue == "M")
                {
                    _kit.TYPE = "MAIN";
                }
                else
                {
                    _kit.TYPE = "COM";
                }
                _kit.MIKC_ACTIVE = 1;
                _kit.ACTIVEDES = "ACTIVE";
                _kit.MIKC_CHG_MAIN_SERIAL = Convert.ToInt32(ddlmain.SelectedValue);
                _kit.CHANGITEM = ddlmain.SelectedItem.ToString();
                _kit.MIKC_ISSCAN = Convert.ToInt32(ddlscan.SelectedValue);
                _kit.ISSCAN = ddlscan.SelectedItem.ToString();
                _kit.MIKC_COST_METHOD = "AMT";
                _kit.MIKC_CREATE_WHEN = DateTime.Now;
                _kit.MIKC_LAST_MODIFY_WHEN = DateTime.Now;
                _kit.MIKC_SCAN_SEQ = Convert.ToInt32(txtscanseq.Text);
                _ItemKitComponent.Add(_kit);
                grdKitcomp.DataSource = _ItemKitComponent;
                grdKitcomp.DataBind();
                txtitm.Text = string.Empty;
                txtnounit.Text = string.Empty;
                txtcost.Text = string.Empty;
                txtseq.Text = string.Empty;
                txtscanseq.Text = string.Empty;
                ddlscan.SelectedValue = "0";
                ddlmain.SelectedValue = "0";
                ddlstatus.SelectedValue = "0";
                ddltype.SelectedValue = "M";
                DisplayMessage("Successfully Inserted!", 3);
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        //Dulaj 2018/Nov/06
        protected void lbtnAddKItItem_Click(object sender, EventArgs e)
        {
            try
            {
                //if(_ItemKitComponent==null)
                //{
                //    DisplayMessage("Kit item is not available", 1);
                //    return;
                //}
                //if (_ItemKitComponent.Count==0)
                //{
                //    DisplayMessage("Kit item is not available", 1);
                //    return;
                //}
                //if ((string.IsNullOrEmpty(txtkitcode.Text.Trim())))
                //{
                //    DisplayMessage("Kit code is empty", 1);
                //    return;
              //  }
                List<ItemKitComponent> check = _ItemKitComponent.Where(x => x.MIKC_ITM_CODE_MAIN.Equals(txtkitcode.Text)).ToList();
                if(check==null)
                {
                    DisplayMessage("Kit item is already added", 1);
                    txtkitcode.Text = "";
                    kitdetadddes.Text = "";
                    return;
                }
                else
                {
                    if(check.Count>0)
                    {
                        DisplayMessage("Kit item is already added", 1);
                        txtkitcode.Text = "";
                        kitdetadddes.Text = "";
                        return;
                    }
                }
                List<ItemKitComponent> kitItems = GetComItemByKitCd(txtkitcode.Text.Trim());

                foreach (ItemKitComponent _kit in kitItems)
                {
                    var _filter = _ItemKitComponent.Where(x => x.MIKC_ITM_CODE_COMPONENT == _kit.MIKC_ITM_CODE_COMPONENT.Trim() && (x.MIKC_ACTIVE == 1 || x.MIKC_ACTIVE == 0)).ToList();
                    if (_filter.Count >0)
                    {
                        _ItemKitComponent.Remove(_filter[0]);
                    }
                        if (_ItemKitComponent.Count < 1)
                        {
                            _kit.MIKC_SEQ_NO = 1;
                        }
                        else
                        {
                            _kit.MIKC_SEQ_NO = Convert.ToInt16(_ItemKitComponent.Max(x => x.MIKC_SEQ_NO).ToString()) + 1;
                        }
                        _kit.MIKC_ITM_CODE_MAIN = txtkitcode.Text;
                        _kit.MIKC_ACTIVE = 1;
                        _kit.ACTIVEDES = "ACTIVE";
                        _kit.MIKC_COST_METHOD = "AMT";
                        _kit.MIKC_CREATE_WHEN = DateTime.Now;
                        _kit.MIKC_LAST_MODIFY_WHEN = DateTime.Now;
                        _ItemKitComponent.Add(_kit);
                    //}
                }
                grdKitcomp.DataSource = _ItemKitComponent;
                grdKitcomp.DataBind();
                txtitm.Text = string.Empty;
                txtnounit.Text = string.Empty;
                txtcost.Text = string.Empty;
                txtseq.Text = string.Empty;
                txtscanseq.Text = string.Empty;
                ddlscan.SelectedValue = "0";
                ddlmain.SelectedValue = "0";
                ddlstatus.SelectedValue = "0";
                ddltype.SelectedValue = "M";
                txtkitcode.Text = "";
                kitdetadddes.Text = "";
                DisplayMessage("Successfully Inserted!", 3);
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }
        //
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSavelconformmessageValue.Value == "No")
            {
                return;
            }
            if (_ItemKitComponent.Count == 0)
            {
                string Msg = "Please add component item";
                DisplayMessage(Msg, 2);
                return;
            }
            foreach(ItemKitComponent kit in _ItemKitComponent)
            {
                kit.MIKC_ITM_CODE_MAIN = txtkititemcode.Text;
            }
            int result = 0;
            string err = string.Empty;

            if (_mstcostlist != null)
            {
                _ComFineItem = new List<MasterKitComFineItem>();
                foreach (mst_itm_fg_cost _cos in _mstcostlist)
                {
                    MasterKitComFineItem _itm = new MasterKitComFineItem();
                    _itm.MIF_ACT = Convert.ToInt32(_cos.Ifc_stus);
                    _itm.MIF_CRE_BY = _cos.Ifc_create_by;
                    _itm.MIF_CRE_DT = _cos.Ifc_create_when;
                    _itm.MIF_FG_CD = _cos.Ifc_fg_item_code;
                    _itm.MIF_KIT_CD = _cos.Ifc_item_code;
                    _itm.MIF_MOD_BY = _cos.Ifc_last_modify_by;
                    _itm.MIF_MOD_DT = _cos.Ifc_last_modify_when;
                    _ComFineItem.Add(_itm);
                }
            }
            result = CHNLSVC.General.SaveItemKitComponentNEW(_ItemKitComponent, _mstcostlist, _ComFineItem, out err);
            if (result != -1)
            {
                string Msg = "Successfully saved. ";
                DisplayMessage(Msg, 3);
                PageClear();
            }
            else
            {
                DisplayMessage(err, 4);
            }

        }

        protected void lbtnaddfin_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtfitem.Text))
                {
                    DisplayMessage("Please enter item code", 1);
                    txtitm.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtkititemcode.Text))
                {
                    DisplayMessage("Please enter kit code", 1);
                    txtitm.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtfcost.Text))
                {
                    DisplayMessage("Please enter unit cost", 1);
                    txtcost.Focus();
                    txtcost.Text = "0";
                    return;
                }
                decimal cost = Convert.ToDecimal(txtfcost.Text);

                if (cost < 0)
                {
                    DisplayMessage("canot add  minus cost", 1);
                    txtcost.Focus();
                    return;
                }
                if (_mstcostlist != null)
                {
                    if (_mstcostlist.Count > 0)
                    {
                        var _filter = _mstcostlist.Where(x => x.Ifc_fg_item_code == txtitm.Text.ToUpper().Trim() && x.Ifc_stus == true).ToList();
                        if (_filter.Count > 0)
                        {
                            DisplayMessage("already add item", 1);
                            return;
                        }
                    }
                }
                else
                {
                    _mstcostlist = new List<mst_itm_fg_cost>();
                }
                mst_itm_fg_cost _kit = new mst_itm_fg_cost();
                _kit.Ifc_cost_amount = cost;
                _kit.Ifc_cost_type = ddlPayType.SelectedValue;
                _kit.Ifc_create_by = Session["UserID"].ToString();
                _kit.Ifc_currency_code = "LKR";
                _kit.Ifc_fg_item_code = txtfitem.Text.ToUpper();
                _kit.Ifc_item_code = txtkititemcode.Text;
                _kit.Ifc_last_modify_by = Session["UserID"].ToString();
                _kit.Ifc_stus = true;
                _kit.IFC_new = true;
                _mstcostlist.Add(_kit);
                grdfitem.DataSource = _mstcostlist.Where(x => x.Ifc_stus == true).ToList();
                grdfitem.DataBind();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtfitem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!LoadItemDetail(txtfitem.Text.ToUpper()))
                {
                    DisplayMessage("Please check the item code", 1);
                    txtitm.Text = "";
                    txtitm.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }

        }
        private void GetFineshitem(string _itm)
        {
            _mstcostlist = CHNLSVC.General.GetFinishGood(_itm);
            if (_mstcostlist != null)
            {
                grdfitem.DataSource = _mstcostlist.Where(x => x.Ifc_stus == true).ToList();
                grdfitem.DataBind();
            }

        }

        protected void lbtnkitDelete_Click(object sender, EventArgs e)
        {
            if (txtDeleteconformmessageValue.Value == "No")
            {
                return;
            }
            if (grdKitcomp.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _ltem = (row.FindControl("lblmikc_itm_code_main") as Label).Text;
                if (_ItemKitComponent != null)
                {
                    var _filter = _ItemKitComponent.SingleOrDefault(x => x.MIKC_ITM_CODE_COMPONENT == _ltem);
                    if (_filter.MIKC_ACTIVE == 0)
                    {
                        DisplayMessage("Item is already inactive", 4);
                        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "inactive()", true);
                        return;
                    }
                    if (_filter.MIKC_SEQ_NO == 0)
                    {
                        _ItemKitComponent.RemoveAll(x => x.MIKC_ITM_CODE_COMPONENT == _ltem);
                    }
                    else
                    {
                        _filter.MIKC_ACTIVE = 0;
                        _filter.ACTIVEDES = "INACTIVE";
                        //_ItemKitComponent.RemoveAll(x => x.MIKC_ACTIVE == 0);
                        //_ItemKitComponent = _ItemKitComponent.Where(x => x.MIKC_ACTIVE == 1).ToList();
                    }

                    grdKitcomp.DataSource = _ItemKitComponent.Where(x => x.MIKC_ACTIVE == 1 || x.MIKC_ACTIVE == 0).ToList();
                    grdKitcomp.DataBind();

                }
            }
        }
        protected void lbtnkitEdit_Click(object sender, EventArgs e)
        {

            try
            {
                if (grdKitcomp.Rows.Count == 0) return;
                var lb = (Button)sender;
                var row = (GridViewRow)lb.NamingContainer;
                if (row != null)
                {
                    bool check = LoadItemDetail((row.FindControl("lblmikc_itm_code_main") as Label).Text);
                    if (check)
                    {
                        txtitm.Text = (row.FindControl("lblmikc_itm_code_main") as Label).Text;
                        txtnounit.Text = (row.FindControl("lblmikc_no_of_unit") as Label).Text;
                        txtcost.Text = (row.FindControl("lblmikc_cost") as Label).Text;
                        txtscanseq.Text = (row.FindControl("lblmikc_scan_seq") as Label).Text;

                        ddltype.SelectedValue = (row.FindControl("lblmikc_tp") as Label).Text;
                        string status = (row.FindControl("lblmikc_active") as Label).Text;
                        if (status.Equals("ACTIVE"))
                        {
                            ddlstatus.SelectedValue = "0";
                        }
                        else
                        {
                            ddlstatus.SelectedValue = "1";
                        }
                        string changeItem = (row.FindControl("lblmikc_chg_main_serial") as Label).Text;
                        if (changeItem.Equals("NO"))
                        {
                            ddlmain.SelectedValue = "0";
                        }
                        else
                        {
                            ddlmain.SelectedValue = "1";
                        }
                        string scan = (row.FindControl("lblmikc_isscan") as Label).Text;
                        if (scan.Equals("NO"))
                        {
                            ddlscan.SelectedValue = "0";
                        }
                        else
                        {
                            ddlscan.SelectedValue = "1";
                        }

                        txtseq.Text = (row.FindControl("lblmikc_seq_no") as Label).Text;

                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }



        }


        protected void lbtnFItemDelete_Click(object sender, EventArgs e)
        {
            if (txtDeleteconformmessageValue.Value == "No")
            {
                return;
            }
            if (grdfitem.Rows.Count == 0) return;
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row != null)
            {
                string _ltem = (row.FindControl("lblifc_fg_item_code") as Label).Text;
                if (_mstcostlist != null)
                {
                    var _filter = _mstcostlist.SingleOrDefault(x => x.Ifc_fg_item_code == _ltem && x.Ifc_item_code == txtkititemcode.Text);
                    if (_filter.IFC_new)
                    {
                        _mstcostlist.RemoveAll(x => x.Ifc_fg_item_code == _ltem && x.Ifc_item_code == txtkititemcode.Text);
                    }
                    else
                    {
                        _filter.Ifc_stus = false;
                        //_ItemKitComponent.RemoveAll(x => x.MIKC_ACTIVE == 0);
                        //_ItemKitComponent = _ItemKitComponent.Where(x => x.MIKC_ACTIVE == 1).ToList();
                    }

                    grdfitem.DataSource = _mstcostlist.Where(x => x.Ifc_stus == true).ToList();
                    grdfitem.DataBind();

                }
            }
        }
        protected void lbtnclear_Click(object sender, EventArgs e)
        {
            if (txtClearlconformmessageValue.Value == "Yes")
            {
                try
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
                catch (Exception ex)
                {
                    //divalert.Visible = true;
                    DisplayMessage(ex.Message, 4);
                }
            }
        }

        protected void btnSearch2_Item_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                txtSearchbyword.Text = "";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Item2";
                BindUCtrlDDLData(result);
                txtSearchbyword.Focus();
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {



            string targetFileName = "";
            targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
            FastForward.SCMWeb.View.Reports.Inventory.clsInventory obj = new FastForward.SCMWeb.View.Reports.Inventory.clsInventory();
            obj.GetKITComponent(Session["UserCompanyCode"].ToString(), txtkititemcode.Text);
            PrintPDF(targetFileName, obj._KIT_ComSetup);
            string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
        }

        public void PrintPDF(string targetFileName, ReportDocument _rpt)
        {
            //  try
            //  {
            ReportDocument rptDoc = (ReportDocument)_rpt;
            DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
            rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            diskOpts.DiskFileName = targetFileName;
            rptDoc.ExportOptions.DestinationOptions = diskOpts;
            rptDoc.Export();

            rptDoc.Close();
            rptDoc.Dispose();

            //rptDoc.Close();
            //rptDoc.Dispose();

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }


    }
}