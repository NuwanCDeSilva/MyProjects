using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using FastForward.SCMWeb.Services;
using FF.BusinessObjects.General;
using FastForward.SCMWeb.View.Reports.Inventory;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.View.Reports.Barcode;

namespace FastForward.SCMWeb.View.Transaction.Inventory
{
    public partial class ItemSplitting : BasePage
    {
        DataTable _serData
        {
            get { if (Session["_serData"] != null) { return (DataTable)Session["_serData"]; } else { return new DataTable(); } }
            set { Session["_serData"] = value; }
        }
        string _serType
        {
            get { if (Session["_serType"] != null) { return (string)Session["_serType"]; } else { return ""; } }
            set { Session["_serType"] = value; }
        }
        string _Para = "";

        string SerPopShow
        {
            get { if (Session["SerPopShow"] != null) { return (string)Session["SerPopShow"]; } else { return ""; } }
            set { Session["SerPopShow"] = value; }
        }

        private List<ItemKitComponent> _kitComs
        {
            get { if (Session["_kitComs"] != null) { return (List<ItemKitComponent>)Session["_kitComs"]; } else { return new List<ItemKitComponent>(); } }
            set { Session["_kitComs"] = value; }
        }
        private ItemKitComponent _kitCom
        {
            get { if (Session["_kitCom"] != null) { return (ItemKitComponent)Session["_kitCom"]; } else { return new ItemKitComponent(); } }
            set { Session["_kitCom"] = value; }
        }
        private List<MasterItemComponent> _itmComs
        {
            get { if (Session["_itmComs"] != null) { return (List<MasterItemComponent>)Session["_itmComs"]; } else { return new List<MasterItemComponent>(); } }
            set { Session["_itmComs"] = value; }
        }
        private MasterItemComponent _itmCom
        {
            get { if (Session["_itmCom"] != null) { return (MasterItemComponent)Session["_itmCom"]; } else { return new MasterItemComponent(); } }
            set { Session["_itmCom"] = value; }
        }
        private MasterLocationNew _mstLoc
        {
            get { if (Session["_mstLoc"] != null) { return (MasterLocationNew)Session["_mstLoc"]; } else { return new MasterLocationNew(); } }
            set { Session["_mstLoc"] = value; }
        }
        private MasterItem _mstItm
        {
            get { if (Session["_mstItm"] != null) { return (MasterItem)Session["_mstItm"]; } else { return new MasterItem(); } }
            set { Session["_mstItm"] = value; }
        }

        private Int32 _lineNo
        {
            get { if (Session["_lineNo"] != null) { return (Int32)Session["_lineNo"]; } else { return new Int32(); } }
            set { Session["_lineNo"] = value; }
        }
        private string _selectedstatus = ItemStatus.GOD.ToString();
        private string _selectedLoc = string.Empty;
        private bool IsAllowChangeStatus = false;
        private enum ItemStatus
        {
            GOD
        }

        Int32 _serId = 0;
        private List<InventorySubSerialMaster> _SubSerialList = null;

        public static string DoFormat(Decimal myNumber)
        {
            var s = string.Format("{0:0.00}", myNumber);

            if (s.EndsWith("00"))
            {
                return s;
            }
            else
            {
                return s;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count < 1)
            {
                Response.Redirect("~/View/ADMIN/Home.aspx"); return;
            }
            if (string.IsNullOrEmpty(Request.QueryString[0]))
            {
                Response.Redirect("~/View/ADMIN/Home.aspx"); return;
            }
            try
            {
                string _mytype = Request.QueryString["TYPE"];
                if (!IsPostBack)
                {
                    dgvSelect.DataSource = new int[] { };
                    dgvSelect.DataBind();

                    dgvItem.DataSource = new int[] { };
                    dgvItem.DataBind();

                    gvLoc.DataSource = new int[] { };
                    gvLoc.DataBind();

                    gvStatus.DataSource = new int[] { };
                    gvStatus.DataBind();

                    DateTime orddate1 = DateTime.Now;
                    dtpFromDate.Text = orddate1.ToString("dd/MMM/yyyy");

                    Decimal lblFreeval = Convert.ToDecimal(0);
                    lblFree.Text = DoFormat(lblFreeval);

                    Decimal lblResval = Convert.ToDecimal(0);
                    lblRes.Text = DoFormat(lblResval);
                    pnlDocSer.Visible = true;
                    if (_mytype == "K")
                    {
                        if (ddlsplittype.Items.FindByValue("K") == null)
                        {
                            ddlsplittype.Items.Insert(3, new ListItem("Cannibalize", "K"));
                        }
                        ddlsplittype.SelectedIndex = 3;
                        ddlsplittype.Enabled = false;
                        lblHeding.Text = "Main Item Cannibalization";
                    }
                    else
                    {
                        lblHeding.Text = "Item Splitting";
                        ddlsplittype.Enabled = true;
                    }
                    Session["documntNo"] = string.Empty;
                }
                else
                {
                    if (SerPopShow == "Show")
                    {
                        PopupSearch.Show();
                    }
                    else
                    {
                        PopupSearch.Hide();
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

        private bool LoadUserPermission()
        {
            string _masterLocation = (string.IsNullOrEmpty(Session["UserDefLoca"].ToString())) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
            if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10104))
                IsAllowChangeStatus = true;
            else
                IsAllowChangeStatus = false;

            return IsAllowChangeStatus;
        }
        private void ClearData()
        {
            //  ddlsplittype.SelectedIndex = 0;
            //txtItem.Text = string.Empty;
            //txtSer.Text = string.Empty;
            txtItemDesn.Text = string.Empty;
            txtWarr.Text = string.Empty;
            txtModel.Text = string.Empty;
            txtBrand.Text = string.Empty;
            txtManual.Text = string.Empty;
            txtRem.Text = string.Empty;

            dgvSelect.DataSource = new int[] { };
            dgvSelect.DataBind();

            dgvItem.DataSource = new int[] { };
            dgvItem.DataBind();

            gvLoc.DataSource = new int[] { };
            gvLoc.DataBind();

            gvStatus.DataSource = new int[] { };
            gvStatus.DataBind();

            Session["ITM"] = null;
            Session["MFC"] = null;
            Session["STUS"] = null;
            Session["SER"] = null;
            Session["LOC"] = null;
            Session["LINE"] = null;
            Session["SUBSERIALS"] = null;
            Session["SERID"] = null;
            Session["COST"] = null;
            Session["SPLLIT_TYPE"] = null;

            _SubSerialList = null;

            lblStus.Text = string.Empty;
            lblFree.Text = string.Empty;
            lblRes.Text = string.Empty;
            ddlsplittype.Enabled = true;
            lblstustext.Text = string.Empty;

            string _mytype = Request.QueryString["TYPE"];

            if (_mytype == "K")
            {
                if (ddlsplittype.Items.FindByValue("K") == null)
                {
                    ddlsplittype.Items.Insert(3, new ListItem("Cannibalize", "K"));
                }
                ddlsplittype.SelectedIndex = 3;
                ddlsplittype.Enabled = false;
            }
            else
            {
                ddlsplittype.Enabled = true;
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

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MainItem:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SerialAvb:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + txtItem.Text.ToUpper() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.SerialByItem:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + txtItem.Text.Trim());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocNo:
                    {
                        string _mytype = Request.QueryString["TYPE"];
                        if (_mytype == "K")
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "CANB" + seperator);
                            break;
                        }
                        else
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "SPLT" + seperator);
                            break;
                        }
                    }
                case CommonUIDefiniton.SearchUserControlType.Technician:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator);
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
                if (lblvalue.Text == "3")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = CHNLSVC.CommonSearch.SearchMainItemsComp(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "3";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }

                else if (lblvalue.Text == "309")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SerialAvb);
                    DataTable result = CHNLSVC.CommonSearch.GetAvailableSeriaSearchDataWeb(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "309";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }

                else if (lblvalue.Text == "312")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MainItem);
                    DataTable result = CHNLSVC.CommonSearch.SearchMainItemsDataSplit(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text.ToUpper());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "312";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "458")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SerialByItem);
                    DataTable result = CHNLSVC.CommonSearch.SearchMainItemsDataSer(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "458";
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

        private void LoadItemDetails()
        {
            if (txtItem.Text != "")
            {
                FF.BusinessObjects.MasterItem _item = (FF.BusinessObjects.MasterItem)CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper());
                if (_item != null && _item.Mi_is_ser1 == -1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Item is not allowed for splitting !!!')", true);
                    txtItem.Text = "";
                    txtItem.Focus();
                    txtItemDesn.Text = "";
                    txtModel.Text = "";
                    txtBrand.Text = "";
                    return;
                }
                if (_item != null && _item.Mi_itm_tp != "M")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Item is not allowed for splitting !!!')", true);
                    txtItem.Text = "";
                    txtItem.Focus();
                    txtItemDesn.Text = "";
                    txtModel.Text = "";
                    txtBrand.Text = "";
                    return;
                }
                if (_item != null)
                {
                    txtItemDesn.Text = _item.Mi_shortdesc;
                    txtItemDesn.ToolTip = _item.Mi_shortdesc;
                    txtBrand.Text = _item.Mi_brand;
                    txtModel.Text = _item.Mi_model;
                    List<InventorySerialN> _inrSer = CHNLSVC.Inventory.Get_INR_SER_DATA(new InventorySerialN()
                    {
                        Ins_itm_cd = string.IsNullOrEmpty(txtItem.Text) ? null : txtItem.Text.ToUpper(),
                        Ins_ser_1 = string.IsNullOrEmpty(txtSer.Text) ? null : txtSer.Text,
                        Ins_available = 1
                    });
                    if (_inrSer == null)
                    {

                        txtItem.Text = "";
                        txtItem.Focus();
                        txtItemDesn.Text = "";
                        txtModel.Text = "";
                        txtBrand.Text = "";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No serial available !!!')", true);
                        return;
                    }
                    else if (_inrSer.Count < 1)
                    {
                        txtItem.Text = "";
                        txtItem.Focus();
                        txtItemDesn.Text = "";
                        txtModel.Text = "";
                        txtBrand.Text = "";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No serial available !!!')", true);
                        return;
                    }
                    Session["SERID"] = _inrSer[0].Ins_ser_id;
                    List<InventorySerialMaster> _invList = CHNLSVC.Inventory.GetSerialMasterData(new InventorySerialMaster()
                    {
                        Irsm_com = Session["UserCompanyCode"].ToString(),
                        Irsm_loc = Session["UserDefLoca"].ToString(),
                        Irsm_itm_cd = _item.Mi_cd,
                        Irsm_ser_id = Convert.ToInt32(Session["SERID"])
                    });
                    if (_invList.Count > 0)
                    {
                        txtWarr.Text = _invList[0].Irsm_warr_no.ToString();
                        lblStus.Text = _invList[0].Irsm_itm_stus.ToString();
                        DataTable dtstatustx = CHNLSVC.Sales.GetItemStatusTxt(lblStus.Text.Trim());
                        if (dtstatustx.Rows.Count > 0)
                        {
                            foreach (DataRow ddr2 in dtstatustx.Rows)
                            {
                                lblstustext.Text = ddr2[0].ToString();
                            }
                        }

                        List<InventoryLocation> _StockBal = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text, lblStus.Text);

                        decimal _freeQty = 0;
                        decimal _resQty = 0;
                        if (_StockBal != null)
                        {
                            if (_StockBal.Count > 0)
                            {
                                string freeval = Convert.ToString(_StockBal[0].Inl_free_qty);
                                _freeQty = Convert.ToDecimal(freeval);
                                string resval = Convert.ToString(_StockBal[0].Inl_res_qty);
                                _resQty = Convert.ToDecimal(resval);
                            }
                        }
                        lblFree.Text = DoFormat(_freeQty);
                        lblRes.Text = DoFormat(_resQty);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid item code !!!')", true);
                    txtItem.Text = "";
                    txtItem.Focus();
                    txtItemDesn.Text = "";
                    txtModel.Text = "";
                    txtBrand.Text = "";
                    return;
                }
            }
        }
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lblvalue.Text == "3")
                {
                    txtItem.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtItem_TextChanged(null, null);

                }
                else if (lblvalue.Text == "309")
                {
                    txtSer.Text = grdResult.SelectedRow.Cells[1].Text;

                    txtSer_TextChanged(null, null);
                }
                else if (lblvalue.Text == "312")
                {
                    string itemCd = grdResult.SelectedRow.Cells[1].Text;
                    txtItem.Text = itemCd;
                    txtItem_TextChanged(null, null);

                }
                else if (lblvalue.Text == "458")
                {
                    txtSer.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtSer_TextChanged(null, null);
                    //Previously Loaded Bin Code
                }
                else if (lblvalue.Text == "TECH")
                {
                    txtTech.Text = grdResult.SelectedRow.Cells[1].Text;
                    lbltechName.Text = grdResult.SelectedRow.Cells[2].Text;
                    txtTech_TextChanged(null, null);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void LoadStatusAndLocation()
        {
            try
            {
                DataTable dt = CHNLSVC.Inventory.LoadLocationAndStatusMainItem(txtItem.Text.Trim(), Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                foreach (GridViewRow myrow in dgvSelect.Rows)
                {
                    Label lblstus = (Label)myrow.FindControl("lblstus");
                    Label lblcurrstusvalue = (Label)myrow.FindControl("lblcurrstusvalue");
                    Label lblcurstusval = (Label)myrow.FindControl("lblcurstusval");
                    LinkButton lbtnstus = (LinkButton)myrow.FindControl("lbtnstus");
                    Label lbtnstusdesc = (Label)myrow.FindControl("lbtnstusdesc");
                    Label lblloc = (Label)myrow.FindControl("lblloc");

                    string status = string.Empty;
                    string location = string.Empty;

                    foreach (DataRow ddritem in dt.Rows)
                    {
                        status = ddritem["ins_itm_stus"].ToString();
                        location = ddritem["ins_loc"].ToString();
                    }

                    DataTable dtstatustx = CHNLSVC.Sales.GetItemStatusTxt(status);

                    if (dtstatustx.Rows.Count > 0)
                    {
                        foreach (DataRow ddr2 in dtstatustx.Rows)
                        {
                            //lblcurstusval.Text = ddr2[0].ToString();
                            //lbtnstusdesc.Text = ddr2[0].ToString();
                        }
                    }

                    // lblstus.Text = status;
                    //lblcurrstusvalue.Text = status;
                    // lbtnstus.Text = status;
                    //lblloc.Text = location;
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

        private void LoadSerialData()
        {
            lblFree.Text = "";
            lblRes.Text = "";
            DataTable _dtSer = new DataTable();

            if (txtItem.Text == "")
            {
                _dtSer = CHNLSVC.Inventory.GetSerialDetailsBySerialwithoutItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtSer.Text);
                if (_dtSer.Rows.Count > 1)
                {
                    dgvItem.DataSource = new DataTable();
                    dgvItem.DataSource = _dtSer;
                    dgvItem.DataBind();

                    Divloc.Visible = false;
                    dvitm.Visible = true;
                    divstus.Visible = false;

                    lblcaption.Text = "Select Item Code";
                    mpexcel.Show();
                }
                else if (_dtSer.Rows.Count == 1)
                {
                    txtItem.Text = _dtSer.Rows[0]["ins_itm_cd"].ToString();

                    LoadItemDetails();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a valid serial !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    txtSer.Text = "";
                    txtSer.Focus();
                    return;
                }
            }

            if (txtItem.Text != "")
            {
                if (ddlsplittype.SelectedValue == "M")
                {
                    MasterItem _item = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text);
                    if (_item.Mi_itm_tp == "M")
                    {
                        if (!string.IsNullOrEmpty(txtSer.Text))
                        {
                            if (!string.IsNullOrEmpty(txtItem.Text.Trim()))
                            {
                                _dtSer = CHNLSVC.Inventory.GetSerialDetailsBySerial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text, txtSer.Text);
                            }

                            if (_dtSer.Rows.Count > 0)
                            {
                                _serId = Convert.ToInt32(_dtSer.Rows[0]["ins_ser_id"].ToString());
                                Session["SERID"] = _serId;
                                lblStus.Text = _dtSer.Rows[0]["ins_itm_stus"].ToString();
                                txtWarr.Text = _dtSer.Rows[0]["ins_warr_no"].ToString();

                                DataTable dtstatustx = CHNLSVC.Sales.GetItemStatusTxt(lblStus.Text.Trim());

                                if (dtstatustx.Rows.Count > 0)
                                {
                                    foreach (DataRow ddr2 in dtstatustx.Rows)
                                    {
                                        lblstustext.Text = ddr2[0].ToString();
                                    }
                                }

                                if (ddlsplittype.SelectedValue == "M")
                                {
                                    LoadSubSerialWeb();
                                }
                                else
                                {
                                    LoadSubSerial();
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid serial !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                                txtSer.Text = "";
                                txtSer.Focus();
                                return;
                            }

                            List<InventoryLocation> _StockBal = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text, lblStus.Text);

                            string freeval = Convert.ToString(_StockBal[0].Inl_free_qty);
                            Decimal free = Convert.ToDecimal(freeval);
                            lblFree.Text = DoFormat(free);

                            string resval = Convert.ToString(_StockBal[0].Inl_res_qty);
                            Decimal res = Convert.ToDecimal(resval);
                            lblRes.Text = DoFormat(res);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Item is not allowed for splitting !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        txtItem.Text = "";
                        txtItem.Focus();
                        return;
                    }
                }
                else
                {
                    MasterItem _item = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text);
                    if (_item.Mi_is_ser1 == 1 && _item.Mi_itm_tp == "M" && _item.Mi_is_subitem == true)
                    {

                        if (!string.IsNullOrEmpty(txtSer.Text))
                        {

                            if (!string.IsNullOrEmpty(txtItem.Text.Trim()))
                            {
                                _dtSer = CHNLSVC.Inventory.GetSerialDetailsBySerial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text, txtSer.Text);
                            }

                            if (_dtSer.Rows.Count > 0)
                            {
                                _serId = Convert.ToInt32(_dtSer.Rows[0]["ins_ser_id"].ToString());
                                Session["SERID"] = _serId;
                                lblStus.Text = _dtSer.Rows[0]["ins_itm_stus"].ToString();
                                txtWarr.Text = _dtSer.Rows[0]["ins_warr_no"].ToString();

                                DataTable dtstatustx = CHNLSVC.Sales.GetItemStatusTxt(lblStus.Text.Trim());

                                if (dtstatustx.Rows.Count > 0)
                                {
                                    foreach (DataRow ddr2 in dtstatustx.Rows)
                                    {
                                        lblstustext.Text = ddr2[0].ToString();
                                    }
                                }

                                LoadSubSerial();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid serial !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                                txtSer.Text = "";
                                txtSer.Focus();
                                return;
                            }

                            List<InventoryLocation> _StockBal = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text, lblStus.Text);

                            string freeval = Convert.ToString(_StockBal[0].Inl_free_qty);
                            Decimal free = Convert.ToDecimal(freeval);
                            lblFree.Text = DoFormat(free);


                            string resval = Convert.ToString(_StockBal[0].Inl_res_qty);
                            Decimal res = Convert.ToDecimal(resval);
                            lblRes.Text = DoFormat(res);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Item is not allowed for splitting !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        txtItem.Text = "";
                        txtItem.Focus();
                        return;
                    }
                }

                //  LoadStatusDescriptions();

                // LoadToolTips();
            }
        }

        private void LoadStatusDescriptions()
        {
            try
            {
                foreach (GridViewRow item in dgvSelect.Rows)
                {
                    Label lblstus = (Label)item.FindControl("lblstus");

                    Label lblcurstusval = (Label)item.FindControl("lblcurstusval");

                    DataTable dtstatustx = CHNLSVC.Sales.GetItemStatusTxt(lblstus.Text.Trim());

                    if (dtstatustx.Rows.Count > 0)
                    {
                        foreach (DataRow ddr2 in dtstatustx.Rows)
                        {
                            lblcurstusval.Text = ddr2[0].ToString();
                        }
                    }
                }

                foreach (GridViewRow item in dgvSelect.Rows)
                {
                    LinkButton lblstus = (LinkButton)item.FindControl("lbtnstus");

                    Label lblcurstusval = (Label)item.FindControl("lbtnstusdesc");

                    DataTable dtstatustx = CHNLSVC.Sales.GetItemStatusTxt(lblstus.Text.Trim());

                    if (dtstatustx.Rows.Count > 0)
                    {
                        foreach (DataRow ddr2 in dtstatustx.Rows)
                        {
                            lblcurstusval.Text = ddr2[0].ToString();
                        }
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



        private void LoadSubSerial()
        {
            //Int32 sessionserid = (Int32)Session["SERID"];
            //_serId = sessionserid;
            //_SubSerialList = CHNLSVC.Inventory.GetAvailablesubSerils(_serId);
            //if (_SubSerialList!=null)
            //{
            //   foreach (var item in _SubSerialList)
            //    {
            //        item.Irsms_itm_ch_stus = item.Irsms_itm_stus;
            //    } 
            //}

            //if (_SubSerialList != null)
            //{
            //    foreach (InventorySubSerialMaster _ser in _SubSerialList)
            //    {
            //        _ser.Irsms_loc_chg = Session["UserDefLoca"].ToString();
            //    }
            //    dgvSelect.AutoGenerateColumns = false;
            //    dgvSelect.DataSource = new List<InventorySubSerialMaster>();
            //    dgvSelect.DataSource = _SubSerialList;
            //    dgvSelect.DataBind();
            //    Session["SUBSERIALS"] = _SubSerialList;
            //}

            //DataTable dtbin = CHNLSVC.Inventory.LoadBinCodeItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtSer.Text.Trim());

            //string bin = string.Empty;

            //foreach (DataRow ddr in dtbin.Rows)
            //{
            //    bin = ddr["ins_bin"].ToString();
            //}

            //foreach (GridViewRow Myitem in dgvSelect.Rows)
            //{
            //    Label lblbin = (Label)Myitem.FindControl("lblbin");
            //    lblbin.Text = bin;
            //}
        }

        private void LoadSubSerialComponents()
        {
            _SubSerialList = CHNLSVC.Inventory.LoadItemCompoData(txtItem.Text.Trim());
            if (_SubSerialList != null)
            {
                foreach (InventorySubSerialMaster _ser in _SubSerialList)
                {
                    _ser.Irsms_loc_chg = _ser.Irsms_loc;
                    _ser.Irsms_itm_ch_stus = _ser.Irsms_itm_stus;
                }
                dgvSelect.AutoGenerateColumns = false;
                dgvSelect.DataSource = new List<InventorySubSerialMaster>();
                dgvSelect.DataSource = _SubSerialList;
                dgvSelect.DataBind();
                Session["SUBSERIALS"] = _SubSerialList;
                DesableSerialTextBox();
            }
        }

        private void LoadCaniberlize()
        {
            dgvSelect.DataSource = new int[] { };
            _kitComs = new List<ItemKitComponent>();
            _kitCom = new ItemKitComponent() { MIKC_ITM_CODE_MAIN = txtItem.Text.ToUpper(), MIKC_ACTIVE = 1 };
            _kitComs = CHNLSVC.Inventory.GetItemKitComponentSplit(_kitCom);
            if (_kitComs != null)
            {

                foreach (ItemKitComponent _com in _kitComs)
                {
                    _com.MIKC_STATUS = "GOOD";
                    _com.MIKC_STATUS_CD = "GOD";
                    _com.MIKC_LOC = Session["UserDefLoca"].ToString();
                    //_com.MIKC_LINE = line;
                    _mstLoc = CHNLSVC.General.GetMasterLocation(new MasterLocationNew()
                    {
                        Ml_com_cd = Session["UserCompanyCode"].ToString(),
                        Ml_loc_cd = Session["UserDefLoca"].ToString(),
                        Ml_act = 1
                    });
                    if (_mstLoc != null)
                    {
                        _com.MIKC_LOC_DES = _mstLoc.Ml_loc_desc;
                    }
                    _mstItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _com.MIKC_ITM_CODE_COMPONENT);
                    if (_mstItm != null)
                    {
                        if (_mstItm.Mi_is_ser1 == 1)
                        {
                            _com.MIKC_IS_SERIAL = _mstItm.Mi_is_ser1;
                        }
                    }

                }
                List<ItemKitComponent> _tempKitComs = new List<ItemKitComponent>();

                //sUB SER
                List<InventorySubSerialMaster> _listSubSer = new List<InventorySubSerialMaster>();
                List<InventorySerialN> _invSerMasterList = CHNLSVC.Inventory.Get_INR_SER_DATA(new InventorySerialN() { Ins_ser_1 = txtSer.Text, Ins_itm_cd = txtItem.Text, Ins_available = 1 });
                if (_invSerMasterList != null)
                {
                    if (_invSerMasterList.Count > 0)
                    {

                        _listSubSer = CHNLSVC.Inventory.GET_INR_SERMSTSUB(new InventorySubSerialMaster() { Irsms_ser_id = _invSerMasterList[0].Ins_ser_id });
                    }
                }

                foreach (var item in _kitComs)
                {
                    foreach (var subSer in _listSubSer)
                    {
                        if (item.MIKC_ITM_CODE_COMPONENT == subSer.Irsms_itm_cd)
                        {
                            item.MIKC_SERIAL_NO = subSer.Irsms_sub_ser;
                            break;
                        }
                    }

                }
                foreach (var _com in _kitComs)
                {
                    // _com.MIKC_LINE = _tempKitComs == null ? 0 : _tempKitComs.Count == 0 ? 0 : (_tempKitComs.Max(c => c.MIKC_LINE) + 1);
                    _mstItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _com.MIKC_ITM_CODE_COMPONENT);
                    //_mstItm = CHNLSVC.General.GetItemMaster(_com.MIKC_ITM_CODE_COMPONENT);
                    Int32 maxLine = 0;
                    if (_mstItm != null)
                    {
                        Int32 _noOfUnit = Decimal.ToInt32(_com.MIKC_NO_OF_UNIT);
                        if (_noOfUnit > 0 && _mstItm.Mi_is_ser1 == 1)
                        {
                            while (_noOfUnit > 0)
                            {
                                maxLine = _tempKitComs == null ? 0 : _tempKitComs.Count == 0 ? 0 : (_tempKitComs.Max(c => c.MIKC_LINE));
                                if (_com.MIKC_IS_SERIAL == 1)
                                {
                                    _com.MIKC_NO_OF_UNIT = 1;
                                }
                                _kitCom = new ItemKitComponent();
                                _kitCom.MIKC_ITM_CODE_MAIN = _com.MIKC_ITM_CODE_MAIN;
                                _kitCom.MIKC_ITM_CODE_COMPONENT = _com.MIKC_ITM_CODE_COMPONENT;
                                _kitCom.MIKC_DESC_COMPONENT = _com.MIKC_DESC_COMPONENT;
                                _kitCom.MIKC_NO_OF_UNIT = _com.MIKC_NO_OF_UNIT;
                                _kitCom.MIKC_CREATE_BY = _com.MIKC_CREATE_BY;
                                _kitCom.MIKC_CREATE_WHEN = _com.MIKC_CREATE_WHEN;
                                _kitCom.MIKC_LAST_MODIFY_BY = _com.MIKC_LAST_MODIFY_BY;
                                _kitCom.MIKC_LAST_MODIFY_WHEN = _com.MIKC_LAST_MODIFY_WHEN;
                                _kitCom.MIKC_COST = _com.MIKC_COST;
                                _kitCom.MIKC_SEQ_NO = _com.MIKC_SEQ_NO;
                                _kitCom.MIKC_ITM_TYPE = _com.MIKC_ITM_TYPE;
                                _kitCom.MIKC_ACTIVE = _com.MIKC_ACTIVE;
                                _kitCom.MIKC_ITEM_CATE = _com.MIKC_ITEM_CATE;
                                _kitCom.MIKC_COST_METHOD = _com.MIKC_COST_METHOD;
                                _kitCom.MIKC_CHG_MAIN_SERIAL = _com.MIKC_CHG_MAIN_SERIAL;
                                _kitCom.MIKC_UOM = _com.MIKC_UOM;
                                _kitCom.MIKC_ISSCAN = _com.MIKC_ISSCAN;
                                _kitCom.MIKC_SCAN_SEQ = _com.MIKC_SCAN_SEQ;
                                _kitCom.MIKC_TP = _com.MIKC_TP;
                                _kitCom.MIKC_STATUS = _com.MIKC_STATUS;
                                _kitCom.MIKC_STATUS_CD = _com.MIKC_STATUS_CD;
                                _kitCom.MIKC_LOC = _com.MIKC_LOC;
                                _kitCom.MIKC_IS_SERIAL = _com.MIKC_IS_SERIAL;
                                _kitCom.MIKC_SERIAL_NO = _com.MIKC_SERIAL_NO;
                                _kitCom.MIKC_ALLOW_EDIT = _com.MIKC_ALLOW_EDIT;
                                if (_kitCom.MIKC_ITM_TYPE == "M")
                                {
                                    _kitCom.MIKC_SERIAL_NO = txtSer.Text.Trim();
                                }
                                _kitCom.MIKC_LINE = maxLine + 1;
                                _tempKitComs.Add(_kitCom);
                                _noOfUnit--;
                            }
                        }
                        else
                        {
                            maxLine = _tempKitComs == null ? 0 : _tempKitComs.Count == 0 ? 0 : (_tempKitComs.Max(c => c.MIKC_LINE));
                            _com.MIKC_LINE = maxLine + 1;
                            _kitCom.MIKC_SERIAL_NO = _com.MIKC_SERIAL_NO;
                            if (_com.MIKC_ITM_TYPE == "M")
                            {
                                _com.MIKC_SERIAL_NO = txtSer.Text.Trim();
                            }
                            _tempKitComs.Add(_com);
                        }
                    }
                }
                if (_tempKitComs != null)
                {
                    _kitComs = _tempKitComs.OrderByDescending(c => c.MIKC_ITM_CODE_COMPONENT).ToList(); ;
                }
                hdfCostTp.Value = "Cost";
                if (_kitComs.Count > 0)
                {
                    hdfCostTp.Value = _kitComs[0].MIKC_COST_METHOD == "PER" ? "Cost (%)" : _kitComs[0].MIKC_COST_METHOD == "AMT" ? "Cost (AMT)" : "Cost";
                }
                dgvSelect.DataSource = _kitComs;
            }
            dgvSelect.DataBind();
            DesableSerialTextBox();
            Label lclCostHed1 = (Label)dgvSelect.HeaderRow.FindControl("lclCostHed");
            lclCostHed1.Text = hdfCostTp.Value;
        }
        private void LoadSplit()
        {
            dgvSelect.DataSource = new int[] { };
            _kitComs = new List<ItemKitComponent>();
            _itmCom = new MasterItemComponent()
            {
                Micp_itm_cd = txtItem.Text.ToUpper(),
                Micp_act = true,
                Mci_com = Session["UserCompanyCode"].ToString(),
                Mi_is_scansub = 0,
                Mi_itm_tp = "M",
                Mi_act = 1
            };
            _itmComs = CHNLSVC.Inventory.Get_MST_ITM_COMPONENT(_itmCom);
            if (_itmComs != null)
            {
                foreach (MasterItemComponent _mic in _itmComs)
                {
                    _kitCom = new ItemKitComponent();
                    _kitCom.MIKC_ITM_CODE_MAIN = _mic.Micp_itm_cd;
                    _kitCom.MIKC_ITM_CODE_COMPONENT = _mic.Micp_comp_itm_cd;
                    MasterItem _mstItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper());
                    _kitCom.MIKC_DESC_COMPONENT = _mstItm != null ? _mstItm.Mi_shortdesc : "";
                    _kitCom.MIKC_NO_OF_UNIT = _mic.Micp_qty;
                    _kitCom.MIKC_CREATE_BY = Session["UserID"].ToString();
                    _kitCom.MIKC_CREATE_WHEN = DateTime.Now;
                    _kitCom.MIKC_LAST_MODIFY_BY = Session["UserID"].ToString();
                    _kitCom.MIKC_LAST_MODIFY_WHEN = DateTime.Now;
                    _kitCom.MIKC_COST = _mic.Micp_cost_percentage;
                    // _kitCom.MIKC_SEQ_NO = "";
                    _kitCom.MIKC_ITM_TYPE = _mic.Micp_itm_tp;
                    _kitCom.MIKC_ACTIVE = Convert.ToInt32(_mic.Micp_act);
                    _kitCom.MIKC_ITEM_CATE = _mic.Micp_cate;
                    _kitCom.MIKC_COST_METHOD = _mic.MICP_IS_PERCENTAGE == 1 ? "PER" : "AMT";
                    //_kitCom.MIKC_CHG_MAIN_SERIAL = "";
                    // _kitCom.MIKC_UOM = "";
                    _kitCom.MIKC_ISSCAN = Convert.ToInt32(_mic.Micp_must_scan);
                    //_kitCom.MIKC_SCAN_SEQ = "";
                    //_kitCom.MIKC_TP = "";
                    _kitComs.Add(_kitCom);
                }
            }

            if (_kitComs != null)
            {

                foreach (ItemKitComponent _com in _kitComs)
                {
                    _com.MIKC_STATUS = "GOOD";
                    _com.MIKC_STATUS_CD = "GOD";
                    _com.MIKC_LOC = Session["UserDefLoca"].ToString();
                    //_com.MIKC_LINE = line;
                    _mstLoc = CHNLSVC.General.GetMasterLocation(new MasterLocationNew()
                    {
                        Ml_com_cd = Session["UserCompanyCode"].ToString(),
                        Ml_loc_cd = Session["UserDefLoca"].ToString(),
                        Ml_act = 1
                    });
                    if (_mstLoc != null)
                    {
                        _com.MIKC_LOC_DES = _mstLoc.Ml_loc_desc;
                    }
                    _mstItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _com.MIKC_ITM_CODE_COMPONENT);
                    if (_mstItm != null)
                    {
                        if (_mstItm.Mi_is_ser1 == 1)
                        {
                            _com.MIKC_IS_SERIAL = _mstItm.Mi_is_ser1;
                        }
                    }
                }
                #region Attach Sub serial
                List<InventorySubSerialMaster> _listSubSer = new List<InventorySubSerialMaster>();
                List<InventorySerialN> _invSerMasterList = CHNLSVC.Inventory.Get_INR_SER_DATA(new InventorySerialN() { Ins_ser_1 = txtSer.Text, Ins_com = Session["UserCompanyCode"].ToString(), Ins_loc = Session["UserDefLoca"].ToString(), Ins_available = 1 });
                if (_invSerMasterList != null)
                {
                    if (_invSerMasterList.Count > 0)
                    {

                        _listSubSer = CHNLSVC.Inventory.GET_INR_SERMSTSUB(new InventorySubSerialMaster() { Irsms_ser_id = _invSerMasterList[0].Ins_ser_id });
                    }
                }

                foreach (var item in _kitComs)
                {
                    foreach (var subSer in _listSubSer)
                    {
                        if (item.MIKC_ITM_CODE_COMPONENT == subSer.Irsms_itm_cd)
                        {
                            item.MIKC_SERIAL_NO = subSer.Irsms_sub_ser;
                        }
                    }

                }
                #endregion
                List<ItemKitComponent> _tempKitComs = new List<ItemKitComponent>();
                foreach (var _com in _kitComs)
                {
                    // _com.MIKC_LINE = _tempKitComs == null ? 0 : _tempKitComs.Count == 0 ? 0 : (_tempKitComs.Max(c => c.MIKC_LINE) + 1);
                    _mstItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _com.MIKC_ITM_CODE_COMPONENT);
                    //_mstItm = CHNLSVC.General.GetItemMaster(_com.MIKC_ITM_CODE_COMPONENT);
                    Int32 maxLine = 0;
                    if (_mstItm != null)
                    {
                        Int32 _noOfUnit = Decimal.ToInt32(_com.MIKC_NO_OF_UNIT);
                        if (_noOfUnit > 0 && _mstItm.Mi_is_ser1 == 1)
                        {
                            while (_noOfUnit > 0)
                            {
                                maxLine = _tempKitComs == null ? 0 : _tempKitComs.Count == 0 ? 0 : (_tempKitComs.Max(c => c.MIKC_LINE));
                                if (_com.MIKC_IS_SERIAL == 1)
                                {
                                    _com.MIKC_NO_OF_UNIT = 1;
                                }
                                _kitCom = new ItemKitComponent();
                                _kitCom.MIKC_ITM_CODE_MAIN = _com.MIKC_ITM_CODE_MAIN;
                                _kitCom.MIKC_ITM_CODE_COMPONENT = _com.MIKC_ITM_CODE_COMPONENT;
                                _kitCom.MIKC_DESC_COMPONENT = _com.MIKC_DESC_COMPONENT;
                                _kitCom.MIKC_NO_OF_UNIT = _com.MIKC_NO_OF_UNIT;
                                _kitCom.MIKC_CREATE_BY = _com.MIKC_CREATE_BY;
                                _kitCom.MIKC_CREATE_WHEN = _com.MIKC_CREATE_WHEN;
                                _kitCom.MIKC_LAST_MODIFY_BY = _com.MIKC_LAST_MODIFY_BY;
                                _kitCom.MIKC_LAST_MODIFY_WHEN = _com.MIKC_LAST_MODIFY_WHEN;
                                _kitCom.MIKC_COST = _com.MIKC_COST;
                                _kitCom.MIKC_SEQ_NO = _com.MIKC_SEQ_NO;
                                _kitCom.MIKC_ITM_TYPE = _com.MIKC_ITM_TYPE;
                                _kitCom.MIKC_ACTIVE = _com.MIKC_ACTIVE;
                                _kitCom.MIKC_ITEM_CATE = _com.MIKC_ITEM_CATE;
                                _kitCom.MIKC_COST_METHOD = _com.MIKC_COST_METHOD;
                                _kitCom.MIKC_CHG_MAIN_SERIAL = _com.MIKC_CHG_MAIN_SERIAL;
                                _kitCom.MIKC_UOM = _com.MIKC_UOM;
                                _kitCom.MIKC_ISSCAN = _com.MIKC_ISSCAN;
                                _kitCom.MIKC_SCAN_SEQ = _com.MIKC_SCAN_SEQ;
                                _kitCom.MIKC_TP = _com.MIKC_TP;
                                _kitCom.MIKC_STATUS = _com.MIKC_STATUS;
                                _kitCom.MIKC_STATUS_CD = _com.MIKC_STATUS_CD;
                                _kitCom.MIKC_LOC = _com.MIKC_LOC;
                                _kitCom.MIKC_IS_SERIAL = _com.MIKC_IS_SERIAL;
                                _kitCom.MIKC_SERIAL_NO = _com.MIKC_SERIAL_NO;
                                if (_kitCom.MIKC_ITM_TYPE == "M")
                                {
                                    _kitCom.MIKC_SERIAL_NO = txtSer.Text.Trim();
                                }
                                _kitCom.MIKC_LINE = maxLine + 1;
                                _tempKitComs.Add(_kitCom);
                                _noOfUnit--;
                            }
                        }
                        else
                        {
                            maxLine = _tempKitComs == null ? 0 : _tempKitComs.Count == 0 ? 0 : (_tempKitComs.Max(c => c.MIKC_LINE));
                            _com.MIKC_LINE = maxLine + 1;
                            _tempKitComs.Add(_com);
                        }
                    }
                }
                if (_tempKitComs != null)
                {
                    _kitComs = _tempKitComs.OrderByDescending(c => c.MIKC_ITM_CODE_COMPONENT).ToList(); ;
                }
                hdfCostTp.Value = "Cost";
                if (_kitComs.Count > 0)
                {
                    hdfCostTp.Value = _kitComs[0].MIKC_COST_METHOD == "PER" ? "Cost (%)" : _kitComs[0].MIKC_COST_METHOD == "AMT" ? "Cost (AMT)" : "Cost";
                }
                dgvSelect.DataSource = _kitComs;
            }
            dgvSelect.DataBind();
            DesableSerialTextBox();
            Label lclCostHed1 = (Label)dgvSelect.HeaderRow.FindControl("lclCostHed");
            lclCostHed1.Text = hdfCostTp.Value;
        }
        private void LoadSubSerialWeb()
        {
            Int32 sessionseridweb = (Int32)Session["SERID"];
            _serId = sessionseridweb;
            _SubSerialList = CHNLSVC.Inventory.GetAvailablesubSerilsMainWeb(txtItem.Text.Trim());
            if (_SubSerialList != null)
            {
                foreach (InventorySubSerialMaster _serWeb in _SubSerialList)
                {
                    _serWeb.Irsms_loc_chg = _serWeb.Irsms_loc;
                    _serWeb.Irsms_itm_ch_stus = _serWeb.Irsms_itm_stus;
                }

                var distinctItems = _SubSerialList.GroupBy(x => x.Irsms_itm_cd).Select(y => y.First()).ToList();

                dgvSelect.AutoGenerateColumns = false;
                dgvSelect.DataSource = new List<InventorySubSerialMaster>();
                dgvSelect.DataSource = distinctItems;
                dgvSelect.DataBind();
                DesableSerialTextBox();
                Session["SUBSERIALS"] = distinctItems;
            }

            foreach (GridViewRow Myitem in dgvSelect.Rows)
            {
                string splittype = (string)Session["SPLLIT_TYPE"];

                Label ser = (Label)Myitem.FindControl("lblserial");
                LinkButton btn = (LinkButton)Myitem.FindControl("lbtngrdInvoiceDetailsEdit");

                if (splittype == "M")
                {
                    if (!string.IsNullOrEmpty(ser.Text))
                    {
                        btn.Visible = false;
                    }
                    else
                    {
                        btn.Visible = true;
                    }
                }
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

        public void BindUCtrlDDLData2(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                if (col.ColumnName != "Serial ID")
                {
                    this.ddlSearchbykey.Items.Add(col.ColumnName);
                }
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }
        protected void btnsrch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                string _mytype = Request.QueryString["TYPE"];
                ddlSerialized.SelectedIndex = 0;
                dgvSelect.EditIndex = -1;
                if (_mytype == "K")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MainItem);
                    DataTable result = CHNLSVC.CommonSearch.SearchMainItemsDataSplit(SearchParams, null, null);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "312";
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
                else
                {
                    if (ddlsplittype.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select split type !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        txtItem.Text = string.Empty;
                        ddlsplittype.Focus();
                        return;
                    }

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = CHNLSVC.CommonSearch.SearchMainItemsComp(SearchParams, null, null);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "3";
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
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

        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtSer.Text = "";
                ClearData();
                bool b2 = false;
                #region validate
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    txtSer.Text = "";
                    return;
                }
                if (ddlsplittype.SelectedIndex < 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select split type !!!')", true);
                    txtItem.Text = string.Empty;
                    ddlsplittype.Focus();
                    return;
                }
                _itmCom = new MasterItemComponent();
                _itmCom.Micp_itm_cd = txtItem.Text.ToUpper();
                _itmCom.Micp_act = true;
                _itmCom.Mci_com = Session["UserCompanyCode"].ToString();
                //_itmCom.Mi_is_scansub = 0;
                //_itmCom.Mi_itm_tp = "M";
                _itmCom.Mi_act = 1;
                string _pgType = Request.QueryString["TYPE"];
                if (_pgType == "S")
                {
                    _itmComs = CHNLSVC.Inventory.Get_MST_ITM_COMPONENT(_itmCom);
                    if (_itmComs != null)
                    {
                        var _vKit = _itmComs.Where(c => c.Micp_itm_tp == "M").ToList();
                        if (_vKit != null)
                        {
                            if (_vKit.Count > 1)
                            {
                                //DispMsg("Multiple main items found ! Please contact inventory dept !"); txtItem.Text = ""; txtSer.Text = ""; return;
                                //Comment as per the dharshana
                            }
                        }
                    }
                }
                if (_pgType == "K")
                {
                    _kitCom = new ItemKitComponent() { MIKC_ITM_CODE_MAIN = txtItem.Text.ToUpper(), MIKC_ACTIVE = 1 };
                    _kitComs = CHNLSVC.Inventory.GetItemKitComponentSplit(_kitCom);
                    if (_kitComs != null)
                    {
                        var _vKit = _kitComs.Where(c => c.MIKC_ITM_TYPE == "M").ToList();
                        if (_vKit != null)
                        {
                            if (_vKit.Count > 1)
                            {
                                // DispMsg("Multiple main items found ! Please contact inventory dept !"); txtItem.Text = ""; txtSer.Text = ""; return;
                                //Comment as per the dharshana
                            }
                        }
                    }

                    //load hdrdata
                    DataTable _cndata = CHNLSVC.Financial.GetCanibalizeHdrData(txtDocNo.Text.ToString());
                    if (_cndata != null && _cndata.Rows.Count>0)
                    {
                        txtTech.Text = _cndata.Rows[0]["ith_del_cd"].ToString();
                        lbltechName.Text = _cndata.Rows[0]["ith_del_party"].ToString();
                    }
                }

                if (ddlsplittype.SelectedValue == "M")
                {
                    string _mytype = Request.QueryString["TYPE"];
                    if (_mytype == "K")
                    {
                        DataTable dt = CHNLSVC.Inventory.CheckAbilityMainItemSplit(txtItem.Text.ToUpper().Trim());
                        if (dt.Rows.Count == 0)
                        {
                            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot split main item " + txtItem.Text.Trim() + " !!!')", true);
                            //txtItem.Text = string.Empty;
                            //return;
                        }
                    }
                    if (_mytype == "S")
                    {
                        _itmCom = new MasterItemComponent()
                        {
                            Micp_itm_cd = txtItem.Text.ToUpper(),
                            Micp_act = true,
                            Mci_com = Session["UserCompanyCode"].ToString(),
                            Mi_is_scansub = 0,
                            Mi_itm_tp = "M",
                            Mi_act = 1
                        };
                        _itmComs = CHNLSVC.Inventory.Get_MST_ITM_COMPONENT(_itmCom);
                        if (_itmComs == null)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot split main item " + txtItem.Text.Trim() + " !!!')", true);
                            txtItem.Text = string.Empty;
                            return;
                        }
                        else if (_itmComs.Count == 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot split main item " + txtItem.Text.Trim() + " !!!')", true);
                            txtItem.Text = string.Empty;
                            return;
                        }
                        else
                        {
                            if (txtItem.Text != "")
                            {
                                //LoadItemDetails();
                            }
                        }
                    }
                }
                #endregion

                #region LoadItemData
                MasterItem _mstItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper());
                if (_mstItm == null)
                {
                    txtItem.Text = "";
                    txtItem.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a valid main item code !!!')", true);
                    return;
                }
                if (_mstItm.Mi_is_ser1 == 1)
                {
                    List<InventorySerialN> _serMsters = CHNLSVC.Inventory.Get_INR_SER_DATA(new InventorySerialN()
                    {
                        Ins_com = Session["UserCompanyCode"].ToString(),
                        Ins_loc = Session["UserDefLoca"].ToString(),
                        Ins_itm_cd = string.IsNullOrEmpty(txtItem.Text) ? null : txtItem.Text.ToUpper(),
                        Ins_ser_1 = string.IsNullOrEmpty(txtSer.Text) ? null : txtSer.Text,
                        Ins_available = 1,
                    });
                    if (_serMsters != null)
                    {
                        if (_serMsters.Count > 1)
                        {
                            dgvResult.DataSource = new int[] { };
                            dgvResult.DataBind();
                            DataTable dt = new DataTable();
                            dt.Columns.Add("Item Code");
                            dt.Columns.Add("Description");
                            dt.Columns.Add("Model");
                            dt.Columns.Add("Brand");
                            dt.Columns.Add("Serial #");

                            foreach (InventorySerialN i in _serMsters)
                            {
                                DataRow _ravi = dt.NewRow();
                                MasterItem _item = CHNLSVC.General.GetItemMaster(i.Ins_itm_cd);
                                _ravi["Item Code"] = i.Ins_itm_cd;
                                _ravi["Description"] = _item.Mi_shortdesc;
                                _ravi["Model"] = _item.Mi_model;
                                _ravi["Brand"] = _item.Mi_brand;
                                _ravi["Serial #"] = i.Ins_ser_1;
                                bool contains = dt.AsEnumerable().Any(row => i.Ins_ser_1 == row.Field<String>("Serial #"));
                                if (!contains)
                                {
                                    dt.Rows.Add(_ravi);
                                }
                            }
                            if (dt.Rows.Count > 1)
                            {
                                dgvResult.DataSource = dt;
                                dgvResult.DataBind();
                                PopupSerial.Show();
                                return;
                            }
                            else
                            {
                                txtItem.Text = _serMsters[0].Ins_itm_cd;
                                txtSer.Text = string.IsNullOrEmpty(txtSer.Text) ? _serMsters[0].Ins_ser_1 : txtSer.Text;
                            }

                        }
                        if (_serMsters.Count == 1)
                        {
                            txtItem.Text = _serMsters[0].Ins_itm_cd;
                            txtSer.Text = string.IsNullOrEmpty(txtSer.Text) ? _serMsters[0].Ins_ser_1 : txtSer.Text;
                            txtSer_TextChanged(null, null);
                        }
                    }

                }
                #endregion


                string _mytype1 = Request.QueryString["TYPE"];
                if (_mytype1 == "K")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MainItem);
                    DataTable result = CHNLSVC.CommonSearch.SearchMainItemsDataSplitByItem(SearchParams, "Code", txtItem.Text.ToUpper().Trim());

                    if (result.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No serials available for this item for the location " + Session["UserDefLoca"].ToString() + " !!!')", true);
                        txtItem.Text = string.Empty;
                        return;
                    }
                    LoadItemDetails();
                }
                if (_mytype1 == "S")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MainItem);
                    DataTable result = CHNLSVC.CommonSearch.SearchMainItemsDataSplitByItem(SearchParams, "Code", txtItem.Text.ToUpper().Trim());

                    if (result.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No serials available for this item for the location " + Session["UserDefLoca"].ToString() + " !!!')", true);
                        txtItem.Text = string.Empty;
                        return;
                    }
                    LoadSplit();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void btnsrch_ser_Click(object sender, EventArgs e)
        {
            try
            {
                string _mytype = Request.QueryString["TYPE"];
                ddlSerialized.SelectedIndex = 0;
                dgvSelect.EditIndex = -1;
                if (_mytype == "K")
                {
                    //if (string.IsNullOrEmpty(txtItem.Text.Trim()))
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter item code !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                    //    txtItem.Focus();
                    //    return;
                    //}

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                    //List<InventorySerialMaster> _serMsters = CHNLSVC.Inventory.GetSerialMasterData(new InventorySerialMaster() { Irsm_com = Session["UserCompanyCode"].ToString(), Irsm_loc = Session["UserDefLoca"].ToString() });

                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SerialByItem);
                    DataTable result = CHNLSVC.CommonSearch.SearchMainItemsDataSer(SearchParams, null, null);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "458";
                    BindUCtrlDDLData2(result);
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
                else
                {
                    if (ddlsplittype.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select split type !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                        ddlsplittype.Focus();
                        txtSer.Text = string.Empty;
                        return;
                    }

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SerialAvb);
                    DataTable result = CHNLSVC.CommonSearch.GetAvailableSeriaSearchDataWeb(SearchParams, null, null);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "309";
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
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

        protected void gvitems_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string itemcode = (dgvItem.SelectedRow.FindControl("lblitemcodepopup") as Label).Text;
                txtItem.Text = itemcode;
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

        protected void txtSer_TextChanged(object sender, EventArgs e)
        {
            ClearData();
            txtItem.Text = "";
            if (string.IsNullOrEmpty(txtSer.Text))
            {
                return;
            }
            if (ddlsplittype.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select split type !!!')", true);
                txtSer.Text = string.Empty;
                ddlsplittype.Focus();
                return;
            }
            string _itemCode = "";
            List<InventorySerialN> _serMst = CHNLSVC.Inventory.Get_INR_SER_DATA(new InventorySerialN()
            {
                Ins_com = Session["UserCompanyCode"].ToString(),
                Ins_loc = Session["UserDefLoca"].ToString(),
                // Ins_itm_cd = _itemCode,
                Ins_ser_1 = txtSer.Text,
                Ins_available = 1,
            });
            if (_serMst == null)
            {
                txtSer.Text = "";
                txtSer.Focus();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a valid serial # !!!')", true); return;
            }
            else if (_serMst.Count == 0)
            {
                txtSer.Text = "";
                txtSer.Focus();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a valid serial # !!!')", true); return;
            }
            else if (_serMst.Count > 1)
            {
                dgvResult.DataSource = new int[] { };
                dgvResult.DataBind();
                DataTable dt = new DataTable();
                dt.Columns.Add("Item Code");
                dt.Columns.Add("Description");
                dt.Columns.Add("Model");
                dt.Columns.Add("Brand");
                dt.Columns.Add("Serial #");

                foreach (InventorySerialN i in _serMst)
                {
                    DataRow _ravi = dt.NewRow();
                    MasterItem _item = CHNLSVC.General.GetItemMaster(i.Ins_itm_cd);
                    _ravi["Item Code"] = i.Ins_itm_cd;
                    _ravi["Description"] = _item.Mi_shortdesc;
                    _ravi["Model"] = _item.Mi_model;
                    _ravi["Brand"] = _item.Mi_brand;
                    _ravi["Serial #"] = i.Ins_ser_1;
                    bool contains = dt.AsEnumerable().Any(row => i.Ins_itm_cd == row.Field<String>("Item Code"));
                    //  dt.Rows.Add(_ravi);
                    if (!contains) { dt.Rows.Add(_ravi); }
                }
                if (dt.Rows.Count > 1)
                {
                    dgvResult.DataSource = dt; dgvResult.DataBind(); PopupSerial.Show(); return;
                }
                else
                {
                    txtItem.Text = _serMst[0].Ins_itm_cd;
                    txtSer.Text = _serMst[0].Ins_ser_1;
                }
            }

            _itemCode = _serMst[0].Ins_itm_cd;
            MasterItem _mstItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itemCode);
            if (_mstItm == null)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a valid main item code !!!')", true);
                return;
            }
            string _pgType = Request.QueryString["TYPE"];
            if (_pgType == "S")
            {
                _itmCom = new MasterItemComponent();
                _itmCom.Micp_itm_cd = _mstItm.Mi_cd;
                _itmCom.Micp_act = true;
                _itmCom.Mci_com = Session["UserCompanyCode"].ToString();
                _itmCom.Mi_act = 1;
                _itmComs = CHNLSVC.Inventory.Get_MST_ITM_COMPONENT(_itmCom);
                if (_itmComs != null)
                {
                    var _vKit = _itmComs.Where(c => c.Micp_itm_tp == "M").ToList();
                    if (_vKit != null)
                    {
                        if (_vKit.Count > 1)
                        {
                            //DispMsg("Multiple main items found ! Please contact inventory dept !"); txtItem.Text = ""; txtSer.Text = ""; return;
                        }
                    }
                }
            }
            if (_pgType == "K")
            {
                _kitCom = new ItemKitComponent() { MIKC_ITM_CODE_MAIN = _mstItm.Mi_cd, MIKC_ACTIVE = 1 };
                _kitComs = CHNLSVC.Inventory.GetItemKitComponentSplit(_kitCom);
                if (_kitComs != null)
                {
                    var _vKit = _kitComs.Where(c => c.MIKC_ITM_TYPE == "M").ToList();
                    if (_vKit != null)
                    {
                        if (_vKit.Count > 1)
                        {
                            // DispMsg("Multiple main items found ! Please contact inventory dept !"); txtItem.Text = ""; txtSer.Text = ""; return;
                        }
                    }
                }
            }
            if (_mstItm.Mi_is_ser1 == 1)
            {
                List<InventorySerialN> _serMsters = CHNLSVC.Inventory.Get_INR_SER_DATA(new InventorySerialN()
                {
                    Ins_com = Session["UserCompanyCode"].ToString(),
                    Ins_loc = Session["UserDefLoca"].ToString(),
                    Ins_itm_cd = _itemCode,
                    Ins_ser_1 = txtSer.Text,
                    Ins_available = 1,
                });
                if (_serMsters != null)
                {
                    if (_serMsters.Count > 1)
                    {

                    }
                    if (_serMsters.Count == 1)
                    {
                        txtItem.Text = _serMsters[0].Ins_itm_cd;
                        txtSer.Text = _serMsters[0].Ins_ser_1;
                    }
                }
                if (ddlsplittype.SelectedValue == "M")
                {
                    DataTable dt = CHNLSVC.Inventory.CheckAbilityMainItemSplit(txtItem.Text.ToUpper().Trim());
                    if (dt.Rows.Count == 0)
                    {
                        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot split main item " + txtItem.Text.Trim() + " !!!')", true);
                        //txtItem.Text = string.Empty;
                        //return;
                    }
                    else
                    {
                        if (txtItem.Text != "")
                        {
                            //  LoadItemDetails();
                        }
                    }
                }
                string _mytype = Request.QueryString["TYPE"];
                if (_mytype == "K")
                {
                    LoadItemDetails();
                    LoadCaniberlize();
                }
                if (_mytype == "S")
                {
                    LoadItemDetails();
                    LoadSplit();
                }
            }
        }
        protected void dgvSelect_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlbincode = (e.Row.FindControl("ddlbincode") as DropDownList);
                    DataTable dtbin = CHNLSVC.Inventory.LoadBinCodeItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    if (dtbin.Rows.Count > 0)
                    {
                        ddlbincode.DataSource = dtbin;
                        ddlbincode.DataTextField = "ibn_bin_desc";
                        ddlbincode.DataValueField = "ibn_bin_cd";
                        ddlbincode.DataBind();
                    }
                    string status = (e.Row.FindControl("lblbincode") as Label).Text;
                    //ddlbincode.Items.FindByValue(status).Selected = true;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void gvLoc_SelectedIndexChangedOld(object sender, EventArgs e)
        {
            string selectedloccode = (gvLoc.SelectedRow.FindControl("lblloccode") as Label).Text;

            string pritm = (string)Session["ITM"];
            string prmfc = (string)Session["MFC"];
            string prstus = (string)Session["STUS"];
            string prser = (string)Session["SER"];
            string prloc = (string)Session["LOC"];
            string prline = (string)Session["LINE"];
            string prcost = (string)Session["COST"];

            foreach (GridViewRow myrow in dgvSelect.Rows)
            {
                Label lblitm = (Label)myrow.FindControl("lblitemcode");
                Label lblmfs = (Label)myrow.FindControl("lblmfc");
                Label lblstus = (Label)myrow.FindControl("lblstus");
                Label lblser = (Label)myrow.FindControl("lblserial");
                Label lblloc = (Label)myrow.FindControl("lblloc");
                LinkButton lblloc2 = (LinkButton)myrow.FindControl("lbtnshcngeloc");
                Label lblline = (Label)myrow.FindControl("irsms_ser_line");
                Label lblcost = (Label)myrow.FindControl("lblcost");

                if ((pritm == lblitm.Text) && (prmfc == lblmfs.Text) && (prstus == lblstus.Text) && (prser == lblser.Text) && (prline == lblline.Text) && (prcost == lblcost.Text))
                {
                    lblloc2.Text = selectedloccode;
                }
            }

            _selectedstatus = string.Empty;

            var list = new List<InventorySubSerialMaster>();

            list = (List<InventorySubSerialMaster>)Session["SUBSERIALS"];

            if (list != null)
            {
                _SubSerialList = list;
            }

            if (gvLoc.Rows.Count > 0)
            {
                if (dgvSelect.Rows.Count > 0)
                {
                    prline = (string)Session["LINE"];

                    Int32 selectedser = Convert.ToInt32(prline);

                    _selectedLoc = selectedloccode;
                    if (_SubSerialList != null && _SubSerialList.Count > 0)
                    {
                        _SubSerialList.Where(w => w.Irsms_ser_line == selectedser).ToList().ForEach(s => s.Irsms_loc_chg = _selectedLoc);
                        dgvSelect.AutoGenerateColumns = false;
                        dgvSelect.DataSource = new List<ReptPickSerials>();
                        dgvSelect.DataSource = _SubSerialList;
                        dgvSelect.DataBind();
                        DesableSerialTextBox();
                    }
                }
            }

            Session["SUBSERIALS"] = _SubSerialList;

            LoadStatusDescriptions();

            LoadToolTips();
            //LoadStatusAndLocation();
            mpexcel.Hide();
        }
        protected void gvLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string locCode = (gvLoc.SelectedRow.FindControl("lblloccode") as Label).Text;
                _mstLoc = CHNLSVC.General.GetMasterLocation(new MasterLocationNew()
                {
                    Ml_com_cd = Session["UserCompanyCode"].ToString(),
                    Ml_loc_cd = locCode,
                    Ml_act = 1
                });
                _kitComs.Where(c => c.MIKC_LINE == _lineNo).FirstOrDefault().MIKC_LOC = locCode;
                _kitComs.Where(c => c.MIKC_LINE == _lineNo).FirstOrDefault().MIKC_LOC_DES = _mstLoc != null ? _mstLoc.Ml_loc_desc : "";
                dgvSelect.DataSource = new int[] { };
                if (_kitComs.Count > 0)
                {
                    dgvSelect.DataSource = _kitComs;
                }
                dgvSelect.DataBind();
                DesableSerialTextBox();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void LoadToolTips()
        {
            try
            {
                foreach (GridViewRow item in dgvSelect.Rows)
                {
                    Label lblloc = (Label)item.FindControl("lblloc");
                    LinkButton lbtnshcngeloc = (LinkButton)item.FindControl("lbtnshcngeloc");

                    DataTable dtloc1 = CHNLSVC.General.LoadLocationDetailsByCode(Session["UserCompanyCode"].ToString(), lblloc.Text);
                    DataTable dtloc2 = CHNLSVC.General.LoadLocationDetailsByCode(Session["UserCompanyCode"].ToString(), lbtnshcngeloc.Text);

                    if (dtloc1.Rows.Count > 0)
                    {
                        foreach (DataRow ddr1 in dtloc1.Rows)
                        {
                            lblloc.ToolTip = ddr1[0].ToString();
                        }
                    }

                    if (dtloc2.Rows.Count > 0)
                    {
                        foreach (DataRow ddr2 in dtloc2.Rows)
                        {
                            lbtnshcngeloc.ToolTip = ddr2[0].ToString();
                        }
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtsaveconfirm.Value == "Yes")
                {
                    string _mytype = Request.QueryString["TYPE"];

                    if (_mytype == "K")
                    {
                        if (string.IsNullOrEmpty(txtSer.Text.Trim()))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the serial # !!!')", true);
                            txtSer.Focus();
                            return;
                        }
                    }

                    if (string.IsNullOrEmpty(txtTech.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the Technician !!!')", true);
                        return;
                    }

                    if (dgvSelect.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the main item !!!')", true);
                        txtItem.Focus();
                        return;
                    }
                    if (_kitComs.Count > 0)
                    {
                        bool err = false;
                        foreach (ItemKitComponent _com in _kitComs)
                        {
                            if (string.IsNullOrEmpty(_com.MIKC_SERIAL_NO))
                            {
                                _mstItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _com.MIKC_ITM_CODE_COMPONENT);
                                if (_mstItm != null)
                                {
                                    if (_mstItm.Mi_is_ser1 == 1)
                                    {
                                        err = true;
                                        break;
                                    }
                                    if (_mstItm.Mi_is_ser1 == 0)
                                    {
                                        _com.MIKC_SERIAL_NO = "N/A";
                                    }
                                }
                            }
                        }

                        if (err)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter serial # !!!')", true);
                            return;
                        }
                    }
                    else
                    {
                        foreach (GridViewRow ROW in dgvSelect.Rows)
                        {
                            Label lblitm = (Label)ROW.FindControl("lblitemcode");
                            Label lblser = (Label)ROW.FindControl("lblserial");

                            DataTable dtisavailable = CHNLSVC.Inventory.LoadItemsBySerial(lblitm.Text.Trim(), txtSer.Text.Trim());

                            string loc = string.Empty;
                            string isser = string.Empty;
                            foreach (DataRow ddre in dtisavailable.Rows)
                            {
                                loc = ddre["ins_loc"].ToString();
                                isser = ddre["mi_is_ser1"].ToString();

                                if ((isser != "0") && (isser != "-1"))
                                {
                                    if (dtisavailable.Rows.Count > 0)
                                    {
                                        string msg = "Serial " + lblser.Text.Trim() + " of the item " + lblitm.Text.Trim() + " already available at the location " + loc + "";
                                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    string catetype = string.Empty;

                    if (_mytype == "K")
                    {
                        catetype = "CANB";
                    }
                    else
                    {
                        catetype = "SPLT";
                    }


                    Int32 sessionserid = (Int32)Session["SERID"];

                    _serId = sessionserid;

                    bool _allowCurrentTrans = false;
                    IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserDefProf"].ToString(), "m_Trans_Service_Itemcannibalize", dtpFromDate, lblBackDateInfor, dtpFromDate.Text, out _allowCurrentTrans);

                    string wrongdaterange = (string)Session["WRONGDATERANGE"];
                    string allowcurdate = (string)Session["ALLOWCURDATE"];

                    if (wrongdaterange == "1")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Date range is not with in the required back date range !!!')", true);
                        return;
                    }

                    if (allowcurdate == "1")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You are not allowed to do process for the current date !!!')", true);
                        return;
                    }

                    //decimal _per = 0;
                    //if (_SubSerialList != null)
                    //{
                    //    foreach (InventorySubSerialMaster item in _SubSerialList)
                    //    {
                    //        _per = _per + item.Irsms_cost_per;
                    //    }
                    //}

                    //if (_per != 100)
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid Cost percentage !!!')", true);
                    //    return;
                    //}

                    List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();
                    List<InventoryHeader> inHeaderList = new List<InventoryHeader>();
                    string documntNo = "";
                    Int32 result = -99;
                    #region Fill InventoryHeader
                    InventoryHeader outHeader = new InventoryHeader();
                    DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    foreach (DataRow r in dt_location.Rows)
                    {
                        outHeader.Ith_sbu = (string)r["ML_OPE_CD"];
                        if (System.DBNull.Value != r["ML_CATE_2"])
                        {
                            outHeader.Ith_channel = (string)r["ML_CATE_2"];
                        }
                        else
                        {
                            outHeader.Ith_channel = string.Empty;
                        }
                    }

                    DateTime date = Convert.ToDateTime(dtpFromDate.Text);

                    outHeader.Ith_acc_no = "STOCK_ADJ";
                    outHeader.Ith_anal_1 = "";
                    outHeader.Ith_anal_2 = "";
                    outHeader.Ith_anal_3 = "";
                    outHeader.Ith_anal_4 = "";
                    outHeader.Ith_anal_5 = "";
                    outHeader.Ith_anal_6 = 0;
                    outHeader.Ith_anal_7 = 0;
                    outHeader.Ith_anal_8 = DateTime.MinValue;
                    outHeader.Ith_anal_9 = DateTime.MinValue;
                    outHeader.Ith_anal_10 = false;
                    outHeader.Ith_anal_11 = false;
                    outHeader.Ith_anal_12 = false;
                    outHeader.Ith_bus_entity = "";
                    outHeader.Ith_cate_tp = catetype;
                    outHeader.Ith_com = Session["UserCompanyCode"].ToString();
                    outHeader.Ith_com_docno = "";
                    outHeader.Ith_cre_by = Session["UserID"].ToString();
                    outHeader.Ith_cre_when = DateTime.Now;
                    outHeader.Ith_del_add1 = "";
                    outHeader.Ith_del_add2 = "";
                    outHeader.Ith_del_code = "";
                    outHeader.Ith_del_party = "";
                    outHeader.Ith_del_town = "";
                    outHeader.Ith_direct = false;
                    outHeader.Ith_doc_date = date.Date;
                    outHeader.Ith_doc_no = string.Empty;
                    outHeader.Ith_doc_tp = "ADJ";
                    outHeader.Ith_doc_year = date.Year;
                    outHeader.Ith_entry_no = "";
                    outHeader.Ith_entry_tp = catetype;
                    outHeader.Ith_git_close = true;
                    outHeader.Ith_git_close_date = DateTime.MinValue;
                    outHeader.Ith_git_close_doc = string.Empty;
                    outHeader.Ith_isprinted = false;
                    outHeader.Ith_is_manual = false;
                    outHeader.Ith_job_no = string.Empty;
                    outHeader.Ith_loading_point = string.Empty;
                    outHeader.Ith_loading_user = string.Empty;
                    outHeader.Ith_loc = Session["UserDefLoca"].ToString();
                    outHeader.Ith_manual_ref = txtManual.Text.Trim();
                    outHeader.Ith_mod_by = Session["UserID"].ToString();
                    outHeader.Ith_mod_when = DateTime.Now;
                    outHeader.Ith_noofcopies = 0;
                    outHeader.Ith_oth_loc = string.Empty;
                    outHeader.Ith_oth_docno = "N/A";
                    outHeader.Ith_remarks = txtRem.Text;
                    outHeader.Ith_session_id = Session["SessionID"].ToString();
                    outHeader.Ith_stus = "A";
                    outHeader.Ith_sub_tp = catetype;
                    outHeader.Ith_vehi_no = string.Empty;
                    outHeader.Ith_bus_entity = txtTech.Text;
                    outHeader.Ith_del_code = txtTech.Text.ToString();
                    outHeader.Ith_del_party = lbltechName.Text.ToString();

                    #endregion
                    MasterAutoNumber masterAuto = new MasterAutoNumber();
                    #region Fill MasterAutoNumber
                    masterAuto.Aut_cate_cd = Session["UserDefLoca"].ToString();
                    masterAuto.Aut_cate_tp = "LOC";
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "ADJ";
                    masterAuto.Aut_number = 5;
                    masterAuto.Aut_start_char = "ADJ";
                    masterAuto.Aut_year = null;
                    #endregion

                    ReptPickSerials _serDet1 = CHNLSVC.Inventory.GetReservedByserialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), null, txtItem.Text, _serId);

                    ReptPickSerials _serDet = CHNLSVC.Inventory.getSerialDet_INTSER(_serDet1.Tus_seq_no, _serDet1.Tus_itm_line, _serDet1.Tus_batch_line, _serDet1.Tus_ser_line);

                    if (_serDet == null)
                    {
                        string msg = "This serial is not available " + txtSer.Text + " !!!";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                        return;
                    }

                    reptPickSerialsList.Add(_serDet);
                    int kitItemsCount = 0;
                    foreach (ReptPickSerials item in reptPickSerialsList)
                    {
                        foreach (GridViewRow dgvSelectorw in dgvSelect.Rows)
                        {
                            DropDownList ddlbincode = (DropDownList)dgvSelectorw.FindControl("ddlbincode");
                            item.Tus_bin_to = ddlbincode.SelectedValue;
                            TextBox tbunits = (TextBox)dgvSelectorw.FindControl("txtnoofunitsid");
                            item.Tus_qty = Convert.ToDecimal(tbunits.Text);
                            _kitComs[kitItemsCount].MIKC_NO_OF_UNIT = item.Tus_qty;
                            kitItemsCount++;
                        }
                    }
                   

                    result = CHNLSVC.CustService.Save_ItemCanibalize_web(masterAuto, outHeader, reptPickSerialsList, _kitComs, "N", out documntNo);
                    string newMsg = "";
                    if (result != -99 && result >= 0)
                    {
                        string msg = documntNo;

                        newMsg = "Successfully Saved !!! " + documntNo;

                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + newMsg + "')", true);
                        Session["documntNo"] = documntNo;
                        Session["mainItem"] = txtItem.Text;
                        Session["mainser"] = txtSer.Text;
                        //Doc Print
                        lbtnPrint_Click(null, null);
                        Clear();
                        lblMssg.Text = "Do you want print now?";
                        PopupConfBox.Show();

                        //                        print(documntNo);
                        lblMssgBarcode.Text = "Do you want to print Barcode now";
                        popupBarcode.Show();
                    }
                    else
                    {
                        newMsg = "Error occurd ! " + documntNo;
                        newMsg = newMsg.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + newMsg + "')", true);
                        return;
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
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                print(Session["documntNo"].ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            PopupConfBox.Hide();
        }
        protected void print(string _docno)
        {
            try
            {
                Session["documntNo"] = _docno;
                Session["GlbReportType"] = "";
                Session["GlbReportName"] = "Item_Canibalise_Print.rpt";
                string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                PrintPDF(targetFileName);
                string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while printing !!!", "E");
                CHNLSVC.MsgPortal.SaveReportErrorLog("Item Splitting Print", "ItemSplitting", ex.Message, Session["UserID"].ToString());
            }

        }

        public void PrintPDF(string targetFileName)
        {
            try
            {
                clsInventory obj = new clsInventory();
                obj.ItemCanibalisePrint(Session["documntNo"].ToString());
                ReportDocument rptDoc = (ReportDocument)obj._itemcanib;
                DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                diskOpts.DiskFileName = targetFileName;
                rptDoc.ExportOptions.DestinationOptions = diskOpts;
                rptDoc.Export();

                rptDoc.Close();
                rptDoc.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected bool IsAllowBackDateForModule(string _com, string _loc, string _pc, string _filename, TextBox _dtpcontrol, Label _lblcontrol, string _date, out bool _allowCurrentDate)
        {
            BackDates _bdt = new BackDates();
            _dtpcontrol.Enabled = false;
            _allowCurrentDate = false;

            bool _isAllow = CHNLSVC.General.IsAllowBackDateForModule(_com, _loc, _pc, _filename.ToString(), _date.ToString(), out _bdt);
            if (_isAllow == true)
            {
                _dtpcontrol.Enabled = true;
                if (_bdt != null)
                {
                    _lblcontrol.Text = "Back dated [" + _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy") + " - " + _bdt.Gad_act_to_dt.Date.ToString("dd/MMM/yyyy") + "]. Valid from " + _bdt.Gad_from_dt + " to " + _bdt.Gad_to_dt + ".";
                    Information.Visible = true;
                    lbtndate.Visible = true;

                    DateTime Selecteddate = Convert.ToDateTime(dtpFromDate.Text.Trim());
                    DateTime appfromdate = Convert.ToDateTime(_bdt.Gad_act_from_dt);
                    DateTime apptodate = Convert.ToDateTime(_bdt.Gad_act_to_dt);

                    if (_bdt.Gad_alw_curr_trans == true)
                    {
                        if (Selecteddate >= appfromdate && Selecteddate <= apptodate)
                        {
                            Session["WRONGDATERANGE"] = "0";
                        }
                        else
                        {
                            Session["WRONGDATERANGE"] = "1";
                        }
                    }
                    else
                    {
                        if (dtpFromDate.Text == DateTime.Now.Date.ToString())
                        {
                            Session["ALLOWCURDATE"] = "1";
                        }
                        else
                        {
                            Session["ALLOWCURDATE"] = "0";
                        }

                        if (Selecteddate >= appfromdate && Selecteddate <= apptodate)
                        {
                            Session["WRONGDATERANGE"] = "0";
                        }
                        else
                        {
                            Session["WRONGDATERANGE"] = "1";
                        }
                    }
                }
                else
                {
                    lbtndate.Visible = false;
                }
            }

            if (_bdt == null)
            {
                _allowCurrentDate = true;
            }
            else
            {
                if (_bdt.Gad_alw_curr_trans == true)
                {
                    _allowCurrentDate = true;
                }
            }

            CheckSessionIsExpired();
            return _isAllow;
        }

        public void CheckSessionIsExpired()
        {
            if (Session["UserID"].ToString() != "ADMIN")
            {
                string _expMsg = "Current Session is expired or has been closed by administrator!";
                if (CHNLSVC.Security.IsSessionExpired(Session["SessionID"].ToString(), Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), out _expMsg) == true)
                {
                    BaseCls.GlbIsExit = true;
                    GC.Collect();
                }
            }
        }

        protected void gvStatus_SelectedIndexChangedOld(object sender, EventArgs e)
        {
            try
            {
                _selectedstatus = string.Empty;

                string pritm = (string)Session["ITM"];
                string prmfc = (string)Session["MFC"];
                string prstus = (string)Session["STUS"];
                string prser = (string)Session["SER"];
                string prloc = (string)Session["LOC"];
                string prline = (string)Session["LINE"];
                string prcost = (string)Session["COST"];

                if (dgvSelect.Rows.Count > 0)
                {
                    prline = (string)Session["LINE"];

                    Int32 selectedser = Convert.ToInt32(prline);

                    var list = new List<InventorySubSerialMaster>();

                    list = (List<InventorySubSerialMaster>)Session["SUBSERIALS"];

                    if (list != null)
                    {
                        _SubSerialList = list;
                    }

                    _selectedstatus = (gvStatus.SelectedRow.FindControl("lblseectedstus") as Label).Text;

                    if (_SubSerialList != null && _SubSerialList.Count > 0)
                    {
                        _SubSerialList.Where(w => w.Irsms_ser_line == selectedser).ToList().ForEach(s => s.Irsms_itm_sts_chg = _selectedstatus);
                        dgvSelect.AutoGenerateColumns = false;
                        dgvSelect.DataSource = new List<ReptPickSerials>();
                        dgvSelect.DataSource = _SubSerialList;
                        dgvSelect.DataBind();
                        DesableSerialTextBox();
                    }

                    foreach (GridViewRow dgvSelectorw in dgvSelect.Rows)
                    {
                        Label lblitm = (Label)dgvSelectorw.FindControl("lblitemcode");
                        Label lblmfs = (Label)dgvSelectorw.FindControl("lblmfc");
                        Label lblstus = (Label)dgvSelectorw.FindControl("lblstus");
                        Label lblser = (Label)dgvSelectorw.FindControl("lblserial");
                        Label lblloc = (Label)dgvSelectorw.FindControl("lblloc");
                        LinkButton lblstatusvalue = (LinkButton)dgvSelectorw.FindControl("lbtnstus");
                        Label lblline = (Label)dgvSelectorw.FindControl("irsms_ser_line");
                        Label lblcost = (Label)dgvSelectorw.FindControl("lblcost");
                        Label stustxt = (Label)dgvSelectorw.FindControl("lbtnstusdesc");

                        if ((pritm == lblitm.Text) && (prmfc == lblmfs.Text) && (prser == lblser.Text) && (prloc == lblloc.Text) && (prline == lblline.Text) && (prcost == lblcost.Text))
                        {
                            lblstatusvalue.Text = _selectedstatus;

                            DataTable dtstatustx = CHNLSVC.Sales.GetItemStatusTxt(_selectedstatus);

                            if (dtstatustx.Rows.Count > 0)
                            {
                                foreach (DataRow ddr2 in dtstatustx.Rows)
                                {
                                    stustxt.Text = ddr2[0].ToString();
                                }
                            }
                        }
                    }
                    Session["SUBSERIALS"] = _SubSerialList;
                }

                LoadStatusDescriptions();

                LoadToolTips();

                //  LoadStatusAndLocation();

                mpexcel.Hide();
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
        protected void gvStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string code = (gvStatus.SelectedRow.FindControl("lblseectedstus") as Label).Text;
                string desc = (gvStatus.SelectedRow.FindControl("lblstustext") as Label).Text;
              //  string unitText = (gvStatus.SelectedRow.FindControl("txtnoofunitsid") as TextBox).Text;
              //  decimal units = Convert.ToDecimal(unitText);
                _kitComs.Where(c => c.MIKC_LINE == _lineNo).FirstOrDefault().MIKC_STATUS_CD = code;
                _kitComs.Where(c => c.MIKC_LINE == _lineNo).FirstOrDefault().MIKC_STATUS = desc;
              //  _kitComs.Where(c => c.MIKC_LINE == _lineNo).FirstOrDefault().MIKC_NO_OF_UNIT = units;
                dgvSelect.DataSource = new int[] { };
                if (_kitComs.Count > 0)
                {
                    dgvSelect.DataSource = _kitComs;
                }
                dgvSelect.DataBind();
                DesableSerialTextBox();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void lbtnstus_ClickOld(object sender, EventArgs e)
        {
            try
            {
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;

                string itemcode = string.Empty;
                string mfc = string.Empty;
                string stus = string.Empty;
                string serial = string.Empty;
                string loc = string.Empty;
                string line = string.Empty;
                string cost = string.Empty;

                if (row != null)
                {
                    itemcode = (row.FindControl("lblitemcode") as Label).Text;
                    mfc = (row.FindControl("lblmfc") as Label).Text;
                    stus = (row.FindControl("lblstus") as Label).Text;
                    serial = (row.FindControl("lblserial") as Label).Text;
                    loc = (row.FindControl("lblloc") as Label).Text;
                    line = (row.FindControl("irsms_ser_line") as Label).Text;
                    cost = (row.FindControl("lblcost") as Label).Text;

                    Session["ITM"] = itemcode;
                    Session["MFC"] = mfc;
                    Session["STUS"] = stus;
                    Session["SER"] = serial;
                    Session["LOC"] = loc;
                    Session["LINE"] = line;
                    Session["COST"] = cost;

                    Divloc.Visible = false;
                    dvitm.Visible = false;
                    divstus.Visible = true;

                    DataTable _tbl = CHNLSVC.Inventory.GetAllCompanyStatus(Session["UserCompanyCode"].ToString());
                    gvStatus.AutoGenerateColumns = false;
                    gvStatus.DataSource = _tbl;
                    gvStatus.DataBind();

                    bool allow = false;
                    allow = LoadUserPermission();

                    if (allow == false)
                    {
                        divstus.Visible = false;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('You do not have permission for status change function.Permission Code :- 10104 !!!')", true);
                    }
                    else
                    {
                        divstus.Visible = true;
                    }
                    lblcaption.Text = "Change Status Code";
                    mpexcel.Show();
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
        protected void lbtnstus_Click(object sender, EventArgs e)
        {
            try
            {
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                if (row != null)
                {
                    Label lblLineNo = (Label)row.FindControl("lblLineNo");
                    _lineNo = Convert.ToInt32(lblLineNo.Text);
                    Divloc.Visible = false;
                    dvitm.Visible = false;
                    divstus.Visible = true;
                    DataTable _tbl = CHNLSVC.Inventory.GetAllCompanyStatus(Session["UserCompanyCode"].ToString());
                    gvStatus.AutoGenerateColumns = false;
                    gvStatus.DataSource = _tbl;
                    gvStatus.DataBind();

                    bool allow = false;
                    allow = LoadUserPermission();

                    if (allow == false)
                    {
                        divstus.Visible = false;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('You do not have permission for status change function.Permission Code :- 10104 !!!')", true);
                    }
                    else
                    {
                        divstus.Visible = true;
                    }
                    lblcaption.Text = "Change Status Code";
                    mpexcel.Show();
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
        protected void lbtnshcngeloc_ClickOld(object sender, EventArgs e)
        {
            try
            {
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;

                string itemcode = string.Empty;
                string mfc = string.Empty;
                string stus = string.Empty;
                string serial = string.Empty;
                string loc = string.Empty;
                string line = string.Empty;
                string cost = string.Empty;

                if (row != null)
                {
                    itemcode = (row.FindControl("lblitemcode") as Label).Text;
                    mfc = (row.FindControl("lblmfc") as Label).Text;
                    stus = (row.FindControl("lblstus") as Label).Text;
                    serial = (row.FindControl("lblserial") as Label).Text;
                    loc = (row.FindControl("lblloc") as Label).Text;
                    line = (row.FindControl("irsms_ser_line") as Label).Text;
                    cost = (row.FindControl("lblcost") as Label).Text;

                    Session["ITM"] = itemcode;
                    Session["MFC"] = mfc;
                    Session["STUS"] = stus;
                    Session["SER"] = serial;
                    Session["LOC"] = loc;
                    Session["LINE"] = line;
                    Session["COST"] = cost;

                    Divloc.Visible = true;
                    dvitm.Visible = false;
                    divstus.Visible = false;

                    List<SystemUserLoc> _ListLoc = CHNLSVC.Security.GetUserLoc(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString());
                    gvLoc.AutoGenerateColumns = false;
                    gvLoc.DataSource = _ListLoc;
                    gvLoc.DataBind();
                    lblcaption.Text = "Change Location Code";

                    foreach (GridViewRow item in gvLoc.Rows)
                    {
                        Label lblloccode = (Label)item.FindControl("lblloccode");
                        Label lbllocdesc = (Label)item.FindControl("lbllocdesc");

                        DataTable dtloc = CHNLSVC.General.LoadLocationDetailsByCode(Session["UserCompanyCode"].ToString(), lblloccode.Text);

                        if (dtloc.Rows.Count > 0)
                        {
                            foreach (DataRow ddr1 in dtloc.Rows)
                            {
                                lbllocdesc.Text = ddr1[0].ToString();
                            }
                        }
                    }

                    mpexcel.Show();
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
        protected void lbtnshcngeloc_Click(object sender, EventArgs e)
        {
            try
            {
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                Label lblLineNo = (Label)row.FindControl("lblLineNo");
                _lineNo = Convert.ToInt32(lblLineNo.Text);
                if (row != null)
                {
                    Divloc.Visible = true;
                    dvitm.Visible = false;
                    divstus.Visible = false;

                    List<SystemUserLoc> _ListLoc = CHNLSVC.Security.GetUserLoc(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString());
                    gvLoc.AutoGenerateColumns = false;
                    gvLoc.DataSource = _ListLoc;
                    gvLoc.DataBind();
                    lblcaption.Text = "Change Location Code";

                    foreach (GridViewRow item in gvLoc.Rows)
                    {
                        Label lblloccode = (Label)item.FindControl("lblloccode");
                        Label lbllocdesc = (Label)item.FindControl("lbllocdesc");

                        DataTable dtloc = CHNLSVC.General.LoadLocationDetailsByCode(Session["UserCompanyCode"].ToString(), lblloccode.Text);

                        if (dtloc.Rows.Count > 0)
                        {
                            foreach (DataRow ddr1 in dtloc.Rows)
                            {
                                lbllocdesc.Text = ddr1[0].ToString();
                            }
                        }
                    }

                    mpexcel.Show();
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

        protected void dgvSelect_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {

            }
            catch
            {

            }
        }

        protected void lbtngrdInvoiceDetailsEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                dgvSelect.EditIndex = grdr.RowIndex;
                dgvSelect.DataSource = new int[] { };
                if (_kitComs != null)
                {
                    List<ItemKitComponent> _list = new List<ItemKitComponent>();
                    if (ddlSerialized.SelectedValue == "Serialized")
                    {
                        _list = _kitComs.Where(c => c.MIKC_IS_SERIAL == 1).ToList();
                    }
                    else if (ddlSerialized.SelectedValue == "NONSerialized")
                    {
                        _list = _kitComs.Where(c => c.MIKC_IS_SERIAL != 1).ToList();
                    }
                    else
                    {
                        _list = _kitComs;
                    }
                    _list = _list.OrderByDescending(c => c.MIKC_ITM_CODE_COMPONENT).ToList();
                    dgvSelect.DataSource = _list;
                }
                dgvSelect.DataBind();
                // DesableSerialTextBox();
                //TextBox txteditser = grdr.FindControl("txteditser") as TextBox;
                //txteditser.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void lbtngrdInvoiceDetailsEdit_ClickOld(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                dgvSelect.EditIndex = grdr.RowIndex;

                var list = new List<InventorySubSerialMaster>();

                list = (List<InventorySubSerialMaster>)Session["SUBSERIALS"];

                if (list != null)
                {
                    _SubSerialList = list;
                }

                dgvSelect.DataSource = _SubSerialList;
                dgvSelect.DataBind();
                DesableSerialTextBox();
                LoadStatusDescriptions();
                LoadToolTips();
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
                    string ser = (row.FindControl("txteditser") as TextBox).Text;
                    string item = (row.FindControl("lblitemcode") as Label).Text;
                    string line = (row.FindControl("lblLineNo") as Label).Text;

                    MasterItem msitem = new MasterItem();
                    msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), item);

                    if (msitem.Mi_is_ser1 != 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This is not a Serialised item !!!')", true);
                        ser = "N/A";
                        return;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(ser))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a serial !!!')", true);
                            return;
                        }

                        if ((ser == "N/A") || (ser == "n/a") || (ser == "na") || (ser == "NA"))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You are not allowed to enter these types serials !!!')", true);
                            return;
                        }
                    }

                    DataTable dtisavailable = CHNLSVC.Inventory.LoadItemsBySerial(item, ser);

                    string loc = string.Empty;

                    foreach (DataRow ddre in dtisavailable.Rows)
                    {
                        loc = ddre["ins_loc"].ToString();
                    }

                    if (dtisavailable.Rows.Count > 0)
                    {
                        string msg = "Serial " + ser + " of the item " + item + " already available at the location " + loc + "";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                        return;
                    }

                    DataTable _dtSer = new DataTable();

                    _dtSer = CHNLSVC.Inventory.GetSerialDetailsBySerialCompany(Session["UserCompanyCode"].ToString(), item, ser);
                    if (_dtSer.Rows.Count > 1)
                    {
                        Int16 selectedser = Convert.ToInt16(line);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Serial already exists !!!')", true);
                        return;
                    }

                    if (ser != "N/A")
                    {
                        if ((_kitComs.FindAll(x => (x.MIKC_SERIAL_NO == ser))).Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This serial already exists !!!')", true);
                            return;
                        }
                    }
                    dgvSelect.EditIndex = -1;
                    _kitComs.Where(c => c.MIKC_LINE == Convert.ToInt32(line)).FirstOrDefault().MIKC_SERIAL_NO = ser;
                    List<ItemKitComponent> _list = new List<ItemKitComponent>();
                    if (ddlSerialized.SelectedValue == "Serialized")
                    {
                        _list = _kitComs.Where(c => c.MIKC_IS_SERIAL == 1).ToList();
                    }
                    else if (ddlSerialized.SelectedValue == "NONSerialized")
                    {
                        _list = _kitComs.Where(c => c.MIKC_IS_SERIAL != 1).ToList();
                    }
                    else
                    {
                        _list = _kitComs;
                    }
                    _list = _list.OrderByDescending(c => c.MIKC_ITM_CODE_COMPONENT).ToList();
                    dgvSelect.DataSource = _list;
                    dgvSelect.DataBind();
                    DesableSerialTextBox();
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
                dgvSelect.DataSource = new int[] { };
                if (_kitComs != null)
                {
                    dgvSelect.DataSource = _kitComs;
                }
                dgvSelect.DataBind();
                DesableSerialTextBox();
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
                    var lb = (LinkButton)sender;
                    var row = (GridViewRow)lb.NamingContainer;

                    if (row != null)
                    {
                        string ser = (row.FindControl("lblserial") as Label).Text;
                        string item = (row.FindControl("lblitemcode") as Label).Text;
                        string line = (row.FindControl("irsms_ser_line") as Label).Text;

                        var list = new List<InventorySubSerialMaster>();

                        list = (List<InventorySubSerialMaster>)Session["SUBSERIALS"];

                        if (list != null)
                        {
                            _SubSerialList = list;
                        }

                        _SubSerialList.RemoveAll(x => x.Irsms_itm_cd == item && x.Irsms_sub_ser == ser);
                        dgvSelect.DataSource = new List<InventoryRequestItem>();
                        dgvSelect.DataSource = _SubSerialList;
                        dgvSelect.DataBind();
                        Session["SUBSERIALS"] = _SubSerialList;
                        DesableSerialTextBox();
                        LoadStatusDescriptions();
                        LoadToolTips();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void ddlsplittype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsplittype.SelectedValue == "A")
            {
                Session["SPLLIT_TYPE"] = "A";
                ddlsplittype.Enabled = false;
            }
            else if (ddlsplittype.SelectedValue == "M")
            {
                Session["SPLLIT_TYPE"] = "M";
                ddlsplittype.Enabled = false;
            }
        }

        protected void dgvResult_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dgvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtItem.Text = dgvResult.SelectedRow.Cells[1].Text;
                txtSer.Text = dgvResult.SelectedRow.Cells[5].Text;
                string _pgType = Request.QueryString["TYPE"];
                if (_pgType == "S")
                {
                    _itmCom = new MasterItemComponent();
                    _itmCom.Micp_itm_cd = txtItem.Text.ToUpper().Trim();
                    _itmCom.Micp_act = true;
                    _itmCom.Mci_com = Session["UserCompanyCode"].ToString();
                    _itmCom.Mi_act = 1;
                    _itmComs = CHNLSVC.Inventory.Get_MST_ITM_COMPONENT(_itmCom);
                    if (_itmComs != null)
                    {
                        var _vKit = _itmComs.Where(c => c.Micp_itm_tp == "M").ToList();
                        if (_vKit != null)
                        {
                            if (_vKit.Count > 1)
                            {
                                // DispMsg("Multiple main items found ! Please contact inventory dept !"); txtItem.Text = ""; txtSer.Text = "";  return;
                            }
                        }
                    }
                }
                if (_pgType == "K")
                {
                    _kitCom = new ItemKitComponent() { MIKC_ITM_CODE_MAIN = txtItem.Text.ToUpper().Trim(), MIKC_ACTIVE = 1 };
                    _kitComs = CHNLSVC.Inventory.GetItemKitComponentSplit(_kitCom);
                    if (_kitComs != null)
                    {
                        var _vKit = _kitComs.Where(c => c.MIKC_ITM_TYPE == "M").ToList();
                        if (_vKit != null)
                        {
                            if (_vKit.Count > 1)
                            {
                                // DispMsg("Multiple main items found ! Please contact inventory dept !"); txtItem.Text = ""; txtSer.Text = ""; return;
                            }
                        }
                    }
                }
                LoadItemDetails();
                string _mytype = Request.QueryString["TYPE"];
                if (_mytype == "K")
                {
                    LoadCaniberlize();
                }
                if (_mytype == "S")
                {
                    LoadSplit();
                }

                // LoadStatusAndLocation();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void Clear()
        {
            txtItem.Text = "";
            txtSer.Text = "";
            txtItemDesn.Text = "";
            txtWarr.Text = "";
            txtModel.Text = "";
            txtBrand.Text = "";
            txtManual.Text = "";
            txtRem.Text = "";
            lblstustext.Text = "";
            lblStus.Text = "";
            lblFree.Text = "";
            lblRes.Text = "";
            dgvSelect.DataSource = new int[] { };
            dgvSelect.DataBind();
            DesableSerialTextBox();
            txtTech.Text = "";
            lbltechName.Text = "";
        }

        protected void ddlSerialized_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvSelect.EditIndex = -1;
            List<ItemKitComponent> _list = new List<ItemKitComponent>();
            if (ddlSerialized.SelectedValue == "Serialized")
            {
                _list = _kitComs.Where(c => c.MIKC_IS_SERIAL == 1).ToList();
            }
            else if (ddlSerialized.SelectedValue == "NONSerialized")
            {
                _list = _kitComs.Where(c => c.MIKC_IS_SERIAL != 1).ToList();
            }
            else
            {
                _list = _kitComs;
            }
            _list = _list.OrderByDescending(c => c.MIKC_ITM_CODE_COMPONENT).ToList();
            hdfCostTp.Value = "Cost";
            if (_list.Count > 0)
            {
                hdfCostTp.Value = _list[0].MIKC_COST_METHOD == "PER" ? "Cost (%)" : _list[0].MIKC_COST_METHOD == "AMT" ? "Cost (AMT)" : "Cost";
            }
            Label lclCostHed = (Label)dgvSelect.HeaderRow.FindControl("lclCostHed");
            lclCostHed.Text = hdfCostTp.Value;
            dgvSelect.DataSource = _list;
            dgvSelect.DataBind();
            DesableSerialTextBox();
        }

        protected void txtDocNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDocNo.Text))
                {
                    bool b2 = false;
                    string toolTip = "";

                    _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                    DataTable _result = CHNLSVC.Inventory.SearchSplitReCallNew(_Para, "Document #", txtDocNo.Text.Trim(), DateTime.MinValue, DateTime.MaxValue);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Document #"].ToString()))
                        {
                            if (txtDocNo.Text.ToUpper() == row["Document #"].ToString())
                            {
                                b2 = true;
                                toolTip = row["Document #"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtDocNo.ToolTip = toolTip;
                    }
                    else
                    {
                        txtDocNo.ToolTip = "";
                        txtDocNo.Text = "";
                        txtDocNo.Focus();
                        DispMsg("Please select a valid document # !!!");
                        return;
                    }
                    //load hdrdata
                    string _pgType = Request.QueryString["TYPE"];
                    if (_pgType == "K")
                    {
                        DataTable _cndata = CHNLSVC.Financial.GetCanibalizeHdrData(txtDocNo.Text.ToString());
                        if (_cndata != null && _cndata.Rows.Count > 0)
                        {
                            txtTech.Text = _cndata.Rows[0]["ith_del_cd"].ToString();
                            lbltechName.Text = _cndata.Rows[0]["ith_del_party"].ToString();
                        }
                        string mainitem = CHNLSVC.Inventory.GetCaniMainItem(txtDocNo.Text, Session["UserDefLoca"].ToString());
                        txtItem.Text = mainitem;

                        string mainitemser = CHNLSVC.Inventory.GetCaniMainSer(txtDocNo.Text, Session["UserDefLoca"].ToString());
                        txtSer.Text = mainitemser;
                    }
                   
                }
                else
                {
                    txtDocNo.ToolTip = "";
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnSeDoc_Click(object sender, EventArgs e)
        {
            try
            {
                txtFDate.Text = DateTime.Today.Date.ToString("dd/MMM/yyyy");
                txtTDate.Text = DateTime.Today.Date.ToString("dd/MMM/yyyy");
                _serData = new DataTable();
                grdResultD.DataSource = new int[] { };
                _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                _serData = CHNLSVC.Inventory.SearchSplitReCallNew(_Para, null, null, DateTime.Today, DateTime.Today);
                if (_serData.Rows.Count > 0)
                {
                    grdResultD.DataSource = _serData;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Document #");
                    BindCmbSearchbykey(dt);
                }
                grdResultD.DataBind();
                txtSearchbywordD.Text = "";
                txtSearchbywordD.Focus();
                _serType = "DocNo";
                PopupSearch.Show();
                SerPopShow = "Show";
                if (grdResultD.PageIndex > 0)
                { grdResultD.SetPageIndex(0); }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
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
            else if (msgType == "A")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + msgText + "');", true);
            }
        }
        public void BindCmbSearchbykey(DataTable _dataSource)
        {
            this.ddlSearchbykeyD.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykeyD.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykeyD.SelectedIndex = 0;
        }
        protected void btnDClose_Click(object sender, EventArgs e)
        {
            SerPopShow = "Hide";
            PopupSearch.Hide();
        }

        protected void lbtnSeByDate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dtTemp = DateTime.Today;
                DateTime dtFrom = DateTime.TryParse(txtFDate.Text, out dtTemp) ? Convert.ToDateTime(txtFDate.Text) : dtTemp;
                DateTime dtTo = DateTime.TryParse(txtTDate.Text, out dtTemp) ? Convert.ToDateTime(txtTDate.Text) : dtTemp;
                _serData = new DataTable();
                grdResultD.DataSource = new int[] { };
                _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                _serData = CHNLSVC.Inventory.SearchSplitReCallNew(_Para, null, null, dtFrom, dtTo);
                if (_serData.Rows.Count > 0)
                {
                    grdResultD.DataSource = _serData;
                    BindCmbSearchbykey(_serData);
                }
                grdResultD.DataBind();
                txtSearchbywordD.Text = "";
                txtSearchbywordD.Focus();
                _serType = "DocNo";
                PopupSearch.Show();
                SerPopShow = "Show";
                if (grdResultD.PageIndex > 0)
                { grdResultD.SetPageIndex(0); }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnSearchD_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                grdResultD.DataSource = new int[] { };
                _Para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                _serData = CHNLSVC.Inventory.SearchSplitReCallNew(_Para, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text.Trim(), DateTime.MinValue, DateTime.MaxValue);
                if (_serData.Rows.Count > 0)
                {
                    grdResultD.DataSource = _serData;
                    BindCmbSearchbykey(_serData);
                }
                grdResultD.DataBind();
                txtSearchbywordD.Text = "";
                txtSearchbywordD.Focus();
                _serType = "DocNo";
                PopupSearch.Show();
                SerPopShow = "Show";
                if (grdResultD.PageIndex > 0)
                { grdResultD.SetPageIndex(0); }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void grdResultD_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string code = grdResultD.SelectedRow.Cells[1].Text;
                if (_serType == "DocNo")
                {
                    txtDocNo.Text = code;
                    txtDocNo_TextChanged(null, null);
                }
                SerPopShow = "Hide";
                PopupSearch.Hide();

            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void grdResultD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdResultD.PageIndex = e.NewPageIndex;
                if (_serData.Rows.Count > 0)
                {
                    grdResultD.DataSource = _serData;
                }
                else
                {
                    grdResultD.DataSource = new int[] { };
                }
                grdResultD.DataBind();
                txtSearchbywordD.Text = string.Empty;
                txtSearchbywordD.Focus();
                PopupSearch.Show();
                SerPopShow = "Show";
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void txtSearchbywordD_TextChanged(object sender, EventArgs e)
        {
            lbtnSearchD_Click(null, null);
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDocNo.Text) || Session["documntNo"].ToString() != null)
                {
                    print(string.IsNullOrEmpty(txtDocNo.Text) ? Session["documntNo"].ToString() : txtDocNo.Text);
                }
                else
                {
                    DispMsg("Please select the document # !!!"); return;
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void lbtnAddSer_Click(object sender, EventArgs e)
        {
            try
            {
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                TextBox txtSerId = row.FindControl("txtSerId") as TextBox;
                txtSerId.ReadOnly = true;
                if (!string.IsNullOrEmpty(txtSerId.Text))
                {
                    string ser = (row.FindControl("txtSerId") as TextBox).Text;
                    string item = (row.FindControl("lblitemcode") as Label).Text;
                    string line = (row.FindControl("lblLineNo") as Label).Text;

                    MasterItem msitem = new MasterItem();
                    msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), item);
                    if (msitem == null)
                    {
                        txtSerId.Text = ""; txtSerId.ReadOnly = false;
                        DispMsg("Item code is invalid !"); return;
                    }
                    if (msitem.Mi_is_ser1 != 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This is not a Serialised item !!!')", true);
                        // ser = "N/A";
                        txtSerId.Text = "";
                        return;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(ser))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a serial !!!')", true);
                            return;
                        }

                        if ((ser == "N/A") || (ser == "n/a") || (ser == "na") || (ser == "NA"))
                        {
                            txtSerId.Text = "";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You are not allowed to enter these types serials !!!')", true);
                            return;
                        }
                    }

                    DataTable dtisavailable = CHNLSVC.Inventory.LoadItemsBySerial(item, ser);

                    string loc = string.Empty;

                    foreach (DataRow ddre in dtisavailable.Rows)
                    {
                        loc = ddre["ins_loc"].ToString();
                    }

                    if (dtisavailable.Rows.Count > 0)
                    {
                        string msg = "Serial " + ser + " of the item " + item + " already available at the location " + loc + "";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                        txtSerId.Text = "";
                        txtSerId.ReadOnly = false;
                        txtSerId.Focus();
                        return;
                    }

                    DataTable _dtSer = new DataTable();

                    _dtSer = CHNLSVC.Inventory.GetSerialDetailsBySerialCompany(Session["UserCompanyCode"].ToString(), item, ser);
                    if (_dtSer.Rows.Count > 1)
                    {
                        Int16 selectedser = Convert.ToInt16(line);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Serial already exists !!!')", true);
                        txtSerId.Text = "";
                        txtSerId.ReadOnly = false;
                        txtSerId.Focus();
                        return;
                    }

                    if (ser != "N/A")
                    {
                        if ((_kitComs.FindAll(x => (x.MIKC_SERIAL_NO == ser))).Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This serial already exists !!!')", true);
                            txtSerId.Text = "";
                            txtSerId.ReadOnly = false;
                            txtSerId.Focus();
                            return;
                        }
                    }
                    dgvSelect.EditIndex = -1;
                    _kitComs.Where(c => c.MIKC_LINE == Convert.ToInt32(line)).FirstOrDefault().MIKC_SERIAL_NO = ser;
                    List<ItemKitComponent> _list = new List<ItemKitComponent>();
                    if (ddlSerialized.SelectedValue == "Serialized")
                    {
                        _list = _kitComs.Where(c => c.MIKC_IS_SERIAL == 1).ToList();
                    }
                    else if (ddlSerialized.SelectedValue == "NONSerialized")
                    {
                        _list = _kitComs.Where(c => c.MIKC_IS_SERIAL != 1).ToList();
                    }
                    else
                    {
                        _list = _kitComs;
                    }
                    _list = _list.OrderByDescending(c => c.MIKC_ITM_CODE_COMPONENT).ToList();
                    dgvSelect.DataSource = _list;
                    dgvSelect.DataBind();
                    txtSerId.ReadOnly = true;

                }
                Int32 rowNo = 0;
                rowNo = row.RowIndex + 1;
                if (dgvSelect.Rows.Count > rowNo)
                {
                    TextBox tmpSerialTemp = dgvSelect.Rows[rowNo].FindControl("txtSerId") as TextBox;
                    tmpSerialTemp.Focus();
                }
                DesableSerialTextBox();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!! " + ex.Message, "E");
            }
        }

        private void DesableSerialTextBox()
        {
            foreach (GridViewRow row in dgvSelect.Rows)
            {
                TextBox txtSerId = row.FindControl("txtSerId") as TextBox;
                if (!string.IsNullOrEmpty(txtSerId.Text))
                {
                    txtSerId.ReadOnly = true;
                }
                else
                {
                    txtSerId.ReadOnly = false;
                }
            }
        }

        protected void lbtngrdInvoiceDetailsDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var lb = (LinkButton)sender;
                var row = (GridViewRow)lb.NamingContainer;
                TextBox txtSerId = row.FindControl("txtSerId") as TextBox;

                if (_kitComs != null)
                {
                    foreach (var item in _kitComs)
                    {
                        if (item.MIKC_SERIAL_NO == txtSerId.Text)
                        {
                            item.MIKC_SERIAL_NO = string.Empty;
                        }
                    }
                }
                txtSerId.Text = string.Empty;
                DesableSerialTextBox();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!! " + ex.Message, "E");
            }
        }

        protected void txtTech_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnTech_Click(object sender, EventArgs e)
        {
            try
            {
                string _mytype = Request.QueryString["TYPE"];
                //txtSearchbyword.Text = string.Empty;
                //DummyDataBind();
                //_serData = new DataTable();
                //_para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Technician);
                //_serData = CHNLSVC.CommonSearch.GetAllTechnician(_para, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                //grdResult.DataSource = _serData;
                //grdResult.DataBind();
                //BindUCtrlDDLData(_serData);
                //UserPopoup.Show();
                //lblvalue.Text = "Technician";
                //txtSearchbyword.Focus();

                if (_mytype == "K")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MainItem);
                    DataTable result = CHNLSVC.CommonSearch.GetAllTechnician(SearchParams, null, null);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "TECH";
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    txtSearchbyword.Focus();
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void btnBarcdYes_Click(object sender, EventArgs e)
        {

            if (txtItem.Text.ToString()=="" && Session["mainser"]==null)
            {

                DispMsg("Please Select Main Item", "E");
                return;
            }

            PrintBarcode();
        }

        protected void btnBarcdNo_Click(object sender, EventArgs e)
        {

        }

        protected void PrintBarcode()
        {
            Rpt7030 rpt = new Rpt7030();
            DataTable ds = new DataTable("tbl");
            ds.Columns.Add("MI_CD", typeof(String));
            ds.Columns.Add("MI_SHORTDESC", typeof(String));
            ds.Columns.Add("MI_MODEL", typeof(String));
            ds.Columns.Add("BARCODE", typeof(String));
            ds.Columns.Add("Company", typeof(String));
            ds.Columns.Add("MainSer", typeof(String));
            string comdesc = "";
            string targetFileName = string.Empty;
            string url = string.Empty;
            DataTable result = new DataTable();
            MasterCompany comm = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
            //!string.IsNullOrEmpty(txtItem.Text) || Session["mainItem"].ToString() != ""
            if (Session["documntNo"].ToString() != "" || txtDocNo.Text !="")
            {
                if (Session["documntNo"].ToString()=="")
                {
                    Session["documntNo"] = txtDocNo.Text;
                }
                result = CHNLSVC.Inventory.Get_canibaliseData(Session["documntNo"].ToString(), Session["UserCompanyCode"].ToString());
            }
            else
            {
                DispMsg("Please select main item code...!!!", "W");
            }
            if (comm != null)
            {
                comdesc = comm.Mc_anal5;
            }

            if (result.Rows.Count > 0)
            {
                for (Int32 i = 0; i < result.Rows.Count; i++)
                {
                    string _itm = result.Rows[i]["Item"].ToString();
                    //result.AsEnumerable().Select(r => r.Field<string>("Item")).FirstOrDefault();
                    MST_ITM item = CHNLSVC.MsgPortal.getItemDetail(_itm.ToUpper());
                    for(int r = 1; r <= Convert.ToInt16(result.Rows[i]["Qty"]); r++)
                    {
                        if (txtSer.Text != "") Session["mainser"] = txtSer.Text;
                        ds.Rows.Add(item.MI_CD.ToUpper(), item.MI_SHORTDESC, item.MI_MODEL, item.MI_CD.ToUpper(), comdesc, Session["mainser"].ToString());
                    }
                }
            }   
            try
            {
                if (ds.Rows.Count > 0)
                {
                    rpt.Database.Tables["ItmCd60x15"].SetDataSource(ds);
                    targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                    PrintPDF(targetFileName, rpt);
                    url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                }
                else
                {
                    DispMsg("Error in processing...!!!", "W");
                }
            }
            catch (Exception ex)
            {
                throw ex;
                CHNLSVC.MsgPortal.SaveReportErrorLog("Item Splitting Print", "ItemSplitting", ex.Message, Session["UserID"].ToString());
            }
            rpt.Close();
        }
        public void PrintPDF(string targetFileName, ReportDocument _rpt)
        {
            try
            {
                ReportDocument rptDoc = (ReportDocument)_rpt;
                DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                diskOpts.DiskFileName = targetFileName;
                rptDoc.ExportOptions.DestinationOptions = diskOpts;
                rptDoc.Export();

                rptDoc.Close();
                rptDoc.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lbtnBPrint_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDocNo.Text))
            {
                

                if (txtSer.Text.ToString() == "" && Session["mainser"]==null)
                {
                    DispMsg("Please Select Main Item", "E");
                    return;
                }
                PrintBarcode();
            }
            else
            {
                DispMsg("Please Select Document Before Print", "W");
            }
        }
    }
}