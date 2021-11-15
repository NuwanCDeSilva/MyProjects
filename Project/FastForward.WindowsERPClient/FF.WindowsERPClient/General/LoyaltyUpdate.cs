using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;
using System.Threading;
using System.Collections;
using FF.WindowsERPClient.General;


//Written By kapila on 31/3/2014
namespace FF.WindowsERPClient.General
{
    public partial class LoyaltyUpdate : Base
    {
        private string _cardSer = "";

        public LoyaltyUpdate()
        {
            InitializeComponent();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            lblAdd1.Text = "";

            lblCus.Text = "";
            lblCusName.Text = "";
            lblMobile.Text = "";

            txtCard_no.Text = "";

        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCardNo:
                    {

                        DateTime _date = CHNLSVC.Security.GetServerDateTime();

                        paramsText.Append("MYAB" + seperator + _date.Date.ToString("d") + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard:
                    {

                        DateTime _date = CHNLSVC.Security.GetServerDateTime();

                        paramsText.Append(lblCus.Text + seperator + _date.Date.ToString("d") + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SerialNIC:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PartyType:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GiftVoucher:
                    {
                        paramsText.Append(null + seperator + 0 + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void LoadGiftVoucher(string p)
        {
            Int32 val;

            if (!int.TryParse(p, out val))
                return;

            List<GiftVoucherPages> _gift = CHNLSVC.Inventory.GetGiftVoucherPages(BaseCls.GlbUserComCode, Convert.ToInt32(p));
            if (_gift != null)
            {
                if (_gift.Count == 1)
                {
                    lblAdd1.Text = _gift[0].Gvp_cus_add1;

                    lblCus.Text = _gift[0].Gvp_cus_cd;
                    lblCusName.Text = _gift[0].Gvp_cus_name;
                    lblMobile.Text = _gift[0].Gvp_cus_mob;


                }
                else
                {

                }
            }
        }

        private void btnGiftVoucher_Click(object sender, EventArgs e)
        {

        }

        private void gvMultipleItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnUpd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCard_no.Text))
            {
                MessageBox.Show("Please enter loyalty card number", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Are You Sure?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                // Int32 X = CHNLSVC.Sales.UpdateVouSettlement(BaseCls.GlbUserComCode,BaseCls.GlbUserDefProf,lblBook.Text,txtGiftVoucher.Text,lblPrefix.Text,lblCd.Text,BaseCls.GlbUserID,txtRef.Text,0,Convert.ToDateTime(txtDate.Text));
                //  btnClose_Click(null, null);
                MessageBox.Show("Successfully Updated", "Gift voucher", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSrchNIC_Click(object sender, EventArgs e)
        {
            //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            //_CommonSearch.ReturnIndex = 0;
            //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
            //DataTable _resultSup = CHNLSVC.CommonSearch.GetCustomerCommon(_CommonSearch.SearchParams, null, null);
            //_CommonSearch.dvResult.DataSource = _resultSup;
            //_CommonSearch.BindUCtrlDDLData(_resultSup);
            //_CommonSearch.obj_TragetTextBox = txtCusCode;
            //_CommonSearch.ShowDialog();

            //txtNIC_Leave(null, null);
            //load_loyalty();

            //txtCard_no.Select();
        }
        private void ClearScreen()
        {

            lblAdd1.Text = "";
            lblCus.Text = "";
            lblCusName.Text = "";
            lblMobile.Text = "";
            txtSer.Text = "";
        }

        private void load_loyalty()
        {
            _cardSer = "";
            List<LoyaltyMemeber> _lstLoy = new List<LoyaltyMemeber>();
            _lstLoy = CHNLSVC.Sales.GetCurrentLoyalByCus(lblCus.Text, "MYAB");
            if (_lstLoy.Count > 0)
            {
                _cardSer = _lstLoy[0].Salcm_no;

                if (_lstLoy[0].Salcm_act == 1)
                    lblStus.Text = "ACTIVE";
                else
                    lblStus.Text = "PENDING";

                if (_lstLoy[0].Salcm_cd_ser != _lstLoy[0].Salcm_no)
                {
                    btnAssign.Enabled = false;
                    txtCard_no.Text = _lstLoy[0].Salcm_cd_ser;
                }
                else
                {
                    btnAssign.Enabled = true;
                }
            }
            else
                MessageBox.Show("Invalid MyAbans loyalty member !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        public void LoadCustProf(MasterBusinessEntity cust)
        {
            lblCus.Text = cust.Mbe_nic;

            lblCus.Text = cust.Mbe_cd;
            lblCusName.Text = cust.MBE_FNAME + " " + cust.MBE_SNAME;
            lblAdd1.Text = cust.Mbe_add1 + " " + cust.Mbe_add2;
            lblMobile.Text = cust.Mbe_mob;

        }

        public MasterBusinessEntity GetbyNIC(string nic)
        {
            return CHNLSVC.Sales.GetCustomerProfileByNIC(nic);
        }

        private void txtNIC_Leave(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtCusCode.Text))
            //{
            //    lblCus.Text = "";
            //    lblCusName.Text = "";
            //    lblAdd1.Text = "";
            //    lblMobile.Text = "";

            //    MasterBusinessEntity custProf = CHNLSVC.Sales.GetCustomerProfileByCom(txtCusCode.Text.Trim().ToUpper(), null, null, null, null, BaseCls.GlbUserComCode);
            //    if (custProf.Mbe_cd != null)
            //    {
            //        lblCus.Text = custProf.Mbe_cd;
            //        lblCusName.Text = custProf.Mbe_name;
            //        lblAdd1.Text = custProf.Mbe_add1 + ' ' + custProf.Mbe_add2;
            //        lblMobile.Text = custProf.Mbe_mob;

            //        load_loyalty();
            //    }
            //}
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lblCus.Text))
            {
                MessageBox.Show("Please select the card !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (MessageBox.Show("Are you sure ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            int _eff = CHNLSVC.Financial.UpdateCardMemberSerial(txtCard_no.Text, lblCus.Text, _cardSer, 0);

            OutSMS smsout = new OutSMS();
            bool isValid = ValidateMobileNo(lblMobile.Text);
            if (isValid == true)
            {
                MasterProfitCenter _mstPc = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                string _mobNumber = lblMobile.Text;
                string _msg = "Dear Customer, your MyAbans card is ready to collect. Card number " + txtCard_no.Text + " Location " + _mstPc.Mpc_desc;

                if (_mobNumber.Length == 10)
                {
                    smsout.Receiverphno = "+94" + _mobNumber.Substring(1, 9);
                    smsout.Senderphno = "+94" + _mobNumber.Substring(1, 9);
                }
                if (_mobNumber.Length == 9)
                {
                    smsout.Receiverphno = "+94" + _mobNumber;
                    smsout.Senderphno = "+94" + _mobNumber;
                }

                smsout.Msg = _msg;
                smsout.Receiver = BaseCls.GlbUserDefProf;
                smsout.Sender = BaseCls.GlbUserID;
                smsout.Seqno = 0;
                smsout.Msgstatus = 0;
                smsout.Msgtype = "GEN_E";
                smsout.Createtime = DateTime.Now;

                Int32 errroCode = CHNLSVC.General.SaveSMSOut(smsout);
            }


            if (_eff > 0)
            {
                MessageBox.Show("Successfully updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearScreen();
            }
            else
                MessageBox.Show("Not Updated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private bool ValidateMobileNo(string num)
        {
            int intNum = 0;
            //check only contain degits
            if (!int.TryParse(num, out intNum))
                return false;
            //check for length
            else
            {
                if (num.Length < 10)
                {
                    return false;
                }
                //check for first three chars
                else
                {
                    string firstChar = num.Substring(0, 3);
                    if (firstChar != "071" && firstChar != "077" && firstChar != "078" && firstChar != "072" && firstChar != "075" && firstChar != "076" && firstChar != "074")
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void btnresend_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            OutSMS smsout = new OutSMS();
            bool isValid = ValidateMobileNo(lblMobile.Text);
            if (isValid == true)
            {
                MasterProfitCenter _mstPc = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                string _mobNumber = lblMobile.Text;
                string _msg = "Dear Customer, your MyAbans card is ready to collect. Card number " + txtCard_no.Text + " Activation code " + _cardSer + " Location " + _mstPc.Mpc_desc;

                if (_mobNumber.Length == 10)
                {
                    smsout.Receiverphno = "+94" + _mobNumber.Substring(1, 9);
                    smsout.Senderphno = "+94" + _mobNumber.Substring(1, 9);
                }
                if (_mobNumber.Length == 9)
                {
                    smsout.Receiverphno = "+94" + _mobNumber;
                    smsout.Senderphno = "+94" + _mobNumber;
                }

                smsout.Msg = _msg;
                smsout.Receiver = BaseCls.GlbUserDefProf;
                smsout.Sender = BaseCls.GlbUserID;
                smsout.Seqno = 0;
                smsout.Msgstatus = 0;
                smsout.Msgtype = "GEN_E";
                smsout.Createtime = DateTime.Now;

                Int32 errroCode = CHNLSVC.General.SaveSMSOut(smsout);
            }
            else
                MessageBox.Show("Invalid Mobile Number !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnIssu_Click(object sender, EventArgs e)
        {
            if (lblStus.Text == "ACTIVE")
            {
                MessageBox.Show("Already activated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //if (string.IsNullOrEmpty(txtSer.Text))
            //{
            //    MessageBox.Show("Please enter activation code", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    txtSer.Focus();
            //    return;
            //}
            //if(_cardSer!=txtSer.Text)
            //{
            //    MessageBox.Show("Invalid activation code !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    txtSer.Text = "";
            //    txtSer.Focus();
            //    return;
            //}
            if (MessageBox.Show("Are you sure ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            int _eff = CHNLSVC.Financial.UpdateCardMemberSerial(txtCard_no.Text, lblCus.Text, _cardSer, 1);

            if (_eff > 0)
            {
                OutSMS smsout = new OutSMS();
                bool isValid = ValidateMobileNo(lblMobile.Text);
                if (isValid == true)
                {
                    MasterProfitCenter _mstPc = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                    string _mobNumber = lblMobile.Text;
                    string _msg = "Dear Customer, your MyAbans card is activated. Thank you";

                    if (_mobNumber.Length == 10)
                    {
                        smsout.Receiverphno = "+94" + _mobNumber.Substring(1, 9);
                        smsout.Senderphno = "+94" + _mobNumber.Substring(1, 9);
                    }
                    if (_mobNumber.Length == 9)
                    {
                        smsout.Receiverphno = "+94" + _mobNumber;
                        smsout.Senderphno = "+94" + _mobNumber;
                    }

                    smsout.Msg = _msg;
                    smsout.Receiver = BaseCls.GlbUserDefProf;
                    smsout.Sender = BaseCls.GlbUserID;
                    smsout.Seqno = 0;
                    smsout.Msgstatus = 0;
                    smsout.Msgtype = "GEN_E";
                    smsout.Createtime = DateTime.Now;

                    Int32 errroCode = CHNLSVC.General.SaveSMSOut(smsout);
                }
            }

            MessageBox.Show("Successfully Activated !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearScreen();

        }

        private void btnCardNo_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCardNo);
            DataTable _result = CHNLSVC.CommonSearch.SearchLoyaltyCardNo(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCard_no;
            _CommonSearch.ShowDialog();
            txtCard_no.Select();
            load_cus_details();
        }

        private void load_cus_details()
        {
            if (!string.IsNullOrEmpty(txtCard_no.Text))
            {
                lblCus.Text = "";
                lblCusName.Text = "";
                lblAdd1.Text = "";
                lblMobile.Text = "";

                DataTable _dt = CHNLSVC.Sales.GetLoyaltyMemberByCardNo(txtCard_no.Text);
                if (_dt.Rows.Count > 0)
                {
                    MasterBusinessEntity custProf = CHNLSVC.Sales.GetCustomerProfileByCom(_dt.Rows[0]["salcm_cus_cd"].ToString(), null, null, null, null, BaseCls.GlbUserComCode);
                    if (custProf.Mbe_cd != null)
                    {
                        lblCus.Text = custProf.Mbe_cd;
                        lblCusName.Text = custProf.Mbe_name;
                        lblAdd1.Text = custProf.Mbe_add1 + ' ' + custProf.Mbe_add2;
                        lblMobile.Text = custProf.Mbe_mob;

                        //  load_loyalty();
                    }
                }
            }
        }

        private void txtCard_no_Leave(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty( txtCard_no.Text))
                load_cus_details();
            else
            {
                lblAdd1.Text = "";
                lblCus.Text = "";
                lblCusName.Text = "";
                lblMobile.Text = "";
            }
        }
    }
}

