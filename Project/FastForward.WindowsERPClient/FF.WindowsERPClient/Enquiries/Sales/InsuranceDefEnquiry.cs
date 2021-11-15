using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Enquiries.Sales
{
    public partial class InsuranceDefEnquiry : FF.WindowsERPClient.Base
    {
        public InsuranceDefEnquiry()
        {
            InitializeComponent();
            BindPartyType();
        }
        public void BindPartyType()
        {
            Dictionary<string, string> PartyTypes = new Dictionary<string, string>();
            PartyTypes.Add("CHNL", "Channel");
            PartyTypes.Add("SCHNL", "Sub Channel");
            PartyTypes.Add("AREA", "Area");
            PartyTypes.Add("REGION", "Region");
            PartyTypes.Add("ZONE", "Zone");
            PartyTypes.Add("PC", "PC");

            DropDownListPartyTypes.DataSource = new BindingSource(PartyTypes, null);
            DropDownListPartyTypes.DisplayMember = "Value";
            DropDownListPartyTypes.ValueMember = "Key";
        }

        private void btnGetDetail_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                MessageBox.Show("Please select the item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!chkSaleTp.Checked)
                if (string.IsNullOrEmpty(Convert.ToString(cmbStatus.Text)))
                {
                    MessageBox.Show("Please select the sale type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            if (!checkBox1.Checked)
                if (string.IsNullOrEmpty(Convert.ToString(cmbTerm.Text)))
                {
                    MessageBox.Show("Please select the term", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            //kapila
            if (!chk_PB.Checked)
                if (string.IsNullOrEmpty(Convert.ToString(txtPB.Text)))
                {
                    MessageBox.Show("Please select the price book", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            if (!chk_PBLevel.Checked)
                if (string.IsNullOrEmpty(Convert.ToString(txtPBLevel.Text)))
                {
                    MessageBox.Show("Please select the price level", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            if (!chkInsCom.Checked)
                if (string.IsNullOrEmpty(Convert.ToString(txtInsCom.Text)))
                {
                    MessageBox.Show("Please select the insurance company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            if (!chkParty.Checked)
                if (string.IsNullOrEmpty(Convert.ToString(txtHierchCode.Text)))
                {
                    MessageBox.Show("Please select the heirachy", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            if (!chkPol.Checked)
                if (string.IsNullOrEmpty(Convert.ToString(txtInsPol.Text)))
                {
                    MessageBox.Show("Please select the policy", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


            dataGridView1.AutoGenerateColumns = false;
            string _error = string.Empty;
            string _saletp = string.Empty;
            //kapila
            string _PB = string.Empty;
            string _PBLevel = string.Empty;
            string _Pol = string.Empty;
            string _Party = string.Empty;
            string _Partycode = string.Empty;
            string _InsCom = string.Empty;

            Int32 _term = 0;
            if (!chkSaleTp.Checked) _saletp = Convert.ToString(cmbStatus.Text);
            if (!checkBox1.Checked) _term = Convert.ToInt32(cmbTerm.Text);

            //kapila
            if (!chk_PB.Checked) _PB = Convert.ToString(txtPB.Text);
            if (!chk_PBLevel.Checked) _PBLevel = Convert.ToString(txtPBLevel.Text);
            if (!chkInsCom.Checked) _InsCom = Convert.ToString(txtInsCom.Text);
            if (!chkPol.Checked) _Pol = Convert.ToString(txtInsPol.Text);
            if (!chkParty.Checked) _Party = DropDownListPartyTypes.SelectedValue.ToString();
            if (!chkParty.Checked) _Partycode = Convert.ToString(txtHierchCode.Text);

            DataTable _tbl = CHNLSVC.Sales.GetInsuEnquiry(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtItem.Text.Trim(), _term, txtFromDt.Value.Date, BaseCls.GlbUserID, _saletp, _InsCom, _Pol, _PB, _PBLevel, _Party, _Partycode, out _error);
            dataGridView1.DataSource = _tbl;

            if (!string.IsNullOrEmpty(_error))
            {
                MessageBox.Show(_error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (_tbl == null || _tbl.Rows.Count <= 0)
            {
                MessageBox.Show("There is no records for the selected criteria", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InsuCom:
                    {
                        paramsText.Append("INS" + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {

                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPB.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InsuPolicy:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        private void btnSearch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItem;
                _CommonSearch.ShowDialog();
                txtItem.Select();
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
        private void txtItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                btnSearch_Item_Click(null, null);
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
        private void txtItem_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text)) return;
                LoadItemDetail(txtItem.Text.Trim());
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
        MasterItem _itemdetail = new MasterItem();
        private bool LoadItemDetail(string _item)
        {
            bool _isValid = false;
            try
            {
                _itemdetail = new MasterItem();
                if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
                if (_itemdetail == null || string.IsNullOrEmpty(_itemdetail.Mi_cd))
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Please check the item code", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Clear();
                    txtItem.Focus();
                    _isValid = false;
                    return _isValid;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return _isValid;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
            return _isValid;
        }

        private void chkSaleTp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSaleTp.Checked)
                cmbStatus.Enabled = false;
            else
                cmbStatus.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                cmbTerm.Enabled = false;
            else
                cmbTerm.Enabled = true;

        }

        private void txtInsCom_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F2)
            {
                btn_srch_ins_com_Click(null, null);
            }
        }

        private void btn_srch_ins_com_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsuCom);
            DataTable _result = CHNLSVC.CommonSearch.GetInsuCompany(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtInsCom;
            _CommonSearch.ShowDialog();
            txtInsCom.Select();
        }

        private void btnHierachySearch_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btn_PB_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceBookByCompany);
            DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPB;
            _CommonSearch.ShowDialog();
            txtPB.Select();
        }

        private void btn_PBLevel_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelByBook);
            DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPBLevel;
            _CommonSearch.ShowDialog();
            txtPBLevel.Select();
        }

        private void btn_srch_pol_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsuPolicy);
            DataTable _result = CHNLSVC.CommonSearch.GetInsuPolicy(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtInsPol;
            _CommonSearch.ShowDialog();
            txtInsPol.Select();
        }

        private void chk_PB_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_PB.Checked == true)
            {
                txtPB.Text = "";
                txtPB.Enabled = false;
                btn_PB.Enabled = false;
            }
            else
            {
                txtPB.Enabled = true;
                btn_PB.Enabled = true;
            }
        }

        private void chk_PBLevel_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_PBLevel.Checked == true)
            {
                txtPBLevel.Text = "";
                txtPBLevel.Enabled = false;
                btn_PBLevel.Enabled = false;
            }
            else
            {
                txtPBLevel.Enabled = true;
                btn_PBLevel.Enabled = true;
            }
        }

        private void chkInsCom_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInsCom.Checked == true)
            {
                txtInsCom.Text = "";
                txtInsCom.Enabled = false;
                btn_srch_ins_com.Enabled = false;
            }
            else
            {
                txtInsCom.Enabled = true;
                btn_srch_ins_com.Enabled = true;
            }
        }

        private void chkPol_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPol.Checked == true)
            {
                txtInsPol.Text = "";
                txtInsPol.Enabled = false;
                btn_srch_pol.Enabled = false;
            }
            else
            {
                txtInsPol.Enabled = true;
                btn_srch_pol.Enabled = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            txtInsCom.Text = "";
            txtInsPol.Text = "";
            txtItem.Text="";
            txtPB.Text="";
            txtPBLevel.Text = "";
            txtHierchCode.Text = "";
        }

        private void chkParty_CheckedChanged(object sender, EventArgs e)
        {
            if (chkParty.Checked == true)
            {
                txtHierchCode.Text = "";
                txtHierchCode.Enabled = false;
                btnHierachySearch.Enabled = false;
            }
            else
            {
                txtHierchCode.Enabled = true;
                btnHierachySearch.Enabled = true;
            }
        }

        //        public void s()
        //        { 

        //             in_asAtDate = Format(Date, "dd/mmm/yyyy")

        //Set pc_cur = New ADODB.Recordset
        //pc_cur.Open "SELECT SVID_COM,SVID_PC,SVID_INS_COM_CD,SVID_POLC_CD,SVID_SALE_TP,SVID_ITM,SVID_TERM From SAR_VEH_INS_DEFN " & _
        //            "GROUP BY SVID_COM,SVID_PC,SVID_INS_COM_CD,SVID_POLC_CD,SVID_SALE_TP,SVID_ITM,SVID_TERM " & _
        //            "ORDER BY SVID_COM,SVID_PC,SVID_INS_COM_CD,SVID_POLC_CD,SVID_SALE_TP,SVID_ITM,SVID_TERM", adocon, adOpenStatic, adLockOptimistic
        //If pc_cur.RecordCount > 0 Then
        //    For i = 1 To pc_cur.RecordCount
        //        SVID_COM = pc_cur!SVID_COM
        //        SVID_PC = pc_cur!SVID_PC
        //        SVID_INS_COM_CD = pc_cur!SVID_INS_COM_CD
        //        SVID_POLC_CD = pc_cur!SVID_POLC_CD
        //        SVID_SALE_TP = pc_cur!SVID_SALE_TP
        //        SVID_ITM = pc_cur!SVID_ITM
        //        SVID_TERM = pc_cur!SVID_TERM

        //    Set itm_cur = New ADODB.Recordset
        //    itm_cur.Open "SELECT MBI_DESC,SVIP_POLC_DESC,SVID_FROM_DT,SVID_TO_DT,SRTP_DESC,SVID_ITM,MI_LONGDESC,MI_MODEL,SVID_VAL,SVID_IS_REQ,SVID_CRE_BY,SVID_CRE_DT " & _
        //                 "FROM SAR_VEH_INS_DEFN INNER JOIN MST_ITM ON SAR_VEH_INS_DEFN.SVID_ITM = MST_ITM.MI_CD " & _
        //                 "                      INNER JOIN SAR_TP  ON SAR_VEH_INS_DEFN.SVID_SALE_TP = SAR_TP.SRTP_CD " & _
        //                 "                      INNER JOIN MST_BUSCOM ON SAR_VEH_INS_DEFN.SVID_INS_COM_CD = MST_BUSCOM.MBI_CD " & _
        //                 "                      INNER JOIN SAR_VEH_INS_POLC ON SAR_VEH_INS_DEFN.SVID_POLC_CD = SAR_VEH_INS_POLC.SVIP_POLC_CD " & _
        //                 "WHERE MBI_TP IN ('INS') AND SVID_COM = '" & SVID_COM & "' AND SVID_PC = '" & SVID_PC & "' AND SVID_INS_COM_CD = '" & SVID_INS_COM_CD & "' " & _
        //                 "AND SVID_POLC_CD = '" & SVID_POLC_CD & "' AND SVID_SALE_TP = '" & SVID_SALE_TP & "' AND SVID_ITM = '" & SVID_ITM & "' AND SVID_TERM = " & SVID_TERM & " " & _
        //                 "AND SVID_FROM_DT <= '" & in_asAtDate & "' AND SVID_TO_DT >= '" & in_asAtDate & "' " & _
        //                 "AND SVID_SEQ IN (SELECT MAX(SVID_SEQ) FROM SAR_VEH_INS_DEFN WHERE SVID_COM = '" & SVID_COM & "' AND SVID_PC = '" & SVID_PC & "' AND SVID_INS_COM_CD = '" & SVID_INS_COM_CD & "' " & _
        //                 "AND SVID_POLC_CD = '" & SVID_POLC_CD & "' AND SVID_SALE_TP = '" & SVID_SALE_TP & "' AND SVID_ITM = '" & SVID_ITM & "' AND SVID_TERM = " & SVID_TERM & " " & _
        //                 "AND SVID_FROM_DT <= '" & in_asAtDate & "' AND SVID_TO_DT >= '" & in_asAtDate & "')", adocon, adOpenStatic, adLockOptimistic
        //    If itm_cur.RecordCount > 0 Then
        //        For j = 1 To itm_cur.RecordCount
        //             SVID_INS_COM_DESC = itm_cur!MBI_DESC
        //             SVID_POLC_DESC = itm_cur!SVIP_POLC_DESC
        //             SVID_FROM_DT = itm_cur!SVID_FROM_DT
        //             SVID_TO_DT = itm_cur!SVID_TO_DT
        //             SVID_SALE_DESC = itm_cur!SRTP_DESC
        //             SVID_MODLE = itm_cur!MI_MODEL
        //             SVID_ITM_DESC = itm_cur!MI_LONGDESC
        //             If itm_cur!SVID_IS_REQ = 1 Then
        //                SVID_IS_REQ = "COMPALSORY"
        //             Else
        //                SVID_IS_REQ = ""
        //             End If
        //             SVID_CRE_BY = itm_cur!SVID_CRE_BY
        //             SVID_CRE_DT = itm_cur!SVID_CRE_DT
        //             SVID_VAL = itm_cur!SVID_VAL

        //             adocon.Execute "INSERT INTO hpt_insu_def (SVID_COM,SVID_PC,SVID_INS_COM_CD  ,SVID_POLC_CD  ,SVID_FROM_DT,SVID_TO_DT,SVID_SALE_TP  ,SVID_TERM,SVID_ITM,SVID_MODLE,SVID_ITM_DESC,SVID_VAL,SVID_IS_REQ,SVID_CRE_BY,SVID_CRE_DT) " & _
        //                            "VALUES                   ('" & SVID_COM & "','" & SVID_PC & "','" & SVID_INS_COM_DESC & "','" & SVID_POLC_DESC & "','" & Format(SVID_FROM_DT, "DD/MMM/YYYY") & "','" & Format(SVID_TO_DT, "DD/MMM/YYYY") & "','" & SVID_SALE_DESC & "'," & SVID_TERM & ",'" & SVID_ITM & "','" & SVID_MODLE & "','" & SVID_ITM_DESC & "'," & SVID_VAL & ",'" & SVID_IS_REQ & "','" & SVID_CRE_BY & "','" & Format(SVID_CRE_DT, "DD/MMM/YYYY") & "')"

        //        Debug.Print i & " of " & pc_cur.RecordCount
        //        DoEvents
        //        itm_cur.MoveNext
        //        Next j
        //    End If
        //    Set itm_cur = Nothing
        //    pc_cur.MoveNext
        //    Next i
        //End If
        //Set pc_cur = Nothing

        //        }


        //DataTable dtPeople = new DataTable("People");
        //dtPeople.Columns.Add("PeopleID", typeof(int));
        //dtPeople.Columns.Add("RoleID", typeof(int));
        //dtPeople.Rows.Add(1, 2);
        //dtPeople.Rows.Add(2, 4);
        //dtPeople.Rows.Add(3, 5);
        //dtPeople.Rows.Add(4, 6);
        //dtPeople.Rows.Add(5, 1);

        //DataTable dtRole = new DataTable("Role");
        //dtRole.Columns.Add("PeopleID", typeof(int));
        //dtRole.Columns.Add("RoleName");
        //dtRole.Rows.Add(1, "Role 1");
        //dtRole.Rows.Add(2, "Role 2");
        //dtRole.Rows.Add(3, "Role 3");
        //dtRole.Rows.Add(4, "Role 4");
        //dtRole.Rows.Add(5, "Role 5");
        //dtRole.Rows.Add(6, "Role 6");

        //DataTable dtSource = new DataTable();
        //for (int j = 0; j < dtRole.Rows.Count; j++)
        //{
        //    dtSource.Columns.Add(dtRole.Rows[j]["RoleName"].ToString(), typeof(bool));
        //}
        //for (int j = 0; j < dtPeople.Rows.Count; j++)
        //{
        //    DataRow dr = dtSource.NewRow();
        //    int RoleID = (int)dtPeople.Rows[j]["RoleID"];
        //    dr[RoleID - 1] = true;
        //    dtSource.Rows.Add(dr);
        //}

        //this.dataGridView1.DataSource = dtSource;
        //this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

    }
}
