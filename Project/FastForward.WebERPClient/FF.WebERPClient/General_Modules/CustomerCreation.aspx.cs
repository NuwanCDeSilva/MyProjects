using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;
using System.Data;
using System.Transactions;
using System.Text;
using System.Drawing;

namespace FF.WebERPClient.General_Modules
{
    public partial class CustomerCreation : BasePage
    {
        public class BusinessEntityTYPE
        {
            //this object stores all types 
            private string typeCD_;

            public string TypeCD_
            {
                get { return typeCD_; }
                set { typeCD_ = value; }
            }
            private string typeDesctipt;

            public string TypeDesctipt
            {
                get { return typeDesctipt; }
                set { typeDesctipt = value; }
            }
            private Boolean isMandatory;

            public Boolean IsMandatory
            {
                get { return isMandatory; }
                set { isMandatory = value; }
            }
        }

        public class BusinessEntityVAL
        {
            //this object stores all the types and the values.(therefore for the same type there can be more than one values)
            private string type_;

            public string Type_
            {
                get { return type_; }
                set { type_ = value; }
            }
            private string val;

            public string Val
            {
                get { return val; }
                set { val = value; }
            }
        }


        public List<CashGeneralDicountDef> DiscountList
        {
            get { return (List<CashGeneralDicountDef>)ViewState["DiscountList"]; }
            set { ViewState["DiscountList"] = value; }
        }
        //private Int32 temp_discount_seq;

        public Int32 Temp_discount_seq
        {
            get { return (Int32)ViewState["DiscountSeq"]; }
            set { ViewState["DiscountSeq"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            uc_CustomerCreation1.UserControlButtonClicked += new
                   EventHandler(txtHiddenCustCD_TextChanged);
            //-------------------------------------------

            uc_CustomerCreation1.EnableMainButtons(false);


            if (true)//TODO: allow depending on permission
            {
                divPermission.Visible = true;
            }

            if (!IsPostBack)
            {
                txtComCode.Text = GlbUserComCode;

                txtItemCD.Attributes.Add("onKeyup", "return clickButton(event,'" + imgItmSearch.ClientID + "')");
                txtCate1.Attributes.Add("onKeyup", "return clickButton(event,'" + imgCate1Search.ClientID + "')");
                txtCate2.Attributes.Add("onKeyup", "return clickButton(event,'" + imgCate2Search.ClientID + "')");
                txtCate3.Attributes.Add("onKeyup", "return clickButton(event,'" + imgCate3Search.ClientID + "')");
                txtComCode.Attributes.Add("onKeyup", "return clickButton(event,'" + imgbtnComSearch.ClientID + "')");
                txtPB.Attributes.Add("onKeyup", "return clickButton(event,'" + imgPBsearch.ClientID + "')");
                txtPBLvl.Attributes.Add("onKeyup", "return clickButton(event,'" + imgPBLsearch.ClientID + "')");
                txtComCode.Attributes.Add("onKeyup", "return clickButton(event,'" + imgbtnComSearch.ClientID + "')");
                txtDiscPC.Attributes.Add("onKeyup", "return clickButton(event,'" + imgPCsearch.ClientID + "')");

                txtItemCD.Attributes.Add("onblur", "return onblurFire(event,'" + btnItmCD.ClientID + "')");

                DiscountList = new List<CashGeneralDicountDef>();
                Temp_discount_seq = 0;
                DataTable datasource2 = CHNLSVC.General.GetSalesTypes("",null,null);
                foreach (DataRow dr in datasource2.Rows)
                {

                    ddlSalesTp.Items.Add(new ListItem(dr["srtp_cd"].ToString() + HttpUtility.HtmlDecode(AddHtmlSpaces(7 - dr["srtp_cd"].ToString().Length)) + "-" + dr["srtp_desc"].ToString(), dr["srtp_cd"].ToString()));
                }

                //----------------------------------------------------------------------------------------------------------
                CustomerCreationUC CUST = new CustomerCreationUC();
                DataTable dt = new DataTable();
                dt = CUST.GetBuizEntityTypes("C");
                List<BusinessEntityTYPE> bindtypeList = new List<BusinessEntityTYPE>();


                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow r in dt.Rows)
                    {
                        // Get the value of the wanted column and cast it to string

                        string TP = Convert.ToString(r["RBT_TP"]);
                        string DESC = Convert.ToString(r["RBT_DESC"]); //rbv_val
                        Boolean isMandetory = Convert.ToBoolean(r["RBT_MAD"]);
                        BusinessEntityTYPE bizTP = new BusinessEntityTYPE();
                        bizTP.IsMandatory = isMandetory;
                        bizTP.TypeCD_ = TP;
                        bizTP.TypeDesctipt = DESC;

                        bindtypeList.Add(bizTP);

                    }
                }
                grvCustSegmentation.DataSource = bindtypeList;
                grvCustSegmentation.DataBind();
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
        protected void grvCustSegmentation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            CustomerCreationUC CUST = new CustomerCreationUC();


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlTypeVal = (DropDownList)e.Row.FindControl("ddlgrvTypeVal");
                Label typeName = (Label)e.Row.FindControl("lblgrvTypeName");
                Label isMand = (Label)e.Row.FindControl("lblgrvIsMand");
                CheckBox chekMand = (CheckBox)e.Row.FindControl("checkIsMandetory");
                if (isMand.Text.Trim().ToUpper() == "TRUE")
                {
                    chekMand.Checked = true;
                    Color myColor = Color.PaleVioletRed;
                    //  chekMand.ForeColor = myColor;
                    chekMand.BackColor = myColor; //only this works for a dissabled checkbox

                    //   chekMand.BorderColor = myColor;

                    isMand.Text = "";
                }
                else
                {
                    chekMand.Checked = false;
                    isMand.Text = "";
                }

                DataTable dtVal = new DataTable();
                dtVal = CUST.GetBuizEntityTypesValues("C", typeName.Text);

                List<string> ddlBindList = new List<string>();

                if (dtVal.Rows.Count > 0)
                {
                    ddlBindList.Add(string.Empty);
                    foreach (DataRow r in dtVal.Rows)
                    {
                        ddlBindList.Add(Convert.ToString(r["RBV_VAL"]));

                    }
                }
                ddlTypeVal.DataSource = ddlBindList;
                ddlTypeVal.DataBind();
                ddlTypeVal.SelectedValue = string.Empty;


            }
        }

        protected void btn_CREATE_Click(object sender, EventArgs e)
        {
            try
            {
                if (uc_CustomerCreation1.CustType == "Individual")
                {
                    if (uc_CustomerCreation1.DOB.Trim() == string.Empty)
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Birthday!");
                        return;
                    }
                    Convert.ToDateTime(uc_CustomerCreation1.DOB.Trim());

                    if (uc_CustomerCreation1.NIC.Trim() == "" && uc_CustomerCreation1.PPNo.Trim() == "" && uc_CustomerCreation1.DL.Trim() == "")
                    {
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Need to fill either NIC or Passport or DL number!");
                        return;
                    }
                    else if (uc_CustomerCreation1.NIC.Trim() != "")
                    {
                            CustomerCreationUC CUST_ = new CustomerCreationUC();
                            Boolean isValid = CUST_.IsValidNIC(uc_CustomerCreation1.NIC.ToUpper());
                            if (isValid == false)
                            {
                                string Msg = "<script>alert('Invalid NIC number!' );</script>";
                                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                                return;
                            }
                       
                    }
                }

            }
            catch (Exception ex)
            {

                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter valid Birthday!");
                return;
            }

            if (uc_CustCreationExternalDet1.Addressline1.Trim() == "" && uc_CustCreationExternalDet1.Addressline2 == "")
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Need to fill address!");
                return;
            }

            if (uc_CustomerCreation1.CustType != "Individual")
            {
                if (uc_CustomerCreation1.BrNo.Trim() == "")
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter BR number!");
                    return;
                }
                //TODO: enter Br no.
                //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Fill all Mandatory fields in the customer segmentation section!");
                //return;
            }

            if (!chekMandotoryAreFilled() && uc_CustomerCreation1.CustType == "Individual")
            {

                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Fill all Mandatory fields in the customer segmentation section!");
                return;
            }
            MasterMsgInfoUCtrl.Clear();

            MasterBusinessEntity custPart1 = new MasterBusinessEntity();
            custPart1 = uc_CustomerCreation1.GetMainCustInfor();
            //----------------------------------------------------------
            MasterBusinessEntity custPart2 = new MasterBusinessEntity();
            custPart2 = uc_CustCreationExternalDet1.GetExtraCustDet(); //uc_CustomerCreation1.GetExtraCustDet();
            //----------------------------------------------------------
            MasterBusinessEntity finalCust = new MasterBusinessEntity();
            finalCust = FinalMasterBusinessEntity(custPart1, custPart2);
            //----------------------------------------------------------
            CustomerAccountRef _account = new CustomerAccountRef();
            //_account.Saca_acc_bal 
            _account.Saca_com_cd = GlbUserComCode;
            try
            {
                if (txtCredLimit.Text.Trim() != string.Empty)
                {
                    _account.Saca_crdt_lmt = Convert.ToDecimal(txtCredLimit.Text.Trim());
                }
                else
                {
                    _account.Saca_crdt_lmt = 0;
                }
                //if (_account.Saca_crdt_lmt < 0)
                //{
                //    txtCredLimit.Focus();
                //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter valid credit limit!");
                //    return;
                //}

            }
            catch (Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid credit limit!");
                return;
            }
            _account.Saca_cre_by = GlbUserName;
            _account.Saca_cre_when = DateTime.Now.Date;
            // _account.Saca_cust_cd = _invoiceHeader.Sah_cus_cd;
            _account.Saca_mod_by = GlbUserName;
            _account.Saca_mod_when = DateTime.Now.Date;
            _account.Saca_ord_bal = 0;
            _account.Saca_session_id = GlbUserSessionID;

            //----------------------------------------------------------
            List<MasterBusinessEntityInfo> bisInfoList = new List<MasterBusinessEntityInfo>();
            foreach (GridViewRow gvr in this.grvCustSegmentation.Rows)
            {
                MasterBusinessEntityInfo bisInfo = new MasterBusinessEntityInfo();
                //bisInfo.Mbei_cd
                bisInfo.Mbei_com = GlbUserComCode;
                Label type = (Label)gvr.Cells[2].FindControl("lblgrvTypeName");
                bisInfo.Mbei_tp = type.Text;
                DropDownList ddlVal = (DropDownList)gvr.Cells[3].FindControl("ddlgrvTypeVal");
                bisInfo.Mbei_val = ddlVal.SelectedValue;
                if (!(ddlVal.SelectedValue == string.Empty))
                {
                    bisInfoList.Add(bisInfo);
                }


            }
            CustomerCreationUC CUST = new CustomerCreationUC();
            CUST.HPCustType = "D";
            string custCD = CUST.SaveCustomer(finalCust, _account, bisInfoList);

            uc_CustomerCreation1.CustCode = custCD;
            if (custCD != string.Empty)
            {
                txtComCode.Text = GlbUserComCode;
                btn_CREATE.Enabled = false;
                string Msg = "<script>alert('Profile Created! Customer Code: " + custCD + "' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            else
            {
                string Msg = "<script>alert('Profile Not Created! Error occured due to wrong data.' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }

            //  string Ms = "<script>alert('TESTING MESSAGE' );</script>";
            //  ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Ms, false);


            List<CashGeneralDicountDef> saveDiscountList = new List<CashGeneralDicountDef>();
            foreach (CashGeneralDicountDef disc in DiscountList)
            {
                //if (disc.Sgdd_pc == "All")
                //{
                //    // CHNLSVC.Sales.GetAllProfCenters(GlbUserComCode);
                //    //CHNLSVC.Sales.GetAllProfCenters(txtComCode.Text.Trim());

                //}
                //else
                //{
                //    saveDiscountList.Add(disc);
                //}
                saveDiscountList.Add(disc);
            }

            try
            {
                if (DiscountList.Count > 0)
                {
                    Int32 effect = 0;
                    using (TransactionScope _tr = new TransactionScope())
                    {
                        foreach (CashGeneralDicountDef disc in saveDiscountList)
                        {
                            disc.Sgdd_cust_cd = custCD;
                            if (disc.Sgdd_pc == "All")
                            {
                                List<string> pclist = new List<string>();
                                pclist = CHNLSVC.Sales.GetAllProfCenters(disc.Sgdd_com);
                                foreach (string pc in pclist)
                                {
                                    List<string> pclist_ = new List<string>();
                                    pclist_.Add(pc);
                                    effect = CHNLSVC.Sales.SaveBusinessEntityDiscount(disc, pclist_);
                                }

                            }
                            else
                            {
                                List<string> pclist = new List<string>();
                                pclist.Add(disc.Sgdd_pc);

                                effect = CHNLSVC.Sales.SaveBusinessEntityDiscount(disc, pclist);
                            }

                        }
                        _tr.Complete();
                    }
                    if (effect < 1)
                    {
                        string Msgg = "<script>alert('No entries gone to discount table' );</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);
                        MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "No entries gone to discount table!");
                        return;

                    }
                    else
                    {
                        string Msg_ = "<script>alert('Discounts inserted successfully' );</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg_, false);
                    }
                    //Int32 effect = CHNLSVC.Sales.SaveBusinessEntityDiscount(DiscountList[0], txtDiscPC.Text.Trim());
                }

            }
            catch (Exception ex)
            {
                string Msgg = "<script>alert('no entries gone to discount table' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Discounts were not added!");
                return;
            }

        }
        protected Boolean chekMandotoryAreFilled()
        {
            foreach (GridViewRow gvr in grvCustSegmentation.Rows)
            {
                DropDownList ddl = (DropDownList)gvr.Cells[3].FindControl("ddlgrvTypeVal");
                Label ismand = (Label)gvr.Cells[0].FindControl("lblgrvIsMandVal");
                Label type = (Label)gvr.Cells[2].FindControl("lblgrvTypeName");
                if (ismand.Text.ToUpper() == "TRUE" && ddl.SelectedValue == string.Empty)
                {
                    return false;
                }

            }
            return true;
        }
        protected MasterBusinessEntity FinalMasterBusinessEntity(MasterBusinessEntity custPart1, MasterBusinessEntity custPart2)
        {
            MasterBusinessEntity customer = new MasterBusinessEntity();
            customer = custPart2;
            customer.Mbe_com = custPart1.Mbe_com;
            customer.Mbe_act = custPart1.Mbe_act;
            customer.Mbe_name = custPart1.Mbe_name;
            customer.Mbe_nic = custPart1.Mbe_nic;
            customer.Mbe_sub_tp = custPart1.Mbe_sub_tp;
            customer.Mbe_mob = custPart1.Mbe_mob;
            customer.Mbe_tp = custPart1.Mbe_tp;
            customer.Mbe_pp_no = custPart1.Mbe_pp_no;
            customer.Mbe_sex = custPart1.Mbe_sex;
            customer.Mbe_cate = custPart1.Mbe_cate;
            customer.Mbe_cre_dt = custPart1.Mbe_cre_dt;
            customer.Mbe_dl_no = custPart1.Mbe_dl_no;
            customer.Mbe_dob = custPart1.Mbe_dob;

            customer.Mbe_agre_send_sms = custPart1.Mbe_agre_send_sms;
            customer.Mbe_br_no = custPart1.Mbe_br_no;
            customer.Mbe_ho_stus = ddlHO_status.SelectedValue;
            customer.Mbe_pc_stus = ddlSH_status.SelectedValue;

            //--------------------------------------

            return customer;
        }

        protected void btn_UPDATE_Click(object sender, EventArgs e)
        {
            MasterBusinessEntity custPart1 = new MasterBusinessEntity();
            custPart1 = uc_CustomerCreation1.GetMainCustInfor();

            MasterBusinessEntity custPart2 = new MasterBusinessEntity();
            custPart2 = uc_CustCreationExternalDet1.GetExtraCustDet(); //uc_CustomerCreation1.GetExtraCustDet();

            MasterBusinessEntity finalCust = new MasterBusinessEntity();
            finalCust = FinalMasterBusinessEntity(custPart1, custPart2);
            finalCust.Mbe_cd = uc_CustomerCreation1.CustCode;
            finalCust.Mbe_com = GlbUserComCode;

            List<MasterBusinessEntityInfo> bisInfoList = new List<MasterBusinessEntityInfo>();
            foreach (GridViewRow gvr in this.grvCustSegmentation.Rows)
            {
                MasterBusinessEntityInfo bisInfo = new MasterBusinessEntityInfo();
                //bisInfo.Mbei_cd
                bisInfo.Mbei_com = GlbUserComCode;
                Label type = (Label)gvr.Cells[2].FindControl("lblgrvTypeName");
                bisInfo.Mbei_tp = type.Text;
                DropDownList ddlVal = (DropDownList)gvr.Cells[3].FindControl("ddlgrvTypeVal");
                bisInfo.Mbei_val = ddlVal.SelectedValue;
                if (!(ddlVal.SelectedValue == string.Empty))
                {
                    bisInfoList.Add(bisInfo);
                }


            }
            CustomerCreationUC CUST = new CustomerCreationUC();
            Int32 effect = CUST.UpdateCustomer(finalCust, Convert.ToDecimal(txtCredLimit.Text.Trim()), bisInfoList);

            if (effect >= 0)
            {

                string Msg = "<script>alert('Profile Updated! (Customer Code:" + finalCust.Mbe_cd + ")' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
                List<CashGeneralDicountDef> saveDiscountList = new List<CashGeneralDicountDef>();
                foreach (CashGeneralDicountDef disc in DiscountList)
                {
                    saveDiscountList.Add(disc);
                }

                try
                {
                    if (DiscountList.Count > 0)
                    {
                        Int32 effect_ = 0;
                        using (TransactionScope _tr = new TransactionScope())
                        {
                            foreach (CashGeneralDicountDef disc in saveDiscountList)
                            {
                                disc.Sgdd_cust_cd = uc_CustomerCreation1.CustCode;//custCD;
                                if (disc.Sgdd_pc == "All")
                                {
                                    List<string> pclist = new List<string>();
                                    pclist = CHNLSVC.Sales.GetAllProfCenters(disc.Sgdd_com);
                                    foreach (string pc in pclist)
                                    {
                                        List<string> pclist_ = new List<string>();
                                        pclist_.Add(pc);
                                        effect_ = CHNLSVC.Sales.SaveBusinessEntityDiscount(disc, pclist_);
                                    }

                                }
                                else
                                {
                                    List<string> pclist = new List<string>();
                                    pclist.Add(disc.Sgdd_pc);

                                    effect_ = CHNLSVC.Sales.SaveBusinessEntityDiscount(disc, pclist);
                                }

                            }
                            _tr.Complete();
                        }
                        if (effect_ < 1)
                        {
                            string Msgg = "<script>alert('No entries gone to discount table' );</script>";
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);
                            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "No entries gone to discount table!");
                            return;

                        }
                        else
                        {
                            string Msg_ = "<script>alert('Discounts inserted successfully' );</script>";
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg_, false);
                        }

                    }

                }
                catch (Exception ex)
                {
                    string Msgg = "<script>alert('no entries gone to discount table' );</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msgg, false);
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Discounts were not added!");
                    return;
                }
            }
            else
            {
                string Msg = "<script>alert('Failed To Update!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
        }

        protected void txtHiddenCustCD_TextChanged(object sender, EventArgs e)
        {
            //SET VALUES IN THE PAGE
            CustomerCreationUC CUST = new CustomerCreationUC();
            MasterBusinessEntity custProf = CUST.GetbyCustCD(uc_CustomerCreation1.CustCode);
            ddlHO_status.SelectedValue = custProf.Mbe_ho_stus;
            ddlSH_status.SelectedValue = custProf.Mbe_pc_stus;

            uc_CustCreationExternalDet1.SetExtraValues(custProf);

            CustomerAccountRef custAccRef = CHNLSVC.Sales.GetCustomerAccount(GlbUserComCode, uc_CustomerCreation1.CustCode);
            txtCredLimit.Text = string.Format("{0:n2}", custAccRef.Saca_crdt_lmt);

            if (uc_CustomerCreation1.CustCode == "")
            {
                btn_CREATE.Enabled = true;
            }
            else
            {
                btn_CREATE.Enabled = false;
            }
            


        }

        protected void btn_CLOSE_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        protected void btn_CLEAR_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/General_Modules/CustomerCreation.aspx", false);
        }

        protected void TextBox8_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox5_TextChanged1(object sender, EventArgs e)
        {

        }

        protected void AddToCredList()
        {

            if (txtDiscPC.Text.Trim() == "" || txtDiscPC.Text.Trim() == string.Empty)
            {
                string Msg = "<script>alert('Please enter Profit center!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            if (txtComCode.Text.Trim() == "" || txtComCode.Text.Trim() == string.Empty)
            {
                string Msg = "<script>alert('Please enter company !' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            if (txtPB.Text.Trim() == "" || txtPB.Text.Trim() == string.Empty)
            {
                string Msg = "<script>alert('Please enter Price Book!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            if (txtPBLvl.Text.Trim() == "" || txtPBLvl.Text.Trim() == string.Empty)
            {
                string Msg = "<script>alert('Please enter Price Book Level!' );</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);
            }
            CashGeneralDicountDef cd = new CashGeneralDicountDef();
            cd.Sgdd_com = GlbUserComCode;

            if (chkAllPC.Checked)
            {
                cd.Sgdd_pc = "All";
            }
            else
            {
                cd.Sgdd_pc = txtDiscPC.Text.Trim();
            }

            cd.Sgdd_sale_tp = ddlSalesTp.SelectedValue;
            cd.Sgdd_pb = txtPB.Text.Trim();
            cd.Sgdd_pb_lvl = txtPBLvl.Text.Trim();
            cd.Sgdd_alw_pro = chkAllowProm.Checked;
            cd.Sgdd_alw_ser = chkAllowSer.Checked;
            if (txtBrand.Text.Trim().ToUpper() == "N/A")
            {
                cd.Sgdd_brand = string.Empty;
            }
            else
            {
                cd.Sgdd_brand = txtBrand.Text.Trim(); //ddlBrand.SelectedValue;
            }

            if (txtCate1.Text.Trim().ToUpper() == "N/A")
            {
                cd.Sgdd_cate1 = string.Empty;
            }
            else
            {
                cd.Sgdd_cate1 = txtCate1.Text.Trim();//ddlCate1.SelectedValue;
            }


            if (txtCate2.Text.Trim().ToUpper() == "N/A")
            {
                cd.Sgdd_cate2 = string.Empty;
            }
            else
            {
                cd.Sgdd_cate2 = txtCate2.Text.Trim();//ddlCate2.SelectedValue;
            }

            if (txtCate3.Text.Trim().ToUpper() == "N/A")
            {
                cd.Sgdd_cate3 = string.Empty;
            }
            else
            {
                cd.Sgdd_cate3 = txtCate3.Text.Trim();//ddlCate3.SelectedValue;
            }


            cd.Sgdd_cre_by = GlbUserName;
            cd.Sgdd_cre_dt = DateTime.Now.Date;
            cd.Sgdd_itm = txtItemCD.Text.Trim();
            cd.Sgdd_stus = true;
            try
            {

                cd.Sgdd_from_dt = Convert.ToDateTime(txtFromDT.Text.Trim());
                cd.Sgdd_to_dt = Convert.ToDateTime(txtToDT.Text.Trim());
            }
            catch (Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid discount dates!");
                return;
            }

            //cd.Sgdd_cust_cd 
            try
            {
                Convert.ToDecimal(txtDiscount.Text.Trim());
            }
            catch (Exception ex)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid discount rate/value!");
                return;
            }
            if (chkIsRate.Checked)
            {
                if (Convert.ToDecimal(txtDiscount.Text.Trim()) > 100)
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Discount rate cannot be grater than 100!");
                    return;
                }
                cd.Sgdd_disc_rt = Convert.ToDecimal(txtDiscount.Text.Trim());
            }
            else
            {
                cd.Sgdd_disc_val = Convert.ToDecimal(txtDiscount.Text.Trim());
            }


            try
            {
                if (txtNoOfcredTimes.Text.Trim() == "")
                {
                    cd.Sgdd_no_of_times = 9999;//4 Digit number
                }
                else
                {
                    cd.Sgdd_no_of_times = Convert.ToInt32(txtNoOfcredTimes.Text.Trim());
                }
            }
            catch (Exception ex)
            {

                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "No. of times for credit is invalid!");
                return;
            }
            Temp_discount_seq = Temp_discount_seq + 1;
            cd.Sgdd_seq = Temp_discount_seq;
            DiscountList.Add(cd);

            grvCreditLimDet.DataSource = DiscountList;
            grvCreditLimDet.DataBind();

        }

        protected void btnAddCredit_Click(object sender, EventArgs e)
        {
            AddToCredList();
        }

        protected void chkAllPC_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllPC.Checked)
            {
                txtDiscPC.Enabled = false;
                txtDiscPC.Text = "All";
            }
            else
            {
                txtDiscPC.Enabled = true;
                txtDiscPC.Text = "";

            }
        }


        #region Searchin
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
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {

                        paramsText.Append(txtCate1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator);
                        break;

                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {

                        paramsText.Append(txtCate1.Text + seperator + txtCate2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {

                        paramsText.Append(GlbUserComCode + seperator + txtPB.Text.Trim() + seperator);
                        // paramsText.Append(GlbUserComCode + seperator + "%" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {

                        paramsText.Append(txtComCode.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(txtComCode.Text.Trim() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion
        protected void imgItmSearch_Click(object sender, ImageClickEventArgs e)
        {

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable dataSource = CHNLSVC.CommonSearch.GetItemSearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtItemCD.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }
        protected void imgBrandSearch_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void imgCate1Search_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCat_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtCate1.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();

        }

        protected void imgCate2Search_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCat_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtCate2.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgCate3Search_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCat_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtCate3.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void txtCate1_TextChanged(object sender, EventArgs e)
        {
            txtCate2.Text = "";
            txtCate3.Text = "";
        }

        protected void grvCreditLimDet_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToUpper())
            {
                case "DELETEITEM":
                    {

                        ImageButton imgbtndelSerial = (ImageButton)e.CommandSource;
                        string _selectedItemDetails = imgbtndelSerial.CommandArgument;
                        string[] arr = _selectedItemDetails.Split(new string[] { "|" }, StringSplitOptions.None);

                        // if (arr[0].Trim() == dis.Sgdd_com && arr[1].Trim() == dis.Sgdd_pc && arr[2].Trim() == dis.Sgdd_sale_tp)

                        DiscountList.RemoveAll((x) => x.Sgdd_seq == Convert.ToInt32(arr[0].Trim()));

                        grvCreditLimDet.DataSource = DiscountList;
                        grvCreditLimDet.DataBind();
                        break;
                    }
            }
        }

        protected void txtCate2_TextChanged(object sender, EventArgs e)
        {
            txtCate3.Text = "";

            if (txtCate1.Text.Trim() == "" || txtCate1.Text.Trim() == string.Empty)
            {
                txtCate2.Text = "";
            }

        }

        protected void txtCate3_TextChanged(object sender, EventArgs e)
        {
            if (txtCate2.Text.Trim() == "" || txtCate2.Text.Trim() == string.Empty)
            {
                txtCate3.Text = "";
            }
        }

        protected void imgbtnComSearch_Click(object sender, ImageClickEventArgs e)
        {

            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
            DataTable dataSource = CHNLSVC.CommonSearch.GetCompanySearchData(MasterCommonSearchUCtrl.SearchParams, null, null);
            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtComCode.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgPBsearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(MasterCommonSearchUCtrl.SearchParams, null, null);


            //MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBook);
            //DataTable dataSource = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPB.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgPBLsearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPriceLevelByBookData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPBLvl.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();
        }

        protected void imgPCsearch_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtDiscPC.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();

        }

        protected void btnItmCD_Click(object sender, EventArgs e)
        {
            MasterItem mstItm = new MasterItem();
            mstItm = CHNLSVC.Inventory.GetItem(GlbUserComCode, txtItemCD.Text.Trim().ToUpper());
            if (mstItm != null)
            {
                txtBrand.Text = mstItm.Mi_brand;
                txtCate1.Text = mstItm.Mi_cate_1;
                txtCate2.Text = mstItm.Mi_cate_2;
                txtCate3.Text = mstItm.Mi_cate_3;


                // Decimal VAT_RATE = CHNLSVC.Sales.GET_Item_vat_Rate(GlbUserComCode, mstItm.Mi_cd, "VAT");
                // lblVatRate.Text = VAT_RATE.ToString() + "%";

            }
            else
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Incorrect item code!");
                return;
            }
        }




    }
}