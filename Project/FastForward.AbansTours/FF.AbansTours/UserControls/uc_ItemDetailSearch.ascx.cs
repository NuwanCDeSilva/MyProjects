using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace FF.AbansTours.UserControls
{
    public partial class uc_ItemDetailSearch : System.Web.UI.UserControl
    {
        BasePage _basePage = null;

        #region properties

        public List<string> ITEM
        {
            get { return (List<string>)ViewState["ITEM"]; }
            set { ViewState["ITEM"] = value; }
        }

        public string BRAND
        {
            get { return (string)ViewState["BRAND"]; }
            set { ViewState["BRAND"] = value; }
        }

        public string CAT_1
        {
            get { return (string)ViewState["CAT_1"]; }
            set { ViewState["CAT_1"] = value; }
        }

        public string CAT_2
        {
            get { return (string)ViewState["CAT_2"]; }
            set { ViewState["CAT_2"] = value; }
        }

        public string CAT_3
        {
            get { return (string)ViewState["CAT_3"]; }
            set { ViewState["CAT_3"] = value; }
        }

        public List<string> PROMOTION
        {
            get { return (List<string>)ViewState["PROMOTION"]; }
            set { ViewState["PROMOTION"] = value; }
        }

        public string CIRCULAR_NO
        {
            get { return (string)ViewState["CIRCULAR_NO"]; }
            set { ViewState["CIRCULAR_NO"] = value; }
        }

        public List<string> SERIAL
        {
            get { return (List<string>)ViewState["SERIAL"]; }
            set { ViewState["SERIAL"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                SERIAL = new List<string>();
                ITEM = new List<string>();
                PROMOTION = new List<string>();
                BRAND = "";
                CAT_1 = "";
                CAT_2 = "";
                CAT_3 = "";
                CIRCULAR_NO = "";
                GridViewSerial.Visible = false;
                GridViewPromotion.Visible = false;
                GridViewItem.Visible = false;
            }
        }

        protected void ImageButtonBrand_Click(object sender, ImageClickEventArgs e)
        {
            _basePage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
            DataTable dataSource = _basePage.CHNLSVC.CommonSearch.GetCat_SearchData(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = TextBoxBrand.ClientID;
            ucc.UCModalPopupExtender.Show();
            CheckBoxBrand.Checked = true;
        }

        protected void ImageButtonMainCategory_Click(object sender, ImageClickEventArgs e)
        {
            _basePage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc =(uc_CommonSearch) pp.Master.FindControl("uc_CommonSearchMaster");

            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            DataTable dataSource = _basePage.CHNLSVC.CommonSearch.GetCat_SearchData(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = TextBoxMainCategory.ClientID;
            ucc.UCModalPopupExtender.Show();
            CheckBoxMainCategory.Checked = true;
        }

        protected void ImageButtonSubCategory_Click(object sender, ImageClickEventArgs e)
        {
            _basePage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
            DataTable dataSource = _basePage.CHNLSVC.CommonSearch.GetCat_SearchData(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = TextBoxSubCategory.ClientID;
            ucc.UCModalPopupExtender.Show();
            CheckBoxSubCategory.Checked = true;
        }

        protected void ImageButtonSubCategory1_Click(object sender, ImageClickEventArgs e)
        {
             _basePage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc =(uc_CommonSearch) pp.Master.FindControl("uc_CommonSearchMaster");

            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
            DataTable dataSource =_basePage.CHNLSVC.CommonSearch.GetCat_SearchData(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = TextBoxSubCategory1.ClientID;
            ucc.UCModalPopupExtender.Show();
            CheckBoxSubCategory1.Checked = true;
        }

        protected void ImageButtonProduct_Click(object sender, ImageClickEventArgs e)
        {
            _basePage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable dataSource =_basePage.CHNLSVC.CommonSearch.GetItemSearchData(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource);
            ucc.BindUCtrlGridData(dataSource);
            ucc.ReturnResultControl = TextBoxProduct.ClientID;
            ucc.UCModalPopupExtender.Show();
            CheckBoxProduct.Checked=true;
        }

        protected void ImageButtonSerial_Click(object sender, ImageClickEventArgs e)
        {
            _basePage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
            DataTable dataSource1 = _basePage.CHNLSVC.CommonSearch.GetItemSerialSearchData(ucc.SearchParams);
            ucc.BindUCtrlDDLData(dataSource1);
            ucc.BindUCtrlGridData(dataSource1);
            ucc.ReturnResultControl = TextBoxSerial.ClientID;
            ucc.UCModalPopupExtender.Show();
            CheckBoxSerial.Checked = true;
        }

        #region Common Search Methods
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            _basePage = new BasePage();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(_basePage.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator+""+seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator+CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator+TextBoxBrand.Text+seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(TextBoxMainCategory.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator+TextBoxBrand.Text+seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(TextBoxMainCategory.Text + seperator + TextBoxSubCategory.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator+TextBoxBrand.Text+seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item_Serials:
                    {
                        paramsText.Append(TextBoxProduct.Text + seperator + "" + seperator + _basePage.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "Loc" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Circular:
                    {
                        paramsText.Append("" + seperator + "Circular" + seperator );
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Promotion:
                    {
                        paramsText.Append(TextBoxCircular.Text + seperator + "Promotion" + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }
        #endregion

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            _basePage = new BasePage();

            if (CheckBoxSerial.Checked)
            {
                SERIAL = new List<string>();
                if (TextBoxSerial.Text == "")
                {
                    Panel13.Visible = true;
                    LabelMessage.Visible = false;
                    DataTable dt = _basePage.CHNLSVC.Inventory.GetSerialByItem(TextBoxBrand.Text, TextBoxMainCategory.Text, TextBoxSubCategory.Text, TextBoxSubCategory1.Text, TextBoxProduct.Text);
                    GridViewSerial.DataSource = dt;
                    GridViewSerial.DataBind();
                    GridViewSerial.Visible = true;
                    GridViewPromotion.Visible = false;
                    GridViewItem.Visible = false;
                    foreach (DataRow dr in dt.Rows)
                    {
                        SERIAL.Add(dr["INS_SER_1"].ToString());
                    }
                }
                else
                {
                    GridViewSerial.Visible = false;
                    GridViewPromotion.Visible = false;
                    GridViewItem.Visible = false;
                    SERIAL.Add(TextBoxSerial.Text);
                    LabelMessage.Text = "Brand - " + BRAND + "<br>" + "Main Category - " + CAT_1 +
                                             "<br>" + "Sub Category - " + CAT_2 + "<br>" + "Sub Category1 - " + CAT_3 +
                                             "<br>" + "Item - " + ITEM + "<br>" + "Serial - " + TextBoxSerial.Text;
                }
            }

            else if (CheckBoxProduct.Checked)
            {
                ITEM = new List<string>();
                if (TextBoxProduct.Text == "")
                {
                    Panel13.Visible = true;
                    LabelMessage.Visible = false;
                    DataTable dt = _basePage.CHNLSVC.Inventory.GetItemByAll(TextBoxBrand.Text, TextBoxMainCategory.Text, TextBoxSubCategory.Text, TextBoxSubCategory1.Text);
                    GridViewItem.DataSource = dt;
                    GridViewItem.DataBind();
                    GridViewSerial.Visible = false;
                    GridViewPromotion.Visible = false;
                    GridViewItem.Visible = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        ITEM.Add(dr["MI_CD"].ToString());
                    }
                }
                else
                {
                    Panel13.Visible = false;
                    ITEM.Add(TextBoxProduct.Text);
                    LabelMessage.Text = "Brand - " + BRAND + "<br>" + "Main Category - " + CAT_1 +
                                         "<br>" + "Sub Category - " + CAT_2 + "<br>" + "Sub Category1 - " + CAT_3 +
                                         "<br>" + "Item - " + TextBoxProduct.Text;
                }
            }
            else if (CheckBoxSubCategory1.Checked)
            {
                BRAND = TextBoxBrand.Text;
                CAT_1 = TextBoxMainCategory.Text;
                CAT_2 = TextBoxSubCategory.Text;
                CAT_3 = TextBoxSubCategory1.Text;
                Panel13.Visible = false;
                LabelMessage.Visible = true;
                LabelMessage.Text = "Brand - " + BRAND + "<br>" + "Main Category - " + CAT_1 +
                                     "<br>" + "Sub Category - " + CAT_2 + "<br>" + "Sub Category1 - " + CAT_3;
            }
            else if (CheckBoxSubCategory.Checked)
            {
                BRAND = TextBoxBrand.Text;
                CAT_1 = TextBoxMainCategory.Text;
                CAT_2 = TextBoxSubCategory.Text;
                Panel13.Visible = false;
                LabelMessage.Visible = true;
                LabelMessage.Text = "Brand - " + BRAND + "<br>" + "Main Category - " + CAT_1 +
                                    "<br>" + "Sub Category - " + CAT_2;
            }
            else if (CheckBoxMainCategory.Checked)
            {
                BRAND = TextBoxBrand.Text;
                CAT_1 = TextBoxMainCategory.Text;
                Panel13.Visible = false;
                LabelMessage.Visible = true;
                LabelMessage.Text = "Brand - " + BRAND + "<br>" + "Main Category - " + CAT_1;
            }
            else
            {
                BRAND = TextBoxBrand.Text;
                Panel13.Visible = false;
                LabelMessage.Visible = true;
                LabelMessage.Text = "Brand - " + BRAND;
            }

            if (CheckBoxPromation.Checked)
            {
                PROMOTION = new List<string>();
                if (TextBoxPromotion.Text == "")
                {
                    _basePage = new BasePage();
                    Page pp = (Page)this.Page;
                    uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");
                    Panel13.Visible = true;
                    LabelMessage.Visible = false;
                    ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
                    DataTable dt = _basePage.CHNLSVC.CommonSearch.GetPromotionSearch(ucc.SearchParams, null, null);
                    GridViewPromotion.DataSource = dt;
                    GridViewPromotion.DataBind();
                    GridViewSerial.Visible = false;
                    GridViewPromotion.Visible = true;
                    GridViewItem.Visible = false;
                    foreach (DataRow dr in dt.Rows)
                    {
                        PROMOTION.Add(dr["Code"].ToString());
                    }
                }
                else
                {
                    Panel13.Visible = false;
                    PROMOTION.Add(TextBoxPromotion.Text);
                    LabelMessage.Text = "Circular - " + CIRCULAR_NO + "<br>" + "Promotion - " + TextBoxPromotion.Text ;
                }
            }
            else if (CheckBoxCircular.Checked)
            {
                CIRCULAR_NO = TextBoxCircular.Text;
                Panel13.Visible = false;
                LabelMessage.Visible = true;
                LabelMessage.Text = "Circular - " + CIRCULAR_NO;
            }

        }

        protected void CheckBoxBrand_CheckedChanged(object sender, EventArgs e)
        {
            if (!CheckBoxBrand.Checked)
                TextBoxBrand.Text = "";
        }

        protected void CheckBoxMainCategory_CheckedChanged(object sender, EventArgs e)
        {
            if (!CheckBoxMainCategory.Checked) {
                TextBoxMainCategory.Text = "";
            }
        }

        protected void CheckBoxSubCategory_CheckedChanged(object sender, EventArgs e)
        {
            if (!CheckBoxSubCategory.Checked) {
                TextBoxSubCategory.Text = "";
            }
        }

        protected void CheckBoxSubCategory1_CheckedChanged(object sender, EventArgs e)
        {
            if (!CheckBoxSubCategory1.Checked) {
                TextBoxSubCategory1.Text = "";
            }
        }


        protected void CheckBoxProduct_CheckedChanged(object sender, EventArgs e)
        {
            if (!CheckBoxProduct.Checked) {
                TextBoxProduct.Text = "";
            }
        }

        protected void CheckBoxSerial_CheckedChanged(object sender, EventArgs e)
        {
            if (!CheckBoxSerial.Checked) {
                TextBoxSerial.Text = "";
            }
        }

        protected void CheckBoxCircular_CheckedChanged(object sender, EventArgs e)
        {
            if (!CheckBoxCircular.Checked)
            {
                ClearItem();
                TextBoxCircular.Text = "";
            }
        }

        protected void CheckBoxPromation_CheckedChanged1(object sender, EventArgs e)
        {
            if (!CheckBoxPromation.Checked) {
                ClearItem();
                TextBoxPromotion.Text = "";
            }
        }

        protected void ImageButtonCircular_Click(object sender, ImageClickEventArgs e)
        {
            ClearItem();
            _basePage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Circular);
            DataTable dataSource1 = _basePage.CHNLSVC.CommonSearch.GetPromotionSearch(ucc.SearchParams,null,null);
            ucc.BindUCtrlDDLData(dataSource1);
            ucc.BindUCtrlGridData(dataSource1);
            ucc.ReturnResultControl = TextBoxCircular.ClientID;
            ucc.UCModalPopupExtender.Show();
            CheckBoxCircular.Checked = true;
        }

        private void ClearItem()
        {
            TextBoxProduct.Text = "";
            TextBoxBrand.Text = "";
            TextBoxMainCategory.Text = "";
            TextBoxSubCategory.Text = "";
            TextBoxSubCategory1.Text = "";
            SERIAL = new List<string>();
            ITEM = new List<string>();
        }

        protected void ImageButtonPromotion_Click1(object sender, ImageClickEventArgs e)
        {
            ClearItem();
            _basePage = new BasePage();
            Page pp = (Page)this.Page;
            uc_CommonSearch ucc = (uc_CommonSearch)pp.Master.FindControl("uc_CommonSearchMaster");

            ucc.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Promotion);
            DataTable dataSource1 = _basePage.CHNLSVC.CommonSearch.GetPromotionSearch(ucc.SearchParams, null, null);
            ucc.BindUCtrlDDLData(dataSource1);
            ucc.BindUCtrlGridData(dataSource1);
            ucc.ReturnResultControl = TextBoxPromotion.ClientID;
            ucc.UCModalPopupExtender.Show();
            CheckBoxPromation.Checked = true;
        }

        protected void ButtonAll_Click(object sender, EventArgs e)
        {
            if (GridViewItem.Visible)
            {
                foreach (GridViewRow gvr in GridViewItem.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                    chkSelect.Checked = true;
                    ITEM.Add(gvr.Cells[1].Text);
                }
            }
            else if (GridViewSerial.Visible)
            {
                foreach (GridViewRow gvr in GridViewSerial.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                    chkSelect.Checked = true;
                    SERIAL.Add(gvr.Cells[2].Text);
                }
            }
            else {
                foreach (GridViewRow gvr in GridViewPromotion.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                    chkSelect.Checked = true;
                    PROMOTION.Add(gvr.Cells[1].Text);
                }
            }
        }

        protected void ButtonNone_Click(object sender, EventArgs e)
        {
            if (GridViewItem.Visible)
            {
                foreach (GridViewRow gvr in GridViewItem.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                    chkSelect.Checked = false;

                }
                ITEM = new List<string>();
            }
            else if (GridViewSerial.Visible)
            {
                foreach (GridViewRow gvr in GridViewSerial.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                    chkSelect.Checked = false;

                }
                SERIAL = new List<string>();
            }
            else
            {
                foreach (GridViewRow gvr in GridViewPromotion.Rows)
                {
                    CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
                    chkSelect.Checked = false;
                    PROMOTION.RemoveAt(gvr.RowIndex);
                }
                PROMOTION = new List<string>();
            }
        }

        protected void ButtonClearPc_Click(object sender, EventArgs e)
        {
            if (GridViewItem.Visible)
            {
                GridViewItem.DataSource = null;
                GridViewItem.DataBind();
                ITEM = new List<string>();
            }
            else if (GridViewSerial.Visible)
            {
                GridViewSerial.DataSource = null;
                GridViewSerial.DataBind();
                SERIAL = new List<string>();
            }
            else
            {
                GridViewPromotion.DataSource = null;
                GridViewPromotion.DataBind();
                PROMOTION = new List<string>();
            }
        }

        protected void chekPc_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow gvr = (GridViewRow)(((CheckBox)sender).NamingContainer);
            CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
            if (!chkSelect.Checked)
            {
                ITEM.RemoveAt(gvr.RowIndex);
            }
            else {
                ITEM.Add(gvr.Cells[1].Text);
            }
        }

        protected void chekPc_CheckedChanged1(object sender, EventArgs e)
        {
            GridViewRow gvr = (GridViewRow)(((CheckBox)sender).NamingContainer);
            CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
            if (!chkSelect.Checked)
            {
                SERIAL.RemoveAt(gvr.RowIndex);
            }
            else
            {
                SERIAL.Add(gvr.Cells[2].Text);
            }
        }

        protected void chekPc_CheckedChanged2(object sender, EventArgs e)
        {
            GridViewRow gvr = (GridViewRow)(((CheckBox)sender).NamingContainer);
            CheckBox chkSelect = (CheckBox)gvr.Cells[1].FindControl("chekPc");
            if (!chkSelect.Checked)
            {
                PROMOTION.RemoveAt(gvr.RowIndex);
            }
            else
            {
                PROMOTION.Add(gvr.Cells[1].Text);
            }
        }
    }
}