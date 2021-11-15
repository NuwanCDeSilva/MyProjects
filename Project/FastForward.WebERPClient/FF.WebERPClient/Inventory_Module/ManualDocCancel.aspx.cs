using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using FF.BusinessObjects;
using System.Text.RegularExpressions;
using System.Globalization;

namespace FF.WebERPClient.Inventory_Module
{
    public partial class ManualDocCancel : BasePage
    {

        #region properties
        
        
        public DataTable TemDocTable {
            get { return (DataTable)ViewState["temdoctable"]; }
            set { ViewState["temdoctable"] = value; }
        } 

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            if(!IsPostBack){
                BindUserItemStatusDDLData(DropDownListDocumentType);
                //load item description on page load
                DropDownListDocumentType_SelectedIndexChanged(null, null);
                CreateTableColumns();
                GridDataBind();
            }
        }

        private void CreateTableColumns() {
            TemDocTable = new DataTable();
            TemDocTable.Columns.Add("BOOKNO", typeof(string));
            TemDocTable.Columns.Add("PREFIX", typeof(string));
            TemDocTable.Columns.Add("START", typeof(int));
            TemDocTable.Columns.Add("END", typeof(int));
            TemDocTable.Columns.Add("DES", typeof(bool));
            TemDocTable.Columns.Add("ITEM_CODE", typeof(string));
        }

        private string AddHtmlSpaces(int length)
        {
            string space = "";
            for (int i = 0; i < length; i++)
            {
                space = space + " &nbsp;";
            }
            return space;
        }

        private void BindUserItemStatusDDLData(DropDownList ddl)
        {
            ddl.Items.Add(new ListItem("--select--", "-1"));
            DataTable itemTable = CHNLSVC.Inventory.Get_all_Items();
            foreach (DataRow dr in itemTable.Rows)
            {
                ddl.Items.Add(new ListItem(dr["MI_CD"].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(13 - dr["MI_CD"].ToString().Length)) + "-" + dr["mi_shortdesc"].ToString(), dr["MI_CD"].ToString()));
            }

            //ddl.DataSource = itemTable;
            //ddl.DataTextField = "MI_CD";
            //ddl.DataValueField = "mi_shortdesc";
            //ddl.DataBind();
        }

        private void GridDataBind() {
            GridViewFinalDocuments.DataSource = TemDocTable;
            GridViewFinalDocuments.DataBind();
        }

        protected void DropDownListDocumentType_SelectedIndexChanged(object sender, EventArgs e)
        {
           // if (DropDownListDocumentType.SelectedValue != "-1")
               // LabelDocumentDescription.Text = DropDownListDocumentType.SelectedItem.Value;
           // else
               // LabelDocumentDescription.Text = string.Empty;

            GridViewDocuments.DataSource = CHNLSVC.Inventory.Get_all_manual_docs_by_type(GlbUserDefLoca, DropDownListDocumentType.SelectedItem.Value);
            GridViewDocuments.DataBind();
            DivRecieptRange.Visible = false;
        }

        protected void GridViewDocuments_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int range = Convert.ToInt32(GridViewDocuments.Rows[e.NewSelectedIndex].Cells[5].Text);
            int startValue = Convert.ToInt32(GridViewDocuments.Rows[e.NewSelectedIndex].Cells[3].Text);
            int endValue = Convert.ToInt32(GridViewDocuments.Rows[e.NewSelectedIndex].Cells[4].Text);
            if (range > 0)
            {
                DivRecieptRange.Visible = true;
                TextBoxRecieptStart.Text = startValue.ToString();
                TextBoxRecieptEnd.Text = string.Empty;
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string bookNo = GridViewDocuments.Rows[GridViewDocuments.SelectedRow.RowIndex].Cells[0].Text;
                string prefix = GridViewDocuments.Rows[GridViewDocuments.SelectedRow.RowIndex].Cells[1].Text;
                int startValue = Convert.ToInt32(GridViewDocuments.Rows[GridViewDocuments.SelectedRow.RowIndex].Cells[3].Text);
                int endRange = Convert.ToInt32(GridViewDocuments.Rows[GridViewDocuments.SelectedRow.RowIndex].Cells[4].Text);
                int endValue = Convert.ToInt32(TextBoxRecieptEnd.Text);
                
                if (endValue >= startValue && endValue < endRange)
                {
                    DataRow[] dr = TemDocTable.Select(string.Format("{0} = {1}", "BOOKNO", bookNo)); 
                    if (dr.Length<=0)
                    {
                        DataRow dataRow = TemDocTable.NewRow();
                        dataRow[0] = bookNo;
                        dataRow[1] = prefix;
                        dataRow[2] = startValue;
                        dataRow[3] = endValue;
                        dataRow[4] = false;
                        dataRow[5] = DropDownListDocumentType.SelectedItem.Value;
                        TemDocTable.Rows.Add(dataRow);
                        GridDataBind();
                        ButtonSave.Attributes.Add("OnClick", "showMessage()");
                    }
                    else
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Item already exists");
                }
                //user enter last value
                else if (endValue >= startValue && endValue == endRange)
                {
                    DataRow[] dr = TemDocTable.Select(string.Format("{0} ={1}", "BOOKNO", bookNo));
                    if (dr.Length <= 0)
                    {
                        DataRow dataRow = TemDocTable.NewRow();
                        dataRow[0] = bookNo;
                        dataRow[1] = prefix;
                        dataRow[2] = startValue;
                        dataRow[3] = endValue;
                        dataRow[4] = true;
                        dataRow[5] = DropDownListDocumentType.SelectedItem.Value;
                        TemDocTable.Rows.Add(dataRow);
                        GridDataBind();
                        ButtonSave.Attributes.Add("OnClick", "showMessage()");
                    }
                    else
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Item already exists");
                }
                else
                { MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "End number not in range");
                
                }
                TextBoxRecieptEnd.Text = string.Empty;
                DivRecieptRange.Visible = false;
            }
            catch (Exception) {
                if(TextBoxRecieptEnd.Text!=string.Empty)
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter end number as integer");
                
                return;
            }
        }

        protected void GridViewFinalDocuments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            TemDocTable.Rows.RemoveAt(e.RowIndex);
            GridDataBind();
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {

                    if (TemDocTable.Rows.Count > 0)
                    {

                        bool des = Convert.ToBoolean(HiddenFieldSaveButon.Value);
                        if (des)
                        {
                        for (int i = 0; i < TemDocTable.Rows.Count; i++)
                        {
                            try
                            {
                                if (Convert.ToBoolean(TemDocTable.Rows[i][4]))
                                {
                                    if (TemDocTable.Rows[i][5].ToString() == "HPRS" || TemDocTable.Rows[i][5].ToString() == "HPRM") { 
                                    
                                        int start = Convert.ToInt32(TemDocTable.Rows[i][2]);
                                        int end = Convert.ToInt32(TemDocTable.Rows[i][3]);
                                        //save reciept and reciept item for every cancel page no
                                        for (; end >= start; start++) {

                                       
                                            MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                                            _receiptAuto.Aut_cate_cd = GlbUserDefProf;
                                            _receiptAuto.Aut_cate_tp = "PC";
                                            _receiptAuto.Aut_start_char = TemDocTable.Rows[i][5].ToString();
                                            _receiptAuto.Aut_direction = 1;
                                            _receiptAuto.Aut_modify_dt = null;
                                            _receiptAuto.Aut_moduleid = "HP";
                                            _receiptAuto.Aut_number = 0;
                                            _receiptAuto.Aut_year = null;
                                            string _cusNo = CHNLSVC.Sales.GetRecieptNo(_receiptAuto);

                                            //insert reciept
                                        int seqNo = CHNLSVC.Inventory.GetSerialID();
                                            RecieptHeader recieptHeadder = new RecieptHeader();
                                            recieptHeadder.Sar_seq_no = seqNo;
                                            recieptHeadder.Sar_prefix = TemDocTable.Rows[i][1].ToString();
                                            recieptHeadder.Sar_manual_ref_no = start.ToString();
                                            recieptHeadder.Sar_receipt_no = _cusNo;
                                            recieptHeadder.Sar_com_cd = GlbUserComCode;
                                            recieptHeadder.Sar_receipt_type = TemDocTable.Rows[i][5].ToString();
                                            recieptHeadder.Sar_receipt_date = DateTime.Now;
                                            recieptHeadder.Sar_profit_center_cd = GlbUserDefProf;
                                            recieptHeadder.Sar_debtor_name = "CASH";
                                            recieptHeadder.Sar_remarks = "Cancel";
                                            recieptHeadder.Sar_act = false;
                                            recieptHeadder.Sar_mod_by = GlbUserName;
                                            recieptHeadder.Sar_mod_when = DateTime.Now;
                                            CHNLSVC.Sales.SaveReceiptHeader(recieptHeadder);

                                            //insert reciept item
                                            RecieptItem recieptItem = new RecieptItem();
                                            recieptItem.Sard_seq_no = seqNo;
                                            recieptItem.Sard_line_no = 1;
                                            recieptItem.Sard_ref_no = start.ToString();
                                            recieptItem.Sard_receipt_no = _cusNo;
                                            recieptItem.Sard_pay_tp = "CASH";
                                            recieptItem.Sard_settle_amt = 0;
                                            CHNLSVC.Sales.SaveReceiptItem(recieptItem);
                                        }
                                        
                                    }
                                    CHNLSVC.Inventory.UpdateGntManDocDt(GlbUserDefLoca, TemDocTable.Rows[i][5].ToString(), "0", Convert.ToInt32(TemDocTable.Rows[i][3]), TemDocTable.Rows[i][1].ToString(), TemDocTable.Rows[i][0].ToString());
                                    CHNLSVC.Inventory.UpdateGntManDocPages(TemDocTable.Rows[i][1].ToString(), GlbUserDefLoca, DropDownListDocumentType.SelectedItem.Value, Convert.ToInt32(TemDocTable.Rows[i][2]), Convert.ToInt32(TemDocTable.Rows[i][3]), GlbUserName);
                                }
                                else
                                {
                                    if (TemDocTable.Rows[i][5].ToString() == "HPRS" || TemDocTable.Rows[i][5].ToString() == "HPRM")
                                    {
                                        int start = Convert.ToInt32(TemDocTable.Rows[i][2]);
                                        int end = Convert.ToInt32(TemDocTable.Rows[i][3]);
                                        //save reciept and reciept item for every cancel page no
                                        for (; end >= start; start++)
                                        {
                                           
                                            MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                                            _receiptAuto.Aut_cate_cd = GlbUserDefProf;
                                            _receiptAuto.Aut_cate_tp = "PC";
                                            _receiptAuto.Aut_start_char = TemDocTable.Rows[i][5].ToString();
                                            _receiptAuto.Aut_direction = 1;
                                            _receiptAuto.Aut_modify_dt = null;
                                            _receiptAuto.Aut_moduleid = "HP";
                                            _receiptAuto.Aut_number = 0;
                                            _receiptAuto.Aut_year = null;
                                            string _cusNo = CHNLSVC.Sales.GetRecieptNo(_receiptAuto);
                                            //get seq no
                                            int seqNo = CHNLSVC.Inventory.GetSerialID();
                                            //insert reciept
                                            RecieptHeader recieptHeadder = new RecieptHeader();
                                            recieptHeadder.Sar_seq_no = seqNo;//start
                                            recieptHeadder.Sar_prefix = TemDocTable.Rows[i][1].ToString();
                                            recieptHeadder.Sar_receipt_no = _cusNo;
                                            recieptHeadder.Sar_manual_ref_no = start.ToString();
                                            recieptHeadder.Sar_com_cd = GlbUserComCode;
                                            recieptHeadder.Sar_receipt_type = TemDocTable.Rows[i][5].ToString();
                                            recieptHeadder.Sar_receipt_date = DateTime.Now;
                                            recieptHeadder.Sar_profit_center_cd = GlbUserDefProf;
                                            recieptHeadder.Sar_debtor_name = "CASH";
                                            recieptHeadder.Sar_remarks = "Cancel";
                                            recieptHeadder.Sar_act = false;
                                            recieptHeadder.Sar_mod_by = GlbUserName;
                                            recieptHeadder.Sar_mod_when = DateTime.Now;
                                            CHNLSVC.Sales.SaveReceiptHeader(recieptHeadder);

                                            //insert reciept item
                                            RecieptItem recieptItem = new RecieptItem();
                                            recieptItem.Sard_seq_no = seqNo;//start
                                            recieptItem.Sard_line_no = 1;
                                            recieptItem.Sard_ref_no = start.ToString();
                                            recieptItem.Sard_receipt_no = _cusNo; 
                                            recieptItem.Sard_pay_tp = "CASH";
                                            recieptItem.Sard_settle_amt = 0;
                                            CHNLSVC.Sales.SaveReceiptItem(recieptItem);
                                        }
                                    }
                                    //update using and used value
                                    CHNLSVC.Inventory.UpdateGntManDocDt(GlbUserDefLoca, TemDocTable.Rows[i][5].ToString(), "1", Convert.ToInt32(TemDocTable.Rows[i][3]), TemDocTable.Rows[i][1].ToString(), TemDocTable.Rows[i][0].ToString());
                                    CHNLSVC.Inventory.UpdateGntManDocPages(TemDocTable.Rows[i][1].ToString(), GlbUserDefLoca, DropDownListDocumentType.SelectedItem.Value, Convert.ToInt32(TemDocTable.Rows[i][2]), Convert.ToInt32(TemDocTable.Rows[i][3]), GlbUserName);
                                }
                            }
                            catch (Exception ex)
                            {
                                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Error occured while processing\n" + ex.Message);
                                return;
                            }
                        }
                        GridViewDocuments.DataSource = CHNLSVC.Inventory.Get_all_manual_docs_by_type(GlbUserDefLoca, DropDownListDocumentType.SelectedItem.Value);
                        GridViewDocuments.DataBind();
                        CreateTableColumns();
                        GridDataBind();
                        DivRecieptRange.Visible = false;
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Records cancelled sucessfully!');", true);
                        ButtonSave.Attributes.Clear();
                    }
            }
        }


        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Inventory_Module/ManualDocCancel.aspx");
        }

        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

        
    }
}