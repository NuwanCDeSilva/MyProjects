using FF.BusinessObjects.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.MasterFiles.Operational
{
    public partial class PrefixMaster : BasePage
    {
        bool _serPopShow
        {
            get { if (Session["_serPopShow"] != null) { return (bool)Session["_serPopShow"]; } else { return false; } }
            set { Session["_serPopShow"] = value; }
        }

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

         bool _ava
         {
             get { if (Session["_ava"] != null) { return (bool)Session["_ava"]; } else { return false; } }
             set { Session["_ava"] = value; }
         }

         string _toolTip
         {
             get { if (Session["_toolTip"] != null) { return (string)Session["_toolTip"]; } else { return ""; } }
             set { Session["_toolTip"] = value; }
         }

        sar_tp _Prefixob = new sar_tp();
        gnr_acc_sun_ledger _sunledgerlist1ob = new gnr_acc_sun_ledger();
        sar_doc_price_defn _Prefixob1 = new sar_doc_price_defn();

        string _para = "";
        List<sar_tp> _PrefixDetails
        {
            get { if (Session["_PrefixDetails"] != null) { return (List<sar_tp>)Session["_PrefixDetails"]; } else { return new List<sar_tp>(); } }
            set { Session["_PrefixDetails"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || Session["UserCompanyCode"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                ClearPageAll();
                ClearSunPageAll();
                ClearPageAllPD();
         
            }
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                decimal tmpDec = 0;
                #region Validate Hdr
                if (string.IsNullOrEmpty(txtsrtp.Text))
                {
                    txtsrtp.Focus(); DispMsg("Please select a SRTP Code !"); return;
                }
                if (string.IsNullOrEmpty(txtdes.Text))
                {
                    txtdes.Focus(); DispMsg("Please select SRTP Description !"); return;
                }
                if (DropDownList2.SelectedIndex < 1)
                {
                    DropDownList2.Focus(); DispMsg("Please select SRTP Category !"); return;
                }
                
                if (DropDownList3.SelectedIndex < 1)
                {
                    DropDownList3.Focus(); DispMsg("Please select SRTP Main TP !"); return;
                }
                #endregion

                List<sar_tp> _PrefixList = Session["_PrefixDetails"] as List<sar_tp>;
                if (_PrefixList == null)
                {
                    _PrefixList = new List<sar_tp>();
                }
                _Prefixob = new sar_tp();
                _Prefixob.srtp_cd = txtsrtp.Text;
                _Prefixob.srtp_desc = txtdes.Text;
                _Prefixob.srtp_cate = DropDownList2.SelectedValue.ToString();
                _Prefixob.srtp_is_main = chkActTarg.Checked ? 1 : 0;
                 _Prefixob.srtp_main_tp = DropDownList3.SelectedValue.ToString();

                _PrefixList.Add(_Prefixob);
                Session["_PrefixDetails"] = _PrefixList;
                //dgvTarget.DataSource = _PrefixList;
                //dgvTarget.DataBind();
                ClearPage();

                int _res = 0;

                _res = CHNLSVC.Sales.SavePrefixMasterDetails(_PrefixList);
                if (_PrefixList.Count < 1)
                {
                    DispMsg("Please add details", "N"); return;
                }
                if (_res == 1)
                {
                    DispMsg("Successfully Saved!", "S");
                    ClearPageAll();
                }


            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }

        }
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

        private void ClearPageAll()
        {
            try
            {
                Session["_PrefixDetails"] = null;


                txtsrtp.Text = ""; txtdes.Text = ""; DropDownList2.SelectedValue = "0";  DropDownList3.SelectedValue = "0";
                txtsrtp.ToolTip = ""; txtdes.ToolTip = ""; DropDownList2.ToolTip = ""; DropDownList3.ToolTip = ""; 

             
              
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        private void ClearPage()
        {
            try
            {
                txtsrtp.Text = ""; txtdes.Text = ""; DropDownList2.SelectedValue = "0"; DropDownList3.SelectedValue = "0";
                txtsrtp.ToolTip = ""; txtdes.ToolTip = ""; DropDownList2.ToolTip = ""; DropDownList3.ToolTip = "";
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void txtpc_TextChanged(object sender, EventArgs e)
        {

            try
            {
                txtpc.ToolTip = "";
                if (!string.IsNullOrEmpty(txtpc.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                    _serData = CHNLSVC.CommonSearch.GetPC_SearchData(_para, "CODE", txtpc.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtpc.Text.ToUpper().Trim(), "CODE", "Description");
                    if (_ava)
                    {
                        txtpc.ToolTip = _toolTip;
                    }
                    else
                    {
                        txtpc.Text = string.Empty;
                        txtpc.Focus();
                        DispMsg("Please enter valid Profit Center !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSepc_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                _serData = CHNLSVC.CommonSearch.GetPC_SearchData(_para, null, null);
                LoadSearchPopup("AllProfitCenters", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

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
               case CommonUIDefiniton.SearchUserControlType.Prefix:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
               case CommonUIDefiniton.SearchUserControlType.srtp:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                 case CommonUIDefiniton.SearchUserControlType.Prefix1:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                 case CommonUIDefiniton.SearchUserControlType.ProfitCenters:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
              default:
                    break;
            }
            return paramsText.ToString();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            _serPopShow = false;
            PopupSearch.Hide();
        }

       

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
             try
            {
                 if (_serType == "")
                {
          
                }
                 else if (_serType == "AllProfitCenters")
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                    _serData = CHNLSVC.CommonSearch.GetPC_SearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                }
                 else if (_serType == "Prefix")
               {
                     _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Prefix);
                     _serData = CHNLSVC.CommonSearch.GetInv_TypCre_SearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
               }
                 else if (_serType == "srtp")
                 {
                     _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.srtp);
                     _serData = CHNLSVC.CommonSearch.GetInv_TypCre_SearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                 }
                 //else if (_serType == "Prefix1")
                 //{
                 //    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.srtp);
                 //    _serData = CHNLSVC.CommonSearch.GetInv_TypCre_SearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                 //}
                 else if (_serType == "ProfitCenters")
                 {
                     _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProfitCenters);
                     _serData = CHNLSVC.CommonSearch.GetPC_SearchData(_para, ddlSerByKey.SelectedValue, txtSerByKey.Text.Trim());
                 }
             dgvResult.DataSource = new int[] { };
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                }
                //txtSerByKey.Text = "";
                txtSerByKey.Focus();
                dgvResult.DataBind();
                PopupSearch.Show();
                _serPopShow = true;
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
             
             
     }

        protected void dgvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvResult.PageIndex = e.NewPageIndex;
                if (_serData.Rows.Count > 0)
                {
                    dgvResult.DataSource = _serData;
                }
                else
                {
                    dgvResult.DataSource = new int[] { };
                }
                dgvResult.DataBind();
                txtSerByKey.Text = string.Empty;
                txtSerByKey.Focus();
                PopupSearch.Show();
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        protected void dgvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string code = dgvResult.SelectedRow.Cells[1].Text;
                if (_serType == "AllProfitCenters")
                {
                    txtpc.Text = code;
                    txtpc_TextChanged(null, null);
                }
                else if (_serType == "Prefix")
                {
                    txtpfx.Text = code;
                    txtpfx_TextChanged(null, null);
                }
                else if (_serType == "srtp")
                {
                    txtsrtp.Text = code;
                    txtsrtp_TextChanged(null, null);
                }
                else if (_serType == "Prefix1")
                {
                    textprfx1.Text = code;
                    textprfx1_TextChanged(null, null);
                }
                else if (_serType == "ProfitCenters")
                {
                    textpc1.Text = code;
                    textpc1_TextChanged(null, null);
                }
            }

            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

      
     

        private void DataAvailable(DataTable _dt, string _valText, string _valCol, string _valToolTipCol = "")
        {
            _ava = false;
            _toolTip = string.Empty;
            foreach (DataRow row in _dt.Rows)
            {
                if (!string.IsNullOrEmpty(row[_valCol].ToString()))
                {
                    if (_valText == row[_valCol].ToString())
                    {
                        _ava = true;
                        if (!string.IsNullOrEmpty(_valToolTipCol))
                        {
                            _toolTip = row[_valToolTipCol].ToString();
                        }
                        break;
                    }
                }
            }
        }

        private void LoadSearchPopup(string serType, string _colName, string _ordTp, bool _isOrder = true)
        {
            if (_isOrder)
            {
                OrderSearchGridData(_colName, _ordTp);
            }
            try
            {
                dgvResult.DataSource = new int[] { };
                dgvResult.DataBind();
                if (_serData != null)
                {
                    if (_serData.Rows.Count > 0)
                    {
                        dgvResult.DataSource = _serData;
                        dgvResult.DataBind();
                        BindDdlSerByKey(_serData);
                    }
                }
                txtSerByKey.Text = "";
                txtSerByKey.Focus();
                _serType = serType;
                PopupSearch.Show();
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        private void OrderSearchGridData(string _colName, string _ordTp)
        {
            try
            {
                if (_serData.Rows.Count > 0)
                {
                    DataView dv = _serData.DefaultView;
                    string dvSortStr = _colName + " " + _ordTp;
                    dv.Sort = dvSortStr;
                    _serData = dv.ToTable();
                }
            }
            catch (Exception)
            {
                DispMsg("Error Occurred while processing !!!", "E");
            }
        }

        public void BindDdlSerByKey(DataTable _dataSource)
        {
            if (_dataSource.Columns.Contains("From Date"))
            {
                _dataSource.Columns.Remove("From Date");
            }
            if (_dataSource.Columns.Contains("To Date"))
            {
                _dataSource.Columns.Remove("To Date");
            }
            this.ddlSerByKey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSerByKey.Items.Add(col.ColumnName);
            }

            this.ddlSerByKey.SelectedIndex = 0;
        }

        protected void txtpfx_TextChanged(object sender, EventArgs e)
        {
            try
            {   
                txtpfx.ToolTip = "";
                if (!string.IsNullOrEmpty(txtpfx.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Prefix);
                    _serData = CHNLSVC.CommonSearch.GetInv_TypCre_SearchData(_para, "CODE", txtpfx.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtpfx.Text.ToUpper().Trim(), "CODE", "Description");
                    if (_ava)
                    {
                        txtpfx.ToolTip = _toolTip;
                    }
                    else
                    {
                        txtpfx.Text = string.Empty;
                        txtpfx.Focus();
                        DispMsg("Please enter valid Prefix !");
                        return;
                    }
                   // GetSunPrefixDetails("textchangeprefixcode");
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnSrch_prfx_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSerByKey.ClientID + "').value = '';", true);
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Prefix);
                _serData = CHNLSVC.CommonSearch.GetInv_TypCre_SearchData(_para, null, null);
                LoadSearchPopup("Prefix", "CODE", "ASC");
            

            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnTarDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string _error = "";
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                Label lblsls_typ = dr.FindControl("lblsls_typ") as Label;
                Label lblprf_cr = dr.FindControl("lblprf_cr") as Label;
                Label lblsub_typ = dr.FindControl("lblsub_typ") as Label;
                Label lblacc_typ = dr.FindControl("lblacc_typ") as Label;
                Label lblacc_cd = dr.FindControl("lblacc_cd") as Label;
                Label lblledg_dec = dr.FindControl("lblledg_dec") as Label;


                List<gnr_acc_sun_ledger> _sunledgerlist1 = Session["_LedgerList"] as List<gnr_acc_sun_ledger>;
                var del = _sunledgerlist1.Where(c => c.ledg_sales_tp == lblsls_typ.Text && c.ledg_pc == lblprf_cr.Text && c.ledg_sub_tp == lblsub_typ.Text && c.ledg_acc_tp == lblacc_typ.Text && c.ledg_acc_cd == lblacc_cd.Text && c.ledg_desc == lblledg_dec.Text).FirstOrDefault();
                if (del != null)
                {
                    _sunledgerlist1.Remove(del);
                }

                Session["_LedgerList"] = _sunledgerlist1;
                dgvTarget.DataSource = _sunledgerlist1;

                dgvTarget.DataBind();






            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnAddTarItem_Click(object sender, EventArgs e)
        {
            try
            {
                
                decimal tmpDec = 0;
                #region Validate Hdr
                if (string.IsNullOrEmpty(txtpc.Text))
                {
                    txtpc.Focus(); DispMsg("Please select a Profit center !"); return;
                }
                if (string.IsNullOrEmpty(txtpfx.Text))
                {
                    txtpfx.Focus(); DispMsg("Please select Prefix !"); return;
                }
                if (DropDownListACC.SelectedIndex < 1)
                {
                    DropDownListACC.Focus(); DispMsg("Please select Type !"); return;
                }
                if (DropDownListSUB.SelectedIndex < 1)
                {
                    DropDownListSUB.Focus(); DispMsg("Please select Account Type !"); return;
                }
                //if (string.IsNullOrEmpty(Accnt.Text))
                //{
                //    Accnt.Focus(); DispMsg("Please select define Account code !"); return;
                //}
                if (DropDownList1.SelectedIndex < 1)
                {
                    DropDownList1.Focus(); DispMsg("Please select Pay Type !"); return;
                }
                #endregion

                List<gnr_acc_sun_ledger> _sunledgerlist1 = Session["_LedgerList"] as List<gnr_acc_sun_ledger>;
                if (_sunledgerlist1 == null)
                {
                    _sunledgerlist1 = new List<gnr_acc_sun_ledger>();
                }
                _sunledgerlist1ob = new gnr_acc_sun_ledger();
                _sunledgerlist1ob.ledg_comp = Session["UserCompanyCode"].ToString();
                _sunledgerlist1ob.ledg_pc = txtpc.Text;
                _sunledgerlist1ob.ledg_sales_tp = txtpfx.Text;
                _sunledgerlist1ob.ledg_sub_tp = DropDownListACC.SelectedValue.ToString();
                _sunledgerlist1ob.ledg_acc_tp = DropDownListSUB.SelectedValue.ToString();
                _sunledgerlist1ob.ledg_acc_cd = Accnt.Text.ToUpper();
                _sunledgerlist1ob.ledg_desc = DropDownList1.SelectedValue.ToString();
               

                _sunledgerlist1.Add(_sunledgerlist1ob);
                Session["_LedgerList"] = _sunledgerlist1;
                dgvTarget.DataSource = _sunledgerlist1;
 
                dgvTarget.DataBind();
                ClearSunPage();
               

            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            try
            {

                int _res = 0;
                List<gnr_acc_sun_ledger> _sunledgerlist1 = Session["_LedgerList"] as List<gnr_acc_sun_ledger>;
                _res = CHNLSVC.Sales.SaveSunLedgerDetails(_sunledgerlist1);
                if (_sunledgerlist1 == null)
                {
                    DispMsg("Please add target details", "E"); return;
                }
                if (_res == 1)
                {
                    DispMsg("Successfully Saved!", "S");
                    ClearSunPageAll();
                }

            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        private void ClearSunPageAll()
        {
            try
            {
                //ucLoactionSearch.PnlHeading.Visible = false;
                _serData = new DataTable();
                _serType = "";
                _ava = false;
                _toolTip = string.Empty;
                _serPopShow = false;
                txtpc.Text = Session["UserCompanyCode"].ToString();
                txtpc_TextChanged(null, null);
                dgvTarget.DataSource = null;
                dgvTarget.DataBind();
                _serData = new DataTable();
                _serType = "";
                _serPopShow = false;
                _toolTip = "";
                _ava = false;
                Session["_LedgerList"] = null;
                txtpc.Text = ""; txtpfx.Text = ""; Accnt.Text = ""; DropDownListACC.SelectedValue = "0"; DropDownListSUB.SelectedValue = "0"; DropDownList1.SelectedValue = "0";
                txtpc.ToolTip = ""; txtpfx.ToolTip = ""; DropDownListACC.ToolTip = ""; DropDownListSUB.ToolTip = ""; DropDownList1.ToolTip = ""; Accnt.ToolTip = "";

                
           
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        private void ClearSunPage()
        {
            try
            {
                //ucLoactionSearch.PnlHeading.Visible = false;
                _serData = new DataTable();
                _serType = "";
                _ava = false;
                _toolTip = string.Empty;
                _serPopShow = false;
                _serData = new DataTable();
                _serType = "";
                _serPopShow = false;
                _toolTip = "";
                _ava = false;
                txtpc.Text = ""; txtpfx.Text = ""; Accnt.Text = ""; DropDownListACC.SelectedValue = "0"; DropDownListSUB.SelectedValue = "0"; DropDownList1.SelectedValue = "0";
                txtpc.ToolTip = ""; txtpfx.ToolTip = ""; DropDownListACC.ToolTip = ""; DropDownListSUB.ToolTip = ""; DropDownList1.ToolTip = ""; Accnt.ToolTip = "";

           }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }


        private void GetSunPrefixDetails(string method)
        {
            string txtbankcodeval = txtpfx.Text.ToUpper().ToString();

           if (method == "textchangeprefixcode" & dgvResult.Rows.Count > 0)
            {
                GridViewRow prfx = dgvResult.Rows[0];

                txtpfx.Text = prfx.Cells[1].Text;

                txtbankcodeval = txtpfx.Text;

            }

            gnr_acc_sun_ledger FacilityData = new gnr_acc_sun_ledger();
            FacilityData.ledg_comp = Session["UserCompanyCode"].ToString();
            FacilityData.ledg_sales_tp = txtpfx.Text;

            DataTable dtaccdata = CHNLSVC.General.LoadSunPrefixFacilityData(FacilityData);

            //DataTable dt = (DataTable)ViewState["ACCOUNT_FACILITY"];

            List<DataRow> list = dtaccdata.AsEnumerable().ToList();
            Session["_BrandManList"] = list;
            dgvTarget.DataSource = dtaccdata;
            dgvTarget.DataBind();



            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSerByKey.ClientID + "').value = '';", true);


        }

        protected void txtsrtp_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtsrtp.Text))
            {
                sar_tp mprefix = CHNLSVC.General.GetMasterPrefixData(new sar_tp() { srtp_cd = txtsrtp.Text });
                if (mprefix != null)
                {
                    LoadData(mprefix);
                }
            }

            else 
            {
                ClearPageAll();
            
            }
            try
            {
               txtsrtp.ToolTip = "";
                if (!string.IsNullOrEmpty(txtpfx.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.srtp);
                    _serData = CHNLSVC.CommonSearch.GetInv_TypCre_SearchData(_para, "CODE", txtsrtp.Text.ToUpper().Trim());
                    DataAvailable(_serData, txtsrtp.Text.ToUpper().Trim(), "CODE", "Description");
                    if (_ava)
                    {
                        txtsrtp.ToolTip = _toolTip;
                    }
                    if (!string.IsNullOrEmpty(txtsrtp.Text))
                    {
                        sar_tp mprefix = CHNLSVC.General.GetMasterPrefixData(new sar_tp() { srtp_cd = txtsrtp.Text});
                        if (mprefix != null)
                        {
                            LoadData(mprefix);
                        }
                    }
                    else
                    {
                        txtsrtp.Text = string.Empty;
                        txtsrtp.Focus();
                        DispMsg("Please enter valid SRTP Code !");
                        return;
                    }
              
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        private void LoadData(sar_tp mprefix)
        {
            try
            {
                //txtsrtp.Text = mprefix.srtp_cd;
                txtdes.Text = mprefix.srtp_desc;
                chkActTarg.Checked = mprefix.srtp_is_main == 1 ? true : false;
                DropDownList2.SelectedIndex = DropDownList2.Items.IndexOf(DropDownList2.Items.FindByValue(mprefix.srtp_cate.ToString()));
                DropDownList3.SelectedIndex = DropDownList3.Items.IndexOf(DropDownList3.Items.FindByValue(mprefix.srtp_main_tp.ToString()));
           
            }
            catch (Exception e)
            {
                DispMsg("Error Occurred while processing !!! ", "E");
            }
        }



        protected void lbtnSesrtp_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                _serData = CHNLSVC.CommonSearch.GetInv_TypCre_SearchData(_para, null, null);
                LoadSearchPopup("srtp", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnpc1_Click(object sender, EventArgs e)
        {
            try
            {
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProfitCenters);
                _serData = CHNLSVC.CommonSearch.GetPC_SearchData(_para, null, null);
                LoadSearchPopup("ProfitCenters", "CODE", "ASC");
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void textpc1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textpc1.ToolTip = "";
                if (!string.IsNullOrEmpty(textpc1.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProfitCenters);
                    _serData = CHNLSVC.CommonSearch.GetPC_SearchData(_para, "CODE", textpc1.Text.ToUpper().Trim());
                    DataAvailable(_serData, textpc1.Text.ToUpper().Trim(), "CODE", "Description");
                    if (_ava)
                    {
                        textpc1.ToolTip = _toolTip;
                    }
                    else
                    {
                        textpc1.Text = string.Empty;
                        textpc1.Focus();
                        DispMsg("Please enter valid Profit Center !");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        
        protected void textprfx1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textprfx1.ToolTip = "";
                if (!string.IsNullOrEmpty(textprfx1.Text))
                {
                    _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Prefix1);
                    _serData = CHNLSVC.CommonSearch.GetInv_Typforupdate_SearchData(_para, "CODE", textprfx1.Text.ToUpper().Trim(), textpc1.Text.ToUpper().Trim());
                    DataAvailable(_serData, textprfx1.Text.ToUpper().Trim(), "CODE", "Description");
                    if (_ava)
                    {
                        textprfx1.ToolTip = _toolTip;
                    }
                    else
                    {
                        textprfx1.Text = string.Empty;
                        textprfx1.Focus();
                        DispMsg("Please enter valid Prefix !");
                        return;
                    }
                    //GetSunPrefixDetails1("textchangeprefix1code");
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        //private void GetSunPrefixDetails1(string method)
        //{
        //    string txtbankcodeval = textprfx1.Text.ToUpper().ToString();

        //    if (method == "textchangeprefix1code" & dgvResult.Rows.Count > 0)
        //    {
        //        GridViewRow prfx = dgvResult.Rows[0];

        //        textprfx1.Text = prfx.Cells[1].Text;

        //        txtbankcodeval = txtpfx.Text;

        //    }

        //    sar_doc_price_defn FacilityData = new sar_doc_price_defn();
        //    FacilityData.sadd_com = Session["UserCompanyCode"].ToString();
        //    FacilityData.sadd_doc_tp = textprfx1.Text;
        //    FacilityData.sadd_pc = textpc1.Text;

        //    DataTable dtaccdata = CHNLSVC.General.LoadSunPrefixFacilityData1(FacilityData);

        //    //DataTable dt = (DataTable)ViewState["ACCOUNT_FACILITY"];

        //    List<DataRow> list = dtaccdata.AsEnumerable().ToList();
        //    Session["_BrandManList"] = list;
        //    dgvTarget.DataSource = dtaccdata;
        //    dgvTarget.DataBind();



        //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSerByKey.ClientID + "').value = '';", true);


        //}
        protected void lbtnprfx1_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSerByKey.ClientID + "').value = '';", true);
                _serData = new DataTable();
                _para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Prefix1);
                _serData = CHNLSVC.CommonSearch.GetInv_Typforupdate_SearchData(_para, null, null, textpc1.Text);
                LoadSearchPopup("Prefix1", "CODE", "ASC");


            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            try
            {

                decimal tmpDec = 0;
                #region Validate Hdr
                if (string.IsNullOrEmpty(textpc1.Text))
                {
                    txtsrtp.Focus(); DispMsg("Please select a Profit Center !"); return;
                }
                if (string.IsNullOrEmpty(textprfx1.Text))
                {
                    txtdes.Focus(); DispMsg("Please select Prefix !"); return;
                }
                //if (string.IsNullOrEmpty(textprfxno.Text))
                //{
                //    txtstatus.Focus(); DispMsg("Please Add Prefix No !"); return;
                //}
                
                #endregion

                List<sar_doc_price_defn> _PrefixList1 = Session["_PrefixDetails"] as List<sar_doc_price_defn>;
                if (_PrefixList1 == null)
                {
                    _PrefixList1 = new List<sar_doc_price_defn>();
                }
                _Prefixob1 = new sar_doc_price_defn();
                _Prefixob1.sadd_com = Session["UserCompanyCode"].ToString();
                _Prefixob1.sadd_prefix = textprfxno.Text;
                _Prefixob1.sadd_pc = textpc1.Text;
                _Prefixob1.sadd_doc_tp = textprfx1.Text;
                _Prefixob1.sadd_mod_by = Session["UserID"].ToString(); 
                _Prefixob1.sadd_mod_when = DateTime.Now;
              
                _PrefixList1.Add(_Prefixob1);
                Session["_PrefixDetails"] = _PrefixList1;
                ClearPagePD();
                DataTable _chk = new DataTable();
                _chk = CHNLSVC.Sales.CheckPrefixMasterDetails(_Prefixob1);
                if (_chk != null)
                {
                    //if (_chk.Rows.Count > 0)
                    // {
                    //DispMsg("Already Exists ", "N"); return;
                    //}
                    //else
                    // {
                    //update

                    if (_chk.Rows.Count > 0)
                    {
                        int _res = 0;
                        _res = CHNLSVC.Sales.UpdatePrefixMasterDetails(_PrefixList1);
                        if (_PrefixList1.Count < 1)
                        {
                            DispMsg("Please add details", "N"); return;
                        }
                        if (_res == 1)
                        {
                            DispMsg("Successfully Updated!", "S");
                            ClearPageAllPD();
                        }
                    }
                    else
                    {
                        DispMsg("Please Enter Valid Details!");
                    }
                }
                //}
                //else
                //{
                //    //update
                //    int _res = 0;
                //    _res = CHNLSVC.Sales.UpdatePrefixMasterDetails(_PrefixList1);
                //    if (_PrefixList1.Count < 1)
                //    {
                //        DispMsg("Please add details", "N"); return;
                //    }
                //    if (_res == 1)
                //    {
                //        DispMsg("Successfully Saved!", "S");
                //        ClearPageAllPD();
                //    }
                //}
                else 
                {
                    DispMsg("Please Enter Valid Prefix No!");
                }

                ClearPageAllPD();
               


            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        private void ClearPagePD()
        {
            try
            {
                textpc1.Text = ""; textprfx1.Text = ""; textprfxno.Text = "";
                textpc1.ToolTip = ""; textprfx1.ToolTip = ""; textprfxno.ToolTip = "";
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }
        protected void cleardefine_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        private void ClearPageAllPD()
        {
            try
            { 
                //sar_doc_price_defn _Prefixob1 = new sar_doc_price_defn();

                Session["_PrefixDetails"] = null;
                textpc1.Text = ""; textprfx1.Text = ""; textprfxno.Text = "";
                textpc1.ToolTip = ""; textprfx1.ToolTip = ""; textprfxno.ToolTip = "";

            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

     
    }
}