using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Sales
{
    public partial class SalePromotor : FF.WindowsERPClient.Base
    {
        string _searchType = "";
        public SalePromotor()
        {
            InitializeComponent();
            bindSchemes();

        }

        private void SystemErrorMessage(Exception ex)
        {
            CHNLSVC.CloseChannel();
            this.Cursor = Cursors.Default;
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void ClearScreen()
        {
            //Clear the screen
            txtAdd1.Clear();
            txtAdd2.Clear();
            txtNIC.Clear();
            txtName.Clear();
            txtMob.Text = "";
            chkActive.Checked = true;
            chkActive.Text = "Active";
            txtCode.Text = "";
            //lstPC.Clear();

        }
        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            //Set the caption of the check box
            if (chkActive.Checked) chkActive.Text = "Active";
            else chkActive.Text = "Inactive";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
           
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.spromotor:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EMP_ALL:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserID + seperator + BaseCls.GlbUserDefProf + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void txtTel_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMob.Text))
            {
                Boolean _isValid = IsValidMobileOrLandNo(txtMob.Text.Trim());
                if (_isValid == false)
                {
                    MessageBox.Show("Invalid telephone number.", "Service Agent", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtMob.Focus();
                    return;
                }
            }
        }


        private void load_details(string _code,string _mob, string _nic)
        {
            if (!string.IsNullOrEmpty(_code))
            {
                txtMob.Text = "";
                txtName.Text = "";
                txtAdd1.Text = "";
                txtAdd2.Text = "";
                txtNIC.Text = "";

          //      DataTable _dt = CHNLSVC.Financial.GetSalesPromoterDet(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtCode.Text);
                List<MST_PROMOTOR> _lstPromo = CHNLSVC.General.Get_PROMOTOR(_code, _mob, _nic);
             if (_lstPromo != null)
                {
                    txtMob.Text = _lstPromo[0].Mpr_mob;
                    txtName.Text = _lstPromo[0].Mpr_name;
                    txtAdd1.Text = _lstPromo[0].Mpr_add1;
                    txtAdd2.Text = _lstPromo[0].Mpr_add2;
                    txtNIC.Text = _lstPromo[0].Mpr_nic;
                    chkActive.Checked = Convert.ToBoolean(_lstPromo[0].Mpr_act);
                }
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
           
        }

        private void bindSchemes()
        {
            
            cmbType.DataSource = null;
            cmbType.DataSource = CHNLSVC.General.Get_PromotorType();
            cmbType.DisplayMember = "MPT_DESC";
            cmbType.ValueMember = "MPT_CD";
            cmbType.SelectedIndex = -1;
        }
        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAdd1.Focus();
        }
        private void txtAdd1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtAdd2.Focus();
        }
        private void txtAdd2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtMob.Focus();
        }
        private void txtTel_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNIC.Focus();
        }

        private void btnSearch_ServiceAgent_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.spromotor);
            DataTable _result = CHNLSVC.CommonSearch.SearchPromotor(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCode;
            _CommonSearch.ShowDialog();
            txtCode.Select();

            load_details(txtCode.Text ,null,null);
        }

        private void txtMob_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)Keys.Back) //allow backspace (to delete)
            {
                e.Handled = !char.IsNumber(e.KeyChar);
            }
        }

        private void btnSearchChannel_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtChanel;
                _CommonSearch.ShowDialog();
                txtChanel.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchSubChannel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtChanel.Text))
                {
                    MessageBox.Show("Please select channel.", "Promotor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSChanel.Text = "";
                    txtChanel.Focus();
                    return;
                }
                _searchType = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSChanel;
                _CommonSearch.ShowDialog();
                txtSChanel.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearchPC_Click(object sender, EventArgs e)
        {
            try
            {
                _searchType = "";
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPC;
                _CommonSearch.ShowDialog();
                txtPC.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnAddItem_Click(object sender, EventArgs e)
        {
            Boolean _isFound = false;
            try
            {

                Base _basePage = new Base();
                if (string.IsNullOrEmpty(cmbCommDef.Text))
                {
                    MessageBox.Show("Please select define by.", "Promotor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11065))
                {
                    if (cmbCommDef.Text != "Profit Center")
                    {
                        MessageBox.Show("you don't have permission to assing promotor for other pc(s). Permission Code - " + 11065, "Promotor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    txtPC.Text = BaseCls.GlbUserDefProf;
                    if (string.IsNullOrEmpty(txtPC.Text))
                    {
                        MessageBox.Show("Enter Profit Center", "Promotor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPC.Focus();
                        return;
                    }
                    if (!string.IsNullOrEmpty(txtPC.Text))
                    {
                        if (txtPC.Text != BaseCls.GlbUserDefProf)
                        {
                            MessageBox.Show("you don't have permission to assing promotor for other pc(s). Permission Code - " + 11065, "Promotor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }

                if (cmbCommDef.Text == "Profit Center")
                {
                    DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(txtCompany.Text, txtChanel.Text, txtSChanel.Text, null, null, null, txtPC.Text);
                    foreach (DataRow drow in dt.Rows)
                    {
                        if (lstPC.Items.Count == 0) lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                        foreach (ListViewItem Item in lstPC.Items)
                        {
                            if (Item.Text == drow["PROFIT_CENTER"].ToString())
                                _isFound = true;
                        }
                        if (_isFound == false)
                        {
                            lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                        }
                        _isFound = false;
                    }
                }
                else if (cmbCommDef.Text == "Sub Channel")
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _searchType = "";
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (!string.IsNullOrEmpty(txtSChanel.Text))
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtSChanel.Text);
                        foreach (DataRow drow in _result.Rows)
                        {
                            if (lstPC.Items.Count == 0) lstPC.Items.Add(drow["CODE"].ToString());
                            foreach (ListViewItem Item in lstPC.Items)
                            {
                                if (Item.Text == drow["CODE"].ToString())
                                    _isFound = true;
                            }
                            if (_isFound == false)
                            {
                                lstPC.Items.Add(drow["CODE"].ToString());
                            }
                            _isFound = false;
                        }
                    }
                    else
                    {
                        foreach (DataRow drow in _result.Rows)
                        {
                            if (lstPC.Items.Count == 0) lstPC.Items.Add(drow["CODE"].ToString());
                            foreach (ListViewItem Item in lstPC.Items)
                            {
                                if (Item.Text == drow["CODE"].ToString())
                                    _isFound = true;
                            }
                            if (_isFound == false)
                            {
                                lstPC.Items.Add(drow["CODE"].ToString());
                            }
                            _isFound = false;
                        }
                    }
                }


                else if (cmbCommDef.Text == "Channel")
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _searchType = "";
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (!string.IsNullOrEmpty(txtChanel.Text))
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtChanel.Text);
                        foreach (DataRow drow in _result.Rows)
                        {
                            if (lstPC.Items.Count == 0) lstPC.Items.Add(drow["CODE"].ToString());
                            foreach (ListViewItem Item in lstPC.Items)
                            {
                                if (Item.Text == drow["CODE"].ToString())
                                    _isFound = true;
                            }
                            if (_isFound == false)
                            {
                                lstPC.Items.Add(drow["CODE"].ToString());
                            }
                            _isFound = false;
                        }
                    }
                    else
                    {
                        foreach (DataRow drow in _result.Rows)
                        {
                            if (lstPC.Items.Count == 0) lstPC.Items.Add(drow["CODE"].ToString());
                            foreach (ListViewItem Item in lstPC.Items)
                            {
                                if (Item.Text == drow["CODE"].ToString())
                                    _isFound = true;
                            }
                            if (_isFound == false)
                            {
                                lstPC.Items.Add(drow["CODE"].ToString());
                            }
                            _isFound = false;
                        }
                    }
                }


                else if (cmbCommDef.Text == "Company")
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _searchType = "";
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                    if (!string.IsNullOrEmpty(txtCompany.Text))
                    {
                        _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtCompany.Text);
                        foreach (DataRow drow in _result.Rows)
                        {
                            if (lstPC.Items.Count == 0) lstPC.Items.Add(drow["CODE"].ToString());
                            foreach (ListViewItem Item in lstPC.Items)
                            {
                                if (Item.Text == drow["CODE"].ToString())
                                    _isFound = true;
                            }
                            if (_isFound == false)
                            {
                                lstPC.Items.Add(drow["CODE"].ToString());
                            }
                            _isFound = false;
                        }
                    }
                    else
                    {
                        foreach (DataRow drow in _result.Rows)
                        {
                            if (lstPC.Items.Count == 0) lstPC.Items.Add(drow["CODE"].ToString());
                            foreach (ListViewItem Item in lstPC.Items)
                            {
                                if (Item.Text == drow["CODE"].ToString())
                                    _isFound = true;
                            }
                            if (_isFound == false)
                            {
                                lstPC.Items.Add(drow["CODE"].ToString());
                            }
                            _isFound = false;
                        }
                    }


                }
            A:
                foreach (ListViewItem Item in lstPC.Items)
                {
                    Item.Checked = true;
                    Item.SubItems.Add(txt_sup.Text);  
                }
                txtChanel.Text = "";
                txtSChanel.Text = "";
                txtPC.Text = "";
                txtPC.Focus();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

     

        
       
        
        private void clear_pc()
        {  lstPC.Clear();
        cmbType.DataSource = null;
        bindSchemes();
        txtChanel.Text = "";
        txtSChanel.Text = "";
        txtPC.Text = "";
        lstPC.Clear();
        txtCompany.Text = "";
        txtChanel.Focus();
        }

        private void btnClearPc_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to clear ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            } 
            clear_pc();
        }

        private void btnPcHierarchySave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to save ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            } 
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                MessageBox.Show("Please select the promoter ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(cmbType.Text))
            {
                MessageBox.Show("Please select the promoter Type ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Please select the promoter name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtAdd1.Text))
            {
                MessageBox.Show("Please select the address", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(txtMob.Text))
            {
                Boolean _isValid = IsValidMobileOrLandNo(txtMob.Text.Trim());
                if (_isValid == false)
                {
                    MessageBox.Show("Invalid telephone number.", "Service Agent", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtMob.Focus();
                    return;
                }
            }


            List<string> pcList = new List<string>();
            foreach (ListViewItem Item in lstPC.Items)  //kapila 5/8/2014
            {
                string pc = Item.Text;

                if (Item.Checked == true)
                    pcList.Add(pc);
            }
            if (pcList.Count == 0)
            {
                MessageBox.Show("Please select the Sub channel/profit center which you have to assign.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (string.IsNullOrEmpty(txtAdd2.Text))
                txtAdd2.Text = ".";


            SalesPromotor _promo = new SalesPromotor();
            _promo.Mpp_promo_mob = txtMob.Text;
            _promo.Mpp_promo_name = txtName.Text;
            _promo.Mpp_promo_nic = txtNIC.Text;
            _promo.Mpp_promo_add1 = txtAdd1.Text;
            _promo.Mpp_promo_add2 = txtAdd2.Text;
            _promo.Mpp_promo_cd = txtCode.Text;
            _promo.Mpp_act = chkActive.Checked;
            if (cmbCommDef.Text == "Company")
            {
            _promo.Mpp_com = "COM";}
            if (cmbCommDef.Text == "Channel")
            {
            _promo.Mpp_com = "SCHNL";}
            if (cmbCommDef.Text == "Sub Channel")
            {
            _promo.Mpp_com = "SCHNL";}

            if (cmbCommDef.Text == "Profit Center")
            {
            _promo.Mpp_com = "PC";}

            _promo.Mpp_cre_by = BaseCls.GlbUserID;
            _promo.Mpp_mod_by = BaseCls.GlbUserID;
            _promo.Mpp_pc = BaseCls.GlbUserDefProf;
            _promo.Mpp_ref_cd = txt_sup.Text.Trim();

            _promo.Mpp_anal1 = cmbType.SelectedValue.ToString ();

            MasterAutoNumber _auto = new MasterAutoNumber();
            //_auto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            //_auto.Aut_cate_tp = "PC";
            //_auto.Aut_start_char = "SLP";
            //_auto.Aut_moduleid = "SLP";
            //_auto.Aut_direction = 1;
            //_auto.Aut_year = Convert.ToDateTime(DateTime.Now.Date).Year;
            //_auto.Aut_modify_dt = null;

            string _msg = "";
            int _effect = CHNLSVC.Financial.savePromoterDetails(_auto, _promo,pcList, out _msg);
            if (_effect > 0)
                MessageBox.Show("Successfully Updated!. ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (_effect <= 0)
                MessageBox.Show("Process terminated!. ", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ClearScreen();
            clear_pc();
        }

      

        private void btnSearchCom_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCompany;
                _CommonSearch.ShowDialog();

                txtCompany.Focus();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtChanel_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCode_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_ServiceAgent_Click(null, null);
        }

        private void txtCompany_DoubleClick(object sender, EventArgs e)
        {
            btnSearchCom_Click(null, null);
        }

        private void txtChanel_DoubleClick(object sender, EventArgs e)
        {
            btnSearchChannel_Click(null, null);
        }

        private void txtSChanel_DoubleClick(object sender, EventArgs e)
        {

            btnSearchSubChannel_Click(null, null);
        }

        private void txtPC_DoubleClick(object sender, EventArgs e)
        {
            btnSearchPC_Click(null, null);
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearch_ServiceAgent_Click(null, null);
            }
        }

        private void txtCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearchCom_Click(null, null);
            }
        }

        private void txtChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearchChannel_Click(null, null);
            }
        }

        private void txtSChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearchSubChannel_Click(null, null);
            }
        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearchPC_Click(null, null);
            }
        }

        private void txtCompany_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSChanel_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPC_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstPC.Items)
            {
                Item.Checked = true;
            }
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstPC.Items)
            {
                Item.Checked = false;
            }
        }

        private void btnPcClear_Click(object sender, EventArgs e)
        {
            txtChanel.Text = "";
            txtSChanel.Text = "";
            txtPC.Text = "";
            lstPC.Clear();
            txtCompany.Text = "";
            txtChanel.Focus();
        }

        private void txtCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                List<MST_PROMOTOR> _lstPromo = CHNLSVC.General.Get_PROMOTOR(txtCode.Text, null, null);
                if (_lstPromo == null)
                {
                    MessageBox.Show("Invlaid Promotor", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }
                else
                {
                    load_details(txtCode.Text, null, null);
                }
            }
        }

        private void btnSavePromo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to save ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            if (string.IsNullOrEmpty(txtCode.Text))
            {
                if (!string.IsNullOrEmpty(txtMob.Text))
                {
                    List<MST_PROMOTOR> _lstPromo = CHNLSVC.General.Get_PROMOTOR(null, txtMob.Text, null);
                    if (_lstPromo != null)
                    {
                        MessageBox.Show("Promotor Already Exist for this mobile number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        load_details(null, txtMob.Text, null);
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(txtNIC.Text))
                {
                    List<MST_PROMOTOR> lstPromo = CHNLSVC.General.Get_PROMOTOR(null, null, txtNIC.Text);
                    if (lstPromo != null)
                    {
                        MessageBox.Show("Promotor Already Exist for this NIC", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        load_details(null, null, txtNIC.Text);
                        return;
                    }
                }
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Please select the promoter name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtAdd1.Text))
            {
                MessageBox.Show("Please select the address", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!string.IsNullOrEmpty(txtMob.Text))
            {
                Boolean _isValid = IsValidMobileOrLandNo(txtMob.Text.Trim());
                if (_isValid == false)
                {
                    MessageBox.Show("Invalid telephone number.", "Service Agent", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMob.Clear();
                    txtMob.Focus();
                    return;
                }
            }
            if (txtemail.Text == "")//add By tharanga 2017/07/13
            {

            }
            else
            {
                IsValidEmail(txtemail.Text.ToString().Trim()); 
            }





            if (string.IsNullOrEmpty(txtAdd2.Text))
                txtAdd2.Text = ".";


            MST_PROMOTOR _promo = new MST_PROMOTOR();
            _promo.Mpr_cd = txtCode.Text;
            _promo.Mpr_mob = txtMob.Text;
            _promo.Mpr_name = txtName.Text;
            _promo.Mpr_nic = txtNIC.Text;
            _promo.Mpr_add1 = txtAdd1.Text;
            _promo.Mpr_add2 = txtAdd2.Text;
            _promo.Mpr_cd = txtCode.Text;
            _promo.Mpr_email = txtemail.Text;
            if (chkActive.Checked)
            {
                _promo.Mpr_act = 1;
            }
            else
            {
                _promo.Mpr_act = 0;
            }

            _promo.Mpr_cre_by = BaseCls.GlbUserID;
            _promo.Mpr_mod_by = BaseCls.GlbUserID;
            //   _promo.Mpp_pc = BaseCls.GlbUserDefProf;
            //   _promo.Mpr_cd = "";

            MasterAutoNumber _auto = new MasterAutoNumber();
            _auto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _auto.Aut_cate_tp = "PC";
            _auto.Aut_start_char = "SLP";
            _auto.Aut_moduleid = "SLP";
            _auto.Aut_direction = 1;
            _auto.Aut_year = null;
            _auto.Aut_modify_dt = null;

            string _msg = "";
            string _doc = "";
            int _isAvailable = 0;

            int _effect = CHNLSVC.Financial.savePromoter(_auto, _promo, out _doc ,  out  _msg);
            if (_effect > 0)
                txtCode.Text = _doc;
                MessageBox.Show(_msg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Add By Tharanga 2017/07/13 send Email and messabe
            //    if (txtemail.Text !="")
            //    {
            //        sendMail(_doc);
            //    }
            CHNLSVC.CustService.SendConfirmationMailPromoter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _doc, BaseCls.GlbDefSubChannel, BaseCls.GlbUserID,txtemail.Text.ToString().Trim(),txtMob.Text.ToString().Trim());
              
            if (_effect <= 0)
                MessageBox.Show("Process terminated!. ", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);

            clear_pc();

        }

        private void btnClearPromo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to clear ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }
            ClearScreen();
            clear_pc();
        }

        private void pnlLoc_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClearPC_Click_1(object sender, EventArgs e)
        {
            lstPC.Clear();
        }

        private void cmbCommDef_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11065))
            {
                if (cmbCommDef.Text != "Profit Center")
                {
                    MessageBox.Show("you don't have permission to assing promotor for other pc(s) .Permission Code - " + 11065, "Promotor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbCommDef.Text = "Profit Center";
                    return;
                }
            }

            if (cmbCommDef.Text == "Profit Center")
            {
                txtCompany.Enabled = false;
                txtChanel.Enabled = false;
                txtSChanel.Enabled = false;
                txtPC.Enabled = true;
                btnSearchPC.Enabled =true;
                btnSearchSubChannel.Enabled = false;
                btnSearchChannel.Enabled = false;
                btnSearchCom.Enabled = false; 
            }
            if (cmbCommDef.Text == "Sub Channel")
            {
                txtCompany.Enabled = false;
                txtChanel.Enabled = true;
                txtSChanel.Enabled = true;
                txtPC.Enabled = false;
                btnSearchSubChannel.Enabled =true;
                btnSearchPC.Enabled = false;

                btnSearchChannel.Enabled = true;
                btnSearchCom.Enabled = false; 
            }

            if (cmbCommDef.Text == "Channel")
            {
                txtCompany.Enabled = false;
                txtChanel.Enabled = true;
                txtSChanel.Enabled = false;
                txtPC.Enabled = false;
                btnSearchSubChannel.Enabled = false;
                btnSearchPC.Enabled = false;

                btnSearchChannel.Enabled = true;
                btnSearchCom.Enabled = false; ;
         
 
            }

            if (cmbCommDef.Text == "Company")
            {
                txtCompany.Enabled = true;
                txtChanel.Enabled = false;
                txtSChanel.Enabled = false;
                txtPC.Enabled = false;
                btnSearchCom.Enabled = true; 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = new DataTable();
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EMP_ALL);
            _result = CHNLSVC.CommonSearch.GetAllEmp(_CommonSearch.SearchParams, null, null);
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txt_sup;
            _CommonSearch.ShowDialog();
            txt_sup.Select();
            Cursor = Cursors.Default;
        }

        private void txt_sup_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_sup.Text))
            {
                DataTable dtTemp = CHNLSVC.Sales.GetManagerDefProfit(BaseCls.GlbUserComCode, txt_sup.Text);
                if (dtTemp.Rows.Count == 0)
                {
                    MessageBox.Show("Please enter correct EPF number", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txt_sup.Clear();
                    txt_sup.Focus();
                    return;
                }
            }
        }



        bool IsValidEmail(string email)
        {
           
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
                MessageBox.Show("Invalid Email Adrress.", "Service Agent", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtemail.Clear();
                txtemail.Focus();
            }
        }//Tharanga 2017/07/13
        //Tharanga 2017/07/13
        private void sendMail(string _doc)
        {
                string _mail = "";

                List<string> list = new List<string>();
                list.Add(txtemail.Text);
               

                _mail += "Dear Sir/Madam," + Environment.NewLine + Environment.NewLine;
                _mail += "\n You have register as a sales promoter in # " + BaseCls.GlbUserComCode + " Your promoter identification is : " + _doc + " . " + Environment.NewLine + Environment.NewLine;

                _mail += "*** This is an automatically generated email, please do not reply ***" + Environment.NewLine;

                for (int i = 0; i < list.Count; i++)
                {
                    CHNLSVC.CommonSearch.Send_SMTPMail(list[i], "Registered as a sales promoter", _mail);
                }

            
        }
       
    }
}
