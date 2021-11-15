using FF.BusinessObjects.InventoryNew;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//Form created by Udesh - 17-Oct-2018
namespace FF.WindowsERPClient.General
{
    public partial class ItemStatusChangeDefinition : Base
    {
        private DataTable dtItemStatus;
        private DataTable dtDefinition;
        private Dictionary<string, string> PartyTypes;

        public ItemStatusChangeDefinition()
        {
            InitializeComponent();

            InitializeItemStausTable();

            InitializeDefinitionTable();

            bind_Combo_DropDownListPartyTypes();

            AddPartys();

            SetBusinessControllers();
        }

        private void InitializeItemStausTable()
        {
            dtItemStatus = new DataTable();
            dtItemStatus.Columns.Add("From");
            dtItemStatus.Columns.Add("To");
            grvStatus.AutoGenerateColumns = false;
        }

        private void InitializeDefinitionTable()
        {
            dtDefinition = new DataTable();
            dtDefinition.Columns.Add("grvChkDefinition");
            dtDefinition.Columns.Add("ISP_PTY_TP");
            dtDefinition.Columns.Add("ISP_PTY_CD");
            dtDefinition.Columns.Add("ISP_ITM_STUS");// Item To Status
            dtDefinition.Columns.Add("ISP_FRM_STUS");// Item From Status
            dtDefinition.Columns.Add("ISP_ITM_CD");
            dtDefinition.Columns.Add("ISP_BRAND");
            dtDefinition.Columns.Add("ISP_ITM_CAT1");
            dtDefinition.Columns.Add("ISP_ITM_CAT2");
            dtDefinition.Columns.Add("ISP_MODEL");
            dtDefinition.Columns.Add("ISP_ALW_QTY");
            dtDefinition.Columns.Add("ISP_ALW_ITM_QTY");
            dtDefinition.Columns.Add("ISP_FRM_DT");// From Date
            dtDefinition.Columns.Add("ISP_TO_DT");// To Date
            grvDefinition.AutoGenerateColumns = false;
        }

        private void bind_Combo_DropDownListPartyTypes()
        {
            PartyTypes = new Dictionary<string, string>();

            PartyTypes.Add("COM", "Company");
            PartyTypes.Add("LOC", "Location");
            PartyTypes.Add("CHNL", "Channel");
            PartyTypes.Add("SCHNL", "Sub Channel");
            PartyTypes.Add("GRD", "Grade"); //Added bu Udesh 15-Nov-2018

            DropDownListPartyTypes.DataSource = new BindingSource(PartyTypes, null);
            DropDownListPartyTypes.DisplayMember = "Value";
            DropDownListPartyTypes.ValueMember = "Key";
        }

        private void AddPartys()
        {
            grvParty.DataSource = null;
            grvParty.AutoGenerateColumns = false;
            grvParty.DataSource = GetPartyData()[0];
        }

        private object[] GetPartyData()
        {
            object[] _resultObj = new object[2];

            DataTable dt = new DataTable();
            DataTable _result = new DataTable();
            Base _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            try
            {
                switch (DropDownListPartyTypes.SelectedValue.ToString())
                {
                    case "COM":
                        {
                            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                            _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                            break;
                        }
                    case "CHNL":
                        {
                            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                            _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                            break;
                        }
                    case "SCHNL":
                        {
                            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                            _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                            break;
                        }

                    case "LOC":
                        {
                            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                            _result = _basePage.CHNLSVC.General.GetSCM2LocationByCompany(BaseCls.GlbUserComCode);

                            break;
                        }
                    //Added by Udesh 15-Nov-2018
                    case "GRD":
                        {
                            _result = _basePage.CHNLSVC.General.GetMstGradeByCompany(BaseCls.GlbUserComCode);
                            break;
                        }
                    default:
                        break;
                }
            }
            catch { }

            _resultObj[0] = _result;        //Add DataTable details
            _resultObj[1] = _CommonSearch;  //Add CommonSearch parameters

            return _resultObj;
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                        break;
                    }
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
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(txtCate1.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                //    {
                //        paramsText.Append(txtCate1.Text + seperator + txtCate2.Text + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                //        break;
                //    }
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
                //case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                //    {
                //        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                //        break;
                //    }
                //case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                //    {
                //        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                //        break;
                //    }
                //case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                //    {
                //        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemBrand:
                    {
                        paramsText.Append("");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ModelMaster:
                    {
                        paramsText.Append("");
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.GPC:
                //    {
                //        paramsText.Append(seperator);
                //        break;
                //    }
                //case CommonUIDefiniton.SearchUserControlType.OPE:
                //    {
                //        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        private void btnClearHirchy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear the table?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                ClearHierarchyTable();
            }
        }

        private void ClearHierarchyTable()
        {
            checkBox_HIERCHY.Checked = false;
            grvParty.DataSource = null;
            grvParty.AutoGenerateColumns = false;
        }

        private void DropDownListPartyTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                ClearHierarchyTable();
                GetPartyData();
                AddPartys();
            }
            catch { }
            Cursor.Current = Cursors.Default;
        }

        private void btnHierachySearch_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                object[] _resultObj = GetPartyData();

                DataTable _result = (DataTable)_resultObj[0];
                CommonSearch.CommonSearch _CommonSearch = (CommonSearch.CommonSearch)_resultObj[1];

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtHierchCode;
                _CommonSearch.ShowDialog();
                txtHierchCode.Focus();
            }
            catch { }
            Cursor.Current = Cursors.Default;

        }

        private void txtHierchCode_DoubleClick(object sender, EventArgs e)
        {
            btnHierachySearch_Click(null, null);
        }

        private void txtHierchCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnHierachySearch_Click(null, null);
        }

        private void txtHierchCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool _isSelected = false;

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
                            MessageBox.Show("Selected!", "Select", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtHierchCode.Text = "";
                            _isSelected = true;
                            break;
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

                if (!_isSelected)
                {
                    MessageBox.Show("Could not find a record", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

            }
        }

        private void btnAll_Hirchy_Click(object sender, EventArgs e)
        {
            SelectAllHierarchy(true);
        }

        private void SelectAllHierarchy(bool _isCheck)
        {
            try
            {
                foreach (DataGridViewRow row in grvParty.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = _isCheck;
                }
                grvParty.EndEdit();
            }
            catch { }
        }

        private void btnNon_Hierachy_Click(object sender, EventArgs e)
        {
            SelectAllHierarchy(false);
        }

        private void txtFromItemstatus_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFromItemstatus.Text))
                {
                    DataTable _itemstatus = CHNLSVC.General.GetItemStatus(txtFromItemstatus.Text);

                    if (_itemstatus.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalid Item Status", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtFromItemstatus.Text = "";
                        lblFromStatus.Text = "";
                        txtFromItemstatus.Focus();
                        return;
                    }

                    else
                    {
                        if (!string.IsNullOrEmpty(txtToItemStatus.Text))
                        {
                            if (string.CompareOrdinal(txtToItemStatus.Text, txtFromItemstatus.Text) != 0)
                            {
                                lblFromStatus.Text = _itemstatus.Rows[0]["MIS_DESC"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("From & To Item Status could not be same", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txtFromItemstatus.Text = "";
                                lblFromStatus.Text = "";
                                txtFromItemstatus.Focus();
                            }
                        }
                        else
                        {
                            lblFromStatus.Text = _itemstatus.Rows[0]["MIS_DESC"].ToString();
                        }
                    }
                }
                else
                {
                    lblFromStatus.Text = "";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }

        private void txtToItemStatus_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtToItemStatus.Text))
                {

                    DataTable _itemstatus = CHNLSVC.General.GetItemStatus(txtToItemStatus.Text);

                    if (_itemstatus.Rows.Count == 0)
                    {
                        MessageBox.Show("Invalid Item Status", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtToItemStatus.Text = "";
                        lblToStatus.Text = "";
                        txtToItemStatus.Focus();
                        return;
                    }

                    else
                    {
                        if (!string.IsNullOrEmpty(txtFromItemstatus.Text))
                        {
                            if (string.CompareOrdinal(txtToItemStatus.Text, txtFromItemstatus.Text) != 0)
                            {
                                lblToStatus.Text = _itemstatus.Rows[0]["MIS_DESC"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("From & To Item Status could not be same", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txtToItemStatus.Text = "";
                                lblToStatus.Text = "";
                                txtToItemStatus.Focus();
                            }
                        }
                        else
                        {
                            lblToStatus.Text = _itemstatus.Rows[0]["MIS_DESC"].ToString();
                        }

                    }
                }
                else
                {
                    lblToStatus.Text = "";

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }

        private void btn_SearchItemFromStatus_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtFromItemstatus;
                _CommonSearch.ShowDialog();
                txtFromItemstatus.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }

        private void btn_SearchItemToStatus_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemStatus);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtToItemStatus;
                _CommonSearch.ShowDialog();
                txtToItemStatus.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
        }

        private void btnAddItemStatus_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFromItemstatus.Text))
            {
                MessageBox.Show("From Item Status could not be empty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtToItemStatus.Text))
            {
                MessageBox.Show("To Item Status could not be empty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.CompareOrdinal(txtFromItemstatus.Text, txtToItemStatus.Text) == 0)
            {
                MessageBox.Show("From & To Item Status could not be same", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (dtItemStatus.Rows.Count > 0)
            {
                var _checkResult = dtItemStatus.AsEnumerable().Where(r => r.Field<string>("From").Equals(txtFromItemstatus.Text) && r.Field<string>("To").Equals(txtToItemStatus.Text)).FirstOrDefault();
                if (_checkResult != null)
                {
                    MessageBox.Show("Record already added!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            var _row =dtItemStatus.NewRow();
            _row["From"] = txtFromItemstatus.Text;
            _row["To"] = txtToItemStatus.Text;

            dtItemStatus.Rows.Add(_row);
            grvStatus.DataSource = dtItemStatus;

            txtFromItemstatus.ResetText();
            txtToItemStatus.ResetText();
            lblFromStatus.ResetText();
            lblToStatus.ResetText();
        }

        private void btnStatusClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear item status table?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                ClearStatusTable();
            }
        }

        private void ClearStatusTable()
        {
            dtItemStatus.Rows.Clear();
            grvStatus.DataSource = dtItemStatus;
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

        private void btnMainCat_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCate1;
                _CommonSearch.txtSearchbyword.Text = txtCate1.Text;
                _CommonSearch.ShowDialog();
                txtCate1.Focus();
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

        private void btnCat_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCate2;
                _CommonSearch.txtSearchbyword.Text = txtCate2.Text;
                _CommonSearch.ShowDialog();
                txtCate2.Focus();
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

        private void btnBrand_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBrand;
                _CommonSearch.txtSearchbyword.Text = txtBrand.Text;
                _CommonSearch.ShowDialog();
                txtBrand.Focus();
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

        private void btnModel_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Model);
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
                DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(_CommonSearch.SearchParams, null, null);

                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtModel; //txtBox;
                _CommonSearch.ShowDialog();
                txtModel.Focus();
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

        private void btnItem_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemCD;
                _CommonSearch.txtSearchbyword.Text = txtItemCD.Text;
                _CommonSearch.ShowDialog();
                txtItemCD.Focus();
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

        private void txtAllowItemQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtAllowQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cmbSelectCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetBusinessControllers();
        }

        private void ClearBusinessData()
        {
            txtItemCD.ResetText();
            txtModel.ResetText();
            txtBrand.ResetText();
            txtCate1.ResetText();
            txtCate2.ResetText();
            lblItemCD.ResetText();
            lblModel.ResetText();
            lblBrand.ResetText();
            lblCate1.ResetText();
            lblCate2.ResetText();
        }

        private void SetBusinessControllers()
        {
            ClearBusinessData();
            txtItemCD.Enabled = false;
            btnItem.Enabled = false;
            txtModel.Enabled = false;
            btnModel.Enabled = false;
            txtBrand.Enabled = false;
            btnBrand.Enabled = false;
            txtCate1.Enabled = false;
            btnMainCat.Enabled = false;
            txtCate2.Enabled = false;
            btnCat.Enabled = false;
            txtAllowItemQuantity.Enabled = true;

            if(!string.IsNullOrEmpty(cmbSelectCat.Text))
            {
                string _itemString = cmbSelectCat.Text.Replace(" ", "");
                string[] _items = _itemString.Split(',');

                foreach (var item in _items)
                {
                    switch (item)
                    {
                        case "ITEM":
                            {
                                txtItemCD.Enabled = true;
                                btnItem.Enabled = true;
                                txtAllowItemQuantity.Enabled = false;
                                break;
                            }
                        case "MODEL":
                            {
                                txtModel.Enabled = true;
                                btnModel.Enabled = true;
                                break;
                            }
                        case "BRAND":
                            {
                                txtBrand.Enabled = true;
                                btnBrand.Enabled = true;
                                break;
                            }
                        case "M.CATEGORY":
                            {
                                txtCate1.Enabled = true;
                                btnMainCat.Enabled = true;
                                break;
                            }
                        case "CATEGORY":
                            {
                                txtCate2.Enabled = true;
                                btnCat.Enabled = true;
                                break;
                            }
                    }
                }
            }
        }



        private void btnAddDefinifion_Click(object sender, EventArgs e)
        {
            try
            {
                bool _isfoundParty = false;
                DataTable _dtTempParty = dtDefinition.Clone();



                if (string.IsNullOrEmpty(DropDownListPartyTypes.Text))
                {
                    MessageBox.Show("Please select Business Hierarchy", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (grvStatus.Rows.Count < 1)
                {
                    MessageBox.Show("Please select item from & to status", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (txtAllowItemQuantity.Text.Length<1)
                {
                    MessageBox.Show("Please enter allow item qty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (txtAllowQuantity.Text.Length < 1)
                {
                    MessageBox.Show("Please enter allow qty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (dateTimePickerFrom.Value.Date > dateTimePickerTo.Value.Date)
                {
                    MessageBox.Show("Please enter valid date renge", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if ((txtAllowItemQuantity.Enabled) && (decimal.Parse(txtAllowQuantity.Text) < decimal.Parse(txtAllowItemQuantity.Text)))
                {
                    MessageBox.Show("Allow qty should be greater than or equal Item qty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }



                if (!string.IsNullOrEmpty(cmbSelectCat.Text))
                {
                    string _itemString = cmbSelectCat.Text.Replace(" ", "");
                    string[] _items = _itemString.Split(',');

                    foreach (var item in _items)
                    {
                        switch (item)
                        {
                            case "ITEM":
                                {
                                    if(string.IsNullOrEmpty(txtItemCD.Text))
                                    {
                                        MessageBox.Show("Please enter item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return;
                                    }
                                    break;
                                }
                            case "MODEL":
                                {
                                    if (string.IsNullOrEmpty(txtModel.Text))
                                    {
                                        MessageBox.Show("Please enter model", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return;
                                    }
                                    break;
                                }
                            case "BRAND":
                                {
                                    if (string.IsNullOrEmpty(txtBrand.Text))
                                    {
                                        MessageBox.Show("Please enter brand", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return;
                                    }
                                    break;
                                }
                            case "M.CATEGORY":
                                {
                                    if (string.IsNullOrEmpty(txtCate1.Text))
                                    {
                                        MessageBox.Show("Please enter main category", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return;
                                    }
                                    break;
                                }
                            case "CATEGORY":
                                {
                                    if (string.IsNullOrEmpty(txtCate2.Text))
                                    {
                                        MessageBox.Show("Please enter category", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return;
                                    }
                                    break;
                                }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


                foreach (DataGridViewRow _row in grvParty.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)_row.Cells["dataGridViewCheckBoxColumn1"];
                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        _isfoundParty = true;

                        for (int _count = 0; _count < grvStatus.Rows.Count; _count++)
                        {
                            DataRow _newRow = _dtTempParty.NewRow();

                            _newRow["grvChkDefinition"] = true;
                            _newRow["ISP_PTY_TP"] = DropDownListPartyTypes.SelectedValue.ToString();
                            _newRow["ISP_PTY_CD"] = _row.Cells["party_Code"].Value.ToString();

                            _newRow["ISP_FRM_DT"] = dateTimePickerFrom.Value.Date.ToShortDateString();
                            _newRow["ISP_TO_DT"] = dateTimePickerTo.Value.Date.ToShortDateString();

                            _newRow["ISP_ITM_STUS"] = grvStatus.Rows[_count].Cells["To"].Value.ToString();
                            _newRow["ISP_FRM_STUS"] = grvStatus.Rows[_count].Cells["From"].Value.ToString();

                            _newRow["ISP_ITM_CD"] = txtItemCD.Text;
                            _newRow["ISP_BRAND"] = txtBrand.Text;
                            _newRow["ISP_ITM_CAT1"] = txtCate1.Text;
                            _newRow["ISP_ITM_CAT2"] = txtCate2.Text;
                            _newRow["ISP_MODEL"] = txtModel.Text;
                            _newRow["ISP_ALW_QTY"] = txtAllowQuantity.Text;
                            _newRow["ISP_ALW_ITM_QTY"] = (txtAllowItemQuantity.Enabled) ? txtAllowItemQuantity.Text : txtAllowQuantity.Text;


                            _dtTempParty.Rows.Add(_newRow);
                        }

                    }
                }

                if (!_isfoundParty)
                {
                    MessageBox.Show(string.Format("Please select {0}", DropDownListPartyTypes.Text), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                dtDefinition.Merge(_dtTempParty);

                grvDefinition.DataSource = null;
                grvDefinition.AutoGenerateColumns = false;
                grvDefinition.DataSource = dtDefinition;

                SelectAllHierarchy(false);
                ClearStatusTable();
                txtItemCD.ResetText();
                txtModel.ResetText();
                txtBrand.ResetText();
                txtCate1.ResetText();
                txtCate2.ResetText();
                lblItemCD.ResetText();
                lblModel.ResetText();
                lblBrand.ResetText();
                lblCate1.ResetText();
                lblCate2.ResetText();
                txtAllowItemQuantity.Text = "0";
                txtAllowQuantity.Text = "0";
                txtHierchCode.ResetText();
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnItemAll_Click(object sender, EventArgs e)
        {
            SelectAllDefinition(true);
        }

        private void SelectAllDefinition(bool _isCheck)
        {
            try
            {
                foreach (DataGridViewRow row in grvDefinition.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = _isCheck;
                }
                grvDefinition.EndEdit();
            }
            catch { }
        }

        private void btnItemNone_Click(object sender, EventArgs e)
        {
            SelectAllDefinition(false);
        }

        private void btnItemClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear the table?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                ClearDefinitionTable();
            }
        }

        private void ClearDefinitionTable()
        {
            chkAllDefinition.Checked = false;
            dtDefinition.Rows.Clear();
            grvDefinition.DataSource = null;
            grvDefinition.AutoGenerateColumns = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear all data?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                ClearAll();
            }
        }

        private void ClearAll()
        {
            ClearDefinitionTable();
            SelectAllHierarchy(false);

            ClearStatusTable();
            txtFromItemstatus.ResetText();
            txtToItemStatus.ResetText();
            lblFromStatus.ResetText();
            lblToStatus.ResetText();

            dateTimePickerFrom.ResetText();
            dateTimePickerTo.ResetText();

            ClearBusinessData();
            txtAllowItemQuantity.Text = "0";
            txtAllowQuantity.Text = "0";
            txtHierchCode.ResetText();
            txtFromItemstatus.ResetText();
            txtToItemStatus.ResetText();
            txtUploadItems.ResetText();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string _err = string.Empty;
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                List<ItemStatus_Change_def> _itemStatus_Change_def = new List<ItemStatus_Change_def>();
                foreach (DataGridViewRow _row in grvDefinition.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)_row.Cells["grvChkDefinition"];
                    if (Convert.ToBoolean(chk.Value) == true)
                    {
                        ItemStatus_Change_def _item = new ItemStatus_Change_def()
                        {
                            pttype = _row.Cells["ISP_PTY_TP"].Value.ToString(),
                            hierarchy = _row.Cells["ISP_PTY_CD"].Value.ToString(),
                            FromDate = DateTime.Parse(_row.Cells["ISP_FRM_DT"].Value.ToString()),
                            ToDate = DateTime.Parse(_row.Cells["ISP_TO_DT"].Value.ToString()),
                            status = _row.Cells["ISP_ITM_STUS"].Value.ToString(),
                            Fromstatus = _row.Cells["ISP_FRM_STUS"].Value.ToString(),
                            item = _row.Cells["ISP_ITM_CD"].Value.ToString(),
                            brand = _row.Cells["ISP_BRAND"].Value.ToString(),
                            mainCategory = _row.Cells["ISP_ITM_CAT1"].Value.ToString(),
                            category = _row.Cells["ISP_ITM_CAT2"].Value.ToString(),
                            model = _row.Cells["ISP_MODEL"].Value.ToString(),
                            alowQuantity = _row.Cells["ISP_ALW_QTY"].Value.ToString(),
                            alowItemQuantity = _row.Cells["ISP_ALW_ITM_QTY"].Value.ToString(),
                            createuser = BaseCls.GlbUserID,
                            company = BaseCls.GlbUserComCode,
                        };

                        _itemStatus_Change_def.Add(_item);
                    }
                }

                if (_itemStatus_Change_def.Count < 1)
                {
                    MessageBox.Show("No data to save", "Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (MessageBox.Show("Are you sure to save?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                Int32 _eff = CHNLSVC.Inventory.SaveItemStatusChangeDef(_itemStatus_Change_def, out _err);
                if (string.IsNullOrEmpty(_err))
                {
                    MessageBox.Show("Successfully Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearAll();
                }
                else
                {
                    MessageBox.Show("Error Occurred while processing...\n" + _err, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            }
            finally
            {
                CHNLSVC.CloseChannel();
                Cursor.Current = Cursors.Default;
            }
        }

        private void grvDefinition_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.grvDefinition.IsCurrentCellDirty && (this.grvDefinition.CurrentCell.OwningColumn == grvDefinition.Columns["grvChkDefinition"]))
                {
                    this.grvDefinition.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
            catch { }
        }

        private void txtFromItemstatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_SearchItemFromStatus_Click(null, null);
        }

        private void txtToItemStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btn_SearchItemToStatus_Click(null, null);
        }

        private void txtCate1_KeyDown(object sender, KeyEventArgs e)
        {
             if (e.KeyCode == Keys.F2)
                 btnMainCat_Click(null, null);            
        }

        private void txtCate2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnCat_Click(null, null);  
        }

        private void txtBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnBrand_Click(null, null);  
        }

        private void txtModel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnModel_Click(null, null);  
        }

        private void txtItemCD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnItem_Click(null, null);
        }

        private void grvDefinition_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {

                if (e.NewValue > chkAllDefinition.Width/2)
                    chkAllDefinition.Visible = false;
                else
                    chkAllDefinition.Visible = true;
            }
        }

        private void txtCate1_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCate1.Text))
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                    //DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, "Code", string.Format("%{0}", txtCate1.Text));

                    var _filter = _result.AsEnumerable().Where(r => r.Field<string>("CODE") == txtCate1.Text).FirstOrDefault();

                    if (_filter==null)
                    {
                        MessageBox.Show("Invalid main category", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtCate1.Text = "";
                        lblCate1.Text = "";
                        txtCate1.Focus();
                        return;
                    }
                    else
                    {
                        lblCate1.Text = _filter["DESCRIPTION"].ToString();
                    }

                }

                else
                {
                    lblCate1.ResetText();
                }


                lblCate2.ResetText();
                txtCate2.ResetText();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtCate2_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCate2.Text))
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                    //DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                    DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, "Code", string.Format("%{0}", txtCate2.Text));

                    var _filter = _result.AsEnumerable().Where(r => r.Field<string>("CODE") == txtCate2.Text).FirstOrDefault();

                    if (_filter == null)
                    {
                        MessageBox.Show("Invalid category", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtCate2.Text = "";
                        lblCate2.Text = "";
                        txtCate2.Focus();
                        return;
                    }
                    else
                    {
                        lblCate2.Text = _filter["DESCRIPTION"].ToString();
                    }

                }

                else
                {
                    lblCate2.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtBrand_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtBrand.Text))
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                    //DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(_CommonSearch.SearchParams, null, null);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(_CommonSearch.SearchParams, "Code", string.Format("%{0}", txtBrand.Text));

                    var _filter = _result.AsEnumerable().Where(r => r.Field<string>("CODE") == txtBrand.Text).FirstOrDefault();

                    if (_filter == null)
                    {
                        MessageBox.Show("Invalid brand", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtBrand.Text = "";
                        lblBrand.Text = "";
                        txtBrand.Focus();
                        return;
                    }
                    else
                    {
                        lblBrand.Text = _filter["DESCRIPTION"].ToString();
                    }

                }

                else
                {
                    lblBrand.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtModel_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtModel.Text))
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
                    DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(_CommonSearch.SearchParams, "Code", string.Format("%{0}", txtModel.Text));

                    var _filter = _result.AsEnumerable().Where(r => r.Field<string>("CODE") == txtModel.Text).FirstOrDefault();

                    if (_filter == null)
                    {
                        MessageBox.Show("Invalid model", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtModel.Text = "";
                        lblModel.Text = "";
                        txtModel.Focus();
                        return;
                    }
                    else
                    {
                        lblModel.Text = _filter["DESCRIPTION"].ToString();
                    }

                }

                else
                {
                    lblModel.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtItemCD_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtItemCD.Text))
                {
                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                    _CommonSearch.ReturnIndex = 0;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                    //DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, null, null);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_CommonSearch.SearchParams, "item", string.Format("%{0}", txtItemCD.Text));

                    var _filter = _result.AsEnumerable().Where(r => r.Field<string>("Item") == txtItemCD.Text).FirstOrDefault();

                    if (_filter == null)
                    {
                        MessageBox.Show("Invalid item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtItemCD.Text = "";
                        lblItemCD.Text = "";
                        txtItemCD.Focus();
                        return;
                    }
                    else
                    {
                        lblItemCD.Text = _filter["Description"].ToString();
                    }

                }

                else
                {
                    lblItemCD.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = "Excel files(*.xls)|*.xls,*.xlsx|All files(*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.ShowDialog();
            txtUploadItems.Text = openFileDialog1.FileName;
        }

        private void btnUploadItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                grvDefinition.DataSource = null;
                string _msg = string.Empty;
                bool _isfoundParty = false;
                StringBuilder _errorLst = new StringBuilder();
                DataTable _dtTempParty = dtDefinition.Clone();
                
                if (string.IsNullOrEmpty(txtUploadItems.Text))
                {
                    MessageBox.Show("Please select upload file path.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUploadItems.Text = "";
                    txtUploadItems.Focus();
                    return;
                }

                

                if (string.IsNullOrEmpty(DropDownListPartyTypes.Text))
                {
                    MessageBox.Show("Please select Business Hierarchy", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (grvStatus.Rows.Count < 1)
                {
                    MessageBox.Show("Please select item from & to status", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (dateTimePickerFrom.Value.Date > dateTimePickerTo.Value.Date)
                {
                    MessageBox.Show("Please enter valid date range", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (string.IsNullOrEmpty(cmbSelectCat.Text))
                {
                    MessageBox.Show("Please select type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


                if (MessageBox.Show(string.Format("You can save {0} type(s) only. Are you sure do you want to proceed?", cmbSelectCat.Text), "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }

                
                System.IO.FileInfo fileObj = new System.IO.FileInfo(txtUploadItems.Text);

                if (fileObj.Exists == false)
                {
                    MessageBox.Show("Selected file does not exist at the following path.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUploadItems.Focus();
                }

                string Extension = fileObj.Extension;

                string conStr = "";

                if (Extension.ToUpper() == ".XLS")
                {

                    conStr = ConfigurationManager.ConnectionStrings["ConStringExcel03"]
                             .ConnectionString;
                }
                else if (Extension.ToUpper() == ".XLSX")
                {
                    conStr = ConfigurationManager.ConnectionStrings["ConStringExcel07"]
                              .ConnectionString;

                }
                else
                {
                    MessageBox.Show("Unable to upload. please select the correct file", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                conStr = String.Format(conStr, txtUploadItems.Text, "NO");
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                cmdExcel.Connection = connExcel;

                //Get the name of First Sheet
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();
                if (dt.Rows.Count > 0)
                {
                    dt.Rows.RemoveAt(0);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No data found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        CommonSearch.CommonSearch _itemSearch = new CommonSearch.CommonSearch();
                        CommonSearch.CommonSearch _brandSearch = new CommonSearch.CommonSearch();
                        CommonSearch.CommonSearch _modelSearch = new CommonSearch.CommonSearch();
                        CommonSearch.CommonSearch _cat1Search = new CommonSearch.CommonSearch();
                        CommonSearch.CommonSearch _cat2Search = new CommonSearch.CommonSearch();
                        //DataTable _itemResult = new DataTable();
                        //DataTable _brandResult = new DataTable();
                        //DataTable _modelResult = new DataTable();
                        //DataTable _cat1Result = new DataTable();
                        //DataTable _cat2Result = new DataTable();

                        #region Prepare Master Data
                        if (txtItemCD.Enabled)
                        {
                            _itemSearch.ReturnIndex = 0;
                            _itemSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                            //_itemResult = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_itemSearch.SearchParams, null, null);
                        }

                        if (txtBrand.Enabled)
                        {
                            _brandSearch.ReturnIndex = 0;
                            _brandSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemBrand);
                            //_brandResult = CHNLSVC.CommonSearch.GetItemBrands(_brandSearch.SearchParams, null, null);
                        }

                        if (txtModel.Enabled)
                        {
                            _modelSearch.ReturnIndex = 0;
                            _modelSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ModelMaster);
                            //_modelResult = CHNLSVC.CommonSearch.GetModelMaster(_modelSearch.SearchParams, null, null);
                        }

                        if (txtCate1.Enabled)
                        {
                            _cat1Search.ReturnIndex = 0;
                            _cat1Search.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                            //_cat1Result = CHNLSVC.CommonSearch.GetCat_SearchData(_cat1Search.SearchParams, null, null);
                        }

                        if (txtCate2.Enabled)
                        {
                            _cat2Search.ReturnIndex = 0;
                            _cat2Search.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                            //_cat2Result = CHNLSVC.CommonSearch.GetCat_SearchData(_cat2Search.SearchParams, null, null);
                        }

                        #endregion

                        int _rowCount = 0;
                        foreach (DataRow _dr in dt.Rows)
                        {
                            _rowCount++;

                            #region Allow Quantity
                            if (txtAllowQuantity.Enabled)
                            {
                                string _codeValue = _dr[0].ToString();
                                decimal _value = 0;

                                if (string.IsNullOrEmpty(_codeValue))
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append(string.Format("Empty Allow Quantity contain in row number {0}", _rowCount));
                                    else _errorLst.Append(string.Format(" and Empty Allow Quantity contain in row number {0}", _rowCount));
                                    continue;
                                }

                                if (!decimal.TryParse(_codeValue, out _value))
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("Allow Quantity must be numeric value - " + _codeValue);
                                    else _errorLst.Append(" and Allow Quantity must be numeric value - " + _codeValue);
                                    continue;
                                }

                                if (_value < 0)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("Allow Quantity must be positive value : " + _codeValue);
                                    else _errorLst.Append(" and Allow Quantity must be positive value : " + _codeValue);
                                    continue;
                                }
                            }
                            #endregion

                            #region Allow Item Quantity
                            if (txtAllowItemQuantity.Enabled)
                            {
                                string _codeValue = _dr[1].ToString();
                                decimal _value = 0;

                                if (string.IsNullOrEmpty(_codeValue))
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append(string.Format("Empty Allow Item Quantity contain in row number {0}", _rowCount));
                                    else _errorLst.Append(string.Format(" and Empty Allow Item Quantity contain in row number {0}", _rowCount));
                                    continue;
                                }

                                if (!decimal.TryParse(_codeValue, out _value))
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("Allow Item Quantity must be numeric value - " + _codeValue);
                                    else _errorLst.Append(" and Allow Item Quantity must be numeric value - " + _codeValue);
                                    continue;
                                }

                                if (_value < 0)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("Allow Item Quantity must be positive value : " + _codeValue);
                                    else _errorLst.Append(" and Allow Item Quantity must be positive value : " + _codeValue);
                                    continue;
                                }

                                if ((txtAllowItemQuantity.Enabled) && (decimal.Parse(_dr[0].ToString()) < decimal.Parse(_dr[1].ToString())))
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("Allow qty should be greater than or equal Item qty");
                                    else _errorLst.Append(" and Allow qty should be greater than or equal Item qty");
                                    continue;
                                }
                            }
                            #endregion

                            #region Item Code
                            if (txtItemCD.Enabled)
                            {
                                string _codeValue = _dr[2].ToString();

                                if (string.IsNullOrEmpty(_codeValue))
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append(string.Format("Empty Item code contain in row number {0}", _rowCount));
                                    else _errorLst.Append(string.Format(" and Empty Item code contain in row number {0}", _rowCount));
                                    continue;
                                }

                                //var _result = _itemResult.AsEnumerable().Where(r => r.Field<string>("Item") == _codeValue).FirstOrDefault();
                                var _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(_itemSearch.SearchParams, "ITEM", _codeValue);
                                if (_result == null || _result.Rows.Count < 1)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("Invalid Item code - " + _codeValue);
                                    else _errorLst.Append(" and Invalid Item code - " + _codeValue);
                                    continue;
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(_dr[2].ToString()))
                                {
                                    if (_dr[2].ToString().Trim().Length > 0)
                                    {
                                        if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append(string.Format("Item code must be empty in row number {0}", _rowCount));
                                        else _errorLst.Append(string.Format(" and Item code must be empty in row number {0}", _rowCount));
                                        continue;
                                    }
                                }
                            }
                            #endregion

                            #region Model
                            if (txtModel.Enabled)
                            {
                                string _codeValue = _dr[3].ToString();

                                if (string.IsNullOrEmpty(_codeValue))
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append(string.Format("Empty Model code contain in row number {0}", _rowCount));
                                    else _errorLst.Append(string.Format(" and Empty Model code contain in row number {0}", _rowCount));
                                    continue;
                                }

                                //var _result = _modelResult.AsEnumerable().Where(r => r.Field<string>("CODE") == _codeValue).FirstOrDefault();
                                var _result = CHNLSVC.CommonSearch.GetModelMaster(_modelSearch.SearchParams, "CODE", _codeValue);
                                if (_result == null || _result.Rows.Count < 1)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("Invalid Model code - " + _codeValue);
                                    else _errorLst.Append(" and Invalid Model code - " + _codeValue);
                                    continue;
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(_dr[3].ToString()))
                                {
                                    if (_dr[3].ToString().Trim().Length > 0)
                                    {
                                        if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append(string.Format("Model code must be empty in row number {0}", _rowCount));
                                        else _errorLst.Append(string.Format(" and Model code must be empty in row number {0}", _rowCount));
                                        continue;
                                    }
                                }
                            }
                            #endregion

                            #region Main Category
                            if (txtCate1.Enabled)
                            {
                                string _codeValue = _dr[4].ToString();

                                if (string.IsNullOrEmpty(_codeValue))
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append(string.Format("Empty Main category code contain in row number {0}", _rowCount));
                                    else _errorLst.Append(string.Format(" and Empty Main category code contain in row number {0}", _rowCount));
                                    continue;
                                }

                                //var _result = _cat1Result.AsEnumerable().Where(r => r.Field<string>("CODE") == _codeValue).FirstOrDefault();
                                var _result = CHNLSVC.CommonSearch.GetCat_SearchData(_cat1Search.SearchParams, "CODE", _codeValue);
                                if (_result == null || _result.Rows.Count < 1)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("Invalid Main category code - " + _codeValue);
                                    else _errorLst.Append(" and Invalid Main category code - " + _codeValue);
                                    continue;
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(_dr[4].ToString()))
                                {
                                    if (_dr[4].ToString().Trim().Length > 0)
                                    {
                                        if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append(string.Format("Main category code must be empty in row number {0}", _rowCount));
                                        else _errorLst.Append(string.Format(" and Main category code must be empty in row number {0}", _rowCount));
                                        continue;
                                    }
                                }
                            }
                            #endregion

                            #region Category
                            if (txtCate2.Enabled)
                            {
                                string _codeValue = _dr[5].ToString();

                                if (string.IsNullOrEmpty(_codeValue))
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append(string.Format("Empty Category code contain in row number {0}", _rowCount));
                                    else _errorLst.Append(string.Format(" and Empty Category code contain in row number {0}", _rowCount));
                                    continue;
                                }

                                //var _result = _cat2Result.AsEnumerable().Where(r => r.Field<string>("CODE") == _codeValue).FirstOrDefault();
                                var _result = CHNLSVC.CommonSearch.GetCat_SearchData(_cat2Search.SearchParams, "CODE", _codeValue);
                                if (_result == null || _result.Rows.Count < 1)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("Invalid Category code - " + _codeValue);
                                    else _errorLst.Append(" and Invalid Category code - " + _codeValue);
                                    continue;
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(_dr[5].ToString()))
                                {
                                    if (_dr[5].ToString().Trim().Length > 0)
                                    {
                                        if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append(string.Format("Category code must be empty in row number {0}", _rowCount));
                                        else _errorLst.Append(string.Format(" and Category code must be empty in row number {0}", _rowCount));
                                        continue;
                                    }
                                }
                            }
                            #endregion

                            #region Brand
                            if (txtBrand.Enabled)
                            {
                                string _codeValue = _dr[6].ToString();

                                if (string.IsNullOrEmpty(_codeValue))
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append(string.Format("Empty Brand code contain in row number {0}", _rowCount));
                                    else _errorLst.Append(string.Format(" and Empty Brand code contain in row number {0}", _rowCount));
                                    continue;
                                }

                                //var _result = _brandResult.AsEnumerable().Where(r => r.Field<string>("CODE") == _codeValue).FirstOrDefault();
                                var _result = CHNLSVC.CommonSearch.GetItemBrands(_brandSearch.SearchParams, "CODE", _codeValue);
                                if (_result == null || _result.Rows.Count < 1)
                                {
                                    if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append("Invalid Brand code - " + _codeValue);
                                    else _errorLst.Append(" and Invalid Brand code - " + _codeValue);
                                    continue;
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(_dr[6].ToString()))
                                {
                                    if (_dr[6].ToString().Trim().Length > 0)
                                    {
                                        if (string.IsNullOrEmpty(_errorLst.ToString())) _errorLst.Append(string.Format("Brand code must be empty in row number {0}", _rowCount));
                                        else _errorLst.Append(string.Format(" and Brand code must be empty in row number {0}", _rowCount));
                                        continue;
                                    }
                                }
                            }
                            #endregion


                            foreach (DataGridViewRow _row in grvParty.Rows)
                            {
                                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)_row.Cells["dataGridViewCheckBoxColumn1"];
                                if (Convert.ToBoolean(chk.Value) == true)
                                {
                                    _isfoundParty = true;

                                    for (int _count = 0; _count < grvStatus.Rows.Count; _count++)
                                    {
                                        DataRow _newRow = _dtTempParty.NewRow();

                                        _newRow["grvChkDefinition"] = true;
                                        _newRow["ISP_PTY_TP"] = DropDownListPartyTypes.SelectedValue.ToString();
                                        _newRow["ISP_PTY_CD"] = _row.Cells["party_Code"].Value.ToString();

                                        _newRow["ISP_FRM_DT"] = dateTimePickerFrom.Value.Date.ToShortDateString();
                                        _newRow["ISP_TO_DT"] = dateTimePickerTo.Value.Date.ToShortDateString();

                                        _newRow["ISP_ITM_STUS"] = grvStatus.Rows[_count].Cells["To"].Value.ToString();
                                        _newRow["ISP_FRM_STUS"] = grvStatus.Rows[_count].Cells["From"].Value.ToString();

                                        _newRow["ISP_ALW_QTY"] = _dr[0].ToString();
                                        _newRow["ISP_ALW_ITM_QTY"] = (txtAllowItemQuantity.Enabled) ? _dr[1].ToString() : _dr[0].ToString();
                                        _newRow["ISP_ITM_CD"] = _dr.ItemArray.Length >= 3 ? (txtItemCD.Enabled) ? _dr[2].ToString() : null : null;
                                        _newRow["ISP_MODEL"] = _dr.ItemArray.Length >= 4 ? (txtModel.Enabled) ? _dr[3].ToString() : null : null;
                                        _newRow["ISP_ITM_CAT1"] = _dr.ItemArray.Length >= 5 ? (txtCate1.Enabled) ? _dr[4].ToString() : null : null;
                                        _newRow["ISP_ITM_CAT2"] = _dr.ItemArray.Length >= 6 ? (txtCate2.Enabled) ? _dr[5].ToString() : null : null;
                                        _newRow["ISP_BRAND"] = _dr.ItemArray.Length >= 7 ? (txtBrand.Enabled) ? _dr[6].ToString() : null : null;

                                        _dtTempParty.Rows.Add(_newRow);
                                    }

                                }
                            }


                            if (!_isfoundParty)
                            {
                                MessageBox.Show(string.Format("Please select {0}", DropDownListPartyTypes.Text), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }

                        }
                    }
                }
                else
                {
                    MessageBox.Show("No data found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!string.IsNullOrEmpty(_errorLst.ToString()))
                {
                    MessageBox.Show("Following discrepancies found when checking the file.\n" + _errorLst.ToString(), "Discrepancies", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Clipboard.SetText(_errorLst.ToString());
                    return;
                }


                dtDefinition = _dtTempParty;

                grvDefinition.AutoGenerateColumns = false;
                grvDefinition.DataSource = dtDefinition;


            }


            catch (Exception err)
            {
                if (err.Message.Contains("already opened"))
                {
                    MessageBox.Show("The Microsoft Office is already opened exclusively by another user, or you need permission to view and write its data.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                else
                {
                    MessageBox.Show("Error Occurred while processing...\n" + err.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void chkAllDefinition_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAllDefinition.Checked)
            {
                SelectAllDefinition(true);
            }
            else
            {
                SelectAllDefinition(false);
            }
        }

    }
}
