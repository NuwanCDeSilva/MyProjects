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

namespace FastForward.SCMWeb.View.Enquiries.Imports
{
    public partial class Cost_and_Profitability_Assistant_View : Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageClear();
            }
        }

        #region Modalpopup
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            string ID = grdResult.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "Pc_HIRC_Company")
            {
                txtcompany.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "masterItem")
            {
                string model = grdResult.SelectedRow.Cells[3].Text;
                txtItem.Text = ID;
                txtModel.Text = model;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "ModelMaster")
            {
                txtModel.Text = ID;
                lblvalue.Text = "";
                return;
            }
            if (lblvalue.Text == "BLHeader")
            {
                txtbl.Text = ID;
                lblvalue.Text = "";
                return;
            }
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResult.PageIndex = e.NewPageIndex;
            if ((lblvalue.Text == "Pc_HIRC_Company"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "masterItem")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams,null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "ModelMaster")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
                DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "BLHeader")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                DataTable result = CHNLSVC.CommonSearch.SearchBLHeader(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopoup.Show();
            }
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            if ((lblvalue.Text == "Pc_HIRC_Company"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "masterItem")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams,ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "ModelMaster")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
                DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
            if (lblvalue.Text == "BLHeader")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                DataTable result = CHNLSVC.CommonSearch.SearchBLHeader(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopoup.Show();
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            if ((lblvalue.Text == "Pc_HIRC_Company"))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
             if (lblvalue.Text == "masterItem")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams,ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = _result;
                grdResult.DataBind();
                UserPopoup.Show();
                return;
            }
             if (lblvalue.Text == "ModelMaster")
             {
                 string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
                 DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                 grdResult.DataSource = _result;
                 grdResult.DataBind();
                 UserPopoup.Show();
                 return;
             }
             if (lblvalue.Text == "BLHeader")
             {
                 string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                 DataTable result = CHNLSVC.CommonSearch.SearchBLHeader(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                 grdResult.DataSource = result;
                 grdResult.DataBind();
                 UserPopoup.Show();
             }
        }
        #endregion

        private void LoadItemStatus()
        {
            DataTable _result1 = CHNLSVC.Inventory.GetOrderStatus("");
            ddlStatus.DataSource = _result1;
            ddlStatus.DataTextField = "mis_desc";
            ddlStatus.DataValueField = "mis_cd";
            ddlStatus.DataBind();
            ddlStatus.SelectedIndex = 20;
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.ModelMaster:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterItem:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BLHeader:
                    {
                        // paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserSBU"].ToString() + seperator);
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserSBU"].ToString() + seperator + 0 + seperator);
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

        private void PageClear()
        {
            txtcompany.Text = Session["UserCompanyCode"].ToString();
            Session["Cost"] = "";
           // txtcompany.Text = string.Empty;
            ddlorderfrom.SelectedIndex = -1;
            txtItem.Text = string.Empty;
            txtItem.Text = string.Empty;
            txtModel.Text = string.Empty;
            txtLast.Text = "10";
            ddlOrderRequest.SelectedIndex = -1;
            LoadItemStatus();
            grditemCost.DataSource = new int[] { };
            grditemCost.DataBind();
            grdDFPriceBook.DataSource = new int[] { };
            grdDFPriceBook.DataBind();
            grdDpPriceBook.DataSource = new int[] { };
            grdDpPriceBook.DataBind();
            grdinventory.DataSource = new int[] { };
            grdinventory.DataBind();
        }
   
        protected void lbtncompany_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Pc_HIRC_Company";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }
        protected void lbtnItem_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "masterItem";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void lbtnmodel_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
                DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "ModelMaster";
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                string _Msg = "Error Occurred while processing..!";
                DisplayMessage(_Msg, 4);
            }
        }

        protected void txtcompany_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, "Code", txtcompany.Text);
            if (_result.Rows.Count == 0)
            {
                DisplayMessage("invalid Company", 2);
            }
        }

        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.masterItem);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, "Item", txtItem.Text);
            if (_result.Rows.Count == 0)
            {
                DisplayMessage("Invalid item ", 2);
            }
            else
            {
                txtModel.Text = _result.Rows[0][2].ToString();
            }
        }

        protected void txtModel_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
            DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(SearchParams, "Code", txtModel.Text);
            if (_result.Rows.Count == 0)
            {
                DisplayMessage("Invalid model ", 2);
            }
        }

        protected void lbtnSearchall_Click(object sender, EventArgs e)
        {
            DataTable _FilterTbl = new DataTable();
            DataRow dr = null;
            if (string.IsNullOrEmpty(txtcompany.Text))
            {
                DisplayMessage("Please enter company", 2);
                return;
            }
            //if (string.IsNullOrEmpty(txtModel.Text) && string.IsNullOrEmpty(txtItem.Text))
            //{
            //    DisplayMessage("Please enter Item or Model", 2);
            //    return;
            //}
            if (string.IsNullOrEmpty(txtLast.Text))
            {
                DisplayMessage("Please enter filter row number", 2);
                return;
            }
            DataTable[] dtArray = CHNLSVC.General.GetITEMCOST(txtcompany.Text, ddlorderfrom.SelectedValue, txtItem.Text, txtModel.Text, ddlStatus.SelectedValue, Convert.ToInt32(txtLast.Text), txtbl.Text);
            DataTable Dutypaid = dtArray[0];
            DataTable DutyFree_pal = dtArray[1];
            DataTable DutyFree = dtArray[2];
            if (Dutypaid != null)
            {
                Dutypaid.Columns.Add(new DataColumn("DutyPaid", typeof(decimal)));
                Dutypaid.Columns.Add(new DataColumn("DF_Pal", typeof(decimal)));
                Dutypaid.Columns.Add(new DataColumn("Df", typeof(decimal)));
                Dutypaid.Columns.Add(new DataColumn("charge", typeof(string)));
                foreach (DataRow row in Dutypaid.Rows)
                {
                    row["DutyPaid"] = row["ITB_UNIT_COST"];

                }
            }
            if ((DutyFree_pal!=null))
            {
                if ((DutyFree_pal.Rows.Count > 0))
                {
                    DutyFree_pal.Columns.Add(new DataColumn("DutyPaid", typeof(decimal)));
                    DutyFree_pal.Columns.Add(new DataColumn("DF_Pal", typeof(decimal)));
                    DutyFree_pal.Columns.Add(new DataColumn("Df", typeof(decimal)));
                    foreach (DataRow row2 in DutyFree_pal.Rows)
                    {
                        row2["DF_Pal"] = row2["ITB_UNIT_COST"];
                    }
                  
                    if (Dutypaid != null)
                    {
                        Dutypaid.Merge(DutyFree_pal);
                    }
                    else
                    {
                        Dutypaid = DutyFree_pal;
                    }
                }

               
            }
            if (DutyFree!=null)
            {
                if ((DutyFree.Rows.Count > 0))
                {
                    DutyFree.Columns.Add(new DataColumn("DutyPaid", typeof(decimal)));
                    DutyFree.Columns.Add(new DataColumn("DF_Pal", typeof(decimal)));
                    DutyFree.Columns.Add(new DataColumn("Df", typeof(decimal)));

                    foreach (DataRow row2 in DutyFree.Rows)
                    {
                        row2["Df"] = row2["ITB_UNIT_COST"];
                    }
                    if (Dutypaid != null)
                    {
                        Dutypaid.Merge(DutyFree);
                    }
                    else
                    {
                        Dutypaid = DutyFree;
                    }
                   
                }
            
            }
           // dr = Dutypaid.NewRow();
           // dr = DutyFree_pal.NewRow();
           // dr = DutyFree.NewRow();

            if (Dutypaid.Rows.Count > 0)
            {
                foreach (DataRow _row in Dutypaid.Rows)
                {
                    string docno = _row[0].ToString();
                    string Item = _row[4].ToString();
                    ImportsBLHeader oHeader = new ImportsBLHeader();
                    oHeader = CHNLSVC.Financial.GET_BL_HEADER_BY_DOC(Session["UserCompanyCode"].ToString(), docno, "A");
                    if (oHeader != null)
                    {
                        List<ImportsBLItems> oImportsBLItems = CHNLSVC.Financial.GET_BL_ITMS_BY_SEQ(oHeader.Ib_seq_no);
                        foreach(ImportsBLItems _bl in oImportsBLItems)
                        {
                            if (Item == _bl.Ibi_itm_cd)
                            {
                                if (_bl.Ibi_tp == "C")
                                {
                                    _row["charge"] = "Charge";
                                }
                                else
                                {
                                    _row["charge"] = "FOC";
                                }
                                
                            }
                        }
                        
                      
                    }
                }
            }
          
            grditemCost.DataSource = Dutypaid;
            grditemCost.DataBind();
        }

        protected void chk_Req_CheckedChanged_Click(object sender, EventArgs e)
        {
            try
            {
                if (grditemCost.Rows.Count == 0) return;
                var checkbox = (CheckBox)sender;
                var row = (GridViewRow)checkbox.NamingContainer;
                if (row != null)
                {
                    if (checkbox.Checked == true)
                    {
                        string supplier = (row.FindControl("col_mbe_name") as Label).Text;
                        string itemDes = (row.FindControl("col_mi_longdesc") as Label).Text;
                        string Cost = (row.FindControl("col_ITB_UNIT_COST") as Label).Text;
                        string GRN_no = (row.FindControl("col_ITH_DOC_NO") as Label).Text;
                        Session["GRN_no"] = GRN_no;
                        Session["Cost"] = Cost;
                        lbsupplier.Text = supplier;
                        lbDes.Text = itemDes;
                        row.BackColor = System.Drawing.Color.LightCyan;
                    }
                    else
                    {
                        row.BackColor = System.Drawing.Color.Transparent;
                    }
                }

            }
               
            catch (Exception ex)
            {
                string _Msg = ex.ToString();
                DisplayMessage(_Msg, 4);
            }
        }

        private void GetPrice()
        {
           // List<INT_BATCHLOGICOST> LogisticCost = new List<INT_BATCHLOGICOST>(); 
            if (string.IsNullOrEmpty(txtcompany.Text))
            {
                DisplayMessage("Please enter company", 2);
                return;
            }
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                DisplayMessage("Please enter item", 2);
                return;
            }
            if (Session["Cost"].ToString() == "")
            {
                DisplayMessage("Please select  cost", 2);
                return;
            }
            DataRow dr = null;
            DataTable PriceTbl = CHNLSVC.General.GetDUTYPRICE_BYITEM(txtcompany.Text, txtItem.Text, System.DateTime.Now);
           DataTable LogisticCost = CHNLSVC.General.GETLOGISTIC_COST(txtcompany.Text, txtItem.Text, Session["GRN_no"].ToString());
            DataView dv = new DataView(PriceTbl);
            dv.RowFilter = "sapl_isdf=1";
            DataTable DFTbl = dv.ToTable();

            DataView dv2 = new DataView(PriceTbl);
            dv2.RowFilter = "sapl_isdf=0";
            DataTable DPTbl = dv2.ToTable();


            DFTbl.Columns.Add(new DataColumn("Margin1", typeof(string)));
            DFTbl.Columns.Add(new DataColumn("Prece1", typeof(string)));
            DFTbl.Columns.Add(new DataColumn("Margin2", typeof(string)));
            DFTbl.Columns.Add(new DataColumn("Prece2", typeof(string)));
            DFTbl.Columns.Add(new DataColumn("LgCost", typeof(string)));
            DFTbl.Columns.Add(new DataColumn("LgMargin", typeof(string)));

            DPTbl.Columns.Add(new DataColumn("Margin1", typeof(string)));
            DPTbl.Columns.Add(new DataColumn("Prece1", typeof(string)));
            DPTbl.Columns.Add(new DataColumn("Margin2", typeof(string)));
            DPTbl.Columns.Add(new DataColumn("Prece2", typeof(string)));
            DPTbl.Columns.Add(new DataColumn("LgCost", typeof(string)));
            DPTbl.Columns.Add(new DataColumn("LgMargin", typeof(string)));


            dr = DPTbl.NewRow();
            dr = DFTbl.NewRow();
            decimal cost =Convert.ToDecimal(Session["Cost"] .ToString());
            foreach (DataRow row in DFTbl.Rows)
            {
                decimal price = Convert.ToDecimal(row["sapd_itm_price"].ToString());
                decimal Margin = price - cost;
                decimal totalMargin = Margin;
                decimal perc = (Margin / cost) * 100;
                string newMargin = DoFormat(totalMargin);
                string newperc = DoFormat(perc);
                row["Margin1"] = newMargin;
                row["Prece1"] = newperc + "%";

                if (LogisticCost!=null)
                {

                    row["LgCost"] = LogisticCost.Rows[0][0].ToString();
                    decimal LgCost = Convert.ToDecimal(LogisticCost.Rows[0][0].ToString());
                    decimal MarginLg = price-(cost + LgCost);
                    row["LgMargin"] = MarginLg.ToString();

                }

            }

            foreach (DataRow row in DPTbl.Rows)
            {
                decimal price = Convert.ToDecimal(row["sapd_itm_price"].ToString());
                decimal Margin =  price - cost;
                decimal totalMargin = Margin;
                decimal perc = (Margin /cost)*100;
                string newMargin = DoFormat(totalMargin);
                string newperc = DoFormat(perc);
                row["Margin1"] = newMargin;
                row["Prece1"] = newperc + "%";

                if (LogisticCost != null)
                {

                    row["LgCost"] = LogisticCost.Rows[0][0].ToString();
                    decimal LgCost = Convert.ToDecimal(LogisticCost.Rows[0][0].ToString());
                    decimal MarginLg = price - (cost + LgCost);
                    row["LgMargin"] = MarginLg.ToString();

                }

            }
            grdDFPriceBook.DataSource = DFTbl;
            grdDFPriceBook.DataBind();
            grdDpPriceBook.DataSource = DPTbl;
            grdDpPriceBook.DataBind();

            ViewState["DFTbl"] = DFTbl;
            ViewState["DPTbl"] = DPTbl;
        }
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
        protected void lbtnView_Click(object sender, EventArgs e)
        {
            GetPrice();
        }

        protected void btnApply_Click(object sender, EventArgs e)
        {
            DataTable DFTbl = ViewState["DFTbl"] as DataTable;
            DataTable DPTbl = ViewState["DPTbl"] as DataTable;

            if (string.IsNullOrEmpty(txtApplyPer.Text))
            {
                DisplayMessage("Please enter percentage", 2);
                return;
            }
            if (Session["Cost"].ToString()=="")
            {
                DisplayMessage("Please select  cost", 2);
                return;
            }
            decimal cost = Convert.ToDecimal(Session["Cost"].ToString());
            if (DFTbl != null)
            {
                foreach (DataRow row in DFTbl.Rows)
                {
                    decimal price = Convert.ToDecimal(row["sapd_itm_price"].ToString());
                    decimal App_per = Convert.ToDecimal(txtApplyPer.Text);
                    decimal Margin = (App_per / 100) * cost;
                    decimal totalMargin = Margin + cost;
                    decimal perc = (Margin / cost)*100;
                    string newMargin = DoFormat(totalMargin);
                    string newperc = DoFormat(perc);
                    row["Margin2"] = newMargin;
                    row["Prece2"] = newperc + "%";

                }
                grdDFPriceBook.DataSource = DFTbl;
                grdDFPriceBook.DataBind();
                ViewState["DFTbl"] = DFTbl;
            }
            if (DPTbl != null)
            {
                foreach (DataRow row in DPTbl.Rows)
                {
                    decimal price = Convert.ToDecimal(row["sapd_itm_price"].ToString());
                    decimal App_per = Convert.ToDecimal(txtApplyPer.Text);
                    decimal Margin = (App_per / 100) * cost;
                    decimal totalMargin = Margin + cost;
                    decimal perc = (Margin / cost) * 100;
                    string newMargin = DoFormat(totalMargin);
                    string newperc = DoFormat(perc);
                    row["Margin2"] = newMargin;
                    row["Prece2"] = newperc + "%";

                }
                grdDpPriceBook.DataSource = DPTbl;
                grdDpPriceBook.DataBind();
                ViewState["DPTbl"] = DPTbl;
            }
            
          

           
           
        }

        protected void btnInventory_Click(object sender, EventArgs e)
        {
            string DFDP = string.Empty;
            if (rdioDF.Checked == true)
            {
                DFDP = "DFS";
            }
            else if (rdioDP.Checked == true)
            {
                DFDP = "DPS";
            }
            if (string.IsNullOrEmpty(txtcompany.Text))
            {
                DisplayMessage("Please enter company", 2);
                return;
            }
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                DisplayMessage("Please enter item", 2);
                return;
            }
            DataTable Inventorybalance = CHNLSVC.Inventory.GetItemInventoryBalancechanel(txtcompany.Text, txtItem.Text, ddlStatus.SelectedValue, DFDP);
            grdinventory.DataSource = Inventorybalance;
            grdinventory.DataBind();

        }

        protected void lbtnDocNumber_Click(object sender, EventArgs e)
        {

            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
            DataTable result = CHNLSVC.CommonSearch.SearchBLHeader(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "BLHeader";
            BindUCtrlDDLData(result);
            UserPopoup.Show();
        }
    }
}