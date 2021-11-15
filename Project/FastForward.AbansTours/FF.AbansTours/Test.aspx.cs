using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.AbansTours;
using FF.AbansTours.UserControls;

namespace WebApplication1
{
    public partial class Test : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void buttonSearchTransferLoc_Click(object sender, ImageClickEventArgs e)
        {
            //BasePage basepage = new BasePage();
            //Page pp = (Page)this.Page;
            //uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
            //ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.WHAREHOUSE);
            //DataTable dataSource = basepage.CHNLSVC.Inventory.GetLocationByType(ucc.SearchParams, null, null);
            //ucc.BindUCtrlDDLData(dataSource);
            //ucc.BindUCtrlGridData(dataSource);
            //ucc.ReturnResultControl = txtTransferLocation.ClientID;
            //ucc.UCModalPopupExtender.Show();
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            BasePage basepage = new BasePage();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(basepage.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(basepage.GlbUserName + seperator + basepage.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        //paramsText.Append(TextBoxCompany.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        //paramsText.Append(TextBoxCompany.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial:
                    {
                        paramsText.Append(basepage.GlbUserComCode + seperator + basepage.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        //paramsText.Append(TextBoxMain.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.WHAREHOUSE:
                    {
                        paramsText.Append(basepage.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        // paramsText.Append(TextBoxMain.Text + seperator + TextBoxSub.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Inventory_Tracker:
                    {
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
    }
}