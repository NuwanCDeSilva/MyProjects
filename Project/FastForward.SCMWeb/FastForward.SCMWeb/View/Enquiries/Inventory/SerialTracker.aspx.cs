using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.Services;
using FastForward.SCMWeb.View.Reports.Inventory;
using FF.BusinessObjects;
using FF.BusinessObjects.InventoryNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Enquiries.Inventory
{
    public partial class SerialTracker : Base //System.Web.UI.Page
    {
        Base _basePage;
        string _currCode { set { Session["_currCode"] = value; } get { return (string)Session["_currCode"]; } }

        string _sortDirection;
        string SortDireaction;
        DataTable dataTable;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Session["UserCompanyCode"] == null || Session["UserID"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                ViewState["sortOrder"] = "desc";
                Session["dataTable"] = dataTable;
                costPannel.Visible = false;
                costPannel.Enabled = false;
                bool b10129 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10129);
                costPannel.Visible = b10129;
                costPannel.Enabled = b10129;
                cmbSerialType.SelectedIndex = 0;
                if (cmbSerialType.Items.Count > 0)
                {
                    cmbSerialType.SelectedIndex = 1;
                }
                pageClear();

                grdSerial.DataSource = new int[] { };
            }

            else
            {
                taxDetailspopup.Hide();
            }



        }

        InventorySerialMaster recalledJobSerial = null;
        private Int32 _warStus = 0;
        private List<Service_job_Det> _scvItemList = new List<Service_job_Det>();
        private List<InventoryWarrantyDetail> wr = new List<InventoryWarrantyDetail>();

        private void getserial()
        {

            //List<InventorySerialMaster> _warrDet = null;
            //_warrDet = _custServiceDAL.GetWarrantyMaster(_item, _ser1, _ser2, _regno, _warr, _invoice, _serid);

        }


        //flipdatatable
        /*    public DataSet FlipDataSet(DataSet my_DataSet)
            {
                DataSet ds = new DataSet();
                foreach (DataTable dt in my_DataSet.Tables)
                {
                    DataTable table = new DataTable();
                    for (int i = 0; i <= dt.Rows.Count; i++)
                    {
                        table.Columns.Add(Convert.ToString(i));
                    }
                    DataRow r = null;
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        r = table.NewRow();
                        r[0] = dt.Columns[k].ToString();
                        for (int j = 1; j <= dt.Rows.Count; j++)
                            r[j] = dt.Rows[j - 1][k];
                        table.Rows.Add(r);
                    }
                    ds.Tables.Add(table);
                }

                return ds;
            }*/
        //

        //transport gridview

        private DataTable GetTransposedTable(DataTable dt)
        {
            DataTable newTable = new DataTable();
            newTable.Columns.Add(new DataColumn("0", typeof(string)));
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                DataRow newRow = newTable.NewRow();
                newRow[0] = dt.Columns[i].ColumnName;
                for (int j = 1; j <= dt.Rows.Count; j++)
                {
                    if (newTable.Columns.Count < dt.Rows.Count + 1)
                        newTable.Columns.Add(new DataColumn(j.ToString(), typeof(string)));
                    newRow[j] = dt.Rows[j - 1][i];
                }
                newTable.Rows.Add(newRow);
            }
            return newTable;
        }

        //

        private void textBoxClear()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
        }


        private void getCost(string itemcode, string serial)
        {
            _basePage = new Base();
            try
            {
                DataTable dt = _basePage.CHNLSVC.Inventory.getunitCost(itemcode, serial);

                double data = dt.Rows[0].Field<double>("irsm_unit_cost");

                TextBox2.Text = data.ToString("N2");
                TextBox1.Text = data.ToString("N2");
            }
            catch (Exception ex) { }
            //     gvproducts.DataSource = dt;// FlipDataTable(dt);
            //     gvproducts.DataBind();
            //     gvproducts.HeaderRow.Visible = false;

        }

        public void getnoofdays(string itemcode, string serial)
        {

            _basePage = new Base();
            try
            {
                DataTable dt = _basePage.CHNLSVC.Inventory.get_noofdays(itemcode, serial);

                decimal days = dt.Rows[0].Field<decimal>("days");
                TextBox9.Text = days.ToString();
                TextBox10.Text = days.ToString();
            }
            catch (Exception ex)
            {
                //  displayMessage(ex.Message);
            }

        }




        //total values
        private void total()
        {
            double value = 0;
            double prcost;
            double storage;
            double handling;
            if (TextBox1.Text == "")
            {

                prcost = 0;
            }
            else
            {
                prcost = Convert.ToDouble(TextBox1.Text);
            }

            if (TextBox3.Text == "")
            {
                storage = 0;
            }
            else
            {
                storage = Convert.ToDouble(TextBox3.Text);
            }

            if (TextBox5.Text == "")
            {
                handling = 0;
            }
            else
            {
                handling = Convert.ToDouble(TextBox5.Text);
            }

            value = prcost + storage + handling;

            TextBox7.Text = value.ToString("N2");

        }


        //
        private void totalCost()
        {
            double value = 0;
            double prcost;
            double storage;
            double handling;
            if (TextBox2.Text == "")
            {

                prcost = 0;
            }
            else
            {
                prcost = Convert.ToDouble(TextBox2.Text);
            }

            if (TextBox4.Text == "")
            {
                storage = 0;
            }
            else
            {
                storage = Convert.ToDouble(TextBox4.Text);
            }

            if (TextBox6.Text == "")
            {
                handling = 0;
            }
            else
            {
                handling = Convert.ToDouble(TextBox6.Text);
            }

            value = prcost + storage + handling;

            TextBox8.Text = value.ToString("N2");

        }
        //



        private void getstorage(string itemcode, string serial)
        {
            _basePage = new Base();

            try
            {
                DataTable dt = _basePage.CHNLSVC.Inventory.getStorageDetails(itemcode, serial);
                //   GridView2.DataSource = dt;
                //    GridView2.DataBind();

                //      string storage = Convert.ToString(dt.Rows[0].Field<string>("irsm_unit_cost")); TextBox3
                decimal data = dt.Rows[0].Field<decimal>("Total sales");
                TextBox3.Text = data.ToString("N2");
                TextBox4.Text = data.ToString("N2");

                decimal handling = dt.Rows[1].Field<decimal>("Total sales");
                TextBox5.Text = handling.ToString("N2");
                TextBox6.Text = handling.ToString("N2");

                //TextBox1.Text = data.ToString();
            }
            catch (Exception ex)
            {
                //  displayMessage(ex.Message);

            }

        }


        /*     public void verticalgrid() {

                 GridView2.DataSource = FlipDataTable(dt);

                 GridView2.DataBind();

                 GridView2.HeaderRow.Visible = false;
             }
             */

        public static DataTable FlipDataTable(DataTable dt)
        {
            DataTable table = new DataTable();
            //Get all the rows and change into columns
            for (int i = 0; i <= dt.Rows.Count; i++)
            {
                table.Columns.Add(Convert.ToString(i));

            }
            DataRow dr;
            //get all the columns and make it as rows
            for (int j = 0; j < dt.Columns.Count; j++)
            {

                dr = table.NewRow();

                dr[0] = dt.Columns[j].ToString();

                for (int k = 1; k <= dt.Rows.Count; k++)

                    dr[k] = dt.Rows[k - 1][j];

                table.Rows.Add(dr);

            }



            return table;

        }





        private void getSerialAndWarrenty()
        {
            /*  List<InventorySerialMaster> _warrMst = new List<InventorySerialMaster>();
              List<InventorySubSerialMaster> _warrMstSub = new List<InventorySubSerialMaster>();
              Dictionary<List<InventorySerialMaster>, List<InventorySubSerialMaster>> _warrMstDic = null;
              int _returnStatus;
              string _returnMsg;

              foreach (KeyValuePair<List<InventorySerialMaster>, List<InventorySubSerialMaster>> pair in _warrMstDic)
              {
                  _warrMst = pair.Key;
                  _warrMstSub = pair.Value;
              }
              _warrMstDic = CHNLSVC.CustService.GetWarrantyMaster(txtSerialNo.Text, "", "", "", "", lblItem.Text, 0, out _returnStatus, out _returnMsg);
              recalledJobSerial = _warrMst[0];
              Label15.Text = _warrMst[0].Irsm_warr_no;
              Label17.Text = _warrMst[0].Irsm_warr_period.ToString();*/
            //    InventoryWarrantyDetail warranty = new InventoryWarrantyDetail();    





        }

        //2015 nov 11
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");


            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SystemRole:
                    {
                        paramsText.Append(txtSerialNo.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SystemUser:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SecUsrPermTp:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Designation:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Department:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(txtSerialNo.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        paramsText.Append(txtSerialNo.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserRole:
                    {
                        paramsText.Append(txtSerialNo + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ApprovePermCode:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ApprovePermLevelCode:
                    {
                        paramsText.Append(txtSerialNo.Text.Trim() + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        //-----------


        private void pageClear()
        {
            _basePage = new Base();
            MasterCompany _mstComp = _basePage.CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
            if (_mstComp != null)
            {
                _currCode = _mstComp.Mc_cur_cd;
            }
            lblCurrCostCode.Text = "(" + _currCode + ")";
            lblOrgCostCode.Text = "(" + _currCode + ")";

            //lblCostCd="(" + _currCode + ")"
            gvSale.DataSource = new int[] { };
            gvSale.DataBind();

            gvMovement.DataSource = new int[] { };
            gvMovement.DataBind();

            gvSubSerial.DataSource = new int[] { };
            gvSubSerial.DataBind();

            gdvResDet.DataSource = new int[] { };
            gdvResDet.DataBind();

            gvMultipleItem.DataSource = new int[] { };
            gvMultipleItem.DataBind();

            GridView1.DataSource = new int[] { };
            GridView1.DataBind();
            Label lblCostCd = (Label)gvMovement.HeaderRow.FindControl("lblCostCd");
            lblCostCd.ForeColor = Color.Purple;
            lblCostCd.Text = "Unit Cost(" + _currCode + ")";

            Label gvSalelblCostCd = (Label)gvSale.HeaderRow.FindControl("lblCostCd");
            gvSalelblCostCd.Text = "Unit Cost(" + _currCode + ")";

            //      gvproducts.DataSource = new int[] { };
            //       gvproducts.DataBind();


            //       GridView2.DataSource = new int[] { };
            //      GridView2.DataBind();




        }

        //clear the serials 


        private void ClearBar1(bool _isMustFocus)
        {
            cmbSerialType.SelectedIndex = 0;
            if (cmbSerialType.Items.Count > 0)
            {
                cmbSerialType.SelectedIndex = 1;
            }
            txtSerialNo.Text = "";
            cmbCaseType.SelectedIndex = 0;
            chkWholeWord.Checked = true;
            lblItem.Text = string.Empty;
            if (_isMustFocus) txtSerialNo.Focus();
            // gvMovement.DataSource = null;
        }

        private void ClearDatasources()
        {
            gvMovement.DataSource = null;
            gvMovement.DataBind();

            gvSale.DataSource = null;
            gvSale.DataBind();

            gvSubSerial.DataSource = null;
            gvSubSerial.DataBind();

            gdvResDet.DataSource = null;
            gdvResDet.DataBind();

        }


        //clear the currently store at


        private void ClearBar2()
        {
            lblCurLocation.Text = string.Empty;
            lblCurCompany.Text = string.Empty;
            lblCurReceivedDate.Text = string.Empty;
            lblCurBin.Text = string.Empty;
            lblCurItemStatus.Text = string.Empty;
            //----



        }

        private void clearBar3()
        {
            lblItemDescription.Text = string.Empty;
            lblItemModel.Text = string.Empty;
            lblItemBrand.Text = string.Empty;
            Label15.Text = string.Empty;
            Label17.Text = string.Empty;
            lblcusdecEntryDate.Text = string.Empty;
            lblEntryNo.Text = string.Empty;
            lblMvEntry.Text = string.Empty;
            lblFinancingRef.Text = string.Empty;
        }


        private void clearBar4()
        {

            lblItmColor.Text = string.Empty;
            lblItmUOM.Text = string.Empty;
            lblItmDimension.Text = string.Empty;
            lblItmWeight.Text = string.Empty;

            lblItmMainCat.Text = string.Empty;
            lblItmHSCode.Text = string.Empty;
            lblItmHPAvailability.Text = string.Empty;
            lblItmInsuAvailability.Text = string.Empty;

        }

        //get the assign location


        private void AssignCurrentLocation(DataTable _currentLocation)
        {
            if (_currentLocation == null)
            {
                lblCurBin.Text = string.Empty;
                lblCurCompany.Text = string.Empty;
                lblCurItemStatus.Text = string.Empty;
                lblCurLocation.Text = string.Empty;
                lblCurReceivedDate.Text = string.Empty;
                return;
            }


            if (_currentLocation.Rows.Count > 0)
            {

                lblCurBin.Text = Convert.ToString(_currentLocation.Rows[0].Field<string>("INS_BIN"));
                lblCurCompany.Text = Convert.ToString(_currentLocation.Rows[0].Field<string>("INS_COM")) + " - " + Convert.ToString(_currentLocation.Rows[0].Field<string>("MC_DESC"));
                lblCurItemStatus.Text = Convert.ToString(_currentLocation.Rows[0].Field<string>("MIS_DESC"));
                lblCurLocation.Text = Convert.ToString(_currentLocation.Rows[0].Field<string>("INS_LOC")) + " - " + Convert.ToString(_currentLocation.Rows[0].Field<string>("ML_LOC_DESC"));
                lblCurReceivedDate.Text = Convert.ToDateTime(_currentLocation.Rows[0].Field<DateTime>("INS_DOC_DT")).Date.ToShortDateString();

            }

        }

        //fill the movements gridview



        private void AssignMovement(DataTable _movement)
        {
            if (_movement == null) return;
            if (_movement.Rows.Count > 0)
            {

                //GridView _source = new GridView();
                //  BindingSource bs = new BindingSource();
                gvMovement.DataSource = _movement;
                Session["dataTable"] = _movement;
                gvMovement.DataBind();
                Label lblCostCd = (Label)gvMovement.HeaderRow.FindControl("lblCostCd");
                lblCostCd.ForeColor = Color.Purple;
                lblCostCd.Text = "Unit Cost(" + _currCode + ")";
            }

        }

        /*   private void showMovements(DataTable _mulmovements) 
           {
               GridView1.DataSource = _mulmovements;
               GridView1.DataBind();
      
           }
             */

        private void AssignSale(DataTable _sale)
        {
            if (_sale == null) return;
            if (_sale.Rows.Count > 0)
            {
                // GridView _source = new GridView();
                gvSale.DataSource = _sale;
                gvSale.DataBind();
                Label gvSalelblCostCd = (Label)gvSale.HeaderRow.FindControl("lblCostCd");
                gvSalelblCostCd.ForeColor = Color.Purple;
                gvSalelblCostCd.Text = "Unit Cost(" + _currCode + ")";
            }
        }




        //   DataTable _InitialStageSearch = null;
        protected void Button1_Click(object sender, EventArgs e)
        {

        }


        private bool GetItemAdvanceDetail(string _item)
        {
            bool _isValid = false;

            try
            {
                MasterItem _itm = new MasterItem();
                if (!string.IsNullOrEmpty(_item)) _itm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                if (_itm != null)
                    if (!string.IsNullOrEmpty(_itm.Mi_cd))
                    {

                        _isValid = true;
                        string _description = _itm.Mi_longdesc;
                        string _model = _itm.Mi_model;
                        string _brand = _itm.Mi_brand;

                        lblItemDescription.Text = _description;
                        lblItemModel.Text = _model;
                        lblItemBrand.Text = _brand;

                        lblItmColor.Text = _itm.Mi_color_ext == "NULL" ? string.Empty : _itm.Mi_color_ext;
                        lblItmUOM.Text = _itm.Mi_itm_uom == "NULL" ? string.Empty : _itm.Mi_itm_uom;
                        decimal d = _itm.Mi_dim_width * _itm.Mi_dim_height * _itm.Mi_dim_length;
                        lblItmDimension.Text = "(" + _itm.Mi_dim_width.ToString() + " x " + _itm.Mi_dim_height.ToString() + " x " +
                            _itm.Mi_dim_length.ToString() + " " + (_itm.Mi_dim_uom == "NULL" ? string.Empty : _itm.Mi_dim_uom) + ") (" + d.ToString() + ")";
                        lblItmWeight.Text = _itm.Mi_net_weight.ToString() + " " + (_itm.Mi_weight_uom == "NULL" ? string.Empty : _itm.Mi_weight_uom);
                        lblItmMainCat.Text = _itm.Mi_cate_1;
                        lblItmHSCode.Text = _itm.Mi_hs_cd;

                        if (_itm.Mi_is_ser1 == 1)
                        { lblItmSerSts1.BackColor = Color.LawnGreen; lblItmSerSts1.ForeColor = Color.Black; }
                        else
                        { lblItmSerSts1.BackColor = Color.Crimson; lblItmSerSts1.ForeColor = Color.White; }

                        if (_itm.Mi_is_ser2 == 1)
                        { lblItmSerSts2.BackColor = Color.LawnGreen; lblItmSerSts2.ForeColor = Color.Black; }

                        else
                        { lblItmSerSts2.BackColor = Color.Crimson; lblItmSerSts2.ForeColor = Color.White; }

                        if (_itm.Mi_is_ser3)
                        { lblItmSerSts3.BackColor = Color.LawnGreen; lblItmSerSts3.ForeColor = Color.Black; }

                        else
                        { lblItmSerSts3.BackColor = Color.Crimson; lblItmSerSts3.ForeColor = Color.White; }
                        lblItmHPAvailability.Text = _itm.Mi_hp_allow ? "Yes" : "No";
                        lblItmInsuAvailability.Text = _itm.Mi_insu_allow ? "Yes" : "No";


                    }

            }
            catch (Exception ex)
            {
                // this.Cursor = Cursors.Default;
                CHNLSVC.CloseChannel();
                return _isValid;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

            return _isValid;
        }

        DataTable _InitialStageSearch = null;
        DataTable _serialfind = null;

        protected void Button1_Click1(object sender, EventArgs e)
        {


        }

        protected void Button1_Click2(object sender, EventArgs e)
        {

        }

        private void displayMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            lblSupInvno.Text = "";
            lblSupInvDt.Text = "";
            //replace link button
            if (cmbSerialType.SelectedIndex != 0)
            {
                lblCurLocation.Visible = true;
                //var _hasSer = CHNLSVC.Inventory.Get_INR_SER_DATA(new InventorySerialN()
                //{
                //    Ins_com = Session["UserCompanyCode"].ToString(),
                //    Ins_loc = Session["UserDefLoca"].ToString(),
                //    Ins_available = 1,
                //    Ins_ser_1 = txtSerialNo.Text.Trim(),
                //    Ser_tp = 0
                //}).FirstOrDefault();
                //if (_hasSer != null)
                //{
                //    lblCurLocation.Visible = true;
                //}
                string _serialtype = cmbSerialType.Text.Trim();
                string _characterCase = cmbCaseType.Text.Trim();
                bool _isMatchWholeWord = chkWholeWord.Checked;

                if (txtSerialNo.Text == "")
                {

                    displayMessage("Please select the serial number");
                    return;

                }
                if (_characterCase == "Lower")
                {
                    //  multiplepopup1.Show();
                }

                /*     if (txtSerialNo.Text.Substring(0, 1) == "%" && chkWholeWord.Checked == false)
                     {
                       //  MessageBox.Show("You can not add % as starting character");
                         return;
                     }
                         */
                string _serial = string.Empty;
                string _serialType = string.Empty;
                Int16 _isWholeWord = Convert.ToInt16(chkWholeWord.Checked ? 1 : 0);

                if (cmbSerialType.Text.Trim() == "Serial 1")
                    _serialType = "SERIAL1";
                else if (cmbSerialType.Text.Trim() == "Serial 2")
                    _serialType = "SERIAL2";
                else if (cmbSerialType.Text.Trim() == "Serial 3")
                    _serialType = "SERIAL3";
                else if (cmbSerialType.Text.Trim() == "Serial 4")
                    _serialType = "SERIAL4";

                if (cmbCaseType.Text == "Normal")
                {
                    _serial = txtSerialNo.Text.Trim();
                }
                else if (cmbCaseType.Text == "Upper")
                {
                    _serial = txtSerialNo.Text.Trim();
                }
                else if (cmbCaseType.Text == "Lower")
                {
                    _serial = txtSerialNo.Text.Trim();
                }
                if (_isWholeWord == 0) _serial += "%";


                _InitialStageSearch = CHNLSVC.Inventory.GetSerialItem(_serialType, Session["UserCompanyCode"].ToString(), _serial, _isWholeWord);

                if (_InitialStageSearch.Rows.Count == 0)
                {

                    // Page.ClientScript.RegisterStartupScript(this.GetType(),"toastr_message", "toastr.error('There was an error', 'Error')", true);
                    //   Page.ClientScript.RegisterStartupScript(this.GetType(), "showStickyWarningToast", "showStickyWarningToast('hello')", true);
                    string msg = "There is no such serial available in the system for the given criteria";
                    ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);

                    //  divscro.Visible = true;


                    lblItem.Text = string.Empty;
                    GetItemAdvanceDetail(lblItem.Text.Trim());
                    AssignCurrentLocation(null);
                    // BindingSource _source = new BindingSource();
                    //   _source.DataSource = new DataTable();

                    gvSubSerial.DataSource = new DataTable();
                    gvMovement.DataSource = new DataTable();
                    gvSale.DataSource = new DataTable();

                    txtSerialNo.Focus();
                    return;

                }
                if (_InitialStageSearch.Rows.Count > 1)
                {
                    //  string return_message;
                    //   int returnm;

                    //   _serialfind = CHNLSVC.CustService.GetWarrantyMaster(txtSerialNo.Text, "", "", "", "", lblItem.Text, 0, out returnm, out return_message);
                    //-------

                    //sub- serials 

                    //

                    gvMultipleItem.DataSource = _InitialStageSearch;
                    gvMultipleItem.DataBind();
                    multiplepopup.Show();
                    //-------
                    //    GetItemAdvanceDetail(lblItem.Text.Trim());

                    return;
                }

                if (_InitialStageSearch.Rows.Count > 0)
                {

                    //call the warranty and period

                    //-------
                    //    ScriptManager.RegisterStartupScript(pnlPopup, pnlPopup.GetType(), "ShowModalPopup", "ShowModalPopup()", true);
                    //   ScriptManager.RegisterStartupScript(this, GetType(), "ShowModalPopup", "ShowModalPopup()", true); 

                    /*
                    dvResultUser.DataSource = _InitialStageSearch;
                    dvResultUser.DataBind();
                    ModalPopupExtender1.Show();*/


                    /*    dvResult.DataSource = _InitialStageSearch;
                        dvResult.DataBind();
                        ModalPopupExtender1.Show();*/
                    //    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSer4Itm);
                    //-------
                    string _item = Convert.ToString(_InitialStageSearch.Rows[0].Field<string>("ins_itm_cd"));
                    string _loc = Convert.ToString(_InitialStageSearch.Rows[0].Field<string>("INS_LOC"));

                    _serial = txtSerialNo.Text.Trim();
                    lblItem.Text = _item;
                    lblCurLocation.Text = _loc;
                    //get the serial and warranty

                    //get serial_1 and serial_2(dilshan on 05/04/2018)*********
                    string _serial_1 = Convert.ToString(_InitialStageSearch.Rows[0].Field<string>("ins_ser_1"));
                    string _serial_2 = Convert.ToString(_InitialStageSearch.Rows[0].Field<string>("ins_ser_2"));

                    lblSerial1.Text = _serial_1;
                    lblSerial2.Text = _serial_2;                    

                    //*********************************************************

                    // subserial

                    DataTable subserial = CHNLSVC.CommonSearch.GetSubSerial(_item, _serial);
                    gvSubSerial.DataSource = subserial;
                    gvSubSerial.DataBind();

                    //


                    //get warranty
                    DataTable dt = CHNLSVC.CommonSearch.GetSerialWarrantyDetails(_serial);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    if (dt.Rows.Count > 0)
                    {
                        Int32 tmp = 0;
                        string warranty_no = Convert.ToString(dt.Rows[0].Field<string>("irsm_warr_no"));
                       // int war_period = dt.Rows[0].Field<int>("irsm_warr_period");
                        Int32 war_period = Int32.TryParse(dt.Rows[0]["irsm_warr_period"].ToString(), out tmp) ? Convert.ToInt32(dt.Rows[0]["irsm_warr_period"].ToString()) : 0;
                        string warranty_p_s = war_period.ToString();
                        //  string warranty_period = Convert.ToString(dt.Rows[0].Field<string>("irsm_warr_period"));
                        Label15.Text = warranty_no;
                        Label17.Text = warranty_p_s;
                    }



                    //end

                    GetItemAdvanceDetail(_item);
                    DataTable _currentLocation = CHNLSVC.Inventory.GetSeriaLocation(_serialType, Session["UserCompanyCode"].ToString(), _serial, _item);
                    ClearBar2();
                    AssignCurrentLocation(_currentLocation);

                    DataTable _movement = CHNLSVC.Inventory.GetSeriaMovement(_serialType, Session["UserCompanyCode"].ToString(), _serial, _item);
                    AssignMovement(_movement);

                    if (_movement.Rows.Count > 0)
                    {
                        var _do = _movement.AsEnumerable().Where(x => x.Field<string>("ITH_DOC_TP") == "DO" || x.Field<string>("ITH_DOC_TP") == "DO").ToList().Select(y => y.Field<string>("ITH_OTH_DOCNO"));
                        if (_do != null)
                            if (_do.Count() > 0)
                            {
                                DataTable _mgTbl = new DataTable();
                                foreach (string _doc in _do)
                                {
                                    DataTable _sale = CHNLSVC.Sales.GetInvoiceDetail(Session["UserCompanyCode"].ToString(), _doc, _item);
                                    _mgTbl.Merge(_sale);
                                }

                                AssignSale(_mgTbl);
                                //    AssignSales(_mgTbl);
                            }
                    }

                    //get the unit cost 2015-12-29
                    getCost(_item, _serial);

                    //test  "HESPRADRKCCRSBK", "HA10EKC9H00158"

                    getstorage(_item, _serial);
                    total();
                    totalCost();
                    getnoofdays(_item, _serial);


                }

                Int32 serialtype = 0;
                Int32 SerialID = 0;
                if (_serialType == "SERIAL1")
                {
                    serialtype = 1;
                }
                else if (_serialType == "SERIAL2")
                {
                    serialtype = 2;
                }
                else if (_serialType == "SERIAL3")
                {
                    serialtype = 3;
                }
                else if (_serialType == "SERIAL4")
                {
                    serialtype = 4;
                }

                DataTable dtserialdata = CHNLSVC.Inventory.LoadSerialId(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtSerialNo.Text.Trim(), serialtype);
                if (dtserialdata.Rows.Count > 0)
                {
                    foreach (DataRow DDRitem in dtserialdata.Rows)
                    {
                        SerialID = Convert.ToInt32(DDRitem["INS_SER_ID"].ToString());
                    }
                }
                else
                {
                    List<InventorySerialN> _invSerList = CHNLSVC.Inventory.Get_INR_SER_DATA(new InventorySerialN() { Ins_ser_1 = txtSerialNo.Text.Trim(),Ins_available = -2 });
                    if (_invSerList.Count > 0)
                    {
                        SerialID = _invSerList[0].Ins_ser_id;
                    }
                }
                

                DataTable dtserialAlldata = CHNLSVC.Inventory.LoadSerialEnquiryData(SerialID);

                foreach (DataRow item in dtserialAlldata.Rows)
                {
                    DateTime grndate;
                    DateTime bldate;

                    if (!string.IsNullOrEmpty(item["irsm_orig_grn_dt"].ToString()))
                    {
                        grndate = Convert.ToDateTime(item["irsm_orig_grn_dt"].ToString());
                        if (grndate != new DateTime(9999, 1, 1))
                        {
                            lblgrndate.Text = grndate.ToString("dd/MMM/yyyy");

                        }
                    }
                    else
                    {
                        lblgrndate.Text = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(item["irsm_bl_dt"].ToString()))
                    {
                        bldate = Convert.ToDateTime(item["irsm_bl_dt"].ToString());
                        lblbldate.Text = bldate.ToString("dd/MMM/yyyy");
                    }
                    else
                    {
                        lblbldate.Text = string.Empty;
                    }

                    lblgrnno.Text = item["irsm_orig_grn_no"].ToString();
                    lblsupp.Text = item["irsm_orig_supp"].ToString();
                    lblsysbl.Text = item["irsm_sys_blno"].ToString();
                    lblbl.Text = item["irsm_blno"].ToString();
                    lbllcno.Text = item["irsm_sys_fin_no"].ToString();
                    if (!String.IsNullOrEmpty(lblgrnno.Text))
                    {
                        InventoryHeader _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(lblgrnno.Text.Trim());
                        if (_invHdr != null)
                        {
                            lblSupInvno.Text = _invHdr.ITH_SUP_INV_NO;
                            lblSupInvDt.Text = _invHdr.ITH_SUP_INV_DT.ToString("dd/MMM/yyyy");
                           // oHeader = CHNLSVC.Financial.GET_BL_HEADER_BY_DOC(Session["UserCompanyCode"].ToString(), txtDocNumber.Text.Trim(), "ALL");
                            ImportsBLHeader _blHdr = CHNLSVC.Financial.GET_BL_HEADER_BY_DOC(_invHdr.Ith_com, _invHdr.Ith_oth_docno, "ALL");
                            if (_blHdr != null)
                            {
                                lblsysbl.Text = _blHdr.Ib_doc_no;
                                lblbl.Text = _blHdr.Ib_bl_no;
                                lblbldate.Text = _blHdr.Ib_bl_dt.ToString("dd/MMM/yyyy");
                            }
                        }
                    }
                }
                //replace link button





            }
            else
            {
                displayMessage("Select Valid Serial Type");
            }
        }

        protected void lBtnAdvSerialSearch_Click(object sender, EventArgs e)
        {


            //replace link button
            if (cmbSerialType.SelectedIndex != 0)
            {
                lblCurLocation.Visible = false;
                DataTable _hasSer = CHNLSVC.MsgPortal.Get_INR_SER_DATA_ADVANCED(new InventorySerialN()
                {
                    Ins_com = Session["UserCompanyCode"].ToString(),
                    Ins_loc = Session["UserDefLoca"].ToString(),
                    Ins_available = 1,
                    Ins_ser_1 = txtSearchbyword.Text.Trim(),//"%"+ txtSearchbyword.Text.Trim() + "%",
                    Ser_tp = 0
                });

                Session["SerialDtl"] = _hasSer;
                // grdSerialData.DataSource = _hasSer;
                // grdSerialData.DataBind();

                grdSerial.DataSource = _hasSer;
                grdSerial.DataBind();

                taxDetailspopup.Show();


                // UserPopup2.Show();


                //    if (_hasSer != null)
                //    {
                //        lblCurLocation.Visible = true;
                //    }
                //    string _serialtype = cmbSerialType.Text.Trim();
                //    string _characterCase = cmbCaseType.Text.Trim();
                //    bool _isMatchWholeWord = chkWholeWord.Checked;

                //    if (txtSerialNoAdv.Text == "")
                //    {

                //        displayMessage("Please select the serial number");
                //        return;

                //    }
                //    if (_characterCase == "Lower")
                //    {
                //        //  multiplepopup1.Show();
                //    }


                //    string _serial = string.Empty;
                //    string _serialType = string.Empty;
                //    Int16 _isWholeWord = Convert.ToInt16(chkWholeWord.Checked ? 1 : 0);

                //    if (cmbSerialType.Text.Trim() == "Serial 1")
                //        _serialType = "SERIAL1";
                //    else if (cmbSerialType.Text.Trim() == "Serial 2")
                //        _serialType = "SERIAL2";
                //    else if (cmbSerialType.Text.Trim() == "Serial 3")
                //        _serialType = "SERIAL3";
                //    else if (cmbSerialType.Text.Trim() == "Serial 4")
                //        _serialType = "SERIAL4";

                //    if (cmbCaseType.Text == "Normal")
                //    {
                //        _serial = txtSerialNoAdv.Text.Trim();
                //    }
                //    else if (cmbCaseType.Text == "Upper")
                //    {
                //        _serial = txtSerialNoAdv.Text.Trim();
                //    }
                //    else if (cmbCaseType.Text == "Lower")
                //    {
                //        _serial = txtSerialNoAdv.Text.Trim();
                //    }
                //    if (_isWholeWord == 0) _serial += "%";


                //    _InitialStageSearch = CHNLSVC.Inventory.GetSerialItem(_serialType, Session["UserCompanyCode"].ToString(), _serial, _isWholeWord);

                //    if (_InitialStageSearch.Rows.Count == 0)
                //    {

                //        // Page.ClientScript.RegisterStartupScript(this.GetType(),"toastr_message", "toastr.error('There was an error', 'Error')", true);
                //        //   Page.ClientScript.RegisterStartupScript(this.GetType(), "showStickyWarningToast", "showStickyWarningToast('hello')", true);
                //        string msg = "There is no such serial available in the system for the given criteria";
                //        ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);

                //        //  divscro.Visible = true;


                //        lblItem.Text = string.Empty;
                //        GetItemAdvanceDetail(lblItem.Text.Trim());
                //        AssignCurrentLocation(null);
                //        // BindingSource _source = new BindingSource();
                //        //   _source.DataSource = new DataTable();

                //        gvSubSerial.DataSource = new DataTable();
                //        gvMovement.DataSource = new DataTable();
                //        gvSale.DataSource = new DataTable();

                //        txtSerialNoAdv.Focus();
                //        return;

                //    }
                //    if (_InitialStageSearch.Rows.Count > 1)
                //    {
                //        //  string return_message;
                //        //   int returnm;

                //        //   _serialfind = CHNLSVC.CustService.GetWarrantyMaster(txtSerialNo.Text, "", "", "", "", lblItem.Text, 0, out returnm, out return_message);
                //        //-------

                //        //sub- serials 

                //        //

                //        gvMultipleItem.DataSource = _InitialStageSearch;
                //        gvMultipleItem.DataBind();
                //        multiplepopup.Show();
                //        //-------
                //        //    GetItemAdvanceDetail(lblItem.Text.Trim());

                //        return;
                //    }

                //    if (_InitialStageSearch.Rows.Count > 0)
                //    {

                //        //call the warranty and period

                //        //-------
                //        //    ScriptManager.RegisterStartupScript(pnlPopup, pnlPopup.GetType(), "ShowModalPopup", "ShowModalPopup()", true);
                //        //   ScriptManager.RegisterStartupScript(this, GetType(), "ShowModalPopup", "ShowModalPopup()", true); 

                //        /*
                //        dvResultUser.DataSource = _InitialStageSearch;
                //        dvResultUser.DataBind();
                //        ModalPopupExtender1.Show();*/


                //        /*    dvResult.DataSource = _InitialStageSearch;
                //            dvResult.DataBind();
                //            ModalPopupExtender1.Show();*/
                //        //    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSer4Itm);
                //        //-------
                //        string _item = Convert.ToString(_InitialStageSearch.Rows[0].Field<string>("ins_itm_cd"));

                //        _serial = txtSerialNoAdv.Text.Trim();
                //        lblItem.Text = _item;
                //        //get the serial and warranty

                //        // subserial

                //        DataTable subserial = CHNLSVC.CommonSearch.GetSubSerial(_item, _serial);
                //        gvSubSerial.DataSource = subserial;
                //        gvSubSerial.DataBind();

                //        //


                //        //get warranty
                //        DataTable dt = CHNLSVC.CommonSearch.GetSerialWarrantyDetails(_serial);
                //        GridView1.DataSource = dt;
                //        GridView1.DataBind();

                //        if (dt.Rows.Count > 0)
                //        {
                //            string warranty_no = Convert.ToString(dt.Rows[0].Field<string>("irsm_warr_no"));
                //            int war_period = dt.Rows[0].Field<int>("irsm_warr_period");
                //            string warranty_p_s = war_period.ToString();
                //            //  string warranty_period = Convert.ToString(dt.Rows[0].Field<string>("irsm_warr_period"));
                //            Label15.Text = warranty_no;
                //            Label17.Text = warranty_p_s;
                //        }



                //        //end

                //        GetItemAdvanceDetail(_item);
                //        DataTable _currentLocation = CHNLSVC.Inventory.GetSeriaLocation(_serialType, Session["UserCompanyCode"].ToString(), _serial, _item);
                //        ClearBar2();
                //        AssignCurrentLocation(_currentLocation);

                //        DataTable _movement = CHNLSVC.Inventory.GetSeriaMovement(_serialType, Session["UserCompanyCode"].ToString(), _serial, _item);
                //        AssignMovement(_movement);

                //        if (_movement.Rows.Count > 0)
                //        {
                //            var _do = _movement.AsEnumerable().Where(x => x.Field<string>("ITH_DOC_TP") == "DO" || x.Field<string>("ITH_DOC_TP") == "DO").ToList().Select(y => y.Field<string>("ITH_OTH_DOCNO"));
                //            if (_do != null)
                //                if (_do.Count() > 0)
                //                {
                //                    DataTable _mgTbl = new DataTable();
                //                    foreach (string _doc in _do)
                //                    {
                //                        DataTable _sale = CHNLSVC.Sales.GetInvoiceDetail(Session["UserCompanyCode"].ToString(), _doc, _item);
                //                        _mgTbl.Merge(_sale);
                //                    }

                //                    AssignSale(_mgTbl);
                //                    //    AssignSales(_mgTbl);
                //                }
                //        }

                //        //get the unit cost 2015-12-29
                //        getCost(_item, _serial);

                //        //test  "HESPRADRKCCRSBK", "HA10EKC9H00158"

                //        getstorage(_item, _serial);
                //        total();
                //        totalCost();
                //        getnoofdays(_item, _serial);


                //    }

                //    Int32 serialtype = 0;
                //    Int32 SerialID = 0;
                //    if (_serialType == "SERIAL1")
                //    {
                //        serialtype = 1;
                //    }
                //    else if (_serialType == "SERIAL2")
                //    {
                //        serialtype = 2;
                //    }
                //    else if (_serialType == "SERIAL3")
                //    {
                //        serialtype = 3;
                //    }
                //    else if (_serialType == "SERIAL4")
                //    {
                //        serialtype = 4;
                //    }

                //    DataTable dtserialdata = CHNLSVC.Inventory.LoadSerialId(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtSerialNoAdv.Text.Trim(), serialtype);

                //    foreach (DataRow DDRitem in dtserialdata.Rows)
                //    {
                //        SerialID = Convert.ToInt32(DDRitem["INS_SER_ID"].ToString());
                //    }

                //    DataTable dtserialAlldata = CHNLSVC.Inventory.LoadSerialEnquiryData(SerialID);

                //    foreach (DataRow item in dtserialAlldata.Rows)
                //    {
                //        DateTime grndate;
                //        DateTime bldate;

                //        if (!string.IsNullOrEmpty(item["irsm_orig_grn_dt"].ToString()))
                //        {
                //            grndate = Convert.ToDateTime(item["irsm_orig_grn_dt"].ToString());
                //            if (grndate != new DateTime(9999, 1, 1))
                //            {
                //                lblgrndate.Text = grndate.ToString("dd/MMM/yyyy");

                //            }
                //        }
                //        else
                //        {
                //            lblgrndate.Text = string.Empty;
                //        }

                //        if (!string.IsNullOrEmpty(item["irsm_bl_dt"].ToString()))
                //        {
                //            bldate = Convert.ToDateTime(item["irsm_bl_dt"].ToString());
                //            lblbldate.Text = bldate.ToString("dd/MMM/yyyy");
                //        }
                //        else
                //        {
                //            lblbldate.Text = string.Empty;
                //        }

                //        lblgrnno.Text = item["irsm_orig_grn_no"].ToString();
                //        lblsupp.Text = item["irsm_orig_supp"].ToString();
                //        lblsysbl.Text = item["irsm_sys_blno"].ToString();
                //        lblbl.Text = item["irsm_blno"].ToString();
                //        lbllcno.Text = item["irsm_sys_fin_no"].ToString();
                //    }
                //    //replace link button
                //}
                //else
                //{
                //    displayMessage("Select Valid Serial Type");
                //}
            }
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            /*     if (txtSerialNo.Text == "")
                 {
             
                 }
                 else {
                //     ModalPopupExtender1.Show();
                 }*/



            if (txtClearlconformmessageValue.Value == "1")
            {


                ClearBar1(true);
                ClearDatasources();
                ClearBar2();
                clearBar3();
                clearBar4();
                textBoxClear();
                ClearGrnData();
            }

        }

        private void ClearGrnData()
        {
            lblgrnno.Text = "";
            lblgrndate.Text = "";
            lblsupp.Text = "";
            lblsysbl.Text = "";
            lblbl.Text = "";
            lblbldate.Text = "";
            lbllcno.Text = "";
            lblSupInvno.Text = "";
            lblSupInvDt.Text = "";
            lblSerial1.Text = "";
            lblSerial2.Text = "";
        }
        protected void Button1_Click3(object sender, EventArgs e)
        {
            // dvResult.DataSource = _InitialStageSearch;
            //  dvResult.DataBind();

        }

        protected void dvResult_PageIndexChanged(object sender, EventArgs e)
        {

            //  dvResult.DataSource = _InitialStageSearch;

        }

        protected void dvResultUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            //  ClearDatasources();

            ClearBar1(true);
            ClearDatasources();
            ClearBar2();
            clearBar3();
            clearBar4();
        }


        //---multiple item select

        private void TakeItemAndLoadSerialDetails(int _row)
        {
            try
            {
                //    if (_InitialStageSearch.Rows.Count == 0) return;

                string _item = Label6.Text;
                string _serial = Label8.Text;
                txtSerialNo.Text = _serial;
                lblItem.Text = _item;

                string _serialtype = cmbSerialType.Text.Trim();
                string _characterCase = cmbCaseType.Text.Trim();
                bool _isMatchWholeWord = chkWholeWord.Checked;

                if (string.IsNullOrEmpty(txtSerialNo.Text))
                {
                    //   MessageBox.Show("Please select the serial no", "Serial No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtSerialNo.Clear();
                    txtSerialNo.Focus();
                    return;
                }


                string _serialType = string.Empty;
                Int16 _isWholeWord = Convert.ToInt16(chkWholeWord.Checked ? 1 : 0);

                if (cmbSerialType.Text.Trim() == "Serial 1")
                    _serialType = "SERIAL1";
                else if (cmbSerialType.Text.Trim() == "Serial 2")
                    _serialType = "SERIAL2";
                else if (cmbSerialType.Text.Trim() == "Serial 3")
                    _serialType = "SERIAL3";
                else if (cmbSerialType.Text.Trim() == "Serial 4")
                    _serialType = "SERIAL4";

                if (cmbCaseType.Text == "Normal")
                    _serial = txtSerialNo.Text.Trim();
                else if (cmbCaseType.Text == "Upper")
                    _serial = txtSerialNo.Text.Trim().ToUpper();
                else if (cmbCaseType.Text == "Lower")
                    _serial = txtSerialNo.Text.Trim().ToLower();


                GetItemAdvanceDetail(_item);
                DataTable _currentLocation = CHNLSVC.Inventory.GetSeriaLocation(_serialType, Session["UserCompanyCode"].ToString(), _serial, _item);
                ClearBar2();
                AssignCurrentLocation(_currentLocation);

                //     ClearBar7();
                DataTable _movement = CHNLSVC.Inventory.GetSeriaMovement(_serialType, Session["UserCompanyCode"].ToString(), _serial, _item);
                AssignMovement(_movement);

                if (_movement.Rows.Count > 0)
                {
                    var _do = _movement.AsEnumerable().Where(x => x.Field<string>("ITH_DOC_TP") == "DO" || x.Field<string>("ITH_DOC_TP") == "DO").ToList().Select(y => y.Field<string>("ITH_OTH_DOCNO"));

                    if (_do != null)
                        if (_do.Count() > 0)
                        {
                            DataTable _mgTbl = new DataTable();
                            foreach (string _doc in _do)
                            {
                                DataTable _sale = CHNLSVC.Sales.GetInvoiceDetail(Session["UserCompanyCode"].ToString(), _doc, _item);
                                _mgTbl.Merge(_sale);
                            }

                            AssignSale(_mgTbl);
                        }
                }
                //   pnlMultipleItem.Visible = false;
                //   return;
            }
            catch (Exception ex)
            {

                // MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void TakeItemAndLoadSerialDetailsReport(int _row)
        {
            try
            {
                //    if (_InitialStageSearch.Rows.Count == 0) return;

                string _item = Label6.Text;
                string _serial = Label8.Text;
                txtSerialNo.Text = _serial;
                lblItem.Text = _item;

                string _serialtype = cmbSerialType.Text.Trim();
                string _characterCase = cmbCaseType.Text.Trim();
                bool _isMatchWholeWord = chkWholeWord.Checked;

                if (string.IsNullOrEmpty(txtSerialNo.Text))
                {
                    //   MessageBox.Show("Please select the serial no", "Serial No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtSerialNo.Clear();
                    txtSerialNo.Focus();
                    return;
                }


                string _serialType = string.Empty;
                Int16 _isWholeWord = Convert.ToInt16(chkWholeWord.Checked ? 1 : 0);

                if (cmbSerialType.Text.Trim() == "Serial 1")
                    _serialType = "SERIAL1";
                else if (cmbSerialType.Text.Trim() == "Serial 2")
                    _serialType = "SERIAL2";
                else if (cmbSerialType.Text.Trim() == "Serial 3")
                    _serialType = "SERIAL3";
                else if (cmbSerialType.Text.Trim() == "Serial 4")
                    _serialType = "SERIAL4";

                if (cmbCaseType.Text == "Normal")
                    _serial = txtSerialNo.Text.Trim();
                else if (cmbCaseType.Text == "Upper")
                    _serial = txtSerialNo.Text.Trim().ToUpper();
                else if (cmbCaseType.Text == "Lower")
                    _serial = txtSerialNo.Text.Trim().ToLower();


                GetItemAdvanceDetail(_item);
                DataTable _currentLocation = CHNLSVC.Inventory.GetSeriaLocation(_serialType, Session["UserCompanyCode"].ToString(), _serial, _item);
                ClearBar2();
                AssignCurrentLocation(_currentLocation);

                //     ClearBar7();
                DataTable _movement = CHNLSVC.Inventory.GetSeriaMovement(_serialType, Session["UserCompanyCode"].ToString(), _serial, _item);
                AssignMovement(_movement);

                if (_movement.Rows.Count > 0)
                {
                    var _do = _movement.AsEnumerable().Where(x => x.Field<string>("ITH_DOC_TP") == "DO" || x.Field<string>("ITH_DOC_TP") == "DO").ToList().Select(y => y.Field<string>("ITH_OTH_DOCNO"));

                    if (_do != null)
                        if (_do.Count() > 0)
                        {
                            DataTable _mgTbl = new DataTable();
                            foreach (string _doc in _do)
                            {
                                DataTable _sale = CHNLSVC.Sales.GetInvoiceDetail(Session["UserCompanyCode"].ToString(), _doc, _item);
                                _mgTbl.Merge(_sale);
                            }

                            AssignSale(_mgTbl);
                        }
                }
                //   pnlMultipleItem.Visible = false;
                //   return;

                InvReportPara _invRepPara = new InvReportPara();

                _invRepPara._GlbCompany = Session["UserCompanyCode"].ToString();
                _invRepPara._GlbDocNo = txtSerialNo.Text;
                _invRepPara._GlbUserID = Session["UserID"].ToString();

                DataTable rep_det = CHNLSVC.Inventory.GetReportParam(_invRepPara._GlbCompany, _invRepPara._GlbUserID);
                string powered_by = "";
                foreach (DataRow row in rep_det.Rows)
                {
                    powered_by = row["MC_IT_POWERED"].ToString();

                }
//------------------------------------ Multiple Print Start

                //Report Print
                string url = "";
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                clsInventory inv = new clsInventory();
                Serial_History _serialHistory = new Serial_History();
                //Set Report data Tables---------------------------------------------

                DataTable OtherDetails = new DataTable();
                DataRow dr;

                //MasterCompany _newCom = new MasterCompany();
                //_newCom = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                //Session["GlbReportCompName"] = _newCom/

                // GLOB_DataTable = new DataTable();
                DataTable shippingDetails = new DataTable();
                DataTable costDetails = new DataTable();

                OtherDetails.Columns.Add("warranty_no", typeof(string));
                OtherDetails.Columns.Add("warranty_p_s", typeof(string));
                OtherDetails.Columns.Add("user", typeof(string));
                OtherDetails.Columns.Add("com", typeof(string));
                OtherDetails.Columns.Add("currency", typeof(string));
                OtherDetails.Columns.Add("MC_IT_POWERED", typeof(string));
                OtherDetails.Columns.Add("RECIEVED", typeof(string));

                dr = OtherDetails.NewRow();

               // dr["warranty_no"] = warranty_no;
               // dr["warranty_p_s"] = warranty_p_s;
                dr["user"] = Session["UserID"].ToString();
                dr["com"] = _invRepPara._GlbCompany;
                dr["currency"] = _currCode;
                dr["MC_IT_POWERED"] = powered_by;

                if (_currentLocation == null)
                {
                    dr["RECIEVED"] = string.Empty;

                }
                else if (_currentLocation.Rows.Count > 0)
                {
                    dr["RECIEVED"] = Convert.ToDateTime(_currentLocation.Rows[0].Field<DateTime>("INS_DOC_DT")).Date.ToShortDateString();

                }

                OtherDetails.Rows.Add(dr);

                //GLOB_DataTable.Clear();

                _InitialStageSearch = (DataTable) Session["_InitialStageSearch"];
                _serialHistory.Database.Tables["ITEM_DETAILS"].SetDataSource(_InitialStageSearch);
                _serialHistory.Database.Tables["SERIAL_DETAILS"].SetDataSource(_currentLocation.AsEnumerable().Take(1).CopyToDataTable());
                // _serialHistory.Database.Tables["SERIAL_DETAILS"].SetDataSource(tblWarrentyDetails);
                _serialHistory.Database.Tables["SERIAL_MOVEMENT_DTL"].SetDataSource(_movement);
                _serialHistory.Database.Tables["OTHER_DETAIL"].SetDataSource(OtherDetails);



                PrintPDF(targetFileName, _serialHistory);
                url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                CHNLSVC.MsgPortal.SaveReportErrorLog("CusdecAsses", "CusdecAsses", "Run Ok", Session["UserID"].ToString());

//---------------------------------------------------- Multiple Print End

            }
            catch (Exception ex)
            {

                // MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        //----end



        protected void gvMultipleItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = gvMultipleItem.SelectedRow.RowIndex;
                Label6.Text = gvMultipleItem.SelectedRow.Cells[2].Text;
                Label8.Text = gvMultipleItem.SelectedRow.Cells[1].Text;

                lblSerial1.Text = gvMultipleItem.SelectedRow.Cells[6].Text;
                lblSerial2.Text = gvMultipleItem.SelectedRow.Cells[7].Text;
                TakeItemAndLoadSerialDetails(index);

                getCost(Label6.Text, Label8.Text);

                //test  "HESPRADRKCCRSBK", "HA10EKC9H00158"

                getstorage(Label6.Text, Label8.Text);
                total();
                totalCost();
                getnoofdays(Label6.Text, Label8.Text);



                /*     if (index <= 0) return;
                     if (index != -1)
                     {
                         //      TakeItemAndLoadSerialDetails(index);
                     }*/

                if (txtPrintconformmessageValue.Value == "1")
                {
                    TakeItemAndLoadSerialDetailsReport(index);

                    

                }
                lblCurLocation.Text = "";
                var _hasSer = CHNLSVC.Inventory.Get_INR_SER_DATA(new InventorySerialN()
                {
                   // Ins_com = Session["UserCompanyCode"].ToString(),
                   // Ins_loc = Session["UserDefLoca"].ToString(),
                    Ins_available = 0,
                    Ins_ser_1 = txtSerialNo.Text.Trim(),
                    Ser_tp = 1
                }).FirstOrDefault();
                if (_hasSer != null)
                {
                    lblCurLocation.Text = _hasSer.Ins_loc;
                }
                if (!string.IsNullOrEmpty(Label8.Text))
                {
                    LoadGrnData(Label8.Text);
                }
            }
            catch (Exception ex)
            {

            }

            //  string h = "hi";
        }

        protected void btnShowPopup_Command(object sender, CommandEventArgs e)
        {

        }
        protected void LinkButtonsubserial_Click(object sender, EventArgs e)
        {

            UserPopoup.Show();

        }
        /*  protected void gvMovement_DataBound(object sender, EventArgs e)
         {

         }
         */
        protected void lbtnComp_Click(object sender, EventArgs e)
        {
            string itmCd = lblItem.Text;
            string serialNo = txtSerialNo.Text;
            string loc = lblCurLocation.Text;

            if (!string.IsNullOrEmpty(itmCd))
            {
                List<ItemKitComponent> _kitComs = new List<ItemKitComponent>();
                ItemKitComponent _kitCom = new ItemKitComponent() { MIKC_ITM_CODE_MAIN = itmCd.ToUpper(), MIKC_ACTIVE = 1 };
                _kitComs = CHNLSVC.Inventory.GetItemKitComponentSplit(_kitCom);
                DataTable dt = ConvertToDatatable(_kitComs, serialNo, loc);
                Session["CompDet"] = dt;
                if (dt.Rows.Count > 0)
                {
                    gdvResDet.DataSource = dt;
                    gdvResDet.DataBind();

                    //DataControlField units = gdvResDet.Columns[4];
                    //units.ItemStyle.HorizontalAlign = HorizontalAlign.Right;

                    //DataControlField cost = gdvResDet.Columns[6];
                    //cost.ItemStyle.HorizontalAlign = HorizontalAlign.Right;

                    ItmResPopup.Show();
                }
                else
                {
                    DisplayMessage("No Component Details!", 2);
                }
            }
            else
            {
                DisplayMessage("Enter Serial number", 2);
            }
        }

        protected void gdvResDet_RowDataBound(object o, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
                //e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        static DataTable ConvertToDatatable(List<ItemKitComponent> list, string serialNo, string loc)
        {
            DataTable dt = new DataTable();




            dt.Columns.Add("Item");
            dt.Columns.Add("Component");
            dt.Columns.Add("Description");
            dt.Columns.Add("Status");
            dt.Columns.Add("No of Units");
            // dt.Columns.Add("Serial #");
            //dt.Columns.Add("Cost");
            //dt.Columns.Add("Loc");
            //dt.Columns.Add("Loc_Desc");
            foreach (var item in list)
            {
                var row = dt.NewRow();

                row["Item"] = item.MIKC_ITM_CODE_MAIN;
                row["Component"] = item.MIKC_ITM_CODE_COMPONENT;
                row["Description"] = item.MIKC_DESC_COMPONENT;
                row["Status"] = "GOOD";
                row["No of Units"] = item.MIKC_NO_OF_UNIT;
                //row["Serial #"] = serialNo;
                //row["Cost"] = item.MIKC_COST;
                //row["Loc"] = loc;
                //row["Loc_Desc"] = item.MIKC_LOC_DES;

                dt.Rows.Add(row);
            }

            return dt;
        }

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

        protected void gdvResDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvResDet.PageIndex = e.NewPageIndex;
            DataTable _result = null;

            _result = (DataTable)Session["CompDet"];
            gdvResDet.DataSource = _result;
            gdvResDet.DataSource = null;
            gdvResDet.DataBind();
            gdvResDet.PageIndex = 0;
            ItmResPopup.Show();
        }

        protected void grdSerial_RowDataBound(object o, GridViewRowEventArgs e)
        {

        }
        protected void grdSerial_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string ID = grdSerial.SelectedRow.Cells[1].Text;

            string aa = grdSerial.Rows[0].Cells[2].Text;

            string Name = grdSerial.SelectedRow.Cells[3].Text;
            txtSerialNo.Text = Name;
            taxDetailspopup.Hide();
            return;

        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
         {

            lblCurLocation.Visible = false;
            DataTable _serialNo = CHNLSVC.MsgPortal.Get_INR_SER_DATA_ADVANCED(new InventorySerialN()
            {
                Ins_com = Session["UserCompanyCode"].ToString(),
                Ins_loc = Session["UserDefLoca"].ToString(),
                Ins_available = 1,
                Ins_ser_1 = txtSearchbyword.Text.Trim(),//"%"+ txtSearchbyword.Text.Trim()+"%",
                Ser_tp = 0
            });

            Session["SerialDtl"] = _serialNo;

            if (_serialNo.Rows.Count > 0)
            {
                // grdSerialData.DataSource = _serialNo;
                // grdSerialData.DataSource = new int[]{};
                grdSerial.DataSource = _serialNo;

            }
            else
            {
                //grdSerialData.DataSource = null;
                grdSerial.DataSource = null;
            }

            // grdSerialData.DataBind();
            grdSerial.DataBind();

            taxDetailspopup.Show();

        }

        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            //UserPopup2.Hide();
        }

        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {

            lblCurLocation.Visible = false;
            DataTable _serialNo = CHNLSVC.MsgPortal.Get_INR_SER_DATA_ADVANCED(new InventorySerialN()
            {
                Ins_com = Session["UserCompanyCode"].ToString(),
                Ins_loc = Session["UserDefLoca"].ToString(),
                Ins_available = 1,
                Ins_ser_1 = txtSearchbyword.Text.Trim(),//"%" + txtSearchbyword.Text.Trim() + "%",
                Ser_tp = 0
            });

            if (_serialNo.Rows.Count > 0)
            {
                grdSerial.DataSource = _serialNo;

            }
            else
            {
                grdSerial.DataSource = null;
            }

            grdSerial.DataBind();
            taxDetailspopup.Show();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            taxDetailspopup.Hide();
        }


        public string sortOrder
        {
            get
            {
                if (ViewState["sortOrder"].ToString() == "desc")
                {
                    ViewState["sortOrder"] = "asc";
                }
                else
                {
                    ViewState["sortOrder"] = "desc";
                }
                return ViewState["sortOrder"].ToString();
            }
            set
            {
                ViewState["sortOrder"] = value;
            }
        }


        protected void gvMovement_Sorting(object sender, GridViewSortEventArgs e)
        {
            // SetSortDirection("desc");
            _sortDirection = sortOrder;
            //Sort the data.
            dataTable = (DataTable)Session["dataTable"];
            if (dataTable != null)
            {
                if (dataTable.Rows.Count > 0)
                {
                    dataTable.DefaultView.Sort = e.SortExpression + " " + _sortDirection;
                    gvMovement.DataSource = dataTable;
                    gvMovement.DataBind();
                    Label lblCostCd = (Label)gvMovement.HeaderRow.FindControl("lblCostCd");
                    lblCostCd.ForeColor = Color.Purple;
                    lblCostCd.Text = "Unit Cost(" + _currCode + ")";
                    SortDireaction = _sortDirection;
                }
            }

        }

        protected void gvMovement_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {

                gvMovement.PageIndex = e.NewPageIndex;
                gvMovement.DataSource = null;
                gvMovement.DataSource = (DataTable)Session["dataTable"]; ;
                gvMovement.DataBind();
                Label lblCostCd = (Label)gvMovement.HeaderRow.FindControl("lblCostCd");
                lblCostCd.ForeColor = Color.Purple;
                lblCostCd.Text = "Unit Cost(" + _currCode + ")";
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lBtnSelect_Click(object sender, EventArgs e)
        {
            Button btn = (Button)(sender);
            string _serialNo = btn.CommandArgument;
            txtSerialNo.Text = _serialNo;
            taxDetailspopup.Hide();
        }

        protected void txtSerialNo_TextChanged(object sender, EventArgs e)
        {
            LinkButton1_Click(sender, e);
        }

        protected void grdSerial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSerial.PageIndex = e.NewPageIndex;
            DataTable _result = null;

            _result = (DataTable)Session["SerialDtl"];
            grdSerial.DataSource = _result;
            grdSerial.DataSource = null;
            grdSerial.DataBind();
            //grdSerial.PageIndex = 1;

            lbtnSearch_Click(sender, null);

            taxDetailspopup.Show();
        }

        protected void lBtnPrint_Click(object sender, EventArgs e)
        {
            if (txtPrintconformmessageValue.Value == "1")
            {
                {

                    DataTable tblWarrentyDetails = null;
                    DataTable subserial = null;
                    DataTable _currentLocation = null;
                    DataTable _movement = null;

                    string warranty_no = "";
                    string warranty_p_s = "";

                    //replace link button
                    if (cmbSerialType.SelectedIndex != 0)
                    {
                        lblCurLocation.Visible = false;
                        var _hasSer = CHNLSVC.Inventory.Get_INR_SER_DATA(new InventorySerialN()
                        {
                            Ins_com = Session["UserCompanyCode"].ToString(),
                            Ins_loc = Session["UserDefLoca"].ToString(),
                            Ins_available = 1,
                            Ins_ser_1 = txtSerialNo.Text.Trim(),
                            Ser_tp = 0
                        }).FirstOrDefault();
                        if (_hasSer != null)
                        {
                            lblCurLocation.Visible = true;
                        }
                        string _serialtype = cmbSerialType.Text.Trim();
                        string _characterCase = cmbCaseType.Text.Trim();
                        bool _isMatchWholeWord = chkWholeWord.Checked;

                        if (txtSerialNo.Text == "")
                        {

                            displayMessage("Please select the serial number");
                            return;

                        }
                        if (_characterCase == "Lower")
                        {
                            //  multiplepopup1.Show();
                        }

                        /*     if (txtSerialNo.Text.Substring(0, 1) == "%" && chkWholeWord.Checked == false)
                             {
                               //  MessageBox.Show("You can not add % as starting character");
                                 return;
                             }
                                 */
                        string _serial = string.Empty;
                        string _serialType = string.Empty;
                        Int16 _isWholeWord = Convert.ToInt16(chkWholeWord.Checked ? 1 : 0);

                        if (cmbSerialType.Text.Trim() == "Serial 1")
                            _serialType = "SERIAL1";
                        else if (cmbSerialType.Text.Trim() == "Serial 2")
                            _serialType = "SERIAL2";
                        else if (cmbSerialType.Text.Trim() == "Serial 3")
                            _serialType = "SERIAL3";
                        else if (cmbSerialType.Text.Trim() == "Serial 4")
                            _serialType = "SERIAL4";

                        if (cmbCaseType.Text == "Normal")
                        {
                            _serial = txtSerialNo.Text.Trim();
                        }
                        else if (cmbCaseType.Text == "Upper")
                        {
                            _serial = txtSerialNo.Text.Trim();
                        }
                        else if (cmbCaseType.Text == "Lower")
                        {
                            _serial = txtSerialNo.Text.Trim();
                        }
                        if (_isWholeWord == 0) _serial += "%";


                        _InitialStageSearch = CHNLSVC.Inventory.GetSerialItem(_serialType, Session["UserCompanyCode"].ToString(), _serial, _isWholeWord);
                        if (_InitialStageSearch.Rows.Count > 0)
                        {
                            for (int x = _InitialStageSearch.Rows.Count - 1; x >= 0; x--)
                            {
                                DataRow dr = _InitialStageSearch.Rows[x];
                                if (dr["INS_ITM_CD"].ToString() != lblItem.Text.Trim().ToUpper())
                                {
                                    dr.Delete();
                                }
                            }
                            _InitialStageSearch.AcceptChanges();
                        }
                        Session["_InitialStageSearch"] = _InitialStageSearch as DataTable;

                        if (_InitialStageSearch.Rows.Count == 0)
                        {

                            // Page.ClientScript.RegisterStartupScript(this.GetType(),"toastr_message", "toastr.error('There was an error', 'Error')", true);
                            //   Page.ClientScript.RegisterStartupScript(this.GetType(), "showStickyWarningToast", "showStickyWarningToast('hello')", true);
                            string msg = "There is no such serial available in the system for the given criteria";
                            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);

                            //  divscro.Visible = true;


                            lblItem.Text = string.Empty;
                            GetItemAdvanceDetail(lblItem.Text.Trim());
                            AssignCurrentLocation(null);
                            // BindingSource _source = new BindingSource();
                            //   _source.DataSource = new DataTable();

                            gvSubSerial.DataSource = new DataTable();
                            gvMovement.DataSource = new DataTable();
                            gvSale.DataSource = new DataTable();

                            txtSerialNo.Focus();
                            return;

                        }
                        //if (_InitialStageSearch.Rows.Count > 1)
                        //{
                        //    //  string return_message;
                        //    //   int returnm;

                        //    //   _serialfind = CHNLSVC.CustService.GetWarrantyMaster(txtSerialNo.Text, "", "", "", "", lblItem.Text, 0, out returnm, out return_message);
                        //    //-------

                        //    //sub- serials 

                        //    //

                        //    gvMultipleItem.DataSource = _InitialStageSearch;
                        //    gvMultipleItem.DataBind();
                        //    multiplepopup.Show();
                        //    //-------
                        //    //    GetItemAdvanceDetail(lblItem.Text.Trim());

                        //    return;
                        //}

                        if (_InitialStageSearch.Rows.Count > 0)
                        {

                            //call the warranty and period

                            //-------
                            //    ScriptManager.RegisterStartupScript(pnlPopup, pnlPopup.GetType(), "ShowModalPopup", "ShowModalPopup()", true);
                            //   ScriptManager.RegisterStartupScript(this, GetType(), "ShowModalPopup", "ShowModalPopup()", true); 

                            /*
                            dvResultUser.DataSource = _InitialStageSearch;
                            dvResultUser.DataBind();
                            ModalPopupExtender1.Show();*/


                            /*    dvResult.DataSource = _InitialStageSearch;
                                dvResult.DataBind();
                                ModalPopupExtender1.Show();*/
                            //    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSer4Itm);
                            //-------
                            string _item = Convert.ToString(_InitialStageSearch.Rows[0].Field<string>("ins_itm_cd"));

                            _serial = txtSerialNo.Text.Trim();
                            lblItem.Text = _item;
                            //get the serial and warranty

                            // subserial

                            subserial = CHNLSVC.CommonSearch.GetSubSerial(_item, _serial);
                            gvSubSerial.DataSource = subserial;
                            gvSubSerial.DataBind();

                            //


                            //get warranty
                            tblWarrentyDetails = CHNLSVC.CommonSearch.GetSerialWarrantyDetails(_serial);
                            GridView1.DataSource = tblWarrentyDetails;
                            GridView1.DataBind();



                            if (tblWarrentyDetails.Rows.Count > 0)
                            {
                                warranty_no = Convert.ToString(tblWarrentyDetails.Rows[0].Field<string>("irsm_warr_no"));
                                int war_period = tblWarrentyDetails.Rows[0].Field<int>("irsm_warr_period");
                                warranty_p_s = war_period.ToString();
                                //  string warranty_period = Convert.ToString(dt.Rows[0].Field<string>("irsm_warr_period"));
                                Label15.Text = warranty_no;
                                Label17.Text = warranty_p_s;
                            }



                            //end

                            GetItemAdvanceDetail(_item);
                            _currentLocation = CHNLSVC.Inventory.GetSeriaLocation(_serialType, Session["UserCompanyCode"].ToString(), _serial, _item);
                            ClearBar2();
                            AssignCurrentLocation(_currentLocation);

                            //Serial Movements
                            _movement = CHNLSVC.Inventory.GetSeriaMovement(_serialType, Session["UserCompanyCode"].ToString(), _serial, _item);
                            AssignMovement(_movement);

                            if (_movement.Rows.Count > 0)
                            {
                                var _do = _movement.AsEnumerable().Where(x => x.Field<string>("ITH_DOC_TP") == "DO" || x.Field<string>("ITH_DOC_TP") == "DO").ToList().Select(y => y.Field<string>("ITH_OTH_DOCNO"));
                                if (_do != null)
                                    if (_do.Count() > 0)
                                    {
                                        DataTable _mgTbl = new DataTable();
                                        foreach (string _doc in _do)
                                        {
                                            DataTable _sale = CHNLSVC.Sales.GetInvoiceDetail(Session["UserCompanyCode"].ToString(), _doc, _item);
                                            _mgTbl.Merge(_sale);
                                        }

                                        AssignSale(_mgTbl);
                                        //    AssignSales(_mgTbl);
                                    }
                            }

                            //get the unit cost 2015-12-29
                            getCost(_item, _serial);

                            //test  "HESPRADRKCCRSBK", "HA10EKC9H00158"

                            getstorage(_item, _serial);
                            total();
                            totalCost();
                            getnoofdays(_item, _serial);


                        }

                        Int32 serialtype = 0;
                        Int32 SerialID = 0;
                        if (_serialType == "SERIAL1")
                        {
                            serialtype = 1;
                        }
                        else if (_serialType == "SERIAL2")
                        {
                            serialtype = 2;
                        }
                        else if (_serialType == "SERIAL3")
                        {
                            serialtype = 3;
                        }
                        else if (_serialType == "SERIAL4")
                        {
                            serialtype = 4;
                        }

                        DataTable dtserialdata = CHNLSVC.Inventory.LoadSerialId(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtSerialNo.Text.Trim(), serialtype);

                        foreach (DataRow DDRitem in dtserialdata.Rows)
                        {
                            SerialID = Convert.ToInt32(DDRitem["INS_SER_ID"].ToString());
                        }

                        DataTable dtserialAlldata = CHNLSVC.Inventory.LoadSerialEnquiryData(SerialID);

                        foreach (DataRow item in dtserialAlldata.Rows)
                        {
                            DateTime grndate;
                            DateTime bldate;

                            if (!string.IsNullOrEmpty(item["irsm_orig_grn_dt"].ToString()))
                            {
                                grndate = Convert.ToDateTime(item["irsm_orig_grn_dt"].ToString());
                                if (grndate != new DateTime(9999, 1, 1))
                                {
                                    lblgrndate.Text = grndate.ToString("dd/MMM/yyyy");

                                }
                            }
                            else
                            {
                                lblgrndate.Text = string.Empty;
                            }

                            if (!string.IsNullOrEmpty(item["irsm_bl_dt"].ToString()))
                            {
                                bldate = Convert.ToDateTime(item["irsm_bl_dt"].ToString());
                                lblbldate.Text = bldate.ToString("dd/MMM/yyyy");
                            }
                            else
                            {
                                lblbldate.Text = string.Empty;
                            }

                            lblgrnno.Text = item["irsm_orig_grn_no"].ToString();
                            lblsupp.Text = item["irsm_orig_supp"].ToString();
                            lblsysbl.Text = item["irsm_sys_blno"].ToString();
                            lblbl.Text = item["irsm_blno"].ToString();
                            lbllcno.Text = item["irsm_sys_fin_no"].ToString();
                        }
                        //replace link button


                        //Report Print
                        string url = "";
                        string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                        if (string.IsNullOrEmpty(txtSerialNo.Text))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Select Assessment No')", true);
                            return;
                        }

                        else
                        {
                            InvReportPara _invRepPara = new InvReportPara();

                            _invRepPara._GlbCompany = Session["UserCompanyCode"].ToString();
                            _invRepPara._GlbDocNo = txtSerialNo.Text;
                            _invRepPara._GlbUserID = Session["UserID"].ToString();

                            DataTable rep_det = CHNLSVC.Inventory.GetReportParam(_invRepPara._GlbCompany, _invRepPara._GlbUserID);
                            string powered_by = "";
                            foreach (DataRow row in rep_det.Rows)
                            {
                                powered_by = row["MC_IT_POWERED"].ToString();

                            }
                            //Dulaj 2018-Sep-06        
                            string no = "";
                            if(ViewState["Si"]!=null)
                            {
                                no = ViewState["Si"].ToString();
                            }
                            DataTable dtCusHdr = CHNLSVC.Inventory.LoadCusdecDatabyDoc(no);
                            //if (dtCusHdr.Rows.Count > 0)
                            //{
                            //    lblcusdecEntryDate.Text = dtCusHdr.Rows[0]["Cusdec Entry Date"].ToString();
                            //    lblEntryNo.Text = dtCusHdr.Rows[0]["Entry"].ToString();
                            //    lblMvEntry.Text = dtCusHdr.Rows[0]["MV Entry"].ToString();
                            //    lblFinancingRef.Text = dtCusHdr.Rows[0]["Financing Ref"].ToString();
                            //}
                            //
                            clsInventory inv = new clsInventory(); 
                            Serial_History _serialHistory = new Serial_History();
                            //Set Report data Tables---------------------------------------------

                            DataTable OtherDetails = new DataTable();
                            DataRow dr;

                            //MasterCompany _newCom = new MasterCompany();
                            //_newCom = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                            //Session["GlbReportCompName"] = _newCom/

                            // GLOB_DataTable = new DataTable();
                            DataTable shippingDetails = new DataTable();
                            DataTable costDetails = new DataTable();

                            OtherDetails.Columns.Add("warranty_no", typeof(string));
                            OtherDetails.Columns.Add("warranty_p_s", typeof(string));
                            OtherDetails.Columns.Add("user", typeof(string));
                            OtherDetails.Columns.Add("com", typeof(string));
                            OtherDetails.Columns.Add("currency", typeof(string));
                            OtherDetails.Columns.Add("MC_IT_POWERED", typeof(string));
                            OtherDetails.Columns.Add("RECIEVED", typeof(string));

                            dr = OtherDetails.NewRow();

                            dr["warranty_no"] = warranty_no;
                            dr["warranty_p_s"] = warranty_p_s;
                            dr["user"] = Session["UserID"].ToString();
                            dr["com"] = _invRepPara._GlbCompany;
                            dr["currency"] = _currCode;
                            dr["MC_IT_POWERED"] = powered_by;

                            if (_currentLocation == null)
                            {
                                dr["RECIEVED"] = string.Empty;
                               
                            }
                            else if (_currentLocation.Rows.Count > 0)
                            {
                                dr["RECIEVED"] = Convert.ToDateTime(_currentLocation.Rows[0].Field<DateTime>("INS_DOC_DT")).Date.ToShortDateString();

                            }
                           
                            OtherDetails.Rows.Add(dr);

                            //GLOB_DataTable.Clear();


                            _serialHistory.Database.Tables["ITEM_DETAILS"].SetDataSource(_InitialStageSearch);
                            _serialHistory.Database.Tables["SERIAL_DETAILS"].SetDataSource(_currentLocation);
                            // _serialHistory.Database.Tables["SERIAL_DETAILS"].SetDataSource(tblWarrentyDetails);
                            _serialHistory.Database.Tables["SERIAL_MOVEMENT_DTL"].SetDataSource(_movement);
                            _serialHistory.Database.Tables["OTHER_DETAIL"].SetDataSource(OtherDetails);
                            //Dulaj 2018/Sep/06
                            _serialHistory.Database.Tables["CusDecDetails"].SetDataSource(dtCusHdr);


                            PrintPDF(targetFileName, _serialHistory);
                            url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                            CHNLSVC.MsgPortal.SaveReportErrorLog("CusdecAsses", "CusdecAsses", "Run Ok", Session["UserID"].ToString());

                        }


                    }
                    else
                    {
                        displayMessage("Select Valid Serial Type");
                    }


                }
            }



        }

        public void PrintPDF(string targetFileName, ReportDocument _rpt)
        {
            try
            {
                //ReportDocument Rel = new ReportDocument();
                ReportDocument rptDoc = (ReportDocument)_rpt;
                DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                //if (rbpdf.Checked)
                //{
                rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                //}
                //if (rbexel.Checked)
                //{
                //    rptDoc.ExportOptions.ExportFormatType = ExportFormatType.Excel;
                //}
                //if (rbexeldata.Checked)
                //{
                //    rptDoc.ExportOptions.ExportFormatType = ExportFormatType.Excel;
                //}
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

        private void LoadGrnData(string _serNo)
        {
            lblSupInvno.Text = "";
            lblSupInvDt.Text = "";
            Int32 _serId = 0;
            List<InventorySerialN> _invSerList = CHNLSVC.Inventory.Get_INR_SER_DATA(new InventorySerialN() { Ins_ser_1 = _serNo, Ins_available = -2 });
            if (_invSerList.Count > 0)
            {
                _serId = _invSerList[0].Ins_ser_id;
            }
            DataTable dtserialAlldata = CHNLSVC.Inventory.LoadSerialEnquiryData(_serId);
          
            foreach (DataRow item in dtserialAlldata.Rows)
            {
                DateTime grndate;
                DateTime bldate;

                if (!string.IsNullOrEmpty(item["irsm_orig_grn_dt"].ToString()))
                {
                    grndate = Convert.ToDateTime(item["irsm_orig_grn_dt"].ToString());
                    if (grndate != new DateTime(9999, 1, 1))
                    {
                        lblgrndate.Text = grndate.ToString("dd/MMM/yyyy");

                    }
                }
                else
                {
                    lblgrndate.Text = string.Empty;
                }

                if (!string.IsNullOrEmpty(item["irsm_bl_dt"].ToString()))
                {
                    bldate = Convert.ToDateTime(item["irsm_bl_dt"].ToString());
                    lblbldate.Text = bldate.ToString("dd/MMM/yyyy");
                }
                else
                {
                    lblbldate.Text = string.Empty;
                }

                lblgrnno.Text = item["irsm_orig_grn_no"].ToString();
                lblsupp.Text = item["irsm_orig_supp"].ToString();
                lblsysbl.Text = item["irsm_sys_blno"].ToString();
                lblbl.Text = item["irsm_blno"].ToString();
                lbllcno.Text = item["irsm_sys_fin_no"].ToString();
                if (!String.IsNullOrEmpty(lblsysbl.Text))
                {
                    //Dulaj 2018-Sep-04
                    DataTable dtCusHdr = CHNLSVC.Inventory.LoadCusdecDatabyDoc(lblsysbl.Text);
                    ViewState["Si"] = lblsysbl.Text;
                    lblcusdecEntryDate.Text = "";
                    if(dtCusHdr.Rows.Count>0)
                    {
                        if (!(string.IsNullOrEmpty(dtCusHdr.Rows[0]["Cusdec Entry Date"].ToString())))
                        {
                            DateTime cusdt = Convert.ToDateTime(dtCusHdr.Rows[0]["Cusdec Entry Date"].ToString());
                            lblcusdecEntryDate.Text = cusdt.Date.ToShortDateString();
                        }
                        
                        lblEntryNo.Text = dtCusHdr.Rows[0]["Entry"].ToString();
                        lblMvEntry.Text = dtCusHdr.Rows[0]["MV Entry"].ToString();
                        lblFinancingRef.Text = dtCusHdr.Rows[0]["Financing Ref"].ToString();
                    }
                    //
                }
                if (!String.IsNullOrEmpty(lblgrnno.Text))
                {
                    InventoryHeader _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(lblgrnno.Text.Trim());
                    if (_invHdr != null)
                    {
                        lblSupInvno.Text = _invHdr.ITH_SUP_INV_NO;
                        lblSupInvDt.Text = _invHdr.ITH_SUP_INV_DT.ToString("dd/MMM/yyyy");
                        // oHeader = CHNLSVC.Financial.GET_BL_HEADER_BY_DOC(Session["UserCompanyCode"].ToString(), txtDocNumber.Text.Trim(), "ALL");
                        ImportsBLHeader _blHdr = CHNLSVC.Financial.GET_BL_HEADER_BY_DOC(_invHdr.Ith_com, _invHdr.Ith_oth_docno, "ALL");
                        if (_blHdr != null)
                        {
                            lblsysbl.Text = _blHdr.Ib_doc_no;
                            lblbl.Text = _blHdr.Ib_bl_no;
                            lblbldate.Text = _blHdr.Ib_bl_dt.ToString("dd/MMM/yyyy");
                        }
                    }
                }
            }
        }
    }
}
