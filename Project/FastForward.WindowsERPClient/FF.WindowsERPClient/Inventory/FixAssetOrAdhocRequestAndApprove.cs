using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Globalization;
using FF.WindowsERPClient.Reports.Sales;

namespace FF.WindowsERPClient.Inventory
{
    public partial class FixAssetOrAdhocRequestAndApprove : Base
    {      
       
        private string gen_ADJ_DocNo;
        public  string fixassetloc;
        public  string validfixassetloc;

        public string Fixassetloc
        {
            get { return fixassetloc; }
            set { fixassetloc = value; }
        }

        public string Validfixassetloc
        {
            get { return validfixassetloc; }
            set { validfixassetloc = value; }
        }

        public string Gen_ADJ_DocNo
        {
            get { return gen_ADJ_DocNo; }
            set { gen_ADJ_DocNo = value; }
        }
       
        private Int32 itmLine;

        public Int32 ItmLine
        {
            get { return itmLine; }
            set { itmLine = value; }
        }
      
        private string selectedItemCD;

        public string SelectedItemCD
        {
            get { return selectedItemCD; }
            set { selectedItemCD = value; }
        }
       
        private List<InventoryAdhocDetail> adhodDetList;

        public List<InventoryAdhocDetail> AdhodDetList
        {
            get { return adhodDetList; }
            set { adhodDetList = value; }
        }
       
        private List<ReptPickSerials> availableSerialList;

        public List<ReptPickSerials> AvailableSerialList
        {
            get { return availableSerialList; }
            set { availableSerialList = value; }
        }
      
        private List<ReptPickSerials> approved_SerialList;

        public List<ReptPickSerials> Approved_SerialList
        {
            get { return approved_SerialList; }
            set { approved_SerialList = value; }
        }
      
        private InventoryAdhocHeader searched_AdhodHeader;

        public InventoryAdhocHeader Searched_AdhodHeader
        {
            get { return searched_AdhodHeader; }
            set { searched_AdhodHeader = value; }
        }

      
        private List<InventoryAdhocDetail> Det_list_selected;

        public List<InventoryAdhocDetail> det_list_selected
        {
            get { return Det_list_selected; }
            set { Det_list_selected = value; }
        }
       
        private List<InventoryAdhocDetail> SearchedAdhocDetList; //Always contain the original requested item detail list

        public List<InventoryAdhocDetail> searchedAdhocDetList
        {
            get { return SearchedAdhocDetList; }
            set { SearchedAdhocDetList = value; }
        }
       
        //------------------**-----------------------------------------------------------------------------------
        private List<HpTransaction> transaction_List;

        public List<HpTransaction> Transaction_List
        {
            get { return transaction_List; }
            set { transaction_List = value; }
        }
        
        private List<RecieptItem> recieptItem;

        public List<RecieptItem> _recieptItem
        {
            get { return recieptItem; }
            set { recieptItem = value; }
        }
        List<PriceDefinitionRef> _PriceDefinitionRef = null;
        //private void LoadCachedObjects()
        //{
        //   MasterProfitCenter _MasterProfitCenter = CacheLayer.Get<MasterProfitCenter>(CacheLayer.Key.ProfitCenter.ToString());
        // List<PriceDefinitionRef> _PriceDefinitionRef = CacheLayer.Get<List<PriceDefinitionRef>>(CacheLayer.Key.PriceDefinition.ToString());
        //}
        public FixAssetOrAdhocRequestAndApprove()
        {
            try
            {
                InitializeComponent();

                ddlReuestType.Items.Clear();
                ComboboxItem item = new ComboboxItem();
                item.Text = "FIXED ASSETS";
                item.Value = "2";
                ddlReuestType.Items.Add(item);

                ComboboxItem item2 = new ComboboxItem();
                item2.Text = "FGAP";
                item2.Value = "1";
                ddlReuestType.Items.Add(item2);

                ddlReuestType.SelectedItem = item;

                Dictionary<string, string> status_list = CHNLSVC.Inventory.Get_all_ItemSatus();
                status_list.Add("Any", "Any");
                foreach (string Key in status_list.Keys)
                {
                    ComboboxItem stat = new ComboboxItem();
                    stat.Text = status_list[Key];
                    stat.Value = Key;
                    ddlStatus.Items.Add(stat);
                }
                ddlStatus.SelectedItem = "Any";
                ddlStatus.Enabled = true;
                //-----------------------------------------------
                ddlAction.Items.Clear();
                ComboboxItem action1 = new ComboboxItem();
                action1.Text = "New Request";
                action1.Value = "Request";
                ddlAction.Items.Add(action1);

                ComboboxItem action2 = new ComboboxItem();
                action2.Text = "Approve Request";
                action2.Value = "Approve";
                ddlAction.Items.Add(action2);

                ComboboxItem action3 = new ComboboxItem();
                action3.Text = "Confirmation";
                action3.Value = "Confirmation";
                ddlAction.Items.Add(action3);

                ddlAction.SelectedItem = action1;//uncommented

                //----------view states--------------------------
                Reset_Session_values();
                //-----------------------------------------------

                //-----------Bind grids------------
                grvAvailableSerials.DataSource = null;
                grvAvailableSerials.AutoGenerateColumns = false;
                //grvAvailableSerials.DataSource = AvailableSerialList;
                BindingSource _source = new BindingSource();
                _source.DataSource = AvailableSerialList;
                grvAvailableSerials.DataSource = _source;
                // grvAvailableSerials.DataBind();


                grvApproveItms.DataSource = null;
                grvApproveItms.AutoGenerateColumns = false;
                // grvApproveItms.DataSource = Approved_SerialList;
                BindingSource _source1 = new BindingSource();
                _source1.DataSource = Approved_SerialList;
                grvApproveItms.DataSource = _source1;
                // grvApproveItms.DataBind();

                grvItmDes.DataSource = null;
                grvItmDes.AutoGenerateColumns = false;
                //grvItmDes.DataSource = AdhodDetList;
                BindingSource _source2 = new BindingSource();
                _source2.DataSource = AdhodDetList;
                grvItmDes.DataSource = _source2;
                // grvItmDes.DataBind();
                //---------------------------------

                //Tharindu 2018-06-06
               string fixloc = LoadFixedAssetLocation().ToString();
               if (string.IsNullOrEmpty(fixloc))
               {
                   MessageBox.Show("FixAsset Loc Not Avilable in this location", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   return;
               }
               else
               {
                   txtSendLoc.Text = fixloc;
               }

               string chkfixloc = CheckFixAssetlocAvailability(fixloc).ToString();
               if (string.IsNullOrEmpty(chkfixloc))
               {
                   MessageBox.Show("FixAsset Loc Not Avilable in Fix asset db", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                   return;
               }

                if (ddlReuestType.SelectedItem == item)
                {

                    //}
                    //if (ddlReuestType.SelectedValue.ToString() == "2")
                    //{
                   // txtSendLoc.Text = BaseCls.GlbUserDefLoca;
                    txtSendLoc.Text = chkfixloc;

                    divLoc.Visible = true;
                    Panel_PriceDet.Visible = false;
                }
                else
                {
                    txtSendLoc.Text = "";
                    divLoc.Visible = false;
                    Panel_PriceDet.Visible = true;
                }

                //BindPaymentType(ddlPayMode);
                //BindReceiptItem();
                //BankOrOther_Charges = 0;
                //AmtToPayForFinishPayment = 0;

                ////  div_payment.Visible = false;
                //pnlPayss.Enabled = false;

                //ucPayModes1.ClearControls();
                ucPayModes1.InvoiceType = "HPR";
                ucPayModes1.TotalAmount = 0;
                ucPayModes1.LoadPayModes();

                //**********************************************
                txtQty.Text = "1";
               // txtSendLoc.Text = BaseCls.GlbUserDefLoca;

                txtSendLoc.Text = chkfixloc;

                btnApprove.Enabled = false;
                Button1.Enabled = false;//CHANGED 01-02-2013

                btnConfirm.Enabled = false;
                btnReject.Enabled = false;
                Button10.Visible = false;
                //panel_manualReceipt.Visible = false;
                //*********************************************
                //--------------------------------
                pnlPayss.Visible = false;
                panel_payment.Visible = false;
                ucPayModes1.Visible = false;
                //---------------------------
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private string LoadFixedAssetLocation()
        {
            //tharindu
            try
            {
                DataTable DT = CHNLSVC.MsgPortal.GetFixAssetLoc_NEW(BaseCls.GlbUserComCode,BaseCls.GlbUserDefLoca);
                
                if (DT == null || DT.Rows.Count == 0)
                {
                    txtPC.Text = "";
                    MessageBox.Show("Fixed Asset Location Not Avialable in This PC");
                    txtPC.Focus();
                   
                 
                }
                else
                {
                    fixassetloc = DT.Rows[0][0].ToString();
                }
               
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

            return fixassetloc;
        }

        private string CheckFixAssetlocAvailability(string loc)
        {
            try
            {
                // tharindu
                DataTable DT = CHNLSVC.MsgPortal.CheckFixAssetlocAvailability(BaseCls.GlbUserComCode, loc);

                if (DT == null || DT.Rows.Count == 0)
                {
                    txtPC.Text = "";
                    MessageBox.Show("Fixed Asset Location Not Avialable in This Location");
                    txtPC.Focus();
                }
                else
                {
                    validfixassetloc = DT.Rows[0][0].ToString();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

            return validfixassetloc;
        }

        private void LoadPriceDefaultValue()
        {
            try
            {
                MasterProfitCenter _MasterProfitCenter = CacheLayer.Get<MasterProfitCenter>(CacheLayer.Key.ProfitCenter.ToString());
                List<PriceDefinitionRef> _PriceDefinitionRef = CacheLayer.Get<List<PriceDefinitionRef>>(CacheLayer.Key.PriceDefinition.ToString());

                if (_PriceDefinitionRef != null) if (_PriceDefinitionRef.Count > 0)
                    {
                        var _defaultValue = _PriceDefinitionRef.Where(x => x.Sadd_def == true).ToList();
                        if (_defaultValue != null)
                            if (_defaultValue.Count > 0)
                            {
                                //DefaultInvoiceType = _defaultValue[0].Sadd_doc_tp;
                                string DefaultBook = _defaultValue[0].Sadd_pb;
                                string DefaultLevel = _defaultValue[0].Sadd_p_lvl;
                                if (DefaultBook == txtPriceBook.Text.Trim().ToUpper())
                                {
                                    txtPBLevel.Text = DefaultLevel;
                                }
                                //DefaultStatus = _defaultValue[0].Sadd_def_stus;
                                //DefaultItemStatus = _defaultValue[0].Sadd_def_stus;
                                //LoadInvoiceType();
                                //LoadPriceBook(cmbInvType.Text);
                                //LoadPriceLevel(cmbInvType.Text, cmbBook.Text.Trim());
                                //LoadLevelStatus(cmbInvType.Text, cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                                //CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
                            }
                    }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
            
        }
        private void Reset_Session_values()
        {
            //----------view states--------------------------
            ItmLine = 0;
            AdhodDetList = new List<InventoryAdhocDetail>();
            SelectedItemCD = string.Empty;
            AvailableSerialList = new List<ReptPickSerials>();
            Approved_SerialList = new List<ReptPickSerials>();
            Searched_AdhodHeader = null;
            det_list_selected = new List<InventoryAdhocDetail>();
            searchedAdhocDetList = new List<InventoryAdhocDetail>();
            Transaction_List = new List<HpTransaction>();
            //-----------------------------------------------
        }
        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        private void FixAssetOrAdhocRequestAndApprove_Load(object sender, EventArgs e)
        {

        }
        private void clearCompleateScreen()
        {
            try
            {
                txtQty.Text = "1";
                Dictionary<string, string> status_list = CHNLSVC.Inventory.Get_all_ItemSatus();
                status_list.Add("Any", "Any");
                foreach (string Key in status_list.Keys)
                {
                    ComboboxItem stat = new ComboboxItem();
                    stat.Text = status_list[Key];
                    stat.Value = Key;
                    ddlStatus.Items.Add(stat);
                }
                ddlStatus.SelectedItem = "Any";
                ddlStatus.Enabled = true;
                //----------------------------------------
                ddlAction.Items.Clear();
                ComboboxItem action1 = new ComboboxItem();
                action1.Text = "New Request";
                action1.Value = "Request";
                ddlAction.Items.Add(action1);

                ComboboxItem action2 = new ComboboxItem();
                action2.Text = "Approve Request";
                action2.Value = "Approve";
                ddlAction.Items.Add(action2);

                ComboboxItem action3 = new ComboboxItem();
                action3.Text = "Confirmation";
                action3.Value = "Confirmation";
                ddlAction.Items.Add(action3);

                // ddlAction.SelectedItem = action1;//uncommented
                //----------view states--------------------------
                Reset_Session_values();
                //-----------------------------------------------
                //-----------Bind grids------------
                grvAvailableSerials.DataSource = null;
                grvAvailableSerials.AutoGenerateColumns = false;
                //grvAvailableSerials.DataSource = AvailableSerialList;           
                BindingSource _source2 = new BindingSource();
                _source2.DataSource = AvailableSerialList;
                grvAvailableSerials.DataSource = _source2;
                // grvAvailableSerials.DataBind();

                grvApproveItms.DataSource = null;
                grvApproveItms.AutoGenerateColumns = false;
                //grvApproveItms.DataSource = Approved_SerialList;
                BindingSource _source1 = new BindingSource();
                _source1.DataSource = Approved_SerialList;
                grvApproveItms.DataSource = _source1;



                // grvApproveItms.DataBind();
                grvItmDes.DataSource = null;
                grvItmDes.AutoGenerateColumns = false;
                //grvItmDes.DataSource = AdhodDetList;
                BindingSource _source = new BindingSource();
                _source.DataSource = AdhodDetList;
                grvItmDes.DataSource = _source;
                // grvItmDes.DataBind();
                //---------------------------------
                ComboboxItem combo_reqtp = (ComboboxItem)ddlReuestType.SelectedItem;
                if (combo_reqtp.Value.ToString() == "2")
                {
                   // txtSendLoc.Text = BaseCls.GlbUserDefLoca;
                    txtSendLoc.Text = fixassetloc;
                    divLoc.Visible = true;
                    Panel_PriceDet.Visible = false;
                }
                else
                {
                    txtSendLoc.Text = "";
                    divLoc.Visible = false;
                    Panel_PriceDet.Visible = true;
                }

                //BindPaymentType(ddlPayMode);
                //BindReceiptItem();
                //BankOrOther_Charges = 0;
                //AmtToPayForFinishPayment = 0;
                //--------------------------------------------------------
                 txtSendLoc.Text = fixassetloc;

               // txtSendLoc.Text = BaseCls.GlbUserDefLoca;
                txtRefNo.Text = "";
                txtItemCD.Text = "";
                txtItmDescription.Text = "";
                txtModel.Text = "";
                txtPBLevel.Text = "";
                txtPC.Text = "";
                txtQty.Text = "";
                txtUnitPrice.Text = "";

                lblApprVal.Text = string.Format("{0:n2}", 0);
                lblPriceDeference.Text = string.Format("{0:n2}", 0);
                lblCollectVal.Text = string.Format("{0:n2}", 0);
                lblReceiptAmt.Text = string.Format("{0:n2}", 0);

                Button10.Visible = false;
                //panel_manualReceipt.Visible = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            // _basePage = new BasePage();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {


                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBook:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "CS" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + "CS" + seperator + txtPriceBook.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.FixAssetRefNo:
                    {
                        //( p_com in NVARCHAR2,p_loc in NVARCHAR2,p_refNo in NVARCHAR2,p_type in NUMBER,p_status in NUMBER,c_data OUT sys_refcursor)
                    //    string Loc = txtSendLoc.Text.Trim().ToUpper();
                        //string Loc = BaseCls.GlbUserDefProf;
                        string Loc = BaseCls.GlbUserDefLoca;
                        
                        ComboboxItem req = (ComboboxItem)(ddlReuestType.SelectedItem);
                        Int32 type = req.Value.ToString() == "2" ? 2 : 1;
                        if(type==1)
                        {
                            Loc = BaseCls.GlbUserDefLoca;
                        }
                        // Int32 status = rdoPending.Checked == true ? 0 : 1;
                        ComboboxItem act = (ComboboxItem)(ddlAction.SelectedItem);
                        Int32 status = act.Value.ToString() == "Approve" ? 1 : act.Value.ToString() == "Confirmation"?3:-1;
                        // Int32 status = ddlAction.SelectedValue == "Approve" ? 0 : 1;
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + Loc + seperator + type + seperator + status);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void ImgSearchRefNo_Click(object sender, EventArgs e)
        {
            try {
                //Tharindu 
                if (ddlAction.SelectedIndex == -1)
                {
                    MessageBox.Show("Select The Action");
                    return;
                }

                 ComboboxItem ACT = (ComboboxItem)(ddlAction.SelectedItem);

                if (ACT.Value.ToString() == "Request")
                {
                    return;
                }

                

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.FixAssetRefNo);
                DataTable _result = CHNLSVC.CommonSearch.GET_FixAsset_ref(_CommonSearch.SearchParams, null, null);            

                if (_result==null)
                {
                    MessageBox.Show("No records found in this location");
                }
                else if (_result.Rows.Count < 1)
                {
                    MessageBox.Show("No records found in this location.");
                }
                else
                {
                    _CommonSearch.dvResult.DataSource = _result;
                    _CommonSearch.BindUCtrlDDLData(_result);
                    _CommonSearch.obj_TragetTextBox = txtRefNo;
                    _CommonSearch.ShowDialog();
                }
                txtRefNo.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnRefOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRefNo.Text.Trim() == "")
                {
                    return;
                }
                ComboboxItem ddlAction_SELECTED = (ComboboxItem)(ddlAction.SelectedItem);
                if (ddlAction_SELECTED == null)
                {
                    MessageBox.Show("Please select the action", "Fixed Asset", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

               
                Reset_Session_values();
                ComboboxItem selectType = (ComboboxItem)(ddlReuestType.SelectedItem);
                Int32 type_ = Convert.ToInt32(selectType.Value);//Convert.ToInt32(ddlReuestType.SelectedValue.ToString());
                string ref_no = txtRefNo.Text.Trim().ToUpper();
                InventoryAdhocHeader Header = new InventoryAdhocHeader();
                List<InventoryAdhocDetail> det_list = null;
              //  string location = txtSendLoc.Text.Trim().ToUpper();
                string location = BaseCls.GlbUserDefLoca;

                //****added on 15-01-2013************************************
                if (type_ == 1)
                {//added by prabhath on 28/12/2013
                    location = BaseCls.GlbUserDefLoca;
                }
                ComboboxItem ACT = (ComboboxItem)ddlAction.SelectedItem;
                int _staus = 0;//added by prabhath on 28/12/2013
                //if (type_ == 1 && ddlAction.SelectedValue.ToString() == "Confirmation")
                if (type_ == 1 && ACT.Value.ToString() == "Confirmation")
                {
                    btn_SendReq.Enabled = false;
                    _staus = 1;//added by prabhath on 28/12/2013
                }
                else
                {
                    btn_SendReq.Enabled = true;
                    _staus = 0;//added by prabhath on 28/12/2013
                }
                //********************************************************** //added by prabhath on 28/12/2013 _status
                searchedAdhocDetList = CHNLSVC.Inventory.GET_adhocDET_byRefNo(BaseCls.GlbUserComCode, location, type_, ref_no, _staus, out Header);// status must be 0
                if (searchedAdhocDetList != null && searchedAdhocDetList.Count > 0)
                {
                    var _no = searchedAdhocDetList.Where(x => x.Iadd_stus == 0).ToList();
                    if (_no != null && _no.Count > 0)
                    {
                       // MessageBox.Show("Request not approved!", "Approval", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }


                // if (ddlAction.SelectedValue.ToString() == "Approve")  //if (rdoPending.Checked)
                if (ddlAction_SELECTED.Value.ToString() == "Approve")  //if (rdoPending.Checked)
                {
                    det_list = CHNLSVC.Inventory.GET_adhocDET_byRefNo(BaseCls.GlbUserComCode, location, type_, ref_no, 0, out Header);
                    if (det_list != null)
                    {
                        List<InventoryAdhocDetail> bind_AdhocDetList = new List<InventoryAdhocDetail>();
                        bind_AdhocDetList.AddRange(det_list);
                        var distinctList = bind_AdhocDetList.GroupBy(x => x.Iadd_claim_itm)
                                      .Select(g => g.First())
                                      .ToList();
                        List<InventoryAdhocDetail> bind_AdhocDetList2 = new List<InventoryAdhocDetail>();
                        bind_AdhocDetList2.AddRange(distinctList);

                        grvItmDes.DataSource = null;
                        grvItmDes.AutoGenerateColumns = false;
                        BindingSource _source = new BindingSource();
                        _source.DataSource = bind_AdhocDetList2;
                        grvItmDes.DataSource = _source;
                        //grvItmDes.DataSource = bind_AdhocDetList2;                  
                    }
                    else
                    {
                        //  MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "There are no Pending requested items available with this Ref.No!");
                        MessageBox.Show("There are no Pending requested items available with this Ref.No!");
                        return;
                    }
                }
                //else if (ddlAction.SelectedValue.ToString() == "Confirmation") //else if (rdoApproved.Checked)
                else if (ddlAction_SELECTED.Value.ToString() == "Confirmation") //else if (rdoApproved.Checked)
                {
                    det_list = CHNLSVC.Inventory.GET_adhocDET_byRefNo(BaseCls.GlbUserComCode, location, type_, ref_no, 1, out Header);

                    if (Header == null)
                    {
                        MessageBox.Show("Please enter valid Ref#");
                        return;
                    }
                    if (Header.Iadh_stus == 3)
                    {
                        MessageBox.Show(Header.Iadh_ref_no + " is already confiremed!");
                        return;
                    }
                    if (det_list != null)
                    {
                        List<InventoryAdhocDetail> bind_AdhocDetList = new List<InventoryAdhocDetail>();
                        bind_AdhocDetList.AddRange(det_list);
                        var distinctList = bind_AdhocDetList.GroupBy(x => x.Iadd_claim_itm)
                                      .Select(g => g.First())
                                      .ToList();
                        List<InventoryAdhocDetail> bind_AdhocDetList2 = new List<InventoryAdhocDetail>();
                        bind_AdhocDetList2.AddRange(distinctList);

                        grvItmDes.DataSource = null;
                        grvItmDes.AutoGenerateColumns = false;
                        BindingSource _source = new BindingSource();
                        _source.DataSource = bind_AdhocDetList2;
                        grvItmDes.DataSource = _source;
                        //grvItmDes.DataSource = bind_AdhocDetList2;                   
                        // det_list = CHNLSVC.Inventory.GET_adhocDET_byRefNo(GlbUserComCode, GlbUserDefLoca, type_, ref_no, 1, out Header);
                    }
                    else
                    {
                        // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "There are no Approved request items available with this Ref.No!");
                        MessageBox.Show("There are no Approved request items available with this Ref.No!");
                        return;
                    }
                }
                else
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Select 'Search Status' first!");
                    MessageBox.Show("Select correct Action please!");
                    return;
                }


                AdhodDetList = det_list;
                //BindingSource _source_ = new BindingSource();
                //_source_.DataSource = AdhodDetList;
                //grvItmDes.DataSource = null;
                //grvItmDes.AutoGenerateColumns = false;
                //grvItmDes.DataSource = _source_; 

                Searched_AdhodHeader = Header;
                if (Header != null)
                {
                    if (Searched_AdhodHeader.Iadh_stus == 4)
                    {
                        clearCompleateScreen();
                        MessageBox.Show(Header.Iadh_ref_no + " has been Cancelled!");
                        return;
                    }

                 //   txtSendLoc.Text = Header.Iadh_loc;
                    txtSendLoc.Text = fixassetloc;

                    //  btnItmClear.Enabled = false;  TODO: ADD THIS BUTTON
                    ddlStatus.Enabled = false;
                    btnApprove.Enabled = false;
                    Button1.Enabled = false;
                    btnItmAdd.Enabled = false;

                    if (Header.Iadh_tp == 1)//FGAP
                    {
                        btnItmAdd.Enabled = true;
                        Decimal totalApprovedVal = 0;
                        foreach (InventoryAdhocDetail detail in AdhodDetList)
                        {
                            totalApprovedVal = totalApprovedVal + detail.Iadd_app_val * Convert.ToDecimal(detail.Iadd_anal1);
                        }
                        lblApprVal.Text = string.Format("{0:n2}", totalApprovedVal);
                        lblCollectVal.Text = string.Format("{0:n2}", totalApprovedVal);
                        lblPriceDeference.Text = string.Format("{0:n2}", 0);
                        //lblReceiptAmt.Text = "0"; 
                        lblReceiptAmt.Text = string.Format("{0:n2}", 0);

                        btnApprove.Enabled = false;
                        Button1.Enabled = false;//CHANGED 01-02-2013
                        btnConfirm.Enabled = true;
                        btnReject.Enabled = true;
                    }
                    else //Header.Iadh_tp == 2 //FIX ASSET
                    {

                        if (Header.Iadh_stus == 0)
                        {
                            btnApprove.Enabled = true;
                            Button1.Enabled = true;//CHANGED 01-02-2013

                            btnConfirm.Enabled = false;
                            btnReject.Enabled = false;
                        }
                        if (Header.Iadh_stus == 1)
                        {
                            btnApprove.Enabled = false;
                            Button1.Enabled = false;//CHANGED 01-02-2013

                            btnConfirm.Enabled = true;
                            btnReject.Enabled = true;
                        }
                    }
                }
                else
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "There are no pending/approved request with this Ref.No!");
                    MessageBox.Show("There are no pending/approved request with this Ref.No!");
                    // txtSendLoc.Text = "";
                    btnItmAdd.Enabled = true;
                    // btnItmClear.Enabled = true; TODO: UNCOMMENT THIS
                    ddlStatus.Enabled = true;
                    btnApprove.Enabled = false;//CHANGED 01-02-2013
                    Button1.Enabled = false;//CHANGED 01-02-2013
                }

                //--------------load first item's details to the gridS.-------------------------------------
                #region load first item's details to the gridS
                //----------GRID 1
                AvailableSerialList = new List<ReptPickSerials>();
                DataGridViewRow gvr = grvItmDes.Rows[0];

                string reqQty = gvr.Cells["reqQty"].Value.ToString();
                string reqStatus = gvr.Cells["lbl_itm_Status"].Value.ToString();
                SelectedItemCD = gvr.Cells["Iadd_claim_itm"].Value.ToString();

                var _dup = from _l in AdhodDetList
                           where _l.Iadd_claim_itm == SelectedItemCD && _l.Iadd_stus == Header.Iadh_stus //&& _l.Iadd_anal4 == ApprSerID.ToString()
                           select _l;

                List<ReptPickSerials> serList = new List<ReptPickSerials>();
                serList = CHNLSVC.Inventory.GET_ser_FOR_STATUS(BaseCls.GlbUserComCode, txtSendLoc.Text.Trim().ToUpper(), SelectedItemCD, string.Empty);


                //GetInventorySerialListById
                foreach (InventoryAdhocDetail det in _dup)
                {
                    var _dup2 = from _l in serList
                                where _l.Tus_itm_cd == det.Iadd_claim_itm && _l.Tus_ser_id == Convert.ToInt32(det.Iadd_anal4)
                        
                                select _l;

                    // Tharindu 2018-06-06
                    if(_dup2.Count() > 0)
                    {
                        _dup2.First().Tus_itm_model = det.Iadd_anal2;
                        _dup2.First().Tus_cre_by = BaseCls.GlbUserID;

                        // det.Iadd_anal2=det.
                        AvailableSerialList.AddRange(_dup2);
                    }

           
                    
                    //if ()
                    //{
                    //    List<ReptPickSerials> request_serList = new List<ReptPickSerials>();
                    //    request_serList = CHNLSVC.Inventory.GetInventorySerialListById(det.Iadd_anal4.ToString(), txtSendLoc.Text.Trim().ToUpper());
                    //    AvailableSerialList.AddRange(request_serList);
                    //}


                }

                grvAvailableSerials.DataSource = null;
                grvAvailableSerials.AutoGenerateColumns = false;
                //grvAvailableSerials.DataSource = AvailableSerialList;
                BindingSource _source2 = new BindingSource();
                _source2.DataSource = AvailableSerialList;
                grvAvailableSerials.DataSource = _source2;


                ////-----GRID 2
                //    if (Header.Iadh_stus == 0)//only Pending FIX ASSET
                //    {                   
                //       // InventoryAdhocHeader Header;
                //        det_list_selected = CHNLSVC.Inventory.GET_adhocDET_byRefNo(BaseCls.GlbUserComCode, location, 2, Header.Iadh_ref_no, 0, out Header);
                //    }
                //    foreach (DataGridViewRow gvrR in this.grvAvailableSerials.Rows)
                //    {
                //        string itemCode = gvrR.Cells["Tus_itm_cd"].Value.ToString();
                //        string serID = gvrR.Cells["lblSerID_av"].Value.ToString();
                //        if (det_list_selected != null)
                //        {
                //            var _exist = from _dupP in det_list_selected
                //                         where _dupP.Iadd_claim_itm == itemCode //&& _dup.Sccd_brd == obj.Sccd_brd 
                //                         select _dupP;

                //            if (_exist.Count() != 0)
                //            {
                //                foreach (InventoryAdhocDetail det in _exist)
                //                {
                //                    if (det.Iadd_anal4 == Convert.ToInt32(serID))
                //                    {
                //                        // gvr.DefaultCellStyle.ForeColor = Color.LightSalmon;
                //                        //************************
                //                        var _dupP = from _l in AvailableSerialList
                //                                   where _l.Tus_itm_cd == itemCode && _l.Tus_ser_id == Convert.ToInt32(serID)
                //                                   select _l;


                //                        // serList= _dup.ToList<ReptPickSerials>();
                //                        Approved_SerialList.AddRange(_dupP);

                //                        AvailableSerialList.RemoveAll(x => x.Tus_itm_cd == itemCode && x.Tus_ser_id == Convert.ToInt32(serID));//&& x.Iadd_anal2 == DelModle

                //                        grvApproveItms.DataSource = null;
                //                        grvApproveItms.AutoGenerateColumns = false;
                //                        grvApproveItms.DataSource = Approved_SerialList;

                //                        grvAvailableSerials.DataSource = null;
                //                        grvAvailableSerials.AutoGenerateColumns = false;
                //                        grvAvailableSerials.DataSource = AvailableSerialList;
                //                        //************************

                //                    }
                //                }
                //            }
                //        }
                //    }
                #endregion

                if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "INV5"))
                {
                    btnApprove.Enabled = false;
                    Button1.Enabled = false;//CHANGED 01-02-2013
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void imgBtnSearchItmCD_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemCD;
                _CommonSearch.ShowDialog();
                txtItemCD.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private InventoryAdhocDetail fillAdhocDet_FixAsset_Request()
        {
            
                InventoryAdhocDetail Det = new InventoryAdhocDetail();
                try
                {
                try
                {
                    Det.Iadd_anal1 = Convert.ToDecimal(txtQty.Text.Trim());
                }
                catch (Exception ex)
                {
                    // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter valid Qty.");
                    MessageBox.Show("Enter valid Qty.");
                    return null;
                }
                if (txtModel.Text == "")
                {
                    this.txtItemCD_Leave(null, null);
                }
                Det.Iadd_anal2 = txtModel.Text.Trim().ToUpper();
                Det.Iadd_anal3 = txtItmDescription.Text.Trim().ToUpper();
                //Det.Iadd_anal4 = ;
                //Det.Iadd_anal5 =;
                //Det.Iadd_app_itm =;
                //Det.Iadd_app_pb = ;
                //Det.Iadd_app_pb_lvl =;
                //Det.Iadd_app_val = ;
                Det.Iadd_claim_itm = txtItemCD.Text.Trim().ToUpper();
                Det.Iadd_claim_pb = txtPriceBook.Text.Trim();
                Det.Iadd_claim_pb_lvl = txtPBLevel.Text.Trim();
                //Det.Iadd_claim_val = ;
                //Det.Iadd_coll_itm =;
                //Det.Iadd_coll_pb = ;
                //Det.Iadd_coll_pb_lvl = ;
                //Det.Iadd_coll_ser1 = ;
                //Det.Iadd_coll_ser2 = ;,
                //Det.Iadd_coll_ser3 = ;
                //Det.Iadd_coll_ser3 = ;
                //Det.Iadd_coll_val = ;
                Det.Iadd_line = ItmLine++;
                //Det.Iadd_ref_no =;
                //Det.Iadd_seq = ;
                Det.Iadd_stus = 0;
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
                }
                finally
                {
                    CHNLSVC.CloseAllChannels();
                }
                return Det;
           
        }
        private InventoryAdhocDetail fillAdhocDet_FGAP_Request()
        {
           
            InventoryAdhocDetail Det = new InventoryAdhocDetail();
            try
            {
                try
                {
                    Det.Iadd_anal1 = Convert.ToDecimal(txtQty.Text.Trim());
                }
                catch (Exception ex)
                {
                    // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter valid Qty.");
                    MessageBox.Show("Enter valid Qty.");
                    return null;
                }

                Det.Iadd_anal2 = txtModel.Text.Trim().ToUpper();
                Det.Iadd_anal3 = txtItmDescription.Text.Trim().ToUpper();
                //Det.Iadd_anal4 = ;
                //Det.Iadd_anal5 =;
                Det.Iadd_app_itm = txtItemCD.Text.Trim().ToUpper(); ;
                Det.Iadd_app_pb = txtPriceBook.Text.ToUpper();
                Det.Iadd_app_pb_lvl = txtPBLevel.Text.ToUpper();


                Det.Iadd_claim_itm = txtItemCD.Text.Trim().ToUpper();
                //Det.Iadd_claim_pb =;
                //Det.Iadd_claim_pb_lvl = ;
                //Det.Iadd_claim_val = ;
                //Det.Iadd_coll_itm =;
                //Det.Iadd_coll_pb = ;
                //Det.Iadd_coll_pb_lvl = ;
                //Det.Iadd_coll_ser1 = ;
                //Det.Iadd_coll_ser2 = ;,
                //Det.Iadd_coll_ser3 = ;
                //Det.Iadd_coll_ser3 = ;
                //Det.Iadd_coll_val = ;
                Det.Iadd_line = ItmLine++;
                //Det.Iadd_ref_no =;
                //Det.Iadd_seq = ;
                Det.Iadd_stus = 1;
                List<PriceDetailRef> _priceDetailRef = new List<PriceDetailRef>();
                _priceDetailRef = CHNLSVC.Sales.GetPrice(BaseCls.GlbUserComCode, txtPC.Text.Trim(), "CS", txtPriceBook.Text, txtPBLevel.Text, string.Empty, txtItemCD.Text, Convert.ToDecimal(txtQty.Text), txtDate.Value);
                if (_priceDetailRef != null)
                {
                    if (_priceDetailRef.Count > 0)
                    {
                        Decimal unitPrice = _priceDetailRef[0].Sapd_itm_price;
                        txtUnitPrice.Text = unitPrice.ToString();
                        if (txtUnitPrice.Text.Trim() != "")
                        {
                            Det.Iadd_app_val = unitPrice;
                            //Det.Iadd_app_val = Convert.ToDecimal(txtUnitPrice.Text.Trim());
                        }
                        else
                        {
                            // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Prices not defined for this item in this PriceBook and Level!!");
                            MessageBox.Show("Prices not defined for this item in this PriceBook and Level!");
                            return null;
                        }
                    }
                    else
                    {
                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Prices not defined for this item in this PriceBook and Level!!");
                        MessageBox.Show("Prices not defined for this item in this PriceBook and Level!");
                        return null;
                    }
                }
                else
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Prices not defined for this item in this PriceBook and Level!!");
                    MessageBox.Show("Prices not defined for this item in this PriceBook and Level!");
                    return null;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
            return Det;
        }
        private void btnItmAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtItemCD.Text.Trim() == "")
                {
                    return;
                }
                MasterItem msitem = new MasterItem();
                try
                {
                    msitem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemCD.Text.Trim().ToUpper());
                    txtModel.Text = msitem.Mi_model;
                    txtItmDescription.Text = msitem.Mi_shortdesc;
                    if (msitem == null)
                    {
                        MessageBox.Show("Invalid Item code!");
                        txtModel.Text = "";
                        txtItmDescription.Text = "";
                        txtItemCD.Text = "";
                        return;
                    }

                    if (ddlReuestType.Text == "FIXED ASSETS")
                    {
                        //if (BaseCls.GlbUserComCode == "ABL")
                        //{
                            //Tharindu 2018-06-06
                            DataTable DT = CHNLSVC.MsgPortal.CheckValidItmInFixAsset(txtItemCD.Text.Trim().ToUpper());
                            if (DT == null || DT.Rows.Count == 0)
                            {
                                MessageBox.Show("This Item is not Available in fixed Asset system, Please Contact Account Department", "Fixed Asset", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtModel.Text = "";
                                txtItmDescription.Text = "";
                                txtItemCD.Text = "";
                                return;
                            }
                        //}
                    }
                    

                    // MasterMsgInfoUCtrl.Clear();
                }
                catch (Exception EX) { return; }
                try
                {
                    Convert.ToDecimal(txtQty.Text.Trim());
                }
                catch (Exception EX)
                {
                    MessageBox.Show("Enter valid Qty.");
                    return;
                }
                ComboboxItem req = (ComboboxItem)ddlReuestType.SelectedItem;
                //if (ddlReuestType.SelectedValue == "2")
                foreach (DataGridViewRow row in grvItmDes.Rows)
                {
                    string item = row.Cells["Iadd_claim_itm"].Value.ToString();
                    if (txtItemCD.Text.Trim() == item)
                    {
                        MessageBox.Show("Cannot add same Item twice");
                        return;
                    }
                }

                if (req.Value.ToString() == "2")
                {
                    InventoryAdhocDetail Det = fillAdhocDet_FixAsset_Request();
                    if (Det != null)
                    {
                        AdhodDetList.Add(Det);
                        BindingSource _source = new BindingSource();
                        _source.DataSource = AdhodDetList;
                        grvItmDes.DataSource = null;
                        grvItmDes.AutoGenerateColumns = false;
                        grvItmDes.DataSource = _source;
                        //grvItmDes.DataBind();
                    }
                }
                //else if (ddlReuestType.SelectedValue == "1")
                else if (req.Value.ToString() == "1")
                {
                    if (txtPriceBook.Text.Trim() == "" || txtPBLevel.Text.Trim() == "")
                    {
                        // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Price Book details.");
                        MessageBox.Show("Enter Price Book details.");
                        return;
                    }
                    if (txtFgapLoc.Text.Trim() == "")
                    {
                        // this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Location.");
                        MessageBox.Show("Enter Location.");
                        txtFgapLoc.Focus();
                        return;
                    }
                    MasterLocation LOC = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, txtFgapLoc.Text.Trim().ToUpper());
                    if (LOC == null)
                    {
                        txtFgapLoc.Text = "";
                        MessageBox.Show("Invalid Location code");
                        return;
                    }

                    if (txtPC.Text.Trim() == "")
                    {
                        //  this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Enter Profit Center.");
                        MessageBox.Show("Enter Profit Center.");
                        txtPC.Focus();
                        return;
                    }
                    DataTable DT = CHNLSVC.General.GetPartyCodes(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper());
                    if (DT == null)
                    {
                        txtPC.Text = "";
                        MessageBox.Show("Invalid Profit center.");
                        txtPC.Focus();
                        return;
                    }
                    //InventoryAdhocDetail Det = fillAdhocDet_FixAsset_Request();
                    txtSendLoc.Text = txtFgapLoc.Text.Trim();
                    InventoryAdhocDetail Det = fillAdhocDet_FGAP_Request();

                    if (Det != null)
                    {
                        if (AdhodDetList == null)
                        {
                            AdhodDetList = new List<InventoryAdhocDetail>();
                        }
                        AdhodDetList.Add(Det);

                        BindingSource _source = new BindingSource();
                        _source.DataSource = AdhodDetList;

                        grvItmDes.DataSource = null;
                        grvItmDes.AutoGenerateColumns = false;
                        grvItmDes.DataSource = _source;
                        //grvItmDes.DataSource = AdhodDetList;

                        // grvItmDes.DataBind();
                        //--------------------------------------------------------------
                        //  if (btnApprove.Enabled == false)//this means this is an alrady approved, and it has been modified
                        //{
                        #region
                        Decimal totalApprovedVal = 0;
                        foreach (InventoryAdhocDetail detail in AdhodDetList)
                        {
                            totalApprovedVal = totalApprovedVal + detail.Iadd_app_val * Convert.ToDecimal(detail.Iadd_anal1);
                        }

                        lblCollectVal.Text = string.Format("{0:n2}", totalApprovedVal);
                        Decimal priceDiference = Convert.ToDecimal(lblCollectVal.Text) - Convert.ToDecimal(lblApprVal.Text);
                        lblPriceDeference.Text = string.Format("{0:n2}", priceDiference);

                        //lblReceiptAmt.Text = priceDiference.ToString();
                        ComboboxItem actION = (ComboboxItem)ddlAction.SelectedItem;
                        if (actION.Value.ToString() != "Approve")
                        {
                            if (priceDiference > 0)
                            {
                                btnConfirm.Enabled = false;
                                //Button10.Visible = false;
                                Button10.Visible = true;// panel_manualReceipt.Visible = true;
                                // btn_SendReq.Enabled = false;
                                lblReceiptAmt.Text = priceDiference.ToString();

                                ucPayModes1.ClearControls();
                                ucPayModes1.InvoiceType = "CS";
                                //ucPayModes1.LoadPayModes();
                                ucPayModes1.TotalAmount = priceDiference;
                                ucPayModes1.LoadData();

                                //-------------------
                                pnlPayss.Visible = true;
                                panel_payment.Visible = true;
                                ucPayModes1.Visible = true;
                                //-------------------

                            }
                            else
                            {
                                lblReceiptAmt.Text = string.Format("{0:n2}", 0);
                                btnConfirm.Enabled = true;
                                Button10.Visible = false;// panel_manualReceipt.Visible = false;
                                ucPayModes1.ClearControls();
                                ucPayModes1.InvoiceType = "CS";
                                //ucPayModes1.LoadPayModes();
                                ucPayModes1.TotalAmount = 0;
                                ucPayModes1.LoadData();
                                //ucPayModes1.LoadPayModes();

                                pnlPayss.Visible = false;
                                panel_payment.Visible = false;
                                ucPayModes1.Visible = false;
                            }
                        }
                        else
                        {
                            pnlPayss.Visible = false;
                            panel_payment.Visible = false;
                            ucPayModes1.Visible = false;
                        }
                        // }
                        #endregion
                        //--------------------------------------------------------------
                    }
                    // else
                    // {

                    // }
                }
                txtItemCD.Text = "";
                txtItmDescription.Text = "";
                txtModel.Text = "";
                txtPBLevel.Text = "";
                txtPriceBook.Text = "";
                // txtPC.Text = "";
                txtQty.Text = "";
                txtUnitPrice.Text = "";
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtItemCD_Leave(object sender, EventArgs e)
        {

            MasterItem msitem = new MasterItem();
            if (txtItemCD.Text.Trim() == "")
            { return; }
            try
            {
                msitem = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItemCD.Text.Trim().ToUpper());
                txtModel.Text = msitem.Mi_model;
                txtItmDescription.Text = msitem.Mi_shortdesc;
                txtQty.Text = "1";
                txtQty.Focus();

                if (msitem == null)
                {
                    MessageBox.Show("Invalid Item code!");
                    txtModel.Text = "";
                    txtItmDescription.Text = "";
                    txtItemCD.Text = "";
                }
                // MasterMsgInfoUCtrl.Clear();
            }
            catch (Exception ex)
            {
                // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Invalid Item code!");
                MessageBox.Show("Invalid Item code!");
                txtModel.Text = "";
                txtItmDescription.Text = "";
                txtItemCD.Text = "";
                return;
            }
            finally {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtPriceBook_Leave(object sender, EventArgs e)
        {

        }

        private void txtPBLevel_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPBLevel.Text.Trim() == "")
                {
                    txtPBLevel.Text = "";
                    return;
                }
                List<PriceDetailRef> _priceDetailRef = new List<PriceDetailRef>();
                _priceDetailRef = CHNLSVC.Sales.GetPrice(BaseCls.GlbUserComCode, txtPC.Text.Trim(), "CS", txtPriceBook.Text, txtPBLevel.Text, string.Empty, txtItemCD.Text, txtQty.Text == "" ? 1 : Convert.ToDecimal(txtQty.Text), txtDate.Value);
                if (_priceDetailRef != null)
                {
                    if (_priceDetailRef.Count > 0)
                    {
                        Decimal unitPrice = _priceDetailRef[0].Sapd_itm_price;
                        txtUnitPrice.Text = unitPrice.ToString();
                        if (txtUnitPrice.Text.Trim() == "")
                        {

                            //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Prices not defined for this item in this PriceBook and Level!!");
                            MessageBox.Show("Prices not defined for this item in this PriceBook and Level!");
                            txtUnitPrice.Text = "";
                            return;
                        }
                    }
                    else
                    {
                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Prices not defined for this item in this PriceBook and Level!!");
                        MessageBox.Show("Prices not defined for this item in this PriceBook and Level!");
                        txtUnitPrice.Text = "";
                        return;
                    }
                }
                else
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Prices not defined for this item in this PriceBook and Level!!");
                    MessageBox.Show("Prices not defined for this item in this PriceBook and Level!");
                    txtUnitPrice.Text = "";
                    return;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
           // txtFgapLoc.Focus();
        }

        private void ddlReuestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                clearCompleateScreen();
                //----------view states--------------------------
                Reset_Session_values();
                //-----------------------------------------------
                txtRefNo.Text = "";
                ComboboxItem REQ = (ComboboxItem)ddlReuestType.SelectedItem;

                ddlAction.Items.Clear();


                ComboboxItem action1 = new ComboboxItem();
                action1.Text = "New Request";
                action1.Value = "Request";
                if (REQ.Value.ToString() == "2")
                {
                    ddlAction.Items.Add(action1);
                }

                ComboboxItem action2 = new ComboboxItem();
                action2.Text = "Approve Request";
                action2.Value = "Approve";
                ddlAction.Items.Add(action2);

                ComboboxItem action3 = new ComboboxItem();
                action3.Text = "Confirmation";
                action3.Value = "Confirmation";
                ddlAction.Items.Add(action3);


                if (REQ.Value.ToString() == "2")
                {
                    txtSendLoc.Text = BaseCls.GlbUserDefLoca;
                    divLoc.Visible = true;
                    Panel_PriceDet.Visible = false;
                    btn_SendReq.Text = "Send Request";
                    // div_payment.Visible = false;
                    // pnlPayss.Enabled = false;
                    pnlPayss.Visible = false;
                    panel_payment.Visible = false;
                    ucPayModes1.Visible = false;

                    //--------22-01-2013----------
                    btnApprove.Enabled = true;
                    Button1.Enabled = true;


                    //------------------------------------------------------
                    ddlAction.SelectedItem = action1;//uncommented
                }
                else
                {
                    txtSendLoc.Text = "";
                    divLoc.Visible = false;
                    Panel_PriceDet.Visible = true;
                    btn_SendReq.Text = "Approve FGAP";

                    //pnlPayss.Visible = true;
                    //panel_payment.Visible = true;
                    //ucPayModes1.Visible = true;
                    pnlPayss.Visible = false;
                    panel_payment.Visible = false;
                    ucPayModes1.Visible = false;

                    //------------------------------------------------------
                    ddlAction.SelectedItem = action2;//uncommented


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }


        private void grvItmDes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                {
                    return;
                }
                if (e.ColumnIndex == 0 && e.RowIndex != -1)
                {

                    #region
                    ComboboxItem combo_stat = (ComboboxItem)ddlStatus.SelectedItem;
                    ComboboxItem combo_reqTp = (ComboboxItem)ddlReuestType.SelectedItem;
                    //ddlStatus.SelectedValue = "Any"; TODO:

                    grvItmDes.Rows[e.RowIndex].Selected = true;
                    DataGridView dgv = (DataGridView)sender;
                    if (dgv.SelectedRows.Count < 1)
                    {
                        return;
                    }
                    //GridViewRow row = grvItmDes.SelectedRow;
                    DataGridViewRow row = grvItmDes.SelectedRows[0];
                    SelectedItemCD = row.Cells[1].Value.ToString();


                    //  Label lblStatus = (Label)row.Cells[6].FindControl("lbl_itm_Status");           
                    //Int32 reqStatus = Convert.ToInt32(lblStatus.Text.Trim());
                    Int32 reqStatus = Convert.ToInt32(row.Cells["lbl_itm_Status"].Value.ToString());
                    Decimal req_qty = Convert.ToDecimal(row.Cells["reqQty"].Value.ToString());


                    List<ReptPickSerials> serList = new List<ReptPickSerials>();
                    string status = string.Empty;
                  //  serList = CHNLSVC.Inventory.GET_ser_FOR_STATUS(BaseCls.GlbUserComCode, txtSendLoc.Text.Trim().ToUpper(), SelectedItemCD, status);
                    serList = CHNLSVC.Inventory.GET_ser_FOR_STATUS(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, SelectedItemCD, status);
                    AvailableSerialList = new List<ReptPickSerials>();

                    if (serList == null)
                    {
                        MessageBox.Show("No available items in the location.");
                        return;
                    }
                    if (serList.Count < 1)
                    {
                        MessageBox.Show("No available items in the location.");
                        return;
                    }

                    if (reqStatus == 0)//only Pending FIX ASSET
                    {
                        AvailableSerialList = serList;

                        string location = txtSendLoc.Text.Trim().ToUpper();
                        InventoryAdhocHeader Header;
                        det_list_selected = CHNLSVC.Inventory.GET_adhocDET_byRefNo(BaseCls.GlbUserComCode, location, 2, txtRefNo.Text.Trim(), 0, out Header);
                    }
                    //else if (reqStatus == 1 && ddlReuestType.SelectedValue == "2") //Approved FIX ASSET   
                    else if (reqStatus == 1 && combo_reqTp.Value.ToString() == "2") //Approved FIX ASSET
                    {
                        //Label lblApprovedSerID = (Label)row.Cells[7].FindControl("lblApprSerID");
                        // Int32 ApprSerID = Convert.ToInt32(lblApprovedSerID.Text.Trim());


                        var _dup = from _l in AdhodDetList
                                   where _l.Iadd_claim_itm == SelectedItemCD && _l.Iadd_stus == reqStatus //&& _l.Iadd_anal4 == ApprSerID.ToString()
                                   select _l;

                        foreach (InventoryAdhocDetail det in _dup)
                        {
                            var _dup2 = from _l in serList
                                        where _l.Tus_itm_cd == det.Iadd_claim_itm && _l.Tus_ser_id == Convert.ToInt32(det.Iadd_anal4)
                                        select _l;


                            AvailableSerialList.AddRange(_dup2);

                        }
                        // serList= _dup.ToList<ReptPickSerials>();

                    }
                    // else if (reqStatus == 1 && ddlReuestType.SelectedValue == "1")// Approved FGAP 
                    else if (reqStatus == 1 && combo_reqTp.Value.ToString() == "1")// Approved FGAP 
                    {
                        //serList = CHNLSVC.Inventory.GET_ser_FOR_STATUS(GlbUserComCode, txtFgapLoc.Text.Trim().ToUpper(), SelectedItemCD, status);
                        Decimal unitPrice = Convert.ToDecimal(row.Cells["unit_price"].Value.ToString());
                        string model = row.Cells["Modle"].Value.ToString();
                        foreach (ReptPickSerials rpc in serList)//req_qty
                        {
                            rpc.Tus_unit_price = unitPrice;
                            rpc.Tus_itm_model = model;
                            rpc.Tus_cre_by = BaseCls.GlbUserID;

                        }
                       
                        AvailableSerialList = serList;
                    }
                    else
                    {
                        AvailableSerialList = null;
                    }
                    //-----------------------------------------------
                    if (AvailableSerialList != null)
                    {
                        foreach (DataGridViewRow grvRow in grvApproveItms.Rows)
                        {
                            string serID = grvRow.Cells["lblSerID_appr"].Value.ToString();
                            AvailableSerialList.RemoveAll(x => x.Tus_ser_id == Convert.ToInt32(serID));//remove already added serials
                        }
                    }
                    //------------------------------------------------
                    grvAvailableSerials.DataSource = null;
                    grvAvailableSerials.AutoGenerateColumns = false;
                    //grvAvailableSerials.DataSource = AvailableSerialList;
                    BindingSource _source = new BindingSource();
                    _source.DataSource = AvailableSerialList;
                    grvAvailableSerials.DataSource = _source;
                    //grvAvailableSerials.DataBind();
                    #endregion

                }

                //------------------delete item from grid----22-01-2013------------------------------------------------------------
                if (e.ColumnIndex == 5 && e.RowIndex != -1)
                {
                    grvItmDes.Rows[e.RowIndex].Selected = true;
                    DataGridView dgv = (DataGridView)sender;

                    if (dgv.SelectedRows.Count < 1)
                    {
                        return;
                    }

                    DataGridViewRow row = grvItmDes.SelectedRows[0];
                    Int32 reqStatus = Convert.ToInt32(row.Cells["lbl_itm_Status"].Value.ToString());
                    string DelItemCD = row.Cells["Iadd_claim_itm"].Value.ToString();
                    deleteItem(row); //DELETE ITEM.
                    //**********
                    grvAvailableSerials.DataSource = null;
                    grvAvailableSerials.AutoGenerateColumns = false;

                    BindingSource _source = new BindingSource();
                    _source.DataSource = new List<ReptPickSerials>();
                    grvAvailableSerials.DataSource = _source;

                    Approved_SerialList.RemoveAll(x => x.Tus_itm_cd == DelItemCD);//&& x.Iadd_anal2 == DelModle

                    grvApproveItms.DataSource = null;
                    grvApproveItms.AutoGenerateColumns = false;
                    // grvApproveItms.DataSource = Approved_SerialList;
                    BindingSource _source2 = new BindingSource();
                    _source2.DataSource = Approved_SerialList;
                    grvApproveItms.DataSource = _source2;
                    //*********
                    //---------------------------------------------------------------------------------
                    #region set approve, collect,price difference, reciept amounts



                    ComboboxItem combo_reqTp = (ComboboxItem)ddlReuestType.SelectedItem;
                    // if (reqStatus == 1 && combo_reqTp.Value.ToString() == "1")// Approved FGAP 
                    if (combo_reqTp.Value.ToString() == "1")// Approved FGAP 
                    {

                        #region
                        Decimal totalApprovedVal = 0;
                        foreach (InventoryAdhocDetail detail in AdhodDetList)
                        {
                            totalApprovedVal = totalApprovedVal + detail.Iadd_app_val * Convert.ToDecimal(detail.Iadd_anal1);
                        }

                        lblCollectVal.Text = string.Format("{0:n2}", totalApprovedVal);
                        Decimal priceDiference = Convert.ToDecimal(lblCollectVal.Text) - Convert.ToDecimal(lblApprVal.Text);
                        lblPriceDeference.Text = string.Format("{0:n2}", priceDiference);

                        //lblReceiptAmt.Text = priceDiference.ToString();
                        if (priceDiference > 0)
                        {
                            btnConfirm.Enabled = false;
                            Button10.Visible = true; //panel_manualReceipt.Visible = true;

                            btn_SendReq.Enabled = false;
                            lblReceiptAmt.Text = priceDiference.ToString();

                            ucPayModes1.ClearControls();
                            ucPayModes1.InvoiceType = "CS";
                            //ucPayModes1.LoadPayModes();
                            ucPayModes1.TotalAmount = priceDiference;
                            ucPayModes1.LoadData();

                        }
                        else
                        {
                            lblReceiptAmt.Text = string.Format("{0:n2}", 0);
                            btnConfirm.Enabled = true;
                            Button10.Visible = false;// panel_manualReceipt.Visible = false;
                            ucPayModes1.ClearControls();
                            ucPayModes1.InvoiceType = "CS";
                            //ucPayModes1.LoadPayModes();
                            ucPayModes1.TotalAmount = 0;
                            ucPayModes1.LoadData();
                            //ucPayModes1.LoadPayModes();
                        }
                        // }
                        #endregion
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
            //----------------------------------------------------------------
        }
        private void deleteItem(DataGridViewRow selectedRow)
        {
            try
            {
                // List<InventoryAdhocDetail> AdhodDetList
                //Int32 rowIndex = e.RowIndex;

                DataGridViewRow row = selectedRow;

                //string DelItemCD = grvItmDes.Rows[rowIndex].Cells[1].Text;
                string DelItemCD = row.Cells["Iadd_claim_itm"].Value.ToString();

                // string DelModle = grvItmDes.Rows[rowIndex].Cells[2].Text;
                string DelModle = row.Cells["Modle"].Value.ToString();

                // AdhodDetList.RemoveAll(x => x.Iadd_app_itm == DelItemCD );//&& x.Iadd_anal2 == DelModle
                ComboboxItem action = (ComboboxItem)ddlAction.SelectedItem;
                //if (ddlAction.SelectedValue == "Request")
                if (action.Value.ToString() == "Request")
                {
                    AdhodDetList.RemoveAll(x => x.Iadd_claim_itm == DelItemCD);//&& x.Iadd_anal2 == DelModle
                }
                else
                {
                    AdhodDetList.RemoveAll(x => x.Iadd_app_itm == DelItemCD);//&& x.Iadd_anal2 == DelModle
                }

                grvItmDes.DataSource = null;
                grvItmDes.AutoGenerateColumns = false;

                BindingSource _source = new BindingSource();
                _source.DataSource = AdhodDetList;
                grvItmDes.DataSource = _source;
                //grvItmDes.DataSource = AdhodDetList;
                //  grvItmDes.DataBind();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow gvr in this.grvAvailableSerials.Rows)
                {
                    // CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("chekSelect1");

                    DataGridViewCheckBoxCell chk = gvr.Cells["chekSelect1"] as DataGridViewCheckBoxCell;

                    //  Int32 serID = Convert.ToInt32(gvr.Cells[7].Text);
                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        // Label lblSerID = (Label)gvr.Cells[4].FindControl("lblSerID_av");//Convert.ToInt32(gvr.Cells[4].FindControl("lblSerID_av"));
                        string lblSerID = gvr.Cells["lblSerID_av"].Value.ToString();
                        Int32 serID = Convert.ToInt32(lblSerID);
                        string ItemCD = gvr.Cells["Tus_itm_cd"].Value.ToString();
                        List<ReptPickSerials> serList = new List<ReptPickSerials>();

                        var _dup = from _l in AvailableSerialList
                                   where _l.Tus_itm_cd == ItemCD && _l.Tus_ser_id == serID
                                   select _l;


                        // serList= _dup.ToList<ReptPickSerials>();
                        Approved_SerialList.AddRange(_dup);

                        AvailableSerialList.RemoveAll(x => x.Tus_itm_cd == ItemCD && x.Tus_ser_id == serID);//&& x.Iadd_anal2 == DelModle


                    }

                    //serList = List<ReptPickSerials> (_dup);
                }
                grvApproveItms.DataSource = null;
                grvApproveItms.AutoGenerateColumns = false;
                // grvApproveItms.DataSource = Approved_SerialList;
                BindingSource _source = new BindingSource();
                _source.DataSource = Approved_SerialList;
                grvApproveItms.DataSource = _source;
                //grvApproveItms.DataBind();


                grvAvailableSerials.DataSource = null;
                grvAvailableSerials.AutoGenerateColumns = false;
                //grvAvailableSerials.DataSource = AvailableSerialList;
                BindingSource _source2 = new BindingSource();
                _source2.DataSource = AvailableSerialList;
                grvAvailableSerials.DataSource = _source2;
                //grvAvailableSerials.DataBind();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow gvr in this.grvApproveItms.Rows)
                {
                    // CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("chekSelect2");
                    // DataGridViewCheckBoxCell chkSelect = gvr.Cells["chekSelect2"] as DataGridViewCheckBoxCell;

                    //  Label lblSerID_appr = (Label)(gvr.Cells[4].FindControl("lblSerID_appr"));                
                    Int32 serID = Convert.ToInt32(gvr.Cells["lblSerID_appr"].Value.ToString());

                    // string DEL_ItemCD = gvr.Cells[1].Text.Trim().ToString();
                    string DEL_ItemCD = gvr.Cells["Tus_itm_cd2"].Value.ToString();

                    List<ReptPickSerials> serList = new List<ReptPickSerials>();

                    var _dup = from _l in Approved_SerialList
                               where _l.Tus_itm_cd == DEL_ItemCD && _l.Tus_ser_id == serID
                               select _l;
                    AvailableSerialList.AddRange(_dup);

                    Approved_SerialList.RemoveAll(x => x.Tus_itm_cd == DEL_ItemCD && x.Tus_ser_id == serID);//&& x.Iadd_anal2 == DelModle

                }
                grvApproveItms.DataSource = null;
                grvApproveItms.AutoGenerateColumns = false;
                grvApproveItms.DataSource = Approved_SerialList;
                //grvApproveItms.DataBind();
                grvAvailableSerials.DataSource = null;
                grvAvailableSerials.AutoGenerateColumns = false;
                grvAvailableSerials.DataSource = AvailableSerialList;
                //grvAvailableSerials.DataBind();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void grvApproveItms_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex != -1)
                {
                    // CheckBox chkSelect = (CheckBox)gvr.Cells[0].FindControl("chekSelect2");
                    // DataGridViewCheckBoxCell chkSelect = gvr.Cells["chekSelect2"] as DataGridViewCheckBoxCell;
                    grvApproveItms.Rows[e.RowIndex].Selected = true;
                    DataGridViewRow gvr = grvApproveItms.SelectedRows[0];

                    Int32 serID = Convert.ToInt32(gvr.Cells["lblSerID_appr"].Value.ToString());

                    string DEL_ItemCD = gvr.Cells["Tus_itm_cd2"].Value.ToString();

                    List<ReptPickSerials> serList = new List<ReptPickSerials>();

                    var _dup = from _l in Approved_SerialList
                               where _l.Tus_itm_cd == DEL_ItemCD && _l.Tus_ser_id == serID
                               select _l;
                    AvailableSerialList.AddRange(_dup);

                    Approved_SerialList.RemoveAll(x => x.Tus_itm_cd == DEL_ItemCD && x.Tus_ser_id == serID);//&& x.Iadd_anal2 == DelModle

                    grvApproveItms.DataSource = null;
                    grvApproveItms.AutoGenerateColumns = false;
                    //grvApproveItms.DataSource = Approved_SerialList;
                    BindingSource _source = new BindingSource();
                    _source.DataSource = Approved_SerialList;
                    grvApproveItms.DataSource = _source;
                    //grvApproveItms.DataBind();
                    if (grvAvailableSerials.Rows.Count > 0 && grvAvailableSerials.Rows[0].Cells["Tus_itm_cd"].Value.ToString() == DEL_ItemCD)
                    {
                        grvAvailableSerials.DataSource = null;
                        grvAvailableSerials.AutoGenerateColumns = false;
                        // grvAvailableSerials.DataSource = AvailableSerialList;
                        BindingSource _source1 = new BindingSource();
                        _source1.DataSource = AvailableSerialList;
                        grvAvailableSerials.DataSource = _source1;
                    }

                    //grvAvailableSerials.DataBind();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Searched_AdhodHeader == null)
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please send the request first!");
                    MessageBox.Show("Please send the request first!");
                    return;
                }
                if (MessageBox.Show("Are you sure you want to Reject?", "Confirm Reject", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                //Searched_AdhodHeader.Iadh_coll_by = GlbUserID;
                //Searched_AdhodHeader.Iadh_coll_dt = 
                Searched_AdhodHeader.Iadh_stus = 2;
                Searched_AdhodHeader.Iadh_app_by = BaseCls.GlbUserID; //rejected person
                Searched_AdhodHeader.Iadh_app_dt = CHNLSVC.Security.GetServerDateTime().Date;//rejected date
                Int32 effect = 0;
                effect = CHNLSVC.Inventory.Save_Adhoc_Confirm(Searched_AdhodHeader, null);
                if (effect > 0)
                {
                    clearCompleateScreen();
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Rejected!");
                    MessageBox.Show("Successfully Rejected!");
                    return;
                }
                else
                {
                    MessageBox.Show("Not Rejected!");
                    return;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (Searched_AdhodHeader == null)
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please send the request first!");
                    MessageBox.Show("Please send the request first!");
                    return;
                }
                // string _OrgPC = txtPC.Text.Trim();
                if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "INV5"))
                {

                }
                else
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "No Permission Granted!");
                    MessageBox.Show("No Permission Granted! \n (required permission type: 'INV5')");
                    return;
                }
                if (MessageBox.Show("Are you sure you want to Approve?", "Confirm Approve", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                //-----------------------------------------------------------------------------------------
                if (Approved_SerialList.Count > 0)
                {
                    List<InventoryAdhocDetail> approved_detList = new List<InventoryAdhocDetail>();
                    //Approve the requested items.
                    InventoryAdhocDetail Det = null;
                    foreach (ReptPickSerials rps in Approved_SerialList)
                    {
                        #region fill Approved detail

                        Det = new InventoryAdhocDetail();
                        //Det.Iadd_anal1 = 1; //not sure
                        // Decimal TotalAppr = searchedAdhocDetList.Find(x => x.Iadd_claim_itm == rps.Tus_itm_cd);                  
                        foreach (InventoryAdhocDetail adhocDet in searchedAdhocDetList)
                        {
                            if (adhocDet.Iadd_claim_itm == rps.Tus_itm_cd)
                            {
                                Det.Iadd_anal1 = adhocDet.Iadd_anal1;
                            }

                        }
                        // Det.Iadd_anal1 = Convert.ToDecimal(rps.Tus_new_remarks);//the qty requested/approved


                        Det.Iadd_anal2 = rps.Tus_itm_model;
                        Det.Iadd_anal3 = rps.Tus_itm_desc; ;
                        Det.Iadd_anal4 = rps.Tus_ser_id;
                        //Det.Iadd_anal5 =;
                        Det.Iadd_app_itm = rps.Tus_itm_cd;
                        Det.Iadd_app_pb = txtPriceBook.Text.Trim().ToUpper();
                        Det.Iadd_app_pb_lvl = txtPBLevel.Text.Trim().ToUpper();
                        if (txtUnitPrice.Text.Trim() != "")
                        {
                            Det.Iadd_app_val = Convert.ToDecimal(txtUnitPrice.Text.Trim());
                        }

                        Det.Iadd_claim_itm = rps.Tus_itm_cd;
                        //Det.Iadd_claim_pb =;
                        //Det.Iadd_claim_pb_lvl = ;
                        //Det.Iadd_claim_val = ;
                        //Det.Iadd_coll_itm =;
                        //Det.Iadd_coll_pb = ;
                        //Det.Iadd_coll_pb_lvl = ;
                        //Det.Iadd_coll_ser1 = ;
                        //Det.Iadd_coll_ser2 = ;,
                        //Det.Iadd_coll_ser3 = ;
                        //Det.Iadd_coll_ser3 = ;
                        //Det.Iadd_coll_val = ;
                        Det.Iadd_line = ItmLine++;
                        //Det.Iadd_ref_no =;
                        //Det.Iadd_seq = ;
                        Det.Iadd_stus = 1;
                        #endregion

                        
                        
                        approved_detList.Add(Det);
                    }

                    //try {
                    //Update header
                    if (Searched_AdhodHeader == null)
                    {
                        Searched_AdhodHeader = new InventoryAdhocHeader();
                    }
                    Searched_AdhodHeader.Iadh_ref_no = txtRefNo.Text.Trim().ToUpper();
                    Searched_AdhodHeader.Iadh_app_by = BaseCls.GlbUserID;
                    Searched_AdhodHeader.Iadh_app_dt = CHNLSVC.Security.GetServerDateTime().Date;
                    Searched_AdhodHeader.Iadh_stus = 1;
                    //call approve method
                    if (approved_detList.Count < 1)
                    {
                        MessageBox.Show("Add serials to Selected Serial List!");
                        return;
                    }

                    Int32 effect = 0;
                    effect = CHNLSVC.Inventory.Save_Adhoc_Approve(Searched_AdhodHeader, approved_detList);
                    if (effect < 0)
                    {
                        clearCompleateScreen();
                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Failed to Approve. Error occured!");
                        MessageBox.Show("Failed to Approve. Error occured!");
                        return;
                    }
                    else if (effect > 0)
                    {
                        clearCompleateScreen();

                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Approved Successfully!");
                        MessageBox.Show("Approved Successfully!");
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (Searched_AdhodHeader == null)
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please send the request first!");
                    MessageBox.Show("Please send the request first!");
                    return;
                }
                if (MessageBox.Show("Do you want to Reject?", "Confirm Reject", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                //Searched_AdhodHeader.Iadh_coll_by = GlbUserName;
                //Searched_AdhodHeader.Iadh_coll_dt =
                Searched_AdhodHeader.Iadh_stus = 2;
                Searched_AdhodHeader.Iadh_app_by = BaseCls.GlbUserID;  //rejected person
                Searched_AdhodHeader.Iadh_app_dt = CHNLSVC.Security.GetServerDateTime().Date;//rejected date
                Int32 effect = 0;
                effect = CHNLSVC.Inventory.Save_Adhoc_Confirm(Searched_AdhodHeader, null);
                if (effect > 0)
                {
                    clearCompleateScreen();
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Rejected!");
                    MessageBox.Show("Successfully Rejected!");
                    return;
                }
                else
                {
                    MessageBox.Show("Not Rejected!");
                    return;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (Panel_PriceDet.Visible == false)
                {
                    this.btnItmAdd_Click(sender, e);
                }
                else
                {
                    txtPriceBook.Focus();
                }
            }
        }

        private void txtPBLevel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.txtPBLevel_Leave(sender, e);
                txtFgapLoc.Focus();
            }
        }

        private void txtFgapLoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txtPC.Focus();
            }
        }

        private void txtPC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txtQty.Focus();
                this.btnItmAdd_Click(sender, e);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (Searched_AdhodHeader == null)
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please send the request first!");
                    MessageBox.Show("Please send the request first!");
                    return;
                }

                if (Approved_SerialList.Count < 1)
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please add serials to Confirm List!");
                    MessageBox.Show("Please add serials to Confirm List!");
                    return;
                }

                if (ddlReuestType.Text == "FIXED ASSETS")
                {
                    ////if (BaseCls.GlbUserComCode == "ABL")
                    ////{
                    //    //Tharindu 2018-06-06
                    //    DataTable DT = CHNLSVC.MsgPortal.CheckValidItmInFixAsset(txtItemCD.Text.Trim().ToUpper());
                    //    if (DT == null || DT.Rows.Count == 0)
                    //    {
                    //        MessageBox.Show("This Item is not Available in fixed Asset system, Please Contact Account Department", "Fixed Asset", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        txtModel.Text = "";
                    //        txtItmDescription.Text = "";
                    //        txtItemCD.Text = "";
                    //        return;
                    //    }
                    ////}
                    string fixloc = LoadFixedAssetLocation().ToString();
                    if (string.IsNullOrEmpty(fixloc))
                    {
                        MessageBox.Show("FixAsset Loc Not Avilable in this location", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        txtSendLoc.Text = fixloc;
                    }

                    string chkfixloc = CheckFixAssetlocAvailability(fixloc).ToString();
                    if (string.IsNullOrEmpty(chkfixloc))
                    {
                        MessageBox.Show("FixAsset Loc Not Avilable in Fix asset db", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (MessageBox.Show("Are you sure you want to Confirm?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                //--------------------23-01-2013-------------------------------**
                Decimal selectedQty = Approved_SerialList.Count;
                
                Decimal requestQty = 0;

                foreach (DataGridViewRow gvr in this.grvItmDes.Rows)
                {
                    string reqQty = gvr.Cells["reqQty"].Value.ToString();
                    requestQty = Convert.ToInt32(reqQty) + requestQty;
                }
                if (selectedQty < requestQty && Searched_AdhodHeader.Iadh_tp == 2)
                {

                    if (MessageBox.Show("Request item count is less than Selected item count. \n Do you want to proceed?", "Confirm save", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }

                }
                else if (selectedQty > requestQty && Searched_AdhodHeader.Iadh_tp == 2)
                {
                    MessageBox.Show("Cannot confirm more than requested Qty.");
                    return;
                }


                //-------------------------------------------------------------**

                List<InventoryAdhocDetail> confirmed_detList = new List<InventoryAdhocDetail>();
                //Approve the requested items.
                InventoryAdhocDetail Det = null;
                foreach (ReptPickSerials rps in Approved_SerialList)
                {
                    #region fill Confirm detail

                    Det = new InventoryAdhocDetail();
                    Det.Iadd_anal1 = 1; //not sure

                    Det.Iadd_anal2 = rps.Tus_itm_model;
                    Det.Iadd_anal3 = rps.Tus_itm_desc;
                    Det.Iadd_anal4 = rps.Tus_ser_id;
                    //Det.Iadd_anal5 =;
                    Det.Iadd_app_itm = rps.Tus_itm_cd;
                    Det.Iadd_app_pb = txtPriceBook.Text.Trim().ToUpper();
                    Det.Iadd_app_pb_lvl = txtPBLevel.Text.Trim().ToUpper();
                    
                    if (txtUnitPrice.Text.Trim() != "")
                    {
                        Det.Iadd_app_val = Convert.ToDecimal(txtUnitPrice.Text.Trim());
                    }

                    Det.Iadd_claim_itm = rps.Tus_itm_cd;
                    //Det.Iadd_claim_pb =;
                    //Det.Iadd_claim_pb_lvl = ;
                    //Det.Iadd_claim_val = ;
                    Det.Iadd_coll_itm = rps.Tus_itm_cd;
                    //Det.Iadd_coll_pb = ;
                    //Det.Iadd_coll_pb_lvl = ;
                    //Det.Iadd_coll_ser1 = ;
                    //Det.Iadd_coll_ser2 = ;,
                    //Det.Iadd_coll_ser3 = ;
                    //Det.Iadd_coll_ser3 = ;
                    //Det.Iadd_coll_val = ;
                    Det.Iadd_line = ItmLine++;
                    //Det.Iadd_ref_no =;
                    //Det.Iadd_seq = ;
                    Det.Iadd_stus = 3;
                    #endregion

                    confirmed_detList.Add(Det);
                }

                //**********24-01-2013*****************
                if (Searched_AdhodHeader.Iadh_tp == 1)
                {
                    Decimal totalVal = 0;
                    Decimal selectedTotVal = 0;
                    foreach (InventoryAdhocDetail detail in AdhodDetList)
                    {
                        totalVal = totalVal + detail.Iadd_app_val * Convert.ToDecimal(detail.Iadd_anal1);
                    }
                    foreach (ReptPickSerials rps in Approved_SerialList)
                    {
                        selectedTotVal = selectedTotVal + rps.Tus_unit_price;
                    }
                    if (totalVal < selectedTotVal)
                    {
                        MessageBox.Show("Cannot collect more than approved amount!");
                        return;
                    }
                }
                //***************************
                //try {
                //Update header
                // Searched_AdhodHeader.Iadh_ref_no = txtRefNo.Text.Trim().ToUpper();
                // Searched_AdhodHeader.Iadh_app_by = GlbUserName;
                // Searched_AdhodHeader.Iadh_app_dt =
                // Searched_AdhodHeader.Iadh_stus = 1;
                Searched_AdhodHeader.Iadh_coll_by = BaseCls.GlbUserID;
                Searched_AdhodHeader.Iadh_coll_dt = CHNLSVC.Security.GetServerDateTime().Date;
                Searched_AdhodHeader.Iadh_stus = 3;

                #region ADJ(-)
                //string AdjNumber = txtAdjustmentNo.Text.Trim();
                //string AdjNumber = "";
                //string manualNum = txtManualRefNo.Text.Trim();
                //string remarks = txtRemarks.Text.Trim();
                //string adj_base = ddlAdjBased.SelectedValue;
                //string adj_sub_type = ddlAdjSubTyepe.SelectedValue;
                //string adj_type = ddlAdjType_.SelectedValue;
                InventoryHeader inHeader = new InventoryHeader();


                //inHeader.Ith_acc_no = "";
                inHeader.Ith_anal_1 = "";
                inHeader.Ith_anal_10 = false;
                inHeader.Ith_anal_11 = false;
                inHeader.Ith_anal_12 = false;
                inHeader.Ith_anal_2 = "";
                inHeader.Ith_anal_3 = "";
                inHeader.Ith_anal_4 = "";
                inHeader.Ith_anal_5 = "";
                inHeader.Ith_anal_6 = 0;
                inHeader.Ith_anal_7 = 0;
                inHeader.Ith_anal_8 = DateTime.MinValue;
                inHeader.Ith_anal_9 = DateTime.MinValue;
                inHeader.Ith_bus_entity = "";
                //inHeader.Ith_cate_tp = ddlAdjSubTyepe.SelectedValue.ToString();
                inHeader.Ith_channel = "";
                inHeader.Ith_com = BaseCls.GlbUserComCode;

                //inHeader.Ith_com ="";
                inHeader.Ith_com_docno = "";
                inHeader.Ith_cre_by = "";
                inHeader.Ith_cre_when = DateTime.MinValue;
                inHeader.Ith_del_add1 = "";
                inHeader.Ith_del_add2 = "";
                inHeader.Ith_del_code = "";

                inHeader.Ith_del_party = "";
                inHeader.Ith_del_town = "";


                inHeader.Ith_direct = true;
                //  inHeader.Ith_direct =true;
                inHeader.Ith_doc_date = DateTime.Today;
                //  inHeader.Ith_doc_date  =DateTime.MinValue;
                inHeader.Ith_doc_no = "";//"DPS32-ADJ-12-588888";

                inHeader.Ith_doc_tp = "ADJ";
                //   inHeader.Ith_doc_tp ="";
                inHeader.Ith_doc_year = DateTime.Today.Year;
                inHeader.Ith_entry_no = "";
                inHeader.Ith_entry_tp = "";
                inHeader.Ith_git_close = true;
                inHeader.Ith_git_close_date = DateTime.MinValue;
                inHeader.Ith_git_close_doc = "";
                inHeader.Ith_isprinted = true;
                inHeader.Ith_is_manual = true;
                inHeader.Ith_job_no = "";
                inHeader.Ith_loading_point = "";
                inHeader.Ith_loading_user = "";
                //inHeader.Ith_loc = BaseCls.GlbUserDefLoca; - commented by Prabhath on 19022014 and added txtSendLoc.Text.trim
                inHeader.Ith_loc = BaseCls.GlbUserDefLoca;
                //if (txtManualRefNo.Text == null || txtManualRefNo.Text == "")
                //{
                //    inHeader.Ith_manual_ref = "N/A";
                //}
                //else
                //{
                //    inHeader.Ith_manual_ref = txtManualRefNo.Text.Trim();
                //}
                inHeader.Ith_manual_ref = "N/A";
                // inHeader.Ith_manual_ref ="";
                inHeader.Ith_mod_by = BaseCls.GlbUserID;//"ADMIN";
                inHeader.Ith_mod_when = DateTime.MinValue;
                inHeader.Ith_noofcopies = 1;
                inHeader.Ith_oth_loc = "";

                inHeader.Ith_remarks = "ADHOC CONFIRM";//txtRemarks.Text;
                // inHeader.Ith_remarks ="";
                inHeader.Ith_sbu = "INV";
                //inHeader.Ith_seq_no = 6; removed by Chamal 12-05-2013
                //inHeader.Ith_seq_no =54;
                inHeader.Ith_session_id = BaseCls.GlbUserSessionID;
                inHeader.Ith_stus = "A";
                inHeader.Ith_sub_tp = (CommonUIDefiniton.AdjustmentType.ADHOC).ToString(); //ddlAdjSubTyepe.SelectedValue.ToString();
                // inHeader.Ith_sub_tp ="";
                inHeader.Ith_vehi_no = "";

                inHeader.Ith_direct = false;

                //---------------updated on 07-05-2013 according to Chamal's advice------------------------
                inHeader.Ith_anal_12 = false;
                inHeader.Ith_oth_docno = Searched_AdhodHeader.Iadh_ref_no;
                inHeader.Ith_cre_by = BaseCls.GlbUserID;
                inHeader.Ith_noofcopies = 0;
                inHeader.Ith_isprinted = false;
                inHeader.Ith_git_close = false;
                inHeader.Ith_is_manual = false;
                inHeader.Ith_oth_loc = BaseCls.GlbUserDefLoca;
                inHeader.Ith_oth_com = BaseCls.GlbUserComCode;
                inHeader.Ith_sub_tp = "SYS";
                inHeader.Ith_entry_tp = "SYS";
                inHeader.Ith_acc_no = "FASET_ADH_ADJ"; //Edit Chamal 15-05-2013
                inHeader.Ith_cate_tp = Searched_AdhodHeader.Iadh_tp == 2 ? "FIXED" : "FGAP";
                inHeader.Ith_remarks = "ADHOC CONFIRM";
                //------------------------------------------
                inHeader.Invoice_no = txtRefNo.Text.Trim().ToUpper();

                //*********************************
                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                masterAuto.Aut_cate_tp = "LOC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "ADJ";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "ADJ";
                masterAuto.Aut_year = null;
                //  masterAuto.Aut_year = Convert.ToDateTime(txtDate_.Text).Year;

                #endregion
                //------------------------------------------------------------
                if (Searched_AdhodHeader.Iadh_tp == 2)
                {
                    #region
                    string location = txtSendLoc.Text.Trim().ToUpper();
                    InventoryAdhocHeader Header;
                    List<InventoryAdhocDetail> det_list = CHNLSVC.Inventory.GET_adhocDET_byRefNo(BaseCls.GlbUserComCode, location, 2, txtRefNo.Text.Trim(), 0, out Header);

                    foreach (InventoryAdhocDetail invdet in AdhodDetList)
                    {
                        var _dup = from _l in confirmed_detList
                                   where _l.Iadd_claim_itm == invdet.Iadd_app_itm//&& _l.Iadd_anal4 == ApprSerID.ToString()
                                   select _l;
                        Decimal count_CONF = _dup.Count();

                        Decimal count_REQ = 0;

                        //var _dup2 = from _l in det_list
                        //            where _l.Iadd_claim_itm == invdet.Iadd_claim_itm//&& _l.Iadd_anal4 == ApprSerID.ToString()
                        //            select _l;
                        //count_REQ = _dup2.Count();
                        foreach (InventoryAdhocDetail req_det in searchedAdhocDetList)
                        {
                            if (req_det.Iadd_claim_itm == invdet.Iadd_app_itm)
                            {
                                count_REQ = req_det.Iadd_anal1;
                            }
                        }


                        if (count_CONF > count_REQ)
                        {
                            // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Cannot Exceed the request item Qty.!");
                            MessageBox.Show("Cannot Exceed the request item Qty.!");
                            return;
                        }
                    }


                    //call confirm method                

                    string AdjNumber = string.Empty;
                    List<ReptPickSerialsSub> rps_subList = new List<ReptPickSerialsSub>();
                    Int16 adjEffect = CHNLSVC.Inventory.ADJMinus(inHeader, Approved_SerialList, rps_subList, masterAuto, out AdjNumber);
                    Searched_AdhodHeader.Iadh_adj_no = AdjNumber;

                    #region ADJ(+)        
                    InventoryHeader inHeader_fix = new InventoryHeader();

                    inHeader_fix.Ith_anal_1 = "";
                    inHeader_fix.Ith_anal_10 = false;
                    inHeader_fix.Ith_anal_11 = false;
                    inHeader_fix.Ith_anal_12 = false;
                    inHeader_fix.Ith_anal_2 = "";
                    inHeader_fix.Ith_anal_3 = "";
                    inHeader_fix.Ith_anal_4 = "";
                    inHeader_fix.Ith_anal_5 = "";
                    inHeader_fix.Ith_anal_6 = 0;
                    inHeader_fix.Ith_anal_7 = 0;
                    inHeader_fix.Ith_anal_8 = DateTime.MinValue;
                    inHeader_fix.Ith_anal_9 = DateTime.MinValue;
                    inHeader_fix.Ith_bus_entity = "";
                    //inHeader_fix_fix.Ith_cate_tp = ddlAdjSubTyepe.SelectedValue.ToString();
                    inHeader_fix.Ith_channel = "";
                    inHeader_fix.Ith_com = BaseCls.GlbUserComCode;

                    //inHeader_fix_fix.Ith_com ="";
                    inHeader_fix.Ith_com_docno = "";
                    inHeader_fix.Ith_cre_by = "";
                    inHeader_fix.Ith_cre_when = DateTime.MinValue;
                    inHeader_fix.Ith_del_add1 = "";
                    inHeader_fix.Ith_del_add2 = "";
                    inHeader_fix.Ith_del_code = "";

                    inHeader_fix.Ith_del_party = "";
                    inHeader_fix.Ith_del_town = "";


                    inHeader_fix.Ith_direct = true;
                    //  inHeader_fix.Ith_direct =true;
                    inHeader_fix.Ith_doc_date = DateTime.Today;
                    //  inHeader_fix.Ith_doc_date  =DateTime.MinValue;
                    inHeader_fix.Ith_doc_no = "";//"DPS32-ADJ-12-588888";

                    inHeader_fix.Ith_doc_tp = "ADJ";
                    //   inHeader_fix.Ith_doc_tp ="";
                    inHeader_fix.Ith_doc_year = DateTime.Today.Year;
                    inHeader_fix.Ith_entry_no = "";
                    inHeader_fix.Ith_entry_tp = "";
                    inHeader_fix.Ith_git_close = true;
                    inHeader_fix.Ith_git_close_date = DateTime.MinValue;
                    inHeader_fix.Ith_git_close_doc = "";
                    inHeader_fix.Ith_isprinted = true;
                    inHeader_fix.Ith_is_manual = true;
                    inHeader_fix.Ith_job_no = "";
                    inHeader_fix.Ith_loading_point = "";
                    inHeader_fix.Ith_loading_user = "";
                    //inHeader_fix.Ith_loc = BaseCls.GlbUserDefLoca; - commented by Prabhath on 19022014 and added txtSendLoc.Text.trim
                    inHeader_fix.Ith_loc = txtSendLoc.Text.Trim();
                   
                    inHeader_fix.Ith_manual_ref = "N/A";
                    // inHeader_fix.Ith_manual_ref ="";
                    inHeader_fix.Ith_mod_by = BaseCls.GlbUserID;//"ADMIN";
                    inHeader_fix.Ith_mod_when = DateTime.MinValue;
                    inHeader_fix.Ith_noofcopies = 1;
                    inHeader_fix.Ith_oth_loc = "";

                    inHeader_fix.Ith_remarks = "ADHOC CONFIRM";//txtRemarks.Text;
                    // inHeader_fix.Ith_remarks ="";
                    inHeader_fix.Ith_sbu = "INV";
                    //inHeader_fix.Ith_seq_no = 6; removed by Chamal 12-05-2013
                    //inHeader_fix.Ith_seq_no =54;
                    inHeader_fix.Ith_session_id = BaseCls.GlbUserSessionID;
                    inHeader_fix.Ith_stus = "A";
                    inHeader_fix.Ith_sub_tp = (CommonUIDefiniton.AdjustmentType.ADHOC).ToString(); //ddlAdjSubTyepe.SelectedValue.ToString();
                    // inHeader_fix.Ith_sub_tp ="";
                    inHeader_fix.Ith_vehi_no = "";

                    inHeader_fix.Ith_direct = false;

                    //---------------updated on 07-05-2013 according to Chamal's advice------------------------
                    inHeader_fix.Ith_anal_12 = false;
                    inHeader_fix.Ith_oth_docno = Searched_AdhodHeader.Iadh_ref_no;
                    inHeader_fix.Ith_cre_by = BaseCls.GlbUserID;
                    inHeader_fix.Ith_noofcopies = 0;
                    inHeader_fix.Ith_isprinted = false;
                    inHeader_fix.Ith_git_close = false;
                    inHeader_fix.Ith_is_manual = false;
                    inHeader_fix.Ith_oth_loc = BaseCls.GlbUserDefLoca;
                    inHeader_fix.Ith_oth_com = BaseCls.GlbUserComCode;
                    inHeader_fix.Ith_sub_tp = "SYS";
                    inHeader_fix.Ith_entry_tp = "SYS";
                    inHeader_fix.Ith_acc_no = "FASET_ADH_ADJ"; //Edit Chamal 15-05-2013
                    inHeader_fix.Ith_cate_tp = Searched_AdhodHeader.Iadh_tp == 2 ? "FIXED" : "FGAP";
                    inHeader_fix.Ith_remarks = "ADHOC CONFIRM";
                    //------------------------------------------
                    inHeader_fix.Invoice_no = txtRefNo.Text.Trim().ToUpper();

                    //*********************************
                    MasterAutoNumber masterAuto_fix = new MasterAutoNumber();
                    masterAuto_fix.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                    masterAuto_fix.Aut_cate_tp = "LOC";
                    masterAuto_fix.Aut_direction = null;
                    masterAuto_fix.Aut_modify_dt = null;
                    masterAuto_fix.Aut_moduleid = "ADJ";
                    masterAuto_fix.Aut_number = 5;//what is Aut_number
                    masterAuto_fix.Aut_start_char = "ADJ";
                    masterAuto_fix.Aut_year = null;
                    //  masterAuto.Aut_year = Convert.ToDateTime(txtDate_.Text).Year;

                    #endregion

                    DataTable dt1 = CHNLSVC.MsgPortal.GetBinlocationFixasset(BaseCls.GlbUserComCode, txtSendLoc.Text);


                    if (dt1 != null || dt1.Rows.Count > 0)
                    {
                        foreach (ReptPickSerials rps in Approved_SerialList)
                        {
                            rps.Tus_bin = dt1.Rows[0][0].ToString();

                        }
                    }


                    Int16 adjEffect1 = CHNLSVC.Inventory.ADJPlus_FIXA(inHeader_fix, Approved_SerialList, rps_subList, masterAuto, out AdjNumber, false, true);
                    Searched_AdhodHeader.Iadh_adj_no = AdjNumber;

                    Int16 adjEffect2 = CHNLSVC.Inventory.SaveAdjMinAdjplus(inHeader, Approved_SerialList, rps_subList, masterAuto,inHeader_fix,out AdjNumber,false, false);
                    
                    if(adjEffect2 > 0)
                    {
                        Int32 effect = 0;
                        effect = CHNLSVC.Inventory.Save_Adhoc_Confirm(Searched_AdhodHeader, confirmed_detList);
                        if (effect < 0)
                        {
                            //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Failed to Confirmed. Error occured. Try Again!");
                            MessageBox.Show("Failed to Confirmed. Error occured. Try Again!");
                        }
                        else if (effect > 0)
                        {
                            // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Confirmed Successfully!  ADJ(-) NO. =" + AdjNumber);
                            MessageBox.Show("Confirmed Successfully!  ADJ(+) NO. =" + AdjNumber);
                            try
                            {
                                Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                                _view.GlbReportName = "FixedAssetConfirmationNotes.rpt";
                                _view.GlbReportDoc = Searched_AdhodHeader.Iadh_ref_no;//"FIX-000001";( Ref #)
                                _view.Show();
                                _view = null;
                            }
                            catch (Exception ex)
                            {
                                return;
                            }

                            clearCompleateScreen();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Data Not Save. Error occured. Try Again!");
                    }


                    

                    //}
                    //catch(Exception ex){
                    //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Error occured!");
                    //    return;
                    //}
                    #endregion
                }
                if (Searched_AdhodHeader.Iadh_tp == 1)
                {
                    //if (lblReceiptAmt.Text != "0")              
                    if (Convert.ToDecimal(lblReceiptAmt.Text) != 0)
                    {
                        if (Convert.ToDecimal(lblReceiptAmt.Text) > 0)
                        {
                            //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Please do payments!");
                            MessageBox.Show("Please do payments!");
                            //btnConfirm.Enabled = false;
                            return;
                        }
                    }
                    else
                    {
                        //call confirm method
                        foreach (ReptPickSerials rps in Approved_SerialList)
                        {
                            rps.Tus_cre_by = BaseCls.GlbUserID;
                        }
                        string AdjNumber = string.Empty;
                        List<ReptPickSerialsSub> rps_subList = new List<ReptPickSerialsSub>();
                        Int16 adjEffect = CHNLSVC.Inventory.ADJMinus(inHeader, Approved_SerialList, rps_subList, masterAuto, out AdjNumber);
                        Searched_AdhodHeader.Iadh_adj_no = AdjNumber;

                        //added for printing
                        Gen_ADJ_DocNo = AdjNumber;

                        Int32 effect = 0;
                        effect = CHNLSVC.Inventory.Save_Adhoc_Confirm(Searched_AdhodHeader, confirmed_detList);
                        if (effect < 0)
                        {
                            // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Failed to Confirmed. Error occured. Try Again!");
                            MessageBox.Show("Failed to Confirmed. Error occured. Try Again!");
                            return;
                        }
                        else if (effect > 0)
                        {
                            //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Confirmed Successfully! ADJ(-) NO. = " + AdjNumber);
                            MessageBox.Show("Confirmed Successfully! ADJ(-) NO. =" + AdjNumber);
                            //try {
                            //    Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                            //    _view.GlbReportName = "FixedAssetConfirmationNotes.rpt";
                            //    _view.GlbReportDoc = Searched_AdhodHeader.Iadh_ref_no;//"FIX-000001";( Ref #)
                            //    _view.Show();
                            //    _view = null;
                            //}
                            //catch(Exception ex){
                            //    return;
                            //}
                            try
                            {
                                Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                                BaseCls.GlbReportName = string.Empty; //add on 14-June-2013
                                _view.GlbReportName = string.Empty; //add on 14-June-2013
                                BaseCls.GlbReportTp = "OUTWARD";
                                _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "Outward_Docs.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_Outward_Docs.rpt" : "Outward_Docs.rpt";
                                _view.GlbReportDoc = AdjNumber;//"AAZPG-DO-12-00123";
                                _view.Show();
                                _view = null;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error in print document.");
                            }
                            clearCompleateScreen();

                        }

                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (ucPayModes1.Balance > 0)
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Payment is not done!");
                    MessageBox.Show("Payments is not compleate! \n Please settle the balance.");
                    return;
                }
                _recieptItem = ucPayModes1.RecieptItemList;
                if (_recieptItem == null)
                {
                    return;
                }
                if (Approved_SerialList == null)
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please add serials to Selected list!");
                    MessageBox.Show("Please add serials to Selected list!");
                    return;
                }
                if (Approved_SerialList.Count < 1)
                {
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please add serials to Selected list!");
                    MessageBox.Show("Please add serials to Selected list!");
                    return;

                }
                //if (checkManualReceipt.Checked == true && txtManualReceiptNo.Text.Trim()=="")
                //{
                //    MessageBox.Show("Please Enter Manual receipt No!");
                //    return;
                //}
                //else
                //{
                //    txtManualReceiptNo.Text = "";
                //}
                //if (MessageBox.Show("Are you sure you want to Compleate?", "Confirm Compleate", MessageBoxButtons.YesNo) == DialogResult.No)
                //{
                //    return;
                //}

                //Decimal totalApprovedVal = 0;
                //foreach (InventoryAdhocDetail detail in AdhodDetList)
                //{
                //    totalApprovedVal = totalApprovedVal + detail.Iadd_app_val * Convert.ToDecimal(detail.Iadd_anal1);
                //}
                //if (totalApprovedVal != Convert.ToDecimal(lblCollectVal.Text))
                //{
                //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please add serials to Selected List!");
                //    return;             
                //}

                //*****************************************************************************************************************
                //Decimal count=0;
                //foreach (DataRow dr in grvItmDes.Rows)
                //{
                //    count = count + Convert.ToDecimal(dr["Iadd_anal1"].ToString());
                //}
                //Decimal count2 = grvApproveItms.Rows.Count;
                //if (count != count2)
                //{
                //    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please add serials to Selected list!");
                //    return;
                //}
                //if (BalanceAmount <= 0 )
                //{

                #region Receipt Header Value Assign
                RecieptHeader _recHeader = new RecieptHeader();
                // _recHeader.Sar_acc_no = "";//////////////////////TODO
                //_recHeader.Sar_acc_no = lblAccNo.Text;

                _recHeader.Sar_act = true;
                _recHeader.Sar_com_cd = BaseCls.GlbUserComCode;
                _recHeader.Sar_comm_amt = 0;
                _recHeader.Sar_create_by = BaseCls.GlbUserID;
                _recHeader.Sar_create_when = CHNLSVC.Security.GetServerDateTime().Date;
                //_recHeader.Sar_currency_cd = txtCurrency.Text;
                //_recHeader.Sar_debtor_add_1 = txtAddress.Text;
                //_recHeader.Sar_debtor_add_2 = txtAddress.Text;
                //_recHeader.Sar_debtor_cd = txtCustomer.Text;
                //_recHeader.Sar_debtor_name = txtCusName.Text;
                _recHeader.Sar_direct = true;
                _recHeader.Sar_direct_deposit_bank_cd = "";
                _recHeader.Sar_direct_deposit_branch = "";
                _recHeader.Sar_epf_rate = 0;
                _recHeader.Sar_esd_rate = 0;
                //if (rdoBtnManager.Checked)
                //{
                //    _recHeader.Sar_is_mgr_iss = true;
                //}
                //else { _recHeader.Sar_is_mgr_iss = false; }
                _recHeader.Sar_is_oth_shop = false;// Not sure!
                //_recHeader.Sar_is_oth_shop = false;//////////////////////TODO
                //if (GlbUserDefProf != ddl_Location.SelectedValue)
                //{
                //    _recHeader.Sar_is_oth_shop = true;// Not sure!
                //    _recHeader.Sar_remarks = "OTHER SHOP COLLECTION-" + ddl_Location.SelectedValue;
                //    _recHeader.Sar_oth_sr = ddl_Location.SelectedValue;
                //}
                //else
                //{
                //    _recHeader.Sar_is_oth_shop = false; // Not sure!
                //    _recHeader.Sar_remarks = "COLLECTION";
                //}

                _recHeader.Sar_is_used = false;//////////////////////TODO
                //  _recHeader.Sar_manual_ref_no = txtManualReceiptNo.Text;
                //_recHeader.Sar_mob_no = txtMobile.Text;
                _recHeader.Sar_mod_by = BaseCls.GlbUserID;
                _recHeader.Sar_mod_when = CHNLSVC.Security.GetServerDateTime().Date;
                //_recHeader.Sar_nic_no = txtNIC.Text;


                //  _recHeader.Sar_prefix = "PRFX";//////////////////////TODO
                //_recHeader.Sar_prefix = ddlPrefix.SelectedValue;

                _recHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;

                //_recHeader.Sar_receipt_date = Convert.ToDateTime(txtDate.Text);//////////////////////TODO
                _recHeader.Sar_receipt_date = CHNLSVC.Security.GetServerDateTime().Date;//Convert.ToDateTime(txtReceiptDate.Text.Trim()).Date;

                //_recHeader.Sar_receipt_no = "na";/////////////////////TODO
                //  _recHeader.Sar_receipt_no = txtReceiptNo.Text;

                // _recHeader.Sar_manual_ref_no = txtReceiptNo.Text; //the receipt no
                //_recHeader.Sar_receipt_type = txtInvType.Text;
                //if (rdoBtnManual.Checked)
                //{
                //    _recHeader.Sar_receipt_type = "HPRM";
                //}
                //else { _recHeader.Sar_receipt_type = "HPRS"; }
                _recHeader.Sar_receipt_type = "FGAP";
                _recHeader.Sar_ref_doc = "";
                _recHeader.Sar_remarks = "FGAP RECEIPT";
                _recHeader.Sar_seq_no = 1;
                _recHeader.Sar_ser_job_no = "";
                _recHeader.Sar_session_id = BaseCls.GlbUserSessionID;
                //_recHeader.Sar_tel_no = txtMobile.Text;

                //  _recHeader.Sar_tot_settle_amt = 0;///////////////////////TODO sum_receipt_amt
                _recHeader.Sar_tot_settle_amt = Math.Round(Convert.ToDecimal(lblReceiptAmt.Text), 2);

                _recHeader.Sar_uploaded_to_finance = false;
                _recHeader.Sar_used_amt = 0;//////////////////////TODO
                _recHeader.Sar_wht_rate = 0;
                //if (checkManualReceipt.Checked == true)
                //{
                //    _recHeader.Sar_anal_3 = "MANUAL";
                //    _recHeader.Sar_anal_8 = 1;
                //    _recHeader.Sar_prefix = "AUTO";
                //}
                //else
                //{
                //    _recHeader.Sar_anal_3 = "SYSTEM";
                //    _recHeader.Sar_anal_8 = 0;
                //    _recHeader.Sar_prefix = "AUTO";
                //}
                //_recHeader.Sar_anal_5 = uc_HpAccountSummary1.Uc_Inst_CommRate;
                //_recHeader.Sar_comm_amt = (uc_HpAccountSummary1.Uc_Inst_CommRate * _recHeader.Sar_tot_settle_amt / 100);

                //_recHeader.Sar_anal_6 = uc_HpAccountSummary1.Uc_AdditonalCommisionRate;

                //  _recHeader.Sar_anal_7 = (uc_HpAccountSummary1.Uc_AdditonalCommisionRate * _recHeader.Sar_tot_settle_amt / 100);
                #endregion
                #region Receipt Details creation
                List<RecieptHeader> receiptHeaderList = new List<RecieptHeader>();
                // receiptHeaderList = Receipt_List;
                receiptHeaderList.Add(_recHeader);
                List<RecieptItem> receipItemList = new List<RecieptItem>();
                receipItemList = _recieptItem;
                List<RecieptItem> save_receipItemList = new List<RecieptItem>();
                List<RecieptItem> finish_receipItemList = new List<RecieptItem>();
                Int32 tempHdrSeq = 0;
                foreach (RecieptHeader _h in receiptHeaderList)
                {
                    _h.Sar_seq_no = tempHdrSeq;// Sar_seq_no is changed when saving records
                    fill_Transactions(_h);
                    tempHdrSeq--;
                    Decimal orginal_HeaderTotAmt = _h.Sar_tot_settle_amt;
                    foreach (RecieptItem _i in receipItemList)
                    {
                        if (_i.Sard_settle_amt <= _h.Sar_tot_settle_amt && _i.Sard_settle_amt != 0)
                        {
                            // _i.Sard_receipt_no = _h.Sar_manual_ref_no;
                            //  save_receipItemList.Add(_i);
                            // finish_receipItemList.Add(_i);
                            RecieptItem ri = new RecieptItem();
                            //ri = _i;
                            ri.Sard_settle_amt = _i.Sard_settle_amt;
                            ri.Sard_pay_tp = _i.Sard_pay_tp;
                            ri.Sard_receipt_no = _h.Sar_manual_ref_no;//when saving  ri.Sard_receipt_no is changed 
                            ri.Sard_seq_no = _h.Sar_seq_no;
                            //-------------------------------    //have to copy all properties.
                            ri.Sard_cc_expiry_dt = _i.Sard_cc_expiry_dt;
                            ri.Sard_cc_is_promo = _i.Sard_cc_is_promo;
                            ri.Sard_cc_period = _i.Sard_cc_period;
                            ri.Sard_cc_tp = _i.Sard_cc_tp;
                            ri.Sard_chq_bank_cd = _i.Sard_chq_bank_cd;
                            ri.Sard_chq_branch = _i.Sard_chq_branch;
                            ri.Sard_credit_card_bank = _i.Sard_credit_card_bank;
                            ri.Sard_deposit_bank_cd = _i.Sard_deposit_bank_cd;
                            ri.Sard_deposit_branch = _i.Sard_deposit_branch;
                            //--------------------------------
                            ri.Sard_ref_no = _i.Sard_ref_no;

                            //********
                            ri.Sard_anal_3 = _i.Sard_anal_3;
                            //--------------------------------
                            save_receipItemList.Add(ri);

                            _h.Sar_tot_settle_amt = _h.Sar_tot_settle_amt - _i.Sard_settle_amt;
                            _i.Sard_settle_amt = 0;

                        }
                        else if (_h.Sar_tot_settle_amt != 0 && _i.Sard_settle_amt != 0)
                        {
                            RecieptItem ri = new RecieptItem();
                            ri.Sard_receipt_no = _h.Sar_manual_ref_no;//when saving  ri.Sard_receipt_no is changed 
                            ri.Sard_settle_amt = _h.Sar_tot_settle_amt;//equals to the header's amount.
                            ri.Sard_pay_tp = _i.Sard_pay_tp;
                            ri.Sard_seq_no = _h.Sar_seq_no;
                            //-------------------------------    //have to copy all properties.
                            ri.Sard_cc_expiry_dt = _i.Sard_cc_expiry_dt;
                            ri.Sard_cc_is_promo = _i.Sard_cc_is_promo;
                            ri.Sard_cc_period = _i.Sard_cc_period;
                            ri.Sard_cc_tp = _i.Sard_cc_tp;
                            ri.Sard_chq_bank_cd = _i.Sard_chq_bank_cd;
                            ri.Sard_chq_branch = _i.Sard_chq_branch;
                            ri.Sard_credit_card_bank = _i.Sard_credit_card_bank;
                            ri.Sard_deposit_bank_cd = _i.Sard_deposit_bank_cd;
                            ri.Sard_deposit_branch = _i.Sard_deposit_branch;

                            //--------------------------------
                            ri.Sard_ref_no = _i.Sard_ref_no;
                            //--------------------------------
                            save_receipItemList.Add(ri);
                            _i.Sard_settle_amt = _i.Sard_settle_amt - _h.Sar_tot_settle_amt;
                            _h.Sar_tot_settle_amt = _h.Sar_tot_settle_amt - ri.Sard_settle_amt;

                        }
                    }
                    _h.Sar_tot_settle_amt = orginal_HeaderTotAmt;


                }
                //  gvPayment.DataSource = save_receipItemList;
                //  gvPayment.DataBind();
                #endregion

                #region Receipt AutoNumber Value Assign
                MasterAutoNumber _receiptAuto = new MasterAutoNumber();
                _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                //_receiptAuto.Aut_cate_tp = "PC";
                _receiptAuto.Aut_cate_tp = "PC"; //GlbUserDefProf;
                _receiptAuto.Aut_direction = 1;
                _receiptAuto.Aut_modify_dt = null;
                _receiptAuto.Aut_moduleid = "FGAP";
                _receiptAuto.Aut_number = 0;
                _receiptAuto.Aut_start_char = "FGAP";
                //Fill the Aut_start_char at the saving place (in BLL)
                //if (_h.Sar_receipt_type=="HPRS")
                //{ _receiptAuto.Aut_start_char = "HPRS"; }
                //else if (_h.Sar_receipt_type == "HPRM")
                //{ _receiptAuto.Aut_start_char = "HPRM"; }
                //_receiptAuto.Aut_year = Convert.ToDateTime(txtDate.Text).Year;
                _receiptAuto.Aut_year = null;
                #endregion

                #region Transaction AutoNumber Value Assign
                MasterAutoNumber _transactionAuto = new MasterAutoNumber();
                _transactionAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                // _transactionAuto.Aut_cate_tp = "PC";//change this to GlbUserDefProf
                _transactionAuto.Aut_cate_tp = "PC";//GlbUserDefProf;
                _transactionAuto.Aut_direction = 1;
                _transactionAuto.Aut_modify_dt = null;
                _transactionAuto.Aut_moduleid = "FGAP";
                _transactionAuto.Aut_number = 0;
                _transactionAuto.Aut_start_char = "FGAP";
                _transactionAuto.Aut_year = null;
                #endregion

                Transaction_List = new List<HpTransaction>();
                fill_Transactions(_recHeader);

                //------------------------------------------------------------------------------------------------------
                //CHNLSVC.Inventory.Save_FGAP_confirmation
                //call confirm method

                //****************************************************************************************************************
                List<InventoryAdhocDetail> confirmed_detList = new List<InventoryAdhocDetail>();
                //Approve the requested items.
                InventoryAdhocDetail Det = null;

                foreach (ReptPickSerials rps in Approved_SerialList)
                {

                    #region fill Confirm detail

                    Det = new InventoryAdhocDetail();
                    Det.Iadd_anal1 = 1; //not sure

                    Det.Iadd_anal2 = rps.Tus_itm_model;
                    Det.Iadd_anal3 = rps.Tus_itm_desc;
                    Det.Iadd_anal4 = rps.Tus_ser_id;
                    //Det.Iadd_anal5 =;
                    Det.Iadd_app_itm = rps.Tus_itm_cd;
                    // Det.Iadd_app_pb = txtPriceBook.Text.Trim().ToUpper();
                    // Det.Iadd_app_pb_lvl = txtPBLevel.Text.Trim().ToUpper();
                    if (txtUnitPrice.Text.Trim() != "")
                    {
                        Det.Iadd_app_val = Convert.ToDecimal(txtUnitPrice.Text.Trim());
                    }

                    Det.Iadd_claim_itm = rps.Tus_itm_cd;
                    //Det.Iadd_claim_pb =;
                    //Det.Iadd_claim_pb_lvl = ;
                    //Det.Iadd_claim_val = ;
                    Det.Iadd_coll_itm = rps.Tus_itm_cd;
                    //Det.Iadd_coll_pb = ;
                    //Det.Iadd_coll_pb_lvl = ;
                    //Det.Iadd_coll_ser1 = ;
                    //Det.Iadd_coll_ser2 = ;,
                    //Det.Iadd_coll_ser3 = ;
                    //Det.Iadd_coll_ser3 = ;
                    //Det.Iadd_coll_val = ;
                    Det.Iadd_line = ItmLine++;
                    //Det.Iadd_ref_no =;
                    //Det.Iadd_seq = ;
                    Det.Iadd_stus = 3;
                    #endregion

                    confirmed_detList.Add(Det);
                }

                //try {
                //Update header
                // Searched_AdhodHeader.Iadh_ref_no = txtRefNo.Text.Trim().ToUpper();
                // Searched_AdhodHeader.Iadh_app_by = GlbUserName;
                // Searched_AdhodHeader.Iadh_app_dt = 
                // Searched_AdhodHeader.Iadh_stus = 1;
                Searched_AdhodHeader.Iadh_coll_by = BaseCls.GlbUserID;
                Searched_AdhodHeader.Iadh_coll_dt = CHNLSVC.Security.GetServerDateTime().Date;
                Searched_AdhodHeader.Iadh_stus = 3;

                #region ADJ(-)
                //string AdjNumber = txtAdjustmentNo.Text.Trim();
                //string AdjNumber = "";
                //string manualNum = txtManualRefNo.Text.Trim();
                //string remarks = txtRemarks.Text.Trim();
                //string adj_base = ddlAdjBased.SelectedValue;
                //string adj_sub_type = ddlAdjSubTyepe.SelectedValue;
                //string adj_type = ddlAdjType_.SelectedValue;
                InventoryHeader inHeader = new InventoryHeader();


                inHeader.Ith_acc_no = "";
                inHeader.Ith_anal_1 = "";
                inHeader.Ith_anal_10 = true;
                inHeader.Ith_anal_11 = true;
                inHeader.Ith_anal_12 = false; //update on 07-05-2013 on Chamal's advice
                inHeader.Ith_anal_2 = "";
                inHeader.Ith_anal_3 = "";
                inHeader.Ith_anal_4 = "";
                inHeader.Ith_anal_5 = "";
                inHeader.Ith_anal_6 = 0;
                inHeader.Ith_anal_7 = 0;
                inHeader.Ith_anal_8 = DateTime.MinValue;
                inHeader.Ith_anal_9 = DateTime.MinValue;
                inHeader.Ith_bus_entity = "";
                //inHeader.Ith_cate_tp = ddlAdjSubTyepe.SelectedValue.ToString();
                inHeader.Ith_channel = "";
                inHeader.Ith_com = BaseCls.GlbUserComCode;

                //inHeader.Ith_com ="";
                inHeader.Ith_com_docno = "";
                inHeader.Ith_cre_by = "";
                inHeader.Ith_cre_when = DateTime.MinValue;
                inHeader.Ith_del_add1 = "";
                inHeader.Ith_del_add2 = "";
                inHeader.Ith_del_code = "";

                inHeader.Ith_del_party = "";
                inHeader.Ith_del_town = "";


                inHeader.Ith_direct = true;
                //  inHeader.Ith_direct =true;
                inHeader.Ith_doc_date = CHNLSVC.Security.GetServerDateTime().Date;// DateTime.Today; //update on 07-05-2013 on Chamal's advice
                //  inHeader.Ith_doc_date  =DateTime.MinValue;
                inHeader.Ith_doc_no = "";//"DPS32-ADJ-12-588888";

                inHeader.Ith_doc_tp = "ADJ";
                //   inHeader.Ith_doc_tp ="";
                inHeader.Ith_doc_year = DateTime.Today.Year;
                inHeader.Ith_entry_no = "";
                inHeader.Ith_entry_tp = "";
                inHeader.Ith_git_close = true;
                inHeader.Ith_git_close_date = DateTime.MinValue;
                inHeader.Ith_git_close_doc = "";
                inHeader.Ith_isprinted = true;
                inHeader.Ith_is_manual = true;
                inHeader.Ith_job_no = "";
                inHeader.Ith_loading_point = "";
                inHeader.Ith_loading_user = "";
                inHeader.Ith_loc = BaseCls.GlbUserDefLoca;
                //if (txtManualRefNo.Text == null || txtManualRefNo.Text == "")
                //{
                //    inHeader.Ith_manual_ref = "N/A";
                //}
                //else
                //{
                //    inHeader.Ith_manual_ref = txtManualRefNo.Text.Trim();
                //}
                inHeader.Ith_manual_ref = "N/A";
                // inHeader.Ith_manual_ref ="";
                inHeader.Ith_mod_by = BaseCls.GlbUserID;//"ADMIN";
                inHeader.Ith_mod_when = DateTime.MinValue;
                inHeader.Ith_noofcopies = 1;
                inHeader.Ith_oth_loc = "";

                inHeader.Ith_remarks = "ADHOC CONFIRM";//"ADHOC Confirm";//txtRemarks.Text;
                // inHeader.Ith_remarks ="";
                inHeader.Ith_sbu = "INV";
                //inHeader.Ith_seq_no = 6; removed by Chamal 12-05-2013
                //inHeader.Ith_seq_no =54;
                inHeader.Ith_session_id = BaseCls.GlbUserSessionID;
                inHeader.Ith_stus = "A";
                inHeader.Ith_sub_tp = (CommonUIDefiniton.AdjustmentType.ADHOC).ToString(); //ddlAdjSubTyepe.SelectedValue.ToString();
                // inHeader.Ith_sub_tp ="";
                inHeader.Ith_vehi_no = "";

                inHeader.Ith_direct = false;

                //---------------updated on 07-05-2013 according to Chamal's advice------------------------
                inHeader.Ith_anal_12 = false;
                inHeader.Ith_oth_docno = Searched_AdhodHeader.Iadh_ref_no;
                inHeader.Ith_cre_by = BaseCls.GlbUserID;
                inHeader.Ith_noofcopies = 0;
                inHeader.Ith_isprinted = false;
                inHeader.Ith_git_close = false;
                inHeader.Ith_is_manual = false;
                inHeader.Ith_oth_loc = BaseCls.GlbUserDefLoca;
                inHeader.Ith_oth_com = BaseCls.GlbUserComCode;
                inHeader.Ith_sub_tp = "SYS";
                inHeader.Ith_entry_tp = "SYS";
                inHeader.Ith_acc_no = "FGAP";
                inHeader.Ith_cate_tp = "FGAP";
                inHeader.Ith_remarks = "ADHOC CONFIRM";
                //------------------------------------------


                //*********************************
                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = BaseCls.GlbUserDefLoca;
                masterAuto.Aut_cate_tp = "LOC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "ADJ";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "ADJ";
                masterAuto.Aut_year = null;
                //  masterAuto.Aut_year = Convert.ToDateTime(txtDate_.Text).Year;

                #endregion
                string AdjNumber = string.Empty;
                List<ReptPickSerialsSub> rps_subList = new List<ReptPickSerialsSub>();
                Int16 adjEffect = CHNLSVC.Inventory.ADJMinus(inHeader, Approved_SerialList, rps_subList, masterAuto, out AdjNumber);
                Searched_AdhodHeader.Iadh_adj_no = AdjNumber;

                Int32 effect = 0;
                //effect = CHNLSVC.Inventory.Save_Adhoc_Confirm(Searched_AdhodHeader, confirmed_detList);//_recieptItem
                string GenReceiptNo = "";
                effect = CHNLSVC.Inventory.Save_FGAP_confirmation(Searched_AdhodHeader, confirmed_detList, receiptHeaderList, save_receipItemList, Transaction_List, _receiptAuto, _transactionAuto, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, out GenReceiptNo);

                if (effect < 0)
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Failed to Confirmed. Error occured. Try Again!");
                    MessageBox.Show("Failed to Confirmed. Error occured. Try Again!");
                    return;
                }
                else if (effect > 0)
                {
                    clearCompleateScreen();
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Confirmed Successfully! ADJ(-) NO. =" + AdjNumber + ", Receipt No.=" + GenReceiptNo);
                    //if (checkManualReceipt.Checked == true)
                    //{
                    //    MessageBox.Show("Confirmed Successfully! \n ADJ(-) NO. =" + AdjNumber );
                    //    if (checkManualReceipt.Checked == true)
                    //    {
                    //      Int32 EFF=  CHNLSVC.Inventory.UpdateManualDocNo(BaseCls.GlbUserDefLoca, "MDOC_AVREC", Convert.ToInt32(txtManualReceiptNo.Text));
                    //    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Confirmed Successfully! \n ADJ(-) NO. =" + AdjNumber + ", Receipt No.=" + GenReceiptNo);
                    //    try
                    //    {
                    //        ReportViewer _view = new ReportViewer();                        
                    //        _view.GlbReportName = "ReceiptPrints.rpt";
                    //        _view.GlbReportDoc = GenReceiptNo;
                    //        _view.GlbReportProfit = BaseCls.GlbUserDefProf;
                    //        _view.Show();
                    //        _view = null;
                    //    }
                    //    catch(Exception ex){

                    //    }

                    //}             
                    MessageBox.Show("Confirmed Successfully! \n ADJ(-) NO. =" + AdjNumber + ", Receipt No.=" + GenReceiptNo);
                    try
                    {
                        ReportViewer _view = new ReportViewer();

                        BaseCls.GlbReportName = string.Empty; //add on 14-June-2013
                        _view.GlbReportName = string.Empty;////add on 14-June-2013

                        BaseCls.GlbReportTp = "REC";
                        _view.GlbReportName = "ReceiptPrints.rpt";
                        _view.GlbReportDoc = GenReceiptNo;
                        _view.GlbReportProfit = BaseCls.GlbUserDefProf;
                        _view.Show();
                        _view = null;
                    }
                    catch (Exception ex)
                    {

                    }
                    //***********************************************************
                    try
                    {
                        Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                        BaseCls.GlbReportTp = "OUTWARD";
                        _view.GlbReportName = BaseCls.GlbUserComCode == "SGL" ? "Outward_Docs.rpt" : BaseCls.GlbDefChannel == "AUTO_DEL" ? "Dealer_Outward_Docs.rpt" : "Outward_Docs.rpt";
                        _view.GlbReportDoc = AdjNumber;//"AAZPG-DO-12-00123";
                        _view.Show();
                        _view = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error in printing out document.");
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
            //------------------------------------------------------------------------------------------------------
        }
        private void fill_Transactions(RecieptHeader r_hdr)
        {
            try
            {
                //(to write to hpt_txn)
                HpTransaction tr = new HpTransaction();
                // tr.Hpt_acc_no = lblAccNo.Text.Trim();
                tr.Hpt_ars = 0;
                tr.Hpt_bal = 0;
                tr.Hpt_crdt = r_hdr.Sar_tot_settle_amt;//amount
                tr.Hpt_cre_by = BaseCls.GlbUserID;
                tr.Hpt_cre_dt = CHNLSVC.Security.GetServerDateTime().Date;
                tr.Hpt_dbt = 0;
                tr.Hpt_com = BaseCls.GlbUserComCode;
                tr.Hpt_pc = BaseCls.GlbUserDefProf;
                if (r_hdr.Sar_is_oth_shop == true)
                {
                    tr.Hpt_desc = ("Other shop collection").ToUpper() + "-" + BaseCls.GlbUserDefProf; ;
                    tr.Hpt_mnl_ref = r_hdr.Sar_prefix + "-" + r_hdr.Sar_manual_ref_no;//+"-"+GlbUserDefProf;   //"prefix-receiptNo-pc"

                }
                else
                {
                    tr.Hpt_desc = ("Payment receive").ToUpper();

                }
                if (r_hdr.Sar_is_mgr_iss)
                {
                    //"prefix-receiptNo-issues"
                    //tr.Hpt_mnl_ref = r_hdr.Sar_prefix + "-" + r_hdr.Sar_manual_ref_no + (" issues").ToUpper();

                }
                else
                { //"prefix-receiptNo"
                    //tr.Hpt_mnl_ref = r_hdr.Sar_prefix + "-" + r_hdr.Sar_manual_ref_no;
                }
                tr.Hpt_pc = BaseCls.GlbUserDefProf;

                tr.Hpt_ref_no = "";
                tr.Hpt_txn_dt = CHNLSVC.Security.GetServerDateTime().Date;
                tr.Hpt_txn_ref = "";
                tr.Hpt_txn_tp = r_hdr.Sar_receipt_type;
                tr.Hpt_ref_no = r_hdr.Sar_seq_no.ToString();

                if (Transaction_List == null)
                {
                    Transaction_List = new List<HpTransaction>();
                }
                Transaction_List.Add(tr);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void btn_SendReq_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlReuestType.Text == "FIXED ASSETS")
                {
                    ////if (BaseCls.GlbUserComCode == "ABL")
                    ////{
                    //    //Tharindu 2018-06-06
                    //    //DataTable DT = CHNLSVC.MsgPortal.CheckValidItmInFixAsset(txtItemCD.Text.Trim().ToUpper());
                    //   DataTable DT = CHNLSVC.MsgPortal.CheckValidItmInFixAsset(txtItemCD.Text.Trim().ToUpper());
                    //    if (DT == null || DT.Rows.Count == 0)
                    //    {
                    //        MessageBox.Show("This Item is not Available in fixed Asset system, Please Contact Account Department", "Fixed Asset", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        txtModel.Text = "";
                    //        txtItmDescription.Text = "";
                    //        txtItemCD.Text = "";
                    //        return;
                    //    }
                    ////}
                    string fixloc = LoadFixedAssetLocation().ToString();
                    if (string.IsNullOrEmpty(fixloc))
                    {
                        MessageBox.Show("FixAsset Loc Not Avilable in this location", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        txtSendLoc.Text = fixloc;
                    }

                    string chkfixloc = CheckFixAssetlocAvailability(fixloc).ToString();
                    if (string.IsNullOrEmpty(chkfixloc))
                    {
                        MessageBox.Show("FixAsset Loc Not Avilable in Fix asset db", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                ComboboxItem combo_reqTp = (ComboboxItem)ddlReuestType.SelectedItem;
                if (txtRefNo.Text.Trim() == "" && combo_reqTp.Value.ToString() == "1")
                {
                    txtRefNo.Focus();
                    // MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Please enter a Reference No!");
                    MessageBox.Show("Please enter a Reference No!");
                    return;
                }
                //Fill Request header
                InventoryAdhocHeader reqHdr = new InventoryAdhocHeader();
                if (combo_reqTp.Value.ToString() == "2")
                {
                    reqHdr.Iadh_ref_no = txtRefNo.Text.Trim().ToUpper();
                    //reqHdr.Iadh_adj_no=
                    //reqHdr.Iadh_anal1=
                    //reqHdr.Iadh_anal2=
                    //reqHdr.Iadh_anal3=
                    //reqHdr.Iadh_anal4=
                    //reqHdr.Iadh_anal5=
                    //reqHdr.Iadh_app_by=
                    //reqHdr.Iadh_app_dt=
                    //reqHdr.Iadh_coll_by=
                    //reqHdr.Iadh_coll_dt=
                    reqHdr.Iadh_com = BaseCls.GlbUserComCode;
                    reqHdr.Iadh_dt = CHNLSVC.Security.GetServerDateTime().Date;
                    reqHdr.Iadh_loc = BaseCls.GlbUserDefLoca;
                    reqHdr.Iadh_pc = BaseCls.GlbUserDefProf;
                    //reqHdr.Iadh_ref_no=
                    reqHdr.Iadh_req_by = BaseCls.GlbUserID;
                    reqHdr.Iadh_req_dt = CHNLSVC.Security.GetServerDateTime().Date;
                    //reqHdr.Iadh_seq
                    reqHdr.Iadh_stus = 0;
                    // reqHdr.Iadh_tp = Convert.ToInt32(ddlReuestType.SelectedValue);
                    reqHdr.Iadh_tp = Convert.ToInt32(combo_reqTp.Value.ToString());

                    //add sachith add remark
                    reqHdr.Iadh_anal1 = txtRemarks.Text;

                }
                else if (combo_reqTp.Value.ToString() == "1")
                {
                    reqHdr.Iadh_ref_no = txtRefNo.Text.Trim().ToUpper();
                    //reqHdr.Iadh_adj_no=
                    //reqHdr.Iadh_anal1=
                    //reqHdr.Iadh_anal2=
                    //reqHdr.Iadh_anal3=
                    //reqHdr.Iadh_anal4=
                    //reqHdr.Iadh_anal5=
                    reqHdr.Iadh_app_by = BaseCls.GlbUserID;
                    reqHdr.Iadh_app_dt = CHNLSVC.Security.GetServerDateTime().Date;
                    //reqHdr.Iadh_coll_by=
                    //reqHdr.Iadh_coll_dt=
                    reqHdr.Iadh_com = BaseCls.GlbUserComCode;
                    reqHdr.Iadh_dt = CHNLSVC.Security.GetServerDateTime().Date;
                    reqHdr.Iadh_loc = txtFgapLoc.Text.Trim().ToUpper();
                    reqHdr.Iadh_pc = txtPC.Text.Trim().ToUpper();
                    //reqHdr.Iadh_ref_no=
                    //reqHdr.Iadh_req_by = GlbUserName;
                    //reqHdr.Iadh_req_dt =
                    //reqHdr.Iadh_seq
                    reqHdr.Iadh_stus = 1;

                    //reqHdr.Iadh_tp = Convert.ToInt32(ddlReuestType.SelectedValue);
                    reqHdr.Iadh_tp = Convert.ToInt32(combo_reqTp.Value.ToString());
                    reqHdr.Iadh_anal1 = txtRemarks.Text;
                }

                Int32 effect = 0;
                if (AdhodDetList.Count > 0)
                {
                    if (Approved_SerialList == null)
                    {
                        Approved_SerialList = new List<ReptPickSerials>();
                    }

                    string RefNumber = "";
                    effect = CHNLSVC.Inventory.Save_Adhoc_Request(reqHdr, AdhodDetList, Approved_SerialList, out RefNumber);

                    if (effect > 0)
                    {

                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Sent Successfully! Reference# :" + RefNumber);
                        MessageBox.Show("Sent Successfully! Reference# :" + RefNumber);
                        clearCompleateScreen();
                        try
                        {
                            if (reqHdr.Iadh_tp == 2)
                            {
                                Reports.Inventory.ReportViewerInventory _view = new Reports.Inventory.ReportViewerInventory();
                                _view.GlbReportName = BaseCls.GlbUserComCode == "SGL"?"SFixedAssetTransferNotes.rpt":"FixedAssetTransferNotes.rpt";
                                _view.GlbReportDoc = RefNumber;//"FIX-000003";";( Ref #)
                                _view.Show();
                                _view = null;
                            }
                            else if (reqHdr.Iadh_tp == 1)
                            {

                            }

                        }
                        catch (Exception EX)
                        {
                            return;
                        }
                    }
                    else
                    {
                        //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "Sending Failed!");
                        MessageBox.Show("Sending Failed!");
                        return;
                    }
                }
                else
                {
                    //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, "No Items are added. Please add Items first!");
                    MessageBox.Show("No Items are added. Please add Items first!");
                    return;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void loadInitials()
        { 
            
        }
        private void btn_CLEAR_Click(object sender, EventArgs e)
        {
            FixAssetOrAdhocRequestAndApprove formnew = new FixAssetOrAdhocRequestAndApprove();
           
            //formnew.Location = this.Location;
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();          
            this.Close();
            //clearCompleateScreen();          

        }

        private void imgBtnPriceBook_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBook);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceBookData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPriceBook;
                _CommonSearch.ShowDialog();
                txtPriceBook.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void imgBtnLevelSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevel);
                DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPBLevel;
                _CommonSearch.ShowDialog();
                txtPBLevel.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void imgbtnSearchLocation_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtFgapLoc;
                _CommonSearch.ShowDialog();
                txtFgapLoc.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void imgbtnSearchProfitCenter_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPC; //txtBox;
                _CommonSearch.ShowDialog();
                txtPC.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void imgBtnItemStatus_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox txtBox = new TextBox();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBox;
                _CommonSearch.ShowDialog();

                foreach (ComboboxItem Itm in ddlStatus.Items)
                {
                    if (Itm.Value.ToString() == txtBox.Text)
                        ddlStatus.SelectedItem = Itm;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
           
        }

        private void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboboxItem combo_stat = (ComboboxItem)ddlStatus.SelectedItem;
                string status = combo_stat.Value.ToString();
                List<ReptPickSerials> serList = new List<ReptPickSerials>();
                if (status == "Any")
                {
                    status = string.Empty;
                }
               // serList = CHNLSVC.Inventory.GET_ser_FOR_STATUS(BaseCls.GlbUserComCode, txtSendLoc.Text.Trim().ToUpper(), SelectedItemCD, status);
                serList = CHNLSVC.Inventory.GET_ser_FOR_STATUS(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, SelectedItemCD, status);

                grvAvailableSerials.DataSource = null;
                grvAvailableSerials.AutoGenerateColumns = false;
                //grvAvailableSerials.DataSource = serList;
                //grvAvailableSerials.DataBind();         
                BindingSource _source = new BindingSource();
                _source.DataSource = serList;
                grvAvailableSerials.DataSource = _source;


                AvailableSerialList = serList;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void grvAvailableSerials_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex != -1)
                {
                    grvAvailableSerials.Rows[e.RowIndex].Selected = true;

                    DataGridViewRow gvr = grvAvailableSerials.SelectedRows[0];
                    //SelectedItemCD = row.Cells[1].Value.ToString();
                    string lblSerID = gvr.Cells["lblSerID_av"].Value.ToString();
                    Int32 serID = Convert.ToInt32(lblSerID);
                    string ItemCD = gvr.Cells["Tus_itm_cd"].Value.ToString();

                    //-------------------------------------------------------
                    ComboboxItem reqTp = (ComboboxItem)ddlReuestType.SelectedItem;
                    ComboboxItem Act = (ComboboxItem)ddlAction.SelectedItem;

                    //CHNLSVC.Inventory.CheckPreRequestAdhocSer(BaseCls.GlbUserComCode,BaseCls.GlbUserDefLoca,

                    if (reqTp.Value.ToString() == "2" && Act.Value.ToString() == "Request")
                    {
                        Boolean hasRequest = CHNLSVC.Inventory.CheckPreRequestAdhocSer(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, serID);
                        if (hasRequest == true)
                        {
                            MessageBox.Show("This serial has been requested already!");
                            return;
                        }
                    }
                    //CheckPreRequestAdhocSer
                    //-------------------------------------------------------

                    List<ReptPickSerials> serList = new List<ReptPickSerials>();

                    var _dup = from _l in AvailableSerialList
                               where _l.Tus_itm_cd == ItemCD && _l.Tus_ser_id == serID
                               select _l;


                    // serList= _dup.ToList<ReptPickSerials>();
                
                                    
                    Approved_SerialList.AddRange(_dup);

                    AvailableSerialList.RemoveAll(x => x.Tus_itm_cd == ItemCD && x.Tus_ser_id == serID);//&& x.Iadd_anal2 == DelModle

                    grvApproveItms.DataSource = null;
                    grvApproveItms.AutoGenerateColumns = false;
                    //grvApproveItms.DataSource = Approved_SerialList;
                    BindingSource _source1 = new BindingSource();
                    _source1.DataSource = Approved_SerialList;
                    grvApproveItms.DataSource = _source1;
                    //grvApproveItms.DataBind();

                    grvAvailableSerials.DataSource = null;
                    grvAvailableSerials.AutoGenerateColumns = false;
                    //  grvAvailableSerials.DataSource = AvailableSerialList;
                    BindingSource _source = new BindingSource();
                    _source.DataSource = AvailableSerialList;
                    grvAvailableSerials.DataSource = _source;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtRefNo_KeyPress(object sender, KeyPressEventArgs e)
        {
           if (e.KeyChar != (char)13)
            {
                return;
            }
            this.btnRefOk_Click(sender, e);
        }

        private void txtRefNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.ImgSearchRefNo_Click(sender, e);
            }
        }

        private void txtItemCD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.imgBtnSearchItmCD_Click(sender, e);
            }
        }

        private void txtPriceBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.imgBtnPriceBook_Click(sender, e);
            }
        }

        private void txtPBLevel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.imgBtnLevelSearch_Click(sender, e);
            }
        }

        private void txtFgapLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.imgbtnSearchLocation_Click(sender, e);
            }
        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.imgbtnSearchProfitCenter_Click(sender, e);
            }
        }

        private void txtItemCD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.txtItemCD_Leave(sender, e);
                 ComboboxItem combo_reqTp = (ComboboxItem)ddlReuestType.SelectedItem;
                 if (combo_reqTp.Value.ToString() == "1")
                 {
                     txtPriceBook.Focus();
                 }
                 else
                 {
                     txtQty.Focus();
                 }
            }
        }

        private void txtPriceBook_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                LoadPriceDefaultValue();
                txtPBLevel.Focus();
            }
        }

        private void btnSenLocSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSendLoc;
                _CommonSearch.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtRefNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ImgSearchRefNo_Click(sender, e);
        }

        private void txtItemCD_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtItemCD_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.imgBtnSearchItmCD_Click(sender, e);
        }

        private void txtPriceBook_DoubleClick(object sender, EventArgs e)
        {
            this.imgBtnPriceBook_Click(sender, e);
        }

        private void txtPBLevel_DoubleClick(object sender, EventArgs e)
        {
            this.imgBtnLevelSearch_Click(sender, e);
        }

        private void txtFgapLoc_DoubleClick(object sender, EventArgs e)
        {
            this.imgbtnSearchLocation_Click(sender, e);
        }

        private void txtPC_DoubleClick(object sender, EventArgs e)
        {
            this.imgbtnSearchProfitCenter_Click(sender, e);
        }

        private void txtRefNo_DoubleClick(object sender, EventArgs e)
        {
            this.ImgSearchRefNo_Click(sender, e);
        }


        private void grvAvailableSerials_DataSourceChanged(object sender, EventArgs e)
        {
            if (Searched_AdhodHeader==null)
            {
                return;
            }
            try {
                if (Searched_AdhodHeader.Iadh_tp == 2 && Searched_AdhodHeader.Iadh_stus == 1)
                {
                    foreach (DataGridViewRow gvr in this.grvAvailableSerials.Rows)
                    {
                        string itemCode = gvr.Cells["Tus_itm_cd"].Value.ToString();
                        string serID = gvr.Cells["lblSerID_av"].Value.ToString();
                        if (det_list_selected != null)
                        {
                            var _exist = from _dup in det_list_selected
                                         where _dup.Iadd_claim_itm == itemCode //&& _dup.Sccd_brd == obj.Sccd_brd 
                                         select _dup;

                            if (_exist.Count() != 0)
                            {
                                foreach (InventoryAdhocDetail det in _exist)
                                {
                                    if (det.Iadd_anal4 == Convert.ToInt32(serID))
                                    {
                                       // gvr.DefaultCellStyle.ForeColor = Color.LightSalmon;
                                        //************************
                                        var _dup = from _l in AvailableSerialList
                                                   where _l.Tus_itm_cd == itemCode && _l.Tus_ser_id == Convert.ToInt32(serID)
                                                   select _l;


                                        // serList= _dup.ToList<ReptPickSerials>();
                                        Approved_SerialList.AddRange(_dup);

                                        AvailableSerialList.RemoveAll(x => x.Tus_itm_cd == itemCode && x.Tus_ser_id == Convert.ToInt32(serID));//&& x.Iadd_anal2 == DelModle

                                        grvApproveItms.DataSource = null;
                                        grvApproveItms.AutoGenerateColumns = false;
                                        //grvApproveItms.DataSource = Approved_SerialList;
                                        BindingSource _source = new BindingSource();
                                        _source.DataSource = Approved_SerialList;
                                        grvApproveItms.DataSource = _source;
                                        //grvApproveItms.DataBind();

                                        grvAvailableSerials.DataSource = null;
                                        grvAvailableSerials.AutoGenerateColumns = false;
                                        //grvAvailableSerials.DataSource = AvailableSerialList; 
                                        BindingSource _source2 = new BindingSource();
                                        _source2.DataSource = AvailableSerialList;
                                        grvAvailableSerials.DataSource = _source2;
                                        //************************
                                       
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
            
        }

        private void btnCancelReq_Click(object sender, EventArgs e)
        {
            try
            {
                this.btnRefOk_Click(sender, e);
                if (Searched_AdhodHeader == null)
                {
                    return;
                }
                else if (Searched_AdhodHeader.Iadh_ref_no != "")
                {

                    if ((Searched_AdhodHeader.Iadh_tp == 2 && Searched_AdhodHeader.Iadh_stus == 0) || Searched_AdhodHeader.Iadh_tp == 1 && Searched_AdhodHeader.Iadh_stus == 1)
                    {
                        if (MessageBox.Show("Are you sure you want to Cancel?", "Confirm Cancel", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }

                        Searched_AdhodHeader.Iadh_stus = 4;
                        Searched_AdhodHeader.Iadh_app_by = BaseCls.GlbUserID; //rejected person
                        Searched_AdhodHeader.Iadh_app_dt = CHNLSVC.Security.GetServerDateTime().Date;//rejected date
                        Int32 effect = 0;
                        effect = CHNLSVC.Inventory.Save_Adhoc_Confirm(Searched_AdhodHeader, null);
                        if (effect > 0)
                        {
                            clearCompleateScreen();
                            //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully Rejected!");
                            MessageBox.Show("Successfully Cancelled!");
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Not Cancelled!");
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void ddlAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // clearCompleateScreen();     
                ComboboxItem actION = (ComboboxItem)ddlAction.SelectedItem;
                Button10.Visible = false;
                if (actION.Value.ToString() == "Approve")
                {
                    btnCancelReq.Enabled = false;
                    if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, string.Empty, "INV5") == false)
                    {
                        // MessageBox.Show("No Permission Granted! This is Head office task. \n(Rquired permission type: 'INV5')");
                        btn_SendReq.Enabled = false;
                        return;
                    }
                    else
                    {
                        btn_SendReq.Enabled = true;
                    }
                    //----------------
                    //pnlPayss.Visible = false;
                    //panel_payment.Visible = false;
                    //ucPayModes1.Visible = false;               

                    //----------------------
                }
                else
                {
                    btnCancelReq.Enabled = false;
                }
                try
                {
                    ComboboxItem REQ = (ComboboxItem)ddlReuestType.SelectedItem;
                    if (REQ.Value.ToString() == "1")
                    {
                        ComboboxItem Act = (ComboboxItem)ddlAction.SelectedItem;
                        if (Act.Value.ToString() == "Request")
                        {
                            MessageBox.Show("Action not valid for FGAP");
                        }
                        if (Act.Value.ToString() == "Confirmation")
                        {
                            btn_SendReq.Enabled = false;
                            txtFgapLoc.Text = BaseCls.GlbUserDefLoca;
                            txtPC.Text = BaseCls.GlbUserDefProf;
                        }
                        else
                        {
                            btn_SendReq.Enabled = true;
                        }
                    }

                    if (REQ.Value.ToString() == "2")
                    {
                        ComboboxItem act = (ComboboxItem)ddlAction.SelectedItem;
                        if (act.Value.ToString() == "Approve" || act.Value.ToString() == "Confirmation")
                        {
                            btn_SendReq.Enabled = false;
                        }
                        else
                        {
                            btn_SendReq.Enabled = true;
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
                }
                finally
                {
                    CHNLSVC.CloseAllChannels();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
            
        }

        private void txtPriceBook_TextChanged(object sender, EventArgs e)
        {
           // txtPBLevel.Text = "";
        }

        private void txtPriceBook_Leave_1(object sender, EventArgs e)
        {

            try {
                LoadPriceDefaultValue();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
            
        }

        private void txtPC_Leave(object sender, EventArgs e)
        {
            try
            {
                DataTable DT = CHNLSVC.General.GetPartyCodes(BaseCls.GlbUserComCode, txtPC.Text.Trim().ToUpper());
                if (DT == null)
                {
                    txtPC.Text = "";
                    MessageBox.Show("Invalid Profit center.");
                    txtPC.Focus();
                    return;
                }
                if (DT.Rows.Count == 0)
                {
                    txtPC.Text = "";
                    MessageBox.Show("Invalid Profit center.");
                    txtPC.Focus();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtFgapLoc_Leave(object sender, EventArgs e)
        {
            try
            {
                MasterLocation LOC = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, txtFgapLoc.Text.Trim().ToUpper());
                if (LOC == null)
                {
                    txtFgapLoc.Text = "";
                    MessageBox.Show("Invalid Location code");

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //Tharindu 2018-06-06
        private void txtSendLoc_Leave(object sender, EventArgs e)
        {
            try
            {
                DataTable DT = CHNLSVC.MsgPortal.GetFixAssetLocation_NEW(BaseCls.GlbUserComCode, txtSendLoc.Text);

                if (DT == null)
                {
                    txtPC.Text = "";
                    MessageBox.Show("Fixed Asset Location Not Avialable in This Location Please Contact Inventroy Department");
                    txtPC.Focus();
                    return;
                }
                if (DT.Rows.Count == 0)
                {
                    txtPC.Text = "";
                    MessageBox.Show("Fixed Asset Location Not Avialable in This Location Please Contact Inventroy Department");
                    txtPC.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        //private void checkManualReceipt_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (checkManualReceipt.Checked == true)
        //    {
        //        txtManualReceiptNo.Enabled = false;
        //        Int32 _NextNo = CHNLSVC.Inventory.GetNextManualDocNo(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "MDOC_AVREC");
        //        if (_NextNo != 0)
        //        {
        //            txtManualReceiptNo.Text = _NextNo.ToString();
        //        }
        //        else
        //        {
        //            txtManualReceiptNo.Text = "";
        //        }
        //    }

        //    else
        //    {
        //        txtManualReceiptNo.Text = "";

        //    }
        //}
    }
}
