using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.Interfaces;

namespace FF.WindowsERPClient.UserControls
{
    /// <summary>
    /// created by Shani  on 28-01-2013
    /// </summary>
    public partial class ucLoactionSearch : UserControl
    {

        ///  Edit History
        ///  ------------
        ///  Sequence          Date                  Name                    Code
        ///  1                 09/04/2013            Prabhath                ucLCSE001


        Base _basePage;
        public event EventHandler ItemAdded;

        #region properties

        public string Company
        {
            get { return TextBoxCompany.Text; }
            set { TextBoxCompany.Text = value; }
        }

        public string CompanyDes
        {
            get { return TextBoxCompanyDes.Text; }
            set { TextBoxCompanyDes.Text = value; }
        }

        public string Channel
        {
            get { return TextBoxChannel.Text; }
            set { TextBoxChannel.Text = value; }
        }

        public string SubChannel
        {
            get { return TextBoxSubChannel.Text; }
            set { TextBoxSubChannel.Text = value; }
        }

        public string Area
        {
            get { return TextBoxArea.Text; }
            set { TextBoxArea.Text = value; }
        }

        public string Regien
        {
            get { return TextBoxRegion.Text; }
            set { TextBoxRegion.Text = value; }
        }

        public string Zone
        {
            get { return TextBoxZone.Text; }
            set { TextBoxZone.Text = value; }
        }

        public string ProfitCenter
        {
            get { return TextBoxLocation.Text; }
            set { TextBoxLocation.Text = value; }
        }

        public void ChangeCompany(Boolean IsAllowed)
        {
            if (IsAllowed == true)
            {
                TextBoxCompany.Enabled = true;
                ImgBtnAccountNo.Enabled = true;
            }
            else
            {
                TextBoxCompany.Enabled = false;
                ImgBtnAccountNo.Enabled = false;
            }
        }
        #endregion

        //ucLCSE001
        #region DisplayAllPc
        private bool _isAllLocation = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAllLocation
        {
            set { _isAllLocation = value; }
        }
        public event EventHandler TextBoxLostFocus;
        private void ucLocationSearch_TextBoxLostFocus(object sender, EventArgs e)
        {

        }
        #endregion

        //ucLCSE001
        #region Search Raw Data - Without Refering Hierarchy
        private bool _isDisplayRawData = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDisplayRawData
        {
            get { return _isDisplayRawData; }
            set { _isDisplayRawData = value; }
        }
        #endregion


        public ucLoactionSearch()
        {
            _basePage = new Base();
            InitializeComponent();
            //ucLCSE001
            TextBoxLostFocus += new EventHandler(ucLocationSearch_TextBoxLostFocus);
        }
        private void ucLoactionSearch_ItemAdded(object sender, EventArgs e)
        {
            ItemAdded(sender, e);
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            _basePage = new Base();
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel:
                    {
                        paramsText.Append(TextBoxCompany.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel:
                    {
                        //ucLCSE001
                        if (_isDisplayRawData == false)
                            paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel.ToString() + seperator);
                        else
                            paramsText.Append(TextBoxCompany.Text + seperator + string.Empty + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel.ToString() + seperator);
                        break;

                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area:
                    {
                        //ucLCSE001
                        if (_isDisplayRawData == false)
                            paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + TextBoxSubChannel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area.ToString() + seperator);
                        else
                            paramsText.Append(TextBoxCompany.Text + seperator + string.Empty + seperator + string.Empty + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region:
                    {
                        //ucLCSE001
                        if (_isDisplayRawData == false)
                            paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + TextBoxSubChannel.Text + seperator + TextBoxArea.Text + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region.ToString() + seperator);
                        else
                            paramsText.Append(TextBoxCompany.Text + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone:
                    {
                        //ucLCSE001
                        if (_isDisplayRawData == false)
                            paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + TextBoxSubChannel.Text + seperator + TextBoxArea.Text + seperator + TextBoxRegion.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone.ToString() + seperator);
                        else
                            paramsText.Append(TextBoxCompany.Text + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + string.Empty + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        paramsText.Append(TextBoxCompany.Text + seperator + TextBoxChannel.Text + seperator + TextBoxSubChannel.Text + seperator + TextBoxArea.Text + seperator + TextBoxRegion.Text + seperator + TextBoxZone.Text + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(TextBoxCompany.Text.Trim() + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void ImgBtnAccountNo_Click(object sender, EventArgs e)
        {
            if (TextBoxCompany.Text.Trim() == "")
            {
                MessageBox.Show("Enter Company Code");
                return;
            }
            _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company);
            DataTable _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

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

            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
            DataTable _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

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

            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
            DataTable _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

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

            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area);
            DataTable _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

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

            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region);
            DataTable _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

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

            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone);
            DataTable _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(_CommonSearch.SearchParams, null, null);

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
            if (_isAllLocation == false)
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                DataTable _result = _basePage.CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
            }
            else
            {
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = _basePage.CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
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
            _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            TextBoxCompanyDes.Text = _CommonSearch.GetLoc_HIRC_SearchDesc(35, TextBoxCompany.Text.ToUpper());
            //GetLoc_HIRC_SearchDesc
            TextBoxLostFocus(sender, e);           //ucLCSE001
        }

        private void TextBoxChannel_Leave(object sender, EventArgs e)
        {
            if (TextBoxCompany.Text.Trim() == "")
            {
                MessageBox.Show("Enter Company Code");
                return;
            }
            _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            TextBoxChannelDes.Text = _CommonSearch.GetLoc_HIRC_SearchDesc(36, TextBoxChannel.Text.ToUpper());
            TextBoxLostFocus(sender, e);           //ucLCSE001
        }

        private void TextBoxSubChannel_Leave(object sender, EventArgs e)
        {
            if (TextBoxCompany.Text.Trim() == "")
            {
                MessageBox.Show("Enter Company Code");
                return;
            }
            _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            TextBoxSubChannelDes.Text = _CommonSearch.GetLoc_HIRC_SearchDesc(37, TextBoxSubChannel.Text.ToUpper());
            TextBoxLostFocus(sender, e);           //ucLCSE001
        }

        private void TextBoxArea_Leave(object sender, EventArgs e)
        {
            if (TextBoxCompany.Text.Trim() == "")
            {
                MessageBox.Show("Enter Company Code");
                return;
            }
            _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            TextBoxAreaDes.Text = _CommonSearch.GetLoc_HIRC_SearchDesc(38, TextBoxArea.Text.ToUpper());
            TextBoxLostFocus(sender, e);           //ucLCSE001
        }

        private void TextBoxRegion_Leave(object sender, EventArgs e)
        {
            if (TextBoxCompany.Text.Trim() == "")
            {
                MessageBox.Show("Enter Company Code");
                return;
            }
            _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            TextBoxRegionDes.Text = _CommonSearch.GetLoc_HIRC_SearchDesc(39, TextBoxRegion.Text.ToUpper());
            TextBoxLostFocus(sender, e);           //ucLCSE001
        }

        private void TextBoxZone_Leave(object sender, EventArgs e)
        {
            if (TextBoxCompany.Text.Trim() == "")
            {
                MessageBox.Show("Enter Company Code");
                return;
            }
            _basePage = new Base();
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            TextBoxZoneDes.Text = _CommonSearch.GetLoc_HIRC_SearchDesc(40, TextBoxZone.Text.ToUpper());
            TextBoxLostFocus(sender, e);           //ucLCSE001
        }

        private void TextBoxLocation_Leave(object sender, EventArgs e)
        {
            if (TextBoxCompany.Text.Trim() == "")
            {
                MessageBox.Show("Enter Company Code");
                return;
            }
            if (_isAllLocation == false)
            {
                _basePage = new Base();
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                TextBoxLocationDes.Text = _CommonSearch.GetLoc_HIRC_SearchDesc(41, TextBoxLocation.Text.ToUpper());
            }
            else
            {
                //TODO: Load description of the PC - ucLCSE001
            }
            TextBoxLostFocus(sender, e);           //ucLCSE001
        }



        private void TextBoxCompany_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ImgBtnAccountNo_Click(null, null);
        }

        private void TextBoxChannel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.imgChaSearch_Click(null, null);
        }

        private void TextBoxSubChannel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.imgSubChaSearch_Click(null, null);
        }

        private void TextBoxArea_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.imgAreaSearch_Click(null, null);
        }

        private void TextBoxRegion_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.imgRegionSearch_Click(null, null);
        }

        private void TextBoxZone_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.imgZoneSearch_Click(null, null);
        }

        private void TextBoxLocation_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.imgProCeSearch_Click(null, null);
        }

        private void TextBoxCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                this.ImgBtnAccountNo_Click(null, null);
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
            if (e.KeyChar != (char)13)
            {
                return;
            }
            else
            {
                // ItemAdded(sender, e);
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                TextBoxCompany.Focus();
            }
        }
    }
}
