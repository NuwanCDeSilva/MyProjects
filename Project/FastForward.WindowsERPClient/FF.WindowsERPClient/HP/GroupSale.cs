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
using FF.WindowsERPClient.HP;
using System.Globalization;

//Written By kapila on 07-03-2013
namespace FF.WindowsERPClient.HP
{
    public partial class GroupSale : Base
    {
        static string _newCompCode;
        bool _isDecimalAllow = false;
        protected List<GroupSaleCustomer> _GrpSaleCustomer = new List<GroupSaleCustomer>();

        public GroupSale()
        {
            InitializeComponent();



            gvCustProd.AutoGenerateColumns = false;
            txtDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            txtValidFrom.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            txtValidTo.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            txtVisitDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
        }

        public void setCompanyCode(string _comp)
        {
            _newCompCode = _comp;
            GetCompanyData(null, null);
        }

        private void BackDatePermission()
        {
            //IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, txtDate, lblBackDateInfor, string.Empty);

        }

        protected void GetGroupSaleData(object sender, EventArgs e)
        {
            try
            {
                GroupSaleHeader _groupSaleHeader = new GroupSaleHeader();
                if (!string.IsNullOrEmpty(txtGroupSaleCode.Text))
                {
                    _groupSaleHeader = CHNLSVC.Sales.GetGroupSaleHeaderDetails(txtGroupSaleCode.Text);

                    if (_groupSaleHeader != null)
                    {
                        lblGSaleDesn.Text = _groupSaleHeader.Hgr_desc;
                        txtValidFrom.Text = _groupSaleHeader.Hgr_from_dt.ToShortDateString(); ;
                        txtValidTo.Text = _groupSaleHeader.Hgr_to_dt.ToShortDateString();
                        txtVisitDate.Text = _groupSaleHeader.HGR_VISIT_DT.ToShortDateString();
                        txtFollow.Text = _groupSaleHeader.HGR_FOLLOW_UP;
                        txtContact.Text = _groupSaleHeader.Hgr_cont_cust;
                        txtContNo.Text = _groupSaleHeader.Hgr_cont_no;

                        if (_groupSaleHeader.Hgr_app_stus == 1)
                        {
                            lbl_status.Text = "Approved";
                            btnApprove.Enabled = false;
                            btnUpdate.Enabled = false;
                            //UpdatePanel1.Update();
                        }
                        else if (_groupSaleHeader.Hgr_app_stus == 2)
                        {
                            lbl_status.Text = "Rejected";
                            btnApprove.Enabled = false;
                            btnUpdate.Enabled = false;
                            //UpdatePanel1.Update();
                        }
                        else
                        {
                            lbl_status.Text = "Pending";
                            btnApprove.Enabled = true;
                            btnUpdate.Enabled = true;
                            //UpdatePanel1.Update();
                        }

                        if (_groupSaleHeader.Hgr_tp == "HS")
                        {
                            optHire.Checked = true;
                        }
                        else if (_groupSaleHeader.Hgr_tp == "CRED")
                        {
                            optCredit.Checked = true;
                        }

                        txtCompCode.Text = _groupSaleHeader.Hgr_Grup_com;
                        GetCompanyData(null, null);

                        _GrpSaleCustomer = CHNLSVC.Sales.GetGroupSaleCustomers(txtGroupSaleCode.Text);
                        gvCustProd.DataSource = _GrpSaleCustomer;


                        lbl_acc.Text = "";
                        lbl_Cust.Text = "";
                        lbl_items.Text = "";
                        lbl_value.Text = "";
                        //calc();
                        Int32 _NP = 0;
                        Int32 _NC = 0;
                        Int32 _NA = 0;
                        Decimal _TV = 0;
                        int Y = CHNLSVC.Sales.GetGroupSaleDet(txtGroupSaleCode.Text, out _NA, out _NP, out _NC, out _TV);
                        update_Summary(_NP, _NA, _NC, _TV);
                    }
                    else
                    {
                        _GrpSaleCustomer = new List<GroupSaleCustomer>();
                        MessageBox.Show("Please select the valid group sale code", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //ClearCompany();
                        //ClearCustomer();
                        //ClearGroupSale();
                        txtGroupSaleCode.Text = "";
                        txtGroupSaleCode.Focus();
                        return;
                    }
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }


        private void update_Summary(Int32 _noofprod, Int32 _noofAcc, Int32 _noofCust, Decimal _val)
        {
            Int32 _XX = (lbl_items.Text == string.Empty) ? 0 : Convert.ToInt32(lbl_items.Text);
            Int32 _X = (_XX + Convert.ToInt32(_noofprod));
            lbl_items.Text = (_X).ToString();

            Int32 _ZZ = (lbl_acc.Text == string.Empty) ? 0 : Convert.ToInt32(lbl_acc.Text);
            Int32 _Z = (_ZZ + Convert.ToInt32(_noofAcc));
            lbl_acc.Text = (_Z).ToString();

            Int32 _AA = (lbl_Cust.Text == string.Empty) ? 0 : Convert.ToInt32(lbl_Cust.Text);
            Int32 _A = (_AA + _noofCust);
            lbl_Cust.Text = (_A).ToString();

            Decimal _YY = (lbl_value.Text == string.Empty) ? 0 : Convert.ToDecimal(lbl_value.Text);
            Decimal _Y = _YY + Convert.ToDecimal(_val);
            lbl_value.Text = (_Y).ToString();
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
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator + -1 + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerAll:
                    {
                        paramsText.Append(seperator);
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
                case CommonUIDefiniton.SearchUserControlType.CustomerCommonByNIC:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void btnCreateNew_Click(object sender, EventArgs e)
        {

            HP.GroupCompany _GrpComp = new HP.GroupCompany();
            _GrpComp.ShowDialog();
            txtCompCode.Text = _newCompCode;
            GetCompanyData(null, null);
        }

        private void GetGroupCompanyDetails()
        {

        }

        private bool IsExistingCust(string _custCode, List<GroupSaleCustomer> _groupSaleCustList)
        {
            bool result = false;
            List<GroupSaleCustomer> _resultList = null;

            if (!string.IsNullOrEmpty(_custCode))
            {
                _resultList = _groupSaleCustList.Where(x => x.Hgc_cust_cd.Equals(_custCode)).ToList();
            }

            if (_resultList.Count > 0)
                result = true;

            return result;
        }

        private void btnAddItemNew_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNoOfAcc.Text))
            {
                MessageBox.Show("Number of A/Cs cannot be zero", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtNoOfProd.Text))
            {
                MessageBox.Show("Number of items cannot be zero", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtValue.Text))
            {
                MessageBox.Show("Value cannot be zero", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (lblCustName.Text == "")
            {
                MessageBox.Show("Please select the customer", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtNoOfProd.Text == "")
            {
                MessageBox.Show("Please enter no of products", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtNoOfAcc.Text == "")
            {
                MessageBox.Show("Please enter no of accounts", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtValue.Text == "")
            {
                MessageBox.Show("Please enter the value", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Convert.ToInt32(txtNoOfProd.Text) < Convert.ToInt32(txtNoOfAcc.Text))
            {
                MessageBox.Show("No of products cannot exceed no of accounts", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (IsExistingCust(txtCustCode.Text, _GrpSaleCustomer))
            {
                //MessageBox.Show("Customer already added.", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return;

                //remove from the list
                List<GroupSaleCustomer> _resultList = null;
                string _custCD = txtCustCode.Text;

                _resultList = _GrpSaleCustomer.Where(x => x.Hgc_cust_cd.Equals(txtCustCode.Text)).ToList();
                Int32 _no_ofProd = _resultList[0].Hgc_no_itm;
                Int32 _no_ofAcc = _resultList[0].Hgc_no_acc;
                Decimal _tot_Val = _resultList[0].Hgc_val;

                update_Summary(-(_no_ofProd), -(_no_ofAcc), -1, -(_tot_Val));

                _GrpSaleCustomer.RemoveAll(x => x.Hgc_cust_cd == _custCD);
            }


            //summary update
            update_Summary(Convert.ToInt32(txtNoOfProd.Text), Convert.ToInt32(txtNoOfAcc.Text), 1, Convert.ToDecimal(txtValue.Text));

            GroupSaleCustomer _grpSaleCust = new GroupSaleCustomer();
            MasterBusinessCompany _masterBusComp = new MasterBusinessCompany();
            //_masterBusComp.Mbe_name = lblCustName.Text;
            //_grpSaleCust.MasterBusinessCompany = _masterBusComp;
            _grpSaleCust.Hgc_Cust_Name = lblCustName.Text;
            _grpSaleCust.Hgc_no_itm = Convert.ToInt32(txtNoOfProd.Text);
            _grpSaleCust.Hgc_no_acc = Convert.ToInt32(txtNoOfAcc.Text);
            _grpSaleCust.Hgc_cust_cd = txtCustCode.Text;
            _grpSaleCust.Hgc_val = Convert.ToDecimal(txtValue.Text);

            _GrpSaleCustomer.Add(_grpSaleCust);

            ClearCustomer();

            //Bind the updated list to grid.
            gvCustProd.DataSource = new List<GroupSaleCustomer>();
            gvCustProd.DataSource = _GrpSaleCustomer;

        }

        private void ClearCustomer()
        {
            txtCustCode.Text = "";
            lblCustName.Text = "";
            lblCustAddr.Text = "";
            txtNoOfProd.Text = "";
            txtNoOfAcc.Text = "";
            txtValue.Text = "";
            btnUpdate.Enabled = true;

        }

        private void ClearCompany()
        {
            txtCompCode.Text = "";
            lblCompName.Text = "";
            lblCountry.Text = "";
            lblEmail.Text = "";
            lblFax.Text = "";
            lblTel.Text = "";
            lblAddr1.Text = "";
            lblAddr2.Text = "";
        }

        private void ClearGroupSaleDet()
        {
            lblGSaleDesn.Text = "";
            txtContact.Text = "";
            txtContNo.Text = "";
            txtFollow.Text = "";
        }


        private void imgbtnSearchGrpCode_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Group_Sale);
                DataTable _result = CHNLSVC.CommonSearch.GetGroupSaleSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtGroupSaleCode;
                _CommonSearch.ShowDialog();
                txtGroupSaleCode.Select();
                GetGroupSaleData(null, null);
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }

        }

        private void imgBtnCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 1;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommonByNIC);
                DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommonByNIC(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCustCode;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtCustCode.Select();
                GetCustomerData(null, null);
                txtNoOfProd.Focus();
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void GetCompanyData(object sender, EventArgs e)
        {
            try
            {
                MasterOutsideParty _outsideParty = null;
                Int32 _countGrpSale = 0;
                if (txtCompCode.Text == "")
                {
                    lblCompName.Text = "";
                    lblCountry.Text = "";
                    lblEmail.Text = "";
                    lblFax.Text = "";
                    lblTel.Text = "";
                    lblAddr1.Text = "";
                    lblAddr2.Text = "";


                    return;
                }
                _outsideParty = CHNLSVC.General.GetOutsideParty(txtCompCode.Text);
                if (_outsideParty != null)
                {
                    lblCompName.Text = _outsideParty.Mbi_desc;
                    lblCountry.Text = "SRI LANKA";
                    lblEmail.Text = _outsideParty.Mbi_email;
                    lblFax.Text = _outsideParty.Mbi_fax;
                    lblTel.Text = _outsideParty.Mbi_tel;
                    lblAddr1.Text = _outsideParty.Mbi_add1;
                    lblAddr2.Text = _outsideParty.Mbi_add2;

                    _countGrpSale = CHNLSVC.Sales.GetGroupSaleCountByCompany(txtCompCode.Text);
                    if (_countGrpSale > 0)
                    {
                        _countGrpSale = _countGrpSale + 1;
                        lblGSaleDesn.Text = _outsideParty.Mbi_desc + " - " + _countGrpSale.ToString("000");
                    }
                    else
                    {
                        lblGSaleDesn.Text = _outsideParty.Mbi_desc;
                    }

                }
                else
                {
                    lblCompName.Text = "";
                    lblCountry.Text = "";
                    lblEmail.Text = "";
                    lblFax.Text = "";
                    lblTel.Text = "";
                    lblAddr1.Text = "";
                    lblAddr2.Text = "";
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }
        protected void GetCustomerData(object sender, EventArgs e)
        {
            try
            {

                MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
                if (!string.IsNullOrEmpty(txtCustCode.Text))
                {
                    _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustCode.Text.Trim(), string.Empty, string.Empty, "C");


                    if (_masterBusinessCompany.Mbe_cd != null)
                    {
                        if (_masterBusinessCompany.Mbe_cd == "CASH")
                        {
                            txtCustCode.Text = _masterBusinessCompany.Mbe_cd;
                            lblCustName.Text = "";
                            lblCustAddr.Text = "";
                        }
                        else
                        {
                            txtCustCode.Text = _masterBusinessCompany.Mbe_cd;
                            lblCustName.Text = _masterBusinessCompany.Mbe_name;
                            lblCustAddr.Text = _masterBusinessCompany.Mbe_add1 + " " + _masterBusinessCompany.Mbe_add2;
                        }
                    }
                    else
                    {

                        MessageBox.Show("Please select the valid customer", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCustCode.Text = "";
                        lblCustName.Text = "";
                        lblCustAddr.Text = "";
                        txtCustCode.Focus();
                        return;
                    }
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, null, "GSAPP"))
                {
                    MessageBox.Show("Permission Denied.", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (txtGroupSaleCode.Text == "")
                {
                    MessageBox.Show("Select the group sale code", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (lbl_status.Text == "APPROVED")
                {
                    MessageBox.Show("Selected group sale is already Approved.", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Int16 row_aff = CHNLSVC.Sales.Approve_group_Sale(txtGroupSaleCode.Text, BaseCls.GlbUserID);

                MessageBox.Show("Successfully Approved.", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void imgbtnSearchComp_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OutsideParty);
                DataTable _result = CHNLSVC.CommonSearch.GetOutsidePartySearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCompCode;
                _CommonSearch.ShowDialog();
                txtCompCode.Select();
                GetCompanyData(null, null);
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string _groupSaleCode = "";

                DataTable _tbl = CHNLSVC.General.CheckGroupSaleInvoiceStatus(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtGroupSaleCode.Text.Trim());
                if (_tbl != null && _tbl.Rows.Count > 0)
                {
                    MessageBox.Show("The group sale already invoiced. You cannot edit the group sale.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (lbl_status.Text == "Approved")
                {
                    MessageBox.Show("Cannot save. Already Approved.", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (optCredit.Checked == false && optHire.Checked == false)
                {
                    MessageBox.Show("Please select the sale type", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (lblCompName.Text == "")
                {
                    MessageBox.Show("Please select the company", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtVisitDate.Text == "")
                {
                    MessageBox.Show("Please enter the visit date", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtValidFrom.Text == "")
                {
                    MessageBox.Show("Please enter the valid from date", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtValidTo.Text == "")
                {
                    MessageBox.Show("Please enter the valid to date", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtFollow.Text == "")
                {
                    MessageBox.Show("Please enter the follow up officer", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtContact.Text == "")
                {
                    MessageBox.Show("Please enter the contact person", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (txtContNo.Text == "")
                {
                    MessageBox.Show("Please enter the contact number", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if ((_GrpSaleCustomer == null) || (_GrpSaleCustomer.Count == 0))
                {
                    MessageBox.Show("Please add customers to List.", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                GroupSaleHeader _groupSale = new GroupSaleHeader();
                _groupSale.Hgr_grup_cd = txtGroupSaleCode.Text;
                _groupSale.Hgr_app_by = "";
                _groupSale.Hgr_app_dt = Convert.ToDateTime(txtDate.Text);
                _groupSale.Hgr_app_stus = 0;
                _groupSale.Hgr_Grup_com = txtCompCode.Text;
                _groupSale.Hgr_com = BaseCls.GlbUserComCode;
                _groupSale.Hgr_cont_cust = txtContact.Text;
                _groupSale.Hgr_cont_no = txtContNo.Text;
                _groupSale.Hgr_cre_by = BaseCls.GlbUserID;
                _groupSale.Hgr_cre_dt = Convert.ToDateTime(txtDate.Text);
                _groupSale.Hgr_desc = lblGSaleDesn.Text;
                _groupSale.Hgr_from_dt = Convert.ToDateTime(txtValidFrom.Text);
                _groupSale.Hgr_no_acc = Convert.ToInt32(lbl_acc.Text);
                _groupSale.Hgr_no_cust = Convert.ToInt32(lbl_Cust.Text);
                _groupSale.Hgr_no_itm = Convert.ToInt32(lbl_items.Text);
                _groupSale.Hgr_pc = BaseCls.GlbUserDefProf;
                _groupSale.Hgr_to_dt = Convert.ToDateTime(txtValidTo.Text);
                _groupSale.Hgr_tot_val = Convert.ToInt32(lbl_value.Text);
                _groupSale.Hgr_tp = (optHire.Checked == true ? "HS" : "CRED");
                _groupSale.HGR_FOLLOW_UP = txtFollow.Text;
                _groupSale.HGR_VISIT_DT = Convert.ToDateTime(txtVisitDate.Text);

                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                masterAuto.Aut_cate_tp = "GS";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "GS";
                masterAuto.Aut_start_char = "GS";
                masterAuto.Aut_year = null;

                _groupSale.GroupSaleCustomerList = _GrpSaleCustomer;

                int row_aff = CHNLSVC.Sales.SaveGroupSaleData(_groupSale, masterAuto, out _groupSaleCode);
                //this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully updated. Group sale code is: " + _groupSaleCode);


                MessageBox.Show("Successfully Saved! Document No. : " + _groupSaleCode + "", "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _GrpSaleCustomer = new List<GroupSaleCustomer>();

                btnClr_Click(null, null);
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "Group Sale", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnClr_Click(object sender, EventArgs e)
        {
            ClearCustomer();
            ClearCompany();
            ClearGroupSaleDet();
            gvCustProd.DataSource = null;
            txtGroupSaleCode.Text = "";
            txtValidFrom.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            txtValidTo.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
            txtVisitDate.Text = DateTime.Today.Date.ToString("dd/MM/yyyy");
        }

        private void GroupSale_Load(object sender, EventArgs e)
        {
            BackDatePermission();
        }

        private void btnNewCust_Click(object sender, EventArgs e)
        {
            General.CustomerCreation _CusCre = new General.CustomerCreation();

            _CusCre._isFromOther = true;
            _CusCre.obj_TragetTextBox = txtCustCode;
            _CusCre.ShowDialog();
            txtCustCode.Select();
            GetCustomerData(null, null);
            txtNoOfProd.Focus();
        }

        private void gvCustProd_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (gvCustProd.RowCount > 0)
            {
                int _rowIndex = e.RowIndex;
                int _colIndex = e.ColumnIndex;

                if (_rowIndex != -1)
                {


                    if (gvCustProd.Columns[_colIndex].Name == "rem_remove")
                    {
                        if (MessageBox.Show("Are you sure ?", "Day End", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return;
                        }
                        OnRemoveFromItemGrid(_rowIndex, _colIndex);


                        return;
                    }
                    else
                    {

                        txtCustCode.Text = Convert.ToString(gvCustProd.Rows[_rowIndex].Cells["Hgc_cust_cd"].Value);

                        MasterBusinessEntity _Customer = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, txtCustCode.Text, null, null, "C");
                        lblCustAddr.Text = _Customer.Mbe_add1 + ' ' + _Customer.Mbe_add2;
                        txtNoOfAcc.Text = Convert.ToString(gvCustProd.Rows[_rowIndex].Cells["Hgc_no_acc"].Value);
                        txtNoOfProd.Text = Convert.ToString(gvCustProd.Rows[_rowIndex].Cells["Hgc_no_itm"].Value);
                        lblCustName.Text = Convert.ToString(gvCustProd.Rows[_rowIndex].Cells["Hgc_Cust_Name"].Value);
                        txtValue.Text = Convert.ToString(gvCustProd.Rows[_rowIndex].Cells["Hgc_val"].Value);
                    }
                }
            }
        }

        protected void OnRemoveFromItemGrid(int _rowIndex, int _colIndex)
        {
            try
            {
                int row_id = _rowIndex;

                string _custCD = Convert.ToString(gvCustProd.Rows[row_id].Cells["Hgc_cust_cd"].Value);
                Int32 _no_ofProd = Convert.ToInt32(gvCustProd.Rows[row_id].Cells["Hgc_no_itm"].Value);
                Int32 _no_ofAcc = Convert.ToInt32(gvCustProd.Rows[row_id].Cells["Hgc_no_acc"].Value);
                Decimal _tot_Val = Convert.ToInt32(gvCustProd.Rows[row_id].Cells["Hgc_val"].Value);

                update_Summary(-(_no_ofProd), -(_no_ofAcc), -1, -(_tot_Val));

                _GrpSaleCustomer.RemoveAll(x => x.Hgc_cust_cd == _custCD);

                gvCustProd.DataSource = new List<GroupSaleCustomer>();
                gvCustProd.DataSource = _GrpSaleCustomer;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
            finally
            {
            }
        }

        private void txtNoOfProd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNoOfAcc.Focus();
            }
        }

        private void txtNoOfAcc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtValue.Focus();
            }
        }

        private void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddItemNew.Focus();
            }
        }

        private void txtNoOfProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsDecimalAllow(_isDecimalAllow, sender, e);
        }

        private void txtNoOfAcc_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsDecimalAllow(_isDecimalAllow, sender, e);
        }

        private void txtValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsDecimalAllow(_isDecimalAllow, sender, e);
        }

        private void optCredit_CheckedChanged(object sender, EventArgs e)
        {
            if (optCredit.Checked == true)
                btnNewCust.Visible = false;
            else
                btnNewCust.Visible = true;
        }

        private void txtContNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsDecimalAllow(_isDecimalAllow, sender, e);
        }

        private void txtGroupSaleCode_TextChanged(object sender, EventArgs e)
        {

        }

    }
}


