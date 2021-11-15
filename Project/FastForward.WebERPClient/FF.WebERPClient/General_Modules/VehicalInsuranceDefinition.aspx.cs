using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Text.RegularExpressions;
using System.Data;
using System.Security.Cryptography;

namespace FF.WebERPClient.General_Modules
{
    public partial class VehicalInsuranceDefinition : BasePage
    {
        #region properties

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
                CreateDtColumn();
                LoadGrid(Main, GridViewFinal);
                BindCombos();
                LoadInsCombos();
                LoadCombos(-5);
                HiddenFieldRowCount.Value = "0";
                TextBoxFrom.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            }
        }

        private void CreateDtColumn() {
            Main = new DataTable();
            Main.Columns.Add("Party", typeof(List<MasterProfitCenter>));
            Main.Columns.Add("Cat", typeof(List<String>));
            Main.Columns.Add("From", typeof(DateTime));
            Main.Columns.Add("To", typeof(DateTime));
            Main.Columns.Add("Sales_Type", typeof(string));
            Main.Columns.Add("Ins_Com", typeof(string));
            Main.Columns.Add("Ins_Pol", typeof(string));
            Main.Columns.Add("Value", typeof(decimal));
            
        }

        private void LoadGrid(DataTable source,GridView gv )
        {
            gv.DataSource = source;
            gv.DataBind();
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
                DropDownListIRange.Items.Clear();
                DropDownListSCat.Items.Add(new ListItem("ALL", "%"));
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
               // DropDownListIRange.Items.Add(new ListItem("ALL", "%"));
                DropDownListSCat.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand(_Mcat, "", "", "", "", "SCat");
                DropDownListSCat.DataTextField = "mi_cate_2";
                DropDownListSCat.DataValueField = "mi_cate_2";
                DropDownListSCat.DataBind();
                //DropDownListIRange.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand(_Mcat, "", "", "", "", "Range");
                //DropDownListIRange.DataTextField = "mi_model";
                //DropDownListIRange.DataValueField = "mi_model";
                //DropDownListIRange.DataBind();
            }
            //load sub cat
            else if (id == 3)
            {
                //DropDownListIRange.Items.Clear();
                //DropDownListIRange.Items.Add(new ListItem("ALL", "%"));
                //DropDownListIRange.DataSource = CHNLSVC.General.GetVehicalRegistrationBrand("", _Scat, "", "", "", "Range");
                //DropDownListIRange.DataTextField = "mi_model";
                //DropDownListIRange.DataValueField = "mi_model";
                //DropDownListIRange.DataBind();
                
            }
           
            //load  item
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

        private void BindCombos()
        {
            DataTable datasource = CHNLSVC.General.GetPartyTypes();
            DropDownListCompany.Items.Clear();
            DropDownListCompany.Items.Add(new ListItem("--SELECT--","-1"));
            foreach (DataRow dr in datasource.Rows)
            {
                DropDownListCompany.Items.Add(new ListItem(dr["mpc_com"].ToString(), dr["mpc_com"].ToString()));
            }
            
            DataTable datasource2 = CHNLSVC.General.GetSalesTypes("",null,null);
            foreach (DataRow dr in datasource2.Rows)
            {
                DropDownListSType.Items.Add(new ListItem(dr["srtp_cd"].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr["srtp_cd"].ToString().Length)) + "-" + dr["srtp_desc"].ToString(), dr["srtp_cd"].ToString()));
            }
        }

        private void LoadInsCombos() {
            DropDownListInsCom.DataSource = CHNLSVC.General.GetInsuranceCompanies();
            DropDownListInsCom.DataTextField= "MBI_DESC";
            DropDownListInsCom.DataValueField = "MBI_CD";
            DropDownListInsCom.DataBind();

            DropDownListInsPol.DataSource = CHNLSVC.General.GetInsurancePolicies();
            DropDownListInsPol.DataTextField = "SVIP_POLC_DESC";
            DropDownListInsPol.DataValueField = "SVIP_POLC_CD";
            DropDownListInsPol.DataBind();
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

        protected void ButtonSearchLoc_Click(object sender, EventArgs e)
        {
            if (TextBoxLoc.Text != "" && !CheckBoxAll.Checked)
            {

                DataTable datasource = CHNLSVC.General.GetPartyCodes(DropDownListCompany.SelectedValue, TextBoxLoc.Text);
                if (datasource.Rows.Count <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid Location Code");
                }
                foreach (DataRow dr in datasource.Rows)
                {
                    ListItem li = ListBoxLoc.Items.FindByValue(dr["mpc_cd"].ToString());
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
            else if (CheckBoxAll.Checked)
            {
                ListBoxLoc.Items.Clear();
                DataTable datasource = CHNLSVC.General.GetPartyCodes(DropDownListCompany.SelectedValue, "");
                if (datasource.Rows.Count <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "No Locations");
                }
                foreach (DataRow dr in datasource.Rows)
                {

                    ListBoxLoc.Items.Add(new ListItem(dr["mpc_cd"].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr["mpc_cd"].ToString().Length)) + "-" + dr["mpc_desc"].ToString(), dr["mpc_cd"].ToString()));
                }
            }
            foreach (ListItem li in ListBoxLoc.Items)
            {
                li.Selected = true;
            }
            Company = DropDownListCompany.SelectedValue;
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (TextBoxItem.Text != "" && !CheckBoxItemAll.Checked)
            {
                DataTable dataSource = CHNLSVC.General.GetVehicalRegistrationBrand(DropDownListCat.SelectedValue, DropDownListSCat.SelectedValue, DropDownListIRange.SelectedValue, TextBoxItem.Text, DropDownListBrand.SelectedValue, "Code");
                if (dataSource.Rows.Count <= 0)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Invalid Item Code");
                }
                foreach (DataRow dr in dataSource.Rows)
                {
                    ListItem li = ListBoxLoc.Items.FindByValue(dr["mi_cd"].ToString());
                    if (li == null)
                    {
                        ListBoxItems.Items.Add(new ListItem(dr["mi_cd"].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(15 - dr["mi_cd"].ToString().Length)) + "-" + dr["mi_shortdesc"].ToString(), dr["mi_cd"].ToString()));
                        TextBoxItem.Text = "";
                    }
                    else
                    {
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

        protected void ButtonOAdd_Click(object sender, EventArgs e)
        {
            if (ListBoxItems.SelectedItem != null && ListBoxLoc.SelectedItem != null)
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
                if (Convert.ToDateTime(dr["From"]) > Convert.ToDateTime(dr["To"]))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "From date has to be smaller than To date");
                    return;
                }
                dr["Sales_Type"] = DropDownListSType.SelectedValue;
                dr["Ins_Com"] = DropDownListInsCom.SelectedValue;
                dr["Ins_Pol"] = DropDownListInsPol.SelectedValue;
                try
                {
                    dr["Value"] = Convert.ToDecimal(TextBoxVal.Text);
                  
                }
                catch (Exception)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Value has to be number");
                    return;
                }
                ListBoxItems.SelectedIndex = -1;
                ListBoxLoc.SelectedIndex = -1;
                Main.Rows.Add(dr);
                LoadGrid(Main, GridViewFinal);
                TextBoxVal.Text = "0";
                TextBoxFrom.Text = string.Empty;
                TextBoxTo.Text = string.Empty;
                DropDownListInsPol.SelectedIndex = 0;
                DropDownListInsCom.SelectedIndex = 0;
                DropDownListSType.SelectedIndex = 0;
                HiddenFieldRowCount.Value = Main.Rows.Count.ToString();
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (Main.Rows.Count > 0)
            {
                try
                {
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
                            for (int k = 0; k < itemsTem.Count; )
                            {
                                if (itemsTem.Count > (k + 50))
                                {
                                    List<string> items = itemsTem.GetRange(k, 50);
                                    //send 200 pcs and 200 items at once
                                    CHNLSVC.General.SaveVehicalInsuranceDefinition(pcs, items, Convert.ToDateTime(Main.Rows[i][2]), Convert.ToDateTime(Main.Rows[i][3]), Main.Rows[i][4].ToString(), Convert.ToDecimal(Main.Rows[i][7]), GlbUserName, Main.Rows[i][5].ToString(), Main.Rows[i][6].ToString(), Convert.ToInt32(DropDownListPeriod.SelectedValue), CheckBoxReq.Checked);
                                    k = k +50;
                                }
                                else
                                {
                                    List<string> items = itemsTem.GetRange(k, itemsTem.Count - k);
                                    CHNLSVC.General.SaveVehicalInsuranceDefinition(pcs, items, Convert.ToDateTime(Main.Rows[i][2]), Convert.ToDateTime(Main.Rows[i][3]), Main.Rows[i][4].ToString(), Convert.ToDecimal(Main.Rows[i][7]), GlbUserName, Main.Rows[i][5].ToString(), Main.Rows[i][6].ToString(), Convert.ToInt32(DropDownListPeriod.SelectedValue), CheckBoxReq.Checked);
                                    k = k + (itemsTem.Count - k);
                                }
                            }
                        }
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Records added sucessfully!');window.location='VehicalInsuranceDefinition.aspx'", true);
                    }
                }
                catch (Exception er)
                {
                    string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = '../Default.aspx';</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                }
            }


        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("../General_Modules/VehicalInsuranceDefinition.aspx");
        }

        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }

        protected void GridViewFinal_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Main.Rows.RemoveAt(e.RowIndex);
                LoadGrid(Main, GridViewFinal);
                HiddenFieldRowCount.Value = Main.Rows.Count.ToString();
            }
            catch (Exception er)
            {
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, er.Message);
                string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = '../Default.aspx';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

            }
        }

        protected void LinkButtonLclear_Click(object sender, EventArgs e)
        {
            ListBoxLoc.Items.Clear();
        }

        protected void LinkButtonItClear_Click(object sender, EventArgs e)
        {
            ListBoxItems.Items.Clear();
        }

        protected void LinkButtonAddCom_Click(object sender, EventArgs e)
        {
            GridViewInsCompany.DataSource = null;
            GridViewInsCompany.DataBind();
            TextBoxInsCode.Text = "";
            TextBoxInsName.Text = "";
            ModalPopupExtender2.Show();
        }

        protected void LinkButtonAddPol_Click(object sender, EventArgs e)
        {
            GridViewPolicy.DataSource = null;
            GridViewPolicy.DataBind();
            TextBoxPType.Text = "";
            ModalPopupExtender1.Show();
        }

        protected void ButtonInsComAdd_Click(object sender, EventArgs e)
        {
            MasterOutsideParty _outPar = new MasterOutsideParty();
            _outPar.Mbi_cd = TextBoxInsCode.Text;
            _outPar.Mbi_desc = TextBoxInsName.Text;
            _outPar.Mbi_tp = "INS";
            _outPar.Mbi_cre_when = DateTime.Now;
            _outPar.Mbi_act = true;
            try
            {
                int retVal = CHNLSVC.General.SaveInsuranceCompany(_outPar);
                if (retVal == -999)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Company alredy excists!');", true);
                }
                DataTable dtCom = CHNLSVC.General.GetInsuranceCompanies();
                LoadGrid(dtCom, GridViewInsCompany);
                LoadInsCombos();
                ModalPopupExtender2.Show();
            }
            catch (Exception er)
            {
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, er.Message);
                string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = '../Default.aspx';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

            }
        }

        protected void ButtonPolAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int retVal = CHNLSVC.General.SaveInsurancePolicy(TextBoxPType.Text, GlbUserName);
                if (retVal == -999)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "KK", "alert('Ploicy alredy excists!');", true);
                }
                DataTable dtPol = CHNLSVC.General.GetInsurancePolicies();
                LoadGrid(dtPol, GridViewPolicy);
                LoadInsCombos();
                ModalPopupExtender1.Show();
            }
            catch (Exception er)
            {
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, er.Message);
                string Msg = "<script>alert('System Error Occur. : " + er.Message + "');window.location = '../Default.aspx';</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

            }
        }
    }
}