using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using FF.WindowsERPClient.General;
using System.IO;
//using IWshRuntimeLibrary;

namespace FF.WindowsERPClient.General
{
    public partial class ProfitCenterMasterDef : Base
    {

        public ProfitCenterMasterDef()
        {
            InitializeComponent();
            bindData();
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(txtComp.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + txtRegion.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + txtRegion.Text + seperator + txtZone.Text + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Province:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.District:
                    {
                        paramsText.Append(txtProvince.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OPE:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerAll:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Department:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void bindData()
        {
            txtComp.Text = BaseCls.GlbUserComCode;
            cmbType.SelectedIndex = 0;
            cmbDel.SelectedIndex = 0;
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            string errMsg = "";

            //kapila 6/4/2015
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10105))
            {
                MessageBox.Show("You don't have the permission.\nPermission Code :- 10105", "Permission", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (string.IsNullOrEmpty(txtChanel.Text) || string.IsNullOrEmpty(txtSChanel.Text) || string.IsNullOrEmpty(txtArea.Text) || string.IsNullOrEmpty(txtRegion.Text) || string.IsNullOrEmpty(txtZone.Text))
            {
                MessageBox.Show("Select the hierarchy !", "Profit Center Definition", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if(string.IsNullOrEmpty(txtPC.Text))
            {
                MessageBox.Show("Enter profit center code !", "Profit Center Definition", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (chkMultiDept.Checked == true && string.IsNullOrEmpty(txtDefDept.Text))
            {
                MessageBox.Show("Select the default department !", "Profit Center Definition", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtDistrict.Text) || string.IsNullOrEmpty(txtProvince.Text) || string.IsNullOrEmpty(txtSquareFeet.Text) || string.IsNullOrEmpty(txtGrade.Text) || string.IsNullOrEmpty(txtEpf.Text) || string.IsNullOrEmpty(txtManName.Text))
            {
                MessageBox.Show("Enter District/Province/Grade/Square feet/EPF/Manager name !", "Profit Center Definition", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtmainpc.Text))
            {
                MessageBox.Show("Enter Main profit center code !", "Profit Center Definition", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            MasterProfitCenter _pcenter = new MasterProfitCenter();

            _pcenter.Mpc_com = txtComp.Text;
            _pcenter.Mpc_cd = txtPC.Text;
            _pcenter.Mpc_desc = txtDesc.Text;
            if (cmbType.SelectedIndex == 0) { _pcenter.Mpc_tp = "P"; } else { _pcenter.Mpc_tp = "C"; }
            _pcenter.Mpc_oth_ref = txtOthRef.Text;
            _pcenter.Mpc_add_1 = txtAdd1.Text;
            _pcenter.Mpc_add_2 = txtAdd2.Text;
            _pcenter.Mpc_tel = txtPhone.Text;
            _pcenter.Mpc_fax = txtFax.Text;
            _pcenter.Mpc_act = chkAct.Checked;
            _pcenter.Mpc_chnl = txtChanel.Text;
            _pcenter.Mpc_ope_cd = txtOprTeam.Text;
            _pcenter.Mpc_def_pb = txtPB.Text;
            _pcenter.Mpc_edit_price = chk_edit_price.Checked;
            _pcenter.Mpc_chk_credit = chkCredBal.Checked;
            _pcenter.Mpc_edit_rate = Convert.ToInt32(txtEditRate.Text);
            _pcenter.Mpc_def_dis_rate = Convert.ToInt32(txtDefDisc.Text);
            _pcenter.Mpc_print_wara_remarks = chkPrintWarRem.Checked;
            _pcenter.Mpc_inter_com = chkInterCom.Checked;
            _pcenter.Mpc_print_dis = chkPrintDisc.Checked;
            _pcenter.Mpc_print_payment = chkPrintPay.Checked;
            _pcenter.Mpc_check_pay = chkChkPay.Checked;
            _pcenter.Mpc_check_cm = chkManCash.Checked;
            _pcenter.Mpc_without_price = chkAllowPrice.Checked;
            _pcenter.Mpc_order_valid_pd = Convert.ToInt32(txtValidPrd.Text);
            _pcenter.Mpc_order_restric = chkOrdRest.Checked;
            _pcenter.Mpc_wara_extend = Convert.ToInt32(txtExtWar.Text);
            _pcenter.Mpc_so_sms = chkSMS.Checked;
            _pcenter.Mpc_multi_dept = chkMultiDept.Checked;
            _pcenter.Mpc_def_dept = txtDefDept.Text;
            _pcenter.Mpc_def_loc = txtDefLoc.Text;
            _pcenter.Mpc_man = txtEpf.Text;
            _pcenter.Mpc_def_exrate = txtDefExRt.Text;
            _pcenter.Mpc_def_customer = txtDefCustomer.Text;

            Int32 _addHours = 0;
            Int32.TryParse(txtAddHours.Text, out _addHours);
            _pcenter.Mpc_add_hours = _addHours;

            //_pcenter.Mpc_add_hours = Convert.ToInt32(txtAddHours.Text);
            _pcenter.Mpc_email = txtEmail.Text;
            if (chkFwdSale.Checked == true) _pcenter.Mpc_fwd_sale_st = dtFwdSale.Value.Date;
            _pcenter.Mpc_max_fwdsale = Convert.ToInt32(txtMaxFwdSale.Text);
            _pcenter.Mpc_hp_sys_rec = chkHPRec.Checked;
            _pcenter.Mpc_is_chk_man_doc = chkManDoc.Checked;
            _pcenter.Mpc_is_do_now = cmbDel.SelectedIndex;
            _pcenter.MPC_DIST = txtDistrict.Text;
            _pcenter.MPC_PROV = txtProvince.Text;
            _pcenter.MPC_OPN_DT = dtOpenDate.Value.Date;
            _pcenter.MPC_SQ_FT = Convert.ToInt32(txtSquareFeet.Text);
            _pcenter.MPC_MAN_NAME = txtManName.Text;
            _pcenter.MPC_JOINED_DT = dtJoined.Value.Date;
            _pcenter.MPC_HOVR_DT = dtHOvr.Value.Date;
            _pcenter.MPC_NO_OF_STAFF = Convert.ToInt32(txtstaff.Text);
            _pcenter.MPC_GRADE = txtGrade.Text;
            _pcenter.MPC_NUM_FWDSALE = 0;
            _pcenter.Mpc_main_pc = txtmainpc.Text.ToString();

            int row_aff = CHNLSVC.General.Save_profit_center(_pcenter, txtChanel.Text, txtSChanel.Text, txtArea.Text, txtRegion.Text, txtZone.Text, BaseCls.GlbUserID, out errMsg);
            if (row_aff != -99 && row_aff >= 0)
            {
                MessageBox.Show("Successfully Updated.", "Profit Center", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(errMsg, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btn_Srch_chnl_Click(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtChanel;
            _CommonSearch.ShowDialog();
            txtChanel.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "CHNL", txtChanel.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtChnlDesc.Text = row2["descp"].ToString();
            }

            clear();
            txtPC.Text = "";
            txtSChanel.Text = "";
            txtArea.Text = "";
            txtRegion.Text = "";
            txtZone.Text = "";

        }

        private void btn_Srch_schnl_Click(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSChanel;
            _CommonSearch.ShowDialog();
            txtSChanel.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "SCHNL", txtSChanel.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtSChnlDesc.Text = row2["descp"].ToString();
            }

            clear();
            txtPC.Text = "";
            txtArea.Text = "";
            txtRegion.Text = "";
            txtZone.Text = "";
        }

        private void btn_Srch_area_Click(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtArea;
            _CommonSearch.ShowDialog();
            txtArea.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "AREA", txtArea.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtAreaDesc.Text = row2["descp"].ToString();
            }


            clear();
            txtPC.Text = "";
            txtRegion.Text = "";
            txtZone.Text = "";
        }

        private void btn_Srch_reg_Click(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtRegion;
            _CommonSearch.ShowDialog();
            txtRegion.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "REGION", txtRegion.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtRegDesc.Text = row2["descp"].ToString();
            }

            clear();
            txtPC.Text = "";
            txtZone.Text = "";
        }

        private void btn_Srch_zone_Click(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtZone;
            _CommonSearch.ShowDialog();
            txtZone.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "ZONE", txtZone.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtZoneDesc.Text = row2["descp"].ToString();
            }

            clear();
            txtPC.Text = "";
        }

        private void btn_Srch_pc_Click(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPC;
            _CommonSearch.ShowDialog();
            txtPC.Select();
            Load_PC_Details(txtComp.Text,txtPC.Text);
        }

        private void clear()
        {
            txtEmail.Text = "";
            txtDesc.Text = "";
            txtOthRef.Text = "";
            cmbDel.SelectedIndex = -1;
            chkManDoc.Checked = false;
            chkHPRec.Checked = false;
            txtMaxFwdSale.Text = "0";
            dtFwdSale.Value = DateTime.Now.Date;
            txtDefDept.Text = "";
            chkMultiDept.Checked = false;
            chkSMS.Checked = false;
            txtExtWar.Text = "0";
            chkOrdRest.Checked = false;
            txtValidPrd.Text = "0";
            chkAllowPrice.Checked = false;
            chkManCash.Checked = false;
            chkChkPay.Checked = false;
            chkPrintPay.Checked = false;
            chkPrintDisc.Checked = false;
            chkInterCom.Checked = false;
            chkPrintWarRem.Checked = false;
            txtDefDisc.Text = "0";
            txtEditRate.Text = "0";
            chkAct.Checked = false;
            txtAdd1.Text ="";
            txtAdd2.Text = "";
            txtAddHours.Text = "";
            txtDefCustomer.Text = "";
            txtPB.Text = "";
            chk_edit_price.Checked = false;
            txtFax.Text = "";
            txtEpf.Text = "";
            txtOprTeam.Text ="";
            txtPhone.Text ="";
            txtDistrict.Text = "";
            txtProvince.Text = "";
            dtOpenDate.Value = DateTime.Now.Date;
            txtSquareFeet.Text = "0";
            txtManName.Text = "";
            dtJoined.Value = DateTime.Now.Date;
            dtHOvr.Value = DateTime.Now.Date;
            txtstaff.Text = "0";
            txtGrade.Text = "";
            txtDefLoc.Text = "";
            txtDefExRt.Text = "0";
            chkCredBal.Checked = false;

        }

        private void Load_PC_Details(string _com, string _code)
        {
            MasterProfitCenter Prof = CHNLSVC.General.GetPCByPCCode(_com, _code);
            if (Prof == null)
            {
                clear();
                return;
            }
          
                try
                {
                    cmbDel.SelectedIndex = Prof.Mpc_is_do_now;
                }
                catch (Exception)
                {
                    
                   
                }
           
            chkManDoc.Checked = Prof.Mpc_is_chk_man_doc;
            chkHPRec.Checked = Prof.Mpc_hp_sys_rec;
            txtMaxFwdSale.Text = Convert.ToString(Prof.Mpc_max_fwdsale);
            if (Prof.Mpc_fwd_sale_st != Convert.ToDateTime("01/Jan/0001")) dtFwdSale.Value = Prof.Mpc_fwd_sale_st;
            txtDefDept.Text = Prof.Mpc_def_dept;
            chkMultiDept.Checked = Prof.Mpc_multi_dept;
            chkSMS.Checked = Prof.Mpc_so_sms;
            txtExtWar.Text = Convert.ToString(Prof.Mpc_wara_extend);
            chkOrdRest.Checked = Prof.Mpc_order_restric;
            txtValidPrd.Text = Convert.ToString(Prof.Mpc_order_valid_pd);
            chkAllowPrice.Checked = Prof.Mpc_without_price;
            chkManCash.Checked = Prof.Mpc_check_cm;
            chkChkPay.Checked = Prof.Mpc_check_pay;
            chkPrintPay.Checked = Prof.Mpc_print_payment;
            chkPrintDisc.Checked = Prof.Mpc_print_dis;
            chkInterCom.Checked = Prof.Mpc_inter_com;
            chkPrintWarRem.Checked = Prof.Mpc_print_wara_remarks;
            txtDefDisc.Text = Convert.ToString(Prof.Mpc_def_dis_rate);
            txtEditRate.Text = Convert.ToString(Prof.Mpc_edit_rate);
            chkCredBal.Checked = Prof.Mpc_chk_credit;
            txtDefExRt.Text = Prof.Mpc_def_exrate;
            txtDesc.Text = Prof.Mpc_desc;
            chkAct.Checked = Prof.Mpc_act;
            txtAdd1.Text = Prof.Mpc_add_1;
            txtAdd2.Text = Prof.Mpc_add_2;
            txtAddHours.Text = Prof.Mpc_add_hours.ToString();
            txtDefCustomer.Text = Prof.Mpc_def_customer;
            txtPB.Text = Prof.Mpc_def_pb;
            chk_edit_price.Checked = Prof.Mpc_edit_price;
            txtFax.Text = Prof.Mpc_fax;
            txtEpf.Text = Prof.Mpc_man;
            txtOprTeam.Text = Prof.Mpc_ope_cd;
            txtPhone.Text = Prof.Mpc_tel;
            txtDistrict.Text = Prof.MPC_DIST;
            txtProvince.Text = Prof.MPC_PROV;
            if (Prof.MPC_OPN_DT != Convert.ToDateTime("01/Jan/0001")) dtOpenDate.Value = Prof.MPC_OPN_DT;
            txtSquareFeet.Text = Prof.MPC_SQ_FT.ToString();
            txtManName.Text = Prof.MPC_MAN_NAME;
            if (Prof.MPC_JOINED_DT != Convert.ToDateTime("01/Jan/0001")) dtJoined.Value = Prof.MPC_JOINED_DT;
            if (Prof.MPC_HOVR_DT != Convert.ToDateTime("01/Jan/0001")) dtHOvr.Value = Prof.MPC_HOVR_DT;
            txtstaff.Text = Prof.MPC_NO_OF_STAFF.ToString();
            txtGrade.Text = Prof.MPC_GRADE;
            txtDefLoc.Text = Prof.Mpc_def_loc;
            txtEmail.Text = Prof.Mpc_email;
            txtOthRef.Text = Prof.Mpc_oth_ref;
            txtmainpc.Text = Prof.Mpc_main_pc;
            try
            {
                //ddlPcType.SelectedValue = Prof.Mpc_tp;
            }
            catch (Exception ex)
            {
                return;
            }

            List<MasterSalesPriorityHierarchy> _lstPCInfor=new List<MasterSalesPriorityHierarchy>();
            _lstPCInfor = CHNLSVC.General.GetPCHeirachy(txtPC.Text,txtComp.Text);

            if (_lstPCInfor != null)
            {
                string _var = "";
                _var = _lstPCInfor.Where(X => X.Mpi_cd == "CHNL").Select(X => X.Mpi_val).ToList()[0];
                if (_var != null) txtChanel.Text = _var;
                _var = _lstPCInfor.Where(X => X.Mpi_cd == "SCHNL").Select(X => X.Mpi_val).ToList()[0];
                if (_var != null) txtSChanel.Text = _var;
                _var = _lstPCInfor.Where(X => X.Mpi_cd == "AREA").Select(X => X.Mpi_val).ToList()[0];
                if (_var != null) txtArea.Text = _var;
                _var = _lstPCInfor.Where(X => X.Mpi_cd == "REGION").Select(X => X.Mpi_val).ToList()[0];
                if (_var != null) txtRegion.Text = _var;
                _var = _lstPCInfor.Where(X => X.Mpi_cd == "ZONE").Select(X => X.Mpi_val).ToList()[0];
                if (_var != null) txtZone.Text = _var;
            }
            
        }

        private void btn_Srch_PB_Click(object sender, EventArgs e)
        {
            txtPB.Text="";
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
            DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPB;
            _CommonSearch.ShowDialog();
            txtPB.Focus();
        }

        private void chkMultiDept_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMultiDept.Checked == true)
            {
                btn_Srch_dep.Enabled = true;
            }
            else
            {
                btn_Srch_dep.Enabled = false;
            }
        }

        private void chkFwdSale_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFwdSale.Checked == true)
            {
                dtFwdSale.Enabled = true;
            }
            else
            {
                dtFwdSale.Enabled = false;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPC.Text = "";
            clear();
            txtChanel.Text = "";
            txtSChanel.Text = "";
            txtArea.Text = "";
            txtRegion.Text = "";
            txtZone.Text = "";
            txtmainpc.Text="";
        }

        private void btn_Srch_dist_Click(object sender, EventArgs e)
        {
            if (txtProvince.Text == "")
            {
                MessageBox.Show("Select the Province", "Profit Center Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.District);
            DataTable _result = CHNLSVC.CommonSearch.GetDistrictByProvinceData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtDistrict;
            _CommonSearch.ShowDialog();
            txtDistrict.Focus();
        }

        private void btn_Srch_prov_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Province);
            DataTable _result = CHNLSVC.CommonSearch.GetProvinceData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtProvince;
            _CommonSearch.ShowDialog();
            txtProvince.Focus();
            txtDistrict.Text = "";
        }

        private void btn_Srch_opr_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OPE);
            DataTable _result = CHNLSVC.CommonSearch.GetOPE(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtOprTeam;
            _CommonSearch.ShowDialog();
            txtOprTeam.Select();
        }

        private void btn_Srch_cust_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _commonSearch = new CommonSearch.CommonSearch();
            _commonSearch = new CommonSearch.CommonSearch();
            _commonSearch.ReturnIndex = 0;
            _commonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
            DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(_commonSearch.SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
            _commonSearch.dvResult.DataSource = _result;
            _commonSearch.BindUCtrlDDLData(_result);
            _commonSearch.obj_TragetTextBox = txtDefCustomer;
            _commonSearch.IsSearchEnter = true; 
            this.Cursor = Cursors.Default;
            _commonSearch.ShowDialog();
            txtDefCustomer.Select();
        }

        private void btn_Srch_loc_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
            DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtDefLoc;
            _CommonSearch.ShowDialog();
            txtDefLoc.Select();
        }

        private void btn_Srch_dep_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Department);
            DataTable _result = CHNLSVC.CommonSearch.Get_Departments(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtDefDept;
            _CommonSearch.ShowDialog();
            txtDefDept.Select();
        }

        private void ProfitCenterMasterDef_Load(object sender, EventArgs e)
        {

        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btn_Srch_pc_Click(null, null);
            }
        }

        private void txtPC_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_pc_Click(null, null);
        }

        private void txtPC_Leave(object sender, EventArgs e)
        {
            Load_PC_Details(txtComp.Text, txtPC.Text);
        }

        private void btnmainpc_Click(object sender, EventArgs e)
        {
            //if (txtPC.Text != txtmainpc.Text)
            //{
            //    MessageBox.Show("Select the Profi", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtmainpc;
            _CommonSearch.ShowDialog();
            txtmainpc.Select();
          
        }

        private void txtmainpc_Leave(object sender, EventArgs e)
        {
            MasterProfitCenter Prof = CHNLSVC.General.GetPCByPCCode(txtComp.Text, txtmainpc.Text);
            if (Prof == null)
            {
                txtmainpc.Text = "";
                MessageBox.Show("Select correct Profit Center", "Profit Center Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
               
            }
        }

    }
}
