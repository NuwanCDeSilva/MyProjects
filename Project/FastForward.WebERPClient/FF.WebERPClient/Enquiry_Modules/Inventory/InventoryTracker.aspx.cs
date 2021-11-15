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

namespace FF.WebERPClient.Enquiry_Modules.Inventory
{
    public partial class InventoryTracker : BasePage
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindJavaScripts();
                BindUserItemStatusDDLData(DDLStatus);
                TextBoxCompany.Text = GlbUserComCode;
                TextBoxLoc.Text = GlbUserDefLoca;
                Uc_CompanySearch1.Company = GlbUserComCode;
                Uc_CompanySearch1.CompanyDes = GlbUserComDesc;
               //load empty grid
                MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                DataTable dataSource = CHNLSVC.CommonSearch.GetInventoryTrackerSearchData(MasterCommonSearchUCtrl.SearchParams);
                GridViewDataBind(dataSource);
                string _masterLocation = (string.IsNullOrEmpty(GlbUserDefLoca)) ? GlbUserDefProf : GlbUserDefLoca;
                if (CHNLSVC.Inventory.CheckUserPermission(GlbUserName, GlbUserComCode, _masterLocation, "INV2"))
                {
                    CheckBoxShowCostVal.Visible = true;
                    CheckBoxShowCostVal.Checked = true;
                    CheckBoxShowCostVal_CheckedChanged(null, null);
                    PanelCompanyDetails.Enabled = true;
                    TextBoxCompany.Enabled = true;
                    ImageButtonCompany.Enabled = true;
                    CollapsiblePanelExtender1.Enabled = true;
                    hdnUserLevel.Value = "1";
                }
                else
                {
                    CheckBoxShowCostVal.Visible = false;
                    CheckBoxShowCostVal.Checked = false;
                    CheckBoxShowCostVal_CheckedChanged(null, null);
                    PanelCompanyDetails.Enabled = false;
                    TextBoxCompany.Enabled = false;
                    ImageButtonCompany.Enabled = false;
                    hdnUserLevel.Value = "0";
                    CollapsiblePanelExtender1.Enabled = false;
                    Image2.Visible = false;
                }
                
            }
                //if postback enable/disable company and location

            if (Convert.ToBoolean(CollapsiblePanelExtender1.ClientState) || CollapsiblePanelExtender1.ClientState==null)
            {
                if (hdnUserLevel.Value == "1")
                {
                    TextBoxCompany.Enabled = true;
                    ImageButtonCompany.Enabled = true;
                }
                TextBoxLoc.Enabled = true;
                ImageButtonLocation.Enabled = true;
                Panel2.Height = 300;
            }
            else
            {
                TextBoxCompany.Enabled = false;
                ImageButtonCompany.Enabled = false;
                TextBoxLoc.Enabled = false;
                ImageButtonLocation.Enabled = false;
                Panel2.Height = 170;
            }

            TextBoxCode.Attributes.Add("onKeyup", "return clickButton(event,'" + imgItemSearch.ClientID + "')");
            TextBoxMain.Attributes.Add("onKeyup", "return clickButton(event,'" + ImageButtonMain.ClientID + "')");
            TextBoxCompany.Attributes.Add("onKeyup", "return clickButton(event,'" + ImageButtonCompany.ClientID + "')");
            TextBoxLoc.Attributes.Add("onKeyup", "return clickButton(event,'" + ImageButtonLocation.ClientID + "')");
            TextBoxSub.Attributes.Add("onKeyup", "return clickButton(event,'" + ImageButtonSub.ClientID + "')");
            TextBoxRange.Attributes.Add("onKeyup", "return clickButton(event,'" + ImageButtonSub1.ClientID + "')");

        }

        private void BindJavaScripts()
        {
            TextBoxCode.Attributes.Add("onblur", "GetItemData()");
            TextBoxMain.Attributes.Add("onchange", "ClearControls('" + TextBoxMain .ClientID+ "')");
            TextBoxSub.Attributes.Add("onchange", "ClearControls('" + TextBoxSub.ClientID + "')");
            TextBoxCompany.Attributes.Add("onchange", "ClearControls('" + TextBoxCompany.ClientID + "')");
        }

        #region common search events
        
        protected void imgItemSearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable dataSource = CHNLSVC.CommonSearch.GetItemSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxCode.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCat_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxMain.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCat_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxSub.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCat_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxRange.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImageButtonCompany_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCompanySearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxCompany.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void ImageButtonLocation_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
            DataTable dataSource = CHNLSVC.CommonSearch.GetUserLocationSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = TextBoxLoc.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        #endregion

        protected void ButtonView_Click(object sender, EventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Inventory_Tracker);
            DataTable dataSource = CHNLSVC.CommonSearch.GetInventoryTrackerSearchData(MasterCommonSearchUCtrl.SearchParams);
            GridViewDataBind(dataSource);
            
        }

        private int GetTotalQty(DataTable dataSource) {
            int total = 0;
            for (int i = 0; i < dataSource.Rows.Count; i++)
            {
                total = total + Convert.ToInt32(dataSource.Rows[i]["FREE_QTY"]);
            }
            return total;
        }

        private void GridViewDataBind(DataTable dataSource)
        {
            GridViewItemDetails.DataSource = dataSource;
            GridViewItemDetails.DataBind();
            //if (CheckBoxShowCostVal.Checked && GridViewItemDetails.Rows.Count > 0)
            //{
                //int total = 0;
                //for (int i = 0; i < GridViewItemDetails.Rows.Count; i++)
                //{
                //    total = total + Convert.ToInt32(GridViewItemDetails.Rows[i].Cells[6].Text);
                //}
                TextBoxTqty.Text = GetTotalQty(dataSource).ToString();
                DivTotal.Visible = true;
            //}
            //else if (!CheckBoxShowCostVal.Checked || GridViewItemDetails.Rows.Count <= 0)
            //    DivTotal.Visible = false;
        }

        protected void GridViewItemDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Inventory_Tracker);
            DataTable dataSource = CHNLSVC.CommonSearch.GetInventoryTrackerSearchData(MasterCommonSearchUCtrl.SearchParams);
            
            GridViewItemDetails.PageIndex = e.NewPageIndex;
            GridViewDataBind(dataSource);         
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Enquiry_Modules/Inventory/InventoryTracker.aspx");
        }

        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../../Default.aspx");
        }


        private void BindUserItemStatusDDLData(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("ALL", "%"));
           // ddl.Items.Add(new ListItem("--select--", "-1"));
            Dictionary<string, string> status_list = CHNLSVC.Inventory.Get_all_ItemSatus();
            foreach (KeyValuePair<string, string> pair in status_list)
            {
               ddl.Items.Add(new ListItem(pair.Value,pair.Key));
                
            }

            //ddl.DataSource = status_list.Keys;
            //ddl.DataBind();
            
        }

        protected void CheckBoxShowCostVal_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxShowCostVal.Checked)
            {
                GridViewItemDetails.Columns[7].Visible = true;
               // DivTotal.Visible = true;
            }
            else
            {
                GridViewItemDetails.Columns[7].Visible = false;
               // DivTotal.Visible = false;
            }
        }

        protected void GridViewItemDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            string status = null;
            if (DDLStatus.SelectedItem == null)
                status = "";
            else
                status = DDLStatus.SelectedItem.Value;

            //if (!TextBoxLoc.Enabled && Uc_CompanySearch1.ProfitCenter != string.Empty)
            //{
            //    uc_ItemSerial1.ITEM_CODE = GridViewItemDetails.Rows[GridViewItemDetails.SelectedIndex].Cells[2].Text;
            //    uc_ItemSerial1.ITEM_STATUS = status;
            //    uc_ItemSerial1.COMPANY = Uc_CompanySearch1.Company;
            //    uc_ItemSerial1.CHANNEL = Uc_CompanySearch1.Channel;
            //    uc_ItemSerial1.SUB_CHANNEL = Uc_CompanySearch1.SubChannel;
            //    uc_ItemSerial1.AREA = Uc_CompanySearch1.Area;
            //    uc_ItemSerial1.REAGION = Uc_CompanySearch1.Region;
            //    uc_ItemSerial1.ZONE = Uc_CompanySearch1.Zone;
            //    uc_ItemSerial1.TYPE = "Loc";
            //    uc_ItemSerial1.LOC = Uc_CompanySearch1.ProfitCenter;

            //}
            //else if (!TextBoxLoc.Enabled)
            //{
            //    uc_ItemSerial1.ITEM_CODE = GridViewItemDetails.Rows[GridViewItemDetails.SelectedIndex].Cells[2].Text;
            //    uc_ItemSerial1.ITEM_STATUS = status;
            //    uc_ItemSerial1.COMPANY = TextBoxCompany.Text;
            //    uc_ItemSerial1.CHANNEL = Uc_CompanySearch1.Channel;
            //    uc_ItemSerial1.SUB_CHANNEL = Uc_CompanySearch1.SubChannel;
            //    uc_ItemSerial1.AREA = Uc_CompanySearch1.Area;
            //    uc_ItemSerial1.REAGION = Uc_CompanySearch1.Region;
            //    uc_ItemSerial1.ZONE = Uc_CompanySearch1.Zone;
            //    uc_ItemSerial1.TYPE = "No_Loc";
            //    uc_ItemSerial1.LOC ="";
            //}
            //else
            //{
            //    uc_ItemSerial1.ITEM_CODE = GridViewItemDetails.Rows[GridViewItemDetails.SelectedIndex].Cells[2].Text;
            //    uc_ItemSerial1.ITEM_STATUS = status;
            //    uc_ItemSerial1.COMPANY = TextBoxCompany.Text;
            //    uc_ItemSerial1.CHANNEL = Uc_CompanySearch1.Channel;
            //    uc_ItemSerial1.SUB_CHANNEL = Uc_CompanySearch1.SubChannel;
            //    uc_ItemSerial1.AREA = Uc_CompanySearch1.Area;
            //    uc_ItemSerial1.REAGION = Uc_CompanySearch1.Region;
            //    uc_ItemSerial1.ZONE = Uc_CompanySearch1.Zone;
            //    uc_ItemSerial1.TYPE = "Loc";
            //    uc_ItemSerial1.LOC = TextBoxLoc.Text;
            //}

            uc_ItemSerial1.ITEM_CODE = GridViewItemDetails.Rows[GridViewItemDetails.SelectedIndex].Cells[2].Text;
            uc_ItemSerial1.ITEM_STATUS = status;
            uc_ItemSerial1.COMPANY = GridViewItemDetails.Rows[GridViewItemDetails.SelectedIndex].Cells[0].Text;
            uc_ItemSerial1.CHANNEL = "";
            uc_ItemSerial1.SUB_CHANNEL = "";
            uc_ItemSerial1.AREA = "";
            uc_ItemSerial1.REAGION = "";
            uc_ItemSerial1.ZONE = "";
            uc_ItemSerial1.TYPE = "Loc";
            uc_ItemSerial1.LOC = GridViewItemDetails.Rows[GridViewItemDetails.SelectedIndex].Cells[1].Text;

            uc_ItemSerial1.IsVisible = true;
            uc_ItemSerial1.Display(); 
            ModalPopupExtender1.Show();
        }

        #region Common Search Methods
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(GlbUserName + seperator + GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location: 
                    {
                        paramsText.Append(TextBoxCompany.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company: 
                    {
                        paramsText.Append(TextBoxCompany.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(TextBoxMain.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(TextBoxMain.Text + seperator + TextBoxSub.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Inventory_Tracker:
                    {
                        string status = null;
                        if (DDLStatus.SelectedItem == null)
                            status = "";
                        else
                            status = DDLStatus.SelectedItem.Value;
                        
                        if (!TextBoxCompany.Enabled && Uc_CompanySearch1.ProfitCenter!=string.Empty)
                        {
                            paramsText.Append(TextBoxCode.Text.ToUpper() + seperator + TextBoxModel.Text.ToUpper() + seperator + status + seperator + Uc_CompanySearch1.Company.ToUpper() + seperator + TextBoxMain.Text.ToUpper() + seperator + TextBoxSub.Text.ToUpper() + seperator + Uc_CompanySearch1.Channel.ToUpper() + seperator + Uc_CompanySearch1.SubChannel.ToUpper() + seperator + Uc_CompanySearch1.Area.ToUpper() + seperator + Uc_CompanySearch1.Region.ToUpper() + seperator + Uc_CompanySearch1.Zone.ToUpper() + seperator + "Loc" + seperator + Uc_CompanySearch1.ProfitCenter.ToUpper() + seperator + TextBoxRange.Text.ToUpper() + seperator);
                       // GridViewItemDetails.EmptyDataText="No Data Found\n"+TextBoxCode.Text + "," + TextBoxModel.Text + "," +status + "," + Uc_CompanySearch1.Company + "," + TextBoxMain.Text + "," + TextBoxSub.Text + "," + Uc_CompanySearch1.Channel + seperator + Uc_CompanySearch1.SubChannel + seperator + Uc_CompanySearch1.Area + seperator + Uc_CompanySearch1.Region + seperator + Uc_CompanySearch1.Zone + seperator + "Loc" + seperator + Uc_CompanySearch1.ProfitCenter + seperator + TextBoxRange.Text + seperator;
                       // GridViewItemDetails.EmptyDataText = "No Data Found<br><b>For Query</b><br>" +
                       //        "Code  :" + TextBoxCode.Text + "<br>" + "Model :" + TextBoxModel.Text + "<br>" + "Status   :" + status + "<br>" + "Company :" + Uc_CompanySearch1.Company + "<br>" + "Main Category    :" + TextBoxMain.Text + "<br>" + "Sub Category  :" + TextBoxSub.Text + "<br>" + "Channel    :" + Uc_CompanySearch1.Channel + "<br>" + "Sub Channel  :" + Uc_CompanySearch1.SubChannel + "<br>" + "Area  :" + Uc_CompanySearch1.Area + "<br>" + "Region  :" + Uc_CompanySearch1.Region + "<br>" + "Zone  :" + Uc_CompanySearch1.Zone   + "<br>" + "Location    :" + Uc_CompanySearch1.ProfitCenter + "<br>" + "Range   :" + TextBoxRange.Text ;
                        }
                        else if (!TextBoxLoc.Enabled) {
                            paramsText.Append(TextBoxCode.Text.ToUpper() + seperator + TextBoxModel.Text.ToUpper() + seperator + status + seperator + Uc_CompanySearch1.Company.ToUpper() + seperator + TextBoxMain.Text.ToUpper() + seperator + TextBoxSub.Text.ToUpper() + seperator + Uc_CompanySearch1.Channel.ToUpper() + seperator + Uc_CompanySearch1.SubChannel.ToUpper() + seperator + Uc_CompanySearch1.Area.ToUpper() + seperator + Uc_CompanySearch1.Region.ToUpper() + seperator + Uc_CompanySearch1.Zone.ToUpper() + seperator + "No_Loc" + seperator + "" + seperator + TextBoxRange.Text.ToUpper() + seperator);
                            //GridViewItemDetails.EmptyDataText = "No Data Found<br><b>For Query</b><br>" +
                            // "Code  :" + TextBoxCode.Text + "<br>" + "Model :" + TextBoxModel.Text + "<br>" + "Status   :" + status + "<br>" + "Company :" + Uc_CompanySearch1.Company + "<br>" + "Main Category    :" + TextBoxMain.Text + "<br>" + "Sub Category  :" + TextBoxSub.Text + "<br>" + "Channel    :" + Uc_CompanySearch1.Channel + "<br>" + "Sub Channel  :" + Uc_CompanySearch1.SubChannel + "<br>" + "Area  :" + Uc_CompanySearch1.Area + "<br>" + "Region  :" + Uc_CompanySearch1.Region + "<br>" + "Zone  :" + Uc_CompanySearch1.Zone   + "<br>" + "Location    :" + Uc_CompanySearch1.ProfitCenter + "<br>" + "Range   :" + TextBoxRange.Text ;
                        }
                        else
                        {

                            paramsText.Append(TextBoxCode.Text + seperator + TextBoxModel.Text.ToUpper() + seperator + status + seperator + TextBoxCompany.Text.ToUpper() + seperator + TextBoxMain.Text.ToUpper() + seperator + TextBoxSub.Text.ToUpper() + seperator + Uc_CompanySearch1.Channel.ToUpper() + seperator + Uc_CompanySearch1.SubChannel.ToUpper() + seperator + Uc_CompanySearch1.Area.ToUpper() + seperator + Uc_CompanySearch1.Region.ToUpper() + seperator + Uc_CompanySearch1.Zone.ToUpper() + seperator + "Loc" + seperator + TextBoxLoc.Text.ToUpper() + seperator + TextBoxRange.Text.ToUpper() + seperator);
                           // GridViewItemDetails.EmptyDataText = "No Data Found<br><b>For Query</b><br>" +
                           //   "Code  :" + TextBoxCode.Text + "<br>" + "Model :" + TextBoxModel.Text + "<br>" + "Status   :" + status + "<br>" + "Company :" + TextBoxCompany.Text + "<br>" + "Main Category    :" + TextBoxMain.Text + "<br>" + "Sub Category  :" + TextBoxSub.Text + "<br>" + "Channel    :" + Uc_CompanySearch1.Channel + "<br>" + "Sub Channel  :" + Uc_CompanySearch1.SubChannel + "<br>" + "Area  :" + Uc_CompanySearch1.Area + "<br>" + "Region  :" + Uc_CompanySearch1.Region + "<br>" + "Zone  :" + Uc_CompanySearch1.Zone   + "<br>" + "Location    :" + Uc_CompanySearch1.ProfitCenter + "<br>" + "Range   :" + TextBoxRange.Text ;
                        }
                        break;
                    }
                    //load empty grid
                case CommonUIDefiniton.SearchUserControlType.Item_Serials:
                    {
                        paramsText.Append("-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "-999" + seperator + "Loc" + seperator + "-999" + seperator + "-999" + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }
        #endregion

        

       
    }
}