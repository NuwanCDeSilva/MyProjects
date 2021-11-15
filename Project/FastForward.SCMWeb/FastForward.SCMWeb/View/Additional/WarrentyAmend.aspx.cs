using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
using FF.BusinessObjects.InventoryNew;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Additional
{

    public partial class WarrentyAmend : Base//System.Web.UI.Page
    {
        List<WarrantyAmendRequest> _request = new List<WarrantyAmendRequest>();
        List<SerialMasterLog> serialMasterLogs = new List<SerialMasterLog>();
        List<InventoryWarrantyDetail> warrantyDetails = new List<InventoryWarrantyDetail>();
        SerialMasterLog serialMasterLog = new SerialMasterLog();
        InventoryWarrantyDetail warrantyDetail = new InventoryWarrantyDetail();
        WarrantyAmendRequest amendRequest = new WarrantyAmendRequest();
        InventorySerialMaster inventorySerialMaster = new InventorySerialMaster();
        MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
        string userid ;
        string session;

        Base _basepage;
        DataTable glbdt = new DataTable();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                dgvItemDetails.DataSource = new int[] { }; 
                
                dgvItemDetails.DataBind();
                dgvChanges.DataSource = new int[] { };
                dgvChanges.DataBind();
                dgvReqData.DataSource = new int[] { };
                dgvReqData.DataBind();
                txtFromDate.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                _request = new List<WarrantyAmendRequest>();
                Session["_request"] = _request;
                chkReq.Checked = false;
                lbtnRequest.Enabled = false;
                panelItemDetails.Visible = false;
                CheckBox chkboxSelectAllCom = (CheckBox)dgvReqData.HeaderRow.FindControl("chkboxSelectAllCom");
                chkboxSelectAllCom.Visible = dgvReqData.Rows.Count > 1 ? true : false;

                //Permition
                bool b10125 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10125);
                bool b10126 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10126);
                if (b10125 && b10126)
                {
                    lbtnRequest.Enabled = true;
                    lbtnRequest.OnClientClick = "SaveConfirm()";
                    lbtnRequest.CssClass = "buttonUndocolor";
                }
                else
                {
                    lbtnRequest.Enabled = false;
                    lbtnRequest.OnClientClick = "";
                    lbtnRequest.CssClass = "buttoncolor";
                }

                if (b10126)
                {
                    lbtnApprove.Enabled = true;
                    lbtnApprove.OnClientClick = "AproveConfirm()";
                    lbtnApprove.CssClass = "buttonUndocolor";
                }
                else
                {
                    lbtnApprove.Enabled = false;
                    lbtnApprove.OnClientClick = "";
                    lbtnApprove.CssClass = "buttoncolor";
                }
                
                lbtnReject.Enabled = false;
                lbtnReject.CssClass = "buttoncolor";
                lbtnReject.OnClientClick = "";

                //....................
                //pageClear();
               ViewState["warranty"] = null;
               glbdt.Columns.AddRange(new DataColumn[14] { new DataColumn("ItemCode"), new DataColumn("Description"), new DataColumn("Model"), 
                                                                        new DataColumn("Serial#"),new DataColumn("Warranty#"),new DataColumn("Invoice#"),new DataColumn("Invoice Date"),
                                                                      new DataColumn("Customer Code"),new DataColumn("Customer Name"),new DataColumn("Do#"), new DataColumn("Do Date"),new DataColumn("Warranty Period")
                                                                      ,new DataColumn("Warranty Remarks"),new DataColumn("serial id")   });

               if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10125)) 
               {
                   //CheckBox2.Enabled = false;
                   //LinkButton11.Enabled = false;
                   //LinkButton6.Enabled = false;
               
               }

               if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10126)) 
               {
               //    CheckBox2.Enabled = true;
               //    LinkButton11.Enabled = true;
               //    LinkButton6.Enabled = true;
               }




            }

        }

        //public void pageClear() 
        //{

        //    gvSubSerial.DataSource = new int[] { };
        //    gvSubSerial.DataBind();

        //    GridView1.DataSource = new int[] { };
        //    GridView1.DataBind();
        //}

        //protected void LinkButton11_Click(object sender, EventArgs e)
        //{
        //    //if (GridView1.Rows.Count<1)
        //    //{
        //    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No amendments made for approval')", true);
        //    //    return;
        //    //}
        //  /*  TextBox10.Visible = true;
        //    TextBox1.Visible = false;

        //    TextBox11.Visible = true;
        //    LinkButton9.Visible = true;

        //    LinkButton1.Visible = false;
        //    btnETD.Visible = true;

        //    Label1.Visible = false;
        //    Label2.Visible = true;

        //    Label3.Visible = false;
        //    Label4.Visible = true;

        //    LinkButton2.Visible = false;
        //    TextBox2.Visible = false;



        //    //invoice 
        //    Label5.Visible = false;
        //    TextBox3.Visible = false;
        //    LinkButton3.Visible = false;

        //    //DO
        //    Label6.Visible = false;
        //    TextBox4.Visible = false;
        //    LinkButton4.Visible = false;
        //    */

        //    _basepage = new Base();
        //    //approval
        //    string approval = "A";
        //    userid = Session["UserID"].ToString();
        //    session = Session["SessionID"].ToString();

        //    if (CheckBox2.Checked == true)
        //    {
        //        //  int eff = _basepage.CHNLSVC.Inventory.warranty_amend_insert(userid, warrenty_dt1, session, id1,"S",userid, warrenty_dt1, "");
        //        try
        //        {
        //            for (int i = 0; i < GridView1.Rows.Count; i++)
        //            {
        //                //
        //                string datetest = GridView1.Rows[i].Cells[6].Text;

        //                int id1 = int.Parse(GridView1.Rows[i].Cells[13].Text);

        //                int warperiod = int.Parse(GridView1.Rows[i].Cells[11].Text);
        //                string remarks = GridView1.Rows[i].Cells[12].Text;
        //                string customer_code = GridView1.Rows[i].Cells[7].Text;

        //                //



        //                int eff = _basepage.CHNLSVC.Inventory.warranty_amend_insert(userid, DateTime.Now, session, id1, approval, userid, DateTime.Now, session, warperiod, remarks, DateTime.Now, customer_code);
        //                if (eff == 1)
        //                {
        //                   displaySuccessNotification("Insert Successfully");
        //                }

        //            }

        //        }

        //        catch (Exception ex) {

        //            displaynotification(ex.Message);
        //        }
        //    }



        //}

        //protected void checkbox1_changed(object sender, EventArgs e)
       
        //{
        //   // int count=gvSubSerial.Rows.Count;
           
        //    for (int i = 0; i < gvSubSerial.Rows.Count; i++) {
        //        CheckBox chk = (CheckBox)gvSubSerial.Rows[i].Cells[0].FindControl("CheckBox1");
        //        if (chk.Checked)
        //        { 
        //    TextBox5.Text = gvSubSerial.Rows[i].Cells[12].Text;
        //    TextBox6.Text = gvSubSerial.Rows[i].Cells[13].Text;
        //    TextBox7.Text = gvSubSerial.Rows[i].Cells[8].Text;
        //    TextBox8.Text = gvSubSerial.Rows[i].Cells[9].Text;
        //    TextBox9.Text = gvSubSerial.Rows[i].Cells[7].Text;
        //    }
        //    }

        //}


        //protected void LinkButton3_Click(object sender, EventArgs e)
        //{

        //    _basepage = new Base();
        //    string serial = TextBox1.Text;
        //    string warranty = TextBox2.Text;
        //    string invoice = TextBox3.Text+'%';
        //    string dono = TextBox4.Text;
        //    DataTable dt = _basepage.CHNLSVC.Inventory.InvoiceWarranty_INFO(serial, warranty, invoice, dono);

            

        //    if (dt.Rows.Count == 0) 
        //    {
        //        displaynotification("Enter Valid Invoice No");
        //    }else{

        //    gvSubSerial.DataSource = dt;
        //    gvSubSerial.DataBind();
        //        }


        //}

        public void displaynotification(string msg)
        {

            //  ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "');", true);
            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickyWarningToast", "showStickyWarningToast('" + msg + "')", true);

        }

        public void displaySuccessNotification(string msg) 
        {

            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "showStickySuccessToast", "showStickySuccessToast('" + msg + "')", true);


        }



        //protected void LinkButton2_Click(object sender, EventArgs e)
        //{
        //    //_basepage = new Base();
        //    //DataTable dt = _basepage.CHNLSVC.Inventory.invoiceInfo();
        //    //BLLoad.DataSource = dt;
        //    //BLLoad.DataBind();
        //    //UserBL.Show();
        //    _basepage = new Base();
        //    string serial = TextBox1.Text;
        //    string warranty = TextBox2.Text+'%';
        //    string invoice = TextBox3.Text;
        //    string dono = TextBox4.Text;
        //    DataTable dt = _basepage.CHNLSVC.Inventory.InvoiceWarranty_INFO(serial, warranty, invoice, dono);
        //    gvSubSerial.DataSource = dt;
        //    gvSubSerial.DataBind();


        //}

        //protected void LinkButton1_Click(object sender, EventArgs e)
        //{
        //    _basepage = new Base();
        //    string serial = TextBox1.Text +'%';
        //    string warranty = TextBox2.Text;
        //    string invoice = TextBox3.Text;
        //    string dono = TextBox4.Text;
        //    DataTable dt = _basepage.CHNLSVC.Inventory.InvoiceWarranty_INFO(serial, warranty, invoice, dono);
        //    gvSubSerial.DataSource = dt;
        //    gvSubSerial.DataBind();
        //}

        //protected void LinkButton4_Click(object sender, EventArgs e)
        //{

        //    _basepage = new Base();
        //    ViewState["do"] = null;      
        //    string serial = TextBox1.Text;
        //    string warranty = TextBox2.Text;
        //    string invoice = TextBox3.Text;
        //    string dono = TextBox4.Text+'%';
        //    DataTable dt = _basepage.CHNLSVC.Inventory.InvoiceWarranty_INFO(serial, warranty, invoice, dono);
            
        //    if (dt.Rows.Count == 0) {

        //        displaynotification("Enter Valid DO#");
        //    }
        //    else { 
        //    gvSubSerial.DataSource = dt;
        //    ViewState["do"] = dt;
        //    gvSubSerial.DataBind();
        //    Label9.Text = "35";
        //    }
        //}

        //protected void gvSubSerial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvSubSerial.PageIndex = e.NewPageIndex;
        //    gvSubSerial.DataSource = null;
        //    if (Label9.Text == "35") 
        //    {
        //        gvSubSerial.DataSource =(DataTable)ViewState["do"];
        //        gvSubSerial.DataBind();
        //    }

        //    else if (Label9.Text == "40") 
        //    {
        //        gvSubSerial.DataSource = ViewState["AllSearch"];
        //        gvSubSerial.DataBind();
            
        //    }
        //}

        //protected void LinkButton10_Click(object sender, EventArgs e)
        //{

        //    _basepage = new Base();
        //    ViewState["AllSearch"] = null;
        //    string serial = TextBox1.Text+'%';
        //    string warranty = TextBox2.Text+'%';
        //    string invoice = TextBox3.Text +'%';
        //    string dono = TextBox4.Text + '%';
        //    DataTable dt = _basepage.CHNLSVC.Inventory.InvoiceWarranty_INFO(serial, warranty, invoice, dono);

        //    if (dt.Rows.Count == 0)
        //    {

        //        displaynotification("Enter Valid Details");
        //    }

        //    else 
        //    {
        //        gvSubSerial.DataSource = dt;
        //        gvSubSerial.DataBind();
        //        Label9.Text = "40";
        //        ViewState["AllSearch"] = dt;
            
        //    }


        //}
       


        private void AddRow(DataTable table)
        {
            DataRowCollection rowCollection = table.Rows;
            // Instantiate a new row using the NewRow method.

            DataRow newRow = table.NewRow();
            // Insert code to fill the row with values.

            // Add the row to the DataRowCollection.
            table.Rows.Add(newRow);
        }
        private void ShowRows(DataTable table)
        {
            // Print the number of rows in the collection.
            Console.WriteLine(table.Rows.Count);
            // Print the value of columns 1 in each row
            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine(row[1]);
            }
        }


        
        protected void LinkButton7_Click(object sender, EventArgs e)
        {




            selectedRows();
            BindSecondGrid();
          //  CheckBox1.Checked = false;

            //for (int i = 0; i < gvSubSerial.Rows.Count; i++)
            //{
            //    CheckBox chk = (CheckBox)gvSubSerial.Rows[i].Cells[0].FindControl("CheckBox1");
            //    if (chk.Checked)
            //    {
            //        chk.Checked = false;
            //    }
            //}


            /*
       DataTable    dtgetselectedRecords = new DataTable();

            dtgetselectedRecords.Columns.AddRange(new DataColumn[14] { new DataColumn("ItemCode"), new DataColumn("Description"), new DataColumn("Model"), 
                                                                        new DataColumn("Serial#"),new DataColumn("Warranty#"),new DataColumn("Invoice#"),new DataColumn("Invoice Date"),
                                                                      new DataColumn("Customer Code"),new DataColumn("Customer Name"),new DataColumn("Do#"), new DataColumn("Do Date"),new DataColumn("Warranty Period")
                                                                      ,new DataColumn("Warranty Remarks"),new DataColumn("serial id")   });
           
            
            ViewState["warranty"] = dtgetselectedRecords;

            if (ViewState["warranty"] == null) 
            {
                displaynotification("datasouse null");
            
            }



            foreach (GridViewRow gvrow in gvSubSerial.Rows)
            {

                if (gvrow.RowType == DataControlRowType.DataRow)
                {
                    //Finding the Checkbox in the Gridview
                    CheckBox chkSelect = (gvrow.Cells[0].FindControl("CheckBox1") as CheckBox);
                    // Checking which checkbox are selected
                    if (chkSelect.Checked)
                    {
                        // assigning the records to a string to the cells
                        string itemcode = gvrow.Cells[1].Text;
                        string description=gvrow.Cells[2].Text;
                        string model = gvrow.Cells[3].Text;
                        string serial = gvrow.Cells[4].Text;
                        string warranty = gvrow.Cells[5].Text;
                        string invoice = gvrow.Cells[6].Text;
                        string invoice_date = gvrow.Cells[7].Text;
                        string customer_code = gvrow.Cells[8].Text;
                        string customer_name = gvrow.Cells[9].Text;
                        string doNo = gvrow.Cells[10].Text;
                        string doDt = gvrow.Cells[11].Text;
                        string warranty_period = gvrow.Cells[12].Text;
                        string warrantyremarks = gvrow.Cells[13].Text;
                        string serialid = gvrow.Cells[14].Text;
                      //  gvrow.Cells[14].Visible = false;
                        // Adding the Rows to the datatable
                        dtgetselectedRecords.Rows.Add(itemcode, description, model, serial, warranty, invoice, invoice_date, customer_code, customer_name, doNo, doDt, warranty_period, warrantyremarks, serialid);

                    }
                }
            }
           // ViewState["warranty"] = null;
           
            GridView1.DataSource = dtgetselectedRecords;
            ViewState["warranty"] = dtgetselectedRecords;

            int count = dtgetselectedRecords.Rows.Count;

            if (count == 0)
            {
               // ViewState["warranty"] = null;


            }
            else 
            {
                DataTable dt = (DataTable)ViewState["warranty"];

                ViewState["warranty"] = dtgetselectedRecords;
                GridView1.DataBind();
            }

            GridView1.DataBind();
            */
            

       /*     if (count == 1) 
            {
                TextBox5.Text = gvSubSerial.Rows[0].Cells[12].Text;
                TextBox6.Text = gvSubSerial.Rows[0].Cells[13].Text;
                TextBox7.Text = gvSubSerial.Rows[0].Cells[8].Text;
                TextBox8.Text=GridView1.Rows[0].Cells[9].Text;
                TextBox9.Text = GridView1.Rows[0].Cells[7].Text;


            }

            */

            

           // glbdt.Rows()
           // CheckBox chkSelect = (gvSubSerial.Cells[0].FindControl("CheckBox1") as CheckBox);
           
                //  gvrow.Cells[14].Visible = false;
                // Adding the Rows to the datatable
          /*              string itemcode="200";
                        string description="";
                        string model="";
                        string serial ="";
                        string warranty="";
                        string invoice="";
                        string invoice_date=""; 
                        string customer_code ="";
                        string customer_name ="";
                        string doNo ="";
                        string doDt ="";
                        string warranty_period ="";
                        string warrantyremarks ="";
                        string serialid ="";
            */




        }

        protected void gvSubSerial_SelectedIndexChanged(object sender, EventArgs e)
        {

           //// CheckBox chkSelect = (gvSubSerial.SelectedRow[0].FindControl("CheckBox1") as CheckBox);

           // CheckBox chkSelect =(CheckBox) gvSubSerial.SelectedRow.Cells[0].FindControl("CheckBox1");
           // if (chkSelect.Checked == true)
           // {
           //     TextBox5.Text = gvSubSerial.SelectedRow.Cells[0].Text;
           // }

        }

       

        protected void BindSecondGrid()
        {
            DataTable dt = (DataTable)ViewState["GetRecords"];
            //GridView1.DataSource = dt;
            //GridView1.DataBind();
        }

        private void selectedRows() 
        {
            //DataTable dt;
            //if (ViewState["GetRecords"] != null)
            //    dt = (DataTable)ViewState["GetRecords"];
            //else
            //    dt = CreateTable();
            //for (int i = 0; i < gvSubSerial.Rows.Count; i++)
            //{
            //    CheckBox chk = (CheckBox)gvSubSerial.Rows[i].Cells[0].FindControl("CheckBox1");
            //    if (chk.Checked)
            //    {
            //        dt = AddGridRow(gvSubSerial.Rows[i], dt);
            //    }
            //    else
            //    {
            //    //    dt = RemoveRow(gvSubSerial.Rows[i], dt);
            //    }

            //    //CheckBox1
            //}

            //ViewState["GetRecords"] = dt;
        }

        private DataTable CreateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ItemCode");
            dt.Columns.Add("Description");
            dt.Columns.Add("Model");
            dt.Columns.Add("Serial#");
            dt.Columns.Add("Warranty#");
            dt.Columns.Add("Invoice#");
            dt.Columns.Add("Invoice Date");
            dt.Columns.Add("Customer Code");
            dt.Columns.Add("Customer Name");
            dt.Columns.Add("Do#");
            dt.Columns.Add("Do Date");
            dt.Columns.Add("Warranty Period");
            dt.Columns.Add("Warranty Remarks");
            dt.Columns.Add("id");
            dt.AcceptChanges();
            return dt;
        }

        private DataTable AddGridRow(GridViewRow gvRow, DataTable dt)
        {
            DataRow[] dr = dt.Select("ItemCode = '" + gvRow.Cells[1].Text + "'");
            if (dr.Length <= 0)
            {
                dt.Rows.Add();
                int rowscount = dt.Rows.Count - 1;
                dt.Rows[rowscount]["ItemCode"] = gvRow.Cells[1].Text;
                dt.Rows[rowscount]["Description"] = gvRow.Cells[2].Text;
                dt.Rows[rowscount]["Model"] = gvRow.Cells[3].Text;
                dt.Rows[rowscount]["Serial#"] = gvRow.Cells[4].Text;
                dt.Rows[rowscount]["Warranty#"] = gvRow.Cells[5].Text;
                dt.Rows[rowscount]["Invoice#"] = gvRow.Cells[6].Text;
                dt.Rows[rowscount]["Invoice Date"] = gvRow.Cells[7].Text;
               // dt.Rows[rowscount]["Customer Code"] = gvRow.Cells[8].Text;
               // dt.Rows[rowscount]["Customer Code"] = TextBox7.Text;
               // dt.Rows[rowscount]["Customer Name"] = gvRow.Cells[9].Text;
               // dt.Rows[rowscount]["Customer Name"] = TextBox8.Text;
                dt.Rows[rowscount]["Do#"] = gvRow.Cells[10].Text;
                dt.Rows[rowscount]["Do Date"] = gvRow.Cells[11].Text;
               // dt.Rows[rowscount]["Warranty Period"] = gvRow.Cells[12].Text;
               // dt.Rows[rowscount]["Warranty Period"] = TextBox5.Text;
               // dt.Rows[rowscount]["Warranty Remarks"] = gvRow.Cells[13].Text;
               // dt.Rows[rowscount]["Warranty Remarks"] = TextBox6.Text;
                dt.Rows[rowscount]["id"] = gvRow.Cells[14].Text;
                
                dt.AcceptChanges();
            }
            return dt;
        }

        protected void LinkButton6_Click(object sender, EventArgs e)
        {
         //   AddRow(dtgetselectedRecords);

     //       selectedRows();
     //       BindSecondGrid();

            //reject

            string approvals = "R";

            //if (CheckBox2.Checked == true)
            //{
            //    //  int eff = _basepage.CHNLSVC.Inventory.warranty_amend_insert(userid, warrenty_dt1, session, id1,"S",userid, warrenty_dt1, "");
            //    try
            //    {
            //        //for (int i = 0; i < GridView1.Rows.Count; i++)
            //        //{
            //        //    //
            //        //    string datetest = GridView1.Rows[i].Cells[6].Text;

            //        //    int id1 = int.Parse(GridView1.Rows[i].Cells[13].Text);

            //        //    int warperiod = int.Parse(GridView1.Rows[i].Cells[11].Text);
            //        //    string remarks = GridView1.Rows[i].Cells[12].Text;
            //        //    string customer_code = GridView1.Rows[i].Cells[7].Text;

            //        //    //



            //        //    int eff = _basepage.CHNLSVC.Inventory.warranty_amend_insert(userid, DateTime.Now, session, id1, approvals, userid, DateTime.Now, session, warperiod, remarks, DateTime.Now, customer_code);
            //        //   /* if (eff == 1)
            //        //    {
            //        //        displaySuccessNotification("Insert Successfully");
            //        //    }
            //        //    */
            //        //}
            //        displaySuccessNotification("Insert Successfully");
            //    }

            //    catch (Exception ex) 
            //    {
            //        displaynotification(ex.Message);
            //    }
            //}



            
        }


        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Item_Serials:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.WarraSerialNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItems:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SalesOrder:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + "SO" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceType:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceWithDate:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Currency:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
               
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.NIC:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Mobile:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Employee_Executive:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "SEX" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceExecutive:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Receipt:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + CommonUIDefiniton.PayMode.ADVAN.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Group_Sale:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + 1 + seperator);
                        break;
                    }

               
              
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableGiftVoucher:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + "ITEM" + seperator);
                        break;
                    }
                
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SalesOrderNew:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.DocNo:
                //    {
                //        paramsText.Append("ADVAN" + seperator + Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                //        break;
                //    }
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


        protected void LinkButton13_Click(object sender, EventArgs e)
        {
            _basepage = new Base();

            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result = _basepage.CHNLSVC.CommonSearch.GetSupplierCommonNew(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "13";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show();
                txtSearchbyword.Text ="";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                //DisplayMessage(ex.Message, 4);
            }
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TextBox7.Text = grdResult.SelectedRow.Cells[1].Text;
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {

        }

        private void DisplayMessage(String Msg, Int32 option)
        {
            Msg = Msg.Replace(@"\r", "").Replace(@"\n", "");
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
               // WriteErrLog(Msg);
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
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }

        }
        string sp;
        //protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (CheckBox2.Checked)
        //    {
        //        TextBox10.Visible = true;
        //        TextBox1.Visible = false;

        //        TextBox11.Visible = true;
        //        LinkButton9.Visible = true;

        //        LinkButton1.Visible = false;
        //        btnETD.Visible = true;

        //        Label1.Visible = false;
        //        Label2.Visible = true;

        //        Label3.Visible = false;
        //        Label4.Visible = true;

        //        LinkButton2.Visible = false;
        //        TextBox2.Visible = false;

        //        //search
        //        LinkButton10.Visible = false;
        //        LinkButton_date.Visible = true;



        //        //invoice 
        //        Label5.Visible = false;
        //        TextBox3.Visible = false;
        //        LinkButton3.Visible = false;

        //        //DO
        //        Label6.Visible = false;
        //        TextBox4.Visible = false;
        //        LinkButton4.Visible = false;

        //        _basepage = new Base();
        //        DataTable dt = _basepage.CHNLSVC.Inventory.get_warrenty_ammendData();
        //        ViewState["search_date_data"] = null;
        //        GridView2.DataSource = dt;
        //        ViewState["search_date_data"] = dt;
        //        GridView2.DataBind();
        //        Label9.Text = "35";
        //        gvSubSerial.Visible = false;
        //        GridView2.Visible = true;
        //        sp = "fa";

        //    }
        //    else 
        //    {
        //        TextBox10.Visible = false;
        //        TextBox1.Visible = true;

        //        TextBox11.Visible = false;
        //        LinkButton9.Visible = false;

        //        LinkButton1.Visible = true;
        //        btnETD.Visible = false;

        //        Label1.Visible = true;
        //        Label2.Visible = false;

        //        Label3.Visible = true;
        //        Label4.Visible = false;

        //        LinkButton2.Visible = true;
        //        TextBox2.Visible = true;



        //        //invoice 
        //        Label5.Visible = true;
        //        TextBox3.Visible = true;
        //        LinkButton3.Visible = true;

        //        //DO
        //        Label6.Visible = true;
        //        TextBox4.Visible = true;
        //        LinkButton4.Visible = true;


        //        LinkButton10.Visible = true;
        //        LinkButton_date.Visible = false;

        //        gvSubSerial.Visible = true;
        //        GridView2.Visible = false;

        //    }

            
        //}

        //protected void LinkButton_date_Click(object sender, EventArgs e)
        //{
        //    _basepage = new Base();

        //    ViewState["search_date"] = null;
        //    if (CheckBox2.Checked == true) { 

        //    DataTable dt = _basepage.CHNLSVC.Inventory.filter_warranty_approve(TextBox10.Text, TextBox11.Text);
        //    GridView2.DataSource = dt;
        //    ViewState["search_date"] =dt;
        //    GridView2.DataBind();

        //    }

        //}

        //protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{

        //    if (Label9.Text=="35")
        //    {
        //        GridView2.PageIndex = e.NewPageIndex;
        //        GridView2.DataSource = null;

        //        GridView2.DataSource = (DataTable)ViewState["search_date_data"];
        //        GridView2.DataBind();

        //    }
        //    else { 

        //    GridView2.PageIndex = e.NewPageIndex;
        //    GridView2.DataSource = null;

        //    GridView2.DataSource = (DataTable)ViewState["search_date"];
        //    GridView2.DataBind();
        //    }

        //}

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            if (hdfClearData.Value == "1")
            {
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void dgvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvResult.PageIndex = e.NewPageIndex;
                DataTable _result = null;
                if (lblSearchType.Text == "WarraSerialNo")
                {
                    _result = (DataTable)Session["WarraSerialNo"];
                }
                else if (lblSearchType.Text == "Country")
                {
                    _result = (DataTable)Session["Country"];
                }
                else if (lblSearchType.Text == "Country")
                {
                    _result = (DataTable)Session["Country"];
                }
                else if (lblSearchType.Text == "Country")
                {
                    _result = (DataTable)Session["Country"];
                }
                else if (lblSearchType.Text == "Customer")
                {
                    _result = (DataTable)Session["Customer"];
                }
                if (_result.Rows.Count > 0)
                {
                    dgvResult.DataSource = _result;
                }
                else
                {
                    dgvResult.DataSource = null;
                }
                dgvResult.DataBind();
                txtSearchbyword.Text = string.Empty;
                txtSearchbyword.Focus();
                PopupSearch.Show();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void dgvResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string code = dgvResult.SelectedRow.Cells[1].Text;
                string desc = dgvResult.SelectedRow.Cells[2].Text;

                if (lblSearchType.Text == "Item_Serials")
                {
                    txtSerial.Text = code;
                    txtSerial_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "WarraSerialNo")
                {
                    txtWarNo.Text = code;
                    txtWarNo_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "InvoiceItems")
                {
                    txtInvoiceNo.Text = code;
                    txtInvoiceNo_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "DocNo")
                {
                    txtDoNo.Text = code;
                    txtDoNo_TextChanged(null, null);
                }
                else if (lblSearchType.Text == "Customer")
                {
                    txtCustomer.Text = code;
                    txtCustomer_TextChanged(null, null);
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
       
        protected void lbtnSearch1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _dt = null;
                Session["Item_Serials"] = null;
                Session["WarraSerialNo"] = null;
                Session["InvoiceItems"] = null;
                Session["DocNo"] = null;
                Session["Customer"] = null;

                if (lblSearchType.Text == "Item_Serials")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                    _dt = CHNLSVC.CommonSearch.SearchWarrentyAmendSerial(para, cmbSearchbykey.SelectedValue,  txtSearchbyword1.Text);
                    _dt.Columns["Serial #"].SetOrdinal(0);
                    Session["Item_Serials"] = _dt;
                }
                else if (lblSearchType.Text == "WarraSerialNo")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WarraSerialNo);
                    _dt = CHNLSVC.CommonSearch.SearchWarrentyAmendWarrenty(para, cmbSearchbykey.SelectedValue,  txtSearchbyword1.Text);
                    _dt.Columns["Warranty #"].SetOrdinal(0);
                    Session["WarraSerialNo"] = _dt;
                }
                else if (lblSearchType.Text == "InvoiceItems")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItems);
                    _dt = CHNLSVC.CommonSearch.SearchWarrentyAmendInvoice(para, cmbSearchbykey.SelectedValue,  txtSearchbyword1.Text);
                    _dt.Columns["Invoice #"].SetOrdinal(0);
                    Session["InvoiceItems"] = _dt;
                }
                else if (lblSearchType.Text == "DocNo")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                    _dt = CHNLSVC.CommonSearch.SearchWarrentyAmendDocNo(para, cmbSearchbykey.SelectedValue,  txtSearchbyword1.Text);
                    _dt.Columns["Document #"].SetOrdinal(0);
                    Session["DocNo"] = _dt;
                }
                else if (lblSearchType.Text == "Customer")
                {
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    _dt = CHNLSVC.CommonSearch.GetSupplierCommonNew(para, cmbSearchbykey.SelectedValue,  txtSearchbyword1.Text);
                    //_dt.Columns["Code"].SetOrdinal(0);
                    //_dt.Columns.Remove("City");
                    //_dt.Columns.Remove("Code1");
                    Session["Customer"] = _dt;
                }
                dgvResult.DataSource = null;
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
                if (_dt.Rows.Count > 0)
                {
                    dgvResult.DataSource = _dt;
                }
                txtSearchbyword1.Text = "";
                txtSearchbyword1.Focus();
                dgvResult.DataBind();
                PopupSearch.Show();
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
        public void BindUCtrlDDLData1(DataTable _dataSource)
        {
            this.cmbSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.cmbSearchbykey.Items.Add(col.ColumnName);
            }

            this.cmbSearchbykey.SelectedIndex = 0;
        }
        protected void chkReq_CheckedChanged(object sender, EventArgs e)
        {
            bool b10126 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10126);
            if (b10126 && chkReq.Checked)
            {
                lbtnReject.Enabled = true;
                lbtnReject.OnClientClick = "RejectConfirm()";
                lbtnReject.CssClass = "buttonUndocolor";
            }
            else
            {
                lbtnReject.Enabled = false;
                lbtnReject.OnClientClick = "";
                lbtnReject.CssClass = "buttoncolor";
            }
            panel1.Visible = !chkReq.Checked;
            panel2.Visible = chkReq.Checked;
            CheckBox chkboxSelectAllCom = (CheckBox)dgvReqData.HeaderRow.FindControl("chkboxSelectAllCom");
            chkboxSelectAllCom.Visible = dgvReqData.Rows.Count > 1 ? true : false;
            dgvItemDetails.DataSource = new int[] { };
            dgvItemDetails.DataBind();
            dgvChanges.DataSource = new int[] { };
            dgvChanges.DataBind();
            dgvReqData.DataSource = new int[] { };
            dgvReqData.DataBind();

            panelItemDetails.Visible = false;
            txtModel.Text = "";
            txtBrand.Text = "";
            txtStatus.Text = "";
            txtSerNo.Text = "";
            txtWarrNo.Text = "";
            txtWarrRem.Text = "";
            txtBrand.Text = "";
            txtPreWarSts.Text = "";
        }

        protected void lbtnSeSerial_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Item_Serials";
                Session["Item_Serials"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                DataTable _result = CHNLSVC.CommonSearch.SearchWarrentyAmendSerial(para, null, null);
                if (_result.Rows.Count > 0)
                {
                    _result.Columns["Serial #"].SetOrdinal(0); 
                    dgvResult.DataSource = _result;
                    Session["Item_Serials"] = _result;
                    BindUCtrlDDLData1(_result);
                }
                else
                {
                    dgvResult.DataSource = null;
                }
                dgvResult.DataBind();
                txtSearchbyword1.Text = "";
                txtSearchbyword1.Focus();
                PopupSearch.Show();
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
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

        protected void lbtnSeWarrenty_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "WarraSerialNo";
                Session["WarraSerialNo"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WarraSerialNo);
                DataTable _result = CHNLSVC.CommonSearch.SearchWarrentyAmendWarrenty(para, null, null);
                if (_result.Rows.Count > 0)
                {
                    _result.Columns["Warranty #"].SetOrdinal(0); 
                    dgvResult.DataSource = _result;
                    Session["WarraSerialNo"] = _result;
                    BindUCtrlDDLData1(_result);
                }
                else
                {
                    dgvResult.DataSource = null;
                }
                dgvResult.DataBind();
                txtSearchbyword1.Text = "";
                txtSearchbyword1.Focus();
                PopupSearch.Show();
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
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

        protected void lbtnSeInvoiceNo_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "InvoiceItems";
                Session["InvoiceItems"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItems);
                DataTable _result = CHNLSVC.CommonSearch.SearchWarrentyAmendInvoice(para, null, null);
                if (_result.Rows.Count > 0)
                {
                    _result.Columns["Invoice #"].SetOrdinal(0); 
                    dgvResult.DataSource = _result;
                    Session["InvoiceItems"] = _result;
                    BindUCtrlDDLData1(_result);
                }
                else
                {
                    dgvResult.DataSource = null;
                }
                dgvResult.DataBind();
                txtSearchbyword1.Text = "";
                txtSearchbyword1.Focus();
                PopupSearch.Show();
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
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

        protected void lbtnSeDo_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "DocNo";
                Session["DocNo"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                DataTable _result = CHNLSVC.CommonSearch.SearchWarrentyAmendDocNo(para, null, null);
                if (_result.Rows.Count > 0)
                {
                    _result.Columns["Document #"].SetOrdinal(0); 
                    dgvResult.DataSource = _result;
                    Session["DocNo"] = _result;
                    BindUCtrlDDLData1(_result);
                }
                else
                {
                    dgvResult.DataSource = null;
                }
                dgvResult.DataBind();
                txtSearchbyword1.Text = "";
                txtSearchbyword1.Focus();
                PopupSearch.Show();
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
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

        protected void lbtnSeCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                lblSearchType.Text = "Customer";
                Session["Customer"] = null;
                string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable _result = CHNLSVC.CommonSearch.GetSupplierCommonNew(para, null, null);
               // _result.Columns.Remove("City");
                if (_result.Rows.Count > 0)
                {
                  //  _result.Columns["Code"].SetOrdinal(0);
                    dgvResult.DataSource = _result;
                    Session["Customer"] = _result;
                    BindUCtrlDDLData1(_result);
                }
                else
                {
                    dgvResult.DataSource = null;
                }
                dgvResult.DataBind();
                txtSearchbyword1.Text = "";
                txtSearchbyword1.Focus();
                PopupSearch.Show();
                if (dgvResult.PageIndex > 0)
                { dgvResult.SetPageIndex(0); }
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

        protected void txtSerial_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSerial.Text != "")
                {
                    bool b2 = false;string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                    DataTable _result = CHNLSVC.CommonSearch.SearchWarrentyAmendSerial(para, "Serial #", txtSerial.Text);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Serial #"].ToString()))
                        {
                            if (txtSerial.Text == row["Serial #"].ToString())
                            {
                                b2 = true;//toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtSerial.ToolTip = toolTip;// txtSerial.Focus();
                        lbtnMainSearch1_Click(null,null);
                    }
                    else
                    {
                        dgvItemDetails.DataSource = new int[] { };
                        dgvItemDetails.DataBind();
                        txtSerial.ToolTip = "";txtSerial.Text = "";txtSerial.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid serial #')", true);return;
                    }
                }
                else
                {
                    txtSerial.ToolTip = "";
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

        protected void txtWarNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtWarNo.Text != "")
                {
                    bool b2 = false; string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                    DataTable _result = CHNLSVC.CommonSearch.SearchWarrentyAmendWarrenty(para, "Warranty #", txtWarNo.Text);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Warranty #"].ToString()))
                        {
                            if (txtWarNo.Text == row["Warranty #"].ToString())
                            {
                                b2 = true;//toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtWarNo.ToolTip = toolTip;// txtWarNo.Focus();
                        lbtnMainSearch1_Click(null, null);
                    }
                    else
                    {
                        dgvItemDetails.DataSource = new int[] { };
                        dgvItemDetails.DataBind();
                        txtWarNo.ToolTip = ""; txtWarNo.Text = ""; txtWarNo.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid warranty #')", true); return;
                    }
                }
                else
                {
                    txtWarNo.ToolTip = "";
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

        protected void txtInvoiceNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtInvoiceNo.Text != "")
                {
                    bool b2 = false; string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                    DataTable _result = CHNLSVC.CommonSearch.SearchWarrentyAmendInvoice(para, "Invoice #", txtInvoiceNo.Text);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Invoice #"].ToString()))
                        {
                            if (txtInvoiceNo.Text == row["Invoice #"].ToString())
                            {
                                b2 = true;//toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtInvoiceNo.ToolTip = toolTip;// txtInvoiceNo.Focus();
                        lbtnMainSearch1_Click(null, null);
                    }
                    else
                    {
                        dgvItemDetails.DataSource = new int[] { };
                        dgvItemDetails.DataBind();
                        txtInvoiceNo.ToolTip = ""; txtInvoiceNo.Text = ""; txtInvoiceNo.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid invoice #')", true); return;
                    }
                }
                else
                {
                    txtInvoiceNo.ToolTip = "";
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

        protected void txtDoNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDoNo.Text != "")
                {
                    bool b2 = false; string toolTip = "";
                    string para = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                    DataTable _result = CHNLSVC.CommonSearch.SearchWarrentyAmendInvoice(para, "Document #", txtDoNo.Text);
                    foreach (DataRow row in _result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["Document #"].ToString()))
                        {
                            if (txtDoNo.Text == row["Document #"].ToString())
                            {
                                b2 = true;//toolTip = row["Description"].ToString();
                                break;
                            }
                        }
                    }
                    if (b2)
                    {
                        txtDoNo.ToolTip = toolTip;// txtDoNo.Focus();
                        lbtnMainSearch1_Click(null, null);
                    }
                    else
                    {
                        dgvItemDetails.DataSource = new int[] { };
                        dgvItemDetails.DataBind();
                        txtDoNo.ToolTip = ""; txtDoNo.Text = ""; txtDoNo.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid document #')", true); return;
                    }
                }
                else
                {
                    txtDoNo.ToolTip = "";
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

        protected void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCustomer.Text != "")
                {
                    txtCustomer.ToolTip = "";
                    if (!CHNLSVC.General.CheckCustomer(txtCustomer.Text.Trim().ToUpper()))
                    {
                        txtCustomer.Text = ""; txtCustomer.Focus();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid customer code')", true); return;
                    }
                    txtCustomer.ToolTip = CHNLSVC.General.Get_Customer_desc(txtCustomer.Text.Trim().ToUpper());
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnMainSearch1_Click(object sender, EventArgs e)
        {
            try
            {
                string serial = txtSerial.Text!=""?txtSerial.Text:null; 
                string warranty = txtWarNo.Text!=""?txtWarNo.Text:null; 
                string inv = txtInvoiceNo.Text!=""?txtInvoiceNo.Text:null; 
                string doc = txtDoNo.Text!=""?txtDoNo.Text:null;
                string location = Session["UserDefLoca"].ToString();
                if (chkloc.Checked)
                {
                    location = string.Empty;
                }
                    
                DataTable dt=new DataTable();
                dt = CHNLSVC.CommonSearch.SearchWarrentyAmendData(Session["UserCompanyCode"].ToString(), "", serial, warranty, inv, doc);
                if (dt.Rows.Count>0)
                {
                    dgvItemDetails.DataSource = dt;
                }
                else
                {
                    dgvItemDetails.DataSource = new int[] { };
                }
                dgvItemDetails.DataBind();
                hdfRowCountItems.Value = dt.Rows.Count.ToString();
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

        protected void lbtnAddChnages_Click(object sender, EventArgs e)
        {
            bool select = false;
            foreach (GridViewRow row in dgvItemDetails.Rows)
            {
                CheckBox chkSelectItem = (CheckBox)row.FindControl("chkSelectItem");
                if (chkSelectItem.Checked)
                {
                    select = true;
                    break;
                }
            }
            if (!select)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a item to add !')", true); return;
            }
            if (String.IsNullOrEmpty(txtChangePeriod.Text) && String.IsNullOrEmpty(txtCustomer.Text) && String.IsNullOrEmpty(txtComDate.Text) )
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter data to add !')", true); return;
            }
            if (!String.IsNullOrEmpty(txtChangePeriod.Text) && String.IsNullOrEmpty(txtRemarksPeriod.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter remarks to add !')", true); return;
            }
            if (!String.IsNullOrEmpty(txtChangePeriod.Text))
            {
                try
                {
                  int i=  Convert.ToInt32(txtChangePeriod.Text);
                  if (i<0)
                  {
                      ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('The change period is invalid')", true); return;
                  }
                }
                catch (Exception)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('The change period is invalid')", true); return;
                }
            }
            if (!String.IsNullOrEmpty(txtCustomer.Text))
            {
                txtCustomer.ToolTip = "";
                if (!CHNLSVC.General.CheckCustomer(txtCustomer.Text.Trim().ToUpper()))
                {
                    txtCustomer.Text = ""; txtCustomer.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a valid customer code')", true); return;
                }
                txtCustomer.ToolTip = CHNLSVC.General.Get_Customer_desc(txtCustomer.Text.Trim().ToUpper());
            }
            if (!String.IsNullOrEmpty(txtComDate.Text))
            {
                try
                {
                    DateTime d = Convert.ToDateTime(txtComDate.Text);
                    //if (i < 0)
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('The change period is invalid')", true); return;
                    //}
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('The warranty com date is invalid')", true); return;
                }
            }
            _request = (List<WarrantyAmendRequest>)Session["_request"];
            foreach (GridViewRow row in dgvItemDetails.Rows)
            {
                CheckBox chkSelectItem = (CheckBox)row.FindControl("chkSelectItem");
                if (chkSelectItem.Checked)
                {
                    WarrantyAmendRequest req = new WarrantyAmendRequest();
                    req.Rwr_req_by = Session["UserID"].ToString();
                    req.Rwr_req_dt = DateTime.Now;
                    req.Rwr_req_session = Session["SessionID"].ToString();
                    Label lblSerial = (Label)row.FindControl("lblSerial");
                    Label lblSerialId = (Label)row.FindControl("lblSerialId");
                    Label lblWarrantyStartDate = (Label)row.FindControl("lblWarrantyStartDate");
                    req.Rwr_ser_id = Convert.ToInt32(lblSerialId.Text);
                    req.Rwr_stus = "P";
                    req.Rwr_warr_period = ConvertToInt(txtChangePeriod.Text);
                    req.Rwr_warr_rmk = txtRemarksPeriod.Text;
                    req.Rwr_st_dt = Convert.ToDateTime(txtComDate.Text);
                    req.Rwr_cust_cd = txtCustomer.Text;
                    var v = _request.Where(c => c.Rwr_ser_id == req.Rwr_ser_id && c.Rwr_stus == req.Rwr_stus).SingleOrDefault();
                    if (v!=null)
                    {
                        _request.Remove(v);
                    }
                    _request.Add(req);
                }
            }
            Session["_request"] = _request;
            txtChangePeriod.Text = "";
            txtRemarksPeriod.Text = "";
            txtCustomer.Text = "";
            txtCusDetails.Text = "";
            txtComDate.Text = "";
            DataTable reqDt = new DataTable();
            DataRow reqRw = reqDt.NewRow();
            reqDt.Columns.Add("rwr_req_by", typeof(String));
            reqDt.Columns.Add("rwr_req_dt", typeof(DateTime));
            reqDt.Columns.Add("rwr_req_session", typeof(String));
            reqDt.Columns.Add("rwr_ser_id", typeof(String));
            reqDt.Columns.Add("rwr_stus", typeof(String));
            reqDt.Columns.Add("rwr_warr_period", typeof(Int32));
            reqDt.Columns.Add("rwr_warr_rmk", typeof(String));
            reqDt.Columns.Add("rwr_st_dt", typeof(DateTime));
            reqDt.Columns.Add("rwr_cust_cd", typeof(String));
            reqDt.Columns.Add("item_code", typeof(String));
            reqDt.Columns.Add("desc", typeof(String));
            reqDt.Columns.Add("model", typeof(String));
            reqDt.Columns.Add("cur_warr_period", typeof(Int32));
            reqDt.Columns.Add("serialNo", typeof(String));
            foreach (var v in _request)
            {
                reqRw = reqDt.NewRow();
                reqRw["Rwr_req_by"] = v.Rwr_req_by;
                reqRw["rwr_req_dt"] = v.Rwr_req_dt;
                reqRw["rwr_req_session"] = v.Rwr_req_session;
                reqRw["rwr_ser_id"] = v.Rwr_ser_id;
                reqRw["rwr_stus"] = v.Rwr_stus;
                reqRw["rwr_warr_period"] = v.Rwr_warr_period;
                reqRw["rwr_warr_rmk"] = v.Rwr_warr_rmk;
                reqRw["rwr_st_dt"] = v.Rwr_st_dt;
                reqRw["rwr_cust_cd"] = v.Rwr_cust_cd;
                InventorySerialMaster SerMstCost = CHNLSVC.Inventory.GetSerialMasterDetailBySerialID(v.Rwr_ser_id);
                reqRw["item_code"] = SerMstCost.Irsm_itm_cd;
                reqRw["desc"] = SerMstCost.Irsm_itm_desc;
                reqRw["model"] = SerMstCost.Irsm_itm_model;
                reqRw["cur_warr_period"] = SerMstCost.Irsm_warr_period;
                reqRw["serialNo"] = SerMstCost.Irsm_ser_1;
                reqDt.Rows.Add(reqRw);
            }
            dgvChanges.DataSource = reqDt;
            dgvChanges.DataBind();
           
        }

        private int ConvertToInt(string s)
        {
            int i = 0;
            try
            {
                Int32.TryParse(s, out i);
                return i;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        protected void chkSelectItem_CheckedChanged(object sender, EventArgs e)
        {
            txtChangePeriod.Text = "";
            txtRemarksPeriod.Text = "";
            txtCustomer.Text = "";
            txtCusDetails.Text = "";
            txtComDate.Text = "";
            var lb = (CheckBox)sender;
            var row = (GridViewRow)lb.NamingContainer;
            CheckBox chkSelectItem = (CheckBox)row.FindControl("chkSelectItem");
            if (chkSelectItem.Checked)
            {
                if (row != null)
                {
                    Label lblWarrantyPeriod = (Label)row.FindControl("lblWarrantyPeriod");
                    Label lblWarrantyRemarks = (Label)row.FindControl("lblWarrantyRemarks");
                    Label lblCustomerCode = (Label)row.FindControl("lblCustomerCode");
                    Label lblCustomer = (Label)row.FindControl("lblCustomer");
                    Label lblWarrantyStartDate = (Label)row.FindControl("lblWarrantyStartDate");
                    txtChangePeriod.Text = lblWarrantyPeriod.Text;
                    txtRemarksPeriod.Text = lblWarrantyRemarks.Text;
                    txtCustomer.Text = lblCustomerCode.Text;
                    txtCusDetails.Text = lblCustomer.Text;
                    txtComDate.Text =Convert.ToDateTime(lblWarrantyStartDate.Text).Date.ToString("dd/MMM/yyyy");
                }
            }
        }

        protected void lbtnRequest_Click(object sender, EventArgs e)
        {
            if (hdfSave.Value == "0")
            { return; }
            bool b10125 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10125);
            bool b10126 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10126);
            if (!b10125 && !b10126)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You don't have the permission.\nPermission Code :- 10125/10126')", true); return;
            }
            
           _request=(List<WarrantyAmendRequest>)Session["_request"];
           if (_request.Count<1)
           {
               ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please add amendments to request ')", true); return;
           }
           string err = "";
           Int32 _effect = 0;_effect= CHNLSVC.General.SaveWarrAmendReq(_request,out err);
           if (_effect >0)
           {
               dgvItemDetails.DataSource = new int[] { };
               dgvItemDetails.DataBind();
               dgvChanges.DataSource = new int[] { };
               dgvChanges.DataBind();
               dgvReqData.DataSource = new int[] { };
               dgvReqData.DataBind();
               _request = new List<WarrantyAmendRequest>();
               Session["_request"] = _request;
               ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Warranty amend request saved successfully !!!')", true);
           }
           if (err != "")
           {
               ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true); return;
           }
           panel1.Visible = true;
           panel2.Visible = false;
           chkReq.Checked = false;
        }

        protected void lbtnApprove_Click(object sender, EventArgs e)
        {
            if (hdfApprove.Value == "0")
            { return; }
            bool b10126 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10126);
            if (!b10126)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You don't have the permission.\nPermission Code :- 10126')", true); return;
            }
            if (!chkReq.Checked && dgvChanges.Rows.Count<1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No amendments made for approval')", true); return;
            }
            if (chkReq.Checked && dgvReqData.Rows.Count < 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('No amendments made for approval')", true); return;
            }
            if (chkReq.Checked)
            {
                #region approve without req
                bool selected = false;
                foreach (GridViewRow row in dgvReqData.Rows)
                {
                    CheckBox chkSelectReq = (CheckBox)row.FindControl("chkSelectReq");
                    if (chkSelectReq.Checked)
                    { selected = true; break; }
                }
                if (!selected)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a item ')", true); return;
                }
                List<WarrantyAmendRequest> warrantyAmendRequest = new List<WarrantyAmendRequest>();
                foreach (GridViewRow row in dgvReqData.Rows)
                {
                    CheckBox chkSelectReq = (CheckBox)row.FindControl("chkSelectReq");
                    if (chkSelectReq.Checked)
                    {
                        WarrantyAmendRequest updateReq = new WarrantyAmendRequest();
                        Label lblSeqNo = (Label)row.FindControl("lblSeqNo");
                        updateReq.Rwr_seq = Convert.ToInt32(lblSeqNo.Text);
                        updateReq.Rwr_stus = "A";
                        updateReq.Rwr_app_by = Session["UserID"].ToString();
                        updateReq.Rwr_app_dt = DateTime.Now;
                        updateReq.Rwr_app_session = Session["SessionID"].ToString();
                        warrantyAmendRequest.Add(updateReq);
                    }
                }
                #region serial master
                serialMasterLogs = new List<SerialMasterLog>();
                warrantyDetails = new List<InventoryWarrantyDetail>();
                foreach (var item in warrantyAmendRequest)
                {
                    inventorySerialMaster = CHNLSVC.Inventory.GetSerialMasterDetailBySerialID(item.Rwr_ser_id);
                    amendRequest = CHNLSVC.General.GetWarrantyAmendReqData(item);
                    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(amendRequest.Rwr_cust_cd, null, null, null, null, Session["UserCompanyCode"].ToString());
                    serialMasterLog = new SerialMasterLog()
                    {
                        Irsm_log_by = Session["UserID"].ToString(),
                        Irsm_log_session = Session["SessionID"].ToString(),
                        Irsm_ser_id = amendRequest.Rwr_ser_id,
                        Irsm_com = Session["UserCompanyCode"].ToString(),
                        Irsm_sbu = inventorySerialMaster.Irsm_sbu,
                        Irsm_channel = inventorySerialMaster.Irsm_channel,
                        Irsm_loc = inventorySerialMaster.Irsm_loc,
                        Irsm_doc_no = inventorySerialMaster.Irsm_doc_no,
                        Irsm_doc_dt = inventorySerialMaster.Irsm_doc_dt,
                        Irsm_invoice_no = inventorySerialMaster.Irsm_invoice_no,
                        Irsm_invoice_dt = inventorySerialMaster.Irsm_invoice_dt,
                        Irsm_acc_no = inventorySerialMaster.Irsm_acc_no,
                        Irsm_doc_year = inventorySerialMaster.Irsm_doc_year,
                        Irsm_direct = inventorySerialMaster.Irsm_direct,
                        Irsm_itm_cd = inventorySerialMaster.Irsm_itm_cd,
                        Irsm_itm_brand = inventorySerialMaster.Irsm_itm_brand,
                        Irsm_itm_model = inventorySerialMaster.Irsm_itm_model,
                        Irsm_itm_desc = inventorySerialMaster.Irsm_itm_desc,
                        Irsm_itm_stus = inventorySerialMaster.Irsm_warr_stus,
                        Irsm_ser_1 = inventorySerialMaster.Irsm_ser_1,
                        Irsm_ser_2 = inventorySerialMaster.Irsm_ser_2,
                        Irsm_ser_3 = inventorySerialMaster.Irsm_ser_3,
                        Irsm_ser_4 = inventorySerialMaster.Irsm_ser_4,
                        Irsm_warr_no = inventorySerialMaster.Irsm_warr_no,
                        Irsm_mfc = inventorySerialMaster.Irsm_mfc,
                        Irsm_unit_cost = inventorySerialMaster.Irsm_unit_cost,
                        Irsm_unit_price = inventorySerialMaster.Irsm_unit_price,
                        Irsm_warr_start_dt = amendRequest.Rwr_st_dt,
                        Irsm_warr_period = amendRequest.Rwr_warr_period,
                        Irsm_warr_rem = amendRequest.Rwr_warr_rmk,
                        Irsm_cust_cd = amendRequest.Rwr_cust_cd,
                        Irsm_cust_prefix = inventorySerialMaster.Irsm_cust_prefix,
                        Irsm_cust_name = inventorySerialMaster.Irsm_cust_name,
                        Irsm_cust_addr = inventorySerialMaster.Irsm_cust_addr,
                        Irsm_cust_del_addr = inventorySerialMaster.Irsm_cust_del_addr,
                        Irsm_cust_town = inventorySerialMaster.Irsm_cust_town,
                        Irsm_cust_tel = inventorySerialMaster.Irsm_cust_tel,
                        Irsm_cust_mobile = inventorySerialMaster.Irsm_cust_mobile,
                        Irsm_cust_fax = inventorySerialMaster.Irsm_cust_fax,
                        Irsm_cust_email = inventorySerialMaster.Irsm_cust_email,
                        Irsm_cust_vat_no = inventorySerialMaster.Irsm_cust_vat_no,
                        Irsm_orig_grn_com = inventorySerialMaster.Irsm_orig_grn_com,
                        Irsm_orig_grn_no = inventorySerialMaster.Irsm_orig_grn_no,
                        Irsm_orig_grn_dt = inventorySerialMaster.Irsm_orig_grn_dt,
                        Irsm_orig_supp = inventorySerialMaster.Irsm_orig_supp,
                        Irsm_exist_grn_com = inventorySerialMaster.Irsm_exist_grn_com,
                        Irsm_exist_grn_no = inventorySerialMaster.Irsm_exist_grn_no,
                        Irsm_exist_grn_dt = inventorySerialMaster.Irsm_exist_grn_dt,
                        Irsm_exist_supp = inventorySerialMaster.Irsm_exist_supp,
                        Irsm_warr_stus = inventorySerialMaster.Irsm_warr_stus,
                        Irsm_cre_by = Session["UserID"].ToString(),
                        Irsm_cre_when = DateTime.Now,
                        Irsm_session_id = Session["SessionID"].ToString(),
                        Irsm_anal_1 = inventorySerialMaster.Irsm_anal_1,
                        Irsm_anal_2 = inventorySerialMaster.Irsm_anal_2,
                        Irsm_anal_3 = inventorySerialMaster.Irsm_anal_3,
                        Irsm_anal_4 = inventorySerialMaster.Irsm_anal_4,
                        Irsm_anal_5 = inventorySerialMaster.Irsm_anal_5,
                        Irsm_rec_com = inventorySerialMaster.Irsm_rec_com,
                        Irsm_reg_no = inventorySerialMaster.Irsm_reg_no,
                        Irsm_sup_warr_pd = inventorySerialMaster.Irsm_sup_warr_pd,
                        Irsm_sup_warr_rem = inventorySerialMaster.Irsm_sup_warr_rem,
                        Irsm_sup_warr_stdt = inventorySerialMaster.Irsm_sup_warr_stdt,
                        Irsm_add_warr_pd = amendRequest.Rwr_warr_period,
                        Irsm_add_warr_rem = amendRequest.Rwr_warr_rmk,
                        Irsm_add_warr_stdt = amendRequest.Rwr_st_dt
                    };
                    serialMasterLogs.Add(serialMasterLog);
                    #endregion
                    /*Warranty Data*/
                    warrantyDetail = new InventoryWarrantyDetail()
                    {
                        Irsm_ser_id = amendRequest.Rwr_ser_id,
                        Irsm_warr_start_dt = amendRequest.Rwr_st_dt,
                        Irsm_warr_period = amendRequest.Rwr_warr_period,
                        Irsm_warr_rem = amendRequest.Rwr_warr_rmk,
                        Irsm_cust_cd = amendRequest.Rwr_cust_cd,
                        Irsm_cust_del_addr = amendRequest.Rwr_cust_cd,
                        Irsm_cust_email = _masterBusinessCompany.Mbe_email,
                        Irsm_cust_fax = _masterBusinessCompany.Mbe_fax,
                        Irsm_cust_mobile = _masterBusinessCompany.Mbe_mob,
                        Irsm_cust_name = _masterBusinessCompany.Mbe_name,
                        //  Irsm_cust_prefix = req.,
                        Irsm_cust_tel = _masterBusinessCompany.Mbe_tel,
                        Irsm_cust_town = _masterBusinessCompany.Mbe_town_cd,
                        // Irsm_cust_vat_no = req.
                        Irsm_cust_addr = _masterBusinessCompany.Mbe_add1,
                    };
                    warrantyDetails.Add(warrantyDetail);
                }
                Int32 _effect = 0;
                string err = "";
                if (warrantyAmendRequest.Count > 0)
                {
                    _effect = CHNLSVC.General.UpdateWarrAmendReq(warrantyAmendRequest, out err);
                    //_effect = CHNLSVC.General.SaveSerialMasterLog(serialMasterLogs, out err);
                    //_effect = CHNLSVC.General.UpdateWarrantyMasterAmend(warrantyDetails, out err);
                    _effect = CHNLSVC.General.SaveUpdateWarrentyAmend(serialMasterLogs, warrantyDetails, out err);
                }
                if (_effect > 0)
                {
                    dgvItemDetails.DataSource = new int[] { };
                    dgvItemDetails.DataBind();
                    dgvChanges.DataSource = new int[] { };
                    dgvChanges.DataBind();
                    dgvReqData.DataSource = new int[] { };
                    dgvReqData.DataBind();
                    _request = new List<WarrantyAmendRequest>();
                    Session["_request"] = _request;
                    panel1.Visible = true;
                    panel2.Visible = false;
                    chkReq.Checked = false;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Warranty amend approved successfully !!!')", true);
                }
                if (err != "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true); return;
                }
                #endregion
            }
            else
            {
                #region approve with req
                _request = (List<WarrantyAmendRequest>)Session["_request"];
                serialMasterLogs = new List<SerialMasterLog>();
                warrantyDetails=new List<InventoryWarrantyDetail>();
                foreach (WarrantyAmendRequest req in _request)
                {
                    InventorySerialMaster SerMstCost = CHNLSVC.Inventory.GetSerialMasterDetailBySerialID(req.Rwr_ser_id);
                    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(req.Rwr_cust_cd, null, null, null, null, Session["UserCompanyCode"].ToString());
                    serialMasterLog = new SerialMasterLog()
                    {
                        Irsm_log_by = Session["UserID"].ToString(),
                        Irsm_log_session = Session["SessionID"].ToString(),
                        Irsm_ser_id = req.Rwr_ser_id,
                        Irsm_com = Session["UserCompanyCode"].ToString(),
                        Irsm_sbu = SerMstCost.Irsm_sbu,
                        Irsm_channel = SerMstCost.Irsm_channel,
                        Irsm_loc = SerMstCost.Irsm_loc,
                        Irsm_doc_no = SerMstCost.Irsm_doc_no,
                        Irsm_doc_dt = SerMstCost.Irsm_doc_dt,
                        Irsm_invoice_no = SerMstCost.Irsm_invoice_no,
                        Irsm_invoice_dt = SerMstCost.Irsm_invoice_dt,
                        Irsm_acc_no = SerMstCost.Irsm_acc_no,
                        Irsm_doc_year = SerMstCost.Irsm_doc_year,
                        Irsm_direct = SerMstCost.Irsm_direct,
                        Irsm_itm_cd = SerMstCost.Irsm_itm_cd,
                        Irsm_itm_brand = SerMstCost.Irsm_itm_brand,
                        Irsm_itm_model = SerMstCost.Irsm_itm_model,
                        Irsm_itm_desc = SerMstCost.Irsm_itm_desc,
                        Irsm_itm_stus = SerMstCost.Irsm_warr_stus,
                        Irsm_ser_1 = SerMstCost.Irsm_ser_1,
                        Irsm_ser_2 = SerMstCost.Irsm_ser_2,
                        Irsm_ser_3 = SerMstCost.Irsm_ser_3,
                        Irsm_ser_4 = SerMstCost.Irsm_ser_4,
                        Irsm_warr_no = SerMstCost.Irsm_warr_no,
                        Irsm_mfc = SerMstCost.Irsm_mfc,
                        Irsm_unit_cost = SerMstCost.Irsm_unit_cost,
                        Irsm_unit_price = SerMstCost.Irsm_unit_price,
                        Irsm_warr_start_dt = req.Rwr_st_dt,
                        Irsm_warr_period = req.Rwr_warr_period,
                        Irsm_warr_rem = req.Rwr_warr_rmk,
                        Irsm_cust_cd = req.Rwr_cust_cd,
                        Irsm_cust_prefix = SerMstCost.Irsm_cust_prefix,
                        Irsm_cust_name = SerMstCost.Irsm_cust_name,
                        Irsm_cust_addr = SerMstCost.Irsm_cust_addr,
                        Irsm_cust_del_addr = SerMstCost.Irsm_cust_del_addr,
                        Irsm_cust_town = SerMstCost.Irsm_cust_town,
                        Irsm_cust_tel = SerMstCost.Irsm_cust_tel,
                        Irsm_cust_mobile = SerMstCost.Irsm_cust_mobile,
                        Irsm_cust_fax = SerMstCost.Irsm_cust_fax,
                        Irsm_cust_email = SerMstCost.Irsm_cust_email,
                        Irsm_cust_vat_no = SerMstCost.Irsm_cust_vat_no,
                        Irsm_orig_grn_com = SerMstCost.Irsm_orig_grn_com,
                        Irsm_orig_grn_no = SerMstCost.Irsm_orig_grn_no,
                        Irsm_orig_grn_dt = SerMstCost.Irsm_orig_grn_dt,
                        Irsm_orig_supp = SerMstCost.Irsm_orig_supp,
                        Irsm_exist_grn_com = SerMstCost.Irsm_exist_grn_com,
                        Irsm_exist_grn_no = SerMstCost.Irsm_exist_grn_no,
                        Irsm_exist_grn_dt = SerMstCost.Irsm_exist_grn_dt,
                        Irsm_exist_supp = SerMstCost.Irsm_exist_supp,
                        Irsm_warr_stus = SerMstCost.Irsm_warr_stus,
                        Irsm_cre_by = Session["UserID"].ToString(),
                        Irsm_cre_when = DateTime.Now,
                        Irsm_session_id = Session["SessionID"].ToString(),
                        Irsm_anal_1 = SerMstCost.Irsm_anal_1,
                        Irsm_anal_2 = SerMstCost.Irsm_anal_2,
                        Irsm_anal_3 = SerMstCost.Irsm_anal_3,
                        Irsm_anal_4 = SerMstCost.Irsm_anal_4,
                        Irsm_anal_5 = SerMstCost.Irsm_anal_5,
                        Irsm_rec_com = SerMstCost.Irsm_rec_com,
                        Irsm_reg_no = SerMstCost.Irsm_reg_no,
                        Irsm_sup_warr_pd = SerMstCost.Irsm_sup_warr_pd,
                        Irsm_sup_warr_rem = SerMstCost.Irsm_sup_warr_rem,
                        Irsm_sup_warr_stdt = SerMstCost.Irsm_sup_warr_stdt,
                        Irsm_add_warr_pd = req.Rwr_warr_period,
                        Irsm_add_warr_rem = req.Rwr_warr_rmk,
                        Irsm_add_warr_stdt = req.Rwr_st_dt
                    };
                    serialMasterLogs.Add(serialMasterLog);

                    warrantyDetail = new InventoryWarrantyDetail()
                    {
                        Irsm_ser_id = req.Rwr_ser_id,
                        Irsm_warr_start_dt = req.Rwr_st_dt,
                        Irsm_warr_period = req.Rwr_warr_period,
                        Irsm_warr_rem = req.Rwr_warr_rmk,
                        Irsm_cust_cd = req.Rwr_cust_cd,
                        Irsm_cust_del_addr = req.Rwr_cust_cd,
                        Irsm_cust_email = _masterBusinessCompany.Mbe_email,
                        Irsm_cust_fax = _masterBusinessCompany.Mbe_fax,
                        Irsm_cust_mobile = _masterBusinessCompany.Mbe_mob,
                        Irsm_cust_name = _masterBusinessCompany.Mbe_name,
                        //  Irsm_cust_prefix = req.,
                        Irsm_cust_tel = _masterBusinessCompany.Mbe_tel,
                        Irsm_cust_town = _masterBusinessCompany.Mbe_town_cd,
                        // Irsm_cust_vat_no = req.
                        Irsm_cust_addr = _masterBusinessCompany.Mbe_add1,
                    };
                    warrantyDetails.Add(warrantyDetail);
                    Int32 _effect = 0;
                    string err = "";

                   // _effect = CHNLSVC.General.SaveSerialMasterLog(serialMasterLogs, out err);
                  //  _effect = CHNLSVC.General.UpdateWarrantyMasterAmend(warrantyDetails, out err);
                    _effect = CHNLSVC.General.SaveUpdateWarrentyAmend(serialMasterLogs, warrantyDetails,out err);

                    if (_effect > 0)
                    {
                        dgvItemDetails.DataSource = new int[] { };
                        dgvItemDetails.DataBind();
                        dgvChanges.DataSource = new int[] { };
                        dgvChanges.DataBind();
                        dgvReqData.DataSource = new int[] { };
                        dgvReqData.DataBind();
                        _request = new List<WarrantyAmendRequest>();
                        Session["_request"] = _request;
                        panel1.Visible = false;
                        panel2.Visible = true;
                        chkReq.Checked = false;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Warranty amend approved successfully !!!')", true);
                       // panel1.Visible = true;
                        panel2.Visible = true;
                        chkReq.Checked = true;
                        //chkReq.Checked = true;
                       // chkReq_CheckedChanged(null, null);
                        //bool b10126 = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10126);
                        if (b10126 && chkReq.Checked)
                        {
                            lbtnReject.Enabled = true;
                            lbtnReject.OnClientClick = "RejectConfirm()";
                            lbtnReject.CssClass = "buttonUndocolor";
                        }
                        else
                        {
                            lbtnReject.Enabled = false;
                            lbtnReject.OnClientClick = "";
                            lbtnReject.CssClass = "buttoncolor";
                        }
                    }
                    if (err != "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true); return;
                    }
                }
                #endregion
            }
        }

        protected void lbtnReject_Click(object sender, EventArgs e)
        {
            if (hdfReject.Value == "0")
            { return; }
            //if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10125))
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You don't have the permission.\nPermission Code :- 10125')", true); return;
            //}
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10126))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You don't have the permission.\nPermission Code :- 10126')", true); return;
            }
            if (chkReq.Checked)
            {
                #region approve without req
                bool selected = false;
                if (dgvReqData.Rows.Count<1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please search a warranty amend request !')", true); return;
                }
                foreach (GridViewRow row in dgvReqData.Rows)
                {
                    CheckBox chkSelectReq = (CheckBox)row.FindControl("chkSelectReq");
                    if (chkSelectReq.Checked)
                    { selected = true; break; }
                }
                if (!selected)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select a warranty amend request !')", true); return;
                }
                List<WarrantyAmendRequest> warrantyAmendRequest = new List<WarrantyAmendRequest>();
                foreach (GridViewRow row in dgvReqData.Rows)
                {
                    CheckBox chkSelectReq = (CheckBox)row.FindControl("chkSelectReq");
                    if (chkSelectReq.Checked)
                    {
                        WarrantyAmendRequest updateReq = new WarrantyAmendRequest();
                        Label lblSeqNo = (Label)row.FindControl("lblSeqNo");
                        updateReq.Rwr_seq = Convert.ToInt32(lblSeqNo.Text);
                        updateReq.Rwr_stus = "R";
                        updateReq.Rwr_app_by = Session["UserID"].ToString();
                        updateReq.Rwr_app_dt = DateTime.Now;
                        updateReq.Rwr_app_session = Session["SessionID"].ToString();
                        warrantyAmendRequest.Add(updateReq);
                    }
                }
                #region serial master
                serialMasterLogs = new List<SerialMasterLog>();
                warrantyDetails = new List<InventoryWarrantyDetail>();
                foreach (var item in warrantyAmendRequest)
                {
                    inventorySerialMaster = CHNLSVC.Inventory.GetSerialMasterDetailBySerialID(item.Rwr_ser_id);
                    amendRequest = CHNLSVC.General.GetWarrantyAmendReqData(item);
                    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(amendRequest.Rwr_cust_cd, null, null, null, null, Session["UserCompanyCode"].ToString());
                    serialMasterLog = new SerialMasterLog()
                    {
                        Irsm_log_by = Session["UserID"].ToString(),
                        Irsm_log_session = Session["SessionID"].ToString(),
                        Irsm_ser_id = amendRequest.Rwr_ser_id,
                        Irsm_com = Session["UserCompanyCode"].ToString(),
                        Irsm_sbu = inventorySerialMaster.Irsm_sbu,
                        Irsm_channel = inventorySerialMaster.Irsm_channel,
                        Irsm_loc = inventorySerialMaster.Irsm_loc,
                        Irsm_doc_no = inventorySerialMaster.Irsm_doc_no,
                        Irsm_doc_dt = inventorySerialMaster.Irsm_doc_dt,
                        Irsm_invoice_no = inventorySerialMaster.Irsm_invoice_no,
                        Irsm_invoice_dt = inventorySerialMaster.Irsm_invoice_dt,
                        Irsm_acc_no = inventorySerialMaster.Irsm_acc_no,
                        Irsm_doc_year = inventorySerialMaster.Irsm_doc_year,
                        Irsm_direct = inventorySerialMaster.Irsm_direct,
                        Irsm_itm_cd = inventorySerialMaster.Irsm_itm_cd,
                        Irsm_itm_brand = inventorySerialMaster.Irsm_itm_brand,
                        Irsm_itm_model = inventorySerialMaster.Irsm_itm_model,
                        Irsm_itm_desc = inventorySerialMaster.Irsm_itm_desc,
                        Irsm_itm_stus = inventorySerialMaster.Irsm_warr_stus,
                        Irsm_ser_1 = inventorySerialMaster.Irsm_ser_1,
                        Irsm_ser_2 = inventorySerialMaster.Irsm_ser_2,
                        Irsm_ser_3 = inventorySerialMaster.Irsm_ser_3,
                        Irsm_ser_4 = inventorySerialMaster.Irsm_ser_4,
                        Irsm_warr_no = inventorySerialMaster.Irsm_warr_no,
                        Irsm_mfc = inventorySerialMaster.Irsm_mfc,
                        Irsm_unit_cost = inventorySerialMaster.Irsm_unit_cost,
                        Irsm_unit_price = inventorySerialMaster.Irsm_unit_price,
                        Irsm_warr_start_dt = amendRequest.Rwr_st_dt,
                        Irsm_warr_period = amendRequest.Rwr_warr_period,
                        Irsm_warr_rem = amendRequest.Rwr_warr_rmk,
                        Irsm_cust_cd = amendRequest.Rwr_cust_cd,
                        Irsm_cust_prefix = inventorySerialMaster.Irsm_cust_prefix,
                        Irsm_cust_name = inventorySerialMaster.Irsm_cust_name,
                        Irsm_cust_addr = inventorySerialMaster.Irsm_cust_addr,
                        Irsm_cust_del_addr = inventorySerialMaster.Irsm_cust_del_addr,
                        Irsm_cust_town = inventorySerialMaster.Irsm_cust_town,
                        Irsm_cust_tel = inventorySerialMaster.Irsm_cust_tel,
                        Irsm_cust_mobile = inventorySerialMaster.Irsm_cust_mobile,
                        Irsm_cust_fax = inventorySerialMaster.Irsm_cust_fax,
                        Irsm_cust_email = inventorySerialMaster.Irsm_cust_email,
                        Irsm_cust_vat_no = inventorySerialMaster.Irsm_cust_vat_no,
                        Irsm_orig_grn_com = inventorySerialMaster.Irsm_orig_grn_com,
                        Irsm_orig_grn_no = inventorySerialMaster.Irsm_orig_grn_no,
                        Irsm_orig_grn_dt = inventorySerialMaster.Irsm_orig_grn_dt,
                        Irsm_orig_supp = inventorySerialMaster.Irsm_orig_supp,
                        Irsm_exist_grn_com = inventorySerialMaster.Irsm_exist_grn_com,
                        Irsm_exist_grn_no = inventorySerialMaster.Irsm_exist_grn_no,
                        Irsm_exist_grn_dt = inventorySerialMaster.Irsm_exist_grn_dt,
                        Irsm_exist_supp = inventorySerialMaster.Irsm_exist_supp,
                        Irsm_warr_stus = inventorySerialMaster.Irsm_warr_stus,
                        Irsm_cre_by = Session["UserID"].ToString(),
                        Irsm_cre_when = DateTime.Now,
                        Irsm_session_id = Session["SessionID"].ToString(),
                        Irsm_anal_1 = inventorySerialMaster.Irsm_anal_1,
                        Irsm_anal_2 = inventorySerialMaster.Irsm_anal_2,
                        Irsm_anal_3 = inventorySerialMaster.Irsm_anal_3,
                        Irsm_anal_4 = inventorySerialMaster.Irsm_anal_4,
                        Irsm_anal_5 = inventorySerialMaster.Irsm_anal_5,
                        Irsm_rec_com = inventorySerialMaster.Irsm_rec_com,
                        Irsm_reg_no = inventorySerialMaster.Irsm_reg_no,
                        Irsm_sup_warr_pd = inventorySerialMaster.Irsm_sup_warr_pd,
                        Irsm_sup_warr_rem = inventorySerialMaster.Irsm_sup_warr_rem,
                        Irsm_sup_warr_stdt = inventorySerialMaster.Irsm_sup_warr_stdt,
                        Irsm_add_warr_pd = amendRequest.Rwr_warr_period,
                        Irsm_add_warr_rem = amendRequest.Rwr_warr_rmk,
                        Irsm_add_warr_stdt = amendRequest.Rwr_st_dt
                    };
                    serialMasterLogs.Add(serialMasterLog);
                #endregion
                    /*Warranty Data*/
                    warrantyDetail = new InventoryWarrantyDetail()
                    {
                        Irsm_ser_id = amendRequest.Rwr_ser_id,
                        Irsm_warr_start_dt = amendRequest.Rwr_st_dt,
                        Irsm_warr_period = amendRequest.Rwr_warr_period,
                        Irsm_warr_rem = amendRequest.Rwr_warr_rmk,
                        Irsm_cust_cd = amendRequest.Rwr_cust_cd,
                        Irsm_cust_del_addr = amendRequest.Rwr_cust_cd,
                        Irsm_cust_email = _masterBusinessCompany.Mbe_email,
                        Irsm_cust_fax = _masterBusinessCompany.Mbe_fax,
                        Irsm_cust_mobile = _masterBusinessCompany.Mbe_mob,
                        Irsm_cust_name = _masterBusinessCompany.Mbe_name,
                        //  Irsm_cust_prefix = req.,
                        Irsm_cust_tel = _masterBusinessCompany.Mbe_tel,
                        Irsm_cust_town = _masterBusinessCompany.Mbe_town_cd,
                        // Irsm_cust_vat_no = req.
                        Irsm_cust_addr = _masterBusinessCompany.Mbe_add1,
                    };
                    warrantyDetails.Add(warrantyDetail);
                }
                Int32 _effect = 0;
                string err = "";
                if (warrantyAmendRequest.Count > 0)
                {
                    _effect = CHNLSVC.General.UpdateWarrAmendReq(warrantyAmendRequest, out err);
                    //_effect = CHNLSVC.General.SaveSerialMasterLog(serialMasterLogs, out err);
                    //_effect = CHNLSVC.General.UpdateWarrantyMasterAmend(warrantyDetails, out err);
                }
                if (_effect > 0)
                {
                    dgvItemDetails.DataSource = new int[] { };
                    dgvItemDetails.DataBind();
                    dgvChanges.DataSource = new int[] { };
                    dgvChanges.DataBind();
                    dgvReqData.DataSource = new int[] { };
                    dgvReqData.DataBind();
                    _request = new List<WarrantyAmendRequest>();
                    Session["_request"] = _request;
                    panel1.Visible = true;
                    panel2.Visible = false;
                    chkReq.Checked = false;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Warranty amend request rejected successfully !!!')", true);
                }
                if (err != "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true); return;
                }
                #endregion
            }
            else
            {
                //#region approve with req
                //_request = (List<WarrantyAmendRequest>)Session["_request"];
                //serialMasterLogs = new List<SerialMasterLog>();
                //warrantyDetails = new List<InventoryWarrantyDetail>();
                //foreach (WarrantyAmendRequest req in _request)
                //{
                //    InventorySerialMaster SerMstCost = CHNLSVC.Inventory.GetSerialMasterDetailBySerialID(req.Rwr_ser_id);
                //    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(req.Rwr_cust_cd, null, null, null, null, Session["UserCompanyCode"].ToString());
                //    serialMasterLog = new SerialMasterLog()
                //    {
                //        Irsm_log_by = Session["UserID"].ToString(),
                //        Irsm_log_session = Session["SessionID"].ToString(),
                //        Irsm_ser_id = req.Rwr_ser_id,
                //        Irsm_com = Session["UserCompanyCode"].ToString(),
                //        Irsm_sbu = SerMstCost.Irsm_sbu,
                //        Irsm_channel = SerMstCost.Irsm_channel,
                //        Irsm_loc = SerMstCost.Irsm_loc,
                //        Irsm_doc_no = SerMstCost.Irsm_doc_no,
                //        Irsm_doc_dt = SerMstCost.Irsm_doc_dt,
                //        Irsm_invoice_no = SerMstCost.Irsm_invoice_no,
                //        Irsm_invoice_dt = SerMstCost.Irsm_invoice_dt,
                //        Irsm_acc_no = SerMstCost.Irsm_acc_no,
                //        Irsm_doc_year = SerMstCost.Irsm_doc_year,
                //        Irsm_direct = SerMstCost.Irsm_direct,
                //        Irsm_itm_cd = SerMstCost.Irsm_itm_cd,
                //        Irsm_itm_brand = SerMstCost.Irsm_itm_brand,
                //        Irsm_itm_model = SerMstCost.Irsm_itm_model,
                //        Irsm_itm_desc = SerMstCost.Irsm_itm_desc,
                //        Irsm_itm_stus = SerMstCost.Irsm_warr_stus,
                //        Irsm_ser_1 = SerMstCost.Irsm_ser_1,
                //        Irsm_ser_2 = SerMstCost.Irsm_ser_2,
                //        Irsm_ser_3 = SerMstCost.Irsm_ser_3,
                //        Irsm_ser_4 = SerMstCost.Irsm_ser_4,
                //        Irsm_warr_no = SerMstCost.Irsm_warr_no,
                //        Irsm_mfc = SerMstCost.Irsm_mfc,
                //        Irsm_unit_cost = SerMstCost.Irsm_unit_cost,
                //        Irsm_unit_price = SerMstCost.Irsm_unit_price,
                //        Irsm_warr_start_dt = req.Rwr_st_dt,
                //        Irsm_warr_period = req.Rwr_warr_period,
                //        Irsm_warr_rem = req.Rwr_warr_rmk,
                //        Irsm_cust_cd = req.Rwr_cust_cd,
                //        Irsm_cust_prefix = SerMstCost.Irsm_cust_prefix,
                //        Irsm_cust_name = SerMstCost.Irsm_cust_name,
                //        Irsm_cust_addr = SerMstCost.Irsm_cust_addr,
                //        Irsm_cust_del_addr = SerMstCost.Irsm_cust_del_addr,
                //        Irsm_cust_town = SerMstCost.Irsm_cust_town,
                //        Irsm_cust_tel = SerMstCost.Irsm_cust_tel,
                //        Irsm_cust_mobile = SerMstCost.Irsm_cust_mobile,
                //        Irsm_cust_fax = SerMstCost.Irsm_cust_fax,
                //        Irsm_cust_email = SerMstCost.Irsm_cust_email,
                //        Irsm_cust_vat_no = SerMstCost.Irsm_cust_vat_no,
                //        Irsm_orig_grn_com = SerMstCost.Irsm_orig_grn_com,
                //        Irsm_orig_grn_no = SerMstCost.Irsm_orig_grn_no,
                //        Irsm_orig_grn_dt = SerMstCost.Irsm_orig_grn_dt,
                //        Irsm_orig_supp = SerMstCost.Irsm_orig_supp,
                //        Irsm_exist_grn_com = SerMstCost.Irsm_exist_grn_com,
                //        Irsm_exist_grn_no = SerMstCost.Irsm_exist_grn_no,
                //        Irsm_exist_grn_dt = SerMstCost.Irsm_exist_grn_dt,
                //        Irsm_exist_supp = SerMstCost.Irsm_exist_supp,
                //        Irsm_warr_stus = SerMstCost.Irsm_warr_stus,
                //        Irsm_cre_by = Session["UserID"].ToString(),
                //        Irsm_cre_when = DateTime.Now,
                //        Irsm_session_id = Session["SessionID"].ToString(),
                //        Irsm_anal_1 = SerMstCost.Irsm_anal_1,
                //        Irsm_anal_2 = SerMstCost.Irsm_anal_2,
                //        Irsm_anal_3 = SerMstCost.Irsm_anal_3,
                //        Irsm_anal_4 = SerMstCost.Irsm_anal_4,
                //        Irsm_anal_5 = SerMstCost.Irsm_anal_5,
                //        Irsm_rec_com = SerMstCost.Irsm_rec_com,
                //        Irsm_reg_no = SerMstCost.Irsm_reg_no,
                //        Irsm_sup_warr_pd = SerMstCost.Irsm_sup_warr_pd,
                //        Irsm_sup_warr_rem = SerMstCost.Irsm_sup_warr_rem,
                //        Irsm_sup_warr_stdt = SerMstCost.Irsm_sup_warr_stdt,
                //        Irsm_add_warr_pd = req.Rwr_warr_period,
                //        Irsm_add_warr_rem = req.Rwr_warr_rmk,
                //        Irsm_add_warr_stdt = req.Rwr_st_dt
                //    };
                //    serialMasterLogs.Add(serialMasterLog);

                //    warrantyDetail = new InventoryWarrantyDetail()
                //    {
                //        Irsm_ser_id = req.Rwr_ser_id,
                //        Irsm_warr_start_dt = req.Rwr_st_dt,
                //        Irsm_warr_period = req.Rwr_warr_period,
                //        Irsm_warr_rem = req.Rwr_warr_rmk,
                //        Irsm_cust_cd = req.Rwr_cust_cd,
                //        Irsm_cust_del_addr = req.Rwr_cust_cd,
                //        Irsm_cust_email = _masterBusinessCompany.Mbe_email,
                //        Irsm_cust_fax = _masterBusinessCompany.Mbe_fax,
                //        Irsm_cust_mobile = _masterBusinessCompany.Mbe_mob,
                //        Irsm_cust_name = _masterBusinessCompany.Mbe_name,
                //        //  Irsm_cust_prefix = req.,
                //        Irsm_cust_tel = _masterBusinessCompany.Mbe_tel,
                //        Irsm_cust_town = _masterBusinessCompany.Mbe_town_cd,
                //        // Irsm_cust_vat_no = req.
                //        Irsm_cust_addr = _masterBusinessCompany.Mbe_add1,
                //    };
                //    warrantyDetails.Add(warrantyDetail);
                //    Int32 _effect = 0;
                //    string err = "";

                //    _effect = CHNLSVC.General.SaveSerialMasterLog(serialMasterLogs, out err);
                //    _effect = CHNLSVC.General.UpdateWarrantyMasterAmend(warrantyDetails, out err);

                //    if (_effect > 0)
                //    {
                //        dgvItemDetails.DataSource = new int[] { };
                //        dgvItemDetails.DataBind();
                //        dgvChanges.DataSource = new int[] { };
                //        dgvChanges.DataBind();
                //        dgvReqData.DataSource = new int[] { };
                //        dgvReqData.DataBind();
                //        _request = new List<WarrantyAmendRequest>();
                //        Session["_request"] = _request;
                //        panel1.Visible = false;
                //        panel2.Visible = true;
                //        chkReq.Checked = false;
                //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Warranty amend approved successfully !!!')", true);
                //    }
                //    if (err != "")
                //    {
                //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true); return;
                //    }
                //}
                //#endregion
            }
        }

        protected void lbtnMainSearch2_Click(object sender, EventArgs e)
        {
            DateTime dtFrom = new DateTime();
            DateTime dtTo = new DateTime();
            panelItemDetails.Visible = false;
            string status = "";
            if (string.IsNullOrEmpty(txtFromDate.Text))
            {
                 ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter from date')", true);return;
            } 
            if (string.IsNullOrEmpty(txtToDate.Text))
            {
                 ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter to date')", true);return;
            }
            try
            {
                dtFrom = Convert.ToDateTime(txtFromDate.Text);
            }
            catch (Exception)
            {
                 ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('From date is invalid !')", true);return;
            }
            try
            {
                dtTo = Convert.ToDateTime(txtToDate.Text);
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('To date is invalid !')", true); return;
            }
            if (dtTo<dtFrom)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('From date is invalid !')", true); return;
            }
            dtFrom = new DateTime(dtFrom.Year, dtFrom.Month, dtFrom.Day, 0, 0, 0);
            dtTo = new DateTime(dtTo.Year, dtTo.Month, dtTo.Day, 23, 59, 59);
            DataTable dt = CHNLSVC.CommonSearch.SearchWarrentyAmendRequestData(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), dtFrom, dtTo);
            dgvReqData.DataSource = new int[] { };
            if (dt.Rows.Count>0)
            {
                dgvReqData.DataSource = dt;
            }
            dgvReqData.DataBind();
        }

        protected void lbtnItemView_Click(object sender, EventArgs e)
        {
            panelItemDetails.Visible = true;
            txtModel.Text = "";
            txtBrand.Text = "";
            txtStatus.Text = "";
            txtSerNo.Text = "";
            txtWarrNo.Text = "";
            txtWarrRem.Text = "";
            txtBrand.Text = "";
            txtPreWarSts.Text = "";
            var lb = (LinkButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            if (row!=null)
            {
                Label lblModel = (Label)row.FindControl("lblModel");
                txtModel.Text = lblModel.Text;
                Label lblBrand = (Label)row.FindControl("lblBrand");
                txtBrand.Text = lblBrand.Text;
                Label lblItmStatus = (Label)row.FindControl("lblItmStatus");
                txtStatus.Text = lblItmStatus.Text;
                Label lblSNo = (Label)row.FindControl("lblSNo");
                txtSerNo.Text = lblSNo.Text;
                Label lblWNo = (Label)row.FindControl("lblWNo");
                txtWarrNo.Text = lblWNo.Text;
                Label lblWRemarks = (Label)row.FindControl("lblWRemarks");
                txtWarrRem.Text = lblWRemarks.Text;
                Label lblWarrStatus = (Label)row.FindControl("lblWarrStatus");
                txtPreWarSts.Text = lblWarrStatus.Text;
            }
        }
    }
}