using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Threading;
using System.Text.RegularExpressions;

namespace FF.WebERPClient.Advance_Module
{
    public partial class VehicalRegistrationDefinition : BasePage
    {
        #region properties

        //public DataTable Category
        //{
        //    get { return (DataTable)ViewState["category"]; }
        //    set { ViewState["category"] = value; }
        //}

        //public DataTable Party
        //{
        //    get { return (DataTable)ViewState["party"]; }
        //    set { ViewState["party"] = value; }
        //}

        public DataTable Main
        {
            get { return (DataTable)ViewState["main"]; }
            set { ViewState["main"] = value; }
        }

        public string Company
        {
            get { return (string)ViewState["com"]; }
            set { ViewState["com"] = value; }
        }

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCombos(-5);
                Company = "";
                //CreateTableCategory();
                //CreateTableParty();
                CreateTableMain();
                //GridBind(GridViewBrand, Category);
               // GridBind(GridViewParty, Party);
                GridBind(GridViewFinal, Main);
                BindCombos();
                HiddenFieldRowCount.Value = "0";
                TextBoxFrom.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            }
        }

        private void BindCombos()
        {
            DataTable datasource = CHNLSVC.General.GetPartyTypes();
            DropDownListCompany.Items.Clear();
            DropDownListCompany.Items.Add(new ListItem("--select--","-1"));
            foreach (DataRow dr in datasource.Rows)
            {
                DropDownListCompany.Items.Add(new ListItem(dr["mpc_com"].ToString(), dr["mpc_com"].ToString()));
            }
            //DataTable datasource1 = CHNLSVC.General.GetPartyCodes();
            //foreach (DataRow dr in datasource1.Rows)
            //{
            //   // DropDownListPCode.Items.Add(new ListItem(dr["mpi_cd"].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr["mpi_cd"].ToString().Length)) + "-" + dr["mpi_val"].ToString(), dr["mpi_cd"].ToString()));
            //}
            DataTable datasource2 = CHNLSVC.General.GetSalesTypes("",null,null);
            foreach (DataRow dr in datasource2.Rows)
            {
                DropDownListSType.Items.Add(new ListItem(dr["srtp_cd"].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr["srtp_cd"].ToString().Length)) + "-" + dr["srtp_desc"].ToString(), dr["srtp_cd"].ToString()));
            }
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


        private void GridBind(GridView gv, DataTable dt)
        {
            gv.DataSource = dt;
            gv.DataBind();
        }

        //private void CreateTableCategory()
        //{
        //    Category = new DataTable();
        //    Category.Columns.Add("Brand", typeof(string));
        //    Category.Columns.Add("Category", typeof(string));
        //    Category.Columns.Add("Sub_Category", typeof(string));
        //    Category.Columns.Add("Item_Range", typeof(string));
        //    Category.Columns.Add("Item", typeof(string));
        //}

        //private void CreateTableParty()
        //{
        //    Party = new DataTable();
        //    Party.Columns.Add("Party_Type", typeof(string));
        //    Party.Columns.Add("Party_Code", typeof(string));
        //}

        private void CreateTableMain()
        {
            Main = new DataTable();
            Main.Columns.Add("Party", typeof(List<MasterProfitCenter>));
            Main.Columns.Add("Cat", typeof(List<String>));
            Main.Columns.Add("From", typeof(DateTime));
            Main.Columns.Add("To", typeof(DateTime));
            Main.Columns.Add("Sales_Type", typeof(string));
            Main.Columns.Add("Registration_Value", typeof(Decimal));
            Main.Columns.Add("Claim_Value", typeof(Decimal));
        }

        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("../General_Modules/VehicalRegistrationDefinition.aspx");
        }

        protected void DropDownListBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCombos(1);
        }

        protected void DropDownListCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCombos(2);
        }

        protected void DropDownListSCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCombos(3);
        }

        protected void DropDownListIRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCombos(4);
        }

        private void LoadCombos(int id)
        {
            
            string _brand = (!string.IsNullOrEmpty(DropDownListBrand.SelectedValue)) ? (DropDownListBrand.SelectedValue) : "";
            string _Mcat = (!string.IsNullOrEmpty(DropDownListCat.SelectedValue)) ? (DropDownListCat.SelectedValue) : "";
            string _Scat = (!string.IsNullOrEmpty(DropDownListSCat.SelectedValue)) ? (DropDownListSCat.SelectedValue) : "";
            string _Range = (!string.IsNullOrEmpty(DropDownListIRange.SelectedValue)) ? (DropDownListIRange.SelectedValue) : "";

            //DropDownListBrand.Items.Clear();
            //load brand
            if (id == 1)
            {
                DropDownListCat.Items.Clear();
                DropDownListCat.Items.Add(new ListItem("ALL", "%"));
                DropDownListSCat.Items.Clear();
                DropDownListSCat.Items.Add(new ListItem("ALL", "%"));
                DropDownListIRange.Items.Clear();
                //DropDownListBrand.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand("", "", "", "", _brand, "Brand");
                //DropDownListBrand.DataTextField = "mi_brand";
                //DropDownListBrand.DataValueField = "mi_brand";
                //DropDownListBrand.DataBind();
                DropDownListCat.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand("", "", "", "", _brand, "MCat");
                DropDownListCat.DataTextField = "mi_cate_1";
                DropDownListCat.DataValueField = "mi_cate_1";
                DropDownListCat.DataBind();
                DropDownListSCat.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand("", "", "", "", _brand, "SCat");
                DropDownListSCat.DataTextField = "mi_cate_2";
                DropDownListSCat.DataValueField = "mi_cate_2";
                DropDownListSCat.DataBind();
                DropDownListIRange.Items.Add(new ListItem("ALL", "%"));
                DropDownListIRange.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand("", "", "", "", _brand, "Range");
                DropDownListIRange.DataTextField = "mi_model";
                DropDownListIRange.DataValueField = "mi_model";
                DropDownListIRange.DataBind();
            }
            //load cat
            else if (id == 2)
            {
                DropDownListSCat.Items.Clear();
                DropDownListSCat.Items.Add(new ListItem("ALL", "%"));
                //DropDownListIRange.Items.Clear();
                //DropDownListBrand.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand(_Mcat, "", "", "", "", "Brand");
                //DropDownListBrand.DataTextField = "mi_brand";
                //DropDownListBrand.DataValueField = "mi_brand";
                //DropDownListBrand.DataBind();
                //DropDownListCat.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand(_Mcat, "", "", "", "", "MCat");
                //DropDownListCat.DataTextField = "mi_cate_1";
                //DropDownListCat.DataValueField = "mi_cate_1";
                //DropDownListCat.DataBind();
                DropDownListSCat.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand(_Mcat, "", "", "", "", "SCat");
                DropDownListSCat.DataTextField = "mi_cate_2";
                DropDownListSCat.DataValueField = "mi_cate_2";
                DropDownListSCat.DataBind();
                //DropDownListIRange.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand(_Mcat, "", "", "", "", "Range");
                //DropDownListIRange.DataTextField = "mi_cate_3";
                //DropDownListIRange.DataValueField = "mi_cate_3";
                //DropDownListIRange.DataBind();
                //ListBoxItems.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand(_Mcat, "", "", "", "", "Code");
                //ListBoxItems.DataTextField = "mi_cd";
                //ListBoxItems.DataValueField = "mi_cd";
                //ListBoxItems.DataBind();
            }
           //load sub cat
            else if (id == 3)
            {
                //DropDownListIRange.Items.Clear();
                //DropDownListIRange.Items.Add(new ListItem("ALL", "%"));
                //DropDownListBrand.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand("", _Scat, "", "", "", "Brand");
                //DropDownListBrand.DataTextField = "mi_brand";
                //DropDownListBrand.DataValueField = "mi_brand";
                //DropDownListBrand.DataBind();
                //DropDownListCat.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand("", _Scat, "", "", "", "MCat");
                //DropDownListCat.DataTextField = "mi_cate_1";
                //DropDownListCat.DataValueField = "mi_cate_1";
                //DropDownListCat.DataBind();
                //DropDownListSCat.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand("", _Scat, "", "", "", "SCat");
                //DropDownListSCat.DataTextField = "mi_cate_2";
                //DropDownListSCat.DataValueField = "mi_cate_2";
                //DropDownListSCat.DataBind();
                //DropDownListIRange.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand("", _Scat, "", "", "", "Range");
                //DropDownListIRange.DataTextField = "mi_cate_3";
                //DropDownListIRange.DataValueField = "mi_cate_3";
                //DropDownListIRange.DataBind();
                //ListBoxItems.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand("", _Scat, "", "", "", "Code");
                //ListBoxItems.DataTextField = "mi_cd";
                //ListBoxItems.DataValueField = "mi_cd";
                //ListBoxItems.DataBind();
            }
            //load sub range
            else if (id == 4)
            {
                //DropDownListBrand.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand("", "", _Range, "", "", "Brand");
                //DropDownListBrand.DataTextField = "mi_brand";
                //DropDownListBrand.DataValueField = "mi_brand";
                //DropDownListBrand.DataBind();
                //DropDownListCat.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand("", "", _Range, "", "", "MCat");
                //DropDownListCat.DataTextField = "mi_cate_1";
                //DropDownListCat.DataValueField = "mi_cate_1";
                //DropDownListCat.DataBind();
                //DropDownListSCat.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand("", "", _Range, "", "", "SCat");
                //DropDownListSCat.DataTextField = "mi_cate_2";
                //DropDownListSCat.DataValueField = "mi_cate_2";
                //DropDownListSCat.DataBind();
                //DropDownListIRange.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand("", "", _Range, "", "", "Range");
                //DropDownListIRange.DataTextField = "mi_cate_3";
                //DropDownListIRange.DataValueField = "mi_cate_3";
                //DropDownListIRange.DataBind();
                //ListBoxItems.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand("", "", _Range, "", "", "Code");
                //ListBoxItems.DataTextField = "mi_cd";
                //ListBoxItems.DataValueField = "mi_cd";
                //ListBoxItems.DataBind();
            }
            //load  item
            else if (id == 5)
            {

                //DropDownListBrand.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand("", "", "", _Code, "", "Brand");
                //DropDownListBrand.DataTextField = "mi_brand";
                //DropDownListBrand.DataValueField = "mi_brand";
                //DropDownListBrand.DataBind();
                //DropDownListCat.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand("", "", "", _Code, "", "MCat");
                //DropDownListCat.DataTextField = "mi_cate_1";
                //DropDownListCat.DataValueField = "mi_cate_1";
                //DropDownListCat.DataBind();
                //DropDownListSCat.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand("", "", "", _Code, "", "SCat");
                //DropDownListSCat.DataTextField = "mi_cate_2";
                //DropDownListSCat.DataValueField = "mi_cate_2";
                //DropDownListSCat.DataBind();
                //DropDownListIRange.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand("", "", "", _Code, "", "Range");
                //DropDownListIRange.DataTextField = "mi_cate_3";
                //DropDownListIRange.DataValueField = "mi_cate_3";
                //DropDownListIRange.DataBind();
                //DropDownListItem.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand("", "", "", _Code, "", "Code");
                //DropDownListItem.DataTextField = "mi_cd";
                //DropDownListItem.DataValueField = "mi_cd";
                //DropDownListItem.DataBind();
            }
            else
            {
                DropDownListIRange.Items.Add(new ListItem("ALL", "%"));
                DropDownListBrand.Items.Add(new ListItem("ALL", "%"));
                DropDownListSCat.Items.Add(new ListItem("ALL", "%"));
                DropDownListCat.Items.Add(new ListItem("ALL", "%"));
                DropDownListIRange.Items.Add(new ListItem("ALL", "%"));
                DropDownListBrand.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand(_Mcat, _Scat, _Range, "", _brand, "Brand");
                DropDownListBrand.DataTextField = "mi_brand";
                DropDownListBrand.DataValueField = "mi_brand";
                DropDownListBrand.DataBind();
                _brand = (!string.IsNullOrEmpty(DropDownListBrand.SelectedValue)) ? (DropDownListBrand.SelectedValue) : "";
                DropDownListCat.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand(_Mcat, _Scat, _Range, "", _brand, "MCat");
                DropDownListCat.DataTextField = "mi_cate_1";
                DropDownListCat.DataValueField = "mi_cate_1";
                DropDownListCat.DataBind();
                _brand = (!string.IsNullOrEmpty(DropDownListBrand.SelectedValue)) ? (DropDownListBrand.SelectedValue) : "";
                _Mcat = (!string.IsNullOrEmpty(DropDownListCat.SelectedValue)) ? (DropDownListCat.SelectedValue) : "";
                DropDownListSCat.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand(_Mcat, _Scat, _Range, "", _brand, "SCat");
                DropDownListSCat.DataTextField = "mi_cate_2";
                DropDownListSCat.DataValueField = "mi_cate_2";
                DropDownListSCat.DataBind();
                _brand = (!string.IsNullOrEmpty(DropDownListBrand.SelectedValue)) ? (DropDownListBrand.SelectedValue) : "";
                _Mcat = (!string.IsNullOrEmpty(DropDownListCat.SelectedValue)) ? (DropDownListCat.SelectedValue) : "";
                _Scat = (!string.IsNullOrEmpty(DropDownListSCat.SelectedValue)) ? (DropDownListSCat.SelectedValue) : "";
                DropDownListIRange.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand(_Mcat, _Scat, _Range, "", _brand, "Range");
                DropDownListIRange.DataTextField = "mi_model";
                DropDownListIRange.DataValueField = "mi_model";
                DropDownListIRange.DataBind();
                _brand = (!string.IsNullOrEmpty(DropDownListBrand.SelectedValue)) ? (DropDownListBrand.SelectedValue) : "";
                _Mcat = (!string.IsNullOrEmpty(DropDownListCat.SelectedValue)) ? (DropDownListCat.SelectedValue) : "";
                _Scat = (!string.IsNullOrEmpty(DropDownListSCat.SelectedValue)) ? (DropDownListSCat.SelectedValue) : "";
                _Range = (!string.IsNullOrEmpty(DropDownListIRange.SelectedValue)) ? (DropDownListIRange.SelectedValue) : "";
                //ListBoxItems.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand(_Mcat, _Scat, _Range, "", _brand, "Code");
                //ListBoxItems.DataTextField = "mi_cd";
                //ListBoxItems.DataValueField = "mi_cd";
                //ListBoxItems.DataBind();
            }  
        }

        protected void ButtonOAdd_Click(object sender, EventArgs e)
        {
            if (ListBoxItems.SelectedItem != null && ListBoxLoc.SelectedItem!=null)
            {
                
                    DataRow dr = Main.NewRow();
                    List<MasterProfitCenter> pcs = new List<MasterProfitCenter>();
                    List<string> items = new List<string>();
                    foreach (ListItem li in ListBoxLoc.Items)
                    {
                        if (li.Selected)
                        {
                            MasterProfitCenter pc = new MasterProfitCenter();
                            pc.Mpc_cd = li.Value;
                            //user change company after search ???
                            pc.Mpc_com = Company;
                            pcs.Add(pc);
                        }
                    }
                    foreach (ListItem li in ListBoxItems.Items)
                    {
                        if (li.Selected)
                        {
                            items.Add(li.Value);
                        }
                    }
                dr["Party"] = pcs;
                dr["Cat"] = items;
                    try
                    {
                        dr["From"] = Convert.ToDateTime(TextBoxFrom.Text);
                        dr["To"] = Convert.ToDateTime(TextBoxTo.Text);
                    }
                    catch (Exception)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please select From and To  dates");
                        return;
                    }
                    if (Convert.ToDateTime(dr["From"]) > Convert.ToDateTime(dr["To"])) {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From date has to be smaller than To date");
                        return;
                    }
                        dr["Sales_Type"] = DropDownListSType.SelectedValue;
                        try
                        {
                            dr["Registration_Value"] = Convert.ToDecimal(TextBoxRvalue.Text);
                            dr["Claim_Value"] = Convert.ToDecimal(TextBoxCvalue.Text);
                        }
                        catch (Exception)
                        {
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Registration value and claim value has to be number");
                            return;
                        }
                        ListBoxItems.SelectedIndex = -1;
                        ListBoxLoc.SelectedIndex = -1;
                    Main.Rows.Add(dr);
                    //CreateTableParty();
                   // CreateTableCategory();
                    GridBind(GridViewFinal, Main);
                    //GridBind(GridViewBrand, Category);
                    //GridBind(GridViewParty, Party);
                    HiddenFieldRowCount.Value = Main.Rows.Count.ToString();
                    TextBoxCvalue.Text = "0";
                    TextBoxRvalue.Text = "0";
                    TextBoxFrom.Text = string.Empty;
                    TextBoxTo.Text = string.Empty;
            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please add company and item details");
            }
        }

       
        protected void GridViewFinal_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Main.Rows.RemoveAt(e.RowIndex);
            GridBind(GridViewFinal, Main);
            HiddenFieldRowCount.Value = Main.Rows.Count.ToString();
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
           
            if (Main.Rows.Count > 0) {
                //main table
                for (int i = 0; i < Main.Rows.Count; i++)
                {
                        List<MasterProfitCenter> pcsTem = (List<MasterProfitCenter>)Main.Rows[i][0];
                        List<string> itemsTem = (List<string>)Main.Rows[i][1];
                        //divide pcs list into parts(200 item) 
                        for (int j = 0; j < pcsTem.Count; )
                        {
                            List<MasterProfitCenter> pcs = new List<MasterProfitCenter>();
                            if (pcsTem.Count > (j + 200))
                            {
                                pcs = pcsTem.GetRange(j, 200);
                                j = j + 200;
                            }
                            else
                            {
                                pcs = pcsTem.GetRange(j, pcsTem.Count - j);
                                j = j + (pcsTem.Count - j);
                            }
                            //divide items list into parts(200 item) 
                            for (int k = 0; k < itemsTem.Count; ) {
                                if (itemsTem.Count > (k + 50))
                                {
                                    List<string> items = itemsTem.GetRange(k, 50);
                                    //send 200 pcs and 200 items at once
                                    CHNLSVC.General.SaveVehicalRegistrationDefinition(pcs, items, Convert.ToDateTime(Main.Rows[i][2]), Convert.ToDateTime(Main.Rows[i][3]), Main.Rows[i][4].ToString(), Convert.ToDecimal(Main.Rows[i][5]), Convert.ToDecimal(Main.Rows[i][6]), GlbUserName,Convert.ToInt32(CheckBoxMandatory.Checked));
                                    k = k + 50;
                                }
                                else
                                {
                                    List<string> items = itemsTem.GetRange(k, itemsTem.Count - k);
                                    CHNLSVC.General.SaveVehicalRegistrationDefinition(pcs, items, Convert.ToDateTime(Main.Rows[i][2]), Convert.ToDateTime(Main.Rows[i][3]), Main.Rows[i][4].ToString(), Convert.ToDecimal(Main.Rows[i][5]), Convert.ToDecimal(Main.Rows[i][6]), GlbUserName, Convert.ToInt32(CheckBoxMandatory.Checked));
                                    k = k + (itemsTem.Count - k);
                                }

                            } 
                        }                          
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Records added sucessfully!');window.location='VehicalRegistrationDefinition.aspx'", true);
                    }

                /* COMMENTED ON 2012\08\09
                 *CHANGE LAYOUT
      
                for (int i = 0; i < Main.Rows.Count; i++) { 
                    DataTable party = (DataTable)Main.Rows[i][0];
                    DataTable category = (DataTable)Main.Rows[i][1];
                    //party table
                    for (int j = 0; j < party.Rows.Count; j++) { 
                        //category table
                        for (int k = 0; k < category.Rows.Count; k++) {
                            int seqNo = CHNLSVC.Inventory.GetSerialID();
                            FF.BusinessObjects.VehicalRegistrationDefnition vehical = new FF.BusinessObjects.VehicalRegistrationDefnition();
                            vehical.Svrd_seq = seqNo;
                            vehical.Svrd_pty_cd = party.Rows[j][1].ToString();
                            vehical.Svrd_pty_tp = party.Rows[j][0].ToString();
                            //vehical.Svrd_brd = category.Rows[k][0].ToString();
                            //vehical.Svrd_cat1 = category.Rows[k][1].ToString();
                            //vehical.Svrd_cat2 = category.Rows[k][2].ToString();
                            //vehical.Svrd_cat3 = category.Rows[k][3].ToString();
                            vehical.Svrd_itm = category.Rows[k][4].ToString();
                            vehical.Svrd_from_dt = Convert.ToDateTime(Main.Rows[i][2]);
                            vehical.Svrd_to_dt = Convert.ToDateTime(Main.Rows[i][3]);
                            vehical.Svrd_sale_tp = Main.Rows[i][4].ToString();
                            vehical.Svrd_val = Convert.ToDecimal(Main.Rows[i][5]);
                            vehical.Svrd_claim_val = Convert.ToDecimal(Main.Rows[i][6]);
                            vehical.Svrd_cre_by = GlbUserName;
                            vehical.Svrd_cre_dt = DateTime.Now;

                            CHNLSVC.General.SaveVehicalRegistrationDefinition(vehical);
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Records updated sucessfully!');window.location='VehicalRegistrationDefinition.aspx'", true);
                        }
                    }
                }
                 * 
                 */
            }
        }



        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (TextBoxItem.Text != "" && !CheckBoxItemAll.Checked)
            {
                DataTable dataSource = CHNLSVC.General.GetVehicalRegistrationBrand(DropDownListCat.SelectedValue, DropDownListSCat.SelectedValue, DropDownListIRange.SelectedValue, TextBoxItem.Text, DropDownListBrand.SelectedValue, "Code");
                if (dataSource.Rows.Count <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid Item code");
                }
                foreach (DataRow dr in dataSource.Rows)
                {
                    ListItem li = ListBoxLoc.Items.FindByValue(dr["mi_cd"].ToString());
                    if (li == null)
                    {
                        ListBoxItems.Items.Add(new ListItem(dr["mi_cd"].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(15 - dr["mi_cd"].ToString().Length)) + "-" + dr["mi_shortdesc"].ToString(), dr["mi_cd"].ToString()));
                        TextBoxItem.Text = "";
                    }
                    else {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Item already in the list");
                    }
                }
            }
            else if (CheckBoxItemAll.Checked)
            {
                ListBoxItems.Items.Clear();
                DataTable dataSource = CHNLSVC.General.GetVehicalRegistrationBrand(DropDownListCat.SelectedValue, DropDownListSCat.SelectedValue, DropDownListIRange.SelectedValue, "%", DropDownListBrand.SelectedValue, "Code");
                if (dataSource.Rows.Count <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No Items");
                }
                foreach (DataRow dr in dataSource.Rows)
                {
                    ListBoxItems.Items.Add(new ListItem(dr["mi_cd"].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(15 - dr["mi_cd"].ToString().Length)) + "-" + dr["mi_shortdesc"].ToString(), dr["mi_cd"].ToString()));
                }
            }
            foreach (ListItem li in ListBoxItems.Items)
            {
                li.Selected = true;
            } 
        }

        protected void ButtonSearchLoc_Click(object sender, EventArgs e)
        {
            
            if (TextBoxLoc.Text != "" && !CheckBoxAll.Checked) {
                
                DataTable datasource = CHNLSVC.General.GetPartyCodes(DropDownListCompany.SelectedValue, TextBoxLoc.Text);
                if (datasource.Rows.Count <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid Location code");
                }
                foreach (DataRow dr in datasource.Rows)
                {
                    ListItem li= ListBoxLoc.Items.FindByValue(dr["mpc_cd"].ToString());
                    if (li == null)
                    {
                        ListBoxLoc.Items.Add(new ListItem(dr["mpc_cd"].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr["mpc_cd"].ToString().Length)) + "-" + dr["mpc_desc"].ToString(), dr["mpc_cd"].ToString()));
                        TextBoxLoc.Text = "";
                    }
                    else
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Location already in the list");
                    }
                }
            }
            else if (CheckBoxAll.Checked) {
                ListBoxLoc.Items.Clear();
                DataTable datasource=CHNLSVC.General.GetPartyCodes(DropDownListCompany.SelectedValue, "");
                if (datasource.Rows.Count <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No Locations");
                }
                foreach(DataRow dr in datasource.Rows){
                    
                    ListBoxLoc.Items.Add(new ListItem(dr["mpc_cd"].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr["mpc_cd"].ToString().Length)) + "-" + dr["mpc_desc"].ToString(), dr["mpc_cd"].ToString()));
                }
            }
            foreach (ListItem li in ListBoxLoc.Items)
            {
                li.Selected = true;
            }
            Company = DropDownListCompany.SelectedValue;
        }

        protected void LinkButtonLclear_Click(object sender, EventArgs e)
        {
            ListBoxLoc.Items.Clear();
        }

        protected void LinkButtonItClear_Click(object sender, EventArgs e)
        {
            ListBoxItems.Items.Clear();
        }
    }
}