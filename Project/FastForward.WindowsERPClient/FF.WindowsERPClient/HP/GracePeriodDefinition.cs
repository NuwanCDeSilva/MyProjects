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

namespace FF.WindowsERPClient.HP
{
    public partial class GracePeriodDefinition : Base
    {

        #region properties

        List<ArrearsDateDef> ArrDateDefList;
        List<ArrearsDateDef> Show_ArrDateDefList;


        #endregion

        public GracePeriodDefinition()
        {
            InitializeComponent();
            ArrDateDefList = new List<ArrearsDateDef>();
            Show_ArrDateDefList = new List<ArrearsDateDef>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                this.Close();
            }
        }

        private void GracePeriodDefinition_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewDetails.AutoGenerateColumns = false;

                dateTimePickerMonthYear.Format = DateTimePickerFormat.Custom;
                dateTimePickerMonthYear.CustomFormat = "MM/yyyy";

                dateTimePickerMonthYear_ValueChanged(null, null);
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

        private void dateTimePickerMonthYear_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime monthDate;
                try
                {
                    monthDate = dateTimePickerMonthYear.Value;
                }
                catch (Exception)
                {
                    dateTimePickerMonthYear.Value = DateTime.Now;
                    return;
                }
                ArrDateDefList = new List<ArrearsDateDef>();

                Show_ArrDateDefList = getOldGraceDef(monthDate);
                if (Show_ArrDateDefList == null)
                {
                    Show_ArrDateDefList = new List<ArrearsDateDef>();
                }

                var source = new BindingSource();
                source.DataSource = Show_ArrDateDefList;
                dataGridViewDetails.DataSource = source;

                DateTime lastDayOFMonth = new DateTime(dateTimePickerMonthYear.Value.Year, dateTimePickerMonthYear.Value.Month, 1).AddMonths(1).AddDays(-1);
                txtAsAtDate.Text = lastDayOFMonth.ToShortDateString();
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

        private List<ArrearsDateDef> getOldGraceDef(DateTime arrDate)
        {
            try
            {
                DateTime lastDayOFMonth = new DateTime(dateTimePickerMonthYear.Value.Year, dateTimePickerMonthYear.Value.Month, 1).AddMonths(1).AddDays(-1);
                List<ArrearsDateDef> _defHeaderList = new List<ArrearsDateDef>();
                _defHeaderList = CHNLSVC.Financial.Get_ArrearsDateDef(BaseCls.GlbUserComCode, lastDayOFMonth, string.Empty, string.Empty);

                return _defHeaderList;
            }
            catch (Exception ex) {
                MessageBox.Show("Error occured while processing!!\n"+ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return null;
            }
        }

        private void buttonSearchCompany_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCompany;
                _CommonSearch.ShowDialog();
                txtCompany.Select();
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



        private void buttonSearchChannel_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtChannel;
                _CommonSearch.ShowDialog();
                txtChannel.Select();
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

        private void buttonSearchSubChannel_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSubChannel;
                _CommonSearch.ShowDialog();
                txtSubChannel.Select();
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

        private void buttonSearchArea_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtArea;
                _CommonSearch.ShowDialog();
                txtArea.Select();
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

        private void buttonSearchRegion_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtRegion;
                _CommonSearch.ShowDialog();
                txtRegion.Select();
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

        private void buttonSearchZone_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtZone;
                _CommonSearch.ShowDialog();
                txtZone.Select();
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

        private void buttonSearchPC_Click(object sender, EventArgs e)
        {
            try
            {
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
                        paramsText.Append(txtCompany.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(txtCompany.Text + seperator + txtChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(txtCompany.Text + seperator + txtChannel.Text + seperator + txtSubChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(txtCompany.Text + seperator + txtChannel.Text + seperator + txtSubChannel.Text + seperator + txtArea.Text + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(txtCompany.Text + seperator + txtChannel.Text + seperator + txtSubChannel.Text + seperator + txtArea.Text + seperator + txtRegion.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(txtCompany.Text + seperator + txtChannel.Text + seperator + txtSubChannel.Text + seperator + txtArea.Text + seperator + txtRegion.Text + seperator + txtZone.Text + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime monthDate = dateTimePickerMonthYear.Value;
                DateTime suppDt = dateTimePickerSuppDate.Value;
                DateTime graceDt = dateTimePickerGraceDate.Value;

                DateTime lastDayOFMonth = new DateTime(dateTimePickerMonthYear.Value.Year,dateTimePickerMonthYear.Value.Month,1).AddMonths(1).AddDays(-1);
                if (suppDt.Date < lastDayOFMonth.Date)
                {
                   MessageBox.Show("Supplementary date should be grater than or equal As at Date!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                if (graceDt.Date < suppDt.Date)
                {
                    MessageBox.Show("Grace date should be grater than or equal Supplementary Date!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }

                string partyTp = string.Empty;
                string partyCd = string.Empty;
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                DataTable _result = new DataTable();
                if (rdoArea.Checked)
                {
                    partyTp = "AREA";
                    partyCd = txtArea.Text;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                    _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtArea.Text);

                }
                if (rdoChannel.Checked)
                {
                    partyTp = "CHNL";
                    partyCd = txtChannel.Text;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                    _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtChannel.Text);
                }
                if (rdoCompany.Checked)
                {
                    partyTp = "COM";
                    partyCd = txtCompany.Text;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtCompany.Text);
                }
                if (rdoProfitCenter.Checked)
                {
                    partyTp = "PC";
                    partyCd = txtPC.Text;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
                    _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtCompany.Text);
                }
                if (rdoRegion.Checked)
                {
                    partyTp = "REG";
                    partyCd = txtRegion.Text;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                    _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtRegion.Text);
                }
                if (rdoSubChannel.Checked)
                {
                    partyTp = "SCHNL";
                    partyCd = txtSubChannel.Text;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                    _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtSubChannel.Text);
                }
                if (rdoZone.Checked)
                {
                    partyTp = "ZONE";
                    partyCd = txtZone.Text;
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                    _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, "CODE", txtZone.Text);
                }
                if (partyTp == string.Empty)
                {
                    MessageBox.Show("Select Party Type!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                if (partyCd == string.Empty)
                {
                    MessageBox.Show("Select Party!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                if (_result.Rows.Count <= 0) {
                    if (partyTp == "REG")
                    {
                        MessageBox.Show(partyCd + " is invalid REGION code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else {
                        MessageBox.Show(partyCd + " is invalid " + partyTp + " code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                ArrearsDateDef ArrDef = new ArrearsDateDef();
                ArrDef.Hadd_ars_dt = lastDayOFMonth;
                ArrDef.Hadd_cre_by = BaseCls.GlbUserID;
                ArrDef.Hadd_cre_dt = _date;
                ArrDef.Hadd_grc_dt = graceDt.Date;
                ArrDef.Hadd_pty_cd = partyCd;
                ArrDef.Hadd_pty_tp = partyTp;
                ArrDef.Hadd_sup_dt = suppDt.Date;

                var _duplicate = from _dup in Show_ArrDateDefList
                                 where _dup.Hadd_pty_cd == partyCd && _dup.Hadd_pty_tp == partyTp
                                 select _dup;
                if (_duplicate.Count() > 0)
                {
                    MessageBox.Show("Definition already added!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
 
                    return;
                }
                else
                {
                    ArrDateDefList.Add(ArrDef);
                    Show_ArrDateDefList.Add(ArrDef);

                    var source = new BindingSource();
                    source.DataSource = Show_ArrDateDefList;
                    dataGridViewDetails.DataSource = source;
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Error occurred while processing!!\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //CHNLSVC.CloseChannel(); 
                return;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
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

        private void Save()
        {
            try
            {
                if (dataGridViewDetails.Rows.Count <= 0) {
                    MessageBox.Show("Please add records before saving", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                Int32 eff = CHNLSVC.Financial.Save_hpr_ars_dt_defn(ArrDateDefList);
                if (eff > 0)
                {
                    MessageBox.Show("Successfully Saved!","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    ClearAll();
                }
                else
                {
                    MessageBox.Show("Error Occured. Failed to save!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while processing!!\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
                return;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
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

        private void ClearAll()
        {
          //datetime picker
            dateTimePickerGraceDate.Value = DateTime.Now;
            dateTimePickerMonthYear.Value = DateTime.Now;
            dateTimePickerSuppDate.Value = DateTime.Now;

            //textbox
            txtArea.Text = "";
            txtAsAtDate.Text = "";
            txtChannel.Text = "";
            txtCompany.Text = "";
            txtPC.Text = "";
            txtRegion.Text = "";
            txtSubChannel.Text = "";
            txtZone.Text = "";

            //radio buttons
            rdoCompany.Checked = true;
            rdoArea.Checked = false;
            rdoChannel.Checked = false;
            rdoProfitCenter.Checked = false;
            rdoRegion.Checked = false;
            rdoSubChannel.Checked = false;
            rdoZone.Checked = false;

            dateTimePickerMonthYear_ValueChanged(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateTimePickerMonthYear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                dateTimePickerSuppDate.Focus();
            }
        }

        private void dateTimePickerSuppDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                dateTimePickerGraceDate.Focus();
            }
        }

        private void dateTimePickerGraceDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                rdoCompany.Focus();
            }
        }

        private void rdoProfitCenter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                txtCompany.Focus();
            }
        }

        private void txtCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtChannel.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                buttonSearchCompany_Click(null, null);
            }
        }

        private void txtChannel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSubChannel.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                buttonSearchChannel_Click(null, null);
            }
        }

        private void txtSubChannel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtArea.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                buttonSearchSubChannel_Click(null, null);
            }
        }

        private void txtArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRegion.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                buttonSearchArea_Click(null, null);
            }
        }

        private void txtRegion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtZone.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                buttonSearchRegion_Click(null, null);
            }
        }

        private void txtZone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPC.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                buttonSearchZone_Click(null, null);
            }
        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonAdd.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                buttonSearchPC_Click(null, null);
            }
        }

        private void dataGridViewDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1 && e.ColumnIndex == 0)
                {
                    if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Int32 rowIndex = e.RowIndex;
                        string parytTp = dataGridViewDetails.Rows[rowIndex].Cells[1].Value.ToString();
                        string parytCd = dataGridViewDetails.Rows[rowIndex].Cells[2].Value.ToString();


                        var _duplicate = from _dup in ArrDateDefList
                                         where _dup.Hadd_pty_tp == parytTp && _dup.Hadd_pty_cd == parytCd
                                         select _dup;
                        if (_duplicate.Count() > 0)
                        {
                            Show_ArrDateDefList.RemoveAt(rowIndex);
                            ArrDateDefList.RemoveAll(x => x.Hadd_pty_tp == parytTp && x.Hadd_pty_cd == parytCd);
                        }
                        else
                        {
                            MessageBox.Show("Cannot delete records already saved in the database!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        var source = new BindingSource();
                        source.DataSource = Show_ArrDateDefList;
                        dataGridViewDetails.DataSource = source;
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

        private void txtCompany_Leave(object sender, EventArgs e)
        {
            try
            {
                lblCompany.Text = GetPC_HIRC_SearchDesc(69, txtCompany.Text.ToUpper());
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

        private void txtChannel_Leave(object sender, EventArgs e)
        {
            try
            {
                lblChannel.Text = GetPC_HIRC_SearchDesc(70, txtChannel.Text.ToUpper());
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

        private void txtSubChannel_Leave(object sender, EventArgs e)
        {
            try
            {
                lblSubChannel.Text = GetPC_HIRC_SearchDesc(71, txtSubChannel.Text.ToUpper());
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

        private void txtArea_Leave(object sender, EventArgs e)
        {
            try
            {
                lblArea.Text = GetPC_HIRC_SearchDesc(72, txtArea.Text.ToUpper());
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

        private void txtRegion_Leave(object sender, EventArgs e)
        {
            try
            {
                lblRegion.Text = GetPC_HIRC_SearchDesc(73, txtRegion.Text.ToUpper());
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

        private void txtZone_Leave(object sender, EventArgs e)
        {
            try
            {
                lblZone.Text = GetPC_HIRC_SearchDesc(74, txtZone.Text.ToUpper());
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
                lblProfitCenter.Text = GetPC_HIRC_SearchDesc(75, txtPC.Text.ToUpper());
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

        public string GetPC_HIRC_SearchDesc(int i, string _code)
        {
            if (i > 75 || i < 69)
            {
                return null;
            }
            ChannelOperator chnlOpt = new ChannelOperator();
            CommonUIDefiniton.SearchUserControlType _type = (CommonUIDefiniton.SearchUserControlType)i;

            Base _basePage = new Base();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            _basePage = new Base();
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                default:
                    break;
            }


            DataTable dt = chnlOpt.CommonSearch.Get_PC_HIRC_SearchData(paramsText.ToString(), "CODE", _code);
            if (dt == null)
            {
                return null;
            }
            if (dt.Rows.Count <= 0 || dt == null || dt.Rows.Count > 1)
                return null;
            else
                return dt.Rows[0][1].ToString();
        }

        private void txtCompany_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonSearchCompany_Click(null, null);
        }

        private void txtChannel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonSearchChannel_Click(null, null);
        }

        private void txtSubChannel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonSearchSubChannel_Click(null, null);
        }

        private void txtArea_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonSearchArea_Click(null, null);
        }

        private void txtRegion_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonSearchRegion_Click(null, null);
        }

        private void txtZone_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonSearchZone_Click(null, null);
        }

        private void txtPC_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            buttonSearchPC_Click(null, null);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10106))
            {
                MessageBox.Show("Sorry, You have no permission to edit the definition!\n( Advice: Required permission code :10106)");
                return;
            }

            if (MessageBox.Show("Are you sure?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            List<ArrearsDateDef>  _arrDefList = new List<ArrearsDateDef>();
            foreach (DataGridViewRow dr in dataGridViewDetails.Rows)
            {
                ArrearsDateDef _objAr = new ArrearsDateDef();
                _objAr.Hadd_sup_dt = Convert.ToDateTime(dr.Cells["Hadd_sup_dt"].Value);
                _objAr.Hadd_grc_dt = Convert.ToDateTime(dr.Cells["Hadd_grc_dt"].Value);
                _objAr.Hadd_seq = Convert.ToInt32(dr.Cells["Hadd_seq"].Value);
                _objAr.Hadd_mod_by = BaseCls.GlbUserID;
                _arrDefList.Add(_objAr);

            }

            Int32 eff = CHNLSVC.Financial.Update_hpr_ars_dt_defn(_arrDefList);
            if (eff > 0)
            {
                MessageBox.Show("Successfully Updated!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAll();
            }
            else
            {
                MessageBox.Show("Error Occured. Failed to update!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


    }
}
