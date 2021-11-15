using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FF.WindowsERPClient.UserControls
{
    //Ceated- Shani 11-02-2013

    public partial class ucProfitCenterSearch : UserControl
    {
        Base _basePage;

        ///  Edit History
        ///  ------------
        ///  Sequence          Date                  Name                    Code
        ///  1                 18/03/2013            Prabhath                ucPCSE001
        ///  2                 21/03/2013            Prabhath                ucPCSE002
        ///  3                 02/05/2013            Prabhath                ucPCSE003


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Company
        {
            get { return TextBoxCompany.Text; }
            set { TextBoxCompany.Text = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CompanyDes
        {
            get { return TextBoxCompanyDes.Text; }
            set { TextBoxCompanyDes.Text = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Channel
        {
            get { return TextBoxChannel.Text; }
            set { TextBoxChannel.Text = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SubChannel
        {
            get { return TextBoxSubChannel.Text; }
            set { TextBoxSubChannel.Text = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Area
        {
            get { return TextBoxArea.Text; }
            set { TextBoxArea.Text = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Regien
        {
            get { return TextBoxRegion.Text; }
            set { TextBoxRegion.Text = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Zone
        {
            get { return TextBoxZone.Text; }
            set { TextBoxZone.Text = value; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ProfitCenter
        {
            get { return TextBoxLocation.Text; }
            set { TextBoxLocation.Text = value; }
        }

        //ucPCSE001
        #region DisplayAllPc
        private bool _isAllProfitCenter = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAllProfitCenter
        {
            set { _isAllProfitCenter = value; }
        }
        public event EventHandler TextBoxLostFocus;
        private void ucProfitCenterSearch_TextBoxLostFocus(object sender, EventArgs e)
        {

        }
        #endregion

        //ucPCSE002
        #region Search Raw Data - Without Refering Hierarchy
        private bool _isDisplayRawData = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDisplayRawData
        {
            get { return _isDisplayRawData; }
            set { _isDisplayRawData = value; }
        }


        #endregion

        public ucProfitCenterSearch()
        {
            InitializeComponent();
            TextBoxLostFocus += new EventHandler(ucProfitCenterSearch_TextBoxLostFocus);      //ucPCSE001

        }

        public void ChangeCompany(Boolean IsAllowed)
        {
            if (IsAllowed == true)
            {
                TextBoxCompany.Enabled = true;
                ImgBtnCom.Enabled = true;
            }
            else
            {
                TextBoxCompany.Enabled = false;
                ImgBtnCom.Enabled = false;
            }
        }

        public void ChangeCompanyReadonly(Boolean IsAllowed)
        {
            if (IsAllowed == true)
            {
                TextBoxCompany.ReadOnly = false;
                ImgBtnCom.Enabled = true;
            }
            else
            {
                TextBoxCompany.ReadOnly = true;
                ImgBtnCom.Enabled = false;
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
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
                        paramsText.Append(TextBoxCompany.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        //ucPCSE002
                        if (_isDisplayRawData == false)
                            paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        else
                            paramsText.Append(TextBoxCompany.Text + seperator + string.Empty + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        //ucPCSE002
                        if (_isDisplayRawData == false)
                            paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + TextBoxSubChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        else
                            paramsText.Append(TextBoxCompany.Text + seperator + string.Empty + seperator + string.Empty + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        //ucPCSE002
                        if (_isDisplayRawData == false)
                            paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + TextBoxSubChannel.Text + seperator + TextBoxArea.Text + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        else
                            paramsText.Append(TextBoxCompany.Text + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        //ucPCSE002
                        if (_isDisplayRawData == false)
                            paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + TextBoxSubChannel.Text + seperator + TextBoxArea.Text + seperator + TextBoxRegion.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        else
                            paramsText.Append(TextBoxCompany.Text + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + TextBoxSubChannel.Text + seperator + TextBoxArea.Text + seperator + TextBoxRegion.Text + seperator + TextBoxZone.Text + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(TextBoxCompany.Text.Trim() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        private void ImgBtnCom_Click(object sender, EventArgs e)
        {
            if (TextBoxCompany.Text.Trim() == "")
            {
                MessageBox.Show("Enter Company Code");
                return;
            }
            _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company);
            DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = TextBoxCompany;
            _CommonSearch.ShowDialog();

            TextBoxCompany.Focus();
        }

        private void imgChaSearch_Click(object sender, EventArgs e)
        {
            if (TextBoxCompany.Text.Trim() == "")
            {
                MessageBox.Show("Enter Company Code");
                return;
            }
            _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = null;
            if (IsDisplayRawData)
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                _CommonSearch.IsRawData = true;
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(_CommonSearch.SearchParams, null, null);
            }
            else
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
                _CommonSearch.IsRawData = false;
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }

            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = TextBoxChannel;
            _CommonSearch.ShowDialog();

            TextBoxChannel.Focus();
        }

        private void imgSubChaSearch_Click(object sender, EventArgs e)
        {
            if (TextBoxCompany.Text.Trim() == "")
            {
                MessageBox.Show("Enter Company Code");
                return;
            }
            _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = null;
            if (IsDisplayRawData)
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                _CommonSearch.IsRawData = true;
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(_CommonSearch.SearchParams, null, null);
            }
            else
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
                _CommonSearch.IsRawData = false;
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = TextBoxSubChannel;
            _CommonSearch.ShowDialog();

            TextBoxSubChannel.Focus();
        }

        private void imgAreaSearch_Click(object sender, EventArgs e)
        {
            if (TextBoxCompany.Text.Trim() == "")
            {
                MessageBox.Show("Enter Company Code");
                return;
            }
            _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = null;
            if (IsDisplayRawData)
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                _CommonSearch.IsRawData = true;
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(_CommonSearch.SearchParams, null, null);
            }
            else
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
                _CommonSearch.IsRawData = false;
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = TextBoxArea;
            _CommonSearch.ShowDialog();

            TextBoxArea.Focus();

        }

        private void imgRegionSearch_Click(object sender, EventArgs e)
        {
            if (TextBoxCompany.Text.Trim() == "")
            {
                MessageBox.Show("Enter Company Code");
                return;
            }
            _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = null;
            if (IsDisplayRawData)
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                _CommonSearch.IsRawData = true;
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(_CommonSearch.SearchParams, null, null);
            }
            else
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
                _CommonSearch.IsRawData = false;
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = TextBoxRegion;
            _CommonSearch.ShowDialog();

            TextBoxRegion.Focus();

        }

        private void imgZoneSearch_Click(object sender, EventArgs e)
        {
            if (TextBoxCompany.Text.Trim() == "")
            {
                MessageBox.Show("Enter Company Code");
                return;
            }
            _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            DataTable _result = null;
            if (IsDisplayRawData)
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                _CommonSearch.IsRawData = true;
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(_CommonSearch.SearchParams, null, null);
            }
            else
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
                _CommonSearch.IsRawData = false;
                _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            }
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = TextBoxZone;
            _CommonSearch.ShowDialog();

            TextBoxZone.Focus();
        }

        private void imgProCeSearch_Click(object sender, EventArgs e)
        {
            if (TextBoxCompany.Text.Trim() == "")
            {
                MessageBox.Show("Enter Company Code");
                return;
            }
            _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            if (_isAllProfitCenter == false)
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
                DataTable _result = _basePage.CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
            }
            else
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable _result = _basePage.CHNLSVC.CommonSearch.GetPC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
            }
            _CommonSearch.obj_TragetTextBox = TextBoxLocation;
            _CommonSearch.ShowDialog();
            TextBoxLocation.Focus();
        }

        public void TextBoxCompany_Leave(object sender, EventArgs e)
        {
            if (TextBoxCompany.Text.Trim() == "")
            {
                MessageBox.Show("Enter Company Code");
                return;
            }
            if (string.IsNullOrEmpty(TextBoxCompany.Text)) { TextBoxCompanyDes.Clear(); return; }
            _basePage = new Base();

            if (!_basePage.CHNLSVC.General.CheckCompany(TextBoxCompany.Text.Trim())) { MessageBox.Show("Please check the company.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); TextBoxCompany.Clear(); TextBoxCompanyDes.Clear(); return; }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            TextBoxCompanyDes.Text = _basePage.FormulateDisplayText(_CommonSearch.Get_pc_HIRC_SearchDesc(69, TextBoxCompany.Text.ToUpper()));
            TextBoxLostFocus(sender, e);           //ucPCSE001

        }

        private void TextBoxChannel_Leave(object sender, EventArgs e)
        {
            if (TextBoxCompany.Text.Trim() == "")
            {
                MessageBox.Show("Enter Company Code");
                return;
            }
            if (string.IsNullOrEmpty(TextBoxChannel.Text)) { TextBoxChannelDes.Clear(); return; }
            _basePage = new Base();
            if (!_basePage.CHNLSVC.General.CheckChannel(TextBoxCompany.Text.Trim(), TextBoxChannel.Text.Trim().ToUpper())) { MessageBox.Show("Please check the channel.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); TextBoxChannel.Clear(); TextBoxChannelDes.Clear(); return; }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            TextBoxChannelDes.Text = _basePage.FormulateDisplayText(_CommonSearch.Get_pc_HIRC_SearchDesc(70, TextBoxChannel.Text.ToUpper()));
            TextBoxLostFocus(sender, e);          //ucPCSE001
        }

        private void TextBoxSubChannel_Leave(object sender, EventArgs e)
        {
            if (TextBoxCompany.Text.Trim() == "")
            {
                MessageBox.Show("Enter Company Code");
                return;
            }
            if (string.IsNullOrEmpty(TextBoxSubChannel.Text)) { TextBoxSubChannelDes.Clear(); return; }
            _basePage = new Base();
            if (!_basePage.CHNLSVC.General.CheckSubChannel(TextBoxCompany.Text.Trim(), TextBoxSubChannel.Text.Trim().ToUpper())) { MessageBox.Show("Please check the sub channel.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); TextBoxSubChannel.Clear(); TextBoxSubChannelDes.Clear(); return; }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            TextBoxSubChannelDes.Text = _basePage.FormulateDisplayText(_CommonSearch.Get_pc_HIRC_SearchDesc(71, TextBoxSubChannel.Text.ToUpper()));
            TextBoxLostFocus(sender, e);          //ucPCSE001
        }

        private void TextBoxArea_Leave(object sender, EventArgs e)
        {
            if (TextBoxCompany.Text.Trim() == "")
            {
                MessageBox.Show("Enter Company Code");
                return;
            }
            if (string.IsNullOrEmpty(TextBoxArea.Text)) { TextBoxAreaDes.Clear(); return; }
            _basePage = new Base();
            //Commented By Prabhath on 02/05/2013 - ucPCSE003
            //CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            //TextBoxAreaDes.Text = _basePage.FormulateDisplayText(_CommonSearch.Get_pc_HIRC_SearchDesc(72, TextBoxArea.Text.ToUpper()));
            if (!_basePage.CHNLSVC.General.CheckArea(TextBoxCompany.Text.Trim(), TextBoxArea.Text.Trim().ToUpper())) { MessageBox.Show("Please check the area.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); TextBoxArea.Clear(); TextBoxAreaDes.Clear(); return; }

            //ucPCSE003
            DataTable _area = _basePage.CHNLSVC.General.GetArea(TextBoxCompany.Text.Trim(), TextBoxArea.Text.Trim().ToUpper());
            if (_area != null) if (_area.Rows.Count > 0) TextBoxAreaDes.Text = _basePage.FormulateDisplayText(Convert.ToString(_area.Rows[0].Field<string>("msar_desc")));

            TextBoxLostFocus(sender, e);          //ucPCSE001
        }

        private void TextBoxRegion_Leave(object sender, EventArgs e)
        {
            if (TextBoxCompany.Text.Trim() == "")
            {
                MessageBox.Show("Enter Company Code");
                return;
            }
            if (string.IsNullOrEmpty(TextBoxRegion.Text)) { TextBoxRegionDes.Clear(); return; }
            _basePage = new Base();
            if (!_basePage.CHNLSVC.General.CheckRegion(TextBoxCompany.Text.Trim(), TextBoxRegion.Text.Trim().ToUpper())) { MessageBox.Show("Please check the region.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); TextBoxRegion.Clear(); TextBoxRegionDes.Clear(); return; }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            TextBoxRegionDes.Text = _basePage.FormulateDisplayText(_CommonSearch.Get_pc_HIRC_SearchDesc(73, TextBoxRegion.Text.ToUpper()));
            TextBoxLostFocus(sender, e);          //ucPCSE001
        }

        private void TextBoxZone_Leave(object sender, EventArgs e)
        {
            if (TextBoxCompany.Text.Trim() == "")
            {
                MessageBox.Show("Enter Company Code");
                return;
            }
            if (string.IsNullOrEmpty(TextBoxZone.Text)) { TextBoxZoneDes.Clear(); return; }
            _basePage = new Base();
            if (!_basePage.CHNLSVC.General.CheckZone(TextBoxCompany.Text.Trim(), TextBoxZone.Text.Trim().ToUpper())) { MessageBox.Show("Please check the zone.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); TextBoxZone.Clear(); TextBoxZoneDes.Clear(); return; }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            TextBoxZoneDes.Text = _basePage.FormulateDisplayText(_CommonSearch.Get_pc_HIRC_SearchDesc(74, TextBoxZone.Text.ToUpper()));
            TextBoxLostFocus(sender, e);          //ucPCSE001
        }

        private void TextBoxLocation_Leave(object sender, EventArgs e)
        {
            if (TextBoxCompany.Text.Trim() == "")
            {
                MessageBox.Show("Enter Company Code");
                return;
            }
            if (string.IsNullOrEmpty(TextBoxLocation.Text)) { TextBoxLocationDes.Clear(); return; }
            _basePage = new Base();

            if (_isAllProfitCenter == false)
            {
                if (!_basePage.CHNLSVC.General.CheckProfitCenter(TextBoxCompany.Text.Trim(), TextBoxLocation.Text.Trim().ToUpper())) { MessageBox.Show("Please check the profit center.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); TextBoxLocation.Clear(); TextBoxLocationDes.Clear(); return; }
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                TextBoxLocationDes.Text = _basePage.FormulateDisplayText(_CommonSearch.Get_pc_HIRC_SearchDesc(75, TextBoxLocation.Text.ToUpper()));
                //ucPCSE001
            }
            else
            {
                //Load description of the PC - ucPCSE001
                if (string.IsNullOrEmpty(TextBoxLocation.Text.Trim())) { TextBoxLocationDes.Text = string.Empty; return; }
                if (!_basePage.CHNLSVC.General.CheckProfitCenter(TextBoxCompany.Text.Trim(), TextBoxLocation.Text.Trim().ToUpper())) { MessageBox.Show("Please check the profit center.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); TextBoxLocation.Clear(); return; }
                DataTable _tb = _basePage.CHNLSVC.Sales.GetProfitCenterTable(TextBoxCompany.Text.Trim(), TextBoxLocation.Text.Trim());
                if (_tb.Rows.Count > 0)
                    TextBoxLocationDes.Text = _basePage.FormulateDisplayText(_tb.Rows[0].Field<string>("mpc_desc").ToString());
                else
                    TextBoxLocationDes.Text = string.Empty;
            }
            TextBoxLostFocus(sender, e);          //ucPCSE001
        }

        private void TextBoxCompany_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxChannel.Focus();
            }
        }

        private void TextBoxChannel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxSubChannel.Focus();
            }
        }

        private void TextBoxSubChannel_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxArea.Focus();
            }
        }

        private void TextBoxArea_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxRegion.Focus();
            }
        }

        private void TextBoxRegion_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxZone.Focus();
            }
        }

        private void TextBoxZone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxLocation.Focus();
            }
        }

        private void TextBoxLocation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxCompany.Focus();
            }
        }

        private void TextBoxCompany_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ImgBtnCom_Click(null, null);
        }

        private void TextBoxChannel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //imgChaSearch_Click
            this.imgChaSearch_Click(null, null);
        }

        private void TextBoxSubChannel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //imgSubChaSearch_Click
            this.imgSubChaSearch_Click(null, null);
        }

        private void TextBoxArea_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //imgAreaSearch_Click
            this.imgAreaSearch_Click(null, null);
        }

        private void TextBoxRegion_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //imgRegionSearch_Click
            this.imgRegionSearch_Click(null, null);
        }

        private void TextBoxZone_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //imgZoneSearch_Click
            this.imgZoneSearch_Click(null, null);
        }

        private void TextBoxLocation_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //imgProCeSearch_Click
            this.imgProCeSearch_Click(null, null);
        }

        private void TextBoxCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (TextBoxCompany.ReadOnly == false)
                    this.ImgBtnCom_Click(null, null);
            }
        }

        private void TextBoxChannel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.imgChaSearch_Click(null, null);
            }
        }

        private void TextBoxSubChannel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.imgSubChaSearch_Click(null, null);
            }
        }

        private void TextBoxArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.imgAreaSearch_Click(null, null);
            }
        }

        private void TextBoxRegion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.imgRegionSearch_Click(null, null);
            }
        }

        private void TextBoxZone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.imgZoneSearch_Click(null, null);
            }
        }

        private void TextBoxLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.imgProCeSearch_Click(null, null);
            }
        }
        //ucPCSE003
        public void ClearScreen(string _company)
        {
            TextBoxArea.Clear();
            TextBoxAreaDes.Clear();

            TextBoxChannel.Clear();
            TextBoxChannelDes.Clear();

            TextBoxLocation.Clear();
            TextBoxLocationDes.Clear();

            TextBoxRegion.Clear();
            TextBoxRegionDes.Clear();

            TextBoxSubChannel.Clear();
            TextBoxSubChannelDes.Clear();

            TextBoxZone.Clear();
            TextBoxZoneDes.Clear();

            TextBoxCompany.Clear();
            TextBoxCompanyDes.Clear();
            TextBoxCompany.Text = _company;
            TextBoxCompanyDes.Focus();
        }
    }
}
