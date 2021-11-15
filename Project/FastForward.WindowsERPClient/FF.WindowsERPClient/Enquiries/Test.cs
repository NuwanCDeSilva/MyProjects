using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FF.WindowsERPClient.Enquiries
{
    public partial class Test : Base
    {
        public Test()
        {
            InitializeComponent();
            //BaseCls.GlbUserComCode = "AAL";
        }

        #region Common Searching Area
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
                case CommonUIDefiniton.SearchUserControlType.AvailableSerial:
                    {
                        //paramsText.Append(GlbUserComCode + seperator + GlbUserDefLoca + seperator + txtItem.Text + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = textBox1;
            _CommonSearch.txtSearchbyword.Text = textBox1.Text; 
            _CommonSearch.ShowDialog();
            textBox1.Select();
        }

        private void Test_Load(object sender, EventArgs e)
        {
            this.Text = "Loc : " +  BaseCls.GlbUserDefLoca + ", Bin : " + BaseCls.GlbDefaultBin;
        }
    }
}
