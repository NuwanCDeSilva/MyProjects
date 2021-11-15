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
using System.Diagnostics;

namespace FF.WindowsERPClient.General
{
    public partial class ExpenseLimitAlloc :Base
    {
        List<Expense_Limit_Alloc> _listExpense = new List<Expense_Limit_Alloc>();
        public ExpenseLimitAlloc()
        {
            InitializeComponent();
            dtFDate.Value = Convert.ToDateTime("01" + "/" + DateTime.Now.Date.Month + "/" + DateTime.Now.Date.Year).Date;
            dtTDate.Value = Convert.ToDateTime("01" + "/" + DateTime.Now.Date.Month + "/" + DateTime.Now.Date.Year).Date;
            bind_Combo_DropDownListPartyTypes();

        }
        private void clear()
        {
            ExpenseLimitAlloc formnew = new ExpenseLimitAlloc();
                formnew.MdiParent = this.MdiParent;
                formnew.Location = this.Location;
                formnew.Show();
                this.Close();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear the screen?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                clear();
            }
        }

     
        private void bind_Combo_DropDownListPartyTypes()
        {
            try
            {
                ddlRemTp.DataSource = null;
                var source = new BindingSource();
                source.DataSource = CHNLSVC.Financial.get_rem_type_by_sec("02", 0);
                if (source.DataSource != null)
                {
                    ddlRemTp.DataSource = source;
                    ddlRemTp.DisplayMember = "RSD_DESC";
                    ddlRemTp.ValueMember = "RSD_CD";
                    
                }

                Dictionary<string, string> PartyTypes = new Dictionary<string, string>();

         //       PartyTypes.Add("GPC", "GPC");
                PartyTypes.Add("CHNL", "Channel");
                PartyTypes.Add("SCHNL", "Sub Channel");
               // PartyTypes.Add("AREA", "Area");
               // PartyTypes.Add("REGION", "Region");
               // PartyTypes.Add("ZONE", "Zone");
                PartyTypes.Add("PC", "PC");

                DropDownListPartyTypes.DataSource = new BindingSource(PartyTypes, null);
                DropDownListPartyTypes.DisplayMember = "Value";
                DropDownListPartyTypes.ValueMember = "Key";



            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Limit Allocation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }




        private void btnClearHirchy_Click(object sender, EventArgs e)
        {
            checkBox_HIERCHY.Checked = false;
            grvParty.DataSource = null;
            grvParty.AutoGenerateColumns = false;
        }

        private void btnAll_Hirchy_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvParty.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grvParty.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }

        private void btnNon_Hierachy_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in grvParty.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = false;
                }
                grvParty.EndEdit();
            }
            catch (Exception ex)
            {

            }
        }
        //------------------------------------------------------------------------

        private List<string> get_selected_hierchyCodes()
        {
            grvParty.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvParty.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(DropDownListPartyTypes.SelectedValue.ToString());
                }
            }
            return list;
        }
        private List<string> get_selected_hierchyTypes()
        {
            grvParty.EndEdit();
            List<string> list = new List<string>();
            foreach (DataGridViewRow dgvr in grvParty.Rows)
            {
                DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    list.Add(dgvr.Cells[1].Value.ToString());
                }
            }
            return list;
        }



        private void btnAddSchemes_Click(object sender, EventArgs e)
        {

        }

        private void btnAddPB_Click(object sender, EventArgs e)
        {

        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Employee_Executive:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + "SEX" + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.InvoiceExecutive:
                    { paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefProf + seperator); break; }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
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
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GPC:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OPE:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        private void btnAddPartys_Click(object sender, EventArgs e)
        {
            Base _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            DataTable dt = new DataTable();
            DataTable _result = new DataTable();
            if (DropDownListPartyTypes.SelectedValue.ToString() == "COM")
            {
                MessageBox.Show("Invalid Hierachy Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
              //  _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
               // _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "CHNL")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "SCHNL")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }

            else if (DropDownListPartyTypes.SelectedValue.ToString() == "AREA")
            {
                MessageBox.Show("Invalid Hierachy Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
               // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
               // _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "REGION")
            {
                MessageBox.Show("Invalid Hierachy Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
              //  _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
               // _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "ZONE")
            {
                MessageBox.Show("Invalid Hierachy Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
              //  _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
              //  _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "PC")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "GPC")
            {
                MessageBox.Show("Invalid Hierachy Type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
                // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GPC);
                //  _result = _basePage.CHNLSVC.CommonSearch.Get_GPC(_CommonSearch.SearchParams, null, null);
                _result = CHNLSVC.General.Get_GET_GPC("", "");
            }
            grvParty.DataSource = null;
            grvParty.AutoGenerateColumns = false;
            grvParty.DataSource = _result;
        }

        private void DropDownListPartyTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnClearHirchy_Click(null, null);
            this.btnAddPartys_Click(null, null);
            _listExpense = new List<Expense_Limit_Alloc>();
            grvDet.DataSource = null;
            grvDet.AutoGenerateColumns = false;
        }



        private void checkBox_HIERCHY_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_HIERCHY.Checked == true)
            {
                this.btnAll_Hirchy_Click(null, null);
            }
            else
            {
                this.btnNon_Hierachy_Click(null, null);
            }
        }



        private void txtHierchCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtHierchCode.Focus();
                if (grvParty.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dgvr in grvParty.Rows)
                    {
                        if (dgvr.Cells["party_Code"].Value.ToString() == txtHierchCode.Text.Trim())
                        {
                            DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dgvr.Cells[0];
                            chk.Value = true;
                            dgvr.Selected = true;
                            MessageBox.Show("Selected!","Select",MessageBoxButtons.OK,MessageBoxIcon.Information);
                            txtHierchCode.Text = "";
                            //return;
                        }
                        else
                        {
                            DataGridViewCheckBoxCell chk = dgvr.Cells[0] as DataGridViewCheckBoxCell;
                            if (Convert.ToBoolean(chk.Value) == false)
                            {
                                dgvr.Selected = false;
                            }
                        }
                    }
                }

            }
        }

        private void btnHierachySearch_Click(object sender, EventArgs e)
        {
            Base _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            DataTable dt = new DataTable();
            DataTable _result = new DataTable();
            if (DropDownListPartyTypes.SelectedValue.ToString() == "COM")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "CHNL")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "SCHNL")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }

            else if (DropDownListPartyTypes.SelectedValue.ToString() == "AREA")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "REGION")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "ZONE")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "PC")
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            else if (DropDownListPartyTypes.SelectedValue.ToString() == "GPC")
            {
                // _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GPC);
                //  _result = _basePage.CHNLSVC.CommonSearch.Get_GPC(_CommonSearch.SearchParams, null, null);
                _result = CHNLSVC.General.Get_GET_GPC("", "");
            }

            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtHierchCode;
            _CommonSearch.ShowDialog();
            txtHierchCode.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(grvDet.Rows.Count==0)
            {
                MessageBox.Show("Please select the details to save", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No) return;

            string msg = "";
            Int32 _eff = CHNLSVC.General.SaveExpenseLimitAloc(_listExpense, out msg);
            if (_eff == 1)
            {
                MessageBox.Show("Successfully Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
            }
            else
            {
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void btnClearGrid_Click(object sender, EventArgs e)
        {
            grvECD_Def.DataSource = null;
            grvECD_Def.AutoGenerateColumns = false;
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
     


        }


        private void txtHierchCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnHierachySearch_Click(null, null);
        }

        private void txtHierchCode_DoubleClick(object sender, EventArgs e)
        {
            btnHierachySearch_Click(null, null);
        }

        private void btnAddLimit_Click(object sender, EventArgs e)
        {
            string _part_tp = "";
            DateTime _tmpFMonth = Convert.ToDateTime("01" + "/" + dtFDate.Value.Date.Month + "/" + dtFDate.Value.Date.Year).Date;
            DateTime _tmpTMonth = Convert.ToDateTime("01" + "/" + dtTDate.Value.Date.Month + "/" + dtTDate.Value.Date.Year).Date;

            _listExpense = new List<Expense_Limit_Alloc>();

            if (DropDownListPartyTypes.Text == "Channel")
                _part_tp = "CHNL";
            if (DropDownListPartyTypes.Text == "Sub Channel")
                _part_tp = "SCHNL";
            if (DropDownListPartyTypes.Text == "PC")
                _part_tp = "PC";

            foreach (DataGridViewRow row in grvParty.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                if (Convert.ToBoolean(chk.Value) == true)
                {
                    Expense_Limit_Alloc _expLimAloc = new Expense_Limit_Alloc();
                    _expLimAloc.Exla_com = BaseCls.GlbUserComCode;
                    _expLimAloc.Exla_cre_by = BaseCls.GlbUserID;
                    _expLimAloc.Exla_exp_cd = ddlRemTp.SelectedValue.ToString();
                    _expLimAloc.Exla_from = _tmpFMonth.Date;
                    _expLimAloc.Exla_pty_cd = row.Cells["party_Code"].Value.ToString();
                    _expLimAloc.Exla_pty_tp = _part_tp;
                    _expLimAloc.Exla_to = _tmpTMonth.Date;
                    _expLimAloc.Exla_val = Convert.ToDecimal(txtVal.Text);
                    _listExpense.Add(_expLimAloc);
                    
                }
            }

            grvDet.AutoGenerateColumns = false;
            grvDet.DataSource = _listExpense;
        }




    }
}
