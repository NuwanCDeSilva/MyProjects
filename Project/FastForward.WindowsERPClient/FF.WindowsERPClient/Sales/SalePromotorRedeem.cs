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
    public partial class SalePromotorRedeem : FF.WindowsERPClient.Base
    {
        string _searchType = "";
        public SalePromotorRedeem()
        {
            InitializeComponent();


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
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPC.Text + seperator);
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

                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
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


        private void load_details(string _code, string _mob, string _nic)
        {
            if (!string.IsNullOrEmpty(_code))
            {
                txtMob.Text = "";
                txtName.Text = "";
                txtAdd1.Text = "";
                txtAdd2.Text = "";
                txtNIC.Text = "";
                txtECash.Text = FormatToCurrency("0");
                txtEPoint.Text = "0";
                txtRCash.Text = FormatToCurrency("0");
                txtRPoint.Text = "0";
                txtCashBal.Text = FormatToCurrency("0");
                txtPointBal.Text = "0";

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
                    txtECash.Text =FormatToCurrency( _lstPromo[0].Mpr_earn_cs.ToString());
                    txtEPoint.Text = _lstPromo[0].Mpr_earn_pt.ToString();
                    txtRCash.Text =FormatToCurrency( _lstPromo[0].Mpr_red_cs.ToString());
                    txtRPoint.Text = _lstPromo[0].Mpr_red_pt.ToString();
                    txtCashBal.Text =FormatToCurrency( (Convert.ToDecimal(_lstPromo[0].Mpr_earn_cs) - Convert.ToDecimal(_lstPromo[0].Mpr_red_cs)).ToString());
                    txtPointBal.Text =( Convert.ToDecimal(_lstPromo[0].Mpr_earn_pt) - Convert.ToDecimal(_lstPromo[0].Mpr_red_pt)).ToString();
                }

                DataTable _dt = CHNLSVC.Financial.getPromotorRedeems(_code);
                gvRedeem.AutoGenerateColumns = false;
                gvRedeem.DataSource = _dt;

                DataTable _dt1 = CHNLSVC.Financial.getPromotorTrans(_code);
                gvEarn.AutoGenerateColumns = false;
                gvEarn.DataSource = _dt1;
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {

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
            if (string.IsNullOrEmpty(txtPC.Text))
            {
                MessageBox.Show("Please select the profit center.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPC.Focus();
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.spromotor);
            DataTable _result = CHNLSVC.CommonSearch.SearchPromotor(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCode;
            _CommonSearch.ShowDialog();
            txtCode.Select();

            load_details(txtCode.Text, null, null);
        }

        private void txtMob_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)Keys.Back) //allow backspace (to delete)
            {
                e.Handled = !char.IsNumber(e.KeyChar);
            }
        }

        private void txtCode_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_ServiceAgent_Click(null, null);
        }

        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearch_ServiceAgent_Click(null, null);
            }
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

                    txtMob.Focus();
                    return;
                }
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
            _auto.Aut_year = Convert.ToDateTime(DateTime.Now.Date).Year;
            _auto.Aut_modify_dt = null;

            string _msg = "";
            string _doc = "";
            int _isAvailable = 0;

            int _effect = CHNLSVC.Financial.savePromoter(_auto, _promo, out _doc, out  _msg);
            if (_effect > 0)
                txtCode.Text = _doc;
            MessageBox.Show(_msg, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


            if (_effect <= 0)
                MessageBox.Show("Process terminated!. ", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //  ClearScreen();

        }

        private void btnClearPromo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to clear ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }
            ClearScreen();
        }

        private void pnlLoc_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnRed_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10111))
            {
                MessageBox.Show("Sorry, You have no permission for redeem points!\n( Advice: Required permission code :10111)", "Redeem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(txtRem.Text))
            {
                    MessageBox.Show("Please enter remarks", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtRem.Focus();
                    return;
            }

            if (!string.IsNullOrEmpty(txtRedPoint.Text))
            {
                if (Convert.ToDecimal(txtRedPoint.Text) > Convert.ToDecimal(txtPointBal.Text))
                {
                    MessageBox.Show("Exceed the balance points", "Redeem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else
                txtRedPoint.Text = "0";

            if (!string.IsNullOrEmpty(txtRedCash.Text))
            {
                if (Convert.ToDecimal(txtRedCash.Text) > Convert.ToDecimal(txtCashBal.Text))
                {
                    MessageBox.Show("Exceed the balance cash", "Redeem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else
                txtRedCash.Text = "0";

            if (txtRedCash.Text == "0" && txtRedPoint.Text == "0")
            {
                MessageBox.Show("Redeem cash/Point value cannot be zero", "Redeem", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRedCash.Focus();
                return;
            }


            if (MessageBox.Show("Are you sure that you want to redeem ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            int _effect = CHNLSVC.Financial.UpdateRedeemPromotor(txtCode.Text, Convert.ToDecimal(txtRedPoint.Text), Convert.ToDecimal(txtRedCash.Text), BaseCls.GlbUserID,txtRem.Text);
            if (_effect > 0)
            {
                MessageBox.Show("Successfully redeem", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
            }
            else
                MessageBox.Show("Process terminated!. ", "Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void clear()
        {
            SalePromotorRedeem formnew = new SalePromotorRedeem();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

        private void txtRedCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtRedPoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtRedPoint_TextChanged(object sender, EventArgs e)
        {
           // txtRedCash.Text = "";
        }

        private void txtRedCash_TextChanged(object sender, EventArgs e)
        {
            //txtRedPoint.Text = "";
        }

        private void ImgBtnPC_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
            DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPC;
            _CommonSearch.txtSearchbyword.Text = txtPC.Text;
            _CommonSearch.ShowDialog();

            txtMob.Text = "";
            txtName.Text = "";
            txtAdd1.Text = "";
            txtAdd2.Text = "";
            txtNIC.Text = "";
            txtECash.Text =FormatToCurrency( "0");
            txtEPoint.Text = "0";
            txtRCash.Text =FormatToCurrency("0");
            txtRPoint.Text = "0";
            txtCashBal.Text =FormatToCurrency("0");
            txtPointBal.Text = "0";

            txtPC.Focus();
        }

        private void gvRedeem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(Convert.ToString(gvRedeem.Rows[e.RowIndex].Cells["mprl_rem"].Value) , "Remarks", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you need to clear the screen.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                ClearData();
        }
         private void ClearData()
        {
            SalePromotorRedeem formnew = new SalePromotorRedeem();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

         private void gvRedeem_CellContentClick(object sender, DataGridViewCellEventArgs e)
         {
             if (e.RowIndex != -1 && e.ColumnIndex == 0)
             {
                 if( Convert.ToDateTime(gvRedeem.Rows[e.RowIndex].Cells["mprl_cre_dt"].Value) != Convert.ToDateTime(DateTime.Now.Date))
                 {
                     MessageBox.Show("Cannot delete! Redeems can be only deleted within the day", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     return;
                 }
                 if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                 {
                     Int32 _seq = Convert.ToInt32(gvRedeem.Rows[e.RowIndex].Cells["mprl_seq"].Value);
                     Int32 _eff = CHNLSVC.Financial.deleteRedeemPoints(_seq, txtCode.Text);
                     gvRedeem.Rows.RemoveAt(e.RowIndex);

                     txtCode_Leave(null, null);
                 }
             }

         }

         private void txtPC_Leave(object sender, EventArgs e)
         {
             txtMob.Text = "";
             txtName.Text = "";
             txtAdd1.Text = "";
             txtAdd2.Text = "";
             txtNIC.Text = "";
             txtECash.Text = FormatToCurrency("0");
             txtEPoint.Text = "0";
             txtRCash.Text = FormatToCurrency("0");
             txtRPoint.Text = "0";
             txtCashBal.Text = FormatToCurrency("0");
             txtPointBal.Text = "0";
         }
    }
}
