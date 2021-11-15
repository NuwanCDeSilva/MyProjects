using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using FF.BusinessObjects;
using FF.WebERPClient.UserControls;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace FF.WebERPClient.General_Modules
{
    public partial class ServiceAgentCreation : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindData();
                txtCode.Attributes.Add("onblur", this.Page.ClientScript.GetPostBackEventReference(this.btnCode, ""));
            }
        }

        protected void GetServiceAgentData(object sender, EventArgs e)
        {
            MasterBusinessEntity _agent = null;

            if (txtCode.Text == "")
            {
                txtName.Text = "";
                txtAddr1.Text = "";
                txtAddr2.Text = "";
                txtAddr3.Text = "";
                txtTel.Text = "";
                txtFax.Text = "";
                txtTown.Text = "";
                txtCat.Text = "";
                txtContact.Text = "";
                txtCordinator.Text = "";
                txtMapedLoc.Text = "";
            

                return;
            }
            _agent = CHNLSVC.Sales.GetCustomerProfile(txtCode.Text, null, null, null, null);
            if (_agent != null)
            {
                txtName.Text = _agent.Mbe_name;
                txtAddr1.Text = _agent.Mbe_add1;
                txtAddr2.Text = _agent.Mbe_add2;
                txtAddr3.Text = "";
                txtTel.Text = _agent.Mbe_tel;
                txtFax.Text = _agent.Mbe_fax;
                txtTown.Text = _agent.Mbe_town_cd;
                txtCat.Text = _agent.Mbe_cate;
                txtContact.Text = _agent.Mbe_contact;
                txtCordinator.Text = _agent.Mbe_email;
                txtMapedLoc.Text = _agent.Mbe_cust_loc;


            }
            else
            {
                txtName.Text = "";
                txtAddr1.Text = "";
                txtAddr2.Text = "";
                txtAddr3.Text = "";
                txtTel.Text = "";
                txtFax.Text = "";
                txtTown.Text = "";
                txtCat.Text = "";
                txtContact.Text = "";
                txtCordinator.Text = "";
                txtMapedLoc.Text = "";
            }

        }

        private void bindData()
        {
            ddlStatus.Items.Clear();
            ddlStatus.Items.Add("Active");
            ddlStatus.Items.Add("Inactive");
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/General_Modules/ServiceAgentCreation.aspx", false);
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCode.Text == "")
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please enter code");
            }
            if (txtName.Text == "")
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Please enter name");
            }
            MasterBusinessEntity _customer = new MasterBusinessEntity();
            _customer.Mbe_com = GlbUserComCode;
            _customer.Mbe_cd = txtCode.Text;
            _customer.Mbe_tp = "SA";
            _customer.Mbe_sub_tp = "SA";
            _customer.Mbe_acc_cd = "";
            _customer.Mbe_name = (txtName.Text).ToUpper();
            _customer.Mbe_add1 = txtAddr1.Text;
            _customer.Mbe_add2 = txtAddr2.Text + " " + txtAddr3.Text;
            _customer.Mbe_country_cd = "";
            _customer.Mbe_province_cd = "";
            _customer.Mbe_distric_cd = "";
            _customer.Mbe_town_cd = txtTown.Text;
            _customer.Mbe_tel = txtTel.Text;
            _customer.Mbe_fax = txtFax.Text;
            _customer.Mbe_mob = "";
            _customer.Mbe_nic = "";
            _customer.Mbe_email = txtCordinator.Text;
            _customer.Mbe_contact = txtContact.Text;
            _customer.Mbe_act = (ddlStatus.SelectedValue=="Active" ? true : false);
            _customer.Mbe_tax_no = "";
            _customer.Mbe_cust_loc = txtMapedLoc.Text;
            _customer.Mbe_cate = txtCat.Text;

            int effect = CHNLSVC.Sales.SaveServiceAgentDetail(_customer);

            string Msg = "<script>alert('Successfully Saved!');window.location = 'ServiceAgentCreation.aspx';</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
        }

        protected void imgbtnCode_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceAgent);
            DataTable dataSource = CHNLSVC.CommonSearch.GetServiceAgent(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtCode.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        protected void imgBtnSerCat_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceAgent);
            DataTable dataSource = CHNLSVC.CommonSearch.GetServiceAgent(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtCode.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        protected void imgBtnMapLoc_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceAgent);
            DataTable dataSource = CHNLSVC.CommonSearch.GetServiceAgent(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtCode.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Group_Sale:
                    {
                        paramsText.Append(GlbUserComCode + seperator + GlbUserDefProf + seperator + -1 + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceAgent:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OutsideParty:
                    {
                        paramsText.Append("HP" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Country:
                    {
                        paramsText.Append(seperator);
                        break;
                    }


                default:
                    break;
            }

            return paramsText.ToString();
        }
    }
}