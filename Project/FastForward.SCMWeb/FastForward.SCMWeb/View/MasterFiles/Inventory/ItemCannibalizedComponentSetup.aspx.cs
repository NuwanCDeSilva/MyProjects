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
    public partial class ItemCannibalizedComponentSetup : BasePage
    {
        List<int> hassuccessitems = new List<int>();
        private ItemKitComponent _kitCom
        {
            get { if (Session["_kitCom"] != null) { return (ItemKitComponent)Session["_kitCom"]; } else { return new ItemKitComponent(); } }
            set { Session["_kitCom"] = value; }
        }
        private List<ItemKitComponent> _kitComs
        {
            get { if (Session["_kitComs"] != null) { return (List<ItemKitComponent>)Session["_kitComs"]; } else { return new List<ItemKitComponent>(); } }
            set { Session["_kitComs"] = value; }
        }
        
        private MasterItem _mstItm
        {
            get { if (Session["_mstItm"] != null) { return (MasterItem)Session["_mstItm"]; } else { return new MasterItem(); } }
            set { Session["_mstItm"] = value; }
        }
        string _userid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    dgvSelect.DataSource = new int[] { };
                    dgvSelect.DataBind();

                    ViewState["ITEMS_SET"] = null;

                    DataTable dtconditions = new DataTable();
                    dtconditions.Columns.AddRange(new DataColumn[14] { new DataColumn("mikc_itm_code_component"), new DataColumn("mikc_desc_component"), 
                        new DataColumn("mikc_no_of_unit",System.Type.GetType("System.Decimal")), new DataColumn("mikc_cost",System.Type.GetType("System.Decimal")), 
                        new DataColumn("mikc_seq_no",System.Type.GetType("System.Int32")), new DataColumn("mikc_itm_type"), new DataColumn("mikc_active"), new DataColumn("mikc_item_cate"), new DataColumn("mikc_cost_method"), new DataColumn("mikc_chg_main_serial"), new DataColumn("mikc_uom"), new DataColumn("mikc_isscan"), new DataColumn("mikc_scan_seq"), new DataColumn("mikc_tp") });
                    ViewState["ITEMS_SET"] = dtconditions;
                    this.BindGrid();
                }
                else
                {
                    string sessionval = (string)Session["MAIN_SERIAL_SEARCH"];
                    if (!string.IsNullOrEmpty(sessionval))
                    {
                        mpexcel.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        private void Clear()
        {
            DataTable dtconditions = new DataTable();
            dtconditions.Columns.AddRange(new DataColumn[14] { new DataColumn("mikc_itm_code_component"), new DataColumn("mikc_desc_component"), new DataColumn("mikc_no_of_unit"), new DataColumn("mikc_cost"), new DataColumn("mikc_seq_no"), new DataColumn("mikc_itm_type"), new DataColumn("mikc_active"), new DataColumn("mikc_item_cate"), new DataColumn("mikc_cost_method"), new DataColumn("mikc_chg_main_serial"), new DataColumn("mikc_uom"), new DataColumn("mikc_isscan"), new DataColumn("mikc_scan_seq"), new DataColumn("mikc_tp") });
            ViewState["ITEMS_SET"] = dtconditions;
            this.BindGrid();

            ViewState["ITEMS_SET"] = dtconditions;

            dgvSelect.DataSource = new int[] { };
            dgvSelect.DataBind();

            hassuccessitems.Clear();

            txtItem.Text = string.Empty;
            txtSer.Text = string.Empty;

            txtitmcodeenter.Text = string.Empty;
            txtdescenter.Text = string.Empty;
            txtnoofunitenter.Text = string.Empty;
            txtcostenter.Text = string.Empty;
            txtseqnoenter.Text = string.Empty;
            ddlstus.SelectedValue = "1";
            RBordby.ClearSelection();

            Session["ITEM_CATE_OF_COMPONET"] = null;
            Session["MAIN_SERIAL_SEARCH"] = null;
            txtLoc.Text = string.Empty;
            txtbinlocation.Text = string.Empty;
            txtserialpopup.Text = string.Empty;
        }
        protected void BindGrid()
        {
            try
            {
                dgvSelect.DataSource = (DataTable)ViewState["ITEMS_SET"];
                dgvSelect.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.MainItem:
                    {
                        paramsText.Append("N/A" + seperator + "N/A");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ComponentItem:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }

                default:
                    break;
            }
            return paramsText.ToString();
        }
        private void FilterData()
        {
            try
            {
                if (lblvalue.Text == "312")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MainItem);
                    DataTable result = CHNLSVC.CommonSearch.SearchMainItemsData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text.ToUpper().Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "312";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }

                else if (lblvalue.Text == "457")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ComponentItem);
                    DataTable result = CHNLSVC.General.SearchComponentItems(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "457";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }

                else if (lblvalue.Text == "5")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                    DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "5";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }

                else if (ViewState["SEARCH"] != null)
                {
                    DataTable result = (DataTable)ViewState["SEARCH"];
                    DataView dv = new DataView(result);
                    string searchParameter = ddlSearchbykey.Text;

                    dv.RowFilter = "" + ddlSearchbykey.Text + " like '%" + txtSearchbyword.Text + "%'";

                    if (dv.Count > 0)
                    {
                        result = dv.ToTable();
                    }
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchbyword.Text))
                {
                    FilterData();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
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
                SIPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void LoadItemKitComponents()
        {
            try
            {
                
                FF.BusinessObjects.MasterItem _item = (FF.BusinessObjects.MasterItem)CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper());

                if (_item == null)
                {
                    txtItem.Text = string.Empty;
                    txtItem.Focus();
                    Clear();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid main item code !!!')", true);
                    return;
                }

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MainItem);
                DataTable result = CHNLSVC.CommonSearch.SearchMainItemsData(SearchParams, "CODE", txtItem.Text.ToUpper().Trim());

                if (result.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You can not cannibalize this item !!!')", true);
                    txtItem.Text = string.Empty;
                    txtSer.Text = string.Empty;
                    Clear();
                    return;
                }
                else
                {
                    //List<InventorySerialN> _serMsters = CHNLSVC.Inventory.Get_INR_SER_DATA(new InventorySerialN() 
                    //{ 
                    //    Ins_com = Session["UserCompanyCode"].ToString(), Ins_loc = Session["UserDefLoca"].ToString(),
                    //    Ins_itm_cd = txtItem.Text.ToUpper(),
                    //    Ins_available = 1,
                    //    Ins_ser_1 = string.IsNullOrEmpty(txtSer.Text) ? null : txtSer.Text
                    //});
                    ////List<InventorySerialN> _serMsters = CHNLSVC.Inventory.GetSerialMasterData(new InventorySerialN() { Irsm_com = Session["UserCompanyCode"].ToString(), Irsm_loc = Session["UserDefLoca"].ToString(), Irsm_itm_cd = txtItem.Text.ToUpper() });
                    //if (_serMsters!=null)
                    //{
                    //    if (_serMsters.Count>0)
                    //    {
                    //        txtSer.Text = _serMsters[0].Ins_ser_1;
                    //    }
                    //}
                    DataTable dtitmkitcompo = CHNLSVC.General.LoadItemKitComponents(txtItem.Text.Trim());
                   
                    RBordby.Enabled = true;
                    RBordby.SelectedIndex = 0;
                    if (dtitmkitcompo!=null)
                    {
                        if (dtitmkitcompo.Rows.Count > 0)
                        {
                            RBordby.Enabled = false;
                            string costMethode = dtitmkitcompo.Rows[0]["mikc_cost_method"].ToString();
                            if (costMethode == "PER")
                            {
                                RBordby.SelectedIndex = 0;
                            }
                            if (costMethode == "AMT")
                            {
                                RBordby.SelectedIndex = 1;
                            }
                            hdfCostTp.Value = "Cost";
                            if (dtitmkitcompo.Rows.Count > 0)
                            {
                                hdfCostTp.Value = costMethode == "PER" ? "Cost (%)" : costMethode == "AMT" ? "Cost (AMT)" : "Cost";
                            }
                            
                        }
                    }
                   
                    dgvSelect.DataSource = dtitmkitcompo;
                    
                    dgvSelect.DataBind();
                    Label lclCostHed = (Label)dgvSelect.HeaderRow.FindControl("lclCostHed");
                    lclCostHed.Text = hdfCostTp.Value;
                    ViewState["ITEMS_SET"] = dtitmkitcompo;
                } 
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lblvalue.Text == "312")
                {
                    txtItem.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtItem_TextChanged(null, null);
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }
                else if (lblvalue.Text == "457")
                {
                    txtitmcodeenter.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtdescenter.Text = grdResult.SelectedRow.Cells[2].Text;
                    txtdescenter.ToolTip = grdResult.SelectedRow.Cells[2].Text;
                    Session["ITEM_CATE_OF_COMPONET"] = grdResult.SelectedRow.Cells[3].Text;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }
                else if (lblvalue.Text == "5")
                {
                    txtLoc.Text = grdResult.SelectedRow.Cells[1].Text;
                    MasterBinLocation _bin = CHNLSVC.General.GetMasterBinLocation(new MasterBinLocation() { Ibl_com_cd = Session["UserCompanyCode"].ToString(), Ibl_loc_cd = txtLoc.Text.ToUpper() });
                    if (_bin != null)
                    {
                        txtbinlocation.Text = _bin.Ibl_bin_cd;
                    }
                    MpDelivery.Show();
                }
            }
            catch
            {

            }
        }

        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            LoadItemKitComponents();
        }

        protected void dgvSelect_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlnewstatus = (e.Row.FindControl("ddlnewstus") as DropDownList);

                string status = (e.Row.FindControl("lblstatusvalue") as Label).Text;
                ddlnewstatus.Items.FindByValue(status).Selected = true;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlaaloedit = (e.Row.FindControl("ddlaaloedit") as DropDownList);

                string status = (e.Row.FindControl("lbleditvalue") as Label).Text;
                ddlaaloedit.Items.FindByValue(status).Selected = true;
            }
        }
       

        protected void btnClear_Click(object sender, EventArgs e)
        {
            if (txtconfirmclear.Value == "Yes")
            {
                try
                {
                    Response.Redirect(Request.RawUrl);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
                finally
                {
                    CHNLSVC.CloseChannel();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtsaveconfirm.Value == "Yes")
            {
                try
                {
                    if (dgvSelect.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You have not added any components !!!')", true);
                        lbtnstus.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(RBordby.SelectedValue))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select cost method !!!')", true);
                        RBordby.Focus();
                        return;
                    }

                    if (RBordby.SelectedValue == "1")
                    {
                        foreach (GridViewRow validaterow in this.dgvSelect.Rows)
                        {
                            Label lblcost = (Label)validaterow.FindControl("lblcost");
                            Decimal percentage = Convert.ToDecimal(lblcost.Text);

                            if (percentage > 100)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please edit/remove cost values greater than 100 of components !!!')", true);
                                return;
                            }
                        }

                        Decimal finalpercentage = 0;
                        foreach (GridViewRow validaterow in this.dgvSelect.Rows)
                        {
                            Label lblcost = (Label)validaterow.FindControl("lblcost");
                            Decimal percentage = Convert.ToDecimal(lblcost.Text);
                            finalpercentage += percentage;
                        }

                        if (finalpercentage > 100)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid cost percentage !!!')", true);
                            return;
                        }
                    }

                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MainItem);
                    DataTable result = CHNLSVC.CommonSearch.SearchMainItemsData(SearchParams, "CODE", txtItem.Text.ToUpper().Trim());

                    if (result.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You can not cannibalize this item !!!')", true);
                        txtItem.Text = string.Empty;
                        txtSer.Text = string.Empty;
                        return;
                    }

                    ItemKitComponent ItemKitComp = new ItemKitComponent();

                    foreach (GridViewRow hiderowbtn in this.dgvSelect.Rows)
                    {
                        _userid = (string)Session["UserID"];

                        Label lblitemcode = (Label)hiderowbtn.FindControl("lblitemcode");
                        Label lbldesc = (Label)hiderowbtn.FindControl("lbldesc");
                        Label lblunits = (Label)hiderowbtn.FindControl("lblunits");
                        Label lblcost = (Label)hiderowbtn.FindControl("lblcost");
                        Label lblseq = (Label)hiderowbtn.FindControl("lblseq");
                        Label mikc_itm_type = (Label)hiderowbtn.FindControl("mikc_itm_type");
                        Label lblstatusvalue = (Label)hiderowbtn.FindControl("lblstatusvalue");
                        DropDownList ddlnewstus = (DropDownList)hiderowbtn.FindControl("ddlnewstus");
                        Label mikc_item_cate = (Label)hiderowbtn.FindControl("mikc_item_cate");
                        Label mikc_cost_method = (Label)hiderowbtn.FindControl("mikc_cost_method");
                        Label mikc_chg_main_serial = (Label)hiderowbtn.FindControl("mikc_chg_main_serial");
                        Label mikc_uom = (Label)hiderowbtn.FindControl("mikc_uom");
                        Label mikc_isscan = (Label)hiderowbtn.FindControl("mikc_isscan");
                        Label mikc_scan_seq = (Label)hiderowbtn.FindControl("mikc_scan_seq");
                        Label mikc_tp = (Label)hiderowbtn.FindControl("mikc_tp");
                        DropDownList mikc_allow_edit = (DropDownList)hiderowbtn.FindControl("ddlaaloedit");//Added By Dulaj 2018/Jun/16

                        ItemKitComp.MIKC_ITM_CODE_MAIN = txtItem.Text.Trim();
                        ItemKitComp.MIKC_ITM_CODE_COMPONENT = lblitemcode.Text.Trim();
                        ItemKitComp.MIKC_DESC_COMPONENT = lbldesc.Text.Trim();
                        ItemKitComp.MIKC_NO_OF_UNIT = Convert.ToDecimal(lblunits.Text.Trim());
                        ItemKitComp.MIKC_CREATE_BY = _userid;
                        ItemKitComp.MIKC_CREATE_WHEN = DateTime.Now.Date;
                        ItemKitComp.MIKC_LAST_MODIFY_BY = _userid;
                        ItemKitComp.MIKC_LAST_MODIFY_WHEN = DateTime.Now.Date;
                        ItemKitComp.MIKC_COST = Convert.ToDecimal(lblcost.Text.Trim());
                        ItemKitComp.MIKC_SEQ_NO =  Convert.ToInt32(lblseq.Text.Trim());
                        ItemKitComp.MIKC_ITM_TYPE = mikc_itm_type.Text.Trim();
                        ItemKitComp.MIKC_ACTIVE = Convert.ToInt32(ddlnewstus.SelectedValue);
                        ItemKitComp.MIKC_ITEM_CATE = mikc_item_cate.Text.Trim();
                        ItemKitComp.MIKC_COST_METHOD = mikc_cost_method.Text.Trim();
                        ItemKitComp.MIKC_CHG_MAIN_SERIAL = Convert.ToInt32(mikc_chg_main_serial.Text.Trim());
                        ItemKitComp.MIKC_UOM = mikc_uom.Text.Trim();
                        ItemKitComp.MIKC_ISSCAN = Convert.ToInt32(mikc_isscan.Text.Trim());
                        ItemKitComp.MIKC_SCAN_SEQ = Convert.ToInt32(mikc_scan_seq.Text.Trim());
                        ItemKitComp.MIKC_TP = mikc_tp.Text.Trim();
                        ItemKitComp.MIKC_ALLOW_EDIT = Convert.ToInt32(mikc_allow_edit.SelectedValue);
                        Int32 Success = CHNLSVC.General.SaveItemKitComponent(ItemKitComp);
                        hassuccessitems.Add(Success);
                    }

                    if (hassuccessitems.Contains(-1))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        return;
                    }
                    else
                    {
                        if (hassuccessitems.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully saved !!!')", true);
                            Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
                finally
                {
                    CHNLSVC.CloseChannel();
                }
            }
        }

        protected void lbtnstus_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a main item code !!!')", true);
                    txtitmcodeenter.Focus();
                    return;
                }
                //if (string.IsNullOrEmpty(txtSer.Text.Trim()))
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a serial # !!!')", true);
                //    txtitmcodeenter.Focus();
                //    return;
                //}
                if (string.IsNullOrEmpty(txtitmcodeenter.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter item code !!!')", true);
                    txtitmcodeenter.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtdescenter.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter description !!!')", true);
                    txtdescenter.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtnoofunitenter.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter no of units !!!')", true);
                    txtnoofunitenter.Focus();
                    return;
                }
                if (Convert.ToDecimal(txtnoofunitenter.Text.Trim())<=0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid no of units !!!')", true);
                    txtnoofunitenter.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtcostenter.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter cost !!!')", true);
                    txtcostenter.Focus();
                    return;
                }

                //if (string.IsNullOrEmpty(txtseqnoenter.Text.Trim()))
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter sequence # !!!')", true);
                //    txtseqnoenter.Focus();
                //    return;
                //}
                _mstItm = CHNLSVC.General.GetItemMaster(txtitmcodeenter.Text.Trim().ToUpper());
                int noOfUnit=0;
                if (_mstItm!=null)
                {
                    if (!(_mstItm.Mi_is_ser1==-1))
                    {
                        if (!Int32.TryParse(txtnoofunitenter.Text,out noOfUnit))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid no of units !!!')", true);
                            txtnoofunitenter.Focus();
                            return;
                        }
                    }
                }
                string costmethod = string.Empty;
                if (RBordby.SelectedValue == "1")
                {
                    costmethod = "PER";
                }
                else if (RBordby.SelectedValue == "0")
                {
                    costmethod = "AMT";
                }

                string itemcate = (string)Session["ITEM_CATE_OF_COMPONET"];
                DataTable dt = (DataTable)ViewState["ITEMS_SET"];
                foreach (DataRow row in dt.Rows)
                {
                    if ( row["mikc_itm_code_component"].ToString()==txtitmcodeenter.Text)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected item is already exists  !!!')", true);
                         return;
                    }
                }
                _kitCom = new ItemKitComponent() {
                    MIKC_ITM_CODE_MAIN = txtItem.Text.ToUpper() ,
                    MIKC_ITM_CODE_COMPONENT = txtitmcodeenter.Text.ToUpper()
                };
                _kitComs = CHNLSVC.Inventory.GetItemKitComponentSplit(_kitCom);
                if (_kitComs!= null)
                {
                    if (_kitComs.Count>0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected item is already exists  !!!')", true);
                        return;
                    }
                }
                decimal _totCost = 0;
                if (costmethod == "PER")
                {
                    if (_kitComs != null)
                    {
                        _totCost = _kitComs.Sum(c => c.MIKC_COST);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        _totCost = _totCost + ConDeceZero(dr["MIKC_COST"].ToString());
                    }
                    _totCost = _totCost + ConDeceZero(txtcostenter.Text);
                    if (_totCost > 100)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected main item cost precentage is exceed !!!')", true);
                        return;
                    }
                }
                
                dt.Rows.Add(txtitmcodeenter.Text.Trim(), txtdescenter.Text.Trim(), txtnoofunitenter.Text.Trim(), txtcostenter.Text.Trim(), 0, "M",
                    ddlstus.SelectedValue, itemcate, costmethod, 0, "N/A", 0, 0, "K", editableddl.SelectedValue);
                ViewState["ITEMS_SET"] = dt;

                dgvSelect.DataSource = null;
                dgvSelect.DataBind();

                hdfCostTp.Value = "Cost";
                if (costmethod!=null)
                {
                    hdfCostTp.Value = costmethod == "PER" ? "Cost (%)" : costmethod == "AMT" ? "Cost (AMT)" : "Cost";
                }
              

                dgvSelect.DataSource = dt;
                dgvSelect.DataBind();
                Label lclCostHed = (Label)dgvSelect.HeaderRow.FindControl("lclCostHed");
                lclCostHed.Text = hdfCostTp.Value;

                txtitmcodeenter.Text = string.Empty;
                txtdescenter.Text = string.Empty;
                txtnoofunitenter.Text = string.Empty;
                txtcostenter.Text = string.Empty;
                txtseqnoenter.Text = string.Empty;
                //ddlstus.SelectedValue = "1";
                RBordby.Enabled = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void dgvSelect_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {

            }
            catch
            {

            }
        }

        protected void dgvSelect_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (txtconfirmdelete.Value == "Yes")
            {
                int Myindex = Convert.ToInt32(e.RowIndex);
                DataTable dt = ViewState["ITEMS_SET"] as DataTable;
                dt.Rows[Myindex].Delete();
                dt.AcceptChanges();
                ViewState["ITEMS_SET"] = dt;
                BindGrid();
            }
        }

        protected void lbtngrdInvoiceDetailsEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                dgvSelect.EditIndex = grdr.RowIndex;

                DataTable dt = ViewState["ITEMS_SET"] as DataTable;

                dgvSelect.DataSource = dt;
                dgvSelect.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }  
        }

        protected void lbtngrdInvoiceDetailsUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;

                if (row != null)
                {
                    string unit = (row.FindControl("txtnoofunits") as TextBox).Text;
                    string cost = (row.FindControl("txtcostgrid") as TextBox).Text;
                    string seq = (row.FindControl("txtsequence") as TextBox).Text;

                    if (string.IsNullOrEmpty(unit))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter no of unit !!!')", true);
                        return;
                    }

                    if (string.IsNullOrEmpty(cost))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter cost !!!')", true);
                        return;
                    }

                    //if (string.IsNullOrEmpty(seq))
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter sequence # !!!')", true);
                    //    return;
                    //}

                    dgvSelect.EditIndex = -1;

                    DataTable dt = (DataTable)ViewState["ITEMS_SET"];

                    dt.Rows[row.RowIndex]["mikc_no_of_unit"] = unit;
                    dt.Rows[row.RowIndex]["mikc_cost"] = cost;
                 //   dt.Rows[row.RowIndex]["mikc_seq_no"] = seq;

                    dgvSelect.DataSource = dt;
                    dgvSelect.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtngrdInvoiceDetailsCancel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                dgvSelect.EditIndex = -1;

                DataTable dt = ViewState["ITEMS_SET"] as DataTable;

                dgvSelect.DataSource = dt;
                dgvSelect.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            } 
        }

        protected void lbtngrdInvoiceDetailsDalete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmdelete.Value == "Yes")
                {
                    LinkButton btn = (LinkButton)sender;
                    GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                    DataTable dt = (DataTable)ViewState["ITEMS_SET"];
                    dt.Rows.RemoveAt(grdr.RowIndex);

                    ViewState["ITEMS_SET"] = dt;
                    dgvSelect.DataSource = dt;
                    dgvSelect.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
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
        protected void lbtnitem_Click(object sender, EventArgs e)
        {
            try
            {
                txtSer.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MainItem);
                DataTable result = CHNLSVC.CommonSearch.SearchMainItemsData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "312";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ComponentItem);
                DataTable result = CHNLSVC.General.SearchComponentItems(SearchParams, null, null);
                grdResult.PageIndex = 0;
                grdResult.DataSource = result;
                grdResult.DataBind();
                
                lblvalue.Text = "457";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void txtitmcodeenter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FF.BusinessObjects.MasterItem _item = (FF.BusinessObjects.MasterItem)CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtitmcodeenter.Text.ToUpper());

                if (_item == null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid item code !!!')", true);
                    txtitmcodeenter.Text = string.Empty;
                    return;
                }

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ComponentItem);
                DataTable result = CHNLSVC.General.SearchComponentItems(SearchParams, "CODE", txtitmcodeenter.Text.Trim());

                if (result.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You can not assign this item for cannibalizing !!!')", true);
                    txtitmcodeenter.Text = string.Empty;
                    return;
                }
                else
                {
                    foreach (DataRow ddr in result.Rows)
                    {
                        txtitmcodeenter.Text = ddr["Code"].ToString();
                        txtdescenter.Text = ddr["Description"].ToString();
                        txtdescenter.ToolTip = ddr["Description"].ToString();
                        Session["ITEM_CATE_OF_COMPONET"] = ddr["Category"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void btnsrch_ser_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtItem.Text))
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a item code !!!')", true);
            //    txtItem.Focus();
            //    return;
            //}
            Clear();
            txtLoc.Text = string.Empty;
            txtbinlocation.Text = string.Empty;
            txtserialpopup.Text = string.Empty;
            MpDelivery.Show();
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            txtSer.Text = "";
            if (Session["UserCompanyCode"]==null)
	        {
		        Response.Redirect("~/Login.aspx");return;
            }
           string _com=Session["UserCompanyCode"]!=null?Session["UserCompanyCode"].ToString():null;
           string _loc=!string.IsNullOrEmpty(txtLoc.Text)?txtLoc.Text.ToUpper():null;
           string _bin=!string.IsNullOrEmpty(txtbinlocation.Text)?txtbinlocation.Text.ToUpper():null;
           string _ser=!string.IsNullOrEmpty(txtserialpopup.Text)?txtserialpopup.Text.ToUpper():null;
           DataTable dtserial = CHNLSVC.General.LoadSerialByBin(_com, _loc, _ser, _bin);
           if (dtserial.Rows.Count<1)
           {
               ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No serial found !!!')", true); 
               MpDelivery.Show();
               return;
           }
           //if (dtserial.Rows.Count == 1)
           //{
           //    //txtItem.Text = dtserial[0][0].tost;
           //    if (!string.IsNullOrEmpty(txtSer.Text))
           //    {
           //        txtLoc.Text = string.Empty;
           //        txtbinlocation.Text = string.Empty;
           //        txtserialpopup.Text = string.Empty;
           //        MpDelivery.Hide();
           //        txtSer_TextChanged(null,null);
           //    }
           //    else
           //    {
           //        MpDelivery.Show();
           //    }
           //}
           if (dtserial.Rows.Count > 0)
           {
               //DataTable _dtSer = new DataTable();

               //_dtSer = CHNLSVC.Inventory.GetSerialDetailsBySerialwithoutItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtserialpopup.Text);
               dgvItem.DataSource = new int[] { };
               dgvItem.DataSource = dtserial;
               dgvItem.DataBind();

               lblcaption.Text = "Select Item Code";
               mpexcel.Show();
           }
        }

        private void LoadSerialData()
        {
            DataTable _dtSer = new DataTable();

            _dtSer = CHNLSVC.Inventory.GetSerialDetailsBySerialwithoutItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtSer.Text);
            if (_dtSer.Rows.Count > 1)
            {
                dgvItem.DataSource = new DataTable();
                dgvItem.DataSource = _dtSer;
                dgvItem.DataBind();

                lblcaption.Text = "Select Item Code";
                mpexcel.Show();
                Session["MAIN_SERIAL_SEARCH"] = "Yes";
            }
            else if (_dtSer.Rows.Count == 1)
            {
                txtItem.Text = _dtSer.Rows[0]["ins_itm_cd"].ToString();

                FF.BusinessObjects.MasterItem _item = (FF.BusinessObjects.MasterItem)CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper());

                if (_item == null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid item code !!!')", true);
                    txtitmcodeenter.Text = string.Empty;
                    return;
                }

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MainItem);
                DataTable result = CHNLSVC.CommonSearch.SearchMainItemsData(SearchParams, "CODE", txtItem.Text.ToUpper().Trim());

                if (result.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You can not cannibalize this item !!!')", true);
                    txtItem.Text = string.Empty;
                    return;
                }
                else
                {
                    DataTable dtitmkitcompo = CHNLSVC.General.LoadItemKitComponents(txtItem.Text.Trim());
                    dgvSelect.DataSource = dtitmkitcompo;
                    dgvSelect.DataBind();

                    ViewState["ITEMS_SET"] = dtitmkitcompo;
                } 
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a valid serial !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                txtSer.Text = "";
                txtSer.Focus();
                return;
            }
        }

        protected void txtSer_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSer.Text))
            {
                DataTable dtserial = CHNLSVC.General.LoadSerialByBin(null, null, txtSer.Text, null);
                if (dtserial.Rows.Count<1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid serial # !!!')", true);
                    return;
                }
                else
                {
                    txtItem.Text = dtserial.Rows[0]["INS_ITM_CD"].ToString();
                    txtItem_TextChanged(null,null);
                }
            }
            // LoadSerialData();
        }

        protected void dgvItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Label lblitemcodepopup = (Label)dgvItem.SelectedRow.FindControl("lblitemcodepopup");
                Label lblSerialPop = (Label)dgvItem.SelectedRow.FindControl("lblSerialPop");
                txtItem.Text = lblitemcodepopup.Text;
                txtSer.Text = lblSerialPop.Text;
                txtItem_TextChanged(null, null);

                //if (!string.IsNullOrEmpty(txtserialpopup.Text.Trim()))
                //{
                //    txtSer.Text = txtserialpopup.Text;
                //    txtSer_TextChanged(null,null);
                //}
                //txtLoc.Text = string.Empty;
                //txtbinlocation.Text = string.Empty;
                //txtserialpopup.Text = string.Empty;
                //Session["MAIN_SERIAL_SEARCH"] = null;
                //mpexcel.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void btnUserLocation_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                txtLoc.Text = string.Empty;
                txtbinlocation.Text = string.Empty;
                txtserialpopup.Text = string.Empty;

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "5";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }
        protected void txtserialpopup_TextChanged(object sender, EventArgs e)
        {
            DataTable dtserial = CHNLSVC.General.LoadSerialByBin(Session["UserCompanyCode"].ToString(), txtLoc.Text.Trim(), txtserialpopup.Text.Trim(),"");

            if (dtserial.Rows.Count > 1)
            {
                DataTable _dtSer = new DataTable();

                _dtSer = CHNLSVC.Inventory.GetSerialDetailsBySerialwithoutItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtserialpopup.Text);

                dgvItem.DataSource = new DataTable();
                dgvItem.DataSource = _dtSer;
                dgvItem.DataBind();

                lblcaption.Text = "Select Item Code";
                mpexcel.Show();
            }
            else if (dtserial.Rows.Count == 1)
            {
                foreach (DataRow ddr in dtserial.Rows)
                {
                    txtLoc.Text = ddr["ins_loc"].ToString();
                    txtbinlocation.Text = ddr["ins_bin"].ToString();
                    txtItem.Text = ddr["ins_itm_cd"].ToString();
                }
                MpDelivery.Show();
            }
        }

        protected void LinkButton13_Click(object sender, EventArgs e)
        {
            txtItem.Text = string.Empty;
            txtLoc.Text = string.Empty;
            txtbinlocation.Text = string.Empty;
            txtserialpopup.Text = string.Empty;

            MpDelivery.Hide();
        }

        protected void LinkButton8_Click(object sender, EventArgs e)
        {
            Session["MAIN_SERIAL_SEARCH"] = null;
            mpexcel.Hide();
        }

        private decimal ConDeceZero(string txt) {
            decimal d=0;
            if (decimal.TryParse(txt,out d))
            {
                return d;
            }
            return d;
        }

        protected void dgvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtItem.Text = dgvResult.SelectedRow.Cells[1].Text;
                txtSer.Text = dgvResult.SelectedRow.Cells[5].Text;
                txtItem_TextChanged(null, null);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void dgvResult_PageIndexChanged(object sender, EventArgs e)
        {

        }
    }
}