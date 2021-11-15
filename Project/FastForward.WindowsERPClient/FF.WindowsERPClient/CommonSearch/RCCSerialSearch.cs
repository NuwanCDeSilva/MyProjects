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
using FF.WindowsERPClient.CommonSearch;
using FF.WindowsERPClient.Services;


namespace FF.WindowsERPClient.CommonSearch
{
    public partial class RCCSerialSearch : Base
    {
        RCC_Entry _objRCC = new RCC_Entry();
        ACInstallRequest _objAcIns = new ACInstallRequest();
        static string RCC_Type;
        static Boolean Is_Oth_Loc;
        static string Fix_Loc;

        public RCCSerialSearch()
        {
            InitializeComponent();
            gvSerial.AutoGenerateColumns = false;
            cmbCriteria.SelectedIndex = 0;

        }

        public void setInitValues(string _rccType, Boolean _isOthLoc, string _fixLoc)
        {
            RCC_Type = _rccType;
            Is_Oth_Loc = _isOthLoc;
            Fix_Loc = _fixLoc;
        }


        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion

        private void btn_View_Click(object sender, EventArgs e)
        {
            string _error = string.Empty;
            string _txt = "";
            string _nser="";
            string _nwar="";
            string _ninv="";
            string _ndoc="";

            try
            {
                if (cmbCriteria.Text == "")
                {
                    MessageBox.Show("Select Criteria !", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                _txt = txtSearch.Text;
                if (txtSearch.ToString().Contains("%"))
                {
                    if (_txt.ToString().Length < 6)
                    {
                        MessageBox.Show("Please enter minimun 5 characters !", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSearch.Focus();
                        return;
                    }
                }
                else
                {
                    if (_txt.ToString().Length < 5)
                    {
                        MessageBox.Show("Please enter minimun 5 characters !", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSearch.Focus();
                        return;
                    }
                }
                if (string.IsNullOrEmpty(txtSearch.Text)) { MessageBox.Show("Please select the search text", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

                Cursor.Current = Cursors.WaitCursor;
                //txtSearch.Text = txtSearch.Text.Remove(txtSearch.Text.ToString().LastIndexOf("%"), ("%").Length);   //kapila 7/12/2015
                if (RCC_Type == "Customer Items" || RCC_Type == "Installation")       //customer item
                {
                    if (Is_Oth_Loc == false)         //same location sale
                    {
                        gvSerial.DataSource = CHNLSVC.Inventory.GetRCCSerialSearchData_Customer(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, 1, 0, txtSearch.Text, cmbCriteria.Text);

                        if (gvSerial.Rows.Count == 0)
                        {
                            if(cmbCriteria.Text=="SERIAL") _nser=txtSearch.Text;
                            if(cmbCriteria.Text=="WARRANTY") _nwar=txtSearch.Text;
                            if(cmbCriteria.Text=="DOCUMENT") _ndoc=txtSearch.Text;
                            if(cmbCriteria.Text=="INVOICE NO") _ninv=txtSearch.Text;
                            //kapila 19/6/2017
                            if (!(string.IsNullOrEmpty(_nser)) && string.IsNullOrEmpty(_nwar) && string.IsNullOrEmpty(_ninv) && string.IsNullOrEmpty(_ndoc))
                            {
                                DataTable _dtSerMst = CHNLSVC.Inventory.GetINRSERMST_Rcc(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, _nser, _nwar, _ninv, _ndoc);
                                gvSerial.DataSource = _dtSerMst;
                                if (_dtSerMst.Rows.Count == 0)
                                    //if (MessageBox.Show("Details not found in warranty item list. Do you want to search in Non-warranty item list?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    //{
                                    //    gvSerial.DataSource = CHNLSVC.Inventory.GetRCCSerialSearchData_Stock(BaseCls.GlbUserComCode,BaseCls.GlbUserDefLoca, 1, 0, txtSearch.Text, cmbCriteria.Text);
                                    //}
                                    //kapila 12/12/2015
                                    MessageBox.Show("Details not found in warranty item list", "RCC Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Details not found in warranty item list", "RCC Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else   //other location sale
                    {
                        gvSerial.DataSource = CHNLSVC.Inventory.GetRCCSerialSearchData_Customer(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, 0, 0, txtSearch.Text, cmbCriteria.Text);

                        if (gvSerial.Rows.Count == 0)
                        {
                            //updated by akila 2017/10/07

                            if (cmbCriteria.Text == "SERIAL") _nser = txtSearch.Text;
                            if (cmbCriteria.Text == "WARRANTY") _nwar = txtSearch.Text;
                            if (cmbCriteria.Text == "DOCUMENT") _ndoc = txtSearch.Text;
                            if (cmbCriteria.Text == "INVOICE NO") _ninv = txtSearch.Text;

                            if (!(string.IsNullOrEmpty(_nser)) && string.IsNullOrEmpty(_nwar) && string.IsNullOrEmpty(_ninv) && string.IsNullOrEmpty(_ndoc))
                            {
                                DataTable _dtSerMst = new DataTable();
                                _dtSerMst = CHNLSVC.Inventory.GetINRSERMST_Rcc(null, null, _nser, _nwar, _ninv, _ndoc);
                                if (_dtSerMst.Rows.Count > 0)
                                {
                                    gvSerial.DataSource = _dtSerMst;
                                }
                                else
                                {
                                    if (MessageBox.Show("Details not found in warranty item list. Do you want to search in Non-warranty item list?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        gvSerial.DataSource = CHNLSVC.MsgPortal.GetRCCSerialSearchData_Stock(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, 0, 0, txtSearch.Text, cmbCriteria.Text);
                                    }
                                }
                            }
                            else
                            {
                                if (MessageBox.Show("Details not found in warranty item list. Do you want to search in Non-warranty item list?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    gvSerial.DataSource = CHNLSVC.MsgPortal.GetRCCSerialSearchData_Stock(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, 0, 0, txtSearch.Text, cmbCriteria.Text);
                                }
                            }

                        }
                    }
                }
                if (RCC_Type == "Fixed Asset")
                {
                    gvSerial.DataSource = CHNLSVC.Inventory.GetRCCSerialSearchData_STN(BaseCls.GlbUserComCode, Fix_Loc, 0, 0, txtSearch.Text, cmbCriteria.Text, out _error);
                }
                if (RCC_Type == "Stock Item")
                {
                    gvSerial.DataSource = CHNLSVC.MsgPortal.GetRCCSerialSearchData_Stock(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, 1, 1, txtSearch.Text, cmbCriteria.Text);

                    if (gvSerial.Rows.Count == 0)
                    {
                        if (MessageBox.Show("Details not found in warranty item list. Do you want to search in Non-warranty item list?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            gvSerial.DataSource = CHNLSVC.MsgPortal.GetRCCSerialSearchData_Stock(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, 1, 1, txtSearch.Text, cmbCriteria.Text);
                        }
                    }

                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(_error, "RCC Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }


        private void gvSerial_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvSerial.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in gvSerial.SelectedRows)
                {
                    if (Convert.ToString(row.Cells[22].Value) == "N")
                    {
                        MessageBox.Show("Warranty has been voided of this serial !", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (RCC_Type == "Installation")
                    {
                        _objAcIns.setSearchValues(Convert.ToString(row.Cells[0].Value), Convert.ToString(row.Cells[1].Value), Convert.ToString(row.Cells[2].Value), Convert.ToInt32(row.Cells[4].Value), Convert.ToInt32(row.Cells[5].Value), Convert.ToInt32(row.Cells[6].Value), Convert.ToInt32(row.Cells[7].Value), Convert.ToString(row.Cells[8].Value), Convert.ToString(row.Cells[9].Value), Convert.ToInt32(row.Cells[10].Value), Convert.ToString(row.Cells[11].Value), Convert.ToString(row.Cells[12].Value), Convert.ToString(row.Cells[13].Value), Convert.ToDateTime(row.Cells[14].Value), Convert.ToString(row.Cells[15].Value), 0, Convert.ToString(row.Cells[18].Value), Convert.ToString(row.Cells[19].Value), Convert.ToString(row.Cells[20].Value), Convert.ToString(row.Cells[21].Value));
                    }
                    else
                    {
                        //updated by akila 2018/03/26
                        if (RCC_Type == "Customer Items")       //customer item
                        {
                            _objRCC.setSearchValues(
                                row.Cells[0].Value == DBNull.Value ? string.Empty : Convert.ToString(row.Cells[0].Value),
                                row.Cells[1].Value == DBNull.Value ? string.Empty : Convert.ToString(row.Cells[1].Value),
                                row.Cells[2].Value == DBNull.Value ? string.Empty : Convert.ToString(row.Cells[2].Value),
                                row.Cells[4].Value == DBNull.Value ? 0: Convert.ToInt32(row.Cells[4].Value),
                                row.Cells[5].Value == DBNull.Value ? 0 : Convert.ToInt32(row.Cells[5].Value),
                                row.Cells[6].Value == DBNull.Value ? 0 : Convert.ToInt32(row.Cells[6].Value),
                                row.Cells[7].Value == DBNull.Value ? 0: Convert.ToInt32(row.Cells[7].Value),
                                row.Cells[8].Value == DBNull.Value ? string.Empty : Convert.ToString(row.Cells[8].Value),
                                row.Cells[9].Value == DBNull.Value ? string.Empty : Convert.ToString(row.Cells[9].Value),
                                row.Cells[10].Value == DBNull.Value ? 0: Convert.ToInt32(row.Cells[10].Value),
                                row.Cells[11].Value == DBNull.Value ? string.Empty : Convert.ToString(row.Cells[11].Value),
                                row.Cells[12].Value == DBNull.Value ? string.Empty : Convert.ToString(row.Cells[12].Value),
                                row.Cells[13].Value == DBNull.Value ? string.Empty : Convert.ToString(row.Cells[13].Value),
                                row.Cells[14].Value == DBNull.Value ? DateTime.MinValue.Date : Convert.ToDateTime(row.Cells[14].Value),
                                row.Cells[15].Value == DBNull.Value ? string.Empty : Convert.ToString(row.Cells[15].Value),
                                0,
                                row.Cells[18].Value == DBNull.Value ? string.Empty : Convert.ToString(row.Cells[18].Value),
                                row.Cells[19].Value == DBNull.Value ? string.Empty : Convert.ToString(row.Cells[19].Value),
                                row.Cells[20].Value == DBNull.Value ? string.Empty : Convert.ToString(row.Cells[20].Value),
                                row.Cells[21].Value == DBNull.Value ? string.Empty : Convert.ToString(row.Cells[21].Value)

                                /*Convert.ToString(row.Cells[0].Value
                                Convert.ToString(row.Cells[1].Value), 
                                Convert.ToString(row.Cells[2].Value), 
                                Convert.ToInt32(row.Cells[4].Value), 
                                Convert.ToInt32(row.Cells[5].Value), 
                                Convert.ToInt32(row.Cells[6].Value), 
                                Convert.ToInt32(row.Cells[7].Value), 
                                Convert.ToString(row.Cells[8].Value),
                                Convert.ToString(row.Cells[9].Value), 
                                Convert.ToInt32(row.Cells[10].Value), 
                                Convert.ToString(row.Cells[11].Value), 
                                Convert.ToString(row.Cells[12].Value), 
                                Convert.ToString(row.Cells[13].Value), 
                                Convert.ToDateTime(row.Cells[14].Value), 
                                Convert.ToString(row.Cells[15].Value), 
                                0, 
                                Convert.ToString(row.Cells[18].Value), 
                                Convert.ToString(row.Cells[19].Value), 
                                Convert.ToString(row.Cells[20].Value), 
                                Convert.ToString(row.Cells[21].Value)*/
                                );
                        }
                        else
                        {
                            _objRCC.setSearchValues(
                                row.Cells[0].Value == DBNull.Value ? string.Empty : Convert.ToString(row.Cells[0].Value),
                                row.Cells[1].Value == DBNull.Value ? string.Empty : Convert.ToString(row.Cells[1].Value),
                                row.Cells[2].Value == DBNull.Value ? string.Empty : Convert.ToString(row.Cells[2].Value),
                                row.Cells[4].Value == DBNull.Value ? 0 : Convert.ToInt32(row.Cells[4].Value),
                                row.Cells[5].Value == DBNull.Value ? 0 : Convert.ToInt32(row.Cells[5].Value),
                                row.Cells[6].Value == DBNull.Value ? 0 : Convert.ToInt32(row.Cells[6].Value),
                                row.Cells[7].Value == DBNull.Value ? 0 : Convert.ToInt32(row.Cells[7].Value),
                                string.Empty, string.Empty, 0, string.Empty, string.Empty, string.Empty, DateTime.Now.Date, string.Empty,
                                row.Cells[16].Value == DBNull.Value ? 0 : Convert.ToInt32(row.Cells[16].Value),
                                row.Cells[18].Value == DBNull.Value ? string.Empty : Convert.ToString(row.Cells[18].Value),
                                row.Cells[19].Value == DBNull.Value ? string.Empty : Convert.ToString(row.Cells[19].Value),
                                row.Cells[20].Value == DBNull.Value ? string.Empty : Convert.ToString(row.Cells[20].Value),
                                row.Cells[21].Value == DBNull.Value ? string.Empty : Convert.ToString(row.Cells[21].Value)

                                /*Convert.ToString(row.Cells[0].Value),
                                Convert.ToString(row.Cells[1].Value), 
                                Convert.ToString(row.Cells[2].Value), 
                                Convert.ToInt32(row.Cells[4].Value), 
                                Convert.ToInt32(row.Cells[5].Value), 
                                Convert.ToInt32(row.Cells[6].Value), 
                                Convert.ToInt32(row.Cells[7].Value), 
                                string.Empty, string.Empty, 0, string.Empty, string.Empty, string.Empty, DateTime.Now.Date, string.Empty, 
                                Convert.ToInt32(row.Cells[16].Value), 
                                Convert.ToString(row.Cells[18].Value), 
                                Convert.ToString(row.Cells[19].Value), 
                                Convert.ToString(row.Cells[20].Value), 
                                Convert.ToString(row.Cells[21].Value)*/
                                );
                        }
                    }
                }
            }
            this.Close();
        }

        private void cmbCriteria_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Enter)
                    txtSearch.Focus();
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            {
                if (e.KeyCode == Keys.Enter)
                    btn_View_Click(null, null);
            }
        }

        private void RCCSerialSearch_Load(object sender, EventArgs e)
        {
            cmbCriteria.SelectedText = "SERIAL";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }






    }
}
