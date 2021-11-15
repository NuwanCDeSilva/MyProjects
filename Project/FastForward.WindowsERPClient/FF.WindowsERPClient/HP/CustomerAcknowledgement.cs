using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.HP
{
    public partial class CustomerAcknowledgement : Base
    {
        List<string> tmpListnew = new List<string>();
        public CustomerAcknowledgement()
        {
            InitializeComponent();
        }

        //private void btnSearchLocation_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
        //        _CommonSearch.ReturnIndex = 0;
        //        _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
        //        DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
        //        _CommonSearch.dvResult.DataSource = _result;
        //        _CommonSearch.BindUCtrlDDLData(_result);
        //        _CommonSearch.obj_TragetTextBox = txtLocation;
        //        _CommonSearch.txtSearchbyword.Text = txtLocation.Text;
        //        _CommonSearch.ShowDialog();

        //    }
        //    catch (Exception err)
        //    {
        //        Cursor.Current = Cursors.Default;
        //        CHNLSVC.CloseChannel();
        //        MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void btnScheme_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllScheme);
                DataTable _result = CHNLSVC.CommonSearch.GetAllScheme(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtScheme;
                _CommonSearch.ShowDialog();
                txtScheme.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {

            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(txtComp.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllScheme:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + txtRegion.Text + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(txtComp.Text + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + txtArea.Text + seperator + txtRegion.Text + seperator + txtZone.Text + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
            
                
                update_PC_List();
                    //validation
                if (string.IsNullOrEmpty(txtPC.Text) && tmpListnew.Count <= 0)
                {
                    MessageBox.Show("Please select Profit Center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //schema update
                string _scheme = "";
                if (chkAllScheme.Checked)
                {
                    _scheme = "ALL";
                }
                else
                {
                    if (lstSchemes.Items.Count > 0)
                    {
                        foreach (ListViewItem _lstitm in lstSchemes.Items)
                        {
                            if (_lstitm.Checked)
                            {
                                _scheme = _scheme + _lstitm.Text + ",";
                            }
                        }
                    }
                }

                if (string.IsNullOrEmpty(_scheme))
                {
                    MessageBox.Show("Please select scheme details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                decimal _margin = 0;
                try
                {
                    _margin = Convert.ToDecimal(txtClsBalance.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Closing balance has to be valid number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string _error = "";
                gvAccount.AutoGenerateColumns = false;
                DataTable odt = new DataTable();
                foreach (var pc in tmpListnew)
                {
                    DataTable murgetb = new DataTable();
                   
                    murgetb = CHNLSVC.Sales.GetAccountAcknoledge(BaseCls.GlbUserComCode, pc, dtFrom.Value.Date, dtTo.Value.Date, _scheme, _margin, out _error);
                    odt.Merge(murgetb);
                }

                //gvAccount.DataSource = CHNLSVC.Sales.GetAccountAcknoledge(BaseCls.GlbUserComCode, txtLocation.Text, dtFrom.Value.Date, dtTo.Value.Date, _scheme, _margin, out _error);
                gvAccount.DataSource = odt;
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtScheme_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtScheme.Text))
                {

                    HpSchemeDetails _tmpSch = new HpSchemeDetails();
                    _tmpSch = CHNLSVC.Sales.getSchemeDetByCode(txtScheme.Text.Trim());

                    if (_tmpSch.Hsd_cd == null)
                    {
                        MessageBox.Show("Invalid scheme.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtScheme.Text = "";
                        txtScheme.Focus();
                        return;
                    }

                    if (_tmpSch.Hsd_act == false)
                    {
                        MessageBox.Show("Inactive scheme.", "Hire sales reversal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtScheme.Text = "";
                        txtScheme.Focus();
                        return;
                    }

                    //txtNewSch.Text = _tmpSch.Hsd_sch_tp;

                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddScheme_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtScheme.Text)) {
                return;
            }


            lstSchemes.Items.Add(txtScheme.Text);
            foreach (ListViewItem _lstView in lstSchemes.Items)
            {
                _lstView.Checked = true;
            }
        }

        private void txtClsBalance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CustomerAcknowledgement formnew = new CustomerAcknowledgement();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
        }

        private void lnkAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem lstview in lstSchemes.Items) {
                lstview.Checked = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (ListViewItem lstview in lstSchemes.Items)
            {
                lstview.Checked = false;
            }
        }

        private void lnkClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lstSchemes.Items.Clear();
        }

        private void update_PC_List()
        {
          
             string _tmpPC = "";
            BaseCls.GlbReportProfit = "";
            tmpListnew.Clear();

            if (lstPC.CheckedItems.Count > 0)
            {
              

                foreach (ListViewItem Item in lstPC.Items)
                {
                    List<string> tmpList = new List<string>();
                    tmpList = Item.Text.Split(new string[] { "|" }, StringSplitOptions.None).ToList();

                    string pc = null;
                    string com = txtComp.Text;
                    if ((tmpList != null) && (tmpList.Count > 0))
                    {
                        pc = tmpList[0];

                    }


                    if (Item.Checked == true)
                    {
                        tmpListnew.Add(pc);
                    }
                }
            }
            else
            {
                tmpListnew.Add(txtPC.Text.ToString());
            }

          
        }
        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
             

                //get selected account
                gvAccount.EndEdit();
                List<string> _accList = new List<string>();
                DataTable _dt = new DataTable();
                _dt.TableName = "aa";

                _dt.Columns.Add("p_ack_user");
                _dt.Columns.Add("p_ack_accno");
                _dt.Columns.Add("p_ack_create_dt");
                _dt.Columns.Add("p_ack_cust_cd");
                _dt.Columns.Add("p_ack_cust_name");
                _dt.Columns.Add("p_ack_cust_add1");
                _dt.Columns.Add("p_ack_cust_add2");
                _dt.Columns.Add("p_ack_guar1_name");
                _dt.Columns.Add("p_ack_guar1_add1");
                _dt.Columns.Add("p_ack_guar1_add2");
                _dt.Columns.Add("p_ack_guar2_name");
                _dt.Columns.Add("p_ack_guar2_add1");
                _dt.Columns.Add("p_ack_guar2_add2");
                _dt.Columns.Add("p_ack_item_cd");
                _dt.Columns.Add("p_ack_hire_val");
                _dt.Columns.Add("p_ack_diriya_val");
                _dt.Columns.Add("p_ack_loc_cd");
                _dt.Columns.Add("p_ack_print_add_tp");
                _dt.Columns.Add("p_com");
                _dt.Columns.Add("p_itm_desc");
                string _type = "";
                if (rdoCus.Checked)
                {
                    _type = "C";
                }
                if (rdoGuar1.Checked)
                {
                    _type = "G1";

                }
                if (rdoGuar2.Checked)
                {
                    _type = "G2";
                }


                foreach (DataGridViewRow gvr in gvAccount.Rows)
                {
                    DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells[0];
                    if (chkSelect.Value != null && chkSelect.Value.ToString().ToUpper() == "TRUE")
                    {
                        string _acc = gvr.Cells[1].Value.ToString();
                        _accList.Add(_acc);

                        DataRow _dr = _dt.NewRow();
                        _dr[0] = BaseCls.GlbUserID;
                        _dr[1] = gvr.Cells[1].Value != null ? gvr.Cells[1].Value.ToString() : "";
                        _dr[2] = DateTime.Now.Date;
                        _dr[3] = gvr.Cells[17] != null ? gvr.Cells[17].Value.ToString() : "";
                        _dr[4] = gvr.Cells[8].Value != null ? gvr.Cells[8].Value.ToString() : "";
                        _dr[5] = gvr.Cells[9].Value != null ? gvr.Cells[9].Value.ToString() : "";
                        _dr[6] = gvr.Cells[10].Value != null ? gvr.Cells[10].Value.ToString() : "";

                        _dr[7] = gvr.Cells[11] != null ? gvr.Cells[11].Value.ToString() : "";
                        _dr[8] = gvr.Cells[12] != null ? gvr.Cells[12].Value.ToString() : "";
                        _dr[9] = gvr.Cells[13] != null ? gvr.Cells[13].Value.ToString() : "";

                        _dr[10] = gvr.Cells[14] != null ? gvr.Cells[14].Value.ToString() : "";
                        _dr[11] = gvr.Cells[15] != null ? gvr.Cells[15].Value.ToString() : "";
                        _dr[12] = gvr.Cells[16] != null ? gvr.Cells[16].Value.ToString() : "";

                        _dr[13] = gvr.Cells[3] != null ? gvr.Cells[3].Value.ToString() : "";
                        _dr[14] = gvr.Cells[18] != null ? gvr.Cells[18].Value.ToString() : "";
                        _dr[15] = gvr.Cells[7] != null ? gvr.Cells[7].Value.ToString() : "";
                       // _dr[16] = txtLocation.Text;
                        _dr[16] = gvr.Cells[20] != null ? gvr.Cells[20].Value.ToString() : "";
                        _dr[17] = _type;
                        _dr[18] = BaseCls.GlbUserComCode;
                        _dr[19] = gvr.Cells[19] != null ? gvr.Cells[19].Value.ToString() : "";

                        _dt.Rows.Add(_dr);
                    }

                }
                if (_accList == null || _accList.Count <= 0)
                {
                    MessageBox.Show("Please select account\\s to process", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //delete
                CHNLSVC.Inventory.DeleteAccountAcknoledge(BaseCls.GlbUserID);

                //process
                string _error = "";
                //insert to temp
                string _invErr = "";
                int _effect1 = CHNLSVC.Inventory.SaveAccountAcknoledge(_dt, out _invErr);
                if (_effect1 == -1)
                {
                    MessageBox.Show("Error occured while processing\n" + _invErr, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int _effect = CHNLSVC.Sales.UpdateAcknoledgementPrintCount(_accList, out _error);
                if (_effect == -1)
                {
                    MessageBox.Show("Error occured while processing\n" + _error, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Reports.HP.clsHpSalesRep __rep = new Reports.HP.clsHpSalesRep();
                //__rep.CustomerAcknowledgementPrintReport();

                Reports.HP.ReportViewerHP _view = new Reports.HP.ReportViewerHP();
                if (BaseCls.GlbUserComCode == "SGL" || BaseCls.GlbUserComCode == "SGD")
                    BaseCls.GlbReportName = "Cust_Acknowledgement_Report_SGL.rpt";
                else
                    BaseCls.GlbReportName = "Cust_Acknowledgement_Report.rpt";

                _view.Show();
                _view = null;
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //open rpt
            }

        private void lnkGridAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataGridViewRow gr in gvAccount.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gvAccount.Rows[gr.Index].Cells[0];
                chk.Value = "true";
            }
        }

        private void lnkGridNone_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (DataGridViewRow gr in gvAccount.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)gvAccount.Rows[gr.Index].Cells[0];
                chk.Value = "false";
            }
        }

        private void lnkGridClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            gvAccount.DataSource = null;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
          
            string com = "";
            //if (chkAllComp.Checked)
            //    com = "LRP";
            //else
            com = txtComp.Text;

            string chanel = txtChanel.Text;
            string subChanel = txtSChanel.Text;
            string area = txtArea.Text;
            string region = txtRegion.Text;
            string zone = txtZone.Text;
            string pc = txtPC.Text;

            Boolean _isChk = false;
            string _adminTeam = "";

            //lstPC.SubItems.Add("Com");
            //if (chkAllComp.Checked == false)
            lstPC.Clear();

            string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;

           
        
                //if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPS"))
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10044))
                {
                    DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(com, chanel, subChanel, area, region, zone, pc);
                    foreach (DataRow drow in dt.Rows)
                    {
                        if (!lstPC.Items.Equals(drow["PROFIT_CENTER"].ToString()))
                            lstPC.Items.Add(drow["PROFIT_CENTER"].ToString()) ;

                    }
                }
                else
                {
                    DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep(BaseCls.GlbUserID, com, chanel, subChanel, area, region, zone, pc);
                    foreach (DataRow drow in dt.Rows)
                    {
                        if (!lstPC.Items.Equals(drow["PROFIT_CENTER"].ToString()))
                            lstPC.Items.Add(drow["PROFIT_CENTER"].ToString() + "|" + drow["COMPANY"].ToString());
                    }
                }

                //if (chkAllComp.Checked)
                //{
                //    com = "";
                //    if (BaseCls.GlbUserComCode == "ABL")
                //    {
                //        com = "LRP";
                //    }
                //    if (BaseCls.GlbUserComCode == "SGL")
                //    {
                //        com = "SGD";
                //    }

                //    if (CHNLSVC.Security.Is_OptionPerimitted(com, BaseCls.GlbUserID, 10044))
                //    {
                //        DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(com, chanel, subChanel, area, region, zone, pc);
                //        foreach (DataRow drow in dt.Rows)
                //        {
                //            if (!lstPC.Items.Equals(drow["PROFIT_CENTER"].ToString()))
                //                lstPC.Items.Add(drow["PROFIT_CENTER"].ToString() + "|" + drow["COMPANY"].ToString());
                //        }
                //    }
                //    else
                //    {
                //        DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep(BaseCls.GlbUserID, com, chanel, subChanel, area, region, zone, pc);
                //        foreach (DataRow drow in dt.Rows)
                //        {
                //            if (!lstPC.Items.Equals(drow["PROFIT_CENTER"].ToString()))
                //                lstPC.Items.Add(drow["PROFIT_CENTER"].ToString() + "|" + drow["COMPANY"].ToString());
                //        }
                //    }
                //}
            
        }

        private void CustomerAcknowledgement_Load(object sender, EventArgs e)
        {
            txtComp.Text = BaseCls.GlbUserComCode;
            
            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(txtComp.Text);
            if (_masterComp != null)
            {
                txtCompDesc.Text = _masterComp.Mc_desc;
            }
          
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

        private void button1_Click(object sender, EventArgs e)
        {
            lstPC.Clear();
        }

        private void txtChanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtChanel;
            _CommonSearch.ShowDialog();
            txtChanel.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "CHNL", txtChanel.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtChnlDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtChanel_MouseDoubleClick(null, null);
            }
        }

        private void txtSChanel_DoubleClick(object sender, EventArgs e)
        {

            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSChanel;
            _CommonSearch.ShowDialog();
            txtSChanel.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "SCHNL", txtSChanel.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtSChnlDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtSChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtSChanel_DoubleClick(null, null);
            }
        }

        private void txtArea_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtArea;
            _CommonSearch.ShowDialog();
            txtArea.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "AREA", txtArea.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtAreaDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtArea_DoubleClick(null, null);
            }
        }

        private void txtRegion_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtRegion;
            _CommonSearch.ShowDialog();
            txtRegion.Select();


            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "REGION", txtRegion.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtRegDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtRegion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtRegion_DoubleClick(null, null);
            }
        }

        private void txtZone_DoubleClick(object sender, EventArgs e)
        {
            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtZone;
            _CommonSearch.ShowDialog();
            txtZone.Select();

            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "ZONE", txtZone.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtZoneDesc.Text = row2["descp"].ToString();
            }
        }

        private void txtZone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtZone_DoubleClick(null, null);
            }
        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                txtPC_DoubleClick(null, null);
            }
            if (e.KeyCode == Keys.Enter)
            {
                load_PCDesc();
            }
        }

        private void txtPC_DoubleClick(object sender, EventArgs e)
        {

            if (txtComp.Text == "")
            {
                MessageBox.Show("Select the Company", "Reports", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location);
            DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtPC;
            _CommonSearch.ShowDialog();
            txtPC.Select();

            load_PCDesc();
        }

        private void txtPC_Leave(object sender, EventArgs e)
        {
            load_PCDesc();
        }
        private void load_PCDesc()
        {
            txtPCDesn.Text = "";
            DataTable LocDes = CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "PC", txtPC.Text);
            foreach (DataRow row2 in LocDes.Rows)
            {
                txtPCDesn.Text = row2["descp"].ToString();
            }
        }
    }
}
